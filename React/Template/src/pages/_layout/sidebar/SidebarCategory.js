import React, {PureComponent} from 'react';
import {Collapse} from 'reactstrap';
import PropTypes from 'prop-types';

export default class SidebarCategory extends PureComponent {
  static propTypes = {
    title: PropTypes.string.isRequired,
    icon: PropTypes.string
  };
  
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = {
      collapse: false
    };
  }
  
  toggle() {
    this.setState({collapse: !this.state.collapse});
  }
  
  render() {
    return (
      <div className={`sidebar__category-wrap${this.state.collapse ? ' sidebar__category-wrap--open' : ''}`}>
        <li className='sidebar__link sidebar__category' onClick={this.toggle}>
          {this.props.icon ? <span className={`sidebar__link-icon lnr lnr-${this.props.icon}`}/> : ''}
          <p className='sidebar__link-title'>{this.props.title}</p>
          <span className='sidebar__category-icon lnr lnr-chevron-right'/>
        </li>
        <Collapse isOpen={this.state.collapse} className='sidebar__submenu-wrap'>
          <ul className='sidebar__submenu'>
            <div>
              {this.props.children}
            </div>
          </ul>
        </Collapse>
      </div>
    )
  }
}