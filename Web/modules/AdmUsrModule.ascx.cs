using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.WebRules;

namespace RO.Common3.Data
{
	public class AdmUsr1 : DataSet
	{
		public AdmUsr1()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmUsr")));
			this.DataSetName = "AdmUsr1";
			this.Namespace = "http://Rintagi.com/DataSet/AdmUsr1";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("UsrId1", typeof(string));
			columns.Add("LoginName1", typeof(string));
			columns.Add("UsrName1", typeof(string));
			columns.Add("CultureId1", typeof(string));
			columns.Add("DefCompanyId1", typeof(string));
			columns.Add("DefProjectId1", typeof(string));
			columns.Add("DefSystemId1", typeof(string));
			columns.Add("UsrEmail1", typeof(string));
			columns.Add("UsrMobile1", typeof(string));
			columns.Add("UsrGroupLs1", typeof(string));
			columns.Add("IPAlert1", typeof(string));
			columns.Add("PwdNoRepeat1", typeof(string));
			columns.Add("PwdDuration1", typeof(string));
			columns.Add("PwdWarn1", typeof(string));
			columns.Add("Active1", typeof(string));
			columns.Add("InternalUsr1", typeof(string));
			columns.Add("TechnicalUsr1", typeof(string));
			columns.Add("EmailLink1", typeof(string));
			columns.Add("MobileLink1", typeof(string));
			columns.Add("FailedAttempt1", typeof(string));
			columns.Add("LastSuccessDt1", typeof(string));
			columns.Add("LastFailedDt1", typeof(string));
			columns.Add("CompanyLs1", typeof(string));
			columns.Add("ProjectLs1", typeof(string));
			columns.Add("HintQuestionId1", typeof(string));
			columns.Add("HintAnswer1", typeof(string));
			columns.Add("InvestorId1", typeof(string));
			columns.Add("CustomerId1", typeof(string));
			columns.Add("VendorId1", typeof(string));
			columns.Add("AgentId1", typeof(string));
			columns.Add("BrokerId1", typeof(string));
			columns.Add("MemberId1", typeof(string));
			columns.Add("LenderId1", typeof(string));
			columns.Add("BorrowerId1", typeof(string));
			columns.Add("GuarantorId1", typeof(string));
			columns.Add("UsageStat", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmUsrModule : RO.Web.ModuleBase
	{
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_1";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_1";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_1";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_1";
		private const string KEY_dtLabel = "Cache:dtLabel3_1";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_1";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_1";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_1";

		private const string KEY_dtCultureId1 = "Cache:dtCultureId1";
		private const string KEY_dtDefCompanyId1 = "Cache:dtDefCompanyId1";
		private const string KEY_dtDefProjectId1 = "Cache:dtDefProjectId1";
		private const string KEY_dtDefSystemId1 = "Cache:dtDefSystemId1";
		private const string KEY_dtUsrGroupLs1 = "Cache:dtUsrGroupLs1";
		private const string KEY_dtUsrImprLink1 = "Cache:dtUsrImprLink1";
		private const string KEY_dtCompanyLs1 = "Cache:dtCompanyLs1";
		private const string KEY_dtProjectLs1 = "Cache:dtProjectLs1";
		private const string KEY_dtHintQuestionId1 = "Cache:dtHintQuestionId1";
		private const string KEY_dtInvestorId1 = "Cache:dtInvestorId1";
		private const string KEY_dtCustomerId1 = "Cache:dtCustomerId1";
		private const string KEY_dtVendorId1 = "Cache:dtVendorId1";
		private const string KEY_dtAgentId1 = "Cache:dtAgentId1";
		private const string KEY_dtBrokerId1 = "Cache:dtBrokerId1";
		private const string KEY_dtMemberId1 = "Cache:dtMemberId1";
		private const string KEY_dtLenderId1 = "Cache:dtLenderId1";
		private const string KEY_dtBorrowerId1 = "Cache:dtBorrowerId1";
		private const string KEY_dtGuarantorId1 = "Cache:dtGuarantorId1";

		private const string KEY_dtSystems = "Cache:dtSystems3_1";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_1";
		private const string KEY_dtAdmUsr1List = "Cache:dtAdmUsr1List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_1";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_1";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_1";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_1";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_1";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_1";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_1";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_1";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmUsrModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
			try { ScriptManager.RegisterStartupScript(this, this.GetType(), "datepicker_i18n", File.ReadAllText(Request.MapPath("~/scripts/i18n/jquery.ui.datepicker-" + lang + ".js")), true); } catch { };
			if (!IsPostBack) { Session.Remove(KEY_dtScrCri); }
			int ii = 0; DataView dvCri = GetScrCriteria(); SetCriHolder(ii, dvCri);
			bConfirm.Value = "Y";
			// To get around ajax not displaying ErrMsg and InfoMsg; Set them to Y to show immediately:
			bErrNow.Value = "N"; bInfoNow.Value = "N"; bExpNow.Value = "N";
			CtrlToFocus.Value = string.Empty;
			EnableValidators(false);
			if (!IsPostBack)
			{
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,1, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				Session.Remove(KEY_dtAdmUsr1List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtCultureId1);
				Session.Remove(KEY_dtDefCompanyId1);
				Session.Remove(KEY_dtDefProjectId1);
				Session.Remove(KEY_dtDefSystemId1);
				Session.Remove(KEY_dtUsrGroupLs1);
				Session.Remove(KEY_dtUsrImprLink1);
				Session.Remove(KEY_dtCompanyLs1);
				Session.Remove(KEY_dtProjectLs1);
				Session.Remove(KEY_dtHintQuestionId1);
				Session.Remove(KEY_dtInvestorId1);
				Session.Remove(KEY_dtCustomerId1);
				Session.Remove(KEY_dtVendorId1);
				Session.Remove(KEY_dtAgentId1);
				Session.Remove(KEY_dtBrokerId1);
				Session.Remove(KEY_dtMemberId1);
				Session.Remove(KEY_dtLenderId1);
				Session.Remove(KEY_dtBorrowerId1);
				Session.Remove(KEY_dtGuarantorId1);
				SetButtonHlp();
				GetSystems();
				SetColumnAuthority();
				GetGlobalFilter();
				GetScreenFilter();
				GetCriteria(dvCri);
				DataTable dtHlp = GetScreenHlp();
				cHelpMsg.HelpTitle = dtHlp.Rows[0]["ScreenTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
				cFootLabel.Text = dtHlp.Rows[0]["FootNote"].ToString();
				if (cFootLabel.Text == string.Empty) { cFootLabel.Visible = false; }
				cTitleLabel.Text = dtHlp.Rows[0]["ScreenTitle"].ToString();
				DataTable dt = GetScreenTab();
				cTab1.InnerText = dt.Rows[0]["TabFolderName"].ToString();
				cTab2.InnerText = dt.Rows[1]["TabFolderName"].ToString();
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 1, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				//WebRule: Set filter on criteria "User Name" change
	                if (!string.IsNullOrEmpty(((TextBox)cCriteria.FindControl("x" + (GetScrCriteria())[1]["ColumnName"].ToString())).Text)) { cFilterId.SelectedIndex = 4; }
				// *** WebRule End *** //
				PopAdmUsr1List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=1&typ=N&sys=3','','',''); return false;"; }
				cCultureId1Search_Script();
				cDefSystemId1Search_Script();
				cUsrGroupLs1Search_Script();
				// *** Page Load (End of) Web Rule starts here *** //
			}
			if (IsPostBack && !ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack) {SetClientRule(null,false);};
			if (!IsPostBack)	// Test for Viewstate being lost.
			{
				bViewState.Text = "Y";
				string xx = Session["Idle:" + Request.Url.PathAndQuery] as string;
				if (xx == "Y")
				{
					Session.Remove("Idle:" + Request.Url.PathAndQuery);
					bInfoNow.Value = "Y"; PreMsgPopup("Page has been idled past preset limit and no longer valid, please be notified that it has been reloaded.");
				}
			}
			else if (string.IsNullOrEmpty(bViewState.Text))		// Viewstate is lost.
			{
				Session["Idle:" + Request.Url.PathAndQuery] = "Y"; Response.Redirect(Request.Url.PathAndQuery);
			}
			if (!string.IsNullOrEmpty(cCurrentTab.Value))
			{
				HtmlControl wcTab = this.FindControl(cCurrentTab.Value.Substring(1)) as HtmlControl;
				wcTab.Style["display"] = "block";
			}
			ScriptManager.RegisterStartupScript(this, this.GetType(), "ScreenLabel", this.ClientID + "={" + string.Join(",", (from dr in GetLabel().AsEnumerable() select string.Format("{0}: {{msg:'{1}',hint:'{2}'}}", "c" + dr.Field<string>("ColumnName") + (dr.Field<int?>("TableId").ToString()), (dr.Field<string>("ErrMessage") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"), (dr.Field<string>("TbHint") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"))).ToArray()) + "};", true);
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			// *** Page Init (Front of) Web Rule starts here *** //
			InitializeComponent();
			//WebRule: Usage calendar display (1 of 2) and height customization
            if (!IsPostBack)
            {
                string defaultYear = DateTime.Today.Year.ToString();
                string defaultMonth = DateTime.Today.Month.ToString();
                string init = "{colorMap:{'R':'less-hour', 'G':'norm-hour', 'B':'over-hour'}}";
                string script =
                @"<script type='text/javascript' language='javascript'>
                DC={data:[]};
                Sys.Application.add_load(function(){DrawCalenderPlanner(DC," + defaultYear + "," + defaultMonth + "," + init + ",'" + cUsageStat.ClientID + @"');});
                </script>";
                ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "CalendarInit", script, false);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CalendarStyleCSS", @"<style>
                .responsive-calendar .day {height:35px;}
                .responsive-calendar .day.less-hour {background-color:#e32403;}
                .responsive-calendar .day.over-hour {background-color:#4100d9;}
                .responsive-calendar .day.norm-hour {background-color:#00cf80;}
            </style>", false);
			// *** WebRule End *** //
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			CheckAuthentication(true);
			if (LcSysConnString == null)
			{
				SetSystem(3);
			}

		}
		#endregion

		private void SetSystem(byte SystemId)
		{
			LcSystemId = SystemId;
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcDesDb = base.DesDb(SystemId);
			LcAppDb = base.AppDb(SystemId);
			LcAppPw = base.AppPwd(SystemId);
			try
			{
				base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
				DataRow row = base.SystemsList.Rows.Find(SystemId);
				base.CSrc = new CurrSrc(true, row);
				base.CTar = new CurrTar(true, row);
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,1,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(1, "User Manager", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					Response.Redirect(Request.RawUrl);
				}
			}
			catch (Exception e) { bErrNow.Value = "Y"; PreMsgPopup(e.Message); }
		}

		private void EnableValidators(bool bEnable)
		{
			foreach (System.Web.UI.WebControls.WebControl va in Page.Validators)
			{
				if (bEnable) {va.Enabled = true;} else {va.Enabled = false;}
			}
		}

		private void CheckAuthentication(bool pageLoad)
		{
          if (IsCronInvoked()) AnonymousLogin();
          else CheckAuthentication(pageLoad, true);
		}

		private void SetButtonHlp()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetButtonHlp(1,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						if (dr["ButtonTypeName"].ToString() == "ClearCri") { cClearCriButton.CssClass = "ButtonImg ClearCriButtonImg"; Session[KEY_bClCriVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cClearCriButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Save") { cSaveButton.CssClass = "ButtonImg SaveButtonImg"; cSaveButton.Text = dr["ButtonName"].ToString(); cSaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bUpdateVisible] = cSaveButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "ExpTxt") { cExpTxtButton.CssClass = "ButtonImg ExpTxtButtonImg"; cExpTxtButton.Text = dr["ButtonName"].ToString(); cExpTxtButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpTxtButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "ExpRtf") { cExpRtfButton.CssClass = "ButtonImg ExpRtfButtonImg"; cExpRtfButton.Text = dr["ButtonName"].ToString(); cExpRtfButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpRtfButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "UndoAll") { cUndoAllButton.CssClass = "ButtonImg UndoAllButtonImg"; cUndoAllButton.Text = dr["ButtonName"].ToString(); cUndoAllButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cUndoAllButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bUndoAllVisible] = cUndoAllButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "More") { cMoreButton.CssClass = "ButtonImg MoreButtonImg"; cMoreButton.Text = dr["ButtonName"].ToString(); cMoreButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cMoreButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "SaveClose") { cSaveCloseButton.CssClass = "ButtonImg SaveCloseButtonImg"; cSaveCloseButton.Text = dr["ButtonName"].ToString(); cSaveCloseButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSaveCloseButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Edit") { cEditButton.CssClass = "ButtonImg EditButtonImg"; cEditButton.Text = dr["ButtonName"].ToString(); cEditButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cEditButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Preview") { cPreviewButton.CssClass = "ButtonImg PreviewButtonImg"; cPreviewButton.Text = dr["ButtonName"].ToString(); cPreviewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPreviewButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						if (dr["ButtonTypeName"].ToString() == "Audit") { cAuditButton.CssClass = "ButtonImg AuditButtonImg"; cAuditButton.Text = dr["ButtonName"].ToString(); cAuditButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cAuditButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						if (dr["ButtonTypeName"].ToString() == "Clear") { cClearButton.CssClass = "ButtonImg ClearButtonImg"; cClearButton.Text = dr["ButtonName"].ToString(); cClearButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cClearButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "New") { cNewButton.CssClass = "ButtonImg NewButtonImg"; cNewButton.Text = dr["ButtonName"].ToString(); cNewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNewButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bNewVisible] = cNewButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "NewSave") { cNewSaveButton.CssClass = "ButtonImg NewSaveButtonImg"; cNewSaveButton.Text = dr["ButtonName"].ToString(); cNewSaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNewSaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bNewSaveVisible] = cNewSaveButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "Copy") { cCopyButton.CssClass = "ButtonImg CopyButtonImg"; cCopyButton.Text = dr["ButtonName"].ToString(); cCopyButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cCopyButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bCopyVisible] = cCopyButton.Visible;}
						else if (dr["ButtonTypeName"].ToString() == "CopySave") { cCopySaveButton.CssClass = "ButtonImg CopySaveButtonImg"; cCopySaveButton.Text = dr["ButtonName"].ToString(); cCopySaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cCopySaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bCopySaveVisible] = cCopySaveButton.Visible;}
						else if (dr["ButtonTypeName"].ToString() == "Delete") { cDeleteButton.CssClass = "ButtonImg DeleteButtonImg"; cDeleteButton.Text = dr["ButtonName"].ToString(); cDeleteButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cDeleteButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bDeleteVisible] = cDeleteButton.Visible;}
					}
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private DataTable GetClientRule()
		{
			DataTable dtRul = (DataTable)Session[KEY_dtClientRule];
			if (dtRul == null)
			{
				CheckAuthentication(false);
				try
				{
					dtRul = (new AdminSystem()).GetClientRule(1,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtClientRule] = dtRul;
			}
			return dtRul;
		}

		private void SetClientRule(ListViewDataItem ee, bool isEditItem)
		{
			DataView dvRul = new DataView(GetClientRule());
			if (dvRul.Count > 0)
			{
				WebControl cc = null;
				string srp = string.Empty;
				string sn = string.Empty;
				string st = string.Empty;
				string sg = string.Empty;
				int ii = 0;
				int jj = 0;
				Regex missing = new Regex("@[0-9]+@");
				string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
				string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
				foreach (DataRowView drv in dvRul)
				{
					if (ee == null)
					{
						srp = drv["ScriptName"].ToString();
						srp = srp.Replace("@cLang@", "\'" + lang + "\'");
						if (drv["ParamName"].ToString() != string.Empty)
						{
							StringBuilder sbName =  new StringBuilder();
							StringBuilder sbType =  new StringBuilder();
							StringBuilder sbInGrid =  new StringBuilder();
							sbName.Append(drv["ParamName"].ToString().Trim());
							sbType.Append(drv["ParamType"].ToString().Trim());
							sbInGrid.Append(drv["ParamInGrid"].ToString().Trim());
							ii = 0;
							while (sbName.Length > 0)
							{
								sn = Utils.PopFirstWord(sbName,(char)44); st = Utils.PopFirstWord(sbType,(char)44);sg = Utils.PopFirstWord(sbInGrid,(char)44);
								if (ee != null && sg == "Y")
								{
									if (st.ToLower() == "combobox" && isEditItem) {srp = srp.Replace("@" + ii.ToString() + "@","'"+((RoboCoder.WebControls.ComboBox)ee.FindControl(sn)).KeyID+"'");} else {srp = srp.Replace("@" + ii.ToString() + "@","'"+((WebControl)ee.FindControl(sn + (!isEditItem ? "l" : ""))).ClientID+"'");}
								}
								else
								{
									if (st.ToLower() == "combobox") {srp = srp.Replace("@" + ii.ToString() + "@","'"+((RoboCoder.WebControls.ComboBox)this.FindControl(sn)).KeyID+"'");} else {srp = srp.Replace("@" + ii.ToString() + "@","'"+((WebControl)this.FindControl(sn)).ClientID+"'");}
								}
								ii = ii + 1;
							}
						}
						if (drv["ScriptEvent"].ToString() == "js")
						{
							ScriptManager.RegisterStartupScript(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<script>"+ drv["ClientRuleProg"].ToString() + srp + "</script>", false);
						}
						else if (drv["ScriptEvent"].ToString() == "css")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<style>"+ drv["ClientRuleProg"].ToString() + "</style>", false);
						}
						else if (drv["ScriptEvent"].ToString() == "css_link")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<link  rel='stylesheet' type='text/css' href='"+ drv["ClientRuleProg"].ToString() + "' />", false);
						}
						else if (drv["ScriptEvent"].ToString() == "js_link")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<script type='text/javascript' src='"+ drv["ClientRuleProg"].ToString() + "'></script>", false);
						}
						else
						{
							srp = missing.Replace(srp, "''");
							string scriptEvent = drv["ScriptEvent"].ToString().TrimStart(new char[]{'_'});
							if (ee != null) {cc = ee.FindControl(drv["ColName"].ToString()) as WebControl;} else {cc = this.FindControl(drv["ColName"].ToString()) as WebControl;}
							if (cc != null && (cc.Attributes[scriptEvent] == null || cc.Attributes[scriptEvent].IndexOf(srp) < 0)) {cc.Attributes[scriptEvent] += srp;}
							if (ee != null && drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on")
							{
								cc = ee.FindControl(drv["ColName"].ToString() + "l") as WebControl;
								if (cc != null && (cc.Attributes[scriptEvent] == null || cc.Attributes[scriptEvent].IndexOf(srp) < 0)) {cc.Attributes[scriptEvent] += srp;}
							}
						}
					}
					jj = jj + 1;
				}
			}
		}

		private DataTable GetScreenCriHlp()
		{
			DataTable dtCriHlp = (DataTable)Session[KEY_dtCriHlp];
			if (dtCriHlp == null)
			{
				CheckAuthentication(false);
				try
				{
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(1,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtCriHlp] = dtCriHlp;
			}
			return dtCriHlp;
		}

		private DataTable GetScreenHlp()
		{
			DataTable dtHlp = (DataTable)Session[KEY_dtScreenHlp];
			if (dtHlp == null)
			{
				CheckAuthentication(false);
				try
				{
					dtHlp = (new AdminSystem()).GetScreenHlp(1,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtScreenHlp] = dtHlp;
			}
			return dtHlp;
		}

		private void GetGlobalFilter()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,1,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					cGlobalFilter.Text = dt.Rows[0]["FilterDesc"].ToString();
					cGlobalFilter.Visible = true;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetScreenFilter()
		{
			try
			{
				DataTable dt = (new AdminSystem()).GetScreenFilter(1,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null)
				{
					cFilterId.DataSource = dt;
					cFilterId.DataBind();
					if (cFilterId.Items.Count > 0)
					{
						if (Request.QueryString["ftr"] != null) {cFilterId.Items.FindByValue(Request.QueryString["ftr"]).Selected = true;} else {cFilterId.Items[0].Selected = true;}
						cFilterLabel.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "QFilter", "QFilter", null, null, null);
						cFilter.Visible = true;
					}
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetSystems()
		{
			Session[KEY_sysConnectionString] = LcSysConnString;
		}

		protected string ColumnWatermark(int idx)
		{
			return GetLabel().Rows[idx]["tbHint"].ToString();
		}
		protected string ColumnHeaderText(int idx)
		{
			return (GetLabel().Rows[idx]["RequiredValid"].ToString() == "Y" ? Config.MandatoryChar : string.Empty)  + GetLabel().Rows[idx]["ColumnHeader"].ToString();
		}
		protected string ColumnToolTip(int idx)
		{
			return GetLabel().Rows[idx]["ToolTip"].ToString();
		}
		protected bool GridColumnVisible(int idx)
		{
			return GetAuthCol().Rows[idx]["ColVisible"].ToString()=="Y";
		}
		protected bool GridColumnEnable(int idx)
		{
			return GetAuthCol().Rows[idx]["ColReadOnly"].ToString()=="N";
		}
		private DataTable GetLabel()
		{
			DataTable dt = (DataTable)Session[KEY_dtLabel];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetScreenLabel(1,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtLabel] = dt;
			}
			return dt;
		}

		private DataTable GetAuthCol()
		{
			DataTable dt = (DataTable)Session[KEY_dtAuthCol];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetAuthCol(1,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthCol] = dt;
			}
			return dt;
		}

		protected DataTable GetAuthRow()
		{
			DataTable dt = (DataTable)Session[KEY_dtAuthRow];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetAuthRow(1,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthRow] = dt;
			}
			return dt;
		}

		private void getReport(string eExport)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				CheckAuthentication(false);
				DataView dv = null;
				int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dv = new DataView((new AdminSystem()).GetExp(1,"GetExpAdmUsr1","Y",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(1,"GetExpAdmUsr1","N",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(1,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[3]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[4]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[5]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[6]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[10]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[16]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[17]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[22]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[23]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[24]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[25]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[26]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[27]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[27]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[28]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[29]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[29]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[30]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[30]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[31]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[31]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[32]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[32]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[33]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[33]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[34]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[34]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[35]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[35]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[36]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[36]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[37]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[37]["ColumnHeader"].ToString() + " Text" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["UsrId1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["LoginName1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UsrName1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId1"].ToString() + (char)9 + drv["CultureId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["DefCompanyId1"].ToString() + (char)9 + drv["DefCompanyId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["DefProjectId1"].ToString() + (char)9 + drv["DefProjectId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["DefSystemId1"].ToString() + (char)9 + drv["DefSystemId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UsrEmail1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UsrMobile1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UsrGroupLs1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UsrImprLink1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["IPAlert1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdNoRepeat1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdDuration1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdWarn1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["Active1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["InternalUsr1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["TechnicalUsr1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["EmailLink1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MobileLink1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["FailedAttempt1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTime(drv["LastSuccessDt1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTime(drv["LastFailedDt1"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["CompanyLs1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ProjectLs1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTimeUTC(drv["ModifiedOn1"].ToString(),base.LUser.Culture,CurrTimeZoneInfo()) + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["HintQuestionId1"].ToString() + (char)9 + drv["HintQuestionId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["HintAnswer1"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["InvestorId1"].ToString() + (char)9 + drv["InvestorId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(drv["CustomerId1"].ToString() + (char)9 + drv["CustomerId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(drv["VendorId1"].ToString() + (char)9 + drv["VendorId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["AgentId1"].ToString() + (char)9 + drv["AgentId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(drv["BrokerId1"].ToString() + (char)9 + drv["BrokerId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(drv["MemberId1"].ToString() + (char)9 + drv["MemberId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(drv["LenderId1"].ToString() + (char)9 + drv["LenderId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(drv["BorrowerId1"].ToString() + (char)9 + drv["BorrowerId1Text"].ToString() + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(drv["GuarantorId1"].ToString() + (char)9 + drv["GuarantorId1Text"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmUsr.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
				}
				else if (eExport == "RTF")
				{
					sb.Append(@"{\rtf1\ansi\ansicpg1252\uc1\deff0\stshfdbch0\stshfloch0\stshfhich0\stshfbi0\deflang1033\deflangfe1033{\fonttbl{\f0\froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f37\fswiss\fcharset0\fprq2{\*\panose 020b0604030504040204}Verdana;}
{\f182\froman\fcharset238\fprq2 Times New Roman CE;}{\f183\froman\fcharset204\fprq2 Times New Roman Cyr;}{\f185\froman\fcharset161\fprq2 Times New Roman Greek;}{\f186\froman\fcharset162\fprq2 Times New Roman Tur;}
{\f187\froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\f188\froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f189\froman\fcharset186\fprq2 Times New Roman Baltic;}{\f190\froman\fcharset163\fprq2 Times New Roman (Vietnamese);}
{\f552\fswiss\fcharset238\fprq2 Verdana CE;}{\f553\fswiss\fcharset204\fprq2 Verdana Cyr;}{\f555\fswiss\fcharset161\fprq2 Verdana Greek;}{\f556\fswiss\fcharset162\fprq2 Verdana Tur;}{\f559\fswiss\fcharset186\fprq2 Verdana Baltic;}
{\f560\fswiss\fcharset163\fprq2 Verdana (Vietnamese);}}{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;
\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;}{\stylesheet{
\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext0 Normal;}{\*\cs10 \additive \ssemihidden Default Paragraph Font;}{\*
\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tscellwidthfts0\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv 
\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs20\lang1024\langfe1024\cgrid\langnp1024\langfenp1024 \snext11 \ssemihidden Normal Table;}}{\*\latentstyles\lsdstimax156\lsdlockeddef0}{\*\rsidtbl \rsid1574081\rsid2952683
\rsid4674135\rsid4855636\rsid6620441\rsid6645578\rsid7160396\rsid7497391\rsid8133092\rsid8259185\rsid8528326\rsid8936003\rsid9058410\rsid10305085\rsid10505691\rsid13726773\rsid14047122\rsid14576392\rsid15756354\rsid16472473\rsid16525787}{\*\generator Micr
osoft Word 11.0.6359;}{\info{\title [[ScreenTitle]]}{\author }{\operator }{\creatim\yr2004\mo12\dy21\hr12\min7}{\revtim\yr2004\mo12\dy21\hr16\min16}{\version7}{\edmins5}{\nofpages1}{\nofwords6}{\nofchars38}
{\*\company robocoder corporation}{\nofcharsws43}{\vern24703}}\margl1440\margr1440 \widowctrl\ftnbj\aenddoc\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\hyphcaps0\formshade\horzdoc\dgmargin\dghspace180\dgvspace180\dghorigin1440\dgvorigin1440
\dghshow1\dgvshow1\jexpand\viewkind1\viewscale100\pgbrdrhead\pgbrdrfoot\splytwnine\ftnlytwnine\htmautsp\nolnhtadjtbl\useltbaln\alntblind\lytcalctblwd\lyttblrtgr\lnbrkrule\nobrkwrptbl\snaptogridincell\allowfieldendsel\wrppunct
\asianbrkrule\rsidroot4855636\newtblstyruls\nogrowautofit \fet0\sectd \linex0\endnhere\sectlinegrid360\sectdefaultcl\sectrsid7497391\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang 
{\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang 
{\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}\pard\plain 
\qc \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4855636 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {\b\fs32\insrsid4855636\charrsid6645578 [[TitleLabel]]}{\b\fs32\insrsid16472473\charrsid6645578 
\par }{\insrsid4855636 
\par }{\b\i\fs18\insrsid4855636\charrsid8936003 [[GlobalFilter]]
\par }\pard \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid8259185 {\insrsid7497391 
\par }\pard \qc \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4855636 {\f37\fs16\insrsid4855636\charrsid1574081 [[Grid]]
\par }\pard \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 {\insrsid4855636 
\par }}");
					sb.Replace("[[TitleLabel]]", cTitleLabel.Text);
					sb.Replace("[[GlobalFilter]]", cGlobalFilter.Text);
					sb.Replace("[[Grid]]", GetRtfGrid(dv));
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmUsr.rtf"; Session["ExportStr"] = sb;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private string GetRtfGrid(DataView dv)
		{
			StringBuilder sb = new StringBuilder("");
			try
			{
				int iColCnt = 0;
				DataTable dtAu;
				dtAu = (new AdminSystem()).GetAuthExp(1,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				if (dtAu != null)
				{
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					//Create Header
					sb.Append(@"\trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 \lang1033\langfe1033\cgrid\langnp1033\langfenp1033 ");
					sb.Append("\r\n");
					sb.Append(@"\b");
					sb.Append(@"{");
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[16]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[17]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[22]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[23]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[24]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[25]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[26]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[27]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[28]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[29]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[30]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[31]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[32]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[33]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[34]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[35]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[36]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[37]["ColumnHeader"].ToString() + @"\cell ");}
					sb.Append(@"}");
					sb.Append(@"\b0");
					sb.Append("\r\n");
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 {");
					sb.Append(@"\insrsid2981395 \trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append("\r\n");
					sb.Append(@"\row }");
					sb.Append("\r\n");
				}
				//Create Data Rows
				foreach (DataRowView drv in dv)
				{
					sb.Append(@"\trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 \lang1033\langfe1033\cgrid\langnp1033\langfenp1033 ");
					sb.Append("\r\n");
					sb.Append(@"{");
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["UsrId1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["LoginName1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["UsrName1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["DefCompanyId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["DefProjectId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["DefSystemId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["UsrEmail1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["UsrMobile1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["UsrGroupLs1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(drv["UsrImprLink1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(drv["IPAlert1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdNoRepeat1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdDuration1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PwdWarn1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["Active1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["InternalUsr1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["TechnicalUsr1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["EmailLink1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["MobileLink1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["FailedAttempt1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTime(drv["LastSuccessDt1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTime(drv["LastFailedDt1"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(drv["CompanyLs1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(drv["ProjectLs1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTimeUTC(drv["ModifiedOn1"].ToString(),base.LUser.Culture,CurrTimeZoneInfo()) + @"\cell ");}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["HintQuestionId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(drv["HintAnswer1"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["InvestorId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(drv["CustomerId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(drv["VendorId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["AgentId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(drv["BrokerId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(drv["MemberId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(drv["LenderId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(drv["BorrowerId1Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(drv["GuarantorId1Text"].ToString() + @"\cell ");}
					sb.Append(@"}");
					sb.Append("\r\n");
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 {");
					sb.Append(@"\insrsid2981395 \trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append("\r\n");
					sb.Append(@"\row }");
					sb.Append("\r\n");
				}
				sb.Append(@"\pard \par }");
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return sb.ToString();
		}

		private StringBuilder GenCellx(int CellCnt)
		{
			StringBuilder sb = new StringBuilder("");
			string strRowPre = @"\clvertalt\clbrdrt\brdrs\brdrw10 \clbrdrl\brdrs\brdrw10 \clbrdrb\brdrs\brdrw10 \clbrdrr\brdrs\brdrw10 \cltxlrtb\clftsWidth3\clwWidth4428\clshdrawnil ";
			for (int cnt=0; cnt<CellCnt; cnt++) {sb.Append(strRowPre + @"\cellx" + cnt.ToString() + " ");}
			return sb;
		}

		public void cCultureId1Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			RoboCoder.WebControls.ComboBox cc = cCultureId1; cc.SetTbVisible(); Session["CtrlAdmCtCulture"] = cc.FocusID;
		}

		public void cCultureId1Search_Script()
		{
				ImageButton ib = cCultureId1Search;
				if (ib != null)
				{
			    	TextBox pp = cUsrId1;
					RoboCoder.WebControls.ComboBox cc = cCultureId1;
					string ss = "&dsp=ComboBox"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmCtCulture.aspx?msy=3" + ss + "&typ=N" + "','" + "','',''); return false;";
				}
		}

		public void cDefSystemId1Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DropDownList cc = cDefSystemId1; Session["CtrlAdmSystems"] = cc.ClientID;
		}

		public void cDefSystemId1Search_Script()
		{
				ImageButton ib = cDefSystemId1Search;
				if (ib != null)
				{
			    	TextBox pp = cUsrId1;
					DropDownList cc = cDefSystemId1;
					string ss = "&dsp=DropDownList"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmSystems.aspx?col=SystemId&msy=3" + ss + "&typ=N" + "','" + cc.ClientID + "','',''); return false;";
				}
		}

		public void cUsrGroupLs1Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			ListBox cc = cUsrGroupLs1; Session["CtrlAdmUsrGroup"] = cc.ClientID;
		}

		public void cUsrGroupLs1Search_Script()
		{
				ImageButton ib = cUsrGroupLs1Search;
				if (ib != null)
				{
			    	TextBox pp = cUsrId1;
					ListBox cc = cUsrGroupLs1;
					string ss = "&dsp=ListBox"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmUsrGroup.aspx?msy=3" + ss + "&typ=N" + "','" + "','',''); return false;";
				}
		}

		public void cPicMed1Tgo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cPicMed1.Visible)
			{
				cPicMed1Pan.Visible = true; cPicMed1.Visible = false;
			}
			else
			{
				cPicMed1Pan.Visible = false; cPicMed1.Visible = true;
			}
		}

		public void cExpTxtButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			getReport("TXT");
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cExpRtfButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			getReport("RTF");
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cClearCriButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataView dvCri = GetScrCriteria();
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
			cCriButton_Click(sender, e);
			if (cFilterId.Items.Count > 0) { cFilterId.SelectedIndex = 0; }
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void GetCriteria(DataView dvCri)
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 1, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
				cCriPanel.Visible = true;   // Enable cCriteria.Visible to be set:
				if (dt.Rows.Count <= 0) { cCriteria.Visible = false; }
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					cCriPanel.Visible = false; cClearCriButton.Visible = false;
				}
				else
				{
					cCriPanel.Visible = (cCriteria.Visible && cCriteria.Controls.Count > 2) || cFilter.Visible;
					if ((bool)Session[KEY_bClCriVisible]) {cClearCriButton.Visible = cCriteria.Visible && cCriteria.Controls.Count > 2;} else {cClearCriButton.Visible = false;}
				}
				if (!IsPostBack)
				{
					int jj = 0; // Zero-based to be consistent with SQL reporting.
					foreach (DataRowView drv in dvCri)
					{
						if (Request.QueryString["Cri" + jj.ToString()] != null)
						{
							dt.Rows[jj+1]["LastCriteria"] = Request.QueryString["Cri" + jj.ToString()].ToString();
						}
						jj = jj + 1;
					}
				}
			    SetCriteria(dt, dvCri);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetSelectedItems(ListBox cObj, string selectedItems)
		{
			string selectedItem;
			bool finish;
			if (selectedItems == string.Empty)
			{
				try {cObj.SelectedIndex = 0;} catch {}
			}
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
			int eIndex = selectedItems.IndexOf("'",sIndex + 1);
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

		protected void cCriButton_Click(object sender, System.EventArgs e)
		{
			//WebRule: Set filter on criteria "User Name" change
                if (!string.IsNullOrEmpty(((TextBox)cCriteria.FindControl("x" + (GetScrCriteria())[1]["ColumnName"].ToString())).Text)) { cFilterId.SelectedIndex = 4; }
			// *** WebRule End *** //
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
			PopAdmUsr1List(sender, e, false, null);
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
			DataTable dtCriHlp = GetScreenCriHlp();
			int ii = 1;
			try
			{
				foreach (DataRowView drv in dvCri)
				{
					cLabel = (Label)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Label");
					if (drv["DisplayName"].ToString() == "ComboBox")
					{
						cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = null; try {val=dt.Rows[ii]["LastCriteria"].ToString();} catch {};
						base.SetCriBehavior(cComboBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cDropDownList.DataSource = dv;
						cDropDownList.DataBind();
						try { cDropDownList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; } catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "ListBox")
					{
						cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = null; try {val=dt.Rows[ii]["LastCriteria"].ToString();} catch {};
						base.SetCriBehavior(cListBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cRadioButtonList.DataSource = dv;
						cRadioButtonList.DataBind();
						try { cRadioButtonList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; } catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "Calendar")
					{
						cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cCalendar, null, cLabel, dtCriHlp.Rows[ii-1]);
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
						base.SetCriBehavior(cCheckBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						if (dt.Rows[ii]["LastCriteria"].ToString() != string.Empty) { cCheckBox.Checked = base.GetBool(dt.Rows[ii]["LastCriteria"].ToString()); }
					}
					else
					{
						cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cTextBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						cTextBox.Text = dt.Rows[ii]["LastCriteria"].ToString();
					}
					ii = ii + 1;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void ReBindCriteria(DataView dvCri)
		{
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			try
			{
				foreach (DataRowView drv in dvCri)
				{
					if (string.IsNullOrEmpty(drv["DdlFtrColumnId"].ToString())  && drv["DisplayMode"].ToString() != "AutoListBox" ) continue;
					if (drv["DisplayName"].ToString() == "ComboBox")
					{
						cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cComboBox.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cDropDownList.DataSource = dv;
						cDropDownList.DataBind();
						try { cDropDownList.Items.FindByValue(val).Selected = true; } catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "ListBox")
					{
						cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						TextBox cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Hidden");
						string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
						if (drv["DisplayMode"].ToString() == "AutoListBox" && cTextBox != null) selectedValues = cTextBox.Text ;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cRadioButtonList.DataSource = dv;
						cRadioButtonList.DataBind();
						try { cRadioButtonList.Items.FindByValue(val).Selected = true; } catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
					}
				}
			}
			catch { }
		}

		private DataView GetScrCriteria()
		{
		    DataTable dtScrCri = (DataTable)Session[KEY_dtScrCri];
		    if (dtScrCri == null)
		    {
				try
				{
					dtScrCri = (new AdminSystem()).GetScrCriteria("1", LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		        Session[KEY_dtScrCri] = dtScrCri;
		    }
		    return dtScrCri.DefaultView;
		}

		private void MakeScrGrpLab(DataRowView drv)
		{
		    Literal cLiteral = new Literal();
			string sLabelCss = drv["LabelCss"].ToString();
			if (sLabelCss != string.Empty)
			{
				if (sLabelCss.StartsWith(".")) {cLiteral.Text = "<div class=\"" + sLabelCss.Substring(1) + "\">";} else {cLiteral.Text = "<div class=\"r-labelL\" style=\"" + sLabelCss + "\">";}
			}
			else {cLiteral.Text = "<div class=\"r-labelL\">";}
		    cCriteria.Controls.Add(cLiteral);
		    Label cLabel = new Label(); cLabel.ID = "x" + drv["ColumnName"].ToString() + "Label"; cLabel.CssClass = "inp-lbl"; cCriteria.Controls.Add(cLabel);
		    cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		}

		private void MakeScrGrpInp(DataRowView drv)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
		    Literal cLiteral = new Literal();
			string sContentCss = drv["ContentCss"].ToString();
			if (sContentCss != string.Empty)
			{
				if (sContentCss.StartsWith(".")) {cLiteral.Text = "<div class=\"" + sContentCss.Substring(1) + "\">";} else {cLiteral.Text = "<div class=\"r-content\" style=\"" + sContentCss + "\">";}
			}
			else {cLiteral.Text = "<div class=\"r-content\">";}
		    cCriteria.Controls.Add(cLiteral);
		    if (drv["DisplayName"].ToString() == "ComboBox")
		    {
		        cComboBox = new RoboCoder.WebControls.ComboBox(); cComboBox.ID = "x" + drv["ColumnName"].ToString(); cComboBox.CssClass = "inp-ddl"; cComboBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cComboBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cComboBox.DataTextField = drv["DdlRefColumnName"].ToString(); cComboBox.AutoPostBack = true;
		        if (drv["DisplayMode"].ToString() == "AutoComplete")
		        {
			        WebControl wcFtr = (WebControl)cCriteria.FindControl("x" + drv["DdlFtrColumnName"].ToString());
		            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
		            context["method"] = "GetScreenIn";
			        context["addnew"] = "Y";
		            context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString();
		            context["requiredValid"] = drv["RequiredValid"].ToString();
		            context["mKey"] = drv["DdlKeyColumnName"].ToString();
		            context["mVal"] = drv["DdlRefColumnName"].ToString();
		            context["mTip"] = drv["DdlRefColumnName"].ToString();
		            context["mImg"] = drv["DdlRefColumnName"].ToString();
		            context["ssd"] = Request.QueryString["ssd"];
		            context["scr"] = "1";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            cComboBox.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
		            cComboBox.DataContext = context;
		            cComboBox.Mode = "A";
		        }
		        cCriteria.Controls.Add(cComboBox);
		    }
		    else if (drv["DisplayName"].ToString() == "DropDownList")
		    {
		        cDropDownList = new DropDownList(); cDropDownList.ID = "x" + drv["ColumnName"].ToString(); cDropDownList.CssClass = "inp-ddl"; cDropDownList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cDropDownList.DataValueField = drv["DdlKeyColumnName"].ToString(); cDropDownList.DataTextField = drv["DdlRefColumnName"].ToString(); cDropDownList.AutoPostBack = true;
		        cCriteria.Controls.Add(cDropDownList);
		    }
		    else if (drv["DisplayName"].ToString() == "ListBox")
		    {
		        cListBox = new ListBox(); cListBox.ID = "x" + drv["ColumnName"].ToString(); cListBox.SelectionMode = ListSelectionMode.Multiple; cListBox.CssClass = "inp-pic"; cListBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cListBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cListBox.DataTextField = drv["DdlRefColumnName"].ToString(); cListBox.AutoPostBack = true;
		        if (drv["RowSize"].ToString() != string.Empty) {cListBox.Rows = int.Parse(drv["RowSize"].ToString()); cListBox.Height = int.Parse(drv["RowSize"].ToString()) * 16 + 7; }
		        cCriteria.Controls.Add(cListBox);
		        if (drv["DisplayMode"].ToString() == "AutoListBox")
		        {
			        WebControl wcFtr = (WebControl)cCriteria.FindControl("x" + drv["DdlFtrColumnName"].ToString());
		            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
		            context["method"] = "GetScreenIn";
			        context["addnew"] = "Y";
		            context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString();
		            context["requiredValid"] = drv["RequiredValid"].ToString();
		            context["mKey"] = drv["DdlKeyColumnName"].ToString();
		            context["mVal"] = drv["DdlRefColumnName"].ToString();
		            context["mTip"] = drv["DdlRefColumnName"].ToString();
		            context["mImg"] = drv["DdlRefColumnName"].ToString();
		            context["ssd"] = Request.QueryString["ssd"];
		            context["scr"] = "1";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmUsr1" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmUsr1" + cListBox.ID;
		            cListBox.Attributes["ac_url"] = "AutoComplete.aspx/DdlSuggestsEx";
		            cListBox.Attributes["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            cListBox.Attributes["searchable"] = "";
		            TextBox tb = new TextBox(); tb.ID = "x" + drv["ColumnName"].ToString() + "Hidden";tb.Attributes["style"] = "display:none;";
		            cCriteria.Controls.Add(tb);
		        }
		    }
		    else if (drv["DisplayName"].ToString() == "RadioButtonList")
		    {
		        cRadioButtonList = new RadioButtonList(); cRadioButtonList.ID = "x" + drv["ColumnName"].ToString(); cRadioButtonList.CssClass = "inp-rad"; cRadioButtonList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cRadioButtonList.DataValueField = drv["DdlKeyColumnName"].ToString(); cRadioButtonList.DataTextField = drv["DdlRefColumnName"].ToString(); cRadioButtonList.AutoPostBack = true; cRadioButtonList.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
		        cRadioButtonList.Attributes["OnClick"] = "this.focus();";
		        cLiteral = new Literal(); cLiteral.Text = "<div>"; cCriteria.Controls.Add(cLiteral);
		        cCriteria.Controls.Add(cRadioButtonList);
		        cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		    }
		    else if (drv["DisplayName"].ToString() == "Calendar")
		    {
		        cCalendar = new System.Web.UI.WebControls.Calendar(); cCalendar.ID = "x" + drv["ColumnName"].ToString(); cCalendar.CssClass = "inp-txt calendar"; cCalendar.SelectionChanged += new EventHandler(cCriButton_Click); cCriteria.Controls.Add(cCalendar);
		    }
		    else if (drv["DisplayName"].ToString() == "CheckBox")
		    {
		        cCheckBox = new CheckBox(); cCheckBox.ID = "x" + drv["ColumnName"].ToString(); cCheckBox.CssClass = "inp-chk"; cCheckBox.AutoPostBack = true; cCheckBox.CheckedChanged += new EventHandler(cCriButton_Click); cCriteria.Controls.Add(cCheckBox);
		        cCheckBox.Attributes["OnClick"] = "this.focus();";
		    }
		    else
		    {
		        cTextBox = new TextBox(); cTextBox.ID = "x" + drv["ColumnName"].ToString(); cTextBox.CssClass = "inp-txt"; cTextBox.AutoPostBack = true; cTextBox.TextChanged += new EventHandler(cCriButton_Click);
		        if (drv["DisplayMode"].ToString() == "MultiLine") { cTextBox.TextMode = TextBoxMode.MultiLine; }
		        else if (drv["DisplayMode"].ToString() == "Password") { cTextBox.TextMode = TextBoxMode.Password; }
				if (drv["ColumnJustify"].ToString() == "L") { cTextBox.Style.Value = "text-align:left;"; }
				else if (drv["ColumnJustify"].ToString() == "R") { cTextBox.Style.Value = "text-align:right;"; }
				else { cTextBox.Style.Value = "text-align:center;"; }
		        cCriteria.Controls.Add(cTextBox);
		    }
		    cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		}

		private void SetCriHolder(int ii, DataView dvCri)
		{
			Literal cLiteral;
			if (dvCri.Count > 0)
			{
			    foreach (DataRowView drv in dvCri)
			    {
			        cLiteral = new Literal(); cLiteral.Text = "<div class=\"r-criteria\">"; cCriteria.Controls.Add(cLiteral);
			        MakeScrGrpLab(drv);
			        MakeScrGrpInp(drv);
			        cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
			    }
			}
		}

		private DataTable MakeColumns(DataTable dt)
		{
		    DataColumnCollection columns = dt.Columns;
		    DataView dvCri = GetScrCriteria();
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

		private DataSet UpdCriteria(bool bUpdate)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataSet ds = new DataSet();
			ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn")));
			DataRow dr = ds.Tables["DtScreenIn"].NewRow();
			DataView dvCri = GetScrCriteria();
			foreach (DataRowView drv in dvCri)
			{
			    if (drv["DisplayName"].ToString() == "ListBox")
			    {
					cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cListBox != null)
					{
						int CriCnt = (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw);
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("1", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
						string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
						bool noneSelected = string.IsNullOrEmpty(selectedValues) || selectedValues == "''";
					    dr[drv["ColumnName"].ToString()] = "(";
					    if (noneSelected && CriCnt+1 > TotalChoiceCnt) dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'-1'";
					    foreach (ListItem li in cListBox.Items)
					    {
					        if (li.Selected)
					        {
					            if (dr[drv["ColumnName"].ToString()].ToString() != "(")
					            {
					                dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ",";
					            }
					            dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'" + li.Value + "'";
					        }
					    }
					}
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cListBox.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
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
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (cComboBox != null && cComboBox.SelectedIndex >= 0 && cComboBox.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cComboBox.SelectedValue; }
			    }
			    else if (drv["DisplayName"].ToString() == "DropDownList")
			    {
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cDropDownList.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (cDropDownList != null && cDropDownList.SelectedIndex >= 0 && cDropDownList.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cDropDownList.SelectedValue; }
			    }
			    else if (drv["DisplayName"].ToString() == "RadioButtonList")
			    {
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cRadioButtonList.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
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
					if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cTextBox.Text))
					{
						if (IsPostBack) { bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");}
						cTextBox.Text = drv["DisplayMode"].ToString().Contains("Date") ? DateTime.Today.Date.ToShortDateString() : "?";
					}
					if (cTextBox != null && cTextBox.Text != string.Empty) { dr[drv["ColumnName"].ToString()] = (",DateUTC,DateTimeUTC,ShortDateTimeUTC,LongDateTimeUTC,".IndexOf("," + drv["DisplayMode"].ToString() + ",") >= 0) ? SetDateTimeUTC(cTextBox.Text,!bUpdate) : (drv["DisplayName"].ToString().Contains("Date") ? (DateTime.Parse(cTextBox.Text,System.Threading.Thread.CurrentThread.CurrentCulture).ToString()): cTextBox.Text); }
			    }
			}
			ds.Tables["DtScreenIn"].Rows.Add(dr);
			if (bUpdate) 
			{
				try
				{
					(new AdminSystem()).UpdScrCriteria("1", "AdmUsr1", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			Session[KEY_dsScrCriVal] = ds;
			return ds;
		}

		private DataTable GetScreenTab()
		{
			CheckAuthentication(false);
			DataTable dtScreenTab = null;
			try
			{
				dtScreenTab = (new AdminSystem()).GetScreenTab(1,base.LUser.CultureId,LcSysConnString,LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return dtScreenTab;
		}

		protected void cbCultureId1(object sender, System.EventArgs e)
		{
			SetCultureId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetCultureId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlCultureId3S1746";
			context["addnew"] = "Y";
			context["mKey"] = "CultureId1";
			context["mVal"] = "CultureId1Text";
			context["mTip"] = "CultureId1Text";
			context["mImg"] = "CultureId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlCultureId3S1746",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtCultureId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void SetDefCompanyId1(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtDefCompanyId1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefCompanyId3S1304",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "DefCompanyId1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefCompanyId3S1304",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefCompanyId3S1304",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtDefCompanyId1] = dv.Table;
				}
			}
		}

		private void SetDefProjectId1(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtDefProjectId1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefProjectId3S1481",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "DefProjectId1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefProjectId3S1481",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefProjectId3S1481",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtDefProjectId1] = dv.Table;
				}
			}
		}

		private void SetDefSystemId1(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtDefSystemId1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefSystemId3S497",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "DefSystemId1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefSystemId3S497",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlDefSystemId3S497",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtDefSystemId1] = dv.Table;
				}
			}
		}

		private void SetUsrGroupLs1(ListBox ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtUsrGroupLs1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlUsrGroupLs3S1741",false,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "UsrGroupLs1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					string key = keyId;
					if (key.StartsWith("(")) { key = key.Substring(1, key.Length - 2); }
					if (key.IndexOf("'") >= 0) { key = key.Replace("''", char.ToString((char)191)).Replace("'", string.Empty).Replace(char.ToString((char)191), "''"); }
					string[] arr = key.Split(',');
					foreach (string sel in arr)
					{
						li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true; bFirst = true;}
					}
					if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlUsrGroupLs3S1741",false,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						foreach (string sel in arr)
						{
							li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true;}
						}
					}
					if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlUsrGroupLs3S1741",false,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (ddl.SelectedIndex < 0 && dv.Count > 0) { ddl.Items[0].Selected = true; }
					Session[KEY_dtUsrGroupLs1] = dv.Table;
				}
			}
		}

		private void SetUsrImprLink1(DataGrid ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtUsrImprLink1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlUsrImprLink3S1749",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "UsrImprLink1 is not NULL"; }
					ViewState["UsrImprLink_RowFilter"] = ss;
					dv.RowFilter = ss;
					ddl.CurrentPageIndex = 0;ddl.AllowPaging = dv.Count > ddl.PageSize;
					ddl.DataSource = dv; ddl.DataBind();
					Session[KEY_dtUsrImprLink1] = dv.Table;
				}
			}
		}

		private void SetCompanyLs1(ListBox ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtCompanyLs1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlCompanyLs3S1742",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "CompanyLs1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					string key = keyId;
					if (key.StartsWith("(")) { key = key.Substring(1, key.Length - 2); }
					if (key.IndexOf("'") >= 0) { key = key.Replace("''", char.ToString((char)191)).Replace("'", string.Empty).Replace(char.ToString((char)191), "''"); }
					string[] arr = key.Split(',');
					foreach (string sel in arr)
					{
						li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true; bFirst = true;}
					}
					if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlCompanyLs3S1742",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						foreach (string sel in arr)
						{
							li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true;}
						}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlCompanyLs3S1742",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (ddl.SelectedIndex < 0 && dv.Count > 0) { ddl.Items[0].Selected = true; }
					Session[KEY_dtCompanyLs1] = dv.Table;
				}
			}
		}

		private void SetProjectLs1(ListBox ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtProjectLs1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlProjectLs3S1743",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ProjectLs1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					string key = keyId;
					if (key.StartsWith("(")) { key = key.Substring(1, key.Length - 2); }
					if (key.IndexOf("'") >= 0) { key = key.Replace("''", char.ToString((char)191)).Replace("'", string.Empty).Replace(char.ToString((char)191), "''"); }
					string[] arr = key.Split(',');
					foreach (string sel in arr)
					{
						li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true; bFirst = true;}
					}
					if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlProjectLs3S1743",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						foreach (string sel in arr)
						{
							li = ddl.Items.FindByValue(sel); if (li != null) {li.Selected = true;}
						}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlProjectLs3S1743",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (ddl.SelectedIndex < 0 && dv.Count > 0) { ddl.Items[0].Selected = true; }
					Session[KEY_dtProjectLs1] = dv.Table;
				}
			}
		}

		private void SetHintQuestionId1(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtHintQuestionId1];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlHintQuestionId3S1119",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "HintQuestionId1 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlHintQuestionId3S1119",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlHintQuestionId3S1119",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtHintQuestionId1] = dv.Table;
				}
			}
		}

		protected void cbInvestorId1(object sender, System.EventArgs e)
		{
			SetInvestorId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetInvestorId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlInvestorId3S316";
			context["addnew"] = "Y";
			context["mKey"] = "InvestorId1";
			context["mVal"] = "InvestorId1Text";
			context["mTip"] = "InvestorId1Text";
			context["mImg"] = "InvestorId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlInvestorId3S316",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtInvestorId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbCustomerId1(object sender, System.EventArgs e)
		{
			SetCustomerId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetCustomerId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlCustomerId3S6";
			context["addnew"] = "Y";
			context["mKey"] = "CustomerId1";
			context["mVal"] = "CustomerId1Text";
			context["mTip"] = "CustomerId1Text";
			context["mImg"] = "CustomerId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlCustomerId3S6",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtCustomerId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbVendorId1(object sender, System.EventArgs e)
		{
			SetVendorId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetVendorId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlVendorId3S7";
			context["addnew"] = "Y";
			context["mKey"] = "VendorId1";
			context["mVal"] = "VendorId1Text";
			context["mTip"] = "VendorId1Text";
			context["mImg"] = "VendorId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlVendorId3S7",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtVendorId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbAgentId1(object sender, System.EventArgs e)
		{
			SetAgentId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetAgentId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlAgentId3S934";
			context["addnew"] = "Y";
			context["mKey"] = "AgentId1";
			context["mVal"] = "AgentId1Text";
			context["mTip"] = "AgentId1Text";
			context["mImg"] = "AgentId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlAgentId3S934",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtAgentId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbBrokerId1(object sender, System.EventArgs e)
		{
			SetBrokerId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetBrokerId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlBrokerId3S935";
			context["addnew"] = "Y";
			context["mKey"] = "BrokerId1";
			context["mVal"] = "BrokerId1Text";
			context["mTip"] = "BrokerId1Text";
			context["mImg"] = "BrokerId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlBrokerId3S935",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtBrokerId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbMemberId1(object sender, System.EventArgs e)
		{
			SetMemberId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetMemberId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlMemberId3S8";
			context["addnew"] = "Y";
			context["mKey"] = "MemberId1";
			context["mVal"] = "MemberId1Text";
			context["mTip"] = "MemberId1Text";
			context["mImg"] = "MemberId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlMemberId3S8",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtMemberId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbLenderId1(object sender, System.EventArgs e)
		{
			SetLenderId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetLenderId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlLenderId3S4185";
			context["addnew"] = "Y";
			context["mKey"] = "LenderId1";
			context["mVal"] = "LenderId1Text";
			context["mTip"] = "LenderId1Text";
			context["mImg"] = "LenderId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlLenderId3S4185",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtLenderId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbBorrowerId1(object sender, System.EventArgs e)
		{
			SetBorrowerId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetBorrowerId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlBorrowerId3S4186";
			context["addnew"] = "Y";
			context["mKey"] = "BorrowerId1";
			context["mVal"] = "BorrowerId1Text";
			context["mTip"] = "BorrowerId1Text";
			context["mImg"] = "BorrowerId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlBorrowerId3S4186",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtBorrowerId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbGuarantorId1(object sender, System.EventArgs e)
		{
			SetGuarantorId1((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetGuarantorId1(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlGuarantorId3S4187";
			context["addnew"] = "Y";
			context["mKey"] = "GuarantorId1";
			context["mVal"] = "GuarantorId1Text";
			context["mTip"] = "GuarantorId1Text";
			context["mImg"] = "GuarantorId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = string.Empty;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(1,"GetDdlGuarantorId3S4187",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("UsrId"))
					{
						context["pMKeyColID"] = cAdmUsr1List.ClientID;
						context["pMKeyCol"] = "UsrId";
						string ss = "(UsrId is null";
						if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR UsrId = " + cAdmUsr1List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtGuarantorId1] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private DataView GetAdmUsr1List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmUsr1List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(1,"GetLisAdmUsr1",true,"Y",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(1,"GetLisAdmUsr1",true,"N",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmUsr1List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmUsr1List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmUsr1";
			context["mKey"] = "UsrId1";
			context["mVal"] = "UsrId1Text";
			context["mTip"] = "UsrId1Text";
			context["mImg"] = "UsrId1Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "Y";
			context["conn"] = string.Empty;
			cAdmUsr1List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmUsr1List.DataContext = context;
			if (dv.Table == null) return;
			cAdmUsr1List.DataSource = dv;
			cAdmUsr1List.Visible = true;
			if (cAdmUsr1List.Items.Count <= 0) {cAdmUsr1List.Visible = false; cAdmUsr1List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmUsr1List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmUsr1List.Items[0].Selected = true; cAdmUsr1List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmUsr1List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmUsr1List_SelectedIndexChanged(sender, e);
					}
				}
			}
		}

		private void SetColumnAuthority()
		{
			DataTable dtAuth = GetAuthCol();
			DataTable dtLabel = GetLabel();
			cTabFolder.CssClass = "TabFolder rmvSpace";
			foreach (DataRow dr in dtAuth.Rows) {if (dr["MasterTable"].ToString() == "Y" && dr["ColVisible"].ToString() == "Y") {cTabFolder.CssClass = "TabFolder"; break;}}
			if (dtAuth != null && dtLabel != null)
			{
				base.SetFoldBehavior(cUsrId1, dtAuth.Rows[0], cUsrId1P1, cUsrId1Label, cUsrId1P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cLoginName1, dtAuth.Rows[1], cLoginName1P1, cLoginName1Label, cLoginName1P2, null, dtLabel.Rows[1], cRFVLoginName1, null, null);
				base.SetFoldBehavior(cUsrName1, dtAuth.Rows[2], cUsrName1P1, cUsrName1Label, cUsrName1P2, null, dtLabel.Rows[2], cRFVUsrName1, null, null);
				base.SetFoldBehavior(cCultureId1, dtAuth.Rows[3], cCultureId1P1, cCultureId1Label, cCultureId1P2, null, dtLabel.Rows[3], cRFVCultureId1, null, null);
				SetCultureId1(cCultureId1,string.Empty);
				base.SetFoldBehavior(cDefCompanyId1, dtAuth.Rows[4], cDefCompanyId1P1, cDefCompanyId1Label, cDefCompanyId1P2, null, dtLabel.Rows[4], null, null, null);
				base.SetFoldBehavior(cDefProjectId1, dtAuth.Rows[5], cDefProjectId1P1, cDefProjectId1Label, cDefProjectId1P2, null, dtLabel.Rows[5], null, null, null);
				base.SetFoldBehavior(cDefSystemId1, dtAuth.Rows[6], cDefSystemId1P1, cDefSystemId1Label, cDefSystemId1P2, null, dtLabel.Rows[6], cRFVDefSystemId1, null, null);
				base.SetFoldBehavior(cUsrEmail1, dtAuth.Rows[7], cUsrEmail1P1, cUsrEmail1Label, cUsrEmail1P2, null, dtLabel.Rows[7], null, null, null);
				base.SetFoldBehavior(cUsrMobile1, dtAuth.Rows[8], cUsrMobile1P1, cUsrMobile1Label, cUsrMobile1P2, null, dtLabel.Rows[8], null, null, null);
				base.SetFoldBehavior(cUsrGroupLs1, dtAuth.Rows[9], cUsrGroupLs1P1, cUsrGroupLs1Label, cUsrGroupLs1P2, null, dtLabel.Rows[9], null, null, null);
				base.SetFoldBehavior(cUsrImprLink1, dtAuth.Rows[10], cUsrImprLink1P1, cUsrImprLink1Label, cUsrImprLink1P2, null, dtLabel.Rows[10], null, null, null);
				base.SetFoldBehavior(cPicMed1, dtAuth.Rows[11], cPicMed1P1, cPicMed1Label, cPicMed1P2, cPicMed1Tgo, cPicMed1Pan, dtLabel.Rows[11], null, null, null);
				base.SetFoldBehavior(cIPAlert1, dtAuth.Rows[12], cIPAlert1P1, cIPAlert1Label, cIPAlert1P2, null, dtLabel.Rows[12], null, null, null);
				base.SetFoldBehavior(cPwdNoRepeat1, dtAuth.Rows[13], cPwdNoRepeat1P1, cPwdNoRepeat1Label, cPwdNoRepeat1P2, null, dtLabel.Rows[13], null, cREVPwdNoRepeat1, null);
				base.SetFoldBehavior(cPwdDuration1, dtAuth.Rows[14], cPwdDuration1P1, cPwdDuration1Label, cPwdDuration1P2, null, dtLabel.Rows[14], null, cREVPwdDuration1, null);
				base.SetFoldBehavior(cPwdWarn1, dtAuth.Rows[15], cPwdWarn1P1, cPwdWarn1Label, cPwdWarn1P2, null, dtLabel.Rows[15], null, cREVPwdWarn1, null);
				base.SetFoldBehavior(cActive1, dtAuth.Rows[16], cActive1P1, cActive1Label, cActive1P2, null, dtLabel.Rows[16], null, null, null);
				base.SetFoldBehavior(cInternalUsr1, dtAuth.Rows[17], cInternalUsr1P1, cInternalUsr1Label, cInternalUsr1P2, null, dtLabel.Rows[17], null, null, null);
				base.SetFoldBehavior(cTechnicalUsr1, dtAuth.Rows[18], cTechnicalUsr1P1, cTechnicalUsr1Label, cTechnicalUsr1P2, null, dtLabel.Rows[18], null, null, null);
				base.SetFoldBehavior(cEmailLink1, dtAuth.Rows[19], cEmailLink1P1, cEmailLink1Label, cEmailLink1P2, null, dtLabel.Rows[19], null, null, null);
				base.SetFoldBehavior(cMobileLink1, dtAuth.Rows[20], cMobileLink1P1, cMobileLink1Label, cMobileLink1P2, null, dtLabel.Rows[20], null, null, null);
				base.SetFoldBehavior(cFailedAttempt1, dtAuth.Rows[21], cFailedAttempt1P1, cFailedAttempt1Label, cFailedAttempt1P2, null, dtLabel.Rows[21], null, null, null);
				base.SetFoldBehavior(cLastSuccessDt1, dtAuth.Rows[22], cLastSuccessDt1P1, cLastSuccessDt1Label, cLastSuccessDt1P2, null, dtLabel.Rows[22], null, null, null);
				base.SetFoldBehavior(cLastFailedDt1, dtAuth.Rows[23], cLastFailedDt1P1, cLastFailedDt1Label, cLastFailedDt1P2, null, dtLabel.Rows[23], null, null, null);
				base.SetFoldBehavior(cCompanyLs1, dtAuth.Rows[24], cCompanyLs1P1, cCompanyLs1Label, cCompanyLs1P2, null, dtLabel.Rows[24], null, null, null);
				base.SetFoldBehavior(cProjectLs1, dtAuth.Rows[25], cProjectLs1P1, cProjectLs1Label, cProjectLs1P2, null, dtLabel.Rows[25], null, null, null);
				base.SetFoldBehavior(cModifiedOn1, dtAuth.Rows[26], cModifiedOn1P1, cModifiedOn1Label, cModifiedOn1P2, null, dtLabel.Rows[26], null, null, null);
				base.SetFoldBehavior(cHintQuestionId1, dtAuth.Rows[27], cHintQuestionId1P1, cHintQuestionId1Label, cHintQuestionId1P2, null, dtLabel.Rows[27], null, null, null);
				base.SetFoldBehavior(cHintAnswer1, dtAuth.Rows[28], cHintAnswer1P1, cHintAnswer1Label, cHintAnswer1P2, null, dtLabel.Rows[28], null, null, null);
				base.SetFoldBehavior(cInvestorId1, dtAuth.Rows[29], cInvestorId1P1, cInvestorId1Label, cInvestorId1P2, null, dtLabel.Rows[29], null, null, null);
				SetInvestorId1(cInvestorId1,string.Empty);
				base.SetFoldBehavior(cCustomerId1, dtAuth.Rows[30], cCustomerId1P1, cCustomerId1Label, cCustomerId1P2, null, dtLabel.Rows[30], null, null, null);
				SetCustomerId1(cCustomerId1,string.Empty);
				base.SetFoldBehavior(cVendorId1, dtAuth.Rows[31], cVendorId1P1, cVendorId1Label, cVendorId1P2, null, dtLabel.Rows[31], null, null, null);
				SetVendorId1(cVendorId1,string.Empty);
				base.SetFoldBehavior(cAgentId1, dtAuth.Rows[32], cAgentId1P1, cAgentId1Label, cAgentId1P2, null, dtLabel.Rows[32], null, null, null);
				SetAgentId1(cAgentId1,string.Empty);
				base.SetFoldBehavior(cBrokerId1, dtAuth.Rows[33], cBrokerId1P1, cBrokerId1Label, cBrokerId1P2, null, dtLabel.Rows[33], null, null, null);
				SetBrokerId1(cBrokerId1,string.Empty);
				base.SetFoldBehavior(cMemberId1, dtAuth.Rows[34], cMemberId1P1, cMemberId1Label, cMemberId1P2, null, dtLabel.Rows[34], null, null, null);
				SetMemberId1(cMemberId1,string.Empty);
				base.SetFoldBehavior(cLenderId1, dtAuth.Rows[35], cLenderId1P1, cLenderId1Label, cLenderId1P2, null, dtLabel.Rows[35], null, null, null);
				SetLenderId1(cLenderId1,string.Empty);
				base.SetFoldBehavior(cBorrowerId1, dtAuth.Rows[36], cBorrowerId1P1, cBorrowerId1Label, cBorrowerId1P2, null, dtLabel.Rows[36], null, null, null);
				SetBorrowerId1(cBorrowerId1,string.Empty);
				base.SetFoldBehavior(cGuarantorId1, dtAuth.Rows[37], cGuarantorId1P1, cGuarantorId1Label, cGuarantorId1P2, null, dtLabel.Rows[37], null, null, null);
				SetGuarantorId1(cGuarantorId1,string.Empty);
				base.SetFoldBehavior(cUsageStat, dtAuth.Rows[38], null, null, null, dtLabel.Rows[38], null, null, null);
			}
			if ((cLoginName1.Attributes["OnChange"] == null || cLoginName1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLoginName1.Visible && !cLoginName1.ReadOnly) {cLoginName1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cUsrName1.Attributes["OnChange"] == null || cUsrName1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrName1.Visible && !cUsrName1.ReadOnly) {cUsrName1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCultureId1.Attributes["OnChange"] == null || cCultureId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCultureId1.Visible && cCultureId1.Enabled) {cCultureId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if (cCultureId1Search.Attributes["OnClick"] == null || cCultureId1Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cCultureId1Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cDefCompanyId1.Attributes["OnChange"] == null || cDefCompanyId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDefCompanyId1.Visible && cDefCompanyId1.Enabled) {cDefCompanyId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDefProjectId1.Attributes["OnChange"] == null || cDefProjectId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDefProjectId1.Visible && cDefProjectId1.Enabled) {cDefProjectId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDefSystemId1.Attributes["OnChange"] == null || cDefSystemId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDefSystemId1.Visible && cDefSystemId1.Enabled) {cDefSystemId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if (cDefSystemId1Search.Attributes["OnClick"] == null || cDefSystemId1Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cDefSystemId1Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cUsrEmail1.Attributes["OnChange"] == null || cUsrEmail1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrEmail1.Visible && !cUsrEmail1.ReadOnly) {cUsrEmail1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cUsrMobile1.Attributes["OnChange"] == null || cUsrMobile1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrMobile1.Visible && !cUsrMobile1.ReadOnly) {cUsrMobile1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cUsrGroupLs1.Attributes["OnChange"] == null || cUsrGroupLs1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrGroupLs1.Visible && cUsrGroupLs1.Enabled) {cUsrGroupLs1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if (cUsrGroupLs1Search.Attributes["OnClick"] == null || cUsrGroupLs1Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cUsrGroupLs1Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cUsrImprLink1.Attributes["OnChange"] == null || cUsrImprLink1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrImprLink1.Visible && cUsrImprLink1.Enabled) {cUsrImprLink1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPicMed1.Attributes["OnChange"] == null || cPicMed1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPicMed1.Visible && cPicMed1.Enabled) {cPicMed1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cIPAlert1.Attributes["OnClick"] == null || cIPAlert1.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cIPAlert1.Visible && cIPAlert1.Enabled) {cIPAlert1.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cPwdNoRepeat1.Attributes["OnChange"] == null || cPwdNoRepeat1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPwdNoRepeat1.Visible && !cPwdNoRepeat1.ReadOnly) {cPwdNoRepeat1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPwdDuration1.Attributes["OnChange"] == null || cPwdDuration1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPwdDuration1.Visible && !cPwdDuration1.ReadOnly) {cPwdDuration1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPwdWarn1.Attributes["OnChange"] == null || cPwdWarn1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPwdWarn1.Visible && !cPwdWarn1.ReadOnly) {cPwdWarn1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cActive1.Attributes["OnClick"] == null || cActive1.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cActive1.Visible && cActive1.Enabled) {cActive1.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cInternalUsr1.Attributes["OnClick"] == null || cInternalUsr1.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cInternalUsr1.Visible && cInternalUsr1.Enabled) {cInternalUsr1.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cTechnicalUsr1.Attributes["OnClick"] == null || cTechnicalUsr1.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cTechnicalUsr1.Visible && cTechnicalUsr1.Enabled) {cTechnicalUsr1.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cEmailLink1.Attributes["OnChange"] == null || cEmailLink1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cEmailLink1.Visible && cEmailLink1.Enabled) {cEmailLink1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cMobileLink1.Attributes["OnChange"] == null || cMobileLink1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMobileLink1.Visible && cMobileLink1.Enabled) {cMobileLink1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cFailedAttempt1.Attributes["OnChange"] == null || cFailedAttempt1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cFailedAttempt1.Visible && !cFailedAttempt1.ReadOnly) {cFailedAttempt1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLastSuccessDt1.Attributes["OnChange"] == null || cLastSuccessDt1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLastSuccessDt1.Visible && !cLastSuccessDt1.ReadOnly) {cLastSuccessDt1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLastFailedDt1.Attributes["OnChange"] == null || cLastFailedDt1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLastFailedDt1.Visible && !cLastFailedDt1.ReadOnly) {cLastFailedDt1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCompanyLs1.Attributes["OnChange"] == null || cCompanyLs1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCompanyLs1.Visible && cCompanyLs1.Enabled) {cCompanyLs1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cProjectLs1.Attributes["OnChange"] == null || cProjectLs1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cProjectLs1.Visible && cProjectLs1.Enabled) {cProjectLs1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cModifiedOn1.Attributes["OnChange"] == null || cModifiedOn1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cModifiedOn1.Visible && !cModifiedOn1.ReadOnly) {cModifiedOn1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cHintQuestionId1.Attributes["OnChange"] == null || cHintQuestionId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cHintQuestionId1.Visible && cHintQuestionId1.Enabled) {cHintQuestionId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cHintAnswer1.Attributes["OnChange"] == null || cHintAnswer1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cHintAnswer1.Visible && !cHintAnswer1.ReadOnly) {cHintAnswer1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cInvestorId1.Attributes["OnChange"] == null || cInvestorId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cInvestorId1.Visible && cInvestorId1.Enabled) {cInvestorId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCustomerId1.Attributes["OnChange"] == null || cCustomerId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCustomerId1.Visible && cCustomerId1.Enabled) {cCustomerId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cVendorId1.Attributes["OnChange"] == null || cVendorId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cVendorId1.Visible && cVendorId1.Enabled) {cVendorId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cAgentId1.Attributes["OnChange"] == null || cAgentId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cAgentId1.Visible && cAgentId1.Enabled) {cAgentId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cBrokerId1.Attributes["OnChange"] == null || cBrokerId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cBrokerId1.Visible && cBrokerId1.Enabled) {cBrokerId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cMemberId1.Attributes["OnChange"] == null || cMemberId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMemberId1.Visible && cMemberId1.Enabled) {cMemberId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLenderId1.Attributes["OnChange"] == null || cLenderId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLenderId1.Visible && cLenderId1.Enabled) {cLenderId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cBorrowerId1.Attributes["OnChange"] == null || cBorrowerId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cBorrowerId1.Visible && cBorrowerId1.Enabled) {cBorrowerId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGuarantorId1.Attributes["OnChange"] == null || cGuarantorId1.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGuarantorId1.Visible && cGuarantorId1.Enabled) {cGuarantorId1.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
		}

		private DataTable SetFunctionality(DataTable dt)
		{
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if (dr["AllowDel"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					if ((bool)Session[KEY_bDeleteVisible]) {cDeleteButton.Visible = false; Session[KEY_bDeleteVisible] = false;}
				}
				else if ((bool)Session[KEY_bDeleteVisible]) {cDeleteButton.Visible = true; cDeleteButton.Enabled = true;}
				if ((dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cUndoAllButton.Visible = false; cSaveButton.Visible = false; cSaveCloseButton.Visible = false;
				}
				else
				{
					if ((bool)Session[KEY_bUndoAllVisible]) {cUndoAllButton.Visible = true; cUndoAllButton.Enabled = true;}
					if ((bool)Session[KEY_bUpdateVisible]) {cSaveButton.Visible = true; cSaveButton.Enabled = true;}
				}
				if (dr["AllowAdd"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cNewButton.Visible = false; cNewSaveButton.Visible = false; cCopyButton.Visible = false; cCopySaveButton.Visible = false;
					if (dt != null && dt.Rows.Count > 0) {dt.Rows[0].Delete();}
				}
				else
				{
					if ((bool)Session[KEY_bNewVisible]) {cNewButton.Visible = true; cNewButton.Enabled = true;} else {cNewButton.Visible = false;}
					if ((bool)Session[KEY_bNewSaveVisible]) {cNewSaveButton.Visible = true; cNewSaveButton.Enabled = true;} else {cNewSaveButton.Visible = false;}
					if ((bool)Session[KEY_bCopyVisible]) {cCopyButton.Visible = true; cCopyButton.Enabled = true;} else {cCopyButton.Visible = false;}
					if ((bool)Session[KEY_bCopySaveVisible]) {cCopySaveButton.Visible = true; cCopySaveButton.Enabled = true;} else {cCopySaveButton.Visible = false;}
				}
			}
			return dt;
		}

		protected void cFilterId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
				cNewButton_Click(sender, new EventArgs());
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
				DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
				Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
				Session.Remove(KEY_dtCultureId1);
				Session.Remove(KEY_dtDefCompanyId1);
				Session.Remove(KEY_dtDefProjectId1);
				Session.Remove(KEY_dtDefSystemId1);
				Session.Remove(KEY_dtUsrGroupLs1);
				Session.Remove(KEY_dtUsrImprLink1);
				Session.Remove(KEY_dtCompanyLs1);
				Session.Remove(KEY_dtProjectLs1);
				Session.Remove(KEY_dtHintQuestionId1);
				Session.Remove(KEY_dtInvestorId1);
				Session.Remove(KEY_dtCustomerId1);
				Session.Remove(KEY_dtVendorId1);
				Session.Remove(KEY_dtAgentId1);
				Session.Remove(KEY_dtBrokerId1);
				Session.Remove(KEY_dtMemberId1);
				Session.Remove(KEY_dtLenderId1);
				Session.Remove(KEY_dtBorrowerId1);
				Session.Remove(KEY_dtGuarantorId1);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
				cCultureId1Search_Script();
				cDefSystemId1Search_Script();
				cUsrGroupLs1Search_Script();
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cLoginName1.Text = string.Empty;}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {cUsrName1.Text = string.Empty;}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {SetCultureId1(cCultureId1,LUser.CultureId.ToString());}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {SetDefCompanyId1(cDefCompanyId1,string.Empty);}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {SetDefProjectId1(cDefProjectId1,string.Empty);}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {SetDefSystemId1(cDefSystemId1,string.Empty);}
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cUsrEmail1.Text = string.Empty;}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cUsrMobile1.Text = "+";}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {SetUsrGroupLs1(cUsrGroupLs1,string.Empty);}
			if (dt.Rows[10]["ColVisible"].ToString() == "Y" && dt.Rows[10]["ColReadOnly"].ToString() != "Y") {SetUsrImprLink1(cUsrImprLink1,string.Empty);}
			if (dt.Rows[11]["ColVisible"].ToString() == "Y" && dt.Rows[11]["ColReadOnly"].ToString() != "Y") {cPicMed1.ImageUrl = "~/images/DefaultImg.png"; }
			if (dt.Rows[12]["ColVisible"].ToString() == "Y" && dt.Rows[12]["ColReadOnly"].ToString() != "Y") {cIPAlert1.Checked = base.GetBool("N");}
			if (dt.Rows[13]["ColVisible"].ToString() == "Y" && dt.Rows[13]["ColReadOnly"].ToString() != "Y") {cPwdNoRepeat1.Text = "1";}
			if (dt.Rows[14]["ColVisible"].ToString() == "Y" && dt.Rows[14]["ColReadOnly"].ToString() != "Y") {cPwdDuration1.Text = "180";}
			if (dt.Rows[15]["ColVisible"].ToString() == "Y" && dt.Rows[15]["ColReadOnly"].ToString() != "Y") {cPwdWarn1.Text = "0";}
			if (dt.Rows[16]["ColVisible"].ToString() == "Y" && dt.Rows[16]["ColReadOnly"].ToString() != "Y") {cActive1.Checked = base.GetBool("Y");}
			if (dt.Rows[17]["ColVisible"].ToString() == "Y" && dt.Rows[17]["ColReadOnly"].ToString() != "Y") {cInternalUsr1.Checked = base.GetBool("N");}
			if (dt.Rows[18]["ColVisible"].ToString() == "Y" && dt.Rows[18]["ColReadOnly"].ToString() != "Y") {cTechnicalUsr1.Checked = base.GetBool("N");}
			if (dt.Rows[19]["ColVisible"].ToString() == "Y" && dt.Rows[19]["ColReadOnly"].ToString() != "Y") {cEmailLink1.Text = string.Empty;}
			if (dt.Rows[20]["ColVisible"].ToString() == "Y" && dt.Rows[20]["ColReadOnly"].ToString() != "Y") {cMobileLink1.Text = string.Empty;}
			if (dt.Rows[21]["ColVisible"].ToString() == "Y" && dt.Rows[21]["ColReadOnly"].ToString() != "Y") {cFailedAttempt1.Text = string.Empty;}
			if (dt.Rows[22]["ColVisible"].ToString() == "Y" && dt.Rows[22]["ColReadOnly"].ToString() != "Y") {cLastSuccessDt1.Text = string.Empty;}
			if (dt.Rows[23]["ColVisible"].ToString() == "Y" && dt.Rows[23]["ColReadOnly"].ToString() != "Y") {cLastFailedDt1.Text = string.Empty;}
			if (dt.Rows[24]["ColVisible"].ToString() == "Y" && dt.Rows[24]["ColReadOnly"].ToString() != "Y") {SetCompanyLs1(cCompanyLs1,string.Empty);}
			if (dt.Rows[25]["ColVisible"].ToString() == "Y" && dt.Rows[25]["ColReadOnly"].ToString() != "Y") {SetProjectLs1(cProjectLs1,string.Empty);}
			if (dt.Rows[26]["ColVisible"].ToString() == "Y" && dt.Rows[26]["ColReadOnly"].ToString() != "Y") {cModifiedOn1.Text = string.Empty;}
			if (dt.Rows[27]["ColVisible"].ToString() == "Y" && dt.Rows[27]["ColReadOnly"].ToString() != "Y") {SetHintQuestionId1(cHintQuestionId1,string.Empty);}
			if (dt.Rows[28]["ColVisible"].ToString() == "Y" && dt.Rows[28]["ColReadOnly"].ToString() != "Y") {cHintAnswer1.Text = string.Empty;}
			if (dt.Rows[29]["ColVisible"].ToString() == "Y" && dt.Rows[29]["ColReadOnly"].ToString() != "Y") {cInvestorId1.ClearSearch();}
			if (dt.Rows[30]["ColVisible"].ToString() == "Y" && dt.Rows[30]["ColReadOnly"].ToString() != "Y") {cCustomerId1.ClearSearch();}
			if (dt.Rows[31]["ColVisible"].ToString() == "Y" && dt.Rows[31]["ColReadOnly"].ToString() != "Y") {cVendorId1.ClearSearch();}
			if (dt.Rows[32]["ColVisible"].ToString() == "Y" && dt.Rows[32]["ColReadOnly"].ToString() != "Y") {cAgentId1.ClearSearch();}
			if (dt.Rows[33]["ColVisible"].ToString() == "Y" && dt.Rows[33]["ColReadOnly"].ToString() != "Y") {cBrokerId1.ClearSearch();}
			if (dt.Rows[34]["ColVisible"].ToString() == "Y" && dt.Rows[34]["ColReadOnly"].ToString() != "Y") {cMemberId1.ClearSearch();}
			if (dt.Rows[35]["ColVisible"].ToString() == "Y" && dt.Rows[35]["ColReadOnly"].ToString() != "Y") {cLenderId1.ClearSearch();}
			if (dt.Rows[36]["ColVisible"].ToString() == "Y" && dt.Rows[36]["ColReadOnly"].ToString() != "Y") {cBorrowerId1.ClearSearch();}
			if (dt.Rows[37]["ColVisible"].ToString() == "Y" && dt.Rows[37]["ColReadOnly"].ToString() != "Y") {cGuarantorId1.ClearSearch();}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cUsrId1.Text = string.Empty;
			cLoginName1.Text = string.Empty;
			cUsrName1.Text = string.Empty;
			SetCultureId1(cCultureId1,LUser.CultureId.ToString());
			SetDefCompanyId1(cDefCompanyId1,string.Empty);
			SetDefProjectId1(cDefProjectId1,string.Empty);
			SetDefSystemId1(cDefSystemId1,string.Empty);
			cUsrEmail1.Text = string.Empty;
			cUsrMobile1.Text = "+";
			SetUsrGroupLs1(cUsrGroupLs1,string.Empty);
			SetUsrImprLink1(cUsrImprLink1,string.Empty);
			cPicMed1.ImageUrl = "~/images/DefaultImg.png";
			cIPAlert1.Checked = base.GetBool("N");
			cPwdNoRepeat1.Text = "1";
			cPwdDuration1.Text = "180";
			cPwdWarn1.Text = "0";
			cActive1.Checked = base.GetBool("Y");
			cInternalUsr1.Checked = base.GetBool("N");
			cTechnicalUsr1.Checked = base.GetBool("N");
			cEmailLink1.Text = string.Empty;
			cMobileLink1.Text = string.Empty;
			cFailedAttempt1.Text = string.Empty;
			cLastSuccessDt1.Text = string.Empty;
			cLastFailedDt1.Text = string.Empty;
			SetCompanyLs1(cCompanyLs1,string.Empty);
			SetProjectLs1(cProjectLs1,string.Empty);
			cModifiedOn1.Text = string.Empty;
			SetHintQuestionId1(cHintQuestionId1,string.Empty);
			cHintAnswer1.Text = string.Empty;
			cInvestorId1.ClearSearch();
			cCustomerId1.ClearSearch();
			cVendorId1.ClearSearch();
			cAgentId1.ClearSearch();
			cBrokerId1.ClearSearch();
			cMemberId1.ClearSearch();
			cLenderId1.ClearSearch();
			cBorrowerId1.ClearSearch();
			cGuarantorId1.ClearSearch();
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmUsr1List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmUsr1List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmUsr1List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmUsr1ById",cAdmUsr1List.SelectedValue,null,null);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmUsr1List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cUsrId1.Text = RO.Common3.Utils.fmNumeric("0",dr["UsrId1"].ToString(),base.LUser.Culture);} catch {cUsrId1.Text = string.Empty;}
					try {cLoginName1.Text = dr["LoginName1"].ToString();} catch {cLoginName1.Text = string.Empty;}
					try {cUsrName1.Text = dr["UsrName1"].ToString();} catch {cUsrName1.Text = string.Empty;}
					SetCultureId1(cCultureId1,dr["CultureId1"].ToString());
					cCultureId1Search_Script();
					SetDefCompanyId1(cDefCompanyId1,dr["DefCompanyId1"].ToString());
					SetDefProjectId1(cDefProjectId1,dr["DefProjectId1"].ToString());
					SetDefSystemId1(cDefSystemId1,dr["DefSystemId1"].ToString());
					cDefSystemId1Search_Script();
					try {cUsrEmail1.Text = dr["UsrEmail1"].ToString();} catch {cUsrEmail1.Text = string.Empty;}
					try {cUsrMobile1.Text = dr["UsrMobile1"].ToString();} catch {cUsrMobile1.Text = string.Empty;}
					SetUsrGroupLs1(cUsrGroupLs1,dr["UsrGroupLs1"].ToString());
					cUsrGroupLs1Search_Script();
					SetUsrImprLink1(cUsrImprLink1,string.Empty);
					try { if (dr["PicMed1"].Equals(System.DBNull.Value)) { cPicMed1.ImageUrl = "~/images/DefaultImg.png"; } else { cPicMed1.OnClientClick = "window.open('" + GetUrlWithQSHash("DnLoad.aspx?key=" + dr["UsrId1"].ToString() + "&tbl=dbo.Usr&knm=UsrId&col=PicMed&hgt=100&wth=100&sys=3") + "'); return false;"; cPicMed1.ImageUrl = RO.Common3.Utils.BlobPlaceHolder(dr["PicMed1"] as byte[],true);} cPicMed1_Click(sender, new ImageClickEventArgs(0, 0)); }
					catch { cPicMed1.ImageUrl = string.Empty; }
					try {cIPAlert1.Checked = base.GetBool(dr["IPAlert1"].ToString());} catch {cIPAlert1.Checked = false;}
					try {cPwdNoRepeat1.Text = RO.Common3.Utils.fmNumeric("0",dr["PwdNoRepeat1"].ToString(),base.LUser.Culture);} catch {cPwdNoRepeat1.Text = string.Empty;}
					try {cPwdDuration1.Text = RO.Common3.Utils.fmNumeric("0",dr["PwdDuration1"].ToString(),base.LUser.Culture);} catch {cPwdDuration1.Text = string.Empty;}
					try {cPwdWarn1.Text = RO.Common3.Utils.fmNumeric("0",dr["PwdWarn1"].ToString(),base.LUser.Culture);} catch {cPwdWarn1.Text = string.Empty;}
					try {cActive1.Checked = base.GetBool(dr["Active1"].ToString());} catch {cActive1.Checked = false;}
					try {cInternalUsr1.Checked = base.GetBool(dr["InternalUsr1"].ToString());} catch {cInternalUsr1.Checked = false;}
					try {cTechnicalUsr1.Checked = base.GetBool(dr["TechnicalUsr1"].ToString());} catch {cTechnicalUsr1.Checked = false;}
					try {cEmailLink1.Text = dr["EmailLink1"].ToString();} catch {cEmailLink1.Text = string.Empty;}
					if (string.IsNullOrEmpty(dr["UsrEmail1URL"].ToString())) {cEmailLink1.Visible = false;}
					else
					{
						cEmailLink1.Visible = true;
						if (dr["UsrEmail1URL"].ToString() == string.Empty) { cEmailLink1.NavigateUrl = string.Empty; }
						else { cEmailLink1.Style.Value = "cursor:pointer;"; cEmailLink1.NavigateUrl = Utils.AddTilde(GetUrlWithQSHash(dr["UsrEmail1URL"].ToString())); }
					    try {cEmailLink1.Text = dr["EmailLink1"].ToString();} catch {cEmailLink1.Text = string.Empty;}
					}
					try {cMobileLink1.Text = dr["MobileLink1"].ToString();} catch {cMobileLink1.Text = string.Empty;}
					if (string.IsNullOrEmpty(dr["UsrMobile1URL"].ToString())) {cMobileLink1.Visible = false;}
					else
					{
						cMobileLink1.Visible = true;
						if (dr["UsrMobile1URL"].ToString() == string.Empty) { cMobileLink1.NavigateUrl = string.Empty; }
						else { cMobileLink1.Style.Value = "cursor:pointer;"; cMobileLink1.NavigateUrl = Utils.AddTilde(GetUrlWithQSHash(dr["UsrMobile1URL"].ToString())); }
					    try {cMobileLink1.Text = dr["MobileLink1"].ToString();} catch {cMobileLink1.Text = string.Empty;}
					}
					try {cFailedAttempt1.Text = RO.Common3.Utils.fmNumeric("0",dr["FailedAttempt1"].ToString(),base.LUser.Culture);} catch {cFailedAttempt1.Text = string.Empty;}
					try {cLastSuccessDt1.Text = RO.Common3.Utils.fmLongDateTime(dr["LastSuccessDt1"].ToString(),base.LUser.Culture);} catch {cLastSuccessDt1.Text = string.Empty;}
					try {cLastFailedDt1.Text = RO.Common3.Utils.fmLongDateTime(dr["LastFailedDt1"].ToString(),base.LUser.Culture);} catch {cLastFailedDt1.Text = string.Empty;}
					SetCompanyLs1(cCompanyLs1,dr["CompanyLs1"].ToString());
					SetProjectLs1(cProjectLs1,dr["ProjectLs1"].ToString());
					try {cModifiedOn1.Text = RO.Common3.Utils.fmShortDateTimeUTC(dr["ModifiedOn1"].ToString(),base.LUser.Culture,CurrTimeZoneInfo());} catch {cModifiedOn1.Text = string.Empty;}
					SetHintQuestionId1(cHintQuestionId1,dr["HintQuestionId1"].ToString());
					try {cHintAnswer1.Text = dr["HintAnswer1"].ToString();} catch {cHintAnswer1.Text = string.Empty;}
					SetInvestorId1(cInvestorId1,dr["InvestorId1"].ToString());
					SetCustomerId1(cCustomerId1,dr["CustomerId1"].ToString());
					SetVendorId1(cVendorId1,dr["VendorId1"].ToString());
					SetAgentId1(cAgentId1,dr["AgentId1"].ToString());
					SetBrokerId1(cBrokerId1,dr["BrokerId1"].ToString());
					SetMemberId1(cMemberId1,dr["MemberId1"].ToString());
					SetLenderId1(cLenderId1,dr["LenderId1"].ToString());
					SetBorrowerId1(cBorrowerId1,dr["BorrowerId1"].ToString());
					SetGuarantorId1(cGuarantorId1,dr["GuarantorId1"].ToString());
				}
			}
			cButPanel.DataBind();
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmUsr1List.FocusID);
			ShowDirty(false); PanelTop.Update();
			cPicMed1Fi.Attributes["onchange"] = "if('" + cUsrId1.Text + "'==''){PopDialog('','Please save the record first before upload','');}else{sendFile(this.files[0],'" + GetUrlWithQSHash("UpLoad.aspx?key=" + cUsrId1.Text + "&tbl=dbo.Usr&knm=UsrId&col=PicMed&hgt=100&wth=100&sys=" + base.LCurr.SystemId.ToString()) + "',refreshUploadCallback(this,'" + cPicMed1.ClientID + "')); return false;}";
			cPicMed1Del.Attributes["onclick"] = "sendFile('','" + GetUrlWithQSHash("UpLoad.aspx?del=true&key=" + cUsrId1.Text + "&tbl=dbo.Usr&knm=UsrId&col=PicMed&hgt=100&wth=100&sys=" + base.LCurr.SystemId.ToString()) + "',refreshUploadCallback(this,'" + cPicMed1.ClientID + "'));return false;";
			//WebRule: Usage calendar display (2 of 2)
            if (!string.IsNullOrEmpty(cUsrId1.Text))
            {
                DataView dv = new DataView((new AdminSystem()).GetDdl(1, "WrGetUageCalender", false, true, 0, cUsrId1.Text, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                /* Must return date in yyyy/mm/dd format (111) */
                System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> dayList = (from r in dv.Table.AsEnumerable()
                select new System.Collections.Generic.Dictionary<string, string>
                { 
                    { "date", r["UsageDt"].ToString()},
                    { "content", r["Cnt"].ToString()},
                    { "color", r["Color"].ToString()},
                    { "url", string.Empty},
                }
                ).ToList();
                string resetYear = string.Empty;
                string resetMonth = string.Empty;
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "CalendarSel", @"<script type='text/javascript' language='javascript'>DC={resetYear:'" + resetYear + "',resetMonth:'" + resetMonth + "',data:" + jss.Serialize(dayList) + @"};</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "CalendarSel", @"<script type='text/javascript' language='javascript'>DC={};</script>", false);
            }

			// *** WebRule End *** //
		}

		protected void cCultureId1_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cCultureId1_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cCultureId1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cCultureId1.Items.Count > 0 && cCultureId1.DataSource != null)
			{
				DataView dv = (DataView) cCultureId1.DataSource; dv.RowFilter = string.Empty;
				DataRowView dr = cCultureId1.DataSetIndex >= 0 && cCultureId1.DataSetIndex < dv.Count ? dv[cCultureId1.DataSetIndex] : dv[0];
				cCultureId1Search_Script();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cDefSystemId1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cDefSystemId1.Items.Count > 0 && Session[KEY_dtDefSystemId1] != null)
			{
				DataView dv = ((DataTable)Session[KEY_dtDefSystemId1]).DefaultView; dv.RowFilter = string.Empty;
				DataRowView dr = cDefSystemId1.SelectedIndex >= 0 && cDefSystemId1.SelectedIndex < dv.Count ? dv[cDefSystemId1.SelectedIndex] : dv[0];
				cDefSystemId1Search_Script();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cUsrGroupLs1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cUsrGroupLs1.Items.Count > 0 && Session[KEY_dtUsrGroupLs1] != null)
			{
				DataView dv = ((DataTable)Session[KEY_dtUsrGroupLs1]).DefaultView; dv.RowFilter = string.Empty;
				DataRowView dr = cUsrGroupLs1.SelectedIndex >= 0 && cUsrGroupLs1.SelectedIndex < dv.Count ? dv[cUsrGroupLs1.SelectedIndex] : dv[0];
				cUsrGroupLs1Search_Script();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cUsrImprLink1_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
		{
			cUsrImprLink1.CurrentPageIndex = e.NewPageIndex;
			DataView dv = ((DataTable)Session[KEY_dtUsrImprLink1]).DefaultView;dv.RowFilter = ViewState["UsrImprLink_RowFilter"] as string;
			cUsrImprLink1.DataSource = dv; cUsrImprLink1.DataBind();
		}

		protected void cPicMed1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		public void cNewSaveButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			try {
			    string msg = SaveDb(sender, new EventArgs());
			    cNewButton_Click(sender, new EventArgs());
			    if (msg != string.Empty && Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(msg); }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cNewButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
			PopAdmUsr1List(sender, e, false, null);
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cClearButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			ClearMaster(sender, e);
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cCopyButton_Click(object sender, System.EventArgs e)
		{
			//WebRule: Make sure essential fields are not copied
            cFailedAttempt1.Text = string.Empty;
            cLastSuccessDt1.Text = string.Empty;
            cLastFailedDt1.Text = string.Empty;
            cHintQuestionId1.ClearSelection();
            cHintAnswer1.Text = string.Empty;
            cModifiedOn1.Text = string.Empty;
			// *** WebRule End *** //
			cUsrId1.Text = string.Empty;
			cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
			ShowDirty(true);
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cCopySaveButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			try {
			    string msg = SaveDb(sender, new EventArgs());
			    cCopyButton_Click(sender, new EventArgs());
			    if (msg != string.Empty && Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(msg); }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cUndoAllButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			if (cUsrId1.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmUsr1List(sender, e, false, cUsrId1.Text); }
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cPreviewButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cEditButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			cScreenSearch.Visible = true;
			cSystem.Visible = true;
			bUseCri.Value = "Y"; GetCriteria(GetScrCriteria());
			Session.Remove(KEY_dtAdmUsr1List); PopAdmUsr1List(sender, e, false, null);
			cEditButton.Visible = false;
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cSaveCloseButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			try {
			    string msg = SaveDb(sender, new EventArgs());
			    if (msg != string.Empty)
			    {
			        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "closeDlg", @"<script type='text/javascript'>window.parent.closeParentDlg();</script>", false);
			    }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private string SaveDb(object sender, System.EventArgs e)
		{
			string rtn = string.Empty;
			// *** System Button Click (Before) Web Rule starts here *** //
			string pid = string.Empty;
			if (ValidPage())
			{
				AdmUsr1 ds = PrepAdmUsrData(null,cUsrId1.Text == string.Empty);
				if (string.IsNullOrEmpty(cAdmUsr1List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(1,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmUsr1List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(1,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
						ShowDirty(false); PopAdmUsr1List(sender, e, false, ds.Tables["AdmUsr"].Rows[0]["UsrId1"]);
						rtn = GetScreenHlp().Rows[0]["UpdMsg"].ToString();
					}
				}
			}
			// *** System Button Click (After) Web Rule starts here *** //
			return rtn;
		}

		public void cSaveButton_Click(object sender, System.EventArgs e)
		{
			try {
			    string msg = SaveDb(sender, e);
			    if (msg != string.Empty && Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(msg); }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
		}

		public void cDeleteButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			if (cUsrId1.Text != string.Empty)
			{
				AdmUsr1 ds = PrepAdmUsrData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(1,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc))
					{
						cAdmUsr1List.ClearSearch(); Session.Remove(KEY_dtAdmUsr1List);
						ShowDirty(false); PopAdmUsr1List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmUsr1 PrepAdmUsrData(DataView dv, bool bAdd)
		{
			AdmUsr1 ds = new AdmUsr1();
			DataRow dr = ds.Tables["AdmUsr"].NewRow();
			DataRow drType = ds.Tables["AdmUsr"].NewRow();
			DataRow drDisp = ds.Tables["AdmUsr"].NewRow();
			if (bAdd) { dr["UsrId1"] = string.Empty; } else { dr["UsrId1"] = cUsrId1.Text; }
			drType["UsrId1"] = "Numeric"; drDisp["UsrId1"] = "TextBox";
			try {dr["LoginName1"] = cLoginName1.Text.Trim();} catch {}
			drType["LoginName1"] = "VarWChar"; drDisp["LoginName1"] = "TextBox";
			try {dr["UsrName1"] = cUsrName1.Text.Trim();} catch {}
			drType["UsrName1"] = "VarWChar"; drDisp["UsrName1"] = "TextBox";
			try {dr["CultureId1"] = cCultureId1.SelectedValue;} catch {}
			drType["CultureId1"] = "Numeric"; drDisp["CultureId1"] = "AutoComplete";
			try {dr["DefCompanyId1"] = cDefCompanyId1.SelectedValue;} catch {}
			drType["DefCompanyId1"] = "Numeric"; drDisp["DefCompanyId1"] = "DropDownList";
			try {dr["DefProjectId1"] = cDefProjectId1.SelectedValue;} catch {}
			drType["DefProjectId1"] = "Numeric"; drDisp["DefProjectId1"] = "DropDownList";
			try {dr["DefSystemId1"] = cDefSystemId1.SelectedValue;} catch {}
			drType["DefSystemId1"] = "Numeric"; drDisp["DefSystemId1"] = "DropDownList";
			try {dr["UsrEmail1"] = cUsrEmail1.Text.Trim();} catch {}
			drType["UsrEmail1"] = "VarWChar"; drDisp["UsrEmail1"] = "TextBox";
			try {dr["UsrMobile1"] = cUsrMobile1.Text.Trim();} catch {}
			drType["UsrMobile1"] = "VarChar"; drDisp["UsrMobile1"] = "TextBox";
			foreach (ListItem li in cUsrGroupLs1.Items)
			{
				if (li.Selected && li.Value != string.Empty)
				{
					if (dr["UsrGroupLs1"].ToString() != string.Empty) { dr["UsrGroupLs1"] = dr["UsrGroupLs1"].ToString() + ","; }
					dr["UsrGroupLs1"] = dr["UsrGroupLs1"].ToString() + li.Value; 
				}
			}
			if (dr["UsrGroupLs1"].ToString() != string.Empty) { dr["UsrGroupLs1"] = "(" + dr["UsrGroupLs1"].ToString() + ")"; }
			drType["UsrGroupLs1"] = "VarChar"; drDisp["UsrGroupLs1"] = "ListBox";
			try {dr["IPAlert1"] = base.SetBool(cIPAlert1.Checked);} catch {}
			drType["IPAlert1"] = "Char"; drDisp["IPAlert1"] = "CheckBox";
			try {dr["PwdNoRepeat1"] = cPwdNoRepeat1.Text.Trim();} catch {}
			drType["PwdNoRepeat1"] = "Numeric"; drDisp["PwdNoRepeat1"] = "TextBox";
			try {dr["PwdDuration1"] = cPwdDuration1.Text.Trim();} catch {}
			drType["PwdDuration1"] = "Numeric"; drDisp["PwdDuration1"] = "TextBox";
			try {dr["PwdWarn1"] = cPwdWarn1.Text.Trim();} catch {}
			drType["PwdWarn1"] = "Numeric"; drDisp["PwdWarn1"] = "TextBox";
			try {dr["Active1"] = base.SetBool(cActive1.Checked);} catch {}
			drType["Active1"] = "Char"; drDisp["Active1"] = "CheckBox";
			try {dr["InternalUsr1"] = base.SetBool(cInternalUsr1.Checked);} catch {}
			drType["InternalUsr1"] = "Char"; drDisp["InternalUsr1"] = "CheckBox";
			try {dr["TechnicalUsr1"] = base.SetBool(cTechnicalUsr1.Checked);} catch {}
			drType["TechnicalUsr1"] = "Char"; drDisp["TechnicalUsr1"] = "CheckBox";
			try {dr["EmailLink1"] = cEmailLink1.Text;} catch {}
			drType["EmailLink1"] = "VarWChar"; drDisp["EmailLink1"] = "HyperLink";
			try {dr["MobileLink1"] = cMobileLink1.Text;} catch {}
			drType["MobileLink1"] = "VarChar"; drDisp["MobileLink1"] = "HyperLink";
			try {dr["FailedAttempt1"] = cFailedAttempt1.Text;} catch {}
			drType["FailedAttempt1"] = "Numeric"; drDisp["FailedAttempt1"] = "StarRating";
			try {dr["LastSuccessDt1"] = base.ToIntDateTime(cLastSuccessDt1.Text,false,true);} catch {}
			drType["LastSuccessDt1"] = "DBTimeStamp"; drDisp["LastSuccessDt1"] = "LongDateTime";
			try {dr["LastFailedDt1"] = base.ToIntDateTime(cLastFailedDt1.Text,false,true);} catch {}
			drType["LastFailedDt1"] = "DBTimeStamp"; drDisp["LastFailedDt1"] = "LongDateTime";
			foreach (ListItem li in cCompanyLs1.Items)
			{
				if (li.Selected && li.Value != string.Empty)
				{
					if (dr["CompanyLs1"].ToString() != string.Empty) { dr["CompanyLs1"] = dr["CompanyLs1"].ToString() + ","; }
					dr["CompanyLs1"] = dr["CompanyLs1"].ToString() + li.Value; 
				}
			}
			if (dr["CompanyLs1"].ToString() != string.Empty) { dr["CompanyLs1"] = "(" + dr["CompanyLs1"].ToString() + ")"; }
			drType["CompanyLs1"] = "VarChar"; drDisp["CompanyLs1"] = "ListBox";
			foreach (ListItem li in cProjectLs1.Items)
			{
				if (li.Selected && li.Value != string.Empty)
				{
					if (dr["ProjectLs1"].ToString() != string.Empty) { dr["ProjectLs1"] = dr["ProjectLs1"].ToString() + ","; }
					dr["ProjectLs1"] = dr["ProjectLs1"].ToString() + li.Value; 
				}
			}
			if (dr["ProjectLs1"].ToString() != string.Empty) { dr["ProjectLs1"] = "(" + dr["ProjectLs1"].ToString() + ")"; }
			drType["ProjectLs1"] = "VarChar"; drDisp["ProjectLs1"] = "ListBox";
			try {dr["HintQuestionId1"] = cHintQuestionId1.SelectedValue;} catch {}
			drType["HintQuestionId1"] = "Numeric"; drDisp["HintQuestionId1"] = "DropDownList";
			try {dr["HintAnswer1"] = cHintAnswer1.Text.Trim();} catch {}
			drType["HintAnswer1"] = "VarWChar"; drDisp["HintAnswer1"] = "TextBox";
			try {dr["InvestorId1"] = cInvestorId1.SelectedValue;} catch {}
			drType["InvestorId1"] = "Numeric"; drDisp["InvestorId1"] = "AutoComplete";
			try {dr["CustomerId1"] = cCustomerId1.SelectedValue;} catch {}
			drType["CustomerId1"] = "Numeric"; drDisp["CustomerId1"] = "AutoComplete";
			try {dr["VendorId1"] = cVendorId1.SelectedValue;} catch {}
			drType["VendorId1"] = "Numeric"; drDisp["VendorId1"] = "AutoComplete";
			try {dr["AgentId1"] = cAgentId1.SelectedValue;} catch {}
			drType["AgentId1"] = "Numeric"; drDisp["AgentId1"] = "AutoComplete";
			try {dr["BrokerId1"] = cBrokerId1.SelectedValue;} catch {}
			drType["BrokerId1"] = "Numeric"; drDisp["BrokerId1"] = "AutoComplete";
			try {dr["MemberId1"] = cMemberId1.SelectedValue;} catch {}
			drType["MemberId1"] = "Numeric"; drDisp["MemberId1"] = "AutoComplete";
			try {dr["LenderId1"] = cLenderId1.SelectedValue;} catch {}
			drType["LenderId1"] = "Numeric"; drDisp["LenderId1"] = "AutoComplete";
			try {dr["BorrowerId1"] = cBorrowerId1.SelectedValue;} catch {}
			drType["BorrowerId1"] = "Numeric"; drDisp["BorrowerId1"] = "AutoComplete";
			try {dr["GuarantorId1"] = cGuarantorId1.SelectedValue;} catch {}
			drType["GuarantorId1"] = "Numeric"; drDisp["GuarantorId1"] = "AutoComplete";
			try {dr["UsageStat"] = cUsageStat.Text;} catch {}
			drType["UsageStat"] = string.Empty; drDisp["UsageStat"] = "Label";
			if (bAdd)
			{
			}
			ds.Tables["AdmUsr"].Rows.Add(dr); ds.Tables["AdmUsr"].Rows.Add(drType); ds.Tables["AdmUsr"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmUsr1List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtCultureId1];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'CultureId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtDefSystemId1];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'DefSystemId', please investigate."); return false;
			}
			return true;
		}

		protected void cbPostBack(object sender, System.EventArgs e)
		{
		}

		protected void IgnoreHeaderConfirm(LinkButton lb)
		{
		    if (lb != null && (lb.Attributes["OnClick"] == null || lb.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && lb.Visible) { lb.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
		}

		protected void IgnoreConfirm()
		{
			if (cExpTxtButton.Attributes["OnClick"] == null || cExpTxtButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cExpTxtButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cExpRtfButton.Attributes["OnClick"] == null || cExpRtfButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cExpRtfButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cUndoAllButton.Attributes["OnClick"] == null || cUndoAllButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cUndoAllButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSaveCloseButton.Attributes["OnClick"] == null || cSaveCloseButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cSaveCloseButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSaveButton.Attributes["OnClick"] == null || cSaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cSaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cNewSaveButton.Attributes["OnClick"] == null || cNewSaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cNewSaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cCopySaveButton.Attributes["OnClick"] == null || cCopySaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cCopySaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cDeleteButton.Attributes["OnClick"] == null || cDeleteButton.Attributes["OnClick"].IndexOf("return confirm") < 0) {cDeleteButton.Attributes["OnClick"] += "return confirm('Delete this record for sure?');";}
			if (cClearButton.Attributes["OnClick"] == null || cClearButton.Attributes["OnClick"].IndexOf("return confirm") < 0) {cClearButton.Attributes["OnClick"] += "return confirm('Clear this record for sure and assume initial defaulted values except non-editable fields? You may click the New button if you wish to create a new record instead.');";}
		}

		protected void InitPreserve()
		{
			cSystemId.Attributes["onchange"] = "javascript:return CanPostBack(true,this);";cSystemId.Attributes["NeedConfirm"] = "Y";
			cFilterId.Attributes["onchange"] = "javascript:return CanPostBack(true,this);";cFilterId.Attributes["NeedConfirm"] = "Y";
		}

		protected void ShowDirty(bool bShow)
		{
			if (bShow) {bPgDirty.Value = "Y";} else {bPgDirty.Value = "N";}
		}

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmUsr1List,null); }

		private void PreMsgPopup(string msg, RoboCoder.WebControls.ComboBox cb, WebControl wc)
		{
		    if (string.IsNullOrEmpty(msg)) return;
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/warning.gif";
		    string focusOnCloseId = cb != null ? cb.FocusID : (wc != null ? wc.ClientID : string.Empty);
		    string msgContent = ReformatErrMsg(msg);
		    if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
		    if (bErrNow.Value == "Y") { iconUrl = "images/error.gif"; bErrNow.Value = "N"; }
		    else if (bInfoNow.Value == "Y") { iconUrl = "images/info.gif"; bInfoNow.Value = "N"; }
			string script =
			@"<script type='text/javascript' lang='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
		}

		private Control FindEditableControl(Control root)
		{
		    Control found = null;
		    if (IsEditableControl(root)) { found = root; return found; }
		    foreach (Control c in root.Controls)
		    {
		        found = FindEditableControl(c); if (found != null) return found;
		    }
		    return null;
		}
	}
}

