
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
import AdmReportReduxObj, { ShowMstFilterApplied } from '../../redux/AdmReport';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmReport || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ReportId22';
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

CopyReportId22InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCopyReportId22(v, filterBy);}}
 SyncByDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncByDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 SyncToDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncToDb';
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
    const columnLabel = (this.props.AdmReport || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cProgramName22) { errors.cProgramName22 = (columnLabel.ProgramName22 || {}).ErrMessage;}
if (isEmptyId((values.cReportTypeCd22 || {}).value)) { errors.cReportTypeCd22 = (columnLabel.ReportTypeCd22 || {}).ErrMessage;}
if (isEmptyId((values.cOrientationCd22 || {}).value)) { errors.cOrientationCd22 = (columnLabel.OrientationCd22 || {}).ErrMessage;}
if (isEmptyId((values.cUnitCd22 || {}).value)) { errors.cUnitCd22 = (columnLabel.UnitCd22 || {}).ErrMessage;}
if (!values.cTopMargin22) { errors.cTopMargin22 = (columnLabel.TopMargin22 || {}).ErrMessage;}
if (!values.cBottomMargin22) { errors.cBottomMargin22 = (columnLabel.BottomMargin22 || {}).ErrMessage;}
if (!values.cLeftMargin22) { errors.cLeftMargin22 = (columnLabel.LeftMargin22 || {}).ErrMessage;}
if (!values.cRightMargin22) { errors.cRightMargin22 = (columnLabel.RightMargin22 || {}).ErrMessage;}
if (!values.cPageWidth22) { errors.cPageWidth22 = (columnLabel.PageWidth22 || {}).ErrMessage;}
if (!values.cPageHeight22) { errors.cPageHeight22 = (columnLabel.PageHeight22 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmReport || {}).Mst || {};
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
        this.props.AdmReport,
        {
          ReportId22: values.cReportId22|| '',
          ProgramName22: values.cProgramName22|| '',
          ReportTypeCd22: (values.cReportTypeCd22|| {}).value || '',
          OrientationCd22: (values.cOrientationCd22|| {}).value || '',
          CopyReportId22: (values.cCopyReportId22|| {}).value || '',
          ModifiedBy22: (values.cModifiedBy22|| {}).value || '',
          ModifiedOn22: values.cModifiedOn22|| '',
          TemplateName22: values.cTemplateName22|| '',
          RptTemplate22: values.cRptTemplate22|| '',
          UnitCd22: (values.cUnitCd22|| {}).value || '',
          TopMargin22: values.cTopMargin22|| '',
          BottomMargin22: values.cBottomMargin22|| '',
          LeftMargin22: values.cLeftMargin22|| '',
          RightMargin22: values.cRightMargin22|| '',
          PageWidth22: values.cPageWidth22|| '',
          PageHeight22: values.cPageHeight22|| '',
          AllowSelect22: values.cAllowSelect22 ? 'Y' : 'N',
          GenerateRp22: values.cGenerateRp22 ? 'Y' : 'N',
          LastGenDt22: values.cLastGenDt22|| '',
          AuthRequired22: values.cAuthRequired22 ? 'Y' : 'N',
          WhereClause22: values.cWhereClause22|| '',
          RegClause22: values.cRegClause22|| '',
          RegCode22: values.cRegCode22|| '',
          ValClause22: values.cValClause22|| '',
          ValCode22: values.cValCode22|| '',
          UpdClause22: values.cUpdClause22|| '',
          UpdCode22: values.cUpdCode22|| '',
          XlsClause22: values.cXlsClause22|| '',
          XlsCode22: values.cXlsCode22|| '',
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
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ReportId22;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmReport.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmReport.Label);
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
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ReportId22;
        this.props.DelMst(this.props.AdmReport, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmReport || {};
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
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
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
      if (!(this.props.AdmReport || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmReport || {};

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
    const AdmReportState = this.props.AdmReport || {};

    if (AdmReportState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmReportState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmReportReduxObj.GetScreenButtons(AdmReportState) || {};
    const itemList = AdmReportState.Dtl || [];
    const auxLabels = AdmReportState.Label || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};

    const columnLabel = AdmReportState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmReportState);
    const authRow = (AdmReportState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmReport || {}).Mst || {});
    const currDtl = ((this.props.AdmReport || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ReportId22));
    const selectList = AdmReportReduxObj.SearchListToSelectList(AdmReportState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ReportId22 = currMst.ReportId22;
const ProgramName22 = currMst.ProgramName22;
const ReportTypeCd22List = AdmReportReduxObj.ScreenDdlSelectors.ReportTypeCd22(AdmReportState);
const ReportTypeCd22 = currMst.ReportTypeCd22;
const OrientationCd22List = AdmReportReduxObj.ScreenDdlSelectors.OrientationCd22(AdmReportState);
const OrientationCd22 = currMst.OrientationCd22;
const CopyReportId22List = AdmReportReduxObj.ScreenDdlSelectors.CopyReportId22(AdmReportState);
const CopyReportId22 = currMst.CopyReportId22;
const ModifiedBy22List = AdmReportReduxObj.ScreenDdlSelectors.ModifiedBy22(AdmReportState);
const ModifiedBy22 = currMst.ModifiedBy22;
const ModifiedOn22 = currMst.ModifiedOn22;
const TemplateName22 = currMst.TemplateName22;
const RptTemplate22 = currMst.RptTemplate22;
const UnitCd22List = AdmReportReduxObj.ScreenDdlSelectors.UnitCd22(AdmReportState);
const UnitCd22 = currMst.UnitCd22;
const TopMargin22 = currMst.TopMargin22;
const BottomMargin22 = currMst.BottomMargin22;
const LeftMargin22 = currMst.LeftMargin22;
const RightMargin22 = currMst.RightMargin22;
const PageWidth22 = currMst.PageWidth22;
const PageHeight22 = currMst.PageHeight22;
const AllowSelect22 = currMst.AllowSelect22;
const GenerateRp22 = currMst.GenerateRp22;
const LastGenDt22 = currMst.LastGenDt22;
const AuthRequired22 = currMst.AuthRequired22;
const WhereClause22 = currMst.WhereClause22;
const RegClause22 = currMst.RegClause22;
const RegCode22 = currMst.RegCode22;
const ValClause22 = currMst.ValClause22;
const ValCode22 = currMst.ValCode22;
const UpdClause22 = currMst.UpdClause22;
const UpdCode22 = currMst.UpdCode22;
const XlsClause22 = currMst.XlsClause22;
const XlsCode22 = currMst.XlsCode22;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmReportState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cReportId22: ReportId22 || '',
                  cProgramName22: ProgramName22 || '',
                  cReportTypeCd22: ReportTypeCd22List.filter(obj => { return obj.key === ReportTypeCd22 })[0],
                  cOrientationCd22: OrientationCd22List.filter(obj => { return obj.key === OrientationCd22 })[0],
                  cCopyReportId22: CopyReportId22List.filter(obj => { return obj.key === CopyReportId22 })[0],
                  cModifiedBy22: ModifiedBy22List.filter(obj => { return obj.key === ModifiedBy22 })[0],
                  cModifiedOn22: ModifiedOn22 || new Date(),
                  cTemplateName22: TemplateName22 || '',
                  cRptTemplate22: RptTemplate22 || '',
                  cUnitCd22: UnitCd22List.filter(obj => { return obj.key === UnitCd22 })[0],
                  cTopMargin22: TopMargin22 || '',
                  cBottomMargin22: BottomMargin22 || '',
                  cLeftMargin22: LeftMargin22 || '',
                  cRightMargin22: RightMargin22 || '',
                  cPageWidth22: PageWidth22 || '',
                  cPageHeight22: PageHeight22 || '',
                  cAllowSelect22: AllowSelect22 === 'Y',
                  cGenerateRp22: GenerateRp22 === 'Y',
                  cLastGenDt22: LastGenDt22 || '',
                  cAuthRequired22: AuthRequired22 === 'Y',
                  cWhereClause22: WhereClause22 || '',
                  cRegClause22: RegClause22 || '',
                  cRegCode22: RegCode22 || '',
                  cValClause22: ValClause22 || '',
                  cValCode22: ValCode22 || '',
                  cUpdClause22: UpdClause22 || '',
                  cUpdCode22: UpdCode22 || '',
                  cXlsClause22: XlsClause22 || '',
                  cXlsCode22: XlsCode22 || '',
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
                                {(this.constructor.ShowSpinner(AdmReportState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22)) return null;
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
            {(authCol.ReportId22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId22 || {}).ColumnHeader} {(columnLabel.ReportId22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReportId22'
disabled = {(authCol.ReportId22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportId22 && touched.cReportId22 && <span className='form__form-group-error'>{errors.cReportId22}</span>}
</div>
</Col>
}
{(authCol.ProgramName22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProgramName22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ProgramName22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProgramName22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProgramName22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cProgramName22'
disabled = {(authCol.ProgramName22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProgramName22 && touched.cProgramName22 && <span className='form__form-group-error'>{errors.cProgramName22}</span>}
</div>
</Col>
}
{(authCol.ReportTypeCd22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportTypeCd22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportTypeCd22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportTypeCd22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportTypeCd22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReportTypeCd22'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReportTypeCd22')}
value={values.cReportTypeCd22}
options={ReportTypeCd22List}
placeholder=''
disabled = {(authCol.ReportTypeCd22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportTypeCd22 && touched.cReportTypeCd22 && <span className='form__form-group-error'>{errors.cReportTypeCd22}</span>}
</div>
</Col>
}
{(authCol.OrientationCd22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.OrientationCd22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.OrientationCd22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OrientationCd22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OrientationCd22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cOrientationCd22'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cOrientationCd22')}
value={values.cOrientationCd22}
options={OrientationCd22List}
placeholder=''
disabled = {(authCol.OrientationCd22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cOrientationCd22 && touched.cOrientationCd22 && <span className='form__form-group-error'>{errors.cOrientationCd22}</span>}
</div>
</Col>
}
{(authCol.CopyReportId22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CopyReportId22 || {}).ColumnHeader} {(columnLabel.CopyReportId22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CopyReportId22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CopyReportId22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCopyReportId22'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCopyReportId22', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCopyReportId22', true)}
onInputChange={this.CopyReportId22InputChange()}
value={values.cCopyReportId22}
defaultSelected={CopyReportId22List.filter(obj => { return obj.key === CopyReportId22 })}
options={CopyReportId22List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CopyReportId22 || {}).readonly ? true: false }/>
</div>
}
{errors.cCopyReportId22 && touched.cCopyReportId22 && <span className='form__form-group-error'>{errors.cCopyReportId22}</span>}
</div>
</Col>
}
{(authCol.ModifiedBy22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedBy22 || {}).ColumnHeader} {(columnLabel.ModifiedBy22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedBy22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedBy22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cModifiedBy22'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cModifiedBy22')}
value={values.cModifiedBy22}
options={ModifiedBy22List}
placeholder=''
disabled = {(authCol.ModifiedBy22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cModifiedBy22 && touched.cModifiedBy22 && <span className='form__form-group-error'>{errors.cModifiedBy22}</span>}
</div>
</Col>
}
{(authCol.ModifiedOn22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedOn22 || {}).ColumnHeader} {(columnLabel.ModifiedOn22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedOn22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedOn22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cModifiedOn22'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn22', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn22', true)}
value={values.cModifiedOn22}
selected={values.cModifiedOn22}
disabled = {(authCol.ModifiedOn22 || {}).readonly ? true: false }/>
</div>
}
{errors.cModifiedOn22 && touched.cModifiedOn22 && <span className='form__form-group-error'>{errors.cModifiedOn22}</span>}
</div>
</Col>
}
{(authCol.TemplateName22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TemplateName22 || {}).ColumnHeader} {(columnLabel.TemplateName22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TemplateName22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TemplateName22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTemplateName22'
disabled = {(authCol.TemplateName22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTemplateName22 && touched.cTemplateName22 && <span className='form__form-group-error'>{errors.cTemplateName22}</span>}
</div>
</Col>
}
{(authCol.RptTemplate22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptTemplate22 || {}).ColumnHeader} {(columnLabel.RptTemplate22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptTemplate22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptTemplate22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptTemplate22'
disabled = {(authCol.RptTemplate22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptTemplate22 && touched.cRptTemplate22 && <span className='form__form-group-error'>{errors.cRptTemplate22}</span>}
</div>
</Col>
}
{(authCol.UnitCd22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UnitCd22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.UnitCd22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UnitCd22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UnitCd22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cUnitCd22'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cUnitCd22')}
value={values.cUnitCd22}
options={UnitCd22List}
placeholder=''
disabled = {(authCol.UnitCd22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUnitCd22 && touched.cUnitCd22 && <span className='form__form-group-error'>{errors.cUnitCd22}</span>}
</div>
</Col>
}
{(authCol.TopMargin22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TopMargin22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TopMargin22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TopMargin22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TopMargin22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTopMargin22'
disabled = {(authCol.TopMargin22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTopMargin22 && touched.cTopMargin22 && <span className='form__form-group-error'>{errors.cTopMargin22}</span>}
</div>
</Col>
}
{(authCol.BottomMargin22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BottomMargin22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.BottomMargin22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BottomMargin22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BottomMargin22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBottomMargin22'
disabled = {(authCol.BottomMargin22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBottomMargin22 && touched.cBottomMargin22 && <span className='form__form-group-error'>{errors.cBottomMargin22}</span>}
</div>
</Col>
}
{(authCol.LeftMargin22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LeftMargin22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LeftMargin22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LeftMargin22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LeftMargin22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLeftMargin22'
disabled = {(authCol.LeftMargin22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLeftMargin22 && touched.cLeftMargin22 && <span className='form__form-group-error'>{errors.cLeftMargin22}</span>}
</div>
</Col>
}
{(authCol.RightMargin22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RightMargin22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RightMargin22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RightMargin22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RightMargin22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRightMargin22'
disabled = {(authCol.RightMargin22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRightMargin22 && touched.cRightMargin22 && <span className='form__form-group-error'>{errors.cRightMargin22}</span>}
</div>
</Col>
}
{(authCol.PageWidth22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageWidth22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.PageWidth22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageWidth22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageWidth22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageWidth22'
disabled = {(authCol.PageWidth22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageWidth22 && touched.cPageWidth22 && <span className='form__form-group-error'>{errors.cPageWidth22}</span>}
</div>
</Col>
}
{(authCol.PageHeight22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageHeight22 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.PageHeight22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageHeight22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageHeight22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageHeight22'
disabled = {(authCol.PageHeight22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageHeight22 && touched.cPageHeight22 && <span className='form__form-group-error'>{errors.cPageHeight22}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncByDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncByDb || {}).readonly || !(authCol.SyncByDb || {}).visible} onClick={this.SyncByDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncByDb || (columnLabel.SyncByDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncToDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncToDb || {}).readonly || !(authCol.SyncToDb || {}).visible} onClick={this.SyncToDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncToDb || (columnLabel.SyncToDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.AllowSelect22 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAllowSelect22'
onChange={handleChange}
defaultChecked={values.cAllowSelect22}
disabled={(authCol.AllowSelect22 || {}).readonly || !(authCol.AllowSelect22 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AllowSelect22 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AllowSelect22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowSelect22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowSelect22 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.GenerateRp22 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cGenerateRp22'
onChange={handleChange}
defaultChecked={values.cGenerateRp22}
disabled={(authCol.GenerateRp22 || {}).readonly || !(authCol.GenerateRp22 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.GenerateRp22 || {}).ColumnHeader}</span>
</label>
{(columnLabel.GenerateRp22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GenerateRp22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GenerateRp22 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.LastGenDt22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastGenDt22 || {}).ColumnHeader} {(columnLabel.LastGenDt22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastGenDt22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastGenDt22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLastGenDt22'
disabled = {(authCol.LastGenDt22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLastGenDt22 && touched.cLastGenDt22 && <span className='form__form-group-error'>{errors.cLastGenDt22}</span>}
</div>
</Col>
}
{(authCol.AuthRequired22 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAuthRequired22'
onChange={handleChange}
defaultChecked={values.cAuthRequired22}
disabled={(authCol.AuthRequired22 || {}).readonly || !(authCol.AuthRequired22 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AuthRequired22 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AuthRequired22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AuthRequired22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AuthRequired22 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.WhereClause22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WhereClause22 || {}).ColumnHeader} {(columnLabel.WhereClause22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WhereClause22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WhereClause22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWhereClause22'
disabled = {(authCol.WhereClause22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWhereClause22 && touched.cWhereClause22 && <span className='form__form-group-error'>{errors.cWhereClause22}</span>}
</div>
</Col>
}
{(authCol.RegClause22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RegClause22 || {}).ColumnHeader} {(columnLabel.RegClause22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RegClause22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RegClause22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRegClause22'
disabled = {(authCol.RegClause22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRegClause22 && touched.cRegClause22 && <span className='form__form-group-error'>{errors.cRegClause22}</span>}
</div>
</Col>
}
{(authCol.RegCode22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RegCode22 || {}).ColumnHeader} {(columnLabel.RegCode22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RegCode22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RegCode22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRegCode22'
disabled = {(authCol.RegCode22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRegCode22 && touched.cRegCode22 && <span className='form__form-group-error'>{errors.cRegCode22}</span>}
</div>
</Col>
}
{(authCol.ValClause22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ValClause22 || {}).ColumnHeader} {(columnLabel.ValClause22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ValClause22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ValClause22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cValClause22'
disabled = {(authCol.ValClause22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cValClause22 && touched.cValClause22 && <span className='form__form-group-error'>{errors.cValClause22}</span>}
</div>
</Col>
}
{(authCol.ValCode22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ValCode22 || {}).ColumnHeader} {(columnLabel.ValCode22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ValCode22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ValCode22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cValCode22'
disabled = {(authCol.ValCode22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cValCode22 && touched.cValCode22 && <span className='form__form-group-error'>{errors.cValCode22}</span>}
</div>
</Col>
}
{(authCol.UpdClause22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UpdClause22 || {}).ColumnHeader} {(columnLabel.UpdClause22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UpdClause22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UpdClause22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUpdClause22'
disabled = {(authCol.UpdClause22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUpdClause22 && touched.cUpdClause22 && <span className='form__form-group-error'>{errors.cUpdClause22}</span>}
</div>
</Col>
}
{(authCol.UpdCode22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UpdCode22 || {}).ColumnHeader} {(columnLabel.UpdCode22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UpdCode22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UpdCode22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUpdCode22'
disabled = {(authCol.UpdCode22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUpdCode22 && touched.cUpdCode22 && <span className='form__form-group-error'>{errors.cUpdCode22}</span>}
</div>
</Col>
}
{(authCol.XlsClause22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.XlsClause22 || {}).ColumnHeader} {(columnLabel.XlsClause22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.XlsClause22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.XlsClause22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cXlsClause22'
disabled = {(authCol.XlsClause22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cXlsClause22 && touched.cXlsClause22 && <span className='form__form-group-error'>{errors.cXlsClause22}</span>}
</div>
</Col>
}
{(authCol.XlsCode22 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.XlsCode22 || {}).ColumnHeader} {(columnLabel.XlsCode22 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.XlsCode22 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.XlsCode22 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cXlsCode22'
disabled = {(authCol.XlsCode22 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cXlsCode22 && touched.cXlsCode22 && <span className='form__form-group-error'>{errors.cXlsCode22}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ReportId22));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmReportState) && <Skeleton height='43px' />) ||
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
  AdmReport: state.AdmReport,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmReportReduxObj.LoadPage.bind(AdmReportReduxObj) },
    { SavePage: AdmReportReduxObj.SavePage.bind(AdmReportReduxObj) },
    { DelMst: AdmReportReduxObj.DelMst.bind(AdmReportReduxObj) },
    { AddMst: AdmReportReduxObj.AddMst.bind(AdmReportReduxObj) },
//    { SearchMemberId64: AdmReportReduxObj.SearchActions.SearchMemberId64.bind(AdmReportReduxObj) },
//    { SearchCurrencyId64: AdmReportReduxObj.SearchActions.SearchCurrencyId64.bind(AdmReportReduxObj) },
//    { SearchCustomerJobId64: AdmReportReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmReportReduxObj) },
{ SearchCopyReportId22: AdmReportReduxObj.SearchActions.SearchCopyReportId22.bind(AdmReportReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            