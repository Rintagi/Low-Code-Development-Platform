<%@ WebService Language="C#" Class="RO.Web.AdmUsrWs" %>
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

            
    public class AdmUsr1 : DataSet
    {
        public AdmUsr1()
        {

            this.Tables.Add(MakeColumns(new DataTable("AdmUsr")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmUsrDef")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmUsrAdd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmUsrUpd")));
            this.Tables.Add(MakeDtlColumns(new DataTable("AdmUsrDel")));
            this.DataSetName = "AdmUsr1";
            this.Namespace = "http://Rintagi.com/DataSet/AdmUsr1";
        }

        private DataTable MakeColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("UsrId1", typeof(string));
            columns.Add("LoginName1", typeof(string));
            columns.Add("UsrName1", typeof(string));
            columns.Add("CultureId1", typeof(string));
            columns.Add("DefCompanyId1", typeof(string));
            columns.Add("DefProjectId1", typeof(string));
            columns.Add("DefSystemId1", typeof(string));
            columns.Add("UsrEmail1", typeof(string));
            columns.Add("UsrMobile1", typeof(string));
            columns.Add("UsrGroupLs1", typeof(string));
            columns.Add("PicMed1", typeof(string));
            columns.Add("IPAlert1", typeof(string));
            columns.Add("PwdNoRepeat1", typeof(string));
            columns.Add("PwdDuration1", typeof(string));
            columns.Add("PwdWarn1", typeof(string));
            columns.Add("Active1", typeof(string));
            columns.Add("InternalUsr1", typeof(string));
            columns.Add("TechnicalUsr1", typeof(string));
            columns.Add("EmailLink1", typeof(string));
            columns.Add("MobileLink1", typeof(string));
            columns.Add("FailedAttempt1", typeof(string));
            columns.Add("LastSuccessDt1", typeof(string));
            columns.Add("LastFailedDt1", typeof(string));
            columns.Add("CompanyLs1", typeof(string));
            columns.Add("ProjectLs1", typeof(string));
            columns.Add("HintQuestionId1", typeof(string));
            columns.Add("HintAnswer1", typeof(string));
            columns.Add("InvestorId1", typeof(string));
            columns.Add("CustomerId1", typeof(string));
            columns.Add("VendorId1", typeof(string));
            columns.Add("AgentId1", typeof(string));
            columns.Add("BrokerId1", typeof(string));
            columns.Add("MemberId1", typeof(string));
            columns.Add("LenderId1", typeof(string));
            columns.Add("BorrowerId1", typeof(string));
            columns.Add("GuarantorId1", typeof(string));
            columns.Add("UsageStat", typeof(string));
            columns.Add("NotificationTitle", typeof(string));
            columns.Add("NotificationContent", typeof(string));
            return dt;
        }

        private DataTable MakeDtlColumns(DataTable dt)
        {
            DataColumnCollection columns = dt.Columns;
            columns.Add("UsrId1", typeof(string));

            return dt;
        }
    }
            
    [ScriptService()]
    [WebService(Namespace = "http://Rintagi.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class AdmUsrWs : RO.Web.AsmxBase
    {
        const int screenId = 1;
        const byte systemId = 3;
        const string programName = "AdmUsr1";

        protected override byte GetSystemId() { return systemId; }
        protected override int GetScreenId() { return screenId; }
        protected override string GetProgramName() { return programName; }
        protected override string GetValidateMstIdSPName() { return "GetLisAdmUsr1"; }
        protected override string GetMstTableName(bool underlying = true) { return "Usr"; }
        protected override string GetDtlTableName(bool underlying = true) { return ""; }
        protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? "UsrId" : "UsrId1"; }
        protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? "" : ""; }
            
        Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>() {
            {"CultureId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCultureId3S1746"},{"mKey","CultureId1"},{"mVal","CultureId1Text"}, }},
            {"DefCompanyId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDefCompanyId3S1304"},{"mKey","DefCompanyId1"},{"mVal","DefCompanyId1Text"}, }},
            {"DefProjectId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDefProjectId3S1481"},{"mKey","DefProjectId1"},{"mVal","DefProjectId1Text"}, }},
            {"DefSystemId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlDefSystemId3S497"},{"mKey","DefSystemId1"},{"mVal","DefSystemId1Text"}, }},
            {"UsrGroupLs1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlUsrGroupLs3S1741"},{"mKey","UsrGroupLs1"},{"mVal","UsrGroupLs1Text"}, }},
            {"UsrImprLink1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlUsrImprLink3S1749"},{"mKey","UsrImprLink1"},{"mVal","UsrImprLink1Text"}, }},
            {"CompanyLs1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCompanyLs3S1742"},{"mKey","CompanyLs1"},{"mVal","CompanyLs1Text"}, }},
            {"ProjectLs1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlProjectLs3S1743"},{"mKey","ProjectLs1"},{"mVal","ProjectLs1Text"}, }},
            {"HintQuestionId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlHintQuestionId3S1119"},{"mKey","HintQuestionId1"},{"mVal","HintQuestionId1Text"}, }},
            {"InvestorId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlInvestorId3S316"},{"mKey","InvestorId1"},{"mVal","InvestorId1Text"}, }},
            {"CustomerId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlCustomerId3S6"},{"mKey","CustomerId1"},{"mVal","CustomerId1Text"}, }},
            {"VendorId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlVendorId3S7"},{"mKey","VendorId1"},{"mVal","VendorId1Text"}, }},
            {"AgentId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlAgentId3S934"},{"mKey","AgentId1"},{"mVal","AgentId1Text"}, }},
            {"BrokerId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBrokerId3S935"},{"mKey","BrokerId1"},{"mVal","BrokerId1Text"}, }},
            {"MemberId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlMemberId3S8"},{"mKey","MemberId1"},{"mVal","MemberId1Text"}, }},
            {"LenderId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlLenderId3S4185"},{"mKey","LenderId1"},{"mVal","LenderId1Text"}, }},
            {"BorrowerId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlBorrowerId3S4186"},{"mKey","BorrowerId1"},{"mVal","BorrowerId1Text"}, }},
            {"GuarantorId1", new SerializableDictionary<string,string>() {{"scr",screenId.ToString()},{"csy",systemId.ToString()},{"conn",""},{"addnew","N"},{"isSys","N"}, {"method","GetDdlGuarantorId3S4187"},{"mKey","GuarantorId1"},{"mVal","GuarantorId1Text"}, }},
        };

        private DataRow MakeTypRow(DataRow dr)
        {
            dr["UsrId1"] = System.Data.OleDb.OleDbType.Numeric.ToString();


            return dr;
        }

        private DataRow MakeDisRow(DataRow dr)
        {
            dr["UsrId1"] = "TextBox";


            return dr;
        }

        private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd)
        {
            dr["UsrId1"] = keyId;

            DataTable dtAuth = _GetAuthCol(screenId);
            if (dtAuth != null)
            {


            }
            return dr;
        }

        private AdmUsr1 PrepAdmUsrData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
        {
            AdmUsr1 ds = new AdmUsr1();
            DataRow dr = ds.Tables["AdmUsr"].NewRow();
            DataRow drType = ds.Tables["AdmUsr"].NewRow();
            DataRow drDisp = ds.Tables["AdmUsr"].NewRow();

            if (bAdd) { dr["UsrId1"] = string.Empty; } else { dr["UsrId1"] = mst["UsrId1"]; }
            drType["UsrId1"] = "Numeric"; drDisp["UsrId1"] = "TextBox";
            try { dr["LoginName1"] = (mst["LoginName1"] ?? "").Trim().Left(32); } catch { }
            drType["LoginName1"] = "VarWChar"; drDisp["LoginName1"] = "TextBox";
            try { dr["UsrName1"] = (mst["UsrName1"] ?? "").Trim().Left(50); } catch { }
            drType["UsrName1"] = "VarWChar"; drDisp["UsrName1"] = "TextBox";
            try { dr["CultureId1"] = mst["CultureId1"]; } catch { }
            drType["CultureId1"] = "Numeric"; drDisp["CultureId1"] = "AutoComplete";
            try { dr["DefCompanyId1"] = mst["DefCompanyId1"]; } catch { }
            drType["DefCompanyId1"] = "Numeric"; drDisp["DefCompanyId1"] = "DropDownList";
            try { dr["DefProjectId1"] = mst["DefProjectId1"]; } catch { }
            drType["DefProjectId1"] = "Numeric"; drDisp["DefProjectId1"] = "DropDownList";
            try { dr["DefSystemId1"] = mst["DefSystemId1"]; } catch { }
            drType["DefSystemId1"] = "Numeric"; drDisp["DefSystemId1"] = "DropDownList";
            try { dr["UsrEmail1"] = (mst["UsrEmail1"] ?? "").Trim().Left(50); } catch { }
            drType["UsrEmail1"] = "VarWChar"; drDisp["UsrEmail1"] = "TextBox";
            try { dr["UsrMobile1"] = (mst["UsrMobile1"] ?? "").Trim().Left(50); } catch { }
            drType["UsrMobile1"] = "VarChar"; drDisp["UsrMobile1"] = "TextBox";
            try { dr["UsrGroupLs1"] = mst["UsrGroupLs1"]; } catch { }
            drType["UsrGroupLs1"] = "VarChar"; drDisp["UsrGroupLs1"] = "ListBox";
            
            try { dr["IPAlert1"] = (mst["IPAlert1"] ?? "").Trim().Left(1); } catch { }
            drType["IPAlert1"] = "Char"; drDisp["IPAlert1"] = "CheckBox";
            try { dr["PwdNoRepeat1"] = (mst["PwdNoRepeat1"] ?? "").Trim().Left(9999999); } catch { }
            drType["PwdNoRepeat1"] = "Numeric"; drDisp["PwdNoRepeat1"] = "TextBox";
            try { dr["PwdDuration1"] = (mst["PwdDuration1"] ?? "").Trim().Left(9999999); } catch { }
            drType["PwdDuration1"] = "Numeric"; drDisp["PwdDuration1"] = "TextBox";
            try { dr["PwdWarn1"] = (mst["PwdWarn1"] ?? "").Trim().Left(9999999); } catch { }
            drType["PwdWarn1"] = "Numeric"; drDisp["PwdWarn1"] = "TextBox";
            try { dr["Active1"] = (mst["Active1"] ?? "").Trim().Left(1); } catch { }
            drType["Active1"] = "Char"; drDisp["Active1"] = "CheckBox";
            try { dr["InternalUsr1"] = (mst["InternalUsr1"] ?? "").Trim().Left(1); } catch { }
            drType["InternalUsr1"] = "Char"; drDisp["InternalUsr1"] = "CheckBox";
            try { dr["TechnicalUsr1"] = (mst["TechnicalUsr1"] ?? "").Trim().Left(1); } catch { }
            drType["TechnicalUsr1"] = "Char"; drDisp["TechnicalUsr1"] = "CheckBox";
            try { dr["EmailLink1"] = mst["EmailLink1"]; } catch { }
            drType["EmailLink1"] = "VarWChar"; drDisp["EmailLink1"] = "HyperLink";
            try { dr["MobileLink1"] = mst["MobileLink1"]; } catch { }
            drType["MobileLink1"] = "VarChar"; drDisp["MobileLink1"] = "HyperLink";
            try { dr["FailedAttempt1"] = mst["FailedAttempt1"]; } catch { }
            drType["FailedAttempt1"] = "Numeric"; drDisp["FailedAttempt1"] = "StarRating";
            try { dr["LastSuccessDt1"] = mst["LastSuccessDt1"]; } catch { }
            drType["LastSuccessDt1"] = "DBTimeStamp"; drDisp["LastSuccessDt1"] = "LongDateTime";
            try { dr["LastFailedDt1"] = mst["LastFailedDt1"]; } catch { }
            drType["LastFailedDt1"] = "DBTimeStamp"; drDisp["LastFailedDt1"] = "LongDateTime";
            try { dr["CompanyLs1"] = mst["CompanyLs1"]; } catch { }
            drType["CompanyLs1"] = "VarChar"; drDisp["CompanyLs1"] = "ListBox";
            try { dr["ProjectLs1"] = mst["ProjectLs1"]; } catch { }
            drType["ProjectLs1"] = "VarChar"; drDisp["ProjectLs1"] = "ListBox";
            try { dr["HintQuestionId1"] = mst["HintQuestionId1"]; } catch { }
            drType["HintQuestionId1"] = "Numeric"; drDisp["HintQuestionId1"] = "DropDownList";
            try { dr["HintAnswer1"] = mst["HintAnswer1"]; } catch { }
            drType["HintAnswer1"] = "VarWChar"; drDisp["HintAnswer1"] = "EncryptedTextBox";
            try { dr["InvestorId1"] = mst["InvestorId1"]; } catch { }
            drType["InvestorId1"] = "Numeric"; drDisp["InvestorId1"] = "AutoComplete";
            try { dr["CustomerId1"] = mst["CustomerId1"]; } catch { }
            drType["CustomerId1"] = "Numeric"; drDisp["CustomerId1"] = "AutoComplete";
            try { dr["VendorId1"] = mst["VendorId1"]; } catch { }
            drType["VendorId1"] = "Numeric"; drDisp["VendorId1"] = "AutoComplete";
            try { dr["AgentId1"] = mst["AgentId1"]; } catch { }
            drType["AgentId1"] = "Numeric"; drDisp["AgentId1"] = "AutoComplete";
            try { dr["BrokerId1"] = mst["BrokerId1"]; } catch { }
            drType["BrokerId1"] = "Numeric"; drDisp["BrokerId1"] = "AutoComplete";
            try { dr["MemberId1"] = mst["MemberId1"]; } catch { }
            drType["MemberId1"] = "Numeric"; drDisp["MemberId1"] = "AutoComplete";
            try { dr["LenderId1"] = mst["LenderId1"]; } catch { }
            drType["LenderId1"] = "Numeric"; drDisp["LenderId1"] = "AutoComplete";
            try { dr["BorrowerId1"] = mst["BorrowerId1"]; } catch { }
            drType["BorrowerId1"] = "Numeric"; drDisp["BorrowerId1"] = "AutoComplete";
            try { dr["GuarantorId1"] = mst["GuarantorId1"]; } catch { }
            drType["GuarantorId1"] = "Numeric"; drDisp["GuarantorId1"] = "AutoComplete";
            try { dr["UsageStat"] = mst["UsageStat"]; } catch { }
            drType["UsageStat"] = string.Empty; drDisp["UsageStat"] = "Label";
            try { dr["NotificationTitle"] = (mst["NotificationTitle"] ?? "").Trim().Left(9999999); } catch { }
            drType["NotificationTitle"] = string.Empty; drDisp["NotificationTitle"] = "TextBox";
            try { dr["NotificationContent"] = mst["NotificationContent"]; } catch { }
            drType["NotificationContent"] = string.Empty; drDisp["NotificationContent"] = "MultiLine";

            if (dtl != null)
            {
                ds.Tables["AdmUsrDef"].Rows.Add(MakeTypRow(ds.Tables["AdmUsrDef"].NewRow()));
                ds.Tables["AdmUsrDef"].Rows.Add(MakeDisRow(ds.Tables["AdmUsrDef"].NewRow()));
                if (bAdd)
                {
                    foreach (var drv in dtl)
                    {
                        ds.Tables["AdmUsrAdd"].Rows.Add(MakeColRow(ds.Tables["AdmUsrAdd"].NewRow(), drv, mst["UsrId1"], true));
                    }
                }
                else
                {
                    var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") != "delete" select r;
                    foreach (var drv in dtlUpd)
                    {
                        ds.Tables["AdmUsrUpd"].Rows.Add(MakeColRow(ds.Tables["AdmUsrUpd"].NewRow(), drv, mst["UsrId1"], false));
                    }
                    var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""]) select r;
                    foreach (var drv in dtlAdd)
                    {
                        ds.Tables["AdmUsrAdd"].Rows.Add(MakeColRow(ds.Tables["AdmUsrAdd"].NewRow(), drv, mst["UsrId1"], true));
                    }
                    var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""] ?? "").ToString()) && (r.ContainsKey("_mode") ? r["_mode"] : "") == "delete" select r;
                    foreach (var drv in dtlDel)
                    {
                        ds.Tables["AdmUsrDel"].Rows.Add(MakeColRow(ds.Tables["AdmUsrDel"].NewRow(), drv, mst["UsrId1"], false));
                    }
                }
            }
            ds.Tables["AdmUsr"].Rows.Add(dr); ds.Tables["AdmUsr"].Rows.Add(drType); ds.Tables["AdmUsr"].Rows.Add(drDisp);

            return ds;
        }

        protected override SerializableDictionary<string, string> InitMaster()
        {
            var mst = new SerializableDictionary<string, string>()
            {
                {"UsrId1",""},
                {"LoginName1",""},
                {"UsrName1",""},
                {"CultureId1",LUser.CultureId.ToString()},
                {"DefCompanyId1",""},
                {"DefProjectId1",""},
                {"DefSystemId1",""},
                {"UsrEmail1",""},
                {"UsrMobile1","+"},
                {"UsrGroupLs1",""},
                {"UsrImprLink1",""},
                {"PicMed1",""},
                {"IPAlert1","N"},
                {"PwdNoRepeat1","1"},
                {"PwdDuration1","180"},
                {"PwdWarn1","0"},
                {"Active1","Y"},
                {"InternalUsr1",""},
                {"TechnicalUsr1",""},
                {"EmailLink1",""},
                {"MobileLink1",""},
                {"FailedAttempt1",""},
                {"LastSuccessDt1",""},
                {"LastFailedDt1",""},
                {"CompanyLs1",""},
                {"ProjectLs1",""},
                {"HintQuestionId1",""},
                {"HintAnswer1",""},
                {"PeekBtn","~/images/lock.png"},
                {"InvestorId1",""},
                {"CustomerId1",""},
                {"VendorId1",""},
                {"AgentId1",""},
                {"BrokerId1",""},
                {"MemberId1",""},
                {"LenderId1",""},
                {"BorrowerId1",""},
                {"GuarantorId1",""},
                {"UsageStat",""},
                {"NotificationTitle",""},
                {"NotificationContent",""},
                {"SendNotificationBtn",""},

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
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAdmUsr1List(string searchStr, int topN, string filterId)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                var effectiveFilterId = GetEffectiveScreenFilterId(filterId, true);                
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, string> context = new Dictionary<string, string>();
                context["method"] = "GetLisAdmUsr1";
                context["mKey"] = "UsrId1";
                context["mVal"] = "UsrId1Text";
                context["mTip"] = "UsrId1Text";
                context["mImg"] = "UsrId1Text";
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
            return GetAdmUsr1List(searchStr, topN, filterId);
        }

        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmUsr1ById(string keyId, SerializableDictionary<string, string> options)
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
                ValidatedMstId("GetLisAdmUsr1", systemId, screenId, "**" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
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
            return GetAdmUsr1ById(keyId, options);
        }
        [WebMethod(EnableSession = false)]
        public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetAdmUsr1DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
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
            return GetAdmUsr1DtlById(keyId, options, filterId);
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
            return (new RO.Facade3.AdminSystem()).GetMstById("GetAdmUsr1ById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw);
        }

        protected override DataTable _GetDtlById(string mstId, int screenFilterId)
        {
            return (new RO.Facade3.AdminSystem()).GetDtlById(screenId, "GetAdmUsr1DtlById", string.IsNullOrEmpty(mstId) ? "-1" : mstId, LcAppConnString, LcAppPw, GetEffectiveScreenFilterId(screenFilterId.ToString(), false), LImpr, LCurr);
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
                var pid = mst["UsrId1"];
                var dtl = new List<SerializableDictionary<string, string>>();
                if (IsGridOnlyScreen())
                {
                    mst["_mode"] = "delete";
                    dtl.Add(mst.Clone());                    
                }
                var ds = PrepAdmUsrData(mst, dtl, IsGridOnlyScreen() ? false : string.IsNullOrEmpty(mst["UsrId1"]));

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

                var pid = mst["UsrId1"];
                isAdd = string.IsNullOrEmpty(pid);
                if (!isAdd)
                {
                    string jsonCri = options.ContainsKey("CurrentScreenCriteria") ? options["CurrentScreenCriteria"] : null;
                    ValidatedMstId("GetLisAdmUsr1", systemId, screenId, "**" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                }
                else
                {
                    ValidateAction(screenId, "A");
                }

                /* current data */
                DataTable dtMst = _GetMstById(pid);
                DataTable dtDtl = null; 
                int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, "UsrId1", "", skipValidation);
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

                var ds = PrepAdmUsrData(mst, dtl, string.IsNullOrEmpty(mst["UsrId1"]));
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

                if (
                    dtMst.Rows.Count > 0 
                    && mst.ContainsKey("PicMed1") 
                    && !string.IsNullOrEmpty(mst["PicMed1"]) 
                    && (mst["PicMed1"]??"").Contains("base64") 
                    && !(mst["PicMed1"]??"").Contains("\"base64\":null")
                    ) 
                {
                    AddDoc(mst["PicMed1"], dtMst.Rows[0]["UsrId1"].ToString(), "dbo.Usr", "UsrId", "PicMed", options.ContainsKey("resizeImage"));
                }


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
            
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCultureId3S1746";
            context["addnew"] = "Y";
            context["mKey"] = "CultureId1";
            context["mVal"] = "CultureId1Text";
            context["mTip"] = "CultureId1Text";
            context["mImg"] = "CultureId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CultureId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDefCompanyId1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlDefCompanyId3S1304", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DefCompanyId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDefProjectId1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlDefProjectId3S1481", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DefProjectId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetDefSystemId1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlDefSystemId3S497", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "DefSystemId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetUsrGroupLs1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlUsrGroupLs3S1741", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "UsrGroupLs1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetUsrImprLink1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlUsrImprLink3S1749", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "UsrImprLink1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCompanyLs1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlCompanyLs3S1742", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CompanyLs1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetProjectLs1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlProjectLs3S1743", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "ProjectLs1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetHintQuestionId1List(string query, int topN, string filterBy)
        {
            Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
            {
                SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                bool bAll = !query.StartsWith("**");
                bool bAddNew = !query.StartsWith("**");
                string keyId = query.Replace("**", "");
                DataTable dt = (new RO.Facade3.AdminSystem()).GetDdl(screenId, "GetDdlHintQuestionId3S1119", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
                return DataTableToApiResponse(dt, "", 0);
            };
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "HintQuestionId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetInvestorId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlInvestorId3S316";
            context["addnew"] = "Y";
            context["mKey"] = "InvestorId1";
            context["mVal"] = "InvestorId1Text";
            context["mTip"] = "InvestorId1Text";
            context["mImg"] = "InvestorId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "InvestorId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCustomerId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlCustomerId3S6";
            context["addnew"] = "Y";
            context["mKey"] = "CustomerId1";
            context["mVal"] = "CustomerId1Text";
            context["mTip"] = "CustomerId1Text";
            context["mImg"] = "CustomerId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "CustomerId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetVendorId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlVendorId3S7";
            context["addnew"] = "Y";
            context["mKey"] = "VendorId1";
            context["mVal"] = "VendorId1Text";
            context["mTip"] = "VendorId1Text";
            context["mImg"] = "VendorId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "VendorId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetAgentId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlAgentId3S934";
            context["addnew"] = "Y";
            context["mKey"] = "AgentId1";
            context["mVal"] = "AgentId1Text";
            context["mTip"] = "AgentId1Text";
            context["mImg"] = "AgentId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "AgentId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBrokerId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlBrokerId3S935";
            context["addnew"] = "Y";
            context["mKey"] = "BrokerId1";
            context["mVal"] = "BrokerId1Text";
            context["mTip"] = "BrokerId1Text";
            context["mImg"] = "BrokerId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BrokerId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetMemberId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlMemberId3S8";
            context["addnew"] = "Y";
            context["mKey"] = "MemberId1";
            context["mVal"] = "MemberId1Text";
            context["mTip"] = "MemberId1Text";
            context["mImg"] = "MemberId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "MemberId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetLenderId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlLenderId3S4185";
            context["addnew"] = "Y";
            context["mKey"] = "LenderId1";
            context["mVal"] = "LenderId1Text";
            context["mTip"] = "LenderId1Text";
            context["mImg"] = "LenderId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "LenderId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetBorrowerId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlBorrowerId3S4186";
            context["addnew"] = "Y";
            context["mKey"] = "BorrowerId1";
            context["mVal"] = "BorrowerId1Text";
            context["mTip"] = "BorrowerId1Text";
            context["mImg"] = "BorrowerId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "BorrowerId1", emptyAutoCompleteResponse));
            return ret;
        }
                        
        [WebMethod(EnableSession = false)]
        public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetGuarantorId1List(string query, int topN, string filterBy)
        {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
            context["method"] = "GetDdlGuarantorId3S4187";
            context["addnew"] = "Y";
            context["mKey"] = "GuarantorId1";
            context["mVal"] = "GuarantorId1Text";
            context["mTip"] = "GuarantorId1Text";
            context["mImg"] = "GuarantorId1Text";
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
            var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", "GuarantorId1", emptyAutoCompleteResponse));
            return ret;
        }

        /* AsmxRule: Custom Function */


        /* AsmxRule End: Custom Function */
           
    }
}
            