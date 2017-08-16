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

    public partial class SidebarModule : RO.Web.ModuleBase
    {
        private const string KEY_SidebarGenerated = "Cache:SidebarGenerated";

        public SidebarModule()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[KEY_SidebarGenerated] == null) try
                {
                    if (base.CPrj != null && base.CSrc != null && Config.DeployType == "DEV" && (new AdminSystem()).IsRegenNeeded("Sidebar", 0, 0, 0, string.Empty, string.Empty))
                    {
                        (new GenSectionSystem()).CreateProgram("S", base.CPrj, base.CSrc);
                        Session[KEY_SidebarGenerated] = true; Response.Redirect(Request.RawUrl);
                    }
                }
                catch (Exception err) { throw new ApplicationException(err.Message); }
                if (Request.IsAuthenticated && base.LUser != null)
                {
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
