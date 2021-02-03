import React, { Component } from 'react';
import ReactDOM from 'react-dom'
import SignaturePad from 'react-signature-canvas'
import log from '../../helpers/logger'

export default class SignaturePanel extends Component {
  constructor(props) {
    super(props);
  }

  state = {trimmedDataURL: this.props.src || null}
  sigPad = {}

  componentDidUpdate() {
    this.sigPad.fromDataURL(this.props.src || null);
  }

  clear = (e) => {
    this.sigPad.clear();
    this.trim();
    // this.setState({trimmedDataURL: null});

    e.preventDefault();
  }
  
  trim = () => {
    if (typeof this.props.onChange === "function") {
      
      // this.setState({trimmedDataURL: this.sigPad.toDataURL('image/png')});
      
      const revisedValues =  this.sigPad.toDataURL('image/png');
      this.props.onChange(this.props.name || (this.props.field || {}).name, revisedValues);
    }
  }

  render () {
    // let {trimmedDataURL} = this.state;

    return <div className='sigOutterContainer'>
      <div className='sigContainer'>
        <SignaturePad canvasProps={{className: 'sigPad'}}
          ref={(ref) => { this.sigPad = ref }}
          onEnd = {this.trim}
          />
      </div>
      <div>
        <button className='sigButtons' onClick={this.clear}>
          Clear
        </button>
      </div>
      {/* {trimmedDataURL
        ? <img className='sigImage'
          src={trimmedDataURL} />
        : null} */}
    </div>
  }
};
