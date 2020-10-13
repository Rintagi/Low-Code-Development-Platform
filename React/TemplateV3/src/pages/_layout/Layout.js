import React, {PureComponent} from 'react';
import {connect} from 'react-redux';
import TopbarWithNavigation from './topbar_with_navigation/TopbarWithNavigation';
import SidebarMobile from './topbar_with_navigation/sidebar_mobile/SidebarMobile';
import Topbar from './topbar/Topbar';
import Sidebar from './sidebar/Sidebar';

class Layout extends PureComponent {
  render() {
    return (
      <div>
        {/* <TopbarWithNavigation/>
        <SidebarMobile/> */}
        {/* Aaron changes */}
        <Topbar/>
        <Sidebar/>
      </div>
    )
  }
}

export default connect()(Layout);
