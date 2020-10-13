namespace RO.Common3
{
	using System;
	using System.Text;
	using System.Configuration;
	using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Net;
    using System.IO;
    using System.Collections.Generic;

    public class Encryption: License
    {
		private string pExpiryDt = "9999.12.01";
        public const string ROVersion = "20200228";
		// RCEncryption uses TripleDES algorithm to encrypt and/or decrypt an input string.
		// By default a key is used to do the decryption, this key should be the same for decryption and encryption.
		
        public Encryption()
            : base(Config.DesLegacyMD5Encrypt)
        {
            if (DateTime.Now >= DateTime.Parse(pExpiryDt)) { throw new Exception("License has expired, please procure another license and try again."); }
        }

        public string GetInstallID()
        {
            return "";
        }
        public string GetAppID()
        {
            return "";
        }
        private System.Collections.Generic.Dictionary<string, string> GetLicense(string installID, string appID, string moduleName)
        {
            return new System.Collections.Generic.Dictionary<string, string>();
        }
        public System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> GetLicenseDetail(string installID, string appID, string moduleName)
        {
            return new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>();
        }
        public bool IsValidateLicense()
        {
            return true;
        }

        public string GetMachineKey()
        {
            return "";
        }

        public bool CheckValidLicense(string moduleName, string resourceName)
        {
            return true;
        }
        public Tuple<string,bool,string> UpdateLicense(string license,string hash)
        {
            return new Tuple<string, bool, string>("",false,"");
        }
        public int GetLicensedCompanyCount()
        {
            return -1;
        }
        public int GetLicensedProjectCount()
        {
            return -1;
        }
        public int GetLicensedModuleCount()
        {
            return -1;
        }
        public int GetLicensedUserCount()
        {
            return -1;
        }
        public bool IsLicensedFeature(string moduleName, string resourceName)
        {
            return true;
        }

        public Tuple<string, string, string> EncodeLicenseString(string licenseJSON, string installID, string appId, bool encrypt, bool perInstance, string signerFile = null)
        {
            return new Tuple<string, string, string>("", "", "");
        }

        public Dictionary<string, Dictionary<string, string>> DecodeLicenseDetail(string licenseJSON)
        {
            Dictionary<string, Dictionary<string, string>> moduleList = new Dictionary<string, Dictionary<string, string>>();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(licenseJSON);

        }

        private Tuple<string, bool, string> GetLicenseJSON()
        {
            bool fullyLicensed = false;
            string licenseJSON = Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, Dictionary<string, string>>(){
                { "Design", new Dictionary<string, string>()
                            {
                                {"CompanyCount", "-1" },
                                {"ProjectCount", "-1" },
                                {"UserCount", "-1" },
                                {"ModuleCount", fullyLicensed ? "-1" : "0" },
                                {"Include", "All" },
                                {"Exclude", fullyLicensed ? "" : "Deploy" },
                                {"Expiry", DateTime.Today.ToUniversalTime().AddYears(fullyLicensed ? 100 : -1).AddMonths(0).AddDays(0).ToString("o")},
                            }
                },
            });

            return new Tuple<string, bool, string>(licenseJSON, fullyLicensed, Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, string>() { { "License", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(licenseJSON)) }, { "LicenseSig", "" }, { "Encrypted", "N" } }))));
        }

        public Tuple<string, bool, string> DecodeLicenseString(string _licenseStringBase64 = null, Action<string> updateLicense = null)
        {
            string licenseStringBase64 = _licenseStringBase64 ?? Config.RintagiLicense;
            Tuple<string, bool, string> defaultLicense = GetLicenseJSON();
            return defaultLicense;
        }


        public string RenewLicense(string LicenseServerEndPoint, string InstallID=null, string AppId = null, string AppNameSpace = null)
        {
            return null;
        }
		protected string GetDesConnStr()
		{
			return Config.GetConnStr(Config.DesProvider, Config.DesServer, Config.DesDatabase, "", Config.DesUserId) + DecryptString(Config.DesPassword);
		}
	}
}