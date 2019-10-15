
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
import AdmScreenReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScreen';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreen || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ScreenId15';
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

MasterTableId15InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchMasterTableId15(v, filterBy);}}
SearchTableId15InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchSearchTableId15(v, filterBy);}}
SearchId15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchId15(v, filterBy);}}
SearchIdR15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchIdR15(v, filterBy);}}
SearchDtlId15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchDtlId15(v, filterBy);}}
SearchDtlIdR15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchDtlIdR15(v, filterBy);}}
SearchUrlId15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchUrlId15(v, filterBy);}}
SearchImgId15InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreen || {}).Mst || {}).SearchTableId15; _this.props.SearchSearchImgId15(v, filterBy);}}
DetailTableId15InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDetailTableId15(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmScreen || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cProgramName15) { errors.cProgramName15 = (columnLabel.ProgramName15 || {}).ErrMessage;}
if (isEmptyId((values.cScreenTypeId15 || {}).value)) { errors.cScreenTypeId15 = (columnLabel.ScreenTypeId15 || {}).ErrMessage;}
if (isEmptyId((values.cViewOnly15 || {}).value)) { errors.cViewOnly15 = (columnLabel.ViewOnly15 || {}).ErrMessage;}
if (isEmptyId((values.cMasterTableId15 || {}).value)) { errors.cMasterTableId15 = (columnLabel.MasterTableId15 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmScreen || {}).Mst || {};
/* ReactRule: Master Record Save */
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
        this.props.AdmScreen,
        {
          ScreenId15: values.cScreenId15|| '',
          ProgramName15: values.cProgramName15|| '',
          ScreenTypeId15: (values.cScreenTypeId15|| {}).value || '',
          ViewOnly15: (values.cViewOnly15|| {}).value || '',
          SearchAscending15: values.cSearchAscending15 ? 'Y' : 'N',
          MasterTableId15: (values.cMasterTableId15|| {}).value || '',
          SearchTableId15: (values.cSearchTableId15|| {}).value || '',
          SearchId15: (values.cSearchId15|| {}).value || '',
          SearchIdR15: (values.cSearchIdR15|| {}).value || '',
          SearchDtlId15: (values.cSearchDtlId15|| {}).value || '',
          SearchDtlIdR15: (values.cSearchDtlIdR15|| {}).value || '',
          SearchUrlId15: (values.cSearchUrlId15|| {}).value || '',
          SearchImgId15: (values.cSearchImgId15|| {}).value || '',
          DetailTableId15: (values.cDetailTableId15|| {}).value || '',
          GridRows15: values.cGridRows15|| '',
          HasDeleteAll15: values.cHasDeleteAll15 ? 'Y' : 'N',
          ShowGridHead15: values.cShowGridHead15 ? 'Y' : 'N',
          GenerateSc15: values.cGenerateSc15 ? 'Y' : 'N',
          GenerateSr15: values.cGenerateSr15 ? 'Y' : 'N',
          ValidateReq15: values.cValidateReq15 ? 'Y' : 'N',
          DeferError15: values.cDeferError15 ? 'Y' : 'N',
          AuthRequired15: values.cAuthRequired15 ? 'Y' : 'N',
          GenAudit15: values.cGenAudit15 ? 'Y' : 'N',
          ScreenObj15: values.cScreenObj15|| '',
          ScreenFilter: values.cScreenFilter|| '',
          MoreInfo: values.cMoreInfo|| '',
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
    const AdmScreenState = this.props.AdmScreen || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ScreenId15;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmScreen.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmScreen.Label);
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
    const AdmScreenState = this.props.AdmScreen || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ScreenId15;
        this.props.DelMst(this.props.AdmScreen, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmScreen || {};
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
    const AdmScreenState = this.props.AdmScreen || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
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
      if (!(this.props.AdmScreen || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScreen || {};

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
    const AdmScreenState = this.props.AdmScreen || {};

    if (AdmScreenState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmScreenState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmScreenReduxObj.GetScreenButtons(AdmScreenState) || {};
    const itemList = AdmScreenState.Dtl || [];
    const auxLabels = AdmScreenState.Label || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};

    const columnLabel = AdmScreenState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmScreenState);
    const authRow = (AdmScreenState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmScreen || {}).Mst || {});
    const currDtl = ((this.props.AdmScreen || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ScreenId15));
    const selectList = AdmScreenReduxObj.SearchListToSelectList(AdmScreenState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ScreenId15 = currMst.ScreenId15;
const ProgramName15 = currMst.ProgramName15;
const ScreenTypeId15List = AdmScreenReduxObj.ScreenDdlSelectors.ScreenTypeId15(AdmScreenState);
const ScreenTypeId15 = currMst.ScreenTypeId15;
const ViewOnly15List = AdmScreenReduxObj.ScreenDdlSelectors.ViewOnly15(AdmScreenState);
const ViewOnly15 = currMst.ViewOnly15;
const SearchAscending15 = currMst.SearchAscending15;
const MasterTableId15List = AdmScreenReduxObj.ScreenDdlSelectors.MasterTableId15(AdmScreenState);
const MasterTableId15 = currMst.MasterTableId15;
const SearchTableId15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchTableId15(AdmScreenState);
const SearchTableId15 = currMst.SearchTableId15;
const SearchId15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchId15(AdmScreenState);
const SearchId15 = currMst.SearchId15;
const SearchIdR15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchIdR15(AdmScreenState);
const SearchIdR15 = currMst.SearchIdR15;
const SearchDtlId15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchDtlId15(AdmScreenState);
const SearchDtlId15 = currMst.SearchDtlId15;
const SearchDtlIdR15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchDtlIdR15(AdmScreenState);
const SearchDtlIdR15 = currMst.SearchDtlIdR15;
const SearchUrlId15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchUrlId15(AdmScreenState);
const SearchUrlId15 = currMst.SearchUrlId15;
const SearchImgId15List = AdmScreenReduxObj.ScreenDdlSelectors.SearchImgId15(AdmScreenState);
const SearchImgId15 = currMst.SearchImgId15;
const DetailTableId15List = AdmScreenReduxObj.ScreenDdlSelectors.DetailTableId15(AdmScreenState);
const DetailTableId15 = currMst.DetailTableId15;
const GridRows15 = currMst.GridRows15;
const HasDeleteAll15 = currMst.HasDeleteAll15;
const ShowGridHead15 = currMst.ShowGridHead15;
const GenerateSc15 = currMst.GenerateSc15;
const GenerateSr15 = currMst.GenerateSr15;
const ValidateReq15 = currMst.ValidateReq15;
const DeferError15 = currMst.DeferError15;
const AuthRequired15 = currMst.AuthRequired15;
const GenAudit15 = currMst.GenAudit15;
const ScreenObj15 = currMst.ScreenObj15;
const ScreenFilter = currMst.ScreenFilter;
const MoreInfo = currMst.MoreInfo;

    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
/* ReactRule: Master Render */
/* ReactRule End: Master Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmScreenState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cScreenId15: ScreenId15 || '',
                  cProgramName15: ProgramName15 || '',
                  cScreenTypeId15: ScreenTypeId15List.filter(obj => { return obj.key === ScreenTypeId15 })[0],
                  cViewOnly15: ViewOnly15List.filter(obj => { return obj.key === ViewOnly15 })[0],
                  cSearchAscending15: SearchAscending15 === 'Y',
                  cMasterTableId15: MasterTableId15List.filter(obj => { return obj.key === MasterTableId15 })[0],
                  cSearchTableId15: SearchTableId15List.filter(obj => { return obj.key === SearchTableId15 })[0],
                  cSearchId15: SearchId15List.filter(obj => { return obj.key === SearchId15 })[0],
                  cSearchIdR15: SearchIdR15List.filter(obj => { return obj.key === SearchIdR15 })[0],
                  cSearchDtlId15: SearchDtlId15List.filter(obj => { return obj.key === SearchDtlId15 })[0],
                  cSearchDtlIdR15: SearchDtlIdR15List.filter(obj => { return obj.key === SearchDtlIdR15 })[0],
                  cSearchUrlId15: SearchUrlId15List.filter(obj => { return obj.key === SearchUrlId15 })[0],
                  cSearchImgId15: SearchImgId15List.filter(obj => { return obj.key === SearchImgId15 })[0],
                  cDetailTableId15: DetailTableId15List.filter(obj => { return obj.key === DetailTableId15 })[0],
                  cGridRows15: GridRows15 || '',
                  cHasDeleteAll15: HasDeleteAll15 === 'Y',
                  cShowGridHead15: ShowGridHead15 === 'Y',
                  cGenerateSc15: GenerateSc15 === 'Y',
                  cGenerateSr15: GenerateSr15 === 'Y',
                  cValidateReq15: ValidateReq15 === 'Y',
                  cDeferError15: DeferError15 === 'Y',
                  cAuthRequired15: AuthRequired15 === 'Y',
                  cGenAudit15: GenAudit15 === 'Y',
                  cScreenObj15: ScreenObj15 || '',
                  cScreenFilter: ScreenFilter || '',
                  cMoreInfo: MoreInfo || '',
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
                                {(this.constructor.ShowSpinner(AdmScreenState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15)) return null;
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
            {(authCol.ScreenId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId15 || {}).ColumnHeader} {(columnLabel.ScreenId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenId15'
disabled = {(authCol.ScreenId15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenId15 && touched.cScreenId15 && <span className='form__form-group-error'>{errors.cScreenId15}</span>}
</div>
</Col>
}
{(authCol.ProgramName15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProgramName15 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ProgramName15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProgramName15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProgramName15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cProgramName15'
disabled = {(authCol.ProgramName15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProgramName15 && touched.cProgramName15 && <span className='form__form-group-error'>{errors.cProgramName15}</span>}
</div>
</Col>
}
{(authCol.ScreenTypeId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenTypeId15 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenTypeId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenTypeId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenTypeId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cScreenTypeId15'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cScreenTypeId15')}
value={values.cScreenTypeId15}
options={ScreenTypeId15List}
placeholder=''
disabled = {(authCol.ScreenTypeId15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenTypeId15 && touched.cScreenTypeId15 && <span className='form__form-group-error'>{errors.cScreenTypeId15}</span>}
</div>
</Col>
}
{(authCol.ViewOnly15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ViewOnly15 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ViewOnly15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ViewOnly15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ViewOnly15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cViewOnly15'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cViewOnly15')}
value={values.cViewOnly15}
options={ViewOnly15List}
placeholder=''
disabled = {(authCol.ViewOnly15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cViewOnly15 && touched.cViewOnly15 && <span className='form__form-group-error'>{errors.cViewOnly15}</span>}
</div>
</Col>
}
{(authCol.SearchAscending15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cSearchAscending15'
onChange={handleChange}
defaultChecked={values.cSearchAscending15}
disabled={(authCol.SearchAscending15 || {}).readonly || !(authCol.SearchAscending15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.SearchAscending15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.SearchAscending15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchAscending15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchAscending15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.MasterTableId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterTableId15 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterTableId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterTableId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterTableId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cMasterTableId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cMasterTableId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cMasterTableId15', true)}
onInputChange={this.MasterTableId15InputChange()}
value={values.cMasterTableId15}
defaultSelected={MasterTableId15List.filter(obj => { return obj.key === MasterTableId15 })}
options={MasterTableId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.MasterTableId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cMasterTableId15 && touched.cMasterTableId15 && <span className='form__form-group-error'>{errors.cMasterTableId15}</span>}
</div>
</Col>
}
{(authCol.SearchTableId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchTableId15 || {}).ColumnHeader} {(columnLabel.SearchTableId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchTableId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchTableId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchTableId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchTableId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchTableId15', true)}
onInputChange={this.SearchTableId15InputChange()}
value={values.cSearchTableId15}
defaultSelected={SearchTableId15List.filter(obj => { return obj.key === SearchTableId15 })}
options={SearchTableId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchTableId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchTableId15 && touched.cSearchTableId15 && <span className='form__form-group-error'>{errors.cSearchTableId15}</span>}
</div>
</Col>
}
{(authCol.SearchId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchId15 || {}).ColumnHeader} {(columnLabel.SearchId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchId15', true)}
onInputChange={this.SearchId15InputChange()}
value={values.cSearchId15}
defaultSelected={SearchId15List.filter(obj => { return obj.key === SearchId15 })}
options={SearchId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchId15 && touched.cSearchId15 && <span className='form__form-group-error'>{errors.cSearchId15}</span>}
</div>
</Col>
}
{(authCol.SearchIdR15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchIdR15 || {}).ColumnHeader} {(columnLabel.SearchIdR15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchIdR15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchIdR15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchIdR15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchIdR15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchIdR15', true)}
onInputChange={this.SearchIdR15InputChange()}
value={values.cSearchIdR15}
defaultSelected={SearchIdR15List.filter(obj => { return obj.key === SearchIdR15 })}
options={SearchIdR15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchIdR15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchIdR15 && touched.cSearchIdR15 && <span className='form__form-group-error'>{errors.cSearchIdR15}</span>}
</div>
</Col>
}
{(authCol.SearchDtlId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchDtlId15 || {}).ColumnHeader} {(columnLabel.SearchDtlId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchDtlId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchDtlId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchDtlId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchDtlId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchDtlId15', true)}
onInputChange={this.SearchDtlId15InputChange()}
value={values.cSearchDtlId15}
defaultSelected={SearchDtlId15List.filter(obj => { return obj.key === SearchDtlId15 })}
options={SearchDtlId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchDtlId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchDtlId15 && touched.cSearchDtlId15 && <span className='form__form-group-error'>{errors.cSearchDtlId15}</span>}
</div>
</Col>
}
{(authCol.SearchDtlIdR15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchDtlIdR15 || {}).ColumnHeader} {(columnLabel.SearchDtlIdR15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchDtlIdR15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchDtlIdR15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchDtlIdR15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchDtlIdR15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchDtlIdR15', true)}
onInputChange={this.SearchDtlIdR15InputChange()}
value={values.cSearchDtlIdR15}
defaultSelected={SearchDtlIdR15List.filter(obj => { return obj.key === SearchDtlIdR15 })}
options={SearchDtlIdR15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchDtlIdR15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchDtlIdR15 && touched.cSearchDtlIdR15 && <span className='form__form-group-error'>{errors.cSearchDtlIdR15}</span>}
</div>
</Col>
}
{(authCol.SearchUrlId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchUrlId15 || {}).ColumnHeader} {(columnLabel.SearchUrlId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchUrlId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchUrlId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchUrlId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchUrlId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchUrlId15', true)}
onInputChange={this.SearchUrlId15InputChange()}
value={values.cSearchUrlId15}
defaultSelected={SearchUrlId15List.filter(obj => { return obj.key === SearchUrlId15 })}
options={SearchUrlId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchUrlId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchUrlId15 && touched.cSearchUrlId15 && <span className='form__form-group-error'>{errors.cSearchUrlId15}</span>}
</div>
</Col>
}
{(authCol.SearchImgId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SearchImgId15 || {}).ColumnHeader} {(columnLabel.SearchImgId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SearchImgId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SearchImgId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cSearchImgId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchImgId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cSearchImgId15', true)}
onInputChange={this.SearchImgId15InputChange()}
value={values.cSearchImgId15}
defaultSelected={SearchImgId15List.filter(obj => { return obj.key === SearchImgId15 })}
options={SearchImgId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.SearchImgId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cSearchImgId15 && touched.cSearchImgId15 && <span className='form__form-group-error'>{errors.cSearchImgId15}</span>}
</div>
</Col>
}
{(authCol.DetailTableId15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailTableId15 || {}).ColumnHeader} {(columnLabel.DetailTableId15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailTableId15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailTableId15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDetailTableId15'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDetailTableId15', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDetailTableId15', true)}
onInputChange={this.DetailTableId15InputChange()}
value={values.cDetailTableId15}
defaultSelected={DetailTableId15List.filter(obj => { return obj.key === DetailTableId15 })}
options={DetailTableId15List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DetailTableId15 || {}).readonly ? true: false }/>
</div>
}
{errors.cDetailTableId15 && touched.cDetailTableId15 && <span className='form__form-group-error'>{errors.cDetailTableId15}</span>}
</div>
</Col>
}
{(authCol.GridRows15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GridRows15 || {}).ColumnHeader} {(columnLabel.GridRows15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GridRows15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GridRows15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cGridRows15'
disabled = {(authCol.GridRows15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cGridRows15 && touched.cGridRows15 && <span className='form__form-group-error'>{errors.cGridRows15}</span>}
</div>
</Col>
}
{(authCol.HasDeleteAll15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cHasDeleteAll15'
onChange={handleChange}
defaultChecked={values.cHasDeleteAll15}
disabled={(authCol.HasDeleteAll15 || {}).readonly || !(authCol.HasDeleteAll15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.HasDeleteAll15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.HasDeleteAll15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HasDeleteAll15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HasDeleteAll15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ShowGridHead15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cShowGridHead15'
onChange={handleChange}
defaultChecked={values.cShowGridHead15}
disabled={(authCol.ShowGridHead15 || {}).readonly || !(authCol.ShowGridHead15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ShowGridHead15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ShowGridHead15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ShowGridHead15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ShowGridHead15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.GenerateSc15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cGenerateSc15'
onChange={handleChange}
defaultChecked={values.cGenerateSc15}
disabled={(authCol.GenerateSc15 || {}).readonly || !(authCol.GenerateSc15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.GenerateSc15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.GenerateSc15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GenerateSc15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GenerateSc15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.GenerateSr15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cGenerateSr15'
onChange={handleChange}
defaultChecked={values.cGenerateSr15}
disabled={(authCol.GenerateSr15 || {}).readonly || !(authCol.GenerateSr15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.GenerateSr15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.GenerateSr15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GenerateSr15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GenerateSr15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ValidateReq15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cValidateReq15'
onChange={handleChange}
defaultChecked={values.cValidateReq15}
disabled={(authCol.ValidateReq15 || {}).readonly || !(authCol.ValidateReq15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ValidateReq15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ValidateReq15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ValidateReq15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ValidateReq15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.DeferError15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cDeferError15'
onChange={handleChange}
defaultChecked={values.cDeferError15}
disabled={(authCol.DeferError15 || {}).readonly || !(authCol.DeferError15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.DeferError15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.DeferError15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DeferError15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DeferError15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AuthRequired15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAuthRequired15'
onChange={handleChange}
defaultChecked={values.cAuthRequired15}
disabled={(authCol.AuthRequired15 || {}).readonly || !(authCol.AuthRequired15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AuthRequired15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AuthRequired15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AuthRequired15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AuthRequired15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.GenAudit15 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cGenAudit15'
onChange={handleChange}
defaultChecked={values.cGenAudit15}
disabled={(authCol.GenAudit15 || {}).readonly || !(authCol.GenAudit15 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.GenAudit15 || {}).ColumnHeader}</span>
</label>
{(columnLabel.GenAudit15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GenAudit15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GenAudit15 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ScreenObj15 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenObj15 || {}).ColumnHeader} {(columnLabel.ScreenObj15 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenObj15 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenObj15 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenObj15'
disabled = {(authCol.ScreenObj15 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenObj15 && touched.cScreenObj15 && <span className='form__form-group-error'>{errors.cScreenObj15}</span>}
</div>
</Col>
}
{(authCol.ScreenFilter || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenFilter || {}).ColumnHeader} {(columnLabel.ScreenFilter || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenFilter || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenFilter || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenFilter'
disabled = {(authCol.ScreenFilter || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenFilter && touched.cScreenFilter && <span className='form__form-group-error'>{errors.cScreenFilter}</span>}
</div>
</Col>
}
{(authCol.MoreInfo || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MoreInfo || {}).ColumnHeader} {(columnLabel.MoreInfo || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MoreInfo || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MoreInfo || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMoreInfo'
disabled = {(authCol.MoreInfo || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMoreInfo && touched.cMoreInfo && <span className='form__form-group-error'>{errors.cMoreInfo}</span>}
</div>
</Col>
}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ScreenId15));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmScreenState) && <Skeleton height='43px' />) ||
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
  AdmScreen: state.AdmScreen,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScreenReduxObj.LoadPage.bind(AdmScreenReduxObj) },
    { SavePage: AdmScreenReduxObj.SavePage.bind(AdmScreenReduxObj) },
    { DelMst: AdmScreenReduxObj.DelMst.bind(AdmScreenReduxObj) },
    { AddMst: AdmScreenReduxObj.AddMst.bind(AdmScreenReduxObj) },
//    { SearchMemberId64: AdmScreenReduxObj.SearchActions.SearchMemberId64.bind(AdmScreenReduxObj) },
//    { SearchCurrencyId64: AdmScreenReduxObj.SearchActions.SearchCurrencyId64.bind(AdmScreenReduxObj) },
//    { SearchCustomerJobId64: AdmScreenReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmScreenReduxObj) },
{ SearchMasterTableId15: AdmScreenReduxObj.SearchActions.SearchMasterTableId15.bind(AdmScreenReduxObj) },
{ SearchSearchTableId15: AdmScreenReduxObj.SearchActions.SearchSearchTableId15.bind(AdmScreenReduxObj) },
{ SearchSearchId15: AdmScreenReduxObj.SearchActions.SearchSearchId15.bind(AdmScreenReduxObj) },
{ SearchSearchIdR15: AdmScreenReduxObj.SearchActions.SearchSearchIdR15.bind(AdmScreenReduxObj) },
{ SearchSearchDtlId15: AdmScreenReduxObj.SearchActions.SearchSearchDtlId15.bind(AdmScreenReduxObj) },
{ SearchSearchDtlIdR15: AdmScreenReduxObj.SearchActions.SearchSearchDtlIdR15.bind(AdmScreenReduxObj) },
{ SearchSearchUrlId15: AdmScreenReduxObj.SearchActions.SearchSearchUrlId15.bind(AdmScreenReduxObj) },
{ SearchSearchImgId15: AdmScreenReduxObj.SearchActions.SearchSearchImgId15.bind(AdmScreenReduxObj) },
{ SearchDetailTableId15: AdmScreenReduxObj.SearchActions.SearchDetailTableId15.bind(AdmScreenReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            