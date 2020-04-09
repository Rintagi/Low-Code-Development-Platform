import React from "react";
import ReactDOM from "react-dom";
import ReCAPTCHA from "react-google-recaptcha";
import { getUrlBeforeRouter } from "../../helpers/formatter";

const TEST_SITE_KEY = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
// const PRODUCTION_SITE_KEY = "6Lee3tsUAAAAAB5JOiK3xtm--Jb6YI8-Yb3twsV9";
//recaptcha key for cases.fintrux.com
const PRODUCTION_SITE_KEY = "6LcAieIUAAAAAFonwK00wtixrOw3RMX6C5XzrRfv"

const DELAY = 1500;

let rerenders = 0;

export default class Recaptcha extends React.Component {
  constructor(props, ...args) {
    super(props, ...args);
    this.state = {
      fireRerender: rerenders,
      callback: "not fired",
      value: "[empty]",
      load: false,
      expired: "false"
    };
    this._reCaptchaRef = React.createRef();
  }

  componentDidMount() {
    setTimeout(() => {
      this.setState({ load: true });
    }, DELAY);
    console.log("didMount - reCaptcha Ref-", this._reCaptchaRef);
  }

  handleChange = value => {
    console.log("Captcha value:", value);
    this.setState({ value });
    if (value !== null || value !== ""){
      this.props.onChange(value);
    }
  };

  asyncScriptOnLoad = () => {
    this.setState({ callback: "called!" });
    console.log("scriptLoad - reCaptcha Ref-", this._reCaptchaRef);
  };
  handleExpired = () => {
    this.setState({ expired: "true" });
  };
  handleExpired2 = () => {
    this.setState({ expired2: "true" });
  };

  render() {
    const { value, callback, load, expired } = this.state || {};

    const prdOnly = getUrlBeforeRouter().includes('cases.fintrux.com') || getUrlBeforeRouter().includes('prd01.rintagi.com');
    //  || getUrlBeforeRouter().includes('solution.rintagi.com');

    return (
      <div className="App">
        {load && (
          <ReCAPTCHA
            style={{ display: "inline-block" }}
            theme="light"
            ref={this._reCaptchaRef}
            // sitekey={TEST_SITE_KEY}
            sitekey={prdOnly ? PRODUCTION_SITE_KEY : TEST_SITE_KEY}
            onChange={this.handleChange}
            asyncScriptOnLoad={this.asyncScriptOnLoad}
          />
        )}
      </div>
    );
  }
}