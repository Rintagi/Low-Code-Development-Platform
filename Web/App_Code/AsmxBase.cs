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
using System.Xml.Serialization;
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
using System.Web.Configuration;
using System.Configuration;


namespace RO.Web
{

    [XmlRoot("SerializableDictictory")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public static SerializableDictionary<TKey, TValue> CreateInstance(Dictionary<TKey, TValue> d)
        {
            var x = new SerializableDictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> v in d)
            {
                x.Add(v.Key, v.Value);
            }
            return x;
        }
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public SerializableDictionary<TKey, TValue> Clone(Dictionary<TKey, TValue> mergeWith = null)
        {
            var x = CreateInstance(this);
            if (mergeWith != null) mergeWith.ToList().ForEach(v => x[v.Key] = v.Value);
            return x;
        }

        protected virtual TValue GetValue(TKey key)
        {
            TValue x = base.TryGetValue(key, out x) ? x : default(TValue); return x;
        }
        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { base[key] = value; }
        }

        #endregion

    }

    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public abstract class AsmxBase : WebService
    {
        public class RintagiLoginToken
        {
            public int UsrId { get; set; }
            public string LoginName { get; set; }
            public string UsrName { get; set; }
            public string UsrEmail { get; set; }
            public string UsrGroup { get; set; }
            public string RowAuthority { get; set; }
            public int CompanyId { get; set; }
            public int ProjectId { get; set; }
            public byte SystemId { get; set; }
            public int DefCompanyId { get; set; }
            public int DefProjectId { get; set; }
            public byte DefSystemId { get; set; }
            public byte DbId { get; set; }
            public string Resources { get; set; }
        }

        public class RintagiLoginJWT
        {
            public string loginId;
            public string loginToken;
            public int iat;
            public int exp;
            public int nbf;
            public string handle { get; set; }
        }

        private string SystemListCacheWatchFile { get { return Server.MapPath("~/RefreshSystemList.txt"); } }

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

        protected UsrImpr LImpr { get; set; }
        protected UsrCurr LCurr { get; set; }
        protected LoginUsr LUser { get; set; }
        protected byte LcSystemId { get; set; }
        protected string LcSysConnString { get; set; }
        protected string LcAppConnString { get; set; }
        protected string LcAppDb { get; set; }
        protected string LcDesDb { get; set; }
        protected string LcAppPw { get; set; }
        protected CurrPrj CPrj { get; set; }
        protected CurrSrc CSrc { get; set; }
        protected CurrTar CTar { get; set; }

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
        protected abstract DataTable _GetDtlById(string pid, int screenFilterId);
        protected int ShortCacheDuration = 0;

        public class FileUploadObj
        {
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string base64;
        }
        public class FileInStreamObj
        {
            public string fileName;
            public string mimeType;
            public Int64 lastModified;
            public string ver;
            public int extensionSize;
        }

        public class AutoCompleteResponse
        {
            public string query;
            public List<SerializableDictionary<string, string>> data;
            public int total;
            public int topN;
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
            public List<SerializableDictionary<string, string>> dtl;
            public string message;
        }
        public class ApiResponse<T, S>
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

        public static bool VerifyHS256JWT(string header, string payload, string base64UrlEncodeSig, string secret)
        {
            Func<byte[], string> base64UrlEncode = (c) => Convert.ToBase64String(c).TrimEnd(new char[] { '=' }).Replace('_', '/').Replace('-', '+');
            HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(secret));
            byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(header + "." + payload));
            return base64UrlEncodeSig == base64UrlEncode(hash);
        }

        protected string MasterKey
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
                    // delay brute force attack, 100K round(ethereum keystore use 260K round, 100K round requires about 5 sec as of 2018/6 hardware)
                    //, we only do this once and stored in class variable so there is a 5 sec delay when app started for API usage
                    Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(jwtMasterKey, UTF8Encoding.UTF8.GetBytes(jwtMasterKey), 100000);
                    _masterkey = (new AdminSystem()).EncryptString(Convert.ToBase64String(k.GetBytes(32)));
                }
                return _masterkey;
            }
        }
        protected string GetSessionEncryptionKey(string time, string usrId)
        {
            System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
            return (new AdminSystem()).EncryptString(Convert.ToBase64String(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(MasterKey + usrId + time))));
        }
        protected string GetSessionSigningKey(string time, string usrId)
        {
            var key = GetSessionEncryptionKey(time, usrId);
            return Convert.ToBase64String(new Rfc2898DeriveBytes(key, UTF8Encoding.UTF8.GetBytes(key), 1).GetBytes(32));
        }

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
            Func<byte[], string> base64UrlEncode = (c) => Convert.ToBase64String(c).TrimEnd(new char[] { '=' }).Replace('_', '/').Replace('-', '+');
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now.ToUniversalTime();
            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddSeconds(validSeconds).Subtract(utc0).TotalSeconds; // Expiration time is up to 1 hour, but lets play on safe side
            var encryptionKey = GetSessionEncryptionKey(iat.ToString(), usr.UsrId.ToString());
            var signingKey = GetSessionSigningKey(iat.ToString(), usr.UsrId.ToString());
            RintagiLoginJWT token = new RintagiLoginJWT()
            {
                iat = iat,
                exp = exp,
                nbf = iat,
                loginToken = CreateEncryptedLoginToken(usr, defCompanyId, defProjectId, defSystemId, curr, impr, resources, encryptionKey),
                loginId = usr.UsrId.ToString(),
                handle = guidHandle
            };
            string payLoad = new JavaScriptSerializer().Serialize(token);
            string header = "{\"typ\":\"JWT\",\"alg\":\"HS256\"}";
            HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(signingKey));
            string content = base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(header)) + "." + base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(payLoad));
            byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(content));
            return content + "." + base64UrlEncode(hash);
        }
        protected RintagiLoginJWT GetLoginUsrInfo(string jwt)
        {
            string[] x = (jwt ?? "").Split(new char[] { '.' });
            Func<string, byte[]> base64UrlDecode = s => Convert.FromBase64String(s.Replace('-', '+').Replace('_', '/') + (s.Length % 4 > 1 ? new string('=', 4 - s.Length % 4) : ""));
            if (x.Length >= 3)
            {
                try
                {
                    Dictionary<string, string> header = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[0])));
                    try
                    {
                        RintagiLoginJWT loginJWT = new JavaScriptSerializer().Deserialize<RintagiLoginJWT>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[1])));
                        string signingKey = GetSessionSigningKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
                        bool valid = header["typ"] == "JWT" && header["alg"] == "HS256" && VerifyHS256JWT(x[0], x[1], x[2], signingKey);
                        if (valid)
                        {
                            return loginJWT;
                        }
                        else return null;
                    }
                    catch
                    {
                        return null;
                    }
                }
                catch { return null; }
            }
            else return null;

        }
        protected Dictionary<byte, Dictionary<string, string>> GetSystemsDict()
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            //int cacheMinutes = 30;
            int cacheMinutes = ShortCacheDuration;
            Dictionary<byte, Dictionary<string, string>> sysDict = cache[KEY_SystemsDict] as Dictionary<byte, Dictionary<string, string>>;

            if (sysDict == null)
            {
                sysDict = new Dictionary<byte, Dictionary<string, string>>();
                DataTable dt = LoadSystemsList();
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
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict[KEY_SysConnectStr] = Config.GetConnStr(dr["dbAppProvider"].ToString(), singleSQLCredential ? Config.DesServer : dr["ServerName"].ToString(), dr["dbDesDatabase"].ToString(), "", singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString());
                    dict[KEY_AppConnectStr] = Config.GetConnStr(dr["dbAppProvider"].ToString(), singleSQLCredential ? Config.DesServer : dr["ServerName"].ToString(), dr["dbAppDatabase"].ToString(), "", singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString());
                    dict[KEY_SystemAbbr] = dr["SystemAbbr"].ToString();
                    dict[KEY_DesDb] = dr["dbDesDatabase"].ToString();
                    dict[KEY_AppDb] = dr["dbAppDatabase"].ToString();
                    dict[KEY_AppUsrId] = singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString();
                    dict[KEY_AppPwd] = singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString();
                    try { dict[KEY_SysAdminEmail] = dr["AdminEmail"].ToString(); }
                    catch { dict[KEY_SysAdminEmail] = string.Empty; }
                    try { dict[KEY_SysAdminPhone] = dr["AdminPhone"].ToString(); }
                    catch { dict[KEY_SysAdminPhone] = string.Empty; }
                    try { dict[KEY_SysCustServEmail] = dr["CustServEmail"].ToString(); }
                    catch { dict[KEY_SysCustServEmail] = string.Empty; }
                    try { dict[KEY_SysCustServPhone] = dr["CustServPhone"].ToString(); }
                    catch { dict[KEY_SysCustServPhone] = string.Empty; }
                    try { dict[KEY_SysCustServFax] = dr["CustServFax"].ToString(); }
                    catch { dict[KEY_SysCustServFax] = string.Empty; }
                    try { dict[KEY_SysWebAddress] = dr["WebAddress"].ToString(); }
                    catch { dict[KEY_SysWebAddress] = string.Empty; }
                    sysDict[byte.Parse(dr["SystemId"].ToString())] = dict;
                }
                cache.Add(KEY_SystemsDict, sysDict, new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
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
            try { return GetSystemsDict()[SystemId][KEY_SystemsDict]; }
            catch { return string.Empty; }
        }

        protected DataTable LoadSystemsList()
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            //int cacheMinutes = 30;
            int cacheMinutes = ShortCacheDuration;

            DataTable dt = cache[KEY_SystemsList] as DataTable;

            if (dt == null)
            {
                dt = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
                cache.Add(KEY_SystemsList, dt, new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dt;
        }
        protected DataTable LoadEntityList()
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            //int cacheMinutes = 30;
            int cacheMinutes = ShortCacheDuration;

            DataTable dt = cache[KEY_EntityList] as DataTable;
            if (dt == null)
            {
                dt = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
                cache.Add(KEY_EntityList, dt, new System.Web.Caching.CacheDependency(SystemListCacheWatchFile)
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, cacheMinutes, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dt;
        }

        protected DataTable _GetCompanyList(bool ignoreCache = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = KEY_CompanyList + "_" + LUser.UsrId.ToString();
            int minutesToCache = ShortCacheDuration;
            DataTable dtCompanyList = cache[cacheKey] as DataTable;
            if (dtCompanyList == null || ignoreCache)
            {
                dtCompanyList = (new LoginSystem()).GetCompanyList(LImpr.Usrs, LImpr.RowAuthoritys, LImpr.Companys == "0" ? LImpr.Companys : LImpr.Companys);
                cache.Insert(cacheKey, dtCompanyList, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtCompanyList;
        }
        protected DataTable _GetProjectList(int companyId, bool ignoreCache = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = KEY_ProjectList + "_" + companyId.ToString() + "_" + LUser.UsrId.ToString();
            int minutesToCache = ShortCacheDuration;
            DataTable dtProjectList = cache[cacheKey] as DataTable;
            if (dtProjectList == null || ignoreCache)
            {
                dtProjectList = (new LoginSystem()).GetProjectList(LImpr.Usrs, LImpr.RowAuthoritys, LImpr.Projects, companyId.ToString());
                cache.Insert(cacheKey, dtProjectList, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtProjectList;
        }

        protected UsrImpr LoadUsrImpr(int usrId, byte systemId, int companyId, int projectId)
        {

            string imprCacheKey = string.Format("{0}_{1}_{2}_{3}_{4}", KEY_CacheLImpr, usrId, systemId, companyId, projectId);
            var context = HttpContext.Current;
            var cache = context.Cache;
            UsrImpr impr = cache[imprCacheKey] as UsrImpr;
            if (impr == null)
            {
                //int cacheMinutes = 1; // cache for 1 minute to avoid frequent DB retrieve for rapid firing API calls
                int cacheMinutes = ShortCacheDuration;
                impr = SetImpersonation(null, usrId, systemId, companyId, projectId);
                cache.Add(imprCacheKey, impr, new System.Web.Caching.CacheDependency(new string[] { SystemListCacheWatchFile }, new string[] { })
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

        protected void SwitchContext(byte sysId, int companyId, int projectId, bool checkSysId = true, bool checkCompanyId = true, bool checkProjectId = true)
        {
            if (LcSystemId != sysId || (LCurr == null || LCurr.CompanyId != companyId || LCurr.ProjectId != projectId))
            {
                LcSystemId = sysId;
                LCurr.CompanyId = companyId;
                LCurr.ProjectId = projectId;
                LCurr.SystemId = sysId;
                LImpr = LoadUsrImpr(LUser.UsrId, sysId, companyId, projectId);
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

        protected Dictionary<string, object> LoadUserSession()
        {
            try
            {
                var context = HttpContext.Current;
                var cache = context.Cache;
                var auth = (HttpContext.Current.Request.Headers["Authorization"] ?? HttpContext.Current.Request.Headers["X-Authorization"]) as string;
                var scope = HttpContext.Current.Request.Headers["X-RintagiScope"] as string;
                byte? systemId = null;
                int? companyId = null;
                int? projectId = null;
                short? cultureId = null;
                if (!string.IsNullOrEmpty(scope))
                {

                    var x = (scope ?? "").Split(new char[] { ',' });
                    try { systemId = byte.Parse(x[0]); }
                    catch { };
                    try { companyId = int.Parse(x[1]); }
                    catch { };
                    try { projectId = int.Parse(x[2]); }
                    catch { };
                    try { cultureId = short.Parse(x[3]); }
                    catch { };
                }
                if (false && Session != null && Session[KEY_CacheLUser] != null)
                {
                    Dictionary<string, object> userSession = new Dictionary<string, object>() { { "LUser", Session[KEY_CacheLUser] }, { "LCurr", Session[KEY_CacheLCurr] }, { "LImpr", Session[KEY_CacheLImpr] } };
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
                        RintagiLoginJWT token = GetLoginUsrInfo(x[1]);
                        var handle = token.handle;
                        var userSession = cache[handle] as Dictionary<string, object>;
                        var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        var now = DateTime.Now.ToUniversalTime().Subtract(utc0).TotalSeconds;
                        var minutesToCache = ShortCacheDuration;
                        var sessionEncryptionKey = GetSessionEncryptionKey(token.iat.ToString(), token.loginId.ToString());
                        if (now < token.exp)
                        {
                            RintagiLoginToken loginToken = DecryptLoginToken(token.loginToken, sessionEncryptionKey);
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
            catch (Exception e)
            {
                if (e.Message == "access_denied") throw;
            }
            return null;
        }
        protected ApiResponse<T, S> ProtectedCall<T, S>(Func<ApiResponse<T, S>> apiCallFn, bool allowAnonymous = false)
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

        protected ApiResponse<T, S> ManagedApiCall<T, S>(Func<ApiResponse<T, S>> apiCallFn)
        {
            try
            {
                return apiCallFn();
            }
            catch (Exception e)
            {
                return new ApiResponse<T, S>() { errorMsg = e.Message, status = "failed" };
            }
        }

        protected Func<ApiResponse<T, S>> RestrictedApiCall<T, S>(Func<ApiResponse<T, S>> apiCallFn, byte systemId, int screenId, string action, string columnName, Func<ApiResponse<T, S>> OnErrorResponse = null)
        {
            Func<ApiResponse<T, S>> fn = () =>
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
                        ((dtMenuAccess != null && !dtMenuAccess.ContainsKey(screenId.ToString()))
                        || dtMenuAccess == null
                        || dtAuthRow.Rows.Count == 0
                        || (dtAuthRow.Rows[0]["ViewOnly"].ToString() == "Y" && (action == "S" || action == "D"))
                        || (dtAuthRow.Rows[0]["AllowAdd"].ToString() == "N" && dtAuthRow.Rows[0]["AllowUpd"].ToString() == "N" && action == "S")
                        || (dtAuthRow.Rows[0]["AllowDel"].ToString() == "N" && action == "D")
                        ))
                    {
                        return errRetFn();
                        ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        Context.Response.StatusCode = 401;
                        Context.Response.TrySkipIisCustomErrors = true;
                        mr.status = "access_denied";
                        mr.errorMsg = "access denied";
                        return mr;
                    }
                    else if (!string.IsNullOrEmpty(columnName) &&
                            ((authCol.ContainsKey(columnName) && (authCol[columnName]["ColVisible"] == "N")) || (!authCol.ContainsKey(columnName) && !authCol.ContainsKey(columnName + "Text"))))
                    {
                        return errRetFn();

                        ApiResponse<T, S> mr = new ApiResponse<T, S>();
                        Context.Response.StatusCode = 401;
                        Context.Response.TrySkipIisCustomErrors = true;
                        mr.status = "access_denied";
                        mr.errorMsg = "access denied";
                        return mr;
                    }
                }
                return apiCallFn();
            };

            return fn;

        }
        protected DataTable _GetLabels(string labelCat)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_SystemLabels_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + labelCat.ToString();
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_SystemLabel_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + labelCat.ToString() + "_" + labelKey;
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_ScreenButtonHlp_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            DataTable dtButtonHlp = cache[cacheKey] as DataTable;
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_ScrCriteria_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
            DataTable dtScreenCriteria = cache[cacheKey] as DataTable;
            if (dtScreenCriteria == null)
            {
                dtScreenCriteria = (new AdminSystem()).GetScrCriteria(screenId.ToString(), LcSysConnString, LcAppPw);
                cache.Insert(cacheKey, dtScreenCriteria, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenCriteria;
        }

        protected DataTable _GetScreenCriHlp(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenCriHlp_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
            DataTable dtCriHlp = cache[cacheKey] as DataTable;
            if (dtCriHlp == null)
            {
                dtCriHlp = (new AdminSystem()).GetScreenCriHlp(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtCriHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtCriHlp;
        }

        protected DataTable _GetScreenIn(int screenId, DataRowView drv)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenIn_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString() + "_" + drv["ScreenCriId"].ToString();
            int minutesToCache = 0;
            DataTable dtScreenIn = cache[cacheKey] as DataTable;
            if (dtScreenIn == null)
            {
                dtScreenIn = (new AdminSystem()).GetScreenIn(screenId.ToString(), "GetDdl" + drv["ColumnName"].ToString() + GetSystemId() + "C" + drv["ScreenCriId"].ToString(), 0, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, LImpr, LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtScreenIn, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenIn;
        }

        protected DataTable _GetScreenTab(int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenTab_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;

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
            string cacheKey = loginHandle + "_ScreenHelp_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;

            DataTable dtScreenHlp = cache[cacheKey] as DataTable;
            if (dtScreenHlp == null)
            {
                dtScreenHlp = (new AdminSystem()).GetScreenHlp(screenId, LUser.CultureId, LcSysConnString, LcAppPw);
                cache.Add(cacheKey, dtScreenHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtScreenHlp;
        }

        protected DataTable _GetLastCriteria(int screenId, int rowExpected, bool refresh = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenLastCriteria_" + LCurr.SystemId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
            DataTable dtLastCriteria = cache[cacheKey] as DataTable;
            if (dtLastCriteria == null || refresh)
            {
                dtLastCriteria = (new AdminSystem()).GetLastCriteria(rowExpected, screenId, 0, LUser.UsrId, LcSysConnString, LcAppPw);
                cache.Insert(cacheKey, dtLastCriteria, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                    , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return dtLastCriteria;
        }
        protected DataTable _GetScreenFilter(int screenId, bool refresh = false)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenFilter_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_ScreenAutRow_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_ScreenAutCol_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
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
            string cacheKey = loginHandle + "_ScreenLabel_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + screenId.ToString();
            int minutesToCache = ShortCacheDuration;
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
            int minutesToCache = ShortCacheDuration;
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
            if (dr["DataTypeSysName"].ToString() == "DateTime") { return string.IsNullOrEmpty(val) ? new DateTime().ToString() : val; }
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

        protected string ValidatedCriVal(int screenId, DataRowView drv, string val)
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
                DataTable dtScreenIn = _GetScreenIn(screenId, drv);
                var dictAllowedChoice = dtScreenIn.AsEnumerable().ToDictionary(dr => dr[drv["DdlKeyColumnName"].ToString()].ToString());
                int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn(screenId.ToString(), "GetDdl" + drv["ColumnName"].ToString() + GetSystemId() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, LImpr, LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : LcSysConnString, LcAppPw)).Count;
                var selectedVals = (val ?? "").Split(new char[] { ',' });
                var matchedVals = string.Join(",",
                    selectedVals
                    .Where(v => { try { return !string.IsNullOrEmpty(v) && (dictAllowedChoice.ContainsKey(v) || v == "0" || v == "'0'"); } catch { return false; } })
                    .Select(v => drv["DisplayName"].ToString() == "ListBox" ? string.Format(v.Contains("'") ? "{0}" : "'{0}'", v) : v)
                    .ToList());
                bool noneSelected = string.IsNullOrEmpty(matchedVals) || matchedVals == "''" || string.IsNullOrEmpty(val);
                if (drv["DisplayName"].ToString() == "ListBox")
                {
                    return noneSelected && CriCnt + 1 > TotalChoiceCnt ? "'-1'" : (string.IsNullOrEmpty(val) ? null : "(" + val + ")");
                }
                else return matchedVals;
            }
            else if (",DateUTC,DateTimeUTC,ShortDateTimeUTC,LongDateTimeUTC,".IndexOf("," + drv["DisplayMode"].ToString() + ",") >= 0)
            {
                return val;
            }
            else if (drv["DisplayName"].ToString().Contains("Date"))
            {
                return val;
            }
            else return val;
        }

        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, List<string> lastScrCri, bool isSave = false)
        {
            DataSet ds = new DataSet();
            DataTable dtScreenCriHlp = _GetScreenCriHlp(screenId);

            ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn"), dvCri));
            int ii = 0;
            DataRow dr = ds.Tables["DtScreenIn"].NewRow();
            DataRowCollection drcScreenCriHlp = dtScreenCriHlp.Rows;
            foreach (DataRowView drv in dvCri)
            {
                string val = ValidatedCriVal(screenId, drv, lastScrCri.Count > ii ? lastScrCri[ii] : null);

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
        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, SerializableDictionary<string, object> lastScrCri, bool isSave = false)
        {
            var x = dvCri.Table.AsEnumerable().Select(dr => lastScrCri.ContainsKey(dr["ColumnName"].ToString()) ? lastScrCri[dr["ColumnName"].ToString()].ToString() : null).ToList();
            return MakeScrCriteria(screenId, dvCri, x, isSave);
        }

        protected DataSet MakeScrCriteria(int screenId, DataView dvCri, DataTable dtLastScrCri)
        {
            return MakeScrCriteria(screenId, dvCri, dtLastScrCri.AsEnumerable().Skip(1).Select(dr => dr["LastCriteria"].ToString()).ToList<string>());
        }

        protected DataView GetCriCache(int systemId, int screenId)
        {
            return _GetScrCriteria(screenId).DefaultView;
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

            int nHeight = maxHeight; // This is 36x10 line:7700 GenScreen
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

        protected void AddDoc(string docJson, string docId, string tableName, string keyColumnName, string columnName, bool resizeToIcon = false)
        {
            byte[] storedContent = null;
            bool dummyImage = false;
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                FileUploadObj fileObj = jss.Deserialize<FileUploadObj>(docJson);
                if (!string.IsNullOrEmpty(fileObj.base64))
                {
                    byte[] content = Convert.FromBase64String(fileObj.base64);
                    dummyImage = fileObj.base64 == "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=";
                    if (resizeToIcon && fileObj.base64.Length > 0 && fileObj.mimeType.StartsWith("image/"))
                    {
                        try
                        {
                            content = ResizeImage(Convert.FromBase64String(fileObj.base64));
                        }
                        catch
                        {
                        }
                    }
                    /* store as 256 byte UTF8 json header + actual binary file content 
                     * if header info > 256 bytes use compact header(256 bytes) + actual header + actual binary file content
                     */
                    string contentHeader = jss.Serialize(new FileInStreamObj() { fileName = fileObj.fileName, lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", extensionSize = 0 });
                    byte[] streamHeader = Enumerable.Repeat((byte)0x20, 256).ToArray();
                    int headerLength = System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader).Length;
                    string compactHeader = jss.Serialize(new FileInStreamObj() { fileName = "", lastModified = fileObj.lastModified, mimeType = fileObj.mimeType, ver = "0100", extensionSize = headerLength });
                    int compactHeaderLength = System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader).Length;
                    if (headerLength <= 256)
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(contentHeader), streamHeader, headerLength);
                    else
                    {
                        Array.Resize(ref streamHeader, 256 + contentHeader.Length);
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), streamHeader, compactHeaderLength);
                        Array.Copy(System.Text.UTF8Encoding.UTF8.GetBytes(compactHeader), 0, streamHeader, 256, headerLength);
                    }
                    if (content.Length == 0 || dummyImage)
                    {
                        storedContent = null;
                    }
                    else if (fileObj.mimeType.StartsWith("image/") && false)
                    {
                        // backward compatability with asp.net side, only store image and not fileinfo
                        storedContent = content;
                    }
                    else
                    {
                        storedContent = new byte[content.Length + streamHeader.Length];
                        Array.Copy(streamHeader, storedContent, streamHeader.Length);
                        Array.Copy(content, 0, storedContent, streamHeader.Length, content.Length);
                    }
                    new AdminSystem().UpdDbImg(docId, tableName, keyColumnName, columnName, content.Length == 0 || dummyImage ? null : storedContent, LcAppConnString, LcAppPw);
                }
            }
            catch (Exception ex) { throw new Exception("invalid attachment format"); }
        }

        // Overload to handle customized SMTP configuration.
        private Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, string smtp)
        {
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, new List<System.Net.Mail.Attachment>(), smtp);
        }

        // "to" may contain email addresses separated by ";".
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml)
        {
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, new List<KeyValuePair<string, byte[]>> { });
        }

        // Overload to handle attachments and being called by the above.
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, List<KeyValuePair<string, byte[]>> att)
        {
            List<System.Net.Mail.Attachment> mailAtts = new List<System.Net.Mail.Attachment>();
            foreach (var f in att)
            {
                var ms = new MemoryStream(f.Value);
                mailAtts.Add(new System.Net.Mail.Attachment(ms, f.Key));
            }
            return SendEmail(subject, body, to, from, replyTo, fromTitle, isHtml, mailAtts, string.Empty);
        }

        // Overload to handle attachments and being called by the above and should not be called publicly.
        // Return number of emails sent today; users should not exceed 10,000 a day in order to avoid smtp IP labelled as spam email.
        protected Int32 SendEmail(string subject, string body, string to, string from, string replyTo, string fromTitle, bool isHtml, List<System.Net.Mail.Attachment> att, string smtp)
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
            foreach (var t in receipients)
            {
                mm.To.Add(new System.Net.Mail.MailAddress(t.Trim()));
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
            DataTable dtLastScrCriteria = _GetLastCriteria(screenId, rowExpected);
            DataSet ds = criteria == null || criteria.Count == 0 ? MakeScrCriteria(screenId, dvCri, dtLastScrCriteria) : MakeScrCriteria(screenId, dvCri, criteria);
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
            if (!string.IsNullOrEmpty(sp))
            {
                dt = (new AdminSystem()).GetScreenIn(screenId.ToString(), sp, 0, requiredValid, topN,
                searchStr.StartsWith("**") ? "" : searchStr, !searchStr.StartsWith("**"), searchStr.StartsWith("**") ? searchStr.Substring(2) : "", ui, uc,
                isSys != "N" ? (string)null : LcAppConnString,
                isSys != "N" ? null : LcAppPw);
            }
            else
            {
                dt = (new AdminSystem()).GetDdl(screenId, getLisMethod, bAddNew, !searchStr.StartsWith("**"), topN, searchStr.StartsWith("**") ? searchStr.Substring(2) : "",
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
                    colType == typeof(byte[]) ? (dr[columnName] != null && includeBLOB ? EncodeFileStream((byte[])(dr[columnName])) : null) :
                    dr[columnName].ToString();
        }

        protected AutoCompleteResponse LisSuggests(string query, string contextStr, int topN)
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
            DataView dvCri = _GetScrCriteria(screenId).DefaultView;
            DataTable dtSuggest = GetLis(context["method"], csy, screenId, query, new List<string>(), context["filter"], context["conn"], context["isSys"], topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string valFR = context.ContainsKey("mValR") ? context["mValR"] : (dtSuggest.Columns.Contains(keyF + "TextR") ? keyF + "TextR" : "");
            string dtlF = context.ContainsKey("mDtl") && false ? context["mDtl"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string dtlFR = context.ContainsKey("mDtlR") && false ? context["mDtlR"] : (dtSuggest.Columns.Contains(keyF + "DtlR") ? keyF + "DtlR" : "");
            string tipF = context.ContainsKey("mTip") && false ? context["mTip"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : (dtSuggest.Columns.Contains(keyF + "Img") ? keyF + "Img" : "");
            string iconUrlF = context.ContainsKey("mIconUrl") && false ? context["mIconUrl"] : (dtSuggest.Columns.Contains(keyF + "Url") ? keyF + "Url" : "");
            bool hasDtlColumn = dtSuggest.Columns.Contains(keyF + "Dtl");
            bool hasValRColumn = dtSuggest.Columns.Contains(keyF + "TextR");
            bool hasDtlRColumn = dtSuggest.Columns.Contains(keyF + "DtlR");
            bool hasIconColumn = dtSuggest.Columns.Contains(keyF + "Url");
            bool hasImgColumn = dtSuggest.Columns.Contains(keyF + "Img");
            total = dtSuggest.Rows.Count;
            List<SerializableDictionary<string, string>> results = new List<SerializableDictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = _GetAuthRow(screenId);
            bool allowAdd = dtAuthRow.Rows.Count == 0 || dtAuthRow.Rows[0]["AllowAdd"].ToString() != "N";
            //dtSuggest.DefaultView.Sort = valF;
            int pos = 1;
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            int matchCount = -1;
            bool hasMatchCount = dtSuggest.Columns.Contains("MatchCount");
            var rx = new Regex("^[^\\]]*\\]");
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                if (hasMatchCount && matchCount < 0 && !string.IsNullOrEmpty(drv[keyF].ToString().Trim())) int.TryParse(drv["MatchCount"].ToString(), out matchCount);

                string ss = drv[keyF].ToString().Trim();
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
                        results.Add(new SerializableDictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"labelL",rx.Replace(drv[valF].ToString(),"")}, // stripped desc
                            {"value",drv[valF].ToString()}, // visible value shown in jquery's autocomplete box
                            {"iconUrl",iconUrlF !="" ?  drv[iconUrlF].ToString() : null}, // optional icon url
                            {"img", imgF !="" ? (drv[imgF].ToString() == "" ? "": "data:application/base64;base64," + Convert.ToBase64String(drv[imgF] as byte[]))  : null}, // optional embedded image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""},// optional alternative tooltips(say expanded description)
                            {"detail",dtlF !="" ? ToStandardString(dtlF,drv.Row) : null}, // optional detail info
                            {"labelR",valFR !="" ? ToStandardString(valFR,drv.Row) : null}, // optional title(right hand side for react presentation)
                            {"detailR",dtlFR !="" ? ToStandardString(dtlFR,drv.Row) : null} // optional detail info(right hand side for react presentation)
                            /* more can be added in the future for say multi-column list */
                            });
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
            ret.topN = topN;
            ret.matchCount = matchCount < 0 ? 0 : matchCount;

            return ret;
        }

        protected AutoCompleteResponse ddlSuggests(string query, Dictionary<string, string> context, int topN)
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
            DataTable dtSuggest = GetDdl(context["method"], context["addnew"] == "Y", csy, screenId, query, context["conn"], context["isSys"], context.ContainsKey("sp") ? context["sp"] : "", context.ContainsKey("requiredValid") ? context["requiredValid"] : "", (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"])) || (context.ContainsKey("pMKeyCol") && !string.IsNullOrEmpty(context["pMKeyCol"])) ? 0 : topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") ? context["mTip"] : "";
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : "";
            bool valueIsKey = context.ContainsKey("valueIsKey");
            total = dtSuggest.Rows.Count;
            List<SerializableDictionary<string, string>> results = new List<SerializableDictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = _GetAuthRow(screenId);
            string[] DesiredKeys = query.StartsWith("**") ? query.Substring(2).Replace("(", "").Replace(")", "").Split(new char[] { ',' }) : new string[0];
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
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
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"value", valueIsKey ? drv[keyF].ToString() : drv[valF].ToString()}, // visible value shown in jquery's autocomplete box, react expect it to be key not label
                            {"img", imgF !="" ? drv[imgF].ToString() : null}, // optional image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
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
            ret.topN = topN;

            return ret;
        }

        protected Dictionary<string, DataRow> GetScreenMenu(byte systemId, int screenId)
        {
            var context = HttpContext.Current;
            var cache = context.Cache;
            string cacheKey = loginHandle + "_ScreenMenu_" + systemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString();
            int minutesToCache = ShortCacheDuration;
            Dictionary<string, DataRow> menu = cache[cacheKey] as Dictionary<string, DataRow>;
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
        protected string EncodeFileStream(byte[] content)
        {
            byte[] header = content.Length > 256 ? content.Take(256).ToArray() : null;
            if (header != null)
            {
                try
                {
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string headerString = System.Text.UTF8Encoding.UTF8.GetString(header);
                    FileInStreamObj fileInfo = jss.Deserialize<FileInStreamObj>(headerString.Substring(0, headerString.IndexOf('}') + 1));
                    int extensionSize = fileInfo.extensionSize;
                    if (extensionSize > 0)
                    {
                        header = content.Skip(256).Take(fileInfo.extensionSize).ToArray();
                        headerString = System.Text.UTF8Encoding.UTF8.GetString(header);
                        fileInfo = jss.Deserialize<FileInStreamObj>(headerString.Substring(0, headerString.IndexOf('}') + 1));
                    }
                    byte[] fileContent = content.Skip(256 + extensionSize).Take(content.Length - 256 - extensionSize).ToArray();
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

        protected List<SerializableDictionary<string, string>> DataTableToListOfObject(DataTable dt, bool includeBLOB = false, Dictionary<string, DataRow> colAuth = null)
        {

            //var x = dt.AsEnumerable().Select(
            //        row => SerializableDictionary<string, string>.CreateInstance(dt.Columns.Cast<DataColumn>().ToDictionary(
            //                column => column.ColumnName,
            //                column => row[column].ToString()
            //            ))
            //    );
            List<SerializableDictionary<string, string>> ret = new List<SerializableDictionary<string, string>>();
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
                            colType == typeof(DateTime) ? ((DateTime)dr[columnName]).ToString("o") :
                            colType == typeof(byte[]) ? (dr[columnName] != null && includeBLOB ? EncodeFileStream((byte[])(dr[columnName])) : null) :
                            dr[columnName].ToString();
                    else rec[columnName] = null;
                }
                ret.Add(rec);
            }
            return ret;
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
            foreach (string column in keyColumns)
            {
                key += dr[column];
            }
            return key;
        }

        protected List<SerializableDictionary<string, string>> DataTableToListOfDdlObject(DataTable dt, string keyColumn, string refColumn)
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
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            AutoCompleteResponse r = new AutoCompleteResponse();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            mr.errorMsg = "";
            r.data = DataTableToListOfDdlObject(dt, keyColumn, refColumn);
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
                    Children = bRecurr ? BuildMenuTree(mh, new DataView(dvMenu.Table, string.Format("ParentId = {0}", drv["MenuId"].ToString()), "ParentId, ParentQId,Qid", DataViewRowState.CurrentRows), path, level + 1, bRecurr) : new List<MenuNode>()
                };
                if (mt.ParentId == "" || mh.Contains(mt.ParentId)) menus.Add(mt);
            }
            return menus;
        }

        public AsmxBase()
        {

        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAuthRow()
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetAuthRow(GetScreenId());
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
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

                DataView dvCri = _GetScrCriteria(GetScreenId()).DefaultView;

                bool isCriVisible = true;
                DataSet ds = MakeScrCriteria(GetScreenId(), dvCri, criteriaValues, true);

                if (validationErrs.Count == 0)
                {
                    if (dvCri.Table.Rows.Count > 0)
                    {
                        (new AdminSystem()).UpdScrCriteria(GetScreenId().ToString(), GetProgramName(), dvCri, LUser.UsrId, isCriVisible, ds, LcAppConnString, LcAppPw);
                        mr.errorMsg = "";
                    }

                    DataTable dtLastCriteria = _GetLastCriteria(GetScreenId(), 0, true);

                    for (int ii = 1; ii < dtLastCriteria.Rows.Count; ii++)
                    {
                        result.Add(dvCri[ii - 1]["ColumnName"].ToString(), dtLastCriteria.Rows[ii]["LastCriteria"].ToString());
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<MenuNode>, SerializableDictionary<string, AutoCompleteResponse>> GetMenu()
        {
            Func<ApiResponse<List<MenuNode>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ApiResponse<List<MenuNode>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<MenuNode>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
                DataTable dtMenuItems = (new MenuSystem()).GetMenu(LUser.CultureId, GetSystemId(), LImpr, LcSysConnString, LcAppPw, 0, 0, 0);
                DataView dvMenu = new DataView(dtMenuItems);
                dvMenu.Sort = "ParentQid, Qid";
                dvMenu.RowFilter = string.Format("ParentQid IS NULL");
                string[] path = new string[0];
                HashSet<string> mh = new HashSet<string>();
                List<MenuNode> menu = BuildMenuTree(mh, dvMenu, path, 0, true);

                DataTable dt = _GetScreenHlp(GetScreenId());
                mr.errorMsg = "";
                mr.data = menu;
                mr.status = "success";

                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), 0, "R", null));
            return ret;
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
            var ret = ProtectedCall(RestrictedApiCall(fn, 3, 0, "R", null));
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> GetScreenCriteria()
        {
            Func<ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScrCriteria(GetScreenId());
                if (!dt.Columns.Contains("LastCriteria"))
                {
                    dt.Columns.Add("LastCriteria", typeof(string));
                }
                DataTable dtLastCriteria = _GetLastCriteria(GetScreenId(), 0);
                //skip first row in last criteria
                for (int ii = 1; ii < dtLastCriteria.Rows.Count; ii++)
                {
                    dt.Rows[ii - 1]["LastCriteria"] = dtLastCriteria.Rows[ii]["LastCriteria"];
                }
                return DataTableToLabelResponse(dt, new List<string>() { "ColumnName" });
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenCriteriaDdlList(string screenCriId, string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                DataTable dt = _GetScrCriteria(GetScreenId());
                var drv = (from r in dt.AsEnumerable() where r.Field<int?>("ScreenCriId").ToString() == screenCriId select r).First();

                if (drv["DisplayMode"].ToString() == "AutoComplete")
                {
                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    context["method"] = "GetScreenIn";
                    context["addnew"] = "Y";
                    //remind Gary to change the following line
                    context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + GetSystemId() + "C" + screenCriId;
                    context["requiredValid"] = drv["RequiredValid"].ToString();
                    context["mKey"] = drv["DdlKeyColumnName"].ToString();
                    context["mVal"] = drv["DdlRefColumnName"].ToString();
                    context["mTip"] = drv["DdlRefColumnName"].ToString();
                    context["mImg"] = drv["DdlRefColumnName"].ToString();
                    context["ssd"] = "1";
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
                    return DataTableToApiResponse((new AdminSystem()).GetScreenIn(GetScreenId().ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, LImpr, LCurr, LcAppConnString, LcAppPw), "", 0);
                }
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
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
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
            return ret;
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

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetColumnContent(string mstId, string dtlId, string columnName, bool isMaster, string screenColumnName)
        {

            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
                ValidatedMstId(GetValidateMstIdSPName(), GetSystemId(), GetScreenId(), "**" + mstId, MatchScreenCriteria(_GetScrCriteria(GetScreenId()).DefaultView, null));
                string keyColumName = isMaster ? GetMstKeyColumnName(true) : GetDtlKeyColumnName(true);
                string tableName = isMaster ? GetMstTableName(true) : GetDtlTableName(true);
                DataTable dt = (new AdminSystem()).GetDbImg(dtlId, tableName, keyColumName, columnName, LcAppConnString, LcAppPw);
                List<SerializableDictionary<string, string>> content = DataTableToListOfObject(dt);
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                mr.data = DataTableToListOfObject(dt, true);
                mr.status = "success";
                mr.errorMsg = "";
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", screenColumnName, emptyListResponse));
            return ret;
        }

        protected string ValidatedDdlValue(string columnName, Dictionary<string, string> mst, Dictionary<string, string> curr)
        {
            var ddlContext = GetDdlContext();
            var context = ddlContext[columnName];
            var val = curr[columnName];
            var refVal = context.ContainsKey("refCol") ? (context["refColSrc"] == "Mst" ? mst : curr)[context["refColSrcName"]] : null;
            var x = ddlContext[columnName].Clone(new Dictionary<string, string>() { { "refColVal", refVal }, { "addNew", "N" } });
            var rec = ddlSuggests("**" + val, x, 1);
            return rec.data.Count > 0 ? rec.data[0]["key"] : "";
        }
        protected void ValidateField(DataRow drAuth, DataRow drLabel, Dictionary<string, string> refRecord, ref SerializableDictionary<string, string> revisedRecord, SerializableDictionary<string, string> refMaster, ref List<KeyValuePair<string, string>> errors, SerializableDictionary<string, string> skipValidation)
        {
            string[] isDdlType = { "ComboBox", "DropDownList", "ListBox", "RadioButtonList", "AutoListBox", "WorkflowStatus" };
            string[] readonlyType = { "DataGrid" };
            string colName = drLabel["ColumnName"].ToString() + drLabel["TableId"].ToString();
            string displayName = drLabel["DisplayName"].ToString();
            string displayMode = drLabel["DisplayMode"].ToString();
            if (readonlyType.Contains(displayName)) return;
            string oldVal = refRecord[colName];
            string val = revisedRecord[colName] ?? "";
            bool isMasterTable = drAuth["MasterTable"].ToString() == "Y";
            string skippedValidation = skipValidation[colName] ?? "";
            string skipAllValidation = skipValidation != null ? skipValidation[(isMasterTable ? "SkipAllMst" : "SkipAllDtl")] ?? "" : "";

            //If it's a listbox, reserve the old value
            if (displayName == "ListBox")
            {
                val = oldVal;
                revisedRecord[colName] = val;
                return;
            }

            if (
                ((drAuth["ColReadOnly"].ToString() == "Y" && !skippedValidation.Contains("ColReadOnly") && !skipAllValidation.Contains("ColReadOnly")) || (drAuth["ColVisible"].ToString() == "N") && !skippedValidation.Contains("ColVisible") && !skipAllValidation.Contains("ColVisible"))
                && (oldVal != val)
                )
            {
                /* this should either bounce or use refRecord value FIXME */
                val = oldVal;
                if (!skippedValidation.Contains("SilentColReadOnly") && !skipAllValidation.Contains("SilentColReadOnly"))
                {
                    errors.Add(new KeyValuePair<string, string>(colName, "readonly value cannot be changed" + " " + drLabel["ColumnHeader"].ToString()));
                }
            }

            if (isDdlType.Contains(displayName))
            {
                // this would empty out invalidate field selection
                val = ValidatedDdlValue(colName, drAuth["MasterTable"].ToString() == "Y" ? refRecord : refMaster, revisedRecord);
            }

            if (drLabel["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(val))
            {
                errors.Add(new KeyValuePair<string, string>(colName, drLabel["ErrMessage"].ToString() + " " + drLabel["ColumnHeader"].ToString()));
            }
            if (!string.IsNullOrEmpty(drLabel["MaskValid"].ToString()) && !(new Regex(drLabel["MaskValid"].ToString())).IsMatch(val))
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
                    ValidateField(drAuth, drLabel, currentMst, ref revisedMst, null, ref errors, skipValidation);
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
            var newDtl = InitDtl();
            for (int ii = 0; ii < dtlList.Count; ii++)
            {
                var dtl = dtlList[ii];
                if (string.IsNullOrEmpty(dtl[dtlKeyIdName]) || dtl["_mode"] != "delete")
                {
                    var currentDtl = string.IsNullOrEmpty(dtl[dtlKeyIdName]) ? newDtl : currDtlList[dtl[dtlKeyIdName]];
                    var revisedDtl = dtl.Clone();
                    var dtlErrors = new List<KeyValuePair<string, string>>();
                    int jj = 0;
                    foreach (DataRow drLabel in dtLabel.Rows)
                    {
                        DataRow drAuth = dtAut.Rows[jj];
                        if (!string.IsNullOrEmpty(drLabel["TableId"].ToString()) && drAuth["MasterTable"].ToString() == "N")
                        {
                            string colName = drAuth["ColName"].ToString();
                            ValidateField(drAuth, drLabel, currentDtl, ref revisedDtl, mst, ref dtlErrors, skipValidation);
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
            //changed
            var currDtlList = dtDtl == null ? new Dictionary<string, SerializableDictionary<string, string>>() : DataTableToListOfObject(dtDtl).ToDictionary(dr => dr[dtlKeyIdName].ToString(), dr => dr);

            List<KeyValuePair<string, string>> mstError = ValidateMst(ref mst, currMst, skipValidation);
            List<List<KeyValuePair<string, string>>> dtlError = ValidateDtl(mst, currDtlList, ref dtlList, dtlKeyIdName, skipValidation);

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

        protected string converDefaultValue(object o)
        {
            try
            {
                DateTime date = (DateTime)o;
                return date.ToString("o");
            }
            catch
            {
                try
                {
                    return o.ToString();
                }
                catch { return ""; }
            }
        }

    }


}