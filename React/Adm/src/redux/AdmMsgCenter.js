
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmMsgCenterService from '../services/AdmMsgCenterService'
import { RintagiScreenRedux, initialRintagiScreenReduxState } from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmMsgCenterRedux extends RintagiScreenRedux {
  allowTmpDtl = false;
  constructor() {
    super();
    this.ActionApiNameMapper = {
      'GET_SEARCH_LIST': 'GetAdmMsgCenter86List',
      'GET_MST': 'GetAdmMsgCenter86ById',
      'GET_DTL_LIST': 'GetAdmMsgCenter86DtlById',
    }
    this.ScreenDdlDef = [
      { columnName: 'MsgTypeCd146', payloadDdlName: 'MsgTypeCd146List', keyName: 'MsgTypeCd146', labelName: 'MsgTypeCd146Text', forMst: true, isAutoComplete: false, apiServiceName: 'GetMsgTypeCd146List', actionTypeName: 'GET_DDL_MsgTypeCd146' },
      { columnName: 'CultureId147', payloadDdlName: 'CultureId147List', keyName: 'CultureId147', labelName: 'CultureId147Text', forMst: false, isAutoComplete: true, apiServiceName: 'GetCultureId147List', actionTypeName: 'GET_DDL_CultureId147' },
    ]
    this.ScreenOnDemandDef = [

    ]
    this.ScreenDocumentDef = [

    ]
    this.ScreenCriDdlDef = [
      { columnName: 'MsgId10', payloadDdlName: '', keyName: '', labelName: '', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_MsgId10' },
      { columnName: 'MsgSource20', payloadDdlName: '', keyName: '', labelName: '', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_MsgSource20' },
      { columnName: 'MsgName30', payloadDdlName: '', keyName: '', labelName: '', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_MsgName30' },
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
  GetScreenName() { return 'AdmMsgCenter' }
  GetMstKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MsgId' : 'MsgId146'; }
  GetDtlKeyColumnName(isUnderlining = false) { return isUnderlining ? 'MsgCenterId' : 'MsgCenterId147'; }
  GetPersistDtlName() { return this.GetScreenName() + '_Dtl'; }
  GetPersistMstName() { return this.GetScreenName() + '_Mst'; }
  GetWebService() { return AdmMsgCenterService; }
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
      MsgCenterId147: null,
      CultureId147: null,
      Msg147: null,
    }
  }
  ExpandMst(mst, state, copy) {
    return {
      ...mst,
      key: Date.now(),
      MsgId146: copy ? null : mst.MsgId146,
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
          MsgId146: null,
          MsgCenterId147: null,
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
    || (state.ScreenCriteria.MsgId10 || {}).LastCriteria
    || (state.ScreenCriteria.MsgSource20 || {}).LastCriteria
    || (state.ScreenCriteria.MsgName30 || {}).LastCriteria
    || state.ScreenCriteria.SearchStr;
}

export default new AdmMsgCenterRedux()
