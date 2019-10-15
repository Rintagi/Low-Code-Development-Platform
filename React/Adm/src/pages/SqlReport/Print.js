import React, { Component } from 'react';
import { bindActionCreators, compose } from 'redux';
import { connect } from 'react-redux';
import { Formik, Field, Form } from 'formik';
import { Button, Row, Col, ButtonToolbar, ButtonGroup, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown} from 'reactstrap';
import LoadingIcon from 'mdi-react/LoadingIcon';
import classNames from 'classnames';
import DropdownField from '../../components/custom/DropdownField';
import AutoCompleteField from '../../components/custom/AutoCompleteField';
import CheckIcon from 'mdi-react/CheckIcon';
import { downloadFile } from '../../helpers/downloadFile';
import { GetReport } from '../../services/SqlReportService';
import { LoadPage,ShowSpinner,changeReportFilterVisibility } from '../../redux/SqlReport';
import { showNotification } from '../../redux/Notification';
import log from '../../helpers/logger';

class Print extends Component {
  constructor(props) {
    super(props);

    this.state = {
      pdfbase64: '',
      runningReport: false
    }

    this.DoSqlReport = this.DoSqlReport.bind(this)
    this.ReportCriteriaChange = this.ReportCriteriaChange.bind(this);
    this.ViewReport = this.ViewReport.bind(this);
  }

  ReportCriteriaChange(handleSubmit, setFieldValue, type, name) {
    const _this = this;
    return function (name, value) {
      if (type === 'autocomplete') {
        setFieldValue(name, value[0]);
      }
      else {
        setFieldValue(name, value);
      }

      //handleSubmit();
    }
  }

  BTrxIdInputChange() {
    const _this = this;
    return function (name, v) {
      //_this.props.SearchMemberId64(v);
    }
  }

  DoSqlReport(values, fmt) {
    const _this = this;

    _this.setState({ runningReport: true });

    GetReport('66', {
      CompanyId10: values.CompanyId.value,
      BTrxId: values.BTrxId.value,
      Summary: values.Summary ? 'Y' : 'N'
    }, fmt).then(
      (result => {
        //_this.setState({ pdfbase64: 'data:application/pdf;base64,' + result.data });
        var byteCharacters = atob(result.data);

        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);

        if (fmt === 'pdf') {
          downloadFile(byteArray, 'test.pdf', 'application/pdf');
        }
        else if (fmt === 'xls') {
          downloadFile(byteArray, 'test.xls', 'application/vnd.ms-excel');
        }
        else if (fmt === 'word') {
          downloadFile(byteArray, 'test.doc', 'application/msword');
        }

        _this.setState({ runningReport: false });
      })
      , (error => {
        _this.setState({ runningReport: false });
        _this.props.showNotification("E", { message: error.errMsg });
      })
    );
  }

  ViewReport(values, { setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    log.debug('form submit', values);

    this.DoSqlReport({
      CompanyId10: values.CompanyId.value,
      BTrxId: values.BTrxId.value,
      Summary: values.Summary ? 'Y' : 'N'
    }, 'pdf');

  }

  componentDidMount() {
    if (!(this.props.SqlReport || {}).AuthCol || true) {
      this.props.LoadPage('66');
    }
  }

  componentDidUpdate(prevprops, prevstates) {
  }

  render() {
    const rptCriDdl = ((this.props.SqlReport || {}).ReportCriDdl || {});
    const rptCri = ((this.props.SqlReport || {}).ReportCriteria || {});

    const selectedCompanyId = (rptCriDdl.CompanyId10 || []).filter(obj => { return obj.key === rptCri.CompanyId10.LastCriteria })
    const selectedBTrxId = (rptCriDdl.BTrxId || []).filter(obj => { return obj.key === rptCri.BTrxId.LastCriteria });

    let filterVisibility = classNames({ 'd-none': true, 'd-block': rptCri.ShowFilter });
    let filterBtnStyle = classNames({ 'filter-button-clicked': rptCri.ShowFilter });
    let filterActive = classNames({ 'filter-icon-active': rptCri.ShowFilter });

    return (
      <div>
        <div className='account'>
          <div className='account__wrapper'>
            <div className='account__card shadow-box rad-4'>
              { (this.state.runningReport || ShowSpinner(this.props.SqlReport)) && <div className='panel__refresh'><LoadingIcon /></div>}
              <div className='account__head'>
                <Row>
                  <Col xs={9}>
                    <h3 className='account__title'>{this.props.SqlReport.ReportHlp.ReportTitle}</h3>
                    <h4 className='account__subhead subhead'>{this.props.SqlReport.ReportHlp.DefaultHlpMsg}</h4>
                  </Col>
                  <Col xs={3}>
                    <ButtonToolbar className="f-right">
                      <UncontrolledDropdown>
                        <ButtonGroup className='btn-group--icons'>
                          <Button className={`${filterBtnStyle}`} onClick={()=>this.props.changeReportFilterVisibility()} outline>
                            <i className={`fa fa-filter icon-holder ${filterActive}`}></i>
                            {/* <i className="filter-applied"></i> */}
                          </Button>
                          <DropdownToggle outline>
                            <i className="fa fa-ellipsis-h icon-holder"></i>
                          </DropdownToggle>
                        </ButtonGroup>
                        <DropdownMenu right className='dropdown__menu dropdown-options'>
                          <DropdownItem className="dropdown-button"><i className="fa fa-question-circle mr-10"></i> {this.props.SqlReport.Label.HelpButton}</DropdownItem>
                        </DropdownMenu>
                      </UncontrolledDropdown>
                    </ButtonToolbar>
                  </Col>
                </Row>
              </div>
              <Formik
                initialValues={{
                  Summary: rptCri.Summary.LastCriteria === 'Y',
                  CompanyId: selectedCompanyId[0],
                  BTrxId: selectedBTrxId[0],
                }}
                key={this.props.SqlReport.key}
                onSubmit={this.ViewReport}
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
                        <div className={`form__form-group filter-padding ${filterVisibility}`}>
                          <Row className='mb-5'>
                            <Col xs={12} md={12}>
                              <label className='form__form-group-label filter-label'>{this.props.SqlReport.ReportCriteria.CompanyId10.ColumnHeader}</label>
                              <div className='form__form-group-field filter-form-border'>
                                <DropdownField
                                  name='CompanyId'
                                  onBlur={setFieldTouched}
                                  onChange={this.ReportCriteriaChange(handleSubmit, setFieldValue, 'ddl', "CompanyId")}
                                  value={values.CompanyId}
                                  //options={this.props.SqlReport.ReportCriDdl.CompanyId10}
                                  options={rptCriDdl.CompanyId10}
                                  placeholder=''
                                />
                              </div>
                            </Col>
                          </Row>
                          <Row className='mb-5'>
                            <Col xs={12} md={12}>
                              <label className='form__form-group-label filter-label'>{this.props.SqlReport.ReportCriteria.BTrxId.ColumnHeader}</label>
                              <div className='form__form-group-field filter-form-border'>
                                <AutoCompleteField
                                  name='BTrxId'
                                  onChange={this.ReportCriteriaChange(handleSubmit, setFieldValue, 'autocomplete', "BTrxId")}
                                  onBlur={this.ReportCriteriaChange(handleSubmit, setFieldTouched, 'autocomplete', "BTrxId")}
                                  onInputChange={this.BTrxIdInputChange()}
                                  value={values.BTrxId}
                                  defaultSelected={selectedBTrxId}
                                  options={this.props.SqlReport.ReportCriDdl.BTrxId}
                                />
                              </div>
                            </Col>
                          </Row>
                          <Row>
                            <Col xs={12} md={12}>
                              <label className='checkbox-btn checkbox-btn--colored-click mr-20 filter-label'>
                                <Field
                                  className='checkbox-btn__checkbox'
                                  type='checkbox'
                                  name='Summary'
                                  onChange={handleChange}
                                  value={values.Summary}
                                  checked={values.Summary}
                                // defaultChecked={true}
                                />
                                <span className='checkbox-btn__checkbox-custom filter-checkbox'><CheckIcon className="filter-checkbox-svg" /></span>
                                <span className='checkbox-btn__label filter-checkbox-label'>{this.props.SqlReport.ReportCriteria.Summary.ColumnHeader}</span>
                              </label>
                            </Col>
                          </Row>
                        </div>
                        <div className='form__form-group mb-0'>
                          <Row className="btn-bottom-row">
                            <Col xs={4} md={4} className="btn-bottom-column">
                              <Button color='success' className='btn btn-success account__btn' type="button" onClick={() => this.DoSqlReport(values, 'pdf')}><i className="fa fa-file-pdf-o mr-5"></i> {this.props.SqlReport.Label.PdfButton}</Button>
                            </Col>
                            <Col xs={4} md={4} className="btn-bottom-column">
                              <Button color='success' className='btn btn-success account__btn' type="button" onClick={() => this.DoSqlReport(values, 'xls')}><i className="fa fa-file-excel-o mr-5"></i> {this.props.SqlReport.Label.ExcelButton}</Button>
                            </Col>
                            <Col xs={4} md={4} className="btn-bottom-column">
                              <Button color='success' className='btn btn-success account__btn' type="button" onClick={() => this.DoSqlReport(values, 'word')}><i className="fa fa-file-word-o mr-5"></i> {this.props.SqlReport.Label.WordButton}</Button>
                            </Col>
                          </Row>
                        </div>
                        {/* <iframe src={this.state.pdfbase64} height="100%" width="100%"></iframe> */}
                      </Form>
                    </div>
                  )}
              />
            </div>
          </div>
        </div>
      </div >
    );
  };
};

const mapStateToProps = (state) => ({
  user: state.user,
  error: state.error,
  SqlReport: state.SqlReport,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { LoadPage: LoadPage },
    { showNotification: showNotification },
    { changeReportFilterVisibility: changeReportFilterVisibility },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(Print);
