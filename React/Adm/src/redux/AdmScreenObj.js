
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmScreenObjService from '../services/AdmScreenObjService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmScreenObjRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmScreenObj10List',
        'GET_MST' : 'GetAdmScreenObj10ById',
        'GET_DTL_LIST' : 'GetAdmScreenObj10DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'GridGrpCd14', payloadDdlName:'GridGrpCd14List', keyName:'GridGrpCd14',labelName:'GridGrpCd14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetGridGrpCd14List', actionTypeName: 'GET_DDL_GridGrpCd14' },
{ columnName: 'ColumnJustify14', payloadDdlName:'ColumnJustify14List', keyName:'ColumnJustify14',labelName:'ColumnJustify14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetColumnJustify14List', actionTypeName: 'GET_DDL_ColumnJustify14' },
{ columnName: 'ScreenId14', payloadDdlName:'ScreenId14List', keyName:'ScreenId14',labelName:'ScreenId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetScreenId14List', actionTypeName: 'GET_DDL_ScreenId14' },
{ columnName: 'GroupRowId14', payloadDdlName:'GroupRowId14List', keyName:'GroupRowId14',labelName:'GroupRowId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetGroupRowId14List', actionTypeName: 'GET_DDL_GroupRowId14' },
{ columnName: 'GroupColId14', payloadDdlName:'GroupColId14List', keyName:'GroupColId14',labelName:'GroupColId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetGroupColId14List', actionTypeName: 'GET_DDL_GroupColId14' },
{ columnName: 'ColumnId14', payloadDdlName:'ColumnId14List', keyName:'ColumnId14',labelName:'ColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetColumnId14List', actionTypeName: 'GET_DDL_ColumnId14' },
{ columnName: 'DisplayModeId14', payloadDdlName:'DisplayModeId14List', keyName:'DisplayModeId14',labelName:'DisplayModeId14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDisplayModeId14List', actionTypeName: 'GET_DDL_DisplayModeId14' },
{ columnName: 'DdlKeyColumnId14', payloadDdlName:'DdlKeyColumnId14List', keyName:'DdlKeyColumnId14',labelName:'DdlKeyColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlKeyColumnId14List', actionTypeName: 'GET_DDL_DdlKeyColumnId14' },
{ columnName: 'DdlRefColumnId14', payloadDdlName:'DdlRefColumnId14List', keyName:'DdlRefColumnId14',labelName:'DdlRefColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlRefColumnId14List', actionTypeName: 'GET_DDL_DdlRefColumnId14' },
{ columnName: 'DdlSrtColumnId14', payloadDdlName:'DdlSrtColumnId14List', keyName:'DdlSrtColumnId14',labelName:'DdlSrtColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlSrtColumnId14List', actionTypeName: 'GET_DDL_DdlSrtColumnId14' },
{ columnName: 'DdlAdnColumnId14', payloadDdlName:'DdlAdnColumnId14List', keyName:'DdlAdnColumnId14',labelName:'DdlAdnColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlAdnColumnId14List', actionTypeName: 'GET_DDL_DdlAdnColumnId14' },
{ columnName: 'DdlFtrColumnId14', payloadDdlName:'DdlFtrColumnId14List', keyName:'DdlFtrColumnId14',labelName:'DdlFtrColumnId14Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDdlFtrColumnId14List', actionTypeName: 'GET_DDL_DdlFtrColumnId14' },
{ columnName: 'DtlLstPosId14', payloadDdlName:'DtlLstPosId14List', keyName:'DtlLstPosId14',labelName:'DtlLstPosId14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDtlLstPosId14List', actionTypeName: 'GET_DDL_DtlLstPosId14' },
{ columnName: 'AggregateCd14', payloadDdlName:'AggregateCd14List', keyName:'AggregateCd14',labelName:'AggregateCd14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetAggregateCd14List', actionTypeName: 'GET_DDL_AggregateCd14' },
{ columnName: 'MatchCd14', payloadDdlName:'MatchCd14List', keyName:'MatchCd14',labelName:'MatchCd14Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetMatchCd14List', actionTypeName: 'GET_DDL_MatchCd14' },
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
    GetScreenName(){return 'AdmScreenObj'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ScreenObjId' :  'ScreenObjId14'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmScreenObjService}
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
        ScreenObjId14: copy ? null : mst.ScreenObjId14,
		
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

  export default new AdmScreenObjRedux()
            