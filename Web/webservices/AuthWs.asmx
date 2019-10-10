<%@ WebService Language="C#" Class="AuthWs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;
using RO.Web;

using System.Xml;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Net;

// Need to run the following 3 lines to generate AdminWs.cs for C# calls at Windows SDK v6.1: CMD manually if AdminWs.asmx is changed:
// C:\
// CD\Rintagi\RO\Service3
// "C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin\wsdl.exe" /nologo /namespace:RO.Service3 /out:"AdminWs.cs" "http://RND08/ROWs/AuthWs.asmx"

[ScriptService()]
[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class AuthWs : AsmxBase
{
    const byte systemId = 3;
    const int screenId = 0;
    const string programName = "Auth";

    protected override byte GetSystemId() { return systemId; }
    protected override int GetScreenId() { return screenId; }
    protected override string GetProgramName() { return programName; }
    protected override string GetValidateMstIdSPName() { throw new NotImplementedException(); }
    protected override string GetMstTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetDtlTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetMstKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    protected override string GetDtlKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
        
    protected override DataTable _GetMstById(string pid)
    {
        throw new NotImplementedException();
    }
    protected override DataTable _GetDtlById(string pid, int screenFilterId)
    {
        throw new NotImplementedException();
    }
    protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitDtl()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitMaster()
    {
        throw new NotImplementedException();
    }
    internal class LoginAttempt
    {
        public string challenge;
        public DateTime lastfailedTime;
        public int failedCount;
        public int challengeCount;
    }

    private const String KEY_CacheLUser = "Cache:LUser";
    private const String KEY_CacheLPref = "Cache:LPref";
    private const String KEY_CacheLImpr = "Cache:LImpr";
    private const String KEY_CacheLCurr = "Cache:LCurr";
    
    public AuthWs()
        : base()
    {
    }

    public class RequestHelpers
    {
        public static string GetClientIpAddress(HttpRequest request)
        {
            try
            {
                var userHostAddress = request.UserHostAddress;

                // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
                // Could use TryParse instead, but I wanted to catch all exceptions
                IPAddress.Parse(userHostAddress);

                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(xForwardedFor))
                    return userHostAddress;

                // Get a list of public ip addresses in the X_FORWARDED_FOR variable
                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();

                // If we found any, return the last one, otherwise return the user host address
                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }
            catch (Exception)
            {
                // Always return all zeroes for any failure (my calling code expects it)
                return "0.0.0.0";
            }
        }

        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
    }

    protected void SetJWTCookie(string refreshToken, bool includeHandle = true)
    {
        var context = HttpContext.Current;
        string appPath = context.Request.ApplicationPath;
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        var Response = context.Response;
        if (string.IsNullOrEmpty(refreshToken))
        {
            HttpCookie refreshTokenCookie = new HttpCookie("refresh_token", null);
            HttpCookie tokenInCookie = new HttpCookie("tokenInCookie", null);
            refreshTokenCookie.Expires = DateTime.UtcNow.AddSeconds(-999);
            tokenInCookie.Expires = DateTime.UtcNow.AddSeconds(-999);
            //Response.Headers.Add("Set-Cookie", "tokenInCookie=;Max-Age=-1");
            Response.Cookies.Add(tokenInCookie);
            Response.Cookies.Add(refreshTokenCookie);
        }
        else
        {
            HttpCookie refreshTokenCookie = new HttpCookie("refresh_token", refreshToken);
            refreshTokenCookie.HttpOnly = true;
            refreshTokenCookie.Path = appPath;
            refreshTokenCookie.Domain = domain;
            if (LUser != null && LUser.LoginName != null && includeHandle)
            {
                var sha256 = new SHA256Managed();
                var handle = Convert.ToBase64String(sha256.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(LUser.LoginName))).Replace("=", "_");
                HttpCookie tokenInCookie = new HttpCookie("tokenInCookie", handle);
                tokenInCookie.HttpOnly = false;
                tokenInCookie.Path = appPath;
                tokenInCookie.Domain = domain;
                //Response.Headers.Add("Set-Cookie", string.Format("tokenInCookie={0};Path={1}", handle, appPath));
                Response.Cookies.Add(tokenInCookie);
            }
            Response.Cookies.Add(refreshTokenCookie);
        }
    }

    [WebMethod(EnableSession = true)]
    public SerializableDictionary<string, string> LoginWithSession(string client_id, string scope, string grant_type, string code, string code_verifier, string redirect_url, string client_secret)
    {
        var context = HttpContext.Current;
        string appPath = context.Request.ApplicationPath;
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        HttpSessionState Session = HttpContext.Current.Session;
        System.Web.Caching.Cache cache = HttpContext.Current.Cache;
        string storedToken = (Session != null ? Session["RintagiLoginAccessCode"] as string : null) ?? cache[code] as string;
        bool syncCookie = true;
        string jwtMasterKey = System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"];
        Func<string, string> getStoredToken = (accessCode) =>
        {
            return storedToken;
        };
        Func<LoginUsr, UsrCurr, UsrImpr, bool, bool> validateScope = (loginUsr, usrCurr, usrImpr, ignoreCache) =>
        {
            LUser = loginUsr;
            LCurr = usrCurr;
            LImpr = usrImpr;
            return ValidateScope(ignoreCache);
        };
        if (grant_type == "refresh_token")
        {
            var refreshTokenInCookie = context.Request.Cookies["refresh_token"];
            var tokenInCookie = context.Request.Cookies["tokenInCookie"];
            if (tokenInCookie != null && tokenInCookie.Value == code && !string.IsNullOrEmpty(code))
            {
                grant_type = "refresh_token";
                code = refreshTokenInCookie.Value;
                syncCookie = true;
            }
        }
        var auth = GetAuthObject();
        var token = auth.GetToken(client_id, scope, grant_type, code, code_verifier, redirect_url, client_secret, appPath, domain, getStoredToken, validateScope);
        if (syncCookie && token != null)
        {
            if (Session != null)
            {
                Session[KEY_CacheLUser] = LUser;
                Session[KEY_CacheLImpr] = LImpr;
                Session[KEY_CacheLCurr] = LCurr;
            }
             
            SetJWTCookie(token["refresh_token"]);
        }
        return token;
    }
    
    /* new stuff */
    [WebMethod(EnableSession = true)]
    public SerializableDictionary<string, string> GetToken(string client_id, string scope, string grant_type, string code, string code_verifier, string redirect_url, string client_secret)
    {
        var context = HttpContext.Current;
        string appPath = context.Request.ApplicationPath;
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        HttpSessionState Session = HttpContext.Current.Session;
        System.Web.Caching.Cache cache = HttpContext.Current.Cache;
        string storedToken = (Session != null ? Session["RintagiLoginAccessCode"] as string : null) ?? cache[code] as string;
        bool syncCookie = true;
        string jwtMasterKey = System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"];
        Func<string,string> getStoredToken = (accessCode)=>{
            return storedToken;
        };
        Func<LoginUsr, UsrCurr, UsrImpr, bool, bool> validateScope = (loginUsr, usrCurr, usrImpr, ignoreCache) =>
        {
            LUser = loginUsr;
            LCurr = usrCurr;
            LImpr = usrImpr;
            return ValidateScope(ignoreCache);
        };

        if (grant_type == "refresh_token")
        {
            var refreshTokenInCookie = context.Request.Cookies["refresh_token"];
            var tokenInCookie = context.Request.Cookies["tokenInCookie"];
            if (tokenInCookie != null && tokenInCookie.Value == code && !string.IsNullOrEmpty(code))
            {
                grant_type = "refresh_token";
                code = refreshTokenInCookie.Value;
                syncCookie = true;
            }
        }
        var auth = GetAuthObject();
        var token = auth.GetToken(client_id, scope, grant_type, code, code_verifier, redirect_url, client_secret, appPath, domain, getStoredToken, validateScope);
        if (syncCookie && token != null)
        {
            SetJWTCookie(token["refresh_token"]);
        }
        return token;
        
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, object> scopeContext = jss.Deserialize<Dictionary<string, object>>(scope);
        //byte? systemId = null;
        //int? companyId = null;
        //int? projectId = null;
        //short? cultureId = null;
        //int access_token_validity = 5 * 60; // 20 minutes
        //int refresh_token_validity = 60 * 60 * 24 * 14; // 14 days

        //try { systemId = byte.Parse(scopeContext["SystemId"].ToString()); }
        //catch { };
        //try { companyId = int.Parse(scopeContext["CompanyId"].ToString()); }
        //catch { };
        //try { projectId = int.Parse(scopeContext["ProjectId"].ToString()); }
        //catch { };
        //try { cultureId = short.Parse(scopeContext["ProjectId"].ToString()); }
        //catch { };

        //RintagiLoginJWT loginJWT = new Func<RintagiLoginJWT>(() =>
        //{
        //    if (grant_type == "authorization_code")
        //    {
        //        storedToken = (Session != null ? Session["RintagiLoginAccessCode"] as string : null) ?? cache[code] as string;
        //        try
        //        {
        //            return GetLoginUsrInfo(storedToken) ?? new RintagiLoginJWT();
        //        }
        //        catch
        //        {
        //        };
        //    }
        //    else if (grant_type == "refresh_token")
        //    {
        //        try
        //        {
        //            return GetLoginUsrInfo(code) ?? new RintagiLoginJWT();
        //        }
        //        catch { }
        //    }
        //    return new RintagiLoginJWT();
        //})();
        //var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //var currTime = DateTime.Now.ToUniversalTime().Subtract(utc0).TotalSeconds;
        //var nbf = loginJWT.nbf;
        //var expiredOn = loginJWT.exp;
        //int remainingSeconds = expiredOn - (int) currTime;
        //var currentHandle = loginJWT.handle;
        //bool keepRefreshToken = remainingSeconds > 120 && grant_type == "refresh_token";
        //if (currTime > nbf && currTime < expiredOn && ValidateJWTHandle(currentHandle))
        //{
        //    string signingKey = GetSessionSigningKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
        //    string encryptionKey = GetSessionEncryptionKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
        //    RintagiLoginToken loginToken = DecryptLoginToken(loginJWT.loginToken, encryptionKey);

        //    LCurr = new UsrCurr(companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId, systemId ?? loginToken.SystemId, systemId ?? loginToken.SystemId);
        //    LImpr = null;

        //    LImpr = SetImpersonation(LImpr, loginToken.UsrId, systemId ?? loginToken.SystemId, companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId);
        //    LUser = new LoginUsr();
        //    LUser.UsrId = loginToken.UsrId;
        //    LUser.DefCompanyId = loginToken.DefCompanyId;
        //    LUser.DefProjectId = loginToken.DefProjectId;
        //    LUser.DefSystemId = loginToken.DefSystemId;
        //    LUser.UsrName = loginToken.UsrName;
        //    LUser.InternalUsr = "Y";
        //    LUser.CultureId = 1;
        //    LUser.HasPic = false;
        //    string refreshTag = keepRefreshToken ? currentHandle : Guid.NewGuid().ToString().Replace("-", "").ToLower();
        //    string loginTag = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        //    if (ValidateScope(true))
        //    {
        //        string loginTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, access_token_validity, loginTag);
        //        string refreshTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, keepRefreshToken ? remainingSeconds : refresh_token_validity, refreshTag);
        //        string token_scope = string.Format("s{0}c{1}p{2}", LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
        //        var Token = new SerializableDictionary<string, string> {
        //        { "access_token", loginTokenJWT},
        //        { "token_type", "Bearer"},
        //        { "iat", currTime.ToString()},
        //        { "expires_in", (access_token_validity - 1).ToString()},
        //        { "scope", token_scope},
        //        { "resources", appPath},
        //        { "refresh_token", refreshTokenJWT},
        //        };
        //        if (Session != null)
        //        {
        //            Session.Remove("RintagiLoginAccessCode");
        //            Session[refreshTag] = refreshTokenJWT;
        //        }
        //        return Token;
        //    }
        //    else
        //    {
        //        return new SerializableDictionary<string, string>() {
        //        { "error", "access_denied"},
        //        { "message", "cannot issue token"},
        //        };
        //    }
        //}
        //return new SerializableDictionary<string, string>() {
        //        { "error", "invalid_token"},
        //        { "message", "cannot issue token"},
        //};
    }

    [WebMethod(EnableSession = true)]
    public ApiResponse<SerializableDictionary<string, string>, object> Logout(string access_token, string refresh_token)
    {
        var context = HttpContext.Current;
        try
        {
            var info = GetLoginUsrInfo(refresh_token);
            var cache = context.Cache;
            var refreshTokenHandle = info.handle;
            cache.Remove(refreshTokenHandle);
            // global flush refresh token handle(if needed)
        }
        catch { }
        try
        {
            LoadUserSession();
            var cache = context.Cache;
            if (loginHandle != null) cache.Remove(loginHandle);
            // global flush access token handle(if needed)
        }
        catch { }
        try
        {
            if (context.Session != null)
            {
                context.Session.Abandon();
            }

        }
        catch { }
        SetJWTCookie(null, true);
        return new ApiResponse<SerializableDictionary<string, string>, object>() { status = "success", data = { } };
    }

    [WebMethod(EnableSession = true)]
    public LoginApiResponse Login(string client_id, string usrName, string password, string nonce, string code_challenge_method, string code_challenge)
    {
        try
        {
            var usrIP = RequestHelpers.GetClientIpAddress(HttpContext.Current.Request);
            var attemptKey = "LoginAttempt_" + usrName + usrIP;
            var challengeCount = 50000; // delay client by 2 seconds on so, depending on hardware
            LoginAttempt lastFailedTry = HttpContext.Current.Cache[attemptKey] as LoginAttempt;
            if (lastFailedTry != null)
            {
                byte[] k = new Rfc2898DeriveBytes(UTF8Encoding.UTF8.GetBytes(lastFailedTry.challenge), UTF8Encoding.UTF8.GetBytes(lastFailedTry.challenge), lastFailedTry.challengeCount).GetBytes(32);
                var x = Convert.ToBase64String(k);
                if (client_id != x)
                {
                    return new LoginApiResponse() { message = "bot detected", status = "failed", error = "access_denied", challengeCount = lastFailedTry.challengeCount, serverChallenge = lastFailedTry.challenge };
                }
            }

            Credential cr = new Credential(usrName, password);
            LoginUsr usr = (new LoginSystem()).GetLoginSecure(cr);
            if (usr == null)
            {
                if (!(new LoginSystem()).IsNullLegacyPwd(usrName))
                {
                    usr = (new LoginSystem()).GetLoginLegacy(usrName, password);
                }
            }

            if (usr != null && usr.UsrId > 0)
            {
                if ((new LoginSystem()).ChkLoginStatus(usr.LoginName))
                {
                    LUser = usr;
                    LCurr = new UsrCurr(usr.DefCompanyId, usr.DefProjectId, usr.DefSystemId, usr.DefSystemId);
                    LImpr = SetImpersonation(null, usr.UsrId, usr.DefSystemId, usr.DefCompanyId, usr.DefProjectId);
                    
                    if (Session != null && ((Session[KEY_CacheLUser] as LoginUsr) == null || (Session[KEY_CacheLUser] as LoginUsr).UsrId == 1))
                    {
                        try
                        {
                            // simulate Form Login
                            System.Web.Security.FormsAuthenticationTicket Ticket = new System.Web.Security.FormsAuthenticationTicket(LUser.LoginName, false, 3600);
                            System.Web.Security.FormsAuthentication.SetAuthCookie(LUser.LoginName, false);
                            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Web.Security.FormsIdentity(Ticket), null);
                            Session[KEY_CacheLUser] = LUser;
                            Session[KEY_CacheLImpr] = LImpr;
                            Session[KEY_CacheLCurr] = LCurr;
                        }
                        catch { }
                    }
                    string guid = Guid.NewGuid().ToString();
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    var referrer = HttpContext.Current.Request.UrlReferrer;                    
                    string jwtToken = CreateLoginJWT(LUser, usr.DefCompanyId, usr.DefProjectId, usr.DefSystemId, LCurr, LImpr, appPath, 10 * 60, guid);
                    HttpContext.Current.Cache.Add(guid, jwtToken, null
                        , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 1, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    var access_token = GetToken("", "", "authorization_code", guid, "", "", "");
                    var accessTokenJWT = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(access_token);
                    HttpContext.Current.Cache.Remove(attemptKey);
                    if (referrer != null && referrer.PathAndQuery.StartsWith(appPath) && referrer.Host == domain)
                    {
                        SetJWTCookie(access_token["refresh_token"], true);
                    }
                    return new LoginApiResponse() { message = "login successful", accessCode = guid, accessToken = access_token, refreshToken = null, status = "success" };
                }
                else
                {
                    //locked();
                    //return false;
                    return new LoginApiResponse() { message = "account locked", status = "failed", error = "access_denied" };
                }
            }//
            else
            {
                //(new LoginSystem()).SetLoginStatus(cLoginName.Text, false, GetVisitorIPAddress(), Provider, ProviderLoginName);
                //failed();
                //return false;
                var cache = HttpContext.Current.Cache;
                var challenge = Guid.NewGuid().ToString().Replace("-", "");
                if (lastFailedTry != null)
                {
                    lastFailedTry = new LoginAttempt() { challenge = challenge, lastfailedTime = DateTime.Now.ToUniversalTime(), challengeCount = lastFailedTry.challengeCount + challengeCount, failedCount = lastFailedTry.failedCount + 1 };
                }
                else
                {
                    lastFailedTry = new LoginAttempt() { challenge = challenge, lastfailedTime = DateTime.Now.ToUniversalTime(), failedCount = 0, challengeCount = 1 };
                    cache.Add(attemptKey, lastFailedTry, null
                        , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 2, 0), System.Web.Caching.CacheItemPriority.Normal, null);
                }
                //return new LoginApiResponse() { message = "bot challenge", status = "failed", error = "access_denied", serverChallenge = challenge, challengeCount = lastFailedTry.challengeCount };
                return new LoginApiResponse() { message = "bot challenge", status = "failed", error = "access_denied", serverChallenge = challenge, challengeCount = lastFailedTry.challengeCount };
            }
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
            var StackTrace = "";
            Dictionary<string, string> err = new Dictionary<string, string>();
            err.Add("type", "System Error");
            err.Add("message", errMsg + " " + StackTrace);
            return new LoginApiResponse() { message = errMsg, status = "failed", error = "server_error" };
        }
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> GetCurrentUsrInfo(string scope)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId, false, false, false,true);
            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();
            SerializableDictionary<string, string> usrInfo = new SerializableDictionary<string, string>();
            usrInfo["UsrId"] = LUser.UsrId.ToString();
            //            usrInfo["UsrEmail"] = LUser.UsrEmail.ToString();
            //usrInfo["LoginName"] = LUser.LoginName.ToString();
            usrInfo["UsrName"] = LUser.UsrName.ToString();
            usrInfo["MemberId"] = LImpr.Members;
            usrInfo["CustomerId"] = LImpr.Customers;
            usrInfo["BorrowerId"] = LImpr.Borrowers;
            usrInfo["LenderId"] = LImpr.Lenders;
            usrInfo["CompanyId"] = LCurr.CompanyId.ToString();
            usrInfo["ProjectId"] = LCurr.ProjectId.ToString();
            usrInfo["CultureId"] = LUser.CultureId.ToString();
            usrInfo["TimeZone"] = CurrTimeZoneInfo().ToString();

            //            usrInfo["UsrGroups"] = LImpr.UsrGroups;
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = usrInfo;
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;
    }

    protected TimeZoneInfo CurrTimeZoneInfo()
    {
        //return Session["Cache:tzInfo"] as TimeZoneInfo ?? TimeZoneInfo.Local;
        return TimeZoneInfo.Local;
    }

    [WebMethod(EnableSession = false)]
    public string GetUrlWithQSHash(string url)
    {
        int questionMarkPos = url.IndexOf('?');
        string path = questionMarkPos >= 0 ? url.Substring(0, questionMarkPos) : url;
        string qs = questionMarkPos >= 0 ? url.Substring(questionMarkPos).Substring(1) : "";

        if (string.IsNullOrEmpty(qs)) return url;
        if (!(path.ToLower().StartsWith("~/dnload.aspx") || path.ToLower().StartsWith("dnload.aspx") || path.ToLower().StartsWith("~/upload.aspx") || path.ToLower().StartsWith("upload.aspx"))) return url;

        /* url with hash to prevent tampering of manual construction, only for dnload.aspx */
        return path + "?" + qs + "&hash=" + GetQSHash(string.Join("&", qs.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).OrderBy(v => v.ToLower()).ToArray()).ToLower().Trim());
    }

    public string GetQSHash(string qs)
    {
        /* calculate the HMAC hash of a string based on unique SessionID and LUser Login Name */
        //byte[] code = System.Web.Security.MachineKey.Protect(System.Text.Encoding.UTF8.GetBytes(Session.SessionID + LUser.UsrId.ToString()), "QueryString");
        byte[] sessionSecret = Session["QSSecret"] as byte[];
        if (sessionSecret == null)
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[32];
            rng.GetBytes(tokenData);
            sessionSecret = tokenData;
            Session["QSSecret"] = tokenData;
        }
        //byte[] code = System.Text.Encoding.ASCII.GetBytes(sessionSecret + LUser.UsrId.ToString());
        byte[] code = sessionSecret;
        System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(code);
        byte[] hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(code) + qs.ToString()));
        string hashString = BitConverter.ToString(hash);
        return hashString.Replace("-", "");
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>> ResetPwdEmail(string emailAddress, string reCaptchaRequest, string refCode)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>>> fn = () =>
        {
            SwitchContext(2, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>>();
            var Request = HttpContext.Current.Request;
            string secret = System.Configuration.ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            bool isHuman = true || ReCaptcha.Validate(secret, reCaptchaRequest);
            DataTable dt = (new LoginSystem()).GetSaltedUserInfo(0, emailAddress, emailAddress);
            Guid g = Guid.NewGuid();
            string GuidString = g.ToString();
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(emailAddress))
            {
                var token = GetAuthObject().GetSignedToken(GuidString + emailAddress);
                string emailCnt = "";
                DataTable dtEmail = new AdminSystem().RunWrRule(0, "WrGetEmailCnt", LcAppConnString, LcAppPw, string.Format("<Params><templateId>{0}</templateId></Params>", "11"), LImpr, LCurr);
                string emlSubject = dtEmail.Rows[0]["EmailSubject"].ToString();
                string emlSenderTitle = dtEmail.Rows[0]["SenderName"].ToString();
                string emlHtml = dtEmail.Rows[0]["EmailTempCnt"].ToString();

                emailCnt = emlHtml.Replace("[[Token]]", token.Item1);
                string from = "no-reply@fintrux.com";
                base.SendEmail(emlSubject, emailCnt, emailAddress, from, from, emlSenderTitle, true);
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = new SerializableDictionary<string, string>() { { "nounce", GuidString + emailAddress }, { "ticketRight", token.Item2 } };
                return mr;
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Account not found.";
                mr.data = null;
                return mr;
            }
        };
        var result = ProtectedCall(fn, true);
        return result;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>> ResetPassword(string emailAddress, string password, string nounce, string ticketLeft, string ticketRight)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>>> fn = () =>
        {
            SwitchContext(2, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>>();
            bool isEmailVerified = GetAuthObject().VerifySignedToken(nounce, ticketLeft, ticketRight);
            if (!isEmailVerified || string.IsNullOrEmpty(emailAddress))
            {
                mr.status = "failed";
                mr.errorMsg = "email address not confirmed";
                mr.data = null;
                return mr;
            }
            var Request = HttpContext.Current.Request;

            /* we use sso provider for temp refcode */
            DataTable dt = (new LoginSystem()).GetSaltedUserInfo(0, emailAddress, emailAddress);
            string resetLoginName = dt.Rows[0]["LoginName"].ToString();
            
            if (!string.IsNullOrEmpty(resetLoginName))
            {
                Credential cr = new Credential(resetLoginName, password);

                if ((new LoginSystem()).UpdUsrPassword(cr, LUser, false))
                {
                    mr.status = "success";

                    try
                    {
                        DataTable dtEmail = new AdminSystem().RunWrRule(0, "WrGetEmailCnt", LcAppConnString, LcAppPw, string.Format("<Params><templateId>{0}</templateId></Params>", "12"), LImpr, LCurr);
                        string emlSubject = dtEmail.Rows[0]["EmailSubject"].ToString();
                        string emlSenderTitle = dtEmail.Rows[0]["SenderName"].ToString();
                        string emlHtml = dtEmail.Rows[0]["EmailTempCnt"].ToString();
                        string from = "no-reply@fintrux.com";
                        base.SendEmail(emlSubject, emlHtml, emailAddress, from, from, emlSenderTitle, true);
                    }
                    catch { }

                    return mr;
                }
                else
                {
                    mr.status = "failed";
                    mr.errorMsg = "Update User Password Failed";
                    return mr;
                }
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Invalid Reset Request";
                mr.data = null;
                return mr;
            }
            
        };
        var result = ProtectedCall(fn, true);
        return result;
    }    
}