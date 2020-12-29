
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
import AdmSredTimeReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSredTime';
import * as AdmSredTimeService from '../../services/AdmSredTimeService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmSredTime || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SredTimeId272';
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

  MemberId272InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchMemberId272(v, filterBy); } }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmSredTime || {}).ColumnLabel || {};
    /* standard field validation */
    if (isEmptyId((values.cMemberId272 || {}).value)) { errors.cMemberId272 = (columnLabel.MemberId272 || {}).ErrMessage; }
    if (!values.cSredTimeDt272) { errors.cSredTimeDt272 = (columnLabel.SredTimeDt272 || {}).ErrMessage; }
    if (!values.cHourSpent272) { errors.cHourSpent272 = (columnLabel.HourSpent272 || {}).ErrMessage; }
    if (!values.cAccomplished272) { errors.cAccomplished272 = (columnLabel.Accomplished272 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmSredTime || {}).Mst || {};

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
        this.props.AdmSredTime,
        {
          SredTimeId272: values.cSredTimeId272 || '',
          MemberId272: (values.cMemberId272 || {}).value || '',
          SredTimeDt272: values.cSredTimeDt272 || '',
          HourSpent272: values.cHourSpent272 || '',
          EnteredBy272: (values.cEnteredBy272 || {}).value || '',
          EnteredOn272: values.cEnteredOn272 || '',
          ModifiedBy272: (values.cModifiedBy272 || {}).value || '',
          ModifiedOn272: values.cModifiedOn272 || '',
          Accomplished272: values.cAccomplished272 || '',
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
    const AdmSredTimeState = this.props.AdmSredTime || {};
    const auxSystemLabels = AdmSredTimeState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SredTimeId272;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmSredTime.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmSredTime.Label);
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
    const AdmSredTimeState = this.props.AdmSredTime || {};
    const auxSystemLabels = AdmSredTimeState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SredTimeId272;
        this.props.DelMst(this.props.AdmSredTime, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmSredTime || {};
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
    const AdmSredTimeState = this.props.AdmSredTime || {};
    const auxSystemLabels = AdmSredTimeState.SystemLabel || {};
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
      if (!(this.props.AdmSredTime || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmSredTime || {};

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
    const AdmSredTimeState = this.props.AdmSredTime || {};

    if (AdmSredTimeState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmSredTimeState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmSredTimeReduxObj.GetScreenButtons(AdmSredTimeState) || {};
    const itemList = AdmSredTimeState.Dtl || [];
    const auxLabels = AdmSredTimeState.Label || {};
    const auxSystemLabels = AdmSredTimeState.SystemLabel || {};

    const columnLabel = AdmSredTimeState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmSredTimeState);
    const authRow = (AdmSredTimeState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmSredTime || {}).Mst || {});
    const currDtl = ((this.props.AdmSredTime || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.SredTimeId272));
    const selectList = AdmSredTimeReduxObj.SearchListToSelectList(AdmSredTimeState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const SredTimeId272 = currMst.SredTimeId272;
    const MemberId272List = AdmSredTimeReduxObj.ScreenDdlSelectors.MemberId272(AdmSredTimeState);
    const MemberId272 = currMst.MemberId272;
    const SredTimeDt272 = currMst.SredTimeDt272;
    const HourSpent272 = currMst.HourSpent272;
    const EnteredBy272List = AdmSredTimeReduxObj.ScreenDdlSelectors.EnteredBy272(AdmSredTimeState);
    const EnteredBy272 = currMst.EnteredBy272;
    const EnteredOn272 = currMst.EnteredOn272;
    const ModifiedBy272List = AdmSredTimeReduxObj.ScreenDdlSelectors.ModifiedBy272(AdmSredTimeState);
    const ModifiedBy272 = currMst.ModifiedBy272;
    const ModifiedOn272 = currMst.ModifiedOn272;
    const Accomplished272 = currMst.Accomplished272;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmSredTimeState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cSredTimeId272: formatContent(SredTimeId272 || '', 'TextBox'),
                    cMemberId272: MemberId272List.filter(obj => { return obj.key === MemberId272 })[0],
                    cSredTimeDt272: SredTimeDt272 || new Date(),
                    cHourSpent272: formatContent(HourSpent272 || '', 'Money'),
                    cEnteredBy272: EnteredBy272List.filter(obj => { return obj.key === EnteredBy272 })[0],
                    cEnteredOn272: EnteredOn272 || new Date(),
                    cModifiedBy272: ModifiedBy272List.filter(obj => { return obj.key === ModifiedBy272 })[0],
                    cModifiedOn272: ModifiedOn272 || new Date(),
                    cAccomplished272: formatContent(Accomplished272 || '', 'TextBox'),
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
                                {(this.constructor.ShowSpinner(AdmSredTimeState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredTimeId272)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredTimeId272)) return null;
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
                              {(authCol.SredTimeId272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SredTimeId272 || {}).ColumnHeader} {(columnLabel.SredTimeId272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SredTimeId272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SredTimeId272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cSredTimeId272'
                                          disabled={(authCol.SredTimeId272 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSredTimeId272 && touched.cSredTimeId272 && <span className='form__form-group-error'>{errors.cSredTimeId272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MemberId272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MemberId272 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MemberId272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MemberId272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MemberId272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cMemberId272'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId272', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId272', true)}
                                          onInputChange={this.MemberId272InputChange()}
                                          value={values.cMemberId272}
                                          defaultSelected={MemberId272List.filter(obj => { return obj.key === MemberId272 })}
                                          options={MemberId272List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.MemberId272 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cMemberId272 && touched.cMemberId272 && <span className='form__form-group-error'>{errors.cMemberId272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.SredTimeDt272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SredTimeDt272 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SredTimeDt272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SredTimeDt272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SredTimeDt272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DatePicker
                                          name='cSredTimeDt272'
                                          onChange={this.DateChange(setFieldValue, setFieldTouched, 'cSredTimeDt272', false)}
                                          onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cSredTimeDt272', true)}
                                          value={values.cSredTimeDt272}
                                          selected={values.cSredTimeDt272}
                                          disabled={(authCol.SredTimeDt272 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cSredTimeDt272 && touched.cSredTimeDt272 && <span className='form__form-group-error'>{errors.cSredTimeDt272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.HourSpent272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.HourSpent272 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.HourSpent272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.HourSpent272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.HourSpent272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      (<div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cHourSpent272'
                                          disabled={(authCol.HourSpent272 || {}).readonly ? 'disabled' : ''} />
                                      </div>)
                                    }
                                    {errors.cHourSpent272 && touched.cHourSpent272 && <span className='form__form-group-error'>{errors.cHourSpent272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.EnteredBy272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.EnteredBy272 || {}).ColumnHeader} {(columnLabel.EnteredBy272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.EnteredBy272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.EnteredBy272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cEnteredBy272'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cEnteredBy272')}
                                          value={values.cEnteredBy272}
                                          options={EnteredBy272List}
                                          placeholder=''
                                          disabled={(authCol.EnteredBy272 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cEnteredBy272 && touched.cEnteredBy272 && <span className='form__form-group-error'>{errors.cEnteredBy272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.EnteredOn272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.EnteredOn272 || {}).ColumnHeader} {(columnLabel.EnteredOn272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.EnteredOn272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.EnteredOn272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DatePicker
                                          name='cEnteredOn272'
                                          onChange={this.DateChange(setFieldValue, setFieldTouched, 'cEnteredOn272', false)}
                                          onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cEnteredOn272', true)}
                                          value={values.cEnteredOn272}
                                          selected={values.cEnteredOn272}
                                          disabled={(authCol.EnteredOn272 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cEnteredOn272 && touched.cEnteredOn272 && <span className='form__form-group-error'>{errors.cEnteredOn272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ModifiedBy272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ModifiedBy272 || {}).ColumnHeader} {(columnLabel.ModifiedBy272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ModifiedBy272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ModifiedBy272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cModifiedBy272'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cModifiedBy272')}
                                          value={values.cModifiedBy272}
                                          options={ModifiedBy272List}
                                          placeholder=''
                                          disabled={(authCol.ModifiedBy272 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cModifiedBy272 && touched.cModifiedBy272 && <span className='form__form-group-error'>{errors.cModifiedBy272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ModifiedOn272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ModifiedOn272 || {}).ColumnHeader} {(columnLabel.ModifiedOn272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ModifiedOn272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ModifiedOn272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DatePicker
                                          name='cModifiedOn272'
                                          onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn272', false)}
                                          onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn272', true)}
                                          value={values.cModifiedOn272}
                                          selected={values.cModifiedOn272}
                                          disabled={(authCol.ModifiedOn272 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cModifiedOn272 && touched.cModifiedOn272 && <span className='form__form-group-error'>{errors.cModifiedOn272}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.Accomplished272 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.Accomplished272 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.Accomplished272 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.Accomplished272 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.Accomplished272 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmSredTimeState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cAccomplished272'
                                          disabled={(authCol.Accomplished272 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cAccomplished272 && touched.cAccomplished272 && <span className='form__form-group-error'>{errors.cAccomplished272}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredTimeId272)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SredTimeId272));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmSredTimeState) && <Skeleton height='43px' />) ||
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
  AdmSredTime: state.AdmSredTime,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSredTimeReduxObj.LoadPage.bind(AdmSredTimeReduxObj) },
    { SavePage: AdmSredTimeReduxObj.SavePage.bind(AdmSredTimeReduxObj) },
    { DelMst: AdmSredTimeReduxObj.DelMst.bind(AdmSredTimeReduxObj) },
    { AddMst: AdmSredTimeReduxObj.AddMst.bind(AdmSredTimeReduxObj) },
    { SearchMemberId272: AdmSredTimeReduxObj.SearchActions.SearchMemberId272.bind(AdmSredTimeReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
