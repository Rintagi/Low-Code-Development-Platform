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
	public class AdmPageObj1001 : DataSet
	{
		public AdmPageObj1001()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmPageObj")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjDef")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjAdd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjUpd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjDel")));
			this.DataSetName = "AdmPageObj1001";
			this.Namespace = "http://Rintagi.com/DataSet/AdmPageObj1001";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("PageObjId1277", typeof(string));
			columns.Add("SectionCd1277", typeof(string));
			columns.Add("GroupRowId1277", typeof(string));
			columns.Add("GroupColId1277", typeof(string));
			columns.Add("LinkTypeCd1277", typeof(string));
			columns.Add("PageObjOrd1277", typeof(string));
			columns.Add("SctGrpRow1277", typeof(string));
			columns.Add("SctGrpCol1277", typeof(string));
			columns.Add("PageObjCss1277", typeof(string));
			columns.Add("PageObjSrp1277", typeof(string));
			columns.Add("BtnDefault", typeof(string));
			columns.Add("BtnHeader", typeof(string));
			columns.Add("BtnFooter", typeof(string));
			columns.Add("BtnSidebar", typeof(string));
			return dt;
		}

		private DataTable MakeDtlColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("PageObjId1277", typeof(string));
			columns.Add("PageLnkId1278", typeof(string));
			columns.Add("PageLnkTxt1278", typeof(string));
			columns.Add("PageLnkRef1278", typeof(string));
			columns.Add("PageLnkImg1278", typeof(string));
			columns.Add("PageLnkAlt1278", typeof(string));
			columns.Add("PageLnkOrd1278", typeof(string));
			columns.Add("Popup1278", typeof(string));
			columns.Add("PageLnkCss1278", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmPageObjModule : RO.Web.ModuleBase
	{

		private const string KEY_lastAddedRow = "Cache:lastAddedRow3_1001";
		private const string KEY_lastSortOrd = "Cache:lastSortOrd3_1001";
		private const string KEY_lastSortImg = "Cache:lastSortImg3_1001";
		private const string KEY_lastSortCol = "Cache:lastSortCol3_1001";
		private const string KEY_lastSortExp = "Cache:lastSortExp3_1001";
		private const string KEY_lastSortUrl = "Cache:lastSortUrl3_1001";
		private const string KEY_lastSortTog = "Cache:lastSortTog3_1001";
		private const string KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride3_1001";
		private const string KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride3_1001";
		private const string KEY_currPageIndex = "Cache:currPageIndex3_1001";
		private const string KEY_bHiImpVisible = "Cache:bHiImpVisible3_1001";
		private const string KEY_bShImpVisible = "Cache:bShImpVisible3_1001";
		private const string KEY_dtAdmPageObjGrid = "Cache:dtAdmPageObjGrid";
		private const string KEY_scrImport = "Cache:scrImport3_1001";
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_1001";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_1001";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_1001";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_1001";
		private const string KEY_dtLabel = "Cache:dtLabel3_1001";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_1001";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_1001";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_1001";

		private const string KEY_dtSectionCd1277 = "Cache:dtSectionCd1277";
		private const string KEY_dtGroupRowId1277 = "Cache:dtGroupRowId1277";
		private const string KEY_dtGroupColId1277 = "Cache:dtGroupColId1277";
		private const string KEY_dtLinkTypeCd1277 = "Cache:dtLinkTypeCd1277";

		private const string KEY_dtSystems = "Cache:dtSystems3_1001";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_1001";
		private const string KEY_dtAdmPageObj1001List = "Cache:dtAdmPageObj1001List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_1001";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_1001";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_1001";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_1001";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_1001";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_1001";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_1001";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_1001";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmPageObjModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,1001, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				cFind.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cFindButton.ClientID + "')");
				cPgSize.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cPgSizeButton.ClientID + "')");
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
				Session.Remove(KEY_dtAdmPageObjGrid);
				Session.Remove(KEY_lastSortCol);
				Session.Remove(KEY_lastSortExp);
				Session.Remove(KEY_lastSortImg);
				Session.Remove(KEY_lastSortUrl);
				Session.Remove(KEY_dtAdmPageObj1001List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtSectionCd1277);
				Session.Remove(KEY_dtGroupRowId1277);
				Session.Remove(KEY_dtGroupColId1277);
				Session.Remove(KEY_dtLinkTypeCd1277);
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
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 1001, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmPageObj1001List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=1001&typ=N&sys=3','','',''); return false;"; }
				//WebRule: Hide irrelevant Module dropdown
                cSystem.Visible = false;
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
				DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
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
					dt = (new AdminSystem()).GetAuthRow(1001,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
					Session[KEY_dtAuthRow] = dt;
					DataRow dr = dt.Rows[0];
					if (!((dr["AllowUpd"].ToString() == "N" || dr["ViewOnly"].ToString() == "G") && dr["AllowAdd"].ToString() == "N") && (Request.QueryString["enb"] == null || Request.QueryString["enb"] != "N"))
					{
						cAdmPageObjGrid.PreRender += new EventHandler(cAdmPageObjGrid_OnPreRender);
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			cGridUploadBtn.Click += cPageLnkImg1278Upl_Click;
			cGridUploadBtn.Click += cPageLnkAlt1278Upl_Click;

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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,1001,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(1001, "Section Object", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(1001,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtRul = (new AdminSystem()).GetClientRule(1001,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					if (ee == null || drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on" || (cAdmPageObjGrid.EditIndex > -1 && GetDataItemIndex(cAdmPageObjGrid.EditIndex) == ee.DataItemIndex))
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(1001,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(1001,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,1001,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(1001,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetScreenLabel(1001,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(1001,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(1001,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(1001,"GetExpAdmPageObj1001","Y",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(1001,"GetExpAdmPageObj1001","N",null,null,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(1001,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[1]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[2]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[3]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[4]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[14]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[15]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[16]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[17]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[18]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[19]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[20]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[21]["ColumnHeader"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageObjId1277"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SectionCd1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["SectionCd1277Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupRowId1277"].ToString() + (char)9 + drv["GroupRowId1277Text"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupColId1277"].ToString() + (char)9 + drv["GroupColId1277Text"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["LinkTypeCd1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["LinkTypeCd1277Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageObjOrd1277"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SctGrpRow1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SctGrpCol1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageObjCss1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageObjSrp1277"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageLnkId1278"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageLnkTxt1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageLnkRef1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageLnkImg1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageLnkAlt1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageLnkOrd1278"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["Popup1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["PageLnkCss1278"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmPageObj.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmPageObj.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(1001,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageObjId1277"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["SectionCd1277Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupRowId1277Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["GroupColId1277Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["LinkTypeCd1277Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageObjOrd1277"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["SctGrpRow1277"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["SctGrpCol1277"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["PageObjCss1277"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["PageObjSrp1277"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageLnkId1278"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(drv["PageLnkTxt1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["PageLnkRef1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["PageLnkImg1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["PageLnkAlt1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["PageLnkOrd1278"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["Popup1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["PageLnkCss1278"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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

		public void cPageLnkImg1278Upl_Click(object sender, System.EventArgs e)
		{
			if (cAdmPageObjGrid.EditIndex > -1 && cAdmPageObjGrid.Items.Count > cAdmPageObjGrid.EditIndex && ((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Fi")).HasFile && ((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Fi")).PostedFile.FileName != string.Empty)
			{
				string pth = "~/home/AdmPageObj_" + LcSystemId.ToString() + "/";
				if (!Directory.Exists(Server.MapPath(pth))) { Directory.CreateDirectory(Server.MapPath(pth)); }
				string fname = pth + Regex.Replace((new FileInfo(((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Fi")).PostedFile.FileName)).Name, "[ #=+]", string.Empty);
				fname = fname.Replace(":","").Replace("..","");
				if (fname != string.Empty)
				{
					if (File.Exists(Server.MapPath(fname))) { (new FileInfo(Server.MapPath(fname))).Delete(); }
					((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Fi")).PostedFile.SaveAs(Server.MapPath(fname));
					((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Pan")).Visible = false; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Visible = true;
					((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Text = fname; ShowDirty(true); ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Focus();
				}
			}
		}

		public void cPageLnkImg1278Tgo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Visible)
			{
				((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Pan")).Visible = true; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Visible = false;
			}
			else
			{
				((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278Pan")).Visible = false; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkImg1278")).Visible = true;
			}
		}

		public void cPageLnkAlt1278Upl_Click(object sender, System.EventArgs e)
		{
			if (cAdmPageObjGrid.EditIndex > -1 && cAdmPageObjGrid.Items.Count > cAdmPageObjGrid.EditIndex && ((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Fi")).HasFile && ((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Fi")).PostedFile.FileName != string.Empty)
			{
				string pth = "~/home/AdmPageObj_" + LcSystemId.ToString() + "/";
				if (!Directory.Exists(Server.MapPath(pth))) { Directory.CreateDirectory(Server.MapPath(pth)); }
				string fname = pth + Regex.Replace((new FileInfo(((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Fi")).PostedFile.FileName)).Name, "[ #=+]", string.Empty);
				fname = fname.Replace(":","").Replace("..","");
				if (fname != string.Empty)
				{
					if (File.Exists(Server.MapPath(fname))) { (new FileInfo(Server.MapPath(fname))).Delete(); }
					((FileUpload)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Fi")).PostedFile.SaveAs(Server.MapPath(fname));
					((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Pan")).Visible = false; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Visible = true;
					((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Text = fname; ShowDirty(true); ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Focus();
				}
			}
		}

		public void cPageLnkAlt1278Tgo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Visible)
			{
				((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Pan")).Visible = true; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Visible = false;
			}
			else
			{
				((Panel)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278Pan")).Visible = false; ((TextBox)cAdmPageObjGrid.EditItem.FindControl("cPageLnkAlt1278")).Visible = true;
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
				dt = (new AdminSystem()).GetLastPageInfo(1001, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 1001, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (cAdmPageObjGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
			PopAdmPageObj1001List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("1001", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1001", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("1001", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("1001", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("1001", LcSysConnString, LcAppPw);
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
		            context["scr"] = "1001";
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
		            context["scr"] = "1001";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmPageObj1001" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmPageObj1001" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("1001", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("1001", "AdmPageObj1001", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			Session[KEY_dsScrCriVal] = ds;
			return ds;
		}

		private void SetSectionCd1277(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtSectionCd1277];
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
						dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlSectionCd3S3118",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("PageObjId"))
					{
						ss = "(PageObjId is null";
						if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR PageObjId = " + cAdmPageObj1001List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "SectionCd1277 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlSectionCd3S3118",true,bAll,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlSectionCd3S3118",true,true,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtSectionCd1277] = dv.Table;
				}
			}
		}

		protected void cbGroupRowId1277(object sender, System.EventArgs e)
		{
			SetGroupRowId1277((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetGroupRowId1277(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlGroupRowId3S3119";
			context["addnew"] = "Y";
			context["mKey"] = "GroupRowId1277";
			context["mVal"] = "GroupRowId1277Text";
			context["mTip"] = "GroupRowId1277Text";
			context["mImg"] = "GroupRowId1277Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1001";
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
					dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlGroupRowId3S3119",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("PageObjId"))
					{
						context["pMKeyColID"] = cAdmPageObj1001List.ClientID;
						context["pMKeyCol"] = "PageObjId";
						string ss = "(PageObjId is null";
						if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR PageObjId = " + cAdmPageObj1001List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtGroupRowId1277] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbGroupColId1277(object sender, System.EventArgs e)
		{
			SetGroupColId1277((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetGroupColId1277(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlGroupColId3S3120";
			context["addnew"] = "Y";
			context["mKey"] = "GroupColId1277";
			context["mVal"] = "GroupColId1277Text";
			context["mTip"] = "GroupColId1277Text";
			context["mImg"] = "GroupColId1277Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1001";
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
					dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlGroupColId3S3120",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("PageObjId"))
					{
						context["pMKeyColID"] = cAdmPageObj1001List.ClientID;
						context["pMKeyCol"] = "PageObjId";
						string ss = "(PageObjId is null";
						if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR PageObjId = " + cAdmPageObj1001List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtGroupColId1277] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbLinkTypeCd1277(object sender, System.EventArgs e)
		{
			SetLinkTypeCd1277((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetLinkTypeCd1277(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlLinkTypeCd3S3123";
			context["addnew"] = "Y";
			context["mKey"] = "LinkTypeCd1277";
			context["mVal"] = "LinkTypeCd1277Text";
			context["mTip"] = "LinkTypeCd1277Text";
			context["mImg"] = "LinkTypeCd1277Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1001";
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
					dv = new DataView((new AdminSystem()).GetDdl(1001,"GetDdlLinkTypeCd3S3123",true,false,0,keyId,LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("PageObjId"))
					{
						context["pMKeyColID"] = cAdmPageObj1001List.ClientID;
						context["pMKeyCol"] = "PageObjId";
						string ss = "(PageObjId is null";
						if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR PageObjId = " + cAdmPageObj1001List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtLinkTypeCd1277] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private DataView GetAdmPageObj1001List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObj1001List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(1001,"GetLisAdmPageObj1001",true,"Y",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(1001,"GetLisAdmPageObj1001",true,"N",0,null,null,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmPageObj1001List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmPageObj1001List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmPageObj1001";
			context["mKey"] = "PageObjId1277";
			context["mVal"] = "PageObjId1277Text";
			context["mTip"] = "PageObjId1277Text";
			context["mImg"] = "PageObjId1277Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "1001";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "Y";
			context["conn"] = string.Empty;
			cAdmPageObj1001List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmPageObj1001List.DataContext = context;
			if (dv.Table == null) return;
			cAdmPageObj1001List.DataSource = dv;
			cAdmPageObj1001List.Visible = true;
			if (cAdmPageObj1001List.Items.Count <= 0) {cAdmPageObj1001List.Visible = false; cAdmPageObj1001List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmPageObj1001List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmPageObj1001List.Items[0].Selected = true; cAdmPageObj1001List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmPageObj1001List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmPageObj1001List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cPageObjId1277, dtAuth.Rows[0], cPageObjId1277P1, cPageObjId1277Label, cPageObjId1277P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cSectionCd1277, dtAuth.Rows[1], cSectionCd1277P1, cSectionCd1277Label, cSectionCd1277P2, null, dtLabel.Rows[1], cRFVSectionCd1277, null, null);
				base.SetFoldBehavior(cGroupRowId1277, dtAuth.Rows[2], cGroupRowId1277P1, cGroupRowId1277Label, cGroupRowId1277P2, null, dtLabel.Rows[2], null, null, null);
				SetGroupRowId1277(cGroupRowId1277,string.Empty);
				base.SetFoldBehavior(cGroupColId1277, dtAuth.Rows[3], cGroupColId1277P1, cGroupColId1277Label, cGroupColId1277P2, null, dtLabel.Rows[3], null, null, null);
				SetGroupColId1277(cGroupColId1277,string.Empty);
				base.SetFoldBehavior(cLinkTypeCd1277, dtAuth.Rows[4], cLinkTypeCd1277P1, cLinkTypeCd1277Label, cLinkTypeCd1277P2, null, dtLabel.Rows[4], cRFVLinkTypeCd1277, null, null);
				SetLinkTypeCd1277(cLinkTypeCd1277,string.Empty);
				base.SetFoldBehavior(cPageObjOrd1277, dtAuth.Rows[5], cPageObjOrd1277P1, cPageObjOrd1277Label, cPageObjOrd1277P2, null, dtLabel.Rows[5], cRFVPageObjOrd1277, null, null);
				base.SetFoldBehavior(cSctGrpRow1277, dtAuth.Rows[6], cSctGrpRow1277P1, cSctGrpRow1277Label, cSctGrpRow1277P2, null, dtLabel.Rows[6], null, null, null);
				base.SetFoldBehavior(cSctGrpCol1277, dtAuth.Rows[7], cSctGrpCol1277P1, cSctGrpCol1277Label, cSctGrpCol1277P2, null, dtLabel.Rows[7], null, null, null);
				base.SetFoldBehavior(cPageObjCss1277, dtAuth.Rows[8], cPageObjCss1277P1, cPageObjCss1277Label, cPageObjCss1277P2, cPageObjCss1277E, null, dtLabel.Rows[8], null, null, null);
				cPageObjCss1277E.Attributes["label_id"] = cPageObjCss1277Label.ClientID; cPageObjCss1277E.Attributes["target_id"] = cPageObjCss1277.ClientID;
				base.SetFoldBehavior(cPageObjSrp1277, dtAuth.Rows[9], cPageObjSrp1277P1, cPageObjSrp1277Label, cPageObjSrp1277P2, cPageObjSrp1277E, null, dtLabel.Rows[9], null, null, null);
				cPageObjSrp1277E.Attributes["label_id"] = cPageObjSrp1277Label.ClientID; cPageObjSrp1277E.Attributes["target_id"] = cPageObjSrp1277.ClientID;
				base.SetFoldBehavior(cBtnDefault, dtAuth.Rows[10], null, null, null, dtLabel.Rows[10], null, null, null);
				base.SetFoldBehavior(cBtnHeader, dtAuth.Rows[11], null, null, null, dtLabel.Rows[11], null, null, null);
				base.SetFoldBehavior(cBtnFooter, dtAuth.Rows[12], null, null, null, dtLabel.Rows[12], null, null, null);
				base.SetFoldBehavior(cBtnSidebar, dtAuth.Rows[13], null, null, null, dtLabel.Rows[13], null, null, null);
			}
			if ((cSectionCd1277.Attributes["OnChange"] == null || cSectionCd1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSectionCd1277.Visible && cSectionCd1277.Enabled) {cSectionCd1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGroupRowId1277.Attributes["OnChange"] == null || cGroupRowId1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGroupRowId1277.Visible && cGroupRowId1277.Enabled) {cGroupRowId1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGroupColId1277.Attributes["OnChange"] == null || cGroupColId1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGroupColId1277.Visible && cGroupColId1277.Enabled) {cGroupColId1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cLinkTypeCd1277.Attributes["OnChange"] == null || cLinkTypeCd1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLinkTypeCd1277.Visible && cLinkTypeCd1277.Enabled) {cLinkTypeCd1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cPageObjOrd1277.Attributes["OnChange"] == null || cPageObjOrd1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPageObjOrd1277.Visible && !cPageObjOrd1277.ReadOnly) {cPageObjOrd1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSctGrpRow1277.Attributes["OnChange"] == null || cSctGrpRow1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSctGrpRow1277.Visible && cSctGrpRow1277.Enabled) {cSctGrpRow1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSctGrpCol1277.Attributes["OnChange"] == null || cSctGrpCol1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSctGrpCol1277.Visible && cSctGrpCol1277.Enabled) {cSctGrpCol1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPageObjCss1277.Attributes["OnChange"] == null || cPageObjCss1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPageObjCss1277.Visible && !cPageObjCss1277.ReadOnly) {cPageObjCss1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cPageObjSrp1277.Attributes["OnChange"] == null || cPageObjSrp1277.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cPageObjSrp1277.Visible && !cPageObjSrp1277.ReadOnly) {cPageObjSrp1277.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (cAdmPageObjGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				cNewButton_Click(sender, new EventArgs());
			}
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (cAdmPageObjGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
				Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
				Session.Remove(KEY_dtSectionCd1277);
				Session.Remove(KEY_dtGroupRowId1277);
				Session.Remove(KEY_dtGroupColId1277);
				Session.Remove(KEY_dtLinkTypeCd1277);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
			}
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {SetSectionCd1277(cSectionCd1277,string.Empty);}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {cGroupRowId1277.ClearSearch();}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {cGroupColId1277.ClearSearch();}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {cLinkTypeCd1277.ClearSearch();}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {cPageObjOrd1277.Text = "10";}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {cSctGrpRow1277.ImageUrl = "~/images/Link.gif"; }
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cSctGrpCol1277.ImageUrl = "~/images/Link.gif"; }
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cPageObjCss1277.Text = string.Empty;}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {cPageObjSrp1277.Text = string.Empty;}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cPageObjId1277.Text = string.Empty;
			SetSectionCd1277(cSectionCd1277,string.Empty);
			cGroupRowId1277.ClearSearch();
			cGroupColId1277.ClearSearch();
			cLinkTypeCd1277.ClearSearch();
			cPageObjOrd1277.Text = "10";
			cSctGrpRow1277.Visible = false;
			cSctGrpCol1277.Visible = false;
			cPageObjCss1277.Text = string.Empty;
			cPageObjSrp1277.Text = string.Empty;
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmPageObj1001List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmPageObj1001List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmPageObj1001List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmPageObj1001ById",cAdmPageObj1001List.SelectedValue,null,null);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmPageObj1001List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cPageObjId1277.Text = RO.Common3.Utils.fmNumeric("0",dr["PageObjId1277"].ToString(),base.LUser.Culture);} catch {cPageObjId1277.Text = string.Empty;}
					SetSectionCd1277(cSectionCd1277,dr["SectionCd1277"].ToString());
					SetGroupRowId1277(cGroupRowId1277,dr["GroupRowId1277"].ToString());
					SetGroupColId1277(cGroupColId1277,dr["GroupColId1277"].ToString());
					SetLinkTypeCd1277(cLinkTypeCd1277,dr["LinkTypeCd1277"].ToString()); cLinkTypeCd1277_SelectedIndexChanged(cLinkTypeCd1277, new EventArgs());
					try {cPageObjOrd1277.Text = RO.Common3.Utils.fmNumeric("0",dr["PageObjOrd1277"].ToString(),base.LUser.Culture);} catch {cPageObjOrd1277.Text = string.Empty;}
					try {cSctGrpRow1277.Text = dr["SctGrpRow1277"].ToString();} catch {cSctGrpRow1277.Text = string.Empty;}
					if (string.IsNullOrEmpty(dr["SctGrpRow1277URL"].ToString())) {cSctGrpRow1277.Visible = false;}
					else
					{
						cSctGrpRow1277.Visible = true;
						if (dr["SctGrpRow1277URL"].ToString() == string.Empty) { cSctGrpRow1277.Attributes.Remove("onclick"); }
						else { cSctGrpRow1277.Style.Value = "cursor:pointer;"; cSctGrpRow1277.Attributes.Add("OnClick","SearchLink('" + dr["SctGrpRow1277URL"].ToString() + "','','',''); return stopEvent(this,event);"); }
					    cSctGrpRow1277.ImageUrl = "~/images/Link.gif";
					}
					try {cSctGrpCol1277.Text = dr["SctGrpCol1277"].ToString();} catch {cSctGrpCol1277.Text = string.Empty;}
					if (string.IsNullOrEmpty(dr["SctGrpCol1277URL"].ToString())) {cSctGrpCol1277.Visible = false;}
					else
					{
						cSctGrpCol1277.Visible = true;
						if (dr["SctGrpCol1277URL"].ToString() == string.Empty) { cSctGrpCol1277.Attributes.Remove("onclick"); }
						else { cSctGrpCol1277.Style.Value = "cursor:pointer;"; cSctGrpCol1277.Attributes.Add("OnClick","SearchLink('" + dr["SctGrpCol1277URL"].ToString() + "','','',''); return stopEvent(this,event);"); }
					    cSctGrpCol1277.ImageUrl = "~/images/Link.gif";
					}
					try {cPageObjCss1277.Text = dr["PageObjCss1277"].ToString();} catch {cPageObjCss1277.Text = string.Empty;}
					try {cPageObjSrp1277.Text = dr["PageObjSrp1277"].ToString();} catch {cPageObjSrp1277.Text = string.Empty;}
				}
			}
			cButPanel.DataBind(); if (!cSaveButton.Visible) { cInsRowButton.Visible = false; }
			DataTable dtAdmPageObjGrid = null;
			int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
			try
			{
				dtAdmPageObjGrid = (new AdminSystem()).GetDtlById(1001,"GetAdmPageObj1001DtlById",cAdmPageObj1001List.SelectedValue,null,null,filterId,base.LImpr,base.LCurr);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (!dtAdmPageObjGrid.Columns.Contains("_NewRow")) { dtAdmPageObjGrid.Columns.Add("_NewRow"); }
			Session[KEY_dtAdmPageObjGrid] = dtAdmPageObjGrid;
			cAdmPageObjGrid.EditIndex = -1;
			cAdmPageObjGridDataPager.PageSize = Int16.Parse(cPgSize.Text); GotoPage(0);
			if (Session[KEY_lastSortCol] != null)
			{
				cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs((string)Session[KEY_lastSortExp], SortDirection.Ascending));
			}
			else if (dtAdmPageObjGrid.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dtAdmPageObjGrid.Rows.Count == 1))
			{
				cAdmPageObjGrid_DataBind(dtAdmPageObjGrid.DefaultView);
			}
			cFindButton_Click(sender, e);
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false; Session.Remove(KEY_scrImport);
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmPageObj1001List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cLinkTypeCd1277_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cLinkTypeCd1277_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cLinkTypeCd1277_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//WebRule: Paste default script
            if (cLinkTypeCd1277.SelectedValue == "CRS")
            {
                cPageObjSrp1277.Text = "    $(window).load(function () { $('.flexslider').flexslider({ animation:'fade', pauseOnHover:true, slideshowSpeed:7000 }); });";
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cLinkTypeCd1277.Items.Count > 0 && cLinkTypeCd1277.DataSource != null)
			{
				DataView dv = (DataView) cLinkTypeCd1277.DataSource; dv.RowFilter = string.Empty;
				DataRowView dr = cLinkTypeCd1277.DataSetIndex >= 0 && cLinkTypeCd1277.DataSetIndex < dv.Count ? dv[cLinkTypeCd1277.DataSetIndex] : dv[0];
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cBtnDefault_Click(object sender, System.EventArgs e)
		{
			//WebRule: Generate default content
            //if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Default", 0, 0, 0, string.Empty, string.Empty))
            if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV")
            {
                (new GenSectionSystem()).CreateProgram("D", base.CPrj, base.CSrc);
                bInfoNow.Value = "Y"; PreMsgPopup("DefaultModule and its CSS generated.");
            }
            else
            {
                bErrNow.Value = "Y"; PreMsgPopup("DefaultModule not generated.");
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cBtnHeader_Click(object sender, System.EventArgs e)
		{
			//WebRule: Generate header content
            //if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Header", 0, 0, 0, string.Empty, string.Empty))
            if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV")
            {
                (new GenSectionSystem()).CreateProgram("H", base.CPrj, base.CSrc);
                bInfoNow.Value = "Y"; PreMsgPopup("HeaderModule and its CSS generated.");
            }
            else
            {
                bErrNow.Value = "Y"; PreMsgPopup("HeaderModule not generated.");
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cBtnFooter_Click(object sender, System.EventArgs e)
		{
			//WebRule: Generate footer content
            //if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Footer", 0, 0, 0, string.Empty, string.Empty))
            if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV")
            {
                (new GenSectionSystem()).CreateProgram("F", base.CPrj, base.CSrc);
                bInfoNow.Value = "Y"; PreMsgPopup("FooterModule and its CSS generated.");
            }
            else
            {
                bErrNow.Value = "Y"; PreMsgPopup("FooterModule not generated.");
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cBtnSidebar_Click(object sender, System.EventArgs e)
		{
			//WebRule: Generate sidebar content
            //if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Sidebar", 0, 0, 0, string.Empty, string.Empty))
            if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV")
            {
                (new GenSectionSystem()).CreateProgram("S", base.CPrj, base.CSrc);
                bInfoNow.Value = "Y"; PreMsgPopup("SidebarModule and its CSS generated.");
            }
            else
            {
                bErrNow.Value = "Y"; PreMsgPopup("SidebarModule not generated.");
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
		}

		protected void cPageLnkImg1278_TextChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true);  // Do not remove; Need to reenable after postback, especially in the grid.
			if (cAdmPageObjGrid.EditIndex > -1 && cAdmPageObjGrid.Items.Count > cAdmPageObjGrid.EditIndex)
			{
			}
		}

		protected void cPageLnkAlt1278_TextChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true);  // Do not remove; Need to reenable after postback, especially in the grid.
			if (cAdmPageObjGrid.EditIndex > -1 && cAdmPageObjGrid.Items.Count > cAdmPageObjGrid.EditIndex)
			{
			}
		}

		public void cFindButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				WebControl cc = sender as WebControl;
				if ((cc != null && cc.ID.Equals("cAdmPageObj1001List")) || cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(cAdmPageObjGrid, new CommandEventArgs("Save", "")))
				{
					string rf = string.Empty;
					if (cFind.Text != string.Empty) { rf = "(" + base.GetExpression(cFind.Text.Trim(), GetAuthCol(), 14, cFindFilter.SelectedValue) + ")"; }
					if (rf != string.Empty) { rf = "((" + rf + " or _NewRow = 'Y' ))"; }
					dv.RowFilter = rf;
					ViewState["_RowFilter"] = rf;
					GotoPage(0); cAdmPageObjGrid_DataBind(dv);
					if (GetCurrPageIndex() != (int)Session[KEY_currPageIndex] && (int)Session[KEY_currPageIndex] < GetTotalPages()) { GotoPage((int)Session[KEY_currPageIndex]); cAdmPageObjGrid_DataBind(dv);}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (dt != null && (ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")))
			{
				DataRow dr = dt.NewRow();
				int? sortorder = null;
				dr["PageLnkOrd1278"] = 10;
				dr["Popup1278"] = "Y";
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
				Session[KEY_lastAddedRow] = 0; Session[KEY_dtAdmPageObjGrid] = dt; Session[KEY_currPageIndex] = 0; GotoPage(0);
				cAdmPageObjGrid_DataBind(dt.DefaultView);
				cAdmPageObjGrid_OnItemEditing(cAdmPageObjGrid, new ListViewEditEventArgs(0));
				// *** Default Value (Grid) Web Rule starts here *** //
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPgSizeButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (cAdmPageObjGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				try {if (int.Parse(cPgSize.Text) < 1 || int.Parse(cPgSize.Text) > 200) {cPgSize.Text = "10";}} catch {cPgSize.Text = "10";}
				cAdmPageObjGridDataPager.PageSize = int.Parse(cPgSize.Text); GotoPage(0);
				if (dt.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dt.Rows.Count == 1)) {cAdmPageObjGrid_DataBind(dt.DefaultView);} else {cFindButton_Click(sender, e);}
				try
				{
					(new AdminSystem()).UpdLastPageInfo(1001, base.LUser.UsrId, cPgSize.Text, LcSysConnString, LcAppPw);
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
					Session["ImportSchema"] = (new AdminSystem()).GetSchemaScrImp(1001,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "SearchLink('ImportSchema.aspx?scm=S&key=1001&csy=3','','',''); return false;";}
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void ScrImportDdl(DataView dvd, DataRowView drv, bool bComboBox, string PKey, string CKey, string CNam, int MaxLen, string MatchCd, bool bAllowNulls)
		{
			if (dvd != null)
			{
				if (dvd.Table.Columns.Contains(PKey))
				{
					if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue))
					{
						dvd.RowFilter = "(" + PKey + " is null)";
					}
					else
					{
						dvd.RowFilter = "(" + PKey + " is null OR " + PKey + " = " + cAdmPageObj1001List.SelectedValue + ")";
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
			DataTable dtAdmPageObjGrid = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (dti != null && dtAdmPageObjGrid != null)
			{
				if (bClear)
				{
					dtAdmPageObjGrid.Rows.Clear();
				}
				cFind.Text = string.Empty;
				int iExist = dtAdmPageObjGrid.Rows.Count;
				iRow = dti.DefaultView.Count - iExist;
				if (!dti.Columns.Contains("_NewRow")) { dti.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmPageObjGrid] = dti;
				cAdmPageObjGrid_DataBind(dti.DefaultView);
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
				DataTable dtAdmPageObjGrid = (DataTable)Session[KEY_dtAdmPageObjGrid];
				try
				{
					if (cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(cAdmPageObjGrid, new CommandEventArgs("Save", "")))
					{
						Session[KEY_scrImport] = ImportFile(dtAdmPageObjGrid.Copy(),cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
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
					if (rows[iRow][5].ToString() == string.Empty) {idel = idel + 1;}
					else
					{
						dt.Rows.Add(dt.NewRow());
						if (rows[iRow][5].ToString() == string.Empty) { rows[iRow][5] = 10;}
						if (rows[iRow][6].ToString() == string.Empty) { rows[iRow][6] = "Y";}
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
				if (cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(cAdmPageObjGrid, new CommandEventArgs("Save", "")))
				{
					try { cAdmPageObjGridDataPager.SetPageProperties(cAdmPageObjGridDataPager.PageSize * pageNo, cAdmPageObjGridDataPager.MaximumRows, false); cAdmPageObjGrid_DataBind(null); }
					catch
					{
						try { cAdmPageObjGridDataPager.SetPageProperties(0, cAdmPageObjGridDataPager.MaximumRows, false); cAdmPageObjGrid_DataBind(null); } catch {}
					}
				}
			}
		}

		protected void cAdmPageObjGrid_OnItemCommand(object sender, ListViewCommandEventArgs e)
		{
		}

		private void cAdmPageObjGrid_DataBind(DataView dvAdmPageObjGrid)
		{
			if (dvAdmPageObjGrid == null)
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
				dvAdmPageObjGrid = dt != null ? dt.DefaultView : null;
			}
			if (dvAdmPageObjGrid != null)
			{
				LcAuth = GetAuthCol().AsEnumerable().ToDictionary<DataRow,string>(dr=>dr["ColName"].ToString());
				cAdmPageObjGrid.DataSource = dvAdmPageObjGrid;
				cAdmPageObjGrid.DataBind();
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
					if (ii >= 14 && !string.IsNullOrEmpty(dr["ColumnHeader"].ToString()) && !string.IsNullOrEmpty(dr["TableId"].ToString()) && dtAuth.Rows[ii]["ColVisible"].ToString() == "Y")
					{
						li = new ListItem();
						li.Value = ii.ToString(); li.Text = dr["ColumnHeader"].ToString().Replace("*", string.Empty);
						cFindFilter.Items.Add(li);
					}
					ii = ii + 1;
				}
			}
		}

		protected void cAdmPageObjGrid_OnSorting(object sender, ListViewSortEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(cAdmPageObjGrid, new CommandEventArgs("Save", "")))
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
					Session[KEY_dtAdmPageObjGrid] = dt;
					cAdmPageObjGrid_DataBind(dv);
				}
			}
		}

		protected void cAdmPageObjGrid_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
		    cAdmPageObjGridDataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
		    DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
		    DataView dv = dt != null ? dt.DefaultView : null;
			cAdmPageObjGrid_DataBind(dv); cAdmPageObjGrid.EditIndex = -1;
		}

		private void GridChkPgDirty(ListViewItem lvi)
		{
				WebControl cc = null; System.Web.UI.WebControls.Image ib = null;
				cc = ((WebControl)lvi.FindControl("cPageLnkTxt1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cPageLnkRef1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cPageLnkImg1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); document.getElementById('" + bConfirm.ClientID + "').value='N';";}
				cc = ((WebControl)lvi.FindControl("cPageLnkImg1278Tgo"));
				if (cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cc.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
				cc = ((WebControl)lvi.FindControl("cPageLnkAlt1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); document.getElementById('" + bConfirm.ClientID + "').value='N';";}
				cc = ((WebControl)lvi.FindControl("cPageLnkAlt1278Tgo"));
				if (cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cc.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
				cc = ((WebControl)lvi.FindControl("cPageLnkOrd1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cPopup1278"));
				if ((cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
				cc = ((WebControl)lvi.FindControl("cPageLnkCss1278"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				ib = ((System.Web.UI.WebControls.Image)lvi.FindControl("cPageLnkCss1278E"));
				ib.Attributes["target_id"] = cc.ClientID;
		}

		protected void cAdmPageObjGrid_OnItemDataBound(object sender, ListViewItemEventArgs e)
		{
			// *** GridItemDataBound (before) Web Rule End *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			bool isEditItem = false;
			bool isImage = true;
			bool hasImageContent = false;
			DataView dvAdmPageObjGrid = dt != null ? dt.DefaultView : null;
			if (cAdmPageObjGrid.EditIndex > -1 && GetDataItemIndex(cAdmPageObjGrid.EditIndex) == e.Item.DataItemIndex)
			{
				isEditItem = true;
				base.SetGridEnabled(e.Item, GetAuthCol(), GetLabel(), 14);
				CheckBox cb = null;
				cb = (CheckBox)e.Item.FindControl("cPopup1278");
				if (cb != null)
				{
					cb.Checked = base.GetBool(dvAdmPageObjGrid[e.Item.DataItemIndex]["Popup1278"].ToString());
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
					cc = e.Item.FindControl("cAdmPageObjGridDelete"); if (cc != null) { cc.Visible = false; }
					cc = e.Item.FindControl("cAdmPageObjGridEdit"); if (cc != null) { cc.Visible = false; }
				}
				else
				{
			        HtmlTableRow tr = e.Item.FindControl("cAdmPageObjGridRow") as HtmlTableRow;
			        LinkButton lb = e.Item.FindControl("cAdmPageObjGridEdit") as LinkButton;
			        if (tr != null && lb != null) { SetDefaultCtrl(tr, lb, string.Empty); }
				}
			}
			if (cAdmPageObjGrid.EditIndex > -1 && GetDataItemIndex(cAdmPageObjGrid.EditIndex) == e.Item.DataItemIndex)
			{
			}
			else
			{
			}
			// *** GridItemDataBound (after) Web Rule End *** //
		}

		protected void cPageLnkId1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "0") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "0"; Session[KEY_lastSortExp] = "PageLnkId1278";Session[KEY_lastSortImg] = "cPageLnkId1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkId1278", SortDirection.Ascending));
		}

		protected void cPageLnkTxt1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "1") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "1"; Session[KEY_lastSortExp] = "PageLnkTxt1278";Session[KEY_lastSortImg] = "cPageLnkTxt1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkTxt1278", SortDirection.Ascending));
		}

		protected void cPageLnkRef1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "2") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "2"; Session[KEY_lastSortExp] = "PageLnkRef1278";Session[KEY_lastSortImg] = "cPageLnkRef1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkRef1278", SortDirection.Ascending));
		}

		protected void cPageLnkImg1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "3") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "3"; Session[KEY_lastSortExp] = "PageLnkImg1278";Session[KEY_lastSortImg] = "cPageLnkImg1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkImg1278", SortDirection.Ascending));
		}

		protected void cPageLnkAlt1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "4") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "4"; Session[KEY_lastSortExp] = "PageLnkAlt1278";Session[KEY_lastSortImg] = "cPageLnkAlt1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkAlt1278", SortDirection.Ascending));
		}

		protected void cPageLnkOrd1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "5") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "5"; Session[KEY_lastSortExp] = "PageLnkOrd1278";Session[KEY_lastSortImg] = "cPageLnkOrd1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkOrd1278", SortDirection.Ascending));
		}

		protected void cPopup1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "6") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "6"; Session[KEY_lastSortExp] = "Popup1278";Session[KEY_lastSortImg] = "cPopup1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("Popup1278", SortDirection.Ascending));
		}

		protected void cPageLnkCss1278hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "7") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "7"; Session[KEY_lastSortExp] = "PageLnkCss1278";Session[KEY_lastSortImg] = "cPageLnkCss1278hi";
			cAdmPageObjGrid_OnSorting(sender, new ListViewSortEventArgs("PageLnkCss1278", SortDirection.Ascending));
		}

		protected void cAdmPageObjGrid_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if ((ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")) && dt != null && dt.DefaultView.Count > GetDataItemIndex(e.NewEditIndex))
			{
				cAdmPageObjGrid.EditIndex = e.NewEditIndex;
				cAdmPageObjGrid_DataBind(null);
				Control wc = null;
				string idx = Page.Request["__EVENTARGUMENT"];
				if (!string.IsNullOrEmpty(idx))
				{
				    Control ctrlToFocus = cAdmPageObjGrid.EditItem.FindControl("c" + idx);
				    if (((WebControl)ctrlToFocus).Enabled) { wc = ctrlToFocus; }
				}
				if (wc == null) { wc = FindEditableControl(cAdmPageObjGrid.EditItem); }
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
             MkMobileLabel(cAdmPageObjGrid.EditItem);
		    }
		    else { e.Cancel = true; }
		}

		private void MkMobileLabel(ListViewItem lvi)
		{
		    Label ml = null;
		    ml = lvi.FindControl("cPageLnkId1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(14); }
		    ml = lvi.FindControl("cPageLnkTxt1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(15); }
		    ml = lvi.FindControl("cPageLnkRef1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(16); }
		    ml = lvi.FindControl("cPageLnkImg1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(17); }
		    ml = lvi.FindControl("cPageLnkAlt1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(18); }
		    ml = lvi.FindControl("cPageLnkOrd1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(19); }
		    ml = lvi.FindControl("cPopup1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(20); }
		    ml = lvi.FindControl("cPageLnkCss1278ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(21); }
		}

		protected void GridFill(ListViewItem lvi, DataTable dt, DataRow dr, bool bInsert)
		{
			TextBox tb = null;
			tb = (TextBox)lvi.FindControl("cPageLnkTxt1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkTxt1278"] = tb.Text;} else {dr["PageLnkTxt1278"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cPageLnkRef1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkRef1278"] = tb.Text;} else {dr["PageLnkRef1278"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cPageLnkImg1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkImg1278"] = tb.Text;} else {dr["PageLnkImg1278"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cPageLnkAlt1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkAlt1278"] = tb.Text;} else {dr["PageLnkAlt1278"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cPageLnkOrd1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkOrd1278"] = tb.Text;} else {dr["PageLnkOrd1278"] = System.DBNull.Value;}
			}
			CheckBox cb = null;
			cb = (CheckBox)lvi.FindControl("cPopup1278");
			if (cb != null)
			{
				dr["Popup1278"] = base.SetBool(cb.Checked);
			}
			tb = (TextBox)lvi.FindControl("cPageLnkCss1278");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["PageLnkCss1278"] = tb.Text;} else {dr["PageLnkCss1278"] = System.DBNull.Value;}
			}
		    if (bInsert) { dt.Rows.InsertAt(dr, 0); }
			Session[KEY_dtAdmPageObjGrid] = dt;
			cAdmPageObjGrid.EditIndex = -1;
			cAdmPageObjGrid_DataBind(dt.DefaultView);
		}

		protected void cAdmPageObjGrid_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			if (dt != null && dt.Rows.Count > 0)
			{
				DataRow dr = dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row;
			    GridFill(cAdmPageObjGrid.EditItem, dt, dr, false);
			    dr["_NewRow"] = "N";
			    if (dt.Columns.Contains("_SortOrder")) dr["_SortOrder"] = null;
			}
		}

		protected void cAdmPageObjGrid_OnItemCanceling(object sender, ListViewCancelEventArgs e)
		{
			PanelTop.Update();
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && dv[GetDataItemIndex(e.ItemIndex)]["_NewRow"].ToString() == "Y")
			{
				cAdmPageObjGrid_OnItemDeleting(sender, new ListViewDeleteEventArgs(e.ItemIndex));
			}
			if (dt.Columns.Contains("_SortOrder")) dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row["_SortOrder"] = null;
			cAdmPageObjGrid.EditIndex = -1; cAdmPageObjGrid_DataBind(null);
		}

		protected void cDeleteAllButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && (cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				GotoPage(0);
				while (dv.Count > 0) {dv.Delete(0);}
				Session[KEY_dtAdmPageObjGrid] = dt;
				cAdmPageObjGrid.EditIndex = -1; cAdmPageObjGrid_DataBind(dv);
			}
		}

		protected void cAdmPageObjGrid_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			DataRow dr = dv != null ? dv[GetDataItemIndex(e.ItemIndex)].Row : null;
			if (dv != null && (cAdmPageObjGrid.EditIndex == e.ItemIndex || UpdateGridRow(sender, new CommandEventArgs("Save",""))))
			{
				// *** Delete Grid Row (before) Web Rule End *** //
				dr.Delete();
				Session[KEY_dtAdmPageObjGrid] = dt;
				cAdmPageObjGrid.EditIndex = -1; cAdmPageObjGrid_DataBind(dv);
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
			cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
			PopAdmPageObj1001List(sender, e, false, null);
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
			cPageObjId1277.Text = string.Empty;
			cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
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
			if (cPageObjId1277.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmPageObj1001List(sender, e, false, cPageObjId1277.Text); }
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
			Session.Remove(KEY_dtAdmPageObj1001List); PopAdmPageObj1001List(sender, e, false, null);
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
			if (ValidPage() && (cAdmPageObjGrid.EditIndex < 0 || UpdateGridRow(cAdmPageObjGrid, new CommandEventArgs("Save", ""))))
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmPageObjGrid];
				DataView dv = dt != null ? dt.DefaultView : null;
				string ftr = string.Empty;
				if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
				AdmPageObj1001 ds = PrepAdmPageObjData(dv,cPageObjId1277.Text == string.Empty);
				if (ftr != string.Empty) {dv.RowFilter = ftr;}
				if (string.IsNullOrEmpty(cAdmPageObj1001List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(1001,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmPageObj1001List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(1001,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
						Session[KEY_currPageIndex] = GetCurrPageIndex();
						ShowDirty(false); PopAdmPageObj1001List(sender, e, false, ds.Tables["AdmPageObj"].Rows[0]["PageObjId1277"]);
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
			if (cPageObjId1277.Text != string.Empty)
			{
				AdmPageObj1001 ds = PrepAdmPageObjData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(1001,false,base.LUser,base.LImpr,base.LCurr,ds,null,null,base.CPrj,base.CSrc))
					{
						cAdmPageObj1001List.ClearSearch(); Session.Remove(KEY_dtAdmPageObj1001List);
						ShowDirty(false); PopAdmPageObj1001List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmPageObj1001 PrepAdmPageObjData(DataView dv, bool bAdd)
		{
			AdmPageObj1001 ds = new AdmPageObj1001();
			DataRow dr = ds.Tables["AdmPageObj"].NewRow();
			DataRow drType = ds.Tables["AdmPageObj"].NewRow();
			DataRow drDisp = ds.Tables["AdmPageObj"].NewRow();
			if (bAdd) { dr["PageObjId1277"] = string.Empty; } else { dr["PageObjId1277"] = cPageObjId1277.Text; }
			drType["PageObjId1277"] = "Numeric"; drDisp["PageObjId1277"] = "TextBox";
			try {dr["SectionCd1277"] = cSectionCd1277.SelectedValue;} catch {}
			drType["SectionCd1277"] = "Char"; drDisp["SectionCd1277"] = "DropDownList";
			try {dr["GroupRowId1277"] = cGroupRowId1277.SelectedValue;} catch {}
			drType["GroupRowId1277"] = "Numeric"; drDisp["GroupRowId1277"] = "AutoComplete";
			try {dr["GroupColId1277"] = cGroupColId1277.SelectedValue;} catch {}
			drType["GroupColId1277"] = "Numeric"; drDisp["GroupColId1277"] = "AutoComplete";
			try {dr["LinkTypeCd1277"] = cLinkTypeCd1277.SelectedValue;} catch {}
			drType["LinkTypeCd1277"] = "Char"; drDisp["LinkTypeCd1277"] = "AutoComplete";
			try {dr["PageObjOrd1277"] = cPageObjOrd1277.Text.Trim();} catch {}
			drType["PageObjOrd1277"] = "Numeric"; drDisp["PageObjOrd1277"] = "TextBox";
			try {dr["SctGrpRow1277"] = cSctGrpRow1277.Text;} catch {}
			drType["SctGrpRow1277"] = "VarChar"; drDisp["SctGrpRow1277"] = "ImagePopUp";
			try {dr["SctGrpCol1277"] = cSctGrpCol1277.Text;} catch {}
			drType["SctGrpCol1277"] = "VarChar"; drDisp["SctGrpCol1277"] = "ImagePopUp";
			try {dr["PageObjCss1277"] = cPageObjCss1277.Text;} catch {}
			drType["PageObjCss1277"] = "VarChar"; drDisp["PageObjCss1277"] = "MultiLine";
			try {dr["PageObjSrp1277"] = cPageObjSrp1277.Text;} catch {}
			drType["PageObjSrp1277"] = "VarChar"; drDisp["PageObjSrp1277"] = "MultiLine";
			if (dv != null)
			{
				ds.Tables["AdmPageObjDef"].Rows.Add(MakeTypRow(ds.Tables["AdmPageObjDef"].NewRow()));
				ds.Tables["AdmPageObjDef"].Rows.Add(MakeDisRow(ds.Tables["AdmPageObjDef"].NewRow()));
				if (bAdd)
				{
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmPageObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjAdd"].NewRow(), drv, true));
					}
				}
				else
				{
					dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmPageObjUpd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjUpd"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.Added;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmPageObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjAdd"].NewRow(), drv, true));
					}
					dv.RowStateFilter = DataViewRowState.Deleted;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmPageObjDel"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjDel"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.CurrentRows;
				}
			}
			ds.Tables["AdmPageObj"].Rows.Add(dr); ds.Tables["AdmPageObj"].Rows.Add(drType); ds.Tables["AdmPageObj"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmPageObj1001List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtSectionCd1277];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'SectionCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtLinkTypeCd1277];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'LinkTypeCd', please investigate."); return false;
			}
			return true;
		}

		private DataRow MakeTypRow(DataRow dr)
		{
			dr["PageObjId1277"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["PageLnkId1278"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["PageLnkTxt1278"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["PageLnkRef1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["PageLnkImg1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["PageLnkAlt1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			dr["PageLnkOrd1278"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["Popup1278"] = System.Data.OleDb.OleDbType.Char.ToString();
			dr["PageLnkCss1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
			return dr;
		}

		private DataRow MakeDisRow(DataRow dr)
		{
			dr["PageObjId1277"] = "TextBox";
			dr["PageLnkId1278"] = "TextBox";
			dr["PageLnkTxt1278"] = "TextBox";
			dr["PageLnkRef1278"] = "TextBox";
			dr["PageLnkImg1278"] = "Upload";
			dr["PageLnkAlt1278"] = "Upload";
			dr["PageLnkOrd1278"] = "TextBox";
			dr["Popup1278"] = "CheckBox";
			dr["PageLnkCss1278"] = "MultiLine";
			return dr;
		}

		private DataRow MakeColRow(DataRow dr, DataRowView drv, bool bAdd)
		{
			dr["PageObjId1277"] = cPageObjId1277.Text;
			DataTable dtAuth = GetAuthCol();
			if (dtAuth != null)
			{
				dr["PageLnkId1278"] = drv["PageLnkId1278"].ToString().Trim();
				dr["PageLnkTxt1278"] = drv["PageLnkTxt1278"].ToString().Trim();
				if (bAdd && dtAuth.Rows[15]["ColReadOnly"].ToString() == "Y" && dr["PageLnkTxt1278"].ToString() == string.Empty) {dr["PageLnkTxt1278"] = System.DBNull.Value;}
				dr["PageLnkRef1278"] = drv["PageLnkRef1278"].ToString().Trim();
				if (bAdd && dtAuth.Rows[16]["ColReadOnly"].ToString() == "Y" && dr["PageLnkRef1278"].ToString() == string.Empty) {dr["PageLnkRef1278"] = System.DBNull.Value;}
				dr["PageLnkImg1278"] = drv["PageLnkImg1278"];
				if (bAdd && dtAuth.Rows[17]["ColReadOnly"].ToString() == "Y" && dr["PageLnkImg1278"].ToString() == string.Empty) {dr["PageLnkImg1278"] = System.DBNull.Value;}
				dr["PageLnkAlt1278"] = drv["PageLnkAlt1278"];
				if (bAdd && dtAuth.Rows[18]["ColReadOnly"].ToString() == "Y" && dr["PageLnkAlt1278"].ToString() == string.Empty) {dr["PageLnkAlt1278"] = System.DBNull.Value;}
				dr["PageLnkOrd1278"] = drv["PageLnkOrd1278"].ToString().Trim();
				dr["Popup1278"] = drv["Popup1278"];
				if (bAdd && dtAuth.Rows[20]["ColReadOnly"].ToString() == "Y" && dr["Popup1278"].ToString() == string.Empty) {dr["Popup1278"] = System.DBNull.Value;}
				dr["PageLnkCss1278"] = drv["PageLnkCss1278"];
				if (bAdd && dtAuth.Rows[21]["ColReadOnly"].ToString() == "Y" && dr["PageLnkCss1278"].ToString() == string.Empty) {dr["PageLnkCss1278"] = System.DBNull.Value;}
			}
			return dr;
		}

		private bool UpdateGridRow(object sender, CommandEventArgs e)
		{
			if (cAdmPageObjGrid.EditIndex > -1)
			{
				TextBox pwd = null;				cAdmPageObjGrid_OnItemUpdating(sender, new ListViewUpdateEventArgs(cAdmPageObjGrid.EditIndex));
			}
			return true;
		}

		protected void cAdmPageObjGrid_OnPreRender(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Image hi = null;
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkId1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkTxt1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkRef1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkImg1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkAlt1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkOrd1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPopup1278hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl("cPageLnkCss1278hi"); if (hi != null) { hi.Visible = false; }
			if (Session[KEY_lastSortImg] != null)
			{
				hi = (System.Web.UI.WebControls.Image)cAdmPageObjGrid.FindControl((string)Session[KEY_lastSortImg]);
				if (hi != null) { hi.ImageUrl = Utils.AddTilde((string)Session[KEY_lastSortUrl]); hi.Visible = true; }
			}
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkId1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkTxt1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkRef1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkImg1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkAlt1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkOrd1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPopup1278hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmPageObjGrid.FindControl("cPageLnkCss1278hl"));
		}

		private string GetButtonId(ListViewItem lvi)
		{
			string ButtonID = String.Empty;
			Control c = lvi.FindControl("cAdmPageObjGridEdit");
			if (c != null) { ButtonID = c.UniqueID; }
			return ButtonID;
		}

		private void SetDefaultCtrl(HtmlTableRow tr, LinkButton lb, string ctrlId)
		{
			tr.Attributes.Add("onclick", "document.getElementById('" + bConfirm.ClientID + "').value='N'; fFocusedEdit('" + lb.UniqueID + "','" + ctrlId + "',event);");
		}

		private int GetCurrPageIndex()
		{
		    return cAdmPageObjGridDataPager.StartRowIndex / cAdmPageObjGridDataPager.PageSize;
		}

		private int GetTotalPages()
		{
		    return (int)Math.Ceiling((double)cAdmPageObjGridDataPager.TotalRowCount / cAdmPageObjGridDataPager.PageSize);
		}

		private int GetDataItemIndex(int editIndex)
		{
		    return cAdmPageObjGridDataPager.StartRowIndex + editIndex;
		}

		protected void cAdmPageObjGrid_OnLayoutCreated(object sender, EventArgs e)
		{
		    // Header:
		    LinkButton lb = null;
		    lb = cAdmPageObjGrid.FindControl("cPageLnkId1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(14); lb.ToolTip = ColumnToolTip(14); lb.Parent.Visible = GridColumnVisible(14); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkTxt1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(15); lb.ToolTip = ColumnToolTip(15); lb.Parent.Visible = GridColumnVisible(15); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkRef1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(16); lb.ToolTip = ColumnToolTip(16); lb.Parent.Visible = GridColumnVisible(16); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkImg1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(17); lb.ToolTip = ColumnToolTip(17); lb.Parent.Visible = GridColumnVisible(17); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkAlt1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(18); lb.ToolTip = ColumnToolTip(18); lb.Parent.Visible = GridColumnVisible(18); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkOrd1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(19); lb.ToolTip = ColumnToolTip(19); lb.Parent.Visible = GridColumnVisible(19); }
		    lb = cAdmPageObjGrid.FindControl("cPopup1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(20); lb.ToolTip = ColumnToolTip(20); lb.Parent.Visible = GridColumnVisible(20); }
		    lb = cAdmPageObjGrid.FindControl("cPageLnkCss1278hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(21); lb.ToolTip = ColumnToolTip(21); lb.Parent.Visible = GridColumnVisible(21); }
		    // Hide DeleteAll:
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if ((dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
		            lb = cAdmPageObjGrid.FindControl("cDeleteAllButton") as LinkButton; if (lb != null) { lb.Visible = false; }
				}
			}
		    // footer:
		    Label gc = null;
		    gc = cAdmPageObjGrid.FindControl("cPageLnkId1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(14); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkTxt1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(15); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkRef1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(16); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkImg1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(17); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkAlt1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(18); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkOrd1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(19); }
		    gc = cAdmPageObjGrid.FindControl("cPopup1278fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(20); }
		    gc = cAdmPageObjGrid.FindControl("cPageLnkCss1278fl") as Label;
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
			if (cGridUploadBtn.Attributes["OnClick"] == null || cGridUploadBtn.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cGridUploadBtn.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmPageObj1001List,null); }

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

