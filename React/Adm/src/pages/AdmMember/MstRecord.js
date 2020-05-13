
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
import AdmMemberReduxObj, { ShowMstFilterApplied } from '../../redux/AdmMember';
import * as AdmMemberService from '../../services/AdmMemberService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmMember || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SredMebrId274';
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

  UserId274InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchUserId274(v, filterBy); } }
  UserId274Change(v, name, values, { setFieldValue, setFieldTouched, forName, _this, blur } = {}) {
    const key = (v || {}).key || v;
    const mstId = (values.cSredMebrId274 || {}).key || values.cSredMebrId274;
    (this || _this).props.GetRefUserId274(mstId, null, key, null)
      .then(ret => {
        ret.dependents.forEach(
          (o => {
            setFieldValue('c' + o.columnName, !ret.result ? null : o.isFileObject ? decodeEmbeddedFileObjectFromServer(ret.result[o.tableColumnName], true) : ret.result[o.tableColumnName]);
          })
        )
      })
  }

  MemberId274InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchMemberId274(v, filterBy); } }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmMember || {}).ColumnLabel || {};
    /* standard field validation */
    if (isEmptyId((values.cUserId274 || {}).value)) { errors.cUserId274 = (columnLabel.UserId274 || {}).ErrMessage; }
    if (!values.cMnSalary274) { errors.cMnSalary274 = (columnLabel.MnSalary274 || {}).ErrMessage; }
    if (!values.cMnNtxBenefit274) { errors.cMnNtxBenefit274 = (columnLabel.MnNtxBenefit274 || {}).ErrMessage; }
    if (!values.cMnTaxBenefit274) { errors.cMnTaxBenefit274 = (columnLabel.MnTaxBenefit274 || {}).ErrMessage; }
    if (!values.cMnWorkHours274) { errors.cMnWorkHours274 = (columnLabel.MnWorkHours274 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmMember || {}).Mst || {};

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
        this.props.AdmMember,
        {
          SredMebrId274: values.cSredMebrId274 || '',
          UserId274: (values.cUserId274 || {}).value || '',
          PicMed275: values.cPicMed275 && values.cPicMed275.ts ?
            JSON.stringify({
              ...values.cPicMed275,
              ts: undefined,
              lastTS: values.cPicMed275.ts,
              base64: this.StripEmbeddedBase64Prefix(values.cPicMed275.base64)
            }) : null,
          MemberId274: (values.cMemberId274 || {}).value || '',
          MemberTitle274: values.cMemberTitle274 || '',
          LT10PercShare274: values.cLT10PercShare274 ? 'Y' : 'N',
          MnSalary274: values.cMnSalary274 || '',
          MnNtxBenefit274: values.cMnNtxBenefit274 || '',
          MnTaxBenefit274: values.cMnTaxBenefit274 || '',
          MnWorkHours274: values.cMnWorkHours274 || '',
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
    const AdmMemberState = this.props.AdmMember || {};
    const auxSystemLabels = AdmMemberState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SredMebrId274;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmMember.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmMember.Label);
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
    const AdmMemberState = this.props.AdmMember || {};
    const auxSystemLabels = AdmMemberState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SredMebrId274;
        this.props.DelMst(this.props.AdmMember, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmMember || {};
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
    const AdmMemberState = this.props.AdmMember || {};
    const auxSystemLabels = AdmMemberState.SystemLabel || {};
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
      if (!(this.props.AdmMember || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmMember || {};

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
    const AdmMemberState = this.props.AdmMember || {};

    if (AdmMemberState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmMemberState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmMemberReduxObj.GetScreenButtons(AdmMemberState) || {};
    const itemList = AdmMemberState.Dtl || [];
    const auxLabels = AdmMemberState.Label || {};
    const auxSystemLabels = AdmMemberState.SystemLabel || {};

    const columnLabel = AdmMemberState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmMemberState);
    const authRow = (AdmMemberState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmMember || {}).Mst || {});
    const currDtl = ((this.props.AdmMember || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.SredMebrId274));
    const selectList = AdmMemberReduxObj.SearchListToSelectList(AdmMemberState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const SredMebrId274 = currMst.SredMebrId274;
    const UserId274List = AdmMemberReduxObj.ScreenDdlSelectors.UserId274(AdmMemberState);
    const UserId274 = currMst.UserId274;
    const PicMed275 = currMst.PicMed275 ? decodeEmbeddedFileObjectFromServer(currMst.PicMed275) : null;
    const PicMed275FileUploadOptions = {
      CancelFileButton: auxSystemLabels.CancelFileBtnLabel,
      DeleteFileButton: auxSystemLabels.DeleteFileBtnLabel,
      MaxImageSize: {
        Width: (columnLabel.PicMed275 || {}).ResizeWidth,
        Height: (columnLabel.PicMed275 || {}).ResizeHeight,
      },
      MinImageSize: {
        Width: (columnLabel.PicMed275 || {}).ColumnSize,
        Height: (columnLabel.PicMed275 || {}).ColumnHeight,
      },
    }
    const MemberId274List = AdmMemberReduxObj.ScreenDdlSelectors.MemberId274(AdmMemberState);
    const MemberId274 = currMst.MemberId274;
    const MemberTitle274 = currMst.MemberTitle274;
    const LT10PercShare274 = currMst.LT10PercShare274;
    const MnSalary274 = currMst.MnSalary274;
    const MnNtxBenefit274 = currMst.MnNtxBenefit274;
    const MnTaxBenefit274 = currMst.MnTaxBenefit274;
    const MnWorkHours274 = currMst.MnWorkHours274;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmMemberState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cSredMebrId274: formatContent(SredMebrId274 || '', 'TextBox'),
                    cUserId274: UserId274List.filter(obj => { return obj.key === UserId274 })[0],
                    cPicMed275: PicMed275,
                    cMemberId274: MemberId274List.filter(obj => { return obj.key === MemberId274 })[0],
                    cMemberTitle274: formatContent(MemberTitle274 || '', 'TextBox'),
                    cLT10PercShare274: LT10PercShare274 === 'Y',
                    cMnSalary274: formatContent(MnSalary274 || '', 'Money'),
                    cMnNtxBenefit274: formatContent(MnNtxBenefit274 || '', 'Money'),
                    cMnTaxBenefit274: formatContent(MnTaxBenefit274 || '', 'Money'),
                    cMnWorkHours274: formatContent(MnWorkHours274 || '', 'Money'),
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
                                {(this.constructor.ShowSpinner(AdmMemberState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredMebrId274)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredMebrId274)) return null;
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
                              {(authCol.SredMebrId274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SredMebrId274 || {}).ColumnHeader} {(columnLabel.SredMebrId274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SredMebrId274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SredMebrId274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cSredMebrId274'
                                          disabled={(authCol.SredMebrId274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSredMebrId274 && touched.cSredMebrId274 && <span className='form__form-group-error'>{errors.cSredMebrId274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.UserId274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UserId274 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.UserId274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UserId274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UserId274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cUserId274'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cUserId274', false, values, [this.UserId274Change])}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cUserId274', true)}
                                          onInputChange={this.UserId274InputChange()}
                                          value={values.cUserId274}
                                          defaultSelected={UserId274List.filter(obj => { return obj.key === UserId274 })}
                                          options={UserId274List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.UserId274 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cUserId274 && touched.cUserId274 && <span className='form__form-group-error'>{errors.cUserId274}</span>}
                                  </div>
                                </Col>
                              }

                              {(authCol.PicMed275 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.PicMed275 || {}).ColumnHeader} {(columnLabel.PicMed275 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.PicMed275 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.PicMed275 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component={FileInputField}
                                          name='cPicMed275'
                                          options={{ ...fileFileUploadOptions, maxFileCount: 1 }}
                                          files={(this.BindFileObject(PicMed275, values.cPicMed275) || []).filter(f => !f.isEmptyFileObject)}
                                          label={(columnLabel.PicMed275 || {}).ToolTip}
                                          disabled={true}
                                        />
                                      </div>
                                    }
                                    {errors.cPicMed275 && touched.cPicMed275 && <span className='form__form-group-error'>{errors.cPicMed275}</span>}
                                  </div>
                                </Col>
                              }

                              {(authCol.MemberId274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MemberId274 || {}).ColumnHeader} {(columnLabel.MemberId274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MemberId274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MemberId274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cMemberId274'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId274', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId274', true)}
                                          onInputChange={this.MemberId274InputChange()}
                                          value={values.cMemberId274}
                                          defaultSelected={MemberId274List.filter(obj => { return obj.key === MemberId274 })}
                                          options={MemberId274List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.MemberId274 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cMemberId274 && touched.cMemberId274 && <span className='form__form-group-error'>{errors.cMemberId274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MemberTitle274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MemberTitle274 || {}).ColumnHeader} {(columnLabel.MemberTitle274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MemberTitle274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MemberTitle274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMemberTitle274'
                                          disabled={(authCol.MemberTitle274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMemberTitle274 && touched.cMemberTitle274 && <span className='form__form-group-error'>{errors.cMemberTitle274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.LT10PercShare274 || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cLT10PercShare274'
                                        onChange={handleChange}
                                        defaultChecked={values.cLT10PercShare274}
                                        disabled={(authCol.LT10PercShare274 || {}).readonly || !(authCol.LT10PercShare274 || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.LT10PercShare274 || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.LT10PercShare274 || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.LT10PercShare274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.LT10PercShare274 || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }
                              {(authCol.MnSalary274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MnSalary274 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MnSalary274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MnSalary274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MnSalary274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMnSalary274'
                                          disabled={(authCol.MnSalary274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMnSalary274 && touched.cMnSalary274 && <span className='form__form-group-error'>{errors.cMnSalary274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MnNtxBenefit274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MnNtxBenefit274 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MnNtxBenefit274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MnNtxBenefit274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MnNtxBenefit274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMnNtxBenefit274'
                                          disabled={(authCol.MnNtxBenefit274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMnNtxBenefit274 && touched.cMnNtxBenefit274 && <span className='form__form-group-error'>{errors.cMnNtxBenefit274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MnTaxBenefit274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MnTaxBenefit274 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MnTaxBenefit274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MnTaxBenefit274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MnTaxBenefit274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMnTaxBenefit274'
                                          disabled={(authCol.MnTaxBenefit274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMnTaxBenefit274 && touched.cMnTaxBenefit274 && <span className='form__form-group-error'>{errors.cMnTaxBenefit274}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MnWorkHours274 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MnWorkHours274 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MnWorkHours274 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MnWorkHours274 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MnWorkHours274 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmMemberState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMnWorkHours274'
                                          disabled={(authCol.MnWorkHours274 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMnWorkHours274 && touched.cMnWorkHours274 && <span className='form__form-group-error'>{errors.cMnWorkHours274}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SredMebrId274)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SredMebrId274));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmMemberState) && <Skeleton height='43px' />) ||
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
  AdmMember: state.AdmMember,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmMemberReduxObj.LoadPage.bind(AdmMemberReduxObj) },
    { SavePage: AdmMemberReduxObj.SavePage.bind(AdmMemberReduxObj) },
    { DelMst: AdmMemberReduxObj.DelMst.bind(AdmMemberReduxObj) },
    { AddMst: AdmMemberReduxObj.AddMst.bind(AdmMemberReduxObj) },
    { SearchUserId274: AdmMemberReduxObj.SearchActions.SearchUserId274.bind(AdmMemberReduxObj) },
    { GetRefUserId274: AdmMemberReduxObj.SearchActions.GetRefUserId274.bind(AdmMemberReduxObj) },
    { SearchMemberId274: AdmMemberReduxObj.SearchActions.SearchMemberId274.bind(AdmMemberReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
