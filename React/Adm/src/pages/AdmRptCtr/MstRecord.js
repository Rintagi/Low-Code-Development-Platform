
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
import AdmRptCtrReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRptCtr';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRptCtr || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RptCtrId161';
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

ReportId161InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId161(v, filterBy);}}
PRptCtrId161InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchPRptCtrId161(v, filterBy);}}
RptElmId161InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptElmId161(v, filterBy);}}
RptCelId161InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptCelId161(v, filterBy);}}
RptStyleId161InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptStyleId161(v, filterBy);}}
CtrToggle161InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmRptCtr || {}).Mst || {}).ReportId161; _this.props.SearchCtrToggle161(v, filterBy);}}
CtrGrouping161InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmRptCtr || {}).Mst || {}).ReportId161; _this.props.SearchCtrGrouping161(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmRptCtr || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cReportId161 || {}).value)) { errors.cReportId161 = (columnLabel.ReportId161 || {}).ErrMessage;}
if (!values.cRptCtrName161) { errors.cRptCtrName161 = (columnLabel.RptCtrName161 || {}).ErrMessage;}
if (isEmptyId((values.cRptCtrTypeCd161 || {}).value)) { errors.cRptCtrTypeCd161 = (columnLabel.RptCtrTypeCd161 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRptCtr || {}).Mst || {};
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
        this.props.AdmRptCtr,
        {
          RptCtrId161: values.cRptCtrId161|| '',
          ReportId161: (values.cReportId161|| {}).value || '',
          RptCtrName161: values.cRptCtrName161|| '',
          PRptCtrId161: (values.cPRptCtrId161|| {}).value || '',
          RptElmId161: (values.cRptElmId161|| {}).value || '',
          RptCelId161: (values.cRptCelId161|| {}).value || '',
          RptStyleId161: (values.cRptStyleId161|| {}).value || '',
          RptCtrTypeCd161: (values.cRptCtrTypeCd161|| {}).value || '',
          CtrTop161: values.cCtrTop161|| '',
          CtrLeft161: values.cCtrLeft161|| '',
          CtrHeight161: values.cCtrHeight161|| '',
          CtrWidth161: values.cCtrWidth161|| '',
          CtrZIndex161: values.cCtrZIndex161|| '',
          CtrPgBrStart161: values.cCtrPgBrStart161 ? 'Y' : 'N',
          CtrPgBrEnd161: values.cCtrPgBrEnd161 ? 'Y' : 'N',
          CtrCanGrow161: values.cCtrCanGrow161 ? 'Y' : 'N',
          CtrCanShrink161: values.cCtrCanShrink161 ? 'Y' : 'N',
          CtrTogether161: values.cCtrTogether161 ? 'Y' : 'N',
          CtrValue161: values.cCtrValue161|| '',
          CtrAction161: values.cCtrAction161|| '',
          CtrVisibility161: (values.cCtrVisibility161|| {}).value || '',
          CtrToggle161: (values.cCtrToggle161|| {}).value || '',
          CtrGrouping161: (values.cCtrGrouping161|| {}).value || '',
          CtrToolTip161: values.cCtrToolTip161|| '',
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
    const AdmRptCtrState = this.props.AdmRptCtr || {};
    const auxSystemLabels = AdmRptCtrState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RptCtrId161;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRptCtr.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRptCtr.Label);
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
    const AdmRptCtrState = this.props.AdmRptCtr || {};
    const auxSystemLabels = AdmRptCtrState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RptCtrId161;
        this.props.DelMst(this.props.AdmRptCtr, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRptCtr || {};
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
    const AdmRptCtrState = this.props.AdmRptCtr || {};
    const auxSystemLabels = AdmRptCtrState.SystemLabel || {};
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
      if (!(this.props.AdmRptCtr || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRptCtr || {};

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
    const AdmRptCtrState = this.props.AdmRptCtr || {};

    if (AdmRptCtrState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmRptCtrState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmRptCtrReduxObj.GetScreenButtons(AdmRptCtrState) || {};
    const itemList = AdmRptCtrState.Dtl || [];
    const auxLabels = AdmRptCtrState.Label || {};
    const auxSystemLabels = AdmRptCtrState.SystemLabel || {};

    const columnLabel = AdmRptCtrState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmRptCtrState);
    const authRow = (AdmRptCtrState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRptCtr || {}).Mst || {});
    const currDtl = ((this.props.AdmRptCtr || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RptCtrId161));
    const selectList = AdmRptCtrReduxObj.SearchListToSelectList(AdmRptCtrState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RptCtrId161 = currMst.RptCtrId161;
const ReportId161List = AdmRptCtrReduxObj.ScreenDdlSelectors.ReportId161(AdmRptCtrState);
const ReportId161 = currMst.ReportId161;
const RptCtrName161 = currMst.RptCtrName161;
const PRptCtrId161List = AdmRptCtrReduxObj.ScreenDdlSelectors.PRptCtrId161(AdmRptCtrState);
const PRptCtrId161 = currMst.PRptCtrId161;
const RptElmId161List = AdmRptCtrReduxObj.ScreenDdlSelectors.RptElmId161(AdmRptCtrState);
const RptElmId161 = currMst.RptElmId161;
const RptCelId161List = AdmRptCtrReduxObj.ScreenDdlSelectors.RptCelId161(AdmRptCtrState);
const RptCelId161 = currMst.RptCelId161;
const RptStyleId161List = AdmRptCtrReduxObj.ScreenDdlSelectors.RptStyleId161(AdmRptCtrState);
const RptStyleId161 = currMst.RptStyleId161;
const RptCtrTypeCd161List = AdmRptCtrReduxObj.ScreenDdlSelectors.RptCtrTypeCd161(AdmRptCtrState);
const RptCtrTypeCd161 = currMst.RptCtrTypeCd161;
const CtrTop161 = currMst.CtrTop161;
const CtrLeft161 = currMst.CtrLeft161;
const CtrHeight161 = currMst.CtrHeight161;
const CtrWidth161 = currMst.CtrWidth161;
const CtrZIndex161 = currMst.CtrZIndex161;
const CtrPgBrStart161 = currMst.CtrPgBrStart161;
const CtrPgBrEnd161 = currMst.CtrPgBrEnd161;
const CtrCanGrow161 = currMst.CtrCanGrow161;
const CtrCanShrink161 = currMst.CtrCanShrink161;
const CtrTogether161 = currMst.CtrTogether161;
const CtrValue161 = currMst.CtrValue161;
const CtrAction161 = currMst.CtrAction161;
const CtrVisibility161List = AdmRptCtrReduxObj.ScreenDdlSelectors.CtrVisibility161(AdmRptCtrState);
const CtrVisibility161 = currMst.CtrVisibility161;
const CtrToggle161List = AdmRptCtrReduxObj.ScreenDdlSelectors.CtrToggle161(AdmRptCtrState);
const CtrToggle161 = currMst.CtrToggle161;
const CtrGrouping161List = AdmRptCtrReduxObj.ScreenDdlSelectors.CtrGrouping161(AdmRptCtrState);
const CtrGrouping161 = currMst.CtrGrouping161;
const CtrToolTip161 = currMst.CtrToolTip161;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmRptCtrState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRptCtrId161: RptCtrId161 || '',
                  cReportId161: ReportId161List.filter(obj => { return obj.key === ReportId161 })[0],
                  cRptCtrName161: RptCtrName161 || '',
                  cPRptCtrId161: PRptCtrId161List.filter(obj => { return obj.key === PRptCtrId161 })[0],
                  cRptElmId161: RptElmId161List.filter(obj => { return obj.key === RptElmId161 })[0],
                  cRptCelId161: RptCelId161List.filter(obj => { return obj.key === RptCelId161 })[0],
                  cRptStyleId161: RptStyleId161List.filter(obj => { return obj.key === RptStyleId161 })[0],
                  cRptCtrTypeCd161: RptCtrTypeCd161List.filter(obj => { return obj.key === RptCtrTypeCd161 })[0],
                  cCtrTop161: CtrTop161 || '',
                  cCtrLeft161: CtrLeft161 || '',
                  cCtrHeight161: CtrHeight161 || '',
                  cCtrWidth161: CtrWidth161 || '',
                  cCtrZIndex161: CtrZIndex161 || '',
                  cCtrPgBrStart161: CtrPgBrStart161 === 'Y',
                  cCtrPgBrEnd161: CtrPgBrEnd161 === 'Y',
                  cCtrCanGrow161: CtrCanGrow161 === 'Y',
                  cCtrCanShrink161: CtrCanShrink161 === 'Y',
                  cCtrTogether161: CtrTogether161 === 'Y',
                  cCtrValue161: CtrValue161 || '',
                  cCtrAction161: CtrAction161 || '',
                  cCtrVisibility161: CtrVisibility161List.filter(obj => { return obj.key === CtrVisibility161 })[0],
                  cCtrToggle161: CtrToggle161List.filter(obj => { return obj.key === CtrToggle161 })[0],
                  cCtrGrouping161: CtrGrouping161List.filter(obj => { return obj.key === CtrGrouping161 })[0],
                  cCtrToolTip161: CtrToolTip161 || '',
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
                                {(this.constructor.ShowSpinner(AdmRptCtrState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptCtrId161)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptCtrId161)) return null;
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
            {(authCol.RptCtrId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCtrId161 || {}).ColumnHeader} {(columnLabel.RptCtrId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCtrId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCtrId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptCtrId161'
disabled = {(authCol.RptCtrId161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptCtrId161 && touched.cRptCtrId161 && <span className='form__form-group-error'>{errors.cRptCtrId161}</span>}
</div>
</Col>
}
{(authCol.ReportId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId161 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId161', true)}
onInputChange={this.ReportId161InputChange()}
value={values.cReportId161}
defaultSelected={ReportId161List.filter(obj => { return obj.key === ReportId161 })}
options={ReportId161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId161 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId161 && touched.cReportId161 && <span className='form__form-group-error'>{errors.cReportId161}</span>}
</div>
</Col>
}
{(authCol.RptCtrName161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCtrName161 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptCtrName161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCtrName161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCtrName161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptCtrName161'
disabled = {(authCol.RptCtrName161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptCtrName161 && touched.cRptCtrName161 && <span className='form__form-group-error'>{errors.cRptCtrName161}</span>}
</div>
</Col>
}
{(authCol.PRptCtrId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PRptCtrId161 || {}).ColumnHeader} {(columnLabel.PRptCtrId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PRptCtrId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PRptCtrId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cPRptCtrId161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cPRptCtrId161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cPRptCtrId161', true)}
onInputChange={this.PRptCtrId161InputChange()}
value={values.cPRptCtrId161}
defaultSelected={PRptCtrId161List.filter(obj => { return obj.key === PRptCtrId161 })}
options={PRptCtrId161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.PRptCtrId161 || {}).readonly ? true: false }/>
</div>
}
{errors.cPRptCtrId161 && touched.cPRptCtrId161 && <span className='form__form-group-error'>{errors.cPRptCtrId161}</span>}
</div>
</Col>
}
{(authCol.RptElmId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptElmId161 || {}).ColumnHeader} {(columnLabel.RptElmId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptElmId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptElmId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptElmId161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptElmId161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptElmId161', true)}
onInputChange={this.RptElmId161InputChange()}
value={values.cRptElmId161}
defaultSelected={RptElmId161List.filter(obj => { return obj.key === RptElmId161 })}
options={RptElmId161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptElmId161 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptElmId161 && touched.cRptElmId161 && <span className='form__form-group-error'>{errors.cRptElmId161}</span>}
</div>
</Col>
}
{(authCol.RptCelId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCelId161 || {}).ColumnHeader} {(columnLabel.RptCelId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCelId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCelId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptCelId161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCelId161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCelId161', true)}
onInputChange={this.RptCelId161InputChange()}
value={values.cRptCelId161}
defaultSelected={RptCelId161List.filter(obj => { return obj.key === RptCelId161 })}
options={RptCelId161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptCelId161 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptCelId161 && touched.cRptCelId161 && <span className='form__form-group-error'>{errors.cRptCelId161}</span>}
</div>
</Col>
}
{(authCol.RptStyleId161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptStyleId161 || {}).ColumnHeader} {(columnLabel.RptStyleId161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptStyleId161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptStyleId161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptStyleId161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptStyleId161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptStyleId161', true)}
onInputChange={this.RptStyleId161InputChange()}
value={values.cRptStyleId161}
defaultSelected={RptStyleId161List.filter(obj => { return obj.key === RptStyleId161 })}
options={RptStyleId161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptStyleId161 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptStyleId161 && touched.cRptStyleId161 && <span className='form__form-group-error'>{errors.cRptStyleId161}</span>}
</div>
</Col>
}
{(authCol.RptCtrTypeCd161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCtrTypeCd161 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptCtrTypeCd161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCtrTypeCd161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCtrTypeCd161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRptCtrTypeCd161'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRptCtrTypeCd161')}
value={values.cRptCtrTypeCd161}
options={RptCtrTypeCd161List}
placeholder=''
disabled = {(authCol.RptCtrTypeCd161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptCtrTypeCd161 && touched.cRptCtrTypeCd161 && <span className='form__form-group-error'>{errors.cRptCtrTypeCd161}</span>}
</div>
</Col>
}
{(authCol.CtrTop161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrTop161 || {}).ColumnHeader} {(columnLabel.CtrTop161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrTop161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrTop161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrTop161'
disabled = {(authCol.CtrTop161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrTop161 && touched.cCtrTop161 && <span className='form__form-group-error'>{errors.cCtrTop161}</span>}
</div>
</Col>
}
{(authCol.CtrLeft161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrLeft161 || {}).ColumnHeader} {(columnLabel.CtrLeft161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrLeft161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrLeft161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrLeft161'
disabled = {(authCol.CtrLeft161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrLeft161 && touched.cCtrLeft161 && <span className='form__form-group-error'>{errors.cCtrLeft161}</span>}
</div>
</Col>
}
{(authCol.CtrHeight161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrHeight161 || {}).ColumnHeader} {(columnLabel.CtrHeight161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrHeight161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrHeight161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrHeight161'
disabled = {(authCol.CtrHeight161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrHeight161 && touched.cCtrHeight161 && <span className='form__form-group-error'>{errors.cCtrHeight161}</span>}
</div>
</Col>
}
{(authCol.CtrWidth161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrWidth161 || {}).ColumnHeader} {(columnLabel.CtrWidth161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrWidth161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrWidth161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrWidth161'
disabled = {(authCol.CtrWidth161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrWidth161 && touched.cCtrWidth161 && <span className='form__form-group-error'>{errors.cCtrWidth161}</span>}
</div>
</Col>
}
{(authCol.CtrZIndex161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrZIndex161 || {}).ColumnHeader} {(columnLabel.CtrZIndex161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrZIndex161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrZIndex161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrZIndex161'
disabled = {(authCol.CtrZIndex161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrZIndex161 && touched.cCtrZIndex161 && <span className='form__form-group-error'>{errors.cCtrZIndex161}</span>}
</div>
</Col>
}
{(authCol.CtrPgBrStart161 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCtrPgBrStart161'
onChange={handleChange}
defaultChecked={values.cCtrPgBrStart161}
disabled={(authCol.CtrPgBrStart161 || {}).readonly || !(authCol.CtrPgBrStart161 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CtrPgBrStart161 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CtrPgBrStart161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrPgBrStart161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrPgBrStart161 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CtrPgBrEnd161 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCtrPgBrEnd161'
onChange={handleChange}
defaultChecked={values.cCtrPgBrEnd161}
disabled={(authCol.CtrPgBrEnd161 || {}).readonly || !(authCol.CtrPgBrEnd161 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CtrPgBrEnd161 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CtrPgBrEnd161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrPgBrEnd161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrPgBrEnd161 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CtrCanGrow161 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCtrCanGrow161'
onChange={handleChange}
defaultChecked={values.cCtrCanGrow161}
disabled={(authCol.CtrCanGrow161 || {}).readonly || !(authCol.CtrCanGrow161 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CtrCanGrow161 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CtrCanGrow161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrCanGrow161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrCanGrow161 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CtrCanShrink161 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCtrCanShrink161'
onChange={handleChange}
defaultChecked={values.cCtrCanShrink161}
disabled={(authCol.CtrCanShrink161 || {}).readonly || !(authCol.CtrCanShrink161 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CtrCanShrink161 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CtrCanShrink161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrCanShrink161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrCanShrink161 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CtrTogether161 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCtrTogether161'
onChange={handleChange}
defaultChecked={values.cCtrTogether161}
disabled={(authCol.CtrTogether161 || {}).readonly || !(authCol.CtrTogether161 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CtrTogether161 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CtrTogether161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrTogether161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrTogether161 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CtrValue161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrValue161 || {}).ColumnHeader} {(columnLabel.CtrValue161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrValue161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrValue161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrValue161'
disabled = {(authCol.CtrValue161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrValue161 && touched.cCtrValue161 && <span className='form__form-group-error'>{errors.cCtrValue161}</span>}
</div>
</Col>
}
{(authCol.CtrAction161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrAction161 || {}).ColumnHeader} {(columnLabel.CtrAction161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrAction161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrAction161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrAction161'
disabled = {(authCol.CtrAction161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrAction161 && touched.cCtrAction161 && <span className='form__form-group-error'>{errors.cCtrAction161}</span>}
</div>
</Col>
}
{(authCol.CtrVisibility161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrVisibility161 || {}).ColumnHeader} {(columnLabel.CtrVisibility161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrVisibility161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrVisibility161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cCtrVisibility161'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cCtrVisibility161')}
value={values.cCtrVisibility161}
options={CtrVisibility161List}
placeholder=''
disabled = {(authCol.CtrVisibility161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrVisibility161 && touched.cCtrVisibility161 && <span className='form__form-group-error'>{errors.cCtrVisibility161}</span>}
</div>
</Col>
}
{(authCol.CtrToggle161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrToggle161 || {}).ColumnHeader} {(columnLabel.CtrToggle161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrToggle161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrToggle161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCtrToggle161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCtrToggle161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCtrToggle161', true)}
onInputChange={this.CtrToggle161InputChange()}
value={values.cCtrToggle161}
defaultSelected={CtrToggle161List.filter(obj => { return obj.key === CtrToggle161 })}
options={CtrToggle161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CtrToggle161 || {}).readonly ? true: false }/>
</div>
}
{errors.cCtrToggle161 && touched.cCtrToggle161 && <span className='form__form-group-error'>{errors.cCtrToggle161}</span>}
</div>
</Col>
}
{(authCol.CtrGrouping161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrGrouping161 || {}).ColumnHeader} {(columnLabel.CtrGrouping161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrGrouping161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrGrouping161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCtrGrouping161'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCtrGrouping161', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCtrGrouping161', true)}
onInputChange={this.CtrGrouping161InputChange()}
value={values.cCtrGrouping161}
defaultSelected={CtrGrouping161List.filter(obj => { return obj.key === CtrGrouping161 })}
options={CtrGrouping161List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CtrGrouping161 || {}).readonly ? true: false }/>
</div>
}
{errors.cCtrGrouping161 && touched.cCtrGrouping161 && <span className='form__form-group-error'>{errors.cCtrGrouping161}</span>}
</div>
</Col>
}
{(authCol.CtrToolTip161 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CtrToolTip161 || {}).ColumnHeader} {(columnLabel.CtrToolTip161 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CtrToolTip161 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CtrToolTip161 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptCtrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCtrToolTip161'
disabled = {(authCol.CtrToolTip161 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCtrToolTip161 && touched.cCtrToolTip161 && <span className='form__form-group-error'>{errors.cCtrToolTip161}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptCtrId161)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RptCtrId161));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmRptCtrState) && <Skeleton height='43px' />) ||
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
  AdmRptCtr: state.AdmRptCtr,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmRptCtrReduxObj.LoadPage.bind(AdmRptCtrReduxObj) },
    { SavePage: AdmRptCtrReduxObj.SavePage.bind(AdmRptCtrReduxObj) },
    { DelMst: AdmRptCtrReduxObj.DelMst.bind(AdmRptCtrReduxObj) },
    { AddMst: AdmRptCtrReduxObj.AddMst.bind(AdmRptCtrReduxObj) },
//    { SearchMemberId64: AdmRptCtrReduxObj.SearchActions.SearchMemberId64.bind(AdmRptCtrReduxObj) },
//    { SearchCurrencyId64: AdmRptCtrReduxObj.SearchActions.SearchCurrencyId64.bind(AdmRptCtrReduxObj) },
//    { SearchCustomerJobId64: AdmRptCtrReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmRptCtrReduxObj) },
{ SearchReportId161: AdmRptCtrReduxObj.SearchActions.SearchReportId161.bind(AdmRptCtrReduxObj) },
{ SearchPRptCtrId161: AdmRptCtrReduxObj.SearchActions.SearchPRptCtrId161.bind(AdmRptCtrReduxObj) },
{ SearchRptElmId161: AdmRptCtrReduxObj.SearchActions.SearchRptElmId161.bind(AdmRptCtrReduxObj) },
{ SearchRptCelId161: AdmRptCtrReduxObj.SearchActions.SearchRptCelId161.bind(AdmRptCtrReduxObj) },
{ SearchRptStyleId161: AdmRptCtrReduxObj.SearchActions.SearchRptStyleId161.bind(AdmRptCtrReduxObj) },
{ SearchCtrToggle161: AdmRptCtrReduxObj.SearchActions.SearchCtrToggle161.bind(AdmRptCtrReduxObj) },
{ SearchCtrGrouping161: AdmRptCtrReduxObj.SearchActions.SearchCtrGrouping161.bind(AdmRptCtrReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            