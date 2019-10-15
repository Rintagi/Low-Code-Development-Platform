
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
import AdmMenuReduxObj, { ShowMstFilterApplied } from '../../redux/AdmMenu';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmMenu || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'MenuId39';
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

ScreenId39InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId39(v, filterBy);}}
ReportId39InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchReportId39(v, filterBy);}}
WizardId39InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchWizardId39(v, filterBy);}}
StaticPgId39InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchStaticPgId39(v, filterBy);}}
 IconUrl({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'IconUrl';
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
    const columnLabel = (this.props.AdmMenu || {}).ColumnLabel || {};
    /* standard field validation */

    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmMenu || {}).Mst || {};
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
        this.props.AdmMenu,
        {
          MenuId39: values.cMenuId39|| '',
          Popup39: values.cPopup39 ? 'Y' : 'N',
          ParentId39: values.cParentId39|| '',
          MenuIndex39: values.cMenuIndex39|| '',
          ScreenId39: (values.cScreenId39|| {}).value || '',
          ReportId39: (values.cReportId39|| {}).value || '',
          WizardId39: (values.cWizardId39|| {}).value || '',
          StaticPgId39: (values.cStaticPgId39|| {}).value || '',
          Miscellaneous39: values.cMiscellaneous39|| '',
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
    const AdmMenuState = this.props.AdmMenu || {};
    const auxSystemLabels = AdmMenuState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).MenuId39;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmMenu.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmMenu.Label);
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
    const AdmMenuState = this.props.AdmMenu || {};
    const auxSystemLabels = AdmMenuState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.MenuId39;
        this.props.DelMst(this.props.AdmMenu, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmMenu || {};
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
    const AdmMenuState = this.props.AdmMenu || {};
    const auxSystemLabels = AdmMenuState.SystemLabel || {};
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
      if (!(this.props.AdmMenu || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmMenu || {};

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
    const AdmMenuState = this.props.AdmMenu || {};

    if (AdmMenuState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmMenuState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmMenuReduxObj.GetScreenButtons(AdmMenuState) || {};
    const itemList = AdmMenuState.Dtl || [];
    const auxLabels = AdmMenuState.Label || {};
    const auxSystemLabels = AdmMenuState.SystemLabel || {};

    const columnLabel = AdmMenuState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmMenuState);
    const authRow = (AdmMenuState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmMenu || {}).Mst || {});
    const currDtl = ((this.props.AdmMenu || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.MenuId39));
    const selectList = AdmMenuReduxObj.SearchListToSelectList(AdmMenuState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const MenuId39 = currMst.MenuId39;
const Popup39 = currMst.Popup39;
const ParentId39 = currMst.ParentId39;
const MenuIndex39 = currMst.MenuIndex39;
const ScreenId39List = AdmMenuReduxObj.ScreenDdlSelectors.ScreenId39(AdmMenuState);
const ScreenId39 = currMst.ScreenId39;
const ReportId39List = AdmMenuReduxObj.ScreenDdlSelectors.ReportId39(AdmMenuState);
const ReportId39 = currMst.ReportId39;
const WizardId39List = AdmMenuReduxObj.ScreenDdlSelectors.WizardId39(AdmMenuState);
const WizardId39 = currMst.WizardId39;
const StaticPgId39List = AdmMenuReduxObj.ScreenDdlSelectors.StaticPgId39(AdmMenuState);
const StaticPgId39 = currMst.StaticPgId39;
const Miscellaneous39 = currMst.Miscellaneous39;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmMenuState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cMenuId39: MenuId39 || '',
                  cPopup39: Popup39 === 'Y',
                  cParentId39: ParentId39 || '',
                  cMenuIndex39: MenuIndex39 || '',
                  cScreenId39: ScreenId39List.filter(obj => { return obj.key === ScreenId39 })[0],
                  cReportId39: ReportId39List.filter(obj => { return obj.key === ReportId39 })[0],
                  cWizardId39: WizardId39List.filter(obj => { return obj.key === WizardId39 })[0],
                  cStaticPgId39: StaticPgId39List.filter(obj => { return obj.key === StaticPgId39 })[0],
                  cMiscellaneous39: Miscellaneous39 || '',
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
                                {(this.constructor.ShowSpinner(AdmMenuState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuId39)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuId39)) return null;
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
            {(authCol.MenuId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuId39 || {}).ColumnHeader} {(columnLabel.MenuId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuId39'
disabled = {(authCol.MenuId39 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuId39 && touched.cMenuId39 && <span className='form__form-group-error'>{errors.cMenuId39}</span>}
</div>
</Col>
}
{(authCol.Popup39 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cPopup39'
onChange={handleChange}
defaultChecked={values.cPopup39}
disabled={(authCol.Popup39 || {}).readonly || !(authCol.Popup39 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.Popup39 || {}).ColumnHeader}</span>
</label>
{(columnLabel.Popup39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Popup39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Popup39 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ParentId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ParentId39 || {}).ColumnHeader} {(columnLabel.ParentId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ParentId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ParentId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cParentId39'
disabled = {(authCol.ParentId39 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cParentId39 && touched.cParentId39 && <span className='form__form-group-error'>{errors.cParentId39}</span>}
</div>
</Col>
}
{(authCol.MenuIndex39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuIndex39 || {}).ColumnHeader} {(columnLabel.MenuIndex39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuIndex39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuIndex39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuIndex39'
disabled = {(authCol.MenuIndex39 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuIndex39 && touched.cMenuIndex39 && <span className='form__form-group-error'>{errors.cMenuIndex39}</span>}
</div>
</Col>
}
{(authCol.ScreenId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId39 || {}).ColumnHeader} {(columnLabel.ScreenId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId39'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId39', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId39', true)}
onInputChange={this.ScreenId39InputChange()}
value={values.cScreenId39}
defaultSelected={ScreenId39List.filter(obj => { return obj.key === ScreenId39 })}
options={ScreenId39List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId39 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId39 && touched.cScreenId39 && <span className='form__form-group-error'>{errors.cScreenId39}</span>}
</div>
</Col>
}
{(authCol.ReportId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportId39 || {}).ColumnHeader} {(columnLabel.ReportId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cReportId39'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId39', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cReportId39', true)}
onInputChange={this.ReportId39InputChange()}
value={values.cReportId39}
defaultSelected={ReportId39List.filter(obj => { return obj.key === ReportId39 })}
options={ReportId39List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ReportId39 || {}).readonly ? true: false }/>
</div>
}
{errors.cReportId39 && touched.cReportId39 && <span className='form__form-group-error'>{errors.cReportId39}</span>}
</div>
</Col>
}
{(authCol.WizardId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WizardId39 || {}).ColumnHeader} {(columnLabel.WizardId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WizardId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WizardId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cWizardId39'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId39', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cWizardId39', true)}
onInputChange={this.WizardId39InputChange()}
value={values.cWizardId39}
defaultSelected={WizardId39List.filter(obj => { return obj.key === WizardId39 })}
options={WizardId39List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.WizardId39 || {}).readonly ? true: false }/>
</div>
}
{errors.cWizardId39 && touched.cWizardId39 && <span className='form__form-group-error'>{errors.cWizardId39}</span>}
</div>
</Col>
}
{(authCol.StaticPgId39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgId39 || {}).ColumnHeader} {(columnLabel.StaticPgId39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgId39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgId39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cStaticPgId39'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cStaticPgId39', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cStaticPgId39', true)}
onInputChange={this.StaticPgId39InputChange()}
value={values.cStaticPgId39}
defaultSelected={StaticPgId39List.filter(obj => { return obj.key === StaticPgId39 })}
options={StaticPgId39List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.StaticPgId39 || {}).readonly ? true: false }/>
</div>
}
{errors.cStaticPgId39 && touched.cStaticPgId39 && <span className='form__form-group-error'>{errors.cStaticPgId39}</span>}
</div>
</Col>
}
{(authCol.Miscellaneous39 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Miscellaneous39 || {}).ColumnHeader} {(columnLabel.Miscellaneous39 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Miscellaneous39 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Miscellaneous39 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMiscellaneous39'
disabled = {(authCol.Miscellaneous39 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMiscellaneous39 && touched.cMiscellaneous39 && <span className='form__form-group-error'>{errors.cMiscellaneous39}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.IconUrl || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.IconUrl || {}).readonly || !(authCol.IconUrl || {}).visible} onClick={this.IconUrl({ naviBar, submitForm, currMst })} >{auxLabels.IconUrl || (columnLabel.IconUrl || {}).ColumnName}</Button>}
</div>
</div>
</Col>
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuId39)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).MenuId39));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmMenuState) && <Skeleton height='43px' />) ||
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
  AdmMenu: state.AdmMenu,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmMenuReduxObj.LoadPage.bind(AdmMenuReduxObj) },
    { SavePage: AdmMenuReduxObj.SavePage.bind(AdmMenuReduxObj) },
    { DelMst: AdmMenuReduxObj.DelMst.bind(AdmMenuReduxObj) },
    { AddMst: AdmMenuReduxObj.AddMst.bind(AdmMenuReduxObj) },
//    { SearchMemberId64: AdmMenuReduxObj.SearchActions.SearchMemberId64.bind(AdmMenuReduxObj) },
//    { SearchCurrencyId64: AdmMenuReduxObj.SearchActions.SearchCurrencyId64.bind(AdmMenuReduxObj) },
//    { SearchCustomerJobId64: AdmMenuReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmMenuReduxObj) },
{ SearchScreenId39: AdmMenuReduxObj.SearchActions.SearchScreenId39.bind(AdmMenuReduxObj) },
{ SearchReportId39: AdmMenuReduxObj.SearchActions.SearchReportId39.bind(AdmMenuReduxObj) },
{ SearchWizardId39: AdmMenuReduxObj.SearchActions.SearchWizardId39.bind(AdmMenuReduxObj) },
{ SearchStaticPgId39: AdmMenuReduxObj.SearchActions.SearchStaticPgId39.bind(AdmMenuReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            