namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Text;

	[SerializableAttribute]
	
	public class LoginUsr
	{
		private string pLoginName;
		private Int32 pUsrId;
		private string pUsrName;
		private string pUsrEmail;
		private string pInternalUsr;
		private string pTechnicalUsr;
		private Int16 pCultureId;
		private string pCulture;
		private byte pDefSystemId;
		private Int32 pDefProjectId;
		private Int32 pDefCompanyId;
		private Int16 pPwdDuration;
		private Int16 pPwdWarn;
        private bool pHasPic;
        private DateTime? pPwdChgDt;
        private string pProvider;
        private bool pTwoFactorAuth;
        private bool pOTPValidated = false;

        public LoginUsr() { }

        public LoginUsr(string loginName, Int32 usrId, string usrName, string usrEmail, string internalUsr, string technicalUsr, Int16 cultureId, string culture, byte defSystemId, Int32 defProjectId, Int32 defCompanyId, Int16 pwdDuration, Int16 pwdWarn, bool hasPic, DateTime? pwdChgDt, string provider, bool twoFactorAuth)
        {
			pLoginName = loginName;
			pUsrId = usrId;
			pUsrName = usrName;
			pUsrEmail = usrEmail;
			pInternalUsr = internalUsr;
			pTechnicalUsr = technicalUsr;
			pCultureId = cultureId;
			pCulture = culture;
			pDefSystemId = defSystemId;
			pDefProjectId = defProjectId;
			pDefCompanyId = defCompanyId;
			pPwdDuration = pwdDuration;
			pPwdWarn = pwdWarn;
            pHasPic = hasPic;
            pPwdChgDt = pwdChgDt;
            pProvider = provider;
            pTwoFactorAuth = twoFactorAuth;
        }

		public string LoginName
		{
			get { return pLoginName; }
			set { pLoginName = value; }	// Need this for ObjectToXml to work.
		}

		public Int32 UsrId
		{
			get {return pUsrId;}
			set { pUsrId = value; }
		}

		public string UsrName
		{
			get { return pUsrName; }
			set { pUsrName = value; }
		}

		public string UsrEmail
		{
			get { return pUsrEmail; }
			set { pUsrEmail = value; }
		}

		public string InternalUsr
		{
			get {return pInternalUsr;}
			set { pInternalUsr = value; }
		}

		public string TechnicalUsr
		{
			get {return pTechnicalUsr;}
			set { pTechnicalUsr = value; }
		}

		public Int16 CultureId
		{
			get {return pCultureId;}
			set { pCultureId = value; }
		}

		public string Culture
		{
			get {return pCulture;}
			set { pCulture = value; }
		}

		public byte DefSystemId
		{
			get {return pDefSystemId;}
			set { pDefSystemId = value; }
		}

		public Int32 DefProjectId
		{
			get { return pDefProjectId; }
			set { pDefProjectId = value; }
		}

		public Int32 DefCompanyId
		{
			get {return pDefCompanyId;}
			set { pDefCompanyId = value; }
		}

		public Int16 PwdDuration
		{
			get { return pPwdDuration; }
			set { pPwdDuration = value; }
		}

		public Int16 PwdWarn
		{
			get { return pPwdWarn; }
			set { pPwdWarn = value; }
		}

        public bool HasPic
        {
            get { return pHasPic; }
            set { pHasPic = value; }
        }

		public DateTime? PwdChgDt
		{
			get { return pPwdChgDt; }
			set { pPwdChgDt = value; }
		}

        public string Provider
        {
            get { return pProvider; }
            set { pProvider = value; }
        }

        public bool TwoFactorAuth
        {
            get { return pTwoFactorAuth; }
            set { pTwoFactorAuth = value; }
        }

        public bool OTPValidated
        {
            get { return pOTPValidated; }
            set { pOTPValidated = value; }
        }
    }
}