import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Redirect, withRouter } from 'react-router-dom';
import LoadingIcon from 'mdi-react/LoadingIcon';
import { login, logout, getCurrentUser, saveProfile, ShowSpinner } from '../../redux/Auth';
import { setTitle, setSpinner } from '../../redux/Global';


class Default extends Component {
  constructor(props) {
    super(props);
    this.props.setSpinner(false);
  }

  render() {
    return (
      <div>
      <div className='account'>
        <div className='account__wrapper'>
          <div className='account__card shadow-box rad-4'>
            {/* {this.props.user.loading && <div className='panel__refresh'><LoadingIcon /></div>} */}
            <div className='account__head'>
              <h1 className='account__title fill-fintrux text-center'>Welcome</h1>       
            </div>
            <div>
              <h3 className='account__subhead subhead text-center'>Please select menu items on the left to get started.</h3>
            </div>
            <div className='empty-block'>
              <img alt='' src={require('../../img/default.png')} />
            </div>
          </div>
        </div>
      </div>
    </div>
    )
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
    { logout: logout },
    { getCurrentUser: getCurrentUser },
    { saveProfile: saveProfile },
    { setTitle: setTitle },
    { setSpinner: setSpinner },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Default));

