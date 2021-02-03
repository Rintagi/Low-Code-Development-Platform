
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
import AdmScrAuditDtlReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScrAuditDtl';
import * as AdmScrAuditDtlService from '../../services/AdmScrAuditDtlService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmScrAuditDtl || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ScrAuditDtlId1301';
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


  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmScrAuditDtl || {}).ColumnLabel || {};
    /* standard field validation */
    if (!values.cScreenObjId1301) { errors.cScreenObjId1301 = (columnLabel.ScreenObjId1301 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmScrAuditDtl || {}).Mst || {};

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
        this.props.AdmScrAuditDtl,
        {
          ScrAuditDtlId1301: values.cScrAuditDtlId1301 || '',
          ScrAuditId1301: values.cScrAuditId1301 || '',
          ScreenObjId1301: values.cScreenObjId1301 || '',
          ScreenObjDesc1301: values.cScreenObjDesc1301 || '',
          ColumnId1301: values.cColumnId1301 || '',
          ColumnDesc1301: values.cColumnDesc1301 || '',
          ChangedFr1301: values.cChangedFr1301 || '',
          ChangedTo1301: values.cChangedTo1301 || '',
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
    const AdmScrAuditDtlState = this.props.AdmScrAuditDtl || {};
    const auxSystemLabels = AdmScrAuditDtlState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ScrAuditDtlId1301;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmScrAuditDtl.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmScrAuditDtl.Label);
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
    const AdmScrAuditDtlState = this.props.AdmScrAuditDtl || {};
    const auxSystemLabels = AdmScrAuditDtlState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ScrAuditDtlId1301;
        this.props.DelMst(this.props.AdmScrAuditDtl, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmScrAuditDtl || {};
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
    const AdmScrAuditDtlState = this.props.AdmScrAuditDtl || {};
    const auxSystemLabels = AdmScrAuditDtlState.SystemLabel || {};
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
      if (!(this.props.AdmScrAuditDtl || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScrAuditDtl || {};

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
    const AdmScrAuditDtlState = this.props.AdmScrAuditDtl || {};

    if (AdmScrAuditDtlState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmScrAuditDtlState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmScrAuditDtlReduxObj.GetScreenButtons(AdmScrAuditDtlState) || {};
    const itemList = AdmScrAuditDtlState.Dtl || [];
    const auxLabels = AdmScrAuditDtlState.Label || {};
    const auxSystemLabels = AdmScrAuditDtlState.SystemLabel || {};

    const columnLabel = AdmScrAuditDtlState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmScrAuditDtlState);
    const authRow = (AdmScrAuditDtlState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmScrAuditDtl || {}).Mst || {});
    const currDtl = ((this.props.AdmScrAuditDtl || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.ScrAuditDtlId1301));
    const selectList = AdmScrAuditDtlReduxObj.SearchListToSelectList(AdmScrAuditDtlState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const ScrAuditDtlId1301 = currMst.ScrAuditDtlId1301;
    const ScrAuditId1301 = currMst.ScrAuditId1301;
    const ScreenObjId1301 = currMst.ScreenObjId1301;
    const ScreenObjDesc1301 = currMst.ScreenObjDesc1301;
    const ColumnId1301 = currMst.ColumnId1301;
    const ColumnDesc1301 = currMst.ColumnDesc1301;
    const ChangedFr1301 = currMst.ChangedFr1301;
    const ChangedTo1301 = currMst.ChangedTo1301;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmScrAuditDtlState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cScrAuditDtlId1301: formatContent(ScrAuditDtlId1301 || '', 'TextBox'),
                    cScrAuditId1301: formatContent(ScrAuditId1301 || '', 'TextBox'),
                    cScreenObjId1301: formatContent(ScreenObjId1301 || '', 'TextBox'),
                    cScreenObjDesc1301: formatContent(ScreenObjDesc1301 || '', 'TextBox'),
                    cColumnId1301: formatContent(ColumnId1301 || '', 'TextBox'),
                    cColumnDesc1301: formatContent(ColumnDesc1301 || '', 'TextBox'),
                    cChangedFr1301: formatContent(ChangedFr1301 || '', 'TextBox'),
                    cChangedTo1301: formatContent(ChangedTo1301 || '', 'TextBox'),
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
                                {(this.constructor.ShowSpinner(AdmScrAuditDtlState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScrAuditDtlId1301)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScrAuditDtlId1301)) return null;
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
                              {(authCol.ScrAuditDtlId1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScrAuditDtlId1301 || {}).ColumnHeader} {(columnLabel.ScrAuditDtlId1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScrAuditDtlId1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScrAuditDtlId1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cScrAuditDtlId1301'
                                          disabled={(authCol.ScrAuditDtlId1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cScrAuditDtlId1301 && touched.cScrAuditDtlId1301 && <span className='form__form-group-error'>{errors.cScrAuditDtlId1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ScrAuditId1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScrAuditId1301 || {}).ColumnHeader} {(columnLabel.ScrAuditId1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScrAuditId1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScrAuditId1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cScrAuditId1301'
                                          disabled={(authCol.ScrAuditId1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cScrAuditId1301 && touched.cScrAuditId1301 && <span className='form__form-group-error'>{errors.cScrAuditId1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ScreenObjId1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScreenObjId1301 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenObjId1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScreenObjId1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScreenObjId1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cScreenObjId1301'
                                          disabled={(authCol.ScreenObjId1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cScreenObjId1301 && touched.cScreenObjId1301 && <span className='form__form-group-error'>{errors.cScreenObjId1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ScreenObjDesc1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ScreenObjDesc1301 || {}).ColumnHeader} {(columnLabel.ScreenObjDesc1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ScreenObjDesc1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ScreenObjDesc1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cScreenObjDesc1301'
                                          disabled={(authCol.ScreenObjDesc1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cScreenObjDesc1301 && touched.cScreenObjDesc1301 && <span className='form__form-group-error'>{errors.cScreenObjDesc1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ColumnId1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ColumnId1301 || {}).ColumnHeader} {(columnLabel.ColumnId1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ColumnId1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ColumnId1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cColumnId1301'
                                          disabled={(authCol.ColumnId1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cColumnId1301 && touched.cColumnId1301 && <span className='form__form-group-error'>{errors.cColumnId1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ColumnDesc1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ColumnDesc1301 || {}).ColumnHeader} {(columnLabel.ColumnDesc1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ColumnDesc1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ColumnDesc1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cColumnDesc1301'
                                          disabled={(authCol.ColumnDesc1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cColumnDesc1301 && touched.cColumnDesc1301 && <span className='form__form-group-error'>{errors.cColumnDesc1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ChangedFr1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ChangedFr1301 || {}).ColumnHeader} {(columnLabel.ChangedFr1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ChangedFr1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ChangedFr1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cChangedFr1301'
                                          disabled={(authCol.ChangedFr1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cChangedFr1301 && touched.cChangedFr1301 && <span className='form__form-group-error'>{errors.cChangedFr1301}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ChangedTo1301 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ChangedTo1301 || {}).ColumnHeader} {(columnLabel.ChangedTo1301 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ChangedTo1301 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ChangedTo1301 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmScrAuditDtlState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cChangedTo1301'
                                          disabled={(authCol.ChangedTo1301 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cChangedTo1301 && touched.cChangedTo1301 && <span className='form__form-group-error'>{errors.cChangedTo1301}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScrAuditDtlId1301)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ScrAuditDtlId1301));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmScrAuditDtlState) && <Skeleton height='43px' />) ||
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
  AdmScrAuditDtl: state.AdmScrAuditDtl,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScrAuditDtlReduxObj.LoadPage.bind(AdmScrAuditDtlReduxObj) },
    { SavePage: AdmScrAuditDtlReduxObj.SavePage.bind(AdmScrAuditDtlReduxObj) },
    { DelMst: AdmScrAuditDtlReduxObj.DelMst.bind(AdmScrAuditDtlReduxObj) },
    { AddMst: AdmScrAuditDtlReduxObj.AddMst.bind(AdmScrAuditDtlReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
