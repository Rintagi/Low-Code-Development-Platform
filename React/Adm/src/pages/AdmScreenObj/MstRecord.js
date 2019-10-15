
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
import AdmScreenObjReduxObj, { ShowMstFilterApplied } from '../../redux/AdmScreenObj';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreenObj || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'ScreenObjId14';
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

ScreenId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchScreenId14(v, filterBy);}}
GroupRowId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchGroupRowId14(v, filterBy);}}
GroupColId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchGroupColId14(v, filterBy);}}
ColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchColumnId14(v, filterBy);}}
DdlKeyColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlKeyColumnId14(v, filterBy);}}
DdlRefColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlRefColumnId14(v, filterBy);}}
DdlSrtColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlSrtColumnId14(v, filterBy);}}
DdlAdnColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlAdnColumnId14(v, filterBy);}}
DdlFtrColumnId14InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchDdlFtrColumnId14(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmScreenObj || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cGridGrpCd14 || {}).value)) { errors.cGridGrpCd14 = (columnLabel.GridGrpCd14 || {}).ErrMessage;}
if (isEmptyId((values.cColumnJustify14 || {}).value)) { errors.cColumnJustify14 = (columnLabel.ColumnJustify14 || {}).ErrMessage;}
if (isEmptyId((values.cScreenId14 || {}).value)) { errors.cScreenId14 = (columnLabel.ScreenId14 || {}).ErrMessage;}
if (isEmptyId((values.cGroupRowId14 || {}).value)) { errors.cGroupRowId14 = (columnLabel.GroupRowId14 || {}).ErrMessage;}
if (isEmptyId((values.cGroupColId14 || {}).value)) { errors.cGroupColId14 = (columnLabel.GroupColId14 || {}).ErrMessage;}
if (!values.cColumnName14) { errors.cColumnName14 = (columnLabel.ColumnName14 || {}).ErrMessage;}
if (isEmptyId((values.cDisplayModeId14 || {}).value)) { errors.cDisplayModeId14 = (columnLabel.DisplayModeId14 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmScreenObj || {}).Mst || {};
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
        this.props.AdmScreenObj,
        {
          ScreenObjId14: values.cScreenObjId14|| '',
          MasterTable14: values.cMasterTable14 ? 'Y' : 'N',
          RequiredValid14: values.cRequiredValid14 ? 'Y' : 'N',
          ColumnWrap14: values.cColumnWrap14 ? 'Y' : 'N',
          GridGrpCd14: (values.cGridGrpCd14|| {}).value || '',
          HideOnTablet14: values.cHideOnTablet14 ? 'Y' : 'N',
          HideOnMobile14: values.cHideOnMobile14 ? 'Y' : 'N',
          RefreshOnCUD14: values.cRefreshOnCUD14 ? 'Y' : 'N',
          TrimOnEntry14: values.cTrimOnEntry14 ? 'Y' : 'N',
          IgnoreConfirm14: values.cIgnoreConfirm14 ? 'Y' : 'N',
          ColumnJustify14: (values.cColumnJustify14|| {}).value || '',
          ColumnSize14: values.cColumnSize14|| '',
          ColumnHeight14: values.cColumnHeight14|| '',
          ResizeWidth14: values.cResizeWidth14|| '',
          ResizeHeight14: values.cResizeHeight14|| '',
          SortOrder14: values.cSortOrder14|| '',
          ScreenId14: (values.cScreenId14|| {}).value || '',
          GroupRowId14: (values.cGroupRowId14|| {}).value || '',
          GroupColId14: (values.cGroupColId14|| {}).value || '',
          ColumnId14: (values.cColumnId14|| {}).value || '',
          ColumnName14: values.cColumnName14|| '',
          DisplayModeId14: (values.cDisplayModeId14|| {}).value || '',
          DisplayDesc18: values.cDisplayDesc18|| '',
          DdlKeyColumnId14: (values.cDdlKeyColumnId14|| {}).value || '',
          DdlRefColumnId14: (values.cDdlRefColumnId14|| {}).value || '',
          DdlSrtColumnId14: (values.cDdlSrtColumnId14|| {}).value || '',
          DdlAdnColumnId14: (values.cDdlAdnColumnId14|| {}).value || '',
          DdlFtrColumnId14: (values.cDdlFtrColumnId14|| {}).value || '',
          ColumnLink14: values.cColumnLink14|| '',
          DtlLstPosId14: (values.cDtlLstPosId14|| {}).value || '',
          LabelVertical14: values.cLabelVertical14 ? 'Y' : 'N',
          LabelCss14: values.cLabelCss14|| '',
          ContentCss14: values.cContentCss14|| '',
          DefaultValue14: values.cDefaultValue14|| '',
          HyperLinkUrl14: values.cHyperLinkUrl14|| '',
          DefAfter14: values.cDefAfter14 ? 'Y' : 'N',
          SystemValue14: values.cSystemValue14|| '',
          DefAlways14: values.cDefAlways14 ? 'Y' : 'N',
          AggregateCd14: (values.cAggregateCd14|| {}).value || '',
          GenerateSp14: values.cGenerateSp14 ? 'Y' : 'N',
          MaskValid14: values.cMaskValid14|| '',
          RangeValidType14: values.cRangeValidType14|| '',
          RangeValidMax14: values.cRangeValidMax14|| '',
          RangeValidMin14: values.cRangeValidMin14|| '',
          MatchCd14: (values.cMatchCd14|| {}).value || '',
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
    const AdmScreenObjState = this.props.AdmScreenObj || {};
    const auxSystemLabels = AdmScreenObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).ScreenObjId14;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmScreenObj.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmScreenObj.Label);
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
    const AdmScreenObjState = this.props.AdmScreenObj || {};
    const auxSystemLabels = AdmScreenObjState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.ScreenObjId14;
        this.props.DelMst(this.props.AdmScreenObj, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmScreenObj || {};
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
    const AdmScreenObjState = this.props.AdmScreenObj || {};
    const auxSystemLabels = AdmScreenObjState.SystemLabel || {};
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
      if (!(this.props.AdmScreenObj || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmScreenObj || {};

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
    const AdmScreenObjState = this.props.AdmScreenObj || {};

    if (AdmScreenObjState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmScreenObjState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmScreenObjReduxObj.GetScreenButtons(AdmScreenObjState) || {};
    const itemList = AdmScreenObjState.Dtl || [];
    const auxLabels = AdmScreenObjState.Label || {};
    const auxSystemLabels = AdmScreenObjState.SystemLabel || {};

    const columnLabel = AdmScreenObjState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmScreenObjState);
    const authRow = (AdmScreenObjState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmScreenObj || {}).Mst || {});
    const currDtl = ((this.props.AdmScreenObj || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.ScreenObjId14));
    const selectList = AdmScreenObjReduxObj.SearchListToSelectList(AdmScreenObjState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const ScreenObjId14 = currMst.ScreenObjId14;
const MasterTable14 = currMst.MasterTable14;
const RequiredValid14 = currMst.RequiredValid14;
const ColumnWrap14 = currMst.ColumnWrap14;
const GridGrpCd14List = AdmScreenObjReduxObj.ScreenDdlSelectors.GridGrpCd14(AdmScreenObjState);
const GridGrpCd14 = currMst.GridGrpCd14;
const HideOnTablet14 = currMst.HideOnTablet14;
const HideOnMobile14 = currMst.HideOnMobile14;
const RefreshOnCUD14 = currMst.RefreshOnCUD14;
const TrimOnEntry14 = currMst.TrimOnEntry14;
const IgnoreConfirm14 = currMst.IgnoreConfirm14;
const ColumnJustify14List = AdmScreenObjReduxObj.ScreenDdlSelectors.ColumnJustify14(AdmScreenObjState);
const ColumnJustify14 = currMst.ColumnJustify14;
const ColumnSize14 = currMst.ColumnSize14;
const ColumnHeight14 = currMst.ColumnHeight14;
const ResizeWidth14 = currMst.ResizeWidth14;
const ResizeHeight14 = currMst.ResizeHeight14;
const SortOrder14 = currMst.SortOrder14;
const ScreenId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.ScreenId14(AdmScreenObjState);
const ScreenId14 = currMst.ScreenId14;
const GroupRowId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.GroupRowId14(AdmScreenObjState);
const GroupRowId14 = currMst.GroupRowId14;
const GroupColId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.GroupColId14(AdmScreenObjState);
const GroupColId14 = currMst.GroupColId14;
const ColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.ColumnId14(AdmScreenObjState);
const ColumnId14 = currMst.ColumnId14;
const ColumnName14 = currMst.ColumnName14;
const DisplayModeId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DisplayModeId14(AdmScreenObjState);
const DisplayModeId14 = currMst.DisplayModeId14;
const DisplayDesc18 = currMst.DisplayDesc18;
const DdlKeyColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DdlKeyColumnId14(AdmScreenObjState);
const DdlKeyColumnId14 = currMst.DdlKeyColumnId14;
const DdlRefColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DdlRefColumnId14(AdmScreenObjState);
const DdlRefColumnId14 = currMst.DdlRefColumnId14;
const DdlSrtColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DdlSrtColumnId14(AdmScreenObjState);
const DdlSrtColumnId14 = currMst.DdlSrtColumnId14;
const DdlAdnColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DdlAdnColumnId14(AdmScreenObjState);
const DdlAdnColumnId14 = currMst.DdlAdnColumnId14;
const DdlFtrColumnId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DdlFtrColumnId14(AdmScreenObjState);
const DdlFtrColumnId14 = currMst.DdlFtrColumnId14;
const ColumnLink14 = currMst.ColumnLink14;
const DtlLstPosId14List = AdmScreenObjReduxObj.ScreenDdlSelectors.DtlLstPosId14(AdmScreenObjState);
const DtlLstPosId14 = currMst.DtlLstPosId14;
const LabelVertical14 = currMst.LabelVertical14;
const LabelCss14 = currMst.LabelCss14;
const ContentCss14 = currMst.ContentCss14;
const DefaultValue14 = currMst.DefaultValue14;
const HyperLinkUrl14 = currMst.HyperLinkUrl14;
const DefAfter14 = currMst.DefAfter14;
const SystemValue14 = currMst.SystemValue14;
const DefAlways14 = currMst.DefAlways14;
const AggregateCd14List = AdmScreenObjReduxObj.ScreenDdlSelectors.AggregateCd14(AdmScreenObjState);
const AggregateCd14 = currMst.AggregateCd14;
const GenerateSp14 = currMst.GenerateSp14;
const MaskValid14 = currMst.MaskValid14;
const RangeValidType14 = currMst.RangeValidType14;
const RangeValidMax14 = currMst.RangeValidMax14;
const RangeValidMin14 = currMst.RangeValidMin14;
const MatchCd14List = AdmScreenObjReduxObj.ScreenDdlSelectors.MatchCd14(AdmScreenObjState);
const MatchCd14 = currMst.MatchCd14;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmScreenObjState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cScreenObjId14: ScreenObjId14 || '',
                  cMasterTable14: MasterTable14 === 'Y',
                  cRequiredValid14: RequiredValid14 === 'Y',
                  cColumnWrap14: ColumnWrap14 === 'Y',
                  cGridGrpCd14: GridGrpCd14List.filter(obj => { return obj.key === GridGrpCd14 })[0],
                  cHideOnTablet14: HideOnTablet14 === 'Y',
                  cHideOnMobile14: HideOnMobile14 === 'Y',
                  cRefreshOnCUD14: RefreshOnCUD14 === 'Y',
                  cTrimOnEntry14: TrimOnEntry14 === 'Y',
                  cIgnoreConfirm14: IgnoreConfirm14 === 'Y',
                  cColumnJustify14: ColumnJustify14List.filter(obj => { return obj.key === ColumnJustify14 })[0],
                  cColumnSize14: ColumnSize14 || '',
                  cColumnHeight14: ColumnHeight14 || '',
                  cResizeWidth14: ResizeWidth14 || '',
                  cResizeHeight14: ResizeHeight14 || '',
                  cSortOrder14: SortOrder14 || '',
                  cScreenId14: ScreenId14List.filter(obj => { return obj.key === ScreenId14 })[0],
                  cGroupRowId14: GroupRowId14List.filter(obj => { return obj.key === GroupRowId14 })[0],
                  cGroupColId14: GroupColId14List.filter(obj => { return obj.key === GroupColId14 })[0],
                  cColumnId14: ColumnId14List.filter(obj => { return obj.key === ColumnId14 })[0],
                  cColumnName14: ColumnName14 || '',
                  cDisplayModeId14: DisplayModeId14List.filter(obj => { return obj.key === DisplayModeId14 })[0],
                  cDisplayDesc18: DisplayDesc18 || '',
                  cDdlKeyColumnId14: DdlKeyColumnId14List.filter(obj => { return obj.key === DdlKeyColumnId14 })[0],
                  cDdlRefColumnId14: DdlRefColumnId14List.filter(obj => { return obj.key === DdlRefColumnId14 })[0],
                  cDdlSrtColumnId14: DdlSrtColumnId14List.filter(obj => { return obj.key === DdlSrtColumnId14 })[0],
                  cDdlAdnColumnId14: DdlAdnColumnId14List.filter(obj => { return obj.key === DdlAdnColumnId14 })[0],
                  cDdlFtrColumnId14: DdlFtrColumnId14List.filter(obj => { return obj.key === DdlFtrColumnId14 })[0],
                  cColumnLink14: ColumnLink14 || '',
                  cDtlLstPosId14: DtlLstPosId14List.filter(obj => { return obj.key === DtlLstPosId14 })[0],
                  cLabelVertical14: LabelVertical14 === 'Y',
                  cLabelCss14: LabelCss14 || '',
                  cContentCss14: ContentCss14 || '',
                  cDefaultValue14: DefaultValue14 || '',
                  cHyperLinkUrl14: HyperLinkUrl14 || '',
                  cDefAfter14: DefAfter14 === 'Y',
                  cSystemValue14: SystemValue14 || '',
                  cDefAlways14: DefAlways14 === 'Y',
                  cAggregateCd14: AggregateCd14List.filter(obj => { return obj.key === AggregateCd14 })[0],
                  cGenerateSp14: GenerateSp14 === 'Y',
                  cMaskValid14: MaskValid14 || '',
                  cRangeValidType14: RangeValidType14 || '',
                  cRangeValidMax14: RangeValidMax14 || '',
                  cRangeValidMin14: RangeValidMin14 || '',
                  cMatchCd14: MatchCd14List.filter(obj => { return obj.key === MatchCd14 })[0],
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
                                {(this.constructor.ShowSpinner(AdmScreenObjState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14)) return null;
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
            {(authCol.ScreenObjId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenObjId14 || {}).ColumnHeader} {(columnLabel.ScreenObjId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenObjId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenObjId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cScreenObjId14'
disabled = {(authCol.ScreenObjId14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cScreenObjId14 && touched.cScreenObjId14 && <span className='form__form-group-error'>{errors.cScreenObjId14}</span>}
</div>
</Col>
}
{(authCol.MasterTable14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cMasterTable14'
onChange={handleChange}
defaultChecked={values.cMasterTable14}
disabled={(authCol.MasterTable14 || {}).readonly || !(authCol.MasterTable14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.MasterTable14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.MasterTable14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MasterTable14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MasterTable14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.RequiredValid14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cRequiredValid14'
onChange={handleChange}
defaultChecked={values.cRequiredValid14}
disabled={(authCol.RequiredValid14 || {}).readonly || !(authCol.RequiredValid14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.RequiredValid14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.RequiredValid14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RequiredValid14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RequiredValid14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColumnWrap14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cColumnWrap14'
onChange={handleChange}
defaultChecked={values.cColumnWrap14}
disabled={(authCol.ColumnWrap14 || {}).readonly || !(authCol.ColumnWrap14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.ColumnWrap14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.ColumnWrap14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnWrap14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnWrap14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.GridGrpCd14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GridGrpCd14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.GridGrpCd14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GridGrpCd14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GridGrpCd14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cGridGrpCd14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cGridGrpCd14')}
value={values.cGridGrpCd14}
options={GridGrpCd14List}
placeholder=''
disabled = {(authCol.GridGrpCd14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cGridGrpCd14 && touched.cGridGrpCd14 && <span className='form__form-group-error'>{errors.cGridGrpCd14}</span>}
</div>
</Col>
}
{(authCol.HideOnTablet14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cHideOnTablet14'
onChange={handleChange}
defaultChecked={values.cHideOnTablet14}
disabled={(authCol.HideOnTablet14 || {}).readonly || !(authCol.HideOnTablet14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.HideOnTablet14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.HideOnTablet14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HideOnTablet14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HideOnTablet14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.HideOnMobile14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cHideOnMobile14'
onChange={handleChange}
defaultChecked={values.cHideOnMobile14}
disabled={(authCol.HideOnMobile14 || {}).readonly || !(authCol.HideOnMobile14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.HideOnMobile14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.HideOnMobile14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HideOnMobile14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HideOnMobile14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.RefreshOnCUD14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cRefreshOnCUD14'
onChange={handleChange}
defaultChecked={values.cRefreshOnCUD14}
disabled={(authCol.RefreshOnCUD14 || {}).readonly || !(authCol.RefreshOnCUD14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.RefreshOnCUD14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.RefreshOnCUD14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RefreshOnCUD14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RefreshOnCUD14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.TrimOnEntry14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cTrimOnEntry14'
onChange={handleChange}
defaultChecked={values.cTrimOnEntry14}
disabled={(authCol.TrimOnEntry14 || {}).readonly || !(authCol.TrimOnEntry14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.TrimOnEntry14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.TrimOnEntry14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TrimOnEntry14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TrimOnEntry14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.IgnoreConfirm14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIgnoreConfirm14'
onChange={handleChange}
defaultChecked={values.cIgnoreConfirm14}
disabled={(authCol.IgnoreConfirm14 || {}).readonly || !(authCol.IgnoreConfirm14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IgnoreConfirm14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IgnoreConfirm14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IgnoreConfirm14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IgnoreConfirm14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.ColumnJustify14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnJustify14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnJustify14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnJustify14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnJustify14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cColumnJustify14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cColumnJustify14')}
value={values.cColumnJustify14}
options={ColumnJustify14List}
placeholder=''
disabled = {(authCol.ColumnJustify14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnJustify14 && touched.cColumnJustify14 && <span className='form__form-group-error'>{errors.cColumnJustify14}</span>}
</div>
</Col>
}
{(authCol.ColumnSize14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnSize14 || {}).ColumnHeader} {(columnLabel.ColumnSize14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnSize14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnSize14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnSize14'
disabled = {(authCol.ColumnSize14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnSize14 && touched.cColumnSize14 && <span className='form__form-group-error'>{errors.cColumnSize14}</span>}
</div>
</Col>
}
{(authCol.ColumnHeight14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnHeight14 || {}).ColumnHeader} {(columnLabel.ColumnHeight14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnHeight14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnHeight14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnHeight14'
disabled = {(authCol.ColumnHeight14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnHeight14 && touched.cColumnHeight14 && <span className='form__form-group-error'>{errors.cColumnHeight14}</span>}
</div>
</Col>
}
{(authCol.ResizeWidth14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ResizeWidth14 || {}).ColumnHeader} {(columnLabel.ResizeWidth14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ResizeWidth14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ResizeWidth14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cResizeWidth14'
disabled = {(authCol.ResizeWidth14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cResizeWidth14 && touched.cResizeWidth14 && <span className='form__form-group-error'>{errors.cResizeWidth14}</span>}
</div>
</Col>
}
{(authCol.ResizeHeight14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ResizeHeight14 || {}).ColumnHeader} {(columnLabel.ResizeHeight14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ResizeHeight14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ResizeHeight14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cResizeHeight14'
disabled = {(authCol.ResizeHeight14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cResizeHeight14 && touched.cResizeHeight14 && <span className='form__form-group-error'>{errors.cResizeHeight14}</span>}
</div>
</Col>
}
{(authCol.SortOrder14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SortOrder14 || {}).ColumnHeader} {(columnLabel.SortOrder14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SortOrder14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SortOrder14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSortOrder14'
disabled = {(authCol.SortOrder14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSortOrder14 && touched.cSortOrder14 && <span className='form__form-group-error'>{errors.cSortOrder14}</span>}
</div>
</Col>
}
{(authCol.ScreenId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ScreenId14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ScreenId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ScreenId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ScreenId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cScreenId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cScreenId14', true)}
onInputChange={this.ScreenId14InputChange()}
value={values.cScreenId14}
defaultSelected={ScreenId14List.filter(obj => { return obj.key === ScreenId14 })}
options={ScreenId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ScreenId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cScreenId14 && touched.cScreenId14 && <span className='form__form-group-error'>{errors.cScreenId14}</span>}
</div>
</Col>
}
{(authCol.GroupRowId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupRowId14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.GroupRowId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupRowId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupRowId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cGroupRowId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupRowId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupRowId14', true)}
onInputChange={this.GroupRowId14InputChange()}
value={values.cGroupRowId14}
defaultSelected={GroupRowId14List.filter(obj => { return obj.key === GroupRowId14 })}
options={GroupRowId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.GroupRowId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cGroupRowId14 && touched.cGroupRowId14 && <span className='form__form-group-error'>{errors.cGroupRowId14}</span>}
</div>
</Col>
}
{(authCol.GroupColId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GroupColId14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.GroupColId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GroupColId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GroupColId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cGroupColId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupColId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGroupColId14', true)}
onInputChange={this.GroupColId14InputChange()}
value={values.cGroupColId14}
defaultSelected={GroupColId14List.filter(obj => { return obj.key === GroupColId14 })}
options={GroupColId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.GroupColId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cGroupColId14 && touched.cGroupColId14 && <span className='form__form-group-error'>{errors.cGroupColId14}</span>}
</div>
</Col>
}
{(authCol.ColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnId14 || {}).ColumnHeader} {(columnLabel.ColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cColumnId14', true)}
onInputChange={this.ColumnId14InputChange()}
value={values.cColumnId14}
defaultSelected={ColumnId14List.filter(obj => { return obj.key === ColumnId14 })}
options={ColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.ColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cColumnId14 && touched.cColumnId14 && <span className='form__form-group-error'>{errors.cColumnId14}</span>}
</div>
</Col>
}
{(authCol.ColumnName14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnName14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ColumnName14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnName14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnName14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnName14'
disabled = {(authCol.ColumnName14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnName14 && touched.cColumnName14 && <span className='form__form-group-error'>{errors.cColumnName14}</span>}
</div>
</Col>
}
{(authCol.DisplayModeId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DisplayModeId14 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DisplayModeId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DisplayModeId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DisplayModeId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDisplayModeId14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDisplayModeId14')}
value={values.cDisplayModeId14}
options={DisplayModeId14List}
placeholder=''
disabled = {(authCol.DisplayModeId14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDisplayModeId14 && touched.cDisplayModeId14 && <span className='form__form-group-error'>{errors.cDisplayModeId14}</span>}
</div>
</Col>
}
{(authCol.DisplayDesc18 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DisplayDesc18 || {}).ColumnHeader} {(columnLabel.DisplayDesc18 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DisplayDesc18 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DisplayDesc18 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDisplayDesc18'
disabled = {(authCol.DisplayDesc18 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDisplayDesc18 && touched.cDisplayDesc18 && <span className='form__form-group-error'>{errors.cDisplayDesc18}</span>}
</div>
</Col>
}
{(authCol.DdlKeyColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlKeyColumnId14 || {}).ColumnHeader} {(columnLabel.DdlKeyColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlKeyColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlKeyColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlKeyColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlKeyColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlKeyColumnId14', true)}
onInputChange={this.DdlKeyColumnId14InputChange()}
value={values.cDdlKeyColumnId14}
defaultSelected={DdlKeyColumnId14List.filter(obj => { return obj.key === DdlKeyColumnId14 })}
options={DdlKeyColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlKeyColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlKeyColumnId14 && touched.cDdlKeyColumnId14 && <span className='form__form-group-error'>{errors.cDdlKeyColumnId14}</span>}
</div>
</Col>
}
{(authCol.DdlRefColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlRefColumnId14 || {}).ColumnHeader} {(columnLabel.DdlRefColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlRefColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlRefColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlRefColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlRefColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlRefColumnId14', true)}
onInputChange={this.DdlRefColumnId14InputChange()}
value={values.cDdlRefColumnId14}
defaultSelected={DdlRefColumnId14List.filter(obj => { return obj.key === DdlRefColumnId14 })}
options={DdlRefColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlRefColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlRefColumnId14 && touched.cDdlRefColumnId14 && <span className='form__form-group-error'>{errors.cDdlRefColumnId14}</span>}
</div>
</Col>
}
{(authCol.DdlSrtColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlSrtColumnId14 || {}).ColumnHeader} {(columnLabel.DdlSrtColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlSrtColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlSrtColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlSrtColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlSrtColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlSrtColumnId14', true)}
onInputChange={this.DdlSrtColumnId14InputChange()}
value={values.cDdlSrtColumnId14}
defaultSelected={DdlSrtColumnId14List.filter(obj => { return obj.key === DdlSrtColumnId14 })}
options={DdlSrtColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlSrtColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlSrtColumnId14 && touched.cDdlSrtColumnId14 && <span className='form__form-group-error'>{errors.cDdlSrtColumnId14}</span>}
</div>
</Col>
}
{(authCol.DdlAdnColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlAdnColumnId14 || {}).ColumnHeader} {(columnLabel.DdlAdnColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlAdnColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlAdnColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlAdnColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlAdnColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlAdnColumnId14', true)}
onInputChange={this.DdlAdnColumnId14InputChange()}
value={values.cDdlAdnColumnId14}
defaultSelected={DdlAdnColumnId14List.filter(obj => { return obj.key === DdlAdnColumnId14 })}
options={DdlAdnColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlAdnColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlAdnColumnId14 && touched.cDdlAdnColumnId14 && <span className='form__form-group-error'>{errors.cDdlAdnColumnId14}</span>}
</div>
</Col>
}
{(authCol.DdlFtrColumnId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DdlFtrColumnId14 || {}).ColumnHeader} {(columnLabel.DdlFtrColumnId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DdlFtrColumnId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DdlFtrColumnId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cDdlFtrColumnId14'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId14', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cDdlFtrColumnId14', true)}
onInputChange={this.DdlFtrColumnId14InputChange()}
value={values.cDdlFtrColumnId14}
defaultSelected={DdlFtrColumnId14List.filter(obj => { return obj.key === DdlFtrColumnId14 })}
options={DdlFtrColumnId14List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.DdlFtrColumnId14 || {}).readonly ? true: false }/>
</div>
}
{errors.cDdlFtrColumnId14 && touched.cDdlFtrColumnId14 && <span className='form__form-group-error'>{errors.cDdlFtrColumnId14}</span>}
</div>
</Col>
}
{(authCol.ColumnLink14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ColumnLink14 || {}).ColumnHeader} {(columnLabel.ColumnLink14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ColumnLink14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ColumnLink14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cColumnLink14'
disabled = {(authCol.ColumnLink14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cColumnLink14 && touched.cColumnLink14 && <span className='form__form-group-error'>{errors.cColumnLink14}</span>}
</div>
</Col>
}
{(authCol.DtlLstPosId14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DtlLstPosId14 || {}).ColumnHeader} {(columnLabel.DtlLstPosId14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DtlLstPosId14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DtlLstPosId14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDtlLstPosId14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDtlLstPosId14')}
value={values.cDtlLstPosId14}
options={DtlLstPosId14List}
placeholder=''
disabled = {(authCol.DtlLstPosId14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDtlLstPosId14 && touched.cDtlLstPosId14 && <span className='form__form-group-error'>{errors.cDtlLstPosId14}</span>}
</div>
</Col>
}
{(authCol.LabelVertical14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cLabelVertical14'
onChange={handleChange}
defaultChecked={values.cLabelVertical14}
disabled={(authCol.LabelVertical14 || {}).readonly || !(authCol.LabelVertical14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.LabelVertical14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.LabelVertical14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelVertical14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelVertical14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.LabelCss14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LabelCss14 || {}).ColumnHeader} {(columnLabel.LabelCss14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LabelCss14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LabelCss14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLabelCss14'
disabled = {(authCol.LabelCss14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLabelCss14 && touched.cLabelCss14 && <span className='form__form-group-error'>{errors.cLabelCss14}</span>}
</div>
</Col>
}
{(authCol.ContentCss14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ContentCss14 || {}).ColumnHeader} {(columnLabel.ContentCss14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ContentCss14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ContentCss14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cContentCss14'
disabled = {(authCol.ContentCss14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cContentCss14 && touched.cContentCss14 && <span className='form__form-group-error'>{errors.cContentCss14}</span>}
</div>
</Col>
}
{(authCol.DefaultValue14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultValue14 || {}).ColumnHeader} {(columnLabel.DefaultValue14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultValue14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultValue14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefaultValue14'
disabled = {(authCol.DefaultValue14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultValue14 && touched.cDefaultValue14 && <span className='form__form-group-error'>{errors.cDefaultValue14}</span>}
</div>
</Col>
}
{(authCol.HyperLinkUrl14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.HyperLinkUrl14 || {}).ColumnHeader} {(columnLabel.HyperLinkUrl14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HyperLinkUrl14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HyperLinkUrl14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cHyperLinkUrl14'
disabled = {(authCol.HyperLinkUrl14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cHyperLinkUrl14 && touched.cHyperLinkUrl14 && <span className='form__form-group-error'>{errors.cHyperLinkUrl14}</span>}
</div>
</Col>
}
{(authCol.DefAfter14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cDefAfter14'
onChange={handleChange}
defaultChecked={values.cDefAfter14}
disabled={(authCol.DefAfter14 || {}).readonly || !(authCol.DefAfter14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.DefAfter14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.DefAfter14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefAfter14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefAfter14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.SystemValue14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemValue14 || {}).ColumnHeader} {(columnLabel.SystemValue14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemValue14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemValue14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cSystemValue14'
disabled = {(authCol.SystemValue14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemValue14 && touched.cSystemValue14 && <span className='form__form-group-error'>{errors.cSystemValue14}</span>}
</div>
</Col>
}
{(authCol.DefAlways14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cDefAlways14'
onChange={handleChange}
defaultChecked={values.cDefAlways14}
disabled={(authCol.DefAlways14 || {}).readonly || !(authCol.DefAlways14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.DefAlways14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.DefAlways14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefAlways14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefAlways14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.AggregateCd14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AggregateCd14 || {}).ColumnHeader} {(columnLabel.AggregateCd14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AggregateCd14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AggregateCd14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cAggregateCd14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cAggregateCd14')}
value={values.cAggregateCd14}
options={AggregateCd14List}
placeholder=''
disabled = {(authCol.AggregateCd14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cAggregateCd14 && touched.cAggregateCd14 && <span className='form__form-group-error'>{errors.cAggregateCd14}</span>}
</div>
</Col>
}
{(authCol.GenerateSp14 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cGenerateSp14'
onChange={handleChange}
defaultChecked={values.cGenerateSp14}
disabled={(authCol.GenerateSp14 || {}).readonly || !(authCol.GenerateSp14 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.GenerateSp14 || {}).ColumnHeader}</span>
</label>
{(columnLabel.GenerateSp14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GenerateSp14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GenerateSp14 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.MaskValid14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MaskValid14 || {}).ColumnHeader} {(columnLabel.MaskValid14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MaskValid14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MaskValid14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMaskValid14'
disabled = {(authCol.MaskValid14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMaskValid14 && touched.cMaskValid14 && <span className='form__form-group-error'>{errors.cMaskValid14}</span>}
</div>
</Col>
}
{(authCol.RangeValidType14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RangeValidType14 || {}).ColumnHeader} {(columnLabel.RangeValidType14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RangeValidType14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RangeValidType14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRangeValidType14'
disabled = {(authCol.RangeValidType14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRangeValidType14 && touched.cRangeValidType14 && <span className='form__form-group-error'>{errors.cRangeValidType14}</span>}
</div>
</Col>
}
{(authCol.RangeValidMax14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RangeValidMax14 || {}).ColumnHeader} {(columnLabel.RangeValidMax14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RangeValidMax14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RangeValidMax14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRangeValidMax14'
disabled = {(authCol.RangeValidMax14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRangeValidMax14 && touched.cRangeValidMax14 && <span className='form__form-group-error'>{errors.cRangeValidMax14}</span>}
</div>
</Col>
}
{(authCol.RangeValidMin14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RangeValidMin14 || {}).ColumnHeader} {(columnLabel.RangeValidMin14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RangeValidMin14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RangeValidMin14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRangeValidMin14'
disabled = {(authCol.RangeValidMin14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRangeValidMin14 && touched.cRangeValidMin14 && <span className='form__form-group-error'>{errors.cRangeValidMin14}</span>}
</div>
</Col>
}
{(authCol.MatchCd14 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MatchCd14 || {}).ColumnHeader} {(columnLabel.MatchCd14 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MatchCd14 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MatchCd14 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmScreenObjState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cMatchCd14'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cMatchCd14')}
value={values.cMatchCd14}
options={MatchCd14List}
placeholder=''
disabled = {(authCol.MatchCd14 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMatchCd14 && touched.cMatchCd14 && <span className='form__form-group-error'>{errors.cMatchCd14}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenObjId14)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).ScreenObjId14));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmScreenObjState) && <Skeleton height='43px' />) ||
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
  AdmScreenObj: state.AdmScreenObj,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScreenObjReduxObj.LoadPage.bind(AdmScreenObjReduxObj) },
    { SavePage: AdmScreenObjReduxObj.SavePage.bind(AdmScreenObjReduxObj) },
    { DelMst: AdmScreenObjReduxObj.DelMst.bind(AdmScreenObjReduxObj) },
    { AddMst: AdmScreenObjReduxObj.AddMst.bind(AdmScreenObjReduxObj) },
//    { SearchMemberId64: AdmScreenObjReduxObj.SearchActions.SearchMemberId64.bind(AdmScreenObjReduxObj) },
//    { SearchCurrencyId64: AdmScreenObjReduxObj.SearchActions.SearchCurrencyId64.bind(AdmScreenObjReduxObj) },
//    { SearchCustomerJobId64: AdmScreenObjReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmScreenObjReduxObj) },
{ SearchScreenId14: AdmScreenObjReduxObj.SearchActions.SearchScreenId14.bind(AdmScreenObjReduxObj) },
{ SearchGroupRowId14: AdmScreenObjReduxObj.SearchActions.SearchGroupRowId14.bind(AdmScreenObjReduxObj) },
{ SearchGroupColId14: AdmScreenObjReduxObj.SearchActions.SearchGroupColId14.bind(AdmScreenObjReduxObj) },
{ SearchColumnId14: AdmScreenObjReduxObj.SearchActions.SearchColumnId14.bind(AdmScreenObjReduxObj) },
{ SearchDdlKeyColumnId14: AdmScreenObjReduxObj.SearchActions.SearchDdlKeyColumnId14.bind(AdmScreenObjReduxObj) },
{ SearchDdlRefColumnId14: AdmScreenObjReduxObj.SearchActions.SearchDdlRefColumnId14.bind(AdmScreenObjReduxObj) },
{ SearchDdlSrtColumnId14: AdmScreenObjReduxObj.SearchActions.SearchDdlSrtColumnId14.bind(AdmScreenObjReduxObj) },
{ SearchDdlAdnColumnId14: AdmScreenObjReduxObj.SearchActions.SearchDdlAdnColumnId14.bind(AdmScreenObjReduxObj) },
{ SearchDdlFtrColumnId14: AdmScreenObjReduxObj.SearchActions.SearchDdlFtrColumnId14.bind(AdmScreenObjReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            