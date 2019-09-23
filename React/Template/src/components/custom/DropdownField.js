import React, { Component } from 'react';
import Dropdown from 'react-dropdown';

export default class DropdownField extends Component {
  constructor(props) {
    super(props);
  }

  handleChange = value => {
    // this is going to call setFieldValue and manually update values.this.props.name
    this.props.onChange(this.props.name, value);
  };

  render() {
    return (
      <div className='form__form-group-input-wrap'>
        <Dropdown
          name={this.props.name}
          options={this.props.options}
          value={this.props.value}
          onChange={this.handleChange}
          className='form__form-group-select'
          placeholder={this.props.placeholder}
          disabled = {this.props.disabled}
        />
      </div>
    );
  };
};
