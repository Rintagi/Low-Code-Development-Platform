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
	public class AdmScreen9 : DataSet
	{
		public AdmScreen9()
		{
			this.Tables.Add(MakeColumns(new DataTable("AdmScreen")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenDef")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenAdd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenUpd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenDel")));
			this.DataSetName = "AdmScreen9";
			this.Namespace = "http://Rintagi.com/DataSet/AdmScreen9";
		}

		private DataTable MakeColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ScreenId15", typeof(string));
			columns.Add("ProgramName15", typeof(string));
			columns.Add("ScreenTypeId15", typeof(string));
			columns.Add("ViewOnly15", typeof(string));
			columns.Add("SearchAscending15", typeof(string));
			columns.Add("MasterTableId15", typeof(string));
			columns.Add("SearchTableId15", typeof(string));
			columns.Add("SearchId15", typeof(string));
			columns.Add("SearchIdR15", typeof(string));
			columns.Add("SearchDtlId15", typeof(string));
			columns.Add("SearchDtlIdR15", typeof(string));
			columns.Add("SearchUrlId15", typeof(string));
			columns.Add("SearchImgId15", typeof(string));
			columns.Add("DetailTableId15", typeof(string));
			columns.Add("GridRows15", typeof(string));
			columns.Add("HasDeleteAll15", typeof(string));
			columns.Add("ShowGridHead15", typeof(string));
			columns.Add("GenerateSc15", typeof(string));
			columns.Add("GenerateSr15", typeof(string));
			columns.Add("ValidateReq15", typeof(string));
			columns.Add("DeferError15", typeof(string));
			columns.Add("AuthRequired15", typeof(string));
			columns.Add("GenAudit15", typeof(string));
			columns.Add("ScreenObj15", typeof(string));
			columns.Add("ScreenFilter", typeof(string));
			columns.Add("MoreInfo", typeof(string));
			return dt;
		}

		private DataTable MakeDtlColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("ScreenId15", typeof(string));
			columns.Add("ScreenHlpId16", typeof(string));
			columns.Add("CultureId16", typeof(string));
			columns.Add("ScreenTitle16", typeof(string));
			columns.Add("DefaultHlpMsg16", typeof(string));
			columns.Add("FootNote16", typeof(string));
			columns.Add("AddMsg16", typeof(string));
			columns.Add("UpdMsg16", typeof(string));
			columns.Add("DelMsg16", typeof(string));
			columns.Add("IncrementMsg16", typeof(string));
			columns.Add("NoMasterMsg16", typeof(string));
			columns.Add("NoDetailMsg16", typeof(string));
			columns.Add("AddMasterMsg16", typeof(string));
			columns.Add("AddDetailMsg16", typeof(string));
			columns.Add("MasterLstTitle16", typeof(string));
			columns.Add("MasterLstSubtitle16", typeof(string));
			columns.Add("MasterRecTitle16", typeof(string));
			columns.Add("MasterRecSubtitle16", typeof(string));
			columns.Add("MasterFoundMsg16", typeof(string));
			columns.Add("DetailLstTitle16", typeof(string));
			columns.Add("DetailLstSubtitle16", typeof(string));
			columns.Add("DetailRecTitle16", typeof(string));
			columns.Add("DetailRecSubtitle16", typeof(string));
			columns.Add("DetailFoundMsg16", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmScreenModule : RO.Web.ModuleBase
	{

		private const string KEY_lastAddedRow = "Cache:lastAddedRow3_9";
		private const string KEY_lastSortOrd = "Cache:lastSortOrd3_9";
		private const string KEY_lastSortImg = "Cache:lastSortImg3_9";
		private const string KEY_lastSortCol = "Cache:lastSortCol3_9";
		private const string KEY_lastSortExp = "Cache:lastSortExp3_9";
		private const string KEY_lastSortUrl = "Cache:lastSortUrl3_9";
		private const string KEY_lastSortTog = "Cache:lastSortTog3_9";
		private const string KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride3_9";
		private const string KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride3_9";
		private const string KEY_currPageIndex = "Cache:currPageIndex3_9";
		private const string KEY_bHiImpVisible = "Cache:bHiImpVisible3_9";
		private const string KEY_bShImpVisible = "Cache:bShImpVisible3_9";
		private const string KEY_dtAdmScreenGrid = "Cache:dtAdmScreenGrid";
		private const string KEY_scrImport = "Cache:scrImport3_9";
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_9";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_9";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_9";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_9";
		private const string KEY_dtLabel = "Cache:dtLabel3_9";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_9";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_9";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_9";

		private const string KEY_dtScreenTypeId15 = "Cache:dtScreenTypeId15";
		private const string KEY_dtViewOnly15 = "Cache:dtViewOnly15";
		private const string KEY_dtMasterTableId15 = "Cache:dtMasterTableId15";
		private const string KEY_dtSearchTableId15 = "Cache:dtSearchTableId15";
		private const string KEY_dtSearchId15 = "Cache:dtSearchId15";
		private const string KEY_dtSearchIdR15 = "Cache:dtSearchIdR15";
		private const string KEY_dtSearchDtlId15 = "Cache:dtSearchDtlId15";
		private const string KEY_dtSearchDtlIdR15 = "Cache:dtSearchDtlIdR15";
		private const string KEY_dtSearchUrlId15 = "Cache:dtSearchUrlId15";
		private const string KEY_dtSearchImgId15 = "Cache:dtSearchImgId15";
		private const string KEY_dtDetailTableId15 = "Cache:dtDetailTableId15";
		private const string KEY_dtCultureId16 = "Cache:dtCultureId16";

		private const string KEY_dtSystems = "Cache:dtSystems3_9";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_9";
		private const string KEY_dtAdmScreen9List = "Cache:dtAdmScreen9List";
		private const string KEY_bNewVisible = "Cache:bNewVisible3_9";
		private const string KEY_bNewSaveVisible = "Cache:bNewSaveVisible3_9";
		private const string KEY_bCopyVisible = "Cache:bCopyVisible3_9";
		private const string KEY_bCopySaveVisible = "Cache:bCopySaveVisible3_9";
		private const string KEY_bDeleteVisible = "Cache:bDeleteVisible3_9";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_9";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_9";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_9";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmScreenModule()
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
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,9, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				cFind.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cFindButton.ClientID + "')");
				cPgSize.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cPgSizeButton.ClientID + "')");
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
				Session.Remove(KEY_dtAdmScreenGrid);
				Session.Remove(KEY_lastSortCol);
				Session.Remove(KEY_lastSortExp);
				Session.Remove(KEY_lastSortImg);
				Session.Remove(KEY_lastSortUrl);
				Session.Remove(KEY_dtAdmScreen9List);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtScreenTypeId15);
				Session.Remove(KEY_dtViewOnly15);
				Session.Remove(KEY_dtMasterTableId15);
				Session.Remove(KEY_dtSearchTableId15);
				Session.Remove(KEY_dtSearchId15);
				Session.Remove(KEY_dtSearchIdR15);
				Session.Remove(KEY_dtSearchDtlId15);
				Session.Remove(KEY_dtSearchDtlIdR15);
				Session.Remove(KEY_dtSearchUrlId15);
				Session.Remove(KEY_dtSearchImgId15);
				Session.Remove(KEY_dtDetailTableId15);
				Session.Remove(KEY_dtCultureId16);
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
				cTab7.InnerText = dt.Rows[0]["TabFolderName"].ToString();
				cTab8.InnerText = dt.Rows[1]["TabFolderName"].ToString();
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 9, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmScreen9List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=9&typ=N&sys=3','','',''); return false;"; }
				cMasterTableId15Search_Script();
				cSearchTableId15Search_Script();
				//WebRule: Create code table screen when requested
                if (!string.IsNullOrEmpty(Request.QueryString["ctb"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                    {
                        cProgramName15.Text = Request.QueryString["ctb"].ToString();
                        SetScreenTypeId15(cScreenTypeId15, "7"); cScreenTypeId15_SelectedIndexChanged(cScreenTypeId15, new EventArgs());
                        SetMasterTableId15(cMasterTableId15, Request.QueryString["cid"].ToString());
                        cSaveButton_Click(sender, new EventArgs());
                    }
                    Response.Redirect((((Config.SslUrl).StartsWith("http") ? (new Uri(Config.SslUrl)) : Request.Url).GetLeftPart(UriPartial.Scheme) + Request.Url.Host + Request.Url.AbsolutePath.ToLower()).Replace("admscreen", Request.QueryString["ctb"].ToString()) + "?msy=" + LCurr.DbId.ToString());
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
				DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
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
					dt = (new AdminSystem()).GetAuthRow(9,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
					Session[KEY_dtAuthRow] = dt;
					DataRow dr = dt.Rows[0];
					if (!((dr["AllowUpd"].ToString() == "N" || dr["ViewOnly"].ToString() == "G") && dr["AllowAdd"].ToString() == "N") && (Request.QueryString["enb"] == null || Request.QueryString["enb"] != "N"))
					{
						cAdmScreenGrid.PreRender += new EventHandler(cAdmScreenGrid_OnPreRender);
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
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,9,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(9, "Screen Definition", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetButtonHlp(9,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtRul = (new AdminSystem()).GetClientRule(9,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					if (ee == null || drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on" || (cAdmScreenGrid.EditIndex > -1 && GetDataItemIndex(cAdmScreenGrid.EditIndex) == ee.DataItemIndex))
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
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(9,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dtHlp = (new AdminSystem()).GetScreenHlp(9,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,9,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				DataTable dt = (new AdminSystem()).GetScreenFilter(9,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetScreenLabel(9,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(9,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthRow(9,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
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
					try {dv = new DataView((new AdminSystem()).GetExp(9,"GetExpAdmScreen9","Y",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
					catch {dv = new DataView((new AdminSystem()).GetExp(9,"GetExpAdmScreen9","N",(string)Session[KEY_sysConnectionString],LcAppPw,filterId,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false)));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(9,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[2]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[3]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[5]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[6]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[7]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[8]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[9]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[9]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[10]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[10]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[11]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[11]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[12]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[12]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[13]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[13]["ColumnHeader"].ToString() + " Text" + (char)9);}
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
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[26]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[27]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[27]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[28]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[29]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[30]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[31]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[32]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[33]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[34]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[35]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[36]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[37]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[38]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[39]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[40]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[41]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[42]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[43]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[44]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[45]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[45]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[46]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[46]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[47]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[47]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[48]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[48]["ColumnHeader"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenId15"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ProgramName15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenTypeId15"].ToString() + (char)9 + drv["ScreenTypeId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ViewOnly15"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["ViewOnly15Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["SearchAscending15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterTableId15"].ToString() + (char)9 + drv["MasterTableId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchTableId15"].ToString() + (char)9 + drv["SearchTableId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchId15"].ToString() + (char)9 + drv["SearchId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchIdR15"].ToString() + (char)9 + drv["SearchIdR15Text"].ToString() + (char)9);}
						if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchDtlId15"].ToString() + (char)9 + drv["SearchDtlId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchDtlIdR15"].ToString() + (char)9 + drv["SearchDtlIdR15Text"].ToString() + (char)9);}
						if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchUrlId15"].ToString() + (char)9 + drv["SearchUrlId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchImgId15"].ToString() + (char)9 + drv["SearchImgId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailTableId15"].ToString() + (char)9 + drv["DetailTableId15Text"].ToString() + (char)9);}
						if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["GridRows15"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["HasDeleteAll15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ShowGridHead15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GenerateSc15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GenerateSr15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ValidateReq15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DeferError15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AuthRequired15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["GenAudit15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ScreenObj15"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenHlpId16"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId16"].ToString() + (char)9 + drv["CultureId16Text"].ToString() + (char)9);}
						if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ScreenTitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DefaultHlpMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["FootNote16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AddMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["UpdMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DelMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["IncrementMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["NoMasterMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["NoDetailMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AddMasterMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["AddDetailMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterLstTitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterLstSubtitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterRecTitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterRecSubtitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["MasterFoundMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DetailLstTitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[45]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DetailLstSubtitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[46]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DetailRecTitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[47]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DetailRecSubtitle16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[48]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["DetailFoundMsg16"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmScreen.xls"; Session["ExportStr"] = sb.Replace("\r\n","\n");
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
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmScreen.rtf"; Session["ExportStr"] = sb;
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
				dtAu = (new AdminSystem()).GetAuthExp(9,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					if (dtAu.Rows[45]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[46]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[47]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[48]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
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
					if (dtAu.Rows[45]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[45]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[46]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[46]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[47]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[47]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[48]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[48]["ColumnHeader"].ToString() + @"\cell ");}
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
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenId15"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ProgramName15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenTypeId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["ViewOnly15Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchAscending15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterTableId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchTableId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchIdR15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[9]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchDtlId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[10]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchDtlIdR15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[11]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchUrlId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[12]["ColExport"].ToString() == "Y") {sb.Append(drv["SearchImgId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[13]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailTableId15Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[14]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["GridRows15"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[15]["ColExport"].ToString() == "Y") {sb.Append(drv["HasDeleteAll15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[16]["ColExport"].ToString() == "Y") {sb.Append(drv["ShowGridHead15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[17]["ColExport"].ToString() == "Y") {sb.Append(drv["GenerateSc15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[18]["ColExport"].ToString() == "Y") {sb.Append(drv["GenerateSr15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[19]["ColExport"].ToString() == "Y") {sb.Append(drv["ValidateReq15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[20]["ColExport"].ToString() == "Y") {sb.Append(drv["DeferError15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[21]["ColExport"].ToString() == "Y") {sb.Append(drv["AuthRequired15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[22]["ColExport"].ToString() == "Y") {sb.Append(drv["GenAudit15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[23]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenObj15"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[26]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ScreenHlpId16"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[27]["ColExport"].ToString() == "Y") {sb.Append(drv["CultureId16Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[28]["ColExport"].ToString() == "Y") {sb.Append(drv["ScreenTitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[29]["ColExport"].ToString() == "Y") {sb.Append(drv["DefaultHlpMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[30]["ColExport"].ToString() == "Y") {sb.Append(drv["FootNote16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[31]["ColExport"].ToString() == "Y") {sb.Append(drv["AddMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[32]["ColExport"].ToString() == "Y") {sb.Append(drv["UpdMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[33]["ColExport"].ToString() == "Y") {sb.Append(drv["DelMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[34]["ColExport"].ToString() == "Y") {sb.Append(drv["IncrementMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[35]["ColExport"].ToString() == "Y") {sb.Append(drv["NoMasterMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[36]["ColExport"].ToString() == "Y") {sb.Append(drv["NoDetailMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[37]["ColExport"].ToString() == "Y") {sb.Append(drv["AddMasterMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[38]["ColExport"].ToString() == "Y") {sb.Append(drv["AddDetailMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[39]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterLstTitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[40]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterLstSubtitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[41]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterRecTitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[42]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterRecSubtitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[43]["ColExport"].ToString() == "Y") {sb.Append(drv["MasterFoundMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[44]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailLstTitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[45]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailLstSubtitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[46]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailRecTitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[47]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailRecSubtitle16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[48]["ColExport"].ToString() == "Y") {sb.Append(drv["DetailFoundMsg16"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
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

		public void cMasterTableId15Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			RoboCoder.WebControls.ComboBox cc = cMasterTableId15; cc.SetTbVisible(); Session["CtrlAdmDbTable"] = cc.FocusID;
		}

		public void cMasterTableId15Search_Script()
		{
				ImageButton ib = cMasterTableId15Search;
				if (ib != null)
				{
			    	TextBox pp = cScreenId15;
					RoboCoder.WebControls.ComboBox cc = cMasterTableId15;
					string ss = "&dsp=ComboBox"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmDbTable.aspx?col=DbTableId" + ss + "&typ=N" + "','" + cc.FocusID + "','',''); return false;";
				}
		}

		public void cSearchTableId15Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			RoboCoder.WebControls.ComboBox cc = cSearchTableId15; cc.SetTbVisible(); Session["CtrlAdmDbTable"] = cc.FocusID;
		}

		public void cSearchTableId15Search_Script()
		{
				ImageButton ib = cSearchTableId15Search;
				if (ib != null)
				{
			    	TextBox pp = cScreenId15;
					RoboCoder.WebControls.ComboBox cc = cSearchTableId15;
					string ss = "&dsp=ComboBox"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmDbTable.aspx?col=DbTableId" + ss + "&typ=N" + "','" + cc.FocusID + "','',''); return false;";
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
				dt = (new AdminSystem()).GetLastPageInfo(9, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 9, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
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
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (cAdmScreenGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
			PopAdmScreen9List(sender, e, false, null);
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
						(new AdminSystem()).MkGetScreenIn("9", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("9", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						(new AdminSystem()).MkGetScreenIn("9", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("9", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], LcAppPw), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw));
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
					dtScrCri = (new AdminSystem()).GetScrCriteria("9", LcSysConnString, LcAppPw);
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
		            context["scr"] = "9";
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
		            context["scr"] = "9";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmScreen9" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmScreen9" + cListBox.ID;
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
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("9", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], LcAppPw)).Count;
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
					(new AdminSystem()).UpdScrCriteria("9", "AdmScreen9", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
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
				dtScreenTab = (new AdminSystem()).GetScreenTab(9,base.LUser.CultureId,LcSysConnString,LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return dtScreenTab;
		}

		private void SetScreenTypeId15(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtScreenTypeId15];
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
						dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlScreenTypeId3S45",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ScreenTypeId15 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlScreenTypeId3S45",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlScreenTypeId3S45",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtScreenTypeId15] = dv.Table;
				}
			}
		}

		private void SetViewOnly15(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtViewOnly15];
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
						dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlViewOnly3S3103",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
					}
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "ViewOnly15 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlViewOnly3S3103",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlViewOnly3S3103",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtViewOnly15] = dv.Table;
				}
			}
		}

		protected void cbMasterTableId15(object sender, System.EventArgs e)
		{
			SetMasterTableId15((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetMasterTableId15(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlMasterTableId3S59";
			context["addnew"] = "Y";
			context["mKey"] = "MasterTableId15";
			context["mVal"] = "MasterTableId15Text";
			context["mTip"] = "MasterTableId15Text";
			context["mImg"] = "MasterTableId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
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
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlMasterTableId3S59",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtMasterTableId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchTableId15(object sender, System.EventArgs e)
		{
			SetSearchTableId15((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetSearchTableId15(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchTableId3S4145";
			context["addnew"] = "Y";
			context["mKey"] = "SearchTableId15";
			context["mVal"] = "SearchTableId15Text";
			context["mTip"] = "SearchTableId15Text";
			context["mImg"] = "SearchTableId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
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
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchTableId3S4145",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchTableId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchId15(object sender, System.EventArgs e)
		{
			SetSearchId15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchId15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchId3S46";
			context["addnew"] = "Y";
			context["mKey"] = "SearchId15";
			context["mVal"] = "SearchId15Text";
			context["mTip"] = "SearchId15Text";
			context["mImg"] = "SearchId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchId3S46",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchIdR15(object sender, System.EventArgs e)
		{
			SetSearchIdR15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchIdR15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchIdR3S4164";
			context["addnew"] = "Y";
			context["mKey"] = "SearchIdR15";
			context["mVal"] = "SearchIdR15Text";
			context["mTip"] = "SearchIdR15Text";
			context["mImg"] = "SearchIdR15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchIdR3S4164",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchIdR15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchDtlId15(object sender, System.EventArgs e)
		{
			SetSearchDtlId15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchDtlId15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchDtlId3S4148";
			context["addnew"] = "Y";
			context["mKey"] = "SearchDtlId15";
			context["mVal"] = "SearchDtlId15Text";
			context["mTip"] = "SearchDtlId15Text";
			context["mImg"] = "SearchDtlId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchDtlId3S4148",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchDtlId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchDtlIdR15(object sender, System.EventArgs e)
		{
			SetSearchDtlIdR15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchDtlIdR15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchDtlIdR3S4165";
			context["addnew"] = "Y";
			context["mKey"] = "SearchDtlIdR15";
			context["mVal"] = "SearchDtlIdR15Text";
			context["mTip"] = "SearchDtlIdR15Text";
			context["mImg"] = "SearchDtlIdR15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchDtlIdR3S4165",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchDtlIdR15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchUrlId15(object sender, System.EventArgs e)
		{
			SetSearchUrlId15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchUrlId15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchUrlId3S4147";
			context["addnew"] = "Y";
			context["mKey"] = "SearchUrlId15";
			context["mVal"] = "SearchUrlId15Text";
			context["mTip"] = "SearchUrlId15Text";
			context["mImg"] = "SearchUrlId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchUrlId3S4147",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchUrlId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbSearchImgId15(object sender, System.EventArgs e)
		{
			SetSearchImgId15((RoboCoder.WebControls.ComboBox)sender,string.Empty,cSearchTableId15.SelectedValue);
		}

		private void SetSearchImgId15(RoboCoder.WebControls.ComboBox ddl, string keyId, string filtr)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlSearchImgId3S4146";
			context["addnew"] = "Y";
			context["mKey"] = "SearchImgId15";
			context["mVal"] = "SearchImgId15Text";
			context["mTip"] = "SearchImgId15Text";
			context["mImg"] = "SearchImgId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			context["refColCID"] = cSearchTableId15.ClientID;
			context["refCol"] = "TableId";
			context["refColDataType"] = "Int";
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlSearchImgId3S4146",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtSearchImgId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,filtr != string.Empty ? "(TableId is null OR TableId = " + filtr + ")" : string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbDetailTableId15(object sender, System.EventArgs e)
		{
			SetDetailTableId15((RoboCoder.WebControls.ComboBox)sender,string.Empty);
		}

		private void SetDetailTableId15(RoboCoder.WebControls.ComboBox ddl, string keyId)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlDetailTableId3S60";
			context["addnew"] = "Y";
			context["mKey"] = "DetailTableId15";
			context["mVal"] = "DetailTableId15Text";
			context["mTip"] = "DetailTableId15Text";
			context["mImg"] = "DetailTableId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
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
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlDetailTableId3S60",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtDetailTableId15] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		protected void cbCultureId16(object sender, System.EventArgs e)
		{
			SetCultureId16((RoboCoder.WebControls.ComboBox)sender,string.Empty,cAdmScreenGrid.EditItem);
		}

		private void SetCultureId16(RoboCoder.WebControls.ComboBox ddl, string keyId, ListViewItem li)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlCultureId3S53";
			context["addnew"] = "Y";
			context["mKey"] = "CultureId16";
			context["mVal"] = "CultureId16Text";
			context["mTip"] = "CultureId16Text";
			context["mImg"] = "CultureId16Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
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
					dv = new DataView((new AdminSystem()).GetDdl(9,"GetDdlCultureId3S53",true,false,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("ScreenId"))
					{
						context["pMKeyColID"] = cAdmScreen9List.ClientID;
						context["pMKeyCol"] = "ScreenId";
						string ss = "(ScreenId is null";
						if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR ScreenId = " + cAdmScreen9List.SelectedValue + ")";}
						dv.RowFilter = ss;
					}
					ddl.DataSource = dv; Session[KEY_dtCultureId16] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private DataView GetAdmScreen9List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreen9List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0;
				string key = string.Empty;
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				else if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				try
				{
					try {dt = (new AdminSystem()).GetLis(9,"GetLisAdmScreen9",true,"Y",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
					catch {dt = (new AdminSystem()).GetLis(9,"GetLisAdmScreen9",true,"N",0,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				dt = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmScreen9List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmScreen9List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmScreen9";
			context["mKey"] = "ScreenId15";
			context["mVal"] = "ScreenId15Text";
			context["mTip"] = "ScreenId15Text";
			context["mImg"] = "ScreenId15Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "9";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue) && cFilter.Visible ? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			cAdmScreen9List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmScreen9List.DataContext = context;
			if (dv.Table == null) return;
			cAdmScreen9List.DataSource = dv;
			cAdmScreen9List.Visible = true;
			if (cAdmScreen9List.Items.Count <= 0) {cAdmScreen9List.Visible = false; cAdmScreen9List_SelectedIndexChanged(sender,e);}
			else
			{
				if (bPageLoad && !string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					try {cAdmScreen9List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmScreen9List.Items[0].Selected = true; cAdmScreen9List_SelectedIndexChanged(sender, e);}
				    cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
				}
				else
				{
					if (ID != null) {if (!cAdmScreen9List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
					if (ID == null)
					{
						cAdmScreen9List_SelectedIndexChanged(sender, e);
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
				base.SetFoldBehavior(cScreenId15, dtAuth.Rows[0], cScreenId15P1, cScreenId15Label, cScreenId15P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cProgramName15, dtAuth.Rows[1], cProgramName15P1, cProgramName15Label, cProgramName15P2, null, dtLabel.Rows[1], cRFVProgramName15, null, null);
				base.SetFoldBehavior(cScreenTypeId15, dtAuth.Rows[2], cScreenTypeId15P1, cScreenTypeId15Label, cScreenTypeId15P2, null, dtLabel.Rows[2], cRFVScreenTypeId15, null, null);
				base.SetFoldBehavior(cViewOnly15, dtAuth.Rows[3], cViewOnly15P1, cViewOnly15Label, cViewOnly15P2, null, dtLabel.Rows[3], cRFVViewOnly15, null, null);
				base.SetFoldBehavior(cSearchAscending15, dtAuth.Rows[4], cSearchAscending15P1, cSearchAscending15Label, cSearchAscending15P2, null, dtLabel.Rows[4], null, null, null);
				base.SetFoldBehavior(cMasterTableId15, dtAuth.Rows[5], cMasterTableId15P1, cMasterTableId15Label, cMasterTableId15P2, null, dtLabel.Rows[5], cRFVMasterTableId15, null, null);
				SetMasterTableId15(cMasterTableId15,string.Empty);
				base.SetFoldBehavior(cSearchTableId15, dtAuth.Rows[6], cSearchTableId15P1, cSearchTableId15Label, cSearchTableId15P2, null, dtLabel.Rows[6], null, null, null);
				SetSearchTableId15(cSearchTableId15,string.Empty);
				base.SetFoldBehavior(cSearchId15, dtAuth.Rows[7], cSearchId15P1, cSearchId15Label, cSearchId15P2, null, dtLabel.Rows[7], null, null, null);
				SetSearchId15(cSearchId15,string.Empty,string.Empty);
				base.SetFoldBehavior(cSearchIdR15, dtAuth.Rows[8], cSearchIdR15P1, cSearchIdR15Label, cSearchIdR15P2, null, dtLabel.Rows[8], null, null, null);
				SetSearchIdR15(cSearchIdR15,string.Empty,string.Empty);
				base.SetFoldBehavior(cSearchDtlId15, dtAuth.Rows[9], cSearchDtlId15P1, cSearchDtlId15Label, cSearchDtlId15P2, null, dtLabel.Rows[9], null, null, null);
				SetSearchDtlId15(cSearchDtlId15,string.Empty,string.Empty);
				base.SetFoldBehavior(cSearchDtlIdR15, dtAuth.Rows[10], cSearchDtlIdR15P1, cSearchDtlIdR15Label, cSearchDtlIdR15P2, null, dtLabel.Rows[10], null, null, null);
				SetSearchDtlIdR15(cSearchDtlIdR15,string.Empty,string.Empty);
				base.SetFoldBehavior(cSearchUrlId15, dtAuth.Rows[11], cSearchUrlId15P1, cSearchUrlId15Label, cSearchUrlId15P2, null, dtLabel.Rows[11], null, null, null);
				SetSearchUrlId15(cSearchUrlId15,string.Empty,string.Empty);
				base.SetFoldBehavior(cSearchImgId15, dtAuth.Rows[12], cSearchImgId15P1, cSearchImgId15Label, cSearchImgId15P2, null, dtLabel.Rows[12], null, null, null);
				SetSearchImgId15(cSearchImgId15,string.Empty,string.Empty);
				base.SetFoldBehavior(cDetailTableId15, dtAuth.Rows[13], cDetailTableId15P1, cDetailTableId15Label, cDetailTableId15P2, null, dtLabel.Rows[13], null, null, null);
				SetDetailTableId15(cDetailTableId15,string.Empty);
				base.SetFoldBehavior(cGridRows15, dtAuth.Rows[14], cGridRows15P1, cGridRows15Label, cGridRows15P2, null, dtLabel.Rows[14], null, null, null);
				base.SetFoldBehavior(cHasDeleteAll15, dtAuth.Rows[15], cHasDeleteAll15P1, cHasDeleteAll15Label, cHasDeleteAll15P2, null, dtLabel.Rows[15], null, null, null);
				base.SetFoldBehavior(cShowGridHead15, dtAuth.Rows[16], cShowGridHead15P1, cShowGridHead15Label, cShowGridHead15P2, null, dtLabel.Rows[16], null, null, null);
				base.SetFoldBehavior(cGenerateSc15, dtAuth.Rows[17], cGenerateSc15P1, cGenerateSc15Label, cGenerateSc15P2, null, dtLabel.Rows[17], null, null, null);
				base.SetFoldBehavior(cGenerateSr15, dtAuth.Rows[18], cGenerateSr15P1, cGenerateSr15Label, cGenerateSr15P2, null, dtLabel.Rows[18], null, null, null);
				base.SetFoldBehavior(cValidateReq15, dtAuth.Rows[19], cValidateReq15P1, cValidateReq15Label, cValidateReq15P2, null, dtLabel.Rows[19], null, null, null);
				base.SetFoldBehavior(cDeferError15, dtAuth.Rows[20], cDeferError15P1, cDeferError15Label, cDeferError15P2, null, dtLabel.Rows[20], null, null, null);
				base.SetFoldBehavior(cAuthRequired15, dtAuth.Rows[21], cAuthRequired15P1, cAuthRequired15Label, cAuthRequired15P2, null, dtLabel.Rows[21], null, null, null);
				base.SetFoldBehavior(cGenAudit15, dtAuth.Rows[22], cGenAudit15P1, cGenAudit15Label, cGenAudit15P2, null, dtLabel.Rows[22], null, null, null);
				base.SetFoldBehavior(cScreenObj15, dtAuth.Rows[23], cScreenObj15P1, cScreenObj15Label, cScreenObj15P2, null, dtLabel.Rows[23], null, null, null);
				base.SetFoldBehavior(cScreenFilter, dtAuth.Rows[24], cScreenFilterP1, cScreenFilterLabel, cScreenFilterP2, null, dtLabel.Rows[24], null, null, null);
				base.SetFoldBehavior(cMoreInfo, dtAuth.Rows[25], cMoreInfoP1, cMoreInfoLabel, cMoreInfoP2, null, dtLabel.Rows[25], null, null, null);
			}
			if ((cProgramName15.Attributes["OnChange"] == null || cProgramName15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cProgramName15.Visible && !cProgramName15.ReadOnly) {cProgramName15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cScreenTypeId15.Attributes["OnChange"] == null || cScreenTypeId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cScreenTypeId15.Visible && cScreenTypeId15.Enabled) {cScreenTypeId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cViewOnly15.Attributes["OnChange"] == null || cViewOnly15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cViewOnly15.Visible && cViewOnly15.Enabled) {cViewOnly15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchAscending15.Attributes["OnClick"] == null || cSearchAscending15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSearchAscending15.Visible && cSearchAscending15.Enabled) {cSearchAscending15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cMasterTableId15.Attributes["OnChange"] == null || cMasterTableId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMasterTableId15.Visible && cMasterTableId15.Enabled) {cMasterTableId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if (cMasterTableId15Search.Attributes["OnClick"] == null || cMasterTableId15Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cMasterTableId15Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cSearchTableId15.Attributes["OnChange"] == null || cSearchTableId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchTableId15.Visible && cSearchTableId15.Enabled) {cSearchTableId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSearchTableId15Search.Attributes["OnClick"] == null || cSearchTableId15Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cSearchTableId15Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cSearchId15.Attributes["OnChange"] == null || cSearchId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchId15.Visible && cSearchId15.Enabled) {cSearchId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchIdR15.Attributes["OnChange"] == null || cSearchIdR15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchIdR15.Visible && cSearchIdR15.Enabled) {cSearchIdR15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchDtlId15.Attributes["OnChange"] == null || cSearchDtlId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchDtlId15.Visible && cSearchDtlId15.Enabled) {cSearchDtlId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchDtlIdR15.Attributes["OnChange"] == null || cSearchDtlIdR15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchDtlIdR15.Visible && cSearchDtlIdR15.Enabled) {cSearchDtlIdR15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchUrlId15.Attributes["OnChange"] == null || cSearchUrlId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchUrlId15.Visible && cSearchUrlId15.Enabled) {cSearchUrlId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cSearchImgId15.Attributes["OnChange"] == null || cSearchImgId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cSearchImgId15.Visible && cSearchImgId15.Enabled) {cSearchImgId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cDetailTableId15.Attributes["OnChange"] == null || cDetailTableId15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cDetailTableId15.Visible && cDetailTableId15.Enabled) {cDetailTableId15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cGridRows15.Attributes["OnChange"] == null || cGridRows15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cGridRows15.Visible && !cGridRows15.ReadOnly) {cGridRows15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cHasDeleteAll15.Attributes["OnClick"] == null || cHasDeleteAll15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cHasDeleteAll15.Visible && cHasDeleteAll15.Enabled) {cHasDeleteAll15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cShowGridHead15.Attributes["OnClick"] == null || cShowGridHead15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cShowGridHead15.Visible && cShowGridHead15.Enabled) {cShowGridHead15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cGenerateSc15.Attributes["OnClick"] == null || cGenerateSc15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cGenerateSc15.Visible && cGenerateSc15.Enabled) {cGenerateSc15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cGenerateSr15.Attributes["OnClick"] == null || cGenerateSr15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cGenerateSr15.Visible && cGenerateSr15.Enabled) {cGenerateSr15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cValidateReq15.Attributes["OnClick"] == null || cValidateReq15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cValidateReq15.Visible && cValidateReq15.Enabled) {cValidateReq15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cDeferError15.Attributes["OnClick"] == null || cDeferError15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDeferError15.Visible && cDeferError15.Enabled) {cDeferError15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cAuthRequired15.Attributes["OnClick"] == null || cAuthRequired15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cAuthRequired15.Visible && cAuthRequired15.Enabled) {cAuthRequired15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cGenAudit15.Attributes["OnClick"] == null || cGenAudit15.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cGenAudit15.Visible && cGenAudit15.Enabled) {cGenAudit15.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
			if ((cScreenObj15.Attributes["OnChange"] == null || cScreenObj15.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cScreenObj15.Visible && cScreenObj15.Enabled) {cScreenObj15.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cScreenFilter.Attributes["OnChange"] == null || cScreenFilter.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cScreenFilter.Visible && cScreenFilter.Enabled) {cScreenFilter.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
			if ((cMoreInfo.Attributes["OnChange"] == null || cMoreInfo.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cMoreInfo.Visible && cMoreInfo.Enabled) {cMoreInfo.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (cAdmScreenGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				cNewButton_Click(sender, new EventArgs());
			}
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (cAdmScreenGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
				Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
				Session.Remove(KEY_dtScreenTypeId15);
				Session.Remove(KEY_dtViewOnly15);
				Session.Remove(KEY_dtMasterTableId15);
				Session.Remove(KEY_dtSearchTableId15);
				Session.Remove(KEY_dtSearchId15);
				Session.Remove(KEY_dtSearchIdR15);
				Session.Remove(KEY_dtSearchDtlId15);
				Session.Remove(KEY_dtSearchDtlIdR15);
				Session.Remove(KEY_dtSearchUrlId15);
				Session.Remove(KEY_dtSearchImgId15);
				Session.Remove(KEY_dtDetailTableId15);
				Session.Remove(KEY_dtCultureId16);
				GetCriteria(GetScrCriteria());
				cFilterId_SelectedIndexChanged(sender, e);
				cMasterTableId15Search_Script();
				cSearchTableId15Search_Script();
			}
		}

		private void ClearMaster(object sender, System.EventArgs e)
		{
			DataTable dt = GetAuthCol();
			if (dt.Rows[1]["ColVisible"].ToString() == "Y" && dt.Rows[1]["ColReadOnly"].ToString() != "Y") {cProgramName15.Text = string.Empty;}
			if (dt.Rows[2]["ColVisible"].ToString() == "Y" && dt.Rows[2]["ColReadOnly"].ToString() != "Y") {SetScreenTypeId15(cScreenTypeId15,string.Empty); cScreenTypeId15_SelectedIndexChanged(cScreenTypeId15, new EventArgs());}
			if (dt.Rows[3]["ColVisible"].ToString() == "Y" && dt.Rows[3]["ColReadOnly"].ToString() != "Y") {SetViewOnly15(cViewOnly15,"N");}
			if (dt.Rows[4]["ColVisible"].ToString() == "Y" && dt.Rows[4]["ColReadOnly"].ToString() != "Y") {cSearchAscending15.Checked = base.GetBool("Y");}
			if (dt.Rows[5]["ColVisible"].ToString() == "Y" && dt.Rows[5]["ColReadOnly"].ToString() != "Y") {cMasterTableId15.ClearSearch();}
			if (dt.Rows[6]["ColVisible"].ToString() == "Y" && dt.Rows[6]["ColReadOnly"].ToString() != "Y") {cSearchTableId15.ClearSearch();}
			if (dt.Rows[7]["ColVisible"].ToString() == "Y" && dt.Rows[7]["ColReadOnly"].ToString() != "Y") {cSearchId15.ClearSearch();}
			if (dt.Rows[8]["ColVisible"].ToString() == "Y" && dt.Rows[8]["ColReadOnly"].ToString() != "Y") {cSearchIdR15.ClearSearch();}
			if (dt.Rows[9]["ColVisible"].ToString() == "Y" && dt.Rows[9]["ColReadOnly"].ToString() != "Y") {cSearchDtlId15.ClearSearch();}
			if (dt.Rows[10]["ColVisible"].ToString() == "Y" && dt.Rows[10]["ColReadOnly"].ToString() != "Y") {cSearchDtlIdR15.ClearSearch();}
			if (dt.Rows[11]["ColVisible"].ToString() == "Y" && dt.Rows[11]["ColReadOnly"].ToString() != "Y") {cSearchUrlId15.ClearSearch();}
			if (dt.Rows[12]["ColVisible"].ToString() == "Y" && dt.Rows[12]["ColReadOnly"].ToString() != "Y") {cSearchImgId15.ClearSearch();}
			if (dt.Rows[13]["ColVisible"].ToString() == "Y" && dt.Rows[13]["ColReadOnly"].ToString() != "Y") {cDetailTableId15.ClearSearch();}
			if (dt.Rows[14]["ColVisible"].ToString() == "Y" && dt.Rows[14]["ColReadOnly"].ToString() != "Y") {cGridRows15.Text = "12";}
			if (dt.Rows[15]["ColVisible"].ToString() == "Y" && dt.Rows[15]["ColReadOnly"].ToString() != "Y") {cHasDeleteAll15.Checked = base.GetBool("Y");}
			if (dt.Rows[16]["ColVisible"].ToString() == "Y" && dt.Rows[16]["ColReadOnly"].ToString() != "Y") {cShowGridHead15.Checked = base.GetBool("Y");}
			if (dt.Rows[17]["ColVisible"].ToString() == "Y" && dt.Rows[17]["ColReadOnly"].ToString() != "Y") {cGenerateSc15.Checked = base.GetBool("Y");}
			if (dt.Rows[18]["ColVisible"].ToString() == "Y" && dt.Rows[18]["ColReadOnly"].ToString() != "Y") {cGenerateSr15.Checked = base.GetBool("Y");}
			if (dt.Rows[19]["ColVisible"].ToString() == "Y" && dt.Rows[19]["ColReadOnly"].ToString() != "Y") {cValidateReq15.Checked = base.GetBool("Y");}
			if (dt.Rows[20]["ColVisible"].ToString() == "Y" && dt.Rows[20]["ColReadOnly"].ToString() != "Y") {cDeferError15.Checked = base.GetBool("N");}
			if (dt.Rows[21]["ColVisible"].ToString() == "Y" && dt.Rows[21]["ColReadOnly"].ToString() != "Y") {cAuthRequired15.Checked = base.GetBool("Y");}
			if (dt.Rows[22]["ColVisible"].ToString() == "Y" && dt.Rows[22]["ColReadOnly"].ToString() != "Y") {cGenAudit15.Checked = base.GetBool("N");}
			if (dt.Rows[23]["ColVisible"].ToString() == "Y" && dt.Rows[23]["ColReadOnly"].ToString() != "Y") {cScreenObj15.Text = string.Empty;}
			if (dt.Rows[24]["ColVisible"].ToString() == "Y" && dt.Rows[24]["ColReadOnly"].ToString() != "Y") {cScreenFilter.ImageUrl = "~/images/custom/adm/AnalToDb.gif"; }
			if (dt.Rows[25]["ColVisible"].ToString() == "Y" && dt.Rows[25]["ColReadOnly"].ToString() != "Y") {cMoreInfo.Text = "www.robocoder.com";}
			// *** Default Value (Folder) Web Rule starts here *** //
		}

		private void InitMaster(object sender, System.EventArgs e)
		{
			cScreenId15.Text = string.Empty;
			cProgramName15.Text = string.Empty;
			SetScreenTypeId15(cScreenTypeId15,string.Empty); cScreenTypeId15_SelectedIndexChanged(cScreenTypeId15, new EventArgs());
			SetViewOnly15(cViewOnly15,"N");
			cSearchAscending15.Checked = base.GetBool("Y");
			cMasterTableId15.ClearSearch();
			cSearchTableId15.ClearSearch();
			cSearchId15.ClearSearch();
			cSearchIdR15.ClearSearch();
			cSearchDtlId15.ClearSearch();
			cSearchDtlIdR15.ClearSearch();
			cSearchUrlId15.ClearSearch();
			cSearchImgId15.ClearSearch();
			cDetailTableId15.ClearSearch();
			cGridRows15.Text = "12";
			cHasDeleteAll15.Checked = base.GetBool("Y");
			cShowGridHead15.Checked = base.GetBool("Y");
			cGenerateSc15.Checked = base.GetBool("Y");
			cGenerateSr15.Checked = base.GetBool("Y");
			cValidateReq15.Checked = base.GetBool("Y");
			cDeferError15.Checked = base.GetBool("N");
			cAuthRequired15.Checked = base.GetBool("Y");
			cGenAudit15.Checked = base.GetBool("N");
			cScreenObj15.Text = string.Empty;
			cScreenFilter.Visible = false;
			cMoreInfo.Text = "www.robocoder.com"; cMoreInfo.Style.Value = "cursor:pointer;"; cMoreInfo.Attributes.Add("OnClick","SearchLink('http://www.robocoder.com','','',''); return stopEvent(this,event);");
			// *** Default Value (Folder) Web Rule starts here *** //
		}
		protected void cAdmScreen9List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmScreen9List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmScreen9List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitMaster(sender, e);      // Do this first to enable defaults for non-database columns.
			DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmScreen9ById",cAdmScreen9List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmScreen9List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cScreenId15.Text = RO.Common3.Utils.fmNumeric("0",dr["ScreenId15"].ToString(),base.LUser.Culture);} catch {cScreenId15.Text = string.Empty;}
					try {cProgramName15.Text = dr["ProgramName15"].ToString();} catch {cProgramName15.Text = string.Empty;}
					SetScreenTypeId15(cScreenTypeId15,dr["ScreenTypeId15"].ToString()); cScreenTypeId15_SelectedIndexChanged(cScreenTypeId15, new EventArgs());
					SetViewOnly15(cViewOnly15,dr["ViewOnly15"].ToString()); if (cViewOnly15.SelectedIndex <= 0 && !(cViewOnly15.Enabled && cViewOnly15.Visible)) { SetViewOnly15(cViewOnly15,"N"); }
					try {cSearchAscending15.Checked = base.GetBool(dr["SearchAscending15"].ToString());} catch {cSearchAscending15.Checked = false;}
					SetMasterTableId15(cMasterTableId15,dr["MasterTableId15"].ToString());
					cMasterTableId15Search_Script();
					SetSearchTableId15(cSearchTableId15,dr["SearchTableId15"].ToString()); cSearchTableId15_SelectedIndexChanged(cSearchTableId15, new EventArgs());
					cSearchTableId15Search_Script();
					SetSearchId15(cSearchId15,dr["SearchId15"].ToString(),cSearchTableId15.SelectedValue);
					SetSearchIdR15(cSearchIdR15,dr["SearchIdR15"].ToString(),cSearchTableId15.SelectedValue);
					SetSearchDtlId15(cSearchDtlId15,dr["SearchDtlId15"].ToString(),cSearchTableId15.SelectedValue);
					SetSearchDtlIdR15(cSearchDtlIdR15,dr["SearchDtlIdR15"].ToString(),cSearchTableId15.SelectedValue);
					SetSearchUrlId15(cSearchUrlId15,dr["SearchUrlId15"].ToString(),cSearchTableId15.SelectedValue);
					SetSearchImgId15(cSearchImgId15,dr["SearchImgId15"].ToString(),cSearchTableId15.SelectedValue);
					SetDetailTableId15(cDetailTableId15,dr["DetailTableId15"].ToString());
					try {cGridRows15.Text = RO.Common3.Utils.fmNumeric("0",dr["GridRows15"].ToString(),base.LUser.Culture);} catch {cGridRows15.Text = string.Empty;}
					try {cHasDeleteAll15.Checked = base.GetBool(dr["HasDeleteAll15"].ToString());} catch {cHasDeleteAll15.Checked = false;}
					try {cShowGridHead15.Checked = base.GetBool(dr["ShowGridHead15"].ToString());} catch {cShowGridHead15.Checked = false;}
					try {cGenerateSc15.Checked = base.GetBool(dr["GenerateSc15"].ToString());} catch {cGenerateSc15.Checked = false;}
					try {cGenerateSr15.Checked = base.GetBool(dr["GenerateSr15"].ToString());} catch {cGenerateSr15.Checked = false;}
					try {cValidateReq15.Checked = base.GetBool(dr["ValidateReq15"].ToString());} catch {cValidateReq15.Checked = false;}
					try {cDeferError15.Checked = base.GetBool(dr["DeferError15"].ToString());} catch {cDeferError15.Checked = false;}
					try {cAuthRequired15.Checked = base.GetBool(dr["AuthRequired15"].ToString());} catch {cAuthRequired15.Checked = false;}
					try {cGenAudit15.Checked = base.GetBool(dr["GenAudit15"].ToString());} catch {cGenAudit15.Checked = false;}
					try {cScreenObj15.Text = dr["ScreenObj15"].ToString();} catch {cScreenObj15.Text = string.Empty;}
					if (string.IsNullOrEmpty(dr["ScreenObj15URL"].ToString())) {cScreenObj15.Visible = false;}
					else
					{
						cScreenObj15.Visible = true;
						if (dr["ScreenObj15URL"].ToString() == string.Empty) { cScreenObj15.Attributes.Remove("onclick"); }
						else { cScreenObj15.Style.Value = "cursor:pointer;"; cScreenObj15.Attributes.Add("OnClick","SearchLink('" + dr["ScreenObj15URL"].ToString() + "','','',''); return stopEvent(this,event);"); }
					    try {cScreenObj15.Text = dr["ScreenObj15"].ToString();} catch {cScreenObj15.Text = string.Empty;}
					}
					if (string.IsNullOrEmpty(dr["ScreenFilter15URL"].ToString())) {cScreenFilter.Visible = false;}
					else
					{
						cScreenFilter.Visible = true;
						if (dr["ScreenFilter15URL"].ToString() == string.Empty) { cScreenFilter.Attributes.Remove("onclick"); }
						else { cScreenFilter.Style.Value = "cursor:pointer;"; cScreenFilter.Attributes.Add("OnClick","SearchLink('" + dr["ScreenFilter15URL"].ToString() + "','','',''); return stopEvent(this,event);"); }
					    try {cScreenFilter.ImageUrl = "~/images/custom/adm/AnalToDb.gif";} catch {cScreenFilter.ImageUrl = string.Empty;}
					}
					if (string.IsNullOrEmpty("http://www.robocoder.com")) {cMoreInfo.Visible = false;}
					else
					{
						cMoreInfo.Visible = true;
						cMoreInfo.Style.Value = "cursor:pointer;"; cMoreInfo.Attributes.Add("OnClick","SearchLink('http://www.robocoder.com','','',''); return stopEvent(this,event);");
					    try {cMoreInfo.Text = "www.robocoder.com";} catch {cMoreInfo.Text = string.Empty;}
					}
				}
			}
			cButPanel.DataBind(); if (!cSaveButton.Visible) { cInsRowButton.Visible = false; }
			DataTable dtAdmScreenGrid = null;
			int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
			try
			{
				dtAdmScreenGrid = (new AdminSystem()).GetDtlById(9,"GetAdmScreen9DtlById",cAdmScreen9List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw,filterId,base.LImpr,base.LCurr);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (!dtAdmScreenGrid.Columns.Contains("_NewRow")) { dtAdmScreenGrid.Columns.Add("_NewRow"); }
			Session[KEY_dtAdmScreenGrid] = dtAdmScreenGrid;
			cAdmScreenGrid.EditIndex = -1;
			cAdmScreenGridDataPager.PageSize = Int16.Parse(cPgSize.Text); GotoPage(0);
			if (Session[KEY_lastSortCol] != null)
			{
				cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs((string)Session[KEY_lastSortExp], SortDirection.Ascending));
			}
			else if (dtAdmScreenGrid.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dtAdmScreenGrid.Rows.Count == 1))
			{
				cAdmScreenGrid_DataBind(dtAdmScreenGrid.DefaultView);
			}
			cFindButton_Click(sender, e);
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false; Session.Remove(KEY_scrImport);
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmScreen9List.FocusID);
			ShowDirty(false); PanelTop.Update();
			// *** List Selection (End of) Web Rule starts here *** //
		}

		protected void cScreenTypeId15_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//WebRule: Hide/Show appropriate fields
            if (cScreenTypeId15.SelectedValue == "5")       // Tab-folder only.
            {
                cSearchTableId15P1.Visible = true; cSearchTableId15P2.Visible = true;
                cSearchId15P1.Visible = true; cSearchId15P2.Visible = true;
                cSearchIdR15P1.Visible = true; cSearchIdR15P2.Visible = true;
                cSearchAscending15P1.Visible = true; cSearchAscending15P2.Visible = true;
                cSearchDtlId15P1.Visible = true; cSearchDtlId15P2.Visible = true;
                cSearchDtlIdR15P1.Visible = true; cSearchDtlIdR15P2.Visible = true;
                cSearchUrlId15P1.Visible = true; cSearchUrlId15P2.Visible = true;
                cSearchImgId15P1.Visible = true; cSearchImgId15P2.Visible = true;
                SetDetailTableId15(cDetailTableId15, string.Empty); cDetailTableId15P1.Visible = false; cDetailTableId15P2.Visible = false;
                cGridRows15P1.Visible = false; cGridRows15P2.Visible = false;
                cHasDeleteAll15P1.Visible = false; cHasDeleteAll15P2.Visible = false;
                cShowGridHead15P1.Visible = false; cShowGridHead15P2.Visible = false;
            }
            else if (cScreenTypeId15.SelectedValue == "7")       // Data Grid only.
            {
                cSearchTableId15P1.Visible = false; cSearchTableId15P2.Visible = false;
                cSearchId15P1.Visible = false; cSearchId15P2.Visible = false;
                cSearchIdR15P1.Visible = false; cSearchIdR15P2.Visible = false;
                cSearchAscending15P1.Visible = false; cSearchAscending15P2.Visible = false;
                cSearchDtlId15P1.Visible = false; cSearchDtlId15P2.Visible = false;
                cSearchDtlIdR15P1.Visible = false; cSearchDtlIdR15P2.Visible = false;
                cSearchUrlId15P1.Visible = false; cSearchUrlId15P2.Visible = false;
                cSearchImgId15P1.Visible = false; cSearchImgId15P2.Visible = false;
                SetDetailTableId15(cDetailTableId15, string.Empty); cDetailTableId15P1.Visible = false; cDetailTableId15P2.Visible = false;
                cGridRows15P1.Visible = true; cGridRows15P2.Visible = true;
                cHasDeleteAll15P1.Visible = true; cHasDeleteAll15P2.Visible = true;
                cShowGridHead15P1.Visible = true; cShowGridHead15P2.Visible = true;
            }
            else
            {
                cSearchTableId15P1.Visible = true; cSearchTableId15P2.Visible = true;
                cSearchId15P1.Visible = true; cSearchId15P2.Visible = true;
                cSearchIdR15P1.Visible = true; cSearchIdR15P2.Visible = true;
                cSearchAscending15P1.Visible = true; cSearchAscending15P2.Visible = true;
                cSearchDtlId15P1.Visible = true; cSearchDtlId15P2.Visible = true;
                cSearchDtlIdR15P1.Visible = true; cSearchDtlIdR15P2.Visible = true;
                cSearchUrlId15P1.Visible = true; cSearchUrlId15P2.Visible = true;
                cSearchImgId15P1.Visible = true; cSearchImgId15P2.Visible = true;
                cDetailTableId15P1.Visible = true; cDetailTableId15P2.Visible = true;
                cGridRows15P1.Visible = true; cGridRows15P2.Visible = true;
                cHasDeleteAll15P1.Visible = true; cHasDeleteAll15P2.Visible = true;
                cShowGridHead15P1.Visible = true; cShowGridHead15P2.Visible = true;
            }
			// *** WebRule End *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cScreenTypeId15.Items.Count > 0 && Session[KEY_dtScreenTypeId15] != null)
			{
				DataView dv = ((DataTable)Session[KEY_dtScreenTypeId15]).DefaultView; dv.RowFilter = string.Empty;
				DataRowView dr = cScreenTypeId15.SelectedIndex >= 0 && cScreenTypeId15.SelectedIndex < dv.Count ? dv[cScreenTypeId15.SelectedIndex] : dv[0];
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cMasterTableId15_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cMasterTableId15_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cMasterTableId15_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cMasterTableId15.Items.Count > 0 && cMasterTableId15.DataSource != null)
			{
				DataView dv = (DataView) cMasterTableId15.DataSource; dv.RowFilter = string.Empty;
				DataRowView dr = cMasterTableId15.DataSetIndex >= 0 && cMasterTableId15.DataSetIndex < dv.Count ? dv[cMasterTableId15.DataSetIndex] : dv[0];
				cMasterTableId15Search_Script();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		protected void cSearchTableId15_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cSearchTableId15_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cSearchTableId15_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true); // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cSearchTableId15.Items.Count > 0 && cSearchTableId15.DataSource != null)
			{
				DataView dv = (DataView) cSearchTableId15.DataSource; dv.RowFilter = string.Empty;
				DataRowView dr = cSearchTableId15.DataSetIndex >= 0 && cSearchTableId15.DataSetIndex < dv.Count ? dv[cSearchTableId15.DataSetIndex] : dv[0];
				if (cSearchId15.Mode!="A") cSearchId15.ClearSearch();				else SetSearchId15(cSearchId15,cSearchId15.SelectedValue,cSearchTableId15.SelectedValue);
				if (cSearchIdR15.Mode!="A") cSearchIdR15.ClearSearch();				else SetSearchIdR15(cSearchIdR15,cSearchIdR15.SelectedValue,cSearchTableId15.SelectedValue);
				if (cSearchDtlId15.Mode!="A") cSearchDtlId15.ClearSearch();				else SetSearchDtlId15(cSearchDtlId15,cSearchDtlId15.SelectedValue,cSearchTableId15.SelectedValue);
				if (cSearchDtlIdR15.Mode!="A") cSearchDtlIdR15.ClearSearch();				else SetSearchDtlIdR15(cSearchDtlIdR15,cSearchDtlIdR15.SelectedValue,cSearchTableId15.SelectedValue);
				if (cSearchUrlId15.Mode!="A") cSearchUrlId15.ClearSearch();				else SetSearchUrlId15(cSearchUrlId15,cSearchUrlId15.SelectedValue,cSearchTableId15.SelectedValue);
				if (cSearchImgId15.Mode!="A") cSearchImgId15.ClearSearch();				else SetSearchImgId15(cSearchImgId15,cSearchImgId15.SelectedValue,cSearchTableId15.SelectedValue);
				cSearchTableId15Search_Script();
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
			}
		}

		public void cFindButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				WebControl cc = sender as WebControl;
				if ((cc != null && cc.ID.Equals("cAdmScreen9List")) || cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(cAdmScreenGrid, new CommandEventArgs("Save", "")))
				{
					string rf = string.Empty;
					if (cFind.Text != string.Empty) { rf = "(" + base.GetExpression(cFind.Text.Trim(), GetAuthCol(), 26, cFindFilter.SelectedValue) + ")"; }
					if (rf != string.Empty) { rf = "((" + rf + " or _NewRow = 'Y' ))"; }
					dv.RowFilter = rf;
					ViewState["_RowFilter"] = rf;
					GotoPage(0); cAdmScreenGrid_DataBind(dv);
					if (GetCurrPageIndex() != (int)Session[KEY_currPageIndex] && (int)Session[KEY_currPageIndex] < GetTotalPages()) { GotoPage((int)Session[KEY_currPageIndex]); cAdmScreenGrid_DataBind(dv);}
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
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (dt != null && (ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")))
			{
				DataRow dr = dt.NewRow();
				int? sortorder = null;
				dr["CultureId16"] = 1;
				dr["IncrementMsg16"] = "View {10} more";
				dr["NoMasterMsg16"] = "No master record selected";
				dr["NoDetailMsg16"] = "No detail record selected";
				dr["AddMasterMsg16"] = "Click here to enter a new master record";
				dr["AddDetailMsg16"] = "Click here to enter a new detail record";
				dr["MasterLstTitle16"] = "Master list";
				dr["MasterLstSubtitle16"] = "Create or select a master record";
				dr["MasterRecTitle16"] = "Master record";
				dr["MasterRecSubtitle16"] = "Manage your master information";
				dr["MasterFoundMsg16"] = "Master records found";
				dr["DetailLstTitle16"] = "Detail list";
				dr["DetailLstSubtitle16"] = "Add or edit a detail record";
				dr["DetailRecTitle16"] = "Detail record";
				dr["DetailRecSubtitle16"] = "Enter or update detail information";
				dr["DetailFoundMsg16"] = "Detail records found";
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
				Session[KEY_lastAddedRow] = 0; Session[KEY_dtAdmScreenGrid] = dt; Session[KEY_currPageIndex] = 0; GotoPage(0);
				cAdmScreenGrid_DataBind(dt.DefaultView);
				cAdmScreenGrid_OnItemEditing(cAdmScreenGrid, new ListViewEditEventArgs(0));
				// *** Default Value (Grid) Web Rule starts here *** //
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPgSizeButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (cAdmScreenGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				try {if (int.Parse(cPgSize.Text) < 1 || int.Parse(cPgSize.Text) > 200) {cPgSize.Text = "3";}} catch {cPgSize.Text = "3";}
				cAdmScreenGridDataPager.PageSize = int.Parse(cPgSize.Text); GotoPage(0);
				if (dt.Rows.Count <= 0 || (!((GetAuthRow().Rows[0]["AllowUpd"].ToString() == "N" || GetAuthRow().Rows[0]["ViewOnly"].ToString() == "G") && GetAuthRow().Rows[0]["AllowAdd"].ToString() == "N") && !(Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N") && dt.Rows.Count == 1)) {cAdmScreenGrid_DataBind(dt.DefaultView);} else {cFindButton_Click(sender, e);}
				try
				{
					(new AdminSystem()).UpdLastPageInfo(9, base.LUser.UsrId, cPgSize.Text, LcSysConnString, LcAppPw);
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
					Session["ImportSchema"] = (new AdminSystem()).GetSchemaScrImp(9,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "SearchLink('ImportSchema.aspx?scm=S&key=9&csy=3','','',''); return false;";}
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void ScrImportDdl(DataView dvd, DataRowView drv, bool bComboBox, string PKey, string CKey, string CNam, int MaxLen, string MatchCd, bool bAllowNulls)
		{
			if (dvd != null)
			{
				if (dvd.Table.Columns.Contains(PKey))
				{
					if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue))
					{
						dvd.RowFilter = "(" + PKey + " is null)";
					}
					else
					{
						dvd.RowFilter = "(" + PKey + " is null OR " + PKey + " = " + cAdmScreen9List.SelectedValue + ")";
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
			DataTable dtAdmScreenGrid = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (dti != null && dtAdmScreenGrid != null)
			{
				if (bClear)
				{
					dtAdmScreenGrid.Rows.Clear();
				}
				cFind.Text = string.Empty;
				int iExist = dtAdmScreenGrid.Rows.Count;
				// Validate dropdown, combobox, etc.
				int jj = 0;
				foreach (DataRowView drv in dti.DefaultView)
				{
					if ((drv.Row.RowState == System.Data.DataRowState.Added || drv.Row.RowState == System.Data.DataRowState.Detached) && jj >= iExist)
					{
						DataTable dtd;
						dtd = jj==iExist ? null : (DataTable)Session[KEY_dtCultureId16];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(9,"GetDdlCultureId3S53",true,true,0,string.Empty,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtCultureId16] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, true, "ScreenId", "CultureId16", "CultureId16Text", 50, "2", false);
					}
					jj = jj + 1;
				}
				iRow = dti.DefaultView.Count - iExist;
				if (!dti.Columns.Contains("_NewRow")) { dti.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmScreenGrid] = dti;
				cAdmScreenGrid_DataBind(dti.DefaultView);
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
				DataTable dtAdmScreenGrid = (DataTable)Session[KEY_dtAdmScreenGrid];
				try
				{
					if (cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(cAdmScreenGrid, new CommandEventArgs("Save", "")))
					{
						Session[KEY_scrImport] = ImportFile(dtAdmScreenGrid.Copy(),cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
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
						if (rows[iRow][9].ToString() == string.Empty) { rows[iRow][9] = "View {10} more";}
						if (rows[iRow][10].ToString() == string.Empty) { rows[iRow][10] = "No master record selected";}
						if (rows[iRow][11].ToString() == string.Empty) { rows[iRow][11] = "No detail record selected";}
						if (rows[iRow][12].ToString() == string.Empty) { rows[iRow][12] = "Click here to enter a new master record";}
						if (rows[iRow][13].ToString() == string.Empty) { rows[iRow][13] = "Click here to enter a new detail record";}
						if (rows[iRow][14].ToString() == string.Empty) { rows[iRow][14] = "Master list";}
						if (rows[iRow][15].ToString() == string.Empty) { rows[iRow][15] = "Create or select a master record";}
						if (rows[iRow][16].ToString() == string.Empty) { rows[iRow][16] = "Master record";}
						if (rows[iRow][17].ToString() == string.Empty) { rows[iRow][17] = "Manage your master information";}
						if (rows[iRow][18].ToString() == string.Empty) { rows[iRow][18] = "Master records found";}
						if (rows[iRow][19].ToString() == string.Empty) { rows[iRow][19] = "Detail list";}
						if (rows[iRow][20].ToString() == string.Empty) { rows[iRow][20] = "Add or edit a detail record";}
						if (rows[iRow][21].ToString() == string.Empty) { rows[iRow][21] = "Detail record";}
						if (rows[iRow][22].ToString() == string.Empty) { rows[iRow][22] = "Enter or update detail information";}
						if (rows[iRow][23].ToString() == string.Empty) { rows[iRow][23] = "Detail records found";}
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
				if (cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(cAdmScreenGrid, new CommandEventArgs("Save", "")))
				{
					try { cAdmScreenGridDataPager.SetPageProperties(cAdmScreenGridDataPager.PageSize * pageNo, cAdmScreenGridDataPager.MaximumRows, false); cAdmScreenGrid_DataBind(null); }
					catch
					{
						try { cAdmScreenGridDataPager.SetPageProperties(0, cAdmScreenGridDataPager.MaximumRows, false); cAdmScreenGrid_DataBind(null); } catch {}
					}
				}
			}
		}

		protected void cAdmScreenGrid_OnItemCommand(object sender, ListViewCommandEventArgs e)
		{
		}

		private void cAdmScreenGrid_DataBind(DataView dvAdmScreenGrid)
		{
			if (dvAdmScreenGrid == null)
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
				dvAdmScreenGrid = dt != null ? dt.DefaultView : null;
			}
			if (dvAdmScreenGrid != null)
			{
				LcAuth = GetAuthCol().AsEnumerable().ToDictionary<DataRow,string>(dr=>dr["ColName"].ToString());
				cAdmScreenGrid.DataSource = dvAdmScreenGrid;
				cAdmScreenGrid.DataBind();
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
					if (ii >= 26 && !string.IsNullOrEmpty(dr["ColumnHeader"].ToString()) && !string.IsNullOrEmpty(dr["TableId"].ToString()) && dtAuth.Rows[ii]["ColVisible"].ToString() == "Y")
					{
						li = new ListItem();
						li.Value = ii.ToString(); li.Text = dr["ColumnHeader"].ToString().Replace("*", string.Empty);
						cFindFilter.Items.Add(li);
					}
					ii = ii + 1;
				}
			}
		}

		protected void cAdmScreenGrid_OnSorting(object sender, ListViewSortEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(cAdmScreenGrid, new CommandEventArgs("Save", "")))
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
					Session[KEY_dtAdmScreenGrid] = dt;
					cAdmScreenGrid_DataBind(dv);
				}
			}
		}

		protected void cAdmScreenGrid_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
		    cAdmScreenGridDataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
		    DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
		    DataView dv = dt != null ? dt.DefaultView : null;
			cAdmScreenGrid_DataBind(dv); cAdmScreenGrid.EditIndex = -1;
		}

		private void GridChkPgDirty(ListViewItem lvi)
		{
				WebControl cc = null;
				cc = ((WebControl)lvi.FindControl("cCultureId16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cScreenTitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDefaultHlpMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cFootNote16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cAddMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cUpdMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDelMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cIncrementMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cNoMasterMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cNoDetailMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cAddMasterMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cAddDetailMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cMasterLstTitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cMasterLstSubtitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cMasterRecTitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cMasterRecSubtitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cMasterFoundMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDetailLstTitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDetailLstSubtitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDetailRecTitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDetailRecSubtitle16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cDetailFoundMsg16"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
		}

		protected void cAdmScreenGrid_OnItemDataBound(object sender, ListViewItemEventArgs e)
		{
			// *** GridItemDataBound (before) Web Rule End *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			bool isEditItem = false;
			bool isImage = true;
			bool hasImageContent = false;
			DataView dvAdmScreenGrid = dt != null ? dt.DefaultView : null;
			if (cAdmScreenGrid.EditIndex > -1 && GetDataItemIndex(cAdmScreenGrid.EditIndex) == e.Item.DataItemIndex)
			{
				isEditItem = true;
				base.SetGridEnabled(e.Item, GetAuthCol(), GetLabel(), 26);
				if (dvAdmScreenGrid[e.Item.DataItemIndex]["CultureId16"].ToString() != string.Empty) {SetCultureId16((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cCultureId16"),dvAdmScreenGrid[e.Item.DataItemIndex]["CultureId16"].ToString(), e.Item);} else {SetCultureId16((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cCultureId16"),"1", e.Item);}
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
					cc = e.Item.FindControl("cAdmScreenGridDelete"); if (cc != null) { cc.Visible = false; }
					cc = e.Item.FindControl("cAdmScreenGridEdit"); if (cc != null) { cc.Visible = false; }
				}
				else
				{
			        HtmlTableRow tr = e.Item.FindControl("cAdmScreenGridRow") as HtmlTableRow;
			        LinkButton lb = e.Item.FindControl("cAdmScreenGridEdit") as LinkButton;
			        if (tr != null && lb != null) { SetDefaultCtrl(tr, lb, string.Empty); }
				}
			}
			if (cAdmScreenGrid.EditIndex > -1 && GetDataItemIndex(cAdmScreenGrid.EditIndex) == e.Item.DataItemIndex)
			{
			}
			else
			{
			}
			// *** GridItemDataBound (after) Web Rule End *** //
		}

		protected void cScreenHlpId16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "0") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "0"; Session[KEY_lastSortExp] = "ScreenHlpId16";Session[KEY_lastSortImg] = "cScreenHlpId16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("ScreenHlpId16", SortDirection.Ascending));
		}

		protected void cCultureId16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "1") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "1"; Session[KEY_lastSortExp] = "CultureId16Text";Session[KEY_lastSortImg] = "cCultureId16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("CultureId16Text", SortDirection.Ascending));
		}

		protected void cScreenTitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "2") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "2"; Session[KEY_lastSortExp] = "ScreenTitle16";Session[KEY_lastSortImg] = "cScreenTitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("ScreenTitle16", SortDirection.Ascending));
		}

		protected void cDefaultHlpMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "3") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "3"; Session[KEY_lastSortExp] = "DefaultHlpMsg16";Session[KEY_lastSortImg] = "cDefaultHlpMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DefaultHlpMsg16", SortDirection.Ascending));
		}

		protected void cFootNote16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "4") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "4"; Session[KEY_lastSortExp] = "FootNote16";Session[KEY_lastSortImg] = "cFootNote16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("FootNote16", SortDirection.Ascending));
		}

		protected void cAddMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "5") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "5"; Session[KEY_lastSortExp] = "AddMsg16";Session[KEY_lastSortImg] = "cAddMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("AddMsg16", SortDirection.Ascending));
		}

		protected void cUpdMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "6") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "6"; Session[KEY_lastSortExp] = "UpdMsg16";Session[KEY_lastSortImg] = "cUpdMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("UpdMsg16", SortDirection.Ascending));
		}

		protected void cDelMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "7") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "7"; Session[KEY_lastSortExp] = "DelMsg16";Session[KEY_lastSortImg] = "cDelMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DelMsg16", SortDirection.Ascending));
		}

		protected void cIncrementMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "8") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "8"; Session[KEY_lastSortExp] = "IncrementMsg16";Session[KEY_lastSortImg] = "cIncrementMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("IncrementMsg16", SortDirection.Ascending));
		}

		protected void cNoMasterMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "9") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "9"; Session[KEY_lastSortExp] = "NoMasterMsg16";Session[KEY_lastSortImg] = "cNoMasterMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("NoMasterMsg16", SortDirection.Ascending));
		}

		protected void cNoDetailMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "10") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "10"; Session[KEY_lastSortExp] = "NoDetailMsg16";Session[KEY_lastSortImg] = "cNoDetailMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("NoDetailMsg16", SortDirection.Ascending));
		}

		protected void cAddMasterMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "11") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "11"; Session[KEY_lastSortExp] = "AddMasterMsg16";Session[KEY_lastSortImg] = "cAddMasterMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("AddMasterMsg16", SortDirection.Ascending));
		}

		protected void cAddDetailMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "12") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "12"; Session[KEY_lastSortExp] = "AddDetailMsg16";Session[KEY_lastSortImg] = "cAddDetailMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("AddDetailMsg16", SortDirection.Ascending));
		}

		protected void cMasterLstTitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "13") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "13"; Session[KEY_lastSortExp] = "MasterLstTitle16";Session[KEY_lastSortImg] = "cMasterLstTitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("MasterLstTitle16", SortDirection.Ascending));
		}

		protected void cMasterLstSubtitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "14") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "14"; Session[KEY_lastSortExp] = "MasterLstSubtitle16";Session[KEY_lastSortImg] = "cMasterLstSubtitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("MasterLstSubtitle16", SortDirection.Ascending));
		}

		protected void cMasterRecTitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "15") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "15"; Session[KEY_lastSortExp] = "MasterRecTitle16";Session[KEY_lastSortImg] = "cMasterRecTitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("MasterRecTitle16", SortDirection.Ascending));
		}

		protected void cMasterRecSubtitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "16") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "16"; Session[KEY_lastSortExp] = "MasterRecSubtitle16";Session[KEY_lastSortImg] = "cMasterRecSubtitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("MasterRecSubtitle16", SortDirection.Ascending));
		}

		protected void cMasterFoundMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "17") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "17"; Session[KEY_lastSortExp] = "MasterFoundMsg16";Session[KEY_lastSortImg] = "cMasterFoundMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("MasterFoundMsg16", SortDirection.Ascending));
		}

		protected void cDetailLstTitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "18") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "18"; Session[KEY_lastSortExp] = "DetailLstTitle16";Session[KEY_lastSortImg] = "cDetailLstTitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DetailLstTitle16", SortDirection.Ascending));
		}

		protected void cDetailLstSubtitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "19") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "19"; Session[KEY_lastSortExp] = "DetailLstSubtitle16";Session[KEY_lastSortImg] = "cDetailLstSubtitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DetailLstSubtitle16", SortDirection.Ascending));
		}

		protected void cDetailRecTitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "20") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "20"; Session[KEY_lastSortExp] = "DetailRecTitle16";Session[KEY_lastSortImg] = "cDetailRecTitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DetailRecTitle16", SortDirection.Ascending));
		}

		protected void cDetailRecSubtitle16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "21") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "21"; Session[KEY_lastSortExp] = "DetailRecSubtitle16";Session[KEY_lastSortImg] = "cDetailRecSubtitle16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DetailRecSubtitle16", SortDirection.Ascending));
		}

		protected void cDetailFoundMsg16hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "22") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "22"; Session[KEY_lastSortExp] = "DetailFoundMsg16";Session[KEY_lastSortImg] = "cDetailFoundMsg16hi";
			cAdmScreenGrid_OnSorting(sender, new ListViewSortEventArgs("DetailFoundMsg16", SortDirection.Ascending));
		}

		protected void cAdmScreenGrid_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if ((ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")) && dt != null && dt.DefaultView.Count > GetDataItemIndex(e.NewEditIndex))
			{
				cAdmScreenGrid.EditIndex = e.NewEditIndex;
				cAdmScreenGrid_DataBind(null);
				Control wc = null;
				string idx = Page.Request["__EVENTARGUMENT"];
				if (!string.IsNullOrEmpty(idx))
				{
				    Control ctrlToFocus = cAdmScreenGrid.EditItem.FindControl("c" + idx);
				    if (((WebControl)ctrlToFocus).Enabled) { wc = ctrlToFocus; }
				}
				if (wc == null) { wc = FindEditableControl(cAdmScreenGrid.EditItem); }
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
             MkMobileLabel(cAdmScreenGrid.EditItem);
		    }
		    else { e.Cancel = true; }
		}

		private void MkMobileLabel(ListViewItem lvi)
		{
		    Label ml = null;
		    ml = lvi.FindControl("cScreenHlpId16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(26); }
		    ml = lvi.FindControl("cCultureId16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(27); }
		    ml = lvi.FindControl("cScreenTitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(28); }
		    ml = lvi.FindControl("cDefaultHlpMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(29); }
		    ml = lvi.FindControl("cFootNote16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(30); }
		    ml = lvi.FindControl("cAddMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(31); }
		    ml = lvi.FindControl("cUpdMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(32); }
		    ml = lvi.FindControl("cDelMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(33); }
		    ml = lvi.FindControl("cIncrementMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(34); }
		    ml = lvi.FindControl("cNoMasterMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(35); }
		    ml = lvi.FindControl("cNoDetailMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(36); }
		    ml = lvi.FindControl("cAddMasterMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(37); }
		    ml = lvi.FindControl("cAddDetailMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(38); }
		    ml = lvi.FindControl("cMasterLstTitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(39); }
		    ml = lvi.FindControl("cMasterLstSubtitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(40); }
		    ml = lvi.FindControl("cMasterRecTitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(41); }
		    ml = lvi.FindControl("cMasterRecSubtitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(42); }
		    ml = lvi.FindControl("cMasterFoundMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(43); }
		    ml = lvi.FindControl("cDetailLstTitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(44); }
		    ml = lvi.FindControl("cDetailLstSubtitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(45); }
		    ml = lvi.FindControl("cDetailRecTitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(46); }
		    ml = lvi.FindControl("cDetailRecSubtitle16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(47); }
		    ml = lvi.FindControl("cDetailFoundMsg16ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(48); }
		}

		protected void GridFill(ListViewItem lvi, DataTable dt, DataRow dr, bool bInsert)
		{
			RoboCoder.WebControls.ComboBox cbb = null;
			cbb = (RoboCoder.WebControls.ComboBox)lvi.FindControl("cCultureId16");
			if (cbb != null)
			{
				if (cbb.SelectedValue != string.Empty)
				{
					dr["CultureId16"] = cbb.SelectedValue;
					dr["CultureId16Text"] = cbb.SelectedText;
				}
				else
				{
					dr["CultureId16"] = System.DBNull.Value;
					dr["CultureId16Text"] = System.DBNull.Value;
				}
			}
			TextBox tb = null;
			tb = (TextBox)lvi.FindControl("cScreenTitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ScreenTitle16"] = tb.Text;} else {dr["ScreenTitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDefaultHlpMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DefaultHlpMsg16"] = tb.Text;} else {dr["DefaultHlpMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cFootNote16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["FootNote16"] = tb.Text;} else {dr["FootNote16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cAddMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["AddMsg16"] = tb.Text;} else {dr["AddMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cUpdMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["UpdMsg16"] = tb.Text;} else {dr["UpdMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDelMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DelMsg16"] = tb.Text;} else {dr["DelMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cIncrementMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["IncrementMsg16"] = tb.Text;} else {dr["IncrementMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cNoMasterMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["NoMasterMsg16"] = tb.Text;} else {dr["NoMasterMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cNoDetailMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["NoDetailMsg16"] = tb.Text;} else {dr["NoDetailMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cAddMasterMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["AddMasterMsg16"] = tb.Text;} else {dr["AddMasterMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cAddDetailMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["AddDetailMsg16"] = tb.Text;} else {dr["AddDetailMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cMasterLstTitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["MasterLstTitle16"] = tb.Text;} else {dr["MasterLstTitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cMasterLstSubtitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["MasterLstSubtitle16"] = tb.Text;} else {dr["MasterLstSubtitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cMasterRecTitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["MasterRecTitle16"] = tb.Text;} else {dr["MasterRecTitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cMasterRecSubtitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["MasterRecSubtitle16"] = tb.Text;} else {dr["MasterRecSubtitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cMasterFoundMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["MasterFoundMsg16"] = tb.Text;} else {dr["MasterFoundMsg16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDetailLstTitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DetailLstTitle16"] = tb.Text;} else {dr["DetailLstTitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDetailLstSubtitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DetailLstSubtitle16"] = tb.Text;} else {dr["DetailLstSubtitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDetailRecTitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DetailRecTitle16"] = tb.Text;} else {dr["DetailRecTitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDetailRecSubtitle16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DetailRecSubtitle16"] = tb.Text;} else {dr["DetailRecSubtitle16"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cDetailFoundMsg16");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["DetailFoundMsg16"] = tb.Text;} else {dr["DetailFoundMsg16"] = System.DBNull.Value;}
			}
		    if (bInsert) { dt.Rows.InsertAt(dr, 0); }
			Session[KEY_dtAdmScreenGrid] = dt;
			cAdmScreenGrid.EditIndex = -1;
			cAdmScreenGrid_DataBind(dt.DefaultView);
		}

		protected void cAdmScreenGrid_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			if (dt != null && dt.Rows.Count > 0)
			{
				DataRow dr = dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row;
			    GridFill(cAdmScreenGrid.EditItem, dt, dr, false);
			    dr["_NewRow"] = "N";
			    if (dt.Columns.Contains("_SortOrder")) dr["_SortOrder"] = null;
			}
		}

		protected void cAdmScreenGrid_OnItemCanceling(object sender, ListViewCancelEventArgs e)
		{
			PanelTop.Update();
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && dv[GetDataItemIndex(e.ItemIndex)]["_NewRow"].ToString() == "Y")
			{
				cAdmScreenGrid_OnItemDeleting(sender, new ListViewDeleteEventArgs(e.ItemIndex));
			}
			if (dt.Columns.Contains("_SortOrder")) dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row["_SortOrder"] = null;
			cAdmScreenGrid.EditIndex = -1; cAdmScreenGrid_DataBind(null);
		}

		protected void cDeleteAllButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && (cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				GotoPage(0);
				while (dv.Count > 0) {dv.Delete(0);}
				Session[KEY_dtAdmScreenGrid] = dt;
				cAdmScreenGrid.EditIndex = -1; cAdmScreenGrid_DataBind(dv);
			}
		}

		protected void cAdmScreenGrid_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			DataRow dr = dv != null ? dv[GetDataItemIndex(e.ItemIndex)].Row : null;
			if (dv != null && (cAdmScreenGrid.EditIndex == e.ItemIndex || UpdateGridRow(sender, new CommandEventArgs("Save",""))))
			{
				// *** Delete Grid Row (before) Web Rule End *** //
				dr.Delete();
				Session[KEY_dtAdmScreenGrid] = dt;
				cAdmScreenGrid.EditIndex = -1; cAdmScreenGrid_DataBind(dv);
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
			cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
			PopAdmScreen9List(sender, e, false, null);
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
			cScreenId15.Text = string.Empty;
			cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
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
			if (cScreenId15.Text == string.Empty) { cClearButton_Click(sender, new EventArgs()); ShowDirty(false); PanelTop.Update();}
			else { PopAdmScreen9List(sender, e, false, cScreenId15.Text); }
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
			Session.Remove(KEY_dtAdmScreen9List); PopAdmScreen9List(sender, e, false, null);
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
			//WebRule: Signal create program needed upon program name change and validate program name
            if (base.SystemsList.Rows.Find(cSystemId.SelectedValue)["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View" && !cProgramName15.Text.StartsWith("u_"))
            {
                cProgramName15.Text = "u_" + cProgramName15.Text;
            }
            bool bRegenNeeded = false;
            if (cAdmScreen9List.SelectedValue != string.Empty && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded(cProgramName15.Text, Int32.Parse(cAdmScreen9List.SelectedValue), 0, 0, (string)Session[KEY_sysConnectionString], LcAppPw)) { bRegenNeeded = true; }
			// *** WebRule End *** //
			string pid = string.Empty;
			if (ValidPage() && (cAdmScreenGrid.EditIndex < 0 || UpdateGridRow(cAdmScreenGrid, new CommandEventArgs("Save", ""))))
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmScreenGrid];
				DataView dv = dt != null ? dt.DefaultView : null;
				string ftr = string.Empty;
				if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
				AdmScreen9 ds = PrepAdmScreenData(dv,cScreenId15.Text == string.Empty);
				if (ftr != string.Empty) {dv.RowFilter = ftr;}
				if (string.IsNullOrEmpty(cAdmScreen9List.SelectedValue))	// Add
				{
					if (ds != null)
					{
						pid = (new AdminSystem()).AddData(9,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc);
					}
					if (!string.IsNullOrEmpty(pid))
					{
						cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmScreen9List(sender, e, false, pid);
						rtn = GetScreenHlp().Rows[0]["AddMsg"].ToString();
					}
				}
				else {
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(9,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc)) {bValid7 = true;}
					if (bValid7)
					{
						cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
						Session[KEY_currPageIndex] = GetCurrPageIndex();
						ShowDirty(false); PopAdmScreen9List(sender, e, false, ds.Tables["AdmScreen"].Rows[0]["ScreenId15"]);
						rtn = GetScreenHlp().Rows[0]["UpdMsg"].ToString();
					}
				}
			}
			//WebRule: Create program upon program name change or add screen
if (bRegenNeeded || pid != string.Empty)		// ProgramName changed or a new screen has been added successfully.
			{
				try
				{
					if (pid == string.Empty) { pid = cScreenId15.Text; }
					DataTable dtTle = (new AdminSystem()).GetScreenHlp(int.Parse(pid), base.LUser.CultureId, (string)Session[KEY_sysConnectionString], LcAppPw);
					DataRow row = base.SystemsList.Rows[cSystemId.SelectedIndex];
					base.CSrc = new CurrSrc(true, row); base.CTar = new CurrTar(true, row);
					(new GenScreensSystem()).CreateProgram(int.Parse(pid), dtTle.Rows[0]["ScreenTitle"].ToString(), row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					(new MenuSystem()).NewMenuItem(int.Parse(pid), 0, 0, dtTle.Rows[0]["ScreenTitle"].ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
					//Response.Redirect(Request.RawUrl);
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
			if (cScreenId15.Text != string.Empty)
			{
				AdmScreen9 ds = PrepAdmScreenData(null,false);
				try
				{
					if (ds != null && (new AdminSystem()).DelData(9,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw,base.CPrj,base.CSrc))
					{
						cAdmScreen9List.ClearSearch(); Session.Remove(KEY_dtAdmScreen9List);
						ShowDirty(false); PopAdmScreen9List(sender, e, false, null);
						if (Config.PromptMsg) {bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString());}
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private AdmScreen9 PrepAdmScreenData(DataView dv, bool bAdd)
		{
			AdmScreen9 ds = new AdmScreen9();
			DataRow dr = ds.Tables["AdmScreen"].NewRow();
			DataRow drType = ds.Tables["AdmScreen"].NewRow();
			DataRow drDisp = ds.Tables["AdmScreen"].NewRow();
			if (bAdd) { dr["ScreenId15"] = string.Empty; } else { dr["ScreenId15"] = cScreenId15.Text; }
			drType["ScreenId15"] = "Numeric"; drDisp["ScreenId15"] = "TextBox";
			try {dr["ProgramName15"] = cProgramName15.Text.Trim();} catch {}
			drType["ProgramName15"] = "VarChar"; drDisp["ProgramName15"] = "TextBox";
			try {dr["ScreenTypeId15"] = cScreenTypeId15.SelectedValue;} catch {}
			drType["ScreenTypeId15"] = "Numeric"; drDisp["ScreenTypeId15"] = "DropDownList";
			try {dr["ViewOnly15"] = cViewOnly15.SelectedValue;} catch {}
			drType["ViewOnly15"] = "Char"; drDisp["ViewOnly15"] = "DropDownList";
			try {dr["SearchAscending15"] = base.SetBool(cSearchAscending15.Checked);} catch {}
			drType["SearchAscending15"] = "Char"; drDisp["SearchAscending15"] = "CheckBox";
			try {dr["MasterTableId15"] = cMasterTableId15.SelectedValue;} catch {}
			drType["MasterTableId15"] = "Numeric"; drDisp["MasterTableId15"] = "AutoComplete";
			try {dr["SearchTableId15"] = cSearchTableId15.SelectedValue;} catch {}
			drType["SearchTableId15"] = "Numeric"; drDisp["SearchTableId15"] = "AutoComplete";
			try {dr["SearchId15"] = cSearchId15.SelectedValue;} catch {}
			drType["SearchId15"] = "Numeric"; drDisp["SearchId15"] = "AutoComplete";
			try {dr["SearchIdR15"] = cSearchIdR15.SelectedValue;} catch {}
			drType["SearchIdR15"] = "Numeric"; drDisp["SearchIdR15"] = "AutoComplete";
			try {dr["SearchDtlId15"] = cSearchDtlId15.SelectedValue;} catch {}
			drType["SearchDtlId15"] = "Numeric"; drDisp["SearchDtlId15"] = "AutoComplete";
			try {dr["SearchDtlIdR15"] = cSearchDtlIdR15.SelectedValue;} catch {}
			drType["SearchDtlIdR15"] = "Numeric"; drDisp["SearchDtlIdR15"] = "AutoComplete";
			try {dr["SearchUrlId15"] = cSearchUrlId15.SelectedValue;} catch {}
			drType["SearchUrlId15"] = "Numeric"; drDisp["SearchUrlId15"] = "AutoComplete";
			try {dr["SearchImgId15"] = cSearchImgId15.SelectedValue;} catch {}
			drType["SearchImgId15"] = "Numeric"; drDisp["SearchImgId15"] = "AutoComplete";
			try {dr["DetailTableId15"] = cDetailTableId15.SelectedValue;} catch {}
			drType["DetailTableId15"] = "Numeric"; drDisp["DetailTableId15"] = "AutoComplete";
			try {dr["GridRows15"] = cGridRows15.Text.Trim();} catch {}
			drType["GridRows15"] = "Numeric"; drDisp["GridRows15"] = "TextBox";
			try {dr["HasDeleteAll15"] = base.SetBool(cHasDeleteAll15.Checked);} catch {}
			drType["HasDeleteAll15"] = "Char"; drDisp["HasDeleteAll15"] = "CheckBox";
			try {dr["ShowGridHead15"] = base.SetBool(cShowGridHead15.Checked);} catch {}
			drType["ShowGridHead15"] = "Char"; drDisp["ShowGridHead15"] = "CheckBox";
			try {dr["GenerateSc15"] = base.SetBool(cGenerateSc15.Checked);} catch {}
			drType["GenerateSc15"] = "Char"; drDisp["GenerateSc15"] = "CheckBox";
			try {dr["GenerateSr15"] = base.SetBool(cGenerateSr15.Checked);} catch {}
			drType["GenerateSr15"] = "Char"; drDisp["GenerateSr15"] = "CheckBox";
			try {dr["ValidateReq15"] = base.SetBool(cValidateReq15.Checked);} catch {}
			drType["ValidateReq15"] = "Char"; drDisp["ValidateReq15"] = "CheckBox";
			try {dr["DeferError15"] = base.SetBool(cDeferError15.Checked);} catch {}
			drType["DeferError15"] = "Char"; drDisp["DeferError15"] = "CheckBox";
			try {dr["AuthRequired15"] = base.SetBool(cAuthRequired15.Checked);} catch {}
			drType["AuthRequired15"] = "Char"; drDisp["AuthRequired15"] = "CheckBox";
			try {dr["GenAudit15"] = base.SetBool(cGenAudit15.Checked);} catch {}
			drType["GenAudit15"] = "Char"; drDisp["GenAudit15"] = "CheckBox";
			try {dr["ScreenObj15"] = cScreenObj15.Text;} catch {}
			drType["ScreenObj15"] = "VarChar"; drDisp["ScreenObj15"] = "HyperPopUp";
			try {dr["ScreenFilter"] = cScreenFilter.Text;} catch {}
			drType["ScreenFilter"] = string.Empty; drDisp["ScreenFilter"] = "ImagePopUp";
			try {dr["MoreInfo"] = cMoreInfo.Text;} catch {}
			drType["MoreInfo"] = string.Empty; drDisp["MoreInfo"] = "HyperPopUp";
			if (dv != null)
			{
				ds.Tables["AdmScreenDef"].Rows.Add(MakeTypRow(ds.Tables["AdmScreenDef"].NewRow()));
				ds.Tables["AdmScreenDef"].Rows.Add(MakeDisRow(ds.Tables["AdmScreenDef"].NewRow()));
				if (bAdd)
				{
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmScreenAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenAdd"].NewRow(), drv, true));
					}
				}
				else
				{
					dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmScreenUpd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenUpd"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.Added;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmScreenAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenAdd"].NewRow(), drv, true));
					}
					dv.RowStateFilter = DataViewRowState.Deleted;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmScreenDel"].Rows.Add(MakeColRow(ds.Tables["AdmScreenDel"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.CurrentRows;
				}
			}
			ds.Tables["AdmScreen"].Rows.Add(dr); ds.Tables["AdmScreen"].Rows.Add(drType); ds.Tables["AdmScreen"].Rows.Add(drDisp);
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), cAdmScreen9List.SelectedValue.ToString()); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtScreenTypeId15];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ScreenTypeId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtViewOnly15];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ViewOnly', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtMasterTableId15];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'MasterTableId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtCultureId16];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'CultureId', please investigate."); return false;
			}
			return true;
		}

		private DataRow MakeTypRow(DataRow dr)
		{
			dr["ScreenId15"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ScreenHlpId16"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["CultureId16"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ScreenTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DefaultHlpMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["FootNote16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["AddMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["UpdMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DelMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["IncrementMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["NoMasterMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["NoDetailMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["AddMasterMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["AddDetailMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["MasterLstTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["MasterLstSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["MasterRecTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["MasterRecSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["MasterFoundMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DetailLstTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DetailLstSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DetailRecTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DetailRecSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			dr["DetailFoundMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
			return dr;
		}

		private DataRow MakeDisRow(DataRow dr)
		{
			dr["ScreenId15"] = "TextBox";
			dr["ScreenHlpId16"] = "TextBox";
			dr["CultureId16"] = "AutoComplete";
			dr["ScreenTitle16"] = "TextBox";
			dr["DefaultHlpMsg16"] = "TextBox";
			dr["FootNote16"] = "TextBox";
			dr["AddMsg16"] = "TextBox";
			dr["UpdMsg16"] = "TextBox";
			dr["DelMsg16"] = "TextBox";
			dr["IncrementMsg16"] = "TextBox";
			dr["NoMasterMsg16"] = "TextBox";
			dr["NoDetailMsg16"] = "TextBox";
			dr["AddMasterMsg16"] = "TextBox";
			dr["AddDetailMsg16"] = "TextBox";
			dr["MasterLstTitle16"] = "TextBox";
			dr["MasterLstSubtitle16"] = "TextBox";
			dr["MasterRecTitle16"] = "TextBox";
			dr["MasterRecSubtitle16"] = "TextBox";
			dr["MasterFoundMsg16"] = "TextBox";
			dr["DetailLstTitle16"] = "TextBox";
			dr["DetailLstSubtitle16"] = "TextBox";
			dr["DetailRecTitle16"] = "TextBox";
			dr["DetailRecSubtitle16"] = "TextBox";
			dr["DetailFoundMsg16"] = "TextBox";
			return dr;
		}

		private DataRow MakeColRow(DataRow dr, DataRowView drv, bool bAdd)
		{
			dr["ScreenId15"] = cScreenId15.Text;
			DataTable dtAuth = GetAuthCol();
			if (dtAuth != null)
			{
				dr["ScreenHlpId16"] = drv["ScreenHlpId16"].ToString().Trim();
				dr["CultureId16"] = drv["CultureId16"];
				dr["ScreenTitle16"] = drv["ScreenTitle16"].ToString().Trim();
				dr["DefaultHlpMsg16"] = drv["DefaultHlpMsg16"].ToString().Trim();
				dr["FootNote16"] = drv["FootNote16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[30]["ColReadOnly"].ToString() == "Y" && dr["FootNote16"].ToString() == string.Empty) {dr["FootNote16"] = System.DBNull.Value;}
				dr["AddMsg16"] = drv["AddMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[31]["ColReadOnly"].ToString() == "Y" && dr["AddMsg16"].ToString() == string.Empty) {dr["AddMsg16"] = System.DBNull.Value;}
				dr["UpdMsg16"] = drv["UpdMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[32]["ColReadOnly"].ToString() == "Y" && dr["UpdMsg16"].ToString() == string.Empty) {dr["UpdMsg16"] = System.DBNull.Value;}
				dr["DelMsg16"] = drv["DelMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[33]["ColReadOnly"].ToString() == "Y" && dr["DelMsg16"].ToString() == string.Empty) {dr["DelMsg16"] = System.DBNull.Value;}
				dr["IncrementMsg16"] = drv["IncrementMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[34]["ColReadOnly"].ToString() == "Y" && dr["IncrementMsg16"].ToString() == string.Empty) {dr["IncrementMsg16"] = System.DBNull.Value;}
				dr["NoMasterMsg16"] = drv["NoMasterMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[35]["ColReadOnly"].ToString() == "Y" && dr["NoMasterMsg16"].ToString() == string.Empty) {dr["NoMasterMsg16"] = System.DBNull.Value;}
				dr["NoDetailMsg16"] = drv["NoDetailMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[36]["ColReadOnly"].ToString() == "Y" && dr["NoDetailMsg16"].ToString() == string.Empty) {dr["NoDetailMsg16"] = System.DBNull.Value;}
				dr["AddMasterMsg16"] = drv["AddMasterMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[37]["ColReadOnly"].ToString() == "Y" && dr["AddMasterMsg16"].ToString() == string.Empty) {dr["AddMasterMsg16"] = System.DBNull.Value;}
				dr["AddDetailMsg16"] = drv["AddDetailMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[38]["ColReadOnly"].ToString() == "Y" && dr["AddDetailMsg16"].ToString() == string.Empty) {dr["AddDetailMsg16"] = System.DBNull.Value;}
				dr["MasterLstTitle16"] = drv["MasterLstTitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[39]["ColReadOnly"].ToString() == "Y" && dr["MasterLstTitle16"].ToString() == string.Empty) {dr["MasterLstTitle16"] = System.DBNull.Value;}
				dr["MasterLstSubtitle16"] = drv["MasterLstSubtitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[40]["ColReadOnly"].ToString() == "Y" && dr["MasterLstSubtitle16"].ToString() == string.Empty) {dr["MasterLstSubtitle16"] = System.DBNull.Value;}
				dr["MasterRecTitle16"] = drv["MasterRecTitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[41]["ColReadOnly"].ToString() == "Y" && dr["MasterRecTitle16"].ToString() == string.Empty) {dr["MasterRecTitle16"] = System.DBNull.Value;}
				dr["MasterRecSubtitle16"] = drv["MasterRecSubtitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[42]["ColReadOnly"].ToString() == "Y" && dr["MasterRecSubtitle16"].ToString() == string.Empty) {dr["MasterRecSubtitle16"] = System.DBNull.Value;}
				dr["MasterFoundMsg16"] = drv["MasterFoundMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[43]["ColReadOnly"].ToString() == "Y" && dr["MasterFoundMsg16"].ToString() == string.Empty) {dr["MasterFoundMsg16"] = System.DBNull.Value;}
				dr["DetailLstTitle16"] = drv["DetailLstTitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[44]["ColReadOnly"].ToString() == "Y" && dr["DetailLstTitle16"].ToString() == string.Empty) {dr["DetailLstTitle16"] = System.DBNull.Value;}
				dr["DetailLstSubtitle16"] = drv["DetailLstSubtitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[45]["ColReadOnly"].ToString() == "Y" && dr["DetailLstSubtitle16"].ToString() == string.Empty) {dr["DetailLstSubtitle16"] = System.DBNull.Value;}
				dr["DetailRecTitle16"] = drv["DetailRecTitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[46]["ColReadOnly"].ToString() == "Y" && dr["DetailRecTitle16"].ToString() == string.Empty) {dr["DetailRecTitle16"] = System.DBNull.Value;}
				dr["DetailRecSubtitle16"] = drv["DetailRecSubtitle16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[47]["ColReadOnly"].ToString() == "Y" && dr["DetailRecSubtitle16"].ToString() == string.Empty) {dr["DetailRecSubtitle16"] = System.DBNull.Value;}
				dr["DetailFoundMsg16"] = drv["DetailFoundMsg16"].ToString().Trim();
				if (bAdd && dtAuth.Rows[48]["ColReadOnly"].ToString() == "Y" && dr["DetailFoundMsg16"].ToString() == string.Empty) {dr["DetailFoundMsg16"] = System.DBNull.Value;}
			}
			return dr;
		}

		private bool UpdateGridRow(object sender, CommandEventArgs e)
		{
			if (cAdmScreenGrid.EditIndex > -1)
			{
				TextBox pwd = null;				cAdmScreenGrid_OnItemUpdating(sender, new ListViewUpdateEventArgs(cAdmScreenGrid.EditIndex));
			}
			return true;
		}

		protected void cAdmScreenGrid_OnPreRender(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Image hi = null;
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cScreenHlpId16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cCultureId16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cScreenTitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDefaultHlpMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cFootNote16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cAddMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cUpdMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDelMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cIncrementMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cNoMasterMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cNoDetailMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cAddMasterMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cAddDetailMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cMasterLstTitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cMasterLstSubtitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cMasterRecTitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cMasterRecSubtitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cMasterFoundMsg16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDetailLstTitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDetailLstSubtitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDetailRecTitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDetailRecSubtitle16hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl("cDetailFoundMsg16hi"); if (hi != null) { hi.Visible = false; }
			if (Session[KEY_lastSortImg] != null)
			{
				hi = (System.Web.UI.WebControls.Image)cAdmScreenGrid.FindControl((string)Session[KEY_lastSortImg]);
				if (hi != null) { hi.ImageUrl = Utils.AddTilde((string)Session[KEY_lastSortUrl]); hi.Visible = true; }
			}
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cScreenHlpId16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cCultureId16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cScreenTitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDefaultHlpMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cFootNote16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cAddMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cUpdMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDelMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cIncrementMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cNoMasterMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cNoDetailMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cAddMasterMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cAddDetailMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cMasterLstTitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cMasterLstSubtitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cMasterRecTitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cMasterRecSubtitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cMasterFoundMsg16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDetailLstTitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDetailLstSubtitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDetailRecTitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDetailRecSubtitle16hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmScreenGrid.FindControl("cDetailFoundMsg16hl"));
		}

		private string GetButtonId(ListViewItem lvi)
		{
			string ButtonID = String.Empty;
			Control c = lvi.FindControl("cAdmScreenGridEdit");
			if (c != null) { ButtonID = c.UniqueID; }
			return ButtonID;
		}

		private void SetDefaultCtrl(HtmlTableRow tr, LinkButton lb, string ctrlId)
		{
			tr.Attributes.Add("onclick", "document.getElementById('" + bConfirm.ClientID + "').value='N'; fFocusedEdit('" + lb.UniqueID + "','" + ctrlId + "',event);");
		}

		private int GetCurrPageIndex()
		{
		    return cAdmScreenGridDataPager.StartRowIndex / cAdmScreenGridDataPager.PageSize;
		}

		private int GetTotalPages()
		{
		    return (int)Math.Ceiling((double)cAdmScreenGridDataPager.TotalRowCount / cAdmScreenGridDataPager.PageSize);
		}

		private int GetDataItemIndex(int editIndex)
		{
		    return cAdmScreenGridDataPager.StartRowIndex + editIndex;
		}

		protected void cAdmScreenGrid_OnLayoutCreated(object sender, EventArgs e)
		{
		    // Header:
		    LinkButton lb = null;
		    lb = cAdmScreenGrid.FindControl("cScreenHlpId16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(26); lb.ToolTip = ColumnToolTip(26); lb.Parent.Visible = GridColumnVisible(26); }
		    lb = cAdmScreenGrid.FindControl("cCultureId16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(27); lb.ToolTip = ColumnToolTip(27); lb.Parent.Visible = GridColumnVisible(27); }
		    lb = cAdmScreenGrid.FindControl("cScreenTitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(28); lb.ToolTip = ColumnToolTip(28); lb.Parent.Visible = GridColumnVisible(28); }
		    lb = cAdmScreenGrid.FindControl("cDefaultHlpMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(29); lb.ToolTip = ColumnToolTip(29); lb.Parent.Visible = GridColumnVisible(29); }
		    lb = cAdmScreenGrid.FindControl("cFootNote16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(30); lb.ToolTip = ColumnToolTip(30); lb.Parent.Visible = GridColumnVisible(30); }
		    lb = cAdmScreenGrid.FindControl("cAddMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(31); lb.ToolTip = ColumnToolTip(31); lb.Parent.Visible = GridColumnVisible(31); }
		    lb = cAdmScreenGrid.FindControl("cUpdMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(32); lb.ToolTip = ColumnToolTip(32); lb.Parent.Visible = GridColumnVisible(32); }
		    lb = cAdmScreenGrid.FindControl("cDelMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(33); lb.ToolTip = ColumnToolTip(33); lb.Parent.Visible = GridColumnVisible(33); }
		    lb = cAdmScreenGrid.FindControl("cIncrementMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(34); lb.ToolTip = ColumnToolTip(34); lb.Parent.Visible = GridColumnVisible(34); }
		    lb = cAdmScreenGrid.FindControl("cNoMasterMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(35); lb.ToolTip = ColumnToolTip(35); lb.Parent.Visible = GridColumnVisible(35); }
		    lb = cAdmScreenGrid.FindControl("cNoDetailMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(36); lb.ToolTip = ColumnToolTip(36); lb.Parent.Visible = GridColumnVisible(36); }
		    lb = cAdmScreenGrid.FindControl("cAddMasterMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(37); lb.ToolTip = ColumnToolTip(37); lb.Parent.Visible = GridColumnVisible(37); }
		    lb = cAdmScreenGrid.FindControl("cAddDetailMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(38); lb.ToolTip = ColumnToolTip(38); lb.Parent.Visible = GridColumnVisible(38); }
		    lb = cAdmScreenGrid.FindControl("cMasterLstTitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(39); lb.ToolTip = ColumnToolTip(39); lb.Parent.Visible = GridColumnVisible(39); }
		    lb = cAdmScreenGrid.FindControl("cMasterLstSubtitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(40); lb.ToolTip = ColumnToolTip(40); lb.Parent.Visible = GridColumnVisible(40); }
		    lb = cAdmScreenGrid.FindControl("cMasterRecTitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(41); lb.ToolTip = ColumnToolTip(41); lb.Parent.Visible = GridColumnVisible(41); }
		    lb = cAdmScreenGrid.FindControl("cMasterRecSubtitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(42); lb.ToolTip = ColumnToolTip(42); lb.Parent.Visible = GridColumnVisible(42); }
		    lb = cAdmScreenGrid.FindControl("cMasterFoundMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(43); lb.ToolTip = ColumnToolTip(43); lb.Parent.Visible = GridColumnVisible(43); }
		    lb = cAdmScreenGrid.FindControl("cDetailLstTitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(44); lb.ToolTip = ColumnToolTip(44); lb.Parent.Visible = GridColumnVisible(44); }
		    lb = cAdmScreenGrid.FindControl("cDetailLstSubtitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(45); lb.ToolTip = ColumnToolTip(45); lb.Parent.Visible = GridColumnVisible(45); }
		    lb = cAdmScreenGrid.FindControl("cDetailRecTitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(46); lb.ToolTip = ColumnToolTip(46); lb.Parent.Visible = GridColumnVisible(46); }
		    lb = cAdmScreenGrid.FindControl("cDetailRecSubtitle16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(47); lb.ToolTip = ColumnToolTip(47); lb.Parent.Visible = GridColumnVisible(47); }
		    lb = cAdmScreenGrid.FindControl("cDetailFoundMsg16hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(48); lb.ToolTip = ColumnToolTip(48); lb.Parent.Visible = GridColumnVisible(48); }
		    // Hide DeleteAll:
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if ((dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || dr["ViewOnly"].ToString() == "G" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
		            lb = cAdmScreenGrid.FindControl("cDeleteAllButton") as LinkButton; if (lb != null) { lb.Visible = false; }
				}
			}
		    // footer:
		    Label gc = null;
		    gc = cAdmScreenGrid.FindControl("cScreenHlpId16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(26); }
		    gc = cAdmScreenGrid.FindControl("cCultureId16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(27); }
		    gc = cAdmScreenGrid.FindControl("cScreenTitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(28); }
		    gc = cAdmScreenGrid.FindControl("cDefaultHlpMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(29); }
		    gc = cAdmScreenGrid.FindControl("cFootNote16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(30); }
		    gc = cAdmScreenGrid.FindControl("cAddMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(31); }
		    gc = cAdmScreenGrid.FindControl("cUpdMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(32); }
		    gc = cAdmScreenGrid.FindControl("cDelMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(33); }
		    gc = cAdmScreenGrid.FindControl("cIncrementMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(34); }
		    gc = cAdmScreenGrid.FindControl("cNoMasterMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(35); }
		    gc = cAdmScreenGrid.FindControl("cNoDetailMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(36); }
		    gc = cAdmScreenGrid.FindControl("cAddMasterMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(37); }
		    gc = cAdmScreenGrid.FindControl("cAddDetailMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(38); }
		    gc = cAdmScreenGrid.FindControl("cMasterLstTitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(39); }
		    gc = cAdmScreenGrid.FindControl("cMasterLstSubtitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(40); }
		    gc = cAdmScreenGrid.FindControl("cMasterRecTitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(41); }
		    gc = cAdmScreenGrid.FindControl("cMasterRecSubtitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(42); }
		    gc = cAdmScreenGrid.FindControl("cMasterFoundMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(43); }
		    gc = cAdmScreenGrid.FindControl("cDetailLstTitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(44); }
		    gc = cAdmScreenGrid.FindControl("cDetailLstSubtitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(45); }
		    gc = cAdmScreenGrid.FindControl("cDetailRecTitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(46); }
		    gc = cAdmScreenGrid.FindControl("cDetailRecSubtitle16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(47); }
		    gc = cAdmScreenGrid.FindControl("cDetailFoundMsg16fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(48); }
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

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,cAdmScreen9List,null); }

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

