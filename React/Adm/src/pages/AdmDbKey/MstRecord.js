
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
import AdmDbKeyReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDbKey';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDbKey || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'KeyId20';
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

TableId20InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchTableId20(v, filterBy);}}
ColumnId20InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmDbKey || {}).Mst || {}).TableId20; _this.props.SearchColumnId20(v, filterBy);}}
RefTableId20InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRefTableId20(v, filterBy);}}
RefColumnId20InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmDbKey || {}).Mst || {}).RefTableId20; _this.props.SearchRefColumnId20(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmDbKey || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cKeyName20) { errors.cKeyName20 = (columnLabel.KeyName20 || {}).ErrMessage;}
if (isEmptyId((values.cTableId20 || {}).value)) { errors.cTableId20 = (columnLabel.TableId20 || {}).ErrMessage;}
if (isEmptyId((values.cColumnId20 || {}).value)) { errors.cColumnId20 = (columnLabel.ColumnId20 || {}).ErrMessage;}
if (isEmptyId((values.cRefColumnId20 || {}).value)) { errors.cRefColumnId20 = (columnLabel.RefColumnId20 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmDbKey || {}).Mst || {};
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
        this.props.AdmDbKey,
        {
          KeyId20: values.cKeyId20|| '',
          KeyName20: values.cKeyName20|| '',
          TableId20: (values.cTableId20|| {}).value || '',
          ColumnId20: (values.cColumnId20|| {}).value || '',
          RefTableId20: (values.cRefTableId20|| {}).value || '',
          RefColumnId20: (values.cRefColumnId20|| {}).value || '',
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
    const AdmDbKeyState = this.props.AdmDbKey || {};
    const auxSystemLabels = AdmDbKeyState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).KeyId20;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmDbKey.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmDbKey.Label);
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
    const AdmDbKeyState = this.props.AdmDbKey || {};
    const auxSystemLabels = AdmDbKeyState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.KeyId20;
        this.props.DelMst(this.props.AdmDbKey, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmDbKey || {};
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
    const AdmDbKeyState = this.props.AdmDbKey || {};
    const auxSystemLabels = AdmDbKeyState.SystemLabel || {};
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
      if (!(this.props.AdmDbKey || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDbKey || {};

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
    const AdmDbKeyState = this.props.AdmDbKey || {};

    if (AdmDbKeyState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmDbKeyState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmDbKeyReduxObj.GetScreenButtons(AdmDbKeyState) || {};
    const itemList = AdmDbKeyState.Dtl || [];
    const auxLabels = AdmDbKeyState.Label || {};
    const auxSystemLabels = AdmDbKeyState.SystemLabel || {};

    const columnLabel = AdmDbKeyState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmDbKeyState);
    const authRow = (AdmDbKeyState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmDbKey || {}).Mst || {});
    const currDtl = ((this.props.AdmDbKey || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.KeyId20));
    const selectList = AdmDbKeyReduxObj.SearchListToSelectList(AdmDbKeyState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const KeyId20 = currMst.KeyId20;
const KeyName20 = currMst.KeyName20;
const TableId20List = AdmDbKeyReduxObj.ScreenDdlSelectors.TableId20(AdmDbKeyState);
const TableId20 = currMst.TableId20;
const ColumnId20List = AdmDbKeyReduxObj.ScreenDdlSelectors.ColumnId20(AdmDbKeyState);
const ColumnId20 = currMst.ColumnId20;
const RefTableId20List = AdmDbKeyReduxObj.ScreenDdlSelectors.RefTableId20(AdmDbKeyState);
const RefTableId20 = currMst.RefTableId20;
const RefColumnId20List = AdmDbKeyReduxObj.ScreenDdlSelectors.RefColumnId20(AdmDbKeyState);
const RefColumnId20 = currMst.RefColumnId20;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmDbKeyState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cKeyId20: KeyId20 || '',
                  cKeyName20: KeyName20 || '',
                  cTableId20: TableId20List.filter(obj => { return obj.key === TableId20 })[0],
                  cColumnId20: ColumnId20List.filter(obj => { return obj.key === ColumnId20 })[0],
                  cRefTableId20: RefTableId20List.filter(obj => { return obj.key === RefTableId20 })[0],
                  cRefColumnId20: RefColumnId20List.filter(obj => { return obj.key === RefColumnId20 })[0],
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
                                {(this.constructor.ShowSpinner(AdmDbKeyState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).KeyId20)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).KeyId20)) return null;
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
            {(authCol.KeyId20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.KeyId20 || {}).ColumnHeader} {(columnLabel.KeyId20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.KeyId20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.KeyId20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cKeyId20'
disabled = {(authCol.KeyId20 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cKeyId20 && touched.cKeyId20 && <span className='form__form-group-error'>{errors.cKeyId20}</span>}
</div>
</Col>
}
{(authCol.KeyName20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.KeyName20 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.KeyName20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.KeyName20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.KeyName20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cKeyName20'
disabled = {(authCol.KeyName20 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cKeyName20 && touched.cKeyName20 && <span className='form__form-group-error'>{errors.cKeyName20}</span>}
</div>
</Col>
}
{(authCol.TableId20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableId20 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TableId20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableId20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableId20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cTableId20'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cTableId20', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cTableId20', true)}
onInputChange={this.TableId20InputChange()}
value={values.cTableId20}
defaultSelected={TableId20List.filter(obj => { return obj.key === TableId20 })}
options={TableId20List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.TableId20 || {}).readonly ? true: false }/>
</div>
}
{errors.cTableId20 && touched.cTableId20 && <span className='form__form-group-error'>{errors.cTableId20}</span>}
</div>
</Col>
}
{(authCol.ColumnId20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnId20 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnId20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnId20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnId20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cColumnId20'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId20', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId20', true)}
onInputChange={this.ColumnId20InputChange()}
value={values.cColumnId20}
defaultSelected={ColumnId20List.filter(obj => { return obj.key === ColumnId20 })}
options={ColumnId20List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ColumnId20 || {}).readonly ? true: false }/>
</div>
}
{errors.cColumnId20 && touched.cColumnId20 && <span className='form__form-group-error'>{errors.cColumnId20}</span>}
</div>
</Col>
}
{(authCol.RefTableId20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RefTableId20 || {}).ColumnHeader} {(columnLabel.RefTableId20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RefTableId20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RefTableId20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRefTableId20'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRefTableId20', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRefTableId20', true)}
onInputChange={this.RefTableId20InputChange()}
value={values.cRefTableId20}
defaultSelected={RefTableId20List.filter(obj => { return obj.key === RefTableId20 })}
options={RefTableId20List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RefTableId20 || {}).readonly ? true: false }/>
</div>
}
{errors.cRefTableId20 && touched.cRefTableId20 && <span className='form__form-group-error'>{errors.cRefTableId20}</span>}
</div>
</Col>
}
{(authCol.RefColumnId20 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RefColumnId20 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RefColumnId20 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RefColumnId20 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RefColumnId20 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbKeyState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRefColumnId20'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRefColumnId20', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRefColumnId20', true)}
onInputChange={this.RefColumnId20InputChange()}
value={values.cRefColumnId20}
defaultSelected={RefColumnId20List.filter(obj => { return obj.key === RefColumnId20 })}
options={RefColumnId20List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RefColumnId20 || {}).readonly ? true: false }/>
</div>
}
{errors.cRefColumnId20 && touched.cRefColumnId20 && <span className='form__form-group-error'>{errors.cRefColumnId20}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).KeyId20)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).KeyId20));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmDbKeyState) && <Skeleton height='43px' />) ||
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
  AdmDbKey: state.AdmDbKey,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmDbKeyReduxObj.LoadPage.bind(AdmDbKeyReduxObj) },
    { SavePage: AdmDbKeyReduxObj.SavePage.bind(AdmDbKeyReduxObj) },
    { DelMst: AdmDbKeyReduxObj.DelMst.bind(AdmDbKeyReduxObj) },
    { AddMst: AdmDbKeyReduxObj.AddMst.bind(AdmDbKeyReduxObj) },
//    { SearchMemberId64: AdmDbKeyReduxObj.SearchActions.SearchMemberId64.bind(AdmDbKeyReduxObj) },
//    { SearchCurrencyId64: AdmDbKeyReduxObj.SearchActions.SearchCurrencyId64.bind(AdmDbKeyReduxObj) },
//    { SearchCustomerJobId64: AdmDbKeyReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmDbKeyReduxObj) },
{ SearchTableId20: AdmDbKeyReduxObj.SearchActions.SearchTableId20.bind(AdmDbKeyReduxObj) },
{ SearchColumnId20: AdmDbKeyReduxObj.SearchActions.SearchColumnId20.bind(AdmDbKeyReduxObj) },
{ SearchRefTableId20: AdmDbKeyReduxObj.SearchActions.SearchRefTableId20.bind(AdmDbKeyReduxObj) },
{ SearchRefColumnId20: AdmDbKeyReduxObj.SearchActions.SearchRefColumnId20.bind(AdmDbKeyReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            