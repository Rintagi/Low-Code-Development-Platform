using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Security.Principal;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using System.Linq;

namespace RO.Web
{
	public partial class ShowPageModule : RO.Web.ModuleBase
	{
        private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppUsrId;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
        private byte LcSystemId;

        DataTable dtPage;
        string sharedCssContent = string.Empty;
        string sharedCssName = string.Empty;
        string sharedJsContent = string.Empty;
        string sharedJsName = string.Empty;
		public ShowPageModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                TranslateItems();
                /* Stop IIS from Caching but allowing export to Excel to work */
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));
                Response.Cache.SetValidUntilExpires(true);
                new AdminSystem().LogUsage(base.LUser != null ? base.LUser.UsrId : 0, string.Empty, dtPage.Rows[0]["StaticPgNm259"].ToString(), 0, Int32.Parse(Request.QueryString["pg"].ToString().ToString().Split(new char[] { ',' }).First()), 0, string.Empty, LcSysConnString, LcAppPw);
                cStyle.Controls.Add(new LiteralControl("<style>" + (!string.IsNullOrEmpty(sharedCssName) ? "/* " + sharedCssName.Replace("*", "").Replace("/", "") + "*/" : "") + sharedCssContent + " " + dtPage.Rows[0]["StaticPgCss259"].ToString() + "</style>"));
                cContent.Controls.Add(new LiteralControl(dtPage.Rows[0]["StaticPgHtm259"].ToString()));
                Page.Title = dtPage.Rows[0]["StaticPgTitle259"].ToString();
                HtmlMeta meta = new HtmlMeta();
                meta.Name = "Description";
                meta.Content = dtPage.Rows[0]["StaticMeta259"].ToString();
                Page.Header.Controls.Add(meta);
                cJS.Controls.Add(new LiteralControl("<script>" + (!string.IsNullOrEmpty(sharedJsName) ? "/* " + sharedJsName.Replace("*", "").Replace("/", "") + "*/" : "") + sharedJsContent + " " + dtPage.Rows[0]["StaticPgJs259"].ToString() + "</script>"));
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
            if ((Request.QueryString["csy"] ?? "").ToString() == string.Empty)
            {
                throw new Exception("Please make sure QueryString has 'csy=' followed by the SystemId and try again.");
            }
            if ((Request.QueryString["pg"] ?? "").ToString() == string.Empty)
            {
                throw new Exception("Please make sure QueryString has 'pg=' followed by the PageId and try again.");
            }
            CheckAuthentication(true, false);

            byte SystemId = byte.Parse(Request.QueryString["csy"].ToString().Split(new char[] { ',' }).First());
            SetSystem(SystemId);
            /* GetAdmPage115ById must match the AdmPage definition, thus changes there may invalidate this gary */
            dtPage = (new AdminSystem()).GetMstById("GetAdmStaticPg114ById", Request.QueryString["pg"].ToString().Split(new char[] { ',' }).First(), LcSysConnString, LcAppPw);
            if (dtPage.Rows.Count == 0) { Response.Redirect(Config.OrdUrl); }
            else
            {
                string StaticCsId = (dtPage.Rows[0]["StaticCsId259"] ?? "").ToString();
                string StaticJsId = (dtPage.Rows[0]["StaticJsId259"] ?? "").ToString();
                try
                {
                    if (!string.IsNullOrEmpty(StaticCsId))
                    {
                        DataTable dtCss = (new AdminSystem()).GetMstById("GetAdmStaticCs115ById", StaticCsId, LcSysConnString, LcAppPw);
                        if (dtCss.Rows.Count > 0)
                        {
                            sharedCssContent = dtCss.Rows[0]["StyleDef260"].ToString();
                            sharedCssName = dtCss.Rows[0]["StaticCsNm260"].ToString();
                        }
                    }
                }
                catch { throw; }
                try
                {
                    if (!string.IsNullOrEmpty(StaticJsId))
                    {
                        DataTable dtJs = (new AdminSystem()).GetMstById("GetAdmStaticJs116ById", StaticJsId, LcSysConnString, LcAppPw);
                        if (dtJs.Rows.Count > 0)
                        {
                            sharedJsContent = dtJs.Rows[0]["ScriptDef261"].ToString();
                            sharedJsName = dtJs.Rows[0]["StaticJsNm261"].ToString();
                        }
                    }
                }
                catch { throw; }
            }
        }
		#endregion

		protected void SetSystem(byte SystemId)
		{
            this.SystemsDict = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);

			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcDesDb = base.DesDb(SystemId);
			LcAppDb = base.AppDb(SystemId);
			LcAppUsrId = base.AppUsrId(SystemId);
			LcAppPw = base.AppPwd(SystemId);
            LcSystemId = SystemId;
        }

        private DataTable GetLabels()
        {
            string labelCat = dtPage.Rows[0]["StaticPgNm259"].ToString();
            DataTable dtLabel = (new AdminSystem()).GetLabels(base.GetCurrCultureId(), labelCat, null, LcSysConnString, LcAppPw);
            DataColumn[] pkey = new DataColumn[1];
            pkey[0] = dtLabel.Columns[0];
            dtLabel.PrimaryKey = pkey;
            return dtLabel;
        }

        protected void TranslateItems()
        {
            DataTable dtLabel = GetLabels();
            foreach (DataRow dr in dtLabel.Rows)
            {
                dtPage.Rows[0]["StaticPgHtm259"] = dtPage.Rows[0]["StaticPgHtm259"].ToString().Replace("[[" + dr[0].ToString() + "]]", dr[1].ToString());
                dtPage.Rows[0]["StaticPgTitle259"] = dtPage.Rows[0]["StaticPgTitle259"].ToString().Replace("[[" + dr[0].ToString() + "]]", dr[1].ToString());
                dtPage.Rows[0]["StaticMeta259"] = dtPage.Rows[0]["StaticMeta259"].ToString().Replace("[[" + dr[0].ToString() + "]]", dr[1].ToString());
            }
        }
    }
}
