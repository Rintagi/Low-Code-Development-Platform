namespace RO.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;
	using System.Text;
	using RO.Facade3;

	public partial class UsrPrefModule : RO.Web.ModuleBase
	{
		public UsrPrefModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                StringBuilder sb = new StringBuilder();
				if (base.LPref == null && Request.QueryString["cmp"] != null && Request.QueryString["cmp"] != string.Empty)
				{
					try { base.LPref = (new LoginSystem()).GetUsrPref(0, Int32.Parse(Request.QueryString["cmp"].ToString()), 0, 3); }
					catch { }
				}
                if (base.LPref != null)
                {
                    if (base.LPref.UsrStyleSheet != null && base.LPref.UsrStyleSheet.Trim() != string.Empty)
                    {
                        sb.Append(base.LPref.UsrStyleSheet);
                    }
                }
                else
                {
                    /* Stop IIS from Caching but allowing export to Excel to work */
                    Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();
                    Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));
                    Response.Cache.SetValidUntilExpires(true);
                }
                Response.ContentType = "text/css";
                Response.Write(sb);  
				Response.Flush();
				Response.End();
			}
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

		}
		#endregion
	}
}