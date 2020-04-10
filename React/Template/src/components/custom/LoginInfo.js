import React, { Fragment, Component } from 'react';
import { Link } from 'react-router-dom';
import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from 'reactstrap';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { getCurrentUser } from '../../redux/Auth'
import classNames from 'classnames';
import log from '../../helpers/logger';
import { getCurrentReactUrlSearch, getCurrentReactUrlPath } from '../../helpers/domutils';

class LoginInfo extends Component {
  constructor(props) {
    super(props);

    this.state = {
      dropdownOpen: false,
      menuOpen: false,
    };

    this.handleLogout = this.handleLogout.bind(this);
    this.toggle = this.toggle.bind(this);
    this.moveTo = this.moveTo.bind(this);
    this.saveAndExit = this.saveAndExit.bind(this);
    this.TopbarRight = this.TopbarRight.bind(this);

    if (!this.props.user || !this.props.user.UsrId) {
      log.debug('load current login user info')
      this.props.getCurrentUser(true);
    }
  }

  handleLogout(event) {
    this.props.logout();
  }

  toggle() {
    this.setState(prevState => ({
      dropdownOpen: !prevState.dropdownOpen
    }));
  }

  moveTo(path) {
    switch (path) {
      case 'settings':
        this.props.history.push('/settings');
        break;
      case 'logout':
        this.props.logout();
        break;
      default:
        break;
    }
    this.setState(prevState => ({
      dropdownOpen: false
    }));
  }

  componentDidMount() {
  }

  saveAndExit() {
    const saveAndExit = (this.props.global || {}).SaveAndExit || {};
    const callback = saveAndExit.callback;
    if (typeof (callback) === 'function' && saveAndExit.pathname && getCurrentReactUrlPath().match(new RegExp(saveAndExit.pathname + '$', 'i'))) {
      callback();
    }
  }

  TopbarRight() {
    const TopbarRight = (this.props.global || {}).TopbarRight || {};
    const callback = TopbarRight.callback;
    if (typeof (callback) === 'function' && TopbarRight.pathname && getCurrentReactUrlPath().match(new RegExp(TopbarRight.pathname + '$', 'i'))) {
      callback();
    }
  }

  render() {
    const isLoggedin = (this.props.user && this.props.user.UsrId);
    const saveAndExit = (this.props.global || {}).SaveAndExit || {};
    const TopbarRight = (this.props.global || {}).TopbarRight || {};
    const hasSaveAndExit = typeof (saveAndExit.callback) === 'function' && saveAndExit.pathname && getCurrentReactUrlPath().match(new RegExp(saveAndExit.pathname + '$', 'i'));
    const hasTopbarRight = TopbarRight.pathname && getCurrentReactUrlPath().match(new RegExp(TopbarRight.pathname + '$', 'i'));
    // log.debug(this.props);

    return (
      <div>
        {
          isLoggedin ? (
            <div>
              <a className='topbar__nav-link pointer d-inline-block' onClick={this.saveAndExit}>
                {hasSaveAndExit && (saveAndExit.label || 'Default Exit')}
              </a>
              {/* <Dropdown className='topbar__nav-link pl-0 d-inline-block' isOpen={this.state.dropdownOpen} toggle={this.toggle}>
                <DropdownToggle className="topbar__avatar mb-0">
                  <i className="fa fa-user fs-16 mr-5 mt-3"></i>
                  <p className="userName">{this.props.user.UsrName}</p>
                  <i className="fa fa-chevron-down topbar-chevron ml-7"></i>
                </DropdownToggle>
                <DropdownMenu className='topbar__menu dropdown__menu'>
                  <span className='topbar__link' onClick={() => { this.moveTo('settings') }}>
                    <DropdownItem className="topbar-dropdown-item">
                      <span className='topbar__link-title'><i className='topbar-icon-small fa fa-address-card-o mr-11 color-green'></i>{this.props.auth.Label.Settings}</span>
                    </DropdownItem>
                  </span>
                  <span className='topbar__link' onClick={() => { this.moveTo('logout') }}>
                    <DropdownItem className="topbar-dropdown-item">
                      <span className='topbar__link-title'><i className='topbar-icon-small fa fa-sign-out mr-11 color-green'></i>{this.props.auth.Label.Logout}</span>
                    </DropdownItem>
                  </span>
                </DropdownMenu>
              </Dropdown> */}
              {hasTopbarRight &&
                <a className='topbar__nav-link pointer d-inline-block' onClick={this.TopbarRight}>
                  <i className="mdi mdi-menu fs-24 lh-23 color-green"></i>
                </a>
              }
            </div>
          ) : (
              saveAndExit.visibility === true &&
              <a className='topbar__nav-link pointer' onClick={this.saveAndExit}>
                {hasSaveAndExit && (saveAndExit.label || 'Default Exit')}
              </a>
            )
        }
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  error: state.error,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { getCurrentUser: getCurrentUser },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(LoginInfo));