
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Prompt, Redirect } from 'react-router';
import { Formik, Field, Form } from 'formik';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import classNames from 'classnames';
import DocumentTitle from 'react-document-title';
import LoadingIcon from 'mdi-react/LoadingIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import DatePicker from '../../components/custom/DatePicker';
import NaviBar from '../../components/custom/NaviBar';
import FileInputField from '../../components/custom/FileInput';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import DropdownField from '../../components/custom/DropdownField';
import ModalDialog from '../../components/custom/ModalDialog';
import { showNotification } from '../../redux/Notification';
import RintagiScreen from '../../components/custom/Screen';
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation'
import {isEmptyId, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getDefaultPath, getNaviPath } from '../../helpers/utils';
import { toMoney, toInputLocalAmountFormat, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist';
import { getNaviBar } from './index';
import AdmReportReduxObj, { ShowMstFilterApplied } from '../../redux/AdmReport';
import Skeleton from 'react-skeleton-loader';
import ControlledPopover from '../../components/custom/ControlledPopover';

class DtlRecord extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmReport || {});
    this.blocker = null;
    this.titleSet = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'ReportId22';
    this.DtlKeyColumnName = 'ReportHlpId96';
    this.hasChangedContent = false;
    this.confirmUnload = this.confirmUnload.bind(this);
    this.AutoCompleteFilterBy = (option, props) => { return true };
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.ValidatePage = this.ValidatePage.bind(this);
    this.SavePage = this.SavePage.bind(this);
    this.FieldChange = this.FieldChange.bind(this);
    this.DateChange = this.DateChange.bind(this);
    this.StripEmbeddedBase64Prefix = this.StripEmbeddedBase64Prefix.bind(this);
    this.FileUploadChange = this.FileUploadChange.bind(this);
//    this.BGlChartId65InputChange = this.BGlChartId65InputChange.bind(this);
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      filename: '',
      submitting: false,
      Buttons: {},
      ScreenButton: null,
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      isMobile: false
    }
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }
  
  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

CultureId96InputChange() { const _this = this; return function (name, v) {const filterBy = ''; _this.props.SearchCultureId96(v, filterBy);}}
/* ReactRule: Detail Record Custom Function */
  /* ReactRule End: Detail Record Custom Function */

  ValidatePage(values) {
    const errors = {};
    const columnLabel = (this.props.AdmReport || {}).ColumnLabel || {};
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    /* standard field validation */
if (isEmptyId((values.cCultureId96 || {}).value)) { errors.cCultureId96 = (columnLabel.CultureId96 || {}).ErrMessage;}
if (!values.cDefaultHlpMsg96) { errors.cDefaultHlpMsg96 = (columnLabel.DefaultHlpMsg96 || {}).ErrMessage;}
if (!values.cReportTitle96) { errors.cReportTitle96 = (columnLabel.ReportTitle96 || {}).ErrMessage;}
    return errors;
  }

  SavePage(values, { setSubmitting, setErrors, resetForm, setFieldValue, setValues }) {


    this.setState({ submittedOn: Date.now(), submitting: true, setSubmitting: setSubmitting });
    const ScreenButton = this.state.ScreenButton || {};
/* ReactRule: Detail Record Save */
    /* ReactRule End: Detail Record Save */

    this.props.SavePage(
      this.props.AdmReport,
      this.props.AdmReport.Mst,
      [
        {
          ReportHlpId96: values.cReportHlpId96 || null,
          CultureId96: (values.cCultureId96|| {}).value || '',
          DefaultHlpMsg96: values.cDefaultHlpMsg96|| '',
          ReportTitle96: values.cReportTitle96|| '',
          _mode: ScreenButton.buttonType === 'DelRow' ? 'delete' : (values.cReportHlpId96 ? 'upd' : 'add'),
        }
      ],
      {
        persist: true,
        keepDtl: ScreenButton.buttonType !== 'NewSaveDtl'
      }
    )
  }
 
   /* standard screen button actions */
  CopyRow({ mst, dtl, dtlId, useMobileView }) {
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || (dtl || {}).ReportHlpId96;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.ReportId22, currDtlId);
          if (useMobileView) {
            const naviBar = getNaviBar('Mst', mst, {}, this.props.AdmReport.Label);
            this.props.history.push(getEditDtlPath(getNaviPath(naviBar, 'Dtl', '/'), '_'));
          }
          else {
            if (this.props.OnCopy) this.props.OnCopy();
          }
        }
        else {
          this.setState({ ModalOpen: true, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
        }
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  DelDtl({ mst, submitForm, ScreenButton, dtl, dtlId }) {
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ReportHlpId96;
        if (currDtlId) {
          const revisedState = {
            ScreenButton
          }
          this.setState(revisedState);
          submitForm();
        }
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });

    }.bind(this);
  }

  SaveCloseDtl({ submitForm, ScreenButton, naviBar, redirectTo, onSuccess }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  NewSaveDtl({ submitForm, ScreenButton, naviBar, mstId, dtl, redirectTo }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  SaveMst({ submitForm, ScreenButton }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  SaveDtl({ submitForm, ScreenButton }) {
    return function (evt) {
      const revisedState = {
        ScreenButton
      }
      this.setState(revisedState);
      submitForm();
    }.bind(this);
  }

  /* end of screen button action */

  /* react related stuff */
  static getDerivedStateFromProps(nextProps, prevState) {
    const nextReduxScreenState = nextProps.AdmReport || {};
    const buttons = nextReduxScreenState.Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'Dtl', prevState);
    const currentKey = nextReduxScreenState.key;
    const waiting = nextReduxScreenState.page_saving || nextReduxScreenState.page_loading;
    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;
    if (prevState.submitting && !waiting && nextReduxScreenState.submittedOn > prevState.submittedOn) {
      prevState.setSubmitting(false);
      revisedState.filename = '';
    }

    return revisedState;
  }

 confirmUnload(message, callback) {
    const AdmReportState = this.props.AdmReport || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
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
      const { mstId, dtlId } = { ...this.props.match.params };
      if (!(this.props.AdmReport || {}).AuthCol || true)
        this.props.LoadPage('Item', { mstId : mstId || '_', dtlId:dtlId || '_' });
    }
    else {
      return;
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const currReduxScreenState = this.props.AdmReport || {};

    if(!this.props.suppressLoadPage) {
      if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
        const _this = this;
        setTimeout(() => _this.props.setSpinner(false), 500);
      }
    }
    
    this.SetPageTitle(currReduxScreenState);
    if (prevstates.key !== (currReduxScreenState.EditDtl || {}).key) {
      if ((prevstates.ScreenButton || {}).buttonType === 'SaveCloseDtl') {
        const currMst = (currReduxScreenState.Mst);
        const currDtl = (currReduxScreenState.EditDtl);
        const dtlList = (currReduxScreenState.DtlList || {}).data || [];

        const naviBar = getNaviBar('Dtl', currMst, currDtl, currReduxScreenState.Label);
        const dtlListPath = getDefaultPath(getNaviPath(naviBar, 'DtlList', '/'));

        this.props.history.push(dtlListPath);
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

  handleFocus = (event) => {
    event.target.setSelectionRange(0, event.target.value.length);
  }

  render() {
    const AdmReportState = this.props.AdmReport || {};
    if (AdmReportState.access_denied) {
      return <Redirect to='/error' />;
    }
    const screenHlp = AdmReportState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailRecTitle = ((screenHlp || {}).DetailRecTitle || '');
    const DetailRecSubtitle = ((screenHlp || {}).DetailRecSubtitle || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');

    const screenButtons = AdmReportReduxObj.GetScreenButtons(AdmReportState) || {};
    const auxLabels = AdmReportState.Label || {};
    const auxSystemLabels = AdmReportState.SystemLabel || {};
    const columnLabel = AdmReportState.ColumnLabel || {};
    const currMst = AdmReportState.Mst;
    const currDtl = AdmReportState.EditDtl;
    const naviBar = getNaviBar('Dtl', currMst, currDtl, screenButtons);
    const authCol = this.GetAuthCol(AdmReportState);
    const authRow = (AdmReportState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
const CultureId96List = AdmReportReduxObj.ScreenDdlSelectors.CultureId96(AdmReportState);
const CultureId96 = currDtl.CultureId96;
const DefaultHlpMsg96 = currDtl.DefaultHlpMsg96;
const ReportTitle96 = currDtl.ReportTitle96;
// custome image upload code
//    const TrxDetImg65 = currDtl.TrxDetImg65 ? (currDtl.TrxDetImg65.startsWith('{') ? JSON.parse(currDtl.TrxDetImg65) : { fileName: '', mimeType: 'image/jpeg', base64: currDtl.TrxDetImg65 }) : null;
//    const TrxDetImg65FileUploadOptions = {
//      CancelFileButton: auxSystemLabels.CancelFileBtnLabel,
//      DeleteFileButton: auxSystemLabels.DeleteFileBtnLabel,
//      MaxImageSize: {
//        Width:(columnLabel.TrxDetImg65 || {}).ResizeWidth,
//        Height:(columnLabel.TrxDetImg65 || {}).ResizeHeight,
//      },
//      MinImageSize: {
//        Width:(columnLabel.TrxDetImg65 || {}).ColumnSize,
//        Height:(columnLabel.TrxDetImg65 || {}).ColumnHeight,
//      },
//    }
/* ReactRule: Detail Record Render */
/* ReactRule End: Detail Record Render */

    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <div className='account'>
            <div className='account__wrapper account-col'>
              <div className='account__card shadow-box rad-4'>
                {/* {!useMobileView && this.constructor.ShowSpinner(this.props.AdmReport) && <div className='panel__refresh'><LoadingIcon /></div>} */}
                {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>}
                <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                <Formik
                  initialValues={{
                  cCultureId96: CultureId96List.filter(obj => { return obj.key === currDtl.CultureId96 })[0],
                  cDefaultHlpMsg96: currDtl.DefaultHlpMsg96 || '',
                  cReportTitle96: currDtl.ReportTitle96 || '',
                  }}
                  validate={this.ValidatePage}
                  onSubmit={this.SavePage}
                  key={currDtl.key}
                  render={({
                    values,
                    errors,
                    touched,
                    isSubmitting,
                    dirty,
                    setFieldValue,
                    setFieldTouched,
                    handleReset,
                    handleChange,
                    submitForm,
                    handleFocus
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
                            <Col xs={9}>
                              <h3 className='account__title'>{DetailRecTitle}</h3>
                              <h4 className='account__subhead subhead'>{DetailRecSubtitle}</h4>
                            </Col>
                            <Col xs={3}>
                              <ButtonToolbar className='f-right'>
                                <UncontrolledDropdown>
                                  <ButtonGroup className='btn-group--icons'>
                                    <i className={dirty ? 'fa fa-exclamation exclamation-icon' : ''}></i>
                                    {
                                      dropdownMenuButtonList.filter(v => !v.expose && !this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22,currDtl.ReportHlpId96)).length > 0 &&
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
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22,currDtl.ReportHlpId96)) return null;
                                          return (
                                            <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, submitForm, mst: currMst, dtl: currDtl, useMobileView })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                        })
                                      }
                                    </DropdownMenu>
                                  }
                                </UncontrolledDropdown>
                              </ButtonToolbar>
                            </Col>
                          </Row>
                        </div>
                        <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}

                          <div className='w-100'>
                            <Row>
            {(authCol.CultureId96 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.CultureId96 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.CultureId96 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.CultureId96 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.CultureId96 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<AutoCompleteField
name='cCultureId96'
onChange={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId96', false)}
onBlur={this.FieldChange(setFieldValue, setFieldTouched, 'cCultureId96', true)}
onInputChange={this.CultureId96InputChange()}
value={values.cCultureId96}
defaultSelected={CultureId96List.filter(obj => { return obj.key === CultureId96 })}
options={CultureId96List}
filterBy={this.AutoCompleteFilterBy}
disabled = {(authCol.CultureId96 || {}).readonly ? true: false }/>
</div>
}
{errors.cCultureId96 && touched.cCultureId96 && <span className='form__form-group-error'>{errors.cCultureId96}</span>}
</div>
</Col>
}
{(authCol.DefaultHlpMsg96 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.DefaultHlpMsg96 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.DefaultHlpMsg96 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.DefaultHlpMsg96 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.DefaultHlpMsg96 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cDefaultHlpMsg96'
disabled = {(authCol.DefaultHlpMsg96 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cDefaultHlpMsg96 && touched.cDefaultHlpMsg96 && <span className='form__form-group-error'>{errors.cDefaultHlpMsg96}</span>}
</div>
</Col>
}
{(authCol.ReportTitle96 || {}).visible &&
 <Col lg={12} xl={12}>
<div className='form__form-group'>
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='20px' />) ||
<label className='form__form-group-label'>{(columnLabel.ReportTitle96 || {}).ColumnHeader} <span className='text-danger'>*</span>{(columnLabel.ReportTitle96 || {}).ToolTip && 
 (<ControlledPopover id={(columnLabel.ReportTitle96 || {}).ColumnName} className='sticky-icon pt-0 lh-23' message= {(columnLabel.ReportTitle96 || {}).ToolTip} />
)}
</label>
}
{((true && this.constructor.ShowSpinner(AdmReportState)) && <Skeleton height='36px' />) ||
<div className='form__form-group-field'>
<Field
type='text'
name='cReportTitle96'
disabled = {(authCol.ReportTitle96 || {}).readonly ? 'disabled': '' }/>
</div>
}
{errors.cReportTitle96 && touched.cReportTitle96 && <span className='form__form-group-error'>{errors.cReportTitle96}</span>}
</div>
</Col>
}
                            </Row>
                          </div>
                          <div className='form__form-group mb-0'>
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
                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ReportId22,currDtl.ReportHlpId96)) return null;
                                        const buttonCount = a.length;
                                        const colWidth = parseInt(12 / buttonCount, 10);
                                        const lastBtn = i === a.length - 1;
                                        const outlineProperty = lastBtn ? false : true;

                                        return (
                                          <Col key={v.tid} xs={colWidth} sm={colWidth} className='btn-bottom-column' >
                                            <Button color='success' type='button' outline={outlineProperty} className='account__btn' disabled={isSubmitting} onClick={this.ScreenButtonAction[v.buttonType]({ submitForm, naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}>{v.label}</Button>
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
  AdmReport: state.AdmReport,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { showNotification: showNotification },
    { LoadPage: AdmReportReduxObj.LoadPage.bind(AdmReportReduxObj) },
    { AddDtl: AdmReportReduxObj.AddDtl.bind(AdmReportReduxObj) },
    { SavePage: AdmReportReduxObj.SavePage.bind(AdmReportReduxObj) },
{ SearchCultureId96: AdmReportReduxObj.SearchActions.SearchCultureId96.bind(AdmReportReduxObj) },
  { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlRecord);

