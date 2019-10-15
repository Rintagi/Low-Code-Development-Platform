import React, { Component } from 'react';
import { Route, Switch, Redirect, withRouter } from 'react-router-dom';
import MainWrapper from './MainWrapper';
import Layout from '../pages/_layout/Layout';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Notification from '../components/custom/Notification';
// Pages import
import Login from '../pages/login/Login';
import ForgetPassword from '../pages/login/ForgetPassword';
import Error from '../pages/error/Error';
import appRoutes from './route';

/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */

export const pagesRoutes = [
  {
    path: "/login",
    name: "Login",
    short: "Login",
    component: Login,
    icon: "sign-in",
    isPublic: true,
    inMenu: true,
    hideSidebar: true
  },
  {
    path: "/forget-password",
    name: "Forget Password",
    short: "ForgetPassword",
    component: ForgetPassword,
    icon: "unlock-alt",
    isPublic: true,
    isAdhoc: true,
  },
  {
    path: "/error",
    name: "Error",
    short: "Error",
    component: Error,
    icon: "sign-in",
    isPublic: true,
    isAdhoc: true,

  },
  ...appRoutes,
  {
    /* this one should always come last as it matches everything or use exactMatch in the router definiton */
    redirect: true,
    path: "/",
    pathTo: "/Default",
    name: "Default",
  },
];


const ProtectedRoute = ({ component: Component, isPublic, user, ...rest }) => {
  if ((user && user.UsrId) || isPublic) {
    const from = (((rest.location || {}).state || {}).from || {}).pathname;
    if (rest.path === '/login' && from && from !== rest.path && ((user && user.UsrId))) {
      //return (<Redirect to={rest.location.state.from.pathname} />)
    }
    return (<Route {...rest} render={(props) => (<Component {...props} />)} />)
  }
  else {
    return (<Route {...rest} render={(props) => (<Redirect
      to={
        {
          pathname: '/login',
          state: { from: props.location }
        }
      } />)} />)
  }
}

class Router extends Component {

  getFullList(){
    const menuList = (this.props.menu || {}).menuList;
    const myScreen = pagesRoutes.filter((v) => (v.inMenu)).reduce((a, o) => { a[o.screenId] = o; return a }, {});
    const myMenu = (menuList || []).filter((m) => (myScreen[m.ScreenId])).map((m) => ({ ...m, reactPath: (myScreen[m.ScreenId] || {}).path }));
    const myParents = (myMenu || {}).reduce((a, o) => {
      a[o.QId] = o;
      a[o.ParentQId] = o;
      a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 2]] = o;
      a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 3]] = o;
      return a
    }, {});
    
    return (menuList || []).filter(m => myParents[m.QId]).map((m) => ({ ...m, reactPath: (myScreen[m.ScreenId] || {}).path }));
  }

  render() {
    // const { } = this.props;
    const myFullList = this.getFullList();
    return (
      <MainWrapper>
        <main>
          <div id='container__wrap'>
            <Layout/>
            <Notification />
            <div className={'container__wrap ' + (myFullList.length > 0 ? "" : "no_sidebar")}>
              {/* Aaron Changes add container div below*/}
              <div className="dashboard container">
                <p className="project-title-desktop">{(this.props.global.pageTitle || '').substring(0, (this.props.global.pageTitle || '').indexOf('-') - 1)}</p>
                <Switch>
                  {pagesRoutes
                    .map((prop, key) => {
                      if (prop.redirect) {
                        return (
                          <Redirect from={prop.path} to={prop.pathTo} key={key} />
                        );
                      }
                      return (
                        <ProtectedRoute
                          key={key}
                          path={prop.path}
                          user={this.props.user}
                          component={prop.component}
                          isPublic={prop.isPublic}
                        />
                      );
                    }
                    )}
                </Switch>
              </div>
            </div>
          </div>
        </main>
      </MainWrapper>

    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  menu: (state.auth || {}).menu,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
  ), dispatch)
)

// export default compose(
//   connect(
//     mapStateToProps,
//     mapDispatchToProps
//   )
// )(Router);
//  export default Router;

// export default connect(
//        mapStateToProps,
//        mapDispatchToProps
//      )(Router);


export default withRouter(connect(
  mapStateToProps, mapDispatchToProps
)(Router));
