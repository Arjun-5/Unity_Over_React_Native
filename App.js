/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, { useState } from 'react';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  StatusBar,
    Button,
    Alert
} from 'react-native';

import {
  Header,
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
import UnityView, { UnityModule } from '@asmadsen/react-native-unity-view';

import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';

function HomeScreen({ navigation }) {
  const onClick = () => {
    navigation.navigate('UnityScreen');
  };

  return (
    <View style={{ flex: 1 }}>
      <Text> React Native App</Text>
      <Button
        style={{ width: '100%' }}
        title="Open Unity Player"
        onPress={onClick}
      />
    </View>
  );
}

function UnityScreen({ navigation }) {

  // your other code
  return (
    <View style={styles.container}>
      <UnityView
        style={{ position: 'absolute', left: 0, right: 0, top: 1, bottom: 1 }}
      />
    </View>
  );
}
const Stack = createStackNavigator();

function MyStack() {
  return (
    <Stack.Navigator>
      <Stack.Screen name="Home" component={HomeScreen} />
      <Stack.Screen name="UnityScreen" component={UnityScreen} />
    </Stack.Navigator>
  );
}

const styles = StyleSheet.create({
									container: {
									flex: 1,
									flexDirection: 'column',
									justifyContent: 'center',
									alignItems: 'center',
									backgroundColor: 'yellow',
									},
                                   scrollView: {
                                     backgroundColor: Colors.lighter,
                                   },
                                   engine: {
                                     position: 'absolute',
                                     right: 0,
                                   },
                                   body: {
                                     backgroundColor: Colors.white,
                                   },
                                   sectionContainer: {
                                     marginTop: 32,
                                     paddingHorizontal: 24,
                                   },
                                   sectionTitle: {
                                     fontSize: 24,
                                     fontWeight: '600',
                                     color: Colors.black,
                                   },
                                   sectionDescription: {
                                     marginTop: 8,
                                     fontSize: 18,
                                     fontWeight: '400',
                                     color: Colors.dark,
                                   },
                                   highlight: {
                                     fontWeight: '700',
                                   },
                                   footer: {
                                     color: Colors.dark,
                                     fontSize: 12,
                                     fontWeight: '600',
                                     padding: 4,
                                     paddingRight: 12,
                                     textAlign: 'right',
                                   },
                                 });

export default function App() {
  return (
    <NavigationContainer>
      <MyStack />
    </NavigationContainer>
  );
}
