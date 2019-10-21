using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using RO.Facade3;
using RO.Common3;

namespace RO.Web
{
	public partial class MyProfileModule : RO.Web.ModuleBase
	{
		private string LcSysConnString;
		private string LcAppPw;
        
        public MyProfileModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated || LUser == null || LUser.LoginName.ToLower() == "anonymous")
                {
                    string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
                    if (string.IsNullOrEmpty(loginUrl)) loginUrl = "~/MyAccount.aspx";
                    SignIn.Visible = true; SignIn.NavigateUrl = loginUrl + (loginUrl.Contains("?") ? "&" : "?") + "logo=N";
                    SignoutPanel.Visible = false;
                    cLoginName.Visible = false;
                    cAppDomainUrl.Visible = false;
                    return;
                }
                else
                {
                    string extAppDomainUrl =
                        !string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["ExtBaseUrl"])
                            ? System.Configuration.ConfigurationManager.AppSettings["ExtBaseUrl"]
                            : Request.Url.AbsoluteUri.Replace(Request.Url.Query, "").Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");
                    cAppDomainUrl.Text = extAppDomainUrl.EndsWith("/") ? extAppDomainUrl.Substring(0, extAppDomainUrl.Length - 1) : extAppDomainUrl;
                    cAppDomainUrl.Visible = true;
                    SignIn.Visible = false;
                    SignoutPanel.Visible = true;
                    cLoginName.Text = LUser.LoginName;
                    cLoginName.Visible = true;
                }
                if (LUser != null)
                {
                    if (base.LPref != null)
                    {
                        switch (base.LPref.SysListVisible)
                        {
                            case "N":
                                SystemsLabel.Visible = false; SystemsList.Visible = false; SystemsList.Enabled = true; CurrSys.Visible = false; break;
                            case "R":
                                SystemsLabel.Visible = true; SystemsList.Visible = true; SystemsList.Enabled = false; CurrSys.Visible = true; break;
                            default:
                                SystemsLabel.Visible = true; SystemsList.Visible = true; SystemsList.Enabled = true; CurrSys.Visible = true; break;
                        }
                        switch (base.LPref.PrjListVisible)
                        {
                            case "N":
                                ProjectLabel.Visible = false; ProjectList.Visible = false; ProjectList.Enabled = true; CurrPrj.Visible = false; break;
                            case "R":
                                ProjectLabel.Visible = true; ProjectList.Visible = true; ProjectList.Enabled = false; CurrPrj.Visible = true; break;
                            default:
                                ProjectLabel.Visible = true; ProjectList.Visible = true; ProjectList.Enabled = true; CurrPrj.Visible = true; break;
                        }
                        switch (base.LPref.ComListVisible)
                        {
                            case "N":
                                CompanyLabel.Visible = false; CompanyList.Visible = false; CompanyList.Enabled = true; CurrCmp.Visible = false; break;
                            case "R":
                                CompanyLabel.Visible = true; CompanyList.Visible = true; CompanyList.Enabled = false; CurrCmp.Visible = true; break;
                            default:
                                CompanyLabel.Visible = true; CompanyList.Visible = true; CompanyList.Enabled = true; CurrCmp.Visible = true; break;
                        }
                    }

                    CompanyList.Attributes["onchange"] = "javascript:return CanPostBack(true, this);"; CompanyList.Attributes["NeedConfirm"] = "Y";
                    ProjectList.Attributes["onchange"] = "javascript:return CanPostBack(true, this);"; ProjectList.Attributes["NeedConfirm"] = "Y";
                    SystemsList.Attributes["onchange"] = "javascript:return CanPostBack(true, this);"; SystemsList.Attributes["NeedConfirm"] = "Y";

                    UserLabel.Text = LUser != null ? LUser.UsrName : Context.User.Identity.Name;
                    if (LUser.HasPic) { UsrPic.ImageUrl = base.GetUrlWithQSHash("~/DnLoad.aspx?key=" + LUser.UsrId.ToString() + "&tbl=dbo.Usr&knm=UsrId&col=PicMed&sys=3"); UsrPic.Visible = true; }

                    SystemsList.DataSource = GetSystemsList(); 
                    CompanyList.DataSource = GetCompanyList();
                    ProjectList.DataSource = GetProjectList();
                    TimeZoneList.DataSource = GetTimeZoneList();
                    
                    TranslateItems();

                    this.DataBind();
                    try { SystemsList.SelectedValue = LCurr.SystemId.ToString(); CurrSys.Text = SystemsList.SelectedItem.Text; }
                    catch { }
                    try { CompanyList.SelectedValue = LCurr.CompanyId.ToString(); CurrCmp.Text = CompanyList.SelectedItem.Text; }
                    catch { }
                    try { ProjectList.SelectedValue = LCurr.ProjectId.ToString(); CurrPrj.Text = ProjectList.SelectedItem.Text; }
                    catch { }
                    try { TimeZoneList.SelectedValue = CurrTimeZoneInfo().Id; }
                    catch { }
                    cMyAccountLink.NavigateUrl = System.Web.Security.FormsAuthentication.LoginUrl; // +(Request.QueryString["typ"] != null ? "?typ=" + Request.QueryString["typ"] : "");
                }
            }
            else if (Request.IsAuthenticated && base.LUser != null)	// Get around selectedIndexChange event not triggered because of viewstate-in-session.
            {
                if (SystemsList.Items.Count <= 0 && Request.Params["__EVENTTARGET"] == SystemsList.UniqueID)
                {
                    SystemsList.DataSource = GetSystemsList(); SystemsList.DataBind();
                    SystemsList.SelectedValue = Request.Params[SystemsList.UniqueID];
                    SystemsList_SelectedIndexChanged(SystemsList, new EventArgs());
                }
                if (CompanyList.Items.Count <= 0 && Request.Params["__EVENTTARGET"] == CompanyList.UniqueID)
                {
                    CompanyList.DataSource = GetCompanyList(); CompanyList.DataBind();
                    CompanyList.SelectedValue = Request.Params[CompanyList.UniqueID];
                    CompanyList_SelectedIndexChanged(CompanyList, new EventArgs());
                }
                if (ProjectList.Items.Count <= 0 && Request.Params["__EVENTTARGET"] == ProjectList.UniqueID)
                {
                    ProjectList.DataSource = GetProjectList(); ProjectList.DataBind();
                    ProjectList.SelectedValue = Request.Params[ProjectList.UniqueID];
                    ProjectList_SelectedIndexChanged(ProjectList, new EventArgs());
                }
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
            CheckAuthentication(true);
        }
		#endregion

		private void CheckAuthentication(bool pageLoad)
		{
 
        }

        public void SignoutButton_Click(object sender, System.EventArgs e)
        {
            bool bNotEmbedded = string.IsNullOrEmpty(Request.QueryString["typ"]);
            base.Signout(bNotEmbedded);
            if (!bNotEmbedded)
            {
                string loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
                if (string.IsNullOrEmpty(loginUrl)) loginUrl = "MyAccount.aspx";
                Response.Redirect(loginUrl + (loginUrl.Contains("?") ? "&" : "?") + "typ=" + Request.QueryString["typ"].ToString());
            }
        }
        private DataView GetCompanyList()
        {
            DataTable dt = (DataTable)Session["CompanyList"];
            if (dt == null)
            {
                dt = (new LoginSystem()).GetCompanyList(base.LImpr.Usrs, base.LImpr.RowAuthoritys, base.LImpr.Companys);
                Session["CompanyList"] = dt;
            }
            return dt.DefaultView;
        }
        private DataView GetTimeZoneList()
        {
            DataTable dtTimeZone = Session["TimeZoneList"] as DataTable;
            if (dtTimeZone == null)
            {
                dtTimeZone = new DataTable();
                dtTimeZone.Columns.Add("TimeZoneId", typeof(string));
                dtTimeZone.Columns.Add("TimeZoneName", typeof(string));
                foreach (TimeZoneInfo tzinfo in TimeZoneInfo.GetSystemTimeZones())
                {
                    DataRow dr = dtTimeZone.NewRow();
                    dr["TimeZoneId"] = tzinfo.Id;
                    dr["TimeZoneName"] = tzinfo.DisplayName;
                    dtTimeZone.Rows.Add(dr);
                }
                Session["TimeZoneList"] = dtTimeZone;
            }
            return dtTimeZone.DefaultView;
        }

        protected void CompanyList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CompanyList.SelectedValue != null && CompanyList.SelectedValue != string.Empty)
            {
                base.LCurr.CompanyId = Int32.Parse(CompanyList.SelectedValue);
            }
            else { base.LCurr.CompanyId = 0; }
            ProjectList.Items.Clear(); Session.Remove("ProjectList"); ProjectList.DataSource = GetProjectList(); ProjectList.DataBind();
            ListItem li = null;
            li = ProjectList.Items.FindByValue(base.LCurr.ProjectId.ToString());
            if (li != null) { ProjectList.ClearSelection(); li.Selected = true; base.LCurr.ProjectId = Int16.Parse(li.Value); }
            else if (ProjectList.Items.Count > 0)
            {
                ProjectList.Items[0].Selected = true;
                try
                {
                    base.LCurr.ProjectId = Int16.Parse(ProjectList.Items[0].Value);
                }
                catch { base.LCurr.ProjectId = 0; }
            }
            else base.LCurr.ProjectId = 0;
            SwitchCmpPrj();
            Response.Redirect(Request.RawUrl);
        }

        private DataView GetProjectList()
        {
            DataTable dt = (DataTable)Session["ProjectList"];
            if (dt == null)
            {
                dt = (new LoginSystem()).GetProjectList(base.LImpr.Usrs, base.LImpr.RowAuthoritys, base.LImpr.Projects, base.LCurr.CompanyId.ToString());
                Session["ProjectList"] = dt;
            }
            return dt.DefaultView;
        }

        protected void ProjectList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ProjectList.SelectedValue != null && ProjectList.SelectedValue != string.Empty)
            {
                base.LCurr.ProjectId = Int16.Parse(ProjectList.SelectedValue);
            }
            else { base.LCurr.ProjectId = 0; }
            SwitchCmpPrj();
            Response.Redirect(Request.RawUrl);
        }

        private DataView GetSystemsList()
        {
            DataTable dt = base.SystemsList;
            if (dt == null)
            {
                dt = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
                base.SystemsDict = dt;      // Instantiate base.SystemsList as well.
            }
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Active='Y'";    // Only display active systems.
            return dv;
        }

        protected void TimeZoneList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            foreach (TimeZoneInfo tzinfo in TimeZoneInfo.GetSystemTimeZones())
            {
                if (tzinfo.Id == TimeZoneList.SelectedValue)
                {
                    Session["Cache:tzInfo"] = tzinfo;
                    Session["Cache:tzUserSelect"] = "1";
                    break;
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void SystemsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            base.LCurr.SystemId = byte.Parse(SystemsList.SelectedValue);
            SwitchSystem(base.LCurr);
            string webAddress = SysWebAddress(byte.Parse(SystemsList.SelectedValue));
            string currentUrl = Request.Url.PathAndQuery.Replace(Request.Url.Query, "");
            string homeQs = "";
            if (!string.IsNullOrEmpty(webAddress) && !webAddress.StartsWith("http"))
            {
                string path = Request.Url.GetComponents(UriComponents.Path, UriFormat.Unescaped);
                webAddress = path.ToUpper().StartsWith(Config.AppNameSpace.ToUpper() + "/") ? "/" + Config.AppNameSpace + "/" + webAddress : "/" + webAddress;
                webAddress = Request.Url.GetLeftPart(UriPartial.Scheme).Replace("http:",Config.EnableSsl ? "https:" : "http:") + Request.Url.Host + Request.Url.AbsolutePath.Replace("/" + path, webAddress);
                homeQs = (new Uri(webAddress)).GetComponents(UriComponents.Query, UriFormat.Unescaped);
            }
            else webAddress = "";

            if (currentUrl.ToLower().EndsWith("default.aspx"))
            {
                string strUrl = !string.IsNullOrEmpty(webAddress) ? webAddress : Request.RawUrl;
                System.Collections.Specialized.NameValueCollection qs = System.Web.HttpUtility.ParseQueryString(new System.Text.RegularExpressions.Regex("^[^?]*[\\?]?").Replace(strUrl, ""));
                qs["msy"] = base.LCurr.SystemId.ToString();
                if (Request.QueryString["ssd"] != null) qs["ssd"] = Request.QueryString["ssd"];
                Response.Redirect((strUrl.IndexOf("?") < 0 ? strUrl : strUrl.Substring(0, strUrl.IndexOf("?"))) + "?" + qs.ToString());
            }
            else
            {
                try
                {
                    string defaultUrl = !string.IsNullOrEmpty(webAddress) ? webAddress : new Uri(Config.SslUrl).PathAndQuery.Replace("/" + Config.AppNameSpace + "/", "");
                    System.Collections.Specialized.NameValueCollection qs = System.Web.HttpUtility.ParseQueryString(new System.Text.RegularExpressions.Regex("^[^?]*[\\?]?").Replace(defaultUrl, ""));
                    if (qs["msy"] == null) qs["msy"] = base.LCurr.SystemId.ToString();
                    if (Request.QueryString["ssd"] != null) qs["ssd"] = Request.QueryString["ssd"];
                    Response.Redirect((defaultUrl.IndexOf("?") < 0 ? defaultUrl : defaultUrl.Substring(0, defaultUrl.IndexOf("?"))) + "?" + qs.ToString());
                }
                catch (ThreadAbortException) { }
                catch
                {
                    string defaultUrl = !string.IsNullOrEmpty(webAddress) ? webAddress : Config.SslUrl;
                    System.Collections.Specialized.NameValueCollection qs = System.Web.HttpUtility.ParseQueryString(new System.Text.RegularExpressions.Regex("^[^?]*[\\?]?").Replace(defaultUrl, ""));
                    if (qs["msy"] == null) qs["msy"] = base.LCurr.SystemId.ToString();
                    Response.Redirect((defaultUrl.IndexOf("?") < 0 ? defaultUrl : defaultUrl.Substring(0, defaultUrl.IndexOf("?"))) + "?" + qs.ToString());
                }
            }
        }

        private DataTable GetLabels()
        {
            DataTable dtLabel = (new AdminSystem()).GetLabels(base.GetCurrCultureId(), "MyProfileModule", null, null, null);
            DataColumn[] pkey = new DataColumn[1];
            pkey[0] = dtLabel.Columns[0];
            dtLabel.PrimaryKey = pkey;
            return dtLabel;
        }
        private void TranslateItems()
        {
            DataTable dtLabel = GetLabels();
            if (dtLabel != null)
            {
                TranslateItem(CompanyLabel, dtLabel.Rows, "Company");
                TranslateItem(ProjectLabel, dtLabel.Rows, "Project");
                TranslateItem(SystemsLabel, dtLabel.Rows, "System");
                TranslateItem(SignoutButton1, dtLabel.Rows, "SignOut");
                TranslateItem(SignoutButton2, dtLabel.Rows, "SignOut");
                TranslateItem(cMyAccountLink, dtLabel.Rows, "MyAccount");
                SignoutButton1.ToolTip = TranslateItem(dtLabel.Rows, "SignOutToolTip");
                SignoutButton2.ToolTip = TranslateItem(dtLabel.Rows, "SignOutToolTip");
            }
        }
    }
}