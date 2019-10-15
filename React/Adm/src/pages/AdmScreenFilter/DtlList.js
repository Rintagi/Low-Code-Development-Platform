
import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import { bindActionCreators, compose } from 'redux';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from 'reactstrap';
import { Formik, Field, Form } from 'formik';
import DocumentTitle from 'react-document-title';
import MagnifyIcon from 'mdi-react/MagnifyIcon';
import CheckIcon from 'mdi-react/CheckIcon';
import PlusIcon from 'mdi-react/PlusIcon';
import RadioboxBlankIcon from 'mdi-react/RadioboxBlankIcon';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import NaviBar from '../../components/custom/NaviBar';
import DropdownField from '../../components/custom/DropdownField';
import RintagiScreen from '../../components/custom/Screen'
import ModalDialog from '../../components/custom/ModalDialog';
import classNames from 'classnames';
import { toMoney, toLocalAmountFormat, toLocalDateFormat, toDate, strFormat } from '../../helpers/formatter';
import { getSelectedFromList, getAddDtlPath, getAddMstPath, getEditDtlPath, getEditMstPath, getNaviPath, getListDisplayContet } from '../../helpers/utils'
import { setTitle, setSpinner } from '../../redux/Global';
import { RememberCurrent, GetCurrent } from '../../redux/Persist'
import { getNaviBar } from './index';
import DtlRecord from './DtlRecord';
import AdmScreenFilterReduxObj from '../../redux/AdmScreenFilter';

class DtlList extends RintagiScreen {
  constructor(props) {
    super(props);
    this.GetReduxState = ()=> (this.props.AdmScreenFilter || {});
    this.titleSet = false;
    this.hasChangedContent = false;
    this.SystemName = 'FintruX';
    this.MstKeyColumnName = 'ScreenFilterId86';
    this.DtlKeyColumnName = 'ScreenFilterHlpId87';
    this.SetCurrentRecordState = this.SetCurrentRecordState.bind(this);
    this.SearchBoxFocus = this.SearchBoxFocus.bind(this);
    this.SelectDtlListRow = this.SelectDtlListRow.bind(this);
    this.FilterDtlList = this.FilterDtlList.bind(this);
    this.FilteredColumnChange = this.FilteredColumnChange.bind(this);
    this.ResetFilterList = this.ResetFilterList.bind(this);
    this.OnModalReturn = this.OnModalReturn.bind(this);
    this.OnDtlCopy = () => { this.setState({ ShowDtl: true }) };
    this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    this.mobileView = window.matchMedia('(max-width: 1200px)');

    this.state = {
      searchFocus: false,
      /* this is needed due to the oddball use of remembering focus which force formik render thus loss the selected value(before sumbit) */
      DtlFilter: {},
      Buttons: {},
      ModalColor: '',
      ModalTitle: '',
      ModalMsg: '',
      ModalOpen: false,
      ModalSuccess: null,
      ShowDtl: false,
      isMobile: false,
    };
    if (!this.props.suppressLoadPage && this.props.history) {
      RememberCurrent('LastAppUrl',(this.props.history || {}).location,true);
    }

    this.props.setSpinner(true);
  }


  /* standard screen button actions */
  Print({ naviBar, mst, mstId }) {
    return function (evt) { }.bind(this);
  }
  CopyRow({ mst, dtlId, dtl, ...rest }) {
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const currDtlId = dtlId || dtl.ScreenFilterHlpId87;
      const copyFn = () => {
        if (currDtlId) {
          this.props.AddDtl(mst.ScreenFilterId86, currDtlId);
          const isMobileView = window.matchMedia('(max-width: 1099px)').matches;
          const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);
          if (useMobileView) {
            const naviBar = getNaviBar('DtlList', mst, {}, this.props.AdmScreenFilter.Label);
            const path = getEditDtlPath(getNaviPath(naviBar, 'Dtl', '/'), '_');
            this.props.history.push(path);
          }
          else {
            this.setState({
              ShowDtl: true,
            });
          }
        }
      }
      if(!this.hasChangedContent) copyFn();
      else this.setState({ ModalOpen: true, ModalSuccess: copyFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);

  }

  AddNewDtl({ naviBar, useMobileView, mstId }) {
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    if (useMobileView) return super.AddNewDtl({ naviBar });

    return function (evt) {
      evt.preventDefault();
      const addFn = () => {
        this.props.AddDtl(mstId, null);
        this.setState({
          ShowDtl: true,
        });
      }
      if (!this.hasChangedContent) addFn();
      else this.setState({ ModalOpen: true, ModalSuccess: addFn, ModalColor: 'warning', ModalTitle: auxSystemLabels.UnsavedPageTitle || '', ModalMsg: auxSystemLabels.UnsavedPageMsg || '' });
    }.bind(this);
  }

  DelDtl({ mst, dtlId, dtl }) {
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      const deleteFn = () => {
        const currDtlId = dtlId || dtl.ScreenFilterHlpId87;
        if (currDtlId) {
          this.props.SavePage(
            this.props.AdmScreenFilter,
            this.props.AdmScreenFilter.Mst,
            [
              {
                ScreenFilterHlpId87: currDtlId,
                _mode: 'delete',
              }
            ],
            {
              persist: true,
              // resizeImage: 'TrxDetImg65',

            }
          )
        }
        else {
          /* delete local temp */
        }
      };
      this.setState({ ModalOpen: true, ModalSuccess: deleteFn, ModalColor: 'danger', ModalTitle: auxSystemLabels.WarningTitle || '', ModalMsg: auxSystemLabels.DeletePageMsg || '' });

    }.bind(this);
  }
  ExpMstTxt() {
    return function (evt) { }.bind(this);
  }
  /* end of screen button action */


  /* react related calls */
  static getDerivedStateFromProps(nextProps, prevState) {
    const buttons = (nextProps.AdmScreenFilter || {}).Buttons || {};
    const revisedButtonDef = super.GetScreenButtonDef(buttons, 'DtlList', prevState);
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
    const { mstId, dtlId } = { ...this.props.match.params };

    if (!(this.props.AdmScreenFilter || {}).AuthCol || true)
      this.props.LoadPage('DtlList', { mstId:mstId || '_', dtlId:dtlId || '_' });

    this.mediaqueryresponse(this.mobileView);
    this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes
  }

  componentDidUpdate(prevProps, prevStates) {
    const currReduxScreenState = this.props.AdmScreenFilter || {};

    if(!currReduxScreenState.page_loading && this.props.global.pageSpinner) {
      const _this = this;
      setTimeout(() => _this.props.setSpinner(false), 500);
    }

    this.SetPageTitle(currReduxScreenState);
  }

  componentWillUnmount() {
    this.mobileView.removeListener(this.mediaqueryresponse);
  }

  render() {
    const AdmScreenFilterState = this.props.AdmScreenFilter || {};
    const { targetMstId, targetDtlId } = this.GetTargetId();
    if (AdmScreenFilterState.access_denied) {
      return <Redirect to='/error' />;
    }
    //if (!AdmScreenFilterState.initialized || AdmScreenFilterState.page_loading) return null;
    const screenHlp = AdmScreenFilterState.ScreenHlp;

    // Labels
    const siteTitle = (this.props.global || {}).pageTitle || '';
    const DetailLstTitle = ((screenHlp || {}).DetailLstTitle || '');
    const DetailLstSubtitle = ((screenHlp || {}).DetailLstSubtitle || '');
    const IncrementMsg = ((screenHlp || {}).IncrementMsg || '');
    const NoDetailMsg = ((screenHlp || {}).NoDetailMsg || '');
    const NoMasterMsg = ((screenHlp || {}).NoMasterMsg || '');
    const AddDetailMsg = ((screenHlp || {}).AddDetailMsg || '');
    const DetailFoundMsg = ((screenHlp || {}).DetailFoundMsg || '');

    const screenButtons = AdmScreenFilterReduxObj.GetScreenButtons(AdmScreenFilterState) || {};
    const itemList = AdmScreenFilterReduxObj.DtlListToSelectList(AdmScreenFilterState, 'ScreenFilterHlpId87');
    const selectList = AdmScreenFilterReduxObj.SearchListToSelectList(AdmScreenFilterState);
    const selectedMst = (selectList || []).filter(v=>v.isSelected)[0] || {};
    const auxLabels = AdmScreenFilterState.Label || {};
    const auxSystemLabels = AdmScreenFilterState.SystemLabel || {};
    const columnLabel = AdmScreenFilterState.ColumnLabel;
    const columnDefinition = Object.keys(columnLabel).reduce((a,o,i)=>{
    const x = columnLabel[o];
       if (x['DtlLstPosId'] == '1'){ a['dTopL'] = x;};
       if (x['DtlLstPosId'] == '2'){ a['dBottomL'] = x;};
       if (x['DtlLstPosId'] == '3'){ a['dTopR'] = x;};
       if (x['DtlLstPosId'] == '4'){ a['dBottomR'] = x;};  
       return a;
    },{});     
    const dTopL =  columnDefinition['dTopL'] || {};
    const dBottomL =  columnDefinition['dBottomL'] || {};
    const dTopR =  columnDefinition['dTopR'] || {};
    const dBottomR =  columnDefinition['dBottomR'] || {};
    const currMst = AdmScreenFilterState.Mst;
    const currDtl = AdmScreenFilterState.EditDtl;
    const dtlFilter = AdmScreenFilterState.DtlFilter;
    const filterList = AdmScreenFilterState.DtlFilter.FilterColumnDdl.map((v, i) => ({ key: v.ColName, label: v.ColumnHeader, value: v.ColName, pos: i }))
    const naviBar = getNaviBar('DtlList', currMst, currDtl, screenButtons);
    const authRow = (AdmScreenFilterState.AuthRow || [])[0] || {};
    const { dropdownMenuButtonList, rowMenuButtonList, bottomButtonList, hasDropdownMenuButton, hasBottomButton, hasRowButton } = this.state.Buttons;
    const hasActableButtons = hasRowButton || hasDropdownMenuButton;
    const activeSelectionVisible = itemList.filter(v => v.isSelected).length > 0;

    let colorDark = classNames({
      // 'color-dark': haveAllButtons
    });

    // Filter visibility
    let filterVisibility = classNames({ 'd-none': true, 'd-block': dtlFilter.ShowFilter });
    let filterBtnStyle = classNames({ 'filter-button-clicked': dtlFilter.ShowFilter });
    let filterActive = classNames({ 'filter-icon-active': dtlFilter.ShowFilter });
    let viewMoreClass = classNames({ 'd-none': dtlFilter.Limit >= itemList.length });

    const isMobileView = this.state.isMobile;
    const useMobileView = (isMobileView && !(this.props.user || {}).desktopView);

    return (
      <DocumentTitle title={siteTitle}>
        <div className='top-level-split'>
          <ModalDialog color={this.state.ModalColor} title={this.state.ModalTitle} onChange={this.OnModalReturn} ModalOpen={this.state.ModalOpen} message={this.state.ModalMsg} />
          <Row className='no-margin-right'>
            <Col className='no-padding-right' xs='6'>
              <div className='account'>
                <div className='account__wrapper account-col'>
                  <div className='account__card rad-4'>
                    {useMobileView && <div className='tabs tabs--justify tabs--bordered-bottom'>
                      <div className='tabs__wrap'>
                        <NaviBar history={this.props.history} navi={naviBar} />
                      </div>
                    </div>}
                    <p className='project-title-mobile mb-10'>{siteTitle.substring(0, document.title.indexOf('-') - 1)}</p>
                    <Formik
                      key={currDtl.key}
                      initialValues={{
                        CurrencyId64: '',
                        FilterBy: filterList.filter(obj => { return obj.key === ((this.state.DtlFilter || {}).FilteredColumn || dtlFilter.FilteredColumn) })[0],
                        FilteredValue: dtlFilter.FilteredValue || '',
                      }}
                      onSubmit={this.FilterDtlList}
                      onReset={this.ResetFilterList}
                      render={({
                        values,
                        isSubmitting,
                        handleSubmit,
                        handleChange,
                        handleReset,
                        handleBlur,
                        handleClick,
                        setFieldValue,
                        resetForm
                      }) => (
                          <div>
                            <div className='account__head'>
                              <Row>
                                <Col xs={useMobileView ? 9 : 8}>
                                  <h3 className='account__title'>{DetailLstTitle}</h3>
                                  <h4 className='account__subhead subhead'>{DetailLstSubtitle}</h4>
                                </Col>
                                <Col xs={useMobileView ? 3 : 4}>
                                  <ButtonToolbar className='f-right'>
                                    <UncontrolledDropdown>
                                      <ButtonGroup className='btn-group--icons'>
                                        <Button className={`mw-50 ${filterBtnStyle}`} onClick={this.props.ChangeDtlListFilterVisibility} outline>
                                          <i className={`fa fa-filter icon-holder ${filterActive}`}></i> {dtlFilter.FilteredValue ? <i className='filter-applied'></i> : ''}
                                          {!useMobileView && <p className='action-menu-label'>{(screenButtons.Filter || {}).label}</p>}
                                        </Button>
                                        {
                                          dropdownMenuButtonList.filter(v => v.expose).map(v => {
                                            if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86)) return null;
                                            return (
                                              <Button
                                                key={v.tid}
                                                onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })}
                                                className='mw-50 add-report-expense'
                                                outline>
                                                <i className={`${v.iconClassName} icon-holder`}></i>
                                                {!useMobileView && <p className='action-menu-label'>{v.label}</p>}
                                              </Button>)
                                          })
                                        }
                                        {activeSelectionVisible &&
                                          dropdownMenuButtonList
                                            .filter(v => !v.expose)
                                            .filter(v => (!this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86,(currDtl || {}).ScreenFilterHlpId87)))
                                            .length > 0 &&
                                          <DropdownToggle className='mw-50' outline>
                                            <i className='fa fa-ellipsis-h icon-holder'></i>
                                            {!useMobileView && <p className='action-menu-label'>{(screenButtons.More || {}).label}</p>}
                                          </DropdownToggle>
                                        }
                                      </ButtonGroup>
                                      {activeSelectionVisible &&
                                        dropdownMenuButtonList
                                          .filter(v => !v.expose)
                                          .filter(v => (!this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86,(currDtl || {}).ScreenFilterHlpId87)))
                                          .length > 0 &&
                                        <DropdownMenu right className={`dropdown__menu dropdown-options`}>
                                          {

                                            dropdownMenuButtonList.filter(v => !v.expose).map(v => {
                                              if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86,(currDtl || {}).ScreenFilterHlpId87)) return null;
                                              return (
                                                <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>)
                                            })
                                          }
                                        </DropdownMenu>
                                      }
                                    </UncontrolledDropdown>
                                  </ButtonToolbar>
                                </Col>
                              </Row>
                            </div>
                            <Form className='form'>
                              <div className='form__form-group'>
                                <div className='form__form-group-narrow'>
                                  <div className='form__form-group-field'>
                                    <span className='radio-btn radio-btn--button btn--button-header h-20 no-pointer'>
                                      <span className='radio-btn__label color-blue fw-700 f-16'>{selectedMst.label || NoMasterMsg}</span>
                                      <span className='radio-btn__label__right color-blue fw-700 f-16'><span className='mr-5'>{(columnLabel.TrxTotal64 || {}).ColumnHeader}</span>
                                      {/* :{!isNaN(selectedMst.labelR) ? toLocalAmountFormat(selectedMst.labelR) : toLocalAmountFormat('0.00')} {selectedMst.detailR} */}
                                      </span>
                                    </span>
                                  </div>
                                  <div className={`form__form-group filter-padding items-filter-margin ${filterVisibility}`}>
                                    <Row className='mb-5'>
                                      <Col xs={12}>
                                        <label className='form__form-group-label filter-label'>{auxSystemLabels.QFilter}</label>
                                        <div className='form__form-group-field filter-form-border'>
                                          <DropdownField
                                            name='FilterBy'
                                            onBlur={handleBlur}
                                            onChange={this.FilteredColumnChange(handleSubmit, setFieldValue, 'FilterBy')}
                                            value={values.FilterBy}
                                            options={filterList}
                                            placeholder=''
                                          />
                                        </div>
                                      </Col>
                                    </Row>
                                    <Row>
                                      <Col xs={12}>
                                        <label className='form__form-group-label filter-label'>{auxSystemLabels.FilterSearchLabel}</label>
                                        <div className='form__form-group-field filter-form-border'>
                                          <Field
                                            type='text'
                                            name='FilteredValue'
                                            value={values.FilteredValue}
                                            // onChange={this.FilteredValueChange(handleReset, setFieldValue, 'FilteredValue')}
                                            onFocus={(e) => this.SearchBoxFocus(e)}
                                            onBlur={(e) => this.SearchBoxFocus(e)}
                                            className='FilteredValueClass'
                                          />
                                          <div className='rbt-aux custom-filter-clear' onClick={handleReset}>
                                            <button className='close rbt-close' type='button'>
                                              <span aria-hidden='true'>×</span>
                                            </button>
                                          </div>
                                          <span onClick={handleSubmit} className={`form__form-group-button desktop-filter-button search-btn-fix ${this.state.searchFocus ? ' active' : ''}`}><MagnifyIcon /></span>
                                        </div>
                                      </Col>
                                    </Row>
                                  </div>
                                  <h5 className='fill-fintrux h5-expense-items fw-700'><span className='color-dark mr-5'>{DetailFoundMsg}:</span> {itemList.length}</h5>
                                </div>
                                {
                                  itemList.slice(0, dtlFilter.Limit).map(
                                    (obj, i) => {
                                      const naviSelectBar = getNaviBar('DtlList', currMst, {}, auxLabels);
                                      return (
                                        <div className='form__form-group-narrow list-divider' key={obj.ScreenFilterHlpId87 || ('_' + i)}>
                                          <div className='form__form-group-field'>
                                            <label className='radio-btn radio-btn--button margin-narrow'>
                                              <Field
                                                key={obj.ScreenFilterHlpId87 || ('_' + i)}
                                                className='radio-btn__radio'
                                                name='expenseItem'
                                                type='radio'
                                                value={values.name}
                                                onClick={this.SelectDtlListRow(naviSelectBar, currMst, obj, i, 'ScreenFilterId86', 'ScreenFilterHlpId87')}
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
                                                <div className='row-cut'>{getListDisplayContet(obj, dTopL)}</div>
                                                <div className='row-cut row-bottom-report'>{getListDisplayContet(obj, dBottomL)}</div>
                                              </span>
                                              <span className={`radio-btn__label__right ${colorDark}`}>
                                                <div className='row-cut'>{getListDisplayContet(obj, dTopR)}</div>
                                                <div className='row-bottom-report f-right'>{getListDisplayContet(obj, dBottomR)}</div>
                                              </span>
                                              {
                                                rowMenuButtonList
                                                  .filter(v => v.expose && !useMobileView)
                                                  .map((v, i) => {
                                                    return (
                                                      <button type='button' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, mst: currMst, useMobileView, dtlId: obj.ScreenFilterHlpId87 })} className={`${v.exposedClassName}`}><i className={`${v.iconClassName}`}></i></button>
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
                                                        if (this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86,obj.ScreenFilterHlpId87)) return null;
                                                        return <DropdownItem key={v.tid} onClick={this.ScreenButtonAction[v.buttonType]({ naviBar: naviSelectBar, mst: currMst, useMobileView, dtlId: obj.ScreenFilterHlpId87 })} className={`${v.className}`}><i className={`${v.iconClassName} mr-10`}></i>{v.label}</DropdownItem>
                                                      })
                                                    }
                                                  </DropdownMenu>
                                                </UncontrolledDropdown>
                                              }
                                            </label>
                                          </div>
                                        </div>
                                      )
                                    }
                                  )
                                }
                              </div>
                              <Button onClick={this.props.ViewMoreDtl} className={`${viewMoreClass} btn btn-view-more-blue account__btn`} type='button'>{strFormat(IncrementMsg, this.props.AdmScreenFilter.DtlFilter.PageSize)}<br /><i className='fa fa-arrow-down'></i></Button>
                              <div className='width-wrapper'>
                                <div className='buttons-bottom-container'>
                                  <Row className='btn-bottom-row'>
                                    <Col xs={3} sm={2} className='btn-bottom-column'>
                                      <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className='fa fa-long-arrow-left'></i></Button>
                                    </Col>
                                    {useMobileView && <Col xs={9} sm={10}>
                                      <Row>
                                        {
                                          bottomButtonList
                                            .filter(v => v.expose)
                                            .map((v, i, a) => {
                                              if ((!activeSelectionVisible && v.buttonType !== 'InsRow') || this.ActionSuppressed(authRow, v.buttonType, (currMst || {}).ScreenFilterId86)) return null;
                                              const buttonCount = a.length;
                                              const colWidth = parseInt(12 / buttonCount, 10);
                                              const lastBtn = i === a.length - 1;
                                              const outlineProperty = lastBtn ? false : true;

                                              return (
                                                <Col xs={colWidth} sm={colWidth} key={v.tid} className='btn-bottom-column' >
                                                  <Button color='success' type='button' outline={outlineProperty} className='account__btn' onClick={this.ScreenButtonAction[v.buttonType]({ naviBar, mst: currMst, dtl: currDtl, useMobileView })}>{v.labelLong}</Button>
                                                </Col>
                                              )
                                            })
                                        }
                                      </Row>
                                    </Col>}
                                  </Row>
                                </div>
                              </div>
                            </Form>
                          </div>
                        )}
                    />
                  </div>
                </div>
              </div>
            </Col>
            {!useMobileView && <Col xs={6}>

              {!activeSelectionVisible &&
                targetDtlId !== '_' &&
                !this.state.ShowDtl &&
                <div className='empty-block'>
                  <img className='folder-img' alt='' src={require('../../img/folder.png')} />
                  <p className='create-new-message'>{NoDetailMsg}. <span className='link-imitation' onClick={this.AddNewDtl({ naviBar, mstId: currMst.ScreenFilterId86 })}>{AddDetailMsg}</span></p>
                </div>}

              {(activeSelectionVisible || this.state.ShowDtl || (targetDtlId ==='_')) && <DtlRecord OnCopy={this.OnDtlCopy} updateChangedState={this.SetCurrentRecordState} suppressLoadPage={true} />}

            </Col>}
          </Row>
        </div>
      </DocumentTitle>
    );
  };
};

const mapStateToProps = (state) => ({
  user: state.user,
  error: state.error,
  AdmScreenFilter: state.AdmScreenFilter,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: AdmScreenFilterReduxObj.LoadPage.bind(AdmScreenFilterReduxObj) },
    { SelectDtl: AdmScreenFilterReduxObj.SelectDtl.bind(AdmScreenFilterReduxObj) },
    { SavePage: AdmScreenFilterReduxObj.SavePage.bind(AdmScreenFilterReduxObj) },
    { AddDtl: AdmScreenFilterReduxObj.AddDtl.bind(AdmScreenFilterReduxObj) },
    { ChangeDtlListFilterVisibility: AdmScreenFilterReduxObj.ChangeDtlListFilterVisibility.bind(AdmScreenFilterReduxObj) },
    { ChangeDtlListFilter: AdmScreenFilterReduxObj.ChangeDtlListFilter.bind(AdmScreenFilterReduxObj) },
    { ViewMoreDtl: AdmScreenFilterReduxObj.ViewMoreDtl.bind(AdmScreenFilterReduxObj) },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(DtlList);

            