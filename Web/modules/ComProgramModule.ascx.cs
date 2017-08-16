namespace RO.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using RO.Facade3;
	using RO.Common3;
	using RO.Common3.Data;

	public partial class ComProgramModule : RO.Web.ModuleBase
	{
		private const string KEY_dtEntity = "Cache:dtEntity";

		private StringBuilder sb = new StringBuilder();

		public ComProgramModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				Session.Remove(KEY_dtEntity);
				GetEntity();
                cHelpLabel.Text = "Please select the appropriate entity, then click 'Compile' button to compile all respective programs inside the project. Please be aware each entity can only be compiled by one process at any time.";
				cTitleLabel.Text = "Compile Programs";
				cEntityId.Focus();
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

		}
		#endregion

        private void CheckAuthentication(bool pageLoad)
        {
            CheckAuthentication(pageLoad, true);
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

		protected void cGenButton_Click(object sender, System.EventArgs e)
		{
            {
                if (CPrj.SrcClientFrwork == "1")
                {
                    PreMsgPopup("Only DotNet 2.0 or later can be compiled this way. Please updgrade project and try again."); return;
                }
                DataTable dt = (new LoginSystem()).GetSystemsList(base.CPrj.SrcDesConnectionString, base.CPrj.SrcDesPassword);
                string ss = "", er = "";
                foreach (DataRow dr in dt.Rows)
                {
                    CurrSrc src = new CurrSrc(true, dr);
                    try
                    {
                        ss = ss + Robot.CompilePrj("Service" + src.SrcSystemId.ToString() + "\\Service" + src.SrcSystemId.ToString() + ".csproj");
                    }
                    catch (Exception ee)
                    {
                        if (!ee.Message.Contains("error MSB1009")) er = er + ee.Message;
                    }
                }

                if (er == "")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CurrSrc src = new CurrSrc(true, dr);
                        try
                        {
                            Robot.RefreshClientTier(CPrj, src);
                        }
                        catch (Exception ee) { if (!ee.Message.Contains("Could not find")) er = er + ee.Message; }
                    }
                }

                try
                {
                    ss = ss + Robot.CompileAuxProj(CPrj);
                }
                catch (Exception ee)
                {
                    if (!ee.Message.Contains("error MSB1009")) er = er + ee.Message;
                }
                
                cCompileMsg.Text = (ss + er);
                if (er != "")
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
			DataTable dt = (DataTable)Session[KEY_dtEntity];
			if (dt != null)
			{
				base.CPrj = new CurrPrj(dt.Rows[cEntityId.SelectedIndex]);
			}
			cEntityId.Focus();
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