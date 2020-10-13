
import {pagesRoutes as AccountRoute} from '../pages/Account/index'
import {pagesRoutes as SqlReportRoute} from '../pages/SqlReport/index'
import {pagesRoutes as DefaultRoute} from '../pages/Default/index'
import {CustomRoutePre, CustomRoutePost, SuppressGenRoute} from '../pages/CustomRoute'
/* all these are dynamic, add the required route for each page */
import {pagesRoutes as AdmAtRowAuthRoute} from '../pages/AdmAtRowAuth/index'
import {pagesRoutes as AdmCtButtonHlpRoute} from '../pages/AdmCtButtonHlp/index'
import {pagesRoutes as AdmButtonHlpRoute} from '../pages/AdmButtonHlp/index'
import {pagesRoutes as AdmChgPwdRoute} from '../pages/AdmChgPwd/index'
import {pagesRoutes as AdmClientRuleRoute} from '../pages/AdmClientRule/index'
import {pagesRoutes as AdmClnTierRoute} from '../pages/AdmClnTier/index'
import {pagesRoutes as AdmCtCultureRoute} from '../pages/AdmCtCulture/index'
import {pagesRoutes as AdmLabelRoute} from '../pages/AdmLabel/index'
import {pagesRoutes as AdmDbTableRoute} from '../pages/AdmDbTable/index'
import {pagesRoutes as AdmDatTierRoute} from '../pages/AdmDatTier/index'
import {pagesRoutes as AdmCtDisplayTypeRoute} from '../pages/AdmCtDisplayType/index'
import {pagesRoutes as AdmEntityRoute} from '../pages/AdmEntity/index'
import {pagesRoutes as AdmFlowchartRoute} from '../pages/AdmFlowchart/index'
import {pagesRoutes as AdmDbKeyRoute} from '../pages/AdmDbKey/index'
import {pagesRoutes as AdmReleaseRoute} from '../pages/AdmRelease/index'
import {pagesRoutes as AdmLabelVwRoute} from '../pages/AdmLabelVw/index'
import {pagesRoutes as AdmMaintMsgRoute} from '../pages/AdmMaintMsg/index'
import {pagesRoutes as AdmMemberRoute} from '../pages/AdmMember/index'
import {pagesRoutes as AdmMenuDrgRoute} from '../pages/AdmMenuDrg/index'
import {pagesRoutes as AdmMenuRoute} from '../pages/AdmMenu/index'
import {pagesRoutes as AdmMenuHlpRoute} from '../pages/AdmMenuHlp/index'
import {pagesRoutes as AdmMenuOptRoute} from '../pages/AdmMenuOpt/index'
import {pagesRoutes as AdmMenuPermRoute} from '../pages/AdmMenuPerm/index'
import {pagesRoutes as AdmMsgCenterRoute} from '../pages/AdmMsgCenter/index'
import {pagesRoutes as AdmAuthColRoute} from '../pages/AdmAuthCol/index'
import {pagesRoutes as AdmColDrgRoute} from '../pages/AdmColDrg/index'
import {pagesRoutes as AdmOvrideRoute} from '../pages/AdmOvride/index'
import {pagesRoutes as AdmPaymentRoute} from '../pages/AdmPayment/index'
import {pagesRoutes as AdmAppItemRoute} from '../pages/AdmAppItem/index'
import {pagesRoutes as AdmRptChaRoute} from '../pages/AdmRptCha/index'
import {pagesRoutes as AdmRptCtrRoute} from '../pages/AdmRptCtr/index'
import {pagesRoutes as AdmReportCriRoute} from '../pages/AdmReportCri/index'
import {pagesRoutes as AdmReportGrpRoute} from '../pages/AdmReportGrp/index'
import {pagesRoutes as AdmDataCatRoute} from '../pages/AdmDataCat/index'
import {pagesRoutes as AdmRptwizTypRoute} from '../pages/AdmRptwizTyp/index'
import {pagesRoutes as AdmReportRoute} from '../pages/AdmReport/index'
import {pagesRoutes as AdmRptElmRoute} from '../pages/AdmRptElm/index'
import {pagesRoutes as AdmReportObjRoute} from '../pages/AdmReportObj/index'
import {pagesRoutes as AdmRptStyleRoute} from '../pages/AdmRptStyle/index'
import {pagesRoutes as AdmRptTblRoute} from '../pages/AdmRptTbl/index'
import {pagesRoutes as AdmTemplateRoute} from '../pages/AdmTemplate/index'
import {pagesRoutes as AdmLicenseRoute} from '../pages/AdmLicense/index'
import {pagesRoutes as AdmRowOvrdRoute} from '../pages/AdmRowOvrd/index'
import {pagesRoutes as AdmRulTierRoute} from '../pages/AdmRulTier/index'
import {pagesRoutes as AdmTbdRuleRoute} from '../pages/AdmTbdRule/index'
import {pagesRoutes as AdmScrAuditDtlRoute} from '../pages/AdmScrAuditDtl/index'
import {pagesRoutes as AdmScrAuditRoute} from '../pages/AdmScrAudit/index'
import {pagesRoutes as AdmScreenCriRoute} from '../pages/AdmScreenCri/index'
import {pagesRoutes as AdmScreenRoute} from '../pages/AdmScreen/index'
import {pagesRoutes as AdmScreenFilterRoute} from '../pages/AdmScreenFilter/index'
import {pagesRoutes as AdmScreenObjRoute} from '../pages/AdmScreenObj/index'
import {pagesRoutes as AdmColHlpRoute} from '../pages/AdmColHlp/index'
import {pagesRoutes as AdmScreenTabRoute} from '../pages/AdmScreenTab/index'
import {pagesRoutes as AdmSctGrpColRoute} from '../pages/AdmSctGrpCol/index'
import {pagesRoutes as AdmPageObjRoute} from '../pages/AdmPageObj/index'
import {pagesRoutes as AdmSctGrpRowRoute} from '../pages/AdmSctGrpRow/index'
import {pagesRoutes as AdmServerRuleRoute} from '../pages/AdmServerRule/index'
import {pagesRoutes as AdmServerRuleOvrdRoute} from '../pages/AdmServerRuleOvrd/index'
import {pagesRoutes as AdmSignupRoute} from '../pages/AdmSignup/index'
import {pagesRoutes as AdmStaticCsRoute} from '../pages/AdmStaticCs/index'
import {pagesRoutes as AdmStaticFiRoute} from '../pages/AdmStaticFi/index'
import {pagesRoutes as AdmStaticJsRoute} from '../pages/AdmStaticJs/index'
import {pagesRoutes as AdmStaticPgRoute} from '../pages/AdmStaticPg/index'
import {pagesRoutes as AdmSystemsRoute} from '../pages/AdmSystems/index'
import {pagesRoutes as AdmSredTimeRoute} from '../pages/AdmSredTime/index'
import {pagesRoutes as AdmCronJobRoute} from '../pages/AdmCronJob/index'
import {pagesRoutes as AdmUsrGroupRoute} from '../pages/AdmUsrGroup/index'
import {pagesRoutes as AdmUsrImprRoute} from '../pages/AdmUsrImpr/index'
import {pagesRoutes as AdmUsrRoute} from '../pages/AdmUsr/index'
import {pagesRoutes as AdmUsrPrefRoute} from '../pages/AdmUsrPref/index'
import {pagesRoutes as AdmAppInfoRoute} from '../pages/AdmAppInfo/index'
import {pagesRoutes as AdmWebRuleRoute} from '../pages/AdmWebRule/index'
import {pagesRoutes as AdmWizardObjRoute} from '../pages/AdmWizardObj/index'
import {pagesRoutes as AdmWizardRuleRoute} from '../pages/AdmWizardRule/index'
export default [
...(CustomRoutePre || []),
...(
SuppressGenRoute ? [] : [
...AccountRoute,
...DefaultRoute,
// ...SqlReportRoute,
]
),
...(
SuppressGenRoute ? [] : [
...AdmAtRowAuthRoute,
...AdmCtButtonHlpRoute,
...AdmButtonHlpRoute,
...AdmChgPwdRoute,
...AdmClientRuleRoute,
...AdmClnTierRoute,
...AdmCtCultureRoute,
...AdmLabelRoute,
...AdmDbTableRoute,
...AdmDatTierRoute,
...AdmCtDisplayTypeRoute,
...AdmEntityRoute,
...AdmFlowchartRoute,
...AdmDbKeyRoute,
...AdmReleaseRoute,
...AdmLabelVwRoute,
...AdmMaintMsgRoute,
...AdmMemberRoute,
...AdmMenuDrgRoute,
...AdmMenuRoute,
...AdmMenuHlpRoute,
...AdmMenuOptRoute,
...AdmMenuPermRoute,
...AdmMsgCenterRoute,
...AdmAuthColRoute,
...AdmColDrgRoute,
...AdmOvrideRoute,
...AdmPaymentRoute,
...AdmAppItemRoute,
...AdmRptChaRoute,
...AdmRptCtrRoute,
...AdmReportCriRoute,
...AdmReportGrpRoute,
...AdmDataCatRoute,
...AdmRptwizTypRoute,
...AdmReportRoute,
...AdmRptElmRoute,
...AdmReportObjRoute,
...AdmRptStyleRoute,
...AdmRptTblRoute,
...AdmTemplateRoute,
...AdmLicenseRoute,
...AdmRowOvrdRoute,
...AdmRulTierRoute,
...AdmTbdRuleRoute,
...AdmScrAuditDtlRoute,
...AdmScrAuditRoute,
...AdmScreenCriRoute,
...AdmScreenRoute,
...AdmScreenFilterRoute,
...AdmScreenObjRoute,
...AdmColHlpRoute,
...AdmScreenTabRoute,
...AdmSctGrpColRoute,
...AdmPageObjRoute,
...AdmSctGrpRowRoute,
...AdmServerRuleRoute,
...AdmServerRuleOvrdRoute,
...AdmSignupRoute,
...AdmStaticCsRoute,
...AdmStaticFiRoute,
...AdmStaticJsRoute,
...AdmStaticPgRoute,
...AdmSystemsRoute,
...AdmSredTimeRoute,
...AdmCronJobRoute,
...AdmUsrGroupRoute,
...AdmUsrImprRoute,
...AdmUsrRoute,
...AdmUsrPrefRoute,
...AdmAppInfoRoute,
...AdmWebRuleRoute,
...AdmWizardObjRoute,
...AdmWizardRuleRoute,
]),
...(CustomRoutePost || []),
];

 document.Rintagi.systemId = '3';