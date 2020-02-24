
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmMenuHlpService from '../services/AdmMenuHlpService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmMenuHlpRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmMenuHlp36List',
      'GET_MST': 'GetAdmMenuHlp36ById',
      'GET_DTL_LIST': 'GetAdmMenuHlp36DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'CultureId40', payloadDdlName: 'CultureId40List', keyName: 'CultureId40', labelName: 'CultureId40Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetCultureId40List', actionTypeName: 'GET_DDL_CultureId40' },
    ]
    this.ScreenOnDemandDef = [

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
  GetScreenName() { return 'AdmMenuHlp' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MenuId' : 'MenuId39'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MenuHlpId' : 'MenuHlpId40'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmMenuHlpService; }
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
      MenuHlpId40: null,
      CultureId40: null,
      MenuText40: null,
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
    if (!copy) return dtlList;
    else if (!this.allowTmpDtl) return [];
    else {
      const now = Date.now();
      return dtlList.map((v, i) => {
        return {
          ...v,
          MenuId39: null,
          MenuHlpId40: null,
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

    || state.ScreenCriteria.SearchStr;
}

export default new AdmMenuHlpRedux()
