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
    const {access_token, renewAccessToken, tokenRenewed,timeout, SystemId, CompanyId, ProjectId, CultureId} = options;
    const scope = '' + (SystemId > 0 ? SystemId : '') + ',' + (typeof CompanyId === "undefined" ? '' : CompanyId)  + ',' + (typeof ProjectId === "undefined" ? '' : ProjectId)  + ',' + (typeof CultureId === "undefined" ? '' : CultureId);
    const requestOptions = {
        method: 'post',
        mode: 'cors',
        //credentials: 'include',
        headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
        'Authorization': 'Bearer ' +  (access_token || ""),
        'X-Authorization': 'Bearer ' +  (access_token || ""),
        ...(options.headers),
        'X-RintagiScope': scope
        },
        ...(options.requestOptions)
        };   
    const fetchPromise = fetch(url,requestOptions)
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
                                status: "fail",
                                statusCode: response.status,
                                errMsg: response.statusText,
                                errType: (response.status === 401 || response.status === 403 || parsedRet.status === "access_denied") ? "access denied error" : "server error",
                            });
                        }
                    }
                })},
                err=>{
                    if (renewAccessToken && !tokenRenewed) {
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
                        status: "fail",
                        errMsg: err.message || err.errMsg,
                        errType: "network error",
                        });
                    }
            })
            .catch(err=>{
                return Promise.reject({
                    data: null,
                    status: "fail",
                    errMsg: err.message || err.errMsg,
                    errSubType: err.errSubType,
                    errType: err.errType || "fetch error",
                });
                //return Promise.reject(err);
            })
        return promiseWithTimeout(fetchPromise,timeout);    
    }
