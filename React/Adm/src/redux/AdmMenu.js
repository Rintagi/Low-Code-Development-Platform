
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmMenuService from '../services/AdmMenuService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmMenuRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmMenu35List',
      'GET_MST': 'GetAdmMenu35ById',
      'GET_DTL_LIST': 'GetAdmMenu35DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'ScreenId39', payloadDdlName: 'ScreenId39List', keyName: 'ScreenId39', labelName: 'ScreenId39Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetScreenId39List', actionTypeName: 'GET_DDL_ScreenId39' },
      { columnName: 'ReportId39', payloadDdlName: 'ReportId39List', keyName: 'ReportId39', labelName: 'ReportId39Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetReportId39List', actionTypeName: 'GET_DDL_ReportId39' },
      { columnName: 'WizardId39', payloadDdlName: 'WizardId39List', keyName: 'WizardId39', labelName: 'WizardId39Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetWizardId39List', actionTypeName: 'GET_DDL_WizardId39' },
      { columnName: 'StaticPgId39', payloadDdlName: 'StaticPgId39List', keyName: 'StaticPgId39', labelName: 'StaticPgId39Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetStaticPgId39List', actionTypeName: 'GET_DDL_StaticPgId39' },
    ]
    this.ScreenOnDemandDef = [
      { columnName: 'IconUrl39', tableColumnName: 'IconUrl', forMst: true, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_IconUrl39' },
    ]

    this.ScreenCriDdlDef = [

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
  GetScreenName() { return 'AdmMenu' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MenuId' : 'MenuId39'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmMenuService; }
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
      MenuId39: copy ? null : mst.MenuId39,
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

    || state.ScreenCriteria.SearchStr;
}

export default new AdmMenuRedux()
