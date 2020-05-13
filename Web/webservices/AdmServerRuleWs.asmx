<%@ WebService Language="C#" Class="RO.Web.AdmServerRuleWs" %>
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
            
    public class AdmServerRule14 : DataSet
    {
        public AdmServerRule14()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmServerRule")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmServerRuleDel")));
            this.DataSetName = "AdmServerRule14";
            this.Namespace = "http://Rintagi.com/DataSet/AdmServerRule14";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ServerRuleId24", typeof(string));
            columns.Add("RuleName24", typeof(string));
            columns.Add("RuleDescription24", typeof(string));
            columns.Add("RuleTypeId24", typeof(string));
            columns.Add("ScreenId24", typeof(string));
            columns.Add("RuleOrder24", typeof(string));
            columns.Add("ProcedureName24", typeof(string));
            columns.Add("ParameterNames24", typeof(string));
            columns.Add("ParameterTypes24", typeof(string));
            columns.Add("CallingParams24", typeof(string));
            columns.Add("RemoveSP", typeof(string));
            columns.Add("MasterTable24", typeof(string));
            columns.Add("OnAdd24", typeof(string));
            columns.Add("OnUpd24", typeof(string));
            columns.Add("OnDel24", typeof(string));
            columns.Add("BeforeCRUD24", typeof(string));
            columns.Add("SrcNS24", typeof(string));
            columns.Add("RunMode24", typeof(string));
            columns.Add("RuleCode24", typeof(string));
            columns.Add("ModifiedBy24", typeof(string));
            columns.Add("LastGenDt24", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ServerRuleId24", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmServerRuleWs : RO.Web.AsmxBase
    {
        const int screenId = 14;
        const byte systemId = 3;
        const string programName = "AdmServerRule14";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmServerRule14"; }
        protected override string GetMstTableName(bool underlying = true) { return "ServerRule"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ServerRuleId" : "ServerRuleId24"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {
            {"RuleTypeId24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRuleTypeId3S151"},{"mKey","RuleTypeId24"},{"mVal","RuleTypeId24Text"}, }},
            {"ScreenId24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlScreenId3S139"},{"mKey","ScreenId24"},{"mVal","ScreenId24Text"}, }},
            {"BeforeCRUD24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBeforeCRUD3S163"},{"mKey","BeforeCRUD24"},{"mVal","BeforeCRUD24Text"}, }},
            {"CrudTypeDesc1289", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBeforeCRUD3S163"},{"mKey","BeforeCRUD24"},{"mVal","CrudTypeDesc1289"}, {"baseTbl", "CtCrudType"},{"baseKeyCol", "CrudTypeCd"},{"baseColName", "CrudTypeDesc"},}},
            {"RunMode24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlRunMode3S4354"},{"mKey","RunMode24"},{"mVal","RunMode24Text"}, }},
            {"ModifiedBy24", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlModifiedBy3S1397"},{"mKey","ModifiedBy24"},{"mVal","ModifiedBy24Text"}, }},
        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["ServerRuleId24"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["ServerRuleId24"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["ServerRuleId24"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmServerRule14 PrepAdmServerRuleData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmServerRule14 ds = new AdmServerRule14();
            DataRow dr = ds.Tables["AdmServerRule"].NewRow();
            DataRow drType = ds.Tables["AdmServerRule"].NewRow();
            DataRow drDisp = ds.Tables["AdmServerRule"].NewRow();

            if (bAdd) { dr["ServerRuleId24"] = string.Empty; } else { dr["ServerRuleId24"] = mst["ServerRuleId24"]; }
            drType["ServerRuleId24"] = "Numeric"; drDisp["ServerRuleId24"] = "TextBox";
            try { dr["RuleName24"] = (mst["RuleName24"] ?? "").Trim().Left(100); } catch { }
            drType["RuleName24"] = "VarWChar"; drDisp["RuleName24"] = "TextBox";
            try { dr["RuleDescription24"] = mst["RuleDescription24"]; } catch { }
            drType["RuleDescription24"] = "VarWChar"; drDisp["RuleDescription24"] = "MultiLine";
            try { dr["RuleTypeId24"] = mst["RuleTypeId24"]; } catch { }
            drType["RuleTypeId24"] = "Numeric"; drDisp["RuleTypeId24"] = "DropDownList";
            try { dr["ScreenId24"] = mst["ScreenId24"]; } catch { }
            drType["ScreenId24"] = "Numeric"; drDisp["ScreenId24"] = "AutoComplete";
            try { dr["RuleOrder24"] = (mst["RuleOrder24"] ?? "").Trim().Left(9999999); } catch { }
            drType["RuleOrder24"] = "Numeric"; drDisp["RuleOrder24"] = "TextBox";
            try { dr["ProcedureName24"] = (mst["ProcedureName24"] ?? "").Trim().Left(50); } catch { }
            drType["ProcedureName24"] = "VarChar"; drDisp["ProcedureName24"] = "TextBox";
            try { dr["ParameterNames24"] = (mst["ParameterNames24"] ?? "").Trim().Left(0); } catch { }
            drType["ParameterNames24"] = "VarChar"; drDisp["ParameterNames24"] = "TextBox";
            try { dr["ParameterTypes24"] = (mst["ParameterTypes24"] ?? "").Trim().Left(0); } catch { }
            drType["ParameterTypes24"] = "VarChar"; drDisp["ParameterTypes24"] = "TextBox";
            try { dr["CallingParams24"] = (mst["CallingParams24"] ?? "").Trim().Left(0); } catch { }
            drType["CallingParams24"] = "VarChar"; drDisp["CallingParams24"] = "TextBox";
            try { dr["RemoveSP"] = (mst["RemoveSP"] ?? "").Trim().Left(9999999); } catch { }
            drType["RemoveSP"] = string.Empty; drDisp["RemoveSP"] = "CheckBox";
            try { dr["MasterTable24"] = (mst["MasterTable24"] ?? "").Trim().Left(1); } catch { }
            drType["MasterTable24"] = "Char"; drDisp["MasterTable24"] = "CheckBox";
            try { dr["OnAdd24"] = (mst["OnAdd24"] ?? "").Trim().Left(1); } catch { }
            drType["OnAdd24"] = "Char"; drDisp["OnAdd24"] = "CheckBox";
            try { dr["OnUpd24"] = (mst["OnUpd24"] ?? "").Trim().Left(1); } catch { }
            drType["OnUpd24"] = "Char"; drDisp["OnUpd24"] = "CheckBox";
            try { dr["OnDel24"] = (mst["OnDel24"] ?? "").Trim().Left(1); } catch { }
            drType["OnDel24"] = "Char"; drDisp["OnDel24"] = "CheckBox";
            try { dr["BeforeCRUD24"] = mst["BeforeCRUD24"]; } catch { }
            drType["BeforeCRUD24"] = "Char"; drDisp["BeforeCRUD24"] = "DropDownList";
            try { dr["SrcNS24"] = (mst["SrcNS24"] ?? "").Trim().Left(30); } catch { }
            drType["SrcNS24"] = "VarChar"; drDisp["SrcNS24"] = "TextBox";
            try { dr["RunMode24"] = mst["RunMode24"]; } catch { }
            drType["RunMode24"] = "Char"; drDisp["RunMode24"] = "DropDownList";
            try { dr["RuleCode24"] = mst["RuleCode24"]; } catch { }
            drType["RuleCode24"] = "VarWChar"; drDisp["RuleCode24"] = "MultiLine";
            try { dr["ModifiedBy24"] = mst["ModifiedBy24"]; } catch { }
            drType["ModifiedBy24"] = "Numeric"; drDisp["ModifiedBy24"] = "DropDownList";
            try { dr["LastGenDt24"] = (mst["LastGenDt24"] ?? "").Trim().Left(9999999); } catch { }
            drType["LastGenDt24"] = "DBTimeStamp"; drDisp["LastGenDt24"] = "TextBox";

            if (dtl != null)
            {
                ds.Tables["AdmServerRuleDef"].Rows.Add(MakeTypRow(ds.Tables["AdmServerRuleDef"].NewRow()));
                ds.Tables["AdmServerRuleDef"].Rows.Add(MakeDisRow(ds.Tables["AdmServerRuleDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmServerRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleAdd"].NewRow(), drv, mst["ServerRuleId24"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmServerRuleUpd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleUpd"].NewRow(), drv, mst["ServerRuleId24"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmServerRuleAdd"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleAdd"].NewRow(), drv, mst["ServerRuleId24"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmServerRuleDel"].Rows.Add(MakeColRow(ds.Tables["AdmServerRuleDel"].NewRow(), drv, mst["ServerRuleId24"], false));
                    }
                }
            }
            ds.Tables["AdmServerRule"].Rows.Add(dr); ds.Tables["AdmServerRule"].Rows.Add(drType); ds.Tables["AdmServerRule"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"ServerRuleId24",""},
                {"RuleName24",""},
                {"RuleDescription24",""},
                {"RuleTypeId24",""},
                {"ScreenId24",""},
                {"RuleOrder24",""},
                {"ProcedureName24",""},
                {"ParameterNames24",""},
                {"ParameterTypes24",""},
                {"CallingParams24",""},
                {"RemoveSP","N"},
                {"MasterTable24",""},
                {"OnAdd24",""},
                {"OnUpd24",""},
                {"OnDel24",""},
                {"BeforeCRUD24",""},
                {"SrcNS24",""},
                {"RunMode24",""},
                {"RuleCode24",""},
                {"SyncByDb","~/images/custom/adm/SyncByDb.gif"},
                {"SyncToDb","~/images/custom/adm/SyncToDb.gif"},
                {"ModifiedBy24",base.LUser.UsrId.ToString()},
                {"LastGenDt24",""},

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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmServerRule14";
                context["mKey"] = "ServerRuleId24";
                context["mVal"] = "ServerRuleId24Text";
                context["mTip"] = "ServerRuleId24Text";
                context["mImg"] = "ServerRuleId24Text";
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
            return GetAdmServerRule14List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14ById(string keyId, SerializableDictionary<string, string> options)
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
                ValidatedMstId("GetLisAdmServerRule14", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            return GetAdmServerRule14ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmServerRule14DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
            return GetAdmServerRule14DtlById(keyId, options, filterId);
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmServerRule14ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmServerRule14DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
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
                var pid = mst["ServerRuleId24"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmServerRuleData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["ServerRuleId24"]));

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

                var pid = mst["ServerRuleId24"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmServerRule14", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ServerRuleId24", "", skipValidation);
                if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                {
                    return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                    {
                        status = "failed",
                        errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                        validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                    };
                }
                var ds = PrepAdmServerRuleData(mst, dtl, string.IsNullOrEmpty(mst["ServerRuleId24"]));
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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRuleTypeId24List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlRuleTypeId3S151", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RuleTypeId24", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetScreenId24List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlScreenId3S139";
            context["addnew"] = "Y";
            context["mKey"] = "ScreenId24";
            context["mVal"] = "ScreenId24Text";
            context["mTip"] = "ScreenId24Text";
            context["mImg"] = "ScreenId24Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ScreenId24", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBeforeCRUD24List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlBeforeCRUD3S163", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BeforeCRUD24", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetRunMode24List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlRunMode3S4354", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "RunMode24", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetModifiedBy24List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlModifiedBy3S1397", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ModifiedBy24", emptyAutoCompleteResponse));
            return ret;
        }

        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            