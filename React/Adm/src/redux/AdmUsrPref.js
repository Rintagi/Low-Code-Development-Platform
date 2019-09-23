
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmUsrPrefService from '../services/AdmUsrPrefService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmUsrPrefRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmUsrPref64List',
        'GET_MST' : 'GetAdmUsrPref64ById',
        'GET_DTL_LIST' : 'GetAdmUsrPref64DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'MenuOptId93', payloadDdlName:'MenuOptId93List', keyName:'MenuOptId93',labelName:'MenuOptId93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetMenuOptId93List', actionTypeName: 'GET_DDL_MenuOptId93' },
{ columnName: 'ComListVisible93', payloadDdlName:'ComListVisible93List', keyName:'ComListVisible93',labelName:'ComListVisible93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetComListVisible93List', actionTypeName: 'GET_DDL_ComListVisible93' },
{ columnName: 'PrjListVisible93', payloadDdlName:'PrjListVisible93List', keyName:'PrjListVisible93',labelName:'PrjListVisible93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetPrjListVisible93List', actionTypeName: 'GET_DDL_PrjListVisible93' },
{ columnName: 'SysListVisible93', payloadDdlName:'SysListVisible93List', keyName:'SysListVisible93',labelName:'SysListVisible93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetSysListVisible93List', actionTypeName: 'GET_DDL_SysListVisible93' },
{ columnName: 'UsrId93', payloadDdlName:'UsrId93List', keyName:'UsrId93',labelName:'UsrId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetUsrId93List', actionTypeName: 'GET_DDL_UsrId93' },
{ columnName: 'UsrGroupId93', payloadDdlName:'UsrGroupId93List', keyName:'UsrGroupId93',labelName:'UsrGroupId93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetUsrGroupId93List', actionTypeName: 'GET_DDL_UsrGroupId93' },
{ columnName: 'CompanyId93', payloadDdlName:'CompanyId93List', keyName:'CompanyId93',labelName:'CompanyId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCompanyId93List', actionTypeName: 'GET_DDL_CompanyId93' },
{ columnName: 'ProjectId93', payloadDdlName:'ProjectId93List', keyName:'ProjectId93',labelName:'ProjectId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetProjectId93List', actionTypeName: 'GET_DDL_ProjectId93' },
{ columnName: 'SystemId93', payloadDdlName:'SystemId93List', keyName:'SystemId93',labelName:'SystemId93Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetSystemId93List', actionTypeName: 'GET_DDL_SystemId93' },
{ columnName: 'MemberId93', payloadDdlName:'MemberId93List', keyName:'MemberId93',labelName:'MemberId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetMemberId93List', actionTypeName: 'GET_DDL_MemberId93' },
{ columnName: 'AgentId93', payloadDdlName:'AgentId93List', keyName:'AgentId93',labelName:'AgentId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetAgentId93List', actionTypeName: 'GET_DDL_AgentId93' },
{ columnName: 'BrokerId93', payloadDdlName:'BrokerId93List', keyName:'BrokerId93',labelName:'BrokerId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetBrokerId93List', actionTypeName: 'GET_DDL_BrokerId93' },
{ columnName: 'CustomerId93', payloadDdlName:'CustomerId93List', keyName:'CustomerId93',labelName:'CustomerId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCustomerId93List', actionTypeName: 'GET_DDL_CustomerId93' },
{ columnName: 'InvestorId93', payloadDdlName:'InvestorId93List', keyName:'InvestorId93',labelName:'InvestorId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetInvestorId93List', actionTypeName: 'GET_DDL_InvestorId93' },
{ columnName: 'VendorId93', payloadDdlName:'VendorId93List', keyName:'VendorId93',labelName:'VendorId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetVendorId93List', actionTypeName: 'GET_DDL_VendorId93' },
{ columnName: 'LenderId93', payloadDdlName:'LenderId93List', keyName:'LenderId93',labelName:'LenderId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetLenderId93List', actionTypeName: 'GET_DDL_LenderId93' },
{ columnName: 'BorrowerId93', payloadDdlName:'BorrowerId93List', keyName:'BorrowerId93',labelName:'BorrowerId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetBorrowerId93List', actionTypeName: 'GET_DDL_BorrowerId93' },
{ columnName: 'GuarantorId93', payloadDdlName:'GuarantorId93List', keyName:'GuarantorId93',labelName:'GuarantorId93Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetGuarantorId93List', actionTypeName: 'GET_DDL_GuarantorId93' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'SampleImage93', tableColumnName: 'SampleImage', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_SampleImage93' },
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
    GetScreenName(){return 'AdmUsrPref'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'UsrPrefId' :  'UsrPrefId93'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmUsrPrefService}
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
        UsrPrefId93: copy ? null : mst.UsrPrefId93,
		
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

  export default new AdmUsrPrefRedux()
            