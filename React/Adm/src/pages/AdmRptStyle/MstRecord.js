
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
import AdmRptStyleReduxObj, { ShowMstFilterApplied } from '../../redux/AdmRptStyle';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmRptStyle || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'RptStyleId167';
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
    const columnLabel = (this.props.AdmRptStyle || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cRptStyleDesc167) { errors.cRptStyleDesc167 = (columnLabel.RptStyleDesc167 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmRptStyle || {}).Mst || {};
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
        this.props.AdmRptStyle,
        {
          RptStyleId167: values.cRptStyleId167|| '',
          DefaultCd167: (values.cDefaultCd167|| {}).value || '',
          RptStyleDesc167: values.cRptStyleDesc167|| '',
          BorderColorD167: values.cBorderColorD167|| '',
          BorderColorL167: values.cBorderColorL167|| '',
          BorderColorR167: values.cBorderColorR167|| '',
          BorderColorT167: values.cBorderColorT167|| '',
          BorderColorB167: values.cBorderColorB167|| '',
          Color167: values.cColor167|| '',
          BgColor167: values.cBgColor167|| '',
          BgGradType167: (values.cBgGradType167|| {}).value || '',
          BgGradColor167: values.cBgGradColor167|| '',
          BgImage167: values.cBgImage167|| '',
          Direction167: (values.cDirection167|| {}).value || '',
          WritingMode167: (values.cWritingMode167|| {}).value || '',
          LineHeight167: values.cLineHeight167|| '',
          Format167: values.cFormat167|| '',
          BorderStyleD167: (values.cBorderStyleD167|| {}).value || '',
          BorderStyleL167: (values.cBorderStyleL167|| {}).value || '',
          BorderStyleR167: (values.cBorderStyleR167|| {}).value || '',
          BorderStyleT167: (values.cBorderStyleT167|| {}).value || '',
          BorderStyleB167: (values.cBorderStyleB167|| {}).value || '',
          FontStyle167: (values.cFontStyle167|| {}).value || '',
          FontFamily167: values.cFontFamily167|| '',
          FontSize167: values.cFontSize167|| '',
          FontWeight167: (values.cFontWeight167|| {}).value || '',
          TextDecor167: (values.cTextDecor167|| {}).value || '',
          TextAlign167: (values.cTextAlign167|| {}).value || '',
          VerticalAlign167: (values.cVerticalAlign167|| {}).value || '',
          BorderWidthD167: values.cBorderWidthD167|| '',
          BorderWidthL167: values.cBorderWidthL167|| '',
          BorderWidthR167: values.cBorderWidthR167|| '',
          BorderWidthT167: values.cBorderWidthT167|| '',
          BorderWidthB167: values.cBorderWidthB167|| '',
          PadLeft167: values.cPadLeft167|| '',
          PadRight167: values.cPadRight167|| '',
          PadTop167: values.cPadTop167|| '',
          PadBottom167: values.cPadBottom167|| '',
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
    const AdmRptStyleState = this.props.AdmRptStyle || {};
    const auxSystemLabels = AdmRptStyleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).RptStyleId167;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmRptStyle.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmRptStyle.Label);
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
    const AdmRptStyleState = this.props.AdmRptStyle || {};
    const auxSystemLabels = AdmRptStyleState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.RptStyleId167;
        this.props.DelMst(this.props.AdmRptStyle, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmRptStyle || {};
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
    const AdmRptStyleState = this.props.AdmRptStyle || {};
    const auxSystemLabels = AdmRptStyleState.SystemLabel || {};
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
      if (!(this.props.AdmRptStyle || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmRptStyle || {};

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
    const AdmRptStyleState = this.props.AdmRptStyle || {};

    if (AdmRptStyleState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmRptStyleState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmRptStyleReduxObj.GetScreenButtons(AdmRptStyleState) || {};
    const itemList = AdmRptStyleState.Dtl || [];
    const auxLabels = AdmRptStyleState.Label || {};
    const auxSystemLabels = AdmRptStyleState.SystemLabel || {};

    const columnLabel = AdmRptStyleState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmRptStyleState);
    const authRow = (AdmRptStyleState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmRptStyle || {}).Mst || {});
    const currDtl = ((this.props.AdmRptStyle || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.RptStyleId167));
    const selectList = AdmRptStyleReduxObj.SearchListToSelectList(AdmRptStyleState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const RptStyleId167 = currMst.RptStyleId167;
const DefaultCd167List = AdmRptStyleReduxObj.ScreenDdlSelectors.DefaultCd167(AdmRptStyleState);
const DefaultCd167 = currMst.DefaultCd167;
const RptStyleDesc167 = currMst.RptStyleDesc167;
const BorderColorD167 = currMst.BorderColorD167;
const BorderColorL167 = currMst.BorderColorL167;
const BorderColorR167 = currMst.BorderColorR167;
const BorderColorT167 = currMst.BorderColorT167;
const BorderColorB167 = currMst.BorderColorB167;
const Color167 = currMst.Color167;
const BgColor167 = currMst.BgColor167;
const BgGradType167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BgGradType167(AdmRptStyleState);
const BgGradType167 = currMst.BgGradType167;
const BgGradColor167 = currMst.BgGradColor167;
const BgImage167 = currMst.BgImage167;
const Direction167List = AdmRptStyleReduxObj.ScreenDdlSelectors.Direction167(AdmRptStyleState);
const Direction167 = currMst.Direction167;
const WritingMode167List = AdmRptStyleReduxObj.ScreenDdlSelectors.WritingMode167(AdmRptStyleState);
const WritingMode167 = currMst.WritingMode167;
const LineHeight167 = currMst.LineHeight167;
const Format167 = currMst.Format167;
const BorderStyleD167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BorderStyleD167(AdmRptStyleState);
const BorderStyleD167 = currMst.BorderStyleD167;
const BorderStyleL167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BorderStyleL167(AdmRptStyleState);
const BorderStyleL167 = currMst.BorderStyleL167;
const BorderStyleR167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BorderStyleR167(AdmRptStyleState);
const BorderStyleR167 = currMst.BorderStyleR167;
const BorderStyleT167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BorderStyleT167(AdmRptStyleState);
const BorderStyleT167 = currMst.BorderStyleT167;
const BorderStyleB167List = AdmRptStyleReduxObj.ScreenDdlSelectors.BorderStyleB167(AdmRptStyleState);
const BorderStyleB167 = currMst.BorderStyleB167;
const FontStyle167List = AdmRptStyleReduxObj.ScreenDdlSelectors.FontStyle167(AdmRptStyleState);
const FontStyle167 = currMst.FontStyle167;
const FontFamily167 = currMst.FontFamily167;
const FontSize167 = currMst.FontSize167;
const FontWeight167List = AdmRptStyleReduxObj.ScreenDdlSelectors.FontWeight167(AdmRptStyleState);
const FontWeight167 = currMst.FontWeight167;
const TextDecor167List = AdmRptStyleReduxObj.ScreenDdlSelectors.TextDecor167(AdmRptStyleState);
const TextDecor167 = currMst.TextDecor167;
const TextAlign167List = AdmRptStyleReduxObj.ScreenDdlSelectors.TextAlign167(AdmRptStyleState);
const TextAlign167 = currMst.TextAlign167;
const VerticalAlign167List = AdmRptStyleReduxObj.ScreenDdlSelectors.VerticalAlign167(AdmRptStyleState);
const VerticalAlign167 = currMst.VerticalAlign167;
const BorderWidthD167 = currMst.BorderWidthD167;
const BorderWidthL167 = currMst.BorderWidthL167;
const BorderWidthR167 = currMst.BorderWidthR167;
const BorderWidthT167 = currMst.BorderWidthT167;
const BorderWidthB167 = currMst.BorderWidthB167;
const PadLeft167 = currMst.PadLeft167;
const PadRight167 = currMst.PadRight167;
const PadTop167 = currMst.PadTop167;
const PadBottom167 = currMst.PadBottom167;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmRptStyleState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cRptStyleId167: RptStyleId167 || '',
                  cDefaultCd167: DefaultCd167List.filter(obj => { return obj.key === DefaultCd167 })[0],
                  cRptStyleDesc167: RptStyleDesc167 || '',
                  cBorderColorD167: BorderColorD167 || '',
                  cBorderColorL167: BorderColorL167 || '',
                  cBorderColorR167: BorderColorR167 || '',
                  cBorderColorT167: BorderColorT167 || '',
                  cBorderColorB167: BorderColorB167 || '',
                  cColor167: Color167 || '',
                  cBgColor167: BgColor167 || '',
                  cBgGradType167: BgGradType167List.filter(obj => { return obj.key === BgGradType167 })[0],
                  cBgGradColor167: BgGradColor167 || '',
                  cBgImage167: BgImage167 || '',
                  cDirection167: Direction167List.filter(obj => { return obj.key === Direction167 })[0],
                  cWritingMode167: WritingMode167List.filter(obj => { return obj.key === WritingMode167 })[0],
                  cLineHeight167: LineHeight167 || '',
                  cFormat167: Format167 || '',
                  cBorderStyleD167: BorderStyleD167List.filter(obj => { return obj.key === BorderStyleD167 })[0],
                  cBorderStyleL167: BorderStyleL167List.filter(obj => { return obj.key === BorderStyleL167 })[0],
                  cBorderStyleR167: BorderStyleR167List.filter(obj => { return obj.key === BorderStyleR167 })[0],
                  cBorderStyleT167: BorderStyleT167List.filter(obj => { return obj.key === BorderStyleT167 })[0],
                  cBorderStyleB167: BorderStyleB167List.filter(obj => { return obj.key === BorderStyleB167 })[0],
                  cFontStyle167: FontStyle167List.filter(obj => { return obj.key === FontStyle167 })[0],
                  cFontFamily167: FontFamily167 || '',
                  cFontSize167: FontSize167 || '',
                  cFontWeight167: FontWeight167List.filter(obj => { return obj.key === FontWeight167 })[0],
                  cTextDecor167: TextDecor167List.filter(obj => { return obj.key === TextDecor167 })[0],
                  cTextAlign167: TextAlign167List.filter(obj => { return obj.key === TextAlign167 })[0],
                  cVerticalAlign167: VerticalAlign167List.filter(obj => { return obj.key === VerticalAlign167 })[0],
                  cBorderWidthD167: BorderWidthD167 || '',
                  cBorderWidthL167: BorderWidthL167 || '',
                  cBorderWidthR167: BorderWidthR167 || '',
                  cBorderWidthT167: BorderWidthT167 || '',
                  cBorderWidthB167: BorderWidthB167 || '',
                  cPadLeft167: PadLeft167 || '',
                  cPadRight167: PadRight167 || '',
                  cPadTop167: PadTop167 || '',
                  cPadBottom167: PadBottom167 || '',
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
                                {(this.constructor.ShowSpinner(AdmRptStyleState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptStyleId167)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptStyleId167)) return null;
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
            {(authCol.RptStyleId167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptStyleId167 || {}).ColumnHeader} {(columnLabel.RptStyleId167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptStyleId167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptStyleId167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptStyleId167'
disabled = {(authCol.RptStyleId167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptStyleId167 && touched.cRptStyleId167 && <span className='form__form-group-error'>{errors.cRptStyleId167}</span>}
</div>
</Col>
}
{(authCol.DefaultCd167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultCd167 || {}).ColumnHeader} {(columnLabel.DefaultCd167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultCd167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultCd167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDefaultCd167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDefaultCd167')}
value={values.cDefaultCd167}
options={DefaultCd167List}
placeholder=''
disabled = {(authCol.DefaultCd167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultCd167 && touched.cDefaultCd167 && <span className='form__form-group-error'>{errors.cDefaultCd167}</span>}
</div>
</Col>
}
{(authCol.RptStyleDesc167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RptStyleDesc167 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.RptStyleDesc167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RptStyleDesc167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RptStyleDesc167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRptStyleDesc167'
disabled = {(authCol.RptStyleDesc167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRptStyleDesc167 && touched.cRptStyleDesc167 && <span className='form__form-group-error'>{errors.cRptStyleDesc167}</span>}
</div>
</Col>
}
{(authCol.BorderColorD167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderColorD167 || {}).ColumnHeader} {(columnLabel.BorderColorD167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderColorD167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderColorD167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderColorD167'
disabled = {(authCol.BorderColorD167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderColorD167 && touched.cBorderColorD167 && <span className='form__form-group-error'>{errors.cBorderColorD167}</span>}
</div>
</Col>
}
{(authCol.BorderColorL167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderColorL167 || {}).ColumnHeader} {(columnLabel.BorderColorL167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderColorL167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderColorL167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderColorL167'
disabled = {(authCol.BorderColorL167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderColorL167 && touched.cBorderColorL167 && <span className='form__form-group-error'>{errors.cBorderColorL167}</span>}
</div>
</Col>
}
{(authCol.BorderColorR167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderColorR167 || {}).ColumnHeader} {(columnLabel.BorderColorR167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderColorR167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderColorR167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderColorR167'
disabled = {(authCol.BorderColorR167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderColorR167 && touched.cBorderColorR167 && <span className='form__form-group-error'>{errors.cBorderColorR167}</span>}
</div>
</Col>
}
{(authCol.BorderColorT167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderColorT167 || {}).ColumnHeader} {(columnLabel.BorderColorT167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderColorT167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderColorT167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderColorT167'
disabled = {(authCol.BorderColorT167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderColorT167 && touched.cBorderColorT167 && <span className='form__form-group-error'>{errors.cBorderColorT167}</span>}
</div>
</Col>
}
{(authCol.BorderColorB167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderColorB167 || {}).ColumnHeader} {(columnLabel.BorderColorB167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderColorB167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderColorB167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderColorB167'
disabled = {(authCol.BorderColorB167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderColorB167 && touched.cBorderColorB167 && <span className='form__form-group-error'>{errors.cBorderColorB167}</span>}
</div>
</Col>
}
{(authCol.Color167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Color167 || {}).ColumnHeader} {(columnLabel.Color167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Color167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Color167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColor167'
disabled = {(authCol.Color167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColor167 && touched.cColor167 && <span className='form__form-group-error'>{errors.cColor167}</span>}
</div>
</Col>
}
{(authCol.BgColor167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BgColor167 || {}).ColumnHeader} {(columnLabel.BgColor167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BgColor167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BgColor167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBgColor167'
disabled = {(authCol.BgColor167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBgColor167 && touched.cBgColor167 && <span className='form__form-group-error'>{errors.cBgColor167}</span>}
</div>
</Col>
}
{(authCol.BgGradType167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BgGradType167 || {}).ColumnHeader} {(columnLabel.BgGradType167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BgGradType167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BgGradType167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBgGradType167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBgGradType167')}
value={values.cBgGradType167}
options={BgGradType167List}
placeholder=''
disabled = {(authCol.BgGradType167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBgGradType167 && touched.cBgGradType167 && <span className='form__form-group-error'>{errors.cBgGradType167}</span>}
</div>
</Col>
}
{(authCol.BgGradColor167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BgGradColor167 || {}).ColumnHeader} {(columnLabel.BgGradColor167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BgGradColor167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BgGradColor167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBgGradColor167'
disabled = {(authCol.BgGradColor167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBgGradColor167 && touched.cBgGradColor167 && <span className='form__form-group-error'>{errors.cBgGradColor167}</span>}
</div>
</Col>
}
{(authCol.BgImage167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BgImage167 || {}).ColumnHeader} {(columnLabel.BgImage167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BgImage167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BgImage167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBgImage167'
disabled = {(authCol.BgImage167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBgImage167 && touched.cBgImage167 && <span className='form__form-group-error'>{errors.cBgImage167}</span>}
</div>
</Col>
}
{(authCol.Direction167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Direction167 || {}).ColumnHeader} {(columnLabel.Direction167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Direction167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Direction167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDirection167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDirection167')}
value={values.cDirection167}
options={Direction167List}
placeholder=''
disabled = {(authCol.Direction167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDirection167 && touched.cDirection167 && <span className='form__form-group-error'>{errors.cDirection167}</span>}
</div>
</Col>
}
{(authCol.WritingMode167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.WritingMode167 || {}).ColumnHeader} {(columnLabel.WritingMode167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.WritingMode167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.WritingMode167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cWritingMode167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cWritingMode167')}
value={values.cWritingMode167}
options={WritingMode167List}
placeholder=''
disabled = {(authCol.WritingMode167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cWritingMode167 && touched.cWritingMode167 && <span className='form__form-group-error'>{errors.cWritingMode167}</span>}
</div>
</Col>
}
{(authCol.LineHeight167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LineHeight167 || {}).ColumnHeader} {(columnLabel.LineHeight167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LineHeight167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LineHeight167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLineHeight167'
disabled = {(authCol.LineHeight167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLineHeight167 && touched.cLineHeight167 && <span className='form__form-group-error'>{errors.cLineHeight167}</span>}
</div>
</Col>
}
{(authCol.Format167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.Format167 || {}).ColumnHeader} {(columnLabel.Format167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Format167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Format167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFormat167'
disabled = {(authCol.Format167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFormat167 && touched.cFormat167 && <span className='form__form-group-error'>{errors.cFormat167}</span>}
</div>
</Col>
}
{(authCol.BorderStyleD167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderStyleD167 || {}).ColumnHeader} {(columnLabel.BorderStyleD167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderStyleD167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderStyleD167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBorderStyleD167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBorderStyleD167')}
value={values.cBorderStyleD167}
options={BorderStyleD167List}
placeholder=''
disabled = {(authCol.BorderStyleD167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderStyleD167 && touched.cBorderStyleD167 && <span className='form__form-group-error'>{errors.cBorderStyleD167}</span>}
</div>
</Col>
}
{(authCol.BorderStyleL167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderStyleL167 || {}).ColumnHeader} {(columnLabel.BorderStyleL167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderStyleL167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderStyleL167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBorderStyleL167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBorderStyleL167')}
value={values.cBorderStyleL167}
options={BorderStyleL167List}
placeholder=''
disabled = {(authCol.BorderStyleL167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderStyleL167 && touched.cBorderStyleL167 && <span className='form__form-group-error'>{errors.cBorderStyleL167}</span>}
</div>
</Col>
}
{(authCol.BorderStyleR167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderStyleR167 || {}).ColumnHeader} {(columnLabel.BorderStyleR167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderStyleR167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderStyleR167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBorderStyleR167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBorderStyleR167')}
value={values.cBorderStyleR167}
options={BorderStyleR167List}
placeholder=''
disabled = {(authCol.BorderStyleR167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderStyleR167 && touched.cBorderStyleR167 && <span className='form__form-group-error'>{errors.cBorderStyleR167}</span>}
</div>
</Col>
}
{(authCol.BorderStyleT167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderStyleT167 || {}).ColumnHeader} {(columnLabel.BorderStyleT167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderStyleT167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderStyleT167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBorderStyleT167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBorderStyleT167')}
value={values.cBorderStyleT167}
options={BorderStyleT167List}
placeholder=''
disabled = {(authCol.BorderStyleT167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderStyleT167 && touched.cBorderStyleT167 && <span className='form__form-group-error'>{errors.cBorderStyleT167}</span>}
</div>
</Col>
}
{(authCol.BorderStyleB167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderStyleB167 || {}).ColumnHeader} {(columnLabel.BorderStyleB167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderStyleB167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderStyleB167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cBorderStyleB167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cBorderStyleB167')}
value={values.cBorderStyleB167}
options={BorderStyleB167List}
placeholder=''
disabled = {(authCol.BorderStyleB167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderStyleB167 && touched.cBorderStyleB167 && <span className='form__form-group-error'>{errors.cBorderStyleB167}</span>}
</div>
</Col>
}
{(authCol.FontStyle167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FontStyle167 || {}).ColumnHeader} {(columnLabel.FontStyle167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FontStyle167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FontStyle167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cFontStyle167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cFontStyle167')}
value={values.cFontStyle167}
options={FontStyle167List}
placeholder=''
disabled = {(authCol.FontStyle167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFontStyle167 && touched.cFontStyle167 && <span className='form__form-group-error'>{errors.cFontStyle167}</span>}
</div>
</Col>
}
{(authCol.FontFamily167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FontFamily167 || {}).ColumnHeader} {(columnLabel.FontFamily167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FontFamily167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FontFamily167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFontFamily167'
disabled = {(authCol.FontFamily167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFontFamily167 && touched.cFontFamily167 && <span className='form__form-group-error'>{errors.cFontFamily167}</span>}
</div>
</Col>
}
{(authCol.FontSize167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FontSize167 || {}).ColumnHeader} {(columnLabel.FontSize167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FontSize167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FontSize167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFontSize167'
disabled = {(authCol.FontSize167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFontSize167 && touched.cFontSize167 && <span className='form__form-group-error'>{errors.cFontSize167}</span>}
</div>
</Col>
}
{(authCol.FontWeight167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FontWeight167 || {}).ColumnHeader} {(columnLabel.FontWeight167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FontWeight167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FontWeight167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cFontWeight167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cFontWeight167')}
value={values.cFontWeight167}
options={FontWeight167List}
placeholder=''
disabled = {(authCol.FontWeight167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFontWeight167 && touched.cFontWeight167 && <span className='form__form-group-error'>{errors.cFontWeight167}</span>}
</div>
</Col>
}
{(authCol.TextDecor167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TextDecor167 || {}).ColumnHeader} {(columnLabel.TextDecor167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TextDecor167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TextDecor167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cTextDecor167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cTextDecor167')}
value={values.cTextDecor167}
options={TextDecor167List}
placeholder=''
disabled = {(authCol.TextDecor167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTextDecor167 && touched.cTextDecor167 && <span className='form__form-group-error'>{errors.cTextDecor167}</span>}
</div>
</Col>
}
{(authCol.TextAlign167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TextAlign167 || {}).ColumnHeader} {(columnLabel.TextAlign167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TextAlign167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TextAlign167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cTextAlign167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cTextAlign167')}
value={values.cTextAlign167}
options={TextAlign167List}
placeholder=''
disabled = {(authCol.TextAlign167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTextAlign167 && touched.cTextAlign167 && <span className='form__form-group-error'>{errors.cTextAlign167}</span>}
</div>
</Col>
}
{(authCol.VerticalAlign167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VerticalAlign167 || {}).ColumnHeader} {(columnLabel.VerticalAlign167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VerticalAlign167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VerticalAlign167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cVerticalAlign167'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cVerticalAlign167')}
value={values.cVerticalAlign167}
options={VerticalAlign167List}
placeholder=''
disabled = {(authCol.VerticalAlign167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cVerticalAlign167 && touched.cVerticalAlign167 && <span className='form__form-group-error'>{errors.cVerticalAlign167}</span>}
</div>
</Col>
}
{(authCol.BorderWidthD167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderWidthD167 || {}).ColumnHeader} {(columnLabel.BorderWidthD167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderWidthD167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderWidthD167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderWidthD167'
disabled = {(authCol.BorderWidthD167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderWidthD167 && touched.cBorderWidthD167 && <span className='form__form-group-error'>{errors.cBorderWidthD167}</span>}
</div>
</Col>
}
{(authCol.BorderWidthL167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderWidthL167 || {}).ColumnHeader} {(columnLabel.BorderWidthL167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderWidthL167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderWidthL167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderWidthL167'
disabled = {(authCol.BorderWidthL167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderWidthL167 && touched.cBorderWidthL167 && <span className='form__form-group-error'>{errors.cBorderWidthL167}</span>}
</div>
</Col>
}
{(authCol.BorderWidthR167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderWidthR167 || {}).ColumnHeader} {(columnLabel.BorderWidthR167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderWidthR167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderWidthR167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderWidthR167'
disabled = {(authCol.BorderWidthR167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderWidthR167 && touched.cBorderWidthR167 && <span className='form__form-group-error'>{errors.cBorderWidthR167}</span>}
</div>
</Col>
}
{(authCol.BorderWidthT167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderWidthT167 || {}).ColumnHeader} {(columnLabel.BorderWidthT167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderWidthT167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderWidthT167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderWidthT167'
disabled = {(authCol.BorderWidthT167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderWidthT167 && touched.cBorderWidthT167 && <span className='form__form-group-error'>{errors.cBorderWidthT167}</span>}
</div>
</Col>
}
{(authCol.BorderWidthB167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorderWidthB167 || {}).ColumnHeader} {(columnLabel.BorderWidthB167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorderWidthB167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorderWidthB167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cBorderWidthB167'
disabled = {(authCol.BorderWidthB167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cBorderWidthB167 && touched.cBorderWidthB167 && <span className='form__form-group-error'>{errors.cBorderWidthB167}</span>}
</div>
</Col>
}
{(authCol.PadLeft167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PadLeft167 || {}).ColumnHeader} {(columnLabel.PadLeft167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PadLeft167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PadLeft167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPadLeft167'
disabled = {(authCol.PadLeft167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPadLeft167 && touched.cPadLeft167 && <span className='form__form-group-error'>{errors.cPadLeft167}</span>}
</div>
</Col>
}
{(authCol.PadRight167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PadRight167 || {}).ColumnHeader} {(columnLabel.PadRight167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PadRight167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PadRight167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPadRight167'
disabled = {(authCol.PadRight167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPadRight167 && touched.cPadRight167 && <span className='form__form-group-error'>{errors.cPadRight167}</span>}
</div>
</Col>
}
{(authCol.PadTop167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PadTop167 || {}).ColumnHeader} {(columnLabel.PadTop167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PadTop167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PadTop167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPadTop167'
disabled = {(authCol.PadTop167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPadTop167 && touched.cPadTop167 && <span className='form__form-group-error'>{errors.cPadTop167}</span>}
</div>
</Col>
}
{(authCol.PadBottom167 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PadBottom167 || {}).ColumnHeader} {(columnLabel.PadBottom167 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PadBottom167 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PadBottom167 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmRptStyleState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPadBottom167'
disabled = {(authCol.PadBottom167 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPadBottom167 && touched.cPadBottom167 && <span className='form__form-group-error'>{errors.cPadBottom167}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).RptStyleId167)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).RptStyleId167));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmRptStyleState) && <Skeleton height='43px' />) ||
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
  AdmRptStyle: state.AdmRptStyle,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmRptStyleReduxObj.LoadPage.bind(AdmRptStyleReduxObj) },
    { SavePage: AdmRptStyleReduxObj.SavePage.bind(AdmRptStyleReduxObj) },
    { DelMst: AdmRptStyleReduxObj.DelMst.bind(AdmRptStyleReduxObj) },
    { AddMst: AdmRptStyleReduxObj.AddMst.bind(AdmRptStyleReduxObj) },
//    { SearchMemberId64: AdmRptStyleReduxObj.SearchActions.SearchMemberId64.bind(AdmRptStyleReduxObj) },
//    { SearchCurrencyId64: AdmRptStyleReduxObj.SearchActions.SearchCurrencyId64.bind(AdmRptStyleReduxObj) },
//    { SearchCustomerJobId64: AdmRptStyleReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmRptStyleReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            