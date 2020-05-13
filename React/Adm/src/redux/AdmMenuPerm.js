
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmMenuPermService from '../services/AdmMenuPermService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmMenuPermRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmMenuPerm58List',
      'GET_MST': 'GetAdmMenuPerm58ById',
      'GET_DTL_LIST': 'GetAdmMenuPerm58DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'GrantDeny231', payloadDdlName: 'GrantDeny231List', keyName: 'GrantDeny231', labelName: 'GrantDeny231Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetGrantDeny231List', actionTypeName: 'GET_DDL_GrantDeny231' },
      { columnName: 'PermKeyId231', payloadDdlName: 'PermKeyId231List', keyName: 'PermKeyId231', labelName: 'PermKeyId231Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetPermKeyId231List', actionTypeName: 'GET_DDL_PermKeyId231' },
      { columnName: 'PermId231', payloadDdlName: 'PermId231List', keyName: 'PermId231', labelName: 'PermId231Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetPermId231List', filterByMaster: true, filterByColumnName: 'PermKeyId231', actionTypeName: 'GET_DDL_PermId231' },
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
  GetScreenName() { return 'AdmMenuPerm' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MenuId' : 'MenuId39'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MenuPrmId' : 'MenuPrmId231'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmMenuPermService; }
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
      MenuPrmId231: null,
      GrantDeny231: null,
      PermKeyId231: null,
      PermId231: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      MenuId39: copy ? null : mst.MenuId39,
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
          MenuId39: null,
          MenuPrmId231: null,
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

    || state.ScreenCriteria.SearchStr;
}

export default new AdmMenuPermRedux()
