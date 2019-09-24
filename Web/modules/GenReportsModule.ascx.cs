namespace RO.Web
{
	using System;
	using System.IO;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using System.Threading;
	using RO.Facade3;
	using RO.Common3;
	using RO.Common3.Data;
	using AjaxControlToolkit;

	public partial class GenReportsModule : RO.Web.ModuleBase
	{
		private const string KEY_dtEntity = "Cache:dtEntity";
		private const string KEY_dtSystem = "Cache:dtSystem";
		private const string KEY_dtClientTier = "Cache:dtClientTier";
		private const string KEY_dtRuleTier = "Cache:dtRuleTier";
		private const string KEY_dtDataTier = "Cache:dtDataTier";
		private const string KEY_dtReportList = "Cache:dtReportList";
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;

		private int LastDataTierId = 0;

		public GenReportsModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
            bErrNow.Value = "N"; bInfoNow.Value = "N";
            if (!IsPostBack)
			{
				Session.Remove(KEY_dtEntity);
				Session.Remove(KEY_dtSystem);
				Session.Remove(KEY_dtClientTier);
				Session.Remove(KEY_dtRuleTier);
				Session.Remove(KEY_dtDataTier);
				Session.Remove(KEY_dtReportList);
				GetEntity();
                cHelpLabel.Text = "Please select the appropriate project and database; then select one or more reports or check 'All Reports'. Click the 'Create' button to generate the respective programs. All default client/rule/data tiers will be regenerated regardless of the selected targets. Any residual programs of 'deleted report(s)' will be removed in this process.";
				cTitleLabel.Text = "Generate Reports";
				if (cReportList.Attributes["OnChange"] == null || cReportList.Attributes["OnChange"].IndexOf("_cAllReport") < 0) { cReportList.Attributes["OnChange"] += "document.getElementById('" + cAllReport.ClientID + "').checked = false;"; }
				ScriptManager.GetCurrent(Parent.Page).SetFocus(cEntityId.ClientID);
			}
			else
			{
				string[] cc = Request.Form.GetValues("__EVENTTARGET");
				if (base.CPrj != null && base.CPrj.EntityId.ToString() != cEntityId.SelectedValue && !(cc != null && cc[0].EndsWith("cEntityId")))
				{
					bErrNow.Value = "Y"; PreMsgPopup("Entity information has been changed, please re-select from menu and try again!");
				}
			}
			cMsgLabel.Style["Color"] = "blue"; cMsgLabel.Text = string.Empty;
		}

		private void Page_Init(object sender, EventArgs e)
		{
            InitializeComponent();
        }

		#region Web Form Designer generated code
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
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

        private void CheckAuthentication(bool pageLoad)
        {
            CheckAuthentication(pageLoad, true);
        }

		private void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
		}

		private void GetEntity()
		{
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			if (dt == null) { dt = (new RobotSystem()).GetEntityList(); }
			if (dt != null)
			{
				Session[KEY_dtEntity] = dt;
				cEntityId.DataSource = dt; cEntityId.DataBind();
				if (cEntityId.Items.Count > 0)
				{
					ListItem li = null;
					if (base.CPrj != null)
					{
						li = cEntityId.Items.FindByValue(base.CPrj.EntityId.ToString());
					}
					if (li != null) { cEntityId.ClearSelection(); li.Selected = true; } 
					else
					{
						cEntityId.Items[0].Selected = true;
					}
					cEntityId_SelectedIndexChanged(null, new EventArgs());
				}
			}
		}

		private void GetSystem()
		{
			DataTable dt = (new LoginSystem()).GetSystemsList(base.CPrj.SrcDesConnectionString, base.CPrj.SrcDesPassword);
			if (dt != null)
			{
				Session[KEY_dtSystem] = dt;
				cSystemId.DataSource = dt; cSystemId.DataBind();
				if (cSystemId.Items.Count > 0)
				{
					cSystemId.Items[0].Selected = true;
					cSystemId_SelectedIndexChanged(null, new EventArgs());
				}
			}
		}

		private void GetClientTier(Int16 EntityId)
		{
			DataTable dt = (new RobotSystem()).GetClientTier(EntityId);
			if (dt != null)
			{
				Session[KEY_dtClientTier] = dt;
				cClientTierId.DataSource = dt; cClientTierId.DataBind();
				if (cClientTierId.Items.Count > 0) {cClientTierId.Items[0].Selected = true;}
			}
		}

		private void GetRuleTier(Int16 EntityId)
		{
			DataTable dt = (new RobotSystem()).GetRuleTier(EntityId);
			if (dt != null)
			{
				Session[KEY_dtRuleTier] = dt;
				cRuleTierId.DataSource = dt; cRuleTierId.DataBind();
				if (cRuleTierId.Items.Count > 0) {cRuleTierId.Items[0].Selected = true;}
			}
		}

		private void GetDataTier(Int16 EntityId)
		{
			DataTable dt = (new RobotSystem()).GetDataTier(EntityId);
			if (dt != null)
			{
				Session[KEY_dtDataTier] = dt;
				cDataTierId.DataSource = dt; cDataTierId.DataBind();
				if (cDataTierId.Items.Count > 0) {cDataTierId.Items[0].Selected = true;}
			}
		}

		protected void cSearchButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			PopReportList(cSearch.Text);
		}

        protected void cGenButton_Click(object sender, System.EventArgs e)
		{
			DataTable dtp = (DataTable)Session[KEY_dtEntity];
			DataTable dts = (DataTable)Session[KEY_dtSystem];
            if (dtp == null || dts == null)
            {
                cMsgLabel.Style["Color"] = "red"; cMsgLabel.Text = "Datatables dtEntity and dtSystem cannot be null. Please call administrator ASAP. Thank you."; return;
            }
            if (Config.DeployType != "DEV" && dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString() != dtp.Rows[cEntityId.SelectedIndex]["EntityCode"].ToString() + "View")
            {
                cMsgLabel.Style["Color"] = "red"; cMsgLabel.Text = "Please do not generate codes on production system. Thank you."; return;
            }
            if (dtp.Rows[cEntityId.SelectedIndex]["EntityCode"].ToString() != "RO" && dts.Rows[cSystemId.SelectedIndex]["SysProgram"].ToString() == "Y")
			{
				cMsgLabel.Style["Color"] = "red"; cMsgLabel.Text = "Please do not regenerate administration codes. Thank you."; return;
			}
			GenReportsSystem GenSys = new GenReportsSystem();
			// Do the removal for Advanced reports:
			DataView dvDel = new DataView(GenSys.GetReportDel(string.Empty, base.CSrc.SrcDbDatabase, base.CSrc.SrcConnectionString, base.CSrc.SrcDbPassword));
			if (dvDel != null)
			{
				foreach (DataRowView drv in dvDel)
				{
					GenSys.DeleteProgram(string.Empty, drv["ProgramName"].ToString(), Int32.Parse(drv["ReportId"].ToString()), drv["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar);
				}
			}
			// Do the removal for user-generated reports:
			dvDel = new DataView(GenSys.GetReportDel("Ut", base.CSrc.SrcDbDatabase, base.CSrc.SrcConnectionString, base.CSrc.SrcDbPassword));
			if (dvDel != null)
			{
				foreach (DataRowView drv in dvDel)
				{
					GenSys.DeleteProgram("Ut", drv["ProgramName"].ToString(), Int32.Parse(drv["ReportId"].ToString()), drv["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar);
				}
			}
			cMsgLabel.Style["Color"] = "blue"; cMsgLabel.Text = string.Empty;
			int iDpl = 0;
			int iGen = 0;
			int iNot = 0;
			string sDpl = string.Empty;
			string sGen = string.Empty;
			string sNot = string.Empty;
			DataView dv = new DataView((DataTable)Session[KEY_dtReportList]);
			if (dv != null)
			{
				int ii = 0;
				foreach (DataRowView drv in dv)
				{
					if (cAllReport.Checked || cReportList.Items[ii].Selected)
					{
						if (dts != null && drv["GenerateRp"].ToString() == "Y" && GenSys.CreateProgram(string.Empty, Int32.Parse(drv["ReportId"].ToString()), drv["ReportTitle"].ToString(), dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw)) { iGen = iGen + 1; } else { iNot = iNot + 1; }
						//if (drv["ReportTypeCd"].ToString() == "S" && DeployRdl(drv["ProgramName"].ToString(), dts, base.CPrj)) { iDpl = iDpl + 1; }
					}
					ii = ii + 1;
				}
				//if (iGen >= 1) { sGen = "s"; Robot.CompileProxy(base.CPrj, base.CSrc); }
                //ii = 0;
                //foreach (DataRowView drv in dv)
                //{
                //    if (drv["GenerateRp"].ToString() == "Y" && (cAllReport.Checked || cReportList.Items[ii].Selected))
                //    {
                //        GenSys.ProxyProgram(string.Empty, Int32.Parse(drv["ReportId"].ToString()), base.CPrj, base.CSrc);
                //    }
                //    ii = ii + 1;
                //}
                if (iGen > 1) { sGen = "s"; }
                if (iDpl > 1) { sDpl = "s"; }
				if (iNot > 1) { sNot = "s"; }
				cMsgLabel.Text = iGen.ToString() + " report" + sGen + " generated and " + iDpl.ToString() + " definition file" + sDpl + " deployed successfully; " + iNot.ToString() + " selected report" + sNot + " not generated.";
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cSystemId.ClientID);
		}

		/* Obsolete Feb 21, 2011.
		// RptServWs is only visible to web tier, not rule tier:
		private bool DeployRdl(string RdlName, DataTable dts, CurrPrj CPrj)
		{
			RptServWs.ReportingService2005 rs = new RptServWs.ReportingService2005();
            rs.Url = Config.WsRptBaseUrl;
			rs.Credentials = new System.Net.NetworkCredential(Config.WsRptUserName, Config.WsRptPassword, Config.WsRptDomain);
			// Create rdl on server:
			FileStream fstream = File.OpenRead(CPrj.TarClientProgramPath + @"reports\" + RdlName + "Report.rdl");
			byte[] rdef = new Byte[fstream.Length];
			fstream.Read(rdef, 0, (int)fstream.Length);
			fstream.Close();
			bool bFound = false;
			RptServWs.CatalogItem[] items = rs.ListChildren("/", false);
			foreach (RptServWs.CatalogItem ci in items)
			{
				if (ci.Type == RptServWs.ItemTypeEnum.Folder && ci.Name == CPrj.EntityCode) { bFound = true; break; }
			}
			if (!bFound) { rs.CreateFolder(CPrj.EntityCode, "/", null); }
			rs.CreateReport(RdlName + "Report", "/" + CPrj.EntityCode, true, rdef, null);
			// Create Data Source:
			bFound = false;
			items = rs.ListChildren("/" + CPrj.EntityCode, false);
			foreach (RptServWs.CatalogItem ci in items)
			{
				if (ci.Type == RptServWs.ItemTypeEnum.DataSource && ci.Name == dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString()) { bFound = true; break; }
			}
			if (!bFound)
			{
				RptServWs.DataSourceDefinition dsd = new RptServWs.DataSourceDefinition();
				dsd.ConnectString = "Data Source = " + dts.Rows[cSystemId.SelectedIndex]["dbAppServer"].ToString() + "; Initial Catalog = " + dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString() + ";";
				dsd.CredentialRetrieval = RptServWs.CredentialRetrievalEnum.Prompt;
				dsd.Enabled = true; dsd.Extension = "SQL";
				rs.CreateDataSource(dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), "/" + CPrj.EntityCode, false, dsd, null);
			}
			// Associate rdl with data source:
			RptServWs.DataSourceReference dsr = new RptServWs.DataSourceReference();
			dsr.Reference = "/" + CPrj.EntityCode + "/" + dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString();
			RptServWs.DataSource ds = new RptServWs.DataSource();
			ds.Item = dsr; ds.Name = dts.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString();
			RptServWs.DataSource[] dss = new RptServWs.DataSource[] { ds };
			rs.SetItemDataSources("/" + CPrj.EntityCode + "/" + RdlName + "Report", dss);
			return true;
		}
		*/
		private void PopReportList(string searchTxt)
		{
			DataTable dt = (new RobotSystem()).GetReportList(searchTxt, base.CSrc.SrcConnectionString, base.CSrc.SrcDbPassword);
			if (dt != null)
			{
				Session[KEY_dtReportList] = dt;
				cReportList.DataSource = dt; cReportList.DataBind();
			}
		}

		protected void cEntityId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			if (dt != null)
			{
				base.CPrj = new CurrPrj(dt.Rows[cEntityId.SelectedIndex]);
				GetClientTier(Int16.Parse(cEntityId.SelectedValue));
				GetRuleTier(Int16.Parse(cEntityId.SelectedValue));
				GetDataTier(Int16.Parse(cEntityId.SelectedValue));
				Session.Remove(KEY_dtSystem);
				GetSystem();
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cEntityId.ClientID);
		}

        protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataTable dt = (DataTable)Session[KEY_dtSystem];
            bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
            if (singleSQLCredential)
            {
                dt.Rows[cSystemId.SelectedIndex]["ServerName"] = Config.DesServer;
                dt.Rows[cSystemId.SelectedIndex]["dbAppUserId"] = Config.DesUserId;
                dt.Rows[cSystemId.SelectedIndex]["dbAppPassword"] = Config.DesPassword;
            }
            base.CSrc = new CurrSrc(true, dt.Rows[cSystemId.SelectedIndex]);
            SetCTar(base.CPrj.TarDesConnectionString, base.CPrj.TarDesPassword);
            PopReportList(cSearch.Text);
            ScriptManager.GetCurrent(Parent.Page).SetFocus(cSystemId.ClientID);
        }

        protected void cClientTierId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (base.CPrj != null)
			{
				DataRow dr = ((DataTable)Session[KEY_dtClientTier]).Rows[cClientTierId.SelectedIndex];
				base.CPrj.TarClientProgramPath = dr["DevProgramPath"].ToString();
				base.CPrj.TarClientFrwork = dr["FrameworkCd"].ToString();
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cClientTierId.ClientID);
		}

		protected void cRuleTierId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (base.CPrj != null)
			{
				DataRow dr = ((DataTable)Session[KEY_dtRuleTier]).Rows[cRuleTierId.SelectedIndex];
				base.CPrj.TarRuleProgramPath = dr["DevProgramPath"].ToString();
				base.CPrj.TarRuleFrwork = dr["FrameworkCd"].ToString();
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cRuleTierId.ClientID);
		}

		protected void cDataTierId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRow dr = ((DataTable)Session[KEY_dtDataTier]).Rows[cDataTierId.SelectedIndex];
			if (base.CPrj != null && SetCTar(Config.GetConnStr(dr["DbProviderOle"].ToString(), dr["DesServer"].ToString(), dr["DesDatabase"].ToString(), "", dr["DesUserId"].ToString()), dr["DesPassword"].ToString()))
			{
				base.CPrj.TarDesProviderCd = dr["DbProviderCd"].ToString();
				base.CPrj.TarDesProvider = dr["DbProviderOle"].ToString();
				base.CPrj.TarDesServer = dr["DesServer"].ToString();
				base.CPrj.TarDesDatabase = dr["DesDatabase"].ToString();
				base.CPrj.TarDesUserId = dr["DesUserId"].ToString();
				base.CPrj.TarDesPassword = dr["DesPassword"].ToString();
				LastDataTierId = cDataTierId.SelectedIndex;
			}
			else
			{
				cDataTierId.ClearSelection(); cDataTierId.Items[LastDataTierId].Selected = true;
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cDataTierId.ClientID);
		}

		protected void cAllReport_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cAllReport.Checked) {cReportList.ClearSelection();}
		}

		private bool SetCTar(string TarDesConnectionString, string TarDesPassword)
		{
			DataTable dt = (new LoginSystem()).GetSystemsList(TarDesConnectionString, TarDesPassword);
			if (dt != null) {base.CTar = new CurrTar (true, dt.Rows[cSystemId.SelectedIndex]); return true;}
			else
			{
				cMsgLabel.Style["Color"] = "red"; cMsgLabel.Text = "Access to selected Data Tier is denied, please investigate and try again."; return false;
			}
		}

        private void PreMsgPopup(string msg)
        {
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/warning.gif";
            string focusOnCloseId = cSystemId.ClientID + @"_ctl01";
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