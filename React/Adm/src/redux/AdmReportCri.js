
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmReportCriService from '../services/AdmReportCriService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmReportCriRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmReportCri69List',
        'GET_MST' : 'GetAdmReportCri69ById',
        'GET_DTL_LIST' : 'GetAdmReportCri69DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'ReportId97', payloadDdlName:'ReportId97List', keyName:'ReportId97',labelName:'ReportId97Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId97List', actionTypeName: 'GET_DDL_ReportId97' },
{ columnName: 'ReportGrpId97', payloadDdlName:'ReportGrpId97List', keyName:'ReportGrpId97',labelName:'ReportGrpId97Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetReportGrpId97List', actionTypeName: 'GET_DDL_ReportGrpId97' },
{ columnName: 'TableId97', payloadDdlName:'TableId97List', keyName:'TableId97',labelName:'TableId97Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetTableId97List', actionTypeName: 'GET_DDL_TableId97' },
{ columnName: 'DataTypeId97', payloadDdlName:'DataTypeId97List', keyName:'DataTypeId97',labelName:'DataTypeId97Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDataTypeId97List', actionTypeName: 'GET_DDL_DataTypeId97' },
{ columnName: 'DisplayModeId97', payloadDdlName:'DisplayModeId97List', keyName:'DisplayModeId97',labelName:'DisplayModeId97Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDisplayModeId97List', actionTypeName: 'GET_DDL_DisplayModeId97' },
{ columnName: 'DdlFtrColumnId97', payloadDdlName:'DdlFtrColumnId97List', keyName:'DdlFtrColumnId97',labelName:'DdlFtrColumnId97Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlFtrColumnId97List', filterByMaster:true, filterByColumnName:'ReportId97',actionTypeName: 'GET_DDL_DdlFtrColumnId97' },
{ columnName: 'CultureId98', payloadDdlName:'CultureId98List', keyName:'CultureId98',labelName:'CultureId98Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId98List', actionTypeName: 'GET_DDL_CultureId98' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ReportId10', payloadDdlName: 'ReportId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriReportId10List', actionTypeName: 'GET_DDL_CRIReportId10' },
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
    GetScreenName(){return 'AdmReportCri'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportCriId' :  'ReportCriId97'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportCriHlpId'  :'ReportCriHlpId98'}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmReportCriService}
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
       ReportCriHlpId98: null,
CultureId98: null,
ColumnHeader98: null,
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        ReportCriId97: copy ? null : mst.ReportCriId97,
		
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
                                    ReportCriId97: null,
                                    ReportCriHlpId98: null,
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
|| (state.ScreenCriteria.ReportId10 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmReportCriRedux()
            