
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmUsrImprService from '../services/AdmUsrImprService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmUsrImprRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmUsrImpr66List',
      'GET_MST': 'GetAdmUsrImpr66ById',
      'GET_DTL_LIST': 'GetAdmUsrImpr66DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'UsrId95', payloadDdlName: 'UsrId95List', keyName: 'UsrId95', labelName: 'UsrId95Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetUsrId95List', actionTypeName: 'GET_DDL_UsrId95' },
      { columnName: 'ImprUsrId95', payloadDdlName: 'ImprUsrId95List', keyName: 'ImprUsrId95', labelName: 'ImprUsrId95Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetImprUsrId95List', actionTypeName: 'GET_DDL_ImprUsrId95' },
      { columnName: 'InputBy95', payloadDdlName: 'InputBy95List', keyName: 'InputBy95', labelName: 'InputBy95Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetInputBy95List', actionTypeName: 'GET_DDL_InputBy95' },
      { columnName: 'ModifiedBy95', payloadDdlName: 'ModifiedBy95List', keyName: 'ModifiedBy95', labelName: 'ModifiedBy95Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetModifiedBy95List', actionTypeName: 'GET_DDL_ModifiedBy95' },
      { columnName: 'TestCulture95', payloadDdlName: 'TestCulture95List', keyName: 'TestCulture95', labelName: 'TestCulture95Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetTestCulture95List', actionTypeName: 'GET_DDL_TestCulture95' },
    ]
    this.ScreenOnDemandDef = [
      { columnName: 'UPicMed1', isFileObject: true, tableColumnName: 'PicMed1', type: 'RefColumn', refColumnName: 'UsrId95', forMst: true, apiServiceName: 'GetRefColumnContent', actionTypeName: 'GET_COLUMN_UPicMed1' },
      { columnName: 'IPicMed1', isFileObject: true, tableColumnName: 'PicMed1', type: 'RefColumn', refColumnName: 'ImprUsrId95', forMst: true, apiServiceName: 'GetRefColumnContent', actionTypeName: 'GET_COLUMN_IPicMed1' },
      { columnName: 'FailedAttempt1', isFileObject: false, tableColumnName: 'FailedAttempt1', type: 'RefColumn', refColumnName: 'ImprUsrId95', forMst: true, apiServiceName: 'GetRefColumnContent', actionTypeName: 'GET_COLUMN_FailedAttempt1' },
      { columnName: 'TestCurrency95', isFileObject: false, tableColumnName: 'TestCurrency95', type: 'RefColumn', refColumnName: 'TestCulture95', forMst: true, apiServiceName: 'GetRefColumnContent', actionTypeName: 'GET_COLUMN_TestCurrency95' },
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
  GetScreenName() { return 'AdmUsrImpr' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'UsrImprId' : 'UsrImprId95'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmUsrImprService; }
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
      UsrImprId95: copy ? null : mst.UsrImprId95,
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

export default new AdmUsrImprRedux()
