import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { getCurrentUser } from '../../redux/Auth';

import log from '../../helpers/logger';

class Error extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    log.debug("error page");
    return (<h4> Access denied </h4>)
  }
}

const mapStateToProps = (state) => ({
  user: (state.auth ||{}).user,
  auth: state.auth,
});

const mapDispatchToProps = (dispatch) =>(
  bindActionCreators(Object.assign({},
    { getCurrentUser: getCurrentUser },
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Error));
