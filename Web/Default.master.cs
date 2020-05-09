using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RO.Web
{
    public partial class DefaultMaster : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack && Request.QueryString["typ"] != null && (Request.QueryString["typ"] ?? "").ToUpper().Split(new char[] { ',' })[0].ToLower() == "n")
			{
                ModuleHeader.Visible = false; ModuleSidebar.Visible = false; ModuleFooter.Visible = false;
            }
		}
	}
}
