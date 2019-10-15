using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Configuration;
using System.Runtime;
using System.Data;

using RO.Common3;
using RO.Common3.Data;

using Newtonsoft;

namespace RO.Facade3
{
    //[XmlRoot("SerializableDictictory")]
    //public class SerializableDictionary<TKey, TValue>
    //    : Dictionary<TKey, TValue>, IXmlSerializable
    //{
    //    public static SerializableDictionary<TKey, TValue> CreateInstance(Dictionary<TKey, TValue> d)
    //    {
    //        var x = new SerializableDictionary<TKey, TValue>();
    //        foreach (KeyValuePair<TKey, TValue> v in d)
    //        {
    //            x.Add(v.Key, v.Value);
    //        }
    //        return x;
    //    }
    //    #region IXmlSerializable Members
    //    public System.Xml.Schema.XmlSchema GetSchema()
    //    {
    //        return null;
    //    }

    //    public void ReadXml(System.Xml.XmlReader reader)
    //    {
    //        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
    //        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

    //        bool wasEmpty = reader.IsEmptyElement;
    //        reader.Read();

    //        if (wasEmpty)
    //            return;

    //        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
    //        {
    //            reader.ReadStartElement("item");

    //            reader.ReadStartElement("key");
    //            TKey key = (TKey)keySerializer.Deserialize(reader);
    //            reader.ReadEndElement();

    //            reader.ReadStartElement("value");
    //            TValue value = (TValue)valueSerializer.Deserialize(reader);
    //            reader.ReadEndElement();

    //            this.Add(key, value);

    //            reader.ReadEndElement();
    //            reader.MoveToContent();
    //        }
    //        reader.ReadEndElement();
    //    }

    //    public void WriteXml(System.Xml.XmlWriter writer)
    //    {
    //        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
    //        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

    //        foreach (TKey key in this.Keys)
    //        {
    //            writer.WriteStartElement("item");

    //            writer.WriteStartElement("key");
    //            keySerializer.Serialize(writer, key);
    //            writer.WriteEndElement();

    //            writer.WriteStartElement("value");
    //            TValue value = this[key];
    //            valueSerializer.Serialize(writer, value);
    //            writer.WriteEndElement();

    //            writer.WriteEndElement();
    //        }
    //    }

    //    public SerializableDictionary<TKey, TValue> Clone(Dictionary<TKey, TValue> mergeWith = null, List<TKey> keys = null)
    //    {
    //        var x = CreateInstance(this);
    //        if (mergeWith != null) mergeWith.ToList().Where(v => keys == null || keys.Count == 0 || keys.Contains(v.Key)).ToList().ForEach(v => x[v.Key] = v.Value);
    //        return x;
    //    }

    //    protected virtual TValue GetValue(TKey key)
    //    {
    //        TValue x = base.TryGetValue(key, out x) ? x : default(TValue); return x;
    //    }
    //    public new TValue this[TKey key]
    //    {
    //        get { return GetValue(key); }
    //        set { base[key] = value; }
    //    }

    //    #endregion

    //}

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

    public sealed class Auth
    {
        private static readonly Lazy<Auth> lazy = new Lazy<Auth>(() => new Auth(),System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        private static string _jwtMasterKey;
        private static string _masterkey;
        private Auth()
        {
        }

        public static bool VerifyHS256JWT(string header, string payload, string base64UrlEncodeSig, string secret)
        {
            Func<byte[], string> base64UrlEncode = (c) => Convert.ToBase64String(c).TrimEnd(new char[] { '=' }).Replace('_', '/').Replace('-', '+');
            HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(secret));
            byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(header + "." + payload));
            return base64UrlEncodeSig == base64UrlEncode(hash);
        }
        public static string GenJWTMasterKey()
        {
            string jwtMasterKey;

            try
            {
                byte[] randomBits = new byte[32];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(randomBits);
                }
                jwtMasterKey = Convert.ToBase64String(randomBits);
            }
            catch
            {
                jwtMasterKey = Config.DesPassword;
            }
            return jwtMasterKey;
        }
        public static Auth GetInstance(string jwtMasterKey = null) {
            var instance = lazy.Value;
            if (_jwtMasterKey == null && !string.IsNullOrEmpty(jwtMasterKey)) {
                _jwtMasterKey = jwtMasterKey;
            }
            else if (_jwtMasterKey != null && jwtMasterKey != null && _jwtMasterKey != jwtMasterKey) {
                throw new Exception("Auth has already been initialized with another key");
            }
            else if (_jwtMasterKey == null && string.IsNullOrEmpty(jwtMasterKey))
            {
                throw new Exception("Auth must be initialized with a secret key");
            }
            return instance;
        }
        public SerializableDictionary<string, string> GetToken(string client_id, string scope, string grant_type, string code, string code_verifier, string redirect_url, string client_secret, string appPath, string appDomain, Func<string, string> getStoredToken, Func<LoginUsr, UsrCurr, UsrImpr, bool, bool> ValidateScope)
        {
            Dictionary<string, object> scopeContext = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(scope);
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

            //var context = HttpContext.Current;
            //string appPath = context.Request.ApplicationPath;
            //string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);
            //HttpSessionState Session = HttpContext.Current.Session;
            //System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            string storedToken;
            RintagiLoginJWT loginJWT = new Func<RintagiLoginJWT>(() =>
            {
                if (grant_type == "authorization_code")
                {
                    storedToken = getStoredToken(code);
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
            UsrCurr LCurr;
            UsrImpr LImpr;
            LoginUsr LUser;
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var currTime = DateTime.Now.ToUniversalTime().Subtract(utc0).TotalSeconds;
            var nbf = loginJWT.nbf;
            var expiredOn = loginJWT.exp;
            int remainingSeconds = expiredOn - (int)currTime;
            var currentHandle = loginJWT.handle;
            bool keepRefreshToken = remainingSeconds > 120 && grant_type == "refresh_token";
            if (currTime > nbf && currTime < expiredOn && ValidateJWTHandle(currentHandle))
            {
                string signingKey = GetSessionSigningKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
                string encryptionKey = GetSessionEncryptionKey(loginJWT.iat.ToString(), loginJWT.loginId.ToString());
                RintagiLoginToken loginToken = DecryptLoginToken(loginJWT.loginToken, encryptionKey);

                LCurr = new UsrCurr(companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId, systemId ?? loginToken.SystemId, systemId ?? loginToken.SystemId);
                LImpr = null;

                LImpr = SetImpersonation(LImpr, loginToken.UsrId, systemId ?? loginToken.SystemId, companyId ?? loginToken.CompanyId, projectId ?? loginToken.ProjectId);
                LUser = new LoginUsr();
                LUser.UsrId = loginToken.UsrId;
                LUser.LoginName = loginToken.LoginName;
                LUser.DefCompanyId = loginToken.DefCompanyId;
                LUser.DefProjectId = loginToken.DefProjectId;
                LUser.DefSystemId = loginToken.DefSystemId;
                LUser.UsrName = loginToken.UsrName;
                LUser.InternalUsr = "Y";
                LUser.CultureId = 1;
                LUser.HasPic = false;
                string refreshTag = keepRefreshToken ? currentHandle : Guid.NewGuid().ToString().Replace("-", "").ToLower();
                string loginTag = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                if (ValidateScope(LUser, LCurr, LImpr, true))
                {
                    string loginTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, access_token_validity, loginTag);
                    string refreshTokenJWT = CreateLoginJWT(LUser, loginToken.DefCompanyId, loginToken.DefProjectId, loginToken.DefSystemId, LCurr, LImpr, appPath, keepRefreshToken ? remainingSeconds : refresh_token_validity, refreshTag);
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
        public string CreateLoginJWT(LoginUsr usr, int defCompanyId, int defProjectId, byte defSystemId, UsrCurr curr, UsrImpr impr, string resources, int validSeconds, string guidHandle)
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
            string payLoad = Newtonsoft.Json.JsonConvert.SerializeObject(token);
            string header = "{\"typ\":\"JWT\",\"alg\":\"HS256\"}";
            HMACSHA256 hmac = new HMACSHA256(System.Text.UTF8Encoding.UTF8.GetBytes(signingKey));
            string content = base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(header)) + "." + base64UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(payLoad));
            byte[] hash = hmac.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(content));
            return content + "." + base64UrlEncode(hash);
        }
        public RintagiLoginJWT GetLoginUsrInfo(string jwt)
        {
            string[] x = (jwt ?? "").Split(new char[] { '.' });
            Func<string, byte[]> base64UrlDecode = s => Convert.FromBase64String(s.Replace('-', '+').Replace('_', '/') + (s.Length % 4 > 1 ? new string('=', 4 - s.Length % 4) : ""));
            if (x.Length >= 3)
            {
                try
                {
                    Dictionary<string, string> header = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[0])));
                    try
                    {
                        RintagiLoginJWT loginJWT = Newtonsoft.Json.JsonConvert.DeserializeObject<RintagiLoginJWT>(System.Text.UTF8Encoding.UTF8.GetString(base64UrlDecode(x[1])));
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
        public bool ValidateJWTHandle(string handle)
        {
            // can check centralized location for universal logout etc.
            return true;
        }
        public string CreateEncryptedLoginToken(LoginUsr usr, int defCompanyId, int defProjectId, byte defSystemId, UsrCurr curr, UsrImpr impr, string resources, string secret)
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
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(loginToken);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            string hash = BitConverter.ToString(hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(json))).Replace("-", "");
            string encrypted = RO.Common3.Utils.ROEncryptString(hash.Left(32) + json, secret);
            return encrypted;
        }
        public RintagiLoginToken DecryptLoginToken(string encryptedToken, string secret)
        {
            string decryptedToken = RO.Common3.Utils.RODecryptString(encryptedToken, secret);
            RintagiLoginToken token = Newtonsoft.Json.JsonConvert.DeserializeObject<RintagiLoginToken>(decryptedToken.Substring(32));
            return token;
        }
        public string GetSessionEncryptionKey(string time, string usrId)
        {
            System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
            return (new AdminSystem()).EncryptString(Convert.ToBase64String(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(MasterKey + usrId + time))));
        }
        public string GetSessionSigningKey(string time, string usrId)
        {
            var key = GetSessionEncryptionKey(time, usrId);
            return Convert.ToBase64String(new Rfc2898DeriveBytes(key, UTF8Encoding.UTF8.GetBytes(key), 1).GetBytes(32));
        }
        public Tuple<string, string> GetSignedToken(string nounce)
        {
            System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
            string hash = BitConverter.ToString(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(nounce))).Replace("-", "");
            return new Tuple<string, string>(hash.Left(6), hash.Substring(6));
        }

        public bool VerifySignedToken(string nounce, string ticketLeft, string ticketRight)
        {
            System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(UTF8Encoding.UTF8.GetBytes(MasterKey));
            string hash = BitConverter.ToString(hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(nounce))).Replace("-", "");
            return hash == ticketLeft.Trim() + ticketRight.Trim();
        }

        private string MasterKey
        {
            get
            {
                if (_masterkey == null)
                {
                    string jwtMasterKey = _jwtMasterKey;
                    // delay brute force attack, 100K round(ethereum keystore use 260K round, 100K round requires about 5 sec as of 2018/6 hardware)
                    //, we only do this once and stored in class variable so there is a 5 sec delay when app started for API usage
                    Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(jwtMasterKey, UTF8Encoding.UTF8.GetBytes(jwtMasterKey), 100000);
                    _masterkey = (new AdminSystem()).EncryptString(Convert.ToBase64String(k.GetBytes(32)));
                }
                return _masterkey;
            }
        }

        private UsrImpr SetImpersonation(UsrImpr LImpr, Int32 usrId, byte systemId, Int32 companyId, Int32 projectId)
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

    }
}
