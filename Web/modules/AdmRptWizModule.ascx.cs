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
using AjaxControlToolkit;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.WebRules;

namespace RO.Common3.Data
{
	public class AdmRptWiz95 : DataSet
	{
        public AdmRptWiz95()
        {
            this.Tables.Add(MakeColumns(new DataTable("AdmRptWiz")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptWizAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptWizDel")));
            this.DataSetName = "AdmRptWiz95";
            this.Namespace = "http://Rintagi.com/DataSet/AdmRptWiz95";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("RptwizId183", typeof(string));
            columns.Add("RptwizName183", typeof(string));
            columns.Add("RptwizDesc183", typeof(string));
            columns.Add("ReportId183", typeof(string));
            columns.Add("AccessCd183", typeof(string));
            columns.Add("UsrId183", typeof(string));
            columns.Add("TemplateName183", typeof(string));
            columns.Add("OrientationCd183", typeof(string));
            columns.Add("UnitCd183", typeof(string));
            columns.Add("TopMargin183", typeof(string));
            columns.Add("BottomMargin183", typeof(string));
            columns.Add("LeftMargin183", typeof(string));
            columns.Add("RightMargin183", typeof(string));
            columns.Add("RptwizTypeCd183", typeof(string));
            columns.Add("RptwizCatId183", typeof(string));
            columns.Add("RptChaTypeCd183", typeof(string));
            columns.Add("ThreeD183", typeof(string));
            columns.Add("GMinValue183", typeof(string));
            columns.Add("GLowRange183", typeof(string));
            columns.Add("GMidRange183", typeof(string));
            columns.Add("GMaxValue183", typeof(string));
            columns.Add("GNeedle183", typeof(string));
            columns.Add("GMinValueId183", typeof(string));
            columns.Add("GLowRangeId183", typeof(string));
            columns.Add("GMidRangeId183", typeof(string));
            columns.Add("GMaxValueId183", typeof(string));
            columns.Add("GNeedleId183", typeof(string));
            columns.Add("GPositive183", typeof(string));
            columns.Add("GFormat183", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("RptwizId183", typeof(string));
            columns.Add("RptwizDtlId184", typeof(string));
            columns.Add("ColumnId184", typeof(string));
            columns.Add("ColHeader184", typeof(string));
            columns.Add("CriOperName184", typeof(string));
            columns.Add("CriSelect184", typeof(string));
            columns.Add("CriHeader184", typeof(string));
            columns.Add("ColSelect184", typeof(string));
            columns.Add("ColGroup184", typeof(string));
            columns.Add("ColSort184", typeof(string));
            columns.Add("AggregateCd184", typeof(string));
            columns.Add("RptChartCd184", typeof(string));
            return dt;
        }
    }
}

namespace RO.Web
{
	public partial class AdmRptWizModule : RO.Web.ModuleBase
	{
        private const string KEY_dtEntity = "Cache:dtEntity";
        private string KEY_dtSystems;
        private string KEY_sysConnectionString;
        private string KEY_dtAdmRptWiz95List;
        private string KEY_bPreviewVisible;
        private string KEY_bNewVisible;
        private string KEY_bNewSaveVisible;
        private string KEY_bCopyVisible;
        private string KEY_bCopySaveVisible;
        private string KEY_bDeleteVisible;
        private string KEY_bUpdateVisible;
        private string KEY_bClCriVisible;
        private string KEY_bHiCriVisible;
        private string KEY_bShCriVisible;
        private string KEY_lastAddedRow;
        private string KEY_lastSortOrder;
        private string KEY_lastSortExpr;
        private string KEY_lastSortCol;
        private string KEY_lastSortUrl;
        private string KEY_lastImpPwdOvride;
        private string KEY_cntImpPwdOvride;
        private string KEY_currPageIndex;
        private string KEY_iHiFindVisible;
        private string KEY_bHiFindVisible;
        private string KEY_iShFindVisible;
        private string KEY_bShFindVisible;
        private string KEY_iHiImpVisible;
        private string KEY_bHiImpVisible;
        private string KEY_iShImpVisible;
        private string KEY_bShImpVisible;
        private string KEY_dtScreenHlp;
        private string KEY_dtClientRule;
        private string KEY_dtAuthCol;
        private string KEY_dtAuthRow;
        private string KEY_dtLabel;
        private string KEY_dtCri;
        private string KEY_dtReportId183;
        private string KEY_dtAccessCd183;
        private string KEY_dtUsrId183;
        private string KEY_dtOrientationCd183;
        private string KEY_dtGPositive183;
        private string KEY_dtUnitCd183;
        private string KEY_dtRptwizTypeCd183;
        private string KEY_dtRptwizCatId183;
        private string KEY_dtRptChaTypeCd183;
        private string KEY_dtGFormat183;
        private string KEY_dtOriColumnId33;
        private string KEY_dtSelColumnId33;
        private string KEY_dtOriColumnId44;
        private string KEY_dtSelColumnId44;
        private string KEY_dtOriColumnId77;
        private string KEY_dtSelColumnId77;
        private string KEY_dtColSort;
        private string KEY_dtAggregate;
        private string KEY_dtRptGroup;
        private string KEY_dtRptChart;
        private string KEY_dtOperator;
        private string KEY_dtDelColumnId;
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;

		public AdmRptWizModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
			bConfirm.Value = "Y";
			// To get around ajax not displaying ErrMsg and InfoMsg; Set them to Y to show immediately:
			bErrNow.Value = "N"; bInfoNow.Value = "N"; bExpNow.Value = "N";
			CtrlToFocus.Value = string.Empty;
			EnableValidators(false);
			if (!IsPostBack)
			{
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,95, null, null);
				if (dtMenuAccess.Rows.Count == 0)
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
                Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
                Session.Remove(KEY_lastSortCol);
                Session.Remove(KEY_lastSortUrl);
                Session.Remove(KEY_dtAdmRptWiz95List);
                Session.Remove(KEY_dtSystems);
                Session.Remove(KEY_sysConnectionString);
                Session.Remove(KEY_dtScreenHlp);
                Session.Remove(KEY_dtClientRule);
                Session.Remove(KEY_dtAuthCol);
                Session.Remove(KEY_dtAuthRow);
                Session.Remove(KEY_dtLabel);
                Session.Remove(KEY_dtCri);
                Session.Remove(KEY_dtReportId183);
                Session.Remove(KEY_dtAccessCd183);
                Session.Remove(KEY_dtUsrId183);
                Session.Remove(KEY_dtOrientationCd183);
                Session.Remove(KEY_dtGPositive183);
                Session.Remove(KEY_dtUnitCd183);
                Session.Remove(KEY_dtRptwizTypeCd183);
                Session.Remove(KEY_dtRptwizCatId183);
                Session.Remove(KEY_dtRptChaTypeCd183);
                Session.Remove(KEY_dtEntity);
                //No need to call cRptwizTypeCd183_SelectedIndexChanged(sender, e);
                //Do not do Session.Remove(KEY_dtSelColumnId33);
                //Must do SetRptGroup before PopAdmRptWiz95List;
                Session.Remove(KEY_dtGFormat183);
                Session.Remove(KEY_dtOriColumnId33);
                Session.Remove(KEY_dtOriColumnId44);
                Session.Remove(KEY_dtOriColumnId77);
                Session.Remove(KEY_dtColSort); SetColSort();
                Session.Remove(KEY_dtAggregate); SetAggregate();
                Session.Remove(KEY_dtRptGroup); SetRptGroup();
                Session.Remove(KEY_dtRptChart); SetRptChart();
                Session.Remove(KEY_dtOperator); SetOperator();
                SetButtonHlp();
				GetSystems();
				SetColumnAuthority();
				GetGlobalFilter();
				//GetScreenFilter();
				PopAdmRptWiz95List(sender, e, true, null);
                DataTable dtHlp = GetScreenHlp();
                cHelpMsg.HelpTitle = dtHlp.Rows[0]["ScreenTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
                cFootLabel.Text = dtHlp.Rows[0]["FootNote"].ToString();
                if (cFootLabel.Text == string.Empty) { cFootLabel.Visible = false; }
                cTitleLabel.Text = dtHlp.Rows[0]["ScreenTitle"].ToString();
                DataTable dt = GetScreenTab();
                cTab3.InnerText = dt.Rows[0]["TabFolderName"].ToString();
                cTab22.InnerText = dt.Rows[1]["TabFolderName"].ToString();
                SetClientRule(null);
                IgnoreConfirm();
                //InitPreserve();
                try
                {
                    (new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 95, 0, 0, string.Empty, LcSysConnString, LcAppPw);
                }
                catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                if (((char)191 + LImpr.RowAuthoritys + (char)191).IndexOf((char)191 + "8" + (char)191) >= 0)
                {
                    cXfer.Visible = true;
                    if (cXfer.Attributes["OnClick"] == null || cXfer.Attributes["OnClick"].IndexOf("return confirm") < 0)
                    {
                        cXfer.Attributes["OnClick"] += "return confirm('Proceed to migrate to advanced report definition for further customization for sure? This report will be removed from here.');";
                    }
                }
                cRptwizCatId183Search_Script();
                cRptwizTypeCd183.Attributes["OnChange"] += "__doPostBack('" + cTabFolder.ClientID + "','');";
            }
            else { PanelVar.Update(); }
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
            /* Testing only - Keep for the quick search for DataViews in sessions:
            foreach (string s in Session.Keys)
            {
                if (Session[s].GetType() == typeof(DataView))
                {
                    throw new Exception(s);
                }
            }
            */
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
            KEY_dtSystems = "Cache:dtSystems95" + this.ID;
            KEY_sysConnectionString = "Cache:sysConnectionString95" + this.ID;
            KEY_dtAdmRptWiz95List = "Cache:dtAdmRptWiz95List" + this.ID;
            KEY_bPreviewVisible = "Cache:bPreviewVisible95" + this.ID;
            KEY_bNewVisible = "Cache:bNewVisible95" + this.ID;
            KEY_bNewSaveVisible = "Cache:bNewSaveVisible95" + this.ID;
            KEY_bCopyVisible = "Cache:bCopyVisible95" + this.ID;
            KEY_bCopySaveVisible = "Cache:bCopySaveVisible95" + this.ID;
            KEY_bDeleteVisible = "Cache:bDeleteVisible95" + this.ID;
            KEY_bUpdateVisible = "Cache:bUpdateVisible95" + this.ID;
            KEY_bClCriVisible = "Cache:bClCriVisible95" + this.ID;
            KEY_bHiCriVisible = "Cache:bHiCriVisible95" + this.ID;
            KEY_bShCriVisible = "Cache:bShCriVisible95" + this.ID;
            KEY_lastAddedRow = "Cache:lastAddedRow95" + this.ID;
            KEY_lastSortOrder = "Cache:lastSortOrder95" + this.ID;
            KEY_lastSortExpr = "Cache:lastSortExpr95" + this.ID;
            KEY_lastSortCol = "Cache:lastSortCol95" + this.ID;
            KEY_lastSortUrl = "Cache:lastSortUrl95" + this.ID;
            KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride95" + this.ID;
            KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride95" + this.ID;
            KEY_currPageIndex = "Cache:currPageIndex95" + this.ID;
            KEY_iHiFindVisible = "Cache:iHiFindVisible95" + this.ID;
            KEY_bHiFindVisible = "Cache:bHiFindVisible95" + this.ID;
            KEY_iShFindVisible = "Cache:iShFindVisible95" + this.ID;
            KEY_bShFindVisible = "Cache:bShFindVisible95" + this.ID;
            KEY_iHiImpVisible = "Cache:iHiImpVisible95" + this.ID;
            KEY_bHiImpVisible = "Cache:bHiImpVisible95" + this.ID;
            KEY_iShImpVisible = "Cache:iShImpVisible95" + this.ID;
            KEY_bShImpVisible = "Cache:bShImpVisible95" + this.ID;
            KEY_dtScreenHlp = "Cache:dtScreenHlp95" + this.ID;
            KEY_dtClientRule = "Cache:dtClientRule95" + this.ID;
            KEY_dtAuthCol = "Cache:dtAuthCol95" + this.ID;
            KEY_dtAuthRow = "Cache:dtAuthRow95" + this.ID;
            KEY_dtLabel = "Cache:dtLabel95" + this.ID;
            KEY_dtCri = "Cache:dtCri95" + this.ID;

            KEY_dtReportId183 = "Cache:dtReportId183" + this.ID;
            KEY_dtAccessCd183 = "Cache:dtAccessCd183" + this.ID;
            KEY_dtUsrId183 = "Cache:dtUsrId183" + this.ID;
            KEY_dtOrientationCd183 = "Cache:dtOrientationCd183" + this.ID;
            KEY_dtGPositive183 = "Cache:dtGPositive183" + this.ID;
            KEY_dtUnitCd183 = "Cache:dtUnitCd183" + this.ID;
            KEY_dtRptwizTypeCd183 = "Cache:dtRptwizTypeCd183" + this.ID;
            KEY_dtRptwizCatId183 = "Cache:dtRptwizCatId183" + this.ID;
            KEY_dtRptChaTypeCd183 = "Cache:dtRptChaTypeCd183" + this.ID;
            KEY_dtGFormat183 = "Cache:dtGFormat183" + this.ID;
            KEY_dtOriColumnId33 = "Cache:dtOriColumnId33" + this.ID;
            KEY_dtSelColumnId33 = "Cache:dtSelColumnId33" + this.ID;
            KEY_dtOriColumnId44 = "Cache:dtOriColumnId44" + this.ID;
            KEY_dtSelColumnId44 = "Cache:dtSelColumnId44" + this.ID;
            KEY_dtOriColumnId77 = "Cache:dtOriColumnId77" + this.ID;
            KEY_dtSelColumnId77 = "Cache:dtSelColumnId77" + this.ID;
            KEY_dtColSort = "Cache:dtColSort" + this.ID;
            KEY_dtAggregate = "Cache:dtAggregate" + this.ID;
            KEY_dtRptGroup = "Cache:dtRptGroup" + this.ID;
            KEY_dtRptChart = "Cache:dtRptChart" + this.ID;
            KEY_dtOperator = "Cache:dtOperator" + this.ID;
            KEY_dtDelColumnId = "Cache:dtDelColumnId" + this.ID;

            CheckAuthentication(true);
            if (LcSysConnString == null)
            {
                SetSystem(3);
                try
                {
                    DataTable dt = (new AdminSystem()).GetAuthRow(95, base.LImpr.RowAuthoritys, LcSysConnString, LcAppPw);
                    Session[KEY_dtAuthRow] = dt;
                }
                catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
            }

		}
		#endregion

        private void SetColSort()
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtColSort];
                if (dt == null) { dt = (new AdminSystem()).GetLabels(base.LUser.CultureId, "SortDirection", null, null, null); }
                if (dt != null)
                {
                    cColSort.DataSource = dt.DefaultView; cColSort.DataBind();
                    if (cColSort.Items.Count > 0) { cColSort.Items[0].Selected = true; }
                    Session[KEY_dtColSort] = dt;
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetAggregate()
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtAggregate];
                if (dt == null)
                {
                    dt = (new AdminSystem()).GetDdl(95, "GetDdlAggregateCd3S1652", false, true, 0, string.Empty, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr);
                }
                if (dt != null)
                {
                    cAggregate.DataSource = dt.DefaultView; cAggregate.DataBind();
                    Session[KEY_dtAggregate] = dt;
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetRptGroup()
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtRptGroup];
                if (dt == null)
                {
                    dt = (new WebRule()).GetDdlRptGroupId3S1652((string)Session[KEY_sysConnectionString], LcAppPw);
                }
                if (dt != null)
                {
                    cRptGroup.DataSource = dt.DefaultView; cRptGroup.DataBind();
                    Session[KEY_dtRptGroup] = dt;
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetRptChart()
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtRptChart];
                if (dt == null)
                {
                    dt = (new WebRule()).GetDdlRptChart3S1652((string)Session[KEY_sysConnectionString], LcAppPw);
                }
                if (dt != null)
                {
                    cRptChart.DataSource = dt.DefaultView; cRptChart.DataBind();
                    Session[KEY_dtRptChart] = dt;
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetOperator()
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtOperator];
                if (dt == null)
                {
                    dt = (new WebRule()).GetDdlOperator3S1652((string)Session[KEY_sysConnectionString], LcAppPw);
                }
                if (dt != null)
                {
                    cOperator.DataSource = dt.DefaultView; cOperator.DataBind();
                    Session[KEY_dtOperator] = dt;
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        protected void cSelect33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId33];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvOri != null && dvSel != null)
            {
                int ii = 0;
                DataRow dr = null;
                foreach (DataRowView drvOri in dvOri)
                {
                    if (cOriColumnId33.Items[ii].Selected)
                    {
                        dr = dvSel.Table.NewRow(); dvSel.Table.Rows.Add(dr);
                        dr[0] = cOriColumnId33.Items[ii].Value;	// Column Id;
                        dr[1] = cOriColumnId33.Items[ii].Text;		// Selected column;
                        dr[2] = cOriColumnId33.Items[ii].Text;		// Aggregate column;
                        dr[3] = cOriColumnId33.Items[ii].Text;		// Report Group column;
                        dr[4] = cOriColumnId33.Items[ii].Text;		// Chart column;
                        dr[5] = cOriColumnId33.Items[ii].Text;		// Gauge column;
                        dr[6] = drvOri[2].ToString();				// NumericData
                        cOriColumnId33.Items[ii].Selected = false;
                    }
                    if (ii < cOriColumnId33.Items.Count - 1) { ii = ii + 1; }
                }
                Session[KEY_dtSelColumnId33] = dvSel.Table;
                cSelColumnId33.DataSource = dvSel; cSelColumnId33.DataBind();
                if (Tab55.Visible) { cSelColumnId55.DataSource = dvSel; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dvSel; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dvSel; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dvSel.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                SetOriColumnId33(cOriColumnId33, string.Empty);
                if (cSelColumnId33.Items.Count > 0) { cSelColumnId33.Items[dvSel.Count - 1].Selected = true; }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cRemove33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId33];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataTable dtDel = (DataTable)Session[KEY_dtDelColumnId];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            DataView dvDel = dtDel != null ? dtDel.DefaultView : null;
            if (dvOri != null && dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId33.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)	// dvSel.Delete does not work.
                {
                    if (drvSel[0].ToString() != cSelColumnId33.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString();		// Column Id;
                        dr[1] = drvSel[1].ToString();		// Selected column;
                        dr[2] = drvSel[2].ToString();		// Aggregate column;
                        dr[3] = drvSel[3].ToString();		// Report Group column;
                        dr[4] = drvSel[4].ToString();		// Chart column;
                        dr[5] = drvSel[5].ToString();		// Gauge column;
                        dr[6] = drvSel[6].ToString();		// NumericData
                        if (drvSel[7].ToString() != string.Empty) { dr[7] = drvSel[7].ToString(); }	// RptwizDtlId
                    }
                    else if (drvSel[7].ToString() != string.Empty)
                    {
                        dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                        dr["RptwizDtlId184"] = drvSel[7]; Session[KEY_dtDelColumnId] = dvDel.Table;
                    }
                }
                Session[KEY_dtSelColumnId33] = dv.Table;
                cSelColumnId33.DataSource = dv; cSelColumnId33.DataBind();
                if (Tab55.Visible) { cSelColumnId55.DataSource = dv; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dv; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dv; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                SetOriColumnId33(cOriColumnId33, keyId);
                if (cSelColumnId33.Items.Count > 0) { cSelColumnId33.Items[dv.Count - 1].Selected = true; }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cTop33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                DataView dv = new DataView(dvSel.Table.Clone());
                int ii = cSelColumnId33.SelectedIndex;
                DataRow dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                dr[0] = dvSel[ii][0].ToString(); dr[1] = dvSel[ii][1].ToString(); dr[2] = dvSel[ii][2].ToString(); dr[3] = dvSel[ii][3].ToString(); dr[4] = dvSel[ii][4].ToString(); dr[5] = dvSel[ii][5].ToString();
                if (dvSel[ii][6].ToString() != string.Empty) { dr[6] = dvSel[ii][6].ToString(); }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId33.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString(); dr[5] = drvSel[5].ToString(); dr[6] = drvSel[6].ToString();
                        if (drvSel[7].ToString() != string.Empty) { dr[7] = drvSel[7].ToString(); }
                    }
                }
                cSelColumnId33.DataSource = dv; cSelColumnId33.DataBind(); cSelColumnId33.Items[0].Selected = true;
                if (Tab55.Visible) { cSelColumnId55.DataSource = dv; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dv; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dv; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                Session[KEY_dtSelColumnId33] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cUp33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId33.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just before the row to be moved up:
                string d0 = string.Empty;
                string d1 = string.Empty;
                string d2 = string.Empty;
                string d3 = string.Empty;
                string d4 = string.Empty;
                string d5 = string.Empty;
                string d6 = string.Empty;
                string d7 = string.Empty;
                int ii = cSelColumnId33.SelectedIndex - 1;
                if (ii >= 0) { d0 = dvSel[ii][0].ToString(); d1 = dvSel[ii][1].ToString(); d2 = dvSel[ii][2].ToString(); d3 = dvSel[ii][3].ToString(); d4 = dvSel[ii][4].ToString(); d5 = dvSel[ii][5].ToString(); d6 = dvSel[ii][6].ToString(); d7 = dvSel[ii][7].ToString(); }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != d0)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString(); dr[5] = drvSel[5].ToString(); dr[6] = drvSel[6].ToString();
                        if (drvSel[7].ToString() != string.Empty) { dr[7] = drvSel[7].ToString(); }
                        if (drvSel[0].ToString() == keyId && d0 != string.Empty)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = d0; dr[1] = d1; dr[2] = d2; dr[3] = d3; dr[4] = d4; dr[5] = d5; dr[6] = d6;
                            if (d7 != string.Empty) { dr[7] = d7; }
                        }
                    }
                }
                cSelColumnId33.DataSource = dv; cSelColumnId33.DataBind(); cSelColumnId33.Items.FindByValue(keyId).Selected = true;
                if (Tab55.Visible) { cSelColumnId55.DataSource = dv; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dv; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dv; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                Session[KEY_dtSelColumnId33] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cDown33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId33.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just after the row to be moved down:
                string d0 = string.Empty;
                string k1 = string.Empty;
                string k2 = string.Empty;
                string k3 = string.Empty;
                string k4 = string.Empty;
                string k5 = string.Empty;
                string k6 = string.Empty;
                string k7 = string.Empty;
                int ii = cSelColumnId33.SelectedIndex;
                k1 = dvSel[ii][1].ToString(); k2 = dvSel[ii][2].ToString(); k3 = dvSel[ii][3].ToString(); k4 = dvSel[ii][4].ToString(); k5 = dvSel[ii][5].ToString(); k6 = dvSel[ii][6].ToString(); k7 = dvSel[ii][7].ToString();
                if (ii + 1 < dvSel.Count) { d0 = dvSel[ii + 1][0].ToString(); }
                else
                {
                    if (ii - 1 >= 0) { d0 = dvSel[ii - 1][0].ToString(); }
                }
                if (d0 != string.Empty)	// More than one row:
                {
                    foreach (DataRowView drvSel in dvSel)
                    {
                        if (drvSel[0].ToString() != keyId)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString(); dr[5] = drvSel[5].ToString(); dr[6] = drvSel[6].ToString();
                            if (drvSel[7].ToString() != string.Empty) { dr[7] = drvSel[7].ToString(); }
                            if (drvSel[0].ToString() == d0)
                            {
                                dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                                dr[0] = keyId; dr[1] = k1; dr[2] = k2; dr[3] = k3; dr[4] = k4; dr[5] = k5; dr[6] = k6;
                                if (k7 != string.Empty) { dr[7] = k7; }
                            }
                        }
                    }
                    cSelColumnId33.DataSource = dv; cSelColumnId33.DataBind(); cSelColumnId33.Items.FindByValue(keyId).Selected = true;
                    if (Tab55.Visible) { cSelColumnId55.DataSource = dv; cSelColumnId55.DataBind(); EnbSelCol55(); }
                    if (Tab66.Visible) { cSelColumnId66.DataSource = dv; cSelColumnId66.DataBind(); }
                    if (Tab88.Visible) { cSelColumnId88.DataSource = dv; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                    if (Tab99.Visible)
                    {
                        DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                        cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                        cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                        cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                        cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                        cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                    }
                    Session[KEY_dtSelColumnId33] = dv.Table;
                }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cBottom33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId33.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString(); dr[5] = drvSel[5].ToString(); dr[6] = drvSel[6].ToString();
                        if (drvSel[7].ToString() != string.Empty) { dr[7] = drvSel[7].ToString(); }
                    }
                }
                int ii = cSelColumnId33.SelectedIndex;
                dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                dr[0] = dvSel[ii][0].ToString(); dr[1] = dvSel[ii][1].ToString(); dr[2] = dvSel[ii][2].ToString(); dr[3] = dvSel[ii][3].ToString(); dr[4] = dvSel[ii][4].ToString(); dr[5] = dvSel[ii][5].ToString(); dr[6] = dvSel[ii][6].ToString();
                if (dvSel[ii][7].ToString() != string.Empty) { dr[7] = dvSel[ii][7].ToString(); }
                cSelColumnId33.DataSource = dv; cSelColumnId33.DataBind(); cSelColumnId33.Items[dvSel.Count - 1].Selected = true;
                if (Tab55.Visible) { cSelColumnId55.DataSource = dv; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dv; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dv; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                Session[KEY_dtSelColumnId33] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cSelButton33_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId33.SelectedIndex >= 0)
            {
                int ii = cSelColumnId33.SelectedIndex;
                if (cSelChange33.Text == string.Empty)
                {
                    dvSel[ii][1] = cSelColumnId33.SelectedItem.Text;
                    dvSel[ii][2] = cSelColumnId33.SelectedItem.Text;		// Aggregate column;
                    dvSel[ii][3] = cSelColumnId33.SelectedItem.Text;		// Report Group column;
                    dvSel[ii][4] = cSelColumnId33.SelectedItem.Text;		// Chart column;
                    dvSel[ii][5] = cSelColumnId33.SelectedItem.Text;		// Gauge column;
                }
                else
                {
                    dvSel[ii][1] = cSelChange33.Text;
                    dvSel[ii][2] = cSelChange33.Text;		// Aggregate column;
                    dvSel[ii][3] = cSelChange33.Text;		// Report Group column;
                    dvSel[ii][4] = cSelChange33.Text;		// Chart column;
                    dvSel[ii][5] = cSelChange33.Text;		// Gauge column;
                }
                cSelColumnId33.DataSource = dvSel; cSelColumnId33.DataBind(); cSelColumnId33.Items[ii].Selected = true;
                if (Tab55.Visible) { cSelColumnId55.DataSource = dvSel; cSelColumnId55.DataBind(); EnbSelCol55(); }
                if (Tab66.Visible) { cSelColumnId66.DataSource = dvSel; cSelColumnId66.DataBind(); }
                if (Tab88.Visible) { cSelColumnId88.DataSource = dvSel; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                if (Tab99.Visible)
                {
                    DataView dvg = new DataView(dvSel.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                    cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                    cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                    cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                    cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                    cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                }
                Session[KEY_dtSelColumnId33] = dvSel.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        private void SetOriColumnId33(ListBox ddl, string keyId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtOriColumnId33];
                DataView dv = dt != null ? dt.DefaultView : null;
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dv == null)
                    {
                        dv = new DataView((new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, bAll, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId));
                    }
                    if (dv != null)
                    {
                        ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dv = new DataView((new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, true, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId));
                            ddl.DataSource = dv; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtOriColumnId33] = dv.Table;
                        // Filter out selected columns:
                        DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
                        DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
                        if (dvSel != null && dvSel.Count > 0)
                        {
                            bool bFound = false;
                            DataRow dr = null;
                            DataView dvClone = new DataView(dv.Table.Clone());
                            foreach (DataRowView drvOri in dv)
                            {
                                bFound = false;
                                foreach (DataRowView drvSel in dvSel)
                                {
                                    if (drvSel[0].ToString() == drvOri[0].ToString()) { bFound = true; break; }
                                }
                                if (!bFound)
                                {
                                    dr = dvClone.Table.NewRow(); dvClone.Table.Rows.Add(dr);
                                    dr[0] = drvOri[0].ToString(); dr[1] = drvOri[1].ToString();
                                }
                            }
                            ddl.DataSource = dvClone; ddl.DataBind();
                            li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        }
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetSelColumnId33(ListBox ddl, string keyId, string filterId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtSelColumnId33];
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dt == null)
                    {
                        dt = (new WebRule()).GetDdlSelColumnId33S1682(95, bAll, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                    }
                    if (dt != null)
                    {
                        ddl.DataSource = dt.DefaultView; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dt.Rows.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dt = (new WebRule()).GetDdlSelColumnId33S1682(95, true, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                            ddl.DataSource = dt.DefaultView; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtSelColumnId33] = dt;
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetSelColumnId99(DropDownList ddl, string keyId, string filterId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtSelColumnId33];
                DataView dv = dt != null ? dt.DefaultView : null;
                if (ddl != null)
                {
                    if (dv == null)
                    {
                        dv = new DataView((new WebRule()).GetDdlSelColumnId33S1682(95, true, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId));
                    }
                    if (dv != null)
                    {
                        DataView dvg = new DataView(dv.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                        ddl.DataSource = dvg; ddl.DataBind();
                        ListItem li = ddl.Items.FindByValue(keyId);
                        if (li != null) { li.Selected = true; }
                        Session[KEY_dtSelColumnId33] = dv.Table;
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        protected void cOriColumnId33_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId33.SelectedIndex >= 0) { cSelColumnId33.SelectedItem.Selected = false; }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cOriColumnId33.ClientID);
        }

        protected void cSelColumnId33_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId33.SelectedIndex >= 0) { cSelChange33.Text = cSelColumnId33.SelectedItem.Text; }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId33.ClientID);
        }

        protected void cSelect44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId44];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvOri != null && dvSel != null)
            {
                bool bFound = false;
                int ii = 0;
                DataRow dr = null;
                foreach (DataRowView drvOri in dvOri)
                {
                    // dvSel.Table.Copy() does not work and need to check this before AddNew.
                    bFound = false;
                    foreach (DataRowView drvSel in dvSel)
                    {
                        if (drvSel[0].ToString() == drvOri[0].ToString()) { bFound = true; break; }
                    }
                    if (cOriColumnId44.Items[ii].Selected)
                    {
                        dr = dvSel.Table.NewRow(); dvSel.Table.Rows.Add(dr);
                        dr[0] = cOriColumnId44.Items[ii].Value;
                        dr[1] = "[ASC] " + cOriColumnId44.Items[ii].Text; dr[2] = ii.ToString();
                        cOriColumnId44.Items[ii].Selected = false;
                        if (ii < cOriColumnId44.Items.Count - 1) { ii = ii + 1; }
                    }
                    else if (!bFound && ii < cOriColumnId44.Items.Count - 1) { ii = ii + 1; }
                }
                cSelColumnId44.DataSource = dvSel; cSelColumnId44.DataBind(); Session[KEY_dtSelColumnId44] = dvSel.Table;
                SetOriColumnId44(cOriColumnId44, string.Empty);
                if (cSelColumnId44.Items.Count > 0) { cSelColumnId44.Items[dvSel.Count - 1].Selected = true; cSelColumnId44_SelectedIndexChanged(sender, new EventArgs()); }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cRemove44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId44];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataTable dtDel = (DataTable)Session[KEY_dtDelColumnId];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            DataView dvDel = dtDel != null ? dtDel.DefaultView : null;
            if (dvOri != null && dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId44.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)	// dvSel.Delete does not work.
                {
                    if (drvSel[0].ToString() != cSelColumnId44.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString();
                        if (drvSel[3].ToString() != string.Empty) { dr[3] = drvSel[3].ToString(); }
                    }
                    else if (drvSel[3].ToString() != string.Empty)
                    {
                        dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                        dr["RptwizDtlId184"] = drvSel[3]; Session[KEY_dtDelColumnId] = dvDel.Table;
                    }
                }
                cSelColumnId44.DataSource = dv; cSelColumnId44.DataBind(); Session[KEY_dtSelColumnId44] = dv.Table;
                SetOriColumnId44(cOriColumnId44, keyId);
                if (cSelColumnId44.Items.Count > 0)
                {
                    cSelColumnId44.Items[dv.Count - 1].Selected = true; cSelColumnId44_SelectedIndexChanged(sender, new EventArgs());
                }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cTop44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                DataView dv = new DataView(dvSel.Table.Clone());
                int ii = cSelColumnId44.SelectedIndex;
                DataRow dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                dr[0] = dvSel[ii][0].ToString(); dr[1] = dvSel[ii][1].ToString(); dr[2] = dvSel[ii][2].ToString();
                if (dvSel[ii][3].ToString() != string.Empty) { dr[3] = dvSel[ii][3].ToString(); }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId44.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString();
                        if (drvSel[3].ToString() != string.Empty) { dr[3] = drvSel[3].ToString(); }
                    }
                }
                cSelColumnId44.DataSource = dv; cSelColumnId44.DataBind(); cSelColumnId44.Items[0].Selected = true;
                Session[KEY_dtSelColumnId44] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cUp44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId44.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just before the row to be moved up:
                string d0 = string.Empty;
                string d1 = string.Empty;
                string d2 = string.Empty;
                string d3 = string.Empty;
                int ii = cSelColumnId44.SelectedIndex - 1;
                if (ii >= 0) { d0 = dvSel[ii][0].ToString(); d1 = dvSel[ii][1].ToString(); d2 = dvSel[ii][2].ToString(); d3 = dvSel[ii][3].ToString(); }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != d0)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString();
                        if (drvSel[3].ToString() != string.Empty) { dr[3] = drvSel[3].ToString(); }
                        if (drvSel[0].ToString() == keyId && d0 != string.Empty)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = d0; dr[1] = d1; dr[2] = d2;
                            if (d3 != string.Empty) { dr[3] = d3; }
                        }
                    }
                }
                cSelColumnId44.DataSource = dv; cSelColumnId44.DataBind(); cSelColumnId44.Items.FindByValue(keyId).Selected = true;
                Session[KEY_dtSelColumnId44] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cDown44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId44.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just after the row to be moved down:
                string d0 = string.Empty;
                string k1 = string.Empty;
                string k2 = string.Empty;
                string k3 = string.Empty;
                int ii = cSelColumnId44.SelectedIndex;
                k1 = dvSel[ii][1].ToString(); k2 = dvSel[ii][2].ToString(); k3 = dvSel[ii][3].ToString();
                if (ii + 1 < dvSel.Count) { d0 = dvSel[ii + 1][0].ToString(); }
                else
                {
                    if (ii - 1 >= 0) { d0 = dvSel[ii - 1][0].ToString(); }
                }
                if (d0 != string.Empty)	// More than one row:
                {
                    foreach (DataRowView drvSel in dvSel)
                    {
                        if (drvSel[0].ToString() != keyId)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString();
                            if (drvSel[3].ToString() != string.Empty) { dr[3] = drvSel[3].ToString(); }
                            if (drvSel[0].ToString() == d0)
                            {
                                dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                                dr[0] = keyId; dr[1] = k1; dr[2] = k2;
                                if (k3 != string.Empty) { dr[3] = k3; }
                            }
                        }
                    }
                    if (d0 == string.Empty)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = keyId; dr[1] = k1; dr[2] = k2; if (k3 != string.Empty) { dr[3] = k3; }
                    }
                    cSelColumnId44.DataSource = dv; cSelColumnId44.DataBind(); cSelColumnId44.Items.FindByValue(keyId).Selected = true;
                    Session[KEY_dtSelColumnId44] = dv.Table;
                }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cBottom44_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId44.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString();
                        if (drvSel[3].ToString() != string.Empty) { dr[3] = drvSel[3].ToString(); }
                    }
                }
                int ii = cSelColumnId44.SelectedIndex;
                dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                dr[0] = dvSel[ii][0].ToString(); dr[1] = dvSel[ii][1].ToString(); dr[2] = dvSel[ii][2].ToString();
                if (dvSel[ii][3].ToString() != string.Empty) { dr[3] = dvSel[ii][3].ToString(); }
                cSelColumnId44.DataSource = dv; cSelColumnId44.DataBind(); cSelColumnId44.Items[dvSel.Count - 1].Selected = true;
                Session[KEY_dtSelColumnId44] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        private void SetOriColumnId44(ListBox ddl, string keyId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtOriColumnId44];
                DataView dv = dt != null ? dt.DefaultView : null;
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dv == null)
                    {
                        dv = new DataView((new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, bAll, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId));
                    }
                    if (dv != null)
                    {
                        ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dv = new DataView((new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, true, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId));
                            ddl.DataSource = dv; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtOriColumnId44] = dv.Table;
                        // Filter out selected columns:
                        DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
                        DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
                        if (dvSel != null && dvSel.Count > 0)
                        {
                            bool bFound = false;
                            DataRow dr = null;
                            DataView dvClone = new DataView(dv.Table.Clone());
                            foreach (DataRowView drvOri in dv)
                            {
                                bFound = false;
                                foreach (DataRowView drvSel in dvSel)
                                {
                                    if (drvSel[0].ToString() == drvOri[0].ToString()) { bFound = true; break; }
                                }
                                if (!bFound)
                                {
                                    dr = dvClone.Table.NewRow(); dvClone.Table.Rows.Add(dr);
                                    dr[0] = drvOri[0].ToString(); dr[1] = drvOri[1].ToString();
                                }
                            }
                            ddl.DataSource = dvClone; ddl.DataBind();
                            li = ddl.Items.FindByValue(keyId);
                            if (li != null) { li.Selected = true; }
                        }
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetSelColumnId44(ListBox ddl, string keyId, string filterId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtSelColumnId44];
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dt == null)
                    {
                        dt = (new WebRule()).GetDdlSelColumnId44S1682(95, bAll, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                    }
                    if (dt != null)
                    {
                        ddl.DataSource = dt.DefaultView; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dt.Rows.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dt = (new WebRule()).GetDdlSelColumnId44S1682(95, true, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                            ddl.DataSource = dt.DefaultView; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtSelColumnId44] = dt;
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        protected void cOriColumnId44_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId44.SelectedIndex >= 0) { cSelColumnId44.SelectedItem.Selected = false; }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cOriColumnId44.ClientID);
        }

        protected void cColSort_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId44];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId44.SelectedIndex >= 0)
            {
                string strFr;
                string strTo;
                int ii = cSelColumnId44.SelectedIndex;
                if (cColSort.SelectedValue == "A") { strFr = "[DSC]"; strTo = "[ASC]"; } else { strFr = "[ASC]"; strTo = "[DSC]"; }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() == cSelColumnId44.SelectedValue)
                    {
                        drvSel[1] = cSelColumnId44.SelectedItem.Text.Replace(strFr, strTo); break;
                    }
                }
                cSelColumnId44.DataSource = dvSel; cSelColumnId44.DataBind(); cSelColumnId44.Items[ii].Selected = true;
                Session[KEY_dtSelColumnId44] = dvSel.Table;
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
            }
        }

        protected void cSelColumnId44_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId44.SelectedIndex >= 0)
            {
                if (cColSort.SelectedIndex >= 0) { cColSort.SelectedItem.Selected = false; }
                if (cSelColumnId44.SelectedItem.Text.IndexOf("[ASC]") >= 0)
                {
                    cColSort.Items.FindByValue("A").Selected = true;
                }
                else cColSort.Items.FindByValue("D").Selected = true;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId44.ClientID);
        }

        protected void cSelColumnId55_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && cSelColumnId55.SelectedIndex >= 0)
            {
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[0].ToString() == cSelColumnId55.SelectedValue)
                    {
                        if (drdSel[6].ToString() == "Y")	// Numeric.
                        {
                            cAggregate.Items.FindByValue("S").Enabled = true;
                            cAggregate.Items.FindByValue("A").Enabled = true;
                        }
                        else
                        {
                            cAggregate.Items.FindByValue("S").Enabled = false;
                            cAggregate.Items.FindByValue("A").Enabled = false;
                        }
                        break;
                    }
                }
                if (cAggregate.SelectedIndex >= 0) { cAggregate.SelectedItem.Selected = false; }
                if (cSelColumnId55.SelectedItem.Text.StartsWith("[SUM]")) { cAggregate.Items.FindByValue("S").Selected = true; }
                else if (cSelColumnId55.SelectedItem.Text.StartsWith("[AVG]")) { cAggregate.Items.FindByValue("A").Selected = true; }
                else if (cSelColumnId55.SelectedItem.Text.StartsWith("[MIN]")) { cAggregate.Items.FindByValue("I").Selected = true; }
                else if (cSelColumnId55.SelectedItem.Text.StartsWith("[MAX]")) { cAggregate.Items.FindByValue("M").Selected = true; }
                else if (cSelColumnId55.SelectedItem.Text.StartsWith("[CNT]")) { cAggregate.Items.FindByValue("C").Selected = true; }
                else cAggregate.Items.FindByValue("N").Selected = true;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId55.ClientID);
        }

        protected void cAggregate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && cSelColumnId55.SelectedIndex >= 0)
            {
                string str;
                int ii = cSelColumnId55.SelectedIndex;
                if (cAggregate.SelectedValue == "S") { str = "[SUM] "; }
                else if (cAggregate.SelectedValue == "A") { str = "[AVG] "; }
                else if (cAggregate.SelectedValue == "I") { str = "[MIN] "; }
                else if (cAggregate.SelectedValue == "M") { str = "[MAX] "; }
                else if (cAggregate.SelectedValue == "C") { str = "[CNT] "; }
                else { str = string.Empty; }
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[0].ToString() == cSelColumnId55.SelectedValue)
                    {
                        if (cSelColumnId55.SelectedItem.Text.IndexOf("] ") >= 0)
                        {
                            drdSel[2] = str + cSelColumnId55.SelectedItem.Text.Substring(cSelColumnId55.SelectedItem.Text.IndexOf("] ") + 2);
                        }
                        else
                        {
                            drdSel[2] = str + cSelColumnId55.SelectedItem.Text;
                        }
                        break;
                    }
                }
                cSelColumnId55.DataSource = dtSel.DefaultView; cSelColumnId55.DataBind(); cSelColumnId55.Items[ii].Selected = true; EnbSelCol55();
                Session[KEY_dtSelColumnId33] = dtSel;
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId55.ClientID);
            }
        }

        protected void cSelColumnId66_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId66.SelectedIndex >= 0)
            {
                if (cRptGroup.SelectedIndex >= 0) { cRptGroup.SelectedItem.Selected = false; }
                if (cSelColumnId66.SelectedItem.Text.StartsWith("[1]")) { cRptGroup.Items.FindByValue("1").Selected = true; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp1.gif"; cRptGroupImg.Visible = true; }
                else if (cSelColumnId66.SelectedItem.Text.StartsWith("[2]")) { cRptGroup.Items.FindByValue("2").Selected = true; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp2.gif"; cRptGroupImg.Visible = true; }
                else if (cSelColumnId66.SelectedItem.Text.StartsWith("[3]")) { cRptGroup.Items.FindByValue("3").Selected = true; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp3.gif"; cRptGroupImg.Visible = true; }
                else { cRptGroup.Items.FindByValue("0").Selected = true; cRptGroupImg.Visible = false; }
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId66.ClientID);
            }
        }

        protected void EnbSelCol55()
        {
            int ii = 0;
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            foreach (DataRow drdSel in dtSel.Rows)
            {
                if (drdSel[3].ToString().IndexOf("] ") >= 0)
                {
                    if (drdSel[2].ToString().IndexOf("] ") >= 0)
                    {
                        cSelColumnId55.Items[ii].Text = cSelColumnId55.Items[ii].Text.Substring(drdSel[2].ToString().IndexOf("] ") + 2);
                        drdSel[2] = drdSel[2].ToString().Substring(drdSel[2].ToString().IndexOf("] ") + 2);
                        Session[KEY_dtSelColumnId33] = dtSel;
                    }
                    cSelColumnId55.Items[ii].Enabled = false;
                }
                else
                {
                    cSelColumnId55.Items[ii].Enabled = true;
                }
                ii = ii + 1;
            }
        }

        protected void EnbRptGroup(object sender, System.EventArgs e)
        {
            bool bHasG1 = false;
            bool bHasG2 = false;
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && dtSel.Rows.Count > 0)
            {
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[3].ToString().StartsWith("[1]")) { bHasG1 = true; }
                    else if (drdSel[3].ToString().StartsWith("[2]")) { bHasG2 = true; }
                    if (bHasG1 && bHasG2) { break; }
                }
                if (bHasG1 && bHasG2) { cRptGroup.Items[2].Enabled = true; cRptGroup.Items[3].Enabled = true; }
                else if (bHasG1 && !bHasG2)
                {
                    cRptGroup.Items[2].Enabled = true; cRptGroup.Items[3].Enabled = false;
                    if (cRptGroup.Items[3].Selected) { cRptGroup.Items[3].Selected = false; cRptGroup.Items[2].Selected = true; cRptGroup_SelectedIndexChanged(sender, e); }
                }
                else
                {
                    cRptGroup.Items[2].Enabled = false;
                    cRptGroup.Items[3].Enabled = false;
                    if (cRptGroup.Items[2].Selected) { cRptGroup.Items[2].Selected = false; cRptGroup.Items[1].Selected = true; cRptGroup_SelectedIndexChanged(sender, e); }
                    if (cRptGroup.Items[3].Selected) { cRptGroup.Items[3].Selected = false; cRptGroup.Items[1].Selected = true; cRptGroup_SelectedIndexChanged(sender, e); }
                }
            }
        }

        protected void cRptGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && cSelColumnId66.SelectedIndex >= 0)
            {
                bool bHasGC = false;
                bool bHasG1 = false;
                bool bHasG2 = false;
                bool bHasG3 = false;
                bool bHasErr = false;
                string str;
                int ii = cSelColumnId66.SelectedIndex;
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if ((drdSel[0].ToString() == cSelColumnId66.SelectedValue && cRptGroup.SelectedValue == "1") || (drdSel[0].ToString() != cSelColumnId66.SelectedValue && drdSel[3].ToString().StartsWith("[1]")))
                    {
                        bHasG1 = true; if (bHasG2 || bHasG3 || bHasGC) { bHasErr = true; break; }
                    }
                    else if ((drdSel[0].ToString() == cSelColumnId66.SelectedValue && cRptGroup.SelectedValue == "2") || (drdSel[0].ToString() != cSelColumnId66.SelectedValue && drdSel[3].ToString().StartsWith("[2]")))
                    {
                        bHasG2 = true; if (bHasG3 || bHasGC) { bHasErr = true; break; }
                    }
                    else if ((drdSel[0].ToString() == cSelColumnId66.SelectedValue && cRptGroup.SelectedValue == "3") || (drdSel[0].ToString() != cSelColumnId66.SelectedValue && drdSel[3].ToString().StartsWith("[3]")))
                    {
                        bHasG3 = true; if (bHasGC) { bHasErr = true; break; }
                    }
                    else if ((drdSel[0].ToString() == cSelColumnId66.SelectedValue && cRptGroup.SelectedValue == "0") || (drdSel[0].ToString() != cSelColumnId66.SelectedValue && !drdSel[3].ToString().StartsWith("[")))
                    {
                        bHasGC = true;
                    }
                }
                if (bHasErr) { cSelColumnId66_SelectedIndexChanged(sender, e); }
                else
                {
                    foreach (DataRow drdSel in dtSel.Rows)
                    {
                        if (drdSel[0].ToString() == cSelColumnId66.SelectedValue)
                        {
                            if (cRptGroup.SelectedValue == "1") { str = "[1] "; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp1.gif"; cRptGroupImg.Visible = true; }
                            else if (cRptGroup.SelectedValue == "2") { str = "[2] "; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp2.gif"; cRptGroupImg.Visible = true; }
                            else if (cRptGroup.SelectedValue == "3") { str = "[3] "; cRptGroupImg.ImageUrl = "~/images/report/RptwizGrp3.gif"; cRptGroupImg.Visible = true; }
                            else { str = string.Empty; cRptGroupImg.Visible = false; }
                            if (cSelColumnId66.SelectedItem.Text.IndexOf("] ") >= 0)
                            {
                                drdSel[3] = str + cSelColumnId66.SelectedItem.Text.Substring(cSelColumnId66.SelectedItem.Text.IndexOf("] ") + 2);
                            }
                            else
                            {
                                drdSel[3] = str + cSelColumnId66.SelectedItem.Text;
                            }
                            break;
                        }
                    }
                    cSelColumnId66.DataSource = dtSel.DefaultView; cSelColumnId66.DataBind(); cSelColumnId66.Items[ii].Selected = true;
                    Session[KEY_dtSelColumnId33] = dtSel; EnbRptGroup(sender, e);
                    if (Tab55.Visible) { EnbSelCol55(); }
                }
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId66.ClientID);
            }
        }

        protected void cSelColumnId88_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId88.SelectedIndex >= 0)
            {
                if (cRptChart.SelectedIndex >= 0) { cRptChart.SelectedItem.Selected = false; }
                if (cSelColumnId88.SelectedItem.Text.StartsWith("[C]")) { cRptChart.Items.FindByValue("C").Selected = true; }
                else if (cSelColumnId88.SelectedItem.Text.StartsWith("[V]")) { cRptChart.Items.FindByValue("V").Selected = true; }
                else if (cSelColumnId88.SelectedItem.Text.StartsWith("[S]")) { cRptChart.Items.FindByValue("S").Selected = true; }
                else { cRptChart.Items.FindByValue("0").Selected = true; }
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId88.ClientID);
            }
        }

        protected void EnbRptChart(object sender, System.EventArgs e)
        {
            bool bHasC = false;
            bool bHasV = false;
            bool bHasS = false;
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && dtSel.Rows.Count > 0)
            {
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[4].ToString().StartsWith("[C]")) { bHasC = true; }
                    else if (drdSel[4].ToString().StartsWith("[V]")) { bHasV = true; }
                    else if (drdSel[4].ToString().StartsWith("[S]")) { bHasS = true; }
                    if (bHasC && bHasV && bHasS) { break; }
                }
                if (bHasC) { cRptChart.Items[1].Enabled = false; } else { cRptChart.Items[1].Enabled = true; }
                if (bHasV) { cRptChart.Items[2].Enabled = false; } else { cRptChart.Items[2].Enabled = true; }
                if (bHasS) { cRptChart.Items[3].Enabled = false; } else { cRptChart.Items[3].Enabled = true; }
            }
        }

        protected void cRptChart_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId33];
            if (dtSel != null && cSelColumnId88.SelectedIndex >= 0)
            {
                string str;
                int ii = cSelColumnId88.SelectedIndex;
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[0].ToString() == cSelColumnId88.SelectedValue)
                    {
                        if (cRptChart.SelectedValue == "C") { str = "[C] "; }
                        else if (cRptChart.SelectedValue == "V") { str = "[V] "; }
                        else if (cRptChart.SelectedValue == "S") { str = "[S] "; }
                        else { str = string.Empty; }
                        if (cSelColumnId88.SelectedItem.Text.StartsWith("[C]") || cSelColumnId88.SelectedItem.Text.StartsWith("[V]") || cSelColumnId88.SelectedItem.Text.StartsWith("[S]"))
                        {
                            drdSel[4] = str + cSelColumnId88.SelectedItem.Text.Substring(cSelColumnId88.SelectedItem.Text.IndexOf("] ") + 2);
                        }
                        else
                        {
                            drdSel[4] = str + cSelColumnId88.SelectedItem.Text;
                        }
                        break;
                    }
                }
                cSelColumnId88.DataSource = dtSel.DefaultView; cSelColumnId88.DataBind(); cSelColumnId88.Items[ii].Selected = true;
                Session[KEY_dtSelColumnId33] = dtSel; EnbRptChart(sender, e);
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId88.ClientID);
            }
        }

        protected void cSelect77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId77];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvOri != null && dvSel != null)
            {
                int ii = 0;
                DataRow dr = null;
                foreach (DataRowView drvOri in dvOri)
                {
                    if (cOriColumnId77.Items[ii].Selected)
                    {
                        dr = dvSel.Table.NewRow(); dvSel.Table.Rows.Add(dr);
                        dr["ColumnId77"] = dvSel.Count;						// Identity
                        dr["ColumnId77Text"] = "[" + cOperator.SelectedValue + "] " + cOriColumnId77.Items[ii].Text;
                        dr["ColumnId"] = cOriColumnId77.Items[ii].Value;		// ColumnId
                        dr["OperatorName"] = cOperator.SelectedValue;			// OperatorName
                        dr["NumericData"] = drvOri[2].ToString();				// NumericData
                        cOriColumnId77.Items[ii].Selected = false;
                    }
                    if (ii < cOriColumnId77.Items.Count - 1) { ii = ii + 1; }
                }
                cSelColumnId77.DataSource = dvSel; cSelColumnId77.DataBind(); Session[KEY_dtSelColumnId77] = dvSel.Table;
                if (cSelColumnId77.Items.Count > 0) { cSelColumnId77.Items[dvSel.Count - 1].Selected = true; cSelColumnId77_SelectedIndexChanged(sender, new EventArgs()); }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cRemove77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtOri = (DataTable)Session[KEY_dtOriColumnId77];
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataTable dtDel = (DataTable)Session[KEY_dtDelColumnId];
            DataView dvOri = dtOri != null ? dtOri.DefaultView : null;
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            DataView dvDel = dtDel != null ? dtDel.DefaultView : null;
            if (dvOri != null && dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                int ii = 1;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)	// dvSel.Delete does not work.
                {
                    if (drvSel["ColumnId77"].ToString() != cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr["ColumnId77"] = ii;
                        dr["ColumnId77Text"] = drvSel["ColumnId77Text"].ToString();
                        dr["ColumnId"] = drvSel["ColumnId"].ToString();
                        dr["OperatorName"] = drvSel["OperatorName"].ToString();
                        dr["NumericData"] = drvSel["NumericData"].ToString();
                        if (drvSel["RptwizDtlId"].ToString() != string.Empty) { dr["RptwizDtlId"] = drvSel["RptwizDtlId"].ToString(); }
                        ii = ii + 1;
                    }
                    else if (drvSel["RptwizDtlId"].ToString() != string.Empty)
                    {
                        dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                        dr["RptwizDtlId184"] = drvSel["RptwizDtlId"]; Session[KEY_dtDelColumnId] = dvDel.Table;
                    }
                }
                cSelColumnId77.DataSource = dv; cSelColumnId77.DataBind(); Session[KEY_dtSelColumnId77] = dv.Table;
                if (cSelColumnId77.Items.Count > 0) { cSelColumnId77.Items[dv.Count - 1].Selected = true; cSelColumnId77_SelectedIndexChanged(sender, new EventArgs()); }
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cTop77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() == cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                    }
                }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                    }
                }
                cSelColumnId77.DataSource = dv; cSelColumnId77.DataBind(); cSelColumnId77.Items[0].Selected = true;
                Session[KEY_dtSelColumnId77] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cUp77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId77.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just before the row to be moved up:
                string d0 = string.Empty;
                string d1 = string.Empty;
                string d2 = string.Empty;
                string d3 = string.Empty;
                string d4 = string.Empty;
                string d5 = string.Empty;
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() == cSelColumnId77.SelectedValue) { break; }
                    d0 = drvSel[0].ToString(); d1 = drvSel[1].ToString(); d2 = drvSel[2].ToString(); d3 = drvSel[3].ToString(); d4 = drvSel[4].ToString(); d5 = drvSel[5].ToString();
                }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != d0)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                        if (drvSel[0].ToString() == cSelColumnId77.SelectedValue && d0 != string.Empty)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = d0; dr[1] = d1; dr[2] = d2; dr[3] = d3; dr[4] = d4;
                            if (d5 != string.Empty) { dr[5] = d5; }
                        }
                    }
                }
                cSelColumnId77.DataSource = dv; cSelColumnId77.DataBind(); cSelColumnId77.Items.FindByValue(keyId).Selected = true;
                Session[KEY_dtSelColumnId77] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cDown77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId77.SelectedValue;
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                // Find the row just after the row to be moved down:
                string d0 = string.Empty;
                string k1 = string.Empty;
                string k2 = string.Empty;
                string k3 = string.Empty;
                string k4 = string.Empty;
                string k5 = string.Empty;
                bool bfound = false;
                foreach (DataRowView drvSel in dvSel)
                {
                    if (bfound) { d0 = drvSel[0].ToString(); break; }
                    if (drvSel[0].ToString() == cSelColumnId77.SelectedValue)
                    {
                        bfound = true;
                        k1 = drvSel[1].ToString(); k2 = drvSel[2].ToString(); k3 = drvSel[3].ToString(); k4 = drvSel[4].ToString(); k5 = drvSel[5].ToString();
                    }
                }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                        if (drvSel[0].ToString() == d0)
                        {
                            dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                            dr[0] = keyId; dr[1] = k1; dr[2] = k2; dr[3] = k3; dr[4] = k4;
                            if (k5 != string.Empty) { dr[5] = k5; }
                        }
                    }
                }
                if (d0 == string.Empty)
                {
                    dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                    dr[0] = keyId; dr[1] = k1; dr[2] = k2; dr[3] = k3; dr[4] = k4; if (k5 != string.Empty) { dr[5] = k5; }
                }
                cSelColumnId77.DataSource = dv; cSelColumnId77.DataBind(); cSelColumnId77.Items.FindByValue(keyId).Selected = true;
                Session[KEY_dtSelColumnId77] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cBottom77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                DataRow dr = null;
                DataView dv = new DataView(dvSel.Table.Clone());
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() != cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                    }
                }
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() == cSelColumnId77.SelectedValue)
                    {
                        dr = dv.Table.NewRow(); dv.Table.Rows.Add(dr);
                        dr[0] = drvSel[0].ToString(); dr[1] = drvSel[1].ToString(); dr[2] = drvSel[2].ToString(); dr[3] = drvSel[3].ToString(); dr[4] = drvSel[4].ToString();
                        if (drvSel[5].ToString() != string.Empty) { dr[5] = drvSel[5].ToString(); }
                    }
                }
                cSelColumnId77.DataSource = dv; cSelColumnId77.DataBind(); cSelColumnId77.Items[dvSel.Count - 1].Selected = true;
                Session[KEY_dtSelColumnId77] = dv.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        protected void cSelButton77_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            DataView dvSel = dtSel != null ? dtSel.DefaultView : null;
            if (dvSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                string keyId = cSelColumnId77.SelectedValue;
                foreach (DataRowView drvSel in dvSel)
                {
                    if (drvSel[0].ToString() == cSelColumnId77.SelectedValue)
                    {
                        if (cSelChange77.Text == string.Empty)
                        {
                            drvSel[1] = "[" + cOperator.SelectedValue + "] " + cSelColumnId77.SelectedItem.Text;
                        }
                        else
                        {
                            drvSel[1] = "[" + cOperator.SelectedValue + "] " + cSelChange77.Text;
                        }
                        break;
                    }
                }
                cSelColumnId77.DataSource = dvSel; cSelColumnId77.DataBind(); cSelColumnId77.Items.FindByValue(keyId).Selected = true;
                Session[KEY_dtSelColumnId77] = dvSel.Table;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

        private void SetOriColumnId77(ListBox ddl, string keyId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtOriColumnId77];
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dt == null)
                    {
                        dt = (new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, bAll, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                    }
                    if (dt != null)
                    {
                        ddl.DataSource = dt.DefaultView; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dt.Rows.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dt = (new WebRule()).GetDdlOriColumnId33S1682(cRptwizCatId183.SelectedValue, true, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                            ddl.DataSource = dt.DefaultView; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtOriColumnId77] = dt;
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        private void SetSelColumnId77(ListBox ddl, string keyId, string filterId)
        {
            try
            {
                DataTable dt = (DataTable)Session[KEY_dtSelColumnId77];
                if (ddl != null)
                {
                    ListItem li = null;
                    bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                    if (dt == null)
                    {
                        dt = (new WebRule()).GetDdlSelColumnId77S1682(95, bAll, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                    }
                    if (dt != null)
                    {
                        ddl.DataSource = dt.DefaultView; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                        if (dt.Rows.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                        {
                            dt = (new WebRule()).GetDdlSelColumnId77S1682(95, true, keyId, filterId, (string)Session[KEY_sysConnectionString], LcAppPw, base.LImpr, base.LCurr, base.LUser.CultureId);
                            ddl.DataSource = dt.Rows; ddl.DataBind();
                            li = ddl.Items.FindByValue(string.Empty); if (li != null) { li.Selected = true; }
                        }
                        Session[KEY_dtSelColumnId77] = dt;
                    }
                }
            }
            catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        }

        protected void cOriColumnId77_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cSelColumnId77.SelectedIndex >= 0) { cSelColumnId77.SelectedItem.Selected = false; }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cOriColumnId77.ClientID);
        }

        protected void cOperator_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            if (dtSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                int ii = cSelColumnId77.SelectedIndex;
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel["ColumnId77"].ToString() == cSelColumnId77.SelectedValue)
                    {
                        drdSel["OperatorName"] = cOperator.SelectedValue;
                        drdSel["ColumnId77Text"] = "[" + cOperator.SelectedValue + "] " + cSelColumnId77.SelectedItem.Text.Substring(cSelColumnId77.SelectedItem.Text.IndexOf("] ") + 2);
                        break;
                    }
                }
                cSelColumnId77.DataSource = dtSel.DefaultView; cSelColumnId77.DataBind(); cSelColumnId77.Items[ii].Selected = true;
                Session[KEY_dtSelColumnId77] = dtSel;
                ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
            }
        }

        protected void cSelColumnId77_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dtSel = (DataTable)Session[KEY_dtSelColumnId77];
            if (dtSel != null && cSelColumnId77.SelectedIndex >= 0)
            {
                cSelChange77.Text = cSelColumnId77.SelectedItem.Text.Substring(cSelColumnId77.SelectedItem.Text.IndexOf("] ") + 2);
                if (cOperator.SelectedIndex >= 0) { cOperator.SelectedItem.Selected = false; }
                foreach (DataRow drdSel in dtSel.Rows)
                {
                    if (drdSel[0].ToString() == cSelColumnId77.SelectedValue)
                    {
                        if (drdSel[4].ToString() == "Y")	// NumericData.
                        {
                            cOperator.Items.FindByValue("like").Enabled = false;
                            cOperator.Items.FindByValue("in").Enabled = false;
                            cOperator.Items.FindByValue("not like").Enabled = false;
                            cOperator.Items.FindByValue("not in").Enabled = false;
                        }
                        else
                        {
                            cOperator.Items.FindByValue("like").Enabled = true;
                            cOperator.Items.FindByValue("in").Enabled = true;
                            cOperator.Items.FindByValue("not like").Enabled = true;
                            cOperator.Items.FindByValue("not in").Enabled = true;
                        }
                        break;
                    }
                }
                cOperator.Items.FindByValue(cSelColumnId77.SelectedItem.Text.Substring(1, cSelColumnId77.SelectedItem.Text.IndexOf("] ") - 1)).Selected = true;
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSelColumnId77.ClientID);
        }

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
            CheckAuthentication(pageLoad, true);
        }

		private void SetButtonHlp()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetButtonHlp(95,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
                        if (dr["ButtonTypeName"].ToString() == "Save") { cSaveButton.CssClass = "ButtonImg SaveButtonImg"; cSaveButton.Text = dr["ButtonName"].ToString(); cSaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bUpdateVisible] = cSaveButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "Preview") { cPreviewButton.CssClass = "ButtonImg PreviewButtonImg"; cPreviewButton.Text = dr["ButtonName"].ToString(); cPreviewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPreviewButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bPreviewVisible] = cPreviewButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "New") { cNewButton.CssClass = "ButtonImg NewButtonImg"; cNewButton.Text = dr["ButtonName"].ToString(); cNewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNewButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bNewVisible] = cNewButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "NewSave") { cNewSaveButton.CssClass = "ButtonImg NewSaveButtonImg"; cNewSaveButton.Text = dr["ButtonName"].ToString(); cNewSaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNewSaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bNewSaveVisible] = cNewSaveButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "Copy") { cCopyButton.CssClass = "ButtonImg CopyButtonImg"; cCopyButton.Text = dr["ButtonName"].ToString(); cCopyButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cCopyButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bCopyVisible] = cCopyButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "CopySave") { cCopySaveButton.CssClass = "ButtonImg CopySaveButtonImg"; cCopySaveButton.Text = dr["ButtonName"].ToString(); cCopySaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cCopySaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bCopySaveVisible] = cCopySaveButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "Delete") { cDeleteButton.CssClass = "ButtonImg DeleteButtonImg"; cDeleteButton.Text = dr["ButtonName"].ToString(); cDeleteButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cDeleteButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bDeleteVisible] = cDeleteButton.Visible; }
                        else if (dr["ButtonTypeName"].ToString() == "More") { cMoreButton.CssClass = "ButtonImg MoreButtonImg"; cMoreButton.Text = dr["ButtonName"].ToString(); cMoreButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cMoreButton.ToolTip = dr["ButtonToolTip"].ToString(); }
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
					dtRul = (new AdminSystem()).GetClientRule(95,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtClientRule] = dtRul;
			}
			return dtRul;
		}

		private void SetClientRule(DataGridItem ee)
		{
			DataView dvRul = new DataView(GetClientRule());
            if (ee != null) { dvRul.RowFilter = "MasterTable = 'N'"; } else { dvRul.RowFilter = "MasterTable <> 'N'"; }
            if (dvRul.Count > 0)
			{
				WebControl cc = null;
				string srp = string.Empty;
				string sn = string.Empty;
				string st = string.Empty;
				int ii = 0;
				foreach (DataRowView drv in dvRul)
				{
                    if (ee == null || drv["ScriptEvent"].ToString().Substring(0, 2).ToLower() != "on")
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
								if (ee != null)
								{
									if (st.ToLower() == "combobox") {srp = srp.Replace("@" + ii.ToString() + "@",((RoboCoder.WebControls.ComboBox)ee.FindControl(sn)).FocusID);} else {srp = srp.Replace("@" + ii.ToString() + "@",((WebControl)ee.FindControl(sn)).ClientID);}
								}
								else
								{
									if (st.ToLower() == "combobox") {srp = srp.Replace("@" + ii.ToString() + "@",((RoboCoder.WebControls.ComboBox)this.FindControl(sn)).FocusID);} else {srp = srp.Replace("@" + ii.ToString() + "@",((WebControl)this.FindControl(sn)).ClientID);}
								}
								ii = ii + 1;
							}
						}
						if (ee != null) {cc = ee.FindControl(drv["ColName"].ToString()) as WebControl;} else {cc = this.FindControl(drv["ColName"].ToString()) as WebControl;}
						if (cc != null && (cc.Attributes[drv["ScriptEvent"].ToString()] == null || cc.Attributes[drv["ScriptEvent"].ToString()].IndexOf(srp) < 0)) {cc.Attributes[drv["ScriptEvent"].ToString()] += srp;}
						if (ee != null && drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on")
						{
							cc = ee.FindControl(drv["ColName"].ToString() + "l") as WebControl;
							if (cc != null && (cc.Attributes[drv["ScriptEvent"].ToString()] == null || cc.Attributes[drv["ScriptEvent"].ToString()].IndexOf(srp) < 0)) {cc.Attributes[drv["ScriptEvent"].ToString()] += srp;}
						}
					}
				}
			}
		}

		private DataTable GetScreenHlp()
		{
			DataTable dtHlp = (DataTable)Session[KEY_dtScreenHlp];
			if (dtHlp == null)
			{
				CheckAuthentication(false);
				try
				{
					dtHlp = (new AdminSystem()).GetScreenHlp(95,base.LUser.CultureId,LcSysConnString,LcAppPw);
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
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,95,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					cGlobalFilter.Text = dt.Rows[0]["FilterDesc"].ToString();
					cGlobalFilter.Visible = true;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

        //private void GetScreenFilter()
        //{
        //    try
        //    {
        //        DataTable dt = (new AdminSystem()).GetScreenFilter(95,base.LUser.CultureId,LcSysConnString,LcAppPw);
        //        if (dt != null)
        //        {
        //            cFilterId.DataSource = dt;
        //            cFilterId.DataBind();
        //            if (cFilterId.Items.Count > 0)
        //            {
        //                if (Request.QueryString["ftr"] != null) {cFilterId.Items.FindByValue(Request.QueryString["ftr"]).Selected = true;} else {cFilterId.Items[0].Selected = true;}
        //                cFilterLabel.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "QFilter", "QFilter", null, null, null);
        //                cFilter.Visible = true;
        //            }
        //        }
        //    }
        //    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
        //}

		private void GetSystems()
		{
			Session[KEY_sysConnectionString] = LcSysConnString;
			DataTable dtSystems = base.SystemsList;
			if (dtSystems != null)
			{
				Session[KEY_dtSystems] = dtSystems;
				cSystemId.DataSource = dtSystems;
				cSystemId.DataBind();
				if (cSystemId.Items.Count > 0)
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

		private DataTable GetLabel()
		{
			DataTable dt = (DataTable)Session[KEY_dtLabel];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetScreenLabel(95,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
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
					dt = (new AdminSystem()).GetAuthCol(95,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthCol] = dt;
			}
			return dt;
		}

		private DataTable GetAuthRow()
		{
			DataTable dt = (DataTable)Session[KEY_dtAuthRow];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetAuthRow(95,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthRow] = dt;
			}
			return dt;
		}

		public void cRptwizCatId183Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DropDownList cc = cRptwizCatId183; Session["CtrlAdmDataCat"] = cc.ClientID;
		}

		public void cRptwizCatId183Search_Script()
		{
				ImageButton ib = cRptwizCatId183Search;
				if (ib != null)
				{
			    	TextBox pp = cRptwizId183;
					DropDownList cc = cRptwizCatId183;
					//string ss = "&dsp=DropDownList"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					//string js = "document.getElementById('" + bConfirm.ClientID + "').value='N'; var w = window.open('AdmDataCat.aspx?col=RptwizCatId" + ss + "&oid=1638&typ=" + base.SetBool(cc.Enabled) + "&frm=aspnetForm&pkey=" + pp.Text + "&key=" + cc.SelectedValue + "','CtrlAdmDataCat','resizable=yes,scrollbars=yes,status=yes,width=780,height=550'); if (w) {w.focus();}";
                    //ib.Attributes["onclick"] = js;
                    string ss = "&dsp=DropDownList"; if (cSystem.Visible) { ss = ss + "&sys=" + cSystemId.SelectedValue; } else { ss = ss + "&sys=3"; }
                    ib.Attributes["onclick"] = "SearchLink('AdmDataCat.aspx?col=RptwizCatId" + ss + "&typ=N" + "','" + cc.ClientID + "','',''); return false;";
                }
		}

		private DataTable GetScreenTab()
		{
			CheckAuthentication(false);
			DataTable dtScreenTab = null;
			try
			{
				dtScreenTab = (new AdminSystem()).GetScreenTab(95,base.LUser.CultureId,LcSysConnString,LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return dtScreenTab;
		}

        private void SetReportId183(DropDownList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtReportId183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                string ss = string.Empty;
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    try
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlReportId3S1672", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                }
                if (dv != null)
                {
                    if (dv.Table.Columns.Contains("RptwizId"))
                    {
                        ss = "(RptwizId is null";
                        if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) { ss = ss + ")"; } else { ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")"; }
                    }
                    dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlReportId3S1672", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlReportId3S1672", true, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    Session[KEY_dtReportId183] = dv.Table;
                }
            }
        }

        private void SetAccessCd183(RadioButtonList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtAccessCd183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                string ss = string.Empty;
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    try
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlAccessCd3S1673", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                }
                if (dv != null)
                {
                    if (dv.Table.Columns.Contains("RptwizId"))
                    {
                        ss = "(RptwizId is null";
                        if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) { ss = ss + ")"; } else { ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")"; }
                    }
                    dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlAccessCd3S1673", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlAccessCd3S1673", false, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    Session[KEY_dtAccessCd183] = dv.Table;
                }
            }
        }

        private void SetUsrId183(DropDownList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtUsrId183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                string ss = string.Empty;
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    try
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUsrId3S1641", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                }
                if (dv != null)
                {
                    if (dv.Table.Columns.Contains("RptwizId"))
                    {
                        ss = "(RptwizId is null";
                        if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) { ss = ss + ")"; } else { ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")"; }
                    }
                    dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUsrId3S1641", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUsrId3S1641", true, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    Session[KEY_dtUsrId183] = dv.Table;
                }
            }
        }

        private void SetOrientationCd183(RadioButtonList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtOrientationCd183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                string ss = string.Empty;
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    try
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlOrientationCd3S1674", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                }
                if (dv != null)
                {
                    if (dv.Table.Columns.Contains("RptwizId"))
                    {
                        ss = "(RptwizId is null";
                        if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) { ss = ss + ")"; } else { ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")"; }
                    }
                    dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlOrientationCd3S1674", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlOrientationCd3S1674", false, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    Session[KEY_dtOrientationCd183] = dv.Table;
                }
            }
        }

        private void SetGPositive183(RadioButtonList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtGPositive183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlGPositive3S1637", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                }
                if (dv != null)
                {
                    ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId);
                    if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlGPositive3S1637", false, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        ddl.DataSource = dv; ddl.DataBind(); li = ddl.Items.FindByValue(keyId);
                        if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 0 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlGPositive3S1637", false, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        ddl.DataSource = dv; ddl.DataBind(); li = ddl.Items.FindByValue(string.Empty);
                        if (li != null) { li.Selected = true; }
                    }
                    if (ddl.SelectedIndex < 0) { ddl.Items[0].Selected = true; }
                    Session[KEY_dtGPositive183] = dv.Table;
                }
            }
        }

        private void SetUnitCd183(DropDownList ddl, string keyId)
        {
            DataTable dt = (DataTable)Session[KEY_dtUnitCd183];
            DataView dv = dt != null ? dt.DefaultView : null;
            if (ddl != null)
            {
                string ss = string.Empty;
                ListItem li = null;
                bool bFirst = false;
                bool bAll = false; if (ddl.Enabled && ddl.Visible) { bAll = true; }
                if (dv == null)
                {
                    bFirst = true;
                    try
                    {
                        dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUnitCd3S1679", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                    }
                    catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                }
                if (dv != null)
                {
                    if (dv.Table.Columns.Contains("RptwizId"))
                    {
                        ss = "(RptwizId is null";
                        if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) { ss = ss + ")"; } else { ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")"; }
                    }
                    dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                    li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    else if (!bFirst)
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUnitCd3S1679", true, bAll, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
                    {
                        try
                        {
                            dv = new DataView((new AdminSystem()).GetDdl(95, "GetDdlUnitCd3S1679", true, true, 0, keyId, (string)Session[KEY_sysConnectionString], LcAppPw, string.Empty, base.LImpr, base.LCurr));
                        }
                        catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
                        dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
                        li = ddl.Items.FindByValue(keyId); if (li != null) { li.Selected = true; }
                    }
                    Session[KEY_dtUnitCd183] = dv.Table;
                }
            }
        }

		private void SetRptwizTypeCd183(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtRptwizTypeCd183];
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
						dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizTypeCd3S1637",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("RptwizId"))
					{
						ss = "(RptwizId is null";
						if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")";}
					}
					dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizTypeCd3S1637",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizTypeCd3S1637",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtRptwizTypeCd183] = dv.Table;
				}
			}
		}

		private void SetRptwizCatId183(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtRptwizCatId183];
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
						dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizCatId3S1638",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("RptwizId"))
					{
						ss = "(RptwizId is null";
						if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")";}
					}
					dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizCatId3S1638",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptwizCatId3S1638",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtRptwizCatId183] = dv.Table;
				}
			}
		}

		private void SetRptChaTypeCd183(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtRptChaTypeCd183];
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
						dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptChaTypeCd3S1739",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("RptwizId"))
					{
						ss = "(RptwizId is null";
						if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")";}
					}
					dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptChaTypeCd3S1739",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlRptChaTypeCd3S1739",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtRptChaTypeCd183] = dv.Table;
				}
			}
		}

		private void SetGFormat183(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtGFormat183];
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
						dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlGFormat3S1794",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (dv.Table.Columns.Contains("RptwizId"))
					{
						ss = "(RptwizId is null";
						if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue)) {ss = ss + ")";} else {ss = ss + " OR RptwizId = " + cAdmRptWiz95List.SelectedValue + ")";}
					}
					dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlGFormat3S1794",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(95,"GetDdlGFormat3S1794",true,true,0,keyId,(string)Session[KEY_sysConnectionString],LcAppPw,string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtGFormat183] = dv.Table;
				}
			}
		}

		private DataView GetAdmRptWiz95List()
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptWiz95List];
			if (dt == null)
			{
				CheckAuthentication(false);
				int filterId = 0; //if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
				string key = string.Empty;
				if (Request.QueryString["key"] != null && bUseCri.Value == string.Empty) { key = Request.QueryString["key"].ToString(); }
				try
				{
                    try { dt = (new AdminSystem()).GetLis(95, "GetLisAdmRptWiz95", true, "Y", 0, (string)Session[KEY_sysConnectionString], LcAppPw, filterId, key, string.Empty, null, base.LImpr, base.LCurr, null); }
                    catch { dt = (new AdminSystem()).GetLis(95, "GetLisAdmRptWiz95", true, "N", 0, (string)Session[KEY_sysConnectionString], LcAppPw, filterId, key, string.Empty, null, base.LImpr, base.LCurr, null); }
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return new DataView(); }
				Session[KEY_dtAdmRptWiz95List] = SetFunctionality(dt);
			}
			return (new DataView(dt,"","",System.Data.DataViewRowState.CurrentRows));
		}

		private void PopAdmRptWiz95List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataView dv = GetAdmRptWiz95List();
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetLisAdmRptWiz95";
			context["mKey"] = "RptwizId183";
			context["mVal"] = "RptwizId183Text";
			context["mTip"] = "RptwizId183Text";
			context["mImg"] = "RptwizId183Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "95";
			context["csy"] = "3";
			//context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
            context["filter"] = "0";
            context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			cAdmRptWiz95List.AutoCompleteUrl = "AutoComplete.aspx/LisSuggests";
			cAdmRptWiz95List.DataContext = context;
			if (dv.Table == null) return;
			cAdmRptWiz95List.Mode = "A";
			cAdmRptWiz95List.DataSource = dv;
			cAdmRptWiz95List.Visible = true;
			if (cAdmRptWiz95List.Items.Count <= 0) {cAdmRptWiz95List.Visible = false; cAdmRptWiz95List_SelectedIndexChanged(sender,e);}
			else
			{
				if (cAdmRptWiz95List.Items.Count == 1)
				{
					cAdmRptWiz95List.Items[0].Selected = true; cAdmRptWiz95List.Visible = false; cAdmRptWiz95List_SelectedIndexChanged(sender,e);
				}
				else
				{
					if (bPageLoad && Request.QueryString["key"] != null && bUseCri.Value == string.Empty)
					{
						try {cAdmRptWiz95List.SelectByValue(Request.QueryString["key"],string.Empty,true);} catch {cAdmRptWiz95List.Items[0].Selected = true; cAdmRptWiz95List_SelectedIndexChanged(sender, e);}
					}
					else
					{
						if (ID != null) {if (!cAdmRptWiz95List.SelectByValue(ID,string.Empty,true)) {ID = null;}}
						if (ID == null)
						{
							if (cAdmRptWiz95List.Items[0].Value == string.Empty || cAdmRptWiz95List.IsDdlVisible)
							{
								cAdmRptWiz95List.tBox_TextChanged(sender, e);
							}
							else {cAdmRptWiz95List_SelectedIndexChanged(sender, e);}
						}
					}
				}
			}
		}

		private void SetColumnAuthority()
		{
			DataTable dtAuth = GetAuthCol();
			DataTable dtLabel = GetLabel();
			cTabFolder.Visible = false;
			foreach (DataRow dr in dtAuth.Rows) {if (dr["MasterTable"].ToString() == "Y" && dr["ColVisible"].ToString() == "Y") {cTabFolder.Visible = true; break;}}
			if (dtAuth != null && dtLabel != null)
			{
				base.SetFoldBehavior(cRptwizId183, dtAuth.Rows[0], cRptwizId183P1, cRptwizId183Label, cRptwizId183P2, null, dtLabel.Rows[0], null, null, null);
				base.SetFoldBehavior(cRptwizName183, dtAuth.Rows[1], cRptwizName183P1, cRptwizName183Label, cRptwizName183P2, null, dtLabel.Rows[1], cRFVRptwizName183, null, null);
				base.SetFoldBehavior(cRptwizDesc183, dtAuth.Rows[2], cRptwizDesc183P1, cRptwizDesc183Label, cRptwizDesc183P2, null, dtLabel.Rows[2], cRFVRptwizDesc183, null, null);
				cRptwizDesc183E.Attributes["label_id"] = cRptwizDesc183Label.ClientID; cRptwizDesc183E.Attributes["target_id"] = cRptwizDesc183.ClientID;
				base.SetFoldBehavior(cRptwizTypeCd183, dtAuth.Rows[3], cRptwizTypeCd183P1, cRptwizTypeCd183Label, cRptwizTypeCd183P2, null, dtLabel.Rows[3], cRFVRptwizTypeCd183, null, null);
				base.SetFoldBehavior(cRptCtrTypeDesc157, dtAuth.Rows[4], cRptCtrTypeDesc157P1, cRptCtrTypeDesc157Label, cRptCtrTypeDesc157P2, null, dtLabel.Rows[4], null, null, null);
				cRptCtrTypeDesc157E.Attributes["label_id"] = cRptCtrTypeDesc157Label.ClientID; cRptCtrTypeDesc157E.Attributes["target_id"] = cRptCtrTypeDesc157.ClientID;
				base.SetFoldBehavior(cTemplateName183, dtAuth.Rows[5], cTemplateName183P1, cTemplateName183Label, cTemplateName183P2, null, dtLabel.Rows[5], null, null, null);
				base.SetFoldBehavior(cRptwizCatId183, dtAuth.Rows[6], cRptwizCatId183P1, cRptwizCatId183Label, cRptwizCatId183P2, null, dtLabel.Rows[6], cRFVRptwizCatId183, null, null);
				base.SetFoldBehavior(cCatDescription181, dtAuth.Rows[7], cCatDescription181P1, cCatDescription181Label, cCatDescription181P2, null, dtLabel.Rows[7], null, null, null);
				cCatDescription181E.Attributes["label_id"] = cCatDescription181Label.ClientID; cCatDescription181E.Attributes["target_id"] = cCatDescription181.ClientID;
				base.SetFoldBehavior(cAccessCd183, dtAuth.Rows[8], cAccessCd183P1, cAccessCd183Label, cAccessCd183P2, null, dtLabel.Rows[8], null, null, null);
				base.SetFoldBehavior(cUsrId183, dtAuth.Rows[9], cUsrId183P1, cUsrId183Label, cUsrId183P2, null, dtLabel.Rows[9], null, null, null);
				base.SetFoldBehavior(cReportId183, dtAuth.Rows[10], cReportId183P1, cReportId183Label, cReportId183P2, null, dtLabel.Rows[10], null, null, null);
				base.SetFoldBehavior(cRptwizCatImg181, dtAuth.Rows[11], null, null, null, dtLabel.Rows[11], null, null, null);
				base.SetFoldBehavior(cOrientationCd183, dtAuth.Rows[12], cOrientationCd183P1, cOrientationCd183Label, cOrientationCd183P2, null, dtLabel.Rows[12], null, null, null);
				base.SetFoldBehavior(cUnitCd183, dtAuth.Rows[13], cUnitCd183P1, cUnitCd183Label, cUnitCd183P2, null, dtLabel.Rows[13], cRFVUnitCd183, null, null);
				base.SetFoldBehavior(cTopMargin183, dtAuth.Rows[14], cTopMargin183P1, cTopMargin183Label, cTopMargin183P2, null, dtLabel.Rows[14], cRFVTopMargin183, null, null);
				base.SetFoldBehavior(cBottomMargin183, dtAuth.Rows[15], cBottomMargin183P1, cBottomMargin183Label, cBottomMargin183P2, null, dtLabel.Rows[15], cRFVBottomMargin183, null, null);
				base.SetFoldBehavior(cLeftMargin183, dtAuth.Rows[16], cLeftMargin183P1, cLeftMargin183Label, cLeftMargin183P2, null, dtLabel.Rows[16], cRFVLeftMargin183, null, null);
				base.SetFoldBehavior(cRightMargin183, dtAuth.Rows[17], cRightMargin183P1, cRightMargin183Label, cRightMargin183P2, null, dtLabel.Rows[17], cRFVRightMargin183, null, null);
				base.SetFoldBehavior(cOriColumnId33, dtAuth.Rows[18], cOriColumnId33P1, cOriColumnId33Label, null, dtLabel.Rows[18], null, null, null);
				base.SetFoldBehavior(cSelColumnId33, dtAuth.Rows[19], cSelColumnId33P1, cSelColumnId33Label, null, dtLabel.Rows[19], null, null, null);
				base.SetFoldBehavior(cSelChange33, dtAuth.Rows[20], cSelChange33P1, cSelChange33Label, null, dtLabel.Rows[20], null, null, null);
				base.SetFoldBehavior(cOriColumnId44, dtAuth.Rows[21], cOriColumnId44P1, cOriColumnId44Label, null, dtLabel.Rows[21], null, null, null);
				base.SetFoldBehavior(cSelColumnId44, dtAuth.Rows[22], cSelColumnId44P1, cSelColumnId44Label, null, dtLabel.Rows[22], null, null, null);
				base.SetFoldBehavior(cSelColumnId55, dtAuth.Rows[23], cSelColumnId55P1, cSelColumnId55Label, null, dtLabel.Rows[23], null, null, null);
				base.SetFoldBehavior(cSelColumnId66, dtAuth.Rows[25], cSelColumnId66P1, cSelColumnId66Label, null, dtLabel.Rows[25], null, null, null);
				base.SetFoldBehavior(cRptChaTypeCd183, dtAuth.Rows[27], cRptChaTypeCd183P1, cRptChaTypeCd183Label, cRptChaTypeCd183P2, null, dtLabel.Rows[27], null, null, null);
				base.SetFoldBehavior(cThreeD183, dtAuth.Rows[28], cThreeD183P1, cThreeD183Label, cThreeD183P2, null, dtLabel.Rows[28], null, null, null);
				base.SetFoldBehavior(cSelColumnId88, dtAuth.Rows[29], cSelColumnId88P1, cSelColumnId88Label, null, dtLabel.Rows[29], null, null, null);
				base.SetFoldBehavior(cGMinValue183, dtAuth.Rows[30], cGMinValue183P1, cGMinValue183Label, null, dtLabel.Rows[30], null, null, null);
				base.SetFoldBehavior(cGLowRange183, dtAuth.Rows[31], cGLowRange183P1, cGLowRange183Label, null, dtLabel.Rows[31], null, null, null);
				base.SetFoldBehavior(cGMidRange183, dtAuth.Rows[32], cGMidRange183P1, cGMidRange183Label, null, dtLabel.Rows[32], null, null, null);
				base.SetFoldBehavior(cGMaxValue183, dtAuth.Rows[33], cGMaxValue183P1, cGMaxValue183Label, null, dtLabel.Rows[33], null, null, null);
				base.SetFoldBehavior(cGNeedle183, dtAuth.Rows[34], cGNeedle183P1, cGNeedle183Label, null, dtLabel.Rows[34], null, null, null);
                base.SetFoldBehavior(cGMinValueId183, dtAuth.Rows[35], null, null, null, dtLabel.Rows[35], null, null, null);
                base.SetFoldBehavior(cGLowRangeId183, dtAuth.Rows[36], null, null, null, dtLabel.Rows[36], null, null, null);
                base.SetFoldBehavior(cGMidRangeId183, dtAuth.Rows[37], null, null, null, dtLabel.Rows[37], null, null, null);
                base.SetFoldBehavior(cGMaxValueId183, dtAuth.Rows[38], null, null, null, null, dtLabel.Rows[38], null, null, null);
                base.SetFoldBehavior(cGNeedleId183, dtAuth.Rows[39], null, null, null, null, dtLabel.Rows[39], null, null, null);
                base.SetFoldBehavior(cGPositive183, dtAuth.Rows[40], null, null, null, null, dtLabel.Rows[40], null, null, null);
				base.SetFoldBehavior(cGFormat183, dtAuth.Rows[41], cGFormat183P1, cGFormat183Label, null, dtLabel.Rows[41], null, null, null);
				base.SetFoldBehavior(cOriColumnId77, dtAuth.Rows[42], cOriColumnId77P1, cOriColumnId77Label, null, dtLabel.Rows[42], null, null, null);
				base.SetFoldBehavior(cSelColumnId77, dtAuth.Rows[43], cSelColumnId77P1, cSelColumnId77Label, null, dtLabel.Rows[43], null, null, null);
				base.SetFoldBehavior(cSelChange77, dtAuth.Rows[44], cSelChange77P1, cSelChange77Label, null, dtLabel.Rows[44], null, null, null);
            }
            if ((cRptwizName183.Attributes["OnChange"] == null || cRptwizName183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRptwizName183.Visible && !cRptwizName183.ReadOnly) { cRptwizName183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRptwizDesc183.Attributes["OnChange"] == null || cRptwizDesc183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRptwizDesc183.Visible && !cRptwizDesc183.ReadOnly) { cRptwizDesc183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cReportId183.Attributes["OnChange"] == null || cReportId183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cReportId183.Visible && cReportId183.Enabled) { cReportId183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cAccessCd183.Attributes["OnClick"] == null || cAccessCd183.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cAccessCd183.Visible && cAccessCd183.Enabled) { cAccessCd183.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cUsrId183.Attributes["OnChange"] == null || cUsrId183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUsrId183.Visible && cUsrId183.Enabled) { cUsrId183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cOrientationCd183.Attributes["OnChange"] == null || cOrientationCd183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cOrientationCd183.Visible && cOrientationCd183.Enabled) { cOrientationCd183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cUnitCd183.Attributes["OnChange"] == null || cUnitCd183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cUnitCd183.Visible && cUnitCd183.Enabled) { cUnitCd183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cTopMargin183.Attributes["OnChange"] == null || cTopMargin183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cTopMargin183.Visible && !cTopMargin183.ReadOnly) { cTopMargin183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cBottomMargin183.Attributes["OnChange"] == null || cBottomMargin183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cBottomMargin183.Visible && !cBottomMargin183.ReadOnly) { cBottomMargin183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cLeftMargin183.Attributes["OnChange"] == null || cLeftMargin183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cLeftMargin183.Visible && !cLeftMargin183.ReadOnly) { cLeftMargin183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRightMargin183.Attributes["OnChange"] == null || cRightMargin183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRightMargin183.Visible && !cRightMargin183.ReadOnly) { cRightMargin183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRptwizTypeCd183.Attributes["OnChange"] == null || cRptwizTypeCd183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRptwizTypeCd183.Visible && cRptwizTypeCd183.Enabled) { cRptwizTypeCd183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cTemplateName183.Attributes["OnChange"] == null || cTemplateName183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cTemplateName183.Visible && !cTemplateName183.ReadOnly) { cTemplateName183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRptwizCatId183.Attributes["OnChange"] == null || cRptwizCatId183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRptwizCatId183.Visible && cRptwizCatId183.Enabled) { cRptwizCatId183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if (cRptwizCatId183Search.Attributes["OnClick"] == null || cRptwizCatId183Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) { cRptwizCatId183Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelect33.Attributes["OnClick"] == null || cSelect33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSelect33.Visible) { cSelect33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRemove33.Attributes["OnClick"] == null || cRemove33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRemove33.Visible) { cRemove33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cTop33.Attributes["OnClick"] == null || cTop33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cTop33.Visible) { cTop33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cUp33.Attributes["OnClick"] == null || cUp33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cUp33.Visible) { cUp33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cDown33.Attributes["OnClick"] == null || cDown33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDown33.Visible) { cDown33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cBottom33.Attributes["OnClick"] == null || cBottom33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cBottom33.Visible) { cBottom33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cSelButton33.Attributes["OnClick"] == null || cSelButton33.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSelButton33.Visible) { cSelButton33.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cSelect44.Attributes["OnClick"] == null || cSelect44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSelect44.Visible) { cSelect44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRemove44.Attributes["OnClick"] == null || cRemove44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRemove44.Visible) { cRemove44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cTop44.Attributes["OnClick"] == null || cTop44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cTop44.Visible) { cTop44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cUp44.Attributes["OnClick"] == null || cUp44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cUp44.Visible) { cUp44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cDown44.Attributes["OnClick"] == null || cDown44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDown44.Visible) { cDown44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cBottom44.Attributes["OnClick"] == null || cBottom44.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cBottom44.Visible) { cBottom44.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cColSort.Attributes["OnClick"] == null || cColSort.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cColSort.Visible) { cColSort.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cAggregate.Attributes["OnClick"] == null || cAggregate.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cAggregate.Visible) { cAggregate.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRptGroup.Attributes["OnClick"] == null || cRptGroup.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRptGroup.Visible) { cRptGroup.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cOperator.Attributes["OnClick"] == null || cOperator.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cOperator.Visible) { cOperator.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cSelect77.Attributes["OnClick"] == null || cSelect77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSelect77.Visible) { cSelect77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRemove77.Attributes["OnClick"] == null || cRemove77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cRemove77.Visible) { cRemove77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cTop77.Attributes["OnClick"] == null || cTop77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cTop77.Visible) { cTop77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cUp77.Attributes["OnClick"] == null || cUp77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cUp77.Visible) { cUp77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cDown77.Attributes["OnClick"] == null || cDown77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cDown77.Visible) { cDown77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cBottom77.Attributes["OnClick"] == null || cBottom77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cBottom77.Visible) { cBottom77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cSelButton77.Attributes["OnClick"] == null || cSelButton77.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cSelButton77.Visible) { cSelButton77.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cRptChaTypeCd183.Attributes["OnChange"] == null || cRptChaTypeCd183.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cRptChaTypeCd183.Visible && cRptChaTypeCd183.Enabled) { cRptChaTypeCd183.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
            if ((cThreeD183.Attributes["OnClick"] == null || cThreeD183.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cThreeD183.Visible && cThreeD183.Enabled) { cThreeD183.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();"; }
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
					cSaveButton.Visible = false;
				}
				else if ((bool)Session[KEY_bUpdateVisible]) {cSaveButton.Visible = true; cSaveButton.Enabled = true;}
				if (dr["AllowAdd"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cNewButton.Visible = false; cNewSaveButton.Visible = false; cCopyButton.Visible = false; cCopySaveButton.Visible = false;
					if (dt != null) {dt.Rows[0].Delete();}
				}
				else
				{
                    if ((bool)Session[KEY_bPreviewVisible]) { cPreviewButton.Visible = true; cPreviewButton.Enabled = true; }
					if ((bool)Session[KEY_bNewVisible]) {cNewButton.Visible = true; cNewButton.Enabled = true;}
					if ((bool)Session[KEY_bNewSaveVisible]) {cNewSaveButton.Visible = true; cNewSaveButton.Enabled = true;}
					if ((bool)Session[KEY_bCopyVisible]) {cCopyButton.Visible = true; cCopyButton.Enabled = true;}
					if ((bool)Session[KEY_bCopySaveVisible]) {cCopySaveButton.Visible = true; cCopySaveButton.Enabled = true;}
				}
			}
			return dt;
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
			Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
            Session.Remove(KEY_dtReportId183);
            Session.Remove(KEY_dtAccessCd183);
            Session.Remove(KEY_dtUsrId183);
            Session.Remove(KEY_dtOrientationCd183);
            Session.Remove(KEY_dtGPositive183);
            Session.Remove(KEY_dtUnitCd183);
            Session.Remove(KEY_dtRptwizTypeCd183);
            Session.Remove(KEY_dtRptwizCatId183);
            Session.Remove(KEY_dtRptChaTypeCd183);
            Session.Remove(KEY_dtOriColumnId33);
            Session.Remove(KEY_dtOriColumnId44);
            Session.Remove(KEY_dtOriColumnId77);
            Session.Remove(KEY_dtAggregate); SetAggregate();
            Session.Remove(KEY_dtRptGroup); SetRptGroup();
            Session.Remove(KEY_dtRptChart); SetRptChart();
            Session.Remove(KEY_dtOperator); SetOperator();
            //SavePreserve(cSystemId.SelectedValue, 2);
            base.CSrc = new CurrSrc(true, dtSystems.Rows[cSystemId.SelectedIndex]);
            base.CTar = new CurrTar(true, dtSystems.Rows[cSystemId.SelectedIndex]);
            cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List); PopAdmRptWiz95List(sender, e, true, null);
        }

		private void InitMaster(object sender, System.EventArgs e)
		{
            cRptwizId183.Text = string.Empty;
            cRptwizName183.Text = string.Empty;
            cRptwizDesc183.Text = string.Empty;
            SetReportId183(cReportId183, string.Empty);
            SetAccessCd183(cAccessCd183, "V");
            SetUsrId183(cUsrId183, LUser.UsrId.ToString());
            cTemplateName183.Text = string.Empty;
            SetOrientationCd183(cOrientationCd183, "P"); SetUnitCd183(cUnitCd183, "in"); cTopMargin183.Text = "1"; cBottomMargin183.Text = "1"; cLeftMargin183.Text = "0.5"; cRightMargin183.Text = "0.5";
            SetRptwizTypeCd183(cRptwizTypeCd183, string.Empty); cRptwizTypeCd183_SelectedIndexChanged(cRptwizTypeCd183, new EventArgs());
            SetRptwizCatId183(cRptwizCatId183, string.Empty); cRptwizCatId183_pullup();
            SetRptChaTypeCd183(cRptChaTypeCd183, string.Empty); cRptChaTypeCd183_pullup();
            cGMinValue183.Text = string.Empty;
            cGLowRange183.Text = string.Empty;
            cGMidRange183.Text = string.Empty;
            cGMaxValue183.Text = string.Empty;
            cGNeedle183.Text = string.Empty;
            SetSelColumnId99(cGMinValueId183, string.Empty, string.Empty);
            SetSelColumnId99(cGLowRangeId183, string.Empty, string.Empty);
            SetSelColumnId99(cGMidRangeId183, string.Empty, string.Empty);
            SetSelColumnId99(cGMaxValueId183, string.Empty, string.Empty);
            SetSelColumnId99(cGNeedleId183, string.Empty, string.Empty);
            SetGPositive183(cGPositive183, "Y");
            SetGFormat183(cGFormat183, string.Empty);
        }

		protected void cAdmRptWiz95List_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmRptWiz95List_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cAdmRptWiz95List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            // Need to do this before cRptwizCatId183_SelectedIndexChanged:
            Session.Remove(KEY_dtDelColumnId); Session[KEY_dtDelColumnId] = (new AdmRptWiz95()).Tables["AdmRptWizDel"];
            Session.Remove(KEY_dtSelColumnId33);
            SetSelColumnId33(cSelColumnId33, string.Empty, cAdmRptWiz95List.SelectedValue);
            SetSelColumnId33(cSelColumnId55, string.Empty, cAdmRptWiz95List.SelectedValue); EnbSelCol55();
            SetSelColumnId33(cSelColumnId66, string.Empty, cAdmRptWiz95List.SelectedValue); EnbRptGroup(sender, e);
            SetSelColumnId33(cSelColumnId88, string.Empty, cAdmRptWiz95List.SelectedValue); EnbRptChart(sender, e);
            Session.Remove(KEY_dtSelColumnId44); SetSelColumnId44(cSelColumnId44, string.Empty, cAdmRptWiz95List.SelectedValue);
            Session.Remove(KEY_dtSelColumnId77); SetSelColumnId77(cSelColumnId77, string.Empty, cAdmRptWiz95List.SelectedValue);
            DataTable dt = null;
			try
			{
				dt = (new AdminSystem()).GetMstById("GetAdmRptWiz95ById",cAdmRptWiz95List.SelectedValue,(string)Session[KEY_sysConnectionString],LcAppPw);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				if (dt.Rows.Count > 0)
				{
					cAdmRptWiz95List.SetDdVisible();
					DataRow dr = dt.Rows[0];
					try {cRptwizId183.Text = RO.Common3.Utils.fmNumeric(dr["RptwizId183"].ToString(),base.LUser.Culture);} catch {cRptwizId183.Text = string.Empty;}
					try {cRptwizName183.Text = dr["RptwizName183"].ToString();} catch {cRptwizName183.Text = string.Empty;}
					try {cRptwizDesc183.Text = dr["RptwizDesc183"].ToString();} catch {cRptwizDesc183.Text = string.Empty;}
                    SetReportId183(cReportId183, dr["ReportId183"].ToString());
                    SetAccessCd183(cAccessCd183, dr["AccessCd183"].ToString());
                    if (dr["UsrId183"].ToString() != base.LUser.UsrId.ToString() && ((char)191 + base.LImpr.UsrGroups + (char)191).IndexOf((char)191 + "5" + (char)191) < 0)
                    {
                        cAccessCd183.Enabled = false; cSaveButton.Visible = false;
                        if (cAccessCd183.SelectedValue == "V") { cPreviewButton.Visible = false; } else { cPreviewButton.Visible = true; }
                    }
                    else
                    {
                        cAccessCd183.Enabled = true; cSaveButton.Visible = true; cPreviewButton.Visible = true;
                    }
                    SetUsrId183(cUsrId183, dr["UsrId183"].ToString());
                    try { cTemplateName183.Text = dr["TemplateName183"].ToString(); }
                    catch { cTemplateName183.Text = string.Empty; }
                    SetOrientationCd183(cOrientationCd183, dr["OrientationCd183"].ToString());
                    SetUnitCd183(cUnitCd183, dr["UnitCd183"].ToString());
                    try { cTopMargin183.Text = RO.Common3.Utils.fmNumeric(dr["TopMargin183"].ToString(), base.LUser.Culture); }
                    catch { cTopMargin183.Text = string.Empty; }
                    try { cBottomMargin183.Text = RO.Common3.Utils.fmNumeric(dr["BottomMargin183"].ToString(), base.LUser.Culture); }
                    catch { cBottomMargin183.Text = string.Empty; }
                    try { cLeftMargin183.Text = RO.Common3.Utils.fmNumeric(dr["LeftMargin183"].ToString(), base.LUser.Culture); }
                    catch { cLeftMargin183.Text = string.Empty; }
                    try { cRightMargin183.Text = RO.Common3.Utils.fmNumeric(dr["RightMargin183"].ToString(), base.LUser.Culture); }
                    catch { cRightMargin183.Text = string.Empty; }
                    SetRptwizTypeCd183(cRptwizTypeCd183, dr["RptwizTypeCd183"].ToString()); cRptwizTypeCd183_SelectedIndexChanged(cRptwizTypeCd183, new EventArgs());
                    SetRptwizCatId183(cRptwizCatId183, dr["RptwizCatId183"].ToString()); cRptwizCatId183_pullup();
                    try { cThreeD183.Checked = base.GetBool(dr["ThreeD183"].ToString()); }
                    catch { cThreeD183.Checked = false; }
                    SetRptChaTypeCd183(cRptChaTypeCd183, dr["RptChaTypeCd183"].ToString()); cRptChaTypeCd183_pullup();
                    try { cGMinValue183.Text = RO.Common3.Utils.fmNumeric(dr["GMinValue183"].ToString(), base.LUser.Culture); }
                    catch { cGMinValue183.Text = string.Empty; }
                    try { cGLowRange183.Text = RO.Common3.Utils.fmNumeric(dr["GLowRange183"].ToString(), base.LUser.Culture); }
                    catch { cGLowRange183.Text = string.Empty; }
                    try { cGMidRange183.Text = RO.Common3.Utils.fmNumeric(dr["GMidRange183"].ToString(), base.LUser.Culture); }
                    catch { cGMidRange183.Text = string.Empty; }
                    try { cGMaxValue183.Text = RO.Common3.Utils.fmNumeric(dr["GMaxValue183"].ToString(), base.LUser.Culture); }
                    catch { cGMaxValue183.Text = string.Empty; }
                    try { cGNeedle183.Text = RO.Common3.Utils.fmNumeric(dr["GNeedle183"].ToString(), base.LUser.Culture); }
                    catch { cGNeedle183.Text = string.Empty; }
                    SetSelColumnId99(cGMinValueId183, dr["GMinValueId183"].ToString(), cAdmRptWiz95List.SelectedValue);
                    SetSelColumnId99(cGLowRangeId183, dr["GLowRangeId183"].ToString(), cAdmRptWiz95List.SelectedValue);
                    SetSelColumnId99(cGMidRangeId183, dr["GMidRangeId183"].ToString(), cAdmRptWiz95List.SelectedValue);
                    SetSelColumnId99(cGMaxValueId183, dr["GMaxValueId183"].ToString(), cAdmRptWiz95List.SelectedValue);
                    SetSelColumnId99(cGNeedleId183, dr["GNeedleId183"].ToString(), cAdmRptWiz95List.SelectedValue);
                    SetGPositive183(cGPositive183, dr["GPositive183"].ToString());
                    SetGFormat183(cGFormat183, dr["GFormat183"].ToString());
                }
			}
			if (dt == null || dt.Rows.Count <= 0) {InitMaster(sender, e);}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cAdmRptWiz95List.FocusID);
			ShowDirty(false); PanelTop.Update();
			//SavePreserve(cAdmRptWiz95List.SelectedValue,3);
            // Prepare preview button:
            DataTable dtSystems = (DataTable)Session[KEY_dtSystems];
            if (!string.IsNullOrEmpty(cReportId183.SelectedValue) && !string.IsNullOrEmpty(cRptwizId183.Text))
            {
                string PvUrl = "window.open('SqlReport.aspx?gen=Y&csy=" + dtSystems.Rows[cSystemId.SelectedIndex]["SystemId"].ToString() + "&typ=N&rpt=" + cReportId183.SelectedValue + "','Preview" + cRptwizId183.Text + "','resizable=yes,toolbar=yes,scrollbars=yes,width=700,height=400');";
                cPreviewButton.Attributes["onclick"] = PvUrl;
            }
            else
            {
                cPreviewButton.Attributes["onclick"] = string.Empty;
            }
        }

		protected void cRptwizTypeCd183_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (cRptwizTypeCd183.Items.Count > 0 && Session[KEY_dtRptwizTypeCd183] != null)
            {
                DataTable dt = GetScreenTab();
                cTab33.InnerText = dt.Rows[2]["TabFolderName"].ToString();
                cTab44.InnerText = dt.Rows[3]["TabFolderName"].ToString();
                cTab55.InnerText = dt.Rows[4]["TabFolderName"].ToString();
                cTab66.InnerText = dt.Rows[5]["TabFolderName"].ToString();
                cTab77.InnerText = dt.Rows[6]["TabFolderName"].ToString();
                cTab88.InnerText = dt.Rows[7]["TabFolderName"].ToString();
                cTab99.InnerText = dt.Rows[8]["TabFolderName"].ToString();
                if (cRptwizTypeCd183.SelectedValue == "L")	// List (temporary not used)
                {
                    Tab22.Visible = true; cTab22.Visible = true;
                    Tab44.Visible = true; cTab44.Visible = true;
                    Tab55.Visible = false; cTab55.Visible = false;
                    Tab66.Visible = true; cTab66.Visible = true;
                    Tab88.Visible = true; cTab88.Visible = true;
                    Tab99.Visible = false; cTab99.Visible = false;
                    cTemplateName183.Text = string.Empty; cTemplateName183P1.Visible = false; cTemplateName183P2.Visible = false;
                }
                else if (cRptwizTypeCd183.SelectedValue == "D")	// Document (temporary not used)
                {
                    Tab22.Visible = false; cTab22.Visible = false;
                    Tab44.Visible = false; cTab44.Visible = false;
                    Tab55.Visible = false; cTab55.Visible = false;
                    Tab66.Visible = false; cTab66.Visible = false;
                    Tab88.Visible = false; cTab88.Visible = false;
                    Tab99.Visible = false; cTab99.Visible = false;
                    cTemplateName183.Visible = true; cTemplateName183P1.Visible = true; cTemplateName183P2.Visible = true;
                }
                else if (cRptwizTypeCd183.SelectedValue == "C")	// Chart
                {
                    Tab22.Visible = true; cTab22.Visible = true;
                    Tab44.Visible = false; cTab44.Visible = false;
                    Tab55.Visible = false; cTab55.Visible = false;
                    Tab66.Visible = false; cTab66.Visible = false;
                    Tab88.Visible = true; cTab88.Visible = true;
                    Tab99.Visible = false; cTab99.Visible = false;
                    cTemplateName183.Text = string.Empty; cTemplateName183P1.Visible = false; cTemplateName183P2.Visible = false;
                }
                else if (cRptwizTypeCd183.SelectedValue == "B")	// Guage
                {
                    Tab22.Visible = true; cTab22.Visible = true;
                    Tab44.Visible = false; cTab44.Visible = false;
                    Tab55.Visible = true; cTab55.Visible = true;
                    Tab66.Visible = false; cTab66.Visible = false;
                    Tab88.Visible = false; cTab88.Visible = false;
                    Tab99.Visible = true; cTab99.Visible = true;
                    EnbSelCol55();
                    cTemplateName183.Text = string.Empty; cTemplateName183P1.Visible = false; cTemplateName183P2.Visible = false;
                    SetGFormat183(cGFormat183, string.Empty);
                }
                else // Summary Table
                {
                    Tab22.Visible = true; cTab22.Visible = true;
                    Tab44.Visible = true; cTab44.Visible = true;
                    Tab55.Visible = true; cTab55.Visible = true;
                    Tab66.Visible = true; cTab66.Visible = true;
                    Tab88.Visible = true; cTab88.Visible = true;
                    Tab99.Visible = false; cTab99.Visible = false;
                    SetSelColumnId33(cSelColumnId55, string.Empty, cAdmRptWiz95List.SelectedValue);
                    EnbSelCol55();
                    cTemplateName183.Text = string.Empty; cTemplateName183P1.Visible = false; cTemplateName183P2.Visible = false;
                }
                if (cRptwizId183.Text == string.Empty)
                {
                    if (cRptwizTypeCd183.SelectedValue == "C" || cRptwizTypeCd183.SelectedValue == "B")	// Chart or Guage
                    {
                        SetOrientationCd183(cOrientationCd183, "L"); SetUnitCd183(cUnitCd183, "in"); cTopMargin183.Text = "1.5"; cBottomMargin183.Text = "2"; cLeftMargin183.Text = "0"; cRightMargin183.Text = "0";
                    }
                    else
                    {
                        SetOrientationCd183(cOrientationCd183, "P"); SetUnitCd183(cUnitCd183, "in"); cTopMargin183.Text = "1"; cBottomMargin183.Text = "1"; cLeftMargin183.Text = "0.5"; cRightMargin183.Text = "0.5";
                    }
                }
                DataView dv = ((DataTable)Session[KEY_dtRptwizTypeCd183]).DefaultView; dv.RowFilter = string.Empty;
                if (Session["ValRet"] != null && cRptwizTypeCd183.SelectedValue != (string)Session["ValRet"])
                {
                    SetRptwizTypeCd183(cRptwizTypeCd183, (string)Session["ValRet"]); Session.Remove("ValRet");
                    dv = ((DataTable)Session[KEY_dtRptwizTypeCd183]).DefaultView; dv.RowFilter = string.Empty;	// In case of a refresh.
                }
                DataRowView dr = dv[cRptwizTypeCd183.SelectedIndex];
                cRptCtrTypeDesc157.Text = dr["RptCtrTypeDesc157"].ToString();
            }
            ScriptManager.GetCurrent(Parent.Page).SetFocus(((DropDownList)sender).ClientID);
        }

        protected void cRptwizCatId183_pullup()
        {
            if (cRptwizCatId183.Items.Count > 0 && Session[KEY_dtRptwizCatId183] != null)
            {
                DataTable dt = (DataTable)Session[KEY_dtRptwizCatId183];
                DataRow dr = dt.Rows[cRptwizCatId183.SelectedIndex];
                cCatDescription181.Text = dr["CatDescription181"].ToString();
                try
                {
                    if (dr["RptwizCatImg181"].ToString() == string.Empty) { cRptwizCatImg181.Visible = false; }
                    else
                    {
                        cRptwizCatImg181.ImageUrl = "~/DnLoad.aspx?key=" + cRptwizCatId183.Text + "&tbl=dbo.RptwizCat&knm=RptwizCatId&col=SampleImage&sys=" + base.LCurr.DbId.ToString();
                        cRptwizCatImg181.Visible = true;
                    }
                }
                catch { cRptwizCatImg181.Visible = false; }
                cRptwizCatId183Search_Script();
                Session.Remove(KEY_dtOriColumnId33); SetOriColumnId33(cOriColumnId33, string.Empty);
                Session.Remove(KEY_dtOriColumnId44); SetOriColumnId44(cOriColumnId44, string.Empty);
                Session.Remove(KEY_dtOriColumnId77); SetOriColumnId77(cOriColumnId77, string.Empty);
            }
        }

        protected void cRptChaTypeCd183_pullup()
        {
            if (cRptChaTypeCd183.SelectedValue == string.Empty) { cRptChaTypeImg183.Visible = false; }
            else
            {
                cRptChaTypeImg183.Visible = true; cRptChaTypeImg183.Enabled = false;
                if (cThreeD183.Checked)
                {
                    if (cRptChaTypeCd183.SelectedValue == "AH") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaPercentStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "AS") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "AP") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaPlain3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BH") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarPercentStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BS") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BP") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarPlain3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CH") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnPercentStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CS") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnStacked3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CP") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnPlain3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "DE") { cRptChaTypeImg183.ImageUrl = "~/images/report/DoughnutExploded3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "DP") { cRptChaTypeImg183.ImageUrl = "~/images/report/DoughnutPlain3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "LS") { cRptChaTypeImg183.ImageUrl = "~/images/report/LineSmooth3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "LP") { cRptChaTypeImg183.ImageUrl = "~/images/report/LinePlain3D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "PE") { cRptChaTypeImg183.ImageUrl = "~/images/report/PieExploded3D.gif"; }
                    else { cRptChaTypeImg183.ImageUrl = "~/images/report/PiePlain3D.gif"; }
                }
                else
                {
                    if (cRptChaTypeCd183.SelectedValue == "AH") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaPercentStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "AS") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "AP") { cRptChaTypeImg183.ImageUrl = "~/images/report/AreaPlain2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BH") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarPercentStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BS") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "BP") { cRptChaTypeImg183.ImageUrl = "~/images/report/BarPlain2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CH") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnPercentStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CS") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnStacked2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "CP") { cRptChaTypeImg183.ImageUrl = "~/images/report/ColumnPlain2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "DE") { cRptChaTypeImg183.ImageUrl = "~/images/report/DoughnutExploded2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "DP") { cRptChaTypeImg183.ImageUrl = "~/images/report/DoughnutPlain2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "LS") { cRptChaTypeImg183.ImageUrl = "~/images/report/LineSmooth2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "LP") { cRptChaTypeImg183.ImageUrl = "~/images/report/LinePlain2D.gif"; }
                    else if (cRptChaTypeCd183.SelectedValue == "PE") { cRptChaTypeImg183.ImageUrl = "~/images/report/PieExploded2D.gif"; }
                    else { cRptChaTypeImg183.ImageUrl = "~/images/report/PiePlain2D.gif"; }
                }
            }
        }

        protected void cRptChaTypeCd183_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cRptChaTypeCd183_pullup();
            ScriptManager.GetCurrent(Parent.Page).SetFocus(((DropDownList)sender).ClientID);
        }

        protected void cThreeD183_CheckedChanged(object sender, System.EventArgs e)
        {
            cRptChaTypeCd183_pullup();
            ScriptManager.GetCurrent(Parent.Page).SetFocus(((CheckBox)sender).ClientID);
        }

        protected void cRptwizCatId183_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Clear all selected columns first if not first time:
            DataRow dr = null;
            DataTable dt33 = (DataTable)Session[KEY_dtSelColumnId33];
            DataTable dt44 = (DataTable)Session[KEY_dtSelColumnId44];
            DataTable dt77 = (DataTable)Session[KEY_dtSelColumnId77];
            DataTable dtDel = (DataTable)Session[KEY_dtDelColumnId];
            DataView dv33 = dt33 != null ? dt33.DefaultView : null;
            DataView dv44 = dt44 != null ? dt44.DefaultView : null;
            DataView dv77 = dt77 != null ? dt77.DefaultView : null;
            DataView dvDel = dtDel != null ? dtDel.DefaultView : null;
            if (dvDel != null)
            {
                if (dv33 != null)
                {
                    foreach (DataRowView drv33 in dv33)
                    {
                        if (drv33["RptwizDtlId"].ToString() != string.Empty)
                        {
                            dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                            dr["RptwizDtlId184"] = drv33["RptwizDtlId"];
                        }
                    }
                    dv33 = new DataView(dv33.Table.Clone()); Session[KEY_dtSelColumnId33] = dv33.Table;
                    cSelColumnId33.DataSource = dv33; cSelColumnId33.DataBind();
                    if (Tab55.Visible) { cSelColumnId55.DataSource = dv33; cSelColumnId55.DataBind(); EnbSelCol55(); }
                    if (Tab66.Visible) { cSelColumnId66.DataSource = dv33; cSelColumnId66.DataBind(); }
                    if (Tab88.Visible) { cSelColumnId88.DataSource = dv33; cSelColumnId88.DataBind(); EnbRptChart(sender, e); }
                    if (Tab99.Visible)
                    {
                        DataView dvg = new DataView(dv33.ToTable()); dvg.Table.Rows.InsertAt(dvg.Table.NewRow(), 0);
                        cGMinValueId183.DataSource = dvg; cGMinValueId183.DataBind();
                        cGLowRangeId183.DataSource = dvg; cGLowRangeId183.DataBind();
                        cGMidRangeId183.DataSource = dvg; cGMidRangeId183.DataBind();
                        cGMaxValueId183.DataSource = dvg; cGMaxValueId183.DataBind();
                        cGNeedleId183.DataSource = dvg; cGNeedleId183.DataBind();
                    }
                }
                if (dv44 != null)
                {
                    foreach (DataRowView drv44 in dv44)
                    {
                        if (drv44["RptwizDtlId"].ToString() != string.Empty)
                        {
                            dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                            dr["RptwizDtlId184"] = drv44["RptwizDtlId"];
                        }
                    }
                    dv44 = new DataView(dv44.Table.Clone()); Session[KEY_dtSelColumnId44] = dv44.Table;
                    cSelColumnId44.DataSource = dv44; cSelColumnId44.DataBind();
                }
                if (dv77 != null)
                {
                    foreach (DataRowView drv77 in dv77)
                    {
                        if (drv77["RptwizDtlId"].ToString() != string.Empty)
                        {
                            dr = dvDel.Table.NewRow(); dvDel.Table.Rows.Add(dr);
                            dr["RptwizDtlId184"] = drv77["RptwizDtlId"];
                        }
                    }
                    dv77 = new DataView(dv77.Table.Clone()); Session[KEY_dtSelColumnId77] = dv77.Table;
                    cSelColumnId77.DataSource = dv77; cSelColumnId77.DataBind(); Session[KEY_dtSelColumnId77] = dv77.Table;
                }
                Session[KEY_dtDelColumnId] = dvDel.Table;
            }
            cRptwizCatId183_pullup();
            ScriptManager.GetCurrent(Parent.Page).SetFocus(((DropDownList)sender).ClientID);
        }

		public void cNewSaveButton_Click(object sender, System.EventArgs e)
		{
			cNewButton_Click(sender, new EventArgs());
			cSaveButton_Click(sender, new EventArgs());
		}

		public void cNewButton_Click(object sender, System.EventArgs e)
		{
			cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
			PopAdmRptWiz95List(sender, e, false, null);
			// In the case of one item and being selected by default.
			if (cAdmRptWiz95List.Items.Count > 0 && !cAdmRptWiz95List.Items[0].Selected)
			{
				cAdmRptWiz95List.SelectedItem.Selected = false; cAdmRptWiz95List.Items[0].Selected = true; cAdmRptWiz95List.SelectByValue(string.Empty,string.Empty,false); cAdmRptWiz95List_SelectedIndexChanged(sender, e);
			}
		}

		public void cCopyButton_Click(object sender, System.EventArgs e)
		{
            cRptwizId183.Text = string.Empty; cReportId183.ClearSelection();
			cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
			ShowDirty(true);
		}

		public void cCopySaveButton_Click(object sender, System.EventArgs e)
		{
			cCopyButton_Click(sender, new EventArgs());
			cSaveButton_Click(sender, new EventArgs());
		}

		public void cSaveButton_Click(object sender, System.EventArgs e)
		{
			string pid = string.Empty;
			if (ValidPage())
			{
                base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
                string RptId = string.Empty;
                DataTable dts = (DataTable)Session[KEY_dtSystems];
                base.CSrc = new CurrSrc(true, dts.Rows[cSystemId.SelectedIndex]);
                base.CTar = new CurrTar(true, dts.Rows[cSystemId.SelectedIndex]);
                AdmRptWiz95 ds = PrepAdmRptWizData(null, cRptwizId183.Text == string.Empty);
                WebRule wr = new WebRule();
                if (string.IsNullOrEmpty(cAdmRptWiz95List.SelectedValue))	// Add
				try
				{
					if (ds != null)
					{
						pid = (new WebRule()).AddAdmRptWiz95(base.LUser,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw);
					}
					if (!string.IsNullOrEmpty(pid))
					{
                        RptId = wr.WrRptwizGen(Int32.Parse(ds.Tables["AdmRptWiz"].Rows[0]["RptwizId183"].ToString()), dts.Rows[cSystemId.SelectedIndex]["SystemId"].ToString(), dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
                        if (RptId.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(RptId); }
                        cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
                        ShowDirty(false); PopAdmRptWiz95List(sender, e, false, pid);
                        (new GenReportsSystem()).CreateProgram("Ut", Int32.Parse(cReportId183.SelectedValue), cReportId183.SelectedItem.Text, dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcSysConnString, LcAppPw);
                        if (Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["AddMsg"].ToString()); }
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				else try
				{
					bool bValid7 = false;
					if (ds != null && (new WebRule()).UpdAdmRptWiz95(base.LUser,base.LCurr,ds,(string)Session[KEY_sysConnectionString],LcAppPw)) {bValid7 = true;}
					if (bValid7)
					{
                        RptId = wr.WrRptwizGen(Int32.Parse(cRptwizId183.Text), dts.Rows[cSystemId.SelectedIndex]["SystemId"].ToString(), dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), (string)Session[KEY_sysConnectionString], LcAppPw);
                        if (RptId.IndexOf("Error:") >= 0) { bErrNow.Value = "Y"; PreMsgPopup(RptId); }
                        cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
                        ShowDirty(false); PopAdmRptWiz95List(sender, e, false, ds.Tables["AdmRptWiz"].Rows[0]["RptwizId183"]);
                        (new GenReportsSystem()).CreateProgram("Ut", Int32.Parse(cReportId183.SelectedValue), cReportId183.SelectedItem.Text, dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcSysConnString, LcAppPw);
                        if (Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["UpdMsg"].ToString()); }
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
		}

        protected void cXfer_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                (new WebRule()).WrXferRpt(Int32.Parse(cReportId183.SelectedValue), (string)Session[KEY_sysConnectionString], LcAppPw);
                if (ValidPage() && cRptwizId183.Text != string.Empty)
                {
                    AdmRptWiz95 ds = PrepAdmRptWizData(null, false);
                    if (ds != null && (new WebRule()).DelAdmRptWiz95(base.LUser, base.LCurr, ds, (string)Session[KEY_sysConnectionString], LcAppPw))
                    {
                        (new WebRule()).WrRptwizDel(Int32.Parse(cReportId183.SelectedValue), (string)Session[KEY_sysConnectionString], LcAppPw);
                        cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
                        ShowDirty(false); PopAdmRptWiz95List(sender, e, false, null);
                        bInfoNow.Value = "Y"; PreMsgPopup("Report '" + cRptwizName183.Text + "' is ready for advanced customization in the Report Definition and is removed from this generator successfully.");
                    }
                }
            }
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
        }

		public void cDeleteButton_Click(object sender, System.EventArgs e)
		{
			if (ValidPage() && cRptwizId183.Text != string.Empty)
			{
				AdmRptWiz95 ds = PrepAdmRptWizData(null,false);
				try
				{
					bool bValid8 = false;
                    if (ds != null && (new WebRule()).DelAdmRptWiz95(base.LUser, base.LCurr, ds, (string)Session[KEY_sysConnectionString], LcAppPw)) { bValid8 = true; }
					if (bValid8)
					{
                        (new WebRule()).WrRptwizDel(Int32.Parse(cReportId183.SelectedValue), (string)Session[KEY_sysConnectionString], LcAppPw);
                        cAdmRptWiz95List.ClearSearch(); Session.Remove(KEY_dtAdmRptWiz95List);
                        ShowDirty(false); PopAdmRptWiz95List(sender, e, false, null);
                        if (Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(GetScreenHlp().Rows[0]["DelMsg"].ToString()); }
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			}
		}

		private AdmRptWiz95 PrepAdmRptWizData(DataView dv, bool bAdd)
		{
			AdmRptWiz95 ds = new AdmRptWiz95();
			DataRow dr = ds.Tables["AdmRptWiz"].NewRow();
			DataRow drType = ds.Tables["AdmRptWiz"].NewRow();
			DataRow drDisp = ds.Tables["AdmRptWiz"].NewRow();
			if (bAdd) { dr["RptwizId183"] = string.Empty; } else { dr["RptwizId183"] = cRptwizId183.Text; }
			drType["RptwizId183"] = "Numeric"; drDisp["RptwizId183"] = "TextBox";
			try {dr["RptwizName183"] = cRptwizName183.Text;} catch {}
			drType["RptwizName183"] = "VarWChar"; drDisp["RptwizName183"] = "TextBox";
			try {dr["RptwizDesc183"] = cRptwizDesc183.Text;} catch {}
			drType["RptwizDesc183"] = "VarWChar"; drDisp["RptwizDesc183"] = "MultiLine";
			try {dr["RptwizTypeCd183"] = cRptwizTypeCd183.SelectedValue;} catch {}
			drType["RptwizTypeCd183"] = "Char"; drDisp["RptwizTypeCd183"] = "DropDownList";
			try {dr["TemplateName183"] = cTemplateName183.Text;} catch {}
			drType["TemplateName183"] = "VarChar"; drDisp["TemplateName183"] = "TextBox";
			try {dr["RptwizCatId183"] = cRptwizCatId183.SelectedValue;} catch {}
			drType["RptwizCatId183"] = "Numeric"; drDisp["RptwizCatId183"] = "DropDownList";
			try {dr["AccessCd183"] = cAccessCd183.SelectedValue;} catch {}
			drType["AccessCd183"] = "Char"; drDisp["AccessCd183"] = "RadioButtonList";
			try {dr["UsrId183"] = cUsrId183.SelectedValue;} catch {}
			drType["UsrId183"] = "Numeric"; drDisp["UsrId183"] = "DropDownList";
			try {dr["ReportId183"] = cReportId183.SelectedValue;} catch {}
			drType["ReportId183"] = "Numeric"; drDisp["ReportId183"] = "DropDownList";
			try {dr["OrientationCd183"] = cOrientationCd183.SelectedValue;} catch {}
			drType["OrientationCd183"] = "Char"; drDisp["OrientationCd183"] = "RadioButtonList";
			try {dr["UnitCd183"] = cUnitCd183.SelectedValue;} catch {}
			drType["UnitCd183"] = "Char"; drDisp["UnitCd183"] = "DropDownList";
			try {dr["TopMargin183"] = cTopMargin183.Text;} catch {}
			drType["TopMargin183"] = "Decimal"; drDisp["TopMargin183"] = "TextBox";
			try {dr["BottomMargin183"] = cBottomMargin183.Text;} catch {}
			drType["BottomMargin183"] = "Decimal"; drDisp["BottomMargin183"] = "TextBox";
			try {dr["LeftMargin183"] = cLeftMargin183.Text;} catch {}
			drType["LeftMargin183"] = "Decimal"; drDisp["LeftMargin183"] = "TextBox";
			try {dr["RightMargin183"] = cRightMargin183.Text;} catch {}
			drType["RightMargin183"] = "Decimal"; drDisp["RightMargin183"] = "TextBox";
			try {dr["RptChaTypeCd183"] = cRptChaTypeCd183.SelectedValue;} catch {}
			drType["RptChaTypeCd183"] = "Char"; drDisp["RptChaTypeCd183"] = "DropDownList";
			try {dr["ThreeD183"] = base.SetBool(cThreeD183.Checked);} catch {}
			drType["ThreeD183"] = "Char"; drDisp["ThreeD183"] = "CheckBox";
			try {dr["GMinValue183"] = cGMinValue183.Text;} catch {}
			drType["GMinValue183"] = "Decimal"; drDisp["GMinValue183"] = "TextBox";
			try {dr["GLowRange183"] = cGLowRange183.Text;} catch {}
			drType["GLowRange183"] = "Decimal"; drDisp["GLowRange183"] = "TextBox";
			try {dr["GMidRange183"] = cGMidRange183.Text;} catch {}
			drType["GMidRange183"] = "Decimal"; drDisp["GMidRange183"] = "TextBox";
			try {dr["GMaxValue183"] = cGMaxValue183.Text;} catch {}
			drType["GMaxValue183"] = "Decimal"; drDisp["GMaxValue183"] = "TextBox";
			try {dr["GNeedle183"] = cGNeedle183.Text;} catch {}
			drType["GNeedle183"] = "Decimal"; drDisp["GNeedle183"] = "TextBox";
			try {dr["GMinValueId183"] = cGMinValueId183.Text;} catch {}
			drType["GMinValueId183"] = "Numeric"; drDisp["GMinValueId183"] = "TextBox";
			try {dr["GLowRangeId183"] = cGLowRangeId183.Text;} catch {}
			drType["GLowRangeId183"] = "Numeric"; drDisp["GLowRangeId183"] = "TextBox";
			try {dr["GMidRangeId183"] = cGMidRangeId183.Text;} catch {}
			drType["GMidRangeId183"] = "Numeric"; drDisp["GMidRangeId183"] = "TextBox";
			try {dr["GMaxValueId183"] = cGMaxValueId183.Text;} catch {}
			drType["GMaxValueId183"] = "Numeric"; drDisp["GMaxValueId183"] = "TextBox";
			try {dr["GNeedleId183"] = cGNeedleId183.Text;} catch {}
			drType["GNeedleId183"] = "Numeric"; drDisp["GNeedleId183"] = "TextBox";
			try { dr["GPositive183"] = cGPositive183.SelectedValue;} catch {}
            drType["GPositive183"] = "Char"; drDisp["GPositive183"] = "RadioButtonList";
			try {dr["GFormat183"] = cGFormat183.SelectedValue;} catch {}
			drType["GFormat183"] = "Char"; drDisp["GFormat183"] = "DropDownList";
			if (bAdd)
			{
				DataTable dtAuth = GetAuthCol();
				if (dtAuth != null)
				{
					if (dtAuth.Rows[8]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[8]["ColVisible"].ToString() == "N") {dr["AccessCd183"] = "V";}
                    if (dtAuth.Rows[12]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[12]["ColVisible"].ToString() == "N") {dr["OrientationCd183"] = "P";}
                    if (dtAuth.Rows[14]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[14]["ColVisible"].ToString() == "N") {dr["TopMargin183"] = 1.00; }
					if (dtAuth.Rows[15]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[15]["ColVisible"].ToString() == "N") {dr["BottomMargin183"] = 1.00;}
					if (dtAuth.Rows[16]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[16]["ColVisible"].ToString() == "N") {dr["LeftMargin183"] = 1.00;}
					if (dtAuth.Rows[17]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[17]["ColVisible"].ToString() == "N") {dr["RightMargin183"] = 1.00;}
					if (dtAuth.Rows[40]["ColReadOnly"].ToString() == "Y" || dtAuth.Rows[40]["ColVisible"].ToString() == "N") {dr["GPositive183"] = "Y";}
				}
			}
			ds.Tables["AdmRptWiz"].Rows.Add(dr); ds.Tables["AdmRptWiz"].Rows.Add(drType); ds.Tables["AdmRptWiz"].Rows.Add(drDisp);
            DataTable dt77 = (DataTable)Session[KEY_dtSelColumnId77];		// Criteria:
            DataTable dt44 = (DataTable)Session[KEY_dtSelColumnId44];		// Sorting:
            DataTable dt33 = (DataTable)Session[KEY_dtSelColumnId33];		// Aggregate, Grouping and charting:
            bool bFound = false;
            if (dt33 != null && dt33.Rows.Count > 0)
            {
                int ii = 10;
                foreach (DataRow drd in dt33.Rows)
                {
                    DataRow drAdd = ds.Tables["AdmRptWizAdd"].NewRow();
                    drAdd["RptwizId183"] = cRptwizId183.Text;
                    if (bAdd) { drAdd["RptwizDtlId184"] = string.Empty; } else { drAdd["RptwizDtlId184"] = drd["RptwizDtlId"]; }
                    drAdd["ColumnId184"] = drd["ColumnId33"];
                    if (drd["ColumnId33Text"].ToString().IndexOf("(") < 0) { drAdd["ColHeader184"] = drd["ColumnId33Text"].ToString().Trim(); } else { drAdd["ColHeader184"] = string.Empty; }
                    drAdd["ColSelect184"] = ii.ToString();
                    if (drd["ColumnId55Text"].ToString().IndexOf("[AVG] ") >= 0) { drAdd["AggregateCd184"] = "A"; }
                    else if (drd["ColumnId55Text"].ToString().IndexOf("[CNT] ") >= 0) { drAdd["AggregateCd184"] = "C"; }
                    else if (drd["ColumnId55Text"].ToString().IndexOf("[MIN] ") >= 0) { drAdd["AggregateCd184"] = "I"; }
                    else if (drd["ColumnId55Text"].ToString().IndexOf("[MAX] ") >= 0) { drAdd["AggregateCd184"] = "M"; }
                    else if (drd["ColumnId55Text"].ToString().IndexOf("[SUM] ") >= 0) { drAdd["AggregateCd184"] = "S"; }
                    if (drd["ColumnId66Text"].ToString().IndexOf("[1] ") >= 0) { drAdd["ColGroup184"] = "1"; }
                    else if (drd["ColumnId66Text"].ToString().IndexOf("[2] ") >= 0) { drAdd["ColGroup184"] = "2"; }
                    else if (drd["ColumnId66Text"].ToString().IndexOf("[3] ") >= 0) { drAdd["ColGroup184"] = "3"; }
                    if (drd["ColumnId88Text"].ToString().IndexOf("[C] ") >= 0) { drAdd["RptChartCd184"] = "C"; }
                    else if (drd["ColumnId88Text"].ToString().IndexOf("[V] ") >= 0) { drAdd["RptChartCd184"] = "V"; }
                    else if (drd["ColumnId88Text"].ToString().IndexOf("[S] ") >= 0) { drAdd["RptChartCd184"] = "S"; }
                    ds.Tables["AdmRptWizAdd"].Rows.Add(drAdd);
                    ii = ii + 10;
                }
            }
            DataView dvAdd = new DataView(ds.Tables["AdmRptWizAdd"]);
            if (dt44 != null && dt44.Rows.Count > 0)
            {
                int ii = 10;
                foreach (DataRow drd in dt44.Rows)
                {
                    bFound = false;
                    foreach (DataRowView drvAdd in dvAdd)
                    {
                        if (drvAdd["ColumnId184"].ToString() == drd["ColumnId44"].ToString())
                        {
                            if (drd["ColumnId44Text"].ToString().IndexOf("[ASC] ") >= 0) { drvAdd["ColSort184"] = ii; } else { drvAdd["ColSort184"] = -ii; }
                            if (!bAdd && drd["RptwizDtlId"] != string.Empty && drvAdd["RptwizDtlId184"].ToString() == string.Empty) { drvAdd["RptwizDtlId184"] = drd["RptwizDtlId"]; }
                            bFound = true; break;
                        }
                    }
                    if (!bFound)
                    {
                        DataRow drAdd = ds.Tables["AdmRptWizAdd"].NewRow();
                        drAdd["RptwizId183"] = cRptwizId183.Text;
                        if (bAdd) { drAdd["RptwizDtlId184"] = string.Empty; } else { drAdd["RptwizDtlId184"] = drd["RptwizDtlId"]; }
                        drAdd["ColumnId184"] = drd["ColumnId44"];
                        if (drd["ColumnId44Text"].ToString().IndexOf("[ASC] ") >= 0) { drAdd["ColSort184"] = ii; } else { drAdd["ColSort184"] = -ii; }
                        ds.Tables["AdmRptWizAdd"].Rows.Add(drAdd);
                    }
                    ii = ii + 10;
                }
            }
            if (dt77 != null && dt77.Rows.Count > 0)
            {
                int ii = 10;
                foreach (DataRow drd in dt77.Rows)
                {
                    bFound = false;
                    foreach (DataRowView drvAdd in dvAdd)
                    {
                        if (drvAdd["ColumnId184"].ToString() == drd["ColumnId77"].ToString() && drvAdd["CriOperName184"] == string.Empty)
                        {
                            drvAdd["CriOperName184"] = drd["OperatorName"];
                            drvAdd["CriSelect184"] = ii;
                            drvAdd["CriHeader184"] = drd["ColumnId77Text"].ToString().Substring(drd["ColumnId77Text"].ToString().IndexOf("] ") + 2).Trim();
                            if (!bAdd && drd["RptwizDtlId"] != string.Empty && drvAdd["RptwizDtlId184"].ToString() == string.Empty) { drvAdd["RptwizDtlId184"] = drd["RptwizDtlId"]; }
                            bFound = true; break;
                        }
                    }
                    if (!bFound)
                    {
                        DataRow drAdd = ds.Tables["AdmRptWizAdd"].NewRow();
                        drAdd["RptwizId183"] = cRptwizId183.Text;
                        if (bAdd) { drAdd["RptwizDtlId184"] = string.Empty; } else { drAdd["RptwizDtlId184"] = drd["RptwizDtlId"]; }
                        drAdd["ColumnId184"] = drd["ColumnId"];
                        drAdd["CriOperName184"] = drd["OperatorName"];
                        drAdd["CriSelect184"] = ii;
                        drAdd["CriHeader184"] = drd["ColumnId77Text"].ToString().Substring(drd["ColumnId77Text"].ToString().IndexOf("] ") + 2).Trim();
                        ds.Tables["AdmRptWizAdd"].Rows.Add(drAdd);
                    }
                    ii = ii + 10;
                }
            }
            DataTable dtDel = (DataTable)Session[KEY_dtDelColumnId];		// Deleted Columns:
            foreach (DataRow drd in dtDel.Rows)
            {
                bFound = false;
                foreach (DataRowView drvAdd in dvAdd)
                {
                    if (drvAdd["RptwizDtlId184"].ToString() == drd["RptwizDtlId184"].ToString()) { bFound = true; break; }
                }
                if (!bFound)
                {
                    DataRow drDel = ds.Tables["AdmRptWizDel"].NewRow();
                    drDel["RptwizDtlId184"] = drd["RptwizDtlId184"];
                    ds.Tables["AdmRptWizDel"].Rows.Add(drDel);
                }
            }
            return ds;
		}

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) {return false;}
			DataTable dt = null;
            dt = (DataTable)Session[KEY_dtUsrId183];
            if (dt != null && dt.Rows.Count <= 0)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'UsrId', please investigate."); return false;
            }
            dt = (DataTable)Session[KEY_dtRptwizTypeCd183];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'RptwizTypeCd', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtRptwizCatId183];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'RptwizCatId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtUnitCd183];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'UnitCd', please investigate."); return false;
			}
            if (Tab99.Visible && cGMinValue183.Text == string.Empty && cGMinValueId183.SelectedValue == string.Empty)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Please enter a static value for Minimum or select a dynamic column and try again."); return false;
            }
            if (Tab99.Visible && cGLowRange183.Text == string.Empty && cGLowRangeId183.SelectedValue == string.Empty)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Please enter a static value for Low-Range-Maximum or select a dynamic column and try again."); return false;
            }
            if (Tab99.Visible && cGMidRange183.Text == string.Empty && cGMidRangeId183.SelectedValue == string.Empty)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Please enter a static value for Mid-Range-Maximum or select a dynamic column and try again."); return false;
            }
            if (Tab99.Visible && cGMaxValue183.Text == string.Empty && cGMaxValueId183.SelectedValue == string.Empty)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Please enter a static value for Maximum or select a dynamic column and try again."); return false;
            }
            if (Tab99.Visible && cGNeedle183.Text == string.Empty && cGNeedleId183.SelectedValue == string.Empty)
            {
                bErrNow.Value = "Y"; PreMsgPopup("Please enter a static value for Needle or select a dynamic column and try again."); return false;
            }
            return true;
		}

		protected void cbPostBack(object sender, System.EventArgs e)
		{
            ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
        }

		protected void IgnoreConfirm()
		{
			if ((cSaveButton.Attributes["OnClick"] == null || cSaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSaveButton.Visible) {cSaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cCopySaveButton.Attributes["OnClick"] == null || cCopySaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cCopySaveButton.Visible) {cCopySaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cDeleteButton.Attributes["OnClick"] == null || cDeleteButton.Attributes["OnClick"].IndexOf("return confirm") < 0) && cDeleteButton.Visible) {cDeleteButton.Attributes["OnClick"] += "return confirm('Delete this record for sure?');";}

            if ((cOrientationCd183.Attributes["OnClick"] == null || cOrientationCd183.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cOrientationCd183.Visible) { cOrientationCd183.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cColSort.Attributes["OnClick"] == null || cColSort.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cColSort.Visible) { cColSort.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cAggregate.Attributes["OnClick"] == null || cAggregate.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cAggregate.Visible) { cAggregate.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRptGroup.Attributes["OnClick"] == null || cRptGroup.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRptGroup.Visible) { cRptGroup.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRptChart.Attributes["OnClick"] == null || cRptChart.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRptChart.Visible) { cRptChart.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }

            if ((cRptwizCatId183Search.Attributes["OnClick"] == null || cRptwizCatId183Search.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRptwizCatId183Search.Visible) { cRptwizCatId183Search.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelect33.Attributes["OnClick"] == null || cSelect33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSelect33.Visible) { cSelect33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRemove33.Attributes["OnClick"] == null || cRemove33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRemove33.Visible) { cRemove33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cTop33.Attributes["OnClick"] == null || cTop33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cTop33.Visible) { cTop33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cUp33.Attributes["OnClick"] == null || cUp33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cUp33.Visible) { cUp33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cDown33.Attributes["OnClick"] == null || cDown33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cDown33.Visible) { cDown33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cBottom33.Attributes["OnClick"] == null || cBottom33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cBottom33.Visible) { cBottom33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelButton33.Attributes["OnClick"] == null || cSelButton33.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSelButton33.Visible) { cSelButton33.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cOriColumnId33.Attributes["OnChange"] == null || cOriColumnId33.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cOriColumnId33.Visible) { cOriColumnId33.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId33.Attributes["OnChange"] == null || cSelColumnId33.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId33.Visible) { cSelColumnId33.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelect44.Attributes["OnClick"] == null || cSelect44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSelect44.Visible) { cSelect44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRemove44.Attributes["OnClick"] == null || cRemove44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRemove44.Visible) { cRemove44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cTop44.Attributes["OnClick"] == null || cTop44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cTop44.Visible) { cTop44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cUp44.Attributes["OnClick"] == null || cUp44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cUp44.Visible) { cUp44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cDown44.Attributes["OnClick"] == null || cDown44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cDown44.Visible) { cDown44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cBottom44.Attributes["OnClick"] == null || cBottom44.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cBottom44.Visible) { cBottom44.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cOriColumnId44.Attributes["OnChange"] == null || cOriColumnId44.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cOriColumnId44.Visible) { cOriColumnId44.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId44.Attributes["OnChange"] == null || cSelColumnId44.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId44.Visible) { cSelColumnId44.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId55.Attributes["OnChange"] == null || cSelColumnId55.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId55.Visible) { cSelColumnId55.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId66.Attributes["OnChange"] == null || cSelColumnId66.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId66.Visible) { cSelColumnId66.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId88.Attributes["OnChange"] == null || cSelColumnId88.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId88.Visible) { cSelColumnId88.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRptChaTypeCd183.Attributes["OnChange"] == null || cRptChaTypeCd183.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cRptChaTypeCd183.Visible) { cRptChaTypeCd183.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cThreeD183.Attributes["OnClick"] == null || cThreeD183.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cThreeD183.Visible) { cThreeD183.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelect77.Attributes["OnClick"] == null || cSelect77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSelect77.Visible) { cSelect77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cRemove77.Attributes["OnClick"] == null || cRemove77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cRemove77.Visible) { cRemove77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cTop77.Attributes["OnClick"] == null || cTop77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cTop77.Visible) { cTop77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cUp77.Attributes["OnClick"] == null || cUp77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cUp77.Visible) { cUp77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cDown77.Attributes["OnClick"] == null || cDown77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cDown77.Visible) { cDown77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cBottom77.Attributes["OnClick"] == null || cBottom77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cBottom77.Visible) { cBottom77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelButton77.Attributes["OnClick"] == null || cSelButton77.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cSelButton77.Visible) { cSelButton77.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cOriColumnId77.Attributes["OnChange"] == null || cOriColumnId77.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cOriColumnId77.Visible) { cOriColumnId77.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cSelColumnId77.Attributes["OnChange"] == null || cSelColumnId77.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cSelColumnId77.Visible) { cSelColumnId77.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
            if ((cOperator.Attributes["OnChange"] == null || cOperator.Attributes["OnChange"].IndexOf("_bConfirm") < 0) && cOperator.Visible) { cOperator.Attributes["OnChange"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
        }

        //protected void InitPreserve()
        //{
        //    aNam.Value = "ctl00_ModuleLogin_SystemsList," + cFilterId.ClientID + "," + cSystemId.ClientID + "," + cAdmRptWiz95List.FocusID;
        //    aVal.Value = LcSystemId.ToString() + "," + (Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0") + "," + cSystemId.SelectedValue + "," + cAdmRptWiz95List.SelectedValue;
        //}

        //protected void SavePreserve(string sValue, int iPos)
        //{
        //    string [] arr = aVal.Value.Split(',');
        //    if (iPos < arr.Length) {arr[iPos] = sValue;}
        //    aVal.Value = string.Join(",",arr);
        //}

		protected void ShowDirty(bool bShow)
		{
			if (bShow) {bPgDirty.Value = "Y";} else {bPgDirty.Value = "N";}
		}

		private void PreMsgPopup(string msg)
		{
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/warning.gif";
		    string focusOnCloseId = cAdmRptWiz95List.ClientID + @"_ctl01";
		    string msgContent = ReformatErrMsg(msg);
		    if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
		    if (bErrNow.Value == "Y") { iconUrl = "images/error.gif"; bErrNow.Value = "N"; }
		    else if (bInfoNow.Value == "Y") { iconUrl = "images/info.gif"; bInfoNow.Value = "N"; }
			string script =
			@"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
		}
	}
}

