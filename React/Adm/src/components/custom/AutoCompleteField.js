import React, { Fragment, Component } from 'react';
import { Typeahead, TypeaheadMenu, Menu,MenuItem } from 'react-bootstrap-typeahead';
import log from '../../helpers/logger';
export default class AutoCompleteField extends Component {
  
  constructor(props) {
    super(props);
    this.menuVisible = false; // can't use state as it is not reflected UNTIL next render, use instance variable instead
    this.isEnter = false;
    this.state = {
      lastSelectedValue:this.props.defaultSelected
    }
  }
  renderMenu=(results, menuProps) => {
    this.currentMatches = results;
    return (<TypeaheadMenu {...menuProps} options={results} />);    
    /* this are for more advanced customization say showing multiple columns like icon/photo etc., not needed for now
    return (
    <Menu {...menuProps}>
      {results.map((result, index) => (
        <MenuItem option={result} position={index}>
          {result.label}
        </MenuItem>
      ))}
    </Menu>
    )
    */
  }
  // handleFocus = (...rest)=>{
  //   this.hasFocus = true;
  // }
  handleFocus = (event) => {
    this.hasFocus = true;
    // event.preventDefault();
    // const target = event.target;
    // setTimeout(target.select.bind(target), 0);
  }
  handleKeyDown = (inputChar,...rest) =>{
    const value = inputChar.target.value;
    this.isEnter = inputChar.keyCode === 13;
    this.isTab = inputChar.keyCode === 9;
    this.isEscape = inputChar.keyCode === 27;
    this.isEmpty = !value;
    if (this.isEnter || this.isTab || this.isEscape) {
      const pickFirst = (this.currentMatches || []).length > 0 && value && this.props.pickFirst;
      if (typeof this.props.onChange === "function") {
        if (pickFirst) this.props.onChange(this.props.name || (this.props.field || {}).name,[this.currentMatches[0]], {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
        else if (this.isEmpty) this.props.onChange(this.props.name || (this.props.field || {}).name,[{}], {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
        else if (this.isEnter || this.isEscape ) {
          const instance = this.typeahead.getInstance();
          const priorValue = (this.currentValue || this.state.lastSelectedValue || this.props.defaultSelected || [null])[0] ;
          const priorSelected = priorValue || this.props.value || {};
          const priorLabel = priorSelected.label;
          if (priorLabel) {
            instance.setState({text:priorLabel});
            instance._updateSelected([priorSelected]);
            if (this.isEnter && false) this.props.onChange(this.props.name || (this.props.field || {}).name, [priorSelected], {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
            //instance.blur();
            instance.focus();
            instance._hideMenu(); // not public interface
          }
        }

      }
    }
  }
  handleMenuShow = (...rest) => {
    this.menuVisible = true;
  }
  handleMenuHide = (...rest) => {
    this.menuVisible = false;
  }
  handlePaginate = () =>{
    const _this = this;
    return function(e, ...rest) {
      if (typeof _this.props.onPaginate === "function") {
        const instance = (_this.typeahead && _this.typeahead.getInstance()) || {};
        const shownResults = (instance.state || {}).shownResults;
        const value = (instance.state || {}).text;
        _this.props.onPaginate(_this.props.name || (_this.props.field || {}).name, value, shownResults , {fieldname:_this.props.fieldname,listidx:_this.props.listidx, fieldpath:_this.props.fieldpath});
      }
    }
  }
  handleChange = (value,...rest) => {
    this.currentValue = value;
    if (this.typeahead) {
      const instance = this.typeahead.getInstance();
      if (!instance.getInput().value) {
        this.handleInputChange("");
        if (!this.hasFocus) {
          if (typeof this.props.onBlur === "function") this.props.onBlur(this.props.name || (this.props.field || {}).name, true);
          instance.focus();
          setTimeout(()=>{instance._hideMenu();},0); // not public interface
        }
        else {
          //instance.blur();
          //instance.focus();
        }
      } 
    }
    this.setState({lastSelectedValue:value});
    // this is going to call setFieldValue and manually update values.this.props.name
    if (typeof this.props.onChange === "function" 
      && !this.menuVisible // a selection is made
      && (value.length > 0 || true) // empty would not trigger onchange
    ) {
      this.props.onChange(this.props.name || (this.props.field || {}).name, value, {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
    }
  };

  handleBlur = (e) => {
    this.hasFocus = false;
    // this is going to call setFieldTouched and manually update touched.this.props.name
    const value = e.target.value;
    const pickFirst = (this.currentMatches || []).length > 0 && value && this.props.pickFirst;
     if (typeof this.props.onBlur === "function") {
      this.props.onBlur(this.props.name || (this.props.field || {}).name, true, {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
    }
    else {
      if (value && typeof this.props.onChange === "function" && pickFirst) {
        this.props.onChange(this.props.name || (this.props.field || {}).name,[this.currentMatches[0]], {listidx:this.props.listidx, fieldpath:this.props.fieldpath});
      }
    }
  };

  handleInputChange = (value,...rest) => {
    // this is going to call setFieldValue and manually update values.this.props.name
    if (typeof this.props.onInputChange === "function") this.props.onInputChange(this.props.name || (this.props.field || {}).name, value, {fieldname:this.props.fieldname,listidx:this.props.listidx, fieldpath:this.props.fieldpath});
  };

  filterBy = (option,props)=>{
    /* customeized client side filtering */
    const item = option;
    const itemText = option.label;
    const searchStr = props.text;
    return true;
  }

  render() {
    return (
      <div className='form__form-group-input-wrap'>
        <Fragment>
          <Typeahead
            ref={(typeahead) => this.typeahead = typeahead}
            renderMenu={this.renderMenu}
            onPaginate={this.handlePaginate()}
            //options={range(0, 1000).map((o) => o.toString())}
            options={this.props.options}
            paginate={true}
            placeholder={this.props.placeholder}
            maxResults = {20}
            flip = {false}
            bsSize= {"sm"}
            defaultInputValue = {""}
            delay = {500}
            value={this.props.value}
            onChange={this.handleChange}
            onBlur={this.handleBlur}
            onSelect={this.handleChange}
            onKeyDown={this.handleKeyDown}
            onFocus={this.handleFocus}
            name={this.props.name || (this.props.field || {}).name}
            onInputChange = {this.handleInputChange}
            defaultSelected = {this.props.defaultSelected}
            onMenuShow={this.handleMenuShow}
            onMenuHide={this.handleMenuHide}
            clearButton = {true}
            disabled={this.props.disabled}
            filterBy = {this.props.filterBy}
            renderMenuItemChildren={this.props.renderMenuItemChildren}
          />
        </Fragment>
      </div>
    );
  };
};
