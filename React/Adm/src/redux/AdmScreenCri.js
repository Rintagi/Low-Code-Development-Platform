
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmScreenCriService from '../services/AdmScreenCriService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmScreenCriRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmScreenCri73List',
      'GET_MST': 'GetAdmScreenCri73ById',
      'GET_DTL_LIST': 'GetAdmScreenCri73DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'ScreenId104', payloadDdlName: 'ScreenId104List', keyName: 'ScreenId104', labelName: 'ScreenId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId104List', actionTypeName: 'GET_DDL_ScreenId104' },
      { columnName: 'ColumnId104', payloadDdlName: 'ColumnId104List', keyName: 'ColumnId104', labelName: 'ColumnId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetColumnId104List', actionTypeName: 'GET_DDL_ColumnId104' },
      { columnName: 'OperatorId104', payloadDdlName: 'OperatorId104List', keyName: 'OperatorId104', labelName: 'OperatorId104Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetOperatorId104List', actionTypeName: 'GET_DDL_OperatorId104' },
      { columnName: 'DisplayModeId104', payloadDdlName: 'DisplayModeId104List', keyName: 'DisplayModeId104', labelName: 'DisplayModeId104Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetDisplayModeId104List', actionTypeName: 'GET_DDL_DisplayModeId104' },
      { columnName: 'ColumnJustify104', payloadDdlName: 'ColumnJustify104List', keyName: 'ColumnJustify104', labelName: 'ColumnJustify104Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetColumnJustify104List', actionTypeName: 'GET_DDL_ColumnJustify104' },
      { columnName: 'DdlKeyColumnId104', payloadDdlName: 'DdlKeyColumnId104List', keyName: 'DdlKeyColumnId104', labelName: 'DdlKeyColumnId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlKeyColumnId104List', actionTypeName: 'GET_DDL_DdlKeyColumnId104' },
      { columnName: 'DdlRefColumnId104', payloadDdlName: 'DdlRefColumnId104List', keyName: 'DdlRefColumnId104', labelName: 'DdlRefColumnId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlRefColumnId104List', actionTypeName: 'GET_DDL_DdlRefColumnId104' },
      { columnName: 'DdlSrtColumnId104', payloadDdlName: 'DdlSrtColumnId104List', keyName: 'DdlSrtColumnId104', labelName: 'DdlSrtColumnId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlSrtColumnId104List', actionTypeName: 'GET_DDL_DdlSrtColumnId104' },
      { columnName: 'DdlFtrColumnId104', payloadDdlName: 'DdlFtrColumnId104List', keyName: 'DdlFtrColumnId104', labelName: 'DdlFtrColumnId104Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlFtrColumnId104List', filterByMaster: true, filterByColumnName: 'ScreenId104', actionTypeName: 'GET_DDL_DdlFtrColumnId104' },
      { columnName: 'CultureId105', payloadDdlName: 'CultureId105List', keyName: 'CultureId105', labelName: 'CultureId105Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetCultureId105List', actionTypeName: 'GET_DDL_CultureId105' },
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
  GetScreenName() { return 'AdmScreenCri' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ScreenCriId' : 'ScreenCriId104'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ScreenCriHlpId' : 'ScreenCriHlpId105'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmScreenCriService; }
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
      ScreenCriHlpId105: null,
      CultureId105: null,
      ColumnHeader105: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      ScreenCriId104: copy ? null : mst.ScreenCriId104,
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
          ScreenCriId104: null,
          ScreenCriHlpId105: null,
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

export default new AdmScreenCriRedux()
