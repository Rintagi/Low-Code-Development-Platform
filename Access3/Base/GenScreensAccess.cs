namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class GenScreensAccessBase : Encryption, IDisposable
	{
        public abstract void DelScreenCriDel(string appDatabase, string procedureName, string dbConnectionString, string dbPassword);
        public abstract void DelScreenDel(string srcDatabase, string appDatabase, string desDatabase, string programName, Int32 screenId, string multiDesignDb, string sysProgram, string dbConnectionString, string dbPassword);
        public abstract void Dispose();
        public abstract string GetDByteOle(string DataTypeName, CurrPrj CPrj);
        public abstract DataTable GetDistinctScreenTab(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetObjGroupCol(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract string GetSByteOle(string DataTypeName, CurrPrj CPrj);
        public abstract DataTable GetScreenAud(Int32 screenId, string screenTypeName, string desDatabase, string multiDesignDb, CurrSrc CSrc);
        public abstract DataTable GetScreenById(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetScreenColumns(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetScreenCriDdlById(Int32 screenId, Int32 screenCriId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc);
        public abstract DataTable GetScreenCriDel(Int32 ScreenId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetScreenCriteria(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetScreenDel(string srcDatabase, string dbConnectionString, string dbPassword);
        public abstract DataTable GetScreenLisI1ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc);
        public abstract DataTable GetScreenLisI2ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc);
        public abstract DataTable GetScreenLisI3ById(Int32 screenId, string procedureName, string appDatabase, string sysDatabase, string desDatabase, string multiDesignDb, string sysProgram, CurrSrc CSrc);
        public abstract void GetScreenObjDdlById(Int32 screenId, Int32 screenObjId, string procedureName, string createProcedure, string appDatabase, string sysDatabase, string desDatabase, string pKey, string multiDesignDb, CurrSrc CSrc);
        public abstract DataTable GetServerRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc, UsrImpr ui, UsrCurr uc);
        public abstract DataTable GetWebRule(Int32 screenId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract void MkScrAudit(string CudAction, Int32 ScreenId, string MasterTable, string Gen, string MultiDesignDb, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void MkScreenUpdIn(Int32 screenId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void SetScrNeedRegen(Int32 screenId, CurrSrc CSrc);
        public abstract void SetScrTab(CurrSrc CSrc);
    }
}