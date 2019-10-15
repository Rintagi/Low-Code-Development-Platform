
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
import AdmAtRowAuthReduxObj, { ShowMstFilterApplied } from '../../redux/AdmAtRowAuth';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmAtRowAuth || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RowAuthId236';
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
    const columnLabel = (this.props.AdmAtRowAuth || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRowAuthName236) { errors.cRowAuthName236 = (columnLabel.RowAuthName236 || {}).ErrMessage;}
if (isEmptyId((values.cAllowSel236 || {}).value)) { errors.cAllowSel236 = (columnLabel.AllowSel236 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmAtRowAuth || {}).Mst || {};
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
        this.props.AdmAtRowAuth,
        {
          RowAuthId236: values.cRowAuthId236|| '',
          RowAuthName236: values.cRowAuthName236|| '',
          OvrideId236: (values.cOvrideId236|| {}).value || '',
          AllowSel236: (values.cAllowSel236|| {}).value || '',
          AllowAdd236: values.cAllowAdd236 ? 'Y' : 'N',
          AllowUpd236: values.cAllowUpd236 ? 'Y' : 'N',
          AllowDel236: values.cAllowDel236 ? 'Y' : 'N',
          SysAdmin236: values.cSysAdmin236 ? 'Y' : 'N',
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
    const AdmAtRowAuthState = this.props.AdmAtRowAuth || {};
    const auxSystemLabels = AdmAtRowAuthState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RowAuthId236;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmAtRowAuth.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmAtRowAuth.Label);
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
    const AdmAtRowAuthState = this.props.AdmAtRowAuth || {};
    const auxSystemLabels = AdmAtRowAuthState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RowAuthId236;
        this.props.DelMst(this.props.AdmAtRowAuth, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmAtRowAuth || {};
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
    const AdmAtRowAuthState = this.props.AdmAtRowAuth || {};
    const auxSystemLabels = AdmAtRowAuthState.SystemLabel || {};
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
      if (!(this.props.AdmAtRowAuth || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmAtRowAuth || {};

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
    const AdmAtRowAuthState = this.props.AdmAtRowAuth || {};

    if (AdmAtRowAuthState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmAtRowAuthState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmAtRowAuthReduxObj.GetScreenButtons(AdmAtRowAuthState) || {};
    const itemList = AdmAtRowAuthState.Dtl || [];
    const auxLabels = AdmAtRowAuthState.Label || {};
    const auxSystemLabels = AdmAtRowAuthState.SystemLabel || {};

    const columnLabel = AdmAtRowAuthState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmAtRowAuthState);
    const authRow = (AdmAtRowAuthState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmAtRowAuth || {}).Mst || {});
    const currDtl = ((this.props.AdmAtRowAuth || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RowAuthId236));
    const selectList = AdmAtRowAuthReduxObj.SearchListToSelectList(AdmAtRowAuthState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RowAuthId236 = currMst.RowAuthId236;
const RowAuthName236 = currMst.RowAuthName236;
const OvrideId236List = AdmAtRowAuthReduxObj.ScreenDdlSelectors.OvrideId236(AdmAtRowAuthState);
const OvrideId236 = currMst.OvrideId236;
const AllowSel236List = AdmAtRowAuthReduxObj.ScreenDdlSelectors.AllowSel236(AdmAtRowAuthState);
const AllowSel236 = currMst.AllowSel236;
const AllowAdd236 = currMst.AllowAdd236;
const AllowUpd236 = currMst.AllowUpd236;
const AllowDel236 = currMst.AllowDel236;
const SysAdmin236 = currMst.SysAdmin236;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmAtRowAuthState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRowAuthId236: RowAuthId236 || '',
                  cRowAuthName236: RowAuthName236 || '',
                  cOvrideId236: OvrideId236List.filter(obj => { return obj.key === OvrideId236 })[0],
                  cAllowSel236: AllowSel236List.filter(obj => { return obj.key === AllowSel236 })[0],
                  cAllowAdd236: AllowAdd236 === 'Y',
                  cAllowUpd236: AllowUpd236 === 'Y',
                  cAllowDel236: AllowDel236 === 'Y',
                  cSysAdmin236: SysAdmin236 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmAtRowAuthState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RowAuthId236)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RowAuthId236)) return null;
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
            {(authCol.RowAuthId236 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowAuthId236 || {}).ColumnHeader} {(columnLabel.RowAuthId236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowAuthId236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowAuthId236 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowAuthId236'
disabled = {(authCol.RowAuthId236 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowAuthId236 && touched.cRowAuthId236 && <span className='form__form-group-error'>{errors.cRowAuthId236}</span>}
</div>
</Col>
}
{(authCol.RowAuthName236 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowAuthName236 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RowAuthName236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowAuthName236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowAuthName236 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowAuthName236'
disabled = {(authCol.RowAuthName236 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowAuthName236 && touched.cRowAuthName236 && <span className='form__form-group-error'>{errors.cRowAuthName236}</span>}
</div>
</Col>
}
{(authCol.OvrideId236 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.OvrideId236 || {}).ColumnHeader} {(columnLabel.OvrideId236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OvrideId236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OvrideId236 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cOvrideId236'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cOvrideId236')}
value={values.cOvrideId236}
options={OvrideId236List}
placeholder=''
disabled = {(authCol.OvrideId236 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cOvrideId236 && touched.cOvrideId236 && <span className='form__form-group-error'>{errors.cOvrideId236}</span>}
</div>
</Col>
}
{(authCol.AllowSel236 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AllowSel236 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.AllowSel236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowSel236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowSel236 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmAtRowAuthState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cAllowSel236'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cAllowSel236')}
value={values.cAllowSel236}
options={AllowSel236List}
placeholder=''
disabled = {(authCol.AllowSel236 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAllowSel236 && touched.cAllowSel236 && <span className='form__form-group-error'>{errors.cAllowSel236}</span>}
</div>
</Col>
}
{(authCol.AllowAdd236 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAllowAdd236'
onChange={handleChange}
defaultChecked={values.cAllowAdd236}
disabled={(authCol.AllowAdd236 || {}).readonly || !(authCol.AllowAdd236 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AllowAdd236 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AllowAdd236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowAdd236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowAdd236 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AllowUpd236 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAllowUpd236'
onChange={handleChange}
defaultChecked={values.cAllowUpd236}
disabled={(authCol.AllowUpd236 || {}).readonly || !(authCol.AllowUpd236 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AllowUpd236 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AllowUpd236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowUpd236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowUpd236 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AllowDel236 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cAllowDel236'
onChange={handleChange}
defaultChecked={values.cAllowDel236}
disabled={(authCol.AllowDel236 || {}).readonly || !(authCol.AllowDel236 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.AllowDel236 || {}).ColumnHeader}</span>
</label>
{(columnLabel.AllowDel236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AllowDel236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AllowDel236 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.SysAdmin236 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cSysAdmin236'
onChange={handleChange}
defaultChecked={values.cSysAdmin236}
disabled={(authCol.SysAdmin236 || {}).readonly || !(authCol.SysAdmin236 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.SysAdmin236 || {}).ColumnHeader}</span>
</label>
{(columnLabel.SysAdmin236 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SysAdmin236 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SysAdmin236 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RowAuthId236)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RowAuthId236));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmAtRowAuthState) && <Skeleton height='43px' />) ||
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
  AdmAtRowAuth: state.AdmAtRowAuth,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmAtRowAuthReduxObj.LoadPage.bind(AdmAtRowAuthReduxObj) },
    { SavePage: AdmAtRowAuthReduxObj.SavePage.bind(AdmAtRowAuthReduxObj) },
    { DelMst: AdmAtRowAuthReduxObj.DelMst.bind(AdmAtRowAuthReduxObj) },
    { AddMst: AdmAtRowAuthReduxObj.AddMst.bind(AdmAtRowAuthReduxObj) },
//    { SearchMemberId64: AdmAtRowAuthReduxObj.SearchActions.SearchMemberId64.bind(AdmAtRowAuthReduxObj) },
//    { SearchCurrencyId64: AdmAtRowAuthReduxObj.SearchActions.SearchCurrencyId64.bind(AdmAtRowAuthReduxObj) },
//    { SearchCustomerJobId64: AdmAtRowAuthReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmAtRowAuthReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            