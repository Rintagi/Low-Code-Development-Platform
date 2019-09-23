import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter, Link } from 'react-router-dom';
import { Prompt } from 'react-router';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, switchCurrent, ShowSpinner } from '../../redux/Auth';
import { showNotification } from '../../redux/Notification';
import { Row, Col, ButtonToolbar, ButtonGroup, UncontrolledDropdown } from 'reactstrap';
import DropdownField from '../../components/custom/DropdownField';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import log from '../../helpers/logger';
import * as systemService from '../../services/systemService';
import NaviBar from '../../components/custom/NaviBar';
import { getNaviBar } from './index';
import {switchLanguage} from '../../helpers/formatter';
import DocumentTitle from 'react-document-title';
import { setTitle } from '../../redux/Global';

class Setting extends Component {
  constructor(props) {
    super(props);
    this.titleSet = false;
    this.SystemName = "FintruX";
    this.state = {
      submitting: false,
      companyList: [],
      projectList: [],
      timeZoneList: [],
      languageList: [],
      key: Date.now(),
      loading: false
    };

    this.saveProfile = this.saveProfile.bind(this);
    this.CompanyValueChange = this.CompanyValueChange.bind(this);
  }

  componentDidMount() {
    if (this.props.user) {

      const promises = [
        systemService.getCompanyList(),
        systemService.getProjectList(this.props.user.CompanyId),
        systemService.getTimeZoneList(),
        systemService.getCultureList("1","en"),
      ]
      this.setState({ loading: true });

      Promise.all(promises)
      .then(
        ([CompanyList, ProjectList, TimeZoneList,CultureList]) => {
          const payload = {
              CompanyList: CompanyList.data.data,
              ProjectList: ProjectList.data.data,
              TimeZoneList: TimeZoneList.data.data,
              CultureList: (CultureList.data.data || []).map(v=>{ 
                return {
                  key: v.CultureTypeId,
                  value: v.CultureTypeId,
                  label: v.CultureTypeLabel,
                  lang: v.CultureTypeName,
                }
              }
              ),
          }

          this.setState({
            companyList: payload.CompanyList,
            projectList: payload.ProjectList,
            timeZoneList: payload.TimeZoneList,
            cultureList: payload.CultureList,
            loading: false,
            key: Date.now()
          });

            return payload;
          }, (error) => {
            this.setState({ loading: false });
            this.props.showNotification("E", { message: error.errMsg });

          });
    }
  }
  componentDidUpdate(prevprops, prevstates) {
    const emptyTitle = '';
    const siteTitle = this.SystemName;

    if (!this.titleSet) {
      this.props.setTitle(siteTitle);
      this.titleSet = true;
    }
  }
  CompanyValueChange(setFieldValue, setFieldTouched, controlName) {
    const _this = this;
    return function (name, value) {
      _this.setState({ loading: true });
      setFieldValue(name, value);
      setFieldTouched(name, true);

      systemService.getProjectList(value.value).then(
        (resp) => {
          _this.setState({
            projectList: resp.data.data,
            loading: false
          });

          setFieldValue('ProjectList', '');
        },
        (error) => {
          console.log(error);
          _this.setState({ loading: false });
          _this.props.showNotification("E", { message: error.errMsg });

        }
      )
    }
  }

  static getDerivedStateFromProps(nextProps, prevState) {
    if (prevState.submitting) {
      prevState.setSubmitting(false);
    }

    if (prevState.key && prevState.key < nextProps.user.key) {
      return {
        key:nextProps.user.key,
      }
    }
    return null;
  }

  saveProfile(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    this.setState({ submitting: true, setSubmitting: setSubmitting });



    const companyId = ((values.CompanyList || {}).value || 0);
    const projectId = ((values.ProjectList || {}).value || 0);
    const culture = ((values.LanguageList[0]) || {key:"1",value:"1",lang:"en"}) || {key:"1",value:"1",lang:"en"};
    this.props.switchCurrent(companyId, projectId, culture);
  }

  render() {

    const naviBar = getNaviBar("Setting", this.props.auth.Label);

    const CompanyList = this.state.companyList;
    const ProjectList = this.state.projectList;
    //const SystemsList = this.props.auth.filter.SystemsList;
    const TimeZoneList = this.state.timeZoneList;
    const LanguageList = this.state.cultureList || [];

    const luser = ((this.props.auth || {}).user || {});

    const selCompany = (CompanyList || []).filter(obj => { return obj.key === luser.CompanyId })
    const selProject = (ProjectList || []).filter(obj => { return obj.key === luser.ProjectId });
    const selTimeZone = (TimeZoneList || []).filter(obj => { return obj.label === luser.TimeZone });
    const selLanguage = (LanguageList || []).filter(obj => { return obj.key === (luser.CultureId || "1") });

    const siteTitle = (this.props.global || {}).pageTitle || '';
    const emptyTitle = '';
    return (
      <DocumentTitle title={siteTitle}>
      <div>
        <div className='account'>
          <div className='account__wrapper'>
            <div className='account__card shadow-box rad-4'>
              {/* {(this.state.loading || this.props.user.loading || ShowSpinner(this.props.auth)) && <div className='panel__refresh'><LoadingIcon /></div>} */}
              <div className='tabs tabs--justify tabs--bordered-bottom mb-30'>
                <div className='tabs__wrap'>
                  <NaviBar history={this.props.history} navi={naviBar} />
                </div>
              </div>
              <p className="project-title-mobile mb-10">{emptyTitle}</p>
              <Formik
                initialValues={{
                  CompanyList: selCompany[0],
                  ProjectList: selProject[0],
                  // SystemsList: '',
                  TimeZoneList: selTimeZone[0],
                  LanguageList: selLanguage,
                }}
                validate={values => {
                }}
                key={this.state.key}
                onSubmit={this.saveProfile}
                render={({
                  errors,
                  touched,
                  isSubmitting,
                  dirty,
                  setFieldTouched,
                  setFieldValue,
                  values
                }) => (
                    <div>
                      <Prompt
                        when={dirty}
                        message={this.props.auth.Label.UnSavedPage}
                      />
                      <div className='account__head'>
                        <Row>
                          <Col xs={9}>
                            <h3 className='account__title'>{this.props.auth.Label.Settings}</h3>
                            <h4 className='account__subhead subhead'>{this.props.auth.Label.SettingsSubtitle}</h4>
                          </Col>
                          <Col xs={3}>
                            <ButtonToolbar className="f-right">
                              <UncontrolledDropdown>
                                <ButtonGroup className='btn-group--icons'>
                                  <i className={dirty ? "fa fa-exclamation exclamation-icon" : ""}></i>
                                </ButtonGroup>
                              </UncontrolledDropdown>
                            </ButtonToolbar>
                          </Col>
                        </Row>
                      </div>
                      <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}
                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.CompanyList}</label>
                          <div className='form__form-group-field'>
                            <DropdownField
                              name='CompanyList'
                              onBlur={setFieldTouched}
                              onChange={this.CompanyValueChange(setFieldValue, setFieldTouched, 'CompanyList')}
                              value={values.CompanyList}
                              options={CompanyList}
                              placeholder=''
                            />
                          </div>
                        </div>

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.ProjectList}</label>
                          <div className='form__form-group-field'>
                            <DropdownField
                              name='ProjectList'
                              onBlur={setFieldTouched}
                              onChange={setFieldValue}
                              value={values.ProjectList}
                              options={ProjectList}
                              placeholder=''
                            />
                          </div>
                        </div>

                        {/* <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.SystemsList}</label>
                        <div className='form__form-group-field'>
                        <DropdownField
                              name='SystemsList'
                              onBlur={setFieldTouched}
                              onChange={setFieldValue}
                              value={values.SystemsList}
                              options={SystemsList}
                              placeholder=''
                            />
                        </div>
                      </div> */}

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.TimeZoneList}</label>
                          <div className='form__form-group-field'>
                            <DropdownField
                              name='TimeZoneList'
                              onBlur={setFieldTouched}
                              onChange={setFieldValue}
                              value={values.TimeZoneList}
                              options={TimeZoneList}
                              placeholder=''
                            />
                          </div>
                        </div>

                       <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.Language}</label>
                        <div className='form__form-group-field'>
                        <AutoCompleteField
                              name='LanguageList'
                              onChange={setFieldValue}
                              onBlur={setFieldTouched}
                              //onInputChange={this.BGlChartId65InputChange()}
                              value={values.LanguageList}
                              defaultSelected={LanguageList.filter(obj => { return obj.key === (values.LanguageList[0] || []).value })}
                              options={LanguageList}
                            />
                        </div>
                      </div>

                      <div className="form__form-group mb-0">
                        <Row className="btn-bottom-row">
                          <Col xs={3} sm={2} className="btn-bottom-column">
                            <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className="fa fa-long-arrow-left"></i></Button>
                          </Col>
                          <Col xs={9} sm={10} className="btn-bottom-column">
                            <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>{this.props.auth.Label.ApplySettingsBtn}</Button>
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
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
    { switchCurrent: switchCurrent },
    { showNotification: showNotification },
    { setTitle: setTitle },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Setting));

