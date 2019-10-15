
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
import AdmDatTierReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDatTier';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDatTier || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'DataTierId195';
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
    const columnLabel = (this.props.AdmDatTier || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cDataTierName195) { errors.cDataTierName195 = (columnLabel.DataTierName195 || {}).ErrMessage;}
if (isEmptyId((values.cEntityId195 || {}).value)) { errors.cEntityId195 = (columnLabel.EntityId195 || {}).ErrMessage;}
if (isEmptyId((values.cDbProviderCd195 || {}).value)) { errors.cDbProviderCd195 = (columnLabel.DbProviderCd195 || {}).ErrMessage;}
if (!values.cServerName195) { errors.cServerName195 = (columnLabel.ServerName195 || {}).ErrMessage;}
if (!values.cDesServer195) { errors.cDesServer195 = (columnLabel.DesServer195 || {}).ErrMessage;}
if (!values.cDesDatabase195) { errors.cDesDatabase195 = (columnLabel.DesDatabase195 || {}).ErrMessage;}
if (!values.cDesUserId195) { errors.cDesUserId195 = (columnLabel.DesUserId195 || {}).ErrMessage;}
if (!values.cDesPassword195) { errors.cDesPassword195 = (columnLabel.DesPassword195 || {}).ErrMessage;}
if (!values.cPortBinPath195) { errors.cPortBinPath195 = (columnLabel.PortBinPath195 || {}).ErrMessage;}
if (!values.cInstBinPath195) { errors.cInstBinPath195 = (columnLabel.InstBinPath195 || {}).ErrMessage;}
if (!values.cScriptPath195) { errors.cScriptPath195 = (columnLabel.ScriptPath195 || {}).ErrMessage;}
if (!values.cDbDataPath195) { errors.cDbDataPath195 = (columnLabel.DbDataPath195 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmDatTier || {}).Mst || {};
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
        this.props.AdmDatTier,
        {
          DataTierId195: values.cDataTierId195|| '',
          DataTierName195: values.cDataTierName195|| '',
          EntityId195: (values.cEntityId195|| {}).value || '',
          DbProviderCd195: (values.cDbProviderCd195|| {}).value || '',
          ServerName195: values.cServerName195|| '',
          DesServer195: values.cDesServer195|| '',
          DesDatabase195: values.cDesDatabase195|| '',
          DesUserId195: values.cDesUserId195|| '',
          DesPassword195: values.cDesPassword195|| '',
          PortBinPath195: values.cPortBinPath195|| '',
          InstBinPath195: values.cInstBinPath195|| '',
          ScriptPath195: values.cScriptPath195|| '',
          DbDataPath195: values.cDbDataPath195|| '',
          IsDevelopment195: values.cIsDevelopment195 ? 'Y' : 'N',
          IsDefault195: values.cIsDefault195 ? 'Y' : 'N',
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
    const AdmDatTierState = this.props.AdmDatTier || {};
    const auxSystemLabels = AdmDatTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).DataTierId195;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmDatTier.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmDatTier.Label);
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
    const AdmDatTierState = this.props.AdmDatTier || {};
    const auxSystemLabels = AdmDatTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.DataTierId195;
        this.props.DelMst(this.props.AdmDatTier, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmDatTier || {};
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
    const AdmDatTierState = this.props.AdmDatTier || {};
    const auxSystemLabels = AdmDatTierState.SystemLabel || {};
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
      if (!(this.props.AdmDatTier || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDatTier || {};

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
    const AdmDatTierState = this.props.AdmDatTier || {};

    if (AdmDatTierState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmDatTierState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmDatTierReduxObj.GetScreenButtons(AdmDatTierState) || {};
    const itemList = AdmDatTierState.Dtl || [];
    const auxLabels = AdmDatTierState.Label || {};
    const auxSystemLabels = AdmDatTierState.SystemLabel || {};

    const columnLabel = AdmDatTierState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmDatTierState);
    const authRow = (AdmDatTierState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmDatTier || {}).Mst || {});
    const currDtl = ((this.props.AdmDatTier || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.DataTierId195));
    const selectList = AdmDatTierReduxObj.SearchListToSelectList(AdmDatTierState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const DataTierId195 = currMst.DataTierId195;
const DataTierName195 = currMst.DataTierName195;
const EntityId195List = AdmDatTierReduxObj.ScreenDdlSelectors.EntityId195(AdmDatTierState);
const EntityId195 = currMst.EntityId195;
const DbProviderCd195List = AdmDatTierReduxObj.ScreenDdlSelectors.DbProviderCd195(AdmDatTierState);
const DbProviderCd195 = currMst.DbProviderCd195;
const ServerName195 = currMst.ServerName195;
const DesServer195 = currMst.DesServer195;
const DesDatabase195 = currMst.DesDatabase195;
const DesUserId195 = currMst.DesUserId195;
const DesPassword195 = currMst.DesPassword195;
const PortBinPath195 = currMst.PortBinPath195;
const InstBinPath195 = currMst.InstBinPath195;
const ScriptPath195 = currMst.ScriptPath195;
const DbDataPath195 = currMst.DbDataPath195;
const IsDevelopment195 = currMst.IsDevelopment195;
const IsDefault195 = currMst.IsDefault195;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmDatTierState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cDataTierId195: DataTierId195 || '',
                  cDataTierName195: DataTierName195 || '',
                  cEntityId195: EntityId195List.filter(obj => { return obj.key === EntityId195 })[0],
                  cDbProviderCd195: DbProviderCd195List.filter(obj => { return obj.key === DbProviderCd195 })[0],
                  cServerName195: ServerName195 || '',
                  cDesServer195: DesServer195 || '',
                  cDesDatabase195: DesDatabase195 || '',
                  cDesUserId195: DesUserId195 || '',
                  cDesPassword195: DesPassword195 || '',
                  cPortBinPath195: PortBinPath195 || '',
                  cInstBinPath195: InstBinPath195 || '',
                  cScriptPath195: ScriptPath195 || '',
                  cDbDataPath195: DbDataPath195 || '',
                  cIsDevelopment195: IsDevelopment195 === 'Y',
                  cIsDefault195: IsDefault195 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmDatTierState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).DataTierId195)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).DataTierId195)) return null;
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
            {(authCol.DataTierId195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataTierId195 || {}).ColumnHeader} {(columnLabel.DataTierId195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataTierId195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataTierId195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDataTierId195'
disabled = {(authCol.DataTierId195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataTierId195 && touched.cDataTierId195 && <span className='form__form-group-error'>{errors.cDataTierId195}</span>}
</div>
</Col>
}
{(authCol.DataTierName195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataTierName195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DataTierName195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataTierName195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataTierName195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDataTierName195'
disabled = {(authCol.DataTierName195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataTierName195 && touched.cDataTierName195 && <span className='form__form-group-error'>{errors.cDataTierName195}</span>}
</div>
</Col>
}
{(authCol.EntityId195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EntityId195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.EntityId195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EntityId195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EntityId195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cEntityId195'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cEntityId195')}
value={values.cEntityId195}
options={EntityId195List}
placeholder=''
disabled = {(authCol.EntityId195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEntityId195 && touched.cEntityId195 && <span className='form__form-group-error'>{errors.cEntityId195}</span>}
</div>
</Col>
}
{(authCol.DbProviderCd195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DbProviderCd195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DbProviderCd195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DbProviderCd195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DbProviderCd195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDbProviderCd195'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDbProviderCd195')}
value={values.cDbProviderCd195}
options={DbProviderCd195List}
placeholder=''
disabled = {(authCol.DbProviderCd195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDbProviderCd195 && touched.cDbProviderCd195 && <span className='form__form-group-error'>{errors.cDbProviderCd195}</span>}
</div>
</Col>
}
{(authCol.ServerName195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ServerName195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ServerName195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ServerName195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ServerName195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cServerName195'
disabled = {(authCol.ServerName195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cServerName195 && touched.cServerName195 && <span className='form__form-group-error'>{errors.cServerName195}</span>}
</div>
</Col>
}
{(authCol.DesServer195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DesServer195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DesServer195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DesServer195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DesServer195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDesServer195'
disabled = {(authCol.DesServer195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDesServer195 && touched.cDesServer195 && <span className='form__form-group-error'>{errors.cDesServer195}</span>}
</div>
</Col>
}
{(authCol.DesDatabase195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DesDatabase195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DesDatabase195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DesDatabase195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DesDatabase195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDesDatabase195'
disabled = {(authCol.DesDatabase195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDesDatabase195 && touched.cDesDatabase195 && <span className='form__form-group-error'>{errors.cDesDatabase195}</span>}
</div>
</Col>
}
{(authCol.DesUserId195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DesUserId195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DesUserId195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DesUserId195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DesUserId195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDesUserId195'
disabled = {(authCol.DesUserId195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDesUserId195 && touched.cDesUserId195 && <span className='form__form-group-error'>{errors.cDesUserId195}</span>}
</div>
</Col>
}
{(authCol.DesPassword195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DesPassword195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DesPassword195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DesPassword195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DesPassword195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDesPassword195'
disabled = {(authCol.DesPassword195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDesPassword195 && touched.cDesPassword195 && <span className='form__form-group-error'>{errors.cDesPassword195}</span>}
</div>
</Col>
}
{(authCol.PortBinPath195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PortBinPath195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.PortBinPath195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PortBinPath195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PortBinPath195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPortBinPath195'
disabled = {(authCol.PortBinPath195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPortBinPath195 && touched.cPortBinPath195 && <span className='form__form-group-error'>{errors.cPortBinPath195}</span>}
</div>
</Col>
}
{(authCol.InstBinPath195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InstBinPath195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.InstBinPath195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InstBinPath195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InstBinPath195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cInstBinPath195'
disabled = {(authCol.InstBinPath195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cInstBinPath195 && touched.cInstBinPath195 && <span className='form__form-group-error'>{errors.cInstBinPath195}</span>}
</div>
</Col>
}
{(authCol.ScriptPath195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScriptPath195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScriptPath195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScriptPath195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScriptPath195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScriptPath195'
disabled = {(authCol.ScriptPath195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScriptPath195 && touched.cScriptPath195 && <span className='form__form-group-error'>{errors.cScriptPath195}</span>}
</div>
</Col>
}
{(authCol.DbDataPath195 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DbDataPath195 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DbDataPath195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DbDataPath195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DbDataPath195 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDatTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDbDataPath195'
disabled = {(authCol.DbDataPath195 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDbDataPath195 && touched.cDbDataPath195 && <span className='form__form-group-error'>{errors.cDbDataPath195}</span>}
</div>
</Col>
}
{(authCol.IsDevelopment195 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsDevelopment195'
onChange={handleChange}
defaultChecked={values.cIsDevelopment195}
disabled={(authCol.IsDevelopment195 || {}).readonly || !(authCol.IsDevelopment195 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsDevelopment195 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsDevelopment195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsDevelopment195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsDevelopment195 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.IsDefault195 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsDefault195'
onChange={handleChange}
defaultChecked={values.cIsDefault195}
disabled={(authCol.IsDefault195 || {}).readonly || !(authCol.IsDefault195 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsDefault195 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsDefault195 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsDefault195 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsDefault195 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).DataTierId195)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).DataTierId195));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmDatTierState) && <Skeleton height='43px' />) ||
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
  AdmDatTier: state.AdmDatTier,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmDatTierReduxObj.LoadPage.bind(AdmDatTierReduxObj) },
    { SavePage: AdmDatTierReduxObj.SavePage.bind(AdmDatTierReduxObj) },
    { DelMst: AdmDatTierReduxObj.DelMst.bind(AdmDatTierReduxObj) },
    { AddMst: AdmDatTierReduxObj.AddMst.bind(AdmDatTierReduxObj) },
//    { SearchMemberId64: AdmDatTierReduxObj.SearchActions.SearchMemberId64.bind(AdmDatTierReduxObj) },
//    { SearchCurrencyId64: AdmDatTierReduxObj.SearchActions.SearchCurrencyId64.bind(AdmDatTierReduxObj) },
//    { SearchCustomerJobId64: AdmDatTierReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmDatTierReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            