
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmReportService from '../services/AdmReportService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmReportRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmReport67List',
      'GET_MST': 'GetAdmReport67ById',
      'GET_DTL_LIST': 'GetAdmReport67DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'ReportTypeCd22', payloadDdlName: 'ReportTypeCd22List', keyName: 'ReportTypeCd22', labelName: 'ReportTypeCd22Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetReportTypeCd22List', actionTypeName: 'GET_DDL_ReportTypeCd22' },
      { columnName: 'OrientationCd22', payloadDdlName: 'OrientationCd22List', keyName: 'OrientationCd22', labelName: 'OrientationCd22Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetOrientationCd22List', actionTypeName: 'GET_DDL_OrientationCd22' },
      { columnName: 'CopyReportId22', payloadDdlName: 'CopyReportId22List', keyName: 'CopyReportId22', labelName: 'CopyReportId22Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetCopyReportId22List', actionTypeName: 'GET_DDL_CopyReportId22' },
      { columnName: 'ModifiedBy22', payloadDdlName: 'ModifiedBy22List', keyName: 'ModifiedBy22', labelName: 'ModifiedBy22Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetModifiedBy22List', actionTypeName: 'GET_DDL_ModifiedBy22' },
      { columnName: 'UnitCd22', payloadDdlName: 'UnitCd22List', keyName: 'UnitCd22', labelName: 'UnitCd22Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetUnitCd22List', actionTypeName: 'GET_DDL_UnitCd22' },
      { columnName: 'CultureId96', payloadDdlName: 'CultureId96List', keyName: 'CultureId96', labelName: 'CultureId96Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetCultureId96List', actionTypeName: 'GET_DDL_CultureId96' },
    ]
    this.ScreenOnDemandDef = [
      { columnName: 'RptTemplate22', tableColumnName: 'RptTemplate', type: 'DocList', forMst: true, apiServiceName: 'GetRptTemplate22List', actionTypeName: 'GET_COLUMN_RptTemplate22' },
    ]
    this.ScreenDocumentDef = [
      { columnName: 'RptTemplate22', tableColumnName: 'RptTemplate', type: 'Get', forMst: true, apiServiceName: 'GetDoc', actionTypeName: 'GET_COLUMN_CONTENT_RptTemplate22' },
      { columnName: 'RptTemplate22', tableColumnName: 'RptTemplate', type: 'Add', forMst: true, apiServiceName: 'SaveRptTemplate22', actionTypeName: 'ADD_COLUMN_CONTENT_RptTemplate22' },
      { columnName: 'RptTemplate22', tableColumnName: 'RptTemplate', type: 'Del', forMst: true, apiServiceName: 'DelRptTemplate22', actionTypeName: 'DEL_COLUMN_CONTENT_RptTemplate22' },
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
  GetScreenName() { return 'AdmReport' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ReportId' : 'ReportId22'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ReportHlpId' : 'ReportHlpId96'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmReportService; }
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
      ReportHlpId96: null,
      CultureId96: null,
      DefaultHlpMsg96: null,
      ReportTitle96: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      ReportId22: copy ? null : mst.ReportId22,
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
          ReportId22: null,
          ReportHlpId96: null,
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

export default new AdmReportRedux()
