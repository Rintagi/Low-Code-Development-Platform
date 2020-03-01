
/* this is runtime loading script for actual installation(production) configuration override say putting app to deep directory structure or
 * web service end point not the same as the app loading source
 * typically for situation where the apps are hosted in CDN and/or not at root level of the domain
 * for reactjs configuration, make sure homepage is set to './' so everything generated is relative 
 */
document.Rintagi = {
  localDev:{
    // these setup is only effective when served via app is served via http://localhost:3000 type, for local npm start development. ignored in production build or proxying to localhost
    appNS:'/RO',
    appDomainUrl:'http://RCPRO/RO', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
    apiBasename: 'http://rcdev03/RO', // webservice url for local development via npm start, i.e. localhost:3000 etc. must be full url in the form of http:// pointing to the site serving , no ending slash
  },
  appRelBase:['React','ReactProxy','ReactPort'],  // path this app is serving UNDER(can be multiple), implicitly assume they are actually /Name/, do not put begin/end slash 
  appNS:'', // used for login token sync(shared login when served under the same domain) between apps and asp.net site
  appDomainUrl:'', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
  apiBasename: '', // webservice url, can be relative or full http:// etc., no ending slash
  useBrowserRouter: false,    // whether to use # based router(default) or standard browser based router(set to true, need server rewrite support, cannot be used for CDN or static file directory)
  appBasename: 'RO/react/Adm', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  appProxyBasename: 'RO/reactproxy', // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
  systemId: 3                
}

