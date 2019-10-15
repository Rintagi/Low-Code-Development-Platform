import React, { PureComponent } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import TopbarSidebarButton from './TopbarSidebarButton';
import { login, logout, getCurrentUser } from '../../../redux/Auth';
import { pagesRoutes } from '../../../app/Router.js';
import LoginInfo from '../../../components/custom/LoginInfo'
import { getDefaultPath } from '../../../helpers/utils'
import log from '../../../helpers/logger';
import { Collapse, Navbar, NavbarToggler, NavbarBrand, Nav, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
class TopbarWithNavigation extends PureComponent {

  constructor(props) {
    super(props);

    this.state = {
      CurMenu: {}
    };

    this.handleLogout = this.handleLogout.bind(this);
  }

  handleLogout(event) {
    this.props.logout();
  }

  // componentDidUpdate(prevProps, prevStates) {
  //   const menuList = (this.props.menu || {}).menuList;
  //   log.debug("full list", menuList);
  //   const myScreen = pagesRoutes.filter((v) => (v.inMenu)).reduce((a,o)=>{a[o.screenId] = o; return a},{});
  //   log.debug("my screen", myScreen);

  //   const myMenu = (menuList || []).filter((m) => (myScreen[m.ScreenId]));
  //   log.debug("my menu", myMenu);

  //   const myParents = (myMenu || {}).reduce((a,o)=>{ a[o.QId] = o; 
  //                                                    a[o.ParentQId] = o; 
  //                                                    a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 2]] = o; 
  //                                                    a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 3]] = o;
  //                                                    return a },{});
  //   log.debug("parents", myParents);
    
  //   const myFullList =(menuList || []).filter(m=>myParents[m.QId]);
  //   log.debug("full list", myFullList);
  // }

  render() {
    return (
      <div className='topbar topbar--navigation'>
        <div className='topbar__wrapper'>
          <div className='topbar__left'>
            <TopbarSidebarButton />
            <Link className='topbar__logo' to='/login'>
              <img alt='' src={require('../../../img/logo.png')} />
            </Link>
          </div>
          <div className='topbar__right'>
            <nav className='topbar__nav'>
              {/* {
                pagesRoutes
                .filter((v)=>(!v.isAdhoc))
                .map((prop, key) => {
                  if (prop.redirect) {
                    return null;
                  } else {
                    if (prop.short !== "Login") {
                      return (
                        <Link key={key} className='topbar__nav-link' to={prop.path}>
                          <i className={`fa fa-${prop.icon} fs-16 mr-2 fill-fintrux`}></i> {prop.name}</Link>
                      );
                    } else {return null}
                  }
                })
              } */}
              {
                pagesRoutes
                  .filter((v) => (v.inMenu))
                  .map((obj, i) => {
                    if (obj.short != "Login") {
                      if (obj.component === null) {
                        return (
                          <a key={i} className='topbar__nav-link' href={obj.path} target="_blank">
                            {/* <i className={`fa fa-${obj.icon} fs-16 mr-2`}></i> */}
                            {obj.menuLabel}
                          </a>
                        )
                      } else {
                        return (
                          <Link key={i} className='topbar__nav-link' to={getDefaultPath(obj.path)}>
                            {/* <i className={`fa fa-${obj.icon} fs-16 mr-2`}></i> */}
                            {obj.menuLabel}
                          </Link>
                        )
                      }
                    }
                    else return null;
                  })
              }
            </nav>

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

export default connect(mapStateToProps, mapDispatchToProps)(TopbarWithNavigation);
