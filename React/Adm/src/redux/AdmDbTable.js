
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmDbTableService from '../services/AdmDbTableService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmDbTableRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmDbTable2List',
      'GET_MST': 'GetAdmDbTable2ById',
      'GET_DTL_LIST': 'GetAdmDbTable2DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'SystemId3', payloadDdlName: 'SystemId3List', keyName: 'SystemId3', labelName: 'SystemId3Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetSystemId3List', actionTypeName: 'GET_DDL_SystemId3' },
      { columnName: 'SheetNameList', payloadDdlName: 'SheetNameListList', keyName: 'SheetNameList', labelName: 'SheetNameListText', forMst: true, isAutoComplete: false, apiServiceName: 'GetSheetNameListList', actionTypeName: 'GET_DDL_SheetNameList' },
      { columnName: 'ModifiedBy3', payloadDdlName: 'ModifiedBy3List', keyName: 'ModifiedBy3', labelName: 'ModifiedBy3Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetModifiedBy3List', actionTypeName: 'GET_DDL_ModifiedBy3' },
      { columnName: 'DataType5', payloadDdlName: 'DataType5List', keyName: 'DataType5', labelName: 'DataType5Text', forMst: false, isAutoComplete: false, apiServiceName: 'GetDataType5List', actionTypeName: 'GET_DDL_DataType5' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'TableName10', payloadDdlName: '', keyName: '', labelName: '', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_TableName10' },
      { columnName: 'TableDesc20', payloadDdlName: '', keyName: '', labelName: '', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_TableDesc20' },
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
  GetScreenName() { return 'AdmDbTable' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'TableId' : 'TableId3'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ColumnId' : 'ColumnId5'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmDbTableService; }
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
      ColumnId5: null,
      ColumnIndex5: null,
      ExternalTable5: null,
      ColumnName5: null,
      DataType5: null,
      ColumnLength5: null,
      ColumnScale5: null,
      DefaultValue5: null,
      AllowNulls5: null,
      ColumnIdentity5: null,
      PrimaryKey5: null,
      IsIndexU5: null,
      IsIndex5: null,
      ColObjective5: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      TableId3: copy ? null : mst.TableId3,
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
          TableId3: null,
          ColumnId5: null,
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
    || (state.ScreenCriteria.TableName10 || {}).LastCriteria
    || (state.ScreenCriteria.TableDesc20 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmDbTableRedux()
