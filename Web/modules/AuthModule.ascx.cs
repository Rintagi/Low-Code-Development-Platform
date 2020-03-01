using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;

namespace RO.Web
{
	public partial class AuthModule : RO.Web.ModuleBase
	{
  //      private string LcSysConnString;
		//private string LcAppPw;
        
        public AuthModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                string xx = Session["UsrSwitch:" + Request.Url.PathAndQuery] as string;
                if (xx == "Y")
                {
                    Session.Remove("UsrSwitch:" + Request.Url.PathAndQuery);
                    PreMsgPopup("Login profile changed, please be notified that page has been reloaded.");
                }

                if (LUser != null) cUsrId.Text = LUser.UsrId.ToString();
            }
            else
            {
                if (LUser != null && LUser.UsrId.ToString() != cUsrId.Text && (LUser.UsrName ?? "").ToLower() != "anonymous")
                {
                    Session["UsrSwitch:" + Request.Url.PathAndQuery] = "Y";
                    this.Redirect(Request.Url.PathAndQuery);
                }
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
            CheckAuthentication(true);
        }
		#endregion

		private void CheckAuthentication(bool pageLoad)
		{
            base.EnforceSSL();
            base.SetupSSD();
        }

        private void PreMsgPopup(string msg)
        {
            int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
            string iconUrl = "images/warning.gif";
            string focusOnCloseId = string.Empty;
            string msgContent = ReformatErrMsg(msg);
            string script =
            @"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(cUsrId, typeof(TextBox), "UserSwitch", script, false);
        }
	}
}