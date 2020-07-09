import { AsyncStorage } from 'react-native';
import Constants from 'expo-constants';

class _AsyncStorage {
     
    constructor(session)
    {
       this.isSession = session;
       //this.storage = session ? sessionStorage : localStorage;
    }

    setItem(k, v) {
       const _this = this;
       return new Promise(function (resolve, reject) {
           AsyncStorage.setItem(k,v);
           resolve({[k]: v});
       });
    }

    getItem(k) {
       const _this = this;
       return new Promise(function (resolve, reject) {
           AsyncStorage.getItem(k)
                       .then(v => resolve(v))
                       .catch(e => resolve(undefined));
       });
    }

    removeItem(k) {
       const _this = this;
       return new Promise(function (resolve, reject) {
           AsyncStorage.removeItem(k);
           resolve({});
       });
    }
}

export const LocalStorage = new _AsyncStorage(false);
export const SessionStorage = new _AsyncStorage(true);