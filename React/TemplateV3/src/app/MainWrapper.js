import React, {PureComponent} from 'react';
import {connect} from 'react-redux';
import classNames from 'classnames';

// class MainWrapper extends PureComponent {
//   render() {
//     return (
//       <div className="theme-light">
//          <div className="wrapper blocks-with-shadow-theme top-navigation">
//           {this.props.children}
//         </div>
//       </div>
//     )
//   }
// }

// export default connect()(MainWrapper);

// Aaron changes
class MainWrapper extends PureComponent {

  render() {
    let wrapperClass = classNames({
      'wrapper': true,
      'wrapper--full-width': this.props.sidebar.collapse,
    });
    
    return (
      <div className="theme-light">
        <div className={wrapperClass}>
          {this.props.children}
        </div>
      </div>
    )
  }
}

export default connect(state => {
  return {
    sidebar: state.sidebar,
  }
})(MainWrapper);