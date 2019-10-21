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
	public class AdmSystems87 : DataSet
	{
		public AdmSystems87()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmSystems")));
			this.DataSetName = "AdmSystems87";
			this.Namespace = "http://Rintagi.com/DataSet/AdmSystems87";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("SystemId45", typeof(string));
			columns.Add("ServerName45", typeof(string));
			columns.Add("SystemName45", typeof(string));
			columns.Add("SystemAbbr45", typeof(string));
			columns.Add("SysProgram45", typeof(string));
			columns.Add("Active45", typeof(string));
			columns.Add("AddDbs", typeof(string));
			columns.Add("dbAppProvider45", typeof(string));
			columns.Add("dbAppServer45", typeof(string));
			columns.Add("dbAppDatabase45", typeof(string));
			columns.Add("dbDesDatabase45", typeof(string));
			columns.Add("dbAppUserId45", typeof(string));
			columns.Add("dbAppPassword45", typeof(string));
			columns.Add("dbX01Provider45", typeof(string));
			columns.Add("dbX01Server45", typeof(string));
			columns.Add("dbX01Database45", typeof(string));
			columns.Add("dbX01UserId45", typeof(string));
			columns.Add("dbX01Password45", typeof(string));
			columns.Add("dbX01Extra45", typeof(string));
			columns.Add("AdminEmail45", typeof(string));
			columns.Add("AdminPhone45", typeof(string));
			columns.Add("CustServEmail45", typeof(string));
			columns.Add("CustServPhone45", typeof(string));
			columns.Add("CustServFax45", typeof(string));
			columns.Add("WebAddress45", typeof(string));
			columns.Add("ResetFromGitRepo", typeof(string));
			columns.Add("CreateReactBase", typeof(string));
			columns.Add("RemoveReactBase", typeof(string));
			columns.Add("PublishReactToSite", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmSystemsModule : RO.Web.ModuleBase
	{
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_87";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_87";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_87";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_87";
		private const string KEY_dtLabel = "Cache:dtLabel3_87";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_87";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_87";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_87";


		private const string KEY_dtSystems = "Cache:dtSystems3_87";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_87";
		private const string KEY_dtAdmSystems87List = "Cache:dtAdmSystems87List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_87";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_87";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_87";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_87";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_87";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_87";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_87";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_87";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmSystemsModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,87, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				Session.Remove(KEY_dtAdmSystems87List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
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
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 87, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmSystems87List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=87&typ=N&sys=3','','',''); return false;"; }
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
			ScriptManager.RegisterStartupScript(this, this.GetType(), "ScreenLabel", this.ClientID + "={" + string.Join(",", (from dr in GetLabel().AsEnumerable() select string.Format("{0}: {{msg:'{1}',hint:'{2}'}}", "c" + dr.Field<string>("ColumnName") + (dr.Field<int?>("TableId").ToString()), (dr.Field<string>("ErrMessage") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"), (dr.Field<string>("TbHint") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"))).ToArray()) + "};", true);
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			// *** Page Init (Front of) Web Rule starts here *** //
			InitializeComponent();
			// *** Page Init (End of) Web Rule starts here *** //
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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,87,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(87, "Systems Maintenance", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(87,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtRul = (new AdminSystem()).GetClientRule(87,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(87,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(87,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,87,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(87,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetScreenLabel(87,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(87,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(87,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(87,"GetExpAdmSystems87","Y",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(87,"GetExpAdmSystems87","N",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(87,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + (char)9);}
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
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["SystemId45"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ServerName45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SystemName45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SystemAbbr45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SysProgram45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["Active45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbAppProvider45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbAppServer45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbAppDatabase45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbDesDatabase45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbAppUserId45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbAppPassword45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01Provider45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01Server45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01Database45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01UserId45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01Password45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["dbX01Extra45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AdminEmail45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AdminPhone45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["CustServEmail45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["CustServPhone45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["CustServFax45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["WebAddress45"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmSystems.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmSystems.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(87,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				if (dtAu != null)
				{
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
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
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + @"\cell ");}
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["SystemId45"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ServerName45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["SystemName45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["SystemAbbr45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["SysProgram45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["Active45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["dbAppProvider45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["dbAppServer45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["dbAppDatabase45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(drv["dbDesDatabase45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(drv["dbAppUserId45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(drv["dbAppPassword45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01Provider45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01Server45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01Database45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01UserId45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01Password45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["dbX01Extra45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["AdminEmail45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["AdminPhone45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["CustServEmail45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(drv["CustServPhone45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(drv["CustServFax45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(drv["WebAddress45"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 87, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			// *** Criteria Trigger (before) Web Rule starts here *** //
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
			PopAdmSystems87List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("87", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("87", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("87", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("87", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("87", LcSysConnString, LcAppPw);
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
		            context["scr"] = "87";
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
		            context["scr"] = "87";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmSystems87" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmSystems87" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("87", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("87", "AdmSystems87", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			Session[KEY_dsScrCriVal] = ds;
			return ds;
		}

		private DataView GetAdmSystems87List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmSystems87List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(87,"GetLisAdmSystems87",true,"Y",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(87,"GetLisAdmSystems87",true,"N",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmSystems87List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmSystems87List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmSystems87";
			context["mKey"] = "SystemId45";
			context["mVal"] = "SystemId45Text";
			context["mTip"] = "SystemId45Text";
			context["mImg"] = "SystemId45Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "87";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "Y";
			context["conn"] = string.Empty;
			cAdmSystems87List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmSystems87List.DataContext = context;
			if (dv.Table == null) return;
			cAdmSystems87List.DataSource = dv;
			cAdmSystems87List.Visible = true;
			if (cAdmSystems87List.Items.Count <= 0) {cAdmSystems87List.Visible = false; cAdmSystems87List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmSystems87List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmSystems87List.Items[0].Selected = true; cAdmSystems87List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmSystems87List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmSystems87List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cSystemId45, dtAuth.Rows[0], cSystemId45P1, cSystemId45Label, cSystemId45P2, null, dtLabel.Rows[0], cRFVSystemId45, null, null);
				base.SetFoldBehavior(cServerName45, dtAuth.Rows[1], cServerName45P1, cServerName45Label, cServerName45P2, null, dtLabel.Rows[1], cRFVServerName45, null, null);
				base.SetFoldBehavior(cSystemName45, dtAuth.Rows[2], cSystemName45P1, cSystemName45Label, cSystemName45P2, null, dtLabel.Rows[2], cRFVSystemName45, null, null);
				base.SetFoldBehavior(cSystemAbbr45, dtAuth.Rows[3], cSystemAbbr45P1, cSystemAbbr45Label, cSystemAbbr45P2, null, dtLabel.Rows[3], cRFVSystemAbbr45, null, null);
				base.SetFoldBehavior(cSysProgram45, dtAuth.Rows[4], cSysProgram45P1, cSysProgram45Label, cSysProgram45P2, null, dtLabel.Rows[4], null, null, null);
				base.SetFoldBehavior(cActive45, dtAuth.Rows[5], cActive45P1, cActive45Label, cActive45P2, null, dtLabel.Rows[5], null, null, null);
				base.SetFoldBehavior(cAddDbs, dtAuth.Rows[6], cAddDbsP1, cAddDbsLabel, cAddDbsP2, null, dtLabel.Rows[6], null, null, null);
				base.SetFoldBehavior(cdbAppProvider45, dtAuth.Rows[7], cdbAppProvider45P1, cdbAppProvider45Label, cdbAppProvider45P2, null, dtLabel.Rows[7], cRFVdbAppProvider45, null, null);
				base.SetFoldBehavior(cdbAppServer45, dtAuth.Rows[8], cdbAppServer45P1, cdbAppServer45Label, cdbAppServer45P2, null, dtLabel.Rows[8], cRFVdbAppServer45, null, null);
				base.SetFoldBehavior(cdbAppDatabase45, dtAuth.Rows[9], cdbAppDatabase45P1, cdbAppDatabase45Label, cdbAppDatabase45P2, null, dtLabel.Rows[9], cRFVdbAppDatabase45, null, null);
				base.SetFoldBehavior(cdbDesDatabase45, dtAuth.Rows[10], cdbDesDatabase45P1, cdbDesDatabase45Label, cdbDesDatabase45P2, null, dtLabel.Rows[10], cRFVdbDesDatabase45, null, null);
				base.SetFoldBehavior(cdbAppUserId45, dtAuth.Rows[11], cdbAppUserId45P1, cdbAppUserId45Label, cdbAppUserId45P2, null, dtLabel.Rows[11], cRFVdbAppUserId45, null, null);
				base.SetFoldBehavior(cdbAppPassword45, dtAuth.Rows[12], cdbAppPassword45P1, cdbAppPassword45Label, cdbAppPassword45P2, null, dtLabel.Rows[12], cRFVdbAppPassword45, null, null);
				base.SetFoldBehavior(cdbX01Provider45, dtAuth.Rows[13], cdbX01Provider45P1, cdbX01Provider45Label, cdbX01Provider45P2, null, dtLabel.Rows[13], null, null, null);
				base.SetFoldBehavior(cdbX01Server45, dtAuth.Rows[14], cdbX01Server45P1, cdbX01Server45Label, cdbX01Server45P2, null, dtLabel.Rows[14], null, null, null);
				base.SetFoldBehavior(cdbX01Database45, dtAuth.Rows[15], cdbX01Database45P1, cdbX01Database45Label, cdbX01Database45P2, null, dtLabel.Rows[15], null, null, null);
				base.SetFoldBehavior(cdbX01UserId45, dtAuth.Rows[16], cdbX01UserId45P1, cdbX01UserId45Label, cdbX01UserId45P2, null, dtLabel.Rows[16], null, null, null);
				base.SetFoldBehavior(cdbX01Password45, dtAuth.Rows[17], cdbX01Password45P1, cdbX01Password45Label, cdbX01Password45P2, null, dtLabel.Rows[17], null, null, null);
				base.SetFoldBehavior(cdbX01Extra45, dtAuth.Rows[18], cdbX01Extra45P1, cdbX01Extra45Label, cdbX01Extra45P2, null, dtLabel.Rows[18], null, null, null);
				base.SetFoldBehavior(cAdminEmail45, dtAuth.Rows[19], cAdminEmail45P1, cAdminEmail45Label, cAdminEmail45P2, null, dtLabel.Rows[19], cRFVAdminEmail45, null, null);
				base.SetFoldBehavior(cAdminPhone45, dtAuth.Rows[20], cAdminPhone45P1, cAdminPhone45Label, cAdminPhone45P2, null, dtLabel.Rows[20], null, null, null);
				base.SetFoldBehavior(cCustServEmail45, dtAuth.Rows[21], cCustServEmail45P1, cCustServEmail45Label, cCustServEmail45P2, null, dtLabel.Rows[21], cRFVCustServEmail45, null, null);
				base.SetFoldBehavior(cCustServPhone45, dtAuth.Rows[22], cCustServPhone45P1, cCustServPhone45Label, cCustServPhone45P2, null, dtLabel.Rows[22], null, null, null);
				base.SetFoldBehavior(cCustServFax45, dtAuth.Rows[23], cCustServFax45P1, cCustServFax45Label, cCustServFax45P2, null, dtLabel.Rows[23], null, null, null);
				base.SetFoldBehavior(cWebAddress45, dtAuth.Rows[24], cWebAddress45P1, cWebAddress45Label, cWebAddress45P2, null, dtLabel.Rows[24], null, null, null);
				base.SetFoldBehavior(cResetFromGitRepo, dtAuth.Rows[25], null, null, null, dtLabel.Rows[25], null, null, null);
				base.SetFoldBehavior(cCreateReactBase, dtAuth.Rows[26], null, null, null, dtLabel.Rows[26], null, null, null);
				base.SetFoldBehavior(cRemoveReactBase, dtAuth.Rows[27], null, null, null, dtLabel.Rows[27], null, null, null);
				base.SetFoldBehavior(cPublishReactToSite, dtAuth.Rows[28], null, null, null, dtLabel.Rows[28], null, null, null);
			}
			if ((cSystemId45.Attributes["OnChange"] == null || cSystemId45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSystemId45.Visible && !cSystemId45.ReadOnly) {cSystemId45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cServerName45.Attributes["OnChange"] == null || cServerName45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cServerName45.Visible && !cServerName45.ReadOnly) {cServerName45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSystemName45.Attributes["OnChange"] == null || cSystemName45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSystemName45.Visible && !cSystemName45.ReadOnly) {cSystemName45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSystemAbbr45.Attributes["OnChange"] == null || cSystemAbbr45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSystemAbbr45.Visible && !cSystemAbbr45.ReadOnly) {cSystemAbbr45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSysProgram45.Attributes["OnClick"] == null || cSysProgram45.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSysProgram45.Visible && cSysProgram45.Enabled) {cSysProgram45.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cActive45.Attributes["OnClick"] == null || cActive45.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cActive45.Visible && cActive45.Enabled) {cActive45.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cAddDbs.Attributes["OnChange"] == null || cAddDbs.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cAddDbs.Visible && cAddDbs.Enabled) {cAddDbs.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cdbAppProvider45.Attributes["OnChange"] == null || cdbAppProvider45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbAppProvider45.Visible && !cdbAppProvider45.ReadOnly) {cdbAppProvider45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbAppServer45.Attributes["OnChange"] == null || cdbAppServer45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbAppServer45.Visible && !cdbAppServer45.ReadOnly) {cdbAppServer45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbAppDatabase45.Attributes["OnChange"] == null || cdbAppDatabase45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbAppDatabase45.Visible && !cdbAppDatabase45.ReadOnly) {cdbAppDatabase45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbDesDatabase45.Attributes["OnChange"] == null || cdbDesDatabase45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbDesDatabase45.Visible && !cdbDesDatabase45.ReadOnly) {cdbDesDatabase45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbAppUserId45.Attributes["OnChange"] == null || cdbAppUserId45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbAppUserId45.Visible && !cdbAppUserId45.ReadOnly) {cdbAppUserId45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbAppPassword45.Attributes["OnChange"] == null || cdbAppPassword45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbAppPassword45.Visible && !cdbAppPassword45.ReadOnly) {cdbAppPassword45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01Provider45.Attributes["OnChange"] == null || cdbX01Provider45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01Provider45.Visible && !cdbX01Provider45.ReadOnly) {cdbX01Provider45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01Server45.Attributes["OnChange"] == null || cdbX01Server45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01Server45.Visible && !cdbX01Server45.ReadOnly) {cdbX01Server45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01Database45.Attributes["OnChange"] == null || cdbX01Database45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01Database45.Visible && !cdbX01Database45.ReadOnly) {cdbX01Database45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01UserId45.Attributes["OnChange"] == null || cdbX01UserId45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01UserId45.Visible && !cdbX01UserId45.ReadOnly) {cdbX01UserId45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01Password45.Attributes["OnChange"] == null || cdbX01Password45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01Password45.Visible && !cdbX01Password45.ReadOnly) {cdbX01Password45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cdbX01Extra45.Attributes["OnChange"] == null || cdbX01Extra45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cdbX01Extra45.Visible && !cdbX01Extra45.ReadOnly) {cdbX01Extra45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cAdminEmail45.Attributes["OnChange"] == null || cAdminEmail45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cAdminEmail45.Visible && !cAdminEmail45.ReadOnly) {cAdminEmail45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cAdminPhone45.Attributes["OnChange"] == null || cAdminPhone45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cAdminPhone45.Visible && !cAdminPhone45.ReadOnly) {cAdminPhone45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCustServEmail45.Attributes["OnChange"] == null || cCustServEmail45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCustServEmail45.Visible && !cCustServEmail45.ReadOnly) {cCustServEmail45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCustServPhone45.Attributes["OnChange"] == null || cCustServPhone45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCustServPhone45.Visible && !cCustServPhone45.ReadOnly) {cCustServPhone45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cCustServFax45.Attributes["OnChange"] == null || cCustServFax45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCustServFax45.Visible && !cCustServFax45.ReadOnly) {cCustServFax45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cWebAddress45.Attributes["OnChange"] == null || cWebAddress45.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cWebAddress45.Visible && !cWebAddress45.ReadOnly) {cWebAddress45.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cServerName45.Text = string.Empty;}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {cSystemName45.Text = string.Empty;}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {cSystemAbbr45.Text = string.Empty;}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {cSysProgram45.Checked = base.GetBool("N");}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {cActive45.Checked = base.GetBool("Y");}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {cAddDbs.ImageUrl = "~/images/custom/adm/SyncToDb.gif"; }
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cdbAppProvider45.Text = string.Empty;}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cdbAppServer45.Text = string.Empty;}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {cdbAppDatabase45.Text = string.Empty;}
			if (dt.Rows[10]["ColVisible"].ToString() == "Y" && dt.Rows[10]["ColReadOnly"].ToString() != "Y") {cdbDesDatabase45.Text = string.Empty;}
			if (dt.Rows[11]["ColVisible"].ToString() == "Y" && dt.Rows[11]["ColReadOnly"].ToString() != "Y") {cdbAppUserId45.Text = string.Empty;}
			if (dt.Rows[12]["ColVisible"].ToString() == "Y" && dt.Rows[12]["ColReadOnly"].ToString() != "Y") {cdbAppPassword45.Text = string.Empty;}
			if (dt.Rows[13]["ColVisible"].ToString() == "Y" && dt.Rows[13]["ColReadOnly"].ToString() != "Y") {cdbX01Provider45.Text = string.Empty;}
			if (dt.Rows[14]["ColVisible"].ToString() == "Y" && dt.Rows[14]["ColReadOnly"].ToString() != "Y") {cdbX01Server45.Text = string.Empty;}
			if (dt.Rows[15]["ColVisible"].ToString() == "Y" && dt.Rows[15]["ColReadOnly"].ToString() != "Y") {cdbX01Database45.Text = string.Empty;}
			if (dt.Rows[16]["ColVisible"].ToString() == "Y" && dt.Rows[16]["ColReadOnly"].ToString() != "Y") {cdbX01UserId45.Text = string.Empty;}
			if (dt.Rows[17]["ColVisible"].ToString() == "Y" && dt.Rows[17]["ColReadOnly"].ToString() != "Y") {cdbX01Password45.Text = string.Empty;}
			if (dt.Rows[18]["ColVisible"].ToString() == "Y" && dt.Rows[18]["ColReadOnly"].ToString() != "Y") {cdbX01Extra45.Text = string.Empty;}
			if (dt.Rows[19]["ColVisible"].ToString() == "Y" && dt.Rows[19]["ColReadOnly"].ToString() != "Y") {cAdminEmail45.Text = string.Empty;}
			if (dt.Rows[20]["ColVisible"].ToString() == "Y" && dt.Rows[20]["ColReadOnly"].ToString() != "Y") {cAdminPhone45.Text = string.Empty;}
			if (dt.Rows[21]["ColVisible"].ToString() == "Y" && dt.Rows[21]["ColReadOnly"].ToString() != "Y") {cCustServEmail45.Text = string.Empty;}
			if (dt.Rows[22]["ColVisible"].ToString() == "Y" && dt.Rows[22]["ColReadOnly"].ToString() != "Y") {cCustServPhone45.Text = string.Empty;}
			if (dt.Rows[23]["ColVisible"].ToString() == "Y" && dt.Rows[23]["ColReadOnly"].ToString() != "Y") {cCustServFax45.Text = string.Empty;}
			if (dt.Rows[24]["ColVisible"].ToString() == "Y" && dt.Rows[24]["ColReadOnly"].ToString() != "Y") {cWebAddress45.Text = string.Empty;}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cSystemId45.Text = string.Empty;
			cServerName45.Text = string.Empty;
			cSystemName45.Text = string.Empty;
			cSystemAbbr45.Text = string.Empty;
			cSysProgram45.Checked = base.GetBool("N");
			cActive45.Checked = base.GetBool("Y");
			cAddDbs.ImageUrl = "~/images/custom/adm/SyncToDb.gif";
			cdbAppProvider45.Text = string.Empty;
			cdbAppServer45.Text = string.Empty;
			cdbAppDatabase45.Text = string.Empty;
			cdbDesDatabase45.Text = string.Empty;
			cdbAppUserId45.Text = string.Empty;
			cdbAppPassword45.Text = string.Empty;
			cdbX01Provider45.Text = string.Empty;
			cdbX01Server45.Text = string.Empty;
			cdbX01Database45.Text = string.Empty;
			cdbX01UserId45.Text = string.Empty;
			cdbX01Password45.Text = string.Empty;
			cdbX01Extra45.Text = string.Empty;
			cAdminEmail45.Text = string.Empty;
			cAdminPhone45.Text = string.Empty;
			cCustServEmail45.Text = string.Empty;
			cCustServPhone45.Text = string.Empty;
			cCustServFax45.Text = string.Empty;
			cWebAddress45.Text = string.Empty;
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmSystems87List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmSystems87List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmSystems87List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmSystems87ById",cAdmSystems87List.SelectedValue,null,null);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmSystems87List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cSystemId45.Text = RO.Common3.Utils.fmNumeric("0",dr["SystemId45"].ToString(),base.LUser.Culture);} catch {cSystemId45.Text = string.Empty;}
					try {cServerName45.Text = dr["ServerName45"].ToString();} catch {cServerName45.Text = string.Empty;}
					try {cSystemName45.Text = dr["SystemName45"].ToString();} catch {cSystemName45.Text = string.Empty;}
					try {cSystemAbbr45.Text = dr["SystemAbbr45"].ToString();} catch {cSystemAbbr45.Text = string.Empty;}
					try {cSysProgram45.Checked = base.GetBool(dr["SysProgram45"].ToString());} catch {cSysProgram45.Checked = false;}
					try {cActive45.Checked = base.GetBool(dr["Active45"].ToString());} catch {cActive45.Checked = false;}
					cAddDbs.ImageUrl = "~/images/custom/adm/SyncToDb.gif";
					try {cdbAppProvider45.Text = dr["dbAppProvider45"].ToString();} catch {cdbAppProvider45.Text = string.Empty;}
					try {cdbAppServer45.Text = dr["dbAppServer45"].ToString();} catch {cdbAppServer45.Text = string.Empty;}
					try {cdbAppDatabase45.Text = dr["dbAppDatabase45"].ToString();} catch {cdbAppDatabase45.Text = string.Empty;}
					try {cdbDesDatabase45.Text = dr["dbDesDatabase45"].ToString();} catch {cdbDesDatabase45.Text = string.Empty;}
					try {cdbAppUserId45.Text = dr["dbAppUserId45"].ToString();} catch {cdbAppUserId45.Text = string.Empty;}
					try {cdbAppPassword45.Text = dr["dbAppPassword45"].ToString();} catch {cdbAppPassword45.Text = string.Empty;}
					try {cdbX01Provider45.Text = dr["dbX01Provider45"].ToString();} catch {cdbX01Provider45.Text = string.Empty;}
					try {cdbX01Server45.Text = dr["dbX01Server45"].ToString();} catch {cdbX01Server45.Text = string.Empty;}
					try {cdbX01Database45.Text = dr["dbX01Database45"].ToString();} catch {cdbX01Database45.Text = string.Empty;}
					try {cdbX01UserId45.Text = dr["dbX01UserId45"].ToString();} catch {cdbX01UserId45.Text = string.Empty;}
					try {cdbX01Password45.Text = dr["dbX01Password45"].ToString();} catch {cdbX01Password45.Text = string.Empty;}
					try {cdbX01Extra45.Text = dr["dbX01Extra45"].ToString();} catch {cdbX01Extra45.Text = string.Empty;}
					try {cAdminEmail45.Text = dr["AdminEmail45"].ToString();} catch {cAdminEmail45.Text = string.Empty;}
					try {cAdminPhone45.Text = dr["AdminPhone45"].ToString();} catch {cAdminPhone45.Text = string.Empty;}
					try {cCustServEmail45.Text = dr["CustServEmail45"].ToString();} catch {cCustServEmail45.Text = string.Empty;}
					try {cCustServPhone45.Text = dr["CustServPhone45"].ToString();} catch {cCustServPhone45.Text = string.Empty;}
					try {cCustServFax45.Text = dr["CustServFax45"].ToString();} catch {cCustServFax45.Text = string.Empty;}
					try {cWebAddress45.Text = dr["WebAddress45"].ToString();} catch {cWebAddress45.Text = string.Empty;}
				}
			}
			cButPanel.DataBind();
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmSystems87List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cAddDbs_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//WebRule: Add physical databases
            if (!string.IsNullOrEmpty(cdbAppDatabase45.Text))
            {
                RO.Rule3.Deploy deploy = new RO.Rule3.Deploy();
                string module = new Regex("^" + Config.AppNameSpace).Replace(cdbAppDatabase45.Text, "");
                deploy.TransferModule(Config.DesServer, Config.DesUserId, Config.DesPassword, Config.AppNameSpace + "Cmon", Config.AppNameSpace, "Cmon", null, null, Config.DesServer, Config.DesUserId, Config.DesPassword, Config.AppNameSpace, module, true, Config.AppNameSpace != "RO", false);
                bInfoNow.Value = "Y"; PreMsgPopup("Physical application and design databases have been created successfully.");
            }
            else
            {
                bErrNow.Value = "Y"; PreMsgPopup("Application database and design database cannot be empty, please try again.");
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cResetFromGitRepo_Click(object sender, System.EventArgs e)
		{
			//WebRule: Reset from Git Repo
            try
            {
                string webRoot = Server.MapPath(@"~/").Replace(@"\", "/");
                string appRoot = webRoot.Replace("/Web/", "");

                var changedFilesRet = Utils.WinProc(@"C:\Program Files\Git\cmd\git.exe", "status -s -uno", true, appRoot);
                var lastXcommitLog = Utils.WinProc(@"C:\Program Files\Git\cmd\git.exe", string.Format("--no-pager log -n 10 --pretty=\"%an %ci %H %s\" -n{0}", 10), true, appRoot);
                System.Collections.Generic.List<string> ruleTierProjects = new System.Collections.Generic.List<string>() { "UsrRules", "UsrAccess" };
                bool needMSBuild = ruleTierProjects.Where(m => new Regex(string.Format("/{0}/", m), RegexOptions.IgnoreCase).IsMatch(changedFilesRet.Item2)).Count() > 0;
                bool noWebSiteBuild = true;
                var revertChangesRet = Utils.WinProc(@"C:\Program Files\Git\cmd\git.exe", "checkout HEAD -- .", true, appRoot);
                if (revertChangesRet.Item1 != 0)
                {
                    throw new Exception(revertChangesRet.Item3);
                }
                System.Collections.Generic.List<string> stdOut = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> stdErr = new System.Collections.Generic.List<string>();
                //bool webSiteBuildSkipped = false;
                if (needMSBuild)
                {

                    int? _runningPid = null;
                    Func<int, string, bool> stdOutHandler = (pid, data) =>
                    {
                        _runningPid = pid;
                        stdOut.Add(data);
                        if (data.Contains("aspnet_compiler.exe") && noWebSiteBuild)
                        {
                            stdOut.Add("Skip website rebuild\r\n");
                            //webSiteBuildSkipped = true;
                            return true;
                        }
                        return false;
                    };
                    Func<int, string, bool> stdErrHandler = (pid, data) =>
                    {
                        _runningPid = pid;
                        stdErr.Add(data);
                        return false;
                    };
                    var publishRet = Utils.WinProc(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe", string.Format(""), true, stdOutHandler, stdErrHandler, appRoot);

                }
                else
                {
                    stdOut.Add("No rebuild needed\r\n");
                }

                PreMsgPopup("Changed Files\r\n" + changedFilesRet.Item2 + "Last 10 commits\r\n" + lastXcommitLog.Item2 + "\r\n\r\n Build Result\r\n" + string.Join("", stdOut.ToArray()));
            }
            catch (Exception ex)
            {
                PreMsgPopup(ex.Message);
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cCreateReactBase_Click(object sender, System.EventArgs e)
		{
			//WebRule: Create React Code Base
            try
            {
                if (string.IsNullOrEmpty(cSystemAbbr45.Text)) throw new Exception("pick the module first and make sure the Abbr is not empty");
                string systemAbbr = cSystemAbbr45.Text;
                string systemId = cSystemId45.Text;
                string webAppRoot = Server.MapPath(@"~/").Replace(@"\", "/");
                string appRoot = webAppRoot.Replace("/Web/", "");
                string reactRootDir = webAppRoot.Replace(@"/Web", "/React");
                string reactTemplateDir = reactRootDir + "/Template";
                string reactModuleDir = reactTemplateDir.Replace("/Template", "/" + systemAbbr);
                string reactModuleNodeModuleDir = reactModuleDir + "/node_modules";
                string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string siteApplicationPath = Context.Request.ApplicationPath;
                string machineName = Environment.MachineName;
                string rintagiJSContent =
string.Format(@"
/* this is runtime loading script for actual installation(production) configuration override say putting app to deep directory structure or
 * web service end point not the same as the app loading source
 * typically for situation where the apps are hosted in CDN and/or not at root level of the domain
 * for reactjs configuration, make sure homepage is set to './' so everything generated is relative 
 */
document.Rintagi = {{
  localDev:{{
    // these setup is only effective when served via app is served via http://localhost:3000 type, for local npm start development. ignored in production build or proxying to localhost
    appNS:'{4}',
    appDomainUrl:'http://{2}/{0}', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
    apiBasename: 'http://{2}/{0}', // webservice url for local development via npm start, i.e. localhost:3000 etc. must be full url in the form of http:// pointing to the site serving , no ending slash
  }},
  appRelBase:['React','ReactProxy','ReactPort'],  // path this app is serving UNDER(can be multiple), implicitly assume they are actually /Name/, do not put begin/end slash 
  appNS:'', // used for login token sync(shared login when served under the same domain) between apps and asp.net site
  appDomainUrl:'', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
  apiBasename: '', // webservice url, can be relative or full http:// etc., no ending slash
  useBrowserRouter: false,    // whether to use # based router(default) or standard browser based router(set to true, need server rewrite support, cannot be used for CDN or static file directory)
  appBasename: '{0}/react/{1}', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  appProxyBasename: '{0}/reactproxy', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  systemId: {3}                
}}
", siteApplicationPath == "/" ? "/" : siteApplicationPath.Substring(1), systemAbbr, machineName,systemId, siteApplicationPath);


                //if (homeDir.Contains("NetworkService"))
                //{
                //    // above from Windows are not the same npm is seeing in case it is run under Network Service
                //    homeDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + "/config/systemprofile";
                //    appDataDir = homeDir + "/AppData";
                //}
                //if (!Directory.Exists(homeDir + "/.config"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user",homeDir + "/.config"));
                //}
                //if (!Directory.Exists(appDataDir + "/Roaming/npm"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user", appDataDir + "/Roaming/npm"));
                //}
                //if (!Directory.Exists(appDataDir + "/Roaming/npm-cache"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user", appDataDir + "/Roaming/npm-cache"));
                //}

                if (!System.IO.Directory.Exists(reactTemplateDir)) throw new Exception(string.Format("no react template directory found({0})", reactTemplateDir));
                if (System.IO.Directory.Exists(reactModuleDir))
                {
                    //throw new Exception(string.Format("react module already exists ({0})", reactModuleDir));
                }
                else
                {
                    ////DirectoryCopy(reactTemplateDir, reactModuleDir, true, true);
                    var copyRet = Utils.WinProc("robocopy.exe", string.Format("{0} {1} /E /S /COPY:DATS /SECFIX /TIMFIX", reactTemplateDir, reactModuleDir), true, appRoot);
                    var runtimeJS = string.Format("{0}/runtime/rintagi.js", reactModuleDir);
                    using (var sr = new StreamWriter(reactModuleDir + "/public/runtime/rintagi.js", false, System.Text.UTF8Encoding.UTF8))
                    {
                        sr.WriteLine(rintagiJSContent);
                        sr.Close();
                    }
                }
                if (!File.Exists(reactModuleDir + "/.npmrc"))
                {
                    using (var sr = new StreamWriter(reactModuleDir + "/.npmrc", false, System.Text.UTF8Encoding.UTF8))
                    {
                        sr.WriteLine(string.Format("prefix={0}npm", reactRootDir.Replace("/", @"\")));
                        sr.WriteLine(string.Format("cache={0}npm-cache", reactRootDir.Replace("/", @"\")));
                        sr.WriteLine(string.Format("update-notifier=false", reactRootDir.Replace("/", @"\")));
                        sr.Flush();
                        sr.Close();
                    }
                }
                //System.Threading.Thread.Sleep(5000);
                string npmPath = @"C:\Program Files\nodejs\npm.cmd";
                //var ret = new Tuple<int,string,string>(0, "", "");
                //var ret1 = WinProc(npmPath, "cache clean --force", true, reactModuleDir);
                var installRet = Utils.WinProc(npmPath, @"install  --no-optional --no-update-notifier", true, reactModuleDir);
                if (installRet.Item1 != 0 || installRet.Item3.Contains("ERR"))
                {
                    bErrNow.Value = "Y";
                    bInfoNow.Value = "N";
                    PreMsgPopup(installRet.Item3);
                }
                else
                {

                    bInfoNow.Value = "Y";
                    bErrNow.Value = "N";
                    bool testPublish = false;
                    if (testPublish)
                    {
                        var buildRet = Utils.WinProc(npmPath, "run build", true, reactModuleDir);
                        var buildDir = reactModuleDir + "/build";
                        var webSiteTargetDir = webAppRoot + "/React/" + systemAbbr;
                        bool isReady = buildRet.Item2.Contains("The build folder is ready to be deployed");

                        var webSiteRuntimeJS = string.Format("{0}/runtime/rintagi.js", webSiteTargetDir);
                        var publishRet = Utils.WinProc("robocopy.exe", string.Format("{0} {1} /MIR /XF rintagi.js", buildDir, webSiteTargetDir), true, appRoot);
                        if (!File.Exists(webSiteRuntimeJS))
                        {
                            using (var sr = new StreamWriter(webSiteRuntimeJS, false, System.Text.UTF8Encoding.UTF8))
                            {
                                sr.WriteLine(rintagiJSContent);
                                sr.Close();
                            }
                        }
                    }
                    else
                    {
                        PreMsgPopup("React base initialized\r\n\r\n" + installRet.Item2);
                    }
                }
            }
            catch (Exception ex)
            {
                PreMsgPopup(ex.Message);
            }

			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cRemoveReactBase_Click(object sender, System.EventArgs e)
		{
			//WebRule: Remove React Code Base
            try
            {
                if (string.IsNullOrEmpty(cSystemAbbr45.Text)) throw new Exception("pick the module first and make sure the Abbr is not empty");
                string systemAbbr = cSystemAbbr45.Text;
                string webAppRoot = Server.MapPath(@"~/").Replace(@"\", "/");
                string appRoot = webAppRoot.Replace("/Web/", "");
                string reactRootDir = webAppRoot.Replace(@"/Web", "/React");
                string reactTemplateDir = reactRootDir + "/Template";
                string reactModuleDir = reactTemplateDir.Replace("/Template", "/" + systemAbbr);
                string reactModuleNodeModuleDir = reactModuleDir + "/node_modules";

                if (System.IO.Directory.Exists(reactModuleDir))
                {
                    System.IO.Directory.Delete(reactModuleDir, true);
                }

                PreMsgPopup(string.Format(string.Format("React Source Directory {0} removed", reactModuleDir)));
            }
            catch (Exception ex)
            {
                PreMsgPopup(ex.Message);
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cPublishReactToSite_Click(object sender, System.EventArgs e)
		{
			//WebRule: Publish React to Site
            try
            {
                if (string.IsNullOrEmpty(cSystemAbbr45.Text)) throw new Exception("pick the module first and make sure the Abbr is not empty");
                string systemAbbr = cSystemAbbr45.Text;
                string systemId = cSystemId45.Text;
                string webAppRoot = Server.MapPath(@"~/").Replace(@"\", "/");
                string appRoot = webAppRoot.Replace("/Web/", "");
                string reactRootDir = webAppRoot.Replace(@"/Web", "/React");
                string reactTemplateDir = reactRootDir + "/Template";
                string reactModuleDir = reactTemplateDir.Replace("/Template", "/" + systemAbbr);
                string reactModuleNodeModuleDir = reactModuleDir + "/node_modules";
                string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string siteApplicationPath = Context.Request.ApplicationPath;
                string machineName = Environment.MachineName;
                string rintagiJSContent =
string.Format(@"
/* this is runtime loading script for actual installation(production) configuration override say putting app to deep directory structure or
 * web service end point not the same as the app loading source
 * typically for situation where the apps are hosted in CDN and/or not at root level of the domain
 * for reactjs configuration, make sure homepage is set to './' so everything generated is relative 
 */
document.Rintagi = {{
  appRelBase:['React','ReactProxy','ReactPort'],  // path this app is serving UNDER(can be multiple), implicitly assume they are actually /Name/, do not put begin/end slash 
  appNS:'', // used for login token sync(shared login when served under the same domain) between apps and asp.net site
  appDomainUrl:'', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
  apiBasename: '', // webservice url, can be relative or full http:// etc., no ending slash
  useBrowserRouter: false,    // whether to use # based router(default) or standard browser based router(set to true, need server rewrite support, cannot be used for CDN or static file directory)
  appBasename: '{0}/react/{1}', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  appProxyBasename: '{0}/reactproxy', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  systemId: {3}                
}}
", siteApplicationPath == "/" ? "/" : siteApplicationPath.Substring(1), systemAbbr, machineName, systemId, siteApplicationPath);

                if (System.Configuration.ConfigurationManager.AppSettings["AdvanceReactBuildVersion"] != "N")
                {
                    using (var sr = new System.IO.StreamReader(reactModuleDir + "/package.json", System.Text.UTF8Encoding.UTF8))
                    {
                        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                        dynamic packageJson = jss.DeserializeObject(sr.ReadToEnd());
                        var ver = ((string)((IDictionary<string, object>)packageJson)["version"]).Split(new char[] { '.' });
                        var _x = ver.Select((v, i) => int.Parse(v) + (i == ver.Length - 1 ? 1 : 0)).Select(v => v.ToString()).ToArray();
                        var newVer = string.Join(".", _x);
                        ((IDictionary<string, object>)packageJson)["version"] = newVer;

                        sr.Close();

                        using (var sw = new System.IO.StreamWriter(reactModuleDir + "/src/app/Version.js", false, System.Text.UTF8Encoding.UTF8))
                        {
                            sw.WriteLine(string.Format("export const Version = '{0}';", newVer));
                            sw.Close();
                        }
                        using (var sw = new System.IO.StreamWriter(reactModuleDir + "/package.json", false, new System.Text.UTF8Encoding(false)))
                        {
                            sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packageJson, Newtonsoft.Json.Formatting.Indented).Replace(@"\u003c", "<").Replace(@"\u003e", ">"));
                            //sw.Write(jss.Serialize(packageJson).Replace(@"\u003c", "<").Replace(@"\u003e", ">"));
                            sw.Close();
                        }
                        try
                        {
                            var aa = Utils.WinProc(@"C:\Program Files\Git\cmd\git.exe", string.Format("add {0} {1}", string.Format("package.json"), string.Format("src/app/Version.js")), true, reactModuleDir);
                            var bb = Utils.WinProc(@"C:\Program Files\Git\cmd\git.exe", string.Format("commit -m \"{0}\"", string.Format("advance UI to version {0}", newVer)), true, reactModuleDir);
                        }
                        catch (Exception ex)
                        {
                            // no git is fine
                            PreMsgPopup(ex.Message);
                        }

                    };
                }

                System.Collections.Generic.List<string> stdIn = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> stdOut = new System.Collections.Generic.List<string>();
                int? _runningPid = null;
                Func<int, string, bool> stdInHandler = (pid, output) =>
                {
                    _runningPid = pid;
                    return false;
                };
                Func<int, string, bool> stdErrHandler = (pid, output) =>
                {
                    _runningPid = pid;
                    return false;
                };

                //string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                //string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //if (homeDir.Contains("NetworkService"))
                //{
                //    // above from Windows are not the same npm is seeing in case it is run under Network Service
                //    homeDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + "/config/systemprofile";
                //    appDataDir = homeDir + "/AppData";
                //}
                //if (!Directory.Exists(homeDir + "/.config"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user",homeDir + "/.config"));
                //}
                //if (!Directory.Exists(appDataDir + "/Roaming/npm"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user", appDataDir + "/Roaming/npm"));
                //}
                //if (!Directory.Exists(appDataDir + "/Roaming/npm-cache"))
                //{
                //    throw new Exception(string.Format("npm requires full access to directory {0} for the current user", appDataDir + "/Roaming/npm-cache"));
                //}
                //if (!System.IO.Directory.Exists(reactTemplateDir)) throw new Exception(string.Format("no react template directory found({0})", reactTemplateDir));
                //if (System.IO.Directory.Exists(reactModuleDir)) throw new Exception(string.Format("react module already exists ({0})", reactModuleDir));
                //if (System.IO.Directory.Exists(reactModuleDir))
                //{
                //    System.IO.Directory.Delete(reactModuleDir, true);
                //}
                ////DirectoryCopy(reactTemplateDir, reactModuleDir, true, true);
                //var copyRet = WinProc("robocopy.exe", string.Format("{0} {1} /E /S /COPY:DATS /SECFIX /TIMFIX", reactTemplateDir, reactModuleDir), true, reactRootDir);
                //if (System.IO.Directory.Exists(reactModuleNodeModuleDir))
                //{
                //    System.IO.Directory.Delete(reactModuleNodeModuleDir, true);
                //}
                //if (!File.Exists(reactModuleDir + ".npmrc"))
                //{
                //    using (var sr = new StreamWriter(reactModuleDir + "/.npmrc",false,System.Text.UTF8Encoding.UTF8))
                //    {
                //        sr.WriteLine(string.Format("prefix={0}/npm", reactRootDir));
                //        sr.WriteLine(string.Format("cache={0}/npm-cache", reactRootDir));
                //        sr.WriteLine(string.Format("update-notifier=false", reactRootDir));
                //        sr.Close();
                //    }
                //}
                string npmPath = @"C:\Program Files\nodejs\npm.cmd";
                if (!System.IO.File.Exists(reactModuleDir + "/.npmrc"))
                {
                    using (var sr = new System.IO.StreamWriter(reactModuleDir + "/.npmrc", false, System.Text.UTF8Encoding.UTF8))
                    {
                        sr.WriteLine(string.Format("prefix={0}npm", reactRootDir.Replace("/", @"\")));
                        sr.WriteLine(string.Format("cache={0}npm-cache", reactRootDir.Replace("/", @"\")));
                        sr.WriteLine(string.Format("update-notifier=false", reactRootDir.Replace("/", @"\")));
                        sr.Flush();
                        sr.Close();
                    }
                }
                //var ret1 = WinProc(npmPath, "cache clean --force", true, reactModuleDir);
                var npmInstallRet = Utils.WinProc(npmPath, @"install  --no-optional --no-update-notifier", true, stdInHandler, stdErrHandler, reactModuleDir);
                var npmRunBuildRet = Utils.WinProc(npmPath, "run build", true, stdInHandler, stdErrHandler, reactModuleDir);
                var buildDir = reactModuleDir + "/build";
                var webSiteTargetDir = webAppRoot + "/React/" + systemAbbr;
                bool isReady = npmRunBuildRet.Item2.Contains("The build folder is ready to be deployed");

                if ((npmRunBuildRet.Item1 != 0 || npmRunBuildRet.Item3.Contains("ERR")))
                {
                    bErrNow.Value = "Y";
                    bInfoNow.Value = "N";
                    PreMsgPopup(npmRunBuildRet.Item3);
                }
                else
                {

                    var webSiteRuntimeJS = string.Format("{0}/runtime/rintagi.js", webSiteTargetDir);
                    var publishRet = Utils.WinProc("robocopy.exe", string.Format("{0} {1} /MIR /XF rintagi.js", buildDir, webSiteTargetDir), true, appRoot);
                    if (publishRet.Item1 >= 8)
                    {
                        // weird robocopy return code for error
                        PreMsgPopup(publishRet.Item3 + "\r\n" + publishRet.Item2);
                    }
                    else
                    {
                        if (!System.IO.File.Exists(webSiteRuntimeJS))
                        {
                            using (var sr = new System.IO.StreamWriter(webSiteRuntimeJS, false, System.Text.UTF8Encoding.UTF8))
                            {
                                sr.WriteLine(rintagiJSContent);
                                sr.Close();
                            }
                        }
                        PreMsgPopup(string.Format("React app for {0} deployed to {1}", systemAbbr, webSiteTargetDir));
                    }
                }
            }
            catch (Exception ex)
            {
                PreMsgPopup(ex.Message);
            }

			// *** WebRule End *** //
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
			cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
			PopAdmSystems87List(sender, e, false, null);
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
			// *** System Button Click (Before) Web Rule starts here *** //
			cSystemId45.Text = string.Empty;
			cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
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
			if (cSystemId45.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmSystems87List(sender, e, false, cSystemId45.Text); }
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
			Session.Remove(KEY_dtAdmSystems87List); PopAdmSystems87List(sender, e, false, null);
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
				AdmSystems87 ds = PrepAdmSystemsData(null,cSystemId45.Text == string.Empty);
				if (string.IsNullOrEmpty(cAdmSystems87List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(87,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmSystems87List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(87,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
						ShowDirty(false); PopAdmSystems87List(sender, e, false, ds.Tables["AdmSystems"].Rows[0]["SystemId45"]);
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
			if (cSystemId45.Text != string.Empty)
			{
				AdmSystems87 ds = PrepAdmSystemsData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(87,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc))
					{
						cAdmSystems87List.ClearSearch(); Session.Remove(KEY_dtAdmSystems87List);
						ShowDirty(false); PopAdmSystems87List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmSystems87 PrepAdmSystemsData(DataView dv, bool bAdd)
		{
			AdmSystems87 ds = new AdmSystems87();
			DataRow dr = ds.Tables["AdmSystems"].NewRow();
			DataRow drType = ds.Tables["AdmSystems"].NewRow();
			DataRow drDisp = ds.Tables["AdmSystems"].NewRow();
			dr["SystemId45"] = cSystemId45.Text;
			drType["SystemId45"] = "Numeric"; drDisp["SystemId45"] = "TextBox";
			try {dr["SystemId45"] = cSystemId45.Text.Trim();} catch {}
			drType["SystemId45"] = "Numeric"; drDisp["SystemId45"] = "TextBox";
			try {dr["ServerName45"] = cServerName45.Text.Trim();} catch {}
			drType["ServerName45"] = "VarChar"; drDisp["ServerName45"] = "TextBox";
			try {dr["SystemName45"] = cSystemName45.Text.Trim();} catch {}
			drType["SystemName45"] = "VarWChar"; drDisp["SystemName45"] = "TextBox";
			try {dr["SystemAbbr45"] = cSystemAbbr45.Text.Trim();} catch {}
			drType["SystemAbbr45"] = "VarChar"; drDisp["SystemAbbr45"] = "TextBox";
			try {dr["SysProgram45"] = base.SetBool(cSysProgram45.Checked);} catch {}
			drType["SysProgram45"] = "Char"; drDisp["SysProgram45"] = "CheckBox";
			try {dr["Active45"] = base.SetBool(cActive45.Checked);} catch {}
			drType["Active45"] = "Char"; drDisp["Active45"] = "CheckBox";
			try {dr["dbAppProvider45"] = cdbAppProvider45.Text.Trim();} catch {}
			drType["dbAppProvider45"] = "VarChar"; drDisp["dbAppProvider45"] = "TextBox";
			try {dr["dbAppServer45"] = cdbAppServer45.Text.Trim();} catch {}
			drType["dbAppServer45"] = "VarChar"; drDisp["dbAppServer45"] = "TextBox";
			try {dr["dbAppDatabase45"] = cdbAppDatabase45.Text.Trim();} catch {}
			drType["dbAppDatabase45"] = "VarChar"; drDisp["dbAppDatabase45"] = "TextBox";
			try {dr["dbDesDatabase45"] = cdbDesDatabase45.Text.Trim();} catch {}
			drType["dbDesDatabase45"] = "VarChar"; drDisp["dbDesDatabase45"] = "TextBox";
			try {dr["dbAppUserId45"] = cdbAppUserId45.Text.Trim();} catch {}
			drType["dbAppUserId45"] = "VarChar"; drDisp["dbAppUserId45"] = "TextBox";
			try {dr["dbAppPassword45"] = cdbAppPassword45.Text.Trim();} catch {}
			drType["dbAppPassword45"] = "VarChar"; drDisp["dbAppPassword45"] = "TextBox";
			try {dr["dbX01Provider45"] = cdbX01Provider45.Text.Trim();} catch {}
			drType["dbX01Provider45"] = "VarChar"; drDisp["dbX01Provider45"] = "TextBox";
			try {dr["dbX01Server45"] = cdbX01Server45.Text.Trim();} catch {}
			drType["dbX01Server45"] = "VarChar"; drDisp["dbX01Server45"] = "TextBox";
			try {dr["dbX01Database45"] = cdbX01Database45.Text.Trim();} catch {}
			drType["dbX01Database45"] = "VarChar"; drDisp["dbX01Database45"] = "TextBox";
			try {dr["dbX01UserId45"] = cdbX01UserId45.Text.Trim();} catch {}
			drType["dbX01UserId45"] = "VarChar"; drDisp["dbX01UserId45"] = "TextBox";
			try {dr["dbX01Password45"] = cdbX01Password45.Text.Trim();} catch {}
			drType["dbX01Password45"] = "VarChar"; drDisp["dbX01Password45"] = "TextBox";
			try {dr["dbX01Extra45"] = cdbX01Extra45.Text.Trim();} catch {}
			drType["dbX01Extra45"] = "VarChar"; drDisp["dbX01Extra45"] = "TextBox";
			try {dr["AdminEmail45"] = cAdminEmail45.Text.Trim();} catch {}
			drType["AdminEmail45"] = "VarWChar"; drDisp["AdminEmail45"] = "TextBox";
			try {dr["AdminPhone45"] = cAdminPhone45.Text.Trim();} catch {}
			drType["AdminPhone45"] = "VarChar"; drDisp["AdminPhone45"] = "TextBox";
			try {dr["CustServEmail45"] = cCustServEmail45.Text.Trim();} catch {}
			drType["CustServEmail45"] = "VarWChar"; drDisp["CustServEmail45"] = "TextBox";
			try {dr["CustServPhone45"] = cCustServPhone45.Text.Trim();} catch {}
			drType["CustServPhone45"] = "VarChar"; drDisp["CustServPhone45"] = "TextBox";
			try {dr["CustServFax45"] = cCustServFax45.Text.Trim();} catch {}
			drType["CustServFax45"] = "VarChar"; drDisp["CustServFax45"] = "TextBox";
			try {dr["WebAddress45"] = cWebAddress45.Text.Trim();} catch {}
			drType["WebAddress45"] = "VarChar"; drDisp["WebAddress45"] = "TextBox";
			if (bAdd)
			{
			}
			ds.Tables["AdmSystems"].Rows.Add(dr); ds.Tables["AdmSystems"].Rows.Add(drType); ds.Tables["AdmSystems"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmSystems87List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmSystems87List,null); }

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

