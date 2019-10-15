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
	public class AdmReport67 : DataSet
	{
		public AdmReport67()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmReport")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportDef")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportAdd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportUpd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportDel")));
			this.DataSetName = "AdmReport67";
			this.Namespace = "http://Rintagi.com/DataSet/AdmReport67";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ReportId22", typeof(string));
			columns.Add("ProgramName22", typeof(string));
			columns.Add("ReportTypeCd22", typeof(string));
			columns.Add("OrientationCd22", typeof(string));
			columns.Add("CopyReportId22", typeof(string));
			columns.Add("ModifiedBy22", typeof(string));
			columns.Add("TemplateName22", typeof(string));
			columns.Add("RptTemplate22", typeof(string));
			columns.Add("UnitCd22", typeof(string));
			columns.Add("TopMargin22", typeof(string));
			columns.Add("BottomMargin22", typeof(string));
			columns.Add("LeftMargin22", typeof(string));
			columns.Add("RightMargin22", typeof(string));
			columns.Add("PageWidth22", typeof(string));
			columns.Add("PageHeight22", typeof(string));
			columns.Add("SyncByDb", typeof(string));
			columns.Add("SyncToDb", typeof(string));
			columns.Add("AllowSelect22", typeof(string));
			columns.Add("GenerateRp22", typeof(string));
			columns.Add("LastGenDt22", typeof(string));
			columns.Add("AuthRequired22", typeof(string));
			columns.Add("WhereClause22", typeof(string));
			columns.Add("RegClause22", typeof(string));
			columns.Add("RegCode22", typeof(string));
			columns.Add("ValClause22", typeof(string));
			columns.Add("ValCode22", typeof(string));
			columns.Add("UpdClause22", typeof(string));
			columns.Add("UpdCode22", typeof(string));
			columns.Add("XlsClause22", typeof(string));
			columns.Add("XlsCode22", typeof(string));
			return dt;
		}

		private DataTable MakeDtlColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ReportId22", typeof(string));
			columns.Add("ReportHlpId96", typeof(string));
			columns.Add("CultureId96", typeof(string));
			columns.Add("DefaultHlpMsg96", typeof(string));
			columns.Add("ReportTitle96", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmReportModule : RO.Web.ModuleBase
	{

		private const string KEY_lastAddedRow = "Cache:lastAddedRow3_67";
		private const string KEY_lastSortOrd = "Cache:lastSortOrd3_67";
		private const string KEY_lastSortImg = "Cache:lastSortImg3_67";
		private const string KEY_lastSortCol = "Cache:lastSortCol3_67";
		private const string KEY_lastSortExp = "Cache:lastSortExp3_67";
		private const string KEY_lastSortUrl = "Cache:lastSortUrl3_67";
		private const string KEY_lastSortTog = "Cache:lastSortTog3_67";
		private const string KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride3_67";
		private const string KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride3_67";
		private const string KEY_currPageIndex = "Cache:currPageIndex3_67";
		private const string KEY_bHiImpVisible = "Cache:bHiImpVisible3_67";
		private const string KEY_bShImpVisible = "Cache:bShImpVisible3_67";
		private const string KEY_dtAdmReportGrid = "Cache:dtAdmReportGrid";
		private const string KEY_scrImport = "Cache:scrImport3_67";
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_67";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_67";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_67";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_67";
		private const string KEY_dtLabel = "Cache:dtLabel3_67";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_67";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_67";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_67";

		private const string KEY_dtReportTypeCd22 = "Cache:dtReportTypeCd22";
		private const string KEY_dtOrientationCd22 = "Cache:dtOrientationCd22";
		private const string KEY_dtCopyReportId22 = "Cache:dtCopyReportId22";
		private const string KEY_dtModifiedBy22 = "Cache:dtModifiedBy22";
		private const string KEY_dtRptTemplate22 = "Cache:dtRptTemplate22";
		private const string KEY_dtUnitCd22 = "Cache:dtUnitCd22";
		private const string KEY_dtCultureId96 = "Cache:dtCultureId96";

		private const string KEY_dtSystems = "Cache:dtSystems3_67";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_67";
		private const string KEY_dtAdmReport67List = "Cache:dtAdmReport67List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_67";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_67";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_67";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_67";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_67";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_67";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_67";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_67";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmReportModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,67, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				cFind.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cFindButton.ClientID + "')");
				cPgSize.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cPgSizeButton.ClientID + "')");
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
				Session.Remove(KEY_dtAdmReportGrid);
				Session.Remove(KEY_lastSortCol);
				Session.Remove(KEY_lastSortExp);
				Session.Remove(KEY_lastSortImg);
				Session.Remove(KEY_lastSortUrl);
				Session.Remove(KEY_dtAdmReport67List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtReportTypeCd22);
				Session.Remove(KEY_dtOrientationCd22);
				Session.Remove(KEY_dtCopyReportId22);
				Session.Remove(KEY_dtModifiedBy22);
				Session.Remove(KEY_dtRptTemplate22);
				Session.Remove(KEY_dtUnitCd22);
				Session.Remove(KEY_dtCultureId96);
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
				cTab28.InnerText = dt.Rows[0]["TabFolderName"].ToString();
				cTab29.InnerText = dt.Rows[1]["TabFolderName"].ToString();
				cTab30.InnerText = dt.Rows[2]["TabFolderName"].ToString();
				cTab31.InnerText = dt.Rows[3]["TabFolderName"].ToString();
				cTab32.InnerText = dt.Rows[4]["TabFolderName"].ToString();
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 67, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				SetRptTemplate22(cRptTemplate22GV, string.Empty);
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmReport67List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=67&typ=N&sys=3','','',''); return false;"; }
				//WebRule: Prevent production access to stored procedure
if (Config.DeployType == "PRD") {cRegCode22.Enabled = false; cValCode22.Enabled = false; cUpdCode22.Enabled = false; cXlsCode22.Enabled = false;}
				else
				{
					if (cSyncByDb.Attributes["OnClick"] == null || cSyncByDb.Attributes["OnClick"].IndexOf("return confirm") < 0) {cSyncByDb.Attributes["OnClick"] += "return confirm('Proceed to obtain stored procedures from the physical database for sure?');";}
					if (cSyncToDb.Attributes["OnClick"] == null || cSyncToDb.Attributes["OnClick"].IndexOf("_bConfirm") < 0) { cSyncToDb.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
					if (cSyncToDb.Attributes["OnClick"] == null || cSyncToDb.Attributes["OnClick"].IndexOf("return confirm") < 0) {cSyncToDb.Attributes["OnClick"] += "return confirm('Proceed to synchronize stored procedures to physical database for sure?');";}
				}
				// *** WebRule End *** //
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
				DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
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
					dt = (new AdminSystem()).GetAuthRow(67,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
					Session[KEY_dtAuthRow] = dt;
					DataRow dr = dt.Rows[0];
					if (!((dr["AllowUpd"].ToString() == "N" || dr["ViewOnly"].ToString() == "G") && dr["AllowAdd"].ToString() == "N") && (Request.QueryString["enb"] == null || Request.QueryString["enb"] != "N"))
					{
						cAdmReportGrid.PreRender += new EventHandler(cAdmReportGrid_OnPreRender);
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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,67,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(67, "Report Definition", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(67,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtRul = (new AdminSystem()).GetClientRule(67,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					if (ee == null || drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on" || (cAdmReportGrid.EditIndex > -1 && GetDataItemIndex(cAdmReportGrid.EditIndex) == ee.DataItemIndex))
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(67,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(67,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,67,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(67,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetScreenLabel(67,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(67,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(67,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(67,"GetExpAdmReport67","Y",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(67,"GetExpAdmReport67","N",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(67,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[2]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[3]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[4]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[5]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[9]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[22]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[23]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[24]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[25]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[26]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[27]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[28]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[29]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[30]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[31]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[32]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[32]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[33]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[34]["ColumnHeader"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReportId22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ProgramName22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReportTypeCd22"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["ReportTypeCd22Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["OrientationCd22"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["OrientationCd22Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["CopyReportId22"].ToString() + (char)9 + drv["CopyReportId22Text"].ToString() + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["ModifiedBy22"].ToString() + (char)9 + drv["ModifiedBy22Text"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTimeUTC(drv["ModifiedOn22"].ToString(),base.LUser.Culture,CurrTimeZoneInfo()) + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["TemplateName22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UnitCd22"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["UnitCd22Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["TopMargin22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["BottomMargin22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["LeftMargin22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["RightMargin22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["PageWidth22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["PageHeight22"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AllowSelect22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GenerateRp22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["LastGenDt22"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AuthRequired22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["WhereClause22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RegClause22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RegCode22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ValClause22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ValCode22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UpdClause22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UpdCode22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["XlsClause22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["XlsCode22"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReportHlpId96"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId96"].ToString() + (char)9 + drv["CultureId96Text"].ToString() + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DefaultHlpMsg96"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ReportTitle96"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmReport.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmReport.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(67,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
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
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + @"\cell ");}
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReportId22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ProgramName22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["ReportTypeCd22Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["OrientationCd22Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["CopyReportId22Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["ModifiedBy22Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmShortDateTimeUTC(drv["ModifiedOn22"].ToString(),base.LUser.Culture,CurrTimeZoneInfo()) + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["TemplateName22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["UnitCd22Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["TopMargin22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["BottomMargin22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["LeftMargin22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["RightMargin22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["PageWidth22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["PageHeight22"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["AllowSelect22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["GenerateRp22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["LastGenDt22"].ToString() + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["AuthRequired22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(drv["WhereClause22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(drv["RegClause22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[24]["ColExport"].ToString() == "Y") {sb.Append(drv["RegCode22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[25]["ColExport"].ToString() == "Y") {sb.Append(drv["ValClause22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(drv["ValCode22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["UpdClause22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(drv["UpdCode22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["XlsClause22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(drv["XlsCode22"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ReportHlpId96"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId96Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(drv["DefaultHlpMsg96"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(drv["ReportTitle96"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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

		public void cRptTemplate22Upl_Click(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(cReportId22.Text) && cRptTemplate22Fi.HasFile && cRptTemplate22Fi.PostedFile.FileName != string.Empty)
			{
				if (cRptTemplate22Fi.PostedFile.FileName.Length > 100)
				{
					bErrNow.Value = "Y"; PreMsgPopup("Filename exceeds a total of 100 characters. Please shorten the filename and upload again.");
				}
				else
				{
					byte[] dc;
					if ("image/gif,image/jpeg,image/png,image/tiff,image/pjpeg,image/x-png".IndexOf(cRptTemplate22Fi.PostedFile.ContentType) >= 0 && cRptTemplate22Fi.PostedFile.ContentLength > int.Parse(Config.ImgThreshold) * 1024)
					{
						System.Drawing.Image oBMP = System.Drawing.Image.FromStream(cRptTemplate22Fi.PostedFile.InputStream);
						int nHeight = int.Parse((Math.Round(decimal.Parse(oBMP.Height.ToString()) * (decimal.Parse(Config.ImgThreshold) / decimal.Parse(oBMP.Width.ToString())))).ToString());
						Bitmap nBMP = new Bitmap(oBMP, int.Parse(Config.ImgThreshold), nHeight);
						using (System.IO.MemoryStream sm = new System.IO.MemoryStream())
				    	{
						    nBMP.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
						    sm.Position = 0;
						    dc = new byte[sm.Length + 1];
						    sm.Read(dc, 0, dc.Length); sm.Close();
					    }
						oBMP.Dispose(); nBMP.Dispose();
					}
					else
					{
						dc = new byte[cRptTemplate22Fi.PostedFile.ContentLength];
						cRptTemplate22Fi.PostedFile.InputStream.Read(dc, 0, dc.Length);
					}
					// In case DocId has not been saved properly, always find the most recent to replace as long as it has the same file name:
					string DocId = string.Empty;
					DocId = new AdminSystem().GetDocId(cReportId22.Text, "dbo.RptTemplate", Path.GetFileName(cRptTemplate22Fi.PostedFile.FileName), base.LUser.UsrId.ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
					if (DocId == string.Empty || !cRptTemplate22Ow.Checked)
					{
						DocId = new AdminSystem().AddDbDoc(cReportId22.Text, "dbo.RptTemplate", Path.GetFileName(cRptTemplate22Fi.PostedFile.FileName), cRptTemplate22Fi.PostedFile.ContentType, dc.Length, dc, (string)Session[KEY_sysConnectionString], LcAppPw, base.LUser);
					}
					else
					{
						new AdminSystem().UpdDbDoc(DocId, "dbo.RptTemplate", Path.GetFileName(cRptTemplate22Fi.PostedFile.FileName), cRptTemplate22Fi.PostedFile.ContentType, dc.Length, dc, (string)Session[KEY_sysConnectionString], LcAppPw, base.LUser);
					}
					cRptTemplate22Pan.Visible = false; cRptTemplate22Div.Visible = true;
					SetRptTemplate22(cRptTemplate22GV, string.Empty);
				}
			}
		}

		public void cRptTemplate22Tgo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cRptTemplate22Div.Visible)
			{
				cRptTemplate22Pan.Visible = true; cRptTemplate22Div.Visible = false;
			}
			else
			{
				cRptTemplate22Pan.Visible = false; cRptTemplate22Div.Visible = true;
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

		private void GetPageInfo()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetLastPageInfo(67, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 67, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (cAdmReportGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
			PopAdmReport67List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("67", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("67", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("67", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("67", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("67", LcSysConnString, LcAppPw);
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
		            context["scr"] = "67";
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
		            context["scr"] = "67";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmReport67" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmReport67" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("67", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("67", "AdmReport67", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
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
				dtScreenTab = (new AdminSystem()).GetScreenTab(67,base.LUser.CultureId,LcSysConnString,LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return dtScreenTab;
		}

		private void SetReportTypeCd22(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtReportTypeCd22];
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
						dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlReportTypeCd3S1729",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ReportTypeCd22 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlReportTypeCd3S1729",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlReportTypeCd3S1729",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtReportTypeCd22] = dv.Table;
				}
			}
		}

		private void SetOrientationCd22(RadioButtonList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtOrientationCd22];
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
						dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlOrientationCd3S1010",false,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "OrientationCd22 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlOrientationCd3S1010",false,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlOrientationCd3S1010",false,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtOrientationCd22] = dv.Table;
				}
			}
		}

		protected void cbCopyReportId22(object sender, System.EventArgs e)
		{
			SetCopyReportId22((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetCopyReportId22(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlCopyReportId3S1012";
			context["addnew"] = "Y";
			context["mKey"] = "CopyReportId22";
			context["mVal"] = "CopyReportId22Text";
			context["mTip"] = "CopyReportId22Text";
			context["mImg"] = "CopyReportId22Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "67";
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
					dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlCopyReportId3S1012",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						context["pMKeyColID"] = cAdmReport67List.ClientID;
						context["pMKeyCol"] = "ReportId";
						string ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtCopyReportId22] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void SetModifiedBy22(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtModifiedBy22];
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
						dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlModifiedBy3S1470",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ModifiedBy22 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlModifiedBy3S1470",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlModifiedBy3S1470",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtModifiedBy22] = dv.Table;
				}
			}
		}

		private void SetRptTemplate22(GridView ddl, string keyId)
		{
			DataView dv = null;
			if (ddl != null)
			{
				string ss = string.Empty;
				if (dv == null)
				{
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlRptTemplate3S1728",false,false,0,cAdmReport67List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						if (dv != null)
						{
							foreach (DataRowView drv in dv)
							{
								if (drv["DocLink"].ToString().IndexOf("&sys=") < 0)
								{
									drv["DocLink"] = drv["DocLink"] + "&sys=" + base.LCurr.DbId.ToString();
								}
							}
						}
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					ddl.PageIndex = 0;
					ddl.DataSource = dv; ddl.DataBind();
					Session[KEY_dtRptTemplate22] = dv.Table;
				}
			}
		}

		private void SetUnitCd22(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtUnitCd22];
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
						dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlUnitCd3S1525",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "UnitCd22 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlUnitCd3S1525",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlUnitCd3S1525",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtUnitCd22] = dv.Table;
				}
			}
		}

		protected void cbCultureId96(object sender, System.EventArgs e)
		{
			SetCultureId96((RoboCoder.WebControls.ComboBox)sender,string.Empty,cAdmReportGrid.EditItem);
		}

		private void SetCultureId96(RoboCoder.WebControls.ComboBox ddl, string keyId, ListViewItem li)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlCultureId3S1022";
			context["addnew"] = "Y";
			context["mKey"] = "CultureId96";
			context["mVal"] = "CultureId96Text";
			context["mTip"] = "CultureId96Text";
			context["mImg"] = "CultureId96Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "67";
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
					dv = new DataView((new AdminSystem()).GetDdl(67,"GetDdlCultureId3S1022",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ReportId"))
					{
						context["pMKeyColID"] = cAdmReport67List.ClientID;
						context["pMKeyCol"] = "ReportId";
						string ss = "(ReportId is null";
						if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ReportId = " + cAdmReport67List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtCultureId96] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private DataView GetAdmReport67List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReport67List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(67,"GetLisAdmReport67",true,"Y",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(67,"GetLisAdmReport67",true,"N",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmReport67List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmReport67List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmReport67";
			context["mKey"] = "ReportId22";
			context["mVal"] = "ReportId22Text";
			context["mTip"] = "ReportId22Text";
			context["mImg"] = "ReportId22Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "67";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			cAdmReport67List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmReport67List.DataContext = context;
			if (dv.Table == null) return;
			cAdmReport67List.DataSource = dv;
			cAdmReport67List.Visible = true;
			if (cAdmReport67List.Items.Count <= 0) {cAdmReport67List.Visible = false; cAdmReport67List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmReport67List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmReport67List.Items[0].Selected = true; cAdmReport67List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmReport67List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmReport67List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cReportId22, dtAuth.Rows[0], cReportId22P1, cReportId22Label, cReportId22P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cProgramName22, dtAuth.Rows[1], cProgramName22P1, cProgramName22Label, cProgramName22P2, null, dtLabel.Rows[1], cRFVProgramName22, null, null);
				base.SetFoldBehavior(cReportTypeCd22, dtAuth.Rows[2], cReportTypeCd22P1, cReportTypeCd22Label, cReportTypeCd22P2, null, dtLabel.Rows[2], cRFVReportTypeCd22, null, null);
				base.SetFoldBehavior(cOrientationCd22, dtAuth.Rows[3], cOrientationCd22P1, cOrientationCd22Label, cOrientationCd22P2, null, dtLabel.Rows[3], null, null, null);
				base.SetFoldBehavior(cCopyReportId22, dtAuth.Rows[4], cCopyReportId22P1, cCopyReportId22Label, cCopyReportId22P2, null, dtLabel.Rows[4], null, null, null);
				SetCopyReportId22(cCopyReportId22,string.Empty);
				base.SetFoldBehavior(cModifiedBy22, dtAuth.Rows[5], cModifiedBy22P1, cModifiedBy22Label, cModifiedBy22P2, null, dtLabel.Rows[5], null, null, null);
				base.SetFoldBehavior(cModifiedOn22, dtAuth.Rows[6], cModifiedOn22P1, cModifiedOn22Label, cModifiedOn22P2, null, dtLabel.Rows[6], null, null, null);
				base.SetFoldBehavior(cTemplateName22, dtAuth.Rows[7], cTemplateName22P1, cTemplateName22Label, cTemplateName22P2, null, dtLabel.Rows[7], null, null, null);
				base.SetFoldBehavior(cRptTemplate22GV, dtAuth.Rows[8], cRptTemplate22P1, cRptTemplate22Label, cRptTemplate22P2, cRptTemplate22Tgo, cRptTemplate22Div, dtLabel.Rows[8], null, null, null);
				cRptTemplate22Pan.Height = cRptTemplate22Div.Height; cRptTemplate22Pan.Width = cRptTemplate22Div.Width;
				base.SetFoldBehavior(cUnitCd22, dtAuth.Rows[9], cUnitCd22P1, cUnitCd22Label, cUnitCd22P2, null, dtLabel.Rows[9], cRFVUnitCd22, null, null);
				base.SetFoldBehavior(cTopMargin22, dtAuth.Rows[10], cTopMargin22P1, cTopMargin22Label, cTopMargin22P2, null, dtLabel.Rows[10], cRFVTopMargin22, null, null);
				base.SetFoldBehavior(cBottomMargin22, dtAuth.Rows[11], cBottomMargin22P1, cBottomMargin22Label, cBottomMargin22P2, null, dtLabel.Rows[11], cRFVBottomMargin22, null, null);
				base.SetFoldBehavior(cLeftMargin22, dtAuth.Rows[12], cLeftMargin22P1, cLeftMargin22Label, cLeftMargin22P2, null, dtLabel.Rows[12], cRFVLeftMargin22, null, null);
				base.SetFoldBehavior(cRightMargin22, dtAuth.Rows[13], cRightMargin22P1, cRightMargin22Label, cRightMargin22P2, null, dtLabel.Rows[13], cRFVRightMargin22, null, null);
				base.SetFoldBehavior(cPageWidth22, dtAuth.Rows[14], cPageWidth22P1, cPageWidth22Label, cPageWidth22P2, null, dtLabel.Rows[14], cRFVPageWidth22, null, null);
				base.SetFoldBehavior(cPageHeight22, dtAuth.Rows[15], cPageHeight22P1, cPageHeight22Label, cPageHeight22P2, null, dtLabel.Rows[15], cRFVPageHeight22, null, null);
				base.SetFoldBehavior(cSyncByDb, dtAuth.Rows[16], cSyncByDbP1, cSyncByDbLabel, cSyncByDbP2, null, dtLabel.Rows[16], null, null, null);
				base.SetFoldBehavior(cSyncToDb, dtAuth.Rows[17], cSyncToDbP1, cSyncToDbLabel, cSyncToDbP2, null, dtLabel.Rows[17], null, null, null);
				base.SetFoldBehavior(cAllowSelect22, dtAuth.Rows[18], cAllowSelect22P1, cAllowSelect22Label, cAllowSelect22P2, null, dtLabel.Rows[18], null, null, null);
				base.SetFoldBehavior(cGenerateRp22, dtAuth.Rows[19], cGenerateRp22P1, cGenerateRp22Label, cGenerateRp22P2, null, dtLabel.Rows[19], null, null, null);
				base.SetFoldBehavior(cLastGenDt22, dtAuth.Rows[20], cLastGenDt22P1, cLastGenDt22Label, cLastGenDt22P2, null, dtLabel.Rows[20], null, null, null);
				base.SetFoldBehavior(cAuthRequired22, dtAuth.Rows[21], cAuthRequired22P1, cAuthRequired22Label, cAuthRequired22P2, null, dtLabel.Rows[21], null, null, null);
				base.SetFoldBehavior(cWhereClause22, dtAuth.Rows[22], cWhereClause22P1, cWhereClause22Label, cWhereClause22P2, cWhereClause22E, null, dtLabel.Rows[22], null, null, null);
				cWhereClause22E.Attributes["label_id"] = cWhereClause22Label.ClientID; cWhereClause22E.Attributes["target_id"] = cWhereClause22.ClientID;
				base.SetFoldBehavior(cRegClause22, dtAuth.Rows[23], cRegClause22P1, cRegClause22Label, cRegClause22P2, cRegClause22E, null, dtLabel.Rows[23], null, null, null);
				cRegClause22E.Attributes["label_id"] = cRegClause22Label.ClientID; cRegClause22E.Attributes["target_id"] = cRegClause22.ClientID;
				base.SetFoldBehavior(cRegCode22, dtAuth.Rows[24], cRegCode22P1, cRegCode22Label, cRegCode22P2, cRegCode22E, null, dtLabel.Rows[24], null, null, null);
				cRegCode22E.Attributes["label_id"] = cRegCode22Label.ClientID; cRegCode22E.Attributes["target_id"] = cRegCode22.ClientID;
				base.SetFoldBehavior(cValClause22, dtAuth.Rows[25], cValClause22P1, cValClause22Label, cValClause22P2, cValClause22E, null, dtLabel.Rows[25], null, null, null);
				cValClause22E.Attributes["label_id"] = cValClause22Label.ClientID; cValClause22E.Attributes["target_id"] = cValClause22.ClientID;
				base.SetFoldBehavior(cValCode22, dtAuth.Rows[26], cValCode22P1, cValCode22Label, cValCode22P2, cValCode22E, null, dtLabel.Rows[26], null, null, null);
				cValCode22E.Attributes["label_id"] = cValCode22Label.ClientID; cValCode22E.Attributes["target_id"] = cValCode22.ClientID;
				base.SetFoldBehavior(cUpdClause22, dtAuth.Rows[27], cUpdClause22P1, cUpdClause22Label, cUpdClause22P2, cUpdClause22E, null, dtLabel.Rows[27], null, null, null);
				cUpdClause22E.Attributes["label_id"] = cUpdClause22Label.ClientID; cUpdClause22E.Attributes["target_id"] = cUpdClause22.ClientID;
				base.SetFoldBehavior(cUpdCode22, dtAuth.Rows[28], cUpdCode22P1, cUpdCode22Label, cUpdCode22P2, cUpdCode22E, null, dtLabel.Rows[28], null, null, null);
				cUpdCode22E.Attributes["label_id"] = cUpdCode22Label.ClientID; cUpdCode22E.Attributes["target_id"] = cUpdCode22.ClientID;
				base.SetFoldBehavior(cXlsClause22, dtAuth.Rows[29], cXlsClause22P1, cXlsClause22Label, cXlsClause22P2, cXlsClause22E, null, dtLabel.Rows[29], null, null, null);
				cXlsClause22E.Attributes["label_id"] = cXlsClause22Label.ClientID; cXlsClause22E.Attributes["target_id"] = cXlsClause22.ClientID;
				base.SetFoldBehavior(cXlsCode22, dtAuth.Rows[30], cXlsCode22P1, cXlsCode22Label, cXlsCode22P2, cXlsCode22E, null, dtLabel.Rows[30], null, null, null);
				cXlsCode22E.Attributes["label_id"] = cXlsCode22Label.ClientID; cXlsCode22E.Attributes["target_id"] = cXlsCode22.ClientID;
			}
			if ((cProgramName22.Attributes["OnChange"] == null || cProgramName22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cProgramName22.Visible && !cProgramName22.ReadOnly) {cProgramName22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cReportTypeCd22.Attributes["OnChange"] == null || cReportTypeCd22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReportTypeCd22.Visible && cReportTypeCd22.Enabled) {cReportTypeCd22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cOrientationCd22.Attributes["OnClick"] == null || cOrientationCd22.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cOrientationCd22.Visible && cOrientationCd22.Enabled) {cOrientationCd22.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cCopyReportId22.Attributes["OnChange"] == null || cCopyReportId22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cCopyReportId22.Visible && cCopyReportId22.Enabled) {cCopyReportId22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cModifiedBy22.Attributes["OnChange"] == null || cModifiedBy22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cModifiedBy22.Visible && cModifiedBy22.Enabled) {cModifiedBy22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cModifiedOn22.Attributes["OnChange"] == null || cModifiedOn22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cModifiedOn22.Visible && !cModifiedOn22.ReadOnly) {cModifiedOn22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cTemplateName22.Attributes["OnChange"] == null || cTemplateName22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cTemplateName22.Visible && !cTemplateName22.ReadOnly) {cTemplateName22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRptTemplate22Tgo.Attributes["OnClick"] == null || cRptTemplate22Tgo.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRptTemplate22Tgo.Visible) {cRptTemplate22Tgo.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cUnitCd22.Attributes["OnChange"] == null || cUnitCd22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUnitCd22.Visible && cUnitCd22.Enabled) {cUnitCd22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cTopMargin22.Attributes["OnChange"] == null || cTopMargin22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cTopMargin22.Visible && !cTopMargin22.ReadOnly) {cTopMargin22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cBottomMargin22.Attributes["OnChange"] == null || cBottomMargin22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cBottomMargin22.Visible && !cBottomMargin22.ReadOnly) {cBottomMargin22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLeftMargin22.Attributes["OnChange"] == null || cLeftMargin22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLeftMargin22.Visible && !cLeftMargin22.ReadOnly) {cLeftMargin22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRightMargin22.Attributes["OnChange"] == null || cRightMargin22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRightMargin22.Visible && !cRightMargin22.ReadOnly) {cRightMargin22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPageWidth22.Attributes["OnChange"] == null || cPageWidth22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPageWidth22.Visible && !cPageWidth22.ReadOnly) {cPageWidth22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPageHeight22.Attributes["OnChange"] == null || cPageHeight22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPageHeight22.Visible && !cPageHeight22.ReadOnly) {cPageHeight22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSyncByDb.Attributes["OnChange"] == null || cSyncByDb.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSyncByDb.Visible && cSyncByDb.Enabled) {cSyncByDb.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cSyncToDb.Attributes["OnChange"] == null || cSyncToDb.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSyncToDb.Visible && cSyncToDb.Enabled) {cSyncToDb.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cAllowSelect22.Attributes["OnClick"] == null || cAllowSelect22.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cAllowSelect22.Visible && cAllowSelect22.Enabled) {cAllowSelect22.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cGenerateRp22.Attributes["OnClick"] == null || cGenerateRp22.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cGenerateRp22.Visible && cGenerateRp22.Enabled) {cGenerateRp22.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cLastGenDt22.Attributes["OnChange"] == null || cLastGenDt22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLastGenDt22.Visible && !cLastGenDt22.ReadOnly) {cLastGenDt22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cAuthRequired22.Attributes["OnClick"] == null || cAuthRequired22.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cAuthRequired22.Visible && cAuthRequired22.Enabled) {cAuthRequired22.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cWhereClause22.Attributes["OnChange"] == null || cWhereClause22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cWhereClause22.Visible && !cWhereClause22.ReadOnly) {cWhereClause22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRegClause22.Attributes["OnChange"] == null || cRegClause22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRegClause22.Visible && !cRegClause22.ReadOnly) {cRegClause22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cRegCode22.Attributes["OnChange"] == null || cRegCode22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRegCode22.Visible && !cRegCode22.ReadOnly) {cRegCode22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cValClause22.Attributes["OnChange"] == null || cValClause22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cValClause22.Visible && !cValClause22.ReadOnly) {cValClause22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cValCode22.Attributes["OnChange"] == null || cValCode22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cValCode22.Visible && !cValCode22.ReadOnly) {cValCode22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cUpdClause22.Attributes["OnChange"] == null || cUpdClause22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUpdClause22.Visible && !cUpdClause22.ReadOnly) {cUpdClause22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cUpdCode22.Attributes["OnChange"] == null || cUpdCode22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUpdCode22.Visible && !cUpdCode22.ReadOnly) {cUpdCode22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cXlsClause22.Attributes["OnChange"] == null || cXlsClause22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cXlsClause22.Visible && !cXlsClause22.ReadOnly) {cXlsClause22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cXlsCode22.Attributes["OnChange"] == null || cXlsCode22.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cXlsCode22.Visible && !cXlsCode22.ReadOnly) {cXlsCode22.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (cAdmReportGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				cNewButton_Click(sender, new EventArgs());
			}
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (cAdmReportGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
				Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
				Session.Remove(KEY_dtReportTypeCd22);
				Session.Remove(KEY_dtOrientationCd22);
				Session.Remove(KEY_dtCopyReportId22);
				Session.Remove(KEY_dtModifiedBy22);
				Session.Remove(KEY_dtRptTemplate22);
				Session.Remove(KEY_dtUnitCd22);
				Session.Remove(KEY_dtCultureId96);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
			}
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cProgramName22.Text = string.Empty;}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {SetReportTypeCd22(cReportTypeCd22,"S");}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {SetOrientationCd22(cOrientationCd22,"P");}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {cCopyReportId22.ClearSearch();}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {SetModifiedBy22(cModifiedBy22,string.Empty);}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {cModifiedOn22.Text = string.Empty;}
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cTemplateName22.Text = string.Empty;}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {SetRptTemplate22(cRptTemplate22GV,string.Empty);}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {SetUnitCd22(cUnitCd22,string.Empty);}
			if (dt.Rows[10]["ColVisible"].ToString() == "Y" && dt.Rows[10]["ColReadOnly"].ToString() != "Y") {cTopMargin22.Text = string.Empty;}
			if (dt.Rows[11]["ColVisible"].ToString() == "Y" && dt.Rows[11]["ColReadOnly"].ToString() != "Y") {cBottomMargin22.Text = string.Empty;}
			if (dt.Rows[12]["ColVisible"].ToString() == "Y" && dt.Rows[12]["ColReadOnly"].ToString() != "Y") {cLeftMargin22.Text = string.Empty;}
			if (dt.Rows[13]["ColVisible"].ToString() == "Y" && dt.Rows[13]["ColReadOnly"].ToString() != "Y") {cRightMargin22.Text = string.Empty;}
			if (dt.Rows[14]["ColVisible"].ToString() == "Y" && dt.Rows[14]["ColReadOnly"].ToString() != "Y") {cPageWidth22.Text = string.Empty;}
			if (dt.Rows[15]["ColVisible"].ToString() == "Y" && dt.Rows[15]["ColReadOnly"].ToString() != "Y") {cPageHeight22.Text = string.Empty;}
			if (dt.Rows[16]["ColVisible"].ToString() == "Y" && dt.Rows[16]["ColReadOnly"].ToString() != "Y") {cSyncByDb.ImageUrl = "~/images/custom/adm/SyncByDb.gif"; }
			if (dt.Rows[17]["ColVisible"].ToString() == "Y" && dt.Rows[17]["ColReadOnly"].ToString() != "Y") {cSyncToDb.ImageUrl = "~/images/custom/adm/SyncToDb.gif"; }
			if (dt.Rows[18]["ColVisible"].ToString() == "Y" && dt.Rows[18]["ColReadOnly"].ToString() != "Y") {cAllowSelect22.Checked = base.GetBool("N");}
			if (dt.Rows[19]["ColVisible"].ToString() == "Y" && dt.Rows[19]["ColReadOnly"].ToString() != "Y") {cGenerateRp22.Checked = base.GetBool("Y");}
			if (dt.Rows[20]["ColVisible"].ToString() == "Y" && dt.Rows[20]["ColReadOnly"].ToString() != "Y") {cLastGenDt22.Text = string.Empty;}
			if (dt.Rows[21]["ColVisible"].ToString() == "Y" && dt.Rows[21]["ColReadOnly"].ToString() != "Y") {cAuthRequired22.Checked = base.GetBool("Y");}
			if (dt.Rows[22]["ColVisible"].ToString() == "Y" && dt.Rows[22]["ColReadOnly"].ToString() != "Y") {cWhereClause22.Text = string.Empty;}
			if (dt.Rows[23]["ColVisible"].ToString() == "Y" && dt.Rows[23]["ColReadOnly"].ToString() != "Y") {cRegClause22.Text = string.Empty;}
			if (dt.Rows[24]["ColVisible"].ToString() == "Y" && dt.Rows[24]["ColReadOnly"].ToString() != "Y") {cRegCode22.Text = string.Empty;}
			if (dt.Rows[25]["ColVisible"].ToString() == "Y" && dt.Rows[25]["ColReadOnly"].ToString() != "Y") {cValClause22.Text = string.Empty;}
			if (dt.Rows[26]["ColVisible"].ToString() == "Y" && dt.Rows[26]["ColReadOnly"].ToString() != "Y") {cValCode22.Text = string.Empty;}
			if (dt.Rows[27]["ColVisible"].ToString() == "Y" && dt.Rows[27]["ColReadOnly"].ToString() != "Y") {cUpdClause22.Text = string.Empty;}
			if (dt.Rows[28]["ColVisible"].ToString() == "Y" && dt.Rows[28]["ColReadOnly"].ToString() != "Y") {cUpdCode22.Text = string.Empty;}
			if (dt.Rows[29]["ColVisible"].ToString() == "Y" && dt.Rows[29]["ColReadOnly"].ToString() != "Y") {cXlsClause22.Text = string.Empty;}
			if (dt.Rows[30]["ColVisible"].ToString() == "Y" && dt.Rows[30]["ColReadOnly"].ToString() != "Y") {cXlsCode22.Text = string.Empty;}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cReportId22.Text = string.Empty;
			cProgramName22.Text = string.Empty;
			SetReportTypeCd22(cReportTypeCd22,"S");
			SetOrientationCd22(cOrientationCd22,"P");
			cCopyReportId22.ClearSearch();
			SetModifiedBy22(cModifiedBy22,string.Empty);
			cModifiedOn22.Text = string.Empty;
			cTemplateName22.Text = string.Empty;
			SetRptTemplate22(cRptTemplate22GV,string.Empty);
			SetUnitCd22(cUnitCd22,string.Empty);
			cTopMargin22.Text = string.Empty;
			cBottomMargin22.Text = string.Empty;
			cLeftMargin22.Text = string.Empty;
			cRightMargin22.Text = string.Empty;
			cPageWidth22.Text = string.Empty;
			cPageHeight22.Text = string.Empty;
			cSyncByDb.ImageUrl = "~/images/custom/adm/SyncByDb.gif";
			cSyncToDb.ImageUrl = "~/images/custom/adm/SyncToDb.gif";
			cAllowSelect22.Checked = base.GetBool("N");
			cGenerateRp22.Checked = base.GetBool("Y");
			cLastGenDt22.Text = string.Empty;
			cAuthRequired22.Checked = base.GetBool("Y");
			cWhereClause22.Text = string.Empty;
			cRegClause22.Text = string.Empty;
			cRegCode22.Text = string.Empty;
			cValClause22.Text = string.Empty;
			cValCode22.Text = string.Empty;
			cUpdClause22.Text = string.Empty;
			cUpdCode22.Text = string.Empty;
			cXlsClause22.Text = string.Empty;
			cXlsCode22.Text = string.Empty;
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmReport67List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmReport67List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmReport67List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmReport67ById",cAdmReport67List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmReport67List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cReportId22.Text = RO.Common3.Utils.fmNumeric("0",dr["ReportId22"].ToString(),base.LUser.Culture);} catch {cReportId22.Text = string.Empty;}
					try {cProgramName22.Text = dr["ProgramName22"].ToString();} catch {cProgramName22.Text = string.Empty;}
					SetReportTypeCd22(cReportTypeCd22,dr["ReportTypeCd22"].ToString()); if (cReportTypeCd22.SelectedIndex <= 0 && !(cReportTypeCd22.Enabled && cReportTypeCd22.Visible)) { SetReportTypeCd22(cReportTypeCd22,"S"); }
					SetOrientationCd22(cOrientationCd22,dr["OrientationCd22"].ToString());
					SetCopyReportId22(cCopyReportId22,dr["CopyReportId22"].ToString());
					SetModifiedBy22(cModifiedBy22,dr["ModifiedBy22"].ToString());
					try {cModifiedOn22.Text = RO.Common3.Utils.fmShortDateTimeUTC(dr["ModifiedOn22"].ToString(),base.LUser.Culture,CurrTimeZoneInfo());} catch {cModifiedOn22.Text = string.Empty;}
					try {cTemplateName22.Text = dr["TemplateName22"].ToString();} catch {cTemplateName22.Text = string.Empty;}
					SetRptTemplate22(cRptTemplate22GV,string.Empty);
					SetUnitCd22(cUnitCd22,dr["UnitCd22"].ToString());
					try {cTopMargin22.Text = RO.Common3.Utils.fmNumeric("2",dr["TopMargin22"].ToString(),base.LUser.Culture);} catch {cTopMargin22.Text = string.Empty;}
					try {cBottomMargin22.Text = RO.Common3.Utils.fmNumeric("2",dr["BottomMargin22"].ToString(),base.LUser.Culture);} catch {cBottomMargin22.Text = string.Empty;}
					try {cLeftMargin22.Text = RO.Common3.Utils.fmNumeric("2",dr["LeftMargin22"].ToString(),base.LUser.Culture);} catch {cLeftMargin22.Text = string.Empty;}
					try {cRightMargin22.Text = RO.Common3.Utils.fmNumeric("2",dr["RightMargin22"].ToString(),base.LUser.Culture);} catch {cRightMargin22.Text = string.Empty;}
					try {cPageWidth22.Text = RO.Common3.Utils.fmNumeric("2",dr["PageWidth22"].ToString(),base.LUser.Culture);} catch {cPageWidth22.Text = string.Empty;}
					try {cPageHeight22.Text = RO.Common3.Utils.fmNumeric("2",dr["PageHeight22"].ToString(),base.LUser.Culture);} catch {cPageHeight22.Text = string.Empty;}
					cSyncByDb.ImageUrl = "~/images/custom/adm/SyncByDb.gif";
					cSyncToDb.ImageUrl = "~/images/custom/adm/SyncToDb.gif";
					try {cAllowSelect22.Checked = base.GetBool(dr["AllowSelect22"].ToString());} catch {cAllowSelect22.Checked = false;}
					try {cGenerateRp22.Checked = base.GetBool(dr["GenerateRp22"].ToString());} catch {cGenerateRp22.Checked = false;}
					try {cLastGenDt22.Text = dr["LastGenDt22"].ToString();} catch {cLastGenDt22.Text = string.Empty;}
					try {cAuthRequired22.Checked = base.GetBool(dr["AuthRequired22"].ToString());} catch {cAuthRequired22.Checked = false;}
					try {cWhereClause22.Text = dr["WhereClause22"].ToString();} catch {cWhereClause22.Text = string.Empty;}
					try {cRegClause22.Text = dr["RegClause22"].ToString();} catch {cRegClause22.Text = string.Empty;}
					try {cRegCode22.Text = dr["RegCode22"].ToString();} catch {cRegCode22.Text = string.Empty;}
					try {cValClause22.Text = dr["ValClause22"].ToString();} catch {cValClause22.Text = string.Empty;}
					try {cValCode22.Text = dr["ValCode22"].ToString();} catch {cValCode22.Text = string.Empty;}
					try {cUpdClause22.Text = dr["UpdClause22"].ToString();} catch {cUpdClause22.Text = string.Empty;}
					try {cUpdCode22.Text = dr["UpdCode22"].ToString();} catch {cUpdCode22.Text = string.Empty;}
					try {cXlsClause22.Text = dr["XlsClause22"].ToString();} catch {cXlsClause22.Text = string.Empty;}
					try {cXlsCode22.Text = dr["XlsCode22"].ToString();} catch {cXlsCode22.Text = string.Empty;}
				}
			}
			cButPanel.DataBind(); if (!cSaveButton.Visible) { cInsRowButton.Visible = false; }
			DataTable dtAdmReportGrid = null;
			int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
			try
			{
				dtAdmReportGrid = (new AdminSystem()).GetDtlById(67,"GetAdmReport67DtlById",cAdmReport67List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,base.LImpr,base.LCurr);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (!dtAdmReportGrid.Columns.Contains("_NewRow")) { dtAdmReportGrid.Columns.Add("_NewRow"); }
			Session[KEY_dtAdmReportGrid] = dtAdmReportGrid;
			cAdmReportGrid.EditIndex = -1;
			cAdmReportGridDataPager.PageSize = Int16.Parse(cPgSize.Text); GotoPage(0);
			if (Session[KEY_lastSortCol] != null)
			{
				cAdmReportGrid_OnSorting(sender, new ListViewSortEventArgs((string)Session[KEY_lastSortExp], SortDirection.Ascending));
			}
			else if (dtAdmReportGrid.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dtAdmReportGrid.Rows.Count == 1))
			{
				cAdmReportGrid_DataBind(dtAdmReportGrid.DefaultView);
			}
			cFindButton_Click(sender, e);
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false; Session.Remove(KEY_scrImport);
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmReport67List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cRptTemplate22GV_PageIndexChanged(object sender, GridViewPageEventArgs e)
		{
			cRptTemplate22GV.PageIndex = e.NewPageIndex;
			cRptTemplate22GV.DataSource = ((DataTable)Session[KEY_dtRptTemplate22]).DefaultView; cRptTemplate22GV.DataBind();
		}

		protected void cRptTemplate22GV_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "deleterow" && cReportId22.Text != string.Empty)
			{
				try
				{
					(new AdminSystem()).DelDoc(cReportId22.Text, e.CommandArgument.ToString(), base.LUser.UsrId.ToString(), "RptTemplate", "Report", "RptTemplate", "ReportId",(string)Session[KEY_sysConnectionString],LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session.Remove(KEY_dtRptTemplate22); SetRptTemplate22(cRptTemplate22GV, string.Empty);
			}
		}

		protected void cSyncByDb_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//WebRule: Obtain s.procs from the physical database
            WebRule wr = new WebRule();
			string err = string.Empty;
			if (Config.AppNameSpace != "RO" && base.LCurr.DbId == 3) { bErrNow.Value = "Y"; PreMsgPopup("Please do not manipulate these administration stored procedures."); }
			else if (cReportId22.Text == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add this report definition first then try again."); }
			else
			{
				string sproc = string.Empty;
				string[] arr;
				arr = cRegClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrGetRptProc(arr[1].ToString().Replace("dbo.",string.Empty), base.LCurr.DbId, Session[KEY_sysConnectionString].ToString(), LcAppPw);
					if (err.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(err); } else if (err != string.Empty) { cRegCode22.Text = err; }
				}
				arr = cValClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrGetRptProc(arr[1].ToString().Replace("dbo.", string.Empty), base.LCurr.DbId, Session[KEY_sysConnectionString].ToString(), LcAppPw);
					if (err.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(err); } else if (err != string.Empty) { cValCode22.Text = err; }
				}
				arr = cUpdClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrGetRptProc(arr[1].ToString().Replace("dbo.", string.Empty), base.LCurr.DbId, Session[KEY_sysConnectionString].ToString(), LcAppPw);
					if (err.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(err); } else if (err != string.Empty) { cUpdCode22.Text = err; }
				}
				arr = cXlsClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrGetRptProc(arr[1].ToString().Replace("dbo.", string.Empty), base.LCurr.DbId, Session[KEY_sysConnectionString].ToString(), LcAppPw);
					if (err.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(err); } else if (err != string.Empty) { cXlsCode22.Text = err; }
				}
				if (bErrNow.Value == "N")
				{
					err = wr.WrUpdRptProc(cReportId22.Text, Session[KEY_sysConnectionString].ToString(), LcAppPw);
					if (err.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(err); }
					else
                    {
                        cLastGenDt22.Text = err;
                        try
                        {
                            SaveDb(sender, new EventArgs());
                            bInfoNow.Value = "Y"; PreMsgPopup("Stored procedures retrieved successfully from physical database.");
                        }
                        catch (Exception er) { bErrNow.Value = "Y"; PreMsgPopup(er.Message); return; }
                    }
				}
			}
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cSyncToDb_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//WebRule: Synchronize to the physical database
            string err = string.Empty;
			if (Config.AppNameSpace != "RO" && base.LCurr.DbId == 3) { bErrNow.Value = "Y"; PreMsgPopup("Please do not synchronize these administration stored procedures to physical database."); }
			else if (cReportId22.Text == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add this report definition first then try again."); }
			else if (cRegClause22.Text != string.Empty && cRegCode22.Text.Trim() == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add the stored procedure for Regular Fields and try again."); }
			else if (cValClause22.Text != string.Empty && cValCode22.Text.Trim() == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add the stored procedure for Parameters and try again."); }
			else if (cUpdClause22.Text != string.Empty && cUpdCode22.Text.Trim() == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add the stored procedure for Update and try again."); }
			else if (cXlsClause22.Text != string.Empty && cXlsCode22.Text.Trim() == string.Empty) { bErrNow.Value = "Y"; PreMsgPopup("Please add the stored procedure for Excel and try again."); }
			else
			{
				WebRule wr = new WebRule();
				DataView dv = new DataView(wr.WrGetReportApp(base.LCurr.DbId, Session[KEY_sysConnectionString].ToString(), LcAppPw));
				string[] arr;
				arr = cRegClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrSyncProc(arr[1].ToString(), cRegCode22.Text, Config.GetConnStr(dv[0]["dbProvider"].ToString(), dv[0]["dbServer"].ToString(), dv[0]["dbDatabase"].ToString(), "", dv[0]["dbUserId"].ToString()), LcAppPw);
				}
				arr = cValClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrSyncProc(arr[1].ToString(), cValCode22.Text, Config.GetConnStr(dv[0]["dbProvider"].ToString(), dv[0]["dbServer"].ToString(), dv[0]["dbDatabase"].ToString(), "", dv[0]["dbUserId"].ToString()), LcAppPw);
				}
				arr = cUpdClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrSyncProc(arr[1].ToString(), cUpdCode22.Text, Config.GetConnStr(dv[0]["dbProvider"].ToString(), dv[0]["dbServer"].ToString(), dv[0]["dbDatabase"].ToString(), "", dv[0]["dbUserId"].ToString()), LcAppPw);
				}
				arr = cXlsClause22.Text.Split(' ');
				if (arr.Length >= 2 && arr[0].ToString().IndexOf("EXEC") >= 0)
				{
					err = wr.WrSyncProc(arr[1].ToString(), cXlsCode22.Text, Config.GetConnStr(dv[0]["dbProvider"].ToString(), dv[0]["dbServer"].ToString(), dv[0]["dbDatabase"].ToString(), "", dv[0]["dbUserId"].ToString()), LcAppPw);
				}
				if (err != string.Empty) { bErrNow.Value = "Y"; PreMsgPopup(err); }
				else
				{
					cLastGenDt22.Text = wr.WrUpdRptProc(cReportId22.Text, Session[KEY_sysConnectionString].ToString(), LcAppPw);
                    try
                    {
                        SaveDb(sender, new EventArgs());
                        bInfoNow.Value = "Y"; PreMsgPopup("Stored procedures synchronized to the physical database successfully.");
                    }
                    catch (Exception er) { bErrNow.Value = "Y"; PreMsgPopup(er.Message); return; }
				}
			}
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		public void cFindButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				WebControl cc = sender as WebControl;
				if ((cc != null && cc.ID.Equals("cAdmReport67List")) || cAdmReportGrid.EditIndex < 0 || UpdateGridRow(cAdmReportGrid, new CommandEventArgs("Save", "")))
				{
					string rf = string.Empty;
					if (cFind.Text != string.Empty) { rf = "(" + base.GetExpression(cFind.Text.Trim(), GetAuthCol(), 31, cFindFilter.SelectedValue) + ")"; }
					if (rf != string.Empty) { rf = "((" + rf + " or _NewRow = 'Y' ))"; }
					dv.RowFilter = rf;
					ViewState["_RowFilter"] = rf;
					GotoPage(0); cAdmReportGrid_DataBind(dv);
					if (GetCurrPageIndex() != (int)Session[KEY_currPageIndex] && (int)Session[KEY_currPageIndex] < GetTotalPages()) { GotoPage((int)Session[KEY_currPageIndex]); cAdmReportGrid_DataBind(dv);}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (dt != null && (ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")))
			{
				DataRow dr = dt.NewRow();
				int? sortorder = null;
				dr["CultureId96"] = 1;
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
				Session[KEY_lastAddedRow] = 0; Session[KEY_dtAdmReportGrid] = dt; Session[KEY_currPageIndex] = 0; GotoPage(0);
				cAdmReportGrid_DataBind(dt.DefaultView);
				cAdmReportGrid_OnItemEditing(cAdmReportGrid, new ListViewEditEventArgs(0));
				// *** Default Value (Grid) Web Rule starts here *** //
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPgSizeButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (cAdmReportGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				try {if (int.Parse(cPgSize.Text) < 1 || int.Parse(cPgSize.Text) > 200) {cPgSize.Text = "2";}} catch {cPgSize.Text = "2";}
				cAdmReportGridDataPager.PageSize = int.Parse(cPgSize.Text); GotoPage(0);
				if (dt.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dt.Rows.Count == 1)) {cAdmReportGrid_DataBind(dt.DefaultView);} else {cFindButton_Click(sender, e);}
				try
				{
					(new AdminSystem()).UpdLastPageInfo(67, base.LUser.UsrId, cPgSize.Text, LcSysConnString, LcAppPw);
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
					Session["ImportSchema"] = (new AdminSystem()).GetSchemaScrImp(67,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "SearchLink('ImportSchema.aspx?scm=S&key=67&csy=3','','',''); return false;";}
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void ScrImportDdl(DataView dvd, DataRowView drv, bool bComboBox, string PKey, string CKey, string CNam, int MaxLen, string MatchCd, bool bAllowNulls)
		{
			if (dvd != null)
			{
				if (dvd.Table.Columns.Contains(PKey))
				{
					if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue))
					{
						dvd.RowFilter = "(" + PKey + " is null)";
					}
					else
					{
						dvd.RowFilter = "(" + PKey + " is null OR " + PKey + " = " + cAdmReport67List.SelectedValue + ")";
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
			DataTable dtAdmReportGrid = (DataTable)Session[KEY_dtAdmReportGrid];
			if (dti != null && dtAdmReportGrid != null)
			{
				if (bClear)
				{
					dtAdmReportGrid.Rows.Clear();
				}
				cFind.Text = string.Empty;
				int iExist = dtAdmReportGrid.Rows.Count;
				// Validate dropdown, combobox, etc.
				int jj = 0;
				foreach (DataRowView drv in dti.DefaultView)
				{
					if ((drv.Row.RowState == System.Data.DataRowState.Added || drv.Row.RowState == System.Data.DataRowState.Detached) && jj >= iExist)
					{
						DataTable dtd;
						dtd = jj==iExist ? null : (DataTable)Session[KEY_dtCultureId96];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(67,"GetDdlCultureId3S1022",true,true,0,string.Empty,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtCultureId96] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, true, "ReportId", "CultureId96", "CultureId96Text", 50, "2", false);
					}
					jj = jj + 1;
				}
				iRow = dti.DefaultView.Count - iExist;
				if (!dti.Columns.Contains("_NewRow")) { dti.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmReportGrid] = dti;
				cAdmReportGrid_DataBind(dti.DefaultView);
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
				DataTable dtAdmReportGrid = (DataTable)Session[KEY_dtAdmReportGrid];
				try
				{
					if (cAdmReportGrid.EditIndex < 0 || UpdateGridRow(cAdmReportGrid, new CommandEventArgs("Save", "")))
					{
						Session[KEY_scrImport] = ImportFile(dtAdmReportGrid.Copy(),cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
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
					if (rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty && rows[iRow][3].ToString() == string.Empty && rows[iRow][4].ToString() == string.Empty) {idel = idel + 1;}
					else
					{
						dt.Rows.Add(dt.NewRow());
						if (rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty) { rows[iRow][1] = 1;}
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
				if (cAdmReportGrid.EditIndex < 0 || UpdateGridRow(cAdmReportGrid, new CommandEventArgs("Save", "")))
				{
					try { cAdmReportGridDataPager.SetPageProperties(cAdmReportGridDataPager.PageSize * pageNo, cAdmReportGridDataPager.MaximumRows, false); cAdmReportGrid_DataBind(null); }
					catch
					{
						try { cAdmReportGridDataPager.SetPageProperties(0, cAdmReportGridDataPager.MaximumRows, false); cAdmReportGrid_DataBind(null); } catch {}
					}
				}
			}
		}

		protected void cAdmReportGrid_OnItemCommand(object sender, ListViewCommandEventArgs e)
		{
		}

		private void cAdmReportGrid_DataBind(DataView dvAdmReportGrid)
		{
			if (dvAdmReportGrid == null)
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
				dvAdmReportGrid = dt != null ? dt.DefaultView : null;
			}
			if (dvAdmReportGrid != null)
			{
				LcAuth = GetAuthCol().AsEnumerable().ToDictionary<DataRow,string>(dr=>dr["ColName"].ToString());
				cAdmReportGrid.DataSource = dvAdmReportGrid;
				cAdmReportGrid.DataBind();
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
					if (ii >= 31 && !string.IsNullOrEmpty(dr["ColumnHeader"].ToString()) && !string.IsNullOrEmpty(dr["TableId"].ToString()) && dtAuth.Rows[ii]["ColVisible"].ToString() == "Y")
					{
						li = new ListItem();
						li.Value = ii.ToString(); li.Text = dr["ColumnHeader"].ToString().Replace("*", string.Empty);
						cFindFilter.Items.Add(li);
					}
					ii = ii + 1;
				}
			}
		}

		protected void cAdmReportGrid_OnSorting(object sender, ListViewSortEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmReportGrid.EditIndex < 0 || UpdateGridRow(cAdmReportGrid, new CommandEventArgs("Save", "")))
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
					Session[KEY_dtAdmReportGrid] = dt;
					cAdmReportGrid_DataBind(dv);
				}
			}
		}

		protected void cAdmReportGrid_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
		    cAdmReportGridDataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
		    DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
		    DataView dv = dt != null ? dt.DefaultView : null;
			cAdmReportGrid_DataBind(dv); cAdmReportGrid.EditIndex = -1;
		}

		private void GridChkPgDirty(ListViewItem lvi)
		{
				WebControl cc = null;
				cc = ((WebControl)lvi.FindControl("cCultureId96"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDefaultHlpMsg96"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cReportTitle96"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
		}

		protected void cAdmReportGrid_OnItemDataBound(object sender, ListViewItemEventArgs e)
		{
			// *** GridItemDataBound (before) Web Rule End *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			bool isEditItem = false;
			bool isImage = true;
			bool hasImageContent = false;
			DataView dvAdmReportGrid = dt != null ? dt.DefaultView : null;
			if (cAdmReportGrid.EditIndex > -1 && GetDataItemIndex(cAdmReportGrid.EditIndex) == e.Item.DataItemIndex)
			{
				isEditItem = true;
				base.SetGridEnabled(e.Item, GetAuthCol(), GetLabel(), 31);
				if (dvAdmReportGrid[e.Item.DataItemIndex]["CultureId96"].ToString() != string.Empty) {SetCultureId96((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cCultureId96"),dvAdmReportGrid[e.Item.DataItemIndex]["CultureId96"].ToString(), e.Item);} else {SetCultureId96((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cCultureId96"),"1", e.Item);}
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
					cc = e.Item.FindControl("cAdmReportGridDelete"); if (cc != null) { cc.Visible = false; }
					cc = e.Item.FindControl("cAdmReportGridEdit"); if (cc != null) { cc.Visible = false; }
				}
				else
				{
			        HtmlTableRow tr = e.Item.FindControl("cAdmReportGridRow") as HtmlTableRow;
			        LinkButton lb = e.Item.FindControl("cAdmReportGridEdit") as LinkButton;
			        if (tr != null && lb != null) { SetDefaultCtrl(tr, lb, string.Empty); }
				}
			}
			if (cAdmReportGrid.EditIndex > -1 && GetDataItemIndex(cAdmReportGrid.EditIndex) == e.Item.DataItemIndex)
			{
			}
			else
			{
			}
			// *** GridItemDataBound (after) Web Rule End *** //
		}

		protected void cReportHlpId96hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "0") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "0"; Session[KEY_lastSortExp] = "ReportHlpId96";Session[KEY_lastSortImg] = "cReportHlpId96hi";
			cAdmReportGrid_OnSorting(sender, new ListViewSortEventArgs("ReportHlpId96", SortDirection.Ascending));
		}

		protected void cCultureId96hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "1") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "1"; Session[KEY_lastSortExp] = "CultureId96Text";Session[KEY_lastSortImg] = "cCultureId96hi";
			cAdmReportGrid_OnSorting(sender, new ListViewSortEventArgs("CultureId96Text", SortDirection.Ascending));
		}

		protected void cDefaultHlpMsg96hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "2") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "2"; Session[KEY_lastSortExp] = "DefaultHlpMsg96";Session[KEY_lastSortImg] = "cDefaultHlpMsg96hi";
			cAdmReportGrid_OnSorting(sender, new ListViewSortEventArgs("DefaultHlpMsg96", SortDirection.Ascending));
		}

		protected void cReportTitle96hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "3") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "3"; Session[KEY_lastSortExp] = "ReportTitle96";Session[KEY_lastSortImg] = "cReportTitle96hi";
			cAdmReportGrid_OnSorting(sender, new ListViewSortEventArgs("ReportTitle96", SortDirection.Ascending));
		}

		protected void cAdmReportGrid_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if ((ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")) && dt != null && dt.DefaultView.Count > GetDataItemIndex(e.NewEditIndex))
			{
				cAdmReportGrid.EditIndex = e.NewEditIndex;
				cAdmReportGrid_DataBind(null);
				Control wc = null;
				string idx = Page.Request["__EVENTARGUMENT"];
				if (!string.IsNullOrEmpty(idx))
				{
				    Control ctrlToFocus = cAdmReportGrid.EditItem.FindControl("c" + idx);
				    if (((WebControl)ctrlToFocus).Enabled) { wc = ctrlToFocus; }
				}
				if (wc == null) { wc = FindEditableControl(cAdmReportGrid.EditItem); }
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
             MkMobileLabel(cAdmReportGrid.EditItem);
		    }
		    else { e.Cancel = true; }
		}

		private void MkMobileLabel(ListViewItem lvi)
		{
		    Label ml = null;
		    ml = lvi.FindControl("cReportHlpId96ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(31); }
		    ml = lvi.FindControl("cCultureId96ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(32); }
		    ml = lvi.FindControl("cDefaultHlpMsg96ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(33); }
		    ml = lvi.FindControl("cReportTitle96ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(34); }
		}

		protected void GridFill(ListViewItem lvi, DataTable dt, DataRow dr, bool bInsert)
		{
			RoboCoder.WebControls.ComboBox cbb = null;
			cbb = (RoboCoder.WebControls.ComboBox)lvi.FindControl("cCultureId96");
			if (cbb != null)
			{
				if (cbb.SelectedValue != string.Empty)
				{
					dr["CultureId96"] = cbb.SelectedValue;
					dr["CultureId96Text"] = cbb.SelectedText;
				}
				else
				{
					dr["CultureId96"] = System.DBNull.Value;
					dr["CultureId96Text"] = System.DBNull.Value;
				}
			}
			TextBox tb = null;
			tb = (TextBox)lvi.FindControl("cDefaultHlpMsg96");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DefaultHlpMsg96"] = tb.Text;} else {dr["DefaultHlpMsg96"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cReportTitle96");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ReportTitle96"] = tb.Text;} else {dr["ReportTitle96"] = System.DBNull.Value;}
			}
		    if (bInsert) { dt.Rows.InsertAt(dr, 0); }
			Session[KEY_dtAdmReportGrid] = dt;
			cAdmReportGrid.EditIndex = -1;
			cAdmReportGrid_DataBind(dt.DefaultView);
		}

		protected void cAdmReportGrid_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			if (dt != null && dt.Rows.Count > 0)
			{
				DataRow dr = dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row;
			    GridFill(cAdmReportGrid.EditItem, dt, dr, false);
			    dr["_NewRow"] = "N";
			    if (dt.Columns.Contains("_SortOrder")) dr["_SortOrder"] = null;
			}
		}

		protected void cAdmReportGrid_OnItemCanceling(object sender, ListViewCancelEventArgs e)
		{
			PanelTop.Update();
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && dv[GetDataItemIndex(e.ItemIndex)]["_NewRow"].ToString() == "Y")
			{
				cAdmReportGrid_OnItemDeleting(sender, new ListViewDeleteEventArgs(e.ItemIndex));
			}
			if (dt.Columns.Contains("_SortOrder")) dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row["_SortOrder"] = null;
			cAdmReportGrid.EditIndex = -1; cAdmReportGrid_DataBind(null);
		}

		protected void cDeleteAllButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && (cAdmReportGrid.EditIndex < 0 || UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				GotoPage(0);
				while (dv.Count > 0) {dv.Delete(0);}
				Session[KEY_dtAdmReportGrid] = dt;
				cAdmReportGrid.EditIndex = -1; cAdmReportGrid_DataBind(dv);
			}
		}

		protected void cAdmReportGrid_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			DataRow dr = dv != null ? dv[GetDataItemIndex(e.ItemIndex)].Row : null;
			if (dv != null && (cAdmReportGrid.EditIndex == e.ItemIndex || UpdateGridRow(sender, new CommandEventArgs("Save",""))))
			{
				// *** Delete Grid Row (before) Web Rule End *** //
				dr.Delete();
				Session[KEY_dtAdmReportGrid] = dt;
				cAdmReportGrid.EditIndex = -1; cAdmReportGrid_DataBind(dv);
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
			cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
			PopAdmReport67List(sender, e, false, null);
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
			cReportId22.Text = string.Empty;
			cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
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
			if (cReportId22.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmReport67List(sender, e, false, cReportId22.Text); }
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
			Session.Remove(KEY_dtAdmReport67List); PopAdmReport67List(sender, e, false, null);
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
			//WebRule: Signal create program needed upon program name change
bool bRegenNeeded = false;
            if (cAdmReport67List.SelectedValue != string.Empty && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded(cProgramName22.Text, 0, Int32.Parse(cAdmReport67List.SelectedValue), 0, (string)Session[KEY_sysConnectionString], LcAppPw)) { bRegenNeeded = true; }
			// *** WebRule End *** //
			string pid = string.Empty;
			if (ValidPage() && (cAdmReportGrid.EditIndex < 0 || UpdateGridRow(cAdmReportGrid, new CommandEventArgs("Save", ""))))
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmReportGrid];
				DataView dv = dt != null ? dt.DefaultView : null;
				string ftr = string.Empty;
				if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
				AdmReport67 ds = PrepAdmReportData(dv,cReportId22.Text == string.Empty);
				if (ftr != string.Empty) {dv.RowFilter = ftr;}
				if (string.IsNullOrEmpty(cAdmReport67List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(67,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmReport67List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(67,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
						Session[KEY_currPageIndex] = GetCurrPageIndex();
						ShowDirty(false); PopAdmReport67List(sender, e, false, ds.Tables["AdmReport"].Rows[0]["ReportId22"]);
						rtn = GetScreenHlp().Rows[0]["UpdMsg"].ToString();
					}
				}
			}
			//WebRule: Create program upon program name change or add report
			if (bRegenNeeded || pid != string.Empty)		// ProgramName changed or a new report has been added successfully.
			{
				try
				{
					if (pid == string.Empty) { pid = cReportId22.Text; }
					DataTable dtTle = (new SqlReportSystem()).GetRptHlp(string.Empty,int.Parse(pid), base.LUser.CultureId, (string)Session[KEY_sysConnectionString], LcAppPw);
					DataRow row = base.SystemsList.Rows[cSystemId.SelectedIndex];
					base.CSrc = new CurrSrc(true, row); base.CTar = new CurrTar(true, row);
					(new GenReportsSystem()).CreateProgram(string.Empty, int.Parse(pid), dtTle.Rows[0]["ReportTitle"].ToString(), row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					(new MenuSystem()).NewMenuItem(0, int.Parse(pid), 0, dtTle.Rows[0]["ReportTitle"].ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
					Response.Redirect(Request.RawUrl);
				}
				catch {}
			}
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
			if (cReportId22.Text != string.Empty)
			{
				AdmReport67 ds = PrepAdmReportData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(67,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc))
					{
						cAdmReport67List.ClearSearch(); Session.Remove(KEY_dtAdmReport67List);
						ShowDirty(false); PopAdmReport67List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmReport67 PrepAdmReportData(DataView dv, bool bAdd)
		{
			AdmReport67 ds = new AdmReport67();
			DataRow dr = ds.Tables["AdmReport"].NewRow();
			DataRow drType = ds.Tables["AdmReport"].NewRow();
			DataRow drDisp = ds.Tables["AdmReport"].NewRow();
			if (bAdd) { dr["ReportId22"] = string.Empty; } else { dr["ReportId22"] = cReportId22.Text; }
			drType["ReportId22"] = "Numeric"; drDisp["ReportId22"] = "TextBox";
			try {dr["ProgramName22"] = cProgramName22.Text.Trim();} catch {}
			drType["ProgramName22"] = "VarChar"; drDisp["ProgramName22"] = "TextBox";
			try {dr["ReportTypeCd22"] = cReportTypeCd22.SelectedValue;} catch {}
			drType["ReportTypeCd22"] = "Char"; drDisp["ReportTypeCd22"] = "DropDownList";
			try {dr["OrientationCd22"] = cOrientationCd22.SelectedValue;} catch {}
			drType["OrientationCd22"] = "Char"; drDisp["OrientationCd22"] = "RadioButtonList";
			try {dr["CopyReportId22"] = cCopyReportId22.SelectedValue;} catch {}
			drType["CopyReportId22"] = "Numeric"; drDisp["CopyReportId22"] = "AutoComplete";
			try {dr["ModifiedBy22"] = base.LUser.UsrId.ToString();} catch {};
			drType["ModifiedBy22"] = "Numeric"; drDisp["ModifiedBy22"] = "DropDownList";
			try {dr["TemplateName22"] = cTemplateName22.Text.Trim();} catch {}
			drType["TemplateName22"] = "VarChar"; drDisp["TemplateName22"] = "TextBox";
			try {dr["RptTemplate22"] = string.Empty;} catch {}
			drType["RptTemplate22"] = "Numeric"; drDisp["RptTemplate22"] = "Document";
			try {dr["UnitCd22"] = cUnitCd22.SelectedValue;} catch {}
			drType["UnitCd22"] = "Char"; drDisp["UnitCd22"] = "DropDownList";
			try {dr["TopMargin22"] = cTopMargin22.Text.Trim();} catch {}
			drType["TopMargin22"] = "Decimal"; drDisp["TopMargin22"] = "TextBox";
			try {dr["BottomMargin22"] = cBottomMargin22.Text.Trim();} catch {}
			drType["BottomMargin22"] = "Decimal"; drDisp["BottomMargin22"] = "TextBox";
			try {dr["LeftMargin22"] = cLeftMargin22.Text.Trim();} catch {}
			drType["LeftMargin22"] = "Decimal"; drDisp["LeftMargin22"] = "TextBox";
			try {dr["RightMargin22"] = cRightMargin22.Text.Trim();} catch {}
			drType["RightMargin22"] = "Decimal"; drDisp["RightMargin22"] = "TextBox";
			try {dr["PageWidth22"] = cPageWidth22.Text.Trim();} catch {}
			drType["PageWidth22"] = "Decimal"; drDisp["PageWidth22"] = "TextBox";
			try {dr["PageHeight22"] = cPageHeight22.Text.Trim();} catch {}
			drType["PageHeight22"] = "Decimal"; drDisp["PageHeight22"] = "TextBox";
			try {dr["AllowSelect22"] = base.SetBool(cAllowSelect22.Checked);} catch {}
			drType["AllowSelect22"] = "Char"; drDisp["AllowSelect22"] = "CheckBox";
			try {dr["GenerateRp22"] = base.SetBool(cGenerateRp22.Checked);} catch {}
			drType["GenerateRp22"] = "Char"; drDisp["GenerateRp22"] = "CheckBox";
			try {dr["LastGenDt22"] = cLastGenDt22.Text.Trim();} catch {}
			drType["LastGenDt22"] = "DBTimeStamp"; drDisp["LastGenDt22"] = "TextBox";
			try {dr["AuthRequired22"] = base.SetBool(cAuthRequired22.Checked);} catch {}
			drType["AuthRequired22"] = "Char"; drDisp["AuthRequired22"] = "CheckBox";
			try {dr["WhereClause22"] = cWhereClause22.Text;} catch {}
			drType["WhereClause22"] = "VarChar"; drDisp["WhereClause22"] = "MultiLine";
			try {dr["RegClause22"] = cRegClause22.Text;} catch {}
			drType["RegClause22"] = "VarChar"; drDisp["RegClause22"] = "MultiLine";
			try {dr["RegCode22"] = cRegCode22.Text;} catch {}
			drType["RegCode22"] = "VarWChar"; drDisp["RegCode22"] = "MultiLine";
			try {dr["ValClause22"] = cValClause22.Text;} catch {}
			drType["ValClause22"] = "VarChar"; drDisp["ValClause22"] = "MultiLine";
			try {dr["ValCode22"] = cValCode22.Text;} catch {}
			drType["ValCode22"] = "VarWChar"; drDisp["ValCode22"] = "MultiLine";
			try {dr["UpdClause22"] = cUpdClause22.Text;} catch {}
			drType["UpdClause22"] = "VarChar"; drDisp["UpdClause22"] = "MultiLine";
			try {dr["UpdCode22"] = cUpdCode22.Text;} catch {}
			drType["UpdCode22"] = "VarWChar"; drDisp["UpdCode22"] = "MultiLine";
			try {dr["XlsClause22"] = cXlsClause22.Text;} catch {}
			drType["XlsClause22"] = "VarChar"; drDisp["XlsClause22"] = "MultiLine";
			try {dr["XlsCode22"] = cXlsCode22.Text;} catch {}
			drType["XlsCode22"] = "VarWChar"; drDisp["XlsCode22"] = "MultiLine";
			if (dv != null)
			{
				ds.Tables["AdmReportDef"].Rows.Add(MakeTypRow(ds.Tables["AdmReportDef"].NewRow()));
				ds.Tables["AdmReportDef"].Rows.Add(MakeDisRow(ds.Tables["AdmReportDef"].NewRow()));
				if (bAdd)
				{
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReportAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReportAdd"].NewRow(), drv, true));
					}
				}
				else
				{
					dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReportUpd"].Rows.Add(MakeColRow(ds.Tables["AdmReportUpd"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.Added;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReportAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReportAdd"].NewRow(), drv, true));
					}
					dv.RowStateFilter = DataViewRowState.Deleted;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmReportDel"].Rows.Add(MakeColRow(ds.Tables["AdmReportDel"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.CurrentRows;
				}
			}
			ds.Tables["AdmReport"].Rows.Add(dr); ds.Tables["AdmReport"].Rows.Add(drType); ds.Tables["AdmReport"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmReport67List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtReportTypeCd22];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ReportTypeCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtOrientationCd22];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'OrientationCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtUnitCd22];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'UnitCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtCultureId96];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'CultureId', please investigate."); return false;
			}
			return true;
		}

		private DataRow MakeTypRow(DataRow dr)
		{
			dr["ReportId22"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ReportHlpId96"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["CultureId96"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["DefaultHlpMsg96"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["ReportTitle96"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			return dr;
		}

		private DataRow MakeDisRow(DataRow dr)
		{
			dr["ReportId22"] = "TextBox";
			dr["ReportHlpId96"] = "TextBox";
			dr["CultureId96"] = "AutoComplete";
			dr["DefaultHlpMsg96"] = "TextBox";
			dr["ReportTitle96"] = "TextBox";
			return dr;
		}

		private DataRow MakeColRow(DataRow dr, DataRowView drv, bool bAdd)
		{
			dr["ReportId22"] = cReportId22.Text;
			DataTable dtAuth = GetAuthCol();
			if (dtAuth != null)
			{
				dr["ReportHlpId96"] = drv["ReportHlpId96"].ToString().Trim();
				dr["CultureId96"] = drv["CultureId96"];
				dr["DefaultHlpMsg96"] = drv["DefaultHlpMsg96"].ToString().Trim();
				dr["ReportTitle96"] = drv["ReportTitle96"].ToString().Trim();
			}
			return dr;
		}

		private bool UpdateGridRow(object sender, CommandEventArgs e)
		{
			if (cAdmReportGrid.EditIndex > -1)
			{
				TextBox pwd = null;				cAdmReportGrid_OnItemUpdating(sender, new ListViewUpdateEventArgs(cAdmReportGrid.EditIndex));
			}
			return true;
		}

		protected void cAdmReportGrid_OnPreRender(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Image hi = null;
			hi = (System.Web.UI.WebControls.Image)cAdmReportGrid.FindControl("cReportHlpId96hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReportGrid.FindControl("cCultureId96hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReportGrid.FindControl("cDefaultHlpMsg96hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmReportGrid.FindControl("cReportTitle96hi"); if (hi != null) { hi.Visible = false; }
			if (Session[KEY_lastSortImg] != null)
			{
				hi = (System.Web.UI.WebControls.Image)cAdmReportGrid.FindControl((string)Session[KEY_lastSortImg]);
				if (hi != null) { hi.ImageUrl = Utils.AddTilde((string)Session[KEY_lastSortUrl]); hi.Visible = true; }
			}
			IgnoreHeaderConfirm((LinkButton)cAdmReportGrid.FindControl("cReportHlpId96hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReportGrid.FindControl("cCultureId96hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReportGrid.FindControl("cDefaultHlpMsg96hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmReportGrid.FindControl("cReportTitle96hl"));
		}

		private string GetButtonId(ListViewItem lvi)
		{
			string ButtonID = String.Empty;
			Control c = lvi.FindControl("cAdmReportGridEdit");
			if (c != null) { ButtonID = c.UniqueID; }
			return ButtonID;
		}

		private void SetDefaultCtrl(HtmlTableRow tr, LinkButton lb, string ctrlId)
		{
			tr.Attributes.Add("onclick", "document.getElementById('" + bConfirm.ClientID + "').value='N'; fFocusedEdit('" + lb.UniqueID + "','" + ctrlId + "',event);");
		}

		private int GetCurrPageIndex()
		{
		    return cAdmReportGridDataPager.StartRowIndex / cAdmReportGridDataPager.PageSize;
		}

		private int GetTotalPages()
		{
		    return (int)Math.Ceiling((double)cAdmReportGridDataPager.TotalRowCount / cAdmReportGridDataPager.PageSize);
		}

		private int GetDataItemIndex(int editIndex)
		{
		    return cAdmReportGridDataPager.StartRowIndex + editIndex;
		}

		protected void cAdmReportGrid_OnLayoutCreated(object sender, EventArgs e)
		{
		    // Header:
		    LinkButton lb = null;
		    lb = cAdmReportGrid.FindControl("cReportHlpId96hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(31); lb.ToolTip = ColumnToolTip(31); lb.Parent.Visible = GridColumnVisible(31); }
		    lb = cAdmReportGrid.FindControl("cCultureId96hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(32); lb.ToolTip = ColumnToolTip(32); lb.Parent.Visible = GridColumnVisible(32); }
		    lb = cAdmReportGrid.FindControl("cDefaultHlpMsg96hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(33); lb.ToolTip = ColumnToolTip(33); lb.Parent.Visible = GridColumnVisible(33); }
		    lb = cAdmReportGrid.FindControl("cReportTitle96hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(34); lb.ToolTip = ColumnToolTip(34); lb.Parent.Visible = GridColumnVisible(34); }
		    // Hide DeleteAll:
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if ((dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
		            lb = cAdmReportGrid.FindControl("cDeleteAllButton") as LinkButton; if (lb != null) { lb.Visible = false; }
				}
			}
		    // footer:
		    Label gc = null;
		    gc = cAdmReportGrid.FindControl("cReportHlpId96fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(31); }
		    gc = cAdmReportGrid.FindControl("cCultureId96fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(32); }
		    gc = cAdmReportGrid.FindControl("cDefaultHlpMsg96fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(33); }
		    gc = cAdmReportGrid.FindControl("cReportTitle96fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(34); }
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmReport67List,null); }

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

