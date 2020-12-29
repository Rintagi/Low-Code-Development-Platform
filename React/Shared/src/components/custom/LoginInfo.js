import React, { Fragment, Component } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from 'reactstrap';
import { updateNotificationChannel } from '../../services/profileService';
import { showNotification } from '../../redux/Notification';
import { messaging, vapidKey, fcmSWUrl, fcmSWScope } from '../../app/FirebaseInit';
import { getMyAppSig, getMyMachine, getFingerPrint } from '../../helpers/config';
import * as fcmServiceWorker from '../../registerFCMServiceWorker';

let appSig = getMyAppSig();
let myMachine = getMyMachine();

function requestPermission() {
    console.log('Requesting permission...');
    Notification && Notification.requestPermission().then((permission) => {
        if (permission === 'granted') {
            console.log('Notification permission granted.');
            // TODO(developer): Retrieve a registration token for use with FCM.
        } else {
            console.log('Unable to get permission to notify.');
        }
    });
}

function setupFCM(component, usrId) {
    navigator.serviceWorker && window.Notification && messaging &&
    fcmServiceWorker.register({scope:fcmSWScope, serviceWorkJS:fcmSWUrl})
        //Promise.resolve('abcd')
        .then((registration) => {
            console.log(registration);
            console.log(window.location);
            // navigator.serviceWorker.controller.postMessage({
            registration && (registration.active || registration.waiting || registration.installing).postMessage({
                type: 'SET_LOGIN_USER',
                usrId: usrId,
                basePath: window.location.pathname + '#',
            });

            //Get registration token. Initially this makes a network call, once retrieved
            //subsequent calls to getToken will return from cache.
            registration && 
            (registration.active || registration.waiting || registration.installing) && 
            window.Notification && 
            Notification.permission === 'granted' 
            && messaging.getToken(
                {
                   vapidKey: vapidKey,
                   serviceWorkerRegistration: registration
                }
            ).then((currentToken) => {
                if (currentToken) {
                    console.log(`fcm token ${currentToken}`);
                    getFingerPrint().then(myMachine=>{
                        updateNotificationChannel(currentToken, myMachine, appSig, 'fcm');
                    });
                } 
                else {
                    // Show permission request.
                    //requestPermission();
                    console.log('No registration token available. Request permission to generate one.');
                    // Show permission UI.
                    //            updateUIForPushPermissionRequired();
                    //            setTokenSentToServer(false);
                }
            }).catch((err) => {
                console.log('An error occurred while retrieving token. ', err);
                //        showToken('Error retrieving registration token. ', err);
                //        setTokenSentToServer(false);
            });

            // fcm messaging, replaced by generic one in componentDidMount
            // messaging.onMessage((payload) => {
            //     // only work if window in focus or fcm notification 'clicked'
            //     console.log('Message received. ', payload);
            //     // ...
            // });
        })
        .catch(err => {

        })
}

class LoginInfo extends Component {
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
        // communication between service worker and front end NOT related to fcm(which is via the messaging.x interface)  
        // must use message.source or message.data.something to distinguish where it is coming from as there can be multiple 
        // service workers  
        navigator.serviceWorker && navigator.serviceWorker.addEventListener("message",
            (message) => {
                console.log('general message received', message);
                const data = message.data;
                if (message.data && message.data.data.title && window.Notification) {
                    this.props.showNotification("I", { message: data.data.body })
                    // const notification = new Notification(message.data.data.title, {
                    //     ...message.data.data,
                    // });

                    // notification.onclick = (event) => {
                    //     console.log(event);
                    //     event.preventDefault(); // prevent the browser from focusing the Notification's tab
                    //     window.open(message.data.data.click_action, '_blank');
                    //     notification.close();
                    // };
                    //no auto close !!!
                    //setTimeout(notification.close.bind(notification), 7000);
                }
            }
        );      
    }

    componentDidUpdate(prevProps, prevStates) {
        if (prevProps.user.UsrId !== this.props.user.UsrId) {
            console.log(this.props.user);
            navigator.serviceWorker && setupFCM(this, this.props.user.UsrId);
        }
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
                                <Link className='topbar__link' onClick={this.close} to='/default'>
                                    <DropdownItem className="topbar-dropdown-item">
                                        <span className='topbar__link-title'><i className='topbar-icon-small fa fa-folder mr-11 fill-fintrux'></i>{this.props.auth.Label.SystemsList}</span>
                                    </DropdownItem>
                                </Link>
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

const mapStateToProps = (state) => ({
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    {
      showNotification: showNotification,
    },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(LoginInfo);

