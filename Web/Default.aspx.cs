namespace RO.Web
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RO.Common3;

	public partial class Default : PageBase
	{
        public Default()
		{
            Page.PreInit += new System.EventHandler(Page_PreInit);
            Page.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				Page.Title = System.Configuration.ConfigurationManager.AppSettings["WebTitle"];
				if (Request.QueryString["wrn"] != null)
				{
					if (Request.QueryString["wrn"].ToString() == "1")
					{
                        PreMsgPopup("Security: The requested is a secured area. Please login and try again.");
					}
					else
					{
						PreMsgPopup("Security: The requested is a secured area and the current session has expired. Please login again.");
                    }
				}
			}
		}

        protected void Page_PreInit(object sender, EventArgs e)
        {
            SetMasterPage();
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

        void PreMsgPopup(string msg)
        {
            string iconUrl = "images/warning.gif";
            string focusOnCloseId = string.Empty;
            string script =
            @"<script type='text/javascript' language='javascript'>
			PopDialog('" + iconUrl + "','" + msg.Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
            ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
        }
    }
}