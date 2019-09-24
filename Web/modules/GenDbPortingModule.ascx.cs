namespace RO.Web
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Globalization;
    using System.Threading;
    using System.Text;
    using RO.Facade3;
    using RO.Rule3;
    using RO.Common3;
    using RO.Common3.Data;
    using AjaxControlToolkit;

	public partial class GenDbPortingModule : RO.Web.ModuleBase
	{
		private const string KEY_dtEntity = "Cache:dtEntity";
		private const string KEY_dtDataTier = "Cache:dtDataTier";
		private const string KEY_dtSrcSystems = "Cache:dtSrcSystems";
		private const string KEY_dtTarSystems = "Cache:dtTarSystems";
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;

		private StringBuilder sbWarnMsg = new StringBuilder("");

		public GenDbPortingModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            bErrNow.Value = "N"; bInfoNow.Value = "N";
            if (!IsPostBack)
			{
				Session.Remove(KEY_dtEntity);
				Session.Remove(KEY_dtDataTier);
				Session.Remove(KEY_dtSrcSystems);
				Session.Remove(KEY_dtTarSystems);
				GetEntity();
                bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
                if (singleSQLCredential)
                {
                    throw new Exception("Please disable shared DB credential(DesShareCred) in web.config and make sure datatier is setup probably before doing porting");
                }

                cHelpLabel.Text = "Please select the source and target servers, then the appropriate database (empty means link-server or Design.Systems table not available) and proceed to other options. Press OK button to generate the respective scripts or execute them at the same time. Check 'Clear Respective Target First' when appropriate.";
				cTitleLabel.Text = "Database Porting";
				cExemptText.Text = "('Printer','Printer_MemberId','Printer_UsrGroupId','ScreenLstInf','ScreenLstCri','ReportLstCri','ReportTmpl','Template','Systems','Usage','Usr','UsrGroup','UsrGroupAuth','UsrImpr','UsrPref')";
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

		protected void Page_Init(object sender, EventArgs e)
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
			this.cSrcDataTierId.SelectedIndexChanged += new System.EventHandler(this.cSrcDataTierId_SelectedIndexChanged);
			this.cTarDataTierId.SelectedIndexChanged += new System.EventHandler(this.cTarDataTierId_SelectedIndexChanged);
			this.cSrcSystemId.SelectedIndexChanged += new System.EventHandler(this.cSrcSystemId_SelectedIndexChanged);
			this.cTarSystemId.SelectedIndexChanged += new System.EventHandler(this.cTarSystemId_SelectedIndexChanged);
			this.cSrcSystemIdDb.SelectedIndexChanged += new System.EventHandler(this.cSrcSystemIdDb_SelectedIndexChanged);
			this.cAllScript.CheckedChanged += new System.EventHandler(this.cAllScript_CheckedChanged);
			this.cExecScript.CheckedChanged += new System.EventHandler(this.cExecScript_CheckedChanged);
			this.cTable.CheckedChanged += new System.EventHandler(this.cTable_CheckedChanged);
			this.cBcpOut.CheckedChanged += new System.EventHandler(this.cBcpOut_CheckedChanged);
			this.cBcpIn.CheckedChanged += new System.EventHandler(this.cBcpIn_CheckedChanged);
			this.cIndex.CheckedChanged += new System.EventHandler(this.cIndex_CheckedChanged);
			this.cSp.CheckedChanged += new System.EventHandler(this.cSp_CheckedChanged);

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
				cEntityId.DataSource = dt;
				cEntityId.DataBind();
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
						base.CPrj = new CurrPrj(dt.Rows[cEntityId.SelectedIndex]);
					}
					GetSrcDataTier();
					GetTarDataTier();
				}
			}
		}

		private void GetSrcDataTier()
		{
			DataTable dt = (DataTable)Session[KEY_dtDataTier];
			if (dt == null) { dt = (new RobotSystem()).GetDataTier(Int16.Parse(cEntityId.SelectedValue)); }
			if (dt != null)
			{
				Session[KEY_dtDataTier] = dt;
				cSrcDataTierId.DataSource = dt;
				cSrcDataTierId.DataBind();
				if (cSrcDataTierId.Items.Count > 0) {cSrcDataTierId.Items[0].Selected = true; GetSrcSystems(0);}
			}
		}

		private void GetTarDataTier()
		{
			DataTable dt = (DataTable)Session[KEY_dtDataTier];
			if (dt == null) { dt = (new RobotSystem()).GetDataTier(Int16.Parse(cEntityId.SelectedValue)); }
			if (dt != null)
			{
				Session[KEY_dtDataTier] = dt;
				cTarDataTierId.DataSource = dt;
				cTarDataTierId.DataBind();
				if (cTarDataTierId.Items.Count > 0) {cTarDataTierId.Items[0].Selected = true; GetTarSystems(cSrcSystemId.SelectedIndex);}
			}
		}

        private void GetSrcSystems(int iSel)
        {
            DataTable dt = (DataTable)Session[KEY_dtSrcSystems];
            if (dt == null)
            {
                DataTable dtSrc = (DataTable)Session[KEY_dtDataTier];
                dt = (new LoginSystem()).GetSystemsList(Config.GetConnStr(dtSrc.Rows[cSrcDataTierId.SelectedIndex]["DbProviderOle"].ToString(), dtSrc.Rows[cSrcDataTierId.SelectedIndex]["DesServer"].ToString(), dtSrc.Rows[cSrcDataTierId.SelectedIndex]["DesDatabase"].ToString(), "", dtSrc.Rows[cSrcDataTierId.SelectedIndex]["DesUserId"].ToString()), dtSrc.Rows[cSrcDataTierId.SelectedIndex]["DesPassword"].ToString());
            }
            if (dt != null)
            {
                cSrcSystemIdDb.Visible = true;
                Session[KEY_dtSrcSystems] = dt;
                cSrcSystemId.DataSource = dt;
                cSrcSystemId.DataBind();
                if (cSrcSystemId.Items.Count > 0)
                {
                    cSrcSystemId.Items[iSel].Selected = true;
                    if (dt.Rows[cSrcSystemId.SelectedIndex]["dbAppDatabase"].ToString() == dt.Rows[cSrcSystemId.SelectedIndex]["dbDesDatabase"].ToString())
                    {
                        cSrcSystemIdDb.Items[0].Value = string.Empty; cSrcSystemIdDb.Items[0].Text = string.Empty; cSrcSystemIdDb.Items[1].Selected = true;
                    }
                    else
                    {
                        cSrcSystemIdDb.Items[0].Value = dt.Rows[cSrcSystemId.SelectedIndex]["dbAppDatabase"].ToString();
                        cSrcSystemIdDb.Items[0].Text = dt.Rows[cSrcSystemId.SelectedIndex]["dbAppServer"].ToString() + ". " + dt.Rows[cSrcSystemId.SelectedIndex]["dbAppDatabase"].ToString();
                    }
                    cSrcSystemIdDb.Items[1].Value = dt.Rows[cSrcSystemId.SelectedIndex]["dbDesDatabase"].ToString();
                    cSrcSystemIdDb.Items[1].Text = dt.Rows[cSrcSystemId.SelectedIndex]["dbAppServer"].ToString() + ". " + dt.Rows[cSrcSystemId.SelectedIndex]["dbDesDatabase"].ToString();
                    base.CSrc = new CurrSrc(true, dt.Rows[cSrcSystemId.SelectedIndex]);
                    base.CSrc.SrcDbDatabase = cSrcSystemIdDb.SelectedValue;
                }
            }
            else
            {
                cSrcSystemId.Items.Clear(); cSrcSystemIdDb.Visible = false;
            }
        }

        private void GetTarSystems(int iSel)
        {
            DataTable dt = (DataTable)Session[KEY_dtTarSystems];
            if (dt == null)
            {
                DataTable dtTar = (DataTable)Session[KEY_dtDataTier];
                dt = (new LoginSystem()).GetSystemsList(Config.GetConnStr(dtTar.Rows[cTarDataTierId.SelectedIndex]["DbProviderOle"].ToString(), dtTar.Rows[cTarDataTierId.SelectedIndex]["DesServer"].ToString(), dtTar.Rows[cTarDataTierId.SelectedIndex]["DesDatabase"].ToString(), "", dtTar.Rows[cTarDataTierId.SelectedIndex]["DesUserId"].ToString()), dtTar.Rows[cTarDataTierId.SelectedIndex]["DesPassword"].ToString());
            }
            if (dt != null)
            {
                cTarSystemIdDb.Visible = true;
                Session[KEY_dtTarSystems] = dt;
                cTarSystemId.DataSource = dt;
                cTarSystemId.DataBind();
                if (cTarSystemId.Items.Count > iSel)
                {
                    cTarSystemId.Items[iSel].Selected = true;
                    if (dt.Rows[cTarSystemId.SelectedIndex]["dbAppDatabase"].ToString() == dt.Rows[cTarSystemId.SelectedIndex]["dbDesDatabase"].ToString())
                    {
                        cTarSystemIdDb.Items[0].Value = string.Empty; cTarSystemIdDb.Items[0].Text = string.Empty;
                    }
                    else
                    {
                        cTarSystemIdDb.Items[0].Value = dt.Rows[cTarSystemId.SelectedIndex]["dbAppDatabase"].ToString();
                        cTarSystemIdDb.Items[0].Text = dt.Rows[cTarSystemId.SelectedIndex]["dbAppServer"].ToString() + ". " + dt.Rows[cTarSystemId.SelectedIndex]["dbAppDatabase"].ToString();
                    }
                    cTarSystemIdDb.Items[1].Value = dt.Rows[cTarSystemId.SelectedIndex]["dbDesDatabase"].ToString();
                    cTarSystemIdDb.Items[1].Text = dt.Rows[cTarSystemId.SelectedIndex]["dbAppServer"].ToString() + ". " + dt.Rows[cTarSystemId.SelectedIndex]["dbDesDatabase"].ToString();
                    cTarSystemIdDb.Items[0].Selected = cSrcSystemIdDb.Items[0].Selected;
                    cTarSystemIdDb.Items[1].Selected = cSrcSystemIdDb.Items[1].Selected;
                    base.CTar = new CurrTar(true, dt.Rows[cTarSystemId.SelectedIndex]);
                    base.CTar.TarDbDatabase = cTarSystemIdDb.SelectedValue;
                }
                else
                {
                    cTarSystemIdDb.Items[0].Value = string.Empty; cTarSystemIdDb.Items[1].Value = string.Empty;
                    cTarSystemIdDb.Items[0].Text = string.Empty; cTarSystemIdDb.Items[1].Text = string.Empty;
                }
            }
            else
            {
                cTarSystemId.Items.Clear(); cTarSystemIdDb.Visible = false;
            }
        }

        protected void cGenButton_Click(object sender, System.EventArgs e)
		{
			cMsgLabel.Text = string.Empty;
			if (!(cSrcSystemId.Items.Count > 0 && cTarSystemId.Items.Count > 0))
			{
				PreMsgPopup("Please make sure the table \"Systems\" in both source and target have valid database entries and try again.");
			}
			else if (cSrcSystemId.SelectedValue != cTarSystemId.SelectedValue)
			{
				PreMsgPopup("Please make sure the source and target \"Database\" are the same and try again.");
			}
			else if (cExecScript.Checked && cSrcDataTierId.SelectedValue == cTarDataTierId.SelectedValue)
			{
				PreMsgPopup("Source and Target data tier can be the same if and only if script is not being executed, please try again.");
			}
			else
			{
				string ss;
				DataTable dt = (DataTable)Session[KEY_dtDataTier];
				string SrcDbProviderCd = dt.Rows[cSrcDataTierId.SelectedIndex]["DbProviderCd"].ToString();
				string SrcPortBinPath = dt.Rows[cSrcDataTierId.SelectedIndex]["PortBinPath"].ToString();
				string SrcScriptPath = dt.Rows[cSrcDataTierId.SelectedIndex]["ScriptPath"].ToString();
				string TarDbProviderCd = dt.Rows[cTarDataTierId.SelectedIndex]["DbProviderCd"].ToString();
				string TarPortBinPath = dt.Rows[cTarDataTierId.SelectedIndex]["PortBinPath"].ToString();
				string TarPortCmdName;
				if (TarDbProviderCd == "S") {TarPortCmdName = TarPortBinPath + "isql.exe";} else {TarPortCmdName = "osql.exe";}
				string TarScriptPath = dt.Rows[cTarDataTierId.SelectedIndex]["ScriptPath"].ToString();
				string TarUdFunctionDb = dt.Rows[cTarDataTierId.SelectedIndex]["DesDatabase"].ToString();
				DbScript ds = new DbScript(cExemptText.Text, false);
				DbPorting pt = new DbPorting();
				if (cClearDb.Checked)
				{
					ss = ds.ScriptClearDb(SrcDbProviderCd, TarDbProviderCd, cTable.Checked, cBcpIn.Checked, cIndex.Checked, cView.Checked, cSp.Checked, base.CSrc, base.CTar);
					Robot.WriteToFile("M",SrcScriptPath + "OClearDb.sql",ss);
					if (TarDbProviderCd == "S") { ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw); }
					Robot.WriteToFile("M",TarScriptPath + "NClearDb.sql",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
					if (cExecScript.Checked) {ds.ExecScript (TarDbProviderCd, "Clear Database", TarPortCmdName, TarScriptPath + "NClearDb.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw);}
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cTable.Checked)
				{
					ss = ds.ScriptCreateTables(SrcDbProviderCd, SrcDbProviderCd, true, base.CSrc, base.CTar);
					Robot.WriteToFile("M",SrcScriptPath + "OTable.sql",ss);
					if (TarDbProviderCd == "S")
					{
						ss = ds.ScriptCreateTables(SrcDbProviderCd, TarDbProviderCd, true, base.CSrc, base.CTar);
						ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw);
					}
					Robot.WriteToFile("M",TarScriptPath + "NTable.sql",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked) { ds.ExecScript(TarDbProviderCd, "Script Tables", TarPortCmdName, TarScriptPath + "NTable.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw); }
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cBcpOut.Checked)
				{
					ss = ds.GenerateBCPFiles("M", TarDbProviderCd, SrcDbProviderCd, SrcPortBinPath, true, TarScriptPath + @"Data\", "~@~", false, base.CSrc, base.CTar);
					Robot.WriteToFile("M",TarScriptPath + "BcpOut.bat",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked) { ds.ExecScript(string.Empty, "Bcp Out", TarScriptPath + "BcpOut.bat", string.Empty, base.CSrc, base.CTar, LcAppConnString, LcAppPw); }
					sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cBcpIn.Checked)
				{
					ss = ds.ScriptTruncData(SrcDbProviderCd, true, base.CSrc, base.CTar);
					if (TarDbProviderCd == "S") { ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw); }
					Robot.WriteToFile("M",TarScriptPath + "BcpIn.sql",ss);
					ss = ds.GenerateBCPFiles("M", TarDbProviderCd, TarDbProviderCd, TarPortBinPath, false, TarScriptPath + @"Data\", "~@~", false, base.CSrc, base.CTar);
					Robot.WriteToFile("M",TarScriptPath + "BcpIn.bat",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked)
					{
						ds.ExecScript (TarDbProviderCd, "Truncate Data", TarPortCmdName, TarScriptPath + "BcpIn.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw);
						ds.ExecScript (TarDbProviderCd, "Bcp In", TarScriptPath + "BcpIn.bat", string.Empty, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					}
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cIndex.Checked)
				{
					ss = ds.ScriptIndexFK(SrcDbProviderCd, SrcDbProviderCd, true, base.CSrc, base.CTar);
					Robot.WriteToFile("M",SrcScriptPath + "OIndex.sql",ss);
					if (TarDbProviderCd == "S") 
					{
						ss = ds.ScriptIndexFK(SrcDbProviderCd, TarDbProviderCd, true, base.CSrc, base.CTar);
						ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw);
					}
					Robot.WriteToFile("M",TarScriptPath + "NIndex.sql",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked) { ds.ExecScript(TarDbProviderCd, "Script Indexes", TarPortCmdName, TarScriptPath + "NIndex.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw); }
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cView.Checked)
				{
					ss = ds.ScriptView(SrcDbProviderCd, SrcDbProviderCd, true, base.CSrc, base.CTar);
					Robot.WriteToFile("M",SrcScriptPath + "OView.sql",ss);
					if (TarDbProviderCd == "S") 
					{
						ss = ds.ScriptView(SrcDbProviderCd, TarDbProviderCd, true, base.CSrc, base.CTar);
						ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw);
					}
					Robot.WriteToFile("M",TarScriptPath + "NView.sql",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked) { ds.ExecScript(TarDbProviderCd, "Script View", TarPortCmdName, TarScriptPath + "NView.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw); }
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (cSp.Checked)
				{
					ss = ds.ScriptSProcedures(SrcDbProviderCd, SrcDbProviderCd, true, base.CSrc, base.CTar);
					Robot.WriteToFile("M",SrcScriptPath + "OSp.sql",ss);
					if (TarDbProviderCd == "S") 
					{
						ss = ds.ScriptSProcedures(SrcDbProviderCd, TarDbProviderCd, true, base.CSrc, base.CTar);
						ss = pt.SqlToSybase(Int16.Parse(cEntityId.SelectedValue), TarUdFunctionDb, ss, LcAppConnString, LcAppPw);
					}
					if (cEncryptSp.Checked) {ss = ds.EncryptSProcedures(SrcDbProviderCd,TarDbProviderCd,ss,true,base.CSrc,base.CTar);}
					Robot.WriteToFile("M",TarScriptPath + "NSp.sql",ss);
                    // ds.ExecScript need to be changed to Utils.WinProc in order for SQL Server on a separate server to work:
                    if (cExecScript.Checked) { ds.ExecScript(TarDbProviderCd, "Script Stored Procedures", TarPortCmdName, TarScriptPath + "NSp.sql", base.CSrc, base.CTar, LcAppConnString, LcAppPw); }
					sbWarnMsg.Append(pt.sbWarning.ToString()); sbWarnMsg.Append(ds.sbWarning.ToString());
				}
				if (SrcScriptPath != TarScriptPath)
				{
					cMsgLabel.Text = "Please check directories " + TarScriptPath + " and " + TarScriptPath + " to see if all the scripts have been generated successfully.";
				}
				else
				{
					cMsgLabel.Text = "Please check directory " + TarScriptPath + " to see if all the scripts have been generated successfully.";
				}
				if (sbWarnMsg.ToString() != string.Empty) {PreMsgPopup(sbWarnMsg.ToString());}
			}
		}

        protected void cEntityId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			base.CPrj = new CurrPrj(dt.Rows[cEntityId.SelectedIndex]);
			Session.Remove(KEY_dtDataTier);
			Session.Remove(KEY_dtSrcSystems);
			Session.Remove(KEY_dtTarSystems);
			GetSrcDataTier();
			GetTarDataTier();
		}

		private void cSrcDataTierId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Session.Remove(KEY_dtSrcSystems);
			GetSrcSystems(0);
		}

		private void cTarDataTierId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Session.Remove(KEY_dtTarSystems);
			GetTarSystems(cSrcSystemId.SelectedIndex);
		}

		private void cSrcSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GetSrcSystems(cSrcSystemId.SelectedIndex);
			GetTarSystems(cSrcSystemId.SelectedIndex);
		}

		private void cTarSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GetTarSystems(cTarSystemId.SelectedIndex);
		}

		private void cSrcSystemIdDb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cSrcSystemIdDb.SelectedItem.Text == string.Empty) {cSrcSystemIdDb.Items[1].Selected = true;}
			base.CSrc.SrcDbDatabase = cSrcSystemIdDb.SelectedValue;
			if (cTarSystemId.Items.Count > 0)
			{
				cTarSystemIdDb.Items[0].Selected = cSrcSystemIdDb.Items[0].Selected;
				cTarSystemIdDb.Items[1].Selected = cSrcSystemIdDb.Items[1].Selected;
				base.CTar.TarDbDatabase = cTarSystemIdDb.SelectedValue;
			}
		}

		private void cAllScript_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cAllScript.Checked)
			{
				cTable.Checked = true;
				cBcpOut.Checked = true;
				cBcpIn.Checked = true;
				cIndex.Checked = true;
				cView.Checked = true;
				cSp.Checked = true;
			}
			else
			{
				cTable.Checked = false;
				cBcpOut.Checked = false;
				cBcpIn.Checked = false;
				cIndex.Checked = false;
				cView.Checked = false;
				cSp.Checked = false;
			}
			if (cSp.Checked) {cEncryptSp.Enabled = true;} else {cEncryptSp.Enabled = false; cEncryptSp.Checked = false;}
		}

		private void cExecScript_CheckedChanged(object sender, System.EventArgs e)
		{
		}

		private void cTable_CheckedChanged(object sender, System.EventArgs e)
		{
			cAllScript.Checked = false;
		}

		private void cBcpOut_CheckedChanged(object sender, System.EventArgs e)
		{
			cAllScript.Checked = false;
		}

		private void cBcpIn_CheckedChanged(object sender, System.EventArgs e)
		{
			cAllScript.Checked = false;
		}

		private void cIndex_CheckedChanged(object sender, System.EventArgs e)
		{
			cAllScript.Checked = false;
		}

		private void cSp_CheckedChanged(object sender, System.EventArgs e)
		{
			cAllScript.Checked = false;
			if (cSp.Checked) {cEncryptSp.Enabled = true;} else {cEncryptSp.Enabled = false; cEncryptSp.Checked = false;}
		}

        private void PreMsgPopup(string msg)
        {
            int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
            string iconUrl = "images/warning.gif";
            string focusOnCloseId = string.Empty;
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