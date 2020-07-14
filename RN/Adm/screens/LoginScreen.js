import React, { Component } from "react";
import { Keyboard, Text, View, TextInput, TouchableWithoutFeedback, Alert, KeyboardAvoidingView, StyleSheet } from 'react-native';
import { Button } from 'react-native-elements';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { login } from '../redux/Auth';

class LoginScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userName: '',
      password: ''
    }
  }
  render() {
    return (
      <KeyboardAvoidingView style={styles.containerView} behavior="padding">
          <View style={styles.loginScreenContainer}>
            <View style={styles.loginFormView}>
              <TextInput
                placeholder="Username"
                style={styles.loginFormTextInput}
                defaultValue={this.state.userName}
                onChangeText={TextInputValue => this.setState({ userName: TextInputValue })}
              />
              <TextInput
                placeholder="Password"
                style={styles.loginFormTextInput}
                defaultValue={this.state.password}
                secureTextEntry={true}
                onChangeText={TextInputValue => this.setState({ password: TextInputValue })}
              />
              <Button
                buttonStyle={styles.loginButton}
                onPress={() => this.onLoginPress()}
                title="Login"
              />
            </View>
          </View>
      </KeyboardAvoidingView>
    );
  }

  componentDidMount() {
  }

  componentWillUnmount() {
  }

  onLoginPress() {
    const { userName, password } = this.state;
    this.props.login(userName, password)
              .then(r => {
                  console.log(r);
                  alert('login successful');
              })
              .catch(e => {
                  alert('login error ' + e);
                  console.log(e);
              })
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#e5e5e5"
  },
  headerText: {
    fontSize: 20,
    textAlign: "center",
    margin: 10,
    fontWeight: "bold"
  }
});

const mapStateToProps = (state) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  global: state.global,
  menu: (state.auth || {}).menu,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { login: login },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(LoginScreen);