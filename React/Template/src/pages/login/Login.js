import React, { Component } from 'react';
import EyeIcon from 'mdi-react/EyeIcon';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser } from '../../redux/Auth';
import { Link } from 'react-router-dom';
import log from '../../helpers/logger';
import { setSpinner } from '../../redux/Global';

class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showPassword: false,
      submitting: false,
      userId: this.props.user.UsrId,
    };

    this.showPassword = this.showPassword.bind(this);
    this.handleSubmit = ((values, { setSubmitting, setErrors /* setValues and other goodies */ }) => {
      this.setState({ submitting: true, setSubmitting: setSubmitting });
      // this.state.submitting = true;
      // this.state.setSubmitting = setSubmitting;
      this.props.login(values.username, values.password);
    });

    this.props.setSpinner(true);

    if (!this.props.user || !this.props.user.UsrId) {
      this.props.getCurrentUser();
    }
    
  }


  componentDidMount() {
    // if (!this.props.user || !this.props.user.UsrId) {
    //   this.props.getCurrentUser();
    // }
    // console.log(this.props.user);
  }
  componentDidUpdate(prevProps, prevStats) {

    const from = ((this.props.location || {}).state || {}).from;
    if ((!prevProps.user || !prevProps.user.UsrId) && this.props.user && this.props.user.UsrId) {
      //window.location.reload();
    }

    if (this.props.global.pageSpinner && !this.state.userId && !this.props.user.loading ) {
      this.props.setSpinner(false);
    }

    if (this.state.submitting &&
      (((this.props.user || {}).status === "failed") ||
        ((this.props.user || {}).errMsg === "Failed to fetch" || (this.props.user || {}).errType === "network error" || (this.props.user || {}).loading === false)
      )
    ) {
      this.state.setSubmitting(false);
    }
  }

  showPassword(e) {
    e.preventDefault();
    this.setState({
      showPassword: !this.state.showPassword
    })
  }

  render() {
    if (this.props.user && this.props.user.UsrId) {
      if (((this.props.location.state || {}).from || {}).pathname) {
        return <Redirect to={((this.props.location.state || {}).from || {}).pathname + ((this.props.location.state || {}).from || {}).search} />
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
              {/* {this.props.user.loading && <div className='panel__refresh'><LoadingIcon /></div>} */}
              <div className='account__head'>
                <h3 className='account__title'>{this.props.auth.Label.LoginScreenTitle}</h3>
                <h4 className='account__subhead subhead'>{this.props.auth.Label.LoginScreenDesc}</h4>
                <h4>{(this.props.user || {}).UsrId}</h4>
              </div>
              <Formik
                initialValues={{
                  username: '',
                  password: '',
                }}
                validate={values => {
                  // same as above, but feel free to move this into a class method now.
                  let errors = {};
                  if (!values.username) {
                    errors.username = this.props.auth.Label.UserNameEmpty;
                  }
                  if (!values.password) {
                    errors.password = this.props.auth.Label.PasswordEmpty;
                  }
                  return errors;
                }}
                onSubmit={this.handleSubmit}
                render={({
                  errors,
                  touched,
                  isSubmitting,
                }) => (
                    <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}
                      <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.UserName} <span className="text-danger">*</span></label>
                        <div className='form__form-group-field'>
                          <Field
                            type="text"
                            name="username"
                          />
                        </div>
                        {errors.username && touched.username && <span className='form__form-group-error'>{errors.username}</span>}
                      </div>
                      <div className='form__form-group'>
                        <label className='form__form-group-label'>{this.props.auth.Label.Password} <span className="text-danger">*</span></label>
                        <div className='form__form-group-field'>
                          <Field
                            type={this.state.showPassword ? 'text' : 'password'}
                            name="password"
                          />
                          <button className={`form__form-group-button${this.state.showPassword ? ' active' : ''}`}
                            onClick={(e) => this.showPassword(e)} type="button"><EyeIcon /></button>
                        </div>
                        {errors.password && touched.password && <span className='form__form-group-error'>{errors.password}</span>}
                      </div>
                      <div className='form__form-group'>
                        <Link className="fill-fintrux" to="/forget-password">Forget Password?</Link>
                      </div>
                      <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>{this.props.auth.Label.Login}</Button>
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
  global: state.global,
  menu: (state.auth || {}).menu,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Login));
