const validate = values => {
  const errors = {};
  if (!values.username) {
    errors.username = 'Username field shouldn’t be empty';
  }
  // if (!values.email) {
  //   errors.email = 'Email field shouldn’t be empty';
  // } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
  //   errors.email = 'Invalid email address'
  // }
  if (!values.password) {
    errors.password = 'Password field shouldn’t be empty';
  }
  // if (!values.select) {
  //   errors.select = 'Please select the option';
  // }
  if (!values.cBGlChartId65) {
    errors.cBGlChartId65 = 'Please select the account';
  }
  // if (!values.dropdown) {
  //   errors.dropdown = 'Please select from dropdown menu';
  // }
  if (!values.cTrxDetDesc65) {
    errors.cTrxDetDesc65 = 'Please enter the description';
  }
  if (!values.cTrxDetAmt65) {
    errors.cTrxDetAmt65 = 'Please enter the amount';
  }
  if (!values.cTrxDetGst65) {
    errors.cTrxDetGst65 = 'Please enter the GST';
  }
  if (!values.cTrxDetImg65) {
    errors.cTrxDetImg65 = 'Please upload your receipt';
  }
  if (!values.cProjectId64) {
    errors.cProjectId64 = 'Please select the project';
  }
  // if (!values.currency) {
  //   errors.currency = 'Please select the currency';
  // }
  if (!values.cCustomerJobId64) {
    errors.cCustomerJobId64 = 'Please select the customer job';
  }
  if (!values.cTrxNote64) {
    errors.cTrxNote64 = 'Please enter the memo';
  }

  return errors;
};

export default validate;
