namespace RO.Access3
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using RO.Common3;
    using RO.Common3.Data;
	using RO.SystemFramewk;

	// Stock rules only. Written over by robot on each deployment.
	public abstract class WebAccessBase : Encryption, IDisposable
	{
		public abstract void Dispose();
		public abstract bool WrIsUniqueEmail(string UsrEmail);
        public abstract DataTable WrAddDocTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword);
        public abstract DataTable WrAddWfsTbl(byte SystemId, string TableName, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetActiveEmails(string MaintMsgId);
        public abstract DataTable WrGetUsrEmail(string UsrId);
        public abstract string WrGetDefCulture(bool CultureIdOnly);

        // Obtain table-valued function from the physical database for virtual table.
        public abstract string WrGetVirtualTbl(string TableId, byte DbId, string dbConnectionString, string dbPassword);

        public abstract string WrSyncByDb(int UsrId, byte SystemId, byte DbId, string TableId, string TableName, string TableDesc, bool MultiDesignDb, string dbConnectionString, string dbPassword);
        public abstract string WrSyncToDb(byte SystemId, string TableId, string TableName, bool MultiDesignDb, string dbConnectionString, string dbPassword);
        public abstract string WrGetCustomSp(string CustomDtlId, byte DbId, string dbConnectionString, string dbPassword);
        public abstract string WrGetSvrRule(string ServerRuleId, byte DbId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetDbTableSys(string TableId, byte DbId, string dbConnectionString, string dbPassword);

        // Return databases affected for synchronization of this stored procedure to physical database.
        public abstract DataTable WrGetSvrRuleSys(string ScreenId, byte DbId, string dbConnectionString, string dbPassword);

        public abstract string WrSyncProc(string ProcedureName, string ProcCode, string dbConnectionString, string dbPassword);
        public abstract string WrSyncFunc(string FunctionName, string ProcCode, string dbConnectionString, string dbPassword);
        public abstract string WrUpdSvrRule(string ServerRuleId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetReportApp(byte DbId, string dbConnectionString, string dbPassword);
        public abstract string WrGetRptProc(string ProcName, byte DbId, string dbConnectionString, string dbPassword);
        public abstract string WrUpdRptProc(string ReportId, string dbConnectionString, string dbPassword);
        public abstract string WrGetMemTranslate(string InStr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetCtCrawler(string CrawlerCd);
        public abstract DataTable WrGetCtButtonHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsCtButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetButtonHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsButtonHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetMenuHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsMenuHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetMsgCenter(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsMsgCenter(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetCultureLbl(string CultureId);
        public abstract void WrInsCultureLbl(DataRow dr, string CultureId);
        public abstract DataTable WrGetReportHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsReportHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetReportCriHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsReportCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetScreenHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsScreenHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetScreenCriHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsScreenCriHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetScreenFilterHlp(string CultureId, string dbConnectionString, string dbPassword);

        public abstract void WrInsScreenFilterHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);

        // Return untranslated items from ScreenObjHlp table:
        public abstract DataTable WrGetScreenObjHlp(string CultureId, string dbConnectionString, string dbPassword);

        public abstract void WrInsScreenObjHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);

        public abstract DataTable WrGetScreenTabHlp(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsScreenTabHlp(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetLabel(string CultureId, string dbConnectionString, string dbPassword);
        public abstract void WrInsLabel(DataRow dr, string CultureId, string dbConnectionString, string dbPassword);
        public abstract string WrRptwizGen(Int32 RptwizId, string SystemId, string AppDatabase, string dbConnectionString, string dbPassword);
        public abstract bool WrRptwizDel(Int32 ReportId, string dbConnectionString, string dbPassword);
        public abstract bool WrXferRpt(Int32 ReportId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetDdlPermId(string PermKeyId, Int32 ScreenId, Int32 TableId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc);
        public abstract DataTable WrGetAdmMenuPerm(Int32 screenId, string keyId58, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc);
        public abstract int CountEmailsSent();
        public abstract DataTable GetDdlOriColumnId33S1682(string rptwizCatId, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId);
        public abstract DataTable GetDdlSelColumnId33S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId);
        public abstract DataTable GetDdlSelColumnId44S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId);
        public abstract DataTable GetDdlSelColumnId77S1682(Int32 screenId, bool bAll, string keyId, string filterId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc, Int16 cultureId);
        public abstract DataTable GetDdlRptGroupId3S1652(string dbConnectionString, string dbPassword);
        public abstract DataTable GetDdlRptChart3S1652(string dbConnectionString, string dbPassword);
        public abstract DataTable GetDdlOperator3S1652(string dbConnectionString, string dbPassword);
        public abstract string AddAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword);
        public abstract bool DelAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword);
        public abstract bool UpdAdmRptWiz95(LoginUsr LUser, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword);
        public abstract void RmTranslatedLbl(string LabelId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrAddMenu(string MenuIndex, string ParentId, string dbConnectionString, string dbPassword);
        public abstract bool WrDelMenu(string MenuId, string dbConnectionString, string dbPassword);
        public abstract void WrUpdMenu(string MenuId, string PMenuId, string ParentId, string MenuText, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrAddScreenTab(string TabFolderOrder, string ScreenId, string dbConnectionString, string dbPassword);
        public abstract bool WrDelScreenTab(string ScreenTabId, string dbConnectionString, string dbPassword);
        public abstract void WrUpdScreenTab(string ScreenTabId, string TabFolderOrder, string TabFolderName, string CultureId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrAddScreenObj(string ScreenId, string PScreenObjId, string TabFolderId, bool IsTab, bool NewRow, string dbConnectionString, string dbPassword);
        public abstract bool WrDelScreenObj(string ScreenObjId, string dbConnectionString, string dbPassword);
        public abstract void WrUpdScreenObj(string ScreenObjId, string PScreenObjId, string TabFolderId, string ColumnHeader, string CultureId, string dbConnectionString, string dbPassword);
        public abstract string WrGetScreenId(string ProgramName, string dbConnectionString, string dbPassword);
        public abstract string WrGetMasterTable(string ScreenId, string ColumnId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetScreenObj(string ScreenId, Int16 CultureId, string ScreenObjId, string dbConnectionString, string dbPassword);
        public abstract string WrCloneScreen(string ScreenId, string dbConnectionString, string dbPassword);
        public abstract string WrCloneReport(string ReportId, string dbConnectionString, string dbPassword);
        public abstract void PurgeScrAudit(Int16 YearOld, string dbConnectionString, string dbPassword);
        public abstract void WrUpdScreenReactGen(string ScreenId, string dbConnectionString, string dbPassword);
        public abstract DataTable WrGetWebRule(string ScreenId, string dbConnectionString, string dbPassword);
    }
}