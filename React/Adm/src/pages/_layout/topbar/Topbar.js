import React, {PureComponent} from 'react';
import TopbarSidebarButton from './TopbarSidebarButton';
import {Link} from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { login, logout, getCurrentUser } from '../../../redux/Auth';
import LoginInfo from '../../../components/custom/LoginInfo'

class Topbar extends PureComponent {
  render() {
    return (
      <div className='topbar'>
        <div className='topbar__wrapper'>
          <div className='topbar__left'>
            <TopbarSidebarButton/>
            <Link className='topbar__logo' to='/login'>
              <img alt='' src={require('../../../img/logo.png')} />
            </Link>
          </div>
          <div className='topbar__right'>
            <LoginInfo {...this.props} />
          </div>
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  menu: (state.auth || {}).menu,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(Topbar);