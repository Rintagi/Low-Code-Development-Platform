import React, { Component } from 'react';
import { Button, Popover, PopoverBody, PopoverHeader, UncontrolledPopover } from 'reactstrap';
import log from '../../helpers/logger';
import { isTouchDevice } from '../../helpers/utils';

export default class ControlledPopover extends Component {
  constructor(props) {
    super(props);
    this.state = {
      popoverOpen: false,
      // isMobile: false,
    };

    // this.mediaqueryresponse = this.mediaqueryresponse.bind(this);
    // this.mobileView = window.matchMedia("(max-width: 560px)");
    this.toggle = this.toggle.bind(this);
  }

  toggle() {
    this.setState({
      popoverOpen: !this.state.popoverOpen
    });
  }

  // mediaqueryresponse(value) {
  //   if (value.matches) { // if media query matches
  //     this.setState({ isMobile: true });
  //   }
  //   else {
  //     this.setState({ isMobile: false });
  //   }

  // }

  // componentDidMount() {
  //   this.mediaqueryresponse(this.mobileView);
  //   this.mobileView.addListener(this.mediaqueryresponse) // attach listener function to listen in on state changes
  //   // const isMobileView = this.state.isMobile;
  // }

  // componentWillUnmount() {
  //   this.mobileView.removeListener(this.mediaqueryresponse);
  // }

  render() {
    log.debug(this.state.popoverOpen);
    return (
      <span>
        <i
          id={this.props.id}
          className={`mdi mdi-help-box fs-20 color-light-grey v-align-sub pl-10 lh-23 pointer ${this.props.className}`}
          onClick={this.toggle}>
        </i>
        <Popover
          placement={this.props.placement || 'top-end'}
          hideArrow={true}
          trigger={isTouchDevice() ? 'classic' : 'hover'}
          target={this.props.id}
          isOpen={this.state.popoverOpen}
          toggle={this.toggle}>
          <PopoverBody>{this.props.message}</PopoverBody>
        </Popover>
      </span>
    );
  }
}