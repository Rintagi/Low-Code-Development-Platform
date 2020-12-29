import React, {PureComponent} from 'react';
import PropTypes from 'prop-types';
import {Badge} from 'reactstrap';
import {NavLink, withRouter} from 'react-router-dom';
import {connect} from 'react-redux';
import {bindActionCreators } from 'redux';
import {hideMobileSidebarVisibility} from '../../../redux/SideBar';

class SidebarLink extends PureComponent {
  static propTypes = {
    title: PropTypes.string.isRequired,
    icon: PropTypes.string,
    new: PropTypes.bool,
    route: PropTypes.string
  };
  
  hideMobileSidebarVisibility = () => {
    this.props.hideMobileSidebarVisibility();
    // this.props.onClick();
  };

  render() {
    return (
      // <NavLink to={this.props.route ? this.props.route : '/'} onClick={this.props.onClick}
      <NavLink to={this.props.route ? this.props.route : '/'} onClick={this.hideMobileSidebarVisibility}
               activeClassName='sidebar__link-active'>
        <li className='sidebar__link'>
          {this.props.icon ? <span className={`sidebar__link-icon lnr lnr-${this.props.icon}`}/> : ''}
          <p className='sidebar__link-title'>
            {this.props.title}
            {this.props.new ? <Badge className='sidebar__link-badge'><span>New</span></Badge> : ''}
          </p>
        </li>
      </NavLink>
    )
  }
}

// export default withRouter(SidebarLink);
const mapStateToProps = (state) => ({

});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},

    { hideMobileSidebarVisibility: hideMobileSidebarVisibility},
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(SidebarLink);