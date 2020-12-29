
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
import AdmButtonHlpReduxObj, { ShowMstFilterApplied } from '../../redux/AdmButtonHlp';
import * as AdmButtonHlpService from '../../services/AdmButtonHlpService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmButtonHlp || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ButtonHlpId116';
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

  CultureId116InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchCultureId116(v, filterBy); } }
  ButtonTypeId116InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchButtonTypeId116(v, filterBy); } }
  ScreenId116InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchScreenId116(v, filterBy); } }
  ReportId116InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchReportId116(v, filterBy); } }
  WizardId116InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchWizardId116(v, filterBy); } }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmButtonHlp || {}).ColumnLabel || {};
    /* standard field validation */
    if (isEmptyId((values.cCultureId116 || {}).value)) { errors.cCultureId116 = (columnLabel.CultureId116 || {}).ErrMessage; }
    if (isEmptyId((values.cButtonTypeId116 || {}).value)) { errors.cButtonTypeId116 = (columnLabel.ButtonTypeId116 || {}).ErrMessage; }
    if (isEmptyId((values.cTopVisible116 || {}).value)) { errors.cTopVisible116 = (columnLabel.TopVisible116 || {}).ErrMessage; }
    if (isEmptyId((values.cRowVisible116 || {}).value)) { errors.cRowVisible116 = (columnLabel.RowVisible116 || {}).ErrMessage; }
    if (isEmptyId((values.cBotVisible116 || {}).value)) { errors.cBotVisible116 = (columnLabel.BotVisible116 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmButtonHlp || {}).Mst || {};

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
        this.props.AdmButtonHlp,
        {
          ButtonHlpId116: values.cButtonHlpId116 || '',
          CultureId116: (values.cCultureId116 || {}).value || '',
          ButtonTypeId116: (values.cButtonTypeId116 || {}).value || '',
          ButtonName116: values.cButtonName116 || '',
          ButtonLongNm116: values.cButtonLongNm116 || '',
          ScreenId116: (values.cScreenId116 || {}).value || '',
          ReportId116: (values.cReportId116 || {}).value || '',
          WizardId116: (values.cWizardId116 || {}).value || '',
          ButtonToolTip116: values.cButtonToolTip116 || '',
          ButtonVisible116: values.cButtonVisible116 ? 'Y' : 'N',
          TopVisible116: (values.cTopVisible116 || {}).value || '',
          RowVisible116: (values.cRowVisible116 || {}).value || '',
          BotVisible116: (values.cBotVisible116 || {}).value || '',
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
    const AdmButtonHlpState = this.props.AdmButtonHlp || {};
    const auxSystemLabels = AdmButtonHlpState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ButtonHlpId116;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmButtonHlp.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmButtonHlp.Label);
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
    const AdmButtonHlpState = this.props.AdmButtonHlp || {};
    const auxSystemLabels = AdmButtonHlpState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ButtonHlpId116;
        this.props.DelMst(this.props.AdmButtonHlp, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmButtonHlp || {};
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
    const AdmButtonHlpState = this.props.AdmButtonHlp || {};
    const auxSystemLabels = AdmButtonHlpState.SystemLabel || {};
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
      if (!(this.props.AdmButtonHlp || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmButtonHlp || {};

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
    const AdmButtonHlpState = this.props.AdmButtonHlp || {};

    if (AdmButtonHlpState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmButtonHlpState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmButtonHlpReduxObj.GetScreenButtons(AdmButtonHlpState) || {};
    const itemList = AdmButtonHlpState.Dtl || [];
    const auxLabels = AdmButtonHlpState.Label || {};
    const auxSystemLabels = AdmButtonHlpState.SystemLabel || {};

    const columnLabel = AdmButtonHlpState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmButtonHlpState);
    const authRow = (AdmButtonHlpState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmButtonHlp || {}).Mst || {});
    const currDtl = ((this.props.AdmButtonHlp || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.ButtonHlpId116));
    const selectList = AdmButtonHlpReduxObj.SearchListToSelectList(AdmButtonHlpState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const ButtonHlpId116 = currMst.ButtonHlpId116;
    const CultureId116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.CultureId116(AdmButtonHlpState);
    const CultureId116 = currMst.CultureId116;
    const ButtonTypeId116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.ButtonTypeId116(AdmButtonHlpState);
    const ButtonTypeId116 = currMst.ButtonTypeId116;
    const ButtonName116 = currMst.ButtonName116;
    const ButtonLongNm116 = currMst.ButtonLongNm116;
    const ScreenId116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.ScreenId116(AdmButtonHlpState);
    const ScreenId116 = currMst.ScreenId116;
    const ReportId116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.ReportId116(AdmButtonHlpState);
    const ReportId116 = currMst.ReportId116;
    const WizardId116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.WizardId116(AdmButtonHlpState);
    const WizardId116 = currMst.WizardId116;
    const ButtonToolTip116 = currMst.ButtonToolTip116;
    const ButtonVisible116 = currMst.ButtonVisible116;
    const TopVisible116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.TopVisible116(AdmButtonHlpState);
    const TopVisible116 = currMst.TopVisible116;
    const RowVisible116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.RowVisible116(AdmButtonHlpState);
    const RowVisible116 = currMst.RowVisible116;
    const BotVisible116List = AdmButtonHlpReduxObj.ScreenDdlSelectors.BotVisible116(AdmButtonHlpState);
    const BotVisible116 = currMst.BotVisible116;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmButtonHlpState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cButtonHlpId116: formatContent(ButtonHlpId116 || '', 'TextBox'),
                    cCultureId116: CultureId116List.filter(obj => { return obj.key === CultureId116 })[0],
                    cButtonTypeId116: ButtonTypeId116List.filter(obj => { return obj.key === ButtonTypeId116 })[0],
                    cButtonName116: formatContent(ButtonName116 || '', 'TextBox'),
                    cButtonLongNm116: formatContent(ButtonLongNm116 || '', 'TextBox'),
                    cScreenId116: ScreenId116List.filter(obj => { return obj.key === ScreenId116 })[0],
                    cReportId116: ReportId116List.filter(obj => { return obj.key === ReportId116 })[0],
                    cWizardId116: WizardId116List.filter(obj => { return obj.key === WizardId116 })[0],
                    cButtonToolTip116: formatContent(ButtonToolTip116 || '', 'TextBox'),
                    cButtonVisible116: ButtonVisible116 === 'Y',
                    cTopVisible116: TopVisible116List.filter(obj => { return obj.key === TopVisible116 })[0],
                    cRowVisible116: RowVisible116List.filter(obj => { return obj.key === RowVisible116 })[0],
                    cBotVisible116: BotVisible116List.filter(obj => { return obj.key === BotVisible116 })[0],
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
                                {(this.constructor.ShowSpinner(AdmButtonHlpState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ButtonHlpId116)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ButtonHlpId116)) return null;
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
                              {(authCol.ButtonHlpId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ButtonHlpId116 || {}).ColumnHeader} {(columnLabel.ButtonHlpId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ButtonHlpId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonHlpId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cButtonHlpId116'
                                          disabled={(authCol.ButtonHlpId116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cButtonHlpId116 && touched.cButtonHlpId116 && <span className='form__form-group-error'>{errors.cButtonHlpId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.CultureId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.CultureId116 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.CultureId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.CultureId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cCultureId116'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId116', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId116', true)}
                                          onInputChange={this.CultureId116InputChange()}
                                          value={values.cCultureId116}
                                          defaultSelected={CultureId116List.filter(obj => { return obj.key === CultureId116 })}
                                          options={CultureId116List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.CultureId116 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cCultureId116 && touched.cCultureId116 && <span className='form__form-group-error'>{errors.cCultureId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ButtonTypeId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ButtonTypeId116 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ButtonTypeId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ButtonTypeId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonTypeId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cButtonTypeId116'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cButtonTypeId116', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cButtonTypeId116', true)}
                                          onInputChange={this.ButtonTypeId116InputChange()}
                                          value={values.cButtonTypeId116}
                                          defaultSelected={ButtonTypeId116List.filter(obj => { return obj.key === ButtonTypeId116 })}
                                          options={ButtonTypeId116List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ButtonTypeId116 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cButtonTypeId116 && touched.cButtonTypeId116 && <span className='form__form-group-error'>{errors.cButtonTypeId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ButtonName116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ButtonName116 || {}).ColumnHeader} {(columnLabel.ButtonName116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ButtonName116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonName116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cButtonName116'
                                          disabled={(authCol.ButtonName116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cButtonName116 && touched.cButtonName116 && <span className='form__form-group-error'>{errors.cButtonName116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ButtonLongNm116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ButtonLongNm116 || {}).ColumnHeader} {(columnLabel.ButtonLongNm116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ButtonLongNm116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonLongNm116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cButtonLongNm116'
                                          disabled={(authCol.ButtonLongNm116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cButtonLongNm116 && touched.cButtonLongNm116 && <span className='form__form-group-error'>{errors.cButtonLongNm116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ScreenId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScreenId116 || {}).ColumnHeader} {(columnLabel.ScreenId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScreenId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScreenId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cScreenId116'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId116', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId116', true)}
                                          onInputChange={this.ScreenId116InputChange()}
                                          value={values.cScreenId116}
                                          defaultSelected={ScreenId116List.filter(obj => { return obj.key === ScreenId116 })}
                                          options={ScreenId116List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ScreenId116 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cScreenId116 && touched.cScreenId116 && <span className='form__form-group-error'>{errors.cScreenId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ReportId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ReportId116 || {}).ColumnHeader} {(columnLabel.ReportId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ReportId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ReportId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cReportId116'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId116', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId116', true)}
                                          onInputChange={this.ReportId116InputChange()}
                                          value={values.cReportId116}
                                          defaultSelected={ReportId116List.filter(obj => { return obj.key === ReportId116 })}
                                          options={ReportId116List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ReportId116 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cReportId116 && touched.cReportId116 && <span className='form__form-group-error'>{errors.cReportId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.WizardId116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.WizardId116 || {}).ColumnHeader} {(columnLabel.WizardId116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.WizardId116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.WizardId116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cWizardId116'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId116', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId116', true)}
                                          onInputChange={this.WizardId116InputChange()}
                                          value={values.cWizardId116}
                                          defaultSelected={WizardId116List.filter(obj => { return obj.key === WizardId116 })}
                                          options={WizardId116List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.WizardId116 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cWizardId116 && touched.cWizardId116 && <span className='form__form-group-error'>{errors.cWizardId116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ButtonToolTip116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ButtonToolTip116 || {}).ColumnHeader} {(columnLabel.ButtonToolTip116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ButtonToolTip116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonToolTip116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cButtonToolTip116'
                                          disabled={(authCol.ButtonToolTip116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cButtonToolTip116 && touched.cButtonToolTip116 && <span className='form__form-group-error'>{errors.cButtonToolTip116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ButtonVisible116 || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cButtonVisible116'
                                        onChange={handleChange}
                                        defaultChecked={values.cButtonVisible116}
                                        disabled={(authCol.ButtonVisible116 || {}).readonly || !(authCol.ButtonVisible116 || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.ButtonVisible116 || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.ButtonVisible116 || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.ButtonVisible116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ButtonVisible116 || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }
                              {(authCol.TopVisible116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.TopVisible116 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TopVisible116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.TopVisible116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.TopVisible116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cTopVisible116'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cTopVisible116')}
                                          value={values.cTopVisible116}
                                          options={TopVisible116List}
                                          placeholder=''
                                          disabled={(authCol.TopVisible116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cTopVisible116 && touched.cTopVisible116 && <span className='form__form-group-error'>{errors.cTopVisible116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.RowVisible116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.RowVisible116 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RowVisible116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.RowVisible116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.RowVisible116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cRowVisible116'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cRowVisible116')}
                                          value={values.cRowVisible116}
                                          options={RowVisible116List}
                                          placeholder=''
                                          disabled={(authCol.RowVisible116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cRowVisible116 && touched.cRowVisible116 && <span className='form__form-group-error'>{errors.cRowVisible116}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.BotVisible116 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.BotVisible116 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.BotVisible116 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.BotVisible116 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.BotVisible116 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmButtonHlpState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cBotVisible116'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cBotVisible116')}
                                          value={values.cBotVisible116}
                                          options={BotVisible116List}
                                          placeholder=''
                                          disabled={(authCol.BotVisible116 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cBotVisible116 && touched.cBotVisible116 && <span className='form__form-group-error'>{errors.cBotVisible116}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ButtonHlpId116)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ButtonHlpId116));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmButtonHlpState) && <Skeleton height='43px' />) ||
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
  AdmButtonHlp: state.AdmButtonHlp,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmButtonHlpReduxObj.LoadPage.bind(AdmButtonHlpReduxObj) },
    { SavePage: AdmButtonHlpReduxObj.SavePage.bind(AdmButtonHlpReduxObj) },
    { DelMst: AdmButtonHlpReduxObj.DelMst.bind(AdmButtonHlpReduxObj) },
    { AddMst: AdmButtonHlpReduxObj.AddMst.bind(AdmButtonHlpReduxObj) },
    { SearchCultureId116: AdmButtonHlpReduxObj.SearchActions.SearchCultureId116.bind(AdmButtonHlpReduxObj) },
    { SearchButtonTypeId116: AdmButtonHlpReduxObj.SearchActions.SearchButtonTypeId116.bind(AdmButtonHlpReduxObj) },
    { SearchScreenId116: AdmButtonHlpReduxObj.SearchActions.SearchScreenId116.bind(AdmButtonHlpReduxObj) },
    { SearchReportId116: AdmButtonHlpReduxObj.SearchActions.SearchReportId116.bind(AdmButtonHlpReduxObj) },
    { SearchWizardId116: AdmButtonHlpReduxObj.SearchActions.SearchWizardId116.bind(AdmButtonHlpReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
