
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
import AdmSignupReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSignup';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmSignup || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'UsrId270';
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
    const columnLabel = (this.props.AdmSignup || {}).ColumnLabel || {};
    /* standard field validation */

    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmSignup || {}).Mst || {};
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
        this.props.AdmSignup,
        {
          UsrId270: values.cUsrId270|| '',
          UsrName270: values.cUsrName270|| '',
          LoginName270: values.cLoginName270|| '',
          UsrEmail270: values.cUsrEmail270|| '',
          UsrPassword270: values.cUsrPassword270|| '',
          ConfirmationToken: values.cConfirmationToken|| '',
          Token: values.cToken|| '',
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
    const AdmSignupState = this.props.AdmSignup || {};
    const auxSystemLabels = AdmSignupState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).UsrId270;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmSignup.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmSignup.Label);
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
    const AdmSignupState = this.props.AdmSignup || {};
    const auxSystemLabels = AdmSignupState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.UsrId270;
        this.props.DelMst(this.props.AdmSignup, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmSignup || {};
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
    const AdmSignupState = this.props.AdmSignup || {};
    const auxSystemLabels = AdmSignupState.SystemLabel || {};
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
      if (!(this.props.AdmSignup || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmSignup || {};

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
    const AdmSignupState = this.props.AdmSignup || {};

    if (AdmSignupState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmSignupState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmSignupReduxObj.GetScreenButtons(AdmSignupState) || {};
    const itemList = AdmSignupState.Dtl || [];
    const auxLabels = AdmSignupState.Label || {};
    const auxSystemLabels = AdmSignupState.SystemLabel || {};

    const columnLabel = AdmSignupState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmSignupState);
    const authRow = (AdmSignupState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmSignup || {}).Mst || {});
    const currDtl = ((this.props.AdmSignup || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.UsrId270));
    const selectList = AdmSignupReduxObj.SearchListToSelectList(AdmSignupState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const UsrId270 = currMst.UsrId270;
const UsrName270 = currMst.UsrName270;
const LoginName270 = currMst.LoginName270;
const UsrEmail270 = currMst.UsrEmail270;
const UsrPassword270 = currMst.UsrPassword270;
const ConfirmationToken = currMst.ConfirmationToken;
const Token = currMst.Token;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmSignupState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cUsrId270: UsrId270 || '',
                  cUsrName270: UsrName270 || '',
                  cLoginName270: LoginName270 || '',
                  cUsrEmail270: UsrEmail270 || '',
                  cUsrPassword270: UsrPassword270 || '',
                  cConfirmationToken: ConfirmationToken || '',
                  cToken: Token || '',
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
                                {(this.constructor.ShowSpinner(AdmSignupState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId270)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId270)) return null;
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
            {(authCol.DummyWhiteSpace7 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace7 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace7 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace7 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace7 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.SignUpTitle || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SignUpTitle || {}).ColumnHeader} {(columnLabel.SignUpTitle || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SignUpTitle || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SignUpTitle || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.SignUpTopMsg || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SignUpTopMsg || {}).ColumnHeader} {(columnLabel.SignUpTopMsg || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SignUpTopMsg || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SignUpTopMsg || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace8 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace8 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace8 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace8 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace8 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace1 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace1 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.UsrId270 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrId270 || {}).ColumnHeader} {(columnLabel.UsrId270 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrId270 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrId270 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrId270'
disabled = {(authCol.UsrId270 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrId270 && touched.cUsrId270 && <span className='form__form-group-error'>{errors.cUsrId270}</span>}
</div>
</Col>
}
{(authCol.UsrName270 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrName270 || {}).ColumnHeader} {(columnLabel.UsrName270 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrName270 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrName270 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrName270'
disabled = {(authCol.UsrName270 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrName270 && touched.cUsrName270 && <span className='form__form-group-error'>{errors.cUsrName270}</span>}
</div>
</Col>
}
{(authCol.LoginName270 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LoginName270 || {}).ColumnHeader} {(columnLabel.LoginName270 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LoginName270 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LoginName270 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLoginName270'
disabled = {(authCol.LoginName270 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLoginName270 && touched.cLoginName270 && <span className='form__form-group-error'>{errors.cLoginName270}</span>}
</div>
</Col>
}
{(authCol.UsrEmail270 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrEmail270 || {}).ColumnHeader} {(columnLabel.UsrEmail270 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrEmail270 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrEmail270 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrEmail270'
disabled = {(authCol.UsrEmail270 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrEmail270 && touched.cUsrEmail270 && <span className='form__form-group-error'>{errors.cUsrEmail270}</span>}
</div>
</Col>
}
{(authCol.UsrPassword270 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrPassword270 || {}).ColumnHeader} {(columnLabel.UsrPassword270 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrPassword270 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrPassword270 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrPassword270'
disabled = {(authCol.UsrPassword270 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrPassword270 && touched.cUsrPassword270 && <span className='form__form-group-error'>{errors.cUsrPassword270}</span>}
</div>
</Col>
}
{(authCol.DummyWhiteSpace2 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace2 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace2 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace2 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace2 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace3 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace3 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.TokenMsg || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TokenMsg || {}).ColumnHeader} {(columnLabel.TokenMsg || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TokenMsg || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TokenMsg || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.ConfirmationToken || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ConfirmationToken || {}).ColumnHeader} {(columnLabel.ConfirmationToken || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ConfirmationToken || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ConfirmationToken || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cConfirmationToken'
disabled = {(authCol.ConfirmationToken || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cConfirmationToken && touched.cConfirmationToken && <span className='form__form-group-error'>{errors.cConfirmationToken}</span>}
</div>
</Col>
}
{(authCol.Token || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Token || {}).ColumnHeader} {(columnLabel.Token || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Token || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Token || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cToken'
disabled = {(authCol.Token || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cToken && touched.cToken && <span className='form__form-group-error'>{errors.cToken}</span>}
</div>
</Col>
}
{(authCol.DummyWhiteSpace9 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace9 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace9 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace9 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace9 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.ResnedToken || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ResnedToken || {}).ColumnHeader} {(columnLabel.ResnedToken || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ResnedToken || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ResnedToken || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace4 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace4 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace4 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace4 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace4 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace5 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace5 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace5 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace5 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace5 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.Submit || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Submit || {}).ColumnHeader} {(columnLabel.Submit || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Submit || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Submit || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.SignUpBtn || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SignUpBtn || {}).ColumnHeader} {(columnLabel.SignUpBtn || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SignUpBtn || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SignUpBtn || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace6 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace6 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace6 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace6 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace6 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace10 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace10 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace10 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace10 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace10 || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.SignUpMsg || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SignUpMsg || {}).ColumnHeader} {(columnLabel.SignUpMsg || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SignUpMsg || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SignUpMsg || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.DummyWhiteSpace11 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSignupState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DummyWhiteSpace11 || {}).ColumnHeader} {(columnLabel.DummyWhiteSpace11 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DummyWhiteSpace11 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DummyWhiteSpace11 || {}).ToolTip} />
)}
</label>
}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId270)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).UsrId270));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmSignupState) && <Skeleton height='43px' />) ||
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
  AdmSignup: state.AdmSignup,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSignupReduxObj.LoadPage.bind(AdmSignupReduxObj) },
    { SavePage: AdmSignupReduxObj.SavePage.bind(AdmSignupReduxObj) },
    { DelMst: AdmSignupReduxObj.DelMst.bind(AdmSignupReduxObj) },
    { AddMst: AdmSignupReduxObj.AddMst.bind(AdmSignupReduxObj) },
//    { SearchMemberId64: AdmSignupReduxObj.SearchActions.SearchMemberId64.bind(AdmSignupReduxObj) },
//    { SearchCurrencyId64: AdmSignupReduxObj.SearchActions.SearchCurrencyId64.bind(AdmSignupReduxObj) },
//    { SearchCustomerJobId64: AdmSignupReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmSignupReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            