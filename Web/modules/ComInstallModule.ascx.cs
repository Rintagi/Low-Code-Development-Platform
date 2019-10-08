namespace RO.Web
{
	using System;
	using System.Data;
    using System.Linq;
    using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using System.Xml;
	using System.Collections;
	using RO.Facade3;
	using RO.Common3;
	using RO.Common3.Data;
    using System.Web.Configuration;
    using System.Configuration;

    public partial class ComInstallModule : RO.Web.ModuleBase
	{
		private string LcSysConnString;
		private string LcAppPw;
		private const string KEY_dtScrCri = "Cache:dtScrCri98";
		private const string KEY_dtEntity = "Cache:dtEntity";

		private StringBuilder sb = new StringBuilder();

		public ComInstallModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				Session.Remove(KEY_dtEntity);
				GetEntity();
				cHelpLabel.Text = "Please select the appropriate project, prepare them then click 'Compile' button to compile deployment package into the appropriate installer '.exe' file.  Then click 'Download' button to retrieve the compiled '.exe' file. Please be aware each project can only be compiled by one process at any time. Make sure Network Service has full control rights to the deployment directory.";
				cTitleLabel.Text = "Compile Installer";
				cEntityId.Focus();
                Tuple<string, string, bool> license = RO.Common3.Utils.CheckValidLicense("Design","Deploy");
                if (!license.Item3)
                {
                    cPrepare.Enabled = false;
                    cOkButton.Enabled = false;
                    cLoadButton.Enabled = false;
                    cRegisterLink.NavigateUrl = (System.Configuration.ConfigurationManager.AppSettings["LicenseServer"] ?? "https://www.rintagi.com") + "/AcquireLicense.aspx?InstallID=" + license.Item1 + "&AppID=" + license.Item2 + "&ModuleName=" + "Design" + "&FromUrl=" + HttpUtility.UrlEncode(Request.Url.ToString());
                    cInstallID.Text = license.Item1;
                    cAppID.Text = license.Item2;
                    cRegisterLink.Visible = true;
                    cInstallID.Visible = true;
                    cInstallIDLabel.Visible = true;
                    cAppID.Visible = true;
                    cAppIDLabel.Visible = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(Config.RintagiLicense))
                    {
                        Tuple<string, bool,string> _license = (new RO.Access3.AdminAccess()).UpdateLicense(null, "");
                        if (_license.Item2)
                        {
                            Config.RintagiLicense = _license.Item3;
                            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                            if (config.AppSettings.Settings["RintagiLicense"] != null) config.AppSettings.Settings["RintagiLicense"].Value = _license.Item3;
                            else config.AppSettings.Settings.Add("RintagiLicense", _license.Item3);
                            config.Save(ConfigurationSaveMode.Modified);
                        }
                    }
                }
            }
			else
			{
				string[] cc = Request.Form.GetValues("__EVENTTARGET");
				if (base.CPrj != null && base.CPrj.EntityId.ToString() != cEntityId.SelectedValue && !(cc != null && cc[0].EndsWith("cEntityId")))
				{
					throw new Exception("Entity information has been changed, please re-select from menu and try again!");
				}
			}
			cMsgLabel.Text = string.Empty;
			CreateReleaseList();
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

		private void CreateReleaseList()
		{
			int filterId = 0;
			string key = string.Empty;
            DataTable dt = (new AdminSystem()).GetLis(98, "GetLisAdmRelease98", false, "N", 0, null, null, filterId, key, string.Empty, GetScrCriteria(), base.LImpr, base.LCurr, UpdCriteria(false));

			Table t = new Table(); t.Style.Add("padding", "5px");
			TableRow tr = null;
			TableCell tc1 = null, tc2 = null;
			Label l = null;
			int ii = 0;
			StringBuilder sbIds = new StringBuilder();
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("<script type=\"text/javascript\" lang=\"javascript\">");
			sb.Append("var urls = [ "); sbIds.Append("var ids = [");

			bool IsProductionUpgrade = false;

			foreach (DataRow dr in dt.Rows)
			{
				//output to javasript array..
				DataTable dtDetails = (new AdminSystem()).GetMstById("GetAdmRelease98ById",dr["ReleaseId191"].ToString(),null, null);
				if (dtDetails.Rows.Count > 0)
				{
					DataRow drDetail = dtDetails.Rows[0];
					if (ii != 0) { sb.Append(","); sbIds.Append(","); }
                    sb.Append("'AdmPuMkDeploy.aspx?csy=3&typ=N&key=" + drDetail["ReleaseId191"].ToString() + "'");
					sbIds.Append(dr["ReleaseId191"].ToString());

					if (drDetail["ReleaseTypeId191"].ToString() == "3") 
					{
                        // PDT i.e. production upgrade force generation to avoid outdated package created for upgrade
						IsProductionUpgrade = true;
					}
					else
					{
						IsProductionUpgrade = false;
					}
				}

				tr = new TableRow();
                tc1 = new TableCell(); tc1.CssClass = "inp-chk";
                l = new Label(); l.CssClass = "inp-lbl";
				l.Text = dr["ReleaseId191Text"].ToString();
				if (IsProductionUpgrade)
				{
					tc1.Controls.Add(new LiteralControl("<input id=\"chkRelease" + dr["ReleaseId191"].ToString() + "\" checked=\"true\" disabled=\"true\" type=\"checkbox\" />"));
				}
				else
				{
                    tc1.Controls.Add(new LiteralControl("<input id=\"chkRelease" + dr["ReleaseId191"].ToString() + "\" checked=\"true\" type=\"checkbox\" />"));
				}
				tc1.Controls.Add(l);
				tr.Controls.Add(tc1);

				tc2 = new TableCell();
				tc2.Controls.Add(new LiteralControl("<img id=\"InProgress" + dr["ReleaseId191"].ToString() + "\" src=\"images/indicator.gif\" style=\"border-width:0px;display:none;\" />"));
				tc2.Controls.Add(new LiteralControl("<img id=\"Complete" + dr["ReleaseId191"].ToString() + "\" src=\"images/checkmark.gif\" style=\"border-width:0px;display:none;\" />"));
				tc2.Controls.Add(new LiteralControl("<img id=\"Cancelled" + dr["ReleaseId191"].ToString() + "\" src=\"images/cancelled.gif\" style=\"border-width:0px;display:none;\" />"));
				tr.Controls.Add(tc2);

				t.Controls.Add(tr);

				ii++;
			}
            System.Collections.Generic.List<string> wipReleaseContent = (new AdminSystem()).HasOutstandReleaseContent(Config.AppNameSpace, LcSysConnString, LcAppPw);
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> wipRengenList = (new AdminSystem()).HasOutstandRegen(Config.AppNameSpace, LcSysConnString, LcAppPw);
            if (wipReleaseContent.Count > 0)
            {
                tr = new TableRow();
                tc1 = new TableCell();
                tc1.VerticalAlign = VerticalAlign.Top;
                tc1.Controls.Add(new LiteralControl("<img id=\"HasWIP" + "" + "\" src=\"images/warning.gif\" style=\"border-width:0px;\" />"));
                tr.Controls.Add(tc1);
                tc1.Controls.Add(new LiteralControl("<span>The following system(s) has work in progress release contents, it may not run correctly on the target server when the installer is run. Please assign a release date to the latest version number if you can.</span>"));
                tc1.Controls.Add(new LiteralControl("<br/><span>" + string.Join(", ", wipReleaseContent.ToArray()) + "</span>"));
                t.Controls.Add(tr);
            }
            else if (wipRengenList.Count > 0)
            {
                tr = new TableRow();
                tc1 = new TableCell();
                tc1.VerticalAlign = VerticalAlign.Top;
                tc1.Controls.Add(new LiteralControl("<img id=\"HasWIP" + "" + "\" src=\"images/warning.gif\" style=\"border-width:0px;\" />"));
                tr.Controls.Add(tc1);
                tc1.Controls.Add(new LiteralControl("<span>The following screen/report/wizard are waiting for regeneration, it may not run correctly on the target server when the installer is run. Please regenerate all changed screen/report/wizard.</span>"));
                System.Collections.Generic.List<string> r = new System.Collections.Generic.List<string>();
                foreach (var x in wipRengenList)
                {
                    r.Add("<div>"
                          + "<div>" + x.Key + "</div>"
                          + "<div>" + string.Join("<br/>", x.Value.ToArray()) + "</div>"
                          + "</div>");
                }
                tc1.Controls.Add(new LiteralControl("<div>" + string.Join("", r.ToArray()) + "</div>"));
                t.Controls.Add(tr);
            }

			sb.AppendLine("];"); sbIds.AppendLine("];");
			sb.Append(sbIds);
			sb.AppendLine("</script>");
			cPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
			cFormPanel.Controls.Add(t);
		}

		private DataSet UpdCriteria(bool bUpdate)
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn")));
			DataRow dr = ds.Tables["DtScreenIn"].NewRow();
			DataView dvCri = GetScrCriteria();
			ds.Tables["DtScreenIn"].Rows.Add(dr);

			foreach (DataRowView drv in dvCri)
			{
				if (drv["ColumnName"].ToString().StartsWith("EntityId"))
				{
					dr[drv["ColumnName"].ToString()] = cEntityId.SelectedValue;
				}
			}
			return ds;
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

		private DataView GetScrCriteria()
		{
			DataTable dtScrCri = (DataTable)Session[KEY_dtScrCri];
			if (dtScrCri == null)
			{
				try
				{
					dtScrCri = (new AdminSystem()).GetScrCriteria("98", LcSysConnString, LcAppPw);
				}
				catch (Exception err) { PreMsgPopup(err.Message); }
				Session[KEY_dtScrCri] = dtScrCri;
			}
			return dtScrCri.DefaultView;
		}

		protected void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
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

		protected void cLoadButton_Click(object sender, System.EventArgs e)
		{

			if (!File.Exists(CPrj.DeployPath + "bin\\Release\\Install.exe"))
			{
				PreMsgPopup("Please compile before download and try again."); return;
			}
			else
			{
                string releaseDtStr = "";
                string releaseType = "";
                DataTable dt = (new AdminSystem()).GetLis(98, "GetLisAdmRelease98", false, "N", 0, null, null, 0, null, string.Empty, GetScrCriteria(), base.LImpr, base.LCurr, UpdCriteria(false));
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dtDetails = (new AdminSystem()).GetMstById("GetAdmRelease98ById", dr["ReleaseId191"].ToString(), null, null);
                    if (dtDetails.Rows[0]["EntityId191"].ToString() == CPrj.EntityId.ToString() && ((byte) dtDetails.Rows[0]["ReleaseTypeId191"]) < 4)
                    {
                        DateTime releaseDt = (DateTime) dtDetails.Rows[0]["ReleaseDate191"];
                        releaseDtStr = releaseDt.ToString("yyyyMMdd") + "_";
                        byte releaseTypeId = ((byte)dtDetails.Rows[0]["ReleaseTypeId191"]);
                        if (releaseTypeId == 1) releaseType = "DEV_";
                        else if (releaseTypeId == 2) releaseType = "PTY_";
                        else if (releaseTypeId == 3) releaseType = "PDT_";
                        break;
                    }
                }
                FileInfo fi = new FileInfo(CPrj.DeployPath + "bin\\Release\\Install.exe");
                if (fi.LastWriteTime < DateTime.Now.AddHours(-4))
                {
                    cOkButton_Click(sender, e); // force compilation, some user don't know it needs to be compiled first
                    if (cMsgLabel.Text.Contains("FAIL")) return;
                }
                Response.Buffer = true;
                Response.ClearHeaders();
                Response.ClearContent();

                // DO NOT use octet-stream or things like that as it will hang IE
                Response.ContentType = "application/exe";

                // we are downloading generated program, make sure it is not cached by proxy or browser by immediately
                // expires it
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-360000));
                Response.Cache.SetValidUntilExpires(true);


                Response.AppendHeader("Content-Length", fi.Length.ToString());
                Response.AppendHeader("Content-Disposition", "Attachment; Filename=\"" + releaseDtStr + Config.AppNameSpace + releaseType + "Install.exe\"");
                Response.TransmitFile(CPrj.DeployPath + "bin\\Release\\Install.exe");
                Response.End();
            }
		}

		protected void cOkButton_Click(object sender, System.EventArgs e)
		{
			// Unlike web based project, this window based project depends only on the availability of MSBuild:
			if (Config.CFrameworkCd == "1")
			{
				PreMsgPopup("Only DotNet 2.0 or later can be compiled this way. Please updgrade project and try again."); return;
			}
			else
			{
				//System user "NetWork Service" must have write permission to directory hosting the sln file:
				EmbedRsc(CPrj.DeployPath);
				string cmd_path = "\"" + Config.BuildExe + "\"";
				string cmd_arg = "\"" + CPrj.DeployPath + "Install.sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                cCompileMsg.Text = Utils.WinProc(cmd_path, cmd_arg, true);
                if (cCompileMsg.Text.IndexOf("failed") >= 0 || cCompileMsg.Text.Replace("errorreport", string.Empty).Replace("warnaserror", string.Empty).IndexOf("error") >= 0)
                {
                    cMsgLabel.Text = "BUILD FAILED!";
                }
                else
                {
                    cMsgLabel.Text = "BUILD COMPLETED!";
                }
            }
		}

		protected void cEntityId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cMsgLabel.Text = string.Empty;
			cCompileMsg.Text = string.Empty;
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			if (dt != null)
			{
				base.CPrj = new CurrPrj(dt.Rows[cEntityId.SelectedIndex]);
			}
			cEntityId.Focus();
		}

		// Automatically embed resources and exclude obsolete items from installer.
		public void EmbedRsc(string DeployPath)
		{
			ArrayList ToDelete;
			XmlNode xn;
			XmlNodeList xl;
			string InstallProj = "Install.csproj";
			XmlDocument xd = new XmlDocument();
			xd.Load(DeployPath + InstallProj);

			// Step 1: Remove all existing EmbeddedResource and None:
			ToDelete = new ArrayList();
			xl = xd.GetElementsByTagName("EmbeddedResource");
			foreach (XmlNode node in xl)
			{
				if (node.Attributes != null)
				{
					string attr = node.Attributes[0].Value;
					if (attr.Contains(".zip") || attr.Contains(".bat") || attr.Contains(".sql")) { ToDelete.Add(node); }
				}
			}
			xl = xd.GetElementsByTagName("None");
			foreach (XmlNode node in xl)
			{
				if (node.Attributes != null)
				{
					string attr = node.Attributes[0].Value;
					if (attr.Contains(".zip") || attr.Contains(".bat") || attr.Contains(".sql")) { ToDelete.Add(node); }
				}
			}
			for (int ii = 0; ii < ToDelete.Count; ii++)
			{
				xn = (XmlNode)ToDelete[ii];
				xn.ParentNode.RemoveChild(xn);
			}

			// Step 2: Remove all empty ItemGroup:
			ToDelete = new ArrayList();
			xl = xd.GetElementsByTagName("ItemGroup");
			foreach (XmlNode node in xl)
			{
				if (!node.HasChildNodes) { ToDelete.Add(node); }
			}
			for (int ii = 0; ii < ToDelete.Count; ii++)
			{
				xn = (XmlNode)ToDelete[ii];
				xn.ParentNode.RemoveChild(xn);
			}

			//Step 3: Embedded ncessary resources:
			xn = xd.DocumentElement;
			XmlNode NewItemNode = xd.CreateNode(XmlNodeType.Element, "ItemGroup", string.Empty);
			DirectoryInfo di = new DirectoryInfo(DeployPath);
			foreach (DirectoryInfo dir in di.GetDirectories())
			{
				if (dir.Name != "bin" && dir.Name != "obj")
				{
					SearchDirX("*.bat", dir, NewItemNode, xd, DeployPath);
					SearchDirX("*.sql", dir, NewItemNode, xd, DeployPath);
					SearchDirX("*.zip", dir, NewItemNode, xd, DeployPath);
					// Two directories deep for now:
					foreach (DirectoryInfo dir1 in dir.GetDirectories())
					{
						SearchDirX("*.bat", dir1, NewItemNode, xd, DeployPath);
						SearchDirX("*.sql", dir1, NewItemNode, xd, DeployPath);
						SearchDirX("*.zip", dir1, NewItemNode, xd, DeployPath);
						foreach (DirectoryInfo dir2 in dir1.GetDirectories())
						{
							SearchDirX("*.bat", dir2, NewItemNode, xd, DeployPath);
							SearchDirX("*.sql", dir2, NewItemNode, xd, DeployPath);
							SearchDirX("*.zip", dir2, NewItemNode, xd, DeployPath);
						}
					}
				}
			}
			xn.AppendChild(NewItemNode);
			xd.Save(DeployPath + InstallProj);

			//for some reason .net leaves  xmlns="" which VS.NET doesnt like so we need to remove it
			StreamReader sr = new StreamReader(DeployPath + InstallProj);
			StringBuilder csproj = new StringBuilder();
			string line;
			while ((line = sr.ReadLine()) != null) { csproj.AppendLine(line); }
			sr.Close();
			StreamWriter sw = new StreamWriter(DeployPath + InstallProj, false);
			sw.Write(csproj.ToString().Replace(" xmlns=\"\"", ""));
			sw.Close();
		}

		// Called by WrEmbedRsc: Finds files based on the pattern and creates a node for them.
		private void SearchDirX(string Pattern, DirectoryInfo SearchDir, XmlNode ItemNode, XmlDocument xd, string DeployPath)
		{
			XmlNode NewNode;
			XmlAttribute xa;
			foreach (FileInfo fi in SearchDir.GetFiles(Pattern))
			{
				NewNode = xd.CreateNode(XmlNodeType.Element, "EmbeddedResource", null);
				xa = xd.CreateAttribute("Include");
				xa.Value = fi.FullName.Replace(DeployPath, "");
				NewNode.Attributes.Append(xa);
				ItemNode.AppendChild(NewNode);
			}
		}

        private void PreMsgPopup(string msg)
        {
            int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
            string iconUrl = "images/error.gif";
            string focusOnCloseId = string.Empty;
            string msgContent = ReformatErrMsg(msg);
            if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
            string script =
            @"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
            ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
        }
    }
}