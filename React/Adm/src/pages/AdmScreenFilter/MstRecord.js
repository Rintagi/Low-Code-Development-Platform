
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
import AdmScreenFilterReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScreenFilter';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreenFilter || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ScreenFilterId86';
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

ScreenId86InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId86(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmScreenFilter || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cScreenId86 || {}).value)) { errors.cScreenId86 = (columnLabel.ScreenId86 || {}).ErrMessage;}
if (!values.cScreenFilterName86) { errors.cScreenFilterName86 = (columnLabel.ScreenFilterName86 || {}).ErrMessage;}
if (!values.cFilterClause86) { errors.cFilterClause86 = (columnLabel.FilterClause86 || {}).ErrMessage;}
if (!values.cFilterOrder86) { errors.cFilterOrder86 = (columnLabel.FilterOrder86 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmScreenFilter || {}).Mst || {};
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
        this.props.AdmScreenFilter,
        {
          ScreenFilterId86: values.cScreenFilterId86|| '',
          ScreenId86: (values.cScreenId86|| {}).value || '',
          ScreenFilterName86: values.cScreenFilterName86|| '',
          FilterClause86: values.cFilterClause86|| '',
          FilterOrder86: values.cFilterOrder86|| '',
          ApplyToMst86: values.cApplyToMst86 ? 'Y' : 'N',
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
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ScreenFilterId86;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmScreenFilter.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmScreenFilter.Label);
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
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ScreenFilterId86;
        this.props.DelMst(this.props.AdmScreenFilter, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmScreenFilter || {};
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
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
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
      if (!(this.props.AdmScreenFilter || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScreenFilter || {};

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
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};

    if (AdmScreenFilterState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmScreenFilterState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmScreenFilterReduxObj.GetScreenButtons(AdmScreenFilterState) || {};
    const itemList = AdmScreenFilterState.Dtl || [];
    const auxLabels = AdmScreenFilterState.Label || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};

    const columnLabel = AdmScreenFilterState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmScreenFilterState);
    const authRow = (AdmScreenFilterState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmScreenFilter || {}).Mst || {});
    const currDtl = ((this.props.AdmScreenFilter || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ScreenFilterId86));
    const selectList = AdmScreenFilterReduxObj.SearchListToSelectList(AdmScreenFilterState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ScreenFilterId86 = currMst.ScreenFilterId86;
const ScreenId86List = AdmScreenFilterReduxObj.ScreenDdlSelectors.ScreenId86(AdmScreenFilterState);
const ScreenId86 = currMst.ScreenId86;
const ScreenFilterName86 = currMst.ScreenFilterName86;
const FilterClause86 = currMst.FilterClause86;
const FilterOrder86 = currMst.FilterOrder86;
const ApplyToMst86 = currMst.ApplyToMst86;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmScreenFilterState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cScreenFilterId86: ScreenFilterId86 || '',
                  cScreenId86: ScreenId86List.filter(obj => { return obj.key === ScreenId86 })[0],
                  cScreenFilterName86: ScreenFilterName86 || '',
                  cFilterClause86: FilterClause86 || '',
                  cFilterOrder86: FilterOrder86 || '',
                  cApplyToMst86: ApplyToMst86 === 'Y',
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
                                {(this.constructor.ShowSpinner(AdmScreenFilterState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86)) return null;
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
            {(authCol.ScreenFilterId86 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenFilterId86 || {}).ColumnHeader} {(columnLabel.ScreenFilterId86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenFilterId86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenFilterId86 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenFilterId86'
disabled = {(authCol.ScreenFilterId86 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenFilterId86 && touched.cScreenFilterId86 && <span className='form__form-group-error'>{errors.cScreenFilterId86}</span>}
</div>
</Col>
}
{(authCol.ScreenId86 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId86 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenId86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId86 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId86'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId86', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId86', true)}
onInputChange={this.ScreenId86InputChange()}
value={values.cScreenId86}
defaultSelected={ScreenId86List.filter(obj => { return obj.key === ScreenId86 })}
options={ScreenId86List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId86 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId86 && touched.cScreenId86 && <span className='form__form-group-error'>{errors.cScreenId86}</span>}
</div>
</Col>
}
{(authCol.ScreenFilterName86 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenFilterName86 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenFilterName86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenFilterName86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenFilterName86 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenFilterName86'
disabled = {(authCol.ScreenFilterName86 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenFilterName86 && touched.cScreenFilterName86 && <span className='form__form-group-error'>{errors.cScreenFilterName86}</span>}
</div>
</Col>
}
{(authCol.FilterClause86 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FilterClause86 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.FilterClause86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FilterClause86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FilterClause86 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFilterClause86'
disabled = {(authCol.FilterClause86 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFilterClause86 && touched.cFilterClause86 && <span className='form__form-group-error'>{errors.cFilterClause86}</span>}
</div>
</Col>
}
{(authCol.FilterOrder86 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FilterOrder86 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.FilterOrder86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FilterOrder86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FilterOrder86 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenFilterState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFilterOrder86'
disabled = {(authCol.FilterOrder86 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFilterOrder86 && touched.cFilterOrder86 && <span className='form__form-group-error'>{errors.cFilterOrder86}</span>}
</div>
</Col>
}
{(authCol.ApplyToMst86 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cApplyToMst86'
onChange={handleChange}
defaultChecked={values.cApplyToMst86}
disabled={(authCol.ApplyToMst86 || {}).readonly || !(authCol.ApplyToMst86 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ApplyToMst86 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ApplyToMst86 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ApplyToMst86 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ApplyToMst86 || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ScreenFilterId86));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmScreenFilterState) && <Skeleton height='43px' />) ||
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
  AdmScreenFilter: state.AdmScreenFilter,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScreenFilterReduxObj.LoadPage.bind(AdmScreenFilterReduxObj) },
    { SavePage: AdmScreenFilterReduxObj.SavePage.bind(AdmScreenFilterReduxObj) },
    { DelMst: AdmScreenFilterReduxObj.DelMst.bind(AdmScreenFilterReduxObj) },
    { AddMst: AdmScreenFilterReduxObj.AddMst.bind(AdmScreenFilterReduxObj) },
//    { SearchMemberId64: AdmScreenFilterReduxObj.SearchActions.SearchMemberId64.bind(AdmScreenFilterReduxObj) },
//    { SearchCurrencyId64: AdmScreenFilterReduxObj.SearchActions.SearchCurrencyId64.bind(AdmScreenFilterReduxObj) },
//    { SearchCustomerJobId64: AdmScreenFilterReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmScreenFilterReduxObj) },
{ SearchScreenId86: AdmScreenFilterReduxObj.SearchActions.SearchScreenId86.bind(AdmScreenFilterReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            