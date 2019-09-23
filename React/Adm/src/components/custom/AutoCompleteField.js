import React, { Fragment, Component } from 'react';
import { Typeahead, TypeaheadMenu, Menu, MenuItem } from 'react-bootstrap-typeahead';
import log from '../../helpers/logger';
export default class AutoCompleteField extends Component {

  constructor(props) {
    super(props);
    this.menuVisible = false; // can't use state as it is not reflected UNTIL next render, use instance variable instead
    this.isEnter = false;
    this.state = {
      lastSelectedValue: this.props.defaultSelected
    }
  }
  renderMenu = (results, menuProps) => {
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
  handleFocus = (event) => {
    this.hasFocus = true;
    event.target.setSelectionRange(0, event.target.value.length);
  }
  handleKeyDown = (inputChar, ...rest) => {
    const value = inputChar.target.value;
    this.isEnter = inputChar.keyCode === 13;
    this.isTab = inputChar.keyCode === 9;
    this.isEscape = inputChar.keyCode === 27;
    this.isEmpty = !value;
    if (this.isEnter || this.isTab || this.isEscape) {
      const pickFirst = (this.currentMatches || []).length > 0 && value && this.props.pickFirst;
      if (typeof this.props.onChange === "function") {
        if (pickFirst) this.props.onChange(this.props.name, [this.currentMatches[0]]);
        else if (this.isEmpty) this.props.onChange(this.props.name, [{}]);
        else if (this.isEnter || this.isEscape) {
          const instance = this.typeahead.getInstance();
          const priorValue = (this.currentValue || this.state.lastSelectedValue || this.props.defaultSelected || [null])[0];
          const priorSelected = priorValue || this.props.value || {};
          const priorLabel = priorSelected.label;
          if (priorLabel) {
            instance.setState({ text: priorLabel });
            instance._updateSelected([priorSelected]);
            if (this.isEnter && false) this.props.onChange(this.props.name, [priorSelected]);
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
  handleChange = (value, ...rest) => {
    this.currentValue = value;
    if (this.typeahead) {
      const instance = this.typeahead.getInstance();
      log.debug(instance);
      if (!instance.getInput().value) {
        this.handleInputChange("");
        if (!this.hasFocus) {
          if (typeof this.props.onBlur === "function") this.props.onBlur(this.props.name, true);
          instance.focus();

          setTimeout(() => { instance._hideMenu(); }, 0); // not public interface
        }
        else {
          //instance.blur();
          //instance.focus();
        }
      }
    }
    this.setState({ lastSelectedValue: value });
    // this is going to call setFieldValue and manually update values.this.props.name
    if (typeof this.props.onChange === "function"
      && !this.menuVisible // a selection is made
      && (value.length > 0 || true) // empty would not trigger onchange
    ) {
      this.props.onChange(this.props.name, value);
    }
  };

  handleBlur = (e) => {
    this.hasFocus = false;
    // this is going to call setFieldTouched and manually update touched.this.props.name
    const value = e.target.value;
    const pickFirst = (this.currentMatches || []).length > 0 && value && this.props.pickFirst;
    if (typeof this.props.onBlur === "function") {
      this.props.onBlur(this.props.name, true);
    }
    else {
      if (value && typeof this.props.onChange === "function" && pickFirst) {
        this.props.onChange(this.props.name, [this.currentMatches[0]]);
      }
    }
  };

  handleInputChange = (value, ...rest) => {
    // this is going to call setFieldValue and manually update values.this.props.name
    if (typeof this.props.onInputChange === "function") this.props.onInputChange(this.props.name, value);
  };

  filterBy = (option, props) => {
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
            id=""
            ref={(typeahead) => this.typeahead = typeahead}
            renderMenu={this.renderMenu}
            onPaginate={(e) => log.debug('Results paginated')}
            //options={range(0, 1000).map((o) => o.toString())}
            options={this.props.options}
            paginate={true}
            placeholder={this.props.placeholder}
            maxResults={20}
            flip={true}
            bsSize={"sm"}
            defaultInputValue={""}
            delay={500}
            value={this.props.value}
            onChange={this.handleChange}
            onBlur={this.handleBlur}
            onSelect={this.handleChange}
            onKeyDown={this.handleKeyDown}
            onFocus={this.handleFocus}
            onClick={this.handleClick}
            name={this.props.name}
            onInputChange={this.handleInputChange}
            defaultSelected={this.props.defaultSelected}
            onMenuShow={this.handleMenuShow}
            onMenuHide={this.handleMenuHide}
            clearButton={true}
            filterBy={this.props.filterBy}
            disabled = {this.props.disabled}
          />
        </Fragment>
      </div>
    );
  };
};
