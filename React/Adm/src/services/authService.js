import { fetchService} from './fetchService';
var pbkdf2 = require('pbkdf2');
var currentAccessScope = {};

export const authService = {
    login, logout, renewAccessToken, getToken, getAccessToken, getAccessControlInfo, 
    isAuthenticated,getUsr, getMenu, getRefreshToken,setAccessScope,getAccessScope
};

// const baseUrl = 'http://fintruxdev/RC/';
//const baseUrl= '/rc/';
const baseUrl = (document.Rintagi || {}).apiBasename + "/webservices";
const fetchAPIResult = fetchService.fetchAPIResult;
const getAPIResult = fetchService.getAPIResult;
function getSystemId(){
    return (document.Rintagi || {}).systemId;
}
function getAccessToken(){ try { return JSON.parse(localStorage["access_token"]) } catch(e) { return {}}};
function getRefreshToken(){ try { return JSON.parse(localStorage["refresh_token"]) } catch(e) { return {}}};
function setAccessScope(accessScope, replace) { 
    currentAccessScope = { 
    ...(replace ? {} : currentAccessScope), 
    ...accessScope }; 
    return currentAccessScope; };
function getAccessScope() {return currentAccessScope;};
const storeAccessToken = (access_token, resources, scope, expires_in)=> {
    if (access_token) {
        localStorage.setItem("access_token",  JSON.stringify({access_token:access_token,  expires_in:expires_in}));
    }
    else {
        localStorage.removeItem("access_token"); 
    }
}
const storeRefreshToken = (refresh_token,resources)=> {
    if (refresh_token)
        localStorage.setItem("refresh_token", JSON.stringify({refresh_token:refresh_token}));
    else localStorage.removeItem("refresh_token");
}
function getAccessControlInfo() { 
    return { 
        access_token: getAccessToken().access_token, 
        renewAccessToken: renewAccessToken,
    }
};

function isAuthenticated() {return false && (getAccessToken().access_token || false);};

function login(username, password, options = {}) {
    const {nonce, code_challenge_method, code_challenge, url, client_id, challenge_answered}= options;
    return fetchAPIResult(url || baseUrl+"/authWs.asmx/Login"
    ,{
        requestOptions : {
            body:JSON.stringify({
                client_id:client_id || null,
                usrName:username || null,
                password:password || null,
                nonce:nonce || null,
                code_challenge_method:code_challenge_method || "plain",
                code_challenge:code_challenge || null     
                })
        },   
    })
    .then(result=>{    
        const apiResult = fetchService.getAPIResult(result);
        if (apiResult.accessCode) {
            return getToken(apiResult.accessCode)
                .then(
                accessToken=>{
                    return {
                        accessCode : apiResult.accessCode,
                        status: "success",
                    }
                },
                error=>{
                    return Promise.reject(error);
                }
                )
        }
        else {
            if (apiResult.error === "access_denied" && (apiResult.message === "bot challenge" || apiResult.message === "bot detected") && !challenge_answered) {
                let derivedKey = pbkdf2.pbkdf2Sync(apiResult.serverChallenge, apiResult.serverChallenge, apiResult.challengeCount, 32, 'sha1');
                let challengeResult = btoa(String.fromCharCode.apply(null, new Uint8Array(derivedKey)));
                return login(username,password,{
                    ...options,
                    client_id: challengeResult,
                    challenge_answered: true,
                });
            }
            else {
                return Promise.reject( {
                    status : "failed",
                    errType: result.status === "success" ? result.data.value.d.error : result.errType,
                    errMsg : result.status === "success" ? result.data.value.d.message : result.errType
                })
            }
        }
    },
    (error=>{
        return Promise.reject(error);
    })
    )
}
function getToken (code,options={}) {
    const {client_id,scope,code_verifier,redirect_url,client_secret,grant_type} = options;
    if (!code) 
        return Promise.reject( {
        status : "failed",
        errType: "authentication error",
        errSubType: "login required",
        errMsg : "login session expired or invalid"
    });

    return fetchAPIResult(baseUrl+"/authWs.asmx/GetToken"
    ,{
        requestOptions : {
            body:JSON.stringify({
                client_id:client_id || "",
                scope:scope || "",
                grant_type:grant_type || "authorization_code",
                code:code || "",
                code_verifier:code_verifier || "",
                redirect_url:redirect_url || "",
                client_secret:client_secret || "", 
                })
        }
    })
    .then(result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.access_token) {
            storeAccessToken(result.data.value.d.access_token,result.data.value.d.resources,result.data.value.d.scope, result.data.value.d.expires_in);
            storeRefreshToken(result.data.value.d.refresh_token,result.data.value.d.resources);
            return {
                access_token : result.data.value.d.access_token,
                refresh_token : result.data.value.d.refresh_token,
                token_type: result.data.value.d.token_type,
                expires_in: result.data.value.d.expires_in,
                status: "success"
            }
        }
        else {
            return Promise.reject( {
                status : "failed",
                errType: result.status === "success" ? result.data.value.d.error : result.errType,
                errMsg : result.status === "success" ? result.data.value.d.message : result.errType
            })
    }
    },
    (error=>{
        return Promise.reject(error);
    })
    )
}
async function logout(currentSessionOnly = false) {
    var access_token = (getAccessToken() || {}).access_token;
    var refresh_token = (getRefreshToken() || {}).refresh_token; 
    storeAccessToken(null);
    if (!currentSessionOnly) {
        storeRefreshToken(null);
        return fetchAPIResult(baseUrl+"/authWs.asmx/Logout"
        ,{
            requestOptions: {
                body:JSON.stringify({
                    access_token:access_token || "",
                    refresh_token:refresh_token || "",
                })
            },
        }).then(result=>result).catch(error=>Promise.reject(error));      
    }
    return true;
}
async function renewAccessToken (refresh_token) {
    return getToken(refresh_token || (getRefreshToken() || {}).refresh_token,{grant_type:"refresh_token"})

           .catch(error=>{
               if (!refresh_token) {
                   storeRefreshToken(null);
                   storeAccessToken(null);
                }
               return Promise.reject(error);
           }
            );
}

async function getUsr(scope) {
    return fetchAPIResult(baseUrl+"/authWs.asmx/GetCurrentUsrInfo"
    ,{
        requestOptions: {
            body:JSON.stringify({
                scope:scope || "",
            })
        },
        ...(getAccessControlInfo())
    })
    .then(
    async result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.status==="success") {
            return {
                data: getAPIResult(result).data,
                supportingData: getAPIResult(result).supportingData || {}
            }
        }
        else {
            return Promise.reject( {
                status : "failed",
                errType: result.status === "success" ? "api call error" : result.errType,
                errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                errMsg : result.status === "success" ? result.data.value.d.message : result.errType
            })
        }
    },
    error=>{
        return Promise.reject(error);
    }
    )

}

async function getMenu(scope) {
    return fetchAPIResult(baseUrl+"/authWs.asmx/GetMenu"
    ,{
        requestOptions: {
            body:JSON.stringify({
                scope: ("" + getSystemId()) || "",
                
            })
        },
        ...(getAccessControlInfo())
    })
    .then(
    async result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.status==="success") {
            return {
                data: getAPIResult(result).data,
                supportingData: getAPIResult(result).supportingData || {}
            }
        }
        else {
            return Promise.reject( {
                status : "failed",
                errType: result.status === "success" ? "api call error" : result.errType,
                errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                errMsg : result.status === "success" ? result.data.value.d.message : result.errType
            })
        }
    },
    error=>{
        return Promise.reject(error);
    }
    )

}