import React, { Component } from 'react';
import { Nav, NavItem, NavLink } from 'reactstrap';

const iconClass = {
    MstList: "fa-th-large",
    MstRecord:"fa-briefcase",
    DtlList:"fa-list-ul",
    DtlRecord: "fa-picture-o",
    Profile: "fa-address-card-o",
    Setting: "fa-cogs",
    NewPassword: "fa-unlock-alt",
}

const activeLinkClass = "active";
const activeTabClass = "fill-fintrux";

export default class NaviBar extends Component {
  constructor(props) {
    super(props);  
    this.onClick = this.onClick.bind(this);
  }

  onClick (v) {
    return (function(evt) {
        if (this.props.mstListCount === 0) return;
        else if (this.props.history) this.props.history.push(v.path);
        else return;
    }).bind(this);
  }
  render() {
    return (
        <Nav tabs>
        {this.props.navi.map((v,i) => {
            return (
                <NavItem key={i}>
                <NavLink onClick={this.onClick(v)} className={(v.active ? activeLinkClass : (this.props.mstListCount === 0 ? 'd-none' : ''))}>
                  <i className={(v.active ? activeTabClass + " fs-tab-icon fa " + (v.iconClass || iconClass[v.type] ||"") : " fs-tab-icon fa " + (v.iconClass || iconClass[v.type] ||""))}></i><span className={(v.active ? "page-name " + activeTabClass : "page-name")}><br />{v.label}</span>
                </NavLink>
               </NavItem>
            )
        })}
        </Nav>
      )  
    };
};
