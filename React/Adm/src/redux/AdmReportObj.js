
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmReportObjService from '../services/AdmReportObjService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmReportObjRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmReportObj13List',
        'GET_MST' : 'GetAdmReportObj13ById',
        'GET_DTL_LIST' : 'GetAdmReportObj13DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'ReportId23', payloadDdlName:'ReportId23List', keyName:'ReportId23',labelName:'ReportId23Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetReportId23List', actionTypeName: 'GET_DDL_ReportId23' },
{ columnName: 'RptObjTypeCd23', payloadDdlName:'RptObjTypeCd23List', keyName:'RptObjTypeCd23',labelName:'RptObjTypeCd23Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetRptObjTypeCd23List', actionTypeName: 'GET_DDL_RptObjTypeCd23' },
{ columnName: 'DataTypeId23', payloadDdlName:'DataTypeId23List', keyName:'DataTypeId23',labelName:'DataTypeId23Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDataTypeId23List', actionTypeName: 'GET_DDL_DataTypeId23' },
{ columnName: 'OperatorId23', payloadDdlName:'OperatorId23List', keyName:'OperatorId23',labelName:'OperatorId23Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetOperatorId23List', actionTypeName: 'GET_DDL_OperatorId23' },
{ columnName: 'ReportCriId23', payloadDdlName:'ReportCriId23List', keyName:'ReportCriId23',labelName:'ReportCriId23Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetReportCriId23List', actionTypeName: 'GET_DDL_ReportCriId23' },
{ columnName: 'CultureId99', payloadDdlName:'CultureId99List', keyName:'CultureId99',labelName:'CultureId99Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetCultureId99List', actionTypeName: 'GET_DDL_CultureId99' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ReportId10', payloadDdlName: 'ReportId10List', keyName:'ReportId', labelName:'ReportDesc', isCheckBox:false, isAutoComplete: false, apiServiceName: 'GetScreenCriReportId10List', actionTypeName: 'GET_DDL_CRIReportId10' },
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
    GetScreenName(){return 'AdmReportObj'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportObjId' :  'ReportObjId23'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ReportObjHlpId'  :'ReportObjHlpId99'}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmReportObjService}
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
       ReportObjHlpId99: null,
CultureId99: null,
ColumnHeader99: null,
HeaderWidth99: null,
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        ReportObjId23: copy ? null : mst.ReportObjId23,
		
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
                                    ReportObjId23: null,
                                    ReportObjHlpId99: null,
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

  export default new AdmReportObjRedux()
            