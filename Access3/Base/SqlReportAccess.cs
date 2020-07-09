using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RO.Common3;
using RO.Common3.Data;
using RO.SystemFramewk;

namespace RO.Access3
{
	public abstract class SqlReportAccessBase : Encryption, IDisposable
	{
        public abstract void Dispose();
        public abstract DataTable GetDocImage(string ReportId, Int16 TemplateId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetGaugeValue(string reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetSqlReport(string reportId, string programName, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal);
        public abstract void UpdSqlReport(string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword);
        public abstract string MemSqlReport(string PublicAccess, string RptMemCriId, string RptMemFldId, string RptMemCriName, string RptMemCriDesc, string RptMemCriLink, string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword);
        public abstract void UpdMemViewdt(string GenPrefix, string RptMemCriId, string dbConnectionString, string dbPassword);
        public abstract void DelRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword);
        public abstract void IniRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetRptCriteria(string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetReportCriteria(string GenPrefix, string reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetReportColumns(string GenPrefix, string reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetCriReportGrp(string GenPrefix, string reportId, string dbConnectionString, string dbPassword);
        public abstract void MkReportGetIn(string GenPrefix, string reportCriId, string procedureName, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword);
        public abstract DataTable GetIn(string reportId, string programName, int TotalCnt, string RequiredValid, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword);
        public abstract int CountRptCri(string ReportCriId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetDdlRptMemCri(string GenPrefix, string reportId, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc);
        public abstract DataTable GetDdlRptMemFld(string GenPrefix, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc);
        public abstract string GetRptWizId(string reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetAllowSelect(string reportId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetRptHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetRptCriHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword);
        public abstract void DelMemCri(string GenPrefix, string reportId, string MemCriId, string DelHeader, string dbConnectionString, string dbPassword);
        public abstract void IniMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetReportObjHlp(string GenPrefix, Int32 ReportId, Int16 CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable GetDdlAccessCd(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc);
        public abstract void DelMemFld(string GenPrefix, string MemFldId, string dbConnectionString, string dbPassword);
        public abstract string UpdMemFld(string GenPrefix, string PublicAccess, string RptMemFldId, string RptMemFldName, Int32 usrId, string dbConnectionString, string dbPassword);
    }
}
