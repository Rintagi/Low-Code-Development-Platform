<%@ WebService Language="C#" Class="RO.Web.AdmRowOvrdWs" %>
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
            
                public class AdmRowOvrd17 : DataSet
                {
                    public AdmRowOvrd17()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmRowOvrd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRowOvrdDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRowOvrdAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRowOvrdUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRowOvrdDel")));
                        this.DataSetName = "AdmRowOvrd17";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmRowOvrd17";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("RowOvrdId238", typeof(string));
                        columns.Add("ScreenId238", typeof(string));
                        columns.Add("ReportId238", typeof(string));
                        columns.Add("RowAuthId238", typeof(string));
                        columns.Add("AllowSel238", typeof(string));
                        columns.Add("AllowAdd238", typeof(string));
                        columns.Add("AllowUpd238", typeof(string));
                        columns.Add("AllowDel238", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("RowOvrdId238", typeof(string));
                        columns.Add("RowOvrdPrmId239", typeof(string));
                        columns.Add("AndCondition239", typeof(string));
                        columns.Add("PermKeyId239", typeof(string));
                        columns.Add("SelLevel239", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmRowOvrdWs : RO.Web.AsmxBase
            {
                const int screenId = 17;
                const byte systemId = 3;
                const string programName = "AdmRowOvrd17";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmRowOvrd17"; }
                protected override string GetMstTableName(bool underlying = true) { return "RowOvrd"; }
                protected override string GetDtlTableName(bool underlying = true) { return "RowOvrdPrm"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "RowOvrdId" : "RowOvrdId238"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "RowOvrdPrmId" : "RowOvrdPrmId239"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"ScreenId238", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S188"},{"mKey","ScreenId238"},{"mVal","ScreenId238Text"}, }},
{"ReportId238", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1750"},{"mKey","ReportId238"},{"mVal","ReportId238Text"}, }},
{"RowAuthId238", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRowAuthId3S200"},{"mKey","RowAuthId238"},{"mVal","RowAuthId238Text"}, }},
{"AllowSel238", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAllowSel3S201"},{"mKey","AllowSel238"},{"mVal","AllowSel238Text"}, }},
{"AndCondition239", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAndCondition3S1981"},{"mKey","AndCondition239"},{"mVal","AndCondition239Text"}, }},
{"PermKeyId239", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlPermKeyId3S1882"},{"mKey","PermKeyId239"},{"mVal","PermKeyId239Text"}, }},
{"SelLevel239", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSelLevel3S1308"},{"mKey","SelLevel239"},{"mVal","SelLevel239Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["RowOvrdId238"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["RowOvrdPrmId239"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["AndCondition239"] = System.Data.OleDb.OleDbType.Char.ToString();
dr["PermKeyId239"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["SelLevel239"] = System.Data.OleDb.OleDbType.Char.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["RowOvrdId238"] = "TextBox";dr["RowOvrdPrmId239"] = "TextBox";
dr["AndCondition239"] = "DropDownList";
dr["PermKeyId239"] = "DropDownList";
dr["SelLevel239"] = "DropDownList";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["RowOvrdId238"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["RowOvrdPrmId239"] = (drv["RowOvrdPrmId239"] ?? "").ToString().Trim().Left(9999999);
dr["AndCondition239"] = drv["AndCondition239"];
dr["PermKeyId239"] = drv["PermKeyId239"];
dr["SelLevel239"] = drv["SelLevel239"];

                    }
                    return dr;
                }

                private AdmRowOvrd17 PrepAdmRowOvrdData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmRowOvrd17 ds = new AdmRowOvrd17();
                    DataRow dr = ds.Tables["AdmRowOvrd"].NewRow();
                    DataRow drType = ds.Tables["AdmRowOvrd"].NewRow();
                    DataRow drDisp = ds.Tables["AdmRowOvrd"].NewRow();
            if (bAdd) { dr["RowOvrdId238"] = string.Empty; } else { dr["RowOvrdId238"] = mst["RowOvrdId238"]; }
drType["RowOvrdId238"] = "Numeric"; drDisp["RowOvrdId238"] = "TextBox";
try { dr["ScreenId238"] = mst["ScreenId238"]; } catch { }
drType["ScreenId238"] = "Numeric"; drDisp["ScreenId238"] = "AutoComplete";
try { dr["ReportId238"] = mst["ReportId238"]; } catch { }
drType["ReportId238"] = "Numeric"; drDisp["ReportId238"] = "AutoComplete";
try { dr["RowAuthId238"] = mst["RowAuthId238"]; } catch { }
drType["RowAuthId238"] = "Numeric"; drDisp["RowAuthId238"] = "DropDownList";
try { dr["AllowSel238"] = mst["AllowSel238"]; } catch { }
drType["AllowSel238"] = "Char"; drDisp["AllowSel238"] = "DropDownList";
try { dr["AllowAdd238"] = (mst["AllowAdd238"] ?? "").Trim().Left(1); } catch { }
drType["AllowAdd238"] = "Char"; drDisp["AllowAdd238"] = "CheckBox";
try { dr["AllowUpd238"] = (mst["AllowUpd238"] ?? "").Trim().Left(1); } catch { }
drType["AllowUpd238"] = "Char"; drDisp["AllowUpd238"] = "CheckBox";
try { dr["AllowDel238"] = (mst["AllowDel238"] ?? "").Trim().Left(1); } catch { }
drType["AllowDel238"] = "Char"; drDisp["AllowDel238"] = "CheckBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmRowOvrdDef"].Rows.Add(MakeTypRow(ds.Tables["AdmRowOvrdDef"].NewRow()));
                        ds.Tables["AdmRowOvrdDef"].Rows.Add(MakeDisRow(ds.Tables["AdmRowOvrdDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmRowOvrdAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRowOvrdAdd"].NewRow(), drv, mst["RowOvrdId238"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["RowOvrdPrmId239"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmRowOvrdUpd"].Rows.Add(MakeColRow(ds.Tables["AdmRowOvrdUpd"].NewRow(), drv, mst["RowOvrdId238"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["RowOvrdPrmId239"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmRowOvrdAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRowOvrdAdd"].NewRow(), drv, mst["RowOvrdId238"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["RowOvrdPrmId239"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmRowOvrdDel"].Rows.Add(MakeColRow(ds.Tables["AdmRowOvrdDel"].NewRow(), drv, mst["RowOvrdId238"], false));
                            }
                        }
                    }
                    ds.Tables["AdmRowOvrd"].Rows.Add(dr); ds.Tables["AdmRowOvrd"].Rows.Add(drType); ds.Tables["AdmRowOvrd"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RowOvrdId238",""},
{"ScreenId238",""},
{"ReportId238",""},
{"RowAuthId238",""},
{"AllowSel238","S"},
{"AllowAdd238",""},
{"AllowUpd238",""},
{"AllowDel238",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RowOvrdPrmId239",""},
{"AndCondition239","Y"},
{"PermKeyId239",""},
{"SelLevel239","S"},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRowOvrd17List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmRowOvrd17";
                    context["mKey"] = "RowOvrdId238";
                    context["mVal"] = "RowOvrdId238Text";
                    context["mTip"] = "RowOvrdId238Text";
                    context["mImg"] = "RowOvrdId238Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRowOvrd17ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmRowOvrd17", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRowOvrd17DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmRowOvrd17", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(17, "GetAdmRowOvrd17DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmRowOvrd17ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmRowOvrd17DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["RowOvrdId238"];
                    var ds = PrepAdmRowOvrdData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["RowOvrdId238"]));
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

                    var pid = mst["RowOvrdId238"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmRowOvrd17", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["RowOvrdPrmId239"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "RowOvrdId238", "RowOvrdPrmId239", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmRowOvrdData(mst, dtl, string.IsNullOrEmpty(mst["RowOvrdId238"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["RowOvrdId238"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId238List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlScreenId3S188";context["addnew"] = "Y";context["mKey"] = "ScreenId238";context["mVal"] = "ScreenId238Text";context["mTip"] = "ScreenId238Text";context["mImg"] = "ScreenId238Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId238", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId238List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlReportId3S1750";context["addnew"] = "Y";context["mKey"] = "ReportId238";context["mVal"] = "ReportId238Text";context["mTip"] = "ReportId238Text";context["mImg"] = "ReportId238Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId238", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRowAuthId238List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlRowAuthId3S200", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RowAuthId238", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAllowSel238List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlAllowSel3S201", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AllowSel238", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAndCondition239List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlAndCondition3S1981", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AndCondition239", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetPermKeyId239List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlPermKeyId3S1882", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "PermKeyId239", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSelLevel239List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSelLevel3S1308", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SelLevel239", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmRowOvrd");
                    var SearchList = GetAdmRowOvrd17List("", 0, "");
                                    var ScreenId238LIst = GetScreenId238List("", 0, "");
                        var ReportId238LIst = GetReportId238List("", 0, "");
                        var RowAuthId238LIst = GetRowAuthId238List("", 0, "");
                        var AllowSel238LIst = GetAllowSel238List("", 0, "");
                        var AndCondition239LIst = GetAndCondition239List("", 0, "");
                        var PermKeyId239LIst = GetPermKeyId239List("", 0, "");
                        var SelLevel239LIst = GetSelLevel239List("", 0, "");

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
            