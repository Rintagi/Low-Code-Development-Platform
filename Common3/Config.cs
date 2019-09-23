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

		public static string GetConnStr(string dbProvider, string dbServer, string dbDatabase, string dbService, string dbUserId)
		{
            if (dbProvider == "Sqloledb" || dbProvider == "MSOLEDBSQL")
			{
				return "Provider=" + dbProvider + ";Data Source=" + dbServer + ";database=" + dbDatabase + ";Connect Timeout=" + DesTimeout + ";" + dbService + "User ID=" + dbUserId + ";password=";
			}
			else	// Sybase for now.
			{
				return "Provider=" + dbProvider + ";Server Name=" + dbServer + ";database=" + dbDatabase + ";Connect Timeout=" + DesTimeout + ";" + dbService + "User ID=" + dbUserId + ";password=";
			}
		}
	}
}