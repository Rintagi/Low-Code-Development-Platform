<%@ WebService Language="C#" Class="RO.Web.AdmScreenWs" %>
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
            
                public class AdmScreen9 : DataSet
                {
                    public AdmScreen9()
                    {
                        this.Tables.Add(MakeColumns(new DataTable("AdmScreen")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenDef")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenAdd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenUpd")));
                        this.Tables.Add(MakeDtlColumns(new DataTable("AdmScreenDel")));
                        this.DataSetName = "AdmScreen9";
                        this.Namespace = "http://Rintagi.com/DataSet/AdmScreen9";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
                        columns.Add("ScreenId15", typeof(string));
                        columns.Add("ProgramName15", typeof(string));
                        columns.Add("ScreenTypeId15", typeof(string));
                        columns.Add("ViewOnly15", typeof(string));
                        columns.Add("SearchAscending15", typeof(string));
                        columns.Add("MasterTableId15", typeof(string));
                        columns.Add("SearchTableId15", typeof(string));
                        columns.Add("SearchId15", typeof(string));
                        columns.Add("SearchIdR15", typeof(string));
                        columns.Add("SearchDtlId15", typeof(string));
                        columns.Add("SearchDtlIdR15", typeof(string));
                        columns.Add("SearchUrlId15", typeof(string));
                        columns.Add("SearchImgId15", typeof(string));
                        columns.Add("DetailTableId15", typeof(string));
                        columns.Add("GridRows15", typeof(string));
                        columns.Add("HasDeleteAll15", typeof(string));
                        columns.Add("ShowGridHead15", typeof(string));
                        columns.Add("GenerateSc15", typeof(string));
                        columns.Add("GenerateSr15", typeof(string));
                        columns.Add("ValidateReq15", typeof(string));
                        columns.Add("DeferError15", typeof(string));
                        columns.Add("AuthRequired15", typeof(string));
                        columns.Add("GenAudit15", typeof(string));
                        columns.Add("ScreenObj15", typeof(string));
                        columns.Add("ScreenFilter", typeof(string));
                        columns.Add("MoreInfo", typeof(string));
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
columns.Add("ScreenId15", typeof(string));
                        columns.Add("ScreenHlpId16", typeof(string));
                        columns.Add("CultureId16", typeof(string));
                        columns.Add("ScreenTitle16", typeof(string));
                        columns.Add("DefaultHlpMsg16", typeof(string));
                        columns.Add("FootNote16", typeof(string));
                        columns.Add("AddMsg16", typeof(string));
                        columns.Add("UpdMsg16", typeof(string));
                        columns.Add("DelMsg16", typeof(string));
                        columns.Add("IncrementMsg16", typeof(string));
                        columns.Add("NoMasterMsg16", typeof(string));
                        columns.Add("NoDetailMsg16", typeof(string));
                        columns.Add("AddMasterMsg16", typeof(string));
                        columns.Add("AddDetailMsg16", typeof(string));
                        columns.Add("MasterLstTitle16", typeof(string));
                        columns.Add("MasterLstSubtitle16", typeof(string));
                        columns.Add("MasterRecTitle16", typeof(string));
                        columns.Add("MasterRecSubtitle16", typeof(string));
                        columns.Add("MasterFoundMsg16", typeof(string));
                        columns.Add("DetailLstTitle16", typeof(string));
                        columns.Add("DetailLstSubtitle16", typeof(string));
                        columns.Add("DetailRecTitle16", typeof(string));
                        columns.Add("DetailRecSubtitle16", typeof(string));
                        columns.Add("DetailFoundMsg16", typeof(string));
                        return dt;
                    }
                }
            
            [ScriptService()]
            [WebService(Namespace = "http://Rintagi.com/")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class AdmScreenWs : RO.Web.AsmxBase
            {
                const int screenId = 9;
                const byte systemId = 3;
                const string programName = "AdmScreen9";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return "GetLisAdmScreen9"; }
                protected override string GetMstTableName(bool underlying = true) { return "Screen"; }
                protected override string GetDtlTableName(bool underlying = true) { return "ScreenHlp"; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ScreenId" : "ScreenId15"; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "ScreenHlpId" : "ScreenHlpId16"; }
            
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            {"ScreenTypeId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenTypeId3S45"},{"mKey","ScreenTypeId15"},{"mVal","ScreenTypeId15Text"}, }},
{"ViewOnly15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlViewOnly3S3103"},{"mKey","ViewOnly15"},{"mVal","ViewOnly15Text"}, }},
{"MasterTableId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlMasterTableId3S59"},{"mKey","MasterTableId15"},{"mVal","MasterTableId15Text"}, }},
{"SearchTableId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchTableId3S4145"},{"mKey","SearchTableId15"},{"mVal","SearchTableId15Text"}, }},
{"SearchId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchId3S46"},{"mKey","SearchId15"},{"mVal","SearchId15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"SearchIdR15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchIdR3S4164"},{"mKey","SearchIdR15"},{"mVal","SearchIdR15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"SearchDtlId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchDtlId3S4148"},{"mKey","SearchDtlId15"},{"mVal","SearchDtlId15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"SearchDtlIdR15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchDtlIdR3S4165"},{"mKey","SearchDtlIdR15"},{"mVal","SearchDtlIdR15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"SearchUrlId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchUrlId3S4147"},{"mKey","SearchUrlId15"},{"mVal","SearchUrlId15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"SearchImgId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlSearchImgId3S4146"},{"mKey","SearchImgId15"},{"mVal","SearchImgId15Text"}, {"refCol","TableId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","SearchTableId15"}}},
{"DetailTableId15", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDetailTableId3S60"},{"mKey","DetailTableId15"},{"mVal","DetailTableId15Text"}, }},
{"CultureId16", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S53"},{"mKey","CultureId16"},{"mVal","CultureId16Text"}, }},
};
private DataRow MakeTypRow(DataRow dr){dr["ScreenId15"] = System.Data.OleDb.OleDbType.Numeric.ToString();dr["ScreenHlpId16"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["CultureId16"] = System.Data.OleDb.OleDbType.Numeric.ToString();
dr["ScreenTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DefaultHlpMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["FootNote16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["AddMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["UpdMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DelMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["IncrementMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["NoMasterMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["NoDetailMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["AddMasterMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["AddDetailMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["MasterLstTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["MasterLstSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["MasterRecTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["MasterRecSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["MasterFoundMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DetailLstTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DetailLstSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DetailRecTitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DetailRecSubtitle16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();
dr["DetailFoundMsg16"] = System.Data.OleDb.OleDbType.VarWChar.ToString();

                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            dr["ScreenId15"] = "TextBox";dr["ScreenHlpId16"] = "TextBox";
dr["CultureId16"] = "AutoComplete";
dr["ScreenTitle16"] = "TextBox";
dr["DefaultHlpMsg16"] = "TextBox";
dr["FootNote16"] = "TextBox";
dr["AddMsg16"] = "TextBox";
dr["UpdMsg16"] = "TextBox";
dr["DelMsg16"] = "TextBox";
dr["IncrementMsg16"] = "TextBox";
dr["NoMasterMsg16"] = "TextBox";
dr["NoDetailMsg16"] = "TextBox";
dr["AddMasterMsg16"] = "TextBox";
dr["AddDetailMsg16"] = "TextBox";
dr["MasterLstTitle16"] = "TextBox";
dr["MasterLstSubtitle16"] = "TextBox";
dr["MasterRecTitle16"] = "TextBox";
dr["MasterRecSubtitle16"] = "TextBox";
dr["MasterFoundMsg16"] = "TextBox";
dr["DetailLstTitle16"] = "TextBox";
dr["DetailLstSubtitle16"] = "TextBox";
dr["DetailRecTitle16"] = "TextBox";
dr["DetailRecSubtitle16"] = "TextBox";
dr["DetailFoundMsg16"] = "TextBox";

                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            dr["ScreenId15"] = keyId;
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            dr["ScreenHlpId16"] = (drv["ScreenHlpId16"] ?? "").ToString().Trim().Left(9999999);
dr["CultureId16"] = drv["CultureId16"];
dr["ScreenTitle16"] = (drv["ScreenTitle16"] ?? "").ToString().Trim().Left(50);
dr["DefaultHlpMsg16"] = (drv["DefaultHlpMsg16"] ?? "").ToString().Trim().Left(0);
dr["FootNote16"] = (drv["FootNote16"] ?? "").ToString().Trim().Left(400);
dr["AddMsg16"] = (drv["AddMsg16"] ?? "").ToString().Trim().Left(100);
dr["UpdMsg16"] = (drv["UpdMsg16"] ?? "").ToString().Trim().Left(100);
dr["DelMsg16"] = (drv["DelMsg16"] ?? "").ToString().Trim().Left(100);
dr["IncrementMsg16"] = (drv["IncrementMsg16"] ?? "").ToString().Trim().Left(100);
dr["NoMasterMsg16"] = (drv["NoMasterMsg16"] ?? "").ToString().Trim().Left(100);
dr["NoDetailMsg16"] = (drv["NoDetailMsg16"] ?? "").ToString().Trim().Left(100);
dr["AddMasterMsg16"] = (drv["AddMasterMsg16"] ?? "").ToString().Trim().Left(100);
dr["AddDetailMsg16"] = (drv["AddDetailMsg16"] ?? "").ToString().Trim().Left(100);
dr["MasterLstTitle16"] = (drv["MasterLstTitle16"] ?? "").ToString().Trim().Left(100);
dr["MasterLstSubtitle16"] = (drv["MasterLstSubtitle16"] ?? "").ToString().Trim().Left(100);
dr["MasterRecTitle16"] = (drv["MasterRecTitle16"] ?? "").ToString().Trim().Left(100);
dr["MasterRecSubtitle16"] = (drv["MasterRecSubtitle16"] ?? "").ToString().Trim().Left(100);
dr["MasterFoundMsg16"] = (drv["MasterFoundMsg16"] ?? "").ToString().Trim().Left(100);
dr["DetailLstTitle16"] = (drv["DetailLstTitle16"] ?? "").ToString().Trim().Left(100);
dr["DetailLstSubtitle16"] = (drv["DetailLstSubtitle16"] ?? "").ToString().Trim().Left(100);
dr["DetailRecTitle16"] = (drv["DetailRecTitle16"] ?? "").ToString().Trim().Left(100);
dr["DetailRecSubtitle16"] = (drv["DetailRecSubtitle16"] ?? "").ToString().Trim().Left(100);
dr["DetailFoundMsg16"] = (drv["DetailFoundMsg16"] ?? "").ToString().Trim().Left(100);

                    }
                    return dr;
                }

                private AdmScreen9 PrepAdmScreenData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    AdmScreen9 ds = new AdmScreen9();
                    DataRow dr = ds.Tables["AdmScreen"].NewRow();
                    DataRow drType = ds.Tables["AdmScreen"].NewRow();
                    DataRow drDisp = ds.Tables["AdmScreen"].NewRow();
            if (bAdd) { dr["ScreenId15"] = string.Empty; } else { dr["ScreenId15"] = mst["ScreenId15"]; }
drType["ScreenId15"] = "Numeric"; drDisp["ScreenId15"] = "TextBox";
try { dr["ProgramName15"] = (mst["ProgramName15"] ?? "").Trim().Left(20); } catch { }
drType["ProgramName15"] = "VarChar"; drDisp["ProgramName15"] = "TextBox";
try { dr["ScreenTypeId15"] = mst["ScreenTypeId15"]; } catch { }
drType["ScreenTypeId15"] = "Numeric"; drDisp["ScreenTypeId15"] = "DropDownList";
try { dr["ViewOnly15"] = mst["ViewOnly15"]; } catch { }
drType["ViewOnly15"] = "Char"; drDisp["ViewOnly15"] = "DropDownList";
try { dr["SearchAscending15"] = (mst["SearchAscending15"] ?? "").Trim().Left(1); } catch { }
drType["SearchAscending15"] = "Char"; drDisp["SearchAscending15"] = "CheckBox";
try { dr["MasterTableId15"] = mst["MasterTableId15"]; } catch { }
drType["MasterTableId15"] = "Numeric"; drDisp["MasterTableId15"] = "AutoComplete";
try { dr["SearchTableId15"] = mst["SearchTableId15"]; } catch { }
drType["SearchTableId15"] = "Numeric"; drDisp["SearchTableId15"] = "AutoComplete";
try { dr["SearchId15"] = mst["SearchId15"]; } catch { }
drType["SearchId15"] = "Numeric"; drDisp["SearchId15"] = "AutoComplete";
try { dr["SearchIdR15"] = mst["SearchIdR15"]; } catch { }
drType["SearchIdR15"] = "Numeric"; drDisp["SearchIdR15"] = "AutoComplete";
try { dr["SearchDtlId15"] = mst["SearchDtlId15"]; } catch { }
drType["SearchDtlId15"] = "Numeric"; drDisp["SearchDtlId15"] = "AutoComplete";
try { dr["SearchDtlIdR15"] = mst["SearchDtlIdR15"]; } catch { }
drType["SearchDtlIdR15"] = "Numeric"; drDisp["SearchDtlIdR15"] = "AutoComplete";
try { dr["SearchUrlId15"] = mst["SearchUrlId15"]; } catch { }
drType["SearchUrlId15"] = "Numeric"; drDisp["SearchUrlId15"] = "AutoComplete";
try { dr["SearchImgId15"] = mst["SearchImgId15"]; } catch { }
drType["SearchImgId15"] = "Numeric"; drDisp["SearchImgId15"] = "AutoComplete";
try { dr["DetailTableId15"] = mst["DetailTableId15"]; } catch { }
drType["DetailTableId15"] = "Numeric"; drDisp["DetailTableId15"] = "AutoComplete";
try { dr["GridRows15"] = (mst["GridRows15"] ?? "").Trim().Left(9999999); } catch { }
drType["GridRows15"] = "Numeric"; drDisp["GridRows15"] = "TextBox";
try { dr["HasDeleteAll15"] = (mst["HasDeleteAll15"] ?? "").Trim().Left(1); } catch { }
drType["HasDeleteAll15"] = "Char"; drDisp["HasDeleteAll15"] = "CheckBox";
try { dr["ShowGridHead15"] = (mst["ShowGridHead15"] ?? "").Trim().Left(1); } catch { }
drType["ShowGridHead15"] = "Char"; drDisp["ShowGridHead15"] = "CheckBox";
try { dr["GenerateSc15"] = (mst["GenerateSc15"] ?? "").Trim().Left(1); } catch { }
drType["GenerateSc15"] = "Char"; drDisp["GenerateSc15"] = "CheckBox";
try { dr["GenerateSr15"] = (mst["GenerateSr15"] ?? "").Trim().Left(1); } catch { }
drType["GenerateSr15"] = "Char"; drDisp["GenerateSr15"] = "CheckBox";
try { dr["ValidateReq15"] = (mst["ValidateReq15"] ?? "").Trim().Left(1); } catch { }
drType["ValidateReq15"] = "Char"; drDisp["ValidateReq15"] = "CheckBox";
try { dr["DeferError15"] = (mst["DeferError15"] ?? "").Trim().Left(1); } catch { }
drType["DeferError15"] = "Char"; drDisp["DeferError15"] = "CheckBox";
try { dr["AuthRequired15"] = (mst["AuthRequired15"] ?? "").Trim().Left(1); } catch { }
drType["AuthRequired15"] = "Char"; drDisp["AuthRequired15"] = "CheckBox";
try { dr["GenAudit15"] = (mst["GenAudit15"] ?? "").Trim().Left(1); } catch { }
drType["GenAudit15"] = "Char"; drDisp["GenAudit15"] = "CheckBox";
try { dr["ScreenObj15"] = mst["ScreenObj15"]; } catch { }
drType["ScreenObj15"] = "VarChar"; drDisp["ScreenObj15"] = "HyperPopUp";
try { dr["ScreenFilter"] = mst["ScreenFilter"]; } catch { }
drType["ScreenFilter"] = string.Empty; drDisp["ScreenFilter"] = "ImagePopUp";
try { dr["MoreInfo"] = mst["MoreInfo"]; } catch { }
drType["MoreInfo"] = string.Empty; drDisp["MoreInfo"] = "HyperPopUp";

                    if (dtl != null)
                    {
                        ds.Tables["AdmScreenDef"].Rows.Add(MakeTypRow(ds.Tables["AdmScreenDef"].NewRow()));
                        ds.Tables["AdmScreenDef"].Rows.Add(MakeDisRow(ds.Tables["AdmScreenDef"].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables["AdmScreenAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenAdd"].NewRow(), drv, mst["ScreenId15"], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["ScreenHlpId16"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables["AdmScreenUpd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenUpd"].NewRow(), drv, mst["ScreenId15"], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["ScreenHlpId16"]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables["AdmScreenAdd"].Rows.Add(MakeColRow(ds.Tables["AdmScreenAdd"].NewRow(), drv, mst["ScreenId15"], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["ScreenHlpId16"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables["AdmScreenDel"].Rows.Add(MakeColRow(ds.Tables["AdmScreenDel"].NewRow(), drv, mst["ScreenId15"], false));
                            }
                        }
                    }
                    ds.Tables["AdmScreen"].Rows.Add(dr); ds.Tables["AdmScreen"].Rows.Add(drType); ds.Tables["AdmScreen"].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ScreenId15",""},
{"ProgramName15",""},
{"ScreenTypeId15",""},
{"ViewOnly15","N"},
{"SearchAscending15","Y"},
{"MasterTableId15",""},
{"SearchTableId15",""},
{"SearchId15",""},
{"SearchIdR15",""},
{"SearchDtlId15",""},
{"SearchDtlIdR15",""},
{"SearchUrlId15",""},
{"SearchImgId15",""},
{"DetailTableId15",""},
{"GridRows15","12"},
{"HasDeleteAll15","Y"},
{"ShowGridHead15","Y"},
{"GenerateSc15","Y"},
{"GenerateSr15","Y"},
{"ValidateReq15","Y"},
{"DeferError15",""},
{"AuthRequired15","Y"},
{"GenAudit15","N"},
{"ScreenObj15",""},
{"ScreenFilter","images/custom/adm/AnalToDb.gif"},
{"MoreInfo","www.robocoder.com"},

                    };
                    /* AsmxRule: Init Master Table */
            

                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            {"ScreenHlpId16",""},
{"CultureId16","1"},
{"ScreenTitle16",""},
{"DefaultHlpMsg16",""},
{"FootNote16",""},
{"AddMsg16",""},
{"UpdMsg16",""},
{"DelMsg16",""},
{"IncrementMsg16","View {10} more"},
{"NoMasterMsg16","No master record selected"},
{"NoDetailMsg16","No detail record selected"},
{"AddMasterMsg16","Click here to enter a new master record"},
{"AddDetailMsg16","Click here to enter a new detail record"},
{"MasterLstTitle16","Master list"},
{"MasterLstSubtitle16","Create or select a master record"},
{"MasterRecTitle16","Master record"},
{"MasterRecSubtitle16","Manage your master information"},
{"MasterFoundMsg16","Master records found"},
{"DetailLstTitle16","Detail list"},
{"DetailLstSubtitle16","Add or edit a detail record"},
{"DetailRecTitle16","Detail record"},
{"DetailRecSubtitle16","Enter or update detail information"},
{"DetailFoundMsg16","Detail records found"},

                    };
                    /* AsmxRule: Init Detail Table */
            

                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            

            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreen9List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context["method"] = "GetLisAdmScreen9";
                    context["mKey"] = "ScreenId15";
                    context["mVal"] = "ScreenId15Text";
                    context["mTip"] = "ScreenId15Text";
                    context["mImg"] = "ScreenId15Text";
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreen9ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmScreen9", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmScreen9DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;            
                        ValidatedMstId("GetLisAdmScreen9", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
DataTable dtColAuth = _GetAuthCol(GetScreenId());
Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById(9, "GetAdmScreen9DtlById", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);
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
                return (new RO.Access3.AdminAccess()).GetMstById("GetAdmScreen9ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmScreen9DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

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
                    var pid = mst["ScreenId15"];
                    var ds = PrepAdmScreenData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["ScreenId15"]));
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

                    var pid = mst["ScreenId15"];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey("currentScreenCriteria") ? options["currentScreenCriteria"] : null;
                        ValidatedMstId("GetLisAdmScreen9", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
                        DataTable dtDtl = _GetDtlById(pid, 0); 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["ScreenHlpId16"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ScreenId15", "ScreenHlpId16", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = "failed",
                            errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = PrepAdmScreenData(mst, dtl, string.IsNullOrEmpty(mst["ScreenId15"]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst["ScreenId15"]))
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
            
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenTypeId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlScreenTypeId3S45", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenTypeId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetViewOnly15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);bool bAll = !query.StartsWith("**");bool bAddNew = !query.StartsWith("**");string keyId = query.Replace("**", "");DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlViewOnly3S3103", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);return DataTableToApiResponse(dt, "", 0);};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ViewOnly15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetMasterTableId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlMasterTableId3S59";context["addnew"] = "Y";context["mKey"] = "MasterTableId15";context["mVal"] = "MasterTableId15Text";context["mTip"] = "MasterTableId15Text";context["mImg"] = "MasterTableId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "MasterTableId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchTableId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchTableId3S4145";context["addnew"] = "Y";context["mKey"] = "SearchTableId15";context["mVal"] = "SearchTableId15Text";context["mTip"] = "SearchTableId15Text";context["mImg"] = "SearchTableId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchTableId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchId3S46";context["addnew"] = "Y";context["mKey"] = "SearchId15";context["mVal"] = "SearchId15Text";context["mTip"] = "SearchId15Text";context["mImg"] = "SearchId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchIdR15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchIdR3S4164";context["addnew"] = "Y";context["mKey"] = "SearchIdR15";context["mVal"] = "SearchIdR15Text";context["mTip"] = "SearchIdR15Text";context["mImg"] = "SearchIdR15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchIdR15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchDtlId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchDtlId3S4148";context["addnew"] = "Y";context["mKey"] = "SearchDtlId15";context["mVal"] = "SearchDtlId15Text";context["mTip"] = "SearchDtlId15Text";context["mImg"] = "SearchDtlId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchDtlId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchDtlIdR15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchDtlIdR3S4165";context["addnew"] = "Y";context["mKey"] = "SearchDtlIdR15";context["mVal"] = "SearchDtlIdR15Text";context["mTip"] = "SearchDtlIdR15Text";context["mImg"] = "SearchDtlIdR15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchDtlIdR15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchUrlId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchUrlId3S4147";context["addnew"] = "Y";context["mKey"] = "SearchUrlId15";context["mVal"] = "SearchUrlId15Text";context["mTip"] = "SearchUrlId15Text";context["mImg"] = "SearchUrlId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchUrlId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchImgId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlSearchImgId3S4146";context["addnew"] = "Y";context["mKey"] = "SearchImgId15";context["mVal"] = "SearchImgId15Text";context["mTip"] = "SearchImgId15Text";context["mImg"] = "SearchImgId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;context["refCol"] = "TableId";context["refColDataType"] = "Int";context["refColVal"] = filterBy;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "SearchImgId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDetailTableId15List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlDetailTableId3S60";context["addnew"] = "Y";context["mKey"] = "DetailTableId15";context["mVal"] = "DetailTableId15Text";context["mTip"] = "DetailTableId15Text";context["mImg"] = "DetailTableId15Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DetailTableId15", emptyAutoCompleteResponse));return ret;}
                        [WebMethod(EnableSession = false)]public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId16List(string query, int topN, string filterBy){Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>{SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();context["method"] = "GetDdlCultureId3S53";context["addnew"] = "Y";context["mKey"] = "CultureId16";context["mVal"] = "CultureId16Text";context["mTip"] = "CultureId16Text";context["mImg"] = "CultureId16Text";context["ssd"] = "";context["scr"] = screenId.ToString();context["csy"] = systemId.ToString();context["filter"] = "0";context["isSys"] = "N";context["conn"] = string.Empty;ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();mr.status = "success";mr.errorMsg = "";mr.data = ddlSuggests(query, context, topN);return mr;};var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId16", emptyAutoCompleteResponse));return ret;}
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
                    var dtLabel = _GetLabels("AdmScreen");
                    var SearchList = GetAdmScreen9List("", 0, "");
                                    var ScreenTypeId15LIst = GetScreenTypeId15List("", 0, "");
                        var ViewOnly15LIst = GetViewOnly15List("", 0, "");
                        var MasterTableId15LIst = GetMasterTableId15List("", 0, "");
                        var SearchTableId15LIst = GetSearchTableId15List("", 0, "");
                        var SearchId15LIst = GetSearchId15List("", 0, "");
                        var SearchIdR15LIst = GetSearchIdR15List("", 0, "");
                        var SearchDtlId15LIst = GetSearchDtlId15List("", 0, "");
                        var SearchDtlIdR15LIst = GetSearchDtlIdR15List("", 0, "");
                        var SearchUrlId15LIst = GetSearchUrlId15List("", 0, "");
                        var SearchImgId15LIst = GetSearchImgId15List("", 0, "");
                        var DetailTableId15LIst = GetDetailTableId15List("", 0, "");
                        var CultureId16LIst = GetCultureId16List("", 0, "");

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
            