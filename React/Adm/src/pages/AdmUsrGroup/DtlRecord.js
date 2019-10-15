
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
import AdmUsrGroupReduxObj, { ShowMstFilterApplied } from '../../redux/AdmUsrGroup';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmUsrGroup || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'UsrGroupId7';
    this.DtlKeyColumnName = 'UsrGroupAuthId58';
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
    const columnLabel = (this.props.AdmUsrGroup || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
if (isEmptyId((values.cSysRowAuthorityId58 || {}).value)) { errors.cSysRowAuthorityId58 = (columnLabel.SysRowAuthorityId58 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmUsrGroup,
      this.props.AdmUsrGroup.Mst,
      [
        {
          UsrGroupAuthId58: values.cUsrGroupAuthId58 || null,
          CompanyId58: (values.cCompanyId58|| {}).value || '',
          ProjectId58: (values.cProjectId58|| {}).value || '',
          Filler: values.cFiller|| '',
          MoreInfo: values.cMoreInfo || '',
          SystemId58: (values.cSystemId58|| {}).value || '',
          SysRowAuthorityId58: (values.cSysRowAuthorityId58|| {}).value || '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cUsrGroupAuthId58 ? 'upd' : 'add'),
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
    const AdmUsrGroupState = this.props.AdmUsrGroup || {};
    const auxSystemLabels = AdmUsrGroupState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).UsrGroupAuthId58;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.UsrGroupId7, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmUsrGroup.Label);
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
    const AdmUsrGroupState = this.props.AdmUsrGroup || {};
    const auxSystemLabels = AdmUsrGroupState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.UsrGroupAuthId58;
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
    const nextReduxScreenState = nextProps.AdmUsrGroup || {};
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
    const AdmUsrGroupState = this.props.AdmUsrGroup || {};
    const auxSystemLabels = AdmUsrGroupState.SystemLabel || {};
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
      if (!(this.props.AdmUsrGroup || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmUsrGroup || {};

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
    const AdmUsrGroupState = this.props.AdmUsrGroup || {};
    if (AdmUsrGroupState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmUsrGroupState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmUsrGroupReduxObj.GetScreenButtons(AdmUsrGroupState) || {};
    const auxLabels = AdmUsrGroupState.Label || {};
    const auxSystemLabels = AdmUsrGroupState.SystemLabel || {};
    const columnLabel = AdmUsrGroupState.ColumnLabel || {};
    const currMst = AdmUsrGroupState.Mst;
    const currDtl = AdmUsrGroupState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmUsrGroupState);
    const authRow = (AdmUsrGroupState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const CompanyId58List = AdmUsrGroupReduxObj.ScreenDdlSelectors.CompanyId58(AdmUsrGroupState);
const CompanyId58 = currDtl.CompanyId58;
const ProjectId58List = AdmUsrGroupReduxObj.ScreenDdlSelectors.ProjectId58(AdmUsrGroupState);
const ProjectId58 = currDtl.ProjectId58;
const Filler = currDtl.Filler;
const MoreInfo = currDtl.MoreInfo;
const SystemId58List = AdmUsrGroupReduxObj.ScreenDdlSelectors.SystemId58(AdmUsrGroupState);
const SystemId58 = currDtl.SystemId58;
const SysRowAuthorityId58List = AdmUsrGroupReduxObj.ScreenDdlSelectors.SysRowAuthorityId58(AdmUsrGroupState);
const SysRowAuthorityId58 = currDtl.SysRowAuthorityId58;
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
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmUsrGroup) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cCompanyId58: CompanyId58List.filter(obj => { return obj.key === currDtl.CompanyId58 })[0],
                  cProjectId58: ProjectId58List.filter(obj => { return obj.key === currDtl.ProjectId58 })[0],
                  cFiller: currDtl.Filler || '',
                  cMoreInfo: currDtl.MoreInfo || '',
                  cSystemId58: SystemId58List.filter(obj => { return obj.key === currDtl.SystemId58 })[0],
                  cSysRowAuthorityId58: SysRowAuthorityId58List.filter(obj => { return obj.key === currDtl.SysRowAuthorityId58 })[0],
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrGroupId7,currDtl.UsrGroupAuthId58)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrGroupId7,currDtl.UsrGroupAuthId58)) return null;
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
            {(authCol.CompanyId58 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CompanyId58 || {}).ColumnHeader} {(columnLabel.CompanyId58 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CompanyId58 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CompanyId58 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cCompanyId58'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cCompanyId58')}
value={values.cCompanyId58}
options={CompanyId58List}
placeholder=''
disabled = {(authCol.CompanyId58 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCompanyId58 && touched.cCompanyId58 && <span className='form__form-group-error'>{errors.cCompanyId58}</span>}
</div>
</Col>
}
{(authCol.ProjectId58 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProjectId58 || {}).ColumnHeader} {(columnLabel.ProjectId58 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProjectId58 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProjectId58 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cProjectId58'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cProjectId58')}
value={values.cProjectId58}
options={ProjectId58List}
placeholder=''
disabled = {(authCol.ProjectId58 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProjectId58 && touched.cProjectId58 && <span className='form__form-group-error'>{errors.cProjectId58}</span>}
</div>
</Col>
}
{(authCol.Filler || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Filler || {}).ColumnHeader} {(columnLabel.Filler || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Filler || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Filler || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFiller'
disabled = {(authCol.Filler || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFiller && touched.cFiller && <span className='form__form-group-error'>{errors.cFiller}</span>}
</div>
</Col>
}
{(authCol.FillerBtn || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FillerBtn || {}).ColumnHeader} {(columnLabel.FillerBtn || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FillerBtn || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FillerBtn || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.MoreInfo || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MoreInfo || {}).ColumnHeader} {(columnLabel.MoreInfo || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MoreInfo || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MoreInfo || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMoreInfo'
disabled = {(authCol.MoreInfo || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMoreInfo && touched.cMoreInfo && <span className='form__form-group-error'>{errors.cMoreInfo}</span>}
</div>
</Col>
}
{(authCol.SystemId58 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemId58 || {}).ColumnHeader} {(columnLabel.SystemId58 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemId58 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemId58 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSystemId58'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSystemId58')}
value={values.cSystemId58}
options={SystemId58List}
placeholder=''
disabled = {(authCol.SystemId58 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemId58 && touched.cSystemId58 && <span className='form__form-group-error'>{errors.cSystemId58}</span>}
</div>
</Col>
}
{(authCol.SysRowAuthorityId58 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SysRowAuthorityId58 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SysRowAuthorityId58 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SysRowAuthorityId58 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SysRowAuthorityId58 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrGroupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSysRowAuthorityId58'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSysRowAuthorityId58')}
value={values.cSysRowAuthorityId58}
options={SysRowAuthorityId58List}
placeholder=''
disabled = {(authCol.SysRowAuthorityId58 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSysRowAuthorityId58 && touched.cSysRowAuthorityId58 && <span className='form__form-group-error'>{errors.cSysRowAuthorityId58}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrGroupId7,currDtl.UsrGroupAuthId58)) return null;
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
  AdmUsrGroup: state.AdmUsrGroup,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmUsrGroupReduxObj.LoadPage.bind(AdmUsrGroupReduxObj) },
    { AddDtl: AdmUsrGroupReduxObj.AddDtl.bind(AdmUsrGroupReduxObj) },
    { SavePage: AdmUsrGroupReduxObj.SavePage.bind(AdmUsrGroupReduxObj) },

  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

