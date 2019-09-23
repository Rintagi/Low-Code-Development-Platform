<%@ WebService Language="C#" Class="SystemWs" %>

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
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Net;


[ScriptService()]
[WebService(Namespace = "http://Rintagi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class SystemWs : AsmxBase
{
    const int screenId = 0;
    const byte systemId = 3;
    const string programName = "System";

    protected override byte GetSystemId() { return systemId; }
    protected override int GetScreenId() { return screenId; }
    protected override string GetProgramName() { return programName; }
    protected override string GetValidateMstIdSPName() { throw new NotImplementedException(); }
    protected override string GetMstTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetDtlTableName(bool underlying = true) { throw new NotImplementedException(); }
    protected override string GetMstKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
    protected override string GetDtlKeyColumnName(bool underlying = false) { throw new NotImplementedException(); }
        
    protected override DataTable _GetMstById(string pid)
    {
        throw new NotImplementedException();
    }
    protected override DataTable _GetDtlById(string pid, int screenFilterId)
    {
        throw new NotImplementedException();
    }
    protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitDtl()
    {
        throw new NotImplementedException();
    }
    protected override SerializableDictionary<string, string> InitMaster()
    {
        throw new NotImplementedException();
    }
    public SystemWs()
        : base()
    {
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCompanyList()
    {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            
            DataTable dt = _GetCompanyList(true);
            return DataTableToDdlApiResponse(dt, "CompanyId", "CompanyDesc");
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", ""));
        return ret;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetCultureList(string CultureId, string langCode)
    {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            LUser = new LoginUsr();
            LUser.UsrId = 1;
            LUser.CultureId = short.Parse(string.IsNullOrEmpty(CultureId) ? "1" : CultureId);
            LCurr = new UsrCurr(0, 0, 3, 3);
            LImpr = new UsrImpr();
            SwitchContext(3, 0, 0, false, false, false);
            DataTable dt = (new AdminSystem()).GetDdl(1, "GetDdlCultureId", false, true, 0, "", LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
            return DataTableToApiResponse(dt, "", 0);
        };
        var ret = ManagedApiCall(fn); 
        return ret;
    }
        
    [WebMethod(EnableSession = false)]
    public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetProjectList(int CompanyId)
    {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            DataTable dt = _GetProjectList(CompanyId,true);
            return DataTableToDdlApiResponse(dt, "ProjectId", "ProjectDesc");
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", ""));
        return ret;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> GetTimeZoneList()
    {
        Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            DataTable dt = _GetTimeZoneList();
            if (dt.Rows.Count > 0 ) { dt.Rows[0]["TimeZoneName"] = " "; }
            return DataTableToDdlApiResponse(dt, "TimeZoneId", "TimeZoneName");
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", ""));
        return ret;
    }

    protected DataTable _GetTimeZoneList()
    {
        var context = HttpContext.Current;
        var cache = context.Cache;
        string cacheKey = loginHandle + "_TimeZoneList_";
        int minutesToCache = 5;
        DataTable dtTimeZone = cache[cacheKey] as DataTable;
        if (dtTimeZone == null)
        {
            dtTimeZone = new DataTable();
            dtTimeZone.Columns.Add("TimeZoneId", typeof(string));
            dtTimeZone.Columns.Add("TimeZoneName", typeof(string));
            foreach (TimeZoneInfo tzinfo in TimeZoneInfo.GetSystemTimeZones())
            {
                DataRow dr = dtTimeZone.NewRow();
                dr["TimeZoneId"] = tzinfo.Id;
                dr["TimeZoneName"] = tzinfo.DisplayName;
                dtTimeZone.Rows.Add(dr);
            }

            cache.Add(cacheKey, dtTimeZone, new System.Web.Caching.CacheDependency(new string[] { }, new string[] { loginHandle })
                , System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minutesToCache, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
        }
        return dtTimeZone;
    }

    [WebMethod(EnableSession = false)]
    public ApiResponse<SerializableDictionary<string, string>, object> SwitchCurrent(int CompanyId, int ProjectId, short CultureId)
    {
        Func<ApiResponse<SerializableDictionary<string, string>, object>> fn = () =>
        {
            base.LCurr.CompanyId = CompanyId;
            base.LCurr.ProjectId = ProjectId;

            ValidateScope();

            ApiResponse<SerializableDictionary<string, string>, object> mr = new ApiResponse<SerializableDictionary<string, string>, object>();

            SerializableDictionary<string, string> current = new SerializableDictionary<string, string>();

            current.Add("CompanyId", CompanyId.ToString());
            current.Add("ProjectId", ProjectId.ToString());
            current.Add("CultureId", CultureId.ToString());
            current.Add("message", "Settings updated");

            mr.status = "success";
            mr.errorMsg = "";
            mr.data = current;

            return mr;
        };
        var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, "R", ""));
        return ret;
    }
}