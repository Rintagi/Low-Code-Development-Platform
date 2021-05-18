namespace RO.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RO.Facade3;
	using RO.Common3.Data;
	using System.Text;
	using System.Linq;
	using System.Collections.Generic;
	using System.Web.UI;

	public partial class MenuTopModule : RO.Web.ModuleBase
	{
        HashSet<string> mh = new HashSet<string>();
        private List<MenuNode> BuildMenuTree(DataView dvMenu, string[] path, int level, bool bRecurr)
		{
			List<MenuNode> menus = new List<MenuNode>();
			string selectedQid = string.Join(".", path);
			string[] subPath = path.Skip<string>(1).ToArray<string>();
			string[] emptyPath = new string[0];
            if (mh.Count == 0) foreach (DataRow dr in dvMenu.Table.Rows) { mh.Add(dr["MenuId"].ToString()); }

			foreach (DataRowView drv in dvMenu)
			{
				MenuNode mt = new MenuNode
				{
					ParentQId = drv["ParentQId"].ToString(),
					ParentId = drv["ParentId"].ToString(),
					QId = drv["QId"].ToString(),
					MenuId = drv["MenuId"].ToString(),
					NavigateUrl = Page.ResolveUrl(drv["NavigateUrl"].ToString()),
					QueryStr = drv["QueryStr"].ToString(),
					IconUrl = Page.ResolveUrl(drv["IconUrl"].ToString()),
                    Popup = drv["Popup"].ToString(),
                    GroupTitle = drv["GroupTitle"].ToString(),
					MenuText = drv["MenuText"].ToString(),
					Selected = selectedQid.StartsWith(drv["QId"].ToString()),
					Level = level,
					Children = bRecurr ? BuildMenuTree(new DataView(dvMenu.Table, string.Format("ParentId = {0}", drv["MenuId"].ToString()), "ParentId, ParentQId,Qid", DataViewRowState.CurrentRows), path, level + 1, bRecurr) : new List<MenuNode>()
				};
                if (mt.ParentId == "" || mh.Contains(mt.ParentId)) menus.Add(mt);
            }
			return menus;
		}
		
		public MenuTopModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Request.IsAuthenticated && base.LUser != null && base.LImpr != null && base.LPref != null)
			{
				if (base.VMenu == null && !IsCronInvoked() && !IsSelfInvoked())
				{
                    try
                    {
                        base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));
                    }
                    catch (Exception ex) 
                    { 
                        base.VMenu = null;
                        ErrorTrace(ex, "critical");
                    }
                }
                if (base.VMenu != null && base.VMenu.Count > 0)
				{
					if (Request.QueryString["id"] == null) { PageBase.ExpandNode = "00"; }
					else
					{
						base.VMenu.RowFilter = "";
						foreach (DataRowView rowChild in base.VMenu)
						{
							if (Request.QueryString["id"].ToString() == rowChild["MenuId"].ToString())
							{
								PageBase.ExpandNode = rowChild["Qid"].ToString(); break;
							}
						}
					}
				}
                if (base.LPref != null && base.VMenu != null && base.VMenu.Count > 0)
                {
                    /* if there is no id in query string, we should assume there is no current selection. */
                    string[] path = new string[0];
                    try
                    {
                        path = PageBase.ExpandNode.Split(new char[] { '.' });
                    }
                    catch { }

                    string menuCss = string.Format(@"<style type='text/css'>{0}</style>", LPref.TopMenuCss);
                    string menuInvokeJS = string.Format(@"<script language='javascript' type='text/javascript'>{0}{1}{2}</script>", LPref.TopMenuJs, LPref.TopMenuIvk, "");
                    if (string.IsNullOrEmpty(Request.QueryString["id"])) path = new string[0];
                    /* Prepare full menu for mobile */
                    List<string> sMenu = BuildsMenu(path, true);
                    MobileMenuId.Text = string.Format("{2}<ul id='TpMobileMenu' class='TpMobileMenuSub' style='display:none;'>{0}</ul>{1}", string.Join("", sMenu.ToArray()), "", "");
                    /* T is the top menu only and B is both the top and vertical.  Ignore S which is Vertical menu only. */
                    if (base.LPref.MenuOption == "B" || base.LPref.MenuOption == "T")
                    {
                        if (base.LPref.MenuOption == "T")
                        {
                            sMenu = BuildsMenu(path, base.LPref.MenuOption == "T");
                            // "visibility:hidden;" instead of "display:none;" for Vertical Menu:
                            MenuId.Text = string.Format(@"{2}<script language='javascript' type='text/javascript'>DuplicateMenuFromMobile();</script>{1}", "", menuInvokeJS, menuCss);
                        }
                        else
                        {
                            sMenu = BuildsMenu(path, base.LPref.MenuOption == "T");
                            // "visibility:hidden;" instead of "display:none;" for Vertical Menu:
                            MenuId.Text = string.Format("{2}<ul id='TpMenu' class='TpMenuSub' style='display:none;'>{0}</ul>{1}", string.Join("", sMenu.ToArray()), menuInvokeJS, menuCss);
                        }
                    }
                }
            }
        }

        private List<string> BuildsMenu(string[] path, bool bRecurr)
        {
            string sessionId = string.Empty;
            if (Request.QueryString["ssd"] != null && Request.QueryString["ssd"].ToString() != string.Empty) { sessionId = Request.QueryString["ssd"].ToString(); }
            DataView dvMenu = new DataView(base.VMenu.Table);
            dvMenu.Sort = "ParentQid, Qid";
            dvMenu.RowFilter = string.Format("ParentQid IS NULL");
            List<MenuNode> menu = BuildMenuTree(dvMenu, path, 0, bRecurr);
            int i = 0, cnt = menu.Count;
            List<string> sMenu = new List<string>();
            foreach (MenuNode m in menu)
            {
                sMenu.Add(m.ToUnorderList("TpMenuIcon", "TpMenuLink", "TpMenuGrpTitle", "TpMenuItem", "TpMenuItemSelected", "TpMenuLevel", "TpMenuItemFocus", "TpMenuNode", "TpMenuItemFirst", i == 0, "TpMenuItemLast", i == cnt - 1, "TpMenu", sessionId));
                i = i + 1;
            }
            return sMenu;
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