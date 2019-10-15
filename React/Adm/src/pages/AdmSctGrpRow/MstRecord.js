
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
import AdmSctGrpRowReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSctGrpRow';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmSctGrpRow || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SctGrpRowId1283';
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
    const columnLabel = (this.props.AdmSctGrpRow || {}).ColumnLabel || {};
    /* standard field validation */

    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmSctGrpRow || {}).Mst || {};
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
        this.props.AdmSctGrpRow,
        {
          SctGrpRowId1283: values.cSctGrpRowId1283|| '',
          SectionCd1283: (values.cSectionCd1283|| {}).value || '',
          GroupRowId1283: (values.cGroupRowId1283|| {}).value || '',
          SctGrpRowCss1283: values.cSctGrpRowCss1283|| '',
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
    const AdmSctGrpRowState = this.props.AdmSctGrpRow || {};
    const auxSystemLabels = AdmSctGrpRowState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SctGrpRowId1283;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmSctGrpRow.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmSctGrpRow.Label);
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
    const AdmSctGrpRowState = this.props.AdmSctGrpRow || {};
    const auxSystemLabels = AdmSctGrpRowState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SctGrpRowId1283;
        this.props.DelMst(this.props.AdmSctGrpRow, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmSctGrpRow || {};
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
    const AdmSctGrpRowState = this.props.AdmSctGrpRow || {};
    const auxSystemLabels = AdmSctGrpRowState.SystemLabel || {};
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
      if (!(this.props.AdmSctGrpRow || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmSctGrpRow || {};

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
    const AdmSctGrpRowState = this.props.AdmSctGrpRow || {};

    if (AdmSctGrpRowState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmSctGrpRowState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmSctGrpRowReduxObj.GetScreenButtons(AdmSctGrpRowState) || {};
    const itemList = AdmSctGrpRowState.Dtl || [];
    const auxLabels = AdmSctGrpRowState.Label || {};
    const auxSystemLabels = AdmSctGrpRowState.SystemLabel || {};

    const columnLabel = AdmSctGrpRowState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmSctGrpRowState);
    const authRow = (AdmSctGrpRowState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmSctGrpRow || {}).Mst || {});
    const currDtl = ((this.props.AdmSctGrpRow || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.SctGrpRowId1283));
    const selectList = AdmSctGrpRowReduxObj.SearchListToSelectList(AdmSctGrpRowState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const SctGrpRowId1283 = currMst.SctGrpRowId1283;
const SectionCd1283List = AdmSctGrpRowReduxObj.ScreenDdlSelectors.SectionCd1283(AdmSctGrpRowState);
const SectionCd1283 = currMst.SectionCd1283;
const GroupRowId1283List = AdmSctGrpRowReduxObj.ScreenDdlSelectors.GroupRowId1283(AdmSctGrpRowState);
const GroupRowId1283 = currMst.GroupRowId1283;
const SctGrpRowCss1283 = currMst.SctGrpRowCss1283;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmSctGrpRowState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cSctGrpRowId1283: SctGrpRowId1283 || '',
                  cSectionCd1283: SectionCd1283List.filter(obj => { return obj.key === SectionCd1283 })[0],
                  cGroupRowId1283: GroupRowId1283List.filter(obj => { return obj.key === GroupRowId1283 })[0],
                  cSctGrpRowCss1283: SctGrpRowCss1283 || '',
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
                                {(this.constructor.ShowSpinner(AdmSctGrpRowState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpRowId1283)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpRowId1283)) return null;
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
            {(authCol.SctGrpRowId1283 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpRowId1283 || {}).ColumnHeader} {(columnLabel.SctGrpRowId1283 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpRowId1283 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpRowId1283 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpRowId1283'
disabled = {(authCol.SctGrpRowId1283 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpRowId1283 && touched.cSctGrpRowId1283 && <span className='form__form-group-error'>{errors.cSctGrpRowId1283}</span>}
</div>
</Col>
}
{(authCol.SectionCd1283 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SectionCd1283 || {}).ColumnHeader} {(columnLabel.SectionCd1283 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SectionCd1283 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SectionCd1283 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSectionCd1283'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSectionCd1283')}
value={values.cSectionCd1283}
options={SectionCd1283List}
placeholder=''
disabled = {(authCol.SectionCd1283 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSectionCd1283 && touched.cSectionCd1283 && <span className='form__form-group-error'>{errors.cSectionCd1283}</span>}
</div>
</Col>
}
{(authCol.GroupRowId1283 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupRowId1283 || {}).ColumnHeader} {(columnLabel.GroupRowId1283 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupRowId1283 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupRowId1283 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cGroupRowId1283'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cGroupRowId1283')}
value={values.cGroupRowId1283}
options={GroupRowId1283List}
placeholder=''
disabled = {(authCol.GroupRowId1283 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cGroupRowId1283 && touched.cGroupRowId1283 && <span className='form__form-group-error'>{errors.cGroupRowId1283}</span>}
</div>
</Col>
}
{(authCol.SctGrpRowCss1283 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpRowCss1283 || {}).ColumnHeader} {(columnLabel.SctGrpRowCss1283 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpRowCss1283 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpRowCss1283 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpRowState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpRowCss1283'
disabled = {(authCol.SctGrpRowCss1283 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpRowCss1283 && touched.cSctGrpRowCss1283 && <span className='form__form-group-error'>{errors.cSctGrpRowCss1283}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpRowId1283)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SctGrpRowId1283));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmSctGrpRowState) && <Skeleton height='43px' />) ||
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
  AdmSctGrpRow: state.AdmSctGrpRow,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSctGrpRowReduxObj.LoadPage.bind(AdmSctGrpRowReduxObj) },
    { SavePage: AdmSctGrpRowReduxObj.SavePage.bind(AdmSctGrpRowReduxObj) },
    { DelMst: AdmSctGrpRowReduxObj.DelMst.bind(AdmSctGrpRowReduxObj) },
    { AddMst: AdmSctGrpRowReduxObj.AddMst.bind(AdmSctGrpRowReduxObj) },
//    { SearchMemberId64: AdmSctGrpRowReduxObj.SearchActions.SearchMemberId64.bind(AdmSctGrpRowReduxObj) },
//    { SearchCurrencyId64: AdmSctGrpRowReduxObj.SearchActions.SearchCurrencyId64.bind(AdmSctGrpRowReduxObj) },
//    { SearchCustomerJobId64: AdmSctGrpRowReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmSctGrpRowReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            