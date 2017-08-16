using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Rule3;
using RO.Common3;
using RO.Common3.Data;
using System.Text;

// DO NOT REMOVE as this is referenced by ComInstall.ascx.
namespace RO.Web
{
	public partial class AdmPuMkDeployModule : RO.Web.ModuleBase
	{
		private string LcSysConnString;
		private string LcAppPw;

		public AdmPuMkDeployModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["key"] != null)
			{
				System.DateTime dStartTime = System.DateTime.Now;
				Response.Write("Start time: " + dStartTime.ToLongTimeString().ToString() + ".\r\n");
				if ((new AdmPuMkDeploySystem()).UpdReleaseBuild(Int16.Parse(Request.QueryString["key"]), (new LoginSystem()).GetRbtVersion()))
				{
					Deploy dp = new Deploy();
					base.CSrc = new CurrSrc(true, null); base.CTar = new CurrTar(true, null);
					string sbWarnMsg = dp.PrepInstall(Int32.Parse(Request.QueryString["key"]), base.CSrc, base.CTar, LcSysConnString, LcAppPw);
					if (sbWarnMsg != string.Empty) {PreMsgPopup(sbWarnMsg);}
					System.DateTime dEndTime = System.DateTime.Now;
					Response.Write("End time: " + dEndTime.ToLongTimeString().ToString() + ".\r\n");
					Response.Write("Total lapsed time: " + ((System.TimeSpan)(dEndTime - dStartTime)).ToString() + "\r\n");
					Response.Write("Deployment for release ID:" + Request.QueryString["key"] + " completed successfully.\r\n\r\n");
				}
				else
				{
					Response.Write("Deployment for release ID:" + Request.QueryString["key"] + " aborted, please try again.\r\n\r\n");
				}
				Response.End();
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
			if (Request.QueryString["csy"] == null || Request.QueryString["csy"].ToString() == string.Empty)
			{
				PreMsgPopup("Please make sure QueryString has 'csy=' followed by the SystemId and try again.");
			}
			else if (LcSysConnString == null)
			{
				SetSystem(byte.Parse(Request.QueryString["csy"].ToString()));
			}

		}
		#endregion

		protected void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
		}

        private void PreMsgPopup(string msg)
        {
            int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
            string iconUrl = "images/warning.gif";
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