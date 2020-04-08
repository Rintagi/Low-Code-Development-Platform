import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, saveProfile, ShowSpinner } from '../../redux/Auth';
import { setTitle } from '../../redux/Global';
import { authService } from '../../services/authService'
import log from '../../helpers/logger';
import AwesomeSlider from 'react-awesome-slider';
import 'react-awesome-slider/dist/styles.css';
import { getUrlBeforeRouter } from "../../helpers/formatter";
import { getDefaultPath } from '../../helpers/utils';
import { getRintagiConfig } from '../../helpers/config';

class Default extends Component {
  constructor(props) {
    super(props);

    this.state = {
      reactQuickMenu: [],
    }

    this.buildQuickMenuTree = this.buildQuickMenuTree.bind(this);
    this.switchSystem = this.switchSystem.bind(this);
    this.showFullList = this.showFullList.bind(this);
  }


  buildSystem(systemList, curSystemId) {
  }

  switchSystem(e) {
    log.debug('switchSystem', e, e.currentMedia.id);
    this.buildQuickMenuTree(e.currentMedia.id);
  };

  showFullList() {
    alert("test");
  }

  buildQuickMenuTree(systemId) {
    return authService.getReactQuickMenu(systemId)
      .then(
        data => {
          log.debug("data", data.data);
          this.setState({ reactQuickMenu: data.data });
          return data;
        }).catch(error => {
          this.setState({ reactQuickMenu: [] });
          log.debug(error);
          return Promise.reject();
        }).finally(() => {

        });
  }

  componentDidMount() {
    const runtimeConfig = getRintagiConfig() || {};
    const curSystemId = runtimeConfig.systemId || '';

    this.buildQuickMenuTree(curSystemId);
  }

  componentDidUpdate(prevprops, prevstates) {
    const emptyTitle = '';
    const siteTitle = "System List";

    if (!this.titleSet) {
      this.props.setTitle(siteTitle);
      this.titleSet = true;
    }
  }

  render() {
    const runtimeConfig = getRintagiConfig() || {};
    const curSystemId = runtimeConfig.systemId || '';
    const appDomainUrl = runtimeConfig.appDomainUrl || "";
    const systemList = (this.props.system || {}).systemList || [];
    log.debug("systemList", systemList);
    var newSysList = [];
    const selectedSys = systemList.filter((v) => (v.SystemId == curSystemId));
    const unSelectedSys = systemList.filter((v) => (v.SystemId != curSystemId));
    if (unSelectedSys) { newSysList = selectedSys.concat(unSelectedSys) } else { newSysList = selectedSys }
    log.debug("newSystemList", newSysList);

    const location = window.location;

    // const localOnly = getUrlBeforeRouter().includes('localhost');
    const localOnly = location.port >= 3000 && location.port <= 3100;
    //const localOnly = false;

    log.debug("reactQuickMenu", this.state.reactQuickMenu);
    const reactQuickMenu = this.state.reactQuickMenu || [];
    log.debug(reactQuickMenu, reactQuickMenu.length);
    return (
      <div>
        {/* Show this if it's local development */}
        {localOnly &&
          (
            <div className='account'>
              <div className='account__wrapper'>
                <div className='account__card shadow-box rad-4'>
                  <div className='account__head'>
                    <h1 className='account__title fill-fintrux text-center'>Welcome</h1>
                  </div>
                  <div>
                    <h3 className='account__subhead subhead text-center'>Please select menu items on the left to get started.</h3>
                  </div>
                  <div className='empty-block'>
                    <img alt='' src={require('../../img/default.png')} />
                  </div>
                </div>
              </div>
            </div>
          )
        }

        {/* Show this only on desktop view, responsive design */}
        {!localOnly &&
          <div className="container hideOnMobile">
            <div className='account'>
              <div className='account__wrapper'>
                <div className='account__card shadow-box rad-4'>
                  <div>
                    <div className="account__head">
                      <div className="row">
                        <div className="col-12">
                          <h3 className="account__title">System List</h3>
                          <h4 className="account__subhead subhead">Select the system and click the menu to get started</h4>
                        </div>
                      </div>
                    </div>
                    <hr />
                    <div className="row systemSelection">
                      {systemList.map((obj, i) => {
                        if (curSystemId === obj.SystemId) {
                          return (
                            <div className="col-md-12 col-lg-12 col-xl-6" key={i}>
                              <div className="card active">
                                <div className="card-body">
                                  <div className="systemLogo">
                                    <h5 className="bold-text">{obj.SystemName}</h5>
                                    <i className="checkedSys fa fa-lg fa-check-circle mr-11 color-white"></i>
                                  </div>
                                </div>
                              </div>
                            </div>
                          )
                        } else {
                          return (
                            <div className="col-md-12 col-lg-12 col-xl-6" key={i}>
                              <div className="card">
                                <a href={appDomainUrl + "/react/" + obj.SystemAbbr}>
                                  <div className="card-body">
                                    <div className="systemLogo">
                                      <h5 className="bold-text">{obj.SystemName}</h5>
                                    </div>
                                  </div>
                                </a>
                              </div>
                            </div>
                          )
                        }
                      })}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        }

        {/* Show this only on mobile device */}
        {!localOnly &&
          <div className="defaultCntSec showOnMobile">
            <div className="systemSec">
              <AwesomeSlider
                fillParent={true}
                bullets={false}
                className="defaultSliderStyle"
                buttons={false}
                onTransitionEnd={(e) => this.switchSystem(e)}
              >
                {newSysList.map((obj, i) => {
                  return (
                    <div className="col-12 col-md-12 col-lg-12" key={i} id={obj.SystemId}>
                      <div className="card customCard">
                        <div className="card-body">
                          <div className="systemLogo">
                            <h5 className="bold-text">{obj.SystemAbbr}</h5>
                          </div>
                          {curSystemId === obj.SystemId ?
                            <div className="systemSelected">
                              <h5 className="systemSelectedText">Current System</h5>
                            </div> : ""
                          }
                          <div className="card__title">
                            <h4 className="text-center bold-text systemTitle underline">{obj.SystemName}</h4>
                            <hr className="titleSeprate" />
                            {/* {curSystemId === obj.SystemId? <h5 className="text-center account__subhead curSystemIdication highlight">Current System</h5> : ""} */}
                            <div className="quickMenuSection">
                              {
                                reactQuickMenu.length > 0 ?
                                  reactQuickMenu.map((o, i) => {
                                    var navigateUrl = o.NavigateUrl ? (appDomainUrl + "/react/" + obj.SystemAbbr + "/#/" + getDefaultPath(o.NavigateUrl.replace('.aspx', ''))) : "";
                                    return (
                                      <div className="nmt-2 col quickMenuItem" key={i}>
                                        <a href={navigateUrl}>
                                          <i className="mdi mdi-chevron-right float-right fs-18 lh-22 color-white"></i>
                                          <div className="form__form-group-label max-w-94p">{o.MenuText} </div>
                                        </a>
                                      </div>
                                    )
                                  })
                                  :
                                  <div className="form__form-group-label max-w-94p noQuickMenu text-center">
                                    <p>No Quick Menu Available</p>
                                  </div>
                              }
                            </div>
                            <h5 className="account__subhead subhead text-center curSystemIdication mt-20">Select the menu on the topbar <br />or Select quick menu above if available</h5>
                          </div>
                          <div className="systemBtnSec">
                            {curSystemId === obj.SystemId ? '' : <a className="btn btn-outline-primary btn-sm switchBtn" href={appDomainUrl + "/react/" + obj.SystemAbbr}>Go to {obj.SystemName}</a>}
                          </div>
                        </div>
                      </div>
                      {/* <div>
                  {curSystemId === obj.SystemId? '' : <a className="btn btn-outline-primary btn-lg switchBtn" href={appDomainUrl + "/react/" + obj.SystemAbbr}>Switch System</a>}
                </div> */}
                      <div className="alert__content text-center notificationSec">
                        <div>
                          <span> Swipe Left or Right to view different systems </span>
                        </div>
                        <div>
                          {/* <p>
                      <a className="switchSystem" onClick={this.showFullList}>CLick to view the full system list.</a>
                    </p> */}
                        </div>
                      </div>
                    </div>)
                })}
              </AwesomeSlider>
            </div>
          </div>
        }
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  global: state.global,
  system: (state.auth || {}).system,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
    { saveProfile: saveProfile },
    { setTitle: setTitle },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Default));

