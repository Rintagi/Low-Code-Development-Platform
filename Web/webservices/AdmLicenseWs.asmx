<%@ WebService Language="C#" Class="RO.Web.AdmLicenseWs" %>
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

            
    public class AdmLicense1022 : DataSet
    {
        public AdmLicense1022()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmLicense")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmLicenseDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmLicenseAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmLicenseUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmLicenseDel")));
            this.DataSetName = "AdmLicense1022";
            this.Namespace = "http://Rintagi.com/DataSet/AdmLicense1022";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("SystemId1317", typeof(string));
            columns.Add("SystemName1317", typeof(string));
            columns.Add("SystemAbbr1317", typeof(string));
            columns.Add("InstallID", typeof(string));
            columns.Add("AppID", typeof(string));
            columns.Add("AppNameSpace", typeof(string));
            columns.Add("RegisterInsall", typeof(string));
            columns.Add("ExpiryDate", typeof(string));
            columns.Add("ModuleIncluded", typeof(string));
            columns.Add("FeatureIncluded", typeof(string));
            columns.Add("FeatureExcluded", typeof(string));
            columns.Add("CompanyCount", typeof(string));
            columns.Add("ProjectCount", typeof(string));
            columns.Add("UserCount", typeof(string));
            columns.Add("LicenseServerUrl", typeof(string));
            columns.Add("LicenseString", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("SystemId1317", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmLicenseWs : RO.Web.AsmxBase
    {
        const int screenId = 1022;
        const byte systemId = 3;
        const string programName = "AdmLicense1022";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmLicense1022"; }
        protected override string GetMstTableName(bool underlying = true) { return "VwRintagi"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "SystemId" : "SystemId1317"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {

        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["SystemId1317"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["SystemId1317"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["SystemId1317"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmLicense1022 PrepAdmLicenseData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmLicense1022 ds = new AdmLicense1022();
            DataRow dr = ds.Tables["AdmLicense"].NewRow();
            DataRow drType = ds.Tables["AdmLicense"].NewRow();
            DataRow drDisp = ds.Tables["AdmLicense"].NewRow();

            if (bAdd) { dr["SystemId1317"] = string.Empty; } else { dr["SystemId1317"] = mst["SystemId1317"]; }
            drType["SystemId1317"] = "Numeric"; drDisp["SystemId1317"] = "TextBox";
            try { dr["SystemName1317"] = (mst["SystemName1317"] ?? "").Trim().Left(50); } catch { }
            drType["SystemName1317"] = "VarWChar"; drDisp["SystemName1317"] = "TextBox";
            try { dr["SystemAbbr1317"] = (mst["SystemAbbr1317"] ?? "").Trim().Left(4); } catch { }
            drType["SystemAbbr1317"] = "VarChar"; drDisp["SystemAbbr1317"] = "TextBox";
            try { dr["InstallID"] = (mst["InstallID"] ?? "").Trim().Left(9999999); } catch { }
            drType["InstallID"] = string.Empty; drDisp["InstallID"] = "TextBox";
            try { dr["AppID"] = (mst["AppID"] ?? "").Trim().Left(9999999); } catch { }
            drType["AppID"] = string.Empty; drDisp["AppID"] = "TextBox";
            try { dr["AppNameSpace"] = (mst["AppNameSpace"] ?? "").Trim().Left(9999999); } catch { }
            drType["AppNameSpace"] = string.Empty; drDisp["AppNameSpace"] = "TextBox";
            try { dr["RegisterInsall"] = mst["RegisterInsall"]; } catch { }
            drType["RegisterInsall"] = string.Empty; drDisp["RegisterInsall"] = "HyperPopUp";
            try { dr["ExpiryDate"] = mst["ExpiryDate"]; } catch { }
            drType["ExpiryDate"] = string.Empty; drDisp["ExpiryDate"] = "DateTimeUTC";
            try { dr["ModuleIncluded"] = (mst["ModuleIncluded"] ?? "").Trim().Left(9999999); } catch { }
            drType["ModuleIncluded"] = string.Empty; drDisp["ModuleIncluded"] = "TextBox";
            try { dr["FeatureIncluded"] = (mst["FeatureIncluded"] ?? "").Trim().Left(9999999); } catch { }
            drType["FeatureIncluded"] = string.Empty; drDisp["FeatureIncluded"] = "TextBox";
            try { dr["FeatureExcluded"] = (mst["FeatureExcluded"] ?? "").Trim().Left(9999999); } catch { }
            drType["FeatureExcluded"] = string.Empty; drDisp["FeatureExcluded"] = "TextBox";
            try { dr["CompanyCount"] = (mst["CompanyCount"] ?? "").Trim().Left(9999999); } catch { }
            drType["CompanyCount"] = string.Empty; drDisp["CompanyCount"] = "TextBox";
            try { dr["ProjectCount"] = (mst["ProjectCount"] ?? "").Trim().Left(9999999); } catch { }
            drType["ProjectCount"] = string.Empty; drDisp["ProjectCount"] = "TextBox";
            try { dr["UserCount"] = (mst["UserCount"] ?? "").Trim().Left(9999999); } catch { }
            drType["UserCount"] = string.Empty; drDisp["UserCount"] = "TextBox";
            try { dr["LicenseServerUrl"] = (mst["LicenseServerUrl"] ?? "").Trim().Left(9999999); } catch { }
            drType["LicenseServerUrl"] = string.Empty; drDisp["LicenseServerUrl"] = "TextBox";
            try { dr["LicenseString"] = mst["LicenseString"]; } catch { }
            drType["LicenseString"] = string.Empty; drDisp["LicenseString"] = "MultiLine";

            if (dtl != null)
            {
                ds.Tables["AdmLicenseDef"].Rows.Add(MakeTypRow(ds.Tables["AdmLicenseDef"].NewRow()));
                ds.Tables["AdmLicenseDef"].Rows.Add(MakeDisRow(ds.Tables["AdmLicenseDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmLicenseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmLicenseAdd"].NewRow(), drv, mst["SystemId1317"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmLicenseUpd"].Rows.Add(MakeColRow(ds.Tables["AdmLicenseUpd"].NewRow(), drv, mst["SystemId1317"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmLicenseAdd"].Rows.Add(MakeColRow(ds.Tables["AdmLicenseAdd"].NewRow(), drv, mst["SystemId1317"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmLicenseDel"].Rows.Add(MakeColRow(ds.Tables["AdmLicenseDel"].NewRow(), drv, mst["SystemId1317"], false));
                    }
                }
            }
            ds.Tables["AdmLicense"].Rows.Add(dr); ds.Tables["AdmLicense"].Rows.Add(drType); ds.Tables["AdmLicense"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"SystemId1317",""},
                {"SystemName1317",""},
                {"SystemAbbr1317",""},
                {"InstallID",""},
                {"AppID",""},
                {"AppNameSpace",""},
                {"RegisterInsall","http://www.rintagi.com"},
                {"ExpiryDate",""},
                {"ModuleIncluded",""},
                {"FeatureIncluded",""},
                {"FeatureExcluded",""},
                {"CompanyCount",""},
                {"ProjectCount",""},
                {"UserCount",""},
                {"LicenseServerUrl",""},
                {"AcquireLicense",""},
                {"LicenseString",""},
                {"RenewLicense",""},

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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmLicense1022List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                var effectiveFilterId = GetEffectiveScreenFilterId(filterId, true);                
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmLicense1022";
                context["mKey"] = "SystemId1317";
                context["mVal"] = "SystemId1317Text";
                context["mTip"] = "SystemId1317Text";
                context["mImg"] = "SystemId1317Text";
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
            return GetAdmLicense1022List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmLicense1022ById(string keyId, SerializableDictionary<string, string> options)
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
                ValidatedMstId("GetLisAdmLicense1022", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            return GetAdmLicense1022ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmLicense1022DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
            return GetAdmLicense1022DtlById(keyId, options, filterId);
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmLicense1022ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmLicense1022DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
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
                var pid = mst["SystemId1317"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmLicenseData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["SystemId1317"]));

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

                var pid = mst["SystemId1317"];
                isAdd = GetLis("GetLisAdmLicense1022", systemId, screenId, "**" + pid, new List<string>(), "0", "", "N", 1, false).Rows.Count == 0;
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmLicense1022", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "SystemId1317", "", skipValidation);
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

                var ds = PrepAdmLicenseData(mst, dtl, string.IsNullOrEmpty(mst["SystemId1317"]));
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
            