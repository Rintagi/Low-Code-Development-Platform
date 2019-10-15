
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmRptChaService from '../services/AdmRptChaService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmRptChaRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmRptCha100List',
        'GET_MST' : 'GetAdmRptCha100ById',
        'GET_DTL_LIST' : 'GetAdmRptCha100DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'RptCtrId206', payloadDdlName:'RptCtrId206List', keyName:'RptCtrId206',labelName:'RptCtrId206Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetRptCtrId206List', actionTypeName: 'GET_DDL_RptCtrId206' },
{ columnName: 'ReportId206', payloadDdlName:'ReportId206List', keyName:'ReportId206',labelName:'ReportId206Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId206List', actionTypeName: 'GET_DDL_ReportId206' },
{ columnName: 'RptChaTypeCd206', payloadDdlName:'RptChaTypeCd206List', keyName:'RptChaTypeCd206',labelName:'RptChaTypeCd206Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRptChaTypeCd206List', actionTypeName: 'GET_DDL_RptChaTypeCd206' },
{ columnName: 'CategoryGrp206', payloadDdlName:'CategoryGrp206List', keyName:'CategoryGrp206',labelName:'CategoryGrp206Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetCategoryGrp206List', actionTypeName: 'GET_DDL_CategoryGrp206' },
{ columnName: 'SeriesGrp206', payloadDdlName:'SeriesGrp206List', keyName:'SeriesGrp206',labelName:'SeriesGrp206Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetSeriesGrp206List', actionTypeName: 'GET_DDL_SeriesGrp206' },
      ]
      this.ScreenOnDemandDef = [

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
    GetScreenName(){return 'AdmRptCha'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'RptChaId' :  'RptChaId206'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmRptChaService}
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
        RptChaId206: copy ? null : mst.RptChaId206,
		
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

  export default new AdmRptChaRedux()
            