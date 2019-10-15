
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
import AdmReportObjReduxObj, { ShowMstFilterApplied } from '../../redux/AdmReportObj';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmReportObj || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ReportObjId23';
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

ReportId23InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId23(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmReportObj || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cReportId23 || {}).value)) { errors.cReportId23 = (columnLabel.ReportId23 || {}).ErrMessage;}
if (!values.cColumnName23) { errors.cColumnName23 = (columnLabel.ColumnName23 || {}).ErrMessage;}
if (isEmptyId((values.cRptObjTypeCd23 || {}).value)) { errors.cRptObjTypeCd23 = (columnLabel.RptObjTypeCd23 || {}).ErrMessage;}
if (!values.cTabIndex23) { errors.cTabIndex23 = (columnLabel.TabIndex23 || {}).ErrMessage;}
if (isEmptyId((values.cDataTypeId23 || {}).value)) { errors.cDataTypeId23 = (columnLabel.DataTypeId23 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmReportObj || {}).Mst || {};
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
        this.props.AdmReportObj,
        {
          ReportObjId23: values.cReportObjId23|| '',
          ReportId23: (values.cReportId23|| {}).value || '',
          ColumnName23: values.cColumnName23|| '',
          RptObjTypeCd23: (values.cRptObjTypeCd23|| {}).value || '',
          TabIndex23: values.cTabIndex23|| '',
          ColumnFormat23: values.cColumnFormat23|| '',
          PaddSize23: values.cPaddSize23|| '',
          PaddChar23: values.cPaddChar23|| '',
          DataTypeId23: (values.cDataTypeId23|| {}).value || '',
          OperatorId23: (values.cOperatorId23|| {}).value || '',
          ReportCriId23: (values.cReportCriId23|| {}).value || '',
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
    const AdmReportObjState = this.props.AdmReportObj || {};
    const auxSystemLabels = AdmReportObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ReportObjId23;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmReportObj.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmReportObj.Label);
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
    const AdmReportObjState = this.props.AdmReportObj || {};
    const auxSystemLabels = AdmReportObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ReportObjId23;
        this.props.DelMst(this.props.AdmReportObj, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmReportObj || {};
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
    const AdmReportObjState = this.props.AdmReportObj || {};
    const auxSystemLabels = AdmReportObjState.SystemLabel || {};
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
      if (!(this.props.AdmReportObj || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmReportObj || {};

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
    const AdmReportObjState = this.props.AdmReportObj || {};

    if (AdmReportObjState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmReportObjState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmReportObjReduxObj.GetScreenButtons(AdmReportObjState) || {};
    const itemList = AdmReportObjState.Dtl || [];
    const auxLabels = AdmReportObjState.Label || {};
    const auxSystemLabels = AdmReportObjState.SystemLabel || {};

    const columnLabel = AdmReportObjState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmReportObjState);
    const authRow = (AdmReportObjState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmReportObj || {}).Mst || {});
    const currDtl = ((this.props.AdmReportObj || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ReportObjId23));
    const selectList = AdmReportObjReduxObj.SearchListToSelectList(AdmReportObjState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ReportObjId23 = currMst.ReportObjId23;
const ReportId23List = AdmReportObjReduxObj.ScreenDdlSelectors.ReportId23(AdmReportObjState);
const ReportId23 = currMst.ReportId23;
const ColumnName23 = currMst.ColumnName23;
const RptObjTypeCd23List = AdmReportObjReduxObj.ScreenDdlSelectors.RptObjTypeCd23(AdmReportObjState);
const RptObjTypeCd23 = currMst.RptObjTypeCd23;
const TabIndex23 = currMst.TabIndex23;
const ColumnFormat23 = currMst.ColumnFormat23;
const PaddSize23 = currMst.PaddSize23;
const PaddChar23 = currMst.PaddChar23;
const DataTypeId23List = AdmReportObjReduxObj.ScreenDdlSelectors.DataTypeId23(AdmReportObjState);
const DataTypeId23 = currMst.DataTypeId23;
const OperatorId23List = AdmReportObjReduxObj.ScreenDdlSelectors.OperatorId23(AdmReportObjState);
const OperatorId23 = currMst.OperatorId23;
const ReportCriId23List = AdmReportObjReduxObj.ScreenDdlSelectors.ReportCriId23(AdmReportObjState);
const ReportCriId23 = currMst.ReportCriId23;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmReportObjState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cReportObjId23: ReportObjId23 || '',
                  cReportId23: ReportId23List.filter(obj => { return obj.key === ReportId23 })[0],
                  cColumnName23: ColumnName23 || '',
                  cRptObjTypeCd23: RptObjTypeCd23List.filter(obj => { return obj.key === RptObjTypeCd23 })[0],
                  cTabIndex23: TabIndex23 || '',
                  cColumnFormat23: ColumnFormat23 || '',
                  cPaddSize23: PaddSize23 || '',
                  cPaddChar23: PaddChar23 || '',
                  cDataTypeId23: DataTypeId23List.filter(obj => { return obj.key === DataTypeId23 })[0],
                  cOperatorId23: OperatorId23List.filter(obj => { return obj.key === OperatorId23 })[0],
                  cReportCriId23: ReportCriId23List.filter(obj => { return obj.key === ReportCriId23 })[0],
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
                                {(this.constructor.ShowSpinner(AdmReportObjState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportObjId23)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportObjId23)) return null;
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
            {(authCol.ReportObjId23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportObjId23 || {}).ColumnHeader} {(columnLabel.ReportObjId23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportObjId23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportObjId23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReportObjId23'
disabled = {(authCol.ReportObjId23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportObjId23 && touched.cReportObjId23 && <span className='form__form-group-error'>{errors.cReportObjId23}</span>}
</div>
</Col>
}
{(authCol.ReportId23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId23 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportId23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId23'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId23', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId23', true)}
onInputChange={this.ReportId23InputChange()}
value={values.cReportId23}
defaultSelected={ReportId23List.filter(obj => { return obj.key === ReportId23 })}
options={ReportId23List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId23 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId23 && touched.cReportId23 && <span className='form__form-group-error'>{errors.cReportId23}</span>}
</div>
</Col>
}
{(authCol.ColumnName23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnName23 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnName23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnName23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnName23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnName23'
disabled = {(authCol.ColumnName23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnName23 && touched.cColumnName23 && <span className='form__form-group-error'>{errors.cColumnName23}</span>}
</div>
</Col>
}
{(authCol.RptObjTypeCd23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptObjTypeCd23 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptObjTypeCd23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptObjTypeCd23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptObjTypeCd23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRptObjTypeCd23'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRptObjTypeCd23')}
value={values.cRptObjTypeCd23}
options={RptObjTypeCd23List}
placeholder=''
disabled = {(authCol.RptObjTypeCd23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptObjTypeCd23 && touched.cRptObjTypeCd23 && <span className='form__form-group-error'>{errors.cRptObjTypeCd23}</span>}
</div>
</Col>
}
{(authCol.TabIndex23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TabIndex23 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TabIndex23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TabIndex23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TabIndex23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTabIndex23'
disabled = {(authCol.TabIndex23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTabIndex23 && touched.cTabIndex23 && <span className='form__form-group-error'>{errors.cTabIndex23}</span>}
</div>
</Col>
}
{(authCol.ColumnFormat23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnFormat23 || {}).ColumnHeader} {(columnLabel.ColumnFormat23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnFormat23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnFormat23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnFormat23'
disabled = {(authCol.ColumnFormat23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnFormat23 && touched.cColumnFormat23 && <span className='form__form-group-error'>{errors.cColumnFormat23}</span>}
</div>
</Col>
}
{(authCol.PaddSize23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PaddSize23 || {}).ColumnHeader} {(columnLabel.PaddSize23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PaddSize23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PaddSize23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPaddSize23'
disabled = {(authCol.PaddSize23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPaddSize23 && touched.cPaddSize23 && <span className='form__form-group-error'>{errors.cPaddSize23}</span>}
</div>
</Col>
}
{(authCol.PaddChar23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PaddChar23 || {}).ColumnHeader} {(columnLabel.PaddChar23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PaddChar23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PaddChar23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPaddChar23'
disabled = {(authCol.PaddChar23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPaddChar23 && touched.cPaddChar23 && <span className='form__form-group-error'>{errors.cPaddChar23}</span>}
</div>
</Col>
}
{(authCol.DataTypeId23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataTypeId23 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DataTypeId23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataTypeId23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataTypeId23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDataTypeId23'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDataTypeId23')}
value={values.cDataTypeId23}
options={DataTypeId23List}
placeholder=''
disabled = {(authCol.DataTypeId23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataTypeId23 && touched.cDataTypeId23 && <span className='form__form-group-error'>{errors.cDataTypeId23}</span>}
</div>
</Col>
}
{(authCol.OperatorId23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.OperatorId23 || {}).ColumnHeader} {(columnLabel.OperatorId23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OperatorId23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OperatorId23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cOperatorId23'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cOperatorId23')}
value={values.cOperatorId23}
options={OperatorId23List}
placeholder=''
disabled = {(authCol.OperatorId23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cOperatorId23 && touched.cOperatorId23 && <span className='form__form-group-error'>{errors.cOperatorId23}</span>}
</div>
</Col>
}
{(authCol.ReportCriId23 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportCriId23 || {}).ColumnHeader} {(columnLabel.ReportCriId23 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportCriId23 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportCriId23 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReportCriId23'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReportCriId23')}
value={values.cReportCriId23}
options={ReportCriId23List}
placeholder=''
disabled = {(authCol.ReportCriId23 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportCriId23 && touched.cReportCriId23 && <span className='form__form-group-error'>{errors.cReportCriId23}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportObjId23)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ReportObjId23));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmReportObjState) && <Skeleton height='43px' />) ||
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
  AdmReportObj: state.AdmReportObj,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmReportObjReduxObj.LoadPage.bind(AdmReportObjReduxObj) },
    { SavePage: AdmReportObjReduxObj.SavePage.bind(AdmReportObjReduxObj) },
    { DelMst: AdmReportObjReduxObj.DelMst.bind(AdmReportObjReduxObj) },
    { AddMst: AdmReportObjReduxObj.AddMst.bind(AdmReportObjReduxObj) },
//    { SearchMemberId64: AdmReportObjReduxObj.SearchActions.SearchMemberId64.bind(AdmReportObjReduxObj) },
//    { SearchCurrencyId64: AdmReportObjReduxObj.SearchActions.SearchCurrencyId64.bind(AdmReportObjReduxObj) },
//    { SearchCustomerJobId64: AdmReportObjReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmReportObjReduxObj) },
{ SearchReportId23: AdmReportObjReduxObj.SearchActions.SearchReportId23.bind(AdmReportObjReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            