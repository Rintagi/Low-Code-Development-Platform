
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmServerRuleOvrdService from '../services/AdmServerRuleOvrdService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmServerRuleOvrdRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmServerRuleOvrd1026List',
      'GET_MST': 'GetAdmServerRuleOvrd1026ById',
      'GET_DTL_LIST': 'GetAdmServerRuleOvrd1026DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'ServerRuleId1322', payloadDdlName: 'ServerRuleId1322List', keyName: 'ServerRuleId1322', labelName: 'ServerRuleId1322Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetServerRuleId1322List', actionTypeName: 'GET_DDL_ServerRuleId1322' },
      { columnName: 'ScreenId1322', payloadDdlName: 'ScreenId1322List', keyName: 'ScreenId1322', labelName: 'ScreenId1322Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId1322List', actionTypeName: 'GET_DDL_ScreenId1322' },
      { columnName: 'RunMode1322', payloadDdlName: 'RunMode1322List', keyName: 'RunMode1322', labelName: 'RunMode1322Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetRunMode1322List', actionTypeName: 'GET_DDL_RunMode1322' },
      { columnName: 'PermKeyId1321', payloadDdlName: 'PermKeyId1321List', keyName: 'PermKeyId1321', labelName: 'PermKeyId1321Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetPermKeyId1321List', actionTypeName: 'GET_DDL_PermKeyId1321' },
      { columnName: 'PermKeyRowId1321', payloadDdlName: 'PermKeyRowId1321List', keyName: 'PermKeyRowId1321', labelName: 'PermKeyRowId1321Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetPermKeyRowId1321List', filterByMaster: true, filterByColumnName: 'PermKeyId1321', actionTypeName: 'GET_DDL_PermKeyRowId1321' },
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
  GetScreenName() { return 'AdmServerRuleOvrd' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'AtServerRuleOvrdId' : 'AtServerRuleOvrdId1322'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ServerRuledOvrdPrmId' : 'ServerRuledOvrdPrmId1321'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmServerRuleOvrdService; }
  GetReducerActionTypePrefix() { return this.GetScreenName(); };
  GetActionType(actionTypeName) { return getAsyncTypes(this.GetReducerActionTypePrefix(), actionTypeName); }
  GetInitState() {
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
      ServerRuledOvrdPrmId1321: null,
      PermKeyId1321: null,
      PermKeyRowId1321: null,
      AndCondition1321: null,
      Match1321: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      AtServerRuleOvrdId1322: copy ? null : mst.AtServerRuleOvrdId1322,
    }
  }
  ExpandDtl(dtlList, copy) {
    if (!copy) return dtlList;
    else if (!this.allowTmpDtl) return [];
    else {
      const now = Date.now();
      return dtlList.map((v, i) => {
        return {
          ...v,
          AtServerRuleOvrdId1322: null,
          ServerRuledOvrdPrmId1321: null,
          TmpKeyId: now + i,
        }
      })
    };
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

export default new AdmServerRuleOvrdRedux()
