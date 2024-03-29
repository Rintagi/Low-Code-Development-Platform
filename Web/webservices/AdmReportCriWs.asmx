<%@ WebService Language="C#" Class="RO.Web.AdmReportCriWs" %>
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

            
    public class AdmReportCri69 : DataSet
    {
        public AdmReportCri69()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmReportCri")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportCriDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportCriAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportCriUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmReportCriDel")));
            this.DataSetName = "AdmReportCri69";
            this.Namespace = "http://Rintagi.com/DataSet/AdmReportCri69";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ReportCriId97", typeof(string));
            columns.Add("ReportId97", typeof(string));
            columns.Add("TabIndex97", typeof(string));
            columns.Add("ColumnName97", typeof(string));
            columns.Add("ReportGrpId97", typeof(string));
            columns.Add("LabelCss97", typeof(string));
            columns.Add("ContentCss97", typeof(string));
            columns.Add("DefaultValue97", typeof(string));
            columns.Add("TableId97", typeof(string));
            columns.Add("TableAbbr97", typeof(string));
            columns.Add("RequiredValid97", typeof(string));
            columns.Add("DataTypeId97", typeof(string));
            columns.Add("DataTypeSize97", typeof(string));
            columns.Add("DisplayModeId97", typeof(string));
            columns.Add("ColumnSize97", typeof(string));
            columns.Add("RowSize97", typeof(string));
            columns.Add("DdlKeyColumnName97", typeof(string));
            columns.Add("DdlRefColumnName97", typeof(string));
            columns.Add("DdlSrtColumnName97", typeof(string));
            columns.Add("DdlFtrColumnId97", typeof(string));
            columns.Add("WhereClause97", typeof(string));
            columns.Add("RegClause97", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("ReportCriId97", typeof(string));
            columns.Add("ReportCriHlpId98", typeof(string));
            columns.Add("CultureId98", typeof(string));
            columns.Add("ColumnHeader98", typeof(string));
            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmReportCriWs : RO.Web.AsmxBase
    {
        const int screenId = 69;
        const byte systemId = 3;
        const string programName = "AdmReportCri69";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmReportCri69"; }
        protected override string GetMstTableName(bool underlying = true) { return "ReportCri"; }
        protected override string GetDtlTableName(bool underlying = true) { return "ReportCriHlp"; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "ReportCriId" : "ReportCriId97"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "ReportCriHlpId" : "ReportCriHlpId98"; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {
            {"ReportId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportId3S1075"},{"mKey","ReportId97"},{"mVal","ReportId97Text"}, }},
            {"ReportGrpId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlReportGrpId3S1076"},{"mKey","ReportGrpId97"},{"mVal","ReportGrpId97Text"}, }},
            {"TableId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlTableId3S1077"},{"mKey","TableId97"},{"mVal","TableId97Text"}, }},
            {"DataTypeId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDataTypeId3S1081"},{"mKey","DataTypeId97"},{"mVal","DataTypeId97Text"}, }},
            {"DisplayModeId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDisplayModeId3S1083"},{"mKey","DisplayModeId97"},{"mVal","DisplayModeId97Text"}, }},
            {"DdlFtrColumnId97", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDdlFtrColumnId3S3367"},{"mKey","DdlFtrColumnId97"},{"mVal","DdlFtrColumnId97Text"}, {"refCol","ReportId"},{"refColDataType","Int"},{"refColSrc","Mst"},{"refColSrcName","ReportId97"}}},
            {"CultureId98", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S1097"},{"mKey","CultureId98"},{"mVal","CultureId98Text"}, }},
        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["ReportCriId97"] = System.Data.OleDb.OleDbType.Numeric.ToString();
            dr["ReportCriHlpId98"] = System.Data.OleDb.OleDbType.Numeric.ToString();
            dr["CultureId98"] = System.Data.OleDb.OleDbType.Numeric.ToString();
            dr["ColumnHeader98"] = System.Data.OleDb.OleDbType.VarWChar.ToString();

            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["ReportCriId97"] = "TextBox";
            dr["ReportCriHlpId98"] = "TextBox";
            dr["CultureId98"] = "AutoComplete";
            dr["ColumnHeader98"] = "TextBox";

            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["ReportCriId97"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {
                dr["ReportCriHlpId98"] = (drv["ReportCriHlpId98"] ?? "").ToString().Trim().Left(9999999);
                dr["CultureId98"] = drv["CultureId98"];
                dr["ColumnHeader98"] = (drv["ColumnHeader98"] ?? "").ToString().Trim().Left(50);

            }
            return dr;
        }

        private AdmReportCri69 PrepAdmReportCriData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmReportCri69 ds = new AdmReportCri69();
            DataRow dr = ds.Tables["AdmReportCri"].NewRow();
            DataRow drType = ds.Tables["AdmReportCri"].NewRow();
            DataRow drDisp = ds.Tables["AdmReportCri"].NewRow();

            if (bAdd) { dr["ReportCriId97"] = string.Empty; } else { dr["ReportCriId97"] = mst["ReportCriId97"]; }
            drType["ReportCriId97"] = "Numeric"; drDisp["ReportCriId97"] = "TextBox";
            try { dr["ReportId97"] = mst["ReportId97"]; } catch { }
            drType["ReportId97"] = "Numeric"; drDisp["ReportId97"] = "AutoComplete";
            try { dr["TabIndex97"] = (mst["TabIndex97"] ?? "").Trim().Left(9999999); } catch { }
            drType["TabIndex97"] = "Numeric"; drDisp["TabIndex97"] = "TextBox";
            try { dr["ColumnName97"] = (mst["ColumnName97"] ?? "").Trim().Left(50); } catch { }
            drType["ColumnName97"] = "VarChar"; drDisp["ColumnName97"] = "TextBox";
            try { dr["ReportGrpId97"] = mst["ReportGrpId97"]; } catch { }
            drType["ReportGrpId97"] = "Numeric"; drDisp["ReportGrpId97"] = "DropDownList";
            try { dr["LabelCss97"] = (mst["LabelCss97"] ?? "").Trim().Left(100); } catch { }
            drType["LabelCss97"] = "VarChar"; drDisp["LabelCss97"] = "TextBox";
            try { dr["ContentCss97"] = (mst["ContentCss97"] ?? "").Trim().Left(100); } catch { }
            drType["ContentCss97"] = "VarChar"; drDisp["ContentCss97"] = "TextBox";
            try { dr["DefaultValue97"] = (mst["DefaultValue97"] ?? "").Trim().Left(100); } catch { }
            drType["DefaultValue97"] = "VarWChar"; drDisp["DefaultValue97"] = "TextBox";
            try { dr["TableId97"] = mst["TableId97"]; } catch { }
            drType["TableId97"] = "Numeric"; drDisp["TableId97"] = "DropDownList";
            try { dr["TableAbbr97"] = (mst["TableAbbr97"] ?? "").Trim().Left(10); } catch { }
            drType["TableAbbr97"] = "VarChar"; drDisp["TableAbbr97"] = "TextBox";
            try { dr["RequiredValid97"] = (mst["RequiredValid97"] ?? "").Trim().Left(1); } catch { }
            drType["RequiredValid97"] = "Char"; drDisp["RequiredValid97"] = "CheckBox";
            try { dr["DataTypeId97"] = mst["DataTypeId97"]; } catch { }
            drType["DataTypeId97"] = "Numeric"; drDisp["DataTypeId97"] = "DropDownList";
            try { dr["DataTypeSize97"] = (mst["DataTypeSize97"] ?? "").Trim().Left(9999999); } catch { }
            drType["DataTypeSize97"] = "Numeric"; drDisp["DataTypeSize97"] = "TextBox";
            try { dr["DisplayModeId97"] = mst["DisplayModeId97"]; } catch { }
            drType["DisplayModeId97"] = "Numeric"; drDisp["DisplayModeId97"] = "DropDownList";
            try { dr["ColumnSize97"] = (mst["ColumnSize97"] ?? "").Trim().Left(9999999); } catch { }
            drType["ColumnSize97"] = "Numeric"; drDisp["ColumnSize97"] = "TextBox";
            try { dr["RowSize97"] = (mst["RowSize97"] ?? "").Trim().Left(9999999); } catch { }
            drType["RowSize97"] = "Numeric"; drDisp["RowSize97"] = "TextBox";
            try { dr["DdlKeyColumnName97"] = (mst["DdlKeyColumnName97"] ?? "").Trim().Left(50); } catch { }
            drType["DdlKeyColumnName97"] = "VarChar"; drDisp["DdlKeyColumnName97"] = "TextBox";
            try { dr["DdlRefColumnName97"] = (mst["DdlRefColumnName97"] ?? "").Trim().Left(50); } catch { }
            drType["DdlRefColumnName97"] = "VarChar"; drDisp["DdlRefColumnName97"] = "TextBox";
            try { dr["DdlSrtColumnName97"] = (mst["DdlSrtColumnName97"] ?? "").Trim().Left(50); } catch { }
            drType["DdlSrtColumnName97"] = "VarChar"; drDisp["DdlSrtColumnName97"] = "TextBox";
            try { dr["DdlFtrColumnId97"] = mst["DdlFtrColumnId97"]; } catch { }
            drType["DdlFtrColumnId97"] = "Numeric"; drDisp["DdlFtrColumnId97"] = "AutoComplete";
            try { dr["WhereClause97"] = mst["WhereClause97"]; } catch { }
            drType["WhereClause97"] = "VarChar"; drDisp["WhereClause97"] = "MultiLine";
            try { dr["RegClause97"] = mst["RegClause97"]; } catch { }
            drType["RegClause97"] = "VarChar"; drDisp["RegClause97"] = "MultiLine";

            if (dtl != null)
            {
                ds.Tables["AdmReportCriDef"].Rows.Add(MakeTypRow(ds.Tables["AdmReportCriDef"].NewRow()));
                ds.Tables["AdmReportCriDef"].Rows.Add(MakeDisRow(ds.Tables["AdmReportCriDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmReportCriAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReportCriAdd"].NewRow(), drv, mst["ReportCriId97"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r["ReportCriHlpId98"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmReportCriUpd"].Rows.Add(MakeColRow(ds.Tables["AdmReportCriUpd"].NewRow(), drv, mst["ReportCriId97"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r["ReportCriHlpId98"]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmReportCriAdd"].Rows.Add(MakeColRow(ds.Tables["AdmReportCriAdd"].NewRow(), drv, mst["ReportCriId97"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r["ReportCriHlpId98"] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmReportCriDel"].Rows.Add(MakeColRow(ds.Tables["AdmReportCriDel"].NewRow(), drv, mst["ReportCriId97"], false));
                    }
                }
            }
            ds.Tables["AdmReportCri"].Rows.Add(dr); ds.Tables["AdmReportCri"].Rows.Add(drType); ds.Tables["AdmReportCri"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"ReportCriId97",""},
                {"ReportId97",""},
                {"TabIndex97",""},
                {"ColumnName97",""},
                {"ReportGrpId97",""},
                {"LabelCss97",""},
                {"ContentCss97",""},
                {"DefaultValue97",""},
                {"TableId97",""},
                {"TableAbbr97",""},
                {"RequiredValid97",""},
                {"DataTypeId97",""},
                {"DataTypeSize97",""},
                {"DisplayModeId97",""},
                {"ColumnSize97",""},
                {"RowSize97",""},
                {"DdlKeyColumnName97",""},
                {"DdlRefColumnName97",""},
                {"DdlSrtColumnName97",""},
                {"DdlFtrColumnId97",""},
                {"WhereClause97",""},
                {"RegClause97",""},

            };
            /* AsmxRule: Init Master Table */
                

            /* AsmxRule End: Init Master Table */

            return mst;
        }

        protected override SerializableDictionary<string, string> InitDtl()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"ReportCriHlpId98",""},
                {"CultureId98","1"},
                {"ColumnHeader98",""},

            };
            /* AsmxRule: Init Detail Table */
            

            /* AsmxRule End: Init Detail Table */
            return mst;
        }
            

        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmReportCri69List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                var effectiveFilterId = GetEffectiveScreenFilterId(filterId, true);                
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmReportCri69";
                context["mKey"] = "ReportCriId97";
                context["mVal"] = "ReportCriId97Text";
                context["mTip"] = "ReportCriId97Text";
                context["mImg"] = "ReportCriId97Text";
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
            return GetAdmReportCri69List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmReportCri69ById(string keyId, SerializableDictionary<string, string> options)
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
                ValidatedMstId("GetLisAdmReportCri69", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            return GetAdmReportCri69ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmReportCri69DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
        {
            bool refreshUsrImpr = options.ContainsKey("ReAuth") && options["ReAuth"] == "Y" ;
            string filterName = options.ContainsKey("FilterName") ? options["FilterName"] : "";

            Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId, true, true, refreshUsrImpr);
                string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;            
                
                ValidatedMstId("GetLisAdmReportCri69", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                string dtlBlobIconOption = !options.ContainsKey("DtlBlob") ? "I" : options["DtlBlob"];
                var dtlBlob = GetBlobOption(dtlBlobIconOption);;
                DataTable dtColAuth = _GetAuthCol(GetScreenId());
                DataTable dtColLabel = _GetScreenLabel(GetScreenId());
                var utcColumnList = dtColLabel.AsEnumerable().Where(dr => dr["DisplayMode"].ToString().Contains("UTC")).Select(dr => dr["ColumnName"].ToString() + dr["TableId"].ToString()).ToArray();
                HashSet<string> utcColumns = new HashSet<string>(utcColumnList);;
                Dictionary<string, DataRow> colAuth = _GetAuthColDict(dtColAuth, GetScreenId());
                ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmReportCri69DtlById", keyId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(!string.IsNullOrEmpty(filterName) ? filterName : filterId.ToString(), false), base.LImpr, base.LCurr);
                mr.data = DataTableToListOfObject(dt, dtlBlob, colAuth, utcColumns);
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
            return GetAdmReportCri69DtlById(keyId, options, filterId);
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmReportCri69ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmReportCri69DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
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
                var pid = mst["ReportCriId97"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmReportCriData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["ReportCriId97"]));

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

                var pid = mst["ReportCriId97"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmReportCri69", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = _GetDtlById(pid, GetEffectiveScreenFilterId("0", false)); 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr["ReportCriHlpId98"].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "ReportCriId97", "ReportCriHlpId98", skipValidation);
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

                var ds = PrepAdmReportCriData(mst, dtl, string.IsNullOrEmpty(mst["ReportCriId97"]));
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
                dtDtl = _GetDtlById(pid, 0);
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
                result.dtl = DataTableToListOfObject(dtDtl, IncludeBLOB.None, colAuth, utcColumns);
                    
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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportId97List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlReportId3S1075";
            context["addnew"] = "Y";
            context["mKey"] = "ReportId97";
            context["mVal"] = "ReportId97Text";
            context["mTip"] = "ReportId97Text";
            context["mImg"] = "ReportId97Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetReportGrpId97List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlReportGrpId3S1076", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ReportGrpId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTableId97List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlTableId3S1077", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "TableId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDataTypeId97List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlDataTypeId3S1081", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DataTypeId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDisplayModeId97List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlDisplayModeId3S1083", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DisplayModeId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDdlFtrColumnId97List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlDdlFtrColumnId3S3367";
            context["addnew"] = "Y";
            context["mKey"] = "DdlFtrColumnId97";
            context["mVal"] = "DdlFtrColumnId97Text";
            context["mTip"] = "DdlFtrColumnId97Text";
            context["mImg"] = "DdlFtrColumnId97Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DdlFtrColumnId97", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId98List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCultureId3S1097";
            context["addnew"] = "Y";
            context["mKey"] = "CultureId98";
            context["mVal"] = "CultureId98Text";
            context["mTip"] = "CultureId98Text";
            context["mImg"] = "CultureId98Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId98", emptyAutoCompleteResponse));
            return ret;
        }

        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            