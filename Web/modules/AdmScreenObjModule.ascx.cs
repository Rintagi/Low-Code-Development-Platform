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
	public class AdmScreenObj10 : DataSet
	{
		public AdmScreenObj10()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmScreenObj")));
			this.DataSetName = "AdmScreenObj10";
			this.Namespace = "http://Rintagi.com/DataSet/AdmScreenObj10";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ScreenObjId14", typeof(string));
			columns.Add("MasterTable14", typeof(string));
			columns.Add("RequiredValid14", typeof(string));
			columns.Add("ColumnWrap14", typeof(string));
			columns.Add("GridGrpCd14", typeof(string));
			columns.Add("HideOnTablet14", typeof(string));
			columns.Add("HideOnMobile14", typeof(string));
			columns.Add("RefreshOnCUD14", typeof(string));
			columns.Add("TrimOnEntry14", typeof(string));
			columns.Add("IgnoreConfirm14", typeof(string));
			columns.Add("ColumnJustify14", typeof(string));
			columns.Add("ColumnSize14", typeof(string));
			columns.Add("ColumnHeight14", typeof(string));
			columns.Add("ResizeWidth14", typeof(string));
			columns.Add("ResizeHeight14", typeof(string));
			columns.Add("SortOrder14", typeof(string));
			columns.Add("ScreenId14", typeof(string));
			columns.Add("GroupRowId14", typeof(string));
			columns.Add("GroupColId14", typeof(string));
			columns.Add("ColumnId14", typeof(string));
			columns.Add("ColumnName14", typeof(string));
			columns.Add("DisplayModeId14", typeof(string));
			columns.Add("DdlKeyColumnId14", typeof(string));
			columns.Add("DdlRefColumnId14", typeof(string));
			columns.Add("DdlSrtColumnId14", typeof(string));
			columns.Add("DdlAdnColumnId14", typeof(string));
			columns.Add("DdlFtrColumnId14", typeof(string));
			columns.Add("ColumnLink14", typeof(string));
			columns.Add("DtlLstPosId14", typeof(string));
			columns.Add("LabelVertical14", typeof(string));
			columns.Add("LabelCss14", typeof(string));
			columns.Add("ContentCss14", typeof(string));
			columns.Add("DefaultValue14", typeof(string));
			columns.Add("HyperLinkUrl14", typeof(string));
			columns.Add("DefAfter14", typeof(string));
			columns.Add("SystemValue14", typeof(string));
			columns.Add("DefAlways14", typeof(string));
			columns.Add("AggregateCd14", typeof(string));
			columns.Add("GenerateSp14", typeof(string));
			columns.Add("MaskValid14", typeof(string));
			columns.Add("RangeValidType14", typeof(string));
			columns.Add("RangeValidMax14", typeof(string));
			columns.Add("RangeValidMin14", typeof(string));
			columns.Add("MatchCd14", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmScreenObjModule : RO.Web.ModuleBase
	{
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_10";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_10";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_10";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_10";
		private const string KEY_dtLabel = "Cache:dtLabel3_10";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_10";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_10";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_10";

		private const string KEY_dtGridGrpCd14 = "Cache:dtGridGrpCd14";
		private const string KEY_dtColumnJustify14 = "Cache:dtColumnJustify14";
		private const string KEY_dtScreenId14 = "Cache:dtScreenId14";
		private const string KEY_dtGroupRowId14 = "Cache:dtGroupRowId14";
		private const string KEY_dtGroupColId14 = "Cache:dtGroupColId14";
		private const string KEY_dtColumnId14 = "Cache:dtColumnId14";
		private const string KEY_dtDisplayModeId14 = "Cache:dtDisplayModeId14";
		private const string KEY_dtDdlKeyColumnId14 = "Cache:dtDdlKeyColumnId14";
		private const string KEY_dtDdlRefColumnId14 = "Cache:dtDdlRefColumnId14";
		private const string KEY_dtDdlSrtColumnId14 = "Cache:dtDdlSrtColumnId14";
		private const string KEY_dtDdlAdnColumnId14 = "Cache:dtDdlAdnColumnId14";
		private const string KEY_dtDdlFtrColumnId14 = "Cache:dtDdlFtrColumnId14";
		private const string KEY_dtDtlLstPosId14 = "Cache:dtDtlLstPosId14";
		private const string KEY_dtAggregateCd14 = "Cache:dtAggregateCd14";
		private const string KEY_dtMatchCd14 = "Cache:dtMatchCd14";

		private const string KEY_dtSystems = "Cache:dtSystems3_10";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_10";
		private const string KEY_dtAdmScreenObj10List = "Cache:dtAdmScreenObj10List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_10";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_10";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_10";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_10";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_10";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_10";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_10";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_10";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmScreenObjModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,10, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				Session.Remove(KEY_dtAdmScreenObj10List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtGridGrpCd14);
				Session.Remove(KEY_dtColumnJustify14);
				Session.Remove(KEY_dtScreenId14);
				Session.Remove(KEY_dtGroupRowId14);
				Session.Remove(KEY_dtGroupColId14);
				Session.Remove(KEY_dtColumnId14);
				Session.Remove(KEY_dtDisplayModeId14);
				Session.Remove(KEY_dtDdlKeyColumnId14);
				Session.Remove(KEY_dtDdlRefColumnId14);
				Session.Remove(KEY_dtDdlSrtColumnId14);
				Session.Remove(KEY_dtDdlAdnColumnId14);
				Session.Remove(KEY_dtDdlFtrColumnId14);
				Session.Remove(KEY_dtDtlLstPosId14);
				Session.Remove(KEY_dtAggregateCd14);
				Session.Remove(KEY_dtMatchCd14);
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
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 10, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmScreenObj10List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=10&typ=N&sys=3','','',''); return false;"; }
				//WebRule: Confirm Document and WorkflowStatus Type plus show/hide fields
                cDisplayModeId14.Attributes["OnChange"] = "if (this.value!='32' && this.value!='53')  {document.getElementById('" + bConfirm.ClientID + "').value='N';} else {document.getElementById('" + bConfirm.ClientID + "').value='Y';};" + cDisplayModeId14.Attributes["OnChange"];
                if (Request.QueryString["mod"] != null && Request.QueryString["mod"].ToLower() == "e")
                {
                    cGroupRowId14P1.Visible = false; cGroupRowId14P2.Visible = false;
                    cGroupColId14P1.Visible = false; cGroupColId14P2.Visible = false;
                }
                else
                {
                    cGroupRowId14P1.Visible = true; cGroupRowId14P2.Visible = true;
                    cGroupColId14P1.Visible = true; cGroupColId14P2.Visible = true;
                }
				// *** WebRule End *** //
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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,10,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(10, "Screen Object", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(10,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtRul = (new AdminSystem()).GetClientRule(10,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(10,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(10,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,10,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(10,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
			DataTable dtSystems = base.SystemsList;
			if (dtSystems != null)
			{
				Session[KEY_dtSystems] = dtSystems;
				cSystemId.DataSource = dtSystems;
				cSystemId.DataBind();
				if (cSystemId.Items.Count > 1)
				{
					if (Request.QueryString["sys"] != null) {cSystemId.Items.FindByValue(Request.QueryString["sys"]).Selected = true;}
					else
					{
						try { cSystemId.Items.FindByValue(base.LCurr.DbId.ToString()).Selected = true; }
						catch {cSystemId.Items[0].Selected = true;}
					}
					base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
					Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
					cSystemLabel.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "Label", null, null, null);
					cSystem.Visible = true;
				}
			}
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
					dt = (new AdminSystem()).GetScreenLabel(10,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(10,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(10,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(10,"GetExpAdmScreenObj10","Y",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(10,"GetExpAdmScreenObj10","N",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(10,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[4]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[10]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[16]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[16]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[17]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[17]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[18]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[19]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[21]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[22]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[23]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[23]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[24]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[24]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[25]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[25]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[26]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[26]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[27]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[27]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[28]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[29]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[29]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[30]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[31]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[32]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[33]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[34]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[35]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[36]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[37]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[38]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[38]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[39]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[40]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[41]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[42]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[43]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[44]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[44]["ColumnHeader"].ToString() + " Text" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenObjId14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterTable14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RequiredValid14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ColumnWrap14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GridGrpCd14"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["GridGrpCd14Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["HideOnTablet14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["HideOnMobile14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RefreshOnCUD14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["TrimOnEntry14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["IgnoreConfirm14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ColumnJustify14"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["ColumnJustify14Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ColumnSize14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ColumnHeight14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ResizeWidth14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ResizeHeight14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["SortOrder14"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenId14"].ToString() + (char)9 + drv["ScreenId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupRowId14"].ToString() + (char)9 + drv["GroupRowId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupColId14"].ToString() + (char)9 + drv["GroupColId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnId14"].ToString() + (char)9 + drv["ColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ColumnName14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["DisplayModeId14"].ToString() + (char)9 + drv["DisplayModeId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DisplayDesc18"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlKeyColumnId14"].ToString() + (char)9 + drv["DdlKeyColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlRefColumnId14"].ToString() + (char)9 + drv["DdlRefColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlSrtColumnId14"].ToString() + (char)9 + drv["DdlSrtColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlAdnColumnId14"].ToString() + (char)9 + drv["DdlAdnColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlFtrColumnId14"].ToString() + (char)9 + drv["DdlFtrColumnId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ColumnLink14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["DtlLstPosId14"].ToString() + (char)9 + drv["DtlLstPosId14Text"].ToString() + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["LabelVertical14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["LabelCss14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ContentCss14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DefaultValue14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["HyperLinkUrl14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DefAfter14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SystemValue14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DefAlways14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AggregateCd14"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["AggregateCd14Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GenerateSp14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MaskValid14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RangeValidType14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RangeValidMax14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RangeValidMin14"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MatchCd14"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["MatchCd14Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmScreenObj.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmScreenObj.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(10,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
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
					if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[38]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[39]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[40]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[41]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[42]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[43]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[44]["ColumnHeader"].ToString() + @"\cell ");}
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenObjId14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterTable14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["RequiredValid14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnWrap14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["GridGrpCd14Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["HideOnTablet14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["HideOnMobile14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["RefreshOnCUD14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["TrimOnEntry14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["IgnoreConfirm14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnJustify14Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ColumnSize14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ColumnHeight14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ResizeWidth14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ResizeHeight14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["SortOrder14"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupRowId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupColId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnName14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["DisplayModeId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(drv["DisplayDesc18"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlKeyColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlRefColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlSrtColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlAdnColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["DdlFtrColumnId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(drv["ColumnLink14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["DtlLstPosId14Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(drv["LabelVertical14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(drv["LabelCss14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["ContentCss14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(drv["DefaultValue14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(drv["HyperLinkUrl14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(drv["DefAfter14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(drv["SystemValue14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(drv["DefAlways14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append(drv["AggregateCd14Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append(drv["GenerateSp14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append(drv["MaskValid14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append(drv["RangeValidType14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append(drv["RangeValidMax14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append(drv["RangeValidMin14"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append(drv["MatchCd14Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 10, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
			PopAdmScreenObj10List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("10", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("10", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("10", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("10", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("10", LcSysConnString, LcAppPw);
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
		            context["scr"] = "10";
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
		            context["scr"] = "10";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmScreenObj10" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmScreenObj10" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("10", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("10", "AdmScreenObj10", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			Session[KEY_dsScrCriVal] = ds;
			return ds;
		}

		private void SetGridGrpCd14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtGridGrpCd14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlGridGrpCd3S2016",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "GridGrpCd14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlGridGrpCd3S2016",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlGridGrpCd3S2016",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtGridGrpCd14] = dv.Table;
				}
			}
		}

		private void SetColumnJustify14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtColumnJustify14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlColumnJustify3S1422",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ColumnJustify14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlColumnJustify3S1422",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlColumnJustify3S1422",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtColumnJustify14] = dv.Table;
				}
			}
		}

		protected void cbScreenId14(object sender, System.EventArgs e)
		{
			SetScreenId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetScreenId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlScreenId3S1268";
			context["addnew"] = "Y";
			context["mKey"] = "ScreenId14";
			context["mVal"] = "ScreenId14Text";
			context["mTip"] = "ScreenId14Text";
			context["mImg"] = "ScreenId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlScreenId3S1268",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtScreenId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbGroupRowId14(object sender, System.EventArgs e)
		{
			SetGroupRowId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetGroupRowId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlGroupRowId3S3136";
			context["addnew"] = "Y";
			context["mKey"] = "GroupRowId14";
			context["mVal"] = "GroupRowId14Text";
			context["mTip"] = "GroupRowId14Text";
			context["mImg"] = "GroupRowId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlGroupRowId3S3136",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtGroupRowId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbGroupColId14(object sender, System.EventArgs e)
		{
			SetGroupColId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetGroupColId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlGroupColId3S1204";
			context["addnew"] = "Y";
			context["mKey"] = "GroupColId14";
			context["mVal"] = "GroupColId14Text";
			context["mTip"] = "GroupColId14Text";
			context["mImg"] = "GroupColId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlGroupColId3S1204",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtGroupColId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbColumnId14(object sender, System.EventArgs e)
		{
			SetColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlColumnId3S74";
			context["addnew"] = "Y";
			context["mKey"] = "ColumnId14";
			context["mVal"] = "ColumnId14Text";
			context["mTip"] = "ColumnId14Text";
			context["mImg"] = "ColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlColumnId3S74",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void SetDisplayModeId14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtDisplayModeId14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDisplayModeId3S81",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "DisplayModeId14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDisplayModeId3S81",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDisplayModeId3S81",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtDisplayModeId14] = dv.Table;
				}
			}
		}

		protected void cbDdlKeyColumnId14(object sender, System.EventArgs e)
		{
			SetDdlKeyColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDdlKeyColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDdlKeyColumnId3S82";
			context["addnew"] = "Y";
			context["mKey"] = "DdlKeyColumnId14";
			context["mVal"] = "DdlKeyColumnId14Text";
			context["mTip"] = "DdlKeyColumnId14Text";
			context["mImg"] = "DdlKeyColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDdlKeyColumnId3S82",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDdlKeyColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbDdlRefColumnId14(object sender, System.EventArgs e)
		{
			SetDdlRefColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDdlRefColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDdlRefColumnId3S83";
			context["addnew"] = "Y";
			context["mKey"] = "DdlRefColumnId14";
			context["mVal"] = "DdlRefColumnId14Text";
			context["mTip"] = "DdlRefColumnId14Text";
			context["mImg"] = "DdlRefColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDdlRefColumnId3S83",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDdlRefColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbDdlSrtColumnId14(object sender, System.EventArgs e)
		{
			SetDdlSrtColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDdlSrtColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDdlSrtColumnId3S1269";
			context["addnew"] = "Y";
			context["mKey"] = "DdlSrtColumnId14";
			context["mVal"] = "DdlSrtColumnId14Text";
			context["mTip"] = "DdlSrtColumnId14Text";
			context["mImg"] = "DdlSrtColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDdlSrtColumnId3S1269",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDdlSrtColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbDdlAdnColumnId14(object sender, System.EventArgs e)
		{
			SetDdlAdnColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDdlAdnColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDdlAdnColumnId3S1281";
			context["addnew"] = "Y";
			context["mKey"] = "DdlAdnColumnId14";
			context["mVal"] = "DdlAdnColumnId14Text";
			context["mTip"] = "DdlAdnColumnId14Text";
			context["mImg"] = "DdlAdnColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDdlAdnColumnId3S1281",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDdlAdnColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbDdlFtrColumnId14(object sender, System.EventArgs e)
		{
			SetDdlFtrColumnId14((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDdlFtrColumnId14(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDdlFtrColumnId3S1282";
			context["addnew"] = "Y";
			context["mKey"] = "DdlFtrColumnId14";
			context["mVal"] = "DdlFtrColumnId14Text";
			context["mTip"] = "DdlFtrColumnId14Text";
			context["mImg"] = "DdlFtrColumnId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDdlFtrColumnId3S1282",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						context["pMKeyColID"] = cAdmScreenObj10List.ClientID;
						context["pMKeyCol"] = "ScreenObjId";
						string ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDdlFtrColumnId14] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void SetDtlLstPosId14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtDtlLstPosId14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDtlLstPosId3S4206",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "DtlLstPosId14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDtlLstPosId3S4206",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlDtlLstPosId3S4206",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtDtlLstPosId14] = dv.Table;
				}
			}
		}

		private void SetAggregateCd14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtAggregateCd14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlAggregateCd3S1238",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "AggregateCd14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlAggregateCd3S1238",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlAggregateCd3S1238",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtAggregateCd14] = dv.Table;
				}
			}
		}

		private void SetMatchCd14(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtMatchCd14];
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
						dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlMatchCd3S1806",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenObjId"))
					{
						ss = "(ScreenObjId is null";
						if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenObjId = " + cAdmScreenObj10List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "MatchCd14 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlMatchCd3S1806",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(10,"GetDdlMatchCd3S1806",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtMatchCd14] = dv.Table;
				}
			}
		}

		private DataView GetAdmScreenObj10List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenObj10List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(10,"GetLisAdmScreenObj10",true,"Y",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(10,"GetLisAdmScreenObj10",true,"N",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmScreenObj10List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmScreenObj10List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmScreenObj10";
			context["mKey"] = "ScreenObjId14";
			context["mVal"] = "ScreenObjId14Text";
			context["mTip"] = "ScreenObjId14Text";
			context["mImg"] = "ScreenObjId14Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "10";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			cAdmScreenObj10List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmScreenObj10List.DataContext = context;
			if (dv.Table == null) return;
			cAdmScreenObj10List.DataSource = dv;
			cAdmScreenObj10List.Visible = true;
			if (cAdmScreenObj10List.Items.Count <= 0) {cAdmScreenObj10List.Visible = false; cAdmScreenObj10List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmScreenObj10List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmScreenObj10List.Items[0].Selected = true; cAdmScreenObj10List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmScreenObj10List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmScreenObj10List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cScreenObjId14, dtAuth.Rows[0], cScreenObjId14P1, cScreenObjId14Label, cScreenObjId14P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cMasterTable14, dtAuth.Rows[1], cMasterTable14P1, cMasterTable14Label, cMasterTable14P2, null, dtLabel.Rows[1], null, null, null);
				base.SetFoldBehavior(cRequiredValid14, dtAuth.Rows[2], cRequiredValid14P1, cRequiredValid14Label, cRequiredValid14P2, null, dtLabel.Rows[2], null, null, null);
				base.SetFoldBehavior(cColumnWrap14, dtAuth.Rows[3], cColumnWrap14P1, cColumnWrap14Label, cColumnWrap14P2, null, dtLabel.Rows[3], null, null, null);
				base.SetFoldBehavior(cGridGrpCd14, dtAuth.Rows[4], cGridGrpCd14P1, cGridGrpCd14Label, cGridGrpCd14P2, null, dtLabel.Rows[4], cRFVGridGrpCd14, null, null);
				base.SetFoldBehavior(cHideOnTablet14, dtAuth.Rows[5], cHideOnTablet14P1, cHideOnTablet14Label, cHideOnTablet14P2, null, dtLabel.Rows[5], null, null, null);
				base.SetFoldBehavior(cHideOnMobile14, dtAuth.Rows[6], cHideOnMobile14P1, cHideOnMobile14Label, cHideOnMobile14P2, null, dtLabel.Rows[6], null, null, null);
				base.SetFoldBehavior(cRefreshOnCUD14, dtAuth.Rows[7], cRefreshOnCUD14P1, cRefreshOnCUD14Label, cRefreshOnCUD14P2, null, dtLabel.Rows[7], null, null, null);
				base.SetFoldBehavior(cTrimOnEntry14, dtAuth.Rows[8], cTrimOnEntry14P1, cTrimOnEntry14Label, cTrimOnEntry14P2, null, dtLabel.Rows[8], null, null, null);
				base.SetFoldBehavior(cIgnoreConfirm14, dtAuth.Rows[9], cIgnoreConfirm14P1, cIgnoreConfirm14Label, cIgnoreConfirm14P2, null, dtLabel.Rows[9], null, null, null);
				base.SetFoldBehavior(cColumnJustify14, dtAuth.Rows[10], cColumnJustify14P1, cColumnJustify14Label, cColumnJustify14P2, null, dtLabel.Rows[10], cRFVColumnJustify14, null, null);
				base.SetFoldBehavior(cColumnSize14, dtAuth.Rows[11], cColumnSize14P1, cColumnSize14Label, cColumnSize14P2, null, dtLabel.Rows[11], null, null, null);
				base.SetFoldBehavior(cColumnHeight14, dtAuth.Rows[12], cColumnHeight14P1, cColumnHeight14Label, cColumnHeight14P2, null, dtLabel.Rows[12], null, null, null);
				base.SetFoldBehavior(cResizeWidth14, dtAuth.Rows[13], cResizeWidth14P1, cResizeWidth14Label, cResizeWidth14P2, null, dtLabel.Rows[13], null, null, null);
				base.SetFoldBehavior(cResizeHeight14, dtAuth.Rows[14], cResizeHeight14P1, cResizeHeight14Label, cResizeHeight14P2, null, dtLabel.Rows[14], null, null, null);
				base.SetFoldBehavior(cSortOrder14, dtAuth.Rows[15], cSortOrder14P1, cSortOrder14Label, cSortOrder14P2, null, dtLabel.Rows[15], null, cREVSortOrder14, null);
				base.SetFoldBehavior(cScreenId14, dtAuth.Rows[16], cScreenId14P1, cScreenId14Label, cScreenId14P2, null, dtLabel.Rows[16], cRFVScreenId14, null, null);
				SetScreenId14(cScreenId14,string.Empty);
				base.SetFoldBehavior(cGroupRowId14, dtAuth.Rows[17], cGroupRowId14P1, cGroupRowId14Label, cGroupRowId14P2, null, dtLabel.Rows[17], cRFVGroupRowId14, null, null);
				SetGroupRowId14(cGroupRowId14,string.Empty);
				base.SetFoldBehavior(cGroupColId14, dtAuth.Rows[18], cGroupColId14P1, cGroupColId14Label, cGroupColId14P2, null, dtLabel.Rows[18], cRFVGroupColId14, null, null);
				SetGroupColId14(cGroupColId14,string.Empty);
				base.SetFoldBehavior(cColumnId14, dtAuth.Rows[19], cColumnId14P1, cColumnId14Label, cColumnId14P2, null, dtLabel.Rows[19], null, null, null);
				SetColumnId14(cColumnId14,string.Empty);
				base.SetFoldBehavior(cColumnName14, dtAuth.Rows[20], cColumnName14P1, cColumnName14Label, cColumnName14P2, null, dtLabel.Rows[20], cRFVColumnName14, null, null);
				base.SetFoldBehavior(cDisplayModeId14, dtAuth.Rows[21], cDisplayModeId14P1, cDisplayModeId14Label, cDisplayModeId14P2, null, dtLabel.Rows[21], cRFVDisplayModeId14, null, null);
				base.SetFoldBehavior(cDisplayDesc18, dtAuth.Rows[22], cDisplayDesc18P1, cDisplayDesc18Label, cDisplayDesc18P2, cDisplayDesc18E, null, dtLabel.Rows[22], null, null, null);
				cDisplayDesc18E.Attributes["label_id"] = cDisplayDesc18Label.ClientID; cDisplayDesc18E.Attributes["target_id"] = cDisplayDesc18.ClientID;
				base.SetFoldBehavior(cDdlKeyColumnId14, dtAuth.Rows[23], cDdlKeyColumnId14P1, cDdlKeyColumnId14Label, cDdlKeyColumnId14P2, null, dtLabel.Rows[23], null, null, null);
				SetDdlKeyColumnId14(cDdlKeyColumnId14,string.Empty);
				base.SetFoldBehavior(cDdlRefColumnId14, dtAuth.Rows[24], cDdlRefColumnId14P1, cDdlRefColumnId14Label, cDdlRefColumnId14P2, null, dtLabel.Rows[24], null, null, null);
				SetDdlRefColumnId14(cDdlRefColumnId14,string.Empty);
				base.SetFoldBehavior(cDdlSrtColumnId14, dtAuth.Rows[25], cDdlSrtColumnId14P1, cDdlSrtColumnId14Label, cDdlSrtColumnId14P2, null, dtLabel.Rows[25], null, null, null);
				SetDdlSrtColumnId14(cDdlSrtColumnId14,string.Empty);
				base.SetFoldBehavior(cDdlAdnColumnId14, dtAuth.Rows[26], cDdlAdnColumnId14P1, cDdlAdnColumnId14Label, cDdlAdnColumnId14P2, null, dtLabel.Rows[26], null, null, null);
				SetDdlAdnColumnId14(cDdlAdnColumnId14,string.Empty);
				base.SetFoldBehavior(cDdlFtrColumnId14, dtAuth.Rows[27], cDdlFtrColumnId14P1, cDdlFtrColumnId14Label, cDdlFtrColumnId14P2, null, dtLabel.Rows[27], null, null, null);
				SetDdlFtrColumnId14(cDdlFtrColumnId14,string.Empty);
				base.SetFoldBehavior(cColumnLink14, dtAuth.Rows[28], cColumnLink14P1, cColumnLink14Label, cColumnLink14P2, null, dtLabel.Rows[28], null, null, null);
				base.SetFoldBehavior(cDtlLstPosId14, dtAuth.Rows[29], cDtlLstPosId14P1, cDtlLstPosId14Label, cDtlLstPosId14P2, null, dtLabel.Rows[29], null, null, null);
				base.SetFoldBehavior(cLabelVertical14, dtAuth.Rows[30], cLabelVertical14P1, cLabelVertical14Label, cLabelVertical14P2, null, dtLabel.Rows[30], null, null, null);
				base.SetFoldBehavior(cLabelCss14, dtAuth.Rows[31], cLabelCss14P1, cLabelCss14Label, cLabelCss14P2, null, dtLabel.Rows[31], null, null, null);
				base.SetFoldBehavior(cContentCss14, dtAuth.Rows[32], cContentCss14P1, cContentCss14Label, cContentCss14P2, null, dtLabel.Rows[32], null, null, null);
				base.SetFoldBehavior(cDefaultValue14, dtAuth.Rows[33], cDefaultValue14P1, cDefaultValue14Label, cDefaultValue14P2, null, dtLabel.Rows[33], null, null, null);
				base.SetFoldBehavior(cHyperLinkUrl14, dtAuth.Rows[34], cHyperLinkUrl14P1, cHyperLinkUrl14Label, cHyperLinkUrl14P2, null, dtLabel.Rows[34], null, null, null);
				base.SetFoldBehavior(cDefAfter14, dtAuth.Rows[35], cDefAfter14P1, cDefAfter14Label, cDefAfter14P2, null, dtLabel.Rows[35], null, null, null);
				base.SetFoldBehavior(cSystemValue14, dtAuth.Rows[36], cSystemValue14P1, cSystemValue14Label, cSystemValue14P2, null, dtLabel.Rows[36], null, null, null);
				base.SetFoldBehavior(cDefAlways14, dtAuth.Rows[37], cDefAlways14P1, cDefAlways14Label, cDefAlways14P2, null, dtLabel.Rows[37], null, null, null);
				base.SetFoldBehavior(cAggregateCd14, dtAuth.Rows[38], cAggregateCd14P1, cAggregateCd14Label, cAggregateCd14P2, null, dtLabel.Rows[38], null, null, null);
				base.SetFoldBehavior(cGenerateSp14, dtAuth.Rows[39], cGenerateSp14P1, cGenerateSp14Label, cGenerateSp14P2, null, dtLabel.Rows[39], null, null, null);
				base.SetFoldBehavior(cMaskValid14, dtAuth.Rows[40], cMaskValid14P1, cMaskValid14Label, cMaskValid14P2, null, dtLabel.Rows[40], null, null, null);
				base.SetFoldBehavior(cRangeValidType14, dtAuth.Rows[41], cRangeValidType14P1, cRangeValidType14Label, cRangeValidType14P2, null, dtLabel.Rows[41], null, null, null);
				base.SetFoldBehavior(cRangeValidMax14, dtAuth.Rows[42], cRangeValidMax14P1, cRangeValidMax14Label, cRangeValidMax14P2, null, dtLabel.Rows[42], null, null, null);
				base.SetFoldBehavior(cRangeValidMin14, dtAuth.Rows[43], cRangeValidMin14P1, cRangeValidMin14Label, cRangeValidMin14P2, null, dtLabel.Rows[43], null, null, null);
				base.SetFoldBehavior(cMatchCd14, dtAuth.Rows[44], cMatchCd14P1, cMatchCd14Label, cMatchCd14P2, null, dtLabel.Rows[44], null, null, null);
			}
			if ((cMasterTable14.Attributes["OnClick"] == null || cMasterTable14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cMasterTable14.Visible && cMasterTable14.Enabled) {cMasterTable14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N'; this.focus();";}
			if ((cRequiredValid14.Attributes["OnClick"] == null || cRequiredValid14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRequiredValid14.Visible && cRequiredValid14.Enabled) {cRequiredValid14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cColumnWrap14.Attributes["OnClick"] == null || cColumnWrap14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cColumnWrap14.Visible && cColumnWrap14.Enabled) {cColumnWrap14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cGridGrpCd14.Attributes["OnChange"] == null || cGridGrpCd14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGridGrpCd14.Visible && cGridGrpCd14.Enabled) {cGridGrpCd14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cHideOnTablet14.Attributes["OnClick"] == null || cHideOnTablet14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cHideOnTablet14.Visible && cHideOnTablet14.Enabled) {cHideOnTablet14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cHideOnMobile14.Attributes["OnClick"] == null || cHideOnMobile14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cHideOnMobile14.Visible && cHideOnMobile14.Enabled) {cHideOnMobile14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cRefreshOnCUD14.Attributes["OnClick"] == null || cRefreshOnCUD14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRefreshOnCUD14.Visible && cRefreshOnCUD14.Enabled) {cRefreshOnCUD14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cTrimOnEntry14.Attributes["OnClick"] == null || cTrimOnEntry14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cTrimOnEntry14.Visible && cTrimOnEntry14.Enabled) {cTrimOnEntry14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cIgnoreConfirm14.Attributes["OnClick"] == null || cIgnoreConfirm14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cIgnoreConfirm14.Visible && cIgnoreConfirm14.Enabled) {cIgnoreConfirm14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cColumnJustify14.Attributes["OnChange"] == null || cColumnJustify14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnJustify14.Visible && cColumnJustify14.Enabled) {cColumnJustify14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cColumnSize14.Attributes["OnChange"] == null || cColumnSize14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnSize14.Visible && !cColumnSize14.ReadOnly) {cColumnSize14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cColumnHeight14.Attributes["OnChange"] == null || cColumnHeight14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnHeight14.Visible && !cColumnHeight14.ReadOnly) {cColumnHeight14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cResizeWidth14.Attributes["OnChange"] == null || cResizeWidth14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cResizeWidth14.Visible && !cResizeWidth14.ReadOnly) {cResizeWidth14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cResizeHeight14.Attributes["OnChange"] == null || cResizeHeight14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cResizeHeight14.Visible && !cResizeHeight14.ReadOnly) {cResizeHeight14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSortOrder14.Attributes["OnChange"] == null || cSortOrder14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSortOrder14.Visible && !cSortOrder14.ReadOnly) {cSortOrder14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cScreenId14.Attributes["OnChange"] == null || cScreenId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cScreenId14.Visible && cScreenId14.Enabled) {cScreenId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGroupRowId14.Attributes["OnChange"] == null || cGroupRowId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGroupRowId14.Visible && cGroupRowId14.Enabled) {cGroupRowId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGroupColId14.Attributes["OnChange"] == null || cGroupColId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGroupColId14.Visible && cGroupColId14.Enabled) {cGroupColId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cColumnId14.Attributes["OnChange"] == null || cColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnId14.Visible && cColumnId14.Enabled) {cColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cColumnName14.Attributes["OnChange"] == null || cColumnName14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnName14.Visible && !cColumnName14.ReadOnly) {cColumnName14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDisplayModeId14.Attributes["OnChange"] == null || cDisplayModeId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDisplayModeId14.Visible && cDisplayModeId14.Enabled) {cDisplayModeId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();return CanPostBack(true,this);";} cDisplayModeId14.Attributes["NeedConfirm"] = "Y";
			if ((cDisplayDesc18.Attributes["OnChange"] == null || cDisplayDesc18.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDisplayDesc18.Visible && !cDisplayDesc18.ReadOnly) {cDisplayDesc18.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";} cDisplayDesc18.Attributes["NeedConfirm"] = "Y";
			if ((cDdlKeyColumnId14.Attributes["OnChange"] == null || cDdlKeyColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDdlKeyColumnId14.Visible && cDdlKeyColumnId14.Enabled) {cDdlKeyColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDdlRefColumnId14.Attributes["OnChange"] == null || cDdlRefColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDdlRefColumnId14.Visible && cDdlRefColumnId14.Enabled) {cDdlRefColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDdlSrtColumnId14.Attributes["OnChange"] == null || cDdlSrtColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDdlSrtColumnId14.Visible && cDdlSrtColumnId14.Enabled) {cDdlSrtColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDdlAdnColumnId14.Attributes["OnChange"] == null || cDdlAdnColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDdlAdnColumnId14.Visible && cDdlAdnColumnId14.Enabled) {cDdlAdnColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDdlFtrColumnId14.Attributes["OnChange"] == null || cDdlFtrColumnId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDdlFtrColumnId14.Visible && cDdlFtrColumnId14.Enabled) {cDdlFtrColumnId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cColumnLink14.Attributes["OnChange"] == null || cColumnLink14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cColumnLink14.Visible && !cColumnLink14.ReadOnly) {cColumnLink14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDtlLstPosId14.Attributes["OnChange"] == null || cDtlLstPosId14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDtlLstPosId14.Visible && cDtlLstPosId14.Enabled) {cDtlLstPosId14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLabelVertical14.Attributes["OnClick"] == null || cLabelVertical14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cLabelVertical14.Visible && cLabelVertical14.Enabled) {cLabelVertical14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cLabelCss14.Attributes["OnChange"] == null || cLabelCss14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLabelCss14.Visible && !cLabelCss14.ReadOnly) {cLabelCss14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cContentCss14.Attributes["OnChange"] == null || cContentCss14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cContentCss14.Visible && !cContentCss14.ReadOnly) {cContentCss14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDefaultValue14.Attributes["OnChange"] == null || cDefaultValue14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDefaultValue14.Visible && !cDefaultValue14.ReadOnly) {cDefaultValue14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cHyperLinkUrl14.Attributes["OnChange"] == null || cHyperLinkUrl14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cHyperLinkUrl14.Visible && !cHyperLinkUrl14.ReadOnly) {cHyperLinkUrl14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDefAfter14.Attributes["OnClick"] == null || cDefAfter14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDefAfter14.Visible && cDefAfter14.Enabled) {cDefAfter14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cSystemValue14.Attributes["OnChange"] == null || cSystemValue14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSystemValue14.Visible && !cSystemValue14.ReadOnly) {cSystemValue14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDefAlways14.Attributes["OnClick"] == null || cDefAlways14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDefAlways14.Visible && cDefAlways14.Enabled) {cDefAlways14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cAggregateCd14.Attributes["OnChange"] == null || cAggregateCd14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cAggregateCd14.Visible && cAggregateCd14.Enabled) {cAggregateCd14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGenerateSp14.Attributes["OnClick"] == null || cGenerateSp14.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cGenerateSp14.Visible && cGenerateSp14.Enabled) {cGenerateSp14.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cMaskValid14.Attributes["OnChange"] == null || cMaskValid14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMaskValid14.Visible && !cMaskValid14.ReadOnly) {cMaskValid14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRangeValidType14.Attributes["OnChange"] == null || cRangeValidType14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRangeValidType14.Visible && !cRangeValidType14.ReadOnly) {cRangeValidType14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRangeValidMax14.Attributes["OnChange"] == null || cRangeValidMax14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRangeValidMax14.Visible && !cRangeValidMax14.ReadOnly) {cRangeValidMax14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRangeValidMin14.Attributes["OnChange"] == null || cRangeValidMin14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRangeValidMin14.Visible && !cRangeValidMin14.ReadOnly) {cRangeValidMin14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cMatchCd14.Attributes["OnChange"] == null || cMatchCd14.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMatchCd14.Visible && cMatchCd14.Enabled) {cMatchCd14.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
				Session.Remove(KEY_dtGridGrpCd14);
				Session.Remove(KEY_dtColumnJustify14);
				Session.Remove(KEY_dtScreenId14);
				Session.Remove(KEY_dtGroupRowId14);
				Session.Remove(KEY_dtGroupColId14);
				Session.Remove(KEY_dtColumnId14);
				Session.Remove(KEY_dtDisplayModeId14);
				Session.Remove(KEY_dtDdlKeyColumnId14);
				Session.Remove(KEY_dtDdlRefColumnId14);
				Session.Remove(KEY_dtDdlSrtColumnId14);
				Session.Remove(KEY_dtDdlAdnColumnId14);
				Session.Remove(KEY_dtDdlFtrColumnId14);
				Session.Remove(KEY_dtDtlLstPosId14);
				Session.Remove(KEY_dtAggregateCd14);
				Session.Remove(KEY_dtMatchCd14);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cMasterTable14.Checked = base.GetBool("N");}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {cRequiredValid14.Checked = base.GetBool("N");}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {cColumnWrap14.Checked = base.GetBool("Y");}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {SetGridGrpCd14(cGridGrpCd14,"N");}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {cHideOnTablet14.Checked = base.GetBool("N");}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {cHideOnMobile14.Checked = base.GetBool("N");}
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cRefreshOnCUD14.Checked = base.GetBool("N");}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cTrimOnEntry14.Checked = base.GetBool("Y");}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {cIgnoreConfirm14.Checked = base.GetBool("Y");}
			if (dt.Rows[10]["ColVisible"].ToString() == "Y" && dt.Rows[10]["ColReadOnly"].ToString() != "Y") {SetColumnJustify14(cColumnJustify14,"L");}
			if (dt.Rows[11]["ColVisible"].ToString() == "Y" && dt.Rows[11]["ColReadOnly"].ToString() != "Y") {cColumnSize14.Text = string.Empty;}
			if (dt.Rows[12]["ColVisible"].ToString() == "Y" && dt.Rows[12]["ColReadOnly"].ToString() != "Y") {cColumnHeight14.Text = string.Empty;}
			if (dt.Rows[13]["ColVisible"].ToString() == "Y" && dt.Rows[13]["ColReadOnly"].ToString() != "Y") {cResizeWidth14.Text = string.Empty;}
			if (dt.Rows[14]["ColVisible"].ToString() == "Y" && dt.Rows[14]["ColReadOnly"].ToString() != "Y") {cResizeHeight14.Text = string.Empty;}
			if (dt.Rows[15]["ColVisible"].ToString() == "Y" && dt.Rows[15]["ColReadOnly"].ToString() != "Y") {cSortOrder14.Text = string.Empty;}
			if (dt.Rows[16]["ColVisible"].ToString() == "Y" && dt.Rows[16]["ColReadOnly"].ToString() != "Y") {cScreenId14.ClearSearch();}
			if (dt.Rows[17]["ColVisible"].ToString() == "Y" && dt.Rows[17]["ColReadOnly"].ToString() != "Y") {SetGroupRowId14(cGroupRowId14,"78");}
			if (dt.Rows[18]["ColVisible"].ToString() == "Y" && dt.Rows[18]["ColReadOnly"].ToString() != "Y") {SetGroupColId14(cGroupColId14,"851");}
			if (dt.Rows[19]["ColVisible"].ToString() == "Y" && dt.Rows[19]["ColReadOnly"].ToString() != "Y") {cColumnId14.ClearSearch();}
			if (dt.Rows[20]["ColVisible"].ToString() == "Y" && dt.Rows[20]["ColReadOnly"].ToString() != "Y") {cColumnName14.Text = string.Empty;}
			if (dt.Rows[21]["ColVisible"].ToString() == "Y" && dt.Rows[21]["ColReadOnly"].ToString() != "Y") {SetDisplayModeId14(cDisplayModeId14,string.Empty); cDisplayModeId14_SelectedIndexChanged(cDisplayModeId14, new EventArgs());}
			if (dt.Rows[23]["ColVisible"].ToString() == "Y" && dt.Rows[23]["ColReadOnly"].ToString() != "Y") {cDdlKeyColumnId14.ClearSearch();}
			if (dt.Rows[24]["ColVisible"].ToString() == "Y" && dt.Rows[24]["ColReadOnly"].ToString() != "Y") {cDdlRefColumnId14.ClearSearch();}
			if (dt.Rows[25]["ColVisible"].ToString() == "Y" && dt.Rows[25]["ColReadOnly"].ToString() != "Y") {cDdlSrtColumnId14.ClearSearch();}
			if (dt.Rows[26]["ColVisible"].ToString() == "Y" && dt.Rows[26]["ColReadOnly"].ToString() != "Y") {cDdlAdnColumnId14.ClearSearch();}
			if (dt.Rows[27]["ColVisible"].ToString() == "Y" && dt.Rows[27]["ColReadOnly"].ToString() != "Y") {cDdlFtrColumnId14.ClearSearch();}
			if (dt.Rows[28]["ColVisible"].ToString() == "Y" && dt.Rows[28]["ColReadOnly"].ToString() != "Y") {cColumnLink14.Text = string.Empty;}
			if (dt.Rows[29]["ColVisible"].ToString() == "Y" && dt.Rows[29]["ColReadOnly"].ToString() != "Y") {SetDtlLstPosId14(cDtlLstPosId14,string.Empty);}
			if (dt.Rows[30]["ColVisible"].ToString() == "Y" && dt.Rows[30]["ColReadOnly"].ToString() != "Y") {cLabelVertical14.Checked = base.GetBool("N");}
			if (dt.Rows[31]["ColVisible"].ToString() == "Y" && dt.Rows[31]["ColReadOnly"].ToString() != "Y") {cLabelCss14.Text = string.Empty;}
			if (dt.Rows[32]["ColVisible"].ToString() == "Y" && dt.Rows[32]["ColReadOnly"].ToString() != "Y") {cContentCss14.Text = string.Empty;}
			if (dt.Rows[33]["ColVisible"].ToString() == "Y" && dt.Rows[33]["ColReadOnly"].ToString() != "Y") {cDefaultValue14.Text = string.Empty;}
			if (dt.Rows[34]["ColVisible"].ToString() == "Y" && dt.Rows[34]["ColReadOnly"].ToString() != "Y") {cHyperLinkUrl14.Text = string.Empty;}
			if (dt.Rows[35]["ColVisible"].ToString() == "Y" && dt.Rows[35]["ColReadOnly"].ToString() != "Y") {cDefAfter14.Checked = base.GetBool("N");}
			if (dt.Rows[36]["ColVisible"].ToString() == "Y" && dt.Rows[36]["ColReadOnly"].ToString() != "Y") {cSystemValue14.Text = string.Empty;}
			if (dt.Rows[37]["ColVisible"].ToString() == "Y" && dt.Rows[37]["ColReadOnly"].ToString() != "Y") {cDefAlways14.Checked = base.GetBool("N");}
			if (dt.Rows[38]["ColVisible"].ToString() == "Y" && dt.Rows[38]["ColReadOnly"].ToString() != "Y") {SetAggregateCd14(cAggregateCd14,string.Empty);}
			if (dt.Rows[39]["ColVisible"].ToString() == "Y" && dt.Rows[39]["ColReadOnly"].ToString() != "Y") {cGenerateSp14.Checked = base.GetBool("Y");}
			if (dt.Rows[40]["ColVisible"].ToString() == "Y" && dt.Rows[40]["ColReadOnly"].ToString() != "Y") {cMaskValid14.Text = string.Empty;}
			if (dt.Rows[41]["ColVisible"].ToString() == "Y" && dt.Rows[41]["ColReadOnly"].ToString() != "Y") {cRangeValidType14.Text = string.Empty;}
			if (dt.Rows[42]["ColVisible"].ToString() == "Y" && dt.Rows[42]["ColReadOnly"].ToString() != "Y") {cRangeValidMax14.Text = string.Empty;}
			if (dt.Rows[43]["ColVisible"].ToString() == "Y" && dt.Rows[43]["ColReadOnly"].ToString() != "Y") {cRangeValidMin14.Text = string.Empty;}
			if (dt.Rows[44]["ColVisible"].ToString() == "Y" && dt.Rows[44]["ColReadOnly"].ToString() != "Y") {SetMatchCd14(cMatchCd14,string.Empty);}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cScreenObjId14.Text = string.Empty;
			cMasterTable14.Checked = base.GetBool("N");
			cRequiredValid14.Checked = base.GetBool("N");
			cColumnWrap14.Checked = base.GetBool("Y");
			SetGridGrpCd14(cGridGrpCd14,"N");
			cHideOnTablet14.Checked = base.GetBool("N");
			cHideOnMobile14.Checked = base.GetBool("N");
			cRefreshOnCUD14.Checked = base.GetBool("N");
			cTrimOnEntry14.Checked = base.GetBool("Y");
			cIgnoreConfirm14.Checked = base.GetBool("Y");
			SetColumnJustify14(cColumnJustify14,"L");
			cColumnSize14.Text = string.Empty;
			cColumnHeight14.Text = string.Empty;
			cResizeWidth14.Text = string.Empty;
			cResizeHeight14.Text = string.Empty;
			cSortOrder14.Text = string.Empty;
			cScreenId14.ClearSearch();
			SetGroupRowId14(cGroupRowId14,"78");
			SetGroupColId14(cGroupColId14,"851");
			cColumnId14.ClearSearch();
			cColumnName14.Text = string.Empty;
			SetDisplayModeId14(cDisplayModeId14,string.Empty); cDisplayModeId14_SelectedIndexChanged(cDisplayModeId14, new EventArgs());

			cDdlKeyColumnId14.ClearSearch();
			cDdlRefColumnId14.ClearSearch();
			cDdlSrtColumnId14.ClearSearch();
			cDdlAdnColumnId14.ClearSearch();
			cDdlFtrColumnId14.ClearSearch();
			cColumnLink14.Text = string.Empty;
			SetDtlLstPosId14(cDtlLstPosId14,string.Empty);
			cLabelVertical14.Checked = base.GetBool("N");
			cLabelCss14.Text = string.Empty;
			cContentCss14.Text = string.Empty;
			cDefaultValue14.Text = string.Empty;
			cHyperLinkUrl14.Text = string.Empty;
			cDefAfter14.Checked = base.GetBool("N");
			cSystemValue14.Text = string.Empty;
			cDefAlways14.Checked = base.GetBool("N");
			SetAggregateCd14(cAggregateCd14,string.Empty);
			cGenerateSp14.Checked = base.GetBool("Y");
			cMaskValid14.Text = string.Empty;
			cRangeValidType14.Text = string.Empty;
			cRangeValidMax14.Text = string.Empty;
			cRangeValidMin14.Text = string.Empty;
			SetMatchCd14(cMatchCd14,string.Empty);
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmScreenObj10List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmScreenObj10List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmScreenObj10List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmScreenObj10ById",cAdmScreenObj10List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmScreenObj10List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cScreenObjId14.Text = RO.Common3.Utils.fmNumeric("0",dr["ScreenObjId14"].ToString(),base.LUser.Culture);} catch {cScreenObjId14.Text = string.Empty;}
					try {cMasterTable14.Checked = base.GetBool(dr["MasterTable14"].ToString());} catch {cMasterTable14.Checked = false;}
					try {cRequiredValid14.Checked = base.GetBool(dr["RequiredValid14"].ToString());} catch {cRequiredValid14.Checked = false;}
					try {cColumnWrap14.Checked = base.GetBool(dr["ColumnWrap14"].ToString());} catch {cColumnWrap14.Checked = false;}
					SetGridGrpCd14(cGridGrpCd14,dr["GridGrpCd14"].ToString()); if (cGridGrpCd14.SelectedIndex <= 0 && !(cGridGrpCd14.Enabled && cGridGrpCd14.Visible)) { SetGridGrpCd14(cGridGrpCd14,"N"); }
					try {cHideOnTablet14.Checked = base.GetBool(dr["HideOnTablet14"].ToString());} catch {cHideOnTablet14.Checked = false;}
					try {cHideOnMobile14.Checked = base.GetBool(dr["HideOnMobile14"].ToString());} catch {cHideOnMobile14.Checked = false;}
					try {cRefreshOnCUD14.Checked = base.GetBool(dr["RefreshOnCUD14"].ToString());} catch {cRefreshOnCUD14.Checked = false;}
					try {cTrimOnEntry14.Checked = base.GetBool(dr["TrimOnEntry14"].ToString());} catch {cTrimOnEntry14.Checked = false;}
					try {cIgnoreConfirm14.Checked = base.GetBool(dr["IgnoreConfirm14"].ToString());} catch {cIgnoreConfirm14.Checked = false;}
					SetColumnJustify14(cColumnJustify14,dr["ColumnJustify14"].ToString()); if (cColumnJustify14.SelectedIndex <= 0 && !(cColumnJustify14.Enabled && cColumnJustify14.Visible)) { SetColumnJustify14(cColumnJustify14,"L"); }
					try {cColumnSize14.Text = RO.Common3.Utils.fmNumeric("0",dr["ColumnSize14"].ToString(),base.LUser.Culture);} catch {cColumnSize14.Text = string.Empty;}
					try {cColumnHeight14.Text = RO.Common3.Utils.fmNumeric("0",dr["ColumnHeight14"].ToString(),base.LUser.Culture);} catch {cColumnHeight14.Text = string.Empty;}
					try {cResizeWidth14.Text = RO.Common3.Utils.fmNumeric("0",dr["ResizeWidth14"].ToString(),base.LUser.Culture);} catch {cResizeWidth14.Text = string.Empty;}
					try {cResizeHeight14.Text = RO.Common3.Utils.fmNumeric("0",dr["ResizeHeight14"].ToString(),base.LUser.Culture);} catch {cResizeHeight14.Text = string.Empty;}
					try {cSortOrder14.Text = RO.Common3.Utils.fmNumeric("0",dr["SortOrder14"].ToString(),base.LUser.Culture);} catch {cSortOrder14.Text = string.Empty;}
					SetScreenId14(cScreenId14,dr["ScreenId14"].ToString());
					SetGroupRowId14(cGroupRowId14,dr["GroupRowId14"].ToString());
					SetGroupColId14(cGroupColId14,dr["GroupColId14"].ToString());
					SetColumnId14(cColumnId14,dr["ColumnId14"].ToString()); cColumnId14_SelectedIndexChanged(cColumnId14, new EventArgs());
					try {cColumnName14.Text = dr["ColumnName14"].ToString();} catch {cColumnName14.Text = string.Empty;}
					SetDisplayModeId14(cDisplayModeId14,dr["DisplayModeId14"].ToString()); cDisplayModeId14_SelectedIndexChanged(cDisplayModeId14, new EventArgs());
					SetDdlKeyColumnId14(cDdlKeyColumnId14,dr["DdlKeyColumnId14"].ToString());
					SetDdlRefColumnId14(cDdlRefColumnId14,dr["DdlRefColumnId14"].ToString());
					SetDdlSrtColumnId14(cDdlSrtColumnId14,dr["DdlSrtColumnId14"].ToString());
					SetDdlAdnColumnId14(cDdlAdnColumnId14,dr["DdlAdnColumnId14"].ToString());
					SetDdlFtrColumnId14(cDdlFtrColumnId14,dr["DdlFtrColumnId14"].ToString());
					try {cColumnLink14.Text = dr["ColumnLink14"].ToString();} catch {cColumnLink14.Text = string.Empty;}
					SetDtlLstPosId14(cDtlLstPosId14,dr["DtlLstPosId14"].ToString());
					try {cLabelVertical14.Checked = base.GetBool(dr["LabelVertical14"].ToString());} catch {cLabelVertical14.Checked = false;}
					try {cLabelCss14.Text = dr["LabelCss14"].ToString();} catch {cLabelCss14.Text = string.Empty;}
					try {cContentCss14.Text = dr["ContentCss14"].ToString();} catch {cContentCss14.Text = string.Empty;}
					try {cDefaultValue14.Text = dr["DefaultValue14"].ToString();} catch {cDefaultValue14.Text = string.Empty;}
					try {cHyperLinkUrl14.Text = dr["HyperLinkUrl14"].ToString();} catch {cHyperLinkUrl14.Text = string.Empty;}
					try {cDefAfter14.Checked = base.GetBool(dr["DefAfter14"].ToString());} catch {cDefAfter14.Checked = false;}
					try {cSystemValue14.Text = dr["SystemValue14"].ToString();} catch {cSystemValue14.Text = string.Empty;}
					try {cDefAlways14.Checked = base.GetBool(dr["DefAlways14"].ToString());} catch {cDefAlways14.Checked = false;}
					SetAggregateCd14(cAggregateCd14,dr["AggregateCd14"].ToString());
					try {cGenerateSp14.Checked = base.GetBool(dr["GenerateSp14"].ToString());} catch {cGenerateSp14.Checked = false;}
					try {cMaskValid14.Text = dr["MaskValid14"].ToString();} catch {cMaskValid14.Text = string.Empty;}
					try {cRangeValidType14.Text = dr["RangeValidType14"].ToString();} catch {cRangeValidType14.Text = string.Empty;}
					try {cRangeValidMax14.Text = dr["RangeValidMax14"].ToString();} catch {cRangeValidMax14.Text = string.Empty;}
					try {cRangeValidMin14.Text = dr["RangeValidMin14"].ToString();} catch {cRangeValidMin14.Text = string.Empty;}
					SetMatchCd14(cMatchCd14,dr["MatchCd14"].ToString());
				}
			}
			cButPanel.DataBind();
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmScreenObj10List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cMasterTable14_CheckedChanged(object sender, System.EventArgs e)
		{
			//WebRule: Initialize Hide on ...
            DataTable dt = (DataTable)Session[KEY_dtScreenId14];
            DataView dv = dt != null ? dt.DefaultView : null;
            dv.RowFilter = "ScreenId14=" + cScreenId14.SelectedValue;
            if (cMasterTable14.Checked && "5,6".IndexOf(dv[0]["ScreenTypeId"].ToString()) >= 0)
            {
                cHideOnMobile14.Checked = false; cHideOnMobile14P1.Visible = false; cHideOnMobile14P2.Visible = false;
                cHideOnTablet14.Checked = false; cHideOnTablet14P1.Visible = false; cHideOnTablet14P2.Visible = false;
                cDtlLstPosId14.ClearSelection(); cDtlLstPosId14P1.Visible = false; cDtlLstPosId14P2.Visible = false;
            }
            else
            {
                cHideOnMobile14P1.Visible = true; cHideOnMobile14P2.Visible = true;
                cHideOnTablet14P1.Visible = true; cHideOnTablet14P2.Visible = true;
                cDtlLstPosId14P1.Visible = true; cDtlLstPosId14P2.Visible = true;
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cColumnId14_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cColumnId14_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cColumnId14_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//WebRule: Initialize MasterTable flag
            string mtb = (new RO.WebRules.WebRule()).WrGetMasterTable(cScreenId14.Text, cColumnId14.SelectedValue, (string)Session[KEY_sysConnectionString], LcAppPw);
            if (string.IsNullOrEmpty(mtb)) { cMasterTable14.Enabled = true; } else { cMasterTable14.Checked = GetBool(mtb); cMasterTable14.Enabled = false; }
            cMasterTable14_CheckedChanged(sender, new EventArgs());
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cColumnId14.Items.Count > 0 && cColumnId14.DataSource != null)
			{
				DataView dv = (DataView) cColumnId14.DataSource; dv.RowFilter = string.Empty;
				DataRowView dr = cColumnId14.DataSetIndex >= 0 && cColumnId14.DataSetIndex < dv.Count ? dv[cColumnId14.DataSetIndex] : dv[0];
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cDisplayModeId14_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//WebRule: Prepare db for document management or workflow ststus plus hide/show appropriate fields
            if (",3,5,17,29,32,53,".IndexOf("," + cDisplayModeId14.SelectedValue + ",") >= 0)   // DropDownList, ListBox, RadioButtonList, DataGridLink or Document or WorkflowStatus
            {
                cRefreshOnCUD14P1.Visible = true; cRefreshOnCUD14P2.Visible = true;
            }
            else
            {
                cRefreshOnCUD14.Checked = false; cRefreshOnCUD14P1.Visible = false; cRefreshOnCUD14P2.Visible = false;
            }
            if (",38,52,53,".IndexOf("," + cDisplayModeId14.SelectedValue + ",") >= 0)   // AutoComplete, AutoListBox, WorkflowStatus
            {
                cColumnHeight14.Text = string.Empty; cColumnHeight14P1.Visible = false; cColumnHeight14P2.Visible = false;
            }
            else
            {
                cColumnHeight14P1.Visible = true; cColumnHeight14P2.Visible = true;
            }
            if (",23,24,25,26,".IndexOf("," + cDisplayModeId14.SelectedValue + ",") >= 0)   // HyperLinks
            {
                cHyperLinkUrl14P1.Visible = true; cHyperLinkUrl14P2.Visible = true;
                cDefAfter14.Checked = false; cDefAfter14P1.Visible = false; cDefAfter14P2.Visible = false;
                cSystemValue14.Text = string.Empty; cSystemValue14P1.Visible = false; cSystemValue14P2.Visible = false;
                cDefAlways14.Checked = false; cDefAlways14P1.Visible = false; cDefAlways14P2.Visible = false;
                cAggregateCd14.ClearSelection(); cAggregateCd14P1.Visible = false; cAggregateCd14P2.Visible = false;
                cMaskValid14.Text = string.Empty; cMaskValid14P1.Visible = false; cMaskValid14P2.Visible = false;
                cRangeValidType14.Text = string.Empty; cRangeValidType14P1.Visible = false; cRangeValidType14P2.Visible = false;
                cRangeValidMax14.Text = string.Empty; cRangeValidMax14P1.Visible = false; cRangeValidMax14P2.Visible = false;
                cRangeValidMin14.Text = string.Empty; cRangeValidMin14P1.Visible = false; cRangeValidMin14P2.Visible = false;
            }
            else
            {
                cHyperLinkUrl14.Text = string.Empty; cHyperLinkUrl14P1.Visible = false; cHyperLinkUrl14P2.Visible = false;
                cDefAfter14P1.Visible = true; cDefAfter14P2.Visible = true;
                cSystemValue14P1.Visible = true; cSystemValue14P2.Visible = true;
                cDefAlways14P1.Visible = true; cDefAlways14P2.Visible = true;
                cAggregateCd14P1.Visible = true; cAggregateCd14P2.Visible = true;
                cMaskValid14P1.Visible = true; cMaskValid14P2.Visible = true;
                cRangeValidType14P1.Visible = true; cRangeValidType14P2.Visible = true;
                cRangeValidMax14P1.Visible = true; cRangeValidMax14P2.Visible = true;
                cRangeValidMin14P1.Visible = true; cRangeValidMin14P2.Visible = true;
            }
            if (IsPostBack && cDisplayModeId14.SelectedValue == "32")   // Document: Do not check cColumnLink14 as it is loaded from this on pageload.
            {
                string ctrlname = Request.Params.Get("__EVENTTARGET");
                if (ctrlname != null && ctrlname != string.Empty && ctrlname.EndsWith(cDisplayModeId14.ID))
                {
                    try
                    {
                        DataTable dt = (new WebRule()).WrAddDocTbl(LCurr.DbId, cColumnName14.Text, (string)Session[KEY_sysConnectionString], LcAppPw);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            SetDdlKeyColumnId14(cDdlKeyColumnId14, dt.Rows[0][0].ToString());
                            SetDdlRefColumnId14(cDdlRefColumnId14, dt.Rows[1][0].ToString());
                            //cColumnLink14.Text = "UpLoad.aspx?tbl=dbo." + cColumnName14.Text;
                        }
                        //else
                        //{
                        //    bInfoNow.Value = "Y"; PreMsgPopup("Table '" + cColumnName14.Text + "' already exist. Please try another column name or should you proceed, this could be sharing the same document storage table with other columns.");
                        //}
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
                }
            }
            if (IsPostBack && cDisplayModeId14.SelectedValue == "53")
            {
                string ctrlname = Request.Params.Get("__EVENTTARGET");
                if (ctrlname != null && ctrlname != string.Empty && ctrlname.EndsWith(cDisplayModeId14.ID))
                {
                    try
                    {
                        DataTable dt = (new WebRule()).WrAddWfsTbl(LCurr.DbId, cColumnName14.Text, (string)Session[KEY_sysConnectionString], LcAppPw);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            SetDdlKeyColumnId14(cDdlKeyColumnId14, dt.Rows[0][0].ToString());
                            SetDdlRefColumnId14(cDdlRefColumnId14, dt.Rows[1][0].ToString());
                            SetDdlSrtColumnId14(cDdlSrtColumnId14, dt.Rows[5][0].ToString());
                            cColumnLink14.Text = base.SystemsList.Rows[cSystemId.SelectedIndex]["SystemAbbr"].ToString() + cColumnName14.Text + ".aspx?col=StatusCd";
                        }
                        //else
                        //{
                        //    bInfoNow.Value = "Y"; PreMsgPopup("Table '" + cColumnName14.Text + "' already exist. Please try another column name or should you proceed, this could be sharing the same document storage table with other columns.");
                        //}
                        (new AdminSystem()).MkWfStatus(cScreenObjId14.Text, "Y", base.SystemsList.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), base.SystemsList.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
                }
            }
			//WebRule: Hide/Show appropriate fields
            if (cDisplayModeId14.SelectedValue == "28")       // Image only.
            {
                cResizeWidth14P1.Visible = true; cResizeWidth14P2.Visible = true;
                cResizeHeight14P1.Visible = true; cResizeHeight14P2.Visible = true;
            }
            else
            {
                cResizeWidth14P1.Visible = false; cResizeWidth14P2.Visible = false;
                cResizeHeight14P1.Visible = false; cResizeHeight14P2.Visible = false;
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cDisplayModeId14.Items.Count > 0 && Session[KEY_dtDisplayModeId14] != null)
			{
				DataView dv = ((DataTable)Session[KEY_dtDisplayModeId14]).DefaultView; dv.RowFilter = string.Empty;
				DataRowView dr = cDisplayModeId14.SelectedIndex >= 0 && cDisplayModeId14.SelectedIndex < dv.Count ? dv[cDisplayModeId14.SelectedIndex] : dv[0];
				cDisplayDesc18.Text = dr["DisplayDesc18"].ToString();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
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
			cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
			PopAdmScreenObj10List(sender, e, false, null);
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
			cScreenObjId14.Text = string.Empty;
			cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
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
			if (cScreenObjId14.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmScreenObj10List(sender, e, false, cScreenObjId14.Text); }
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
			Session.Remove(KEY_dtAdmScreenObj10List); PopAdmScreenObj10List(sender, e, false, null);
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
				AdmScreenObj10 ds = PrepAdmScreenObjData(null,cScreenObjId14.Text == string.Empty);
				if (string.IsNullOrEmpty(cAdmScreenObj10List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(10,true,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmScreenObj10List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(10,true,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
						ShowDirty(false); PopAdmScreenObj10List(sender, e, false, ds.Tables["AdmScreenObj"].Rows[0]["ScreenObjId14"]);
						rtn = GetScreenHlp().Rows[0]["UpdMsg"].ToString();
					}
				}
			}
			//WebRule: Synchronize the label with the controlling object
            int filterId = 0;
            string key = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
            else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
            DataTable dtItms = (new WebRule()).WrGetScreenObj(null, base.LUser.CultureId, cAdmScreenObj10List.SelectedValue,(string)Session[KEY_sysConnectionString], LcAppPw);

            string checkMaster = dtItms.Rows[0]["MasterTable"].ToString();
            string objDisMode = dtItms.Rows[0]["DisplayMode"].ToString();
            string disHeight = dtItms.Rows[0]["ColumnHeight"].ToString();
            string RequiredValid = dtItms.Rows[0]["RequiredValid"].ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", @"
                    <script type='text/javascript'>
                        passMasterToParent('" + checkMaster + @"');
                        passHeightToParent('" + disHeight + @"' , '" + objDisMode + @"');
                        passMandatoryToParent('" + RequiredValid + @"');

                        function passMasterToParent(val)
                        {
                            var objText;
                            if ( (objText = window.parent) && (objText = objText.updObjMaster) && ('function' == typeof objText || 'object' == typeof objText) ){ objText(val); }
                        }
                         function passHeightToParent(height, type)
                        {
                            var objHeight;
                            if ( (objHeight = window.parent) && (objHeight = objHeight.updObjHeight) && ('function' == typeof objHeight || 'object' == typeof objHeight) ){ objHeight(height, type); }
                        }
                        
                        function passMandatoryToParent(val)
                        {
                            var objMandatory;
                            if ( (objMandatory = window.parent) && (objMandatory = objMandatory.updObjMandatory) && ('function' == typeof objMandatory || 'object' == typeof objMandatory) ){ objMandatory(val); }
                        }
                    </script>
            ", false);
			// *** WebRule End *** //
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
			if (cScreenObjId14.Text != string.Empty)
			{
				AdmScreenObj10 ds = PrepAdmScreenObjData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(10,true,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc))
					{
						cAdmScreenObj10List.ClearSearch(); Session.Remove(KEY_dtAdmScreenObj10List);
						ShowDirty(false); PopAdmScreenObj10List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmScreenObj10 PrepAdmScreenObjData(DataView dv, bool bAdd)
		{
			AdmScreenObj10 ds = new AdmScreenObj10();
			DataRow dr = ds.Tables["AdmScreenObj"].NewRow();
			DataRow drType = ds.Tables["AdmScreenObj"].NewRow();
			DataRow drDisp = ds.Tables["AdmScreenObj"].NewRow();
			if (bAdd) { dr["ScreenObjId14"] = string.Empty; } else { dr["ScreenObjId14"] = cScreenObjId14.Text; }
			drType["ScreenObjId14"] = "Numeric"; drDisp["ScreenObjId14"] = "TextBox";
			try {dr["MasterTable14"] = base.SetBool(cMasterTable14.Checked);} catch {}
			drType["MasterTable14"] = "Char"; drDisp["MasterTable14"] = "CheckBox";
			try {dr["RequiredValid14"] = base.SetBool(cRequiredValid14.Checked);} catch {}
			drType["RequiredValid14"] = "Char"; drDisp["RequiredValid14"] = "CheckBox";
			try {dr["ColumnWrap14"] = base.SetBool(cColumnWrap14.Checked);} catch {}
			drType["ColumnWrap14"] = "Char"; drDisp["ColumnWrap14"] = "CheckBox";
			try {dr["GridGrpCd14"] = cGridGrpCd14.SelectedValue;} catch {}
			drType["GridGrpCd14"] = "Char"; drDisp["GridGrpCd14"] = "DropDownList";
			try {dr["HideOnTablet14"] = base.SetBool(cHideOnTablet14.Checked);} catch {}
			drType["HideOnTablet14"] = "Char"; drDisp["HideOnTablet14"] = "CheckBox";
			try {dr["HideOnMobile14"] = base.SetBool(cHideOnMobile14.Checked);} catch {}
			drType["HideOnMobile14"] = "Char"; drDisp["HideOnMobile14"] = "CheckBox";
			try {dr["RefreshOnCUD14"] = base.SetBool(cRefreshOnCUD14.Checked);} catch {}
			drType["RefreshOnCUD14"] = "Char"; drDisp["RefreshOnCUD14"] = "CheckBox";
			try {dr["TrimOnEntry14"] = base.SetBool(cTrimOnEntry14.Checked);} catch {}
			drType["TrimOnEntry14"] = "Char"; drDisp["TrimOnEntry14"] = "CheckBox";
			try {dr["IgnoreConfirm14"] = base.SetBool(cIgnoreConfirm14.Checked);} catch {}
			drType["IgnoreConfirm14"] = "Char"; drDisp["IgnoreConfirm14"] = "CheckBox";
			try {dr["ColumnJustify14"] = cColumnJustify14.SelectedValue;} catch {}
			drType["ColumnJustify14"] = "Char"; drDisp["ColumnJustify14"] = "DropDownList";
			try {dr["ColumnSize14"] = cColumnSize14.Text.Trim();} catch {}
			drType["ColumnSize14"] = "Numeric"; drDisp["ColumnSize14"] = "TextBox";
			try {dr["ColumnHeight14"] = cColumnHeight14.Text.Trim();} catch {}
			drType["ColumnHeight14"] = "Numeric"; drDisp["ColumnHeight14"] = "TextBox";
			try {dr["ResizeWidth14"] = cResizeWidth14.Text.Trim();} catch {}
			drType["ResizeWidth14"] = "Numeric"; drDisp["ResizeWidth14"] = "TextBox";
			try {dr["ResizeHeight14"] = cResizeHeight14.Text.Trim();} catch {}
			drType["ResizeHeight14"] = "Numeric"; drDisp["ResizeHeight14"] = "TextBox";
			try {dr["SortOrder14"] = cSortOrder14.Text.Trim();} catch {}
			drType["SortOrder14"] = "Numeric"; drDisp["SortOrder14"] = "TextBox";
			try {dr["ScreenId14"] = cScreenId14.SelectedValue;} catch {}
			drType["ScreenId14"] = "Numeric"; drDisp["ScreenId14"] = "AutoComplete";
			try {dr["GroupRowId14"] = cGroupRowId14.SelectedValue;} catch {}
			drType["GroupRowId14"] = "Numeric"; drDisp["GroupRowId14"] = "AutoComplete";
			try {dr["GroupColId14"] = cGroupColId14.SelectedValue;} catch {}
			drType["GroupColId14"] = "Numeric"; drDisp["GroupColId14"] = "AutoComplete";
			try {dr["ColumnId14"] = cColumnId14.SelectedValue;} catch {}
			drType["ColumnId14"] = "Numeric"; drDisp["ColumnId14"] = "AutoComplete";
			try {dr["ColumnName14"] = cColumnName14.Text.Trim();} catch {}
			drType["ColumnName14"] = "VarChar"; drDisp["ColumnName14"] = "TextBox";
			try {dr["DisplayModeId14"] = cDisplayModeId14.SelectedValue;} catch {}
			drType["DisplayModeId14"] = "Numeric"; drDisp["DisplayModeId14"] = "DropDownList";
			try {dr["DdlKeyColumnId14"] = cDdlKeyColumnId14.SelectedValue;} catch {}
			drType["DdlKeyColumnId14"] = "Numeric"; drDisp["DdlKeyColumnId14"] = "AutoComplete";
			try {dr["DdlRefColumnId14"] = cDdlRefColumnId14.SelectedValue;} catch {}
			drType["DdlRefColumnId14"] = "Numeric"; drDisp["DdlRefColumnId14"] = "AutoComplete";
			try {dr["DdlSrtColumnId14"] = cDdlSrtColumnId14.SelectedValue;} catch {}
			drType["DdlSrtColumnId14"] = "Numeric"; drDisp["DdlSrtColumnId14"] = "AutoComplete";
			try {dr["DdlAdnColumnId14"] = cDdlAdnColumnId14.SelectedValue;} catch {}
			drType["DdlAdnColumnId14"] = "Numeric"; drDisp["DdlAdnColumnId14"] = "AutoComplete";
			try {dr["DdlFtrColumnId14"] = cDdlFtrColumnId14.SelectedValue;} catch {}
			drType["DdlFtrColumnId14"] = "Numeric"; drDisp["DdlFtrColumnId14"] = "AutoComplete";
			try {dr["ColumnLink14"] = cColumnLink14.Text.Trim();} catch {}
			drType["ColumnLink14"] = "VarChar"; drDisp["ColumnLink14"] = "TextBox";
			try {dr["DtlLstPosId14"] = cDtlLstPosId14.SelectedValue;} catch {}
			drType["DtlLstPosId14"] = "Numeric"; drDisp["DtlLstPosId14"] = "DropDownList";
			try {dr["LabelVertical14"] = base.SetBool(cLabelVertical14.Checked);} catch {}
			drType["LabelVertical14"] = "Char"; drDisp["LabelVertical14"] = "CheckBox";
			try {dr["LabelCss14"] = cLabelCss14.Text.Trim();} catch {}
			drType["LabelCss14"] = "VarChar"; drDisp["LabelCss14"] = "TextBox";
			try {dr["ContentCss14"] = cContentCss14.Text.Trim();} catch {}
			drType["ContentCss14"] = "VarChar"; drDisp["ContentCss14"] = "TextBox";
			try {dr["DefaultValue14"] = cDefaultValue14.Text.Trim();} catch {}
			drType["DefaultValue14"] = "VarWChar"; drDisp["DefaultValue14"] = "TextBox";
			try {dr["HyperLinkUrl14"] = cHyperLinkUrl14.Text.Trim();} catch {}
			drType["HyperLinkUrl14"] = "VarWChar"; drDisp["HyperLinkUrl14"] = "TextBox";
			try {dr["DefAfter14"] = base.SetBool(cDefAfter14.Checked);} catch {}
			drType["DefAfter14"] = "Char"; drDisp["DefAfter14"] = "CheckBox";
			try {dr["SystemValue14"] = cSystemValue14.Text.Trim();} catch {}
			drType["SystemValue14"] = "VarWChar"; drDisp["SystemValue14"] = "TextBox";
			try {dr["DefAlways14"] = base.SetBool(cDefAlways14.Checked);} catch {}
			drType["DefAlways14"] = "Char"; drDisp["DefAlways14"] = "CheckBox";
			try {dr["AggregateCd14"] = cAggregateCd14.SelectedValue;} catch {}
			drType["AggregateCd14"] = "Char"; drDisp["AggregateCd14"] = "DropDownList";
			try {dr["GenerateSp14"] = base.SetBool(cGenerateSp14.Checked);} catch {}
			drType["GenerateSp14"] = "Char"; drDisp["GenerateSp14"] = "CheckBox";
			try {dr["MaskValid14"] = cMaskValid14.Text.Trim();} catch {}
			drType["MaskValid14"] = "VarChar"; drDisp["MaskValid14"] = "TextBox";
			try {dr["RangeValidType14"] = cRangeValidType14.Text.Trim();} catch {}
			drType["RangeValidType14"] = "VarChar"; drDisp["RangeValidType14"] = "TextBox";
			try {dr["RangeValidMax14"] = cRangeValidMax14.Text.Trim();} catch {}
			drType["RangeValidMax14"] = "VarChar"; drDisp["RangeValidMax14"] = "TextBox";
			try {dr["RangeValidMin14"] = cRangeValidMin14.Text.Trim();} catch {}
			drType["RangeValidMin14"] = "VarChar"; drDisp["RangeValidMin14"] = "TextBox";
			try {dr["MatchCd14"] = cMatchCd14.SelectedValue;} catch {}
			drType["MatchCd14"] = "Char"; drDisp["MatchCd14"] = "DropDownList";
			if (bAdd)
			{
			}
			ds.Tables["AdmScreenObj"].Rows.Add(dr); ds.Tables["AdmScreenObj"].Rows.Add(drType); ds.Tables["AdmScreenObj"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmScreenObj10List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtGridGrpCd14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'GridGrpCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtColumnJustify14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ColumnJustify', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtScreenId14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ScreenId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtGroupRowId14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'GroupRowId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtGroupColId14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'GroupColId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtDisplayModeId14];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'DisplayModeId', please investigate."); return false;
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmScreenObj10List,null); }

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

