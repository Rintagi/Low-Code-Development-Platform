using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.WebRules;

namespace RO.Web
{
	public partial class HelpModule : RO.Web.ModuleBase
	{
        public string HelpTitle
        {
            get
            {
                return base.ViewState["HelpTitle"] as string;
            }
            set
            {
                base.ViewState["HelpTitle"] = value;
            }
        }

        public string HelpMsg
		{
			get 
			{
				return base.ViewState["HelpMsg"] as string;
			}
			set
			{
				base.ViewState["HelpMsg"] = value;
			}
        }

        //can only be used once on a screen..
		public HelpModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            TranslateItems();
            //if (!IsPostBack)
            //{
            //    HelpMsg = HttpUtility.HtmlAttributeEncode(HelpMsg ?? "").Replace("\"", "'").Replace("'", "\\'").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
            //    HelpTitle = HttpUtility.HtmlAttributeEncode(HelpTitle ?? "").Replace("\"", "'").Replace("'", "\\'").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
            //}
            //if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            //{
            //    cHelpTxt.Text = System.Web.HttpUtility.HtmlEncode(HelpMsg);
            //    cTitle.Text = System.Web.HttpUtility.HtmlEncode(HelpTitle);
            //}
            cHelpTxt.Text = HelpMsg;
            cTitle.Text = HelpTitle;
		}
        
		protected void Page_Init(object sender, EventArgs e)
		{
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		#endregion

        private DataTable GetLabels()
        {
            DataTable dtLabel = (new AdminSystem()).GetLabels(LUser != null && LUser.LoginName.ToLower() != "anonymous" ? LUser.CultureId : (short)1, "HelpModule", null, null, null);
            DataColumn[] pkey = new DataColumn[1];
            pkey[0] = dtLabel.Columns[0];
            dtLabel.PrimaryKey = pkey;
            return dtLabel;
        }

        private void TranslateItems()
        {
            DataTable dtLabel = GetLabels();
            AdminSystem adm = new AdminSystem();
            if (dtLabel != null)
            {
                TranslateItem(cImage, dtLabel.Rows, "cHelpImg");
            }
        }
    }
}

