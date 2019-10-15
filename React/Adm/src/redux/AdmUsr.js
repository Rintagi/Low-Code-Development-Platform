
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmUsrService from '../services/AdmUsrService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmUsrRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmUsr1List',
        'GET_MST' : 'GetAdmUsr1ById',
        'GET_DTL_LIST' : 'GetAdmUsr1DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'CultureId1', payloadDdlName:'CultureId1List', keyName:'CultureId1',labelName:'CultureId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId1List', actionTypeName: 'GET_DDL_CultureId1' },
{ columnName: 'DefCompanyId1', payloadDdlName:'DefCompanyId1List', keyName:'DefCompanyId1',labelName:'DefCompanyId1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDefCompanyId1List', actionTypeName: 'GET_DDL_DefCompanyId1' },
{ columnName: 'DefProjectId1', payloadDdlName:'DefProjectId1List', keyName:'DefProjectId1',labelName:'DefProjectId1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDefProjectId1List', actionTypeName: 'GET_DDL_DefProjectId1' },
{ columnName: 'DefSystemId1', payloadDdlName:'DefSystemId1List', keyName:'DefSystemId1',labelName:'DefSystemId1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetDefSystemId1List', actionTypeName: 'GET_DDL_DefSystemId1' },
{ columnName: 'UsrGroupLs1', payloadDdlName:'UsrGroupLs1List', keyName:'UsrGroupLs1',labelName:'UsrGroupLs1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetUsrGroupLs1List', actionTypeName: 'GET_DDL_UsrGroupLs1' },
{ columnName: 'UsrImprLink1', payloadDdlName:'UsrImprLink1List', keyName:'UsrImprLink1',labelName:'UsrImprLink1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetUsrImprLink1List', actionTypeName: 'GET_DDL_UsrImprLink1' },
{ columnName: 'CompanyLs1', payloadDdlName:'CompanyLs1List', keyName:'CompanyLs1',labelName:'CompanyLs1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetCompanyLs1List', actionTypeName: 'GET_DDL_CompanyLs1' },
{ columnName: 'ProjectLs1', payloadDdlName:'ProjectLs1List', keyName:'ProjectLs1',labelName:'ProjectLs1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetProjectLs1List', actionTypeName: 'GET_DDL_ProjectLs1' },
{ columnName: 'HintQuestionId1', payloadDdlName:'HintQuestionId1List', keyName:'HintQuestionId1',labelName:'HintQuestionId1Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetHintQuestionId1List', actionTypeName: 'GET_DDL_HintQuestionId1' },
{ columnName: 'InvestorId1', payloadDdlName:'InvestorId1List', keyName:'InvestorId1',labelName:'InvestorId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetInvestorId1List', actionTypeName: 'GET_DDL_InvestorId1' },
{ columnName: 'CustomerId1', payloadDdlName:'CustomerId1List', keyName:'CustomerId1',labelName:'CustomerId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCustomerId1List', actionTypeName: 'GET_DDL_CustomerId1' },
{ columnName: 'VendorId1', payloadDdlName:'VendorId1List', keyName:'VendorId1',labelName:'VendorId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetVendorId1List', actionTypeName: 'GET_DDL_VendorId1' },
{ columnName: 'AgentId1', payloadDdlName:'AgentId1List', keyName:'AgentId1',labelName:'AgentId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetAgentId1List', actionTypeName: 'GET_DDL_AgentId1' },
{ columnName: 'BrokerId1', payloadDdlName:'BrokerId1List', keyName:'BrokerId1',labelName:'BrokerId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetBrokerId1List', actionTypeName: 'GET_DDL_BrokerId1' },
{ columnName: 'MemberId1', payloadDdlName:'MemberId1List', keyName:'MemberId1',labelName:'MemberId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetMemberId1List', actionTypeName: 'GET_DDL_MemberId1' },
{ columnName: 'LenderId1', payloadDdlName:'LenderId1List', keyName:'LenderId1',labelName:'LenderId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetLenderId1List', actionTypeName: 'GET_DDL_LenderId1' },
{ columnName: 'BorrowerId1', payloadDdlName:'BorrowerId1List', keyName:'BorrowerId1',labelName:'BorrowerId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetBorrowerId1List', actionTypeName: 'GET_DDL_BorrowerId1' },
{ columnName: 'GuarantorId1', payloadDdlName:'GuarantorId1List', keyName:'GuarantorId1',labelName:'GuarantorId1Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetGuarantorId1List', actionTypeName: 'GET_DDL_GuarantorId1' },
      ]
      this.ScreenOnDemandDef = [
{ columnName: 'PicMed1', tableColumnName: 'PicMed', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_PicMed1' },
//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'UsrEmail10', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_UsrEmail10' },
{ columnName: 'UsrName20', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_UsrName20' },

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
    GetScreenName(){return 'AdmUsr'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'UsrId' :  'UsrId1'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmUsrService}
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
        UsrId1: copy ? null : mst.UsrId1,
		
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
|| (state.ScreenCriteria.UsrEmail10 || {}).LastCriteria
|| (state.ScreenCriteria.UsrName20 || {}).LastCriteria
|| (state.ScreenCriteria.UsrGroupLs30 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmUsrRedux()
            