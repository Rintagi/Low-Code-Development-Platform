import React, { Fragment, Component } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from 'reactstrap';

export default class LoginInfo extends Component {
    constructor(props) {
        super(props);

        this.state = {
            dropdownOpen: false
        };

        this.handleLogout = this.handleLogout.bind(this);
        this.toggle = this.toggle.bind(this);
        this.close = this.close.bind(this);
    }

    handleLogout(event) {
        this.props.logout();
    }

    toggle() {
        this.setState(prevState => ({
            dropdownOpen: !prevState.dropdownOpen
        }));
    }

    close() {
        this.setState(prevState => ({
            dropdownOpen: false
        }));
    }

    componentDidMount() {
    }


    render() {
        const isLoggedin = (this.props.user && this.props.user.UsrId);

        return (
            <div>
                {
                    isLoggedin ? (
                        <Dropdown className='topbar__nav-link pl-0' isOpen={this.state.dropdownOpen} toggle={this.toggle}>
                            <DropdownToggle className="topbar__avatar mb-0">
                                <i className="fa fa-user fs-16 mr-2 mt-3"></i>
                                <p className="userName">{this.props.user.UsrName}</p>
                                <i className="fa fa-chevron-down topbar-chevron ml-7"></i>
                            </DropdownToggle>
                            <DropdownMenu className='topbar__menu dropdown__menu'>
                                <Link className='topbar__link' onClick={this.close} to='/profile'>
                                    <DropdownItem className="topbar-dropdown-item">
                                        <span className='topbar__link-title'><i className='topbar-icon-small fa fa-address-card-o mr-11 fill-fintrux'></i>{this.props.auth.Label.Profile}</span>
                                    </DropdownItem>
                                </Link>
                                <Link className='topbar__link' onClick={this.close} to='/setting'>
                                    <DropdownItem className="topbar-dropdown-item">
                                        <span className='topbar__link-title'><i className='topbar-icon-small fa fa-cogs mr-11 fill-fintrux'></i>{this.props.auth.Label.Settings}</span>
                                    </DropdownItem>
                                </Link>
                                <Link className='topbar__link' onClick={this.close} to='/newpassword'>
                                    <DropdownItem className="topbar-dropdown-item">
                                        <span className='topbar__link-title'><i className='topbar-icon-small fa fa-unlock-alt mr-11 fill-fintrux'></i>{this.props.auth.Label.NewPassword}</span>
                                    </DropdownItem>
                                </Link>
                                <Link className='topbar__link' onClick={this.handleLogout} to='/login'>
                                    <DropdownItem className="topbar-dropdown-item">
                                        <span className='topbar__link-title'><i className='topbar-icon-small fa fa-sign-out mr-11 fill-fintrux'></i>{this.props.auth.Label.Logout}</span>
                                    </DropdownItem>
                                </Link>
                            </DropdownMenu>
                        </Dropdown>
                    ) : (
                            <Link className='topbar__nav-link' to='/login'>
                                <i className={`fa fa-sign-in fs-16 mr-2`}></i>{this.props.auth.Label.Login}
                            </Link>
                        )
                }
            </div>

        )
    }

}


