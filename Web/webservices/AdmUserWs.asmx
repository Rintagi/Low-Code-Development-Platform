<%@ WebService Language="C#" Class="AdmUserWs" %>

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
public partial class AdmUserWs : AsmxBase
{
    const int screenId = 2;
    const byte systemId = 3;
    const string programName = "AdmUsr1";
        
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
    public AdmUserWs()
        : base()
    {
    }

    private IEnumerable<SerializableDictionary<string, string>> _GetDdlUsrGroupLs()
    {
        DataTable dt = (new AdminSystem()).GetDdl(1, "GetDdlUsrGroupLs3S1741", false, true, 0, "", LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);
        var ret = from x in dt.AsEnumerable() select new SerializableDictionary<string, string>() { { "UsrGroupLs1", x.Field<Int16?>("UsrGroupLs1").ToString() }, { "UsrGroupLs1Text", x.Field<string>("UsrGroupLs1Text") } };
        return ret;   
    }

    private IEnumerable<SerializableDictionary<string, string>> _GetDdlCultureId()
    {
        DataTable dt = (new AdminSystem()).GetDdl(1,"GetDdlCultureId3S1746",false,true,0,"",LcAppConnString,LcAppPw,string.Empty,base.LImpr,base.LCurr);
        var ret = from x in dt.AsEnumerable() select new SerializableDictionary<string, string>() { { "CultureId1", x.Field<Int16?>("CultureId1").ToString() }, { "CultureId1Text", x.Field<string>("CultureId1Text") } };
        return ret;   
    }

    private IEnumerable<SerializableDictionary<string, string>> _GetAdmUsrById(string usrId)
    {
        DataTable dt = (new AdminSystem()).GetMstById("GetAdmUsr1ById", usrId, null, null);
        var columns = dt.Columns;
        var ret = dt.AsEnumerable().Select(dr=>{
                        SerializableDictionary<string, string> data = new SerializableDictionary<string, string>();
                        foreach (DataColumn c in dt.Columns)
                        {
                            data[c.ColumnName] = dr[c.ColumnName].ToString();

                        }
                        return data;            
                    }); 
        return ret;
    }
    
    [WebMethod(EnableSession = true)]
    public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetDdlCultureId()
    {
        Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = _GetDdlCultureId().ToList();
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;
        
    }

    [WebMethod(EnableSession = true)]
    public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetDdlUsrGroupLs()
    {
        Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = _GetDdlUsrGroupLs().ToList();
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;

    }
    [WebMethod(EnableSession = true)]
    public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetUsrById(string usrId)
    {
        Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            mr.data = _GetAdmUsrById(usrId).ToList();
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;

    }                   
                       
    [WebMethod(EnableSession = true)]
    public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetUsrDtl(string usrId, bool bWithSupportedData = false)
    {
        Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
        {
            SwitchContext(LCurr.SystemId, LCurr.CompanyId, LCurr.ProjectId);
            var groupLsList = Task.Run(() => bWithSupportedData ? _GetDdlUsrGroupLs() : new List<SerializableDictionary<string, string>>());
            var cultureIdList = Task.Run(() => bWithSupportedData ? _GetDdlCultureId() : new List<SerializableDictionary<string, string>>());
            var usrInfo = Task.Run(() => _GetAdmUsrById(usrId));
            ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
            mr.status = "success";
            mr.errorMsg = "";
            try
            {
                Task.WaitAll(new Task[] { usrInfo, groupLsList, cultureIdList }, 1000);
                mr.data = usrInfo.Result.ToList();
                mr.supportingData = new SerializableDictionary<string, AutoCompleteResponse>{
                    { "groupLsList", new AutoCompleteResponse(){ total = 0, topN=0, data=groupLsList.Result.ToList() } }, 
                    { "cultureIdList",new AutoCompleteResponse(){ total = 0, topN=0, data=cultureIdList.Result.ToList() }}
                };
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
            return mr;
        };
        var result = ProtectedCall(fn);
        return result;
    }
    
}