/* customized code APPENDED to service-worker.js via importScripts() service worker model
 * or (registered seperately similar to how service-worker.js is registered)
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

/* self is the registered service worker object */

// this file runs in the context of service worker thus is located under public/ root
// AND MUST BE CALLED firebase-messaging-sw.js

/*
global scope properties
https://developer.mozilla.org/en-US/docs/Web/API/ServiceWorkerGlobalScope

// key ones
self.clients
self.registration
self.caches
*/

importScripts('https://www.gstatic.com/firebasejs/8.1.1/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.1.1/firebase-messaging.js');
importScripts('runtime/firebaseConfig.js');

// firebaseConfig is in loaded runtime
firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();

let usrId = undefined;
let basePath = self.fcmAppReactRoot || '';

// this is FCM specific, can't control what notification to show, disabled and use generic one below
false && messaging.onBackgroundMessage(function (payload) {
    //messaging.setBackgroundMessageHandler(function(payload) {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    // Customize notification here
    const notificationTitle = 'Background Message Title';
    const notificationOptions = {
        body: 'Background Message body.',
        icon: '/firebase-logo.png'
    };
    // clients and registration are under global scope
    const promiseChain = clients
        .matchAll({
            type: "window",
            includeUncontrolled: true,
        })
        .then((windowClients) => {
            let hasClient = false;
            let clientWindow = null;
            for (let i = 0; i < windowClients.length; i++) {
                const windowClient = windowClients[i];
                //console.log('[firebase-messaging-sw.js] notify client window ', payload);
                //console.log(windowClient);
                hasClient = true;
                clientWindow = windowClient;
                //windowClient.postMessage(payload);
            }
            return hasClient;
        })
        .then((hasClient) => {
            // this is service worker to front end, nothing to do with FCM
            // equivalent of alert() in front end but not modal
            if (!hasClient && false) {
                console.log('[firebase-messaging-sw.js] show notification ', payload);
                return registration.showNotification(notificationTitle, notificationOptions);
            }
            //else return clientWindow.focus();
        });
    console.log(promiseChain);
    return promiseChain;
});

// this is generic push handling, nothing to do with FCM helper(messaging) but would receive it
self.addEventListener('push', function (event) {
    //console.log('Received a push message', event);
    var messageContent = (event.data && event.data.json()) || {
        data:{
            title: 'Push message',
            body: 'receieved empty message',
            icon: 'images/logo.png',
            click_action: (basePath || '') + '/'            
        }
    };
    //console.log(messageContent);
    var title = ((messageContent || {}).data || {}).title || ((messageContent || {}).notification || {}).title || 'Yay a message.';
    var body = ((messageContent || {}).data || {}).body || ((messageContent || {}).notification || {}).body || 'We have received a push message.';
    var icon = ((messageContent || {}).data || {}).icon || ((messageContent || {}).notification || {}).icon || '/images/logo.png';
    var tag = ((messageContent || {}).data || {}).tag || ((messageContent || {}).notification || {}).tag;
    var forUsrId = ((messageContent || {}).data || {}).forUsrId || ((messageContent || {}).notification || {}).forUsrId;

    // clients and registration are under global scope
    const promiseChain = clients
        .matchAll({
            type: "window",
            includeUncontrolled: true,
        })
        .then((windowClients) => {
            let hasClient = false;
            let clientWindow = null;
            for (let i = 0; i < windowClients.length; i++) {
                const windowClient = windowClients[i];
                console.log('[firebase-messaging-sw.js] client window ', usrId, windowClient);
                hasClient = hasClient || windowClient.visibilityState === 'visible';
                //console.log();
                // hasClient = true;
                // clientWindow = windowClient;
                // do not notify front end, we are in FCM sw which is doing it by itself(i.e. no double)
                // windowClient.postMessage(messageContent);
            }
            return hasClient;
        })
        .then(hasClient => {
            // show notification only if it 
            // not for specific user
            // or current login is specific user
            // or don't know what current login is
            if ((!hasClient
                &&
                (!forUsrId
                    || forUsrId == usrId
                    || (!usrId)
                ))
                // || true
            ) {
                // in the background or not open show notification
                console.log('[firebase-messaging-sw.js] client not visible');
                return self.registration.showNotification(title, {
                    body: body,
                    icon: icon,
                    tag: tag,
                    data: messageContent,
                })
            }
            else
                return null;
        });

    event.waitUntil(
        promiseChain
    );
});

self.addEventListener("notificationclick", function (event) {
    // user interact with notification(not FCM notification but generic SW one like showNotification above) 
    console.log('notification click', event);
    event.notification.close();
    const payload = event.notification.data;
    var url = (basePath || '') + payload.data.click_action;
    console.log(url);
    const promiseChain = clients
        .matchAll({
            type: "window",
            includeUncontrolled: true,
        })
        .then((windowClients) => {
            let hasClient = false;
            for (let i = 0; i < windowClients.length; i++) {
                const windowClient = windowClients[i];
                console.log('[firebase-messaging-sw.js] notify client window on notification click ', usrId, windowClient);
                //console.log(windowClient);
                hasClient = true;
                clientWindow = windowClient;
                //notify client when user ack the notification, align fcm logic treatment
                windowClient.postMessage(payload);
                //can only be path under this control
                //console.log(url);
                //navigatte doesn't work, must be done via the client(message handler)
                clientWindow.navigate(url);
                return clientWindow.focus();
            }
            console.log(event.notification);
            if (!hasClient) {
                if (Notification.prototype.hasOwnProperty('data')) {
                    console.log('Using Data');
                    return clients.openWindow(url);
                }
            }
            return hasClient;
        });
    event.waitUntil(promiseChain); 
});


// communication channel from client side
self.addEventListener('message', (event) => {
    console.log(event);
    if (event.data && event.data.type === 'SET_LOGIN_USER') {
        usrId = event.data.usrId;
        basePath = event.data.basePath || "/";
        // do something
    }
});
