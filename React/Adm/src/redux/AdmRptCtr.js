
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmRptCtrService from '../services/AdmRptCtrService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmRptCtrRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmRptCtr90List',
        'GET_MST' : 'GetAdmRptCtr90ById',
        'GET_DTL_LIST' : 'GetAdmRptCtr90DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'ReportId161', payloadDdlName:'ReportId161List', keyName:'ReportId161',labelName:'ReportId161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId161List', actionTypeName: 'GET_DDL_ReportId161' },
{ columnName: 'PRptCtrId161', payloadDdlName:'PRptCtrId161List', keyName:'PRptCtrId161',labelName:'PRptCtrId161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetPRptCtrId161List', actionTypeName: 'GET_DDL_PRptCtrId161' },
{ columnName: 'RptElmId161', payloadDdlName:'RptElmId161List', keyName:'RptElmId161',labelName:'RptElmId161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetRptElmId161List', actionTypeName: 'GET_DDL_RptElmId161' },
{ columnName: 'RptCelId161', payloadDdlName:'RptCelId161List', keyName:'RptCelId161',labelName:'RptCelId161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetRptCelId161List', actionTypeName: 'GET_DDL_RptCelId161' },
{ columnName: 'RptStyleId161', payloadDdlName:'RptStyleId161List', keyName:'RptStyleId161',labelName:'RptStyleId161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetRptStyleId161List', actionTypeName: 'GET_DDL_RptStyleId161' },
{ columnName: 'RptCtrTypeCd161', payloadDdlName:'RptCtrTypeCd161List', keyName:'RptCtrTypeCd161',labelName:'RptCtrTypeCd161Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRptCtrTypeCd161List', actionTypeName: 'GET_DDL_RptCtrTypeCd161' },
{ columnName: 'CtrVisibility161', payloadDdlName:'CtrVisibility161List', keyName:'CtrVisibility161',labelName:'CtrVisibility161Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetCtrVisibility161List', actionTypeName: 'GET_DDL_CtrVisibility161' },
{ columnName: 'CtrToggle161', payloadDdlName:'CtrToggle161List', keyName:'CtrToggle161',labelName:'CtrToggle161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCtrToggle161List', filterByMaster:true, filterByColumnName:'ReportId161',actionTypeName: 'GET_DDL_CtrToggle161' },
{ columnName: 'CtrGrouping161', payloadDdlName:'CtrGrouping161List', keyName:'CtrGrouping161',labelName:'CtrGrouping161Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCtrGrouping161List', filterByMaster:true, filterByColumnName:'ReportId161',actionTypeName: 'GET_DDL_CtrGrouping161' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ReportId10', payloadDdlName: 'ReportId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriReportId10List', actionTypeName: 'GET_DDL_CRIReportId10' },
{ columnName: 'CtrValue20', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_CtrValue20' },
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
    GetScreenName(){return 'AdmRptCtr'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'RptCtrId' :  'RptCtrId161'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmRptCtrService}
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
        RptCtrId161: copy ? null : mst.RptCtrId161,
		
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
|| (state.ScreenCriteria.ReportId10 || {}).LastCriteria
|| (state.ScreenCriteria.CtrValue20 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmRptCtrRedux()
            