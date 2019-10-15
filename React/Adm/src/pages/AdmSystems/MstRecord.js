
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
import AdmSystemsReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSystems';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmSystems || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SystemId45';
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

 AddDbs({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'AddDbs';
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
    const columnLabel = (this.props.AdmSystems || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cSystemId45) { errors.cSystemId45 = (columnLabel.SystemId45 || {}).ErrMessage;}
if (!values.cServerName45) { errors.cServerName45 = (columnLabel.ServerName45 || {}).ErrMessage;}
if (!values.cSystemName45) { errors.cSystemName45 = (columnLabel.SystemName45 || {}).ErrMessage;}
if (!values.cSystemAbbr45) { errors.cSystemAbbr45 = (columnLabel.SystemAbbr45 || {}).ErrMessage;}
if (!values.cdbAppProvider45) { errors.cdbAppProvider45 = (columnLabel.dbAppProvider45 || {}).ErrMessage;}
if (!values.cdbAppServer45) { errors.cdbAppServer45 = (columnLabel.dbAppServer45 || {}).ErrMessage;}
if (!values.cdbAppDatabase45) { errors.cdbAppDatabase45 = (columnLabel.dbAppDatabase45 || {}).ErrMessage;}
if (!values.cdbDesDatabase45) { errors.cdbDesDatabase45 = (columnLabel.dbDesDatabase45 || {}).ErrMessage;}
if (!values.cdbAppUserId45) { errors.cdbAppUserId45 = (columnLabel.dbAppUserId45 || {}).ErrMessage;}
if (!values.cdbAppPassword45) { errors.cdbAppPassword45 = (columnLabel.dbAppPassword45 || {}).ErrMessage;}
if (!values.cAdminEmail45) { errors.cAdminEmail45 = (columnLabel.AdminEmail45 || {}).ErrMessage;}
if (!values.cCustServEmail45) { errors.cCustServEmail45 = (columnLabel.CustServEmail45 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmSystems || {}).Mst || {};
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
        this.props.AdmSystems,
        {
          SystemId45: values.cSystemId45|| '',
          ServerName45: values.cServerName45|| '',
          SystemName45: values.cSystemName45|| '',
          SystemAbbr45: values.cSystemAbbr45|| '',
          SysProgram45: values.cSysProgram45 ? 'Y' : 'N',
          Active45: values.cActive45 ? 'Y' : 'N',
          dbAppProvider45: values.cdbAppProvider45|| '',
          dbAppServer45: values.cdbAppServer45|| '',
          dbAppDatabase45: values.cdbAppDatabase45|| '',
          dbDesDatabase45: values.cdbDesDatabase45|| '',
          dbAppUserId45: values.cdbAppUserId45|| '',
          dbAppPassword45: values.cdbAppPassword45|| '',
          dbX01Provider45: values.cdbX01Provider45|| '',
          dbX01Server45: values.cdbX01Server45|| '',
          dbX01Database45: values.cdbX01Database45|| '',
          dbX01UserId45: values.cdbX01UserId45|| '',
          dbX01Password45: values.cdbX01Password45|| '',
          dbX01Extra45: values.cdbX01Extra45|| '',
          AdminEmail45: values.cAdminEmail45|| '',
          AdminPhone45: values.cAdminPhone45|| '',
          CustServEmail45: values.cCustServEmail45|| '',
          CustServPhone45: values.cCustServPhone45|| '',
          CustServFax45: values.cCustServFax45|| '',
          WebAddress45: values.cWebAddress45|| '',
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
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SystemId45;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmSystems.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmSystems.Label);
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
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SystemId45;
        this.props.DelMst(this.props.AdmSystems, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmSystems || {};
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
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
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
      if (!(this.props.AdmSystems || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmSystems || {};

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
    const AdmSystemsState = this.props.AdmSystems || {};

    if (AdmSystemsState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmSystemsState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmSystemsReduxObj.GetScreenButtons(AdmSystemsState) || {};
    const itemList = AdmSystemsState.Dtl || [];
    const auxLabels = AdmSystemsState.Label || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};

    const columnLabel = AdmSystemsState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmSystemsState);
    const authRow = (AdmSystemsState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmSystems || {}).Mst || {});
    const currDtl = ((this.props.AdmSystems || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.SystemId45));
    const selectList = AdmSystemsReduxObj.SearchListToSelectList(AdmSystemsState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const SystemId45 = currMst.SystemId45;
const ServerName45 = currMst.ServerName45;
const SystemName45 = currMst.SystemName45;
const SystemAbbr45 = currMst.SystemAbbr45;
const SysProgram45 = currMst.SysProgram45;
const Active45 = currMst.Active45;
const dbAppProvider45 = currMst.dbAppProvider45;
const dbAppServer45 = currMst.dbAppServer45;
const dbAppDatabase45 = currMst.dbAppDatabase45;
const dbDesDatabase45 = currMst.dbDesDatabase45;
const dbAppUserId45 = currMst.dbAppUserId45;
const dbAppPassword45 = currMst.dbAppPassword45;
const dbX01Provider45 = currMst.dbX01Provider45;
const dbX01Server45 = currMst.dbX01Server45;
const dbX01Database45 = currMst.dbX01Database45;
const dbX01UserId45 = currMst.dbX01UserId45;
const dbX01Password45 = currMst.dbX01Password45;
const dbX01Extra45 = currMst.dbX01Extra45;
const AdminEmail45 = currMst.AdminEmail45;
const AdminPhone45 = currMst.AdminPhone45;
const CustServEmail45 = currMst.CustServEmail45;
const CustServPhone45 = currMst.CustServPhone45;
const CustServFax45 = currMst.CustServFax45;
const WebAddress45 = currMst.WebAddress45;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmSystemsState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cSystemId45: SystemId45 || '',
                  cServerName45: ServerName45 || '',
                  cSystemName45: SystemName45 || '',
                  cSystemAbbr45: SystemAbbr45 || '',
                  cSysProgram45: SysProgram45 === 'Y',
                  cActive45: Active45 === 'Y',
                  cdbAppProvider45: dbAppProvider45 || '',
                  cdbAppServer45: dbAppServer45 || '',
                  cdbAppDatabase45: dbAppDatabase45 || '',
                  cdbDesDatabase45: dbDesDatabase45 || '',
                  cdbAppUserId45: dbAppUserId45 || '',
                  cdbAppPassword45: dbAppPassword45 || '',
                  cdbX01Provider45: dbX01Provider45 || '',
                  cdbX01Server45: dbX01Server45 || '',
                  cdbX01Database45: dbX01Database45 || '',
                  cdbX01UserId45: dbX01UserId45 || '',
                  cdbX01Password45: dbX01Password45 || '',
                  cdbX01Extra45: dbX01Extra45 || '',
                  cAdminEmail45: AdminEmail45 || '',
                  cAdminPhone45: AdminPhone45 || '',
                  cCustServEmail45: CustServEmail45 || '',
                  cCustServPhone45: CustServPhone45 || '',
                  cCustServFax45: CustServFax45 || '',
                  cWebAddress45: WebAddress45 || '',
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
                                {(this.constructor.ShowSpinner(AdmSystemsState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)) return null;
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
            {(authCol.SystemId45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemId45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemId45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemId45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemId45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSystemId45'
disabled = {(authCol.SystemId45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemId45 && touched.cSystemId45 && <span className='form__form-group-error'>{errors.cSystemId45}</span>}
</div>
</Col>
}
{(authCol.ServerName45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ServerName45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ServerName45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ServerName45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ServerName45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cServerName45'
disabled = {(authCol.ServerName45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cServerName45 && touched.cServerName45 && <span className='form__form-group-error'>{errors.cServerName45}</span>}
</div>
</Col>
}
{(authCol.SystemName45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemName45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemName45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemName45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemName45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSystemName45'
disabled = {(authCol.SystemName45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemName45 && touched.cSystemName45 && <span className='form__form-group-error'>{errors.cSystemName45}</span>}
</div>
</Col>
}
{(authCol.SystemAbbr45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemAbbr45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemAbbr45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemAbbr45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemAbbr45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSystemAbbr45'
disabled = {(authCol.SystemAbbr45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemAbbr45 && touched.cSystemAbbr45 && <span className='form__form-group-error'>{errors.cSystemAbbr45}</span>}
</div>
</Col>
}
{(authCol.SysProgram45 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cSysProgram45'
onChange={handleChange}
defaultChecked={values.cSysProgram45}
disabled={(authCol.SysProgram45 || {}).readonly || !(authCol.SysProgram45 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.SysProgram45 || {}).ColumnHeader}</span>
</label>
{(columnLabel.SysProgram45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SysProgram45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SysProgram45 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.Active45 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cActive45'
onChange={handleChange}
defaultChecked={values.cActive45}
disabled={(authCol.Active45 || {}).readonly || !(authCol.Active45 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.Active45 || {}).ColumnHeader}</span>
</label>
{(columnLabel.Active45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Active45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Active45 || {}).ToolTip} />
)}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.AddDbs || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.AddDbs || {}).readonly || !(authCol.AddDbs || {}).visible} onClick={this.AddDbs({ naviBar, submitForm, currMst })} >{auxLabels.AddDbs || (columnLabel.AddDbs || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.dbAppProvider45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbAppProvider45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbAppProvider45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbAppProvider45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbAppProvider45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbAppProvider45'
disabled = {(authCol.dbAppProvider45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbAppProvider45 && touched.cdbAppProvider45 && <span className='form__form-group-error'>{errors.cdbAppProvider45}</span>}
</div>
</Col>
}
{(authCol.dbAppServer45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbAppServer45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbAppServer45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbAppServer45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbAppServer45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbAppServer45'
disabled = {(authCol.dbAppServer45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbAppServer45 && touched.cdbAppServer45 && <span className='form__form-group-error'>{errors.cdbAppServer45}</span>}
</div>
</Col>
}
{(authCol.dbAppDatabase45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbAppDatabase45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbAppDatabase45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbAppDatabase45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbAppDatabase45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbAppDatabase45'
disabled = {(authCol.dbAppDatabase45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbAppDatabase45 && touched.cdbAppDatabase45 && <span className='form__form-group-error'>{errors.cdbAppDatabase45}</span>}
</div>
</Col>
}
{(authCol.dbDesDatabase45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbDesDatabase45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbDesDatabase45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbDesDatabase45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbDesDatabase45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbDesDatabase45'
disabled = {(authCol.dbDesDatabase45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbDesDatabase45 && touched.cdbDesDatabase45 && <span className='form__form-group-error'>{errors.cdbDesDatabase45}</span>}
</div>
</Col>
}
{(authCol.dbAppUserId45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbAppUserId45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbAppUserId45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbAppUserId45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbAppUserId45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbAppUserId45'
disabled = {(authCol.dbAppUserId45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbAppUserId45 && touched.cdbAppUserId45 && <span className='form__form-group-error'>{errors.cdbAppUserId45}</span>}
</div>
</Col>
}
{(authCol.dbAppPassword45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbAppPassword45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.dbAppPassword45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbAppPassword45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbAppPassword45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbAppPassword45'
disabled = {(authCol.dbAppPassword45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbAppPassword45 && touched.cdbAppPassword45 && <span className='form__form-group-error'>{errors.cdbAppPassword45}</span>}
</div>
</Col>
}
{(authCol.dbX01Provider45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01Provider45 || {}).ColumnHeader} {(columnLabel.dbX01Provider45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01Provider45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01Provider45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01Provider45'
disabled = {(authCol.dbX01Provider45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01Provider45 && touched.cdbX01Provider45 && <span className='form__form-group-error'>{errors.cdbX01Provider45}</span>}
</div>
</Col>
}
{(authCol.dbX01Server45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01Server45 || {}).ColumnHeader} {(columnLabel.dbX01Server45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01Server45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01Server45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01Server45'
disabled = {(authCol.dbX01Server45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01Server45 && touched.cdbX01Server45 && <span className='form__form-group-error'>{errors.cdbX01Server45}</span>}
</div>
</Col>
}
{(authCol.dbX01Database45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01Database45 || {}).ColumnHeader} {(columnLabel.dbX01Database45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01Database45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01Database45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01Database45'
disabled = {(authCol.dbX01Database45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01Database45 && touched.cdbX01Database45 && <span className='form__form-group-error'>{errors.cdbX01Database45}</span>}
</div>
</Col>
}
{(authCol.dbX01UserId45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01UserId45 || {}).ColumnHeader} {(columnLabel.dbX01UserId45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01UserId45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01UserId45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01UserId45'
disabled = {(authCol.dbX01UserId45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01UserId45 && touched.cdbX01UserId45 && <span className='form__form-group-error'>{errors.cdbX01UserId45}</span>}
</div>
</Col>
}
{(authCol.dbX01Password45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01Password45 || {}).ColumnHeader} {(columnLabel.dbX01Password45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01Password45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01Password45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01Password45'
disabled = {(authCol.dbX01Password45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01Password45 && touched.cdbX01Password45 && <span className='form__form-group-error'>{errors.cdbX01Password45}</span>}
</div>
</Col>
}
{(authCol.dbX01Extra45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.dbX01Extra45 || {}).ColumnHeader} {(columnLabel.dbX01Extra45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.dbX01Extra45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.dbX01Extra45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cdbX01Extra45'
disabled = {(authCol.dbX01Extra45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cdbX01Extra45 && touched.cdbX01Extra45 && <span className='form__form-group-error'>{errors.cdbX01Extra45}</span>}
</div>
</Col>
}
{(authCol.AdminEmail45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AdminEmail45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AdminEmail45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AdminEmail45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AdminEmail45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAdminEmail45'
disabled = {(authCol.AdminEmail45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAdminEmail45 && touched.cAdminEmail45 && <span className='form__form-group-error'>{errors.cAdminEmail45}</span>}
</div>
</Col>
}
{(authCol.AdminPhone45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AdminPhone45 || {}).ColumnHeader} {(columnLabel.AdminPhone45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AdminPhone45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AdminPhone45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAdminPhone45'
disabled = {(authCol.AdminPhone45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAdminPhone45 && touched.cAdminPhone45 && <span className='form__form-group-error'>{errors.cAdminPhone45}</span>}
</div>
</Col>
}
{(authCol.CustServEmail45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CustServEmail45 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CustServEmail45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CustServEmail45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CustServEmail45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCustServEmail45'
disabled = {(authCol.CustServEmail45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCustServEmail45 && touched.cCustServEmail45 && <span className='form__form-group-error'>{errors.cCustServEmail45}</span>}
</div>
</Col>
}
{(authCol.CustServPhone45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CustServPhone45 || {}).ColumnHeader} {(columnLabel.CustServPhone45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CustServPhone45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CustServPhone45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCustServPhone45'
disabled = {(authCol.CustServPhone45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCustServPhone45 && touched.cCustServPhone45 && <span className='form__form-group-error'>{errors.cCustServPhone45}</span>}
</div>
</Col>
}
{(authCol.CustServFax45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CustServFax45 || {}).ColumnHeader} {(columnLabel.CustServFax45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CustServFax45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CustServFax45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCustServFax45'
disabled = {(authCol.CustServFax45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCustServFax45 && touched.cCustServFax45 && <span className='form__form-group-error'>{errors.cCustServFax45}</span>}
</div>
</Col>
}
{(authCol.WebAddress45 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WebAddress45 || {}).ColumnHeader} {(columnLabel.WebAddress45 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WebAddress45 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WebAddress45 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWebAddress45'
disabled = {(authCol.WebAddress45 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWebAddress45 && touched.cWebAddress45 && <span className='form__form-group-error'>{errors.cWebAddress45}</span>}
</div>
</Col>
}
{(authCol.ResetFromGitRepo || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ResetFromGitRepo || {}).ColumnHeader} {(columnLabel.ResetFromGitRepo || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ResetFromGitRepo || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ResetFromGitRepo || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.CreateReactBase || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CreateReactBase || {}).ColumnHeader} {(columnLabel.CreateReactBase || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CreateReactBase || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CreateReactBase || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.RemoveReactBase || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RemoveReactBase || {}).ColumnHeader} {(columnLabel.RemoveReactBase || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RemoveReactBase || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RemoveReactBase || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.PublishReactToSite || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSystemsState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PublishReactToSite || {}).ColumnHeader} {(columnLabel.PublishReactToSite || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PublishReactToSite || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PublishReactToSite || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SystemId45));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmSystemsState) && <Skeleton height='43px' />) ||
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
  AdmSystems: state.AdmSystems,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSystemsReduxObj.LoadPage.bind(AdmSystemsReduxObj) },
    { SavePage: AdmSystemsReduxObj.SavePage.bind(AdmSystemsReduxObj) },
    { DelMst: AdmSystemsReduxObj.DelMst.bind(AdmSystemsReduxObj) },
    { AddMst: AdmSystemsReduxObj.AddMst.bind(AdmSystemsReduxObj) },
//    { SearchMemberId64: AdmSystemsReduxObj.SearchActions.SearchMemberId64.bind(AdmSystemsReduxObj) },
//    { SearchCurrencyId64: AdmSystemsReduxObj.SearchActions.SearchCurrencyId64.bind(AdmSystemsReduxObj) },
//    { SearchCustomerJobId64: AdmSystemsReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmSystemsReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            