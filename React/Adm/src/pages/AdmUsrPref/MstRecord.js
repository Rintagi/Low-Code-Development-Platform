
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
import ListBox from '../../components/custom/ListBox';
import { default as FileInputFieldV1 } from '../../components/custom/FileInputV1';
import { default as FileInputField } from '../../components/custom/FileInput';
import RintagiScreen from '../../components/custom/Screen';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import { isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getDefaultPath, decodeEmbeddedFileObjectFromServer } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat, formatContent } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import AdmUsrPrefReduxObj, { ShowMstFilterApplied } from '../../redux/AdmUsrPref';
import * as AdmUsrPrefService from '../../services/AdmUsrPrefService';
import { getRintagiConfig } from '../../helpers/config';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';
import log from '../../helpers/logger';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = () => (this.props.AdmUsrPref || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'UsrPrefId93';
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
    this.StripEmbeddedBase64Prefix = this.StripEmbeddedBase64Prefix.bind(this);
    this.DropdownChangeV1 = this.DropdownChangeV1.bind(this);
    this.FileUploadChangeV1 = this.FileUploadChangeV1.bind(this);
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

  UsrId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchUsrId93(v, filterBy); } }
  CompanyId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchCompanyId93(v, filterBy); } }
  ProjectId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchProjectId93(v, filterBy); } }
  MemberId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchMemberId93(v, filterBy); } }
  AgentId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchAgentId93(v, filterBy); } }
  BrokerId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchBrokerId93(v, filterBy); } }
  CustomerId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchCustomerId93(v, filterBy); } }
  InvestorId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchInvestorId93(v, filterBy); } }
  VendorId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchVendorId93(v, filterBy); } }
  LenderId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchLenderId93(v, filterBy); } }
  BorrowerId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchBorrowerId93(v, filterBy); } }
  GuarantorId93InputChange() { const _this = this; return function (name, v) { const filterBy = ''; _this.props.SearchGuarantorId93(v, filterBy); } }
  /* ReactRule: Master Record Custom Function */

  /* ReactRule End: Master Record Custom Function */

  /* form related input handling */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmUsrPref || {}).ColumnLabel || {};
    /* standard field validation */
    if (!values.cUsrPrefDesc93) { errors.cUsrPrefDesc93 = (columnLabel.UsrPrefDesc93 || {}).ErrMessage; }
    if (isEmptyId((values.cMenuOptId93 || {}).value)) { errors.cMenuOptId93 = (columnLabel.MenuOptId93 || {}).ErrMessage; }
    if (isEmptyId((values.cComListVisible93 || {}).value)) { errors.cComListVisible93 = (columnLabel.ComListVisible93 || {}).ErrMessage; }
    if (isEmptyId((values.cPrjListVisible93 || {}).value)) { errors.cPrjListVisible93 = (columnLabel.PrjListVisible93 || {}).ErrMessage; }
    if (isEmptyId((values.cSysListVisible93 || {}).value)) { errors.cSysListVisible93 = (columnLabel.SysListVisible93 || {}).ErrMessage; }
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmUsrPref || {}).Mst || {};

    /* ReactRule: Master Record Save */

    /* ReactRule End: Master Record Save */

    if (errors.length > 0) {
      this.props.showNotification('E', { message: errors[0] });
      setSubmitting(false);
    }
    else {
      const { ScreenButton, OnClickColumeName } = this;
      this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting, key: currMst.key, ScreenButton: ScreenButton, OnClickColumeName: OnClickColumeName });
      this.ScreenButton = null;
      this.OnClickColumeName = null;
      this.props.SavePage(
        this.props.AdmUsrPref,
        {
          UsrPrefId93: values.cUsrPrefId93 || '',
          UsrPrefDesc93: values.cUsrPrefDesc93 || '',
          MenuOptId93: (values.cMenuOptId93 || {}).value || '',
          MasterPgFile93: values.cMasterPgFile93 || '',
          ComListVisible93: (values.cComListVisible93 || {}).value || '',
          PrjListVisible93: (values.cPrjListVisible93 || {}).value || '',
          SysListVisible93: (values.cSysListVisible93 || {}).value || '',
          PrefDefault93: values.cPrefDefault93 ? 'Y' : 'N',
          SampleImage93: values.cSampleImage93 && values.cSampleImage93.ts ?
            JSON.stringify({
              ...values.cSampleImage93,
              ts: undefined,
              lastTS: values.cSampleImage93.ts,
              base64: this.StripEmbeddedBase64Prefix(values.cSampleImage93.base64)
            }) : null,
          UsrStyleSheet93: values.cUsrStyleSheet93 || '',
          UsrId93: (values.cUsrId93 || {}).value || '',
          UsrGroupId93: (values.cUsrGroupId93 || {}).value || '',
          CompanyId93: (values.cCompanyId93 || {}).value || '',
          ProjectId93: (values.cProjectId93 || {}).value || '',
          SystemId93: (values.cSystemId93 || {}).value || '',
          MemberId93: (values.cMemberId93 || {}).value || '',
          AgentId93: (values.cAgentId93 || {}).value || '',
          BrokerId93: (values.cBrokerId93 || {}).value || '',
          CustomerId93: (values.cCustomerId93 || {}).value || '',
          InvestorId93: (values.cInvestorId93 || {}).value || '',
          VendorId93: (values.cVendorId93 || {}).value || '',
          LenderId93: (values.cLenderId93 || {}).value || '',
          BorrowerId93: (values.cBorrowerId93 || {}).value || '',
          GuarantorId93: (values.cGuarantorId93 || {}).value || '',
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
    const AdmUsrPrefState = this.props.AdmUsrPref || {};
    const auxSystemLabels = AdmUsrPrefState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).UsrPrefId93;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'MstRecord', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmUsrPref.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('MstRecord', {}, {}, this.props.AdmUsrPref.Label);
            this.props.history.push(getEditMstPath(getNaviPath(naviBar, 'MstRecord', '/'), '_'));
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
    const AdmUsrPrefState = this.props.AdmUsrPref || {};
    const auxSystemLabels = AdmUsrPrefState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.UsrPrefId93;
        this.props.DelMst(this.props.AdmUsrPref, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmUsrPref || {};
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
    const AdmUsrPrefState = this.props.AdmUsrPref || {};
    const auxSystemLabels = AdmUsrPrefState.SystemLabel || {};
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
      if (!(this.props.AdmUsrPref || {}).AuthCol || true) {
        this.props.LoadPage('MstRecord', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmUsrPref || {};

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
        const naviBar = getNaviBar('MstRecord', currMst, currDtl, currReduxScreenState.Label);
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
    const AdmUsrPrefState = this.props.AdmUsrPref || {};

    if (AdmUsrPrefState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmUsrPrefState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmUsrPrefReduxObj.GetScreenButtons(AdmUsrPrefState) || {};
    const itemList = AdmUsrPrefState.Dtl || [];
    const auxLabels = AdmUsrPrefState.Label || {};
    const auxSystemLabels = AdmUsrPrefState.SystemLabel || {};

    const columnLabel = AdmUsrPrefState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmUsrPrefState);
    const authRow = (AdmUsrPrefState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmUsrPref || {}).Mst || {});
    const currDtl = ((this.props.AdmUsrPref || {}).EditDtl || {});
    const naviBar = getNaviBar('MstRecord', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'DtlRecord' && v.type !== 'DtlList') || currMst.UsrPrefId93));
    const selectList = AdmUsrPrefReduxObj.SearchListToSelectList(AdmUsrPrefState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};

    const UsrPrefId93 = currMst.UsrPrefId93;
    const UsrPrefDesc93 = currMst.UsrPrefDesc93;
    const MenuOptId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.MenuOptId93(AdmUsrPrefState);
    const MenuOptId93 = currMst.MenuOptId93;
    const MasterPgFile93 = currMst.MasterPgFile93;
    const ComListVisible93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.ComListVisible93(AdmUsrPrefState);
    const ComListVisible93 = currMst.ComListVisible93;
    const PrjListVisible93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.PrjListVisible93(AdmUsrPrefState);
    const PrjListVisible93 = currMst.PrjListVisible93;
    const SysListVisible93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.SysListVisible93(AdmUsrPrefState);
    const SysListVisible93 = currMst.SysListVisible93;
    const PrefDefault93 = currMst.PrefDefault93;
    const SampleImage93 = currMst.SampleImage93 ? decodeEmbeddedFileObjectFromServer(currMst.SampleImage93) : null;
    const SampleImage93FileUploadOptions = {
      CancelFileButton: auxSystemLabels.CancelFileBtnLabel,
      DeleteFileButton: auxSystemLabels.DeleteFileBtnLabel,
      MaxImageSize: {
        Width: (columnLabel.SampleImage93 || {}).ResizeWidth,
        Height: (columnLabel.SampleImage93 || {}).ResizeHeight,
      },
      MinImageSize: {
        Width: (columnLabel.SampleImage93 || {}).ColumnSize,
        Height: (columnLabel.SampleImage93 || {}).ColumnHeight,
      },
    }
    const UsrStyleSheet93 = currMst.UsrStyleSheet93;
    const UsrId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.UsrId93(AdmUsrPrefState);
    const UsrId93 = currMst.UsrId93;
    const UsrGroupId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.UsrGroupId93(AdmUsrPrefState);
    const UsrGroupId93 = currMst.UsrGroupId93;
    const CompanyId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.CompanyId93(AdmUsrPrefState);
    const CompanyId93 = currMst.CompanyId93;
    const ProjectId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.ProjectId93(AdmUsrPrefState);
    const ProjectId93 = currMst.ProjectId93;
    const SystemId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.SystemId93(AdmUsrPrefState);
    const SystemId93 = currMst.SystemId93;
    const MemberId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.MemberId93(AdmUsrPrefState);
    const MemberId93 = currMst.MemberId93;
    const AgentId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.AgentId93(AdmUsrPrefState);
    const AgentId93 = currMst.AgentId93;
    const BrokerId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.BrokerId93(AdmUsrPrefState);
    const BrokerId93 = currMst.BrokerId93;
    const CustomerId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.CustomerId93(AdmUsrPrefState);
    const CustomerId93 = currMst.CustomerId93;
    const InvestorId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.InvestorId93(AdmUsrPrefState);
    const InvestorId93 = currMst.InvestorId93;
    const VendorId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.VendorId93(AdmUsrPrefState);
    const VendorId93 = currMst.VendorId93;
    const LenderId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.LenderId93(AdmUsrPrefState);
    const LenderId93 = currMst.LenderId93;
    const BorrowerId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.BorrowerId93(AdmUsrPrefState);
    const BorrowerId93 = currMst.BorrowerId93;
    const GuarantorId93List = AdmUsrPrefReduxObj.ScreenDdlSelectors.GuarantorId93(AdmUsrPrefState);
    const GuarantorId93 = currMst.GuarantorId93;

    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const fileFileUploadOptions = {
      CancelFileButton: 'Cancel',
      DeleteFileButton: 'Delete',
      MaxImageSize: {
        Width: 1024,
        Height: 768,
      },
      MinImageSize: {
        Width: 40,
        Height: 40,
      },
      maxSize: 5 * 1024 * 1024,
    }

    /* ReactRule: Master Render */

    /* ReactRule End: Master Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmUsrPrefState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                    cUsrPrefId93: formatContent(UsrPrefId93 || '', 'TextBox'),
                    cUsrPrefDesc93: formatContent(UsrPrefDesc93 || '', 'TextBox'),
                    cMenuOptId93: MenuOptId93List.filter(obj => { return obj.key === MenuOptId93 })[0],
                    cMasterPgFile93: formatContent(MasterPgFile93 || '', 'TextBox'),
                    cComListVisible93: ComListVisible93List.filter(obj => { return obj.key === ComListVisible93 })[0],
                    cPrjListVisible93: PrjListVisible93List.filter(obj => { return obj.key === PrjListVisible93 })[0],
                    cSysListVisible93: SysListVisible93List.filter(obj => { return obj.key === SysListVisible93 })[0],
                    cPrefDefault93: PrefDefault93 === 'Y',
                    cSampleImage93: SampleImage93,
                    cUsrStyleSheet93: formatContent(UsrStyleSheet93 || '', 'MultiLine'),
                    cUsrId93: UsrId93List.filter(obj => { return obj.key === UsrId93 })[0],
                    cUsrGroupId93: UsrGroupId93List.filter(obj => { return obj.key === UsrGroupId93 })[0],
                    cCompanyId93: CompanyId93List.filter(obj => { return obj.key === CompanyId93 })[0],
                    cProjectId93: ProjectId93List.filter(obj => { return obj.key === ProjectId93 })[0],
                    cSystemId93: SystemId93List.filter(obj => { return obj.key === SystemId93 })[0],
                    cMemberId93: MemberId93List.filter(obj => { return obj.key === MemberId93 })[0],
                    cAgentId93: AgentId93List.filter(obj => { return obj.key === AgentId93 })[0],
                    cBrokerId93: BrokerId93List.filter(obj => { return obj.key === BrokerId93 })[0],
                    cCustomerId93: CustomerId93List.filter(obj => { return obj.key === CustomerId93 })[0],
                    cInvestorId93: InvestorId93List.filter(obj => { return obj.key === InvestorId93 })[0],
                    cVendorId93: VendorId93List.filter(obj => { return obj.key === VendorId93 })[0],
                    cLenderId93: LenderId93List.filter(obj => { return obj.key === LenderId93 })[0],
                    cBorrowerId93: BorrowerId93List.filter(obj => { return obj.key === BorrowerId93 })[0],
                    cGuarantorId93: GuarantorId93List.filter(obj => { return obj.key === GuarantorId93 })[0],
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
                                {(this.constructor.ShowSpinner(AdmUsrPrefState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrPrefId93)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrPrefId93)) return null;
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
                          {(selectedMst || {}).key ?
                            <div className='form__form-group'>
                              <div className='form__form-group-narrow'>
                                <div className='form__form-group-field'>
                                  <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                    <span className='radio-btn__label color-blue fw-700 f-14'>{selectedMst.label || ''}</span>
                                    <span className='radio-btn__label__right color-blue fw-700 f-14'><span className='mr-5'>{selectedMst.labelR || ''}</span>
                                    </span>
                                  </span>
                                </div>
                              </div>
                              <div className='form__form-group-field'>
                                <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                  <span className='radio-btn__label color-blue fw-700 f-14'>{selectedMst.detail || ''}</span>
                                  <span className='radio-btn__label__right color-blue fw-700 f-14'><span className='mr-5'>{selectedMst.detailR || ''}</span>
                                  </span>
                                </span>
                              </div>
                            </div>
                            :
                            <div className='form__form-group'>
                              <div className='form__form-group-narrow'>
                                <div className='form__form-group-field'>
                                  <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                    <span className='radio-btn__label color-blue fw-700 f-14'>{NoMasterMsg}</span>
                                  </span>
                                </div>
                              </div>
                            </div>
                          }
                          <div className='w-100'>
                            <Row>
                              {(authCol.UsrPrefId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UsrPrefId93 || {}).ColumnHeader} {(columnLabel.UsrPrefId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UsrPrefId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UsrPrefId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cUsrPrefId93'
                                          disabled={(authCol.UsrPrefId93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cUsrPrefId93 && touched.cUsrPrefId93 && <span className='form__form-group-error'>{errors.cUsrPrefId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.UsrPrefDesc93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UsrPrefDesc93 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.UsrPrefDesc93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UsrPrefDesc93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UsrPrefDesc93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cUsrPrefDesc93'
                                          disabled={(authCol.UsrPrefDesc93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cUsrPrefDesc93 && touched.cUsrPrefDesc93 && <span className='form__form-group-error'>{errors.cUsrPrefDesc93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MenuOptId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MenuOptId93 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.MenuOptId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MenuOptId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MenuOptId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cMenuOptId93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cMenuOptId93')}
                                          value={values.cMenuOptId93}
                                          options={MenuOptId93List}
                                          placeholder=''
                                          disabled={(authCol.MenuOptId93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMenuOptId93 && touched.cMenuOptId93 && <span className='form__form-group-error'>{errors.cMenuOptId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MasterPgFile93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MasterPgFile93 || {}).ColumnHeader} {(columnLabel.MasterPgFile93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MasterPgFile93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MasterPgFile93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          type='text'
                                          name='cMasterPgFile93'
                                          disabled={(authCol.MasterPgFile93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cMasterPgFile93 && touched.cMasterPgFile93 && <span className='form__form-group-error'>{errors.cMasterPgFile93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ComListVisible93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ComListVisible93 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ComListVisible93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ComListVisible93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ComListVisible93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cComListVisible93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cComListVisible93')}
                                          value={values.cComListVisible93}
                                          options={ComListVisible93List}
                                          placeholder=''
                                          disabled={(authCol.ComListVisible93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cComListVisible93 && touched.cComListVisible93 && <span className='form__form-group-error'>{errors.cComListVisible93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.PrjListVisible93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.PrjListVisible93 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.PrjListVisible93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.PrjListVisible93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.PrjListVisible93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cPrjListVisible93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cPrjListVisible93')}
                                          value={values.cPrjListVisible93}
                                          options={PrjListVisible93List}
                                          placeholder=''
                                          disabled={(authCol.PrjListVisible93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cPrjListVisible93 && touched.cPrjListVisible93 && <span className='form__form-group-error'>{errors.cPrjListVisible93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.SysListVisible93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SysListVisible93 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SysListVisible93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SysListVisible93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SysListVisible93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cSysListVisible93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cSysListVisible93')}
                                          value={values.cSysListVisible93}
                                          options={SysListVisible93List}
                                          placeholder=''
                                          disabled={(authCol.SysListVisible93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSysListVisible93 && touched.cSysListVisible93 && <span className='form__form-group-error'>{errors.cSysListVisible93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.PrefDefault93 || {}).visible &&
                                <Col lg={12} xl={12}>
                                  <div className='form__form-group'>
                                    <label className='checkbox-btn checkbox-btn--colored-click'>
                                      <Field
                                        className='checkbox-btn__checkbox'
                                        type='checkbox'
                                        name='cPrefDefault93'
                                        onChange={handleChange}
                                        defaultChecked={values.cPrefDefault93}
                                        disabled={(authCol.PrefDefault93 || {}).readonly || !(authCol.PrefDefault93 || {}).visible}
                                      />
                                      <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
                                      <span className='checkbox-btn__label'>{(columnLabel.PrefDefault93 || {}).ColumnHeader}</span>
                                    </label>
                                    {(columnLabel.PrefDefault93 || {}).ToolTip &&
                                      (<ControlledPopover id={(columnLabel.PrefDefault93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.PrefDefault93 || {}).ToolTip} />
                                      )}
                                  </div>
                                </Col>
                              }

                              {(authCol.SampleImage93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SampleImage93 || {}).ColumnHeader} {(columnLabel.SampleImage93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SampleImage93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SampleImage93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <FileInputFieldV1
                                          name='cSampleImage93'
                                          onChange={this.FileUploadChangeV1(setFieldValue, setFieldTouched, 'cSampleImage93')}
                                          fileInfo={{ filename: this.state.filename }}
                                          options={SampleImage93FileUploadOptions}
                                          value={values.cSampleImage93 || SampleImage93}
                                          label={auxSystemLabels.PickFileBtnLabel}
                                          onError={(e, fileName) => { this.props.showNotification('E', { message: 'problem loading file ' + fileName }) }}
                                        />
                                      </div>
                                    }
                                    {errors.cSampleImage93 && touched.cSampleImage93 && <span className='form__form-group-error'>{errors.cSampleImage93}</span>}
                                  </div>
                                </Col>
                              }

                              {(authCol.UsrStyleSheet93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UsrStyleSheet93 || {}).ColumnHeader} {(columnLabel.UsrStyleSheet93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UsrStyleSheet93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UsrStyleSheet93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <Field
                                          component='textarea'
                                          name='cUsrStyleSheet93'
                                          disabled={(authCol.UsrStyleSheet93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cUsrStyleSheet93 && touched.cUsrStyleSheet93 && <span className='form__form-group-error'>{errors.cUsrStyleSheet93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.UsrId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UsrId93 || {}).ColumnHeader} {(columnLabel.UsrId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UsrId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UsrId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cUsrId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cUsrId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cUsrId93', true)}
                                          onInputChange={this.UsrId93InputChange()}
                                          value={values.cUsrId93}
                                          defaultSelected={UsrId93List.filter(obj => { return obj.key === UsrId93 })}
                                          options={UsrId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.UsrId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cUsrId93 && touched.cUsrId93 && <span className='form__form-group-error'>{errors.cUsrId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.UsrGroupId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.UsrGroupId93 || {}).ColumnHeader} {(columnLabel.UsrGroupId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.UsrGroupId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.UsrGroupId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cUsrGroupId93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cUsrGroupId93')}
                                          value={values.cUsrGroupId93}
                                          options={UsrGroupId93List}
                                          placeholder=''
                                          disabled={(authCol.UsrGroupId93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cUsrGroupId93 && touched.cUsrGroupId93 && <span className='form__form-group-error'>{errors.cUsrGroupId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.CompanyId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.CompanyId93 || {}).ColumnHeader} {(columnLabel.CompanyId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.CompanyId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.CompanyId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cCompanyId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCompanyId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCompanyId93', true)}
                                          onInputChange={this.CompanyId93InputChange()}
                                          value={values.cCompanyId93}
                                          defaultSelected={CompanyId93List.filter(obj => { return obj.key === CompanyId93 })}
                                          options={CompanyId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.CompanyId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cCompanyId93 && touched.cCompanyId93 && <span className='form__form-group-error'>{errors.cCompanyId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.ProjectId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.ProjectId93 || {}).ColumnHeader} {(columnLabel.ProjectId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.ProjectId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.ProjectId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cProjectId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cProjectId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cProjectId93', true)}
                                          onInputChange={this.ProjectId93InputChange()}
                                          value={values.cProjectId93}
                                          defaultSelected={ProjectId93List.filter(obj => { return obj.key === ProjectId93 })}
                                          options={ProjectId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.ProjectId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cProjectId93 && touched.cProjectId93 && <span className='form__form-group-error'>{errors.cProjectId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.SystemId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.SystemId93 || {}).ColumnHeader} {(columnLabel.SystemId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.SystemId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.SystemId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <DropdownField
                                          name='cSystemId93'
                                          onChange={this.DropdownChangeV1(setFieldValue, setFieldTouched, 'cSystemId93')}
                                          value={values.cSystemId93}
                                          options={SystemId93List}
                                          placeholder=''
                                          disabled={(authCol.SystemId93 || {}).readonly ? 'disabled' : ''} />
                                      </div>
                                    }
                                    {errors.cSystemId93 && touched.cSystemId93 && <span className='form__form-group-error'>{errors.cSystemId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.MemberId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.MemberId93 || {}).ColumnHeader} {(columnLabel.MemberId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.MemberId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.MemberId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cMemberId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId93', true)}
                                          onInputChange={this.MemberId93InputChange()}
                                          value={values.cMemberId93}
                                          defaultSelected={MemberId93List.filter(obj => { return obj.key === MemberId93 })}
                                          options={MemberId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.MemberId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cMemberId93 && touched.cMemberId93 && <span className='form__form-group-error'>{errors.cMemberId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.AgentId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.AgentId93 || {}).ColumnHeader} {(columnLabel.AgentId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.AgentId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.AgentId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cAgentId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cAgentId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cAgentId93', true)}
                                          onInputChange={this.AgentId93InputChange()}
                                          value={values.cAgentId93}
                                          defaultSelected={AgentId93List.filter(obj => { return obj.key === AgentId93 })}
                                          options={AgentId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.AgentId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cAgentId93 && touched.cAgentId93 && <span className='form__form-group-error'>{errors.cAgentId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.BrokerId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.BrokerId93 || {}).ColumnHeader} {(columnLabel.BrokerId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.BrokerId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.BrokerId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cBrokerId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cBrokerId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cBrokerId93', true)}
                                          onInputChange={this.BrokerId93InputChange()}
                                          value={values.cBrokerId93}
                                          defaultSelected={BrokerId93List.filter(obj => { return obj.key === BrokerId93 })}
                                          options={BrokerId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.BrokerId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cBrokerId93 && touched.cBrokerId93 && <span className='form__form-group-error'>{errors.cBrokerId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.CustomerId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.CustomerId93 || {}).ColumnHeader} {(columnLabel.CustomerId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.CustomerId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.CustomerId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cCustomerId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCustomerId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCustomerId93', true)}
                                          onInputChange={this.CustomerId93InputChange()}
                                          value={values.cCustomerId93}
                                          defaultSelected={CustomerId93List.filter(obj => { return obj.key === CustomerId93 })}
                                          options={CustomerId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.CustomerId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cCustomerId93 && touched.cCustomerId93 && <span className='form__form-group-error'>{errors.cCustomerId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.InvestorId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.InvestorId93 || {}).ColumnHeader} {(columnLabel.InvestorId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.InvestorId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.InvestorId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cInvestorId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cInvestorId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cInvestorId93', true)}
                                          onInputChange={this.InvestorId93InputChange()}
                                          value={values.cInvestorId93}
                                          defaultSelected={InvestorId93List.filter(obj => { return obj.key === InvestorId93 })}
                                          options={InvestorId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.InvestorId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cInvestorId93 && touched.cInvestorId93 && <span className='form__form-group-error'>{errors.cInvestorId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.VendorId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.VendorId93 || {}).ColumnHeader} {(columnLabel.VendorId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.VendorId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.VendorId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cVendorId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cVendorId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cVendorId93', true)}
                                          onInputChange={this.VendorId93InputChange()}
                                          value={values.cVendorId93}
                                          defaultSelected={VendorId93List.filter(obj => { return obj.key === VendorId93 })}
                                          options={VendorId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.VendorId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cVendorId93 && touched.cVendorId93 && <span className='form__form-group-error'>{errors.cVendorId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.LenderId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.LenderId93 || {}).ColumnHeader} {(columnLabel.LenderId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.LenderId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.LenderId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cLenderId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cLenderId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cLenderId93', true)}
                                          onInputChange={this.LenderId93InputChange()}
                                          value={values.cLenderId93}
                                          defaultSelected={LenderId93List.filter(obj => { return obj.key === LenderId93 })}
                                          options={LenderId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.LenderId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cLenderId93 && touched.cLenderId93 && <span className='form__form-group-error'>{errors.cLenderId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.BorrowerId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.BorrowerId93 || {}).ColumnHeader} {(columnLabel.BorrowerId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.BorrowerId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.BorrowerId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cBorrowerId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cBorrowerId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cBorrowerId93', true)}
                                          onInputChange={this.BorrowerId93InputChange()}
                                          value={values.cBorrowerId93}
                                          defaultSelected={BorrowerId93List.filter(obj => { return obj.key === BorrowerId93 })}
                                          options={BorrowerId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.BorrowerId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cBorrowerId93 && touched.cBorrowerId93 && <span className='form__form-group-error'>{errors.cBorrowerId93}</span>}
                                  </div>
                                </Col>
                              }
                              {(authCol.GuarantorId93 || {}).visible &&
                                <Col lg={6} xl={6}>
                                  <div className='form__form-group'>
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='20px' />) ||
                                      <label className='form__form-group-label'>{(columnLabel.GuarantorId93 || {}).ColumnHeader} {(columnLabel.GuarantorId93 || {}).ToolTip &&
                                        (<ControlledPopover id={(columnLabel.GuarantorId93 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message={(columnLabel.GuarantorId93 || {}).ToolTip} />
                                        )}
                                      </label>
                                    }
                                    {((true && this.constructor.ShowSpinner(AdmUsrPrefState)) && <Skeleton height='36px' />) ||
                                      <div className='form__form-group-field'>
                                        <AutoCompleteField
                                          name='cGuarantorId93'
                                          onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGuarantorId93', false, values)}
                                          onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGuarantorId93', true)}
                                          onInputChange={this.GuarantorId93InputChange()}
                                          value={values.cGuarantorId93}
                                          defaultSelected={GuarantorId93List.filter(obj => { return obj.key === GuarantorId93 })}
                                          options={GuarantorId93List}
                                          filterBy={this.AutoCompleteFilterBy}
                                          disabled={(authCol.GuarantorId93 || {}).readonly ? true : false} />
                                      </div>
                                    }
                                    {errors.cGuarantorId93 && touched.cGuarantorId93 && <span className='form__form-group-error'>{errors.cGuarantorId93}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrPrefId93)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).UsrPrefId93));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmUsrPrefState) && <Skeleton height='43px' />) ||
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
  AdmUsrPref: state.AdmUsrPref,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmUsrPrefReduxObj.LoadPage.bind(AdmUsrPrefReduxObj) },
    { SavePage: AdmUsrPrefReduxObj.SavePage.bind(AdmUsrPrefReduxObj) },
    { DelMst: AdmUsrPrefReduxObj.DelMst.bind(AdmUsrPrefReduxObj) },
    { AddMst: AdmUsrPrefReduxObj.AddMst.bind(AdmUsrPrefReduxObj) },
    { SearchUsrId93: AdmUsrPrefReduxObj.SearchActions.SearchUsrId93.bind(AdmUsrPrefReduxObj) },
    { SearchCompanyId93: AdmUsrPrefReduxObj.SearchActions.SearchCompanyId93.bind(AdmUsrPrefReduxObj) },
    { SearchProjectId93: AdmUsrPrefReduxObj.SearchActions.SearchProjectId93.bind(AdmUsrPrefReduxObj) },
    { SearchMemberId93: AdmUsrPrefReduxObj.SearchActions.SearchMemberId93.bind(AdmUsrPrefReduxObj) },
    { SearchAgentId93: AdmUsrPrefReduxObj.SearchActions.SearchAgentId93.bind(AdmUsrPrefReduxObj) },
    { SearchBrokerId93: AdmUsrPrefReduxObj.SearchActions.SearchBrokerId93.bind(AdmUsrPrefReduxObj) },
    { SearchCustomerId93: AdmUsrPrefReduxObj.SearchActions.SearchCustomerId93.bind(AdmUsrPrefReduxObj) },
    { SearchInvestorId93: AdmUsrPrefReduxObj.SearchActions.SearchInvestorId93.bind(AdmUsrPrefReduxObj) },
    { SearchVendorId93: AdmUsrPrefReduxObj.SearchActions.SearchVendorId93.bind(AdmUsrPrefReduxObj) },
    { SearchLenderId93: AdmUsrPrefReduxObj.SearchActions.SearchLenderId93.bind(AdmUsrPrefReduxObj) },
    { SearchBorrowerId93: AdmUsrPrefReduxObj.SearchActions.SearchBorrowerId93.bind(AdmUsrPrefReduxObj) },
    { SearchGuarantorId93: AdmUsrPrefReduxObj.SearchActions.SearchGuarantorId93.bind(AdmUsrPrefReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);
