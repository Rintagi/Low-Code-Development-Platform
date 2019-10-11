using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Security.Principal;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace RO.Web
{
	public partial class SqlReportModule : RO.Web.ModuleBase
	{
		private string KEY_dtPublicCri;
		private string KEY_dtPublicFld;
		private string KEY_sQueryStr;
		private string KEY_dtPrinters;
		private string KEY_dtReportHlp;
		private string KEY_dtCriHlp;
		private string KEY_dtClientRule;
        private string KEY_bViewVisible;
        private string KEY_bExpPdfVisible;
        private string KEY_bExpDocVisible;
        private string KEY_bExpXlsVisible;
        private string KEY_bPrintVisible;
		private string KEY_bClCriVisible;
		private string KEY_bShCriVisible;
		private string KEY_bCsMizVisible;
		private string KEY_dtSqlCri;
		private string KEY_dtSqlObj;
		private string KEY_dtSqlGrp;
		private string KEY_dtMemCriDdl;
		private string KEY_dtMemFldDdl;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppUsrId;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
        private byte LcSystemId;
		private string GenPrefix;

		public SqlReportModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                /* Stop IIS from Caching but allowing export to Excel to work */
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));
                Response.Cache.SetValidUntilExpires(true);
                Session.Remove(KEY_sQueryStr);
			}
			// Need these on every postback to take care of rpt:
			KEY_dtPrinters = "Cache:dtPrinters" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtReportHlp = "Cache:dtReportHlp" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtCriHlp = "Cache:dtCriHlp" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtClientRule = "Cache:dtClientRule" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_bViewVisible = "Cache:bViewVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
            KEY_bExpPdfVisible = "Cache:bExpPdfVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
            KEY_bExpDocVisible = "Cache:bExpDocVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
            KEY_bExpXlsVisible = "Cache:bExpXlsVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
            KEY_bPrintVisible = "Cache:bPrintVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_bClCriVisible = "Cache:bClCriVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_bShCriVisible = "Cache:bShCriVisible2" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_bCsMizVisible = "Cache:bCsMizVisible" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtSqlCri = "Cache:dtSqlCri" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtSqlObj = "Cache:dtSqlObj" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtSqlGrp = "Cache:dtSqlGrp" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtMemCriDdl = "Cache:dtMemCriDdl" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtPublicCri = "Cache:dtPublicCri" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtMemFldDdl = "Cache:dtMemFldDdl" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			KEY_dtPublicFld = "Cache:dtPublicFld" + LcSystemId.ToString() + "_" + QueryStr["rpt"].ToString();
			if (!IsPostBack)
			{
				Session.Remove(KEY_dtSqlCri); Session.Remove(KEY_dtSqlGrp); SetSelHolder();
			}
			int ii = 0; DataView dvCri = GetSqlCriteria(); DataView dvGrp = GetCriReportGrp(); SetCriHolder(ii, dvCri, dvGrp);
//				CheckAuthentication(true);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
            string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "datepicker_i18n", "scripts/i18n/jquery.ui.datepicker-" + lang + ".js");
            if (!IsPostBack)
            {
                DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, byte.Parse(QueryStr["csy"].ToString()), base.LImpr, LcSysConnString, LcAppPw, null, Int32.Parse(QueryStr["rpt"].ToString()), null);
                if (dtMenuAccess.Rows.Count == 0 && Request.QueryString["gen"] != "Y")
                {
                    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
                    throw new Exception(message);
                }
                Session.Remove(KEY_dtReportHlp);
                Session.Remove(KEY_dtCriHlp);
                Session.Remove(KEY_dtClientRule);
                Session.Remove(KEY_dtSqlObj);
                Session.Remove(KEY_dtMemCriDdl);
                Session.Remove(KEY_dtPublicCri);
                Session.Remove(KEY_dtMemFldDdl);
                Session.Remove(KEY_dtPublicFld);
                cMemFldDdl_SelectedIndexChanged(sender, EventArgs.Empty);
                SetButtonHlp();
                if ((bool)Session[KEY_bClCriVisible]) { cClearCriButton.Visible = cCriteria.Visible; } else { cClearCriButton.Visible = false; }
                if ((bool)Session[KEY_bShCriVisible]) { cShowCriButton.Visible = !cCriteria.Visible; } else { cShowCriButton.Visible = false; }
                if ((bool)Session[KEY_bCsMizVisible]) { cCustomizeButton.Visible = cCriteria.Visible; } else { cCustomizeButton.Visible = false; }
                SetCriteria((new SqlReportSystem()).GetRptCriteria(byte.Parse(dvCri.Count.ToString()), GenPrefix, Int32.Parse(QueryStr["rpt"].ToString()), base.LUser.UsrId, LcSysConnString, LcAppPw), dvCri);
                DataTable dtHlp = GetReportHlp();
                cHelpMsg.HelpTitle = dtHlp.Rows[0]["ReportTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
                cTitleLabel.Text = dtHlp.Rows[0]["ReportTitle"].ToString();
                SetClientRule();
                new AdminSystem().LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ReportTitle"].ToString(), 0, Int32.Parse(QueryStr["rpt"].ToString()), 0, string.Empty, LcSysConnString, LcAppPw);
                if (QueryStr["key"] != null && QueryStr["key"].ToString() != string.Empty)
                {
                    SetPublicCri(cPublicCri, string.Empty);
                    SetMemCriDdl(cMemCriDdl, QueryStr["key"].ToString(), true);
                    cMemCriDdl.SetDdVisible();
                    cMemCriDdl_SelectedIndexChanged(sender, EventArgs.Empty);
                    SetPublicFld(cPublicFld, string.Empty);
                }
                else
                {
                    SetMemCriDdl(cMemCriDdl, string.Empty, true);
                }
                // Allow criteria values to be passed (act=Y) as "cri??" where ?? is the numeric order for the crietria expected:
                bool bPopCri = false;
                DataTable dtPopCri = new DataTable();
                dtPopCri.Columns.Add(new DataColumn("LastCriteria", typeof(string)));
                int jj = 0;	//Popular Criteria if launched directly
                foreach (DataRowView drv in dvCri)
                {
                    DataRow dr = dtPopCri.NewRow();
                    if (Request.QueryString["Cri" + jj.ToString()] != null)
                    {
                        dr["LastCriteria"] = Request.QueryString["Cri" + jj.ToString()].ToString();
                        bPopCri = true;
                    }
                    dtPopCri.Rows.Add(dr);
                    jj = jj + 1;

                }
                if (bPopCri) SetCriteria(dtPopCri, dvCri);
                cBanPanel.Visible = true; cViewer.ShowToolBar = true;
                if (QueryStr["act"] != null)
                {
                    if (QueryStr["act"].ToString() == "Y") // PDF
                    {
                        cExpPdfButton_Click(sender, new EventArgs());
                    }
                    else if (QueryStr["act"].ToString() == "D") // Dashboard i.e. no banner etc.
                    {
                        cViewButton_Click(sender, new EventArgs());
                        cBanPanel.Visible = false;
                    }
                    else if (QueryStr["act"].ToString() == "V") // simple viewing may still have the title/banner/button
                    {
                        cViewButton_Click(sender, new EventArgs());
                    }
                    cMemPanel.Visible = false;
                }
                // Temporary disabled: 2012.02.08
                //else { cMemPanel.Visible = true; }
                if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "E" || GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "R")
                {
                    cExpPdfButton.Visible = false;
                    cPrintButton.Visible = false;
                    cPrinter.Visible = false;
                    cViewButton.Visible = false;
                    if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "E")
                    {
                        cExpDocButton.Visible = false;
                    }
                    else
                    {
                        cExpXlsButton.Visible = false;
                    }
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
            if ((Request.QueryString["csy"] ?? "").ToString() == string.Empty)
            {
                throw new Exception("Please make sure QueryString has 'csy=' followed by the SystemId and try again.");
            }
            if ((Request.QueryString["rpt"] ?? "").ToString() == string.Empty)
            {
                throw new Exception("Please make sure QueryString has 'rpt=' followed by the ReportId and try again.");
            }
            byte SystemId = byte.Parse(Request.QueryString["csy"].ToString().Split(new char[] { ',' }).First());
            if (base.SystemsList == null) this.SystemsDict = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
            DataTable dt = (new SqlReportSystem()).GetAllowSelect(Request.QueryString["rpt"] ?? "", base.SysConnectStr(SystemId), base.AppPwd(SystemId));
            if (IsSelfInvoked())
            {
                ImpersonateLogin();
            }
            else
            {
                CheckAuthentication(true, dt.Rows.Count == 0 || dt.Rows[0]["AuthRequired"].ToString() == "Y");
            }
            if (base.LUser != null)		// Must do this on every postback.
			{
                if (Request.QueryString["gen"] != null && Request.QueryString["gen"] == "Y") { GenPrefix = "Ut"; } else { GenPrefix = string.Empty; }
                if (Request.QueryString["csy"] == null || Request.QueryString["csy"].ToString() == string.Empty)	// System selected.
				{
					if (Request.Path.ToLower().Contains("default.aspx"))
					{
						SetSystem(base.LCurr.SystemId);
					}
					else
					{
						throw new Exception("Please make sure QueryString has 'csy=' followed by the SystemId and try again.");
					}
				}
				else
				{
                    //base.LCurr.SystemId = byte.Parse(Request.QueryString["csy"].ToString().Split(new char[] { ',' }).First()); 
                    SetSystem(SystemId);
				}
			    if (!string.IsNullOrEmpty(Request.QueryString["rpt"]))
			    {
				    KEY_sQueryStr = "Cache:sQueryStr" + LcSystemId.ToString() + "_" + Request.QueryString["rpt"].ToString();
			    }
			    else
			    {
				    KEY_sQueryStr = "Cache:sQueryStr" + LcSystemId.ToString();
			    }
            }
        }
		#endregion

		private NameValueCollection QueryStr
		{
			get
			{
				NameValueCollection nvc = (NameValueCollection)Session[KEY_sQueryStr];
				if (LUser == null) {return new NameValueCollection();}
				if (nvc == null)
				{
                    // For dashboard catalog only:
                    //string tt = string.Empty; ;
                    //Control cc = cBanPanel.Parent.NamingContainer;
                    //if (cc.GetType().Name == "GenericWebPart")
                    //{
                    //    tt = ((GenericWebPart)cc).TitleUrl;	// Only available at dashboard runtime.
                    //}
                    //string qs = (new AdminSystem()).GetQueryStr(tt, LcSysConnString, LcAppPw);
                    //if (qs != null && qs != string.Empty)
                    //{
                    //    nvc = new NameValueCollection();
                    //    string[] arr = qs.Split('&');
                    //    foreach (string str in arr)
                    //    {
                    //        string[] key = str.Split('=');
                    //        if (key.Length == 2) { nvc.Add(key[0], key[1]); }
                    //    }
                    //    ((GenericWebPart)cc).TitleUrl = string.Empty;   // Prevent Title being displayed as hyperlink.
                    //}
                    //else
                    //{
						nvc = Request.QueryString;
					//}
                    if (!string.IsNullOrEmpty(KEY_sQueryStr)) { Session[KEY_sQueryStr] = nvc; }
                }
				return nvc;
			}
		}

		protected void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcDesDb = base.DesDb(SystemId);
			LcAppDb = base.AppDb(SystemId);
			LcAppUsrId = base.AppUsrId(SystemId);
			LcAppPw = base.AppPwd(SystemId);
            LcSystemId = SystemId;
            try
            {
                base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
                DataRow row = base.SystemsList.Rows.Find(SystemId);
                base.CSrc = new CurrSrc(true, row);
                base.CTar = new CurrTar(true, row);
                if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && !string.IsNullOrEmpty(Request.QueryString["rpt"]) && (new AdminSystem()).IsRegenNeeded(string.Empty, 0, Int32.Parse(Request.QueryString["rpt"].ToString()), 0, LcSysConnString, LcAppPw))
                {
                    (new GenReportsSystem()).CreateProgram(string.Empty, Int32.Parse(Request.QueryString["rpt"].ToString()), GetReportHlp().Rows[0]["ReportTitle"].ToString(), row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception e) { throw new ApplicationException(e.Message); }
        }

        protected void cCriButton_Click(object sender, System.EventArgs e)
        {
            if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetSqlCriteria());
        }
        private void ReBindCriteria(DataView dvCri)
        {
            ListBox cListBox;
            RoboCoder.WebControls.ComboBox cComboBox;
            DropDownList cDropDownList;
            RadioButtonList cRadioButtonList;

            foreach (DataRowView drv in dvCri)
            {
                if (string.IsNullOrEmpty(drv["DdlFtrColumnId"].ToString())) continue;

                if (drv["DisplayName"].ToString() == "ComboBox")
                {
                    cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    var val = cComboBox.SelectedValue;
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, val, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cComboBox.DataSource = dv;
                    try { cComboBox.SelectByValue(val, string.Empty, false); }
                    catch { try { cComboBox.SelectedIndex = 0; } catch { } }
                }
                else if (drv["DisplayName"].ToString() == "DropDownList")
                {
                    cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    var val = cDropDownList.SelectedValue;
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cDropDownList.DataSource = dv;
                    cDropDownList.DataBind();
                    try { cDropDownList.Items.FindByValue(val).Selected = true; }
                    catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
                }
                else if (drv["DisplayName"].ToString() == "ListBox")
                {
                    cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
                    TextBox cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Hidden");
                    if (drv["DisplayMode"].ToString() == "AutoListBox" && cTextBox != null) selectedValues = cTextBox.Text;
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), drv["DisplayMode"].ToString() != "AutoComplete", selectedValues, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cListBox.DataSource = dv;
                    cListBox.DataBind();
                    GetSelectedItems(cListBox, selectedValues);
                }
                else if (drv["DisplayName"].ToString() == "RadioButtonList")
                {
                    cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    string val = cRadioButtonList.SelectedValue;
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cRadioButtonList.DataSource = dv;
                    cRadioButtonList.DataBind();
                    try { cRadioButtonList.Items.FindByValue(val).Selected = true; }
                    catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
                }
            }

        }
		private void SetCriteria(DataTable dt, DataView dvCri)
		{
			Label cLabel;
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataTable dtCriHlp = GetReportCriHlp();
			int ii = 0;

			foreach (DataRowView drv in dvCri)
			{
				cLabel = (Label)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Label");
				if (drv["DisplayName"].ToString() == "ComboBox")
				{
					cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    string val = null; try { val = dt.Rows[ii]["LastCriteria"].ToString(); }
                    catch { };
                    base.SetCriBehavior(cComboBox, null, cLabel, dtCriHlp.Rows[ii]);
					(new SqlReportSystem()).MkReportGetIn(GenPrefix, drv["ReportCriId"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), LcAppDb, LcDesDb, LcSysConnString, LcAppPw);
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, val, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cComboBox.DataSource = dv;
					try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); }
					catch { try { cComboBox.SelectedIndex = 0; } catch { } }
				}
				else if (drv["DisplayName"].ToString() == "DropDownList")
				{
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii]);
					(new SqlReportSystem()).MkReportGetIn(GenPrefix, drv["ReportCriId"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), LcAppDb, LcDesDb, LcSysConnString, LcAppPw);
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cDropDownList.DataSource = dv;
					cDropDownList.DataBind();
					try { cDropDownList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; }
					catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
				}
				else if (drv["DisplayName"].ToString() == "ListBox")
				{
					cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    string val = null; try { val = dt.Rows[ii]["LastCriteria"].ToString(); }
                    catch { };
                    base.SetCriBehavior(cListBox, null, cLabel, dtCriHlp.Rows[ii]);
                    (new SqlReportSystem()).MkReportGetIn(GenPrefix, drv["ReportCriId"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), LcAppDb, LcDesDb, LcSysConnString, LcAppPw);
                    //cListBox.DataSource = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cListBox.DataSource = dv;
                    cListBox.DataBind();
					GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
				}
				else if (drv["DisplayName"].ToString() == "RadioButtonList")
				{
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii]);
					(new SqlReportSystem()).MkReportGetIn(GenPrefix, drv["ReportCriId"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), LcAppDb, LcDesDb, LcSysConnString, LcAppPw);
                    DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                    FilterCriteriaDdl(cCriteria, dv, drv);
                    cRadioButtonList.DataSource = dv;
					cRadioButtonList.DataBind();
					try { cRadioButtonList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; }
					catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
				}
				else if (drv["DisplayName"].ToString() == "Calendar")
				{
					cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					base.SetCriBehavior(cCalendar, null, cLabel, dtCriHlp.Rows[ii]);
					if (dt.Rows[ii]["LastCriteria"].ToString() == string.Empty)
					{
						cCalendar.SelectedDates.Clear();
					}
					else
					{ 
						cCalendar.SelectedDate = DateTime.Parse(dt.Rows[ii]["LastCriteria"].ToString()); cCalendar.VisibleDate = cCalendar.SelectedDate; 
					}
				}
				else if (drv["DisplayName"].ToString() == "CheckBox")
				{
					cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					base.SetCriBehavior(cCheckBox, null, cLabel, dtCriHlp.Rows[ii]);
					if (dt.Rows[ii]["LastCriteria"].ToString() != string.Empty) { cCheckBox.Checked = base.GetBool(dt.Rows[ii]["LastCriteria"].ToString()); }
				}
				else
				{
					cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					base.SetCriBehavior(cTextBox, null, cLabel, dtCriHlp.Rows[ii]);
					cTextBox.Text = dt.Rows[ii]["LastCriteria"].ToString(); 
				}
				ii = ii + 1;
			}
		}

		private void GetSelectedItems(ListBox cObj, string selectedItems)
		{
			string selectedItem;
			bool finish;
			if (selectedItems == string.Empty) { try {cObj.SelectedIndex = 0;} catch {} }
			else
			{
				finish = false;
				while (!finish)
				{
					selectedItem = GetSelectedItem(ref selectedItems);
					if (selectedItem == string.Empty) { finish = true; }
					else
					{
						try { cObj.Items.FindByValue(selectedItem).Selected = true; }
						catch { finish = true; try {cObj.SelectedIndex = 0;} catch {} }
					}
				}
			}
		}

		private string GetSelectedItem(ref string selectedItems)
		{
			string selectedItem;
			int sIndex = selectedItems.IndexOf("'");
			int eIndex = selectedItems.IndexOf("'", sIndex + 1);
			if (sIndex >= 0 && eIndex >= 0)
			{
				selectedItem = selectedItems.Substring(sIndex + 1, eIndex - sIndex - 1);
			}
			else
			{
				selectedItem = string.Empty;
			}
			selectedItems = selectedItems.Substring(eIndex + 1, selectedItems.Length - eIndex - 1);
			return selectedItem;
		}

		private void SetButtonHlp()
		{
			DataView dv = new DataView((new AdminSystem()).GetButtonHlp(0, Int32.Parse(QueryStr["rpt"].ToString()), 0, base.LUser.CultureId, LcSysConnString, LcAppPw));
			if (dv != null && dv.Count > 0)
			{
				foreach (DataRowView drv in dv)
				{
                    if (drv["ButtonTypeName"].ToString() == "ClearCri") { cClearCriButton.CssClass = "ButtonImg ClearCriButtonImg"; Session[KEY_bClCriVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cClearCriButton.ToolTip = drv["ButtonToolTip"].ToString(); }
                    if (drv["ButtonTypeName"].ToString() == "ShowCri") { cShowCriButton.CssClass = "ButtonImg ShowCriButtonImg"; Session[KEY_bShCriVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cShowCriButton.ToolTip = drv["ButtonToolTip"].ToString(); cShowCriButton.Text = drv["ButtonName"].ToString(); }
					if (drv["ButtonTypeName"].ToString() == "Customize")
					{
                        if (string.IsNullOrEmpty(GenPrefix)) { Session[KEY_bCsMizVisible] = false; }
                        else
                        {
                            string wostr = "'AdmRptWiz.aspx?csy=" + LcSystemId.ToString() + "&sys=" + LCurr.DbId.ToString() + "&typ=N&frm=aspnetForm&key=" + (new SqlReportSystem()).GetRptWizId(QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw) + "','CtrlAdmRptWiz'";
                            string js = "window.open(" + wostr + ",'resizable=yes,scrollbars=yes,status=yes,width=700,height=500'); return false;";
                            cCustomizeButton.CssClass = "ButtonImg CustomizeButtonImg"; cCustomizeButton.Text = drv["ButtonName"].ToString(); cCustomizeButton.ToolTip = drv["ButtonToolTip"].ToString(); Session[KEY_bCsMizVisible] = base.GetBool(drv["ButtonVisible"].ToString());
                            cCustomizeButton.Attributes["onclick"] = js;
                        }
					}
                    if (drv["ButtonTypeName"].ToString() == "View")
                    {
                        if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T")
                        {
                            cViewButton.Visible = false; Session[KEY_bViewVisible] = false;
                        }
                        else
                        {
                            cViewButton.CssClass = "ButtonImg ViewButtonImg"; cViewButton.Text = drv["ButtonName"].ToString();
                            cViewButton.Visible = base.GetBool(drv["ButtonVisible"].ToString()); Session[KEY_bViewVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cViewButton.ToolTip = drv["ButtonToolTip"].ToString();
                        }
                    }
                    if (drv["ButtonTypeName"].ToString() == "ExpPdf")
                    {
                        if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T")
                        {
                            cExpPdfButton.Visible = false; Session[KEY_bExpPdfVisible] = false;
                        }
                        else
                        {
                            cExpPdfButton.CssClass = "ButtonImg ExpPdfButtonImg"; cExpPdfButton.Text = drv["ButtonName"].ToString();
                            cExpPdfButton.Visible = base.GetBool(drv["ButtonVisible"].ToString()); Session[KEY_bExpPdfVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cExpPdfButton.ToolTip = drv["ButtonToolTip"].ToString();
                        }
                    }
                    if (drv["ButtonTypeName"].ToString() == "ExpDoc")
                    {
                        if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T")
                        {
                            cExpDocButton.Visible = false; Session[KEY_bExpDocVisible] = false;
                        }
                        else
                        {
                            cExpDocButton.CssClass = "ButtonImg ExpDocButtonImg"; cExpDocButton.Text = drv["ButtonName"].ToString();
                            cExpDocButton.Visible = base.GetBool(drv["ButtonVisible"].ToString()); Session[KEY_bExpDocVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cExpDocButton.ToolTip = drv["ButtonToolTip"].ToString();
                        }
                    }
                    if (drv["ButtonTypeName"].ToString() == "ExpXls")
                    {
                        if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T")
                        {
                            cExpXlsButton.Visible = false; Session[KEY_bExpXlsVisible] = false;
                        }
                        else
                        {
                            cExpXlsButton.CssClass = "ButtonImg ExpXlsButtonImg"; cExpXlsButton.Text = drv["ButtonName"].ToString();
                            cExpXlsButton.Visible = base.GetBool(drv["ButtonVisible"].ToString()); Session[KEY_bExpXlsVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cExpXlsButton.ToolTip = drv["ButtonToolTip"].ToString();
                        }
                    }
                    if (drv["ButtonTypeName"].ToString() == "Print")
                    {
                        if ("S,R,B".IndexOf(GetReportHlp().Rows[0]["ReportTypeCd"].ToString()) >= 0)
                        {
                            cPrinter.Visible = false; cPrintButton.Visible = false; Session[KEY_bPrintVisible] = false;
                        }
                        else
                        {
                            cPrinter.Visible = true; SetPrinters();
                            cPrintButton.CssClass = "ButtonImg PrintButtonImg"; cPrintButton.Text = drv["ButtonName"].ToString();
                            cPrintButton.Visible = base.GetBool(drv["ButtonVisible"].ToString()); Session[KEY_bPrintVisible] = base.GetBool(drv["ButtonVisible"].ToString()); cPrintButton.ToolTip = drv["ButtonToolTip"].ToString();
                        }
                    }
				}
			}
		}

		private void SetPrinters()
		{
			if (base.LUser.InternalUsr == "N" && GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T")
			{
				throw new ApplicationException("This type of document (.txt) is reserved for internal user only.");
			}
			else
			{
				Session.Remove(KEY_dtPrinters);
				DataTable dtPrinters = (new AdminSystem()).GetPrinterList(base.LImpr.UsrGroups, base.LImpr.Members);
				if (dtPrinters != null && dtPrinters.Rows.Count > 0)
				{
					cPrinter.DataSource = dtPrinters;
					cPrinter.DataBind();
					cPrinter.SelectedIndex = 0;
					Session[KEY_dtPrinters] = dtPrinters;
				}
			}
		}

		private void MakeSqlGrpLab(DataRowView drv)
		{
            Literal cLiteral = new Literal();
            string sLabelCss = drv["LabelCss"].ToString();
            if (sLabelCss != string.Empty)
            {
                if (sLabelCss.StartsWith(".")) { cLiteral.Text = "<div class=\"" + sLabelCss.Substring(1) + "\">"; } else { cLiteral.Text = "<div class=\"r-labelL\" style=\"" + sLabelCss + "\">"; }
            }
            else { cLiteral.Text = "<div class=\"r-labelL\">"; }
            cCriteria.Controls.Add(cLiteral);
            Label cLabel = new Label(); cLabel.ID = "x" + drv["ColumnName"].ToString() + "Label"; cLabel.CssClass = "inp-lbl"; cCriteria.Controls.Add(cLabel);
            cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		}

		private void MakeSqlGrpInp(DataRowView drv)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			//AjaxControlToolkit.CalendarExtender cCalendarExtender;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			//AjaxControlToolkit.Rating cRating;
			TextBox cTextBox;
			Literal cLiteral = new Literal();
            DataView dvDependent = new DataView(drv.Row.Table, "DdlFtrColumnId = " + drv["ReportCriId"].ToString() + " AND DisplayMode <> 'AutoComplete' AND DisplayName in ('ComboBox','DropDownList','ListBox','RadioButtonList')", null, DataViewRowState.CurrentRows);
			string sContentCss = drv["ContentCss"].ToString();
            if (sContentCss != string.Empty)
            {
                if (sContentCss.StartsWith(".")) { cLiteral.Text = "<div class=\"" + sContentCss.Substring(1) + "\">"; } else { cLiteral.Text = "<div class=\"r-content\" style=\"" + sContentCss + "\">"; }
            }
            else { cLiteral.Text = "<div class=\"r-content\">"; }
            cCriteria.Controls.Add(cLiteral);
			if (drv["DisplayName"].ToString() == "ComboBox")
			{
				cComboBox = new RoboCoder.WebControls.ComboBox(); cComboBox.ID = "x" + drv["ColumnName"].ToString(); cComboBox.CssClass = "inp-ddl";
                cComboBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cComboBox.DataTextField = drv["DdlRefColumnName"].ToString(); cComboBox.AutoPostBack = dvDependent.Count > 0; cComboBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
                if (drv["DisplayMode"].ToString() == "AutoComplete")
                {
                    WebControl wcFtr = (WebControl) cCriteria.FindControl("x"+drv["DdlFtrColumnName"].ToString());

                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    context["method"] = QueryStr["rpt"].ToString() + drv["ColumnName"].ToString();
                    context["addnew"] = "Y";
                    context["sp"] = QueryStr["rpt"].ToString() + drv["ColumnName"].ToString();
                    context["requiredValid"] = drv["RequiredValid"].ToString();
                    context["mKey"] = drv["DdlKeyColumnName"].ToString();
                    context["mVal"] = drv["DdlRefColumnName"].ToString();
                    context["mTip"] = drv["DdlRefColumnName"].ToString();
                    context["mImg"] = drv["DdlRefColumnName"].ToString();
                    context["ssd"] = Request.QueryString["ssd"];
                    context["rpt"] = Request.QueryString["rpt"];
                    context["reportCriId"] = drv["ReportCriId"].ToString();
                    context["csy"] = LcSystemId.ToString();
                    context["genPrefix"] = GenPrefix;
                    context["filter"] = "0";
                    context["isSys"] = "N";
                    context["conn"] = null;
                    context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
                    context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
                    cComboBox.AutoCompleteUrl = "AutoComplete.aspx/RptCriDdlSuggests";
                    cComboBox.DataContext = context;
                    cComboBox.Mode = "A";
                }
                cCriteria.Controls.Add(cComboBox);
			}
			else if (drv["DisplayName"].ToString() == "DropDownList")
			{
				cDropDownList = new DropDownList(); cDropDownList.ID = "x" + drv["ColumnName"].ToString(); cDropDownList.CssClass = "inp-ddl";
                cDropDownList.DataValueField = drv["DdlKeyColumnName"].ToString(); cDropDownList.DataTextField = drv["DdlRefColumnName"].ToString(); cDropDownList.AutoPostBack = dvDependent.Count > 0; cDropDownList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
				cCriteria.Controls.Add(cDropDownList);
			}
			else if (drv["DisplayName"].ToString() == "ListBox")
			{
				cListBox = new ListBox(); cListBox.ID = "x" + drv["ColumnName"].ToString(); cListBox.SelectionMode = ListSelectionMode.Multiple; cListBox.CssClass = "inp-pic";
                cListBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cListBox.DataTextField = drv["DdlRefColumnName"].ToString(); cListBox.AutoPostBack = dvDependent.Count > 0; cListBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
                if (drv["RowSize"].ToString() != string.Empty) { cListBox.Rows = int.Parse(drv["RowSize"].ToString()); cListBox.Height = int.Parse(drv["RowSize"].ToString()) * 16 + 7; }
				cCriteria.Controls.Add(cListBox);
                if (drv["DisplayMode"].ToString() == "AutoListBox")
                {
                    WebControl wcFtr = (WebControl)cCriteria.FindControl("x" + drv["DdlFtrColumnName"].ToString());
                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    context["method"] = QueryStr["rpt"].ToString() + drv["ColumnName"].ToString();
                    context["addnew"] = "Y";
                    context["sp"] = QueryStr["rpt"].ToString() + drv["ColumnName"].ToString();
                    context["requiredValid"] = drv["RequiredValid"].ToString();
                    context["mKey"] = drv["DdlKeyColumnName"].ToString();
                    context["mVal"] = drv["DdlRefColumnName"].ToString();
                    context["mTip"] = drv["DdlRefColumnName"].ToString();
                    context["mImg"] = drv["DdlRefColumnName"].ToString();
                    context["ssd"] = Request.QueryString["ssd"];
                    context["rpt"] = Request.QueryString["rpt"];
                    context["reportCriId"] = drv["ReportCriId"].ToString();
                    context["csy"] = LcSystemId.ToString();
                    context["genPrefix"] = GenPrefix;
                    context["filter"] = "0";
                    context["isSys"] = "N";
                    context["conn"] = null;
                    context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
                    context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
                    Session["SqlReport" + "_" + LcSystemId.ToString() + "_" + Request.QueryString["rpt"] + cListBox.ID] = context;
                    cListBox.Attributes["ac_context"] = "SqlReport" + "_" + LcSystemId.ToString() + "_" + Request.QueryString["rpt"] + cListBox.ID;
                    cListBox.Attributes["ac_url"] = "AutoComplete.aspx/RptCriDdlSuggestsEx";
                    cListBox.Attributes["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
                    cListBox.Attributes["searchable"] = "";
                    TextBox tb = new TextBox(); tb.ID = "x" + drv["ColumnName"].ToString() + "Hidden"; tb.Attributes["style"] = "display:none;";
                    cCriteria.Controls.Add(tb);
                }
            }
			else if (drv["DisplayName"].ToString() == "RadioButtonList")
			{
				cRadioButtonList = new RadioButtonList(); cRadioButtonList.ID = "x" + drv["ColumnName"].ToString();
                cRadioButtonList.DataValueField = drv["DdlKeyColumnName"].ToString(); cRadioButtonList.DataTextField = drv["DdlRefColumnName"].ToString(); cRadioButtonList.AutoPostBack = dvDependent.Count > 0; cRadioButtonList.RepeatDirection = RepeatDirection.Horizontal; cRadioButtonList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
                cRadioButtonList.Attributes["OnClick"] = "this.focus();";
                cLiteral = new Literal(); cLiteral.Text = "<div class=\"inp-rad\">"; cCriteria.Controls.Add(cLiteral);
				cCriteria.Controls.Add(cRadioButtonList);
                cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
            }
			else if (drv["DisplayName"].ToString() == "Calendar")
			{
                cCalendar = new System.Web.UI.WebControls.Calendar(); cCalendar.ID = "x" + drv["ColumnName"].ToString(); cCalendar.CssClass = "inp-txt calendar"; cCriteria.Controls.Add(cCalendar); 
			}
			else if (drv["DisplayName"].ToString() == "CheckBox")
			{
                cCheckBox = new CheckBox(); cCheckBox.ID = "x" + drv["ColumnName"].ToString(); cCheckBox.CssClass = "inp-chk"; cCheckBox.AutoPostBack = dvDependent.Count > 0; cCriteria.Controls.Add(cCheckBox); cCheckBox.CheckedChanged += new EventHandler(cCriButton_Click);
                cCheckBox.Attributes["OnClick"] = "this.focus();";
            }
			else
			{
                cTextBox = new TextBox(); cTextBox.ID = "x" + drv["ColumnName"].ToString(); cTextBox.CssClass = "inp-txt"; cTextBox.AutoPostBack = dvDependent.Count > 0; cTextBox.TextChanged += new EventHandler(cCriButton_Click);
				if (drv["DisplayMode"].ToString() == "MultiLine") { cTextBox.TextMode = TextBoxMode.MultiLine; }
				else if (drv["DisplayMode"].ToString() == "Password") { cTextBox.TextMode = TextBoxMode.Password; }
				cCriteria.Controls.Add(cTextBox);
			}
            cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
        }

		private void SetCriHolder(int ii, DataView dvCri, DataView dvGrp)
		{
            Literal cLiteral;
            if (dvCri.Count > 0)
            {
                foreach (DataRowView drv in dvCri)
                {
                    cLiteral = new Literal(); cLiteral.Text = "<div class=\"r-criteria\">"; cCriteria.Controls.Add(cLiteral);
                    MakeSqlGrpLab(drv);
                    MakeSqlGrpInp(drv);
                    cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
                }
            }
        }

		private void SetSelHolder()
		{
            DataTable dt = (new SqlReportSystem()).GetAllowSelect(QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw);
            if (dt.Rows.Count > 0 && dt.Rows[0]["AllowSelect"].ToString() == "Y")
			{
                cSelectHint.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "SqlReport", "DeselectAll", null, null, null);
				cSelectHint.Visible = true;
				cSelectList.Visible = true;
				cSelectList.Width = cViewer.Width;
				cSelectList.DataSource = GetSqlColumns(); cSelectList.DataBind();
				foreach (ListItem li in cSelectList.Items) { li.Selected = true; }
			}
		}

		private DataTable GetClientRule()
		{
			DataTable dtRul = (DataTable)Session[KEY_dtClientRule];
			if (dtRul == null)
			{
//				CheckAuthentication(false);
				dtRul = (new AdminSystem()).GetClientRule(0, Int32.Parse(QueryStr["rpt"].ToString()), base.LUser.CultureId, LcSysConnString, LcAppPw);
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

		private DataView GetSqlCriteria()
		{
			DataTable dt = (DataTable)Session[KEY_dtSqlCri];
			if (dt == null)
			{
				dt = (new SqlReportSystem()).GetReportCriteria(GenPrefix, QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw);
				Session[KEY_dtSqlCri] = dt;
			}
			return dt.DefaultView;
		}

		private DataView GetSqlColumns()
		{
			DataTable dt = (DataTable)Session[KEY_dtSqlObj];
			if (dt == null)
			{
				dt = (new SqlReportSystem()).GetReportColumns(GenPrefix, QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw);
				Session[KEY_dtSqlObj] = dt;
			}
			return dt.DefaultView;
		}

		private DataView GetCriReportGrp()
		{
			DataTable dt = (DataTable)Session[KEY_dtSqlGrp];
			if (dt == null)
			{
				dt = (new SqlReportSystem()).GetCriReportGrp(GenPrefix, QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw);
				Session[KEY_dtSqlGrp] = dt;
			}
			return dt.DefaultView;
		}

		private DataTable GetReportCriHlp()
		{
			DataTable dtCriHlp = (DataTable)Session[KEY_dtCriHlp];
			if (dtCriHlp == null)
			{
//				CheckAuthentication(false);
				dtCriHlp = (new SqlReportSystem()).GetRptCriHlp(GenPrefix, Int32.Parse(QueryStr["rpt"].ToString()), base.LUser.CultureId, LcSysConnString, LcAppPw);
				Session[KEY_dtCriHlp] = dtCriHlp;
			}
			return dtCriHlp;
		}

		private DataTable GetReportHlp()
		{
			DataTable dtHlp = (DataTable)Session[KEY_dtReportHlp];
			if (dtHlp == null)
			{
//				CheckAuthentication(false);
				dtHlp = (new SqlReportSystem()).GetRptHlp(GenPrefix, Int32.Parse(QueryStr["rpt"].ToString()), base.LUser.CultureId, LcSysConnString, LcAppPw);
                if (!string.IsNullOrEmpty(KEY_dtReportHlp)) { Session[KEY_dtReportHlp] = dtHlp; }
            }
			return dtHlp;
		}

        //private void CheckAuthentication(bool pageLoad)
        //{
        //    CheckAuthentication(pageLoad, true);
        //}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			DataView dvCri = GetSqlCriteria();
			foreach (DataRowView drv in dvCri)
			{
				if (drv["DataTypeSysName"].ToString() == "DateTime") { columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
				else if (drv["DataTypeSysName"].ToString() == "Byte") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
				else if (drv["DataTypeSysName"].ToString() == "Int16") { columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
				else if (drv["DataTypeSysName"].ToString() == "Int32") { columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
				else if (drv["DataTypeSysName"].ToString() == "Int64") { columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
				else if (drv["DataTypeSysName"].ToString() == "Single") { columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
				else if (drv["DataTypeSysName"].ToString() == "Double") { columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
				else if (drv["DataTypeSysName"].ToString() == "Byte[]") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
				else if (drv["DataTypeSysName"].ToString() == "Object") { columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
				else { columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
			}
			return dt;
		}

		private DataSet UpdCriteria(bool bUpd, bool bMem)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataSet ds = new DataSet();
			ds.Tables.Add(MakeColumns(new DataTable("DtSqlReportIn")));
			DataRow dr = ds.Tables["DtSqlReportIn"].NewRow();
			DataView dvCri = GetSqlCriteria();
			foreach (DataRowView drv in dvCri)
			{
				if (drv["DisplayName"].ToString() == "ListBox")
				{
                    bool bAll = false;
                    cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cListBox != null)
					{
                        int CriCnt = (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw);
                        int TotalChoiceCnt = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), true, string.Empty, base.LImpr, base.LCurr, LcAppConnString, LcAppPw)).Count;
                        if (drv["DisplayMode"].ToString() == "AutoListBox")
                        {
                            TextBox tb = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Hidden");
                            var selected = tb != null ? tb.Text : null;
                            DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), false, selected, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                            FilterCriteriaDdl(cCriteria, dv, drv);
                            cListBox.DataSource = dv;
                            cListBox.DataBind();
                            GetSelectedItems(cListBox, selected);
                        }
                        string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
                        bool noneSelected = string.IsNullOrEmpty(selectedValues) || selectedValues == "''";
                        dr[drv["ColumnName"].ToString()] = "(";
						foreach (ListItem li in cListBox.Items)
						{
                            if (li.Selected || (noneSelected && CriCnt + 1 > TotalChoiceCnt))
                            {
                                if ((li.Value.ToString().Trim() == "" && li.Selected) || (noneSelected && CriCnt + 1 > TotalChoiceCnt)) { bAll = true; noneSelected = true; }
                                if (bAll || li.Selected)
                                {
                                    if (dr[drv["ColumnName"].ToString()].ToString() != "(")
                                    {
                                        dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ",";
                                    }
                                    dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'" + (li.Value.ToString().Trim() == string.Empty && CriCnt + 1 > TotalChoiceCnt ? "0" : li.Value.ToString().Trim()) + "'";
                                }
                            }
						}
					}
                    if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cListBox.SelectedValue))
                    {
                        throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                    }
					if (dr[drv["ColumnName"].ToString()].ToString() == "(''" || dr[drv["ColumnName"].ToString()].ToString() == "(") { dr[drv["ColumnName"].ToString()] = string.Empty; } else { dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ")"; }
				}
				else if (drv["DisplayName"].ToString() == "Calendar")
				{
					cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCalendar != null && cCalendar.SelectedDate > DateTime.Parse("0001-01-01")) { dr[drv["ColumnName"].ToString()] = cCalendar.SelectedDate; }
				}
				else if (drv["DisplayName"].ToString() == "ComboBox")
				{
					cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cComboBox.SelectedValue))
                    {
                        throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                    }
                    if (cComboBox != null && cComboBox.SelectedIndex >= 0 && cComboBox.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cComboBox.SelectedValue; }
				}
				else if (drv["DisplayName"].ToString() == "DropDownList")
				{
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cDropDownList.SelectedValue))
                    {
                        throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                    }
                    if (cDropDownList != null && cDropDownList.SelectedIndex >= 0 && cDropDownList.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cDropDownList.SelectedValue; }
				}
				else if (drv["DisplayName"].ToString() == "RadioButtonList")
				{
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cRadioButtonList.SelectedValue))
                    {
                        throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                    }
                    if (cRadioButtonList != null && cRadioButtonList.SelectedIndex >= 0 && cRadioButtonList.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cRadioButtonList.SelectedValue; }
				}
				else if (drv["DisplayName"].ToString() == "CheckBox")
				{
					cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCheckBox != null) { dr[drv["ColumnName"].ToString()] = base.SetBool(cCheckBox.Checked); }
				}
				else
				{
					cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                    if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cTextBox.Text))
                    {
                        throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                    }
                    if (cTextBox != null && cTextBox.Text != string.Empty) { dr[drv["ColumnName"].ToString()] = drv["DisplayMode"].ToString() == "DateUTC" ? base.SetDateTimeUTC(cTextBox.Text, !bUpd) : cTextBox.Text; }
				}
			}
			ds.Tables["DtSqlReportIn"].Rows.Add(dr);
			if (bUpd) { (new SqlReportSystem()).UpdSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw); }
			if (bMem) 
			{ 
				string GenStr = string.Empty; if (GenPrefix != string.Empty) {GenStr = "gen=Y&";}
				string MemId = (new SqlReportSystem()).MemSqlReport(cPublicCri.SelectedValue, cMemCriId.Value, cMemFldDdl.SelectedValue, cMemCriName.Text, cMemCriDesc.Text, "SqlReport.aspx?" + GenStr + "csy=" + LcSystemId.ToString() + "&typ=N&rpt=" + QueryStr["rpt"].ToString() + "&key=" + cMemCriId.Value, QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw);
				if (cMemCriId.Value == string.Empty)	// Make sure the link is properly updated.
				{
					cMemCriId.Value = (new SqlReportSystem()).MemSqlReport(cPublicCri.SelectedValue, MemId, cMemFldDdl.SelectedValue, cMemCriName.Text, cMemCriDesc.Text, "SqlReport.aspx?" + GenStr + "csy=" + LcSystemId.ToString() + "&typ=N&rpt=" + QueryStr["rpt"].ToString() + "&key=" + MemId, QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw);
				}
			}
			return ds;
		}

		protected void cbPostBack(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		public void cSelectHint_Click(object sender, System.EventArgs e)
		{
			string str = (new AdminSystem()).GetLabel(base.LUser.CultureId, "SqlReport", "SelectAll", null, null, null);
			if (cSelectHint.Text == str)
			{
                cSelectHint.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "SqlReport", "DeselectAll", null, null, null);
				foreach (ListItem li in cSelectList.Items) { li.Selected = true; }
			}
			else
			{
				cSelectHint.Text = str;
				foreach (ListItem li in cSelectList.Items) { li.Selected = false; }
			}
		}

		protected void cMemCriDdl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cMemCriDdl.SelectedIndex >= 0 && cMemCriDdl.SelectedValue != string.Empty)
			{
				DataTable dt = (DataTable)Session[KEY_dtMemCriDdl];
				DataView dv = dt != null ? dt.DefaultView : null;
				if (dv != null)
				{
					dv.Sort = "RptMemCriName"; int row = dv.Find(cMemCriDdl.SelectedText);
					if (dv[row]["UsrId"].ToString() == string.Empty) { SetPublicCri(cPublicCri, "P"); } else { SetPublicCri(cPublicCri, "V"); }
					cMemFldId.Value = dv[row]["RptMemFldId"].ToString();
					cMemCriDesc.Text = dv[row]["RptMemCriDesc"].ToString();
					if (((char)191 + LImpr.Usrs + (char)191).IndexOf((char)191 + dv[row]["InputBy"].ToString() + (char)191) >= 0) { cMemCriUpd.Visible = true; cMemCriDel.Visible = true; } else { cMemCriUpd.Visible = false; cMemCriDel.Visible = false; }
				}
				DataView dvCri = GetSqlCriteria();
				SetCriteria((new SqlReportSystem()).GetMemCri(GenPrefix, byte.Parse(dvCri.Count.ToString()), QueryStr["rpt"].ToString(), cMemCriDdl.SelectedValue, LcSysConnString, LcAppPw), dvCri);
				if (cCriteria.Visible == false) { cViewButton_Click(sender, new EventArgs()); }
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriDdl.FocusID);
		}

		private void SetPublicCri(RadioButtonList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtPublicCri];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				ListItem li = null;
				if (dv == null)
				{
					dv = new DataView((new SqlReportSystem()).GetDdlAccessCd(true, keyId, LcSysConnString, LcAppPw, base.LImpr, base.LCurr));
				}
				if (dv != null)
				{
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId);
					if (li != null) { li.Selected = true; }
					Session[KEY_dtPublicCri] = dv.Table;
				}
			}
		}

		private void SetPublicFld(RadioButtonList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtPublicFld];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				ListItem li = null;
				if (dv == null)
				{
					dv = new DataView((new SqlReportSystem()).GetDdlAccessCd(true, keyId, LcSysConnString, LcAppPw, base.LImpr, base.LCurr));
				}
				if (dv != null)
				{
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId);
					if (li != null) { li.Selected = true; }
					Session[KEY_dtPublicFld] = dv.Table;
				}
			}
		}

		protected void cbMemCriDdl(object sender, System.EventArgs e)
		{
			SetMemCriDdl((RoboCoder.WebControls.ComboBox)sender, string.Empty, true);
		}

		private void SetMemCriDdl(RoboCoder.WebControls.ComboBox ddl, string keyId, bool bSearch)
		{
			DataTable dt = (DataTable)Session[KEY_dtMemCriDdl];
			DataView dv = dt != null ? dt.DefaultView : null;
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "";
            context["addnew"] = "Y";
            context["mKey"] = "RptMemCriId";
            context["mVal"] = "RptMemCriName";
            context["mTip"] = "RptMemCriName";
            context["mImg"] = "RptMemCriName";
            context["ssd"] = Request.QueryString["ssd"];
            context["rpt"] = Request.QueryString["rpt"];
            context["csy"] = LcSystemId.ToString();
            context["genPrefix"] = GenPrefix;
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = null;
            context["refColCID"] = null;
            ddl.AutoCompleteUrl = "AutoComplete.aspx/RptMemCriSuggests";
            ddl.DataContext = context;
            if (ddl != null)
            {
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible && bSearch) { bAll = true; }
				if ((dv == null && keyId != string.Empty) || ((dv == null || dv.Count <= 2) && bSearch))
				{
					bFirst = true;
                    dv = new DataView((new SqlReportSystem()).GetDdlRptMemCri(GenPrefix, QueryStr["rpt"].ToString(), bAll, 0, keyId, LcSysConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr));
				}
				if (dv != null)
				{
					ddl.DataSource = dv;
					if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) { keyId = ddl.SearchText.Substring(2); }
					if (!ddl.SelectByValue(keyId, string.Empty, true) && !bFirst)	//SelectByValue must be executed first
					{
                        dv = new DataView((new SqlReportSystem()).GetDdlRptMemCri(GenPrefix, QueryStr["rpt"].ToString(), bAll, 0, keyId, LcSysConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        ddl.DataSource = dv; ddl.SelectByValue(keyId, string.Empty, true);
					}
					if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
                        dv = new DataView((new SqlReportSystem()).GetDdlRptMemCri(GenPrefix, QueryStr["rpt"].ToString(), true, 0, keyId, LcSysConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        ddl.DataSource = dv; ddl.SelectByValue(keyId, string.Empty, true);
					}
					Session[KEY_dtMemCriDdl] = dv.Table;
					if (keyId == string.Empty && bSearch) { ddl.ApplyFilter(string.Empty); }
				}
			}
		}

		public void cMemCriNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			cMemCriId.Value = string.Empty; cMemCriName.Text = string.Empty; cMemCriDesc.Text = string.Empty;
			if (cMemFldDdl.SelectedIndex > 0) { cMemFldDdl.SelectedItem.Selected = false; cMemFldDdl.Items[0].Selected = true; cMemFldDdl.ClearSearch(); }
			cMemCriDdl.Visible = false; cMemCriName.Visible = true; cSavPanel.Visible = true;
			cMemCriNew.Visible = false; cMemCriUpd.Visible = false; cMemCriDel.Visible = false;
			cMemCriSav.Visible = true; cMemCriCnc.Visible = true; cPublicCri.Visible = true;
			cPlaceHolder.Visible = false;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriName.ClientID);
		}

		public void cMemCriUpd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cMemCriDdl.SelectedIndex >= 0 && cMemCriDdl.SelectedValue != string.Empty)
			{
				cMemCriId.Value = cMemCriDdl.SelectedValue; cMemCriName.Text = cMemCriDdl.SelectedItem.Text;
				cMemCriDdl.Visible = false; cMemCriName.Visible = true; cSavPanel.Visible = true;
				cMemCriNew.Visible = false; cMemCriUpd.Visible = false; cMemCriDel.Visible = false;
				cMemCriSav.Visible = true; cMemCriCnc.Visible = true; cPublicCri.Visible = true;
				SetMemFldDdl(cMemFldDdl, cMemFldId.Value, true);
				cMemFldDdl_SelectedIndexChanged(sender, new EventArgs());
				cPlaceHolder.Visible = false;
				ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriName.ClientID);
			}
		}

		public void cMemCriDel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cMemCriDdl.SelectedIndex >= 0 && cMemCriDdl.SelectedValue != string.Empty)
			{
				(new SqlReportSystem()).DelMemCri(GenPrefix, QueryStr["rpt"].ToString(), cMemCriDdl.SelectedValue, LcSysConnString, LcAppPw);
				Session.Remove(KEY_dtMemCriDdl); SetMemCriDdl(cMemCriDdl, string.Empty, true);
				cMemCriDdl_SelectedIndexChanged(sender, new EventArgs());
				cClearCriButton_Click(sender, new EventArgs());
				cPlaceHolder.Visible = false;
				ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriDdl.FocusID);
			}
		}

		public void cMemCriSav_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdCriteria(false, true);
			cMemCriDdl.Visible = true; cMemCriName.Visible = false; cSavPanel.Visible = false;
			cMemCriNew.Visible = true; cMemCriSav.Visible = false; cMemCriCnc.Visible = false; cPublicCri.Visible = false;
			Session.Remove(KEY_dtMemCriDdl); SetMemCriDdl(cMemCriDdl, cMemCriId.Value, true);
			cMemCriDdl_SelectedIndexChanged(sender, new EventArgs());
			cPlaceHolder.Visible = false;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriDdl.FocusID);
		}

		public void cMemCriCnc_Click(object sender, System.EventArgs e)
		{
			cMemCriDdl.Visible = true; cMemCriName.Visible = false; cSavPanel.Visible = false;
			cMemCriNew.Visible = true; cMemCriSav.Visible = false; cMemCriCnc.Visible = false; cPublicCri.Visible = false;
			cMemCriDdl_SelectedIndexChanged(sender, new EventArgs());
			cPlaceHolder.Visible = false;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemCriDdl.FocusID);
		}

		public void cMemFldNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			cMemFldId.Value = string.Empty; cMemFldName.Text = string.Empty;
			cMemFldDdl.Visible = false; cMemFldName.Visible = true;
			cMemFldNew.Visible = false; cMemFldUpd.Visible = false; cMemFldDel.Visible = false;
			cMemFldSav.Visible = true; cMemFldCnc.Visible = true; cPublicFld.Visible = true;
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldName.ClientID);
		}

		public void cMemFldUpd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cMemFldDdl.SelectedIndex >= 0 && cMemFldDdl.SelectedValue != string.Empty)
			{
				cMemFldId.Value = cMemFldDdl.SelectedValue; cMemFldName.Text = cMemFldDdl.SelectedItem.Text;
				cMemFldDdl.Visible = false; cMemFldName.Visible = true;
				cMemFldNew.Visible = false; cMemFldUpd.Visible = false; cMemFldDel.Visible = false;
				cMemFldSav.Visible = true; cMemFldCnc.Visible = true; cPublicFld.Visible = true;
				ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldName.ClientID);
			}
		}

		public void cMemFldDel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cMemFldDdl.SelectedIndex >= 0 && cMemFldDdl.SelectedValue != string.Empty)
			{
				(new SqlReportSystem()).DelMemFld(GenPrefix, cMemFldDdl.SelectedValue, LcSysConnString, LcAppPw);
				cMemFldDdl_SelectedIndexChanged(sender, new EventArgs());
				ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldDdl.FocusID);
			}
		}

		public void cMemFldSav_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			cMemFldId.Value = (new SqlReportSystem()).UpdMemFld(GenPrefix, cPublicFld.SelectedValue, cMemFldId.Value, cMemFldName.Text, base.LUser.UsrId, LcSysConnString, LcAppPw);
			cMemFldDdl.Visible = true; cMemFldName.Visible = false;
			cMemFldNew.Visible = true; cMemFldSav.Visible = false; cMemFldCnc.Visible = false; cPublicFld.Visible = false;
			cMemFldDdl_SelectedIndexChanged(sender, new EventArgs());
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldDdl.FocusID);
		}

		public void cMemFldCnc_Click(object sender, System.EventArgs e)
		{
			cMemFldDdl.Visible = true; cMemFldName.Visible = false;
			cMemFldNew.Visible = true; cMemFldSav.Visible = false; cMemFldCnc.Visible = false; cPublicFld.Visible = false;
			cMemFldDdl_SelectedIndexChanged(sender, new EventArgs());
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldDdl.FocusID);
		}

		protected void cMemFldDdl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cMemFldDdl.SelectedIndex >= 0 && cMemFldDdl.SelectedValue != string.Empty)
			{
				cMemFldId.Value = cMemFldDdl.SelectedValue;
				DataTable dt = (DataTable)Session[KEY_dtMemFldDdl];
				DataView dv = dt != null ? dt.DefaultView : null;
				if (dv != null)
				{
					dv.Sort = "RptMemFldName"; int row = dv.Find(cMemFldDdl.SelectedItem.Text);
					if (dv[row]["UsrId"].ToString() == string.Empty) { SetPublicFld(cPublicFld, "P"); } else { SetPublicFld(cPublicFld, "V"); }
					if (((char)191 + LImpr.Usrs + (char)191).IndexOf((char)191 + dv[row]["InputBy"].ToString() + (char)191) >= 0) { cMemFldUpd.Visible = true; cMemFldDel.Visible = true; } else { cMemFldUpd.Visible = false; cMemFldDel.Visible = false; }
				}
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cMemFldDdl.FocusID);
		}

		protected void cbMemFldDdl(object sender, System.EventArgs e)
		{
			Session.Remove(KEY_dtMemFldDdl); SetMemFldDdl((RoboCoder.WebControls.ComboBox)sender, string.Empty, true);
		}

		private void SetMemFldDdl(RoboCoder.WebControls.ComboBox ddl, string keyId, bool bSearch)
		{
			DataTable dt = (DataTable)Session[KEY_dtMemFldDdl];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible && bSearch) { bAll = true; }
				if ((dv == null && keyId != string.Empty) || ((dv == null || dv.Count <= 2) && bSearch))
				{
					bFirst = true;
					dv = new DataView((new SqlReportSystem()).GetDdlRptMemFld(GenPrefix, bAll, keyId, LcSysConnString, LcAppPw, base.LImpr, base.LCurr));
				}
				if (dv != null)
				{
					ddl.DataSource = dv;
					if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) { keyId = ddl.SearchText.Substring(2); }
					if (!ddl.SelectByValue(keyId, string.Empty, true) && !bFirst)	//SelectByValue must be executed first
					{
						dv = new DataView((new SqlReportSystem()).GetDdlRptMemFld(GenPrefix, bAll, keyId, LcSysConnString, LcAppPw, base.LImpr, base.LCurr));
						ddl.DataSource = dv; ddl.SelectByValue(keyId, string.Empty, true);
					}
					if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						dv = new DataView((new SqlReportSystem()).GetDdlRptMemFld(GenPrefix, true, keyId, LcSysConnString, LcAppPw, base.LImpr, base.LCurr));
						ddl.DataSource = dv; ddl.SelectByValue(keyId, string.Empty, true);
					}
					Session[KEY_dtMemFldDdl] = dv.Table;
					if (keyId == string.Empty && bSearch) { ddl.ApplyFilter(string.Empty); }
				}
			}
		}

		public void cShowCriButton_Click(object sender, System.EventArgs e)
		{
			cCriPanel.Visible = true; cCriteria.Visible = true;
            cClearCriButton.Visible = (bool)Session[KEY_bClCriVisible];
            cViewButton.Visible = (bool)Session[KEY_bViewVisible];
            cExpPdfButton.Visible = (bool)Session[KEY_bExpPdfVisible];
            cExpDocButton.Visible = (bool)Session[KEY_bExpDocVisible];
            cExpXlsButton.Visible = (bool)Session[KEY_bExpXlsVisible];
            cPrintButton.Visible = (bool)Session[KEY_bPrintVisible];
            cShowCriButton.Visible = false; cViewer.Visible = false;
		}

		public void cClearCriButton_Click(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			//AjaxControlToolkit.CalendarExtender cCalendarExtender;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataView dvCri = GetSqlCriteria();
			foreach (DataRowView drv in dvCri)
			{
				if (drv["DisplayName"].ToString() == "ComboBox")	// Reset to page 1 by reassigning the datasource:
				{
					cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cComboBox != null && cComboBox.Items.Count > 0) { cComboBox.DataSource = cComboBox.DataSource; cComboBox.SelectByValue(cComboBox.Items[0].Value, string.Empty, true); }
				}
				else if (drv["DisplayName"].ToString() == "DropDownList")
				{
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cDropDownList != null && cDropDownList.Items.Count > 0) { cDropDownList.SelectedIndex = 0; }
				}
				else if (drv["DisplayName"].ToString() == "ListBox")
				{
					cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cListBox != null && cListBox.Items.Count > 0) { cListBox.SelectedIndex = 0; }
				}
				else if (drv["DisplayName"].ToString() == "RadioButtonList")
				{
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cRadioButtonList != null && cRadioButtonList.Items.Count > 0) { cRadioButtonList.SelectedIndex = 0; }
				}
				else if (drv["DisplayName"].ToString() == "Calendar")
				{
					cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCalendar != null)
					{
						if (drv["RequiredValid"].ToString() == "N") { cCalendar.SelectedDates.Clear(); } else { cCalendar.SelectedDate = System.DateTime.Today; }
					}
				}
				else if (drv["DisplayName"].ToString() == "CheckBox")
				{
					cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCheckBox != null) { cCheckBox.Checked = false; }
				}
				else
				{
					cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cTextBox != null)
					{
						if (drv["RequiredValid"].ToString() == "N") { cTextBox.Text = string.Empty; } else if (drv["DisplayMode"].ToString().IndexOf("Date") >= 0) { cTextBox.Text = System.DateTime.Today.ToString(); } else { cTextBox.Text = "0"; }
					}
				}
			}
		}

		public void cPrintButton_Click(object sender, System.EventArgs e)
		{
			DataSet ds = UpdCriteria(true, false);
            if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "T") { DoTxtRpt(ds); }
            else
            {
                cCriPanel.Visible = false; cCriteria.Visible = false; cClearCriButton.Visible = false;
            }
		}

        private void DoRpt(string fmt)
        {
            DataSet ds;
            if (cMemCriDdl.SelectedIndex >= 0 && cMemCriDdl.SelectedValue != string.Empty)
            {
                (new SqlReportSystem()).UpdMemViewdt(GenPrefix, cMemCriDdl.SelectedValue, LcSysConnString, LcAppPw);
            }
            ds = UpdCriteria(true, false);
            try
            {
                if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "S") { DoSqlRptLocal(ds, fmt); }
                else if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "R") { DoRtfRpt(ds); }
                else if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "B") { LoadGauge(ds); }
                else if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "X") { DoXlsRpt(ds); }	// Need to disable programs generared by Genreport in the future.
                else if (GetReportHlp().Rows[0]["ReportTypeCd"].ToString() == "E") { DoXlsTmplRpt(ds); }
                cCriPanel.Visible = false; cCriteria.Visible = false; cClearCriButton.Visible = false;
                cShowCriButton.Visible = (bool)Session[KEY_bShCriVisible];
            }
            catch (Exception err) { PreMsgPopup(err.Message + (err.InnerException != null ? " " + err.InnerException.Message : "")); return; }
        }

		public void cViewButton_Click(object sender, System.EventArgs e)
		{
            DoRpt(string.Empty);
        }

        public void cExpPdfButton_Click(object sender, System.EventArgs e)
        {
            DoRpt("pdf");
        }

        public void cExpDocButton_Click(object sender, System.EventArgs e)
        {
            DoRpt("doc");
        }

        public void cExpXlsButton_Click(object sender, System.EventArgs e)
        {
            DoRpt("xls");
        }

        private void PreMsgPopup(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
            string iconUrl = "images/error.gif";
            string msgContent = ReformatErrMsg(msg);
            if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
            string script =
            @"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + @"','');
			</script>";
            ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
        }

		private void DoSqlRptLocal(DataSet ds, string fmt)
		{
			DataView dvCri = GetSqlCriteria();
			DataView dvObj = GetSqlColumns();
			int ii = dvCri.Count + dvObj.Count + 21;
			dvObj.RowFilter = "RptObjTypeCd = 'P'"; ii = ii + dvObj.Count; dvObj.RowFilter = string.Empty;
			cViewer.ProcessingMode = ProcessingMode.Local;
			cViewer.LocalReport.EnableHyperlinks = true;
			cViewer.LocalReport.EnableExternalImages = true;
            string urlBase =
                !string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["ExtBaseUrl"])
                    ? System.Configuration.ConfigurationManager.AppSettings["ExtBaseUrl"]
                    : Request.Url.AbsoluteUri.Replace(Request.Url.Query, "").Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");
            System.Xml.XmlDocument rpt = new System.Xml.XmlDocument();
            string fileName = Request.MapPath("reports\\" + GetReportHlp().Rows[0]["ProgramName"].ToString() + "Report.rdl");
            if (File.Exists(fileName + "c")) { rpt.Load(fileName + "c"); } else { rpt.Load(fileName); }     // Run custom-built report if exists.
			System.Xml.XmlNamespaceManager ns = new System.Xml.XmlNamespaceManager(rpt.NameTable);
			ns.AddNamespace("x", rpt.DocumentElement.NamespaceURI);
			System.Xml.XmlNodeList images = rpt.SelectNodes("//x:Image[x:Source='External']/x:Value", ns);
			foreach (System.Xml.XmlNode n in images)
			{
                if (!(n.InnerText.StartsWith("http:") || n.InnerText.StartsWith("https:")))
                {
                    if (!n.InnerText.StartsWith("="))
                    {
                        n.InnerText = urlBase + n.InnerText;
                    }
                    else
                    {
                        n.InnerText = n.InnerText.Replace("~/", urlBase);
                    }
                }
			}
            System.Xml.XmlNodeList datasourcename = rpt.SelectNodes("//x:DataSetName", ns);
            foreach (System.Xml.XmlNode n in datasourcename)
            {
                n.InnerText = LcAppDb;
            }
            System.Xml.XmlNodeList dataset = rpt.SelectNodes("//x:DataSet", ns);
            foreach (System.Xml.XmlNode n in dataset)
            {
                n.Attributes["Name"].Value = LcAppDb;
            }
            using (MemoryStream ms = new MemoryStream())
			{
				rpt.Save(ms);
				ms.Seek(0, SeekOrigin.Begin);
				cViewer.LocalReport.LoadReportDefinition(ms);
			}
			DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
			string sNull = null;
			ReportParameter[] pms = new ReportParameter[ii];
			pms[0] = new ReportParameter("reportId", QueryStr["rpt"].ToString());
			pms[1] = new ReportParameter("RowAuthoritys", base.LImpr.RowAuthoritys);
			pms[2] = new ReportParameter("Usrs", base.LImpr.Usrs);
			pms[3] = new ReportParameter("UsrGroups", base.LImpr.UsrGroups);
			pms[4] = new ReportParameter("Cultures", base.LImpr.Cultures);
			pms[5] = new ReportParameter("Companys", base.LImpr.Companys);
			pms[6] = new ReportParameter("Projects", base.LImpr.Projects);
			pms[7] = new ReportParameter("Agents", base.LImpr.Agents);
			pms[8] = new ReportParameter("Brokers", base.LImpr.Brokers);
			pms[9] = new ReportParameter("Customers", base.LImpr.Customers);
			pms[10] = new ReportParameter("Investors", base.LImpr.Investors);
			pms[11] = new ReportParameter("Members", base.LImpr.Members);
			pms[12] = new ReportParameter("Vendors", base.LImpr.Vendors);
			pms[13] = new ReportParameter("currCompanyId", base.LCurr.CompanyId.ToString());
			pms[14] = new ReportParameter("currProjectId", base.LCurr.ProjectId.ToString());
			ii = 15;
			foreach (DataRowView drv in dvCri)
			{
				if (drv["RequiredValid"].ToString() == "N")
				{
					if (dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
					{
						pms[ii] = new ReportParameter(drv["ColumnName"].ToString(), sNull);
					}
					else
					{
						pms[ii] = new ReportParameter(drv["ColumnName"].ToString(), dr[drv["ColumnName"].ToString()].ToString());
					}
				}
				else
				{
					pms[ii] = new ReportParameter(drv["ColumnName"].ToString(), dr[drv["ColumnName"].ToString()].ToString());
				}
				ii = ii + 1;
			}
			pms[ii] = new ReportParameter("bUpd", "N"); ii = ii + 1;
			pms[ii] = new ReportParameter("bXls", "N"); ii = ii + 1;
			pms[ii] = new ReportParameter("bVal", "N"); ii = ii + 1;
			pms[ii] = new ReportParameter("ReportTitle", GetReportHlp().Rows[0]["ReportTitle"].ToString()); ii = ii + 1;
			pms[ii] = new ReportParameter("UsrName", base.LUser.UsrName); ii = ii + 1;
			pms[ii] = new ReportParameter("UrlBase", base.UrlBase); ii = ii + 1;
			DataTable dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LImpr, base.LCurr, UpdCriteria(false, false), LcAppConnString, LcAppPw, false, false, true);
			if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
            CovertRptUTC(dt);
            DataView dv;
			dv = new DataView((new SqlReportSystem()).GetReportObjHlp(GenPrefix, Int32.Parse(QueryStr["rpt"].ToString()), base.LUser.CultureId, LcSysConnString, LcAppPw));
			foreach (DataRowView drv in dv)
			{
				if (drv["RptObjTypeCd"].ToString() == "P") // Parameter.
				{
					pms[ii] = new ReportParameter(drv["ColumnName"].ToString(), dt.Rows[0][drv["ColumnName"].ToString()].ToString()); ii = ii + 1;
				}
				pms[ii] = new ReportParameter("L_" + drv["ColumnName"].ToString(), drv["ColumnHeader"].ToString()); ii = ii + 1;
			}
			cViewer.LocalReport.SetParameters(pms);
			string reportName = "SqlReport";
			if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
			cViewer.LocalReport.DisplayName = reportName + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
			dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LImpr, base.LCurr, UpdCriteria(false, false), LcAppConnString, LcAppPw, false, false, false);
			ReportDataSource rptDataSource = new ReportDataSource(LcAppDb, dt);
			cViewer.LocalReport.DataSources.Clear();
			cViewer.ShowRefreshButton = false;
			cViewer.LocalReport.DataSources.Add(rptDataSource);
			cViewer.LocalReport.Refresh();

            /* Skip the view mode */
            if (fmt != string.Empty)
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename = string.Empty;
                byte[] bytes = cViewer.LocalReport.Render(fmt == "doc" ? "Word" : (fmt == "xls" ? "Excel" : fmt), null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", cTitleLabel.Text, fmt);
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.Clear();
                if (QueryStr["act"] != null && QueryStr["act"].ToString() == "Y")
                {
                    Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                }
                else
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                }
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                if (!string.IsNullOrEmpty(Request["runas"])) Session.Abandon();
            }
            cViewer.Visible = true; cViewButton.Visible = false; cExpPdfButton.Visible = false; cExpDocButton.Visible = false; cExpXlsButton.Visible = false;
        }

		private void DoTxtRpt(DataSet ds)
		{
			StringBuilder sb = new StringBuilder();
			DataTable dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, false, false, false);
			if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
            CovertRptUTC(dt);
            string reportName = GetReportHlp().Rows[0]["ProgramName"].ToString();
			if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
			DataTable dtTemplate = null;
			Int16 iPrev = -1;
			Int16 iCurr;
			string ss = string.Empty;
			int ii = 0;
			while (ii < dt.Rows.Count)
			{
				if (dt.Columns.Contains("TemplateId")) { try { iCurr = Int16.Parse(dt.Rows[ii]["TemplateId"].ToString()); } catch { iCurr = 0; } } else { iCurr = 0; }
				if (iCurr != iPrev)
				{
					dtTemplate = (new SqlReportSystem()).GetDocImage(QueryStr["rpt"].ToString(), iCurr, LcSysConnString, LcAppPw);
					ss = UTF8Encoding.UTF8.GetString((byte[])dtTemplate.Rows[0]["DocImage"]);
					if (ss.IndexOf("\\x1b") == 1) { ss.Remove(0, 1); }	//Remove unwanted character.
					iPrev = iCurr;
				}
				sb.Append(MergeValues(ss, ii, dt));
				ii = ii + 1;
			}
			sb.Replace("\\x1b", "\x1b");	//Replace with true escape character 27.
			DirectPrint dp = new DirectPrint();
			bool bPrintOK = false;
			dp.PrintString(cPrinter.SelectedItem.Value, "Rintagi Printing", sb.ToString(), ref bPrintOK);
			DataTable dtPrinters = (DataTable)Session[KEY_dtPrinters];
			if (bPrintOK && dtPrinters != null && dtPrinters.Rows[cPrinter.SelectedIndex]["UpdatePrinted"].ToString() == "Y")
			{
				(new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, true, false, false);
			}
		}

        private System.Collections.Generic.KeyValuePair<string,byte[]> GetTemplate(string TemplateId, string TemplateName)
        {
            if (!string.IsNullOrEmpty(TemplateId))
            {
                DataTable dtTemplate = (new SqlReportSystem()).GetDocImage(QueryStr["rpt"].ToString(), Int16.Parse(TemplateId), LcSysConnString, LcAppPw);
                return new KeyValuePair<string, byte[]>(dtTemplate.Rows[0]["TemplateName"].ToString(), dtTemplate.Rows[0]["DocImage"] as byte[]);
            }
            else if (!string.IsNullOrEmpty(TemplateName))
            {
                using (FileStream fs = new FileStream(Request.MapPath("reports\\" + TemplateName), FileMode.Open, FileAccess.Read, FileShare.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] content = new byte[fs.Length];
                    br.Read(content, 0, (int)fs.Length);
                    return new KeyValuePair<string, byte[]>(TemplateName, content);
                }
            }
            return new KeyValuePair<string, byte[]>();

        }
        private void DoXlsTmplRpt(DataSet ds)
        {
            DataTable dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, false, false, false);
            if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
            CovertRptUTC(dt);
            string reportName = GetReportHlp().Rows[0]["ProgramName"].ToString();
            if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
            System.Collections.Generic.KeyValuePair<string, byte[]> template = new KeyValuePair<string,byte[]>();
            if (dt.Columns.Contains("TemplateId"))
            {
                template = GetTemplate(dt.Rows[0]["TemplateId"].ToString(),"");
            }
            else if (dt.Columns.Contains("TemplateName"))
            {
                template = GetTemplate("",dt.Rows[0]["TemplateName"].ToString());
            }
            else
            {
                template = GetTemplate("", GetReportHlp().Rows[0]["TemplateName"].ToString());
            }
            if (!string.IsNullOrEmpty(template.Key))
            {
                byte[] content = MergeToExcel(template.Value, dt);
                string templateName = template.Key;
                ExportToStream(reportName + templateName.Substring(templateName.IndexOf("."), templateName.Length - templateName.IndexOf(".")), content);
            }

        }
        private byte[] MergeToExcel2003WorkSheet(byte[] worksheetXML, DataTable dt)
        {
            string xlsXML = System.Text.Encoding.UTF8.GetString(worksheetXML);
            DataView dv = new DataView(dt);
            // list tags
            Regex reSheet = new Regex("<Worksheet\\s+[^>/]*>(.*?)</Worksheet>", RegexOptions.Singleline);
            MatchEvaluator matchSheet = (sheet) =>
                {
                    string sheetContent = sheet.Value;

                    Regex reRow = new Regex("<Row[^>/]*>(.*?)</Row>", RegexOptions.Singleline);
                    // rows of data in the form of [=[TagName]] for each cell
                    Regex reLine = new Regex("([^\\[]*)(\\[=\\[[^\\]]*\\]\\])(.*)", RegexOptions.Singleline);
                    // sub-total break in the form of [/[Column1]X] break on Column1, X is the style to control formula replacement 
                    // X = 0 means always show group break line
                    // X = lines means only show if # of lines in group > X
                    // Column1 indicate level of break, nothing means grand total
                    Regex reGrpBreak = new Regex("\\[\\/\\[([^\\]]*)\\](\\d)?\\]", RegexOptions.Singleline);
                    // block rows begin in the form of [{[Column1=X,Column2=Y]] where Column1,2 matches the filter value. filtering is optional
                    Regex reBlockBegin = new Regex("\\[\\{\\[([^\\]]*)\\]\\]", RegexOptions.Singleline);
                    // block rows end in the form of [}[Column1=X]]
                    Regex reBlockEnd = new Regex("\\[\\}\\[([^\\]]*)\\]\\]", RegexOptions.Singleline);
                    Regex reRowWithIndex1 = new Regex("(<Row\\s+)(ss:Index=\"[0-9]+\")([^>]*>.*?</Row>)", RegexOptions.Singleline);
                    Regex reRowWithIndex2 = new Regex("(<Row\\s+)(ss:Index=\"[0-9]+\")([^>]*/>)", RegexOptions.Singleline);
                    Regex reFormula = new Regex("(?<=[\\s\\(\\\"=])((R)\\[(\\-(1|2))\\])([^:]*?:?)((R)\\[(\\-1)\\])([^\\\"]*?\"?)", RegexOptions.Singleline);
                    string currentBlock = "";
                    string currentBreak = "";
                    string breakField = "";
                    int grpBreakStyle = 0;
                    int detRowCnt = 0;
                    int grpRowCnt = 0;
                    bool bInBlock = false;
                    string blockFilter = "";
                    bool bRowExpanded = false;
                    List<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();

                    MatchEvaluator matchLine = (m) =>
                    {
                        return m.Value;
                    };
                    MatchEvaluator matchEval = (m) =>
                    {
                        var match = reLine.Match(m.Value); // first match([=[ tagName)
                        var blockBegin = reBlockBegin.Match(m.Value);
                        var blockEnd = reBlockEnd.Match(m.Value);
                        var grpBreak = reGrpBreak.Match(m.Value);
                        if (blockBegin.Success)
                        {
                            filters = new List<KeyValuePair<string, string>>();
                            if (blockBegin.Groups[1].Value.Contains("="))
                            {

                                string[] filterDef = blockBegin.Groups[1].Value.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string f in filterDef)
                                {
                                    string[] filter = f.Trim().Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (filter.Length > 1)
                                    {
                                        blockFilter = blockBegin.Groups[1].Value.Trim();
                                        filters.Add(new KeyValuePair<string, string>(filter[0].Trim(), filter[1].Trim()));
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                            bInBlock = true;
                        }

                        if ((!bInBlock && match.Success)
                            ||
                            (bInBlock && blockEnd.Success)
                            ||
                            (bInBlock && !blockEnd.Success)
                            )
                        {
                            if (grpBreak.Success)
                            {
                                currentBreak = currentBreak + reGrpBreak.Replace(m.Value, "");
                                if (!string.IsNullOrEmpty(grpBreak.Groups[1].Value))
                                {
                                    breakField = grpBreak.Groups[1].Value;
                                    grpBreakStyle = string.IsNullOrEmpty(grpBreak.Groups[2].Value) ? 0 : int.Parse(grpBreak.Groups[2].Value);
                                }
                                else if (!string.IsNullOrEmpty(breakField))
                                {
                                    grpRowCnt = grpRowCnt + 1;
                                }
                            }
                            else if (!blockBegin.Success && !blockEnd.Success)
                            {
                                currentBlock = currentBlock + m.Value;
                                detRowCnt = detRowCnt + 1;
                            }
                            if (bInBlock && !blockEnd.Success)
                            {
                                return "";
                            }
                            else
                            {
                                match = reLine.Match(string.IsNullOrEmpty(currentBlock) ? currentBreak : currentBlock);
                                System.Collections.Generic.List<string> retList = new System.Collections.Generic.List<string>();
                                System.Collections.Generic.HashSet<string> subset = null;
                                if (filters.Count > 0)
                                {
                                    foreach (var f in filters)
                                    {
                                        var s = new System.Collections.Generic.HashSet<string>(dt.AsEnumerable().Where(dr => dr["TagName"].ToString() == "[=[" + f.Key + "]]" && dr["TagValue"].ToString() == f.Value).Select(dr => dr["TagGrp"].ToString()));
                                        if (subset == null) subset = s;
                                        else subset.IntersectWith(s);
                                    }
                                }
                                var groups = new System.Collections.Generic.HashSet<string>((from x in dt.AsEnumerable() where x.Field<string>("TagName") == match.Groups[2].Value.Trim() select x.Field<string>("TagGrp")));
                                var rows = from x in dt.AsEnumerable()
                                           where x.Field<string>("TagGrp") != null && groups.Contains((x.Field<string>("TagGrp") ?? ""))
                                                && (subset == null || (subset.Contains((x.Field<string>("TagGrp") ?? ""))))
                                           group x by x.Field<string>("TagGrp");
                                string lastGrp = "";
                                int lines = 0;
                                int grpLines = 0;
                                bool addBreak = false;
                                var brkLine = currentBreak;
                                foreach (var g in rows)
                                {
                                    var val = currentBlock;
                                    foreach (var r in g)
                                    {
                                        string tag = r["TagName"].ToString();
                                        string tagValue = r["TagValue"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                                        decimal val_dec;
                                        DateTime val_date;
                                        bool isDec = decimal.TryParse(tagValue, out val_dec) && !tagValue.StartsWith(" ");
                                        bool isDate = DateTime.TryParse(tagValue, out val_date) && !tagValue.StartsWith(" ");
                                        bool isEscapeDec = decimal.TryParse(tagValue, out val_dec) && tagValue.StartsWith(" ");
                                        bool isEscapeDate = DateTime.TryParse(tagValue, out val_date) && tagValue.StartsWith(" ");

                                        if (!string.IsNullOrEmpty(currentBreak) && "[=["+breakField+"]]" == tag)
                                        {
                                            if (lines == 0) lastGrp = r["TagValue"].ToString();
                                            else if (lastGrp != r["TagValue"].ToString())
                                            {
                                                addBreak = true;
                                                lastGrp = r["TagValue"].ToString();
                                            }
                                        }
                                        if (isDate && !isDec)
                                        {
                                            tagValue = ((val_date - new DateTime(1900, 1, 1)).TotalDays + 2).ToString();
                                            val = val.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");
                                            if (!addBreak) brkLine = brkLine.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");

                                        }
                                        else if (isDec)
                                        {
                                            val = val.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");
                                            if (!addBreak) brkLine = brkLine.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");
                                        }
                                        else
                                        {
                                            if (isEscapeDate || isEscapeDec) tagValue = tagValue.Substring(1);
                                            val = val.Replace(tag, tagValue);
                                            if (!addBreak) brkLine = brkLine.Replace(tag, tagValue);
                                        }
                                    }
                                    if (addBreak)
                                    {
                                        if (!string.IsNullOrEmpty(brkLine))
                                        {
                                            if (grpBreakStyle == 0)
                                            {
                                                int grpCounts = detRowCnt * grpLines + grpRowCnt;
                                                brkLine = reFormula.Replace(brkLine,
                                                    fm =>
                                                    {
                                                        string v = fm.Groups[2].Value + "[-" + grpCounts.ToString() + "]" + fm.Groups[5].Value + fm.Groups[6].Value;
                                                        //return fm.Value;
                                                        return v;
                                                    });

                                                retList.Add(brkLine);
                                            }
                                            else if (grpLines > grpBreakStyle)
                                            {
                                                int grpCounts = detRowCnt * grpLines + grpRowCnt;
                                                brkLine = reFormula.Replace(brkLine,
                                                    fm =>
                                                    {
                                                        string v = fm.Groups[2].Value + "[-" + grpCounts.ToString() + "]" + fm.Groups[5].Value + fm.Groups[6].Value;
                                                        //return fm.Value;
                                                        return v;
                                                    });
                                                retList.Add(brkLine);
                                            }
                                        }
                                        addBreak = false;
                                        brkLine = currentBreak;
                                        grpLines = 0;
                                    }
                                    lines = lines + 1;
                                    grpLines = grpLines + 1;
                                    retList.Add(val);
                                }
                                if (rows.Count() == 0)
                                {
                                    // a special case when there is [=[TagName]] line specified but with no matching contents, we just output a row with all the tag replaced with empty string
                                    Regex reListTag = new Regex("(\\[=\\[[^\\]]*\\]\\])", RegexOptions.Singleline); // individual [=[TagName]] in a line
                                    var val = reListTag.Replace(currentBlock, ""); // replace all [=[TagName]] for the current block with empty strng
                                    lines = lines + 1;
                                    // don't increment group count as there really is nothing for group total
                                    // grpLines = grpLines + 1;
                                    //retList.Add(val);
                                    retList.Add("<Row></Row>"); // or just replace with empty row
                                }
                                if (addBreak || grpLines > 0)
                                {
                                    if (!string.IsNullOrEmpty(brkLine))
                                    {
                                        if (grpBreakStyle == 0)
                                        {
                                            int grpCounts = detRowCnt * grpLines + grpRowCnt;
                                            brkLine = reFormula.Replace(brkLine,
                                                fm =>
                                                {
                                                    string v = fm.Groups[2].Value + "[-" + grpCounts.ToString() + "]" + fm.Groups[5].Value + fm.Groups[6].Value;
                                                    //return fm.Value;
                                                    return v;
                                                });

                                            retList.Add(brkLine);
                                        }
                                        else if (grpLines > grpBreakStyle)
                                        {
                                            int grpCounts = detRowCnt * grpLines + grpRowCnt;
                                            brkLine = reFormula.Replace(brkLine,
                                                fm =>
                                                {
                                                    string v = fm.Groups[2].Value + "[-" + grpCounts.ToString() + "]" + fm.Groups[5].Value + fm.Groups[6].Value;
                                                    //return fm.Value;
                                                    return v;
                                                });
                                            retList.Add(brkLine);
                                        }
                                    }
                                    addBreak = false;
                                    brkLine = currentBreak;
                                }
                                currentBlock = ""; currentBreak = ""; breakField = ""; grpBreakStyle = 0;
                                bInBlock = false;
                                blockFilter = "";
                                filters = new List<KeyValuePair<string, string>>();
                                bRowExpanded = true;
                                return string.Join("\r\n", retList.ToArray());
                            }
                        }
                        else
                        {
                            if (blockEnd.Success)
                            {
                                currentBlock = ""; currentBreak = ""; breakField = ""; grpBreakStyle = 0;
                                bInBlock = false;
                                blockFilter = "";
                                filters = new List<KeyValuePair<string, string>>();
                            }
                            if (bRowExpanded)
                            {
                                string x = m.Value;
                                x = reRowWithIndex1.Replace(x,
                                    m1 =>
                                    {
                                        return m1.Groups[1].Value + "" + m1.Groups[3].Value;
                                    });
                                x = reRowWithIndex2.Replace(x,m1 => 
                                {
                                    return m1.Groups[1].Value + "" + m1.Groups[3].Value;
                                });
                                return x;
                            }
                            else
                                return m.Value;
                        }

                    };
                    sheetContent = reRow.Replace(sheetContent, matchEval);
                    return sheetContent;
                };

            xlsXML = reSheet.Replace(xlsXML, matchSheet);

            // simple tags must be after list tags
            dv.RowFilter = "TagName like '[[][[]%'";
            foreach (DataRowView drv in dv)
            {
                string tag = drv["TagName"].ToString();
                string tagValue = drv["TagValue"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                decimal val_dec;
                DateTime val_date;
                bool isDec = decimal.TryParse(tagValue, out val_dec) && !tagValue.StartsWith(" ");
                bool isDate = DateTime.TryParse(tagValue, out val_date) && !tagValue.StartsWith(" ");
                bool isEscapeDec = decimal.TryParse(tagValue, out val_dec) && tagValue.StartsWith(" ");
                bool isEscapeDate = DateTime.TryParse(tagValue, out val_date) && tagValue.StartsWith(" ");

                if (isDate && !isDec)
                {
                    tagValue = ((val_date - new DateTime(1900, 1, 1)).TotalDays + 2).ToString();
                    xlsXML = xlsXML.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");
                }
                else if (isDec)
                {
                    xlsXML = xlsXML.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");
                }
                else
                {
                    if (isEscapeDate || isEscapeDec) tagValue = tagValue.Substring(1);
                    xlsXML = xlsXML.Replace(tag, tagValue);
                }
            }

            // we have modified the row counts so must remove this
            Regex reRowCount = new Regex("ss:ExpandedRowCount=\"[0-9]*\"");
            xlsXML = reRowCount.Replace(xlsXML, " ");

            return System.Text.Encoding.UTF8.GetBytes(xlsXML);
        }
        private byte[] MergeToExcel2007WorkSheet(byte[] worksheetXML, DataTable dt, bool isShareStringXML, System.Collections.Generic.List<string> sharedStringsList)
        {
            string xlsXML = System.Text.Encoding.UTF8.GetString(worksheetXML);
            DataView dv = new DataView(dt);
            // simple tags
            dv.RowFilter = "TagName like '[[][[]%'";
            foreach (DataRowView drv in dv)
            {
                string tag = drv["TagName"].ToString();
                string tagValue = drv["TagValue"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                decimal val_dec;
                DateTime val_date;
                bool isDec = decimal.TryParse(tagValue, out val_dec) && !tagValue.StartsWith(" ");
                bool isDate = DateTime.TryParse(tagValue, out val_date) && !tagValue.StartsWith(" ");
                bool isEscapeDec = decimal.TryParse(tagValue, out val_dec) && tagValue.StartsWith(" ");
                bool isEscapeDate = DateTime.TryParse(tagValue, out val_date) && tagValue.StartsWith(" ");
                if (isDec || isDate)
                {
                    int idx = sharedStringsList.FindIndex(s => s.Contains(tag));
                    if (idx >= 0 && !isShareStringXML)
                    {
                        Regex re = new Regex("t=\"s\">" + "[\\r\\n\\s]*" + "<v>" + idx.ToString() + "</v>" + "[\\r\\n\\s]*</c>", RegexOptions.Singleline);
                        xlsXML = re.Replace(xlsXML, s =>
                        {
                            string v = s.Value;
//                            return "t=\"" + (isDate && !isDec ? "d" : "n") + "\">" + string.Format("<v>{0}</v></c>", isDate && !isDec ? ((val_date - new DateTime(1900, 1, 1)).TotalDays + 2).ToString() : tagValue);
                            return ">" + string.Format("<v>{0}</v></c>", isDate && !isDec ? ((val_date - new DateTime(1900, 1, 1)).TotalDays + 2).ToString() : (val_dec == 0.0m ? "0" : tagValue));
                        });
                    }
                    else
                    {
                        if (isEscapeDate || isEscapeDec) tagValue = tagValue.Substring(1);
                        xlsXML = xlsXML.Replace(tag, tagValue);
                    }

                }
                else
                {
                    xlsXML = xlsXML.Replace(tag, tagValue);
                }
            }
            // list tags
            Regex reRow = new Regex("<Row[^>]*>(.*?)</Row>", RegexOptions.Singleline);
            Regex reLine = new Regex("([^\\[]*)(\\[=\\[[^\\]]*\\]\\])(.*)", RegexOptions.Singleline);
            MatchEvaluator matchLine = (m) =>
            {
                return m.Value;
            };
            MatchEvaluator matchEval = (m) =>
            {
                var match = reLine.Match(m.Value); // first match([=[ tagName)
                if (match.Success)
                {
                    System.Collections.Generic.List<string> retList = new System.Collections.Generic.List<string>();
                    var groups = new System.Collections.Generic.HashSet<string>((from x in dt.AsEnumerable() where x.Field<string>("TagName") == match.Groups[2].Value.Trim() select x.Field<string>("TagGrp")));
                    var rows = from x in dt.AsEnumerable()
                               where x.Field<string>("TagGrp") != null && groups.Contains((x.Field<string>("TagGrp") ?? ""))
                               group x by x.Field<string>("TagGrp");

                    foreach (var g in rows)
                    {
                        var val = m.Value;
                        foreach (var r in g)
                        {
                            string tag = r["TagName"].ToString();
                            string tagValue = r["TagValue"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                            try
                            {
                                Decimal.Parse(tagValue);
                                val = val.Replace("<Data ss:Type=\"String\">" + tag + "</Data>", "<Data ss:Type=\"Number\">" + tagValue + "</Data>");

                            }
                            catch
                            {
                                val = val.Replace(tag, tagValue);
                            }
                        }
                        retList.Add(val);
                    }
                    return string.Join("\r\n", retList.ToArray());
                }
                else return m.Value;
            };
            xlsXML = reRow.Replace(xlsXML, matchEval);

            // we have modified the row counts so must remove this
            Regex reRowCount = new Regex("ss:ExpandedRowCount=\"[0-9]*\"");
            xlsXML = reRowCount.Replace(xlsXML, " ");

            return System.Text.Encoding.UTF8.GetBytes(xlsXML);
        }
        private byte[] MergeToExcel(byte[] tmplXls, DataTable dt)
        {
            DataTable dtTag = new DataTable();
            dtTag.Columns.Add("TagName",typeof(string));
            dtTag.Columns.Add("TagValue",typeof(string));
            dtTag.Columns.Add("TagGrp", typeof(string));
            if (!dt.Columns.Contains("TagName"))
            {
                // we output one single copy of the first record so the 'common used once' column can be used
                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        DataRow dr = dtTag.NewRow();
                        dr["TagName"] = "[[" + dc.ColumnName + "]]";
                        dr["TagValue"] = dt.Rows[0][dc.ColumnName].ToString();
                        dtTag.Rows.Add(dr);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    // now make them multiple line friendly
                    int rowId = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            DataRow drt = dtTag.NewRow();
                            drt["TagGrp"] = "[=Row[" + rowId.ToString();
                            drt["TagName"] = "[=[" + dc.ColumnName + "]]";
                            drt["TagValue"] = dt.Rows[rowId][dc.ColumnName].ToString();
                            dtTag.Rows.Add(drt);
                        }
                        rowId = rowId + 1;
                    }
                }
            }
            else
            {
                dtTag = dt;
            }
            string fileSig = System.Text.Encoding.UTF8.GetString(tmplXls, 0, 5);
            if (fileSig == "<?xml")
            {
                return ConvertXML2XLS(MergeToExcel2003WorkSheet(tmplXls, dtTag));
            }
            else if (fileSig.StartsWith("PK"))
            {
                System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, byte[]>> replacedSheets = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, byte[]>>();

                using (MemoryStream msOut = new MemoryStream())
                using (Ionic.Zip.ZipFile resultFile = new Ionic.Zip.ZipFile())
                {
                    resultFile.CompressionMethod = Ionic.Zip.CompressionMethod.Deflate;
                    resultFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    using (MemoryStream ms = new MemoryStream(tmplXls))
                    using (Ionic.Zip.ZipFile tmplFile = Ionic.Zip.ZipFile.Read(ms))
                    {
                        System.Collections.Generic.List<string> shareStringList = new System.Collections.Generic.List<string>();

                        using (MemoryStream msShareString = new MemoryStream())
                        {
                            tmplFile["xl/sharedStrings.xml"].Extract(msShareString);
                            string stringXml = System.Text.Encoding.UTF8.GetString(msShareString.ToArray());
                            Regex reShare = new Regex("<si[^>]*>(.*?)</si>");
                            foreach (Match m in reShare.Matches(stringXml))
                            {
                                shareStringList.Add(m.Groups[1].Value);
                            }
                        }
                        foreach (Ionic.Zip.ZipEntry srcEntry in tmplFile.Entries)
                        {
                            string sheetName = srcEntry.FileName;
                            using (MemoryStream msEntry = new MemoryStream())
                            {
                                srcEntry.Extract(msEntry);
                                if ((sheetName.StartsWith("xl/worksheets/") && !sheetName.Contains("_rels")) || sheetName.StartsWith("xl/sharedStrings"))
                                {
                                    byte[] merged = MergeToExcel2007WorkSheet(msEntry.ToArray(), dtTag, sheetName.StartsWith("xl/sharedStrings"), shareStringList);
                                    Ionic.Zip.ZipEntry entry = resultFile.AddEntry(srcEntry.FileName, merged);

                                }
                                else if (sheetName.StartsWith("xl/workbook.xml"))
                                {
                                    string wbXML = System.Text.Encoding.UTF8.GetString(msEntry.ToArray());
                                    Regex re = new Regex("(<calcPr)([^>/]*)(/>)");
                                    string result = re.Replace(wbXML, s =>
                                    {
                                        if (s.Value.Contains("fullCalcOnLoad")) return s.Value;
                                        else return s.Groups[1].Value + s.Groups[2].Value + " fullCalcOnLoad=\"1\" " + s.Groups[3].Value;
                                    });
                                    Ionic.Zip.ZipEntry entry = resultFile.AddEntry(srcEntry.FileName, System.Text.Encoding.UTF8.GetBytes(result));
                                }
                                else
                                {
                                    if (srcEntry.IsDirectory)
                                    {
                                        resultFile.AddDirectoryByName(srcEntry.FileName);
                                    }
                                    else resultFile.AddEntry(srcEntry.FileName, msEntry.ToArray());
                                }

                            }
                        }
                    }
                    resultFile.Save(msOut);
                    msOut.Close();
                    byte[] output = msOut.ToArray();
                    using (MemoryStream mstest = new MemoryStream(output))
                    {
                        Ionic.Zip.ZipFile f = Ionic.Zip.ZipFile.Read(mstest);
                    }
                    return output;
                }
            }
            else
            {
                throw new Exception("The template must be in either XML Spreadsheet(.xml) or Excel 2007 Workbook(.xlsx or .xlsm) format");
            }
        }
        
        private void DoRtfRpt(DataSet ds)
		{
			StringBuilder sb = new StringBuilder();
			DataTable dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, false, false, false);
			if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
            CovertRptUTC(dt);
            string reportName = GetReportHlp().Rows[0]["ProgramName"].ToString();
			if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
			DataTable dtTemplate = null;
			Int16 iPrev = -1;
			Int16 iCurr;
			int ii = 0;
			string ss = string.Empty;
			while (ii < dt.Rows.Count)
			{
				if (dt.Columns.Contains("TemplateId")) { try { iCurr = Int16.Parse(dt.Rows[ii]["TemplateId"].ToString()); } catch { iCurr = 0; } } else { iCurr = 0; }
				if (iCurr != iPrev)
				{
					dtTemplate = (new SqlReportSystem()).GetDocImage(QueryStr["rpt"].ToString(), iCurr, LcSysConnString, LcAppPw);
					ss = UTF8Encoding.UTF8.GetString((byte[])dtTemplate.Rows[0]["DocImage"]);
					ss = CleanDoc(true, ss, "[", "[", 1);			// Clean up garbage between [[;
					ss = CleanDoc(true, ss, "]", "]", 1);			// Clean up garbage between ]];
					ss = CleanDoc(false, ss, "[[", "]]", 2);		// Clean up garbage among column tags;
					iPrev = iCurr;
				}
				sb.Append(MergeValues(ss, ii, dt));
				ii = ii + 1;
			}
			sb.Replace("}{\\rtf1", "\\page ");
			ExportToStream(reportName + ".rtf", sb);
		}

		private void DoXlsRpt(DataSet ds)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<?xml version=\"1.0\"?>" + Environment.NewLine);
			sb.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"" + Environment.NewLine);
			sb.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\"" + Environment.NewLine);
			sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" + Environment.NewLine);
			sb.Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"" + Environment.NewLine);
			sb.Append("xmlns:html=\"http://www.w3.org/TR/REC-html40\">" + Environment.NewLine);
			DataTable dt = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, false, false, false);
			if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
			string reportName = GetReportHlp().Rows[0]["ProgramName"].ToString();
            CovertRptUTC(dt);
            if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
			foreach (DataRow dr in dt.Rows)
			{
				sb.Append((dr["ReportXml"].ToString() + (char)13));
			}
			sb.Append(("</Workbook>" + (char)13));

            byte[] content = ConvertXML2XLS(Encoding.UTF8.GetBytes(sb.ToString()));
            ExportToStream(reportName + ".xls", content);
			//ExportToStream(reportName + ".xls", sb);
		}

		private string CleanDoc(bool bPrep, string sDoc, string s1, string s2, int iLen)
		{
			string sClean = "";
			int i1 = 0;
			int i2 = 0;
			while (i1 >= 0)
			{
				i1 = sDoc.IndexOf(s1, 0);
				if (i1 < 0) { sClean = sClean + sDoc; }
				else
				{
					i2 = sDoc.IndexOf(s2, i1 + 1);
					if (i2 < 0) { sClean = sClean + sDoc; i1 = -1; }
					else
					{
						sClean = sClean + sDoc.Substring(0, i1 + iLen) + CleanTag(bPrep, sDoc.Substring(i1 + iLen, i2 - i1));
						sDoc = sDoc.Substring(i2 + iLen);
					}
				}
			}
			return sClean;
		}

		private string CleanTag(bool bPrep, string sTag)
		{
			sTag = StripTag(sTag, (char)13);		// Strip Carriage Returns
			sTag = StripTag(sTag, (char)10);		// Strip Line Feeds
			// Strip unwanted formating between special characters
			string sr = "";
			string delimStr = "} ";
			char[] dd = delimStr.ToCharArray();
			string[] sa = sTag.Split(dd);
			foreach (string ss in sa)
			{
				if (ss.IndexOf("{", 0) < 0) { sr = sr + ss; }
			}
			if (bPrep && sr.Length > 1)		// Get around an unknown and invisible character
			{
				return sTag;	// in case user has useful info between [[ that is more than one character long;
			}
			else
			{
				return sr;
			}
		}

		private string StripTag(string sTag, char cs)
		{
			// Strip sTag clean of all special character cs
			int ii;
			ii = sTag.IndexOf(cs);
			while (ii >= 0)
			{
				sTag = sTag.Remove(ii, 1);
				ii = sTag.IndexOf(cs);
			}
			return sTag;
		}

		private void ExportToStream(string sFileName, StringBuilder sb)
		{
			System.IO.Stream oStream = null;
			StreamWriter sw = null;
			oStream = new MemoryStream();
			sw = new StreamWriter(oStream, System.Text.Encoding.Default);
			sw.WriteLine(sb);
			sw.Flush();
			oStream.Seek(0, SeekOrigin.Begin);
			Response.Buffer = true;
			Response.ClearHeaders();
			Response.ClearContent();
            if (sFileName.EndsWith(".xls") || sFileName.EndsWith(".xlsx")) Response.ContentType = "application/vnd.ms-excel";
            else if (sFileName.EndsWith(".rtf")) Response.ContentType = "text/rtf";
            else Response.ContentType = "APPLICATION/OCTET-STREAM";
            Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + sFileName);
			byte[] streamByte = new byte[oStream.Length];
			oStream.Read(streamByte, 0, (int)oStream.Length);
            Response.BinaryWrite(streamByte);
			Response.End();
			if (oStream != null) { oStream.Close(); }
			if (sw != null) { sw.Close(); }
		}

        private void ExportToStream(string sFileName, byte[] content)
        {
            string fileSig = System.Text.Encoding.UTF8.GetString(content, 0, 5);
            bool isXLSX = fileSig.StartsWith("PK");

            string mime = "application/octet-stream";
            if ((sFileName.EndsWith(".xls") || sFileName.EndsWith(".xml")) && !isXLSX)
            {
                mime = "application/vnd.ms-excel";
            }
            else if (sFileName.EndsWith(".xlsx") || ((sFileName.EndsWith(".xls") || sFileName.EndsWith(".xml")) && isXLSX))
            {
                mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else if (sFileName.EndsWith(".xlsm"))
            {
                mime = "application/vnd.ms-excel.sheet.macroEnabled.12";
            }
            Response.Buffer = true;
            Response.ClearHeaders();
            Response.ClearContent();
            if (sFileName.EndsWith(".xls") || sFileName.EndsWith(".xlsx") || sFileName.EndsWith(".xlsm") || sFileName.EndsWith(".xml")) Response.ContentType = mime;
            else if (sFileName.EndsWith(".rtf")) Response.ContentType = "text/rtf";
            else Response.ContentType = "application/octet-stream";
            if (sFileName.EndsWith(".xml")) sFileName = sFileName.Replace(".xml", ".xls" + (isXLSX ? "x" : ""));
            Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + sFileName);
            Response.BinaryWrite(content);
            Response.End();
            if (!string.IsNullOrEmpty(Request["runas"])) Session.Abandon();
        }

		private string MergeValues(string ss, int ii, DataTable dt)
		{
			string str;
			DataView dvObj = GetSqlColumns();
			foreach (DataRowView drv in dvObj)
			{
				try
				{
					if (cSelectList.Visible && !cSelectList.Items.FindByValue(drv["ColumnName"].ToString()).Selected)
					{
						ss = ss.Replace("[[" + drv["ColumnName"].ToString() + "]]", string.Empty);
					}
					else
					{
						str = dt.Rows[ii][drv["ColumnName"].ToString()].ToString();
						if (drv["columnFormat"].ToString() != string.Empty)
						{
                            if (drv["columnFormat"].ToString().StartsWith("_") && drv["columnFormat"].ToString().IndexOf("MM") >= 0 && drv["columnFormat"].ToString().IndexOf("hh") < 0)
                            {
                                /* unfortunately, have to introduce special prefix due to all over the place formatting in 
                                 * existing systems and this is only good for long date
                                 */
                                str = RO.Common3.Utils.fmLongDate(str, LUser.Culture, drv["columnFormat"].ToString().Substring(1));
                            }
                            else if (drv["columnFormat"].ToString().StartsWith("_") && drv["columnFormat"].ToString().IndexOf("#") >= 0 && drv["columnFormat"].ToString().IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) >= 0)
                            {
                                /* unfortunately, have to introduce special prefix due to all over the place formatting in 
                                 * expect _#.(no decimal) or _#.00(two decimal)
                                 */

                                string scale = drv["columnFormat"].ToString().Substring(drv["columnFormat"].ToString().LastIndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)).Trim();

                                str = RO.Common3.Utils.fmNumeric(scale.Length <= 1 ? "0" : scale.Replace(".","").Length.ToString(), str, LUser.Culture);
                            }
                            else if (drv["columnFormat"].ToString().IndexOf("MMM") >= 0 || drv["columnFormat"].ToString() == "D")
							{
								if (drv["columnFormat"].ToString().IndexOf("hh") >= 0) { str = RO.Common3.Utils.fmLongDateTime(str, LUser.Culture); } else { str = RO.Common3.Utils.fmLongDate(str, LUser.Culture); }
							}
							else if (drv["columnFormat"].ToString().IndexOf("MM") >= 0 || drv["columnFormat"].ToString() == "d")
							{
								if (drv["columnFormat"].ToString().IndexOf("hh") >= 0) { str = RO.Common3.Utils.fmShortDateTime(str, LUser.Culture); } else { str = RO.Common3.Utils.fmShortDate(str, LUser.Culture); }
							}
							else if (drv["columnFormat"].ToString().IndexOf("$") >= 0 || drv["columnFormat"].ToString() == "c") { str = RO.Common3.Utils.fmCurrency(str, LUser.Culture); }
							else if (drv["columnFormat"].ToString().IndexOf("#") >= 0 || drv["columnFormat"].ToString() == "n") { str = RO.Common3.Utils.fmMoney(str, LUser.Culture); }
						}
						if (drv["PaddSize"].ToString() != string.Empty)
						{
							ss = ss.Replace("[[" + drv["ColumnName"].ToString() + "]]", str.PadLeft(int.Parse(drv["PaddSize"].ToString()), Convert.ToChar(drv["PaddChar"].ToString())));
						}
						else
						{
							ss = ss.Replace("[[" + drv["ColumnName"].ToString() + "]]", str);
						}
					}
				}
				catch { ss = ss.Replace("[[" + drv["ColumnName"].ToString() + "]]", string.Empty); }
			}
			return ss;
		}


		private void LoadGauge(DataSet ds)
		{
			cPlaceHolder.Controls.Clear();
			double MinValue;
			double LowRange;
			double MidRange;
			double MaxValue;
			double Needle;
			StringBuilder sb = new StringBuilder();
			sb.Append("<script type='text/javascript'>" + Environment.NewLine);
			sb.Append("Event.observe(window, 'load', function() {loadcanvas('cv" + this.ID + "',");
			DataTable dtn = (new SqlReportSystem()).GetSqlReport(QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), GetSqlCriteria(), base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, false, false, false);
			if (dtn == null || dtn.Rows.Count <= 0 || dtn.Columns.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
			DataTable dts = (new SqlReportSystem()).GetGaugeValue(QueryStr["rpt"].ToString(), LcSysConnString, LcAppPw);
			if (dts.Rows[0]["GMinValueCol"].ToString() != string.Empty)
			{
				try { MinValue = double.Parse(dtn.Rows[0][dts.Rows[0]["GMinValueCol"].ToString()].ToString()); }
				catch { throw new ApplicationException("Expecting Minimal Value to be numeric. Please rectify and try again."); }
			} else MinValue = double.Parse(dts.Rows[0]["GMinValue"].ToString());
			if (dts.Rows[0]["GLowRangeCol"].ToString() != string.Empty)
			{
				try { LowRange = double.Parse(dtn.Rows[0][dts.Rows[0]["GLowRangeCol"].ToString()].ToString()); }
				catch { throw new ApplicationException("Expecting Low-Range-Maximum to be numeric. Please rectify and try again."); }
			} else LowRange = double.Parse(dts.Rows[0]["GLowRange"].ToString());
			if (dts.Rows[0]["GMidRangeCol"].ToString() != string.Empty)
			{
				try { MidRange = double.Parse(dtn.Rows[0][dts.Rows[0]["GMidRangeCol"].ToString()].ToString()); }
				catch { throw new ApplicationException("Expecting Mid-Range-Maximum to be numeric. Please rectify and try again."); }
			} else MidRange = double.Parse(dts.Rows[0]["GMidRange"].ToString());
			if (dts.Rows[0]["GMaxValueCol"].ToString() != string.Empty)
			{
				try { MaxValue = double.Parse(dtn.Rows[0][dts.Rows[0]["GMaxValueCol"].ToString()].ToString()); }
				catch { throw new ApplicationException("Expecting Maximum Value to be numeric. Please rectify and try again."); }
			} else MaxValue = double.Parse(dts.Rows[0]["GMaxValue"].ToString());
			if (dts.Rows[0]["GNeedleCol"].ToString() != string.Empty)
			{
				try { Needle = double.Parse(dtn.Rows[0][dts.Rows[0]["GNeedleCol"].ToString()].ToString()); }
				catch { throw new ApplicationException("Expecting Needle Value to be numeric. Please rectify and try again."); }
			} else Needle = double.Parse(dts.Rows[0]["GNeedle"].ToString());

			if (LowRange < MinValue)
			{
				throw new ApplicationException("Expecting Low-Range-Maximum to be greater than or equal to Minimum Value. Please rectify and try again.");
			}
			if (MidRange < LowRange)
			{
				throw new ApplicationException("Expecting Mid-Range-Maximum to be greater than or equal to Low-Range-Maximum. Please rectify and try again.");
			}
			if (MaxValue < MidRange)
			{
				throw new ApplicationException("Expecting Maximum value to be greater than or equal to Mid-Range-Maximum. Please rectify and try again.");
			}

			sb.Append(Needle.ToString() + "," + MinValue.ToString() + "," + LowRange.ToString() + "," + MidRange.ToString() + "," + MaxValue.ToString() + ",'" + dts.Rows[0]["GPositive"].ToString() + "');});" + (char)13 + "</script>");
			cPlaceHolder.Controls.Add(new LiteralControl(sb.ToString()));

			Literal cLiteral1 = new Literal();
			cLiteral1.Text = "<table><tr><td colspan=\"3\" align=\"center\">"; cPlaceHolder.Controls.Add(cLiteral1);
			Literal cLiteral2 = new Literal();
			cLiteral2.Text = "</tr><tr><td colspan=\"3\" align=\"left\"><canvas id=\"cv" + this.ID + "\" width=\"400\" height=\"200\" style=\"border:none\"></canvas></td></tr><tr><td align=\"left\">";
			cPlaceHolder.Controls.Add(cLiteral2);
			Label cLow = new Label();
			cLow.ID = "cLow"; cLow.CssClass = "inp-lbl"; cLow.Text = MinValue.ToString(dts.Rows[0]["GFormat"].ToString(), new System.Globalization.CultureInfo(LUser.Culture)); cPlaceHolder.Controls.Add(cLow);
			Literal cLiteral3 = new Literal();
			cLiteral3.Text = "</td><td align=\"center\" width=\"33%\">"; cPlaceHolder.Controls.Add(cLiteral3);
			Label cNeedle = new Label();
			cNeedle.ID = "cNeedle"; cNeedle.CssClass = "inp-txt"; cNeedle.Text = Needle.ToString(dts.Rows[0]["GFormat"].ToString(), new System.Globalization.CultureInfo(LUser.Culture)); cPlaceHolder.Controls.Add(cNeedle);
			if (Needle < MinValue || Needle > MaxValue) { cNeedle.Style.Add("color", "red"); }
			Literal cLiteral4 = new Literal();
			cLiteral4.Text = "</td><td align=\"right\" width=\"33%\">"; cPlaceHolder.Controls.Add(cLiteral4);
			Label cHigh = new Label();
			cHigh.ID = "cHigh"; cHigh.CssClass = "inp-lbl"; cHigh.Text = MaxValue.ToString(dts.Rows[0]["GFormat"].ToString(), new System.Globalization.CultureInfo(LUser.Culture)); cPlaceHolder.Controls.Add(cHigh);
			Literal cLiteral5 = new Literal();
			cLiteral5.Text = "</td></tr></table>"; cPlaceHolder.Controls.Add(cLiteral5);
			cPlaceHolder.Visible = true;
		}
	}
}
