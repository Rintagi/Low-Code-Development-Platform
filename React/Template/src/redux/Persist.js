/* 
 * this is not generic persist for redux but just simple session based persist to survive browser refresh 
 */

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