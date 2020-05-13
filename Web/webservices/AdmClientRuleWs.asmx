<%@ WebService Language="C#" Class="RO.Web.AdmClientRuleWs" %>
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
            
    public class AdmClientRule79 : DataSet
    {
        public AdmClientRule79()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmClientRule")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmClientRuleDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmClientRuleAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmClientRuleUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmClientRuleDel")));
            this.DataSetName = "AdmClientRule79";
            this.Namespace = "http://Rintagi.com/DataSet/AdmClientRule79";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ClientRuleId127", typeof(string));
            columns.Add("RuleMethodId127", typeof(string));
            columns.Add("RuleName127", typeof(string));
            columns.Add("RuleDescription127", typeof(string));
            columns.Add("RuleTypeId127", typeof(string));
            columns.Add("ScreenId127", typeof(string));
            columns.Add("ReportId127", typeof(string));
            columns.Add("CultureId127", typeof(string));
            columns.Add("ScreenObjHlpId127", typeof(string));
            columns.Add("ScreenCriHlpId127", typeof(string));
            columns.Add("ReportCriHlpId127", typeof(string));
            columns.Add("RuleCntTypeId127", typeof(string));
            columns.Add("ClientRuleProg127", typeof(string));
            columns.Add("ClientScript127", typeof(string));
            columns.Add("UserScriptEvent127", typeof(string));
            columns.Add("UserScriptName127", typeof(string));
            columns.Add("ScriptParam127", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ClientRuleId127", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmClientRuleWs : RO.Web.AsmxBase
    {
        const int screenId = 79;
        const byte systemId = 3;
        const string programName = "AdmClientRule79";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmClientRule79"; }
        protected override string GetMstTableName(bool underlying = true) { return "ClientRule"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ClientRuleId" : "ClientRuleId127"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {
            {"RuleMethodId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleMethodId3S4073"},{"mKey","RuleMethodId127"},{"mVal","RuleMethodId127Text"}, }},
            {"RuleMethodDesc1295", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleMethodId3S4073"},{"mKey","RuleMethodId127"},{"mVal","RuleMethodDesc1295"}, {"baseTbl", "CtRuleMethod"},{"baseKeyCol", "RuleMethodId"},{"baseColName", "RuleMethodDesc"},}},
            {"RuleTypeId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleTypeId3S1293"},{"mKey","RuleTypeId127"},{"mVal","RuleTypeId127Text"}, }},
            {"ScreenId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S1258"},{"mKey","ScreenId127"},{"mVal","ScreenId127Text"}, }},
            {"ReportId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1265"},{"mKey","ReportId127"},{"mVal","ReportId127Text"}, }},
            {"CultureId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S1259"},{"mKey","CultureId127"},{"mVal","CultureId127Text"}, }},
            {"ScreenObjHlpId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenObjHlpId3S1260"},{"mKey","ScreenObjHlpId127"},{"mVal","ScreenObjHlpId127Text"}, {"refCol","ScreenId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ScreenId127"}}},
            {"ScreenCriHlpId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenCriHlpId3S1266"},{"mKey","ScreenCriHlpId127"},{"mVal","ScreenCriHlpId127Text"}, {"refCol","ScreenId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ScreenId127"}}},
            {"ReportCriHlpId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportCriHlpId3S1267"},{"mKey","ReportCriHlpId127"},{"mVal","ReportCriHlpId127Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId127"}}},
            {"RuleCntTypeId127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleCntTypeId3S4075"},{"mKey","RuleCntTypeId127"},{"mVal","RuleCntTypeId127Text"}, }},
            {"RuleCntTypeDesc1294", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleCntTypeId3S4075"},{"mKey","RuleCntTypeId127"},{"mVal","RuleCntTypeDesc1294"}, {"baseTbl", "CtRuleCntType"},{"baseKeyCol", "RuleCntTypeId"},{"baseColName", "RuleCntTypeDesc"},}},
            {"ClientScript127", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlClientScript3S1261"},{"mKey","ClientScript127"},{"mVal","ClientScript127Text"}, }},
            {"ClientScriptHelp126", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlClientScript3S1261"},{"mKey","ClientScript127"},{"mVal","ClientScriptHelp126"}, {"baseTbl", "CtClientScript"},{"baseKeyCol", "ClientScriptId"},{"baseColName", "ClientScriptHelp"},}},
        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["ClientRuleId127"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["ClientRuleId127"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["ClientRuleId127"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmClientRule79 PrepAdmClientRuleData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmClientRule79 ds = new AdmClientRule79();
            DataRow dr = ds.Tables["AdmClientRule"].NewRow();
            DataRow drType = ds.Tables["AdmClientRule"].NewRow();
            DataRow drDisp = ds.Tables["AdmClientRule"].NewRow();

            if (bAdd) { dr["ClientRuleId127"] = string.Empty; } else { dr["ClientRuleId127"] = mst["ClientRuleId127"]; }
            drType["ClientRuleId127"] = "Numeric"; drDisp["ClientRuleId127"] = "TextBox";
            try { dr["RuleMethodId127"] = mst["RuleMethodId127"]; } catch { }
            drType["RuleMethodId127"] = "Numeric"; drDisp["RuleMethodId127"] = "DropDownList";
            try { dr["RuleName127"] = (mst["RuleName127"] ?? "").Trim().Left(100); } catch { }
            drType["RuleName127"] = "VarWChar"; drDisp["RuleName127"] = "TextBox";
            try { dr["RuleDescription127"] = mst["RuleDescription127"]; } catch { }
            drType["RuleDescription127"] = "VarWChar"; drDisp["RuleDescription127"] = "MultiLine";
            try { dr["RuleTypeId127"] = mst["RuleTypeId127"]; } catch { }
            drType["RuleTypeId127"] = "Numeric"; drDisp["RuleTypeId127"] = "DropDownList";
            try { dr["ScreenId127"] = mst["ScreenId127"]; } catch { }
            drType["ScreenId127"] = "Numeric"; drDisp["ScreenId127"] = "AutoComplete";
            try { dr["ReportId127"] = mst["ReportId127"]; } catch { }
            drType["ReportId127"] = "Numeric"; drDisp["ReportId127"] = "AutoComplete";
            try { dr["CultureId127"] = mst["CultureId127"]; } catch { }
            drType["CultureId127"] = "Numeric"; drDisp["CultureId127"] = "AutoComplete";
            try { dr["ScreenObjHlpId127"] = mst["ScreenObjHlpId127"]; } catch { }
            drType["ScreenObjHlpId127"] = "Numeric"; drDisp["ScreenObjHlpId127"] = "AutoComplete";
            try { dr["ScreenCriHlpId127"] = mst["ScreenCriHlpId127"]; } catch { }
            drType["ScreenCriHlpId127"] = "Numeric"; drDisp["ScreenCriHlpId127"] = "AutoComplete";
            try { dr["ReportCriHlpId127"] = mst["ReportCriHlpId127"]; } catch { }
            drType["ReportCriHlpId127"] = "Numeric"; drDisp["ReportCriHlpId127"] = "AutoComplete";
            try { dr["RuleCntTypeId127"] = mst["RuleCntTypeId127"]; } catch { }
            drType["RuleCntTypeId127"] = "Numeric"; drDisp["RuleCntTypeId127"] = "DropDownList";
            try { dr["ClientRuleProg127"] = mst["ClientRuleProg127"]; } catch { }
            drType["ClientRuleProg127"] = "VarWChar"; drDisp["ClientRuleProg127"] = "MultiLine";
            try { dr["ClientScript127"] = mst["ClientScript127"]; } catch { }
            drType["ClientScript127"] = "Numeric"; drDisp["ClientScript127"] = "DropDownList";
            try { dr["UserScriptEvent127"] = (mst["UserScriptEvent127"] ?? "").Trim().Left(50); } catch { }
            drType["UserScriptEvent127"] = "VarChar"; drDisp["UserScriptEvent127"] = "TextBox";
            try { dr["UserScriptName127"] = mst["UserScriptName127"]; } catch { }
            drType["UserScriptName127"] = "VarWChar"; drDisp["UserScriptName127"] = "MultiLine";
            try { dr["ScriptParam127"] = (mst["ScriptParam127"] ?? "").Trim().Left(500); } catch { }
            drType["ScriptParam127"] = "VarWChar"; drDisp["ScriptParam127"] = "TextBox";

            if (dtl != null)
            {
                ds.Tables["AdmClientRuleDef"].Rows.Add(MakeTypRow(ds.Tables["AdmClientRuleDef"].NewRow()));
                ds.Tables["AdmClientRuleDef"].Rows.Add(MakeDisRow(ds.Tables["AdmClientRuleDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmClientRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmClientRuleAdd"].NewRow(), drv, mst["ClientRuleId127"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmClientRuleUpd"].Rows.Add(MakeColRow(ds.Tables["AdmClientRuleUpd"].NewRow(), drv, mst["ClientRuleId127"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmClientRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmClientRuleAdd"].NewRow(), drv, mst["ClientRuleId127"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmClientRuleDel"].Rows.Add(MakeColRow(ds.Tables["AdmClientRuleDel"].NewRow(), drv, mst["ClientRuleId127"], false));
                    }
                }
            }
            ds.Tables["AdmClientRule"].Rows.Add(dr); ds.Tables["AdmClientRule"].Rows.Add(drType); ds.Tables["AdmClientRule"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"ClientRuleId127",""},
                {"RuleMethodId127",""},
                {"RuleName127",""},
                {"RuleDescription127",""},
                {"RuleTypeId127",""},
                {"ScreenId127",""},
                {"ReportId127",""},
                {"CultureId127","1"},
                {"ScreenObjHlpId127",""},
                {"ScreenCriHlpId127",""},
                {"ReportCriHlpId127",""},
                {"RuleCntTypeId127",""},
                {"ClientRuleProg127",""},
                {"ClientScript127",""},
                {"UserScriptEvent127",""},
                {"UserScriptName127",""},
                {"ScriptParam127",""},

            };
            /* AsmxRule: Init Master Table */
                

            /* AsmxRule End: Init Master Table */

            return mst;
        }

        protected override SerializableDictionary<string, string> InitDtl()
        {
            var mst = new SerializableDictionary<string, string>()
            {


            };
            /* AsmxRule: Init Detail Table */
            

            /* AsmxRule End: Init Detail Table */
            return mst;
        }
            

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmClientRule79List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmClientRule79";
                context["mKey"] = "ClientRuleId127";
                context["mVal"] = "ClientRuleId127Text";
                context["mTip"] = "ClientRuleId127Text";
                context["mImg"] = "ClientRuleId127Text";
                context["ssd"] = "";
                context["scr"] = screenId.ToString();
                context["csy"] = systemId.ToString();
                context["filter"] = filterId;
                context["isSys"] = "N";
                context["conn"] = string.Empty;
                AutoCompleteResponse r = LisSuggests(searchStr, jss.Serialize(context), topN, _CurrentScreenCriteria);
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
        public override ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetSearchList(string searchStr, int topN, string filterId, SerializableDictionary<string, string> desiredScreenCriteria)
        {
            if (desiredScreenCriteria != null)
            {
                _SetEffectiveScrCriteria(desiredScreenCriteria);
            }
            return GetAdmClientRule79List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmClientRule79ById(string keyId, SerializableDictionary<string, string> options)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;


            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                string mstBlobIconOption = !options.ContainsKey("MstBlob") ? "I" : options["MstBlob"];
                string dtlBlobIconOption = !options.ContainsKey("DtlBlob") ? "I" : options["DtlBlob"];
                var mstBlob = GetBlobOption(mstBlobIconOption);
                var dtlBlob = GetBlobOption(dtlBlobIconOption);
                string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                bool includeDtl = !IsGridOnlyScreen() && (!options.ContainsKey("IncludeDtl") || options["IncludeDtl"] == "Y") && !String.IsNullOrEmpty(GetDtlTableName());
                ValidatedMstId("GetLisAdmClientRule79", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                DataTable dt = _GetMstById(keyId);
                DataTable dtColAuth = _GetAuthCol(GetScreenId());
                DataTable dtColLabel = _GetScreenLabel(GetScreenId());
                Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
                var utcColumnList = dtColLabel.AsEnumerable().Where(dr => dr["DisplayMode"].ToString().Contains("UTC")).Select(dr => dr["ColumnName"].ToString() + dr["TableId"].ToString()).ToArray();
                HashSet<string> utcColumns = new HashSet<string>(utcColumnList);
                ApiResponse <List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, AutoCompleteResponse> supportingData = new SerializableDictionary<string,AutoCompleteResponse>();
                mr.data = DataTableToListOfObject(dt, mstBlob, colAuth, utcColumns);
                mr.supportingData = includeDtl ? new SerializableDictionary<string, AutoCompleteResponse>() { { "dtl", new AutoCompleteResponse() { data = DataTableToListOfObject(_GetDtlById(keyId, 0), dtlBlob, colAuth, utcColumns) } } } : supportingData;
                mr.status = "success";
                mr.errorMsg = "";
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
            return ret;
        }
        [WebMethod(EnableSession = false)]
        public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetMstById(string keyId, SerializableDictionary<string, string> options)
        {
            return GetAdmClientRule79ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmClientRule79DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            string filterName = options.ContainsKey("FilterName") ? options["FilterName"] : "";

            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;            
                
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
        public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetDtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
        {
            return GetAdmClientRule79DtlById(keyId, options, filterId);
        }
        [WebMethod(EnableSession = false)]
        public override ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewMst()
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmClientRule79ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmClientRule79DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
        }
        protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
        {
            return ddlContext;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMst(SerializableDictionary<string, string> mst, SerializableDictionary<string, string> options)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            bool noTrans = Config.NoTrans;
            int commandTimeOut = Config.CommandTimeOut;

            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                var pid = mst["ClientRuleId127"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmClientRuleData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["ClientRuleId127"]));

                if (IsGridOnlyScreen())
                    (new RO.Facade3.AdminSystem()).UpdData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc, noTrans, commandTimeOut);                    
                else    
                    (new RO.Facade3.AdminSystem()).DelData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc, noTrans, commandTimeOut);

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
            bool isAdd = false;
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            bool noTrans = Config.NoTrans;
            int commandTimeOut = Config.CommandTimeOut;

            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                SerializableDictionary<string, string> skipValidation = new SerializableDictionary<string, string>() { { "SkipAllMst", "SilentColReadOnly" }, { "SkipAllDtl", "SilentColReadOnly" } };
                if (IsGridOnlyScreen() && dtl.Count == 0)
                {
                    dtl.Add(mst.Clone());
                }
                /* AsmxRule: Save Data Before */


                /* AsmxRule End: Save Data Before */

                var pid = mst["ClientRuleId127"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmClientRule79", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ClientRuleId127", "", skipValidation);
                if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                {
                    return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                    {
                        status = "failed",
                        errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                        validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                    };
                }
                var ds = PrepAdmClientRuleData(mst, dtl, string.IsNullOrEmpty(mst["ClientRuleId127"]));
                string msg = string.Empty;

                if (isAdd && !IsGridOnlyScreen())
                {
                    pid = (new RO.Facade3.AdminSystem()).AddData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc, noTrans, commandTimeOut);

                    if (!string.IsNullOrEmpty(pid))
                    {
                        msg = _GetScreenHlp(screenId).Rows[0]["AddMsg"].ToString();
                    }
                }
                else
                {
                    bool ok = (new RO.Facade3.AdminSystem()).UpdData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc, noTrans, commandTimeOut);

                    if (ok)
                    {
                        msg = _GetScreenHlp(screenId).Rows[0]["UpdMsg"].ToString();
                    }
                }

                /* read updated records */
                dtMst = _GetMstById(pid);

                /* AsmxRule: Save Data After */


                /* AsmxRule End: Save Data After */

                ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                SaveDataResponse result = new SaveDataResponse();
                DataTable dtColAuth = _GetAuthCol(GetScreenId());
                DataTable dtColLabel = _GetScreenLabel(GetScreenId());
                Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
                var utcColumnList = dtColLabel.AsEnumerable().Where(dr => dr["DisplayMode"].ToString().Contains("UTC")).Select(dr => dr["ColumnName"].ToString() + dr["TableId"].ToString()).ToArray();
                HashSet<string> utcColumns = new HashSet<string>(utcColumnList);

                result.mst = DataTableToListOfObject(dtMst, IncludeBLOB.None, colAuth, utcColumns)[0];

                    
                result.message = msg;
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "S", null));
            return ret;
        }
            
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRuleMethodId127List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlRuleMethodId3S4073", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RuleMethodId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRuleTypeId127List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlRuleTypeId3S1293", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RuleTypeId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlScreenId3S1258";
            context["addnew"] = "Y";
            context["mKey"] = "ScreenId127";
            context["mVal"] = "ScreenId127Text";
            context["mTip"] = "ScreenId127Text";
            context["mImg"] = "ScreenId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;

            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlReportId3S1265";
            context["addnew"] = "Y";
            context["mKey"] = "ReportId127";
            context["mVal"] = "ReportId127Text";
            context["mTip"] = "ReportId127Text";
            context["mImg"] = "ReportId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;

            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCultureId3S1259";
            context["addnew"] = "Y";
            context["mKey"] = "CultureId127";
            context["mVal"] = "CultureId127Text";
            context["mTip"] = "CultureId127Text";
            context["mImg"] = "CultureId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;

            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenObjHlpId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlScreenObjHlpId3S1260";
            context["addnew"] = "Y";
            context["mKey"] = "ScreenObjHlpId127";
            context["mVal"] = "ScreenObjHlpId127Text";
            context["mTip"] = "ScreenObjHlpId127Text";
            context["mImg"] = "ScreenObjHlpId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;            
            context["refCol"] = "ScreenId";
            context["refColDataType"] = "Int";
            context["refColVal"] = filterBy;
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenObjHlpId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenCriHlpId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlScreenCriHlpId3S1266";
            context["addnew"] = "Y";
            context["mKey"] = "ScreenCriHlpId127";
            context["mVal"] = "ScreenCriHlpId127Text";
            context["mTip"] = "ScreenCriHlpId127Text";
            context["mImg"] = "ScreenCriHlpId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;            
            context["refCol"] = "ScreenId";
            context["refColDataType"] = "Int";
            context["refColVal"] = filterBy;
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenCriHlpId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportCriHlpId127List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlReportCriHlpId3S1267";
            context["addnew"] = "Y";
            context["mKey"] = "ReportCriHlpId127";
            context["mVal"] = "ReportCriHlpId127Text";
            context["mTip"] = "ReportCriHlpId127Text";
            context["mImg"] = "ReportCriHlpId127Text";
            context["ssd"] = "";
            context["scr"] = screenId.ToString();
            context["csy"] = systemId.ToString();
            context["filter"] = "0";
            context["isSys"] = "N";
            context["conn"] = string.Empty;            
            context["refCol"] = "ReportId";
            context["refColDataType"] = "Int";
            context["refColVal"] = filterBy;
            ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = ddlSuggests(query, context, topN);
            return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportCriHlpId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRuleCntTypeId127List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlRuleCntTypeId3S4075", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RuleCntTypeId127", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetClientScript127List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlClientScript3S1261", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ClientScript127", emptyAutoCompleteResponse));
            return ret;
        }

        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            