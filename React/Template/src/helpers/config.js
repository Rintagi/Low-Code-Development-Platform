/* configuration setup, like url of web service endpoint etc. this is dom/web based */
import { getReactContainerInfo } from './domutils';

var Fingerprint2 = require('fingerprintjs2');
var sjcl = require('sjcl');
var pbkdf2 = require('pbkdf2');

export let myMachine = null;

export function getMyMachine() {
    return myMachine;
}

export function getFingerPrint() {
    return new Promise(function (resolve, reject) {
        setTimeout(() => {
            Fingerprint2.get(function (components) {
                components.sort();
                //log.debug(components);
                const a = JSON.stringify(components);
                const sha256 = new sjcl.hash.sha256();
                sha256.update(a);
                const h = btoa(sha256.finalize());
                myMachine = h;
                resolve(h);
            })
        }, 500);
    });
};

export function setupRuntime() {
    const rintagi = document.Rintagi || {};
    const location = window.location;
    const href = location.href;
    const pathName = location.pathname;
    const origin = location.origin;
    const reactBase = document.appRelBase || ['React','ReactProxy','ReactPort'];
    const appBase = reactBase.reduce((a,b)=>{
        const regex = new RegExp('.*((/)?' + b + '((/|#)|$))','i');
        const m = pathName.match(regex);
        if (!a && m && m.length > 0) {
          return m[0].replace(m[1],'').replace(/\/$/,'');
        }
        else return a;
    },undefined);
    const apiBasename = origin + (appBase || '');
    const appDomainUrl = origin + (appBase || '');
    rintagi.apiBasename = rintagi.apiBasename || apiBasename;
    rintagi.appDomainUrl = rintagi.appDomainUrl || appDomainUrl;
    rintagi.appNS = rintagi.appNS || appDomainUrl.replace(origin,'') || '/';
    if (location.pathname === "/" && location.protocol === "http:" && location.port >= 3000 && location.port <= 3100) {
        rintagi.apiBasename = (rintagi.localDev || {}).apiBasename || rintagi.apiBasename;
        rintagi.appNS = (rintagi.localDev || {}).appNS || rintagi.appNS;
        rintagi.appDomainUrl = (rintagi.localDev || {}).appDomainUrl || rintagi.appDomainUrl;
    }
    const myUrl = href.match(/^\s*(http[^#]*)(\/?#?)(.*)$/);
    const reactInfo = getReactContainerInfo(document);
    rintagi.myDocumentUrl = myUrl[1];
    rintagi.myJS = reactInfo.myJS;
    rintagi.myJSHostingRoot = reactInfo.myJSHostingRoot;
  
    document.Rintagi = rintagi;
};
  
  
export function getRintagiConfig()
{
    return document.Rintagi;
}

setupRuntime();