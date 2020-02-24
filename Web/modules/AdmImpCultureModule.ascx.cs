using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Web
{
	public partial class AdmImpCultureModule : RO.Web.ModuleBase
	{
		private const string KEY_dtAdmImpCulture = "Cache:dtAdmImpCulture";
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppPw;

		public AdmImpCultureModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			CheckAuthentication(true);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
			ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "datepicker_i18n", "scripts/i18n/jquery.ui.datepicker-" + lang + ".js");
			cMsgLabel.Text = "";
			if (!IsPostBack)
			{
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw, null, null, 1);
				if (dtMenuAccess.Rows.Count == 0)
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				SetButtonHlp();
				Session.Remove(KEY_dtAdmImpCulture);
				cHelpLabel.Text = "Please backup the database, check \"All SpreadSheets\" or select the appropriate import SpreadSheet then press Import button to import. Use the buttons on the right to filter the directory or list all import files respectively. Only Local SpreadSheet will be processed if selected.";
				cTitleLabel.Text = "Culture Type Import";
				cLocation.Text = @Config.PathXlsImport;
				(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, "Culture Type Import", 0, 0, 1, string.Empty, LcSysConnString, LcAppPw);
				Session["ImportSchema"] = (new AdminSystem()).GetSchemaWizImp(1,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "window.open('ImportSchema.aspx?scm=W&key=1&csy=3','ImportSchemaW1','resizable=yes,scrollbars=yes,status=yes,width=700,height=500'); return false;";}
				cBrowse.Attributes.Add("OnChange", "__doPostBack('" + cBrowseButton.ClientID.Replace("_", "$") + "','')");
			}
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
			if (LcSysConnString == null) { SetSystem(3); }

		}
		#endregion

		private void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
			try
			{
				base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
				DataRow row = base.SystemsList.Rows.Find(SystemId);
				base.CSrc = new CurrSrc(true, row);
				base.CTar = new CurrTar(true, row);
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,0,0,1,LcSysConnString,LcAppPw))
				{
					(new GenWizardsSystem()).CreateProgram(1, "Culture Type Import", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					this.Redirect(Request.RawUrl);
				}
			}
			catch (Exception e) { PreMsgPopup(e.Message); }
		}

		private void CheckAuthentication(bool pageLoad)
		{
			CheckAuthentication(pageLoad, true);
		}

		private void SetButtonHlp()
		{
			DataTable dt;
			dt = (new AdminSystem()).GetButtonHlp(0,0,1,base.LUser.CultureId,LcSysConnString,LcAppPw);
			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["ButtonTypeName"].ToString() == "Search") { cSearchButton.CssClass = "ButtonImg SearchButtonImg"; cSearchButton.Text = string.Empty; cSearchButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSearchButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "List") { cListButton.CssClass = "ButtonImg ListButtonImg"; cListButton.Text = string.Empty; cListButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cListButton.ToolTip = dr["ButtonToolTip"].ToString(); }
					if (dr["ButtonTypeName"].ToString() == "Import") { cImportButton.CssClass = "small blue button"; cImportButton.Text = dr["ButtonName"].ToString(); cImportButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cImportButton.ToolTip = dr["ButtonToolTip"].ToString(); }
				}
			}
		}

		public void cSearchButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmImpCulture];
			if (dt != null)
			{
				DataView dv = dt.DefaultView;
				dv.RowFilter = "FileFullName like '*" + cSearch.Text + "*'";
				cFileList.DataSource = dv; cFileList.DataBind();
			}
		}

		public void cListButton_Click(object sender, System.EventArgs e)
		{
			PopFileList(cSearch.Text);
		}

		protected void cBrowseButton_Click(object sender, EventArgs e)
		{
			if (cBrowse.HasFile && cBrowse.PostedFile.FileName != string.Empty)
			{
				string fNameO = (new FileInfo(cBrowse.PostedFile.FileName)).Name;
				try
				{
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
				catch (Exception err) {throw new Exception("Unable to retrieve sheet names from \"" + fNameO + "\": " + err.Message);}
			}
		}

		public void cImportButton_Click(object sender, System.EventArgs e)
		{
			int iCnt;
			if (cFNameO.Text != string.Empty)
			{
				if (cWorkSheet.Items.Count > 0 && cWorkSheet.SelectedItem.Text != string.Empty && cStartRow.Text != string.Empty)
				{
					cAllFile.Checked = false; cFileList.ClearSelection();
					try
					{
						iCnt = ImportClient(cOverwrite.Checked,LUser.UsrId,cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
						cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + " rows from \"" + cFNameO.Text + "\" has been imported;<br />";
					}
					catch (Exception err) {PreMsgPopup("Error in spreadsheet \"" + cFNameO.Text + "\": " + err.Message);}
					finally {cWorkSheet.Items.Clear(); cFNameO.Text = string.Empty;}
				}
				else
				{
					PreMsgPopup("Please indicate a worksheet for the selected local spreadsheet, enter the starting row and try again.");
				}
			}
			else	// Multiple server spreadsheets import.
			{
				if (cWorkSheetM.Text != string.Empty && cStartRow.Text != string.Empty)
				{
					if (cAllFile.Checked)
					{
						DataTable dt = (DataTable)Session[KEY_dtAdmImpCulture];
						if (dt != null)
						{
							foreach (DataRowView drv in dt.DefaultView)
							{
								try
								{
									iCnt = ImportFile(cOverwrite.Checked,LUser.UsrId,drv["FileName"].ToString(),cWorkSheetM.Text,cStartRow.Text,drv["FileFullName"].ToString());
									cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + " rows from \"" + drv["FileName"].ToString() + "\" has been imported;<br />";
								}
								catch (Exception err) {PreMsgPopup("Error in spreadsheet \"" + drv["FileName"].ToString() + "\": " + err.Message); break;}
							}
						}
					}
					else
					{
						foreach (ListItem li in cFileList.Items)
						{
							if (li.Selected)
							{
								try
								{
									iCnt = ImportFile(cOverwrite.Checked,LUser.UsrId,li.Value,cWorkSheetM.Text,cStartRow.Text,li.Text);
									cMsgLabel.Text = cMsgLabel.Text + iCnt.ToString() + " rows from \"" + li.Value + "\" has been imported;<br />";
								}
								catch (Exception err) {PreMsgPopup("Error in spreadsheet \"" + li.Value + "\": " + err.Message); break;}
							}
						}
					}
				}
				else
				{
					PreMsgPopup("If import from server location is your choice, please enter a worksheet name or the sheet order for the selected spreadsheets, indicate the starting row and try again.");
				}
			}
		}

		private int ImportClient(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName)
		{
			try
			{
				DataSet ds = RO.Common3.XmlUtils.XmlToDataSet(((new XLSImport()).ImportFile(fileName, workSheet, startRow, fileFullName)));
				DataRowCollection rows = ds.Tables[0].Rows;
				DataColumnCollection cols = ds.Tables[0].Columns;
				int iStart = int.Parse(startRow) - 1;
				for ( int iRow = iStart; iRow < rows.Count; iRow++ )
				{
					if (rows[iRow][0].ToString() == string.Empty && rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty && rows[iRow][3].ToString() == string.Empty && rows[iRow][6].ToString() == string.Empty) {rows[iRow].Delete();}
					else
					{
						try {rows[iRow][0] = rows[iRow][0].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][1] = rows[iRow][1].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][2] = rows[iRow][2].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][3] = rows[iRow][3].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][4] = rows[iRow][4].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][5] = rows[iRow][5].ToString().Replace("\r","").Replace("\n","");} catch {};
						try {rows[iRow][6] = rows[iRow][6].ToString().Replace("\r","").Replace("\n","");} catch {};
					}
				}
				return (new AdminSystem()).ImportRows(1,"WizAdmImpCulture",bOverwrite,usrId,ds,iStart,fileName,LcAppConnString,LcAppPw,CPrj,CSrc);
			}
			catch(Exception e) { if (e != null) throw e; }
			return 0;
		}

		private void PopFileList(string searchTxt)
		{
			DataTable dt = GetFileList(cLocation.Text);
			if (dt != null)
			{
				cSearch.Text = string.Empty;
				DataView dv = dt.DefaultView;
				dv.Sort = "FileFullName";
				Session[KEY_dtAdmImpCulture] = dt;
				cFileList.DataSource = dv; cFileList.DataBind();
			}
		}

		private DataTable GetFileList(string fileFolder)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FileName", typeof(String));
			dt.Columns.Add("FileFullName", typeof(String));
			DirectoryInfo di = new DirectoryInfo(fileFolder);
			// Capture SpreadSheets in current folder
			FileInfo[] files = di.GetFiles("*.xls",SearchOption.AllDirectories);
			foreach (FileInfo fi in files)
			{
				dt.Rows.InsertAt(dt.NewRow(), 0);
				dt.Rows[0]["FileName"] = fi.Name;
				dt.Rows[0]["FileFullName"] = fi.FullName;
			}
			return dt;
		}

		private int ImportFile(bool bOverwrite, Int32 usrId, string fileName, string workSheet, string startRow, string fileFullName)
		{
			try
			{
				DataSet ds = RO.Common3.XmlUtils.XmlToDataSet(((new XLSImport()).ImportFile(fileName, workSheet, startRow, fileFullName)));
				DataRowCollection rows = ds.Tables[0].Rows;
				DataColumnCollection cols = ds.Tables[0].Columns;
				int iStart = int.Parse(startRow) - 1;
				for ( int iRow = iStart; iRow < rows.Count; iRow++ )
				{
					if (rows[iRow][0].ToString() == string.Empty && rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty && rows[iRow][3].ToString() == string.Empty && rows[iRow][6].ToString() == string.Empty) {rows[iRow].Delete();}
					else
					{
						rows[iRow][0] = rows[iRow][0].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][1] = rows[iRow][1].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][2] = rows[iRow][2].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][3] = rows[iRow][3].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][4] = rows[iRow][4].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][5] = rows[iRow][5].ToString().Replace("\r","").Replace("\n","");
						rows[iRow][6] = rows[iRow][6].ToString().Replace("\r","").Replace("\n","");
					}
				}
				return (new AdminSystem()).ImportRows(1,"WizAdmImpCulture",bOverwrite,usrId,ds,iStart,fileName,LcAppConnString,LcAppPw,CPrj,CSrc);
			}
			catch (Exception e) { PreMsgPopup(e.Message); return 0; }
		}

		protected void cFileList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cAllFile.Checked = false;
		}

		protected void cAllFile_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cAllFile.Checked) {cFileList.ClearSelection();}
		}

		private void PreMsgPopup(string msg)
		{
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/info.gif";
		    string focusOnCloseId = string.Empty;
		    string msgContent = ReformatErrMsg(msg);
		    if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
			string script =
			@"<script type='text/javascript' lang='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
		}
	}
}

