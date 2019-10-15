
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
import AdmMenuOptReduxObj, { ShowMstFilterApplied } from '../../redux/AdmMenuOpt';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmMenuOpt || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'MenuOptId248';
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
    const columnLabel = (this.props.AdmMenuOpt || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cMenuOptName248) { errors.cMenuOptName248 = (columnLabel.MenuOptName248 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmMenuOpt || {}).Mst || {};
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
        this.props.AdmMenuOpt,
        {
          MenuOptId248: values.cMenuOptId248|| '',
          MenuOptName248: values.cMenuOptName248|| '',
          MenuOptDesc248: values.cMenuOptDesc248|| '',
          MenuOptIco248: values.cMenuOptIco248|| '',
          MenuOptImg248: values.cMenuOptImg248|| '',
          ProjectId248: (values.cProjectId248|| {}).value || '',
          OvaRating248: values.cOvaRating248|| '',
          InputBy248: (values.cInputBy248|| {}).value || '',
          InputOn248: values.cInputOn248|| '',
          TopMenuCss248: values.cTopMenuCss248|| '',
          SidMenuCss248: values.cSidMenuCss248|| '',
          TopMenuJs248: values.cTopMenuJs248|| '',
          SidMenuJs248: values.cSidMenuJs248|| '',
          TopMenuIvk248: values.cTopMenuIvk248|| '',
          SidMenuIvk248: values.cSidMenuIvk248|| '',
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
    const AdmMenuOptState = this.props.AdmMenuOpt || {};
    const auxSystemLabels = AdmMenuOptState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).MenuOptId248;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmMenuOpt.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmMenuOpt.Label);
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
    const AdmMenuOptState = this.props.AdmMenuOpt || {};
    const auxSystemLabels = AdmMenuOptState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.MenuOptId248;
        this.props.DelMst(this.props.AdmMenuOpt, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmMenuOpt || {};
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
    const AdmMenuOptState = this.props.AdmMenuOpt || {};
    const auxSystemLabels = AdmMenuOptState.SystemLabel || {};
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
      if (!(this.props.AdmMenuOpt || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmMenuOpt || {};

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
    const AdmMenuOptState = this.props.AdmMenuOpt || {};

    if (AdmMenuOptState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmMenuOptState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmMenuOptReduxObj.GetScreenButtons(AdmMenuOptState) || {};
    const itemList = AdmMenuOptState.Dtl || [];
    const auxLabels = AdmMenuOptState.Label || {};
    const auxSystemLabels = AdmMenuOptState.SystemLabel || {};

    const columnLabel = AdmMenuOptState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmMenuOptState);
    const authRow = (AdmMenuOptState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmMenuOpt || {}).Mst || {});
    const currDtl = ((this.props.AdmMenuOpt || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.MenuOptId248));
    const selectList = AdmMenuOptReduxObj.SearchListToSelectList(AdmMenuOptState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const MenuOptId248 = currMst.MenuOptId248;
const MenuOptName248 = currMst.MenuOptName248;
const MenuOptDesc248 = currMst.MenuOptDesc248;
const MenuOptIco248 = currMst.MenuOptIco248;
const MenuOptImg248 = currMst.MenuOptImg248;
const ProjectId248List = AdmMenuOptReduxObj.ScreenDdlSelectors.ProjectId248(AdmMenuOptState);
const ProjectId248 = currMst.ProjectId248;
const OvaRating248 = currMst.OvaRating248;
const InputBy248List = AdmMenuOptReduxObj.ScreenDdlSelectors.InputBy248(AdmMenuOptState);
const InputBy248 = currMst.InputBy248;
const InputOn248 = currMst.InputOn248;
const TopMenuCss248 = currMst.TopMenuCss248;
const SidMenuCss248 = currMst.SidMenuCss248;
const TopMenuJs248 = currMst.TopMenuJs248;
const SidMenuJs248 = currMst.SidMenuJs248;
const TopMenuIvk248 = currMst.TopMenuIvk248;
const SidMenuIvk248 = currMst.SidMenuIvk248;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmMenuOptState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cMenuOptId248: MenuOptId248 || '',
                  cMenuOptName248: MenuOptName248 || '',
                  cMenuOptDesc248: MenuOptDesc248 || '',
                  cMenuOptIco248: MenuOptIco248 || '',
                  cMenuOptImg248: MenuOptImg248 || '',
                  cProjectId248: ProjectId248List.filter(obj => { return obj.key === ProjectId248 })[0],
                  cOvaRating248: OvaRating248 || '',
                  cInputBy248: InputBy248List.filter(obj => { return obj.key === InputBy248 })[0],
                  cInputOn248: InputOn248 || new Date(),
                  cTopMenuCss248: TopMenuCss248 || '',
                  cSidMenuCss248: SidMenuCss248 || '',
                  cTopMenuJs248: TopMenuJs248 || '',
                  cSidMenuJs248: SidMenuJs248 || '',
                  cTopMenuIvk248: TopMenuIvk248 || '',
                  cSidMenuIvk248: SidMenuIvk248 || '',
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
                                {(this.constructor.ShowSpinner(AdmMenuOptState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuOptId248)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuOptId248)) return null;
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
            {(authCol.MenuOptId248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuOptId248 || {}).ColumnHeader} {(columnLabel.MenuOptId248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuOptId248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuOptId248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuOptId248'
disabled = {(authCol.MenuOptId248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuOptId248 && touched.cMenuOptId248 && <span className='form__form-group-error'>{errors.cMenuOptId248}</span>}
</div>
</Col>
}
{(authCol.MenuOptName248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuOptName248 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MenuOptName248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuOptName248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuOptName248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuOptName248'
disabled = {(authCol.MenuOptName248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuOptName248 && touched.cMenuOptName248 && <span className='form__form-group-error'>{errors.cMenuOptName248}</span>}
</div>
</Col>
}
{(authCol.MenuOptDesc248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuOptDesc248 || {}).ColumnHeader} {(columnLabel.MenuOptDesc248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuOptDesc248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuOptDesc248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuOptDesc248'
disabled = {(authCol.MenuOptDesc248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuOptDesc248 && touched.cMenuOptDesc248 && <span className='form__form-group-error'>{errors.cMenuOptDesc248}</span>}
</div>
</Col>
}
{(authCol.MenuOptIco248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuOptIco248 || {}).ColumnHeader} {(columnLabel.MenuOptIco248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuOptIco248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuOptIco248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuOptIco248'
disabled = {(authCol.MenuOptIco248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuOptIco248 && touched.cMenuOptIco248 && <span className='form__form-group-error'>{errors.cMenuOptIco248}</span>}
</div>
</Col>
}
{(authCol.MenuOptImg248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MenuOptImg248 || {}).ColumnHeader} {(columnLabel.MenuOptImg248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MenuOptImg248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MenuOptImg248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMenuOptImg248'
disabled = {(authCol.MenuOptImg248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMenuOptImg248 && touched.cMenuOptImg248 && <span className='form__form-group-error'>{errors.cMenuOptImg248}</span>}
</div>
</Col>
}
{(authCol.ProjectId248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProjectId248 || {}).ColumnHeader} {(columnLabel.ProjectId248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProjectId248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProjectId248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cProjectId248'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cProjectId248')}
value={values.cProjectId248}
options={ProjectId248List}
placeholder=''
disabled = {(authCol.ProjectId248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProjectId248 && touched.cProjectId248 && <span className='form__form-group-error'>{errors.cProjectId248}</span>}
</div>
</Col>
}
{(authCol.OvaRating248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.OvaRating248 || {}).ColumnHeader} {(columnLabel.OvaRating248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.OvaRating248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.OvaRating248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cOvaRating248'
disabled = {(authCol.OvaRating248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cOvaRating248 && touched.cOvaRating248 && <span className='form__form-group-error'>{errors.cOvaRating248}</span>}
</div>
</Col>
}
{(authCol.InputBy248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InputBy248 || {}).ColumnHeader} {(columnLabel.InputBy248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InputBy248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InputBy248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cInputBy248'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cInputBy248')}
value={values.cInputBy248}
options={InputBy248List}
placeholder=''
disabled = {(authCol.InputBy248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cInputBy248 && touched.cInputBy248 && <span className='form__form-group-error'>{errors.cInputBy248}</span>}
</div>
</Col>
}
{(authCol.InputOn248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InputOn248 || {}).ColumnHeader} {(columnLabel.InputOn248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InputOn248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InputOn248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cInputOn248'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cInputOn248', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cInputOn248', true)}
value={values.cInputOn248}
selected={values.cInputOn248}
disabled = {(authCol.InputOn248 || {}).readonly ? true: false }/>
</div>
}
{errors.cInputOn248 && touched.cInputOn248 && <span className='form__form-group-error'>{errors.cInputOn248}</span>}
</div>
</Col>
}
{(authCol.TopMenuCss248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TopMenuCss248 || {}).ColumnHeader} {(columnLabel.TopMenuCss248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TopMenuCss248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TopMenuCss248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTopMenuCss248'
disabled = {(authCol.TopMenuCss248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTopMenuCss248 && touched.cTopMenuCss248 && <span className='form__form-group-error'>{errors.cTopMenuCss248}</span>}
</div>
</Col>
}
{(authCol.SidMenuCss248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SidMenuCss248 || {}).ColumnHeader} {(columnLabel.SidMenuCss248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SidMenuCss248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SidMenuCss248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSidMenuCss248'
disabled = {(authCol.SidMenuCss248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSidMenuCss248 && touched.cSidMenuCss248 && <span className='form__form-group-error'>{errors.cSidMenuCss248}</span>}
</div>
</Col>
}
{(authCol.TopMenuJs248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TopMenuJs248 || {}).ColumnHeader} {(columnLabel.TopMenuJs248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TopMenuJs248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TopMenuJs248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTopMenuJs248'
disabled = {(authCol.TopMenuJs248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTopMenuJs248 && touched.cTopMenuJs248 && <span className='form__form-group-error'>{errors.cTopMenuJs248}</span>}
</div>
</Col>
}
{(authCol.SidMenuJs248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SidMenuJs248 || {}).ColumnHeader} {(columnLabel.SidMenuJs248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SidMenuJs248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SidMenuJs248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSidMenuJs248'
disabled = {(authCol.SidMenuJs248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSidMenuJs248 && touched.cSidMenuJs248 && <span className='form__form-group-error'>{errors.cSidMenuJs248}</span>}
</div>
</Col>
}
{(authCol.TopMenuIvk248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TopMenuIvk248 || {}).ColumnHeader} {(columnLabel.TopMenuIvk248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TopMenuIvk248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TopMenuIvk248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTopMenuIvk248'
disabled = {(authCol.TopMenuIvk248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTopMenuIvk248 && touched.cTopMenuIvk248 && <span className='form__form-group-error'>{errors.cTopMenuIvk248}</span>}
</div>
</Col>
}
{(authCol.SidMenuIvk248 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SidMenuIvk248 || {}).ColumnHeader} {(columnLabel.SidMenuIvk248 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SidMenuIvk248 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SidMenuIvk248 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmMenuOptState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSidMenuIvk248'
disabled = {(authCol.SidMenuIvk248 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSidMenuIvk248 && touched.cSidMenuIvk248 && <span className='form__form-group-error'>{errors.cSidMenuIvk248}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).MenuOptId248)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).MenuOptId248));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmMenuOptState) && <Skeleton height='43px' />) ||
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
  AdmMenuOpt: state.AdmMenuOpt,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmMenuOptReduxObj.LoadPage.bind(AdmMenuOptReduxObj) },
    { SavePage: AdmMenuOptReduxObj.SavePage.bind(AdmMenuOptReduxObj) },
    { DelMst: AdmMenuOptReduxObj.DelMst.bind(AdmMenuOptReduxObj) },
    { AddMst: AdmMenuOptReduxObj.AddMst.bind(AdmMenuOptReduxObj) },
//    { SearchMemberId64: AdmMenuOptReduxObj.SearchActions.SearchMemberId64.bind(AdmMenuOptReduxObj) },
//    { SearchCurrencyId64: AdmMenuOptReduxObj.SearchActions.SearchCurrencyId64.bind(AdmMenuOptReduxObj) },
//    { SearchCustomerJobId64: AdmMenuOptReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmMenuOptReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            