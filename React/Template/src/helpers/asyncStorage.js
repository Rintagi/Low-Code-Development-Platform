/* abstract out localStorage/sessionStorage into Promise, 
 * can be replaced with other implementation for React Native or Server side nodejs rendering
 */
import log from '../helpers/logger'

class AsyncStorage {
     
     constructor(session)
     {
        this.isSession = session;
        this.storage = session ? sessionStorage : localStorage;
     }

     setItem(k, v) {
        const _this = this;
        return new Promise(function (resolve, reject) {
            _this.storage.setItem(k,v);
            resolve({[k]: v});
        });
     }

     getItem(k) {
        const _this = this;
        return new Promise(function (resolve, reject) {
            const v = _this.storage.getItem(k); 
            resolve(v);
        });
     }

     removeItem(k) {
        const _this = this;
        return new Promise(function (resolve, reject) {
            _this.storage.removeItem(k);
            resolve({});
        });
     }
}

export const LocalStorage = new AsyncStorage(false);
export const SessionStorage = new AsyncStorage(true);