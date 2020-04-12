namespace RO.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using RO.Facade3;
    using RO.Common3;
    using RO.Common3.Data;

    public partial class FooterModule : RO.Web.ModuleBase
    {
        private const string KEY_FooterGenerated = "Cache:FooterGenerated";

        public FooterModule()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isFullyLicensed = RO.Common3.Utils.IsFullyLicense("Design", "Deploy");
                Tuple<string, bool, string> licenseDetail = RO.Common3.Utils.DecodeLicense(null);
                Dictionary<string, Dictionary<string, string>> moduleList = RO.Common3.Utils.DecodeLicenseDetail(licenseDetail.Item1);
                Dictionary<string, string> admLicenseDetail = moduleList.ContainsKey("Design") ? moduleList["Design"] : null;
                if (Session[KEY_FooterGenerated] == null) try
                {
                    if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Footer", 0, 0, 0, string.Empty, string.Empty))
                    {
                        (new GenSectionSystem()).CreateProgram("F", base.CPrj, base.CSrc);
                        Session[KEY_FooterGenerated] = true; this.Redirect(Request.RawUrl);
                    }
                }
                catch (Exception err) { throw new ApplicationException(err.Message); }
                if (Request.IsAuthenticated && base.LUser != null)
                {
                    try {
                        byte sid = byte.Parse(((DropDownList)Page.Master.FindControl("ModuleHeader").FindControl("ModuleProfile").FindControl("SystemsList")).SelectedValue);
                        cVersionTxt.Text = "&#169;1999-" + DateTime.Now.Year.ToString()
                                            + " Robocoder Corporation. All rights reserved (V" + (new LoginSystem()).GetAppVersion(base.SysConnectStr(sid), base.AppPwd(sid))
                                            + " by R" + (new LoginSystem()).GetRbtVersion() + "). Protected by U.S. Patent 6,876,314."
                                            + (
                                                isFullyLicensed && !licenseDetail.Item2
                                                ? " Perpetual license"
                                                : !licenseDetail.Item2 || admLicenseDetail == null ? " Trial license"
                                                : string.Format(" Licensed till {0}", admLicenseDetail["Expiry"])
                                                )
                                                ;
                    } catch { cVersionTxt.Text = "&#169;1999-" + DateTime.Now.Year.ToString() + " Robocoder Corporation. All rights reserved."; }
                }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
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

    }
}
