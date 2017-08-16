namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Data;
	using System.Text;

	[SerializableAttribute]
	
	public class CurrTar
	{
		private bool pTarCnPool;
		private String pTarServerName;
		private String pTarDbProvider;
		private String pTarDbServer;
		private String pTarDbDatabase;
		private String pTarDbUserId;
		private String pTarDbPassword;
		
		public CurrTar(bool TarCnPool, DataRow dr)
		{
			pTarCnPool = TarCnPool;
			if (dr != null)
			{
				pTarServerName = dr["ServerName"].ToString();
				pTarDbProvider = dr["dbAppProvider"].ToString();
				pTarDbServer = dr["dbAppServer"].ToString();
				pTarDbDatabase = dr["dbDesDatabase"].ToString();
				pTarDbUserId = dr["dbAppUserId"].ToString();
				pTarDbPassword = dr["dbAppPassword"].ToString();
			}
		}

		public bool TarCnPool
		{
			get {return pTarCnPool;}
			set {pTarCnPool = value;}
		}

		public string TarServerName
		{
			get {return pTarServerName;}
			set {pTarServerName = value;}
		}

		public string TarDbProvider
		{
			get {return pTarDbProvider;}
			set {pTarDbProvider = value;}
		}

		public string TarDbServer
		{
			get {return pTarDbServer;}
			set {pTarDbServer = value;}
		}

		public string TarDbDatabase
		{
			get {return pTarDbDatabase;}
			set {pTarDbDatabase = value;}
		}

		public string TarDbUserId
		{
			get {return pTarDbUserId;}
			set {pTarDbUserId = value;}
		}

		public string TarDbPassword
		{
			get {return pTarDbPassword;}
			set {pTarDbPassword = value;}
		}

		public string TarConnectionString
		{
			get
			{
				if (pTarCnPool) {return Config.GetConnStr(pTarDbProvider, pTarServerName, pTarDbDatabase, "", pTarDbUserId);}
				else {return Config.GetConnStr(pTarDbProvider, pTarServerName, pTarDbDatabase, "OLE DB Services=-4;", pTarDbUserId);}
			}
		}
	}
}