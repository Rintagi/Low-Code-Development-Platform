<%@ WebService Language="C#" Class="RO.Web.AdmScreenCriWs" %>
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
            
                public class AdmScreenCri73 : DataSet
                {
                    public AdmScreenCri73()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmScreenCri")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenCriDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenCriAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenCriUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenCriDel")));
                        this.DataSetName = "AdmScreenCri73";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmScreenCri73";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("ScreenCriId104", typeof(string));
                        columns.Add("ScreenId104", typeof(string));
                        columns.Add("LabelCss104", typeof(string));
                        columns.Add("ContentCss104", typeof(string));
                        columns.Add("ColumnId104", typeof(string));
                        columns.Add("OperatorId104", typeof(string));
                        columns.Add("DisplayModeId104", typeof(string));
                        columns.Add("TabIndex104", typeof(string));
                        columns.Add("RequiredValid104", typeof(string));
                        columns.Add("ColumnJustify104", typeof(string));
                        columns.Add("ColumnSize104", typeof(string));
                        columns.Add("RowSize104", typeof(string));
                        columns.Add("DdlKeyColumnId104", typeof(string));
                        columns.Add("DdlRefColumnId104", typeof(string));
                        columns.Add("DdlSrtColumnId104", typeof(string));
                        columns.Add("DdlFtrColumnId104", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("ScreenCriId104", typeof(string));
                        columns.Add("ScreenCriHlpId105", typeof(string));
                        columns.Add("CultureId105", typeof(string));
                        columns.Add("ColumnHeader105", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmScreenCriWs : RO.Web.AsmxBase
            {
                const int screenId = 73;
                const byte systemId = 3;
                const string programName = "AdmScreenCri73";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmScreenCri73"; }
                protected override string GetMstTableName(bool underlying = true) { return "ScreenCri"; }
                protected override string GetDtlTableName(bool underlying = true) { return "ScreenCriHlp"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ScreenCriId" : "ScreenCriId104"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "ScreenCriHlpId" : "ScreenCriHlpId105"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"ScreenId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S1179"},{"mKey","ScreenId104"},{"mVal","ScreenId104Text"}, }},
{"ColumnId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlColumnId3S1181"},{"mKey","ColumnId104"},{"mVal","ColumnId104Text"}, }},
{"OperatorId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlOperatorId3S3305"},{"mKey","OperatorId104"},{"mVal","OperatorId104Text"}, }},
{"DisplayModeId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDisplayModeId3S1183"},{"mKey","DisplayModeId104"},{"mVal","DisplayModeId104Text"}, }},
{"ColumnJustify104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlColumnJustify3S1425"},{"mKey","ColumnJustify104"},{"mVal","ColumnJustify104Text"}, }},
{"DdlKeyColumnId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlKeyColumnId3S1185"},{"mKey","DdlKeyColumnId104"},{"mVal","DdlKeyColumnId104Text"}, }},
{"DdlRefColumnId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlRefColumnId3S1186"},{"mKey","DdlRefColumnId104"},{"mVal","DdlRefColumnId104Text"}, }},
{"DdlSrtColumnId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlSrtColumnId3S1278"},{"mKey","DdlSrtColumnId104"},{"mVal","DdlSrtColumnId104Text"}, }},
{"DdlFtrColumnId104", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlFtrColumnId3S3365"},{"mKey","DdlFtrColumnId104"},{"mVal","DdlFtrColumnId104Text"}, {"refCol","ScreenId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ScreenId104"}}},
{"CultureId105", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S1191"},{"mKey","CultureId105"},{"mVal","CultureId105Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["ScreenCriId104"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["ScreenCriHlpId105"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["CultureId105"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["ColumnHeader105"] = System.Data.OleDb.OleDbType.VarWChar.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["ScreenCriId104"] = "TextBox";dr["ScreenCriHlpId105"] = "TextBox";
dr["CultureId105"] = "AutoComplete";
dr["ColumnHeader105"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["ScreenCriId104"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["ScreenCriHlpId105"] = (drv["ScreenCriHlpId105"] ?? "").ToString().Trim().Left(9999999);
dr["CultureId105"] = drv["CultureId105"];
dr["ColumnHeader105"] = (drv["ColumnHeader105"] ?? "").ToString().Trim().Left(50);

                    }
                    return dr;
                }

                private AdmScreenCri73 PrepAdmScreenCriData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmScreenCri73 ds = new AdmScreenCri73();
                    DataRow dr = ds.Tables["AdmScreenCri"].NewRow();
                    DataRow drType = ds.Tables["AdmScreenCri"].NewRow();
                    DataRow drDisp = ds.Tables["AdmScreenCri"].NewRow();
            if (bAdd) { dr["ScreenCriId104"] = string.Empty; } else { dr["ScreenCriId104"] = mst["ScreenCriId104"]; }
drType["ScreenCriId104"] = "Numeric"; drDisp["ScreenCriId104"] = "TextBox";
try { dr["ScreenId104"] = mst["ScreenId104"]; } catch { }
drType["ScreenId104"] = "Numeric"; drDisp["ScreenId104"] = "AutoComplete";
try { dr["LabelCss104"] = (mst["LabelCss104"] ?? "").Trim().Left(100); } catch { }
drType["LabelCss104"] = "VarChar"; drDisp["LabelCss104"] = "TextBox";
try { dr["ContentCss104"] = (mst["ContentCss104"] ?? "").Trim().Left(100); } catch { }
drType["ContentCss104"] = "VarChar"; drDisp["ContentCss104"] = "TextBox";
try { dr["ColumnId104"] = mst["ColumnId104"]; } catch { }
drType["ColumnId104"] = "Numeric"; drDisp["ColumnId104"] = "AutoComplete";
try { dr["OperatorId104"] = mst["OperatorId104"]; } catch { }
drType["OperatorId104"] = "Numeric"; drDisp["OperatorId104"] = "DropDownList";
try { dr["DisplayModeId104"] = mst["DisplayModeId104"]; } catch { }
drType["DisplayModeId104"] = "Numeric"; drDisp["DisplayModeId104"] = "DropDownList";
try { dr["TabIndex104"] = (mst["TabIndex104"] ?? "").Trim().Left(9999999); } catch { }
drType["TabIndex104"] = "Numeric"; drDisp["TabIndex104"] = "TextBox";
try { dr["RequiredValid104"] = (mst["RequiredValid104"] ?? "").Trim().Left(1); } catch { }
drType["RequiredValid104"] = "Char"; drDisp["RequiredValid104"] = "CheckBox";
try { dr["ColumnJustify104"] = mst["ColumnJustify104"]; } catch { }
drType["ColumnJustify104"] = "Char"; drDisp["ColumnJustify104"] = "DropDownList";
try { dr["ColumnSize104"] = (mst["ColumnSize104"] ?? "").Trim().Left(9999999); } catch { }
drType["ColumnSize104"] = "Numeric"; drDisp["ColumnSize104"] = "TextBox";
try { dr["RowSize104"] = (mst["RowSize104"] ?? "").Trim().Left(9999999); } catch { }
drType["RowSize104"] = "Numeric"; drDisp["RowSize104"] = "TextBox";
try { dr["DdlKeyColumnId104"] = mst["DdlKeyColumnId104"]; } catch { }
drType["DdlKeyColumnId104"] = "Numeric"; drDisp["DdlKeyColumnId104"] = "AutoComplete";
try { dr["DdlRefColumnId104"] = mst["DdlRefColumnId104"]; } catch { }
drType["DdlRefColumnId104"] = "Numeric"; drDisp["DdlRefColumnId104"] = "AutoComplete";
try { dr["DdlSrtColumnId104"] = mst["DdlSrtColumnId104"]; } catch { }
drType["DdlSrtColumnId104"] = "Numeric"; drDisp["DdlSrtColumnId104"] = "AutoComplete";
try { dr["DdlFtrColumnId104"] = mst["DdlFtrColumnId104"]; } catch { }
drType["DdlFtrColumnId104"] = "Numeric"; drDisp["DdlFtrColumnId104"] = "AutoComplete";

                    if (dtl != null)
                    {
                        ds.Tables["AdmScreenCriDef"].Rows.Add(MakeTypRow(ds.Tables["AdmScreenCriDef"].NewRow()));
                        ds.Tables["AdmScreenCriDef"].Rows.Add(MakeDisRow(ds.Tables["AdmScreenCriDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmScreenCriAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenCriAdd"].NewRow(), drv, mst["ScreenCriId104"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["ScreenCriHlpId105"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmScreenCriUpd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenCriUpd"].NewRow(), drv, mst["ScreenCriId104"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["ScreenCriHlpId105"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmScreenCriAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenCriAdd"].NewRow(), drv, mst["ScreenCriId104"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["ScreenCriHlpId105"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmScreenCriDel"].Rows.Add(MakeColRow(ds.Tables["AdmScreenCriDel"].NewRow(), drv, mst["ScreenCriId104"], false));
                            }
                        }
                    }
                    ds.Tables["AdmScreenCri"].Rows.Add(dr); ds.Tables["AdmScreenCri"].Rows.Add(drType); ds.Tables["AdmScreenCri"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ScreenCriId104",""},
{"ScreenId104",""},
{"LabelCss104",""},
{"ContentCss104",""},
{"ColumnId104",""},
{"OperatorId104","1"},
{"DisplayModeId104",""},
{"TabIndex104",""},
{"RequiredValid104",""},
{"ColumnJustify104","L"},
{"ColumnSize104",""},
{"RowSize104",""},
{"DdlKeyColumnId104",""},
{"DdlRefColumnId104",""},
{"DdlSrtColumnId104",""},
{"DdlFtrColumnId104",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ScreenCriHlpId105",""},
{"CultureId105","1"},
{"ColumnHeader105",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenCri73List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmScreenCri73";
                    context["mKey"] = "ScreenCriId104";
                    context["mVal"] = "ScreenCriId104Text";
                    context["mTip"] = "ScreenCriId104Text";
                    context["mImg"] = "ScreenCriId104Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenCri73ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmScreenCri73", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenCri73DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmScreenCri73", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(73, "GetAdmScreenCri73DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmScreenCri73ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmScreenCri73DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["ScreenCriId104"];
                    var ds = PrepAdmScreenCriData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["ScreenCriId104"]));
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

                    var pid = mst["ScreenCriId104"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmScreenCri73", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["ScreenCriHlpId105"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ScreenCriId104", "ScreenCriHlpId105", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmScreenCriData(mst, dtl, string.IsNullOrEmpty(mst["ScreenCriId104"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["ScreenCriId104"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlScreenId3S1179";context["addnew"] = "Y";context["mKey"] = "ScreenId104";context["mVal"] = "ScreenId104Text";context["mTip"] = "ScreenId104Text";context["mImg"] = "ScreenId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetColumnId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlColumnId3S1181";context["addnew"] = "Y";context["mKey"] = "ColumnId104";context["mVal"] = "ColumnId104Text";context["mTip"] = "ColumnId104Text";context["mImg"] = "ColumnId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ColumnId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetOperatorId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlOperatorId3S3305", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "OperatorId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDisplayModeId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDisplayModeId3S1183", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DisplayModeId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetColumnJustify104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlColumnJustify3S1425", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ColumnJustify104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlKeyColumnId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlKeyColumnId3S1185";context["addnew"] = "Y";context["mKey"] = "DdlKeyColumnId104";context["mVal"] = "DdlKeyColumnId104Text";context["mTip"] = "DdlKeyColumnId104Text";context["mImg"] = "DdlKeyColumnId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlKeyColumnId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlRefColumnId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlRefColumnId3S1186";context["addnew"] = "Y";context["mKey"] = "DdlRefColumnId104";context["mVal"] = "DdlRefColumnId104Text";context["mTip"] = "DdlRefColumnId104Text";context["mImg"] = "DdlRefColumnId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlRefColumnId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlSrtColumnId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlSrtColumnId3S1278";context["addnew"] = "Y";context["mKey"] = "DdlSrtColumnId104";context["mVal"] = "DdlSrtColumnId104Text";context["mTip"] = "DdlSrtColumnId104Text";context["mImg"] = "DdlSrtColumnId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlSrtColumnId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlFtrColumnId104List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlFtrColumnId3S3365";context["addnew"] = "Y";context["mKey"] = "DdlFtrColumnId104";context["mVal"] = "DdlFtrColumnId104Text";context["mTip"] = "DdlFtrColumnId104Text";context["mImg"] = "DdlFtrColumnId104Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "ScreenId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlFtrColumnId104", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId105List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCultureId3S1191";context["addnew"] = "Y";context["mKey"] = "CultureId105";context["mVal"] = "CultureId105Text";context["mTip"] = "CultureId105Text";context["mImg"] = "CultureId105Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId105", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmScreenCri");
                    var SearchList = GetAdmScreenCri73List("", 0, "");
                                    var ScreenId104LIst = GetScreenId104List("", 0, "");
                        var ColumnId104LIst = GetColumnId104List("", 0, "");
                        var OperatorId104LIst = GetOperatorId104List("", 0, "");
                        var DisplayModeId104LIst = GetDisplayModeId104List("", 0, "");
                        var ColumnJustify104LIst = GetColumnJustify104List("", 0, "");
                        var DdlKeyColumnId104LIst = GetDdlKeyColumnId104List("", 0, "");
                        var DdlRefColumnId104LIst = GetDdlRefColumnId104List("", 0, "");
                        var DdlSrtColumnId104LIst = GetDdlSrtColumnId104List("", 0, "");
                        var DdlFtrColumnId104LIst = GetDdlFtrColumnId104List("", 0, "");
                        var CultureId105LIst = GetCultureId105List("", 0, "");

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
            