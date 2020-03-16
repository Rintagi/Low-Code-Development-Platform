import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, resetPasswordEmail, ShowSpinner } from '../../redux/Auth';
import { Row, Col } from 'reactstrap';
import { Link } from 'react-router-dom';
import log from '../../helpers/logger';

class ForgetPassword extends Component {
  constructor(props) {
    super(props);
    this.state = {
      submitting: false,
    };

    this.resetPasswordEmail = this.resetPasswordEmail.bind(this);
  }


  // componentDidMount() {
  //   if (!this.props.user || !this.props.user.UsrId) {
  //     this.props.getCurrentUser();
  //   }
  //   // console.log(this.props.user);
  // }
  // componentDidUpdate(prevProps, prevStats) {

  //   const from = ((this.props.location || {}).state || {}).from;
  //   if ((!prevProps.user || !prevProps.user.UsrId) && this.props.user && this.props.user.UsrId) {
  //     //window.location.reload();
  //   }

  //   if (this.state.submitting &&
  //     (((this.props.user || {}).status === "failed") ||
  //       ((this.props.user || {}).errMsg === "Failed to fetch")
  //     )
  //   ) {
  //     this.state.setSubmitting(false);
  //   }
  // }

  static getDerivedStateFromProps(nextProps, prevState) {
    if (prevState.submitting) {
      prevState.setSubmitting(false);
    }
    return null;
  }


  resetPasswordEmail(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    this.setState({ submitting: true, setSubmitting: setSubmitting });
    console.log(values);

    this.props.resetPasswordEmail(
      {
        resetLoginName: values.cResetLoginName,
        resetUsrEmail: values.cResetUsrEmail,
      }
    )
  }

  render() {
    if (this.props.user && this.props.user.UsrId) {
      if (((this.props.location.state || {}).from || {}).pathname) {
        return <Redirect to={((this.props.location.state || {}).from || {}).pathname} />
      }
      else {
        return <Redirect to='/' />
      }
    }

    return (
      <div>
        <div className='account'>
          <div className='account__wrapper'>
            <div className='account__card shadow-box rad-4'>
              {this.props.user.loading && <div className='panel__refresh'><LoadingIcon /></div>}
              {ShowSpinner(this.props.auth) && <div className='panel__refresh'><LoadingIcon /></div>}
              <div className='account__head'>
                <h3 className='account__title'>{this.props.auth.Label.ResetPwdTitle}</h3>
                <h4 className='account__subhead subhead'>{this.props.auth.Label.ResetPwdSubtitle}</h4>
                {/* <h4>{(this.props.user || {}).UsrId}</h4> */}
              </div>
              <Formik
                initialValues={{
                  cResetLoginName: '',
                  cResetUsrEmail: '',
                  // password: '',
                }}
                validate={values => {
                  // // same as above, but feel free to move this into a class method now.
                  // let errors = {};
                  // if (!values.username) {
                  //   errors.username = this.props.auth.Label.UserNameEmpty;
                  // }
                  // // if (!values.email) {
                  // //   errors.email = 'This field is required';
                  // // } else
                  // if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
                  //   errors.email = 'Invalid email address';
                  // }
                  // return errors;
                }}
                onSubmit={this.resetPasswordEmail}
                render={({
                  errors,
                  touched,
                  isSubmitting,
                }) => (
                    <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}
                      <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.ResetLoginName}</label>
                        <div className='form__form-group-field'>
                          <Field
                            type="text"
                            name="cResetLoginName"
                          />
                        </div>
                        {/* {errors.username && touched.username && <span className='form__form-group-error'>{errors.username}</span>} */}
                      </div>
                      {/* <div className='form__form-group'>
                        <label className='form__form-group-label'>or</label>
                      </div> */}
                      <div className='form__or'>
                        <p className="fill-fintrux">{this.props.auth.Label.Or}</p>
                      </div>
                      <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.ResetUsrEmail}</label>
                        <div className='form__form-group-field'>
                          <Field
                            type="email"
                            name="cResetUsrEmail"
                          />
                        </div>
                        {/* {errors.email && touched.email && <span className='form__form-group-error'>{errors.email}</span>} */}
                      </div>
                      {/* <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>Reset</Button> */}
                      <div className="form__form-group mb-0">
                        <Row className="btn-bottom-row">
                          <Col xs={3} sm={2} className="btn-bottom-column">
                            <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className="fa fa-long-arrow-left"></i></Button>
                          </Col>
                          <Col xs={9} sm={10} className="btn-bottom-column">
                            <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>{this.props.auth.Label.ResetPwdBtn}</Button>
                          </Col>
                        </Row>
                      </div>
                    </Form>
                  )}
              />
            </div>
          </div>
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
    {resetPasswordEmail: resetPasswordEmail}
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ForgetPassword));
