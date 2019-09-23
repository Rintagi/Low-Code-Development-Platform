import {fetchData,getAccessControlInfo, getAccessScope, baseUrl} from './webAPIBase';
import log from '../helpers/logger';

export function updUsrPwd(j, p, newPwd, confirmPwd){
    return fetchData(baseUrl+'/ProfileWs.asmx/UpdUsrPwd'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    j: j,
                    p: p,
                    NewUsrPassword: newPwd,
                    ConfirmPwd: confirmPwd,
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}

export function getProfileInfo(newPwd, confirmPwd){
    return fetchData(baseUrl+'/ProfileWs.asmx/GetProfileInfo'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}

export function updateProfile(newLoginName, newUsrName, newUsrEmail){
    return fetchData(baseUrl+'/ProfileWs.asmx/UpdateProfile'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    NewLoginName : newLoginName, 
                    NewUsrName: newUsrName,
                    NewUsrEmail: newUsrEmail
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}

export function resetPwd(resetLoginName, resetUsrEmail){
    return fetchData(baseUrl+'/ProfileWs.asmx/ResetPwd'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    ResetLoginName: resetLoginName,
                    ResetUsrEmail: resetUsrEmail,
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}