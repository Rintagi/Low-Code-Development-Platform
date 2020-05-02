
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmRptStyleService from '../services/AdmRptStyleService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmRptStyleRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmRptStyle89List',
      'GET_MST': 'GetAdmRptStyle89ById',
      'GET_DTL_LIST': 'GetAdmRptStyle89DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'DefaultCd167', payloadDdlName: 'DefaultCd167List', keyName: 'DefaultCd167', labelName: 'DefaultCd167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetDefaultCd167List', actionTypeName: 'GET_DDL_DefaultCd167' },
      { columnName: 'BgGradType167', payloadDdlName: 'BgGradType167List', keyName: 'BgGradType167', labelName: 'BgGradType167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBgGradType167List', actionTypeName: 'GET_DDL_BgGradType167' },
      { columnName: 'Direction167', payloadDdlName: 'Direction167List', keyName: 'Direction167', labelName: 'Direction167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetDirection167List', actionTypeName: 'GET_DDL_Direction167' },
      { columnName: 'WritingMode167', payloadDdlName: 'WritingMode167List', keyName: 'WritingMode167', labelName: 'WritingMode167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetWritingMode167List', actionTypeName: 'GET_DDL_WritingMode167' },
      { columnName: 'BorderStyleD167', payloadDdlName: 'BorderStyleD167List', keyName: 'BorderStyleD167', labelName: 'BorderStyleD167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBorderStyleD167List', actionTypeName: 'GET_DDL_BorderStyleD167' },
      { columnName: 'BorderStyleL167', payloadDdlName: 'BorderStyleL167List', keyName: 'BorderStyleL167', labelName: 'BorderStyleL167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBorderStyleL167List', actionTypeName: 'GET_DDL_BorderStyleL167' },
      { columnName: 'BorderStyleR167', payloadDdlName: 'BorderStyleR167List', keyName: 'BorderStyleR167', labelName: 'BorderStyleR167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBorderStyleR167List', actionTypeName: 'GET_DDL_BorderStyleR167' },
      { columnName: 'BorderStyleT167', payloadDdlName: 'BorderStyleT167List', keyName: 'BorderStyleT167', labelName: 'BorderStyleT167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBorderStyleT167List', actionTypeName: 'GET_DDL_BorderStyleT167' },
      { columnName: 'BorderStyleB167', payloadDdlName: 'BorderStyleB167List', keyName: 'BorderStyleB167', labelName: 'BorderStyleB167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBorderStyleB167List', actionTypeName: 'GET_DDL_BorderStyleB167' },
      { columnName: 'FontStyle167', payloadDdlName: 'FontStyle167List', keyName: 'FontStyle167', labelName: 'FontStyle167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetFontStyle167List', actionTypeName: 'GET_DDL_FontStyle167' },
      { columnName: 'FontWeight167', payloadDdlName: 'FontWeight167List', keyName: 'FontWeight167', labelName: 'FontWeight167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetFontWeight167List', actionTypeName: 'GET_DDL_FontWeight167' },
      { columnName: 'TextDecor167', payloadDdlName: 'TextDecor167List', keyName: 'TextDecor167', labelName: 'TextDecor167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetTextDecor167List', actionTypeName: 'GET_DDL_TextDecor167' },
      { columnName: 'TextAlign167', payloadDdlName: 'TextAlign167List', keyName: 'TextAlign167', labelName: 'TextAlign167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetTextAlign167List', actionTypeName: 'GET_DDL_TextAlign167' },
      { columnName: 'VerticalAlign167', payloadDdlName: 'VerticalAlign167List', keyName: 'VerticalAlign167', labelName: 'VerticalAlign167Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetVerticalAlign167List', actionTypeName: 'GET_DDL_VerticalAlign167' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'DefaultCd10', payloadDdlName: 'DefaultCd10List', keyName: 'DefaultCd', labelName: 'DefaultName', isCheckBox:false, isAutoComplete: false, apiServiceName: 'GetScreenCriDefaultCd10List', actionTypeName: 'GET_DDL_CRIDefaultCd10' },
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
  GetScreenName() { return 'AdmRptStyle' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'RptStyleId' : 'RptStyleId167'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmRptStyleService; }
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

    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      RptStyleId167: copy ? null : mst.RptStyleId167,
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
    || (state.ScreenCriteria.DefaultCd10 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmRptStyleRedux()
