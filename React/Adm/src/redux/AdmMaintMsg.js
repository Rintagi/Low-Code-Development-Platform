
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmMaintMsgService from '../services/AdmMaintMsgService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmMaintMsgRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmMaintMsg109List',
      'GET_MST': 'GetAdmMaintMsg109ById',
      'GET_DTL_LIST': 'GetAdmMaintMsg109DtlById',
    }
    this.ScreenDdlDef = [

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
  GetScreenName() { return 'AdmMaintMsg' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MaintMsgId' : 'MaintMsgId233'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmMaintMsgService; }
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
      MaintMsgId233: copy ? null : mst.MaintMsgId233,
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

export default new AdmMaintMsgRedux()
