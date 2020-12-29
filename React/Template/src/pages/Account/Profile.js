import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, saveProfile, ShowSpinner } from '../../redux/Auth';
import { Row, Col } from 'reactstrap';
import NaviBar from '../../components/custom/NaviBar';
import { getNaviBar } from './index';
import { Link } from 'react-router-dom';
import log from '../../helpers/logger';
import { getProfileInfo, updateNotificationChannel } from '../../services/profileService';
import DocumentTitle from 'react-document-title';
import { setTitle } from '../../redux/Global';
import { showNotification } from '../../redux/Notification';
import { messaging, vapidKey, fcmSWScope, fcmSWUrl } from '../../app/FirebaseInit';
import { getMyAppSig, getMyMachine, getFingerPrint } from '../../helpers/config';
import * as fcmServiceWorker from '../../registerFCMServiceWorker';

let appSig = getMyAppSig();
let myMachine = getMyMachine();

function requestPermission() {
  console.log('Requesting permission...');
  Notification.requestPermission().then((permission) => {
    if (permission === 'granted') {
      console.log('Notification permission granted.');
      // TODO(developer): Retrieve a registration token for use with FCM.

    } else {
      console.log('Unable to get permission to notify.');
    }
  });
}

class Profile extends Component {
  constructor(props) {
    super(props);
    this.titleSet = false;
    this.SystemName = (document.Rintagi || {}).systemName || 'Rintagi';
    this.state = {
      submitting: false,
      loginName: '',
      userName: '',
      userEmail: '',
      fcmToken: '',
      key: Date.now()
    };

    this.saveProfile = this.saveProfile.bind(this);
    this.handleFocus = this.handleFocus.bind(this);
    this.deleteFCMToken = this.deleteFCMToken.bind(this);
    this.enableFCMNotification = this.enableFCMNotification.bind(this);
  }

  componentDidMount() {
    if (this.props.user) {
      getProfileInfo().then(
        (resp) => {
          this.setState({
            loginName: resp.data.LoginName,
            userName: resp.data.UsrName,
            userEmail: resp.data.UsrEmail,
            key: Date.now()
          });
        },
        (error) => {
          console.log(error);
        }
      )
    }

    if (window.Notification && navigator.serviceWorker && messaging) {
      if (Notification.permission === "granted") {
        fcmServiceWorker.register({scope:fcmSWScope, serviceWorkJS:fcmSWUrl})
          //Promise.resolve('abcd')
          .then((registration) => {
            log.debug(registration);
            registration && messaging.getToken(
              {
                vapidKey: vapidKey,
                serviceWorkerRegistration: registration
              }
            ).then((currentToken) => {
              this.setState({
                fcmToken: currentToken
              });
              getFingerPrint().then((myMachine)=>{
                  updateNotificationChannel(currentToken, myMachine, appSig, 'fcm')
                  .then(result => {
                    log.debug(result);
                  })
                  .catch(err => {
                    log.debug(err);
                  })
              })
            })
          }).catch((err) => {
            console.log('Error retrieving registration token. ', err);
          });
      }
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

  static getDerivedStateFromProps(nextProps, prevState) {
    if (prevState.submitting) {
      prevState.setSubmitting(false);
    }
    return null;
  }

  saveProfile(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    this.setState({ submitting: true, setSubmitting: setSubmitting });
    // this.state.submitting = true;
    // this.state.setSubmitting = setSubmitting;
    // this.state.resetForm  = resetForm;
    console.log(values);

    this.props.saveProfile(
      {
        newLoginName: values.cNewLoginName,
        newUserName: values.cNewUserName,
        newUserEmail: values.cNewUserEmail
      }
    )
  }

  handleFocus = (event) => {
    log.debug(event);
    // event.target.select();
    event.target.setSelectionRange(0, event.target.value.length);
  }

  deleteFCMToken(evt) {
    navigator.serviceWorker && window.Notification && messaging &&     
    fcmServiceWorker.register({scope:fcmSWScope, serviceWorkJS:fcmSWUrl})
      //Promise.resolve('abcd')
      .then((registration) => {
        log.debug(registration);
        registration && messaging.getToken(
          {
            vapidKey: vapidKey,
            serviceWorkerRegistration: registration
          }
        ).then((currentToken) => {
          messaging.deleteToken(currentToken).then(() => {
            console.log(`Token deleted. ${currentToken}`);
          }).catch((err) => {
            console.log('Unable to delete token. ', err);
          });
        }
        )
      }).catch((err) => {
        console.log('Error retrieving registration token. ', err);
      });
  }

  enableFCMNotification(evt) {
    navigator.serviceWorker && window.Notification && messaging &&
    fcmServiceWorker.register({scope:fcmSWScope, serviceWorkJS:fcmSWUrl})
      //Promise.resolve('abcd')
      .then((registration) => {
        log.debug(registration);
        const usrId = this.props.user.UsrId;
        if (registration) {
          // pass in our info(would be done on app loading generally but on-demand enabling skipped that, see LoginInfo.js)
          (registration.active || registration.waiting || registration.installing).postMessage({
            type: 'SET_LOGIN_USER',
            usrId: usrId,
            basePath: window.location.pathname + '#',
          });

          Notification.requestPermission()
            .then((permission) => {
              if (permission === 'granted') {
                console.log('Notification permission granted.');
                // TODO(developer): Retrieve a registration token for use with FCM.
                messaging.getToken(
                  {
                    vapidKey: vapidKey,
                    serviceWorkerRegistration: registration
                  }
                ).then((currentToken) => {
                  //weird firefox issue getToken needs to be called again to get the correct token
                  messaging.getToken(
                    {
                      vapidKey: vapidKey,
                      serviceWorkerRegistration: registration
                    }
                  ).then((currentToken) => {
                    this.setState({
                      fcmToken: currentToken
                    });
                    getFingerPrint().then((myMachine)=>{
                      updateNotificationChannel(currentToken, myMachine, appSig, 'fcm')
                      .then(result => {
                        log.debug(result);
                      })
                      .catch(err => {
                        log.debug(err);
                      })
                    });
                  });                  
                })
              } else {
                console.log('Unable to get permission to notify.');
                this.props.showNotification("I", { message: 'please consider allow notification to have enhanced user experience' });
              }
            })
            .catch(err => {
              console.log('failed to get notification permission');
            })
            ;
          if (Notification.permission !== "granted") {
            this.props.showNotification("I", { message: 'please consider allow notification to have enhanced user experience' });
          }
        }
      })
      .catch(err => {
        console.log('Error retrieving registration token. ', err);
      });
  }

  render() {

    const naviBar = getNaviBar("Profile", this.props.auth.Label);
    const usrName = ((this.props.auth || {}).user || {}).UsrName;
    const siteTitle = (this.props.global || {}).pageTitle || '';
    log.debug(siteTitle);
    const emptyTitle = '';
    return (
      <DocumentTitle title={siteTitle}>
        <div>
          <div className='account'>
            <div className='account__wrapper'>
              <div className='account__card shadow-box rad-4'>
                {this.props.user.loading && <div className='panel__refresh'><LoadingIcon /></div>}
                {ShowSpinner(this.props.auth) && <div className='panel__refresh'><LoadingIcon /></div>}
                <div className='tabs tabs--justify tabs--bordered-bottom mb-30'>
                  <div className='tabs__wrap'>
                    <NaviBar history={this.props.history} navi={naviBar} />
                  </div>
                </div>
                <p className="project-title-mobile mb-10">{emptyTitle}</p>
                <div className='account__head'>
                  <h3 className='account__title'>{this.props.auth.Label.Profile}</h3>
                  <h4 className='account__subhead subhead'>{this.props.auth.Label.ProfileSubtitle}</h4>
                </div>
                <Formik
                  initialValues={{
                    cNewLoginName: this.state.loginName,
                    cNewUserName: this.state.userName,
                    cNewUserEmail: this.state.userEmail,
                  }}
                  validate={values => {
                  }}
                  key={this.state.key}
                  onSubmit={this.saveProfile}
                  render={({
                    errors,
                    touched,
                    isSubmitting,
                    values
                  }) => (
                      <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}
                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.NewLoginName}</label>
                          <div className='form__form-group-field'>
                            <Field
                              type="text"
                              name="cNewLoginName"
                              onClick={this.handleFocus}
                            />
                          </div>
                        </div>

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.NewUserName}</label>
                          <div className='form__form-group-field'>
                            <Field
                              type="text"
                              name="cNewUserName"
                              onClick={this.handleFocus}
                            />
                          </div>
                        </div>

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.NewUserEmail}</label>
                          <div className='form__form-group-field'>
                            <Field
                              type="email"
                              name="cNewUserEmail"
                            />
                          </div>
                        </div>

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>FCM Token</label>
                          <div className='form__form-group-field'>
                            <Field
                              type="text"
                              name="cFCMToken"
                              value={this.state.fcmToken || ''}
                            />
                          </div>
                          { window.Notification && navigator.serviceWorker &&
                          <div>
                            <Button type='button' onClick={this.deleteFCMToken}>delete</Button>
                            <Button type='button' onClick={this.enableFCMNotification}>enable notification</Button>
                          </div>
                          }
                          </div>

                        <div className="form__form-group mb-0">
                          <Row className="btn-bottom-row">
                            <Col xs={3} sm={2} className="btn-bottom-column">
                              <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className="fa fa-long-arrow-left"></i></Button>
                            </Col>
                            <Col xs={9} sm={10} className="btn-bottom-column">
                              <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>{this.props.auth.Label.UpdProfileBtn}</Button>
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
    { saveProfile: saveProfile },
    { setTitle: setTitle },
    { showNotification: showNotification },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Profile));

