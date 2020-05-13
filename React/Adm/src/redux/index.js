
import { sidebarReducer } from './SideBar';
import { authReducer } from './Auth';
import { rintagiReducer } from './_Rintagi';
import { notificationReducer } from './Notification';
import { globalReducer } from './Global';
import { SqlReportReducer } from './SqlReport';
import { CustomReducer } from './Custom';

/* below are dynamic, put shared static one above this */
import AdmAtRowAuthReduxObj from './AdmAtRowAuth';
import AdmCtButtonHlpReduxObj from './AdmCtButtonHlp';
import AdmButtonHlpReduxObj from './AdmButtonHlp';
import AdmChgPwdReduxObj from './AdmChgPwd';
import AdmClientRuleReduxObj from './AdmClientRule';
import AdmClnTierReduxObj from './AdmClnTier';
import AdmCtCultureReduxObj from './AdmCtCulture';
import AdmLabelReduxObj from './AdmLabel';
import AdmDbTableReduxObj from './AdmDbTable';
import AdmDatTierReduxObj from './AdmDatTier';
import AdmCtDisplayTypeReduxObj from './AdmCtDisplayType';
import AdmEntityReduxObj from './AdmEntity';
import AdmDbKeyReduxObj from './AdmDbKey';
import AdmReleaseReduxObj from './AdmRelease';
import AdmLabelVwReduxObj from './AdmLabelVw';
import AdmMaintMsgReduxObj from './AdmMaintMsg';
import AdmMemberReduxObj from './AdmMember';
import AdmMenuDrgReduxObj from './AdmMenuDrg';
import AdmMenuReduxObj from './AdmMenu';
import AdmMenuHlpReduxObj from './AdmMenuHlp';
import AdmMenuOptReduxObj from './AdmMenuOpt';
import AdmMenuPermReduxObj from './AdmMenuPerm';
import AdmMsgCenterReduxObj from './AdmMsgCenter';
import AdmAuthColReduxObj from './AdmAuthCol';
import AdmColDrgReduxObj from './AdmColDrg';
import AdmOvrideReduxObj from './AdmOvride';
import AdmPaymentReduxObj from './AdmPayment';
import AdmAppItemReduxObj from './AdmAppItem';
import AdmRptChaReduxObj from './AdmRptCha';
import AdmRptCtrReduxObj from './AdmRptCtr';
import AdmReportCriReduxObj from './AdmReportCri';
import AdmReportGrpReduxObj from './AdmReportGrp';
import AdmDataCatReduxObj from './AdmDataCat';
import AdmRptwizTypReduxObj from './AdmRptwizTyp';
import AdmReportReduxObj from './AdmReport';
import AdmRptElmReduxObj from './AdmRptElm';
import AdmReportObjReduxObj from './AdmReportObj';
import AdmRptStyleReduxObj from './AdmRptStyle';
import AdmRptTblReduxObj from './AdmRptTbl';
import AdmTemplateReduxObj from './AdmTemplate';
import AdmLicenseReduxObj from './AdmLicense';
import AdmRowOvrdReduxObj from './AdmRowOvrd';
import AdmRulTierReduxObj from './AdmRulTier';
import AdmTbdRuleReduxObj from './AdmTbdRule';
import AdmScrAuditDtlReduxObj from './AdmScrAuditDtl';
import AdmScrAuditReduxObj from './AdmScrAudit';
import AdmScreenCriReduxObj from './AdmScreenCri';
import AdmScreenReduxObj from './AdmScreen';
import AdmScreenFilterReduxObj from './AdmScreenFilter';
import AdmScreenObjReduxObj from './AdmScreenObj';
import AdmColHlpReduxObj from './AdmColHlp';
import AdmScreenTabReduxObj from './AdmScreenTab';
import AdmSctGrpColReduxObj from './AdmSctGrpCol';
import AdmPageObjReduxObj from './AdmPageObj';
import AdmSctGrpRowReduxObj from './AdmSctGrpRow';
import AdmServerRuleReduxObj from './AdmServerRule';
import AdmServerRuleOvrdReduxObj from './AdmServerRuleOvrd';
import AdmSignupReduxObj from './AdmSignup';
import AdmStaticCsReduxObj from './AdmStaticCs';
import AdmStaticFiReduxObj from './AdmStaticFi';
import AdmStaticJsReduxObj from './AdmStaticJs';
import AdmStaticPgReduxObj from './AdmStaticPg';
import AdmSystemsReduxObj from './AdmSystems';
import AdmSredTimeReduxObj from './AdmSredTime';
import AdmCronJobReduxObj from './AdmCronJob';
import AdmUsrGroupReduxObj from './AdmUsrGroup';
import AdmUsrImprReduxObj from './AdmUsrImpr';
import AdmUsrReduxObj from './AdmUsr';
import AdmUsrPrefReduxObj from './AdmUsrPref';
import AdmAppInfoReduxObj from './AdmAppInfo';
import AdmWebRuleReduxObj from './AdmWebRule';
import AdmWizardObjReduxObj from './AdmWizardObj';
import AdmWizardRuleReduxObj from './AdmWizardRule';
export default {
    auth: authReducer,
    rintagi: rintagiReducer,
    global: globalReducer,
    sidebar: sidebarReducer,
    notification: notificationReducer,
    SqlReport: SqlReportReducer,
    ...(CustomReducer || {}),
    /* dynamic go to here */
    AdmAtRowAuth: AdmAtRowAuthReduxObj.ReduxReducer(),
    AdmCtButtonHlp: AdmCtButtonHlpReduxObj.ReduxReducer(),
    AdmButtonHlp: AdmButtonHlpReduxObj.ReduxReducer(),
    AdmChgPwd: AdmChgPwdReduxObj.ReduxReducer(),
    AdmClientRule: AdmClientRuleReduxObj.ReduxReducer(),
    AdmClnTier: AdmClnTierReduxObj.ReduxReducer(),
    AdmCtCulture: AdmCtCultureReduxObj.ReduxReducer(),
    AdmLabel: AdmLabelReduxObj.ReduxReducer(),
    AdmDbTable: AdmDbTableReduxObj.ReduxReducer(),
    AdmDatTier: AdmDatTierReduxObj.ReduxReducer(),
    AdmCtDisplayType: AdmCtDisplayTypeReduxObj.ReduxReducer(),
    AdmEntity: AdmEntityReduxObj.ReduxReducer(),
    AdmDbKey: AdmDbKeyReduxObj.ReduxReducer(),
    AdmRelease: AdmReleaseReduxObj.ReduxReducer(),
    AdmLabelVw: AdmLabelVwReduxObj.ReduxReducer(),
    AdmMaintMsg: AdmMaintMsgReduxObj.ReduxReducer(),
    AdmMember: AdmMemberReduxObj.ReduxReducer(),
    AdmMenuDrg: AdmMenuDrgReduxObj.ReduxReducer(),
    AdmMenu: AdmMenuReduxObj.ReduxReducer(),
    AdmMenuHlp: AdmMenuHlpReduxObj.ReduxReducer(),
    AdmMenuOpt: AdmMenuOptReduxObj.ReduxReducer(),
    AdmMenuPerm: AdmMenuPermReduxObj.ReduxReducer(),
    AdmMsgCenter: AdmMsgCenterReduxObj.ReduxReducer(),
    AdmAuthCol: AdmAuthColReduxObj.ReduxReducer(),
    AdmColDrg: AdmColDrgReduxObj.ReduxReducer(),
    AdmOvride: AdmOvrideReduxObj.ReduxReducer(),
    AdmPayment: AdmPaymentReduxObj.ReduxReducer(),
    AdmAppItem: AdmAppItemReduxObj.ReduxReducer(),
    AdmRptCha: AdmRptChaReduxObj.ReduxReducer(),
    AdmRptCtr: AdmRptCtrReduxObj.ReduxReducer(),
    AdmReportCri: AdmReportCriReduxObj.ReduxReducer(),
    AdmReportGrp: AdmReportGrpReduxObj.ReduxReducer(),
    AdmDataCat: AdmDataCatReduxObj.ReduxReducer(),
    AdmRptwizTyp: AdmRptwizTypReduxObj.ReduxReducer(),
    AdmReport: AdmReportReduxObj.ReduxReducer(),
    AdmRptElm: AdmRptElmReduxObj.ReduxReducer(),
    AdmReportObj: AdmReportObjReduxObj.ReduxReducer(),
    AdmRptStyle: AdmRptStyleReduxObj.ReduxReducer(),
    AdmRptTbl: AdmRptTblReduxObj.ReduxReducer(),
    AdmTemplate: AdmTemplateReduxObj.ReduxReducer(),
    AdmLicense: AdmLicenseReduxObj.ReduxReducer(),
    AdmRowOvrd: AdmRowOvrdReduxObj.ReduxReducer(),
    AdmRulTier: AdmRulTierReduxObj.ReduxReducer(),
    AdmTbdRule: AdmTbdRuleReduxObj.ReduxReducer(),
    AdmScrAuditDtl: AdmScrAuditDtlReduxObj.ReduxReducer(),
    AdmScrAudit: AdmScrAuditReduxObj.ReduxReducer(),
    AdmScreenCri: AdmScreenCriReduxObj.ReduxReducer(),
    AdmScreen: AdmScreenReduxObj.ReduxReducer(),
    AdmScreenFilter: AdmScreenFilterReduxObj.ReduxReducer(),
    AdmScreenObj: AdmScreenObjReduxObj.ReduxReducer(),
    AdmColHlp: AdmColHlpReduxObj.ReduxReducer(),
    AdmScreenTab: AdmScreenTabReduxObj.ReduxReducer(),
    AdmSctGrpCol: AdmSctGrpColReduxObj.ReduxReducer(),
    AdmPageObj: AdmPageObjReduxObj.ReduxReducer(),
    AdmSctGrpRow: AdmSctGrpRowReduxObj.ReduxReducer(),
    AdmServerRule: AdmServerRuleReduxObj.ReduxReducer(),
    AdmServerRuleOvrd: AdmServerRuleOvrdReduxObj.ReduxReducer(),
    AdmSignup: AdmSignupReduxObj.ReduxReducer(),
    AdmStaticCs: AdmStaticCsReduxObj.ReduxReducer(),
    AdmStaticFi: AdmStaticFiReduxObj.ReduxReducer(),
    AdmStaticJs: AdmStaticJsReduxObj.ReduxReducer(),
    AdmStaticPg: AdmStaticPgReduxObj.ReduxReducer(),
    AdmSystems: AdmSystemsReduxObj.ReduxReducer(),
    AdmSredTime: AdmSredTimeReduxObj.ReduxReducer(),
    AdmCronJob: AdmCronJobReduxObj.ReduxReducer(),
    AdmUsrGroup: AdmUsrGroupReduxObj.ReduxReducer(),
    AdmUsrImpr: AdmUsrImprReduxObj.ReduxReducer(),
    AdmUsr: AdmUsrReduxObj.ReduxReducer(),
    AdmUsrPref: AdmUsrPrefReduxObj.ReduxReducer(),
    AdmAppInfo: AdmAppInfoReduxObj.ReduxReducer(),
    AdmWebRule: AdmWebRuleReduxObj.ReduxReducer(),
    AdmWizardObj: AdmWizardObjReduxObj.ReduxReducer(),
    AdmWizardRule: AdmWizardRuleReduxObj.ReduxReducer(),
}
