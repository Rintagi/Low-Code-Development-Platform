import React, {PureComponent} from 'react';
import CheckIcon from 'mdi-react/CheckIcon';
import PropTypes from 'prop-types';
import RadioboxBlankIcon from 'mdi-react/RadioboxBlankIcon';

class RadioButtonField extends PureComponent {
  componentDidMount() {
    if (this.props.defaultChecked) {
      console.log(this.props);
      this.props.onChange(this.props.radioValue);
    }
  }

  onChange = () => {
    this.props.onChange(this.props.radioValue);
  };

  render() {
    const disabled = this.props.disabled;

    return (
      <label
        className={`radio-btn${disabled ? ' disabled' : ''}${this.props.class ? ` radio-btn--${this.props.class} margin-narrow` : ''}`}>
        <input className='radio-btn__radio' name={this.props.name} type='radio'
               onChange={this.onChange} checked={this.props.value === this.props.radioValue} disabled={disabled}/>
        <span className='radio-btn__radio-custom'/>
        <span className="radio-bg-solver"></span>
        {this.props.class === 'button' ?
          <span className='radio-btn__label-svg'>
              <CheckIcon className='radio-btn__label-check'/>
              <RadioboxBlankIcon className='radio-btn__label-uncheck'/>
            </span> : ''}
        <span className='radio-btn__label'>{this.props.label}</span>
        <span className='radio-btn__label__right'>{parseFloat(this.props.amount).toFixed(2)}</span>
      </label>
    )
  }
}

const renderRadioButtonField = (props) => (
  <RadioButtonField
    {...props.input}
    label={props.label}
    amount={props.amount}
    defaultChecked={props.defaultChecked}
    disabled={props.disabled}
    radioValue={props.radioValue}
    class={props.class}
  />
);

renderRadioButtonField.propTypes = {
  input: PropTypes.object.isRequired,
  label: PropTypes.string,
  // amount: PropTypes.number,
  amount: PropTypes.string,
  defaultChecked: PropTypes.bool,
  disabled: PropTypes.bool,
  radioValue: PropTypes.number,
  class: PropTypes.string
};

export default renderRadioButtonField;
