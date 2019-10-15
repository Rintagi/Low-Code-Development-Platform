
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
import AdmRulTierReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRulTier';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRulTier || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RuleTierId196';
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

/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmRulTier || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRuleTierName196) { errors.cRuleTierName196 = (columnLabel.RuleTierName196 || {}).ErrMessage;}
if (isEmptyId((values.cEntityId196 || {}).value)) { errors.cEntityId196 = (columnLabel.EntityId196 || {}).ErrMessage;}
if (isEmptyId((values.cLanguageCd196 || {}).value)) { errors.cLanguageCd196 = (columnLabel.LanguageCd196 || {}).ErrMessage;}
if (isEmptyId((values.cFrameworkCd196 || {}).value)) { errors.cFrameworkCd196 = (columnLabel.FrameworkCd196 || {}).ErrMessage;}
if (!values.cDevProgramPath196) { errors.cDevProgramPath196 = (columnLabel.DevProgramPath196 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRulTier || {}).Mst || {};
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
        this.props.AdmRulTier,
        {
          RuleTierId196: values.cRuleTierId196|| '',
          RuleTierName196: values.cRuleTierName196|| '',
          EntityId196: (values.cEntityId196|| {}).value || '',
          LanguageCd196: (values.cLanguageCd196|| {}).value || '',
          FrameworkCd196: (values.cFrameworkCd196|| {}).value || '',
          DevProgramPath196: values.cDevProgramPath196|| '',
          IsDefault196: values.cIsDefault196 ? 'Y' : 'N',
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
    const AdmRulTierState = this.props.AdmRulTier || {};
    const auxSystemLabels = AdmRulTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RuleTierId196;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRulTier.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRulTier.Label);
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
    const AdmRulTierState = this.props.AdmRulTier || {};
    const auxSystemLabels = AdmRulTierState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RuleTierId196;
        this.props.DelMst(this.props.AdmRulTier, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRulTier || {};
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
    const AdmRulTierState = this.props.AdmRulTier || {};
    const auxSystemLabels = AdmRulTierState.SystemLabel || {};
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
      if (!(this.props.AdmRulTier || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRulTier || {};

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
    const AdmRulTierState = this.props.AdmRulTier || {};

    if (AdmRulTierState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmRulTierState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmRulTierReduxObj.GetScreenButtons(AdmRulTierState) || {};
    const itemList = AdmRulTierState.Dtl || [];
    const auxLabels = AdmRulTierState.Label || {};
    const auxSystemLabels = AdmRulTierState.SystemLabel || {};

    const columnLabel = AdmRulTierState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmRulTierState);
    const authRow = (AdmRulTierState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRulTier || {}).Mst || {});
    const currDtl = ((this.props.AdmRulTier || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RuleTierId196));
    const selectList = AdmRulTierReduxObj.SearchListToSelectList(AdmRulTierState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RuleTierId196 = currMst.RuleTierId196;
const RuleTierName196 = currMst.RuleTierName196;
const EntityId196List = AdmRulTierReduxObj.ScreenDdlSelectors.EntityId196(AdmRulTierState);
const EntityId196 = currMst.EntityId196;
const LanguageCd196List = AdmRulTierReduxObj.ScreenDdlSelectors.LanguageCd196(AdmRulTierState);
const LanguageCd196 = currMst.LanguageCd196;
const FrameworkCd196List = AdmRulTierReduxObj.ScreenDdlSelectors.FrameworkCd196(AdmRulTierState);
const FrameworkCd196 = currMst.FrameworkCd196;
const DevProgramPath196 = currMst.DevProgramPath196;
const IsDefault196 = currMst.IsDefault196;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmRulTierState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRuleTierId196: RuleTierId196 || '',
                  cRuleTierName196: RuleTierName196 || '',
                  cEntityId196: EntityId196List.filter(obj => { return obj.key === EntityId196 })[0],
                  cLanguageCd196: LanguageCd196List.filter(obj => { return obj.key === LanguageCd196 })[0],
                  cFrameworkCd196: FrameworkCd196List.filter(obj => { return obj.key === FrameworkCd196 })[0],
                  cDevProgramPath196: DevProgramPath196 || '',
                  cIsDefault196: IsDefault196 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmRulTierState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RuleTierId196)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RuleTierId196)) return null;
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
            {(authCol.RuleTierId196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTierId196 || {}).ColumnHeader} {(columnLabel.RuleTierId196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTierId196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTierId196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleTierId196'
disabled = {(authCol.RuleTierId196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTierId196 && touched.cRuleTierId196 && <span className='form__form-group-error'>{errors.cRuleTierId196}</span>}
</div>
</Col>
}
{(authCol.RuleTierName196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RuleTierName196 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RuleTierName196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RuleTierName196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RuleTierName196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRuleTierName196'
disabled = {(authCol.RuleTierName196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRuleTierName196 && touched.cRuleTierName196 && <span className='form__form-group-error'>{errors.cRuleTierName196}</span>}
</div>
</Col>
}
{(authCol.EntityId196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EntityId196 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.EntityId196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EntityId196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EntityId196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cEntityId196'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cEntityId196')}
value={values.cEntityId196}
options={EntityId196List}
placeholder=''
disabled = {(authCol.EntityId196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEntityId196 && touched.cEntityId196 && <span className='form__form-group-error'>{errors.cEntityId196}</span>}
</div>
</Col>
}
{(authCol.LanguageCd196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LanguageCd196 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LanguageCd196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LanguageCd196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LanguageCd196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cLanguageCd196'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cLanguageCd196')}
value={values.cLanguageCd196}
options={LanguageCd196List}
placeholder=''
disabled = {(authCol.LanguageCd196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLanguageCd196 && touched.cLanguageCd196 && <span className='form__form-group-error'>{errors.cLanguageCd196}</span>}
</div>
</Col>
}
{(authCol.FrameworkCd196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FrameworkCd196 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.FrameworkCd196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FrameworkCd196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FrameworkCd196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cFrameworkCd196'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cFrameworkCd196')}
value={values.cFrameworkCd196}
options={FrameworkCd196List}
placeholder=''
disabled = {(authCol.FrameworkCd196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFrameworkCd196 && touched.cFrameworkCd196 && <span className='form__form-group-error'>{errors.cFrameworkCd196}</span>}
</div>
</Col>
}
{(authCol.DevProgramPath196 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DevProgramPath196 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DevProgramPath196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DevProgramPath196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DevProgramPath196 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRulTierState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDevProgramPath196'
disabled = {(authCol.DevProgramPath196 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDevProgramPath196 && touched.cDevProgramPath196 && <span className='form__form-group-error'>{errors.cDevProgramPath196}</span>}
</div>
</Col>
}
{(authCol.IsDefault196 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIsDefault196'
onChange={handleChange}
defaultChecked={values.cIsDefault196}
disabled={(authCol.IsDefault196 || {}).readonly || !(authCol.IsDefault196 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IsDefault196 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IsDefault196 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IsDefault196 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IsDefault196 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RuleTierId196)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RuleTierId196));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmRulTierState) && <Skeleton height='43px' />) ||
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
  AdmRulTier: state.AdmRulTier,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmRulTierReduxObj.LoadPage.bind(AdmRulTierReduxObj) },
    { SavePage: AdmRulTierReduxObj.SavePage.bind(AdmRulTierReduxObj) },
    { DelMst: AdmRulTierReduxObj.DelMst.bind(AdmRulTierReduxObj) },
    { AddMst: AdmRulTierReduxObj.AddMst.bind(AdmRulTierReduxObj) },
//    { SearchMemberId64: AdmRulTierReduxObj.SearchActions.SearchMemberId64.bind(AdmRulTierReduxObj) },
//    { SearchCurrencyId64: AdmRulTierReduxObj.SearchActions.SearchCurrencyId64.bind(AdmRulTierReduxObj) },
//    { SearchCustomerJobId64: AdmRulTierReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmRulTierReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            