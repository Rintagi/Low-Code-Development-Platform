namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class GenSectionAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
        public abstract DataTable GetPageLnk(string PageObjId);
        public abstract DataTable GetPageObj(string SectionCd);
        public abstract void SetSctNeedRegen(string SectionCd);
    }
}