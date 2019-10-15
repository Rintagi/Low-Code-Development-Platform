
import {fetchData,getAccessControlInfo, getAccessScope, baseUrl} from './webAPIBase';

let activeScope = {};

export function setAccessScope(scope) {
    activeScope = {
        ...activeScope,
        ...scope,
    }
}

export function GetAuthCol(accessScope){
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetAuthCol'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetAuthRow'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetScreenLabel'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetLabels'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetSystemLabels'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetScreenButtonHlp'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetScreenHlp'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetScreenCriteria'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetNewMst'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetNewDtl'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetScreenFilter'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetColumnContent'
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
export function GetAdmReport67List(searchStr, topN, filterId,accessScope){
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetAdmReport67List'
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
export function GetAdmReport67ById(keyId,accessScope){   
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetAdmReport67ById'
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
export function GetAdmReport67DtlById(keyId,filterId,accessScope){
    return fetchData(baseUrl+'/AdmReportWs.asmx/GetAdmReport67DtlById'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/LoadInitPage'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/SaveData'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/DelMst'
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
    return fetchData(baseUrl+'/AdmReportWs.asmx/SetScreenCriteria'
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
            export function GetReportTypeCd22List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetReportTypeCd22List'

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
export function GetOrientationCd22List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetOrientationCd22List'

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
export function GetCopyReportId22List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetCopyReportId22List'

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
export function GetModifiedBy22List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetModifiedBy22List'

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
export function GetUnitCd22List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetUnitCd22List'

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
export function GetCultureId96List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportWs.asmx/GetCultureId96List'

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