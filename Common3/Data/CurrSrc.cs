namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Data;
	using System.Text;

	[SerializableAttribute]
	
	public class CurrSrc
	{
		private bool pSrcCnPool;
		private byte pSrcSystemId;
		private string pSrcServerName;
		private string pSrcDbProvider;
		private string pSrcDbServer;
		private string pSrcDbDatabase;
		private string pSrcDbUserId;
		private string pSrcDbPassword;
		
		public CurrSrc(bool SrcCnPool, DataRow dr)
		{
			pSrcCnPool = SrcCnPool;
			if (dr != null)
			{
				pSrcSystemId = byte.Parse(dr["SystemId"].ToString());
				pSrcServerName = dr["ServerName"].ToString();
				pSrcDbProvider = dr["dbAppProvider"].ToString();
				pSrcDbServer = dr["dbAppServer"].ToString();
				pSrcDbDatabase = dr["dbDesDatabase"].ToString();
				pSrcDbUserId = dr["dbAppUserId"].ToString();
				pSrcDbPassword = dr["dbAppPassword"].ToString();
			}
		}

		public bool SrcCnPool
		{
			get {return pSrcCnPool;}
			set {pSrcCnPool = value;}
		}

		public byte SrcSystemId
		{
			get {return pSrcSystemId;}
			set {pSrcSystemId = value;}
		}

		public string SrcServerName
		{
			get {return pSrcServerName;}
			set {pSrcServerName = value;}
		}

		public string SrcDbProvider
		{
			get {return pSrcDbProvider;}
			set {pSrcDbProvider = value;}
		}

		public string SrcDbServer
		{
			get {return pSrcDbServer;}
			set {pSrcDbServer = value;}
		}

		public string SrcDbDatabase
		{
			get {return pSrcDbDatabase;}
			set {pSrcDbDatabase = value;}
		}

		public string SrcDbUserId
		{
			get {return pSrcDbUserId;}
			set {pSrcDbUserId = value;}
		}

		public string SrcDbPassword
		{
			get {return pSrcDbPassword;}
			set {pSrcDbPassword = value;}
		}

		public string SrcConnectionString
		{
			get
			{
				if (pSrcCnPool) {return Config.GetConnStr(pSrcDbProvider, pSrcServerName, pSrcDbDatabase, "", pSrcDbUserId);}
				else {return Config.GetConnStr(pSrcDbProvider, pSrcServerName, pSrcDbDatabase, "OLE DB Services=-4;", pSrcDbUserId);}
			}
		}
	}
}