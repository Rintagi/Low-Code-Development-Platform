
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmScrAuditService from '../services/AdmScrAuditService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmScrAuditRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmScrAudit1019List',
      'GET_MST': 'GetAdmScrAudit1019ById',
      'GET_DTL_LIST': 'GetAdmScrAudit1019DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'CudAction1300', payloadDdlName: 'CudAction1300List', keyName: 'CudAction1300', labelName: 'CudAction1300Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetCudAction1300List', actionTypeName: 'GET_DDL_CudAction1300' },
      { columnName: 'ScreenId1300', payloadDdlName: 'ScreenId1300List', keyName: 'ScreenId1300', labelName: 'ScreenId1300Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId1300List', actionTypeName: 'GET_DDL_ScreenId1300' },
      { columnName: 'TableId1300', payloadDdlName: 'TableId1300List', keyName: 'TableId1300', labelName: 'TableId1300Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetTableId1300List', actionTypeName: 'GET_DDL_TableId1300' },
      { columnName: 'ChangedBy1300', payloadDdlName: 'ChangedBy1300List', keyName: 'ChangedBy1300', labelName: 'ChangedBy1300Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetChangedBy1300List', actionTypeName: 'GET_DDL_ChangedBy1300' },
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
  GetScreenName() { return 'AdmScrAudit' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ScrAuditId' : 'ScrAuditId1300'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmScrAuditService; }
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
      ScrAuditId1300: copy ? null : mst.ScrAuditId1300,
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

export default new AdmScrAuditRedux()
