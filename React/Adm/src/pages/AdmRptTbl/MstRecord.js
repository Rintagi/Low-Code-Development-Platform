
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
import AdmRptTblReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRptTbl';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRptTbl || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RptTblId162';
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

RptCtrId162InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptCtrId162(v, filterBy);}}
ParentId162InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchParentId162(v, filterBy);}}
ReportId162InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId162(v, filterBy);}}
TblToggle162InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmRptTbl || {}).Mst || {}).ReportId162; _this.props.SearchTblToggle162(v, filterBy);}}
TblGrouping162InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmRptTbl || {}).Mst || {}).ReportId162; _this.props.SearchTblGrouping162(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmRptTbl || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cRptCtrId162 || {}).value)) { errors.cRptCtrId162 = (columnLabel.RptCtrId162 || {}).ErrMessage;}
if (isEmptyId((values.cReportId162 || {}).value)) { errors.cReportId162 = (columnLabel.ReportId162 || {}).ErrMessage;}
if (isEmptyId((values.cRptTblTypeCd162 || {}).value)) { errors.cRptTblTypeCd162 = (columnLabel.RptTblTypeCd162 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRptTbl || {}).Mst || {};
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
        this.props.AdmRptTbl,
        {
          RptTblId162: values.cRptTblId162|| '',
          RptCtrId162: (values.cRptCtrId162|| {}).value || '',
          ParentId162: (values.cParentId162|| {}).value || '',
          ReportId162: (values.cReportId162|| {}).value || '',
          TblToggle162: (values.cTblToggle162|| {}).value || '',
          TblGrouping162: (values.cTblGrouping162|| {}).value || '',
          RptTblTypeCd162: (values.cRptTblTypeCd162|| {}).value || '',
          TblVisibility162: (values.cTblVisibility162|| {}).value || '',
          TblRepeatNew162: values.cTblRepeatNew162 ? 'Y' : 'N',
          TblOrder162: values.cTblOrder162|| '',
          ColWidth162: values.cColWidth162|| '',
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
    const AdmRptTblState = this.props.AdmRptTbl || {};
    const auxSystemLabels = AdmRptTblState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RptTblId162;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRptTbl.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRptTbl.Label);
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
    const AdmRptTblState = this.props.AdmRptTbl || {};
    const auxSystemLabels = AdmRptTblState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RptTblId162;
        this.props.DelMst(this.props.AdmRptTbl, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRptTbl || {};
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
    const AdmRptTblState = this.props.AdmRptTbl || {};
    const auxSystemLabels = AdmRptTblState.SystemLabel || {};
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
      if (!(this.props.AdmRptTbl || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRptTbl || {};

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
    const AdmRptTblState = this.props.AdmRptTbl || {};

    if (AdmRptTblState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmRptTblState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmRptTblReduxObj.GetScreenButtons(AdmRptTblState) || {};
    const itemList = AdmRptTblState.Dtl || [];
    const auxLabels = AdmRptTblState.Label || {};
    const auxSystemLabels = AdmRptTblState.SystemLabel || {};

    const columnLabel = AdmRptTblState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmRptTblState);
    const authRow = (AdmRptTblState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRptTbl || {}).Mst || {});
    const currDtl = ((this.props.AdmRptTbl || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RptTblId162));
    const selectList = AdmRptTblReduxObj.SearchListToSelectList(AdmRptTblState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RptTblId162 = currMst.RptTblId162;
const RptCtrId162List = AdmRptTblReduxObj.ScreenDdlSelectors.RptCtrId162(AdmRptTblState);
const RptCtrId162 = currMst.RptCtrId162;
const ParentId162List = AdmRptTblReduxObj.ScreenDdlSelectors.ParentId162(AdmRptTblState);
const ParentId162 = currMst.ParentId162;
const ReportId162List = AdmRptTblReduxObj.ScreenDdlSelectors.ReportId162(AdmRptTblState);
const ReportId162 = currMst.ReportId162;
const TblToggle162List = AdmRptTblReduxObj.ScreenDdlSelectors.TblToggle162(AdmRptTblState);
const TblToggle162 = currMst.TblToggle162;
const TblGrouping162List = AdmRptTblReduxObj.ScreenDdlSelectors.TblGrouping162(AdmRptTblState);
const TblGrouping162 = currMst.TblGrouping162;
const RptTblTypeCd162List = AdmRptTblReduxObj.ScreenDdlSelectors.RptTblTypeCd162(AdmRptTblState);
const RptTblTypeCd162 = currMst.RptTblTypeCd162;
const TblVisibility162List = AdmRptTblReduxObj.ScreenDdlSelectors.TblVisibility162(AdmRptTblState);
const TblVisibility162 = currMst.TblVisibility162;
const TblRepeatNew162 = currMst.TblRepeatNew162;
const TblOrder162 = currMst.TblOrder162;
const ColWidth162 = currMst.ColWidth162;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmRptTblState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRptTblId162: RptTblId162 || '',
                  cRptCtrId162: RptCtrId162List.filter(obj => { return obj.key === RptCtrId162 })[0],
                  cParentId162: ParentId162List.filter(obj => { return obj.key === ParentId162 })[0],
                  cReportId162: ReportId162List.filter(obj => { return obj.key === ReportId162 })[0],
                  cTblToggle162: TblToggle162List.filter(obj => { return obj.key === TblToggle162 })[0],
                  cTblGrouping162: TblGrouping162List.filter(obj => { return obj.key === TblGrouping162 })[0],
                  cRptTblTypeCd162: RptTblTypeCd162List.filter(obj => { return obj.key === RptTblTypeCd162 })[0],
                  cTblVisibility162: TblVisibility162List.filter(obj => { return obj.key === TblVisibility162 })[0],
                  cTblRepeatNew162: TblRepeatNew162 === 'Y',
                  cTblOrder162: TblOrder162 || '',
                  cColWidth162: ColWidth162 || '',
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
                                {(this.constructor.ShowSpinner(AdmRptTblState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptTblId162)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptTblId162)) return null;
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
            {(authCol.RptTblId162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptTblId162 || {}).ColumnHeader} {(columnLabel.RptTblId162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptTblId162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptTblId162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptTblId162'
disabled = {(authCol.RptTblId162 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptTblId162 && touched.cRptTblId162 && <span className='form__form-group-error'>{errors.cRptTblId162}</span>}
</div>
</Col>
}
{(authCol.RptCtrId162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptCtrId162 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptCtrId162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptCtrId162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptCtrId162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptCtrId162'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCtrId162', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptCtrId162', true)}
onInputChange={this.RptCtrId162InputChange()}
value={values.cRptCtrId162}
defaultSelected={RptCtrId162List.filter(obj => { return obj.key === RptCtrId162 })}
options={RptCtrId162List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptCtrId162 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptCtrId162 && touched.cRptCtrId162 && <span className='form__form-group-error'>{errors.cRptCtrId162}</span>}
</div>
</Col>
}
{(authCol.ParentId162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ParentId162 || {}).ColumnHeader} {(columnLabel.ParentId162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ParentId162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ParentId162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cParentId162'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cParentId162', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cParentId162', true)}
onInputChange={this.ParentId162InputChange()}
value={values.cParentId162}
defaultSelected={ParentId162List.filter(obj => { return obj.key === ParentId162 })}
options={ParentId162List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ParentId162 || {}).readonly ? true: false }/>
</div>
}
{errors.cParentId162 && touched.cParentId162 && <span className='form__form-group-error'>{errors.cParentId162}</span>}
</div>
</Col>
}
{(authCol.ReportId162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId162 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportId162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId162'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId162', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId162', true)}
onInputChange={this.ReportId162InputChange()}
value={values.cReportId162}
defaultSelected={ReportId162List.filter(obj => { return obj.key === ReportId162 })}
options={ReportId162List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId162 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId162 && touched.cReportId162 && <span className='form__form-group-error'>{errors.cReportId162}</span>}
</div>
</Col>
}
{(authCol.TblToggle162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TblToggle162 || {}).ColumnHeader} {(columnLabel.TblToggle162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblToggle162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblToggle162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cTblToggle162'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cTblToggle162', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cTblToggle162', true)}
onInputChange={this.TblToggle162InputChange()}
value={values.cTblToggle162}
defaultSelected={TblToggle162List.filter(obj => { return obj.key === TblToggle162 })}
options={TblToggle162List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.TblToggle162 || {}).readonly ? true: false }/>
</div>
}
{errors.cTblToggle162 && touched.cTblToggle162 && <span className='form__form-group-error'>{errors.cTblToggle162}</span>}
</div>
</Col>
}
{(authCol.TblGrouping162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TblGrouping162 || {}).ColumnHeader} {(columnLabel.TblGrouping162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblGrouping162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblGrouping162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cTblGrouping162'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cTblGrouping162', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cTblGrouping162', true)}
onInputChange={this.TblGrouping162InputChange()}
value={values.cTblGrouping162}
defaultSelected={TblGrouping162List.filter(obj => { return obj.key === TblGrouping162 })}
options={TblGrouping162List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.TblGrouping162 || {}).readonly ? true: false }/>
</div>
}
{errors.cTblGrouping162 && touched.cTblGrouping162 && <span className='form__form-group-error'>{errors.cTblGrouping162}</span>}
</div>
</Col>
}
{(authCol.RptTblTypeCd162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptTblTypeCd162 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptTblTypeCd162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptTblTypeCd162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptTblTypeCd162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cRptTblTypeCd162'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cRptTblTypeCd162')}
value={values.cRptTblTypeCd162}
options={RptTblTypeCd162List}
placeholder=''
disabled = {(authCol.RptTblTypeCd162 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptTblTypeCd162 && touched.cRptTblTypeCd162 && <span className='form__form-group-error'>{errors.cRptTblTypeCd162}</span>}
</div>
</Col>
}
{(authCol.TblVisibility162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TblVisibility162 || {}).ColumnHeader} {(columnLabel.TblVisibility162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblVisibility162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblVisibility162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cTblVisibility162'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cTblVisibility162')}
value={values.cTblVisibility162}
options={TblVisibility162List}
placeholder=''
disabled = {(authCol.TblVisibility162 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTblVisibility162 && touched.cTblVisibility162 && <span className='form__form-group-error'>{errors.cTblVisibility162}</span>}
</div>
</Col>
}
{(authCol.TblRepeatNew162 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cTblRepeatNew162'
onChange={handleChange}
defaultChecked={values.cTblRepeatNew162}
disabled={(authCol.TblRepeatNew162 || {}).readonly || !(authCol.TblRepeatNew162 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.TblRepeatNew162 || {}).ColumnHeader}</span>
</label>
{(columnLabel.TblRepeatNew162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblRepeatNew162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblRepeatNew162 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.TblOrder162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TblOrder162 || {}).ColumnHeader} {(columnLabel.TblOrder162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblOrder162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblOrder162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTblOrder162'
disabled = {(authCol.TblOrder162 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTblOrder162 && touched.cTblOrder162 && <span className='form__form-group-error'>{errors.cTblOrder162}</span>}
</div>
</Col>
}
{(authCol.ColWidth162 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColWidth162 || {}).ColumnHeader} {(columnLabel.ColWidth162 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColWidth162 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColWidth162 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptTblState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColWidth162'
disabled = {(authCol.ColWidth162 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColWidth162 && touched.cColWidth162 && <span className='form__form-group-error'>{errors.cColWidth162}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptTblId162)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RptTblId162));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmRptTblState) && <Skeleton height='43px' />) ||
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
  AdmRptTbl: state.AdmRptTbl,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmRptTblReduxObj.LoadPage.bind(AdmRptTblReduxObj) },
    { SavePage: AdmRptTblReduxObj.SavePage.bind(AdmRptTblReduxObj) },
    { DelMst: AdmRptTblReduxObj.DelMst.bind(AdmRptTblReduxObj) },
    { AddMst: AdmRptTblReduxObj.AddMst.bind(AdmRptTblReduxObj) },
//    { SearchMemberId64: AdmRptTblReduxObj.SearchActions.SearchMemberId64.bind(AdmRptTblReduxObj) },
//    { SearchCurrencyId64: AdmRptTblReduxObj.SearchActions.SearchCurrencyId64.bind(AdmRptTblReduxObj) },
//    { SearchCustomerJobId64: AdmRptTblReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmRptTblReduxObj) },
{ SearchRptCtrId162: AdmRptTblReduxObj.SearchActions.SearchRptCtrId162.bind(AdmRptTblReduxObj) },
{ SearchParentId162: AdmRptTblReduxObj.SearchActions.SearchParentId162.bind(AdmRptTblReduxObj) },
{ SearchReportId162: AdmRptTblReduxObj.SearchActions.SearchReportId162.bind(AdmRptTblReduxObj) },
{ SearchTblToggle162: AdmRptTblReduxObj.SearchActions.SearchTblToggle162.bind(AdmRptTblReduxObj) },
{ SearchTblGrouping162: AdmRptTblReduxObj.SearchActions.SearchTblGrouping162.bind(AdmRptTblReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            