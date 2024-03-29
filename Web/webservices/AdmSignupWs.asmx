<%@ WebService Language="C#" Class="RO.Web.AdmSignupWs" %>
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
    using System.Numerics;

            
    public class AdmSignup1018 : DataSet
    {
        public AdmSignup1018()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmSignup")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmSignupDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmSignupAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmSignupUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmSignupDel")));
            this.DataSetName = "AdmSignup1018";
            this.Namespace = "http://Rintagi.com/DataSet/AdmSignup1018";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("DummyWhiteSpace7", typeof(string));
            columns.Add("SignUpTitle", typeof(string));
            columns.Add("SignUpTopMsg", typeof(string));
            columns.Add("DummyWhiteSpace8", typeof(string));
            columns.Add("DummyWhiteSpace1", typeof(string));
            columns.Add("UsrId270", typeof(string));
            columns.Add("UsrName270", typeof(string));
            columns.Add("LoginName270", typeof(string));
            columns.Add("UsrEmail270", typeof(string));
            columns.Add("UsrPassword270", typeof(string));
            columns.Add("DummyWhiteSpace2", typeof(string));
            columns.Add("DummyWhiteSpace3", typeof(string));
            columns.Add("TokenMsg", typeof(string));
            columns.Add("ConfirmationToken", typeof(string));
            columns.Add("Token", typeof(string));
            columns.Add("DummyWhiteSpace9", typeof(string));
            columns.Add("DummyWhiteSpace4", typeof(string));
            columns.Add("DummyWhiteSpace5", typeof(string));
            columns.Add("DummyWhiteSpace6", typeof(string));
            columns.Add("DummyWhiteSpace10", typeof(string));
            columns.Add("DummyWhiteSpace11", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("UsrId270", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmSignupWs : RO.Web.AsmxBase
    {
        const int screenId = 1018;
        const byte systemId = 3;
        const string programName = "AdmSignup1018";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmSignup1018"; }
        protected override string GetMstTableName(bool underlying = true) { return "VwUsr"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "UsrId" : "UsrId270"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {

        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["UsrId270"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["UsrId270"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["UsrId270"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmSignup1018 PrepAdmSignupData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmSignup1018 ds = new AdmSignup1018();
            DataRow dr = ds.Tables["AdmSignup"].NewRow();
            DataRow drType = ds.Tables["AdmSignup"].NewRow();
            DataRow drDisp = ds.Tables["AdmSignup"].NewRow();

            try { dr["DummyWhiteSpace7"] = mst["DummyWhiteSpace7"]; } catch { }
            drType["DummyWhiteSpace7"] = string.Empty; drDisp["DummyWhiteSpace7"] = "Label";
            try { dr["SignUpTitle"] = mst["SignUpTitle"]; } catch { }
            drType["SignUpTitle"] = string.Empty; drDisp["SignUpTitle"] = "Label";
            try { dr["SignUpTopMsg"] = mst["SignUpTopMsg"]; } catch { }
            drType["SignUpTopMsg"] = string.Empty; drDisp["SignUpTopMsg"] = "Label";
            try { dr["DummyWhiteSpace8"] = mst["DummyWhiteSpace8"]; } catch { }
            drType["DummyWhiteSpace8"] = string.Empty; drDisp["DummyWhiteSpace8"] = "Label";
            try { dr["DummyWhiteSpace1"] = mst["DummyWhiteSpace1"]; } catch { }
            drType["DummyWhiteSpace1"] = string.Empty; drDisp["DummyWhiteSpace1"] = "Label";
            if (bAdd) { dr["UsrId270"] = string.Empty; } else { dr["UsrId270"] = mst["UsrId270"]; }
            drType["UsrId270"] = "Numeric"; drDisp["UsrId270"] = "TextBox";
            try { dr["UsrName270"] = (mst["UsrName270"] ?? "").Trim().Left(50); } catch { }
            drType["UsrName270"] = "VarWChar"; drDisp["UsrName270"] = "TextBox";
            try { dr["LoginName270"] = (mst["LoginName270"] ?? "").Trim().Left(32); } catch { }
            drType["LoginName270"] = "VarWChar"; drDisp["LoginName270"] = "TextBox";
            try { dr["UsrEmail270"] = (mst["UsrEmail270"] ?? "").Trim().Left(50); } catch { }
            drType["UsrEmail270"] = "VarWChar"; drDisp["UsrEmail270"] = "TextBox";
            try { dr["UsrPassword270"] = mst["UsrPassword270"]; } catch { }
            drType["UsrPassword270"] = "VarBinary"; drDisp["UsrPassword270"] = "Password";
            try { dr["DummyWhiteSpace2"] = mst["DummyWhiteSpace2"]; } catch { }
            drType["DummyWhiteSpace2"] = string.Empty; drDisp["DummyWhiteSpace2"] = "Label";
            try { dr["DummyWhiteSpace3"] = mst["DummyWhiteSpace3"]; } catch { }
            drType["DummyWhiteSpace3"] = string.Empty; drDisp["DummyWhiteSpace3"] = "Label";
            try { dr["TokenMsg"] = mst["TokenMsg"]; } catch { }
            drType["TokenMsg"] = string.Empty; drDisp["TokenMsg"] = "Label";
            try { dr["ConfirmationToken"] = (mst["ConfirmationToken"] ?? "").Trim().Left(9999999); } catch { }
            drType["ConfirmationToken"] = string.Empty; drDisp["ConfirmationToken"] = "TextBox";
            try { dr["Token"] = (mst["Token"] ?? "").Trim().Left(9999999); } catch { }
            drType["Token"] = string.Empty; drDisp["Token"] = "TextBox";
            try { dr["DummyWhiteSpace9"] = mst["DummyWhiteSpace9"]; } catch { }
            drType["DummyWhiteSpace9"] = string.Empty; drDisp["DummyWhiteSpace9"] = "Label";
            try { dr["DummyWhiteSpace4"] = mst["DummyWhiteSpace4"]; } catch { }
            drType["DummyWhiteSpace4"] = string.Empty; drDisp["DummyWhiteSpace4"] = "Label";
            try { dr["DummyWhiteSpace5"] = mst["DummyWhiteSpace5"]; } catch { }
            drType["DummyWhiteSpace5"] = string.Empty; drDisp["DummyWhiteSpace5"] = "Label";
            try { dr["DummyWhiteSpace6"] = mst["DummyWhiteSpace6"]; } catch { }
            drType["DummyWhiteSpace6"] = string.Empty; drDisp["DummyWhiteSpace6"] = "Label";
            try { dr["DummyWhiteSpace10"] = mst["DummyWhiteSpace10"]; } catch { }
            drType["DummyWhiteSpace10"] = string.Empty; drDisp["DummyWhiteSpace10"] = "Label";
            try { dr["DummyWhiteSpace11"] = mst["DummyWhiteSpace11"]; } catch { }
            drType["DummyWhiteSpace11"] = string.Empty; drDisp["DummyWhiteSpace11"] = "Label";

            if (dtl != null)
            {
                ds.Tables["AdmSignupDef"].Rows.Add(MakeTypRow(ds.Tables["AdmSignupDef"].NewRow()));
                ds.Tables["AdmSignupDef"].Rows.Add(MakeDisRow(ds.Tables["AdmSignupDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmSignupAdd"].Rows.Add(MakeColRow(ds.Tables["AdmSignupAdd"].NewRow(), drv, mst["UsrId270"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmSignupUpd"].Rows.Add(MakeColRow(ds.Tables["AdmSignupUpd"].NewRow(), drv, mst["UsrId270"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmSignupAdd"].Rows.Add(MakeColRow(ds.Tables["AdmSignupAdd"].NewRow(), drv, mst["UsrId270"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmSignupDel"].Rows.Add(MakeColRow(ds.Tables["AdmSignupDel"].NewRow(), drv, mst["UsrId270"], false));
                    }
                }
            }
            ds.Tables["AdmSignup"].Rows.Add(dr); ds.Tables["AdmSignup"].Rows.Add(drType); ds.Tables["AdmSignup"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"DummyWhiteSpace7",""},
                {"SignUpTitle",""},
                {"SignUpTopMsg",""},
                {"DummyWhiteSpace8",""},
                {"DummyWhiteSpace1",""},
                {"UsrId270",""},
                {"UsrName270",""},
                {"LoginName270",""},
                {"UsrEmail270",""},
                {"UsrPassword270",""},
                {"DummyWhiteSpace2",""},
                {"DummyWhiteSpace3",""},
                {"TokenMsg",""},
                {"ConfirmationToken",""},
                {"Token",""},
                {"DummyWhiteSpace9",""},
                {"ResnedToken",""},
                {"DummyWhiteSpace4",""},
                {"DummyWhiteSpace5",""},
                {"Submit",""},
                {"SignUpBtn",""},
                {"DummyWhiteSpace6",""},
                {"DummyWhiteSpace10",""},
                {"SignUpMsg",""},
                {"DummyWhiteSpace11",""},

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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmSignup1018List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                var effectiveFilterId = GetEffectiveScreenFilterId(filterId, true);                
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmSignup1018";
                context["mKey"] = "UsrId270";
                context["mVal"] = "UsrId270Text";
                context["mTip"] = "UsrId270Text";
                context["mImg"] = "UsrId270Text";
                context["ssd"] = "";
                context["scr"] = screenId.ToString();
                context["csy"] = systemId.ToString();
                context["filter"] = effectiveFilterId.ToString();
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
            return GetAdmSignup1018List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmSignup1018ById(string keyId, SerializableDictionary<string, string> options)
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
                ValidatedMstId("GetLisAdmSignup1018", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                DataTable dt = _GetMstById(keyId);
                DataTable dtColAuth = _GetAuthCol(GetScreenId());
                DataTable dtColLabel = _GetScreenLabel(GetScreenId());
                Dictionary<string, DataRow> colAuth = _GetAuthColDict(dtColAuth, GetScreenId());
                var utcColumnList = dtColLabel.AsEnumerable().Where(dr => dr["DisplayMode"].ToString().Contains("UTC")).Select(dr => dr["ColumnName"].ToString() + dr["TableId"].ToString()).ToArray();
                HashSet<string> utcColumns = new HashSet<string>(utcColumnList);
                ApiResponse <List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                SerializableDictionary<string, AutoCompleteResponse> supportingData = new SerializableDictionary<string,AutoCompleteResponse>();
                /* Get Master By Id After start here */


                /* Get Master By Id After end here */
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
            return GetAdmSignup1018ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmSignup1018DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
            return GetAdmSignup1018DtlById(keyId, options, filterId);
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmSignup1018ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmSignup1018DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
        }
        protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
        {
            return ddlContext;
        }

        [WebMethod(EnableSession = false)]
        public override ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMst(SerializableDictionary<string, string> mst, SerializableDictionary<string, string> options)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            bool noTrans = Config.NoTrans;
            int commandTimeOut = Config.CommandTimeOut;

            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                var pid = mst["UsrId270"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmSignupData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["UsrId270"]));

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
        public override ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> SaveData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, SerializableDictionary<string, string> options)
        {
            bool isAdd = false;
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            string screenButton = options.ContainsKey("ScreenButton") ? options["ScreenButton"] : "";
            string actionButton = options.ContainsKey("OnClickColumeName") ? options["OnClickColumeName"] : "";
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
                /* AsmxRule: Save Data Before Validation */


                /* AsmxRule End: Save Data Before Validation */

                var pid = mst["UsrId270"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmSignup1018", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "UsrId270", "", skipValidation);
                if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                {
                    return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                    {
                        status = "failed",
                        errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                        validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                    };
                }
                /* AsmxRule: Save Data Before */


                /* AsmxRule End: Save Data Before */

                var ds = PrepAdmSignupData(mst, dtl, string.IsNullOrEmpty(mst["UsrId270"]));
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
                Dictionary<string, DataRow> colAuth = _GetAuthColDict(dtColAuth, GetScreenId());
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
            


        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            