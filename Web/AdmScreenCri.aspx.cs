using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RO.Common3;

namespace RO.Web
{
	public partial class AdmScreenCri : RO.Web.PageBase
	{

		public AdmScreenCri()
		{
			Page.PreInit += new System.EventHandler(Page_PreInit);
			Page.Init += new System.EventHandler(Page_Init);
			Page.Title = Config.WebTitle + " - Screen Criteria";
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
	}
}
