/* configuration setup, like url of web service endpoint etc. this is react native(expo) based */
import log from './logger';
import { getReactContainerInfo, uuidv4 } from './domutils';
import { LocalStorage } from './asyncStorage';
import Constants from 'expo-constants';

export let myMachine = null;
/* expo config(via Constants), if not using expo must be replaced with proper mechanism for react-native */
const rintagiConfig = (((Constants || {}).manifest || {}).extra || {}).serverConfig || {
    apiBasename:'http://localhost/ro',
    restfulApi:false,
};

export function getMyMachine() {
    return myMachine;
}

// should find other native ways to identify the device
export function getFingerPrint() {
    return new Promise(function (resolve, reject) {
        setTimeout(() => {
            LocalStorage.getItem('myMachine')
                        .then(ret => {
                            if (!ret) {
                               myMachine = uuidv4();
                               log.debug(myMachine);
                               LocalStorage.setItem('myMachine', myMachine);
                               resolve(myMachine);     
                            }
                            else resolve(ret);
                        })
                        .catch(error => {
                            log.debug(error);
                            reject(error);
                        })
            return;
        }, 500);
    });
};

export function setupRuntime() {
    return;
};
  
  
export function getRintagiConfig()
{
    return rintagiConfig;
}

setupRuntime();