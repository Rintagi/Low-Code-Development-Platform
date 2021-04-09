
import { sidebarReducer } from './SideBar';
import { authReducer } from './Auth';
import { rintagiReducer } from './_Rintagi';
import { notificationReducer } from './Notification';
import { globalReducer } from './Global';
import { SqlReportReducer } from './SqlReport';
import { CustomReducer } from './Custom';

/* below are dynamic, put shared static one above this */
import AdmAppInfoReduxObj from './AdmAppInfo';
import AdmAppItemReduxObj from './AdmAppItem';
import AdmAtRowAuthReduxObj from './AdmAtRowAuth';
import AdmAuthColReduxObj from './AdmAuthCol';
import AdmButtonHlpReduxObj from './AdmButtonHlp';
import AdmChgPwdReduxObj from './AdmChgPwd';
import AdmClientRuleReduxObj from './AdmClientRule';
import AdmClnTierReduxObj from './AdmClnTier';
import AdmColDrgReduxObj from './AdmColDrg';
import AdmColHlpReduxObj from './AdmColHlp';
import AdmCompPrefReduxObj from './AdmCompPref';
import AdmCronJobReduxObj from './AdmCronJob';
import AdmCtButtonHlpReduxObj from './AdmCtButtonHlp';
import AdmCtCultureReduxObj from './AdmCtCulture';
import AdmCtDisplayTypeReduxObj from './AdmCtDisplayType';
import AdmDataCatReduxObj from './AdmDataCat';
import AdmDatTierReduxObj from './AdmDatTier';
import AdmDbKeyReduxObj from './AdmDbKey';
import AdmDbTableReduxObj from './AdmDbTable';
import AdmEntityReduxObj from './AdmEntity';
import AdmFlowchartReduxObj from './AdmFlowchart';
import AdmLabelReduxObj from './AdmLabel';
import AdmLabelVwReduxObj from './AdmLabelVw';
import AdmLicenseReduxObj from './AdmLicense';
import AdmMaintMsgReduxObj from './AdmMaintMsg';
import AdmMemberReduxObj from './AdmMember';
import AdmMenuReduxObj from './AdmMenu';
import AdmMenuDrgReduxObj from './AdmMenuDrg';
import AdmMenuHlpReduxObj from './AdmMenuHlp';
import AdmMenuOptReduxObj from './AdmMenuOpt';
import AdmMenuPermReduxObj from './AdmMenuPerm';
import AdmMsgCenterReduxObj from './AdmMsgCenter';
import AdmOvrideReduxObj from './AdmOvride';
import AdmPageObjReduxObj from './AdmPageObj';
import AdmPaymentReduxObj from './AdmPayment';
import AdmPostmanReduxObj from './AdmPostman';
import AdmReleaseReduxObj from './AdmRelease';
import AdmReportReduxObj from './AdmReport';
import AdmReportCriReduxObj from './AdmReportCri';
import AdmReportGrpReduxObj from './AdmReportGrp';
import AdmReportObjReduxObj from './AdmReportObj';
import AdmRowOvrdReduxObj from './AdmRowOvrd';
import AdmRptChaReduxObj from './AdmRptCha';
import AdmRptCtrReduxObj from './AdmRptCtr';
import AdmRptElmReduxObj from './AdmRptElm';
import AdmRptStyleReduxObj from './AdmRptStyle';
import AdmRptTblReduxObj from './AdmRptTbl';
import AdmRptwizTypReduxObj from './AdmRptwizTyp';
import AdmRulTierReduxObj from './AdmRulTier';
import AdmScrAuditReduxObj from './AdmScrAudit';
import AdmScrAuditDtlReduxObj from './AdmScrAuditDtl';
import AdmScreenReduxObj from './AdmScreen';
import AdmScreenCriReduxObj from './AdmScreenCri';
import AdmScreenFilterReduxObj from './AdmScreenFilter';
import AdmScreenObjReduxObj from './AdmScreenObj';
import AdmScreenTabReduxObj from './AdmScreenTab';
import AdmSctGrpColReduxObj from './AdmSctGrpCol';
import AdmSctGrpRowReduxObj from './AdmSctGrpRow';
import AdmServerRuleReduxObj from './AdmServerRule';
import AdmServerRuleOvrdReduxObj from './AdmServerRuleOvrd';
import AdmSignupReduxObj from './AdmSignup';
import AdmSredTimeReduxObj from './AdmSredTime';
import AdmStaticCsReduxObj from './AdmStaticCs';
import AdmStaticFiReduxObj from './AdmStaticFi';
import AdmStaticJsReduxObj from './AdmStaticJs';
import AdmStaticPgReduxObj from './AdmStaticPg';
import AdmSystemsReduxObj from './AdmSystems';
import AdmTbdRuleReduxObj from './AdmTbdRule';
import AdmTemplateReduxObj from './AdmTemplate';
import AdmUsrReduxObj from './AdmUsr';
import AdmUsrGroupReduxObj from './AdmUsrGroup';
import AdmUsrImprReduxObj from './AdmUsrImpr';
import AdmUsrPrefReduxObj from './AdmUsrPref';
import AdmWebRuleReduxObj from './AdmWebRule';
import AdmWizardObjReduxObj from './AdmWizardObj';
import AdmWizardRuleReduxObj from './AdmWizardRule';
let redux = {
    auth: authReducer,
    rintagi: rintagiReducer,
    global: globalReducer,
    sidebar: sidebarReducer,
    notification: notificationReducer,
    SqlReport: SqlReportReducer,
    ...(CustomReducer || {}),
    /* dynamic go to here */
    AdmAppInfo: AdmAppInfoReduxObj.ReduxReducer(),
    AdmAppItem: AdmAppItemReduxObj.ReduxReducer(),
    AdmAtRowAuth: AdmAtRowAuthReduxObj.ReduxReducer(),
    AdmAuthCol: AdmAuthColReduxObj.ReduxReducer(),
    AdmButtonHlp: AdmButtonHlpReduxObj.ReduxReducer(),
    AdmChgPwd: AdmChgPwdReduxObj.ReduxReducer(),
    AdmClientRule: AdmClientRuleReduxObj.ReduxReducer(),
    AdmClnTier: AdmClnTierReduxObj.ReduxReducer(),
    AdmColDrg: AdmColDrgReduxObj.ReduxReducer(),
    AdmColHlp: AdmColHlpReduxObj.ReduxReducer(),
    AdmCompPref: AdmCompPrefReduxObj.ReduxReducer(),
    AdmCronJob: AdmCronJobReduxObj.ReduxReducer(),
    AdmCtButtonHlp: AdmCtButtonHlpReduxObj.ReduxReducer(),
    AdmCtCulture: AdmCtCultureReduxObj.ReduxReducer(),
    AdmCtDisplayType: AdmCtDisplayTypeReduxObj.ReduxReducer(),
    AdmDataCat: AdmDataCatReduxObj.ReduxReducer(),
    AdmDatTier: AdmDatTierReduxObj.ReduxReducer(),
    AdmDbKey: AdmDbKeyReduxObj.ReduxReducer(),
    AdmDbTable: AdmDbTableReduxObj.ReduxReducer(),
    AdmEntity: AdmEntityReduxObj.ReduxReducer(),
    AdmFlowchart: AdmFlowchartReduxObj.ReduxReducer(),
    AdmLabel: AdmLabelReduxObj.ReduxReducer(),
    AdmLabelVw: AdmLabelVwReduxObj.ReduxReducer(),
    AdmLicense: AdmLicenseReduxObj.ReduxReducer(),
    AdmMaintMsg: AdmMaintMsgReduxObj.ReduxReducer(),
    AdmMember: AdmMemberReduxObj.ReduxReducer(),
    AdmMenu: AdmMenuReduxObj.ReduxReducer(),
    AdmMenuDrg: AdmMenuDrgReduxObj.ReduxReducer(),
    AdmMenuHlp: AdmMenuHlpReduxObj.ReduxReducer(),
    AdmMenuOpt: AdmMenuOptReduxObj.ReduxReducer(),
    AdmMenuPerm: AdmMenuPermReduxObj.ReduxReducer(),
    AdmMsgCenter: AdmMsgCenterReduxObj.ReduxReducer(),
    AdmOvride: AdmOvrideReduxObj.ReduxReducer(),
    AdmPageObj: AdmPageObjReduxObj.ReduxReducer(),
    AdmPayment: AdmPaymentReduxObj.ReduxReducer(),
    AdmPostman: AdmPostmanReduxObj.ReduxReducer(),
    AdmRelease: AdmReleaseReduxObj.ReduxReducer(),
    AdmReport: AdmReportReduxObj.ReduxReducer(),
    AdmReportCri: AdmReportCriReduxObj.ReduxReducer(),
    AdmReportGrp: AdmReportGrpReduxObj.ReduxReducer(),
    AdmReportObj: AdmReportObjReduxObj.ReduxReducer(),
    AdmRowOvrd: AdmRowOvrdReduxObj.ReduxReducer(),
    AdmRptCha: AdmRptChaReduxObj.ReduxReducer(),
    AdmRptCtr: AdmRptCtrReduxObj.ReduxReducer(),
    AdmRptElm: AdmRptElmReduxObj.ReduxReducer(),
    AdmRptStyle: AdmRptStyleReduxObj.ReduxReducer(),
    AdmRptTbl: AdmRptTblReduxObj.ReduxReducer(),
    AdmRptwizTyp: AdmRptwizTypReduxObj.ReduxReducer(),
    AdmRulTier: AdmRulTierReduxObj.ReduxReducer(),
    AdmScrAudit: AdmScrAuditReduxObj.ReduxReducer(),
    AdmScrAuditDtl: AdmScrAuditDtlReduxObj.ReduxReducer(),
    AdmScreen: AdmScreenReduxObj.ReduxReducer(),
    AdmScreenCri: AdmScreenCriReduxObj.ReduxReducer(),
    AdmScreenFilter: AdmScreenFilterReduxObj.ReduxReducer(),
    AdmScreenObj: AdmScreenObjReduxObj.ReduxReducer(),
    AdmScreenTab: AdmScreenTabReduxObj.ReduxReducer(),
    AdmSctGrpCol: AdmSctGrpColReduxObj.ReduxReducer(),
    AdmSctGrpRow: AdmSctGrpRowReduxObj.ReduxReducer(),
    AdmServerRule: AdmServerRuleReduxObj.ReduxReducer(),
    AdmServerRuleOvrd: AdmServerRuleOvrdReduxObj.ReduxReducer(),
    AdmSignup: AdmSignupReduxObj.ReduxReducer(),
    AdmSredTime: AdmSredTimeReduxObj.ReduxReducer(),
    AdmStaticCs: AdmStaticCsReduxObj.ReduxReducer(),
    AdmStaticFi: AdmStaticFiReduxObj.ReduxReducer(),
    AdmStaticJs: AdmStaticJsReduxObj.ReduxReducer(),
    AdmStaticPg: AdmStaticPgReduxObj.ReduxReducer(),
    AdmSystems: AdmSystemsReduxObj.ReduxReducer(),
    AdmTbdRule: AdmTbdRuleReduxObj.ReduxReducer(),
    AdmTemplate: AdmTemplateReduxObj.ReduxReducer(),
    AdmUsr: AdmUsrReduxObj.ReduxReducer(),
    AdmUsrGroup: AdmUsrGroupReduxObj.ReduxReducer(),
    AdmUsrImpr: AdmUsrImprReduxObj.ReduxReducer(),
    AdmUsrPref: AdmUsrPrefReduxObj.ReduxReducer(),
    AdmWebRule: AdmWebRuleReduxObj.ReduxReducer(),
    AdmWizardObj: AdmWizardObjReduxObj.ReduxReducer(),
    AdmWizardRule: AdmWizardRuleReduxObj.ReduxReducer(),
}
 export default redux;
