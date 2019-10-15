import { fetchData, getAccessControlInfo, getAccessScope, baseUrl } from './webAPIBase';
import log from '../helpers/logger';

export function getCompanyList() {
    return fetchData(baseUrl + '/SystemWs.asmx/GetCompanyList'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}
export function getProjectList(companyId) {
    return fetchData(baseUrl + '/SystemWs.asmx/GetProjectList'
        , {
            requestOptions: {
                body: JSON.stringify({
                    CompanyId: companyId || 0
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}
export function getTimeZoneList() {
    return fetchData(baseUrl + '/SystemWs.asmx/GetTimeZoneList'
        , {
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}

export function getCultureList(CultureId, lang){
    return fetchData(baseUrl+'/SystemWs.asmx/GetCultureList'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    CultureId:CultureId,
                    langCode:lang || "",
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}

export function switchCurrent(companyId, projectId, cultureId) {
    console.log(JSON.stringify({
        CompanyId: companyId,
        ProjectId: projectId,
        CultureId: +cultureId,
    }));
    return fetchData(baseUrl + '/SystemWs.asmx/SwitchCurrent'
        , {
            requestOptions: {
                body: JSON.stringify({
                    CompanyId: companyId,
                    ProjectId: projectId,
                    CultureId: +cultureId,
                }),
            },
            ...(getAccessControlInfo())
        }
    )
}
