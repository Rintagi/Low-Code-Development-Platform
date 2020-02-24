<%@ WebService Language="C#" Class="RO.Web.AdmAppInfoWs" %>
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
            
    public class AdmAppInfo82 : DataSet
    {
        public AdmAppInfo82()
        {
            this.Tables.Add(MakeColumns(new DataTable("AdmAppInfo")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppInfoDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppInfoAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppInfoUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmAppInfoDel")));
            this.DataSetName = "AdmAppInfo82";
            this.Namespace = "http://Rintagi.com/DataSet/AdmAppInfo82";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("AppInfoId135", typeof(string));
            columns.Add("VersionMa135", typeof(string));
            columns.Add("VersionMi135", typeof(string));
            columns.Add("VersionDt135", typeof(string));
            columns.Add("LunarDt", typeof(string));
            columns.Add("CultureTypeName135", typeof(string));
            columns.Add("VersionValue135", typeof(string));
            columns.Add("AppZipId135", typeof(string));
            columns.Add("Prerequisite135", typeof(string));
            columns.Add("Readme135", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("AppInfoId135", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmAppInfoWs : RO.Web.AsmxBase
    {
        const int screenId = 82;
        const byte systemId = 3;
        const string programName = "AdmAppInfo82";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmAppInfo82"; }
        protected override string GetMstTableName(bool underlying = true) { return "AppInfo"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "AppInfoId" : "AppInfoId135"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {
            {"CultureTypeName135", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureTypeName3S3073"},{"mKey","CultureTypeName135"},{"mVal","CultureTypeName135Text"}, }},
            {"AppZipId135", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAppZipId3S1416"},{"mKey","DocId"},{"mVal","FileName"}, { "tableName", "AppZipId" }}},
            {"AppItemLink135", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAppItemLink3S1349"},{"mKey","AppItemLink135"},{"mVal","AppItemLink135Text"}, }},
        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["AppInfoId135"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["AppInfoId135"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["AppInfoId135"] = keyId;
            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmAppInfo82 PrepAdmAppInfoData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmAppInfo82 ds = new AdmAppInfo82();
            DataRow dr = ds.Tables["AdmAppInfo"].NewRow();
            DataRow drType = ds.Tables["AdmAppInfo"].NewRow();
            DataRow drDisp = ds.Tables["AdmAppInfo"].NewRow();

            if (bAdd) { dr["AppInfoId135"] = string.Empty; } else { dr["AppInfoId135"] = mst["AppInfoId135"]; }
            drType["AppInfoId135"] = "Numeric"; drDisp["AppInfoId135"] = "TextBox";
            try { dr["VersionMa135"] = (mst["VersionMa135"] ?? "").Trim().Left(9999999); } catch { }
            drType["VersionMa135"] = "Numeric"; drDisp["VersionMa135"] = "TextBox";
            try { dr["VersionMi135"] = (mst["VersionMi135"] ?? "").Trim().Left(9999999); } catch { }
            drType["VersionMi135"] = "Numeric"; drDisp["VersionMi135"] = "TextBox";
            try { dr["VersionDt135"] = mst["VersionDt135"]; } catch { }
            drType["VersionDt135"] = "DBDate"; drDisp["VersionDt135"] = "LongDate";
            try { dr["LunarDt"] = mst["LunarDt"]; } catch { }
            drType["LunarDt"] = string.Empty; drDisp["LunarDt"] = "Label";
            try { dr["CultureTypeName135"] = mst["CultureTypeName135"]; } catch { }
            drType["CultureTypeName135"] = "VarChar"; drDisp["CultureTypeName135"] = "AutoComplete";
            try { dr["VersionValue135"] = Decimal.Parse((mst["VersionValue135"] ?? "").Trim(), System.Globalization.NumberStyles.Currency, new System.Globalization.CultureInfo(base.LUser.Culture)).ToString(); } catch { }
            drType["VersionValue135"] = "Currency"; drDisp["VersionValue135"] = "Currency";
            try { dr["AppZipId135"] = mst["AppZipId135"]; } catch { }
            drType["AppZipId135"] = "Numeric"; drDisp["AppZipId135"] = "Document";
            
            try { dr["Prerequisite135"] = mst["Prerequisite135"]; } catch { }
            drType["Prerequisite135"] = "VarWChar"; drDisp["Prerequisite135"] = "MultiLine";
            try { dr["Readme135"] = mst["Readme135"]; } catch { }
            drType["Readme135"] = "VarWChar"; drDisp["Readme135"] = "MultiLine";

            if (dtl != null)
            {
                ds.Tables["AdmAppInfoDef"].Rows.Add(MakeTypRow(ds.Tables["AdmAppInfoDef"].NewRow()));
                ds.Tables["AdmAppInfoDef"].Rows.Add(MakeDisRow(ds.Tables["AdmAppInfoDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmAppInfoAdd"].Rows.Add(MakeColRow(ds.Tables["AdmAppInfoAdd"].NewRow(), drv, mst["AppInfoId135"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmAppInfoUpd"].Rows.Add(MakeColRow(ds.Tables["AdmAppInfoUpd"].NewRow(), drv, mst["AppInfoId135"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmAppInfoAdd"].Rows.Add(MakeColRow(ds.Tables["AdmAppInfoAdd"].NewRow(), drv, mst["AppInfoId135"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmAppInfoDel"].Rows.Add(MakeColRow(ds.Tables["AdmAppInfoDel"].NewRow(), drv, mst["AppInfoId135"], false));
                    }
                }
            }
            ds.Tables["AdmAppInfo"].Rows.Add(dr); ds.Tables["AdmAppInfo"].Rows.Add(drType); ds.Tables["AdmAppInfo"].Rows.Add(drDisp);
            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"AppInfoId135",""},
                {"VersionMa135",""},
                {"VersionMi135",""},
                {"VersionDt135",""},
                {"LunarDt",""},
                {"CultureTypeName135",LUser.Culture},
                {"VersionValue135",""},
                {"AppZipId135",""},
                {"AppItemLink135",""},
                {"Prerequisite135",""},
                {"Readme135",""},

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
        protected override DataTable _GetDocList(string mstId, string screenColumnName)
        {
            return (new AdminSystem()).GetDdl(GetScreenId(), GetDdlContext()[screenColumnName]["method"], false, true, 0, mstId, LcAppConnString, LcAppPw, string.Empty, LImpr, LCurr);
        }
        protected override string _GetDocTableName(string screenColumnName)
        {
            return GetDdlContext()[screenColumnName]["tableName"];
        }   

            

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppInfo82List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmAppInfo82";
                context["mKey"] = "AppInfoId135";
                context["mVal"] = "AppInfoId135Text";
                context["mTip"] = "AppInfoId135Text";
                context["mImg"] = "AppInfoId135Text";
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
            return GetAdmAppInfo82List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppInfo82ById(string keyId, SerializableDictionary<string, string> options)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;


            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                string mstBlobIconOption = !options.ContainsKey("MstBlob") ? "N" : options["MstBlob"];
                string dtlBlobIconOption = !options.ContainsKey("DtlBlob") ? "N" : options["DtlBlob"];
                var mstBlob = GetBlobOption(mstBlobIconOption);
                var dtlBlob = GetBlobOption(dtlBlobIconOption);
                string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                bool includeDtl = (!options.ContainsKey("IncludeDtl") || options["IncludeDtl"] == "Y") && !String.IsNullOrEmpty(GetDtlTableName());
                ValidatedMstId("GetLisAdmAppInfo82", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            return GetAdmAppInfo82ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmAppInfo82DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;

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
            return GetAdmAppInfo82DtlById(keyId, options, filterId);
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
            return (new RO.Access3.AdminAccess()).GetMstById("GetAdmAppInfo82ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);

        }
        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Access3.AdminAccess()).GetDtlById(screenId, "GetAdmAppInfo82DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

        }
        protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
        {
            return ddlContext;
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMst(SerializableDictionary<string, string> mst, SerializableDictionary<string, string> options)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;

            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                var pid = mst["AppInfoId135"];
                var ds = PrepAdmAppInfoData(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst["AppInfoId135"]));
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
            bool isAdd = false;
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                SerializableDictionary<string, string> skipValidation = new SerializableDictionary<string, string>() { { "SkipAllMst", "SilentColReadOnly" }, { "SkipAllDtl", "SilentColReadOnly" } };
                /* AsmxRule: Save Data Before */


                /* AsmxRule End: Save Data Before */

                var pid = mst["AppInfoId135"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmAppInfo82", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "AppInfoId135", "", skipValidation);
                if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                {
                    return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                    {
                        status = "failed",
                        errorMsg = "content invalid " + string.Join(" ", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                        validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                    };
                }
                var ds = PrepAdmAppInfoData(mst, dtl, string.IsNullOrEmpty(mst["AppInfoId135"]));
                string msg = string.Empty;

                if (isAdd)
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

                /* AsmxRule: Save Data After */


                /* AsmxRule End: Save Data After */

                ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                SaveDataResponse result = new SaveDataResponse();
                DataTable dtColAuth = _GetAuthCol(GetScreenId());
                DataTable dtColLabel = _GetScreenLabel(GetScreenId());
                Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr["ColName"].ToString());
                var utcColumnList = dtColLabel.AsEnumerable().Where(dr => dr["DisplayMode"].ToString().Contains("UTC")).Select(dr => dr["ColumnName"].ToString() + dr["TableId"].ToString()).ToArray();
                HashSet<string> utcColumns = new HashSet<string>(utcColumnList);

                result.mst = DataTableToListOfObject(dtMst, IncludeBLOB.Icon, colAuth, utcColumns)[0];

                    
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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureTypeName135List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCultureTypeName3S3073";
            context["addnew"] = "Y";
            context["mKey"] = "CultureTypeName135";
            context["mVal"] = "CultureTypeName135Text";
            context["mTip"] = "CultureTypeName135Text";
            context["mImg"] = "CultureTypeName135Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureTypeName135", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelAppZipId135(string mstId, string dtlId, bool isMaster, string[] docIdList, string screenColumnName)
        {
            return DelMultiDoc(mstId, dtlId, isMaster, docIdList, "AppZipId135", "AppZipId", "AppInfo", "AppZipId", "AppInfoId");
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> SaveAppZipId135(string mstId, string dtlId, bool isMaster, string docId, bool overwrite, string screenColumnName, string docJson, SerializableDictionary<string, string> options)
        {
            return SaveMultiDoc(mstId, dtlId, isMaster, docId, overwrite, "AppZipId135", "AppZipId", docJson, options);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAppZipId135List(string mstId, string dtlId, bool isMaster)
        {
            return GetMultiDocList(mstId, dtlId, isMaster, "AppZipId135", "GetDdlAppZipId3S1416");
        }

                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAppItemLink135List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, "GetDdlAppItemLink3S1349", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AppItemLink135", emptyAutoCompleteResponse));
            return ret;
        }
        public ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> _LoadInitPage(SerializableDictionary<string, string> options)
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
                var dtLabel = _GetLabels("AdmAppInfo");
                var SearchList = GetAdmAppInfo82List("", 0, "");
                var CultureTypeName135List = GetCultureTypeName135List("", 0, "");
                var AppItemLink135List = GetAppItemLink135List("", 0, "");

                LoadScreenPageResponse result = new LoadScreenPageResponse();

                ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                mr.status = "success";
                mr.errorMsg = "";
                mr.data = result;
                return mr;
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", null));
            return ret;
        }           
            

        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            