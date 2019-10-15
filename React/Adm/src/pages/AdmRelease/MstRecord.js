
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
import AdmReleaseReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRelease';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRelease || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ReleaseId191';
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
    const columnLabel = (this.props.AdmRelease || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cReleaseName191) { errors.cReleaseName191 = (columnLabel.ReleaseName191 || {}).ErrMessage;}
if (isEmptyId((values.cReleaseOs191 || {}).value)) { errors.cReleaseOs191 = (columnLabel.ReleaseOs191 || {}).ErrMessage;}
if (isEmptyId((values.cEntityId191 || {}).value)) { errors.cEntityId191 = (columnLabel.EntityId191 || {}).ErrMessage;}
if (isEmptyId((values.cReleaseTypeId191 || {}).value)) { errors.cReleaseTypeId191 = (columnLabel.ReleaseTypeId191 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRelease || {}).Mst || {};
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
        this.props.AdmRelease,
        {
          ReleaseId191: values.cReleaseId191|| '',
          ReleaseName191: values.cReleaseName191|| '',
          ReleaseBuild191: values.cReleaseBuild191|| '',
          ReleaseDate191: values.cReleaseDate191|| '',
          ReleaseOs191: (values.cReleaseOs191|| {}).value || '',
          EntityId191: (values.cEntityId191|| {}).value || '',
          ReleaseTypeId191: (values.cReleaseTypeId191|| {}).value || '',
          DeployPath199: values.cDeployPath199|| '',
          TarScriptAft191: values.cTarScriptAft191|| '',
          ReadMe191: values.cReadMe191|| '',
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ReleaseId191;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRelease.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRelease.Label);
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ReleaseId191;
        this.props.DelMst(this.props.AdmRelease, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRelease || {};
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
    const AdmReleaseState = this.props.AdmRelease || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};
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
      if (!(this.props.AdmRelease || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRelease || {};

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
    const AdmReleaseState = this.props.AdmRelease || {};

    if (AdmReleaseState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmReleaseState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmReleaseReduxObj.GetScreenButtons(AdmReleaseState) || {};
    const itemList = AdmReleaseState.Dtl || [];
    const auxLabels = AdmReleaseState.Label || {};
    const auxSystemLabels = AdmReleaseState.SystemLabel || {};

    const columnLabel = AdmReleaseState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmReleaseState);
    const authRow = (AdmReleaseState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRelease || {}).Mst || {});
    const currDtl = ((this.props.AdmRelease || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ReleaseId191));
    const selectList = AdmReleaseReduxObj.SearchListToSelectList(AdmReleaseState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ReleaseId191 = currMst.ReleaseId191;
const ReleaseName191 = currMst.ReleaseName191;
const ReleaseBuild191 = currMst.ReleaseBuild191;
const ReleaseDate191 = currMst.ReleaseDate191;
const ReleaseOs191List = AdmReleaseReduxObj.ScreenDdlSelectors.ReleaseOs191(AdmReleaseState);
const ReleaseOs191 = currMst.ReleaseOs191;
const EntityId191List = AdmReleaseReduxObj.ScreenDdlSelectors.EntityId191(AdmReleaseState);
const EntityId191 = currMst.EntityId191;
const ReleaseTypeId191List = AdmReleaseReduxObj.ScreenDdlSelectors.ReleaseTypeId191(AdmReleaseState);
const ReleaseTypeId191 = currMst.ReleaseTypeId191;
const DeployPath199 = currMst.DeployPath199;
const TarScriptAft191 = currMst.TarScriptAft191;
const ReadMe191 = currMst.ReadMe191;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmReleaseState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cReleaseId191: ReleaseId191 || '',
                  cReleaseName191: ReleaseName191 || '',
                  cReleaseBuild191: ReleaseBuild191 || '',
                  cReleaseDate191: ReleaseDate191 || new Date(),
                  cReleaseOs191: ReleaseOs191List.filter(obj => { return obj.key === ReleaseOs191 })[0],
                  cEntityId191: EntityId191List.filter(obj => { return obj.key === EntityId191 })[0],
                  cReleaseTypeId191: ReleaseTypeId191List.filter(obj => { return obj.key === ReleaseTypeId191 })[0],
                  cDeployPath199: DeployPath199 || '',
                  cTarScriptAft191: TarScriptAft191 || '',
                  cReadMe191: ReadMe191 || '',
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
                                {(this.constructor.ShowSpinner(AdmReleaseState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191)) return null;
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
            {(authCol.ReleaseId191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseId191 || {}).ColumnHeader} {(columnLabel.ReleaseId191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseId191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseId191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReleaseId191'
disabled = {(authCol.ReleaseId191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReleaseId191 && touched.cReleaseId191 && <span className='form__form-group-error'>{errors.cReleaseId191}</span>}
</div>
</Col>
}
{(authCol.ReleaseName191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseName191 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReleaseName191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseName191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseName191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReleaseName191'
disabled = {(authCol.ReleaseName191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReleaseName191 && touched.cReleaseName191 && <span className='form__form-group-error'>{errors.cReleaseName191}</span>}
</div>
</Col>
}
{(authCol.ReleaseBuild191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseBuild191 || {}).ColumnHeader} {(columnLabel.ReleaseBuild191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseBuild191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseBuild191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReleaseBuild191'
disabled = {(authCol.ReleaseBuild191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReleaseBuild191 && touched.cReleaseBuild191 && <span className='form__form-group-error'>{errors.cReleaseBuild191}</span>}
</div>
</Col>
}
{(authCol.ReleaseDate191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseDate191 || {}).ColumnHeader} {(columnLabel.ReleaseDate191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseDate191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseDate191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cReleaseDate191'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cReleaseDate191', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cReleaseDate191', true)}
value={values.cReleaseDate191}
selected={values.cReleaseDate191}
disabled = {(authCol.ReleaseDate191 || {}).readonly ? true: false }/>
</div>
}
{errors.cReleaseDate191 && touched.cReleaseDate191 && <span className='form__form-group-error'>{errors.cReleaseDate191}</span>}
</div>
</Col>
}
{(authCol.ReleaseOs191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseOs191 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReleaseOs191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseOs191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseOs191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReleaseOs191'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReleaseOs191')}
value={values.cReleaseOs191}
options={ReleaseOs191List}
placeholder=''
disabled = {(authCol.ReleaseOs191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReleaseOs191 && touched.cReleaseOs191 && <span className='form__form-group-error'>{errors.cReleaseOs191}</span>}
</div>
</Col>
}
{(authCol.EntityId191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EntityId191 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.EntityId191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EntityId191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EntityId191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cEntityId191'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cEntityId191')}
value={values.cEntityId191}
options={EntityId191List}
placeholder=''
disabled = {(authCol.EntityId191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEntityId191 && touched.cEntityId191 && <span className='form__form-group-error'>{errors.cEntityId191}</span>}
</div>
</Col>
}
{(authCol.ReleaseTypeId191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReleaseTypeId191 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReleaseTypeId191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReleaseTypeId191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReleaseTypeId191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cReleaseTypeId191'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cReleaseTypeId191')}
value={values.cReleaseTypeId191}
options={ReleaseTypeId191List}
placeholder=''
disabled = {(authCol.ReleaseTypeId191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReleaseTypeId191 && touched.cReleaseTypeId191 && <span className='form__form-group-error'>{errors.cReleaseTypeId191}</span>}
</div>
</Col>
}
{(authCol.DeployPath199 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DeployPath199 || {}).ColumnHeader} {(columnLabel.DeployPath199 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DeployPath199 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DeployPath199 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDeployPath199'
disabled = {(authCol.DeployPath199 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDeployPath199 && touched.cDeployPath199 && <span className='form__form-group-error'>{errors.cDeployPath199}</span>}
</div>
</Col>
}
{(authCol.TarScriptAft191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TarScriptAft191 || {}).ColumnHeader} {(columnLabel.TarScriptAft191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TarScriptAft191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TarScriptAft191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTarScriptAft191'
disabled = {(authCol.TarScriptAft191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTarScriptAft191 && touched.cTarScriptAft191 && <span className='form__form-group-error'>{errors.cTarScriptAft191}</span>}
</div>
</Col>
}
{(authCol.ReadMe191 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReadMe191 || {}).ColumnHeader} {(columnLabel.ReadMe191 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReadMe191 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReadMe191 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReleaseState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReadMe191'
disabled = {(authCol.ReadMe191 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReadMe191 && touched.cReadMe191 && <span className='form__form-group-error'>{errors.cReadMe191}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReleaseId191)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ReleaseId191));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmReleaseState) && <Skeleton height='43px' />) ||
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
  AdmRelease: state.AdmRelease,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmReleaseReduxObj.LoadPage.bind(AdmReleaseReduxObj) },
    { SavePage: AdmReleaseReduxObj.SavePage.bind(AdmReleaseReduxObj) },
    { DelMst: AdmReleaseReduxObj.DelMst.bind(AdmReleaseReduxObj) },
    { AddMst: AdmReleaseReduxObj.AddMst.bind(AdmReleaseReduxObj) },
//    { SearchMemberId64: AdmReleaseReduxObj.SearchActions.SearchMemberId64.bind(AdmReleaseReduxObj) },
//    { SearchCurrencyId64: AdmReleaseReduxObj.SearchActions.SearchCurrencyId64.bind(AdmReleaseReduxObj) },
//    { SearchCustomerJobId64: AdmReleaseReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmReleaseReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            