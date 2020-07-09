
import { fetchData, getAccessControlInfo, getAccessScope, baseUrl } from './webAPIBase';

let activeScope = {};

export function setAccessScope(scope) {
    activeScope = {
        ...activeScope,
        ...scope,
    }
}

export function GetAuthCol(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAuthCol'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAuthRow(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAuthRow'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )

}
export function GetScreenLabel(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetScreenLabel'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope || getAccessScope())
        }
    )

}

export function GetLabels(labelCat, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetLabels'
        , {
            requestOptions: {
                body: JSON.stringify({
                    labelCat: labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetSystemLabels(labelCat, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetSystemLabels'
        , {
            requestOptions: {
                body: JSON.stringify({
                    labelCat: labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetScreenButtonHlp(labelCat, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetScreenButtonHlp'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenHlp(labelCat, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetScreenHlp'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenCriteria(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetScreenCriteria'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewMst(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetNewMst'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewDtl(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetNewDtl'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenFilter(accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetScreenFilter'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetSearchList(searchStr, topN, filterId, desiredScreenCriteria, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetSearchList'
        , {
            requestOptions: {
                body: JSON.stringify({
                    searchStr: searchStr || '',
                    topN: topN || 0,
                    filterId: ('' + (filterId || 0)),
                    desiredScreenCriteria: desiredScreenCriteria || {},
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAdmUsrPref64List(searchStr, topN, filterId, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAdmUsrPref64List'
        , {
            requestOptions: {
                body: JSON.stringify({
                    searchStr: searchStr || '',
                    topN: topN || 0,
                    filterId: ('' + (filterId || 0)),
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAdmUsrPref64ById(keyId, options, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAdmUsrPref64ById'
        , {
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: options || {
                        CurrentScreenCriteria: JSON.stringify({}),
                    },
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export const GetMstById = GetAdmUsrPref64ById;
export function GetAdmUsrPref64DtlById(keyId, filterId, options, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAdmUsrPref64DtlById'
        , {
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: options || {
                        CurrentScreenCriteria: JSON.stringify({}),
                    },
                    filterId: filterId || 0,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export const GetDtlById = GetAdmUsrPref64DtlById;
export function LoadInitPage(options, accessScope) {
    const reqJson = JSON.stringify({
        options: options
    });
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/LoadInitPage'
        , {
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function SaveData(mst, dtl, options, accessScope) {
    const reqJson = JSON.stringify({
        mst: mst || {},
        dtl: dtl || [],
        options: options || {}
    });
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/SaveData'
        , {
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function DelMst(mst, options, accessScope) {
    const reqJson = JSON.stringify({
        mst: mst,
        options: options
    });
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/DelMst'
        , {
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function SetScreenCriteria(criteriaValues, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/SetScreenCriteria'
        , {
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
export function GetRefColumnContent(mstId, dtlId, refKeyId, isMaster, refScreenColumnName, options, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetRefColumnContent'
        , {
            requestOptions: {
                body: JSON.stringify({
                    mstId: mstId || null,
                    dtlId: dtlId || null,
                    refKeyId: refKeyId || null,
                    refScreenColumnName: refScreenColumnName || null,
                    isMaster: isMaster || false,
                    options: options || {},
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetColumnContent(mstId, dtlId, columnName, isMaster, screenColumnName, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetColumnContent'
        , {
            requestOptions: {
                body: JSON.stringify({
                    mstId: mstId || '',
                    dtlId: dtlId || '',
                    screenColumnName: screenColumnName,
                    columnName: columnName,
                    isMaster: isMaster || false,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetEmbeddedDoc(mstId, dtlId, isMaster, screenColumnName, accessScope) {
    const reqJson = JSON.stringify({
        mstId: mstId || '',
        dtlId: dtlId || '',
        isMaster: isMaster || false,
        columnName: screenColumnName || '',
        screenColumnName: screenColumnName || '',
    });
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetColumnContent'
        , {
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function SaveEmbeddedImage(mstId, dtlId, isMaster, screenColumnName, docJson, options, accessScope) {
    const reqJson = JSON.stringify({
        mstId: mstId || '',
        dtlId: dtlId || '',
        isMaster: isMaster || false,
        screenColumnName: screenColumnName || '',
        docJson: docJson || '',
        options: options || {},
    });
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/AddDocColumnContent'
        , {
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function SaveEmbeddedDoc(...args) { return SaveEmbeddedImage(...args) }


export function GetDocZipDownload(keyId, options, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetDocZipDownload'
        , {
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || null,
                    options: options ||{},
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

/*screen criteria dll and screen dropdownlist/autocomplete*/

export function GetMenuOptId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetMenuOptId93List'
        , {
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


export function GetComListVisible93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetComListVisible93List'
        , {
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


export function GetPrjListVisible93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetPrjListVisible93List'
        , {
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


export function GetSysListVisible93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetSysListVisible93List'
        , {
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


export function GetUsrId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetUsrId93List'
        , {
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


export function GetUsrGroupId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetUsrGroupId93List'
        , {
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


export function GetCompanyId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetCompanyId93List'
        , {
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


export function GetProjectId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetProjectId93List'
        , {
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


export function GetSystemId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetSystemId93List'
        , {
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


export function GetMemberId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetMemberId93List'
        , {
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


export function GetAgentId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetAgentId93List'
        , {
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


export function GetBrokerId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetBrokerId93List'
        , {
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


export function GetCustomerId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetCustomerId93List'
        , {
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


export function GetInvestorId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetInvestorId93List'
        , {
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


export function GetVendorId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetVendorId93List'
        , {
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


export function GetLenderId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetLenderId93List'
        , {
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


export function GetBorrowerId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetBorrowerId93List'
        , {
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


export function GetGuarantorId93List(query, topN, filterBy, accessScope) {
    return fetchData(baseUrl + '/AdmUsrPrefWs.asmx/GetGuarantorId93List'
        , {
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

/* ReactRule: Service Custom Function */


/* ReactRule End: Service Custom Function */
