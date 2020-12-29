import { makeBlob } from './utils';
import { v1 as v1uuid, v4 as v4uuid} from 'uuid';

const sjcl = require('sjcl');
const _pbkdf2 = require('pbkdf2');

export function base64Encode(s) {
  return btoa(s)
}

export function base64Decode(s) {
  return atob(s)
}

export function sha256(s) {
  const h = sjcl.hash.sha256.hash(s);
  return h;
}

export function pbkdf2(password, salt, round, keyLength, hashType) {
  return new Promise(function (resolve, reject) { 
    _pbkdf2.pbkdf2(password, salt, round, keyLength, 
      (err, derivedKey)=> {
        if (err) reject(err);
        else resolve(derivedKey);
      }
      )
  });
}

function wordToByteArray(word, length) {
  var ba = [], i, xFF = 0xFF;
  if (length > 0)
      ba.push(word >>> 24);
  if (length > 1)
      ba.push((word >>> 16) & xFF);
  if (length > 2)
      ba.push((word >>> 8) & xFF);
  if (length > 3)
      ba.push(word & xFF);
  return ba;
}

function wordArrayToByteArray(wordArray, length) {
  if (wordArray.hasOwnProperty("sigBytes") && wordArray.hasOwnProperty("words")) {
      length = wordArray.sigBytes;
      wordArray = wordArray.words;
  }
  else {
      length = length * 4;
  }
  var result = [],
      bytes,
      i = 0;
  while (length > 0) {
      bytes = wordToByteArray(wordArray[i], Math.min(4, length));
      length -= bytes.length;
      result.push(bytes);
      i++;
  }
  return [].concat.apply([], result);
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

export function scrambleString(s)
{
  const h = sha256(s);
  return arrayBufferToBase64(wordArrayToByteArray(h, h.length));
  return  s;
}

export function uuid() {
  return v1uuid();  
  //return Array.from((window.crypto || window.msCrypto).getRandomValues(new Uint32Array(4))).map(n => n.toString(16)).join('-');
}

export function uuidv4() {
  return v4uuid();  
  // return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
  //   var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
  //   return v.toString(16);
  // });
}

export function getCurrentReactUrlPath() {
  const href = window.location.href;
  return href.replace(/\?.*$/, '');
}

export function getCurrentReactUrlSearch() {
  const href = window.location.href;
  return href.replace(/^[^?]*\?/, '');
}

export function isTouchDevice() {
  if ('ontouchstart' in window || navigator.msMaxTouchPoints) {
    return true;
  }
}

export function isAndroid() {
  if (navigator.userAgent.includes('Android')) {
    return true;
  }
}

export function isIphone() {
  if (navigator.userAgent.includes('iPhone')) {
    return true;
  }
}

export function isSafari() {
  if (navigator.userAgent.includes('Safari')) {
    return true;
  }
}
export function isHttps() {
  if (window.location.protocol === 'https:') {
    return true;
  }
}

export function IsMobile() {
  return isAndroid() || isIphone();
}

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
    parser.href = parser.href.toString();

    // IE 7 and 6 wont load "protocol" and "host" even with the above workaround,
    // so we take the protocol/host from window.location and place them manually
    if (parser.host === "" && parser.protocol !== "file:") {
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
        if (n && n.tagName === "SCRIPT") {
          var mySource = n.src.match(/^\s*(http.*)(\/static\/js)(\/main.+\.js)\s*$/);
          if (mySource && mySource.length === 4) {
            reactInfo.myJS = mySource[3];
            reactInfo.myJSHostingRoot = mySource[1];  
          }
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

export function base64Codec(urlsafe) {
  var chars = urlsafe
                  ? "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_"
                  : "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

  // Use a lookup table to find the index.
  var lookup = new Uint8Array(256);
  
  for (var i = 0; i < chars.length; i++) {
      lookup[chars.charCodeAt(i)] = i;
  }

  this.encode = function (arraybuffer, nopadding) {
      var bytes = new Uint8Array(arraybuffer),
      i, len = bytes.length, base64 = "";

      for (i = 0; i < len; i += 3) {
          base64 += chars[bytes[i] >> 2];
          base64 += chars[((bytes[i] & 3) << 4) | (bytes[i + 1] >> 4)];
          base64 += chars[((bytes[i + 1] & 15) << 2) | (bytes[i + 2] >> 6)];
          base64 += chars[bytes[i + 2] & 63];
      }

      if ((len % 3) === 2) {
          base64 = base64.substring(0, base64.length - 1) + (urlsafe || nopadding ? "" : "=");
      } else if (len % 3 === 1) {
          base64 = base64.substring(0, base64.length - 2) + (urlsafe || nopadding ? "" : "==");
      }

      return base64;
  };

  this.decode = function (base64) {
      var bufferLength = base64.length * 0.75,
      len = base64.length, i, p = 0,
      encoded1, encoded2, encoded3, encoded4;

      if (base64[base64.length - 1] === "=") {
          bufferLength--;
          if (base64[base64.length - 2] === "=") {
              bufferLength--;
          }
      }

      var arraybuffer = new ArrayBuffer(bufferLength),
      bytes = new Uint8Array(arraybuffer);

      for (i = 0; i < len; i += 4) {
          encoded1 = lookup[base64.charCodeAt(i)];
          encoded2 = lookup[base64.charCodeAt(i + 1)];
          encoded3 = lookup[base64.charCodeAt(i + 2)];
          encoded4 = lookup[base64.charCodeAt(i + 3)];

          bytes[p++] = (encoded1 << 2) | (encoded2 >> 4);
          bytes[p++] = ((encoded2 & 15) << 4) | (encoded3 >> 2);
          bytes[p++] = ((encoded3 & 3) << 6) | (encoded4 & 63);
      }

      return arraybuffer;
  };

  return this;
}

export function ab2str(buf) {
  return String.fromCharCode.apply(null, new Uint16Array(buf));
}

export function str2ab(str) {
  var buf = new ArrayBuffer(str.length * 2); // 2 bytes for each char
  var bufView = new Uint16Array(buf);
  for (var i = 0, strLen = str.length; i < strLen; i++) {
      bufView[i] = str.charCodeAt(i);
  }
  return buf;
}

export function base64UrlEncode(s) {
  return new base64Codec(true).encode;
}

export function base64UrlDecode(s) {
  return new base64Codec(true).decode;
}

export function coerceToArrayBuffer(thing, name) {
  if (typeof thing === "string") {
      thing = thing.replace(/-/g, "+").replace(/_/g, "/");
      var b64Decode = new base64Codec(false).decode;
      thing = b64Decode(thing);
      if (typeof thing === "string") {
        thing = str2ab(thing);
      }
  }

  if (Array.isArray(thing)) {
      thing = new Uint8Array(thing);
  }
  // Uint8Array to ArrayBuffer
  if (thing instanceof Uint8Array) {
      thing = thing.buffer;
  }

  // error if none of the above worked
  if (!(thing instanceof ArrayBuffer)) {
      throw new TypeError("could not coerce '" + name + "' to ArrayBuffer");
  }
  else {
      return new Uint8Array(thing);
  }
}