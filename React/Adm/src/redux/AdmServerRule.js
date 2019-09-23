
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmServerRuleService from '../services/AdmServerRuleService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmServerRuleRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmServerRule14List',
        'GET_MST' : 'GetAdmServerRule14ById',
        'GET_DTL_LIST' : 'GetAdmServerRule14DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'RuleTypeId24', payloadDdlName:'RuleTypeId24List', keyName:'RuleTypeId24',labelName:'RuleTypeId24Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRuleTypeId24List', actionTypeName: 'GET_DDL_RuleTypeId24' },
{ columnName: 'ScreenId24', payloadDdlName:'ScreenId24List', keyName:'ScreenId24',labelName:'ScreenId24Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenId24List', actionTypeName: 'GET_DDL_ScreenId24' },
{ columnName: 'BeforeCRUD24', payloadDdlName:'BeforeCRUD24List', keyName:'BeforeCRUD24',labelName:'BeforeCRUD24Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetBeforeCRUD24List', actionTypeName: 'GET_DDL_BeforeCRUD24' },
{ columnName: 'ModifiedBy24', payloadDdlName:'ModifiedBy24List', keyName:'ModifiedBy24',labelName:'ModifiedBy24Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetModifiedBy24List', actionTypeName: 'GET_DDL_ModifiedBy24' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'SyncByDb', tableColumnName: 'SyncByDb', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SyncByDb' },
{ columnName: 'SyncToDb', tableColumnName: 'SyncToDb', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SyncToDb' },
//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ScreenId10', payloadDdlName: 'ScreenId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriScreenId10List', actionTypeName: 'GET_DDL_CRIScreenId10' },
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
    GetScreenName(){return 'AdmServerRule'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ServerRuleId' :  'ServerRuleId24'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmServerRuleService}
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
        ServerRuleId24: copy ? null : mst.ServerRuleId24,
		
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
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmServerRuleRedux()
            