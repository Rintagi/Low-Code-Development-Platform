
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
import AdmClientRuleReduxObj, { ShowMstFilterApplied } from '../../redux/AdmClientRule';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmClientRule || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ClientRuleId127';
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

ScreenId127InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId127(v, filterBy);}}
ReportId127InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId127(v, filterBy);}}
CultureId127InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureId127(v, filterBy);}}
ScreenObjHlpId127InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmClientRule || {}).Mst || {}).ScreenId127; _this.props.SearchScreenObjHlpId127(v, filterBy);}}
ScreenCriHlpId127InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmClientRule || {}).Mst || {}).ScreenId127; _this.props.SearchScreenCriHlpId127(v, filterBy);}}
ReportCriHlpId127InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmClientRule || {}).Mst || {}).ReportId127; _this.props.SearchReportCriHlpId127(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmClientRule || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cRuleMethodId127 || {}).value)) { errors.cRuleMethodId127 = (columnLabel.RuleMethodId127 || {}).ErrMessage;}
if (!values.cRuleName127) { errors.cRuleName127 = (columnLabel.RuleName127 || {}).ErrMessage;}
if (isEmptyId((values.cRuleTypeId127 || {}).value)) { errors.cRuleTypeId127 = (columnLabel.RuleTypeId127 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmClientRule || {}).Mst || {};
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
        this.props.AdmClientRule,
        {
          ClientRuleId127: values.cClientRuleId127|| '',
          RuleMethodId127: (values.cRuleMethodId127|| {}).value || '',
          RuleMethodDesc1295: values.cRuleMethodDesc1295|| '',
          RuleName127: values.cRuleName127|| '',
          RuleDescription127: values.cRuleDescription127|| '',
          RuleTypeId127: (values.cRuleTypeId127|| {}).value || '',
          ScreenId127: (values.cScreenId127|| {}).value || '',
          ReportId127: (values.cReportId127|| {}).value || '',
          CultureId127: (values.cCultureId127|| {}).value || '',
          ScreenObjHlpId127: (values.cScreenObjHlpId127|| {}).value || '',
          ScreenCriHlpId127: (values.cScreenCriHlpId127|| {}).value || '',
          ReportCriHlpId127: (values.cReportCriHlpId127|| {}).value || '',
          RuleCntTypeId127: (values.cRuleCntTypeId127|| {}).value || '',
          RuleCntTypeDesc1294: values.cRuleCntTypeDesc1294|| '',
          ClientRuleProg127: values.cClientRuleProg127|| '',
          ClientScript127: (values.cClientScript127|| {}).value || '',
          ClientScriptHelp126: values.cClientScriptHelp126|| '',
          UserScriptEvent127: values.cUserScriptEvent127|| '',
          UserScriptName127: values.cUserScriptName127|| '',
          ScriptParam127: values.cScriptParam127|| '',
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
    const AdmClientRuleState = this.props.AdmClientRule || {};
    const auxSystemLabels = AdmClientRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ClientRuleId127;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmClientRule.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmClientRule.Label);
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
    const AdmClientRuleState = this.props.AdmClientRule || {};
    const auxSystemLabels = AdmClientRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ClientRuleId127;
        this.props.DelMst(this.props.AdmClientRule, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmClientRule || {};
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
    const AdmClientRuleState = this.props.AdmClientRule || {};
    const auxSystemLabels = AdmClientRuleState.SystemLabel || {};
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
      if (!(this.props.AdmClientRule || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmClientRule || {};

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
    const AdmClientRuleState = this.props.AdmClientRule || {};

    if (AdmClientRuleState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmClientRuleState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmClientRuleReduxObj.GetScreenButtons(AdmClientRuleState) || {};
    const itemList = AdmClientRuleState.Dtl || [];
    const auxLabels = AdmClientRuleState.Label || {};
    const auxSystemLabels = AdmClientRuleState.SystemLabel || {};

    const columnLabel = AdmClientRuleState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmClientRuleState);
    const authRow = (AdmClientRuleState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmClientRule || {}).Mst || {});
    const currDtl = ((this.props.AdmClientRule || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ClientRuleId127));
    const selectList = AdmClientRuleReduxObj.SearchListToSelectList(AdmClientRuleState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ClientRuleId127 = currMst.ClientRuleId127;
const RuleMethodId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.RuleMethodId127(AdmClientRuleState);
const RuleMethodId127 = currMst.RuleMethodId127;
const RuleMethodDesc1295 = currMst.RuleMethodDesc1295;
const RuleName127 = currMst.RuleName127;
const RuleDescription127 = currMst.RuleDescription127;
const RuleTypeId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.RuleTypeId127(AdmClientRuleState);
const RuleTypeId127 = currMst.RuleTypeId127;
const ScreenId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ScreenId127(AdmClientRuleState);
const ScreenId127 = currMst.ScreenId127;
const ReportId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ReportId127(AdmClientRuleState);
const ReportId127 = currMst.ReportId127;
const CultureId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.CultureId127(AdmClientRuleState);
const CultureId127 = currMst.CultureId127;
const ScreenObjHlpId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ScreenObjHlpId127(AdmClientRuleState);
const ScreenObjHlpId127 = currMst.ScreenObjHlpId127;
const ScreenCriHlpId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ScreenCriHlpId127(AdmClientRuleState);
const ScreenCriHlpId127 = currMst.ScreenCriHlpId127;
const ReportCriHlpId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ReportCriHlpId127(AdmClientRuleState);
const ReportCriHlpId127 = currMst.ReportCriHlpId127;
const RuleCntTypeId127List = AdmClientRuleReduxObj.ScreenDdlSelectors.RuleCntTypeId127(AdmClientRuleState);
const RuleCntTypeId127 = currMst.RuleCntTypeId127;
const RuleCntTypeDesc1294 = currMst.RuleCntTypeDesc1294;
const ClientRuleProg127 = currMst.ClientRuleProg127;
const ClientScript127List = AdmClientRuleReduxObj.ScreenDdlSelectors.ClientScript127(AdmClientRuleState);
const ClientScript127 = currMst.ClientScript127;
const ClientScriptHelp126 = currMst.ClientScriptHelp126;
const UserScriptEvent127 = currMst.UserScriptEvent127;
const UserScriptName127 = currMst.UserScriptName127;
const ScriptParam127 = currMst.ScriptParam127;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmClientRuleState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cClientRuleId127: ClientRuleId127 || '',
                  cRuleMethodId127: RuleMethodId127List.filter(obj => { return obj.key === RuleMethodId127 })[0],
                  cRuleMethodDesc1295: RuleMethodDesc1295 || '',
                  cRuleName127: RuleName127 || '',
                  cRuleDescription127: RuleDescription127 || '',
                  cRuleTypeId127: RuleTypeId127List.filter(obj => { return obj.key === RuleTypeId127 })[0],
                  cScreenId127: ScreenId127List.filter(obj => { return obj.key === ScreenId127 })[0],
                  cReportId127: ReportId127List.filter(obj => { return obj.key === ReportId127 })[0],
                  cCultureId127: CultureId127List.filter(obj => { return obj.key === CultureId127 })[0],
                  cScreenObjHlpId127: ScreenObjHlpId127List.filter(obj => { return obj.key === ScreenObjHlpId127 })[0],
                  cScreenCriHlpId127: ScreenCriHlpId127List.filter(obj => { return obj.key === ScreenCriHlpId127 })[0],
                  cReportCriHlpId127: ReportCriHlpId127List.filter(obj => { return obj.key === ReportCriHlpId127 })[0],
                  cRuleCntTypeId127: RuleCntTypeId127List.filter(obj => { return obj.key === RuleCntTypeId127 })[0],
                  cRuleCntTypeDesc1294: RuleCntTypeDesc1294 || '',
                  cClientRuleProg127: ClientRuleProg127 || '',
                  cClientScript127: ClientScript127List.filter(obj => { return obj.key === ClientScript127 })[0],
                  cClientScriptHelp126: ClientScriptHelp126 || '',
                  cUserScriptEvent127: UserScriptEvent127 || '',
                  cUserScriptName127: UserScriptName127 || '',
                  cScriptParam127: ScriptParam127 || '',
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
                                {(this.constructor.ShowSpinner(AdmClientRuleState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientRuleId127)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientRuleId127)) return null;
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
            {(authCol.ClientRuleId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientRuleId127 || {}).ColumnHeader} {(columnLabel.ClientRuleId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientRuleId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientRuleId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cClientRuleId127'
disabled = {(authCol.ClientRuleId127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientRuleId127 && touched.cClientRuleId127 && <span className='form__form-group-error'>{errors.cClientRuleId127}</span>}
</div>
</Col>
}
{(authCol.RuleMethodId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleMethodId127 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleMethodId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleMethodId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleMethodId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleMethodId127'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleMethodId127')}
value={values.cRuleMethodId127}
options={RuleMethodId127List}
placeholder=''
disabled = {(authCol.RuleMethodId127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleMethodId127 && touched.cRuleMethodId127 && <span className='form__form-group-error'>{errors.cRuleMethodId127}</span>}
</div>
</Col>
}
{(authCol.RuleMethodDesc1295 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleMethodDesc1295 || {}).ColumnHeader} {(columnLabel.RuleMethodDesc1295 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleMethodDesc1295 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleMethodDesc1295 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleMethodDesc1295'
disabled = {(authCol.RuleMethodDesc1295 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleMethodDesc1295 && touched.cRuleMethodDesc1295 && <span className='form__form-group-error'>{errors.cRuleMethodDesc1295}</span>}
</div>
</Col>
}
{(authCol.RuleName127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleName127 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleName127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleName127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleName127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleName127'
disabled = {(authCol.RuleName127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleName127 && touched.cRuleName127 && <span className='form__form-group-error'>{errors.cRuleName127}</span>}
</div>
</Col>
}
{(authCol.RuleDescription127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleDescription127 || {}).ColumnHeader} {(columnLabel.RuleDescription127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleDescription127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleDescription127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleDescription127'
disabled = {(authCol.RuleDescription127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleDescription127 && touched.cRuleDescription127 && <span className='form__form-group-error'>{errors.cRuleDescription127}</span>}
</div>
</Col>
}
{(authCol.RuleTypeId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTypeId127 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleTypeId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTypeId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTypeId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleTypeId127'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleTypeId127')}
value={values.cRuleTypeId127}
options={RuleTypeId127List}
placeholder=''
disabled = {(authCol.RuleTypeId127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTypeId127 && touched.cRuleTypeId127 && <span className='form__form-group-error'>{errors.cRuleTypeId127}</span>}
</div>
</Col>
}
{(authCol.ScreenId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId127 || {}).ColumnHeader} {(columnLabel.ScreenId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId127', true)}
onInputChange={this.ScreenId127InputChange()}
value={values.cScreenId127}
defaultSelected={ScreenId127List.filter(obj => { return obj.key === ScreenId127 })}
options={ScreenId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId127 && touched.cScreenId127 && <span className='form__form-group-error'>{errors.cScreenId127}</span>}
</div>
</Col>
}
{(authCol.ReportId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId127 || {}).ColumnHeader} {(columnLabel.ReportId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId127', true)}
onInputChange={this.ReportId127InputChange()}
value={values.cReportId127}
defaultSelected={ReportId127List.filter(obj => { return obj.key === ReportId127 })}
options={ReportId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId127 && touched.cReportId127 && <span className='form__form-group-error'>{errors.cReportId127}</span>}
</div>
</Col>
}
{(authCol.CultureId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureId127 || {}).ColumnHeader} {(columnLabel.CultureId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId127', true)}
onInputChange={this.CultureId127InputChange()}
value={values.cCultureId127}
defaultSelected={CultureId127List.filter(obj => { return obj.key === CultureId127 })}
options={CultureId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureId127 && touched.cCultureId127 && <span className='form__form-group-error'>{errors.cCultureId127}</span>}
</div>
</Col>
}
{(authCol.ScreenObjHlpId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenObjHlpId127 || {}).ColumnHeader} {(columnLabel.ScreenObjHlpId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenObjHlpId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenObjHlpId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenObjHlpId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenObjHlpId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenObjHlpId127', true)}
onInputChange={this.ScreenObjHlpId127InputChange()}
value={values.cScreenObjHlpId127}
defaultSelected={ScreenObjHlpId127List.filter(obj => { return obj.key === ScreenObjHlpId127 })}
options={ScreenObjHlpId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenObjHlpId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenObjHlpId127 && touched.cScreenObjHlpId127 && <span className='form__form-group-error'>{errors.cScreenObjHlpId127}</span>}
</div>
</Col>
}
{(authCol.ScreenCriHlpId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenCriHlpId127 || {}).ColumnHeader} {(columnLabel.ScreenCriHlpId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenCriHlpId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenCriHlpId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenCriHlpId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenCriHlpId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenCriHlpId127', true)}
onInputChange={this.ScreenCriHlpId127InputChange()}
value={values.cScreenCriHlpId127}
defaultSelected={ScreenCriHlpId127List.filter(obj => { return obj.key === ScreenCriHlpId127 })}
options={ScreenCriHlpId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenCriHlpId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenCriHlpId127 && touched.cScreenCriHlpId127 && <span className='form__form-group-error'>{errors.cScreenCriHlpId127}</span>}
</div>
</Col>
}
{(authCol.ReportCriHlpId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportCriHlpId127 || {}).ColumnHeader} {(columnLabel.ReportCriHlpId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportCriHlpId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportCriHlpId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportCriHlpId127'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportCriHlpId127', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportCriHlpId127', true)}
onInputChange={this.ReportCriHlpId127InputChange()}
value={values.cReportCriHlpId127}
defaultSelected={ReportCriHlpId127List.filter(obj => { return obj.key === ReportCriHlpId127 })}
options={ReportCriHlpId127List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportCriHlpId127 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportCriHlpId127 && touched.cReportCriHlpId127 && <span className='form__form-group-error'>{errors.cReportCriHlpId127}</span>}
</div>
</Col>
}
{(authCol.RuleCntTypeId127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleCntTypeId127 || {}).ColumnHeader} {(columnLabel.RuleCntTypeId127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleCntTypeId127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleCntTypeId127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleCntTypeId127'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleCntTypeId127')}
value={values.cRuleCntTypeId127}
options={RuleCntTypeId127List}
placeholder=''
disabled = {(authCol.RuleCntTypeId127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleCntTypeId127 && touched.cRuleCntTypeId127 && <span className='form__form-group-error'>{errors.cRuleCntTypeId127}</span>}
</div>
</Col>
}
{(authCol.RuleCntTypeDesc1294 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleCntTypeDesc1294 || {}).ColumnHeader} {(columnLabel.RuleCntTypeDesc1294 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleCntTypeDesc1294 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleCntTypeDesc1294 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleCntTypeDesc1294'
disabled = {(authCol.RuleCntTypeDesc1294 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleCntTypeDesc1294 && touched.cRuleCntTypeDesc1294 && <span className='form__form-group-error'>{errors.cRuleCntTypeDesc1294}</span>}
</div>
</Col>
}
{(authCol.ClientRuleProg127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientRuleProg127 || {}).ColumnHeader} {(columnLabel.ClientRuleProg127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientRuleProg127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientRuleProg127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cClientRuleProg127'
disabled = {(authCol.ClientRuleProg127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientRuleProg127 && touched.cClientRuleProg127 && <span className='form__form-group-error'>{errors.cClientRuleProg127}</span>}
</div>
</Col>
}
{(authCol.ClientScript127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientScript127 || {}).ColumnHeader} {(columnLabel.ClientScript127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientScript127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientScript127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cClientScript127'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cClientScript127')}
value={values.cClientScript127}
options={ClientScript127List}
placeholder=''
disabled = {(authCol.ClientScript127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientScript127 && touched.cClientScript127 && <span className='form__form-group-error'>{errors.cClientScript127}</span>}
</div>
</Col>
}
{(authCol.ClientScriptHelp126 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientScriptHelp126 || {}).ColumnHeader} {(columnLabel.ClientScriptHelp126 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientScriptHelp126 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientScriptHelp126 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cClientScriptHelp126'
disabled = {(authCol.ClientScriptHelp126 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientScriptHelp126 && touched.cClientScriptHelp126 && <span className='form__form-group-error'>{errors.cClientScriptHelp126}</span>}
</div>
</Col>
}
{(authCol.UserScriptEvent127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UserScriptEvent127 || {}).ColumnHeader} {(columnLabel.UserScriptEvent127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UserScriptEvent127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UserScriptEvent127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUserScriptEvent127'
disabled = {(authCol.UserScriptEvent127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUserScriptEvent127 && touched.cUserScriptEvent127 && <span className='form__form-group-error'>{errors.cUserScriptEvent127}</span>}
</div>
</Col>
}
{(authCol.UserScriptName127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UserScriptName127 || {}).ColumnHeader} {(columnLabel.UserScriptName127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UserScriptName127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UserScriptName127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUserScriptName127'
disabled = {(authCol.UserScriptName127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUserScriptName127 && touched.cUserScriptName127 && <span className='form__form-group-error'>{errors.cUserScriptName127}</span>}
</div>
</Col>
}
{(authCol.ScriptParam127 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScriptParam127 || {}).ColumnHeader} {(columnLabel.ScriptParam127 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScriptParam127 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScriptParam127 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClientRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScriptParam127'
disabled = {(authCol.ScriptParam127 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScriptParam127 && touched.cScriptParam127 && <span className='form__form-group-error'>{errors.cScriptParam127}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientRuleId127)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ClientRuleId127));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmClientRuleState) && <Skeleton height='43px' />) ||
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
  AdmClientRule: state.AdmClientRule,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmClientRuleReduxObj.LoadPage.bind(AdmClientRuleReduxObj) },
    { SavePage: AdmClientRuleReduxObj.SavePage.bind(AdmClientRuleReduxObj) },
    { DelMst: AdmClientRuleReduxObj.DelMst.bind(AdmClientRuleReduxObj) },
    { AddMst: AdmClientRuleReduxObj.AddMst.bind(AdmClientRuleReduxObj) },
//    { SearchMemberId64: AdmClientRuleReduxObj.SearchActions.SearchMemberId64.bind(AdmClientRuleReduxObj) },
//    { SearchCurrencyId64: AdmClientRuleReduxObj.SearchActions.SearchCurrencyId64.bind(AdmClientRuleReduxObj) },
//    { SearchCustomerJobId64: AdmClientRuleReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmClientRuleReduxObj) },
{ SearchScreenId127: AdmClientRuleReduxObj.SearchActions.SearchScreenId127.bind(AdmClientRuleReduxObj) },
{ SearchReportId127: AdmClientRuleReduxObj.SearchActions.SearchReportId127.bind(AdmClientRuleReduxObj) },
{ SearchCultureId127: AdmClientRuleReduxObj.SearchActions.SearchCultureId127.bind(AdmClientRuleReduxObj) },
{ SearchScreenObjHlpId127: AdmClientRuleReduxObj.SearchActions.SearchScreenObjHlpId127.bind(AdmClientRuleReduxObj) },
{ SearchScreenCriHlpId127: AdmClientRuleReduxObj.SearchActions.SearchScreenCriHlpId127.bind(AdmClientRuleReduxObj) },
{ SearchReportCriHlpId127: AdmClientRuleReduxObj.SearchActions.SearchReportCriHlpId127.bind(AdmClientRuleReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            