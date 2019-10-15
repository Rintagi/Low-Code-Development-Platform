<%@ WebService Language="C#" Class="RO.Web.AdmMenuHlpWs" %>
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
            
                public class AdmMenuHlp36 : DataSet
                {
                    public AdmMenuHlp36()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmMenuHlp")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmMenuHlpDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmMenuHlpAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmMenuHlpUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmMenuHlpDel")));
                        this.DataSetName = "AdmMenuHlp36";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmMenuHlp36";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("MenuId39", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("MenuId39", typeof(string));
                        columns.Add("MenuHlpId40", typeof(string));
                        columns.Add("CultureId40", typeof(string));
                        columns.Add("MenuText40", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmMenuHlpWs : RO.Web.AsmxBase
            {
                const int screenId = 36;
                const byte systemId = 3;
                const string programName = "AdmMenuHlp36";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmMenuHlp36"; }
                protected override string GetMstTableName(bool underlying = true) { return "Menu"; }
                protected override string GetDtlTableName(bool underlying = true) { return "MenuHlp"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "MenuId" : "MenuId39"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "MenuHlpId" : "MenuHlpId40"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"CultureId40", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S448"},{"mKey","CultureId40"},{"mVal","CultureId40Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["MenuId39"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["MenuHlpId40"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["CultureId40"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["MenuText40"] = System.Data.OleDb.OleDbType.VarWChar.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["MenuId39"] = "TextBox";dr["MenuHlpId40"] = "TextBox";
dr["CultureId40"] = "AutoComplete";
dr["MenuText40"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["MenuId39"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["MenuHlpId40"] = (drv["MenuHlpId40"] ?? "").ToString().Trim().Left(9999999);
dr["CultureId40"] = drv["CultureId40"];
dr["MenuText40"] = (drv["MenuText40"] ?? "").ToString().Trim().Left(50);

                    }
                    return dr;
                }

                private AdmMenuHlp36 PrepAdmMenuHlpData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmMenuHlp36 ds = new AdmMenuHlp36();
                    DataRow dr = ds.Tables["AdmMenuHlp"].NewRow();
                    DataRow drType = ds.Tables["AdmMenuHlp"].NewRow();
                    DataRow drDisp = ds.Tables["AdmMenuHlp"].NewRow();
            if (bAdd) { dr["MenuId39"] = string.Empty; } else { dr["MenuId39"] = mst["MenuId39"]; }
drType["MenuId39"] = "Numeric"; drDisp["MenuId39"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmMenuHlpDef"].Rows.Add(MakeTypRow(ds.Tables["AdmMenuHlpDef"].NewRow()));
                        ds.Tables["AdmMenuHlpDef"].Rows.Add(MakeDisRow(ds.Tables["AdmMenuHlpDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmMenuHlpAdd"].Rows.Add(MakeColRow(ds.Tables["AdmMenuHlpAdd"].NewRow(), drv, mst["MenuId39"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["MenuHlpId40"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmMenuHlpUpd"].Rows.Add(MakeColRow(ds.Tables["AdmMenuHlpUpd"].NewRow(), drv, mst["MenuId39"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["MenuHlpId40"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmMenuHlpAdd"].Rows.Add(MakeColRow(ds.Tables["AdmMenuHlpAdd"].NewRow(), drv, mst["MenuId39"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["MenuHlpId40"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmMenuHlpDel"].Rows.Add(MakeColRow(ds.Tables["AdmMenuHlpDel"].NewRow(), drv, mst["MenuId39"], false));
                            }
                        }
                    }
                    ds.Tables["AdmMenuHlp"].Rows.Add(dr); ds.Tables["AdmMenuHlp"].Rows.Add(drType); ds.Tables["AdmMenuHlp"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"MenuId39",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"MenuHlpId40",""},
{"CultureId40",""},
{"MenuText40",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmMenuHlp36List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmMenuHlp36";
                    context["mKey"] = "MenuId39";
                    context["mVal"] = "MenuId39Text";
                    context["mTip"] = "MenuId39Text";
                    context["mImg"] = "MenuId39Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmMenuHlp36ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmMenuHlp36", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmMenuHlp36DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmMenuHlp36", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(36, "GetAdmMenuHlp36DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmMenuHlp36ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmMenuHlp36DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["MenuId39"];
                    var ds = PrepAdmMenuHlpData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["MenuId39"]));
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

                    var pid = mst["MenuId39"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmMenuHlp36", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["MenuHlpId40"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "MenuId39", "MenuHlpId40", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmMenuHlpData(mst, dtl, string.IsNullOrEmpty(mst["MenuId39"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["MenuId39"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId40List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCultureId3S448";context["addnew"] = "Y";context["mKey"] = "CultureId40";context["mVal"] = "CultureId40Text";context["mTip"] = "CultureId40Text";context["mImg"] = "CultureId40Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId40", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmMenuHlp");
                    var SearchList = GetAdmMenuHlp36List("", 0, "");
                                    var CultureId40LIst = GetCultureId40List("", 0, "");

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
            