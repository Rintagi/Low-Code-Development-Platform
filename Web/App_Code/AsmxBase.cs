using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using System.Collections;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Configuration;
using System.Threading.Tasks;

namespace RO.Web
{

    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public abstract class AsmxBase : WebService
    {
        //public class RintagiLoginToken
        //{
        //    public int UsrId { get; set; }
        //    public string LoginName { get; set; }
        //    public string UsrName { get; set; }
        //    public string UsrEmail { get; set; }
        //    public string UsrGroup { get; set; }
        //    public string RowAuthority { get; set; }
        //    public int CompanyId { get; set; }
        //    public int ProjectId { get; set; }
        //    public byte SystemId { get; set; }
        //    public int DefCompanyId { get; set; }
        //    public int DefProjectId { get; set; }
        //    public byte DefSystemId { get; set; }
        //    public byte DbId { get; set; }
        //    public string Resources { get; set; }
        //}

        //public class RintagiLoginJWT
        //{
        //    public string loginId;
        //    public string loginToken;
        //    public int iat;
        //    public int exp;
        //    public int nbf;
        //    public string handle { get; set; }
        //}

        protected enum IncludeBLOB { None, Icon, Content };
        private string SystemListCacheWatchFile { get {return Server.MapPath("~/RefreshSystemList.txt");}}

		private const String KEY_SystemsList = "Cache:SystemsList";
		private const String KEY_SystemsDict = "Cache:SystemsDict";
		private const String KEY_SysConnectStr = "Cache:SysConnectStr";
		private const String KEY_AppConnectStr = "Cache:AppConnectStr";
        private const String KEY_SystemAbbr = "Cache:SystemAbbr";
        private const String KEY_DesDb = "Cache:DesDb";
		private const String KEY_AppDb = "Cache:AppDb";
		private const String KEY_AppUsrId = "Cache:AppUsrId";
		private const String KEY_AppPwd = "Cache:AppPwd";
        private const String KEY_SysAdminEmail = "Cache:SysAdminEmail";
        private const String KEY_SysAdminPhone = "Cache:SysAdminPhone";
        private const String KEY_SysCustServEmail = "Cache:SysCustServEmail";
        private const String KEY_SysCustServPhone = "Cache:SysCustServPhone";
        private const String KEY_SysCustServFax = "Cache:SysCustServFax";
        private const String KEY_SysWebAddress = "Cache:SysWebAddress";

		private const String KEY_CacheLUser = "Cache:LUser";
		private const String KEY_CacheLPref = "Cache:LPref";
        private const String KEY_CacheLImpr = "Cache:LImpr";
		private const String KEY_CacheLCurr = "Cache:LCurr";
		private const String KEY_CacheCPrj = "Cache:CPrj";
		private const String KEY_CacheCSrc = "Cache:CSrc";
		private const String KEY_CacheCTar = "Cache:CTar";
		private const String KEY_CacheVMenu = "Cache:VMenu";

        private const String KEY_EntityList = "Cache:EntityList";
        private const String KEY_CompanyList = "Cache:CompanyList";
        private const String KEY_ProjectList = "Cache:ProjectList";
        private const String KEY_CultureList = "Cache:CultureList";

        protected UsrImpr LImpr {get;set;}
        protected UsrCurr LCurr {get;set;}
        protected LoginUsr LUser {get;set;}
        protected byte LcSystemId {get;set;}
        protected string LcSysConnString {get;set;}
        protected string LcAppConnString {get;set;}
        protected string LcAppDb {get;set;}
        protected string LcDesDb {get;set;}
        protected string LcAppPw {get;set;}
        protected CurrPrj CPrj {get;set;}
        protected CurrSrc CSrc {get;set;}
        protected CurrTar CTar {get;set;}

        protected List<string> _CurrentScreenCriteria = null;

        protected string loginHandle;
        protected abstract byte GetSystemId();
        protected abstract int GetScreenId();
        protected abstract string GetProgramName();
        protected abstract string GetValidateMstIdSPName();
        protected abstract string GetMstTableName(bool underlying = true);
        protected abstract string GetDtlTableName(bool underlying = true);
        protected abstract string GetMstKeyColumnName(bool underlying = false);
        protected abstract string GetDtlKeyColumnName(bool underlying = false);
        protected abstract Dictionary<string, SerializableDictionary<string, string>> GetDdlContext();
        protected abstract SerializableDictionary<string, string> InitMaster();
        protected abstract SerializableDictionary<string, string> InitDtl();
        protected abstract DataTable _GetMstById(string pid);
        protected abstract DataTable _GetDtlById(string pid,int screenFilterId);

        protected virtual bool AllowAnonymous() { return false; }

        static protected List<string> LisSuggestsOptions = new List<string>() { "startKeyVal", "startLabelVal" };
 
        public class AsmXResult<ContentClass>
        {
            public ContentClass d;
        }

        public class _ReactFileUploadObj
        {
        // this goes hand in hand with the react file upload control any change there must be reflected here
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string base64;
            public float height;
            public float width;
            public int size;
            public string previewUrl;
        }

        public class FileUploadObj
        {
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string base64;
            public string previewUrl;
            public string icon;
            public float height;
            public float width;
            public int size;
        }
        public class FileInStreamObj
        {
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string ver;
            public float height;
            public float width;
            public int size;
            public string previewUrl; 
            public int extensionSize;
            public bool contentIsJSON = false;
        }

        public class AutoCompleteResponse
        {
            public string query;
            public List<SerializableDictionary<string, string>> data;
            public int total;
            public int topN;
            public int skipped;
            public int matchCount;
        }

        public class AutoCompleteResponseObj
        {
            public string query;
            public SerializableDictionary<string, SerializableDictionary<string, string>> data;
            public int total;
            public int topN;
        }
        public class SaveDataResponse
        {
            public SerializableDictionary<string, string> mst;
            public List<SerializableDictionary<string,string>> dtl;
            public string message;
        }
        public class ApiResponse<T,S>
        {
//            public List<SerializableDictionary<string, string>> data;
            public T data;
            public S supportingData;
            public string status;
            public string errorMsg;
            public List<KeyValuePair<string, string>> validationErrors;
        }

        public class LoginApiResponse
        {
            public string message;
            public string status;
            public string errorMsg;
            public string accessCode;
            public string error;
            public string serverChallenge;
            public int challengeCount;
            public SerializableDictionary<string, string> accessToken;
            public SerializableDictionary<string, string> refreshToken;
        }
        public class LoadScreenPageResponse
        {
            public List<SerializableDictionary<string, string>> AuthRow;
            public List<SerializableDictionary<string, string>> AuthCol;
            public List<SerializableDictionary<string, string>> ColumnDef;
            public List<SerializableDictionary<string, string>> ScreenCriteria;
            public List<SerializableDictionary<string, string>> ScreenCriteriaDef;
            public List<SerializableDictionary<string, string>> ScreenFilter;
            public List<SerializableDictionary<string, string>> ScreenHlp;
            public List<SerializableDictionary<string, string>> ScreenButtonHlp;
            public List<SerializableDictionary<string, string>> Label;
            public SerializableDictionary<string, List<SerializableDictionary<string, string>>> Ddl;
            public List<SerializableDictionary<string, string>> SearchList;
            public SerializableDictionary<string, string> Mst;
            public List<SerializableDictionary<string, string>> Dtl;
        }
        private static string _masterkey;

        public struct ScreenResponse
        {
            public SerializableDictionary<string, SerializableDictionary<string, object>> data;
            public string status;
            public string errorMsg;
            public List<KeyValuePair<string, string>> validationErrors;
        }

        public struct MenuResponse
        {
            public List<MenuNode> data;
            public string status;
            public string errorMsg;
        }

        #region General helper functions
        public static int ToUnixTime(DateTime time)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (int)DateTime.SpecifyKind(time, DateTimeKind.Utc).Subtract(utc0).TotalSeconds;            
        }
        public static string TranslateISO8601DateTime(string t, bool storeInUTC = true)
        {
            try
            {
                bool hasTZInfo = new Regex("Z$", RegexOptions.IgnoreCase).IsMatch(t) || new Regex(@"[\-\+0-9\:]+$", RegexOptions.IgnoreCase).IsMatch(t);
                if (storeInUTC)
                    return (!hasTZInfo ? DateTime.Parse(t, null, System.Globalization.DateTimeStyles.RoundtripKind) : DateTime.Parse(t, null, System.Globalization.DateTimeStyles.RoundtripKind).ToUniversalTime()).ToString("F");
                else
                    return DateTime.Parse(new Regex(@"Z|([\-\+0-9\:]+)$", RegexOptions.IgnoreCase).Replace(t, ""), null, System.Globalization.DateTimeStyles.RoundtripKind).ToString("F");
            }
            catch { return null; }
        }
        public static bool VerifyHS256JWT(string header, string payload, string base64UrlEncodeSig, string secret)
        {
            Func<byte[], string> base64UrlEncode = (c) => Convert.ToBase64String(c).TrimEnd(new char[] { '=' }).Replace('_', '/').Replace('-', '+');
            HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(secret));
            byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(header + "." + payload));
            return base64UrlEncodeSig == base64UrlEncode(hash);
        }
        public static string ToXMLParams(Dictionary<string, string> WrSPCallParams, string xlmRootName = "Params", List<string> onlyInclude = null)
        {
            List<string> x = WrSPCallParams
                    .Where(kvp => onlyInclude == null || onlyInclude.Contains(kvp.Key))
                    .Aggregate(new List<string>(), (a, kvp) => { a.Add(string.Format("<{0}>{1}</{0}>", kvp.Key, kvp.Value)); return a; });
            string xmlParams = string.Format("<{0}>{1}</{0}>",
                    xlmRootName,
                    string.Join("", x.ToArray()));
            return xmlParams;
        }
        public static List<string> GetExceptionMessage(Exception ex)
        {
            List<string> msg = new List<string>();
            for (var x = ex; x != null; x = x.InnerException)
            {
                if (x is AggregateException && ((AggregateException)x).InnerExceptions.Count > 1)
                {
                    if (((AggregateException)x).InnerExceptions.Count > 1)
                        foreach (var y in ((AggregateException)x).InnerExceptions)
                        {
                            msg.Add(string.Join("\r\n", GetExceptionMessage(y).ToArray()));
                        }
                }
                else
                {
                    msg.Add(x.Message);
                }
            }
            return msg;
        }
        #endregion
        public class ReCaptcha
        {
            public bool Success { get; set; }
            public List<string> ErrorCodes { get; set; }

            public static bool Validate(string secret, string encodedResponse)
            {
                if (string.IsNullOrEmpty(encodedResponse)) return false;

                var client = new System.Net.WebClient();

                if (string.IsNullOrEmpty(secret)) return false;

                var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                var reCaptcha = serializer.Deserialize<ReCaptcha>(googleReply);

                return reCaptcha.Success;
            }
        }

        protected Tuple<string, string, string> GetCurrentCallInfo()
        {
            var Request = HttpContext.Current.Request;
            var Path = Request.Path;
            var FunctionName = Request.PathInfo;
            var ModuleEndPoint = Path.Replace(FunctionName, "");
            var ServiceEndPoint = new Regex("(/.*)/.*$").Replace(ModuleEndPoint, "$1");
            return new Tuple<string, string, string>(ServiceEndPoint, ModuleEndPoint, FunctionName);
        }
        protected ReturnType PostAsmXRequest<ReturnType>(string url, string jsonBodyContent, bool forwardAccessToken = false)
        {
            Uri uri = new Uri(url);
            var CalleeEndPoint = uri.AbsolutePath;
            var CurrentCallInfo = GetCurrentCallInfo();
            var ServiceEndPoint = CurrentCallInfo.Item1;

            bool sameEndpoint = (uri.IsLoopback || uri.Host == HttpContext.Current.Request.Url.Host) && uri.AbsolutePath.StartsWith(ServiceEndPoint);

            uri.GetLeftPart(UriPartial.Path);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var auth = (HttpContext.Current.Request.Headers["Authorization"] ?? HttpContext.Current.Request.Headers["X-Authorization"]) as string;
            var scope = HttpContext.Current.Request.Headers["X-RintagiScope"] as string;

            if (forwardAccessToken || sameEndpoint)
            {
                request.Method = "POST";
                request.Headers.Add("X-Authorization", auth);
                request.Headers.Add("X-RintagiScope", scope);
            }

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonBodyContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                    {
                        string result = reader.ReadToEnd();
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        AsmXResult<ReturnType> content = jss.Deserialize<AsmXResult<ReturnType>>(result);
                        return content.d;
                    }

                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
            }

            return default(ReturnType);
        }

        #region Authentications
        protected RO.Facade3.Auth GetAuthObject()
        {
            string jwtMasterKey = System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"];
            if (string.IsNullOrEmpty(jwtMasterKey)) 
            {
                RO.Facade3.Auth.GenJWTMasterKey();
                System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"] = jwtMasterKey;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                if (config.AppSettings.Settings["JWTMasterKey"] != null) config.AppSettings.Settings["JWTMasterKey"].Value = jwtMasterKey;
                else config.AppSettings.Settings.Add("JWTMasterKey", jwtMasterKey);
                // save to web.config on production, but silently failed. this would remove comments in appsettings 
                if (Config.DeployType == "PRD") config.Save(ConfigurationSaveMode.Modified);
            }

            var auth = RO.Facade3.Auth.GetInstance(jwtMasterKey);
            return auth;
        }
        protected string MasterKey_
        {
            get
            {

                if (_masterkey == null)
                {
                    string jwtMasterKey = System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"];
                    if (string.IsNullOrEmpty(jwtMasterKey))
                    {
                        try
                        {
                            byte[] randomBits = new byte[32];
                            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                            {
                                // Fill the array with a random value.
                                rngCsp.GetBytes(randomBits);
                            }
                            jwtMasterKey = Convert.ToBase64String(randomBits);
                            System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"] = jwtMasterKey;
                            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                            if (config.AppSettings.Settings["JWTMasterKey"] != null) config.AppSettings.Settings["JWTMasterKey"].Value = jwtMasterKey;
                            else config.AppSettings.Settings.Add("JWTMasterKey", jwtMasterKey);
                            // save to web.config on production, but silently failed. this would remove comments in appsettings 
                            if (Config.DeployType == "PRD") config.Save(ConfigurationSaveMode.Modified);
                        }
                        catch
                        {
                            jwtMasterKey = Config.DesPassword;
                        }
                    }
                    RO.Facade3.Auth.GetInstance(jwtMasterKey);
                    // delay brute force attack, 100K round(ethereum keystore use 260K round, 100K round requires about 5 sec as of 2018/6 hardware)
                    //, we only do this once and stored in class variable so there is a 5 sec delay when app started for API usage
                    Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(jwtMasterKey, UTF8Encoding.UTF8.GetBytes(jwtMasterKey), 100000);
                    _masterkey = (new AdminSystem()).EncryptString(Convert.ToBase64String(k.GetBytes(32)));
                }
                return _masterkey;
            }
        }
        //protected string GetSessionEncryptionKey(string time, string usrId)
        //{
        //    System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
        //    return (new AdminSystem()).EncryptString(Convert.ToBase64String(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(MasterKey + usrId + time))));
        //}
        //protected string GetSessionSigningKey(string time, string usrId)
        //{
        //    var key = GetSessionEncryptionKey(time, usrId);
        //    return Convert.ToBase64String(new Rfc2898DeriveBytes(key, UTF8Encoding.UTF8.GetBytes(key), 1).GetBytes(32));
        //}

        //protected Tuple<string, string> GetSignedToken(string nounce)
        //{
        //    System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
        //    string hash = BitConverter.ToString(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(nounce))).Replace("-", "");
        //    return new Tuple<string, string>(hash.Left(6), hash.Substring(6));
        //}
        //protected bool VerifySignedToken(string nounce, string ticketLeft, string ticketRight)
        //{
        //    System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
        //    string hash = BitConverter.ToString(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(nounce))).Replace("-", "");
        //    return hash == ticketLeft.Trim() + ticketRight.Trim();
        //}

        protected UsrImpr SetImpersonation(UsrImpr LImpr, Int32 usrId, byte systemId, Int32 companyId, Int32 projectId)
        {
            UsrImpr ui = null;
            ui = (new LoginSystem()).GetUsrImpr(usrId, companyId, projectId, systemId);
            if (ui != null)
            {
                if (LImpr == null)
                {
                    LImpr = ui;
                    //if (LUser.LoginName == "Anonymous") { LImpr.Cultures = LUser.CultureId.ToString(); }
                }
                else // Append:
                {
                    LImpr.Usrs = ui.Usrs;
                    LImpr.UsrGroups = ui.UsrGroups;
                    LImpr.Cultures = ui.Cultures;
                    LImpr.RowAuthoritys = ui.RowAuthoritys;
                    LImpr.Companys = ui.Companys;
                    LImpr.Projects = ui.Projects;
                    LImpr.Investors = ui.Investors;
                    LImpr.Customers = ui.Customers;
                    LImpr.Vendors = ui.Vendors;
                    LImpr.Agents = ui.Agents;
                    LImpr.Brokers = ui.Brokers;
                    LImpr.Members = ui.Members;
                }
                DataTable dt = (new LoginSystem()).GetUsrImprNext(usrId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string TestDupUsrs = (char)191 + LImpr.Usrs + (char)191;
                        if (TestDupUsrs.IndexOf((char)191 + dr["ImprUsrId"].ToString() + (char)191) < 0)
                        {
                            SetImpersonation(LImpr, Int32.Parse(dr["ImprUsrId"].ToString()), systemId, companyId, projectId);
                        }
                    }
                }
            }
            return LImpr;
        }
        protected string CreateEncryptedLoginToken(LoginUsr usr, int defCompanyId, int defProjectId, byte defSystemId, UsrCurr curr, UsrImpr impr, string resources, string secret)
        {
            RintagiLoginToken loginToken = new RintagiLoginToken()
            {
                UsrId = usr.UsrId,
                LoginName = usr.LoginName,
                UsrName = usr.UsrName,
                UsrEmail = usr.UsrEmail,
                UsrGroup = impr.UsrGroups,
                RowAuthority = impr.RowAuthoritys,
                SystemId = curr.SystemId,
                CompanyId = curr.CompanyId,
                ProjectId = curr.ProjectId,
                DefSystemId = defSystemId,
                DefCompanyId = defCompanyId,
                DefProjectId = defProjectId,
                DbId = curr.DbId,
                Resources = resources,
            };
            string json = new JavaScriptSerializer().Serialize(loginToken);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            string hash = BitConverter.ToString(hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(json))).Replace("-", "");
            string encrypted = RO.Common3.Utils.ROEncryptString(hash.Left(32) + json, secret);
            return encrypted;
        }
        protected RintagiLoginToken DecryptLoginToken(string encryptedToken, string secret)
        {
            string decryptedToken = RO.Common3.Utils.RODecryptString(encryptedToken, secret);
            RintagiLoginToken token = new JavaScriptSerializer().Deserialize<RintagiLoginToken>(decryptedToken.Substring(32));
            return token;
        }
        protected string CreateLoginJWT(LoginUsr usr, int defCompanyId, int defProjectId, byte defSystemId, UsrCurr curr, UsrImpr impr, string resources, int validSeconds, string guidHandle)
        {
            return GetAuthObject().CreateLoginJWT(usr, defCompanyId, defProjectId, defSystemId, curr, impr, resources, validSeconds, guidHandle);

            //Func<byte[], string> base64UrlEncode = (c) => Convert.ToBase64String(c).TrimEnd(new char[] { '=' }).Replace('_', '/').Replace('-', '+');
            //var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //var issueTime = DateTime.Now.ToUniversalTime();
            //var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            //var exp = (int)issueTime.AddSeconds(validSeconds).Subtract(utc0).TotalSeconds; // Expiration time is up to 1 hour, but lets play on safe side
            //var encryptionKey = GetSessionEncryptionKey(iat.ToString(), usr.UsrId.ToString());
            //var signingKey = GetSessionSigningKey(iat.ToString(), usr.UsrId.ToString());
            //RintagiLoginJWT token = new RintagiLoginJWT()
            //{
            //    iat = iat,
            //    exp = exp,
            //    nbf = iat,
            //    loginToken = CreateEncryptedLoginToken(usr, defCompanyId, defProjectId, defSystemId, curr, impr, resources, encryptionKey),
            //    loginId = usr.UsrId.ToString(),
            //    handle = guidHandle
            //};
            //string payLoad = new JavaScriptSerializer().Serialize(token);
            //string header = "{\"typ\":\"JWT\",\"alg\":\"HS256\"}";
            //HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(signingKey));
            //string content = base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(header)) + "." + base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(payLoad));
            //byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(content));
            //return content + "." + base64UrlEncode(hash);
        }
        protected RO.Facade3.RintagiLoginJWT GetLoginUsrInfo(string jwt)
        {
            return GetAuthObject().GetLoginUsrInfo(jwt);
            //string[] x = (jwt ?? "").Split(new char[] { '.' });
            //Func<string, byte[]> base64UrlDecode = s => Convert.FromBase64String(s.Replace('-', '+').Replace('_', '/') + (s.Length % 4 > 1 ? new string('=', 4 - s.Length % 4) : ""));
            //if (x.Length >= 3)
            //{
            //    try
            //    {
            //        Dictionary<string, string> header = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[0])));
            //        try
            //        {
            //            RintagiLoginJWT loginJWT = new JavaScriptSerializer().Deserialize<RintagiLoginJWT>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[1])));
            //            string signingKey = GetSessionSigningKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
            //            bool valid = header["typ"] == "JWT" && header["alg"] == "HS256" && VerifyHS256JWT(x[0], x[1], x[2], signingKey);
            //            if (valid)
            //            {
            //                return loginJWT;
            //            }
            //            else return null;
            //        }
            //        catch
            //        {
            //            return null;
            //        }
            //    }
            //    catch { return null; }
            //}
            //else return null;

        }
        protected bool ValidateJWTHandle(string handle)
        {
            // can check centralized location for universal logout etc.
            return true;
        }
        #endregion
        protected Dictionary<byte,Dictionary<string,string>> GetSystemsDict(bool ignoreCache=false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            int cacheMinutes = 30;
            Dictionary<byte, Dictionary<string, string>> sysDict = cache[KEY_SystemsDict] as Dictionary<byte, Dictionary<string, string>>;

            if (sysDict == null) {
                sysDict = new Dictionary<byte, Dictionary<string, string>>();
                DataTable dt = LoadSystemsList(ignoreCache);
                dt.PrimaryKey = new DataColumn[] { dt.Columns["SystemId"] };
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Active"].ToString() == "N")
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y"; 
                foreach (DataRow dr in dt.Rows)
			    {
				    Dictionary<string,string> dict = new Dictionary<string,string>();
                    dict[KEY_SysConnectStr] = Config.GetConnStr(dr["dbAppProvider"].ToString(), singleSQLCredential ? Config.DesServer : dr["ServerName"].ToString(), dr["dbDesDatabase"].ToString(), "", singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString());
                    dict[KEY_AppConnectStr] = Config.GetConnStr(dr["dbAppProvider"].ToString(), singleSQLCredential ? Config.DesServer : dr["ServerName"].ToString(), dr["dbAppDatabase"].ToString(), "", singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString());
                    dict[KEY_SystemAbbr] = dr["SystemAbbr"].ToString();
				    dict[KEY_DesDb] = dr["dbDesDatabase"].ToString();
				    dict[KEY_AppDb] = dr["dbAppDatabase"].ToString();
                    dict[KEY_AppUsrId] = singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString();
                    dict[KEY_AppPwd] = singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString();
                    try { dict[KEY_SysAdminEmail] = dr["AdminEmail"].ToString(); } catch { dict[KEY_SysAdminEmail] = string.Empty; } 
                    try { dict[KEY_SysAdminPhone] = dr["AdminPhone"].ToString(); } catch { dict[KEY_SysAdminPhone] = string.Empty; } 
                    try { dict[KEY_SysCustServEmail] = dr["CustServEmail"].ToString(); } catch { dict[KEY_SysCustServEmail] = string.Empty; } 
                    try { dict[KEY_SysCustServPhone] = dr["CustServPhone"].ToString(); } catch { dict[KEY_SysCustServPhone] = string.Empty; } 
                    try { dict[KEY_SysCustServFax] = dr["CustServFax"].ToString(); } catch { dict[KEY_SysCustServFax] = string.Empty; } 
                    try { dict[KEY_SysWebAddress] = dr["WebAddress"].ToString(); } catch { dict[KEY_SysWebAddress] = string.Empty; } 
                    sysDict[byte.Parse(dr["SystemId"].ToString())] = dict;
                }
                cache.Add(KEY_SystemsDict,sysDict,new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return sysDict;
        }

        protected string SysCustServEmail(byte SystemId)
        {
            try { return GetSystemsDict()[SystemId][KEY_SysCustServEmail]; }
            catch { return string.Empty; }
        }

        protected String SysAdminEmail(byte SystemId)
        {
            try { return GetSystemsDict()[SystemId][KEY_SystemsDict]; } catch { return string.Empty; }
        }

        protected DataTable LoadSystemsList(bool ignoreCache=false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            int cacheMinutes = 30;

            DataTable dt = cache[KEY_SystemsList] as DataTable;

            if (dt == null || ignoreCache) {
                dt = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
                cache.Insert(KEY_SystemsList,dt,new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                dt.PrimaryKey = new DataColumn[] { dt.Columns["SystemId"] };
            }
            return dt;
        }
        protected DataTable LoadEntityList(bool ignoreCache)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            int cacheMinutes = 30;

            DataTable dt = cache[KEY_EntityList] as DataTable;
            if (dt == null || ignoreCache)
            {
                dt = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
                cache.Insert(KEY_EntityList, dt, new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dt;
        }

        protected DataTable _GetCompanyList(bool ignoreCache = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = KEY_CompanyList + "_" + LUser.UsrId.ToString();
            int minutesToCache = 1;
            DataTable dtCompanyList = cache[cacheKey] as DataTable;
            if (dtCompanyList == null || ignoreCache)
            {
                dtCompanyList = (new LoginSystem()).GetCompanyList(LImpr.Usrs, LImpr.RowAuthoritys, LImpr.Companys == "0" ? LImpr.Companys : LImpr.Companys);
                cache.Insert(cacheKey, dtCompanyList, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtCompanyList;
        }
        protected DataTable _GetProjectList(int companyId,bool ignoreCache=false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = KEY_ProjectList + "_" +  companyId.ToString() + "_" + LUser.UsrId.ToString();
            int minutesToCache = 1;
            DataTable dtProjectList = cache[cacheKey] as DataTable;
            if (dtProjectList == null || ignoreCache)
            {
                dtProjectList = (new LoginSystem()).GetProjectList(LImpr.Usrs, LImpr.RowAuthoritys, LImpr.Projects, companyId.ToString());
                cache.Insert(cacheKey, dtProjectList, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtProjectList;
        }
        protected bool ValidateScope(bool ignoreCache = false)
        {
            try
            {
                //DataTable dtMenu = _GetMenu(LCurr.SystemId, ignoreCache);
                //if (dtMenu.Rows.Count == 0) throw new Exception("access_denied");
                DataTable dtCompany = _GetCompanyList(ignoreCache);
                if (LCurr.CompanyId > 0 && LCurr.CompanyId != LUser.DefCompanyId && dtCompany.AsEnumerable().Where(dr => dr["CompanyId"].ToString() == LCurr.CompanyId.ToString()).Count() == 0)
                {
                    try
                    {
                        // force to first defined company if default not in list
                        LCurr.CompanyId = (int)dtCompany.AsEnumerable().Where(dr => !string.IsNullOrEmpty(dr["CompanyId"].ToString())).First()["CompanyId"];
                    }
                    catch
                    {
                        throw new Exception("access_denied");
                    }

                }
                DataTable dtProject = _GetProjectList(LCurr.CompanyId, ignoreCache);
                if (LCurr.ProjectId > 0 && LCurr.ProjectId != LUser.DefProjectId && dtProject.AsEnumerable().Where(dr => dr["ProjectId"].ToString() == LCurr.ProjectId.ToString()).Count() == 0)
                {
                    try
                    {
                        // force to first defined company if default not in list
                        LCurr.ProjectId = (int)dtProject.AsEnumerable().Where(dr => !string.IsNullOrEmpty(dr["ProjectId"].ToString())).First()["ProjectId"];
                    }
                    catch
                    {
                        throw new Exception("access_denied");
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                var x = "";
                return false;
            }
        }
        protected UsrImpr LoadUsrImpr(int usrId, byte systemId, int companyId, int projectId, bool ignoreCache)
        {
            
            string imprCacheKey = string.Format("{0}_{1}_{2}_{3}_{4}", KEY_CacheLImpr, usrId, systemId, companyId, projectId);
            var context = HttpContext.Current;
            var cache = context.Cache;
            UsrImpr impr = cache[imprCacheKey] as UsrImpr;
            if (impr == null|| ignoreCache)
            {
                int cacheMinutes = 1; // cache for 1 minute to avoid frequent DB retrieve for rapid firing API calls
                impr = SetImpersonation(null, usrId, systemId, companyId, projectId);
                cache.Insert(imprCacheKey, impr, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.Default, null);
            }
            return impr;
        }

        protected Dictionary<string, object> CreateUserSession(RintagiLoginToken token, byte? systemId = null, int? companyId = null, int? projectId = null)
        {
            Dictionary<string, string> dSys = GetSystemsDict()[systemId ?? token.SystemId];
            byte? dbId = systemId;
            LUser = new LoginUsr(token.LoginName, token.UsrId, token.UsrName, token.UsrEmail, "N", "N", 1, "English(US)", token.DefSystemId, token.DefProjectId, token.DefCompanyId, 9999, 0, false, null, null, false);
            LCurr = new UsrCurr(companyId ?? token.CompanyId, projectId ?? token.ProjectId, systemId ?? token.SystemId, dbId ?? token.DbId);
            LImpr = SetImpersonation(null, token.UsrId, systemId ?? token.SystemId, companyId ?? token.CompanyId, projectId ?? token.ProjectId);
            Dictionary<string, object> userSession = new Dictionary<string, object> { { "LUser", LUser }, { "LCurr", LCurr }, { "LImpr", LImpr } };
            return userSession;
        }

        protected void SetupAnonymousUser()
        {
            LUser = new LoginUsr("Anonymous", 1, "Anonymous", null, "N", "N", 1, "English(US)", 0, 0, 0, 9999, 0, false, null, null, false);
            LCurr = new UsrCurr(0, 0, 3, 3);
            LImpr = SetImpersonation(null, 1, LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
            loginHandle = "Anonymous";
        }

        protected void SwitchContext(byte sysId, int companyId, int projectId,bool checkSysId = true,bool checkCompanyId = true, bool checkProjectId = true, bool ignoreCache=false)
        {
            if (LcSystemId != sysId || (LCurr == null || LCurr.CompanyId != companyId || LCurr.ProjectId != projectId)) {
                LcSystemId = sysId;
                LCurr.CompanyId = companyId;
                LCurr.ProjectId = projectId;
                LCurr.SystemId = sysId;
                LImpr = LoadUsrImpr(LUser.UsrId, sysId, companyId, projectId,ignoreCache);
                CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
                DataRow row = LoadSystemsList().Rows.Find(sysId);
                bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
                if (singleSQLCredential)
                {

                }
                CSrc = new CurrSrc(true, row);
                CTar = new CurrTar(true, row);
                Dictionary<string, string> sysDict = GetSystemsDict()[sysId];
                LcSysConnString = sysDict[KEY_SysConnectStr];
                LcAppConnString = sysDict[KEY_AppConnectStr];
                LcDesDb = sysDict[KEY_DesDb];
                LcAppDb = sysDict[KEY_AppDb];
                LcAppPw = sysDict[KEY_AppPwd];
            }
            if (checkSysId)
            {
                DataTable dtMenu = _GetMenu(sysId);
                if (dtMenu.Rows.Count == 0) throw new Exception("access_denied");
            }
            /* validate selected company/project */
            if (checkCompanyId && LCurr.CompanyId > 0)
            {
                DataTable dtCompany = _GetCompanyList();
                if (LCurr.CompanyId != LUser.DefCompanyId && dtCompany.AsEnumerable().Where(dr => dr["CompanyId"].ToString() == LCurr.CompanyId.ToString()).Count() == 0) throw new Exception("access_denied");
            }
            if (checkProjectId && LCurr.ProjectId > 0)
            {
                DataTable dtProject = _GetProjectList(LCurr.CompanyId);
                if (LCurr.ProjectId != LUser.DefProjectId && dtProject.AsEnumerable().Where(dr => dr["ProjectId"].ToString() == LCurr.ProjectId.ToString()).Count() == 0) throw new Exception("access_denied");
            }

        }
        protected void SwitchLang(short cultureId)
        {
            LUser.CultureId = cultureId;
        }


        protected Dictionary<string,object> LoadUserSession()
        {
            try
            {
                var context = HttpContext.Current;
                var cache = context.Cache;
//                var accessTokenInCookie = context.Request.Cookies["access_token"];
//                var refreshTokenInCookie = context.Request.Cookies["refresh_token"];
                var auth = (HttpContext.Current.Request.Headers["Authorization"] ?? HttpContext.Current.Request.Headers["X-Authorization"]) as string;
                var scope = HttpContext.Current.Request.Headers["X-RintagiScope"] as string;
                byte? systemId = null;
                int? companyId = null;
                int? projectId = null;
                short? cultureId = null;
                if (!string.IsNullOrEmpty(scope))
                {

                    var x = (scope??"").Split(new char[] { ',' });
                    try { systemId = byte.Parse(x[0]); } catch { };
                    try { companyId = int.Parse(x[1]); } catch { };
                    try { projectId = int.Parse(x[2]); } catch { };
                    try { cultureId = short.Parse(x[3]); } catch { };
                }
                if (false && Session != null && Session[KEY_CacheLUser] != null)
                {
                    Dictionary<string, object> userSession = new Dictionary<string,object>(){{ "LUser", Session[KEY_CacheLUser] }, { "LCurr", Session[KEY_CacheLCurr] }, { "LImpr", Session[KEY_CacheLImpr] }};
                    LUser = userSession["LUser"] as LoginUsr;
                    LImpr = userSession["LImpr"] as UsrImpr;
                    LCurr = userSession["LCurr"] as UsrCurr;
                    return userSession;
                }
                else if (auth != null && auth.StartsWith("Bearer"))
                {
                    var x = auth.Split(new char[] { ' ' });
                    if (x.Length > 1)
                    {
                        var authObj = GetAuthObject();
                        RO.Facade3.RintagiLoginJWT token = authObj.GetLoginUsrInfo(x[1]);
                        var handle = token.handle;
                        var userSession = cache[handle] as Dictionary<string, object>;
                        var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        var now = DateTime.Now.ToUniversalTime().Subtract(utc0).TotalSeconds;
                        var minutesToCache = 5;
                        var sessionEncryptionKey = authObj.GetSessionEncryptionKey(token.iat.ToString(), token.loginId.ToString());

                        if (now < token.exp && ValidateJWTHandle(handle)) {
                            RintagiLoginToken loginToken = authObj.DecryptLoginToken(token.loginToken, sessionEncryptionKey);
                            if ((
                                projectId != null && projectId.Value != loginToken.ProjectId
                                ) ||
                                (
                                companyId != null && companyId.Value != loginToken.CompanyId
                                ) ||
                                (
                                systemId != null && systemId.Value != loginToken.SystemId
                                ) ||
                                userSession == null
                                )
                            {
                                /* this means a browser refresh would remember the last scope for minutesToCache until token expiry */
                                userSession = CreateUserSession(loginToken, systemId, companyId, projectId);
                                cache.Insert(handle, userSession, new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                            }

                            LUser = userSession["LUser"] as LoginUsr;
                            LImpr = userSession["LImpr"] as UsrImpr;
                            LCurr = userSession["LCurr"] as UsrCurr;
                            if (cultureId != null && LUser.CultureId != cultureId)
                            {
                                SwitchLang(cultureId.Value);
                            }
                            loginHandle = handle;
                            return userSession;
                        }
                        else if (cache[handle] as Dictionary<string, object> != null)
                        {
                            cache.Remove(handle);
                        }
                    }
                }
            }
            catch (Exception e) {
                if (e.Message == "access_denied") throw;
            }
            return null;
        }
        protected ApiResponse<T,S> ProtectedCall<T,S>(Func<ApiResponse<T,S>> apiCallFn, bool allowAnonymous = false)
        {
            HttpContext Context = HttpContext.Current;
            try
            {
                Dictionary<string, object> userSessionInfo = LoadUserSession();

                if (!allowAnonymous && (LUser == null || LUser.UsrId == 1)) // not login or anonymous
                {
                    ApiResponse<T, S> mr = new ApiResponse<T, S>();
                    Context.Response.StatusCode = 401;
                    Context.Response.TrySkipIisCustomErrors = true;
                    mr.status = "access_denied";
                    mr.errorMsg = "requires login";
                    return mr;
                }
                else
                {
                    if (LUser == null) SetupAnonymousUser();
                    return ManagedApiCall(apiCallFn);
                }
            }
            catch (Exception e) {
                ApiResponse<T, S> mr = new ApiResponse<T, S>();
                Context.Response.StatusCode = 401;
                Context.Response.TrySkipIisCustomErrors = true;
                mr.status = "access_denied";
                mr.errorMsg = e.Message;
                return mr;
            }
        }
        protected async Task<ApiResponse<T, S>> ProtectedCallAsync<T, S>(Func<Task<ApiResponse<T, S>>> apiCallFn, bool allowAnonymous = false)
        {
            HttpContext Context = HttpContext.Current;
            try
            {
                Dictionary<string, object> userSessionInfo = LoadUserSession();

                if (!allowAnonymous && (LUser == null || LUser.UsrId == 1)) // not login or anonymous
                {
                    ApiResponse<T, S> mr = new ApiResponse<T, S>();
                    Context.Response.StatusCode = 401;
                    Context.Response.TrySkipIisCustomErrors = true;
                    mr.status = "access_denied";
                    mr.errorMsg = "requires login";
                    return mr;
                }
                else
                {
                    if (LUser == null) SetupAnonymousUser();
                    return await ManagedApiCallAsync(apiCallFn);
                }
            }
            catch (Exception e)
            {
                ApiResponse<T, S> mr = new ApiResponse<T, S>();
                Context.Response.StatusCode = 401;
                Context.Response.TrySkipIisCustomErrors = true;
                mr.status = "access_denied";
                mr.errorMsg = e.Message;
                return mr;
            }
        }
        protected Action<Exception> GetErrorTracing()
        {
            var Context = HttpContext.Current;
            return (e) =>
            {
                GetErrorTracingEx(Context != null ? Context.Request : null)(e, null);
            };
        }
        protected Action<Exception, string> GetErrorTracingEx(HttpRequest Request)
        {
            return (e, severity) =>
            {
                string supportEmail = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"];
                if (supportEmail != "none" && supportEmail != string.Empty)
                {
                    try
                    {
                        string webtitle = System.Configuration.ConfigurationManager.AppSettings["WebTitle"] ?? "";
                        string to = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"] ?? "cs@robocoder.com";
                        string from = "cs@robocoder.com";
                        string fromTitle = "";
                        string replyTo = "";
                        string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                        string[] smtpConfig = smtpServer.Split(new char[] { '|' });
                        bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
                        int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
                        string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
                        string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
                        string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
                        string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
                        System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                        string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string sourceIP = string.Format("From: {0}\r\n\r\n", Request != null ? Request.UserHostAddress : "unknown request url");
                        string usrId = string.Format("User: {0}\r\n\r\n", LUser != null ? LUser.UsrId.ToString() : "");
                        string currentTime = string.Format("Server Time: {0} \r\n\r\n UTC: {1} \r\n\r\n", DateTime.Now.ToString("O"), DateTime.UtcNow.ToString("O"));
                        var exMessages = GetExceptionMessage(e);
                        Exception innerException = e.InnerException;

                        foreach (var t in receipients)
                        {
                            mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
                        }
                        mm.Subject = webtitle + " Application Error " + (Request != null ? Request.Url.GetLeftPart(UriPartial.Path) : "unknown request url");
                        mm.Body = (Request != null ? Request.Url.ToString() : "unknown request url" ) + "\r\n\r\n" + sourceIP + usrId + currentTime + exMessages[exMessages.Count - 1] + "\r\n\r\n" + e.StackTrace + (innerException != null ? "\r\n InnerException: \r\n\r\n" + string.Join("\r\n", exMessages.ToArray()) + "\r\n\r\n" + innerException.StackTrace : "") + "\r\n";
                        mm.IsBodyHtml = false;
                        mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
                        mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
                        (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
                        mm.Dispose();   // Error is trapped and reported from the caller.

                    }
                    catch (Exception ex)
                    {
                        var x = "";
                    }
                }
            };
        }

        protected void ErrorTracing(Exception e)
        {
            GetErrorTracingEx(HttpContext.Current != null ? HttpContext.Current.Request : null)(e, null);
        }

        protected ApiResponse<T,S> ManagedApiCall<T,S>(Func<ApiResponse<T,S>> apiCallFn)
        {
            try 
            {
                return apiCallFn();
            }
            catch (Exception e)
            {
                string supportEmail = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"];
                if (supportEmail != "none" && supportEmail != string.Empty)
                {
                    try
                    {
                        HttpRequest Request = HttpContext.Current.Request;
                        string webtitle = System.Configuration.ConfigurationManager.AppSettings["WebTitle"] ?? "";
                        string to = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"] ?? "cs@robocoder.com";
                        string from = "cs@robocoder.com";
                        string fromTitle = "";
                        string replyTo = "";
                        string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                        string[] smtpConfig = smtpServer.Split(new char[] { '|' });
                        bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
                        int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
                        string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
                        string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
                        string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
                        string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
                        System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                        string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string sourceIP = string.Format("From: {0}\r\n\r\n",Request.UserHostAddress);
                        string usrId = string.Format("User: {0}\r\n\r\n", LUser != null ? LUser.UsrId.ToString() : "");
                        string currentTime = string.Format("Server Time: {0} \r\n\r\n UTC: {1} \r\n\r\n", DateTime.Now.ToString("O"), DateTime.UtcNow.ToString("O"));
                        Exception innerException = e.InnerException;

                        foreach (var t in receipients)
                        {
                            mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
                        }
                        mm.Subject = webtitle + " Application Error " + Request.Url.GetLeftPart(UriPartial.Path);
                        mm.Body = Request.Url.ToString() + "\r\n\r\n" + sourceIP + usrId + currentTime + e.Message + "\r\n\r\n" + e.StackTrace + (innerException != null ? "\r\n InnerException: \r\n\r\n" + innerException.Message + "\r\n\r\n" + innerException.StackTrace : "") + "\r\n";
                        mm.IsBodyHtml = false;
                        mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
                        mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
                        (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
                        mm.Dispose();   // Error is trapped and reported from the caller.

                    }
                    catch (Exception ex)
                    {
                        var x = "";
                    }
                }
                return new ApiResponse<T,S>() { 
                    errorMsg = e.Message + (Config.DeployType=="DEV" ? e.StackTrace : "")
                    , status = "failed"
                };
            }
        }

        protected async Task<ApiResponse<T, S>> ManagedApiCallAsync<T, S>(Func<Task<ApiResponse<T, S>>> apiCallFn)
        {
            try
            {
                return await apiCallFn();
            }
            catch (Exception e)
            {
                string supportEmail = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"];
                if (supportEmail != "none" && supportEmail != string.Empty)
                {
                    try
                    {
                        HttpRequest Request = HttpContext.Current.Request;
                        string webtitle = System.Configuration.ConfigurationManager.AppSettings["WebTitle"] ?? "";
                        string to = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"] ?? "cs@robocoder.com";
                        string from = "cs@robocoder.com";
                        string fromTitle = "";
                        string replyTo = "";
                        string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                        string[] smtpConfig = smtpServer.Split(new char[] { '|' });
                        bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
                        int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
                        string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
                        string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
                        string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
                        string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
                        System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                        string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string sourceIP = string.Format("From: {0}\r\n\r\n", Request.UserHostAddress);
                        string usrId = string.Format("User: {0}\r\n\r\n", LUser != null ? LUser.UsrId.ToString() : "");
                        Exception innerException = e.InnerException;

                        foreach (var t in receipients)
                        {
                            mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
                        }
                        mm.Subject = webtitle + " Application Error " + Request.Url.GetLeftPart(UriPartial.Path);
                        mm.Body = Request.Url.ToString() + "\r\n\r\n" + sourceIP + usrId + e.Message + "\r\n\r\n" + e.StackTrace + (innerException != null ? "\r\n InnerException: \r\n\r\n" + innerException.Message + "\r\n\r\n" + innerException.StackTrace : "") + "\r\n";
                        mm.IsBodyHtml = false;
                        mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
                        mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
                        (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
                        mm.Dispose();   // Error is trapped and reported from the caller.

                    }
                    catch (Exception ex)
                    {
                        var x = "";
                    }
                }
                return new ApiResponse<T, S>()
                {
                    errorMsg = e.Message + (Config.DeployType == "DEV" ? e.StackTrace : "")
                    ,
                    status = "failed"
                };
            }
        }

        protected Func<ApiResponse<T, S>> RestrictedApiCall<T, S>(Func<ApiResponse<T, S>> apiCallFn, byte systemId, int screenId, string action, string columnName, Func<ApiResponse<T, S>> OnErrorResponse = null)
        {
            Func<ApiResponse<T,S>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                Func<ApiResponse<T, S>> errRetFn = () =>
                {
                    if (OnErrorResponse != null) return OnErrorResponse();

                    ApiResponse<T, S> mr = new ApiResponse<T, S>();
                    Context.Response.StatusCode = 401;
                    Context.Response.TrySkipIisCustomErrors = true;
                    mr.status = "access_denied";
                    mr.errorMsg = "access denied";
                    return mr;
                };
                if (screenId > 0 || !string.IsNullOrEmpty(columnName))
                {
                    Dictionary<string, DataRow> dtMenuAccess = GetScreenMenu(systemId, screenId);
                    DataTable dtAuthRow = _GetAuthRow(screenId);
                    DataTable dtAuthCol = _GetAuthCol(screenId);
                    Dictionary<string, DataRow> authCol = dtAuthCol.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
                    
                    if ( // screen based checking, i.e. record level
                        !AllowAnonymous() &&
                        ((dtMenuAccess != null && !dtMenuAccess.ContainsKey(screenId.ToString()))
                        || dtMenuAccess == null
                        || dtAuthRow.Rows.Count == 0
                        || (dtAuthRow.Rows[0]["ViewOnly"].ToString() == "Y" && (action == "S" || action == "D"))
                        || (dtAuthRow.Rows[0]["AllowAdd"].ToString() == "N" && dtAuthRow.Rows[0]["AllowUpd"].ToString() == "N" && action == "S")
                        || (dtAuthRow.Rows[0]["AllowDel"].ToString() == "N" && action == "D")
                        ))
                    {
                        return errRetFn();
                        //ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        //Context.Response.StatusCode = 401;
                        //Context.Response.TrySkipIisCustomErrors = true;
                        //mr.status = "access_denied";
                        //mr.errorMsg = "access denied";
                        //return mr;
                    }
                    else if (!string.IsNullOrEmpty(columnName) &&
                            ((authCol.ContainsKey(columnName) && (authCol[columnName]["ColVisible"].ToString() == "N")) || (!authCol.ContainsKey(columnName) && !authCol.ContainsKey(columnName+"Text"))))
                    {
                        return errRetFn();

                        //ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        //Context.Response.StatusCode = 401;
                        //Context.Response.TrySkipIisCustomErrors = true;
                        //mr.status = "access_denied";
                        //mr.errorMsg = "access denied";
                        //return mr;
                    }
                }
                return apiCallFn();
            };

            return fn;

        }

        protected Func<Task<ApiResponse<T, S>>> RestrictedApiCallAsync<T, S>(Func<Task<ApiResponse<T, S>>> apiCallFn, byte systemId, int screenId, string action, string columnName, Func<Task<ApiResponse<T, S>>> OnErrorResponse = null)
        {
            Func<Task<ApiResponse<T, S>>> fn = async () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                Func<Task<ApiResponse<T, S>>> errRetFn = async () =>
                {
                    if (OnErrorResponse != null) return await OnErrorResponse();

                    ApiResponse<T, S> mr = new ApiResponse<T, S>();
                    Context.Response.StatusCode = 401;
                    Context.Response.TrySkipIisCustomErrors = true;
                    mr.status = "access_denied";
                    mr.errorMsg = "access denied";
                    return mr;
                };
                if (screenId > 0 || !string.IsNullOrEmpty(columnName))
                {
                    Dictionary<string, DataRow> dtMenuAccess = GetScreenMenu(systemId, screenId);
                    DataTable dtAuthRow = _GetAuthRow(screenId);
                    DataTable dtAuthCol = _GetAuthCol(screenId);
                    Dictionary<string, DataRow> authCol = dtAuthCol.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());

                    if ( // screen based checking, i.e. record level
                        !AllowAnonymous() &&
                        ((dtMenuAccess != null && !dtMenuAccess.ContainsKey(screenId.ToString()))
                        || dtMenuAccess == null
                        || dtAuthRow.Rows.Count == 0
                        || (dtAuthRow.Rows[0]["ViewOnly"].ToString() == "Y" && (action == "S" || action == "D"))
                        || (dtAuthRow.Rows[0]["AllowAdd"].ToString() == "N" && dtAuthRow.Rows[0]["AllowUpd"].ToString() == "N" && action == "S")
                        || (dtAuthRow.Rows[0]["AllowDel"].ToString() == "N" && action == "D")
                        ))
                    {
                        return await errRetFn();
                        //ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        //Context.Response.StatusCode = 401;
                        //Context.Response.TrySkipIisCustomErrors = true;
                        //mr.status = "access_denied";
                        //mr.errorMsg = "access denied";
                        //return mr;
                    }
                    else if (!string.IsNullOrEmpty(columnName) &&
                            ((authCol.ContainsKey(columnName) && (authCol[columnName]["ColVisible"].ToString() == "N")) || (!authCol.ContainsKey(columnName) && !authCol.ContainsKey(columnName + "Text"))))
                    {
                        return await errRetFn();

                        //ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        //Context.Response.StatusCode = 401;
                        //Context.Response.TrySkipIisCustomErrors = true;
                        //mr.status = "access_denied";
                        //mr.errorMsg = "access denied";
                        //return mr;
                    }
                }
                return await apiCallFn();
            };

            return fn;

        }

        protected bool _AllowScreenColumnAccess(int screenId, string columnName, string action, Dictionary<string, DataRow> dtMenuAccess, DataTable dtAuthRow, Dictionary<string, DataRow> authCol)
        {
            if ( // screen based checking, i.e. record level
                !AllowAnonymous() &&
                ((dtMenuAccess != null && !dtMenuAccess.ContainsKey(screenId.ToString()))
                || dtMenuAccess == null
                || dtAuthRow.Rows.Count == 0
                || (dtAuthRow.Rows[0]["ViewOnly"].ToString() == "Y" && (action == "S" || action == "D"))
                || (dtAuthRow.Rows[0]["AllowAdd"].ToString() == "N" && dtAuthRow.Rows[0]["AllowUpd"].ToString() == "N" && action == "S")
                || (dtAuthRow.Rows[0]["AllowDel"].ToString() == "N" && action == "D")
                ))
            {
                return false;
            }

            else if (!string.IsNullOrEmpty(columnName) &&
                    ((authCol.ContainsKey(columnName) && (authCol[columnName]["ColVisible"].ToString() == "N")) || (!authCol.ContainsKey(columnName) && !authCol.ContainsKey(columnName + "Text"))))
            {
                return false;
            }

            return true;
        }

        protected DataTable _GetLabels(string labelCat)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_SystemLabels_" + GetSystemId() + "_" + LUser.CultureId.ToString() + "_" + labelCat.ToString();
            int minutesToCache = 1;
            DataTable dtLabel = cache[cacheKey] as DataTable;
            if (dtLabel == null)
            {
                /* this should be fixed in GetLabels in SP as it doesn't support default FIXEME */
                //dtLabel = (new AdminSystem()).GetLabels(LUser.CultureId, labelCat, LCurr.CompanyId.ToString(), LcSysConnString, LcAppPw);
                dtLabel = (new AdminSystem()).GetLabels(LUser.CultureId, labelCat, null, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtLabel, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtLabel;
        }

        protected string _GetLabel(string labelCat, string labelKey)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_SystemLabel_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString() + "_" + labelCat.ToString() + "_" + labelKey;
            int minutesToCache = 1;
            string label = cache[cacheKey] as string;
            if (label == null)
            {
                label = (new AdminSystem()).GetLabel(LUser != null && LUser.LoginName.ToLower() != "anonymous" ? LUser.CultureId : (short)1, labelCat, labelKey, LCurr.CompanyId.ToString(), LcSysConnString, LcAppPw);
                cache.Add(cacheKey, label, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return label;
        }

        protected DataTable _GetScreenButtonHlp(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenButtonHlp_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            DataTable dtButtonHlp = cache[cacheKey] as DataTable;
            int minutesToCache = 1;
            if (dtButtonHlp == null)
            {
                dtButtonHlp = (new AdminSystem()).GetButtonHlp(GetScreenId(), 0, 0, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtButtonHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtButtonHlp;
        }
        protected DataTable _GetScrCriteria(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScrCriteria_" + GetSystemId().ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtScreenCriteria = null;
            lock (cache)
            {
                DataTable dt = cache[cacheKey] as DataTable;
                if (dt != null) dtScreenCriteria = dt.Copy();
            }

            if (dtScreenCriteria == null)
            {
                dtScreenCriteria = (new AdminSystem()).GetScrCriteria(screenId.ToString(), LcSysConnString, LcAppPw);
                lock (cache)
                {
                    if (cache[cacheKey] as DataTable == null)
                    {
                        cache.Insert(cacheKey, dtScreenCriteria, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                            , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    }
                }
            }
            return dtScreenCriteria;
        }

        protected Tuple<List<string>, SerializableDictionary<string, string>> _GetCurrentScrCriteria(DataView dv, SerializableDictionary<string, SerializableDictionary<string, string>> lastCriteria, bool useLast)
        {
            List<string> currentCriteriaList = dv.Count == 0 ? new List<string>() : new List<string>(Enumerable.Range(0, dv.Count).Select(_ => ""));
            SerializableDictionary<string, string> currentScrCriteria = new SerializableDictionary<string, string>();
            int ii = 0;
            foreach (DataRowView drv in dv)
            {
                bool required = drv["RequiredValid"].ToString() == "Y";
                string columnName = drv["ColumnName"].ToString();
                string keyName = drv["DdlKeyColumnName"].ToString();
                string lastValue =lastCriteria[columnName] != null ? lastCriteria[columnName]["LastCriteria"] : "";
                if (!required)
                {
                    currentCriteriaList[ii] = currentScrCriteria[columnName] = lastValue;
                }
                else if (drv["DisplayMode"].ToString() == "AutoListBox")
                {
                    // FIXME, keylist needs to be validated properly for required criteria
                    currentCriteriaList[ii] = lastValue;
                }
                else if (drv["DisplayName"].ToString() == "ComboBox" ||
                    drv["DisplayName"].ToString() == "DropDownList" ||
                    drv["DisplayName"].ToString() == "ListBox" ||
                    drv["DisplayMode"].ToString() == "AutoListBox" ||
                    drv["DisplayName"].ToString() == "RadioButtonList"
                    )
                {
                    var list = GetScreenCriteriaDdlList(drv["ScreenCriId"].ToString(), !string.IsNullOrEmpty(lastValue) ? "**" + lastValue : "", 0, "");
                    try
                    {
                        var firstChoice = list.data.data.FirstOrDefault();
                        var lastValid = list.data.data.Where(v => v["key"] == lastValue).Count() > 0;
                        currentCriteriaList[ii] = currentScrCriteria[columnName] = 
                                                            useLast && lastValid && !string.IsNullOrEmpty(lastValue)
                                                            ? lastValue
                                                            : (firstChoice != null) ? firstChoice["key"] : MakeCriteriaVal(drv.Row, "");
                    }
                    catch (Exception ex)
                    {
                        // invalid value or something else 
                        currentCriteriaList[ii] = MakeCriteriaVal(drv.Row, "");
                    }

                }
                else currentCriteriaList[ii] = currentScrCriteria[columnName] =
                                                                required ? (useLast && !string.IsNullOrEmpty(lastValue) ? lastValue : MakeCriteriaVal(drv.Row, ""))
                                                                         : (useLast ? lastValue : MakeCriteriaVal(drv.Row, ""));
                ii = ii + 1;
            }
            return new Tuple<List<string>, SerializableDictionary<string, string>>(currentCriteriaList, currentScrCriteria);
        }

        protected List<string> _SetCurrentScrCriteria(List<string> criteria)
        {
            return _CurrentScreenCriteria = criteria;
        }

        protected DataTable _GetScreenCriHlp(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenCriHlp_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtCriHlp = cache[cacheKey] as DataTable;
            if (dtCriHlp == null)
            {
                dtCriHlp = (new AdminSystem()).GetScreenCriHlp(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtCriHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtCriHlp;
        }

        protected DataTable _GetScreenIn(int screenId, DataRowView drv, bool refresh)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenIn_" + GetSystemId().ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString() + "_" + drv["ScreenCriId"].ToString();
            int minutesToCache = 1;
            DataTable dtScreenIn = cache[cacheKey] as DataTable;
            if (dtScreenIn == null || refresh)
            {
                dtScreenIn = (new AdminSystem()).GetScreenIn(screenId.ToString(), "GetDdl" + drv["ColumnName"].ToString() + GetSystemId().ToString() + "C" + drv["ScreenCriId"].ToString(), 0, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, LImpr, LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtScreenIn, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenIn;
        }

        protected DataTable _GetScreenTab(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenTab_" + GetSystemId().ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;

            DataTable dtScreenTab = cache[cacheKey] as DataTable;
            if (dtScreenTab == null)
            {
                dtScreenTab = (new AdminSystem()).GetScreenTab(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtScreenTab, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenTab;
        }

        protected DataTable _GetScreenHlp(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenHelp_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString()  + "_" + screenId.ToString();
            int minutesToCache = 1;

            DataTable dtScreenHlp = cache[cacheKey] as DataTable;
            if (dtScreenHlp == null)
            {
                dtScreenHlp = (new AdminSystem()).GetScreenHlp(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtScreenHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenHlp;
        }

        protected DataTable _GetLastScrCriteria(int screenId,int rowExpected, bool refresh=false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenLastCriteria_" + GetSystemId().ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtLastCriteria = cache[cacheKey] as DataTable;
            if (dtLastCriteria == null || refresh)
            {
                dtLastCriteria = (new AdminSystem()).GetLastCriteria(rowExpected, screenId, 0, LUser.UsrId, LcSysConnString, LcAppPw);
                cache.Insert(cacheKey, dtLastCriteria, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtLastCriteria;
        }
        protected DataTable _GetScreenFilter(int screenId, bool refresh=false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenFilter_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtScreenFilter = cache[cacheKey] as DataTable;
            if (dtScreenFilter == null)
            {
                dtScreenFilter = (new AdminSystem()).GetScreenFilter(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Insert(cacheKey, dtScreenFilter, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenFilter;
        }
        protected DataTable _GetAuthRow(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenAutRow_" + GetSystemId().ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtAuthRow = cache[cacheKey] as DataTable;
            if (dtAuthRow == null)
            {
                dtAuthRow = (new AdminSystem()).GetAuthRow(screenId, LImpr.RowAuthoritys, LcSysConnString, LcAppPw); ;
                cache.Add(cacheKey, dtAuthRow, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtAuthRow;
        }
        protected DataTable _GetAuthCol(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenAutCol_" + GetSystemId().ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtAuthCol = cache[cacheKey] as DataTable;
            if (dtAuthCol == null)
            {
                dtAuthCol = (new AdminSystem()).GetAuthCol(screenId, LImpr, LCurr, LcSysConnString, LcAppPw); ;
                cache.Add(cacheKey, dtAuthCol, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtAuthCol;
        }
        protected DataTable _GetScreenLabel(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenLabel_" + GetSystemId().ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            int minutesToCache = 1;
            DataTable dtLabel = cache[cacheKey] as DataTable;
            if (dtLabel == null)
            {
                dtLabel = (new AdminSystem()).GetScreenLabel(screenId, LUser.CultureId, LImpr, LCurr, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtLabel, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtLabel;
        }
        protected DataTable _GetMenu(byte systemId, bool ignoreCache = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_Menu_" + systemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString();
            int minutesToCache = 1;
            DataTable dtMenuItems = cache[cacheKey] as DataTable;
            if (dtMenuItems == null || ignoreCache)
            {
                dtMenuItems = (new MenuSystem()).GetMenu(LUser.CultureId, systemId, LImpr, LcSysConnString, LcAppPw, 0, 0, 0);
                cache.Insert(cacheKey, dtMenuItems, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtMenuItems;
        }
        protected string MakeCriteriaVal(DataRow dr, string val)
        {
            if (dr["DataTypeSysName"].ToString() == "DateTime") { return string.IsNullOrEmpty(val) ? new DateTime(1900, 1, 1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Byte") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Int16") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Int32") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Int64") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Single") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Double") { return string.IsNullOrEmpty(val) ? (-1).ToString() : val; }
            else if (dr["DataTypeSysName"].ToString() == "Byte[]") { return string.IsNullOrEmpty(val) ? val : val; }
            else if (dr["DataTypeSysName"].ToString() == "Object") { return string.IsNullOrEmpty(val) ? val : val; }
            else { return string.IsNullOrEmpty(val) ? Guid.NewGuid().ToString() : val; }

        }
        protected DataTable MakeColumns(DataTable dt, DataView dvCri)
        {
            DataColumnCollection columns = dt.Columns;
            foreach (DataRowView drv in dvCri)
            {
                if (drv["DataTypeSysName"].ToString() == "DateTime") { columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
                else if (drv["DataTypeSysName"].ToString() == "Int16") { columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
                else if (drv["DataTypeSysName"].ToString() == "Int32") { columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
                else if (drv["DataTypeSysName"].ToString() == "Int64") { columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
                else if (drv["DataTypeSysName"].ToString() == "Single") { columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
                else if (drv["DataTypeSysName"].ToString() == "Double") { columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte[]") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
                else if (drv["DataTypeSysName"].ToString() == "Object") { columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
                else { columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
            }
            return dt;
        }

        protected string ValidatedCriVal(int screenId, DataRowView drv, string val, bool refresh)
        {
            if (drv["DisplayName"].ToString() == "ListBox"
                ||
                drv["DisplayName"].ToString() == "ComboBox"
                ||
                drv["DisplayName"].ToString() == "DropDownList"
                ||
                drv["DisplayName"].ToString() == "RadioButtonList"
                )
            {
                int CriCnt = (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
                DataTable dtScreenIn = _GetScreenIn(screenId, drv, refresh);
                var dictAllowedChoice = dtScreenIn.AsEnumerable().ToDictionary(dr => dr[drv["DdlKeyColumnName"].ToString()].ToString());
                int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn(screenId.ToString(), "GetDdl" + drv["ColumnName"].ToString() + GetSystemId().ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, LImpr, LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : LcSysConnString, LcAppPw)).Count;
                var selectedVals = (val??"").Split(new char[] { ',' });
                var matchedVals = string.Join(",",
                    selectedVals
                    .Where(v=> { try { return !string.IsNullOrEmpty(v) && (dictAllowedChoice.ContainsKey(v) || v=="0" || v=="'0'" ); } catch { return false;}})
                    .Select(v=> drv["DisplayName"].ToString() == "ListBox" ?  string.Format( v.Contains("'") ? "{0}" : "'{0}'", v): v)
                    .ToList());
                bool noneSelected = string.IsNullOrEmpty(matchedVals) || matchedVals == "''" || string.IsNullOrEmpty(val);
                if (drv["DisplayName"].ToString() == "ListBox")
                {
                    return noneSelected && CriCnt + 1 > TotalChoiceCnt ? "'-1'" : (string.IsNullOrEmpty(val) ? null : "(" + val + ")");
                }
                else return matchedVals;
            }
            else if (",DateUTC,DateTimeUTC,ShortDateTimeUTC,LongDateTimeUTC,".IndexOf("," + drv["DisplayMode"].ToString() + ",") >= 0) {
                return val;
            }
            else if (drv["DisplayName"].ToString().Contains("Date")) {
                return val;
            }
            else return val;
        }

        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, List<string> lastScrCri, bool refresh, bool isSave)
        {
            DataSet ds = new DataSet();
            DataTable dtScreenCriHlp = _GetScreenCriHlp(screenId);

            ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn"), dvCri));
            int ii = 0;
            DataRow dr = ds.Tables["DtScreenIn"].NewRow();
            DataRowCollection drcScreenCriHlp = dtScreenCriHlp.Rows;
            foreach (DataRowView drv in dvCri)
            {
                string val = ValidatedCriVal(screenId, drv, lastScrCri.Count > ii ? lastScrCri[ii] : null,refresh);
                
                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(val) && isSave)
                {
                    string columnHeader = (drcScreenCriHlp.Count > ii) ? drcScreenCriHlp[ii]["ColumnHeader"].ToString() : drv["ColumnName"].ToString();
                    string columnMsg = (drcScreenCriHlp.Count > ii) ? drcScreenCriHlp[ii]["ColumnHeader"].ToString() : drv["ColumnName"].ToString();
                    throw new Exception(columnHeader + "cannot be empty ");
                }
                else
                {
                    if (!string.IsNullOrEmpty(val) || drv["RequiredValid"].ToString() == "Y") dr[drv["ColumnName"].ToString()] = (object)MakeCriteriaVal(drv.Row, val);
                }
                ii = ii + 1;
            }
            ds.Tables["DtScreenIn"].Rows.Add(dr);
            return ds;
        }
        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, SerializableDictionary<string,object> lastScrCri,bool refresh, bool isSave)
        {
            var x = dvCri.Table.AsEnumerable().Select(dr => lastScrCri.ContainsKey(dr["ColumnName"].ToString()) ? lastScrCri[dr["ColumnName"].ToString()].ToString() : null).ToList();
            return MakeScrCriteria(screenId, dvCri, x, refresh, isSave);
        }

        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, DataTable dtLastScrCri, bool refresh,bool isSave)
        {
            return MakeScrCriteria(screenId, dvCri, dtLastScrCri.AsEnumerable().Skip(1).Select(dr => dr["LastCriteria"].ToString()).ToList<string>(),refresh,isSave);
        }

        protected DataView GetCriCache(int systemId, int screenId)
        {
            return new DataView(_GetScrCriteria(screenId));
        }

        protected byte[] ResizeImage(byte[] image, int maxHeight = 360)
        {

            byte[] dc;

            System.Drawing.Image oBMP = null;

            using (var ms = new MemoryStream(image))
            {
                oBMP = System.Drawing.Image.FromStream(ms);
                ms.Close();
            }

            UInt16 orientCode = 1;

            try
            {
                using (var ms2 = new MemoryStream(image))
                {
                    var r = new ExifLib.ExifReader(ms2);
                    r.GetTagValue(ExifLib.ExifTags.Orientation, out orientCode);
                }
            }
            catch { }

            int nHeight = maxHeight < oBMP.Height ? maxHeight : oBMP.Height; // This is 36x10 line:7700 GenScreen
            int nWidth = int.Parse((Math.Round(decimal.Parse(oBMP.Width.ToString()) * (nHeight / decimal.Parse(oBMP.Height.ToString())))).ToString());

            var nBMP = new System.Drawing.Bitmap(oBMP, nWidth, nHeight);
            using (System.IO.MemoryStream sm = new System.IO.MemoryStream())
            {
                // 1 = do nothing
                if (orientCode == 3)
                {
                    // rotate 180
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                }
                else if (orientCode == 6)
                {
                    //rotate 90
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                }
                else if (orientCode == 8)
                {
                    // same as -90
                    nBMP.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                }
                nBMP.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
                sm.Position = 0;
                dc = new byte[sm.Length + 1];
                sm.Read(dc, 0, dc.Length); sm.Close();
            }
            oBMP.Dispose(); nBMP.Dispose();

            return dc;
        }

        protected List<_ReactFileUploadObj> DestructureFileUploadObject(string docJson)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            try {
                FileUploadObj fileObj = jss.Deserialize<FileUploadObj>(docJson);
                return new List<_ReactFileUploadObj>() { new _ReactFileUploadObj() { base64 = fileObj.base64, fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType } };
            } 
            catch {
                List<_ReactFileUploadObj> reactFileArray = jss.Deserialize<List<_ReactFileUploadObj>>(docJson);
                return reactFileArray;
            }

        }
        protected List<_ReactFileUploadObj> AddDoc(string docJson, string docId, string tableName, string keyColumnName, string columnName, bool resizeToIcon = false)
        {
            byte[] storedContent = null;
            bool dummyImage = false;
            Func<List<_ReactFileUploadObj>, List<_ReactFileUploadObj>> resizeImages = (l) =>
            {
                if (!resizeToIcon) return l;
                List<_ReactFileUploadObj> x = new List<_ReactFileUploadObj>();
                foreach (_ReactFileUploadObj fileObj in l)
                {
                    try
                    {
                        dummyImage = fileObj.base64 == "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=";
                        byte[] content = Convert.FromBase64String(fileObj.base64);
                        if (fileObj.base64.Length > 0 && (fileObj.mimeType ?? "application/octet-stream").StartsWith("image/"))
                        {
                            try
                            {
                                content = ResizeImage(Convert.FromBase64String(fileObj.base64));
                            }
                            catch
                            {
                            }
                        }
                        x.Add(new _ReactFileUploadObj() { base64 = Convert.ToBase64String(content), fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType });
                    }
                    catch
                    {
                    }
                }
                return x;
            }
            ;
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                List<_ReactFileUploadObj> fileArray = DestructureFileUploadObject(docJson);
                //FileUploadObj fileObj = jss.Deserialize<FileUploadObj>(docJson);
                _ReactFileUploadObj fileObj = fileArray.Count > 0 ? fileArray[0] : new _ReactFileUploadObj();
                bool backwardCompatible = false;
                if (fileArray.Count == 0)
                {
                    // empty list means DELETE 
                    new AdminSystem().UpdDbImg(docId, tableName, keyColumnName, columnName, null, LcAppConnString, LcAppPw);
                }
                else if (fileArray.Count > (backwardCompatible ? 1 : 0))
                {
                    var resizedFiles = resizeImages(fileArray);
                    byte[] fileStreamHeader = EncodeFileStreamHeader(fileArray);
                    byte[] content = System.Text.UTF8Encoding.UTF8.GetBytes(docJson);
                    storedContent = new byte[content.Length + fileStreamHeader.Length];
                    Array.Copy(fileStreamHeader, storedContent, fileStreamHeader.Length);
                    Array.Copy(content, 0, storedContent, fileStreamHeader.Length, content.Length);
                    new AdminSystem().UpdDbImg(docId, tableName, keyColumnName, columnName, storedContent, LcAppConnString, LcAppPw);
                    return resizedFiles;
                }
                else if (!string.IsNullOrEmpty(fileObj.base64))
                {
                    byte[] content = Convert.FromBase64String(fileObj.base64);
                    dummyImage = fileObj.base64 == "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=";
                    if (resizeToIcon && fileObj.base64.Length > 0 && (fileObj.mimeType ?? "application/octet-stream").StartsWith("image/"))
                    {
                        try
                        {
                            content = ResizeImage(Convert.FromBase64String(fileObj.base64));
                        }
                        catch
                        {
                        }
                    }
                    byte[] fileStreamHeader = EncodeFileStreamHeader(fileObj);
                    if (content.Length == 0 || dummyImage)
                    {
                        storedContent = null;
                    }
                    else if ((fileObj.mimeType ?? "application/octet-stream").StartsWith("image/") && true)
                    {
                        // backward compatability with asp.net side, only store image and not fileinfo
                        storedContent = content;
                    }
                    else
                    {
                        storedContent = new byte[content.Length + fileStreamHeader.Length];
                        Array.Copy(fileStreamHeader, storedContent, fileStreamHeader.Length);
                        Array.Copy(content, 0, storedContent, fileStreamHeader.Length, content.Length);
                    }
                    new AdminSystem().UpdDbImg(docId, tableName, keyColumnName, columnName, content.Length == 0 || dummyImage ? null : storedContent, LcAppConnString, LcAppPw);
                    return resizeToIcon && content.Length > 0 && !dummyImage ? new List<_ReactFileUploadObj>() { new _ReactFileUploadObj() { base64 = Convert.ToBase64String(content), fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType }} : null;
                }
                else
                {
                    // no content means unchanged
                }
                return null;
            }
            catch (Exception ex) { throw new Exception("invalid attachment format " + (string.IsNullOrEmpty(docId) ? "missing master key id" : ex.Message)); }
        }

        // Overload to handle customized SMTP configuration.
        private Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, string smtp, string bcc=null)
        {
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, new List<System.Net.Mail.Attachment>(), smtp, bcc);
        }

        // "to" may contain email addresses separated by ";".
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, string bcc=null)
        {
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, new List<KeyValuePair<string, byte[]>> { }, bcc);
        }

        // Overload to handle attachments and being called by the above.
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, List<KeyValuePair<string, byte[]>> att, string bcc = null)
        {
            List<System.Net.Mail.Attachment> mailAtts = new List<System.Net.Mail.Attachment>();
            foreach (var f in att)
            {
                var ms = new MemoryStream(f.Value);
                mailAtts.Add(new System.Net.Mail.Attachment(ms, f.Key));
            }
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, mailAtts, string.Empty, bcc);
        }

        // Overload to handle attachments and being called by the above and should not be called publicly.
        // Return number of emails sent today; users should not exceed 10,000 a day in order to avoid smtp IP labelled as spam email.
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, List<System.Net.Mail.Attachment> att, string smtp, string bcc=null)
        {
            Int32 iEmailsSentToday = (new RO.WebRules.WebRule()).CountEmailsSent();
            string[] smtpConfig = (string.IsNullOrEmpty(smtp) ? Config.SmtpServer : smtp).Split(new char[] { '|' });
            bool bSsl = smtpConfig[0].Trim() == "true" ? true : false;
            int port = smtpConfig.Length > 1 ? int.Parse(smtpConfig[1].Trim()) : 25;
            string server = smtpConfig.Length > 2 ? smtpConfig[2].Trim() : null;
            string username = smtpConfig.Length > 3 ? smtpConfig[3].Trim() : null;
            string password = smtpConfig.Length > 4 ? smtpConfig[4].Trim() : null;
            string domain = smtpConfig.Length > 5 ? smtpConfig[5].Trim() : null;
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
            string[] receipients = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] bccRecipients = bcc != null ? bcc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries) : new string[]{};
            foreach (var t in receipients)
            {
                mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
            }
            foreach (var t in bccRecipients)
            {
                mm.Bcc.Add(new System.Net.Mail.MailAddress(t.Trim()));
            }
            mm.Subject = subject;
            mm.Body = body;
            if (att != null && att.Count > 0)
            {
                foreach (var item in att) { mm.Attachments.Add(item); }
            }
            mm.IsBodyHtml = isHtml;

            mm.From = new System.Net.Mail.MailAddress(string.IsNullOrEmpty(username) || !(username ?? "").Contains("@") ? from : username, string.IsNullOrEmpty(fromTitle) ? from : fromTitle);    // Address must be the same as the smtp login user.
            mm.ReplyToList.Add(new System.Net.Mail.MailAddress(string.IsNullOrEmpty(replyTo) ? from : replyTo)); // supplied from would become reply too for the 'sending on behalf of'
            (new RO.WebRules.WebRule()).SendEmail(bSsl, port, server, username, password, domain, mm);
            mm.Dispose();   // Error is trapped and reported from the caller.
            return iEmailsSentToday;
        }


        protected DataTable GetLis(string getLisMethod, int systemId, int screenId, string searchStr, List<string> criteria, string filterId, string conn, string isSys, int topN)
        {
            // this is a system wide format and must be kept in sync with robot if there is any change of it
            DataView dvCri = GetCriCache(systemId, screenId);
            UsrCurr uc = LCurr;
            UsrImpr ui = LImpr;
            LoginUsr usr = LUser;
            int rowExpected = dvCri.Count;
            DataTable dtLastScrCriteria = _GetLastScrCriteria(screenId, rowExpected);
            DataSet ds = criteria == null || criteria.Count == 0 ? MakeScrCriteria(screenId, dvCri, dtLastScrCriteria,true,false) : MakeScrCriteria(screenId, dvCri, criteria,true,false);
            DataTable dt = (new AdminSystem()).GetLis(screenId, getLisMethod, true, "Y", topN, isSys != "N" ? (string)null : LcAppConnString, isSys != "N" ? null : LcAppPw,
                string.IsNullOrEmpty(filterId) ? 0 : int.Parse(filterId), searchStr.StartsWith("**") ? searchStr.Substring(2) : "", searchStr.StartsWith("**") ? "" : searchStr,
                dvCri, ui, uc, ds);

            return dt;
        }

        protected DataTable GetDdl(string getLisMethod, bool bAddNew, int systemId, int screenId, string searchStr, string conn, string isSys, string sp, string requiredValid, int topN)
        {
            UsrCurr uc = LCurr;
            UsrImpr ui = LImpr;
            LoginUsr usr = LUser;
            DataTable dt = null;
            Regex cleanup = new Regex("^undefined|null$");
            if (!string.IsNullOrEmpty(sp))
            {
                var regex = new System.Text.RegularExpressions.Regex("C[0-9]+$");
                var scrCriId = sp.Replace(regex.Replace(sp, ""), "").Replace("C", "");
                int CriCnt = (new AdminSystem()).CountScrCri(scrCriId, string.IsNullOrEmpty(conn) ? "N" : "Y", LcSysConnString, LcAppPw);
                dt = (new AdminSystem()).GetScreenIn(screenId.ToString(), sp, CriCnt, requiredValid, topN,
                searchStr.StartsWith("**") ? "" : searchStr, !searchStr.StartsWith("**"), searchStr.StartsWith("**") ? cleanup.Replace(searchStr.Substring(2),"") : "", ui, uc,
                isSys != "N" ? (string)null : LcAppConnString,
                isSys != "N" ? null : LcAppPw);
            }
            else
            {
                dt = (new AdminSystem()).GetDdl(screenId, getLisMethod, bAddNew, !searchStr.StartsWith("**"), topN, searchStr.StartsWith("**") ? cleanup.Replace(searchStr.Substring(2),"") : "",
                    isSys != "N" ? (string)null : LcAppConnString,
                    isSys != "N" ? null : LcAppPw, searchStr.StartsWith("**") ? "" : searchStr, ui, uc);
            }
            return dt;
        }


        protected void ValidatedMstId(string getListMethod, byte csy, int screenId, string query, List<string> criteria)
        {
            DataTable dtSuggest = GetLis(getListMethod, csy, screenId, query, criteria, "0", "", "N", 1);
            if (dtSuggest.Rows.Count == 0)
            {
                throw new Exception("access denied");
            }
        }
        protected List<string> MatchScreenCriteria(DataView dvCri, string jsonCri)
        {
            List<string> scrCri = new List<string>();
            if (!string.IsNullOrEmpty(jsonCri))
            {
                try
                {
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> cri = jss.Deserialize<Dictionary<string, string>>(jsonCri);
                    foreach (DataRowView drv in dvCri)
                    {
                        scrCri.Add(cri[drv["ColumnName"].ToString()]);
                    }
                }
                catch { }
            }
            return scrCri;
        }
        protected string ToStandardString(string columnName, DataRow dr, bool includeBLOB = false) 
        {
            var colType = dr[columnName].GetType();
            return
                colType == typeof(DateTime) ? ((DateTime)dr[columnName]).ToString(((DateTime)dr[columnName]).TimeOfDay.Ticks > 0 ? "o" : "yyyy.MM.dd") :
                    colType == typeof(byte[]) ? (dr[columnName] != null && includeBLOB ? DecodeFileStream((byte[])(dr[columnName])) : null) :
                    dr[columnName].ToString();
        }

        protected AutoCompleteResponse LisSuggests(string query, string contextStr, int topN, List<string> currentCriteria = null)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int screenId = int.Parse(context["scr"]);
            UsrCurr uc = LCurr;
            UsrImpr ui = LImpr;
            LoginUsr usr = LUser;
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataView dvCri = new DataView(_GetScrCriteria(screenId));
            DataTable dtSuggest = GetLis(context["method"], csy, screenId, query, currentCriteria ?? new List<string>(), context["filter"], context["conn"], context["isSys"], topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string valFR = context.ContainsKey("mValR") ? context["mValR"] : (dtSuggest.Columns.Contains(keyF + "TextR") ? keyF + "TextR" : "");
            string dtlF = context.ContainsKey("mDtl") && false ? context["mDtl"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string dtlFR = context.ContainsKey("mDtlR") && false ? context["mDtlR"] : (dtSuggest.Columns.Contains(keyF + "DtlR") ? keyF + "DtlR" : "");
            string tipF = context.ContainsKey("mTip") && false ? context["mTip"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : (dtSuggest.Columns.Contains(keyF + "Img") ? keyF + "Img" : "");
            string iconUrlF = context.ContainsKey("mIconUrl") && false ? context["mIconUrl"] : (dtSuggest.Columns.Contains(keyF + "Url") ? keyF + "Url" : "");
            // optimization on return, requesting 100 may only return records beyond key value, this is assuming the sorting original sort sequence from backend
            string startKeyVal = context.ContainsKey("startKeyVal") ? context["startKeyVal"] : "";
            string startLabelVal = context.ContainsKey("startLabelVal") ? context["startLabelVal"].ToLowerInvariant() : "";
            bool bFullImage = context.ContainsKey("fullImage");
            bool valueIsKey = context.ContainsKey("valueIsKey") || true;
            bool hasDtlColumn = dtSuggest.Columns.Contains(keyF + "Dtl");
            bool hasValRColumn = dtSuggest.Columns.Contains(keyF + "TextR");
            bool hasDtlRColumn = dtSuggest.Columns.Contains(keyF + "DtlR");
            bool hasIconColumn = dtSuggest.Columns.Contains(keyF + "Url");
            bool hasImgColumn = dtSuggest.Columns.Contains(keyF + "Img");
            total = dtSuggest.Rows.Count;
            int skipped = 0;
            List<SerializableDictionary<string, string>> results = new List<SerializableDictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = _GetAuthRow(screenId);
            bool allowAdd = dtAuthRow.Rows.Count == 0 || dtAuthRow.Rows[0]["AllowAdd"].ToString() != "N";
            //dtSuggest.DefaultView.Sort = valF;
            int pos = 1;
            string doublestar = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            int matchCount = -1;
            bool hasMatchCount = dtSuggest.Columns.Contains("MatchCount");
            var rx = new Regex("^[^\\]]*\\]");
            bool dropEnded = string.IsNullOrEmpty(startKeyVal);

            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                if (hasMatchCount && matchCount < 0 && !string.IsNullOrEmpty(drv[keyF].ToString().Trim()))  int.TryParse(drv["MatchCount"].ToString(), out matchCount);

                string ss = drv[keyF].ToString().Trim();
                string label = drv[valF].ToString().Trim().ToLowerInvariant();
                bool startTaking = !string.IsNullOrEmpty(startLabelVal) && label.CompareTo(startLabelVal) > 0;
                //if (allowAdd || ss != string.Empty)
                if (ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss))
                    {
                        total = total - 1;
                    }
                    else if (GetCriCache(csy, screenId).Count > 0 || regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        if (dropEnded || startTaking)
                        {
                            results.Add(new SerializableDictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"labelL",rx.Replace(drv[valF].ToString(),"")}, // stripped desc
                            {"value", valueIsKey ? drv[keyF].ToString() : drv[valF].ToString()}, // visible value shown in jquery's autocomplete box, react expect this to be the key not label
                            {"iconUrl",iconUrlF !="" ?  drv[iconUrlF].ToString() : null}, // optional icon url
                            {"img", imgF !="" ? (drv[imgF].ToString() == "" ? "": "data:application/base64;base64," + Convert.ToBase64String(BlobImage(drv[imgF] as byte[],bFullImage) ?? new byte[0]))  : null}, // optional embedded image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""},// optional alternative tooltips(say expanded description)
                            {"detail",dtlF !="" ? ToStandardString(dtlF,drv.Row) : null}, // optional detail info
                            {"labelR",valFR !="" ? ToStandardString(valFR,drv.Row) : null}, // optional title(right hand side for react presentation)
                            {"detailR",dtlFR !="" ? ToStandardString(dtlFR,drv.Row) : null} // optional detail info(right hand side for react presentation)
                            /* more can be added in the future for say multi-column list */
                            });
                        }
                        else 
                        {
                            dropEnded = (ss == startKeyVal || (label.CompareTo(startLabelVal) >= 0));
                            skipped = skipped + 1;
                        }
                    }
                    else
                    {
                        total = total - 1;
                        matchCount = matchCount - 1;
                    }
                    if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.skipped = skipped;
            ret.topN = topN;
            ret.matchCount = matchCount < 0 ? 0 : matchCount;

            return ret;
        }

        protected string getNonEmptyStr(string str)
        {
            return (!string.IsNullOrEmpty(str)) ? str : " ";
        }

        protected AutoCompleteResponse ddlSuggests(string inQuery, Dictionary<string, string> context, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int screenId = int.Parse(context["scr"]);
            UsrCurr uc = LCurr;
            UsrImpr ui = LImpr;
            LoginUsr usr = LUser;
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataTable dtSuggest = GetDdl(context["method"], context["addnew"] == "Y", csy, screenId, inQuery, context["conn"], context["isSys"], context.ContainsKey("sp") ? context["sp"] : "", context.ContainsKey("requiredValid") ? context["requiredValid"] : "", (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"])) || (context.ContainsKey("pMKeyCol") && !string.IsNullOrEmpty(context["pMKeyCol"])) ? 0 : topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") ? context["mTip"] : "";
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : "";
            bool valueIsKey = context.ContainsKey("valueIsKey") || true;
            total = dtSuggest.Rows.Count;
            List<SerializableDictionary<string, string>> results = new List<SerializableDictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = _GetAuthRow(screenId);
            string[] DesiredKeys = inQuery.StartsWith("**") ? inQuery.Substring(2).Replace("(", "").Replace(")", "").Replace("undefined", "").Replace("null", "").Split(new char[] { ',' }) : new string[0];
            string query = System.Text.RegularExpressions.Regex.Escape(inQuery.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape("**");
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            //dtSuggest.DefaultView.Sort = valF;
            string filter = "";
            string[] needQuoteType = { "Char", "Date", "Time", "String" };
            if (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"]))
            {
                string[] x = context["refCol"].Split(new char[] { '_' });
                bool isList = context.ContainsKey("refColValIsList") && context["refColValIsList"] == "Y";
                string[] filterColumnIsIntType = { "Int", "SmallInt", "TinyInt", "BigInt" };
                bool filterColumnIsList = context.ContainsKey("refColDataType") && "TinyInt,SmallInt,Int,BigInt,Char,NChar".IndexOf(context["refColDataType"]) >= 0 && dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains("String");
                try
                {
                    if (filterColumnIsList)
                    {
                        if ("Char,NChar".IndexOf(context["refColDataType"]) >= 0)
                            filter = string.Format(" (',' + SUBSTRING(ISNULL({0},'()'),2,LEN(ISNULL({0},'()'))-2) + ',' LIKE '%,''{1}'',%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                        else
                            filter = string.Format(" (',' + SUBSTRING(ISNULL({0},'()'),2,LEN(ISNULL({0},'()'))-2) + ',' LIKE '%,{1},%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                    }
                    else
                    {
                        bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                        if (needQuote)
                        {
                            if (isList)
                                filter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], "'" + context["refColVal"].Replace("'", "''").Replace(",", "','") + "'");
                            else
                            {
                                filter = string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                            }

                        }
                        else
                        {
                            if (isList)
                                filter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            else
                            {
                                filter = string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            }

                        }
                    }

                }
                catch { }
            }
            if (context.ContainsKey("pMKeyCol") && !string.IsNullOrEmpty(context["pMKeyCol"]) && dtSuggest.Columns.Contains(context["pMKeyCol"]))
            {
                string[] x = context["pMKeyCol"].Split(new char[] { '_' });
                try
                {
                    bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                    if (needQuote)
                        filter = filter + (!string.IsNullOrEmpty(filter) ? " AND " : string.Empty) + string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["pMKeyColVal"].Replace("'", "''"));
                    else
                        filter = filter + (!string.IsNullOrEmpty(filter) ? " AND " : string.Empty) + string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["pMKeyColVal"]);
                }
                catch { }
            }
            try
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    dtSuggest.DefaultView.RowFilter = filter;
                    total = dtSuggest.DefaultView.Count;
                }
            }
            catch { }
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[keyF].ToString().Trim();
                if (true || ss != string.Empty) // include empty line for quick empty out selection
                {
                    if (Choices.ContainsKey(ss) || (query.StartsWith(doublestar) && !DesiredKeys.Contains(ss) && !DesiredKeys.Contains("'" + ss + "'")))
                    {
                        total = total - 1;
                    }
                    else if (regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        results.Add(new SerializableDictionary<string, string> { 
                            {"key", getNonEmptyStr(drv[keyF].ToString())}, // internal key 
                            {"label", getNonEmptyStr(drv[valF].ToString())}, // visible dropdown list as used in jquery's autocomplete
                            {"value", getNonEmptyStr(valueIsKey ? drv[keyF].ToString() : drv[valF].ToString())}, // visible value shown in jquery's autocomplete box, react expect it to be key not label
                            {"img", imgF !="" ? drv[imgF].ToString() : null}, // optional image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
                    }
                    /* use 25 to match default front end of react autocomplete to show the ... */
                    //if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                    if (Choices.Count >= (topN > 0 ? topN : 25)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.topN = topN;

            return ret;
        }

        protected Dictionary<string, DataRow> GetScreenMenu(byte systemId, int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenMenu_" + systemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString();
            int minutesToCache = 1;
            Dictionary<string,DataRow> menu = cache[cacheKey] as Dictionary<string,DataRow>;
            if (menu == null || !menu.ContainsKey(screenId.ToString()))
            {
                Dictionary<string, DataRow> m = menu ?? new Dictionary<string, DataRow>();
                DataTable dtMenu = (new MenuSystem()).GetMenu(LUser.CultureId, systemId, LImpr, LcSysConnString, LcAppPw, screenId, null, null);
                if (dtMenu.Rows.Count > 0)
                {
                    m[screenId.ToString()] = dtMenu.Rows[0];
                }
                if (menu == null)
                {
                    menu = m;
                    cache.Insert(cacheKey, m, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                        , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                }
            }
            return menu;
        }
        protected byte[] EncodeFileStreamHeader(_ReactFileUploadObj fileObj)
        {
            /* store as 256 byte UTF8 json header + actual binary file content 
              * if header info > 256 bytes use compact header(256 bytes) + actual header + actual binary file content
              */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string contentHeader = jss.Serialize(new FileInStreamObj() { fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", height=fileObj.height, width=fileObj.width, size=fileObj.size, extensionSize = 0 });
            byte[] streamHeader = Enumerable.Repeat((byte)0x20, 256).ToArray();
            int headerLength = System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader).Length;
            string compactHeader = jss.Serialize(new FileInStreamObj() { fileName = "", lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", height=fileObj.height, width=fileObj.width, size=fileObj.size, extensionSize = headerLength });
            int compactHeaderLength = System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader).Length;
            if (headerLength <= 256)
                Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader), streamHeader, headerLength);
            else
            {
                Array.Resize(ref streamHeader, 256 + headerLength);
                Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), streamHeader, compactHeaderLength);
                Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), 0, streamHeader, 255, headerLength);
            }
            return streamHeader;
        }
        protected byte[] EncodeFileStreamHeader(List<_ReactFileUploadObj> fileObjList)
        {
            /* store as 256 byte UTF8 json header + actual JSON stream(whatever it is ) 
              */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            byte[] streamHeader = Enumerable.Repeat((byte)0x20, 256).ToArray();
            int headerLength = 0;
            string compactHeader = jss.Serialize(new FileInStreamObj() { fileName = "", contentIsJSON = true, extensionSize = 0 });
            int compactHeaderLength = System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader).Length;
            Array.Resize(ref streamHeader, 256 + headerLength);
            Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), streamHeader, compactHeaderLength);
            Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), 0, streamHeader, 255, headerLength);
            return streamHeader;
        }
        protected string DecodeFileStream(byte[] content)
        {
            byte[] header = content.Length > 256 ? content.Take(256).ToArray() : null;
            if (header != null)
            {
                try
                {
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    jss.MaxJsonLength = Int32.MaxValue;
                    string headerString = System.Text.UTF8Encoding.UTF8.GetString(header);
                    FileInStreamObj fileInfo = jss.Deserialize<FileInStreamObj>(headerString.Substring(0,headerString.IndexOf('}')+1));
                    int extensionSize = fileInfo.extensionSize;
                    if (extensionSize > 0)
                    {
                        header = content.Skip(256).Take(fileInfo.extensionSize).ToArray();
                        headerString = System.Text.UTF8Encoding.UTF8.GetString(header);
                        fileInfo = jss.Deserialize<FileInStreamObj>(headerString.Substring(0, headerString.IndexOf('}')+1));
                    }
                    byte[] fileContent = content.Skip(256 + extensionSize).Take(content.Length - 256 - extensionSize).ToArray();
                    if (fileInfo.contentIsJSON)
                    {
                        return System.Text.UTF8Encoding.UTF8.GetString(fileContent);
                    }
                    else
                        return jss.Serialize(new FileUploadObj() { base64 = Convert.ToBase64String(fileContent), mimeType = fileInfo.mimeType, lastModified = fileInfo.lastModified, fileName = fileInfo.fileName });

                }
                catch (Exception ex)
                {
                    return Convert.ToBase64String(content);
                }
            }
            else
            {
                return Convert.ToBase64String(content);
            }
        }

        protected byte[] BlobImage(byte[] content, bool bFullBLOB = false)
        {
            Func<byte[], byte[]> tryResizeImage = (ba) =>
            {
                try
                {
                    return ResizeImage(ba, 96);
                }
                catch
                {
                    return null;
                }
            };

            try
            {
                string fileContent = DecodeFileStream(content);
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                try
                {
                    FileUploadObj fileInfo = jss.Deserialize<FileUploadObj>(fileContent);
                    return bFullBLOB ? Convert.FromBase64String(fileInfo.base64) : tryResizeImage(Convert.FromBase64String(fileInfo.base64));
                }
                catch
                {
                    try
                    {
                        List<_ReactFileUploadObj> fileList = jss.Deserialize<List<_ReactFileUploadObj>>(fileContent);
                        List<FileUploadObj> x = new List<FileUploadObj>();
                        foreach (var fileInfo in fileList)
                        {
                            return bFullBLOB ? Convert.FromBase64String(fileInfo.base64) : tryResizeImage(Convert.FromBase64String(fileInfo.base64));
                        }
                        return null;
                    }
                    catch
                    {
                        return bFullBLOB ? Convert.FromBase64String(fileContent) : tryResizeImage(Convert.FromBase64String(fileContent));
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        protected string BlobPlaceHolder(byte[] content)
        {
            Func<byte[], byte[]> tryResizeImage = (ba) =>
            {
                try
                {
                    return ResizeImage(ba,96);
                }
                catch
                {
                    return null;
                }
            };

            try
            {
                string fileContent = DecodeFileStream(content);
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                try
                {
                    FileUploadObj fileInfo = jss.Deserialize<FileUploadObj>(fileContent);
                    byte[] icon = tryResizeImage(Convert.FromBase64String(fileInfo.base64));
                    return jss.Serialize(new FileUploadObj() { icon = icon != null ? Convert.ToBase64String(icon) : null, mimeType = fileInfo.mimeType, lastModified = fileInfo.lastModified, fileName = fileInfo.fileName });
                }
                catch {
                    try
                    {
                        List<_ReactFileUploadObj> fileList = jss.Deserialize<List<_ReactFileUploadObj>>(fileContent);
                        List<FileUploadObj> x = new List<FileUploadObj>();
                        foreach (var fileInfo in fileList)
                        {
                            byte[] icon = tryResizeImage(Convert.FromBase64String(fileInfo.base64));
                            x.Add(new FileUploadObj() { icon = icon != null ? Convert.ToBase64String(icon) : null, mimeType = fileInfo.mimeType, lastModified = fileInfo.lastModified, fileName = fileInfo.fileName });
                        }
                        return jss.Serialize(x);
                    }
                    catch
                    {
                        byte[] icon = tryResizeImage(Convert.FromBase64String(fileContent));
                        return jss.Serialize(new List<FileUploadObj>() { new FileUploadObj() { icon = icon != null ? Convert.ToBase64String(icon) : null, mimeType = "image/jpeg", fileName = "image" }});
                    }
                }
            }
            catch {
                return null;
            }
        }
        protected List<SerializableDictionary<string, string>> DataTableToListOfObject(DataTable dt,IncludeBLOB includeBLOB=IncludeBLOB.Icon, Dictionary<string,DataRow> colAuth = null, HashSet<string> utcColumns = null)
        {

            //var x = dt.AsEnumerable().Select(
            //        row => SerializableDictionary<string, string>.CreateInstance(dt.Columns.Cast<DataColumn>().ToDictionary(
            //                column => column.ColumnName,
            //                column => row[column].ToString()
            //            ))
            //    );
            List<SerializableDictionary<string, string>> ret = new List<SerializableDictionary<string, string>>();
            if (dt == null) return ret;
            foreach (DataRow dr in dt.Rows)
            {
                SerializableDictionary<string, string> rec = new SerializableDictionary<string, string>();
                foreach (DataColumn col in dt.Columns)
                {
                    var columnName = col.ColumnName;
                    var colType = dr[columnName].GetType();
                    if (colAuth == null
                        || columnName == GetMstKeyColumnName()
                        || columnName == GetDtlKeyColumnName()
                        || (colAuth.ContainsKey(columnName) && colAuth[columnName]["ColVisible"].ToString() != "N")
                        || (colAuth.ContainsKey(columnName + "Text") && colAuth[columnName + "Text"]["ColVisible"].ToString() != "N")
                        )
                        rec[columnName] =
                            colType == typeof(DateTime) ? (((DateTime)dr[columnName]).ToString("o") + (utcColumns == null || !utcColumns.Contains(columnName) ? "" : "Z")) :
                            colType == typeof(byte[]) ? (dr[columnName] != null ? ((includeBLOB == IncludeBLOB.Content || ((byte[])(dr[columnName])).Length < 256) ? DecodeFileStream((byte[])(dr[columnName])) : BlobPlaceHolder((byte[])(dr[columnName]))) : null) :
                            dr[columnName].ToString();
                    else rec[columnName] = null;
                }
                ret.Add(rec);
            }
            return ret;
        }

        protected List<SerializableDictionary<string, string>> DataTableToListOfObject(DataTable dt, bool includeBLOB, Dictionary<string, DataRow> colAuth)
        {
            return DataTableToListOfObject(dt, includeBLOB ? IncludeBLOB.Content : IncludeBLOB.Icon, colAuth);
        }

        protected List<SerializableDictionary<string, string>> DataTableToListOfDdlObject(DataRowView drCri, DataTable dt)
        {

            //var x = dt.AsEnumerable().Select(
            //        row => SerializableDictionary<string, string>.CreateInstance(dt.Columns.Cast<DataColumn>().ToDictionary(
            //                column => column.ColumnName,
            //                column => row[column].ToString()
            //            ))
            //    );

            string keyColumn = drCri["DdlKeyColumnName"].ToString();
            string refColumn = drCri["DdlRefColumnName"].ToString();
            int idx = 0;

            List<SerializableDictionary<string, string>> ret = new List<SerializableDictionary<string, string>>();
            foreach (DataRow dr in dt.Rows)
            {
                SerializableDictionary<string, string> rec = new SerializableDictionary<string, string>();

                rec.Add("key", dr[keyColumn].ToString());
                rec.Add("label", dr[refColumn].ToString());
                rec.Add("value", dr[keyColumn].ToString());
                rec.Add("idx", idx.ToString());
                ret.Add(rec);

                idx++;
            }
            return ret;
        }

        protected SerializableDictionary<string, SerializableDictionary<string, string>> DataTableToLabelObject(DataTable dt, List<string> keyColumns)
        {
            var ret = new SerializableDictionary<string, SerializableDictionary<string, string>>();
            int tabIndex = 10;
            foreach (DataRow dr in dt.Rows)
            {
                var rec = new SerializableDictionary<string, string>();
                foreach (DataColumn col in dt.Columns)
                {
                    rec[col.ColumnName] = dr[col.ColumnName].ToString();
                }
                rec["TabIndex"] = tabIndex.ToString();
                ret.Add(GetKeyValue(dr, keyColumns), rec);
                tabIndex += 10;
            }
            return ret;
        }

        private string GetKeyValue(DataRow dr, List<string> keyColumns)
        {
            string key = string.Empty;
            foreach(string column in keyColumns)
            {
                key += dr[column];
            }
            return key;
        }

        protected List<SerializableDictionary<string, string>> DataTableToListOfDdlObject(DataTable dt, string keyColumn, string refColumn, List<string> addtionalColumns)
        {
            //var x = dt.AsEnumerable().Select(
            //        row => SerializableDictionary<string, string>.CreateInstance(dt.Columns.Cast<DataColumn>().ToDictionary(
            //                column => column.ColumnName,
            //                column => row[column].ToString()
            //            ))
            //    );

            int idx = 0;
            var x = DataTableToListOfObject(dt);
            List<SerializableDictionary<string, string>> ret = new List<SerializableDictionary<string, string>>();
            foreach (var o in x)
            {
                SerializableDictionary<string, string> rec = new SerializableDictionary<string, string>();

                rec.Add("key", o[keyColumn].ToString());
                rec.Add("label", string.IsNullOrEmpty(o[refColumn]) ? " " : o[refColumn]); // react doesn't like empty label
                rec.Add("value", o[keyColumn].ToString());
                rec.Add("idx", idx.ToString());

                foreach(string column in addtionalColumns)
                {
                    rec.Add(column, o[column].ToString());
                }

                ret.Add(rec);

                idx++;
            }
            return ret;
        }
        protected ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> DataTableToLabelResponse(DataTable dt, List<string> keyColumns)
        {
            ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> mr = new ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>>();
            AutoCompleteResponseObj r = new AutoCompleteResponseObj();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            mr.errorMsg = "";
            r.data = DataTableToLabelObject(dt, keyColumns);
            r.query = "";
            r.topN = 0;
            r.total = dt.Rows.Count;
            mr.data = r;
            mr.status = "success";
            return mr;
        }

        protected ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> DataTableToApiResponse(DataTable dt, string query, int topN)
        {
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            AutoCompleteResponse r = new AutoCompleteResponse();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            mr.errorMsg = "";
            r.data = DataTableToListOfObject(dt);
            r.query = query;
            r.topN = topN;
            r.total = dt.Rows.Count;
            mr.data = r;
            mr.status = "success";
            return mr;
        }

        protected ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> DataTableToDdlApiResponse(DataTable dt, string keyColumn, string refColumn)
        {
            return DataTableToDdlApiResponse(dt, keyColumn, refColumn, new List<string>() { });
        }

        protected ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> DataTableToDdlApiResponse(DataTable dt, string keyColumn, string refColumn, List<string> additionalColumns)
        {
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            AutoCompleteResponse r = new AutoCompleteResponse();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            mr.errorMsg = "";
            r.data = DataTableToListOfDdlObject(dt, keyColumn, refColumn, additionalColumns);
            mr.data = r;
            mr.status = "success";
            return mr;
        }

        protected string GetValueOrDefault(SerializableDictionary<string, string> options, string buttonName, string dflt)
        {
            return options.ContainsKey(buttonName) ? options[buttonName] : dflt;
        }
        protected string GetCriteriaLs(ArrayList values)
        {
            string result = string.Empty;

            if (values == null)
            {
                return result;
            }

            foreach (string val in values)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += ",";
                    }

                    result += "'" + val + "'";
                }
            }

            if (!string.IsNullOrEmpty(result))
            {
                result = "(" + result + ")";
            }

            return result;
        }

        private List<MenuNode> BuildMenuTree(HashSet<string> mh, DataView dvMenu, string[] path, int level, bool bRecurr)
        {
            List<MenuNode> menus = new List<MenuNode>();
            string selectedQid = string.Join(".", path);
            //string[] subPath = path.Skip<string>(1).ToArray<string>();
            //string[] emptyPath = new string[0];
            if (mh.Count == 0) foreach (DataRow dr in dvMenu.Table.Rows) { mh.Add(dr["MenuId"].ToString()); }

            foreach (DataRowView drv in dvMenu)
            {
                MenuNode mt = new MenuNode
                {
                    ParentQId = drv["ParentQId"].ToString(),
                    ParentId = drv["ParentId"].ToString(),
                    QId = drv["QId"].ToString(),
                    MenuId = drv["MenuId"].ToString(),
                    NavigateUrl = drv["NavigateUrl"].ToString(),
                    QueryStr = drv["QueryStr"].ToString(),
                    IconUrl = drv["IconUrl"].ToString(),
                    Popup = drv["Popup"].ToString(),
                    GroupTitle = drv["GroupTitle"].ToString(),
                    MenuText = drv["MenuText"].ToString(),
                    Selected = selectedQid.StartsWith(drv["QId"].ToString()),
                    Level = level,
                    Children = bRecurr ? BuildMenuTree(mh,new DataView(dvMenu.Table, string.Format("ParentId = {0}", drv["MenuId"].ToString()), "ParentId, ParentQId,Qid", DataViewRowState.CurrentRows), path, level + 1, bRecurr) : new List<MenuNode>()
                };
                if (mt.ParentId == "" || mh.Contains(mt.ParentId)) menus.Add(mt);
            }
            return menus;
        }

        public AsmxBase()
        {
 
        }


        protected ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> emptyListResponse()
        {
            List<SerializableDictionary<string, string>> content = new List<SerializableDictionary<string, string>>();
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.data = new List<SerializableDictionary<string, string>>();
            mr.status = "success";
            mr.errorMsg = "";
            return mr;
        }
        protected ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> emptyAutoCompleteResponse()
        {
            List<SerializableDictionary<string, string>> content = new List<SerializableDictionary<string, string>>();
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            AutoCompleteResponse r = new AutoCompleteResponse();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            mr.errorMsg = "";
            r.data = content;
            r.query = "";
            r.topN = 0;
            r.total = 0;
            mr.data = r;
            mr.status = "success";
            return mr;
        }
        protected ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMultiDoc(string mstId, string dtlId, bool isMaster, string[] docIdList, string screenColumnName, string columnName, string mstTableName, string ddlKeyTableName, string mstKeyColumnName)
        {
            //screenColumnName = "FirmDoc25";
            //string columnName = "FirmDoc";
            //string mstTableName = "FirmInfo";
            //string ddlKeyTableName = "FirmDoc";
            //string mstKeyColumnName = "FirmInfoId";
            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                List<string> deletedDocId = new List<string>();
                DataTable dtDocList = _GetDocList(mstId, screenColumnName);
                foreach (var docId in docIdList)
                {
                    bool hasDoc = (from x in dtDocList.AsEnumerable() where !string.IsNullOrEmpty(docId) && x["DocId"].ToString() == docId select x).Count() > 0;
                    (new AdminSystem()).DelDoc(isMaster ? mstId : dtlId, docId, LUser.UsrId.ToString(), ddlKeyTableName, mstTableName, columnName, mstKeyColumnName, LcAppConnString, LcAppPw);
                    deletedDocId.Add(docId);
                }

                ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                SaveDataResponse result = new SaveDataResponse()
                {
                    mst = isMaster ? new SerializableDictionary<string, string>() { { "docIdList", jss.Serialize(deletedDocId) } } : null,
                    dtl = !isMaster ? new List<SerializableDictionary<string, string>>() {
                        new SerializableDictionary<string, string>() {{"docIdList", jss.Serialize(deletedDocId)}}
                    } : null
                };
                string msg = "Document(s) Deleted";
                result.message = msg;
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "S", screenColumnName)); ;
            return ret;
        }
        protected ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> SaveMultiDoc(string mstId, string dtlId, bool isMaster, string docId, bool overwrite, string screenColumnName, string tableName, string docJson, SerializableDictionary<string, string> options)
        {
            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                List<_ReactFileUploadObj> fileArray = DestructureFileUploadObject(docJson);
                DataTable dtDocList = _GetDocList(mstId, screenColumnName);
                bool hasDoc = (from x in dtDocList.AsEnumerable() where x["DocId"].ToString() == docId select x).Count() > 0;
                _ReactFileUploadObj fileObj = fileArray[0];
                // In case DocId has not been saved properly, always find the most recent to replace as long as it has the same file name:
                string DocId = string.Empty;
                byte[] content = Convert.FromBase64String(fileObj.base64);
                DocId = new AdminSystem().GetDocId(isMaster ? mstId : dtlId, tableName, fileObj.fileName, LUser.UsrId.ToString(), LcAppConnString, LcAppPw);
                if (DocId == string.Empty || !overwrite)
                {
                    DocId = new AdminSystem().AddDbDoc(isMaster ? mstId : dtlId, tableName, fileObj.fileName, fileObj.mimeType, content.Length, content, LcAppConnString, LcAppPw, LUser);
                }
                else
                {
                    new AdminSystem().UpdDbDoc(DocId, tableName, fileObj.fileName, fileObj.mimeType, content.Length, content, LcAppConnString, LcAppPw, LUser);
                }

                ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                SaveDataResponse result = new SaveDataResponse()
                {
                    mst = isMaster ? new SerializableDictionary<string, string>() { { "DocId", DocId } } : null,
                    dtl = !isMaster ? new List<SerializableDictionary<string, string>>() {
                        new SerializableDictionary<string, string>() {{ "DocId", DocId }}
                    } : null
                };
                string msg = "Document Saved";
                result.message = msg;
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "S", screenColumnName)); ;
            return ret;
        }
        protected ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetMultiDocList(string mstId, string dtlId, bool isMaster, string screenColumnName, string getDdlMethod)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                var dtScreenCriteria = _GetScrCriteria(GetScreenId());
                var dvScreenCriteria = new DataView(dtScreenCriteria);
                var LastScreenCriteria = GetScreenCriteria();
                var savedScreenCriteria = LastScreenCriteria.data.data;
                var currentScrCriteria = _GetCurrentScrCriteria(dvScreenCriteria, savedScreenCriteria, true);

                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(dvScreenCriteria, new JavaScriptSerializer().Serialize(currentScrCriteria.Item2)));
                DataTable dt = (new AdminSystem()).GetDdl(GetScreenId(), getDdlMethod, false, false, 0, mstId, LcAppConnString, LcAppPw, string.Empty, LImpr, LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", screenColumnName, emptyAutoCompleteResponse), AllowAnonymous());
            return ret;
        }
        protected LoadScreenPageResponse _GetScreenMetaData(bool accessControlled)
        {
            int screenId = GetScreenId();
            byte systemId = GetSystemId();
            string custLabelCat = GetProgramName().Replace(screenId.ToString(), "");
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, accessControlled, false, false, true);
            var dtAuthCol = _GetAuthCol(screenId);
            var dtAuthRow = _GetAuthRow(screenId);
            var dtScreenLabel = _GetScreenLabel(screenId);
            var dtScreenCriteria = _GetScrCriteria(screenId);
            var dtScreenFilter = _GetScreenFilter(screenId);
            var dtScreenHlp = _GetScreenHlp(screenId);
            var dtScreenButtonHlp = _GetScreenButtonHlp(screenId);
            var dtLabel = _GetLabels(custLabelCat);

            LoadScreenPageResponse result = new LoadScreenPageResponse()
            {
                AuthCol = DataTableToListOfObject(dtAuthCol),
                AuthRow = DataTableToListOfObject(dtAuthRow),
                ColumnDef = DataTableToListOfObject(dtScreenLabel),
                Label = DataTableToListOfObject(dtLabel),
                ScreenButtonHlp = DataTableToListOfObject(dtScreenButtonHlp),
                ScreenCriteria = DataTableToListOfObject(dtScreenCriteria),
                ScreenFilter = DataTableToListOfObject(dtScreenFilter),
                ScreenHlp = DataTableToListOfObject(dtScreenHlp),
            };
            return result;
        }

        protected SerializableDictionary<string, List<SerializableDictionary<string, string>>> _GetScreeDdls(bool accessControlled)
        {
            int screenId = GetScreenId();
            byte systemId = GetSystemId();
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, accessControlled, false, false, true);
            Dictionary<string, DataRow> dtMenuAccess = GetScreenMenu(systemId, screenId);
            DataTable dtAuthRow = _GetAuthRow(screenId);
            DataTable dtAuthCol = _GetAuthCol(screenId);
            Dictionary<string, DataRow> authCol = dtAuthCol.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());

            var ddlContext = GetDdlContext();
            var Ddl = new SerializableDictionary<string, List<SerializableDictionary<string, string>>>();

            foreach (var x in ddlContext.Select((context)=>{
                if (!accessControlled || _AllowScreenColumnAccess(screenId, context.Key, "R", dtMenuAccess, dtAuthRow, authCol))
                {
                    bool bAll = true;
                    bool bAddNew = true;
                    bool bAutoComplete = context.Value.ContainsKey("autocomplete") && context.Value["autocomplete"] == "Y";
                    string keyId = "";
                    DataTable dt = (new AdminSystem()).GetDdl(screenId, context.Value["method"], bAddNew, bAll, bAutoComplete ? 50 : 0, keyId, LcAppConnString, LcAppPw, string.Empty, LImpr, LCurr);
                    return new KeyValuePair<string, List<SerializableDictionary<string, string>>>(context.Key, DataTableToListOfObject(dt));
                }
                else
                {
                    return new KeyValuePair<string, List<SerializableDictionary<string, string>>>(context.Key, new List<SerializableDictionary<string, string>>());
                }
            })) {
                Ddl[x.Key] = x.Value;
            }
            return Ddl;
        }
        protected bool _SetEffectiveScrCriteria(SerializableDictionary<string, string> desiredScreenCriteria)
        {
            var LastScreenCriteria = GetScreenCriteria(true);
            var dtScreenCriteria = _GetScrCriteria(GetScreenId());
            Func<SerializableDictionary<string, string>, SerializableDictionary<string, SerializableDictionary<string, string>>> fn = (d) =>
            {
                if (d == null) return null;
                SerializableDictionary<string, SerializableDictionary<string, string>> x = new SerializableDictionary<string, SerializableDictionary<string, string>>();
                foreach (var o in d)
                {
                    x[o.Key] = new SerializableDictionary<string, string>() { { "LastCriteria", o.Value } };
                }
                return x;
            };
            var filledScrCriteria = fn(desiredScreenCriteria);
            var currentScrCriteria = _GetCurrentScrCriteria(new DataView(dtScreenCriteria), filledScrCriteria ?? LastScreenCriteria.data.data, true);
            var effectiveScrCriteria = _SetCurrentScrCriteria(currentScrCriteria.Item1);
            bool validFilledScrCriteria = (filledScrCriteria.Where((o, i) => {
                return effectiveScrCriteria[i] == o.Value["LastCriteria"]; 
            }).Count() == filledScrCriteria.Count);
            return validFilledScrCriteria;
        }

        protected virtual DataTable _GetDocList(string mstId, string screenColumnName)
        {
            throw new NotImplementedException("Must implement _GetDocList");
        }
        protected virtual string _GetDocTableName(string screenColumnName)
        {
            throw new NotImplementedException("Must implement _GetDocTableName");
        }
        protected string ValidatedDdlValue(string columnName, Dictionary<string, string> refRecord, Dictionary<string, string> curr, Dictionary<string, string> refMst)
        {
            var ddlContext = GetDdlContext();
            if (ddlContext == null)
            {
                throw new Exception(string.Format("must define proper Ddl context {0} {1}", GetScreenId(), GetSystemId()));
            }
            else if (!ddlContext.ContainsKey(columnName)) {
                throw new Exception(string.Format("must define proper Ddl context for {0} {1} {2}", GetScreenId(), GetSystemId(), columnName));
            }
            var context = ddlContext[columnName];
            var val = curr.ContainsKey(columnName) ? curr[columnName] : (refRecord.ContainsKey(columnName) ? refRecord[columnName] : "");
            var refVal = context.ContainsKey("refCol") && (context["refColSrc"] == "Mst" ? refMst : curr).ContainsKey(context["refColSrcName"]) ? (context["refColSrc"] == "Mst" ? refMst : curr)[context["refColSrcName"]] : null;
            var x = ddlContext[columnName].Clone(new Dictionary<string, string>() { { "refColVal", refVal }, { "addNew", "N" } });
            var rec = ddlSuggests("**" + val, x, 1);
            return rec.data.Count > 0 ? rec.data[0]["key"] : "";
        }
        protected void ValidateField(DataRow drAuth, DataRow drLabel, Dictionary<string, string> refRecord, Dictionary<string,string> defRefRecord, ref SerializableDictionary<string, string> revisedRecord, SerializableDictionary<string, string> refMaster, ref List<KeyValuePair<string, string>> errors, SerializableDictionary<string, string> skipValidation)
        {
            string[] isDdlType = { "ComboBox", "DropDownList", "ListBox", "RadioButtonList" };
            string[] isSimpleTextType = { "TextBox", "MultiLine"};
            string[] isStringDataType = { "10","11","14","15","Char"};
            string colName = drLabel["ColumnName"].ToString() + drLabel["TableId"].ToString();
            string displayName = drLabel["DisplayName"].ToString();
            string displayMode = drLabel["DisplayMode"].ToString();
            // not sure when this will disappear FIXME
            string colLength = drLabel.Table.Columns.Contains("ColumnLength") ? drLabel["ColumnLength"].ToString() : "999999";
            string dataType = drLabel.Table.Columns.Contains("DataType") ? drLabel["DataType"].ToString() : "";
            
            if (displayMode == "Document") return;

            string oldVal = refRecord.ContainsKey(colName) ? refRecord[colName] : null;
            string val = revisedRecord.ContainsKey(colName) ? revisedRecord[colName] ?? "" : oldVal;
            string defaultValue = defRefRecord.ContainsKey(colName) ? defRefRecord[colName] : null;
            bool isMasterTable = drAuth["MasterTable"].ToString() == "Y";

            // comma delimited rules to skip
            string skippedValidation = skipValidation != null ? skipValidation[colName] ??  "" : "";
            string skipAllValidation = skipValidation != null ? skipValidation[(isMasterTable ? "SkipAllMst" : "SkipAllDtl")] ?? "" : "";
            if (
                ((drAuth["ColReadOnly"].ToString() == "Y" && !skippedValidation.Contains("ColReadOnly") && !skipAllValidation.Contains("ColReadOnly")) || (drAuth["ColVisible"].ToString() == "N") && !skippedValidation.Contains("ColVisible") && !skipAllValidation.Contains("ColVisible"))
                && (oldVal != val)
                )
            {
                /* this should either bounce or use refRecord value FIXME */
                val = oldVal;
                errors.Add(new KeyValuePair<string, string>(colName, "readonly value cannot be changed" + " " + drLabel["ColumnHeader"].ToString()));
            }

            if (isDdlType.Contains(displayName))
            {
                // this would empty out invalidate field selection
                val = ValidatedDdlValue(colName, refRecord, revisedRecord, drAuth["MasterTable"].ToString() == "Y" ? revisedRecord : refMaster);
            }
            else if (isSimpleTextType.Contains(displayMode) && isStringDataType.Contains(dataType))
            {
                // textbox, should be trimmed to avoid overflow error, but lack database size(ColumnLength but not in label definition change that FIXME!!!!!);
                int columnLength = 999999;
                bool hasColumnLength = int.TryParse(colLength, out columnLength);
                if (hasColumnLength && columnLength > 0 && !string.IsNullOrEmpty(val) && val.Trim().Length > columnLength && !skippedValidation.Contains("MaxLength"))
                {
                    // bounce
                    errors.Add(new KeyValuePair<string, string>(colName, "content length cannot exceed" + " " + columnLength.ToString()));
                }
                else
                {
                    // trim then silent cut off 
                    val = string.IsNullOrEmpty(val) ? val : (hasColumnLength ? val.Trim().Left(columnLength) : val.Trim());
                }
            }
            if (drLabel["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(val) && !skippedValidation.Contains("RequiredValid") && !skipAllValidation.Contains("RequiredValid"))
            {
                // supplied value assumed to be from user
                if (revisedRecord.ContainsKey(colName) || (string.IsNullOrEmpty(oldVal) && string.IsNullOrEmpty(defaultValue)))
                {
                    errors.Add(new KeyValuePair<string, string>(colName, drLabel["ErrMessage"].ToString() + " " + drLabel["ColumnHeader"].ToString()));
                }
                else val = string.IsNullOrEmpty(oldVal) ? defaultValue : oldVal; // nothing from user and there is existing, bypass check use existing as the value
            }
            if (!string.IsNullOrEmpty(drLabel["MaskValid"].ToString()) && !(new Regex(drLabel["MaskValid"].ToString())).IsMatch(val) && !skippedValidation.Contains("MaskValid"))
            {
                errors.Add(new KeyValuePair<string, string>(colName, drLabel["ErrMessage"].ToString() + " " + drLabel["ColumnHeader"].ToString()));
            }
            /* should include range check too but dtLabel doesn't have that info, needs to be expanded */

            revisedRecord[colName] = val;
        }
        protected List<KeyValuePair<string, string>> ValidateMst(ref SerializableDictionary<string, string> mst, SerializableDictionary<string, string> currentMst, SerializableDictionary<string, string> skipValidation)
        {
            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();

            var screenId = GetScreenId();
            DataTable dtAut = _GetAuthCol(screenId);
            DataTable dtLabel = _GetScreenLabel(screenId);
            var newMst = InitMaster();
            var revisedMst = mst.Clone();
            int ii = 0;

            foreach (DataRow drLabel in dtLabel.Rows)
            {
                DataRow drAuth = dtAut.Rows[ii];
                if (!string.IsNullOrEmpty(drLabel["TableId"].ToString()) && drAuth["MasterTable"].ToString() == "Y")
                {
                    string colName = drAuth["ColName"].ToString();
                    ValidateField(drAuth, drLabel, currentMst, InitMaster(), ref revisedMst, null, ref errors, skipValidation);
                }
                ii = ii + 1;
            }
            mst = revisedMst;
            return errors;
        }
        protected List<List<KeyValuePair<string, string>>> ValidateDtl(SerializableDictionary<string, string> mst, Dictionary<string, SerializableDictionary<string, string>> currDtlList, ref List<SerializableDictionary<string, string>> dtlList, string dtlKeyIdName, SerializableDictionary<string, string> skipValidation)
        {
            List<List<KeyValuePair<string, string>>> errors = new List<List<KeyValuePair<string, string>>>();
            List<SerializableDictionary<string, string>> validatedList = new List<SerializableDictionary<string, string>>();
            var screenId = GetScreenId();
            DataTable dtAut = _GetAuthCol(screenId);
            DataTable dtLabel = _GetScreenLabel(screenId);
            for (int ii = 0; ii < dtlList.Count; ii++)
            {
                var newDtl = InitDtl();
                var dtl = dtlList[ii];
                if (dtl["_mode"] == "delete" && !string.IsNullOrEmpty(dtl[dtlKeyIdName]))
                {
                    var revisedDtl = newDtl;
                    revisedDtl[dtlKeyIdName] = dtl[dtlKeyIdName];
                    revisedDtl["_mode"] = dtl["_mode"];
                    validatedList.Add(revisedDtl);
                }
                else if (string.IsNullOrEmpty(dtl[dtlKeyIdName]) || dtl["_mode"] != "delete")
                {
                    var currentDtl = string.IsNullOrEmpty(dtl[dtlKeyIdName]) || !currDtlList.ContainsKey(dtl[dtlKeyIdName]) ? newDtl : currDtlList[dtl[dtlKeyIdName]];
                    var revisedDtl = dtl.Clone();
                    var dtlErrors = new List<KeyValuePair<string, string>>();
                    int jj = 0;
                    // non-exist key, treat as new
                    if (!string.IsNullOrEmpty(dtl[dtlKeyIdName]) && !currDtlList.ContainsKey(dtl[dtlKeyIdName])) revisedDtl[dtl[dtlKeyIdName]] = null;
                    foreach (DataRow drLabel in dtLabel.Rows)
                    {
                        DataRow drAuth = dtAut.Rows[jj];
                        if (!string.IsNullOrEmpty(drLabel["TableId"].ToString()) && drAuth["MasterTable"].ToString() == "N")
                        {
                            string colName = drAuth["ColName"].ToString();
                            ValidateField(drAuth, drLabel, currentDtl,InitDtl(), ref revisedDtl, mst, ref dtlErrors, skipValidation);
                        }
                        jj = jj + 1;
                    }
                    if (dtlErrors.Count > 0)
                    {
                        errors.Add(dtlErrors);
                    }
                    validatedList.Add(revisedDtl);
                }
                else if (!string.IsNullOrEmpty(dtl[dtlKeyIdName]) && dtl["_mode"] == "delete")
                {
                    validatedList.Add(dtl);
                }
            }
            dtlList = validatedList;
            return errors;
        }
        protected Tuple<List<KeyValuePair<string, string>>, List<List<KeyValuePair<string, string>>>> ValidateInput(ref SerializableDictionary<string, string> mst, ref List<SerializableDictionary<string, string>> dtlList, DataTable dtMst, DataTable dtDtl, string mstKeyIdName, string dtlKeyIdName, SerializableDictionary<string, string> skipValidation)
        {
            var pid = mst[mstKeyIdName];
            var currMst = string.IsNullOrEmpty(pid) || dtMst.Rows.Count == 0 ? InitMaster() : DataTableToListOfObject(dtMst)[0];

            List<KeyValuePair<string, string>> mstError = ValidateMst(ref mst, currMst, skipValidation);
            List<List<KeyValuePair<string, string>>> dtlError = new List<List<KeyValuePair<string, string>>>();

            if (dtDtl != null)
            {
                var currDtlList = DataTableToListOfObject(dtDtl).ToDictionary(dr => dr[dtlKeyIdName].ToString(), dr => dr);
                dtlError = ValidateDtl(mst, currDtlList, ref dtlList, dtlKeyIdName, skipValidation);
            }

            return new Tuple<List<KeyValuePair<string, string>>, List<List<KeyValuePair<string, string>>>>(mstError, dtlError);
        }
        protected string TranslateItem(DataRowCollection rows, string key)
        {
            try
            {
                return rows.Find(key)[1].ToString();
            }
            catch { return "ERR!"; }
        }
        protected byte[] Encrypt(byte[] clearData, byte[] password, byte[] salt)
        {
            System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 1000);
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8); pdb.Reset(); aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            using (System.Security.Cryptography.ICryptoTransform enc = aes.CreateEncryptor())
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, enc, System.Security.Cryptography.CryptoStreamMode.Write))
            {
                cs.Write(clearData, 0, clearData.Length);
                cs.Close();
                ms.Close();
                return ms.ToArray();
            }
        }
        protected byte[] Decrypt(byte[] encryptedData, byte[] password, byte[] salt)
        {
            System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 1000);
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8); pdb.Reset(); aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            using (System.Security.Cryptography.ICryptoTransform enc = aes.CreateDecryptor())
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, enc, System.Security.Cryptography.CryptoStreamMode.Write))
            {
                cs.Write(encryptedData, 0, encryptedData.Length);
                cs.Close();
                ms.Close();
                return ms.ToArray();
            }
        }

        // Procedure for foreign currency rate from external source (do not call this repeatedly, use timer to break up the calls):
        protected string GetExtFxRate(string FrISOCurrencySymbol, string ToISOCurrencySymbol)
        {
            try
            {
                /* The following iGoogle Currency Converter has been retired by Google */
                /*
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                System.Text.RegularExpressions.Regex re = new Regex("^[0-9.]+\\s");
                string url = string.Format("http://www.google.com/ig/calculator?hl={0}&q=1{1}=?{2}", "en", FrISOCurrencySymbol, ToISOCurrencySymbol);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Referer = "http://www.checkmin.com";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
                string result = sr.ReadToEnd();
                System.Collections.Generic.Dictionary<string, string> o = jss.Deserialize<System.Collections.Generic.Dictionary<string, string>>(result);
                if (o["error"] == "0" || o["error"] == "")
                {
                    Match m = re.Match(o["rhs"]);
                    if (m.Success) { return m.Value.Trim(); }
                }
                */
                /* new google page crawling */
                /* the following is blocked/retired by google */
                /*
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                System.Text.RegularExpressions.Regex re = new Regex("^[0-9.]+\\s");
                string url = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", "1", FrISOCurrencySymbol, ToISOCurrencySymbol, Guid.NewGuid().ToString());
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Referer = "http://www.checkmin.com";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
                var rate = Regex.Matches(sr.ReadToEnd(),"<span class=\"?bld\"?>([0-9.]+)(.*)</span>")[0].Groups[1].Value;
                return rate.Trim().Replace(",", ".");*/

                var context = HttpContext.Current;
                var cache = context.Cache;
                bool isCrypto = FrISOCurrencySymbol == "FTX" || FrISOCurrencySymbol == "ETH" || ToISOCurrencySymbol == "FTX" || ToISOCurrencySymbol == "ETH";
                string cacheKey = "FXRate_" + FrISOCurrencySymbol + "_" + ToISOCurrencySymbol;
                int minutesToCache = isCrypto ? 15 : 60;
                string price = "";
                lock (cache)
                {
                    price = cache[cacheKey] as string;
                }

                if (!string.IsNullOrEmpty(price)) return price;

                var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/tools/price-conversion");
                var CmcAPIKey = System.Configuration.ConfigurationManager.AppSettings["CMCAPIKey"];

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["amount"] = "1";
                queryString["symbol"] = FrISOCurrencySymbol;
                queryString["convert"] = ToISOCurrencySymbol;

                URL.Query = queryString.ToString();

                var client = new WebClient();
                client.Headers.Add("X-CMC_PRO_API_KEY", CmcAPIKey);
                client.Headers.Add("Accepts", "application/json");
                string jsonString = client.DownloadString(URL.ToString());
                price = Newtonsoft.Json.Linq.JObject.Parse(jsonString).SelectToken("['data'].['quote'].['" + ToISOCurrencySymbol + "']['price']").ToString();

                lock (cache)
                {
                    if (cache[cacheKey] as string == null)
                    {
                        cache.Insert(cacheKey, price, new System.Web.Caching.CacheDependency(new string[] { }, new string[] {})
                            , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.Normal, null);
                    }
                }

                return price.ToString();
            }
            catch (Exception ex) {

                ErrorTracing(new Exception(string.Format("failed to get FX Rate ({0}) - ({1})", FrISOCurrencySymbol, ToISOCurrencySymbol),ex));
                throw ex;
                //return string.Empty; 
            }
        }

        /* helper functions for ASP.NET inline definition(like default value) conversion */
        protected string converDefaultValue(object val)
        {
            if (val is DateTime) return ((DateTime)val).ToString("o");
            else return val == null ? "" : val.ToString();
        }

        protected string convertDefaultValue(object val)
        {
            if (val is DateTime) return ((DateTime)val).ToString("o");
            else return val == null ? "" : val.ToString();
        }
        #region visible extern service endpoint
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAuthRow()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetAuthRow(GetScreenId());
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAuthCol()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dtAuthCol = _GetAuthCol(GetScreenId());
                DataTable dtLabel = _GetScreenLabel(GetScreenId());
                List<SerializableDictionary<string, string>> x = new List<SerializableDictionary<string, string>>();
                int ii = 0;
                foreach (DataRow dr in dtAuthCol.Rows)
                {
                    SerializableDictionary<string, string> rec = new SerializableDictionary<string, string>();
                    foreach (DataColumn col in dtAuthCol.Columns)
                    {
                        // normalize naming as AuthCol return '*Text' for dropdown items which is not the convention used in other places
                        rec[col.ColumnName] = col.ColumnName == "ColName" ? dtLabel.Rows[ii]["ColumnName"].ToString() + dtLabel.Rows[ii]["TableId"].ToString() : dr[col.ColumnName].ToString();
                    }
                    x.Add(rec);
                    ii = ii + 1;
                }
                ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                AutoCompleteResponse r = new AutoCompleteResponse();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                mr.errorMsg = "";
                r.data = x;
                r.query = "";
                r.topN = 999999;
                r.total = dtAuthCol.Rows.Count;
                mr.data = r;
                mr.status = "success";
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> GetScreenLabel()
        {
            Func<ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScreenLabel(GetScreenId());
                return DataTableToLabelResponse(dt, new List<string>() { "ColumnName", "TableId" });
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> SetScreenCriteria(SerializableDictionary<string, object> criteriaValues)
        {
            Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                List<KeyValuePair<string, string>> validationErrs = new List<KeyValuePair<string, string>>();

                DataView dvCri = new DataView(_GetScrCriteria(GetScreenId()));
                bool isCriVisible = true;
                DataSet ds = MakeScrCriteria(GetScreenId(), dvCri, criteriaValues, true, true);

                if (validationErrs.Count == 0)
                {
                    (new AdminSystem()).UpdScrCriteria(GetScreenId().ToString(), GetProgramName(), dvCri, LUser.UsrId, isCriVisible, ds, LcAppConnString, LcAppPw);
                    mr.errorMsg = "";

                    DataTable dtLastScrCriteria = _GetLastScrCriteria(GetScreenId(), 0, true);

                    for (int ii = 1; ii < dtLastScrCriteria.Rows.Count; ii++)
                    {
                        result.Add(dvCri[ii - 1]["ColumnName"].ToString(), dtLastScrCriteria.Rows[ii]["LastCriteria"].ToString());
                    }

                    mr.data = result;
                    mr.status = "success";
                }
                else
                {
                    mr.status = "failed";
                    mr.validationErrors = validationErrs;
                    return mr;
                }

                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> SetScreenFilter(string filterId)
        {
            Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                mr.errorMsg = "";
                mr.data = result;
                mr.status = "success";

                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetScreenTab()
        {
            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                DataTable dt = _GetScreenTab(GetScreenId());
                mr.errorMsg = "";
                mr.data = DataTableToListOfObject(dt); ;
                mr.status = "success";

                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> GetScreenHlp()
        {
            Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                DataTable dt = _GetScreenHlp(GetScreenId());
                mr.errorMsg = "";
                mr.data = DataTableToListOfObject(dt)[0]; ;
                mr.status = "success";

                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetMenu(byte systemId)
        {
            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
             
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
               DataTable dtMenu = _GetMenu(LCurr.SystemId, true);
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                mr.data = DataTableToListOfObject(dtMenu, false, null);
                mr.status = "success";
                mr.errorMsg = "";
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> Labels(string labelCat)
        {
            return GetLabels(labelCat);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetLabels(string labelCat)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetLabels(labelCat);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSystemLabels(string labelCat)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(3, LCurr.CompanyId, LCurr.ProjectId);
                DataTable dtS = _GetLabels("cSystem");
                dtS.Merge(_GetLabels("QFilter"));
                dtS.Merge(_GetLabels("cGrid"));
                return DataTableToApiResponse(dtS, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, 3, 0, "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenButtonHlp()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScreenButtonHlp(GetScreenId());
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> GetScreenCriteria(bool refresh)
        {
            Func<ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScrCriteria(GetScreenId());
                if (!dt.Columns.Contains("LastCriteria"))
                {
                    dt.Columns.Add("LastCriteria", typeof(string));
                }
                DataTable dtLastScrCriteria = _GetLastScrCriteria(GetScreenId(), 0, refresh);
                //skip first row in last criteria
                for (int ii = 1; ii < dtLastScrCriteria.Rows.Count && (ii-1) < dt.Rows.Count; ii++)
                {
                    dt.Rows[ii - 1]["LastCriteria"] = dtLastScrCriteria.Rows[ii]["LastCriteria"];
                }
                return DataTableToLabelResponse(dt, new List<string>() { "ColumnName" });
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> GetScreenCriteria()
        {

            return GetScreenCriteria(false);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenCriteriaDef()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScrCriteria(GetScreenId());
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenCriteriaDdlList(string screenCriId, string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScrCriteria(GetScreenId());
                var drv = (from r in dt.AsEnumerable() where r.Field<int?>("ScreenCriId").ToString() == screenCriId select r).FirstOrDefault();
                if (drv == null)
                {
                    throw new Exception(string.Format("Criteria Id {0} not found", screenCriId));
                }
                else if (drv["DisplayMode"].ToString() == "AutoComplete")
                {
                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    context["method"] = "GetScreenIn";
                    context["addnew"] = "Y";
                    context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + GetSystemId().ToString() + "C" + screenCriId;
                    context["requiredValid"] = drv["RequiredValid"].ToString();
                    context["mKey"] = drv["DdlKeyColumnName"].ToString();
                    context["mVal"] = drv["DdlRefColumnName"].ToString();
                    context["mTip"] = drv["DdlRefColumnName"].ToString();
                    context["mImg"] = drv["DdlRefColumnName"].ToString();
                    context["ssd"] = "";
                    context["scr"] = GetScreenId().ToString();
                    context["csy"] = GetSystemId().ToString();
                    context["filter"] = "0";
                    context["isSys"] = "N";
                    context["conn"] = LcSysConnString;
                    context["refColCID"] = "";
                    context["refCol"] = drv["DdlFtrColumnName"].ToString();
                    context["refColDataType"] = drv["DdlFtrDataType"].ToString();
                    context["refColVal"] = filterBy;
                    ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.status = "success";
                    mr.errorMsg = "";
                    mr.data = ddlSuggests(query, context, topN);
                    return mr;
                }
                else
                {
                    return DataTableToApiResponse((new AdminSystem()).GetScreenIn(GetScreenId().ToString(), "GetDdl" + drv["ColumnName"].ToString() + GetSystemId().ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, LImpr, LCurr, LcAppConnString, LcAppPw), "", 0);
                }
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenFilter()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScreenFilter(GetScreenId());
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null), AllowAnonymous());
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetColumnContent(string mstId, string dtlId, string columnName, bool isMaster, string screenColumnName)
        {

            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                DataTable dtAut = _GetAuthCol(GetScreenId());
                DataTable dtLabel = _GetScreenLabel(GetScreenId());
                string keyColumName = isMaster ? GetMstKeyColumnName(true) : GetDtlKeyColumnName(true);
                string tableName = isMaster ? GetMstTableName(true) : GetDtlTableName(true);
                int ii = 0;
                DataTable dt = null;
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                if (string.IsNullOrEmpty(mstId) || (string.IsNullOrEmpty(dtlId) && !isMaster))
                {
                    mr.data = null;
                    mr.status = "failed";
                    mr.errorMsg = "invalid key";
                    return mr;
                }

                foreach (DataRow drLabel in dtLabel.Rows)
                {
                    DataRow drAuth = dtAut.Rows[ii];
                    if ("HyperLink,ImageButton".IndexOf(drLabel["DisplayName"].ToString()) >= 0
                        && !string.IsNullOrEmpty(drLabel["TableId"].ToString())
                        && drAuth["MasterTable"].ToString() == (isMaster ? "Y" : "N")
                        && (drLabel["ColumnName"].ToString() + drLabel["TableId"].ToString()) == screenColumnName)
                    {
                        /* both are technically wrong as there is no info for the underlying tablecolumnname in GetScreenLabel or GetAuthCol 
                         * should add that info in one of them. we are just assuming the same imagebutton field would not appear more than once in a screen
                         * and use drLabel's version
                         */
                        string colName = drAuth["ColName"].ToString();
                        string tableColumnName = drLabel["ColumnName"].ToString();
                        dt = (new AdminSystem()).GetDbImg(isMaster ? mstId : dtlId, tableName, keyColumName, tableColumnName, LcAppConnString, LcAppPw);
                        mr.data = DataTableToListOfObject(dt, IncludeBLOB.Content);
                        mr.status = "success";
                        mr.errorMsg = "";
                        return mr;
                    }
                    ii = ii + 1;
                }
                mr.data = null;
                mr.status = "failed";
                mr.errorMsg = "access denied";
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", screenColumnName, emptyListResponse), AllowAnonymous());
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> AddDocColumnContent(string mstId, string dtlId, bool isMaster, string screenColumnName, string docJson, SerializableDictionary<string, string> options)
        {

            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                DataTable dtAut = _GetAuthCol(GetScreenId());
                DataTable dtLabel = _GetScreenLabel(GetScreenId());
                List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
                int ii = 0;
                foreach (DataRow drLabel in dtLabel.Rows)
                {
                    DataRow drAuth = dtAut.Rows[ii];
                    if ("HyperLink,ImageButton".IndexOf(drLabel["DisplayName"].ToString()) >= 0
                        && !string.IsNullOrEmpty(drLabel["TableId"].ToString())
                        && drAuth["MasterTable"].ToString() == (isMaster ? "Y" : "N")
                        && (drLabel["ColumnName"].ToString() + drLabel["TableId"].ToString()) == screenColumnName)
                    {
                        /* both are technically wrong as there is no info for the underlying tablecolumnname in GetScreenLabel or GetAuthCol 
                         * should add that info in one of them. we are just assuming the same imagebutton field would not appear more than once in a screen
                         * and use drLabel's version
                         */
                        string colName = drAuth["ColName"].ToString();
                        string tableColumnName = drLabel["ColumnName"].ToString();
                        SerializableDictionary<string, string> currentMst = new SerializableDictionary<string, string>();
                        SerializableDictionary<string, string> revisedMst = new SerializableDictionary<string, string>() { { screenColumnName, docJson } };
                        ValidateField(drAuth, drLabel, currentMst, InitMaster(), ref revisedMst, null, ref errors, null);
                        if (errors.Count == 0)
                        {
                            List<_ReactFileUploadObj> savedObj = AddDoc(docJson, isMaster ? mstId : dtlId, isMaster ? GetMstTableName(true) : GetDtlTableName(true), isMaster ? GetMstKeyColumnName(true) : GetDtlKeyColumnName(true), tableColumnName, options.ContainsKey("resizeImage"));
                            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                            jss.MaxJsonLength = Int32.MaxValue;
                            ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                            SaveDataResponse result = new SaveDataResponse()
                            {
                                mst = isMaster && savedObj != null ? new SerializableDictionary<string, string>() { { "fileObject", jss.Serialize(savedObj) } } : null
                                ,
                                dtl = !isMaster && savedObj != null ? new List<SerializableDictionary<string, string>>() {
                                    new SerializableDictionary<string, string>() {{"fileObject", jss.Serialize(savedObj)}}
                                } : null
                            };
                            string msg = "image updated";
                            result.message = msg;
                            mr.status = "success";
                            mr.errorMsg = "";
                            mr.data = result;
                            return mr;
                        }
                        break;
                    }
                    ii = ii + 1;
                }

                return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                {
                    status = "failed",
                    errorMsg = "content invalid " + string.Join(" ", errors).ToArray(),
                    validationErrors = errors,
                };


            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "S", screenColumnName));
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetDoc(string mstId, string dtlId, bool isMaster, string docId, string screenColumnName)
        {
            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                DataTable dtDocList = _GetDocList(isMaster ? mstId : dtlId, screenColumnName);
                bool hasDoc = (from x in dtDocList.AsEnumerable() where !string.IsNullOrEmpty(docId) && x["DocId"].ToString() == docId select x).Count() > 0;
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();

                if (hasDoc)
                {
                    DataTable dt = (new AdminSystem()).GetDbDoc(docId, _GetDocTableName(screenColumnName), LcAppConnString, LcAppPw);
                    mr.data = DataTableToListOfObject(dt, IncludeBLOB.Content);
                    mr.status = "success";
                    mr.errorMsg = "";
                    return mr;
                }
                else
                {
                    return new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>()
                    {
                        data = null,
                        status = "failed",
                        errorMsg = "access denied"
                    };
                }
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", screenColumnName, emptyListResponse), AllowAnonymous());
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDocList(string mstId, string dtlId, bool isMaster, string screenColumnName)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(new DataView(_GetScrCriteria(GetScreenId())), null));
                DataTable dt = _GetDocList(isMaster ? mstId : dtlId, screenColumnName);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", screenColumnName, emptyAutoCompleteResponse), AllowAnonymous());
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenMetaData(SerializableDictionary<string, string> options)
        {
            int screenId = GetScreenId();
            byte systemId = GetSystemId(); 

            Func<ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {

                LoadScreenPageResponse result = _GetScreenMetaData(!AllowAnonymous());
                ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null), AllowAnonymous());
            return ret;

        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenDdls()
        {
            int screenId = GetScreenId();
            byte systemId = GetSystemId(); 

            Func<ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                LoadScreenPageResponse result = new LoadScreenPageResponse()
                {
                    Ddl = _GetScreeDdls(!AllowAnonymous()),
                };

                ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null), AllowAnonymous());
            return ret;
        }
        #endregion
    }


}