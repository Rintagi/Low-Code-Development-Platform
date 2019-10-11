import React from 'react';
import App from './app/App';
import {render} from 'react-dom'
import {Provider} from 'react-redux'
import store from './app/store';
import ScrollToTop from './app/ScrollToTop';
import { getUserConfirmation } from './helpers/navigation'
import { createBrowserHistory, useBasename } from "history";
import {HashRouter, BrowserRouter} from "react-router-dom";
import {setupRuntime} from './helpers/utils';
/* Rintagi runtime configuration(rintagi.js under public/runtime) is loaded in index.html so must be accessed via document namespace
 * this is for post-deployment override
 */

const useBrowserRouter = (document.Rintagi || {}).useBrowserRouter;
const appBasename = useBrowserRouter ? (document.Rintagi || {}).appBasename : "/"; // muse use absolute root if using HashRouter to avoid unnecessary path after #
const Router = useBrowserRouter ? BrowserRouter : HashRouter;

// this is only used in development for IIS proxy to webpack dev server or running node.js webserver behind IIS or other proxy server like apache
// the IIS/Apache rewrite must match this, use empty if using HashRouter
const appProxyBasename =  useBrowserRouter ? (document.Rintagi || {}).appProxyBasename : ""; // no ending slash

/* 'feature' in react-router, history props must be set directly at the router level below except when it is directly imported rather than import then use alias  
 * this is ignored even it is being passed
const hist = createBrowserHistory(
  {basename:process.env.NODE_ENV === "development" ? appProxyBasename : appBasename,getUserConfirmation:getUserConfirmation,}
  
);
 */

const baseName = process.env.NODE_ENV === "development" ? appProxyBasename : appBasename;

render(
  <Provider store={store}>
    <Router basename={baseName} getUserConfirmation={getUserConfirmation}>
      <ScrollToTop>
        <App/>
      </ScrollToTop>
    </Router>
  </Provider>,
  document.getElementById('root')
);
