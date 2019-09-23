
import {fetchData,getAccessControlInfo, getAccessScope, baseUrl} from './webAPIBase';

let activeScope = {};

export function setAccessScope(scope) {
    activeScope = {
        ...activeScope,
        ...scope,
    }
}

export function GetAuthCol(accessScope){
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetAuthCol'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetAuthRow'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenLabel'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetLabels'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetSystemLabels'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenButtonHlp'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenHlp'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenCriteria'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetNewMst'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetNewDtl'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenFilter'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetColumnContent'
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
export function GetAdmReportCri69List(searchStr, topN, filterId,accessScope){
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetAdmReportCri69List'
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
export function GetAdmReportCri69ById(keyId,accessScope){   
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetAdmReportCri69ById'
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
export function GetAdmReportCri69DtlById(keyId,filterId,accessScope){
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetAdmReportCri69DtlById'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/LoadInitPage'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/SaveData'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/DelMst'
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
    return fetchData(baseUrl+'/AdmReportCriWs.asmx/SetScreenCriteria'
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
            export function GetScreenCriReportId10List(query, topN, filterBy, accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetScreenCriteriaDdlList'
,{
requestOptions: {
body: JSON.stringify({
screenCriId: 4,
query: query || '',
topN: topN || 0,
filterBy: filterBy || null
}),
},
...(getAccessControlInfo()),
...(accessScope)
}
)
}export function GetReportId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetReportId97List'

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
export function GetReportGrpId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetReportGrpId97List'

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
export function GetTableId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetTableId97List'

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
export function GetDataTypeId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetDataTypeId97List'

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
export function GetDisplayModeId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetDisplayModeId97List'

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
export function GetDdlFtrColumnId97List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetDdlFtrColumnId97List'

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
export function GetCultureId98List(query, topN, filterBy,accessScope){
return fetchData(baseUrl+'/AdmReportCriWs.asmx/GetCultureId98List'

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