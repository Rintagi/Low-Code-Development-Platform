import React, { Component } from 'react';
import Dropdown from 'react-dropdown';
import log from '../../helpers/logger'
export default class DropdownField extends Component {
  constructor(props) {
    super(props);
  }

  handleChange = (value => {
    // this is going to call setFieldValue and manually update values.this.props.name
    if (typeof this.props.onChange === "function")
      this.props.onChange(this.props.name || (this.props.field || {}).name, value, { fieldname: this.props.fieldname, listidx: this.props.listidx, fieldpath: this.props.fieldpath });
    // simulate formik validation routine  
    // if (typeof this.props.validate === "function")
    //   this.props.validate(value);
  }).bind(this);

  render() {
    return (
      // <div className='form__form-group-input-wrap'>
      <Dropdown
        name={this.props.name || (this.props.field || {}).name}
        options={this.props.options}
        value={this.props.value}
        onChange={this.handleChange}
        className={`form__form-group-select ${this.props.className}`}
        placeholder={this.props.placeholder}
        disabled={this.props.disabled}
      />
      // </div>
    );
  };
};
