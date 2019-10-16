import {fetchService} from './fetchService';
import log from '../helpers/logger';
import {delay} from '../helpers/utils';
import {setupRuntime} from '../helpers/utils';

var sjcl = require('sjcl');
var Fingerprint2 = require('fingerprintjs2');
var pbkdf2 = require('pbkdf2');
var currentAccessScope = {};

export const authService = {
    login, logout, renewAccessToken, getToken, getAccessToken, getAccessControlInfo
    , isAuthenticated,getUsr, getMenu, getRefreshToken,setAccessScope,getAccessScope, resetPwdEmail, resetPassword
};

let myMachine = null;
function getFingerPrint() {
    return new Promise(function (resolve, reject) {
        setTimeout(() => {
            Fingerprint2.get(function (components) {
                components.sort();
                log.debug(components);
                const a = JSON.stringify(components);
                const sha256 = new sjcl.hash.sha256();
                sha256.update(a);
                const h = btoa(sha256.finalize());
                myMachine = h;
                resolve(h);
              })  
        }, 500);
    });
};

const getMyMachine = getFingerPrint();

function setCookie(name,value,days, path) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days*24*60*60*1000));
        expires = "; expires=" + date.toUTCString();
    }  
    const href = window.location.href;
    document.cookie = name + "=" + (value || "")  + expires + "; path=" + (path || "/") +";secure";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}
function eraseCookie(name) {   
    document.cookie = name+'=; Max-Age=-99999999;';  
}

function parsedUrl(url) {
    var parser = document.createElement("a");
    parser.href = url;
    var o = {};
    // IE 8 and 9 dont load the attributes "protocol" and "host" in case the source URL
    // is just a pathname, that is, "/example" and not "http://domain.com/example".
    parser.href = parser.href;

    // IE 7 and 6 wont load "protocol" and "host" even with the above workaround,
    // so we take the protocol/host from window.location and place them manually
    if (parser.host === "") {
        var newProtocolAndHost = window.location.protocol + "//" + window.location.host;
        if (url.charAt(1) === "/") {
            parser.href = newProtocolAndHost + url;
        } else {
            // the regex gets everything up to the last "/"
            // /path/takesEverythingUpToAndIncludingTheLastForwardSlash/thisIsIgnored
            // "/" is inserted before because IE takes it of from pathname
            var currentFolder = ("/" + parser.pathname).match(/.*\//)[0];
            parser.href = newProtocolAndHost + currentFolder + url;
        }
    }

    // copies all the properties to this object
    var properties = ['host', 'hostname', 'hash', 'href', 'port', 'protocol', 'search'];
    for (var i = 0, n = properties.length; i < n; i++) {
        o[properties[i]] = parser[properties[i]];
    }

    // pathname is special because IE takes the "/" of the starting of pathname
    o.pathname = (parser.pathname.charAt(0) !== "/" ? "/" : "") + parser.pathname;
    return o;
}

// const baseUrl = 'http://fintruxdev/RC/';
//const baseUrl= '/rc/';
const runtimeConfig = (document.Rintagi || {});
const debuglog = runtimeConfig.debugAlert ? alert : log.debug;
const apiBasename = runtimeConfig.apiBasename;
const appDomainUrl = runtimeConfig.appDomainUrl || runtimeConfig.apiBasename;
const appNS = runtimeConfig.appNS || (parsedUrl(appDomainUrl) || {}).pathname || '/';
const baseUrl = apiBasename + "/webservices";
const fetchAPIResult = fetchService.fetchAPIResult;
const getAPIResult = fetchService.getAPIResult;

function getSystemId(){
    return (document.Rintagi || {}).systemId;
}

function makeNameFromNS(name) {
    return ((appNS.toUpperCase().replace(/^\//,'') + '_') || '') + name;
}

function wordToByteArray(word, length) {
    var ba = [], i, xFF = 0xFF;
    if (length > 0)
        ba.push(word >>> 24);
    if (length > 1)
        ba.push((word >>> 16) & xFF);
    if (length > 2)
        ba.push((word >>> 8) & xFF);
    if (length > 3)
        ba.push(word & xFF);
    return ba;
}

function wordArrayToByteArray(wordArray, length) {
    if (wordArray.hasOwnProperty("sigBytes") && wordArray.hasOwnProperty("words")) {
        length = wordArray.sigBytes;
        wordArray = wordArray.words;
    }

    var result = [],
        bytes,
        i = 0;
    while (length > 0) {
        bytes = wordToByteArray(wordArray[i], Math.min(4, length));
        length -= bytes.length;
        result.push(bytes);
        i++;
    }
    return [].concat.apply([], result);
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return btoa(binary);
}

function rememberUserHandle(userIdentity) {
    const h = sjcl.hash.sha256.hash(userIdentity);
    const handle = arrayBufferToBase64(wordArrayToByteArray(h,h.length)).replace(/=/g,'_');
    localStorage.setItem(makeNameFromNS("user_handle"), handle); 
    setCookie(makeNameFromNS("tokenInCookieJS"),handle,null,appNS);
}
function getTokenName(name) {
    const x = btoa(sjcl.hash.sha256.hash((appDomainUrl || "").toLowerCase() + name + (getUserHandle() || "")+(myMachine||"")));
    return x;
}
function eraseUserHandle() {
    localStorage.removeItem(makeNameFromNS("user_handle"));
    eraseCookie(makeNameFromNS("tokenInCookieJS"));
}
function getUserHandle(){
    const x = localStorage[makeNameFromNS("user_handle")] || getCookie(makeNameFromNS("tokenInCookieJS"));
    return x;
}

function getLoginFromCookie()
{
    try {
        var user_handle = getCookie(makeNameFromNS(appDomainUrl,"tokenInCookieJS"));
        var token = getCookie(makeNameFromNS(appDomainUrl,"tokenJS"));
        if (user_handle && token) {
            localStorage.setItem(makeNameFromNS(appDomainUrl, "user_handle"), user_handle);
            var tokenName = getTokenName(appDomainUrl, "refresh_token");
            localStorage.setItem(tokenName, JSON.stringify({ refresh_token: token }));
            eraseCookie(makeNameFromNS(appDomainUrl,"tokenInCookieJS"));
            eraseCookie(makeNameFromNS(appDomainUrl,"tokenJS"));
        }
    } catch (e) {/**/}

}

function getAccessToken(){ 
    return getMyMachine
        .then(()=>{
        try { 
            getLoginFromCookie();
            if (!getUserHandle()) return {};
            return JSON.parse(
                sessionStorage[getTokenName("access_token")] || 
                localStorage[getTokenName("access_token")] || 
                ((atob((getCookie(getTokenName("access_token")) || "").replace("_",'='))) && false))
            } 
        catch(e) { 
            return {}
        }
    })
};
function getRefreshToken(){ 
    try { 
        var x = JSON.parse(
            sessionStorage[getTokenName("refresh_token")] || 
            localStorage[getTokenName("refresh_token")] || 
            ((atob((getCookie(getTokenName("refresh_token")) || "").replace("_",'='))) && false)
            );

        return x;
        } 
    catch(e) { 
        const tokenInCookie = getCookie(makeNameFromNS("tokenInCookieJS"));
        if (tokenInCookie) {
            return {
                refresh_token:tokenInCookie,
            }
        }
        else
            return {}
    }
};
function setAccessScope(accessScope, replace) { 
    currentAccessScope = { 
    ...(replace ? {} : currentAccessScope), 
    ...accessScope }; 
    return currentAccessScope; };
function getAccessScope() {return currentAccessScope;};
const storeAccessToken = (access_token, resources, scope, expires_in)=> {
    if (access_token) {
        const tokenString = JSON.stringify({access_token:access_token,  expires_in:expires_in});
        const tokenStringForCookie = btoa(tokenString).replace(/=/g,'_');
        //setCookie(getTokenName("access_token"),tokenStringForCookie);
        sessionStorage.setItem(getTokenName("access_token"),  tokenString);
        localStorage.setItem(getTokenName("access_token"),  tokenString);
    }
    else {
        //eraseCookie(getTokenName("access_token"));
        sessionStorage.removeItem(getTokenName("access_token")); 
        localStorage.removeItem(getTokenName("access_token")); 
    }
}
const storeRefreshToken = (refresh_token,resources)=> {
    if (refresh_token) {
        const tokenString = JSON.stringify({refresh_token:refresh_token});
        const tokenStringForCookie = btoa(tokenString).replace(/=/g,'_');
        //setCookie(getTokenName("refresh_token"),tokenStringForCookie);
        sessionStorage.setItem(getTokenName("refresh_token"),tokenString );
        localStorage.setItem(getTokenName("refresh_token"),tokenString );
    }
    else {
        //eraseCookie(getTokenName("refresh_token"));
        sessionStorage.removeItem(getTokenName("refresh_token"));
        localStorage.removeItem(getTokenName("refresh_token"));
    }
}

const eraseRefreshToken = ()=>{
    //eraseCookie(getTokenName("refresh_token"));
    sessionStorage.removeItem(getTokenName("refresh_token"));
    localStorage.removeItem(getTokenName("refresh_token"));    
}

function getAccessControlInfo() { 
//    const access_token = getAccessToken().access_token;
    return { 
//        access_token: access_token, 
        getAccessToken: getAccessToken,
        renewAccessToken: renewAccessToken,
    }
};

function isAuthenticated() {return false && (getUserHandle() || false);};

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
            rememberUserHandle(username);
            return getToken(apiResult.accessCode)
                .then(
                accessToken=>{
                    return {
                        accessCode : apiResult.accessCode,
                        status: "success",
                    }
                },
                error=>{
                    eraseUserHandle();
                    return Promise.reject(error);
                }
                )
        }
        else {
            if (apiResult.error === "access_denied" && (apiResult.message === "Your email or password is incorrect" || apiResult.message === "bot detected") && !challenge_answered) {
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
let tokenRefreshPromise = null;

async function getToken (code,options={}) {
    const {client_id,scope,code_verifier,redirect_url,client_secret,grant_type} = options;
    if (!code) {
        return Promise.reject( {
        status : "failed",
        errType: "authentication error",
        errSubType: "login required",
        // errMsg : "login session expired or invalid"
        errMsg : ""
        });
    }
    if (tokenRefreshPromise) {
        return tokenRefreshPromise;
    }

    const requestPromise = tokenRefreshPromise = fetchAPIResult(baseUrl+"/authWs.asmx/GetToken"
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
    .finally(()=>{
        tokenRefreshPromise = null;
    });
    return requestPromise;
}

async function logout(keepToken = false, currentSessionOnly = false) {
    return getAccessToken().then(
        token=>{
            var refresh_token = (getRefreshToken() || {}).refresh_token; 
            if (!keepToken) {
                storeAccessToken(null);
            }
            if (!keepToken) {
                eraseRefreshToken();
                eraseUserHandle();
            }
            if (!currentSessionOnly) {
                return fetchAPIResult(baseUrl+"/authWs.asmx/Logout"
                ,{
                    requestOptions: {
                        body:JSON.stringify({
                            access_token:token.access_token || "",
                            refresh_token:refresh_token || "",
                        })
                    },
                }).then(result=>result).catch(error=>Promise.reject(error));      
            }
            return true;
        }
    )

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

async function resetPwdEmail(emailAddress, reCaptchaRequest, refCode) {
    return fetchAPIResult(baseUrl+"/AuthWs.asmx/ResetPwdEmail"
    ,{
        requestOptions: {
            body:JSON.stringify({
                emailAddress:emailAddress || "",
                reCaptchaRequest: reCaptchaRequest || "",
                refCode:refCode || "",
            })
        }
    })
    .then(
    async result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
            return {
                data: getAPIResult(result).data,
            }
        }
        else {
            console.log(result);
            return Promise.reject( {
                status : "failed",
                errType: result.status === "success" ? "api call error" : result.errType,
                errSubType: result.errSubType || result.data.value.d.error,
                errMsg : result.status === "success" ? result.data.value.d.message || result.data.value.d.errorMsg : result.errType
            })
        }
    },
    error=>{
        return Promise.reject(error);
    }
    )

}


function resetPassword(emailAddress, password, nounce, ticketLeft, ticketRight) {
    return fetchAPIResult(baseUrl+"/AuthWs.asmx/ResetPassword"
    ,{
        requestOptions: {
            body:JSON.stringify({
                emailAddress:emailAddress || "",
                password: password || "",
                nounce: nounce || "",
                ticketLeft:ticketLeft || "",
                ticketRight: ticketRight || "",
            })
        }
    })
    .then(
    async result=>{
        if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
            return {
                data: getAPIResult(result).data,
            }
        }
        else {
            console.log(result);
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

async function getMenu(scope) {
    return fetchAPIResult(baseUrl+"/authWs.asmx/GetMenu"
    ,{
        requestOptions: {
            body:JSON.stringify({
                scope:scope || "",
                systemId: getSystemId() || 5,
                
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