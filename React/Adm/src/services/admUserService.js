import { fetchService } from './fetchService';
import { authService } from './authService';

export const admUserService = {
    getUsrDtl,getUsrDtlMulti
};

const baseUrl = (document.Rintagi || {}).apiBasename + "/webservices";
const fetchAPIResult = fetchService.fetchAPIResult;
const getAPIResult = fetchService.getAPIResult;
const getAccessControlInfo = authService.getAccessControlInfo;

async function getUsrDtl(usrId,withSupportingData) {
    return fetchAPIResult(baseUrl+"/admUserWs.asmx/GetUsrDtl"
    ,{
        requestOptions: {
            body:JSON.stringify({
                usrId:usrId || "",
                bWithSupportedData: withSupportingData || false
            })
        },
        ...(getAccessControlInfo())
    })
    .then(
    async result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
            return {
                data: getAPIResult(result).data,
                supportingData: getAPIResult(result).supportingData || {}
            }
        }
        else {
            return Promise.reject( {
                status : "failed",
                errType: result.status === "success" ? "api call error" : result.errType,
                errSubType: result.errSubType || result.data.value.d.error,
                errMsg : result.status === "success" ? result.data.value.d.message : result.errType
            })
        }
    },
    error=>{
        return Promise.reject(error);
    }
    )

}

function getUsrDtlMulti(usrId) {

    var usrDtl =  fetchAPIResult(baseUrl+"/admUserWs.asmx/GetUsrById"
    ,{
        requestOptions: {
            body:JSON.stringify({
                usrId:usrId || "",
            })
        },
        ...(authService.getAccessControlInfo())
    })
    var usrGrpLsList =  fetchAPIResult(baseUrl+"/admUserWs.asmx/GetDdlUsrGroupLs"
    ,{
        requestOptions: {
            body:""
        },
        ...(authService.getAccessControlInfo())
    })
    var cultureIdList =  fetchAPIResult(baseUrl+"/admUserWs.asmx/GetDdlCultureId"
    ,{
        requestOptions: {
            body:""
        },
        ...(getAccessControlInfo())
    })

    return Promise.all([usrDtl,usrGrpLsList,cultureIdList])
            .then(
                ([usr,grp,culture])=>{
                    var x = getAPIResult(usr);
                    var y = getAPIResult(grp);
                    var z = getAPIResult(culture);
                    if (x.status === "failed") return Promise.reject(x.errorMsg);
                    if (y.status === "failed") return Promise.reject(y.errorMsg);
                    if (z.status === "failed") return Promise.reject(z.errorMsg);
                    return {usr:x.data, grp:y.data, culture:z.data};
                }
                ,error=>{
                    return Promise.reject(error)
                }
            )
}