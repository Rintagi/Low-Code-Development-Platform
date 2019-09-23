import React, {PureComponent} from 'react';
import {Alert, Button} from 'reactstrap';
import PropTypes from 'prop-types';
import InformationOutlineIcon from 'mdi-react/InformationOutlineIcon';
import ThumbUpOutlineIcon from 'mdi-react/ThumbUpOutlineIcon';
import CommentAlertOutlineIcon from 'mdi-react/CommentAlertOutlineIcon';
import CloseCircleOutlineIcon from 'mdi-react/CloseCircleOutlineIcon';

export default class AlertComponent extends PureComponent {
  static propTypes = {
    color: PropTypes.string,
    icon: PropTypes.bool,
    className: PropTypes.string
  };
  
  constructor(props) {
    super(props);
    
    this.state = {
      visible: true
    };
    
    this.onShow = this.onShow.bind(this);
    this.onDismiss = this.onDismiss.bind(this);
  }
  
  onShow() {
    this.setState({visible: true});
  }
  
  onDismiss() {
    this.setState({visible: false});
  }
  
  render() {
    let Icon;
    
    switch (this.props.color) {
      case 'info':
        Icon = <InformationOutlineIcon/>;
        break;
      case 'success':
        Icon = <ThumbUpOutlineIcon/>;
        break;
      case 'warning':
        Icon = <CommentAlertOutlineIcon/>;
        break;
      case 'danger':
        Icon = <CloseCircleOutlineIcon/>;
        break;
      default:
        break;
    }
    
    if (this.state.visible) {
      return (
        <Alert color={this.props.color} className={this.props.className} isOpen={this.state.visible}>
          {this.props.icon && <div className='alert__icon'>{Icon}</div>}
          
          <div className='alert__content'>
            {this.props.children}
          </div>
        </Alert>
      );
    }
    
    return <Button onClick={this.onShow}>Show Alert</Button>;
  }
}