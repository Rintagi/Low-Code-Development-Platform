
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
import AdmServerRuleOvrdReduxObj, { ShowMstFilterApplied } from '../../redux/AdmServerRuleOvrd';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmServerRuleOvrd || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'AtServerRuleOvrdId1322';
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

  ServerRuleId1322InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchServerRuleId1322(v, filterBy); } }
  ScreenId1322InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchScreenId1322(v, filterBy); } }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmServerRuleOvrd || {}).ColumnLabel || {};
    /* standard field validation */
    if (!values.cServerRuleOvrdName1322) { errors.cServerRuleOvrdName1322 = (columnLabel.ServerRuleOvrdName1322 || {}).ErrMessage; }
    if (isEmptyId((values.cServerRuleId1322 || {}).value)) { errors.cServerRuleId1322 = (columnLabel.ServerRuleId1322 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmServerRuleOvrd || {}).Mst || {};

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
        this.props.AdmServerRuleOvrd,
        {
          AtServerRuleOvrdId1322: values.cAtServerRuleOvrdId1322 || '',
          ServerRuleOvrdDesc1322: values.cServerRuleOvrdDesc1322 || '',
          ServerRuleOvrdName1322: values.cServerRuleOvrdName1322 || '',
          ServerRuleId1322: (values.cServerRuleId1322 || {}).value || '',
          ScreenId1322: (values.cScreenId1322 || {}).value || '',
          Priority1322: values.cPriority1322 || '',
          Disable1322: values.cDisable1322 ? 'Y' : 'N',
          RunMode1322: (values.cRunMode1322 || {}).value || '',
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
    const AdmServerRuleOvrdState = this.props.AdmServerRuleOvrd || {};
    const auxSystemLabels = AdmServerRuleOvrdState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).AtServerRuleOvrdId1322;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmServerRuleOvrd.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmServerRuleOvrd.Label);
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
    const AdmServerRuleOvrdState = this.props.AdmServerRuleOvrd || {};
    const auxSystemLabels = AdmServerRuleOvrdState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.AtServerRuleOvrdId1322;
        this.props.DelMst(this.props.AdmServerRuleOvrd, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmServerRuleOvrd || {};
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
    const AdmServerRuleOvrdState = this.props.AdmServerRuleOvrd || {};
    const auxSystemLabels = AdmServerRuleOvrdState.SystemLabel || {};
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
      if (!(this.props.AdmServerRuleOvrd || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmServerRuleOvrd || {};

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
    const AdmServerRuleOvrdState = this.props.AdmServerRuleOvrd || {};

    if (AdmServerRuleOvrdState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmServerRuleOvrdState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmServerRuleOvrdReduxObj.GetScreenButtons(AdmServerRuleOvrdState) || {};
    const itemList = AdmServerRuleOvrdState.Dtl || [];
    const auxLabels = AdmServerRuleOvrdState.Label || {};
    const auxSystemLabels = AdmServerRuleOvrdState.SystemLabel || {};

    const columnLabel = AdmServerRuleOvrdState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmServerRuleOvrdState);
    const authRow = (AdmServerRuleOvrdState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmServerRuleOvrd || {}).Mst || {});
    const currDtl = ((this.props.AdmServerRuleOvrd || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.AtServerRuleOvrdId1322));
    const selectList = AdmServerRuleOvrdReduxObj.SearchListToSelectList(AdmServerRuleOvrdState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const AtServerRuleOvrdId1322 = currMst.AtServerRuleOvrdId1322;
    const ServerRuleOvrdDesc1322 = currMst.ServerRuleOvrdDesc1322;
    const ServerRuleOvrdName1322 = currMst.ServerRuleOvrdName1322;
    const ServerRuleId1322List = AdmServerRuleOvrdReduxObj.ScreenDdlSelectors.ServerRuleId1322(AdmServerRuleOvrdState);
    const ServerRuleId1322 = currMst.ServerRuleId1322;
    const ScreenId1322List = AdmServerRuleOvrdReduxObj.ScreenDdlSelectors.ScreenId1322(AdmServerRuleOvrdState);
    const ScreenId1322 = currMst.ScreenId1322;
    const Priority1322 = currMst.Priority1322;
    const Disable1322 = currMst.Disable1322;
    const RunMode1322List = AdmServerRuleOvrdReduxObj.ScreenDdlSelectors.RunMode1322(AdmServerRuleOvrdState);
    const RunMode1322 = currMst.RunMode1322;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmServerRuleOvrdState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cAtServerRuleOvrdId1322: formatContent(AtServerRuleOvrdId1322 || '', 'TextBox'),
                    cServerRuleOvrdDesc1322: formatContent(ServerRuleOvrdDesc1322 || '', 'TextBox'),
                    cServerRuleOvrdName1322: formatContent(ServerRuleOvrdName1322 || '', 'TextBox'),
                    cServerRuleId1322: ServerRuleId1322List.filter(obj => { return obj.key === ServerRuleId1322 })[0],
                    cScreenId1322: ScreenId1322List.filter(obj => { return obj.key === ScreenId1322 })[0],
                    cPriority1322: formatContent(Priority1322 || '', 'TextBox'),
                    cDisable1322: Disable1322 === 'Y',
                    cRunMode1322: RunMode1322List.filter(obj => { return obj.key === RunMode1322 })[0],
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
                                {(this.constructor.ShowSpinner(AdmServerRuleOvrdState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AtServerRuleOvrdId1322)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AtServerRuleOvrdId1322)) return null;
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
                              {(authCol.AtServerRuleOvrdId1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.AtServerRuleOvrdId1322 || {}).ColumnHeader} {(columnLabel.AtServerRuleOvrdId1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.AtServerRuleOvrdId1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.AtServerRuleOvrdId1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cAtServerRuleOvrdId1322'
                                          disabled={(authCol.AtServerRuleOvrdId1322 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cAtServerRuleOvrdId1322 && touched.cAtServerRuleOvrdId1322 && <span className='form__form-group-error'>{errors.cAtServerRuleOvrdId1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ServerRuleOvrdDesc1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ServerRuleOvrdDesc1322 || {}).ColumnHeader} {(columnLabel.ServerRuleOvrdDesc1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ServerRuleOvrdDesc1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ServerRuleOvrdDesc1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cServerRuleOvrdDesc1322'
                                          disabled={(authCol.ServerRuleOvrdDesc1322 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cServerRuleOvrdDesc1322 && touched.cServerRuleOvrdDesc1322 && <span className='form__form-group-error'>{errors.cServerRuleOvrdDesc1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ServerRuleOvrdName1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ServerRuleOvrdName1322 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ServerRuleOvrdName1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ServerRuleOvrdName1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ServerRuleOvrdName1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cServerRuleOvrdName1322'
                                          disabled={(authCol.ServerRuleOvrdName1322 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cServerRuleOvrdName1322 && touched.cServerRuleOvrdName1322 && <span className='form__form-group-error'>{errors.cServerRuleOvrdName1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ServerRuleId1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ServerRuleId1322 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ServerRuleId1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ServerRuleId1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ServerRuleId1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cServerRuleId1322'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cServerRuleId1322', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cServerRuleId1322', true)}
                                          onInputChange={this.ServerRuleId1322InputChange()}
                                          value={values.cServerRuleId1322}
                                          defaultSelected={ServerRuleId1322List.filter(obj => { return obj.key === ServerRuleId1322 })}
                                          options={ServerRuleId1322List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ServerRuleId1322 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cServerRuleId1322 && touched.cServerRuleId1322 && <span className='form__form-group-error'>{errors.cServerRuleId1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ScreenId1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScreenId1322 || {}).ColumnHeader} {(columnLabel.ScreenId1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScreenId1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScreenId1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cScreenId1322'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId1322', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId1322', true)}
                                          onInputChange={this.ScreenId1322InputChange()}
                                          value={values.cScreenId1322}
                                          defaultSelected={ScreenId1322List.filter(obj => { return obj.key === ScreenId1322 })}
                                          options={ScreenId1322List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ScreenId1322 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cScreenId1322 && touched.cScreenId1322 && <span className='form__form-group-error'>{errors.cScreenId1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Priority1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Priority1322 || {}).ColumnHeader} {(columnLabel.Priority1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Priority1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Priority1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cPriority1322'
                                          disabled={(authCol.Priority1322 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cPriority1322 && touched.cPriority1322 && <span className='form__form-group-error'>{errors.cPriority1322}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Disable1322 || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cDisable1322'
                                        onChange={handleChange}
                                        defaultChecked={values.cDisable1322}
                                        disabled={(authCol.Disable1322 || {}).readonly || !(authCol.Disable1322 || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.Disable1322 || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.Disable1322 || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.Disable1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Disable1322 || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }
                              {(authCol.RunMode1322 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.RunMode1322 || {}).ColumnHeader} {(columnLabel.RunMode1322 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.RunMode1322 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.RunMode1322 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmServerRuleOvrdState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cRunMode1322'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cRunMode1322')}
                                          value={values.cRunMode1322}
                                          options={RunMode1322List}
                                          placeholder=''
                                          disabled={(authCol.RunMode1322 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cRunMode1322 && touched.cRunMode1322 && <span className='form__form-group-error'>{errors.cRunMode1322}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AtServerRuleOvrdId1322)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).AtServerRuleOvrdId1322));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmServerRuleOvrdState) && <Skeleton height='43px' />) ||
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
  AdmServerRuleOvrd: state.AdmServerRuleOvrd,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmServerRuleOvrdReduxObj.LoadPage.bind(AdmServerRuleOvrdReduxObj) },
    { SavePage: AdmServerRuleOvrdReduxObj.SavePage.bind(AdmServerRuleOvrdReduxObj) },
    { DelMst: AdmServerRuleOvrdReduxObj.DelMst.bind(AdmServerRuleOvrdReduxObj) },
    { AddMst: AdmServerRuleOvrdReduxObj.AddMst.bind(AdmServerRuleOvrdReduxObj) },
    { SearchServerRuleId1322: AdmServerRuleOvrdReduxObj.SearchActions.SearchServerRuleId1322.bind(AdmServerRuleOvrdReduxObj) },
    { SearchScreenId1322: AdmServerRuleOvrdReduxObj.SearchActions.SearchScreenId1322.bind(AdmServerRuleOvrdReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
