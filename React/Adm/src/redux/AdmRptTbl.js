
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmRptTblService from '../services/AdmRptTblService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmRptTblRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmRptTbl92List',
      'GET_MST': 'GetAdmRptTbl92ById',
      'GET_DTL_LIST': 'GetAdmRptTbl92DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'RptCtrId162', payloadDdlName: 'RptCtrId162List', keyName: 'RptCtrId162', labelName: 'RptCtrId162Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetRptCtrId162List', actionTypeName: 'GET_DDL_RptCtrId162' },
      { columnName: 'ParentId162', payloadDdlName: 'ParentId162List', keyName: 'ParentId162', labelName: 'ParentId162Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetParentId162List', actionTypeName: 'GET_DDL_ParentId162' },
      { columnName: 'ReportId162', payloadDdlName: 'ReportId162List', keyName: 'ReportId162', labelName: 'ReportId162Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetReportId162List', actionTypeName: 'GET_DDL_ReportId162' },
      { columnName: 'TblToggle162', payloadDdlName: 'TblToggle162List', keyName: 'TblToggle162', labelName: 'TblToggle162Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetTblToggle162List', filterByMaster: true, filterByColumnName: 'ReportId162', actionTypeName: 'GET_DDL_TblToggle162' },
      { columnName: 'TblGrouping162', payloadDdlName: 'TblGrouping162List', keyName: 'TblGrouping162', labelName: 'TblGrouping162Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetTblGrouping162List', filterByMaster: true, filterByColumnName: 'ReportId162', actionTypeName: 'GET_DDL_TblGrouping162' },
      { columnName: 'RptTblTypeCd162', payloadDdlName: 'RptTblTypeCd162List', keyName: 'RptTblTypeCd162', labelName: 'RptTblTypeCd162Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetRptTblTypeCd162List', actionTypeName: 'GET_DDL_RptTblTypeCd162' },
      { columnName: 'TblVisibility162', payloadDdlName: 'TblVisibility162List', keyName: 'TblVisibility162', labelName: 'TblVisibility162Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetTblVisibility162List', actionTypeName: 'GET_DDL_TblVisibility162' },
      { columnName: 'CelNum164', payloadDdlName: 'CelNum164List', keyName: 'CelNum164', labelName: 'CelNum164Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetCelNum164List', actionTypeName: 'GET_DDL_CelNum164' },
    ]
    this.ScreenOnDemandDef = [

    ]

    this.ScreenCriDdlDef = [
      { columnName: 'ReportId10', payloadDdlName: 'ReportId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriReportId10List', actionTypeName: 'GET_DDL_CRIReportId10' },
      { columnName: 'RptCtrId20', payloadDdlName: 'RptCtrId20List', isAutoComplete: true, apiServiceName: 'GetScreenCriRptCtrId20List', actionTypeName: 'GET_DDL_CRIRptCtrId20' },
    ]
    this.SearchActions = {
      ...[...this.ScreenDdlDef].reduce((a, v) => { a['Search' + v.columnName] = this.MakeSearchAction(v); return a; }, {}),
      ...[...this.ScreenCriDdlDef].reduce((a, v) => { a['SearchCri' + v.columnName] = this.MakeSearchAction(v); return a; }, {}),
      ...[...this.ScreenOnDemandDef].reduce((a, v) => { a['Get' + v.columnName] = this.MakeGetColumnOnDemandAction(v); return a; }, {}),
    }
    this.ScreenDdlSelectors = this.ScreenDdlDef.reduce((a, v) => { a[v.columnName] = this.MakeDdlSelectors(v); return a; }, {})
    this.ScreenCriDdlSelectors = this.ScreenCriDdlDef.reduce((a, v) => { a[v.columnName] = this.MakeCriDdlSelectors(v); return a; }, {})
    this.actionReducers = this.MakeActionReducers();
  }
  GetScreenName() { return 'AdmRptTbl' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RptTblId' : 'RptTblId162'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RptCelId' : 'RptCelId164'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmRptTblService; }
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
      RptCelId164: null,
      RowNum164: null,
      RowHeight164: null,
      RowVisibility164: null,
      CelNum164: null,
      CelColSpan164: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      RptTblId162: copy ? null : mst.RptTblId162,
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
          RptTblId162: null,
          RptCelId164: null,
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
    || (state.ScreenCriteria.ReportId10 || {}).LastCriteria
    || (state.ScreenCriteria.RptCtrId20 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmRptTblRedux()
