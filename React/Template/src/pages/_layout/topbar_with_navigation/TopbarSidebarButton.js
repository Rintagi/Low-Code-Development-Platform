import React, {PureComponent} from 'react';
import MenuIcon from 'mdi-react/MenuIcon';
import {changeMobileSidebarVisibility} from '../../../redux/SideBar';
import {connect} from 'react-redux';

class TopbarSidebarButton extends PureComponent {
  
  changeMobileSidebarVisibility = () => {
    this.props.dispatch(changeMobileSidebarVisibility());
  };
  
  render() {
    return (
      <div>
        <button className='topbar__button topbar__button--mobile' onClick={this.changeMobileSidebarVisibility}>
          <MenuIcon className='topbar__button-icon'/>
        </button>
      </div>
    )
  }
}

export default connect(state => {
  return {sidebar: state.sidebar};
})(TopbarSidebarButton);
