
                import { sidebarReducer } from './SideBar';
                import { authReducer } from './Auth';
                import { notificationReducer } from './Notification';
                import { globalReducer } from './Global';
                import { SqlReportReducer } from './SqlReport';

                /* below are dynamic, put shared static one above this */
            import AdmAtRowAuthReduxObj from './AdmAtRowAuth';
import AdmChgPwdReduxObj from './AdmChgPwd';
import AdmClientRuleReduxObj from './AdmClientRule';
import AdmClnTierReduxObj from './AdmClnTier';
import AdmCtCultureReduxObj from './AdmCtCulture';
import AdmLabelReduxObj from './AdmLabel';
import AdmDbTableReduxObj from './AdmDbTable';
import AdmDatTierReduxObj from './AdmDatTier';
import AdmDbKeyReduxObj from './AdmDbKey';
import AdmReleaseReduxObj from './AdmRelease';
import AdmMaintMsgReduxObj from './AdmMaintMsg';
import AdmMenuDrgReduxObj from './AdmMenuDrg';
import AdmMenuReduxObj from './AdmMenu';
import AdmMenuHlpReduxObj from './AdmMenuHlp';
import AdmMenuOptReduxObj from './AdmMenuOpt';
import AdmMenuPermReduxObj from './AdmMenuPerm';
import AdmMsgCenterReduxObj from './AdmMsgCenter';
import AdmAuthColReduxObj from './AdmAuthCol';
import AdmOvrideReduxObj from './AdmOvride';
import AdmPaymentReduxObj from './AdmPayment';
import AdmAppItemReduxObj from './AdmAppItem';
import AdmRptChaReduxObj from './AdmRptCha';
import AdmRptCtrReduxObj from './AdmRptCtr';
import AdmReportCriReduxObj from './AdmReportCri';
import AdmDataCatReduxObj from './AdmDataCat';
import AdmReportReduxObj from './AdmReport';
import AdmReportObjReduxObj from './AdmReportObj';
import AdmRptStyleReduxObj from './AdmRptStyle';
import AdmRptTblReduxObj from './AdmRptTbl';
import AdmRowOvrdReduxObj from './AdmRowOvrd';
import AdmRulTierReduxObj from './AdmRulTier';
import AdmTbdRuleReduxObj from './AdmTbdRule';
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
import AdmSignupReduxObj from './AdmSignup';
import AdmStaticCsReduxObj from './AdmStaticCs';
import AdmStaticJsReduxObj from './AdmStaticJs';
import AdmStaticPgReduxObj from './AdmStaticPg';
import AdmSystemsReduxObj from './AdmSystems';
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
              global: globalReducer,
              sidebar: sidebarReducer,
              notification: notificationReducer,
              SqlReport: SqlReportReducer,

              /* dynamic go to here */
            AdmAtRowAuth: AdmAtRowAuthReduxObj.ReduxReducer(),
AdmChgPwd: AdmChgPwdReduxObj.ReduxReducer(),
AdmClientRule: AdmClientRuleReduxObj.ReduxReducer(),
AdmClnTier: AdmClnTierReduxObj.ReduxReducer(),
AdmCtCulture: AdmCtCultureReduxObj.ReduxReducer(),
AdmLabel: AdmLabelReduxObj.ReduxReducer(),
AdmDbTable: AdmDbTableReduxObj.ReduxReducer(),
AdmDatTier: AdmDatTierReduxObj.ReduxReducer(),
AdmDbKey: AdmDbKeyReduxObj.ReduxReducer(),
AdmRelease: AdmReleaseReduxObj.ReduxReducer(),
AdmMaintMsg: AdmMaintMsgReduxObj.ReduxReducer(),
AdmMenuDrg: AdmMenuDrgReduxObj.ReduxReducer(),
AdmMenu: AdmMenuReduxObj.ReduxReducer(),
AdmMenuHlp: AdmMenuHlpReduxObj.ReduxReducer(),
AdmMenuOpt: AdmMenuOptReduxObj.ReduxReducer(),
AdmMenuPerm: AdmMenuPermReduxObj.ReduxReducer(),
AdmMsgCenter: AdmMsgCenterReduxObj.ReduxReducer(),
AdmAuthCol: AdmAuthColReduxObj.ReduxReducer(),
AdmOvride: AdmOvrideReduxObj.ReduxReducer(),
AdmPayment: AdmPaymentReduxObj.ReduxReducer(),
AdmAppItem: AdmAppItemReduxObj.ReduxReducer(),
AdmRptCha: AdmRptChaReduxObj.ReduxReducer(),
AdmRptCtr: AdmRptCtrReduxObj.ReduxReducer(),
AdmReportCri: AdmReportCriReduxObj.ReduxReducer(),
AdmDataCat: AdmDataCatReduxObj.ReduxReducer(),
AdmReport: AdmReportReduxObj.ReduxReducer(),
AdmReportObj: AdmReportObjReduxObj.ReduxReducer(),
AdmRptStyle: AdmRptStyleReduxObj.ReduxReducer(),
AdmRptTbl: AdmRptTblReduxObj.ReduxReducer(),
AdmRowOvrd: AdmRowOvrdReduxObj.ReduxReducer(),
AdmRulTier: AdmRulTierReduxObj.ReduxReducer(),
AdmTbdRule: AdmTbdRuleReduxObj.ReduxReducer(),
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
AdmSignup: AdmSignupReduxObj.ReduxReducer(),
AdmStaticCs: AdmStaticCsReduxObj.ReduxReducer(),
AdmStaticJs: AdmStaticJsReduxObj.ReduxReducer(),
AdmStaticPg: AdmStaticPgReduxObj.ReduxReducer(),
AdmSystems: AdmSystemsReduxObj.ReduxReducer(),
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
            