import 'rc-slider/assets/index.css';
import React, {PureComponent} from 'react';
import Slider from 'rc-slider';
import PropTypes from 'prop-types';

//To use Range Slider, call <Range min={0} max={1000} value={[350, 635]} tipFormatter={value => `$${value}`}/>

const createSliderWithTooltip = Slider.createSliderWithTooltip;
const Range = createSliderWithTooltip(Slider.Range);

export default class RangeTheme extends PureComponent {
  static propTypes = {
    marks: PropTypes.object,
    value: PropTypes.array,
    min: PropTypes.number,
    max: PropTypes.number,
    tipFormatter: PropTypes.func
  };
  
  render() {
    return (
      <div className='slider'>
        <div className='slider__min'>
          <p>{this.props.tipFormatter ? this.props.tipFormatter(this.props.min) : this.props.min}</p>
        </div>
        <div className='slider__max'>
          <p>{this.props.tipFormatter ? this.props.tipFormatter(this.props.max) : this.props.max}</p>
        </div>
        <Range min={this.props.min} max={this.props.max} defaultValue={this.props.value}
               tipFormatter={this.props.tipFormatter} marks={this.props.marks}
               tipProps={{visible: true}}/>
      </div>
    )
  }
}