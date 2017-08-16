namespace RO.Web
{
	using System;
	using System.Data;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.ComponentModel;

	public class PageBase : System.Web.UI.Page
	{
		private static string PageExpandNode;	//Temporary bug get-around on treeview auto-collapse.

		public PageBase()
		{
		}

		public static string ExpandNode
		{
			get
			{
				return PageExpandNode;
			}
			set
			{
				PageExpandNode = value;
			}
		}

        protected void SetMasterPage()
        {
            string MasterPgFile = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["m"]))
            {
                MasterPgFile = Request.QueryString["m"].ToString();
            }
            else
            {
                try { MasterPgFile = ((RO.Common3.Data.UsrPref)Session["Cache:LPref"]).MasterPgFile; }
                catch { }
                finally { if (string.IsNullOrEmpty(MasterPgFile)) try { MasterPgFile = RO.Common3.Config.MasterPgFile; } catch { MasterPgFile = "Default.master"; } }
            }
            if (string.IsNullOrEmpty(MasterPgFile) || !System.IO.File.Exists(Server.MapPath(MasterPgFile))) { Page.MasterPageFile = "Default.Master"; } else { Page.MasterPageFile = MasterPgFile; }
        }
    }
}