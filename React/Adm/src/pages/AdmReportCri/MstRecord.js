
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
import AdmReportCriReduxObj, { ShowMstFilterApplied } from '../../redux/AdmReportCri';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmReportCri || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ReportCriId97';
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

ReportId97InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId97(v, filterBy);}}
DdlFtrColumnId97InputChange() { const _this = this; return function (name, v) {const filterBy = ((_this.props.AdmReportCri || {}).Mst || {}).ReportId97; _this.props.SearchDdlFtrColumnId97(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmReportCri || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cReportId97 || {}).value)) { errors.cReportId97 = (columnLabel.ReportId97 || {}).ErrMessage;}
if (!values.cTabIndex97) { errors.cTabIndex97 = (columnLabel.TabIndex97 || {}).ErrMessage;}
if (!values.cColumnName97) { errors.cColumnName97 = (columnLabel.ColumnName97 || {}).ErrMessage;}
if (isEmptyId((values.cReportGrpId97 || {}).value)) { errors.cReportGrpId97 = (columnLabel.ReportGrpId97 || {}).ErrMessage;}
if (isEmptyId((values.cDataTypeId97 || {}).value)) { errors.cDataTypeId97 = (columnLabel.DataTypeId97 || {}).ErrMessage;}
if (isEmptyId((values.cDisplayModeId97 || {}).value)) { errors.cDisplayModeId97 = (columnLabel.DisplayModeId97 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmReportCri || {}).Mst || {};
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
        this.props.AdmReportCri,
        {
          ReportCriId97: values.cReportCriId97|| '',
          ReportId97: (values.cReportId97|| {}).value || '',
          TabIndex97: values.cTabIndex97|| '',
          ColumnName97: values.cColumnName97|| '',
          ReportGrpId97: (values.cReportGrpId97|| {}).value || '',
          LabelCss97: values.cLabelCss97|| '',
          ContentCss97: values.cContentCss97|| '',
          DefaultValue97: values.cDefaultValue97|| '',
          TableId97: (values.cTableId97|| {}).value || '',
          TableAbbr97: values.cTableAbbr97|| '',
          RequiredValid97: values.cRequiredValid97 ? 'Y' : 'N',
          DataTypeId97: (values.cDataTypeId97|| {}).value || '',
          DataTypeSize97: values.cDataTypeSize97|| '',
          DisplayModeId97: (values.cDisplayModeId97|| {}).value || '',
          ColumnSize97: values.cColumnSize97|| '',
          RowSize97: values.cRowSize97|| '',
          DdlKeyColumnName97: values.cDdlKeyColumnName97|| '',
          DdlRefColumnName97: values.cDdlRefColumnName97|| '',
          DdlSrtColumnName97: values.cDdlSrtColumnName97|| '',
          DdlFtrColumnId97: (values.cDdlFtrColumnId97|| {}).value || '',
          WhereClause97: values.cWhereClause97|| '',
          RegClause97: values.cRegClause97|| '',
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
    const AdmReportCriState = this.props.AdmReportCri || {};
    const auxSystemLabels = AdmReportCriState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ReportCriId97;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmReportCri.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmReportCri.Label);
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
    const AdmReportCriState = this.props.AdmReportCri || {};
    const auxSystemLabels = AdmReportCriState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ReportCriId97;
        this.props.DelMst(this.props.AdmReportCri, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmReportCri || {};
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
    const AdmReportCriState = this.props.AdmReportCri || {};
    const auxSystemLabels = AdmReportCriState.SystemLabel || {};
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
      if (!(this.props.AdmReportCri || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmReportCri || {};

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
    const AdmReportCriState = this.props.AdmReportCri || {};

    if (AdmReportCriState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmReportCriState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmReportCriReduxObj.GetScreenButtons(AdmReportCriState) || {};
    const itemList = AdmReportCriState.Dtl || [];
    const auxLabels = AdmReportCriState.Label || {};
    const auxSystemLabels = AdmReportCriState.SystemLabel || {};

    const columnLabel = AdmReportCriState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmReportCriState);
    const authRow = (AdmReportCriState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmReportCri || {}).Mst || {});
    const currDtl = ((this.props.AdmReportCri || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ReportCriId97));
    const selectList = AdmReportCriReduxObj.SearchListToSelectList(AdmReportCriState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ReportCriId97 = currMst.ReportCriId97;
const ReportId97List = AdmReportCriReduxObj.ScreenDdlSelectors.ReportId97(AdmReportCriState);
const ReportId97 = currMst.ReportId97;
const TabIndex97 = currMst.TabIndex97;
const ColumnName97 = currMst.ColumnName97;
const ReportGrpId97List = AdmReportCriReduxObj.ScreenDdlSelectors.ReportGrpId97(AdmReportCriState);
const ReportGrpId97 = currMst.ReportGrpId97;
const LabelCss97 = currMst.LabelCss97;
const ContentCss97 = currMst.ContentCss97;
const DefaultValue97 = currMst.DefaultValue97;
const TableId97List = AdmReportCriReduxObj.ScreenDdlSelectors.TableId97(AdmReportCriState);
const TableId97 = currMst.TableId97;
const TableAbbr97 = currMst.TableAbbr97;
const RequiredValid97 = currMst.RequiredValid97;
const DataTypeId97List = AdmReportCriReduxObj.ScreenDdlSelectors.DataTypeId97(AdmReportCriState);
const DataTypeId97 = currMst.DataTypeId97;
const DataTypeSize97 = currMst.DataTypeSize97;
const DisplayModeId97List = AdmReportCriReduxObj.ScreenDdlSelectors.DisplayModeId97(AdmReportCriState);
const DisplayModeId97 = currMst.DisplayModeId97;
const ColumnSize97 = currMst.ColumnSize97;
const RowSize97 = currMst.RowSize97;
const DdlKeyColumnName97 = currMst.DdlKeyColumnName97;
const DdlRefColumnName97 = currMst.DdlRefColumnName97;
const DdlSrtColumnName97 = currMst.DdlSrtColumnName97;
const DdlFtrColumnId97List = AdmReportCriReduxObj.ScreenDdlSelectors.DdlFtrColumnId97(AdmReportCriState);
const DdlFtrColumnId97 = currMst.DdlFtrColumnId97;
const WhereClause97 = currMst.WhereClause97;
const RegClause97 = currMst.RegClause97;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmReportCriState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cReportCriId97: ReportCriId97 || '',
                  cReportId97: ReportId97List.filter(obj => { return obj.key === ReportId97 })[0],
                  cTabIndex97: TabIndex97 || '',
                  cColumnName97: ColumnName97 || '',
                  cReportGrpId97: ReportGrpId97List.filter(obj => { return obj.key === ReportGrpId97 })[0],
                  cLabelCss97: LabelCss97 || '',
                  cContentCss97: ContentCss97 || '',
                  cDefaultValue97: DefaultValue97 || '',
                  cTableId97: TableId97List.filter(obj => { return obj.key === TableId97 })[0],
                  cTableAbbr97: TableAbbr97 || '',
                  cRequiredValid97: RequiredValid97 === 'Y',
                  cDataTypeId97: DataTypeId97List.filter(obj => { return obj.key === DataTypeId97 })[0],
                  cDataTypeSize97: DataTypeSize97 || '',
                  cDisplayModeId97: DisplayModeId97List.filter(obj => { return obj.key === DisplayModeId97 })[0],
                  cColumnSize97: ColumnSize97 || '',
                  cRowSize97: RowSize97 || '',
                  cDdlKeyColumnName97: DdlKeyColumnName97 || '',
                  cDdlRefColumnName97: DdlRefColumnName97 || '',
                  cDdlSrtColumnName97: DdlSrtColumnName97 || '',
                  cDdlFtrColumnId97: DdlFtrColumnId97List.filter(obj => { return obj.key === DdlFtrColumnId97 })[0],
                  cWhereClause97: WhereClause97 || '',
                  cRegClause97: RegClause97 || '',
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
                                {(this.constructor.ShowSpinner(AdmReportCriState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportCriId97)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportCriId97)) return null;
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
            {(authCol.ReportCriId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportCriId97 || {}).ColumnHeader} {(columnLabel.ReportCriId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportCriId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportCriId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReportCriId97'
disabled = {(authCol.ReportCriId97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportCriId97 && touched.cReportCriId97 && <span className='form__form-group-error'>{errors.cReportCriId97}</span>}
</div>
</Col>
}
{(authCol.ReportId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId97'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId97', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId97', true)}
onInputChange={this.ReportId97InputChange()}
value={values.cReportId97}
defaultSelected={ReportId97List.filter(obj => { return obj.key === ReportId97 })}
options={ReportId97List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId97 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId97 && touched.cReportId97 && <span className='form__form-group-error'>{errors.cReportId97}</span>}
</div>
</Col>
}
{(authCol.TabIndex97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TabIndex97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TabIndex97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TabIndex97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TabIndex97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTabIndex97'
disabled = {(authCol.TabIndex97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTabIndex97 && touched.cTabIndex97 && <span className='form__form-group-error'>{errors.cTabIndex97}</span>}
</div>
</Col>
}
{(authCol.ColumnName97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnName97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnName97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnName97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnName97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnName97'
disabled = {(authCol.ColumnName97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnName97 && touched.cColumnName97 && <span className='form__form-group-error'>{errors.cColumnName97}</span>}
</div>
</Col>
}
{(authCol.ReportGrpId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportGrpId97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportGrpId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportGrpId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportGrpId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReportGrpId97'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReportGrpId97')}
value={values.cReportGrpId97}
options={ReportGrpId97List}
placeholder=''
disabled = {(authCol.ReportGrpId97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportGrpId97 && touched.cReportGrpId97 && <span className='form__form-group-error'>{errors.cReportGrpId97}</span>}
</div>
</Col>
}
{(authCol.LabelCss97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelCss97 || {}).ColumnHeader} {(columnLabel.LabelCss97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelCss97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelCss97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelCss97'
disabled = {(authCol.LabelCss97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelCss97 && touched.cLabelCss97 && <span className='form__form-group-error'>{errors.cLabelCss97}</span>}
</div>
</Col>
}
{(authCol.ContentCss97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ContentCss97 || {}).ColumnHeader} {(columnLabel.ContentCss97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ContentCss97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ContentCss97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cContentCss97'
disabled = {(authCol.ContentCss97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cContentCss97 && touched.cContentCss97 && <span className='form__form-group-error'>{errors.cContentCss97}</span>}
</div>
</Col>
}
{(authCol.DefaultValue97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultValue97 || {}).ColumnHeader} {(columnLabel.DefaultValue97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultValue97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultValue97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefaultValue97'
disabled = {(authCol.DefaultValue97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultValue97 && touched.cDefaultValue97 && <span className='form__form-group-error'>{errors.cDefaultValue97}</span>}
</div>
</Col>
}
{(authCol.TableId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableId97 || {}).ColumnHeader} {(columnLabel.TableId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cTableId97'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cTableId97')}
value={values.cTableId97}
options={TableId97List}
placeholder=''
disabled = {(authCol.TableId97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTableId97 && touched.cTableId97 && <span className='form__form-group-error'>{errors.cTableId97}</span>}
</div>
</Col>
}
{(authCol.TableAbbr97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableAbbr97 || {}).ColumnHeader} {(columnLabel.TableAbbr97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableAbbr97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableAbbr97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTableAbbr97'
disabled = {(authCol.TableAbbr97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTableAbbr97 && touched.cTableAbbr97 && <span className='form__form-group-error'>{errors.cTableAbbr97}</span>}
</div>
</Col>
}
{(authCol.RequiredValid97 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cRequiredValid97'
onChange={handleChange}
defaultChecked={values.cRequiredValid97}
disabled={(authCol.RequiredValid97 || {}).readonly || !(authCol.RequiredValid97 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.RequiredValid97 || {}).ColumnHeader}</span>
</label>
{(columnLabel.RequiredValid97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RequiredValid97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RequiredValid97 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.DataTypeId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataTypeId97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DataTypeId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataTypeId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataTypeId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDataTypeId97'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDataTypeId97')}
value={values.cDataTypeId97}
options={DataTypeId97List}
placeholder=''
disabled = {(authCol.DataTypeId97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataTypeId97 && touched.cDataTypeId97 && <span className='form__form-group-error'>{errors.cDataTypeId97}</span>}
</div>
</Col>
}
{(authCol.DataTypeSize97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DataTypeSize97 || {}).ColumnHeader} {(columnLabel.DataTypeSize97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DataTypeSize97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DataTypeSize97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDataTypeSize97'
disabled = {(authCol.DataTypeSize97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDataTypeSize97 && touched.cDataTypeSize97 && <span className='form__form-group-error'>{errors.cDataTypeSize97}</span>}
</div>
</Col>
}
{(authCol.DisplayModeId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DisplayModeId97 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DisplayModeId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DisplayModeId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DisplayModeId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDisplayModeId97'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDisplayModeId97')}
value={values.cDisplayModeId97}
options={DisplayModeId97List}
placeholder=''
disabled = {(authCol.DisplayModeId97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDisplayModeId97 && touched.cDisplayModeId97 && <span className='form__form-group-error'>{errors.cDisplayModeId97}</span>}
</div>
</Col>
}
{(authCol.ColumnSize97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnSize97 || {}).ColumnHeader} {(columnLabel.ColumnSize97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnSize97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnSize97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnSize97'
disabled = {(authCol.ColumnSize97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnSize97 && touched.cColumnSize97 && <span className='form__form-group-error'>{errors.cColumnSize97}</span>}
</div>
</Col>
}
{(authCol.RowSize97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowSize97 || {}).ColumnHeader} {(columnLabel.RowSize97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowSize97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowSize97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowSize97'
disabled = {(authCol.RowSize97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowSize97 && touched.cRowSize97 && <span className='form__form-group-error'>{errors.cRowSize97}</span>}
</div>
</Col>
}
{(authCol.DdlKeyColumnName97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlKeyColumnName97 || {}).ColumnHeader} {(columnLabel.DdlKeyColumnName97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlKeyColumnName97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlKeyColumnName97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDdlKeyColumnName97'
disabled = {(authCol.DdlKeyColumnName97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDdlKeyColumnName97 && touched.cDdlKeyColumnName97 && <span className='form__form-group-error'>{errors.cDdlKeyColumnName97}</span>}
</div>
</Col>
}
{(authCol.DdlRefColumnName97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlRefColumnName97 || {}).ColumnHeader} {(columnLabel.DdlRefColumnName97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlRefColumnName97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlRefColumnName97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDdlRefColumnName97'
disabled = {(authCol.DdlRefColumnName97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDdlRefColumnName97 && touched.cDdlRefColumnName97 && <span className='form__form-group-error'>{errors.cDdlRefColumnName97}</span>}
</div>
</Col>
}
{(authCol.DdlSrtColumnName97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlSrtColumnName97 || {}).ColumnHeader} {(columnLabel.DdlSrtColumnName97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlSrtColumnName97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlSrtColumnName97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDdlSrtColumnName97'
disabled = {(authCol.DdlSrtColumnName97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDdlSrtColumnName97 && touched.cDdlSrtColumnName97 && <span className='form__form-group-error'>{errors.cDdlSrtColumnName97}</span>}
</div>
</Col>
}
{(authCol.DdlFtrColumnId97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlFtrColumnId97 || {}).ColumnHeader} {(columnLabel.DdlFtrColumnId97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlFtrColumnId97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlFtrColumnId97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlFtrColumnId97'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId97', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId97', true)}
onInputChange={this.DdlFtrColumnId97InputChange()}
value={values.cDdlFtrColumnId97}
defaultSelected={DdlFtrColumnId97List.filter(obj => { return obj.key === DdlFtrColumnId97 })}
options={DdlFtrColumnId97List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlFtrColumnId97 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlFtrColumnId97 && touched.cDdlFtrColumnId97 && <span className='form__form-group-error'>{errors.cDdlFtrColumnId97}</span>}
</div>
</Col>
}
{(authCol.WhereClause97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WhereClause97 || {}).ColumnHeader} {(columnLabel.WhereClause97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WhereClause97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WhereClause97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cWhereClause97'
disabled = {(authCol.WhereClause97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWhereClause97 && touched.cWhereClause97 && <span className='form__form-group-error'>{errors.cWhereClause97}</span>}
</div>
</Col>
}
{(authCol.RegClause97 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RegClause97 || {}).ColumnHeader} {(columnLabel.RegClause97 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RegClause97 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RegClause97 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportCriState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRegClause97'
disabled = {(authCol.RegClause97 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRegClause97 && touched.cRegClause97 && <span className='form__form-group-error'>{errors.cRegClause97}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportCriId97)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ReportCriId97));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmReportCriState) && <Skeleton height='43px' />) ||
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
  AdmReportCri: state.AdmReportCri,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmReportCriReduxObj.LoadPage.bind(AdmReportCriReduxObj) },
    { SavePage: AdmReportCriReduxObj.SavePage.bind(AdmReportCriReduxObj) },
    { DelMst: AdmReportCriReduxObj.DelMst.bind(AdmReportCriReduxObj) },
    { AddMst: AdmReportCriReduxObj.AddMst.bind(AdmReportCriReduxObj) },
//    { SearchMemberId64: AdmReportCriReduxObj.SearchActions.SearchMemberId64.bind(AdmReportCriReduxObj) },
//    { SearchCurrencyId64: AdmReportCriReduxObj.SearchActions.SearchCurrencyId64.bind(AdmReportCriReduxObj) },
//    { SearchCustomerJobId64: AdmReportCriReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmReportCriReduxObj) },
{ SearchReportId97: AdmReportCriReduxObj.SearchActions.SearchReportId97.bind(AdmReportCriReduxObj) },
{ SearchDdlFtrColumnId97: AdmReportCriReduxObj.SearchActions.SearchDdlFtrColumnId97.bind(AdmReportCriReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            