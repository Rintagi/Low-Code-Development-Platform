import React, {PureComponent} from 'react';
import NotificationSystem from 'react-notification-system';
import PropTypes from 'prop-types';

export default class NotificationComponent extends PureComponent {
  static propTypes = {
    position: PropTypes.string.isRequired,
    title: PropTypes.string,
    message: PropTypes.string,
    img: PropTypes.string,
    className: PropTypes.string
  };

  constructor(props) {
    super(props);

    this.state = {
      _notificationSystem: null
    };

    this._addNotification = this._addNotification.bind(this);
  }

  _addNotification(e) {
    e.preventDefault();
    this._notificationSystem.addNotification({
      level: (this.props.className ? `${this.props.className}` : 'success'),
      position: `${this.props.position}`,
      // autoDismiss: 0,
      children: (
        <div className='notification__content-wrap'>
          {this.props.img ? <img className='notification__img' src={this.props.img} alt='notification-img'/> : ''}
          <div className='notification__content'>
            <h5 className='notification__title bold-text'>{this.props.title}</h5>
            <p className='notification__message'>{this.props.message}</p>
          </div>
        </div>
      )
    });
  }

  componentDidMount() {
    this._notificationSystem = this.refs.notificationSystem;
  }

  render() {
    const style = {
      Dismiss: {
        DefaultStyle: {
          top: '20px',
          right: '20px',
          backgroundColor: 'transparent',
          fontWeight: 'normal',
        }
      },
      Containers: {
        DefaultStyle: {
          maxWidth: '420px',
          width: '100%',
          zIndex: '99'
        },
        tr: {
          top: '60px'
        },
        tl: {
          top: '60px',
          left: '240px'
        },
        bl:{
          left: '240px'
        },
        tc: {
          top: '60px',
          left: '0',
          maxWidth: '100vw',
          width: '100vw',
          padding: '0'
        },
        bc: {
          bottom: '0px',
          left: '10px',
          right: '10px',
          maxWidth: '600px',
          // width: '100%',
          padding: '0'
        }
      },
      NotificationItem: {
        DefaultStyle: {
          borderTop: 'none',
          padding: '0',
          width: '100%',
          boxShadow: '0 2px 8px 0 rgba(0, 0, 0, 0.07)',
          borderRadius: '0',
          height: 'auto'
        },
        success: {
          backgroundColor: '#21A79B'
        },

        error: {
          backgroundColor: '#ff4861'
        },

        warning: {
          backgroundColor: '#f6da6e'
        },

        info: {
          backgroundColor: '#70bbfd'
        }
      }
    };

    return (
      <div className={this.props.className ? `notification notification--${this.props.className}` :
        'notification notification--basic'}>
        <div onClick={this._addNotification}>
          {this.props.children}
        </div>
        <NotificationSystem ref='notificationSystem' style={style}/>
      </div>
    );
  }
}
