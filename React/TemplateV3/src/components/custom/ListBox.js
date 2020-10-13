import React, { Component } from 'react';
import log from '../../helpers/logger'
import CheckIcon from 'mdi-react/CheckIcon';
export default class ListBox extends Component {
  constructor(props) {
    super(props);

    const selectedValue = ((this.props.value || '').replace('(', '').replace(')', '').split(",") || []).filter(o => (o)).reduce((a, o) => { a[o] = true; return a }, {});
    this.state = {
      options: (this.props.options || []).filter(o => o.value),
      selectedValue: selectedValue,
    }
  }

  hanldeCheck = (value => {
    return function (evt) {
      const curValues = { ...this.state.selectedValue };
      curValues[value] = !curValues[value];

      if (typeof this.props.onChange === 'function') {
        this.setState({
          selectedValue: curValues
        });
        const revisedValues = Object.keys(curValues).filter(o => (curValues[o])).map(o => (o)).join(",");
        this.props.onChange(this.props.name || (this.props.field || {}).name, revisedValues);
      } else {
        this.setState({
          selectedValue: curValues
        });
      }

    }.bind(this);

  }).bind(this);

  render() {
    const optionList = (this.props.options || []).filter(o => o.value);
    return (
      <div className='listboxSelection'>
        {optionList.map((obj, i) => (
          <div className='form__form-group' key={i}>
            <label className='checkbox-btn checkbox-btn--colored-click'>
              <input
                name={this.props.name + "_" + obj.value}
                value={obj.value}
                type="checkbox"
                listidx={i}
                keyid={obj.key}
                className='checkbox-btn__checkbox'
                disabled={obj.disabled}
                onClick={this.hanldeCheck(obj.value)}
                defaultChecked={this.state.selectedValue[obj.value]}
              />
              <span className='checkbox-btn__checkbox-custom'><CheckIcon /></span>
              <span className='checkbox-btn__label'>{obj.label}</span>
            </label>
          </div>))}
      </div>
    );
  };
};
