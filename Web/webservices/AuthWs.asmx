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
    byte systemId = 3;
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
    /* new stuff */
    [WebMethod(EnableSession = false)]
    public SerializableDictionary<string, string> GetToken(string client_id, string scope, string grant_type, string code, string code_verifier, string redirect_url, string client_secret)
    {
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> scopeContext = jss.Deserialize<Dictionary<string, object>>(scope);
        byte? systemId = null;
        int? companyId = null;
        int? projectId = null;
        short? cultureId = null;
        int access_token_validity = 5 * 60; // 20 minutes
        int refresh_token_validity = 60 * 60 * 24 * 14; // 14 days

        try { systemId = byte.Parse(scopeContext["SystemId"].ToString()); }
        catch { };
        try { companyId = int.Parse(scopeContext["CompanyId"].ToString()); }
        catch { };
        try { projectId = int.Parse(scopeContext["ProjectId"].ToString()); }
        catch { };
        try { cultureId = short.Parse(scopeContext["ProjectId"].ToString()); }
        catch { };

        var context = HttpContext.Current;
        string appPath = context.Request.ApplicationPath;
        string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
        HttpSessionState Session = HttpContext.Current.Session;
        System.Web.Caching.Cache cache = HttpContext.Current.Cache;
        string storedToken;
        RintagiLoginJWT loginJWT = new Func<RintagiLoginJWT>(() =>
        {
            if (grant_type == "authorization_code")
            {
                storedToken = (Session != null ? Session["RintagiLoginAccessCode"] as string : null) ?? cache[code] as string;
                try
                {
                    return GetLoginUsrInfo(storedToken) ?? new RintagiLoginJWT();
                }
                catch
                {
                };
            }
            else if (grant_type == "refresh_token")
            {
                try
                {
                    return GetLoginUsrInfo(code) ?? new RintagiLoginJWT();
                }
                catch { }
            }
            return new RintagiLoginJWT();
        })();
        var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var currTime = DateTime.Now.ToUniversalTime().Subtract(utc0).TotalSeconds;
        var nbf = loginJWT.nbf;
        var expiredOn = loginJWT.exp;

        if (currTime > nbf && currTime < expiredOn)
        {
            string signingKey = GetSessionSigningKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
            string encryptionKey = GetSessionEncryptionKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
            RintagiLoginToken loginToken = DecryptLoginToken(loginJWT.loginToken, encryptionKey);

            LCurr = new UsrCurr(companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId, systemId ?? loginToken.SystemId, systemId ?? loginToken.SystemId);
            LImpr = null;

            LImpr = SetImpersonation(LImpr, loginToken.UsrId, systemId ?? loginToken.SystemId, companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId);
            LUser = new LoginUsr();
            LUser.UsrId = loginToken.UsrId;
            LUser.DefCompanyId = loginToken.DefCompanyId;
            LUser.DefProjectId = loginToken.DefProjectId;
            LUser.DefSystemId = loginToken.DefSystemId;
            LUser.UsrName = loginToken.UsrName;
            LUser.InternalUsr = "Y";
            LUser.CultureId = 1;
            LUser.HasPic = false;
            string refreshTag = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            string loginTag = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            if (ValidateScope(true))
            {
                string loginTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, access_token_validity, loginTag);
                string refreshTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, refresh_token_validity, refreshTag);
                string token_scope = string.Format("s{0}c{1}p{2}", LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
                var Token = new SerializableDictionary<string, string> {
                { "access_token", loginTokenJWT},
                { "token_type", "Bearer"},
                { "iat", currTime.ToString()},
                { "expires_in", (access_token_validity - 1).ToString()},
                { "scope", token_scope},
                { "resources", appPath},
                { "refresh_token", refreshTokenJWT},
                };
                if (Session != null)
                {
                    Session.Remove("RintagiLoginAccessCode");
                    Session[refreshTag] = refreshTokenJWT;
                }
                return Token;
            }
            else
            {
                return new SerializableDictionary<string, string>() {
                { "error", "access_denied"},
                { "message", "cannot issue token"},
        };
            }
        }
        return new SerializableDictionary<string, string>() {
                { "error", "invalid_token"},
                { "message", "cannot issue token"},
        };
    }

    [WebMethod(EnableSession = false)]
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
            cache.Remove(loginHandle);
            // global flush access token handle(if needed)
        }
        catch { }
        return new ApiResponse<SerializableDictionary<string, string>, object>() { status = "success", data = { } };
    }

    [WebMethod(EnableSession = false)]
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
                    string guid = Guid.NewGuid().ToString();
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string jwtToken = CreateLoginJWT(LUser, usr.DefCompanyId, usr.DefProjectId, usr.DefSystemId, LCurr, LImpr, appPath, 10 * 60, guid);
                    HttpContext.Current.Cache.Add(guid, jwtToken, null
                        , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 1, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    var access_token = GetToken("", "", "authorization_code", guid, "", "", "");
                    var accessTokenJWT = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(access_token);
                    HttpContext.Current.Cache.Remove(attemptKey);
                    return new LoginApiResponse() { message = "login successful", accessCode = guid, accessToken = access_token, refreshToken = null, status = "success" };
                }
                else
                {
                    //locked();
                    //return false;
                    return new LoginApiResponse() { message = "account locked", status = "failed", error = "access_denied" };
                }
            }
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
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId, false, false, false);
            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();
            SerializableDictionary<string, string> usrInfo = new SerializableDictionary<string, string>();
            usrInfo["UsrId"] = LUser.UsrId.ToString();
            //            usrInfo["UsrEmail"] = LUser.UsrEmail.ToString();
            //usrInfo["LoginName"] = LUser.LoginName.ToString();
            usrInfo["UsrName"] = LUser.UsrName.ToString();
            usrInfo["MemberId"] = LImpr.Members;
            usrInfo["CustomerId"] = LImpr.Customers;
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

    [WebMethod(EnableSession = false)]
    public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetMenu(string scope)
    {
        Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {

            try { systemId = byte.Parse(scope); }
            catch { };
            
            SwitchContext(systemId , LCurr.CompanyId, LCurr.ProjectId);
            DataTable dtMenu = _GetMenu(LCurr.SystemId, true);
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.data = DataTableToListOfObject(dtMenu, false, null);
            mr.status = "success";
            mr.errorMsg = "";
            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, 0, "R", null));
        return ret;
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
}