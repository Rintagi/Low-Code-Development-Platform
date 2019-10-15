
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
import AdmRptChaReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRptCha';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRptCha || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RptChaId206';
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

RptCtrId206InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptCtrId206(v, filterBy);}}
ReportId206InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId206(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmRptCha || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cRptCtrId206 || {}).value)) { errors.cRptCtrId206 = (columnLabel.RptCtrId206 || {}).ErrMessage;}
if (isEmptyId((values.cReportId206 || {}).value)) { errors.cReportId206 = (columnLabel.ReportId206 || {}).ErrMessage;}
if (isEmptyId((values.cRptChaTypeCd206 || {}).value)) { errors.cRptChaTypeCd206 = (columnLabel.RptChaTypeCd206 || {}).ErrMessage;}
if (isEmptyId((values.cCategoryGrp206 || {}).value)) { errors.cCategoryGrp206 = (columnLabel.CategoryGrp206 || {}).ErrMessage;}
if (!values.cChartData206) { errors.cChartData206 = (columnLabel.ChartData206 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRptCha || {}).Mst || {};
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
        this.props.AdmRptCha,
        {
          RptChaId206: values.cRptChaId206|| '',
          RptCtrId206: (values.cRptCtrId206|| {}).value || '',
          ReportId206: (values.cReportId206|| {}).value || '',
          RptChaTypeCd206: (values.cRptChaTypeCd206|| {}).value || '',
          ThreeD206: values.cThreeD206 ? 'Y' : 'N',
          CategoryGrp206: (values.cCategoryGrp206|| {}).value || '',
          ChartData206: values.cChartData206|| '',
          SeriesGrp206: (values.cSeriesGrp206|| {}).value || '',
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
    const AdmRptChaState = this.props.AdmRptCha || {};
    const auxSystemLabels = AdmRptChaState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RptChaId206;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRptCha.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRptCha.Label);
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
    const AdmRptChaState = this.props.AdmRptCha || {};
    const auxSystemLabels = AdmRptChaState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RptChaId206;
        this.props.DelMst(this.props.AdmRptCha, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRptCha || {};
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
    const AdmRptChaState = this.props.AdmRptCha || {};
    const auxSystemLabels = AdmRptChaState.SystemLabel || {};
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
      if (!(this.props.AdmRptCha || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRptCha || {};

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
    const AdmRptChaState = this.props.AdmRptCha || {};

    if (AdmRptChaState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmRptChaState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmRptChaReduxObj.GetScreenButtons(AdmRptChaState) || {};
    const itemList = AdmRptChaState.Dtl || [];
    const auxLabels = AdmRptChaState.Label || {};
    const auxSystemLabels = AdmRptChaState.SystemLabel || {};

    const columnLabel = AdmRptChaState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmRptChaState);
    const authRow = (AdmRptChaState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRptCha || {}).Mst || {});
    const currDtl = ((this.props.AdmRptCha || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RptChaId206));
    const selectList = AdmRptChaReduxObj.SearchListToSelectList(AdmRptChaState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RptChaId206 = currMst.RptChaId206;
const RptCtrId206List = AdmRptChaReduxObj.ScreenDdlSelectors.RptCtrId206(AdmRptChaState);
const RptCtrId206 = currMst.RptCtrId206;
const ReportId206List = AdmRptChaReduxObj.ScreenDdlSelectors.ReportId206(AdmRptChaState);
const ReportId206 = currMst.ReportId206;
const RptChaTypeCd206List = AdmRptChaReduxObj.ScreenDdlSelectors.RptChaTypeCd206(AdmRptChaState);
const RptChaTypeCd206 = currMst.RptChaTypeCd206;
const ThreeD206 = currMst.ThreeD206;
const CategoryGrp206List = AdmRptChaReduxObj.ScreenDdlSelectors.CategoryGrp206(AdmRptChaState);
const CategoryGrp206 = currMst.CategoryGrp206;
const ChartData206 = currMst.ChartData206;
const SeriesGrp206List = AdmRptChaReduxObj.ScreenDdlSelectors.SeriesGrp206(AdmRptChaState);
const SeriesGrp206 = currMst.SeriesGrp206;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmRptChaState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRptChaId206: RptChaId206 || '',
                  cRptCtrId206: RptCtrId206List.filter(obj => { return obj.key === RptCtrId206 })[0],
                  cReportId206: ReportId206List.filter(obj => { return obj.key === ReportId206 })[0],
                  cRptChaTypeCd206: RptChaTypeCd206List.filter(obj => { return obj.key === RptChaTypeCd206 })[0],
                  cThreeD206: ThreeD206 === 'Y',
                  cCategoryGrp206: CategoryGrp206List.filter(obj => { return obj.key === CategoryGrp206 })[0],
                  cChartData206: ChartData206 || '',
                  cSeriesGrp206: SeriesGrp206List.filter(obj => { return obj.key === SeriesGrp206 })[0],
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
                                {(this.constructor.ShowSpinner(AdmRptChaState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptChaId206)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptChaId206)) return null;
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
            {(authCol.RptChaId206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptChaId206 || {}).ColumnHeader} {(columnLabel.RptChaId206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptChaId206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptChaId206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptChaId206'
disabled = {(authCol.RptChaId206 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptChaId206 && touched.cRptChaId206 && <span className='form__form-group-error'>{errors.cRptChaId206}</span>}
</div>
</Col>
}
{(authCol.RptCtrId206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCtrId206 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptCtrId206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCtrId206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCtrId206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptCtrId206'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCtrId206', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCtrId206', true)}
onInputChange={this.RptCtrId206InputChange()}
value={values.cRptCtrId206}
defaultSelected={RptCtrId206List.filter(obj => { return obj.key === RptCtrId206 })}
options={RptCtrId206List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptCtrId206 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptCtrId206 && touched.cRptCtrId206 && <span className='form__form-group-error'>{errors.cRptCtrId206}</span>}
</div>
</Col>
}
{(authCol.ReportId206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId206 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportId206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId206'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId206', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId206', true)}
onInputChange={this.ReportId206InputChange()}
value={values.cReportId206}
defaultSelected={ReportId206List.filter(obj => { return obj.key === ReportId206 })}
options={ReportId206List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId206 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId206 && touched.cReportId206 && <span className='form__form-group-error'>{errors.cReportId206}</span>}
</div>
</Col>
}
{(authCol.RptChaTypeCd206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptChaTypeCd206 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptChaTypeCd206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptChaTypeCd206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptChaTypeCd206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRptChaTypeCd206'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRptChaTypeCd206')}
value={values.cRptChaTypeCd206}
options={RptChaTypeCd206List}
placeholder=''
disabled = {(authCol.RptChaTypeCd206 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptChaTypeCd206 && touched.cRptChaTypeCd206 && <span className='form__form-group-error'>{errors.cRptChaTypeCd206}</span>}
</div>
</Col>
}
{(authCol.ThreeD206 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cThreeD206'
onChange={handleChange}
defaultChecked={values.cThreeD206}
disabled={(authCol.ThreeD206 || {}).readonly || !(authCol.ThreeD206 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ThreeD206 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ThreeD206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ThreeD206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ThreeD206 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.CategoryGrp206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CategoryGrp206 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CategoryGrp206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CategoryGrp206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CategoryGrp206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cCategoryGrp206'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cCategoryGrp206')}
value={values.cCategoryGrp206}
options={CategoryGrp206List}
placeholder=''
disabled = {(authCol.CategoryGrp206 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCategoryGrp206 && touched.cCategoryGrp206 && <span className='form__form-group-error'>{errors.cCategoryGrp206}</span>}
</div>
</Col>
}
{(authCol.ChartData206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ChartData206 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ChartData206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ChartData206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ChartData206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cChartData206'
disabled = {(authCol.ChartData206 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cChartData206 && touched.cChartData206 && <span className='form__form-group-error'>{errors.cChartData206}</span>}
</div>
</Col>
}
{(authCol.SeriesGrp206 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SeriesGrp206 || {}).ColumnHeader} {(columnLabel.SeriesGrp206 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SeriesGrp206 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SeriesGrp206 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptChaState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSeriesGrp206'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSeriesGrp206')}
value={values.cSeriesGrp206}
options={SeriesGrp206List}
placeholder=''
disabled = {(authCol.SeriesGrp206 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSeriesGrp206 && touched.cSeriesGrp206 && <span className='form__form-group-error'>{errors.cSeriesGrp206}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptChaId206)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RptChaId206));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmRptChaState) && <Skeleton height='43px' />) ||
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
  AdmRptCha: state.AdmRptCha,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmRptChaReduxObj.LoadPage.bind(AdmRptChaReduxObj) },
    { SavePage: AdmRptChaReduxObj.SavePage.bind(AdmRptChaReduxObj) },
    { DelMst: AdmRptChaReduxObj.DelMst.bind(AdmRptChaReduxObj) },
    { AddMst: AdmRptChaReduxObj.AddMst.bind(AdmRptChaReduxObj) },
//    { SearchMemberId64: AdmRptChaReduxObj.SearchActions.SearchMemberId64.bind(AdmRptChaReduxObj) },
//    { SearchCurrencyId64: AdmRptChaReduxObj.SearchActions.SearchCurrencyId64.bind(AdmRptChaReduxObj) },
//    { SearchCustomerJobId64: AdmRptChaReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmRptChaReduxObj) },
{ SearchRptCtrId206: AdmRptChaReduxObj.SearchActions.SearchRptCtrId206.bind(AdmRptChaReduxObj) },
{ SearchReportId206: AdmRptChaReduxObj.SearchActions.SearchReportId206.bind(AdmRptChaReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            