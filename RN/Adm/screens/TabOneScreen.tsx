import * as React from 'react';
import { StyleSheet, Button } from 'react-native';

import EditScreenInfo from '../components/EditScreenInfo';
import { Text, View } from '../components/Themed';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { logout } from '../redux/Auth';

import * as LocalAuthentication from 'expo-local-authentication';

class TabOneScreen extends React.Component<any,any> {
  constructor(props: any) {
    super(props);
    this.state = {

    };
  }
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.title}>Tab One</Text>
        <View style={styles.separator} lightColor="#eee" darkColor="rgba(255,255,255,0.1)" />
        <Text>{this.state.isAuthorized && (this.props.user || {}).UsrName}</Text>
        <Button
          onPress={() => this.onShowSensitiveInfo()}
          title="Show Login Info"
        />
        <Button
          onPress={() => this.onLogoutPress()}
          title="Logout"
        />
        {/* <EditScreenInfo path="/screens/TabOneScreen.tsx" /> */}
      </View>
    );
  }
  onLogoutPress() {
    this.props.logout(false);
  }
  async onShowSensitiveInfo() {
    const hasBio = await LocalAuthentication.hasHardwareAsync();
    const authType = await LocalAuthentication.supportedAuthenticationTypesAsync();
    const isEnrolled = await LocalAuthentication.isEnrolledAsync();    
    const auth = await LocalAuthentication.authenticateAsync({
      promptMessage: 'auth required for showing wallet',
    });
    console.log(hasBio);
    console.log(authType);
    console.log(isEnrolled);
    console.log(auth);
    if (!isEnrolled
      || !hasBio
      || (auth && auth.success)
      )
      {
        this.setState({
          isAuthorized:true
        })    
      }
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
});

const mapStateToProps = (state: any) => ({
  user: (state.auth || {}).user,
  auth: state.auth,
  global: state.global,
});

const mapDispatchToProps = (dispatch: any) => (
  bindActionCreators(Object.assign({},
    { logout: logout },
  ), dispatch)
)
export default connect(mapStateToProps, mapDispatchToProps)(TabOneScreen);