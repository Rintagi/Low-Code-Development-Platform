import * as React from 'react';

import { StyleSheet, Platform, Permission, Button } from 'react-native';
import EditScreenInfo from '../components/EditScreenInfo';
import { Text, View } from '../components/Themed';
import { BarCodeScanner } from 'expo-barcode-scanner';

export default class TabTwoScreen extends React.Component<any,any> {
  constructor(props: any) {
    super(props);
    this.state = {
      hasScanner: false
    };
  }
  componentDidMount() {
    const platform = Platform.OS;
    if (platform !== 'web') {
      BarCodeScanner.requestPermissionsAsync();
    }
  }
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.title}>Tab Two</Text>
        <View style={styles.separator} lightColor="#eee" darkColor="rgba(255,255,255,0.1)" />
        {this.state.openBarcodeScanner && <BarCodeScanner
          onBarCodeScanned={this.handleBarCodeScanned.bind(this)}
          style={StyleSheet.absoluteFillObject}
        />}
        <Text>{this.state.barCodeType}</Text>
        <Text>{this.state.barCodeContent}</Text>
        <Button
          title="scanBarcode"
          onPress={this.onScanBarcodePress.bind(this)}
          color="blue"
        />
        {/* <EditScreenInfo path="/screens/TabTwoScreen.tsx" /> */}
      </View>
    );
  }
  onScanBarcodePress()
  {
    this.setState({
      openBarcodeScanner: true,
    })
  }
  handleBarCodeScanned(result: { type: any; data: any; })
  {
    const { type, data } = result;
    this.setState({
      openBarcodeScanner: false,
      barCodeContent: data,
      barCodeType: type,
    })

    alert(`Bar code with type ${type} and data ${data} has been scanned!`);
  };
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
