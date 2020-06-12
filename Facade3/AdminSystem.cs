namespace RO.Facade3
{
	using System;
	using System.Data;
	using System.Collections;
    using System.Collections.Generic;
    using RO.Common3;
    using RO.Common3.Data;
	using RO.Rule3;

	public class AdminSystem : MarshalByRefObject
	{
		// For screens:
		public string GetMaintMsg()
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetMaintMsg();
			}
		}

		public DataTable GetHomeTabs(Int32 UsrId, Int32 CompanyId, Int32 ProjectId, byte SystemId)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetHomeTabs(UsrId, CompanyId, ProjectId, SystemId);
			}
		}

		public string SetCult(int UsrId, Int16 CultureId)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.SetCult(UsrId, CultureId);
			}
		}

		public byte GetCult(string lang)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetCult(lang);
			}
		}

		public DataTable GetLang(Int16 CultureId)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetLang(CultureId);
			}
		}

        public DataTable GetLastPageInfo(Int32 screenId, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetLastPageInfo(screenId, usrId, dbConnectionString, dbPassword);
            }
        }

        public void UpdLastPageInfo(Int32 screenId, Int32 usrId, string lastPageInfo, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.UpdLastPageInfo(screenId, usrId, lastPageInfo, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetLastCriteria(Int32 rowsExpected, Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            return (new AdminRules()).GetLastCriteria(rowsExpected, screenId, reportId, usrId, dbConnectionString, dbPassword);
        }

        public void DelDshFldDtl(string DshFldDtlId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.DelDshFldDtl(DshFldDtlId, dbConnectionString, dbPassword);
            }
        }

        public void DelDshFld(string DshFldId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.DelDshFld(DshFldId, dbConnectionString, dbPassword);
            }
        }

        public string UpdDshFld(string PublicAccess, string DshFldId, string DshFldName, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.UpdDshFld(PublicAccess, DshFldId, DshFldName, usrId, dbConnectionString, dbPassword);
            }
        }

        public string GetSchemaScrImp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetSchemaScrImp(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public string GetScrImpTmpl(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScrImpTmpl(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetButtonHlp(Int32 screenId, Int32 reportId, Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetButtonHlp(screenId, reportId, wizardId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetClientRule(Int32 screenId, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetClientRule(screenId, reportId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetScreenHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenHlp(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetGlobalFilter(Int32 usrId, Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetGlobalFilter(usrId, screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetScreenFilter(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenFilter(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetScreenTab(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenTab(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetScreenCriHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenCriHlp(screenId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public void LogUsage(Int32 UsrId, string UsageNote, string EntityTitle, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string Miscellaneous, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.LogUsage(UsrId, UsageNote, EntityTitle, ScreenId, ReportId, WizardId, Miscellaneous, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetInfoByCol(Int32 ScreenId, string ColumnName, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetInfoByCol(ScreenId, ColumnName, dbConnectionString, dbPassword);
            }
        }

		// "string dbConnectionString, string dbPassword" is for backward compatibility and should be deleted:
        public bool IsValidOvride(Credential cr, Int32 usrId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.IsValidOvride(cr, usrId);
            }
        }

        public string GetMsg(string Msg, Int16 CultureId, string TechnicalUsr, string dbConnectionString, string dbPassword)
        {
            return (new AdminRules()).GetMsg(Msg, CultureId, TechnicalUsr, dbConnectionString, dbPassword);
        }
        public DataTable GetCronJob(int? jobId, string jobLink, string dbConnectionString, string dbPassword)
        {
            return (new AdminRules()).GetCronJob(jobId, jobLink, dbConnectionString, dbPassword);
        }
        public void UpdCronJob(int jobId, DateTime? lastRun, DateTime? nextRun, string dbConnectionString, string dbPassword)
        {
            (new AdminRules()).UpdCronJob(jobId, lastRun, nextRun, dbConnectionString, dbPassword);
        }
        public void UpdCronJobStatus(int jobId, string err, string dbConnectionString, string dbPassword)
        {
            (new AdminRules()).UpdCronJobStatus(jobId, err, dbConnectionString, dbPassword);
        }

        // Obtain translated label one at a time from the table "Label" on system dependent database.
        public string GetLabel(Int16 CultureId, string LabelCat, string LabelKey, string CompanyId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetLabel(CultureId, LabelCat, LabelKey, CompanyId, dbConnectionString, dbPassword);
            }
        }

        // Obtain translated labels as one category from the table "Label" on system dependent database.
        public DataTable GetLabels(Int16 CultureId, string LabelCat, string CompanyId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetLabels(CultureId, LabelCat, CompanyId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetScrCriteria(string screenId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScrCriteria(screenId, dbConnectionString, dbPassword);
            }
        }

        public void MkGetScreenIn(string screenId, string screenCriId, string procedureName, string appDatabase, string sysDatabase, string multiDesignDb, string dbConnectionString, string dbPassword, bool reGen = true)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.MkGetScreenIn(screenId, screenCriId, procedureName, appDatabase, sysDatabase, multiDesignDb, dbConnectionString, dbPassword, reGen);
            }
        }

        /* revised to allow filtering by keyid at server level */
        public DataTable GetScreenIn(string screenId, string procedureName, int TotalCnt, string RequiredValid, int topN, string FilterTxt, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenIn(screenId, procedureName, TotalCnt, RequiredValid, topN, FilterTxt, bAll, keyId, ui, uc, dbConnectionString, dbPassword);
            }
        }

        /* always full list - backward compatiblity until all programs have been re-generated */
        public DataTable GetScreenIn(string screenId, string procedureName, int TotalCnt, string RequiredValid, int topN, string FilterTxt, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenIn(screenId, procedureName, TotalCnt, RequiredValid, topN, FilterTxt, true, string.Empty, ui, uc, dbConnectionString, dbPassword);
            }
        }

        /* For backward compatibility only - to be deleted */
        public DataTable GetScreenIn(string screenId, string procedureName, string RequiredValid, int topN, string FilterTxt, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetScreenIn(screenId, procedureName, 0, RequiredValid, topN, FilterTxt, true, string.Empty, ui, uc, dbConnectionString, dbPassword);
            }
        }

        public int CountScrCri(string ScreenCriId, string MultiDesignDb, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.CountScrCri(ScreenCriId, MultiDesignDb, dbConnectionString, dbPassword);
            }
        }

        public void UpdScrCriteria(string screenId, string programName, DataView dvCri, Int32 usrId, bool isCriVisible, DataSet ds, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.UpdScrCriteria(screenId, programName, dvCri, usrId, isCriVisible, ds, dbConnectionString, dbPassword);
            }
        }

        public string EncryptString(string inStr)
        {
            return (new Encryption()).EncryptString(inStr);
        }

		public DataTable GetAuthRow(Int32 ScreenId, string RowAuthoritys, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetAuthRow(ScreenId, RowAuthoritys, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetAuthCol(Int32 ScreenId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetAuthCol(ScreenId, ui, uc, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetAuthExp(Int32 ScreenId, Int16 CultureId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetAuthExp(ScreenId, CultureId, ui, uc, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetScreenLabel(Int32 ScreenId, Int16 CultureId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetScreenLabel(ScreenId, CultureId, ui, uc, dbConnectionString, dbPassword);
			}
		}

        public DataTable GetDdl(Int32 screenId, string procedureName, bool bAddNew, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDdl(screenId, procedureName, bAddNew, bAll, topN, keyId, dbConnectionString, dbPassword, filterTxt, ui, uc);
            }
        }

        public DataTable RunWrRule(int screenId, string procedureName, string dbConnectionString, string dbPassword, string parameterXML, UsrImpr ui, UsrCurr uc, bool noTrans = false)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.RunWrRule(screenId, procedureName, dbConnectionString, dbPassword, parameterXML, ui, uc, noTrans);
            }
        }

        public DataTable GetExp(Int32 screenId, string procedureName, string useGlobalFilter, string dbConnectionString, string dbPassword, Int32 screenFilterId, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetExp(screenId, procedureName, useGlobalFilter, dbConnectionString, dbPassword, screenFilterId, dvCri, ui, uc, ds);
            }
        }

        public DataTable GetLis(Int32 screenId, string procedureName, bool bAddRow, string useGlobalFilter, int topN, string dbConnectionString, string dbPassword, Int32 screenFilterId, string key, string filterTxt, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetLis(screenId, procedureName, bAddRow, useGlobalFilter, topN, dbConnectionString, dbPassword, screenFilterId, key, filterTxt, dvCri, ui, uc, ds);
            }
        }

        public DataTable GetMstById(string procedureName, string keyId1, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetMstById(procedureName, keyId1, dbConnectionString, dbPassword);
            }
        }

        /* Albeit rare, this overload shall take care of more than one column as primary key */
        public DataTable GetMstById(string procedureName, string keyId1, string keyId2, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetMstById(procedureName, keyId1, keyId2, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetDtlById(Int32 screenId, string procedureName, string keyId, string dbConnectionString, string dbPassword, Int32 screenFilterId, UsrImpr ui, UsrCurr uc)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDtlById(screenId, procedureName, keyId, dbConnectionString, dbPassword, screenFilterId, ui, uc);
            }
        }

        public string AddData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc, bool noTrans = false, int commandTimeOut = 1800)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess(commandTimeOut))
            {
                return dac.AddData(ScreenId, bDeferError, LUser, LImpr, LCurr, ds, dbConnectionString, dbPassword, CPrj, CSrc, noTrans);
            }
        }

        public bool UpdData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc, bool noTrans = false, int commandTimeOut = 1800)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess(commandTimeOut))
            {
                return dac.UpdData(ScreenId, bDeferError, LUser, LImpr, LCurr, ds, dbConnectionString, dbPassword, CPrj, CSrc, noTrans);
            }
        }

        public bool DelData(Int32 ScreenId, bool bDeferError, LoginUsr LUser, UsrImpr LImpr, UsrCurr LCurr, DataSet ds, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc, bool noTrans = false, int commandTimeOut = 1800)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess(commandTimeOut))
            {
                return dac.DelData(ScreenId, bDeferError, LUser, LImpr, LCurr, ds, dbConnectionString, dbPassword, CPrj, CSrc, noTrans);
            }
        }

        public string DelDoc(string MasterId, string DocId, string UsrId, string DdlKeyTableName, string TableName, string ColumnName, string pMKey, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.DelDoc(MasterId, DocId, UsrId, DdlKeyTableName, TableName, ColumnName, pMKey, dbConnectionString, dbPassword);
            }
        }

        public bool IsRegenNeeded(string ProgramName, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.IsRegenNeeded(ProgramName, ScreenId, ReportId, WizardId, dbConnectionString, dbPassword);
            }
        }

		// For reports:

        public DataTable GetIn(Int32 reportId, string procedureName, int TotalCnt, string RequiredValid, bool bAll, string keyId, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetIn(reportId, procedureName, TotalCnt, RequiredValid, bAll, keyId, ui, uc, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetIn(Int32 reportId, string procedureName, int TotalCnt, string RequiredValid, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetIn(reportId, procedureName, TotalCnt, RequiredValid, true, string.Empty, ui, uc, dbConnectionString, dbPassword);
            }
        }

        // To be deleted: for backward compatibility only.
        public DataTable GetIn(Int32 reportId, string procedureName, bool bAddNew, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetIn(reportId, procedureName, bAddNew, ui, uc, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetRptDt(Int32 reportId, string procedureName, UsrImpr ui, UsrCurr uc, DataSet ds, DataView dvCri, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal, int commandTimeOut = 1800)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess(commandTimeOut))
            {
                return dac.GetRptDt(reportId, procedureName, ui, uc, ds, dvCri, dbConnectionString, dbPassword, bUpd, bXls, bVal);
            }
        }

        public bool UpdRptDt(Int32 reportId, string procedureName, Int32 usrId, DataSet ds, DataView dvCri, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.UpdRptDt(reportId, procedureName, usrId, ds, dvCri, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetPrinterList(string UsrGroups, string Members)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetPrinterList(UsrGroups, Members);
            }
        }

		public void UpdLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, Int32 criId, string lastCriteria, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				dac.UpdLastCriteria(screenId, reportId, usrId, criId, lastCriteria, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetReportHlp(reportId, cultureId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportCriHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetReportCriHlp(reportId, cultureId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportSct()
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetReportSct();
			}
		}

		public DataTable GetReportItem(Int32 ReportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetReportItem(ReportId, dbConnectionString, dbPassword);
			}
		}

		public string GetRptPwd(string pwd)
		{
			using (Access3.AdminAccess dac = new Access3.AdminAccess())
			{
				return dac.GetRptPwd(pwd);
			}
		}

        // For Wizards:

        public string GetSchemaWizImp(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetSchemaWizImp(wizardId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public string GetWizImpTmpl(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetWizImpTmpl(wizardId, cultureId, dbConnectionString, dbPassword);
            }
        }

        public int ImportRows(Int32 wizardId, string procedureName, bool bOverwrite, Int32 usrId, DataSet ds, int iStart, string fileName, string dbConnectionString, string dbPassword, CurrPrj CPrj, CurrSrc CSrc, bool noTrans = false, int commandTimeOut = 1800)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess(commandTimeOut))
            {
                return dac.ImportRows(wizardId, procedureName, bOverwrite, usrId, ds, iStart, fileName, dbConnectionString, dbPassword, CPrj, CSrc, noTrans);
            }
        }

        public string AddDbDoc(string MasterId, string TblName, string DocName, string MimeType, long DocSize, byte[] dc, string dbConnectionString, string dbPassword, LoginUsr lu)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.AddDbDoc(MasterId, TblName, DocName, MimeType, DocSize, dc, dbConnectionString, dbPassword, lu);
            }
        }

        public string GetDocId(string MasterId, string TblName, string DocName, string UsrId, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDocId(MasterId, TblName, DocName, UsrId, dbConnectionString, dbPassword);
            }
        }

        public void UpdDbDoc(string DocId, string TblName, string DocName, string MimeType, long DocSize, byte[] dc, string dbConnectionString, string dbPassword, LoginUsr lu, string MasterId = null)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.UpdDbDoc(DocId, TblName, DocName, MimeType, DocSize, dc, dbConnectionString, dbPassword, lu);
            }
        }

        public void UpdDbImg(string DocId, string TblName, string KeyName, string ColName, byte[] dc, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.UpdDbImg(DocId, TblName, KeyName, ColName, dc, dbConnectionString, dbPassword);
            }
        }

        public bool IsMDesignDb(string TblName)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.IsMDesignDb(TblName);
            }
        }

        public DataTable GetDbDoc(string DocId, string TblName, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDbDoc(DocId, TblName, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetDbImg(string DocId, string TblName, string KeyName, string ColName, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDbImg(DocId, TblName, KeyName, ColName, dbConnectionString, dbPassword);
            }
        }

        public Dictionary<string, List<string>> HasOutstandRegen(string ns, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.HasOutstandRegen(ns, dbConnectionString, dbPassword);
            }
        }

        public List<string> HasOutstandReleaseContent(string ns, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.HasOutstandReleaseContent(ns, dbConnectionString, dbPassword);
            }
        }

        public string GetDesignVersion(string ns, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetDesignVersion(ns, dbConnectionString, dbPassword);
            }
        }

        public void UpdFxRate(string FrCurrency, string ToCurrency, string ToFxRate)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.UpdFxRate(FrCurrency, ToCurrency, ToFxRate);
            }
        }

        public DataTable GetFxRate(string FrCurrency, string ToCurrency)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                return dac.GetFxRate(FrCurrency, ToCurrency);
            }
        }

        public void MkWfStatus(string ScreenObjId, string MasterTable, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword)
        {
            using (Access3.AdminAccess dac = new Access3.AdminAccess())
            {
                dac.MkWfStatus(ScreenObjId, MasterTable, appDatabase, sysDatabase, dbConnectionString, dbPassword);
            }
        }
    }
}