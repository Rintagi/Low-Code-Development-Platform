<%@ WebService Language="C#" Class="AdminWs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;

using System.Xml;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;
using System.Web.Configuration;
using System.Configuration;

// Need to run the following 3 lines to generate AdminWs.cs for C# calls at Windows SDK v6.1: CMD manually if AdminWs.asmx is changed:
// C:\
// CD\Rintagi\RO\Service3
// "C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin\wsdl.exe" /nologo /namespace:RO.Service3 /out:"AdminWs.cs" "http://RND08/ROWs/AdminWs.asmx"

[ScriptService()]
[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class AdminWs : WebService
{
    private const String KEY_SystemsDict = "Cache:SystemsDict";
    private const String KEY_SysConnectStr = "Cache:SysConnectStr";
    private const String KEY_AppConnectStr = "Cache:AppConnectStr";
    private const String KEY_AppPwd = "Cache:AppPwd";

    private const String KEY_CacheLUser = "Cache:LUser";
    private const String KEY_CacheLImpr = "Cache:LImpr";
    private const String KEY_CacheLCurr = "Cache:LCurr";

    public struct MenuResponse
    {
        public string menuId;
        public string label;
        public string status;
        public string errorMsg;
        public string navigateUrl;
    }

    public struct ScreenColResponse
    {
        public string tabId;
        public string columnId;
        public string label;
        public string status;
        public string errorMsg;
    }

    public struct ScreenColListResponse
    {
        public List<Dictionary<string, string>> screenObjList;
        public string status;
        public string errorMsg;
    }

    //For menu:
    [WebMethod(EnableSession = true)]

    public MenuResponse AddMenu(string PMenuId, string ParentId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        int screenId = 121; // Hardcoded AdmMenuDrg, must change if that changed

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        MenuResponse mr = new MenuResponse();

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId, "U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            DataTable dtMenu = (new RO.WebRules.WebRule()).WrAddMenu(PMenuId, ParentId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            mr.menuId = dtMenu.Rows[0][0].ToString();
            mr.label = dtMenu.Rows[0][1].ToString();
            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }


    [WebMethod(EnableSession = true)]
    //public void DelMenuHlp(int menuId, string contextStr)
    public MenuResponse DelMenu(string MenuId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        MenuResponse mr = new MenuResponse();

        int screenId = 121; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            (new RO.WebRules.WebRule()).WrDelMenu(MenuId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public MenuResponse UpdMenu(string MenuId, string PMenuId, string ParentId, string MenuText, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        MenuResponse mr = new MenuResponse();

        int screenId = 121; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            (new RO.WebRules.WebRule()).WrUpdMenu(MenuId, PMenuId, ParentId, MenuText, usr.CultureId.ToString(), dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public MenuResponse GetMenuUrl(string MenuId, byte SysId)
    {
        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        MenuResponse mr = new MenuResponse();

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr LImpr = (UsrImpr)Session[KEY_CacheLImpr];
        try
        {
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[SysId];
            DataTable dtMenuItems = (new MenuSystem()).GetMenu(usr.CultureId, SysId, LImpr, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], 0, 0, 0);
            foreach (DataRow dr in dtMenuItems.Rows)
            {
                if (dr["MenuId"].ToString() == MenuId)
                {
                    mr.navigateUrl = dr["NavigateUrl"].ToString();
                }
            }

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    // For screen tabs:

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrAddScreenTab(string TabFolderOrder, string ScreenId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            DataTable dtTab = (new RO.WebRules.WebRule()).WrAddScreenTab(TabFolderOrder, ScreenId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            mr.tabId = dtTab.Rows[0][0].ToString();
            mr.label = dtTab.Rows[0][1].ToString();
            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrDelScreenTab(string ScreenTabId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }
        try
        {
            (new RO.WebRules.WebRule()).WrDelScreenTab(ScreenTabId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrUpdScreenTab(string ScreenTabId, string TabFolderOrder, string TabFolderName, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }
        try
        {
            (new RO.WebRules.WebRule()).WrUpdScreenTab(ScreenTabId, TabFolderOrder, TabFolderName, usr.CultureId.ToString(), dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    // For screen columns
    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrAddScreenObj(string ScreenId, string PScreenObjId, string TabFolderId, string IsTab, string NewRow, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }
        try
        {

            DataTable dtColumn = (new RO.WebRules.WebRule()).WrAddScreenObj(ScreenId, PScreenObjId, TabFolderId, IsTab == "Y", NewRow == "Y", dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            mr.columnId = dtColumn.Rows[0][0].ToString();
            mr.label = dtColumn.Rows[0][1].ToString();
            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrDelScreenObj(string ScreenObjId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }
        try
        {
            (new RO.WebRules.WebRule()).WrDelScreenObj(ScreenObjId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = ReformatErrMsg(err.Message == "" ? "Cannot delete this column." : err.Message, 3);// this is hard coded for the Screen IDE, must change if it is not the right #
        }
        return mr;
    }

    protected string ReformatErrMsg(string msg, byte SysId)
    {
        LoginUsr LUser = (LoginUsr)Session[KEY_CacheLUser];
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];
        // Reformat:
        if (LUser != null && msg != null)
        {
            byte sid;
            string sm = msg;
            Regex re = new Regex("(\\|[0-9]{1,5}\\|)");
            if (re.IsMatch(msg))
            {
                Match m = re.Match(msg);
                sid = byte.Parse(m.Captures[0].Value.Replace("|", string.Empty));
                sm = re.Replace(sm, string.Empty);
            }
            else { sid = 0; }
            msg = (new AdminSystem()).GetMsg(sm, LUser.CultureId, LUser.TechnicalUsr, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]).Replace("\r\n", "\r").Replace("\r", "</br>");
        }

        if (msg.IndexOf("\n") >= 0)
        {
            int rCount = 0;
            int cCount = 0;
            System.Text.StringBuilder sr = new System.Text.StringBuilder();
            System.Collections.Generic.Dictionary<string, string> Keys = new System.Collections.Generic.Dictionary<string, string>();
            string[] lb = { "\n" };
            foreach (string line in msg.Split(lb, StringSplitOptions.None))
            {
                rCount = rCount + 1;
                cCount = 0;
                string rowCssCls = string.Format("Row{0} {1}", rCount, rCount % 2 == 0 ? "even" : "odd");
                System.Text.StringBuilder sc = new System.Text.StringBuilder();
                foreach (string el in line.Split('|'))
                {
                    cCount = cCount + 1;
                    string colCssCls = string.Format("Col{0} {1}", cCount, cCount % 2 == 0 ? "even" : "odd");
                    if (cCount == 1 && !string.IsNullOrEmpty(el) && !Keys.ContainsKey(el))
                    {
                        Keys[el] = line;
                        rowCssCls = rowCssCls + " RowBreak";
                        sc.Append(string.Format("<td class='{0}'>{1}</td>", colCssCls, el));
                    }
                    else
                    {
                        sc.Append(string.Format("<td class='{0}'>{1}</td>", colCssCls, Keys.ContainsKey(el) ? "" : el));
                    }
                }
                sr.Append(string.Format("<tr class='{0}'>{1}</tr>", rowCssCls, sc.ToString()));
            }
            return "<div class='ErrLstContainer'><table class='ErrLst'>" + sr.ToString() + "</table></div>";
        }
        else if (!string.IsNullOrEmpty(msg))
        {
            return msg;
        }
        else return "Error: no detail message.";
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrUpdScreenObj(string ScreenObjId, string PScreenObjId, string TabFolderId, string ColumnHeader, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();

        int screenId = 1007; // Hardcoded AdmMenuDrg, must change if that changed

        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }
        try
        {
            (new RO.WebRules.WebRule()).WrUpdScreenObj(ScreenObjId, PScreenObjId, TabFolderId, ColumnHeader, usr.CultureId.ToString(), dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColListResponse WrGetScreenObj(string ScreenId, byte SysId)
    {
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColListResponse mr = new ScreenColListResponse();

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        List<Dictionary<string, string>> screenObjList = new List<Dictionary<string, string>>();
        try
        {
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[SysId];

            DataTable dtItms = (new RO.WebRules.WebRule()).WrGetScreenObj(ScreenId, usr.CultureId, null, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);

            foreach (DataRow dr in dtItms.Rows)
            {
                Dictionary<string, string> d = new Dictionary<string, string>{
                    {"ScreenTabId",dr["ScreenTabId"].ToString()}
                    ,{"TabFolderName",dr["TabFolderName"].ToString()}
                    ,{"TabFolderOrder",dr["TabFolderOrder"].ToString()}
                    ,{"ScreenObjId",dr["ScreenObjId"].ToString()}
                    ,{"MasterTable",dr["MasterTable"].ToString()}
                    ,{"NewGroupRow",dr["NewGroupRow"].ToString()}
                    ,{"ScreenTypeName",dr["ScreenTypeName"].ToString()}
                    ,{"ColumnName",dr["ColumnName"].ToString()}
                    ,{"TabIndex",dr["TabIndex"].ToString()}
                    ,{"ColCssClass",dr["ColCssClass"].ToString()}
                    ,{"RowCssClass",dr["RowCssClass"].ToString()}
                    ,{"GridGrpCd",dr["GridGrpCd"].ToString()}
                    ,{"ColumnHeight",dr["ColumnHeight"].ToString()}
                    ,{"ColumnHeader",dr["ColumnHeader"].ToString()}
                    ,{"ProgramName",dr["ProgramName"].ToString()}
                    ,{"RequiredValid",dr["RequiredValid"].ToString()}
                    ,{"DisplayMode",dr["DisplayMode"].ToString()}
                };
                screenObjList.Add(d);
            }

            mr.status = "";
            mr.errorMsg = "";
            mr.screenObjList = screenObjList;
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    private string JsonToXML(string json, string rootName)
    {
        XmlDocument doc = new XmlDocument();

        using (var reader = System.Runtime.Serialization.Json.JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
        {
            System.Xml.Linq.XElement xml = System.Xml.Linq.XElement.Load(reader);
            if (!string.IsNullOrEmpty(rootName)) xml.Name = rootName;
            return xml.ToString();
        }
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrResizeGridLayout(string grdObjId, string grdItemWidth, string grdItemHeight, byte SysId)
    {
        int screenId = 1007; // this is hard coded for the Screen IDE in system 3, must change if it is not the right #
                             //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                             //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        string paramXML = "<Params>"
            + "<ScreenObjId>" + grdObjId.ToString() + "</ScreenObjId>"
            + "<ColumnWidth>" + grdItemWidth.ToString() + "</ColumnWidth>"
            + "<ColumnHeight>" + grdItemHeight.ToString() + "</ColumnHeight>"
            + "</Params>";

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            (new AdminSystem()).RunWrRule(screenId,"WrUpdScreenGridObjSize", dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], paramXML, impr, uc);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrUpdTabFolderLayout(string ScreenObjList, byte SysId)
    {
        int screenId = 1007; // this is hard coded for the Screen IDE in system 3, must change if it is not the right #
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, string>> screenObjs = jss.Deserialize<List<Dictionary<string, string>>>("[" + ScreenObjList.Substring(1) + "]");
        string screenObjListXML = "<ScreenObjs><ScreenObj>"
            + string.Join("</ScreenObj><ScreenObj>", (from x in screenObjs select "<id>" + x["ID"] + "</id>" + "<tabId>" + x["TABID"] + "</tabId>" + "<r>" + x["ROW"] + "</r>" + "<c>" + x["COL"] + "</c>" + "<g>" + x["NEWROWGRP"] + "</g>" + "<h>" + x["COLHEIGHT"] + "</h>").ToArray())
            + "</ScreenObj></ScreenObjs>";

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];
        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3);

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            (new AdminSystem()).RunWrRule(screenId,"WrUpdScreenObjLayoutAndSize", dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], screenObjListXML, impr, uc);
            mr.status = "";
            mr.errorMsg = "" ;
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    public ScreenColResponse WrUpdGridLayout(string grdObjId, string gridGrpCd, string prevGrdObjId, string nextGrdObjId, string nextGridGrpCd, string oldSideItmId, string oldSideItmCd, string SysId)
    {
        int screenId = 1007; // this is hard coded for the Screen IDE in system 3, must change if it is not the right #
        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);
        Dictionary<string, string> param = new Dictionary<string, string>();
        param["ScreenObjId"] = grdObjId.ToString();
        param["GridGrpCd"] = gridGrpCd.ToString();
        param["PriorScreenObjId"] = prevGrdObjId.ToString();
        param["NextScreenObjId"] = nextGridGrpCd.ToString();
        param["NextGridGrpCd"] = nextGridGrpCd.ToString();
        string xxJSON = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(param);
        string xxXML = JsonToXML(xxJSON,"Params");

        string paramXML = "<Params>"
            + "<ScreenObjId>" + grdObjId.ToString() + "</ScreenObjId>"
            + "<GridGrpCd>" + gridGrpCd.ToString() + "</GridGrpCd>"
            + "<PriorScreenObjId>" + prevGrdObjId.ToString() + "</PriorScreenObjId>"
            + "<NextScreenObjId>" + nextGrdObjId.ToString() + "</NextScreenObjId>"
            + "<NextGridGrpCd>" + nextGridGrpCd.ToString() + "</NextGridGrpCd>"
            + "<OldSideItmId>" + oldSideItmId.ToString() + "</OldSideItmId>"
            + "<OldSideItmCd>" + oldSideItmCd.ToString() + "</OldSideItmCd>"
            + "</Params>";

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        ScreenColResponse mr = new ScreenColResponse();
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[byte.Parse(SysId)];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId,"U", 3); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            mr.status = "failed";
            mr.errorMsg = accessCheck.Value;
            return mr;
        }

        try
        {
            (new AdminSystem()).RunWrRule(screenId,"WrUpdScreenGridObjLayout", dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], paramXML, impr, uc);

            mr.status = "";
            mr.errorMsg = "";
        }
        catch (Exception err)
        {
            mr.status = "failed";
            mr.errorMsg = err.Message;
        }
        return mr;
    }

    [WebMethod(EnableSession = true)]
    private string RunWrRule(int screenId, string SPName, string jsonParam, string SysId, bool isSys)
    {
        string paramXML = JsonToXML(jsonParam, "Params");

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[byte.Parse(SysId)];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];
        KeyValuePair<bool, string> accessCheck = HaveProperAccess(screenId, "R", byte.Parse(SysId)); // this is hard coded for the Screen IDE, must change if it is not the right #

        if (accessCheck.Key == false)
        {
            return "{status:'failed',errorMsg:'" + accessCheck.Value.Replace("'", "\\'") + "',result:[]}";
        }

        try
        {
            DataTable dt = (new AdminSystem()).RunWrRule(screenId, SPName, dSys[isSys ? KEY_SysConnectStr : KEY_AppConnectStr], dSys[KEY_AppPwd], paramXML, impr, uc);
            var lst = dt.AsEnumerable()
                .Select(r =>
                    r.Table.Columns.Cast<DataColumn>()
                    .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal]))
                    .ToDictionary(z => z.Key, z => z.Value)
                )
                .ToList();
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var content = serializer.Serialize(lst);
            return "{status:'failed',errorMsg:'',result:" + content + "}";

        }
        catch (Exception err)
        {
            return "{status:'',errorMsg:'" + err.Message.Replace("'", "\\'") + "',result:[]}";
        }
    }

    private KeyValuePair<bool, string> HaveProperAccess(int screenId, string action, byte SysId)
    {
        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
        Dictionary<string, string> dSys = dSysList[SysId];

        LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr impr = (UsrImpr)Session[KEY_CacheLImpr];

        DataTable dtMenuAccess = (new MenuSystem()).GetMenu(usr.CultureId, SysId, impr, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], screenId, null, null);
        string accessDeniedMsg = (new AdminSystem()).GetLabel(usr.CultureId, "cSystem", "AccessDeny", null, null, null);

        if (dtMenuAccess.Rows.Count == 0) return new KeyValuePair<bool, string>(false, accessDeniedMsg);

        if (
            (Config.DeployType == "PRD" || (Config.AppNameSpace != "RO" && SysId == 3))
            && (screenId == 1007 && action == "U") // screen objection ide cannot be used for update on production system or admin screens when the name space is not RO
            ) new KeyValuePair<bool, string>(false, accessDeniedMsg);

        DataTable dtRowAuth = (new AdminSystem()).GetAuthRow(screenId, impr.RowAuthoritys, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
        DataRow dr = dtRowAuth.Rows[0];

        if ((dr["AllowUpd"].ToString() == "N" && action=="U")) return new KeyValuePair<bool, string>(false, accessDeniedMsg);

        return new KeyValuePair<bool, string>(true, "");
    }

    // For screens:

    [WebMethod]
    public string GetLastPageInfo(Int32 screenId, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLastPageInfo(screenId, usrId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void UpdLastPageInfo(Int32 screenId, Int32 usrId, string lastPageInfo, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdLastPageInfo(screenId, usrId, lastPageInfo, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLastCriteria(Int32 rowsExpected, Int32 screenId, Int32 reportId, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLastCriteria(rowsExpected, screenId, reportId, usrId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void DelDshFldDtl(string DshFldDtlId, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).DelDshFldDtl(DshFldDtlId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public void DelDshFld(string DshFldId, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).DelDshFld(DshFldId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string UpdDshFld(string PublicAccess, string DshFldId, string DshFldName, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).UpdDshFld(PublicAccess, DshFldId, DshFldName, usrId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetSchemaScrImp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetSchemaScrImp(screenId, cultureId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetButtonHlp(Int32 screenId, Int32 reportId, Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetButtonHlp(screenId, reportId, wizardId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetClientRule(Int32 screenId, Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetClientRule(screenId, reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenHlp(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetGlobalFilter(Int32 usrId, Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetGlobalFilter(usrId, screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenFilter(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenFilter(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenTab(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenTab(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenCriHlp(Int32 screenId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenCriHlp(screenId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void LogUsage(Int32 UsrId, string UsageNote, string EntityTitle, Int32 ScreenId, Int32 ReportId, Int32 WizardId, string Miscellaneous, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).LogUsage(UsrId, UsageNote, EntityTitle, ScreenId, ReportId, WizardId, Miscellaneous, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetInfoByCol(Int32 ScreenId, string ColumnName, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetInfoByCol(ScreenId, ColumnName, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public bool IsValidOvride(string cr, Int32 usrId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).IsValidOvride(cr.XmlToObject<Credential>(), usrId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetMsg(string Msg, Int16 CultureId, string TechnicalUsr, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetMsg(Msg, CultureId, TechnicalUsr, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLabel(Int16 CultureId, string LabelCat, string LabelKey, string CompanyId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLabel(CultureId, LabelCat, LabelKey, CompanyId, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetLabels(Int16 CultureId, string LabelCat, string CompanyId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetLabels(CultureId, LabelCat, CompanyId, dbConnectionString, dbPassword).DataTableToXml();
    }

    //[WebMethod]
    //public string GetFormat()
    //{
    //    //return (new AdminSystem()).GetFormat().DataTableToXml();
    //}

    [WebMethod]
    public string GetScrCriteria(string screenId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScrCriteria(screenId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void MkGetScreenIn(string screenId, string screenCriId, string procedureName, string appDatabase, string sysDatabase, string multiDesignDb, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).MkGetScreenIn(screenId, screenCriId, procedureName, appDatabase, sysDatabase, multiDesignDb, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetScreenIn(string screenId, string procedureName, string RequiredValid, int topN, string FilterTxt, string ui, string uc, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenIn(screenId, procedureName, RequiredValid, topN, FilterTxt, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public void UpdScrCriteria(string screenId, string programName, string dvCri, Int32 usrId, bool isCriVisible, string ds, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdScrCriteria(screenId, programName, XmlUtils.XmlToDataView(dvCri), usrId, isCriVisible, XmlUtils.XmlToDataSet(ds), dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string EncryptString(string inStr)
    {
        return (new AdminSystem()).EncryptString(inStr);
    }

    [WebMethod]
    public string GetAuthRow(Int32 ScreenId, string RowAuthoritys, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetAuthRow(ScreenId, RowAuthoritys, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetAuthCol(Int32 ScreenId, string ui, string uc, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetAuthCol(ScreenId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetAuthExp(Int32 ScreenId, Int16 CultureId, string ui, string uc, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetAuthExp(ScreenId, CultureId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetScreenLabel(Int32 ScreenId, Int16 CultureId, string ui, string uc, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetScreenLabel(ScreenId, CultureId, ui.XmlToObject<UsrImpr>(), uc.XmlToObject<UsrCurr>(), dbConnectionString, dbPassword).DataTableToXml();
    }

    // For reports:

    [WebMethod]
    public string GetPrinterList(string UsrGroups, string Members)
    {
        return (new AdminSystem()).GetPrinterList(UsrGroups, Members).DataTableToXml();
    }

    [WebMethod]
    public void UpdLastCriteria(Int32 screenId, Int32 reportId, Int32 usrId, Int32 criId, string lastCriteria, string dbConnectionString, string dbPassword)
    {
        (new AdminSystem()).UpdLastCriteria(screenId, reportId, usrId, criId, lastCriteria, dbConnectionString, dbPassword);
    }

    [WebMethod]
    public string GetReportHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportHlp(reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetReportCriHlp(Int32 reportId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportCriHlp(reportId, cultureId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetReportSct()
    {
        return (new AdminSystem()).GetReportSct().DataTableToXml();
    }

    [WebMethod]
    public string GetReportItem(Int32 ReportId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetReportItem(ReportId, dbConnectionString, dbPassword).DataTableToXml();
    }

    [WebMethod]
    public string GetRptPwd(string pwd)
    {
        return (new AdminSystem()).GetRptPwd(pwd);
    }

    // For Wizards:

    [WebMethod]
    public string GetSchemaWizImp(Int32 wizardId, Int16 cultureId, string dbConnectionString, string dbPassword)
    {
        return (new AdminSystem()).GetSchemaWizImp(wizardId, cultureId, dbConnectionString, dbPassword);
    }

    private void SetSessionTimezoneInfo(string tzBaseOffset, string tzOffset, string hasDST)
    {
        TimeSpan baseOffset = new TimeSpan(0, 0 - int.Parse(tzBaseOffset??"0"), 0);
        bool bSupportDST = int.Parse(hasDST ?? "0") == 1;
        TimeZoneInfo foundTimeZone = null;
        TimeZoneInfo matchTimeZone = null;
        foreach (TimeZoneInfo tzInfo in TimeZoneInfo.GetSystemTimeZones())
        {
            if (tzInfo.BaseUtcOffset == baseOffset)
            {
                if (bSupportDST && tzInfo.SupportsDaylightSavingTime)
                {
                    if (matchTimeZone == null) matchTimeZone = tzInfo;
                    if (tzInfo.DisplayName.Contains("Canada") || tzInfo.DisplayName.Contains("Tokyo") || tzInfo.DisplayName.Contains("Beijing"))
                    {
                        foundTimeZone = tzInfo;
                        break;
                    }
                }
                else if (!bSupportDST && !tzInfo.SupportsDaylightSavingTime)
                {
                    if (matchTimeZone == null) matchTimeZone = tzInfo;
                    if (tzInfo.DisplayName.Contains("Canada") || tzInfo.DisplayName.Contains("Tokyo") || tzInfo.DisplayName.Contains("Beijing"))
                    {
                        foundTimeZone = tzInfo;
                        break;
                    }

                }
            }
        }
        if (foundTimeZone == null) foundTimeZone = matchTimeZone;
        if (foundTimeZone == null) foundTimeZone = TimeZoneInfo.Local;
        if (Session["Cache:tzUserSelect"] == null) Session["tzInfo"] = foundTimeZone;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public List<string> HasWIP()
    {
        string dbConnectionString = Config.GetConnStr(Config.DesProvider,Config.DesServer,Config.DesDatabase,"",Config.DesUserId);
        string dbPassword = Config.DesPassword;
        return new AdminSystem().HasOutstandReleaseContent(Config.AppNameSpace, dbConnectionString, dbPassword);
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public Dictionary<string, List<string>> HasRegenWaiting()
    {
        string dbConnectionString = Config.GetConnStr(Config.DesProvider, Config.DesServer, Config.DesDatabase, "", Config.DesUserId);
        string dbPassword = Config.DesPassword;
        return new AdminSystem().HasOutstandRegen(Config.AppNameSpace, dbConnectionString, dbPassword);
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public string GetDesignVersion()
    {
        string dbConnectionString = Config.GetConnStr(Config.DesProvider, Config.DesServer, Config.DesDatabase, "", Config.DesUserId);
        string dbPassword = Config.DesPassword;
        return new AdminSystem().GetDesignVersion(Config.AppNameSpace, dbConnectionString, dbPassword);
    }
    // receive browser capability(like timezone and geolocation etc.)
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json,XmlSerializeString=false,UseHttpGet=false)]
    public string BrowserCap(string contextStr)
    {
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        if (Session != null)
        {
            if (context.ContainsKey("tzOffset")) Session["Cache:tzOffset"] = context["tzOffset"]; // minutes behind
            if (context.ContainsKey("tzBaseOffset")) Session["Cache:tzBaseOffset"] = context["tzBaseOffset"]; // minutes behind
            if (context.ContainsKey("tzHasDST")) Session["Cache:tzHasDST"] = context["tzHasDST"]; // 0 or 1
            if (context.ContainsKey("tzDST")) Session["Cache:tzDST"] = context["tzDST"]; // 0 or 1 
            if (context.ContainsKey("myLat")) Session["Cache:myLat"] = context["myLat"];
            if (context.ContainsKey("myLng")) Session["Cache:myLng"] = context["myLng"];
            if (context.ContainsKey("myCity")) Session["Cache:City"] = context["myCity"];
            if (context.ContainsKey("myState")) Session["Cache:State"] = context["myState"];
            if (context.ContainsKey("myCountry")) Session["Cache:Country"] = context["myCountry"];
            if (context.ContainsKey("myPostal")) Session["Cache:PostalCode"] = context["myPostal"];
            if (context.ContainsKey("tzFullDateTime")) Session["Cache:tzFullDateTime"] = context["tzFullDateTime"];
            SetSessionTimezoneInfo(Session["Cache:tzBaseOffset"] as string, Session["Cache:tzOffset"] as string, Session["Cache:tzHasDST"] as string);
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (KeyValuePair<string, string> kv in context)
        {
            sb.Append(kv.Key); sb.Append(":"); sb.Append(kv.Value);
        }
        return sb.ToString();
    }

    [WebMethod(EnableSession = true)]
    public string GetSupportInfo(string Typ)
    {
        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        UsrCurr LCurr = (UsrCurr)(Session[KEY_CacheLCurr]);
        DataTable dtSys = (new LoginSystem()).GetSystemsList(string.Empty, string.Empty);
        string systemName = "Administration";
        string adminEmail = "cs@robocoder.com";
        string csEmail = "cs@robocoder.com";
        foreach (DataRow dr in dtSys.Rows)
        {
            if (dr["SystemId"].ToString() == LCurr.SystemId.ToString())
            {
                systemName = dr["SystemName"].ToString();
                adminEmail = dr["AdminEmail"].ToString();
                csEmail = dr["CustServEmail"].ToString();
                break;
            }
        }

        if (Typ == "C") { return csEmail; } else { return adminEmail; }
    }

    [WebMethod]
    public string[] CreateInstaller(string iType, DateTime rebuildTime)
    {
        //if (!HttpContext.Current.Request.IsLocal) return new string[]{"","access denied"};

        string dbConnectionString = Config.GetConnStr(Config.DesProvider,Config.DesServer,Config.DesDatabase,"",Config.DesUserId);
        string dbPassword = Config.DesPassword;

        if (this.Application["CreateInstaller"] != null) { return new string[]{"","deployment in progress since " + this.Application["CreateInstaller"].ToString()}; }

        try
        {
            this.Application["CreateInstaller"] = DateTime.Now;

            DataTable dtScrCri = (new AdminSystem()).GetScrCriteria("98", dbConnectionString, dbPassword);
            DataTable dtScreenIn = new DataTable("DtScreenIn");
            foreach (DataRowView drv in dtScrCri.DefaultView)
            {
                if (drv["DataTypeSysName"].ToString() == "DateTime") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
                else if (drv["DataTypeSysName"].ToString() == "Int16") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
                else if (drv["DataTypeSysName"].ToString() == "Int32") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
                else if (drv["DataTypeSysName"].ToString() == "Int64") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
                else if (drv["DataTypeSysName"].ToString() == "Single") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
                else if (drv["DataTypeSysName"].ToString() == "Double") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte[]") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
                else if (drv["DataTypeSysName"].ToString() == "Object") { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
                else { dtScreenIn.Columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
            }
            DataSet dsCri = new DataSet();
            dsCri.Tables.Add(dtScreenIn);
            DataRow dr = dsCri.Tables["DtScreenIn"].NewRow();
            dsCri.Tables["DtScreenIn"].Rows.Add(dr);
            foreach (DataRowView drv in dtScrCri.DefaultView)
            {
                if (drv["ColumnName"].ToString().StartsWith("EntityId"))
                {
                    dr[drv["ColumnName"].ToString()] = iType.Contains("PTY") ? 2 : 3; // 2 is prototype, 3 is production
                }
            }
            UsrImpr impr = new UsrImpr("1", "5", "1", "1", "", "", "", "", "", "", "", "", "", "", "");
            UsrCurr curr = new UsrCurr();
            DataTable dtRel = (new AdminSystem()).GetLis(98, "GetLisAdmRelease98", false, "N", 0, null, null, 0, string.Empty, string.Empty, dtScrCri.DefaultView, impr, curr, dsCri);

            Deploy dp = new Deploy();
            CurrSrc src = new CurrSrc(true, null);
            CurrTar tar = new CurrTar(true, null);
            DataTable dtEntities = (new RobotSystem()).GetEntityList();
            dtEntities.DefaultView.RowFilter = "EntityId = " + (iType.Contains("PTY") ? 2 : 3).ToString() + " ";
            CurrPrj prj = new CurrPrj(dtEntities.DefaultView[0].Row);

            foreach (DataRowView drv in dtRel.DefaultView)
            {
                if (drv["ReleaseId191Text"].ToString().Contains("["+iType+"]"))
                {
                    if (!File.Exists(prj.DeployPath + "\\bin\\release\\Install.exe")
                        || new FileInfo(prj.DeployPath + "\\bin\\release\\Install.exe").LastWriteTime < rebuildTime)
                    {
                        try
                        {
                            // remove existing resource directories
                            DirectoryInfo diRoot = new DirectoryInfo(prj.DeployPath);
                            foreach (DirectoryInfo di in diRoot.GetDirectories())
                            {
                                if (di.Name.EndsWith((iType.StartsWith("N") ? iType.Substring(1) : iType) + "_" + Config.AppNameSpace))
                                {
                                    try { di.Delete(true); }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                        string sbWarnMsg = dp.PrepInstall((short)drv["ReleaseId191"], src, tar, dbConnectionString, dbPassword);
                        EmbedRsc(prj.DeployPath);
                    }
                    string cmd_path = "\"" + Config.BuildExe + "\"";
                    string cmd_arg = "\"" + prj.DeployPath + "Install.sln\" /p:Configuration=Release /t:Rebuild /v:minimal /nologo";
                    string sCompileMsg = Utils.WinProc(cmd_path, cmd_arg, true);
                    if (sCompileMsg.IndexOf("failed") >= 0 || sCompileMsg.Replace("errorreport", string.Empty).Replace("warnaserror", string.Empty).IndexOf("error") >= 0)
                        return new string[]{"",
                                            sCompileMsg};
                    else
                        return new string[]{prj.DeployPath + "\\bin\\release\\Install.exe",
                                            sCompileMsg};
                }
            }
        }
        catch (Exception e)
        {
            return new string[]{"",e.Message + Environment.NewLine + e.StackTrace};
        }
        finally
        {
            this.Application.Remove("CreateInstaller");
        }
        return new string[]{"",""};
    }

    [WebMethod]
    public void UpdateLicense(string license, string hash)
    {
        Tuple<string,bool,string> _license = (new RO.Access3.AdminAccess()).UpdateLicense(license, hash);
        if (_license.Item2)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            if (config.AppSettings.Settings["RintagiLicense"] != null) config.AppSettings.Settings["RintagiLicense"].Value = license;
            else config.AppSettings.Settings.Add("RintagiLicense", license);
            config.Save(ConfigurationSaveMode.Modified);
        }
    }

    [WebMethod]
    public Dictionary<string, string> GetLicenseDetail(string installID, string appID, string moduleName)
    {
        string SysId = System.Configuration.ConfigurationManager.AppSettings["LicenseModule"] ?? "3";
        KeyValuePair<string, string> conn = (from dr in ((new LoginSystem()).GetSystemsList(string.Empty, string.Empty)).AsEnumerable()
                                             where dr["SystemId"].ToString() == SysId
                                             select new KeyValuePair<string, string>(Config.GetConnStr(dr["dbAppProvider"].ToString(), dr["ServerName"].ToString(), dr["dbAppDatabase"].ToString(), "", dr["dbAppUserId"].ToString()), dr["dbAppPassword"].ToString())).First();
        UsrImpr impr = new UsrImpr("1", "11", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        Dictionary<string, string> license = new Dictionary<string, string>();
        string installDtl = "<Params>"
                + "<installID>" + installID + "</installID>"
                + "<appID>" + appID + "</appID>"
                + "<moduleName>" + moduleName + "</moduleName>"
                + "</Params>";
        DataTable dt = new AdminSystem().RunWrRule(1234, "WrGetLicenseDetail", conn.Key, conn.Value, installDtl, impr, new UsrCurr());
        List<Dictionary<string, string>> result =
            (from dr in dt.AsEnumerable()
             select new Dictionary<string, string>
            {
                { "InstallID", dr["InstallID"].ToString()},
                { "AppID", dr["AppID"].ToString()},
                { "Expiry", ((DateTime)dr["Expiry"]).ToLocalTime().ToUniversalTime().ToString("O")},
                { "PermLicense", dr["PermLicense"].ToString()},
                { "Modules",dr["Modules"].ToString()}
            }).ToList();
        string xxJSON = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result);
        string signerFileName = System.Configuration.ConfigurationManager.AppSettings["LicenseSignerPath"];
        string sig = RO.Common3.Utils.SignData(UTF8Encoding.UTF8.GetBytes(xxJSON), signerFileName);
        license.Add("License", xxJSON);
        license.Add("LicenseSig", sig);

        return license;
    }

    [WebMethod]
    public string GetLicense(string installID, string appID, string moduleName)
    {
        string SysId = System.Configuration.ConfigurationManager.AppSettings["LicenseModule"] ?? "3";
        bool singleSQLCredential = (System.Configuration.ConfigurationManager.AppSettings["DesShareCred"] ?? "N") == "Y";
        KeyValuePair<string, string> conn = (from dr in ((new LoginSystem()).GetSystemsList(string.Empty, string.Empty)).AsEnumerable()
                                             where dr["SystemId"].ToString() == SysId
                                             select new KeyValuePair<string, string>(Config.GetConnStr(dr["dbAppProvider"].ToString(), singleSQLCredential ? Config.DesServer : dr["ServerName"].ToString(), dr["dbAppDatabase"].ToString(), "",singleSQLCredential ? Config.DesUserId : dr["dbAppUserId"].ToString()), singleSQLCredential ? Config.DesPassword : dr["dbAppPassword"].ToString())).First();
        UsrImpr impr = new UsrImpr("1", "11", "1", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
        Dictionary<string, string> license = new Dictionary<string, string>();
        string installDtl = "<Params>"
                + "<installID>" + installID + "</installID>"
                + "<appID>" + appID + "</appID>"
                + "<moduleName>" + moduleName + "</moduleName>"
                + "</Params>";
        DataTable dt = new AdminSystem().RunWrRule(1234, "WrGetLicenseDetail", conn.Key, conn.Value, installDtl, impr, new UsrCurr());
        List<Dictionary<string, string>> result =
            (from dr in dt.AsEnumerable()
             select new Dictionary<string, string>
            {
                { "InstallID", dr["InstallID"].ToString()},
                { "AppID", dr["AppID"].ToString()},
                { "CompanyCount", dr["CompanyCount"].ToString()},
                { "ProjectCount", dr["ProjectCount"].ToString()},
                { "UserCount", dr["UserCount"].ToString()},
                { "ModuleCount", dr["ModuleCount"].ToString()},
                { "ModuleName", dr["ModuleName"].ToString()},
                { "Include", dr["Include"].ToString()},
                { "Exclude", dr["Exclude"].ToString()},
                { "Expiry", ((DateTime)dr["Expiry"]).ToLocalTime().ToUniversalTime().ToString("O")},
                { "PerInstance", dr["PerInstance"].ToString()},
            }).ToList();
        bool perInstance = result.Where(d => d["ModuleName"] == "Design").Select(d => d["PerInstance"] == "Y").Count() > 0;
        Dictionary<string, Dictionary<string, string>> ret = result.ToDictionary(m => m["ModuleName"], m => m);
        string xxJSON = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ret);
        string signerFileName = System.Configuration.ConfigurationManager.AppSettings["LicenseSignerPath"];
        Tuple<string, string, string> encodedLicense = RO.Common3.Utils.EncodeLicenseString(xxJSON, installID, appID, perInstance, true, signerFileName);
        return Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new Dictionary<string, string>()
        {
            { "License",encodedLicense.Item1},
            { "LicenseSig",encodedLicense.Item2},
            { "Encrypted",encodedLicense.Item3},
            { "PerInstance",perInstance ? "Y" : "N"},
        })));
    }

    [WebMethod]
    public string GetInstallDetail()
    {
        var installDetail = RO.Common3.Utils.GetInstallDetail();
        var _i = new Dictionary<string, string>
        {
            {"InstallID", installDetail.Item1 },
            {"AppID", installDetail.Item2 },
        };
        return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(_i);
    }
    private void EmbedRsc(string DeployPath)
    {
        ArrayList ToDelete;
        XmlNode xn;
        XmlNodeList xl;
        string InstallProj = "Install.csproj";
        XmlDocument xd = new XmlDocument();
        xd.Load(DeployPath + InstallProj);

        // Step 1: Remove all existing EmbeddedResource and None:
        ToDelete = new ArrayList();
        xl = xd.GetElementsByTagName("EmbeddedResource");
        foreach (XmlNode node in xl)
        {
            if (node.Attributes != null)
            {
                string attr = node.Attributes[0].Value;
                if (attr.Contains(".zip") || attr.Contains(".bat") || attr.Contains(".sql")) { ToDelete.Add(node); }
            }
        }
        xl = xd.GetElementsByTagName("None");
        foreach (XmlNode node in xl)
        {
            if (node.Attributes != null)
            {
                string attr = node.Attributes[0].Value;
                if (attr.Contains(".zip") || attr.Contains(".bat") || attr.Contains(".sql")) { ToDelete.Add(node); }
            }
        }
        for (int ii = 0; ii < ToDelete.Count; ii++)
        {
            xn = (XmlNode)ToDelete[ii];
            xn.ParentNode.RemoveChild(xn);
        }

        // Step 2: Remove all empty ItemGroup:
        ToDelete = new ArrayList();
        xl = xd.GetElementsByTagName("ItemGroup");
        foreach (XmlNode node in xl)
        {
            if (!node.HasChildNodes) { ToDelete.Add(node); }
        }
        for (int ii = 0; ii < ToDelete.Count; ii++)
        {
            xn = (XmlNode)ToDelete[ii];
            xn.ParentNode.RemoveChild(xn);
        }

        //Step 3: Embedded ncessary resources:
        xn = xd.DocumentElement;
        XmlNode NewItemNode = xd.CreateNode(XmlNodeType.Element, "ItemGroup", string.Empty);
        DirectoryInfo di = new DirectoryInfo(DeployPath);
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            if (dir.Name != "bin" && dir.Name != "obj")
            {
                SearchDirX("*.bat", dir, NewItemNode, xd, DeployPath);
                SearchDirX("*.sql", dir, NewItemNode, xd, DeployPath);
                SearchDirX("*.zip", dir, NewItemNode, xd, DeployPath);
                // Two directories deep for now:
                foreach (DirectoryInfo dir1 in dir.GetDirectories())
                {
                    SearchDirX("*.bat", dir1, NewItemNode, xd, DeployPath);
                    SearchDirX("*.sql", dir1, NewItemNode, xd, DeployPath);
                    SearchDirX("*.zip", dir1, NewItemNode, xd, DeployPath);
                    foreach (DirectoryInfo dir2 in dir1.GetDirectories())
                    {
                        SearchDirX("*.bat", dir2, NewItemNode, xd, DeployPath);
                        SearchDirX("*.sql", dir2, NewItemNode, xd, DeployPath);
                        SearchDirX("*.zip", dir2, NewItemNode, xd, DeployPath);
                    }
                }
            }
        }
        xn.AppendChild(NewItemNode);
        xd.Save(DeployPath + InstallProj);

        //for some reason .net leaves  xmlns="" which VS.NET doesnt like so we need to remove it
        StreamReader sr = new StreamReader(DeployPath + InstallProj);
        StringBuilder csproj = new StringBuilder();
        string line;
        while ((line = sr.ReadLine()) != null) { csproj.AppendLine(line); }
        sr.Close();
        StreamWriter sw = new StreamWriter(DeployPath + InstallProj, false);
        sw.Write(csproj.ToString().Replace(" xmlns=\"\"", ""));
        sw.Close();
    }

    // Called by WrEmbedRsc: Finds files based on the pattern and creates a node for them.
    private void SearchDirX(string Pattern, DirectoryInfo SearchDir, XmlNode ItemNode, XmlDocument xd, string DeployPath)
    {
        XmlNode NewNode;
        XmlAttribute xa;
        foreach (FileInfo fi in SearchDir.GetFiles(Pattern))
        {
            NewNode = xd.CreateNode(XmlNodeType.Element, "EmbeddedResource", null);
            xa = xd.CreateAttribute("Include");
            xa.Value = fi.FullName.Replace(DeployPath, "");
            NewNode.Attributes.Append(xa);
            ItemNode.AppendChild(NewNode);
        }
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, string> GetCurrentUsrInfo(string Scope)
    {
        HttpContext Context = HttpContext.Current;
        HttpSessionState Session = HttpContext.Current.Session;
        LoginUsr User = (LoginUsr)Session[KEY_CacheLUser];
        UsrCurr Curr = (UsrCurr)Session[KEY_CacheLCurr];
        UsrImpr Impr = (UsrImpr)Session[KEY_CacheLImpr];
        Dictionary<string, string> usrInfo = new Dictionary<string, string>();
        usrInfo["UsrId"] = User == null ? "1" : User.UsrId.ToString();
        usrInfo["UsrGroup"] = Impr == null ? "" : Impr.UsrGroups;
        return usrInfo;
    }
}