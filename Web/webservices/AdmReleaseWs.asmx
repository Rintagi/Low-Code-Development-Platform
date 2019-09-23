<%@ WebService Language="C#" Class="RO.Web.AdmReleaseWs" %>
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
            
                public class AdmRelease98 : DataSet
                {
                    public AdmRelease98()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmRelease")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmReleaseDel")));
                        this.DataSetName = "AdmRelease98";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmRelease98";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("ReleaseId191", typeof(string));
                        columns.Add("ReleaseName191", typeof(string));
                        columns.Add("ReleaseBuild191", typeof(string));
                        columns.Add("ReleaseDate191", typeof(string));
                        columns.Add("ReleaseOs191", typeof(string));
                        columns.Add("EntityId191", typeof(string));
                        columns.Add("ReleaseTypeId191", typeof(string));
                        columns.Add("DeployPath199", typeof(string));
                        columns.Add("TarScriptAft191", typeof(string));
                        columns.Add("ReadMe191", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("ReleaseId191", typeof(string));
                        columns.Add("ReleaseDtlId192", typeof(string));
                        columns.Add("ObjectType192", typeof(string));
                        columns.Add("RunOrder192", typeof(string));
                        columns.Add("SrcObject192", typeof(string));
                        columns.Add("SProcOnly192", typeof(string));
                        columns.Add("ObjectName192", typeof(string));
                        columns.Add("ObjectExempt192", typeof(string));
                        columns.Add("SrcClientTierId192", typeof(string));
                        columns.Add("SrcRuleTierId192", typeof(string));
                        columns.Add("SrcDataTierId192", typeof(string));
                        columns.Add("TarDataTierId192", typeof(string));
                        columns.Add("DoSpEncrypt192", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmReleaseWs : RO.Web.AsmxBase
            {
                const int screenId = 98;
                const byte systemId = 3;
                const string programName = "AdmRelease98";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmRelease98"; }
                protected override string GetMstTableName(bool underlying = true) { return "Release"; }
                protected override string GetDtlTableName(bool underlying = true) { return "ReleaseDtl"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ReleaseId" : "ReleaseId191"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "ReleaseDtlId" : "ReleaseDtlId192"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"ReleaseOs191", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReleaseOs3S1703"},{"mKey","ReleaseOs191"},{"mVal","ReleaseOs191Text"}, }},
{"EntityId191", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlEntityId3S1704"},{"mKey","EntityId191"},{"mVal","EntityId191Text"}, }},
{"ReleaseTypeId191", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReleaseTypeId3S1705"},{"mKey","ReleaseTypeId191"},{"mVal","ReleaseTypeId191Text"}, }},
{"ObjectType192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlObjectType3S1715"},{"mKey","ObjectType192"},{"mVal","ObjectType192Text"}, }},
{"SProcOnly192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSProcOnly3S1722"},{"mKey","SProcOnly192"},{"mVal","SProcOnly192Text"}, }},
{"SrcClientTierId192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSrcClientTierId3S1716"},{"mKey","SrcClientTierId192"},{"mVal","SrcClientTierId192Text"}, }},
{"SrcRuleTierId192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSrcRuleTierId3S1718"},{"mKey","SrcRuleTierId192"},{"mVal","SrcRuleTierId192Text"}, }},
{"SrcDataTierId192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSrcDataTierId3S1720"},{"mKey","SrcDataTierId192"},{"mVal","SrcDataTierId192Text"}, }},
{"TarDataTierId192", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTarDataTierId3S1721"},{"mKey","TarDataTierId192"},{"mVal","TarDataTierId192Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["ReleaseId191"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["ReleaseDtlId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["ObjectType192"] = System.Data.OleDb.OleDbType.Char.ToString();
dr["RunOrder192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["SrcObject192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["SProcOnly192"] = System.Data.OleDb.OleDbType.Char.ToString();
dr["ObjectName192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["ObjectExempt192"] = System.Data.OleDb.OleDbType.VarChar.ToString();
dr["SrcClientTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["SrcRuleTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["SrcDataTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["TarDataTierId192"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["DoSpEncrypt192"] = System.Data.OleDb.OleDbType.Char.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["ReleaseId191"] = "TextBox";dr["ReleaseDtlId192"] = "TextBox";
dr["ObjectType192"] = "DropDownList";
dr["RunOrder192"] = "TextBox";
dr["SrcObject192"] = "TextBox";
dr["SProcOnly192"] = "DropDownList";
dr["ObjectName192"] = "MultiLine";
dr["ObjectExempt192"] = "MultiLine";
dr["SrcClientTierId192"] = "DropDownList";
dr["SrcRuleTierId192"] = "DropDownList";
dr["SrcDataTierId192"] = "DropDownList";
dr["TarDataTierId192"] = "DropDownList";
dr["DoSpEncrypt192"] = "CheckBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["ReleaseId191"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["ReleaseDtlId192"] = (drv["ReleaseDtlId192"] ?? "").ToString().Trim().Left(9999999);
dr["ObjectType192"] = drv["ObjectType192"];
dr["RunOrder192"] = (drv["RunOrder192"] ?? "").ToString().Trim().Left(9999999);
dr["SrcObject192"] = (drv["SrcObject192"] ?? "").ToString().Trim().Left(50);
dr["SProcOnly192"] = drv["SProcOnly192"];
dr["ObjectName192"] = drv["ObjectName192"];
dr["ObjectExempt192"] = drv["ObjectExempt192"];
dr["SrcClientTierId192"] = drv["SrcClientTierId192"];
dr["SrcRuleTierId192"] = drv["SrcRuleTierId192"];
dr["SrcDataTierId192"] = drv["SrcDataTierId192"];
dr["TarDataTierId192"] = drv["TarDataTierId192"];
dr["DoSpEncrypt192"] = drv["DoSpEncrypt192"];

                    }
                    return dr;
                }

                private AdmRelease98 PrepAdmReleaseData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmRelease98 ds = new AdmRelease98();
                    DataRow dr = ds.Tables["AdmRelease"].NewRow();
                    DataRow drType = ds.Tables["AdmRelease"].NewRow();
                    DataRow drDisp = ds.Tables["AdmRelease"].NewRow();
            if (bAdd) { dr["ReleaseId191"] = string.Empty; } else { dr["ReleaseId191"] = mst["ReleaseId191"]; }
drType["ReleaseId191"] = "Numeric"; drDisp["ReleaseId191"] = "TextBox";
try { dr["ReleaseName191"] = (mst["ReleaseName191"] ?? "").Trim().Left(50); } catch { }
drType["ReleaseName191"] = "VarWChar"; drDisp["ReleaseName191"] = "TextBox";
try { dr["ReleaseBuild191"] = (mst["ReleaseBuild191"] ?? "").Trim().Left(20); } catch { }
drType["ReleaseBuild191"] = "VarChar"; drDisp["ReleaseBuild191"] = "TextBox";
try { dr["ReleaseDate191"] = mst["ReleaseDate191"]; } catch { }
drType["ReleaseDate191"] = "DBDate"; drDisp["ReleaseDate191"] = "LongDate";
try { dr["ReleaseOs191"] = mst["ReleaseOs191"]; } catch { }
drType["ReleaseOs191"] = "Char"; drDisp["ReleaseOs191"] = "DropDownList";
try { dr["EntityId191"] = mst["EntityId191"]; } catch { }
drType["EntityId191"] = "Numeric"; drDisp["EntityId191"] = "DropDownList";
try { dr["ReleaseTypeId191"] = mst["ReleaseTypeId191"]; } catch { }
drType["ReleaseTypeId191"] = "Numeric"; drDisp["ReleaseTypeId191"] = "DropDownList";
try { dr["DeployPath199"] = (mst["DeployPath199"] ?? "").Trim().Left(100); } catch { }
drType["DeployPath199"] = "VarChar"; drDisp["DeployPath199"] = "TextBox";
try { dr["TarScriptAft191"] = mst["TarScriptAft191"]; } catch { }
drType["TarScriptAft191"] = "VarChar"; drDisp["TarScriptAft191"] = "MultiLine";
try { dr["ReadMe191"] = mst["ReadMe191"]; } catch { }
drType["ReadMe191"] = "VarChar"; drDisp["ReadMe191"] = "MultiLine";

                    if (dtl != null)
                    {
                        ds.Tables["AdmReleaseDef"].Rows.Add(MakeTypRow(ds.Tables["AdmReleaseDef"].NewRow()));
                        ds.Tables["AdmReleaseDef"].Rows.Add(MakeDisRow(ds.Tables["AdmReleaseDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmReleaseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseAdd"].NewRow(), drv, mst["ReleaseId191"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["ReleaseDtlId192"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmReleaseUpd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseUpd"].NewRow(), drv, mst["ReleaseId191"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["ReleaseDtlId192"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmReleaseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseAdd"].NewRow(), drv, mst["ReleaseId191"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["ReleaseDtlId192"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmReleaseDel"].Rows.Add(MakeColRow(ds.Tables["AdmReleaseDel"].NewRow(), drv, mst["ReleaseId191"], false));
                            }
                        }
                    }
                    ds.Tables["AdmRelease"].Rows.Add(dr); ds.Tables["AdmRelease"].Rows.Add(drType); ds.Tables["AdmRelease"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ReleaseId191",""},
{"ReleaseName191",""},
{"ReleaseBuild191",""},
{"ReleaseDate191",""},
{"ReleaseOs191",""},
{"EntityId191",""},
{"ReleaseTypeId191",""},
{"DeployPath199",""},
{"TarScriptAft191",""},
{"ReadMe191",""},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ReleaseDtlId192",""},
{"ObjectType192",""},
{"RunOrder192",""},
{"SrcObject192",""},
{"SProcOnly192",""},
{"ObjectName192",""},
{"ObjectExempt192",""},
{"SrcClientTierId192",""},
{"SrcRuleTierId192",""},
{"SrcDataTierId192",""},
{"TarDataTierId192",""},
{"DoSpEncrypt192",""},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRelease98List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmRelease98";
                    context["mKey"] = "ReleaseId191";
                    context["mVal"] = "ReleaseId191Text";
                    context["mTip"] = "ReleaseId191Text";
                    context["mImg"] = "ReleaseId191Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRelease98ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmRelease98", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRelease98DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmRelease98", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(98, "GetAdmRelease98DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmRelease98ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmRelease98DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["ReleaseId191"];
                    var ds = PrepAdmReleaseData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["ReleaseId191"]));
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

                    var pid = mst["ReleaseId191"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmRelease98", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["ReleaseDtlId192"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ReleaseId191", "ReleaseDtlId192", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmReleaseData(mst, dtl, string.IsNullOrEmpty(mst["ReleaseId191"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["ReleaseId191"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReleaseOs191List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlReleaseOs3S1703", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReleaseOs191", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetEntityId191List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlEntityId3S1704", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "EntityId191", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReleaseTypeId191List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlReleaseTypeId3S1705", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReleaseTypeId191", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetObjectType192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlObjectType3S1715", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ObjectType192", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSProcOnly192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSProcOnly3S1722", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SProcOnly192", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSrcClientTierId192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSrcClientTierId3S1716", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SrcClientTierId192", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSrcRuleTierId192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSrcRuleTierId3S1718", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SrcRuleTierId192", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSrcDataTierId192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlSrcDataTierId3S1720", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SrcDataTierId192", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTarDataTierId192List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlTarDataTierId3S1721", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TarDataTierId192", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmRelease");
                    var SearchList = GetAdmRelease98List("", 0, "");
                                    var ReleaseOs191LIst = GetReleaseOs191List("", 0, "");
                        var EntityId191LIst = GetEntityId191List("", 0, "");
                        var ReleaseTypeId191LIst = GetReleaseTypeId191List("", 0, "");
                        var ObjectType192LIst = GetObjectType192List("", 0, "");
                        var SProcOnly192LIst = GetSProcOnly192List("", 0, "");
                        var SrcClientTierId192LIst = GetSrcClientTierId192List("", 0, "");
                        var SrcRuleTierId192LIst = GetSrcRuleTierId192List("", 0, "");
                        var SrcDataTierId192LIst = GetSrcDataTierId192List("", 0, "");
                        var TarDataTierId192LIst = GetTarDataTierId192List("", 0, "");

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
            