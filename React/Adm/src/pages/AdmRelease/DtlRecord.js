
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
import AdmReleaseReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRelease';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRelease || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'ReleaseId191';
    this.DtlKeyColumnName = 'ReleaseDtlId192';
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


/* ReactRule: Detail Record Custom Function */
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmRelease || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
if (isEmptyId((values.cObjectType192 || {}).value)) { errors.cObjectType192 = (columnLabel.ObjectType192 || {}).ErrMessage;}
if (isEmptyId((values.cSProcOnly192 || {}).value)) { errors.cSProcOnly192 = (columnLabel.SProcOnly192 || {}).ErrMessage;}
if (!values.cObjectName192) { errors.cObjectName192 = (columnLabel.ObjectName192 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmRelease,
      this.props.AdmRelease.Mst,
      [
        {
          ReleaseDtlId192: values.cReleaseDtlId192 || null,
          ObjectType192: (values.cObjectType192|| {}).value || '',
          RunOrder192: values.cRunOrder192|| '',
          SrcObject192: values.cSrcObject192|| '',
          SProcOnly192: (values.cSProcOnly192|| {}).value || '',
          ObjectName192: values.cObjectName192 || '',
          ObjectExempt192: values.cObjectExempt192 || '',
          SrcClientTierId192: (values.cSrcClientTierId192|| {}).value || '',
          SrcRuleTierId192: (values.cSrcRuleTierId192|| {}).value || '',
          SrcDataTierId192: (values.cSrcDataTierId192|| {}).value || '',
          TarDataTierId192: (values.cTarDataTierId192|| {}).value || '',
          DoSpEncrypt192: values.cDoSpEncrypt192 ? 'Y' : 'N',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cReleaseDtlId192 ? 'upd' : 'add'),
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).ReleaseDtlId192;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.ReleaseId191, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmRelease.Label);
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ReleaseDtlId192;
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
    const nextReduxScreenState = nextProps.AdmRelease || {};
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
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
      if (!(this.props.AdmRelease || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRelease || {};

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
    const AdmReleaseState = this.props.AdmRelease || {};
    if (AdmReleaseState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmReleaseState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmReleaseReduxObj.GetScreenButtons(AdmReleaseState) || {};
    const auxLabels = AdmReleaseState.Label || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
    const columnLabel = AdmReleaseState.ColumnLabel || {};
    const currMst = AdmReleaseState.Mst;
    const currDtl = AdmReleaseState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmReleaseState);
    const authRow = (AdmReleaseState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const ObjectType192List = AdmReleaseReduxObj.ScreenDdlSelectors.ObjectType192(AdmReleaseState);
const ObjectType192 = currDtl.ObjectType192;
const RunOrder192 = currDtl.RunOrder192;
const SrcObject192 = currDtl.SrcObject192;
const SProcOnly192List = AdmReleaseReduxObj.ScreenDdlSelectors.SProcOnly192(AdmReleaseState);
const SProcOnly192 = currDtl.SProcOnly192;
const ObjectName192 = currDtl.ObjectName192;
const ObjectExempt192 = currDtl.ObjectExempt192;
const SrcClientTierId192List = AdmReleaseReduxObj.ScreenDdlSelectors.SrcClientTierId192(AdmReleaseState);
const SrcClientTierId192 = currDtl.SrcClientTierId192;
const SrcRuleTierId192List = AdmReleaseReduxObj.ScreenDdlSelectors.SrcRuleTierId192(AdmReleaseState);
const SrcRuleTierId192 = currDtl.SrcRuleTierId192;
const SrcDataTierId192List = AdmReleaseReduxObj.ScreenDdlSelectors.SrcDataTierId192(AdmReleaseState);
const SrcDataTierId192 = currDtl.SrcDataTierId192;
const TarDataTierId192List = AdmReleaseReduxObj.ScreenDdlSelectors.TarDataTierId192(AdmReleaseState);
const TarDataTierId192 = currDtl.TarDataTierId192;
const DoSpEncrypt192 = currDtl.DoSpEncrypt192;
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
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmRelease) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cObjectType192: ObjectType192List.filter(obj => { return obj.key === currDtl.ObjectType192 })[0],
                  cRunOrder192: currDtl.RunOrder192 || '',
                  cSrcObject192: currDtl.SrcObject192 || '',
                  cSProcOnly192: SProcOnly192List.filter(obj => { return obj.key === currDtl.SProcOnly192 })[0],
                  cObjectName192: currDtl.ObjectName192 || '',
                  cObjectExempt192: currDtl.ObjectExempt192 || '',
                  cSrcClientTierId192: SrcClientTierId192List.filter(obj => { return obj.key === currDtl.SrcClientTierId192 })[0],
                  cSrcRuleTierId192: SrcRuleTierId192List.filter(obj => { return obj.key === currDtl.SrcRuleTierId192 })[0],
                  cSrcDataTierId192: SrcDataTierId192List.filter(obj => { return obj.key === currDtl.SrcDataTierId192 })[0],
                  cTarDataTierId192: TarDataTierId192List.filter(obj => { return obj.key === currDtl.TarDataTierId192 })[0],
                  cDoSpEncrypt192: currDtl.DoSpEncrypt192 === 'Y',
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191,currDtl.ReleaseDtlId192)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191,currDtl.ReleaseDtlId192)) return null;
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
            {(authCol.ObjectType192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ObjectType192 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ObjectType192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ObjectType192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ObjectType192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cObjectType192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cObjectType192')}
value={values.cObjectType192}
options={ObjectType192List}
placeholder=''
disabled = {(authCol.ObjectType192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cObjectType192 && touched.cObjectType192 && <span className='form__form-group-error'>{errors.cObjectType192}</span>}
</div>
</Col>
}
{(authCol.RunOrder192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RunOrder192 || {}).ColumnHeader} {(columnLabel.RunOrder192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RunOrder192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RunOrder192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRunOrder192'
disabled = {(authCol.RunOrder192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRunOrder192 && touched.cRunOrder192 && <span className='form__form-group-error'>{errors.cRunOrder192}</span>}
</div>
</Col>
}
{(authCol.SrcObject192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SrcObject192 || {}).ColumnHeader} {(columnLabel.SrcObject192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SrcObject192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SrcObject192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSrcObject192'
disabled = {(authCol.SrcObject192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSrcObject192 && touched.cSrcObject192 && <span className='form__form-group-error'>{errors.cSrcObject192}</span>}
</div>
</Col>
}
{(authCol.SProcOnly192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SProcOnly192 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SProcOnly192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SProcOnly192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SProcOnly192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSProcOnly192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSProcOnly192')}
value={values.cSProcOnly192}
options={SProcOnly192List}
placeholder=''
disabled = {(authCol.SProcOnly192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSProcOnly192 && touched.cSProcOnly192 && <span className='form__form-group-error'>{errors.cSProcOnly192}</span>}
</div>
</Col>
}
{(authCol.ObjectName192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ObjectName192 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ObjectName192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ObjectName192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ObjectName192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cObjectName192'
disabled = {(authCol.ObjectName192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cObjectName192 && touched.cObjectName192 && <span className='form__form-group-error'>{errors.cObjectName192}</span>}
</div>
</Col>
}
{(authCol.ObjectExempt192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ObjectExempt192 || {}).ColumnHeader} {(columnLabel.ObjectExempt192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ObjectExempt192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ObjectExempt192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cObjectExempt192'
disabled = {(authCol.ObjectExempt192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cObjectExempt192 && touched.cObjectExempt192 && <span className='form__form-group-error'>{errors.cObjectExempt192}</span>}
</div>
</Col>
}
{(authCol.SrcClientTierId192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SrcClientTierId192 || {}).ColumnHeader} {(columnLabel.SrcClientTierId192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SrcClientTierId192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SrcClientTierId192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSrcClientTierId192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSrcClientTierId192')}
value={values.cSrcClientTierId192}
options={SrcClientTierId192List}
placeholder=''
disabled = {(authCol.SrcClientTierId192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSrcClientTierId192 && touched.cSrcClientTierId192 && <span className='form__form-group-error'>{errors.cSrcClientTierId192}</span>}
</div>
</Col>
}
{(authCol.SrcRuleTierId192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SrcRuleTierId192 || {}).ColumnHeader} {(columnLabel.SrcRuleTierId192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SrcRuleTierId192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SrcRuleTierId192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSrcRuleTierId192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSrcRuleTierId192')}
value={values.cSrcRuleTierId192}
options={SrcRuleTierId192List}
placeholder=''
disabled = {(authCol.SrcRuleTierId192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSrcRuleTierId192 && touched.cSrcRuleTierId192 && <span className='form__form-group-error'>{errors.cSrcRuleTierId192}</span>}
</div>
</Col>
}
{(authCol.SrcDataTierId192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SrcDataTierId192 || {}).ColumnHeader} {(columnLabel.SrcDataTierId192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SrcDataTierId192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SrcDataTierId192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSrcDataTierId192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSrcDataTierId192')}
value={values.cSrcDataTierId192}
options={SrcDataTierId192List}
placeholder=''
disabled = {(authCol.SrcDataTierId192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSrcDataTierId192 && touched.cSrcDataTierId192 && <span className='form__form-group-error'>{errors.cSrcDataTierId192}</span>}
</div>
</Col>
}
{(authCol.TarDataTierId192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TarDataTierId192 || {}).ColumnHeader} {(columnLabel.TarDataTierId192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TarDataTierId192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TarDataTierId192 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cTarDataTierId192'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cTarDataTierId192')}
value={values.cTarDataTierId192}
options={TarDataTierId192List}
placeholder=''
disabled = {(authCol.TarDataTierId192 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTarDataTierId192 && touched.cTarDataTierId192 && <span className='form__form-group-error'>{errors.cTarDataTierId192}</span>}
</div>
</Col>
}
{(authCol.DoSpEncrypt192 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cDoSpEncrypt192'
onChange={handleChange}
defaultChecked={values.cDoSpEncrypt192}
disabled={(authCol.DoSpEncrypt192 || {}).readonly || !(authCol.DoSpEncrypt192 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.DoSpEncrypt192 || {}).ColumnHeader}</span>
</label>
{(columnLabel.DoSpEncrypt192 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DoSpEncrypt192 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DoSpEncrypt192 || {}).ToolTip} />
)}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191,currDtl.ReleaseDtlId192)) return null;
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
  AdmRelease: state.AdmRelease,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmReleaseReduxObj.LoadPage.bind(AdmReleaseReduxObj) },
    { AddDtl: AdmReleaseReduxObj.AddDtl.bind(AdmReleaseReduxObj) },
    { SavePage: AdmReleaseReduxObj.SavePage.bind(AdmReleaseReduxObj) },

  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

