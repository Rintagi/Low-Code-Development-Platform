/* customized code APPENDED to service-work.js via importScripts() service worker model
 * this is not running in the context of a DOM so things like alert() is not available
 * console.log() etc. is
 * also this is straight import so need to have other seperate build process if polyfill
 * or depedency on npm module is needed
 * THIS IS THE FINAL code viewable at the site level, not source code going through any 
 * webpack/gulp etc.
 * better to use a seperate build-process and output to this file or juse use
 * standard JS, ES5/6 probably is fine as service worker is supported only in 
 * new browsers
 * already in the context of service worker object(thus self. should be availble)
 * 
 * DO NOT USE const, should only use var as you can override var but not const
 */
/* self the registered service worker object */

;

console.log(self);
/* global scope function */
console.log(skipWaiting);

/* communication channel from client(i.e. browser etc.) to this */
self.addEventListener('message', function handler (event) {
    var msg = event.data || {};
    if (msg === 'skipWaiting' || msg.command === "skipWaiting") {
        console.log('new sw kicks in');
        self.skipWaiting();
    }
 });

 /* our fetch listener, service-worker.js only handle build time defined url GET(luckily) so we can
  * do whatever we want here as event listener is 'first' response win
  * i.e. we can't change cache behavior of build-time files
  * (like main js which is actually NOT cached in service-work.js which is correct as it only loads once)
 */
 self.addEventListener('fetch', function(event) {
    if (event.request.method === 'GET') {
      // Should we call event.respondWith() inside this fetch event handler?
      // This needs to be determined synchronously, which will give other fetch
      // handlers a chance to handle the request if need be.
      var shouldRespond;
      // check service-worker.js for the helper
      var url = stripIgnoredUrlParameters(event.request.url, ignoreUrlParametersMatching);
      var urlObj = new URL(event.request.url);
      var cacheName = 'whatever cache name'
      if (shouldRespond) {
        /* this would STOP all following listener for fetch */  
        event.respondWith(
            caches.open(cacheName).then(function(cache) {
              return cache.match(url).then(function(response) {
                if (response) {
                  return response;
                }
                throw Error('The cached response that was expected is missing.');
              });
            }).catch(function(e) {
              // Fall back to just fetch()ing the request if some unexpected error
              // prevented the cached response from being valid.
              console.warn('Couldn\'t serve response for "%s" from cache: %O', event.request.url, e);
              return fetch(event.request);
            })
          );  
      }  
    }
    else if (event.request.method === "POST") {
      var url = stripIgnoredUrlParameters(event.request.url, ignoreUrlParametersMatching);
      var urlObj = new URL(event.request.url);
    }
});

self.addEventListener('install', event => {
    console.log('installing');
    event.waitUntil(
        Promise.resolve("something")
    );
});

self.addEventListener('activate', event => {
    console.log('activated');
    event.waitUntil(
        Promise.resolve("something")
    );
});
    
/* override buggy version, logic in code is a double negative !!! */
isPathWhitelisted = function(whitelist, absoluteUrlString) {
// If the whitelist is empty, then consider all URLs to be whitelisted.
// WRONG, empty means NO WHITELIST in the logic as returning true means NOT WHITELIST!!! WTF google
if (whitelist.length === 0) {
    return true;
}

// Otherwise compare each path regex to the path of the URL passed in.
// must be ALL to handle multiple pattersn, not .some in the buggy version
var path = (new URL(absoluteUrlString)).pathname;
return whitelist.every(function(whitelistedPathRegex) {
    return path.match(whitelistedPathRegex);
});
};
