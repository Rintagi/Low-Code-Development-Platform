
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
import AdmUsrImprReduxObj, { ShowMstFilterApplied } from '../../redux/AdmUsrImpr';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmUsrImpr || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'UsrImprId95';
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

UsrId95InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchUsrId95(v, filterBy);}}
 UPicMed({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'UPicMed';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
ImprUsrId95InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchImprUsrId95(v, filterBy);}}
 IPicMed({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'IPicMed';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
TestCulture95InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchTestCulture95(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmUsrImpr || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cUsrId95 || {}).value)) { errors.cUsrId95 = (columnLabel.UsrId95 || {}).ErrMessage;}
if (isEmptyId((values.cImprUsrId95 || {}).value)) { errors.cImprUsrId95 = (columnLabel.ImprUsrId95 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmUsrImpr || {}).Mst || {};
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
        this.props.AdmUsrImpr,
        {
          UsrImprId95: values.cUsrImprId95|| '',
          UsrId95: (values.cUsrId95|| {}).value || '',
          ImprUsrId95: (values.cImprUsrId95|| {}).value || '',
          FailedAttempt1: values.cFailedAttempt1|| '',
          InputBy95: (values.cInputBy95|| {}).value || '',
          InputOn95: values.cInputOn95|| '',
          ModifiedBy95: (values.cModifiedBy95|| {}).value || '',
          ModifiedOn95: values.cModifiedOn95|| '',
          TestCulture95: (values.cTestCulture95|| {}).value || '',
          TestCurrency95: values.cTestCurrency95|| '',
          SignOff95: values.cSignOff95|| '',
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
    const AdmUsrImprState = this.props.AdmUsrImpr || {};
    const auxSystemLabels = AdmUsrImprState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).UsrImprId95;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmUsrImpr.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmUsrImpr.Label);
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
    const AdmUsrImprState = this.props.AdmUsrImpr || {};
    const auxSystemLabels = AdmUsrImprState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.UsrImprId95;
        this.props.DelMst(this.props.AdmUsrImpr, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmUsrImpr || {};
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
    const AdmUsrImprState = this.props.AdmUsrImpr || {};
    const auxSystemLabels = AdmUsrImprState.SystemLabel || {};
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
      if (!(this.props.AdmUsrImpr || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmUsrImpr || {};

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
    const AdmUsrImprState = this.props.AdmUsrImpr || {};

    if (AdmUsrImprState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmUsrImprState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmUsrImprReduxObj.GetScreenButtons(AdmUsrImprState) || {};
    const itemList = AdmUsrImprState.Dtl || [];
    const auxLabels = AdmUsrImprState.Label || {};
    const auxSystemLabels = AdmUsrImprState.SystemLabel || {};

    const columnLabel = AdmUsrImprState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmUsrImprState);
    const authRow = (AdmUsrImprState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmUsrImpr || {}).Mst || {});
    const currDtl = ((this.props.AdmUsrImpr || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.UsrImprId95));
    const selectList = AdmUsrImprReduxObj.SearchListToSelectList(AdmUsrImprState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const UsrImprId95 = currMst.UsrImprId95;
const UsrId95List = AdmUsrImprReduxObj.ScreenDdlSelectors.UsrId95(AdmUsrImprState);
const UsrId95 = currMst.UsrId95;
const ImprUsrId95List = AdmUsrImprReduxObj.ScreenDdlSelectors.ImprUsrId95(AdmUsrImprState);
const ImprUsrId95 = currMst.ImprUsrId95;
const FailedAttempt1 = currMst.FailedAttempt1;
const InputBy95List = AdmUsrImprReduxObj.ScreenDdlSelectors.InputBy95(AdmUsrImprState);
const InputBy95 = currMst.InputBy95;
const InputOn95 = currMst.InputOn95;
const ModifiedBy95List = AdmUsrImprReduxObj.ScreenDdlSelectors.ModifiedBy95(AdmUsrImprState);
const ModifiedBy95 = currMst.ModifiedBy95;
const ModifiedOn95 = currMst.ModifiedOn95;
const TestCulture95List = AdmUsrImprReduxObj.ScreenDdlSelectors.TestCulture95(AdmUsrImprState);
const TestCulture95 = currMst.TestCulture95;
const TestCurrency95 = currMst.TestCurrency95;
const SignOff95 = currMst.SignOff95;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmUsrImprState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cUsrImprId95: UsrImprId95 || '',
                  cUsrId95: UsrId95List.filter(obj => { return obj.key === UsrId95 })[0],
                  cImprUsrId95: ImprUsrId95List.filter(obj => { return obj.key === ImprUsrId95 })[0],
                  cFailedAttempt1: FailedAttempt1 || '',
                  cInputBy95: InputBy95List.filter(obj => { return obj.key === InputBy95 })[0],
                  cInputOn95: InputOn95 || new Date(),
                  cModifiedBy95: ModifiedBy95List.filter(obj => { return obj.key === ModifiedBy95 })[0],
                  cModifiedOn95: ModifiedOn95 || new Date(),
                  cTestCulture95: TestCulture95List.filter(obj => { return obj.key === TestCulture95 })[0],
                  cTestCurrency95: TestCurrency95 || '',
                  cSignOff95: SignOff95 || '',
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
                                {(this.constructor.ShowSpinner(AdmUsrImprState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrImprId95)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrImprId95)) return null;
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
            {(authCol.UsrImprId95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrImprId95 || {}).ColumnHeader} {(columnLabel.UsrImprId95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrImprId95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrImprId95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrImprId95'
disabled = {(authCol.UsrImprId95 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrImprId95 && touched.cUsrImprId95 && <span className='form__form-group-error'>{errors.cUsrImprId95}</span>}
</div>
</Col>
}
{(authCol.UsrId95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrId95 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.UsrId95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrId95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrId95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cUsrId95'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cUsrId95', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cUsrId95', true)}
onInputChange={this.UsrId95InputChange()}
value={values.cUsrId95}
defaultSelected={UsrId95List.filter(obj => { return obj.key === UsrId95 })}
options={UsrId95List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.UsrId95 || {}).readonly ? true: false }/>
</div>
}
{errors.cUsrId95 && touched.cUsrId95 && <span className='form__form-group-error'>{errors.cUsrId95}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.UPicMed || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.UPicMed || {}).readonly || !(authCol.UPicMed || {}).visible} onClick={this.UPicMed({ naviBar, submitForm, currMst })} >{auxLabels.UPicMed || (columnLabel.UPicMed || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.ImprUsrId95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ImprUsrId95 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ImprUsrId95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ImprUsrId95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ImprUsrId95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cImprUsrId95'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cImprUsrId95', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cImprUsrId95', true)}
onInputChange={this.ImprUsrId95InputChange()}
value={values.cImprUsrId95}
defaultSelected={ImprUsrId95List.filter(obj => { return obj.key === ImprUsrId95 })}
options={ImprUsrId95List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ImprUsrId95 || {}).readonly ? true: false }/>
</div>
}
{errors.cImprUsrId95 && touched.cImprUsrId95 && <span className='form__form-group-error'>{errors.cImprUsrId95}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.IPicMed || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.IPicMed || {}).readonly || !(authCol.IPicMed || {}).visible} onClick={this.IPicMed({ naviBar, submitForm, currMst })} >{auxLabels.IPicMed || (columnLabel.IPicMed || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.FailedAttempt1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FailedAttempt1 || {}).ColumnHeader} {(columnLabel.FailedAttempt1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FailedAttempt1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FailedAttempt1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFailedAttempt1'
disabled = {(authCol.FailedAttempt1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFailedAttempt1 && touched.cFailedAttempt1 && <span className='form__form-group-error'>{errors.cFailedAttempt1}</span>}
</div>
</Col>
}
{(authCol.InputBy95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InputBy95 || {}).ColumnHeader} {(columnLabel.InputBy95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InputBy95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InputBy95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cInputBy95'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cInputBy95')}
value={values.cInputBy95}
options={InputBy95List}
placeholder=''
disabled = {(authCol.InputBy95 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cInputBy95 && touched.cInputBy95 && <span className='form__form-group-error'>{errors.cInputBy95}</span>}
</div>
</Col>
}
{(authCol.InputOn95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InputOn95 || {}).ColumnHeader} {(columnLabel.InputOn95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InputOn95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InputOn95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cInputOn95'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cInputOn95', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cInputOn95', true)}
value={values.cInputOn95}
selected={values.cInputOn95}
disabled = {(authCol.InputOn95 || {}).readonly ? true: false }/>
</div>
}
{errors.cInputOn95 && touched.cInputOn95 && <span className='form__form-group-error'>{errors.cInputOn95}</span>}
</div>
</Col>
}
{(authCol.ModifiedBy95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedBy95 || {}).ColumnHeader} {(columnLabel.ModifiedBy95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedBy95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedBy95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cModifiedBy95'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cModifiedBy95')}
value={values.cModifiedBy95}
options={ModifiedBy95List}
placeholder=''
disabled = {(authCol.ModifiedBy95 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cModifiedBy95 && touched.cModifiedBy95 && <span className='form__form-group-error'>{errors.cModifiedBy95}</span>}
</div>
</Col>
}
{(authCol.ModifiedOn95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedOn95 || {}).ColumnHeader} {(columnLabel.ModifiedOn95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedOn95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedOn95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cModifiedOn95'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn95', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn95', true)}
value={values.cModifiedOn95}
selected={values.cModifiedOn95}
disabled = {(authCol.ModifiedOn95 || {}).readonly ? true: false }/>
</div>
}
{errors.cModifiedOn95 && touched.cModifiedOn95 && <span className='form__form-group-error'>{errors.cModifiedOn95}</span>}
</div>
</Col>
}
{(authCol.TestCulture95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TestCulture95 || {}).ColumnHeader} {(columnLabel.TestCulture95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TestCulture95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TestCulture95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cTestCulture95'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cTestCulture95', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cTestCulture95', true)}
onInputChange={this.TestCulture95InputChange()}
value={values.cTestCulture95}
defaultSelected={TestCulture95List.filter(obj => { return obj.key === TestCulture95 })}
options={TestCulture95List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.TestCulture95 || {}).readonly ? true: false }/>
</div>
}
{errors.cTestCulture95 && touched.cTestCulture95 && <span className='form__form-group-error'>{errors.cTestCulture95}</span>}
</div>
</Col>
}
{(authCol.TestCurrency95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TestCurrency95 || {}).ColumnHeader} {(columnLabel.TestCurrency95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TestCurrency95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TestCurrency95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTestCurrency95'
disabled = {(authCol.TestCurrency95 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTestCurrency95 && touched.cTestCurrency95 && <span className='form__form-group-error'>{errors.cTestCurrency95}</span>}
</div>
</Col>
}
{(authCol.SignOff95 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SignOff95 || {}).ColumnHeader} {(columnLabel.SignOff95 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SignOff95 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SignOff95 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrImprState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSignOff95'
disabled = {(authCol.SignOff95 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSignOff95 && touched.cSignOff95 && <span className='form__form-group-error'>{errors.cSignOff95}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrImprId95)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).UsrImprId95));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmUsrImprState) && <Skeleton height='43px' />) ||
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
  AdmUsrImpr: state.AdmUsrImpr,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmUsrImprReduxObj.LoadPage.bind(AdmUsrImprReduxObj) },
    { SavePage: AdmUsrImprReduxObj.SavePage.bind(AdmUsrImprReduxObj) },
    { DelMst: AdmUsrImprReduxObj.DelMst.bind(AdmUsrImprReduxObj) },
    { AddMst: AdmUsrImprReduxObj.AddMst.bind(AdmUsrImprReduxObj) },
//    { SearchMemberId64: AdmUsrImprReduxObj.SearchActions.SearchMemberId64.bind(AdmUsrImprReduxObj) },
//    { SearchCurrencyId64: AdmUsrImprReduxObj.SearchActions.SearchCurrencyId64.bind(AdmUsrImprReduxObj) },
//    { SearchCustomerJobId64: AdmUsrImprReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmUsrImprReduxObj) },
{ SearchUsrId95: AdmUsrImprReduxObj.SearchActions.SearchUsrId95.bind(AdmUsrImprReduxObj) },
{ SearchImprUsrId95: AdmUsrImprReduxObj.SearchActions.SearchImprUsrId95.bind(AdmUsrImprReduxObj) },
{ SearchTestCulture95: AdmUsrImprReduxObj.SearchActions.SearchTestCulture95.bind(AdmUsrImprReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            