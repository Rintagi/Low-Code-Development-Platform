<%@ WebService Language="C#" Class="RO.Web.AdmDataCatWs" %>
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
            
                public class AdmDataCat96 : DataSet
                {
                    public AdmDataCat96()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmDataCat")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmDataCatDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmDataCatAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmDataCatUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmDataCatDel")));
                        this.DataSetName = "AdmDataCat96";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmDataCat96";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("RptwizCatId181", typeof(string));
                        columns.Add("RptwizTypId181", typeof(string));
                        columns.Add("RptwizCatName181", typeof(string));
                        columns.Add("CatDescription181", typeof(string));
                        columns.Add("TableId181", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("RptwizCatId181", typeof(string));
                        columns.Add("RptwizCatDtlId182", typeof(string));
                        columns.Add("ColumnId182", typeof(string));
                        columns.Add("DisplayModeId182", typeof(string));
                        columns.Add("ColumnSize182", typeof(string));
                        columns.Add("RowSize182", typeof(string));
                        columns.Add("DdlKeyColNm182", typeof(string));
                        columns.Add("DdlRefColNm182", typeof(string));
                        columns.Add("RegClause182", typeof(string));
                        columns.Add("StoredProc182", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmDataCatWs : RO.Web.AsmxBase
            {
                const int screenId = 96;
                const byte systemId = 3;
                const string programName = "AdmDataCat96";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmDataCat96"; }
                protected override string GetMstTableName(bool underlying = true) { return "RptwizCat"; }
                protected override string GetDtlTableName(bool underlying = true) { return "RptwizCatDtl"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "RptwizCatId" : "RptwizCatId181"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "RptwizCatDtlId" : "RptwizCatDtlId182"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"RptwizTypId181", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptwizTypId3S1671"},{"mKey","RptwizTypId181"},{"mVal","RptwizTypId181Text"}, }},
{"TableId181", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTableId3S1663"},{"mKey","TableId181"},{"mVal","TableId181Text"}, }},
{"ColumnId182", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlColumnId3S1667"},{"mKey","ColumnId182"},{"mVal","ColumnId182Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","TableId181"}}},
{"DisplayModeId182", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDisplayModeId3S1843"},{"mKey","DisplayModeId182"},{"mVal","DisplayModeId182Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["RptwizCatId181"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["RptwizCatDtlId182"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["ColumnId182"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["DisplayModeId182"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["ColumnSize182"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["RowSize182"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["DdlKeyColNm182"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["DdlRefColNm182"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["RegClause182"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["StoredProc182"] = System.Data.OleDb.OleDbType.VarChar.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["RptwizCatId181"] = "TextBox";dr["RptwizCatDtlId182"] = "TextBox";
dr["ColumnId182"] = "AutoComplete";
dr["DisplayModeId182"] = "DropDownList";
dr["ColumnSize182"] = "TextBox";
dr["RowSize182"] = "TextBox";
dr["DdlKeyColNm182"] = "TextBox";
dr["DdlRefColNm182"] = "TextBox";
dr["RegClause182"] = "MultiLine";
dr["StoredProc182"] = "MultiLine";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["RptwizCatId181"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["RptwizCatDtlId182"] = (drv["RptwizCatDtlId182"] ?? "").ToString().Trim().Left(9999999);
dr["ColumnId182"] = drv["ColumnId182"];
dr["DisplayModeId182"] = drv["DisplayModeId182"];
dr["ColumnSize182"] = (drv["ColumnSize182"] ?? "").ToString().Trim().Left(9999999);
dr["RowSize182"] = (drv["RowSize182"] ?? "").ToString().Trim().Left(9999999);
dr["DdlKeyColNm182"] = (drv["DdlKeyColNm182"] ?? "").ToString().Trim().Left(50);
dr["DdlRefColNm182"] = (drv["DdlRefColNm182"] ?? "").ToString().Trim().Left(50);
dr["RegClause182"] = drv["RegClause182"];
dr["StoredProc182"] = drv["StoredProc182"];

                    }
                    return dr;
                }

                private AdmDataCat96 PrepAdmDataCatData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmDataCat96 ds = new AdmDataCat96();
                    DataRow dr = ds.Tables["AdmDataCat"].NewRow();
                    DataRow drType = ds.Tables["AdmDataCat"].NewRow();
                    DataRow drDisp = ds.Tables["AdmDataCat"].NewRow();
            if (bAdd) { dr["RptwizCatId181"] = string.Empty; } else { dr["RptwizCatId181"] = mst["RptwizCatId181"]; }
drType["RptwizCatId181"] = "Numeric"; drDisp["RptwizCatId181"] = "TextBox";
try { dr["RptwizTypId181"] = mst["RptwizTypId181"]; } catch { }
drType["RptwizTypId181"] = "Numeric"; drDisp["RptwizTypId181"] = "AutoComplete";
try { dr["RptwizCatName181"] = (mst["RptwizCatName181"] ?? "").Trim().Left(100); } catch { }
drType["RptwizCatName181"] = "VarWChar"; drDisp["RptwizCatName181"] = "TextBox";
try { dr["CatDescription181"] = mst["CatDescription181"]; } catch { }
drType["CatDescription181"] = "VarWChar"; drDisp["CatDescription181"] = "MultiLine";
try { dr["TableId181"] = mst["TableId181"]; } catch { }
drType["TableId181"] = "Numeric"; drDisp["TableId181"] = "AutoComplete";


                    if (dtl != null)
                    {
                        ds.Tables["AdmDataCatDef"].Rows.Add(MakeTypRow(ds.Tables["AdmDataCatDef"].NewRow()));
                        ds.Tables["AdmDataCatDef"].Rows.Add(MakeDisRow(ds.Tables["AdmDataCatDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmDataCatAdd"].Rows.Add(MakeColRow(ds.Tables["AdmDataCatAdd"].NewRow(), drv, mst["RptwizCatId181"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["RptwizCatDtlId182"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmDataCatUpd"].Rows.Add(MakeColRow(ds.Tables["AdmDataCatUpd"].NewRow(), drv, mst["RptwizCatId181"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["RptwizCatDtlId182"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmDataCatAdd"].Rows.Add(MakeColRow(ds.Tables["AdmDataCatAdd"].NewRow(), drv, mst["RptwizCatId181"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["RptwizCatDtlId182"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmDataCatDel"].Rows.Add(MakeColRow(ds.Tables["AdmDataCatDel"].NewRow(), drv, mst["RptwizCatId181"], false));
                            }
                        }
                    }
                    ds.Tables["AdmDataCat"].Rows.Add(dr); ds.Tables["AdmDataCat"].Rows.Add(drType); ds.Tables["AdmDataCat"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptwizCatId181",""},
{"RptwizTypId181",""},
{"RptwizCatName181",""},
{"CatDescription181",""},
{"TableId181",""},
{"SampleImage181",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptwizCatDtlId182",""},
{"ColumnId182",""},
{"DisplayModeId182",""},
{"ColumnSize182",""},
{"RowSize182",""},
{"DdlKeyColNm182",""},
{"DdlRefColNm182",""},
{"RegClause182",""},
{"StoredProc182",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmDataCat96List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmDataCat96";
                    context["mKey"] = "RptwizCatId181";
                    context["mVal"] = "RptwizCatId181Text";
                    context["mTip"] = "RptwizCatId181Text";
                    context["mImg"] = "RptwizCatId181Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmDataCat96ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmDataCat96", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmDataCat96DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmDataCat96", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(96, "GetAdmDataCat96DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmDataCat96ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmDataCat96DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["RptwizCatId181"];
                    var ds = PrepAdmDataCatData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["RptwizCatId181"]));
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

                    var pid = mst["RptwizCatId181"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmDataCat96", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["RptwizCatDtlId182"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "RptwizCatId181", "RptwizCatDtlId182", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmDataCatData(mst, dtl, string.IsNullOrEmpty(mst["RptwizCatId181"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["RptwizCatId181"]))
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
                                                if (!string.IsNullOrEmpty(x["SampleImage181"]))
{
foreach (DataRow dr in dtDtl.Rows)
{
/* use primary key or heuristic that new dtl has larger max dtlId before save(which is bad but assuming there is only 1 detail add, it would be fine. should be done in the UpdData call during add/update FIXME */
if ((!string.IsNullOrEmpty(x["RptwizCatDtlId182"]) && dr["RptwizCatDtlId182"].ToString() == x["RptwizCatDtlId182"])
||
(string.IsNullOrEmpty(x["RptwizCatDtlId182"]) && int.Parse(dr["RptwizCatDtlId182"].ToString()) > maxDtlId)
)
{AddDoc(x["SampleImage181"], dr["RptwizCatDtlId182"].ToString(), "dbo.RptwizCatDtl", "RptwizCatDtlId", "SampleImage", options.ContainsKey("resizeImage"));}
}
}
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptwizTypId181List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlRptwizTypId3S1671";context["addnew"] = "Y";context["mKey"] = "RptwizTypId181";context["mVal"] = "RptwizTypId181Text";context["mTip"] = "RptwizTypId181Text";context["mImg"] = "RptwizTypId181Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptwizTypId181", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTableId181List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlTableId3S1663";context["addnew"] = "Y";context["mKey"] = "TableId181";context["mVal"] = "TableId181Text";context["mTip"] = "TableId181Text";context["mImg"] = "TableId181Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TableId181", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetColumnId182List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlColumnId3S1667";context["addnew"] = "Y";context["mKey"] = "ColumnId182";context["mVal"] = "ColumnId182Text";context["mTip"] = "ColumnId182Text";context["mImg"] = "ColumnId182Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ColumnId182", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDisplayModeId182List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDisplayModeId3S1843", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DisplayModeId182", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmDataCat");
                    var SearchList = GetAdmDataCat96List("", 0, "");
                                    var RptwizTypId181LIst = GetRptwizTypId181List("", 0, "");
                        var TableId181LIst = GetTableId181List("", 0, "");
                        var ColumnId182LIst = GetColumnId182List("", 0, "");
                        var DisplayModeId182LIst = GetDisplayModeId182List("", 0, "");

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
            