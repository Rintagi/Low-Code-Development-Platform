
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
import AdmClnTierReduxObj, { ShowMstFilterApplied } from '../../redux/AdmClnTier';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmClnTier || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ClientTierId194';
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
    const columnLabel = (this.props.AdmClnTier || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cClientTierName194) { errors.cClientTierName194 = (columnLabel.ClientTierName194 || {}).ErrMessage;}
if (isEmptyId((values.cEntityId194 || {}).value)) { errors.cEntityId194 = (columnLabel.EntityId194 || {}).ErrMessage;}
if (isEmptyId((values.cLanguageCd194 || {}).value)) { errors.cLanguageCd194 = (columnLabel.LanguageCd194 || {}).ErrMessage;}
if (isEmptyId((values.cFrameworkCd194 || {}).value)) { errors.cFrameworkCd194 = (columnLabel.FrameworkCd194 || {}).ErrMessage;}
if (!values.cDevProgramPath194) { errors.cDevProgramPath194 = (columnLabel.DevProgramPath194 || {}).ErrMessage;}
if (!values.cWsProgramPath194) { errors.cWsProgramPath194 = (columnLabel.WsProgramPath194 || {}).ErrMessage;}
if (!values.cXlsProgramPath194) { errors.cXlsProgramPath194 = (columnLabel.XlsProgramPath194 || {}).ErrMessage;}
if (!values.cDevCompilePath194) { errors.cDevCompilePath194 = (columnLabel.DevCompilePath194 || {}).ErrMessage;}
if (!values.cWsCompilePath194) { errors.cWsCompilePath194 = (columnLabel.WsCompilePath194 || {}).ErrMessage;}
if (!values.cXlsCompilePath194) { errors.cXlsCompilePath194 = (columnLabel.XlsCompilePath194 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmClnTier || {}).Mst || {};
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
        this.props.AdmClnTier,
        {
          ClientTierId194: values.cClientTierId194|| '',
          ClientTierName194: values.cClientTierName194|| '',
          EntityId194: (values.cEntityId194|| {}).value || '',
          LanguageCd194: (values.cLanguageCd194|| {}).value || '',
          FrameworkCd194: (values.cFrameworkCd194|| {}).value || '',
          DevProgramPath194: values.cDevProgramPath194|| '',
          WsProgramPath194: values.cWsProgramPath194|| '',
          XlsProgramPath194: values.cXlsProgramPath194|| '',
          DevCompilePath194: values.cDevCompilePath194|| '',
          WsCompilePath194: values.cWsCompilePath194|| '',
          XlsCompilePath194: values.cXlsCompilePath194|| '',
          CombineAsm194: values.cCombineAsm194 ? 'Y' : 'N',
          IsDefault194: values.cIsDefault194 ? 'Y' : 'N',
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
    const AdmClnTierState = this.props.AdmClnTier || {};
    const auxSystemLabels = AdmClnTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ClientTierId194;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmClnTier.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmClnTier.Label);
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
    const AdmClnTierState = this.props.AdmClnTier || {};
    const auxSystemLabels = AdmClnTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ClientTierId194;
        this.props.DelMst(this.props.AdmClnTier, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmClnTier || {};
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
    const AdmClnTierState = this.props.AdmClnTier || {};
    const auxSystemLabels = AdmClnTierState.SystemLabel || {};
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
      if (!(this.props.AdmClnTier || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmClnTier || {};

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
    const AdmClnTierState = this.props.AdmClnTier || {};

    if (AdmClnTierState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmClnTierState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmClnTierReduxObj.GetScreenButtons(AdmClnTierState) || {};
    const itemList = AdmClnTierState.Dtl || [];
    const auxLabels = AdmClnTierState.Label || {};
    const auxSystemLabels = AdmClnTierState.SystemLabel || {};

    const columnLabel = AdmClnTierState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmClnTierState);
    const authRow = (AdmClnTierState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmClnTier || {}).Mst || {});
    const currDtl = ((this.props.AdmClnTier || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ClientTierId194));
    const selectList = AdmClnTierReduxObj.SearchListToSelectList(AdmClnTierState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ClientTierId194 = currMst.ClientTierId194;
const ClientTierName194 = currMst.ClientTierName194;
const EntityId194List = AdmClnTierReduxObj.ScreenDdlSelectors.EntityId194(AdmClnTierState);
const EntityId194 = currMst.EntityId194;
const LanguageCd194List = AdmClnTierReduxObj.ScreenDdlSelectors.LanguageCd194(AdmClnTierState);
const LanguageCd194 = currMst.LanguageCd194;
const FrameworkCd194List = AdmClnTierReduxObj.ScreenDdlSelectors.FrameworkCd194(AdmClnTierState);
const FrameworkCd194 = currMst.FrameworkCd194;
const DevProgramPath194 = currMst.DevProgramPath194;
const WsProgramPath194 = currMst.WsProgramPath194;
const XlsProgramPath194 = currMst.XlsProgramPath194;
const DevCompilePath194 = currMst.DevCompilePath194;
const WsCompilePath194 = currMst.WsCompilePath194;
const XlsCompilePath194 = currMst.XlsCompilePath194;
const CombineAsm194 = currMst.CombineAsm194;
const IsDefault194 = currMst.IsDefault194;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmClnTierState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cClientTierId194: ClientTierId194 || '',
                  cClientTierName194: ClientTierName194 || '',
                  cEntityId194: EntityId194List.filter(obj => { return obj.key === EntityId194 })[0],
                  cLanguageCd194: LanguageCd194List.filter(obj => { return obj.key === LanguageCd194 })[0],
                  cFrameworkCd194: FrameworkCd194List.filter(obj => { return obj.key === FrameworkCd194 })[0],
                  cDevProgramPath194: DevProgramPath194 || '',
                  cWsProgramPath194: WsProgramPath194 || '',
                  cXlsProgramPath194: XlsProgramPath194 || '',
                  cDevCompilePath194: DevCompilePath194 || '',
                  cWsCompilePath194: WsCompilePath194 || '',
                  cXlsCompilePath194: XlsCompilePath194 || '',
                  cCombineAsm194: CombineAsm194 === 'Y',
                  cIsDefault194: IsDefault194 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmClnTierState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientTierId194)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientTierId194)) return null;
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
            {(authCol.ClientTierId194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientTierId194 || {}).ColumnHeader} {(columnLabel.ClientTierId194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientTierId194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientTierId194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cClientTierId194'
disabled = {(authCol.ClientTierId194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientTierId194 && touched.cClientTierId194 && <span className='form__form-group-error'>{errors.cClientTierId194}</span>}
</div>
</Col>
}
{(authCol.ClientTierName194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ClientTierName194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ClientTierName194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ClientTierName194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ClientTierName194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cClientTierName194'
disabled = {(authCol.ClientTierName194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cClientTierName194 && touched.cClientTierName194 && <span className='form__form-group-error'>{errors.cClientTierName194}</span>}
</div>
</Col>
}
{(authCol.EntityId194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EntityId194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.EntityId194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EntityId194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EntityId194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cEntityId194'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cEntityId194')}
value={values.cEntityId194}
options={EntityId194List}
placeholder=''
disabled = {(authCol.EntityId194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEntityId194 && touched.cEntityId194 && <span className='form__form-group-error'>{errors.cEntityId194}</span>}
</div>
</Col>
}
{(authCol.LanguageCd194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LanguageCd194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LanguageCd194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LanguageCd194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LanguageCd194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cLanguageCd194'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cLanguageCd194')}
value={values.cLanguageCd194}
options={LanguageCd194List}
placeholder=''
disabled = {(authCol.LanguageCd194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLanguageCd194 && touched.cLanguageCd194 && <span className='form__form-group-error'>{errors.cLanguageCd194}</span>}
</div>
</Col>
}
{(authCol.FrameworkCd194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FrameworkCd194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.FrameworkCd194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FrameworkCd194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FrameworkCd194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cFrameworkCd194'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cFrameworkCd194')}
value={values.cFrameworkCd194}
options={FrameworkCd194List}
placeholder=''
disabled = {(authCol.FrameworkCd194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFrameworkCd194 && touched.cFrameworkCd194 && <span className='form__form-group-error'>{errors.cFrameworkCd194}</span>}
</div>
</Col>
}
{(authCol.DevProgramPath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DevProgramPath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DevProgramPath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DevProgramPath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DevProgramPath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDevProgramPath194'
disabled = {(authCol.DevProgramPath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDevProgramPath194 && touched.cDevProgramPath194 && <span className='form__form-group-error'>{errors.cDevProgramPath194}</span>}
</div>
</Col>
}
{(authCol.WsProgramPath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WsProgramPath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.WsProgramPath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WsProgramPath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WsProgramPath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWsProgramPath194'
disabled = {(authCol.WsProgramPath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWsProgramPath194 && touched.cWsProgramPath194 && <span className='form__form-group-error'>{errors.cWsProgramPath194}</span>}
</div>
</Col>
}
{(authCol.XlsProgramPath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.XlsProgramPath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.XlsProgramPath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.XlsProgramPath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.XlsProgramPath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cXlsProgramPath194'
disabled = {(authCol.XlsProgramPath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cXlsProgramPath194 && touched.cXlsProgramPath194 && <span className='form__form-group-error'>{errors.cXlsProgramPath194}</span>}
</div>
</Col>
}
{(authCol.DevCompilePath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DevCompilePath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DevCompilePath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DevCompilePath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DevCompilePath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDevCompilePath194'
disabled = {(authCol.DevCompilePath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDevCompilePath194 && touched.cDevCompilePath194 && <span className='form__form-group-error'>{errors.cDevCompilePath194}</span>}
</div>
</Col>
}
{(authCol.WsCompilePath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WsCompilePath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.WsCompilePath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WsCompilePath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WsCompilePath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWsCompilePath194'
disabled = {(authCol.WsCompilePath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWsCompilePath194 && touched.cWsCompilePath194 && <span className='form__form-group-error'>{errors.cWsCompilePath194}</span>}
</div>
</Col>
}
{(authCol.XlsCompilePath194 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.XlsCompilePath194 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.XlsCompilePath194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.XlsCompilePath194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.XlsCompilePath194 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmClnTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cXlsCompilePath194'
disabled = {(authCol.XlsCompilePath194 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cXlsCompilePath194 && touched.cXlsCompilePath194 && <span className='form__form-group-error'>{errors.cXlsCompilePath194}</span>}
</div>
</Col>
}
{(authCol.CombineAsm194 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCombineAsm194'
onChange={handleChange}
defaultChecked={values.cCombineAsm194}
disabled={(authCol.CombineAsm194 || {}).readonly || !(authCol.CombineAsm194 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CombineAsm194 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CombineAsm194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CombineAsm194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CombineAsm194 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.IsDefault194 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsDefault194'
onChange={handleChange}
defaultChecked={values.cIsDefault194}
disabled={(authCol.IsDefault194 || {}).readonly || !(authCol.IsDefault194 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsDefault194 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsDefault194 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsDefault194 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsDefault194 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ClientTierId194)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ClientTierId194));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmClnTierState) && <Skeleton height='43px' />) ||
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
  AdmClnTier: state.AdmClnTier,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmClnTierReduxObj.LoadPage.bind(AdmClnTierReduxObj) },
    { SavePage: AdmClnTierReduxObj.SavePage.bind(AdmClnTierReduxObj) },
    { DelMst: AdmClnTierReduxObj.DelMst.bind(AdmClnTierReduxObj) },
    { AddMst: AdmClnTierReduxObj.AddMst.bind(AdmClnTierReduxObj) },
//    { SearchMemberId64: AdmClnTierReduxObj.SearchActions.SearchMemberId64.bind(AdmClnTierReduxObj) },
//    { SearchCurrencyId64: AdmClnTierReduxObj.SearchActions.SearchCurrencyId64.bind(AdmClnTierReduxObj) },
//    { SearchCustomerJobId64: AdmClnTierReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmClnTierReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            