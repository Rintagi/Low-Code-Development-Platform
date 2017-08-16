using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using RO.Facade3;
using RO.Common3;
using System.Linq;

namespace RO.Web
{
    public partial class ImportSchemaModule : RO.Web.ModuleBase
    {
		private string LcSysConnString;
		private string LcAppPw;

		public ImportSchemaModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

        protected void Page_Load(object sender, EventArgs e)
        {

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
			if (!string.IsNullOrEmpty(Request.QueryString["csy"]))
			{
                SetSystem(byte.Parse(Request.QueryString["csy"].ToString().Split(new char[] { ',' }).First()));
            }

		}
		#endregion

		protected void SetSystem(byte SystemId)
		{
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppPw = base.AppPwd(SystemId);
		}

        public string GetWizardSchema()
        {
            string schema = "";

            if (string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                try { schema = Session["ImportSchema"].ToString(); }
                catch { }
            }
            else
            {
                int wizardId = int.Parse(Request.QueryString["key"]);
				schema = (new AdminSystem()).GetSchemaWizImp(wizardId, base.LUser.CultureId, LcSysConnString, LcAppPw).ToString();
            }
            return schema;
        }

        public string GetScrSchema()
        {
            string schema = "";

            if (string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                try { schema = Session["ImportSchema"].ToString(); }
                catch { }
            }
            else
            {
                int screenId = int.Parse(Request.QueryString["key"]);
                schema = (new AdminSystem()).GetSchemaScrImp(screenId, base.LUser.CultureId, LcSysConnString, LcAppPw);
            }
            return schema;
        }
        public string GetWizardTmpl()
        {
            int wizardId = int.Parse(Request.QueryString["key"]);
            string tmplXml = (new AdminSystem()).GetWizImpTmpl(wizardId, base.LUser.CultureId, LcSysConnString, LcAppPw).ToString();
            return tmplXml;
        }
        public string GetScrGridTmpl()
        {
            int screenId = int.Parse(Request.QueryString["key"]);
            string tmplXml = (new AdminSystem()).GetScrImpTmpl(screenId, base.LUser.CultureId, LcSysConnString, LcAppPw).ToString();
            return tmplXml;
        }
    }
}