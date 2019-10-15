import log from '../helpers/logger';

export const fetchService = {
    fetchAPIResult,tryParseJSON,getAPIResult
};

function tryParseJSON(content) {
    try { 
        return {
            value : JSON.parse(content),
            err: null
        }
    } 
    catch (e) {
        return {
            value : content,
            err: e
        }
    }
};    

function getAPIResult(result) {
    try {
        return result.data.value.d;
    }
    catch(e) {
        return {};
    }
}

async function promiseWithTimeout(somePromise,timeoutInMS=0)
{
    if (timeoutInMS<=0) 
        return somePromise;
    else {
        const timer = new Promise((resolve,reject)=>{
            let wait = setTimeout(
                ()=>{
                    clearTimeout(wait);
                    reject({
                        localTimeout:true,
                    })
                }
                ,timeoutInMS);
        })
        return Promise.race([somePromise,timer]);
    }
}
async function fetchAPIResult(url, options={headers:{},requestOptions:{}}) {
    const {access_token, getAccessToken, renewAccessToken, tokenRenewed,timeout, SystemId, CompanyId, ProjectId, CultureId} = options;
    const scope = '' + (SystemId > 0 ? SystemId : '') + ',' + (typeof CompanyId === "undefined" ? '' : CompanyId)  + ',' + (typeof ProjectId === "undefined" ? '' : ProjectId)  + ',' + (typeof CultureId === "undefined" ? '' : CultureId);
    const fetchPromise = 
            (getAccessToken ? getAccessToken() : Promise.resolve({}))
            .then(token=>{
                const requestOptions = {
                    method: 'post',
                    mode: 'cors',
                    //credentials: 'include',
                    //credentials: 'same-origin',
                    headers: { 
                    'Content-Type': 'application/json', 
                    'Accept': 'application/json',
                    'Authorization': 'Bearer ' +  (token.access_token || ""),
                    'X-Authorization': 'Bearer ' +  (token.access_token || ""),
                    ...(options.headers),
                    'X-RintagiScope': scope
                    },
                    ...(options.requestOptions)
                    };   
                return fetch(url,requestOptions);
            })
            .then(response=>{
//                JSON.parse('abc{');
                let ret = response.text();
                return ret.then(bodyText=>{
                    let parsedRet = tryParseJSON(bodyText);
                    if (response.ok && parsedRet.status !== "access_denied") {
                        return {
                            data: parsedRet,
                            status: "success",
                            errMsg: null,
                            errType: null
                        }
                    } else {
                        if ((response.status === 401 || response.status === 403 || parsedRet.status === "access_denied") && renewAccessToken && !tokenRenewed) {
                            return renewAccessToken()
                                .then(
                                    newToken=>{
                                    return fetchAPIResult(url, {
                                        ...(options),
                                        access_token:newToken.access_token,
                                        tokenRenewed:true
                                    }) 
                                }
/*                                 , error => {
                                    return Promise.reject(error);
                                } */
                                )
                                .catch(error=>{
                                    return Promise.reject(error)
                                }) 
                        }
                        else {
                            return ({
                                data: parsedRet,
                                status: "failed",
                                statusCode: response.status,
                                errMsg: response.statusText,
                                errType: (response.status === 401 || response.status === 403 || parsedRet.status === "access_denied") ? "access denied error" : "server error",
                            });
                        }
                    }
                })},
                err=>{
                    const isTypeError = err.name === "TypeError"; // offline or other reason that failed at network level
                    if (!isTypeError && renewAccessToken && !tokenRenewed) {
                        // only renew(flush) token for non-network error to avoid re-login(network error can be transient)
                        return renewAccessToken()
                        .then(
                            newToken=>{
                            return fetchAPIResult(url, {
                                ...(options),
                                access_token:newToken.access_token,
                                tokenRenewed:true
                            }) 
                        }
/*                                 , error => {
                            return Promise.reject(error);
                        } */
                        )
                        .catch(error=>{
                            return Promise.reject(error)
                        }) 
                    }
                    else {
                        return Promise.reject({
                        data: null,
                        status: "failed",
                        errMsg: err.message || err.errMsg,
                        errType: "network error",
                        });
                    }
            })
            .catch(err=>{
                return Promise.reject({
                    data: null,
                    status: "failed",
                    errMsg: err.message || err.errMsg,
                    errSubType: err.errSubType,
                    errType: err.errType || "fetch error",
                });
                //return Promise.reject(err);
            })
        return promiseWithTimeout(fetchPromise,timeout);    
    }
