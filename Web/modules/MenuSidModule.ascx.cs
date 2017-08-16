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

	public partial class MenuSidModule : RO.Web.ModuleBase
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

		public MenuSidModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Request.IsAuthenticated && base.LUser != null && base.LImpr != null && base.LPref != null)
			{
				if (base.VMenu == null)
				{
                    try
                    {
                        base.VMenu = new DataView((new MenuSystem()).GetMenu(base.LUser.CultureId, base.LCurr.SystemId, base.LImpr, base.SysConnectStr(base.LCurr.SystemId), base.AppPwd(base.LCurr.SystemId), null, null, null));
                    }
                    catch { base.VMenu = null; }
				}

                if (base.VMenu != null)
                {
                    /* if there is no id in query string, we should assume there is no current selection rather than the '00' */
                    string[] path = new string[0];
                    try
                    {
                        path = PageBase.ExpandNode.Split(new char[] { '.' });
                    }
                    catch { }

                    string menuCss = string.Format(@"<style type='text/css'>{0}</style>", LPref.SidMenuCss);
                    string menuInvokeJS = string.Format(@"<script language='javascript' type='text/javascript'>{0}{1}{2}</script>", LPref.SidMenuJs, LPref.SidMenuIvk, "(function() { $('#VrMenu').css('display','block').css('visibility','visible');})();");

                    if (string.IsNullOrEmpty(Request.QueryString["id"])) path = new string[0];
                    string selectedParent = path.Length > 0 ? path[0] : null;

                    var x = (from r in base.VMenu.Table.AsEnumerable()
                             where r.Field<string>("Qid") == selectedParent
                             select r.Field<int>("MenuId")).ToArray();

                    int parentMenuId = x.Length > 0 ? x[0] : -1;

                    /* S is the vertical menu only and B is both the top and vertical.  Ignore T which is top menu only. */
                    if (base.LPref.MenuOption == "B" || base.LPref.MenuOption == "S")  // B is both top and vertical menus.
                    {
                        string sessionId = string.Empty;
                        if (Request.QueryString["ssd"] != null && Request.QueryString["ssd"].ToString() != string.Empty) { sessionId = Request.QueryString["ssd"].ToString(); }
                        DataView dvMenu = new DataView(base.VMenu.Table);
                        dvMenu.Sort = "ParentQid, Qid";
                        dvMenu.RowFilter = base.LPref.MenuOption == "S" ? string.Format("ParentQid IS NULL") : string.Format("ParentId = {0}", parentMenuId);
                        List<MenuNode> menu = BuildMenuTree(dvMenu, path, 0, true);
                        int i = 0, cnt = menu.Count;
                        List<string> sMenu = new List<string>();
                        foreach (MenuNode m in menu)
                        {
                            sMenu.Add(m.ToUnorderList("VrMenuIcon", "VrMenuLink", "VrMenuGrpTitle", "VrMenuItem", "VrMenuItemSelected", "VrMenuLevel", "VrMenuItemFocus", "VrMenuNode", "VrMenuItemFirst", i == 0, "VrMenuItemLast", i == cnt - 1, "VrMenu", sessionId));
                            i = i + 1;
                        }
                        // "display:none;" instead of "visibility:hidden;" for Top Menu:
                        MenuId.Text = string.Format("{2}<ul id='VrMenu' class='VrMenuSub' style='visibility:hidden;'>{0}</ul>{1}{2}", string.Join("", sMenu.ToArray()), menuInvokeJS, menuCss);
                    }
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

		}
		#endregion
   }
}