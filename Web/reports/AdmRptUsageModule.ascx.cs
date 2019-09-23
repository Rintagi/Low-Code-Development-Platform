using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Linq;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Common3.Data
{
	public class DsAdmRptUsageIn : DataSet
	{
		public DsAdmRptUsageIn()
		{
			this.Tables.Add(MakeColumns(new DataTable("DtAdmRptUsageIn")));
			this.DataSetName = "DsAdmRptUsageIn";
			this.Namespace = "http://Rintagi.com/DataSet/DsAdmRptUsageIn";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("Summary", typeof(String));
			columns.Add("SystemId", typeof(Byte));
			columns.Add("FrDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			columns.Add("CompanyId", typeof(Int32));
			columns.Add("ProjectId", typeof(Int32));
			columns.Add("AgentId", typeof(Int32));
			columns.Add("BrokerId", typeof(Int32));
			columns.Add("VendorId", typeof(Int32));
			columns.Add("CustomerId", typeof(Int32));
			columns.Add("InvestorId", typeof(Int32));
			columns.Add("MemberId", typeof(Int32));
			columns.Add("BorrowerId", typeof(Int32));
			columns.Add("GuarantorId", typeof(Int32));
			columns.Add("LenderId", typeof(Int32));
			columns.Add("UsrGroupId", typeof(Int16));
			columns.Add("UserId", typeof(Int32));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmRptUsageModule : RO.Web.ModuleBase
	{

		public ReportDocument rp = new ReportDocument();
		private const string KEY_dtReportSct = "Cache:dtReportSct54";
		private const string KEY_dtReportItem = "Cache:dtReportItem54";
		private const string KEY_dtReportHlp = "Cache:dtReportHlp54";
		private const string KEY_dtCri = "Cache:dtCri54";
		private const string KEY_dtClientRule = "Cache:dtClientRule54";
		private const string KEY_dtPrinters = "Cache:dtPrinters54";
		private const string KEY_bClCriVisible = "Cache:bClCriVisible54";
		private const string KEY_bShCriVisible = "Cache:bShCriVisible54";
		private enum exportTo {TXT, RTF, XML, XLS, PDF, DOC, VIEW};
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;

		public AdmRptUsageModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Unload(object sender, System.EventArgs e)
		{
			rp.Dispose();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			CheckAuthentication(true);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
			ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "datepicker_i18n", "scripts/i18n/jquery.ui.datepicker-" + lang + ".js");
			rp.Load(Server.MapPath(@"reports/AdmRptUsageReport.rpt"));
			if (!IsPostBack)
			{
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId,3, base.LImpr, LcSysConnString, LcAppPw, null, 54, null);
				if (dtMenuAccess.Rows.Count == 0)
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				/* Stop IIS from Caching but allowing export to Excel to work */
				HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
				HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				HttpContext.Current.Response.Cache.SetNoStore();
				Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));
				Response.Cache.SetValidUntilExpires(true);
				Session.Remove(KEY_dtReportSct);
				Session.Remove(KEY_dtReportItem);
				Session.Remove(KEY_dtReportHlp);
				Session.Remove(KEY_dtCri);
				Session.Remove(KEY_dtClientRule);
				SetButtonHlp();
				bool bBatchPrint = false;
				if (base.LUser.InternalUsr == "N")
				{
					cPrintButton.Visible = false; cPrinter.Visible = false;
				}
				else
				{
					Session.Remove(KEY_dtPrinters);
					DataTable dtPrinters;
					dtPrinters = (new AdminSystem()).GetPrinterList(base.LImpr.UsrGroups, base.LImpr.Members);
					if (dtPrinters != null && dtPrinters.Rows.Count > 0)
					{
						cPrinter.DataSource = dtPrinters;
						cPrinter.DataBind();
						cPrinter.SelectedIndex = 0;
						Session[KEY_dtPrinters] = dtPrinters;
					}
					int ii = 0;	//Update criteria for Batch reporting.
					while (Request.QueryString["Cri"+ii.ToString()] != null && Request.QueryString["Val"+ii.ToString()] != null)
					{
						(new AdminSystem()).UpdLastCriteria(0,54,base.LUser.UsrId,Int32.Parse(Request.QueryString["Cri"+ii.ToString()]),Request.QueryString["Val"+ii.ToString()],LcSysConnString,LcAppPw);
						bBatchPrint = true; ii = ii +1;
					}
				}
				DataTable dt;
				DataView dv;
				dt = (new AdminSystem()).GetLastCriteria(17,0,54,base.LUser.UsrId,LcSysConnString,LcAppPw);
				if ((bool)Session[KEY_bClCriVisible]) {cClearCriButton.Visible = cCriteria.Visible;} else {cClearCriButton.Visible = false;}
				if ((bool)Session[KEY_bShCriVisible]) {cShowCriButton.Visible = !cCriteria.Visible;} else {cShowCriButton.Visible = false;}
				DataTable dtCri = GetReportCriHlp();
				string selectedVal = null;
				base.SetCriBehavior(cSummary, cSummaryP1, cSummaryLabel, dtCri.Rows[0]);
				cSummary.AutoPostBack = false;
				if (dt.Rows[0]["LastCriteria"].ToString() != string.Empty)
				{
					cSummary.Checked = base.GetBool(dt.Rows[0]["LastCriteria"].ToString());
				}
				base.SetCriBehavior(cSystemId, cSystemIdP1, cSystemIdLabel, dtCri.Rows[1]);
				cSystemId.AutoPostBack = false;
				try {selectedVal = dt.Rows[1]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54SystemId",(new SqlReportSystem()).CountRptCri("5",LcSysConnString,LcAppPw),"Y",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cSystemId.DataSource = dv;
				cSystemId.DataBind();
				try
				{
					cSystemId.Items.FindByValue(dt.Rows[1]["LastCriteria"].ToString()).Selected = true;
				}
				catch
				{
					try {cSystemId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cFrDate, cFrDateP1, cFrDateLabel, dtCri.Rows[2]);
				cFrDate.AutoPostBack = false;
				if (dt.Rows[2]["LastCriteria"].ToString() != string.Empty)
				{
					cFrDate.Text = dt.Rows[2]["LastCriteria"].ToString();
				}
				base.SetCriBehavior(cToDate, cToDateP1, cToDateLabel, dtCri.Rows[3]);
				cToDate.AutoPostBack = false;
				if (dt.Rows[3]["LastCriteria"].ToString() != string.Empty)
				{
					cToDate.Text = dt.Rows[3]["LastCriteria"].ToString();
				}
				base.SetCriBehavior(cCompanyId, cCompanyIdP1, cCompanyIdLabel, dtCri.Rows[4]);
				cCompanyId.AutoPostBack = false;
				try {selectedVal = dt.Rows[4]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54CompanyId",(new SqlReportSystem()).CountRptCri("25",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cCompanyId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54CompanyId";
					context["addnew"] = "Y";
					context["sp"] = "54CompanyId";
					context["requiredValid"] = "N";
					context["mKey"] = cCompanyId.DataValueField;
					context["mVal"] = cCompanyId.DataTextField;
					context["mTip"] = cCompanyId.DataTextField;
					context["mImg"] = cCompanyId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "25";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cCompanyId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cCompanyId.DataContext = context;
					cCompanyId.Mode = "A";
					cCompanyId.AutoPostBack = false;
				}
				try
				{
					cCompanyId.SelectByValue(dt.Rows[4]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cCompanyId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cProjectId, cProjectIdP1, cProjectIdLabel, dtCri.Rows[5]);
				cProjectId.AutoPostBack = false;
				try {selectedVal = dt.Rows[5]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54ProjectId",(new SqlReportSystem()).CountRptCri("26",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cProjectId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54ProjectId";
					context["addnew"] = "Y";
					context["sp"] = "54ProjectId";
					context["requiredValid"] = "N";
					context["mKey"] = cProjectId.DataValueField;
					context["mVal"] = cProjectId.DataTextField;
					context["mTip"] = cProjectId.DataTextField;
					context["mImg"] = cProjectId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "26";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cProjectId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cProjectId.DataContext = context;
					cProjectId.Mode = "A";
					cProjectId.AutoPostBack = false;
				}
				try
				{
					cProjectId.SelectByValue(dt.Rows[5]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cProjectId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cAgentId, cAgentIdP1, cAgentIdLabel, dtCri.Rows[6]);
				cAgentId.AutoPostBack = false;
				try {selectedVal = dt.Rows[6]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54AgentId",(new SqlReportSystem()).CountRptCri("8",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cAgentId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54AgentId";
					context["addnew"] = "Y";
					context["sp"] = "54AgentId";
					context["requiredValid"] = "N";
					context["mKey"] = cAgentId.DataValueField;
					context["mVal"] = cAgentId.DataTextField;
					context["mTip"] = cAgentId.DataTextField;
					context["mImg"] = cAgentId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "8";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cAgentId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cAgentId.DataContext = context;
					cAgentId.Mode = "A";
					cAgentId.AutoPostBack = false;
				}
				try
				{
					cAgentId.SelectByValue(dt.Rows[6]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cAgentId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cBrokerId, cBrokerIdP1, cBrokerIdLabel, dtCri.Rows[7]);
				cBrokerId.AutoPostBack = false;
				try {selectedVal = dt.Rows[7]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54BrokerId",(new SqlReportSystem()).CountRptCri("11",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cBrokerId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54BrokerId";
					context["addnew"] = "Y";
					context["sp"] = "54BrokerId";
					context["requiredValid"] = "N";
					context["mKey"] = cBrokerId.DataValueField;
					context["mVal"] = cBrokerId.DataTextField;
					context["mTip"] = cBrokerId.DataTextField;
					context["mImg"] = cBrokerId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "11";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cBrokerId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cBrokerId.DataContext = context;
					cBrokerId.Mode = "A";
					cBrokerId.AutoPostBack = false;
				}
				try
				{
					cBrokerId.SelectByValue(dt.Rows[7]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cBrokerId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cVendorId, cVendorIdP1, cVendorIdLabel, dtCri.Rows[8]);
				cVendorId.AutoPostBack = false;
				try {selectedVal = dt.Rows[8]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54VendorId",(new SqlReportSystem()).CountRptCri("12",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cVendorId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54VendorId";
					context["addnew"] = "Y";
					context["sp"] = "54VendorId";
					context["requiredValid"] = "N";
					context["mKey"] = cVendorId.DataValueField;
					context["mVal"] = cVendorId.DataTextField;
					context["mTip"] = cVendorId.DataTextField;
					context["mImg"] = cVendorId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "12";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cVendorId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cVendorId.DataContext = context;
					cVendorId.Mode = "A";
					cVendorId.AutoPostBack = false;
				}
				try
				{
					cVendorId.SelectByValue(dt.Rows[8]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cVendorId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cCustomerId, cCustomerIdP1, cCustomerIdLabel, dtCri.Rows[9]);
				cCustomerId.AutoPostBack = false;
				try {selectedVal = dt.Rows[9]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54CustomerId",(new SqlReportSystem()).CountRptCri("13",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cCustomerId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54CustomerId";
					context["addnew"] = "Y";
					context["sp"] = "54CustomerId";
					context["requiredValid"] = "N";
					context["mKey"] = cCustomerId.DataValueField;
					context["mVal"] = cCustomerId.DataTextField;
					context["mTip"] = cCustomerId.DataTextField;
					context["mImg"] = cCustomerId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "13";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cCustomerId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cCustomerId.DataContext = context;
					cCustomerId.Mode = "A";
					cCustomerId.AutoPostBack = false;
				}
				try
				{
					cCustomerId.SelectByValue(dt.Rows[9]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cCustomerId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cInvestorId, cInvestorIdP1, cInvestorIdLabel, dtCri.Rows[10]);
				cInvestorId.AutoPostBack = false;
				try {selectedVal = dt.Rows[10]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54InvestorId",(new SqlReportSystem()).CountRptCri("9",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cInvestorId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54InvestorId";
					context["addnew"] = "Y";
					context["sp"] = "54InvestorId";
					context["requiredValid"] = "N";
					context["mKey"] = cInvestorId.DataValueField;
					context["mVal"] = cInvestorId.DataTextField;
					context["mTip"] = cInvestorId.DataTextField;
					context["mImg"] = cInvestorId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "9";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cInvestorId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cInvestorId.DataContext = context;
					cInvestorId.Mode = "A";
					cInvestorId.AutoPostBack = false;
				}
				try
				{
					cInvestorId.SelectByValue(dt.Rows[10]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cInvestorId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cMemberId, cMemberIdP1, cMemberIdLabel, dtCri.Rows[11]);
				cMemberId.AutoPostBack = false;
				try {selectedVal = dt.Rows[11]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54MemberId",(new SqlReportSystem()).CountRptCri("14",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cMemberId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54MemberId";
					context["addnew"] = "Y";
					context["sp"] = "54MemberId";
					context["requiredValid"] = "N";
					context["mKey"] = cMemberId.DataValueField;
					context["mVal"] = cMemberId.DataTextField;
					context["mTip"] = cMemberId.DataTextField;
					context["mImg"] = cMemberId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "14";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cMemberId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cMemberId.DataContext = context;
					cMemberId.Mode = "A";
					cMemberId.AutoPostBack = false;
				}
				try
				{
					cMemberId.SelectByValue(dt.Rows[11]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cMemberId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cBorrowerId, cBorrowerIdP1, cBorrowerIdLabel, dtCri.Rows[12]);
				cBorrowerId.AutoPostBack = false;
				try {selectedVal = dt.Rows[12]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54BorrowerId",(new SqlReportSystem()).CountRptCri("58",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cBorrowerId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54BorrowerId";
					context["addnew"] = "Y";
					context["sp"] = "54BorrowerId";
					context["requiredValid"] = "N";
					context["mKey"] = cBorrowerId.DataValueField;
					context["mVal"] = cBorrowerId.DataTextField;
					context["mTip"] = cBorrowerId.DataTextField;
					context["mImg"] = cBorrowerId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "58";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cBorrowerId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cBorrowerId.DataContext = context;
					cBorrowerId.Mode = "A";
					cBorrowerId.AutoPostBack = false;
				}
				try
				{
					cBorrowerId.SelectByValue(dt.Rows[12]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cBorrowerId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cGuarantorId, cGuarantorIdP1, cGuarantorIdLabel, dtCri.Rows[13]);
				cGuarantorId.AutoPostBack = false;
				try {selectedVal = dt.Rows[13]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54GuarantorId",(new SqlReportSystem()).CountRptCri("59",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cGuarantorId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54GuarantorId";
					context["addnew"] = "Y";
					context["sp"] = "54GuarantorId";
					context["requiredValid"] = "N";
					context["mKey"] = cGuarantorId.DataValueField;
					context["mVal"] = cGuarantorId.DataTextField;
					context["mTip"] = cGuarantorId.DataTextField;
					context["mImg"] = cGuarantorId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "59";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cGuarantorId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cGuarantorId.DataContext = context;
					cGuarantorId.Mode = "A";
					cGuarantorId.AutoPostBack = false;
				}
				try
				{
					cGuarantorId.SelectByValue(dt.Rows[13]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cGuarantorId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cLenderId, cLenderIdP1, cLenderIdLabel, dtCri.Rows[14]);
				cLenderId.AutoPostBack = false;
				try {selectedVal = dt.Rows[14]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54LenderId",(new SqlReportSystem()).CountRptCri("60",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cLenderId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54LenderId";
					context["addnew"] = "Y";
					context["sp"] = "54LenderId";
					context["requiredValid"] = "N";
					context["mKey"] = cLenderId.DataValueField;
					context["mVal"] = cLenderId.DataTextField;
					context["mTip"] = cLenderId.DataTextField;
					context["mImg"] = cLenderId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "60";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cLenderId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cLenderId.DataContext = context;
					cLenderId.Mode = "A";
					cLenderId.AutoPostBack = false;
				}
				try
				{
					cLenderId.SelectByValue(dt.Rows[14]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cLenderId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cUsrGroupId, cUsrGroupIdP1, cUsrGroupIdLabel, dtCri.Rows[15]);
				cUsrGroupId.AutoPostBack = false;
				try {selectedVal = dt.Rows[15]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54UsrGroupId",(new SqlReportSystem()).CountRptCri("15",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cUsrGroupId.DataSource = dv;
				cUsrGroupId.DataBind();
				try
				{
					cUsrGroupId.Items.FindByValue(dt.Rows[15]["LastCriteria"].ToString()).Selected = true;
				}
				catch
				{
					try {cUsrGroupId.SelectedIndex = 0;} catch {}
				}
				base.SetCriBehavior(cUserId, cUserIdP1, cUserIdLabel, dtCri.Rows[16]);
				cUserId.AutoPostBack = false;
				try {selectedVal = dt.Rows[16]["LastCriteria"].ToString();} catch { selectedVal = null;};
				dv = new DataView((new AdminSystem()).GetIn(54,"GetIn54UserId",(new SqlReportSystem()).CountRptCri("16",LcSysConnString,LcAppPw),"N",base.LImpr,base.LCurr,LcAppConnString,LcAppPw));
				cUserId.DataSource = dv;
				if (true)
				{
					System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
					context["method"] = "54UserId";
					context["addnew"] = "Y";
					context["sp"] = "54UserId";
					context["requiredValid"] = "N";
					context["mKey"] = cUserId.DataValueField;
					context["mVal"] = cUserId.DataTextField;
					context["mTip"] = cUserId.DataTextField;
					context["mImg"] = cUserId.DataTextField;
					context["ssd"] = Request.QueryString["ssd"];
					context["rpt"] = "54";
					context["reportCriId"] = "16";
					context["csy"] = "3";
					context["genPrefix"] = "";
					context["filter"] = "0";
					context["isSys"] = "N";
					context["conn"] = null;
					context["refColCID"] = null;
					context["refCol"] = null;
					cUserId.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
					cUserId.DataContext = context;
					cUserId.Mode = "A";
					cUserId.AutoPostBack = false;
				}
				try
				{
					cUserId.SelectByValue(dt.Rows[16]["LastCriteria"],string.Empty,false);
				}
				catch
				{
					try {cUserId.SelectedIndex = 0;} catch {}
				}
				DataTable dtHlp = GetReportHlp();
				cHelpMsg.HelpTitle = dtHlp.Rows[0]["ReportTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
				cTitleLabel.Text = dtHlp.Rows[0]["ReportTitle"].ToString();
				SetClientRule();
				(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ReportTitle"].ToString(), 0, 54, 0, string.Empty, LcSysConnString, LcAppPw);
				if (bBatchPrint)
				{
					getReport(true, exportTo.VIEW);
					Response.Write("<script lang='javascript'>opener=self;window.close();</script>");
				}
			}
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			if (LcSysConnString == null) { SetSystem(3); }
			this.cViewer.Search += new CrystalDecisions.Web.SearchEventHandler(this.cViewer_Search);
			this.cViewer.ViewZoom += new CrystalDecisions.Web.ZoomEventHandler(this.cViewer_ViewZoom);
			this.cViewer.Navigate += new CrystalDecisions.Web.NavigateEventHandler(this.cViewer_Navigate);
			this.cViewer.Drill += new CrystalDecisions.Web.DrillEventHandler(this.cViewer_Drill);

		}
		#endregion

		protected void cSummary_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((DropDownList)sender).ClientID);
		}

		protected void cFrDate_TextChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
		}

		protected void cToDate_TextChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
		}

		protected void cCompanyId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cProjectId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAgentId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cBrokerId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cVendorId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cCustomerId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cInvestorId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cMemberId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cBorrowerId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cGuarantorId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cLenderId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cUsrGroupId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((DropDownList)sender).ClientID);
		}

		protected void cUserId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!IsPostBack) return;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		private void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
			try
			{
				base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
				DataRow row = base.SystemsList.Rows.Find(SystemId);
				base.CSrc = new CurrSrc(true, row);
				base.CTar = new CurrTar(true, row);
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,0,54,0,LcSysConnString,LcAppPw))
				{
					(new GenReportsSystem()).CreateProgram(string.Empty,54, "Usage Report", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					Response.Redirect(Request.RawUrl);
				}
			}
			catch (Exception e) { PreMsgPopup(e.Message); }
		}

		private void SetButtonHlp()
		{
			DataTable dt;
			dt = (new AdminSystem()).GetButtonHlp(0,54,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["ButtonTypeName"].ToString() == "ClearCri") { cClearCriButton.CssClass = "ButtonImg ClearCriButtonImg"; Session[KEY_bClCriVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cClearCriButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "ShowCri") { cShowCriButton.CssClass = "ButtonImg ShowCriButtonImg"; cShowCriButton.Text = dr["ButtonName"].ToString(); Session[KEY_bShCriVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cShowCriButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "ExpTxt") { cExpTxtButton.CssClass = "ButtonImg ExpTxtButtonImg"; cExpTxtButton.Text = dr["ButtonName"].ToString(); cExpTxtButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpTxtButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "View") { cViewButton.CssClass = "ButtonImg ViewButtonImg"; cViewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cViewButton.Text = dr["ButtonName"].ToString(); cViewButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "ExpPdf") { cExpPdfButton.CssClass = "ButtonImg ExpPdfButtonImg"; cExpPdfButton.Text = dr["ButtonName"].ToString(); cExpPdfButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpPdfButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "ExpDoc") { cExpDocButton.CssClass = "ButtonImg ExpDocButtonImg"; cExpDocButton.Text = dr["ButtonName"].ToString(); cExpDocButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpDocButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "ExpXls") { cExpXlsButton.CssClass = "ButtonImg ExpXlsButtonImg"; cExpXlsButton.Text = dr["ButtonName"].ToString(); cExpXlsButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpXlsButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "Print") { cPrintButton.CssClass = "ButtonImg PrintButtonImg"; cPrintButton.Text = dr["ButtonName"].ToString(); cPrintButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPrintButton.ToolTip = dr["ButtonToolTip"].ToString(); }
				}
			}
		}

		private DataTable GetClientRule()
		{
			DataTable dtRul = (DataTable)Session[KEY_dtClientRule];
			if (dtRul == null)
			{
				CheckAuthentication(false);
				dtRul = (new AdminSystem()).GetClientRule(0,54,base.LUser.CultureId,LcSysConnString,LcAppPw);
				Session[KEY_dtClientRule] = dtRul;
			}
			return dtRul;
		}

		private void SetClientRule()
		{
			DataView dvRul = new DataView(GetClientRule());
			if (dvRul.Count > 0)
			{
				WebControl cc = null;
				string srp = string.Empty;
				string sn = string.Empty;
				string st = string.Empty;
				int ii = 0;
				foreach (DataRowView drv in dvRul)
				{
					srp = drv["ScriptName"].ToString();
					if (drv["ParamName"].ToString() != string.Empty)
					{
						StringBuilder sbName =  new StringBuilder();
						StringBuilder sbType =  new StringBuilder();
						sbName.Append(drv["ParamName"].ToString().Trim());
						sbType.Append(drv["ParamType"].ToString().Trim());
						ii = 0;
						while (sbName.Length > 0)
						{
							sn = Utils.PopFirstWord(sbName,(char)44); st = Utils.PopFirstWord(sbType,(char)44);
							if (st.ToLower() == "combobox") {srp = srp.Replace("@" + ii.ToString() + "@",((RoboCoder.WebControls.ComboBox)this.FindControl(sn)).FocusID);} else {srp = srp.Replace("@" + ii.ToString() + "@",((WebControl)this.FindControl(sn)).ClientID);}
							ii = ii + 1;
						}
					}
					cc = this.FindControl(drv["ColName"].ToString()) as WebControl;
					if (cc != null && (cc.Attributes[drv["ScriptEvent"].ToString()] == null || cc.Attributes[drv["ScriptEvent"].ToString()].IndexOf(srp) < 0)) {cc.Attributes[drv["ScriptEvent"].ToString()] += srp;}
				}
			}
		}

		private DataTable GetReportCriHlp()
		{
			DataTable dtCri = (DataTable)Session[KEY_dtCri];
			if (dtCri == null)
			{
				CheckAuthentication(false);
				dtCri = (new AdminSystem()).GetReportCriHlp(54,base.LUser.CultureId,LcSysConnString,LcAppPw);
				Session[KEY_dtCri] = dtCri;
			}
			return dtCri;
		}

		private DataTable GetReportHlp()
		{
			DataTable dtHlp = (DataTable)Session[KEY_dtReportHlp];
			if (dtHlp == null)
			{
				CheckAuthentication(false);
			    dtHlp = (new AdminSystem()).GetReportHlp(54,base.LUser.CultureId,LcSysConnString,LcAppPw);
				Session[KEY_dtReportHlp] = dtHlp;
			}
			return dtHlp;
		}

		private void CheckAuthentication(bool pageLoad)
		{
			CheckAuthentication(pageLoad, true);
		}

		private DataView GetRptCriteria()
		{
			return ((new SqlReportSystem()).GetReportCriteria(string.Empty,"54",LcSysConnString,LcAppPw)).DefaultView;
		}

		private DsAdmRptUsageIn UpdCriteria(bool bUpdate)
		{
			DsAdmRptUsageIn ds = new DsAdmRptUsageIn();
			DataRow dr = ds.Tables["DtAdmRptUsageIn"].NewRow();
			bool bAll = false; string selectedVal = null; DataView dv = null;int TotalChoiceCnt = 0;int CriCnt=0;bool noneSelected=true;
			dr["Summary"] = base.SetBool(cSummary.Checked);
			if (cSystemId.SelectedIndex >= 0 && cSystemId.SelectedValue != string.Empty) {dr["SystemId"] = cSystemId.SelectedValue;}
			if (IsPostBack && cSystemId.SelectedValue == string.Empty) { throw new ApplicationException("Criteria column: SystemId should not be empty. Please rectify and try again.");};
			if (cFrDate.Text != string.Empty) {dr["FrDate"] = base.SetDateTimeUTC(cFrDate.Text, !bUpdate);}
			if (IsPostBack && cFrDate.Text == string.Empty) { throw new ApplicationException("Criteria column: FrDate should not be empty. Please rectify and try again.");};
			if (cToDate.Text != string.Empty) {dr["ToDate"] = base.SetDateTimeUTC(cToDate.Text, !bUpdate);}
			if (cCompanyId.SelectedIndex >= 0 && cCompanyId.SelectedValue != string.Empty) {dr["CompanyId"] = cCompanyId.SelectedValue;}
			if (cProjectId.SelectedIndex >= 0 && cProjectId.SelectedValue != string.Empty) {dr["ProjectId"] = cProjectId.SelectedValue;}
			if (cAgentId.SelectedIndex >= 0 && cAgentId.SelectedValue != string.Empty) {dr["AgentId"] = cAgentId.SelectedValue;}
			if (cBrokerId.SelectedIndex >= 0 && cBrokerId.SelectedValue != string.Empty) {dr["BrokerId"] = cBrokerId.SelectedValue;}
			if (cVendorId.SelectedIndex >= 0 && cVendorId.SelectedValue != string.Empty) {dr["VendorId"] = cVendorId.SelectedValue;}
			if (cCustomerId.SelectedIndex >= 0 && cCustomerId.SelectedValue != string.Empty) {dr["CustomerId"] = cCustomerId.SelectedValue;}
			if (cInvestorId.SelectedIndex >= 0 && cInvestorId.SelectedValue != string.Empty) {dr["InvestorId"] = cInvestorId.SelectedValue;}
			if (cMemberId.SelectedIndex >= 0 && cMemberId.SelectedValue != string.Empty) {dr["MemberId"] = cMemberId.SelectedValue;}
			if (cBorrowerId.SelectedIndex >= 0 && cBorrowerId.SelectedValue != string.Empty) {dr["BorrowerId"] = cBorrowerId.SelectedValue;}
			if (cGuarantorId.SelectedIndex >= 0 && cGuarantorId.SelectedValue != string.Empty) {dr["GuarantorId"] = cGuarantorId.SelectedValue;}
			if (cLenderId.SelectedIndex >= 0 && cLenderId.SelectedValue != string.Empty) {dr["LenderId"] = cLenderId.SelectedValue;}
			if (cUsrGroupId.SelectedIndex >= 0 && cUsrGroupId.SelectedValue != string.Empty) {dr["UsrGroupId"] = cUsrGroupId.SelectedValue;}
			if (cUserId.SelectedIndex >= 0 && cUserId.SelectedValue != string.Empty) {dr["UserId"] = cUserId.SelectedValue;}
			ds.Tables["DtAdmRptUsageIn"].Rows.Add(dr);
			if (bUpdate) {(new AdminSystem()).UpdRptDt(54,"UpdAdmRptUsage",base.LUser.UsrId,ds,GetRptCriteria(),LcAppConnString,LcAppPw);}
			return ds;
		}

		private DataView GetReportSct()
		{
			DataTable dtSct = (DataTable)Session[KEY_dtReportSct];
			if (dtSct == null)
			{
				CheckAuthentication(false);
				dtSct = (new AdminSystem()).GetReportSct();
				Session[KEY_dtReportSct] = dtSct;
			}
			return dtSct.DefaultView;
		}

		private DataView GetReportItem()
		{
			DataTable dtItem = (DataTable)Session[KEY_dtReportItem];
			if (dtItem == null)
			{
				CheckAuthentication(false);
				dtItem = (new AdminSystem()).GetReportItem(54,LcSysConnString,LcAppPw);
				DataView dvSct = GetReportSct();
				foreach (DataRowView drv in dvSct) {dtItem = MapReportSct(dtItem, drv["ReportSctName"].ToString());}
				Session[KEY_dtReportItem] = dtItem;
			}
			return dtItem.DefaultView;
		}

		// In case formula referencecs not in order.
		private DataTable MapReportSct(DataTable dt, string SctName)
		{
			int ii = 0;
			DataView dv = dt.DefaultView;
			dv.RowFilter = "ReportSctName = '" + SctName + "'";
			foreach (DataRowView drv in dv)
			{
				ii = ii + 1;
				drv["InternalField"] = SctName + ii.ToString();
			}
			return dt;
		}

		private void SetReportSct(DataView dv, string SctName)
		{
			DataView dvCopy = new DataView(dv.Table.Copy());
			CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition ffd;
			CrystalDecisions.CrystalReports.Engine.FieldObject fo;
			dv.RowFilter = "ReportSctName = '" + SctName + "'";
			int iPos1;
			int iPos2;
			string sKey;
			foreach (DataRowView drv in dv)
			{
				ffd = rp.DataDefinition.FormulaFields[drv["InternalField"].ToString()];
				fo = (FieldObject)rp.ReportDefinition.ReportObjects[drv["InternalField"].ToString()];
				if (drv["FormulaReady"].ToString() == "N")
				{
					drv["FormulaReady"] = "Y";
					iPos1 = drv["ItemFormula"].ToString().IndexOf("{@");
					while (iPos1 >= 0)
					{
						iPos2 = drv["ItemFormula"].ToString().IndexOf("}", iPos1 + 1);
						if (iPos2 > iPos1)
						{
							sKey = drv["ItemFormula"].ToString().Substring(iPos1 + 1, iPos2 - iPos1 - 1);
							dvCopy.RowFilter = "ReportItemName = '" + sKey + "'";
							if (dvCopy.Count != 1) { throw new Exception("Referenced Item Issue: Non-unique report item name or referenced item '" + sKey + "' in item formula not found!"); }
							drv["ItemFormula"] = drv["ItemFormula"].ToString().Replace("{" + sKey + "}", "{@" + dvCopy[0]["InternalField"].ToString() + "}");
							iPos1 = drv["ItemFormula"].ToString().IndexOf("{@", iPos2 + 1);
						}
						else {iPos1 = -1;}
					}
				}
				ffd.Text = drv["ItemFormula"].ToString();
				if (drv["FontBold"].ToString() == "Y" && drv["FontItalic"].ToString() == "Y") {fo.ApplyFont(new System.Drawing.Font(drv["FontFamily"].ToString(),float.Parse(drv["FontSize"].ToString()),System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic));}
				else if (drv["FontBold"].ToString() == "Y") {fo.ApplyFont(new System.Drawing.Font(drv["FontFamily"].ToString(),float.Parse(drv["FontSize"].ToString()),System.Drawing.FontStyle.Bold));}
				else if (drv["FontItalic"].ToString() == "Y") {fo.ApplyFont(new System.Drawing.Font(drv["FontFamily"].ToString(),float.Parse(drv["FontSize"].ToString()),System.Drawing.FontStyle.Italic));}
				else {fo.ApplyFont(new System.Drawing.Font(drv["FontFamily"].ToString(),float.Parse(drv["FontSize"].ToString()),System.Drawing.FontStyle.Regular));}
				fo.Left = Int32.Parse(drv["PosLeft"].ToString());
				fo.Top = Int32.Parse(drv["PosTop"].ToString());
				fo.Width = Int32.Parse(drv["PosWidth"].ToString());
				fo.Height = Int32.Parse(drv["PosHeight"].ToString());
				if (drv["Suppress"].ToString() == "Y") {fo.ObjectFormat.EnableSuppress = true;} else {fo.ObjectFormat.EnableSuppress = false;}
				if (drv["Alignment"].ToString() == "C") {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.HorizontalCenterAlign;}
				else if (drv["Alignment"].ToString() == "L") {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.LeftAlign;}
				else {fo.ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.RightAlign;}
				if (drv["LineTop"].ToString() == "S") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine;}
				else if (drv["LineTop"].ToString() == "D") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.DoubleLine;}
				if (drv["LineBottom"].ToString() == "S") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine;}
				else if (drv["LineBottom"].ToString() == "D") {fo.Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.DoubleLine;}
			}
		}

		private void getReport(bool sendToPrinter, exportTo eExport)
		{
			string reportName = "AdmRptUsage";
			cCriteria.Visible = false; cClearCriButton.Visible = false; cShowCriButton.Visible = (bool)Session[KEY_bShCriVisible];
			DataTable dt = (new AdminSystem()).GetRptDt(54,"GetAdmRptUsage",base.LImpr,base.LCurr,UpdCriteria(false),GetRptCriteria(),LcAppConnString,LcAppPw,false,false,false);
			CovertRptUTC(dt);
			if (dt.Rows.Count > 0) {if (dt.Columns.Contains("ReportName")) {reportName = dt.Rows[0]["ReportName"].ToString();}}
			else {PreMsgPopup("For your information, no data is currently available as per your reporting criteria.");}
			if (Config.DeployType == "DEV" && Config.AppNameSpace == "RO")
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt);
				ds.DataSetName = "DsAdmRptUsage";
				ds.Tables[0].TableName = "DtAdmRptUsage";
				string xsdPath = Server.MapPath("~/reports/AdmRptUsageReport.xsd");
				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(xsdPath))
				{
					ds.WriteXmlSchema(writer);
					writer.Close();
				}
			}
			reportName = reportName + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
			DataView dvItem = GetReportItem(); DataView dvSct = GetReportSct();
			foreach (DataRowView drv in dvSct) {SetReportSct(dvItem, drv["ReportSctName"].ToString());}
			rp.Refresh();
			if (sendToPrinter)
			{
				rp.PrintOptions.PrinterName = cPrinter.SelectedItem.Value;
				rp.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
			}
			rp.SetDataSource(dt);
			cViewer.ReportSource = rp;
			if (cViewerWidth.Value != string.Empty) { cViewer.Width = Unit.Pixel(int.Parse(cViewerWidth.Value)); }
			cViewer.Visible = true;
			cViewer.DisplayGroupTree = false;
			if (sendToPrinter) { rp.PrintToPrinter(1,false,0,0); }
			if (eExport == exportTo.TXT)
			{
				StringBuilder sb = new StringBuilder();
				if (dt.Columns.Contains("a.UsageDt")) {sb.Append("a.UsageDt" + (char)9);}
				if (dt.Columns.Contains("a.UsageDt")) {sb.Append("a.UsageDt" + (char)9);}
				if (dt.Columns.Contains("Summary")) {sb.Append("Summary" + (char)9);}
				if (dt.Columns.Contains("SystemName")) {sb.Append("System Name" + (char)9);}
				if (dt.Columns.Contains("UsrTypeGrp")) {sb.Append("Type Group" + (char)9);}
				if (dt.Columns.Contains("UsrTypeName")) {sb.Append("Type Name" + (char)9);}
				if (dt.Columns.Contains("EntityTitle")) {sb.Append("Entity Title" + (char)9);}
				if (dt.Columns.Contains("UsrGroupName")) {sb.Append("UsrGroupName" + (char)9);}
				if (dt.Columns.Contains("UsrName")) {sb.Append("UsrName" + (char)9);}
				if (dt.Columns.Contains("UsageDt")) {sb.Append("UsageDt" + (char)9);}
				if (dt.Columns.Contains("UsageNote")) {sb.Append("Usage Note" + (char)9);}
				if (dt.Columns.Contains("NumTimes")) {sb.Append("# of Times" + (char)9);}
				sb.Append(Environment.NewLine);
				DataView dv = new DataView(dt);
				foreach (DataRowView drv in dv)
				{
					if (dt.Columns.Contains("a.UsageDt")) {sb.Append(drv["a.UsageDt"].ToString() + (char)9);}
					if (dt.Columns.Contains("a.UsageDt")) {sb.Append(drv["a.UsageDt"].ToString() + (char)9);}
					if (dt.Columns.Contains("Summary")) {sb.Append(drv["Summary"].ToString() + (char)9);}
					if (dt.Columns.Contains("SystemName")) {sb.Append(drv["SystemName"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsrTypeGrp")) {sb.Append(drv["UsrTypeGrp"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsrTypeName")) {sb.Append(drv["UsrTypeName"].ToString() + (char)9);}
					if (dt.Columns.Contains("EntityTitle")) {sb.Append(drv["EntityTitle"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsrGroupName")) {sb.Append(drv["UsrGroupName"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsrName")) {sb.Append(drv["UsrName"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsageDt")) {sb.Append(drv["UsageDt"].ToString() + (char)9);}
					if (dt.Columns.Contains("UsageNote")) {sb.Append(drv["UsageNote"].ToString() + (char)9);}
					if (dt.Columns.Contains("NumTimes")) {sb.Append(drv["NumTimes"].ToString() + (char)9);}
					sb.Append(Environment.NewLine);
				}
				ExportToStream(null, reportName + ".xls", sb.Replace("\r\n","\n"), exportTo.TXT);
			}
			else if (eExport == exportTo.XLS)
			{
				ExportToStream(rp, reportName + ".xls", null, exportTo.XLS);
			}
			else if (eExport == exportTo.PDF)
			{
				ExportToStream(rp, reportName + ".pdf", null, exportTo.PDF);
			}
			else if (eExport == exportTo.DOC)
			{
				ExportToStream(rp, reportName + ".doc", null, exportTo.DOC);
			}
		}

		private void ExportToStream(object oReport, string sFileName, StringBuilder sb, exportTo eExport)
		{
			System.IO.Stream oStream =  null;
			StreamWriter sw = null;
			ExportOptions oOptions = new ExportOptions();
			ExportRequestContext oRequest = new ExportRequestContext();
			Response.Buffer = true;
			Response.ClearHeaders();
			Response.ClearContent();
			if (eExport == exportTo.TXT)
			{
				oStream = new MemoryStream();
				sw = new StreamWriter(oStream,System.Text.Encoding.Default);
				sw.WriteLine(sb);
				sw.Flush();
				oStream.Seek(0,SeekOrigin.Begin);
				Response.ContentType = "application/vnd.ms-excel";
			}
			else if (eExport == exportTo.XLS)
			{
				oOptions.ExportFormatType = ExportFormatType.Excel;
				oOptions.FormatOptions = new ExcelFormatOptions();
				oRequest.ExportInfo = oOptions;
				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.Excel);
				Response.ContentType = "application/vnd.ms-excel";
			}
			else if (eExport == exportTo.PDF)
			{
				oOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
				oOptions.FormatOptions = new PdfRtfWordFormatOptions();
				oRequest.ExportInfo = oOptions;
				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.PortableDocFormat);
				Response.ContentType = "application/pdf";
			}
			else if (eExport == exportTo.DOC)
			{
				oOptions.ExportFormatType = ExportFormatType.WordForWindows;
				oOptions.FormatOptions = new PdfRtfWordFormatOptions();
				oRequest.ExportInfo = oOptions;
				oStream = ((ReportDocument)oReport).ExportToStream(ExportFormatType.WordForWindows);
				Response.ContentType = "application/msword";
			}
			Response.AppendHeader("Content-Disposition", "Attachment; Filename=\"" + sFileName.Replace(" ","_") + "\"");
			byte[] streamByte = new byte[oStream.Length];
			oStream.Read(streamByte, 0, (int)oStream.Length);
			Response.BinaryWrite(streamByte);
			Response.End();
			if (oStream != null) {oStream.Close();}
			if (sw != null) {sw.Close();}
		}

		public void cShowCriButton_Click(object sender, System.EventArgs e)
		{
			cCriteria.Visible = true; cShowCriButton.Visible = false;
			cClearCriButton.Visible = (bool)Session[KEY_bClCriVisible];
			cViewer.Visible = false;
		}

		public void cClearCriButton_Click(object sender, System.EventArgs e)
		{
			cSummary.Checked = false;
			if (cSystemId.Items.Count > 0) {cSystemId.SelectedIndex = 0;}
			cFrDate.Text = "0";
			cToDate.Text = "";
			if (cCompanyId.Items.Count > 0) {cCompanyId.DataSource = cCompanyId.DataSource; cCompanyId.SelectByValue(cCompanyId.Items[0].Value,string.Empty,true);}
			if (cProjectId.Items.Count > 0) {cProjectId.DataSource = cProjectId.DataSource; cProjectId.SelectByValue(cProjectId.Items[0].Value,string.Empty,true);}
			if (cAgentId.Items.Count > 0) {cAgentId.DataSource = cAgentId.DataSource; cAgentId.SelectByValue(cAgentId.Items[0].Value,string.Empty,true);}
			if (cBrokerId.Items.Count > 0) {cBrokerId.DataSource = cBrokerId.DataSource; cBrokerId.SelectByValue(cBrokerId.Items[0].Value,string.Empty,true);}
			if (cVendorId.Items.Count > 0) {cVendorId.DataSource = cVendorId.DataSource; cVendorId.SelectByValue(cVendorId.Items[0].Value,string.Empty,true);}
			if (cCustomerId.Items.Count > 0) {cCustomerId.DataSource = cCustomerId.DataSource; cCustomerId.SelectByValue(cCustomerId.Items[0].Value,string.Empty,true);}
			if (cInvestorId.Items.Count > 0) {cInvestorId.DataSource = cInvestorId.DataSource; cInvestorId.SelectByValue(cInvestorId.Items[0].Value,string.Empty,true);}
			if (cMemberId.Items.Count > 0) {cMemberId.DataSource = cMemberId.DataSource; cMemberId.SelectByValue(cMemberId.Items[0].Value,string.Empty,true);}
			if (cBorrowerId.Items.Count > 0) {cBorrowerId.DataSource = cBorrowerId.DataSource; cBorrowerId.SelectByValue(cBorrowerId.Items[0].Value,string.Empty,true);}
			if (cGuarantorId.Items.Count > 0) {cGuarantorId.DataSource = cGuarantorId.DataSource; cGuarantorId.SelectByValue(cGuarantorId.Items[0].Value,string.Empty,true);}
			if (cLenderId.Items.Count > 0) {cLenderId.DataSource = cLenderId.DataSource; cLenderId.SelectByValue(cLenderId.Items[0].Value,string.Empty,true);}
			if (cUsrGroupId.Items.Count > 0) {cUsrGroupId.SelectedIndex = 0;}
			if (cUserId.Items.Count > 0) {cUserId.DataSource = cUserId.DataSource; cUserId.SelectByValue(cUserId.Items[0].Value,string.Empty,true);}
		}

		public void cExpTxtButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(false, exportTo.TXT);
		}

		public void cViewButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(false, exportTo.VIEW);
		}

		public void cExpXlsButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(false, exportTo.XLS);
		}

		public void cExpPdfButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(false, exportTo.PDF);
		}

		public void cExpDocButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(false, exportTo.DOC);
		}

		public void cPrintImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			cPrintButton_Click(sender, new EventArgs());
		}

		public void cPrintButton_Click(object sender, System.EventArgs e)
		{
			UpdCriteria(true);
			getReport(true, exportTo.VIEW);
		}

		private void cViewer_Search(object source, CrystalDecisions.Web.SearchEventArgs e)
		{
			getReport(false, exportTo.VIEW);
		}

		private void cViewer_ViewZoom(object source, CrystalDecisions.Web.ZoomEventArgs e)
		{
			getReport(false, exportTo.VIEW);
		}


		private void cViewer_Drill(object source, CrystalDecisions.Web.DrillEventArgs e)
		{
			getReport(false, exportTo.VIEW);
		}

		private void cViewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
		{
			getReport(false, exportTo.VIEW);
		}

		private void PreMsgPopup(string msg)
		{
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/info.gif";
		    string focusOnCloseId = string.Empty;
		    string msgContent = ReformatErrMsg(msg);
		    if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
			string script =
			@"<script type='text/javascript' lang='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
		}
	}
}
