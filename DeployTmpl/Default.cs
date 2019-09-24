using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Install
{
	public partial class Default : Form
	{
		Item item = new Item();			// Version info.
		ItemPDT iPDT = new ItemPDT();		// Existing Rbt:Rintagi/App:Production.
		ItemDEV iDEV = new ItemDEV();		// Existing Rbt:Developer/App:Extranet.
		ItemPTY iPTY = new ItemPTY();		// Existing Rbt:Application/App:Prototype.
		ItemNPDT nPDT = new ItemNPDT();		// New Rbt:Rintagi/App:Production.
		ItemNDEV nDEV = new ItemNDEV();	// New Rbt:Developer/App:Extranet.
		ItemNPTY nPTY = new ItemNPTY();		// New Rbt:Application/App:Prototype.

		public Default()
		{
			InitializeComponent();

            copyright.Text = "V3.1.30501 �1999-" + DateTime.Now.Year.ToString() + " robocoder corporation. All rights reserved.";
            item.SetVersion(lvVersion);
			txtOldNS.Text = item.GetOldNS();
			txtWebServer.Text = "localhost";
            cbSingleDeveloper.Checked = true;
            cbOverWrite.Visible = txtOldNS.Text == "RO" && item.GetInsType().Contains("PTY") && cbNew.Checked;
            cbOverWrite.Checked = txtOldNS.Text == "RO" && item.GetInsType().Contains("PTY");
            cbSingleDeveloper.Visible = false;
            if (txtOldNS.Text == "RO" && !item.GetInsType().Contains("PTY"))
			{
				rbDevUpgrade.Text = "Developer"; rbPtyUpgrade.Text = "Production";
				rbPdtUpgrade.Visible = false;
				if (item.GetInsType() == "DEV" || item.GetInsType() == "NDEV")
				{
					rbDevUpgrade.Enabled = true; rbPtyUpgrade.Enabled = false;
					rbDevUpgrade.Checked = true; rbDevUpgrade_CheckedChanged(null, new EventArgs());
				}
				else	//item.GetInsType() == "PDT"
				{
					rbDevUpgrade.Enabled = false; rbPtyUpgrade.Enabled = true;
					rbPtyUpgrade.Checked = true; rbPtyUpgrade_CheckedChanged(null, new EventArgs());
				}
			}
			else
			{
				rbPdtUpgrade.Text = "Production"; rbPtyUpgrade.Text = "Prototype";
				rbDevUpgrade.Visible = false;
				if (item.GetInsType() == "PDT" || item.GetInsType() == "NPDT")
				{
					rbPdtUpgrade.Enabled = true; rbPtyUpgrade.Enabled = false;
					rbPdtUpgrade.Checked = true; rbPdtUpgrade_CheckedChanged(null, new EventArgs());
				}
				else	//item.GetInsType() == "PTY"
				{
					rbPdtUpgrade.Enabled = false; rbPtyUpgrade.Enabled = true;
					rbPtyUpgrade.Checked = true; rbPtyUpgrade_CheckedChanged(null, new EventArgs());
				}
			}
			InitializeTiers(txtOldNS.Text, txtWebServer.Text);
            //rbCFrwork4.Visible = false;
            //rbCFrwork3.Visible = false;
            string[] v = System.Environment.Version.ToString().Split(new char[] { '.' });
            if (v[0] == "4" || true)
            {
                rbCFrwork4.Checked = true; 
                rbRFrwork4.Checked = true;
            }
            else
            {
                rbCFrwork3.Checked = true;
                rbRFrwork3.Checked = true;
            }
			rbDbProvider10.Checked = true;
            txtDbPath.Visible = false;
            lblDbPath.Visible = false;
            rbDbProvider10.Visible = false;
            rbDbProvider12.Visible = false;
            rbDbProvider14.Visible = false;
            rbDbProvider16.Visible = false;

			InitPanel(false);
		}

		private void InitPanel(bool bOn)
		{
			if (bOn)
			{
				rtfLicense.Visible = false;
				btnCancel.Text = "Cancel";
				btnInstall.Text = "Install";
				gbUpgrade.Visible = true;
				gbNmspace.Visible = true;
				gbClient.Visible = true;
				gbWs.Visible = true;
				gbXls.Visible = true;
				gbBackup.Visible = true;
				if (txtOldNS.Text == "RO")
				{
					if (item.GetInsType() == "DEV" || item.GetInsType() == "NDEV")
					{
						rbDevUpgrade_CheckedChanged(null, new EventArgs());
					}
                    else if (item.GetInsType().Contains("PTY"))
                    {
                        rbPtyUpgrade_CheckedChanged(null, new EventArgs());
                    }
                    else	//item.GetInsType() == "PDT"
                    {
                        rbPtyUpgrade_CheckedChanged(null, new EventArgs());
                    }
				}
				else
				{
					if (item.GetInsType() == "PDT" || item.GetInsType() == "NPDT")
					{
						rbPdtUpgrade_CheckedChanged(null, new EventArgs());
					}
					else	//item.GetInsType() == "PTY"
					{
						rbPtyUpgrade_CheckedChanged(null, new EventArgs());
					}
				}
			}
			else
			{
				rtfLicense.Visible = true; rtfLicense.Text = Utils.ExtractTxt("License.txt");
				btnCancel.Text = "Decline";
				btnInstall.Text = "Accept";
				gbUpgrade.Visible = false;
				gbNmspace.Visible = false;
				gbClient.Visible = false;
				gbWs.Visible = false;
				gbXls.Visible = false;
				gbRptWs.Visible = false;
				gbRule.Visible = false;
				gbData.Visible = false;
                gbBackup.Visible = false;
			}
		}

		private void InitializeTiers(string NmSpace, string WebServer)
		{
			if (txtOldNS.Text == "RO") { label8.Text = "Rintagi Installer"; } else { label8.Text = "Application Installer"; }
            string oldBasePath = "C:\\inetpub\\wwwroot\\";
            NmSpace = string.IsNullOrEmpty(NmSpace) ? (txtOldNS.Text == "RO" ? "ZZ" : txtOldNS.Text) : NmSpace;
            bool newRuleStructure = Directory.Exists("C:\\Rintagi\\" + NmSpace + "\\Rule") || cbNew.Checked || (!cbNew.Checked && (item.GetInsType().Contains("PDT") && Directory.Exists("C:\\Rintagi\\" + NmSpace + "\\Web")));
            if (Directory.Exists(oldBasePath + NmSpace) && !cbNew.Checked)
            {
                txtClientTier.Text = oldBasePath + NmSpace + "\\Web";
                txtRuleTier.Text = "C:\\Rintagi\\" + NmSpace;
                txtWsTier.Text = oldBasePath + NmSpace + "Ws";
                txtXlsTier.Text = oldBasePath + "WsXls";
            }
            else
            {
                txtClientTier.Text = "C:\\Rintagi\\" + NmSpace + "\\Web";
                txtRuleTier.Text = "C:\\Rintagi\\" + NmSpace + (newRuleStructure && false ? "\\Rule" : "");
                txtWsTier.Text = "C:\\Rintagi\\" + NmSpace + "\\" + NmSpace + "Ws";
                txtXlsTier.Text = "C:\\Rintagi\\" + (item.GetInsType().Contains("PDT") ? "" : NmSpace + "\\") + "WsXls";
            }
            txtWsUrl.Text = "http://" + WebServer + "/ReportServer/ReportService2005.asmx"; txtWsUrl.Visible = false;	// Obsolete Feb 21, 2011.
        }

        private void btnBackup_Click(object sender, EventArgs e)
		{
			string uniqueStr = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
			//string Winzip = "\"C:\\Program Files\\WinZip\\winzip32.exe\"";
			ProgressInd.Maximum = 100; ProgressInd.Value = 0; lblCurrent.Text = string.Empty;
			if (!txtBkPath1.Visible)
			{
				lblBkPath1.Visible = true; txtBkPath1.Visible = true;
				lblBkPath2.Visible = true; txtBkPath2.Visible = true;
			}
			else
			{
				string MsgValid = ValidateControls(true);
				if (MsgValid != string.Empty) { MessageBox.Show(MsgValid); }
				else
				{
                    if (cbInstallDB.Checked && !Utils.TestSQL("M", txtServerName.Text, txtUserName.Text, txtPassword.Text, cbIntegratedSecurity.Checked)) return;

                    btnBackup.Enabled = false;
					lblBkPath1.Visible = false; txtBkPath1.Visible = false;
					lblBkPath2.Visible = false; txtBkPath2.Visible = false;
                    Dictionary<string, string> tiers = new Dictionary<string, string>();
                    Dictionary<string, string> dataServer = new Dictionary<string, string>();
                    tiers["client"] = txtClientTier.Text;
                    tiers["ws"] = txtWsTier.Text;
                    tiers["xls"] = txtXlsTier.Text;
                    if (gbRule.Visible) tiers["rule"] = txtRuleTier.Text;
                    dataServer["serverType"] = rbDbProvider16.Checked || rbDbProvider10.Checked || rbDbProvider12.Checked || rbDbProvider14.Checked || true ? "M" : "S";
                    dataServer["server"] = txtServerName.Text;
                    dataServer["user"] = txtUserName.Text;
                    dataServer["password"] = txtPassword.Text;
                    dataServer["design"] = txtNewNS.Text + "Design";
                    Action<int, string> progress = (int p, string msg) => 
                    {
                        try
                        {
                            ProgressInd.Value += p;
                        }
                        catch { }
                        lblCurrent.Text = msg;
                        Application.DoEvents();
                    };
                    if (!cbInstallDB.Checked) dataServer["serverType"] = "";
                    bool done = Utils.Backup(tiers, dataServer, txtBkPath1.Text + "\\" + txtNewNS.Text + uniqueStr, txtBkPath2.Text + "\\" + txtNewNS.Text + uniqueStr, progress, cbIntegratedSecurity.Checked);
                    if (!done)
                    {
                        btnBackup.Enabled = true;
                        lblBkPath1.Visible = true; txtBkPath1.Visible = true;
                        lblBkPath2.Visible = true; txtBkPath2.Visible = true;
                    }
				}
			}
			ProgressInd.Value = 0; Application.DoEvents();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnInstall_Click(object sender, EventArgs e)
		{
 
			if (rtfLicense.Visible)
			{
				InitPanel(true);
			}
			else
			{
				// Cannot try & catch because Sybase s.proc. creation may trigger an error when one s.proc. depends on another:
 
                ProgressInd.Maximum = 100; ProgressInd.Value = 0; lblCurrent.Text = string.Empty;
				string MsgValid = ValidateControls(false);
				if (MsgValid != string.Empty) { MessageBox.Show(MsgValid); }
				else
				{
                    int ver = rbDbProvider16.Checked ? 13 : (rbDbProvider10.Checked ? 10 : ( rbDbProvider12.Checked ? 11 : 12));
                    if (cbInstallDB.Checked)
                    {
                        if (!Utils.TestSQL("M", txtServerName.Text, txtUserName.Text, txtPassword.Text, cbIntegratedSecurity.Checked)) return;
                        KeyValuePair<string, string> bcpPath = Utils.GetSQLBcpPath();
                        if (string.IsNullOrEmpty(bcpPath.Key))
                        {
                            MessageBox.Show(rbDbProvider16.Checked ? "SQL Server 2016 client is not installed on this machine" : (rbDbProvider10.Checked ? "SQL Server 2008 client is not installed on this machine" : (rbDbProvider12.Checked ? "SQL Server 2012 client is not installed on this machine" : "SQL Server 2014 client is not installed on this machine")));
                            return;
                        }
                    }
                    else if (cbNew.Checked)
                    {
                        if (!Utils.TestSQL("M", txtServerName.Text, txtUserName.Text, txtPassword.Text, cbIntegratedSecurity.Checked)) return;
                    }
                    if (btnBackup.Enabled && !cbNew.Checked)
                    {
                        DialogResult r = MessageBox.Show(
                            "You haven't made a backup yet, are you sure?\r\rPress OK to SKIP BACKUP and\r\rproceed to upgrade!",
                            "Upgrade WITHOUT BACKUP",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Exclamation
                            );
                        if (r != System.Windows.Forms.DialogResult.OK) return;
                    }

					picLog.Visible = false;
					lblBkPath1.Visible = false; txtBkPath1.Visible = false;
					lblBkPath2.Visible = false; txtBkPath2.Visible = false;
					lblCurrent.Text = "Deployment started ...";
					ToggleControls();
					Utils.DeleteFile("error.txt");
					Utils.DeleteFile("Install.log");
					if (cbNew.Checked) { DoNew(); } else { DoExist(); }
					ToggleControls();
					lblCurrent.Text = "Deployment completed.";
					picLog.Visible = true;
				}
				ProgressInd.Value = 0; Application.DoEvents();
			}
		}

		// Take care of eixsting Rintagi or applications:
		private void DoExist()
		{
            Dictionary<string, string> tiers = new Dictionary<string, string>();
            Dictionary<string, string> dataServer = new Dictionary<string, string>();
            tiers["client"] = txtClientTier.Text;
            tiers["ws"] = txtWsTier.Text;
            tiers["xls"] = txtXlsTier.Text;
            if (gbRule.Visible) tiers["rule"] = txtRuleTier.Text;
            else tiers["rule"] = "";
            tiers["newNS"] = txtNewNS.Text;
            tiers["wsUrl"] = txtWsUrl.Text;
            tiers["isNet2"] = rbCFrwork3.Checked ? "Y" : "N";
            tiers["webServer"] = txtWebServer.Text;
            dataServer["serverType"] = rbDbProvider16.Checked || rbDbProvider10.Checked || rbDbProvider12.Checked || rbDbProvider14.Checked || true ? "M" : "S"; ;
            dataServer["server"] = txtServerName.Text;
            dataServer["user"] = txtUserName.Text;
            dataServer["password"] = txtPassword.Text;
            dataServer["appUser"] = txtAppUserName.Text;
            dataServer["appPwd"] = txtAppPassword.Text;
            dataServer["dbpath"] = cbInstallDB.Checked ? txtDbPath.Text : "";
            dataServer["design"] = txtNewNS.Text + "Design";
            dataServer["serverVer"] = rbDbProvider16.Checked ? "130" : (rbDbProvider10.Checked ? "100" : (rbDbProvider12.Checked ? "110" : "120"));
            dataServer["IntegratedSecurity"] = cbIntegratedSecurity.Checked ? "Y" : "N";
           
            Action<int, string> progress = (int p, string msg) =>
            {
                try
                {
                    ProgressInd.Value += p;
                }
                catch { }
                if (!string.IsNullOrEmpty(msg)) lblCurrent.Text = msg;
                Application.DoEvents();
            };
            Utils.UpgradeApp(tiers, dataServer, "", "", progress, item, iPDT, iDEV, iPTY, nPDT, nDEV, nPTY);
		}

		// Take care of new Rintagi or applications:
		private void DoNew()
		{
            Dictionary<string, string> tiers = new Dictionary<string, string>();
            Dictionary<string, string> dataServer = new Dictionary<string, string>();
            tiers["client"] = txtClientTier.Text;
            tiers["ws"] = txtWsTier.Text;
            tiers["xls"] = txtXlsTier.Text;
            if (gbRule.Visible) tiers["rule"] = txtRuleTier.Text;
            else tiers["rule"] = "";

            tiers["site"] = "Default Web Site";

            tiers["newNS"] = txtNewNS.Text;
            tiers["wsUrl"] = txtWsUrl.Text;
            tiers["isNet2"] = rbCFrwork3.Checked ? "Y" : "N";
            tiers["enable32Bit"] = cb32Bit.Checked ? "Y" : "N";
            tiers["webServer"] = txtWebServer.Text;
            dataServer["serverType"] = rbDbProvider16.Checked || rbDbProvider10.Checked || rbDbProvider12.Checked || rbDbProvider14.Checked || true ? "M" : "S";
            dataServer["server"] = txtServerName.Text;
            dataServer["user"] = txtUserName.Text;
            dataServer["password"] = txtPassword.Text;
            dataServer["appUser"] = string.IsNullOrEmpty(txtAppUserName.Text) ? txtUserName.Text : txtAppUserName.Text;
            dataServer["appPwd"] = string.IsNullOrEmpty(txtAppPassword.Text) ? txtPassword.Text : txtAppPassword.Text; ; 
            dataServer["dbpath"] = cbInstallDB.Checked ? "DefaultDataPath" : ""; //
            dataServer["design"] = txtNewNS.Text + "Design";
            dataServer["serverVer"] = rbDbProvider16.Checked ? "130" : (rbDbProvider10.Checked ? "100" : (rbDbProvider12.Checked ? "110" : "120"));
            dataServer["IntegratedSecurity"] = cbIntegratedSecurity.Checked ? "Y" : "N";
            Action<int, string> progress = (int p, string msg) =>
            {
                try
                {
                    ProgressInd.Value += p;
                }
                catch { }
                if (!string.IsNullOrEmpty(msg)) lblCurrent.Text = msg;
                Application.DoEvents();
            };
            Utils.NewApp(tiers, dataServer, "", "", progress, item, iPDT, iDEV, iPTY, nPDT, nDEV, nPTY,false);
		}

		private string ValidateControls(bool bBackup)
		{
            txtOldNS.Text = txtOldNS.Text.ToUpper();
            txtNewNS.Text = txtNewNS.Text.ToUpper();
            if (txtNewNS.Text == string.Empty) { return "Please enter the target namespace and try again."; }
			else if (txtClientTier.Text == string.Empty) { return "Please enter the client tier directory and try again."; }
			else if (cbNew.Checked && Directory.Exists(txtClientTier.Text) && !cbOverWrite.Checked) { return "Please delete client tier directory and try again to perform new installation."; }
            //else if (!cbNew.Checked && txtOldNS.Text == "RO" && txtNewNS.Text == "RO" && item.GetInsType() != "PTY" && !bBackup) { return "Please do not attempt to upgrade Rintagi itself."; }
			else if (!cbNew.Checked && !Directory.Exists(txtClientTier.Text)) { return "Please enter a valid client tier directory and try again."; }
            else if (!cbNew.Checked && Directory.Exists(txtRuleTier.Text) && item.GetInsType().Contains("PDT") && !bBackup) { return string.Format("Rule tier {0} found though you are installing a production package.", txtRuleTier.Text); }
            else if (gbRule.Visible && txtRuleTier.Text == string.Empty) { return "Please enter the rule tier directory and try again."; }
			else if (gbRule.Visible && !cbNew.Checked && !Directory.Exists(txtRuleTier.Text)) { return "Please enter a valid rule tier directory and try again."; }
            else if (gbRptWs.Visible && txtWsUrl.Text == string.Empty) { return "Please enter the web service server name and try again."; }
            else if (gbData.Visible && txtServerName.Text == string.Empty) { return "Please enter the data server name and try again."; }
			else if (gbData.Visible && txtUserName.Text == string.Empty && !cbIntegratedSecurity.Checked) { return "Please enter the sys usr login name and try again."; }
			else if (gbData.Visible && txtPassword.Text == string.Empty && !cbIntegratedSecurity.Checked) { return "Please enter the sys user login password and try again."; }
            else if (gbData.Visible && txtAppUserName.Text == string.Empty  && cbIntegratedSecurity.Checked && cbNew.Checked) { return "Please enter the app user login name and try again."; }
            else if (gbData.Visible && txtAppPassword.Text == string.Empty && cbIntegratedSecurity.Checked && cbNew.Checked) { return "Please enter the app user login password and try again."; }
            else if (!string.IsNullOrEmpty(txtAppUserName.Text) && cbNew.Checked && txtAppUserName.Text.Contains(" ")) {return "App User Name only accept letters, symbols and numbers";}
            else return string.Empty;
		}

		private void ToggleControls()
		{
			btnBackup.Enabled = !btnBackup.Enabled;
			btnCancel.Enabled = !btnCancel.Enabled;
			btnInstall.Enabled = !btnInstall.Enabled;
			gbUpgrade.Enabled = !gbUpgrade.Enabled;
			gbNmspace.Enabled = !gbNmspace.Enabled;
			gbClient.Enabled = !gbClient.Enabled;
			gbRule.Enabled = !gbRule.Enabled;
			gbWs.Enabled = !gbWs.Enabled;
			gbXls.Enabled = !gbXls.Enabled;
			//gbRptWs.Enabled = !gbRptWs.Enabled;
            gbData.Enabled = !gbData.Enabled;
		}

		private void ShowBackup(bool bShow)
		{
			lblCurrent.Text = string.Empty;
			btnBackup.Visible = bShow;
			lblBkPath1.Visible = bShow;
			lblBkPath2.Visible = bShow;
			txtBkPath1.Visible = bShow;
			txtBkPath2.Visible = bShow;
		}

		private void btnCBrowse_Click(object sender, EventArgs e)
		{
			DialogResult result = FolderDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				txtClientTier.Text = FolderDialog.SelectedPath;
			}
		}

		private void btnRBrowse_Click(object sender, EventArgs e)
		{
			DialogResult result = FolderDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				txtRuleTier.Text = FolderDialog.SelectedPath;
			}
		}

		private void rbPdtUpgrade_CheckedChanged(object sender, EventArgs e)
		{
			txtNewNS.Text = txtOldNS.Text; 
            //txtNewNS.Enabled = false;
			cbNew.Checked = false; cbNew.Enabled = true;
			gbRule.Visible = false; gbData.Visible = true;
			if (txtOldNS.Text == "RO") 
			{
				ShowBackup(true);
			}
			else 
			{
				ShowBackup(true);
			}
		}

		private void rbDevUpgrade_CheckedChanged(object sender, EventArgs e)
		{
			txtNewNS.Text = ""; txtNewNS.Enabled = true;
			cbNew.Checked = false; cbNew.Enabled = true;
			gbRule.Visible = true; gbData.Visible = true; ShowBackup(true);
		}

		private void rbPtyUpgrade_CheckedChanged(object sender, EventArgs e)
		{
			if (txtOldNS.Text == "RO" && !item.GetInsType().Contains("PTY"))
			{
				txtNewNS.Text = ""; txtNewNS.Enabled = true;
				cbNew.Checked = false; cbNew.Enabled = false;
				gbRule.Visible = false; gbData.Visible = true; ShowBackup(true);
			}
			else
			{
                if (txtOldNS.Text == "RO")
                {
                    txtNewNS.Text = "RO"; txtNewNS.Enabled = false;
                }
                else
                {
                    txtNewNS.Text = txtOldNS.Text; txtNewNS.Enabled = true;
                }
				cbNew.Checked = false; cbNew.Enabled = true;
				gbRule.Visible = true; gbData.Visible = true; ShowBackup(true);
			}
		}

		private void txtNewNS_TextChanged(object sender, EventArgs e)
		{
			if (txtNewNS.Text != string.Empty) { InitializeTiers(txtNewNS.Text, txtWebServer.Text); }
		}

		private void txtWebServer_TextChanged(object sender, EventArgs e)
		{
			if (txtWebServer.Text != string.Empty) { InitializeTiers(txtNewNS.Text, txtWebServer.Text); }
		}

		private void picLog_Click(object sender, EventArgs e)
		{
			Utils.ExecuteCommand("notepad.exe", Application.StartupPath + "\\Install.log", false);
		}

		private void picReadMe_Click(object sender, EventArgs e)
		{
            Utils.ExtractTxtRsc(string.Empty, "ReleaseNote.txt", string.Empty, string.Empty, new List<string>(), new List<string>(), false, "100",false);
			Utils.ExecuteCommand("notepad.exe", Application.StartupPath + "\\ReleaseNote.txt", false);
			Utils.DeleteFile("ReleaseNote.txt");
		}

		private void picHelp_Click(object sender, EventArgs e)
		{
			Utils.ExtractTxtRsc(string.Empty, "Help.txt", string.Empty, string.Empty, new List<string>(),new List<string>(),false, "100",false);
			Utils.ExecuteCommand("notepad.exe", Application.StartupPath + "\\Help.txt", false);
			Utils.DeleteFile("Help.txt");
		}

        private void SingleDeveloper_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSingleDeveloper.Checked)
            {
                AppUsrLabel.Visible = cbIntegratedSecurity.Checked;
                AppPwdLabel.Visible = cbIntegratedSecurity.Checked;
                txtAppUserName.Visible = cbIntegratedSecurity.Checked;
                txtAppPassword.Visible = cbIntegratedSecurity.Checked;
            }
            else
            {
                AppUsrLabel.Visible = true;
                AppPwdLabel.Visible = true;
                txtAppUserName.Visible = true;
                txtAppPassword.Visible = true;
            }

        }

        private void cbNew_CheckedChanged(object sender, EventArgs e)
        {
            bool showAppUsr = (!cbSingleDeveloper.Checked || rbPdtUpgrade.Checked || cbIntegratedSecurity.Checked) && cbNew.Checked;
            if (cbNew.Checked)
            {
                //cbSingleDeveloper.Visible = item.GetInsType().Contains("DEV");
                cbSingleDeveloper.Visible = true;
                //rbCFrwork4.Visible = true;
                //rbCFrwork3.Visible = true;

                AppUsrLabel.Visible = showAppUsr;
                AppPwdLabel.Visible = showAppUsr;
                txtAppUserName.Visible = showAppUsr;
                txtAppPassword.Visible = showAppUsr;
                btnBackup.Visible = false;
                txtBkPath1.Visible = false;
                txtBkPath2.Visible = false;
                lblBkPath1.Visible = false;
                lblBkPath2.Visible = false;
                //txtDbPath.Visible = true;
                //lblDbPath.Visible = true;
                cbInstallDB.Visible = true;
                cb32Bit.Visible = true;
                cbOverWrite.Visible = true;
                if (txtNewNS.Text != string.Empty) { InitializeTiers(txtNewNS.Text, txtWebServer.Text); }
            }
            else {
                cbSingleDeveloper.Visible = false;
                rbCFrwork4.Visible = false;
                rbCFrwork3.Visible = false;
                
                AppUsrLabel.Visible = showAppUsr;
                AppPwdLabel.Visible = showAppUsr;
                txtAppUserName.Visible = showAppUsr;
                txtAppPassword.Visible = showAppUsr;
                btnBackup.Visible = true;
                txtBkPath1.Visible = true;
                txtBkPath2.Visible = true;
                lblBkPath1.Visible = true;
                lblBkPath2.Visible = true;
                //txtDbPath.Visible = false;
                //lblDbPath.Visible = false;
                cbInstallDB.Visible = false;
                cb32Bit.Visible = false;
                cbOverWrite.Visible = false;

            }
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            Dictionary<Control,string> toolTipMsg = new Dictionary<Control,string>{
                {txtServerName,"SQL Server(and instance) name in the form of 'serverName\\instanceName', skip instanceName for default instance"},
                {txtUserName,"database login for this app, must have create database right for new installation"},
                {txtAppUserName,"This is the additional database login exclusively for this app; it may contain letters, symbols, and numbers; it comes with CREATE DATABASE capability"},
                {txtAppPassword,"This is the password for the above login"},
                {cbSingleDeveloper,"If unchecked, an additional credential is expected to be added to the database for the exclusive access of this application"},
                {cbIntegratedSecurity,"check this if you want to use current windows login to access the database server, App Usr/Password must then be provided for new installation for the application credential to the database server(which would be created if not exists)"},
                {cbNew,"check this to create a new installation, uncheck to upgrade existing installation"},
                {cbInstallDB,"check this to install new database/apply database changes in addition to the Web tier, uncheck to only install the Web tier(for standby web tier installation or moving web tier) "},
                {cb32Bit,"check this if you have 32 bit office installed and cannot install 64 bit Access Engine Runtime"},
                {cbOverWrite,"check this ONLY IF you are running the first time bootstrap installation of Rintagi from a git download"},
                {txtDbPath,"location where the database would be stored, this refers to the SQL Server machine, not necessary where this installer currently run in case of a two tier setup. Leaving this empty for a new installation means a client only installation for a two tier setup(i.e. it will not create any database)"},
            };
            try {
                toolTip.SetToolTip((Control)sender, toolTipMsg[(Control)sender]);
            } catch {}
        }

        private void cbIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIntegratedSecurity.Checked)
            {
                if (cbNew.Checked)
                {
                    AppUsrLabel.Visible = true;
                    AppPwdLabel.Visible = true; 
                    txtAppUserName.Visible = true;
                    txtAppPassword.Visible = true;
                    cbSingleDeveloper.Visible = false;
                }
                txtUserName.Visible = false;
                txtPassword.Visible = false;
                SysPwdLabel.Visible = false;
                SysUsrLabel.Visible = false;
            }
            else
            {
                txtUserName.Visible = true;
                txtPassword.Visible = true;
                SysPwdLabel.Visible = true;
                SysUsrLabel.Visible = true; 
                if (cbNew.Checked)
                {
                    AppUsrLabel.Visible = !cbSingleDeveloper.Checked || rbPdtUpgrade.Checked;
                    AppPwdLabel.Visible = !cbSingleDeveloper.Checked || rbPdtUpgrade.Checked;
                    txtAppUserName.Visible = !cbSingleDeveloper.Checked || rbPdtUpgrade.Checked;
                    txtAppPassword.Visible = !cbSingleDeveloper.Checked || rbPdtUpgrade.Checked;
                    cbSingleDeveloper.Visible = true;
                }
            }
        }
	}
}
