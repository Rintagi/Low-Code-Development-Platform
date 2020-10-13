namespace RO.Web
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using RO.Facade3;
    using RO.Common3;
    using RO.Common3.Data;

    public partial class HeaderModule : RO.Web.ModuleBase
    {
        private const string KEY_HeaderGenerated = "Cache:HeaderGenerated";

        public HeaderModule()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[KEY_HeaderGenerated] == null) try
                {
                    if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Header", 0, 0, 0, string.Empty, string.Empty))
                    {
                        (new GenSectionSystem()).CreateProgram("H", base.CPrj, base.CSrc);
                        Session[KEY_HeaderGenerated] = true; this.Redirect(Request.RawUrl);
                    }
                }
                catch (Exception err) { throw new ApplicationException(err.Message); }
                if (!Request.IsAuthenticated || LUser == null || LUser.LoginName.ToLower() == "anonymous")
                {
                    string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
                    if (string.IsNullOrEmpty(loginUrl)) loginUrl = "~/MyAccount.aspx";
                    cSignIn.Visible = true; cSignIn.NavigateUrl = loginUrl + (loginUrl.Contains("?") ? "&" : "?") + "logo=N"; cProfileButton.Visible = false;
                }
                else
                {
                    cSignIn.Visible = false; cProfileButton.Visible = true;
                }
                if (Request.IsAuthenticated && base.LUser != null)
                {
                    SetCultureId(cLang, LUser.CultureId.ToString());
                    cWelcomeTime.Text = Utils.fmLongDate(DateTime.Now.ToString(), LUser.Culture ?? "en-us");
                }
                if (base.LUser != null && base.LPref != null && Request.QueryString["typ"] != null && Request.QueryString["typ"].ToString().ToUpper() == "N")
                {
                    cLogoHolder.Visible = false;
                    cLinkHolder.Visible = false;
                    cSociHolder.Visible = false;
                }
                if (cLinkHolder.Controls.Count <= 0) { cLinkButton.Visible = false; }
                if (cSociHolder.Controls.Count <= 0) { cSociButton.Visible = false; }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (this.Visible && Request.IsAuthenticated && base.LUser != null && base.VMenu != null)
            {
                base.VMenu.RowFilter = "QId='" + PageBase.ExpandNode + "'";
            }
            if (base.VMenu != null && base.VMenu.Count > 0)
            {
                string ftr = base.VMenu.RowFilter;
                cBreadCrumb.Text = "<strong>" + base.VMenu[0]["MenuText"].ToString() + "</strong>";
                while (base.VMenu[0]["ParentQId"].ToString() != string.Empty)
                {
                    base.VMenu.RowFilter = "QId='" + base.VMenu[0]["ParentQId"].ToString() + "'";
                    cBreadCrumb.Text = base.VMenu[0]["MenuText"].ToString() + "&gt; " + cBreadCrumb.Text;
                }
                base.VMenu.RowFilter = ftr;
                if (HttpContext.Current.Request.Url.AbsolutePath.Contains(Config.SslUrl)) { cMobileCrumb.Text = Config.WebTitle; } else { cMobileCrumb.Text = base.VMenu[0]["MenuText"].ToString(); }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
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

        protected void cbPostBack(object sender, System.EventArgs e)
        {
        }

        protected void cbCultureId(object sender, System.EventArgs e)
        {
            SetCultureId((RoboCoder.WebControls.ComboBox)sender, string.Empty);
        }

        private void SetCultureId(RoboCoder.WebControls.ComboBox ddl, string keyId)
        {
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCultureId";
            context["addnew"] = "Y";
            context["mKey"] = "CultureTypeId";
            context["mVal"] = "CultureTypeLabel";
            context["mTip"] = "CultureTypeLabel";
            context["mImg"] = "CultureTypeLabel";
            context["ssd"] = Request.QueryString["ssd"];
            context["scr"] = "1";
            context["csy"] = "3";
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;
            ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
            ddl.DataContext = context;
            if (ddl != null)
            {
                DataView dv = null;
                if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) { keyId = ddl.SearchText.Substring(2); }
                try { dv = new DataView((new AdminSystem()).GetDdl(1, "GetDdlCultureId", true, false, 0, keyId, null, null, string.Empty, base.LImpr, base.LCurr)); } catch { return; }
                ddl.DataSource = dv;
                try { ddl.SelectByValue(keyId, string.Empty, false); } catch { try { ddl.SelectedIndex = 0; } catch { } }
            }
        }

        protected void cLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cLang.SelectedValue))
            {
                base.LUser.CultureId = short.Parse(cLang.SelectedValue);
                base.LUser.Culture = (new AdminSystem()).SetCult(base.LUser.UsrId, base.LUser.CultureId);
                base.LImpr = null; SetImpersonation(LUser.UsrId);
                base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));
                this.Redirect(Request.Url.PathAndQuery);    // No need to SetCultureId(cLang, LUser.CultureId.ToString());
            }
        }

        protected void lanResetBtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            base.LUser.CultureId = 1;
            base.LUser.Culture = (new AdminSystem()).SetCult(base.LUser.UsrId, base.LUser.CultureId);
            if ((LUser.LoginName ?? string.Empty).ToLower() != "anonymous") { base.LImpr = null; SetImpersonation(LUser.UsrId); } else { base.LImpr.Cultures = base.LUser.CultureId.ToString(); }
            base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));
            this.Redirect(Request.Url.PathAndQuery);
        }
    }
}
