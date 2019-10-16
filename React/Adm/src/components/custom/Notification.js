import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import Alert from 'react-s-alert';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { clearNotification } from '../../redux/Notification'
import 'react-s-alert/dist/s-alert-default.css';
import 'react-s-alert/dist/s-alert-css-effects/slide.css';
import 'react-s-alert/dist/s-alert-css-effects/scale.css';
import 'react-s-alert/dist/s-alert-css-effects/flip.css';
import 'react-s-alert/dist/s-alert-css-effects/jelly.css';
import 'react-s-alert/dist/s-alert-css-effects/stackslide.css';
import 'react-s-alert/dist/s-alert-css-effects/genie.css';
import 'react-s-alert/dist/s-alert-css-effects/bouncyflip.css';
import log from '../../helpers/logger';


// export default class Notification extends Component {
class Notification extends Component {

  componentDidUpdate(prevprops, prevstates) {
    this.props.clearNotification();
  }

  render() {
    if ((this.props.notification || {}).message) {
      // alert(this.props.notification.message);
      if (this.props.notification.msgType === "E") {
        log.debug(this.props.notification.message);
        Alert.error(this.props.notification.message);
        // Alert.error(this.props.notification.message,{
        //   timeout: this.props.notification.timeout || "none"
        // });
      } else {
        Alert.success(this.props.notification.message);
        // Alert.success(this.props.notification.message,{
        //   timeout: this.props.notification.timeout || "none"
        // });
      }
    }
    return (
      <Alert
        stack={{ limit: 1 }}
        html={true}
        position="bottom"
        effect='stackslide'
        timeout={5000}
        offset={0}
      />
    )
  }

}

const mapStateToProps = (state) => ({
  notification: state.notification,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { clearNotification: clearNotification }
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Notification));

