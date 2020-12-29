import React, {PureComponent} from 'react';
import {connect} from 'react-redux';
import { bindActionCreators } from 'redux';
import Scrollbar from 'react-smooth-scrollbar';
import {withRouter} from 'react-router';
import classNames from 'classnames';
import SidebarContent from './SidebarContent';
import {changeMobileSidebarVisibility, hideSidebar} from '../../../redux/SideBar';
import { pagesRoutes } from '../../../app/Router.js';
import log from '../../../helpers/logger';
import { login, logout, getCurrentUser } from '../../../redux/Auth';

class Sidebar extends PureComponent {
  
  constructor(props) {
    super(props);

    this.getFullList = this.getFullList.bind(this);
  }

  changeMobileSidebarVisibility = () => {
    this.props.dispatch(changeMobileSidebarVisibility());
  };

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
    let sidebarClass = classNames({
      'sidebar': true,
      'sidebar--show': this.props.sidebar.show,
      'sidebar--collapse': this.props.sidebar.collapse
    });

    const myFullList = this.getFullList();

    return (
      <div>
      {myFullList.length > 0  &&
        <div className={sidebarClass}>
          <div className='sidebar__back' onClick={this.changeMobileSidebarVisibility}/>
          <Scrollbar className='sidebar__scroll scroll'>
            <div className='sidebar__wrapper sidebar__wrapper--desktop'>
              <SidebarContent myFullList={myFullList} onClick={() => {}}/>
            </div>
            <div className='sidebar__wrapper sidebar__wrapper--mobile'>
              <SidebarContent myFullList={myFullList} onClick={this.changeMobileSidebarVisibility}/>
            </div>
          </Scrollbar>
        </div>    
    }
    </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  menu: (state.auth || {}).menu,
  sidebar: state.sidebar,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(Sidebar);