<%@ WebService Language="C#" Class="RO.Web.AdmAppItemWs" %>
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
            
                public class AdmAppItem83 : DataSet
                {
                    public AdmAppItem83()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmAppItem")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppItemDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppItemAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppItemUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppItemDel")));
                        this.DataSetName = "AdmAppItem83";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmAppItem83";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("AppItemId136", typeof(string));
                        columns.Add("AppInfoId136", typeof(string));
                        columns.Add("LanguageCd136", typeof(string));
                        columns.Add("ItemOrder136", typeof(string));
                        columns.Add("FrameworkCd136", typeof(string));
                        columns.Add("ObjectTypeCd136", typeof(string));
                        columns.Add("DbProviderCd136", typeof(string));
                        columns.Add("RelativePath136", typeof(string));
                        columns.Add("AppItemName136", typeof(string));
                        columns.Add("MultiDesignDb136", typeof(string));
                        columns.Add("RemoveItem136", typeof(string));
                        columns.Add("AppItemCode136", typeof(string));
                        columns.Add("ScreenId136", typeof(string));
                        columns.Add("ReportId136", typeof(string));
                        columns.Add("WizardId136", typeof(string));
                        columns.Add("CustomId136", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("AppItemId136", typeof(string));

                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmAppItemWs : RO.Web.AsmxBase
            {
                const int screenId = 83;
                const byte systemId = 3;
                const string programName = "AdmAppItem83";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmAppItem83"; }
                protected override string GetMstTableName(bool underlying = true) { return "AppItem"; }
                protected override string GetDtlTableName(bool underlying = true) { return ""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "AppItemId" : "AppItemId136"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"AppInfoId136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAppInfoId3S1352"},{"mKey","AppInfoId136"},{"mVal","AppInfoId136Text"}, }},
{"LanguageCd136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlLanguageCd3S1355"},{"mKey","LanguageCd136"},{"mVal","LanguageCd136Text"}, }},
{"FrameworkCd136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlFrameworkCd3S1356"},{"mKey","FrameworkCd136"},{"mVal","FrameworkCd136Text"}, }},
{"ObjectTypeCd136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlObjectTypeCd3S1354"},{"mKey","ObjectTypeCd136"},{"mVal","ObjectTypeCd136Text"}, }},
{"DbProviderCd136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDbProviderCd3S1357"},{"mKey","DbProviderCd136"},{"mVal","DbProviderCd136Text"}, }},
{"ScreenId136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S1364"},{"mKey","ScreenId136"},{"mVal","ScreenId136Text"}, }},
{"ReportId136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1365"},{"mKey","ReportId136"},{"mVal","ReportId136Text"}, }},
{"WizardId136", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlWizardId3S1366"},{"mKey","WizardId136"},{"mVal","WizardId136Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["AppItemId136"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["AppItemId136"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["AppItemId136"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            

                    }
                    return dr;
                }

                private AdmAppItem83 PrepAdmAppItemData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmAppItem83 ds = new AdmAppItem83();
                    DataRow dr = ds.Tables["AdmAppItem"].NewRow();
                    DataRow drType = ds.Tables["AdmAppItem"].NewRow();
                    DataRow drDisp = ds.Tables["AdmAppItem"].NewRow();
            if (bAdd) { dr["AppItemId136"] = string.Empty; } else { dr["AppItemId136"] = mst["AppItemId136"]; }
drType["AppItemId136"] = "Numeric"; drDisp["AppItemId136"] = "TextBox";
try { dr["AppInfoId136"] = mst["AppInfoId136"]; } catch { }
drType["AppInfoId136"] = "Numeric"; drDisp["AppInfoId136"] = "AutoComplete";
try { dr["LanguageCd136"] = mst["LanguageCd136"]; } catch { }
drType["LanguageCd136"] = "Char"; drDisp["LanguageCd136"] = "DropDownList";
try { dr["ItemOrder136"] = (mst["ItemOrder136"] ?? "").Trim().Left(9999999); } catch { }
drType["ItemOrder136"] = "Numeric"; drDisp["ItemOrder136"] = "TextBox";
try { dr["FrameworkCd136"] = mst["FrameworkCd136"]; } catch { }
drType["FrameworkCd136"] = "Char"; drDisp["FrameworkCd136"] = "DropDownList";
try { dr["ObjectTypeCd136"] = mst["ObjectTypeCd136"]; } catch { }
drType["ObjectTypeCd136"] = "Char"; drDisp["ObjectTypeCd136"] = "DropDownList";
try { dr["DbProviderCd136"] = mst["DbProviderCd136"]; } catch { }
drType["DbProviderCd136"] = "Char"; drDisp["DbProviderCd136"] = "DropDownList";
try { dr["RelativePath136"] = (mst["RelativePath136"] ?? "").Trim().Left(100); } catch { }
drType["RelativePath136"] = "VarChar"; drDisp["RelativePath136"] = "TextBox";
try { dr["AppItemName136"] = (mst["AppItemName136"] ?? "").Trim().Left(50); } catch { }
drType["AppItemName136"] = "VarChar"; drDisp["AppItemName136"] = "TextBox";
try { dr["MultiDesignDb136"] = (mst["MultiDesignDb136"] ?? "").Trim().Left(1); } catch { }
drType["MultiDesignDb136"] = "Char"; drDisp["MultiDesignDb136"] = "CheckBox";
try { dr["RemoveItem136"] = (mst["RemoveItem136"] ?? "").Trim().Left(1); } catch { }
drType["RemoveItem136"] = "Char"; drDisp["RemoveItem136"] = "CheckBox";
try { dr["AppItemCode136"] = mst["AppItemCode136"]; } catch { }
drType["AppItemCode136"] = "VarWChar"; drDisp["AppItemCode136"] = "MultiLine";
try { dr["ScreenId136"] = mst["ScreenId136"]; } catch { }
drType["ScreenId136"] = "Numeric"; drDisp["ScreenId136"] = "AutoComplete";
try { dr["ReportId136"] = mst["ReportId136"]; } catch { }
drType["ReportId136"] = "Numeric"; drDisp["ReportId136"] = "AutoComplete";
try { dr["WizardId136"] = mst["WizardId136"]; } catch { }
drType["WizardId136"] = "Numeric"; drDisp["WizardId136"] = "AutoComplete";
try { dr["CustomId136"] = (mst["CustomId136"] ?? "").Trim().Left(9999999); } catch { }
drType["CustomId136"] = "Numeric"; drDisp["CustomId136"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmAppItemDef"].Rows.Add(MakeTypRow(ds.Tables["AdmAppItemDef"].NewRow()));
                        ds.Tables["AdmAppItemDef"].Rows.Add(MakeDisRow(ds.Tables["AdmAppItemDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmAppItemAdd"].Rows.Add(MakeColRow(ds.Tables["AdmAppItemAdd"].NewRow(), drv, mst["AppItemId136"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmAppItemUpd"].Rows.Add(MakeColRow(ds.Tables["AdmAppItemUpd"].NewRow(), drv, mst["AppItemId136"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmAppItemAdd"].Rows.Add(MakeColRow(ds.Tables["AdmAppItemAdd"].NewRow(), drv, mst["AppItemId136"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmAppItemDel"].Rows.Add(MakeColRow(ds.Tables["AdmAppItemDel"].NewRow(), drv, mst["AppItemId136"], false));
                            }
                        }
                    }
                    ds.Tables["AdmAppItem"].Rows.Add(dr); ds.Tables["AdmAppItem"].Rows.Add(drType); ds.Tables["AdmAppItem"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"AppItemId136",""},
{"AppInfoId136",""},
{"LanguageCd136",""},
{"ItemOrder136",""},
{"FrameworkCd136",""},
{"ObjectTypeCd136",""},
{"DbProviderCd136",""},
{"RelativePath136",""},
{"AppItemName136",""},
{"MultiDesignDb136",""},
{"RemoveItem136",""},
{"AppItemCode136",""},
{"ScreenId136",""},
{"ReportId136",""},
{"WizardId136",""},
{"CustomId136",""},

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
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppItem83List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmAppItem83";
                    context["mKey"] = "AppItemId136";
                    context["mVal"] = "AppItemId136Text";
                    context["mTip"] = "AppItemId136Text";
                    context["mImg"] = "AppItemId136Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppItem83ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmAppItem83", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppItem83DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmAppItem83ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmAppItem83DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["AppItemId136"];
                    var ds = PrepAdmAppItemData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["AppItemId136"]));
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

                    var pid = mst["AppItemId136"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmAppItem83", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = null; 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "AppItemId136", "", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmAppItemData(mst, dtl, string.IsNullOrEmpty(mst["AppItemId136"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["AppItemId136"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAppInfoId136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlAppInfoId3S1352";context["addnew"] = "Y";context["mKey"] = "AppInfoId136";context["mVal"] = "AppInfoId136Text";context["mTip"] = "AppInfoId136Text";context["mImg"] = "AppInfoId136Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AppInfoId136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetLanguageCd136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlLanguageCd3S1355", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "LanguageCd136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetFrameworkCd136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlFrameworkCd3S1356", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "FrameworkCd136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetObjectTypeCd136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlObjectTypeCd3S1354", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ObjectTypeCd136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDbProviderCd136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDbProviderCd3S1357", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DbProviderCd136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlScreenId3S1364";context["addnew"] = "Y";context["mKey"] = "ScreenId136";context["mVal"] = "ScreenId136Text";context["mTip"] = "ScreenId136Text";context["mImg"] = "ScreenId136Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlReportId3S1365";context["addnew"] = "Y";context["mKey"] = "ReportId136";context["mVal"] = "ReportId136Text";context["mTip"] = "ReportId136Text";context["mImg"] = "ReportId136Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId136", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetWizardId136List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlWizardId3S1366";context["addnew"] = "Y";context["mKey"] = "WizardId136";context["mVal"] = "WizardId136Text";context["mTip"] = "WizardId136Text";context["mImg"] = "WizardId136Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "WizardId136", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmAppItem");
                    var SearchList = GetAdmAppItem83List("", 0, "");
                                    var AppInfoId136LIst = GetAppInfoId136List("", 0, "");
                        var LanguageCd136LIst = GetLanguageCd136List("", 0, "");
                        var FrameworkCd136LIst = GetFrameworkCd136List("", 0, "");
                        var ObjectTypeCd136LIst = GetObjectTypeCd136List("", 0, "");
                        var DbProviderCd136LIst = GetDbProviderCd136List("", 0, "");
                        var ScreenId136LIst = GetScreenId136List("", 0, "");
                        var ReportId136LIst = GetReportId136List("", 0, "");
                        var WizardId136LIst = GetWizardId136List("", 0, "");

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
            