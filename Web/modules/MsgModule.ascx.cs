using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;

namespace RO.Web
{
	public partial class MsgModule: RO.Web.ModuleBase
	{
		private byte sid;
		private string msg;
        
        public MsgModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Response.Expires = 0;
			Response.Cache.SetNoStore();
			Response.AppendHeader("Pragma","no-cache");
			if (Request.QueryString["typ"] != null)
			{
				if (Request.QueryString["typ"].ToString() == "E")
				{
					cImage.ImageUrl = "../Images/error.gif";
					msg = Session["ErrMsg"] as string;
				}
				else if (Request.QueryString["typ"].ToString() == "W")
				{
					cImage.ImageUrl = "../Images/warning.gif";
                    msg = Session["WarnMsg"] as string;
				}
				else
				{
					cImage.ImageUrl = "../Images/info.gif";
					msg = Session["InfoMsg"] as string;
				}
				if (msg == null || string.IsNullOrEmpty(msg)) { cMessage.Text = "<script language='javascript'>this.close();</script>"; }
				else if (base.LUser == null ) { cMessage.Text = msg; }
				else
				{
					string sr = msg;
					Regex re = new Regex("(\\|[0-9]{1,5}\\|)");
					if (re.IsMatch(sr))
					{
						Match m = re.Match(sr);
						sid = byte.Parse(m.Captures[0].Value.Replace("|", string.Empty));
						msg = re.Replace(msg, string.Empty);
					}
					else { sid = 0; }
                    //if (Config.Architect == "W")
                    //{
                    //    try
                    //    {
                    //        cMessage.Text = AdminFacade().GetMsg(msg, base.LUser.CultureId, base.LUser.TechnicalUsr, base.SysConnectStr(sid), base.AppPwd(sid));
                    //    }
                    //    catch	// In case the error is caused by web service not working:
                    //    {
                    //        cMessage.Text = (new AdminSystem()).GetMsg(msg, base.LUser.CultureId, base.LUser.TechnicalUsr, base.SysConnectStr(sid), base.AppPwd(sid));
                    //    }
                    //    int iSta = cMessage.Text.IndexOf("System.ApplicationException:");
                    //    int iEnd = cMessage.Text.IndexOf("\n   at");
                    //    if (iSta >= 0 && iEnd >= 0 && iEnd > iSta)
                    //    {
                    //        Session["ErrStackTrace"] = cMessage.Text.Substring(iEnd + 5);
                    //        cMessage.Text = cMessage.Text.Substring(iSta + 29, iEnd - iSta - 29);
                    //    }
                    //}
                    //else
                    //{
					cMessage.Text = (new AdminSystem()).GetMsg(msg, base.LUser.CultureId, base.LUser.TechnicalUsr, base.SysConnectStr(sid), base.AppPwd(sid));
					//}
				}
				if (!string.IsNullOrEmpty(Session["ErrStackTrace"] as string) && ((base.LUser == null && Config.DeployType!="PRD") || base.LUser.TechnicalUsr == "Y"))
				{
					cStackTrace.Text = Session["ErrStackTrace"].ToString();
					cStackTrace.Font.Name = "Verdana";
					cStackTrace.Font.Size = new FontUnit("11px");
					cTechPanel.Visible = true;
				}
				Session.Remove("ErrMsg"); Session.Remove("WarnMsg"); Session.Remove("InfoMsg"); Session.Remove("ErrStackTrace");
                cMessage.Text = ReformatErrMsg(cMessage.Text);
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

		}
		#endregion
	}
}