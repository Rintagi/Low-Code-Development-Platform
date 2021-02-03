import 'react-app-polyfill/ie11';
import 'react-app-polyfill/stable';
import React from 'react';
import App from './app/App';
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import store from './app/store';
import { Helmet, HelmetProvider } from 'react-helmet-async';
import ScrollToTop from './app/ScrollToTop';
import { getUserConfirmation } from './helpers/navigation'
// import { createBrowserHistory, useBasename } from "history";
import { HashRouter, BrowserRouter } from "react-router-dom";
import log from './helpers/logger'
//import * as serviceWorker from './serviceWorker';
import * as serviceWorker from './registerServiceWorkerV3';
import { setupRuntime, getRintagiConfig } from './helpers/config';

/* sample trivial web worker implementation, can be removed/comment out */
import * as Comlink from 'comlink';
/* eslint-disable import/no-webpack-loader-syntax */
import Foo from './worker/foo_worker';
/* eslint-disable import/no-webpack-loader-syntax */
import Bar from './worker/bar_worker';

async function initFoo() {
  const worker = new Foo();
  const obj = Comlink.wrap(worker);
  const x = await obj.inc();
  console.log(x);
}

async function initBar() {
  const worker = new Bar();
  const obj = Comlink.wrap(worker);
  const x = await obj.inc();
  console.log(x);
}
initFoo();
initBar();

/* Rintagi runtime configuration(rintagi.js under public/runtime) is loaded in index.html so must be accessed via document namespace
 * this is for post-deployment override
 */
const runtimeConfig = getRintagiConfig() || {};

/* BrowserRouter requires server side urlrewrite help as refresh non-root path hit server */
const useBrowserRouter = runtimeConfig.useBrowserRouter;
const appBasename = useBrowserRouter ? runtimeConfig.appBasename : "/"; // muse use absolute root if using HashRouter to avoid unnecessary path after #
const Router = useBrowserRouter ? BrowserRouter : HashRouter;

// this is only used in development for IIS proxy to webpack dev server or running node.js webserver behind IIS or other proxy server like apache
// the IIS/Apache rewrite must match this, use empty if using HashRouter
const appProxyBasename = useBrowserRouter ? runtimeConfig.appProxyBasename : ""; // no ending slash

/* 'feature' in react-router, history props must be set directly at the router level below except when it is directly imported rather than import then use alias  
 * this is ignored even it is being passed
const hist = createBrowserHistory(
  {basename:process.env.NODE_ENV === "development" ? appProxyBasename : appBasename,getUserConfirmation:getUserConfirmation,}
  
);
 */

/* This code enables proper work of touch events on mobile browsers */
document.addEventListener('touchstart', () => { });
document.addEventListener('touchend', () => { });
document.addEventListener('touchcancel', () => { });
document.addEventListener('touchmove', () => { });
/* This code enables proper work of touch events on mobile browsers */

/* override log level, should it be enabled ? */
if (runtimeConfig.logLevel) log.setLevel(runtimeConfig.logLevel);

/* service worker coordination */

// window.addEventListener('popstate', (e) => {'####1', log.debug(e)});
// window.addEventListener('pushstate', (e) => {'####2', log.debug(e)});
// window.addEventListener('replacestate', (e) => {log.debug('####3', e)});

(navigator.serviceWorker && navigator.serviceWorker.addEventListener('controllerchange', function () {
    console.log('running service worker controller changed');
  })
  );

//console.log((navigator.serviceWorker || {}).controller);
 //serviceWorker.register({scope:'/static/'});
serviceWorker.register({updateViaCache:'none'});
 
/* below is app specific */
document.backPath = '/my-network';

window.addEventListener('hashchange', (e) => {
  if (e.newURL && (e.newURL.includes('/my-network') || e.newURL.includes('/marketplace') || e.newURL.includes('/transfers'))) {
    document.backPath = e.newURL.split("#")[1];
    log.debug('####', document.backPath);
  }
});
/* end app specific */


const baseName = process.env.NODE_ENV === "development" ? appProxyBasename : appBasename;

render(
  <HelmetProvider>
    <Provider store={store}>
      <Router basename={baseName} getUserConfirmation={getUserConfirmation}>
        <ScrollToTop>
          <App />
        </ScrollToTop>
      </Router>
    </Provider>
  </HelmetProvider>
  , document.getElementById('root')
);

// import React from 'react';
// import ReactDOM from 'react-dom';
// import './index.css';
// import App from './App';
// import * as serviceWorker from './serviceWorker';

// ReactDOM.render(
//   <React.StrictMode>
//     <App />
//   </React.StrictMode>,
//   document.getElementById('root')
// );

// // If you want your app to work offline and load faster, you can change
// // unregister() to register() below. Note this comes with some pitfalls.
// // Learn more about service workers: https://bit.ly/CRA-PWA
// serviceWorker.unregister();
