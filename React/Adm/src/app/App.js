import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.css'
import '../scss/app.scss';

import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { setSpinner } from '../redux/Global';

import Router from './Router';
import { hasBlocker } from '../helpers/navigation'

class App extends Component {
  constructor() {
    super();
  }

  componentDidMount() {
    window.addEventListener('beforeunload', (e) => {
      if (hasBlocker()) {
        // Cancel the event as stated by the standard.
        e.preventDefault();
        // Chrome requires returnValue to be set.
        e.returnValue = "are you sure";
        return "are you sure";
      }
    })
  }

  render() {
    return (
      <div>
        {
          this.props.global.pageSpinner &&
          <div className='load'>
            <div className='load__icon-wrap'>
              {/* <svg className='load__icon'>
                <path fill='#21A79B' d='M12,4V2A10,10 0 0,0 2,12H4A8,8 0 0,1 12,4Z' />
              </svg> */}
              <div className="cssload-loader">
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
                <div className="cssload-side"></div>
              </div>
            </div>
          </div>
        }
        <div>
          <Router />
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
  global: state.global,
});

// const mapDispatchToProps = (dispatch) =>(
//   bindActionCreators(Object.assign({},
//     { setSpinner: setSpinner },
//   ), dispatch)
// )

const mapDispatchToProps = (dispatch) => (
  bindActionCreators({}, dispatch)
)

export default withRouter(connect(
  mapStateToProps, mapDispatchToProps
)(App));
