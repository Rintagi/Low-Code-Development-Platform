
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
import AdmDataCatReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDataCat';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDataCat || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'RptwizCatId181';
    this.DtlKeyColumnName = 'RptwizCatDtlId182';
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

ColumnId182InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmDataCat || {}).Mst || {}).TableId181; _this.props.SearchColumnId182(v, filterBy);}}
/* ReactRule: Detail Record Custom Function */
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmDataCat || {}).ColumnLabel || {};
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
      this.props.AdmDataCat,
      this.props.AdmDataCat.Mst,
      [
        {
          RptwizCatDtlId182: values.cRptwizCatDtlId182 || null,
          ColumnId182: (values.cColumnId182|| {}).value || '',
          DisplayModeId182: (values.cDisplayModeId182|| {}).value || '',
          ColumnSize182: values.cColumnSize182|| '',
          RowSize182: values.cRowSize182|| '',
          DdlKeyColNm182: values.cDdlKeyColNm182|| '',
          DdlRefColNm182: values.cDdlRefColNm182|| '',
          RegClause182: values.cRegClause182 || '',
          StoredProc182: values.cStoredProc182 || '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cRptwizCatDtlId182 ? 'upd' : 'add'),
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).RptwizCatDtlId182;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.RptwizCatId181, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmDataCat.Label);
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.RptwizCatDtlId182;
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
    const nextReduxScreenState = nextProps.AdmDataCat || {};
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
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
      if (!(this.props.AdmDataCat || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDataCat || {};

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
    const AdmDataCatState = this.props.AdmDataCat || {};
    if (AdmDataCatState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmDataCatState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmDataCatReduxObj.GetScreenButtons(AdmDataCatState) || {};
    const auxLabels = AdmDataCatState.Label || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
    const columnLabel = AdmDataCatState.ColumnLabel || {};
    const currMst = AdmDataCatState.Mst;
    const currDtl = AdmDataCatState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmDataCatState);
    const authRow = (AdmDataCatState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const ColumnId182List = AdmDataCatReduxObj.ScreenDdlSelectors.ColumnId182(AdmDataCatState);
const ColumnId182 = currDtl.ColumnId182;
const DisplayModeId182List = AdmDataCatReduxObj.ScreenDdlSelectors.DisplayModeId182(AdmDataCatState);
const DisplayModeId182 = currDtl.DisplayModeId182;
const ColumnSize182 = currDtl.ColumnSize182;
const RowSize182 = currDtl.RowSize182;
const DdlKeyColNm182 = currDtl.DdlKeyColNm182;
const DdlRefColNm182 = currDtl.DdlRefColNm182;
const RegClause182 = currDtl.RegClause182;
const StoredProc182 = currDtl.StoredProc182;
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
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmDataCat) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cColumnId182: ColumnId182List.filter(obj => { return obj.key === currDtl.ColumnId182 })[0],
                  cDisplayModeId182: DisplayModeId182List.filter(obj => { return obj.key === currDtl.DisplayModeId182 })[0],
                  cColumnSize182: currDtl.ColumnSize182 || '',
                  cRowSize182: currDtl.RowSize182 || '',
                  cDdlKeyColNm182: currDtl.DdlKeyColNm182 || '',
                  cDdlRefColNm182: currDtl.DdlRefColNm182 || '',
                  cRegClause182: currDtl.RegClause182 || '',
                  cStoredProc182: currDtl.StoredProc182 || '',
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181,currDtl.RptwizCatDtlId182)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181,currDtl.RptwizCatDtlId182)) return null;
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
            {(authCol.ColumnId182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnId182 || {}).ColumnHeader} {(columnLabel.ColumnId182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnId182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnId182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cColumnId182'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId182', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId182', true)}
onInputChange={this.ColumnId182InputChange()}
value={values.cColumnId182}
defaultSelected={ColumnId182List.filter(obj => { return obj.key === ColumnId182 })}
options={ColumnId182List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ColumnId182 || {}).readonly ? true: false }/>
</div>
}
{errors.cColumnId182 && touched.cColumnId182 && <span className='form__form-group-error'>{errors.cColumnId182}</span>}
</div>
</Col>
}
{(authCol.DisplayModeId182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DisplayModeId182 || {}).ColumnHeader} {(columnLabel.DisplayModeId182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DisplayModeId182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DisplayModeId182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDisplayModeId182'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDisplayModeId182')}
value={values.cDisplayModeId182}
options={DisplayModeId182List}
placeholder=''
disabled = {(authCol.DisplayModeId182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDisplayModeId182 && touched.cDisplayModeId182 && <span className='form__form-group-error'>{errors.cDisplayModeId182}</span>}
</div>
</Col>
}
{(authCol.ColumnSize182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnSize182 || {}).ColumnHeader} {(columnLabel.ColumnSize182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnSize182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnSize182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnSize182'
disabled = {(authCol.ColumnSize182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnSize182 && touched.cColumnSize182 && <span className='form__form-group-error'>{errors.cColumnSize182}</span>}
</div>
</Col>
}
{(authCol.RowSize182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowSize182 || {}).ColumnHeader} {(columnLabel.RowSize182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowSize182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowSize182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowSize182'
disabled = {(authCol.RowSize182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowSize182 && touched.cRowSize182 && <span className='form__form-group-error'>{errors.cRowSize182}</span>}
</div>
</Col>
}
{(authCol.DdlKeyColNm182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlKeyColNm182 || {}).ColumnHeader} {(columnLabel.DdlKeyColNm182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlKeyColNm182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlKeyColNm182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDdlKeyColNm182'
disabled = {(authCol.DdlKeyColNm182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDdlKeyColNm182 && touched.cDdlKeyColNm182 && <span className='form__form-group-error'>{errors.cDdlKeyColNm182}</span>}
</div>
</Col>
}
{(authCol.DdlRefColNm182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlRefColNm182 || {}).ColumnHeader} {(columnLabel.DdlRefColNm182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlRefColNm182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlRefColNm182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDdlRefColNm182'
disabled = {(authCol.DdlRefColNm182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDdlRefColNm182 && touched.cDdlRefColNm182 && <span className='form__form-group-error'>{errors.cDdlRefColNm182}</span>}
</div>
</Col>
}
{(authCol.RegClause182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RegClause182 || {}).ColumnHeader} {(columnLabel.RegClause182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RegClause182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RegClause182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRegClause182'
disabled = {(authCol.RegClause182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRegClause182 && touched.cRegClause182 && <span className='form__form-group-error'>{errors.cRegClause182}</span>}
</div>
</Col>
}
{(authCol.StoredProc182 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StoredProc182 || {}).ColumnHeader} {(columnLabel.StoredProc182 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StoredProc182 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StoredProc182 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStoredProc182'
disabled = {(authCol.StoredProc182 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStoredProc182 && touched.cStoredProc182 && <span className='form__form-group-error'>{errors.cStoredProc182}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181,currDtl.RptwizCatDtlId182)) return null;
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
  AdmDataCat: state.AdmDataCat,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmDataCatReduxObj.LoadPage.bind(AdmDataCatReduxObj) },
    { AddDtl: AdmDataCatReduxObj.AddDtl.bind(AdmDataCatReduxObj) },
    { SavePage: AdmDataCatReduxObj.SavePage.bind(AdmDataCatReduxObj) },
{ SearchColumnId182: AdmDataCatReduxObj.SearchActions.SearchColumnId182.bind(AdmDataCatReduxObj) },
  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

