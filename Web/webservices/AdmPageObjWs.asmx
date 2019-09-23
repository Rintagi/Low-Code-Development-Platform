<%@ WebService Language="C#" Class="RO.Web.AdmPageObjWs" %>
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
            
                public class AdmPageObj1001 : DataSet
                {
                    public AdmPageObj1001()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmPageObj")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmPageObjDel")));
                        this.DataSetName = "AdmPageObj1001";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmPageObj1001";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("PageObjId1277", typeof(string));
                        columns.Add("SectionCd1277", typeof(string));
                        columns.Add("GroupRowId1277", typeof(string));
                        columns.Add("GroupColId1277", typeof(string));
                        columns.Add("LinkTypeCd1277", typeof(string));
                        columns.Add("PageObjOrd1277", typeof(string));
                        columns.Add("SctGrpRow1277", typeof(string));
                        columns.Add("SctGrpCol1277", typeof(string));
                        columns.Add("PageObjCss1277", typeof(string));
                        columns.Add("PageObjSrp1277", typeof(string));
                        columns.Add("BtnDefault", typeof(string));
                        columns.Add("BtnHeader", typeof(string));
                        columns.Add("BtnFooter", typeof(string));
                        columns.Add("BtnSidebar", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("PageObjId1277", typeof(string));
                        columns.Add("PageLnkId1278", typeof(string));
                        columns.Add("PageLnkTxt1278", typeof(string));
                        columns.Add("PageLnkRef1278", typeof(string));
                        columns.Add("PageLnkImg1278", typeof(string));
                        columns.Add("PageLnkAlt1278", typeof(string));
                        columns.Add("PageLnkOrd1278", typeof(string));
                        columns.Add("Popup1278", typeof(string));
                        columns.Add("PageLnkCss1278", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmPageObjWs : RO.Web.AsmxBase
            {
                const int screenId = 1001;
                const byte systemId = 3;
                const string programName = "AdmPageObj1001";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmPageObj1001"; }
                protected override string GetMstTableName(bool underlying = true) { return "PageObj"; }
                protected override string GetDtlTableName(bool underlying = true) { return "PageLnk"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "PageObjId" : "PageObjId1277"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "PageLnkId" : "PageLnkId1278"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"SectionCd1277", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSectionCd3S3118"},{"mKey","SectionCd1277"},{"mVal","SectionCd1277Text"}, }},
{"GroupRowId1277", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGroupRowId3S3119"},{"mKey","GroupRowId1277"},{"mVal","GroupRowId1277Text"}, }},
{"GroupColId1277", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGroupColId3S3120"},{"mKey","GroupColId1277"},{"mVal","GroupColId1277Text"}, }},
{"LinkTypeCd1277", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlLinkTypeCd3S3123"},{"mKey","LinkTypeCd1277"},{"mVal","LinkTypeCd1277Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["PageObjId1277"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["PageLnkId1278"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["PageLnkTxt1278"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["PageLnkRef1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["PageLnkImg1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["PageLnkAlt1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["PageLnkOrd1278"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["Popup1278"] = System.Data.OleDb.OleDbType.Char.ToString();
dr["PageLnkCss1278"] = System.Data.OleDb.OleDbType.VarChar.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["PageObjId1277"] = "TextBox";dr["PageLnkId1278"] = "TextBox";
dr["PageLnkTxt1278"] = "TextBox";
dr["PageLnkRef1278"] = "TextBox";
dr["PageLnkImg1278"] = "Upload";
dr["PageLnkAlt1278"] = "Upload";
dr["PageLnkOrd1278"] = "TextBox";
dr["Popup1278"] = "CheckBox";
dr["PageLnkCss1278"] = "MultiLine";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["PageObjId1277"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["PageLnkId1278"] = (drv["PageLnkId1278"] ?? "").ToString().Trim().Left(9999999);
dr["PageLnkTxt1278"] = (drv["PageLnkTxt1278"] ?? "").ToString().Trim().Left(4000);
dr["PageLnkRef1278"] = (drv["PageLnkRef1278"] ?? "").ToString().Trim().Left(1000);
dr["PageLnkImg1278"] = drv["PageLnkImg1278"];
dr["PageLnkAlt1278"] = drv["PageLnkAlt1278"];
dr["PageLnkOrd1278"] = (drv["PageLnkOrd1278"] ?? "").ToString().Trim().Left(9999999);
dr["Popup1278"] = drv["Popup1278"];
dr["PageLnkCss1278"] = drv["PageLnkCss1278"];

                    }
                    return dr;
                }

                private AdmPageObj1001 PrepAdmPageObjData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmPageObj1001 ds = new AdmPageObj1001();
                    DataRow dr = ds.Tables["AdmPageObj"].NewRow();
                    DataRow drType = ds.Tables["AdmPageObj"].NewRow();
                    DataRow drDisp = ds.Tables["AdmPageObj"].NewRow();
            if (bAdd) { dr["PageObjId1277"] = string.Empty; } else { dr["PageObjId1277"] = mst["PageObjId1277"]; }
drType["PageObjId1277"] = "Numeric"; drDisp["PageObjId1277"] = "TextBox";
try { dr["SectionCd1277"] = mst["SectionCd1277"]; } catch { }
drType["SectionCd1277"] = "Char"; drDisp["SectionCd1277"] = "DropDownList";
try { dr["GroupRowId1277"] = mst["GroupRowId1277"]; } catch { }
drType["GroupRowId1277"] = "Numeric"; drDisp["GroupRowId1277"] = "AutoComplete";
try { dr["GroupColId1277"] = mst["GroupColId1277"]; } catch { }
drType["GroupColId1277"] = "Numeric"; drDisp["GroupColId1277"] = "AutoComplete";
try { dr["LinkTypeCd1277"] = mst["LinkTypeCd1277"]; } catch { }
drType["LinkTypeCd1277"] = "Char"; drDisp["LinkTypeCd1277"] = "AutoComplete";
try { dr["PageObjOrd1277"] = (mst["PageObjOrd1277"] ?? "").Trim().Left(9999999); } catch { }
drType["PageObjOrd1277"] = "Numeric"; drDisp["PageObjOrd1277"] = "TextBox";
try { dr["SctGrpRow1277"] = mst["SctGrpRow1277"]; } catch { }
drType["SctGrpRow1277"] = "VarChar"; drDisp["SctGrpRow1277"] = "ImagePopUp";
try { dr["SctGrpCol1277"] = mst["SctGrpCol1277"]; } catch { }
drType["SctGrpCol1277"] = "VarChar"; drDisp["SctGrpCol1277"] = "ImagePopUp";
try { dr["PageObjCss1277"] = mst["PageObjCss1277"]; } catch { }
drType["PageObjCss1277"] = "VarChar"; drDisp["PageObjCss1277"] = "MultiLine";
try { dr["PageObjSrp1277"] = mst["PageObjSrp1277"]; } catch { }
drType["PageObjSrp1277"] = "VarChar"; drDisp["PageObjSrp1277"] = "MultiLine";





                    if (dtl != null)
                    {
                        ds.Tables["AdmPageObjDef"].Rows.Add(MakeTypRow(ds.Tables["AdmPageObjDef"].NewRow()));
                        ds.Tables["AdmPageObjDef"].Rows.Add(MakeDisRow(ds.Tables["AdmPageObjDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmPageObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjAdd"].NewRow(), drv, mst["PageObjId1277"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["PageLnkId1278"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmPageObjUpd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjUpd"].NewRow(), drv, mst["PageObjId1277"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["PageLnkId1278"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmPageObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjAdd"].NewRow(), drv, mst["PageObjId1277"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["PageLnkId1278"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmPageObjDel"].Rows.Add(MakeColRow(ds.Tables["AdmPageObjDel"].NewRow(), drv, mst["PageObjId1277"], false));
                            }
                        }
                    }
                    ds.Tables["AdmPageObj"].Rows.Add(dr); ds.Tables["AdmPageObj"].Rows.Add(drType); ds.Tables["AdmPageObj"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"PageObjId1277",""},
{"SectionCd1277",""},
{"GroupRowId1277",""},
{"GroupColId1277",""},
{"LinkTypeCd1277",""},
{"PageObjOrd1277","10"},
{"SctGrpRow1277",""},
{"SctGrpCol1277",""},
{"PageObjCss1277",""},
{"PageObjSrp1277",""},
{"BtnDefault",""},
{"BtnHeader",""},
{"BtnFooter",""},
{"BtnSidebar",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"PageLnkId1278",""},
{"PageLnkTxt1278",""},
{"PageLnkRef1278",""},
{"PageLnkImg1278",""},
{"PageLnkAlt1278",""},
{"PageLnkOrd1278","10"},
{"Popup1278","Y"},
{"PageLnkCss1278",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmPageObj1001List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmPageObj1001";
                    context["mKey"] = "PageObjId1277";
                    context["mVal"] = "PageObjId1277Text";
                    context["mTip"] = "PageObjId1277Text";
                    context["mImg"] = "PageObjId1277Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmPageObj1001ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmPageObj1001", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmPageObj1001DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmPageObj1001", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(1001, "GetAdmPageObj1001DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmPageObj1001ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmPageObj1001DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["PageObjId1277"];
                    var ds = PrepAdmPageObjData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["PageObjId1277"]));
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

                    var pid = mst["PageObjId1277"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmPageObj1001", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["PageLnkId1278"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "PageObjId1277", "PageLnkId1278", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmPageObjData(mst, dtl, string.IsNullOrEmpty(mst["PageObjId1277"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["PageObjId1277"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSectionCd1277List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSectionCd3S3118", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SectionCd1277", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGroupRowId1277List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlGroupRowId3S3119";context["addnew"] = "Y";context["mKey"] = "GroupRowId1277";context["mVal"] = "GroupRowId1277Text";context["mTip"] = "GroupRowId1277Text";context["mImg"] = "GroupRowId1277Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GroupRowId1277", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGroupColId1277List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlGroupColId3S3120";context["addnew"] = "Y";context["mKey"] = "GroupColId1277";context["mVal"] = "GroupColId1277Text";context["mTip"] = "GroupColId1277Text";context["mImg"] = "GroupColId1277Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GroupColId1277", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetLinkTypeCd1277List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlLinkTypeCd3S3123";context["addnew"] = "Y";context["mKey"] = "LinkTypeCd1277";context["mVal"] = "LinkTypeCd1277Text";context["mTip"] = "LinkTypeCd1277Text";context["mImg"] = "LinkTypeCd1277Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "LinkTypeCd1277", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmPageObj");
                    var SearchList = GetAdmPageObj1001List("", 0, "");
                                    var SectionCd1277LIst = GetSectionCd1277List("", 0, "");
                        var GroupRowId1277LIst = GetGroupRowId1277List("", 0, "");
                        var GroupColId1277LIst = GetGroupColId1277List("", 0, "");
                        var LinkTypeCd1277LIst = GetLinkTypeCd1277List("", 0, "");

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
            