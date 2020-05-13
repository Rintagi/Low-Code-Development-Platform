
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmDataCatService from '../services/AdmDataCatService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmDataCatRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmDataCat96List',
      'GET_MST': 'GetAdmDataCat96ById',
      'GET_DTL_LIST': 'GetAdmDataCat96DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'RptwizTypId181', payloadDdlName: 'RptwizTypId181List', keyName: 'RptwizTypId181', labelName: 'RptwizTypId181Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetRptwizTypId181List', actionTypeName: 'GET_DDL_RptwizTypId181' },
      { columnName: 'TableId181', payloadDdlName: 'TableId181List', keyName: 'TableId181', labelName: 'TableId181Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetTableId181List', actionTypeName: 'GET_DDL_TableId181' },
      { columnName: 'ColumnId182', payloadDdlName: 'ColumnId182List', keyName: 'ColumnId182', labelName: 'ColumnId182Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetColumnId182List', filterByMaster: true, filterByColumnName: 'TableId181', actionTypeName: 'GET_DDL_ColumnId182' },
      { columnName: 'DisplayModeId182', payloadDdlName: 'DisplayModeId182List', keyName: 'DisplayModeId182', labelName: 'DisplayModeId182Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetDisplayModeId182List', actionTypeName: 'GET_DDL_DisplayModeId182' },
    ]
    this.ScreenOnDemandDef = [
      { columnName: 'SampleImage181', isFileObject: true, tableColumnName: 'SampleImage', forMst: true, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SampleImage181' },
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
  GetScreenName() { return 'AdmDataCat' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RptwizCatId' : 'RptwizCatId181'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RptwizCatDtlId' : 'RptwizCatDtlId182'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmDataCatService; }
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
      RptwizCatDtlId182: null,
      ColumnId182: null,
      DisplayModeId182: null,
      ColumnSize182: null,
      RowSize182: null,
      DdlKeyColNm182: null,
      DdlRefColNm182: null,
      RegClause182: null,
      StoredProc182: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      RptwizCatId181: copy ? null : mst.RptwizCatId181,
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
          RptwizCatId181: null,
          RptwizCatDtlId182: null,
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

export default new AdmDataCatRedux()
