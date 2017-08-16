using System;
using System.Data;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Facade3
{
	public class SqlReportSystem : MarshalByRefObject
	{

		public DataTable GetDocImage(string ReportId, Int16 TemplateId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetDocImage(ReportId, TemplateId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetGaugeValue(string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetGaugeValue(reportId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetSqlReport(string reportId, string programName, DataView dvCri, UsrImpr ui, UsrCurr uc, DataSet ds, string dbConnectionString, string dbPassword, bool bUpd, bool bXls, bool bVal)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetSqlReport(reportId, programName, dvCri, ui, uc, ds, dbConnectionString, dbPassword, bUpd, bXls, bVal);
			}
		}

		public void UpdSqlReport(string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dac.UpdSqlReport(reportId, programName, dvCri, usrId, ds, dbConnectionString, dbPassword);
			}
		}

		public string MemSqlReport(string PublicAccess, string RptMemCriId, string RptMemFldId, string RptMemCriName, string RptMemCriDesc, string RptMemCriLink, string reportId, string programName, DataView dvCri, Int32 usrId, DataSet ds, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.MemSqlReport(PublicAccess, RptMemCriId, RptMemFldId, RptMemCriName, RptMemCriDesc, RptMemCriLink, reportId, programName, dvCri, usrId, ds, dbConnectionString, dbPassword);
			}
		}

		public void UpdMemViewdt(string GenPrefix, string RptMemCriId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dac.UpdMemViewdt(GenPrefix, RptMemCriId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetRptCriteria(Int32 rowsExpected, string GenPrefix, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			DataTable dt = null;
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dt = dac.GetRptCriteria(GenPrefix, reportId, usrId, dbConnectionString, dbPassword);
			}
			if (dt.Rows.Count != rowsExpected)
			{
				using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
				{
					dac.DelRptCriteria(GenPrefix, reportId, usrId, dbConnectionString, dbPassword);
					dac.IniRptCriteria(GenPrefix, reportId, usrId, dbConnectionString, dbPassword);
					dt = dac.GetRptCriteria(GenPrefix, reportId, usrId, dbConnectionString, dbPassword);
				}
			}
			return dt;
		}

		public DataTable GetReportCriteria(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetReportCriteria(GenPrefix, reportId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportColumns(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetReportColumns(GenPrefix, reportId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetCriReportGrp(string GenPrefix, string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetCriReportGrp(GenPrefix, reportId, dbConnectionString, dbPassword);
			}
		}

		public void MkReportGetIn(string GenPrefix, string reportCriId, string procedureName, string appDatabase, string sysDatabase, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dac.MkReportGetIn(GenPrefix, reportCriId, procedureName, appDatabase, sysDatabase, dbConnectionString, dbPassword);
			}
		}

        public DataTable GetIn(string reportId, string programName, int TotalCnt, string RequiredValid, bool bAll, string keyid, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
            {
                return dac.GetIn(reportId, programName, TotalCnt, RequiredValid, bAll, keyid, ui, uc, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetIn(string reportId, string programName, int TotalCnt, string RequiredValid, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
        {
            using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
            {
                return dac.GetIn(reportId, programName, TotalCnt, RequiredValid, true, string.Empty, ui, uc, dbConnectionString, dbPassword);
            }
        }

        /* For backward compatibility only - to be deleted */
        public DataTable GetIn(string reportId, string programName, string RequiredValid, UsrImpr ui, UsrCurr uc, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
                return dac.GetIn(reportId, programName, 0, RequiredValid, true, string.Empty, ui, uc, dbConnectionString, dbPassword);
            }
		}

        public int CountRptCri(string ReportCriId, string dbConnectionString, string dbPassword)
        {
            using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
            {
                return dac.CountRptCri(ReportCriId, dbConnectionString, dbPassword);
            }
        }

        public DataTable GetDdlRptMemCri(string GenPrefix, string reportId, bool bAll, int topN, string keyId, string dbConnectionString, string dbPassword, string filterTxt, UsrImpr ui, UsrCurr uc)
        {
            using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
            {
                return dac.GetDdlRptMemCri(GenPrefix, reportId, bAll, topN, keyId, dbConnectionString, dbPassword, filterTxt, ui, uc);
            }
        }

		public DataTable GetDdlRptMemFld(string GenPrefix, bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetDdlRptMemFld(GenPrefix, bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

		public string GetRptWizId(string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetRptWizId(reportId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetAllowSelect(string reportId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetAllowSelect(reportId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetRptHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetRptHlp(GenPrefix, reportId, cultureId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetRptCriHlp(string GenPrefix, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetRptCriHlp(GenPrefix, reportId, cultureId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetMemCri(string GenPrefix, byte rowsExpected, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			DataTable dt = null;
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dt = dac.GetMemCri(GenPrefix, reportId, MemCriId, dbConnectionString, dbPassword);
			}
			if (dt.Rows.Count != rowsExpected)
			{
				using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
				{
					dac.DelMemCri(GenPrefix, reportId, MemCriId, "N", dbConnectionString, dbPassword);
					dac.IniMemCri(GenPrefix, reportId, MemCriId, dbConnectionString, dbPassword);
					dt = dac.GetMemCri(GenPrefix, reportId, MemCriId, dbConnectionString, dbPassword);
				}
			}
			return dt;
		}

		public void DelMemCri(string GenPrefix, string reportId, string MemCriId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dac.DelMemCri(GenPrefix, reportId, MemCriId, "Y", dbConnectionString, dbPassword);
			}
		}

		public DataTable GetReportObjHlp(string GenPrefix, Int32 ReportId, Int16 CultureId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetReportObjHlp(GenPrefix, ReportId, CultureId, dbConnectionString, dbPassword);
			}
		}

		public DataTable GetDdlAccessCd(bool bAll, string keyId, string dbConnectionString, string dbPassword, UsrImpr ui, UsrCurr uc)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.GetDdlAccessCd(bAll, keyId, dbConnectionString, dbPassword, ui, uc);
			}
		}

		public void DelMemFld(string GenPrefix, string MemFldId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				dac.DelMemFld(GenPrefix, MemFldId, dbConnectionString, dbPassword);
			}
		}

		public string UpdMemFld(string GenPrefix, string PublicAccess, string RptMemFldId, string RptMemFldName, Int32 usrId, string dbConnectionString, string dbPassword)
		{
			using (Access3.SqlReportAccess dac = new Access3.SqlReportAccess())
			{
				return dac.UpdMemFld(GenPrefix, PublicAccess, RptMemFldId, RptMemFldName, usrId, dbConnectionString, dbPassword);
			}
		}
	}
}
