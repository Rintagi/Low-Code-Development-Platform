import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Formik, Field, Form } from 'formik';
import { Redirect, withRouter } from 'react-router-dom';
import EyeIcon from 'mdi-react/EyeIcon';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, changePassword, ShowSpinner, getWebAuthnRegistrationRequest, webauthnRegistration, getWeb3SigningRequest, web3Registration } from '../../redux/Auth';
import { Row, Col } from 'reactstrap';
import NaviBar from '../../components/custom/NaviBar';
import { getNaviBar } from './index';
import ProgressBar from '../../components/custom/ProgressBar';
import queryString from 'query-string'
import Alert from '../../components/Alert';
import { Link } from 'react-router-dom';
import log from '../../helpers/logger';
import DocumentTitle from '../../components/custom/DocumentTitle';
import { setTitle } from '../../redux/Global';
import { makeEIP712Types } from '../../helpers/utils';
import { parsedUrl, base64UrlEncode, base64UrlDecode, base64Codec, coerceToArrayBuffer, IsMobile } from '../../helpers/domutils';
import { getRintagiConfig } from '../../helpers/config';
import Web3 from "web3";
import WalletConnectProvider from "@walletconnect/web3-provider";
//import * as sigUtil from 'eth-sig-util';

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
class NewPassword extends Component {
  constructor(props) {
    super(props);
    this.titleSet = false;
    this.SystemName = (document.Rintagi || {}).systemName || 'Rintagi';
    this.state = {
      submitting: false,
      showPassword: false,
      j: '',
      p: ''
    };

    this.showPassword = this.showPassword.bind(this);
    this.changePassword = this.changePassword.bind(this);
    this.webauthnRegistration = this.webauthnRegistration.bind(this);
    this.connectWeb3 = this.connectWeb3.bind(this);
  }

  componentDidMount() {
    const qs = queryString.parse(this.props.location.search);
    const fido2 = typeof window != 'undefined' && window.PublicKeyCredential;
    if (fido2) {
      const _this = this;
      const myUrl = parsedUrl(window.location);
      this.props.getWebAuthnRegistrationRequest(myUrl.href)
        .then(result => {
          log.debug(result);
          _this.setState({
            registrationRequest: ((result || {}).data || {}).registrationRequest,
          });
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

    this.setState({
      j: qs.j,
      p: qs.p,
    });

  }
  componentDidUpdate(prevprops, prevstates) {
    const emptyTitle = '';
    const siteTitle = this.SystemName;

    if (!this.titleSet) {
      this.props.setTitle(siteTitle);
      this.titleSet = true;
    }
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
  showPassword(e) {
    e.preventDefault();
    this.setState({
      showPassword: !this.state.showPassword,
      submitting: false,
    })
  }

  static getDerivedStateFromProps(nextProps, prevState) {
    if (prevState.submitting) {
      prevState.setSubmitting(false);
    }
    return null;
  }

  changePassword(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    this.setState({ submitting: true, setSubmitting: setSubmitting });
    console.log(values);

    this.props.changePassword(
      {
        j: this.state.j || '',
        p: this.state.p || '',
        newUsrPassword: values.cNewUsrPassword,
        confirmPwd: values.cConfirmPwd,
      }
    );
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

  getRegistrationRequestAsync() {
    const _this = this;
    const request = this.state.registrationRequest;
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

  getWeb3RegistrationRequestAsync() {
    const _this = this;
    const request = this.state.web3RegistrationRequest;
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

  webauthnRegistration(values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const _this = this;
    const b64url = new base64Codec(true);
    let registrationRequest = null;
    return function (evt) {
      _this.setState({ submitting: true, setSubmitting: setSubmitting });
      _this.getRegistrationRequestAsync()
        .then((request) => {
          registrationRequest = request;
          const x = JSON.parse(request);
          request = _this.transformWebAuthRequest(x);
          return navigator.credentials.create({
            publicKey: request
          })
        })
        .then(function (newCredentialInfo) {
          var extensions = newCredentialInfo.getClientExtensionResults();
          var result = {
            Id: newCredentialInfo.id,
            RawId: b64url.encode(newCredentialInfo.rawId),
            Type: newCredentialInfo.type,
            Response: {
              AttestationObject: b64url.encode(newCredentialInfo.response.attestationObject),
              ClientDataJson: b64url.encode(newCredentialInfo.response.clientDataJSON),
            },
            Extensions: extensions,
            attestationObject: b64url.encode(newCredentialInfo.response.attestationObject),
            platform: navigator.platform,
            isMobile: IsMobile(),
          };
          var resultJSON = JSON.stringify(result);
          _this.props.webauthnRegistration(registrationRequest, resultJSON)
            .then(result => {
              log.debug(result);
              if (((result || {}).data || {}).credentialId) {
                localStorage["Fido2Login"] = newCredentialInfo.id;
                if (result.data.message) {
                  alert(result.data.message);
                }
              }
            })
            .catch(error => {
              log.debug(error);
              alert("registration failed");
            });
        }).catch((err) => {
          alert(err);
          // No acceptable authenticator or user refused consent. Handle appropriately.
        }).finally(
          () => {
            _this.setState({ submitting: false });
          }
        );
      console.log(values);

    }
  }

  async signEIP712Msg(web3, from, msgParams, chainId) {
    const provider = web3.currentProvider || {};
    const _chainId = chainId || await web3.eth.getChainId();
    var method = provider.isMetaMask ? 'eth_signTypedData_v4' : 'eth_signTypedData';
    var params = [from, msgParams];
    return web3.currentProvider
      .request({
        method: method,
        params: params,
      })
      .then((result) => {
        log.debug(result);
        return result;
      })
      .catch((error) => {
        log.debug(error);
        return Promise.reject(error);
      });
  }
  async testSimpleToken(web3Wallet, from, chainId) {
    const _this = this;
    const rintagi = getRintagiConfig() || {};
    const web3rpc = rintagi.web3rpc || {};
    const simpleTokenAbi = '[{"inputs":[],"stateMutability":"nonpayable","type":"constructor"},{"inputs":[{"indexed":true,"internalType":"address","name":"owner","type":"address"},{"indexed":true,"internalType":"address","name":"spender","type":"address"},{"indexed":false,"internalType":"uint256","name":"value","type":"uint256"}],"anonymous":false,"name":"Approval","type":"event"},{"inputs":[{"indexed":true,"internalType":"address","name":"previousOwner","type":"address"},{"indexed":true,"internalType":"address","name":"newOwner","type":"address"}],"anonymous":false,"name":"OwnershipTransferred","type":"event"},{"inputs":[{"indexed":false,"internalType":"address","name":"account","type":"address"}],"anonymous":false,"name":"Paused","type":"event"},{"inputs":[{"indexed":true,"internalType":"address","name":"account","type":"address"}],"anonymous":false,"name":"PauserAdded","type":"event"},{"inputs":[{"indexed":true,"internalType":"address","name":"account","type":"address"}],"anonymous":false,"name":"PauserRemoved","type":"event"},{"inputs":[{"indexed":true,"internalType":"address","name":"from","type":"address"},{"indexed":true,"internalType":"address","name":"to","type":"address"},{"indexed":false,"internalType":"uint256","name":"value","type":"uint256"}],"anonymous":false,"name":"Transfer","type":"event"},{"inputs":[{"indexed":false,"internalType":"address","name":"account","type":"address"}],"anonymous":false,"name":"Unpaused","type":"event"},{"inputs":[],"name":"INITIAL_SUPPLY","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"MAX_INT","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"account","type":"address"}],"name":"addPauser","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"owner","type":"address"},{"internalType":"address","name":"spender","type":"address"}],"name":"allowance","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"value","type":"uint256"}],"name":"approve","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"holder","type":"address"},{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"nonce","type":"uint256"},{"internalType":"uint256","name":"expiry","type":"uint256"},{"internalType":"uint256","name":"value","type":"uint256"},{"internalType":"bytes","name":"signature","type":"bytes"}],"name":"authorize","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"authorize_type","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"authorize_type_hash","outputs":[{"internalType":"bytes32","name":"","type":"bytes32"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"holder","type":"address"},{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"nonce","type":"uint256"},{"internalType":"uint256","name":"expiry","type":"uint256"},{"internalType":"uint256","name":"value","type":"uint256"},{"internalType":"bytes","name":"signature","type":"bytes"}],"name":"authorize_verified","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"owner","type":"address"}],"name":"balanceOf","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"decimals","outputs":[{"internalType":"uint8","name":"","type":"uint8"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"subtractedValue","type":"uint256"}],"name":"decreaseAllowance","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"domain_seperator_hash","outputs":[{"internalType":"bytes32","name":"","type":"bytes32"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"domain_seperator_type","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"eip712_domain","outputs":[{"internalType":"string","name":"name","type":"string"},{"internalType":"string","name":"version","type":"string"},{"internalType":"uint256","name":"chainId","type":"uint256"},{"internalType":"address","name":"verifyingContract","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getChainId","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getEIP712Domain","outputs":[{"components":[{"internalType":"string","name":"name","type":"string"},{"internalType":"string","name":"version","type":"string"},{"internalType":"uint256","name":"chainId","type":"uint256"},{"internalType":"address","name":"verifyingContract","type":"address"}],"internalType":"struct EIP712Domain","name":"","type":"tuple"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"addedValue","type":"uint256"}],"name":"increaseAllowance","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"isOwner","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"addr","type":"address"}],"name":"isPOAAddress","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"addr","type":"address"}],"name":"isPOADisabled","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"account","type":"address"}],"name":"isPauser","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_addr","type":"address"}],"name":"isValidContractAddress","outputs":[{"internalType":"bool","name":"hasCode","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"name","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"nonces","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"owner","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint8","name":"v","type":"uint8"},{"internalType":"bytes32","name":"r","type":"bytes32"},{"internalType":"bytes32","name":"s","type":"bytes32"}],"name":"packSignature","outputs":[{"internalType":"bytes","name":"","type":"bytes"}],"stateMutability":"pure","type":"function"},{"inputs":[],"name":"pause","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"paused","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"holder","type":"address"},{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"nonce","type":"uint256"},{"internalType":"uint256","name":"expiry","type":"uint256"},{"internalType":"bool","name":"allowed","type":"bool"},{"internalType":"uint8","name":"v","type":"uint8"},{"internalType":"bytes32","name":"r","type":"bytes32"},{"internalType":"bytes32","name":"s","type":"bytes32"}],"name":"permit","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"permit_type","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"permit_type_hash","outputs":[{"internalType":"bytes32","name":"","type":"bytes32"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"holder","type":"address"},{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"nonce","type":"uint256"},{"internalType":"uint256","name":"expiry","type":"uint256"},{"internalType":"bool","name":"allowed","type":"bool"},{"internalType":"uint8","name":"v","type":"uint8"},{"internalType":"bytes32","name":"r","type":"bytes32"},{"internalType":"bytes32","name":"s","type":"bytes32"}],"name":"permit_verified","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"renouncePauser","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"bytes","name":"sig","type":"bytes"}],"name":"splitSignature","outputs":[{"internalType":"uint8","name":"","type":"uint8"},{"internalType":"bytes32","name":"","type":"bytes32"},{"internalType":"bytes32","name":"","type":"bytes32"}],"stateMutability":"pure","type":"function"},{"inputs":[],"name":"symbol","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalSupply","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"to","type":"address"},{"internalType":"uint256","name":"value","type":"uint256"}],"name":"transfer","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"from","type":"address"},{"internalType":"address","name":"to","type":"address"},{"internalType":"uint256","name":"value","type":"uint256"}],"name":"transferFrom","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"newOwner","type":"address"}],"name":"transferOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"unpause","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"version","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"}]';
    const simpleTokenNetworkId = 5;
    const simpleTokenAddress = "0xc2296464fb0d0974955c1c4fb01d71b4ad46367b"; // this is on goerli(chain id 5)
    const private9999Url = web3rpc[simpleTokenNetworkId+''];
    const web3Private = new Web3(private9999Url);
    const simpleToken = new web3Private.eth.Contract(JSON.parse(simpleTokenAbi), simpleTokenAddress);
    log.debug(simpleToken, private9999Url);
    const holder = from;
    const spender = web3Private.utils.toChecksumAddress("0xCD2a3d9F938E13CD947Ec05AbC7FE734Df8DD826");
    const nonce = 1;
    const expiry = Date.now()*1000;
    const allowedValue = web3Private.utils.toWei(10+'','ether');
    let msgParams;
    simpleToken.methods.getEIP712Domain().call()
        .then(result=>{
          log.debug(result);
          return result;
        })
        .then(eip712domain=>{
          const eip712Domain = "EIP712Domain(string name,string version,uint256 chainId,address verifyingContract)";
          const authorizeFunc = "authorize(address holder,address spender,uint256 nonce,uint256 expiry,uint256 value)";
          const permitFunc = "permit(address holder,address spender,uint256 nonce,uint256 expiry,bool allowed)";
          log.debug(web3Wallet.utils.keccak256(eip712Domain));
          log.debug(web3Wallet.utils.keccak256(authorizeFunc));
          const authorizeTypes = makeEIP712Types(eip712Domain, [authorizeFunc]);
          msgParams = JSON.stringify({
            types: authorizeTypes,
            primaryType: "authorize",
            domain: {
              name: eip712domain[0]
              , version: eip712domain[1]
              , chainId: eip712domain[2]
              , verifyingContract: eip712domain[3]
            },
            message: {
              holder: holder,
              spender: spender,
              nonce: nonce,
              expiry: expiry,
              value: allowedValue,
            }
          });
          return _this.signEIP712Msg(web3Wallet, from, msgParams, chainId);          
        })
        .then(result=>{
          log.debug(result);
          // const recovered = sigUtil.recoverTypedSignature_v4({ data: JSON.parse(msgParams), sig: result });
          // log.debug(recovered);
          const verified = simpleToken.methods.authorize_verified(holder, spender, nonce, expiry, allowedValue, result).call()
                            .then(result=>{
                              log.debug(result);
                            })
        })
        .catch(err=>{
          log.debug(err);
        })
  }
  async signEIP712Sample(web3, from) {

    const provider = web3.currentProvider || {};
    const _chainId = await web3.eth.getChainId();
    const eip712Domain = "EIP712Domain(string name,string version,uint256 chainId,address verifyingContract)";
    const mailStruct = "Mail(Person from,Person to,string contents)";
    const personStruct = "Person(string name,address wallet)";
    const mailTypes = makeEIP712Types(eip712Domain, [personStruct, mailStruct]);
    const msgParams = JSON.stringify({
      types: mailTypes,
      primaryType: "Mail",
      domain: {
        name: "Ether Mail"
        , version: "1"
        , chainId: _chainId
        , verifyingContract: "0xCcCCccccCCCCcCCCCCCcCcCccCcCCCcCcccccccC"
      },
      message: {
        from: { name: "Cow", wallet: "0xCD2a3d9F938E13CD947Ec05AbC7FE734Df8DD826" },
        to: { name: "Bob", wallet: "0xbBbBBBBbbBBBbbbBbbBbbbbBBbBbbbbBbBbbBBbB" },
        contents: "Hello, Bob!"
      }
    })
    var params = [from, msgParams];
    log.debug(params);
    var method = provider.isMetaMask ? 'eth_signTypedData_v4' : 'eth_signTypedData';

    web3.currentProvider
      .request({
        method: method,
        params: params,
      })
      .then((result) => {
        log.debug(result);
      })
      .catch((error) => {
        log.debug(error);
      });

  }
  connectWeb3(metamask, values, { setSubmitting, setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    const _this = this;
    const rintagi = getRintagiConfig() || {};
    const web3rpc = rintagi.web3rpc || {};
    const web3NetworkId = rintagi.web3Networkid || 1;
    const infuraId = rintagi.infuraId
    const web3rpcUrl = web3rpc[web3NetworkId+''];
    const ethereumProvider = typeof window.ethereum != 'undefined' && window.ethereum;

    log.debug(web3rpc,web3rpcUrl);
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
        const web3Wallet = new Web3(provider);
        web3Wallet.eth.getChainId().then(chainId => {
          window.chainId = chainId;
          log.debug(chainId);
        })
        const wallet = provider.enable()
          .then(
            (accounts) => {
              log.debug(accounts, provider.walletMeta);
              if ((accounts || []).length > 0) {
                //_this.testSimpleToken(web3Wallet, accounts[0], window.chainId);
                //_this.signEIP712Sample(web3Wallet, accounts[0], 100);
                return web3Wallet.eth.personal.sign(signingRequest, accounts[0]);
              }
            }
          )
          .then((sig) => {
            log.debug(sig);
            sig && _this.props.web3Registration(signingRequest, sig)
              .then(result => {
                log.debug(result);
                if (((result || {}).data || {}).walletAddress) {
                  if (result.data.message) {
                    alert(result.data.message);
                  }
                }
              })
              .catch(error => {
                log.debug(error);
                alert("registration failed");
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

  render() {

    const naviBar = getNaviBar("NewPassword", this.props.auth.Label);
    const siteTitle = (this.props.global || {}).pageTitle || '';
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
                  <h3 className='account__title'>{this.props.auth.Label.NewPassword}</h3>
                  <h4 className='account__subhead subhead'>{this.props.auth.Label.NewPasswordSubtitle}</h4>
                  {/* <h4>{(this.props.user || {}).UsrId}</h4> */}
                </div>
                <Formik
                  initialValues={{
                    cNewUsrPassword: '',
                    cConfirmPwd: '',
                    // password: '',
                  }}
                  validate={values => {
                    // // same as above, but feel free to move this into a class method now.
                    let errors = {};
                    if (!values.cNewUsrPassword) {
                      errors.cNewUsrPassword = this.props.auth.Label.NewUsrPasswordEmpty;
                    }
                    if (values.cNewUsrPassword !== values.cConfirmPwd) {
                      errors.cConfirmPwd = this.props.auth.Label.ConfirmPwdEmpty;
                    }
                    // if (!values.email) {
                    // //   errors.email = 'This field is required';
                    // // } else
                    // if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
                    //   errors.email = 'Invalid email address';
                    // }
                    return errors;
                  }}
                  onSubmit={this.changePassword}
                  render={({
                    errors,
                    touched,
                    setSubmitting,
                    isSubmitting,
                    values
                  }) => (
                      <Form className='form'> {/* this line equals to <form className='form' onSubmit={handleSubmit} */}
                        <div className='form__form-group'>
                          <div className='form__form-group-field'>
                            <ProgressBar
                              name="cPasswordStrength"
                              text={values.cNewUsrPassword}
                            />
                          </div>
                        </div>

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.NewuserPassword}</label>
                          <div className='form__form-group-field'>
                            <Field
                              type={this.state.showPassword ? 'text' : 'password'}
                              name="cNewUsrPassword"

                            />
                            <button className={`form__form-group-button${this.state.showPassword ? ' active' : ''}`}
                              onClick={(e) => this.showPassword(e)} type="button"><EyeIcon /></button>
                          </div>
                          {errors.cNewUsrPassword && touched.cNewUsrPassword && <span className='form__form-group-error'>{errors.cNewUsrPassword}</span>}

                        </div>
                        {/* <div className='form__form-group'>
                        <label className='form__form-group-label'>or</label>
                      </div> */}

                        <div className='form__form-group'>
                          <label className='form__form-group-label'>{this.props.auth.Label.ConfirmPwd}</label>
                          <div className='form__form-group-field'>
                            <Field
                              type={this.state.showPassword ? 'text' : 'password'}
                              name="cConfirmPwd"
                            />
                            <button className={`form__form-group-button${this.state.showPassword ? ' active' : ''}`}
                              onClick={(e) => this.showPassword(e)} type="button"><EyeIcon /></button>
                          </div>
                          {errors.cConfirmPwd && touched.cConfirmPwd && <span className='form__form-group-error'>{errors.cConfirmPwd}</span>}
                        </div>

                        <div className='form__form-group'>
                          <Alert color='info' className='alert--neutral' icon>
                            <p><span className='bold-text'>{this.props.auth.Label.Information}:</span> {this.props.auth.Label.PwdHlpMsgLabel}</p>
                          </Alert>
                        </div>

                        <div className="form__form-group mb-0">
                          <Row className="btn-bottom-row">
                            <Col xs={3} sm={2} className="btn-bottom-column">
                              <Button color='success' className='btn btn-outline-success account__btn' onClick={this.props.history.goBack} outline><i className="fa fa-long-arrow-left"></i></Button>
                            </Col>
                            <Col xs={9} sm={10} className="btn-bottom-column">
                              <Button color='success' className='btn btn-success account__btn' type="submit" disabled={isSubmitting}>{this.props.auth.Label.pdPwdBtn}</Button>
                            </Col>
                          </Row>
                        </div>
                        {this.state.registrationRequest &&
                          <div className="form__form-group mb-0">
                            <Row className="btn-bottom-row">
                              <Col xs={1} sm={12} className="btn-bottom-column">
                                <Button color='success' className='btn btn-success account__btn' onClick={this.webauthnRegistration(values, { setSubmitting })} disabled={isSubmitting}>{"WebAuthn Registration"}</Button>
                              </Col>
                            </Row>
                          </div>
                        }
                        {this.state.web3SigningRequest &&
                          <div className="form__form-group mb-0">
                            <Row className="btn-bottom-row">
                              <Col xs={1} sm={12} className="btn-bottom-column">
                                <Button color='success' className='btn btn-success account__btn' onClick={this.connectWeb3(false, values, { setSubmitting })} disabled={isSubmitting}>{"Eth1 Mobile Wallet Registration"}</Button>
                              </Col>
                            </Row>
                          </div>
                        }
                        {this.state.web3SigningRequest && window.ethereum &&
                          <div className="form__form-group mb-0">
                            <Row className="btn-bottom-row">
                              <Col xs={1} sm={12} className="btn-bottom-column">
                                <Button color='success' className='btn btn-success account__btn' onClick={this.connectWeb3(true, values, { setSubmitting })} disabled={isSubmitting}>{"MetaMask Wallet Registration"}</Button>
                              </Col>
                            </Row>
                          </div>
                        }
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
    { changePassword: changePassword },
    { getWebAuthnRegistrationRequest: getWebAuthnRegistrationRequest },
    { webauthnRegistration: webauthnRegistration },
    { getWeb3SigningRequest: getWeb3SigningRequest },
    { web3Registration: web3Registration },
    { setTitle: setTitle },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(NewPassword));
