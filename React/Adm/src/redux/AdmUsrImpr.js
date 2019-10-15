
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmUsrImprService from '../services/AdmUsrImprService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmUsrImprRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmUsrImpr66List',
        'GET_MST' : 'GetAdmUsrImpr66ById',
        'GET_DTL_LIST' : 'GetAdmUsrImpr66DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'UsrId95', payloadDdlName:'UsrId95List', keyName:'UsrId95',labelName:'UsrId95Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetUsrId95List', actionTypeName: 'GET_DDL_UsrId95' },
{ columnName: 'ImprUsrId95', payloadDdlName:'ImprUsrId95List', keyName:'ImprUsrId95',labelName:'ImprUsrId95Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetImprUsrId95List', actionTypeName: 'GET_DDL_ImprUsrId95' },
{ columnName: 'InputBy95', payloadDdlName:'InputBy95List', keyName:'InputBy95',labelName:'InputBy95Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetInputBy95List', actionTypeName: 'GET_DDL_InputBy95' },
{ columnName: 'ModifiedBy95', payloadDdlName:'ModifiedBy95List', keyName:'ModifiedBy95',labelName:'ModifiedBy95Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetModifiedBy95List', actionTypeName: 'GET_DDL_ModifiedBy95' },
{ columnName: 'TestCulture95', payloadDdlName:'TestCulture95List', keyName:'TestCulture95',labelName:'TestCulture95Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetTestCulture95List', actionTypeName: 'GET_DDL_TestCulture95' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'UPicMed1', tableColumnName: 'UPicMed', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_UPicMed1' },
{ columnName: 'IPicMed1', tableColumnName: 'IPicMed', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_IPicMed1' },
//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [

      ]
      this.SearchActions = {
        ...[...this.ScreenDdlDef].reduce((a,v)=>{a['Search' + v.columnName] = this.MakeSearchAction(v); return a;},{}),
        ...[...this.ScreenCriDdlDef].reduce((a,v)=>{a['SearchCri' + v.columnName] = this.MakeSearchAction(v); return a;},{}),
        ...[...this.ScreenOnDemandDef].reduce((a,v)=>{a['Get' + v.columnName] = this.MakeGetColumnOnDemandAction(v); return a;},{}),
      } 
      this.ScreenDdlSelectors = this.ScreenDdlDef.reduce((a,v)=>{a[v.columnName] = this.MakeDdlSelectors(v); return a;},{})
      this.ScreenCriDdlSelectors = this.ScreenCriDdlDef.reduce((a,v)=>{a[v.columnName] = this.MakeCriDdlSelectors(v); return a;},{})
      this.actionReducers = this.MakeActionReducers();
    }
    GetScreenName(){return 'AdmUsrImpr'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'UsrImprId' :  'UsrImprId95'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmUsrImprService}
    GetReducerActionTypePrefix(){return this.GetScreenName()};
    GetActionType(actionTypeName){return getAsyncTypes(this.GetReducerActionTypePrefix(),actionTypeName)}
    GetInitState(){
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
        UsrImprId95: copy ? null : mst.UsrImprId95,
		
        // CurrencyId64Text: GetCurrencyId64Cd(mst.CurrencyId64, state),
        // MemberId64Text: GetMemberId64Text(mst.MemberId64, state),
        // /* specific app rule */
        // Posted64: copy ? 'N' : mst.Posted64,
        // TrxTotal64: copy ? '0' : mst.TrxTotal64,
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
              // detailR: v.detailR ? GetCurrencyId64Cd(v.detailR, state) : '',
			  detailR: v.detailR,
              detail: v.detail || '',
              idx: i,
              // CurrencyId64: v.detailR,
              isSelected: v.isSelected,
            }
          })
    }
  }

/* ReactRule: Redux Custom Function */

/* ReactRule End: Redux Custom Function */

  /* helper functions */
  // export function GetCurrencyId64Cd(CurrencyId64, state) {
    // try {
      // const d = ((state.ddl.CurrencyId64 || {}) || []).reduce((r, v, i, a) => { r[v.CurrencyId64] = v.CurrencyName; return r; }, {});
      // return (d || {})[CurrencyId64];
    // } catch (e) {
      // return '';
    // }
  // }

  // export function GetMemberId64Text(MemberId64, state) {
    // try {
      // const d = (state.ddl.MemberId64).reduce((r, v, i, a) => { r[v.key] = v.label; return r; }, {});
      // return (d || {})[MemberId64];
    // } catch (e) {
      // return '';
    // }
  // }

  export function ShowMstFilterApplied(state) {
    return !state 
      || !state.ScreenCriteria
//      || (state.ScreenCriteria.MemberId10 || {}).LastCriteria
//      || (state.ScreenCriteria.CustomerJobId20 || {}).LastCriteria
//      || (state.ScreenCriteria.Posted30 ||{}).LastCriteria

      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmUsrImprRedux()
            