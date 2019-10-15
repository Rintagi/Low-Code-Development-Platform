
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
import AdmDbTableReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDbTable';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDbTable || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'TableId3';
    this.DtlKeyColumnName = 'ColumnId5';
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
    const columnLabel = (this.props.AdmDbTable || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
if (!values.cColumnName5) { errors.cColumnName5 = (columnLabel.ColumnName5 || {}).ErrMessage;}
if (isEmptyId((values.cDataType5 || {}).value)) { errors.cDataType5 = (columnLabel.DataType5 || {}).ErrMessage;}
if (!values.cColumnLength5) { errors.cColumnLength5 = (columnLabel.ColumnLength5 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmDbTable,
      this.props.AdmDbTable.Mst,
      [
        {
          ColumnId5: values.cColumnId5 || null,
          ColumnIndex5: values.cColumnIndex5|| '',
          ExternalTable5: values.cExternalTable5|| '',
          ColumnName5: values.cColumnName5|| '',
          DataType5: (values.cDataType5|| {}).value || '',
          ColumnLength5: values.cColumnLength5|| '',
          ColumnScale5: values.cColumnScale5|| '',
          DefaultValue5: values.cDefaultValue5|| '',
          AllowNulls5: values.cAllowNulls5 || '',
          ColumnIdentity5: values.cColumnIdentity5 ? 'Y' : 'N',
          PrimaryKey5: values.cPrimaryKey5 ? 'Y' : 'N',
          IsIndexU5: values.cIsIndexU5 ? 'Y' : 'N',
          IsIndex5: values.cIsIndex5 ? 'Y' : 'N',
          ColObjective5: values.cColObjective5|| '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cColumnId5 ? 'upd' : 'add'),
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).ColumnId5;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.TableId3, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmDbTable.Label);
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ColumnId5;
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
    const nextReduxScreenState = nextProps.AdmDbTable || {};
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
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
      if (!(this.props.AdmDbTable || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDbTable || {};

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
    const AdmDbTableState = this.props.AdmDbTable || {};
    if (AdmDbTableState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmDbTableState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmDbTableReduxObj.GetScreenButtons(AdmDbTableState) || {};
    const auxLabels = AdmDbTableState.Label || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
    const columnLabel = AdmDbTableState.ColumnLabel || {};
    const currMst = AdmDbTableState.Mst;
    const currDtl = AdmDbTableState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmDbTableState);
    const authRow = (AdmDbTableState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const ColumnIndex5 = currDtl.ColumnIndex5;
const ExternalTable5 = currDtl.ExternalTable5;
const ColumnName5 = currDtl.ColumnName5;
const DataType5List = AdmDbTableReduxObj.ScreenDdlSelectors.DataType5(AdmDbTableState);
const DataType5 = currDtl.DataType5;
const ColumnLength5 = currDtl.ColumnLength5;
const ColumnScale5 = currDtl.ColumnScale5;
const DefaultValue5 = currDtl.DefaultValue5;
const AllowNulls5 = currDtl.AllowNulls5;
const ColumnIdentity5 = currDtl.ColumnIdentity5;
const PrimaryKey5 = currDtl.PrimaryKey5;
const IsIndexU5 = currDtl.IsIndexU5;
const IsIndex5 = currDtl.IsIndex5;
const ColObjective5 = currDtl.ColObjective5;
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
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmDbTable) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cColumnIndex5: currDtl.ColumnIndex5 || '',
                  cExternalTable5: currDtl.ExternalTable5 || '',
                  cColumnName5: currDtl.ColumnName5 || '',
                  cDataType5: DataType5List.filter(obj => { return obj.key === currDtl.DataType5 })[0],
                  cColumnLength5: currDtl.ColumnLength5 || '',
                  cColumnScale5: currDtl.ColumnScale5 || '',
                  cDefaultValue5: currDtl.DefaultValue5 || '',
                  cAllowNulls5: currDtl.AllowNulls5 || '',
                  cColumnIdentity5: currDtl.ColumnIdentity5 === 'Y',
                  cPrimaryKey5: currDtl.PrimaryKey5 === 'Y',
                  cIsIndexU5: currDtl.IsIndexU5 === 'Y',
                  cIsIndex5: currDtl.IsIndex5 === 'Y',
                  cColObjective5: currDtl.ColObjective5 || '',
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
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3,currDtl.ColumnId5)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3,currDtl.ColumnId5)) return null;
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
            {(authCol.ColumnIndex5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnIndex5 || {}).ColumnHeader} {(columnLabel.ColumnIndex5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnIndex5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnIndex5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnIndex5'
disabled = {(authCol.ColumnIndex5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnIndex5 && touched.cColumnIndex5 && <span className='form__form-group-error'>{errors.cColumnIndex5}</span>}
</div>
</Col>
}
{(authCol.ExternalTable5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ExternalTable5 || {}).ColumnHeader} {(columnLabel.ExternalTable5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ExternalTable5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ExternalTable5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cExternalTable5'
disabled = {(authCol.ExternalTable5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cExternalTable5 && touched.cExternalTable5 && <span className='form__form-group-error'>{errors.cExternalTable5}</span>}
</div>
</Col>
}
{(authCol.ColumnName5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnName5 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnName5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnName5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnName5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnName5'
disabled = {(authCol.ColumnName5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnName5 && touched.cColumnName5 && <span className='form__form-group-error'>{errors.cColumnName5}</span>}
</div>
</Col>
}
{(authCol.DataType5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataType5 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DataType5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataType5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataType5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDataType5'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDataType5')}
value={values.cDataType5}
options={DataType5List}
placeholder=''
disabled = {(authCol.DataType5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataType5 && touched.cDataType5 && <span className='form__form-group-error'>{errors.cDataType5}</span>}
</div>
</Col>
}
{(authCol.ColumnLength5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnLength5 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnLength5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnLength5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnLength5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnLength5'
disabled = {(authCol.ColumnLength5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnLength5 && touched.cColumnLength5 && <span className='form__form-group-error'>{errors.cColumnLength5}</span>}
</div>
</Col>
}
{(authCol.ColumnScale5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnScale5 || {}).ColumnHeader} {(columnLabel.ColumnScale5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnScale5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnScale5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnScale5'
disabled = {(authCol.ColumnScale5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnScale5 && touched.cColumnScale5 && <span className='form__form-group-error'>{errors.cColumnScale5}</span>}
</div>
</Col>
}
{(authCol.DefaultValue5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultValue5 || {}).ColumnHeader} {(columnLabel.DefaultValue5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultValue5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultValue5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefaultValue5'
disabled = {(authCol.DefaultValue5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultValue5 && touched.cDefaultValue5 && <span className='form__form-group-error'>{errors.cDefaultValue5}</span>}
</div>
</Col>
}
{(authCol.AllowNulls5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AllowNulls5 || {}).ColumnHeader} {(columnLabel.AllowNulls5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowNulls5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowNulls5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAllowNulls5'
disabled = {(authCol.AllowNulls5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAllowNulls5 && touched.cAllowNulls5 && <span className='form__form-group-error'>{errors.cAllowNulls5}</span>}
</div>
</Col>
}
{(authCol.ColumnIdentity5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cColumnIdentity5'
onChange={handleChange}
defaultChecked={values.cColumnIdentity5}
disabled={(authCol.ColumnIdentity5 || {}).readonly || !(authCol.ColumnIdentity5 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ColumnIdentity5 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ColumnIdentity5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnIdentity5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnIdentity5 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.PrimaryKey5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cPrimaryKey5'
onChange={handleChange}
defaultChecked={values.cPrimaryKey5}
disabled={(authCol.PrimaryKey5 || {}).readonly || !(authCol.PrimaryKey5 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.PrimaryKey5 || {}).ColumnHeader}</span>
</label>
{(columnLabel.PrimaryKey5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PrimaryKey5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PrimaryKey5 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.IsIndexU5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsIndexU5'
onChange={handleChange}
defaultChecked={values.cIsIndexU5}
disabled={(authCol.IsIndexU5 || {}).readonly || !(authCol.IsIndexU5 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsIndexU5 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsIndexU5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsIndexU5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsIndexU5 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.IsIndex5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsIndex5'
onChange={handleChange}
defaultChecked={values.cIsIndex5}
disabled={(authCol.IsIndex5 || {}).readonly || !(authCol.IsIndex5 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsIndex5 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsIndex5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsIndex5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsIndex5 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColObjective5 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColObjective5 || {}).ColumnHeader} {(columnLabel.ColObjective5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColObjective5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColObjective5 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColObjective5'
disabled = {(authCol.ColObjective5 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColObjective5 && touched.cColObjective5 && <span className='form__form-group-error'>{errors.cColObjective5}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3,currDtl.ColumnId5)) return null;
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
  AdmDbTable: state.AdmDbTable,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmDbTableReduxObj.LoadPage.bind(AdmDbTableReduxObj) },
    { AddDtl: AdmDbTableReduxObj.AddDtl.bind(AdmDbTableReduxObj) },
    { SavePage: AdmDbTableReduxObj.SavePage.bind(AdmDbTableReduxObj) },

  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

