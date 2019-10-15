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
	public class AdmRelease98 : DataSet
	{
		public AdmRelease98()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmRelease")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseDef")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseAdd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseUpd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseDel")));
			this.DataSetName = "AdmRelease98";
			this.Namespace = "http://Rintagi.com/DataSet/AdmRelease98";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ReleaseId191", typeof(string));
			columns.Add("ReleaseName191", typeof(string));
			columns.Add("ReleaseBuild191", typeof(string));
			columns.Add("ReleaseDate191", typeof(string));
			columns.Add("ReleaseOs191", typeof(string));
			columns.Add("EntityId191", typeof(string));
			columns.Add("ReleaseTypeId191", typeof(string));
			columns.Add("TarScriptAft191", typeof(string));
			columns.Add("ReadMe191", typeof(string));
			return dt;
		}

		private DataTable MakeDtlColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ReleaseId191", typeof(string));
			columns.Add("ReleaseDtlId192", typeof(string));
			columns.Add("ObjectType192", typeof(string));
			columns.Add("RunOrder192", typeof(string));
			columns.Add("SrcObject192", typeof(string));
			columns.Add("SProcOnly192", typeof(string));
			columns.Add("ObjectName192", typeof(string));
			columns.Add("ObjectExempt192", typeof(string));
			columns.Add("SrcClientTierId192", typeof(string));
			columns.Add("SrcRuleTierId192", typeof(string));
			columns.Add("SrcDataTierId192", typeof(string));
			columns.Add("TarDataTierId192", typeof(string));
			columns.Add("DoSpEncrypt192", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmReleaseModule : RO.Web.ModuleBase
	{

		private const string KEY_lastAddedRow = "Cache:lastAddedRow3_98";
		private const string KEY_lastSortOrd = "Cache:lastSortOrd3_98";
		private const string KEY_lastSortImg = "Cache:lastSortImg3_98";
		private const string KEY_lastSortCol = "Cache:lastSortCol3_98";
		private const string KEY_lastSortExp = "Cache:lastSortExp3_98";
		private const string KEY_lastSortUrl = "Cache:lastSortUrl3_98";
		private const string KEY_lastSortTog = "Cache:lastSortTog3_98";
		private const string KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride3_98";
		private const string KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride3_98";
		private const string KEY_currPageIndex = "Cache:currPageIndex3_98";
		private const string KEY_bHiImpVisible = "Cache:bHiImpVisible3_98";
		private const string KEY_bShImpVisible = "Cache:bShImpVisible3_98";
		private const string KEY_dtAdmReleaseGrid = "Cache:dtAdmReleaseGrid";
		private const string KEY_scrImport = "Cache:scrImport3_98";
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_98";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_98";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_98";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_98";
		private const string KEY_dtLabel = "Cache:dtLabel3_98";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_98";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_98";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_98";

		private const string KEY_dtReleaseOs191 = "Cache:dtReleaseOs191";
		private const string KEY_dtEntityId191 = "Cache:dtEntityId191";
		private const string KEY_dtReleaseTypeId191 = "Cache:dtReleaseTypeId191";
		private const string KEY_dtObjectType192 = "Cache:dtObjectType192";
		private const string KEY_dtSProcOnly192 = "Cache:dtSProcOnly192";
		private const string KEY_dtSrcClientTierId192 = "Cache:dtSrcClientTierId192";
		private const string KEY_dtSrcRuleTierId192 = "Cache:dtSrcRuleTierId192";
		private const string KEY_dtSrcDataTierId192 = "Cache:dtSrcDataTierId192";
		private const string KEY_dtTarDataTierId192 = "Cache:dtTarDataTierId192";

		private const string KEY_dtSystems = "Cache:dtSystems3_98";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_98";
		private const string KEY_dtAdmRelease98List = "Cache:dtAdmRelease98List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_98";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_98";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_98";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_98";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_98";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_98";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_98";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_98";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmReleaseModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,98, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				cFind.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cFindButton.ClientID + "')");
				cPgSize.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cPgSizeButton.ClientID + "')");
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
				Session.Remove(KEY_dtAdmReleaseGrid);
				Session.Remove(KEY_lastSortCol);
				Session.Remove(KEY_lastSortExp);
				Session.Remove(KEY_lastSortImg);
				Session.Remove(KEY_lastSortUrl);
				Session.Remove(KEY_dtAdmRelease98List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtReleaseOs191);
				Session.Remove(KEY_dtEntityId191);
				Session.Remove(KEY_dtReleaseTypeId191);
				Session.Remove(KEY_dtObjectType192);
				Session.Remove(KEY_dtSProcOnly192);
				Session.Remove(KEY_dtSrcClientTierId192);
				Session.Remove(KEY_dtSrcRuleTierId192);
				Session.Remove(KEY_dtSrcDataTierId192);
				Session.Remove(KEY_dtTarDataTierId192);
				SetButtonHlp();
				GetSystems();
				SetColumnAuthority();
				GetGlobalFilter();
				GetScreenFilter();
				GetPageInfo();
				GetCriteria(dvCri);
				DataTable dtHlp = GetScreenHlp();
				cHelpMsg.HelpTitle = dtHlp.Rows[0]["ScreenTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
				cFootLabel.Text = dtHlp.Rows[0]["FootNote"].ToString();
				if (cFootLabel.Text == string.Empty) { cFootLabel.Visible = false; }
				cTitleLabel.Text = dtHlp.Rows[0]["ScreenTitle"].ToString();
				DataTable dt = GetScreenTab();
				cTab61.InnerText = dt.Rows[0]["TabFolderName"].ToString();
				cTab62.InnerText = dt.Rows[1]["TabFolderName"].ToString();
				cTab63.InnerText = dt.Rows[2]["TabFolderName"].ToString();
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 98, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmRelease98List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=98&typ=N&sys=3','','',''); return false;"; }
				// *** Page Load (End of) Web Rule starts here *** //
				cBrowse.Attributes.Add("OnChange", "__doPostBack('" + cBrowseButton.ClientID.Replace("_", "$") + "','')");
				DataTable dtLabel = (new AdminSystem()).GetLabels(base.LUser.CultureId, "cGrid", null, null, null);
				if (dtLabel != null)
				{
				    cGridFtrLabel.Text = dtLabel.Rows[0][1].ToString();
				    cGridFndLabel.Text = dtLabel.Rows[1][1].ToString();
				    cImpPwdLabel.Text = dtLabel.Rows[2][1].ToString();
				    cImportPwd.ToolTip = dtLabel.Rows[3][1].ToString();
				    cGridWksLabel.Text = dtLabel.Rows[4][1].ToString();
				    cGridStrLabel.Text = dtLabel.Rows[5][1].ToString();
				    cStartRow.ToolTip = dtLabel.Rows[6][1].ToString();
				}
			}
			else
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
				if (dt != null)
				{
					string sr = ViewState["_SortColumn"] as string;
					if (sr != null) { dt.DefaultView.Sort = sr.ToString(); }
					string rf = ViewState["_RowFilter"] as string;
					if (rf != null) { dt.DefaultView.RowFilter = rf.ToString(); }
				}
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
				try
				{
					DataTable dt;
					dt = (new AdminSystem()).GetAuthRow(98,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
					Session[KEY_dtAuthRow] = dt;
					DataRow dr = dt.Rows[0];
					if (!((dr["AllowUpd"].ToString() == "N" || dr["ViewOnly"].ToString() == "G") && dr["AllowAdd"].ToString() == "N") && (Request.QueryString["enb"] == null || Request.QueryString["enb"] != "N"))
					{
						cAdmReleaseGrid.PreRender += new EventHandler(cAdmReleaseGrid_OnPreRender);
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,98,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(98, "Installer Configuration", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(98,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
						if (dr["ButtonTypeName"].ToString() == "InsRow") { cInsRowButton.CssClass = "ButtonImg InsRowButtonImg"; cInsRowButton.Text = "ADD"; cInsRowButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cInsRowButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "PgSize") { cPgSizeButton.CssClass = "ButtonImg PgSizeButtonImg"; cPgSizeButton.Text = string.Empty; cPgSizeButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPgSizeButton.ToolTip = dr["ButtonToolTip"].ToString(); cPgSize.Visible = base.GetBool(dr["ButtonVisible"].ToString()); }
						else if (dr["ButtonTypeName"].ToString() == "First") { cFirstButton.CssClass = "ButtonImg FirstButtonImg"; cFirstButton.Text = string.Empty; cFirstButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cFirstButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Prev") { cPrevButton.CssClass = "ButtonImg PrevButtonImg"; cPrevButton.Text = string.Empty; cPrevButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPrevButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Next") { cNextButton.CssClass = "ButtonImg NextButtonImg"; cNextButton.Text = string.Empty; cNextButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNextButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Last") { cLastButton.CssClass = "ButtonImg LastButtonImg"; cLastButton.Text = string.Empty; cLastButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cLastButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Find") { cFindButton.CssClass = "ButtonImg FindButtonImg"; cFindButton.Text = string.Empty; cFindButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cFindButton.ToolTip = dr["ButtonToolTip"].ToString(); cFind.Visible = base.GetBool(dr["ButtonVisible"].ToString()); }
						else if (dr["ButtonTypeName"].ToString() == "Import") { cImportButton.CssClass = "ButtonImg ImportButtonImg"; cImportButton.Text = string.Empty; cImportButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cImportButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Continue") { cContinueButton.CssClass = "ButtonImg ContinueButtonImg"; cContinueButton.Text = string.Empty; cContinueButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cContinueButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "HideImp") { cHideImpButton.CssClass = "ButtonImg HideImpButtonImg"; cHideImpButton.Text = string.Empty; cHideImpButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cHideImpButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bHiImpVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cHideImpButton.Visible = false; }
						else if (dr["ButtonTypeName"].ToString() == "ShowImp") { cShowImpButton.CssClass = "ButtonImg ShowImpButtonImg"; cShowImpButton.Text = string.Empty; cShowImpButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cShowImpButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bShImpVisible] = cShowImpButton.Visible; }
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
					dtRul = (new AdminSystem()).GetClientRule(98,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtClientRule] = dtRul;
			}
			return dtRul;
		}

		private void SetClientRule(ListViewDataItem ee, bool isEditItem)
		{
			DataView dvRul = new DataView(GetClientRule());
			if (ee != null) {dvRul.RowFilter = "MasterTable = 'N'";} else {dvRul.RowFilter = "MasterTable <> 'N'";}
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
					if (ee == null || drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on" || (cAdmReleaseGrid.EditIndex > -1 && GetDataItemIndex(cAdmReleaseGrid.EditIndex) == ee.DataItemIndex))
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(98,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(98,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,98,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(98,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetScreenLabel(98,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(98,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(98,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(98,"GetExpAdmRelease98","Y",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(98,"GetExpAdmRelease98","N",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(98,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[4]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[5]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[6]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[11]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[14]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[16]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[17]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[17]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[18]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[19]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[20]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReleaseId191"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReleaseName191"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReleaseBuild191"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDate(drv["ReleaseDate191"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReleaseOs191"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["ReleaseOs191Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["EntityId191"].ToString() + (char)9 + drv["EntityId191Text"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["ReleaseTypeId191"].ToString() + (char)9 + drv["ReleaseTypeId191Text"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DeployPath199"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["TarScriptAft191"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReadMe191"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReleaseDtlId192"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ObjectType192"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["ObjectType192Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["RunOrder192"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SrcObject192"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SProcOnly192"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["SProcOnly192Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ObjectName192"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ObjectExempt192"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcClientTierId192"].ToString() + (char)9 + drv["SrcClientTierId192Text"].ToString() + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcRuleTierId192"].ToString() + (char)9 + drv["SrcRuleTierId192Text"].ToString() + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcDataTierId192"].ToString() + (char)9 + drv["SrcDataTierId192Text"].ToString() + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["TarDataTierId192"].ToString() + (char)9 + drv["TarDataTierId192Text"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DoSpEncrypt192"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmRelease.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmRelease.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(98,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReleaseId191"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ReleaseName191"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["ReleaseBuild191"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDate(drv["ReleaseDate191"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["ReleaseOs191Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["EntityId191Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["ReleaseTypeId191Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["DeployPath199"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["TarScriptAft191"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["ReadMe191"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReleaseDtlId192"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(drv["ObjectType192Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["RunOrder192"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcObject192"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(drv["SProcOnly192Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(drv["ObjectName192"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["ObjectExempt192"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcClientTierId192Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcRuleTierId192Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["SrcDataTierId192Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["TarDataTierId192Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["DoSpEncrypt192"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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

		private void GetPageInfo()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetLastPageInfo(98, base.LUser.UsrId, LcSysConnString, LcAppPw);
				cPgSize.Text = dt.Rows[0]["LastPageInfo"].ToString();
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 98, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (cAdmReleaseGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
			PopAdmRelease98List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("98", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("98", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("98", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("98", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("98", LcSysConnString, LcAppPw);
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
		            context["scr"] = "98";
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
		            context["scr"] = "98";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmRelease98" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmRelease98" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("98", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("98", "AdmRelease98", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
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
				dtScreenTab = (new AdminSystem()).GetScreenTab(98,base.LUser.CultureId,LcSysConnString,LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return dtScreenTab;
		}

		private void SetReleaseOs191(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtReleaseOs191];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseOs3S1703",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ReleaseOs191 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseOs3S1703",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseOs3S1703",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtReleaseOs191] = dv.Table;
				}
			}
		}

		private void SetEntityId191(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtEntityId191];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlEntityId3S1704",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "EntityId191 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlEntityId3S1704",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlEntityId3S1704",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtEntityId191] = dv.Table;
				}
			}
		}

		private void SetReleaseTypeId191(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtReleaseTypeId191];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseTypeId3S1705",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ReleaseTypeId191 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseTypeId3S1705",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlReleaseTypeId3S1705",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtReleaseTypeId191] = dv.Table;
				}
			}
		}

		private void SetObjectType192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtObjectType192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlObjectType3S1715",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ObjectType192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlObjectType3S1715",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlObjectType3S1715",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtObjectType192] = dv.Table;
				}
			}
		}

		private void SetSProcOnly192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtSProcOnly192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSProcOnly3S1722",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "SProcOnly192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSProcOnly3S1722",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSProcOnly3S1722",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtSProcOnly192] = dv.Table;
				}
			}
		}

		private void SetSrcClientTierId192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtSrcClientTierId192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcClientTierId3S1716",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "SrcClientTierId192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcClientTierId3S1716",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcClientTierId3S1716",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtSrcClientTierId192] = dv.Table;
				}
			}
		}

		private void SetSrcRuleTierId192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtSrcRuleTierId192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcRuleTierId3S1718",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "SrcRuleTierId192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcRuleTierId3S1718",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcRuleTierId3S1718",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtSrcRuleTierId192] = dv.Table;
				}
			}
		}

		private void SetSrcDataTierId192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtSrcDataTierId192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcDataTierId3S1720",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "SrcDataTierId192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcDataTierId3S1720",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlSrcDataTierId3S1720",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtSrcDataTierId192] = dv.Table;
				}
			}
		}

		private void SetTarDataTierId192(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtTarDataTierId192];
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
						dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlTarDataTierId3S1721",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReleaseId"))
					{
						ss = "(ReleaseId is null";
						if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReleaseId = " + cAdmRelease98List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "TarDataTierId192 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlTarDataTierId3S1721",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(98,"GetDdlTarDataTierId3S1721",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtTarDataTierId192] = dv.Table;
				}
			}
		}

		private DataView GetAdmRelease98List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRelease98List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(98,"GetLisAdmRelease98",true,"Y",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(98,"GetLisAdmRelease98",true,"N",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmRelease98List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmRelease98List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmRelease98";
			context["mKey"] = "ReleaseId191";
			context["mVal"] = "ReleaseId191Text";
			context["mTip"] = "ReleaseId191Text";
			context["mImg"] = "ReleaseId191Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "98";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "Y";
			context["conn"] = string.Empty;
			cAdmRelease98List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmRelease98List.DataContext = context;
			if (dv.Table == null) return;
			cAdmRelease98List.DataSource = dv;
			cAdmRelease98List.Visible = true;
			if (cAdmRelease98List.Items.Count <= 0) {cAdmRelease98List.Visible = false; cAdmRelease98List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmRelease98List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmRelease98List.Items[0].Selected = true; cAdmRelease98List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmRelease98List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmRelease98List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cReleaseId191, dtAuth.Rows[0], cReleaseId191P1, cReleaseId191Label, cReleaseId191P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cReleaseName191, dtAuth.Rows[1], cReleaseName191P1, cReleaseName191Label, cReleaseName191P2, null, dtLabel.Rows[1], cRFVReleaseName191, null, null);
				base.SetFoldBehavior(cReleaseBuild191, dtAuth.Rows[2], cReleaseBuild191P1, cReleaseBuild191Label, cReleaseBuild191P2, null, dtLabel.Rows[2], null, null, null);
				base.SetFoldBehavior(cReleaseDate191, dtAuth.Rows[3], cReleaseDate191P1, cReleaseDate191Label, cReleaseDate191P2, null, dtLabel.Rows[3], null, null, null);
				base.SetFoldBehavior(cReleaseOs191, dtAuth.Rows[4], cReleaseOs191P1, cReleaseOs191Label, cReleaseOs191P2, null, dtLabel.Rows[4], cRFVReleaseOs191, null, null);
				base.SetFoldBehavior(cEntityId191, dtAuth.Rows[5], cEntityId191P1, cEntityId191Label, cEntityId191P2, null, dtLabel.Rows[5], cRFVEntityId191, null, null);
				base.SetFoldBehavior(cReleaseTypeId191, dtAuth.Rows[6], cReleaseTypeId191P1, cReleaseTypeId191Label, cReleaseTypeId191P2, null, dtLabel.Rows[6], cRFVReleaseTypeId191, null, null);
				base.SetFoldBehavior(cDeployPath199, dtAuth.Rows[7], cDeployPath199P1, cDeployPath199Label, cDeployPath199P2, null, dtLabel.Rows[7], null, null, null);
				base.SetFoldBehavior(cTarScriptAft191, dtAuth.Rows[8], cTarScriptAft191P1, cTarScriptAft191Label, cTarScriptAft191P2, cTarScriptAft191E, null, dtLabel.Rows[8], null, null, null);
				cTarScriptAft191E.Attributes["label_id"] = cTarScriptAft191Label.ClientID; cTarScriptAft191E.Attributes["target_id"] = cTarScriptAft191.ClientID;
				base.SetFoldBehavior(cReadMe191, dtAuth.Rows[9], cReadMe191P1, cReadMe191Label, cReadMe191P2, cReadMe191E, null, dtLabel.Rows[9], null, null, null);
				cReadMe191E.Attributes["label_id"] = cReadMe191Label.ClientID; cReadMe191E.Attributes["target_id"] = cReadMe191.ClientID;
			}
			if ((cReleaseName191.Attributes["OnChange"] == null || cReleaseName191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReleaseName191.Visible && !cReleaseName191.ReadOnly) {cReleaseName191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cReleaseBuild191.Attributes["OnChange"] == null || cReleaseBuild191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReleaseBuild191.Visible && !cReleaseBuild191.ReadOnly) {cReleaseBuild191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cReleaseDate191.Attributes["OnChange"] == null || cReleaseDate191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReleaseDate191.Visible && !cReleaseDate191.ReadOnly) {cReleaseDate191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cReleaseOs191.Attributes["OnChange"] == null || cReleaseOs191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReleaseOs191.Visible && cReleaseOs191.Enabled) {cReleaseOs191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cEntityId191.Attributes["OnChange"] == null || cEntityId191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cEntityId191.Visible && cEntityId191.Enabled) {cEntityId191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cReleaseTypeId191.Attributes["OnChange"] == null || cReleaseTypeId191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReleaseTypeId191.Visible && cReleaseTypeId191.Enabled) {cReleaseTypeId191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDeployPath199.Attributes["OnChange"] == null || cDeployPath199.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDeployPath199.Visible && !cDeployPath199.ReadOnly) {cDeployPath199.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cTarScriptAft191.Attributes["OnChange"] == null || cTarScriptAft191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cTarScriptAft191.Visible && !cTarScriptAft191.ReadOnly) {cTarScriptAft191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cReadMe191.Attributes["OnChange"] == null || cReadMe191.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReadMe191.Visible && !cReadMe191.ReadOnly) {cReadMe191.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
					cShowImpButton.Visible = false;
				}
				else
				{
					cShowImpButton.Visible = dr["ViewOnly"].ToString() != "G";
					if ((bool)Session[KEY_bUndoAllVisible]) {cUndoAllButton.Visible = true; cUndoAllButton.Enabled = true;}
					if ((bool)Session[KEY_bUpdateVisible]) {cSaveButton.Visible = true; cSaveButton.Enabled = true;}
				}
				if ((dr["AllowAdd"].ToString() == "N" && dr["AllowUpd"].ToString() == "N") || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N")) { cInsRowButton.Visible = false; }
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
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (cAdmReleaseGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				cNewButton_Click(sender, new EventArgs());
			}
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (cAdmReleaseGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
				Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
				Session.Remove(KEY_dtReleaseOs191);
				Session.Remove(KEY_dtEntityId191);
				Session.Remove(KEY_dtReleaseTypeId191);
				Session.Remove(KEY_dtObjectType192);
				Session.Remove(KEY_dtSProcOnly192);
				Session.Remove(KEY_dtSrcClientTierId192);
				Session.Remove(KEY_dtSrcRuleTierId192);
				Session.Remove(KEY_dtSrcDataTierId192);
				Session.Remove(KEY_dtTarDataTierId192);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
			}
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cReleaseName191.Text = string.Empty;}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {cReleaseBuild191.Text = string.Empty;}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {cReleaseDate191.Text = string.Empty;}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {SetReleaseOs191(cReleaseOs191,string.Empty);}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {SetEntityId191(cEntityId191,string.Empty); cEntityId191_SelectedIndexChanged(cEntityId191, new EventArgs());}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {SetReleaseTypeId191(cReleaseTypeId191,string.Empty);}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cTarScriptAft191.Text = string.Empty;}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {cReadMe191.Text = string.Empty;}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cReleaseId191.Text = string.Empty;
			cReleaseName191.Text = string.Empty;
			cReleaseBuild191.Text = string.Empty;
			cReleaseDate191.Text = string.Empty;
			SetReleaseOs191(cReleaseOs191,string.Empty);
			SetEntityId191(cEntityId191,string.Empty); cEntityId191_SelectedIndexChanged(cEntityId191, new EventArgs());
			SetReleaseTypeId191(cReleaseTypeId191,string.Empty);

			cTarScriptAft191.Text = string.Empty;
			cReadMe191.Text = string.Empty;
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmRelease98List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmRelease98List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmRelease98List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmRelease98ById",cAdmRelease98List.SelectedValue,null,null);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmRelease98List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cReleaseId191.Text = RO.Common3.Utils.fmNumeric("0",dr["ReleaseId191"].ToString(),base.LUser.Culture);} catch {cReleaseId191.Text = string.Empty;}
					try {cReleaseName191.Text = dr["ReleaseName191"].ToString();} catch {cReleaseName191.Text = string.Empty;}
					try {cReleaseBuild191.Text = dr["ReleaseBuild191"].ToString();} catch {cReleaseBuild191.Text = string.Empty;}
					try {cReleaseDate191.Text = RO.Common3.Utils.fmLongDate(dr["ReleaseDate191"].ToString(),base.LUser.Culture);} catch {cReleaseDate191.Text = string.Empty;}
					SetReleaseOs191(cReleaseOs191,dr["ReleaseOs191"].ToString());
					SetEntityId191(cEntityId191,dr["EntityId191"].ToString()); cEntityId191_SelectedIndexChanged(cEntityId191, new EventArgs());
					SetReleaseTypeId191(cReleaseTypeId191,dr["ReleaseTypeId191"].ToString());
					try {cTarScriptAft191.Text = dr["TarScriptAft191"].ToString();} catch {cTarScriptAft191.Text = string.Empty;}
					try {cReadMe191.Text = dr["ReadMe191"].ToString();} catch {cReadMe191.Text = string.Empty;}
				}
			}
			cButPanel.DataBind(); if (!cSaveButton.Visible) { cInsRowButton.Visible = false; }
			DataTable dtAdmReleaseGrid = null;
			int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
			try
			{
				dtAdmReleaseGrid = (new AdminSystem()).GetDtlById(98,"GetAdmRelease98DtlById",cAdmRelease98List.SelectedValue,null,null,filterId,base.LImpr,base.LCurr);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (!dtAdmReleaseGrid.Columns.Contains("_NewRow")) { dtAdmReleaseGrid.Columns.Add("_NewRow"); }
			Session[KEY_dtAdmReleaseGrid] = dtAdmReleaseGrid;
			cAdmReleaseGrid.EditIndex = -1;
			cAdmReleaseGridDataPager.PageSize = Int16.Parse(cPgSize.Text); GotoPage(0);
			if (Session[KEY_lastSortCol] != null)
			{
				cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs((string)Session[KEY_lastSortExp], SortDirection.Ascending));
			}
			else if (dtAdmReleaseGrid.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dtAdmReleaseGrid.Rows.Count == 1))
			{
				cAdmReleaseGrid_DataBind(dtAdmReleaseGrid.DefaultView);
			}
			cFindButton_Click(sender, e);
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false; Session.Remove(KEY_scrImport);
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmRelease98List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cEntityId191_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cEntityId191.Items.Count > 0 && Session[KEY_dtEntityId191] != null)
			{
				DataView dv = ((DataTable)Session[KEY_dtEntityId191]).DefaultView; dv.RowFilter = string.Empty;
				DataRowView dr = cEntityId191.SelectedIndex >= 0 && cEntityId191.SelectedIndex < dv.Count ? dv[cEntityId191.SelectedIndex] : dv[0];
				cDeployPath199.Text = dr["DeployPath199"].ToString();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		public void cFindButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				WebControl cc = sender as WebControl;
				if ((cc != null && cc.ID.Equals("cAdmRelease98List")) || cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(cAdmReleaseGrid, new CommandEventArgs("Save", "")))
				{
					string rf = string.Empty;
					if (cFind.Text != string.Empty) { rf = "(" + base.GetExpression(cFind.Text.Trim(), GetAuthCol(), 10, cFindFilter.SelectedValue) + ")"; }
					if (rf != string.Empty) { rf = "((" + rf + " or _NewRow = 'Y' ))"; }
					dv.RowFilter = rf;
					ViewState["_RowFilter"] = rf;
					GotoPage(0); cAdmReleaseGrid_DataBind(dv);
					if (GetCurrPageIndex() != (int)Session[KEY_currPageIndex] && (int)Session[KEY_currPageIndex] < GetTotalPages()) { GotoPage((int)Session[KEY_currPageIndex]); cAdmReleaseGrid_DataBind(dv);}
					try {ScriptManager.GetCurrent(Parent.Page).SetFocus(((TextBox)sender).ClientID);} catch {}
				}
			}
			grdCount.InnerText = "(" + RO.Common3.Utils.fmNumeric("0",dv.Count.ToString(),base.LUser.Culture) + " found)";
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cFirstButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(0);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cLastButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetTotalPages() - 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cNextButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetCurrPageIndex() + 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPrevButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetCurrPageIndex() - 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cInsRowButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (dt != null && (ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")))
			{
				DataRow dr = dt.NewRow();
				int? sortorder = null;
				if (dt.Columns.Contains("_SortOrder"))
				{
				    try
				    {
				        sortorder = 1;
				    } catch {};
				}
				dt.Rows.InsertAt(dr, 0);
				dt.Rows[0]["_NewRow"] = "Y";
				if (sortorder.HasValue) dr["_SortOrder"] = sortorder;
				Session[KEY_lastAddedRow] = 0; Session[KEY_dtAdmReleaseGrid] = dt; Session[KEY_currPageIndex] = 0; GotoPage(0);
				cAdmReleaseGrid_DataBind(dt.DefaultView);
				cAdmReleaseGrid_OnItemEditing(cAdmReleaseGrid, new ListViewEditEventArgs(0));
				// *** Default Value (Grid) Web Rule starts here *** //
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPgSizeButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (cAdmReleaseGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				try {if (int.Parse(cPgSize.Text) < 1 || int.Parse(cPgSize.Text) > 200) {cPgSize.Text = "10";}} catch {cPgSize.Text = "10";}
				cAdmReleaseGridDataPager.PageSize = int.Parse(cPgSize.Text); GotoPage(0);
				if (dt.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dt.Rows.Count == 1)) {cAdmReleaseGrid_DataBind(dt.DefaultView);} else {cFindButton_Click(sender, e);}
				try
				{
					(new AdminSystem()).UpdLastPageInfo(98, base.LUser.UsrId, cPgSize.Text, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
		}

		public void cGoto_TextChanged(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			try {GotoPage(Convert.ToInt32(cGoto.Text) - 1);} catch {} finally {cGoto.Text = (GetCurrPageIndex() + 1).ToString();}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cHideImpButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			cImport.Visible = false; cHideImpButton.Visible = false; cShowImpButton.Visible = (bool)Session[KEY_bShImpVisible];
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false;
			if ((cShowImpButton.Attributes["OnClick"] == null || cShowImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cShowImpButton.Visible) {cShowImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cShowImpButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			cImport.Visible = true; cShowImpButton.Visible = false; cHideImpButton.Visible = (bool)Session[KEY_bHiImpVisible];
			cNaviPanel.Visible = false; cImportPwdPanel.Visible = false;
			if ((cImportButton.Attributes["OnClick"] == null || cImportButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cImportButton.Visible) {cImportButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cHideImpButton.Attributes["OnClick"] == null || cHideImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cHideImpButton.Visible) {cHideImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSchemaImage.Visible)
			{
				try
				{
					Session["ImportSchema"] = (new AdminSystem()).GetSchemaScrImp(98,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "SearchLink('ImportSchema.aspx?scm=S&key=98&csy=3','','',''); return false;";}
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void ScrImportDdl(DataView dvd, DataRowView drv, bool bComboBox, string PKey, string CKey, string CNam, int MaxLen, string MatchCd, bool bAllowNulls)
		{
			if (dvd != null)
			{
				if (dvd.Table.Columns.Contains(PKey))
				{
					if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue))
					{
						dvd.RowFilter = "(" + PKey + " is null)";
					}
					else
					{
						dvd.RowFilter = "(" + PKey + " is null OR " + PKey + " = " + cAdmRelease98List.SelectedValue + ")";
					}
				}
				bool bFound = false; bool bUnique = true;
				if (!string.IsNullOrEmpty(drv[CKey].ToString().Trim()))   // Always do this first.
				{
					foreach (DataRowView drvd in dvd)
					{
						if (drvd[0].ToString().Trim() == drv[CKey].ToString().Trim()) {drv[CNam] = drvd[1].ToString(); bFound = true; break;}
					}
				}
				if (!string.IsNullOrEmpty(drv[CNam].ToString().Trim()))
				{
					if (drv[CNam].ToString().Length > MaxLen) { drv[CNam] = drv[CNam].ToString().Substring(0,MaxLen); }
					string cnam = drv[CNam].ToString(); object ckey = drv[CKey];	// Saved for reverse later.
					if (!bFound && "2,3,4".IndexOf(MatchCd) >= 0)  // Exact match.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower() == cnam.Trim().ToLower())
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!bFound && "3,4".IndexOf(MatchCd) >= 0)  // StartsWith.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower().StartsWith(cnam.Trim().ToLower()))
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!bFound && "4".IndexOf(MatchCd) >= 0)  // Wild search.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower().IndexOf(cnam.Trim().ToLower()) >= 0)
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!(bFound && bUnique) && MatchCd != "1") {drv[CNam] = "Invalid>" + drv[CNam].ToString(); bErrNow.Value = "Y"; PreMsgPopup("Import has invalid data, please check for \"Invalid>\", rectify and try again.");}
				}
			}
		}

		protected void ScrImport(bool bClear)
		{
			int iRow;
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false;
			DataTable dti = (DataTable)Session[KEY_scrImport];
			DataTable dtAdmReleaseGrid = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (dti != null && dtAdmReleaseGrid != null)
			{
				if (bClear)
				{
					dtAdmReleaseGrid.Rows.Clear();
				}
				cFind.Text = string.Empty;
				int iExist = dtAdmReleaseGrid.Rows.Count;
				// Validate dropdown, combobox, etc.
				int jj = 0;
				foreach (DataRowView drv in dti.DefaultView)
				{
					if ((drv.Row.RowState == System.Data.DataRowState.Added || drv.Row.RowState == System.Data.DataRowState.Detached) && jj >= iExist)
					{
						DataTable dtd;
						dtd = (DataTable)Session[KEY_dtObjectType192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlObjectType3S1715",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtObjectType192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "ObjectType192", "ObjectType192Text", 50, "2", false);
						dtd = (DataTable)Session[KEY_dtSProcOnly192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlSProcOnly3S1722",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtSProcOnly192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "SProcOnly192", "SProcOnly192Text", 50, "2", false);
						dtd = (DataTable)Session[KEY_dtSrcClientTierId192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlSrcClientTierId3S1716",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtSrcClientTierId192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "SrcClientTierId192", "SrcClientTierId192Text", 50, "2", true);
						dtd = (DataTable)Session[KEY_dtSrcRuleTierId192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlSrcRuleTierId3S1718",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtSrcRuleTierId192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "SrcRuleTierId192", "SrcRuleTierId192Text", 50, "2", true);
						dtd = (DataTable)Session[KEY_dtSrcDataTierId192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlSrcDataTierId3S1720",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtSrcDataTierId192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "SrcDataTierId192", "SrcDataTierId192Text", 50, "2", true);
						dtd = (DataTable)Session[KEY_dtTarDataTierId192];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(98,"GetDdlTarDataTierId3S1721",true,true,0,string.Empty,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtTarDataTierId192] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "ReleaseId", "TarDataTierId192", "TarDataTierId192Text", 50, "2", true);
					}
					jj = jj + 1;
				}
				iRow = dti.DefaultView.Count - iExist;
				if (!dti.Columns.Contains("_NewRow")) { dti.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmReleaseGrid] = dti;
				cAdmReleaseGrid_DataBind(dti.DefaultView);
				string msg = iRow.ToString() + " rows from selected source imported successfully. " + Session[KEY_cntImpPwdOvride].ToString() + " rows validated by Password Override. Please press Save button to save to database.";
				if (Session["CtrlAcctList"] != null)
				{
					System.Collections.Hashtable ht = (System.Collections.Hashtable) Session["CtrlAcctList"];
					string ss = string.Empty;
					System.Collections.IDictionaryEnumerator de = ht.GetEnumerator();
					while ( de.MoveNext() )
					{
						ss = ss + de.Key + " (" + de.Value.ToString() + "), ";
					}
					msg = msg + " List of Password Override items: " + ss.Remove(ss.Length - 2, 2) + ".";
				}
				Session.Remove(KEY_scrImport); Session.Remove("CtrlAcctList");
				cImportPwd.Text = string.Empty;
				ShowDirty(true);
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0;
				bInfoNow.Value = "Y"; PreMsgPopup(msg);
			}
		}

		public void cContinueButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			ScrImport(false);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		protected void cBrowseButton_Click(object sender, EventArgs e)
		{
			if (cBrowse.HasFile && cBrowse.PostedFile.FileName != string.Empty)
			{
				string fileAndPath = cBrowse.PostedFile.FileName;
				string fNameO = string.Empty;
				try {
				    foreach (var c in Path.GetInvalidPathChars()) { fileAndPath = fileAndPath.Replace(c, '_'); }
				    fNameO = Path.GetFileName(fileAndPath);
				}
				catch (Exception err) {
				    bErrNow.Value = "Y"; PreMsgPopup("Invalid characters in file and/or path \"" + fileAndPath + "\": " + err.Message);
				    return;
				}
				try {
					string fName;
					if (fNameO.LastIndexOf(".") >= 0)
					{
						fName = fNameO.Insert(fNameO.LastIndexOf("."), "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
					}
					else
					{
						fName = fNameO + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
					}
					fName = fName.Replace(":","").Replace("..","");
					if (!Directory.Exists(Config.PathTmpImport)) { Directory.CreateDirectory(Config.PathTmpImport); }
					cBrowse.PostedFile.SaveAs(Config.PathTmpImport + fName);
					cWorkSheet.DataSource = (new XLSImport()).GetSheetNames(Config.PathTmpImport + fName); cWorkSheet.DataBind();
					cFNameO.Text = fNameO; cFName.Text = fName;
				}
				catch (Exception err) {bErrNow.Value = "Y"; PreMsgPopup("Unable to retrieve sheet names from \"" + fNameO + "\": " + err.Message); }
			}
		}

		public void cImportButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			if (cFNameO.Text != string.Empty && cWorkSheet.Items.Count > 0 && cWorkSheet.SelectedItem.Text != string.Empty && cStartRow.Text != string.Empty)
			{
				DataTable dtAdmReleaseGrid = (DataTable)Session[KEY_dtAdmReleaseGrid];
				try
				{
					if (cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(cAdmReleaseGrid, new CommandEventArgs("Save", "")))
					{
						Session[KEY_scrImport] = ImportFile(dtAdmReleaseGrid.Copy(),cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
					}
					cHideImpButton_Click(sender,new EventArgs());
				}
				catch (Exception err) {bErrNow.Value = "Y"; PreMsgPopup("Error in spreadsheet \"" + cFNameO.Text + "\":<br>" + err.Message); return; }
				finally {cWorkSheet.Items.Clear();}
				ScrImport(false);
			}
			else
			{
				bInfoNow.Value = "Y"; PreMsgPopup("Please select a spreadsheet, then a worksheet, indicate the starting row and try again.");
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private DataTable ImportFile(DataTable dt, string fileName, string workSheet, string startRow, string fileFullName)
		{
			try
			{
				DataTable dtImp = RO.Common3.XmlUtils.XmlToDataSet((new XLSImport()).ImportFile(fileName,workSheet,startRow,fileFullName)).Tables[0];
				DataRowCollection rows = dtImp.Rows;
				DataColumnCollection cols = dt.Columns;
				Func<string, string, bool> isDateCol = (cl, c) => ("," + cl + ",").IndexOf("," + c + ",") >= 0;
				Func<string, string, bool> isDateUTCCol = (cl, c) => ("," + cl + ",").IndexOf("," + c + ",") >= 0;
				string ss;
				int iStart = int.Parse(startRow) - 1;
				int idv = dt.Rows.Count;
				int idel = 0;
				System.Collections.Generic.List<string> ErrLst = new System.Collections.Generic.List<string>();
				bool bHasErr = false;
				for ( int iRow = iStart; iRow < rows.Count; iRow++ )
				{
					if (rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty && rows[iRow][5].ToString() == string.Empty && rows[iRow][6].ToString() == string.Empty && rows[iRow][7].ToString() == string.Empty && rows[iRow][17].ToString() == string.Empty) {idel = idel + 1;}
					else
					{
						dt.Rows.Add(dt.NewRow());
						for ( int iCol = 0; iCol < cols.Count; iCol++ )
						{
							try { ss = rows[iRow][iCol].ToString().Trim(); } catch { ss = string.Empty; }
							try {
							    if (!string.IsNullOrEmpty(ss)) {
							        dt.Rows[iRow - iStart - idel + idv][iCol] = isDateCol("", iCol.ToString()) ? base.ToIntDateTime(ss, isDateUTCCol("", iCol.ToString()), true) : ss;
							    }
							}
							catch (Exception ex) {
							    if (ss.EndsWith("%")) {
							        try { dt.Rows[iRow - iStart - idel + idv][iCol] = ss.Left(ss.Length - 1); } catch { }
							    }
							    else {
							        bHasErr = true;
							        ErrLst.Add("Row " + (iRow + 1).ToString() + " Col " + Utils.Num2ExcelCol(iCol + 1) + ": " + Server.HtmlEncode(ex.Message));
							    }
							}
						}
					}
				}
				if (bHasErr) { throw new Exception(string.Join("<br>", ErrLst)); }
			}
			catch(Exception e) { throw (e); }
			return dt;
		}

		private void GotoPage(int pageNo)
		{
			if (pageNo >= 0 && pageNo < GetTotalPages())
			{
				if (cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(cAdmReleaseGrid, new CommandEventArgs("Save", "")))
				{
					try { cAdmReleaseGridDataPager.SetPageProperties(cAdmReleaseGridDataPager.PageSize * pageNo, cAdmReleaseGridDataPager.MaximumRows, false); cAdmReleaseGrid_DataBind(null); }
					catch
					{
						try { cAdmReleaseGridDataPager.SetPageProperties(0, cAdmReleaseGridDataPager.MaximumRows, false); cAdmReleaseGrid_DataBind(null); } catch {}
					}
				}
			}
		}

		protected void cAdmReleaseGrid_OnItemCommand(object sender, ListViewCommandEventArgs e)
		{
		}

		private void cAdmReleaseGrid_DataBind(DataView dvAdmReleaseGrid)
		{
			if (dvAdmReleaseGrid == null)
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
				dvAdmReleaseGrid = dt != null ? dt.DefaultView : null;
			}
			if (dvAdmReleaseGrid != null)
			{
				LcAuth = GetAuthCol().AsEnumerable().ToDictionary<DataRow,string>(dr=>dr["ColName"].ToString());
				cAdmReleaseGrid.DataSource = dvAdmReleaseGrid;
				cAdmReleaseGrid.DataBind();
				int totalPages = GetTotalPages(); if (totalPages <= 0) { totalPages = 1; }
				cPageNoLabel.Text = " of " + totalPages.ToString(); cGoto.Text = (GetCurrPageIndex() + 1).ToString();
			}
			if (cFindFilter.Items.Count <= 0)
			{
				DataTable dtAuth = GetAuthCol();
				DataTable dtLabel = GetLabel();
				int ii = 0;
				ListItem li = new ListItem();
				li.Value = string.Empty; li.Text = "All";
				cFindFilter.Items.Add(li);
				foreach (DataRow dr in dtLabel.Rows)
				{
					if (ii >= 10 && !string.IsNullOrEmpty(dr["ColumnHeader"].ToString()) && !string.IsNullOrEmpty(dr["TableId"].ToString()) && dtAuth.Rows[ii]["ColVisible"].ToString() == "Y")
					{
						li = new ListItem();
						li.Value = ii.ToString(); li.Text = dr["ColumnHeader"].ToString().Replace("*", string.Empty);
						cFindFilter.Items.Add(li);
					}
					ii = ii + 1;
				}
			}
		}

		protected void cAdmReleaseGrid_OnSorting(object sender, ListViewSortEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(cAdmReleaseGrid, new CommandEventArgs("Save", "")))
				{
					string ftr = string.Empty;
					if (!dv.Table.Columns.Contains("_SortOrder")) { dv.Table.Columns.Add("_SortOrder"); }
					if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
					if (Session[KEY_lastSortUrl] == null)   // First time.
					{
					    dv.Sort = "_SortOrder DESC, " + e.SortExpression; Session[KEY_lastSortUrl] = "~/images/ArrowUp.png"; Session[KEY_lastSortTog] = "N";
					}
					else if ((string)Session[KEY_lastSortTog] == "Y")
					{
					    if (((string)Session[KEY_lastSortUrl]).IndexOf("ArrowDn") >= 0)
					    {
							dv.Sort = "_SortOrder DESC, " + e.SortExpression; Session[KEY_lastSortUrl] = "~/images/ArrowUp.png";
					    }
					    else
					    {
							dv.Sort = "_SortOrder DESC, " + e.SortExpression + " DESC"; Session[KEY_lastSortUrl] = "~/images/ArrowDn.png";
					    }
					    Session[KEY_lastSortTog] = "N";
					}
					else if (((string)Session[KEY_lastSortUrl]).IndexOf("ArrowDn") >= 0) { dv.Sort = "_SortOrder DESC, " + e.SortExpression + " DESC"; } else { dv.Sort = "_SortOrder DESC, " + e.SortExpression; }
					ViewState["_SortColumn"] = dv.Sort;
					if (ftr != string.Empty) { dv.RowFilter = ftr; }
					Session[KEY_dtAdmReleaseGrid] = dt;
					cAdmReleaseGrid_DataBind(dv);
				}
			}
		}

		protected void cAdmReleaseGrid_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
		    cAdmReleaseGridDataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
		    DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
		    DataView dv = dt != null ? dt.DefaultView : null;
			cAdmReleaseGrid_DataBind(dv); cAdmReleaseGrid.EditIndex = -1;
		}

		private void GridChkPgDirty(ListViewItem lvi)
		{
				WebControl cc = null; System.Web.UI.WebControls.Image ib = null;
				cc = ((WebControl)lvi.FindControl("cObjectType192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cRunOrder192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cSrcObject192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cSProcOnly192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cObjectName192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				ib = ((System.Web.UI.WebControls.Image)lvi.FindControl("cObjectName192E"));
				ib.Attributes["target_id"] = cc.ClientID;
				cc = ((WebControl)lvi.FindControl("cObjectExempt192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				ib = ((System.Web.UI.WebControls.Image)lvi.FindControl("cObjectExempt192E"));
				ib.Attributes["target_id"] = cc.ClientID;
				cc = ((WebControl)lvi.FindControl("cSrcClientTierId192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cSrcRuleTierId192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cSrcDataTierId192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cTarDataTierId192"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDoSpEncrypt192"));
				if ((cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
		}

		protected void cAdmReleaseGrid_OnItemDataBound(object sender, ListViewItemEventArgs e)
		{
			// *** GridItemDataBound (before) Web Rule End *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			bool isEditItem = false;
			bool isImage = true;
			bool hasImageContent = false;
			DataView dvAdmReleaseGrid = dt != null ? dt.DefaultView : null;
			if (cAdmReleaseGrid.EditIndex > -1 && GetDataItemIndex(cAdmReleaseGrid.EditIndex) == e.Item.DataItemIndex)
			{
				isEditItem = true;
				base.SetGridEnabled(e.Item, GetAuthCol(), GetLabel(), 10);
				SetObjectType192((DropDownList)e.Item.FindControl("cObjectType192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["ObjectType192"].ToString());
				SetSProcOnly192((DropDownList)e.Item.FindControl("cSProcOnly192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["SProcOnly192"].ToString());
				SetSrcClientTierId192((DropDownList)e.Item.FindControl("cSrcClientTierId192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["SrcClientTierId192"].ToString());
				SetSrcRuleTierId192((DropDownList)e.Item.FindControl("cSrcRuleTierId192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["SrcRuleTierId192"].ToString());
				SetSrcDataTierId192((DropDownList)e.Item.FindControl("cSrcDataTierId192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["SrcDataTierId192"].ToString());
				SetTarDataTierId192((DropDownList)e.Item.FindControl("cTarDataTierId192"),dvAdmReleaseGrid[e.Item.DataItemIndex]["TarDataTierId192"].ToString());
				CheckBox cb = null;
				cb = (CheckBox)e.Item.FindControl("cDoSpEncrypt192");
				if (cb != null)
				{
					cb.Checked = base.GetBool(dvAdmReleaseGrid[e.Item.DataItemIndex]["DoSpEncrypt192"].ToString());
				}
				GridChkPgDirty(e.Item);
			}
			SetClientRule((ListViewDataItem) e.Item,isEditItem);
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
			    Control cc = null;
				if (!CanAct('S') || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cc = e.Item.FindControl("cAdmReleaseGridDelete"); if (cc != null) { cc.Visible = false; }
					cc = e.Item.FindControl("cAdmReleaseGridEdit"); if (cc != null) { cc.Visible = false; }
				}
				else
				{
			        HtmlTableRow tr = e.Item.FindControl("cAdmReleaseGridRow") as HtmlTableRow;
			        LinkButton lb = e.Item.FindControl("cAdmReleaseGridEdit") as LinkButton;
			        if (tr != null && lb != null) { SetDefaultCtrl(tr, lb, string.Empty); }
				}
			}
			if (cAdmReleaseGrid.EditIndex > -1 && GetDataItemIndex(cAdmReleaseGrid.EditIndex) == e.Item.DataItemIndex)
			{
			}
			else
			{
			}
			// *** GridItemDataBound (after) Web Rule End *** //
		}

		protected void cReleaseDtlId192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "0") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "0"; Session[KEY_lastSortExp] = "ReleaseDtlId192";Session[KEY_lastSortImg] = "cReleaseDtlId192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("ReleaseDtlId192", SortDirection.Ascending));
		}

		protected void cObjectType192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "1") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "1"; Session[KEY_lastSortExp] = "ObjectType192Text";Session[KEY_lastSortImg] = "cObjectType192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("ObjectType192Text", SortDirection.Ascending));
		}

		protected void cRunOrder192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "2") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "2"; Session[KEY_lastSortExp] = "RunOrder192";Session[KEY_lastSortImg] = "cRunOrder192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("RunOrder192", SortDirection.Ascending));
		}

		protected void cSrcObject192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "3") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "3"; Session[KEY_lastSortExp] = "SrcObject192";Session[KEY_lastSortImg] = "cSrcObject192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("SrcObject192", SortDirection.Ascending));
		}

		protected void cSProcOnly192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "4") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "4"; Session[KEY_lastSortExp] = "SProcOnly192Text";Session[KEY_lastSortImg] = "cSProcOnly192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("SProcOnly192Text", SortDirection.Ascending));
		}

		protected void cObjectName192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "5") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "5"; Session[KEY_lastSortExp] = "ObjectName192";Session[KEY_lastSortImg] = "cObjectName192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("ObjectName192", SortDirection.Ascending));
		}

		protected void cObjectExempt192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "6") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "6"; Session[KEY_lastSortExp] = "ObjectExempt192";Session[KEY_lastSortImg] = "cObjectExempt192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("ObjectExempt192", SortDirection.Ascending));
		}

		protected void cSrcClientTierId192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "7") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "7"; Session[KEY_lastSortExp] = "SrcClientTierId192Text";Session[KEY_lastSortImg] = "cSrcClientTierId192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("SrcClientTierId192Text", SortDirection.Ascending));
		}

		protected void cSrcRuleTierId192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "8") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "8"; Session[KEY_lastSortExp] = "SrcRuleTierId192Text";Session[KEY_lastSortImg] = "cSrcRuleTierId192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("SrcRuleTierId192Text", SortDirection.Ascending));
		}

		protected void cSrcDataTierId192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "9") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "9"; Session[KEY_lastSortExp] = "SrcDataTierId192Text";Session[KEY_lastSortImg] = "cSrcDataTierId192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("SrcDataTierId192Text", SortDirection.Ascending));
		}

		protected void cTarDataTierId192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "10") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "10"; Session[KEY_lastSortExp] = "TarDataTierId192Text";Session[KEY_lastSortImg] = "cTarDataTierId192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("TarDataTierId192Text", SortDirection.Ascending));
		}

		protected void cDoSpEncrypt192hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "11") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "11"; Session[KEY_lastSortExp] = "DoSpEncrypt192";Session[KEY_lastSortImg] = "cDoSpEncrypt192hi";
			cAdmReleaseGrid_OnSorting(sender, new ListViewSortEventArgs("DoSpEncrypt192", SortDirection.Ascending));
		}

		protected void cAdmReleaseGrid_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if ((ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")) && dt != null && dt.DefaultView.Count > GetDataItemIndex(e.NewEditIndex))
			{
				cAdmReleaseGrid.EditIndex = e.NewEditIndex;
				cAdmReleaseGrid_DataBind(null);
				Control wc = null;
				string idx = Page.Request["__EVENTARGUMENT"];
				if (!string.IsNullOrEmpty(idx))
				{
				    Control ctrlToFocus = cAdmReleaseGrid.EditItem.FindControl("c" + idx);
				    if (((WebControl)ctrlToFocus).Enabled) { wc = ctrlToFocus; }
				}
				if (wc == null) { wc = FindEditableControl(cAdmReleaseGrid.EditItem); }
				if (wc != null)
             {
                 if (wc is RoboCoder.WebControls.ComboBox)
                 {
                     if (isMobile.Text != "isMobile") ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)wc).FocusID);
                 }
                 else if (wc is CheckBox && ("c" + idx) == wc.ID)
                 {
                     ((CheckBox)wc).Checked = !((CheckBox)wc).Checked; ShowDirty(true);
                 }
                 else
                 {
                     if (isMobile.Text != "isMobile") ScriptManager.GetCurrent(Parent.Page).SetFocus(wc.ClientID);
                 }
             }
             MkMobileLabel(cAdmReleaseGrid.EditItem);
		    }
		    else { e.Cancel = true; }
		}

		private void MkMobileLabel(ListViewItem lvi)
		{
		    Label ml = null;
		    ml = lvi.FindControl("cReleaseDtlId192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(10); }
		    ml = lvi.FindControl("cObjectType192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(11); }
		    ml = lvi.FindControl("cRunOrder192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(12); }
		    ml = lvi.FindControl("cSrcObject192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(13); }
		    ml = lvi.FindControl("cSProcOnly192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(14); }
		    ml = lvi.FindControl("cObjectName192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(15); }
		    ml = lvi.FindControl("cObjectExempt192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(16); }
		    ml = lvi.FindControl("cSrcClientTierId192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(17); }
		    ml = lvi.FindControl("cSrcRuleTierId192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(18); }
		    ml = lvi.FindControl("cSrcDataTierId192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(19); }
		    ml = lvi.FindControl("cTarDataTierId192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(20); }
		    ml = lvi.FindControl("cDoSpEncrypt192ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(21); }
		}

		protected void GridFill(ListViewItem lvi, DataTable dt, DataRow dr, bool bInsert)
		{
			DropDownList ddl = null;
			ddl = (DropDownList)lvi.FindControl("cObjectType192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["ObjectType192"] = ddl.SelectedValue;
					dr["ObjectType192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["ObjectType192"] = System.DBNull.Value;
					dr["ObjectType192Text"] = System.DBNull.Value;
				}
			}
			TextBox tb = null;
			tb = (TextBox)lvi.FindControl("cRunOrder192");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["RunOrder192"] = tb.Text;} else {dr["RunOrder192"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cSrcObject192");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["SrcObject192"] = tb.Text;} else {dr["SrcObject192"] = System.DBNull.Value;}
			}
			ddl = (DropDownList)lvi.FindControl("cSProcOnly192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["SProcOnly192"] = ddl.SelectedValue;
					dr["SProcOnly192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["SProcOnly192"] = System.DBNull.Value;
					dr["SProcOnly192Text"] = System.DBNull.Value;
				}
			}
			tb = (TextBox)lvi.FindControl("cObjectName192");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ObjectName192"] = tb.Text;} else {dr["ObjectName192"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cObjectExempt192");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ObjectExempt192"] = tb.Text;} else {dr["ObjectExempt192"] = System.DBNull.Value;}
			}
			ddl = (DropDownList)lvi.FindControl("cSrcClientTierId192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["SrcClientTierId192"] = ddl.SelectedValue;
					dr["SrcClientTierId192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["SrcClientTierId192"] = System.DBNull.Value;
					dr["SrcClientTierId192Text"] = System.DBNull.Value;
				}
			}
			ddl = (DropDownList)lvi.FindControl("cSrcRuleTierId192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["SrcRuleTierId192"] = ddl.SelectedValue;
					dr["SrcRuleTierId192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["SrcRuleTierId192"] = System.DBNull.Value;
					dr["SrcRuleTierId192Text"] = System.DBNull.Value;
				}
			}
			ddl = (DropDownList)lvi.FindControl("cSrcDataTierId192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["SrcDataTierId192"] = ddl.SelectedValue;
					dr["SrcDataTierId192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["SrcDataTierId192"] = System.DBNull.Value;
					dr["SrcDataTierId192Text"] = System.DBNull.Value;
				}
			}
			ddl = (DropDownList)lvi.FindControl("cTarDataTierId192");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["TarDataTierId192"] = ddl.SelectedValue;
					dr["TarDataTierId192Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["TarDataTierId192"] = System.DBNull.Value;
					dr["TarDataTierId192Text"] = System.DBNull.Value;
				}
			}
			CheckBox cb = null;
			cb = (CheckBox)lvi.FindControl("cDoSpEncrypt192");
			if (cb != null)
			{
				dr["DoSpEncrypt192"] = base.SetBool(cb.Checked);
			}
		    if (bInsert) { dt.Rows.InsertAt(dr, 0); }
			Session[KEY_dtAdmReleaseGrid] = dt;
			cAdmReleaseGrid.EditIndex = -1;
			cAdmReleaseGrid_DataBind(dt.DefaultView);
		}

		protected void cAdmReleaseGrid_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			if (dt != null && dt.Rows.Count > 0)
			{
				DataRow dr = dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row;
			    GridFill(cAdmReleaseGrid.EditItem, dt, dr, false);
			    dr["_NewRow"] = "N";
			    if (dt.Columns.Contains("_SortOrder")) dr["_SortOrder"] = null;
			}
		}

		protected void cAdmReleaseGrid_OnItemCanceling(object sender, ListViewCancelEventArgs e)
		{
			PanelTop.Update();
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && dv[GetDataItemIndex(e.ItemIndex)]["_NewRow"].ToString() == "Y")
			{
				cAdmReleaseGrid_OnItemDeleting(sender, new ListViewDeleteEventArgs(e.ItemIndex));
			}
			if (dt.Columns.Contains("_SortOrder")) dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row["_SortOrder"] = null;
			cAdmReleaseGrid.EditIndex = -1; cAdmReleaseGrid_DataBind(null);
		}

		protected void cDeleteAllButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && (cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				GotoPage(0);
				while (dv.Count > 0) {dv.Delete(0);}
				Session[KEY_dtAdmReleaseGrid] = dt;
				cAdmReleaseGrid.EditIndex = -1; cAdmReleaseGrid_DataBind(dv);
			}
		}

		protected void cAdmReleaseGrid_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			DataRow dr = dv != null ? dv[GetDataItemIndex(e.ItemIndex)].Row : null;
			if (dv != null && (cAdmReleaseGrid.EditIndex == e.ItemIndex || UpdateGridRow(sender, new CommandEventArgs("Save",""))))
			{
				// *** Delete Grid Row (before) Web Rule End *** //
				dr.Delete();
				Session[KEY_dtAdmReleaseGrid] = dt;
				cAdmReleaseGrid.EditIndex = -1; cAdmReleaseGrid_DataBind(dv);
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
			cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
			PopAdmRelease98List(sender, e, false, null);
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cClearButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			ClearMaster(sender, e);
			cDeleteAllButton_Click(sender, new EventArgs());
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cCopyButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			cReleaseId191.Text = string.Empty;
			cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
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
			if (cReleaseId191.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmRelease98List(sender, e, false, cReleaseId191.Text); }
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
			Session.Remove(KEY_dtAdmRelease98List); PopAdmRelease98List(sender, e, false, null);
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
			if (ValidPage() && (cAdmReleaseGrid.EditIndex < 0 || UpdateGridRow(cAdmReleaseGrid, new CommandEventArgs("Save", ""))))
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmReleaseGrid];
				DataView dv = dt != null ? dt.DefaultView : null;
				string ftr = string.Empty;
				if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
				AdmRelease98 ds = PrepAdmReleaseData(dv,cReleaseId191.Text == string.Empty);
				if (ftr != string.Empty) {dv.RowFilter = ftr;}
				if (string.IsNullOrEmpty(cAdmRelease98List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(98,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmRelease98List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(98,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
						Session[KEY_currPageIndex] = GetCurrPageIndex();
						ShowDirty(false); PopAdmRelease98List(sender, e, false, ds.Tables["AdmRelease"].Rows[0]["ReleaseId191"]);
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
			if (cReleaseId191.Text != string.Empty)
			{
				AdmRelease98 ds = PrepAdmReleaseData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(98,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc))
					{
						cAdmRelease98List.ClearSearch(); Session.Remove(KEY_dtAdmRelease98List);
						ShowDirty(false); PopAdmRelease98List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmRelease98 PrepAdmReleaseData(DataView dv, bool bAdd)
		{
			AdmRelease98 ds = new AdmRelease98();
			DataRow dr = ds.Tables["AdmRelease"].NewRow();
			DataRow drType = ds.Tables["AdmRelease"].NewRow();
			DataRow drDisp = ds.Tables["AdmRelease"].NewRow();
			if (bAdd) { dr["ReleaseId191"] = string.Empty; } else { dr["ReleaseId191"] = cReleaseId191.Text; }
			drType["ReleaseId191"] = "Numeric"; drDisp["ReleaseId191"] = "TextBox";
			try {dr["ReleaseName191"] = cReleaseName191.Text.Trim();} catch {}
			drType["ReleaseName191"] = "VarWChar"; drDisp["ReleaseName191"] = "TextBox";
			try {dr["ReleaseBuild191"] = cReleaseBuild191.Text.Trim();} catch {}
			drType["ReleaseBuild191"] = "VarChar"; drDisp["ReleaseBuild191"] = "TextBox";
			try {dr["ReleaseDate191"] = cReleaseDate191.Text;} catch {}
			drType["ReleaseDate191"] = "DBDate"; drDisp["ReleaseDate191"] = "LongDate";
			try {dr["ReleaseOs191"] = cReleaseOs191.SelectedValue;} catch {}
			drType["ReleaseOs191"] = "Char"; drDisp["ReleaseOs191"] = "DropDownList";
			try {dr["EntityId191"] = cEntityId191.SelectedValue;} catch {}
			drType["EntityId191"] = "Numeric"; drDisp["EntityId191"] = "DropDownList";
			try {dr["ReleaseTypeId191"] = cReleaseTypeId191.SelectedValue;} catch {}
			drType["ReleaseTypeId191"] = "Numeric"; drDisp["ReleaseTypeId191"] = "DropDownList";
			try {dr["TarScriptAft191"] = cTarScriptAft191.Text;} catch {}
			drType["TarScriptAft191"] = "VarChar"; drDisp["TarScriptAft191"] = "MultiLine";
			try {dr["ReadMe191"] = cReadMe191.Text;} catch {}
			drType["ReadMe191"] = "VarChar"; drDisp["ReadMe191"] = "MultiLine";
			if (dv != null)
			{
				ds.Tables["AdmReleaseDef"].Rows.Add(MakeTypRow(ds.Tables["AdmReleaseDef"].NewRow()));
				ds.Tables["AdmReleaseDef"].Rows.Add(MakeDisRow(ds.Tables["AdmReleaseDef"].NewRow()));
				if (bAdd)
				{
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReleaseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseAdd"].NewRow(), drv, true));
					}
				}
				else
				{
					dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReleaseUpd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseUpd"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.Added;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReleaseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseAdd"].NewRow(), drv, true));
					}
					dv.RowStateFilter = DataViewRowState.Deleted;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReleaseDel"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseDel"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.CurrentRows;
				}
			}
			ds.Tables["AdmRelease"].Rows.Add(dr); ds.Tables["AdmRelease"].Rows.Add(drType); ds.Tables["AdmRelease"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmRelease98List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtReleaseOs191];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ReleaseOs', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtEntityId191];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'EntityId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtReleaseTypeId191];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ReleaseTypeId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtObjectType192];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ObjectType', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtSProcOnly192];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'SProcOnly', please investigate."); return false;
			}
			return true;
		}

		private DataRow MakeTypRow(DataRow dr)
		{
			dr["ReleaseId191"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ReleaseDtlId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ObjectType192"] = System.Data.OleDb.OleDbType.Char.ToString();
			dr["RunOrder192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["SrcObject192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["SProcOnly192"] = System.Data.OleDb.OleDbType.Char.ToString();
			dr["ObjectName192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["ObjectExempt192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["SrcClientTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["SrcRuleTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["SrcDataTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["TarDataTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["DoSpEncrypt192"] = System.Data.OleDb.OleDbType.Char.ToString();
			return dr;
		}

		private DataRow MakeDisRow(DataRow dr)
		{
			dr["ReleaseId191"] = "TextBox";
			dr["ReleaseDtlId192"] = "TextBox";
			dr["ObjectType192"] = "DropDownList";
			dr["RunOrder192"] = "TextBox";
			dr["SrcObject192"] = "TextBox";
			dr["SProcOnly192"] = "DropDownList";
			dr["ObjectName192"] = "MultiLine";
			dr["ObjectExempt192"] = "MultiLine";
			dr["SrcClientTierId192"] = "DropDownList";
			dr["SrcRuleTierId192"] = "DropDownList";
			dr["SrcDataTierId192"] = "DropDownList";
			dr["TarDataTierId192"] = "DropDownList";
			dr["DoSpEncrypt192"] = "CheckBox";
			return dr;
		}

		private DataRow MakeColRow(DataRow dr, DataRowView drv, bool bAdd)
		{
			dr["ReleaseId191"] = cReleaseId191.Text;
			DataTable dtAuth = GetAuthCol();
			if (dtAuth != null)
			{
				dr["ReleaseDtlId192"] = drv["ReleaseDtlId192"].ToString().Trim();
				dr["ObjectType192"] = drv["ObjectType192"];
				dr["RunOrder192"] = drv["RunOrder192"].ToString().Trim();
				if (bAdd && dtAuth.Rows[12]["ColReadOnly"].ToString() == "Y" && dr["RunOrder192"].ToString() == string.Empty) {dr["RunOrder192"] = System.DBNull.Value;}
				dr["SrcObject192"] = drv["SrcObject192"].ToString().Trim();
				if (bAdd && dtAuth.Rows[13]["ColReadOnly"].ToString() == "Y" && dr["SrcObject192"].ToString() == string.Empty) {dr["SrcObject192"] = System.DBNull.Value;}
				dr["SProcOnly192"] = drv["SProcOnly192"];
				dr["ObjectName192"] = drv["ObjectName192"];
				dr["ObjectExempt192"] = drv["ObjectExempt192"];
				if (bAdd && dtAuth.Rows[16]["ColReadOnly"].ToString() == "Y" && dr["ObjectExempt192"].ToString() == string.Empty) {dr["ObjectExempt192"] = System.DBNull.Value;}
				dr["SrcClientTierId192"] = drv["SrcClientTierId192"];
				if (bAdd && dtAuth.Rows[17]["ColReadOnly"].ToString() == "Y" && dr["SrcClientTierId192"].ToString() == string.Empty) {dr["SrcClientTierId192"] = System.DBNull.Value;}
				dr["SrcRuleTierId192"] = drv["SrcRuleTierId192"];
				if (bAdd && dtAuth.Rows[18]["ColReadOnly"].ToString() == "Y" && dr["SrcRuleTierId192"].ToString() == string.Empty) {dr["SrcRuleTierId192"] = System.DBNull.Value;}
				dr["SrcDataTierId192"] = drv["SrcDataTierId192"];
				if (bAdd && dtAuth.Rows[19]["ColReadOnly"].ToString() == "Y" && dr["SrcDataTierId192"].ToString() == string.Empty) {dr["SrcDataTierId192"] = System.DBNull.Value;}
				dr["TarDataTierId192"] = drv["TarDataTierId192"];
				if (bAdd && dtAuth.Rows[20]["ColReadOnly"].ToString() == "Y" && dr["TarDataTierId192"].ToString() == string.Empty) {dr["TarDataTierId192"] = System.DBNull.Value;}
				dr["DoSpEncrypt192"] = drv["DoSpEncrypt192"];
			}
			return dr;
		}

		private bool UpdateGridRow(object sender, CommandEventArgs e)
		{
			if (cAdmReleaseGrid.EditIndex > -1)
			{
				TextBox pwd = null;				cAdmReleaseGrid_OnItemUpdating(sender, new ListViewUpdateEventArgs(cAdmReleaseGrid.EditIndex));
			}
			return true;
		}

		protected void cAdmReleaseGrid_OnPreRender(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Image hi = null;
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cReleaseDtlId192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cObjectType192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cRunOrder192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cSrcObject192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cSProcOnly192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cObjectName192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cObjectExempt192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cSrcClientTierId192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cSrcRuleTierId192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cSrcDataTierId192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cTarDataTierId192hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl("cDoSpEncrypt192hi"); if (hi != null) { hi.Visible = false; }
			if (Session[KEY_lastSortImg] != null)
			{
				hi = (System.Web.UI.WebControls.Image)cAdmReleaseGrid.FindControl((string)Session[KEY_lastSortImg]);
				if (hi != null) { hi.ImageUrl = Utils.AddTilde((string)Session[KEY_lastSortUrl]); hi.Visible = true; }
			}
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cReleaseDtlId192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cObjectType192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cRunOrder192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cSrcObject192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cSProcOnly192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cObjectName192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cObjectExempt192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cSrcClientTierId192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cSrcRuleTierId192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cSrcDataTierId192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cTarDataTierId192hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReleaseGrid.FindControl("cDoSpEncrypt192hl"));
		}

		private string GetButtonId(ListViewItem lvi)
		{
			string ButtonID = String.Empty;
			Control c = lvi.FindControl("cAdmReleaseGridEdit");
			if (c != null) { ButtonID = c.UniqueID; }
			return ButtonID;
		}

		private void SetDefaultCtrl(HtmlTableRow tr, LinkButton lb, string ctrlId)
		{
			tr.Attributes.Add("onclick", "document.getElementById('" + bConfirm.ClientID + "').value='N'; fFocusedEdit('" + lb.UniqueID + "','" + ctrlId + "',event);");
		}

		private int GetCurrPageIndex()
		{
		    return cAdmReleaseGridDataPager.StartRowIndex / cAdmReleaseGridDataPager.PageSize;
		}

		private int GetTotalPages()
		{
		    return (int)Math.Ceiling((double)cAdmReleaseGridDataPager.TotalRowCount / cAdmReleaseGridDataPager.PageSize);
		}

		private int GetDataItemIndex(int editIndex)
		{
		    return cAdmReleaseGridDataPager.StartRowIndex + editIndex;
		}

		protected void cAdmReleaseGrid_OnLayoutCreated(object sender, EventArgs e)
		{
		    // Header:
		    LinkButton lb = null;
		    lb = cAdmReleaseGrid.FindControl("cReleaseDtlId192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(10); lb.ToolTip = ColumnToolTip(10); lb.Parent.Visible = GridColumnVisible(10); }
		    lb = cAdmReleaseGrid.FindControl("cObjectType192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(11); lb.ToolTip = ColumnToolTip(11); lb.Parent.Visible = GridColumnVisible(11); }
		    lb = cAdmReleaseGrid.FindControl("cRunOrder192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(12); lb.ToolTip = ColumnToolTip(12); lb.Parent.Visible = GridColumnVisible(12); }
		    lb = cAdmReleaseGrid.FindControl("cSrcObject192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(13); lb.ToolTip = ColumnToolTip(13); lb.Parent.Visible = GridColumnVisible(13); }
		    lb = cAdmReleaseGrid.FindControl("cSProcOnly192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(14); lb.ToolTip = ColumnToolTip(14); lb.Parent.Visible = GridColumnVisible(14); }
		    lb = cAdmReleaseGrid.FindControl("cObjectName192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(15); lb.ToolTip = ColumnToolTip(15); lb.Parent.Visible = GridColumnVisible(15); }
		    lb = cAdmReleaseGrid.FindControl("cObjectExempt192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(16); lb.ToolTip = ColumnToolTip(16); lb.Parent.Visible = GridColumnVisible(16); }
		    lb = cAdmReleaseGrid.FindControl("cSrcClientTierId192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(17); lb.ToolTip = ColumnToolTip(17); lb.Parent.Visible = GridColumnVisible(17); }
		    lb = cAdmReleaseGrid.FindControl("cSrcRuleTierId192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(18); lb.ToolTip = ColumnToolTip(18); lb.Parent.Visible = GridColumnVisible(18); }
		    lb = cAdmReleaseGrid.FindControl("cSrcDataTierId192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(19); lb.ToolTip = ColumnToolTip(19); lb.Parent.Visible = GridColumnVisible(19); }
		    lb = cAdmReleaseGrid.FindControl("cTarDataTierId192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(20); lb.ToolTip = ColumnToolTip(20); lb.Parent.Visible = GridColumnVisible(20); }
		    lb = cAdmReleaseGrid.FindControl("cDoSpEncrypt192hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(21); lb.ToolTip = ColumnToolTip(21); lb.Parent.Visible = GridColumnVisible(21); }
		    // Hide DeleteAll:
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if ((dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
		            lb = cAdmReleaseGrid.FindControl("cDeleteAllButton") as LinkButton; if (lb != null) { lb.Visible = false; }
				}
			}
		    // footer:
		    Label gc = null;
		    gc = cAdmReleaseGrid.FindControl("cReleaseDtlId192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(10); }
		    gc = cAdmReleaseGrid.FindControl("cObjectType192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(11); }
		    gc = cAdmReleaseGrid.FindControl("cRunOrder192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(12); }
		    gc = cAdmReleaseGrid.FindControl("cSrcObject192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(13); }
		    gc = cAdmReleaseGrid.FindControl("cSProcOnly192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(14); }
		    gc = cAdmReleaseGrid.FindControl("cObjectName192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(15); }
		    gc = cAdmReleaseGrid.FindControl("cObjectExempt192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(16); }
		    gc = cAdmReleaseGrid.FindControl("cSrcClientTierId192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(17); }
		    gc = cAdmReleaseGrid.FindControl("cSrcRuleTierId192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(18); }
		    gc = cAdmReleaseGrid.FindControl("cSrcDataTierId192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(19); }
		    gc = cAdmReleaseGrid.FindControl("cTarDataTierId192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(20); }
		    gc = cAdmReleaseGrid.FindControl("cDoSpEncrypt192fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(21); }
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
			if (cInsRowButton.Attributes["OnClick"] == null || cInsRowButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cInsRowButton.Attributes["onclick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cBrowse.Attributes["OnClick"] == null || cBrowse.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cBrowse.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cPgSizeButton.Attributes["OnClick"] == null || cPgSizeButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cPgSizeButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cFirstButton.Attributes["OnClick"] == null || cFirstButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cFirstButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cPrevButton.Attributes["OnClick"] == null || cPrevButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cPrevButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cNextButton.Attributes["OnClick"] == null || cNextButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cNextButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cLastButton.Attributes["OnClick"] == null || cLastButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cLastButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cFindButton.Attributes["OnClick"] == null || cFindButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cFindButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cHideImpButton.Attributes["OnClick"] == null || cHideImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cHideImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cShowImpButton.Attributes["OnClick"] == null || cShowImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cShowImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmRelease98List,null); }

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

