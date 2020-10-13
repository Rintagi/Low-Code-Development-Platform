import log from '../helpers/logger';
import { getUrl } from '../services/systemService';
import { getDefaultPath } from '../helpers/utils';
import { getReactContainerInfo, getReactContainerStatus, uuid } from '../helpers/domutils';

/* this is rintagi controlled redux, DO NOT EXPAND OR MODIFY
 * it would be overwritten
 */

export const BUNDLE_CHECK_RESULT = 'BUNDLE_CHECK_RESULT';

const initState={
  bundleStatus: {
    currentJSBundleName: null,
    cachedJSBundleName: null,
    latestJSBundleName: null,
    lastCheckedBundleName: null,
    lastBundleCheckOn: null,  
    latestBundleLastModified: null,
    latestBundleETag: null,
  }
}

// Reducer
export function rintagiReducer(state = initState, action) {
  switch (action.type) {
    case BUNDLE_CHECK_RESULT:
      return { 
        ...state, 
        bundleStatus: {
          ...state.bundleStatus,
          ...action.payload,
        }
      }
    default:
      return state;
  }
}

const onGoingCheck = {
  waiting: undefined,
};
let lastCheckOn = 0;
let refreshing = false;
export function refreshApp(refresh) {
  if (refreshing) return false;
  else if (refresh) return refreshing = refresh;
  else return false;
}
export function checkBundleUpdate(docObj, fromLocation) {
  return (dispatch, getState, { webApi }) => {
    const currentBundleStatus = (getState().rintagi || {}).bundleStatus || {};
    if (onGoingCheck.waiting) {
      return onGoingCheck.waiting;
    }
    const _uuid = uuid();
    const rintagi = docObj.Rintagi;
    const swRegistration = docObj.swRegistration;
    const swUnRegister = docObj.swUnRegister;
    const myRunningJS = rintagi.myJS;
    const indexCached = rintagi.indexCached !== false;
    const defaultCheckIntervalSec = 60;
    const checkInterval = rintagi.bundleCheckInterval || defaultCheckIntervalSec;
    const myself = rintagi.myDocumentUrl;
    const myselfLatest = myself + (myself[myself.length - 1] === '/' ? '' : '/') + '?' + uuid(); // bypass caching via (?) and don't need server urlrewrite support
    const lastCheckedVersion = currentBundleStatus.lastCheckedBundleName;
    const lastModified = currentBundleStatus.latestBundleLastModified;
    const eTag = currentBundleStatus.latestBundleETag;
    const now = Date.now();
    const options = {
      headers: {
      }
    }
    log.debug(indexCached, now - lastCheckOn, now, lastCheckOn);
    if (lastModified) options.headers['If-Modified-Since'] = lastModified;
    //if (eTag) options.headers['If-None-Match'] = eTag;
    const me = (now - lastCheckOn) < checkInterval * 1000
      ? Promise.resolve({})
      : getUrl(myself, options)
        .then(ret => {
          lastCheckOn = !indexCached ? now : lastCheckOn;
          return getReactContainerStatus(ret);
        })
        .catch(error => {
          return {

          }
        });
    const latestMe = 
          (!indexCached || (now - lastCheckOn) < checkInterval * 1000)
      ? Promise.resolve({})
      : getUrl(myselfLatest, options)
        .then(ret => {
          lastCheckOn = now;
          log.debug('forced check ' + myselfLatest);
          return getReactContainerStatus(ret);
        })
        .catch(error => {
          return {

          }
        });
    const meAndLatestMe = Promise.all([me, latestMe]);
    onGoingCheck.waiting = meAndLatestMe
      .then(([me, newMe]) => {
        const latestMe = newMe.myJS ? newMe : (me.myJS ? me : {});
        const latestBundleJS = newMe.myJS || me.myJS;
        const latestLastModified = newMe.lastModified || me.lastModified;
        const latestETag = newMe.etag || me.etag;
        log.debug(myself, myselfLatest, latestMe, myRunningJS);
        if (
          (newMe.myJS && myRunningJS !== newMe.myJS)
          ||
          (me.myJS && myRunningJS !== me.myJS)
          ||
          (newMe.myJS && newMe.myJS !== me.myJS)
        ) {
          const currentUrl = fromLocation.href;
          const refreshUrl = currentUrl.replace(myself, myselfLatest);
          //lastCheckOn = now;
          // this (.update()) doesn't work, still use the old one even after call
          if (swRegistration) {
            false && swRegistration.update()
              .then(r => {
                console.log('sw refreshed via update')
                console.log(r);
              })
              .catch(error => {
                console.log('sw refreshed failed')
                console.log(error);
              })
          }
          if ((
            !lastCheckedVersion ||
            (latestBundleJS && lastCheckedVersion !== latestBundleJS)
          )
          ) {
            log.info(myRunningJS, latestBundleJS);
            console.log('a new version is available, please refresh the page');
            if (typeof swUnRegister === 'function') {
              // this is a must to force no caching before reload, basically disable service worker
              // we don't do it here, leave to front end(see screen.js)
              // document.swUnRegister();
            }
            // alert('a new version is available, please refresh the page');
            // window.location.reload();
          }
          //window.location = refreshUrl;
          //window.location.reload();
        }
        if (latestBundleJS
          // only dispatch if there is change ?
          //&& latestBundleJS !== lastCheckedVersion
          ) {
          dispatch({ type: BUNDLE_CHECK_RESULT
            , payload: {
              currentJSBundleName: myRunningJS,
              cachedJSBundleName: me.myJS,
              latestJSBundleName: latestBundleJS,
              lastCheckedBundleName: latestBundleJS,
              lastBundleCheckOn: now,  
              latestBundleLastModified: latestLastModified,
              latestBundleETag: latestETag,
                     
            } });
  
        }
        return {
          latestMe:latestMe || {},
          currentJSBundleName: myRunningJS,
        } 
      })
      .catch(error => {
        console.log(error);
        return Promise.reject(error);
      })
      .finally(() => {
        onGoingCheck.waiting = undefined;
      })
    return onGoingCheck.waiting;
  }
}