/* this is a stub for react native, 
 * most call will fail as there is no navigator or window or document object
 * so they should not be called from react native(i.e. the front end part at all)
 * only url parsing works as it is replaced by url package
 */
import { makeBlob } from './utils';
import url from 'url';
import { base64Encode as _base64Encode, base64Decode as _base64Decode } from './Base64Utils';
import { _sha256 } from './sha256';
import { hexToByteArray } from './sha1';
import { PBKDF2 } from './pbkdf2';

export function base64Decode(s) {
  return _base64Decode(s);
}
export function base64Encode(s) {
  return _base64Encode(s);
}
export function arrayBufferToBase64(buffer) {
  var binary = '';
  var bytes = new Uint8Array(buffer);
  var len = bytes.byteLength;
  for (var i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
  }
  return base64Encode(binary);
}

export function sha256(s) {
  return _sha256(s);
}

export function pbkdf2(password, salt, round, keyLength, hashType) {
  return new Promise(function (resolve, reject) { 
    var mypbkdf2 = new PBKDF2(password, salt, round, keyLength);
    mypbkdf2.deriveKey(
      progress=>{
        //console.log(progress + "% completed")
      }
      , derivedKey=>{ 
        resolve(hexToByteArray(derivedKey))
      }
      );
  });
}

export function uuid() {
  // this is not crypto safe
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function uuidv4() {
  // this is not crypto safe
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function scrambleString(s)
{
  const h = sha256(s);
  return arrayBufferToBase64(h);
}

export function parsedUrl(urlString) {
  // replace by non-dom dependent library
  return url.parse(urlString);
}

export function setCookie(name, value, days, path) {
  return;
}

export function getCookie(name) {
  return null;
}
export function eraseCookie(name) {
  return;
}

/* below will fail on react-native DOM and should never be called !!! */
export function getCurrentReactUrlPath() {
  if (typeof window === 'undefined') return '';
  const href = ((window || {}).location || {}).href || '/';
  return href.replace(/\?.*$/, '');
}

export function getCurrentReactUrlSearch() {
  if (typeof window === 'undefined') return '';
  const href = ((window || {}).location || {}).href || '';
  return href.replace(/^[^?]*\?/, '');
}

export function isTouchDevice() {
  if (typeof window === 'undefined') return true;
  if ('ontouchstart' in window || (navigator || {}).msMaxTouchPoints) {
    return true;
  }
}

export function isAndroid() {
  if (typeof window === 'undefined') return true;
  if (((navigator || {}).userAgent || '').includes('Android')) {
    return true;
  }
}

export function isIphone() {
  if (typeof window === 'undefined') return true;
  if (((navigator || {}).userAgent || '').includes('Android')) {
    return true;
  }
}

export function isHttps() {
  if (typeof window === 'undefined') return true;
  if ((((window || {}).location||{}).protocol || {}) === 'https:') {
    return true;
  }
}


export function previewContent({ dataObj, winObj, containerUrl, download, isImage, features, replace, dataPromise, previewSig } = {}) {
    if (typeof window === 'undefined') return undefined;

    const isIE = (window || {}).navigator && (window || {}).navigator.msSaveOrOpenBlob && false;
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


export function getReactContainerInfo(documentObject={}) {
    if (typeof document === 'undefined') return true;

    // this only work if it is using the stand minimalist layout of React
    // i.e. id='root' inside body of index.html and there is NO other
    // script file inside body and build script out something like /static/js/xxxx.js
    // adjust according if needed
    const reactInfo = {}
    if (!documentObject) return reactInfo;
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
    if (typeof window === 'undefined') return {};
    if (window && (httpRet.status === 200 || httpRet.status === 304)) {
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
  