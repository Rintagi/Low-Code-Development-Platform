
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmWebRuleService from '../services/AdmWebRuleService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmWebRuleRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmWebRule80List',
      'GET_MST': 'GetAdmWebRule80ById',
      'GET_DTL_LIST': 'GetAdmWebRule80DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'RuleTypeId128', payloadDdlName: 'RuleTypeId128List', keyName: 'RuleTypeId128', labelName: 'RuleTypeId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetRuleTypeId128List', actionTypeName: 'GET_DDL_RuleTypeId128' },
      { columnName: 'ScreenId128', payloadDdlName: 'ScreenId128List', keyName: 'ScreenId128', labelName: 'ScreenId128Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId128List', actionTypeName: 'GET_DDL_ScreenId128' },
      { columnName: 'ScreenObjId128', payloadDdlName: 'ScreenObjId128List', keyName: 'ScreenObjId128', labelName: 'ScreenObjId128Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenObjId128List', filterByMaster: true, filterByColumnName: 'ScreenId128', actionTypeName: 'GET_DDL_ScreenObjId128' },
      { columnName: 'ButtonTypeId128', payloadDdlName: 'ButtonTypeId128List', keyName: 'ButtonTypeId128', labelName: 'ButtonTypeId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetButtonTypeId128List', actionTypeName: 'GET_DDL_ButtonTypeId128' },
      { columnName: 'EventId128', payloadDdlName: 'EventId128List', keyName: 'EventId128', labelName: 'EventId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetEventId128List', actionTypeName: 'GET_DDL_EventId128' },
      { columnName: 'ForCompanyId128', payloadDdlName: 'ForCompanyId128List', keyName: 'ForCompanyId128', labelName: 'ForCompanyId128Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetForCompanyId128List', actionTypeName: 'GET_DDL_ForCompanyId128' },
      { columnName: 'ReactEventId128', payloadDdlName: 'ReactEventId128List', keyName: 'ReactEventId128', labelName: 'ReactEventId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetReactEventId128List', actionTypeName: 'GET_DDL_ReactEventId128' },
      { columnName: 'ReduxEventId128', payloadDdlName: 'ReduxEventId128List', keyName: 'ReduxEventId128', labelName: 'ReduxEventId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetReduxEventId128List', actionTypeName: 'GET_DDL_ReduxEventId128' },
      { columnName: 'ServiceEventId128', payloadDdlName: 'ServiceEventId128List', keyName: 'ServiceEventId128', labelName: 'ServiceEventId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetServiceEventId128List', actionTypeName: 'GET_DDL_ServiceEventId128' },
      { columnName: 'AsmxEventId128', payloadDdlName: 'AsmxEventId128List', keyName: 'AsmxEventId128', labelName: 'AsmxEventId128Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetAsmxEventId128List', actionTypeName: 'GET_DDL_AsmxEventId128' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'ScreenId10', payloadDdlName: 'ScreenId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriScreenId10List', actionTypeName: 'GET_DDL_CRIScreenId10' },
    ]
    this.SearchActions = {
      ...[...this.ScreenDdlDef].reduce((a, v) => { a['Search' + v.columnName] = this.MakeSearchAction(v); return a; }, {}),
      ...[...this.ScreenCriDdlDef].reduce((a, v) => { a['SearchCri' + v.columnName] = this.MakeSearchAction(v); return a; }, {}),
      ...[...this.ScreenOnDemandDef].filter(f => f.type !== 'DocList' && f.type !== 'RefColumn').reduce((a, v) => { a['Get' + v.columnName] = this.MakeGetColumnOnDemandAction(v); return a; }, {}),
      ...[...this.ScreenOnDemandDef].filter(f => f.type === 'RefColumn').reduce((a, v) => { a['Get' + v.columnName] = this.MakeGetRefColumnOnDemandAction(v); return a; }, {}),
      ...this.MakePullUpOnDemandAction([...this.ScreenOnDemandDef].filter(f => f.type === 'RefColumn').reduce((a, v) => { a['GetRef' + v.refColumnName] = { dependents: [...((a['GetRef' + v.refColumnName] || {}).dependents || []), v] }; return a; }, {})),
      ...[...this.ScreenOnDemandDef].filter(f => f.type === 'DocList').reduce((a, v) => { a['Get' + v.columnName] = this.MakeGetDocumentListAction(v); return a; }, {}),
    }
    this.OnDemandActions = {
      ...[...this.ScreenDocumentDef].filter(f => f.type === 'Get').reduce((a, v) => { a['Get' + v.columnName + 'Content'] = this.MakeGetDocumentContentAction(v); return a; }, {}),
      ...[...this.ScreenDocumentDef].filter(f => f.type === 'Add').reduce((a, v) => { a['Add' + v.columnName + 'Content'] = this.MakeAddDocumentContentAction(v); return a; }, {}),
      ...[...this.ScreenDocumentDef].filter(f => f.type === 'Del').reduce((a, v) => { a['Del' + v.columnName + 'Content'] = this.MakeDelDocumentContentAction(v); return a; }, {}),
    }
    this.ScreenDdlSelectors = this.ScreenDdlDef.reduce((a, v) => { a[v.columnName] = this.MakeDdlSelectors(v); return a; }, {})
    this.ScreenCriDdlSelectors = this.ScreenCriDdlDef.reduce((a, v) => { a[v.columnName] = this.MakeCriDdlSelectors(v); return a; }, {})
    this.actionReducers = this.MakeActionReducers();
  }
  GetScreenName() { return 'AdmWebRule' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'WebRuleId' : 'WebRuleId128'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmWebRuleService; }
  GetReducerActionTypePrefix() { return this.GetScreenName(); };
  GetActionType(actionTypeName) { return getAsyncTypes(this.GetReducerActionTypePrefix(), actionTypeName); }
  GetInitState() {
    return {
      ...initialRintagiScreenReduxState,
      Label: {
        ...initialRintagiScreenReduxState.Label,
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
          detailR: v.detailR || ' ',
          detail: v.detail || ' ',
          idx: i,
          isSelected: v.isSelected,
        }
      })
  }
}

/* ReactRule: Redux Custom Function */

/* ReactRule End: Redux Custom Function */

/* helper functions */

export function ShowMstFilterApplied(state) {
  return !state
    || !state.ScreenCriteria
    || (state.ScreenCriteria.ScreenId10 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmWebRuleRedux()
