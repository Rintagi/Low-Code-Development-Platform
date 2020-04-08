import { fetchService } from './fetchService';
import log from '../helpers/logger';
import { delay } from '../helpers/utils';
import { getCookie, setCookie, eraseCookie, parsedUrl } from '../helpers/domutils';
import { setupRuntime, getRintagiConfig, getFingerPrint, myMachine } from '../helpers/config';
import { LocalStorage, SessionStorage } from '../helpers/asyncStorage';
var sjcl = require('sjcl');
var pbkdf2 = require('pbkdf2');

var currentAccessScope = {};

export const authService = {
    login, logout, renewAccessToken, getToken, getAccessToken, getAccessControlInfo
    , isAuthenticated, getUsr, getMenu, getReactQuickMenu, getSystems, getServerIdentity, getRefreshToken, setAccessScope, getAccessScope, resetPwdEmail, resetPassword
};

const getMyMachine = getFingerPrint();
const rintagi = getRintagiConfig() || {};

// const baseUrl = 'http://fintruxdev/RC/';
//const baseUrl= '/rc/';
const runtimeConfig = rintagi;
const debuglog = runtimeConfig.debugAlert ? alert : log.debug;
const apiBasename = runtimeConfig.apiBasename;
const appDomainUrl = runtimeConfig.appDomainUrl || runtimeConfig.apiBasename;
const appNS = (runtimeConfig.appNS || (parsedUrl(appDomainUrl) || {}).pathname || '/').toUpperCase();
const baseUrl = apiBasename + "/webservices";
const fetchAPIResult = fetchService.fetchAPIResult;
const getAPIResult = fetchService.getAPIResult;

function getSystemId() {
    return runtimeConfig.systemId;
}

function makeNameFromNS(name) {
    return ((appNS.toUpperCase().replace(/^\//, '') + '_') || '') + name;
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

async function rememberUserHandle(userIdentity) {
    const h = sjcl.hash.sha256.hash(userIdentity);
    const handle = arrayBufferToBase64(wordArrayToByteArray(h, h.length)).replace(/=/g, '_');
    //localStorage.setItem(makeNameFromNS("user_handle"), handle);
    LocalStorage.setItem(makeNameFromNS("user_handle"), handle)
        .then(ret => {

        })
        .catch(error => {
            log.error(error);
        })
    setCookie(makeNameFromNS("tokenInCookieJS"), handle, null, appNS);
}
async function getTokenName(name) {
    const userHandle = await getUserHandle();
    const x = btoa(sjcl.hash.sha256.hash((appDomainUrl || "").toLowerCase() + name + (userHandle || "") + (myMachine || "")));
    return x;
}

async function eraseUserHandle() {
    //localStorage.removeItem(makeNameFromNS("user_handle"));
    LocalStorage.removeItem(makeNameFromNS("user_handle"));
    eraseCookie(makeNameFromNS("tokenInCookieJS"));
}
async function getUserHandle() {
    //const x = localStorage[makeNameFromNS("user_handle")] || getCookie(makeNameFromNS("tokenInCookieJS"));
    const x = await LocalStorage.getItem(makeNameFromNS("user_handle")) || getCookie(makeNameFromNS("tokenInCookieJS"));
    return x;
}

async function getLoginFromCookie() {
    try {
        var user_handle = getCookie(makeNameFromNS("tokenInCookieJS"));
        var token = getCookie(makeNameFromNS("tokenJS"));
        if (user_handle && token) {
            //localStorage.setItem(makeNameFromNS("user_handle"), user_handle);
            LocalStorage.setItem(makeNameFromNS("user_handle"), user_handle);
            var tokenName = await getTokenName("refresh_token");
            //localStorage.setItem(tokenName, JSON.stringify({ refresh_token: token }));
            LocalStorage.setItem(makeNameFromNS("user_handle"), user_handle);
            eraseCookie(makeNameFromNS("tokenInCookieJS"));
            eraseCookie(makeNameFromNS("tokenJS"));
        }
    } catch (e) {/**/ }

}

async function getAccessToken() {
    return getMyMachine
        .then(async () => {
            try {
                await getLoginFromCookie();
                const userHandle = await getUserHandle();
                if (!userHandle) return {};
                const tokenName = await getTokenName("access_token");
                const accessToken = await SessionStorage.getItem(tokenName) 
                                    ||
                                    await LocalStorage.getItem(tokenName)
                                    ||
                                    ((atob((getCookie(tokenName) || "").replace("_", '='))) && false)
                                    ;
                return JSON.parse(accessToken);
            }
            catch (e) {
                return {}
            }
        })
};

async function getRefreshToken() {
    try {
        const tokenName = await getTokenName("refresh_token");
        const refreshToken = await SessionStorage.getItem(tokenName) 
                            ||
                            await LocalStorage.getItem(tokenName)
                            ||
                            ((atob((getCookie(tokenName) || "").replace("_", '='))) && false)
                            ;
        var x = JSON.parse(refreshToken);
        return x;
    }
    catch (e) {
        log.error(e);
        const tokenInCookie = getCookie(makeNameFromNS("tokenJS"));
        if (tokenInCookie) {
            return {
                refresh_token: tokenInCookie,
            }
        }
        else
            return {}
    }
};

function setAccessScope(accessScope, replace) {
    currentAccessScope = {
        ...(replace ? {} : currentAccessScope),
        ...accessScope
    };
    return currentAccessScope;
};

function getAccessScope() { return currentAccessScope; };

const storeAccessToken = async (access_token, resources, scope, expires_in, isLogout) => {
    if (access_token) {
        const tokenString = JSON.stringify({ access_token: access_token, expires_in: expires_in });
        const tokenStringForCookie = btoa(tokenString).replace(/=/g, '_');
        const tokenName = await getTokenName("access_token");
        //setCookie(getTokenName("access_token"),tokenStringForCookie);
        //sessionStorage.setItem(getTokenName("access_token"), tokenString);
        SessionStorage.setItem(tokenName, tokenString);
        //localStorage.setItem(getTokenName("access_token"), tokenString);
        LocalStorage.setItem(tokenName, tokenString);
    }
    else {
        //eraseCookie(getTokenName("access_token"));
        if (isLogout) {
            console.log('logout clean up');
        }
        console.log('erase access token');
        const tokenName = await getTokenName("access_token");
        //sessionStorage.removeItem(getTokenName("access_token"));
        SessionStorage.removeItem(tokenName);
        //localStorage.removeItem(getTokenName("access_token"));
        LocalStorage.removeItem(tokenName);
    }
}

const storeRefreshToken = async (refresh_token, resources, isLogout) => {
    if (refresh_token) {
        const tokenString = JSON.stringify({ refresh_token: refresh_token });
        const tokenStringForCookie = btoa(tokenString).replace(/=/g, '_');
        const tokenName = await getTokenName("refresh_token");
        //setCookie(getTokenName("refresh_token"),tokenStringForCookie);
        //sessionStorage.setItem(getTokenName("refresh_token"), tokenString);
        SessionStorage.setItem(tokenName, tokenString);
        //localStorage.setItem(getTokenName("refresh_token"), tokenString);
        LocalStorage.setItem(tokenName, tokenString);
    }
    else {
        //eraseCookie(getTokenName("refresh_token"));
        if (isLogout) {
            console.log('logout clean up');
        }
        console.log('erase refresh token');
        const tokenName = await getTokenName("refresh_token");
        //sessionStorage.removeItem(getTokenName("refresh_token"));
        SessionStorage.removeItem(tokenName);
        //localStorage.removeItem(getTokenName("refresh_token"));
        LocalStorage.removeItem(tokenName);
    }
}

const eraseRefreshToken = async () => {
    //eraseCookie(getTokenName("refresh_token"));
    eraseCookie(makeNameFromNS("tokenInCookieJS"));
    eraseCookie(makeNameFromNS("tokenJS"));
    const tokenName = await getTokenName("refresh_token");
    //sessionStorage.removeItem(getTokenName("refresh_token"));
    SessionStorage.removeItem(tokenName);
    //localStorage.removeItem(getTokenName("refresh_token"));
    LocalStorage.removeItem(tokenName);
}

function getAccessControlInfo() {
    //    const access_token = getAccessToken().access_token;
    return {
        //        access_token: access_token, 
        getAccessToken: getAccessToken,
        renewAccessToken: renewAccessToken,
    }
};

function isAuthenticated() { return false && (getUserHandle() || false); };

function login(username, password, options = {}) {
    const { nonce, code_challenge_method, code_challenge, url, client_id, challenge_answered } = options;
    return fetchAPIResult(url || baseUrl + "/authWs.asmx/Login"
        , {
            requestOptions: {
                body: JSON.stringify({
                    client_id: client_id || null,
                    usrName: username || null,
                    password: password || null,
                    nonce: nonce || null,
                    code_challenge_method: code_challenge_method || "plain",
                    code_challenge: code_challenge || null
                })
            },
        })
        .then(result => {
            const apiResult = fetchService.getAPIResult(result);
            if (apiResult.accessCode || (apiResult.accessToken || {}).refresh_token) {
                rememberUserHandle(username);
                if ((apiResult.accessToken || {}).refresh_token) {
                    return renewAccessToken((apiResult.accessToken || {}).refresh_token)
                        .then(
                            accessToken => {
                                return {
                                    accessCode: apiResult.accessCode,
                                    refresh_token: accessToken.refresh_token,
                                    status: "success",
                                }
                            },
                            error => {
                                eraseUserHandle();
                                return Promise.reject(error);
                            }
                        )
                }
                else
                    return getToken(apiResult.accessCode)
                        .then(
                            accessToken => {
                                return {
                                    accessCode: apiResult.accessCode,
                                    status: "success",
                                }
                            },
                            error => {
                                eraseUserHandle();
                                return Promise.reject(error);
                            }
                        )
            }
            else {
                if (apiResult.error === "access_denied" && (apiResult.message === "Your email or password is incorrect" || apiResult.message === "bot detected") && !challenge_answered) {
                    let derivedKey = pbkdf2.pbkdf2Sync(apiResult.serverChallenge, apiResult.serverChallenge, apiResult.challengeCount, 32, 'sha1');
                    let challengeResult = btoa(String.fromCharCode.apply(null, new Uint8Array(derivedKey)));
                    return login(username, password, {
                        ...options,
                        client_id: challengeResult,
                        challenge_answered: true,
                    });
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? result.data.value.d.error : result.errType,
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            }
        },
            (error => {
                return Promise.reject(error);
            })
        )
}

let tokenRefreshPromise = null;

async function getToken(code, options = {}) {
    const { client_id, scope, code_verifier, redirect_url, client_secret, grant_type, re_auth } = options;
    if (!code) {
        return Promise.reject({
            status: "failed",
            errType: "authentication error",
            errSubType: "login required",
            // errMsg : "login session expired or invalid"
            errMsg: ""
        });
    }
    if (tokenRefreshPromise) {
        return tokenRefreshPromise;
    }

    const requestPromise = tokenRefreshPromise = fetchAPIResult(baseUrl + "/authWs.asmx/GetToken"
        , {
            requestOptions: {
                body: JSON.stringify({
                    client_id: client_id || "",
                    scope: scope || "",
                    grant_type: grant_type || "authorization_code",
                    code: code || "",
                    code_verifier: code_verifier || "",
                    redirect_url: redirect_url || "",
                    client_secret: client_secret || "",
                    re_auth: re_auth ? "Y" : "N",
                })
            }
        })
        .then(result => {
            if (result.status === "success" && result.data.value.d && result.data.value.d.access_token) {
                const access_token = result.data.value.d.access_token;
                const refresh_token = result.data.value.d.refresh_token;
                if (!access_token || !refresh_token) {
                    console.log('no token returned');
                    console.log(result);
                }
                else {
                    storeAccessToken(result.data.value.d.access_token, result.data.value.d.resources, result.data.value.d.scope, result.data.value.d.expires_in);
                    storeRefreshToken(result.data.value.d.refresh_token, result.data.value.d.resources);    
                }
                return {
                    access_token: result.data.value.d.access_token,
                    refresh_token: result.data.value.d.refresh_token,
                    token_type: result.data.value.d.token_type,
                    expires_in: result.data.value.d.expires_in,
                    status: "success"
                }
            }
            else {
                return Promise.reject({
                    status: "failed",
                    errType: result.status === "success" ? result.data.value.d.error : result.errType,
                    errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                })
            }
        },
            (error => {
                return Promise.reject(error);
            })
        )
        .finally(() => {
            tokenRefreshPromise = null;
        });
    return requestPromise;
}

async function logout(keepToken = false, currentSessionOnly = false) {
    return getAccessToken().then(
        async (token) => {
            console.log('logout successful');
            var refresh_token = (await getRefreshToken() || {}).refresh_token;
            if (!keepToken) {
                storeAccessToken(null, null, null, true);
            }
            if (!keepToken) {
                eraseRefreshToken();
                eraseUserHandle();
            }
            if (!currentSessionOnly) {
                return fetchAPIResult(baseUrl + "/authWs.asmx/Logout"
                    , {
                        requestOptions: {
                            body: JSON.stringify({
                                access_token: token.access_token || "",
                                refresh_token: refresh_token || "",
                            })
                        },
                    }).then(result => result).catch(error => Promise.reject(error));
            }
            return true;
        }
    )

}
async function renewAccessToken(refresh_token, re_auth) {
    return getToken(refresh_token || (await getRefreshToken() || {}).refresh_token, { grant_type: "refresh_token", re_auth })

        .catch(error => {
            if (!refresh_token) {
                if (error.errType !== "network error" 
                    && error.errorMsg !== "Failed to fetch" 
                    && error.errType !== "fetch error" 
                    ) {
                    console.log('failed to renew access token, flushing tokens');
                    console.log(error);
                    storeRefreshToken(null);
                    storeAccessToken(null);    
                } else {
                    console.log('failed to renew access token, network error, keep token');
                }
            }
            return Promise.reject(error);
        }
        );
}

async function getUsr(scope) {
    return fetchAPIResult(baseUrl + "/authWs.asmx/GetCurrentUsrInfo"
        , {
            requestOptions: {
                body: JSON.stringify({
                    scope: scope || "",
                })
            },
            ...(getAccessControlInfo())
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                        supportingData: getAPIResult(result).supportingData || {}
                    }
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )

}

async function resetPwdEmail(emailAddress, reCaptchaRequest, refCode) {
    return fetchAPIResult(baseUrl + "/AuthWs.asmx/ResetPwdEmail"
        , {
            requestOptions: {
                body: JSON.stringify({
                    emailAddress: emailAddress || "",
                    reCaptchaRequest: reCaptchaRequest || "",
                    refCode: refCode || "",
                })
            }
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                    }
                }
                else {
                    console.log(result);
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || result.data.value.d.error,
                        errMsg: result.status === "success" ? result.data.value.d.message || result.data.value.d.errorMsg : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )

}


function resetPassword(emailAddress, password, nounce, ticketLeft, ticketRight) {
    return fetchAPIResult(baseUrl + "/AuthWs.asmx/ResetPassword"
        , {
            requestOptions: {
                body: JSON.stringify({
                    emailAddress: emailAddress || "",
                    password: password || "",
                    nounce: nounce || "",
                    ticketLeft: ticketLeft || "",
                    ticketRight: ticketRight || "",
                })
            }
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                    }
                }
                else {
                    console.log(result);
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || result.data.value.d.error,
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )
}

async function getMenu(scope) {
    return fetchAPIResult(baseUrl + "/authWs.asmx/GetMenu"
        , {
            requestOptions: {
                body: JSON.stringify({
                    scope: scope || "",
                    systemId: getSystemId() || 5,

                })
            },
            ...(getAccessControlInfo())
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                        supportingData: getAPIResult(result).supportingData || {}
                    }
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )
}

async function getReactQuickMenu(systemId, scope) {
    return fetchAPIResult(baseUrl + "/authWs.asmx/GetReactQuickMenu"
        , {
            requestOptions: {
                body: JSON.stringify({
                    scope: scope || "",
                    systemId: systemId || getSystemId() || 5,

                })
            },
            ...(getAccessControlInfo())
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                        supportingData: getAPIResult(result).supportingData || {}
                    }
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )
}

async function getSystems(ignoreCache, scope) {
    return fetchAPIResult(baseUrl + "/authWs.asmx/GetSystems"
        , {
            requestOptions: {
                body: JSON.stringify({
                    scope: scope || "",
                    ignoreCache: ignoreCache || false,
                })
            },
            ...(getAccessControlInfo())
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                        supportingData: getAPIResult(result).supportingData || {}
                    }
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )
}

async function getServerIdentity(scope) {
    return fetchAPIResult(baseUrl + "/authWs.asmx/GetServerIdentity"
        , {
            requestOptions: {
                body: JSON.stringify({
                    scope: scope || ""
                })
            },
            ...(getAccessControlInfo())
        })
        .then(
            async result => {
                if (result.status === "success" && result.data.value.d && result.data.value.d.status === "success") {
                    return {
                        data: getAPIResult(result).data,
                        supportingData: getAPIResult(result).supportingData || {}
                    }
                }
                else {
                    return Promise.reject({
                        status: "failed",
                        errType: result.status === "success" ? "api call error" : result.errType,
                        errSubType: result.errSubType || (result.status === "success" ? result.data.value.d.error : null),
                        errMsg: result.status === "success" ? result.data.value.d.message : result.errType
                    })
                }
            },
            error => {
                return Promise.reject(error);
            }
        )
}