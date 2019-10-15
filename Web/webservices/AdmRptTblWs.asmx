<%@ WebService Language="C#" Class="RO.Web.AdmRptTblWs" %>
namespace RO.Web

                {
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
            
                public class AdmRptTbl92 : DataSet
                {
                    public AdmRptTbl92()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmRptTbl")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptTblDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptTblAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptTblUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptTblDel")));
                        this.DataSetName = "AdmRptTbl92";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmRptTbl92";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("RptTblId162", typeof(string));
                        columns.Add("RptCtrId162", typeof(string));
                        columns.Add("ParentId162", typeof(string));
                        columns.Add("ReportId162", typeof(string));
                        columns.Add("TblToggle162", typeof(string));
                        columns.Add("TblGrouping162", typeof(string));
                        columns.Add("RptTblTypeCd162", typeof(string));
                        columns.Add("TblVisibility162", typeof(string));
                        columns.Add("TblRepeatNew162", typeof(string));
                        columns.Add("TblOrder162", typeof(string));
                        columns.Add("ColWidth162", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("RptTblId162", typeof(string));
                        columns.Add("RptCelId164", typeof(string));
                        columns.Add("RowNum164", typeof(string));
                        columns.Add("RowHeight164", typeof(string));
                        columns.Add("RowVisibility164", typeof(string));
                        columns.Add("CelNum164", typeof(string));
                        columns.Add("CelColSpan164", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmRptTblWs : RO.Web.AsmxBase
            {
                const int screenId = 92;
                const byte systemId = 3;
                const string programName = "AdmRptTbl92";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmRptTbl92"; }
                protected override string GetMstTableName(bool underlying = true) { return "RptTbl"; }
                protected override string GetDtlTableName(bool underlying = true) { return "RptCel"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "RptTblId" : "RptTblId162"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "RptCelId" : "RptCelId164"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"RptCtrId162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptCtrId3S1610"},{"mKey","RptCtrId162"},{"mVal","RptCtrId162Text"}, }},
{"ParentId162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlParentId3S1609"},{"mKey","ParentId162"},{"mVal","ParentId162Text"}, }},
{"ReportId162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1695"},{"mKey","ReportId162"},{"mVal","ReportId162Text"}, }},
{"TblToggle162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTblToggle3S1696"},{"mKey","TblToggle162"},{"mVal","TblToggle162Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId162"}}},
{"TblGrouping162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTblGrouping3S1615"},{"mKey","TblGrouping162"},{"mVal","TblGrouping162Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId162"}}},
{"RptTblTypeCd162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptTblTypeCd3S1611"},{"mKey","RptTblTypeCd162"},{"mVal","RptTblTypeCd162Text"}, }},
{"TblVisibility162", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTblVisibility3S1617"},{"mKey","TblVisibility162"},{"mVal","TblVisibility162Text"}, }},
{"CelNum164", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCelNum3S1624"},{"mKey","CelNum164"},{"mVal","CelNum164Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["RptTblId162"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["RptCelId164"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["RowNum164"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["RowHeight164"] = System.Data.OleDb.OleDbType.Decimal.ToString();
dr["RowVisibility164"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["CelNum164"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["CelColSpan164"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["RptTblId162"] = "TextBox";dr["RptCelId164"] = "TextBox";
dr["RowNum164"] = "TextBox";
dr["RowHeight164"] = "TextBox";
dr["RowVisibility164"] = "TextBox";
dr["CelNum164"] = "AutoComplete";
dr["CelColSpan164"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["RptTblId162"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["RptCelId164"] = (drv["RptCelId164"] ?? "").ToString().Trim().Left(9999999);
dr["RowNum164"] = (drv["RowNum164"] ?? "").ToString().Trim().Left(9999999);
dr["RowHeight164"] = (drv["RowHeight164"] ?? "").ToString().Trim().Left(9999999);
dr["RowVisibility164"] = (drv["RowVisibility164"] ?? "").ToString().Trim().Left(1000);
dr["CelNum164"] = drv["CelNum164"];
dr["CelColSpan164"] = (drv["CelColSpan164"] ?? "").ToString().Trim().Left(9999999);

                    }
                    return dr;
                }

                private AdmRptTbl92 PrepAdmRptTblData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmRptTbl92 ds = new AdmRptTbl92();
                    DataRow dr = ds.Tables["AdmRptTbl"].NewRow();
                    DataRow drType = ds.Tables["AdmRptTbl"].NewRow();
                    DataRow drDisp = ds.Tables["AdmRptTbl"].NewRow();
            if (bAdd) { dr["RptTblId162"] = string.Empty; } else { dr["RptTblId162"] = mst["RptTblId162"]; }
drType["RptTblId162"] = "Numeric"; drDisp["RptTblId162"] = "TextBox";
try { dr["RptCtrId162"] = mst["RptCtrId162"]; } catch { }
drType["RptCtrId162"] = "Numeric"; drDisp["RptCtrId162"] = "AutoComplete";
try { dr["ParentId162"] = mst["ParentId162"]; } catch { }
drType["ParentId162"] = "Numeric"; drDisp["ParentId162"] = "AutoComplete";
try { dr["ReportId162"] = mst["ReportId162"]; } catch { }
drType["ReportId162"] = "Numeric"; drDisp["ReportId162"] = "AutoComplete";
try { dr["TblToggle162"] = mst["TblToggle162"]; } catch { }
drType["TblToggle162"] = "Numeric"; drDisp["TblToggle162"] = "AutoComplete";
try { dr["TblGrouping162"] = mst["TblGrouping162"]; } catch { }
drType["TblGrouping162"] = "Numeric"; drDisp["TblGrouping162"] = "AutoComplete";
try { dr["RptTblTypeCd162"] = mst["RptTblTypeCd162"]; } catch { }
drType["RptTblTypeCd162"] = "Char"; drDisp["RptTblTypeCd162"] = "DropDownList";
try { dr["TblVisibility162"] = mst["TblVisibility162"]; } catch { }
drType["TblVisibility162"] = "Char"; drDisp["TblVisibility162"] = "RadioButtonList";
try { dr["TblRepeatNew162"] = (mst["TblRepeatNew162"] ?? "").Trim().Left(1); } catch { }
drType["TblRepeatNew162"] = "Char"; drDisp["TblRepeatNew162"] = "CheckBox";
try { dr["TblOrder162"] = (mst["TblOrder162"] ?? "").Trim().Left(9999999); } catch { }
drType["TblOrder162"] = "Numeric"; drDisp["TblOrder162"] = "TextBox";
try { dr["ColWidth162"] = (mst["ColWidth162"] ?? "").Trim().Left(9999999); } catch { }
drType["ColWidth162"] = "Decimal"; drDisp["ColWidth162"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmRptTblDef"].Rows.Add(MakeTypRow(ds.Tables["AdmRptTblDef"].NewRow()));
                        ds.Tables["AdmRptTblDef"].Rows.Add(MakeDisRow(ds.Tables["AdmRptTblDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmRptTblAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptTblAdd"].NewRow(), drv, mst["RptTblId162"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["RptCelId164"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmRptTblUpd"].Rows.Add(MakeColRow(ds.Tables["AdmRptTblUpd"].NewRow(), drv, mst["RptTblId162"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["RptCelId164"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmRptTblAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptTblAdd"].NewRow(), drv, mst["RptTblId162"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["RptCelId164"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmRptTblDel"].Rows.Add(MakeColRow(ds.Tables["AdmRptTblDel"].NewRow(), drv, mst["RptTblId162"], false));
                            }
                        }
                    }
                    ds.Tables["AdmRptTbl"].Rows.Add(dr); ds.Tables["AdmRptTbl"].Rows.Add(drType); ds.Tables["AdmRptTbl"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptTblId162",""},
{"RptCtrId162",""},
{"ParentId162",""},
{"ReportId162",""},
{"TblToggle162",""},
{"TblGrouping162",""},
{"RptTblTypeCd162",""},
{"TblVisibility162",""},
{"TblRepeatNew162",""},
{"TblOrder162",""},
{"ColWidth162",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptCelId164",""},
{"RowNum164",""},
{"RowHeight164",""},
{"RowVisibility164",""},
{"CelNum164",""},
{"CelColSpan164",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptTbl92List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmRptTbl92";
                    context["mKey"] = "RptTblId162";
                    context["mVal"] = "RptTblId162Text";
                    context["mTip"] = "RptTblId162Text";
                    context["mImg"] = "RptTblId162Text";
                    context["ssd"] = "1";
                    context["scr"] = screenId.ToString();
                    context["csy"] = systemId.ToString();
                    context["filter"] = filterId;
                    context["isSys"] = "N";
                    context["conn"] = string.Empty;
                    AutoCompleteResponse r = LisSuggests(searchStr, jss.Serialize(context), topN);
                    ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.errorMsg = "";
                    mr.data = r;
                    mr.status = "success";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptTbl92ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmRptTbl92", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    DataTable dt = _GetMstById(keyId);
                    DataTable dtColAuth = _GetAuthCol(GetScreenId());
                    Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = DataTableToListOfObject(dt, false, colAuth);
                    mr.status = "success";
                    mr.errorMsg = "";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptTbl92DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmRptTbl92", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(92, "GetAdmRptTbl92DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
mr.data = DataTableToListOfObject(dt, false, colAuth);
                    mr.status = "success";
                    mr.errorMsg = "";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewMst()
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var mst = InitMaster();
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = new List<SerializableDictionary<string, string>>() { mst };
                    mr.status = "success";
                    mr.errorMsg = "";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
                return ret;
            }
            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewDtl()
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var dtl = InitDtl();
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = new List<SerializableDictionary<string, string>>() { dtl };
                    mr.status = "success";
                    mr.errorMsg = "";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
                return ret;
            }

            protected override DataTable _GetMstById(string mstId)
            {
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmRptTbl92ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmRptTbl92DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

            }
            protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
            {
                return ddlContext;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMst(SerializableDictionary<string, string> mst, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var pid = mst["RptTblId162"];
                    var ds = PrepAdmRptTblData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["RptTblId162"]));
                    (new RO.Access3.AdminAccess()).DelData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                    ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    SaveDataResponse result = new SaveDataResponse();
                    string msg = _GetScreenHlp(screenId).Rows[0]["DelMsg"].ToString();
                    result.message = msg;
                    mr.status = "success";
                    mr.errorMsg = "";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "D", null));
                return ret;

            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> SaveData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    //throw new Exception("aaa");
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    SerializableDictionary<string, string> skipValidation = new SerializableDictionary<string, string>(){ { "SkipAllMst", "SilentColReadOnly" }, { "SkipAllDtl", "SilentColReadOnly" } };
                    /* AsmxRule: Save Data Before */


                    /* AsmxRule End: Save Data Before */

                    var pid = mst["RptTblId162"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmRptTbl92", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["RptCelId164"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "RptTblId162", "RptCelId164", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmRptTblData(mst, dtl, string.IsNullOrEmpty(mst["RptTblId162"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["RptTblId162"]))
                    {
                        pid = (new RO.Access3.AdminAccess()).AddData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                        if (!string.IsNullOrEmpty(pid))
                        {
                            msg = _GetScreenHlp(screenId).Rows[0]["AddMsg"].ToString();
                        }
                    }
                    else
                    {
                        bool ok = (new RO.Access3.AdminAccess()).UpdData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                        if (ok)
                        {
                            msg = _GetScreenHlp(screenId).Rows[0]["UpdMsg"].ToString();
                        }
                    }

                    /* read updated records */
                    dtMst = _GetMstById(pid);
                        dtDtl = _GetDtlById(pid, 0);
                     foreach (var x in dtl){
                        
                     }
                     /* AsmxRule: Save Data After */


                    /* AsmxRule End: Save Data After */

                    ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    SaveDataResponse result = new SaveDataResponse();
                    DataTable dtColAuth = _GetAuthCol(GetScreenId());
                    Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());

                    result.mst = DataTableToListOfObject(dtMst, false, colAuth)[0];
                        result.dtl = DataTableToListOfObject(dtDtl, false, colAuth);
                    
                    result.message = msg;
                    mr.status = "success";
                    mr.errorMsg = "";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "S", null));
                return ret;
            }
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptCtrId162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlRptCtrId3S1610";context["addnew"] = "Y";context["mKey"] = "RptCtrId162";context["mVal"] = "RptCtrId162Text";context["mTip"] = "RptCtrId162Text";context["mImg"] = "RptCtrId162Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptCtrId162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetParentId162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlParentId3S1609";context["addnew"] = "Y";context["mKey"] = "ParentId162";context["mVal"] = "ParentId162Text";context["mTip"] = "ParentId162Text";context["mImg"] = "ParentId162Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ParentId162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlReportId3S1695";context["addnew"] = "Y";context["mKey"] = "ReportId162";context["mVal"] = "ReportId162Text";context["mTip"] = "ReportId162Text";context["mImg"] = "ReportId162Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTblToggle162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlTblToggle3S1696";context["addnew"] = "Y";context["mKey"] = "TblToggle162";context["mVal"] = "TblToggle162Text";context["mTip"] = "TblToggle162Text";context["mImg"] = "TblToggle162Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "ReportId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TblToggle162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTblGrouping162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlTblGrouping3S1615";context["addnew"] = "Y";context["mKey"] = "TblGrouping162";context["mVal"] = "TblGrouping162Text";context["mTip"] = "TblGrouping162Text";context["mImg"] = "TblGrouping162Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "ReportId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TblGrouping162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptTblTypeCd162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlRptTblTypeCd3S1611", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptTblTypeCd162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTblVisibility162List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlTblVisibility3S1617", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TblVisibility162", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCelNum164List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCelNum3S1624";context["addnew"] = "Y";context["mKey"] = "CelNum164";context["mVal"] = "CelNum164Text";context["mTip"] = "CelNum164Text";context["mImg"] = "CelNum164Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CelNum164", emptyAutoCompleteResponse));return ret;}
            [WebMethod(EnableSession = false)]
            public ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> LoadInitPage(SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var dtAuthCol = _GetAuthCol(screenId);
                    var dtAuthRow = _GetAuthRow(screenId);
                    var dtScreenLabel = _GetScreenLabel(screenId);
                    var dtScreenCriteria = _GetScrCriteria(screenId);
                    var dtScreenFilter = _GetScreenFilter(screenId);
                    var dtScreenHlp = _GetScreenHlp(screenId);
                    var dtScreenButtonHlp = _GetScreenButtonHlp(screenId);
                    var dtLabel = _GetLabels("AdmRptTbl");
                    var SearchList = GetAdmRptTbl92List("", 0, "");
                                    var RptCtrId162LIst = GetRptCtrId162List("", 0, "");
                        var ParentId162LIst = GetParentId162List("", 0, "");
                        var ReportId162LIst = GetReportId162List("", 0, "");
                        var TblToggle162LIst = GetTblToggle162List("", 0, "");
                        var TblGrouping162LIst = GetTblGrouping162List("", 0, "");
                        var RptTblTypeCd162LIst = GetRptTblTypeCd162List("", 0, "");
                        var TblVisibility162LIst = GetTblVisibility162List("", 0, "");
                        var CelNum164LIst = GetCelNum164List("", 0, "");

                    LoadScreenPageResponse result = new LoadScreenPageResponse();

                    ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.status = "success";
                    mr.errorMsg = "";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "S", null));
                return ret;
            }           
            
/* AsmxRule: Custom Function */


             /* AsmxRule End: Custom Function */
           
            }
        }
            