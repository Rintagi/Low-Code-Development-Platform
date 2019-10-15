
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
import AdmWizardRuleReduxObj, { ShowMstFilterApplied } from '../../redux/AdmWizardRule';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmWizardRule || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'WizardRuleId73';
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
    const columnLabel = (this.props.AdmWizardRule || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRuleName73) { errors.cRuleName73 = (columnLabel.RuleName73 || {}).ErrMessage;}
if (isEmptyId((values.cRuleTypeId73 || {}).value)) { errors.cRuleTypeId73 = (columnLabel.RuleTypeId73 || {}).ErrMessage;}
if (isEmptyId((values.cWizardId73 || {}).value)) { errors.cWizardId73 = (columnLabel.WizardId73 || {}).ErrMessage;}
if (!values.cRuleOrder73) { errors.cRuleOrder73 = (columnLabel.RuleOrder73 || {}).ErrMessage;}
if (!values.cProcedureName73) { errors.cProcedureName73 = (columnLabel.ProcedureName73 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmWizardRule || {}).Mst || {};
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
        this.props.AdmWizardRule,
        {
          WizardRuleId73: values.cWizardRuleId73|| '',
          RuleName73: values.cRuleName73|| '',
          RuleDescription73: values.cRuleDescription73|| '',
          RuleTypeId73: (values.cRuleTypeId73|| {}).value || '',
          WizardId73: (values.cWizardId73|| {}).value || '',
          RuleOrder73: values.cRuleOrder73|| '',
          ProcedureName73: values.cProcedureName73|| '',
          BeforeCRUD73: values.cBeforeCRUD73 ? 'Y' : 'N',
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
    const AdmWizardRuleState = this.props.AdmWizardRule || {};
    const auxSystemLabels = AdmWizardRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).WizardRuleId73;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmWizardRule.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmWizardRule.Label);
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
    const AdmWizardRuleState = this.props.AdmWizardRule || {};
    const auxSystemLabels = AdmWizardRuleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.WizardRuleId73;
        this.props.DelMst(this.props.AdmWizardRule, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmWizardRule || {};
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
    const AdmWizardRuleState = this.props.AdmWizardRule || {};
    const auxSystemLabels = AdmWizardRuleState.SystemLabel || {};
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
      if (!(this.props.AdmWizardRule || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmWizardRule || {};

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
    const AdmWizardRuleState = this.props.AdmWizardRule || {};

    if (AdmWizardRuleState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmWizardRuleState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmWizardRuleReduxObj.GetScreenButtons(AdmWizardRuleState) || {};
    const itemList = AdmWizardRuleState.Dtl || [];
    const auxLabels = AdmWizardRuleState.Label || {};
    const auxSystemLabels = AdmWizardRuleState.SystemLabel || {};

    const columnLabel = AdmWizardRuleState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmWizardRuleState);
    const authRow = (AdmWizardRuleState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmWizardRule || {}).Mst || {});
    const currDtl = ((this.props.AdmWizardRule || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.WizardRuleId73));
    const selectList = AdmWizardRuleReduxObj.SearchListToSelectList(AdmWizardRuleState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const WizardRuleId73 = currMst.WizardRuleId73;
const RuleName73 = currMst.RuleName73;
const RuleDescription73 = currMst.RuleDescription73;
const RuleTypeId73List = AdmWizardRuleReduxObj.ScreenDdlSelectors.RuleTypeId73(AdmWizardRuleState);
const RuleTypeId73 = currMst.RuleTypeId73;
const WizardId73List = AdmWizardRuleReduxObj.ScreenDdlSelectors.WizardId73(AdmWizardRuleState);
const WizardId73 = currMst.WizardId73;
const RuleOrder73 = currMst.RuleOrder73;
const ProcedureName73 = currMst.ProcedureName73;
const BeforeCRUD73 = currMst.BeforeCRUD73;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmWizardRuleState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cWizardRuleId73: WizardRuleId73 || '',
                  cRuleName73: RuleName73 || '',
                  cRuleDescription73: RuleDescription73 || '',
                  cRuleTypeId73: RuleTypeId73List.filter(obj => { return obj.key === RuleTypeId73 })[0],
                  cWizardId73: WizardId73List.filter(obj => { return obj.key === WizardId73 })[0],
                  cRuleOrder73: RuleOrder73 || '',
                  cProcedureName73: ProcedureName73 || '',
                  cBeforeCRUD73: BeforeCRUD73 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmWizardRuleState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardRuleId73)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardRuleId73)) return null;
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
            {(authCol.WizardRuleId73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardRuleId73 || {}).ColumnHeader} {(columnLabel.WizardRuleId73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardRuleId73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardRuleId73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWizardRuleId73'
disabled = {(authCol.WizardRuleId73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWizardRuleId73 && touched.cWizardRuleId73 && <span className='form__form-group-error'>{errors.cWizardRuleId73}</span>}
</div>
</Col>
}
{(authCol.RuleName73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleName73 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleName73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleName73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleName73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleName73'
disabled = {(authCol.RuleName73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleName73 && touched.cRuleName73 && <span className='form__form-group-error'>{errors.cRuleName73}</span>}
</div>
</Col>
}
{(authCol.RuleDescription73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleDescription73 || {}).ColumnHeader} {(columnLabel.RuleDescription73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleDescription73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleDescription73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleDescription73'
disabled = {(authCol.RuleDescription73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleDescription73 && touched.cRuleDescription73 && <span className='form__form-group-error'>{errors.cRuleDescription73}</span>}
</div>
</Col>
}
{(authCol.RuleTypeId73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTypeId73 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleTypeId73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTypeId73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTypeId73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRuleTypeId73'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRuleTypeId73')}
value={values.cRuleTypeId73}
options={RuleTypeId73List}
placeholder=''
disabled = {(authCol.RuleTypeId73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTypeId73 && touched.cRuleTypeId73 && <span className='form__form-group-error'>{errors.cRuleTypeId73}</span>}
</div>
</Col>
}
{(authCol.WizardId73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardId73 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.WizardId73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardId73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardId73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cWizardId73'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cWizardId73')}
value={values.cWizardId73}
options={WizardId73List}
placeholder=''
disabled = {(authCol.WizardId73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWizardId73 && touched.cWizardId73 && <span className='form__form-group-error'>{errors.cWizardId73}</span>}
</div>
</Col>
}
{(authCol.RuleOrder73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleOrder73 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleOrder73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleOrder73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleOrder73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleOrder73'
disabled = {(authCol.RuleOrder73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleOrder73 && touched.cRuleOrder73 && <span className='form__form-group-error'>{errors.cRuleOrder73}</span>}
</div>
</Col>
}
{(authCol.ProcedureName73 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProcedureName73 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ProcedureName73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProcedureName73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProcedureName73 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmWizardRuleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cProcedureName73'
disabled = {(authCol.ProcedureName73 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProcedureName73 && touched.cProcedureName73 && <span className='form__form-group-error'>{errors.cProcedureName73}</span>}
</div>
</Col>
}
{(authCol.BeforeCRUD73 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cBeforeCRUD73'
onChange={handleChange}
defaultChecked={values.cBeforeCRUD73}
disabled={(authCol.BeforeCRUD73 || {}).readonly || !(authCol.BeforeCRUD73 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.BeforeCRUD73 || {}).ColumnHeader}</span>
</label>
{(columnLabel.BeforeCRUD73 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BeforeCRUD73 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BeforeCRUD73 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).WizardRuleId73)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).WizardRuleId73));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmWizardRuleState) && <Skeleton height='43px' />) ||
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
  AdmWizardRule: state.AdmWizardRule,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmWizardRuleReduxObj.LoadPage.bind(AdmWizardRuleReduxObj) },
    { SavePage: AdmWizardRuleReduxObj.SavePage.bind(AdmWizardRuleReduxObj) },
    { DelMst: AdmWizardRuleReduxObj.DelMst.bind(AdmWizardRuleReduxObj) },
    { AddMst: AdmWizardRuleReduxObj.AddMst.bind(AdmWizardRuleReduxObj) },
//    { SearchMemberId64: AdmWizardRuleReduxObj.SearchActions.SearchMemberId64.bind(AdmWizardRuleReduxObj) },
//    { SearchCurrencyId64: AdmWizardRuleReduxObj.SearchActions.SearchCurrencyId64.bind(AdmWizardRuleReduxObj) },
//    { SearchCustomerJobId64: AdmWizardRuleReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmWizardRuleReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            