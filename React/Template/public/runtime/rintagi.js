/* this is runtime loading script for actual installation(production) configuration override say putting app to deep directory structure or
 * web service end point not the same as the app loading source
 * typically for situation where the apps are hosted in CDN and/or not at root level of the domain
 * for reactjs configuration, make sure homepage is set to "./" so everything generated is relative 
 */
document.Rintagi = {
    systemId:3,
    useBrowserRouter: false,    // whether to use # based router(default) or standard browser based router(set to true, need server rewrite support, cannot be used for CDN or static file directory)
    appNS:'/RO',              // name space for storage key/cookie name formation for usage of multiple app hanging under same domain
    appDomainUrl:'http://localhost/RO', // master domain this app is targetting, empty/null means the same as apiBasename, no ending slash, design for multiple api endpoint usage(js hosting not the same as webservice hosting)
    appBasename: "RO/react/Adm", // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
    appProxyBasename: "RO/reactproxy", // basename after domain where all the react stuff is seated , no ending slash, only used for browserRouter as basename
    apiBasename: "http://localhost/RO" // webservice url, can be relative or full http:// etc., no ending slash
}