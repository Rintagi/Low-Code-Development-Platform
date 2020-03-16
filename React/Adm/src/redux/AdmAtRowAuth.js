
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmAtRowAuthService from '../services/AdmAtRowAuthService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmAtRowAuthRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmAtRowAuth22List',
      'GET_MST': 'GetAdmAtRowAuth22ById',
      'GET_DTL_LIST': 'GetAdmAtRowAuth22DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'OvrideId236', payloadDdlName: 'OvrideId236List', keyName: 'OvrideId236', labelName: 'OvrideId236Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetOvrideId236List', actionTypeName: 'GET_DDL_OvrideId236' },
      { columnName: 'AllowSel236', payloadDdlName: 'AllowSel236List', keyName: 'AllowSel236', labelName: 'AllowSel236Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetAllowSel236List', actionTypeName: 'GET_DDL_AllowSel236' },
      { columnName: 'PermKeyId237', payloadDdlName: 'PermKeyId237List', keyName: 'PermKeyId237', labelName: 'PermKeyId237Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetPermKeyId237List', actionTypeName: 'GET_DDL_PermKeyId237' },
      { columnName: 'SelLevel237', payloadDdlName: 'SelLevel237List', keyName: 'SelLevel237', labelName: 'SelLevel237Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetSelLevel237List', actionTypeName: 'GET_DDL_SelLevel237' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [

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
  GetScreenName() { return 'AdmAtRowAuth' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RowAuthId' : 'RowAuthId236'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RowAuthPrmId' : 'RowAuthPrmId237'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmAtRowAuthService; }
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
      RowAuthPrmId237: null,
      PermKeyId237: null,
      SelLevel237: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      RowAuthId236: copy ? null : mst.RowAuthId236,
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
          RowAuthId236: null,
          RowAuthPrmId237: null,
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
          detailR: v.detailR,
          detail: v.detail || '',
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

    || state.ScreenCriteria.SearchStr;
}

export default new AdmAtRowAuthRedux()
