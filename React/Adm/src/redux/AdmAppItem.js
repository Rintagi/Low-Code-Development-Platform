
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmAppItemService from '../services/AdmAppItemService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmAppItemRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmAppItem83List',
        'GET_MST' : 'GetAdmAppItem83ById',
        'GET_DTL_LIST' : 'GetAdmAppItem83DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'AppInfoId136', payloadDdlName:'AppInfoId136List', keyName:'AppInfoId136',labelName:'AppInfoId136Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetAppInfoId136List', actionTypeName: 'GET_DDL_AppInfoId136' },
{ columnName: 'LanguageCd136', payloadDdlName:'LanguageCd136List', keyName:'LanguageCd136',labelName:'LanguageCd136Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetLanguageCd136List', actionTypeName: 'GET_DDL_LanguageCd136' },
{ columnName: 'FrameworkCd136', payloadDdlName:'FrameworkCd136List', keyName:'FrameworkCd136',labelName:'FrameworkCd136Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetFrameworkCd136List', actionTypeName: 'GET_DDL_FrameworkCd136' },
{ columnName: 'ObjectTypeCd136', payloadDdlName:'ObjectTypeCd136List', keyName:'ObjectTypeCd136',labelName:'ObjectTypeCd136Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetObjectTypeCd136List', actionTypeName: 'GET_DDL_ObjectTypeCd136' },
{ columnName: 'DbProviderCd136', payloadDdlName:'DbProviderCd136List', keyName:'DbProviderCd136',labelName:'DbProviderCd136Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDbProviderCd136List', actionTypeName: 'GET_DDL_DbProviderCd136' },
{ columnName: 'ScreenId136', payloadDdlName:'ScreenId136List', keyName:'ScreenId136',labelName:'ScreenId136Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenId136List', actionTypeName: 'GET_DDL_ScreenId136' },
{ columnName: 'ReportId136', payloadDdlName:'ReportId136List', keyName:'ReportId136',labelName:'ReportId136Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId136List', actionTypeName: 'GET_DDL_ReportId136' },
{ columnName: 'WizardId136', payloadDdlName:'WizardId136List', keyName:'WizardId136',labelName:'WizardId136Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetWizardId136List', actionTypeName: 'GET_DDL_WizardId136' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'AppItemCode10', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_AppItemCode10' },
      ]
      this.SearchActions = {
        ...[...this.ScreenDdlDef].reduce((a,v)=>{a['Search' + v.columnName] = this.MakeSearchAction(v); return a;},{}),
        ...[...this.ScreenCriDdlDef].reduce((a,v)=>{a['SearchCri' + v.columnName] = this.MakeSearchAction(v); return a;},{}),
        ...[...this.ScreenOnDemandDef].reduce((a,v)=>{a['Get' + v.columnName] = this.MakeGetColumnOnDemandAction(v); return a;},{}),
      } 
      this.ScreenDdlSelectors = this.ScreenDdlDef.reduce((a,v)=>{a[v.columnName] = this.MakeDdlSelectors(v); return a;},{})
      this.ScreenCriDdlSelectors = this.ScreenCriDdlDef.reduce((a,v)=>{a[v.columnName] = this.MakeCriDdlSelectors(v); return a;},{})
      this.actionReducers = this.MakeActionReducers();
    }
    GetScreenName(){return 'AdmAppItem'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'AppItemId' :  'AppItemId136'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmAppItemService}
    GetReducerActionTypePrefix(){return this.GetScreenName()};
    GetActionType(actionTypeName){return getAsyncTypes(this.GetReducerActionTypePrefix(),actionTypeName)}
    GetInitState(){
      return {
        ...initialRintagiScreenReduxState,
        Label: {
          ...initialRintagiScreenReduxState.Label,
          ...Label, 
        }
      }
      };
    
    GetDefaultDtl(state) { 
      return (state || {}).NewDtl || 
      {
       
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        AppItemId136: copy ? null : mst.AppItemId136,
		
        // CurrencyId64Text: GetCurrencyId64Cd(mst.CurrencyId64, state),
        // MemberId64Text: GetMemberId64Text(mst.MemberId64, state),
        // /* specific app rule */
        // Posted64: copy ? 'N' : mst.Posted64,
        // TrxTotal64: copy ? '0' : mst.TrxTotal64,
      }
    }

ExpandDtl(dtlList, copy) {
                                return dtlList;
                             }
    
    SearchListToSelectList(state) {
        const searchList = ((state || {}).SearchList || {}).data || [];
        return searchList
          .map((v, i) => {
            return {
              key: v.key || null,
              value: v.labelL || v.label || ' ', 
              label: v.labelL || v.label || ' ',
              labelR: v.labelR || ' ',
              // detailR: v.detailR ? GetCurrencyId64Cd(v.detailR, state) : '',
			  detailR: v.detailR,
              detail: v.detail || '',
              idx: i,
              // CurrencyId64: v.detailR,
              isSelected: v.isSelected,
            }
          })
    }
  }

/* ReactRule: Redux Custom Function */

/* ReactRule End: Redux Custom Function */

  /* helper functions */
  // export function GetCurrencyId64Cd(CurrencyId64, state) {
    // try {
      // const d = ((state.ddl.CurrencyId64 || {}) || []).reduce((r, v, i, a) => { r[v.CurrencyId64] = v.CurrencyName; return r; }, {});
      // return (d || {})[CurrencyId64];
    // } catch (e) {
      // return '';
    // }
  // }

  // export function GetMemberId64Text(MemberId64, state) {
    // try {
      // const d = (state.ddl.MemberId64).reduce((r, v, i, a) => { r[v.key] = v.label; return r; }, {});
      // return (d || {})[MemberId64];
    // } catch (e) {
      // return '';
    // }
  // }

  export function ShowMstFilterApplied(state) {
    return !state 
      || !state.ScreenCriteria
//      || (state.ScreenCriteria.MemberId10 || {}).LastCriteria
//      || (state.ScreenCriteria.CustomerJobId20 || {}).LastCriteria
//      || (state.ScreenCriteria.Posted30 ||{}).LastCriteria
|| (state.ScreenCriteria.AppItemCode10 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmAppItemRedux()
            