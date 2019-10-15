import {fetchData,getAccessControlInfo,baseUrl} from './webAPIBase';
import log from '../helpers/logger';

export function GetReportHlp(rptId,accessScope){
    return fetchData(baseUrl+'/SqlReportWs.asmx/GetReportHlp'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    rptId: rptId
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetReportCriteria(rptId,accessScope){
    return fetchData(baseUrl+'/SqlReportWs.asmx/GetReportCriteria'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    rptId: rptId
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetReportCriDdl(rptId,accessScope){
    return fetchData(baseUrl+'/SqlReportWs.asmx/GetReportCriDdl'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    rptId: rptId
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetReport(reportId, criteria, format){
    return fetchData(baseUrl+'/SqlReportWs.asmx/GetReport'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    reportId: reportId,
                    criteria: criteria,
                    fmt: format
                }),
            },
            ...(getAccessControlInfo())
        }
    )

}
