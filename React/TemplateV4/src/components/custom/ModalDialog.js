import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import {Button, ButtonToolbar, Modal} from 'reactstrap';
import log from '../../helpers/logger';

class ModalDialog extends Component {
  constructor(props) {
    super(props);
    this.state = {
      // modal: false,
      backdrop: "static"
    };
    this.end = this.end.bind(this);
    this.continue = this.continue.bind(this);
  }
  
  end() {  
    if (this.props.onChange) this.props.onChange("N");
  }

  continue(){
    
    if (this.props.onChange) this.props.onChange("Y");
  }
  
  render() {
    let Icon; 
    switch (this.props.color) {
      case 'primary':
        Icon = <span className='lnr lnr-pushpin modal__title-icon'/>;
        break;
      case 'success':
        Icon = <span className='lnr lnr-thumbs-up modal__title-icon'/>;
        break;
      case 'warning':
        Icon = <span className='lnr lnr-flag modal__title-icon'/>;
        break;
      case 'error':
        Icon = <span className='lnr lnr-cross-circle modal__title-icon'/>;
        break;
      case 'danger':
        Icon = <span className='lnr lnr-warning modal__title-icon'/>;
        break;
      default:
        break;
    }
    
    return (
      <div>
        <Modal isOpen={this.props.ModalOpen} toggle={this.end} backdrop={this.state.backdrop}
               className={`modal-dialog--${this.props.color} ${this.props.colored ? 'modal-dialog--colored' : ''} ${this.props.header ? 'modal-dialog--header' : ''}`}>
          <div>{this.props.component}</div>
          <div className='modal__header'>
            <span className='lnr lnr-cross modal__close-btn' onClick={this.end}/>
            {this.props.header ? '' : Icon}
            <h4 className='bold-text  modal__title'>{this.props.title}</h4>
          </div>
          <div className='modal__body'>
            {this.props.message}
          </div>
          <ButtonToolbar className='modal__footer'>
            <Button onClick={this.end}>Cancel</Button>
            <Button outline={this.props.colored} color={this.props.color} onClick={this.continue}>Ok</Button>
          </ButtonToolbar>
        </Modal>
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
 
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
  ), dispatch)
)

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ModalDialog));