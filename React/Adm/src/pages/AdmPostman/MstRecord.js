
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Prompt, Redirect } from 'react-router';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import { Formik, Field, Form } from 'formik';
import DocumentTitle from '../../components/custom/DocumentTitle';
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
import SignaturePanel from '../../components/custom/SignaturePanel';
import RintagiScreen from '../../components/custom/Screen';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import { isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getDefaultPath, decodeEmbeddedFileObjectFromServer } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat, formatContent } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import AdmPostmanReduxObj, { ShowMstFilterApplied } from '../../redux/AdmPostman';
import * as AdmPostmanService from '../../services/AdmPostmanService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmPostman || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SystemId1317';
    this.SystemName = (document.Rintagi || {}).systemName || 'Rintagi';
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

  Submit({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      this.OnClickColumeName = 'Submit';
      //Enter Custom Code here, eg: submitForm();
      evt.preventDefault();
    }.bind(this);
  }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmPostman || {}).ColumnLabel || {};
    /* standard field validation */
    if (!values.cSystemId1317) { errors.cSystemId1317 = (columnLabel.SystemId1317 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmPostman || {}).Mst || {};

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
        this.props.AdmPostman,
        {
          SystemId1317: values.cSystemId1317 || '',
          SystemName1317: values.cSystemName1317 || '',
          SystemAbbr1317: values.cSystemAbbr1317 || '',
          ServerName1317: values.cServerName1317 || '',
          EndpointUrl: values.cEndpointUrl || '',
          Method: values.cMethod || '',
          Result: values.cResult || '',
          OTPCode: values.cOTPCode || '',
          QueryString: values.cQueryString || '',
          QSUrlEncode: values.cQSUrlEncode ? 'Y' : 'N',
          Body: values.cBody || '',
          PostContentType: (values.cPostContentType || {}).value || '',
          BodyUrlEncode: values.cBodyUrlEncode ? 'Y' : 'N',
          Header: values.cHeader || '',
          Cookie: values.cCookie || '',
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
    const AdmPostmanState = this.props.AdmPostman || {};
    const auxSystemLabels = AdmPostmanState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SystemId1317;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmPostman.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmPostman.Label);
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
    const AdmPostmanState = this.props.AdmPostman || {};
    const auxSystemLabels = AdmPostmanState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SystemId1317;
        this.props.DelMst(this.props.AdmPostman, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmPostman || {};
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
    const AdmPostmanState = this.props.AdmPostman || {};
    const auxSystemLabels = AdmPostmanState.SystemLabel || {};
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
      if (!(this.props.AdmPostman || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmPostman || {};

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
    const AdmPostmanState = this.props.AdmPostman || {};

    if (AdmPostmanState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmPostmanState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmPostmanReduxObj.GetScreenButtons(AdmPostmanState) || {};
    const itemList = AdmPostmanState.Dtl || [];
    const auxLabels = AdmPostmanState.Label || {};
    const auxSystemLabels = AdmPostmanState.SystemLabel || {};

    const columnLabel = AdmPostmanState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmPostmanState);
    const authRow = (AdmPostmanState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmPostman || {}).Mst || {});
    const currDtl = ((this.props.AdmPostman || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.SystemId1317));
    const selectList = AdmPostmanReduxObj.SearchListToSelectList(AdmPostmanState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const SystemId1317 = currMst.SystemId1317;
    const SystemName1317 = currMst.SystemName1317;
    const SystemAbbr1317 = currMst.SystemAbbr1317;
    const ServerName1317 = currMst.ServerName1317;
    const EndpointUrl = currMst.EndpointUrl;
    const Method = currMst.Method;
    const Result = currMst.Result;
    const OTPCode = currMst.OTPCode;
    const QueryString = currMst.QueryString;
    const QSUrlEncode = currMst.QSUrlEncode;
    const Body = currMst.Body;
    const PostContentTypeList = AdmPostmanReduxObj.ScreenDdlSelectors.PostContentType(AdmPostmanState);
    const PostContentType = currMst.PostContentType;
    const BodyUrlEncode = currMst.BodyUrlEncode;
    const Header = currMst.Header;
    const Cookie = currMst.Cookie;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmPostmanState) && <div className='panel__refresh'></div>} */}
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
                    cServerName1317: formatContent(ServerName1317 || '', 'TextBox'),
                    cEndpointUrl: formatContent(EndpointUrl || '', 'TextBox'),
                    cMethod: formatContent(Method || '', 'TextBox'),
                    cResult: formatContent(Result || '', 'MultiLine'),
                    cOTPCode: formatContent(OTPCode || '', 'OTPTextBox'),
                    cQueryString: formatContent(QueryString || '', 'MultiLine'),
                    cQSUrlEncode: QSUrlEncode === 'Y',
                    cBody: formatContent(Body || '', 'MultiLine'),
                    cPostContentType: PostContentTypeList.filter(obj => { return obj.key === PostContentType })[0],
                    cBodyUrlEncode: BodyUrlEncode === 'Y',
                    cHeader: formatContent(Header || '', 'MultiLine'),
                    cCookie: formatContent(Cookie || '', 'MultiLine'),
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
                                {(this.constructor.ShowSpinner(AdmPostmanState) && <Skeleton height='40px' />) ||
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
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemId1317 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemId1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemId1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemId1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
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
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemName1317 || {}).ColumnHeader} {(columnLabel.SystemName1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemName1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemName1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
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
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemAbbr1317 || {}).ColumnHeader} {(columnLabel.SystemAbbr1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemAbbr1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemAbbr1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
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
                              {(authCol.ServerName1317 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ServerName1317 || {}).ColumnHeader} {(columnLabel.ServerName1317 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ServerName1317 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ServerName1317 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cServerName1317'
                                          disabled={(authCol.ServerName1317 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cServerName1317 && touched.cServerName1317 && <span className='form__form-group-error'>{errors.cServerName1317}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.EndpointUrl || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.EndpointUrl || {}).ColumnHeader} {(columnLabel.EndpointUrl || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.EndpointUrl || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.EndpointUrl || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cEndpointUrl'
                                          disabled={(authCol.EndpointUrl || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cEndpointUrl && touched.cEndpointUrl && <span className='form__form-group-error'>{errors.cEndpointUrl}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Method || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Method || {}).ColumnHeader} {(columnLabel.Method || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Method || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Method || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMethod'
                                          disabled={(authCol.Method || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMethod && touched.cMethod && <span className='form__form-group-error'>{errors.cMethod}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Result || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Result || {}).ColumnHeader} {(columnLabel.Result || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Result || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Result || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cResult'
                                          disabled={(authCol.Result || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cResult && touched.cResult && <span className='form__form-group-error'>{errors.cResult}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.OTPCode || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.OTPCode || {}).ColumnHeader} {(columnLabel.OTPCode || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.OTPCode || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.OTPCode || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      (<div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cOTPCode'
                                          disabled={(authCol.OTPCode || {}).readonly ? 'disabled' : ''} />
                                      </div>)
                                    }
                                    {errors.cOTPCode && touched.cOTPCode && <span className='form__form-group-error'>{errors.cOTPCode}</span>}
                                  </div>
                                </Col>
                              }
                                {(authCol.Submit || {}).visible &&
                                  <Col lg={6} xl={6}>
                                    <div className='form__form-group'>
                                      <div className='d-block'>
                                          <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10'
                                            disabled={(authCol.Submit || {}).readonly || !(authCol.Submit || {}).visible}
                                            onClick={this.Submit({ naviBar, submitForm, currMst })} >
                                            {auxLabels.Submit || (columnLabel.Submit || {}).ColumnHeader || (columnLabel.Submit || {}).ColumnName}
                                          </Button>
                                      </div>
                                    </div>
                                  </Col>
                                }
                              {(authCol.QueryString || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.QueryString || {}).ColumnHeader} {(columnLabel.QueryString || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.QueryString || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.QueryString || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cQueryString'
                                          disabled={(authCol.QueryString || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cQueryString && touched.cQueryString && <span className='form__form-group-error'>{errors.cQueryString}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.QSUrlEncode || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cQSUrlEncode'
                                        onChange={handleChange}
                                        defaultChecked={values.cQSUrlEncode}
                                        disabled={(authCol.QSUrlEncode || {}).readonly || !(authCol.QSUrlEncode || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.QSUrlEncode || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.QSUrlEncode || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.QSUrlEncode || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.QSUrlEncode || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }
                              {(authCol.Body || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Body || {}).ColumnHeader} {(columnLabel.Body || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Body || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Body || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cBody'
                                          disabled={(authCol.Body || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cBody && touched.cBody && <span className='form__form-group-error'>{errors.cBody}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.PostContentType || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.PostContentType || {}).ColumnHeader} {(columnLabel.PostContentType || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.PostContentType || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.PostContentType || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cPostContentType'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cPostContentType')}
                                          value={values.cPostContentType}
                                          options={PostContentTypeList}
                                          placeholder=''
                                          disabled={(authCol.PostContentType || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cPostContentType && touched.cPostContentType && <span className='form__form-group-error'>{errors.cPostContentType}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.BodyUrlEncode || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cBodyUrlEncode'
                                        onChange={handleChange}
                                        defaultChecked={values.cBodyUrlEncode}
                                        disabled={(authCol.BodyUrlEncode || {}).readonly || !(authCol.BodyUrlEncode || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.BodyUrlEncode || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.BodyUrlEncode || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.BodyUrlEncode || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.BodyUrlEncode || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }
                              {(authCol.Header || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Header || {}).ColumnHeader} {(columnLabel.Header || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Header || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Header || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cHeader'
                                          disabled={(authCol.Header || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cHeader && touched.cHeader && <span className='form__form-group-error'>{errors.cHeader}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Cookie || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Cookie || {}).ColumnHeader} {(columnLabel.Cookie || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Cookie || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Cookie || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmPostmanState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cCookie'
                                          disabled={(authCol.Cookie || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cCookie && touched.cCookie && <span className='form__form-group-error'>{errors.cCookie}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId1317)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SystemId1317));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmPostmanState) && <Skeleton height='43px' />) ||
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
  AdmPostman: state.AdmPostman,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmPostmanReduxObj.LoadPage.bind(AdmPostmanReduxObj) },
    { SavePage: AdmPostmanReduxObj.SavePage.bind(AdmPostmanReduxObj) },
    { DelMst: AdmPostmanReduxObj.DelMst.bind(AdmPostmanReduxObj) },
    { AddMst: AdmPostmanReduxObj.AddMst.bind(AdmPostmanReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
