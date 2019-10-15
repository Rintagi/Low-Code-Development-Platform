
import {fetchData,getAccessControlInfo, getAccessScope, baseUrl} from './webAPIBase';

let activeScope = {};

export function setAccessScope(scope) {
    activeScope = {
        ...activeScope,
        ...scope,
    }
}

export function GetAuthCol(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetAuthCol'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAuthRow(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetAuthRow'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )

}
export function GetScreenLabel(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenLabel'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope || getAccessScope())
        }
    )

}

export function GetLabels(labelCat,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetLabels'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    labelCat:labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetSystemLabels(labelCat,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetSystemLabels'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    labelCat:labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetScreenButtonHlp(labelCat,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenButtonHlp'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenHlp(labelCat,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenHlp'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenCriteria(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenCriteria'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewMst(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetNewMst'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewDtl(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetNewDtl'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenFilter(accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenFilter'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetColumnContent(mstId, dtlId, columnName, isMaster, screenColumnName, accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetColumnContent'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    mstId: mstId || '',
                    dtlId: dtlId || '',
                    screenColumnName: screenColumnName,
                    columnName: columnName,
                    isMaster: isMaster,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAdmStaticPg114List(searchStr, topN, filterId,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetAdmStaticPg114List'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    searchStr: searchStr || '',
                    topN: topN || 0,
                    filterId: ('' +  (filterId || 0)),
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAdmStaticPg114ById(keyId,accessScope){   
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetAdmStaticPg114ById'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: {
                        currentScreenCriteria : JSON.stringify({}),
                    },
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAdmStaticPg114DtlById(keyId,filterId,accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetAdmStaticPg114DtlById'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: {
                        currentScreenCriteria : JSON.stringify({}),
                    },
                    filterId: filterId || 0,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function LoadInitPage(options,accessScope) {
    const reqJson = JSON.stringify({
        options: options
    });
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/LoadInitPage'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function SaveData(mst,dtl,options,accessScope){
    const reqJson = JSON.stringify({
        mst: mst,
        dtl: dtl,
        options: options
    });
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/SaveData'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function DelMst(mst,options,accessScope){
    const reqJson = JSON.stringify({
        mst: mst,
        options: options
    });
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/DelMst'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function SetScreenCriteria(criteriaValues, accessScope){
    return fetchData(baseUrl+'/AdmStaticPgWs.asmx/SetScreenCriteria'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    criteriaValues: criteriaValues
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

/*screen criteria dll and screen dropdownlist/autocomplete*/           
            export function GetScreenCriStaticPgId10List(query, topN, filterBy, accessScope){
return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenCriteriaDdlList'
,{
requestOptions: {
body: JSON.stringify({
screenCriId: 1070,
query: query || '',
topN: topN || 0,
filterBy: filterBy || null
}),
},
...(getAccessControlInfo()),
...(accessScope)
}
)
}
export function GetScreenCriStaticMeta20List(query, topN, filterBy, accessScope){
return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetScreenCriteriaDdlList'
,{
requestOptions: {
body: JSON.stringify({
screenCriId: 55,
query: query || '',
topN: topN || 0,
filterBy: filterBy || null
}),
},
...(getAccessControlInfo()),
...(accessScope)
}
)
}export function GetStaticCsId259List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetStaticCsId259List'

                                                    ,{
                                                        requestOptions: {
                                                            body: JSON.stringify({
                                                                query: query || '',
                                                                topN: topN || 0,
                                                                filterBy: filterBy || null
                                                            }),
                                                        },
                                                        ...(getAccessControlInfo()),
                                                        ...(accessScope)
                                                    }
                                                )
                                            }
export function GetStaticJsId259List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmStaticPgWs.asmx/GetStaticJsId259List'

                                                    ,{
                                                        requestOptions: {
                                                            body: JSON.stringify({
                                                                query: query || '',
                                                                topN: topN || 0,
                                                                filterBy: filterBy || null
                                                            }),
                                                        },
                                                        ...(getAccessControlInfo()),
                                                        ...(accessScope)
                                                    }
                                                )
                                            }/* ReactRule: Service Custom Function *//* ReactRule End: Service Custom Function */