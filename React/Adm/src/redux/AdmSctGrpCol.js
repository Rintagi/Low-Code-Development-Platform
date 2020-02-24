
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmSctGrpColService from '../services/AdmSctGrpColService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmSctGrpColRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmSctGrpCol1003List',
      'GET_MST': 'GetAdmSctGrpCol1003ById',
      'GET_DTL_LIST': 'GetAdmSctGrpCol1003DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'SectionCd1284', payloadDdlName: 'SectionCd1284List', keyName: 'SectionCd1284', labelName: 'SectionCd1284Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetSectionCd1284List', actionTypeName: 'GET_DDL_SectionCd1284' },
      { columnName: 'GroupColId1284', payloadDdlName: 'GroupColId1284List', keyName: 'GroupColId1284', labelName: 'GroupColId1284Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetGroupColId1284List', actionTypeName: 'GET_DDL_GroupColId1284' },
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
  GetScreenName() { return 'AdmSctGrpCol' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'SctGrpColId' : 'SctGrpColId1284'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmSctGrpColService; }
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
      SctGrpColId1284: copy ? null : mst.SctGrpColId1284,
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

export default new AdmSctGrpColRedux()
