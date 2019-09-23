<%@ WebService Language="C#" Class="RO.Web.AdmScreenObjWs" %>
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
            
                public class AdmScreenObj10 : DataSet
                {
                    public AdmScreenObj10()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmScreenObj")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenObjDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenObjAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenObjUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenObjDel")));
                        this.DataSetName = "AdmScreenObj10";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmScreenObj10";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("ScreenObjId14", typeof(string));
                        columns.Add("MasterTable14", typeof(string));
                        columns.Add("RequiredValid14", typeof(string));
                        columns.Add("ColumnWrap14", typeof(string));
                        columns.Add("GridGrpCd14", typeof(string));
                        columns.Add("HideOnTablet14", typeof(string));
                        columns.Add("HideOnMobile14", typeof(string));
                        columns.Add("RefreshOnCUD14", typeof(string));
                        columns.Add("TrimOnEntry14", typeof(string));
                        columns.Add("IgnoreConfirm14", typeof(string));
                        columns.Add("ColumnJustify14", typeof(string));
                        columns.Add("ColumnSize14", typeof(string));
                        columns.Add("ColumnHeight14", typeof(string));
                        columns.Add("ResizeWidth14", typeof(string));
                        columns.Add("ResizeHeight14", typeof(string));
                        columns.Add("SortOrder14", typeof(string));
                        columns.Add("ScreenId14", typeof(string));
                        columns.Add("GroupRowId14", typeof(string));
                        columns.Add("GroupColId14", typeof(string));
                        columns.Add("ColumnId14", typeof(string));
                        columns.Add("ColumnName14", typeof(string));
                        columns.Add("DisplayModeId14", typeof(string));
                        columns.Add("DisplayDesc18", typeof(string));
                        columns.Add("DdlKeyColumnId14", typeof(string));
                        columns.Add("DdlRefColumnId14", typeof(string));
                        columns.Add("DdlSrtColumnId14", typeof(string));
                        columns.Add("DdlAdnColumnId14", typeof(string));
                        columns.Add("DdlFtrColumnId14", typeof(string));
                        columns.Add("ColumnLink14", typeof(string));
                        columns.Add("DtlLstPosId14", typeof(string));
                        columns.Add("LabelVertical14", typeof(string));
                        columns.Add("LabelCss14", typeof(string));
                        columns.Add("ContentCss14", typeof(string));
                        columns.Add("DefaultValue14", typeof(string));
                        columns.Add("HyperLinkUrl14", typeof(string));
                        columns.Add("DefAfter14", typeof(string));
                        columns.Add("SystemValue14", typeof(string));
                        columns.Add("DefAlways14", typeof(string));
                        columns.Add("AggregateCd14", typeof(string));
                        columns.Add("GenerateSp14", typeof(string));
                        columns.Add("MaskValid14", typeof(string));
                        columns.Add("RangeValidType14", typeof(string));
                        columns.Add("RangeValidMax14", typeof(string));
                        columns.Add("RangeValidMin14", typeof(string));
                        columns.Add("MatchCd14", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("ScreenObjId14", typeof(string));

                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmScreenObjWs : RO.Web.AsmxBase
            {
                const int screenId = 10;
                const byte systemId = 3;
                const string programName = "AdmScreenObj10";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmScreenObj10"; }
                protected override string GetMstTableName(bool underlying = true) { return "ScreenObj"; }
                protected override string GetDtlTableName(bool underlying = true) { return ""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ScreenObjId" : "ScreenObjId14"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"GridGrpCd14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGridGrpCd3S2016"},{"mKey","GridGrpCd14"},{"mVal","GridGrpCd14Text"}, }},
{"ColumnJustify14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlColumnJustify3S1422"},{"mKey","ColumnJustify14"},{"mVal","ColumnJustify14Text"}, }},
{"ScreenId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S1268"},{"mKey","ScreenId14"},{"mVal","ScreenId14Text"}, }},
{"GroupRowId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGroupRowId3S3136"},{"mKey","GroupRowId14"},{"mVal","GroupRowId14Text"}, }},
{"GroupColId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGroupColId3S1204"},{"mKey","GroupColId14"},{"mVal","GroupColId14Text"}, }},
{"ColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlColumnId3S74"},{"mKey","ColumnId14"},{"mVal","ColumnId14Text"}, }},
{"DisplayModeId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDisplayModeId3S81"},{"mKey","DisplayModeId14"},{"mVal","DisplayModeId14Text"}, }},
{"DdlKeyColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlKeyColumnId3S82"},{"mKey","DdlKeyColumnId14"},{"mVal","DdlKeyColumnId14Text"}, }},
{"DdlRefColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlRefColumnId3S83"},{"mKey","DdlRefColumnId14"},{"mVal","DdlRefColumnId14Text"}, }},
{"DdlSrtColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlSrtColumnId3S1269"},{"mKey","DdlSrtColumnId14"},{"mVal","DdlSrtColumnId14Text"}, }},
{"DdlAdnColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlAdnColumnId3S1281"},{"mKey","DdlAdnColumnId14"},{"mVal","DdlAdnColumnId14Text"}, }},
{"DdlFtrColumnId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlFtrColumnId3S1282"},{"mKey","DdlFtrColumnId14"},{"mVal","DdlFtrColumnId14Text"}, }},
{"DtlLstPosId14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDtlLstPosId3S4206"},{"mKey","DtlLstPosId14"},{"mVal","DtlLstPosId14Text"}, }},
{"AggregateCd14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAggregateCd3S1238"},{"mKey","AggregateCd14"},{"mVal","AggregateCd14Text"}, }},
{"MatchCd14", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlMatchCd3S1806"},{"mKey","MatchCd14"},{"mVal","MatchCd14Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["ScreenObjId14"] = System.Data.OleDb.OleDbType.Numeric.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["ScreenObjId14"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["ScreenObjId14"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            

                    }
                    return dr;
                }

                private AdmScreenObj10 PrepAdmScreenObjData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmScreenObj10 ds = new AdmScreenObj10();
                    DataRow dr = ds.Tables["AdmScreenObj"].NewRow();
                    DataRow drType = ds.Tables["AdmScreenObj"].NewRow();
                    DataRow drDisp = ds.Tables["AdmScreenObj"].NewRow();
            if (bAdd) { dr["ScreenObjId14"] = string.Empty; } else { dr["ScreenObjId14"] = mst["ScreenObjId14"]; }
drType["ScreenObjId14"] = "Numeric"; drDisp["ScreenObjId14"] = "TextBox";
try { dr["MasterTable14"] = (mst["MasterTable14"] ?? "").Trim().Left(1); } catch { }
drType["MasterTable14"] = "Char"; drDisp["MasterTable14"] = "CheckBox";
try { dr["RequiredValid14"] = (mst["RequiredValid14"] ?? "").Trim().Left(1); } catch { }
drType["RequiredValid14"] = "Char"; drDisp["RequiredValid14"] = "CheckBox";
try { dr["ColumnWrap14"] = (mst["ColumnWrap14"] ?? "").Trim().Left(1); } catch { }
drType["ColumnWrap14"] = "Char"; drDisp["ColumnWrap14"] = "CheckBox";
try { dr["GridGrpCd14"] = mst["GridGrpCd14"]; } catch { }
drType["GridGrpCd14"] = "Char"; drDisp["GridGrpCd14"] = "DropDownList";
try { dr["HideOnTablet14"] = (mst["HideOnTablet14"] ?? "").Trim().Left(1); } catch { }
drType["HideOnTablet14"] = "Char"; drDisp["HideOnTablet14"] = "CheckBox";
try { dr["HideOnMobile14"] = (mst["HideOnMobile14"] ?? "").Trim().Left(1); } catch { }
drType["HideOnMobile14"] = "Char"; drDisp["HideOnMobile14"] = "CheckBox";
try { dr["RefreshOnCUD14"] = (mst["RefreshOnCUD14"] ?? "").Trim().Left(1); } catch { }
drType["RefreshOnCUD14"] = "Char"; drDisp["RefreshOnCUD14"] = "CheckBox";
try { dr["TrimOnEntry14"] = (mst["TrimOnEntry14"] ?? "").Trim().Left(1); } catch { }
drType["TrimOnEntry14"] = "Char"; drDisp["TrimOnEntry14"] = "CheckBox";
try { dr["IgnoreConfirm14"] = (mst["IgnoreConfirm14"] ?? "").Trim().Left(1); } catch { }
drType["IgnoreConfirm14"] = "Char"; drDisp["IgnoreConfirm14"] = "CheckBox";
try { dr["ColumnJustify14"] = mst["ColumnJustify14"]; } catch { }
drType["ColumnJustify14"] = "Char"; drDisp["ColumnJustify14"] = "DropDownList";
try { dr["ColumnSize14"] = (mst["ColumnSize14"] ?? "").Trim().Left(9999999); } catch { }
drType["ColumnSize14"] = "Numeric"; drDisp["ColumnSize14"] = "TextBox";
try { dr["ColumnHeight14"] = (mst["ColumnHeight14"] ?? "").Trim().Left(9999999); } catch { }
drType["ColumnHeight14"] = "Numeric"; drDisp["ColumnHeight14"] = "TextBox";
try { dr["ResizeWidth14"] = (mst["ResizeWidth14"] ?? "").Trim().Left(9999999); } catch { }
drType["ResizeWidth14"] = "Numeric"; drDisp["ResizeWidth14"] = "TextBox";
try { dr["ResizeHeight14"] = (mst["ResizeHeight14"] ?? "").Trim().Left(9999999); } catch { }
drType["ResizeHeight14"] = "Numeric"; drDisp["ResizeHeight14"] = "TextBox";
try { dr["SortOrder14"] = (mst["SortOrder14"] ?? "").Trim().Left(9999999); } catch { }
drType["SortOrder14"] = "Numeric"; drDisp["SortOrder14"] = "TextBox";
try { dr["ScreenId14"] = mst["ScreenId14"]; } catch { }
drType["ScreenId14"] = "Numeric"; drDisp["ScreenId14"] = "AutoComplete";
try { dr["GroupRowId14"] = mst["GroupRowId14"]; } catch { }
drType["GroupRowId14"] = "Numeric"; drDisp["GroupRowId14"] = "AutoComplete";
try { dr["GroupColId14"] = mst["GroupColId14"]; } catch { }
drType["GroupColId14"] = "Numeric"; drDisp["GroupColId14"] = "AutoComplete";
try { dr["ColumnId14"] = mst["ColumnId14"]; } catch { }
drType["ColumnId14"] = "Numeric"; drDisp["ColumnId14"] = "AutoComplete";
try { dr["ColumnName14"] = (mst["ColumnName14"] ?? "").Trim().Left(50); } catch { }
drType["ColumnName14"] = "VarChar"; drDisp["ColumnName14"] = "TextBox";
try { dr["DisplayModeId14"] = mst["DisplayModeId14"]; } catch { }
drType["DisplayModeId14"] = "Numeric"; drDisp["DisplayModeId14"] = "DropDownList";
try { dr["DisplayDesc18"] = mst["DisplayDesc18"]; } catch { }
drType["DisplayDesc18"] = "VarWChar"; drDisp["DisplayDesc18"] = "MultiLine";
try { dr["DdlKeyColumnId14"] = mst["DdlKeyColumnId14"]; } catch { }
drType["DdlKeyColumnId14"] = "Numeric"; drDisp["DdlKeyColumnId14"] = "AutoComplete";
try { dr["DdlRefColumnId14"] = mst["DdlRefColumnId14"]; } catch { }
drType["DdlRefColumnId14"] = "Numeric"; drDisp["DdlRefColumnId14"] = "AutoComplete";
try { dr["DdlSrtColumnId14"] = mst["DdlSrtColumnId14"]; } catch { }
drType["DdlSrtColumnId14"] = "Numeric"; drDisp["DdlSrtColumnId14"] = "AutoComplete";
try { dr["DdlAdnColumnId14"] = mst["DdlAdnColumnId14"]; } catch { }
drType["DdlAdnColumnId14"] = "Numeric"; drDisp["DdlAdnColumnId14"] = "AutoComplete";
try { dr["DdlFtrColumnId14"] = mst["DdlFtrColumnId14"]; } catch { }
drType["DdlFtrColumnId14"] = "Numeric"; drDisp["DdlFtrColumnId14"] = "AutoComplete";
try { dr["ColumnLink14"] = (mst["ColumnLink14"] ?? "").Trim().Left(1000); } catch { }
drType["ColumnLink14"] = "VarChar"; drDisp["ColumnLink14"] = "TextBox";
try { dr["DtlLstPosId14"] = mst["DtlLstPosId14"]; } catch { }
drType["DtlLstPosId14"] = "Numeric"; drDisp["DtlLstPosId14"] = "DropDownList";
try { dr["LabelVertical14"] = (mst["LabelVertical14"] ?? "").Trim().Left(1); } catch { }
drType["LabelVertical14"] = "Char"; drDisp["LabelVertical14"] = "CheckBox";
try { dr["LabelCss14"] = (mst["LabelCss14"] ?? "").Trim().Left(1000); } catch { }
drType["LabelCss14"] = "VarChar"; drDisp["LabelCss14"] = "TextBox";
try { dr["ContentCss14"] = (mst["ContentCss14"] ?? "").Trim().Left(1000); } catch { }
drType["ContentCss14"] = "VarChar"; drDisp["ContentCss14"] = "TextBox";
try { dr["DefaultValue14"] = (mst["DefaultValue14"] ?? "").Trim().Left(200); } catch { }
drType["DefaultValue14"] = "VarWChar"; drDisp["DefaultValue14"] = "TextBox";
try { dr["HyperLinkUrl14"] = (mst["HyperLinkUrl14"] ?? "").Trim().Left(200); } catch { }
drType["HyperLinkUrl14"] = "VarWChar"; drDisp["HyperLinkUrl14"] = "TextBox";
try { dr["DefAfter14"] = (mst["DefAfter14"] ?? "").Trim().Left(1); } catch { }
drType["DefAfter14"] = "Char"; drDisp["DefAfter14"] = "CheckBox";
try { dr["SystemValue14"] = (mst["SystemValue14"] ?? "").Trim().Left(200); } catch { }
drType["SystemValue14"] = "VarWChar"; drDisp["SystemValue14"] = "TextBox";
try { dr["DefAlways14"] = (mst["DefAlways14"] ?? "").Trim().Left(1); } catch { }
drType["DefAlways14"] = "Char"; drDisp["DefAlways14"] = "CheckBox";
try { dr["AggregateCd14"] = mst["AggregateCd14"]; } catch { }
drType["AggregateCd14"] = "Char"; drDisp["AggregateCd14"] = "DropDownList";
try { dr["GenerateSp14"] = (mst["GenerateSp14"] ?? "").Trim().Left(1); } catch { }
drType["GenerateSp14"] = "Char"; drDisp["GenerateSp14"] = "CheckBox";
try { dr["MaskValid14"] = (mst["MaskValid14"] ?? "").Trim().Left(100); } catch { }
drType["MaskValid14"] = "VarChar"; drDisp["MaskValid14"] = "TextBox";
try { dr["RangeValidType14"] = (mst["RangeValidType14"] ?? "").Trim().Left(50); } catch { }
drType["RangeValidType14"] = "VarChar"; drDisp["RangeValidType14"] = "TextBox";
try { dr["RangeValidMax14"] = (mst["RangeValidMax14"] ?? "").Trim().Left(50); } catch { }
drType["RangeValidMax14"] = "VarChar"; drDisp["RangeValidMax14"] = "TextBox";
try { dr["RangeValidMin14"] = (mst["RangeValidMin14"] ?? "").Trim().Left(50); } catch { }
drType["RangeValidMin14"] = "VarChar"; drDisp["RangeValidMin14"] = "TextBox";
try { dr["MatchCd14"] = mst["MatchCd14"]; } catch { }
drType["MatchCd14"] = "Char"; drDisp["MatchCd14"] = "DropDownList";

                    if (dtl != null)
                    {
                        ds.Tables["AdmScreenObjDef"].Rows.Add(MakeTypRow(ds.Tables["AdmScreenObjDef"].NewRow()));
                        ds.Tables["AdmScreenObjDef"].Rows.Add(MakeDisRow(ds.Tables["AdmScreenObjDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmScreenObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenObjAdd"].NewRow(), drv, mst["ScreenObjId14"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmScreenObjUpd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenObjUpd"].NewRow(), drv, mst["ScreenObjId14"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmScreenObjAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenObjAdd"].NewRow(), drv, mst["ScreenObjId14"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmScreenObjDel"].Rows.Add(MakeColRow(ds.Tables["AdmScreenObjDel"].NewRow(), drv, mst["ScreenObjId14"], false));
                            }
                        }
                    }
                    ds.Tables["AdmScreenObj"].Rows.Add(dr); ds.Tables["AdmScreenObj"].Rows.Add(drType); ds.Tables["AdmScreenObj"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ScreenObjId14",""},
{"MasterTable14",""},
{"RequiredValid14",""},
{"ColumnWrap14","Y"},
{"GridGrpCd14","N"},
{"HideOnTablet14","N"},
{"HideOnMobile14","N"},
{"RefreshOnCUD14",""},
{"TrimOnEntry14","Y"},
{"IgnoreConfirm14","Y"},
{"ColumnJustify14","L"},
{"ColumnSize14",""},
{"ColumnHeight14",""},
{"ResizeWidth14",""},
{"ResizeHeight14",""},
{"SortOrder14",""},
{"ScreenId14",""},
{"GroupRowId14","78"},
{"GroupColId14","851"},
{"ColumnId14",""},
{"ColumnName14",""},
{"DisplayModeId14",""},
{"DisplayDesc18",""},
{"DdlKeyColumnId14",""},
{"DdlRefColumnId14",""},
{"DdlSrtColumnId14",""},
{"DdlAdnColumnId14",""},
{"DdlFtrColumnId14",""},
{"ColumnLink14",""},
{"DtlLstPosId14",""},
{"LabelVertical14","N"},
{"LabelCss14",""},
{"ContentCss14",""},
{"DefaultValue14",""},
{"HyperLinkUrl14",""},
{"DefAfter14","N"},
{"SystemValue14",""},
{"DefAlways14","N"},
{"AggregateCd14",""},
{"GenerateSp14","Y"},
{"MaskValid14",""},
{"RangeValidType14",""},
{"RangeValidMax14",""},
{"RangeValidMin14",""},
{"MatchCd14",""},

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
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenObj10List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmScreenObj10";
                    context["mKey"] = "ScreenObjId14";
                    context["mVal"] = "ScreenObjId14Text";
                    context["mTip"] = "ScreenObjId14Text";
                    context["mImg"] = "ScreenObjId14Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenObj10ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmScreenObj10", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreenObj10DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmScreenObj10ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmScreenObj10DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["ScreenObjId14"];
                    var ds = PrepAdmScreenObjData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["ScreenObjId14"]));
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

                    var pid = mst["ScreenObjId14"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmScreenObj10", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = null; 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ScreenObjId14", "", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmScreenObjData(mst, dtl, string.IsNullOrEmpty(mst["ScreenObjId14"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["ScreenObjId14"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGridGrpCd14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlGridGrpCd3S2016", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GridGrpCd14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetColumnJustify14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlColumnJustify3S1422", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ColumnJustify14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlScreenId3S1268";context["addnew"] = "Y";context["mKey"] = "ScreenId14";context["mVal"] = "ScreenId14Text";context["mTip"] = "ScreenId14Text";context["mImg"] = "ScreenId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGroupRowId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlGroupRowId3S3136";context["addnew"] = "Y";context["mKey"] = "GroupRowId14";context["mVal"] = "GroupRowId14Text";context["mTip"] = "GroupRowId14Text";context["mImg"] = "GroupRowId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GroupRowId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGroupColId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlGroupColId3S1204";context["addnew"] = "Y";context["mKey"] = "GroupColId14";context["mVal"] = "GroupColId14Text";context["mTip"] = "GroupColId14Text";context["mImg"] = "GroupColId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GroupColId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlColumnId3S74";context["addnew"] = "Y";context["mKey"] = "ColumnId14";context["mVal"] = "ColumnId14Text";context["mTip"] = "ColumnId14Text";context["mImg"] = "ColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDisplayModeId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDisplayModeId3S81", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DisplayModeId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlKeyColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlKeyColumnId3S82";context["addnew"] = "Y";context["mKey"] = "DdlKeyColumnId14";context["mVal"] = "DdlKeyColumnId14Text";context["mTip"] = "DdlKeyColumnId14Text";context["mImg"] = "DdlKeyColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlKeyColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlRefColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlRefColumnId3S83";context["addnew"] = "Y";context["mKey"] = "DdlRefColumnId14";context["mVal"] = "DdlRefColumnId14Text";context["mTip"] = "DdlRefColumnId14Text";context["mImg"] = "DdlRefColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlRefColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlSrtColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlSrtColumnId3S1269";context["addnew"] = "Y";context["mKey"] = "DdlSrtColumnId14";context["mVal"] = "DdlSrtColumnId14Text";context["mTip"] = "DdlSrtColumnId14Text";context["mImg"] = "DdlSrtColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlSrtColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlAdnColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlAdnColumnId3S1281";context["addnew"] = "Y";context["mKey"] = "DdlAdnColumnId14";context["mVal"] = "DdlAdnColumnId14Text";context["mTip"] = "DdlAdnColumnId14Text";context["mImg"] = "DdlAdnColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlAdnColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlFtrColumnId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDdlFtrColumnId3S1282";context["addnew"] = "Y";context["mKey"] = "DdlFtrColumnId14";context["mVal"] = "DdlFtrColumnId14Text";context["mTip"] = "DdlFtrColumnId14Text";context["mImg"] = "DdlFtrColumnId14Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlFtrColumnId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDtlLstPosId14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlDtlLstPosId3S4206", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DtlLstPosId14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAggregateCd14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlAggregateCd3S1238", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AggregateCd14", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetMatchCd14List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlMatchCd3S1806", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "MatchCd14", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmScreenObj");
                    var SearchList = GetAdmScreenObj10List("", 0, "");
                                    var GridGrpCd14LIst = GetGridGrpCd14List("", 0, "");
                        var ColumnJustify14LIst = GetColumnJustify14List("", 0, "");
                        var ScreenId14LIst = GetScreenId14List("", 0, "");
                        var GroupRowId14LIst = GetGroupRowId14List("", 0, "");
                        var GroupColId14LIst = GetGroupColId14List("", 0, "");
                        var ColumnId14LIst = GetColumnId14List("", 0, "");
                        var DisplayModeId14LIst = GetDisplayModeId14List("", 0, "");
                        var DdlKeyColumnId14LIst = GetDdlKeyColumnId14List("", 0, "");
                        var DdlRefColumnId14LIst = GetDdlRefColumnId14List("", 0, "");
                        var DdlSrtColumnId14LIst = GetDdlSrtColumnId14List("", 0, "");
                        var DdlAdnColumnId14LIst = GetDdlAdnColumnId14List("", 0, "");
                        var DdlFtrColumnId14LIst = GetDdlFtrColumnId14List("", 0, "");
                        var DtlLstPosId14LIst = GetDtlLstPosId14List("", 0, "");
                        var AggregateCd14LIst = GetAggregateCd14List("", 0, "");
                        var MatchCd14LIst = GetMatchCd14List("", 0, "");

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
            