import React, { PureComponent } from 'react';
import { Card, CardBody, Col, Progress, Row } from 'reactstrap';
import classNames from 'classnames';

export default class ProgressBar extends PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      value: 0,
      valueText: "",
      score: 0
    };

    this.scorePassword = this.scorePassword.bind(this);
  }

  scorePassword = (pass) => {
    var score = 0;
    if (!pass) {
      this.setState({
        score: 0
      });
    } else {
      var letters = {};
      for (var i = 0; i < pass.length; i++) {
        letters[pass[i]] = (letters[pass[i]] || 0) + 1;
        score += 5.0 / letters[pass[i]];
      }
      // bonus points for mixing it up
      var variations = {
        digits: /\d/.test(pass),
        lower: /[a-z]/.test(pass),
        upper: /[A-Z]/.test(pass),
        nonWords: /\W/.test(pass)
      };
      var variationCount = 0;
      for (var check in variations) {
        variationCount += (variations[check] == true) ? 1 : 0;
      }
      score += (variationCount - 1) * 10;
      if (pass.length >= 8) score += 40;
      else if (pass.length >= 6) score += 20;
      if (score > 100) score = 100;

      this.setState({
        score: parseInt(score, 10)
      });
    }
  }

  componentDidUpdate() {
    this.scorePassword(this.props.text);

    if (this.state.score === 0) {
      this.setState({
        value: 0,
        valueText: ""
      });
    } else if (this.state.score > 0 && this.state.score <= 30) {
      this.setState({
        value: 25,
        valueText: "Weak"
      });
    } else if (this.state.score > 30 && this.state.score <= 60) {
      this.setState({
        value: 50,
        valueText: "Normal"
      });
    } else if (this.state.score > 60 && this.state.score <= 80) {
      this.setState({
        value: 75,
        valueText: "Good"
      });
    } else {
      this.setState({
        value: 100,
        valueText: "Strong"
      });
    }
  }

  render() {

    let progressBarClass = classNames({
      'progress-wrap--pink': this.state.score <= 30 ? true : false,
      'progress-wrap--yellow': this.state.score > 30 && this.state.score <= 60 ? true : false,
      'progress-wrap--blue': this.state.score > 60 && this.state.score <= 80 ? true : false
    });

    return (
      <Col md={12} lg={12} className="px-0">
        <div className={`progress-wrap progress-wrap--big ${progressBarClass}`}>
          <Progress
            value={this.state.value}
            name={this.props.name}
          >
            {this.state.valueText}
          </Progress>
        </div>
      </Col>
    )
  }
}