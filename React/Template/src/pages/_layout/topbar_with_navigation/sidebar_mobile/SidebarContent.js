import React, { PureComponent } from 'react';
import { NavLink, Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getDefaultPath } from '../../../../helpers/utils'
import { login, logout, getCurrentUser } from '../../../../redux/Auth';
import { pagesRoutes } from '../../../../app/Router.js';
import LoginInfo from '../../../../components/custom/LoginInfo'
import LogoutVariantIcon from 'mdi-react/LogoutVariantIcon';

class SidebarContent extends PureComponent {
  constructor(props) {
    super(props);

    this.handleLogout = this.handleLogout.bind(this);
  }

  handleLogout(event) {
    this.props.onClick();
    this.props.logout();
  }

  hideSidebar = () => {
    this.props.onClick();
  };

  render() {
    // const { } = this.props;
    return (
      <div className='sidebar__content'>
        <ul className='sidebar__block'>
          {/* {pagesRoutes
            .filter((v) => (v.inMenu))
            .map((obj, i) => {
              if (obj.redirect) {
                return null;
              } else {
                if (obj.short !== "Login") {
                  return (
                    <NavLink key={i} to={getDefaultPath(obj.path)} onClick={this.hideSidebar}>
                      <li className='sidebar__link'>
                        <i className={`fa fa-${obj.icon} sidebar__link-icon`}></i>
                        <p className='sidebar__link-title'>
                          {obj.menuLabel}
                        </p>
                      </li>
                    </NavLink>
                  );
                } else {
                  if (obj.short === "Login" && (!this.props.user || !this.props.user.UsrId)) {
                    return (
                      <NavLink key={i} to={obj.path} onClick={this.hideSidebar}>
                        <li className='sidebar__link'>
                          <i className={`fa fa-${obj.icon} sidebar__link-icon`}></i>
                          <p className='sidebar__link-title'>
                            {obj.menuLabel}
                          </p>
                        </li>
                      </NavLink>
                    );
                  } else {
                    return (
                      <NavLink key={i} to={obj.path} onClick={this.handleLogout}>
                        <li className='sidebar__link'>
                          <i className='fa fa-sign-out sidebar__link-icon'></i>
                          <p className='sidebar__link-title'>
                            {this.props.auth.Label.Logout} ({this.props.user.UsrName})
                          </p>
                        </li>
                      </NavLink>
                    );
                  }
                }
              }
            }
            )} */}
          {pagesRoutes
            .filter((v) => (v.inMenu))
            .map((obj, i) => {
              if (obj.redirect) {
                return null;
              } else {
                if (obj.short != "Login") {
                  if (obj.component === null) {
                    return (
                      <a key={i} className='topbar__nav-link' href={obj.path} target="_blank">
                        <li className='sidebar__link'>
                          <i className={`fa fa-${obj.icon} sidebar__link-icon`}></i>
                          <p className='sidebar__link-title'>
                            {obj.menuLabel}
                          </p>
                        </li>  
                      </a>
                    )
                  } else {
                    return (
                      <NavLink key={i} to={getDefaultPath(obj.path)} onClick={this.hideSidebar}>
                        <li className='sidebar__link'>
                          <i className={`fa fa-${obj.icon} sidebar__link-icon`}></i>
                          <p className='sidebar__link-title'>
                            {obj.menuLabel}
                          </p>
                        </li>
                      </NavLink>
                    );
                  }
                }
                else { return null; }
              }
            }
            )}

          {/* <LoginInfo {...this.props} /> */}

        </ul>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(SidebarContent);
