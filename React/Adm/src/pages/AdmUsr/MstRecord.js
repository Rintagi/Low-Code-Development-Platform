
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
import AdmUsrReduxObj, { ShowMstFilterApplied } from '../../redux/AdmUsr';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmUsr || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'UsrId1';
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

CultureId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureId1(v, filterBy);}}
 PicMed({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'PicMed';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
InvestorId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchInvestorId1(v, filterBy);}}
CustomerId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCustomerId1(v, filterBy);}}
VendorId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchVendorId1(v, filterBy);}}
AgentId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchAgentId1(v, filterBy);}}
BrokerId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchBrokerId1(v, filterBy);}}
MemberId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchMemberId1(v, filterBy);}}
LenderId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchLenderId1(v, filterBy);}}
BorrowerId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchBorrowerId1(v, filterBy);}}
GuarantorId1InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchGuarantorId1(v, filterBy);}}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmUsr || {}).ColumnLabel || {};
    /* standard field validation */
if (!values.cLoginName1) { errors.cLoginName1 = (columnLabel.LoginName1 || {}).ErrMessage;}
if (!values.cUsrName1) { errors.cUsrName1 = (columnLabel.UsrName1 || {}).ErrMessage;}
if (isEmptyId((values.cCultureId1 || {}).value)) { errors.cCultureId1 = (columnLabel.CultureId1 || {}).ErrMessage;}
if (isEmptyId((values.cDefSystemId1 || {}).value)) { errors.cDefSystemId1 = (columnLabel.DefSystemId1 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmUsr || {}).Mst || {};
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
        this.props.AdmUsr,
        {
          UsrId1: values.cUsrId1|| '',
          LoginName1: values.cLoginName1|| '',
          UsrName1: values.cUsrName1|| '',
          CultureId1: (values.cCultureId1|| {}).value || '',
          DefCompanyId1: (values.cDefCompanyId1|| {}).value || '',
          DefProjectId1: (values.cDefProjectId1|| {}).value || '',
          DefSystemId1: (values.cDefSystemId1|| {}).value || '',
          UsrEmail1: values.cUsrEmail1|| '',
          UsrMobile1: values.cUsrMobile1|| '',
          UsrGroupLs1: (values.cUsrGroupLs1|| {}).value || '',
          UsrImprLink1: (values.cUsrImprLink1|| {}).value || '',
          IPAlert1: values.cIPAlert1 ? 'Y' : 'N',
          PwdNoRepeat1: values.cPwdNoRepeat1|| '',
          PwdDuration1: values.cPwdDuration1|| '',
          PwdWarn1: values.cPwdWarn1|| '',
          Active1: values.cActive1 ? 'Y' : 'N',
          InternalUsr1: values.cInternalUsr1 ? 'Y' : 'N',
          TechnicalUsr1: values.cTechnicalUsr1 ? 'Y' : 'N',
          EmailLink1: values.cEmailLink1|| '',
          MobileLink1: values.cMobileLink1|| '',
          FailedAttempt1: values.cFailedAttempt1|| '',
          LastSuccessDt1: values.cLastSuccessDt1|| '',
          LastFailedDt1: values.cLastFailedDt1|| '',
          CompanyLs1: (values.cCompanyLs1|| {}).value || '',
          ProjectLs1: (values.cProjectLs1|| {}).value || '',
          ModifiedOn1: values.cModifiedOn1|| '',
          HintQuestionId1: (values.cHintQuestionId1|| {}).value || '',
          HintAnswer1: values.cHintAnswer1|| '',
          InvestorId1: (values.cInvestorId1|| {}).value || '',
          CustomerId1: (values.cCustomerId1|| {}).value || '',
          VendorId1: (values.cVendorId1|| {}).value || '',
          AgentId1: (values.cAgentId1|| {}).value || '',
          BrokerId1: (values.cBrokerId1|| {}).value || '',
          MemberId1: (values.cMemberId1|| {}).value || '',
          LenderId1: (values.cLenderId1|| {}).value || '',
          BorrowerId1: (values.cBorrowerId1|| {}).value || '',
          GuarantorId1: (values.cGuarantorId1|| {}).value || '',
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
    const AdmUsrState = this.props.AdmUsr || {};
    const auxSystemLabels = AdmUsrState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).UsrId1;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmUsr.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmUsr.Label);
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
    const AdmUsrState = this.props.AdmUsr || {};
    const auxSystemLabels = AdmUsrState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.UsrId1;
        this.props.DelMst(this.props.AdmUsr, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmUsr || {};
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
    const AdmUsrState = this.props.AdmUsr || {};
    const auxSystemLabels = AdmUsrState.SystemLabel || {};
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
      if (!(this.props.AdmUsr || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmUsr || {};

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
    const AdmUsrState = this.props.AdmUsr || {};

    if (AdmUsrState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmUsrState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmUsrReduxObj.GetScreenButtons(AdmUsrState) || {};
    const itemList = AdmUsrState.Dtl || [];
    const auxLabels = AdmUsrState.Label || {};
    const auxSystemLabels = AdmUsrState.SystemLabel || {};

    const columnLabel = AdmUsrState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmUsrState);
    const authRow = (AdmUsrState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmUsr || {}).Mst || {});
    const currDtl = ((this.props.AdmUsr || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.UsrId1));
    const selectList = AdmUsrReduxObj.SearchListToSelectList(AdmUsrState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const UsrId1 = currMst.UsrId1;
const LoginName1 = currMst.LoginName1;
const UsrName1 = currMst.UsrName1;
const CultureId1List = AdmUsrReduxObj.ScreenDdlSelectors.CultureId1(AdmUsrState);
const CultureId1 = currMst.CultureId1;
const DefCompanyId1List = AdmUsrReduxObj.ScreenDdlSelectors.DefCompanyId1(AdmUsrState);
const DefCompanyId1 = currMst.DefCompanyId1;
const DefProjectId1List = AdmUsrReduxObj.ScreenDdlSelectors.DefProjectId1(AdmUsrState);
const DefProjectId1 = currMst.DefProjectId1;
const DefSystemId1List = AdmUsrReduxObj.ScreenDdlSelectors.DefSystemId1(AdmUsrState);
const DefSystemId1 = currMst.DefSystemId1;
const UsrEmail1 = currMst.UsrEmail1;
const UsrMobile1 = currMst.UsrMobile1;
const UsrGroupLs1List = AdmUsrReduxObj.ScreenDdlSelectors.UsrGroupLs1(AdmUsrState);
const UsrGroupLs1 = currMst.UsrGroupLs1;
const UsrImprLink1List = AdmUsrReduxObj.ScreenDdlSelectors.UsrImprLink1(AdmUsrState);
const UsrImprLink1 = currMst.UsrImprLink1;
const IPAlert1 = currMst.IPAlert1;
const PwdNoRepeat1 = currMst.PwdNoRepeat1;
const PwdDuration1 = currMst.PwdDuration1;
const PwdWarn1 = currMst.PwdWarn1;
const Active1 = currMst.Active1;
const InternalUsr1 = currMst.InternalUsr1;
const TechnicalUsr1 = currMst.TechnicalUsr1;
const EmailLink1 = currMst.EmailLink1;
const MobileLink1 = currMst.MobileLink1;
const FailedAttempt1 = currMst.FailedAttempt1;
const LastSuccessDt1 = currMst.LastSuccessDt1;
const LastFailedDt1 = currMst.LastFailedDt1;
const CompanyLs1List = AdmUsrReduxObj.ScreenDdlSelectors.CompanyLs1(AdmUsrState);
const CompanyLs1 = currMst.CompanyLs1;
const ProjectLs1List = AdmUsrReduxObj.ScreenDdlSelectors.ProjectLs1(AdmUsrState);
const ProjectLs1 = currMst.ProjectLs1;
const ModifiedOn1 = currMst.ModifiedOn1;
const HintQuestionId1List = AdmUsrReduxObj.ScreenDdlSelectors.HintQuestionId1(AdmUsrState);
const HintQuestionId1 = currMst.HintQuestionId1;
const HintAnswer1 = currMst.HintAnswer1;
const InvestorId1List = AdmUsrReduxObj.ScreenDdlSelectors.InvestorId1(AdmUsrState);
const InvestorId1 = currMst.InvestorId1;
const CustomerId1List = AdmUsrReduxObj.ScreenDdlSelectors.CustomerId1(AdmUsrState);
const CustomerId1 = currMst.CustomerId1;
const VendorId1List = AdmUsrReduxObj.ScreenDdlSelectors.VendorId1(AdmUsrState);
const VendorId1 = currMst.VendorId1;
const AgentId1List = AdmUsrReduxObj.ScreenDdlSelectors.AgentId1(AdmUsrState);
const AgentId1 = currMst.AgentId1;
const BrokerId1List = AdmUsrReduxObj.ScreenDdlSelectors.BrokerId1(AdmUsrState);
const BrokerId1 = currMst.BrokerId1;
const MemberId1List = AdmUsrReduxObj.ScreenDdlSelectors.MemberId1(AdmUsrState);
const MemberId1 = currMst.MemberId1;
const LenderId1List = AdmUsrReduxObj.ScreenDdlSelectors.LenderId1(AdmUsrState);
const LenderId1 = currMst.LenderId1;
const BorrowerId1List = AdmUsrReduxObj.ScreenDdlSelectors.BorrowerId1(AdmUsrState);
const BorrowerId1 = currMst.BorrowerId1;
const GuarantorId1List = AdmUsrReduxObj.ScreenDdlSelectors.GuarantorId1(AdmUsrState);
const GuarantorId1 = currMst.GuarantorId1;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmUsrState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cUsrId1: UsrId1 || '',
                  cLoginName1: LoginName1 || '',
                  cUsrName1: UsrName1 || '',
                  cCultureId1: CultureId1List.filter(obj => { return obj.key === CultureId1 })[0],
                  cDefCompanyId1: DefCompanyId1List.filter(obj => { return obj.key === DefCompanyId1 })[0],
                  cDefProjectId1: DefProjectId1List.filter(obj => { return obj.key === DefProjectId1 })[0],
                  cDefSystemId1: DefSystemId1List.filter(obj => { return obj.key === DefSystemId1 })[0],
                  cUsrEmail1: UsrEmail1 || '',
                  cUsrMobile1: UsrMobile1 || '',
                  cUsrGroupLs1: UsrGroupLs1List.filter(obj => { return obj.key === UsrGroupLs1 })[0],
                  cUsrImprLink1: UsrImprLink1List.filter(obj => { return obj.key === UsrImprLink1 })[0],
                  cIPAlert1: IPAlert1 === 'Y',
                  cPwdNoRepeat1: PwdNoRepeat1 || '',
                  cPwdDuration1: PwdDuration1 || '',
                  cPwdWarn1: PwdWarn1 || '',
                  cActive1: Active1 === 'Y',
                  cInternalUsr1: InternalUsr1 === 'Y',
                  cTechnicalUsr1: TechnicalUsr1 === 'Y',
                  cEmailLink1: EmailLink1 || '',
                  cMobileLink1: MobileLink1 || '',
                  cFailedAttempt1: FailedAttempt1 || '',
                  cLastSuccessDt1: LastSuccessDt1 || new Date(),
                  cLastFailedDt1: LastFailedDt1 || new Date(),
                  cCompanyLs1: CompanyLs1List.filter(obj => { return obj.key === CompanyLs1 })[0],
                  cProjectLs1: ProjectLs1List.filter(obj => { return obj.key === ProjectLs1 })[0],
                  cModifiedOn1: ModifiedOn1 || new Date(),
                  cHintQuestionId1: HintQuestionId1List.filter(obj => { return obj.key === HintQuestionId1 })[0],
                  cHintAnswer1: HintAnswer1 || '',
                  cInvestorId1: InvestorId1List.filter(obj => { return obj.key === InvestorId1 })[0],
                  cCustomerId1: CustomerId1List.filter(obj => { return obj.key === CustomerId1 })[0],
                  cVendorId1: VendorId1List.filter(obj => { return obj.key === VendorId1 })[0],
                  cAgentId1: AgentId1List.filter(obj => { return obj.key === AgentId1 })[0],
                  cBrokerId1: BrokerId1List.filter(obj => { return obj.key === BrokerId1 })[0],
                  cMemberId1: MemberId1List.filter(obj => { return obj.key === MemberId1 })[0],
                  cLenderId1: LenderId1List.filter(obj => { return obj.key === LenderId1 })[0],
                  cBorrowerId1: BorrowerId1List.filter(obj => { return obj.key === BorrowerId1 })[0],
                  cGuarantorId1: GuarantorId1List.filter(obj => { return obj.key === GuarantorId1 })[0],
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
                                {(this.constructor.ShowSpinner(AdmUsrState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId1)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId1)) return null;
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
            {(authCol.UsrId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrId1 || {}).ColumnHeader} {(columnLabel.UsrId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrId1'
disabled = {(authCol.UsrId1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrId1 && touched.cUsrId1 && <span className='form__form-group-error'>{errors.cUsrId1}</span>}
</div>
</Col>
}
{(authCol.LoginName1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LoginName1 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.LoginName1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LoginName1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LoginName1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLoginName1'
disabled = {(authCol.LoginName1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLoginName1 && touched.cLoginName1 && <span className='form__form-group-error'>{errors.cLoginName1}</span>}
</div>
</Col>
}
{(authCol.UsrName1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrName1 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.UsrName1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrName1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrName1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrName1'
disabled = {(authCol.UsrName1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrName1 && touched.cUsrName1 && <span className='form__form-group-error'>{errors.cUsrName1}</span>}
</div>
</Col>
}
{(authCol.CultureId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureId1 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId1', true)}
onInputChange={this.CultureId1InputChange()}
value={values.cCultureId1}
defaultSelected={CultureId1List.filter(obj => { return obj.key === CultureId1 })}
options={CultureId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureId1 && touched.cCultureId1 && <span className='form__form-group-error'>{errors.cCultureId1}</span>}
</div>
</Col>
}
{(authCol.DefCompanyId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefCompanyId1 || {}).ColumnHeader} {(columnLabel.DefCompanyId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefCompanyId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefCompanyId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDefCompanyId1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDefCompanyId1')}
value={values.cDefCompanyId1}
options={DefCompanyId1List}
placeholder=''
disabled = {(authCol.DefCompanyId1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefCompanyId1 && touched.cDefCompanyId1 && <span className='form__form-group-error'>{errors.cDefCompanyId1}</span>}
</div>
</Col>
}
{(authCol.DefProjectId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefProjectId1 || {}).ColumnHeader} {(columnLabel.DefProjectId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefProjectId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefProjectId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDefProjectId1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDefProjectId1')}
value={values.cDefProjectId1}
options={DefProjectId1List}
placeholder=''
disabled = {(authCol.DefProjectId1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefProjectId1 && touched.cDefProjectId1 && <span className='form__form-group-error'>{errors.cDefProjectId1}</span>}
</div>
</Col>
}
{(authCol.DefSystemId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefSystemId1 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DefSystemId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefSystemId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefSystemId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cDefSystemId1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cDefSystemId1')}
value={values.cDefSystemId1}
options={DefSystemId1List}
placeholder=''
disabled = {(authCol.DefSystemId1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefSystemId1 && touched.cDefSystemId1 && <span className='form__form-group-error'>{errors.cDefSystemId1}</span>}
</div>
</Col>
}
{(authCol.UsrEmail1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrEmail1 || {}).ColumnHeader} {(columnLabel.UsrEmail1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrEmail1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrEmail1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrEmail1'
disabled = {(authCol.UsrEmail1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrEmail1 && touched.cUsrEmail1 && <span className='form__form-group-error'>{errors.cUsrEmail1}</span>}
</div>
</Col>
}
{(authCol.UsrMobile1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrMobile1 || {}).ColumnHeader} {(columnLabel.UsrMobile1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrMobile1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrMobile1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUsrMobile1'
disabled = {(authCol.UsrMobile1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrMobile1 && touched.cUsrMobile1 && <span className='form__form-group-error'>{errors.cUsrMobile1}</span>}
</div>
</Col>
}
{(authCol.UsrGroupLs1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrGroupLs1 || {}).ColumnHeader} {(columnLabel.UsrGroupLs1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrGroupLs1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrGroupLs1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cUsrGroupLs1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cUsrGroupLs1')}
value={values.cUsrGroupLs1}
options={UsrGroupLs1List}
placeholder=''
disabled = {(authCol.UsrGroupLs1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrGroupLs1 && touched.cUsrGroupLs1 && <span className='form__form-group-error'>{errors.cUsrGroupLs1}</span>}
</div>
</Col>
}
{(authCol.UsrImprLink1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsrImprLink1 || {}).ColumnHeader} {(columnLabel.UsrImprLink1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsrImprLink1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsrImprLink1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cUsrImprLink1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cUsrImprLink1')}
value={values.cUsrImprLink1}
options={UsrImprLink1List}
placeholder=''
disabled = {(authCol.UsrImprLink1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUsrImprLink1 && touched.cUsrImprLink1 && <span className='form__form-group-error'>{errors.cUsrImprLink1}</span>}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.PicMed || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.PicMed || {}).readonly || !(authCol.PicMed || {}).visible} onClick={this.PicMed({ naviBar, submitForm, currMst })} >{auxLabels.PicMed || (columnLabel.PicMed || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.IPAlert1 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cIPAlert1'
onChange={handleChange}
defaultChecked={values.cIPAlert1}
disabled={(authCol.IPAlert1 || {}).readonly || !(authCol.IPAlert1 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.IPAlert1 || {}).ColumnHeader}</span>
</label>
{(columnLabel.IPAlert1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.IPAlert1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.IPAlert1 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.PwdNoRepeat1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PwdNoRepeat1 || {}).ColumnHeader} {(columnLabel.PwdNoRepeat1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PwdNoRepeat1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PwdNoRepeat1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPwdNoRepeat1'
disabled = {(authCol.PwdNoRepeat1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPwdNoRepeat1 && touched.cPwdNoRepeat1 && <span className='form__form-group-error'>{errors.cPwdNoRepeat1}</span>}
</div>
</Col>
}
{(authCol.PwdDuration1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PwdDuration1 || {}).ColumnHeader} {(columnLabel.PwdDuration1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PwdDuration1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PwdDuration1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPwdDuration1'
disabled = {(authCol.PwdDuration1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPwdDuration1 && touched.cPwdDuration1 && <span className='form__form-group-error'>{errors.cPwdDuration1}</span>}
</div>
</Col>
}
{(authCol.PwdWarn1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.PwdWarn1 || {}).ColumnHeader} {(columnLabel.PwdWarn1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.PwdWarn1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.PwdWarn1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cPwdWarn1'
disabled = {(authCol.PwdWarn1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cPwdWarn1 && touched.cPwdWarn1 && <span className='form__form-group-error'>{errors.cPwdWarn1}</span>}
</div>
</Col>
}
{(authCol.Active1 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cActive1'
onChange={handleChange}
defaultChecked={values.cActive1}
disabled={(authCol.Active1 || {}).readonly || !(authCol.Active1 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.Active1 || {}).ColumnHeader}</span>
</label>
{(columnLabel.Active1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.Active1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.Active1 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.InternalUsr1 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cInternalUsr1'
onChange={handleChange}
defaultChecked={values.cInternalUsr1}
disabled={(authCol.InternalUsr1 || {}).readonly || !(authCol.InternalUsr1 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.InternalUsr1 || {}).ColumnHeader}</span>
</label>
{(columnLabel.InternalUsr1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InternalUsr1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InternalUsr1 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.TechnicalUsr1 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cTechnicalUsr1'
onChange={handleChange}
defaultChecked={values.cTechnicalUsr1}
disabled={(authCol.TechnicalUsr1 || {}).readonly || !(authCol.TechnicalUsr1 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.TechnicalUsr1 || {}).ColumnHeader}</span>
</label>
{(columnLabel.TechnicalUsr1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TechnicalUsr1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TechnicalUsr1 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.EmailLink1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.EmailLink1 || {}).ColumnHeader} {(columnLabel.EmailLink1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.EmailLink1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.EmailLink1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cEmailLink1'
disabled = {(authCol.EmailLink1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cEmailLink1 && touched.cEmailLink1 && <span className='form__form-group-error'>{errors.cEmailLink1}</span>}
</div>
</Col>
}
{(authCol.MobileLink1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MobileLink1 || {}).ColumnHeader} {(columnLabel.MobileLink1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MobileLink1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MobileLink1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cMobileLink1'
disabled = {(authCol.MobileLink1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cMobileLink1 && touched.cMobileLink1 && <span className='form__form-group-error'>{errors.cMobileLink1}</span>}
</div>
</Col>
}
{(authCol.FailedAttempt1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.FailedAttempt1 || {}).ColumnHeader} {(columnLabel.FailedAttempt1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.FailedAttempt1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.FailedAttempt1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cFailedAttempt1'
disabled = {(authCol.FailedAttempt1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cFailedAttempt1 && touched.cFailedAttempt1 && <span className='form__form-group-error'>{errors.cFailedAttempt1}</span>}
</div>
</Col>
}
{(authCol.LastSuccessDt1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastSuccessDt1 || {}).ColumnHeader} {(columnLabel.LastSuccessDt1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastSuccessDt1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastSuccessDt1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cLastSuccessDt1'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cLastSuccessDt1', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cLastSuccessDt1', true)}
value={values.cLastSuccessDt1}
selected={values.cLastSuccessDt1}
disabled = {(authCol.LastSuccessDt1 || {}).readonly ? true: false }/>
</div>
}
{errors.cLastSuccessDt1 && touched.cLastSuccessDt1 && <span className='form__form-group-error'>{errors.cLastSuccessDt1}</span>}
</div>
</Col>
}
{(authCol.LastFailedDt1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastFailedDt1 || {}).ColumnHeader} {(columnLabel.LastFailedDt1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastFailedDt1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastFailedDt1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cLastFailedDt1'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cLastFailedDt1', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cLastFailedDt1', true)}
value={values.cLastFailedDt1}
selected={values.cLastFailedDt1}
disabled = {(authCol.LastFailedDt1 || {}).readonly ? true: false }/>
</div>
}
{errors.cLastFailedDt1 && touched.cLastFailedDt1 && <span className='form__form-group-error'>{errors.cLastFailedDt1}</span>}
</div>
</Col>
}
{(authCol.CompanyLs1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CompanyLs1 || {}).ColumnHeader} {(columnLabel.CompanyLs1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CompanyLs1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CompanyLs1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cCompanyLs1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cCompanyLs1')}
value={values.cCompanyLs1}
options={CompanyLs1List}
placeholder=''
disabled = {(authCol.CompanyLs1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cCompanyLs1 && touched.cCompanyLs1 && <span className='form__form-group-error'>{errors.cCompanyLs1}</span>}
</div>
</Col>
}
{(authCol.ProjectLs1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ProjectLs1 || {}).ColumnHeader} {(columnLabel.ProjectLs1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ProjectLs1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ProjectLs1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cProjectLs1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cProjectLs1')}
value={values.cProjectLs1}
options={ProjectLs1List}
placeholder=''
disabled = {(authCol.ProjectLs1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cProjectLs1 && touched.cProjectLs1 && <span className='form__form-group-error'>{errors.cProjectLs1}</span>}
</div>
</Col>
}
{(authCol.ModifiedOn1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedOn1 || {}).ColumnHeader} {(columnLabel.ModifiedOn1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedOn1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedOn1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cModifiedOn1'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn1', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn1', true)}
value={values.cModifiedOn1}
selected={values.cModifiedOn1}
disabled = {(authCol.ModifiedOn1 || {}).readonly ? true: false }/>
</div>
}
{errors.cModifiedOn1 && touched.cModifiedOn1 && <span className='form__form-group-error'>{errors.cModifiedOn1}</span>}
</div>
</Col>
}
{(authCol.HintQuestionId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.HintQuestionId1 || {}).ColumnHeader} {(columnLabel.HintQuestionId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HintQuestionId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HintQuestionId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cHintQuestionId1'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cHintQuestionId1')}
value={values.cHintQuestionId1}
options={HintQuestionId1List}
placeholder=''
disabled = {(authCol.HintQuestionId1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cHintQuestionId1 && touched.cHintQuestionId1 && <span className='form__form-group-error'>{errors.cHintQuestionId1}</span>}
</div>
</Col>
}
{(authCol.HintAnswer1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.HintAnswer1 || {}).ColumnHeader} {(columnLabel.HintAnswer1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.HintAnswer1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.HintAnswer1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cHintAnswer1'
disabled = {(authCol.HintAnswer1 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cHintAnswer1 && touched.cHintAnswer1 && <span className='form__form-group-error'>{errors.cHintAnswer1}</span>}
</div>
</Col>
}
{(authCol.InvestorId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.InvestorId1 || {}).ColumnHeader} {(columnLabel.InvestorId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.InvestorId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.InvestorId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cInvestorId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cInvestorId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cInvestorId1', true)}
onInputChange={this.InvestorId1InputChange()}
value={values.cInvestorId1}
defaultSelected={InvestorId1List.filter(obj => { return obj.key === InvestorId1 })}
options={InvestorId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.InvestorId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cInvestorId1 && touched.cInvestorId1 && <span className='form__form-group-error'>{errors.cInvestorId1}</span>}
</div>
</Col>
}
{(authCol.CustomerId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CustomerId1 || {}).ColumnHeader} {(columnLabel.CustomerId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CustomerId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CustomerId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCustomerId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCustomerId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCustomerId1', true)}
onInputChange={this.CustomerId1InputChange()}
value={values.cCustomerId1}
defaultSelected={CustomerId1List.filter(obj => { return obj.key === CustomerId1 })}
options={CustomerId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CustomerId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cCustomerId1 && touched.cCustomerId1 && <span className='form__form-group-error'>{errors.cCustomerId1}</span>}
</div>
</Col>
}
{(authCol.VendorId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VendorId1 || {}).ColumnHeader} {(columnLabel.VendorId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VendorId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VendorId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cVendorId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cVendorId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cVendorId1', true)}
onInputChange={this.VendorId1InputChange()}
value={values.cVendorId1}
defaultSelected={VendorId1List.filter(obj => { return obj.key === VendorId1 })}
options={VendorId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.VendorId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cVendorId1 && touched.cVendorId1 && <span className='form__form-group-error'>{errors.cVendorId1}</span>}
</div>
</Col>
}
{(authCol.AgentId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.AgentId1 || {}).ColumnHeader} {(columnLabel.AgentId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.AgentId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.AgentId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cAgentId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cAgentId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cAgentId1', true)}
onInputChange={this.AgentId1InputChange()}
value={values.cAgentId1}
defaultSelected={AgentId1List.filter(obj => { return obj.key === AgentId1 })}
options={AgentId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.AgentId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cAgentId1 && touched.cAgentId1 && <span className='form__form-group-error'>{errors.cAgentId1}</span>}
</div>
</Col>
}
{(authCol.BrokerId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BrokerId1 || {}).ColumnHeader} {(columnLabel.BrokerId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BrokerId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BrokerId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cBrokerId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cBrokerId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cBrokerId1', true)}
onInputChange={this.BrokerId1InputChange()}
value={values.cBrokerId1}
defaultSelected={BrokerId1List.filter(obj => { return obj.key === BrokerId1 })}
options={BrokerId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.BrokerId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cBrokerId1 && touched.cBrokerId1 && <span className='form__form-group-error'>{errors.cBrokerId1}</span>}
</div>
</Col>
}
{(authCol.MemberId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.MemberId1 || {}).ColumnHeader} {(columnLabel.MemberId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MemberId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MemberId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cMemberId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cMemberId1', true)}
onInputChange={this.MemberId1InputChange()}
value={values.cMemberId1}
defaultSelected={MemberId1List.filter(obj => { return obj.key === MemberId1 })}
options={MemberId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.MemberId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cMemberId1 && touched.cMemberId1 && <span className='form__form-group-error'>{errors.cMemberId1}</span>}
</div>
</Col>
}
{(authCol.LenderId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LenderId1 || {}).ColumnHeader} {(columnLabel.LenderId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LenderId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LenderId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cLenderId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cLenderId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cLenderId1', true)}
onInputChange={this.LenderId1InputChange()}
value={values.cLenderId1}
defaultSelected={LenderId1List.filter(obj => { return obj.key === LenderId1 })}
options={LenderId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.LenderId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cLenderId1 && touched.cLenderId1 && <span className='form__form-group-error'>{errors.cLenderId1}</span>}
</div>
</Col>
}
{(authCol.BorrowerId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BorrowerId1 || {}).ColumnHeader} {(columnLabel.BorrowerId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BorrowerId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BorrowerId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cBorrowerId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cBorrowerId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cBorrowerId1', true)}
onInputChange={this.BorrowerId1InputChange()}
value={values.cBorrowerId1}
defaultSelected={BorrowerId1List.filter(obj => { return obj.key === BorrowerId1 })}
options={BorrowerId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.BorrowerId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cBorrowerId1 && touched.cBorrowerId1 && <span className='form__form-group-error'>{errors.cBorrowerId1}</span>}
</div>
</Col>
}
{(authCol.GuarantorId1 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.GuarantorId1 || {}).ColumnHeader} {(columnLabel.GuarantorId1 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.GuarantorId1 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.GuarantorId1 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cGuarantorId1'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cGuarantorId1', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cGuarantorId1', true)}
onInputChange={this.GuarantorId1InputChange()}
value={values.cGuarantorId1}
defaultSelected={GuarantorId1List.filter(obj => { return obj.key === GuarantorId1 })}
options={GuarantorId1List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.GuarantorId1 || {}).readonly ? true: false }/>
</div>
}
{errors.cGuarantorId1 && touched.cGuarantorId1 && <span className='form__form-group-error'>{errors.cGuarantorId1}</span>}
</div>
</Col>
}
{(authCol.UsageStat || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmUsrState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UsageStat || {}).ColumnHeader} {(columnLabel.UsageStat || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UsageStat || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UsageStat || {}).ToolTip} />
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).UsrId1)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).UsrId1));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmUsrState) && <Skeleton height='43px' />) ||
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
  AdmUsr: state.AdmUsr,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmUsrReduxObj.LoadPage.bind(AdmUsrReduxObj) },
    { SavePage: AdmUsrReduxObj.SavePage.bind(AdmUsrReduxObj) },
    { DelMst: AdmUsrReduxObj.DelMst.bind(AdmUsrReduxObj) },
    { AddMst: AdmUsrReduxObj.AddMst.bind(AdmUsrReduxObj) },
//    { SearchMemberId64: AdmUsrReduxObj.SearchActions.SearchMemberId64.bind(AdmUsrReduxObj) },
//    { SearchCurrencyId64: AdmUsrReduxObj.SearchActions.SearchCurrencyId64.bind(AdmUsrReduxObj) },
//    { SearchCustomerJobId64: AdmUsrReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmUsrReduxObj) },
{ SearchCultureId1: AdmUsrReduxObj.SearchActions.SearchCultureId1.bind(AdmUsrReduxObj) },
{ SearchInvestorId1: AdmUsrReduxObj.SearchActions.SearchInvestorId1.bind(AdmUsrReduxObj) },
{ SearchCustomerId1: AdmUsrReduxObj.SearchActions.SearchCustomerId1.bind(AdmUsrReduxObj) },
{ SearchVendorId1: AdmUsrReduxObj.SearchActions.SearchVendorId1.bind(AdmUsrReduxObj) },
{ SearchAgentId1: AdmUsrReduxObj.SearchActions.SearchAgentId1.bind(AdmUsrReduxObj) },
{ SearchBrokerId1: AdmUsrReduxObj.SearchActions.SearchBrokerId1.bind(AdmUsrReduxObj) },
{ SearchMemberId1: AdmUsrReduxObj.SearchActions.SearchMemberId1.bind(AdmUsrReduxObj) },
{ SearchLenderId1: AdmUsrReduxObj.SearchActions.SearchLenderId1.bind(AdmUsrReduxObj) },
{ SearchBorrowerId1: AdmUsrReduxObj.SearchActions.SearchBorrowerId1.bind(AdmUsrReduxObj) },
{ SearchGuarantorId1: AdmUsrReduxObj.SearchActions.SearchGuarantorId1.bind(AdmUsrReduxObj) },
    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            