
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
import AdmPageObjReduxObj, { ShowMstFilterApplied } from '../../redux/AdmPageObj';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmPageObj || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'PageObjId1277';
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

GroupRowId1277InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchGroupRowId1277(v, filterBy);}}
GroupColId1277InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchGroupColId1277(v, filterBy);}}
LinkTypeCd1277InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchLinkTypeCd1277(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmPageObj || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cSectionCd1277 || {}).value)) { errors.cSectionCd1277 = (columnLabel.SectionCd1277 || {}).ErrMessage;}
if (isEmptyId((values.cLinkTypeCd1277 || {}).value)) { errors.cLinkTypeCd1277 = (columnLabel.LinkTypeCd1277 || {}).ErrMessage;}
if (!values.cPageObjOrd1277) { errors.cPageObjOrd1277 = (columnLabel.PageObjOrd1277 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmPageObj || {}).Mst || {};
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
        this.props.AdmPageObj,
        {
          PageObjId1277: values.cPageObjId1277|| '',
          SectionCd1277: (values.cSectionCd1277|| {}).value || '',
          GroupRowId1277: (values.cGroupRowId1277|| {}).value || '',
          GroupColId1277: (values.cGroupColId1277|| {}).value || '',
          LinkTypeCd1277: (values.cLinkTypeCd1277|| {}).value || '',
          PageObjOrd1277: values.cPageObjOrd1277|| '',
          SctGrpRow1277: values.cSctGrpRow1277|| '',
          SctGrpCol1277: values.cSctGrpCol1277|| '',
          PageObjCss1277: values.cPageObjCss1277|| '',
          PageObjSrp1277: values.cPageObjSrp1277|| '',
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
    const AdmPageObjState = this.props.AdmPageObj || {};
    const auxSystemLabels = AdmPageObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).PageObjId1277;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmPageObj.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmPageObj.Label);
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
    const AdmPageObjState = this.props.AdmPageObj || {};
    const auxSystemLabels = AdmPageObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.PageObjId1277;
        this.props.DelMst(this.props.AdmPageObj, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmPageObj || {};
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
    const AdmPageObjState = this.props.AdmPageObj || {};
    const auxSystemLabels = AdmPageObjState.SystemLabel || {};
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
      if (!(this.props.AdmPageObj || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmPageObj || {};

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
    const AdmPageObjState = this.props.AdmPageObj || {};

    if (AdmPageObjState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmPageObjState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmPageObjReduxObj.GetScreenButtons(AdmPageObjState) || {};
    const itemList = AdmPageObjState.Dtl || [];
    const auxLabels = AdmPageObjState.Label || {};
    const auxSystemLabels = AdmPageObjState.SystemLabel || {};

    const columnLabel = AdmPageObjState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmPageObjState);
    const authRow = (AdmPageObjState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmPageObj || {}).Mst || {});
    const currDtl = ((this.props.AdmPageObj || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.PageObjId1277));
    const selectList = AdmPageObjReduxObj.SearchListToSelectList(AdmPageObjState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const PageObjId1277 = currMst.PageObjId1277;
const SectionCd1277List = AdmPageObjReduxObj.ScreenDdlSelectors.SectionCd1277(AdmPageObjState);
const SectionCd1277 = currMst.SectionCd1277;
const GroupRowId1277List = AdmPageObjReduxObj.ScreenDdlSelectors.GroupRowId1277(AdmPageObjState);
const GroupRowId1277 = currMst.GroupRowId1277;
const GroupColId1277List = AdmPageObjReduxObj.ScreenDdlSelectors.GroupColId1277(AdmPageObjState);
const GroupColId1277 = currMst.GroupColId1277;
const LinkTypeCd1277List = AdmPageObjReduxObj.ScreenDdlSelectors.LinkTypeCd1277(AdmPageObjState);
const LinkTypeCd1277 = currMst.LinkTypeCd1277;
const PageObjOrd1277 = currMst.PageObjOrd1277;
const SctGrpRow1277 = currMst.SctGrpRow1277;
const SctGrpCol1277 = currMst.SctGrpCol1277;
const PageObjCss1277 = currMst.PageObjCss1277;
const PageObjSrp1277 = currMst.PageObjSrp1277;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmPageObjState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cPageObjId1277: PageObjId1277 || '',
                  cSectionCd1277: SectionCd1277List.filter(obj => { return obj.key === SectionCd1277 })[0],
                  cGroupRowId1277: GroupRowId1277List.filter(obj => { return obj.key === GroupRowId1277 })[0],
                  cGroupColId1277: GroupColId1277List.filter(obj => { return obj.key === GroupColId1277 })[0],
                  cLinkTypeCd1277: LinkTypeCd1277List.filter(obj => { return obj.key === LinkTypeCd1277 })[0],
                  cPageObjOrd1277: PageObjOrd1277 || '',
                  cSctGrpRow1277: SctGrpRow1277 || '',
                  cSctGrpCol1277: SctGrpCol1277 || '',
                  cPageObjCss1277: PageObjCss1277 || '',
                  cPageObjSrp1277: PageObjSrp1277 || '',
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
                                {(this.constructor.ShowSpinner(AdmPageObjState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).PageObjId1277)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).PageObjId1277)) return null;
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
            {(authCol.PageObjId1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageObjId1277 || {}).ColumnHeader} {(columnLabel.PageObjId1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageObjId1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageObjId1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageObjId1277'
disabled = {(authCol.PageObjId1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageObjId1277 && touched.cPageObjId1277 && <span className='form__form-group-error'>{errors.cPageObjId1277}</span>}
</div>
</Col>
}
{(authCol.SectionCd1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SectionCd1277 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SectionCd1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SectionCd1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SectionCd1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSectionCd1277'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSectionCd1277')}
value={values.cSectionCd1277}
options={SectionCd1277List}
placeholder=''
disabled = {(authCol.SectionCd1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSectionCd1277 && touched.cSectionCd1277 && <span className='form__form-group-error'>{errors.cSectionCd1277}</span>}
</div>
</Col>
}
{(authCol.GroupRowId1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupRowId1277 || {}).ColumnHeader} {(columnLabel.GroupRowId1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupRowId1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupRowId1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cGroupRowId1277'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupRowId1277', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupRowId1277', true)}
onInputChange={this.GroupRowId1277InputChange()}
value={values.cGroupRowId1277}
defaultSelected={GroupRowId1277List.filter(obj => { return obj.key === GroupRowId1277 })}
options={GroupRowId1277List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.GroupRowId1277 || {}).readonly ? true: false }/>
</div>
}
{errors.cGroupRowId1277 && touched.cGroupRowId1277 && <span className='form__form-group-error'>{errors.cGroupRowId1277}</span>}
</div>
</Col>
}
{(authCol.GroupColId1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupColId1277 || {}).ColumnHeader} {(columnLabel.GroupColId1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupColId1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupColId1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cGroupColId1277'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupColId1277', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupColId1277', true)}
onInputChange={this.GroupColId1277InputChange()}
value={values.cGroupColId1277}
defaultSelected={GroupColId1277List.filter(obj => { return obj.key === GroupColId1277 })}
options={GroupColId1277List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.GroupColId1277 || {}).readonly ? true: false }/>
</div>
}
{errors.cGroupColId1277 && touched.cGroupColId1277 && <span className='form__form-group-error'>{errors.cGroupColId1277}</span>}
</div>
</Col>
}
{(authCol.LinkTypeCd1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LinkTypeCd1277 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LinkTypeCd1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LinkTypeCd1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LinkTypeCd1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cLinkTypeCd1277'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cLinkTypeCd1277', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cLinkTypeCd1277', true)}
onInputChange={this.LinkTypeCd1277InputChange()}
value={values.cLinkTypeCd1277}
defaultSelected={LinkTypeCd1277List.filter(obj => { return obj.key === LinkTypeCd1277 })}
options={LinkTypeCd1277List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.LinkTypeCd1277 || {}).readonly ? true: false }/>
</div>
}
{errors.cLinkTypeCd1277 && touched.cLinkTypeCd1277 && <span className='form__form-group-error'>{errors.cLinkTypeCd1277}</span>}
</div>
</Col>
}
{(authCol.PageObjOrd1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageObjOrd1277 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.PageObjOrd1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageObjOrd1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageObjOrd1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageObjOrd1277'
disabled = {(authCol.PageObjOrd1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageObjOrd1277 && touched.cPageObjOrd1277 && <span className='form__form-group-error'>{errors.cPageObjOrd1277}</span>}
</div>
</Col>
}
{(authCol.SctGrpRow1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpRow1277 || {}).ColumnHeader} {(columnLabel.SctGrpRow1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpRow1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpRow1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpRow1277'
disabled = {(authCol.SctGrpRow1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpRow1277 && touched.cSctGrpRow1277 && <span className='form__form-group-error'>{errors.cSctGrpRow1277}</span>}
</div>
</Col>
}
{(authCol.SctGrpCol1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpCol1277 || {}).ColumnHeader} {(columnLabel.SctGrpCol1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpCol1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpCol1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpCol1277'
disabled = {(authCol.SctGrpCol1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpCol1277 && touched.cSctGrpCol1277 && <span className='form__form-group-error'>{errors.cSctGrpCol1277}</span>}
</div>
</Col>
}
{(authCol.PageObjCss1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageObjCss1277 || {}).ColumnHeader} {(columnLabel.PageObjCss1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageObjCss1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageObjCss1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageObjCss1277'
disabled = {(authCol.PageObjCss1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageObjCss1277 && touched.cPageObjCss1277 && <span className='form__form-group-error'>{errors.cPageObjCss1277}</span>}
</div>
</Col>
}
{(authCol.PageObjSrp1277 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PageObjSrp1277 || {}).ColumnHeader} {(columnLabel.PageObjSrp1277 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PageObjSrp1277 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PageObjSrp1277 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPageObjSrp1277'
disabled = {(authCol.PageObjSrp1277 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPageObjSrp1277 && touched.cPageObjSrp1277 && <span className='form__form-group-error'>{errors.cPageObjSrp1277}</span>}
</div>
</Col>
}
{(authCol.BtnDefault || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BtnDefault || {}).ColumnHeader} {(columnLabel.BtnDefault || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BtnDefault || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BtnDefault || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.BtnHeader || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BtnHeader || {}).ColumnHeader} {(columnLabel.BtnHeader || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BtnHeader || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BtnHeader || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.BtnFooter || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BtnFooter || {}).ColumnHeader} {(columnLabel.BtnFooter || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BtnFooter || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BtnFooter || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.BtnSidebar || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmPageObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BtnSidebar || {}).ColumnHeader} {(columnLabel.BtnSidebar || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BtnSidebar || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BtnSidebar || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).PageObjId1277)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).PageObjId1277));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmPageObjState) && <Skeleton height='43px' />) ||
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
  AdmPageObj: state.AdmPageObj,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmPageObjReduxObj.LoadPage.bind(AdmPageObjReduxObj) },
    { SavePage: AdmPageObjReduxObj.SavePage.bind(AdmPageObjReduxObj) },
    { DelMst: AdmPageObjReduxObj.DelMst.bind(AdmPageObjReduxObj) },
    { AddMst: AdmPageObjReduxObj.AddMst.bind(AdmPageObjReduxObj) },
//    { SearchMemberId64: AdmPageObjReduxObj.SearchActions.SearchMemberId64.bind(AdmPageObjReduxObj) },
//    { SearchCurrencyId64: AdmPageObjReduxObj.SearchActions.SearchCurrencyId64.bind(AdmPageObjReduxObj) },
//    { SearchCustomerJobId64: AdmPageObjReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmPageObjReduxObj) },
{ SearchGroupRowId1277: AdmPageObjReduxObj.SearchActions.SearchGroupRowId1277.bind(AdmPageObjReduxObj) },
{ SearchGroupColId1277: AdmPageObjReduxObj.SearchActions.SearchGroupColId1277.bind(AdmPageObjReduxObj) },
{ SearchLinkTypeCd1277: AdmPageObjReduxObj.SearchActions.SearchLinkTypeCd1277.bind(AdmPageObjReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            