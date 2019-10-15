
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
import AdmCtCultureReduxObj, { ShowMstFilterApplied } from '../../redux/AdmCtCulture';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmCtCulture || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'CultureTypeId8';
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

CountryCd8InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCountryCd8(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmCtCulture || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cCultureTypeDesc8) { errors.cCultureTypeDesc8 = (columnLabel.CultureTypeDesc8 || {}).ErrMessage;}
if (!values.cCultureTypeName8) { errors.cCultureTypeName8 = (columnLabel.CultureTypeName8 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmCtCulture || {}).Mst || {};
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
        this.props.AdmCtCulture,
        {
          CultureTypeId8: values.cCultureTypeId8|| '',
          CultureTypeDesc8: values.cCultureTypeDesc8|| '',
          CultureTypeName8: values.cCultureTypeName8|| '',
          CurrencyCd8: values.cCurrencyCd8|| '',
          CountryCd8: (values.cCountryCd8|| {}).value || '',
          CultureDefault8: values.cCultureDefault8 ? 'Y' : 'N',
          EnNumberRule8: values.cEnNumberRule8 ? 'Y' : 'N',
          ToTranslate8: values.cToTranslate8 ? 'Y' : 'N',
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
    const AdmCtCultureState = this.props.AdmCtCulture || {};
    const auxSystemLabels = AdmCtCultureState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).CultureTypeId8;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmCtCulture.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmCtCulture.Label);
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
    const AdmCtCultureState = this.props.AdmCtCulture || {};
    const auxSystemLabels = AdmCtCultureState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.CultureTypeId8;
        this.props.DelMst(this.props.AdmCtCulture, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmCtCulture || {};
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
    const AdmCtCultureState = this.props.AdmCtCulture || {};
    const auxSystemLabels = AdmCtCultureState.SystemLabel || {};
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
      if (!(this.props.AdmCtCulture || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmCtCulture || {};

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
    const AdmCtCultureState = this.props.AdmCtCulture || {};

    if (AdmCtCultureState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmCtCultureState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmCtCultureReduxObj.GetScreenButtons(AdmCtCultureState) || {};
    const itemList = AdmCtCultureState.Dtl || [];
    const auxLabels = AdmCtCultureState.Label || {};
    const auxSystemLabels = AdmCtCultureState.SystemLabel || {};

    const columnLabel = AdmCtCultureState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmCtCultureState);
    const authRow = (AdmCtCultureState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmCtCulture || {}).Mst || {});
    const currDtl = ((this.props.AdmCtCulture || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.CultureTypeId8));
    const selectList = AdmCtCultureReduxObj.SearchListToSelectList(AdmCtCultureState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const CultureTypeId8 = currMst.CultureTypeId8;
const CultureTypeDesc8 = currMst.CultureTypeDesc8;
const CultureTypeName8 = currMst.CultureTypeName8;
const CurrencyCd8 = currMst.CurrencyCd8;
const CountryCd8List = AdmCtCultureReduxObj.ScreenDdlSelectors.CountryCd8(AdmCtCultureState);
const CountryCd8 = currMst.CountryCd8;
const CultureDefault8 = currMst.CultureDefault8;
const EnNumberRule8 = currMst.EnNumberRule8;
const ToTranslate8 = currMst.ToTranslate8;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmCtCultureState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cCultureTypeId8: CultureTypeId8 || '',
                  cCultureTypeDesc8: CultureTypeDesc8 || '',
                  cCultureTypeName8: CultureTypeName8 || '',
                  cCurrencyCd8: CurrencyCd8 || '',
                  cCountryCd8: CountryCd8List.filter(obj => { return obj.key === CountryCd8 })[0],
                  cCultureDefault8: CultureDefault8 === 'Y',
                  cEnNumberRule8: EnNumberRule8 === 'Y',
                  cToTranslate8: ToTranslate8 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmCtCultureState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CultureTypeId8)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CultureTypeId8)) return null;
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
            {(authCol.CultureTypeId8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureTypeId8 || {}).ColumnHeader} {(columnLabel.CultureTypeId8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureTypeId8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureTypeId8 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCultureTypeId8'
disabled = {(authCol.CultureTypeId8 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCultureTypeId8 && touched.cCultureTypeId8 && <span className='form__form-group-error'>{errors.cCultureTypeId8}</span>}
</div>
</Col>
}
{(authCol.CultureTypeDesc8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureTypeDesc8 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureTypeDesc8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureTypeDesc8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureTypeDesc8 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCultureTypeDesc8'
disabled = {(authCol.CultureTypeDesc8 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCultureTypeDesc8 && touched.cCultureTypeDesc8 && <span className='form__form-group-error'>{errors.cCultureTypeDesc8}</span>}
</div>
</Col>
}
{(authCol.CultureTypeName8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureTypeName8 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureTypeName8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureTypeName8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureTypeName8 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCultureTypeName8'
disabled = {(authCol.CultureTypeName8 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCultureTypeName8 && touched.cCultureTypeName8 && <span className='form__form-group-error'>{errors.cCultureTypeName8}</span>}
</div>
</Col>
}
{(authCol.CurrencyCd8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CurrencyCd8 || {}).ColumnHeader} {(columnLabel.CurrencyCd8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CurrencyCd8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CurrencyCd8 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCurrencyCd8'
disabled = {(authCol.CurrencyCd8 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCurrencyCd8 && touched.cCurrencyCd8 && <span className='form__form-group-error'>{errors.cCurrencyCd8}</span>}
</div>
</Col>
}
{(authCol.CountryCd8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CountryCd8 || {}).ColumnHeader} {(columnLabel.CountryCd8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CountryCd8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CountryCd8 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmCtCultureState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCountryCd8'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCountryCd8', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCountryCd8', true)}
onInputChange={this.CountryCd8InputChange()}
value={values.cCountryCd8}
defaultSelected={CountryCd8List.filter(obj => { return obj.key === CountryCd8 })}
options={CountryCd8List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CountryCd8 || {}).readonly ? true: false }/>
</div>
}
{errors.cCountryCd8 && touched.cCountryCd8 && <span className='form__form-group-error'>{errors.cCountryCd8}</span>}
</div>
</Col>
}
{(authCol.CultureDefault8 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cCultureDefault8'
onChange={handleChange}
defaultChecked={values.cCultureDefault8}
disabled={(authCol.CultureDefault8 || {}).readonly || !(authCol.CultureDefault8 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.CultureDefault8 || {}).ColumnHeader}</span>
</label>
{(columnLabel.CultureDefault8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureDefault8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureDefault8 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.EnNumberRule8 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cEnNumberRule8'
onChange={handleChange}
defaultChecked={values.cEnNumberRule8}
disabled={(authCol.EnNumberRule8 || {}).readonly || !(authCol.EnNumberRule8 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.EnNumberRule8 || {}).ColumnHeader}</span>
</label>
{(columnLabel.EnNumberRule8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EnNumberRule8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EnNumberRule8 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ToTranslate8 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cToTranslate8'
onChange={handleChange}
defaultChecked={values.cToTranslate8}
disabled={(authCol.ToTranslate8 || {}).readonly || !(authCol.ToTranslate8 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ToTranslate8 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ToTranslate8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ToTranslate8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ToTranslate8 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).CultureTypeId8)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).CultureTypeId8));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmCtCultureState) && <Skeleton height='43px' />) ||
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
  AdmCtCulture: state.AdmCtCulture,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmCtCultureReduxObj.LoadPage.bind(AdmCtCultureReduxObj) },
    { SavePage: AdmCtCultureReduxObj.SavePage.bind(AdmCtCultureReduxObj) },
    { DelMst: AdmCtCultureReduxObj.DelMst.bind(AdmCtCultureReduxObj) },
    { AddMst: AdmCtCultureReduxObj.AddMst.bind(AdmCtCultureReduxObj) },
//    { SearchMemberId64: AdmCtCultureReduxObj.SearchActions.SearchMemberId64.bind(AdmCtCultureReduxObj) },
//    { SearchCurrencyId64: AdmCtCultureReduxObj.SearchActions.SearchCurrencyId64.bind(AdmCtCultureReduxObj) },
//    { SearchCustomerJobId64: AdmCtCultureReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmCtCultureReduxObj) },
{ SearchCountryCd8: AdmCtCultureReduxObj.SearchActions.SearchCountryCd8.bind(AdmCtCultureReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            