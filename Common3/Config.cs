namespace RO.Common3
{
	using System;
	using System.Text;
	using System.Configuration;
	using RO.SystemFramewk;

	public class Config
	{
        private static string wConverterUrl;
        private static string wConverterKey;
        private static string wMasterPgFile;
        private static string wImgThreshold;
		private static string wCookieHttpOnly;
		private static string wPwdExpDays;
		private static string wAdminLoginOnly;
        private static string wArchitect;
        private static string wWsdlExe;
        private static string wWsBaseUrl;
        private static string wWsDomain;
        private static string wWsUserName;
        private static string wWsPassword;
        private static string wWsRptBaseUrl;
        private static string wWsRptDomain;
        private static string wWsRptUserName;
        private static string wWsRptPassword;
        private static string wSmtpServer;
		private static string wPromptMsg;
        private static string wNewUrl;
        private static string wPmtUrl;
        private static string wOrdUrl;
		private static string wSslUrl;
		private static string wBuildExe;
		private static string wBackupPath;
		private static string wAppNameSpace;
		private static string wDeployType;
		private static string wClientTierPath;
		private static string wCLanguageCd;
		private static string wCFrameworkCd;
		private static string wRuleTierPath;
		private static string wRLanguageCd;
		private static string wRFrameworkCd;
		private static string wDProviderCd;
		private static string wWebTitle;
		private static string wReadOnlyBColor;
		private static string wMandatoryChar;
		private static string wEnableSsl;
		private static string wPathRtfTemplate;
		private static string wPathTxtTemplate;
		private static string wPathXlsImport;
		private static string wPathTmpImport;
		private static string wDoubleByteDb;
		private static string wLoginImage;
		private static string wDesProvider;
		private static string wDesServer;
		private static string wDesDatabase;
		private static string wDesUserId;
		private static string wDesPassword;
		private static string wDesTimeout;		// in seconds.
        private static string wGoogleAPIKey;
        private static string wGoogleClientId;
        private static string wFacebookAppId;
        private static string wSignUpUrl;
        private static string wPasswordComplexity;
        private static string wPasswordHelpMsg;
        private static string wLicense;
        private static string wAzureAPIClientId;
        private static string wAzureAPISecret;
        private static string wAzureAPIRedirectUrl;
        private static string wSecuredColumnKey;
        private static string wTrustedLoginFederationUrl;
        private static string wEnableTwoFactorAuth;
        private static string wRedirectProjectRoot;
        private static string wExtBasePath;
        private static string wExtBaseUrl;
        private static string wExtDomain;
        private static string wWsXlsUrl;
        private static string wIntBaseUrl;
        private static string wIntDomain;
        private static string wIntBasePath;
        private static string wTranslateExtUrl;
        private static bool wNoTrans = false;
        private static int wCommandTimeOut = 1800;

        private static string wJWTMasterKey;
        private static string wLicenseServer;
        private static string wDesShareCred;
        private static string wIntegratedSecurity;
        private static string wODBCDriver;
        private static string wLocalWebConfig;
        private static string wTechSuppEmail;
        private static string wBehindProxy;
        private static string wBehindSecureProxy;
        private static string wZipViaDirectPost;
        private static string wCronJobBaseUrl;
        private static string wRunCronJob;
        private static string wDownloadGroupLs;
        private static string wGitCheckoutBranch;
        private static string wDropInstallerLocation;
        private static string wPublishReactModules;
        private static string wAdvanceReactBuildVersion;
        private static string wServerIdentity;
        private static string wLicenseModule;
        private static string wLicenseSignerPath;
        private static string wRunCronJobModules;
        private static string wPasswordResetModule;
        private static string wReCaptchaSecretKey;
        private static string wResetPwdEmailTemp;
        private static string wMainSiteUrl;
        private static string wCMCAPIKey;
        private static string wPaypalExpressAPIUserName;
        private static string wPaypalExpressAPIEncPwd;
        private static string wPaypalExpressAPIEncSignature;
        private static string wPaypalRESTAPIClientID;
        private static string wPaypalRESTAPIEncSecret;
        private static string wReactTemplate;
        private static string wDesLegacyMD5Encrypt;
        private static string wHardenedTOTP;
        private static string wExportExcelCSV;
        private static string wFCMServerKey;
        private static string wFXMinuteToCache;
        private static int? wPageCacheControl;
        private static string wEnableFido2;
        private static string wEnableCryptoWallet;
        private static string wEnforceGitCommit;
        private static string wBrokenARRProxy;
        static Config()
		{
            wConverterUrl = ConfigurationManager.AppSettings["WsConverterUrl"];
            wConverterKey = ConfigurationManager.AppSettings["WsConverterKey"];
            wMasterPgFile = ConfigurationManager.AppSettings["MasterPgFile"];
            wImgThreshold = ConfigurationManager.AppSettings["ImgThreshold"];
			wCookieHttpOnly = ConfigurationManager.AppSettings["CookieHttpOnly"];
			wPwdExpDays = ConfigurationManager.AppSettings["PwdExpDays"];
			wAdminLoginOnly = ConfigurationManager.AppSettings["AdminLoginOnly"];
			wArchitect = ConfigurationManager.AppSettings["Architect"];
            wWsdlExe = ConfigurationManager.AppSettings["WsdlExe"];
            wWsBaseUrl = ConfigurationManager.AppSettings["WsBaseUrl"];
            wWsDomain = ConfigurationManager.AppSettings["WsDomain"];
            wWsUserName = ConfigurationManager.AppSettings["WsUserName"];
            wWsPassword = ConfigurationManager.AppSettings["WsPassword"];
            wWsRptBaseUrl = ConfigurationManager.AppSettings["WsRptBaseUrl"];
            wWsRptDomain = ConfigurationManager.AppSettings["WsRptDomain"];
            wWsRptUserName = ConfigurationManager.AppSettings["WsRptUserName"];
            wWsRptPassword = ConfigurationManager.AppSettings["WsRptPassword"];
            wSmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
			wPromptMsg = ConfigurationManager.AppSettings["PromptMsg"];
            wNewUrl = ConfigurationManager.AppSettings["NewUrl"];
            wPmtUrl = ConfigurationManager.AppSettings["PmtUrl"];
            wOrdUrl = ConfigurationManager.AppSettings["OrdUrl"];
			wSslUrl = ConfigurationManager.AppSettings["SslUrl"];
			wBuildExe = ConfigurationManager.AppSettings["BuildExe"];
			wBackupPath = ConfigurationManager.AppSettings["BackupPath"];
			wAppNameSpace = ConfigurationManager.AppSettings["AppNameSpace"];
			wDeployType = ConfigurationManager.AppSettings["DeployType"];
			wClientTierPath = ConfigurationManager.AppSettings["ClientTierPath"];
			wCLanguageCd = ConfigurationManager.AppSettings["CLanguageCd"];
			wCFrameworkCd = ConfigurationManager.AppSettings["CFrameworkCd"];
			wRuleTierPath = ConfigurationManager.AppSettings["RuleTierPath"];
			wRLanguageCd = ConfigurationManager.AppSettings["RLanguageCd"];
			wRFrameworkCd = ConfigurationManager.AppSettings["RFrameworkCd"];
			wDProviderCd = ConfigurationManager.AppSettings["DProviderCd"];
			wWebTitle = ConfigurationManager.AppSettings["WebTitle"];
			wReadOnlyBColor = ConfigurationManager.AppSettings["ReadOnlyBColor"];
			wMandatoryChar = ConfigurationManager.AppSettings["MandatoryChar"];
			wEnableSsl = ConfigurationManager.AppSettings["EnableSsl"];
			wPathRtfTemplate = ConfigurationManager.AppSettings["PathRtfTemplate"];
			wPathTxtTemplate = ConfigurationManager.AppSettings["PathTxtTemplate"];
			wPathXlsImport = ConfigurationManager.AppSettings["PathXlsImport"];
			wPathTmpImport = ConfigurationManager.AppSettings["PathTmpImport"];
			wDoubleByteDb = ConfigurationManager.AppSettings["DoubleByteDb"];
			wLoginImage = ConfigurationManager.AppSettings["LoginImage"];
			wDesProvider = ConfigurationManager.AppSettings["DesProvider"];
			wDesServer = ConfigurationManager.AppSettings["DesServer"];
			wDesDatabase = ConfigurationManager.AppSettings["DesDatabase"];
			wDesUserId = ConfigurationManager.AppSettings["DesUserId"];
			wDesPassword = ConfigurationManager.AppSettings["DesPassword"];
			wDesTimeout = ConfigurationManager.AppSettings["DesTimeout"];
            wGoogleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"] ?? "";
            wGoogleClientId = ConfigurationManager.AppSettings["GoogleClientId"] ?? "";
            wFacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"] ?? "";
            wSignUpUrl = ConfigurationManager.AppSettings["SignUpUrl"] ?? "";
            wPasswordComplexity = ConfigurationManager.AppSettings["PasswordComplexity"] ?? "";
            wPasswordHelpMsg = ConfigurationManager.AppSettings["PasswordHelpMsg"] ?? "";
            wLicense = ConfigurationManager.AppSettings["RintagiLicense"] ?? "";
            wAzureAPIClientId = ConfigurationManager.AppSettings["AzureAPIClientId"] ?? "";
            wAzureAPISecret = ConfigurationManager.AppSettings["AzureAPISecret"] ?? "";
            wAzureAPIRedirectUrl = ConfigurationManager.AppSettings["AzureAPIRedirectUrl"] ?? "";
            wSecuredColumnKey = ConfigurationManager.AppSettings["SecuredColumnKey"] ?? "";
            wTrustedLoginFederationUrl = ConfigurationManager.AppSettings["TrustedLoginFederationUrl"];
            wEnableTwoFactorAuth = ConfigurationManager.AppSettings["EnableTwoFactorAuth"];
            wExtBaseUrl = ConfigurationManager.AppSettings["ExtBaseUrl"] ?? "";
            wExtBasePath = ConfigurationManager.AppSettings["ExtBasePath"] ?? "";
//            wExtDomain = ConfigurationManager.AppSettings["ExtDomain"];
            wRedirectProjectRoot = ConfigurationManager.AppSettings["RedirectProjectRoot"];
            wWsXlsUrl = ConfigurationManager.AppSettings["WsXlsUrl"];
            wIntBaseUrl = ConfigurationManager.AppSettings["IntBaseUrl"];
            wTranslateExtUrl = ConfigurationManager.AppSettings["TranslateExtUrl"];
            if (!string.IsNullOrEmpty(wRedirectProjectRoot))
            {
                string[] redirect = wRedirectProjectRoot.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (redirect.Length == 2)
                {
                    wPathRtfTemplate = (wPathRtfTemplate ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                    wPathTmpImport = (wPathTmpImport ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                    wPathTxtTemplate = (wPathTmpImport ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                    wPathXlsImport = (wPathXlsImport ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                    wClientTierPath = (wClientTierPath ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                    wRuleTierPath = (wRuleTierPath ?? "").ReplaceInsensitive(redirect[0], redirect[1]);
                }
            }
            if (!string.IsNullOrEmpty(wExtBaseUrl))
            {
                var match = new System.Text.RegularExpressions.Regex("https?://([^/]*)(/?.*)").Match(wExtBaseUrl);
                if (match != null && match.Groups.Count > 0)
                {
                    if (string.IsNullOrEmpty(wExtBasePath)) wExtBasePath = string.IsNullOrEmpty(match.Groups[2].Value) ? "/" : match.Groups[2].Value;
                    wExtDomain = match.Groups[1].Value;
                }
            }
            if (!string.IsNullOrEmpty(wIntBaseUrl))
            {
                var match = new System.Text.RegularExpressions.Regex("https?://([^/]*)(/?.*)").Match(wIntBaseUrl);
                if (match != null && match.Groups.Count > 0)
                {
                    if (string.IsNullOrEmpty(wIntBasePath)) wIntBasePath = string.IsNullOrEmpty(match.Groups[2].Value) ? "/" : match.Groups[2].Value;
                    wIntDomain = match.Groups[1].Value;
                }
            }
            try
            {
                wNoTrans = (ConfigurationManager.AppSettings["NoTrans"] ?? "").ToUpper() == "Y";
            }
            catch { }
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["CommandTimeOut"], out wCommandTimeOut);
            }
            catch { }

            wJWTMasterKey = System.Configuration.ConfigurationManager.AppSettings["JWTMasterKey"];
            wLicenseServer = System.Configuration.ConfigurationManager.AppSettings["LicenseServer"];
            wDesShareCred = System.Configuration.ConfigurationManager.AppSettings["DesShareCred"];
            wIntegratedSecurity = System.Configuration.ConfigurationManager.AppSettings["SSPI"];
            wODBCDriver = System.Configuration.ConfigurationManager.AppSettings["ODBCDriver"];
            wLocalWebConfig = System.Configuration.ConfigurationManager.AppSettings["LocalWebConfig"];
            wTechSuppEmail = System.Configuration.ConfigurationManager.AppSettings["TechSuppEmail"];
            wBehindProxy = System.Configuration.ConfigurationManager.AppSettings["BehindProxy"];
            wBehindSecureProxy = System.Configuration.ConfigurationManager.AppSettings["BehindSecureProxy"];
            wZipViaDirectPost = System.Configuration.ConfigurationManager.AppSettings["ZipViaDirectPost"];
            wCronJobBaseUrl = System.Configuration.ConfigurationManager.AppSettings["CronJobBaseUrl"];
            wRunCronJob = System.Configuration.ConfigurationManager.AppSettings["RunCronJob"];
            wDownloadGroupLs = System.Configuration.ConfigurationManager.AppSettings["DownloadGroupLs"];
            wGitCheckoutBranch = System.Configuration.ConfigurationManager.AppSettings["GitCheckoutBranch"];
            wDropInstallerLocation = System.Configuration.ConfigurationManager.AppSettings["DeployDropLocation"];
            wPublishReactModules = System.Configuration.ConfigurationManager.AppSettings["PublishReactModules"];
            wAdvanceReactBuildVersion = System.Configuration.ConfigurationManager.AppSettings["AdvanceReactBuildVersion"];
            wServerIdentity = System.Configuration.ConfigurationManager.AppSettings["ServerIdentity"];
            wLicenseModule = System.Configuration.ConfigurationManager.AppSettings["LicenseModule"];
            wLicenseSignerPath = System.Configuration.ConfigurationManager.AppSettings["LicenseSignerPath"];
            wRunCronJobModules = System.Configuration.ConfigurationManager.AppSettings["RunCronJobModules"];
            wPasswordResetModule = System.Configuration.ConfigurationManager.AppSettings["PasswordResetModule"];
            wReCaptchaSecretKey = System.Configuration.ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            wResetPwdEmailTemp = System.Configuration.ConfigurationManager.AppSettings["ResetPwdEmailTemp"];
            wMainSiteUrl = System.Configuration.ConfigurationManager.AppSettings["MainSiteUrl"];
            wCMCAPIKey = System.Configuration.ConfigurationManager.AppSettings["CMCAPIKey"];
            wPaypalExpressAPIUserName = System.Configuration.ConfigurationManager.AppSettings["PaypalExpressAPIUserName"];
            wPaypalExpressAPIEncPwd = System.Configuration.ConfigurationManager.AppSettings["PaypalExpressAPIEncPwd"];
            wPaypalExpressAPIEncSignature = System.Configuration.ConfigurationManager.AppSettings["PaypalExpressAPIEncSignature"];
            wPaypalRESTAPIClientID = System.Configuration.ConfigurationManager.AppSettings["PaypalRESTAPIClientID"];
            wPaypalRESTAPIEncSecret = System.Configuration.ConfigurationManager.AppSettings["PaypalRESTAPIEncSecret"];
            wReactTemplate = System.Configuration.ConfigurationManager.AppSettings["ReactTemplate"];
            wDesLegacyMD5Encrypt = System.Configuration.ConfigurationManager.AppSettings["DesLegacyMD5Encrypt"];
            wHardenedTOTP = System.Configuration.ConfigurationManager.AppSettings["HardenedTOTP"];
            wExportExcelCSV = System.Configuration.ConfigurationManager.AppSettings["ExportExcelCSV"];
            wFCMServerKey = System.Configuration.ConfigurationManager.AppSettings["FCMServerKey"];
            wFXMinuteToCache = System.Configuration.ConfigurationManager.AppSettings["FXMinuteToCache"];
            try
            {
                wPageCacheControl = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageCacheControl"]);
            }
            catch { }
            wEnableFido2 = System.Configuration.ConfigurationManager.AppSettings["EnableFido2"];
            wEnableCryptoWallet = System.Configuration.ConfigurationManager.AppSettings["EnableCryptoWallet"];
            wEnforceGitCommit = System.Configuration.ConfigurationManager.AppSettings["EnforceGitCommit"];
            wBrokenARRProxy = System.Configuration.ConfigurationManager.AppSettings["BrokenARRProxy"];
        }
        
        public static string WsConverterUrl { get { return wConverterUrl; } }

        public static string WsConverterKey { get { return wConverterKey; } }

        public static string MasterPgFile { get { return wMasterPgFile; } }

        public static string ImgThreshold { get { return wImgThreshold; } }

		public static string CookieHttpOnly { get { return wCookieHttpOnly; } }

		public static string PwdExpDays { get { return wPwdExpDays; } }

		public static string AdminLoginOnly { get { return wAdminLoginOnly; } }

        public static string Architect { get { return wArchitect; } }

        public static string WsdlExe { get { return wWsdlExe; } }

        public static string WsBaseUrl { get { return wWsBaseUrl; } }

        public static string WsDomain { get { return wWsDomain; } }

        public static string WsUserName { get { return wWsUserName; } }

        public static string WsPassword { get { return wWsPassword; } }

        public static string WsRptBaseUrl { get { return wWsRptBaseUrl; } }

        public static string WsRptDomain { get { return wWsDomain; } }

        public static string WsRptUserName { get { return wWsRptUserName; } }

        public static string WsRptPassword { get { return wWsRptPassword; } }

		public static string SmtpServer { get { return wSmtpServer; } }

		public static bool PromptMsg { get { if (wPromptMsg.ToLower() == "true") return true; else return false; } }

        public static string NewUrl { get { return wNewUrl; } }

        public static string PmtUrl { get { return wPmtUrl; } }

        public static string OrdUrl { get { return wOrdUrl; } }

		public static string SslUrl { get { return wSslUrl; } }

		public static string BuildExe { get {return wBuildExe;} }

		public static string BackupPath { get {return wBackupPath;} }

		public static string AppNameSpace { get {return wAppNameSpace;} }

		public static string DeployType { get {return wDeployType;} }

		public static string ClientTierPath { get {return wClientTierPath;} }

		public static string CLanguageCd { get {return wCLanguageCd;} }

		public static string CFrameworkCd { get {return wCFrameworkCd;} }

		public static string RuleTierPath { get {return wRuleTierPath;} }

		public static string RLanguageCd { get {return wRLanguageCd;} }

		public static string RFrameworkCd { get {return wRFrameworkCd;} }

		public static string DProviderCd { get {return wDProviderCd;} }

		public static string WebTitle { get {return wWebTitle;} }

		public static string ReadOnlyBColor { get {return wReadOnlyBColor;} }

		public static string MandatoryChar { get { return wMandatoryChar; } }

		public static bool EnableSsl { get {if (wEnableSsl.ToLower() == "true") return true; else return false;} }

		public static string PathRtfTemplate { get {return wPathRtfTemplate;} }

		public static string PathTxtTemplate { get {return wPathTxtTemplate;} }

		public static string PathXlsImport { get {return wPathXlsImport;} }

		public static string PathTmpImport { get {return wPathTmpImport;} }

		public static bool DoubleByteDb { get {if (wDoubleByteDb.ToLower() == "true") return true; else return false;} }

		public static string LoginImage { get {return wLoginImage;} }

		public static string DesProvider { get {return wDesProvider;} }

		public static string DesServer { get {return wDesServer;} }

		public static string DesDatabase { get {return wDesDatabase;} }

		public static string DesUserId { get {return wDesUserId;} }

		public static string DesPassword { get {return wDesPassword;} }

		private static string DesTimeout { get {return wDesTimeout;} }

        public static string GoogleAPIKey { get { return wGoogleAPIKey; } }

        public static string GoogleClientId { get { return wGoogleClientId; } }

        public static string FacebookAppId { get { return wFacebookAppId; } }

        public static string AzureAPIScret { get { return wAzureAPISecret; } }

        public static string AzureAPIClientId { get { return wAzureAPIClientId; } }

        public static string AzureAPIRedirectUrl { get { return wAzureAPIRedirectUrl; } }

        public static string SignUpUrl { get { return wSignUpUrl; } }

        public static string PasswordComplexity { get { return wPasswordComplexity; } }

        public static string PasswordHelpMsg { get { return wPasswordHelpMsg; } }

        public static string SecuredColumnKey { get { return wSecuredColumnKey; } }

        public static string TrustedLoginFederationUrl { get { return wTrustedLoginFederationUrl; } }

        public static string EnableTwoFactorAuth { get { return wEnableTwoFactorAuth; } }

        public static string ExtBaseUrl { get { return wExtBaseUrl; } }

        public static string ExtBasePath { get { return wExtBasePath; } }

        public static string ExtDomain { get { return wExtDomain; } }

        public static string IntBaseUrl { get { return wIntBaseUrl; } }

        public static string IntBasePath { get { return wIntBasePath; } }

        public static string IntDomain { get { return wIntDomain; } }

        public static string WsXlsUrl { get { return wWsXlsUrl; } }

        public static bool TranslateExtUrl { get { return wTranslateExtUrl == "Y"; } }

        public static bool NoTrans { get { return wNoTrans; } }

        public static int CommandTimeOut { get { return wCommandTimeOut; } }

        public static string JWTMasterKey { get { return wJWTMasterKey; } }

        public static string LicenseServer { get { return string.IsNullOrEmpty(wLicenseServer) ? "https://www.rintagi.com" : wLicenseServer; } }
        public static bool DesShareCred { get { return (wDesShareCred ?? "N").ToUpper() == "Y" ; } }
        public static bool IntegratedSecurity { get { return (wIntegratedSecurity ?? "").ToUpper() == "Y" ; } }
        public static string ODBCDriver { get { return string.IsNullOrEmpty(wODBCDriver) ? "ODBC Driver 13 for SQL Server" : wODBCDriver; } }
        public static string RedirectProjectRoot { get { return wRedirectProjectRoot; } }
        public static string LocalWebConfig { get { return wLocalWebConfig ?? ""; } }
        public static string TechSuppEmail { get { return wTechSuppEmail ?? ""; } }
        public static bool BehindProxy { get { return (wBehindProxy ?? "").ToUpper() == "Y" ; } }
        public static bool BehindSecureProxy { get { return (wBehindSecureProxy ?? "").ToUpper() == "Y" ; } }
        public static bool ZipViaDirectPost { get { return (wZipViaDirectPost ?? "Y").ToUpper() == "Y" ; } }
        public static bool RunCronJob { get { return (wRunCronJob ?? "").ToUpper() != "N"; } }
        public static string CronJobBaseUrl { get { return wCronJobBaseUrl; } }
        public static string DownloadGroupLs { get { return string.IsNullOrEmpty(wDownloadGroupLs) ? "5" : wDownloadGroupLs; } }
        public static string GitCheckoutBranch { get { return wGitCheckoutBranch; } }
        public static string DropInstallerLocation { get { return wDropInstallerLocation; } }
        public static string PublishReactModules { get { return wPublishReactModules; } }
        public static bool AdvanceReactBuildVersion { get { return (wAdvanceReactBuildVersion ?? "").ToUpper() == "Y" ; } } 
        public static string ServerIdentity { get { return wServerIdentity; } }
        public static string LicenseModule { get { return string.IsNullOrEmpty(wLicenseModule) ? "3" : wLicense; } }
        public static string LicenseSignerPath { get { return wLicenseSignerPath; } }
        public static string RunCronJobModules { get { return wRunCronJobModules; } }
        public static string PasswordResetModule { get { return string.IsNullOrEmpty(wPasswordResetModule) ? "3" : wPasswordResetModule; } }
        public static string ReCaptchaSecretKey { get { return wReCaptchaSecretKey; } }
        public static string ResetPwdEmailTemp { get { return string.IsNullOrEmpty(wResetPwdEmailTemp) ? "3" : wResetPwdEmailTemp; } }
        public static string MainSiteUrl { get { return wMainSiteUrl; } }
        public static string CMCAPIKey { get { return wCMCAPIKey; } }
        public static string PaypalExpressAPIUserName { get { return wPaypalExpressAPIUserName; } }
        public static string PaypalExpressAPIEncPwd { get { return wPaypalExpressAPIEncPwd; } }
        public static string PaypalExpressAPIEncSignature { get { return wPaypalExpressAPIEncSignature; } }
        public static string PaypalRESTAPIClientID { get { return wPaypalRESTAPIClientID; } }
        public static string PaypalRESTAPIEncSecret { get { return wPaypalRESTAPIEncSecret; } }
        public static string ReactTemplate { get { return string.IsNullOrEmpty(wReactTemplate) ? "TemplateV4" : wReactTemplate;; } }
        public static bool DesLegacyMD5Encrypt { get { return (wDesLegacyMD5Encrypt ?? "Y").ToUpper() == "Y"; } }
        public static bool HardenedTOTP { get { return (wHardenedTOTP ?? "N").ToUpper() == "Y"; } }
        public static bool ExportExcelCSV { get { return (wExportExcelCSV ?? "Y").ToUpper() == "Y"; } }
        public static string FCMServerKey { get { return wFCMServerKey ; } }
        public static string FXMinuteToCache { get { return wFXMinuteToCache; } }
        public static int? PageCacheControl { get { return wPageCacheControl; } }
        public static bool EnableFido2 { get { return (wEnableFido2 ?? "N").ToUpper() == "Y"; } }
        public static bool EnableCryptoWallet { get { return (wEnableCryptoWallet ?? "N").ToUpper() == "Y"; } }
        public static bool EnforceGitCommit { get { return (wEnforceGitCommit ?? "N").ToUpper() == "Y"; } }
        public static bool BrokenARRProxy { get { return (wBrokenARRProxy ?? "N").ToUpper() == "Y"; } }
        public static string RintagiLicense
        { 
            get { return wLicense; } 
            set {
                Configuration myConfiguration = (Configuration)System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                if (wLicense != value)
                {
                    myConfiguration.AppSettings.Settings.Remove("RintagiLicense");
                    myConfiguration.AppSettings.Settings.Add("RintagiLicense", value);
                    myConfiguration.Save();
                }
            } 
        }
        public static string GetConnStr(string connectionString, string decryptedPasword)
        {
            if (connectionString.EndsWith("Pwd=") && false)
            {
                return connectionString.Replace("Pwd=", "Pwd=\"" + decryptedPasword + "\"");
            }
            else
            {
                return connectionString + decryptedPasword;
            }
        }

        public static string ConvertOleDbConnStrToOdbcConnStr(string connectionString)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"Data Source=([^;]*);database=([^;]*);(Integrated Security=sspi;)*.*User ID=([^;]*);.*password=([^;]*)");
            string odbcDriverVersion = string.IsNullOrEmpty(Config.ODBCDriver) ? "ODBC Driver 13 for SQL Server" : Config.ODBCDriver;
            return rx.Replace(connectionString, m => {
                var dbServer = m.Groups[1].Value;
                var dbName = m.Groups[2].Value;
                var bIntegratedSecurity = !string.IsNullOrEmpty(m.Groups[3].Value);
                var uid = m.Groups[4].Value;
                var pwd = m.Groups[5].Value;
                return "Driver={" + odbcDriverVersion + "}; Server=" + dbServer + ";database=" + dbName + ";" + (bIntegratedSecurity ? "Trusted_Connection=Yes" : "Uid=" + uid + ";Pwd=" + pwd);
            });
        }

		public static string GetConnStr(string dbProvider, string dbServer, string dbDatabase, string dbService, string dbUserId, string driverType = "oledb")
		{
            bool singleSQLCredential = Config.DesShareCred;
            bool useOdbc = (driverType ?? "").ToLower() == "odbc" || (dbProvider ?? "").ToLower() == "odbc" || (singleSQLCredential && (Config.DesProvider ?? "").ToLower() == "odbc");
            bool useSqlClient = (driverType ?? "").ToLower() == "sqlclient";
            bool useOleDb = !useOdbc && !useSqlClient;
            //string defaultOleDbProvider = "SQLOLEDB";
            string defaultOleDbProvider = "MSOLEDBSQL";
            dbProvider = (dbProvider ?? "").ToLower() == "sqloledb" && 
                ((Config.DesProvider ?? "").ToLower() == "msoledbsql" || (Config.DesProvider ?? "").ToLower() == "odbc") ? Config.DesProvider : dbProvider;
            if (useOdbc
                || useOleDb
                || useSqlClient
                )
			{
                bool bIntegratedSecurity = singleSQLCredential && Config.IntegratedSecurity;
                string odbcDriverVersion = string.IsNullOrEmpty(Config.ODBCDriver) ? "ODBC Driver 13 for SQL Server" : Config.ODBCDriver;
                string sqlClientDriverVersion = System.Configuration.ConfigurationManager.AppSettings["SqlClientDriver"] ?? "SQL Server Native Client 11.0";
                if (useOdbc)
                {
                    return "Driver={" + odbcDriverVersion + "}" 
                                + "; Server=" + (singleSQLCredential ? Config.DesServer : dbServer) 
                                + "; database=" + dbDatabase 
                                + ";" + (bIntegratedSecurity 
                                                ? "Trusted_Connection=Yes" 
                                                : "Uid=" + (Config.DesShareCred ? Config.DesUserId : dbUserId) + ";Pwd=");
                }
                else if (useSqlClient)
                {
                    return "Driver={" + sqlClientDriverVersion + "}" 
                                + "; Server=" + dbServer 
                                + "; database=" + dbDatabase
                                + ";" + (bIntegratedSecurity ? "Trusted_Connection=Yes" : "Uid=" + (Config.DesShareCred ? Config.DesUserId : dbUserId) + ";Pwd=");
                }
                else
                {
                    return "Provider=" 
                                + (singleSQLCredential 
                                    ? ((Config.DesProvider ?? "").ToLower() != "odbc" ? Config.DesProvider : defaultOleDbProvider) 
                                    : ((dbProvider ?? "").ToLower() != "odbc" ? dbProvider : defaultOleDbProvider)) 
                                + ((dbProvider ?? "").ToUpper() == "MSOLEDBSQL" && false ? ";DataTypeCompatibility=80" : "") 
                                + ";Data Source=" + (singleSQLCredential ? Config.DesServer : dbServer) 
                                + ";database=" + dbDatabase 
                                + ";Connect Timeout=" + DesTimeout 
                                + ";" + (bIntegratedSecurity 
                                            ? "Integrated Security=sspi;"
                                            : "User ID=" + (Config.DesShareCred ? Config.DesUserId : dbUserId) + ";") 
                                + dbService + ";password=";
                }
			}
			else	// Sybase for now.
			{
				return "Provider=" + dbProvider + ";Server Name=" + dbServer + ";database=" + dbDatabase + ";Connect Timeout=" + DesTimeout + ";" + dbService + "User ID=" + dbUserId + ";password=";
			}
		}
	}
}