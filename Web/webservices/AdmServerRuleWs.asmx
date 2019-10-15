<%@ WebService Language="C#" Class="RO.Web.AdmServerRuleWs" %>
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
            
                public class AdmServerRule14 : DataSet
                {
                    public AdmServerRule14()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmServerRule")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleDel")));
                        this.DataSetName = "AdmServerRule14";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmServerRule14";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("ServerRuleId24", typeof(string));
                        columns.Add("RuleName24", typeof(string));
                        columns.Add("RuleDescription24", typeof(string));
                        columns.Add("RuleTypeId24", typeof(string));
                        columns.Add("ScreenId24", typeof(string));
                        columns.Add("RuleOrder24", typeof(string));
                        columns.Add("ProcedureName24", typeof(string));
                        columns.Add("ParameterNames24", typeof(string));
                        columns.Add("ParameterTypes24", typeof(string));
                        columns.Add("CallingParams24", typeof(string));
                        columns.Add("MasterTable24", typeof(string));
                        columns.Add("OnAdd24", typeof(string));
                        columns.Add("OnUpd24", typeof(string));
                        columns.Add("OnDel24", typeof(string));
                        columns.Add("BeforeCRUD24", typeof(string));
                        columns.Add("CrudTypeDesc1289", typeof(string));
                        columns.Add("RuleCode24", typeof(string));
                        columns.Add("ModifiedBy24", typeof(string));
                        columns.Add("LastGenDt24", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("ServerRuleId24", typeof(string));

                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmServerRuleWs : RO.Web.AsmxBase
            {
                const int screenId = 14;
                const byte systemId = 3;
                const string programName = "AdmServerRule14";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmServerRule14"; }
                protected override string GetMstTableName(bool underlying = true) { return "ServerRule"; }
                protected override string GetDtlTableName(bool underlying = true) { return ""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ServerRuleId" : "ServerRuleId24"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"RuleTypeId24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleTypeId3S151"},{"mKey","RuleTypeId24"},{"mVal","RuleTypeId24Text"}, }},
{"ScreenId24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S139"},{"mKey","ScreenId24"},{"mVal","ScreenId24Text"}, }},
{"BeforeCRUD24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBeforeCRUD3S163"},{"mKey","BeforeCRUD24"},{"mVal","BeforeCRUD24Text"}, }},
{"ModifiedBy24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlModifiedBy3S1397"},{"mKey","ModifiedBy24"},{"mVal","ModifiedBy24Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["ServerRuleId24"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["ServerRuleId24"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["ServerRuleId24"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            

                    }
                    return dr;
                }

                private AdmServerRule14 PrepAdmServerRuleData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmServerRule14 ds = new AdmServerRule14();
                    DataRow dr = ds.Tables["AdmServerRule"].NewRow();
                    DataRow drType = ds.Tables["AdmServerRule"].NewRow();
                    DataRow drDisp = ds.Tables["AdmServerRule"].NewRow();
            if (bAdd) { dr["ServerRuleId24"] = string.Empty; } else { dr["ServerRuleId24"] = mst["ServerRuleId24"]; }
drType["ServerRuleId24"] = "Numeric"; drDisp["ServerRuleId24"] = "TextBox";
try { dr["RuleName24"] = (mst["RuleName24"] ?? "").Trim().Left(100); } catch { }
drType["RuleName24"] = "VarWChar"; drDisp["RuleName24"] = "TextBox";
try { dr["RuleDescription24"] = mst["RuleDescription24"]; } catch { }
drType["RuleDescription24"] = "VarWChar"; drDisp["RuleDescription24"] = "MultiLine";
try { dr["RuleTypeId24"] = mst["RuleTypeId24"]; } catch { }
drType["RuleTypeId24"] = "Numeric"; drDisp["RuleTypeId24"] = "DropDownList";
try { dr["ScreenId24"] = mst["ScreenId24"]; } catch { }
drType["ScreenId24"] = "Numeric"; drDisp["ScreenId24"] = "AutoComplete";
try { dr["RuleOrder24"] = (mst["RuleOrder24"] ?? "").Trim().Left(9999999); } catch { }
drType["RuleOrder24"] = "Numeric"; drDisp["RuleOrder24"] = "TextBox";
try { dr["ProcedureName24"] = (mst["ProcedureName24"] ?? "").Trim().Left(50); } catch { }
drType["ProcedureName24"] = "VarChar"; drDisp["ProcedureName24"] = "TextBox";
try { dr["ParameterNames24"] = (mst["ParameterNames24"] ?? "").Trim().Left(0); } catch { }
drType["ParameterNames24"] = "VarChar"; drDisp["ParameterNames24"] = "TextBox";
try { dr["ParameterTypes24"] = (mst["ParameterTypes24"] ?? "").Trim().Left(0); } catch { }
drType["ParameterTypes24"] = "VarChar"; drDisp["ParameterTypes24"] = "TextBox";
try { dr["CallingParams24"] = (mst["CallingParams24"] ?? "").Trim().Left(0); } catch { }
drType["CallingParams24"] = "VarChar"; drDisp["CallingParams24"] = "TextBox";
try { dr["MasterTable24"] = (mst["MasterTable24"] ?? "").Trim().Left(1); } catch { }
drType["MasterTable24"] = "Char"; drDisp["MasterTable24"] = "CheckBox";
try { dr["OnAdd24"] = (mst["OnAdd24"] ?? "").Trim().Left(1); } catch { }
drType["OnAdd24"] = "Char"; drDisp["OnAdd24"] = "CheckBox";
try { dr["OnUpd24"] = (mst["OnUpd24"] ?? "").Trim().Left(1); } catch { }
drType["OnUpd24"] = "Char"; drDisp["OnUpd24"] = "CheckBox";
try { dr["OnDel24"] = (mst["OnDel24"] ?? "").Trim().Left(1); } catch { }
drType["OnDel24"] = "Char"; drDisp["OnDel24"] = "CheckBox";
try { dr["BeforeCRUD24"] = mst["BeforeCRUD24"]; } catch { }
drType["BeforeCRUD24"] = "Char"; drDisp["BeforeCRUD24"] = "DropDownList";
try { dr["CrudTypeDesc1289"] = mst["CrudTypeDesc1289"]; } catch { }
drType["CrudTypeDesc1289"] = "VarWChar"; drDisp["CrudTypeDesc1289"] = "Label";
try { dr["RuleCode24"] = mst["RuleCode24"]; } catch { }
drType["RuleCode24"] = "VarWChar"; drDisp["RuleCode24"] = "MultiLine";


try { dr["ModifiedBy24"] = mst["ModifiedBy24"]; } catch { }
drType["ModifiedBy24"] = "Numeric"; drDisp["ModifiedBy24"] = "DropDownList";
try { dr["LastGenDt24"] = (mst["LastGenDt24"] ?? "").Trim().Left(9999999); } catch { }
drType["LastGenDt24"] = "DBTimeStamp"; drDisp["LastGenDt24"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmServerRuleDef"].Rows.Add(MakeTypRow(ds.Tables["AdmServerRuleDef"].NewRow()));
                        ds.Tables["AdmServerRuleDef"].Rows.Add(MakeDisRow(ds.Tables["AdmServerRuleDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmServerRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleAdd"].NewRow(), drv, mst["ServerRuleId24"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmServerRuleUpd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleUpd"].NewRow(), drv, mst["ServerRuleId24"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmServerRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleAdd"].NewRow(), drv, mst["ServerRuleId24"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmServerRuleDel"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleDel"].NewRow(), drv, mst["ServerRuleId24"], false));
                            }
                        }
                    }
                    ds.Tables["AdmServerRule"].Rows.Add(dr); ds.Tables["AdmServerRule"].Rows.Add(drType); ds.Tables["AdmServerRule"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ServerRuleId24",""},
{"RuleName24",""},
{"RuleDescription24",""},
{"RuleTypeId24",""},
{"ScreenId24",""},
{"RuleOrder24",""},
{"ProcedureName24",""},
{"ParameterNames24",""},
{"ParameterTypes24",""},
{"CallingParams24",""},
{"MasterTable24",""},
{"OnAdd24",""},
{"OnUpd24",""},
{"OnDel24",""},
{"BeforeCRUD24",""},
{"CrudTypeDesc1289",""},
{"RuleCode24",""},
{"SyncByDb","~/images/custom/adm/SyncByDb.gif"},
{"SyncToDb","~/images/custom/adm/SyncToDb.gif"},
{"ModifiedBy24",base.LUser.UsrId.ToString()},
{"LastGenDt24",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmServerRule14";
                    context["mKey"] = "ServerRuleId24";
                    context["mVal"] = "ServerRuleId24Text";
                    context["mTip"] = "ServerRuleId24Text";
                    context["mImg"] = "ServerRuleId24Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmServerRule14", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
mr.data = new List<SerializableDictionary<string,string>>();
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmServerRule14ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmServerRule14DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["ServerRuleId24"];
                    var ds = PrepAdmServerRuleData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["ServerRuleId24"]));
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

                    var pid = mst["ServerRuleId24"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmServerRule14", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = null; 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ServerRuleId24", "", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmServerRuleData(mst, dtl, string.IsNullOrEmpty(mst["ServerRuleId24"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["ServerRuleId24"]))
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

                     foreach (var x in dtl){
                        
                     }
                     /* AsmxRule: Save Data After */


                    /* AsmxRule End: Save Data After */

                    ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    SaveDataResponse result = new SaveDataResponse();
                    DataTable dtColAuth = _GetAuthCol(GetScreenId());
                    Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());

                    result.mst = DataTableToListOfObject(dtMst, false, colAuth)[0];

                    
                    result.message = msg;
                    mr.status = "success";
                    mr.errorMsg = "";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "S", null));
                return ret;
            }
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRuleTypeId24List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlRuleTypeId3S151", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RuleTypeId24", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId24List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlScreenId3S139";context["addnew"] = "Y";context["mKey"] = "ScreenId24";context["mVal"] = "ScreenId24Text";context["mTip"] = "ScreenId24Text";context["mImg"] = "ScreenId24Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId24", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBeforeCRUD24List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBeforeCRUD3S163", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BeforeCRUD24", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetModifiedBy24List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlModifiedBy3S1397", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ModifiedBy24", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmServerRule");
                    var SearchList = GetAdmServerRule14List("", 0, "");
                                    var RuleTypeId24LIst = GetRuleTypeId24List("", 0, "");
                        var ScreenId24LIst = GetScreenId24List("", 0, "");
                        var BeforeCRUD24LIst = GetBeforeCRUD24List("", 0, "");
                        var ModifiedBy24LIst = GetModifiedBy24List("", 0, "");

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
            