import React, {PureComponent} from 'react';
import Select from 'react-select';
import PropTypes from 'prop-types';

class SelectField extends PureComponent {
  
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
  }
  
  handleChange = (selectedOption) => {
    this.props.onChange(selectedOption);
  };
  
  render() {
    const {value, name} = this.props;
    
    return (
      <Select
        name={name}
        value={value}
        onChange={this.handleChange}
        options={this.props.options}
        clearable={false}
        className='form__form-group-select'
        placeholder={this.props.placeholder}
      />
    )
  }
}

const renderSelectField = (props) => (
  <div className='form__form-group-input-wrap'>
    <SelectField
      {...props.input}
      options={props.options}
    />
    {props.meta.touched && props.meta.error && <span className='form__form-group-error'>{props.meta.error}</span>}
  </div>
);

renderSelectField.propTypes = {
  input: PropTypes.object.isRequired,
  meta: PropTypes.object,
  options: PropTypes.array
};

export default renderSelectField;
