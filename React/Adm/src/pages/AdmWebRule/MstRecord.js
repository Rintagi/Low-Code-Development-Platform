
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
import AdmWebRuleReduxObj, { ShowMstFilterApplied } from '../../redux/AdmWebRule';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmWebRule || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'WebRuleId128';
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

ScreenId128InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId128(v, filterBy);}}
ScreenObjId128InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmWebRule || {}).Mst || {}).ScreenId128; _this.props.SearchScreenObjId128(v, filterBy);}}
 Snippet1({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'Snippet1';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 Snippet4({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'Snippet4';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 Snippet2({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'Snippet2';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 Snippet3({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'Snippet3';
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
    const columnLabel = (this.props.AdmWebRule || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRuleName128) { errors.cRuleName128 = (columnLabel.RuleName128 || {}).ErrMessage;}
if (isEmptyId((values.cRuleTypeId128 || {}).value)) { errors.cRuleTypeId128 = (columnLabel.RuleTypeId128 || {}).ErrMessage;}
if (isEmptyId((values.cScreenId128 || {}).value)) { errors.cScreenId128 = (columnLabel.ScreenId128 || {}).ErrMessage;}
if (isEmptyId((values.cEventId128 || {}).value)) { errors.cEventId128 = (columnLabel.EventId128 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmWebRule || {}).Mst || {};
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
        this.props.AdmWebRule,
        {
          WebRuleId128: values.cWebRuleId128|| '',
          RuleName128: values.cRuleName128|| '',
          RuleDescription128: values.cRuleDescription128|| '',
          RuleTypeId128: (values.cRuleTypeId128|| {}).value || '',
          ScreenId128: (values.cScreenId128|| {}).value || '',
          ScreenObjId128: (values.cScreenObjId128|| {}).value || '',
          ButtonTypeId128: (values.cButtonTypeId128|| {}).value || '',
          EventId128: (values.cEventId128|| {}).value || '',
          WebRuleProg128: values.cWebRuleProg128|| '',
          ReactEventId128: (values.cReactEventId128|| {}).value || '',
          ReactRuleProg128: values.cReactRuleProg128|| '',
          ReduxEventId128: (values.cReduxEventId128|| {}).value || '',
          ReduxRuleProg128: values.cReduxRuleProg128|| '',
          ServiceEventId128: (values.cServiceEventId128|| {}).value || '',
          ServiceRuleProg128: values.cServiceRuleProg128|| '',
          AsmxEventId128: (values.cAsmxEventId128|| {}).value || '',
          AsmxRuleProg128: values.cAsmxRuleProg128|| '',
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
    const AdmWebRuleState = this.props.AdmWebRule || {};
    const auxSystemLabels = AdmWebRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).WebRuleId128;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmWebRule.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmWebRule.Label);
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
    const AdmWebRuleState = this.props.AdmWebRule || {};
    const auxSystemLabels = AdmWebRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.WebRuleId128;
        this.props.DelMst(this.props.AdmWebRule, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmWebRule || {};
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
    const AdmWebRuleState = this.props.AdmWebRule || {};
    const auxSystemLabels = AdmWebRuleState.SystemLabel || {};
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
      if (!(this.props.AdmWebRule || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmWebRule || {};

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
    const AdmWebRuleState = this.props.AdmWebRule || {};

    if (AdmWebRuleState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmWebRuleState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmWebRuleReduxObj.GetScreenButtons(AdmWebRuleState) || {};
    const itemList = AdmWebRuleState.Dtl || [];
    const auxLabels = AdmWebRuleState.Label || {};
    const auxSystemLabels = AdmWebRuleState.SystemLabel || {};

    const columnLabel = AdmWebRuleState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmWebRuleState);
    const authRow = (AdmWebRuleState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmWebRule || {}).Mst || {});
    const currDtl = ((this.props.AdmWebRule || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.WebRuleId128));
    const selectList = AdmWebRuleReduxObj.SearchListToSelectList(AdmWebRuleState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const WebRuleId128 = currMst.WebRuleId128;
const RuleName128 = currMst.RuleName128;
const RuleDescription128 = currMst.RuleDescription128;
const RuleTypeId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.RuleTypeId128(AdmWebRuleState);
const RuleTypeId128 = currMst.RuleTypeId128;
const ScreenId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ScreenId128(AdmWebRuleState);
const ScreenId128 = currMst.ScreenId128;
const ScreenObjId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ScreenObjId128(AdmWebRuleState);
const ScreenObjId128 = currMst.ScreenObjId128;
const ButtonTypeId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ButtonTypeId128(AdmWebRuleState);
const ButtonTypeId128 = currMst.ButtonTypeId128;
const EventId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.EventId128(AdmWebRuleState);
const EventId128 = currMst.EventId128;
const WebRuleProg128 = currMst.WebRuleProg128;
const ReactEventId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ReactEventId128(AdmWebRuleState);
const ReactEventId128 = currMst.ReactEventId128;
const ReactRuleProg128 = currMst.ReactRuleProg128;
const ReduxEventId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ReduxEventId128(AdmWebRuleState);
const ReduxEventId128 = currMst.ReduxEventId128;
const ReduxRuleProg128 = currMst.ReduxRuleProg128;
const ServiceEventId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.ServiceEventId128(AdmWebRuleState);
const ServiceEventId128 = currMst.ServiceEventId128;
const ServiceRuleProg128 = currMst.ServiceRuleProg128;
const AsmxEventId128List = AdmWebRuleReduxObj.ScreenDdlSelectors.AsmxEventId128(AdmWebRuleState);
const AsmxEventId128 = currMst.AsmxEventId128;
const AsmxRuleProg128 = currMst.AsmxRuleProg128;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmWebRuleState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cWebRuleId128: WebRuleId128 || '',
                  cRuleName128: RuleName128 || '',
                  cRuleDescription128: RuleDescription128 || '',
                  cRuleTypeId128: RuleTypeId128List.filter(obj => { return obj.key === RuleTypeId128 })[0],
                  cScreenId128: ScreenId128List.filter(obj => { return obj.key === ScreenId128 })[0],
                  cScreenObjId128: ScreenObjId128List.filter(obj => { return obj.key === ScreenObjId128 })[0],
                  cButtonTypeId128: ButtonTypeId128List.filter(obj => { return obj.key === ButtonTypeId128 })[0],
                  cEventId128: EventId128List.filter(obj => { return obj.key === EventId128 })[0],
                  cWebRuleProg128: WebRuleProg128 || '',
                  cReactEventId128: ReactEventId128List.filter(obj => { return obj.key === ReactEventId128 })[0],
                  cReactRuleProg128: ReactRuleProg128 || '',
                  cReduxEventId128: ReduxEventId128List.filter(obj => { return obj.key === ReduxEventId128 })[0],
                  cReduxRuleProg128: ReduxRuleProg128 || '',
                  cServiceEventId128: ServiceEventId128List.filter(obj => { return obj.key === ServiceEventId128 })[0],
                  cServiceRuleProg128: ServiceRuleProg128 || '',
                  cAsmxEventId128: AsmxEventId128List.filter(obj => { return obj.key === AsmxEventId128 })[0],
                  cAsmxRuleProg128: AsmxRuleProg128 || '',
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
                                {(this.constructor.ShowSpinner(AdmWebRuleState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WebRuleId128)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WebRuleId128)) return null;
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
            {(authCol.WebRuleId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WebRuleId128 || {}).ColumnHeader} {(columnLabel.WebRuleId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WebRuleId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WebRuleId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWebRuleId128'
disabled = {(authCol.WebRuleId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWebRuleId128 && touched.cWebRuleId128 && <span className='form__form-group-error'>{errors.cWebRuleId128}</span>}
</div>
</Col>
}
{(authCol.RuleName128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleName128 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleName128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleName128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleName128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleName128'
disabled = {(authCol.RuleName128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleName128 && touched.cRuleName128 && <span className='form__form-group-error'>{errors.cRuleName128}</span>}
</div>
</Col>
}
{(authCol.RuleDescription128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleDescription128 || {}).ColumnHeader} {(columnLabel.RuleDescription128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleDescription128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleDescription128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleDescription128'
disabled = {(authCol.RuleDescription128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleDescription128 && touched.cRuleDescription128 && <span className='form__form-group-error'>{errors.cRuleDescription128}</span>}
</div>
</Col>
}
{(authCol.RuleTypeId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTypeId128 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleTypeId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTypeId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTypeId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleTypeId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleTypeId128')}
value={values.cRuleTypeId128}
options={RuleTypeId128List}
placeholder=''
disabled = {(authCol.RuleTypeId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTypeId128 && touched.cRuleTypeId128 && <span className='form__form-group-error'>{errors.cRuleTypeId128}</span>}
</div>
</Col>
}
{(authCol.ScreenId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId128 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId128'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId128', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId128', true)}
onInputChange={this.ScreenId128InputChange()}
value={values.cScreenId128}
defaultSelected={ScreenId128List.filter(obj => { return obj.key === ScreenId128 })}
options={ScreenId128List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId128 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId128 && touched.cScreenId128 && <span className='form__form-group-error'>{errors.cScreenId128}</span>}
</div>
</Col>
}
{(authCol.ScreenObjId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenObjId128 || {}).ColumnHeader} {(columnLabel.ScreenObjId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenObjId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenObjId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenObjId128'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenObjId128', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenObjId128', true)}
onInputChange={this.ScreenObjId128InputChange()}
value={values.cScreenObjId128}
defaultSelected={ScreenObjId128List.filter(obj => { return obj.key === ScreenObjId128 })}
options={ScreenObjId128List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenObjId128 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenObjId128 && touched.cScreenObjId128 && <span className='form__form-group-error'>{errors.cScreenObjId128}</span>}
</div>
</Col>
}
{(authCol.ButtonTypeId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ButtonTypeId128 || {}).ColumnHeader} {(columnLabel.ButtonTypeId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ButtonTypeId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ButtonTypeId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cButtonTypeId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cButtonTypeId128')}
value={values.cButtonTypeId128}
options={ButtonTypeId128List}
placeholder=''
disabled = {(authCol.ButtonTypeId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cButtonTypeId128 && touched.cButtonTypeId128 && <span className='form__form-group-error'>{errors.cButtonTypeId128}</span>}
</div>
</Col>
}
{(authCol.EventId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EventId128 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.EventId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EventId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EventId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cEventId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cEventId128')}
value={values.cEventId128}
options={EventId128List}
placeholder=''
disabled = {(authCol.EventId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEventId128 && touched.cEventId128 && <span className='form__form-group-error'>{errors.cEventId128}</span>}
</div>
</Col>
}
{(authCol.WebRuleProg128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WebRuleProg128 || {}).ColumnHeader} {(columnLabel.WebRuleProg128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WebRuleProg128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WebRuleProg128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWebRuleProg128'
disabled = {(authCol.WebRuleProg128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWebRuleProg128 && touched.cWebRuleProg128 && <span className='form__form-group-error'>{errors.cWebRuleProg128}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.Snippet1 || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.Snippet1 || {}).readonly || !(authCol.Snippet1 || {}).visible} onClick={this.Snippet1({ naviBar, submitForm, currMst })} >{auxLabels.Snippet1 || (columnLabel.Snippet1 || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.Snippet4 || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.Snippet4 || {}).readonly || !(authCol.Snippet4 || {}).visible} onClick={this.Snippet4({ naviBar, submitForm, currMst })} >{auxLabels.Snippet4 || (columnLabel.Snippet4 || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.Snippet2 || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.Snippet2 || {}).readonly || !(authCol.Snippet2 || {}).visible} onClick={this.Snippet2({ naviBar, submitForm, currMst })} >{auxLabels.Snippet2 || (columnLabel.Snippet2 || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.Snippet3 || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.Snippet3 || {}).readonly || !(authCol.Snippet3 || {}).visible} onClick={this.Snippet3({ naviBar, submitForm, currMst })} >{auxLabels.Snippet3 || (columnLabel.Snippet3 || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.ReactEventId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReactEventId128 || {}).ColumnHeader} {(columnLabel.ReactEventId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReactEventId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReactEventId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReactEventId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReactEventId128')}
value={values.cReactEventId128}
options={ReactEventId128List}
placeholder=''
disabled = {(authCol.ReactEventId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReactEventId128 && touched.cReactEventId128 && <span className='form__form-group-error'>{errors.cReactEventId128}</span>}
</div>
</Col>
}
{(authCol.ReactRuleProg128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReactRuleProg128 || {}).ColumnHeader} {(columnLabel.ReactRuleProg128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReactRuleProg128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReactRuleProg128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReactRuleProg128'
disabled = {(authCol.ReactRuleProg128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReactRuleProg128 && touched.cReactRuleProg128 && <span className='form__form-group-error'>{errors.cReactRuleProg128}</span>}
</div>
</Col>
}
{(authCol.ReduxEventId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReduxEventId128 || {}).ColumnHeader} {(columnLabel.ReduxEventId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReduxEventId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReduxEventId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReduxEventId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReduxEventId128')}
value={values.cReduxEventId128}
options={ReduxEventId128List}
placeholder=''
disabled = {(authCol.ReduxEventId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReduxEventId128 && touched.cReduxEventId128 && <span className='form__form-group-error'>{errors.cReduxEventId128}</span>}
</div>
</Col>
}
{(authCol.ReduxRuleProg128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReduxRuleProg128 || {}).ColumnHeader} {(columnLabel.ReduxRuleProg128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReduxRuleProg128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReduxRuleProg128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReduxRuleProg128'
disabled = {(authCol.ReduxRuleProg128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReduxRuleProg128 && touched.cReduxRuleProg128 && <span className='form__form-group-error'>{errors.cReduxRuleProg128}</span>}
</div>
</Col>
}
{(authCol.ServiceEventId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ServiceEventId128 || {}).ColumnHeader} {(columnLabel.ServiceEventId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ServiceEventId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ServiceEventId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cServiceEventId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cServiceEventId128')}
value={values.cServiceEventId128}
options={ServiceEventId128List}
placeholder=''
disabled = {(authCol.ServiceEventId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cServiceEventId128 && touched.cServiceEventId128 && <span className='form__form-group-error'>{errors.cServiceEventId128}</span>}
</div>
</Col>
}
{(authCol.ServiceRuleProg128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ServiceRuleProg128 || {}).ColumnHeader} {(columnLabel.ServiceRuleProg128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ServiceRuleProg128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ServiceRuleProg128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cServiceRuleProg128'
disabled = {(authCol.ServiceRuleProg128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cServiceRuleProg128 && touched.cServiceRuleProg128 && <span className='form__form-group-error'>{errors.cServiceRuleProg128}</span>}
</div>
</Col>
}
{(authCol.AsmxEventId128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AsmxEventId128 || {}).ColumnHeader} {(columnLabel.AsmxEventId128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AsmxEventId128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AsmxEventId128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cAsmxEventId128'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cAsmxEventId128')}
value={values.cAsmxEventId128}
options={AsmxEventId128List}
placeholder=''
disabled = {(authCol.AsmxEventId128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAsmxEventId128 && touched.cAsmxEventId128 && <span className='form__form-group-error'>{errors.cAsmxEventId128}</span>}
</div>
</Col>
}
{(authCol.AsmxRuleProg128 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AsmxRuleProg128 || {}).ColumnHeader} {(columnLabel.AsmxRuleProg128 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AsmxRuleProg128 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AsmxRuleProg128 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWebRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAsmxRuleProg128'
disabled = {(authCol.AsmxRuleProg128 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAsmxRuleProg128 && touched.cAsmxRuleProg128 && <span className='form__form-group-error'>{errors.cAsmxRuleProg128}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WebRuleId128)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).WebRuleId128));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmWebRuleState) && <Skeleton height='43px' />) ||
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
  AdmWebRule: state.AdmWebRule,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmWebRuleReduxObj.LoadPage.bind(AdmWebRuleReduxObj) },
    { SavePage: AdmWebRuleReduxObj.SavePage.bind(AdmWebRuleReduxObj) },
    { DelMst: AdmWebRuleReduxObj.DelMst.bind(AdmWebRuleReduxObj) },
    { AddMst: AdmWebRuleReduxObj.AddMst.bind(AdmWebRuleReduxObj) },
//    { SearchMemberId64: AdmWebRuleReduxObj.SearchActions.SearchMemberId64.bind(AdmWebRuleReduxObj) },
//    { SearchCurrencyId64: AdmWebRuleReduxObj.SearchActions.SearchCurrencyId64.bind(AdmWebRuleReduxObj) },
//    { SearchCustomerJobId64: AdmWebRuleReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmWebRuleReduxObj) },
{ SearchScreenId128: AdmWebRuleReduxObj.SearchActions.SearchScreenId128.bind(AdmWebRuleReduxObj) },
{ SearchScreenObjId128: AdmWebRuleReduxObj.SearchActions.SearchScreenObjId128.bind(AdmWebRuleReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            