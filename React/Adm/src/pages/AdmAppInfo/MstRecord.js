
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
import AdmAppInfoReduxObj, { ShowMstFilterApplied } from '../../redux/AdmAppInfo';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmAppInfo || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'AppInfoId135';
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

CultureTypeName135InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureTypeName135(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmAppInfo || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cVersionMa135) { errors.cVersionMa135 = (columnLabel.VersionMa135 || {}).ErrMessage;}
if (!values.cVersionMi135) { errors.cVersionMi135 = (columnLabel.VersionMi135 || {}).ErrMessage;}
if (isEmptyId((values.cAppItemLink135 || {}).value)) { errors.cAppItemLink135 = (columnLabel.AppItemLink135 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmAppInfo || {}).Mst || {};
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
        this.props.AdmAppInfo,
        {
          AppInfoId135: values.cAppInfoId135|| '',
          VersionMa135: values.cVersionMa135|| '',
          VersionMi135: values.cVersionMi135|| '',
          VersionDt135: values.cVersionDt135|| '',
          CultureTypeName135: (values.cCultureTypeName135|| {}).value || '',
          VersionValue135: values.cVersionValue135|| '',
          AppZipId135: values.cAppZipId135|| '',
          AppItemLink135: (values.cAppItemLink135|| {}).value || '',
          Prerequisite135: values.cPrerequisite135|| '',
          Readme135: values.cReadme135|| '',
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
    const AdmAppInfoState = this.props.AdmAppInfo || {};
    const auxSystemLabels = AdmAppInfoState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).AppInfoId135;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmAppInfo.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmAppInfo.Label);
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
    const AdmAppInfoState = this.props.AdmAppInfo || {};
    const auxSystemLabels = AdmAppInfoState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.AppInfoId135;
        this.props.DelMst(this.props.AdmAppInfo, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmAppInfo || {};
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
    const AdmAppInfoState = this.props.AdmAppInfo || {};
    const auxSystemLabels = AdmAppInfoState.SystemLabel || {};
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
      if (!(this.props.AdmAppInfo || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmAppInfo || {};

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
    const AdmAppInfoState = this.props.AdmAppInfo || {};

    if (AdmAppInfoState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmAppInfoState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmAppInfoReduxObj.GetScreenButtons(AdmAppInfoState) || {};
    const itemList = AdmAppInfoState.Dtl || [];
    const auxLabels = AdmAppInfoState.Label || {};
    const auxSystemLabels = AdmAppInfoState.SystemLabel || {};

    const columnLabel = AdmAppInfoState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmAppInfoState);
    const authRow = (AdmAppInfoState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmAppInfo || {}).Mst || {});
    const currDtl = ((this.props.AdmAppInfo || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.AppInfoId135));
    const selectList = AdmAppInfoReduxObj.SearchListToSelectList(AdmAppInfoState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const AppInfoId135 = currMst.AppInfoId135;
const VersionMa135 = currMst.VersionMa135;
const VersionMi135 = currMst.VersionMi135;
const VersionDt135 = currMst.VersionDt135;
const CultureTypeName135List = AdmAppInfoReduxObj.ScreenDdlSelectors.CultureTypeName135(AdmAppInfoState);
const CultureTypeName135 = currMst.CultureTypeName135;
const VersionValue135 = currMst.VersionValue135;
const AppZipId135 = currMst.AppZipId135;
const AppItemLink135List = AdmAppInfoReduxObj.ScreenDdlSelectors.AppItemLink135(AdmAppInfoState);
const AppItemLink135 = currMst.AppItemLink135;
const Prerequisite135 = currMst.Prerequisite135;
const Readme135 = currMst.Readme135;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmAppInfoState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cAppInfoId135: AppInfoId135 || '',
                  cVersionMa135: VersionMa135 || '',
                  cVersionMi135: VersionMi135 || '',
                  cVersionDt135: VersionDt135 || new Date(),
                  cCultureTypeName135: CultureTypeName135List.filter(obj => { return obj.key === CultureTypeName135 })[0],
                  cVersionValue135: VersionValue135 || '',
                  cAppZipId135: AppZipId135 || '',
                  cAppItemLink135: AppItemLink135List.filter(obj => { return obj.key === AppItemLink135 })[0],
                  cPrerequisite135: Prerequisite135 || '',
                  cReadme135: Readme135 || '',
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
                                {(this.constructor.ShowSpinner(AdmAppInfoState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppInfoId135)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppInfoId135)) return null;
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
            {(authCol.AppInfoId135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppInfoId135 || {}).ColumnHeader} {(columnLabel.AppInfoId135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppInfoId135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppInfoId135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAppInfoId135'
disabled = {(authCol.AppInfoId135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppInfoId135 && touched.cAppInfoId135 && <span className='form__form-group-error'>{errors.cAppInfoId135}</span>}
</div>
</Col>
}
{(authCol.VersionMa135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VersionMa135 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.VersionMa135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VersionMa135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VersionMa135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cVersionMa135'
disabled = {(authCol.VersionMa135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cVersionMa135 && touched.cVersionMa135 && <span className='form__form-group-error'>{errors.cVersionMa135}</span>}
</div>
</Col>
}
{(authCol.VersionMi135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VersionMi135 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.VersionMi135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VersionMi135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VersionMi135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cVersionMi135'
disabled = {(authCol.VersionMi135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cVersionMi135 && touched.cVersionMi135 && <span className='form__form-group-error'>{errors.cVersionMi135}</span>}
</div>
</Col>
}
{(authCol.VersionDt135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VersionDt135 || {}).ColumnHeader} {(columnLabel.VersionDt135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VersionDt135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VersionDt135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cVersionDt135'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cVersionDt135', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cVersionDt135', true)}
value={values.cVersionDt135}
selected={values.cVersionDt135}
disabled = {(authCol.VersionDt135 || {}).readonly ? true: false }/>
</div>
}
{errors.cVersionDt135 && touched.cVersionDt135 && <span className='form__form-group-error'>{errors.cVersionDt135}</span>}
</div>
</Col>
}
{(authCol.LunarDt || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LunarDt || {}).ColumnHeader} {(columnLabel.LunarDt || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LunarDt || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LunarDt || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.CultureTypeName135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureTypeName135 || {}).ColumnHeader} {(columnLabel.CultureTypeName135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureTypeName135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureTypeName135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureTypeName135'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureTypeName135', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureTypeName135', true)}
onInputChange={this.CultureTypeName135InputChange()}
value={values.cCultureTypeName135}
defaultSelected={CultureTypeName135List.filter(obj => { return obj.key === CultureTypeName135 })}
options={CultureTypeName135List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureTypeName135 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureTypeName135 && touched.cCultureTypeName135 && <span className='form__form-group-error'>{errors.cCultureTypeName135}</span>}
</div>
</Col>
}
{(authCol.VersionValue135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VersionValue135 || {}).ColumnHeader} {(columnLabel.VersionValue135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VersionValue135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VersionValue135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cVersionValue135'
disabled = {(authCol.VersionValue135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cVersionValue135 && touched.cVersionValue135 && <span className='form__form-group-error'>{errors.cVersionValue135}</span>}
</div>
</Col>
}
{(authCol.AppZipId135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppZipId135 || {}).ColumnHeader} {(columnLabel.AppZipId135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppZipId135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppZipId135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAppZipId135'
disabled = {(authCol.AppZipId135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppZipId135 && touched.cAppZipId135 && <span className='form__form-group-error'>{errors.cAppZipId135}</span>}
</div>
</Col>
}
{(authCol.AppItemLink135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppItemLink135 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AppItemLink135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppItemLink135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppItemLink135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cAppItemLink135'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cAppItemLink135')}
value={values.cAppItemLink135}
options={AppItemLink135List}
placeholder=''
disabled = {(authCol.AppItemLink135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppItemLink135 && touched.cAppItemLink135 && <span className='form__form-group-error'>{errors.cAppItemLink135}</span>}
</div>
</Col>
}
{(authCol.Prerequisite135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Prerequisite135 || {}).ColumnHeader} {(columnLabel.Prerequisite135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Prerequisite135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Prerequisite135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPrerequisite135'
disabled = {(authCol.Prerequisite135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPrerequisite135 && touched.cPrerequisite135 && <span className='form__form-group-error'>{errors.cPrerequisite135}</span>}
</div>
</Col>
}
{(authCol.Readme135 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Readme135 || {}).ColumnHeader} {(columnLabel.Readme135 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Readme135 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Readme135 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppInfoState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReadme135'
disabled = {(authCol.Readme135 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReadme135 && touched.cReadme135 && <span className='form__form-group-error'>{errors.cReadme135}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppInfoId135)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).AppInfoId135));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmAppInfoState) && <Skeleton height='43px' />) ||
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
  AdmAppInfo: state.AdmAppInfo,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmAppInfoReduxObj.LoadPage.bind(AdmAppInfoReduxObj) },
    { SavePage: AdmAppInfoReduxObj.SavePage.bind(AdmAppInfoReduxObj) },
    { DelMst: AdmAppInfoReduxObj.DelMst.bind(AdmAppInfoReduxObj) },
    { AddMst: AdmAppInfoReduxObj.AddMst.bind(AdmAppInfoReduxObj) },
//    { SearchMemberId64: AdmAppInfoReduxObj.SearchActions.SearchMemberId64.bind(AdmAppInfoReduxObj) },
//    { SearchCurrencyId64: AdmAppInfoReduxObj.SearchActions.SearchCurrencyId64.bind(AdmAppInfoReduxObj) },
//    { SearchCustomerJobId64: AdmAppInfoReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmAppInfoReduxObj) },
{ SearchCultureTypeName135: AdmAppInfoReduxObj.SearchActions.SearchCultureTypeName135.bind(AdmAppInfoReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            