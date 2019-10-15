
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
import AdmScreenCriReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScreenCri';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreenCri || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ScreenCriId104';
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

ScreenId104InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId104(v, filterBy);}}
ColumnId104InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchColumnId104(v, filterBy);}}
DdlKeyColumnId104InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlKeyColumnId104(v, filterBy);}}
DdlRefColumnId104InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlRefColumnId104(v, filterBy);}}
DdlSrtColumnId104InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlSrtColumnId104(v, filterBy);}}
DdlFtrColumnId104InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmScreenCri || {}).Mst || {}).ScreenId104; _this.props.SearchDdlFtrColumnId104(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmScreenCri || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cScreenId104 || {}).value)) { errors.cScreenId104 = (columnLabel.ScreenId104 || {}).ErrMessage;}
if (isEmptyId((values.cColumnId104 || {}).value)) { errors.cColumnId104 = (columnLabel.ColumnId104 || {}).ErrMessage;}
if (isEmptyId((values.cOperatorId104 || {}).value)) { errors.cOperatorId104 = (columnLabel.OperatorId104 || {}).ErrMessage;}
if (isEmptyId((values.cDisplayModeId104 || {}).value)) { errors.cDisplayModeId104 = (columnLabel.DisplayModeId104 || {}).ErrMessage;}
if (!values.cTabIndex104) { errors.cTabIndex104 = (columnLabel.TabIndex104 || {}).ErrMessage;}
if (isEmptyId((values.cColumnJustify104 || {}).value)) { errors.cColumnJustify104 = (columnLabel.ColumnJustify104 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmScreenCri || {}).Mst || {};
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
        this.props.AdmScreenCri,
        {
          ScreenCriId104: values.cScreenCriId104|| '',
          ScreenId104: (values.cScreenId104|| {}).value || '',
          LabelCss104: values.cLabelCss104|| '',
          ContentCss104: values.cContentCss104|| '',
          ColumnId104: (values.cColumnId104|| {}).value || '',
          OperatorId104: (values.cOperatorId104|| {}).value || '',
          DisplayModeId104: (values.cDisplayModeId104|| {}).value || '',
          TabIndex104: values.cTabIndex104|| '',
          RequiredValid104: values.cRequiredValid104 ? 'Y' : 'N',
          ColumnJustify104: (values.cColumnJustify104|| {}).value || '',
          ColumnSize104: values.cColumnSize104|| '',
          RowSize104: values.cRowSize104|| '',
          DdlKeyColumnId104: (values.cDdlKeyColumnId104|| {}).value || '',
          DdlRefColumnId104: (values.cDdlRefColumnId104|| {}).value || '',
          DdlSrtColumnId104: (values.cDdlSrtColumnId104|| {}).value || '',
          DdlFtrColumnId104: (values.cDdlFtrColumnId104|| {}).value || '',
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
    const AdmScreenCriState = this.props.AdmScreenCri || {};
    const auxSystemLabels = AdmScreenCriState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ScreenCriId104;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmScreenCri.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmScreenCri.Label);
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
    const AdmScreenCriState = this.props.AdmScreenCri || {};
    const auxSystemLabels = AdmScreenCriState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ScreenCriId104;
        this.props.DelMst(this.props.AdmScreenCri, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmScreenCri || {};
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
    const AdmScreenCriState = this.props.AdmScreenCri || {};
    const auxSystemLabels = AdmScreenCriState.SystemLabel || {};
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
      if (!(this.props.AdmScreenCri || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScreenCri || {};

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
    const AdmScreenCriState = this.props.AdmScreenCri || {};

    if (AdmScreenCriState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmScreenCriState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmScreenCriReduxObj.GetScreenButtons(AdmScreenCriState) || {};
    const itemList = AdmScreenCriState.Dtl || [];
    const auxLabels = AdmScreenCriState.Label || {};
    const auxSystemLabels = AdmScreenCriState.SystemLabel || {};

    const columnLabel = AdmScreenCriState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmScreenCriState);
    const authRow = (AdmScreenCriState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmScreenCri || {}).Mst || {});
    const currDtl = ((this.props.AdmScreenCri || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ScreenCriId104));
    const selectList = AdmScreenCriReduxObj.SearchListToSelectList(AdmScreenCriState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ScreenCriId104 = currMst.ScreenCriId104;
const ScreenId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.ScreenId104(AdmScreenCriState);
const ScreenId104 = currMst.ScreenId104;
const LabelCss104 = currMst.LabelCss104;
const ContentCss104 = currMst.ContentCss104;
const ColumnId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.ColumnId104(AdmScreenCriState);
const ColumnId104 = currMst.ColumnId104;
const OperatorId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.OperatorId104(AdmScreenCriState);
const OperatorId104 = currMst.OperatorId104;
const DisplayModeId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.DisplayModeId104(AdmScreenCriState);
const DisplayModeId104 = currMst.DisplayModeId104;
const TabIndex104 = currMst.TabIndex104;
const RequiredValid104 = currMst.RequiredValid104;
const ColumnJustify104List = AdmScreenCriReduxObj.ScreenDdlSelectors.ColumnJustify104(AdmScreenCriState);
const ColumnJustify104 = currMst.ColumnJustify104;
const ColumnSize104 = currMst.ColumnSize104;
const RowSize104 = currMst.RowSize104;
const DdlKeyColumnId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.DdlKeyColumnId104(AdmScreenCriState);
const DdlKeyColumnId104 = currMst.DdlKeyColumnId104;
const DdlRefColumnId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.DdlRefColumnId104(AdmScreenCriState);
const DdlRefColumnId104 = currMst.DdlRefColumnId104;
const DdlSrtColumnId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.DdlSrtColumnId104(AdmScreenCriState);
const DdlSrtColumnId104 = currMst.DdlSrtColumnId104;
const DdlFtrColumnId104List = AdmScreenCriReduxObj.ScreenDdlSelectors.DdlFtrColumnId104(AdmScreenCriState);
const DdlFtrColumnId104 = currMst.DdlFtrColumnId104;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmScreenCriState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cScreenCriId104: ScreenCriId104 || '',
                  cScreenId104: ScreenId104List.filter(obj => { return obj.key === ScreenId104 })[0],
                  cLabelCss104: LabelCss104 || '',
                  cContentCss104: ContentCss104 || '',
                  cColumnId104: ColumnId104List.filter(obj => { return obj.key === ColumnId104 })[0],
                  cOperatorId104: OperatorId104List.filter(obj => { return obj.key === OperatorId104 })[0],
                  cDisplayModeId104: DisplayModeId104List.filter(obj => { return obj.key === DisplayModeId104 })[0],
                  cTabIndex104: TabIndex104 || '',
                  cRequiredValid104: RequiredValid104 === 'Y',
                  cColumnJustify104: ColumnJustify104List.filter(obj => { return obj.key === ColumnJustify104 })[0],
                  cColumnSize104: ColumnSize104 || '',
                  cRowSize104: RowSize104 || '',
                  cDdlKeyColumnId104: DdlKeyColumnId104List.filter(obj => { return obj.key === DdlKeyColumnId104 })[0],
                  cDdlRefColumnId104: DdlRefColumnId104List.filter(obj => { return obj.key === DdlRefColumnId104 })[0],
                  cDdlSrtColumnId104: DdlSrtColumnId104List.filter(obj => { return obj.key === DdlSrtColumnId104 })[0],
                  cDdlFtrColumnId104: DdlFtrColumnId104List.filter(obj => { return obj.key === DdlFtrColumnId104 })[0],
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
                                {(this.constructor.ShowSpinner(AdmScreenCriState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenCriId104)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenCriId104)) return null;
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
            {(authCol.ScreenCriId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenCriId104 || {}).ColumnHeader} {(columnLabel.ScreenCriId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenCriId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenCriId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenCriId104'
disabled = {(authCol.ScreenCriId104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenCriId104 && touched.cScreenCriId104 && <span className='form__form-group-error'>{errors.cScreenCriId104}</span>}
</div>
</Col>
}
{(authCol.ScreenId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId104', true)}
onInputChange={this.ScreenId104InputChange()}
value={values.cScreenId104}
defaultSelected={ScreenId104List.filter(obj => { return obj.key === ScreenId104 })}
options={ScreenId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId104 && touched.cScreenId104 && <span className='form__form-group-error'>{errors.cScreenId104}</span>}
</div>
</Col>
}
{(authCol.LabelCss104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelCss104 || {}).ColumnHeader} {(columnLabel.LabelCss104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelCss104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelCss104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelCss104'
disabled = {(authCol.LabelCss104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelCss104 && touched.cLabelCss104 && <span className='form__form-group-error'>{errors.cLabelCss104}</span>}
</div>
</Col>
}
{(authCol.ContentCss104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ContentCss104 || {}).ColumnHeader} {(columnLabel.ContentCss104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ContentCss104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ContentCss104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cContentCss104'
disabled = {(authCol.ContentCss104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cContentCss104 && touched.cContentCss104 && <span className='form__form-group-error'>{errors.cContentCss104}</span>}
</div>
</Col>
}
{(authCol.ColumnId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnId104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cColumnId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId104', true)}
onInputChange={this.ColumnId104InputChange()}
value={values.cColumnId104}
defaultSelected={ColumnId104List.filter(obj => { return obj.key === ColumnId104 })}
options={ColumnId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ColumnId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cColumnId104 && touched.cColumnId104 && <span className='form__form-group-error'>{errors.cColumnId104}</span>}
</div>
</Col>
}
{(authCol.OperatorId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.OperatorId104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.OperatorId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OperatorId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OperatorId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cOperatorId104'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cOperatorId104')}
value={values.cOperatorId104}
options={OperatorId104List}
placeholder=''
disabled = {(authCol.OperatorId104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cOperatorId104 && touched.cOperatorId104 && <span className='form__form-group-error'>{errors.cOperatorId104}</span>}
</div>
</Col>
}
{(authCol.DisplayModeId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DisplayModeId104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DisplayModeId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DisplayModeId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DisplayModeId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDisplayModeId104'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDisplayModeId104')}
value={values.cDisplayModeId104}
options={DisplayModeId104List}
placeholder=''
disabled = {(authCol.DisplayModeId104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDisplayModeId104 && touched.cDisplayModeId104 && <span className='form__form-group-error'>{errors.cDisplayModeId104}</span>}
</div>
</Col>
}
{(authCol.TabIndex104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TabIndex104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TabIndex104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TabIndex104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TabIndex104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTabIndex104'
disabled = {(authCol.TabIndex104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTabIndex104 && touched.cTabIndex104 && <span className='form__form-group-error'>{errors.cTabIndex104}</span>}
</div>
</Col>
}
{(authCol.RequiredValid104 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cRequiredValid104'
onChange={handleChange}
defaultChecked={values.cRequiredValid104}
disabled={(authCol.RequiredValid104 || {}).readonly || !(authCol.RequiredValid104 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.RequiredValid104 || {}).ColumnHeader}</span>
</label>
{(columnLabel.RequiredValid104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RequiredValid104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RequiredValid104 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColumnJustify104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnJustify104 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnJustify104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnJustify104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnJustify104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cColumnJustify104'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cColumnJustify104')}
value={values.cColumnJustify104}
options={ColumnJustify104List}
placeholder=''
disabled = {(authCol.ColumnJustify104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnJustify104 && touched.cColumnJustify104 && <span className='form__form-group-error'>{errors.cColumnJustify104}</span>}
</div>
</Col>
}
{(authCol.ColumnSize104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnSize104 || {}).ColumnHeader} {(columnLabel.ColumnSize104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnSize104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnSize104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnSize104'
disabled = {(authCol.ColumnSize104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnSize104 && touched.cColumnSize104 && <span className='form__form-group-error'>{errors.cColumnSize104}</span>}
</div>
</Col>
}
{(authCol.RowSize104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowSize104 || {}).ColumnHeader} {(columnLabel.RowSize104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowSize104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowSize104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowSize104'
disabled = {(authCol.RowSize104 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowSize104 && touched.cRowSize104 && <span className='form__form-group-error'>{errors.cRowSize104}</span>}
</div>
</Col>
}
{(authCol.DdlKeyColumnId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlKeyColumnId104 || {}).ColumnHeader} {(columnLabel.DdlKeyColumnId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlKeyColumnId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlKeyColumnId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlKeyColumnId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlKeyColumnId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlKeyColumnId104', true)}
onInputChange={this.DdlKeyColumnId104InputChange()}
value={values.cDdlKeyColumnId104}
defaultSelected={DdlKeyColumnId104List.filter(obj => { return obj.key === DdlKeyColumnId104 })}
options={DdlKeyColumnId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlKeyColumnId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlKeyColumnId104 && touched.cDdlKeyColumnId104 && <span className='form__form-group-error'>{errors.cDdlKeyColumnId104}</span>}
</div>
</Col>
}
{(authCol.DdlRefColumnId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlRefColumnId104 || {}).ColumnHeader} {(columnLabel.DdlRefColumnId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlRefColumnId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlRefColumnId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlRefColumnId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlRefColumnId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlRefColumnId104', true)}
onInputChange={this.DdlRefColumnId104InputChange()}
value={values.cDdlRefColumnId104}
defaultSelected={DdlRefColumnId104List.filter(obj => { return obj.key === DdlRefColumnId104 })}
options={DdlRefColumnId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlRefColumnId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlRefColumnId104 && touched.cDdlRefColumnId104 && <span className='form__form-group-error'>{errors.cDdlRefColumnId104}</span>}
</div>
</Col>
}
{(authCol.DdlSrtColumnId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlSrtColumnId104 || {}).ColumnHeader} {(columnLabel.DdlSrtColumnId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlSrtColumnId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlSrtColumnId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlSrtColumnId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlSrtColumnId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlSrtColumnId104', true)}
onInputChange={this.DdlSrtColumnId104InputChange()}
value={values.cDdlSrtColumnId104}
defaultSelected={DdlSrtColumnId104List.filter(obj => { return obj.key === DdlSrtColumnId104 })}
options={DdlSrtColumnId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlSrtColumnId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlSrtColumnId104 && touched.cDdlSrtColumnId104 && <span className='form__form-group-error'>{errors.cDdlSrtColumnId104}</span>}
</div>
</Col>
}
{(authCol.DdlFtrColumnId104 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlFtrColumnId104 || {}).ColumnHeader} {(columnLabel.DdlFtrColumnId104 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlFtrColumnId104 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlFtrColumnId104 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlFtrColumnId104'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId104', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId104', true)}
onInputChange={this.DdlFtrColumnId104InputChange()}
value={values.cDdlFtrColumnId104}
defaultSelected={DdlFtrColumnId104List.filter(obj => { return obj.key === DdlFtrColumnId104 })}
options={DdlFtrColumnId104List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlFtrColumnId104 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlFtrColumnId104 && touched.cDdlFtrColumnId104 && <span className='form__form-group-error'>{errors.cDdlFtrColumnId104}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenCriId104)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ScreenCriId104));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmScreenCriState) && <Skeleton height='43px' />) ||
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
  AdmScreenCri: state.AdmScreenCri,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScreenCriReduxObj.LoadPage.bind(AdmScreenCriReduxObj) },
    { SavePage: AdmScreenCriReduxObj.SavePage.bind(AdmScreenCriReduxObj) },
    { DelMst: AdmScreenCriReduxObj.DelMst.bind(AdmScreenCriReduxObj) },
    { AddMst: AdmScreenCriReduxObj.AddMst.bind(AdmScreenCriReduxObj) },
//    { SearchMemberId64: AdmScreenCriReduxObj.SearchActions.SearchMemberId64.bind(AdmScreenCriReduxObj) },
//    { SearchCurrencyId64: AdmScreenCriReduxObj.SearchActions.SearchCurrencyId64.bind(AdmScreenCriReduxObj) },
//    { SearchCustomerJobId64: AdmScreenCriReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmScreenCriReduxObj) },
{ SearchScreenId104: AdmScreenCriReduxObj.SearchActions.SearchScreenId104.bind(AdmScreenCriReduxObj) },
{ SearchColumnId104: AdmScreenCriReduxObj.SearchActions.SearchColumnId104.bind(AdmScreenCriReduxObj) },
{ SearchDdlKeyColumnId104: AdmScreenCriReduxObj.SearchActions.SearchDdlKeyColumnId104.bind(AdmScreenCriReduxObj) },
{ SearchDdlRefColumnId104: AdmScreenCriReduxObj.SearchActions.SearchDdlRefColumnId104.bind(AdmScreenCriReduxObj) },
{ SearchDdlSrtColumnId104: AdmScreenCriReduxObj.SearchActions.SearchDdlSrtColumnId104.bind(AdmScreenCriReduxObj) },
{ SearchDdlFtrColumnId104: AdmScreenCriReduxObj.SearchActions.SearchDdlFtrColumnId104.bind(AdmScreenCriReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            