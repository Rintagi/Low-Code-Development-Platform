import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
import moment from 'moment';
import log from '../../helpers/logger';

export default class DatePickerField extends Component {
  constructor(props) {
    super(props);
    this.state = {
      startDate: this.props.selected ? moment(this.props.selected) : moment()
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleBlur = this.handleBlur.bind(this);
    this.handleFocus = this.handleFocus.bind(this);

  }

  handleChange(date) {
    this.setState({
      startDate: date
    });
    console.log(date);
    this.props.onChange(date);
  }

  handleFocus = (e) => {
    log.debug(e);
    e.target.setSelectionRange(0, e.target.value.length);
  }

  handleBlur = (e) => {
    if (typeof this.props.onBlur === "function") {
      this.props.onBlur(this.props.name, true);
    }
  };

  render() {

    return (
      <div className='date-picker'>
        <DatePicker
          name={this.props.name}
          className='form__form-group-datepicker'
          selected={this.state.startDate}
          onChange={this.handleChange}
          dateFormat='LL'
          // showTimeSelect
          onBlur={this.handleBlur}
          onFocus={this.handleFocus}
          disabled={this.props.disabled}
        />
      </div>
    )
  }
}
