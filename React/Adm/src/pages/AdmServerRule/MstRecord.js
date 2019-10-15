
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
import AdmServerRuleReduxObj, { ShowMstFilterApplied } from '../../redux/AdmServerRule';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmServerRule || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ServerRuleId24';
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

ScreenId24InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId24(v, filterBy);}}
 SyncByDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncByDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 SyncToDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncToDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmServerRule || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRuleName24) { errors.cRuleName24 = (columnLabel.RuleName24 || {}).ErrMessage;}
if (isEmptyId((values.cRuleTypeId24 || {}).value)) { errors.cRuleTypeId24 = (columnLabel.RuleTypeId24 || {}).ErrMessage;}
if (!values.cRuleOrder24) { errors.cRuleOrder24 = (columnLabel.RuleOrder24 || {}).ErrMessage;}
if (!values.cProcedureName24) { errors.cProcedureName24 = (columnLabel.ProcedureName24 || {}).ErrMessage;}
if (isEmptyId((values.cBeforeCRUD24 || {}).value)) { errors.cBeforeCRUD24 = (columnLabel.BeforeCRUD24 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmServerRule || {}).Mst || {};
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
        this.props.AdmServerRule,
        {
          ServerRuleId24: values.cServerRuleId24|| '',
          RuleName24: values.cRuleName24|| '',
          RuleDescription24: values.cRuleDescription24|| '',
          RuleTypeId24: (values.cRuleTypeId24|| {}).value || '',
          ScreenId24: (values.cScreenId24|| {}).value || '',
          RuleOrder24: values.cRuleOrder24|| '',
          ProcedureName24: values.cProcedureName24|| '',
          ParameterNames24: values.cParameterNames24|| '',
          ParameterTypes24: values.cParameterTypes24|| '',
          CallingParams24: values.cCallingParams24|| '',
          MasterTable24: values.cMasterTable24 ? 'Y' : 'N',
          OnAdd24: values.cOnAdd24 ? 'Y' : 'N',
          OnUpd24: values.cOnUpd24 ? 'Y' : 'N',
          OnDel24: values.cOnDel24 ? 'Y' : 'N',
          BeforeCRUD24: (values.cBeforeCRUD24|| {}).value || '',
          RuleCode24: values.cRuleCode24|| '',
          ModifiedBy24: (values.cModifiedBy24|| {}).value || '',
          ModifiedOn24: values.cModifiedOn24|| '',
          LastGenDt24: values.cLastGenDt24|| '',
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
    const AdmServerRuleState = this.props.AdmServerRule || {};
    const auxSystemLabels = AdmServerRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ServerRuleId24;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmServerRule.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmServerRule.Label);
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
    const AdmServerRuleState = this.props.AdmServerRule || {};
    const auxSystemLabels = AdmServerRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ServerRuleId24;
        this.props.DelMst(this.props.AdmServerRule, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmServerRule || {};
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
    const AdmServerRuleState = this.props.AdmServerRule || {};
    const auxSystemLabels = AdmServerRuleState.SystemLabel || {};
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
      if (!(this.props.AdmServerRule || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmServerRule || {};

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
    const AdmServerRuleState = this.props.AdmServerRule || {};

    if (AdmServerRuleState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmServerRuleState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmServerRuleReduxObj.GetScreenButtons(AdmServerRuleState) || {};
    const itemList = AdmServerRuleState.Dtl || [];
    const auxLabels = AdmServerRuleState.Label || {};
    const auxSystemLabels = AdmServerRuleState.SystemLabel || {};

    const columnLabel = AdmServerRuleState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmServerRuleState);
    const authRow = (AdmServerRuleState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmServerRule || {}).Mst || {});
    const currDtl = ((this.props.AdmServerRule || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ServerRuleId24));
    const selectList = AdmServerRuleReduxObj.SearchListToSelectList(AdmServerRuleState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ServerRuleId24 = currMst.ServerRuleId24;
const RuleName24 = currMst.RuleName24;
const RuleDescription24 = currMst.RuleDescription24;
const RuleTypeId24List = AdmServerRuleReduxObj.ScreenDdlSelectors.RuleTypeId24(AdmServerRuleState);
const RuleTypeId24 = currMst.RuleTypeId24;
const ScreenId24List = AdmServerRuleReduxObj.ScreenDdlSelectors.ScreenId24(AdmServerRuleState);
const ScreenId24 = currMst.ScreenId24;
const RuleOrder24 = currMst.RuleOrder24;
const ProcedureName24 = currMst.ProcedureName24;
const ParameterNames24 = currMst.ParameterNames24;
const ParameterTypes24 = currMst.ParameterTypes24;
const CallingParams24 = currMst.CallingParams24;
const MasterTable24 = currMst.MasterTable24;
const OnAdd24 = currMst.OnAdd24;
const OnUpd24 = currMst.OnUpd24;
const OnDel24 = currMst.OnDel24;
const BeforeCRUD24List = AdmServerRuleReduxObj.ScreenDdlSelectors.BeforeCRUD24(AdmServerRuleState);
const BeforeCRUD24 = currMst.BeforeCRUD24;
const RuleCode24 = currMst.RuleCode24;
const ModifiedBy24List = AdmServerRuleReduxObj.ScreenDdlSelectors.ModifiedBy24(AdmServerRuleState);
const ModifiedBy24 = currMst.ModifiedBy24;
const ModifiedOn24 = currMst.ModifiedOn24;
const LastGenDt24 = currMst.LastGenDt24;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmServerRuleState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cServerRuleId24: ServerRuleId24 || '',
                  cRuleName24: RuleName24 || '',
                  cRuleDescription24: RuleDescription24 || '',
                  cRuleTypeId24: RuleTypeId24List.filter(obj => { return obj.key === RuleTypeId24 })[0],
                  cScreenId24: ScreenId24List.filter(obj => { return obj.key === ScreenId24 })[0],
                  cRuleOrder24: RuleOrder24 || '',
                  cProcedureName24: ProcedureName24 || '',
                  cParameterNames24: ParameterNames24 || '',
                  cParameterTypes24: ParameterTypes24 || '',
                  cCallingParams24: CallingParams24 || '',
                  cMasterTable24: MasterTable24 === 'Y',
                  cOnAdd24: OnAdd24 === 'Y',
                  cOnUpd24: OnUpd24 === 'Y',
                  cOnDel24: OnDel24 === 'Y',
                  cBeforeCRUD24: BeforeCRUD24List.filter(obj => { return obj.key === BeforeCRUD24 })[0],
                  cRuleCode24: RuleCode24 || '',
                  cModifiedBy24: ModifiedBy24List.filter(obj => { return obj.key === ModifiedBy24 })[0],
                  cModifiedOn24: ModifiedOn24 || new Date(),
                  cLastGenDt24: LastGenDt24 || '',
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
                                {(this.constructor.ShowSpinner(AdmServerRuleState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ServerRuleId24)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ServerRuleId24)) return null;
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
            {(authCol.ServerRuleId24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ServerRuleId24 || {}).ColumnHeader} {(columnLabel.ServerRuleId24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ServerRuleId24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ServerRuleId24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cServerRuleId24'
disabled = {(authCol.ServerRuleId24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cServerRuleId24 && touched.cServerRuleId24 && <span className='form__form-group-error'>{errors.cServerRuleId24}</span>}
</div>
</Col>
}
{(authCol.RuleName24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleName24 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleName24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleName24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleName24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleName24'
disabled = {(authCol.RuleName24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleName24 && touched.cRuleName24 && <span className='form__form-group-error'>{errors.cRuleName24}</span>}
</div>
</Col>
}
{(authCol.RuleDescription24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleDescription24 || {}).ColumnHeader} {(columnLabel.RuleDescription24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleDescription24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleDescription24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleDescription24'
disabled = {(authCol.RuleDescription24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleDescription24 && touched.cRuleDescription24 && <span className='form__form-group-error'>{errors.cRuleDescription24}</span>}
</div>
</Col>
}
{(authCol.RuleTypeId24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTypeId24 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleTypeId24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTypeId24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTypeId24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleTypeId24'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleTypeId24')}
value={values.cRuleTypeId24}
options={RuleTypeId24List}
placeholder=''
disabled = {(authCol.RuleTypeId24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTypeId24 && touched.cRuleTypeId24 && <span className='form__form-group-error'>{errors.cRuleTypeId24}</span>}
</div>
</Col>
}
{(authCol.ScreenId24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId24 || {}).ColumnHeader} {(columnLabel.ScreenId24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId24'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId24', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId24', true)}
onInputChange={this.ScreenId24InputChange()}
value={values.cScreenId24}
defaultSelected={ScreenId24List.filter(obj => { return obj.key === ScreenId24 })}
options={ScreenId24List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId24 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId24 && touched.cScreenId24 && <span className='form__form-group-error'>{errors.cScreenId24}</span>}
</div>
</Col>
}
{(authCol.RuleOrder24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleOrder24 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleOrder24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleOrder24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleOrder24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleOrder24'
disabled = {(authCol.RuleOrder24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleOrder24 && touched.cRuleOrder24 && <span className='form__form-group-error'>{errors.cRuleOrder24}</span>}
</div>
</Col>
}
{(authCol.ProcedureName24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProcedureName24 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ProcedureName24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProcedureName24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProcedureName24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cProcedureName24'
disabled = {(authCol.ProcedureName24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProcedureName24 && touched.cProcedureName24 && <span className='form__form-group-error'>{errors.cProcedureName24}</span>}
</div>
</Col>
}
{(authCol.ParameterNames24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ParameterNames24 || {}).ColumnHeader} {(columnLabel.ParameterNames24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ParameterNames24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ParameterNames24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cParameterNames24'
disabled = {(authCol.ParameterNames24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cParameterNames24 && touched.cParameterNames24 && <span className='form__form-group-error'>{errors.cParameterNames24}</span>}
</div>
</Col>
}
{(authCol.ParameterTypes24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ParameterTypes24 || {}).ColumnHeader} {(columnLabel.ParameterTypes24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ParameterTypes24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ParameterTypes24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cParameterTypes24'
disabled = {(authCol.ParameterTypes24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cParameterTypes24 && touched.cParameterTypes24 && <span className='form__form-group-error'>{errors.cParameterTypes24}</span>}
</div>
</Col>
}
{(authCol.CallingParams24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CallingParams24 || {}).ColumnHeader} {(columnLabel.CallingParams24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CallingParams24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CallingParams24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCallingParams24'
disabled = {(authCol.CallingParams24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCallingParams24 && touched.cCallingParams24 && <span className='form__form-group-error'>{errors.cCallingParams24}</span>}
</div>
</Col>
}
{(authCol.MasterTable24 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cMasterTable24'
onChange={handleChange}
defaultChecked={values.cMasterTable24}
disabled={(authCol.MasterTable24 || {}).readonly || !(authCol.MasterTable24 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.MasterTable24 || {}).ColumnHeader}</span>
</label>
{(columnLabel.MasterTable24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterTable24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterTable24 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.OnAdd24 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cOnAdd24'
onChange={handleChange}
defaultChecked={values.cOnAdd24}
disabled={(authCol.OnAdd24 || {}).readonly || !(authCol.OnAdd24 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.OnAdd24 || {}).ColumnHeader}</span>
</label>
{(columnLabel.OnAdd24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OnAdd24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OnAdd24 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.OnUpd24 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cOnUpd24'
onChange={handleChange}
defaultChecked={values.cOnUpd24}
disabled={(authCol.OnUpd24 || {}).readonly || !(authCol.OnUpd24 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.OnUpd24 || {}).ColumnHeader}</span>
</label>
{(columnLabel.OnUpd24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OnUpd24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OnUpd24 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.OnDel24 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cOnDel24'
onChange={handleChange}
defaultChecked={values.cOnDel24}
disabled={(authCol.OnDel24 || {}).readonly || !(authCol.OnDel24 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.OnDel24 || {}).ColumnHeader}</span>
</label>
{(columnLabel.OnDel24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OnDel24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OnDel24 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.BeforeCRUD24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BeforeCRUD24 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.BeforeCRUD24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BeforeCRUD24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BeforeCRUD24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBeforeCRUD24'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBeforeCRUD24')}
value={values.cBeforeCRUD24}
options={BeforeCRUD24List}
placeholder=''
disabled = {(authCol.BeforeCRUD24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBeforeCRUD24 && touched.cBeforeCRUD24 && <span className='form__form-group-error'>{errors.cBeforeCRUD24}</span>}
</div>
</Col>
}
{(authCol.CrudTypeDesc1289 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CrudTypeDesc1289 || {}).ColumnHeader} {(columnLabel.CrudTypeDesc1289 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CrudTypeDesc1289 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CrudTypeDesc1289 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.RuleCode24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleCode24 || {}).ColumnHeader} {(columnLabel.RuleCode24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleCode24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleCode24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleCode24'
disabled = {(authCol.RuleCode24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleCode24 && touched.cRuleCode24 && <span className='form__form-group-error'>{errors.cRuleCode24}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncByDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncByDb || {}).readonly || !(authCol.SyncByDb || {}).visible} onClick={this.SyncByDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncByDb || (columnLabel.SyncByDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncToDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncToDb || {}).readonly || !(authCol.SyncToDb || {}).visible} onClick={this.SyncToDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncToDb || (columnLabel.SyncToDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.ModifiedBy24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedBy24 || {}).ColumnHeader} {(columnLabel.ModifiedBy24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedBy24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedBy24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cModifiedBy24'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cModifiedBy24')}
value={values.cModifiedBy24}
options={ModifiedBy24List}
placeholder=''
disabled = {(authCol.ModifiedBy24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cModifiedBy24 && touched.cModifiedBy24 && <span className='form__form-group-error'>{errors.cModifiedBy24}</span>}
</div>
</Col>
}
{(authCol.ModifiedOn24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedOn24 || {}).ColumnHeader} {(columnLabel.ModifiedOn24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedOn24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedOn24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cModifiedOn24'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn24', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn24', true)}
value={values.cModifiedOn24}
selected={values.cModifiedOn24}
disabled = {(authCol.ModifiedOn24 || {}).readonly ? true: false }/>
</div>
}
{errors.cModifiedOn24 && touched.cModifiedOn24 && <span className='form__form-group-error'>{errors.cModifiedOn24}</span>}
</div>
</Col>
}
{(authCol.LastGenDt24 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastGenDt24 || {}).ColumnHeader} {(columnLabel.LastGenDt24 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastGenDt24 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastGenDt24 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmServerRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLastGenDt24'
disabled = {(authCol.LastGenDt24 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLastGenDt24 && touched.cLastGenDt24 && <span className='form__form-group-error'>{errors.cLastGenDt24}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ServerRuleId24)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ServerRuleId24));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmServerRuleState) && <Skeleton height='43px' />) ||
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
  AdmServerRule: state.AdmServerRule,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmServerRuleReduxObj.LoadPage.bind(AdmServerRuleReduxObj) },
    { SavePage: AdmServerRuleReduxObj.SavePage.bind(AdmServerRuleReduxObj) },
    { DelMst: AdmServerRuleReduxObj.DelMst.bind(AdmServerRuleReduxObj) },
    { AddMst: AdmServerRuleReduxObj.AddMst.bind(AdmServerRuleReduxObj) },
//    { SearchMemberId64: AdmServerRuleReduxObj.SearchActions.SearchMemberId64.bind(AdmServerRuleReduxObj) },
//    { SearchCurrencyId64: AdmServerRuleReduxObj.SearchActions.SearchCurrencyId64.bind(AdmServerRuleReduxObj) },
//    { SearchCustomerJobId64: AdmServerRuleReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmServerRuleReduxObj) },
{ SearchScreenId24: AdmServerRuleReduxObj.SearchActions.SearchScreenId24.bind(AdmServerRuleReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            