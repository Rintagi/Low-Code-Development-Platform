
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
import ListBox from '../../components/custom/ListBox';
import { default as FileInputFieldV1 } from '../../components/custom/FileInputV1';
import { default as FileInputField } from '../../components/custom/FileInput';
import RintagiScreen from '../../components/custom/Screen';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import { isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getDefaultPath, decodeEmbeddedFileObjectFromServer } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat, formatContent } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import AdmLicenseReduxObj, { ShowMstFilterApplied } from '../../redux/AdmLicense';
import * as AdmLicenseService from '../../services/AdmLicenseService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmLicense || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SystemId1317';
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
    this.StripEmbeddedBase64Prefix = this.StripEmbeddedBase64Prefix.bind(this);
    this.DropdownChangeV1 = this.DropdownChangeV1.bind(this);
    this.FileUploadChangeV1 = this.FileUploadChangeV1.bind(this);
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

  AcquireLicense({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      this.OnClickColumeName = 'AcquireLicense';
      //Enter Custom Code here, eg: submitForm();
      evt.preventDefault();
    }.bind(this);
  }
  RenewLicense({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      this.OnClickColumeName = 'RenewLicense';
      //Enter Custom Code here, eg: submitForm();
      evt.preventDefault();
    }.bind(this);
  }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmLicense || {}).ColumnLabel || {};
    /* standard field validation */
    if (!values.cSystemId1317) { errors.cSystemId1317 = (columnLabel.SystemId1317 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmLicense || {}).Mst || {};

    /* ReactRule: Master Record Save */

    /* ReactRule End: Master Record Save */

    if (errors.length > 0) {
      this.props.showNotification('E', { message: errors[0] });
      setSubmitting(false);
    }
    else {
      const { ScreenButton, OnClickColumeName } = this;
      this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting, key: currMst.key, ScreenButton: ScreenButton, OnClickColumeName: OnClickColumeName });
      this.ScreenButton = null;
      this.OnClickColumeName = null;
      this.props.SavePage(
        this.props.AdmLicense,
        {
          SystemId1317: values.cSystemId1317 || '',
          SystemName1317: values.cSystemName1317 || '',
          SystemAbbr1317: values.cSystemAbbr1317 || '',
          InstallID: values.cInstallID || '',
          AppID: values.cAppID || '',
          AppNameSpace: values.cAppNameSpace || '',
          ExpiryDate: values.cExpiryDate || '',
          ModuleIncluded: values.cModuleIncluded || '',
          FeatureIncluded: values.cFeatureIncluded || '',
          FeatureExcluded: values.cFeatureExcluded || '',
          CompanyCount: values.cCompanyCount || '',
          ProjectCount: values.cProjectCount || '',
          UserCount: values.cUserCount || '',
          LicenseServerUrl: values.cLicenseServerUrl || '',
          LicenseString: values.cLicenseString || '',
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
    const AdmLicenseState = this.props.AdmLicense || {};
    const auxSystemLabels = AdmLicenseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SystemId1317;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmLicense.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmLicense.Label);
            this.props.history.push(getEditMstPath(getNaviPath(naviBar, 'MstRecord', '/'), '_'));
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
    const AdmLicenseState = this.props.AdmLicense || {};
    const auxSystemLabels = AdmLicenseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SystemId1317;
        this.props.DelMst(this.props.AdmLicense, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmLicense || {};
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
    const AdmLicenseState = this.props.AdmLicense || {};
    const auxSystemLabels = AdmLicenseState.SystemLabel || {};
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
      if (!(this.props.AdmLicense || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmLicense || {};

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
        const naviBar = getNaviBar('MstRecord', currMst, currDtl, currReduxScreenState.Label);
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
    const AdmLicenseState = this.props.AdmLicense || {};

    if (AdmLicenseState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmLicenseState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmLicenseReduxObj.GetScreenButtons(AdmLicenseState) || {};
    const itemList = AdmLicenseState.Dtl || [];
    const auxLabels = AdmLicenseState.Label || {};
    const auxSystemLabels = AdmLicenseState.SystemLabel || {};

    const columnLabel = AdmLicenseState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmLicenseState);
    const authRow = (AdmLicenseState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmLicense || {}).Mst || {});
    const currDtl = ((this.props.AdmLicense || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.SystemId1317));
    const selectList = AdmLicenseReduxObj.SearchListToSelectList(AdmLicenseState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const SystemId1317 = currMst.SystemId1317;
    const SystemName1317 = currMst.SystemName1317;
    const SystemAbbr1317 = currMst.SystemAbbr1317;
    const InstallID = currMst.InstallID;
    const AppID = currMst.AppID;
    const AppNameSpace = currMst.AppNameSpace;
    const RegisterInsall = currMst.RegisterInsall;
    const ExpiryDate = currMst.ExpiryDate;
    const ModuleIncluded = currMst.ModuleIncluded;
    const FeatureIncluded = currMst.FeatureIncluded;
    const FeatureExcluded = currMst.FeatureExcluded;
    const CompanyCount = currMst.CompanyCount;
    const ProjectCount = currMst.ProjectCount;
    const UserCount = currMst.UserCount;
    const LicenseServerUrl = currMst.LicenseServerUrl;
    const LicenseString = currMst.LicenseString;

    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const fileFileUploadOptions = {
      CancelFileButton: 'Cancel',
      DeleteFileButton: 'Delete',
      MaxImageSize: {
        Width: 1024,
        Height: 768,
      },
      MinImageSize: {
        Width: 40,
        Height: 40,
      },
      maxSize: 5 * 1024 * 1024,
    }

    /* ReactRule: Master Render */

    /* ReactRule End: Master Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmLicenseState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cSystemId1317: formatContent(SystemId1317 || '', 'TextBox'),
                    cSystemName1317: formatContent(SystemName1317 || '', 'TextBox'),
                    cSystemAbbr1317: formatContent(SystemAbbr1317 || '', 'TextBox'),
                    cInstallID: formatContent(InstallID || '', 'TextBox'),
                    cAppID: formatContent(AppID || '', 'TextBox'),
                    cAppNameSpace: formatContent(AppNameSpace || '', 'TextBox'),
                    cRegisterInsall: formatContent(RegisterInsall || '', 'HyperPopUp'),
                    cExpiryDate: ExpiryDate || new Date(),
                    cModuleIncluded: formatContent(ModuleIncluded || '', 'TextBox'),
                    cFeatureIncluded: formatContent(FeatureIncluded || '', 'TextBox'),
                    cFeatureExcluded: formatContent(FeatureExcluded || '', 'TextBox'),
                    cCompanyCount: formatContent(CompanyCount || '', 'TextBox'),
                    cProjectCount: formatContent(ProjectCount || '', 'TextBox'),
                    cUserCount: formatContent(UserCount || '', 'TextBox'),
                    cLicenseServerUrl: formatContent(LicenseServerUrl || '', 'TextBox'),
                    cLicenseString: formatContent(LicenseString || '', 'MultiLine'),
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
                                {(this.constructor.ShowSpinner(AdmLicenseState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId1317)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId1317)) return null;
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
                          {(selectedMst || {}).key ?
                            <div className='form__form-group'>
                              <div className='form__form-group-narrow'>
                                <div className='form__form-group-field'>
                                  <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                    <span className='radio-btn__label color-blue fw-700 f-14'>{selectedMst.label || ''}</span>
                                    <span className='radio-btn__label__right color-blue fw-700 f-14'><span className='mr-5'>{selectedMst.labelR || ''}</span>
                                    </span>
                                  </span>
                                </div>
                              </div>
                              <div className='form__form-group-field'>
                                <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                  <span className='radio-btn__label color-blue fw-700 f-14'>{selectedMst.detail || ''}</span>
                                  <span className='radio-btn__label__right color-blue fw-700 f-14'><span className='mr-5'>{selectedMst.detailR || ''}</span>
                                  </span>
                                </span>
                              </div>
                            </div>
                            :
                            <div className='form__form-group'>
                              <div className='form__form-group-narrow'>
                                <div className='form__form-group-field'>
                                  <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                    <span className='radio-btn__label color-blue fw-700 f-14'>{NoMasterMsg}</span>
                                  </span>
                                </div>
                              </div>
                            </div>
                          }
                          <div className='w-100'>
                            <Row>
                              {(authCol.SystemId1317 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemId1317 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemId1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemId1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemId1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cSystemId1317'
                                          disabled={(authCol.SystemId1317 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSystemId1317 && touched.cSystemId1317 && <span className='form__form-group-error'>{errors.cSystemId1317}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.SystemName1317 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemName1317 || {}).ColumnHeader} {(columnLabel.SystemName1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemName1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemName1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cSystemName1317'
                                          disabled={(authCol.SystemName1317 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSystemName1317 && touched.cSystemName1317 && <span className='form__form-group-error'>{errors.cSystemName1317}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.SystemAbbr1317 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemAbbr1317 || {}).ColumnHeader} {(columnLabel.SystemAbbr1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemAbbr1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemAbbr1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cSystemAbbr1317'
                                          disabled={(authCol.SystemAbbr1317 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSystemAbbr1317 && touched.cSystemAbbr1317 && <span className='form__form-group-error'>{errors.cSystemAbbr1317}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.InstallID || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.InstallID || {}).ColumnHeader} {(columnLabel.InstallID || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.InstallID || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.InstallID || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cInstallID'
                                          disabled={(authCol.InstallID || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cInstallID && touched.cInstallID && <span className='form__form-group-error'>{errors.cInstallID}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.AppID || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.AppID || {}).ColumnHeader} {(columnLabel.AppID || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.AppID || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.AppID || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cAppID'
                                          disabled={(authCol.AppID || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cAppID && touched.cAppID && <span className='form__form-group-error'>{errors.cAppID}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.AppNameSpace || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.AppNameSpace || {}).ColumnHeader} {(columnLabel.AppNameSpace || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.AppNameSpace || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.AppNameSpace || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cAppNameSpace'
                                          disabled={(authCol.AppNameSpace || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cAppNameSpace && touched.cAppNameSpace && <span className='form__form-group-error'>{errors.cAppNameSpace}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.RegisterInsall || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.RegisterInsall || {}).ColumnHeader} {(columnLabel.RegisterInsall || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.RegisterInsall || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.RegisterInsall || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cRegisterInsall'
                                          disabled={(authCol.RegisterInsall || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cRegisterInsall && touched.cRegisterInsall && <span className='form__form-group-error'>{errors.cRegisterInsall}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ExpiryDate || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ExpiryDate || {}).ColumnHeader} {(columnLabel.ExpiryDate || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ExpiryDate || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ExpiryDate || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DatePicker
                                          name='cExpiryDate'
                                          onChange={this.DateChange(setFieldValue, setFieldTouched, 'cExpiryDate', false)}
                                          onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cExpiryDate', true)}
                                          value={values.cExpiryDate}
                                          selected={values.cExpiryDate}
                                          disabled={(authCol.ExpiryDate || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cExpiryDate && touched.cExpiryDate && <span className='form__form-group-error'>{errors.cExpiryDate}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ModuleIncluded || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ModuleIncluded || {}).ColumnHeader} {(columnLabel.ModuleIncluded || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ModuleIncluded || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ModuleIncluded || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cModuleIncluded'
                                          disabled={(authCol.ModuleIncluded || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cModuleIncluded && touched.cModuleIncluded && <span className='form__form-group-error'>{errors.cModuleIncluded}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.FeatureIncluded || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.FeatureIncluded || {}).ColumnHeader} {(columnLabel.FeatureIncluded || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.FeatureIncluded || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.FeatureIncluded || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cFeatureIncluded'
                                          disabled={(authCol.FeatureIncluded || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cFeatureIncluded && touched.cFeatureIncluded && <span className='form__form-group-error'>{errors.cFeatureIncluded}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.FeatureExcluded || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.FeatureExcluded || {}).ColumnHeader} {(columnLabel.FeatureExcluded || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.FeatureExcluded || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.FeatureExcluded || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cFeatureExcluded'
                                          disabled={(authCol.FeatureExcluded || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cFeatureExcluded && touched.cFeatureExcluded && <span className='form__form-group-error'>{errors.cFeatureExcluded}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.CompanyCount || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.CompanyCount || {}).ColumnHeader} {(columnLabel.CompanyCount || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.CompanyCount || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.CompanyCount || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cCompanyCount'
                                          disabled={(authCol.CompanyCount || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cCompanyCount && touched.cCompanyCount && <span className='form__form-group-error'>{errors.cCompanyCount}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ProjectCount || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ProjectCount || {}).ColumnHeader} {(columnLabel.ProjectCount || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ProjectCount || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ProjectCount || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cProjectCount'
                                          disabled={(authCol.ProjectCount || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cProjectCount && touched.cProjectCount && <span className='form__form-group-error'>{errors.cProjectCount}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.UserCount || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UserCount || {}).ColumnHeader} {(columnLabel.UserCount || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UserCount || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UserCount || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cUserCount'
                                          disabled={(authCol.UserCount || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cUserCount && touched.cUserCount && <span className='form__form-group-error'>{errors.cUserCount}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.LicenseServerUrl || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.LicenseServerUrl || {}).ColumnHeader} {(columnLabel.LicenseServerUrl || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.LicenseServerUrl || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.LicenseServerUrl || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cLicenseServerUrl'
                                          disabled={(authCol.LicenseServerUrl || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cLicenseServerUrl && touched.cLicenseServerUrl && <span className='form__form-group-error'>{errors.cLicenseServerUrl}</span>}
                                  </div>
                                </Col>
                              }
                              <Col lg={6} xl={6}>
                                <div className='form__form-group'>
                                  <div className='d-block'>
                                    {(authCol.AcquireLicense || {}).visible &&
                                      <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10'
                                        disabled={(authCol.AcquireLicense || {}).readonly || !(authCol.AcquireLicense || {}).visible}
                                        onClick={this.AcquireLicense({ naviBar, submitForm, currMst })} >
                                        {auxLabels.AcquireLicense || (columnLabel.AcquireLicense || {}).ColumnName}
                                      </Button>}
                                  </div>
                                </div>
                              </Col>
                              {(authCol.LicenseString || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.LicenseString || {}).ColumnHeader} {(columnLabel.LicenseString || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.LicenseString || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.LicenseString || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmLicenseState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cLicenseString'
                                          disabled={(authCol.LicenseString || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cLicenseString && touched.cLicenseString && <span className='form__form-group-error'>{errors.cLicenseString}</span>}
                                  </div>
                                </Col>
                              }
                              <Col lg={6} xl={6}>
                                <div className='form__form-group'>
                                  <div className='d-block'>
                                    {(authCol.RenewLicense || {}).visible &&
                                      <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10'
                                        disabled={(authCol.RenewLicense || {}).readonly || !(authCol.RenewLicense || {}).visible}
                                        onClick={this.RenewLicense({ naviBar, submitForm, currMst })} >
                                        {auxLabels.RenewLicense || (columnLabel.RenewLicense || {}).ColumnName}
                                      </Button>}
                                  </div>
                                </div>
                              </Col>
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId1317)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SystemId1317));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmLicenseState) && <Skeleton height='43px' />) ||
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
  AdmLicense: state.AdmLicense,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmLicenseReduxObj.LoadPage.bind(AdmLicenseReduxObj) },
    { SavePage: AdmLicenseReduxObj.SavePage.bind(AdmLicenseReduxObj) },
    { DelMst: AdmLicenseReduxObj.DelMst.bind(AdmLicenseReduxObj) },
    { AddMst: AdmLicenseReduxObj.AddMst.bind(AdmLicenseReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
