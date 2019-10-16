import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
import moment from 'moment';
import log from '../../helpers/logger';

export default class DatePickerField extends Component {
  constructor(props) {
    super(props);
    this.state = {
      // startDate: this.props.selected ? moment(this.props.selected) : moment()
      startDate: this.props.selected ? moment(this.props.selected) : null,
      isOpen: false,
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleBlur = this.handleBlur.bind(this);
    this.toggleCalendar = this.toggleCalendar.bind(this);
    this.handleOutsideClick = this.handleOutsideClick.bind(this);
  }

  handleChange(date) {
    if (!this.props.disabled) {
      this.setState({
        startDate: date
      });
      this.toggleCalendar();
      this.props.onChange(date, this.props.name || (this.props.field || {}).name, { fieldname: this.props.fieldname, listidx: this.props.listidx, fieldpath: this.props.fieldpath });
    }
  }

  toggleCalendar = (e) => {
    e && e.preventDefault();
    if (!this.props.disabled) {
      this.setState({ isOpen: !this.state.isOpen });
    }
  };

  handleOutsideClick = (e) => {
    e && e.preventDefault();
    if (!this.props.disabled) {
      this.setState({ isOpen: false });
    }
  }

  handleBlur = (e) => {
    if (!this.props.disabled) {
      if (typeof this.props.onBlur === "function") {
        this.props.onBlur(this.props.name || (this.props.field || {}).name, true, { fieldname: this.props.fieldname, listidx: this.props.listidx, fieldpath: this.props.fieldpath });
      }
    }
  };

  render() {

    //log.debug(this.props.value);
    const isUTC = this.props.isUTC;
    //const contentHasTZInfo = /z$/i.test(this.props.value) || /[\-\+0-9\:]$/i.test(this.props.value);
    return (
      <div className='date-picker'>
        <button
          className={`btn-as-input pointer ${this.props.disabled && 'btn-as-input-disabled'}`}
          onClick={this.toggleCalendar}>
          {(this.props.value === '' || this.props.value === undefined) ? '' : moment(this.props.value, isUTC ? moment.ISO_8601 : undefined).format('MMMM D, YYYY')}
        </button>
        {this.state.isOpen &&
          <DatePicker
            // customInput={<CustomInput />}
            name={this.props.name || (this.props.field || {}).name}
            className='form__form-group-datepicker'
            selected={this.state.startDate}
            onChange={this.handleChange}
            dateFormat='LL'
            onClickOutside={this.handleOutsideClick}
            // showTimeSelect
            onBlur={this.handleBlur}
            showMonthDropdown
            showYearDropdown
            dropdownMode="select"
            autoComplete='off'
            withPortal
            disabled={this.props.disabled}
            fixedHeight
            // readOnly="readonly"
            // readOnly={true}
            // minDate={moment().subtract(12, 'M')}
            // maxDate={moment().subtract(21, 'Y')}
            minDate={this.props.minDate}
            maxDate={this.props.maxDate}
            showDisabledMonthNavigation={this.props.showDisabledMonthNavigation}
            inline
          />
        }
      </div>
    )
  }
}

