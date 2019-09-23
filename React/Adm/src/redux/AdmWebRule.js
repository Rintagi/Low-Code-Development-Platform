
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmWebRuleService from '../services/AdmWebRuleService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmWebRuleRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmWebRule80List',
        'GET_MST' : 'GetAdmWebRule80ById',
        'GET_DTL_LIST' : 'GetAdmWebRule80DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'RuleTypeId128', payloadDdlName:'RuleTypeId128List', keyName:'RuleTypeId128',labelName:'RuleTypeId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRuleTypeId128List', actionTypeName: 'GET_DDL_RuleTypeId128' },
{ columnName: 'ScreenId128', payloadDdlName:'ScreenId128List', keyName:'ScreenId128',labelName:'ScreenId128Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenId128List', actionTypeName: 'GET_DDL_ScreenId128' },
{ columnName: 'ScreenObjId128', payloadDdlName:'ScreenObjId128List', keyName:'ScreenObjId128',labelName:'ScreenObjId128Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenObjId128List', filterByMaster:true, filterByColumnName:'ScreenId128',actionTypeName: 'GET_DDL_ScreenObjId128' },
{ columnName: 'ButtonTypeId128', payloadDdlName:'ButtonTypeId128List', keyName:'ButtonTypeId128',labelName:'ButtonTypeId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetButtonTypeId128List', actionTypeName: 'GET_DDL_ButtonTypeId128' },
{ columnName: 'EventId128', payloadDdlName:'EventId128List', keyName:'EventId128',labelName:'EventId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetEventId128List', actionTypeName: 'GET_DDL_EventId128' },
{ columnName: 'ReactEventId128', payloadDdlName:'ReactEventId128List', keyName:'ReactEventId128',labelName:'ReactEventId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetReactEventId128List', actionTypeName: 'GET_DDL_ReactEventId128' },
{ columnName: 'ReduxEventId128', payloadDdlName:'ReduxEventId128List', keyName:'ReduxEventId128',labelName:'ReduxEventId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetReduxEventId128List', actionTypeName: 'GET_DDL_ReduxEventId128' },
{ columnName: 'ServiceEventId128', payloadDdlName:'ServiceEventId128List', keyName:'ServiceEventId128',labelName:'ServiceEventId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetServiceEventId128List', actionTypeName: 'GET_DDL_ServiceEventId128' },
{ columnName: 'AsmxEventId128', payloadDdlName:'AsmxEventId128List', keyName:'AsmxEventId128',labelName:'AsmxEventId128Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetAsmxEventId128List', actionTypeName: 'GET_DDL_AsmxEventId128' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'Snippet1', tableColumnName: 'Snippet1', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_Snippet1' },
{ columnName: 'Snippet4', tableColumnName: 'Snippet4', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_Snippet4' },
{ columnName: 'Snippet2', tableColumnName: 'Snippet2', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_Snippet2' },
{ columnName: 'Snippet3', tableColumnName: 'Snippet3', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_Snippet3' },
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
    GetScreenName(){return 'AdmWebRule'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'WebRuleId' :  'WebRuleId128'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmWebRuleService}
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
        WebRuleId128: copy ? null : mst.WebRuleId128,
		
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

  export default new AdmWebRuleRedux()
            