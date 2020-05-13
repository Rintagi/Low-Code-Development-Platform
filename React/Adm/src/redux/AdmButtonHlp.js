
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmButtonHlpService from '../services/AdmButtonHlpService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmButtonHlpRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmButtonHlp76List',
      'GET_MST': 'GetAdmButtonHlp76ById',
      'GET_DTL_LIST': 'GetAdmButtonHlp76DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'CultureId116', payloadDdlName: 'CultureId116List', keyName: 'CultureId116', labelName: 'CultureId116Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetCultureId116List', actionTypeName: 'GET_DDL_CultureId116' },
      { columnName: 'ButtonTypeId116', payloadDdlName: 'ButtonTypeId116List', keyName: 'ButtonTypeId116', labelName: 'ButtonTypeId116Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetButtonTypeId116List', actionTypeName: 'GET_DDL_ButtonTypeId116' },
      { columnName: 'ScreenId116', payloadDdlName: 'ScreenId116List', keyName: 'ScreenId116', labelName: 'ScreenId116Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId116List', actionTypeName: 'GET_DDL_ScreenId116' },
      { columnName: 'ReportId116', payloadDdlName: 'ReportId116List', keyName: 'ReportId116', labelName: 'ReportId116Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetReportId116List', actionTypeName: 'GET_DDL_ReportId116' },
      { columnName: 'WizardId116', payloadDdlName: 'WizardId116List', keyName: 'WizardId116', labelName: 'WizardId116Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetWizardId116List', actionTypeName: 'GET_DDL_WizardId116' },
      { columnName: 'TopVisible116', payloadDdlName: 'TopVisible116List', keyName: 'TopVisible116', labelName: 'TopVisible116Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetTopVisible116List', actionTypeName: 'GET_DDL_TopVisible116' },
      { columnName: 'RowVisible116', payloadDdlName: 'RowVisible116List', keyName: 'RowVisible116', labelName: 'RowVisible116Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetRowVisible116List', actionTypeName: 'GET_DDL_RowVisible116' },
      { columnName: 'BotVisible116', payloadDdlName: 'BotVisible116List', keyName: 'BotVisible116', labelName: 'BotVisible116Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetBotVisible116List', actionTypeName: 'GET_DDL_BotVisible116' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'CultureId10', payloadDdlName: 'CultureId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriCultureId10List', actionTypeName: 'GET_DDL_CRICultureId10' },
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
  GetScreenName() { return 'AdmButtonHlp' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'ButtonHlpId' : 'ButtonHlpId116'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmButtonHlpService; }
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
      ButtonHlpId116: copy ? null : mst.ButtonHlpId116,
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
    || (state.ScreenCriteria.CultureId10 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmButtonHlpRedux()
