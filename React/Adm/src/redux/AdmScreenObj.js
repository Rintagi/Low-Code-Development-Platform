
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmScreenObjService from '../services/AdmScreenObjService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmScreenObjRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmScreenObj10List',
      'GET_MST': 'GetAdmScreenObj10ById',
      'GET_DTL_LIST': 'GetAdmScreenObj10DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'GridGrpCd14', payloadDdlName: 'GridGrpCd14List', keyName: 'GridGrpCd14', labelName: 'GridGrpCd14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetGridGrpCd14List', actionTypeName: 'GET_DDL_GridGrpCd14' },
      { columnName: 'ColumnJustify14', payloadDdlName: 'ColumnJustify14List', keyName: 'ColumnJustify14', labelName: 'ColumnJustify14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetColumnJustify14List', actionTypeName: 'GET_DDL_ColumnJustify14' },
      { columnName: 'ScreenId14', payloadDdlName: 'ScreenId14List', keyName: 'ScreenId14', labelName: 'ScreenId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId14List', actionTypeName: 'GET_DDL_ScreenId14' },
      { columnName: 'GroupRowId14', payloadDdlName: 'GroupRowId14List', keyName: 'GroupRowId14', labelName: 'GroupRowId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetGroupRowId14List', actionTypeName: 'GET_DDL_GroupRowId14' },
      { columnName: 'GroupColId14', payloadDdlName: 'GroupColId14List', keyName: 'GroupColId14', labelName: 'GroupColId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetGroupColId14List', actionTypeName: 'GET_DDL_GroupColId14' },
      { columnName: 'ColumnId14', payloadDdlName: 'ColumnId14List', keyName: 'ColumnId14', labelName: 'ColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetColumnId14List', actionTypeName: 'GET_DDL_ColumnId14' },
      { columnName: 'DisplayModeId14', payloadDdlName: 'DisplayModeId14List', keyName: 'DisplayModeId14', labelName: 'DisplayModeId14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetDisplayModeId14List', actionTypeName: 'GET_DDL_DisplayModeId14' },
      { columnName: 'DdlKeyColumnId14', payloadDdlName: 'DdlKeyColumnId14List', keyName: 'DdlKeyColumnId14', labelName: 'DdlKeyColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlKeyColumnId14List', actionTypeName: 'GET_DDL_DdlKeyColumnId14' },
      { columnName: 'DdlRefColumnId14', payloadDdlName: 'DdlRefColumnId14List', keyName: 'DdlRefColumnId14', labelName: 'DdlRefColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlRefColumnId14List', actionTypeName: 'GET_DDL_DdlRefColumnId14' },
      { columnName: 'DdlSrtColumnId14', payloadDdlName: 'DdlSrtColumnId14List', keyName: 'DdlSrtColumnId14', labelName: 'DdlSrtColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlSrtColumnId14List', actionTypeName: 'GET_DDL_DdlSrtColumnId14' },
      { columnName: 'DdlAdnColumnId14', payloadDdlName: 'DdlAdnColumnId14List', keyName: 'DdlAdnColumnId14', labelName: 'DdlAdnColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlAdnColumnId14List', actionTypeName: 'GET_DDL_DdlAdnColumnId14' },
      { columnName: 'DdlFtrColumnId14', payloadDdlName: 'DdlFtrColumnId14List', keyName: 'DdlFtrColumnId14', labelName: 'DdlFtrColumnId14Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetDdlFtrColumnId14List', actionTypeName: 'GET_DDL_DdlFtrColumnId14' },
      { columnName: 'DtlLstPosId14', payloadDdlName: 'DtlLstPosId14List', keyName: 'DtlLstPosId14', labelName: 'DtlLstPosId14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetDtlLstPosId14List', actionTypeName: 'GET_DDL_DtlLstPosId14' },
      { columnName: 'AggregateCd14', payloadDdlName: 'AggregateCd14List', keyName: 'AggregateCd14', labelName: 'AggregateCd14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetAggregateCd14List', actionTypeName: 'GET_DDL_AggregateCd14' },
      { columnName: 'MatchCd14', payloadDdlName: 'MatchCd14List', keyName: 'MatchCd14', labelName: 'MatchCd14Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetMatchCd14List', actionTypeName: 'GET_DDL_MatchCd14' },
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
  GetScreenName() { return 'AdmScreenObj' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ScreenObjId' : 'ScreenObjId14'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmScreenObjService; }
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
      ScreenObjId14: copy ? null : mst.ScreenObjId14,
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

    || state.ScreenCriteria.SearchStr;
}

export default new AdmScreenObjRedux()
