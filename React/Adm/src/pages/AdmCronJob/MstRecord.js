
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
import AdmCronJobReduxObj, { ShowMstFilterApplied } from '../../redux/AdmCronJob';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmCronJob || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'CronJobId264';
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
    const columnLabel = (this.props.AdmCronJob || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cCronJobName264) { errors.cCronJobName264 = (columnLabel.CronJobName264 || {}).ErrMessage;}
if (!values.cJobLink264) { errors.cJobLink264 = (columnLabel.JobLink264 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmCronJob || {}).Mst || {};
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
        this.props.AdmCronJob,
        {
          CronJobId264: values.cCronJobId264|| '',
          CronJobName264: values.cCronJobName264|| '',
          LastRun264: values.cLastRun264|| '',
          NextRun264: values.cNextRun264|| '',
          JobLink264: values.cJobLink264|| '',
          LastStatus264: values.cLastStatus264|| '',
          Year264: values.cYear264|| '',
          Month264: values.cMonth264|| '',
          Day264: values.cDay264|| '',
          Hour264: values.cHour264|| '',
          Minute264: values.cMinute264|| '',
          DayOfWeek264: values.cDayOfWeek264|| '',
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
    const AdmCronJobState = this.props.AdmCronJob || {};
    const auxSystemLabels = AdmCronJobState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).CronJobId264;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmCronJob.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmCronJob.Label);
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
    const AdmCronJobState = this.props.AdmCronJob || {};
    const auxSystemLabels = AdmCronJobState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.CronJobId264;
        this.props.DelMst(this.props.AdmCronJob, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmCronJob || {};
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
    const AdmCronJobState = this.props.AdmCronJob || {};
    const auxSystemLabels = AdmCronJobState.SystemLabel || {};
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
      if (!(this.props.AdmCronJob || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmCronJob || {};

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
    const AdmCronJobState = this.props.AdmCronJob || {};

    if (AdmCronJobState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmCronJobState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmCronJobReduxObj.GetScreenButtons(AdmCronJobState) || {};
    const itemList = AdmCronJobState.Dtl || [];
    const auxLabels = AdmCronJobState.Label || {};
    const auxSystemLabels = AdmCronJobState.SystemLabel || {};

    const columnLabel = AdmCronJobState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmCronJobState);
    const authRow = (AdmCronJobState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmCronJob || {}).Mst || {});
    const currDtl = ((this.props.AdmCronJob || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.CronJobId264));
    const selectList = AdmCronJobReduxObj.SearchListToSelectList(AdmCronJobState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const CronJobId264 = currMst.CronJobId264;
const CronJobName264 = currMst.CronJobName264;
const LastRun264 = currMst.LastRun264;
const NextRun264 = currMst.NextRun264;
const JobLink264 = currMst.JobLink264;
const LastStatus264 = currMst.LastStatus264;
const Year264 = currMst.Year264;
const Month264 = currMst.Month264;
const Day264 = currMst.Day264;
const Hour264 = currMst.Hour264;
const Minute264 = currMst.Minute264;
const DayOfWeek264 = currMst.DayOfWeek264;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmCronJobState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cCronJobId264: CronJobId264 || '',
                  cCronJobName264: CronJobName264 || '',
                  cLastRun264: LastRun264 || new Date(),
                  cNextRun264: NextRun264 || new Date(),
                  cJobLink264: JobLink264 || '',
                  cLastStatus264: LastStatus264 || '',
                  cYear264: Year264 || '',
                  cMonth264: Month264 || '',
                  cDay264: Day264 || '',
                  cHour264: Hour264 || '',
                  cMinute264: Minute264 || '',
                  cDayOfWeek264: DayOfWeek264 || '',
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
                                {(this.constructor.ShowSpinner(AdmCronJobState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CronJobId264)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CronJobId264)) return null;
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
            {(authCol.CronJobId264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CronJobId264 || {}).ColumnHeader} {(columnLabel.CronJobId264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CronJobId264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CronJobId264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCronJobId264'
disabled = {(authCol.CronJobId264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCronJobId264 && touched.cCronJobId264 && <span className='form__form-group-error'>{errors.cCronJobId264}</span>}
</div>
</Col>
}
{(authCol.CronJobName264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CronJobName264 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CronJobName264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CronJobName264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CronJobName264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCronJobName264'
disabled = {(authCol.CronJobName264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCronJobName264 && touched.cCronJobName264 && <span className='form__form-group-error'>{errors.cCronJobName264}</span>}
</div>
</Col>
}
{(authCol.LastRun264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastRun264 || {}).ColumnHeader} {(columnLabel.LastRun264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastRun264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastRun264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cLastRun264'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cLastRun264', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cLastRun264', true)}
value={values.cLastRun264}
selected={values.cLastRun264}
disabled = {(authCol.LastRun264 || {}).readonly ? true: false }/>
</div>
}
{errors.cLastRun264 && touched.cLastRun264 && <span className='form__form-group-error'>{errors.cLastRun264}</span>}
</div>
</Col>
}
{(authCol.NextRun264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.NextRun264 || {}).ColumnHeader} {(columnLabel.NextRun264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.NextRun264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.NextRun264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cNextRun264'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cNextRun264', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cNextRun264', true)}
value={values.cNextRun264}
selected={values.cNextRun264}
disabled = {(authCol.NextRun264 || {}).readonly ? true: false }/>
</div>
}
{errors.cNextRun264 && touched.cNextRun264 && <span className='form__form-group-error'>{errors.cNextRun264}</span>}
</div>
</Col>
}
{(authCol.JobLink264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.JobLink264 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.JobLink264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.JobLink264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.JobLink264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cJobLink264'
disabled = {(authCol.JobLink264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cJobLink264 && touched.cJobLink264 && <span className='form__form-group-error'>{errors.cJobLink264}</span>}
</div>
</Col>
}
{(authCol.LastStatus264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastStatus264 || {}).ColumnHeader} {(columnLabel.LastStatus264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastStatus264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastStatus264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLastStatus264'
disabled = {(authCol.LastStatus264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLastStatus264 && touched.cLastStatus264 && <span className='form__form-group-error'>{errors.cLastStatus264}</span>}
</div>
</Col>
}
{(authCol.Year264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Year264 || {}).ColumnHeader} {(columnLabel.Year264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Year264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Year264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cYear264'
disabled = {(authCol.Year264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cYear264 && touched.cYear264 && <span className='form__form-group-error'>{errors.cYear264}</span>}
</div>
</Col>
}
{(authCol.Month264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Month264 || {}).ColumnHeader} {(columnLabel.Month264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Month264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Month264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMonth264'
disabled = {(authCol.Month264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMonth264 && touched.cMonth264 && <span className='form__form-group-error'>{errors.cMonth264}</span>}
</div>
</Col>
}
{(authCol.Day264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Day264 || {}).ColumnHeader} {(columnLabel.Day264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Day264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Day264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDay264'
disabled = {(authCol.Day264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDay264 && touched.cDay264 && <span className='form__form-group-error'>{errors.cDay264}</span>}
</div>
</Col>
}
{(authCol.Hour264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Hour264 || {}).ColumnHeader} {(columnLabel.Hour264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Hour264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Hour264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cHour264'
disabled = {(authCol.Hour264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cHour264 && touched.cHour264 && <span className='form__form-group-error'>{errors.cHour264}</span>}
</div>
</Col>
}
{(authCol.Minute264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Minute264 || {}).ColumnHeader} {(columnLabel.Minute264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Minute264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Minute264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMinute264'
disabled = {(authCol.Minute264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMinute264 && touched.cMinute264 && <span className='form__form-group-error'>{errors.cMinute264}</span>}
</div>
</Col>
}
{(authCol.DayOfWeek264 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DayOfWeek264 || {}).ColumnHeader} {(columnLabel.DayOfWeek264 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DayOfWeek264 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DayOfWeek264 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDayOfWeek264'
disabled = {(authCol.DayOfWeek264 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDayOfWeek264 && touched.cDayOfWeek264 && <span className='form__form-group-error'>{errors.cDayOfWeek264}</span>}
</div>
</Col>
}
{(authCol.CronJobMsg || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCronJobState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CronJobMsg || {}).ColumnHeader} {(columnLabel.CronJobMsg || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CronJobMsg || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CronJobMsg || {}).ToolTip} />
)}
</label>
}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CronJobId264)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).CronJobId264));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmCronJobState) && <Skeleton height='43px' />) ||
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
  AdmCronJob: state.AdmCronJob,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmCronJobReduxObj.LoadPage.bind(AdmCronJobReduxObj) },
    { SavePage: AdmCronJobReduxObj.SavePage.bind(AdmCronJobReduxObj) },
    { DelMst: AdmCronJobReduxObj.DelMst.bind(AdmCronJobReduxObj) },
    { AddMst: AdmCronJobReduxObj.AddMst.bind(AdmCronJobReduxObj) },
//    { SearchMemberId64: AdmCronJobReduxObj.SearchActions.SearchMemberId64.bind(AdmCronJobReduxObj) },
//    { SearchCurrencyId64: AdmCronJobReduxObj.SearchActions.SearchCurrencyId64.bind(AdmCronJobReduxObj) },
//    { SearchCustomerJobId64: AdmCronJobReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmCronJobReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            