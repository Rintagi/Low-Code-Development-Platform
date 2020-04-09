import { makeBlob } from './utils';

export function previewContent({ dataObj, winObj, containerUrl, download, isImage, features, replace, dataPromise, previewSig } = {}) {
  
    const isIE = window.navigator && window.navigator.msSaveOrOpenBlob && false;
    const containerPage = containerUrl 
                          || process.env.PUBLIC_URL + '/helper/showAttachment.html' + (previewSig ? '?fileSig=' + previewSig : '');
    const win = !isIE && (winObj || window.open(containerPage, '_blank', features || "", replace));
    const dataAvailable = dataObj && dataObj.base64;
  
    const deliverContent = function (dataObj) {
      if (previewSig) {
        try {
          sessionStorage.setItem("PreviewAttachment", JSON.stringify({ ...dataObj, fileSig: previewSig }));
        } catch (e) {
  
        }
      }
  
      if ((!isIE || (/image/i).test((dataObj || {}).mimeType)) && !download) {
        if (win) {
          win.postMessage({ ...dataObj, fileSig: previewSig, download }, "*");
        }
      }
      else {
        const fileObjWithBlob = makeBlob(dataObj);
        const blob = (fileObjWithBlob || {}).blob;
        const fileName = (fileObjWithBlob || {}).fileName;
        if (isIE) {
          window.navigator.msSaveOrOpenBlob(blob, fileName);
        }
        else {
          const a = document.createElement('a');
          const url = URL.createObjectURL(blob);
          a.href = url;
          a.download = (fileObjWithBlob || {}).fileName;
          document.body.appendChild(a);
          a.click();
          setTimeout(function () {
            document.body.removeChild(a);
            window.close();
          }, 500);
        }
      }
    }
    let injected = false;
    const injectContent = function () {
      if (injected) return;
      injected = true;
      if (dataObj && dataObj.base64) {
        deliverContent(dataObj);
      }
      else if (typeof dataPromise === "function" 
            || typeof dataPromise === "object") {
        if (typeof dataPromise.then === "function") {
          dataPromise
          .then(dataJSON => {
              deliverContent({ ...(dataObj || {}), ...dataJSON, download });
              }
            )
          .catch(error => {
              if (win) win.close();
            })
        }
        else {
          const dataJSON = dataPromise();
          deliverContent({ ...(dataObj || {}), ...dataJSON, download });
        }
      }
      else {
        if (win) win.close();
        // no data, no data callback do nothing ?
      }
    }
  
    setTimeout(() => {
      injectContent();
    }, 1000);
  
    if (win) {
      win.onload = function () {
        injectContent();
      }
    }
  
    return win;
  }

export function parsedUrl(url) {
    var parser = document.createElement("a");
    parser.href = url;
    var o = {};
    // IE 8 and 9 dont load the attributes "protocol" and "host" in case the source URL
    // is just a pathname, that is, "/example" and not "http://domain.com/example".
    parser.href = parser.href;

    // IE 7 and 6 wont load "protocol" and "host" even with the above workaround,
    // so we take the protocol/host from window.location and place them manually
    if (parser.host === "") {
        var newProtocolAndHost = window.location.protocol + "//" + window.location.host;
        if (url.charAt(1) === "/") {
            parser.href = newProtocolAndHost + url;
        } else {
            // the regex gets everything up to the last "/"
            // /path/takesEverythingUpToAndIncludingTheLastForwardSlash/thisIsIgnored
            // "/" is inserted before because IE takes it of from pathname
            var currentFolder = ("/" + parser.pathname).match(/.*\//)[0];
            parser.href = newProtocolAndHost + currentFolder + url;
        }
    }

    // copies all the properties to this object
    var properties = ['host', 'hostname', 'hash', 'href', 'port', 'protocol', 'search'];
    for (var i = 0, n = properties.length; i < n; i++) {
        o[properties[i]] = parser[properties[i]];
    }

    // pathname is special because IE takes the "/" of the starting of pathname
    o.pathname = (parser.pathname.charAt(0) !== "/" ? "/" : "") + parser.pathname;
    return o;
}

export function setCookie(name, value, days, path) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    const href = window.location.href;
    document.cookie = name + "=" + (value || "") + expires + "; path=" + (path || "/") + ";secure";
}

export function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
export function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;path=/;';
}

export function getReactContainerInfo(documentObject={document}) {
    // this only work if it is using the stand minimalist layout of React
    // i.e. id='root' inside body of index.html and there is NO other
    // script file inside body and build script out something like /static/js/xxxx.js
    // adjust according if needed
    const reactInfo = {}
    for (var c in documentObject.body.childNodes) {
      try {
        var n = documentObject.body.childNodes[c];
        if (n.tagName === "SCRIPT") {
          var mySource = n.src.match(/^\s*(http.*)(\/static\/js\/)(.+\.js)\s*$/);
          reactInfo.myJS = mySource[3];
          reactInfo.myJSHostingRoot = mySource[1];
        }  
      } catch (e) {
        console.log(e);
      }
    }
    return reactInfo;
  }
  
export function getReactContainerStatus(httpRet) {
    if (httpRet.status === 200 || httpRet.status === 304) {
        const content = httpRet.content;
        const parser = new DOMParser();
        const doc = parser.parseFromString(content, 'text/html');
        const latest = getReactContainerInfo(doc);
        const latestContainer = {
        date: httpRet.headers.get('Date'),
        etag: httpRet.headers.get('ETag'),
        lastModified: httpRet.headers.get('Last-Modified'),
        status: httpRet.status,
        url: httpRet.url,
        redirected: httpRet.redirected,
        ok: httpRet.ok,
        ...latest,
        }
        return latestContainer;  
    }
    else return {}
}
  