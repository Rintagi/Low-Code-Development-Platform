// Firebase App (the core Firebase SDK) is always required and
// must be listed before other Firebase SDKs
import firebase from "firebase/app";

// Add the Firebase services that you want to use
// import "firebase/auth";
// import "firebase/firestore";
import "firebase/messaging";

// loaded via index.html, global name space i.e. window.
const firebaseConfig = window.firebaseConfig;
const vapidKey = window.vapidKey;
const fcmSWUrl = window.fcmSWUrl;
const fcmSWScope = window.fcmSWScope;
const initializedFirebaseApp = firebaseConfig && firebase.initializeApp(firebaseConfig);
const messaging = navigator.serviceWorker && window.Notification && initializedFirebaseApp && initializedFirebaseApp.messaging();
export { messaging, vapidKey, fcmSWUrl, fcmSWScope };
