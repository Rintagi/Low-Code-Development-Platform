import React, { Component } from 'react';
import EyeIcon from 'mdi-react/EyeIcon';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, getWebAuthnAssertionRequest, webauthnAssertion, getWeb3SigningRequest, web3Assertion } from '../../redux/Auth';
import { Link } from 'react-router-dom';
import log from '../../helpers/logger';
import { setSpinner } from '../../redux/Global';
import { parsedUrl, base64UrlEncode, base64UrlDecode, base64Codec, coerceToArrayBuffer, IsMobile, isSafari } from '../../helpers/domutils';
import { getRintagiConfig } from '../../helpers/config';
import Web3 from "web3";
import WalletConnectProvider from "@walletconnect/web3-provider";

function trackWeb3(provider) {
  provider.on("accountsChanged", (accounts) => {
    log.debug("web3 account changed", provider, accounts);
  });

  // Subscribe to chainId change
  provider.on("chainChanged", (chainId) => {
    log.debug("web3 chain changed", provider, chainId);
  });

  // Subscribe to provider connection
  provider.on("connect", (info) => {
    const { chainId } = info || {}
    log.debug("web3 connected", provider, info);
  });

  // Subscribe to provider disconnection
  provider.on("disconnect", (code, reason) => {
    log.debug("web3 disconnected", provider, code, reason);
  });
}

class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showPassword: false,
      submitting: false,
      userId: this.props.user.UsrId,
    };

    this.showPassword = this.showPassword.bind(this);
    this.webAuthnLogin = this.webAuthnLogin.bind(this);
    this.connectWeb3 = this.connectWeb3.bind(this);
    this.handleSubmit = ((values, { setSubmitting, setErrors /* setValues and other goodies */ }) => {
      this.setState({ submitting: true, setSubmitting: setSubmitting });
      // this.state.submitting = true;
      // this.state.setSubmitting = setSubmitting;
      this.props.login(values.username, values.password);
    });

    this.props.setSpinner(true);

    this.props.getCurrentUser()
      .then((data) => {
        //log.debug('get current user return', this.props.user);
        if (!this.props.user || !this.props.user.UsrId) {
          const _this = this;
          this.props.setSpinner(false, 'login end');
          // this.setState({ showBox: true });
          setTimeout(() => { _this.props.setSpinner(false, '#####Login2Then'); }, 500);
        }
        //log.debug(data);
        // do nothing if info retrieved
      })
      .catch(error => {
        log.debug('get current user error');
        const _this = this;
        this.props.setSpinner(false, 'login end');
        // this.setState({ showBox: true });
        setTimeout(() => { _this.props.setSpinner(false); }, 500);
      })
      .finally(() => {
        const _this = this;
        this.props.setSpinner(false, 'login end');
        // log.debug('get current user done');
        /* must reset this */
        setTimeout(() => { _this.props.setSpinner(false, '#####Login2Final'); }, 500);
      });

  }

  transformWebAuthRequest(request) {
    request.status = undefined;
    request.errorMessage = undefined;
    request.challenge = coerceToArrayBuffer(request.challenge, 'challenge');
    if (request.user && request.user.id) {
      request.user.id = coerceToArrayBuffer(request.user.id, 'user.id');
    }
    if (request.excludeCredentials && request.excludeCredentials) {
      request.excludeCredentials = (request.excludeCredentials || []).map(function (c, i) {
        c.id = coerceToArrayBuffer(c.id, 'excludeCredentials.' + i + '.id');
        return c;
      });
    }
    if (request.allowCredentials && request.allowCredentials) {
      request.allowCredentials = (request.allowCredentials || []).map(function (c, i) {
        c.id = coerceToArrayBuffer(c.id, 'allowCredentials.' + i + '.id');
        return c;
      });
    }
    if (request.authenticatorSelection && !request.authenticatorSelection.authenticatorAttachment) {
      request.authenticatorSelection.authenticatorAttachment = undefined;
    }
    return request;
  }

  getAssertionRequestAsync() {
    const _this = this;
    const request = this.state.assertionRequest;
    return new Promise(function (resolve, reject) {
      if (!request) reject("no request json");
      else {
        try {
          const x = JSON.parse(request);
          resolve(request);
        } catch (e) {
          reject(e);
        }
      }
    });
  }

  webAuthnLogin(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const _this = this;
    let registeredFido2 = null;
    let loginRequest = null;
    const b64url = new base64Codec(true);

    try {
      registeredFido2 = localStorage["Fido2Login"] || "none";
    }
    catch (e) {

    }
    return function (evt) {
      _this.setState({ submitting: true, setSubmitting: setSubmitting });
      _this.getAssertionRequestAsync()
        .then(function (request) {
          loginRequest = request;
          const x = JSON.parse(request);
          request = _this.transformWebAuthRequest(x);
          if ((!request.allowCredentials || request.allowCredentials.length == 0) && registeredFido2 != "none" && IsMobile()) {
            request.allowCredentials = [
              {
                id: b64url.decode(registeredFido2),
                type: "public-key",
                // can't use these for iOS safari
                transports: isSafari() ? undefined : ["ble", "internal", "lightning", "nfc", "usb"],
              },
            ];
          }
          return navigator.credentials.get({
            publicKey: request
          });
        }
        )
        .then(function (rawAssertion) {
          var id = rawAssertion.id; // id during registration b64url
          var assertion = {
            id: rawAssertion.id,
            rawId: b64url.encode(rawAssertion.rawId),
            type: rawAssertion.type,
            extensions: rawAssertion.getClientExtensionResults(),
            response: {
              clientDataJSON: b64url.encode(rawAssertion.response.clientDataJSON),
              userHandle: rawAssertion.response.userHandle && b64url.encode(rawAssertion.response.userHandle) ? b64url.encode(rawAssertion.response.userHandle) : null,
              signature: b64url.encode(rawAssertion.response.signature),
              authenticatorData: b64url.encode(rawAssertion.response.authenticatorData)
            }
          };
          var assertionJSON = JSON.stringify(assertion);
          _this.props.webauthnAssertion(loginRequest, assertionJSON)
                .then(result=>{
                  log.debug(result);
                })
                .catch(error=>{
                  log.debug(error);
                });
        })
        .catch(function (err) {
          console.log(err);
          if (
            err.name == "NotAllowedError" // user denied OR another active prompt by another process(firefox/chrome)
            ||
            err.name == "UnknownError" // Legacy Edge when another active prompt by another process 
            ||
            err.name == "NotSupportedError" // Legacy Edge when another active prompt by another process 
          ) {
            if (err.name == "NotSupportedError"
              &&
              loginRequest.allowCredentials.length == 0) {
              alert(err);
            }
            else {
              alert(err);
            }
          }
        })
      console.log(values);
    }
  }

  connectWeb3(metamask, values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const _this = this;
    const rintagi = getRintagiConfig() || {};
    const web3rpc = rintagi.web3rpc || {};
    const web3NetworkId = rintagi.web3Networkid || 1;
    const infuraId = rintagi.infuraId
    const web3rpcUrl = web3rpc[web3NetworkId];
    const ethereumProvider = typeof window.ethereum != 'undefined' && window.ethereum;

    return function (evt) {
      // Create WalletConnect Provider
      const provider = metamask && ethereumProvider
      ? ethereumProvider
      : new WalletConnectProvider({
        infuraId: infuraId, // required
        pollingInterval: 60 * 60 * 1000, // in ms must do this to stop the frequent polling(default 1s, too much for infura)
        rpc: {
          100: "https://rpc.xdaichain.com/", // required for any non-ethereum networkId(returned from remote wallet)
          ...web3rpc,
        }
      });
      // Enable session (triggers QR Code modal)
      trackWeb3(provider);
      try {
        const signingRequest = _this.state.web3SigningRequest;
        const web3Wallet= new Web3(provider);
        const wallet = provider.enable()
          .then(
            (accounts) => {
              log.debug(accounts);
              if ((accounts || []).length > 0) {
                return web3Wallet.eth.personal.sign(signingRequest, accounts[0]);    
              }
            }
          )
          .then((sig) => {
            log.debug(sig);
            _this.props.web3Assertion(signingRequest, sig)
            .then(result => {
              log.debug(result);
            })
            .catch(error => {
              log.debug(error);
            });
          })
          .catch(error => {
            log.debug(error);
          })
          .finally(() => {
            setTimeout(() => {
              // must delay the disconnect or else the signing request would trigger another wallet connect modal popup
              log.debug("disconnect");
              provider.disconnect && provider.disconnect();              
            }, 30000);
          })
          ;
        log.debug(provider, web3Wallet);
        const web3 = new Web3(web3rpcUrl);
        return { web3, web3Wallet };
        // setTimeout(() => {
        //   log.debug('disconnect after 5s');
        //   provider.disconnect();
        // }, 60*60*1000);
      }
      catch (e) {
        log.debug(e);
      }
    }
  }

  componentDidMount() {
    // if (!this.props.user || !this.props.user.UsrId) {
    //   this.props.getCurrentUser();
    // }
    // console.log(this.props.user);
    const fido2 = typeof window != 'undefined' && window.PublicKeyCredential;
    if (fido2) {
      const _this = this;
      const myUrl = parsedUrl(window.location);
      const loginName = localStorage["Fido2Login"];
      this.props.getWebAuthnAssertionRequest(myUrl.href, loginName)
        .then(result => {
          log.debug(result);
          if (!_this.isUnmounted) {
            _this.setState({
              assertionRequest: ((result || {}).data || {}).assertionRequest,
            });  
          }
          else {
            log.debug('unmounted login component');
          }
        })
        .catch(error => {
          log.debug(error);
        })
    }
    const ethereum = typeof window != 'undefined' && window.ethereum;
    if (ethereum || true) {
      const _this = this;
      const myUrl = parsedUrl(window.location);
      this.props.getWeb3SigningRequest(myUrl.href)
        .then(result => {
          log.debug(result);
          _this.setState({
            web3SigningRequest: ((result || {}).data || {}).signingRequest,
          });
        })
        .catch(error => {
          log.debug(error);
        })
    }

  }

  componentWillUnmount() {
    // NOTE setup flag
    this.isUnmounted = true;
    log.debug('login component unmounted');
  }

  componentDidUpdate(prevProps, prevStats) {

    const from = ((this.props.location || {}).state || {}).from;
    if ((!prevProps.user || !prevProps.user.UsrId) && this.props.user && this.props.user.UsrId) {
      //window.location.reload();
    }

    // if (this.props.global.pageSpinner && !this.state.userId && !this.props.user.loading) {
    //   this.props.setSpinner(false, 'login end');
    // }

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
                  values,
                  setSubmitting,
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
                      <Button color='success' className='btn btn-success account__btn mb-20' type="submit" disabled={isSubmitting}>{this.props.auth.Label.Login}</Button>
                      {
                        this.state.assertionRequest &&
                        <Button color='success' className='btn btn-success account__btn mb-20' onClick={this.webAuthnLogin(values, { setSubmitting })} disabled={isSubmitting}>{"WebAuthn Login"}</Button>
                      }
                      {
                        this.state.web3SigningRequest  &&
                        <Button color='success' className='btn btn-success account__btn mb-20' onClick={this.connectWeb3(false, values, { setSubmitting })} disabled={isSubmitting}>{"Eth1 Mobile Wallet Login"}</Button>
                      }
                      {
                        this.state.web3SigningRequest && window.ethereum &&
                        <Button color='success' className='btn btn-success account__btn mb-20' onClick={this.connectWeb3(true, values, { setSubmitting })} disabled={isSubmitting}>{"MetaMask Login"}</Button>
                      }
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
    { getWebAuthnAssertionRequest: getWebAuthnAssertionRequest },
    { webauthnAssertion: webauthnAssertion },
    { getWeb3SigningRequest: getWeb3SigningRequest },
    { web3Assertion: web3Assertion },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Login));
