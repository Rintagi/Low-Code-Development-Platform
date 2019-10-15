
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
import AdmDataCatReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDataCat';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDataCat || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RptwizCatId181';
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

RptwizTypId181InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchRptwizTypId181(v, filterBy);}}
TableId181InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchTableId181(v, filterBy);}}
 SampleImage({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SampleImage';
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
    const columnLabel = (this.props.AdmDataCat || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cRptwizTypId181 || {}).value)) { errors.cRptwizTypId181 = (columnLabel.RptwizTypId181 || {}).ErrMessage;}
if (!values.cRptwizCatName181) { errors.cRptwizCatName181 = (columnLabel.RptwizCatName181 || {}).ErrMessage;}
if (!values.cCatDescription181) { errors.cCatDescription181 = (columnLabel.CatDescription181 || {}).ErrMessage;}
if (isEmptyId((values.cTableId181 || {}).value)) { errors.cTableId181 = (columnLabel.TableId181 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmDataCat || {}).Mst || {};
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
        this.props.AdmDataCat,
        {
          RptwizCatId181: values.cRptwizCatId181|| '',
          RptwizTypId181: (values.cRptwizTypId181|| {}).value || '',
          RptwizCatName181: values.cRptwizCatName181|| '',
          CatDescription181: values.cCatDescription181|| '',
          TableId181: (values.cTableId181|| {}).value || '',
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RptwizCatId181;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmDataCat.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmDataCat.Label);
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RptwizCatId181;
        this.props.DelMst(this.props.AdmDataCat, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmDataCat || {};
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
    const AdmDataCatState = this.props.AdmDataCat || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};
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
      if (!(this.props.AdmDataCat || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDataCat || {};

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
    const AdmDataCatState = this.props.AdmDataCat || {};

    if (AdmDataCatState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmDataCatState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmDataCatReduxObj.GetScreenButtons(AdmDataCatState) || {};
    const itemList = AdmDataCatState.Dtl || [];
    const auxLabels = AdmDataCatState.Label || {};
    const auxSystemLabels = AdmDataCatState.SystemLabel || {};

    const columnLabel = AdmDataCatState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmDataCatState);
    const authRow = (AdmDataCatState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmDataCat || {}).Mst || {});
    const currDtl = ((this.props.AdmDataCat || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RptwizCatId181));
    const selectList = AdmDataCatReduxObj.SearchListToSelectList(AdmDataCatState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RptwizCatId181 = currMst.RptwizCatId181;
const RptwizTypId181List = AdmDataCatReduxObj.ScreenDdlSelectors.RptwizTypId181(AdmDataCatState);
const RptwizTypId181 = currMst.RptwizTypId181;
const RptwizCatName181 = currMst.RptwizCatName181;
const CatDescription181 = currMst.CatDescription181;
const TableId181List = AdmDataCatReduxObj.ScreenDdlSelectors.TableId181(AdmDataCatState);
const TableId181 = currMst.TableId181;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmDataCatState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRptwizCatId181: RptwizCatId181 || '',
                  cRptwizTypId181: RptwizTypId181List.filter(obj => { return obj.key === RptwizTypId181 })[0],
                  cRptwizCatName181: RptwizCatName181 || '',
                  cCatDescription181: CatDescription181 || '',
                  cTableId181: TableId181List.filter(obj => { return obj.key === TableId181 })[0],
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
                                {(this.constructor.ShowSpinner(AdmDataCatState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181)) return null;
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
            {(authCol.RptwizCatId181 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptwizCatId181 || {}).ColumnHeader} {(columnLabel.RptwizCatId181 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptwizCatId181 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptwizCatId181 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptwizCatId181'
disabled = {(authCol.RptwizCatId181 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptwizCatId181 && touched.cRptwizCatId181 && <span className='form__form-group-error'>{errors.cRptwizCatId181}</span>}
</div>
</Col>
}
{(authCol.RptwizTypId181 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptwizTypId181 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptwizTypId181 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptwizTypId181 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptwizTypId181 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cRptwizTypId181'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cRptwizTypId181', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cRptwizTypId181', true)}
onInputChange={this.RptwizTypId181InputChange()}
value={values.cRptwizTypId181}
defaultSelected={RptwizTypId181List.filter(obj => { return obj.key === RptwizTypId181 })}
options={RptwizTypId181List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.RptwizTypId181 || {}).readonly ? true: false }/>
</div>
}
{errors.cRptwizTypId181 && touched.cRptwizTypId181 && <span className='form__form-group-error'>{errors.cRptwizTypId181}</span>}
</div>
</Col>
}
{(authCol.RptwizCatName181 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptwizCatName181 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptwizCatName181 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptwizCatName181 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptwizCatName181 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptwizCatName181'
disabled = {(authCol.RptwizCatName181 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptwizCatName181 && touched.cRptwizCatName181 && <span className='form__form-group-error'>{errors.cRptwizCatName181}</span>}
</div>
</Col>
}
{(authCol.CatDescription181 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CatDescription181 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CatDescription181 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CatDescription181 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CatDescription181 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cCatDescription181'
disabled = {(authCol.CatDescription181 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCatDescription181 && touched.cCatDescription181 && <span className='form__form-group-error'>{errors.cCatDescription181}</span>}
</div>
</Col>
}
{(authCol.TableId181 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableId181 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TableId181 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableId181 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableId181 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDataCatState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cTableId181'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cTableId181', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cTableId181', true)}
onInputChange={this.TableId181InputChange()}
value={values.cTableId181}
defaultSelected={TableId181List.filter(obj => { return obj.key === TableId181 })}
options={TableId181List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.TableId181 || {}).readonly ? true: false }/>
</div>
}
{errors.cTableId181 && touched.cTableId181 && <span className='form__form-group-error'>{errors.cTableId181}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SampleImage || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SampleImage || {}).readonly || !(authCol.SampleImage || {}).visible} onClick={this.SampleImage({ naviBar, submitForm, currMst })} >{auxLabels.SampleImage || (columnLabel.SampleImage || {}).ColumnName}</Button>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptwizCatId181)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RptwizCatId181));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmDataCatState) && <Skeleton height='43px' />) ||
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
  AdmDataCat: state.AdmDataCat,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmDataCatReduxObj.LoadPage.bind(AdmDataCatReduxObj) },
    { SavePage: AdmDataCatReduxObj.SavePage.bind(AdmDataCatReduxObj) },
    { DelMst: AdmDataCatReduxObj.DelMst.bind(AdmDataCatReduxObj) },
    { AddMst: AdmDataCatReduxObj.AddMst.bind(AdmDataCatReduxObj) },
//    { SearchMemberId64: AdmDataCatReduxObj.SearchActions.SearchMemberId64.bind(AdmDataCatReduxObj) },
//    { SearchCurrencyId64: AdmDataCatReduxObj.SearchActions.SearchCurrencyId64.bind(AdmDataCatReduxObj) },
//    { SearchCustomerJobId64: AdmDataCatReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmDataCatReduxObj) },
{ SearchRptwizTypId181: AdmDataCatReduxObj.SearchActions.SearchRptwizTypId181.bind(AdmDataCatReduxObj) },
{ SearchTableId181: AdmDataCatReduxObj.SearchActions.SearchTableId181.bind(AdmDataCatReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            