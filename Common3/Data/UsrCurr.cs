namespace RO.Common3.Data
{
	using System;
	using System.IO;
	using System.Text;

	[SerializableAttribute]
	
	public class UsrCurr
	{
		private Int32 pCompanyId;
		private Int32 pProjectId;
		private byte pSystemId;
		private byte pDbId;

        public UsrCurr() { }

		public UsrCurr(Int32 CompanyId, Int32 ProjectId, byte SystemId, byte DbId)
		{
			pCompanyId = CompanyId;		// Current company selection from the LoginModule.
			pProjectId = ProjectId;		// Current project selection from the LoginModule.
			pSystemId = SystemId;		// Current system selection from the LoginModule.
			pDbId = DbId;				// Current database selection from the most recent administration screen.
		}

		public Int32 CompanyId
		{
			get {return pCompanyId;}
			set {pCompanyId = value;}
		}

		public Int32 ProjectId
		{
			get { return pProjectId; }
			set { pProjectId = value; }
		}

		public byte SystemId
		{
			get {return pSystemId;}
			set {pSystemId = value;}
		}

		public byte DbId
		{
			get {return pDbId;}
			set {pDbId = value;}
		}
	}
}