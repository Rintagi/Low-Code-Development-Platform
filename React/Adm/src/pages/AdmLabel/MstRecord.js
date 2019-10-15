
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
import AdmLabelReduxObj, { ShowMstFilterApplied } from '../../redux/AdmLabel';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmLabel || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'LabelId215';
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

CultureId215InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureId215(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmLabel || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cCultureId215 || {}).value)) { errors.cCultureId215 = (columnLabel.CultureId215 || {}).ErrMessage;}
if (!values.cLabelCat215) { errors.cLabelCat215 = (columnLabel.LabelCat215 || {}).ErrMessage;}
if (!values.cLabelKey215) { errors.cLabelKey215 = (columnLabel.LabelKey215 || {}).ErrMessage;}
if (!values.cLabelText215) { errors.cLabelText215 = (columnLabel.LabelText215 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmLabel || {}).Mst || {};
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
        this.props.AdmLabel,
        {
          LabelId215: values.cLabelId215|| '',
          CultureId215: (values.cCultureId215|| {}).value || '',
          LabelCat215: values.cLabelCat215|| '',
          LabelKey215: values.cLabelKey215|| '',
          LabelText215: values.cLabelText215|| '',
          CompanyId215: (values.cCompanyId215|| {}).value || '',
          SortOrder215: values.cSortOrder215|| '',
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
    const AdmLabelState = this.props.AdmLabel || {};
    const auxSystemLabels = AdmLabelState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).LabelId215;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmLabel.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmLabel.Label);
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
    const AdmLabelState = this.props.AdmLabel || {};
    const auxSystemLabels = AdmLabelState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.LabelId215;
        this.props.DelMst(this.props.AdmLabel, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmLabel || {};
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
    const AdmLabelState = this.props.AdmLabel || {};
    const auxSystemLabels = AdmLabelState.SystemLabel || {};
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
      if (!(this.props.AdmLabel || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmLabel || {};

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
    const AdmLabelState = this.props.AdmLabel || {};

    if (AdmLabelState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmLabelState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmLabelReduxObj.GetScreenButtons(AdmLabelState) || {};
    const itemList = AdmLabelState.Dtl || [];
    const auxLabels = AdmLabelState.Label || {};
    const auxSystemLabels = AdmLabelState.SystemLabel || {};

    const columnLabel = AdmLabelState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmLabelState);
    const authRow = (AdmLabelState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmLabel || {}).Mst || {});
    const currDtl = ((this.props.AdmLabel || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.LabelId215));
    const selectList = AdmLabelReduxObj.SearchListToSelectList(AdmLabelState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const LabelId215 = currMst.LabelId215;
const CultureId215List = AdmLabelReduxObj.ScreenDdlSelectors.CultureId215(AdmLabelState);
const CultureId215 = currMst.CultureId215;
const LabelCat215 = currMst.LabelCat215;
const LabelKey215 = currMst.LabelKey215;
const LabelText215 = currMst.LabelText215;
const CompanyId215List = AdmLabelReduxObj.ScreenDdlSelectors.CompanyId215(AdmLabelState);
const CompanyId215 = currMst.CompanyId215;
const SortOrder215 = currMst.SortOrder215;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmLabelState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cLabelId215: LabelId215 || '',
                  cCultureId215: CultureId215List.filter(obj => { return obj.key === CultureId215 })[0],
                  cLabelCat215: LabelCat215 || '',
                  cLabelKey215: LabelKey215 || '',
                  cLabelText215: LabelText215 || '',
                  cCompanyId215: CompanyId215List.filter(obj => { return obj.key === CompanyId215 })[0],
                  cSortOrder215: SortOrder215 || '',
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
                                {(this.constructor.ShowSpinner(AdmLabelState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).LabelId215)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).LabelId215)) return null;
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
            {(authCol.LabelId215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelId215 || {}).ColumnHeader} {(columnLabel.LabelId215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelId215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelId215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelId215'
disabled = {(authCol.LabelId215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelId215 && touched.cLabelId215 && <span className='form__form-group-error'>{errors.cLabelId215}</span>}
</div>
</Col>
}
{(authCol.CultureId215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureId215 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureId215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureId215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureId215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureId215'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId215', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId215', true)}
onInputChange={this.CultureId215InputChange()}
value={values.cCultureId215}
defaultSelected={CultureId215List.filter(obj => { return obj.key === CultureId215 })}
options={CultureId215List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureId215 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureId215 && touched.cCultureId215 && <span className='form__form-group-error'>{errors.cCultureId215}</span>}
</div>
</Col>
}
{(authCol.LabelCat215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelCat215 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LabelCat215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelCat215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelCat215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelCat215'
disabled = {(authCol.LabelCat215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelCat215 && touched.cLabelCat215 && <span className='form__form-group-error'>{errors.cLabelCat215}</span>}
</div>
</Col>
}
{(authCol.LabelKey215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelKey215 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LabelKey215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelKey215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelKey215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelKey215'
disabled = {(authCol.LabelKey215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelKey215 && touched.cLabelKey215 && <span className='form__form-group-error'>{errors.cLabelKey215}</span>}
</div>
</Col>
}
{(authCol.LabelText215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelText215 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LabelText215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelText215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelText215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelText215'
disabled = {(authCol.LabelText215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelText215 && touched.cLabelText215 && <span className='form__form-group-error'>{errors.cLabelText215}</span>}
</div>
</Col>
}
{(authCol.CompanyId215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CompanyId215 || {}).ColumnHeader} {(columnLabel.CompanyId215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CompanyId215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CompanyId215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cCompanyId215'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cCompanyId215')}
value={values.cCompanyId215}
options={CompanyId215List}
placeholder=''
disabled = {(authCol.CompanyId215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCompanyId215 && touched.cCompanyId215 && <span className='form__form-group-error'>{errors.cCompanyId215}</span>}
</div>
</Col>
}
{(authCol.SortOrder215 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SortOrder215 || {}).ColumnHeader} {(columnLabel.SortOrder215 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SortOrder215 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SortOrder215 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSortOrder215'
disabled = {(authCol.SortOrder215 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSortOrder215 && touched.cSortOrder215 && <span className='form__form-group-error'>{errors.cSortOrder215}</span>}
</div>
</Col>
}
{(authCol.RemoveBtn || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmLabelState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RemoveBtn || {}).ColumnHeader} {(columnLabel.RemoveBtn || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RemoveBtn || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RemoveBtn || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).LabelId215)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).LabelId215));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmLabelState) && <Skeleton height='43px' />) ||
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
  AdmLabel: state.AdmLabel,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmLabelReduxObj.LoadPage.bind(AdmLabelReduxObj) },
    { SavePage: AdmLabelReduxObj.SavePage.bind(AdmLabelReduxObj) },
    { DelMst: AdmLabelReduxObj.DelMst.bind(AdmLabelReduxObj) },
    { AddMst: AdmLabelReduxObj.AddMst.bind(AdmLabelReduxObj) },
//    { SearchMemberId64: AdmLabelReduxObj.SearchActions.SearchMemberId64.bind(AdmLabelReduxObj) },
//    { SearchCurrencyId64: AdmLabelReduxObj.SearchActions.SearchCurrencyId64.bind(AdmLabelReduxObj) },
//    { SearchCustomerJobId64: AdmLabelReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmLabelReduxObj) },
{ SearchCultureId215: AdmLabelReduxObj.SearchActions.SearchCultureId215.bind(AdmLabelReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            