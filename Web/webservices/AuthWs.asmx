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
using Fido2NetLib;
using Fido2NetLib.Objects;
using System.Threading.Tasks;


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

    private List<DataRow> LinkedUserLogin = new List<DataRow>();

    protected override byte GetSystemId() { return systemId; }
    protected override int GetScreenId() { return screenId; }
    protected override string GetProgramName() { return programName; }
    protected override string GetValidateMstIdSPName() { throw new NotImplementedException(); }
    protected override string GetMstTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetDtlTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetMstKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    protected override string GetDtlKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewMst() { throw new NotImplementedException(); }
    public override ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchList(string searchStr, int topN, string filterId, SerializableDictionary<string, string> desiredScreenCriteria){ throw new NotImplementedException(); }
    public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetDtlById(string keyId, SerializableDictionary<string, string> options, int filterId){ throw new NotImplementedException(); }
    public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetMstById(string keyId, SerializableDictionary<string, string> options){ throw new NotImplementedException(); }
        
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

    public class WebAuthnChallenge
    {
        public string nonce;
        public string usrId;
        public string appDomainUrl;
        public string appPath;
        public int expiredAfter;
    }

    public class WebAuthnMeta
    {
        public string credentialId;
        public string publicKey;
        public string aaguid;
        public string authenticatorType;
        public string appPath;
        public string usrId;
        public string platform;
        public string requestIPChain;
        public bool isMobile;
        public bool userPresence;
        public bool userVerified;
    }

    public class AuthenticatorAttestationRawResponseEx : AuthenticatorAttestationRawResponse
    {
        public string platform;
        public bool isMobile;
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
        HttpRequest Request = context.Request;
        string appPath = context.Request.ApplicationPath.ToUpper();
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        var Response = context.Response;
        /* below needs to be revised, FIXME 
        if (string.IsNullOrEmpty(refreshToken))
        {
            HttpCookie refreshTokenCookie = new HttpCookie(appPath.Replace("/","") + "_refresh_token", null);
            HttpCookie tokenInCookie = new HttpCookie(appPath.Replace("/", "") + "_tokenInCookie", null);
            refreshTokenCookie.Expires = DateTime.UtcNow.AddSeconds(-999);
            tokenInCookie.Expires = DateTime.UtcNow.AddSeconds(-999);
            //Response.Headers.Add("Set-Cookie", "tokenInCookie=;Max-Age=-1");
            Response.Cookies.Add(tokenInCookie);
            Response.Cookies.Add(refreshTokenCookie);
        }
        else
        {
            HttpCookie refreshTokenCookie = new HttpCookie(appPath.Replace("/", "") + "_refresh_token", refreshToken);
            refreshTokenCookie.HttpOnly = true;
            refreshTokenCookie.Path = "/";
            refreshTokenCookie.Secure = Request.IsSecureConnection;
            refreshTokenCookie.Domain = domain;
            if (LUser != null && LUser.LoginName != null && includeHandle)
            {
                var sha256 = new SHA256Managed();
                var handle = Convert.ToBase64String(sha256.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(LUser.LoginName))).Replace("=", "_");
                HttpCookie tokenInCookie = new HttpCookie(appPath + "_tokenInCookie", handle);
                tokenInCookie.HttpOnly = false;
                tokenInCookie.Path = "/";
                tokenInCookie.Domain = domain;
                //Response.Headers.Add("Set-Cookie", string.Format("tokenInCookie={0};Path={1}", handle, appPath));
                Response.Cookies.Add(tokenInCookie);
            }
            Response.Cookies.Add(refreshTokenCookie);
        }
        */
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
        string jwtMasterKey = Config.JWTMasterKey;
        Func<string, string> getStoredToken = (accessCode) =>
        {
            return storedToken;
        };
        Func<LoginUsr, UsrCurr, UsrImpr, UsrPref, string, bool, bool> validateScope = (loginUsr, usrCurr, usrImpr, usrPref, currentHandle, ignoreCache) =>
        {
            LUser = loginUsr;
            LCurr = usrCurr;
            LImpr = usrImpr;
            LPref = usrPref;
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
                Session[KEY_CacheLPref] = LPref;
                
            }
             
            SetJWTCookie(token["refresh_token"]);
        }
        return token;
    }
    
    /* new stuff */
    [WebMethod(EnableSession = true)]
    public SerializableDictionary<string, string> GetToken(string client_id, string scope, string grant_type, string code, string code_verifier, string redirect_url, string client_secret, string re_auth)
    {
        var context = HttpContext.Current;
        string appPath = context.Request.ApplicationPath;
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        int access_token_validity = LUser != null && LUser.PwdDuration == 23456 ? 60 * 60 * 24 * 750 : 10 * 60;
        HttpSessionState Session = HttpContext.Current.Session;
        System.Web.Caching.Cache cache = HttpContext.Current.Cache;
        string storedToken = (Session != null ? Session["RintagiLoginAccessCode"] as string : null) ?? cache[code] as string;
        bool syncCookie = true;
        string jwtMasterKey = Config.JWTMasterKey;
        Func<string,string> getStoredToken = (accessCode)=>{
            return storedToken;
        };
        Func<LoginUsr, UsrCurr, UsrImpr, UsrPref, string, bool, bool> validateScope = (loginUsr, usrCurr, usrImpr, usrPref, currentHandle, ignoreCache) =>
        {
            LUser = loginUsr;
            LCurr = usrCurr;
            LImpr = usrImpr;
            LPref = usrPref;
            if (re_auth == "Y")
            {
                try
                {
                    cache.Remove(currentHandle);
                }
                catch { }
            }
            var accountNotLocked = new LoginSystem().ChkLoginStatus(LUser.LoginName);
            return accountNotLocked && ValidateScope(ignoreCache);
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
        try
        {
            var auth = GetAuthObject();
            var token = auth.GetToken(client_id, scope, grant_type, code, code_verifier, redirect_url, client_secret, appPath, domain, getStoredToken, validateScope, re_auth == "Y", access_token_validity);
            if (syncCookie && token != null)
            {
                SetJWTCookie(token["refresh_token"]);
            }
            if (re_auth == "Y")
            {
                LoadUsrImpr(LUser.UsrId, LCurr.DbId, LCurr.CompanyId, LCurr.ProjectId, true);
            }

            if (token == null || !token.ContainsKey("access_token"))
            {
                ErrorTracing(new Exception(string.Format("Problem refresh token to {0}", LUser != null ? LUser.UsrId.ToString() : "unknown user")));
            }
            return token;
        }
        catch (Exception ex)
        {
            ErrorTracing(new Exception(string.Format("Problem refresh token to {0}", LUser != null ? LUser.UsrId.ToString() : "unknown user"), ex));
            return new SerializableDictionary<string, string>() {
                { "error", "invalid_token"},
                { "message", "cannot issue token"},
            };
        }
        
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

    protected LoginUsr SSOLogin(string SelectedLoginName, string ProviderLoginName, string Provider)
    {
        LoginUsr intendedUser = null;
        Credential cr = new Credential(SelectedLoginName, intendedUser != null ? intendedUser.UsrId.ToString() : ProviderLoginName, new byte[32], Provider.Left(5));
        LoginUsr usr = (new LoginSystem()).GetLoginSecure(cr);

        if (usr != null)
        {
            usr.OTPValidated = !string.IsNullOrEmpty(Provider);
        }
        return usr;
    }
    
    protected LoginApiResponse _Login(LoginUsr usr, string provider = null, string providerLoginName = null)
    {
        if ((new LoginSystem()).ChkLoginStatus(usr.LoginName))
        {
            LUser = usr;
            LCurr = new UsrCurr(usr.DefCompanyId, usr.DefProjectId, usr.DefSystemId, usr.DefSystemId);
            LImpr = SetImpersonation(null, usr.UsrId, usr.DefSystemId, usr.DefCompanyId, usr.DefProjectId);
            LPref = (new LoginSystem()).GetUsrPref(LUser.UsrId, LCurr.CompanyId, LCurr.ProjectId, LCurr.SystemId);
            (new LoginSystem()).SetLoginStatus(usr.LoginName, true, GetVisitorIPAddress(), provider, providerLoginName);

            if (!(new LoginSystem()).IsUsrSafeIP(usr.UsrId, GetVisitorIPAddress())) // Email the account holder.
            {
                string from = base.SysCustServEmail(3);
                var reset_url1 = GetResetLoginUrl(usr.UsrId.ToString(), "", "", "k", "&ip=" + HttpUtility.UrlEncode(GetVisitorIPAddress()), null, null);
                var reset_url2 = GetResetLoginUrl(usr.UsrId.ToString(), "", "", "j", "", null, null);
                string machineName = Environment.MachineName;
                string sBody = "Someone recently tried to login to your account '" + LUser.LoginName.Left(3) + "****" + LUser.LoginName.Right(3) + "' at <b>" 
                        + ResolveUrlCustom(HttpContext.Current.Request.Url.AbsolutePath, false, true) 
                        + string.Format(" (On Server {0})", machineName) + "</b> from an unrecognized IP location <b>" 
                        + GetVisitorIPAddress() 
                        + "</b>.<br /><br />You may choose to ignore this message or click <a href=" + reset_url1.Value + ">YES</a> if this IP Address location will be used again or click <a href=" + reset_url2.Value + ">NO</a> to reset your password immediately.";
                try
                {
                    base.SendEmail("Review Recent Login", sBody, usr.UsrEmail, from, from, Config.WebTitle + " Customer Care", true);
                }
                catch { }
            }
            
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
                    Session[KEY_CacheLPref] = LPref;
                }
                catch { }
            }
            string guid = Guid.NewGuid().ToString();
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var referrer = HttpContext.Current.Request.UrlReferrer;
            // service account(pwd duration of 23456) has two year validity
            int access_token_validity = LUser.PwdDuration == 23456 ? 60*60*24*750 : 60 * 10;
            string jwtToken = CreateLoginJWT(LUser, usr.DefCompanyId, usr.DefProjectId, usr.DefSystemId, LCurr, LImpr, appPath, access_token_validity, guid);
            HttpContext.Current.Cache.Add(guid, jwtToken, null
                , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 1, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            var access_token = GetToken("", "", "authorization_code", guid, "", "", "", "N");
            var accessTokenJWT = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(access_token);
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
                LoginApiResponse loginResponse = _Login(usr);
                if (loginResponse != null && loginResponse.status == "success")
                {
                    HttpContext.Current.Cache.Remove(attemptKey);
                }
                else
                {
                }
                return loginResponse;
            }//
            else
            {
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
            // this is getting a bit dangerous now
            usrInfo["Usrs"] = LImpr.Usrs.ToString();
            usrInfo["MemberId"] = LImpr.Members;
            usrInfo["CustomerId"] = LImpr.Customers;
            usrInfo["BorrowerId"] = LImpr.Borrowers;
            usrInfo["LenderId"] = LImpr.Lenders;
            usrInfo["Brokers"] = LImpr.Brokers;
            usrInfo["Guarantors"] = LImpr.Guarantors;
            usrInfo["Agents"] = LImpr.Agents;
            usrInfo["Investors"] = LImpr.Investors;
            usrInfo["Vendors"] = LImpr.Vendors;
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
            byte resetPwdSystemId = 2;
            byte.TryParse(Config.PasswordResetModule, out resetPwdSystemId);
            SwitchContext(resetPwdSystemId, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, string>>();
            var Request = HttpContext.Current.Request;
            string secret = Config.ReCaptchaSecretKey;
            bool bypassRecaptcha = true;
            bool isHuman = bypassRecaptcha || ReCaptcha.Validate(secret, reCaptchaRequest);
            DataTable dt = (new LoginSystem()).GetSaltedUserInfo(0, emailAddress, emailAddress);
            Guid g = Guid.NewGuid();
            string GuidString = g.ToString();
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(emailAddress))
            {
                var token = GetAuthObject().GetSignedToken(GuidString + emailAddress);
                string emailCnt = "";
                string emlSubject = "";
                string emlSenderTitle = "";
                string emlHtml = "";
                string emailTemplateId = Config.ResetPwdEmailTemp;

                if (!string.IsNullOrEmpty(emailTemplateId))
                {
                    DataTable dtEmail = new AdminSystem().RunWrRule(0, "WrGetEmailCnt", LcAppConnString, LcAppPw, string.Format("<Params><templateId>{0}</templateId></Params>", emailTemplateId), LImpr, LCurr);
                    emlSubject = dtEmail.Rows[0]["EmailSubject"].ToString();
                    emlSenderTitle = dtEmail.Rows[0]["SenderName"].ToString();
                    emlHtml = dtEmail.Rows[0]["EmailTempCnt"].ToString();
                }
                else
                {
                    emlSubject = "Password Reset Request";
                    emlSenderTitle = "Customer Support";
                    emlHtml = "<div>Hi there,</div><br/><br/><div>Please use the following confirmation code to reset your password</div><br/><br/>"
                            + "<div>[[Token]]</div><br/><br/><div>If you are not aware of such request, please contact customer support ASAP as there may be intruder(s) attempting to break into your account </div>";
                }

                emailCnt = emlHtml.Replace("[[Token]]", token.Item1);
                string from = base.SysCustServEmail(base.LCurr.SystemId) ?? "cs@robocoder.com";
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
            byte resetPwdSystemId = 2;
            byte.TryParse(Config.PasswordResetModule, out resetPwdSystemId);
            SwitchContext(resetPwdSystemId, LCurr.CompanyId, LCurr.ProjectId);
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

    private string RegisterWebAuthnSetup(string appDomainUrl, string registrationJSON)
    {
        HttpRequest Request = HttpContext.Current.Request;
        bool isSafari = Request.UserAgent.Contains("Safari");
        //var x = Newtonsoft.Json.JsonConvert.DeserializeObject(registrationJSON);
        var serverDomain = new Uri(appDomainUrl).GetComponents(UriComponents.Host, UriFormat.Unescaped);
        var serverPath = new Uri(appDomainUrl).GetComponents(UriComponents.Path, UriFormat.Unescaped);
        string usrId = LUser.UsrId.ToString();
        string usrIdB64 = System.Convert.ToBase64String(usrId.ToUtf8ByteArray());

        var dtLogin = new LoginSystem().GetLogins(null, "Fido2", usrId);

        Fido2Configuration fido2Config = new Fido2Configuration
        {
            //                Origin = serverDomain,
            ServerDomain = serverDomain,
            ServerName = serverDomain,
            //                ServerIcon = "https://www.rintagi.com/images/a.gif",
            Timeout = 60 * 1000,
            TimestampDriftTolerance = 1000
        };
        byte[] challenge =
            Newtonsoft.Json.JsonConvert.SerializeObject(
            new WebAuthnChallenge
            {
                nonce = Guid.NewGuid().ToString(),
                usrId = serverPath + usrIdB64,
                appPath = serverPath,
                expiredAfter = RO.Common3.Utils.ToUnixTime(DateTime.UtcNow.AddMinutes(30))
            }
            ).ToUtf8ByteArray();
        Fido2User user = new Fido2User
        {
            DisplayName = LUser.UsrName,
            /* must be restricted to no more than than 64 for device like yubikey as it would fail without reason */
            //Name = (Guid.NewGuid().ToString() + " " + DateTime.UtcNow.ToString("o")).Left(64),
            //Id= Guid.NewGuid().ToString().ToUtf8ByteArray()
            Name = (serverPath + LUser.LoginName).Left(64),
            Id = (serverPath + usrIdB64).ToUtf8ByteArray()
        };
        AuthenticatorSelection authenticatorSelection = new AuthenticatorSelection
        {
            RequireResidentKey = false,
            UserVerification = UserVerificationRequirement.Discouraged,
            //                 AuthenticatorAttachment = AuthenticatorAttachment.Platform,
        };
        // AttestationConveyancePreference.None would suppress aaguid return for registration
        // iOS Safari must use None 
        // AttestationConveyancePreference attConveyancePreference = AttestationConveyancePreference.Indirect;
        AttestationConveyancePreference attConveyancePreference = isSafari ? AttestationConveyancePreference.None : AttestationConveyancePreference.Indirect;
        List<PublicKeyCredentialDescriptor> excludedCredentials = dtLogin.AsEnumerable().Select(
                dr =>
                {
                    return new PublicKeyCredentialDescriptor()
                    {
                        Id = base64UrlDecode(dr["LoginName"].ToString()),
                        Transports = 
                            isSafari ? null 
                                    : new AuthenticatorTransport[]{
                                    AuthenticatorTransport.Ble, AuthenticatorTransport.Internal, AuthenticatorTransport.Lightning, AuthenticatorTransport.Nfc, AuthenticatorTransport.Usb
                            },
                        Type = PublicKeyCredentialType.PublicKey,
                    };
                }
                ).ToList();
        //new List<PublicKeyCredentialDescriptor> { };
        AuthenticationExtensionsClientInputs clientExtensions = new AuthenticationExtensionsClientInputs
        {
            Extensions = true,
            SimpleTransactionAuthorization = "you are registering to " + GetDomainUrl(false),
            Location = true,
            UserVerificationMethod = true,
            BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds
            {
                FAR = float.MaxValue,
                FRR = float.MaxValue
            }
        };

        var fido2 = new Fido2(fido2Config);

        // must do this for the verification to work
        var options = fido2.RequestNewCredential(user, excludedCredentials, authenticatorSelection, attConveyancePreference, clientExtensions);
        // the challenge is random byte but we need more info, replace it
        options.Challenge = challenge;
        string createRequestJson = options.ToJson();
        return createRequestJson;
    }

    private string VerifyWebAuthnSetup(string appDomainUrl, string registrationJSON, string loginName, bool TwoFA = false)
    {
        //var x = Newtonsoft.Json.JsonConvert.DeserializeObject(registrationJSON);
        var serverDomain = new Uri(appDomainUrl).GetComponents(UriComponents.Host, UriFormat.Unescaped);
        var serverPath = new Uri(appDomainUrl).GetComponents(UriComponents.Path, UriFormat.Unescaped);
        Fido2Configuration fido2Config = new Fido2Configuration
        {
            Origin = serverDomain,
            ServerDomain = serverDomain,
            ServerName = serverDomain,
            ServerIcon = "https://www.rintagi.com/images/a.gif",
            Timeout = 60 * 1000,
            TimestampDriftTolerance = 1000
        };
        byte[] challenge =
            Newtonsoft.Json.JsonConvert.SerializeObject(
            new WebAuthnChallenge
            {
                nonce = Guid.NewGuid().ToString(),
                usrId = null,
                appPath = serverPath,
                expiredAfter = RO.Common3.Utils.ToUnixTime(DateTime.UtcNow.AddMinutes(30))
            }
            ).ToUtf8ByteArray();
        AuthenticatorSelection authSelection = new AuthenticatorSelection
        {
            RequireResidentKey = false,
            UserVerification = UserVerificationRequirement.Required,
            AuthenticatorAttachment = TwoFA ? AuthenticatorAttachment.Platform : (AuthenticatorAttachment?)null,
            //                AuthenticatorAttachment = null,
        };
        var dtLogin = new LoginSystem().GetLogins(loginName, "Fido2");
        //var dtLogin = new LoginSystem().GetLogins("John Doe", "Fido2");
        List<PublicKeyCredentialDescriptor> allowedCredentials = dtLogin.AsEnumerable().Select(
                dr =>
                {
                    return new PublicKeyCredentialDescriptor()
                    {
                        Id = base64UrlDecode(dr["LoginName"].ToString()),
                        Transports = new AuthenticatorTransport[]{
                                AuthenticatorTransport.Ble, AuthenticatorTransport.Internal, AuthenticatorTransport.Lightning, AuthenticatorTransport.Nfc, AuthenticatorTransport.Usb
                            },
                        Type = PublicKeyCredentialType.PublicKey,
                    };
                }
                ).ToList();

        //List<PublicKeyCredentialDescriptor> allowedCredentials = new List<PublicKeyCredentialDescriptor> {};
        AuthenticationExtensionsClientInputs clientExtensions = new AuthenticationExtensionsClientInputs
        {
            Extensions = true,
            SimpleTransactionAuthorization = "you are signing in to abc.com",
            Location = true,
            UserVerificationMethod = true,
        };

        var fido2 = new Fido2(fido2Config);

        var verificationRequest = Fido2NetLib.AssertionOptions.Create(fido2Config, challenge, allowedCredentials, UserVerificationRequirement.Preferred, clientExtensions);
        string verificationRequestJson = verificationRequest.ToJson();
        return verificationRequestJson;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> GetWebAuthnRegistrationRequest(string hostingDomainUrl)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            string registrationRequest = RegisterWebAuthnSetup(hostingDomainUrl, null);

            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();

            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();

            result.Add("registrationRequest", registrationRequest);

            mr.status = "success";
            mr.errorMsg = "";
            mr.data = result;

            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
        return ret;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> GetWeb3SigningRequest()
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            HttpRequest Request = HttpContext.Current.Request;
            string usrId = LUser == null ? "" : LUser.UsrId.ToString();
            string challenge =
                Newtonsoft.Json.JsonConvert.SerializeObject(
                new WebAuthnChallenge
                {
                    nonce = Guid.NewGuid().ToString(),
                    usrId = usrId,
                    expiredAfter = RO.Common3.Utils.ToUnixTime(DateTime.UtcNow.AddMinutes(30))
                }
                );
            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();

            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();

            result.Add("signingRequest", challenge);

            mr.status = "success";
            mr.errorMsg = "";
            mr.data = result;

            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null), true);
        return ret;
    }
    
    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> GetWebAuthnAssertionRequest(string hostingDomainUrl, string loginName)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            string assertionRequest = VerifyWebAuthnSetup(hostingDomainUrl, loginName, null);

            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();

            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();

            result.Add("assertionRequest", assertionRequest);

            mr.status = "success";
            mr.errorMsg = "";
            mr.data = result;

            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null), true);
        return ret;
    }

    protected string GetSiteUrl(bool bIncludeTitle = false)
    {
        string site =
            ResolveUrlCustom("", !IsProxy(), true)
            //Request.Url.Scheme + "://" + Request.Url.Host + Request.ApplicationPath 
            + (bIncludeTitle ? " (" + Config.WebTitle + ")" : "");
        return site;
    }

    protected void NotifyUser(string subject, string body, string email, string from)
    {
        HttpRequest Request = HttpContext.Current.Request;
        
        string site = GetSiteUrl(true);
        string requestIPChain = string.Format("\r\n\r\nRequest Source IP info : [{0}]\r\n", string.Join(",", GetClientIpChain(Request).ToArray()));
        SendEmail(subject + " " + site, body + requestIPChain, email, from, "", Config.WebTitle + " Customer Care", false);
    }
    
    protected void LinkUserLogin(int UsrId, string ProviderCd, string LoginName, string LoginMeta = null)
    {
        Dictionary<string, string> providers = new Dictionary<string, string>{
                {"F","Facebook"},
                {"G","Google"},
                {"M","Microsoft"},
                {"O","Office 365/Azure"},
                {"W","Windows"},
                {"Fido2","WebAuthn/Fido2"},
                {"Eth1","Eth1 Wallet"}
                };
        new LoginSystem().LinkUserLogin(UsrId, ProviderCd, LoginName, LoginMeta);
        LinkedUserLogin = new LoginSystem().GetLinkedUserLogin(LUser.UsrId).AsEnumerable().ToList();

        try
        {
            string from = base.SysCustServEmail(3);
            DataTable dtLabels = _GetLabels("MyAccountModule");
            DataColumn[] pkey = new DataColumn[1];
            pkey[0] = dtLabels.Columns[0];
            dtLabels.PrimaryKey = pkey;

            string subject = TranslateItem(dtLabels.Rows, "ProfileChangedEmailSubject");
            string body = string.Format(TranslateItem(dtLabels.Rows, "LoginLinkAddedEmailBody"), LUser.LoginName, providers[ProviderCd], LoginName);
            string site = GetSiteUrl(true);
            if (!string.IsNullOrEmpty(LUser.UsrEmail) || (LinkedUserLogin.Count > 0 && !string.IsNullOrEmpty(LinkedUserLogin[0]["UsrEmail"].ToString())))
            {
                NotifyUser(subject, body, LUser.UsrEmail ?? LinkedUserLogin[0]["UsrEmail"].ToString(), from);
            }
        }
        catch (Exception ex) {
            ErrorTrace(ex, "error");
        }
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> WebAuthnRegistration(string requestJSON, string resultJSON)
    {
        var Request = HttpContext.Current.Request;
        
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {

            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();

            if (LUser != null && LUser.LoginName != "Anonymous")
            {
                var aaguidLookup = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase){
                    {"6028B017-B1D4-4C02-B4B3-AFCDAFC96BB2","Windows Hello software authenticator"},
                    {"6E96969E-A5CF-4AAD-9B56-305FE6C82795","Windows Hello VBS software authenticator"},
                    {"08987058-CADC-4B81-B6E1-30DE50DCBE96","Windows Hello TPM authenticator"},
                    {"9DDD1817-AF5A-4672-A2B9-3E3DD95000A9","Windows Hello VBS hardware authenticator"},
                    {"cb69481e-8ff7-4039-93ec-0a2729a154a8","YubiKey 5 5.1 all variance"},
                    {"ee882879-721c-4913-9775-3dfcce97072a","YubiKey 5 5.2 all variance"},
                    {"fa2b99dc-9e39-4257-8f92-4a30d23c4118","YubiKey 5 NFC"},
                    {"2fc0579f-8113-47ea-b116-bb5a8db9202a","YubiKey 5 NFC"},
                    {"c5ef55ff-ad9a-4b9f-b580-adebafe026d0","YubiKey 5Ci"},
                    {"f8a011f3-8c0a-4d15-8006-17111f9edc7d","Security Key By Yubico"},
                    {"b92c3f9a-c014-4056-887f-140a2501163b","Security Key By Yubico"},
                    {"6d44ba9b-f6ec-2e49-b930-0c8fe920cb73","Security Key NFC By Yubico"},
                    {"149a2021-8ef6-4133-96b8-81f8d5b7f1f5","Security Key NFC By Yubico"},
                };

                var x = resultJSON;
                Fido2Configuration fido2Config = new Fido2Configuration
                {
                    Origin = "https://" + Request.Url.Host,
                    ServerDomain = Request.Url.Host,
                    ServerName = "site name",
                    ServerIcon = "https://www.rintagi.com/images/a.gif",
                    Timeout = 60 * 1000,
                    TimestampDriftTolerance = 1000
                };

                var fido2 = new Fido2(fido2Config);
                IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
                {
                    var id = args.CredentialId; // generated ID by authenticator(should be always unique for each authenticator, equivalent to client cert)
                    var u = args.User;          // user info, kind of useless as this can be changed by js at client side
                    return await Task.FromResult(true);
                };

                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<CredentialCreateOptions>(requestJSON);
                AuthenticatorAttestationRawResponseEx regResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAttestationRawResponseEx>(x);
                var success = fido2.MakeNewCredentialAsync(regResponse, request, callback).Result;
                var clientDataJson = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorResponse>(System.Text.UTF8Encoding.UTF8.GetString(regResponse.Response.ClientDataJson));
                var challenge = clientDataJson.Challenge;
                var regState = Newtonsoft.Json.JsonConvert.DeserializeObject<WebAuthnChallenge>(System.Text.UTF8Encoding.UTF8.GetString(challenge));
                var forUsrId = regState.usrId;
                var appPath = regState.appPath;
                var expiry = regState.expiredAfter;
                var credentialId = base64UrlEncode(success.Result.CredentialId);
                var publicKey = System.Convert.ToBase64String(success.Result.PublicKey);
                var signingCounter = success.Result.Counter; // designed for replay attact prevention but useless for a multiple node situation
                var user = success.Result.User;
                var usrId = System.Convert.ToBase64String(user.Id);
                var aaguid = success.Result.Aaguid;
                var authData = new AuthenticatorData(AuthenticatorAttestationResponse.Parse(regResponse).AttestationObject.AuthData);
                var userPresence = authData.UserPresent;
                var userVerified = authData.UserVerified;
                var loginMeta = Newtonsoft.Json.JsonConvert.SerializeObject(new WebAuthnMeta
                {
                    credentialId = credentialId,
                    publicKey = publicKey,
                    aaguid = aaguid.ToString(),
                    usrId = usrId,
                    appPath = appPath,
                    platform = regResponse.platform,
                    isMobile = regResponse.isMobile,
                    authenticatorType = aaguidLookup.ContainsKey(aaguid.ToString()) ? aaguidLookup[aaguid.ToString()] : "unknown",
                    requestIPChain = string.Format("[{0}]", string.Join(",", GetClientIpChain(Request).ToArray())),
                    userPresence = userPresence,
                    userVerified = userVerified,
                });
                LinkUserLogin(LUser.UsrId, "Fido2", credentialId, loginMeta);
                try
                {
                    // re-gen to prevent re-registration
                    string extAppDomainUrl =
                        !string.IsNullOrWhiteSpace(Config.ExtBaseUrl) && !string.IsNullOrEmpty(Request.Headers["X-Forwarded-For"])
                            ? Config.ExtBaseUrl
                            : Request.Url.AbsoluteUri.Replace(Request.Url.Query, "").Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");

                    //RegisterWebAuthnSetup(extAppDomainUrl, null);
                    //string assertionRequest = VerifyWebAuthnSetup(hostingDomainUrl, loginName, null);
                }
                catch { }
                if (!LUser.TwoFactorAuth && false)
                {
                    new LoginSystem().WrSetUsrOTPSecret(LUser.UsrId, true);
                }

                result["credentialId"] = credentialId;
                result["message"] = string.Format("WebAuthn(Fido2) registration successful with id {0}{1}"
                        , credentialId
                        , LUser.TwoFactorAuth ? "" : "\r\nPlease consider enable 2FA and/or choosing really random/complex password of at least 16 characters to harden other adhoc login"
                        );

                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Invalid WebAuthn Registration Request";
                mr.data = null;                
            }
            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
        return ret;
    }

    [WebMethod(EnableSession = true)]
    public LoginApiResponse WebAuthnAssertion(string requestJSON, string resultJSON)
    {
        var context = HttpContext.Current;
        HttpRequest Request = context.Request;
        try
        {
            var x = resultJSON;
            Fido2Configuration fido2Config = new Fido2Configuration
            {
                Origin = "https://" + Request.Url.Host,
                ServerDomain = Request.Url.Host,
                ServerName = "site name",
                ServerIcon = "https://www.rintagi.com/images/a.gif",
                Timeout = 60 * 1000,
                TimestampDriftTolerance = 1000
            };

            var fido2 = new Fido2(fido2Config);


            // 4. Create callback to check if userhandle owns the credentialId
            IsUserHandleOwnerOfCredentialIdAsync callback = async (args) =>
            {
                var u = args.UserHandle;
                var id = args.CredentialId;
                return await Task.FromResult(true);
            };

            // 5. Make the assertion
            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<AssertionOptions>(requestJSON);
            AuthenticatorAssertionRawResponse assertionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAssertionRawResponse>(x);
            var credentialId = base64UrlEncode(assertionResponse.Id);
            // 3. Get credential counter from database(design flaw of WebAuthn, required centralized storage), similar to ethereum transaction nonce
            uint storedCounter = 0; // 0 means always success
            var dtLogin = new LoginSystem().GetLogins(credentialId, "Fido2");
            var loginMeta = dtLogin.Rows[0]["LoginMeta"].ToString();
            var loginName = dtLogin.Rows[0]["RLoginName"].ToString();
            var webAuthMeta = Newtonsoft.Json.JsonConvert.DeserializeObject<WebAuthnMeta>(loginMeta);
            var publicKey = base64UrlDecode(webAuthMeta.publicKey);
            var success = fido2.MakeAssertionAsync(assertionResponse, request, publicKey, storedCounter, callback).Result;
            var clientData = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorResponse>(System.Text.UTF8Encoding.UTF8.GetString(assertionResponse.Response.ClientDataJson));
            var challenge = clientData.Challenge;
            var assertionState = Newtonsoft.Json.JsonConvert.DeserializeObject<WebAuthnChallenge>(System.Text.UTF8Encoding.UTF8.GetString(challenge));
            var authData = new AuthenticatorData(assertionResponse.Response.AuthenticatorData);
            var userPresence = authData.UserPresent;
            var userVerified = authData.UserVerified;
            var usr = SSOLogin(null, credentialId, "Fido2");
            if (usr != null && usr.UsrId > 0)
            {
                LoginApiResponse loginResponse = _Login(usr);
                loginResponse.username = usr.LoginName;
                return loginResponse;
            }
            else
            {
                return new LoginApiResponse() { message = "webauthn asserted but not registered", status = "failed", error = "access_denied" };
            }

            //SSOLogin(null, credentialId, "Fido2");
        }
        catch (Exception ex)
        {
            RO.Common3.Utils.NeverThrow(ex);
            return new LoginApiResponse() { message = "invalid webauthn assertion", status = "failed", error = "access_denied"};
        }            
    }
    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> Web3Registration(string requestJSON, string resultJSON)
    {
        var Request = HttpContext.Current.Request;

        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            string sig = resultJSON;
            if (LUser != null 
                && LUser.LoginName != "Anonymous" 
                && !string.IsNullOrEmpty(sig))
            {
                var challenge = Newtonsoft.Json.JsonConvert.DeserializeObject<WebAuthnChallenge>(requestJSON);
                var signer = new Nethereum.Signer.EthereumMessageSigner();
                byte[] message = System.Text.UTF8Encoding.UTF8.GetBytes(requestJSON);
                string signerAddress = signer.EcRecover(message, sig);
                LinkUserLogin(LUser.UsrId, "Eth1", signerAddress);
                result["walletAddress"] = signerAddress;
                result["message"] = string.Format("Eth1 Wallet registration successful with wallet address {0}{1}"
                        , signerAddress
                        , LUser.TwoFactorAuth ? "" : "\r\nPlease consider enable 2FA and/or choosing really random/complex password of at least 16 characters to harden other adhoc login"
                        );

                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
            }
            else
            {
                mr.status = "failed";
                mr.errorMsg = "Invalid Eth1 Wallet Registration Request";
                mr.data = null;
            }
            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
        return ret;
    }

    [WebMethod(EnableSession = true)]
    public LoginApiResponse Web3Assertion(string requestJSON, string resultJSON)
    {
        var context = HttpContext.Current;
        HttpRequest Request = context.Request;
        try
        {
            string sig = resultJSON;
            var challenge = Newtonsoft.Json.JsonConvert.DeserializeObject<WebAuthnChallenge>(requestJSON);
            var signer = new Nethereum.Signer.EthereumMessageSigner();
            byte[] message = System.Text.UTF8Encoding.UTF8.GetBytes(requestJSON);
            string signerAddress = signer.EcRecover(message, sig);
            var usr = SSOLogin(null, signerAddress, "Eth1");
            if (usr != null && usr.UsrId > 0)
            {
                LoginApiResponse loginResponse = _Login(usr);
                loginResponse.username = usr.LoginName;
                return loginResponse;
            }
            else
            {
                return new LoginApiResponse() { message = "wallet address not registered", status = "failed", error = "access_denied" };
            }

            //SSOLogin(null, credentialId, "Fido2");
        }
        catch (Exception ex)
        {
            RO.Common3.Utils.NeverThrow(ex);
            return new LoginApiResponse() { message = "invalid eth1 wallet assertion", status = "failed", error = "access_denied" };
        }
    }    
        
}