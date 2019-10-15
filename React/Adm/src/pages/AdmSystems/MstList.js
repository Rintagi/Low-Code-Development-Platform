
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Redirect,withRouter } from 'react-router';
import { Formik, Field, Form } from 'formik';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import LoadingIcon from 'mdi-react/LoadingIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import PlusIcon from 'mdi-react/PlusIcon';
import MagnifyIcon from 'mdi-react/MagnifyIcon';
import RadioboxBlankIcon from 'mdi-react/RadioboxBlankIcon';
import classNames from 'classnames';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import DropdownField from '../../components/custom/DropdownField';
import NaviBar from '../../components/custom/NaviBar';
import RintagiScreen from '../../components/custom/Screen'
import ModalDialog from '../../components/custom/ModalDialog';
import { getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath } from '../../helpers/utils'
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import AdmSystemsReduxObj, { ShowMstFilterApplied } from '../../redux/AdmSystems';
import { setTitle, setSpinner } from '../../redux/Global';
import { getNaviBar } from './index';
import MstRecord from './MstRecord';
import DocumentTitle from 'react-document-title';

class MstList extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmSystems || {});
    this.hasChangedContent = false;
    this.titleSet = false;
    this.MstKeyColumnName = 'SystemId45';
    this.SystemName = 'FintruX';
    this.SetCurrentRecordState = this.SetCurrentRecordState.bind(this);
    this.SearchBoxFocus = this.SearchBoxFocus.bind(this);
    this.SelectMstListRow = this.SelectMstListRow.bind(this);
    this.FilterValueChange = this.SearchFilterValueChange.bind(this);
    this.ShowMoreSearchList = this.ShowMoreSearchList().bind(this);
    this.RefreshSearchList = this.RefreshSearchList.bind(this);
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.OnMstCopy = () => { this.setState({ ShowMst: true }) };
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      Buttons: {},
      ScreenButton: null,
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      ShowMst: false,
      isMobile: false,
    };
//    const lastAppUrl = GetCurrent('LastAppUrl',true);
      const lastAppUrl = null;
 
    if (lastAppUrl && !(this.props.AdmSystems || {}).initialized) {
      if (lastAppUrl.pathname !== ((this.props.history || {}).location || {}).pathname) {
          this.props.history.push(lastAppUrl.pathname);
      }
    }
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }

 

  /* standard screen button actions for each screen, must implement if button defined */
  Print({ mst, mstId }) {
    return function (evt) { }.bind(this);
  }

  CopyHdr({ naviBar, ScreenButton, useMobileView, mst, mstId }) {
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const fromMstId = mstId || mst.SystemId45;
      const copyFn = () => {
        this.props.AddMst(fromMstId, 'MstList', 0);
        this.setState({
          ScreenButton: ScreenButton.buttonType,
          key: fromMstId,
          ShowMst: true,
        });
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  AddNewMst({ naviBar, useMobileView }) {
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    if (useMobileView) return super.AddNewMst({ naviBar });
    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddMst(null, 'MstList', 0);
        this.setState({
          ScreenButton: 'New',
          ShowMst: true,
        });
      }
      if(!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  };

  AddNewDtl({ naviBar, mstId, useMobileView }) {
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    if (useMobileView) return super.AddNewDtl({ naviBar, mstId });
    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddDtl(mstId, null, -1);
        this.props.history.push(getAddDtlPath(getNaviPath(naviBar, 'DtlList', '/')) + '_');
      }
      if(!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  };

  DelMst({ naviBar, ScreenButton, mst, mstId }) {
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    return this.Prompt({
      okFn: function(evt) {
        const fromMstId = mstId || mst.SystemId45;
        this.props.DelMst(this.props.AdmSystems, fromMstId);        
      }.bind(this) ,
      message: auxSystemLabels.DeletePageMsg || ''
    }).bind(this); 
  }

  ExpMstTxt() {
    return function (evt) { }.bind(this);
  }
  /* end of screen button action */



  RefreshSearchList(values, { setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const AdmSystemsState = this.props.AdmSystems || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    const refreshFn = (() => {
      this.props.SetScreenCriteria(values.search,
        {
           
        },
        (values.cFilterId) ? values.cFilterId.value : 0);
      }).bind(this);
    if(true || !this.hasChangedContent) refreshFn();
    else this.setState({ ModalOpen: true, ModalSuccess: refreshFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
  }

  /* react related calls */
  static getDerivedStateFromProps(nextProps, prevState) {
    const buttons = (nextProps.AdmSystems || {}).Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'MstList', prevState);

    let revisedState = {};
    if (revisedButtonDef) revisedState.Buttons = revisedButtonDef;
    return revisedState;
  }

  mediaqueryresponse(value) {
    if (value.matches) { // if media query matches
      this.setState({ isMobile: true });
    }
    else {
      this.setState({ isMobile: false });
    }
  }

  componentDidMount() {
    const { mstId } = { ...this.props.match.params };
    if (!(this.props.AdmSystems || {}).AuthCol || true) {
      this.props.LoadPage('SearchList', { mstId: mstId || '_' });
      this.props.LoadInitPage({ keyId: null });
    }

    this.mediaqueryresponse(this.mobileView);

    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes

  }

  componentDidUpdate(prevProps, prevStates) {
    const currReduxScreenState = this.props.AdmSystems || {};
    // const isMobileView = this.state.isMobile;
    // const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

    if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
      const _this = this;
      setTimeout(() => _this.props.setSpinner(false), 500);
    }
    // else if(currReduxScreenState.initialized && this.props.global.pageSpinner) {
    //   this.props.setSpinner(false);
    // }
    
    this.SetPageTitle(currReduxScreenState);

    if (prevStates.key !== (currReduxScreenState.Mst || {}).SystemId45) {
      const isMobileView = this.state.isMobile;
      const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

      if (prevStates.ScreenButton === 'Copy' && useMobileView) {
        const naviBar = getNaviBar('MstList', {}, {}, currReduxScreenState.Label);
        const editMstPath = getEditMstPath(getNaviPath(naviBar, 'Mst', '/'), '_');
        this.props.history.push(editMstPath);
      }
    }
  }

  componentWillUnmount() {
    this.mobileView.removeListener(this.mediaqueryresponse);
  }

  render() {
    const AdmSystemsState = this.props.AdmSystems || {};

    if (AdmSystemsState.access_denied) {
      return <Redirect to='/error' />;
    }

    // if (!AdmSystemsState.initialized || AdmSystemsState.page_loading) return null;
    const screenHlp = AdmSystemsState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const MasterLstTitle = ((screenHlp || {}).MasterLstTitle || '');
    const MasterLstSubtitle = ((screenHlp || {}).MasterLstSubtitle || '');
    const IncrementMsg = ((screenHlp || {}).IncrementMsg || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');
    const AddMasterMsg = ((screenHlp || {}).AddMasterMsg || '');
    const MasterFoundMsg = ((screenHlp || {}).MasterFoundMsg || '');

    const screenButtons = AdmSystemsReduxObj.GetScreenButtons(AdmSystemsState);
    const screenDdlSelectors = AdmSystemsReduxObj.ScreenDdlSelectors;
    const screenCriDdlSelectors = AdmSystemsReduxObj.ScreenCriDdlSelectors;
    const screenCriteria = AdmSystemsState.ScreenCriteria || {}
    const selectList = AdmSystemsReduxObj.SearchListToSelectList(AdmSystemsState);
    const itemList = AdmSystemsState.Dtl || [];
    const auxLabels = AdmSystemsState.Label || {};
    const auxSystemLabels = AdmSystemsState.SystemLabel || {};
    const columnLabel = AdmSystemsState.ColumnLabel || {};
    const currMst = AdmSystemsState.Mst;
    const currDtl = AdmSystemsState.EditDtl;
    const naviBar = getNaviBar('MstList', currMst, {}, screenButtons).filter(v=>((v.type !== 'Dtl' && v.type !=='DtlList') || currMst.SystemId45));
    const naviSelectBar = getNaviBar('MstList', {}, {}, screenButtons);
    const screenFilterList = AdmSystemsReduxObj.QuickFilterDdlToSelectList(AdmSystemsState);
    const screenFilterSelected = screenFilterList.filter(obj => { return obj.key === screenCriteria.FilterId });
    const authRow = (AdmSystemsState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, rowMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasBottomButton || hasRowButton || hasDropdownMenuButton;

    let colorDark = classNames({
      // 'color-dark': haveAllButtons
    });

    let noMargin = classNames({ 'mb-0': !hasBottomButton });

    // Filter visibility
    let filterVisibility = classNames({ 'd-none': true, 'd-block': screenCriteria.ShowFilter });
    let filterBtnStyle = classNames({ 'filter-button-clicked': screenCriteria.ShowFilter });
    let filterActive = classNames({ 'filter-icon-active': screenCriteria.ShowFilter });

 

    const hasScreenFilter = screenFilterList.length > 0;
    const activeSelectionVisible = selectList.filter(v => v.isSelected).length > 0;

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
    const getMainRowDesc = (desc) => (desc.replace(/^[0-9.]+\s\[\w*\]/));
    return (
      <DocumentTitle title={siteTitle}>
      <div className='top-level-split'>
        <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
        <Row className='no-margin-right'>
          <Col className='no-padding-right' xs='6'>
            <div className='account'>
              <div className='account__wrapper account-col'>
                <div className='account__card rad-4'>
                  {/* {this.constructor.ShowSpinner(AdmSystemsState,'MstList') && siteSpinner} */}
                  {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                    <div className='tabs__wrap'>
                      <NaviBar history={this.props.history} navi={naviBar} key={currMst.key} mstListCount={(selectList||[]).length} />
                    </div>
                  </div>}
                  <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                  <div className='account__head'>
                    <Row>
                      <Col xs={8}>
                        <h3 className='account__title'>{MasterLstTitle}</h3>
                        <h4 className='account__subhead subhead'>{MasterLstSubtitle}</h4>
                      </Col>
                      <Col xs={4}>
                        <ButtonToolbar className='f-right'>
                          <UncontrolledDropdown>
                            <ButtonGroup className='btn-group--icons'>
                              <Button className={`mw-50 ${filterBtnStyle}`} onClick={this.props.changeMstListFilterVisibility} outline>
                                <i className={`fa fa-filter icon-holder ${filterActive}`}></i>{ShowMstFilterApplied(AdmSystemsState) && <i className='filter-applied'></i>}
                                {!useMobileView && <p className='action-menu-label'>{(screenButtons.Filter || {}).label}</p>}
                              </Button>
                              {
                                dropdownMenuButtonList.filter(v => v.expose).map(v => {
                                  return (
                                    <Button
                                      key={v.tid || v.buttonType}
                                      // disabled={!activeSelectionVisible || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)}
                                      onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}
                                      className='mw-50 add-report-expense'
                                      outline>
                                      <i className={`${v.iconClassName} icon-holder`}></i>
                                      {!useMobileView && <p className='action-menu-label'>{v.label}</p>}
                                    </Button>)
                                })
                              }
                              {activeSelectionVisible &&
                                dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                                <DropdownToggle className='mw-50' outline>
                                  <i className='fa fa-ellipsis-h icon-holder'></i>
                                  {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                </DropdownToggle>
                              }
                            </ButtonGroup>
                            {dropdownMenuButtonList.filter(v => !v.expose).length > 0 &&
                              <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                {
                                  dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                    return (
                                      <DropdownItem
                                        key={v.tid || v.order}
                                        disabled={!activeSelectionVisible || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)}
                                        onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}
                                        className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                  })
                                }
                              </DropdownMenu>
                            }
                          </UncontrolledDropdown>
                        </ButtonToolbar>
                      </Col>
                    </Row>
                  </div>
                  <Formik
                    initialValues={{

 
                      search: screenCriteria.SearchStr || '',
                      cFilterId: (screenFilterSelected.length > 0 ? screenFilterSelected[0] : screenFilterList[0])
                    }}
                    key={AdmSystemsState.searchListVersion}
                    onSubmit={this.RefreshSearchList}
                    render={({
                      values,
                      isSubmitting,
                      handleChange,
                      handleSubmit,
                      handleBlur,
                      setFieldValue,
                      setFieldTouched
                    }) => (
                        <div>
                          <Form className='form'>
                            <div className='form__form-group'>
                              <div className={`form__form-group filter-padding ${filterVisibility}`} key={screenCriteria.key}>
                                <Row className='mb-5'>
                                  
                                </Row>
                                <Row>
                                  {hasScreenFilter && <Col xs={4} md={3}>
                                    <label className='form__form-group-label filter-label'>{auxSystemLabels.QFilter}</label>
                                    <div className='form__form-group-field filter-form-border'>
                                      <DropdownField
                                        name='cFilterId'
                                        onBlur={setFieldTouched}
                                        // onChange={setFieldValue}
                                        onChange={this.SearchFilterValueChange(handleSubmit, setFieldValue, 'ddl', 'cFilterId')}
                                        value={values.cFilterId}
                                        options={screenFilterList}
                                      />
                                    </div>
                                  </Col>}
                                  <Col xs={hasScreenFilter ? 8 : 12} md={hasScreenFilter ? 9 : 12} className={hasScreenFilter ? 'col-last-modified' : ''}>
                                    <label className='form__form-group-label filter-label'>{auxSystemLabels.FilterSearchLabel}</label>
                                    <div className='form__form-group-field filter-form-border'>
                                      <Field
                                        className='white-left-border'
                                        type='text'
                                        name='search'
                                        value={values.search}
                                        onFocus={(e) => this.SearchBoxFocus(e)}
                                        onBlur={(e) => this.SearchBoxFocus(e)}
                                      />
                                      <span onClick={handleSubmit} className={`form__form-group-button desktop-filter-button search-btn-fix ${this.state.searchFocus ? ' active' : ''}`}><MagnifyIcon /></span>
                                    </div>
                                  </Col>
                                </Row>
                              </div>
                              <h5 className='fill-fintrux pb-10 fw-700'><span className='color-dark mr-5'>{MasterFoundMsg}:</span> {screenCriteria.MatchCount}</h5>
                              {selectList.map((obj, i) => {
                                return (
                                  <div className='form__form-group-narrow list-divider' key={i}>
                                    <div className='form__form-group-field'>
                                      <label className='radio-btn radio-btn--button margin-narrow'>
                                        <Field
                                          className='radio-btn__radio'
                                          name='SystemId45'
                                          listidx={obj.idx}
                                          keyid={obj.key}
                                          type='radio'
                                          value={obj.key || ''}
                                          onClick={this.SelectMstListRow({naviBar:naviSelectBar,useMobileView})}
                                          defaultChecked={obj.isSelected ? true : false}
                                        />
                                        <span className='radio-bg-solver'></span>
                                        {hasActableButtons &&
                                          <span className='radio-btn__label-svg'>
                                            <CheckIcon className='radio-btn__label-check' />
                                            <RadioboxBlankIcon className='radio-btn__label-uncheck' />
                                          </span>
                                        }
                                        <span className={`radio-btn__label ${colorDark}`}>
                                          <div className='row-cut'>{this.FormatSearchTitleL(obj.label)}</div>
                                          <div className='row-cut row-bottom-report'>{this.FormatSearchSubTitleL(obj.detail)}</div>
                                        </span>
                                        <span className={`radio-btn__label__right ${colorDark}`}>
                                          <div>{this.FormatSearchTitleR(obj.labelR)}</div>
                                          <div className='row-bottom-report f-right'>{this.FormatSearchSubTitleR(obj.detailR)}</div>
                                        </span>
                                        {
                                          rowMenuButtonList
                                            .filter(v => v.expose && !useMobileView)
                                            .map((v, i) => {
                                              if (this.ActionSuppressed(authRow, v.buttonType, obj.key)) return null;
                                              return (
                                                <button type='button' key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, useMobileView, mstId: obj.key })} className={`${v.exposedClassName}`}><i className={`${v.iconClassName}`}></i></button>
                                              )
                                            })
                                        }
                                        {
                                          rowMenuButtonList.filter(v => !v.expose || useMobileView).length > 0 &&
                                          <UncontrolledDropdown className={`btn-row-dropdown`}>
                                            <DropdownToggle className='btn-row-dropdown-icon btn-row-menu' onClick={(e) => { e.preventDefault() }}>
                                              <i className='fa fa-ellipsis-h icon-holder menu-icon'></i>
                                            </DropdownToggle>
                                            <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                              {rowMenuButtonList
                                                .filter(v => !v.expose || useMobileView)
                                                .map((v) => {
                                                  if (this.ActionSuppressed(authRow, v.buttonType, obj.key)) return null;
                                                  return <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, ScreenButton: v, useMobileView, mstId: obj.key })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.labelLong}</DropdownItem>
                                                })
                                              }
                                            </DropdownMenu>
                                          </UncontrolledDropdown>
                                        }
                                      </label>
                                    </div>
                                  </div>
                                )
                              })}
                            </div>
                            {this.constructor.ShowMoreMstBtn(AdmSystemsState) && <Button className={`btn btn-view-more-blue account__btn ${noMargin}`} onClick={this.ShowMoreSearchList} type='button'>{strFormat(IncrementMsg, AdmSystemsState.ScreenCriteria.Increment)}<br /><i className='fa fa-arrow-down'></i></Button>}
                            {useMobileView && activeSelectionVisible &&
                              bottomButtonList.filter(v => v.expose).length > 0 &&
                              <div className='width-wrapper'>
                                <div className='buttons-bottom-container'>
                                  <Row className='btn-bottom-row'>
                                    {
                                      bottomButtonList
                                        .filter(v => v.expose)
                                        .map((v, i, a) => {
                                          if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).SystemId45)) return null;
                                          const buttonCount = a.length;
                                          const colWidth = parseInt(12 / buttonCount, 10);
                                          const lastBtn = i === a.length - 1;
                                          const outlineProperty = lastBtn ? false : true;
                                          return (
                                            <Col key={v.tid} xs={colWidth} sm={colWidth} className='btn-bottom-column'>
                                              <Button color='success' type='button' outline={outlineProperty} className='account__btn' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, ScreenButton: v, mst: currMst, dtl: currDtl, useMobileView })}>{v.labelLong}</Button>
                                            </Col>
                                          )
                                        })
                                    }
                                  </Row>
                                </div>
                              </div>
                            }
                          </Form>
                        </div>
                      )}
                  />
                </div>
              </div>
            </div>
          </Col>
          {!useMobileView &&
            <Col xs={6}>
              {!activeSelectionVisible &&
                !this.state.ShowMst &&
                <div className='empty-block'>
                  <img className='folder-img' alt='' src={require('../../img/folder.png')} />
                  <p className='create-new-message'>{NoMasterMsg}. <span className='link-imitation' onClick={this.AddNewMst({ naviBar: naviSelectBar, useMobileView })}>{AddMasterMsg}</span></p>
                </div>
              }

              {(activeSelectionVisible || this.state.ShowMst) && <MstRecord key={currMst.key} history={this.props.history} updateChangedState={this.SetCurrentRecordState} onCopy={this.OnMstCopy} suppressLoadPage={true} />}

            </Col>}
        </Row>

      </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  error: state.error,
  AdmSystems: state.AdmSystems,
  filter: state.filter,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmSystemsReduxObj.LoadPage.bind(AdmSystemsReduxObj) },
    { LoadInitPage: AdmSystemsReduxObj.LoadInitPage.bind(AdmSystemsReduxObj) },
    { LoadSearchList: AdmSystemsReduxObj.LoadSearchList.bind(AdmSystemsReduxObj) },
    { SelectMst: AdmSystemsReduxObj.SelectMst.bind(AdmSystemsReduxObj) },
    { DelMst: AdmSystemsReduxObj.DelMst.bind(AdmSystemsReduxObj) },
    { AddMst: AdmSystemsReduxObj.AddMst.bind(AdmSystemsReduxObj) },
    { AddDtl: AdmSystemsReduxObj.AddDtl.bind(AdmSystemsReduxObj) },    
    { changeMstListFilterVisibility: AdmSystemsReduxObj.ChangeMstListFilterVisibility.bind(AdmSystemsReduxObj) },
    { SetScreenCriteria: AdmSystemsReduxObj.SetScreenCriteria.bind(AdmSystemsReduxObj) },
 
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MstList));

            