
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmClientRuleService from '../services/AdmClientRuleService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmClientRuleRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmClientRule79List',
        'GET_MST' : 'GetAdmClientRule79ById',
        'GET_DTL_LIST' : 'GetAdmClientRule79DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'RuleMethodId127', payloadDdlName:'RuleMethodId127List', keyName:'RuleMethodId127',labelName:'RuleMethodId127Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRuleMethodId127List', actionTypeName: 'GET_DDL_RuleMethodId127' },
{ columnName: 'RuleTypeId127', payloadDdlName:'RuleTypeId127List', keyName:'RuleTypeId127',labelName:'RuleTypeId127Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRuleTypeId127List', actionTypeName: 'GET_DDL_RuleTypeId127' },
{ columnName: 'ScreenId127', payloadDdlName:'ScreenId127List', keyName:'ScreenId127',labelName:'ScreenId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenId127List', actionTypeName: 'GET_DDL_ScreenId127' },
{ columnName: 'ReportId127', payloadDdlName:'ReportId127List', keyName:'ReportId127',labelName:'ReportId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId127List', actionTypeName: 'GET_DDL_ReportId127' },
{ columnName: 'CultureId127', payloadDdlName:'CultureId127List', keyName:'CultureId127',labelName:'CultureId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId127List', actionTypeName: 'GET_DDL_CultureId127' },
{ columnName: 'ScreenObjHlpId127', payloadDdlName:'ScreenObjHlpId127List', keyName:'ScreenObjHlpId127',labelName:'ScreenObjHlpId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenObjHlpId127List', filterByMaster:true, filterByColumnName:'ScreenId127',actionTypeName: 'GET_DDL_ScreenObjHlpId127' },
{ columnName: 'ScreenCriHlpId127', payloadDdlName:'ScreenCriHlpId127List', keyName:'ScreenCriHlpId127',labelName:'ScreenCriHlpId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenCriHlpId127List', filterByMaster:true, filterByColumnName:'ScreenId127',actionTypeName: 'GET_DDL_ScreenCriHlpId127' },
{ columnName: 'ReportCriHlpId127', payloadDdlName:'ReportCriHlpId127List', keyName:'ReportCriHlpId127',labelName:'ReportCriHlpId127Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportCriHlpId127List', filterByMaster:true, filterByColumnName:'ReportId127',actionTypeName: 'GET_DDL_ReportCriHlpId127' },
{ columnName: 'RuleCntTypeId127', payloadDdlName:'RuleCntTypeId127List', keyName:'RuleCntTypeId127',labelName:'RuleCntTypeId127Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRuleCntTypeId127List', actionTypeName: 'GET_DDL_RuleCntTypeId127' },
{ columnName: 'ClientScript127', payloadDdlName:'ClientScript127List', keyName:'ClientScript127',labelName:'ClientScript127Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetClientScript127List', actionTypeName: 'GET_DDL_ClientScript127' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ScreenId10', payloadDdlName: 'ScreenId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriScreenId10List', actionTypeName: 'GET_DDL_CRIScreenId10' },
{ columnName: 'ReportId20', payloadDdlName: 'ReportId20List', isAutoComplete: true, apiServiceName: 'GetScreenCriReportId20List', actionTypeName: 'GET_DDL_CRIReportId20' },
{ columnName: 'CultureId30', payloadDdlName: 'CultureId30List', isAutoComplete: true, apiServiceName: 'GetScreenCriCultureId30List', actionTypeName: 'GET_DDL_CRICultureId30' },
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
    GetScreenName(){return 'AdmClientRule'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ClientRuleId' :  'ClientRuleId127'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmClientRuleService}
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
        ClientRuleId127: copy ? null : mst.ClientRuleId127,
		
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
|| (state.ScreenCriteria.ScreenId10 || {}).LastCriteria
|| (state.ScreenCriteria.ReportId20 || {}).LastCriteria
|| (state.ScreenCriteria.CultureId30 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmClientRuleRedux()
            