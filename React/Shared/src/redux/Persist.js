/* 
 * this is not generic persist for redux but just simple session based persist to survive browser refresh 
 */
import { LocalStorage, SessionStorage } from '../helpers/asyncStorage';

const storageLocation =(acrossSession)=>(acrossSession ? localStorage : sessionStorage);

export function RememberCurrent(key, item, acrossSession = true) {
    if (key) {
        try {
            storageLocation(acrossSession).setItem(key,JSON.stringify(item));
        } catch (e) {
            storageLocation(acrossSession).setItem(key,item);
        }
    }
    else {
        storageLocation(acrossSession).removeItem(key);
    }
}

export function GetCurrent(key, acrossSession = true) {
    try {
        return JSON.parse(storageLocation(acrossSession)[key]); 

    } catch (e)
    {
        return storageLocation(acrossSession)[key]; 
    }    
}

export async function RememberCurrentAsync(key, item, acrossSession = true) {
    if (item != undefined && item != null) {
        try {
            return (acrossSession ? LocalStorage : SessionStorage).setItem(key, JSON.stringify(item));
        } catch (e) {
            return (acrossSession ? LocalStorage : SessionStorage).setItem(key, item);
        }
    }
    else {
        return (acrossSession ? LocalStorage : SessionStorage).removeItem(key);
    }
}

export async function GetCurrentAsync(key, acrossSession = true) {
    return (acrossSession ? LocalStorage : SessionStorage)
    .getItem(key)
    .then(v => {
        try {
            return JSON.parse(v)
        } catch (e) {
            return v;
        }
    })
    .catch(error => {
        return null;
    })
}