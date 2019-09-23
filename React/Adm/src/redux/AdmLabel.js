
import { getAsyncTypes } from '../helpers/actionType'
import * as AdmLabelService from '../services/AdmLabelService'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class AdmLabelRedux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'GetAdmLabel112List',
        'GET_MST' : 'GetAdmLabel112ById',
        'GET_DTL_LIST' : 'GetAdmLabel112DtlById',
      }
      this.ScreenDdlDef = [
{ columnName: 'CultureId215', payloadDdlName:'CultureId215List', keyName:'CultureId215',labelName:'CultureId215Text', forMst: true, isAutoComplete:true, apiServiceName: 'GetCultureId215List', actionTypeName: 'GET_DDL_CultureId215' },
{ columnName: 'CompanyId215', payloadDdlName:'CompanyId215List', keyName:'CompanyId215',labelName:'CompanyId215Text', forMst: true, isAutoComplete:false, apiServiceName: 'GetCompanyId215List', actionTypeName: 'GET_DDL_CompanyId215' },
      ]
      this.ScreenOnDemandDef = [

//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
{ columnName: 'CultureId10', payloadDdlName: 'CultureId10List', isAutoComplete: true, apiServiceName: 'GetScreenCriCultureId10List', actionTypeName: 'GET_DDL_CRICultureId10' },
{ columnName: 'LabelCat20', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_LabelCat20' },
{ columnName: 'LabelKey30', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_LabelKey30' },
{ columnName: 'LabelText40', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_LabelText40' },
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
    GetScreenName(){return 'AdmLabel'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? 'LabelId' :  'LabelId215'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? ''  :''}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return AdmLabelService}
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
        LabelId215: copy ? null : mst.LabelId215,
		
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
|| (state.ScreenCriteria.CultureId10 || {}).LastCriteria
|| (state.ScreenCriteria.LabelCat20 || {}).LastCriteria
|| (state.ScreenCriteria.LabelKey30 || {}).LastCriteria
|| (state.ScreenCriteria.LabelText40 || {}).LastCriteria
      || state.ScreenCriteria.SearchStr;
  }

  export default new AdmLabelRedux()
            