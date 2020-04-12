using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;

namespace RO.Web
{
	public partial class EncryptPwdModule : RO.Web.ModuleBase
	{
		private string LcSysConnString;
		private string LcAppPw;
        
        public EncryptPwdModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                if (base.LUser != null)
                {
                    new AdminSystem().LogUsage(base.LUser.UsrId, string.Empty, "Encrypt Password", 0, 0, 0, "EncryptPwd.aspx", LcSysConnString, LcAppPw);
                    if (base.LImpr.RowAuthoritys.Split(new char[]{(char) 191},StringSplitOptions.RemoveEmptyEntries).Contains("8")
                        && 
                        System.Configuration.ConfigurationManager.AppSettings["ShowHttpHeaders"] == "Y")
                    {
                        foreach (var key in Request.Headers.AllKeys) {
                            // don't show cookie or auth header
                            if (!key.Contains("Cookie")
                                &&
                                !key.Contains("Authorization")
                                )
                            {
                                cMsg.Text = cMsg.Text + Environment.NewLine + key + " = " + Request.Headers[key];
                            }
                        }
                        cMsg.Text = cMsg.Text + Environment.NewLine + string.Format("IsProxy: {0}",IsProxy());
                        cMsg.Text = cMsg.Text + Environment.NewLine + ResolveUrl("~/Dnload.aspx");
                        cMsg.Text = cMsg.Text + Environment.NewLine + ResolveUrlCustom("~/Dnload.aspx", false, true);
                        cMsg.Text = cMsg.Text + Environment.NewLine + ResolveUrlCustom("~/Dnload.aspx", true, true);
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.ExtBaseUrl;
                        cMsg.Text = cMsg.Text + Environment.NewLine + GetDomainUrl(false);
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.ExtDomain;
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.ExtBasePath;
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.IntBaseUrl;
                        cMsg.Text = cMsg.Text + Environment.NewLine + GetDomainUrl(true);
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.IntDomain;
                        cMsg.Text = cMsg.Text + Environment.NewLine + Config.IntBasePath;
                        cMsg.Visible = true;
                    }
                }
				CheckAuthentication(true);
			}
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
			if (LcSysConnString == null)
			{
				SetSystem(3);
			}

		}
		#endregion

		private void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
		}

        private void CheckAuthentication(bool pageLoad)
        {
            bool allowBroadAccess = (System.Configuration.ConfigurationManager.AppSettings["DesEncryptPwdAccess"] ?? "N") == "Y";
            if (allowBroadAccess) return;
            CheckAuthentication(pageLoad, !Request.IsLocal);
        }

        protected void cEncryptButton_Click(object sender, System.EventArgs e)
        {
            CheckAuthentication(false);
            if (cValidate.Checked)
            {
                OleDbDataAdapter da = new OleDbDataAdapter();
                var dbConnectionString = Config.GetConnStr(Config.DesProvider, Config.DesServer, Config.DesDatabase, "", Config.DesUserId);

                OleDbCommand cmd = new OleDbCommand("GetSystemsList", new OleDbConnection(dbConnectionString + cInstr.Text));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
            }

            cOutstr.Text = (new AdminSystem()).EncryptString(cInstr.Text);
        }
    }
}