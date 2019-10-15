<%@ WebService Language="C#" Class="SqlReportWs" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.Rule3;
using RO.Web;
using System.Xml;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;

[ScriptService()]
[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class SqlReportWs : RO.Web.AsmxBase
{
    const int screenId = 0;
    const byte systemId = 1;
    const string programName = "SqlReport";

    protected override byte GetSystemId() { return systemId; }
    protected override int GetScreenId() { return screenId; }
    protected override string GetProgramName() { return programName; }
    protected override string GetValidateMstIdSPName() { throw new NotImplementedException(); }
    protected override string GetMstTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetDtlTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetMstKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    protected override string GetDtlKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
        
    protected override DataTable _GetMstById(string pid)
    {
        throw new NotImplementedException();
    }
    protected override DataTable _GetDtlById(string pid, int screenFilterId)
    {
        throw new NotImplementedException();
    }
    protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitDtl()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitMaster()
    {
        throw new NotImplementedException();
    }
    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> GetReportHlp(string rptId)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SerializableDictionary<string, string>, SerializableDictionary<string, AutoCompleteResponse>>();
            SerializableDictionary<string, string> result = new SerializableDictionary<string, string>();
            DataTable dt = _GetReportHlp(rptId);
            mr.errorMsg = "";
            mr.data = DataTableToListOfObject(dt)[0]; ;
            mr.status = "success";

            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
        return ret;
    }

    protected DataTable _GetReportHlp(string rptId)
    {
        var context = HttpContext.Current;
        var cache = context.Cache;
        string cacheKey = loginHandle + "_ReportHlp_" + LCurr.SystemId.ToString() + "_" + LUser.CultureId.ToString() + "_" + rptId.ToString();
        int minutesToCache = 1;

        DataTable dtReportHlp = cache[cacheKey] as DataTable;
        if (dtReportHlp == null)
        {
            string GenPrefix = string.Empty;
            dtReportHlp = (new SqlReportSystem()).GetRptHlp(GenPrefix, Int32.Parse(rptId), base.LUser.CultureId, LcSysConnString, LcAppPw);

            cache.Add(cacheKey, dtReportHlp, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
        }
        return dtReportHlp;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>> GetReportCriteria(string rptId)
    {
        Func<ApiResponse<AutoCompleteResponseObj, SerializableDictionary<string, AutoCompleteResponseObj>>> fn = () =>
        {
            SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);
            DataTable dt = _GetReportCriteria(rptId);
            if (!dt.Columns.Contains("LastCriteria"))
            {
                dt.Columns.Add("LastCriteria", typeof(string));
            }
            DataTable dtLastCriteria = _GetRptCriteria(dt.Rows.Count, int.Parse(rptId), LUser.UsrId);
            //skip first row in last criteria
            for(int ii = 0; ii < dtLastCriteria.Rows.Count; ii++)
            {
                dt.Rows[ii]["LastCriteria"] = dtLastCriteria.Rows[ii]["LastCriteria"];
            }
            return DataTableToLabelResponse(dt, new List<string>() { "ColumnName" });
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
        return ret;
    }

    protected DataTable _GetReportCriteria(string rptId)
    {
        var context = HttpContext.Current;
        var cache = context.Cache;
        string cacheKey = loginHandle + "_ReportCriteria_" + LCurr.SystemId.ToString() + "_" + LCurr.CompanyId.ToString() + "_" + LCurr.ProjectId.ToString() + "_" + rptId.ToString();
        int minutesToCache = 1;
        DataTable dtReportCriteria = cache[cacheKey] as DataTable;
        if (dtReportCriteria == null)
        {
            string GenPrefix = string.Empty;
            dtReportCriteria = (new SqlReportSystem()).GetReportCriteria(GenPrefix, rptId, LcSysConnString, LcAppPw);
            cache.Add(cacheKey, dtReportCriteria, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
        }
        return dtReportCriteria;
    }

    protected DataTable _GetRptCriteria(int rowsExpected, int rptId, int usrId)
    {
        //don't cache
        string GenPrefix = string.Empty;
        DataTable dtRptCriteria = (new SqlReportSystem()).GetRptCriteria(rowsExpected, GenPrefix, rptId, usrId, LcSysConnString, LcAppPw);
        return dtRptCriteria;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, List<SerializableDictionary<string, string>>>, string> GetReportCriDdl(string rptId)
    {
        Func<ApiResponse<SerializableDictionary<string, List<SerializableDictionary<string, string>>>, string>> fn = () =>
        {
            SwitchContext(GetSystemId(), LCurr.CompanyId, LCurr.ProjectId);

            var resp = new ApiResponse<SerializableDictionary<string, List<SerializableDictionary<string, string>>>, string>();

            resp.data = GetReportCriteriaDdls(rptId);
            resp.errorMsg = "";
            resp.status = "success";

            return resp;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, GetSystemId(), GetScreenId(), "R", null));
        return ret;
    }

    protected SerializableDictionary<string, List<SerializableDictionary<string, string>>> GetReportCriteriaDdls(string rptId)
    {
        DataView dvCri = new DataView(_GetReportCriteria(rptId));
        SerializableDictionary<string, List<SerializableDictionary<string, string>>> ddls = new SerializableDictionary<string, List<SerializableDictionary<string, string>>>();

        foreach (DataRowView drv in dvCri)
        {
            string columnName = drv["ColumnName"].ToString();

            if (drv["DisplayName"].ToString() == "ListBox")
            {
                var dt = (new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), true, "", base.LImpr, base.LCurr, LcAppConnString, LcAppPw);
                ddls.Add(columnName, DataTableToListOfDdlObject(drv, dt));
            }
            else if (drv["DisplayName"].ToString() == "ComboBox")
            {
                var dt = (new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, "", base.LImpr, base.LCurr, LcAppConnString, LcAppPw);
                ddls.Add(columnName, DataTableToListOfDdlObject(drv, dt));
            }
            else if (drv["DisplayName"].ToString() == "DropDownList")
            {
                var dt = (new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, "", base.LImpr, base.LCurr, LcAppConnString, LcAppPw);
                dt.Rows[0][drv["DdlRefColumnName"].ToString()] = " ";
                ddls.Add(columnName, DataTableToListOfDdlObject(drv, dt));
            }
            else if (drv["DisplayName"].ToString() == "RadioButtonList")
            {
                var dt = (new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), base.LImpr, base.LCurr, LcAppConnString, LcAppPw);
                ddls.Add(columnName, DataTableToListOfDdlObject(drv, dt));
            }
        }

        return ddls;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<string, object> GetReport(string reportId, SerializableDictionary<string, string> criteria, string fmt)
    {
        Func<ApiResponse<string, object>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);

            ApiResponse<string, object> result = new ApiResponse<string, object>();

            result.data = Convert.ToBase64String(DoPdfRptLocal(reportId, criteria, fmt));
            result.status = "success";

            return result;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
        return ret;
    }

    protected byte[] DoPdfRptLocal(string rptId, SerializableDictionary<string,string> criteria, string fmt)
    {
        DataSet ds = UpdCriteria(rptId, true, false, criteria);
        string GenPrefix = string.Empty;
        Microsoft.Reporting.WebForms.ReportViewer cViewer = new Microsoft.Reporting.WebForms.ReportViewer();
        DataView dvCri = new DataView(_GetReportCriteria(rptId));
        DataView dvObj = GetSqlColumns(rptId);
        int ii = dvCri.Count + dvObj.Count + 21;
        dvObj.RowFilter = "RptObjTypeCd = 'P'"; ii = ii + dvObj.Count; dvObj.RowFilter = string.Empty;
        cViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        cViewer.LocalReport.EnableHyperlinks = true;
        cViewer.LocalReport.EnableExternalImages = true;
        //string urlBase = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "").Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");
        string urlBase = "http://fintrux/rc/";
        System.Xml.XmlDocument rpt = new System.Xml.XmlDocument();
        //string fileName = Request.MapPath("reports\\" + GetReportHlp(rptId).Rows[0]["ProgramName"].ToString() + "Report.rdl");
        //string fileName = Server.MapPath("reports\\" + GetReportHlp(rptId).Rows[0]["ProgramName"].ToString() + "Report.rdl");
        string fileName = Server.MapPath("../reports//" + _GetReportHlp(rptId).Rows[0]["ProgramName"].ToString() + "Report.rdl");
        if (File.Exists(fileName + "c")) { rpt.Load(fileName + "c"); } else { rpt.Load(fileName); }     // Run custom-built report if exists.
        System.Xml.XmlNamespaceManager ns = new System.Xml.XmlNamespaceManager(rpt.NameTable);
        ns.AddNamespace("x", rpt.DocumentElement.NamespaceURI);
        System.Xml.XmlNodeList images = rpt.SelectNodes("//x:Image[x:Source='External']/x:Value", ns);
        foreach (System.Xml.XmlNode n in images)
        {
            if (!(n.InnerText.StartsWith("http:") || n.InnerText.StartsWith("https:")))
            {
                if (!n.InnerText.StartsWith("="))
                {
                    n.InnerText = urlBase + n.InnerText;
                }
                else
                {
                    n.InnerText = n.InnerText.Replace("~/", urlBase);
                }
            }
        }
        System.Xml.XmlNodeList datasourcename = rpt.SelectNodes("//x:DataSetName", ns);
        foreach (System.Xml.XmlNode n in datasourcename)
        {
            n.InnerText = LcAppDb;
        }
        System.Xml.XmlNodeList dataset = rpt.SelectNodes("//x:DataSet", ns);
        foreach (System.Xml.XmlNode n in dataset)
        {
            n.Attributes["Name"].Value = LcAppDb;
        }
        using (MemoryStream ms = new MemoryStream())
        {
            rpt.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            cViewer.LocalReport.LoadReportDefinition(ms);
        }
        DataRow dr = ds.Tables["DtSqlReportIn"].Rows[0];
        string sNull = null;
        Microsoft.Reporting.WebForms.ReportParameter[] pms = new Microsoft.Reporting.WebForms.ReportParameter[ii];
        pms[0] = new Microsoft.Reporting.WebForms.ReportParameter("reportId", rptId);
        pms[1] = new Microsoft.Reporting.WebForms.ReportParameter("RowAuthoritys", base.LImpr.RowAuthoritys);
        pms[2] = new Microsoft.Reporting.WebForms.ReportParameter("Usrs", base.LImpr.Usrs);
        pms[3] = new Microsoft.Reporting.WebForms.ReportParameter("UsrGroups", base.LImpr.UsrGroups);
        pms[4] = new Microsoft.Reporting.WebForms.ReportParameter("Cultures", base.LImpr.Cultures);
        pms[5] = new Microsoft.Reporting.WebForms.ReportParameter("Companys", base.LImpr.Companys);
        pms[6] = new Microsoft.Reporting.WebForms.ReportParameter("Projects", base.LImpr.Projects);
        pms[7] = new Microsoft.Reporting.WebForms.ReportParameter("Agents", base.LImpr.Agents);
        pms[8] = new Microsoft.Reporting.WebForms.ReportParameter("Brokers", base.LImpr.Brokers);
        pms[9] = new Microsoft.Reporting.WebForms.ReportParameter("Customers", base.LImpr.Customers);
        pms[10] = new Microsoft.Reporting.WebForms.ReportParameter("Investors", base.LImpr.Investors);
        pms[11] = new Microsoft.Reporting.WebForms.ReportParameter("Members", base.LImpr.Members);
        pms[12] = new Microsoft.Reporting.WebForms.ReportParameter("Vendors", base.LImpr.Vendors);
        pms[13] = new Microsoft.Reporting.WebForms.ReportParameter("currCompanyId", base.LCurr.CompanyId.ToString());
        pms[14] = new Microsoft.Reporting.WebForms.ReportParameter("currProjectId", base.LCurr.ProjectId.ToString());
        ii = 15;
        foreach (DataRowView drv in dvCri)
        {
            if (drv["RequiredValid"].ToString() == "N")
            {
                if (dr[drv["ColumnName"].ToString()].ToString().Trim() == string.Empty)
                {
                    pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter(drv["ColumnName"].ToString(), sNull);
                }
                else
                {
                    pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter(drv["ColumnName"].ToString(), dr[drv["ColumnName"].ToString()].ToString());
                }
            }
            else
            {
                pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter(drv["ColumnName"].ToString(), dr[drv["ColumnName"].ToString()].ToString());
            }
            ii = ii + 1;
        }
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("bUpd", "N"); ii = ii + 1;
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("bXls", "N"); ii = ii + 1;
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("bVal", "N"); ii = ii + 1;
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("ReportTitle", _GetReportHlp(rptId).Rows[0]["ReportTitle"].ToString()); ii = ii + 1;
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("UsrName", base.LUser.UsrName); ii = ii + 1;
        //pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("UrlBase", base.UrlBase); ii = ii + 1;
        pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("UrlBase", urlBase); ii = ii + 1;
        DataTable dt = (new SqlReportSystem()).GetSqlReport(rptId, _GetReportHlp(rptId).Rows[0]["ProgramName"].ToString(), dvCri, base.LImpr, base.LCurr, UpdCriteria(rptId, false, false, criteria), LcAppConnString, LcAppPw, false, false, true);
        if (dt == null || dt.Rows.Count <= 0) { throw new ApplicationException("Currently there is nothing to report on. Please try again later."); }
        CovertRptUTC(dt);
        DataView dv;
        dv = new DataView((new SqlReportSystem()).GetReportObjHlp(GenPrefix, Int32.Parse(rptId), base.LUser.CultureId, LcSysConnString, LcAppPw));
        foreach (DataRowView drv in dv)
        {
            if (drv["RptObjTypeCd"].ToString() == "P") // Parameter.
            {
                pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter(drv["ColumnName"].ToString(), dt.Rows[0][drv["ColumnName"].ToString()].ToString()); ii = ii + 1;
            }
            pms[ii] = new Microsoft.Reporting.WebForms.ReportParameter("L_" + drv["ColumnName"].ToString(), drv["ColumnHeader"].ToString()); ii = ii + 1;
        }
        cViewer.LocalReport.SetParameters(pms);
        string reportName = "SqlReport";
        if (dt.Columns.Contains("ReportName")) { reportName = dt.Rows[0]["ReportName"].ToString(); }
        cViewer.LocalReport.DisplayName = reportName + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        dt = (new SqlReportSystem()).GetSqlReport(rptId, _GetReportHlp(rptId).Rows[0]["ProgramName"].ToString(), dvCri, base.LImpr, base.LCurr, UpdCriteria(rptId, false, false, criteria), LcAppConnString, LcAppPw, false, false, false);
        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource(LcAppDb, dt);
        cViewer.LocalReport.DataSources.Clear();
        cViewer.ShowRefreshButton = false;
        cViewer.LocalReport.DataSources.Add(rptDataSource);
        cViewer.LocalReport.Refresh();

        /* Skip the view mode */
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        string filename = string.Empty;
        byte[] bytes = cViewer.LocalReport.Render(fmt == "doc" ? "Word" : (fmt == "xls" ? "Excel" : fmt), null, out mimeType, out encoding, out extension, out streamids, out warnings);
        //filename = string.Format("{0}.{1}", cTitleLabel.Text, "pdf");
        filename = string.Format("{0}.{1}", reportName, "pdf");

        return bytes;
    }

    protected void CovertRptUTC(DataTable dt)
    {
        if (dt == null) return;

        List<int> ord = new List<int>();

        if (dt.Columns.Contains("ModifiedOn")) ord.Add(dt.Columns["ModifiedOn"].Ordinal);
        if (dt.Columns.Contains("InputOn")) ord.Add(dt.Columns["InputOn"].Ordinal);
        if (dt.Columns.Contains("UsageDt")) ord.Add(dt.Columns["UsageDt"].Ordinal);
        if (ord.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (int o in ord)
                {
                    dr[o] = SetDateTimzeLocal(dr[o].ToString(), false);
                }
            }
        }
    }

    protected DateTime SetDateTimzeLocal(string datetimeUTC, bool forceConvert)
    {
        DateTime d = Convert.ToDateTime(datetimeUTC, System.Threading.Thread.CurrentThread.CurrentCulture);
        if (d.Hour == 0 && d.Minute == 0 && d.Second == 0 && d.Millisecond == 0 && !forceConvert) return d;
        //TimeZoneInfo tzinfo = Session["Cache:tzInfo"] as TimeZoneInfo ?? TimeZoneInfo.Local;
        TimeZoneInfo tzinfo = TimeZoneInfo.Local;
        return TimeZoneInfo.ConvertTimeFromUtc(d, tzinfo);
    }

    private DataSet UpdCriteria(string rptId, bool bUpd, bool bMem, SerializableDictionary<string, string> criteria)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(MakeColumns(rptId, new DataTable("DtSqlReportIn")));
        DataRow dr = ds.Tables["DtSqlReportIn"].NewRow();
        DataView dvCri = new DataView(_GetReportCriteria(rptId));
        foreach (DataRowView drv in dvCri)
        {
            string criValue = string.Empty;
            criteria.TryGetValue(drv["ColumnName"].ToString(), out criValue);

            if (string.IsNullOrEmpty(criValue))
            {
                throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " is not found.");
            }

            if (drv["DisplayName"].ToString() == "ListBox")
            {
#if false
                bool bAll = false;
                cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
                if (cListBox != null)
                {
                    int CriCnt = (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw);
                    int TotalChoiceCnt = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), true, string.Empty, base.LImpr, base.LCurr, LcAppConnString, LcAppPw)).Count;
                    if (drv["DisplayMode"].ToString() == "AutoListBox")
                    {
                        TextBox tb = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Hidden");
                        var selected = tb != null ? tb.Text : null;
                        DataView dv = new DataView((new SqlReportSystem()).GetIn(QueryStr["rpt"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), false, selected, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                        FilterCriteriaDdl(cCriteria, dv, drv);
                        cListBox.DataSource = dv;
                        cListBox.DataBind();
                        GetSelectedItems(cListBox, selected);
                    }
                    string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
                    bool noneSelected = string.IsNullOrEmpty(selectedValues) || selectedValues == "''";
                    dr[drv["ColumnName"].ToString()] = "(";
                    foreach (ListItem li in cListBox.Items)
                    {
                        if (li.Selected || (noneSelected && CriCnt + 1 > TotalChoiceCnt))
                        {
                            if ((li.Value.ToString().Trim() == "" && li.Selected) || (noneSelected && CriCnt + 1 > TotalChoiceCnt)) { bAll = true; noneSelected = true; }
                            if (bAll || li.Selected)
                            {
                                if (dr[drv["ColumnName"].ToString()].ToString() != "(")
                                {
                                    dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ",";
                                }
                                dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'" + (li.Value.ToString().Trim() == string.Empty && CriCnt + 1 > TotalChoiceCnt ? "0" : li.Value.ToString().Trim()) + "'";
                            }
                        }
                    }
                }
                if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cListBox.SelectedValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }
                if (dr[drv["ColumnName"].ToString()].ToString() == "(''" || dr[drv["ColumnName"].ToString()].ToString() == "(") { dr[drv["ColumnName"].ToString()] = string.Empty; } else { dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ")"; }
#endif

                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }

                DataView dv = new DataView((new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), 0, drv["RequiredValid"].ToString(), true, criValue, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));

                if (!IsValidLs(dv, drv["DdlKeyColumnName"].ToString(), criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + ": '" + criValue + "' is not a valid value.");
                }

                dr[drv["ColumnName"].ToString()] = criValue;
            }
            else if (drv["DisplayName"].ToString() == "Calendar")
            {
                DateTime selectedDate = DateTime.MinValue;

                if (DateTime.TryParse(criValue, out selectedDate))
                {
                    dr[drv["ColumnName"].ToString()] = selectedDate;
                }
            }
            else if (drv["DisplayName"].ToString() == "ComboBox")
            {
                string val = null; //try { val = dt.Rows[ii]["LastCriteria"].ToString(); }
                val = criValue;
                //(new SqlReportSystem()).MkReportGetIn(GenPrefix, drv["ReportCriId"].ToString(), QueryStr["rpt"].ToString() + drv["ColumnName"].ToString(), LcAppDb, LcDesDb, LcSysConnString, LcAppPw);
                DataView dv = new DataView((new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, val, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));
                //FilterCriteriaDdl(cCriteria, dv, drv);

                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }

                if (!IsValidId(dv, drv["DdlKeyColumnName"].ToString(), criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + ": '" + criValue + "' is not a valid value.");
                }

                if (criValue != string.Empty)
                {
                    dr[drv["ColumnName"].ToString()] = criValue;
                }
            }
            else if (drv["DisplayName"].ToString() == "DropDownList")
            {
                string val = null; //try { val = dt.Rows[ii]["LastCriteria"].ToString(); }
                val = criValue;
                DataView dv = new DataView((new SqlReportSystem()).GetIn(rptId, rptId + drv["ColumnName"].ToString(), (new SqlReportSystem()).CountRptCri(drv["ReportCriId"].ToString(), LcSysConnString, LcAppPw), drv["RequiredValid"].ToString(), true, val, base.LImpr, base.LCurr, LcAppConnString, LcAppPw));

                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }

                if (!IsValidId(dv, drv["DdlKeyColumnName"].ToString(), criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + ": '" + criValue + "' is not a valid value.");
                }

                if (criValue != string.Empty)
                {
                    dr[drv["ColumnName"].ToString()] = criValue;
                }
            }
            else if (drv["DisplayName"].ToString() == "RadioButtonList")
            {
                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }

                if (criValue != string.Empty)
                {
                    dr[drv["ColumnName"].ToString()] = criValue;
                }
            }
            else if (drv["DisplayName"].ToString() == "CheckBox")
            {
                if (criValue != string.Empty)
                {
                    dr[drv["ColumnName"].ToString()] = criValue; //assume 'Y' or 'N'?
                }
            }
            else
            {
                if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(criValue))
                {
                    throw new ApplicationException("Criteria column: " + drv["ColumnName"].ToString() + " should not be empty. Please rectify and try again.");
                }

                if (criValue != string.Empty)
                {
                    dr[drv["ColumnName"].ToString()] = drv["DisplayMode"].ToString() == "DateUTC" ? SetDateTimeUTC(criValue, !bUpd) : criValue;
                }
            }
        }
        ds.Tables["DtSqlReportIn"].Rows.Add(dr);
        if (bUpd) { (new SqlReportSystem()).UpdSqlReport(rptId, _GetReportHlp(rptId).Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw); }
#if false
        if (bMem)
        {

            string GenStr = string.Empty; if (GenPrefix != string.Empty) {GenStr = "gen=Y&";}
            string MemId = (new SqlReportSystem()).MemSqlReport(cPublicCri.SelectedValue, cMemCriId.Value, cMemFldDdl.SelectedValue, cMemCriName.Text, cMemCriDesc.Text, "SqlReport.aspx?" + GenStr + "csy=" + LcSystemId.ToString() + "&typ=N&rpt=" + QueryStr["rpt"].ToString() + "&key=" + cMemCriId.Value, QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw);
            if (cMemCriId.Value == string.Empty)    // Make sure the link is properly updated.
            {
                cMemCriId.Value = (new SqlReportSystem()).MemSqlReport(cPublicCri.SelectedValue, MemId, cMemFldDdl.SelectedValue, cMemCriName.Text, cMemCriDesc.Text, "SqlReport.aspx?" + GenStr + "csy=" + LcSystemId.ToString() + "&typ=N&rpt=" + QueryStr["rpt"].ToString() + "&key=" + MemId, QueryStr["rpt"].ToString(), GetReportHlp().Rows[0]["ProgramName"].ToString(), dvCri, base.LUser.UsrId, ds, LcAppConnString, LcAppPw);
            }

        }
#endif
        return ds;
    }

#if false
    public void FilterCriteriaDdl(Control criContainer, DataView dv, DataRowView criDrv)
    {
        if (!string.IsNullOrEmpty(criDrv["DdlFtrColumnName"].ToString()) &&
            dv.Table.Columns.Contains(criDrv["DdlFtrColumnName"].ToString()))
        {
            string tabIndex = criDrv.Row.Table.Columns.Contains("DdlFtrColumnTabIndex") ? criDrv["DdlFtrColumnTabIndex"].ToString() : "";
            KeyValuePair<string, bool> filterColVal = GetCriteriaColumnValue(criContainer, "x" + criDrv["DdlFtrColumnName"].ToString() + tabIndex);
            string rowFilter = GetCriteriaRowFilter(dv.Table, criDrv["DdlFtrColumnName"].ToString(), filterColVal);
            dv.RowFilter = rowFilter;
        }
    }
#endif

    protected bool IsValidLs(DataView dv, string keyColumn, string valueLs)
    {
        valueLs = valueLs.Replace("(", "").Replace(")", "");

        string[] keys = valueLs.Split(new string[] { "," }, StringSplitOptions.None);
        int keysFound = 0;

        foreach(string key in keys)
        {
            foreach(DataRowView drv in dv)
            {
                if (drv[keyColumn].ToString() == key)
                {
                    keysFound++;
                    break;
                }
            }
        }

        return keys.Length == keysFound;
    }

    protected bool IsValidId(DataView dv, string keyColumn, string value)
    {
        bool bFound = false;

        foreach(DataRowView drv in dv)
        {
            if (drv[keyColumn].ToString() == value)
            {
                bFound = true;
                break;
            }
        }
        return bFound;
    }

    protected string SetDateTimeUTC(string datetime, bool forceConvert)
    {
        DateTime d = Convert.ToDateTime(datetime, System.Threading.Thread.CurrentThread.CurrentCulture);
        if (d.Hour == 0 && d.Minute == 0 && d.Second == 0 && d.Millisecond == 0 && !forceConvert) return datetime;
        //TimeZoneInfo tzinfo = Session["Cache:tzInfo"] as TimeZoneInfo ?? TimeZoneInfo.Local;
        TimeZoneInfo tzinfo = TimeZoneInfo.Utc;
        return TimeZoneInfo.ConvertTimeToUtc(d, tzinfo).ToString();
    }

    //private DataView GetSqlCriteria(string rptId)
    //{
    //    string GenPrefix = string.Empty;
    //    return (new SqlReportSystem()).GetReportCriteria(GenPrefix, rptId, LcSysConnString, LcAppPw).DefaultView;
    //}

    private DataView GetSqlColumns(string rptId)
    {
        string GenPrefix = string.Empty;
        return (new SqlReportSystem()).GetReportColumns(GenPrefix, rptId, LcSysConnString, LcAppPw).DefaultView;
    }

    private DataTable MakeColumns(string rptId, DataTable dt)
    {
        DataColumnCollection columns = dt.Columns;
        DataView dvCri = new DataView(_GetReportCriteria(rptId));
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

}
