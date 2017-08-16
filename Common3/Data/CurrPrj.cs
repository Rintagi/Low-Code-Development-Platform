namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Data;
	using System.Text;

	[SerializableAttribute]
	
	public class CurrPrj
	{
		private Int16 pEntityId;
		private string pEntityCode;
		private string pDeployPath;

		private string pSrcClientProgramPath;
		private string pSrcClientFrwork;
		private string pSrcRuleProgramPath;
        private string pSrcWsProgramPath;
        private string pSrcRuleFrwork;
		private string pSrcDesProviderCd;
		private string pSrcDesProvider;
		private string pSrcDesServer;
		private string pSrcDesDatabase;
		private string pSrcDesUserId;
		private string pSrcDesPassword;

		private string pTarClientProgramPath;
		private string pTarClientFrwork;
		private string pTarRuleProgramPath;
        private string pTarWsProgramPath;
        private string pTarRuleFrwork;
		private string pTarDesProviderCd;
		private string pTarDesProvider;
		private string pTarDesServer;
		private string pTarDesDatabase;
		private string pTarDesUserId;
		private string pTarDesPassword;

		public CurrPrj(DataRow dr)
		{
			if (dr != null)
			{
				pEntityId = Int16.Parse(dr["EntityId"].ToString());
				pEntityCode = dr["EntityCode"].ToString();
				pDeployPath = dr["DeployPath"].ToString();
				pSrcClientProgramPath = dr["ClientProgramPath"].ToString();
				pSrcClientFrwork = dr["ClientFrwork"].ToString();
				pSrcRuleProgramPath = dr["RuleProgramPath"].ToString();
                pSrcWsProgramPath = dr["WsProgramPath"].ToString();
                pSrcRuleFrwork = dr["RuleFrwork"].ToString();
				pSrcDesProviderCd = dr["DbProviderCd"].ToString();
				pSrcDesProvider = dr["DbProviderOle"].ToString();
				pSrcDesServer = dr["DesServer"].ToString();
				pSrcDesDatabase = dr["DesDatabase"].ToString();
				pSrcDesUserId = dr["DesUserId"].ToString();
				pSrcDesPassword = dr["DesPassword"].ToString();
				pTarClientProgramPath = dr["ClientProgramPath"].ToString();
				pTarClientFrwork = dr["ClientFrwork"].ToString();
				pTarRuleProgramPath = dr["RuleProgramPath"].ToString();
                pTarWsProgramPath = dr["WsProgramPath"].ToString();
                pTarRuleFrwork = dr["RuleFrwork"].ToString();
				pTarDesProviderCd = dr["DbProviderCd"].ToString();
				pTarDesProvider = dr["DbProviderOle"].ToString();
				pTarDesServer = dr["DesServer"].ToString();
				pTarDesDatabase = dr["DesDatabase"].ToString();
				pTarDesUserId = dr["DesUserId"].ToString();
				pTarDesPassword = dr["DesPassword"].ToString();
			}
		}

		public Int16 EntityId
		{
			get { return pEntityId; }
			set { pEntityId = value; }	// Need this for ObjectToXml to work.
		}

		public string EntityCode
		{
			get { return pEntityCode; }
			set { pEntityCode = value; }
		}

		public string DeployPath
		{
			get { return pDeployPath; }
			set { pDeployPath = value; }
		}

		public string SrcClientProgramPath
		{
			get {return pSrcClientProgramPath;}
			set {pSrcClientProgramPath = value;}
		}

		public string SrcClientFrwork
		{
			get {return pSrcClientFrwork;}
			set {pSrcClientFrwork = value;}
		}

		public string SrcRuleProgramPath
		{
			get {return pSrcRuleProgramPath;}
			set {pSrcRuleProgramPath = value;}
		}

        public string SrcWsProgramPath
        {
            get { return pSrcWsProgramPath; }
            set { pSrcWsProgramPath = value; }
        }

		public string SrcRuleFrwork
		{
			get {return pSrcRuleFrwork;}
			set {pSrcRuleFrwork = value;}
		}

		public string SrcDesProviderCd
		{
			get {return pSrcDesProviderCd;}
			set {pSrcDesProviderCd = value;}
		}

		public string SrcDesProvider
		{
			get {return pSrcDesProvider;}
			set {pSrcDesProvider = value;}
		}

		public string SrcDesServer
		{
			get {return pSrcDesServer;}
			set {pSrcDesServer = value;}
		}

		public string SrcDesDatabase
		{
			get {return pSrcDesDatabase;}
			set {pSrcDesDatabase = value;}
		}

		public string SrcDesUserId
		{
			get {return pSrcDesUserId;}
			set {pSrcDesUserId = value;}
		}

		public string SrcDesPassword
		{
			get {return pSrcDesPassword;}
			set {pSrcDesPassword = value;}
		}

		public string TarClientProgramPath
		{
			get {return pTarClientProgramPath;}
			set {pTarClientProgramPath = value;}
		}

		public string TarClientFrwork
		{
			get {return pTarClientFrwork;}
			set {pTarClientFrwork = value;}
		}

		public string TarRuleProgramPath
		{
			get {return pTarRuleProgramPath;}
			set {pTarRuleProgramPath = value;}
		}

        public string TarWsProgramPath
        {
            get { return pTarWsProgramPath; }
            set { pTarWsProgramPath = value; }
        }

		public string TarRuleFrwork
		{
			get {return pTarRuleFrwork;}
			set {pTarRuleFrwork = value;}
		}

		public string TarDesProviderCd
		{
			get {return pTarDesProviderCd;}
			set {pTarDesProviderCd = value;}
		}

		public string TarDesProvider
		{
			get {return pTarDesProvider;}
			set {pTarDesProvider = value;}
		}

		public string TarDesServer
		{
			get {return pTarDesServer;}
			set {pTarDesServer = value;}
		}

		public string TarDesDatabase
		{
			get {return pTarDesDatabase;}
			set {pTarDesDatabase = value;}
		}

		public string TarDesUserId
		{
			get {return pTarDesUserId;}
			set {pTarDesUserId = value;}
		}

		public string TarDesPassword
		{
			get {return pTarDesPassword;}
			set {pTarDesPassword = value;}
		}

		public string SrcDesConnectionString
		{
			get {return Config.GetConnStr(pSrcDesProvider, pSrcDesServer, pSrcDesDatabase, "", pSrcDesUserId);}
		}

		public string TarDesConnectionString
		{
			get {return Config.GetConnStr(pTarDesProvider, pTarDesServer, pTarDesDatabase, "", pTarDesUserId);}
		}
	}
}