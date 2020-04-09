import React from 'react';
import App from './app/App';
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import store from './app/store';
import ScrollToTop from './app/ScrollToTop';
import { getUserConfirmation } from './helpers/navigation'
// import { createBrowserHistory, useBasename } from "history";
import { HashRouter, BrowserRouter } from "react-router-dom";
import log from './helpers/logger'
import * as serviceWorker from './registerServiceWorker';
import { setupRuntime, getRintagiConfig } from './helpers/config';

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
    console.log('service work controller changed');
  })
  );

console.log((navigator.serviceWorker || {}).controller);
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
  <Provider store={store}>
    <Router basename={baseName} getUserConfirmation={getUserConfirmation}>
      <ScrollToTop>
        <App />
      </ScrollToTop>
    </Router>
  </Provider>,
  document.getElementById('root')
);
