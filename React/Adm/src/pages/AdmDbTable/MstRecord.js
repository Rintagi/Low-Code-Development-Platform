
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
import AdmDbTableReduxObj, { ShowMstFilterApplied } from '../../redux/AdmDbTable';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class MstRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmDbTable || {});
    this.blocker = null;
    this.titleSet = false;
    this.MstKeyColumnName = 'TableId3';
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

 ModelSample({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'ModelSample';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 SyncByDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncByDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 AnalToDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'AnalToDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}
 SyncToDb({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
return function (evt) {
this.OnClickColumeName = 'SyncToDb';
//Enter Custom Code here, eg: submitForm();
evt.preventDefault();
}.bind(this);
}/* ReactRule: Master Record Custom Function */
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
    const columnLabel = (this.props.AdmDbTable || {}).ColumnLabel || {};
    /* standard field validation */
if (isEmptyId((values.cSystemId3 || {}).value)) { errors.cSystemId3 = (columnLabel.SystemId3 || {}).ErrMessage;}
if (!values.cTableName3) { errors.cTableName3 = (columnLabel.TableName3 || {}).ErrMessage;}
if (!values.cTableDesc3) { errors.cTableDesc3 = (columnLabel.TableDesc3 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {
    const errors = [];
    const currMst = (this.props.AdmDbTable || {}).Mst || {};
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
        this.props.AdmDbTable,
        {
          TableId3: values.cTableId3|| '',
          SystemId3: (values.cSystemId3|| {}).value || '',
          TableName3: values.cTableName3|| '',
          TableDesc3: values.cTableDesc3|| '',
          TblObjective3: values.cTblObjective3|| '',
          VirtualTbl3: values.cVirtualTbl3 ? 'Y' : 'N',
          MultiDesignDb3: values.cMultiDesignDb3 ? 'Y' : 'N',
          UploadSheet: values.cUploadSheet|| '',
          SheetNameList: (values.cSheetNameList|| {}).value || '',
          RowsToExamine: values.cRowsToExamine|| '',
          ModifiedBy3: (values.cModifiedBy3|| {}).value || '',
          ModifiedOn3: values.cModifiedOn3|| '',
          LastSyncDt3: values.cLastSyncDt3|| '',
          VirtualSql3: values.cVirtualSql3|| '',
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || (mst || {}).TableId3;
      const copyFn = () => {
        if (fromMstId) {
          this.props.AddMst(fromMstId, 'Mst', 0);
          /* this is application specific rule as the Posted flag needs to be reset */
          this.props.AdmDbTable.Mst.Posted64 = 'N';
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', {}, {}, this.props.AdmDbTable.Label);
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const fromMstId = mstId || mst.TableId3;
        this.props.DelMst(this.props.AdmDbTable, fromMstId);
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });
    }.bind(this);
  }
  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmDbTable || {};
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
    const AdmDbTableState = this.props.AdmDbTable || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};
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
      if (!(this.props.AdmDbTable || {}).AuthCol || true) {
        this.props.LoadPage('Mst', { mstId: mstId || '_' });
      }
    }
    else {
      return;
    }
  }

  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmDbTable || {};

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
    const AdmDbTableState = this.props.AdmDbTable || {};

    if (AdmDbTableState.access_denied) {
      return <Redirect to='/error' />;
    }

    const screenHlp = AdmDbTableState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterRecTitle = ((screenHlp || {}).MasterRecTitle || '');
    const MasterRecSubtitle = ((screenHlp || {}).MasterRecSubtitle || '');

    const screenButtons = AdmDbTableReduxObj.GetScreenButtons(AdmDbTableState) || {};
    const itemList = AdmDbTableState.Dtl || [];
    const auxLabels = AdmDbTableState.Label || {};
    const auxSystemLabels = AdmDbTableState.SystemLabel || {};

    const columnLabel = AdmDbTableState.ColumnLabel || {};
    const authCol = this.GetAuthCol(AdmDbTableState);
    const authRow = (AdmDbTableState.AuthRow || [])[0] || {};
    const currMst = ((this.props.AdmDbTable || {}).Mst || {});
    const currDtl = ((this.props.AdmDbTable || {}).EditDtl || {});
    const naviBar = getNaviBar('Mst', currMst, currDtl, screenButtons).filter(v => ((v.type !== 'Dtl' && v.type !== 'DtlList') || currMst.TableId3));
    const selectList = AdmDbTableReduxObj.SearchListToSelectList(AdmDbTableState);
    const selectedMst = (selectList || []).filter(v => v.isSelected)[0] || {};
const TableId3 = currMst.TableId3;
const SystemId3List = AdmDbTableReduxObj.ScreenDdlSelectors.SystemId3(AdmDbTableState);
const SystemId3 = currMst.SystemId3;
const TableName3 = currMst.TableName3;
const TableDesc3 = currMst.TableDesc3;
const TblObjective3 = currMst.TblObjective3;
const VirtualTbl3 = currMst.VirtualTbl3;
const MultiDesignDb3 = currMst.MultiDesignDb3;
const UploadSheet = currMst.UploadSheet;
const SheetNameListList = AdmDbTableReduxObj.ScreenDdlSelectors.SheetNameList(AdmDbTableState);
const SheetNameList = currMst.SheetNameList;
const RowsToExamine = currMst.RowsToExamine;
const ModifiedBy3List = AdmDbTableReduxObj.ScreenDdlSelectors.ModifiedBy3(AdmDbTableState);
const ModifiedBy3 = currMst.ModifiedBy3;
const ModifiedOn3 = currMst.ModifiedOn3;
const LastSyncDt3 = currMst.LastSyncDt3;
const VirtualSql3 = currMst.VirtualSql3;

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
                {/* {!useMobileView && this.constructor.ShowSpinner(AdmDbTableState) && <div className='panel__refresh'></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cTableId3: TableId3 || '',
                  cSystemId3: SystemId3List.filter(obj => { return obj.key === SystemId3 })[0],
                  cTableName3: TableName3 || '',
                  cTableDesc3: TableDesc3 || '',
                  cTblObjective3: TblObjective3 || '',
                  cVirtualTbl3: VirtualTbl3 === 'Y',
                  cMultiDesignDb3: MultiDesignDb3 === 'Y',
                  cUploadSheet: UploadSheet || '',
                  cSheetNameList: SheetNameListList.filter(obj => { return obj.key === SheetNameList })[0],
                  cRowsToExamine: RowsToExamine || '',
                  cModifiedBy3: ModifiedBy3List.filter(obj => { return obj.key === ModifiedBy3 })[0],
                  cModifiedOn3: ModifiedOn3 || new Date(),
                  cLastSyncDt3: LastSyncDt3 || '',
                  cVirtualSql3: VirtualSql3 || '',
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
                                {(this.constructor.ShowSpinner(AdmDbTableState) && <Skeleton height='40px' />) ||
                                  <UncontrolledDropdown>
                                    <ButtonGroup className='btn-group--icons'>
                                      <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                      {
                                        dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3)).length > 0 &&
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
                                            if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3)) return null;
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
            {(authCol.TableId3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableId3 || {}).ColumnHeader} {(columnLabel.TableId3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableId3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableId3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTableId3'
disabled = {(authCol.TableId3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTableId3 && touched.cTableId3 && <span className='form__form-group-error'>{errors.cTableId3}</span>}
</div>
</Col>
}
{(authCol.SystemId3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SystemId3 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.SystemId3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SystemId3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SystemId3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSystemId3'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSystemId3')}
value={values.cSystemId3}
options={SystemId3List}
placeholder=''
disabled = {(authCol.SystemId3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSystemId3 && touched.cSystemId3 && <span className='form__form-group-error'>{errors.cSystemId3}</span>}
</div>
</Col>
}
{(authCol.TableName3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableName3 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TableName3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableName3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableName3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTableName3'
disabled = {(authCol.TableName3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTableName3 && touched.cTableName3 && <span className='form__form-group-error'>{errors.cTableName3}</span>}
</div>
</Col>
}
{(authCol.TableDesc3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TableDesc3 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.TableDesc3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TableDesc3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TableDesc3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTableDesc3'
disabled = {(authCol.TableDesc3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTableDesc3 && touched.cTableDesc3 && <span className='form__form-group-error'>{errors.cTableDesc3}</span>}
</div>
</Col>
}
{(authCol.TblObjective3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.TblObjective3 || {}).ColumnHeader} {(columnLabel.TblObjective3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.TblObjective3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.TblObjective3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cTblObjective3'
disabled = {(authCol.TblObjective3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cTblObjective3 && touched.cTblObjective3 && <span className='form__form-group-error'>{errors.cTblObjective3}</span>}
</div>
</Col>
}
{(authCol.VirtualTbl3 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cVirtualTbl3'
onChange={handleChange}
defaultChecked={values.cVirtualTbl3}
disabled={(authCol.VirtualTbl3 || {}).readonly || !(authCol.VirtualTbl3 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.VirtualTbl3 || {}).ColumnHeader}</span>
</label>
{(columnLabel.VirtualTbl3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VirtualTbl3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VirtualTbl3 || {}).ToolTip} />
)}
</div>
</Col>
}
{(authCol.MultiDesignDb3 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
<label className='checkbox-btn checkbox-btn--colored-click'>
<Field
className='checkbox-btn__checkbox'
type='checkbox'
name='cMultiDesignDb3'
onChange={handleChange}
defaultChecked={values.cMultiDesignDb3}
disabled={(authCol.MultiDesignDb3 || {}).readonly || !(authCol.MultiDesignDb3 || {}).visible}
/>
<span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
<span className='checkbox-btn__label'>{(columnLabel.MultiDesignDb3 || {}).ColumnHeader}</span>
</label>
{(columnLabel.MultiDesignDb3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.MultiDesignDb3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.MultiDesignDb3 || {}).ToolTip} />
)}
</div>
</Col>
}
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.ModelSample || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.ModelSample || {}).readonly || !(authCol.ModelSample || {}).visible} onClick={this.ModelSample({ naviBar, submitForm, currMst })} >{auxLabels.ModelSample || (columnLabel.ModelSample || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncByDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncByDb || {}).readonly || !(authCol.SyncByDb || {}).visible} onClick={this.SyncByDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncByDb || (columnLabel.SyncByDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.AnalToDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.AnalToDb || {}).readonly || !(authCol.AnalToDb || {}).visible} onClick={this.AnalToDb({ naviBar, submitForm, currMst })} >{auxLabels.AnalToDb || (columnLabel.AnalToDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
<Col lg={6} xl={6}>
<div className='form__form-group'>
<div className='d-block'>
{(authCol.SyncToDb || {}).visible && <Button color='secondary' size='sm' className='admin-ap-post-btn mb-10' disabled={(authCol.SyncToDb || {}).readonly || !(authCol.SyncToDb || {}).visible} onClick={this.SyncToDb({ naviBar, submitForm, currMst })} >{auxLabels.SyncToDb || (columnLabel.SyncToDb || {}).ColumnName}</Button>}
</div>
</div>
</Col>
{(authCol.UploadSheet || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.UploadSheet || {}).ColumnHeader} {(columnLabel.UploadSheet || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.UploadSheet || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.UploadSheet || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cUploadSheet'
disabled = {(authCol.UploadSheet || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cUploadSheet && touched.cUploadSheet && <span className='form__form-group-error'>{errors.cUploadSheet}</span>}
</div>
</Col>
}
{(authCol.SheetNameList || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.SheetNameList || {}).ColumnHeader} {(columnLabel.SheetNameList || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.SheetNameList || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.SheetNameList || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cSheetNameList'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cSheetNameList')}
value={values.cSheetNameList}
options={SheetNameListList}
placeholder=''
disabled = {(authCol.SheetNameList || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cSheetNameList && touched.cSheetNameList && <span className='form__form-group-error'>{errors.cSheetNameList}</span>}
</div>
</Col>
}
{(authCol.RowsToExamine || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.RowsToExamine || {}).ColumnHeader} {(columnLabel.RowsToExamine || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.RowsToExamine || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.RowsToExamine || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cRowsToExamine'
disabled = {(authCol.RowsToExamine || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cRowsToExamine && touched.cRowsToExamine && <span className='form__form-group-error'>{errors.cRowsToExamine}</span>}
</div>
</Col>
}
{(authCol.BtnScan || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.BtnScan || {}).ColumnHeader} {(columnLabel.BtnScan || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.BtnScan || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.BtnScan || {}).ToolTip} />
)}
</label>
}
</div>
</Col>
}
{(authCol.ModifiedBy3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedBy3 || {}).ColumnHeader} {(columnLabel.ModifiedBy3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedBy3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedBy3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DropdownField
name='cModifiedBy3'
onChange={this.DropdownChange(setFieldValue, setFieldTouched, 'cModifiedBy3')}
value={values.cModifiedBy3}
options={ModifiedBy3List}
placeholder=''
disabled = {(authCol.ModifiedBy3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cModifiedBy3 && touched.cModifiedBy3 && <span className='form__form-group-error'>{errors.cModifiedBy3}</span>}
</div>
</Col>
}
{(authCol.ModifiedOn3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ModifiedOn3 || {}).ColumnHeader} {(columnLabel.ModifiedOn3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ModifiedOn3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ModifiedOn3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<DatePicker
name='cModifiedOn3'
onChange={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn3', false)}
onBlur={this.DateChange(setFieldValue, setFieldTouched, 'cModifiedOn3', true)}
value={values.cModifiedOn3}
selected={values.cModifiedOn3}
disabled = {(authCol.ModifiedOn3 || {}).readonly ? true: false }/>
</div>
}
{errors.cModifiedOn3 && touched.cModifiedOn3 && <span className='form__form-group-error'>{errors.cModifiedOn3}</span>}
</div>
</Col>
}
{(authCol.LastSyncDt3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.LastSyncDt3 || {}).ColumnHeader} {(columnLabel.LastSyncDt3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.LastSyncDt3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.LastSyncDt3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cLastSyncDt3'
disabled = {(authCol.LastSyncDt3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cLastSyncDt3 && touched.cLastSyncDt3 && <span className='form__form-group-error'>{errors.cLastSyncDt3}</span>}
</div>
</Col>
}
{(authCol.VirtualSql3 || {}).visible &&
 <Col lg={6} xl={6}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.VirtualSql3 || {}).ColumnHeader} {(columnLabel.VirtualSql3 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.VirtualSql3 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.VirtualSql3 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmDbTableState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cVirtualSql3'
disabled = {(authCol.VirtualSql3 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cVirtualSql3 && touched.cVirtualSql3 && <span className='form__form-group-error'>{errors.cVirtualSql3}</span>}
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).TableId3)) return null;
                                        const buttonCount = a.length - a.filter(x => this.ActionSuppressed(authRow, x.buttonType, (currMst || {}).TableId3));
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === (a.length - 1);
                                        const outlineProperty = lastBtn ? false : true;
                                        return (
                                          <Col key={v.tid || v.order} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            {(this.constructor.ShowSpinner(AdmDbTableState) && <Skeleton height='43px' />) ||
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
  AdmDbTable: state.AdmDbTable,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmDbTableReduxObj.LoadPage.bind(AdmDbTableReduxObj) },
    { SavePage: AdmDbTableReduxObj.SavePage.bind(AdmDbTableReduxObj) },
    { DelMst: AdmDbTableReduxObj.DelMst.bind(AdmDbTableReduxObj) },
    { AddMst: AdmDbTableReduxObj.AddMst.bind(AdmDbTableReduxObj) },
//    { SearchMemberId64: AdmDbTableReduxObj.SearchActions.SearchMemberId64.bind(AdmDbTableReduxObj) },
//    { SearchCurrencyId64: AdmDbTableReduxObj.SearchActions.SearchCurrencyId64.bind(AdmDbTableReduxObj) },
//    { SearchCustomerJobId64: AdmDbTableReduxObj.SearchActions.SearchCustomerJobId64.bind(AdmDbTableReduxObj) },

    { showNotification: showNotification },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(MstRecord);

            