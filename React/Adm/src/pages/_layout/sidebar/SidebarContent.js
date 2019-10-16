import React, { PureComponent } from 'react';
import { bindActionCreators } from 'redux';
import SidebarLink from './SidebarLink';
import SidebarCategory from './SidebarCategory';
import { connect } from 'react-redux';
import log from '../../../helpers/logger';
import { pagesRoutes } from '../../../app/Router.js';
import { getDefaultPath } from '../../../helpers/utils'
import { login, logout, getCurrentUser } from '../../../redux/Auth';

class SidebarContent extends PureComponent {

  constructor(props) {
    super(props);

    this.buildTree = this.buildTree.bind(this);
  }

  hideSidebar = () => {
    this.props.onClick();
  };

  buildTree(menuList, parentid) {
    return menuList.filter((obj) => (!parentid || obj.ParentId == parentid)).map((obj, i) => {
          if (obj.ParentId == parentid) {
            var dl = "";
            var childStr = this.buildTree(menuList, obj.MenuId);
            if (childStr.length > 0) {
                if(obj.ParentId === ""){
                  return (<SidebarCategory key={i} title={obj.MenuText} icon='menu'> {childStr}</SidebarCategory>);
                }else{
                  return (<SidebarCategory key={i} title={obj.MenuText}> {childStr}</SidebarCategory>);
                }
            } else {
                dl = <SidebarLink key={i} title={obj.MenuText} route={obj.reactPath ? getDefaultPath(obj.reactPath) : null} />;
                return (dl);
            }
          }    
          return ''
    })
  }

  render() {
    // const menuList = (this.props.menu || {}).menuList;
    // const myScreen = pagesRoutes.filter((v) => (v.inMenu)).reduce((a, o) => { a[o.screenId] = o; return a }, {});
    // const myMenu = (menuList || []).filter((m) => (myScreen[m.ScreenId])).map((m) => ({ ...m, reactPath: (myScreen[m.ScreenId] || {}).path }));
    // const myParents = (myMenu || {}).reduce((a, o) => {
    //   a[o.QId] = o;
    //   a[o.ParentQId] = o;
    //   a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 2]] = o;
    //   a[o.ParentQId.split(".")[o.ParentQId.split(".").length - 3]] = o;
    //   return a
    // }, {});
    // const myFullList = (menuList || []).filter(m => myParents[m.QId]).map((m) => ({ ...m, reactPath: (myScreen[m.ScreenId] || {}).path }));

    // log.debug("fullList2", myFullList);

    const myFullList = this.props.myFullList;
    return (
      <div className='sidebar__content'>
        <ul className='sidebar__block'>
          <div>
          {
            myFullList.length > 0 ? this.buildTree(myFullList, '') : ''
          }
          </div>

          {/* <SidebarCategory title='First Level Item' icon='menu'>
            <SidebarLink title='Second Level Item' route="" />
            <SidebarLink title='Second Level Item' route=""/>
            <SidebarCategory title='Second Level Item'>
              <SidebarLink title='Third Level Item' />
              <SidebarLink title='Third Level Item' />
            </SidebarCategory>
          </SidebarCategory> */}
        </ul>
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

export default connect(mapStateToProps, mapDispatchToProps)(SidebarContent);