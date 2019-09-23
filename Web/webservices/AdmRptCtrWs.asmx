<%@ WebService Language="C#" Class="RO.Web.AdmRptCtrWs" %>
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
            
                public class AdmRptCtr90 : DataSet
                {
                    public AdmRptCtr90()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmRptCtr")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptCtrDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptCtrAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptCtrUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptCtrDel")));
                        this.DataSetName = "AdmRptCtr90";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmRptCtr90";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("RptCtrId161", typeof(string));
                        columns.Add("ReportId161", typeof(string));
                        columns.Add("RptCtrName161", typeof(string));
                        columns.Add("PRptCtrId161", typeof(string));
                        columns.Add("RptElmId161", typeof(string));
                        columns.Add("RptCelId161", typeof(string));
                        columns.Add("RptStyleId161", typeof(string));
                        columns.Add("RptCtrTypeCd161", typeof(string));
                        columns.Add("CtrTop161", typeof(string));
                        columns.Add("CtrLeft161", typeof(string));
                        columns.Add("CtrHeight161", typeof(string));
                        columns.Add("CtrWidth161", typeof(string));
                        columns.Add("CtrZIndex161", typeof(string));
                        columns.Add("CtrPgBrStart161", typeof(string));
                        columns.Add("CtrPgBrEnd161", typeof(string));
                        columns.Add("CtrCanGrow161", typeof(string));
                        columns.Add("CtrCanShrink161", typeof(string));
                        columns.Add("CtrTogether161", typeof(string));
                        columns.Add("CtrValue161", typeof(string));
                        columns.Add("CtrAction161", typeof(string));
                        columns.Add("CtrVisibility161", typeof(string));
                        columns.Add("CtrToggle161", typeof(string));
                        columns.Add("CtrGrouping161", typeof(string));
                        columns.Add("CtrToolTip161", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("RptCtrId161", typeof(string));

                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmRptCtrWs : RO.Web.AsmxBase
            {
                const int screenId = 90;
                const byte systemId = 3;
                const string programName = "AdmRptCtr90";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmRptCtr90"; }
                protected override string GetMstTableName(bool underlying = true) { return "RptCtr"; }
                protected override string GetDtlTableName(bool underlying = true) { return ""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "RptCtrId" : "RptCtrId161"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"ReportId161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1577"},{"mKey","ReportId161"},{"mVal","ReportId161Text"}, }},
{"PRptCtrId161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlPRptCtrId3S1574"},{"mKey","PRptCtrId161"},{"mVal","PRptCtrId161Text"}, }},
{"RptElmId161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptElmId3S1575"},{"mKey","RptElmId161"},{"mVal","RptElmId161Text"}, }},
{"RptCelId161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptCelId3S1576"},{"mKey","RptCelId161"},{"mVal","RptCelId161Text"}, }},
{"RptStyleId161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptStyleId3S1580"},{"mKey","RptStyleId161"},{"mVal","RptStyleId161Text"}, }},
{"RptCtrTypeCd161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRptCtrTypeCd3S1578"},{"mKey","RptCtrTypeCd161"},{"mVal","RptCtrTypeCd161Text"}, }},
{"CtrVisibility161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCtrVisibility3S1587"},{"mKey","CtrVisibility161"},{"mVal","CtrVisibility161Text"}, }},
{"CtrToggle161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCtrToggle3S1697"},{"mKey","CtrToggle161"},{"mVal","CtrToggle161Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId161"}}},
{"CtrGrouping161", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCtrGrouping3S1595"},{"mKey","CtrGrouping161"},{"mVal","CtrGrouping161Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId161"}}},
};
private DataRow MakeTypRow(DataRow dr){dr["RptCtrId161"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["RptCtrId161"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["RptCtrId161"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            

                    }
                    return dr;
                }

                private AdmRptCtr90 PrepAdmRptCtrData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmRptCtr90 ds = new AdmRptCtr90();
                    DataRow dr = ds.Tables["AdmRptCtr"].NewRow();
                    DataRow drType = ds.Tables["AdmRptCtr"].NewRow();
                    DataRow drDisp = ds.Tables["AdmRptCtr"].NewRow();
            if (bAdd) { dr["RptCtrId161"] = string.Empty; } else { dr["RptCtrId161"] = mst["RptCtrId161"]; }
drType["RptCtrId161"] = "Numeric"; drDisp["RptCtrId161"] = "TextBox";
try { dr["ReportId161"] = mst["ReportId161"]; } catch { }
drType["ReportId161"] = "Numeric"; drDisp["ReportId161"] = "AutoComplete";
try { dr["RptCtrName161"] = (mst["RptCtrName161"] ?? "").Trim().Left(100); } catch { }
drType["RptCtrName161"] = "VarWChar"; drDisp["RptCtrName161"] = "TextBox";
try { dr["PRptCtrId161"] = mst["PRptCtrId161"]; } catch { }
drType["PRptCtrId161"] = "Numeric"; drDisp["PRptCtrId161"] = "AutoComplete";
try { dr["RptElmId161"] = mst["RptElmId161"]; } catch { }
drType["RptElmId161"] = "Numeric"; drDisp["RptElmId161"] = "AutoComplete";
try { dr["RptCelId161"] = mst["RptCelId161"]; } catch { }
drType["RptCelId161"] = "Numeric"; drDisp["RptCelId161"] = "AutoComplete";
try { dr["RptStyleId161"] = mst["RptStyleId161"]; } catch { }
drType["RptStyleId161"] = "Numeric"; drDisp["RptStyleId161"] = "AutoComplete";
try { dr["RptCtrTypeCd161"] = mst["RptCtrTypeCd161"]; } catch { }
drType["RptCtrTypeCd161"] = "Char"; drDisp["RptCtrTypeCd161"] = "DropDownList";
try { dr["CtrTop161"] = (mst["CtrTop161"] ?? "").Trim().Left(9999999); } catch { }
drType["CtrTop161"] = "Decimal"; drDisp["CtrTop161"] = "TextBox";
try { dr["CtrLeft161"] = (mst["CtrLeft161"] ?? "").Trim().Left(9999999); } catch { }
drType["CtrLeft161"] = "Decimal"; drDisp["CtrLeft161"] = "TextBox";
try { dr["CtrHeight161"] = (mst["CtrHeight161"] ?? "").Trim().Left(9999999); } catch { }
drType["CtrHeight161"] = "Decimal"; drDisp["CtrHeight161"] = "TextBox";
try { dr["CtrWidth161"] = (mst["CtrWidth161"] ?? "").Trim().Left(9999999); } catch { }
drType["CtrWidth161"] = "Decimal"; drDisp["CtrWidth161"] = "TextBox";
try { dr["CtrZIndex161"] = (mst["CtrZIndex161"] ?? "").Trim().Left(9999999); } catch { }
drType["CtrZIndex161"] = "Numeric"; drDisp["CtrZIndex161"] = "TextBox";
try { dr["CtrPgBrStart161"] = (mst["CtrPgBrStart161"] ?? "").Trim().Left(1); } catch { }
drType["CtrPgBrStart161"] = "Char"; drDisp["CtrPgBrStart161"] = "CheckBox";
try { dr["CtrPgBrEnd161"] = (mst["CtrPgBrEnd161"] ?? "").Trim().Left(1); } catch { }
drType["CtrPgBrEnd161"] = "Char"; drDisp["CtrPgBrEnd161"] = "CheckBox";
try { dr["CtrCanGrow161"] = (mst["CtrCanGrow161"] ?? "").Trim().Left(1); } catch { }
drType["CtrCanGrow161"] = "Char"; drDisp["CtrCanGrow161"] = "CheckBox";
try { dr["CtrCanShrink161"] = (mst["CtrCanShrink161"] ?? "").Trim().Left(1); } catch { }
drType["CtrCanShrink161"] = "Char"; drDisp["CtrCanShrink161"] = "CheckBox";
try { dr["CtrTogether161"] = (mst["CtrTogether161"] ?? "").Trim().Left(1); } catch { }
drType["CtrTogether161"] = "Char"; drDisp["CtrTogether161"] = "CheckBox";
try { dr["CtrValue161"] = (mst["CtrValue161"] ?? "").Trim().Left(1000); } catch { }
drType["CtrValue161"] = "VarWChar"; drDisp["CtrValue161"] = "TextBox";
try { dr["CtrAction161"] = (mst["CtrAction161"] ?? "").Trim().Left(500); } catch { }
drType["CtrAction161"] = "VarChar"; drDisp["CtrAction161"] = "TextBox";
try { dr["CtrVisibility161"] = mst["CtrVisibility161"]; } catch { }
drType["CtrVisibility161"] = "Char"; drDisp["CtrVisibility161"] = "RadioButtonList";
try { dr["CtrToggle161"] = mst["CtrToggle161"]; } catch { }
drType["CtrToggle161"] = "Numeric"; drDisp["CtrToggle161"] = "AutoComplete";
try { dr["CtrGrouping161"] = mst["CtrGrouping161"]; } catch { }
drType["CtrGrouping161"] = "Numeric"; drDisp["CtrGrouping161"] = "AutoComplete";
try { dr["CtrToolTip161"] = (mst["CtrToolTip161"] ?? "").Trim().Left(200); } catch { }
drType["CtrToolTip161"] = "VarWChar"; drDisp["CtrToolTip161"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmRptCtrDef"].Rows.Add(MakeTypRow(ds.Tables["AdmRptCtrDef"].NewRow()));
                        ds.Tables["AdmRptCtrDef"].Rows.Add(MakeDisRow(ds.Tables["AdmRptCtrDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmRptCtrAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptCtrAdd"].NewRow(), drv, mst["RptCtrId161"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmRptCtrUpd"].Rows.Add(MakeColRow(ds.Tables["AdmRptCtrUpd"].NewRow(), drv, mst["RptCtrId161"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmRptCtrAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptCtrAdd"].NewRow(), drv, mst["RptCtrId161"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmRptCtrDel"].Rows.Add(MakeColRow(ds.Tables["AdmRptCtrDel"].NewRow(), drv, mst["RptCtrId161"], false));
                            }
                        }
                    }
                    ds.Tables["AdmRptCtr"].Rows.Add(dr); ds.Tables["AdmRptCtr"].Rows.Add(drType); ds.Tables["AdmRptCtr"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptCtrId161",""},
{"ReportId161",""},
{"RptCtrName161",""},
{"PRptCtrId161",""},
{"RptElmId161",""},
{"RptCelId161",""},
{"RptStyleId161",""},
{"RptCtrTypeCd161",""},
{"CtrTop161",""},
{"CtrLeft161",""},
{"CtrHeight161",""},
{"CtrWidth161",""},
{"CtrZIndex161",""},
{"CtrPgBrStart161",""},
{"CtrPgBrEnd161",""},
{"CtrCanGrow161",""},
{"CtrCanShrink161",""},
{"CtrTogether161",""},
{"CtrValue161",""},
{"CtrAction161",""},
{"CtrVisibility161",""},
{"CtrToggle161",""},
{"CtrGrouping161",""},
{"CtrToolTip161",""},

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
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptCtr90List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmRptCtr90";
                    context["mKey"] = "RptCtrId161";
                    context["mVal"] = "RptCtrId161Text";
                    context["mTip"] = "RptCtrId161Text";
                    context["mImg"] = "RptCtrId161Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptCtr90ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmRptCtr90", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptCtr90DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmRptCtr90ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmRptCtr90DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["RptCtrId161"];
                    var ds = PrepAdmRptCtrData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["RptCtrId161"]));
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

                    var pid = mst["RptCtrId161"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmRptCtr90", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = null; 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "RptCtrId161", "", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmRptCtrData(mst, dtl, string.IsNullOrEmpty(mst["RptCtrId161"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["RptCtrId161"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlReportId3S1577";context["addnew"] = "Y";context["mKey"] = "ReportId161";context["mVal"] = "ReportId161Text";context["mTip"] = "ReportId161Text";context["mImg"] = "ReportId161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetPRptCtrId161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlPRptCtrId3S1574";context["addnew"] = "Y";context["mKey"] = "PRptCtrId161";context["mVal"] = "PRptCtrId161Text";context["mTip"] = "PRptCtrId161Text";context["mImg"] = "PRptCtrId161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "PRptCtrId161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptElmId161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlRptElmId3S1575";context["addnew"] = "Y";context["mKey"] = "RptElmId161";context["mVal"] = "RptElmId161Text";context["mTip"] = "RptElmId161Text";context["mImg"] = "RptElmId161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptElmId161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptCelId161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlRptCelId3S1576";context["addnew"] = "Y";context["mKey"] = "RptCelId161";context["mVal"] = "RptCelId161Text";context["mTip"] = "RptCelId161Text";context["mImg"] = "RptCelId161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptCelId161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptStyleId161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlRptStyleId3S1580";context["addnew"] = "Y";context["mKey"] = "RptStyleId161";context["mVal"] = "RptStyleId161Text";context["mTip"] = "RptStyleId161Text";context["mImg"] = "RptStyleId161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptStyleId161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRptCtrTypeCd161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlRptCtrTypeCd3S1578", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RptCtrTypeCd161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCtrVisibility161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlCtrVisibility3S1587", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CtrVisibility161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCtrToggle161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCtrToggle3S1697";context["addnew"] = "Y";context["mKey"] = "CtrToggle161";context["mVal"] = "CtrToggle161Text";context["mTip"] = "CtrToggle161Text";context["mImg"] = "CtrToggle161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "ReportId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CtrToggle161", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCtrGrouping161List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCtrGrouping3S1595";context["addnew"] = "Y";context["mKey"] = "CtrGrouping161";context["mVal"] = "CtrGrouping161Text";context["mTip"] = "CtrGrouping161Text";context["mImg"] = "CtrGrouping161Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "ReportId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CtrGrouping161", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmRptCtr");
                    var SearchList = GetAdmRptCtr90List("", 0, "");
                                    var ReportId161LIst = GetReportId161List("", 0, "");
                        var PRptCtrId161LIst = GetPRptCtrId161List("", 0, "");
                        var RptElmId161LIst = GetRptElmId161List("", 0, "");
                        var RptCelId161LIst = GetRptCelId161List("", 0, "");
                        var RptStyleId161LIst = GetRptStyleId161List("", 0, "");
                        var RptCtrTypeCd161LIst = GetRptCtrTypeCd161List("", 0, "");
                        var CtrVisibility161LIst = GetCtrVisibility161List("", 0, "");
                        var CtrToggle161LIst = GetCtrToggle161List("", 0, "");
                        var CtrGrouping161LIst = GetCtrGrouping161List("", 0, "");

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
            