
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
import AdmScreenReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScreen';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreen || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'ScreenId15';
    this.DtlKeyColumnName = 'ScreenHlpId16';
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

CultureId16InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureId16(v, filterBy);}}
/* ReactRule: Detail Record Custom Function */
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmScreen || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
if (isEmptyId((values.cCultureId16 || {}).value)) { errors.cCultureId16 = (columnLabel.CultureId16 || {}).ErrMessage;}
if (!values.cScreenTitle16) { errors.cScreenTitle16 = (columnLabel.ScreenTitle16 || {}).ErrMessage;}
if (!values.cDefaultHlpMsg16) { errors.cDefaultHlpMsg16 = (columnLabel.DefaultHlpMsg16 || {}).ErrMessage;}
if (!values.cIncrementMsg16) { errors.cIncrementMsg16 = (columnLabel.IncrementMsg16 || {}).ErrMessage;}
if (!values.cNoMasterMsg16) { errors.cNoMasterMsg16 = (columnLabel.NoMasterMsg16 || {}).ErrMessage;}
if (!values.cNoDetailMsg16) { errors.cNoDetailMsg16 = (columnLabel.NoDetailMsg16 || {}).ErrMessage;}
if (!values.cAddMasterMsg16) { errors.cAddMasterMsg16 = (columnLabel.AddMasterMsg16 || {}).ErrMessage;}
if (!values.cAddDetailMsg16) { errors.cAddDetailMsg16 = (columnLabel.AddDetailMsg16 || {}).ErrMessage;}
if (!values.cMasterLstTitle16) { errors.cMasterLstTitle16 = (columnLabel.MasterLstTitle16 || {}).ErrMessage;}
if (!values.cMasterLstSubtitle16) { errors.cMasterLstSubtitle16 = (columnLabel.MasterLstSubtitle16 || {}).ErrMessage;}
if (!values.cMasterRecTitle16) { errors.cMasterRecTitle16 = (columnLabel.MasterRecTitle16 || {}).ErrMessage;}
if (!values.cMasterRecSubtitle16) { errors.cMasterRecSubtitle16 = (columnLabel.MasterRecSubtitle16 || {}).ErrMessage;}
if (!values.cMasterFoundMsg16) { errors.cMasterFoundMsg16 = (columnLabel.MasterFoundMsg16 || {}).ErrMessage;}
if (!values.cDetailLstTitle16) { errors.cDetailLstTitle16 = (columnLabel.DetailLstTitle16 || {}).ErrMessage;}
if (!values.cDetailLstSubtitle16) { errors.cDetailLstSubtitle16 = (columnLabel.DetailLstSubtitle16 || {}).ErrMessage;}
if (!values.cDetailRecTitle16) { errors.cDetailRecTitle16 = (columnLabel.DetailRecTitle16 || {}).ErrMessage;}
if (!values.cDetailRecSubtitle16) { errors.cDetailRecSubtitle16 = (columnLabel.DetailRecSubtitle16 || {}).ErrMessage;}
if (!values.cDetailFoundMsg16) { errors.cDetailFoundMsg16 = (columnLabel.DetailFoundMsg16 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmScreen,
      this.props.AdmScreen.Mst,
      [
        {
          ScreenHlpId16: values.cScreenHlpId16 || null,
          CultureId16: (values.cCultureId16|| {}).value || '',
          ScreenTitle16: values.cScreenTitle16|| '',
          DefaultHlpMsg16: values.cDefaultHlpMsg16|| '',
          FootNote16: values.cFootNote16|| '',
          AddMsg16: values.cAddMsg16|| '',
          UpdMsg16: values.cUpdMsg16|| '',
          DelMsg16: values.cDelMsg16|| '',
          IncrementMsg16: values.cIncrementMsg16|| '',
          NoMasterMsg16: values.cNoMasterMsg16|| '',
          NoDetailMsg16: values.cNoDetailMsg16|| '',
          AddMasterMsg16: values.cAddMasterMsg16|| '',
          AddDetailMsg16: values.cAddDetailMsg16|| '',
          MasterLstTitle16: values.cMasterLstTitle16|| '',
          MasterLstSubtitle16: values.cMasterLstSubtitle16|| '',
          MasterRecTitle16: values.cMasterRecTitle16|| '',
          MasterRecSubtitle16: values.cMasterRecSubtitle16|| '',
          MasterFoundMsg16: values.cMasterFoundMsg16|| '',
          DetailLstTitle16: values.cDetailLstTitle16|| '',
          DetailLstSubtitle16: values.cDetailLstSubtitle16|| '',
          DetailRecTitle16: values.cDetailRecTitle16|| '',
          DetailRecSubtitle16: values.cDetailRecSubtitle16|| '',
          DetailFoundMsg16: values.cDetailFoundMsg16|| '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cScreenHlpId16 ? 'upd' : 'add'),
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
    const AdmScreenState = this.props.AdmScreen || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).ScreenHlpId16;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.ScreenId15, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmScreen.Label);
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
    const AdmScreenState = this.props.AdmScreen || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ScreenHlpId16;
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
    const nextReduxScreenState = nextProps.AdmScreen || {};
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
      const { mstId, dtlId } = { ...this.props.match.params };
      if (!(this.props.AdmScreen || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScreen || {};

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
    const AdmScreenState = this.props.AdmScreen || {};
    if (AdmScreenState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmScreenState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmScreenReduxObj.GetScreenButtons(AdmScreenState) || {};
    const auxLabels = AdmScreenState.Label || {};
    const auxSystemLabels = AdmScreenState.SystemLabel || {};
    const columnLabel = AdmScreenState.ColumnLabel || {};
    const currMst = AdmScreenState.Mst;
    const currDtl = AdmScreenState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmScreenState);
    const authRow = (AdmScreenState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const CultureId16List = AdmScreenReduxObj.ScreenDdlSelectors.CultureId16(AdmScreenState);
const CultureId16 = currDtl.CultureId16;
const ScreenTitle16 = currDtl.ScreenTitle16;
const DefaultHlpMsg16 = currDtl.DefaultHlpMsg16;
const FootNote16 = currDtl.FootNote16;
const AddMsg16 = currDtl.AddMsg16;
const UpdMsg16 = currDtl.UpdMsg16;
const DelMsg16 = currDtl.DelMsg16;
const IncrementMsg16 = currDtl.IncrementMsg16;
const NoMasterMsg16 = currDtl.NoMasterMsg16;
const NoDetailMsg16 = currDtl.NoDetailMsg16;
const AddMasterMsg16 = currDtl.AddMasterMsg16;
const AddDetailMsg16 = currDtl.AddDetailMsg16;
const MasterLstTitle16 = currDtl.MasterLstTitle16;
const MasterLstSubtitle16 = currDtl.MasterLstSubtitle16;
const MasterRecTitle16 = currDtl.MasterRecTitle16;
const MasterRecSubtitle16 = currDtl.MasterRecSubtitle16;
const MasterFoundMsg16 = currDtl.MasterFoundMsg16;
const DetailLstTitle16 = currDtl.DetailLstTitle16;
const DetailLstSubtitle16 = currDtl.DetailLstSubtitle16;
const DetailRecTitle16 = currDtl.DetailRecTitle16;
const DetailRecSubtitle16 = currDtl.DetailRecSubtitle16;
const DetailFoundMsg16 = currDtl.DetailFoundMsg16;
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
/* ReactRule: Detail Record Render */
/* ReactRule End: Detail Record Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmScreen) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cCultureId16: CultureId16List.filter(obj => { return obj.key === currDtl.CultureId16 })[0],
                  cScreenTitle16: currDtl.ScreenTitle16 || '',
                  cDefaultHlpMsg16: currDtl.DefaultHlpMsg16 || '',
                  cFootNote16: currDtl.FootNote16 || '',
                  cAddMsg16: currDtl.AddMsg16 || '',
                  cUpdMsg16: currDtl.UpdMsg16 || '',
                  cDelMsg16: currDtl.DelMsg16 || '',
                  cIncrementMsg16: currDtl.IncrementMsg16 || '',
                  cNoMasterMsg16: currDtl.NoMasterMsg16 || '',
                  cNoDetailMsg16: currDtl.NoDetailMsg16 || '',
                  cAddMasterMsg16: currDtl.AddMasterMsg16 || '',
                  cAddDetailMsg16: currDtl.AddDetailMsg16 || '',
                  cMasterLstTitle16: currDtl.MasterLstTitle16 || '',
                  cMasterLstSubtitle16: currDtl.MasterLstSubtitle16 || '',
                  cMasterRecTitle16: currDtl.MasterRecTitle16 || '',
                  cMasterRecSubtitle16: currDtl.MasterRecSubtitle16 || '',
                  cMasterFoundMsg16: currDtl.MasterFoundMsg16 || '',
                  cDetailLstTitle16: currDtl.DetailLstTitle16 || '',
                  cDetailLstSubtitle16: currDtl.DetailLstSubtitle16 || '',
                  cDetailRecTitle16: currDtl.DetailRecTitle16 || '',
                  cDetailRecSubtitle16: currDtl.DetailRecSubtitle16 || '',
                  cDetailFoundMsg16: currDtl.DetailFoundMsg16 || '',
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15,currDtl.ScreenHlpId16)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15,currDtl.ScreenHlpId16)) return null;
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
            {(authCol.CultureId16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureId16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureId16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureId16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureId16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureId16'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId16', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId16', true)}
onInputChange={this.CultureId16InputChange()}
value={values.cCultureId16}
defaultSelected={CultureId16List.filter(obj => { return obj.key === CultureId16 })}
options={CultureId16List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureId16 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureId16 && touched.cCultureId16 && <span className='form__form-group-error'>{errors.cCultureId16}</span>}
</div>
</Col>
}
{(authCol.ScreenTitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenTitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenTitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenTitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenTitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenTitle16'
disabled = {(authCol.ScreenTitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenTitle16 && touched.cScreenTitle16 && <span className='form__form-group-error'>{errors.cScreenTitle16}</span>}
</div>
</Col>
}
{(authCol.DefaultHlpMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultHlpMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DefaultHlpMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultHlpMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultHlpMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefaultHlpMsg16'
disabled = {(authCol.DefaultHlpMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultHlpMsg16 && touched.cDefaultHlpMsg16 && <span className='form__form-group-error'>{errors.cDefaultHlpMsg16}</span>}
</div>
</Col>
}
{(authCol.FootNote16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FootNote16 || {}).ColumnHeader} {(columnLabel.FootNote16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FootNote16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FootNote16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFootNote16'
disabled = {(authCol.FootNote16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFootNote16 && touched.cFootNote16 && <span className='form__form-group-error'>{errors.cFootNote16}</span>}
</div>
</Col>
}
{(authCol.AddMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AddMsg16 || {}).ColumnHeader} {(columnLabel.AddMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AddMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AddMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAddMsg16'
disabled = {(authCol.AddMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAddMsg16 && touched.cAddMsg16 && <span className='form__form-group-error'>{errors.cAddMsg16}</span>}
</div>
</Col>
}
{(authCol.UpdMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UpdMsg16 || {}).ColumnHeader} {(columnLabel.UpdMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UpdMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UpdMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUpdMsg16'
disabled = {(authCol.UpdMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUpdMsg16 && touched.cUpdMsg16 && <span className='form__form-group-error'>{errors.cUpdMsg16}</span>}
</div>
</Col>
}
{(authCol.DelMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DelMsg16 || {}).ColumnHeader} {(columnLabel.DelMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DelMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DelMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDelMsg16'
disabled = {(authCol.DelMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDelMsg16 && touched.cDelMsg16 && <span className='form__form-group-error'>{errors.cDelMsg16}</span>}
</div>
</Col>
}
{(authCol.IncrementMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.IncrementMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.IncrementMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IncrementMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IncrementMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cIncrementMsg16'
disabled = {(authCol.IncrementMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cIncrementMsg16 && touched.cIncrementMsg16 && <span className='form__form-group-error'>{errors.cIncrementMsg16}</span>}
</div>
</Col>
}
{(authCol.NoMasterMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.NoMasterMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.NoMasterMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.NoMasterMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.NoMasterMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cNoMasterMsg16'
disabled = {(authCol.NoMasterMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cNoMasterMsg16 && touched.cNoMasterMsg16 && <span className='form__form-group-error'>{errors.cNoMasterMsg16}</span>}
</div>
</Col>
}
{(authCol.NoDetailMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.NoDetailMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.NoDetailMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.NoDetailMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.NoDetailMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cNoDetailMsg16'
disabled = {(authCol.NoDetailMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cNoDetailMsg16 && touched.cNoDetailMsg16 && <span className='form__form-group-error'>{errors.cNoDetailMsg16}</span>}
</div>
</Col>
}
{(authCol.AddMasterMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AddMasterMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AddMasterMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AddMasterMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AddMasterMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAddMasterMsg16'
disabled = {(authCol.AddMasterMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAddMasterMsg16 && touched.cAddMasterMsg16 && <span className='form__form-group-error'>{errors.cAddMasterMsg16}</span>}
</div>
</Col>
}
{(authCol.AddDetailMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AddDetailMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AddDetailMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AddDetailMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AddDetailMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAddDetailMsg16'
disabled = {(authCol.AddDetailMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAddDetailMsg16 && touched.cAddDetailMsg16 && <span className='form__form-group-error'>{errors.cAddDetailMsg16}</span>}
</div>
</Col>
}
{(authCol.MasterLstTitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterLstTitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterLstTitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterLstTitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterLstTitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterLstTitle16'
disabled = {(authCol.MasterLstTitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterLstTitle16 && touched.cMasterLstTitle16 && <span className='form__form-group-error'>{errors.cMasterLstTitle16}</span>}
</div>
</Col>
}
{(authCol.MasterLstSubtitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterLstSubtitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterLstSubtitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterLstSubtitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterLstSubtitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterLstSubtitle16'
disabled = {(authCol.MasterLstSubtitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterLstSubtitle16 && touched.cMasterLstSubtitle16 && <span className='form__form-group-error'>{errors.cMasterLstSubtitle16}</span>}
</div>
</Col>
}
{(authCol.MasterRecTitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterRecTitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterRecTitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterRecTitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterRecTitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterRecTitle16'
disabled = {(authCol.MasterRecTitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterRecTitle16 && touched.cMasterRecTitle16 && <span className='form__form-group-error'>{errors.cMasterRecTitle16}</span>}
</div>
</Col>
}
{(authCol.MasterRecSubtitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterRecSubtitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterRecSubtitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterRecSubtitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterRecSubtitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterRecSubtitle16'
disabled = {(authCol.MasterRecSubtitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterRecSubtitle16 && touched.cMasterRecSubtitle16 && <span className='form__form-group-error'>{errors.cMasterRecSubtitle16}</span>}
</div>
</Col>
}
{(authCol.MasterFoundMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterFoundMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterFoundMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterFoundMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterFoundMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterFoundMsg16'
disabled = {(authCol.MasterFoundMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterFoundMsg16 && touched.cMasterFoundMsg16 && <span className='form__form-group-error'>{errors.cMasterFoundMsg16}</span>}
</div>
</Col>
}
{(authCol.DetailLstTitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailLstTitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DetailLstTitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailLstTitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailLstTitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDetailLstTitle16'
disabled = {(authCol.DetailLstTitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDetailLstTitle16 && touched.cDetailLstTitle16 && <span className='form__form-group-error'>{errors.cDetailLstTitle16}</span>}
</div>
</Col>
}
{(authCol.DetailLstSubtitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailLstSubtitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DetailLstSubtitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailLstSubtitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailLstSubtitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDetailLstSubtitle16'
disabled = {(authCol.DetailLstSubtitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDetailLstSubtitle16 && touched.cDetailLstSubtitle16 && <span className='form__form-group-error'>{errors.cDetailLstSubtitle16}</span>}
</div>
</Col>
}
{(authCol.DetailRecTitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailRecTitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DetailRecTitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailRecTitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailRecTitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDetailRecTitle16'
disabled = {(authCol.DetailRecTitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDetailRecTitle16 && touched.cDetailRecTitle16 && <span className='form__form-group-error'>{errors.cDetailRecTitle16}</span>}
</div>
</Col>
}
{(authCol.DetailRecSubtitle16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailRecSubtitle16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DetailRecSubtitle16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailRecSubtitle16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailRecSubtitle16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDetailRecSubtitle16'
disabled = {(authCol.DetailRecSubtitle16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDetailRecSubtitle16 && touched.cDetailRecSubtitle16 && <span className='form__form-group-error'>{errors.cDetailRecSubtitle16}</span>}
</div>
</Col>
}
{(authCol.DetailFoundMsg16 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DetailFoundMsg16 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DetailFoundMsg16 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DetailFoundMsg16 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DetailFoundMsg16 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDetailFoundMsg16'
disabled = {(authCol.DetailFoundMsg16 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDetailFoundMsg16 && touched.cDetailFoundMsg16 && <span className='form__form-group-error'>{errors.cDetailFoundMsg16}</span>}
</div>
</Col>
}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenId15,currDtl.ScreenHlpId16)) return null;
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
  AdmScreen: state.AdmScreen,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmScreenReduxObj.LoadPage.bind(AdmScreenReduxObj) },
    { AddDtl: AdmScreenReduxObj.AddDtl.bind(AdmScreenReduxObj) },
    { SavePage: AdmScreenReduxObj.SavePage.bind(AdmScreenReduxObj) },
{ SearchCultureId16: AdmScreenReduxObj.SearchActions.SearchCultureId16.bind(AdmScreenReduxObj) },
  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

