// In production, we register a service worker to serve assets from local cache.

// This lets the app load faster on subsequent visits in production, and gives
// it offline capabilities. However, it also means that developers (and users)
// will only see deployed updates on the "N+1" visit to a page, since previously
// cached resources are updated in the background.

// To learn more about the benefits of this model, read https://goo.gl/KwvDNy.
// This link also includes instructions on opting out of this behavior.

const isLocalhost = Boolean(
  window.location.hostname === 'localhost' ||
  // [::1] is the IPv6 localhost address.
  window.location.hostname === '[::1]' ||
  // 127.0.0.1/8 is considered localhost for IPv4.
  window.location.hostname.match(
    /^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/
  )
);

export function register(scope) {
  if ((process.env.NODE_ENV === 'production' || true) && 'serviceWorker' in navigator) {
    // The URL constructor is available in all browsers that support SW.
    const publicUrl = new URL(process.env.PUBLIC_URL, window.location);
    if (publicUrl.origin !== window.location.origin) {
      // Our service worker won't work if PUBLIC_URL is on a different origin
      // from what our page is served on. This might happen if a CDN is used to
      // serve assets; see https://github.com/facebookincubator/create-react-app/issues/2374
      return;
    }

    window.addEventListener('load', () => {
      const swUrl = `${process.env.PUBLIC_URL}/service-worker.js`;

      if (isLocalhost) {
        // This is running on localhost. Lets check if a service worker still exists or not.
        checkValidServiceWorker(swUrl,scope);

        // Add some additional logging to localhost, pointing developers to the
        // service worker/PWA documentation.
        navigator.serviceWorker.ready.then(() => {
          console.log(
            'This web app is being served cache-first by a service ' +
            'worker. To learn more, visit https://goo.gl/SC7cgQ'
          );
        });
      } else {
        // Is not local host. Just register service worker
        registerValidSW(swUrl,scope);
      }
    });
  }
}

function registerValidSW(swUrl, scope) {
  navigator.serviceWorker
    .register(swUrl, scope)
    .then(registration => {
      // it is ridiculous that this is not stored somewhere or pass to other part of the system
      // via postMessage(if there are listener setup BEFORE difficult, see index.js as this happens way earlier than React)
      // so we stuck it to document object(which should never go away except refresh)
      document.swRegistration = registration;
      listenForWaitingServiceWorker(registration, promptUserToRefresh);
      console.log('service work registration');
      console.log(registration);
      const sw = registration.installing || registration.waiting || registration.active;
      if (sw) {
        sw.addEventListener("statechange", (e) => {
          console.log(e);
        })
      }
      registration.addEventListener('updatefound', () => {
        const installingWorker = registration.installing;
        console.log('new sw version found');
        installingWorker.addEventListener('statechange', () => {
          if (installingWorker.state === 'installed') {
            if (navigator.serviceWorker.controller) {
              // At this point, the old content will have been purged and
              // the fresh content will have been added to the cache.
              // It's the perfect time to display a "New content is
              // available; please refresh." chat in your web app.
              console.log('New content is available; please refresh.');
            } else {
              // At this point, everything has been precached.
              // It's the perfect time to display a
              // "Content is cached for offline use." chat.
              console.log('Content is cached for offline use.');
            }
          }
        });
      });
    })
    .catch(error => {
      console.error('Error during service worker registration:', error);
    });
}

function checkValidServiceWorker(swUrl,scope) {
  // Check if the service worker can be found. If it can't reload the page.
  fetch(swUrl)
    .then(response => {
      // Ensure service worker exists, and that we really are getting a JS file.
      if (
        response.status === 404 ||
        response.headers.get('content-type').indexOf('javascript') === -1
      ) {
        // No service worker found. Probably a different app. Reload the page.
        navigator.serviceWorker.ready.then(registration => {
          registration.unregister().then(() => {
            window.location.reload();
          });
        });
      } else {
        // Service worker found. Proceed as normal.
        registerValidSW(swUrl,scope);
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
    console.log('waiting for new SW installation');
    reg.installing.addEventListener('statechange', function(e) {
      console.log('new SW installed');
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
var refreshing;

(navigator.serviceWorker && navigator.serviceWorker.addEventListener('controllerchange',
  function() {
    console.log('controller changed, should find a way to refresh window ');
    if (refreshing) return;
    refreshing = true;
    //window.location.reload();
  }
));

function promptUserToRefresh(reg) {
  // this is just an example
  // don't use window.confirm in real life; it's terrible
  // blind skip
  reg.waiting.postMessage('skipWaiting');
  // if (window.confirm("New SW version available! OK to refresh?")) {
  // }
}

export function unregister() {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.ready.then(registration => {
      registration.unregister();
    });
  }
}

