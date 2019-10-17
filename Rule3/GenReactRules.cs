namespace RO.Rule3
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Linq;
    using RO.Common3;
    using RO.Common3.Data;
    using RO.SystemFramewk;
    using RO.Access3;
    using RO.WebRules;
    using System.Collections.Generic;

    public class GenReactRules
    {

        private short CultureId;
        private string dbConnectionString;
        private string dbPassword;
        private string system;
        private string fileDirectory;
        private byte DbId;

        public GenReactRules(short _CultureId, string _dbConnectionString, string _dbPassword, string _system, string _fileDirectory, byte _DbId)
        {
            CultureId = _CultureId;
            dbConnectionString = _dbConnectionString;
            dbPassword = _dbPassword;
            system = _system;
            fileDirectory = _fileDirectory;
            DbId = _DbId;
        }

        public bool CreateProgram(string ScreenId, string ScreenName)
        {
            try
            {
                string screenId = ScreenId;
                string screenName = ScreenName;

                DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
                string screenTypeName = dvItms[0]["ScreenTypeName"].ToString();

                if ("I1,I2".IndexOf(screenTypeName) >= 0)
                {
                    DataView dvReactRule = new DataView((new WebRule()).WrGetWebRule(screenId, dbConnectionString, dbPassword));

                    StringBuilder sbReactJsRoute = MakeReactJsRoute(screenId);
                    StringBuilder sbReactJsReduxIndex = MakeReactJsReduxIndex();
                    StringBuilder sbReactJsIndex = MakeReactJsIndex(screenId, screenName);
                    StringBuilder sbReactJsMstList = MakeReactJsMstList(screenId, screenName);
                    StringBuilder sbReactJsMstRecord = MakeReactJsMstRecord(screenId, screenName, dvReactRule);
                    StringBuilder sbReactJsRedux = MakeReactJsRedux(screenId, screenName, dvReactRule);
                    StringBuilder sbReactJsService = MakeReactJsService(screenId, screenName, dvReactRule);
                    StringBuilder sbAsmx = MakeReactJsAsmx(screenId, screenName);

                    StreamWriter sw = new StreamWriter(fileDirectory + "src/app/route.js");
                    try { sw.Write(sbReactJsRoute); }
                    finally { sw.Close(); }

                    sw = new StreamWriter(fileDirectory + "src/redux/index.js");
                    try { sw.Write(sbReactJsReduxIndex); }
                    finally { sw.Close(); }

                    string path = fileDirectory + "src/pages/" + screenName;
                    DirectoryInfo di = Directory.CreateDirectory(path);

                    sw = new StreamWriter(path + "/index.js");
                    try { sw.Write(sbReactJsIndex); }
                    finally { sw.Close(); }

                    sw = new StreamWriter(path + "/MstList.js");
                    try { sw.Write(sbReactJsMstList); }
                    finally { sw.Close(); }

                    sw = new StreamWriter(path + "/MstRecord.js");
                    try { sw.Write(sbReactJsMstRecord); }
                    finally { sw.Close(); }

                    if ("I2".IndexOf(screenTypeName) >= 0)
                    {
                        StringBuilder sbReactJsDtlList = MakeReactJsDtlList(screenId, screenName);
                        StringBuilder sbReactJsDtlRecord = MakeReactJsDtlRecord(screenId, screenName, dvReactRule);

                        sw = new StreamWriter(path + "/DtlList.js");
                        try { sw.Write(sbReactJsDtlList); }
                        finally { sw.Close(); }

                        sw = new StreamWriter(path + "/DtlRecord.js");
                        try { sw.Write(sbReactJsDtlRecord); }
                        finally { sw.Close(); }
                    }

                    sw = new StreamWriter(fileDirectory + "src/redux/" + di.Name + ".js");
                    try { sw.Write(sbReactJsRedux); }
                    finally { sw.Close(); }

                    sw = new StreamWriter(fileDirectory + "src/services/" + di.Name + "Service.js");
                    try { sw.Write(sbReactJsService); }
                    finally { sw.Close(); }

                    sw = new StreamWriter("C:/Rintagi/" + Config.AppNameSpace + "/Web/webservices/" + di.Name + "Ws.asmx");
                    try { sw.Write(sbAsmx); }
                    finally { sw.Close(); }
                }
                else
                {

                }
            }
            catch (Exception e) { ApplicationAssert.CheckCondition(false, "", "", e.Message); return false; }

            return true;
        }

        private StringBuilder MakeReactJsRoute(string screenId)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string SystemId = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    SystemId = drv["SystemId"].ToString();
                    break;
                }
            }

            List<string> ImportInitialResults = new List<string>();
            List<string> ExportDefaultResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            DataTable dt = (new RobotAccess()).GetScreenList("", dbConnectionString, dbPassword);
            if (dt != null)
            {
                string ImportInitialValue = "";
                string ExportDefaultValue = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string generateSc = dr["GenerateSc"].ToString();
                    string generateSr = dr["GenerateSr"].ToString();
                    string reactGenerated = dr["ReactGenerated"].ToString();
                    string programName = dr["ProgramName"].ToString();

                    if ((generateSc == "Y" || generateSr == "Y") && reactGenerated == "Y")
                    {
                        ImportInitialValue = "import {pagesRoutes as " + programName + "Route} from '../pages/" + programName + "/index'";
                        ExportDefaultValue = "..." + programName + "Route,";

                        ImportInitialResults.Add(ImportInitialValue);
                        ExportDefaultResults.Add(ExportDefaultValue);
                    }
                }
            }

            string ImportInitialCnt = string.Join(Environment.NewLine, ImportInitialResults.Select(s => addIndent(s, 0)));
            string ExportDefaultCnt = string.Join(Environment.NewLine, ExportDefaultResults.Select(s => addIndent(s, 0)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                import {pagesRoutes as AccountRoute} from '../pages/Account/index'
                import {pagesRoutes as SqlReportRoute} from '../pages/SqlReport/index'
                import {pagesRoutes as DefaultRoute} from '../pages/Default/index'
                /* all these are dynamic, add the required route for each page */
            ");
            sb.Append(ImportInitialCnt);
            sb.Append(@"
                export default [
                ...AccountRoute,
                ...DefaultRoute,
                // ...SqlReportRoute,
            ");
            sb.Append(ExportDefaultCnt);
            sb.Append(@"
                ];

            ");
            sb.Append(" document.Rintagi.systemId = '" + SystemId + "';");
            return sb;
        }

        private StringBuilder MakeReactJsReduxIndex()
        {

            List<string> ImportInitialResults = new List<string>();
            List<string> ExportDefaultResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            DataTable dt = (new RobotAccess()).GetScreenList("", dbConnectionString, dbPassword);
            if (dt != null)
            {
                string ImportInitialValue = "";
                string ExportDefaultValue = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string generateSc = dr["GenerateSc"].ToString();
                    string generateSr = dr["GenerateSr"].ToString();
                    string reactGenerated = dr["ReactGenerated"].ToString();
                    string programName = dr["ProgramName"].ToString();

                    if ((generateSc == "Y" || generateSr == "Y") && reactGenerated == "Y")
                    {
                        ImportInitialValue = "import " + programName + "ReduxObj from './" + programName + "';";
                        ExportDefaultValue = programName + ": " + programName + "ReduxObj.ReduxReducer(),";

                        ImportInitialResults.Add(ImportInitialValue);
                        ExportDefaultResults.Add(ExportDefaultValue);
                    }
                }
            }

            string ImportInitialCnt = string.Join(Environment.NewLine, ImportInitialResults.Select(s => addIndent(s, 0)));
            string ExportDefaultCnt = string.Join(Environment.NewLine, ExportDefaultResults.Select(s => addIndent(s, 0)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                import { sidebarReducer } from './SideBar';
                import { authReducer } from './Auth';
                import { notificationReducer } from './Notification';
                import { globalReducer } from './Global';
                import { SqlReportReducer } from './SqlReport';

                /* below are dynamic, put shared static one above this */
            ");
            sb.Append(ImportInitialCnt);
            sb.Append(@"
              export default {
              auth: authReducer,
              global: globalReducer,
              sidebar: sidebarReducer,
              notification: notificationReducer,
              SqlReport: SqlReportReducer,

              /* dynamic go to here */
            ");
            sb.Append(ExportDefaultCnt);
            sb.Append(@"
              }
            ");
            return sb;
        }

        //React Page Generation
        private StringBuilder MakeReactJsIndex(string screenId, string screenName)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenPrimaryKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }
            string screenDetailKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }

            string ImportRouter = "";
            string DetailRouter = "";
            string NaviPath = "";
            string screenTypeName = dvItms[0]["ScreenTypeName"].ToString();

            if ("I1".IndexOf(screenTypeName) >= 0)
            {
                NaviPath = "path: naviPath(v.type === 'MstList' ? '_' : mst.[[---ScreenPrimaryKey---]], '', v.path),";
            }
            else if ("I2".IndexOf(screenTypeName) >= 0)
            {
                ImportRouter = @" import DtlList from './DtlList';
                                  import DtlRecord from './DtlRecord';";
                DetailRouter = @"
                    {
                        path: '/[[---ScreenName---]]/:mstId?/DtlList/:dtlId?',
                        name: 'Detail List',
                        short: 'DetailList',
                        component: DtlList,
                        icon: 'list-ul',
                        isPublic: false,
                        type: 'DtlList',
                        order: 3,
                        screenId : [[---ScreenId---]]
                      },
                      {
                        path: '/[[---ScreenName---]]/:mstId?/Dtl/:dtlId?',
                        name: 'Detail Record',
                        short: 'DetailRecord',
                        component: DtlRecord,
                        icon: 'picture-o',
                        isPublic: false,
                        type: 'DtlRecord',
                        order: 4,
                        screenId : [[---ScreenId---]]
                      },";

                NaviPath = "path: naviPath(v.type === 'MstList' ? '_' : mst.[[---ScreenPrimaryKey---]], v.type === 'DtlList' ? '_' : dtl.[[---ScreenDetailKey---]], v.path),";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
/* this is external interface(for import) for this screen */
import { naviPath } from '../../helpers/utils'
import MstList from './MstList';
import MstRecord from './MstRecord';
   ");
            sb.Append(ImportRouter);
            sb.Append(@"
/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */
export const pagesRoutes = [
  {
    path: '/[[---ScreenName---]]/Mst/:mstId?',
    name: 'Master Record',
    short: 'MasterRecord',
    component: MstRecord,
    icon: 'briefcase',
    isPublic: false,
    type: 'MstRecord',
    order: 2,
    screenId : [[---ScreenId---]]
  },
   ");
            sb.Append(DetailRouter);
            sb.Append(@"
  {
    path: '/[[---ScreenName---]]/:mstId?',
    name: 'Master List',
    short: 'MasterList',
    component: MstList,
    icon: 'usd',
    isPublic: false,
    type: 'MstList',
    order: 1,
    inMenu: true,
    menuLabel: '[[---ScreenName---]]',
    screenId : [[---ScreenId---]]
  },
]

function naviLabel(label, type) {
  if (type === 'MstList') return ((label || {}).MasterLst || {}).label || '';
  else if (type === 'MstRecord') return ((label || {}).MasterRec || {}).label || '';
  else if (type === 'DtlList') return ((label || {}).DetailLst || {}).label || '';
  else if (type === 'DtlRecord') return ((label || {}).DetailRec || {}).label || '';
  else return type;
}

export function getNaviBar(type, mst, dtl, label) {
  return pagesRoutes
    .sort((a, b) => a.order - b.order)
    .map((v, i) => {
      return {
        ");
            sb.Append(NaviPath);
            sb.Append(@"
        label: naviLabel(label, v.type),
        type: v.type,
        active: v.type === type,
      }
    });
}
            ").Replace("[[---ScreenName---]]", screenName)
              .Replace("[[---ScreenPrimaryKey---]]", screenPrimaryKey)
              .Replace("[[---ScreenDetailKey---]]", screenDetailKey)
              .Replace("[[---ScreenId---]]", screenId);

            return sb;
        }

        private StringBuilder MakeReactJsMstList(string screenId, string screenName)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenPrimaryKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }

            List<string> GetCriteriaFuncResults = new List<string>();
            List<string> RefreshSearchListResults = new List<string>();
            List<string> RenderInitialCriResults = new List<string>();
            List<string> FormikInitialCriResults = new List<string>();
            List<string> FormikControlResults = new List<string>();
            List<string> GetCriteriaBotResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            //Screen Criteria
            DataTable dtScrCri = (new AdminAccess()).GetScrCriteria(screenId, dbConnectionString, dbPassword);
            foreach (DataRow dr in dtScrCri.Rows)
            {
                string screenCriId = dr["ScreenCriId"].ToString();
                string columnName = dr["ColumnName"].ToString();
                string displayMode = dr["DisplayMode"].ToString();
                string DdlKeyColumnName = dr["DdlKeyColumnName"].ToString();
                string DdlRefColumnName = dr["DdlRefColumnName"].ToString();
                string GetCriteriaFuncValue = "";
                string RefreshSearchListValue = "";
                string RenderInitialCriValue = "";
                string FormikInitialCriValue = "";
                string FormikControlValue = "";
                string GetCriteriaBotValue = "";

                if (displayMode == "TextBox")
                {
                    FormikControlValue = "<Col xs={12} md={12}>" + Environment.NewLine
                                          + "<label className='form__form-group-label filter-label'>{(screenCriteria." + columnName + " || {}).ColumnHeader}</label>" + Environment.NewLine
                                          + "<div className='form__form-group-field filter-form-border'>" + Environment.NewLine
                                            + "<Field" + Environment.NewLine
                                               + "type='text'" + Environment.NewLine
                                               + "name='cCri" + columnName + "'" + Environment.NewLine
                                               + "value={values.cCri" + columnName + "}" + Environment.NewLine
                                               + "onBlur = {this.SearchFilterTextValueChange(handleSubmit, setFieldValue, 'text', 'cCri" + columnName + "')}" + Environment.NewLine
                                         + "/>" + Environment.NewLine
                                          + "</div>" + Environment.NewLine
                                        + "</Col>";

                    FormikInitialCriValue = "cCri" + columnName + ": ((screenCriteria || {})." + columnName + " || {}).LastCriteria,";

                    RefreshSearchListValue = columnName + ": (values.cCri" + columnName + ") ? values.cCri" + columnName + " : ''," + Environment.NewLine;
                }
                else if (displayMode == "AutoComplete")
                {
                    GetCriteriaFuncValue = "Cri" + columnName + "InputChange() {" + Environment.NewLine
                                           + "return function (name, v) {" + Environment.NewLine
                                             + "this.props.SearchCri" + columnName + "(v);" + Environment.NewLine
                                           + "}.bind(this);" + Environment.NewLine
                                         + "}";

                    FormikControlValue = "<Col xs={12} md={12}>" + Environment.NewLine
                                          + "<label className='form__form-group-label filter-label'>{(screenCriteria." + columnName + " || {}).ColumnHeader}</label>" + Environment.NewLine
                                          + "<div className='form__form-group-field filter-form-border'>" + Environment.NewLine
                                            + "<AutoCompleteField" + Environment.NewLine
                                              + "name='cCri" + columnName + "'" + Environment.NewLine
                                              + "onChange={this.SearchFilterValueChange(handleSubmit, setFieldValue, 'autocomplete', 'cCri" + columnName + "')}" + Environment.NewLine
                                              + "onInputChange={this.Cri" + columnName + "InputChange()}" + Environment.NewLine
                                              + "value={values.cCri" + columnName + "}" + Environment.NewLine
                                              + "defaultSelected={Cri" + columnName + "Selected}" + Environment.NewLine
                                              + "options={Cri" + columnName + "List}" + Environment.NewLine
                                            + "/>" + Environment.NewLine
                                          + "</div>" + Environment.NewLine
                                        + "</Col>";

                    GetCriteriaBotValue = "{ SearchCri" + columnName + ": [[---ScreenName---]]ReduxObj.SearchActions.SearchCri" + columnName + ".bind([[---ScreenName---]]ReduxObj) },";
                    RenderInitialCriValue = "const Cri" + columnName + "List = screenCriDdlSelectors." + columnName + "([[---ScreenName---]]State);" + Environment.NewLine
                                     + "const Cri" + columnName + "Selected = Cri" + columnName + "List.filter(obj => { return obj.key === screenCriteria." + columnName + ".LastCriteria });";

                    FormikInitialCriValue = "cCri" + columnName + ": Cri" + columnName + "Selected[0],";

                    RefreshSearchListValue = columnName + ": (values.cCri" + columnName + ") ? values.cCri" + columnName + ".value : ''," + Environment.NewLine;
                }
                else if (displayMode == "DropDownList")
                {
                    FormikControlValue = "<Col xs={12} md={12}>" + Environment.NewLine
                                          + "<label className='form__form-group-label filter-label'>{(screenCriteria." + columnName + " || {}).ColumnHeader}</label>" + Environment.NewLine
                                          + "<div className='form__form-group-field filter-form-border'>" + Environment.NewLine
                                            + "<DropdownField" + Environment.NewLine
                                              + "name='cCri" + columnName + "'" + Environment.NewLine
                                              + "onChange={this.SearchFilterValueChange(handleSubmit, setFieldValue, 'ddl', 'cCri" + columnName + "')}" + Environment.NewLine
                                              + "value={values.cCri" + columnName + "}" + Environment.NewLine
                                              + "options={Cri" + columnName + "List}" + Environment.NewLine
                                              + "placeholder=''" + Environment.NewLine
                                            + "/>" + Environment.NewLine
                                          + "</div>" + Environment.NewLine
                                        + "</Col>";
                    RenderInitialCriValue = "const Cri" + columnName + "List = screenCriDdlSelectors." + columnName + "([[---ScreenName---]]State);" + Environment.NewLine
                                     + "const Cri" + columnName + "Selected = Cri" + columnName + "List.filter(obj => { return obj.key === screenCriteria." + columnName + ".LastCriteria });";

                    FormikInitialCriValue = "cCri" + columnName + ": Cri" + columnName + "Selected[0],";

                    RefreshSearchListValue = columnName + ": (values.cCri" + columnName + ") ? values.cCri" + columnName + ".value : ''," + Environment.NewLine;
                }





                GetCriteriaFuncResults.Add(GetCriteriaFuncValue);
                RefreshSearchListResults.Add(RefreshSearchListValue);
                RenderInitialCriResults.Add(RenderInitialCriValue);
                FormikInitialCriResults.Add(FormikInitialCriValue);
                FormikControlResults.Add(FormikControlValue);
                GetCriteriaBotResults.Add(GetCriteriaBotValue);
            }

            string GetWebRules = screenName == "XrsBtrxGen"
                                 ? "FormatSearchSubTitleL = (v) =>{ return toLocalDateFormat(v); }" + Environment.NewLine
                                   + "FormatSearchTitleR = (v) =>{ return toLocalAmountFormat(v); }" + Environment.NewLine
                                 : "";

            string GetCriteriaFuncCnt = string.Join(Environment.NewLine, GetCriteriaFuncResults.Select(s => addIndent(s, 0)));
            string RefreshSearchListCnt = string.Join(Environment.NewLine, RefreshSearchListResults.Select(s => addIndent(s, 0)));
            string RenderInitialCriCnt = string.Join(Environment.NewLine, RenderInitialCriResults.Select(s => addIndent(s, 0)));
            string FormikInitialCriCnt = string.Join(Environment.NewLine, FormikInitialCriResults.Select(s => addIndent(s, 0)));
            string FormikControlCnt = string.Join(Environment.NewLine, FormikControlResults.Select(s => addIndent(s, 0)));
            string GetCriteriaBotCnt = string.Join(Environment.NewLine, GetCriteriaBotResults.Select(s => addIndent(s, 0)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Redirect,withRouter } from 'react-router';
import { Formik, Field, Form } from 'formik';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import LoadingIcon from 'mdi-react/LoadingIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import PlusIcon from 'mdi-react/PlusIcon';
import MagnifyIcon from 'mdi-react/MagnifyIcon';
import RadioboxBlankIcon from 'mdi-react/RadioboxBlankIcon';
import classNames from 'classnames';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import DropdownField from '../../components/custom/DropdownField';
import NaviBar from '../../components/custom/NaviBar';
import RintagiScreen from '../../components/custom/Screen'
import ModalDialog from '../../components/custom/ModalDialog';
import { getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import [[---ScreenName---]]ReduxObj, { ShowMstFilterApplied } from '../../redux/[[---ScreenName---]]';
import { setTitle, setSpinner } from '../../redux/Global';
import { getNaviBar } from './index';
import MstRecord from './MstRecord';
import DocumentTitle from 'react-document-title';

class MstList extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.[[---ScreenName---]] || {});
    this.hasChangedContent = false;
    this.titleSet = false;
    this.MstKeyColumnName = '[[---ScreenPrimaryKey---]]';
    this.SystemName = 'FintruX';
    this.SetCurrentRecordState = this.SetCurrentRecordState.bind(this);
    this.SearchBoxFocus = this.SearchBoxFocus.bind(this);
    this.SelectMstListRow = this.SelectMstListRow.bind(this);
    this.FilterValueChange = this.SearchFilterValueChange.bind(this);
    this.ShowMoreSearchList = this.ShowMoreSearchList().bind(this);
    this.RefreshSearchList = this.RefreshSearchList.bind(this);
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.OnMstCopy = () => { this.setState({ ShowMst: true }) };
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      Buttons: {},
      ScreenButton: null,
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      ShowMst: false,
      isMobile: false,
    };
//    const lastAppUrl = GetCurrent('LastAppUrl',true);
      const lastAppUrl = null;
 
    if (lastAppUrl && !(this.props.[[---ScreenName---]] || {}).initialized) {
      if (lastAppUrl.pathname !== ((this.props.history || {}).location || {}).pathname) {
          this.props.history.push(lastAppUrl.pathname);
      }
    }
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }

 ");
            sb.Append(GetWebRules);
            sb.Append(GetCriteriaFuncCnt);
            sb.Append(@"

  /* standard screen button actions for each screen, must implement if button defined */
  Print({ mst, mstId }) {
    return function (evt) { }.bind(this);
  }

  CopyHdr({ naviBar, ScreenButton, useMobileView, mst, mstId }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || mst.[[---ScreenPrimaryKey---]];
      const copyFn = () => {
        this.props.AddMst(fromMstId, 'MstList', 0);
        this.setState({
          ScreenButton: ScreenButton.buttonType,
          key: fromMstId,
          ShowMst: true,
        });
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  AddNewMst({ naviBar, useMobileView }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    if (useMobileView) return super.AddNewMst({ naviBar });
    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddMst(null, 'MstList', 0);
        this.setState({
          ScreenButton: 'New',
          ShowMst: true,
        });
      }
      if(!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  };

  AddNewDtl({ naviBar, mstId, useMobileView }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    if (useMobileView) return super.AddNewDtl({ naviBar, mstId });
    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddDtl(mstId, null, -1);
        this.props.history.push(getAddDtlPath(getNaviPath(naviBar, 'DtlList', '/')) + '_');
      }
      if(!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  };

  DelMst({ naviBar, ScreenButton, mst, mstId }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return this.Prompt({
      okFn: function(evt) {
        const fromMstId = mstId || mst.[[---ScreenPrimaryKey---]];
        this.props.DelMst(this.props.[[---ScreenName---]], fromMstId);        
      }.bind(this) ,
      message: auxSystemLabels.DeletePageMsg || ''
    }).bind(this); 
  }

  ExpMstTxt() {
    return function (evt) { }.bind(this);
  }
  /* end of screen button action */



  RefreshSearchList(values, { setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const refreshFn = (() => {
      this.props.SetScreenCriteria(values.search,
        {
           ");
            sb.Append(RefreshSearchListCnt);
            sb.Append(@"
        },
        (values.cFilterId) ? values.cFilterId.value : 0);
      }).bind(this);
    if(true || !this.hasChangedContent) refreshFn();
    else this.setState({ ModalOpen: true, ModalSuccess: refreshFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
  }

  /* react related calls */
  static getDerivedStateFromProps(nextProps, prevState) {
    const buttons = (nextProps.[[---ScreenName---]] || {}).Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'MstList', prevState);

    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;
    return revisedState;
  }

  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

  componentDidMount() {
    const { mstId } = { ...this.props.match.params };
    if (!(this.props.[[---ScreenName---]] || {}).AuthCol || true) {
      this.props.LoadPage('SearchList', { mstId: mstId || '_' });
      this.props.LoadInitPage({ keyId: null });
    }

    this.mediaqueryresponse(this.mobileView);

    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes

  }

  componentDidUpdate(prevProps, prevStates) {
    const currReduxScreenState = this.props.[[---ScreenName---]] || {};
    // const isMobileView = this.state.isMobile;
    // const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

    if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
      const _this = this;
      setTimeout(() => _this.props.setSpinner(false), 500);
    }
    // else if(currReduxScreenState.initialized && this.props.global.pageSpinner) {
    //   this.props.setSpinner(false);
    // }
    
    this.SetPageTitle(currReduxScreenState);

    if (prevStates.key !== (currReduxScreenState.Mst || {}).[[---ScreenPrimaryKey---]]) {
      const isMobileView = this.state.isMobile;
      const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

      if (prevStates.ScreenButton === 'Copy' && useMobileView) {
        const naviBar = getNaviBar('MstList', {}, {}, currReduxScreenState.Label);
        const editMstPath = getEditMstPath(getNaviPath(naviBar, 'Mst', '/'), '_');
        this.props.history.push(editMstPath);
      }
    }
  }

  componentWillUnmount() {
    this.mobileView.removeListener(this.mediaqueryresponse);
  }

  render() {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};

    if ([[---ScreenName---]]State.access_denied) {
      return <Redirect to='/error' />;
    }

    // if (![[---ScreenName---]]State.initialized || [[---ScreenName---]]State.page_loading) return null;
    const screenHlp = [[---ScreenName---]]State.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterLstTitle = ((screenHlp || {}).MasterLstTitle || '');
    const MasterLstSubtitle = ((screenHlp || {}).MasterLstSubtitle || '');
    const IncrementMsg = ((screenHlp || {}).IncrementMsg || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');
    const AddMasterMsg = ((screenHlp || {}).AddMasterMsg || '');
    const MasterFoundMsg = ((screenHlp || {}).MasterFoundMsg || '');

    const screenButtons = [[---ScreenName---]]ReduxObj.GetScreenButtons([[---ScreenName---]]State);
    const screenDdlSelectors = [[---ScreenName---]]ReduxObj.ScreenDdlSelectors;
    const screenCriDdlSelectors = [[---ScreenName---]]ReduxObj.ScreenCriDdlSelectors;
    const screenCriteria = [[---ScreenName---]]State.ScreenCriteria || {}
    const selectList = [[---ScreenName---]]ReduxObj.SearchListToSelectList([[---ScreenName---]]State);
    const itemList = [[---ScreenName---]]State.Dtl || [];
    const auxLabels = [[---ScreenName---]]State.Label || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const columnLabel = [[---ScreenName---]]State.ColumnLabel || {};
    const currMst = [[---ScreenName---]]State.Mst;
    const currDtl = [[---ScreenName---]]State.EditDtl;
    const naviBar = getNaviBar('MstList', currMst, {}, screenButtons).filter(v=>((v.type !== 'Dtl' && v.type !=='DtlList') || currMst.[[---ScreenPrimaryKey---]]));
    const naviSelectBar = getNaviBar('MstList', {}, {}, screenButtons);
    const screenFilterList = [[---ScreenName---]]ReduxObj.QuickFilterDdlToSelectList([[---ScreenName---]]State);
    const screenFilterSelected = screenFilterList.filter(obj => { return obj.key === screenCriteria.FilterId });
    const authRow = ([[---ScreenName---]]State.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, rowMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    let colorDark = classNames({
      // 'color-dark': haveAllButtons
    });

    let noMargin = classNames({ 'mb-0': !hasBottomButton });

    // Filter visibility
    let filterVisibility = classNames({ 'd-none': true, 'd-block': screenCriteria.ShowFilter });
    let filterBtnStyle = classNames({ 'filter-button-clicked': screenCriteria.ShowFilter });
    let filterActive = classNames({ 'filter-icon-active': screenCriteria.ShowFilter });

 ");
            sb.Append(RenderInitialCriCnt);
            sb.Append(@"

    const hasScreenFilter = screenFilterList.length > 0;
    const activeSelectionVisible = selectList.filter(v => v.isSelected).length > 0;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const getMainRowDesc = (desc) => (desc.replace(/^[0-9.]+\s\[\w*\]/));
    return (
      <DocumentTitle title={siteTitle}>
      <div className='top-level-split'>
        <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
        <Row className='no-margin-right'>
          <Col className='no-padding-right' xs='6'>
            <div className='account'>
              <div className='account__wrapper account-col'>
                <div className='account__card rad-4'>
                  {/* {this.constructor.ShowSpinner([[---ScreenName---]]State,'MstList') && siteSpinner} */}
                  {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                    <div className='tabs__wrap'>
                      <NaviBar history={this.props.history} navi={naviBar} key={currMst.key} mstListCount={(selectList||[]).length} />
                    </div>
                  </div>}
                  <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                  <div className='account__head'>
                    <Row>
                      <Col xs={8}>
                        <h3 className='account__title'>{MasterLstTitle}</h3>
                        <h4 className='account__subhead subhead'>{MasterLstSubtitle}</h4>
                      </Col>
                      <Col xs={4}>
                        <ButtonToolbar className='f-right'>
                          <UncontrolledDropdown>
                            <ButtonGroup className='btn-group--icons'>
                              <Button className={`mw-50 ${filterBtnStyle}`} onClick={this.props.changeMstListFilterVisibility} outline>
                                <i className={`fa fa-filter icon-holder ${filterActive}`}></i>{ShowMstFilterApplied([[---ScreenName---]]State) && <i className='filter-applied'></i>}
                                {!useMobileView && <p className='action-menu-label'>{(screenButtons.Filter || {}).label}</p>}
                              </Button>
                              {
                                dropdownMenuButtonList.filter(v => v.expose).map(v => {
                                  return (
                                    <Button
                                      key={v.tid || v.buttonType}
                                      // disabled={!activeSelectionVisible || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]])}
                                      onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}
                                      className='mw-50 add-report-expense'
                                      outline>
                                      <i className={`${v.iconClassName} icon-holder`}></i>
                                      {!useMobileView && <p className='action-menu-label'>{v.label}</p>}
                                    </Button>)
                                })
                              }
                              {activeSelectionVisible &&
                                dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                                <DropdownToggle className='mw-50' outline>
                                  <i className='fa fa-ellipsis-h icon-holder'></i>
                                  {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                </DropdownToggle>
                              }
                            </ButtonGroup>
                            {dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                              <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                {
                                  dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                    return (
                                      <DropdownItem
                                        key={v.tid || v.order}
                                        disabled={!activeSelectionVisible || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]])}
                                        onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}
                                        className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                  })
                                }
                              </DropdownMenu>
                            }
                          </UncontrolledDropdown>
                        </ButtonToolbar>
                      </Col>
                    </Row>
                  </div>
                  <Formik
                    initialValues={{

 ");
            sb.Append(FormikInitialCriCnt);
            sb.Append(@"
                      search: screenCriteria.SearchStr || '',
                      cFilterId: (screenFilterSelected.length > 0 ? screenFilterSelected[0] : screenFilterList[0])
                    }}
                    key={[[---ScreenName---]]State.searchListVersion}
                    onSubmit={this.RefreshSearchList}
                    render={({
                      values,
                      isSubmitting,
                      handleChange,
                      handleSubmit,
                      handleBlur,
                      setFieldValue,
                      setFieldTouched
                    }) => (
                        <div>
                          <Form className='form'>
                            <div className='form__form-group'>
                              <div className={`form__form-group filter-padding ${filterVisibility}`} key={screenCriteria.key}>
                                <Row className='mb-5'>
                                  ");
            sb.Append(FormikControlCnt);
            sb.Append(@"
                                </Row>
                                <Row>
                                  {hasScreenFilter && <Col xs={4} md={3}>
                                    <label className='form__form-group-label filter-label'>{auxSystemLabels.QFilter}</label>
                                    <div className='form__form-group-field filter-form-border'>
                                      <DropdownField
                                        name='cFilterId'
                                        onBlur={setFieldTouched}
                                        // onChange={setFieldValue}
                                        onChange={this.SearchFilterValueChange(handleSubmit, setFieldValue, 'ddl', 'cFilterId')}
                                        value={values.cFilterId}
                                        options={screenFilterList}
                                      />
                                    </div>
                                  </Col>}
                                  <Col xs={hasScreenFilter ? 8 : 12} md={hasScreenFilter ? 9 : 12} className={hasScreenFilter ? 'col-last-modified' : ''}>
                                    <label className='form__form-group-label filter-label'>{auxSystemLabels.FilterSearchLabel}</label>
                                    <div className='form__form-group-field filter-form-border'>
                                      <Field
                                        className='white-left-border'
                                        type='text'
                                        name='search'
                                        value={values.search}
                                        onFocus={(e) => this.SearchBoxFocus(e)}
                                        onBlur={(e) => this.SearchBoxFocus(e)}
                                      />
                                      <span onClick={handleSubmit} className={`form__form-group-button desktop-filter-button search-btn-fix ${this.state.searchFocus ? ' active' : ''}`}><MagnifyIcon /></span>
                                    </div>
                                  </Col>
                                </Row>
                              </div>
                              <h5 className='fill-fintrux pb-10 fw-700'><span className='color-dark mr-5'>{MasterFoundMsg}:</span> {screenCriteria.MatchCount}</h5>
                              {selectList.map((obj, i) => {
                                return (
                                  <div className='form__form-group-narrow list-divider' key={i}>
                                    <div className='form__form-group-field'>
                                      <label className='radio-btn radio-btn--button margin-narrow'>
                                        <Field
                                          className='radio-btn__radio'
                                          name='[[---ScreenPrimaryKey---]]'
                                          listidx={obj.idx}
                                          keyid={obj.key}
                                          type='radio'
                                          value={obj.key || ''}
                                          onClick={this.SelectMstListRow({naviBar:naviSelectBar,useMobileView})}
                                          defaultChecked={obj.isSelected ? true : false}
                                        />
                                        <span className='radio-bg-solver'></span>
                                        {hasActableButtons &&
                                          <span className='radio-btn__label-svg'>
                                            <CheckIcon className='radio-btn__label-check' />
                                            <RadioboxBlankIcon className='radio-btn__label-uncheck' />
                                          </span>
                                        }
                                        <span className={`radio-btn__label ${colorDark}`}>
                                          <div className='row-cut'>{this.FormatSearchTitleL(obj.label)}</div>
                                          <div className='row-cut row-bottom-report'>{this.FormatSearchSubTitleL(obj.detail)}</div>
                                        </span>
                                        <span className={`radio-btn__label__right ${colorDark}`}>
                                          <div>{this.FormatSearchTitleR(obj.labelR)}</div>
                                          <div className='row-bottom-report f-right'>{this.FormatSearchSubTitleR(obj.detailR)}</div>
                                        </span>
                                        {
                                          rowMenuButtonList
                                            .filter(v => v.expose && !useMobileView)
                                            .map((v, i) => {
                                              if (this.ActionSuppressed(authRow, v.buttonType, obj.key)) return null;
                                              return (
                                                <button type='button' key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, useMobileView, mstId: obj.key })} className={`${v.exposedClassName}`}><i className={`${v.iconClassName}`}></i></button>
                                              )
                                            })
                                        }
                                        {
                                          rowMenuButtonList.filter(v => !v.expose || useMobileView).length > 0 &&
                                          <UncontrolledDropdown className={`btn-row-dropdown`}>
                                            <DropdownToggle className='btn-row-dropdown-icon btn-row-menu' onClick={(e) => { e.preventDefault() }}>
                                              <i className='fa fa-ellipsis-h icon-holder menu-icon'></i>
                                            </DropdownToggle>
                                            <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                              {rowMenuButtonList
                                                .filter(v => !v.expose || useMobileView)
                                                .map((v) => {
                                                  if (this.ActionSuppressed(authRow, v.buttonType, obj.key)) return null;
                                                  return <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, useMobileView, mstId: obj.key })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.labelLong}</DropdownItem>
                                                })
                                              }
                                            </DropdownMenu>
                                          </UncontrolledDropdown>
                                        }
                                      </label>
                                    </div>
                                  </div>
                                )
                              })}
                            </div>
                            {this.constructor.ShowMoreMstBtn([[---ScreenName---]]State) && <Button className={`btn btn-view-more-blue account__btn ${noMargin}`} onClick={this.ShowMoreSearchList} type='button'>{strFormat(IncrementMsg, [[---ScreenName---]]State.ScreenCriteria.Increment)}<br /><i className='fa fa-arrow-down'></i></Button>}
                            {useMobileView && activeSelectionVisible &&
                              bottomButtonList.filter(v => v.expose).length > 0 &&
                              <div className='width-wrapper'>
                                <div className='buttons-bottom-container'>
                                  <Row className='btn-bottom-row'>
                                    {
                                      bottomButtonList
                                        .filter(v => v.expose)
                                        .map((v, i, a) => {
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]])) return null;
                                          const buttonCount = a.length;
                                          const colWidth = parseInt(12 / buttonCount, 10);
                                          const lastBtn = i === a.length - 1;
                                          const outlineProperty = lastBtn ? false : true;
                                          return (
                                            <Col key={v.tid} xs={colWidth} sm={colWidth} className='btn-bottom-column'>
                                              <Button color='success' type='button' outline={outlineProperty} className='account__btn' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}>{v.labelLong}</Button>
                                            </Col>
                                          )
                                        })
                                    }
                                  </Row>
                                </div>
                              </div>
                            }
                          </Form>
                        </div>
                      )}
                  />
                </div>
              </div>
            </div>
          </Col>
          {!useMobileView &&
            <Col xs={6}>
              {!activeSelectionVisible &&
                !this.state.ShowMst &&
                <div className='empty-block'>
                  <img className='folder-img' alt='' src={require('../../img/folder.png')} />
                  <p className='create-new-message'>{NoMasterMsg}. <span className='link-imitation' onClick={this.AddNewMst({ naviBar: naviSelectBar, useMobileView })}>{AddMasterMsg}</span></p>
                </div>
              }

              {(activeSelectionVisible || this.state.ShowMst) && <MstRecord key={currMst.key} history={this.props.history} updateChangedState={this.SetCurrentRecordState} onCopy={this.OnMstCopy} suppressLoadPage={true} />}

            </Col>}
        </Row>

      </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  error: state.error,
  [[---ScreenName---]]: state.[[---ScreenName---]],
  filter: state.filter,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: [[---ScreenName---]]ReduxObj.LoadPage.bind([[---ScreenName---]]ReduxObj) },
    { LoadInitPage: [[---ScreenName---]]ReduxObj.LoadInitPage.bind([[---ScreenName---]]ReduxObj) },
    { LoadSearchList: [[---ScreenName---]]ReduxObj.LoadSearchList.bind([[---ScreenName---]]ReduxObj) },
    { SelectMst: [[---ScreenName---]]ReduxObj.SelectMst.bind([[---ScreenName---]]ReduxObj) },
    { DelMst: [[---ScreenName---]]ReduxObj.DelMst.bind([[---ScreenName---]]ReduxObj) },
    { AddMst: [[---ScreenName---]]ReduxObj.AddMst.bind([[---ScreenName---]]ReduxObj) },
    { AddDtl: [[---ScreenName---]]ReduxObj.AddDtl.bind([[---ScreenName---]]ReduxObj) },    
    { changeMstListFilterVisibility: [[---ScreenName---]]ReduxObj.ChangeMstListFilterVisibility.bind([[---ScreenName---]]ReduxObj) },
    { SetScreenCriteria: [[---ScreenName---]]ReduxObj.SetScreenCriteria.bind([[---ScreenName---]]ReduxObj) },
 ");
            sb.Append(GetCriteriaBotCnt);
            sb.Append(@"
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MstList));

            ").Replace("[[---ScreenName---]]", screenName).Replace("[[---ScreenPrimaryKey---]]", screenPrimaryKey);

            return sb;
        }

        private StringBuilder MakeReactJsMstRecord(string screenId, string screenName, DataView dvReactRule)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenPrimaryKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }
            string screenDetailKeyName = "";
            string screenDetailKey = "";
            string screenDetailTableName = "";
            List<string> MasterCustomFunctionResults = new List<string>();
            List<string> MasterSaveResults = new List<string>();
            List<string> MasterRenderResults = new List<string>();
            List<string> MasterScreenColumnResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            foreach (DataRowView drv in dvReactRule)
            {
                if (drv["ReactEventId"].ToString() == "1") //Master Record Custom Function
                {
                    string MasterCustomFunctionValue = drv["ReactRuleProg"].ToString();
                    MasterCustomFunctionResults.Add(MasterCustomFunctionValue);
                }
                else if (drv["ReactEventId"].ToString() == "2") //Master Record Save
                {
                    string MasterSaveValue = drv["ReactRuleProg"].ToString();
                    MasterSaveResults.Add(MasterSaveValue);
                }
                else if (drv["ReactEventId"].ToString() == "3") //Master Record Render
                {
                    string MasterRenderValue = drv["ReactRuleProg"].ToString();
                    MasterRenderResults.Add(MasterRenderValue);
                }
                //This is not being used yet. No test case yet. 
                else if (drv["ReactEventId"].ToString() == "4") //Master Record Screen Column
                {
                    string MasterScreenColumnValue = drv["ReactRuleProg"].ToString();
                    MasterScreenColumnResults.Add(MasterScreenColumnValue);
                }
            }

            string MasterCustomFunctionCnt = string.Join(Environment.NewLine, MasterCustomFunctionResults.Select(s => addIndent(s, 24)));
            string MasterSaveCnt = string.Join(Environment.NewLine, MasterSaveResults.Select(s => addIndent(s, 24)));
            string MasterRenderCnt = string.Join(Environment.NewLine, MasterRenderResults.Select(s => addIndent(s, 24)));
            string MasterScreenColumnCnt = string.Join(Environment.NewLine, MasterScreenColumnResults.Select(s => addIndent(s, 24)));

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKeyName = drv["PrimaryKey"].ToString();
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    screenDetailTableName = drv["TableName"].ToString();
                    break;
                }
            }

            List<string> functionResults = new List<string>();
            List<string> validatorResults = new List<string>();
            List<string> saveBtnResults = new List<string>();
            List<string> renderLabelResults = new List<string>();
            List<string> formikInitialResults = new List<string>();
            List<string> formikControlResults = new List<string>();
            List<string> bindActionCreatorsResults = new List<string>();

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y")
                {
                    string skeletonEnabled = "true";
                    string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                    string ColumnName = drv["ColumnName"].ToString();
                    string DdlFtrColumnId = drv["DdlFtrColumnId"].ToString();
                    string DdlFtrColumnName = drv["DdlFtrColumnName"].ToString();
                    string DdlAdnColumnName = drv["DdlAdnColumnName"].ToString();
                    string DdlFtrTableId = drv["DdlFtrTableId"].ToString();
                    string DdlFtrDataType = drv["DdlFtrDataType"].ToString();
                    string RefColSrc = DdlFtrTableId == dvItms[0]["TableId"].ToString() ? "Mst" : "Dtl";

                    if (drv["DisplayMode"].ToString() == "TextBox") //---------Textbox
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + "|| '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + " || '',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                                + " <Col lg={6} xl={6}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                      + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                      + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                      + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                      + ")}" + Environment.NewLine
                                      + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<Field" + Environment.NewLine
                                            + "type='text'" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "AutoComplete") //---------Autocomplete
                    {
                        //autocomplete function
                        string functionValue = columnId + "InputChange() { const _this = this; return function (name, v) {const filterBy = " + (string.IsNullOrEmpty(DdlFtrColumnName) ? "'';" : ("((_this.props.[[---ScreenName---]] || {})." + RefColSrc + " || {})." + DdlFtrColumnName + DdlFtrTableId + ";")) + " _this.props.Search" + columnId + "(v, filterBy);}}";
                        functionResults.Add(functionValue);

                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (isEmptyId((values.c" + columnId + " || {}).value)) { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": (values.c" + columnId + "|| {}).value || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + "List = [[---ScreenName---]]ReduxObj.ScreenDdlSelectors." + columnId + "([[---ScreenName---]]State);" + Environment.NewLine
                                                + "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + "List.filter(obj => { return obj.key === " + columnId + " })[0],";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={6} xl={6}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                        + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<AutoCompleteField" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "onChange={this.FieldChange(setFieldValue, setFieldTouched, 'c" + columnId + "', false)}" + Environment.NewLine
                                            + "onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'c" + columnId + "', true)}" + Environment.NewLine
                                            + "onInputChange={this." + columnId + "InputChange()}" + Environment.NewLine
                                            + "value={values.c" + columnId + "}" + Environment.NewLine
                                            + "defaultSelected={" + columnId + "List.filter(obj => { return obj.key === " + columnId + " })}" + Environment.NewLine
                                            + "options={" + columnId + "List}" + Environment.NewLine
                                            + "filterBy={this.AutoCompleteFilterBy}" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? true: false }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                 + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);

                        //mapDispatchToProps: bindActionCreatorsResults
                        string bindActionCreatorsValue = "{ Search" + columnId + ": [[---ScreenName---]]ReduxObj.SearchActions.Search" + columnId + ".bind([[---ScreenName---]]ReduxObj) },";
                        bindActionCreatorsResults.Add(bindActionCreatorsValue);

                    }
                    else if (drv["DisplayMode"].ToString() == "DropDownList" || drv["DisplayMode"].ToString() == "RadioButtonList" || drv["DisplayMode"].ToString() == "ListBox" || drv["DisplayMode"].ToString() == "WorkflowStatus" || drv["DisplayMode"].ToString() == "AutoListBox" || drv["DisplayMode"].ToString() == "DataGridLink") //---------DropdownList / RadioButtonList / ListBox / WorkflowStatus / AutoListBox / DataGridLink
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (isEmptyId((values.c" + columnId + " || {}).value)) { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": (values.c" + columnId + "|| {}).value || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + "List = [[---ScreenName---]]ReduxObj.ScreenDdlSelectors." + columnId + "([[---ScreenName---]]State);" + Environment.NewLine
                                                + "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + "List.filter(obj => { return obj.key === " + columnId + " })[0],";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={6} xl={6}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                        + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<DropdownField" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'c" + columnId + "')}" + Environment.NewLine
                                            + "value={values.c" + columnId + "}" + Environment.NewLine
                                            + "options={" + columnId + "List}" + Environment.NewLine
                                            + "placeholder=''" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                 + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);

                    }
                    else if (drv["DisplayMode"].ToString().Contains("Date")) //---------Date (Any type)
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + "|| '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + " || new Date(),";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={6} xl={6}>" + Environment.NewLine
                                     + "<div className='form__form-group'>" + Environment.NewLine
                                       + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                         + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                         + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                         + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                         + ")}" + Environment.NewLine
                                         + "</label>" + Environment.NewLine
                                       + "}" + Environment.NewLine
                                       + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                       + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<DatePicker" + Environment.NewLine
                                             + "name='c" + columnId + "'" + Environment.NewLine
                                             + "onChange={this.DateChange(setFieldValue, setFieldTouched, 'c" + columnId + "', false)}" + Environment.NewLine
                                             + "onBlur={this.DateChange(setFieldValue, setFieldTouched, 'c" + columnId + "', true)}" + Environment.NewLine
                                             + "value={values.c" + columnId + "}" + Environment.NewLine
                                             + "selected={values.c" + columnId + "}" + Environment.NewLine
                                             + "disabled = {(authCol." + columnId + " || {}).readonly ? true: false }"
                                           + "/>" + Environment.NewLine
                                         + "</div>" + Environment.NewLine
                                       + "}" + Environment.NewLine
                                       + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                     + "</div>" + Environment.NewLine
                                 + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "CheckBox") //---------Checkbox
                    {
                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + " ? 'Y' : 'N',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + " === 'Y',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={12} xl={12}>" + Environment.NewLine
                                           + "<div className='form__form-group'>" + Environment.NewLine
                                                + "<label className='checkbox-btn checkbox-btn--colored-click'>" + Environment.NewLine
                                                  + "<Field" + Environment.NewLine
                                                    + "className='checkbox-btn__checkbox'" + Environment.NewLine
                                                    + "type='checkbox'" + Environment.NewLine
                                                    + "name='c" + columnId + "'" + Environment.NewLine
                                                    + "onChange={handleChange}" + Environment.NewLine
                                                    + "defaultChecked={values.c" + columnId + "}" + Environment.NewLine
                                                    + "disabled={(authCol." + columnId + " || {}).readonly || !(authCol." + columnId + " || {}).visible}" + Environment.NewLine
                                                  + "/>" + Environment.NewLine
                                                  + "<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>" + Environment.NewLine
                                                  + "<span className='checkbox-btn__label'>{(columnLabel." + columnId + " || {}).ColumnHeader}</span>" + Environment.NewLine
                                                + "</label>" + Environment.NewLine
                                                 + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                                  + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                                  + ")}" + Environment.NewLine
                                            + "</div>" + Environment.NewLine
                                         + "</Col>" + Environment.NewLine
                                        + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "ImageButton") //---------ImageButton
                    {
                        //ImageButton function
                        string functionValue = " " + ColumnName + "({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {" + Environment.NewLine
                                        + "return function (evt) {" + Environment.NewLine
                                          + "this.OnClickColumeName = '" + ColumnName + "';" + Environment.NewLine
                                          + "//Enter Custom Code here, eg: submitForm();" + Environment.NewLine
                                          + "evt.preventDefault();" + Environment.NewLine
                                        + "}.bind(this);" + Environment.NewLine
                                      + "}";
                        functionResults.Add(functionValue);

                        //formik control
                        string formikControlValue = "<Col lg={6} xl={6}>" + Environment.NewLine
                                            + "<div className='form__form-group'>" + Environment.NewLine
                                                + "<div className='d-block'>" + Environment.NewLine
                                                    + "{(authCol." + ColumnName + " || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol." + ColumnName + " || {}).readonly || !(authCol." + ColumnName + " || {}).visible} onClick={this." + ColumnName + "({ naviBar, submitForm, currMst })} >{auxLabels." + ColumnName + " || (columnLabel." + ColumnName + " || {}).ColumnName}</Button>}" + Environment.NewLine
                                                + "</div>" + Environment.NewLine
                                            + "</div>" + Environment.NewLine
                                         + "</Col>";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "Label" || drv["DisplayMode"].ToString() == "Action Button" ||
                             drv["DisplayMode"].ToString() == "DataGridLink" || drv["DisplayMode"].ToString() == "PlaceHolder") //---------Label / Action Button / DataGridLink / PlaceHolder
                    {
                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={6} xl={6}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                        + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else  //--------------------------------For the rest of the control, using textbox instead
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + "|| '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currMst." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + " || '',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={6} xl={6}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                        + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<Field" + Environment.NewLine
                                            + "type='text'" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                }

            }
            string functionCnt = string.Join(Environment.NewLine, functionResults);
            string validatorCnt = string.Join(Environment.NewLine, validatorResults);
            string saveBtnCnt = string.Join(Environment.NewLine, saveBtnResults.Select(s => addIndent(s, 10)));
            string renderLabelCnt = string.Join(Environment.NewLine, renderLabelResults);
            string formikInitialCnt = string.Join(Environment.NewLine, formikInitialResults.Select(s => addIndent(s, 18)));
            string formikControlCnt = string.Join(Environment.NewLine, formikControlResults);
            string bindActionCreatorsCnt = string.Join(Environment.NewLine, bindActionCreatorsResults);

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Prompt, Redirect } from 'react-router';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import { Formik, Field, Form } from 'formik';
import DocumentTitle from 'react-document-title';
import classNames from 'classnames';
import LoadingIcon from 'mdi-react/LoadingIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import DatePicker from '../../components/custom/DatePicker';
import NaviBar from '../../components/custom/NaviBar';
import DropdownField from '../../components/custom/DropdownField';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import RintagiScreen from '../../components/custom/Screen';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import { isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getDefaultPath } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import [[---ScreenName---]]ReduxObj, { ShowMstFilterApplied } from '../../redux/[[---ScreenName---]]';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.[[---ScreenName---]] || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = '[[---screenPrimaryKey---]]';
    this.SystemName = 'FintruX';
    this.confirmUnload = this.confirmUnload.bind(this);
    this.hasChangedContent = false;
    this.setDirtyFlag = this.setDirtyFlag.bind(this);
    this.AutoCompleteFilterBy = (option, props) => { return true };
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.ValidatePage = this.ValidatePage.bind(this);
    this.SavePage = this.SavePage.bind(this);
    this.FieldChange = this.FieldChange.bind(this);
    this.DateChange = this.DateChange.bind(this);
    this.DropdownChange = this.DropdownChange.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.SubmitForm = ((submitForm, options = {}) => {
      const _this = this;
      return (evt) => {
        submitForm();
      }
    }
    );
    this.state = {
      submitting: false,
      ScreenButton: null,
      key: '',
      Buttons: {},
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      ModalCancel: null,
      isMobile: false,
    }
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl', (this.props.history || {}).location, true);
    }

    if (!this.props.suppressLoadPage) {
      this.props.setSpinner(true);
    }
  }

  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

");
            sb.Append(functionCnt);
            sb.Append("/* ReactRule: Master Record Custom Function */");
            sb.Append(MasterCustomFunctionCnt);
            sb.Append(@"
/* ReactRule End: Master Record Custom Function */

  /* form related input handling */
//  PostToAp({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
//    return function (evt) {
//      this.OnClickColumeName = 'PostToAp';
//      submitForm();
//      evt.preventDefault();
//    }.bind(this);
//  }

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.[[---ScreenName---]] || {}).ColumnLabel || {};
    /* standard field validation */
");
            sb.Append(validatorCnt);
            sb.Append(@"
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.[[---ScreenName---]] || {}).Mst || {};
");
            sb.Append("/* ReactRule: Master Record Save */");
            sb.Append(MasterSaveCnt);
            sb.Append(@"
/* ReactRule End: Master Record Save */

// No need to generate this, put this in the webrule
//    if ((+(currMst.TrxTotal64)) === 0 && (this.ScreenButton || {}).buttonType === 'SaveClose') {
//      errors.push('Please add at least one expense.');
//    } else if ((this.ScreenButton || {}).buttonType === 'Save' && values.cTrxNote64 !== 'ENTER-PURPOSE-OF-THIS-EXPENSE') {
//      // errors.push('Please do not change the Memo on Chq if Save Only');
//      // setFieldValue('cTrxNote64', 'ENTER-PURPOSE-OF-THIS-EXPENSE');
//    } else if ((this.ScreenButton || {}).buttonType === 'SaveClose' && values.cTrxNote64 === 'ENTER-PURPOSE-OF-THIS-EXPENSE') {
//      errors.push('Please change the Memo on Chq if Save & Pay Me');
//    }
    if (errors.length > 0) {
      this.props.showNotification('E', { message: errors[0] });
      setSubmitting(false);
    }
    else {
      const { ScreenButton, OnClickColumeName } = this;
      this.setState({submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting, key: currMst.key, ScreenButton: ScreenButton, OnClickColumeName: OnClickColumeName });
      this.ScreenButton = null;
      this.OnClickColumeName = null;
      this.props.SavePage(
        this.props.[[---ScreenName---]],
        {
");
            sb.Append(saveBtnCnt);
            sb.Append(@"
        },
        [],
        {
          persist: true,
          ScreenButton: (ScreenButton || {}).buttonType,
          OnClickColumeName: OnClickColumeName,
        }
      );
    }
  }
  /* end of form related handling functions */

  /* standard screen button actions */
  SaveMst({ submitForm, ScreenButton }) {
    return function (evt) {
      this.ScreenButton = ScreenButton;
      submitForm();
    }.bind(this);
  }
  SaveCloseMst({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      this.ScreenButton = ScreenButton;
      submitForm();
    }.bind(this);
  }
  NewSaveMst({ submitForm, ScreenButton }) {
    return function (evt) {
      this.ScreenButton = ScreenButton;
      submitForm();
    }.bind(this);
  }
  CopyHdr({ ScreenButton, mst, mstId, useMobileView }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).[[---screenPrimaryKey---]];
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.[[---ScreenName---]].Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.[[---ScreenName---]].Label);
            this.props.history.push(getEditMstPath(getNaviPath(naviBar, 'Mst', '/'), '_'));
          }
          else {
            if (this.props.onCopy) this.props.onCopy();
          }
        }
        else {
          this.setState({ ModalOpen: true, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
        }
      }
      if (!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }
  DelMst({ naviBar, ScreenButton, mst, mstId }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.[[---screenPrimaryKey---]];
        this.props.DelMst(this.props.[[---ScreenName---]], fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.[[---ScreenName---]] || {};
    const buttons = nextReduxScreenState.Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'Mst', prevState);
    const currentKey = nextReduxScreenState.key;
    const waiting = nextReduxScreenState.page_saving || nextReduxScreenState.page_loading;
    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;

    if (prevState.submitting && !waiting && nextReduxScreenState.submittedOn > prevState.submittedOn) {
      prevState.setSubmitting(false);
    }

    return revisedState;
  }

  confirmUnload(message, callback) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const confirm = () => {
      callback(true);
    }
    const cancel = () => {
      callback(false);
    }
    this.setState({ ModalOpen: true, ModalSuccess: confirm, ModalCancel: cancel, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: message });
  }

  setDirtyFlag(dirty) {
    /* this is called during rendering but has side-effect, undesirable but only way to pass formik dirty flag around */
    if (dirty) {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = this.confirmUnload;
      registerBlocker(this.confirmUnload);
    }
    else {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = null;
    }
    if (this.props.updateChangedState) this.props.updateChangedState(dirty);
    this.SetCurrentRecordState(dirty);
    return true;
  }

  componentDidMount() {
    this.mediaqueryresponse(this.mobileView);
    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes
    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const suppressLoadPage = this.props.suppressLoadPage;
    if (!suppressLoadPage) {
      const { mstId } = { ...this.props.match.params };
      if (!(this.props.[[---ScreenName---]] || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.[[---ScreenName---]] || {};

    if (!this.props.suppressLoadPage) {
      if (!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
        const _this = this;
        setTimeout(() => _this.props.setSpinner(false), 500);
      }
    }

    const currMst = currReduxScreenState.Mst || {};
    this.SetPageTitle(currReduxScreenState);
    if (prevstates.key !== currMst.key) {
      if ((prevstates.ScreenButton || {}).buttonType === 'SaveClose') {
        const currDtl = currReduxScreenState.EditDtl || {};
        const dtlList = (currReduxScreenState.DtlList || {}).data || [];
        const naviBar = getNaviBar('Mst', currMst, currDtl, currReduxScreenState.Label);
        const searchListPath = getDefaultPath(getNaviPath(naviBar, 'MstList', '/'))
        this.props.history.push(searchListPath);
      }
    }
  }

  componentWillUnmount() {
    if (this.blocker) {
      unregisterBlocker(this.blocker);
      this.blocker = null;
    }
    this.mobileView.removeListener(this.mediaqueryresponse);
  }


  render() {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};

    if ([[---ScreenName---]]State.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = [[---ScreenName---]]State.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = [[---ScreenName---]]ReduxObj.GetScreenButtons([[---ScreenName---]]State) || {};
    const itemList = [[---ScreenName---]]State.Dtl || [];
    const auxLabels = [[---ScreenName---]]State.Label || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};

    const columnLabel = [[---ScreenName---]]State.ColumnLabel || {};
    const authCol = this.GetAuthCol([[---ScreenName---]]State);
    const authRow = ([[---ScreenName---]]State.AuthRow || [])[0] || {};
    const currMst = ((this.props.[[---ScreenName---]] || {}).Mst || {});
    const currDtl = ((this.props.[[---ScreenName---]] || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.[[---screenPrimaryKey---]]));
    const selectList = [[---ScreenName---]]ReduxObj.SearchListToSelectList([[---ScreenName---]]State);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
");

            sb.Append(renderLabelCnt);
            sb.Append(@"

    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
");
            sb.Append("/* ReactRule: Master Render */");
            sb.Append(MasterRenderCnt);
            sb.Append(@"
/* ReactRule End: Master Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner([[---ScreenName---]]State) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
");
            sb.Append(formikInitialCnt);
            sb.Append(@"
                  }}
                  validate={this.ValidatePage}
                  onSubmit={this.SavePage}
                  key={currMst.key}
                  render={({
                    values,
                    errors,
                    touched,
                    isSubmitting,
                    dirty,
                    setFieldValue,
                    setFieldTouched,
                    handleChange,
                    submitForm
                  }) => (
                      <div>
                        {this.setDirtyFlag(dirty) &&
                          <Prompt
                            when={dirty}
                            message={auxSystemLabels.UnsavedPageMsg || ''}
                          />
                        }
                        <div className='account__head'>
                          <Row>
                            <Col xs={useMobileView ? 9 : 8}>
                              <h3 className='account__title'>{MasterRecTitle}</h3>
                              <h4 className='account__subhead subhead'>{MasterRecSubtitle}</h4>
                            </Col>
                            <Col xs={useMobileView ? 3 : 4}>
                              <ButtonToolbar className='f-right'>
                                {(this.constructor.ShowSpinner([[---ScreenName---]]State) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---screenPrimaryKey---]])).length > 0 &&
                                        <DropdownToggle className='mw-50' outline>
                                          <i className='fa fa-ellipsis-h icon-holder'></i>
                                          {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                        </DropdownToggle>
                                      }
                                    </ButtonGroup>
                                    {
                                      dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                                      <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                        {
                                          dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---screenPrimaryKey---]])) return null;
                                            return (
                                              <DropdownItem key={v.tid || v.order} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, submitForm, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                          })
                                        }
                                      </DropdownMenu>
                                    }
                                  </UncontrolledDropdown>
                                }
                              </ButtonToolbar>
                            </Col>
                          </Row>
                        </div>
                        <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}

                          <div className='w-100'>
                            <Row>
            ");
            sb.Append(formikControlCnt);
            sb.Append(@"
                            </Row>
                          </div>
                          <div className='form__form-group mart-5 mb-0'>
                            <Row className='btn-bottom-row'>
                              {useMobileView && <Col xs={3} sm={2} className='btn-bottom-column'>
                                <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className='fa fa-long-arrow-left'></i></Button>
                              </Col>}
                              <Col
                                xs={useMobileView ? 9 : 12}
                                sm={useMobileView ? 10 : 12}>
                                <Row>
                                  {
                                    bottomButtonList
                                      .filter(v => v.expose)
                                      .map((v, i, a) => {
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---screenPrimaryKey---]])) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).[[---screenPrimaryKey---]]));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner([[---ScreenName---]]State) && <Skeleton height='43px' />) ||
                                              <Button color='success' type='button' outline={outlineProperty} className='account__btn' disabled={isSubmitting} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, submitForm, ScreenButton: v, mst: currMst, useMobileView })}>{v.label}</Button>
                                            }
                                          </Col>
                                        )
                                      })
                                  }
                                </Row>
                              </Col>
                            </Row>
                          </div>
                        </Form>
                      </div>
                    )}
                />
              </div>
            </div>
          </div>
        </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  error: state.error,
  [[---ScreenName---]]: state.[[---ScreenName---]],
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: [[---ScreenName---]]ReduxObj.LoadPage.bind([[---ScreenName---]]ReduxObj) },
    { SavePage: [[---ScreenName---]]ReduxObj.SavePage.bind([[---ScreenName---]]ReduxObj) },
    { DelMst: [[---ScreenName---]]ReduxObj.DelMst.bind([[---ScreenName---]]ReduxObj) },
    { AddMst: [[---ScreenName---]]ReduxObj.AddMst.bind([[---ScreenName---]]ReduxObj) },
//    { SearchMemberId64: [[---ScreenName---]]ReduxObj.SearchActions.SearchMemberId64.bind([[---ScreenName---]]ReduxObj) },
//    { SearchCurrencyId64: [[---ScreenName---]]ReduxObj.SearchActions.SearchCurrencyId64.bind([[---ScreenName---]]ReduxObj) },
//    { SearchCustomerJobId64: [[---ScreenName---]]ReduxObj.SearchActions.SearchCustomerJobId64.bind([[---ScreenName---]]ReduxObj) },
");
            sb.Append(bindActionCreatorsCnt);
            sb.Append(@"
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            ").Replace("[[---ScreenName---]]", screenName).Replace("[[---screenPrimaryKey---]]", screenPrimaryKey).Replace("[[---ScreenDetailKey---]]", screenDetailKey);

            return sb;
        }

        private StringBuilder MakeReactJsDtlList(string screenId, string screenName)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenPrimaryKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }
            string screenDetailKey = "";

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import { bindActionCreators, compose } from 'redux';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import { Formik, Field, Form } from 'formik';
import DocumentTitle from 'react-document-title';
import MagnifyIcon from 'mdi-react/MagnifyIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import PlusIcon from 'mdi-react/PlusIcon';
import RadioboxBlankIcon from 'mdi-react/RadioboxBlankIcon';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import NaviBar from '../../components/custom/NaviBar';
import DropdownField from '../../components/custom/DropdownField';
import RintagiScreen from '../../components/custom/Screen'
import ModalDialog from '../../components/custom/ModalDialog';
import classNames from 'classnames';
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { getSelectedFromList, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getListDisplayContet } from '../../helpers/utils'
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import DtlRecord from './DtlRecord';
import [[---ScreenName---]]ReduxObj from '../../redux/[[---ScreenName---]]';

class DtlList extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.[[---ScreenName---]] || {});
    this.titleSet = false;
    this.hasChangedContent = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = '[[---ScreenPrimaryKey---]]';
    this.DtlKeyColumnName = '[[---screenDetailKey---]]';
    this.SetCurrentRecordState = this.SetCurrentRecordState.bind(this);
    this.SearchBoxFocus = this.SearchBoxFocus.bind(this);
    this.SelectDtlListRow = this.SelectDtlListRow.bind(this);
    this.FilterDtlList = this.FilterDtlList.bind(this);
    this.FilteredColumnChange = this.FilteredColumnChange.bind(this);
    this.ResetFilterList = this.ResetFilterList.bind(this);
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.OnDtlCopy = () => { this.setState({ ShowDtl: true }) };
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      searchFocus: false,
      /* this is needed due to the oddball use of remembering focus which force formik render thus loss the selected value(before sumbit) */
      DtlFilter: {},
      Buttons: {},
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      ShowDtl: false,
      isMobile: false,
    };
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }


  /* standard screen button actions */
  Print({ naviBar, mst, mstId }) {
    return function (evt) { }.bind(this);
  }
  CopyRow({ mst, dtlId, dtl, ...rest }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || dtl.[[---screenDetailKey---]];
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.[[---ScreenPrimaryKey---]], currDtlId);
          const isMobileView = window.matchMedia('(max-width: 1099px)').matches;
          const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
          if (useMobileView) {
            const naviBar = getNaviBar('DtlList', mst, {}, this.props.[[---ScreenName---]].Label);
            const path = getEditDtlPath(getNaviPath(naviBar, 'Dtl', '/'), '_');
            this.props.history.push(path);
          }
          else {
            this.setState({
              ShowDtl: true,
            });
          }
        }
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);

  }

  AddNewDtl({ naviBar, useMobileView, mstId }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    if (useMobileView) return super.AddNewDtl({ naviBar });

    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddDtl(mstId, null);
        this.setState({
          ShowDtl: true,
        });
      }
      if (!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  DelDtl({ mst, dtlId, dtl }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.[[---screenDetailKey---]];
        if (currDtlId) {
          this.props.SavePage(
            this.props.[[---ScreenName---]],
            this.props.[[---ScreenName---]].Mst,
            [
              {
                [[---screenDetailKey---]]: currDtlId,
                _mode: 'delete',
              }
            ],
            {
              persist: true,
              // resizeImage: 'TrxDetImg65',

            }
          )
        }
        else {
          /* delete local temp */
        }
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });

    }.bind(this);
  }
  ExpMstTxt() {
    return function (evt) { }.bind(this);
  }
  /* end of screen button action */


  /* react related calls */
  static getDerivedStateFromProps(nextProps, prevState) {
    const buttons = (nextProps.[[---ScreenName---]] || {}).Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'DtlList', prevState);
    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;
    return revisedState;
  }

  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

  componentDidMount() {
    const { mstId, dtlId } = { ...this.props.match.params };

    if (!(this.props.[[---ScreenName---]] || {}).AuthCol || true)
      this.props.LoadPage('DtlList', { mstId:mstId || '_', dtlId:dtlId || '_' });

    this.mediaqueryresponse(this.mobileView);
    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes
  }

  componentDidUpdate(prevProps, prevStates) {
    const currReduxScreenState = this.props.[[---ScreenName---]] || {};

    if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
      const _this = this;
      setTimeout(() => _this.props.setSpinner(false), 500);
    }

    this.SetPageTitle(currReduxScreenState);
  }

  componentWillUnmount() {
    this.mobileView.removeListener(this.mediaqueryresponse);
  }

  render() {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const { targetMstId, targetDtlId } = this.GetTargetId();
    if ([[---ScreenName---]]State.access_denied) {
      return <Redirect to='/error' />;
    }
    //if (![[---ScreenName---]]State.initialized || [[---ScreenName---]]State.page_loading) return null;
    const screenHlp = [[---ScreenName---]]State.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailLstTitle = ((screenHlp || {}).DetailLstTitle || '');
    const DetailLstSubtitle = ((screenHlp || {}).DetailLstSubtitle || '');
    const IncrementMsg = ((screenHlp || {}).IncrementMsg || '');
    const NoDetailMsg = ((screenHlp || {}).NoDetailMsg || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');
    const AddDetailMsg = ((screenHlp || {}).AddDetailMsg || '');
    const DetailFoundMsg = ((screenHlp || {}).DetailFoundMsg || '');

    const screenButtons = [[---ScreenName---]]ReduxObj.GetScreenButtons([[---ScreenName---]]State) || {};
    const itemList = [[---ScreenName---]]ReduxObj.DtlListToSelectList([[---ScreenName---]]State, '[[---screenDetailKey---]]');
    const selectList = [[---ScreenName---]]ReduxObj.SearchListToSelectList([[---ScreenName---]]State);
    const selectedMst = (selectList || []).filter(v=>v.isSelected)[0] || {};
    const auxLabels = [[---ScreenName---]]State.Label || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const columnLabel = [[---ScreenName---]]State.ColumnLabel;
    const columnDefinition = Object.keys(columnLabel).reduce((a,o,i)=>{
    const x = columnLabel[o];
       if (x['DtlLstPosId'] == '1'){ a['dTopL'] = x;};
       if (x['DtlLstPosId'] == '2'){ a['dBottomL'] = x;};
       if (x['DtlLstPosId'] == '3'){ a['dTopR'] = x;};
       if (x['DtlLstPosId'] == '4'){ a['dBottomR'] = x;};  
       return a;
    },{});     
    const dTopL =  columnDefinition['dTopL'] || {};
    const dBottomL =  columnDefinition['dBottomL'] || {};
    const dTopR =  columnDefinition['dTopR'] || {};
    const dBottomR =  columnDefinition['dBottomR'] || {};
    const currMst = [[---ScreenName---]]State.Mst;
    const currDtl = [[---ScreenName---]]State.EditDtl;
    const dtlFilter = [[---ScreenName---]]State.DtlFilter;
    const filterList = [[---ScreenName---]]State.DtlFilter.FilterColumnDdl.map((v, i) => ({ key: v.ColName, label: v.ColumnHeader, value: v.ColName, pos: i }))
    const naviBar = getNaviBar('DtlList', currMst, currDtl, screenButtons);
    const authRow = ([[---ScreenName---]]State.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, rowMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasRowButton || hasDropdownMenuButton;
    const activeSelectionVisible = itemList.filter(v => v.isSelected).length > 0;

    let colorDark = classNames({
      // 'color-dark': haveAllButtons
    });

    // Filter visibility
    let filterVisibility = classNames({ 'd-none': true, 'd-block': dtlFilter.ShowFilter });
    let filterBtnStyle = classNames({ 'filter-button-clicked': dtlFilter.ShowFilter });
    let filterActive = classNames({ 'filter-icon-active': dtlFilter.ShowFilter });
    let viewMoreClass = classNames({ 'd-none': dtlFilter.Limit >= itemList.length });

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

    return (
      <DocumentTitle title={siteTitle}>
        <div className='top-level-split'>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <Row className='no-margin-right'>
            <Col className='no-padding-right' xs='6'>
              <div className='account'>
                <div className='account__wrapper account-col'>
                  <div className='account__card rad-4'>
                    {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                      <div className='tabs__wrap'>
                        <NaviBar history={this.props.history} navi={naviBar} />
                      </div>
                    </div>}
                    <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                    <Formik
                      key={currDtl.key}
                      initialValues={{
                        CurrencyId64: '',
                        FilterBy: filterList.filter(obj => { return obj.key === ((this.state.DtlFilter || {}).FilteredColumn || dtlFilter.FilteredColumn) })[0],
                        FilteredValue: dtlFilter.FilteredValue || '',
                      }}
                      onSubmit={this.FilterDtlList}
                      onReset={this.ResetFilterList}
                      render={({
                        values,
                        isSubmitting,
                        handleSubmit,
                        handleChange,
                        handleReset,
                        handleBlur,
                        handleClick,
                        setFieldValue,
                        resetForm
                      }) => (
                          <div>
                            <div className='account__head'>
                              <Row>
                                <Col xs={useMobileView ? 9 : 8}>
                                  <h3 className='account__title'>{DetailLstTitle}</h3>
                                  <h4 className='account__subhead subhead'>{DetailLstSubtitle}</h4>
                                </Col>
                                <Col xs={useMobileView ? 3 : 4}>
                                  <ButtonToolbar className='f-right'>
                                    <UncontrolledDropdown>
                                      <ButtonGroup className='btn-group--icons'>
                                        <Button className={`mw-50 ${filterBtnStyle}`} onClick={this.props.ChangeDtlListFilterVisibility} outline>
                                          <i className={`fa fa-filter icon-holder ${filterActive}`}></i> {dtlFilter.FilteredValue ? <i className='filter-applied'></i> : ''}
                                          {!useMobileView && <p className='action-menu-label'>{(screenButtons.Filter || {}).label}</p>}
                                        </Button>
                                        {
                                          dropdownMenuButtonList.filter(v => v.expose).map(v => {
                                            if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]])) return null;
                                            return (
                                              <Button
                                                key={v.tid}
                                                onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })}
                                                className='mw-50 add-report-expense'
                                                outline>
                                                <i className={`${v.iconClassName} icon-holder`}></i>
                                                {!useMobileView && <p className='action-menu-label'>{v.label}</p>}
                                              </Button>)
                                          })
                                        }
                                        {activeSelectionVisible &&
                                          dropdownMenuButtonList
                                            .filter(v => !v.expose)
                                            .filter(v => (!this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],(currDtl || {}).[[---screenDetailKey---]])))
                                            .length > 0 &&
                                          <DropdownToggle className='mw-50' outline>
                                            <i className='fa fa-ellipsis-h icon-holder'></i>
                                            {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                          </DropdownToggle>
                                        }
                                      </ButtonGroup>
                                      {activeSelectionVisible &&
                                        dropdownMenuButtonList
                                          .filter(v => !v.expose)
                                          .filter(v => (!this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],(currDtl || {}).[[---screenDetailKey---]])))
                                          .length > 0 &&
                                        <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                          {

                                            dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                              if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],(currDtl || {}).[[---screenDetailKey---]])) return null;
                                              return (
                                                <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                            })
                                          }
                                        </DropdownMenu>
                                      }
                                    </UncontrolledDropdown>
                                  </ButtonToolbar>
                                </Col>
                              </Row>
                            </div>
                            <Form className='form'>
                              <div className='form__form-group'>
                                <div className='form__form-group-narrow'>
                                  <div className='form__form-group-field'>
                                    <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                      <span className='radio-btn__label color-blue fw-700 f-16'>{selectedMst.label || NoMasterMsg}</span>
                                      <span className='radio-btn__label__right color-blue fw-700 f-16'><span className='mr-5'>{(columnLabel.TrxTotal64 || {}).ColumnHeader}</span>
                                      {/* :{!isNaN(selectedMst.labelR) ? toLocalAmountFormat(selectedMst.labelR) : toLocalAmountFormat('0.00')} {selectedMst.detailR} */}
                                      </span>
                                    </span>
                                  </div>
                                  <div className={`form__form-group filter-padding items-filter-margin ${filterVisibility}`}>
                                    <Row className='mb-5'>
                                      <Col xs={12}>
                                        <label className='form__form-group-label filter-label'>{auxSystemLabels.QFilter}</label>
                                        <div className='form__form-group-field filter-form-border'>
                                          <DropdownField
                                            name='FilterBy'
                                            onBlur={handleBlur}
                                            onChange={this.FilteredColumnChange(handleSubmit, setFieldValue, 'FilterBy')}
                                            value={values.FilterBy}
                                            options={filterList}
                                            placeholder=''
                                          />
                                        </div>
                                      </Col>
                                    </Row>
                                    <Row>
                                      <Col xs={12}>
                                        <label className='form__form-group-label filter-label'>{auxSystemLabels.FilterSearchLabel}</label>
                                        <div className='form__form-group-field filter-form-border'>
                                          <Field
                                            type='text'
                                            name='FilteredValue'
                                            value={values.FilteredValue}
                                            // onChange={this.FilteredValueChange(handleReset, setFieldValue, 'FilteredValue')}
                                            onFocus={(e) => this.SearchBoxFocus(e)}
                                            onBlur={(e) => this.SearchBoxFocus(e)}
                                            className='FilteredValueClass'
                                          />
                                          <div className='rbt-aux custom-filter-clear' onClick={handleReset}>
                                            <button className='close rbt-close' type='button'>
                                              <span aria-hidden='true'>×</span>
                                            </button>
                                          </div>
                                          <span onClick={handleSubmit} className={`form__form-group-button desktop-filter-button search-btn-fix ${this.state.searchFocus ? ' active' : ''}`}><MagnifyIcon /></span>
                                        </div>
                                      </Col>
                                    </Row>
                                  </div>
                                  <h5 className='fill-fintrux h5-expense-items fw-700'><span className='color-dark mr-5'>{DetailFoundMsg}:</span> {itemList.length}</h5>
                                </div>
                                {
                                  itemList.slice(0, dtlFilter.Limit).map(
                                    (obj, i) => {
                                      const naviSelectBar = getNaviBar('DtlList', currMst, {}, auxLabels);
                                      return (
                                        <div className='form__form-group-narrow list-divider' key={obj.[[---screenDetailKey---]] || ('_' + i)}>
                                          <div className='form__form-group-field'>
                                            <label className='radio-btn radio-btn--button margin-narrow'>
                                              <Field
                                                key={obj.[[---screenDetailKey---]] || ('_' + i)}
                                                className='radio-btn__radio'
                                                name='expenseItem'
                                                type='radio'
                                                value={values.name}
                                                onClick={this.SelectDtlListRow(naviSelectBar, currMst, obj, i, '[[---ScreenPrimaryKey---]]', '[[---screenDetailKey---]]')}
                                                defaultChecked={obj.isSelected ? true : false}
                                              />
                                              <span className='radio-bg-solver'></span>
                                              {hasActableButtons &&
                                                <span className='radio-btn__label-svg'>
                                                  <CheckIcon className='radio-btn__label-check' />
                                                  <RadioboxBlankIcon className='radio-btn__label-uncheck' />
                                                </span>
                                              }
                                              <span className={`radio-btn__label ${colorDark}`}>
                                                <div className='row-cut'>{getListDisplayContet(obj, dTopL)}</div>
                                                <div className='row-cut row-bottom-report'>{getListDisplayContet(obj, dBottomL)}</div>
                                              </span>
                                              <span className={`radio-btn__label__right ${colorDark}`}>
                                                <div className='row-cut'>{getListDisplayContet(obj, dTopR)}</div>
                                                <div className='row-bottom-report f-right'>{getListDisplayContet(obj, dBottomR)}</div>
                                              </span>
                                              {
                                                rowMenuButtonList
                                                  .filter(v => v.expose && !useMobileView)
                                                  .map((v, i) => {
                                                    return (
                                                      <button type='button' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, mst: currMst, useMobileView, dtlId: obj.[[---screenDetailKey---]] })} className={`${v.exposedClassName}`}><i className={`${v.iconClassName}`}></i></button>
                                                    )
                                                  })
                                              }
                                              {
                                                rowMenuButtonList.filter(v => !v.expose || useMobileView).length > 0 &&
                                                <UncontrolledDropdown className={`btn-row-dropdown`}>
                                                  <DropdownToggle className='btn-row-dropdown-icon btn-row-menu' onClick={(e) => { e.preventDefault() }}>
                                                    <i className='fa fa-ellipsis-h icon-holder menu-icon'></i>
                                                  </DropdownToggle>
                                                  <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                                    {rowMenuButtonList
                                                      .filter(v => !v.expose || useMobileView)
                                                      .map((v) => {
                                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],obj.[[---screenDetailKey---]])) return null;
                                                        return <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, mst: currMst, useMobileView, dtlId: obj.[[---screenDetailKey---]] })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>
                                                      })
                                                    }
                                                  </DropdownMenu>
                                                </UncontrolledDropdown>
                                              }
                                            </label>
                                          </div>
                                        </div>
                                      )
                                    }
                                  )
                                }
                              </div>
                              <Button onClick={this.props.ViewMoreDtl} className={`${viewMoreClass} btn btn-view-more-blue account__btn`} type='button'>{strFormat(IncrementMsg, this.props.[[---ScreenName---]].DtlFilter.PageSize)}<br /><i className='fa fa-arrow-down'></i></Button>
                              <div className='width-wrapper'>
                                <div className='buttons-bottom-container'>
                                  <Row className='btn-bottom-row'>
                                    <Col xs={3} sm={2} className='btn-bottom-column'>
                                      <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className='fa fa-long-arrow-left'></i></Button>
                                    </Col>
                                    {useMobileView && <Col xs={9} sm={10}>
                                      <Row>
                                        {
                                          bottomButtonList
                                            .filter(v => v.expose)
                                            .map((v, i, a) => {
                                              if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]])) return null;
                                              const buttonCount = a.length;
                                              const colWidth = parseInt(12 / buttonCount, 10);
                                              const lastBtn = i === a.length - 1;
                                              const outlineProperty = lastBtn ? false : true;

                                              return (
                                                <Col xs={colWidth} sm={colWidth} key={v.tid} className='btn-bottom-column' >
                                                  <Button color='success' type='button' outline={outlineProperty} className='account__btn' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })}>{v.labelLong}</Button>
                                                </Col>
                                              )
                                            })
                                        }
                                      </Row>
                                    </Col>}
                                  </Row>
                                </div>
                              </div>
                            </Form>
                          </div>
                        )}
                    />
                  </div>
                </div>
              </div>
            </Col>
            {!useMobileView && <Col xs={6}>

              {!activeSelectionVisible &&
                targetDtlId !== '_' &&
                !this.state.ShowDtl &&
                <div className='empty-block'>
                  <img className='folder-img' alt='' src={require('../../img/folder.png')} />
                  <p className='create-new-message'>{NoDetailMsg}. <span className='link-imitation' onClick={this.AddNewDtl({ naviBar, mstId: currMst.[[---ScreenPrimaryKey---]] })}>{AddDetailMsg}</span></p>
                </div>}

              {(activeSelectionVisible || this.state.ShowDtl || (targetDtlId ==='_')) && <DtlRecord OnCopy={this.OnDtlCopy} updateChangedState={this.SetCurrentRecordState} suppressLoadPage={true} />}

            </Col>}
          </Row>
        </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: state.user,
  error: state.error,
  [[---ScreenName---]]: state.[[---ScreenName---]],
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: [[---ScreenName---]]ReduxObj.LoadPage.bind([[---ScreenName---]]ReduxObj) },
    { SelectDtl: [[---ScreenName---]]ReduxObj.SelectDtl.bind([[---ScreenName---]]ReduxObj) },
    { SavePage: [[---ScreenName---]]ReduxObj.SavePage.bind([[---ScreenName---]]ReduxObj) },
    { AddDtl: [[---ScreenName---]]ReduxObj.AddDtl.bind([[---ScreenName---]]ReduxObj) },
    { ChangeDtlListFilterVisibility: [[---ScreenName---]]ReduxObj.ChangeDtlListFilterVisibility.bind([[---ScreenName---]]ReduxObj) },
    { ChangeDtlListFilter: [[---ScreenName---]]ReduxObj.ChangeDtlListFilter.bind([[---ScreenName---]]ReduxObj) },
    { ViewMoreDtl: [[---ScreenName---]]ReduxObj.ViewMoreDtl.bind([[---ScreenName---]]ReduxObj) },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlList);

            ").Replace("[[---ScreenName---]]", screenName)
              .Replace("[[---ScreenPrimaryKey---]]", screenPrimaryKey)
              .Replace("[[---screenDetailKey---]]", screenDetailKey);

            return sb;
        }

        private StringBuilder MakeReactJsDtlRecord(string screenId, string screenName, DataView dvReactRule)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenPrimaryKey = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }
            string screenDetailKey = "";

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    break;
                }
            }

            List<string> functionResults = new List<string>();
            List<string> validatorResults = new List<string>();
            List<string> saveBtnResults = new List<string>();
            List<string> renderLabelResults = new List<string>();
            List<string> formikInitialResults = new List<string>();
            List<string> formikControlResults = new List<string>();
            List<string> bindActionCreatorsResults = new List<string>();

            List<string> DetailCustomFunctionResults = new List<string>();
            List<string> DetailSaveResults = new List<string>();
            List<string> DetailRenderResults = new List<string>();
            List<string> DetailScreenColumnResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            foreach (DataRowView drv in dvReactRule)
            {

                if (drv["ReactEventId"].ToString() == "5") //Detail Record Custom Function
                {
                    string DetailCustomFunctionValue = drv["ReactRuleProg"].ToString();
                    DetailCustomFunctionResults.Add(DetailCustomFunctionValue);
                }
                else if (drv["ReactEventId"].ToString() == "6") //Detail Record Save
                {
                    string DetailSaveValue = drv["ReactRuleProg"].ToString();
                    DetailSaveResults.Add(DetailSaveValue);
                }
                else if (drv["ReactEventId"].ToString() == "7") //Detail Record Render
                {
                    string DetailRenderValue = drv["ReactRuleProg"].ToString();
                    DetailRenderResults.Add(DetailRenderValue);
                }
                else if (drv["ReactEventId"].ToString() == "8") //Detail Record Screen Column
                {
                    string DetailScreenColumnValue = drv["ReactRuleProg"].ToString();
                    DetailScreenColumnResults.Add(DetailScreenColumnValue);
                }
            }

            string DetailCustomFunctionCnt = string.Join(Environment.NewLine, DetailCustomFunctionResults.Select(s => addIndent(s, 24)));
            string DetailSaveCnt = string.Join(Environment.NewLine, DetailSaveResults.Select(s => addIndent(s, 24)));
            string DetailRenderCnt = string.Join(Environment.NewLine, DetailRenderResults.Select(s => addIndent(s, 24)));
            string DetailScreenColumnCnt = string.Join(Environment.NewLine, DetailScreenColumnResults.Select(s => addIndent(s, 24)));

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N")
                {
                    string skeletonEnabled = "true";
                    string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                    string ColumnName = drv["ColumnName"].ToString();
                    string DdlFtrColumnId = drv["DdlFtrColumnId"].ToString();
                    string DdlFtrColumnName = drv["DdlFtrColumnName"].ToString();
                    string DdlAdnColumnName = drv["DdlAdnColumnName"].ToString();
                    string DdlFtrTableId = drv["DdlFtrTableId"].ToString();
                    string DdlFtrDataType = drv["DdlFtrDataType"].ToString();
                    string RefColSrc = DdlFtrTableId == dvItms[0]["TableId"].ToString() ? "Mst" : "Dtl";

                    if (columnId == screenDetailKey) continue; // skip key column as it is produced else where

                    if (drv["DisplayMode"].ToString() == "TextBox") //---------Textbox
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + "|| '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": currDtl." + columnId + " || '',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                                + " <Col lg={12} xl={12}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                         + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<Field" + Environment.NewLine
                                            + "type='text'" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                 + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "AutoComplete") //---------Autocomplete
                    {
                        //autocomplete function
                        string functionValue = columnId + "InputChange() { const _this = this; return function (name, v) {const filterBy = " + (string.IsNullOrEmpty(DdlFtrColumnName) ? "'';" : ("((_this.props.[[---ScreenName---]] || {})." + RefColSrc + " || {})." + DdlFtrColumnName + DdlFtrTableId + ";")) + " _this.props.Search" + columnId + "(v, filterBy);}}";
                        functionResults.Add(functionValue);

                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (isEmptyId((values.c" + columnId + " || {}).value)) { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": (values.c" + columnId + "|| {}).value || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + "List = [[---ScreenName---]]ReduxObj.ScreenDdlSelectors." + columnId + "([[---ScreenName---]]State);" + Environment.NewLine
                                                + "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + "List.filter(obj => { return obj.key === currDtl." + columnId + " })[0],";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={12} xl={12}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                         + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<AutoCompleteField" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "onChange={this.FieldChange(setFieldValue, setFieldTouched, 'c" + columnId + "', false)}" + Environment.NewLine
                                            + "onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'c" + columnId + "', true)}" + Environment.NewLine
                                            + "onInputChange={this." + columnId + "InputChange()}" + Environment.NewLine
                                            + "value={values.c" + columnId + "}" + Environment.NewLine
                                            + "defaultSelected={" + columnId + "List.filter(obj => { return obj.key === " + columnId + " })}" + Environment.NewLine
                                            + "options={" + columnId + "List}" + Environment.NewLine
                                            + "filterBy={this.AutoCompleteFilterBy}" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? true: false }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                  + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);

                        //mapDispatchToProps: bindActionCreatorsResults
                        string bindActionCreatorsValue = "{ Search" + columnId + ": [[---ScreenName---]]ReduxObj.SearchActions.Search" + columnId + ".bind([[---ScreenName---]]ReduxObj) },";
                        bindActionCreatorsResults.Add(bindActionCreatorsValue);

                    }
                    else if (drv["DisplayMode"].ToString() == "DropDownList" || drv["DisplayMode"].ToString() == "RadioButtonList" || drv["DisplayMode"].ToString() == "ListBox" || drv["DisplayMode"].ToString() == "WorkflowStatus" || drv["DisplayMode"].ToString() == "AutoListBox" || drv["DisplayMode"].ToString() == "DataGridLink") //---------DropdownList / RadioButtonList / ListBox / WorkflowStatus / AutoListBox / DataGridLink
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (isEmptyId((values.c" + columnId + " || {}).value)) { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": (values.c" + columnId + "|| {}).value || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + "List = [[---ScreenName---]]ReduxObj.ScreenDdlSelectors." + columnId + "([[---ScreenName---]]State);" + Environment.NewLine
                                                + "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": " + columnId + "List.filter(obj => { return obj.key === currDtl." + columnId + " })[0],";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                             + " <Col lg={12} xl={12}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                         + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<DropdownField" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'c" + columnId + "')}" + Environment.NewLine
                                            + "value={values.c" + columnId + "}" + Environment.NewLine
                                            + "options={" + columnId + "List}" + Environment.NewLine
                                            + "placeholder=''" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                  + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);

                    }
                    else if (drv["DisplayMode"].ToString().Contains("Date")) //---------Date (Any type)
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + " || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": currDtl." + columnId + " || new Date(),";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                             + " <Col lg={12} xl={12}>" + Environment.NewLine
                                     + "<div className='form__form-group'>" + Environment.NewLine
                                       + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                         + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                          + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                         + "</label>" + Environment.NewLine
                                       + "}" + Environment.NewLine
                                       + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                       + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<DatePicker" + Environment.NewLine
                                             + "name='c" + columnId + "'" + Environment.NewLine
                                             + "onChange={this.DateChange(setFieldValue, setFieldTouched, 'c" + columnId + "', false)}" + Environment.NewLine
                                             + "onBlur={this.DateChange(setFieldValue, setFieldTouched, 'c" + columnId + "', true)}" + Environment.NewLine
                                             + "value={values.c" + columnId + "}" + Environment.NewLine
                                             + "selected={values.c" + columnId + "}" + Environment.NewLine
                                             + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                           + "/>" + Environment.NewLine
                                         + "</div>" + Environment.NewLine
                                       + "}" + Environment.NewLine
                                       + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                     + "</div>" + Environment.NewLine
                                  + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "CheckBox") //---------Checkbox
                    {
                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + " ? 'Y' : 'N',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": currDtl." + columnId + " === 'Y',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                             + " <Col lg={12} xl={12}>" + Environment.NewLine
                                           + "<div className='form__form-group'>" + Environment.NewLine
                                                + "<label className='checkbox-btn checkbox-btn--colored-click'>" + Environment.NewLine
                                                  + "<Field" + Environment.NewLine
                                                    + "className='checkbox-btn__checkbox'" + Environment.NewLine
                                                    + "type='checkbox'" + Environment.NewLine
                                                    + "name='c" + columnId + "'" + Environment.NewLine
                                                    + "onChange={handleChange}" + Environment.NewLine
                                                    + "defaultChecked={values.c" + columnId + "}" + Environment.NewLine
                                                    + "disabled={(authCol." + columnId + " || {}).readonly || !(authCol." + columnId + " || {}).visible}" + Environment.NewLine
                                                  + "/>" + Environment.NewLine
                                                  + "<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>" + Environment.NewLine
                                                  + "<span className='checkbox-btn__label'>{(columnLabel." + columnId + " || {}).ColumnHeader}</span>" + Environment.NewLine
                                                + "</label>" + Environment.NewLine
                                                + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                                  + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                                + ")}" + Environment.NewLine
                                            + "</div>" + Environment.NewLine
                                          + "</Col>" + Environment.NewLine
                                        + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else if (drv["DisplayMode"].ToString() == "ImageButton") //---------ImageButton
                    {
                        if (string.IsNullOrEmpty(drv["ColumnId"].ToString()))
                        {
                            //ImageButton function
                            string functionValue = " " + ColumnName + "({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {" + Environment.NewLine
                                            + "return function (evt) {" + Environment.NewLine
                                              + "this.OnClickColumeName = '" + ColumnName + "';" + Environment.NewLine
                                              + "//Enter Custom Code here, eg: submitForm();" + Environment.NewLine
                                              + "evt.preventDefault();" + Environment.NewLine
                                            + "}.bind(this);" + Environment.NewLine
                                          + "}";
                            functionResults.Add(functionValue);

                            //formik control
                            string formikControlValue = "<Col lg={12} xl={12}>" + Environment.NewLine
                                                + "<div className='form__form-group'>" + Environment.NewLine
                                                    + "<div className='d-block'>" + Environment.NewLine
                                                        + "{(authCol." + ColumnName + " || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol." + ColumnName + " || {}).readonly || !(authCol." + ColumnName + " || {}).visible} onClick={this." + ColumnName + "({ naviBar, submitForm, currMst })} >{auxLabels." + ColumnName + " || (columnLabel." + ColumnName + " || {}).ColumnName}</Button>}" + Environment.NewLine
                                                    + "</div>" + Environment.NewLine
                                                + "</div>" + Environment.NewLine
                                             + "</Col>";
                            formikControlResults.Add(formikControlValue);
                        }
                        else
                        {
                            //save button function call
                            string saveBtnValue = columnId + ": values.c" + columnId + " ? " + Environment.NewLine
                                                 + "JSON.stringify({" + Environment.NewLine
                                                     + "...values.c" + columnId + "," + Environment.NewLine
                                                     + "base64: this.StripEmbeddedBase64Prefix(values.c" + columnId + ".base64)"
                                                    + "}) : null,";
                            saveBtnResults.Add(saveBtnValue);

                            //render label
                            string renderLabelValue = "const " + columnId + " = currDtl." + columnId + " ? (currDtl." + columnId + ".startsWith('{') ? JSON.parse(currDtl." + columnId + ") : { fileName: '', mimeType: 'image/jpeg', base64: currDtl." + columnId + " }) : null; " + Environment.NewLine
                                                      + "const " + columnId + "FileUploadOptions = { " + Environment.NewLine
                                                        + "CancelFileButton: auxSystemLabels.CancelFileBtnLabel, " + Environment.NewLine
                                                        + "DeleteFileButton: auxSystemLabels.DeleteFileBtnLabel, " + Environment.NewLine
                                                        + "MaxImageSize: { " + Environment.NewLine
                                                          + "Width:(columnLabel." + columnId + " || {}).ResizeWidth, " + Environment.NewLine
                                                          + "Height:(columnLabel." + columnId + " || {}).ResizeHeight, " + Environment.NewLine
                                                        + "}, " + Environment.NewLine
                                                        + "MinImageSize: { " + Environment.NewLine
                                                          + "Width:(columnLabel." + columnId + " || {}).ColumnSize, " + Environment.NewLine
                                                          + "Height:(columnLabel." + columnId + " || {}).ColumnHeight, " + Environment.NewLine
                                                        + "}, " + Environment.NewLine
                                                      + "} ";
                            renderLabelResults.Add(renderLabelValue);

                            //formik initial value
                            string formikInitialValue = "c" + columnId + ": " + columnId + ",";
                            formikInitialResults.Add(formikInitialValue);

                            //formik control
                            string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                                  + " <Col lg={12} xl={12}>" + Environment.NewLine
                                        + "<div className='form__form-group'>" + Environment.NewLine
                                          + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                            + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                             + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                            + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                            + ")}" + Environment.NewLine
                                            + "</label>" + Environment.NewLine
                                          + "}" + Environment.NewLine
                                          + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                            + "<div className='form__form-group-field'>" + Environment.NewLine
                                              + "<FileInputField" + Environment.NewLine
                                                + "name='c" + columnId + "'" + Environment.NewLine
                                                + "onChange={this.FileUploadChange(setFieldValue, 'c" + columnId + "')}" + Environment.NewLine
                                                + "fileInfo={{ filename: this.state.filename }}" + Environment.NewLine
                                                + "options={" + columnId + "FileUploadOptions}" + Environment.NewLine
                                                + "value={values.c" + columnId + " || " + columnId + "}" + Environment.NewLine
                                                + "label={auxSystemLabels.PickFileBtnLabel}" + Environment.NewLine
                                                + "onError={(e, fileName) => {this.props.showNotification('E', { message: 'problem loading file ' + fileName })}}" + Environment.NewLine
                                              + "/>" + Environment.NewLine
                                            + "</div>" + Environment.NewLine
                                          + "}" + Environment.NewLine
                                          + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                    + "</Col>" + Environment.NewLine
                                  + "}";
                            formikControlResults.Add(formikControlValue);
                        }
                    }
                    else if (drv["DisplayMode"].ToString() == "Label" || drv["DisplayMode"].ToString() == "Action Button" ||
                             drv["DisplayMode"].ToString() == "DataGridLink" || drv["DisplayMode"].ToString() == "PlaceHolder") //---------Label / Action Button / DataGridLink / PlaceHolder
                    {
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                            + " <Col lg={12} xl={12}>" + Environment.NewLine
                                  + "<div className='form__form-group'>" + Environment.NewLine
                                    + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                      + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                       + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                      + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                      + ")}" + Environment.NewLine
                                      + "</label>" + Environment.NewLine
                                    + "}" + Environment.NewLine
                                  + "</div>" + Environment.NewLine
                              + "</Col>" + Environment.NewLine
                            + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                    else //Treat all the other control as textbox for now
                    {
                        //validator
                        if (drv["RequiredValid"].ToString() == "Y")
                        {
                            string validatorValue = "if (!values.c" + columnId + ") { errors.c" + columnId + " = (columnLabel." + columnId + " || {}).ErrMessage;}";
                            validatorResults.Add(validatorValue);
                        }

                        //save button function call
                        string saveBtnValue = columnId + ": values.c" + columnId + " || '',";
                        saveBtnResults.Add(saveBtnValue);

                        //render label
                        string renderLabelValue = "const " + columnId + " = currDtl." + columnId + ";";
                        renderLabelResults.Add(renderLabelValue);

                        //formik initial value
                        string formikInitialValue = "c" + columnId + ": currDtl." + columnId + " || '',";
                        formikInitialResults.Add(formikInitialValue);

                        //formik control
                        string formikControlValue = "{(authCol." + columnId + " || {}).visible &&" + Environment.NewLine
                              + " <Col lg={12} xl={12}>" + Environment.NewLine
                                    + "<div className='form__form-group'>" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='20px' />) ||" + Environment.NewLine
                                        + "<label className='form__form-group-label'>{(columnLabel." + columnId + " || {}).ColumnHeader} " + ((drv["RequiredValid"].ToString() == "Y") ? "<span className='text-danger'>*</span>" : "")
                                         + "{(columnLabel." + columnId + " || {}).ToolTip && " + Environment.NewLine
                                        + " (<ControlledPopover id={(columnLabel." + columnId + " || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel." + columnId + " || {}).ToolTip} />" + Environment.NewLine
                                        + ")}" + Environment.NewLine
                                        + "</label>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{((" + skeletonEnabled + " && this.constructor.ShowSpinner([[---ScreenName---]]State)) && <Skeleton height='36px' />) ||" + Environment.NewLine
                                        + "<div className='form__form-group-field'>" + Environment.NewLine
                                          + "<Field" + Environment.NewLine
                                            + "type='text'" + Environment.NewLine
                                            + "name='c" + columnId + "'" + Environment.NewLine
                                            + "disabled = {(authCol." + columnId + " || {}).readonly ? 'disabled': '' }"
                                          + "/>" + Environment.NewLine
                                        + "</div>" + Environment.NewLine
                                      + "}" + Environment.NewLine
                                      + "{errors.c" + columnId + " && touched.c" + columnId + " && <span className='form__form-group-error'>{errors.c" + columnId + "}</span>}" + Environment.NewLine
                                    + "</div>" + Environment.NewLine
                                + "</Col>" + Environment.NewLine
                              + "}";
                        formikControlResults.Add(formikControlValue);
                    }
                }

            }
            string functionCnt = string.Join(Environment.NewLine, functionResults);
            string validatorCnt = string.Join(Environment.NewLine, validatorResults);
            string saveBtnCnt = string.Join(Environment.NewLine, saveBtnResults.Select(s=>addIndent(s,10)));
            string renderLabelCnt = string.Join(Environment.NewLine, renderLabelResults);
            string formikInitialCnt = string.Join(Environment.NewLine, formikInitialResults.Select(s => addIndent(s, 18)));
            string formikControlCnt = string.Join(Environment.NewLine, formikControlResults);
            string bindActionCreatorsCnt = string.Join(Environment.NewLine, bindActionCreatorsResults);

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Prompt, Redirect } from 'react-router';
import { Formik, Field, Form } from 'formik';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import classNames from 'classnames';
import DocumentTitle from 'react-document-title';
import LoadingIcon from 'mdi-react/LoadingIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import DatePicker from '../../components/custom/DatePicker';
import NaviBar from '../../components/custom/NaviBar';
import FileInputField from '../../components/custom/FileInput';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import DropdownField from '../../components/custom/DropdownField';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import RintagiScreen from '../../components/custom/Screen';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import {isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getDefaultPath, getNaviPath } from '../../helpers/utils';
import { toMoney, toInputLocalAmountFormat, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist';
import { getNaviBar } from './index';
import [[---ScreenName---]]ReduxObj, { ShowMstFilterApplied } from '../../redux/[[---ScreenName---]]';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.[[---ScreenName---]] || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = '[[---ScreenPrimaryKey---]]';
    this.DtlKeyColumnName = '[[---ScreenDetailKey---]]';
    this.hasChangedContent = false;
    this.confirmUnload = this.confirmUnload.bind(this);
    this.AutoCompleteFilterBy = (option, props) => { return true };
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.ValidatePage = this.ValidatePage.bind(this);
    this.SavePage = this.SavePage.bind(this);
    this.FieldChange = this.FieldChange.bind(this);
    this.DateChange = this.DateChange.bind(this);
    this.StripEmbeddedBase64Prefix = this.StripEmbeddedBase64Prefix.bind(this);
    this.FileUploadChange = this.FileUploadChange.bind(this);
//    this.BGlChartId65InputChange = this.BGlChartId65InputChange.bind(this);
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      filename: '',
      submitting: false,
      Buttons: {},
      ScreenButton: null,
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      isMobile: false
    }
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }
  
  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

");
            sb.Append(functionCnt + Environment.NewLine);
            sb.Append("/* ReactRule: Detail Record Custom Function */");
            sb.Append(DetailCustomFunctionCnt);
            sb.Append(@"
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.[[---ScreenName---]] || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
");
            sb.Append(validatorCnt);
            sb.Append(@"
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
");
            sb.Append("/* ReactRule: Detail Record Save */");
            sb.Append(DetailSaveCnt);
            sb.Append(@"
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.[[---ScreenName---]],
      this.props.[[---ScreenName---]].Mst,
      [
        {
          [[---ScreenDetailKey---]]: values.c[[---ScreenDetailKey---]] || null,
");
            sb.Append(saveBtnCnt);
            sb.Append(@"
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.c[[---ScreenDetailKey---]] ? 'upd' : 'add'),
        }
      ],
      {
        persist: true,
        keepDtl: ScreenButton.buttonType !== 'NewSaveDtl'
      }
    )
  }
 
   /* standard screen button actions */
  CopyRow({ mst, dtl, dtlId, useMobileView }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).[[---ScreenDetailKey---]];
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.[[---ScreenPrimaryKey---]], currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.[[---ScreenName---]].Label);
            this.props.history.push(getEditDtlPath(getNaviPath(naviBar, 'Dtl', '/'), '_'));
          }
          else {
            if (this.props.OnCopy) this.props.OnCopy();
          }
        }
        else {
          this.setState({ ModalOpen: true, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
        }
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  DelDtl({ mst, submitForm, ScreenButton, dtl, dtlId }) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.[[---ScreenDetailKey---]];
        if (currDtlId) {
          const revisedState = {
            ScreenButton
          }
          this.setState(revisedState);
          submitForm();
        }
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });

    }.bind(this);
  }

  SaveCloseDtl({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  NewSaveDtl({ submitForm, ScreenButton, naviBar, mstId, dtl, redirectTo }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  SaveMst({ submitForm, ScreenButton }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  SaveDtl({ submitForm, ScreenButton }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.[[---ScreenName---]] || {};
    const buttons = nextReduxScreenState.Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'Dtl', prevState);
    const currentKey = nextReduxScreenState.key;
    const waiting = nextReduxScreenState.page_saving || nextReduxScreenState.page_loading;
    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;
    if (prevState.submitting && !waiting && nextReduxScreenState.submittedOn > prevState.submittedOn) {
      prevState.setSubmitting(false);
      revisedState.filename = '';
    }

    return revisedState;
  }

 confirmUnload(message, callback) {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const confirm = () => {
      callback(true);
    }
    const cancel = () => {
      callback(false);
    }
    this.setState({ ModalOpen: true, ModalSuccess: confirm, ModalCancel: cancel, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: message });
  }
  
  setDirtyFlag(dirty) {
   /* this is called during rendering but has side-effect, undesirable but only way to pass formik dirty flag around */
    if (dirty) {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = this.confirmUnload;
      registerBlocker(this.confirmUnload);
    }
    else {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = null;
    }
    if (this.props.updateChangedState) this.props.updateChangedState(dirty);
    this.SetCurrentRecordState(dirty);
    return true;
  }

  componentDidMount() {
    this.mediaqueryresponse(this.mobileView);
    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes
    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const suppressLoadPage = this.props.suppressLoadPage;
    if (!suppressLoadPage) {
      const { mstId, dtlId } = { ...this.props.match.params };
      if (!(this.props.[[---ScreenName---]] || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.[[---ScreenName---]] || {};

    if(!this.props.suppressLoadPage) {
      if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
        const _this = this;
        setTimeout(() => _this.props.setSpinner(false), 500);
      }
    }
    
    this.SetPageTitle(currReduxScreenState);
    if (prevstates.key !== (currReduxScreenState.EditDtl || {}).key) {
      if ((prevstates.ScreenButton || {}).buttonType === 'SaveCloseDtl') {
        const currMst = (currReduxScreenState.Mst);
        const currDtl = (currReduxScreenState.EditDtl);
        const dtlList = (currReduxScreenState.DtlList || {}).data || [];

        const naviBar = getNaviBar('Dtl', currMst, currDtl, currReduxScreenState.Label);
        const dtlListPath = getDefaultPath(getNaviPath(naviBar, 'DtlList', '/'));

        this.props.history.push(dtlListPath);
      }
    }

  }

  componentWillUnmount() {
    if (this.blocker) {
      unregisterBlocker(this.blocker);
      this.blocker = null;
    }
    this.mobileView.removeListener(this.mediaqueryresponse);
  }

  handleFocus = (event) => {
    event.target.setSelectionRange(0, event.target.value.length);
  }

  render() {
    const [[---ScreenName---]]State = this.props.[[---ScreenName---]] || {};
    if ([[---ScreenName---]]State.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = [[---ScreenName---]]State.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = [[---ScreenName---]]ReduxObj.GetScreenButtons([[---ScreenName---]]State) || {};
    const auxLabels = [[---ScreenName---]]State.Label || {};
    const auxSystemLabels = [[---ScreenName---]]State.SystemLabel || {};
    const columnLabel = [[---ScreenName---]]State.ColumnLabel || {};
    const currMst = [[---ScreenName---]]State.Mst;
    const currDtl = [[---ScreenName---]]State.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol([[---ScreenName---]]State);
    const authRow = ([[---ScreenName---]]State.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
");
            sb.Append(renderLabelCnt);
            sb.Append(@"
// custome image upload code
//    const TrxDetImg65 = currDtl.TrxDetImg65 ? (currDtl.TrxDetImg65.startsWith('{') ? JSON.parse(currDtl.TrxDetImg65) : { fileName: '', mimeType: 'image/jpeg', base64: currDtl.TrxDetImg65 }) : null;
//    const TrxDetImg65FileUploadOptions = {
//      CancelFileButton: auxSystemLabels.CancelFileBtnLabel,
//      DeleteFileButton: auxSystemLabels.DeleteFileBtnLabel,
//      MaxImageSize: {
//        Width:(columnLabel.TrxDetImg65 || {}).ResizeWidth,
//        Height:(columnLabel.TrxDetImg65 || {}).ResizeHeight,
//      },
//      MinImageSize: {
//        Width:(columnLabel.TrxDetImg65 || {}).ColumnSize,
//        Height:(columnLabel.TrxDetImg65 || {}).ColumnHeight,
//      },
//    }
");
            sb.Append("/* ReactRule: Detail Record Render */");
            sb.Append(DetailRenderCnt);
            sb.Append(@"
/* ReactRule End: Detail Record Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.[[---ScreenName---]]) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
");
            sb.Append(formikInitialCnt);
            sb.Append(@"
                  }}
                  validate={this.ValidatePage}
                  onSubmit={this.SavePage}
                  key={currDtl.key}
                  render={({
                    values,
                    errors,
                    touched,
                    isSubmitting,
                    dirty,
                    setFieldValue,
                    setFieldTouched,
                    handleReset,
                    handleChange,
                    submitForm,
                    handleFocus
                  }) => (
                      <div>
                        {this.setDirtyFlag(dirty) &&
                          <Prompt
                            when={dirty}
                            message={auxSystemLabels.UnsavedPageMsg || ''}
                          />
                        }
                        <div className='account__head'>
                          <Row>
                            <Col xs={9}>
                              <h3 className='account__title'>{DetailRecTitle}</h3>
                              <h4 className='account__subhead subhead'>{DetailRecSubtitle}</h4>
                            </Col>
                            <Col xs={3}>
                              <ButtonToolbar className='f-right'>
                                <UncontrolledDropdown>
                                  <ButtonGroup className='btn-group--icons'>
                                    <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                    {
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],currDtl.[[---ScreenDetailKey---]])).length > 0 &&
                                      <DropdownToggle className='mw-50' outline>
                                        <i className='fa fa-ellipsis-h icon-holder'></i>
                                        {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                      </DropdownToggle>
                                    }
                                  </ButtonGroup>
                                  {
                                    dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                                    <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],currDtl.[[---ScreenDetailKey---]])) return null;
                                          return (
                                            <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, submitForm, mst: currMst, dtl: currDtl, useMobileView })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                        })
                                      }
                                    </DropdownMenu>
                                  }
                                </UncontrolledDropdown>
                              </ButtonToolbar>
                            </Col>
                          </Row>
                        </div>
                        <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}

                          <div className='w-100'>
                            <Row>
            ");
            sb.Append(formikControlCnt);
            sb.Append(@"
                            </Row>
                          </div>
                          <div className='form__form-group mb-0'>
                            <Row className='btn-bottom-row'>
                              {useMobileView && <Col xs={3} sm={2} className='btn-bottom-column'>
                                <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className='fa fa-long-arrow-left'></i></Button>
                              </Col>}
                              <Col
                                xs={useMobileView ? 9 : 12}
                                sm={useMobileView ? 10 : 12}>
                                <Row>
                                  {
                                    bottomButtonList
                                      .filter(v => v.expose)
                                      .map((v, i, a) => {
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).[[---ScreenPrimaryKey---]],currDtl.[[---ScreenDetailKey---]])) return null;
                                        const buttonCount = a.length;
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === a.length - 1;
                                        const outlineProperty = lastBtn ? false : true;

                                        return (
                                          <Col key={v.tid} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            <Button color='success' type='button' outline={outlineProperty} className='account__btn' disabled={isSubmitting} onClick={this.ScreenButtonAction[v.buttonType]({ submitForm, naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}>{v.label}</Button>
                                          </Col>
                                        )
                                      })
                                  }
                                </Row>
                              </Col>
                            </Row>
                          </div>
                        </Form>
                      </div>
                    )}
                />
              </div>
            </div>
          </div>
        </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  error: state.error,
  [[---ScreenName---]]: state.[[---ScreenName---]],
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: [[---ScreenName---]]ReduxObj.LoadPage.bind([[---ScreenName---]]ReduxObj) },
    { AddDtl: [[---ScreenName---]]ReduxObj.AddDtl.bind([[---ScreenName---]]ReduxObj) },
    { SavePage: [[---ScreenName---]]ReduxObj.SavePage.bind([[---ScreenName---]]ReduxObj) },
");
            sb.Append(bindActionCreatorsCnt);
            sb.Append(@"
  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

").Replace("[[---ScreenName---]]", screenName).Replace("[[---ScreenPrimaryKey---]]", screenPrimaryKey).Replace("[[---ScreenDetailKey---]]", screenDetailKey);

            return sb;
        }
        //End React Page Generation

        //Redux Generation
        private StringBuilder MakeReactJsRedux(string screenId, string screenName, DataView dvReactRule)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenDef = screenName + screenId;

            string screenPrimaryKey = "";
            string screenPrimaryKeyName = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    screenPrimaryKeyName = drv["PrimaryKey"].ToString();
                    break;
                }
            }

            string screenDetailKeyName = "";
            string screenDetailKey = "";
            string screenDetailTableName = "";

            string ExpandDtl = "";
            string screenTypeName = dvItms[0]["ScreenTypeName"].ToString();

            if ("I1".IndexOf(screenTypeName) >= 0)
            {
                ExpandDtl = @"ExpandDtl(dtlList, copy) {
                                return dtlList;
                             }";
            }
            else if ("I2".IndexOf(screenTypeName) >= 0)
            {
                ExpandDtl = @"ExpandDtl(dtlList, copy) {
                                if (!copy) return dtlList;
                                else if (!this.allowTmpDtl) return []; 
                                else { const now = Date.now();
                                  return dtlList.map((v,i) => {
                                  return {
                                    ...v,
                                    [[---screenPrimaryKey---]]: null,
                                    [[---screenDetailKey---]]: null,
                                    TmpKeyId: now + i,
                                  }
                                })
                              };
                            }    ";
            }

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKeyName = drv["PrimaryKey"].ToString();
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    screenDetailTableName = drv["TableName"].ToString();
                    break;
                }
            }

            List<string> GetCriteriaTopResults = new List<string>();
            List<string> GetDdlListResults = new List<string>();
            List<string> ScreenOnDemandDefResults = new List<string>();
            List<string> GetDefaultDtlResults = new List<string>();
            List<string> GetCriteriaBotResults = new List<string>();
            List<string> ReduxCustomFunctionResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            foreach (DataRowView drv in dvReactRule)
            {
                if (drv["ReduxEventId"].ToString() == "1") //Redux custom function
                {
                    string ReduxCustomFunctionValue = drv["ReduxRuleProg"].ToString();
                    ReduxCustomFunctionResults.Add(ReduxCustomFunctionValue);
                }
            }

            //Screen Criteria
            DataTable dtScrCri = (new AdminAccess()).GetScrCriteria(screenId, dbConnectionString, dbPassword);
            foreach (DataRow dr in dtScrCri.Rows)
            {
                string screenCriId = dr["ScreenCriId"].ToString();
                string columnName = dr["ColumnName"].ToString();
                string displayMode = dr["DisplayMode"].ToString();
                string DdlKeyColumnName = dr["DdlKeyColumnName"].ToString();
                string DdlRefColumnName = dr["DdlRefColumnName"].ToString();
                string GetCriteriaTopValue = "";
                string GetCriteriaBotValue = "";

                if (displayMode == "TextBox")
                {
                    GetCriteriaTopValue = "{ columnName: '" + columnName + "', payloadDdlName: '', keyName:'', labelName:'', isCheckBox:false, isAutoComplete: false, apiServiceName: '', actionTypeName: 'GET_" + columnName + "' },";
                }
                else if (displayMode == "AutoComplete")
                {
                    GetCriteriaTopValue = "{ columnName: '" + columnName + "', payloadDdlName: '" + columnName + "List', isAutoComplete: true, apiServiceName: 'GetScreenCri" + columnName + "List', actionTypeName: 'GET_DDL_CRI" + columnName + "' },";
                }
                else if (displayMode == "DropDownList")
                {
                    GetCriteriaTopValue = "{ columnName: '" + columnName + "', payloadDdlName: '" + columnName + "List', keyName:'" + DdlKeyColumnName + "', labelName:'" + DdlRefColumnName + "', isCheckBox:false, isAutoComplete: false, apiServiceName: 'GetScreenCri" + columnName + "List', actionTypeName: 'GET_DDL_CRI" + columnName + "' },";
                }

                GetCriteriaBotValue = "|| (state.ScreenCriteria." + columnName + " || {}).LastCriteria";

                GetCriteriaTopResults.Add(GetCriteriaTopValue);
                GetCriteriaBotResults.Add(GetCriteriaBotValue);
            }

            foreach (DataRowView drv in dvItms)
            {
                string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string columnName = drv["ColumnName"].ToString();
                string DisplayMode = drv["DisplayMode"].ToString();
                string DdlFtrColumnName = drv["DdlFtrColumnName"].ToString();
                string DdlFtrTableId = drv["DdlFtrTableId"].ToString();

                if (DisplayMode == "AutoComplete")
                {
                    string GetDdlListValue = "{ columnName: '" + columnId + "', payloadDdlName:'" + columnId + "List', keyName:'" + columnId + "',labelName:'" + columnId + "Text', forMst: true, isAutoComplete:true, apiServiceName: 'Get" + columnId + "List', " + (string.IsNullOrEmpty(DdlFtrColumnName) ? "" : ("filterByMaster:true, filterByColumnName:'" + DdlFtrColumnName + DdlFtrTableId + "',")) + "actionTypeName: 'GET_DDL_" + columnId + "' },";
                    GetDdlListResults.Add(GetDdlListValue);
                }
                else if (DisplayMode == "DropDownList" || DisplayMode == "RadioButtonList" || DisplayMode == "ListBox" || DisplayMode == "WorkflowStatus" || DisplayMode == "AutoListBox" || DisplayMode == "DataGridLink")
                {
                    string GetDdlListValue = "{ columnName: '" + columnId + "', payloadDdlName:'" + columnId + "List', keyName:'" + columnId + "',labelName:'" + columnId + "Text', forMst: true, isAutoComplete:false, apiServiceName: 'Get" + columnId + "List', actionTypeName: 'GET_DDL_" + columnId + "' },";
                    GetDdlListResults.Add(GetDdlListValue);
                }
                else if (DisplayMode == "ImageButton")
                {
                    string ScreenOnDemandDefValue = "{ columnName: '" + columnId + "', tableColumnName: '" + columnName + "', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_" + columnId + "' },";
                    ScreenOnDemandDefResults.Add(ScreenOnDemandDefValue);
                }

                if (drv["MasterTable"].ToString() == "N")
                {
                    string GetDefaultDtlValue = columnId + ": null,";
                    GetDefaultDtlResults.Add(GetDefaultDtlValue);
                }

            }

            string GetCriteriaTopCnt = string.Join(Environment.NewLine, GetCriteriaTopResults.Select(s => addIndent(s, 0)));
            string GetDdlListCnt = string.Join(Environment.NewLine, GetDdlListResults.Select(s => addIndent(s, 0)));
            string ScreenOnDemandDefCnt = string.Join(Environment.NewLine, ScreenOnDemandDefResults.Select(s => addIndent(s, 0)));
            string GetDefaultDtlCnt = string.Join(Environment.NewLine, GetDefaultDtlResults.Select(s => addIndent(s, 0)));
            string GetCriteriaBotCnt = string.Join(Environment.NewLine, GetCriteriaBotResults.Select(s => addIndent(s, 0)));
            string ReduxCustomFunctionCnt = string.Join(Environment.NewLine, ReduxCustomFunctionResults.Select(s => addIndent(s, 0)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import { getAsyncTypes } from '../helpers/actionType'
import * as [[---ScreenName---]]Service from '../services/[[---ScreenName---]]Service'
import {RintagiScreenRedux,initialRintagiScreenReduxState} from './_ScreenReducer'
const Label = {
  PostToAp: 'Post to AP',
}
class [[---ScreenName---]]Redux extends RintagiScreenRedux {
    allowTmpDtl = false;
    constructor() {
      super();
      this.ActionApiNameMapper = {
        'GET_SEARCH_LIST' : 'Get[[---ScreenDef---]]List',
        'GET_MST' : 'Get[[---ScreenDef---]]ById',
        'GET_DTL_LIST' : 'Get[[---ScreenDef---]]DtlById',
      }
      this.ScreenDdlDef = [
");
            sb.Append(GetDdlListCnt);
            sb.Append(@"
      ]
      this.ScreenOnDemandDef = [
");
            sb.Append(ScreenOnDemandDefCnt);
            sb.Append(@"
//        { columnName: 'TrxDetImg65', tableColumnName: 'TrxDetImg', forMst: false, apiServiceName: 'GetColumnContent', actionTypeName: 'GET_COLUMN_TRXDETIMG65' },
      ]

      this.ScreenCriDdlDef = [
");
            sb.Append(GetCriteriaTopCnt);
            sb.Append(@"
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
    GetScreenName(){return '[[---ScreenName---]]'}
    GetMstKeyColumnName(isUnderlining = false) {return isUnderlining ? '[[---screenPrimaryKeyName---]]' :  '[[---screenPrimaryKey---]]'}
    GetDtlKeyColumnName(isUnderlining = false) {return isUnderlining ? '[[---screenDetailKeyName---]]'  :'[[---screenDetailKey---]]'}
    GetPersistDtlName() {return this.GetScreenName() + '_Dtl'}
    GetPersistMstName() {return this.GetScreenName() + '_Mst'}
    GetWebService() {return [[---ScreenName---]]Service}
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
       ");
            sb.Append(GetDefaultDtlCnt);
            sb.Append(@"
      }
    }
    ExpandMst(mst, state, copy) {
      return {
        ...mst,
		 key: Date.now(),
        [[---screenPrimaryKey---]]: copy ? null : mst.[[---screenPrimaryKey---]],
		
        // CurrencyId64Text: GetCurrencyId64Cd(mst.CurrencyId64, state),
        // MemberId64Text: GetMemberId64Text(mst.MemberId64, state),
        // /* specific app rule */
        // Posted64: copy ? 'N' : mst.Posted64,
        // TrxTotal64: copy ? '0' : mst.TrxTotal64,
      }
    }

");
            sb.Append(ExpandDtl);
            sb.Append(@"
    
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
");
            sb.Append(ReduxCustomFunctionCnt);
            sb.Append(@"
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
");
            sb.Append(GetCriteriaBotCnt);
            sb.Append(@"
      || state.ScreenCriteria.SearchStr;
  }

  export default new [[---ScreenName---]]Redux()
            ")
                  .Replace("[[---ScreenName---]]", screenName)
                  .Replace("[[---ScreenDef---]]", screenDef)
                  .Replace("[[---screenPrimaryKey---]]", screenPrimaryKey)
                  .Replace("[[---screenPrimaryKeyName---]]", screenPrimaryKeyName)
                  .Replace("[[---screenDetailKey---]]", screenDetailKey)
                  .Replace("[[---screenDetailKeyName---]]", screenDetailKeyName);
            return sb;
        }
        //End Redux Generation

        //React Service Generation
        private StringBuilder MakeReactJsService(string screenId, string screenName, DataView dvReactRule)
        {
            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));
            string screenDef = screenName + screenId;
            List<string> GetCriteriaResults = new List<string>();
            List<string> GetDdlListResults = new List<string>();
            List<string> ServiceCustomFunctionResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            foreach (DataRowView drv in dvReactRule)
            {
                if (drv["ServiceEventId"].ToString() == "1") //React service custom function
                {
                    string ServiceCustomFunctionValue = drv["ServiceRuleProg"].ToString();
                    ServiceCustomFunctionResults.Add(ServiceCustomFunctionValue);
                }
            }

            string ServiceCustomFunctionCnt = string.Join(Environment.NewLine, ServiceCustomFunctionResults.Select(s => addIndent(s, 24)));

            //Screen Criteria
            DataTable dtScrCri = (new AdminAccess()).GetScrCriteria(screenId, dbConnectionString, dbPassword);
            foreach (DataRow dr in dtScrCri.Rows)
            {
                string screenCriId = dr["ScreenCriId"].ToString();
                string columnName = dr["ColumnName"].ToString();
                string GetCriteriaValue = "export function GetScreenCri" + columnName + "List(query, topN, filterBy, accessScope){" + Environment.NewLine
                                            + "return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenCriteriaDdlList'" + Environment.NewLine
                                                + ",{" + Environment.NewLine
                                                    + "requestOptions: {" + Environment.NewLine
                                                        + "body: JSON.stringify({" + Environment.NewLine
                                                            + "screenCriId: " + screenCriId + "," + Environment.NewLine
                                                            + "query: query || ''," + Environment.NewLine
                                                            + "topN: topN || 0," + Environment.NewLine
                                                            + "filterBy: filterBy || null" + Environment.NewLine
                                                        + "})," + Environment.NewLine
                                                    + "}," + Environment.NewLine
                                                + "...(getAccessControlInfo())," + Environment.NewLine
                                                + "...(accessScope)" + Environment.NewLine
                                                + "}" + Environment.NewLine
                                            + ")" + Environment.NewLine
                                          + "}";
                GetCriteriaResults.Add(GetCriteriaValue);
            }

            //Dropdownlist or Autocomplete
            foreach (DataRowView drv in dvItms)
            {
                string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string DisplayMode = drv["DisplayMode"].ToString();
                if (DisplayMode == "AutoComplete" || DisplayMode == "DropDownList" || DisplayMode == "RadioButtonList" || DisplayMode == "ListBox" || DisplayMode == "WorkflowStatus" || DisplayMode == "AutoListBox" || DisplayMode == "DataGridLink")
                {
                    string GetDdlListValue = "export function Get" + columnId + "List(query, topN, filterBy,accessScope){" + Environment.NewLine
                                                + "return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/Get" + columnId + "List'" + Environment.NewLine
                                                + @"
                                                    ,{
                                                        requestOptions: {
                                                            body: JSON.stringify({
                                                                query: query || '',
                                                                topN: topN || 0,
                                                                filterBy: filterBy || null
                                                            }),
                                                        },
                                                        ...(getAccessControlInfo()),
                                                        ...(accessScope)
                                                    }
                                                )
                                            }";

                    GetDdlListResults.Add(GetDdlListValue);
                }
            }

            string GetCriteriaCnt = string.Join(Environment.NewLine, GetCriteriaResults.Select(s => addIndent(s, 0)));
            string GetDdlListCnt = string.Join(Environment.NewLine, GetDdlListResults.Select(s => addIndent(s, 0)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
import {fetchData,getAccessControlInfo, getAccessScope, baseUrl} from './webAPIBase';

let activeScope = {};

export function setAccessScope(scope) {
    activeScope = {
        ...activeScope,
        ...scope,
    }
}

export function GetAuthCol(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetAuthCol'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetAuthRow(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetAuthRow'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )

}
export function GetScreenLabel(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenLabel'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope || getAccessScope())
        }
    )

}

export function GetLabels(labelCat,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetLabels'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    labelCat:labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetSystemLabels(labelCat,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetSystemLabels'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    labelCat:labelCat
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function GetScreenButtonHlp(labelCat,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenButtonHlp'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenHlp(labelCat,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenHlp'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenCriteria(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenCriteria'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewMst(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetNewMst'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetNewDtl(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetNewDtl'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetScreenFilter(accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetScreenFilter'
        ,{
            requestOptions: {
                body: JSON.stringify({
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function GetColumnContent(mstId, dtlId, columnName, isMaster, screenColumnName, accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/GetColumnContent'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    mstId: mstId || '',
                    dtlId: dtlId || '',
                    screenColumnName: screenColumnName,
                    columnName: columnName,
                    isMaster: isMaster,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function Get[[---ScreenDef---]]List(searchStr, topN, filterId,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/Get[[---ScreenDef---]]List'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    searchStr: searchStr || '',
                    topN: topN || 0,
                    filterId: ('' +  (filterId || 0)),
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function Get[[---ScreenDef---]]ById(keyId,accessScope){   
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/Get[[---ScreenDef---]]ById'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: {
                        currentScreenCriteria : JSON.stringify({}),
                    },
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function Get[[---ScreenDef---]]DtlById(keyId,filterId,accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/Get[[---ScreenDef---]]DtlById'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    keyId: keyId || '',
                    options: {
                        currentScreenCriteria : JSON.stringify({}),
                    },
                    filterId: filterId || 0,
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function LoadInitPage(options,accessScope) {
    const reqJson = JSON.stringify({
        options: options
    });
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/LoadInitPage'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}
export function SaveData(mst,dtl,options,accessScope){
    const reqJson = JSON.stringify({
        mst: mst,
        dtl: dtl,
        options: options
    });
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/SaveData'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function DelMst(mst,options,accessScope){
    const reqJson = JSON.stringify({
        mst: mst,
        options: options
    });
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/DelMst'
        ,{
            requestOptions: {
                body: reqJson,
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

export function SetScreenCriteria(criteriaValues, accessScope){
    return fetchData(baseUrl+'/[[---ScreenName---]]Ws.asmx/SetScreenCriteria'
        ,{
            requestOptions: {
                body: JSON.stringify({
                    criteriaValues: criteriaValues
                }),
            },
            ...(getAccessControlInfo()),
            ...(accessScope)
        }
    )
}

/*screen criteria dll and screen dropdownlist/autocomplete*/           
            ");
            sb.Append(GetCriteriaCnt);
            sb.Append(GetDdlListCnt);
            sb.Append("/* ReactRule: Service Custom Function */");
            sb.Append(ServiceCustomFunctionCnt);
            sb.Append("/* ReactRule End: Service Custom Function */")
              .Replace("[[---ScreenName---]]", screenName)
              .Replace("[[---ScreenDef---]]", screenDef);

            return sb;
        }
        //End React Service Generation

        //Asmx Generation
        private StringBuilder MakeReactJsAsmx(string screenId, string screenName)
        {
            List<string> AsmxCustomFunctionResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            DataView dvAsmxRule = new DataView((new WebRule()).WrGetWebRule(screenId, dbConnectionString, dbPassword));

            DataView dvItms = new DataView((new WebRule()).WrGetScreenObj(screenId, CultureId, null, dbConnectionString, dbPassword));

            string screenPrimaryKey = "";
            string screenPrimaryKeyName = "";
            string screenPrimaryKeyType = "";
            string screenPrimaryKeyDis = "";
            string screenPrimaryTableName = "";
            string SystemId = "";
            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "Y" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenPrimaryKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    screenPrimaryKeyName = drv["PrimaryKey"].ToString();
                    screenPrimaryKeyType = drv["DataTypeSByteOle"].ToString();
                    screenPrimaryKeyDis = drv["DisplayMode"].ToString();
                    screenPrimaryTableName = drv["TableName"].ToString();
                    SystemId = drv["SystemId"].ToString();
                    break;
                }
            }

            string screenDetailKeyName = "";
            string screenDetailKey = "";
            string screenDetailTableName = "";

            foreach (DataRowView drv in dvAsmxRule)
            {
                if (drv["AsmxEventId"].ToString() == "5") //Asmx custom function
                {
                    string AsmxCustomFunctionValue = drv["AsmxRuleProg"].ToString();
                    AsmxCustomFunctionResults.Add(AsmxCustomFunctionValue);
                }
            }

            string AsmxCustomFunctionCnt = string.Join(Environment.NewLine, AsmxCustomFunctionResults.Select(s => addIndent(s, 24)));

            foreach (DataRowView drv in dvItms)
            {
                if (drv["MasterTable"].ToString() == "N" && !string.IsNullOrEmpty(drv["PrimaryKey"].ToString()))
                {
                    screenDetailKeyName = drv["PrimaryKey"].ToString();
                    screenDetailKey = drv["PrimaryKey"].ToString() + drv["TableId"].ToString();
                    screenDetailTableName = drv["TableName"].ToString();
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(("<%@ WebService Language=\"C#\" Class=\"" + Config.AppNameSpace + ".Web.[[---ScreenName---]]Ws\" %>") + Environment.NewLine);
            sb.Append(("namespace " + Config.AppNameSpace + ".Web") + Environment.NewLine);
            sb.Append(@"
                {
                    using System;
                    using System.Data;
                    using System.Web;
                    using System.Web.Services;
                    using RO.Facade3;
                    using RO.Common3;
                    using RO.Common3.Data;
                    using RO.Rule3;
                    using RO.Web;
                    using System.Xml;
                    using System.Collections;
                    using System.IO;
                    using System.Text;
                    using System.Web.Script.Services;
                    using System.Text.RegularExpressions;
                    using System.Collections.Generic;
                    using System.Web.SessionState;
                    using System.Linq;
            ");
            sb.Append(GetScreenDataSet(screenId, screenName, screenPrimaryKey, dvItms));
            sb.Append(@"
            [ScriptService()]
            [WebService(Namespace = ""http://Rintagi.com/"")]
            [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
            public partial class [[---ScreenName---]]Ws : RO.Web.AsmxBase
            {
                const int screenId = [[---ScreenId---]];
                const byte systemId = [[---systemId---]];
                const string programName = ""[[---ScreenDef---]]"";

                protected override byte GetSystemId() { return systemId; }
                protected override int GetScreenId() { return screenId; }
                protected override string GetProgramName() { return programName; }
                protected override string GetValidateMstIdSPName() { return ""GetLis[[---ScreenDef---]]""; }
                protected override string GetMstTableName(bool underlying = true) { return ""[[---screenPrimaryTableName---]]""; }
                protected override string GetDtlTableName(bool underlying = true) { return ""[[---screenDetailTableName---]]""; }
                protected override string GetMstKeyColumnName(bool underlying = false) { return underlying ? ""[[---screenPrimaryKeyName---]]"" : ""[[---ScreenPrimaryKey---]]""; }
                protected override string GetDtlKeyColumnName(bool underlying = false) { return underlying ? ""[[---screenDetailKeyName---]]"" : ""[[---screenDetailKey---]]""; }
            ");
            sb.Append(GetScreenDdlDef(screenId, screenName, screenPrimaryKey, dvItms) + Environment.NewLine);
            sb.Append(GetScreenDataMappingSet(screenId, screenName, screenPrimaryKey, screenPrimaryKeyType, screenPrimaryKeyDis, dvItms, dvAsmxRule) + Environment.NewLine);
            sb.Append(GetScreenCRUD(screenId, screenName, screenPrimaryKey, screenPrimaryKeyType, screenPrimaryKeyDis, dvItms, dvAsmxRule) + Environment.NewLine);
            sb.Append(GetScreenDdlFunctions(screenId, screenName, screenPrimaryKey, screenPrimaryKeyType, screenPrimaryKeyDis, dvItms) + Environment.NewLine);
            sb.Append(("/* AsmxRule: Custom Function */") + Environment.NewLine);
            sb.Append(AsmxCustomFunctionCnt + Environment.NewLine);
            sb.Append(@"
             /* AsmxRule End: Custom Function */
           
            }
        }
            ").Replace("[[---ScreenName---]]", screenName)
              .Replace("[[---ScreenPrimaryKey---]]", screenPrimaryKey)
              .Replace("[[---screenPrimaryKeyName---]]", screenPrimaryKeyName)
              .Replace("[[---ScreenDef---]]", screenName + screenId)
              .Replace("[[---ScreenId---]]", screenId)
              .Replace("[[---screenDetailKey---]]", screenDetailKey)
              .Replace("[[---screenDetailKeyName---]]", screenDetailKeyName)
              .Replace("[[---screenPrimaryTableName---]]", screenPrimaryTableName)
              .Replace("[[---screenDetailTableName---]]", screenDetailTableName)
              .Replace("[[---systemId---]]", SystemId);

            return sb;
        }

        private StringBuilder GetScreenDataSet(string screenId, string screenName, string screenPrimaryKey, DataView dvItms)
        {
            List<string> ScreenDataSetMasterResults = new List<string>();
            List<string> ScreenDataSetDetailResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;
            foreach (DataRowView drv in dvItms)
            {
                string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string ColumnName = drv["ColumnName"].ToString();

                if (drv["DefAlways"].ToString() == "N" || (drv["DefAlways"].ToString() == "Y" && string.IsNullOrEmpty(drv["SystemValue"].ToString())))
                {
                    if (drv["DisplayMode"].ToString() != "DataGridLink")
                    {
                        if (drv["MasterTable"].ToString() == "Y")
                        {
                            if (drv["DisplayMode"].ToString() != "ImageButton")
                            {
                                string ScreenDataSetValue = "columns.Add(\"" + columnId + "\", typeof(string));";
                                ScreenDataSetMasterResults.Add(ScreenDataSetValue);
                            }
                        }
                        else
                        {
                            string ScreenDataSetValue = "columns.Add(\"" + columnId + "\", typeof(string));";
                            ScreenDataSetDetailResults.Add(ScreenDataSetValue);
                        }
                    }
                }
            }

            string ScreenDataSetMasterCnt = string.Join(Environment.NewLine, ScreenDataSetMasterResults.Select(s => addIndent(s, 24)));
            string ScreenDataSetDetailCnt = string.Join(Environment.NewLine, ScreenDataSetDetailResults.Select(s => addIndent(s, 24)));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                public class [[---ScreenDef---]] : DataSet
                {
                    public [[---ScreenDef---]]()
                    {
                        this.Tables.Add(MakeColumns(new DataTable(""[[---ScreenName---]]"")));
                        this.Tables.Add(MakeDtlColumns(new DataTable(""[[---ScreenName---]]Def"")));
                        this.Tables.Add(MakeDtlColumns(new DataTable(""[[---ScreenName---]]Add"")));
                        this.Tables.Add(MakeDtlColumns(new DataTable(""[[---ScreenName---]]Upd"")));
                        this.Tables.Add(MakeDtlColumns(new DataTable(""[[---ScreenName---]]Del"")));
                        this.DataSetName = ""[[---ScreenDef---]]"";
                        this.Namespace = ""http://Rintagi.com/DataSet/[[---ScreenDef---]]"";
                    }

                    private DataTable MakeColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
");
            sb.Append(ScreenDataSetMasterCnt);
            sb.Append(@"
                        return dt;
                    }

                    private DataTable MakeDtlColumns(DataTable dt)
                    {
                        DataColumnCollection columns = dt.Columns;
");
            sb.Append("columns.Add(\"" + screenPrimaryKey + "\", typeof(string));" + Environment.NewLine);
            sb.Append(ScreenDataSetDetailCnt);
            sb.Append(@"
                        return dt;
                    }
                }
            ");

            return sb;
        }

        private StringBuilder GetScreenDdlDef(string screenId, string screenName, string screenPrimaryKey, DataView dvItms)
        {
            List<string> ScreenDdlDefResults = new List<string>();
            foreach (DataRowView drv in dvItms)
            {
                string ColumnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string ColumnName = drv["ColumnName"].ToString();
                string SPName = "GetDdl" + ColumnName + DbId + "S" + drv["ScreenObjId"].ToString();
                string DdlFtrColumnId = drv["DdlFtrColumnId"].ToString();
                string DdlFtrColumnName = drv["DdlFtrColumnName"].ToString();
                string DdlAdnColumnName = drv["DdlAdnColumnName"].ToString();
                string DdlFtrTableId = drv["DdlFtrTableId"].ToString();
                string DdlFtrDataType = drv["DdlFtrDataType"].ToString();
                string RefColSrc = DdlFtrTableId == dvItms[0]["TableId"].ToString() ? "Mst" : "Dtl";
                string AdditionalColumn = string.IsNullOrEmpty(DdlFtrColumnId) ? "" : "{\"refCol\",\"" + DdlAdnColumnName + "\"},{\"refColDataType\",\"" + DdlFtrDataType + "\"},{\"refColSrc\",\"" + RefColSrc + "\"},{\"refColSrcName\",\"" + DdlFtrColumnName + DdlFtrTableId + "\"}";

                if (drv["DisplayMode"].ToString() == "AutoComplete" || drv["DisplayMode"].ToString() == "DropDownList" || drv["DisplayMode"].ToString() == "RadioButtonList" || drv["DisplayMode"].ToString() == "ListBox" || drv["DisplayMode"].ToString() == "WorkflowStatus" || drv["DisplayMode"].ToString() == "AutoListBox" || drv["DisplayMode"].ToString() == "DataGridLink")
                {
                    if (drv["MasterTable"].ToString() == "Y")
                    {
                        string ScreenDdlDefValue = "{\"" + ColumnId + "\", new SerializableDictionary<string,string>() {{\"scr\",screenId.ToString()},{\"csy\",systemId.ToString()},{\"conn\",\"\"},{\"addnew\",\"N\"},{\"isSys\",\"N\"}, {\"method\",\"" + SPName + "\"},{\"mKey\",\"" + ColumnId + "\"},{\"mVal\",\"" + ColumnId + "Text\"}, " + AdditionalColumn + "}},";
                        ScreenDdlDefResults.Add(ScreenDdlDefValue);
                    }
                    else
                    {
                        string ScreenDdlDefValue = "{\"" + ColumnId + "\", new SerializableDictionary<string,string>() {{\"scr\",screenId.ToString()},{\"csy\",systemId.ToString()},{\"conn\",\"\"},{\"addnew\",\"N\"},{\"isSys\",\"N\"}, {\"method\",\"" + SPName + "\"},{\"mKey\",\"" + ColumnId + "\"},{\"mVal\",\"" + ColumnId + "Text\"}, " + AdditionalColumn + "}},";
                        ScreenDdlDefResults.Add(ScreenDdlDefValue);
                    }
                }
            }

            string ScreenDdlDefCnt = string.Join(Environment.NewLine, ScreenDdlDefResults);

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
               Dictionary<string, SerializableDictionary<string, string>> ddlContext = new Dictionary<string, SerializableDictionary<string, string>>(){
            ");
            sb.Append(ScreenDdlDefCnt + Environment.NewLine);
            sb.Append("};");

            return sb;
        }

        private StringBuilder GetScreenDataMappingSet(string screenId, string screenName, string screenPrimaryKey, string screenPrimaryKeyType, string screenPrimaryKeyDis, DataView dvItms, DataView dvAsmxRule)
        {
            List<string> InitMasterTableResults = new List<string>();
            List<string> InitDetailTableResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            List<string> MakeTypRowResults = new List<string>();
            List<string> MakeDisRowResults = new List<string>();
            List<string> MakeColRowResults = new List<string>();
            List<string> PrepDataResults = new List<string>();
            List<string> InitMasterResults = new List<string>();
            List<string> InitDtlResults = new List<string>();
            int ii = 0;

            foreach (DataRowView drv in dvAsmxRule)
            {
                if (drv["AsmxEventId"].ToString() == "1") //Init Master Table
                {
                    string InitMasterTableValue = drv["AsmxRuleProg"].ToString();
                    InitMasterTableResults.Add(InitMasterTableValue);
                }
                else if (drv["AsmxEventId"].ToString() == "2") //Init Detail Table
                {
                    string InitDetailTableValue = drv["AsmxRuleProg"].ToString();
                    InitDetailTableResults.Add(InitDetailTableValue);
                }
            }

            string InitMasterTableCnt = string.Join(Environment.NewLine, InitMasterTableResults.Select(s => addIndent(s, 24)));
            string InitDetailTableCnt = string.Join(Environment.NewLine, InitDetailTableResults.Select(s => addIndent(s, 24)));

            foreach (DataRowView drv in dvItms)
            {
                string ColumnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string ColumnName = drv["ColumnName"].ToString();
                string PrimaryKey = drv["PrimaryKey"].ToString();
                string DataType = drv["DataTypeDByteOle"].ToString();
                string DisplayMode = drv["DisplayMode"].ToString();
                string DefaultValue = drv["DefaultValue"].ToString();
                string ColumnLength = (DataType.Contains("Char") && !string.IsNullOrEmpty(drv["TableId"].ToString())) ? drv["ColumnLength"].ToString() : "9999999";

                if (drv["MasterTable"].ToString() == "Y")
                {
                    if (drv["DefAlways"].ToString() == "N" || (drv["DefAlways"].ToString() == "Y" && string.IsNullOrEmpty(drv["SystemValue"].ToString())))
                    {
                        if (ColumnName == PrimaryKey)
                        {
                            string PrepDataValue = "if (bAdd) { dr[\"" + ColumnId + "\"] = string.Empty; } else { dr[\"" + ColumnId + "\"] = mst[\"" + ColumnId + "\"]; }" + Environment.NewLine
                                                 + "drType[\"" + ColumnId + "\"] = \"" + DataType + "\"; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                            PrepDataResults.Add(PrepDataValue);
                        }
                        else
                        {
                            if (DisplayMode == "TextBox") //---------Textbox
                            {
                                if (!string.IsNullOrEmpty(drv["ColumnId"].ToString()))
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = (mst[\"" + ColumnId + "\"] ?? \"\").Trim().Left(" + ColumnLength + "); } catch { }" + Environment.NewLine
                                                        + "drType[\"" + ColumnId + "\"] = \"" + DataType + "\"; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                                else
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = (mst[\"" + ColumnId + "\"] ?? \"\").Trim().Left(" + ColumnLength + "); } catch { }" + Environment.NewLine
                                                        + "drType[\"" + ColumnId + "\"] = string.Empty; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                            }
                            else if (DisplayMode == "Currency") //---------Currency
                            {
                                if (!string.IsNullOrEmpty(drv["ColumnId"].ToString()))
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = Decimal.Parse((mst[\"" + ColumnId + "\"] ?? \"\").Trim(), System.Globalization.NumberStyles.Currency, new System.Globalization.CultureInfo(base.LUser.Culture)).ToString(); } catch { }" + Environment.NewLine
                                                       + "drType[\"" + ColumnId + "\"] = \"" + DataType + "\"; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                                else
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = Decimal.Parse((mst[\"" + ColumnId + "\"] ?? \"\").Trim(), System.Globalization.NumberStyles.Currency, new System.Globalization.CultureInfo(base.LUser.Culture)).ToString(); } catch { }" + Environment.NewLine
                                                       + "drType[\"" + ColumnId + "\"] = string.Empty; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                            }
                            else if (DisplayMode == "CheckBox") //---------CheckBox
                            {
                                if (!string.IsNullOrEmpty(drv["ColumnId"].ToString()))
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = (mst[\"" + ColumnId + "\"] ?? \"\").Trim().Left(" + ColumnLength + "); } catch { }" + Environment.NewLine
                                                         + "drType[\"" + ColumnId + "\"] = \"Char\"; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                                else
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = (mst[\"" + ColumnId + "\"] ?? \"\").Trim().Left(" + ColumnLength + "); } catch { }" + Environment.NewLine
                                                         + "drType[\"" + ColumnId + "\"] = string.Empty; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }

                            }
                            else if (DisplayMode == "Action Button" || DisplayMode == "ImageButton" ||
                                     DisplayMode == "DataGridLink" || DisplayMode == "PlaceHolder") //---------Action Button / ImageButton / DataGridLink / PlaceHolder
                            {
                                string PrepDataValue = "";
                                PrepDataResults.Add(PrepDataValue);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(drv["ColumnId"].ToString()))
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = mst[\"" + ColumnId + "\"]; } catch { }" + Environment.NewLine
                                                + "drType[\"" + ColumnId + "\"] = \"" + DataType + "\"; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }
                                else
                                {
                                    string PrepDataValue = "try { dr[\"" + ColumnId + "\"] = mst[\"" + ColumnId + "\"]; } catch { }" + Environment.NewLine
                                                + "drType[\"" + ColumnId + "\"] = string.Empty; drDisp[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                                    PrepDataResults.Add(PrepDataValue);
                                }

                            }

                        }

                        if (!string.IsNullOrEmpty(DefaultValue))
                        {
                            if (DefaultValue.Contains("RoboCoder.WebControls"))
                            {
                                string InitMasterValue = "{\"" + ColumnId + "\",\"\"},";
                                InitMasterResults.Add(InitMasterValue);
                            }
                            else
                            {
                                if (DisplayMode == "ImageButton" || DisplayMode == "CheckBox") //---------ImageButton or CheckBox
                                {
                                    string InitMasterValue = "{\"" + ColumnId + "\",\"" + DefaultValue.Replace('"', '\"') + "\"},";
                                    InitMasterResults.Add(InitMasterValue);
                                }
                                else if (DisplayMode.Contains("Date")) //---------Date (Any type)
                                {
                                    string InitMasterValue = "{\"" + ColumnId + "\", convertDefaultValue(" + DefaultValue.Replace('"', '\"') + ")},";
                                    InitMasterResults.Add(InitMasterValue);
                                }
                                else if (DisplayMode == "Label" || DisplayMode == "Action Button" || DisplayMode == "ImageButton" ||
                                         DisplayMode == "DataGridLink" || DisplayMode == "PlaceHolder") //---------Label / Action Button / ImageButton / DataGridLink / PlaceHolder
                                {
                                    string InitMasterValue = "";
                                    InitMasterResults.Add(InitMasterValue);
                                }
                                else
                                {
                                    if (DefaultValue.Contains("base."))
                                    {
                                        string InitMasterValue = "{\"" + ColumnId + "\"," + DefaultValue + "},";
                                        InitMasterResults.Add(InitMasterValue);
                                    }
                                    else
                                    {
                                        string InitMasterValue = "{\"" + ColumnId + "\",\"" + DefaultValue.Replace('"', '\"') + "\"},";
                                        InitMasterResults.Add(InitMasterValue);
                                    }


                                }
                            }
                        }
                        else
                        {
                            string InitMasterValue = "{\"" + ColumnId + "\",\"\"},";
                            InitMasterResults.Add(InitMasterValue);
                        }
                    }
                }
                else
                {
                    if (drv["DefAlways"].ToString() == "N" || (drv["DefAlways"].ToString() == "Y" && string.IsNullOrEmpty(drv["SystemValue"].ToString())))
                    {
                        string MakeTypRowValue = "dr[\"" + ColumnId + "\"] = System.Data.OleDb.OleDbType." + DataType + ".ToString();";
                        MakeTypRowResults.Add(MakeTypRowValue);

                        string MakeDisRowValue = "dr[\"" + ColumnId + "\"] = \"" + DisplayMode + "\";";
                        MakeDisRowResults.Add(MakeDisRowValue);

                        if (DisplayMode == "TextBox") //---------Textbox
                        {
                            string MakeColRowValue = "dr[\"" + ColumnId + "\"] = (drv[\"" + ColumnId + "\"] ?? \"\").ToString().Trim().Left(" + ColumnLength + ");";
                            MakeColRowResults.Add(MakeColRowValue);
                        }
                        else if (DisplayMode == "ImageButton") //---------ImageButton
                        {
                            string MakeColRowValue = "dr[\"" + ColumnId + "\"] = drv[\"" + ColumnId + "\"]; if (bAdd && dtAuth.Rows[" + ii + "][\"ColReadOnly\"].ToString() == \"Y\" && dr[\"" + ColumnId + "\"].ToString() == string.Empty) { dr[\"" + ColumnId + "\"] = System.DBNull.Value; }";
                            MakeColRowResults.Add(MakeColRowValue);
                        }
                        else if (DisplayMode == "Action Button" || DisplayMode == "ImageButton" ||
                                  DisplayMode == "DataGridLink" || DisplayMode == "PlaceHolder") //---------Action Button / ImageButton / DataGridLink / PlaceHolder
                        {
                            string MakeColRowValue = "";
                            MakeColRowResults.Add(MakeColRowValue);
                        }
                        else
                        {
                            string MakeColRowValue = "dr[\"" + ColumnId + "\"] = drv[\"" + ColumnId + "\"];";
                            MakeColRowResults.Add(MakeColRowValue);
                        }

                        string InitDtlValue = "{\"" + ColumnId + "\",\"" + DefaultValue + "\"},";
                        InitDtlResults.Add(InitDtlValue);
                    }

                }

                ii++;
            }

            string MakeTypRowCnt = string.Join(Environment.NewLine, MakeTypRowResults);
            string MakeDisRowCnt = string.Join(Environment.NewLine, MakeDisRowResults);
            string MakeColRowCnt = string.Join(Environment.NewLine, MakeColRowResults);
            string PrepDataCnt = string.Join(Environment.NewLine, PrepDataResults);
            string InitMasterCnt = string.Join(Environment.NewLine, InitMasterResults);
            string InitDtlCnt = string.Join(Environment.NewLine, InitDtlResults);

            StringBuilder sb = new StringBuilder();
            sb.Append("private DataRow MakeTypRow(DataRow dr){");
            sb.Append("dr[\"" + screenPrimaryKey + "\"] = System.Data.OleDb.OleDbType." + screenPrimaryKeyType + ".ToString();");
            sb.Append(MakeTypRowCnt + Environment.NewLine);
            sb.Append(@"
                    return dr;
                }

                private DataRow MakeDisRow(DataRow dr){
            ");
            sb.Append("dr[\"" + screenPrimaryKey + "\"] = \"" + screenPrimaryKeyDis + "\";");
            sb.Append(MakeDisRowCnt + Environment.NewLine);
            sb.Append(@"
                    return dr;
                }

                private DataRow MakeColRow(DataRow dr, SerializableDictionary<string, string> drv, string keyId, bool bAdd){
            ");
            sb.Append("dr[\"" + screenPrimaryKey + "\"] = keyId;");
            sb.Append(@"
                    DataTable dtAuth = _GetAuthCol(screenId);
                    if (dtAuth != null)
                    {
            ");
            sb.Append(MakeColRowCnt + Environment.NewLine);
            sb.Append(@"
                    }
                    return dr;
                }

                private [[---ScreenDef---]] Prep[[---ScreenName---]]Data(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, bool bAdd)
                {
                    [[---ScreenDef---]] ds = new [[---ScreenDef---]]();
                    DataRow dr = ds.Tables[""[[---ScreenName---]]""].NewRow();
                    DataRow drType = ds.Tables[""[[---ScreenName---]]""].NewRow();
                    DataRow drDisp = ds.Tables[""[[---ScreenName---]]""].NewRow();
            ");
            sb.Append(PrepDataCnt + Environment.NewLine);
            sb.Append(@"
                    if (dtl != null)
                    {
                        ds.Tables[""[[---ScreenName---]]Def""].Rows.Add(MakeTypRow(ds.Tables[""[[---ScreenName---]]Def""].NewRow()));
                        ds.Tables[""[[---ScreenName---]]Def""].Rows.Add(MakeDisRow(ds.Tables[""[[---ScreenName---]]Def""].NewRow()));
                        if (bAdd)
                        {
                            foreach (var drv in dtl)
                            {
                                ds.Tables[""[[---ScreenName---]]Add""].Rows.Add(MakeColRow(ds.Tables[""[[---ScreenName---]]Add""].NewRow(), drv, mst[""[[---ScreenPrimaryKey---]]""], true));
                            }
                        }
                        else
                        {
                            var dtlUpd = from r in dtl where !string.IsNullOrEmpty((r[""[[---screenDetailKey---]]""] ?? """").ToString()) && (r.ContainsKey(""_mode"") ? r[""_mode""] : """") != ""delete"" select r;
                            foreach (var drv in dtlUpd)
                            {
                                ds.Tables[""[[---ScreenName---]]Upd""].Rows.Add(MakeColRow(ds.Tables[""[[---ScreenName---]]Upd""].NewRow(), drv, mst[""[[---ScreenPrimaryKey---]]""], false));
                            }
                            var dtlAdd = from r in dtl.AsEnumerable() where string.IsNullOrEmpty(r[""[[---screenDetailKey---]]""]) select r;
                            foreach (var drv in dtlAdd)
                            {
                                ds.Tables[""[[---ScreenName---]]Add""].Rows.Add(MakeColRow(ds.Tables[""[[---ScreenName---]]Add""].NewRow(), drv, mst[""[[---ScreenPrimaryKey---]]""], true));
                            }
                            var dtlDel = from r in dtl.AsEnumerable() where !string.IsNullOrEmpty((r[""[[---screenDetailKey---]]""] ?? """").ToString()) && (r.ContainsKey(""_mode"") ? r[""_mode""] : """") == ""delete"" select r;
                            foreach (var drv in dtlDel)
                            {
                                ds.Tables[""[[---ScreenName---]]Del""].Rows.Add(MakeColRow(ds.Tables[""[[---ScreenName---]]Del""].NewRow(), drv, mst[""[[---ScreenPrimaryKey---]]""], false));
                            }
                        }
                    }
                    ds.Tables[""[[---ScreenName---]]""].Rows.Add(dr); ds.Tables[""[[---ScreenName---]]""].Rows.Add(drType); ds.Tables[""[[---ScreenName---]]""].Rows.Add(drDisp);
                    return ds;
                }

                protected override SerializableDictionary<string, string> InitMaster()
                {
                    var mst = new SerializableDictionary<string, string>(){
            ");
            sb.Append(InitMasterCnt + Environment.NewLine);
            sb.Append(@"
                    };
                    /* AsmxRule: Init Master Table */
            ");
            sb.Append(InitMasterTableCnt + Environment.NewLine);
            sb.Append(@"
                    /* AsmxRule End: Init Master Table */

                    return mst;
                }

                protected override SerializableDictionary<string, string> InitDtl()
                {
                    var mst = new SerializableDictionary<string, string>(){
            ");
            sb.Append(InitDtlCnt + Environment.NewLine);
            sb.Append(@"
                    };
                    /* AsmxRule: Init Detail Table */
            ");
            sb.Append(InitDetailTableCnt + Environment.NewLine);
            sb.Append(@"
                    /* AsmxRule End: Init Detail Table */
                    return mst;
                }
            ");
            return sb;
        }

        private StringBuilder GetScreenCRUD(string screenId, string screenName, string screenPrimaryKey, string screenPrimaryKeyType, string screenPrimaryKeyDis, DataView dvItms, DataView dvAsmxRule)
        {
            List<string> GetDtlByIdResults = new List<string>();
            List<string> CurrentDataResults = new List<string>();
            List<string> DtDtlResults = new List<string>();
            List<string> DtlImgUploadResults = new List<string>();
            List<string> DtlResults = new List<string>();
            List<string> AsmxSaveDataBeforeResults = new List<string>();
            List<string> AsmxSaveDataAfterResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;

            string screenTypeName = dvItms[0]["ScreenTypeName"].ToString();

            foreach (DataRowView drv in dvAsmxRule)
            {
                if (drv["AsmxEventId"].ToString() == "3") //Save Data Before
                {
                    string AsmxSaveDataBeforeValue = drv["AsmxRuleProg"].ToString();
                    AsmxSaveDataBeforeResults.Add(AsmxSaveDataBeforeValue);
                }
                else if (drv["AsmxEventId"].ToString() == "4") //Save Data After
                {
                    string AsmxSaveDataAfterValue = drv["AsmxRuleProg"].ToString();
                    AsmxSaveDataAfterResults.Add(AsmxSaveDataAfterValue);
                }
            }

            if ("I1".IndexOf(screenTypeName) >= 0)  /*Tab Folder Only Screen*/
            {
                string GetDtlByIdValue = "ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();" + Environment.NewLine
                                       + "mr.data = new List<SerializableDictionary<string,string>>();";
                GetDtlByIdResults.Add(GetDtlByIdValue);

                string CurrentDataValue = "DataTable dtDtl = null;";
                CurrentDataResults.Add(CurrentDataValue);

            }
            else if ("I2".IndexOf(screenTypeName) >= 0) /*Tab Folder with Grid Screen*/
            {
                string GetDtlByIdValue = "ValidatedMstId(\"GetLis[[---ScreenDef---]]\", systemId, screenId, \"**\" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));" + Environment.NewLine
                                        + "DataTable dtColAuth = _GetAuthCol(GetScreenId());" + Environment.NewLine
                                        + "Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr[\"ColName\"].ToString());" + Environment.NewLine
                                        + "ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();" + Environment.NewLine
                                        + "DataTable dt = (new RO.Access3.AdminAccess()).GetDtlById([[---ScreenId---]], \"Get[[---ScreenDef---]]DtlById\", keyId, LcAppConnString, LcAppPw, filterId, base.LImpr, base.LCurr);" + Environment.NewLine
                                        + "mr.data = DataTableToListOfObject(dt, false, colAuth);";
                GetDtlByIdResults.Add(GetDtlByIdValue);

                string CurrentDataValue = "DataTable dtDtl = _GetDtlById(pid, 0);";
                CurrentDataResults.Add(CurrentDataValue);

                string DtDtlValue = "dtDtl = _GetDtlById(pid, 0);";
                DtDtlResults.Add(DtDtlValue);

                foreach (DataRowView drv in dvItms)
                {
                    if (drv["DisplayMode"].ToString() == "ImageButton" && !string.IsNullOrEmpty(drv["ColumnId"].ToString())) //---------ImageButton
                    {
                        string ColumnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                        string ColumnName = drv["ColumnName"].ToString();
                        string DtlImgUploadValue = "if (!string.IsNullOrEmpty(x[\"" + ColumnId + "\"]))" + Environment.NewLine
                            + "{" + Environment.NewLine
                                + "foreach (DataRow dr in dtDtl.Rows)" + Environment.NewLine
                                + "{" + Environment.NewLine
                                    + "/* use primary key or heuristic that new dtl has larger max dtlId before save(which is bad but assuming there is only 1 detail add, it would be fine. should be done in the UpdData call during add/update FIXME */" + Environment.NewLine
                                    + "if ((!string.IsNullOrEmpty(x[\"[[---screenDetailKey---]]\"]) && dr[\"[[---screenDetailKey---]]\"].ToString() == x[\"[[---screenDetailKey---]]\"])" + Environment.NewLine
                                        + "||" + Environment.NewLine
                                        + "(string.IsNullOrEmpty(x[\"[[---screenDetailKey---]]\"]) && int.Parse(dr[\"[[---screenDetailKey---]]\"].ToString()) > maxDtlId)" + Environment.NewLine
                                        + ")" + Environment.NewLine
                                    + "{AddDoc(x[\"" + ColumnId + "\"], dr[\"[[---screenDetailKey---]]\"].ToString(), \"dbo.[[---screenDetailTableName---]]\", \"[[---screenDetailKeyName---]]\", \"" + ColumnName + "\", options.ContainsKey(\"resizeImage\"));}" + Environment.NewLine
                                + "}" + Environment.NewLine
                            + "}";
                        DtlImgUploadResults.Add(DtlImgUploadValue);
                    }
                }

                string DtlValue = "result.dtl = DataTableToListOfObject(dtDtl, false, colAuth);";
                DtlResults.Add(DtlValue);
            }
            else
            {

            }

            string GetDtlByIdCnt = string.Join(Environment.NewLine, GetDtlByIdResults.Select(s => addIndent(s, 24)));
            string CurrentDataCnt = string.Join(Environment.NewLine, CurrentDataResults.Select(s => addIndent(s, 24)));
            string DtDtlCnt = string.Join(Environment.NewLine, DtDtlResults.Select(s => addIndent(s, 24)));
            string DtlImgUploadCnt = string.Join(Environment.NewLine, DtlImgUploadResults.Select(s => addIndent(s, 24)));
            string DtlCnt = string.Join(Environment.NewLine, DtlResults.Select(s => addIndent(s, 24)));
            string AsmxSaveDataBeforeCnt = string.Join(Environment.NewLine, AsmxSaveDataBeforeResults.Select(s => addIndent(s, 24)));
            string AsmxSaveDataAfterCnt = string.Join(Environment.NewLine, AsmxSaveDataAfterResults.Select(s => addIndent(s, 24)));

            StringBuilder sb = new StringBuilder();

            sb.Append(@"
            [WebMethod(EnableSession = false)]
            public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> Get[[---ScreenDef---]]List(string searchStr, int topN, string filterId)
            {
                Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, string> context = new Dictionary<string, string>();
                    context[""method""] = ""GetLis[[---ScreenDef---]]"";
                    context[""mKey""] = ""[[---ScreenPrimaryKey---]]"";
                    context[""mVal""] = ""[[---ScreenPrimaryKey---]]Text"";
                    context[""mTip""] = ""[[---ScreenPrimaryKey---]]Text"";
                    context[""mImg""] = ""[[---ScreenPrimaryKey---]]Text"";
                    context[""ssd""] = ""1"";
                    context[""scr""] = screenId.ToString();
                    context[""csy""] = systemId.ToString();
                    context[""filter""] = filterId;
                    context[""isSys""] = ""N"";
                    context[""conn""] = string.Empty;
                    AutoCompleteResponse r = LisSuggests(searchStr, jss.Serialize(context), topN);
                    ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.errorMsg = """";
                    mr.data = r;
                    mr.status = ""success"";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""R"", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> Get[[---ScreenDef---]]ById(string keyId, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey(""currentScreenCriteria"") ? options[""currentScreenCriteria""] : null;
                    ValidatedMstId(""GetLis[[---ScreenDef---]]"", systemId, screenId, ""**"" + keyId, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    DataTable dt = _GetMstById(keyId);
                    DataTable dtColAuth = _GetAuthCol(GetScreenId());
                    Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr[""ColName""].ToString());
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = DataTableToListOfObject(dt, false, colAuth);
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""R"", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> Get[[---ScreenDef---]]DtlById(string keyId, SerializableDictionary<string, string> options, int filterId)
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    string jsonCri = options.ContainsKey(""currentScreenCriteria"") ? options[""currentScreenCriteria""] : null;            
");
            sb.Append(GetDtlByIdCnt);
            sb.Append(@"
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""R"", null));
                return ret;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewMst()
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var mst = InitMaster();
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = new List<SerializableDictionary<string, string>>() { mst };
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""R"", null));
                return ret;
            }
            [WebMethod(EnableSession = false)]
            public ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> GetNewDtl()
            {
                Func<ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var dtl = InitDtl();
                    ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<List<SerializableDictionary<string, string>>, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.data = new List<SerializableDictionary<string, string>>() { dtl };
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""R"", null));
                return ret;
            }

            protected override DataTable _GetMstById(string mstId)
            {
                return (new RO.Access3.AdminAccess()).GetMstById(""Get[[---ScreenDef---]]ById"", string.IsNullOrEmpty(mstId) ? ""-1"" : mstId, LcAppConnString, LcAppPw);

            }
            protected override DataTable _GetDtlById(string mstId, int screenFilterId)
            {
                return (new RO.Access3.AdminAccess()).GetDtlById(screenId, ""Get[[---ScreenDef---]]DtlById"", string.IsNullOrEmpty(mstId) ? ""-1"" : mstId, LcAppConnString, LcAppPw, screenFilterId, LImpr, LCurr);

            }
            protected override Dictionary<string, SerializableDictionary<string, string>> GetDdlContext()
            {
                return ddlContext;
            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> DelMst(SerializableDictionary<string, string> mst, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var pid = mst[""[[---ScreenPrimaryKey---]]""];
                    var ds = Prep[[---ScreenName---]]Data(mst, new List<SerializableDictionary<string, string>>(), string.IsNullOrEmpty(mst[""[[---ScreenPrimaryKey---]]""]));
                    (new RO.Access3.AdminAccess()).DelData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                    ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    SaveDataResponse result = new SaveDataResponse();
                    string msg = _GetScreenHlp(screenId).Rows[0][""DelMsg""].ToString();
                    result.message = msg;
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""D"", null));
                return ret;

            }

            [WebMethod(EnableSession = false)]
            public ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> SaveData(SerializableDictionary<string, string> mst, List<SerializableDictionary<string, string>> dtl, SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    //throw new Exception(""aaa"");
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
                    SerializableDictionary<string, string> skipValidation = new SerializableDictionary<string, string>(){ { ""SkipAllMst"", ""SilentColReadOnly"" }, { ""SkipAllDtl"", ""SilentColReadOnly"" } };
                    /* AsmxRule: Save Data Before */
");
            sb.Append(AsmxSaveDataBeforeCnt + Environment.NewLine);
            sb.Append(@"
                    /* AsmxRule End: Save Data Before */

                    var pid = mst[""[[---ScreenPrimaryKey---]]""];
                    if (!string.IsNullOrEmpty(pid))
                    {
                        string jsonCri = options.ContainsKey(""currentScreenCriteria"") ? options[""currentScreenCriteria""] : null;
                        ValidatedMstId(""GetLis[[---ScreenDef---]]"", systemId, screenId, ""**"" + pid, MatchScreenCriteria(_GetScrCriteria(screenId).DefaultView, jsonCri));
                    }

                    /* current data */
                    DataTable dtMst = _GetMstById(pid);
");
            sb.Append(CurrentDataCnt);
            sb.Append(@" 
                    int maxDtlId = dtDtl == null ? -1 : dtDtl.AsEnumerable().Select(dr => dr[""[[---screenDetailKey---]]""].ToString()).Where((s) => !string.IsNullOrEmpty(s)).Select(id => int.Parse(id)).DefaultIfEmpty(-1).Max();
                    var validationResult = ValidateInput(ref mst, ref dtl, dtMst, dtDtl, ""[[---ScreenPrimaryKey---]]"", ""[[---screenDetailKey---]]"", skipValidation);
                    if (validationResult.Item1.Count > 0 || validationResult.Item2.Count > 0)
                    {
                        return new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>()
                        {
                            status = ""failed"",
                            errorMsg = ""content invalid "" + string.Join("" "", (validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0]).ToArray()),
                            validationErrors = validationResult.Item1.Count > 0 ? validationResult.Item1 : validationResult.Item2[0],
                        };
                    }
                    var ds = Prep[[---ScreenName---]]Data(mst, dtl, string.IsNullOrEmpty(mst[""[[---ScreenPrimaryKey---]]""]));
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(mst[""[[---ScreenPrimaryKey---]]""]))
                    {
                        pid = (new RO.Access3.AdminAccess()).AddData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                        if (!string.IsNullOrEmpty(pid))
                        {
                            msg = _GetScreenHlp(screenId).Rows[0][""AddMsg""].ToString();
                        }
                    }
                    else
                    {
                        bool ok = (new RO.Access3.AdminAccess()).UpdData(screenId, false, base.LUser, base.LImpr, base.LCurr, ds, LcAppConnString, LcAppPw, base.CPrj, base.CSrc);

                        if (ok)
                        {
                            msg = _GetScreenHlp(screenId).Rows[0][""UpdMsg""].ToString();
                        }
                    }

                    /* read updated records */
                    dtMst = _GetMstById(pid);
");
            sb.Append(DtDtlCnt);
            sb.Append(@"
                     foreach (var x in dtl){
                        ");
            sb.Append(DtlImgUploadCnt);
            sb.Append(@"
                     }
                     /* AsmxRule: Save Data After */
");
            sb.Append(AsmxSaveDataAfterCnt + Environment.NewLine);
            sb.Append(@"
                    /* AsmxRule End: Save Data After */

                    ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<SaveDataResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    SaveDataResponse result = new SaveDataResponse();
                    DataTable dtColAuth = _GetAuthCol(GetScreenId());
                    Dictionary<string, DataRow> colAuth = dtColAuth.AsEnumerable().ToDictionary(dr => dr[""ColName""].ToString());

                    result.mst = DataTableToListOfObject(dtMst, false, colAuth)[0];
");
            sb.Append(DtlCnt);
            sb.Append(@"
                    
                    result.message = msg;
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""S"", null));
                return ret;
            }
            ");

            return sb;
        }

        private StringBuilder GetScreenDdlFunctions(string screenId, string screenName, string screenPrimaryKey, string screenPrimaryKeyType, string screenPrimaryKeyDis, DataView dvItms)
        {
            List<string> GetScreenDdlFunctionsResults = new List<string>();
            List<string> GetDdlListResults = new List<string>();
            Func<string, int, string> addIndent = (s, c) => new String(' ', c) + s;
            foreach (DataRowView drv in dvItms)
            {
                string columnId = drv["ColumnName"].ToString() + drv["TableId"].ToString();
                string ColumnName = drv["ColumnName"].ToString();
                string DisplayMode = drv["DisplayMode"].ToString();
                string SPName = "GetDdl" + ColumnName + DbId + "S" + drv["ScreenObjId"].ToString();
                string DdlFtrColumnName = drv["DdlFtrColumnName"].ToString();
                string DdlFtrDataType = drv["DdlFtrDataType"].ToString();
                string DdlAdnColumnName = drv["DdlAdnColumnName"].ToString();
                if (DisplayMode == "AutoComplete")
                {
                    string GetScreenDdlFunctionsValue = "[WebMethod(EnableSession = false)]"
                                                      + "public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> Get" + columnId + "List(string query, int topN, string filterBy)"
                                                      + "{"
                                                      + "Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>"
                                                      + "{"
                                                      + "SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);"
                                                      + "System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();"
                                                      + "context[\"method\"] = \"" + SPName + "\";"
                                                      + "context[\"addnew\"] = \"Y\";"
                                                      + "context[\"mKey\"] = \"" + columnId + "\";"
                                                      + "context[\"mVal\"] = \"" + columnId + "Text\";"
                                                      + "context[\"mTip\"] = \"" + columnId + "Text\";"
                                                      + "context[\"mImg\"] = \"" + columnId + "Text\";"
                                                      + "context[\"ssd\"] = \"\";"
                                                      + "context[\"scr\"] = screenId.ToString();"
                                                      + "context[\"csy\"] = systemId.ToString();"
                                                      + "context[\"filter\"] = \"0\";"
                                                      + "context[\"isSys\"] = \"N\";"
                                                      + "context[\"conn\"] = string.Empty;"
                                                      + (string.IsNullOrEmpty(DdlFtrColumnName)
                                                        ? ""
                                                        : ("context[\"refCol\"] = \"" + DdlAdnColumnName + "\";"
                                                            + "context[\"refColDataType\"] = \"" + DdlFtrDataType + "\";"
                                                            + "context[\"refColVal\"] = filterBy;")
                                                        )
                                                      + "ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>();"
                                                      + "mr.status = \"success\";"
                                                      + "mr.errorMsg = \"\";"
                                                      + "mr.data = ddlSuggests(query, context, topN);"
                                                      + "return mr;"
                                                      + "};"
                                                      + "var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, \"R\", \"" + columnId + "\", emptyAutoCompleteResponse));"
                                                      + "return ret;"
                                                      + "}";

                    GetScreenDdlFunctionsResults.Add(GetScreenDdlFunctionsValue);

                    string GetDdlListValue = "var " + columnId + "LIst = Get" + columnId + "List(\"\", 0, \"\");";
                    GetDdlListResults.Add(GetDdlListValue);
                }
                else if (DisplayMode == "DropDownList" || drv["DisplayMode"].ToString() == "RadioButtonList" || drv["DisplayMode"].ToString() == "ListBox" || drv["DisplayMode"].ToString() == "WorkflowStatus" || drv["DisplayMode"].ToString() == "AutoListBox" || drv["DisplayMode"].ToString() == "DataGridLink")
                {
                    string GetScreenDdlFunctionsValue = "[WebMethod(EnableSession = false)]"
                                                      + "public ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>> Get" + columnId + "List(string query, int topN, string filterBy)"
                                                      + "{"
                                                      + "Func<ApiResponse<AutoCompleteResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>"
                                                      + "{"
                                                      + "SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);"
                                                      + "bool bAll = !query.StartsWith(\"**\");"
                                                      + "bool bAddNew = !query.StartsWith(\"**\");"
                                                      + "string keyId = query.Replace(\"**\", \"\");"
                                                      + "DataTable dt = (new RO.Access3.AdminAccess()).GetDdl(screenId, \"" + SPName + "\", bAddNew, bAll, 0, keyId, LcAppConnString, LcAppPw, string.Empty, base.LImpr, base.LCurr);"
                                                      + "return DataTableToApiResponse(dt, \"\", 0);"
                                                      + "};"
                                                      + "var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, \"R\", \"" + columnId + "\", emptyAutoCompleteResponse));"
                                                      + "return ret;"
                                                      + "}";

                    GetScreenDdlFunctionsResults.Add(GetScreenDdlFunctionsValue);

                    string GetDdlListValue = "var " + columnId + "LIst = Get" + columnId + "List(\"\", 0, \"\");";
                    GetDdlListResults.Add(GetDdlListValue);
                }
            }

            string GetScreenDdlFunctionsCnt = string.Join(Environment.NewLine, GetScreenDdlFunctionsResults.Select(s => addIndent(s, 24)));
            string GetDdlListCnt = string.Join(Environment.NewLine, GetDdlListResults.Select(s => addIndent(s, 24)));


            StringBuilder sb = new StringBuilder();

            sb.Append(GetScreenDdlFunctionsCnt);
            sb.Append(@"
            [WebMethod(EnableSession = false)]
            public ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> LoadInitPage(SerializableDictionary<string, string> options)
            {
                Func<ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>> fn = () =>
                {
                    SwitchContext(systemId, LCurr.CompanyId, LCurr.ProjectId);
                    var dtAuthCol = _GetAuthCol(screenId);
                    var dtAuthRow = _GetAuthRow(screenId);
                    var dtScreenLabel = _GetScreenLabel(screenId);
                    var dtScreenCriteria = _GetScrCriteria(screenId);
                    var dtScreenFilter = _GetScreenFilter(screenId);
                    var dtScreenHlp = _GetScreenHlp(screenId);
                    var dtScreenButtonHlp = _GetScreenButtonHlp(screenId);
                    var dtLabel = _GetLabels(""[[---ScreenName---]]"");
                    var SearchList = Get[[---ScreenDef---]]List("""", 0, """");
            ");
            sb.Append(GetDdlListCnt);
            sb.Append(@"

                    LoadScreenPageResponse result = new LoadScreenPageResponse();

                    ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>> mr = new ApiResponse<LoadScreenPageResponse, SerializableDictionary<string, AutoCompleteResponse>>();
                    mr.status = ""success"";
                    mr.errorMsg = """";
                    mr.data = result;
                    return mr;
                };
                var ret = ProtectedCall(RestrictedApiCall(fn, systemId, screenId, ""S"", null));
                return ret;
            }           
            ");

            return sb;
        }
        //End Asmx Generation

    }
}