
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
import RintagiScreen from '../../components/custom/Screen';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import { isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getDefaultPath } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import AdmWizardObjReduxObj, { ShowMstFilterApplied } from '../../redux/AdmWizardObj';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmWizardObj || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'WizardId71';
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
    this.DropdownChange = this.DropdownChange.bind(this);
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
//  PostToAp({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
//    return function (evt) {
//      this.OnClickColumeName = 'PostToAp';
//      submitForm();
//      evt.preventDefault();
//    }.bind(this);
//  }

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmWizardObj || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cWizardTypeId71 || {}).value)) { errors.cWizardTypeId71 = (columnLabel.WizardTypeId71 || {}).ErrMessage;}
if (isEmptyId((values.cMasterTableId71 || {}).value)) { errors.cMasterTableId71 = (columnLabel.MasterTableId71 || {}).ErrMessage;}
if (!values.cWizardTitle71) { errors.cWizardTitle71 = (columnLabel.WizardTitle71 || {}).ErrMessage;}
if (!values.cProgramName71) { errors.cProgramName71 = (columnLabel.ProgramName71 || {}).ErrMessage;}
if (!values.cDefWorkSheet71) { errors.cDefWorkSheet71 = (columnLabel.DefWorkSheet71 || {}).ErrMessage;}
if (!values.cDefStartRow71) { errors.cDefStartRow71 = (columnLabel.DefStartRow71 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmWizardObj || {}).Mst || {};
/* ReactRule: Master Record Save */
/* ReactRule End: Master Record Save */

// No need to generate this, put this in the webrule
//    if ((+(currMst.TrxTotal64)) === 0 && (this.ScreenButton || {}).buttonType === 'SaveClose') {
//      errors.push('Please add at least one expense.');
//    } else if ((this.ScreenButton || {}).buttonType === 'Save' && values.cTrxNote64 !== 'ENTER-PURPOSE-OF-THIS-EXPENSE') {
//      // errors.push('Please do not change the Memo on Chq if Save Only');
//      // setFieldValue('cTrxNote64', 'ENTER-PURPOSE-OF-THIS-EXPENSE');
//    } else if ((this.ScreenButton || {}).buttonType === 'SaveClose' && values.cTrxNote64 === 'ENTER-PURPOSE-OF-THIS-EXPENSE') {
//      errors.push('Please change the Memo on Chq if Save & Pay Me');
//    }
    if (errors.length > 0) {
      this.props.showNotification('E', { message: errors[0] });
      setSubmitting(false);
    }
    else {
      const { ScreenButton, OnClickColumeName } = this;
      this.setState({submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting, key: currMst.key, ScreenButton: ScreenButton, OnClickColumeName: OnClickColumeName });
      this.ScreenButton = null;
      this.OnClickColumeName = null;
      this.props.SavePage(
        this.props.AdmWizardObj,
        {
          WizardId71: values.cWizardId71|| '',
          WizardTypeId71: (values.cWizardTypeId71|| {}).value || '',
          MasterTableId71: (values.cMasterTableId71|| {}).value || '',
          WizardTitle71: values.cWizardTitle71|| '',
          ProgramName71: values.cProgramName71|| '',
          DefWorkSheet71: values.cDefWorkSheet71|| '',
          DefStartRow71: values.cDefStartRow71|| '',
          DefOverwrite71: values.cDefOverwrite71 ? 'Y' : 'N',
          OvwrReadonly71: values.cOvwrReadonly71 ? 'Y' : 'N',
          AuthRequired71: values.cAuthRequired71 ? 'Y' : 'N',
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
    const AdmWizardObjState = this.props.AdmWizardObj || {};
    const auxSystemLabels = AdmWizardObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).WizardId71;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmWizardObj.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmWizardObj.Label);
            this.props.history.push(getEditMstPath(getNaviPath(naviBar, 'Mst', '/'), '_'));
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
    const AdmWizardObjState = this.props.AdmWizardObj || {};
    const auxSystemLabels = AdmWizardObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.WizardId71;
        this.props.DelMst(this.props.AdmWizardObj, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmWizardObj || {};
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
    const AdmWizardObjState = this.props.AdmWizardObj || {};
    const auxSystemLabels = AdmWizardObjState.SystemLabel || {};
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
      if (!(this.props.AdmWizardObj || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmWizardObj || {};

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
        const naviBar = getNaviBar('Mst', currMst, currDtl, currReduxScreenState.Label);
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
    const AdmWizardObjState = this.props.AdmWizardObj || {};

    if (AdmWizardObjState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmWizardObjState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmWizardObjReduxObj.GetScreenButtons(AdmWizardObjState) || {};
    const itemList = AdmWizardObjState.Dtl || [];
    const auxLabels = AdmWizardObjState.Label || {};
    const auxSystemLabels = AdmWizardObjState.SystemLabel || {};

    const columnLabel = AdmWizardObjState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmWizardObjState);
    const authRow = (AdmWizardObjState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmWizardObj || {}).Mst || {});
    const currDtl = ((this.props.AdmWizardObj || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.WizardId71));
    const selectList = AdmWizardObjReduxObj.SearchListToSelectList(AdmWizardObjState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const WizardId71 = currMst.WizardId71;
const WizardTypeId71List = AdmWizardObjReduxObj.ScreenDdlSelectors.WizardTypeId71(AdmWizardObjState);
const WizardTypeId71 = currMst.WizardTypeId71;
const MasterTableId71List = AdmWizardObjReduxObj.ScreenDdlSelectors.MasterTableId71(AdmWizardObjState);
const MasterTableId71 = currMst.MasterTableId71;
const WizardTitle71 = currMst.WizardTitle71;
const ProgramName71 = currMst.ProgramName71;
const DefWorkSheet71 = currMst.DefWorkSheet71;
const DefStartRow71 = currMst.DefStartRow71;
const DefOverwrite71 = currMst.DefOverwrite71;
const OvwrReadonly71 = currMst.OvwrReadonly71;
const AuthRequired71 = currMst.AuthRequired71;

    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
/* ReactRule: Master Render */
/* ReactRule End: Master Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmWizardObjState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cWizardId71: WizardId71 || '',
                  cWizardTypeId71: WizardTypeId71List.filter(obj => { return obj.key === WizardTypeId71 })[0],
                  cMasterTableId71: MasterTableId71List.filter(obj => { return obj.key === MasterTableId71 })[0],
                  cWizardTitle71: WizardTitle71 || '',
                  cProgramName71: ProgramName71 || '',
                  cDefWorkSheet71: DefWorkSheet71 || '',
                  cDefStartRow71: DefStartRow71 || '',
                  cDefOverwrite71: DefOverwrite71 === 'Y',
                  cOvwrReadonly71: OvwrReadonly71 === 'Y',
                  cAuthRequired71: AuthRequired71 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmWizardObjState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardId71)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardId71)) return null;
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

                          <div className='w-100'>
                            <Row>
            {(authCol.WizardId71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardId71 || {}).ColumnHeader} {(columnLabel.WizardId71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardId71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardId71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWizardId71'
disabled = {(authCol.WizardId71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWizardId71 && touched.cWizardId71 && <span className='form__form-group-error'>{errors.cWizardId71}</span>}
</div>
</Col>
}
{(authCol.WizardTypeId71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardTypeId71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.WizardTypeId71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardTypeId71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardTypeId71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cWizardTypeId71'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cWizardTypeId71')}
value={values.cWizardTypeId71}
options={WizardTypeId71List}
placeholder=''
disabled = {(authCol.WizardTypeId71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWizardTypeId71 && touched.cWizardTypeId71 && <span className='form__form-group-error'>{errors.cWizardTypeId71}</span>}
</div>
</Col>
}
{(authCol.MasterTableId71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterTableId71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MasterTableId71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterTableId71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterTableId71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cMasterTableId71'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cMasterTableId71')}
value={values.cMasterTableId71}
options={MasterTableId71List}
placeholder=''
disabled = {(authCol.MasterTableId71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterTableId71 && touched.cMasterTableId71 && <span className='form__form-group-error'>{errors.cMasterTableId71}</span>}
</div>
</Col>
}
{(authCol.WizardTitle71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardTitle71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.WizardTitle71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardTitle71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardTitle71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWizardTitle71'
disabled = {(authCol.WizardTitle71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWizardTitle71 && touched.cWizardTitle71 && <span className='form__form-group-error'>{errors.cWizardTitle71}</span>}
</div>
</Col>
}
{(authCol.ProgramName71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProgramName71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ProgramName71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProgramName71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProgramName71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cProgramName71'
disabled = {(authCol.ProgramName71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProgramName71 && touched.cProgramName71 && <span className='form__form-group-error'>{errors.cProgramName71}</span>}
</div>
</Col>
}
{(authCol.DefWorkSheet71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefWorkSheet71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DefWorkSheet71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefWorkSheet71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefWorkSheet71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefWorkSheet71'
disabled = {(authCol.DefWorkSheet71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefWorkSheet71 && touched.cDefWorkSheet71 && <span className='form__form-group-error'>{errors.cDefWorkSheet71}</span>}
</div>
</Col>
}
{(authCol.DefStartRow71 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefStartRow71 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DefStartRow71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefStartRow71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefStartRow71 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefStartRow71'
disabled = {(authCol.DefStartRow71 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefStartRow71 && touched.cDefStartRow71 && <span className='form__form-group-error'>{errors.cDefStartRow71}</span>}
</div>
</Col>
}
{(authCol.DefOverwrite71 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cDefOverwrite71'
onChange={handleChange}
defaultChecked={values.cDefOverwrite71}
disabled={(authCol.DefOverwrite71 || {}).readonly || !(authCol.DefOverwrite71 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.DefOverwrite71 || {}).ColumnHeader}</span>
</label>
{(columnLabel.DefOverwrite71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefOverwrite71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefOverwrite71 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.OvwrReadonly71 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cOvwrReadonly71'
onChange={handleChange}
defaultChecked={values.cOvwrReadonly71}
disabled={(authCol.OvwrReadonly71 || {}).readonly || !(authCol.OvwrReadonly71 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.OvwrReadonly71 || {}).ColumnHeader}</span>
</label>
{(columnLabel.OvwrReadonly71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OvwrReadonly71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OvwrReadonly71 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AuthRequired71 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAuthRequired71'
onChange={handleChange}
defaultChecked={values.cAuthRequired71}
disabled={(authCol.AuthRequired71 || {}).readonly || !(authCol.AuthRequired71 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AuthRequired71 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AuthRequired71 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AuthRequired71 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AuthRequired71 || {}).ToolTip} />
)}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardId71)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).WizardId71));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmWizardObjState) && <Skeleton height='43px' />) ||
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
  AdmWizardObj: state.AdmWizardObj,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmWizardObjReduxObj.LoadPage.bind(AdmWizardObjReduxObj) },
    { SavePage: AdmWizardObjReduxObj.SavePage.bind(AdmWizardObjReduxObj) },
    { DelMst: AdmWizardObjReduxObj.DelMst.bind(AdmWizardObjReduxObj) },
    { AddMst: AdmWizardObjReduxObj.AddMst.bind(AdmWizardObjReduxObj) },
//    { SearchMemberId64: AdmWizardObjReduxObj.SearchActions.SearchMemberId64.bind(AdmWizardObjReduxObj) },
//    { SearchCurrencyId64: AdmWizardObjReduxObj.SearchActions.SearchCurrencyId64.bind(AdmWizardObjReduxObj) },
//    { SearchCustomerJobId64: AdmWizardObjReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmWizardObjReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            