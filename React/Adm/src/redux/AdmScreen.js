
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmScreenService from '../services/AdmScreenService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmScreenRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmScreen9List',
        'GET_MST' : 'GetAdmScreen9ById',
        'GET_DTL_LIST' : 'GetAdmScreen9DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'ScreenTypeId15', payloadDdlName:'ScreenTypeId15List', keyName:'ScreenTypeId15',labelName:'ScreenTypeId15Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetScreenTypeId15List', actionTypeName: 'GET_DDL_ScreenTypeId15' },
{ columnName: 'ViewOnly15', payloadDdlName:'ViewOnly15List', keyName:'ViewOnly15',labelName:'ViewOnly15Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetViewOnly15List', actionTypeName: 'GET_DDL_ViewOnly15' },
{ columnName: 'MasterTableId15', payloadDdlName:'MasterTableId15List', keyName:'MasterTableId15',labelName:'MasterTableId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetMasterTableId15List', actionTypeName: 'GET_DDL_MasterTableId15' },
{ columnName: 'SearchTableId15', payloadDdlName:'SearchTableId15List', keyName:'SearchTableId15',labelName:'SearchTableId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchTableId15List', actionTypeName: 'GET_DDL_SearchTableId15' },
{ columnName: 'SearchId15', payloadDdlName:'SearchId15List', keyName:'SearchId15',labelName:'SearchId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchId15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchId15' },
{ columnName: 'SearchIdR15', payloadDdlName:'SearchIdR15List', keyName:'SearchIdR15',labelName:'SearchIdR15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchIdR15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchIdR15' },
{ columnName: 'SearchDtlId15', payloadDdlName:'SearchDtlId15List', keyName:'SearchDtlId15',labelName:'SearchDtlId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchDtlId15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchDtlId15' },
{ columnName: 'SearchDtlIdR15', payloadDdlName:'SearchDtlIdR15List', keyName:'SearchDtlIdR15',labelName:'SearchDtlIdR15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchDtlIdR15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchDtlIdR15' },
{ columnName: 'SearchUrlId15', payloadDdlName:'SearchUrlId15List', keyName:'SearchUrlId15',labelName:'SearchUrlId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchUrlId15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchUrlId15' },
{ columnName: 'SearchImgId15', payloadDdlName:'SearchImgId15List', keyName:'SearchImgId15',labelName:'SearchImgId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetSearchImgId15List', filterByMaster:true, filterByColumnName:'SearchTableId15',actionTypeName: 'GET_DDL_SearchImgId15' },
{ columnName: 'DetailTableId15', payloadDdlName:'DetailTableId15List', keyName:'DetailTableId15',labelName:'DetailTableId15Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetDetailTableId15List', actionTypeName: 'GET_DDL_DetailTableId15' },
{ columnName: 'CultureId16', payloadDdlName:'CultureId16List', keyName:'CultureId16',labelName:'CultureId16Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId16List', actionTypeName: 'GET_DDL_CultureId16' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'ScreenId10', payloadDdlName: 'ScreenId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriScreenId10List', actionTypeName: 'GET_DDL_CRIScreenId10' },
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
    GetScreenName(){return 'AdmScreen'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ScreenId' :  'ScreenId15'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? 'ScreenHlpId'  :'ScreenHlpId16'}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmScreenService}
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
       ScreenHlpId16: null,
CultureId16: null,
ScreenTitle16: null,
DefaultHlpMsg16: null,
FootNote16: null,
AddMsg16: null,
UpdMsg16: null,
DelMsg16: null,
IncrementMsg16: null,
NoMasterMsg16: null,
NoDetailMsg16: null,
AddMasterMsg16: null,
AddDetailMsg16: null,
MasterLstTitle16: null,
MasterLstSubtitle16: null,
MasterRecTitle16: null,
MasterRecSubtitle16: null,
MasterFoundMsg16: null,
DetailLstTitle16: null,
DetailLstSubtitle16: null,
DetailRecTitle16: null,
DetailRecSubtitle16: null,
DetailFoundMsg16: null,
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        ScreenId15: copy ? null : mst.ScreenId15,
		
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
                                    ScreenId15: null,
                                    ScreenHlpId16: null,
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
|| (state.ScreenCriteria.ScreenId10 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmScreenRedux()
            