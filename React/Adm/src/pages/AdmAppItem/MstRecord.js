
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
import AdmAppItemReduxObj, { ShowMstFilterApplied } from '../../redux/AdmAppItem';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmAppItem || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'AppItemId136';
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

AppInfoId136InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchAppInfoId136(v, filterBy);}}
ScreenId136InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId136(v, filterBy);}}
ReportId136InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId136(v, filterBy);}}
WizardId136InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchWizardId136(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmAppItem || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cAppInfoId136 || {}).value)) { errors.cAppInfoId136 = (columnLabel.AppInfoId136 || {}).ErrMessage;}
if (isEmptyId((values.cObjectTypeCd136 || {}).value)) { errors.cObjectTypeCd136 = (columnLabel.ObjectTypeCd136 || {}).ErrMessage;}
if (!values.cAppItemName136) { errors.cAppItemName136 = (columnLabel.AppItemName136 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmAppItem || {}).Mst || {};
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
        this.props.AdmAppItem,
        {
          AppItemId136: values.cAppItemId136|| '',
          AppInfoId136: (values.cAppInfoId136|| {}).value || '',
          LanguageCd136: (values.cLanguageCd136|| {}).value || '',
          ItemOrder136: values.cItemOrder136|| '',
          FrameworkCd136: (values.cFrameworkCd136|| {}).value || '',
          ObjectTypeCd136: (values.cObjectTypeCd136|| {}).value || '',
          DbProviderCd136: (values.cDbProviderCd136|| {}).value || '',
          RelativePath136: values.cRelativePath136|| '',
          AppItemName136: values.cAppItemName136|| '',
          MultiDesignDb136: values.cMultiDesignDb136 ? 'Y' : 'N',
          RemoveItem136: values.cRemoveItem136 ? 'Y' : 'N',
          AppItemCode136: values.cAppItemCode136|| '',
          ScreenId136: (values.cScreenId136|| {}).value || '',
          ReportId136: (values.cReportId136|| {}).value || '',
          WizardId136: (values.cWizardId136|| {}).value || '',
          CustomId136: values.cCustomId136|| '',
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
    const AdmAppItemState = this.props.AdmAppItem || {};
    const auxSystemLabels = AdmAppItemState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).AppItemId136;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmAppItem.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmAppItem.Label);
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
    const AdmAppItemState = this.props.AdmAppItem || {};
    const auxSystemLabels = AdmAppItemState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.AppItemId136;
        this.props.DelMst(this.props.AdmAppItem, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmAppItem || {};
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
    const AdmAppItemState = this.props.AdmAppItem || {};
    const auxSystemLabels = AdmAppItemState.SystemLabel || {};
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
      if (!(this.props.AdmAppItem || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmAppItem || {};

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
    const AdmAppItemState = this.props.AdmAppItem || {};

    if (AdmAppItemState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmAppItemState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmAppItemReduxObj.GetScreenButtons(AdmAppItemState) || {};
    const itemList = AdmAppItemState.Dtl || [];
    const auxLabels = AdmAppItemState.Label || {};
    const auxSystemLabels = AdmAppItemState.SystemLabel || {};

    const columnLabel = AdmAppItemState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmAppItemState);
    const authRow = (AdmAppItemState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmAppItem || {}).Mst || {});
    const currDtl = ((this.props.AdmAppItem || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.AppItemId136));
    const selectList = AdmAppItemReduxObj.SearchListToSelectList(AdmAppItemState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const AppItemId136 = currMst.AppItemId136;
const AppInfoId136List = AdmAppItemReduxObj.ScreenDdlSelectors.AppInfoId136(AdmAppItemState);
const AppInfoId136 = currMst.AppInfoId136;
const LanguageCd136List = AdmAppItemReduxObj.ScreenDdlSelectors.LanguageCd136(AdmAppItemState);
const LanguageCd136 = currMst.LanguageCd136;
const ItemOrder136 = currMst.ItemOrder136;
const FrameworkCd136List = AdmAppItemReduxObj.ScreenDdlSelectors.FrameworkCd136(AdmAppItemState);
const FrameworkCd136 = currMst.FrameworkCd136;
const ObjectTypeCd136List = AdmAppItemReduxObj.ScreenDdlSelectors.ObjectTypeCd136(AdmAppItemState);
const ObjectTypeCd136 = currMst.ObjectTypeCd136;
const DbProviderCd136List = AdmAppItemReduxObj.ScreenDdlSelectors.DbProviderCd136(AdmAppItemState);
const DbProviderCd136 = currMst.DbProviderCd136;
const RelativePath136 = currMst.RelativePath136;
const AppItemName136 = currMst.AppItemName136;
const MultiDesignDb136 = currMst.MultiDesignDb136;
const RemoveItem136 = currMst.RemoveItem136;
const AppItemCode136 = currMst.AppItemCode136;
const ScreenId136List = AdmAppItemReduxObj.ScreenDdlSelectors.ScreenId136(AdmAppItemState);
const ScreenId136 = currMst.ScreenId136;
const ReportId136List = AdmAppItemReduxObj.ScreenDdlSelectors.ReportId136(AdmAppItemState);
const ReportId136 = currMst.ReportId136;
const WizardId136List = AdmAppItemReduxObj.ScreenDdlSelectors.WizardId136(AdmAppItemState);
const WizardId136 = currMst.WizardId136;
const CustomId136 = currMst.CustomId136;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmAppItemState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cAppItemId136: AppItemId136 || '',
                  cAppInfoId136: AppInfoId136List.filter(obj => { return obj.key === AppInfoId136 })[0],
                  cLanguageCd136: LanguageCd136List.filter(obj => { return obj.key === LanguageCd136 })[0],
                  cItemOrder136: ItemOrder136 || '',
                  cFrameworkCd136: FrameworkCd136List.filter(obj => { return obj.key === FrameworkCd136 })[0],
                  cObjectTypeCd136: ObjectTypeCd136List.filter(obj => { return obj.key === ObjectTypeCd136 })[0],
                  cDbProviderCd136: DbProviderCd136List.filter(obj => { return obj.key === DbProviderCd136 })[0],
                  cRelativePath136: RelativePath136 || '',
                  cAppItemName136: AppItemName136 || '',
                  cMultiDesignDb136: MultiDesignDb136 === 'Y',
                  cRemoveItem136: RemoveItem136 === 'Y',
                  cAppItemCode136: AppItemCode136 || '',
                  cScreenId136: ScreenId136List.filter(obj => { return obj.key === ScreenId136 })[0],
                  cReportId136: ReportId136List.filter(obj => { return obj.key === ReportId136 })[0],
                  cWizardId136: WizardId136List.filter(obj => { return obj.key === WizardId136 })[0],
                  cCustomId136: CustomId136 || '',
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
                                {(this.constructor.ShowSpinner(AdmAppItemState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppItemId136)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppItemId136)) return null;
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
            {(authCol.AppItemId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppItemId136 || {}).ColumnHeader} {(columnLabel.AppItemId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppItemId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppItemId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAppItemId136'
disabled = {(authCol.AppItemId136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppItemId136 && touched.cAppItemId136 && <span className='form__form-group-error'>{errors.cAppItemId136}</span>}
</div>
</Col>
}
{(authCol.AppInfoId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppInfoId136 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AppInfoId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppInfoId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppInfoId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cAppInfoId136'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cAppInfoId136', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cAppInfoId136', true)}
onInputChange={this.AppInfoId136InputChange()}
value={values.cAppInfoId136}
defaultSelected={AppInfoId136List.filter(obj => { return obj.key === AppInfoId136 })}
options={AppInfoId136List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.AppInfoId136 || {}).readonly ? true: false }/>
</div>
}
{errors.cAppInfoId136 && touched.cAppInfoId136 && <span className='form__form-group-error'>{errors.cAppInfoId136}</span>}
</div>
</Col>
}
{(authCol.LanguageCd136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LanguageCd136 || {}).ColumnHeader} {(columnLabel.LanguageCd136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LanguageCd136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LanguageCd136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cLanguageCd136'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cLanguageCd136')}
value={values.cLanguageCd136}
options={LanguageCd136List}
placeholder=''
disabled = {(authCol.LanguageCd136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLanguageCd136 && touched.cLanguageCd136 && <span className='form__form-group-error'>{errors.cLanguageCd136}</span>}
</div>
</Col>
}
{(authCol.ItemOrder136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ItemOrder136 || {}).ColumnHeader} {(columnLabel.ItemOrder136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ItemOrder136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ItemOrder136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cItemOrder136'
disabled = {(authCol.ItemOrder136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cItemOrder136 && touched.cItemOrder136 && <span className='form__form-group-error'>{errors.cItemOrder136}</span>}
</div>
</Col>
}
{(authCol.FrameworkCd136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FrameworkCd136 || {}).ColumnHeader} {(columnLabel.FrameworkCd136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FrameworkCd136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FrameworkCd136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cFrameworkCd136'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cFrameworkCd136')}
value={values.cFrameworkCd136}
options={FrameworkCd136List}
placeholder=''
disabled = {(authCol.FrameworkCd136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFrameworkCd136 && touched.cFrameworkCd136 && <span className='form__form-group-error'>{errors.cFrameworkCd136}</span>}
</div>
</Col>
}
{(authCol.ObjectTypeCd136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ObjectTypeCd136 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ObjectTypeCd136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ObjectTypeCd136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ObjectTypeCd136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cObjectTypeCd136'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cObjectTypeCd136')}
value={values.cObjectTypeCd136}
options={ObjectTypeCd136List}
placeholder=''
disabled = {(authCol.ObjectTypeCd136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cObjectTypeCd136 && touched.cObjectTypeCd136 && <span className='form__form-group-error'>{errors.cObjectTypeCd136}</span>}
</div>
</Col>
}
{(authCol.DbProviderCd136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DbProviderCd136 || {}).ColumnHeader} {(columnLabel.DbProviderCd136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DbProviderCd136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DbProviderCd136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDbProviderCd136'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDbProviderCd136')}
value={values.cDbProviderCd136}
options={DbProviderCd136List}
placeholder=''
disabled = {(authCol.DbProviderCd136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDbProviderCd136 && touched.cDbProviderCd136 && <span className='form__form-group-error'>{errors.cDbProviderCd136}</span>}
</div>
</Col>
}
{(authCol.RelativePath136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RelativePath136 || {}).ColumnHeader} {(columnLabel.RelativePath136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RelativePath136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RelativePath136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRelativePath136'
disabled = {(authCol.RelativePath136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRelativePath136 && touched.cRelativePath136 && <span className='form__form-group-error'>{errors.cRelativePath136}</span>}
</div>
</Col>
}
{(authCol.AppItemName136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppItemName136 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AppItemName136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppItemName136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppItemName136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAppItemName136'
disabled = {(authCol.AppItemName136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppItemName136 && touched.cAppItemName136 && <span className='form__form-group-error'>{errors.cAppItemName136}</span>}
</div>
</Col>
}
{(authCol.MultiDesignDb136 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cMultiDesignDb136'
onChange={handleChange}
defaultChecked={values.cMultiDesignDb136}
disabled={(authCol.MultiDesignDb136 || {}).readonly || !(authCol.MultiDesignDb136 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.MultiDesignDb136 || {}).ColumnHeader}</span>
</label>
{(columnLabel.MultiDesignDb136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MultiDesignDb136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MultiDesignDb136 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.RemoveItem136 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cRemoveItem136'
onChange={handleChange}
defaultChecked={values.cRemoveItem136}
disabled={(authCol.RemoveItem136 || {}).readonly || !(authCol.RemoveItem136 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.RemoveItem136 || {}).ColumnHeader}</span>
</label>
{(columnLabel.RemoveItem136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RemoveItem136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RemoveItem136 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AppItemCode136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AppItemCode136 || {}).ColumnHeader} {(columnLabel.AppItemCode136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AppItemCode136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AppItemCode136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cAppItemCode136'
disabled = {(authCol.AppItemCode136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAppItemCode136 && touched.cAppItemCode136 && <span className='form__form-group-error'>{errors.cAppItemCode136}</span>}
</div>
</Col>
}
{(authCol.ScreenId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId136 || {}).ColumnHeader} {(columnLabel.ScreenId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId136'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId136', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId136', true)}
onInputChange={this.ScreenId136InputChange()}
value={values.cScreenId136}
defaultSelected={ScreenId136List.filter(obj => { return obj.key === ScreenId136 })}
options={ScreenId136List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId136 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId136 && touched.cScreenId136 && <span className='form__form-group-error'>{errors.cScreenId136}</span>}
</div>
</Col>
}
{(authCol.ReportId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId136 || {}).ColumnHeader} {(columnLabel.ReportId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId136'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId136', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId136', true)}
onInputChange={this.ReportId136InputChange()}
value={values.cReportId136}
defaultSelected={ReportId136List.filter(obj => { return obj.key === ReportId136 })}
options={ReportId136List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId136 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId136 && touched.cReportId136 && <span className='form__form-group-error'>{errors.cReportId136}</span>}
</div>
</Col>
}
{(authCol.WizardId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardId136 || {}).ColumnHeader} {(columnLabel.WizardId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cWizardId136'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId136', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId136', true)}
onInputChange={this.WizardId136InputChange()}
value={values.cWizardId136}
defaultSelected={WizardId136List.filter(obj => { return obj.key === WizardId136 })}
options={WizardId136List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.WizardId136 || {}).readonly ? true: false }/>
</div>
}
{errors.cWizardId136 && touched.cWizardId136 && <span className='form__form-group-error'>{errors.cWizardId136}</span>}
</div>
</Col>
}
{(authCol.CustomId136 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CustomId136 || {}).ColumnHeader} {(columnLabel.CustomId136 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CustomId136 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CustomId136 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAppItemState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCustomId136'
disabled = {(authCol.CustomId136 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCustomId136 && touched.cCustomId136 && <span className='form__form-group-error'>{errors.cCustomId136}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).AppItemId136)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).AppItemId136));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmAppItemState) && <Skeleton height='43px' />) ||
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
  AdmAppItem: state.AdmAppItem,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmAppItemReduxObj.LoadPage.bind(AdmAppItemReduxObj) },
    { SavePage: AdmAppItemReduxObj.SavePage.bind(AdmAppItemReduxObj) },
    { DelMst: AdmAppItemReduxObj.DelMst.bind(AdmAppItemReduxObj) },
    { AddMst: AdmAppItemReduxObj.AddMst.bind(AdmAppItemReduxObj) },
//    { SearchMemberId64: AdmAppItemReduxObj.SearchActions.SearchMemberId64.bind(AdmAppItemReduxObj) },
//    { SearchCurrencyId64: AdmAppItemReduxObj.SearchActions.SearchCurrencyId64.bind(AdmAppItemReduxObj) },
//    { SearchCustomerJobId64: AdmAppItemReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmAppItemReduxObj) },
{ SearchAppInfoId136: AdmAppItemReduxObj.SearchActions.SearchAppInfoId136.bind(AdmAppItemReduxObj) },
{ SearchScreenId136: AdmAppItemReduxObj.SearchActions.SearchScreenId136.bind(AdmAppItemReduxObj) },
{ SearchReportId136: AdmAppItemReduxObj.SearchActions.SearchReportId136.bind(AdmAppItemReduxObj) },
{ SearchWizardId136: AdmAppItemReduxObj.SearchActions.SearchWizardId136.bind(AdmAppItemReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            