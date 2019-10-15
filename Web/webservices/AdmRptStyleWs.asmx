<%@ WebService Language="C#" Class="RO.Web.AdmRptStyleWs" %>
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
            
                public class AdmRptStyle89 : DataSet
                {
                    public AdmRptStyle89()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmRptStyle")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptStyleDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptStyleAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptStyleUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptStyleDel")));
                        this.DataSetName = "AdmRptStyle89";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmRptStyle89";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("RptStyleId167", typeof(string));
                        columns.Add("DefaultCd167", typeof(string));
                        columns.Add("RptStyleDesc167", typeof(string));
                        columns.Add("BorderColorD167", typeof(string));
                        columns.Add("BorderColorL167", typeof(string));
                        columns.Add("BorderColorR167", typeof(string));
                        columns.Add("BorderColorT167", typeof(string));
                        columns.Add("BorderColorB167", typeof(string));
                        columns.Add("Color167", typeof(string));
                        columns.Add("BgColor167", typeof(string));
                        columns.Add("BgGradType167", typeof(string));
                        columns.Add("BgGradColor167", typeof(string));
                        columns.Add("BgImage167", typeof(string));
                        columns.Add("Direction167", typeof(string));
                        columns.Add("WritingMode167", typeof(string));
                        columns.Add("LineHeight167", typeof(string));
                        columns.Add("Format167", typeof(string));
                        columns.Add("BorderStyleD167", typeof(string));
                        columns.Add("BorderStyleL167", typeof(string));
                        columns.Add("BorderStyleR167", typeof(string));
                        columns.Add("BorderStyleT167", typeof(string));
                        columns.Add("BorderStyleB167", typeof(string));
                        columns.Add("FontStyle167", typeof(string));
                        columns.Add("FontFamily167", typeof(string));
                        columns.Add("FontSize167", typeof(string));
                        columns.Add("FontWeight167", typeof(string));
                        columns.Add("TextDecor167", typeof(string));
                        columns.Add("TextAlign167", typeof(string));
                        columns.Add("VerticalAlign167", typeof(string));
                        columns.Add("BorderWidthD167", typeof(string));
                        columns.Add("BorderWidthL167", typeof(string));
                        columns.Add("BorderWidthR167", typeof(string));
                        columns.Add("BorderWidthT167", typeof(string));
                        columns.Add("BorderWidthB167", typeof(string));
                        columns.Add("PadLeft167", typeof(string));
                        columns.Add("PadRight167", typeof(string));
                        columns.Add("PadTop167", typeof(string));
                        columns.Add("PadBottom167", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("RptStyleId167", typeof(string));

                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmRptStyleWs : RO.Web.AsmxBase
            {
                const int screenId = 89;
                const byte systemId = 3;
                const string programName = "AdmRptStyle89";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmRptStyle89"; }
                protected override string GetMstTableName(bool underlying = true) { return "RptStyle"; }
                protected override string GetDtlTableName(bool underlying = true) { return ""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "RptStyleId" : "RptStyleId167"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"DefaultCd167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDefaultCd3S1571"},{"mKey","DefaultCd167"},{"mVal","DefaultCd167Text"}, }},
{"BgGradType167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBgGradType3S1551"},{"mKey","BgGradType167"},{"mVal","BgGradType167Text"}, }},
{"Direction167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDirection3S1568"},{"mKey","Direction167"},{"mVal","Direction167Text"}, }},
{"WritingMode167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlWritingMode3S1569"},{"mKey","WritingMode167"},{"mVal","WritingMode167Text"}, }},
{"BorderStyleD167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorderStyleD3S1540"},{"mKey","BorderStyleD167"},{"mVal","BorderStyleD167Text"}, }},
{"BorderStyleL167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorderStyleL3S1541"},{"mKey","BorderStyleL167"},{"mVal","BorderStyleL167Text"}, }},
{"BorderStyleR167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorderStyleR3S1542"},{"mKey","BorderStyleR167"},{"mVal","BorderStyleR167Text"}, }},
{"BorderStyleT167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorderStyleT3S1543"},{"mKey","BorderStyleT167"},{"mVal","BorderStyleT167Text"}, }},
{"BorderStyleB167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorderStyleB3S1544"},{"mKey","BorderStyleB167"},{"mVal","BorderStyleB167Text"}, }},
{"FontStyle167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlFontStyle3S1554"},{"mKey","FontStyle167"},{"mVal","FontStyle167Text"}, }},
{"FontWeight167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlFontWeight3S1557"},{"mKey","FontWeight167"},{"mVal","FontWeight167Text"}, }},
{"TextDecor167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTextDecor3S1559"},{"mKey","TextDecor167"},{"mVal","TextDecor167Text"}, }},
{"TextAlign167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTextAlign3S1560"},{"mKey","TextAlign167"},{"mVal","TextAlign167Text"}, }},
{"VerticalAlign167", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlVerticalAlign3S1561"},{"mKey","VerticalAlign167"},{"mVal","VerticalAlign167Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["RptStyleId167"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["RptStyleId167"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["RptStyleId167"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            

                    }
                    return dr;
                }

                private AdmRptStyle89 PrepAdmRptStyleData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmRptStyle89 ds = new AdmRptStyle89();
                    DataRow dr = ds.Tables["AdmRptStyle"].NewRow();
                    DataRow drType = ds.Tables["AdmRptStyle"].NewRow();
                    DataRow drDisp = ds.Tables["AdmRptStyle"].NewRow();
            if (bAdd) { dr["RptStyleId167"] = string.Empty; } else { dr["RptStyleId167"] = mst["RptStyleId167"]; }
drType["RptStyleId167"] = "Numeric"; drDisp["RptStyleId167"] = "TextBox";
try { dr["DefaultCd167"] = mst["DefaultCd167"]; } catch { }
drType["DefaultCd167"] = "Char"; drDisp["DefaultCd167"] = "DropDownList";
try { dr["RptStyleDesc167"] = (mst["RptStyleDesc167"] ?? "").Trim().Left(300); } catch { }
drType["RptStyleDesc167"] = "VarWChar"; drDisp["RptStyleDesc167"] = "TextBox";
try { dr["BorderColorD167"] = (mst["BorderColorD167"] ?? "").Trim().Left(100); } catch { }
drType["BorderColorD167"] = "VarChar"; drDisp["BorderColorD167"] = "TextBox";
try { dr["BorderColorL167"] = (mst["BorderColorL167"] ?? "").Trim().Left(100); } catch { }
drType["BorderColorL167"] = "VarChar"; drDisp["BorderColorL167"] = "TextBox";
try { dr["BorderColorR167"] = (mst["BorderColorR167"] ?? "").Trim().Left(100); } catch { }
drType["BorderColorR167"] = "VarChar"; drDisp["BorderColorR167"] = "TextBox";
try { dr["BorderColorT167"] = (mst["BorderColorT167"] ?? "").Trim().Left(100); } catch { }
drType["BorderColorT167"] = "VarChar"; drDisp["BorderColorT167"] = "TextBox";
try { dr["BorderColorB167"] = (mst["BorderColorB167"] ?? "").Trim().Left(100); } catch { }
drType["BorderColorB167"] = "VarChar"; drDisp["BorderColorB167"] = "TextBox";
try { dr["Color167"] = (mst["Color167"] ?? "").Trim().Left(100); } catch { }
drType["Color167"] = "VarChar"; drDisp["Color167"] = "TextBox";
try { dr["BgColor167"] = (mst["BgColor167"] ?? "").Trim().Left(100); } catch { }
drType["BgColor167"] = "VarChar"; drDisp["BgColor167"] = "TextBox";
try { dr["BgGradType167"] = mst["BgGradType167"]; } catch { }
drType["BgGradType167"] = "Numeric"; drDisp["BgGradType167"] = "DropDownList";
try { dr["BgGradColor167"] = (mst["BgGradColor167"] ?? "").Trim().Left(100); } catch { }
drType["BgGradColor167"] = "VarChar"; drDisp["BgGradColor167"] = "TextBox";
try { dr["BgImage167"] = (mst["BgImage167"] ?? "").Trim().Left(200); } catch { }
drType["BgImage167"] = "VarChar"; drDisp["BgImage167"] = "TextBox";
try { dr["Direction167"] = mst["Direction167"]; } catch { }
drType["Direction167"] = "Char"; drDisp["Direction167"] = "DropDownList";
try { dr["WritingMode167"] = mst["WritingMode167"]; } catch { }
drType["WritingMode167"] = "Char"; drDisp["WritingMode167"] = "DropDownList";
try { dr["LineHeight167"] = (mst["LineHeight167"] ?? "").Trim().Left(9999999); } catch { }
drType["LineHeight167"] = "Numeric"; drDisp["LineHeight167"] = "TextBox";
try { dr["Format167"] = (mst["Format167"] ?? "").Trim().Left(100); } catch { }
drType["Format167"] = "VarChar"; drDisp["Format167"] = "TextBox";
try { dr["BorderStyleD167"] = mst["BorderStyleD167"]; } catch { }
drType["BorderStyleD167"] = "Numeric"; drDisp["BorderStyleD167"] = "DropDownList";
try { dr["BorderStyleL167"] = mst["BorderStyleL167"]; } catch { }
drType["BorderStyleL167"] = "Numeric"; drDisp["BorderStyleL167"] = "DropDownList";
try { dr["BorderStyleR167"] = mst["BorderStyleR167"]; } catch { }
drType["BorderStyleR167"] = "Numeric"; drDisp["BorderStyleR167"] = "DropDownList";
try { dr["BorderStyleT167"] = mst["BorderStyleT167"]; } catch { }
drType["BorderStyleT167"] = "Numeric"; drDisp["BorderStyleT167"] = "DropDownList";
try { dr["BorderStyleB167"] = mst["BorderStyleB167"]; } catch { }
drType["BorderStyleB167"] = "Numeric"; drDisp["BorderStyleB167"] = "DropDownList";
try { dr["FontStyle167"] = mst["FontStyle167"]; } catch { }
drType["FontStyle167"] = "Char"; drDisp["FontStyle167"] = "DropDownList";
try { dr["FontFamily167"] = (mst["FontFamily167"] ?? "").Trim().Left(100); } catch { }
drType["FontFamily167"] = "VarChar"; drDisp["FontFamily167"] = "TextBox";
try { dr["FontSize167"] = (mst["FontSize167"] ?? "").Trim().Left(9999999); } catch { }
drType["FontSize167"] = "Numeric"; drDisp["FontSize167"] = "TextBox";
try { dr["FontWeight167"] = mst["FontWeight167"]; } catch { }
drType["FontWeight167"] = "Numeric"; drDisp["FontWeight167"] = "DropDownList";
try { dr["TextDecor167"] = mst["TextDecor167"]; } catch { }
drType["TextDecor167"] = "Char"; drDisp["TextDecor167"] = "DropDownList";
try { dr["TextAlign167"] = mst["TextAlign167"]; } catch { }
drType["TextAlign167"] = "Char"; drDisp["TextAlign167"] = "DropDownList";
try { dr["VerticalAlign167"] = mst["VerticalAlign167"]; } catch { }
drType["VerticalAlign167"] = "Char"; drDisp["VerticalAlign167"] = "DropDownList";
try { dr["BorderWidthD167"] = (mst["BorderWidthD167"] ?? "").Trim().Left(9999999); } catch { }
drType["BorderWidthD167"] = "Numeric"; drDisp["BorderWidthD167"] = "TextBox";
try { dr["BorderWidthL167"] = (mst["BorderWidthL167"] ?? "").Trim().Left(9999999); } catch { }
drType["BorderWidthL167"] = "Numeric"; drDisp["BorderWidthL167"] = "TextBox";
try { dr["BorderWidthR167"] = (mst["BorderWidthR167"] ?? "").Trim().Left(9999999); } catch { }
drType["BorderWidthR167"] = "Numeric"; drDisp["BorderWidthR167"] = "TextBox";
try { dr["BorderWidthT167"] = (mst["BorderWidthT167"] ?? "").Trim().Left(9999999); } catch { }
drType["BorderWidthT167"] = "Numeric"; drDisp["BorderWidthT167"] = "TextBox";
try { dr["BorderWidthB167"] = (mst["BorderWidthB167"] ?? "").Trim().Left(9999999); } catch { }
drType["BorderWidthB167"] = "Numeric"; drDisp["BorderWidthB167"] = "TextBox";
try { dr["PadLeft167"] = (mst["PadLeft167"] ?? "").Trim().Left(9999999); } catch { }
drType["PadLeft167"] = "Numeric"; drDisp["PadLeft167"] = "TextBox";
try { dr["PadRight167"] = (mst["PadRight167"] ?? "").Trim().Left(9999999); } catch { }
drType["PadRight167"] = "Numeric"; drDisp["PadRight167"] = "TextBox";
try { dr["PadTop167"] = (mst["PadTop167"] ?? "").Trim().Left(9999999); } catch { }
drType["PadTop167"] = "Numeric"; drDisp["PadTop167"] = "TextBox";
try { dr["PadBottom167"] = (mst["PadBottom167"] ?? "").Trim().Left(9999999); } catch { }
drType["PadBottom167"] = "Numeric"; drDisp["PadBottom167"] = "TextBox";

                    if (dtl != null)
                    {
                        ds.Tables["AdmRptStyleDef"].Rows.Add(MakeTypRow(ds.Tables["AdmRptStyleDef"].NewRow()));
                        ds.Tables["AdmRptStyleDef"].Rows.Add(MakeDisRow(ds.Tables["AdmRptStyleDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmRptStyleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptStyleAdd"].NewRow(), drv, mst["RptStyleId167"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmRptStyleUpd"].Rows.Add(MakeColRow(ds.Tables["AdmRptStyleUpd"].NewRow(), drv, mst["RptStyleId167"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmRptStyleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptStyleAdd"].NewRow(), drv, mst["RptStyleId167"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmRptStyleDel"].Rows.Add(MakeColRow(ds.Tables["AdmRptStyleDel"].NewRow(), drv, mst["RptStyleId167"], false));
                            }
                        }
                    }
                    ds.Tables["AdmRptStyle"].Rows.Add(dr); ds.Tables["AdmRptStyle"].Rows.Add(drType); ds.Tables["AdmRptStyle"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"RptStyleId167",""},
{"DefaultCd167",""},
{"RptStyleDesc167",""},
{"BorderColorD167",""},
{"BorderColorL167",""},
{"BorderColorR167",""},
{"BorderColorT167",""},
{"BorderColorB167",""},
{"Color167",""},
{"BgColor167",""},
{"BgGradType167",""},
{"BgGradColor167",""},
{"BgImage167",""},
{"Direction167",""},
{"WritingMode167",""},
{"LineHeight167",""},
{"Format167",""},
{"BorderStyleD167",""},
{"BorderStyleL167",""},
{"BorderStyleR167",""},
{"BorderStyleT167",""},
{"BorderStyleB167",""},
{"FontStyle167",""},
{"FontFamily167",""},
{"FontSize167",""},
{"FontWeight167",""},
{"TextDecor167",""},
{"TextAlign167",""},
{"VerticalAlign167",""},
{"BorderWidthD167",""},
{"BorderWidthL167",""},
{"BorderWidthR167",""},
{"BorderWidthT167",""},
{"BorderWidthB167",""},
{"PadLeft167",""},
{"PadRight167",""},
{"PadTop167",""},
{"PadBottom167",""},

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
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptStyle89List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmRptStyle89";
                    context["mKey"] = "RptStyleId167";
                    context["mVal"] = "RptStyleId167Text";
                    context["mTip"] = "RptStyleId167Text";
                    context["mImg"] = "RptStyleId167Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptStyle89ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmRptStyle89", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmRptStyle89DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmRptStyle89ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmRptStyle89DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["RptStyleId167"];
                    var ds = PrepAdmRptStyleData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["RptStyleId167"]));
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

                    var pid = mst["RptStyleId167"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmRptStyle89", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = null; 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "RptStyleId167", "", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmRptStyleData(mst, dtl, string.IsNullOrEmpty(mst["RptStyleId167"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["RptStyleId167"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDefaultCd167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDefaultCd3S1571", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DefaultCd167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBgGradType167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBgGradType3S1551", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BgGradType167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDirection167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDirection3S1568", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "Direction167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetWritingMode167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlWritingMode3S1569", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "WritingMode167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorderStyleD167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBorderStyleD3S1540", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorderStyleD167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorderStyleL167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBorderStyleL3S1541", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorderStyleL167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorderStyleR167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBorderStyleR3S1542", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorderStyleR167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorderStyleT167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBorderStyleT3S1543", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorderStyleT167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorderStyleB167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlBorderStyleB3S1544", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorderStyleB167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetFontStyle167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlFontStyle3S1554", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "FontStyle167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetFontWeight167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlFontWeight3S1557", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "FontWeight167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTextDecor167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlTextDecor3S1559", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TextDecor167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTextAlign167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlTextAlign3S1560", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TextAlign167", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetVerticalAlign167List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlVerticalAlign3S1561", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "VerticalAlign167", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmRptStyle");
                    var SearchList = GetAdmRptStyle89List("", 0, "");
                                    var DefaultCd167LIst = GetDefaultCd167List("", 0, "");
                        var BgGradType167LIst = GetBgGradType167List("", 0, "");
                        var Direction167LIst = GetDirection167List("", 0, "");
                        var WritingMode167LIst = GetWritingMode167List("", 0, "");
                        var BorderStyleD167LIst = GetBorderStyleD167List("", 0, "");
                        var BorderStyleL167LIst = GetBorderStyleL167List("", 0, "");
                        var BorderStyleR167LIst = GetBorderStyleR167List("", 0, "");
                        var BorderStyleT167LIst = GetBorderStyleT167List("", 0, "");
                        var BorderStyleB167LIst = GetBorderStyleB167List("", 0, "");
                        var FontStyle167LIst = GetFontStyle167List("", 0, "");
                        var FontWeight167LIst = GetFontWeight167List("", 0, "");
                        var TextDecor167LIst = GetTextDecor167List("", 0, "");
                        var TextAlign167LIst = GetTextAlign167List("", 0, "");
                        var VerticalAlign167LIst = GetVerticalAlign167List("", 0, "");

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
            