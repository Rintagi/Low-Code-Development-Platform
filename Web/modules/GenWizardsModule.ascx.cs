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

	public partial class GenWizardsModule : RO.Web.ModuleBase
	{
		private const string KEY_dtEntity = "Cache:dtEntity";
		private const string KEY_dtSystem = "Cache:dtSystem";
		private const string KEY_dtClientTier = "Cache:dtClientTier";
		private const string KEY_dtRuleTier = "Cache:dtRuleTier";
		private const string KEY_dtDataTier = "Cache:dtDataTier";
		private const string KEY_dtWizardList = "Cache:dtWizardList";
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;

		private int LastDataTierId = 0;

		public GenWizardsModule()
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
				Session.Remove(KEY_dtWizardList);
				GetEntity();
                cHelpLabel.Text = "Please select the appropriate project and database; then select one or more wizards or check 'All Wizards'. Click the 'Create' button to generate the respective programs. All default client/rule/data tiers will be regenerated regardless of the selected targets. Any residual programs of 'deleted wizard(s)' will be removed in this process.";
				cTitleLabel.Text = "Generate Wizards";
				if (cWizardList.Attributes["OnChange"] == null || cWizardList.Attributes["OnChange"].IndexOf("_cAllWizard") < 0) { cWizardList.Attributes["OnChange"] += "document.getElementById('" + cAllWizard.ClientID + "').checked = false;"; }
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
			PopWizardList(cSearch.Text);
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
			GenWizardsSystem GenSys = new GenWizardsSystem();
			// Do the removal first.
			DataView dvDel = new DataView(GenSys.GetWizardDel(base.CSrc.SrcDbDatabase, base.CSrc.SrcConnectionString, base.CSrc.SrcDbPassword));
			if (dvDel != null)
			{
				foreach (DataRowView drv in dvDel)
				{
					GenSys.DeleteProgram(drv["ProgramName"].ToString(), Int32.Parse(drv["WizardId"].ToString()), drv["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar);
				}
			}
			cMsgLabel.Style["Color"] = "blue"; cMsgLabel.Text = string.Empty;
			int iGen = 0;
			string sGen = string.Empty;
			DataView dv = new DataView((DataTable)Session[KEY_dtWizardList]);
			if (dv != null)
			{
				DataTable dt = (DataTable)Session[KEY_dtSystem];
				int ii = 0;
				foreach (DataRowView drv in dv)
				{
					if (cAllWizard.Checked || cWizardList.Items[ii].Selected)
					{
						if (dt != null && GenSys.CreateProgram(Int32.Parse(drv["WizardId"].ToString()), drv["WizardTitle"].ToString(), dt.Rows[cSystemId.SelectedIndex]["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw)) {iGen = iGen + 1;} 
					}
					ii = ii + 1;
				}
				//if (iGen >= 1) { sGen = "s"; Robot.CompileProxy(base.CPrj, base.CSrc); }
                //ii = 0;
                //foreach (DataRowView drv in dv)
                //{
                //    if (cAllWizard.Checked || cWizardList.Items[ii].Selected)
                //    {
                //        GenSys.ProxyProgram(Int32.Parse(drv["WizardId"].ToString()), base.CPrj, base.CSrc);
                //    }
                //    ii = ii + 1;
                //}
                if (iGen >= 1) { sGen = "s"; }
                cMsgLabel.Text = iGen.ToString() + " wizard" + sGen + " generated successfully.";
			}
			ScriptManager.GetCurrent(Parent.Page).SetFocus(cSystemId.ClientID);
		}

		private void PopWizardList(string searchTxt)
		{
			DataTable dt = (new RobotSystem()).GetWizardList(searchTxt, base.CSrc.SrcConnectionString, base.CSrc.SrcDbPassword);
			if (dt != null)
			{
				Session[KEY_dtWizardList] = dt;
				cWizardList.DataSource = dt; cWizardList.DataBind();
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
            PopWizardList(cSearch.Text);
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

		protected void cAllWizard_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cAllWizard.Checked) {cWizardList.ClearSelection();}
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