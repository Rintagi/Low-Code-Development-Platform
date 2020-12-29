
register_fcm_sw = function register(config) {
    var registerValidSW = function (swUrl, config) {
        return fcmRegistration = navigator.serviceWorker
            .register(swUrl, config)
            .then(function (registration) {
                console.log('fcm service work registration');
                var sw = registration.installing || registration.waiting || registration.active;
                if (sw) {
                    sw.addEventListener("statechange", function (e) {
                        console.log(e);
                    });
                    registration.addEventListener('updatefound', function () {
                        var installingWorker = registration.installing;
                        console.log('new fcm sw version found');
                        installingWorker.onstatechange = function () {
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
                                    // console.log('Content is cached for offline use.');

                                    // Execute callback
                                    if (config && config.onSuccess) {
                                        config.onSuccess(registration);
                                    }
                                }
                            }
                        }
                    })
                    return registration;
                }
                else
                    return registration;
            })
            .catch(function (error) {
                console.error('Error during  fcm service worker registration:', error);
                return Promise.reject(error);
            });
    }


    if (
       'serviceWorker' in navigator
        && 'Notification' in window
        ) {

        // already registered
        if (this.fcmRegistration && (this.fcmRegistration.installing || this.fcmRegistration.waiting || this.fcmRegistration.active)) return Promise.resolve(this.fcmRegistration);
        var fcmScope = 'firebase-cloud-messaging-push-scope';
        var swUrl = 'firebase-messaging-sw.js';
        
        this.fcmRegistration = registerValidSW(swUrl, { scope: fcmScope });
        return this.fcmRegistration;
    } else {
        return typeof Promise !== 'undefined' && Promise.resolve(null);
    }
}

