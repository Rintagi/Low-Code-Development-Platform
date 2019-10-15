using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Web.Services;
using System.Web.Script.Services;
using RO.Facade3;
using RO.Common3.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RO.Web
{


    public partial class AutoComplete : Page
    {
        public struct AutoCompleteResponse
        {
            public string query;
            public List<Dictionary<string, string>> data;
            public int total;
            public int topN;
        }

        public struct ChartResponse
        {
            public List<List<string>> data;
            public string xLabel;
            public string yLabel;
            public string rptTitle;
        }

        // this needs to be keep in sync with Module Base 

        /* and loginmodule as ssd is introduced to handle multiple tab situation which affects
         * LCurr
         * 2011.7.14 gary
         */

        private const String KEY_SystemsDict = "Cache:SystemsDict";
        private const String KEY_SysConnectStr = "Cache:SysConnectStr";
        private const String KEY_AppConnectStr = "Cache:AppConnectStr";
        private const String KEY_AppPwd = "Cache:AppPwd";

        private const String KEY_CacheLUser = "Cache:LUser";
        private const String KEY_CacheLImpr = "Cache:LImpr";
        private const String KEY_CacheLCurr = "Cache:LCurr";

        static private UsrCurr GetUsrCurr(byte csy, string ssd)
        {
            /* this function mimic LoginModule equivalent to provide the proper LCurr context
             * based on ssd= in the query string
             */
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;

            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            UsrCurr uc = (UsrCurr)Session[KEY_CacheLCurr];

            if (usr != null && !string.IsNullOrEmpty(ssd))
            {
                Dictionary<string, string> CmpPrj = (Dictionary<string, string>)Session["CmpPrj" + ssd];
                if (uc != null)
                {
                    uc = new UsrCurr(Int32.Parse(CmpPrj["cmp"]), Int32.Parse(CmpPrj["prj"]), csy, uc.DbId);
                }
                else
                {
                    uc = new UsrCurr(Int32.Parse(CmpPrj["cmp"]), Int32.Parse(CmpPrj["prj"]), csy, csy);
                }
            }
            return uc;
        }

        static private DataTable GetLastScrCriteria(string ssd, int systemId, int screenId, DataView dvCri)
        {
            // this is a system wide format and must be kept in sync with robot if there is any change of it
            string criKey = string.Format("Cache:dtScrCri{0}_{1}",systemId,screenId);
            HttpSessionState Session = HttpContext.Current.Session;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[byte.Parse(systemId.ToString())];
            UsrCurr uc = GetUsrCurr(byte.Parse(systemId.ToString()), ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, screenId, 0, usr.UsrId ,dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            return dt;
        }

      
        static private DataSet MakeScrCriteria(DataView dvCri, DataTable dtLastScrCri)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn"), dvCri));
            int ii = 1;
            DataRow dr = ds.Tables["DtScreenIn"].NewRow();
            foreach (DataRowView drv in dvCri)
            {
                object val = dtLastScrCri.Rows[ii]["LastCriteria"];
                dr[drv["ColumnName"].ToString()] = val; 
                ii = ii + 1;
            }
            ds.Tables["DtScreenIn"].Rows.Add(dr);
            return ds;
        }

        static private DataSet MakeRptCriteria(DataView dvCri, Dictionary<string,string> cri)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(MakeColumns(new DataTable("DtSqlReportIn"),dvCri));
            DataRow dr = ds.Tables["DtSqlReportIn"].NewRow();
            int ii = 0;
            foreach (DataRowView drv in dvCri)
            {
                try
                {
                    dr[drv["ColumnName"].ToString()] = cri[ii.ToString()];
                }
                catch { }
                ii = ii + 1;
            }
            ds.Tables["DtSqlReportIn"].Rows.Add(dr);
            return ds;
        }

        static private DataSet MakeCriteria(DataView dvCri, string searchText)
        {
            // construct a search text criteria
            // the criteria definition can only have ONE(1) criteria which is a text box type
            DataSet ds = new DataSet();
            ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn"), dvCri));
            DataRow dr = ds.Tables["DtScreenIn"].NewRow();
            if (searchText != string.Empty && dvCri.Count > 0) { dr[dvCri[0]["ColumnName"].ToString()] = searchText; }
            ds.Tables["DtScreenIn"].Rows.Add(dr);
            return ds;
        }

        static private DataTable MakeColumns(DataTable dt, DataView dvCri)
        {
            DataColumnCollection columns = dt.Columns;
            foreach (DataRowView drv in dvCri)
            {
                if (drv["DataTypeSysName"].ToString() == "DateTime") { columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
                else if (drv["DataTypeSysName"].ToString() == "Int16") { columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
                else if (drv["DataTypeSysName"].ToString() == "Int32") { columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
                else if (drv["DataTypeSysName"].ToString() == "Int64") { columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
                else if (drv["DataTypeSysName"].ToString() == "Single") { columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
                else if (drv["DataTypeSysName"].ToString() == "Double") { columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
                else if (drv["DataTypeSysName"].ToString() == "Byte[]") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
                else if (drv["DataTypeSysName"].ToString() == "Object") { columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
                else { columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
            }
            return dt;
        }

        static private DataTable GetAuthRow(string ssd, int systemId, int screenId)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[byte.Parse(systemId.ToString())];
            UsrCurr uc = GetUsrCurr(byte.Parse(systemId.ToString()), ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            DataTable dt = (new AdminSystem()).GetAuthRow(screenId, ui.RowAuthoritys, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            return dt;
        }

        static private DataView GetCriCache(string ssd, int systemId, int screenId)
        {
            // this is a system wide format and must be kept in sync with robot if there is any change of it
            string criKey = string.Format("Cache:dtScrCri{0}_{1}",systemId,screenId);
            HttpSessionState Session = HttpContext.Current.Session;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[byte.Parse(systemId.ToString())];
            UsrCurr uc = GetUsrCurr(byte.Parse(systemId.ToString()), ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            DataTable dt = (DataTable) Session[criKey];
            if (dt == null) {
				dt = (new AdminSystem()).GetScrCriteria(screenId.ToString(), dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
                //Session[criKey] = dt;
            }
            return dt.DefaultView;
        }
        static private DataView GetSqlRptCriCache(string ssd, string genPrefix, int systemId, int rptId)
        {
            // this is a system wide format and must be kept in sync with robot if there is any change of it
            string criKey = string.Format("Cache:dtSqlCri{0}_{1}", systemId, rptId);
            HttpSessionState Session = HttpContext.Current.Session;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[byte.Parse(systemId.ToString())];
            UsrCurr uc = GetUsrCurr(byte.Parse(systemId.ToString()), ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            DataTable dt = (DataTable)Session[criKey];
            if (dt == null)
            {
                dt = (new SqlReportSystem()).GetReportCriteria(genPrefix, rptId.ToString(), dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
                //Session[criKey] = dt;
            }
            return dt.DefaultView;
        }
        static private DataTable GetDdlRptMemCri(byte csy, string ssd, string getLisMethod, bool bAddNew, int systemId, string GenPrefix, string rptId, string searchStr, string conn, string isSys, string sp, string requiredValid, int topN)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dt = null;
            dt = (new SqlReportSystem()).GetDdlRptMemCri(GenPrefix, rptId,
                !searchStr.StartsWith("**"),
                topN,
                searchStr.StartsWith("**") ? searchStr.Substring(2) : "",
                isSys != "N" ? null : dSys[KEY_SysConnectStr],
                isSys != "N" ? null : dSys[KEY_AppPwd],
                searchStr.StartsWith("**") ? "" : searchStr,
                ui,
                uc);
            return dt;
        }
        static private DataTable GetRptCriDdl(byte csy, string ssd, string getLisMethod, bool bAddNew, int systemId, string GenPrefix, string rptId, string rptCridId, string searchStr, string conn, string isSys, string sp, string requiredValid, int topN)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dt = null;
            dt = (new SqlReportSystem()).GetIn(rptId, sp, (new SqlReportSystem()).CountRptCri(rptCridId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]), requiredValid, !searchStr.StartsWith("**"), searchStr.StartsWith("**") ? searchStr.Substring(2) : "", ui, uc, dSys[KEY_AppConnectStr], dSys[KEY_AppPwd]);

            return dt;
        }
        static private DataTable GetDdl(byte csy, string ssd, string getLisMethod, bool bAddNew, int systemId, int screenId, string searchStr, string conn, string isSys,string sp, string requiredValid, int topN)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dt = null;
            if (!string.IsNullOrEmpty(sp))
            {
                var regex = new System.Text.RegularExpressions.Regex("C[0-9]+$");
                var scrCriId = sp.Replace(regex.Replace(sp, ""), "").Replace("C", "");
                int CriCnt = (new AdminSystem()).CountScrCri(scrCriId, string.IsNullOrEmpty(conn) ? "N" : "Y", dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
                dt = (new AdminSystem()).GetScreenIn(screenId.ToString(), sp, CriCnt, requiredValid, topN,
                searchStr.StartsWith("**") ? "" : searchStr, !searchStr.StartsWith("**"), searchStr.StartsWith("**") ? searchStr.Substring(2) : "", ui, uc,
                isSys != "N" ? null : (string)(string.IsNullOrEmpty(conn) ? dSys[KEY_AppConnectStr] : Session[conn]),
                isSys != "N" ? null : dSys[KEY_AppPwd]);
            }
            else
            {
                dt = (new AdminSystem()).GetDdl(screenId, getLisMethod, bAddNew, !searchStr.StartsWith("**"), topN, searchStr.StartsWith("**") ? searchStr.Substring(2) : "",
                    isSys != "N" ? null : (string)(string.IsNullOrEmpty(conn) ? dSys[KEY_AppConnectStr] : Session[conn]),
                    isSys != "N" ? null : dSys[KEY_AppPwd], searchStr.StartsWith("**") ? "" : searchStr, ui, uc);
            }
            return dt;
        }
        static private DataTable[] GetRptData(byte csy, string ssd, int systemId, string GenPrefix, string rptId, Dictionary<string,string> rptCri)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dtMenuAccess = (new MenuSystem()).GetMenu(usr.CultureId, csy, ui, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd], null, int.Parse(rptId), null);
            DataTable dtRptHlp = (new SqlReportSystem()).GetRptHlp(GenPrefix, int.Parse(rptId), usr.CultureId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            DataTable dtRptObjHlp = (new SqlReportSystem()).GetReportObjHlp(GenPrefix, int.Parse(rptId), usr.CultureId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            DataView dvCri = GetSqlRptCriCache(ssd, GenPrefix, csy, int.Parse(rptId));
            DataSet dsRptCri = MakeRptCriteria(dvCri,rptCri);
            DataTable dtRpt = (new SqlReportSystem()).GetSqlReport(rptId, dtRptHlp.Rows[0]["ProgramName"].ToString(), dvCri, ui, uc, dsRptCri, dSys[KEY_AppConnectStr], dSys[KEY_AppPwd], false, false, false);
            DataTable dtRptParm = (new SqlReportSystem()).GetSqlReport(rptId, dtRptHlp.Rows[0]["ProgramName"].ToString(), dvCri, ui, uc, dsRptCri, dSys[KEY_AppConnectStr], dSys[KEY_AppPwd], false, false, true);
            return new DataTable[]{ dtRptHlp, dtRptObjHlp, dtRpt, dtRptParm};
        }
        static private DataTable GetRptObjHlp(byte csy, string ssd, int systemId, string GenPrefix, string rptId)
        {
            HttpSessionState Session = HttpContext.Current.Session;
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataTable dtRptObjHlp = (new SqlReportSystem()).GetReportObjHlp(GenPrefix, int.Parse(rptId), usr.CultureId, dSys[KEY_SysConnectStr], dSys[KEY_AppPwd]);
            return dtRptObjHlp;

        }

        static private DataTable GetLis(byte csy, string ssd, string getLisMethod, int systemId, int screenId, string searchStr, string filterId, string conn, string isSys, int topN)
        {
            // this is a system wide format and must be kept in sync with robot if there is any change of it
            string criValKey = string.Format("Cache:dsScrCriVal{0}_{1}", systemId, screenId);
            HttpSessionState Session = HttpContext.Current.Session;
            DataView dvCri = GetCriCache(ssd, systemId, screenId);
            HttpContext Context = HttpContext.Current;
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            DataSet ds = (DataSet)Session[criValKey] ?? MakeScrCriteria(dvCri, GetLastScrCriteria(ssd, systemId, screenId, dvCri));
            DataTable dt = (new AdminSystem()).GetLis(screenId, getLisMethod, true, "Y", topN, isSys != "N" ? (string)null : (string)(string.IsNullOrEmpty(conn) ? dSys[KEY_AppConnectStr] : Session[conn]), isSys != "N" ? null : dSys[KEY_AppPwd],
                string.IsNullOrEmpty(filterId) ? 0 : int.Parse(filterId),searchStr.StartsWith("**") ? searchStr.Substring(2) : "",searchStr.StartsWith("**") ? "" : searchStr,
                dvCri,ui,uc,ds);
            return dt;
        }
        public AutoComplete()
        {
            
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
//            string x = "";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InitializeComponent();
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse DdlSuggestsEx(string query, string contextKey, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> clientContext = jss.Deserialize<Dictionary<string, string>>(contextKey);
            Dictionary<string, string> context = HttpContext.Current.Session[clientContext["contextKey"]] as Dictionary<string, string>;
            context["refColVal"] = clientContext["refColVal"];
            context["pMKeyVal"] = clientContext["pMKeyVal"];
            context["refColValIsList"] = clientContext["refColValIsList"];
            return DdlSuggests(query, jss.Serialize(context), topN);
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse DdlSuggests(string query, string contextStr, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

            HttpContext Context = HttpContext.Current;
            HttpSessionState Session = HttpContext.Current.Session;
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int screenId = int.Parse(context["scr"]);
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy,context["ssd"]);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataTable dtSuggest = GetDdl(csy, context["ssd"], context["method"], context["addnew"] == "Y", csy, screenId, query, context["conn"], context["isSys"], context.ContainsKey("sp") ? context["sp"] : "", context.ContainsKey("requiredValid") ? context["requiredValid"] : "", (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"])) || (context.ContainsKey("pMKeyCol") && !string.IsNullOrEmpty(context["pMKeyCol"])) ? 0 : topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") ? context["mTip"] : "";
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : "";
            total = dtSuggest.Rows.Count;
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = GetAuthRow(context["ssd"], csy, screenId);
            string[] DesiredKeys = query.StartsWith("**") ? query.Substring(2).Replace("(", "").Replace(")", "").Split(new char[] { ',' }) : new string[0];
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape("**");
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            //dtSuggest.DefaultView.Sort = valF;
            string filter = "";
            string[] needQuoteType = {"Char","Date","Time","String"};
            if (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"]))
            {
                string[] x = context["refCol"].Split(new char[]{'_'});
                bool isList = context.ContainsKey("refColValIsList") && context["refColValIsList"] == "Y";
                string[] filterColumnIsIntType = {"Int","SmallInt","TinyInt","BigInt"};
                bool filterColumnIsList = context.ContainsKey("refColDataType") && "TinyInt,SmallInt,Int,BigInt,Char,NChar".IndexOf(context["refColDataType"]) >= 0 && dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains("String");
                try
                {
                    if (filterColumnIsList)
                    {
                        if ("Char,NChar".IndexOf(context["refColDataType"]) >= 0)
                            filter = string.Format(" (',' + SUBSTRING(ISNULL({0},'()'),2,LEN(ISNULL({0},'()'))-2) + ',' LIKE '%,''{1}'',%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                        else
                            filter = string.Format(" (',' + SUBSTRING(ISNULL({0},'()'),2,LEN(ISNULL({0},'()'))-2) + ',' LIKE '%,{1},%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                    }
                    else
                    {
                        bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                        if (needQuote)
                        {
                            if (isList)
                                filter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], "'" + context["refColVal"].Replace("'", "''").Replace(",", "','") + "'");
                            else
                            {
                                filter = string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                            }

                        }
                        else
                        {
                            if (isList)
                                filter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            else
                            {
                                filter = string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            }

                        }
                    }

                }
                catch { }
            }
            if (context.ContainsKey("pMKeyCol") && !string.IsNullOrEmpty(context["pMKeyCol"]) && dtSuggest.Columns.Contains(context["pMKeyCol"]))
            {
                string[] x = context["pMKeyCol"].Split(new char[] { '_' });
                try
                {
                    bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                    if (needQuote)
                        filter = filter + (!string.IsNullOrEmpty(filter) ? " AND " : string.Empty) + string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["pMKeyColVal"].Replace("'","''"));
                    else
                        filter = filter + (!string.IsNullOrEmpty(filter) ? " AND " : string.Empty) + string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["pMKeyColVal"]);
                }
                catch { }
            }
            try
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    dtSuggest.DefaultView.RowFilter = filter;
                    total = dtSuggest.DefaultView.Count;
                }
            }
            catch { }
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[keyF].ToString().Trim();
                if (true || ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss) || (query.StartsWith(doublestar) && !DesiredKeys.Contains(ss) && !DesiredKeys.Contains("'" + ss + "'")))
                    {
                        total = total - 1;
                    }
                    else if (regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        results.Add(new Dictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"value",drv[valF].ToString()}, // visible value shown in jquery's autocomplete box
                            {"img", imgF !="" ? drv[imgF].ToString() : null}, // optional image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
                    }
                    if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.topN = topN;
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse RptCriDdlSuggests(string query, string contextStr, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

            HttpContext Context = HttpContext.Current;
            HttpSessionState Session = HttpContext.Current.Session;
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int rptId = int.Parse(context["rpt"]);
            string genPrefix = context["genPrefix"];
            string rptCriId = context["reportCriId"];
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, context["ssd"]);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataTable dtSuggest = GetRptCriDdl(csy, context["ssd"], context["method"], context["addnew"] == "Y", csy, genPrefix, rptId.ToString(), rptCriId, query, context["conn"], context["isSys"], context.ContainsKey("sp") ? context["sp"] : "", context.ContainsKey("requiredValid") ? context["requiredValid"] : "", context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"]) ? 0 : topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") ? context["mTip"] : "";
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : "";
            total = dtSuggest.Rows.Count;
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            string[] DesiredKeys = query.StartsWith("**") ? query.Substring(2).Replace("(", "").Replace(")", "").Split(new char[] { ',' }) : new string[0];
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape("**");
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            //dtSuggest.DefaultView.Sort = valF;
            string[] needQuoteType = { "Char", "Date", "Time", "String" };
            if (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"]))
            {
                string[] x = context["refCol"].Split(new char[] { '_' });
                bool isList = context.ContainsKey("refColValIsList") && context["refColValIsList"] == "Y";
                string[] filterColumnIsIntType = {"Int","SmallInt","TinyInt","BigInt","Char","NChar"};
                bool filterColumnIsList = context.ContainsKey("refColDataType") && "TinyInt,SmallInt,Int,BigInt,Char,NChar".IndexOf(context["refColDataType"]) >= 0 && dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains("String");
                try
                {
                    if (filterColumnIsList)
                    {
                        dtSuggest.DefaultView.RowFilter = string.Format(" (',' + SUBSTRING(ISNULL({0},'()'),2,LEN(ISNULL({0},'()'))-2) + ',' like '%,{1},%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                    }
                    else
                    {
                        bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                        if (needQuote)
                        {
                            if (isList)
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], "'" + context["refColVal"].Replace("'", "''").Replace(",", "','") + "'");
                            else
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));

                        }
                        else
                        {
                            if (isList)
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            else
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);

                        }
                    }
                }
                catch { }
            }
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[keyF].ToString().Trim();
                if (true || ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss) || (query.StartsWith(doublestar) && !DesiredKeys.Contains(ss) && !DesiredKeys.Contains("'" + ss + "'")))
                    {
                        total = total - 1;
                    }
                    else if (regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        results.Add(new Dictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"value",drv[valF].ToString()}, // visible value shown in jquery's autocomplete box
                            {"img", imgF !="" ? drv[imgF].ToString() : null}, // optional icon url
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
                    }
                    if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.topN = topN;
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse RptCriDdlSuggestsEx(string query, string contextKey, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> clientContext = jss.Deserialize<Dictionary<string, string>>(contextKey);
            Dictionary<string, string> context = HttpContext.Current.Session[clientContext["contextKey"]] as Dictionary<string, string>;
            context["refColVal"] = clientContext["refColVal"];
            context["refColValIsList"] = clientContext["refColValIsList"];
            return RptCriDdlSuggests(query, jss.Serialize(context), topN);
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse RptMemCriSuggests(string query, string contextStr, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

            HttpContext Context = HttpContext.Current;
            HttpSessionState Session = HttpContext.Current.Session;
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            string rptId = context["rpt"];
            string genPrefix = context["genPrefix"];
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, context["ssd"]);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataTable dtSuggest = GetDdlRptMemCri(csy, context["ssd"], context["method"], context["addnew"] == "Y", csy, genPrefix, rptId, query, context["conn"], context["isSys"], context.ContainsKey("sp") ? context["sp"] : "", context.ContainsKey("requiredValid") ? context["requiredValid"] : "", topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") ? context["mTip"] : "";
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : "";
            total = dtSuggest.Rows.Count;
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            string[] DesiredKeys = query.StartsWith("**") ? query.Substring(2).Replace("(", "").Replace(")", "").Split(new char[] { ',' }) : new string[0];
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape("**");
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.ToLower().Replace("\\ ", ".*"));
            //dtSuggest.DefaultView.Sort = valF;
            string[] needQuoteType = { "Char", "Date", "Time", "String" };
            if (context.ContainsKey("refCol") && !string.IsNullOrEmpty(context["refCol"]))
            {
                string[] x = context["refCol"].Split(new char[] { '_' });
                bool isList = context.ContainsKey("refColValIsList") && context["refColValIsList"] == "Y";
                string[] filterColumnIsIntType = {"Int","SmallInt","TinyInt","BigInt"};
                bool filterColumnIsList = context.ContainsKey("refColDataType") && "TinyInt,SmallInt,Int,BigInt,Char,NChar".IndexOf(context["refColDataType"]) >= 0 && dtSuggest.Columns[x[x.Length - 1]].DataType.ToString() == "String";
                try
                {
                    if (filterColumnIsList)
                    {
                        dtSuggest.DefaultView.RowFilter = string.Format(" (',' + SUBSTRING({0},2,LEN({0})-2) + ',' like '%,{1},%' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));
                    }
                    else
                    {
                        bool needQuote = needQuoteType.Any(dtSuggest.Columns[x[x.Length - 1]].DataType.ToString().Contains);
                        if (needQuote)
                        {
                            if (isList)
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], "'" + context["refColVal"].Replace("'", "''").Replace(",", "','") + "'");
                            else
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} = '{1}' OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"].Replace("'", "''"));

                        }
                        else
                        {
                            if (isList)
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} IN ({1}) OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);
                            else
                                dtSuggest.DefaultView.RowFilter = string.Format(" ({0} = {1} OR {0} IS NULL) ", x[x.Length - 1], context["refColVal"]);

                        }
                    }
                }
                catch { }
            }
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[keyF].ToString().Trim();
                if (true || ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss) || (query.StartsWith(doublestar) && !DesiredKeys.Contains(ss) && !DesiredKeys.Contains("'" + ss + "'")))
                    {
                        total = total - 1;
                    }
                    else if (regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        results.Add(new Dictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"value",drv[valF].ToString()}, // visible value shown in jquery's autocomplete box
                            {"img", imgF !="" ? drv[imgF].ToString() : null}, // optional icon url
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
                    }
                    if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.topN = topN;
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }

        [WebMethod(EnableSession = true)]
        static public ChartResponse RptGetChart(string contextStr)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

            HttpContext Context = HttpContext.Current;
            HttpRequest request = Context.Request;
            HttpSessionState Session = HttpContext.Current.Session;
            ChartResponse ret = new ChartResponse();
            Func<Dictionary<string,string>,string,string,string> tryGet = (coll, key, dflt) => coll.ContainsKey(key) ? coll[key] : dflt;

            string genPrefix = tryGet(context, "genPrefix", "");
            string rptUrl = tryGet(context, "rptUrl", "");
            string rptId = tryGet(context, "rpt", null);
            string csy = tryGet(context, "csy", null); ;
            string ssd = tryGet(context,"ssd",null);
            string xLabelCol = tryGet(context,"xLabelCol",null);
            string yLabelCol = tryGet(context,"yLabelCol",null);

            if (!string.IsNullOrEmpty(rptUrl)
                &&
                (csy==null || rptId == null))
            {
                string[] rptUrlQS = rptUrl.Substring(rptUrl.IndexOf("?") + 1).Split(new char[]{'&'});
                foreach (var q in rptUrlQS)
                {
                    string[] kv = q.Split(new char[] { '=' });
                    if (kv.Length > 1)
                    {
                        string k = kv[0].ToLower();
                        if (k == "csy" && csy == null) csy = kv[1];
                        else if (k == "rpt" && rptId == null) rptId = kv[1];
                    }
                }
            }

            if (request.UrlReferrer != null
                &&
                (csy == null || ssd == null)
                )
            {
                string[] referrerQS = request.UrlReferrer.Query.Split(new char[]{'&'});
                foreach (var q in referrerQS) 
                {
                    string[] kv=q.Split(new char[]{'='});
                    if (kv.Length > 1)
                    {
                        string k = kv[0].ToLower();
                        if (k == "csy" && csy == null) csy = kv[1];
                        else if (k == "ssd" && ssd == null) ssd = kv[1];
                    }
                }
            }
             
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[byte.Parse(csy)];
            UsrCurr uc = GetUsrCurr(byte.Parse(csy), ssd);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            string[] rptUrlQueryString = rptUrl.Substring(rptUrl.IndexOf("?") + 1).Split(new char[]{'&'});
            Dictionary<string,string> rptDefaultCri = (from v in rptUrlQueryString 
                                                        where v.ToLower().StartsWith("cri")
                                                        select 
                                                        v.Split(new char[]{'='})).ToDictionary(v=>v[0].ToLower().Replace("cri",""),v=>v.Length>1 ? System.Web.HttpUtility.UrlDecode(v[1]) : "");
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            Dictionary<string,string> rptCri = (from v in context.AsEnumerable()
                                                where v.Key.ToLower().StartsWith("cri")
                                                select new KeyValuePair<string, string>(v.Key.ToLower().Replace("cri", ""), v.Value))
                                                .Aggregate<KeyValuePair<string, string>, Dictionary<string, string>>(
                                                rptDefaultCri, 
                                                (cri, v) => { cri[v.Key] = v.Value; return cri; });

            DataTable[] dtRptContent = GetRptData(byte.Parse(csy), ssd, byte.Parse(csy), genPrefix, rptId, rptCri);
            DataTable dtRptHlp = dtRptContent[0];
            DataTable dtRptObjHlp = dtRptContent[1];
            DataTable dtRpt = dtRptContent[2];
            DataTable dtRptParm = dtRptContent[3];
            DataView dvRptObjHlp = dtRptObjHlp.DefaultView;

            dvRptObjHlp.RowFilter = "RptObjTypeCd = 'F'";
            int xColIdx = string.IsNullOrEmpty(xLabelCol) ? 0 : dtRpt.Columns.IndexOf(xLabelCol);
            int yColIdx = string.IsNullOrEmpty(yLabelCol) ? 1 : dtRpt.Columns.IndexOf(yLabelCol);
            if (xColIdx < 0) xColIdx = 0;
            if (yColIdx < 0) yColIdx = 1;

            xLabelCol = string.IsNullOrEmpty(xLabelCol) ? dtRpt.Columns[xColIdx].ColumnName : xLabelCol;
            yLabelCol = string.IsNullOrEmpty(yLabelCol) ? dtRpt.Columns[yColIdx].ColumnName : yLabelCol;

            List<List<string>> dataPoint = (from r in dtRpt.AsEnumerable()
                                           select new List<string> 
                                           { 
                                               r[xColIdx].ToString(),r[yColIdx].ToString()
                                           }
                                           ).ToList();
            ret.data = dataPoint;
            try {
                try
                {
                    ret.xLabel = dtRptParm.Rows[0][xLabelCol].ToString();
                }
                catch 
                {
                    string label = (from x in dtRptObjHlp.AsEnumerable()
                                 where x["ColumnName"].ToString() == xLabelCol
                                 select x["ColumnHeader"].ToString()).FirstOrDefault();

                    ret.xLabel = string.IsNullOrEmpty(label) ? xLabelCol : label;
                }
            }
            catch { };
            try {
                try
                {
                    ret.yLabel = dtRptParm.Rows[0][yLabelCol].ToString();
                }
                catch 
                {
                    string label = (from x in dtRptObjHlp.AsEnumerable()
                                    where x["ColumnName"].ToString() == yLabelCol
                                    select x["ColumnHeader"].ToString()).FirstOrDefault();
                    ret.yLabel = string.IsNullOrEmpty(label) ? yLabelCol : label;
                }
            }
            catch { };
            try
            {
                try
                {
                    ret.rptTitle = (dtRptParm.Rows[0]["SubTitle"] ?? "").ToString();
                }
                catch 
                {
                    ret.rptTitle = (dtRptHlp.Rows[0]["ReportTitle"] ?? "").ToString();
                }
            }
            catch { }

            /* returning data */
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse LisSuggests(string query, string contextStr, int topN)
        {
            /* this is intended to be used by keyid fields where the text suggested ties to specific key
             * in a table and not using returned value is supposed to be an error
             */
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, string> context = jss.Deserialize<Dictionary<string, string>>(contextStr);

            HttpContext Context = HttpContext.Current;
            HttpSessionState Session = HttpContext.Current.Session;
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int screenId = int.Parse(context["scr"]);
            Dictionary<byte, Dictionary<string, string>> dSysList = (Dictionary<byte, Dictionary<string, string>>)Session[KEY_SystemsDict];
            Dictionary<string, string> dSys = dSysList[csy];
            UsrCurr uc = GetUsrCurr(csy, context["ssd"]);
            UsrImpr ui = (UsrImpr)Session[KEY_CacheLImpr];
            LoginUsr usr = (LoginUsr)Session[KEY_CacheLUser];
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;
            DataTable dtSuggest = GetLis(csy, context["ssd"], context["method"], csy, screenId, query, context["filter"], context["conn"], context["isSys"], topN);
            string keyF = context["mKey"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string dtlF = context.ContainsKey("mDtl") && false ? context["mDtl"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string tipF = context.ContainsKey("mTip") && false ? context["mTip"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : (dtSuggest.Columns.Contains(keyF + "Img") ? keyF + "Img" : "");
            string iconUrlF = context.ContainsKey("mIconUrl") && false ? context["mIconUrl"] : (dtSuggest.Columns.Contains(keyF + "Url")  ? keyF + "Url" : "");
            bool hasDtlColumn = dtSuggest.Columns.Contains(keyF + "Dtl");
            bool hasIconColumn = dtSuggest.Columns.Contains(keyF + "Url");
            bool hasImgColumn = dtSuggest.Columns.Contains(keyF + "Img");
            total = dtSuggest.Rows.Count;
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            DataTable dtAuthRow = GetAuthRow(context["ssd"], csy, screenId);
            bool allowAdd = dtAuthRow.Rows.Count == 0 || dtAuthRow.Rows[0]["AllowAdd"].ToString() != "N";
            //dtSuggest.DefaultView.Sort = valF;
            //int pos = 1;
            query = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            string doublestar = System.Text.RegularExpressions.Regex.Escape(query.ToLower());
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(query.Replace("\\ ", ".*"));
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[keyF].ToString().Trim();
                if (allowAdd || ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss))
                    {
                        total = total - 1;
                    }
                    else if (GetCriCache(context["ssd"], csy, screenId).Count > 0 || regex.IsMatch(drv[valF].ToString().ToLower()) || query.StartsWith(doublestar) || string.IsNullOrEmpty(query))
                    {
                        Choices[drv[keyF].ToString()] = drv[valF].ToString();
                        results.Add(new Dictionary<string, string> { 
                            {"key",drv[keyF].ToString()}, // internal key 
                            {"label",drv[valF].ToString()}, // visible dropdown list as used in jquery's autocomplete
                            {"value",drv[valF].ToString()}, // visible value shown in jquery's autocomplete box
                            {"iconUrl",iconUrlF !="" ?  drv[iconUrlF].ToString() : null}, // optional icon url
                            {"img", imgF !="" ? (drv[imgF].ToString() == "" ? "": "data:application/base64;base64," + Convert.ToBase64String(drv[imgF] as byte[]))  : null}, // optional embedded image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""},// optional alternative tooltips(say expanded description)
                            {"detail",dtlF !="" ? drv[dtlF].ToString() : null} // optional alternative tooltips(say expanded description)
                            /* more can be added in the future for say multi-column list */
                            });
                    }
                    else
                    {
                        total = total - 1;
                    }
                    if (Choices.Count >= (topN > 0 ? topN : 15)) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            ret.topN = topN;
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }

        [WebMethod(EnableSession = true)]
        static public AutoCompleteResponse TextSuggests(string query,Dictionary<string,string> context)
        {
            /* this is intended to be used by Textbox(i.e. free text) where the suggestions are from
             * some existing data but allows free typing
             */
            HttpContext Context = HttpContext.Current;
            HttpSessionState Session = HttpContext.Current.Session;
            AutoCompleteResponse ret = new AutoCompleteResponse();
            byte csy = byte.Parse(context["csy"]);
            int screenid = int.Parse(context["scr"]);
            List<string> suggestion = new List<string>();
            List<string> key = new List<string>();
            int total = 0;

            /* where the actual wiring of data starts */
            DataTable dtSuggest = GetLis(csy, context["ssd"], context["method"], csy, screenid, query, "", "", "N", 0);
            string keyF = context["mKey"].ToString() + context["mTblId"].ToString();
            string valF = context.ContainsKey("mVal") ? context["mVal"] : keyF + "Text";
            string tipF = context.ContainsKey("mTip") && false ? context["mTip"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string dtlF = context.ContainsKey("mDtl") && false ? context["mDtl"] : (dtSuggest.Columns.Contains(keyF + "Dtl") ? keyF + "Dtl" : "");
            string imgF = context.ContainsKey("mImg") && false ? context["mImg"] : (dtSuggest.Columns.Contains(keyF + "Img") ? keyF + "Img" : "");
            string iconUrlF = context.ContainsKey("mIconUrl")  && false ? context["mIconUrl"] : (dtSuggest.Columns.Contains(keyF + "Url") ? keyF + "Url" : "");
            total = dtSuggest.Rows.Count;
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Dictionary<string, string> Choices = new Dictionary<string, string>();
            dtSuggest.DefaultView.Sort = valF;
            
            foreach (DataRowView drv in dtSuggest.DefaultView)
            {
                string ss = drv[valF].ToString().Trim();
                if (ss != string.Empty)
                {
                    if (Choices.ContainsKey(ss))
                    {
                        total = total - 1;
                    }
                    else
                    {
                        Choices[drv[valF].ToString()] = drv[keyF].ToString();
                        results.Add(new Dictionary<string, string> { 
                            {"label",drv[valF].ToString()},
                            {"value",drv[keyF].ToString()},
                            {"iconUrl",iconUrlF !="" ? drv[iconUrlF].ToString() : ""}, // optional icon url
                            {"img", imgF !="" ? (drv[imgF].ToString() == "" ? "": "data:application/base64;base64," + Convert.ToBase64String(drv[imgF] as byte[]))  : ""}, // optional embedded image
                            {"tooltips",tipF !="" ? drv[tipF].ToString() : ""}, // optional alternative tooltips(say expanded description)
                            {"detail",tipF !="" ? drv[dtlF].ToString() : ""} // optional alternative tooltips(say expanded description)
                            });
                    }

                    if (Choices.Count >= 15) break;
                }
                else
                {
                    total = total - 1;
                }
            }

            /* returning data */
            ret.query = query;
            ret.data = results;
            ret.total = total;
            
            Context.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Context.Response.Expires = 0;
            Context.Response.CacheControl = "no-cache";

            return ret;
        }


        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
