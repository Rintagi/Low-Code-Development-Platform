
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
import AdmAuthColReduxObj, { ShowMstFilterApplied } from '../../redux/AdmAuthCol';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmAuthCol || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'ScreenObjId14';
    this.DtlKeyColumnName = 'ColOvrdId241';
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

PermKeyRowId241InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmAuthCol || {}).Dtl || {}).PermKeyId241; _this.props.SearchPermKeyRowId241(v, filterBy);}}
/* ReactRule: Detail Record Custom Function */
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmAuthCol || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */

    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmAuthCol,
      this.props.AdmAuthCol.Mst,
      [
        {
          ColOvrdId241: values.cColOvrdId241 || null,
          ColVisible241: values.cColVisible241 ? 'Y' : 'N',
          ColReadOnly241: values.cColReadOnly241 ? 'Y' : 'N',
          ColExport241: values.cColExport241 ? 'Y' : 'N',
          PermKeyId241: (values.cPermKeyId241|| {}).value || '',
          PermKeyRowId241: (values.cPermKeyRowId241|| {}).value || '',
          Priority241: values.cPriority241|| '',
          ToolTip241: values.cToolTip241|| '',
          ColumnHeader241: values.cColumnHeader241|| '',
          ErrMessage241: values.cErrMessage241|| '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cColOvrdId241 ? 'upd' : 'add'),
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
    const AdmAuthColState = this.props.AdmAuthCol || {};
    const auxSystemLabels = AdmAuthColState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).ColOvrdId241;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.ScreenObjId14, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmAuthCol.Label);
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
    const AdmAuthColState = this.props.AdmAuthCol || {};
    const auxSystemLabels = AdmAuthColState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ColOvrdId241;
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
    const nextReduxScreenState = nextProps.AdmAuthCol || {};
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
    const AdmAuthColState = this.props.AdmAuthCol || {};
    const auxSystemLabels = AdmAuthColState.SystemLabel || {};
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
      if (!(this.props.AdmAuthCol || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmAuthCol || {};

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
    const AdmAuthColState = this.props.AdmAuthCol || {};
    if (AdmAuthColState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmAuthColState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmAuthColReduxObj.GetScreenButtons(AdmAuthColState) || {};
    const auxLabels = AdmAuthColState.Label || {};
    const auxSystemLabels = AdmAuthColState.SystemLabel || {};
    const columnLabel = AdmAuthColState.ColumnLabel || {};
    const currMst = AdmAuthColState.Mst;
    const currDtl = AdmAuthColState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmAuthColState);
    const authRow = (AdmAuthColState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const ColVisible241 = currDtl.ColVisible241;
const ColReadOnly241 = currDtl.ColReadOnly241;
const ColExport241 = currDtl.ColExport241;
const PermKeyId241List = AdmAuthColReduxObj.ScreenDdlSelectors.PermKeyId241(AdmAuthColState);
const PermKeyId241 = currDtl.PermKeyId241;
const PermKeyRowId241List = AdmAuthColReduxObj.ScreenDdlSelectors.PermKeyRowId241(AdmAuthColState);
const PermKeyRowId241 = currDtl.PermKeyRowId241;
const Priority241 = currDtl.Priority241;
const ToolTip241 = currDtl.ToolTip241;
const ColumnHeader241 = currDtl.ColumnHeader241;
const ErrMessage241 = currDtl.ErrMessage241;
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
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmAuthCol) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cColVisible241: currDtl.ColVisible241 === 'Y',
                  cColReadOnly241: currDtl.ColReadOnly241 === 'Y',
                  cColExport241: currDtl.ColExport241 === 'Y',
                  cPermKeyId241: PermKeyId241List.filter(obj => { return obj.key === currDtl.PermKeyId241 })[0],
                  cPermKeyRowId241: PermKeyRowId241List.filter(obj => { return obj.key === currDtl.PermKeyRowId241 })[0],
                  cPriority241: currDtl.Priority241 || '',
                  cToolTip241: currDtl.ToolTip241 || '',
                  cColumnHeader241: currDtl.ColumnHeader241 || '',
                  cErrMessage241: currDtl.ErrMessage241 || '',
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14,currDtl.ColOvrdId241)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14,currDtl.ColOvrdId241)) return null;
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
            {(authCol.ColVisible241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cColVisible241'
onChange={handleChange}
defaultChecked={values.cColVisible241}
disabled={(authCol.ColVisible241 || {}).readonly || !(authCol.ColVisible241 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ColVisible241 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ColVisible241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColVisible241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColVisible241 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColReadOnly241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cColReadOnly241'
onChange={handleChange}
defaultChecked={values.cColReadOnly241}
disabled={(authCol.ColReadOnly241 || {}).readonly || !(authCol.ColReadOnly241 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ColReadOnly241 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ColReadOnly241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColReadOnly241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColReadOnly241 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColExport241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cColExport241'
onChange={handleChange}
defaultChecked={values.cColExport241}
disabled={(authCol.ColExport241 || {}).readonly || !(authCol.ColExport241 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ColExport241 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ColExport241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColExport241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColExport241 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.PermKeyId241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PermKeyId241 || {}).ColumnHeader} {(columnLabel.PermKeyId241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PermKeyId241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PermKeyId241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cPermKeyId241'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cPermKeyId241')}
value={values.cPermKeyId241}
options={PermKeyId241List}
placeholder=''
disabled = {(authCol.PermKeyId241 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPermKeyId241 && touched.cPermKeyId241 && <span className='form__form-group-error'>{errors.cPermKeyId241}</span>}
</div>
</Col>
}
{(authCol.PermKeyRowId241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PermKeyRowId241 || {}).ColumnHeader} {(columnLabel.PermKeyRowId241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PermKeyRowId241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PermKeyRowId241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cPermKeyRowId241'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cPermKeyRowId241', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cPermKeyRowId241', true)}
onInputChange={this.PermKeyRowId241InputChange()}
value={values.cPermKeyRowId241}
defaultSelected={PermKeyRowId241List.filter(obj => { return obj.key === PermKeyRowId241 })}
options={PermKeyRowId241List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.PermKeyRowId241 || {}).readonly ? true: false }/>
</div>
}
{errors.cPermKeyRowId241 && touched.cPermKeyRowId241 && <span className='form__form-group-error'>{errors.cPermKeyRowId241}</span>}
</div>
</Col>
}
{(authCol.Priority241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Priority241 || {}).ColumnHeader} {(columnLabel.Priority241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Priority241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Priority241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPriority241'
disabled = {(authCol.Priority241 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPriority241 && touched.cPriority241 && <span className='form__form-group-error'>{errors.cPriority241}</span>}
</div>
</Col>
}
{(authCol.ToolTip241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ToolTip241 || {}).ColumnHeader} {(columnLabel.ToolTip241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ToolTip241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ToolTip241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cToolTip241'
disabled = {(authCol.ToolTip241 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cToolTip241 && touched.cToolTip241 && <span className='form__form-group-error'>{errors.cToolTip241}</span>}
</div>
</Col>
}
{(authCol.ColumnHeader241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnHeader241 || {}).ColumnHeader} {(columnLabel.ColumnHeader241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnHeader241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnHeader241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnHeader241'
disabled = {(authCol.ColumnHeader241 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnHeader241 && touched.cColumnHeader241 && <span className='form__form-group-error'>{errors.cColumnHeader241}</span>}
</div>
</Col>
}
{(authCol.ErrMessage241 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ErrMessage241 || {}).ColumnHeader} {(columnLabel.ErrMessage241 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ErrMessage241 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ErrMessage241 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAuthColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cErrMessage241'
disabled = {(authCol.ErrMessage241 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cErrMessage241 && touched.cErrMessage241 && <span className='form__form-group-error'>{errors.cErrMessage241}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14,currDtl.ColOvrdId241)) return null;
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
  AdmAuthCol: state.AdmAuthCol,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmAuthColReduxObj.LoadPage.bind(AdmAuthColReduxObj) },
    { AddDtl: AdmAuthColReduxObj.AddDtl.bind(AdmAuthColReduxObj) },
    { SavePage: AdmAuthColReduxObj.SavePage.bind(AdmAuthColReduxObj) },
{ SearchPermKeyRowId241: AdmAuthColReduxObj.SearchActions.SearchPermKeyRowId241.bind(AdmAuthColReduxObj) },
  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

