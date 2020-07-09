namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	public abstract class GenReportsAccessBase : Encryption, IDisposable
	{
        public abstract void DelReportCriDel(string GenPrefix, string appDatabase, Int32 reportId, string procedureName, string dbConnectionString, string dbPassword);
        public abstract void DelReportDel(string GenPrefix, string srcDatabase, string appDatabase, string desDatabase, string programName, string dbConnectionString, string dbPassword);
        public abstract void Dispose();
        public abstract DataTable GetCriReportGrp(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetReportById(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetReportCha(string GenPrefix, string RptCtrId, CurrSrc CSrc);
        public abstract DataTable GetReportColumns(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetReportCriDel(string GenPrefix, Int32 reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetReportCriteria(string GenPrefix, Int32 reportId, CurrPrj CPrj, CurrSrc CSrc);
        public abstract DataTable GetReportCtr(string GenPrefix, string PRptCtrId, string RptElmId, string RptCelId, CurrSrc CSrc);
        public abstract DataTable GetReportDel(string GenPrefix, string srcDatabase, string dbConnectionString, string dbPassword);
        public abstract DataTable GetReportElm(string GenPrefix, Int32 reportId, CurrSrc CSrc);
        public abstract DataTable GetReportTbl(string GenPrefix, string RptCtrId, string ParentId, CurrSrc CSrc);
        public abstract void MkReportGet(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void MkReportGetIn(string GenPrefix, Int32 reportCriId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void MkReportUpd(string GenPrefix, Int32 reportId, string procedureName, CurrSrc CSrc, string appDatabase, string sysDatabase);
        public abstract void SetRptNeedRegen(Int32 reportId, CurrSrc CSrc);
    }
}