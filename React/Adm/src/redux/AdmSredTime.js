
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmSredTimeService from '../services/AdmSredTimeService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
class AdmSredTimeRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmSredTime123List',
      'GET_MST': 'GetAdmSredTime123ById',
      'GET_DTL_LIST': 'GetAdmSredTime123DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'MemberId272', payloadDdlName: 'MemberId272List', keyName: 'MemberId272', labelName: 'MemberId272Text', forMst: true, isAutoComplete: true, apiServiceName: 'GetMemberId272List', actionTypeName: 'GET_DDL_MemberId272' },
      { columnName: 'EnteredBy272', payloadDdlName: 'EnteredBy272List', keyName: 'EnteredBy272', labelName: 'EnteredBy272Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetEnteredBy272List', actionTypeName: 'GET_DDL_EnteredBy272' },
      { columnName: 'ModifiedBy272', payloadDdlName: 'ModifiedBy272List', keyName: 'ModifiedBy272', labelName: 'ModifiedBy272Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetModifiedBy272List', actionTypeName: 'GET_DDL_ModifiedBy272' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'MemberId10', payloadDdlName: 'MemberId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriMemberId10List', actionTypeName: 'GET_DDL_CRIMemberId10' },
      
      
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
  GetScreenName() { return 'AdmSredTime' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'SredTimeId' : 'SredTimeId272'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? '' : ''; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmSredTimeService; }
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
      SredTimeId272: copy ? null : mst.SredTimeId272,
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
    || (state.ScreenCriteria.MemberId10 || {}).LastCriteria
    || (state.ScreenCriteria.SredTimeDt20 || {}).LastCriteria
    || (state.ScreenCriteria.SredTimeDt30 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmSredTimeRedux()
