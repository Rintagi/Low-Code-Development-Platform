
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
import AdmSctGrpColReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSctGrpCol';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmSctGrpCol || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'SctGrpColId1284';
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
    const columnLabel = (this.props.AdmSctGrpCol || {}).ColumnLabel || {};
    /* standard field validation */

    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmSctGrpCol || {}).Mst || {};
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
        this.props.AdmSctGrpCol,
        {
          SctGrpColId1284: values.cSctGrpColId1284|| '',
          SectionCd1284: (values.cSectionCd1284|| {}).value || '',
          GroupColId1284: (values.cGroupColId1284|| {}).value || '',
          SctGrpColCss1284: values.cSctGrpColCss1284|| '',
          SctGrpColDiv1284: values.cSctGrpColDiv1284|| '',
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
    const AdmSctGrpColState = this.props.AdmSctGrpCol || {};
    const auxSystemLabels = AdmSctGrpColState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).SctGrpColId1284;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmSctGrpCol.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmSctGrpCol.Label);
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
    const AdmSctGrpColState = this.props.AdmSctGrpCol || {};
    const auxSystemLabels = AdmSctGrpColState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.SctGrpColId1284;
        this.props.DelMst(this.props.AdmSctGrpCol, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmSctGrpCol || {};
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
    const AdmSctGrpColState = this.props.AdmSctGrpCol || {};
    const auxSystemLabels = AdmSctGrpColState.SystemLabel || {};
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
      if (!(this.props.AdmSctGrpCol || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmSctGrpCol || {};

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
    const AdmSctGrpColState = this.props.AdmSctGrpCol || {};

    if (AdmSctGrpColState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmSctGrpColState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmSctGrpColReduxObj.GetScreenButtons(AdmSctGrpColState) || {};
    const itemList = AdmSctGrpColState.Dtl || [];
    const auxLabels = AdmSctGrpColState.Label || {};
    const auxSystemLabels = AdmSctGrpColState.SystemLabel || {};

    const columnLabel = AdmSctGrpColState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmSctGrpColState);
    const authRow = (AdmSctGrpColState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmSctGrpCol || {}).Mst || {});
    const currDtl = ((this.props.AdmSctGrpCol || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.SctGrpColId1284));
    const selectList = AdmSctGrpColReduxObj.SearchListToSelectList(AdmSctGrpColState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const SctGrpColId1284 = currMst.SctGrpColId1284;
const SectionCd1284List = AdmSctGrpColReduxObj.ScreenDdlSelectors.SectionCd1284(AdmSctGrpColState);
const SectionCd1284 = currMst.SectionCd1284;
const GroupColId1284List = AdmSctGrpColReduxObj.ScreenDdlSelectors.GroupColId1284(AdmSctGrpColState);
const GroupColId1284 = currMst.GroupColId1284;
const SctGrpColCss1284 = currMst.SctGrpColCss1284;
const SctGrpColDiv1284 = currMst.SctGrpColDiv1284;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmSctGrpColState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cSctGrpColId1284: SctGrpColId1284 || '',
                  cSectionCd1284: SectionCd1284List.filter(obj => { return obj.key === SectionCd1284 })[0],
                  cGroupColId1284: GroupColId1284List.filter(obj => { return obj.key === GroupColId1284 })[0],
                  cSctGrpColCss1284: SctGrpColCss1284 || '',
                  cSctGrpColDiv1284: SctGrpColDiv1284 || '',
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
                                {(this.constructor.ShowSpinner(AdmSctGrpColState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpColId1284)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpColId1284)) return null;
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
            {(authCol.SctGrpColId1284 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpColId1284 || {}).ColumnHeader} {(columnLabel.SctGrpColId1284 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpColId1284 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpColId1284 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpColId1284'
disabled = {(authCol.SctGrpColId1284 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpColId1284 && touched.cSctGrpColId1284 && <span className='form__form-group-error'>{errors.cSctGrpColId1284}</span>}
</div>
</Col>
}
{(authCol.SectionCd1284 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SectionCd1284 || {}).ColumnHeader} {(columnLabel.SectionCd1284 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SectionCd1284 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SectionCd1284 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSectionCd1284'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSectionCd1284')}
value={values.cSectionCd1284}
options={SectionCd1284List}
placeholder=''
disabled = {(authCol.SectionCd1284 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSectionCd1284 && touched.cSectionCd1284 && <span className='form__form-group-error'>{errors.cSectionCd1284}</span>}
</div>
</Col>
}
{(authCol.GroupColId1284 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupColId1284 || {}).ColumnHeader} {(columnLabel.GroupColId1284 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupColId1284 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupColId1284 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cGroupColId1284'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cGroupColId1284')}
value={values.cGroupColId1284}
options={GroupColId1284List}
placeholder=''
disabled = {(authCol.GroupColId1284 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cGroupColId1284 && touched.cGroupColId1284 && <span className='form__form-group-error'>{errors.cGroupColId1284}</span>}
</div>
</Col>
}
{(authCol.SctGrpColCss1284 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpColCss1284 || {}).ColumnHeader} {(columnLabel.SctGrpColCss1284 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpColCss1284 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpColCss1284 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpColCss1284'
disabled = {(authCol.SctGrpColCss1284 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpColCss1284 && touched.cSctGrpColCss1284 && <span className='form__form-group-error'>{errors.cSctGrpColCss1284}</span>}
</div>
</Col>
}
{(authCol.SctGrpColDiv1284 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SctGrpColDiv1284 || {}).ColumnHeader} {(columnLabel.SctGrpColDiv1284 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SctGrpColDiv1284 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SctGrpColDiv1284 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmSctGrpColState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSctGrpColDiv1284'
disabled = {(authCol.SctGrpColDiv1284 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSctGrpColDiv1284 && touched.cSctGrpColDiv1284 && <span className='form__form-group-error'>{errors.cSctGrpColDiv1284}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SctGrpColId1284)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).SctGrpColId1284));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmSctGrpColState) && <Skeleton height='43px' />) ||
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
  AdmSctGrpCol: state.AdmSctGrpCol,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSctGrpColReduxObj.LoadPage.bind(AdmSctGrpColReduxObj) },
    { SavePage: AdmSctGrpColReduxObj.SavePage.bind(AdmSctGrpColReduxObj) },
    { DelMst: AdmSctGrpColReduxObj.DelMst.bind(AdmSctGrpColReduxObj) },
    { AddMst: AdmSctGrpColReduxObj.AddMst.bind(AdmSctGrpColReduxObj) },
//    { SearchMemberId64: AdmSctGrpColReduxObj.SearchActions.SearchMemberId64.bind(AdmSctGrpColReduxObj) },
//    { SearchCurrencyId64: AdmSctGrpColReduxObj.SearchActions.SearchCurrencyId64.bind(AdmSctGrpColReduxObj) },
//    { SearchCustomerJobId64: AdmSctGrpColReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmSctGrpColReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            