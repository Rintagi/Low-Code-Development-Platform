// This optional code is used to register a service worker.
// register() is not called by default.

// This lets the app load faster on subsequent visits in production, and gives
// it offline capabilities. However, it also means that developers (and users)
// will only see deployed updates on subsequent visits to a page, after all the
// existing tabs open on the page have been closed, since previously cached
// resources are updated in the background.

// To learn more about the benefits of this model and instructions on how to
// opt-in, read https://bit.ly/CRA-PWA

// this is part of the react app so can use process.env etc.
// the actual service worker js should be under PUBLIC_URL
// create-react-app generate standard 'service-worker.js'(for caching)
// we can use other hand coded one for other purposes
// there can be multiple SW registered for the same app
const serviceWorkJS = `${process.env.PUBLIC_URL}/firebase-messaging-sw.js`
const fcmScope = 'firebase-cloud-messaging-push-scope';

const isLocalhost = Boolean(
  window.location.hostname === 'localhost' ||
  // [::1] is the IPv6 localhost address.
  window.location.hostname === '[::1]' ||
  // 127.0.0.0/8 are considered localhost for IPv4.
  window.location.hostname.match(
    /^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/
  )
);

export let fcmRegistration;
export let fcmReRegister;
export let fcmUnRegister;



export function register(config) {
  console.log(config);
  if ((process.env.NODE_ENV === 'production' || true) 
  && 'serviceWorker' in navigator
  && 'Notification' in window
  ) {
    // The URL constructor is available in all browsers that support SW.
    const publicUrl = new URL(process.env.PUBLIC_URL, window.location.href);
    if (publicUrl.origin !== window.location.origin) {
      // Our service worker won't work if PUBLIC_URL is on a different origin
      // from what our page is served on. This might happen if a CDN is used to
      // serve assets; see https://github.com/facebook/create-react-app/issues/2374
      return Promise.reject("");
    }

    // already registered
    if (fcmRegistration) return Promise.resolve(fcmRegistration);

    const swUrl = (config || {}).serviceWorkJS || serviceWorkJS;
    
    if (isLocalhost) {
      // Add some additional logging to localhost, pointing developers to the
      // service worker/PWA documentation.
      navigator.serviceWorker.ready.then(() => {
        console.log(`${serviceWorkJS} loaded`);
      });

      // This is running on localhost. Let's check if a service worker still exists or not.
      const registration = checkValidServiceWorker(swUrl, { scope: fcmScope, ...config });

      return registration;

    } else {
      // Is not localhost. Just register service worker
      const registration = registerValidSW(swUrl, { scope: fcmScope, ...config });

      return registration;
    }
    // window.addEventListener('load', () => {

    // });
  }
  else return Promise.reject("");
}

function registerValidSW(swUrl, config) {
  return fcmRegistration = navigator.serviceWorker
    .register(swUrl, config)
    .then(registration => {
      // it is ridiculous that this is not stored somewhere or pass to other part of the system
      // via postMessage(if there are listener setup BEFORE difficult, see index.js as this happens way earlier than React)
      // so we stuck it to document object(which should never go away except refresh)
      document.fcmRegistration = registration;
      document.fcmReRegister = () => registerValidSW(swUrl, config);
      document.fcmUnRegister = unregister;
      console.log('fcm service work registration');
      const sw = registration.installing || registration.waiting || registration.active;
      if (sw) {
        sw.addEventListener("statechange", (e) => {
          console.log(e);
        })
        registration.addEventListener('updatefound', () => {
          const installingWorker = registration.installing;
          console.log('new fcm sw version found');
          installingWorker.onstatechange = () => {
            if (installingWorker.state === 'installed') {
              if (navigator.serviceWorker.controller) {
                // At this point, the updated precached content has been fetched,
                // but the previous service worker will still serve the older
                // content until all client tabs are closed.
                console.log(
                  'New content is available and will be used when all ' +
                  'tabs for this page are closed. See https://bit.ly/CRA-PWA.'
                );

                // Execute callback
                if (config && config.onUpdate) {
                  config.onUpdate(registration);
                }
              } else {
                // At this point, everything has been precached.
                // It's the perfect time to display a
                // "Content is cached for offline use." message.
                console.log('Content is cached for offline use.');

                // Execute callback
                if (config && config.onSuccess) {
                  config.onSuccess(registration);
                }
              }
            }
          }
        })
        return registration;
      };
    })
    .catch(error => {
      console.error('Error during  fcm service worker registration:', error);
    });
}

function checkValidServiceWorker(swUrl, config) {
  // Check if the service worker can be found. If it can't reload the page.
  return fetch(swUrl, {
    headers: { 'Service-Worker': 'script' },
  })
    .then(response => {
      // Ensure service worker exists, and that we really are getting a JS file.
      const contentType = response.headers.get('content-type');
      if (
        response.status === 404 ||
        (contentType != null && contentType.indexOf('javascript') === -1)
      ) {
        // No service worker found. Probably a different app. Reload the page.
        navigator.serviceWorker.ready.then(registration => {
          registration.unregister().then(() => {
            window.location.reload();
          });
        });
      } else {
        // Service worker found. Proceed as normal.
        return registerValidSW(swUrl, config);
      }
    })
    .catch(() => {
      console.log(
        'No internet connection found. App is running in offline mode.'
      );
    });
}

function listenForWaitingServiceWorker(reg, callback) {
  function awaitStateChange() {
    console.log('waiting for new fcm SW installation');
    reg.installing.addEventListener('statechange', function (e) {
      console.log('new fcm SW installed');
      console.log(this);
      console.log(e);
      if (this.state === 'installed') callback(reg);
    });
  }
  if (!reg) return;
  if (reg.waiting) return callback(reg);
  if (reg.installing) awaitStateChange();
  reg.addEventListener('updatefound', awaitStateChange);
}

// reload once when the new Service Worker starts activating
// var refreshing;

// (navigator.serviceWorker && navigator.serviceWorker.addEventListener('controllerchange',
//   function() {
//     console.log('controller changed, should find a way to refresh window ');
//     if (refreshing) return;
//     refreshing = true;
//     //window.location.reload();
//   }
// ));

// function promptUserToRefresh(reg) {
//   // blind skip without waiting(i.e. flush sw cache and used new one, the js is still OLD version! only sw is updated)
//   reg.waiting.postMessage('skipWaiting');
//   // force reload which would kick in new version of index.html(by the new sw if it is cached) 
//   // if (window.confirm("New SW version available! OK to refresh?")) {
//   // must do this to use new version  
//   // we don't do it here but use more aggressive strategy of in-app reload(see screen.js)
//   // only one can be used to avoid double prompt  
//   // window.location.reload();
//   // }
// }

export function unregister() {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.ready
      .then(registration => {
        registration.unregister();
      })
      .catch(error => {
        console.error(error.message);
      });
  }
}
