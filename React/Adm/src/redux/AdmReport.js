
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmReportService from '../services/AdmReportService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmReportRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmReport67List',
        'GET_MST' : 'GetAdmReport67ById',
        'GET_DTL_LIST' : 'GetAdmReport67DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'ReportTypeCd22', payloadDdlName:'ReportTypeCd22List', keyName:'ReportTypeCd22',labelName:'ReportTypeCd22Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetReportTypeCd22List', actionTypeName: 'GET_DDL_ReportTypeCd22' },
{ columnName: 'OrientationCd22', payloadDdlName:'OrientationCd22List', keyName:'OrientationCd22',labelName:'OrientationCd22Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetOrientationCd22List', actionTypeName: 'GET_DDL_OrientationCd22' },
{ columnName: 'CopyReportId22', payloadDdlName:'CopyReportId22List', keyName:'CopyReportId22',labelName:'CopyReportId22Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCopyReportId22List', actionTypeName: 'GET_DDL_CopyReportId22' },
{ columnName: 'ModifiedBy22', payloadDdlName:'ModifiedBy22List', keyName:'ModifiedBy22',labelName:'ModifiedBy22Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetModifiedBy22List', actionTypeName: 'GET_DDL_ModifiedBy22' },
{ columnName: 'UnitCd22', payloadDdlName:'UnitCd22List', keyName:'UnitCd22',labelName:'UnitCd22Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetUnitCd22List', actionTypeName: 'GET_DDL_UnitCd22' },
{ columnName: 'CultureId96', payloadDdlName:'CultureId96List', keyName:'CultureId96',labelName:'CultureId96Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId96List', actionTypeName: 'GET_DDL_CultureId96' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'SyncByDb', tableColumnName: 'SyncByDb', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SyncByDb' },
{ columnName: 'SyncToDb', tableColumnName: 'SyncToDb', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SyncToDb' },
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
    GetScreenName(){return 'AdmReport'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportId' :  'ReportId22'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportHlpId'  :'ReportHlpId96'}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmReportService}
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
       ReportHlpId96: null,
CultureId96: null,
DefaultHlpMsg96: null,
ReportTitle96: null,
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        ReportId22: copy ? null : mst.ReportId22,
		
        // CurrencyId64Text: GetCurrencyId64Cd(mst.CurrencyId64, state),
        // MemberId64Text: GetMemberId64Text(mst.MemberId64, state),
        // /* specific app rule */
        // Posted64: copy ? 'N' : mst.Posted64,
        // TrxTotal64: copy ? '0' : mst.TrxTotal64,
      }
    }

ExpandDtl(dtlList, copy) {
                                if (!copy) return dtlList;
                                else if (!this.allowTmpDtl) return []; 
                                else { const now = Date.now();
                                  return dtlList.map((v,i) => {
                                  return {
                                    ...v,
                                    ReportId22: null,
                                    ReportHlpId96: null,
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

  export default new AdmReportRedux()
            