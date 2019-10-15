
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
import AdmStaticPgReduxObj, { ShowMstFilterApplied } from '../../redux/AdmStaticPg';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmStaticPg || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'StaticPgId259';
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
    const columnLabel = (this.props.AdmStaticPg || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cStaticPgNm259) { errors.cStaticPgNm259 = (columnLabel.StaticPgNm259 || {}).ErrMessage;}
if (!values.cStaticPgTitle259) { errors.cStaticPgTitle259 = (columnLabel.StaticPgTitle259 || {}).ErrMessage;}
if (!values.cStaticPgHtm259) { errors.cStaticPgHtm259 = (columnLabel.StaticPgHtm259 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmStaticPg || {}).Mst || {};
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
        this.props.AdmStaticPg,
        {
          StaticPgId259: values.cStaticPgId259|| '',
          StaticPgNm259: values.cStaticPgNm259|| '',
          StaticPgTitle259: values.cStaticPgTitle259|| '',
          MasterPgFile259: values.cMasterPgFile259|| '',
          StaticCsId259: (values.cStaticCsId259|| {}).value || '',
          StaticJsId259: (values.cStaticJsId259|| {}).value || '',
          StaticPgUrl259: values.cStaticPgUrl259|| '',
          StaticMeta259: values.cStaticMeta259|| '',
          StaticPgHtm259: values.cStaticPgHtm259|| '',
          StaticPgCss259: values.cStaticPgCss259|| '',
          StaticPgJs259: values.cStaticPgJs259|| '',
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
    const AdmStaticPgState = this.props.AdmStaticPg || {};
    const auxSystemLabels = AdmStaticPgState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).StaticPgId259;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmStaticPg.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmStaticPg.Label);
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
    const AdmStaticPgState = this.props.AdmStaticPg || {};
    const auxSystemLabels = AdmStaticPgState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.StaticPgId259;
        this.props.DelMst(this.props.AdmStaticPg, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmStaticPg || {};
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
    const AdmStaticPgState = this.props.AdmStaticPg || {};
    const auxSystemLabels = AdmStaticPgState.SystemLabel || {};
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
      if (!(this.props.AdmStaticPg || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmStaticPg || {};

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
    const AdmStaticPgState = this.props.AdmStaticPg || {};

    if (AdmStaticPgState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmStaticPgState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmStaticPgReduxObj.GetScreenButtons(AdmStaticPgState) || {};
    const itemList = AdmStaticPgState.Dtl || [];
    const auxLabels = AdmStaticPgState.Label || {};
    const auxSystemLabels = AdmStaticPgState.SystemLabel || {};

    const columnLabel = AdmStaticPgState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmStaticPgState);
    const authRow = (AdmStaticPgState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmStaticPg || {}).Mst || {});
    const currDtl = ((this.props.AdmStaticPg || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.StaticPgId259));
    const selectList = AdmStaticPgReduxObj.SearchListToSelectList(AdmStaticPgState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const StaticPgId259 = currMst.StaticPgId259;
const StaticPgNm259 = currMst.StaticPgNm259;
const StaticPgTitle259 = currMst.StaticPgTitle259;
const MasterPgFile259 = currMst.MasterPgFile259;
const StaticCsId259List = AdmStaticPgReduxObj.ScreenDdlSelectors.StaticCsId259(AdmStaticPgState);
const StaticCsId259 = currMst.StaticCsId259;
const StaticJsId259List = AdmStaticPgReduxObj.ScreenDdlSelectors.StaticJsId259(AdmStaticPgState);
const StaticJsId259 = currMst.StaticJsId259;
const StaticPgUrl259 = currMst.StaticPgUrl259;
const StaticMeta259 = currMst.StaticMeta259;
const StaticPgHtm259 = currMst.StaticPgHtm259;
const StaticPgCss259 = currMst.StaticPgCss259;
const StaticPgJs259 = currMst.StaticPgJs259;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmStaticPgState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cStaticPgId259: StaticPgId259 || '',
                  cStaticPgNm259: StaticPgNm259 || '',
                  cStaticPgTitle259: StaticPgTitle259 || '',
                  cMasterPgFile259: MasterPgFile259 || '',
                  cStaticCsId259: StaticCsId259List.filter(obj => { return obj.key === StaticCsId259 })[0],
                  cStaticJsId259: StaticJsId259List.filter(obj => { return obj.key === StaticJsId259 })[0],
                  cStaticPgUrl259: StaticPgUrl259 || '',
                  cStaticMeta259: StaticMeta259 || '',
                  cStaticPgHtm259: StaticPgHtm259 || '',
                  cStaticPgCss259: StaticPgCss259 || '',
                  cStaticPgJs259: StaticPgJs259 || '',
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
                                {(this.constructor.ShowSpinner(AdmStaticPgState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).StaticPgId259)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).StaticPgId259)) return null;
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
            {(authCol.StaticPgId259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgId259 || {}).ColumnHeader} {(columnLabel.StaticPgId259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgId259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgId259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgId259'
disabled = {(authCol.StaticPgId259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgId259 && touched.cStaticPgId259 && <span className='form__form-group-error'>{errors.cStaticPgId259}</span>}
</div>
</Col>
}
{(authCol.StaticPgNm259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgNm259 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.StaticPgNm259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgNm259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgNm259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgNm259'
disabled = {(authCol.StaticPgNm259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgNm259 && touched.cStaticPgNm259 && <span className='form__form-group-error'>{errors.cStaticPgNm259}</span>}
</div>
</Col>
}
{(authCol.StaticPgTitle259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgTitle259 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.StaticPgTitle259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgTitle259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgTitle259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgTitle259'
disabled = {(authCol.StaticPgTitle259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgTitle259 && touched.cStaticPgTitle259 && <span className='form__form-group-error'>{errors.cStaticPgTitle259}</span>}
</div>
</Col>
}
{(authCol.MasterPgFile259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MasterPgFile259 || {}).ColumnHeader} {(columnLabel.MasterPgFile259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterPgFile259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterPgFile259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMasterPgFile259'
disabled = {(authCol.MasterPgFile259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMasterPgFile259 && touched.cMasterPgFile259 && <span className='form__form-group-error'>{errors.cMasterPgFile259}</span>}
</div>
</Col>
}
{(authCol.StaticCsId259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticCsId259 || {}).ColumnHeader} {(columnLabel.StaticCsId259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticCsId259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticCsId259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cStaticCsId259'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cStaticCsId259')}
value={values.cStaticCsId259}
options={StaticCsId259List}
placeholder=''
disabled = {(authCol.StaticCsId259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticCsId259 && touched.cStaticCsId259 && <span className='form__form-group-error'>{errors.cStaticCsId259}</span>}
</div>
</Col>
}
{(authCol.StaticJsId259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticJsId259 || {}).ColumnHeader} {(columnLabel.StaticJsId259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticJsId259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticJsId259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cStaticJsId259'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cStaticJsId259')}
value={values.cStaticJsId259}
options={StaticJsId259List}
placeholder=''
disabled = {(authCol.StaticJsId259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticJsId259 && touched.cStaticJsId259 && <span className='form__form-group-error'>{errors.cStaticJsId259}</span>}
</div>
</Col>
}
{(authCol.StaticPgUrl259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgUrl259 || {}).ColumnHeader} {(columnLabel.StaticPgUrl259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgUrl259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgUrl259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgUrl259'
disabled = {(authCol.StaticPgUrl259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgUrl259 && touched.cStaticPgUrl259 && <span className='form__form-group-error'>{errors.cStaticPgUrl259}</span>}
</div>
</Col>
}
{(authCol.StaticMeta259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticMeta259 || {}).ColumnHeader} {(columnLabel.StaticMeta259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticMeta259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticMeta259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticMeta259'
disabled = {(authCol.StaticMeta259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticMeta259 && touched.cStaticMeta259 && <span className='form__form-group-error'>{errors.cStaticMeta259}</span>}
</div>
</Col>
}
{(authCol.StaticPgHtm259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgHtm259 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.StaticPgHtm259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgHtm259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgHtm259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgHtm259'
disabled = {(authCol.StaticPgHtm259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgHtm259 && touched.cStaticPgHtm259 && <span className='form__form-group-error'>{errors.cStaticPgHtm259}</span>}
</div>
</Col>
}
{(authCol.StaticPgCss259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgCss259 || {}).ColumnHeader} {(columnLabel.StaticPgCss259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgCss259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgCss259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgCss259'
disabled = {(authCol.StaticPgCss259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgCss259 && touched.cStaticPgCss259 && <span className='form__form-group-error'>{errors.cStaticPgCss259}</span>}
</div>
</Col>
}
{(authCol.StaticPgJs259 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPgJs259 || {}).ColumnHeader} {(columnLabel.StaticPgJs259 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPgJs259 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPgJs259 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cStaticPgJs259'
disabled = {(authCol.StaticPgJs259 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cStaticPgJs259 && touched.cStaticPgJs259 && <span className='form__form-group-error'>{errors.cStaticPgJs259}</span>}
</div>
</Col>
}
{(authCol.StaticPHolder || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmStaticPgState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.StaticPHolder || {}).ColumnHeader} {(columnLabel.StaticPHolder || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.StaticPHolder || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.StaticPHolder || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).StaticPgId259)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).StaticPgId259));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmStaticPgState) && <Skeleton height='43px' />) ||
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
  AdmStaticPg: state.AdmStaticPg,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmStaticPgReduxObj.LoadPage.bind(AdmStaticPgReduxObj) },
    { SavePage: AdmStaticPgReduxObj.SavePage.bind(AdmStaticPgReduxObj) },
    { DelMst: AdmStaticPgReduxObj.DelMst.bind(AdmStaticPgReduxObj) },
    { AddMst: AdmStaticPgReduxObj.AddMst.bind(AdmStaticPgReduxObj) },
//    { SearchMemberId64: AdmStaticPgReduxObj.SearchActions.SearchMemberId64.bind(AdmStaticPgReduxObj) },
//    { SearchCurrencyId64: AdmStaticPgReduxObj.SearchActions.SearchCurrencyId64.bind(AdmStaticPgReduxObj) },
//    { SearchCustomerJobId64: AdmStaticPgReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmStaticPgReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            