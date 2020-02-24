import log from './logger';
import { valueToNumber, toCapital, toLocalDateFormat, formatContent } from './formatter';

export function getSelectedFromList(key, keyList, keyColumeName, defaultObj) {
  const x = keyList.filter((v) => v[keyColumeName] === key);
  return x;
}

export function objectListToDropdownList(objectList, fn) {
  return objectList
    .map((v, i) => fn(v, i))
    .map(o => ({
      ...o,
      label: toCapital(o.label)
    }));
}

export function objectListToDropdownListNoFormat(objectList, fn) {
  return objectList
    .map((v, i) => fn(v, i))
    .map(o => ({
      ...o,
      label: o.label
    }));
}

export function ToReactDropdownObj(keyName, labelName) {
  return function (o) {
    return {
      ...o // preserve existing data including Ddl additional column
      // standard value for React including label that cannot be empty(need single space)
      , key: (o['value'] || o['key'] || o[keyName] || "").trim()
      , label: o['label'] || o[labelName] || ' '
      , value: (o['value'] || o['key'] || o[keyName] || "").trim()
    }
  }
}

export function ToReactDropdownList(objectList, keyName, labelName, noCapitalFormat) {
  if (noCapitalFormat) {
    return objectListToDropdownListNoFormat(objectList || [], ToReactDropdownObj(keyName, labelName)).filter(o => o.key);
  }
  else {
    return objectListToDropdownList(objectList || [], ToReactDropdownObj(keyName, labelName)).filter(o => o.key);
  }
}

export function objectListToDict(objectList, keyColumnName, fn) {
  return objectList.length === 0 ? {} : objectList.reduce((r, v, i, a) => {
    r[v[keyColumnName]] = (typeof fn === "function") ? fn(v) : v;
    return r
  }, {});
}

export function mergeArray(l1, l2, keyFn) {
  if (l1.length === 0) return l2;
  const o = l1.reduce((a, v) => { a[keyFn(v)] = v; return a; }, {});
  return [
    ...l1
      .map(o => ({
        ...o,
        label: toCapital(o.label)
      }))
    ,
    ...l2.filter(v => !o[keyFn(v)] && keyFn(v))
  ];
}

export function naviPath(mstId,dtlId,path)
{ 
  return path.replace(/:mstId\?/,mstId || '_').replace(/:dtlId\?/,dtlId || '_');
}

export function getNaviPath(naviBar,type,fallbackPath)
{
    return naviBar.reduce((a,v)=>v.type===type ? v.path: a,fallbackPath);
}
export function getAddMstPath(path)
{
    return path.replace(/[0-9]*$/,"").replace(/_$/,"");
}
export function getAddDtlPath(path)
{
    return path.replace(/[0-9]*$/,"").replace(/_$/,"");
}
export function getEditMstPath(path,mstId)
{
    return path.replace(/_$/,mstId||"");
}
export function getEditDtlPath(path,dtlId)
{
    return path.replace(/_$/,dtlId||"");
}
export function getDefaultPath(path)
{
    return path.replace(/\:.*\?/,"_");
}

export function deepEqualX(o1, o2, important) {
  const x = o1 === o2 ||
    (Array.isArray(o1) && Array.isArray(o2) && o1.length === o2.length && (o1.length === 0 || o1.filter((o, i) => o !== o2[i]).length === 0)) ||
    (typeof o1 === typeof o2 && !Array.isArray(o1) && typeof o1 === "object" && (important || Object.keys(o1)).filter(k =>
      !Object.is(o1[k], o2[k])
      && (
        (!Array.isArray(o1[k]) && typeof o1[k] !== "object") ||
        (Array.isArray(o1[k]) && !deepEqualX(o1[k], o2[k])) ||
        (typeof o1[k] === "object" && !deepEqualX(o1[k], o2[k]))
      )).length === 0)
  return x;
}

export function deepNotEqualX(o1, o2, important) {
  return (typeof o1 !== typeof o2)
    || (Array.isArray(o1) && Array.isArray(o2) && (o1.length !== o2.length || o1.filter((o, i) => o !== o2[i]).length > 0))
    || (typeof o1 === "object" && !Array.isArray(o1) && (important || Object.keys(o1)).filter(k =>
      (Array.isArray(o1[k]) && deepEqualX(o1[k], o2[k]))
      || (typeof o1[k] === "object" && deepEqualX(o1[k], o2[k]))
      || Object.is(o1[k], o2[k])
    ).length === 0)
    || (o1 !== o2 && (typeof o1 !== "object" && !Array.isArray(o1)))
}
export function isPhoneFormat(v) {
  return (v && new RegExp(/([0-9\s\-]{8,})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/).test(v));
}
export function isUrlFormat(v) {
  // return (v && new RegExp(/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/).test(v));
  return (v && new RegExp(/^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-zA-Z0-9]+([\-\.]{1}[a-zA-Z0-9]+)*\.[a-zA-Z]{2,5}(:[0-9]{1,5})?(\/.*)?$/g).test(v));
}
export function isLinkedinFormat(v) {
  // return (v && new RegExp(/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/).test(v));
  return (v && new RegExp(/(https?)?:?(\/\/)?(([w]{3}||\w\w)\.)?linkedin.com(\w+:{0,1}\w*@)?(\S+)(:([0-9])+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/g).test(v));
}
export function isEmailFormat(v) {
  // return (v && new RegExp(/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/).test(v));
  return (v && new RegExp(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,10}$/i).test(v));
}
export function isVideoFormat(v) {
  return (v && (new RegExp(/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)/g).test(v) || new RegExp(/^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$/g).test(v)));
}
export function isVimeoFormat(v) {
  return (v && new RegExp(/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)/g).test(v));
}
export function getVimeoId(url) {
  var regExp = /(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/(?:[^\/]*)\/videos\/|album\/(?:\d+)\/video\/|video\/|)(\d+)(?:[a-zA-Z0-9_\-]+)?/i;
  var match = url.match(regExp);

  return match[1];
}
export function isYouTubeFormat(v) {
  return (v && new RegExp(/^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$/g).test(v));
}
export function getYouTubeId(url) {
  var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
  var match = url.match(regExp);

  return match && match[2];
}
export function isCreditCardFormat(v) {
  return (v && new RegExp(/^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/).test(v));
}
export function isCVVFormat(v) {
  return (v && new RegExp(/^[0-9]{3,4}$/).test(v));
}

export function isEmptyId(val) {
  return !val || (typeof val === 'string' && val.trim() === "");
}
export function isEmpty(val) {
  return !val || (typeof val === 'string' && val.trim() === "");
}
export function isEmptyArray(value) {
  return (Array.isArray(value) && value.length === 0)
}
export function isEmptyObject(value) {
  return (Array.isArray(value) && value.length === 0)
    // only simple object test, anything that is not simple we assume it is not empty(including Date which has no key)
    || (!Array.isArray(value) && typeof value === 'object' && value.constructor === Object && Object.keys(value).length === 0)
}
export function isValidRange(lowerBound, upperBound, defaultIfNaN) {
  return (val) => { const x = val !== undefined && val !== null && val !== "" && typeof val !== "object" ? val : (defaultIfNaN); return (+x) >= lowerBound && (+x) <= upperBound };
}
export function isNotEmpty(val) {
  return !isEmpty(val);
}
export function isNot(f) {
  return function (val) {
    return !f(val);
  }
}

export function isNumber(val) {
  try {
    // const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.)\d+)?$/);
    var result = !isEmpty(val) && regex.test(val);
    return result;
  } catch (e) {
    return false;
  }
}
export function isPositiveNumber(val) {
  {
    try {
      // const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
      // const regex = new RegExp(/^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$/);
      const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.)\d+)?$/);
      var result = !isEmpty(val) &&
        // valueToNumber(val) > 0 &&
        regex.test(val);
      return result;
    } catch (e) {
      return false;
    }
  }
}

export function isWholeNumber(val) {
  try {
    var result = Number.isInteger(parseFloat(val));
    return result;
  } catch (e) {
    return false;
  }
}

export function allowTwoDecimals(val) {
  {
    try {
      const regex = new RegExp(/^\d*((\.)\d{0,2})?$/);
      var result = !isEmpty(val) && regex.test(val);
      return result;
    } catch (e) {
      return false;
    }
  }
}

export function lengthBetween(lowerBound, upperBound) {
  return (strVal) => typeof strVal === 'string' && strVal.length >= lowerBound && strVal.length <= upperBound;
}
export function oneOf(val, listOfFn = []) {
  for (var i = 0; i < listOfFn.length; i = i + 1) {
    if (listOfFn[i](val)) return true;
  }
  return false;
}
export function allOf(val, listOfFn = []) {
  for (var i = 0; i < listOfFn.length; i = i + 1) {
    if (!listOfFn[i](val)) return false;
  }
  return true;
}

export function range(n) {
  return Array.from({ length: n }, (v, k) => k)
}
export function valOf(o) {
  return typeof o === "object" && o.constructor === Object && !Array.isArray(o) ? o.value : o
}

export function deepFlatten(o) {
  const into = []
  const _flatten = function (o, prefix) {
    return Object.keys(o).reduce((a, k) => {
      const xk = (prefix ? prefix + "." : "") + k
      const val = o[k];
      if (val.constructor === Object || val.constructor === Array) {
        return { ...a, ..._flatten(val, xk) }
      }
      else {
        return { ...a, [xk]: val }
      }
    }, into)
  }
  return _flatten(o, null)
}

export function randomMilSec(min, max) {
  return Math.floor(Math.random() * max) + min
}
export function delay(valOrFnOrPromise, t) {
  // t = 0 means still setTimeout(i.e. trampoline), t === null or undefined means immediate call in the chain
  const _this = this;
  return new Promise(function (resolve, reject) {
    const f = (() => {
      const isPromise = typeof (valOrFnOrPromise || {}).then === "function";
      if (typeof valOrFnOrPromise === "function" || isPromise) {
        try {
          if (isPromise) {
              valOrFnOrPromise
              .then((...args)=>{
                resolve.bind(_this)(...args);
              }
              )
              .catch(e=>{
                reject.bind(_this)(e);
              })
          }
          else {
            const result = valOrFnOrPromise()
            resolve.bind(_this)(result)  
          }
        } catch (e) {
          reject.bind(_this)(e)
        }
      }
      else
        resolve.bind(_this)(valOrFnOrPromise)
    }).bind(_this);
    if (t === undefined || t === null) f();
    else setTimeout(f, isNaN(t) || t < 0 ? 0 : t);
  });
}


export function matchPattern(regex) {
  log.debug(regex);
  return (strVal) => typeof strVal === 'string' && strVal && new RegExp(regex).test(strVal);
}

/* these are for complex file objects used for file upload in React component */
export function makeReactFileObject(o) {
  return o.base64 || o.thumb ? { ...o, previewUrl: 'data:' + o.mimeType + ';base64,' + (o.thumb || o.base64) } : o;
}

export function fromBlobToFileObject(blob) {
  try {
    return JSON.parse(blob);
  }
  catch (e) {
    return {
      fileName: '',
      base64: blob,
    }
  }
}

export function debounce(func, debObj, immediate) {
  // var timeout;
  return function () {
    var context = this, args = arguments;
    // log.debug(context, args);
    var later = function () {
      debObj.timeout = null;
      if (!immediate) func.apply(context, args);
    };
    var callNow = immediate && !debObj.timeout;
    // log.debug(timeout);
    clearTimeout(debObj.timeout);
    debObj.timeout = setTimeout(later, debObj.waitTime);
    // log.debug(timeout);
    if (callNow) func.apply(context, args);
  }
}

export function destructure(obj, jsonFields) {
  const x = jsonFields.reduce((a, o) => { if (a[o] && typeof (a[o]) === "string") a[o] = makeReactFileObject(fromBlobToFileObject(a[o])); return a; }, { ...obj });
  return x;
}
export function toJsonValues(obj, jsonFields, columDef) {
  const x = jsonFields.reduce((a, name) => {
    const v = a[name];
    const fileNameColumn = ((columDef || {})[name] || {}).fileNameColumn;
    const tsColumn = ((columDef || {})[name] || {}).timeStampColumn;
    //const ts = v && (v.ts || ((v[0] || {}).ts));
    const ts = v && (v.ts || (Array.isArray(v) && (v.filter(f => f && !f.isEmptyFileObject && f.ts)[0] || {}).ts));
    const fileName = v && (ts || (Array.isArray(v) && v.filter(f => !f.isEmptyFileObject).length === 0)) && (v.fileName || ((v || []).filter(f => f && !f.isEmptyFileObject).map(f => (f && f.fileName) || '').join(",")));
    a[name] = JSON.stringify(cleanupReactFileObject(v, columDef, name));
    if (fileNameColumn && fileName) a[fileNameColumn] = fileName;
    if (tsColumn && ts) a[tsColumn] = ts + '';
    return a;
  }, { ...obj });
  return x;
}

export function reviseEmbeddedFileObjectFromServer(list, o) {
  if (!o) return list;
  let file = o;
  try {
    file = JSON.parse(o)
  } catch (e) {

    file = typeof o === "string" ? {
      base64: o,
      fileName: 'image',
      mimeType: 'image/jpeg'
    } : Array.isArray(o) && o.length > 0 ? o : undefined
  }
  return file ? (Array.isArray(file) ? file : [file]) : list;
  return list || (file && [file])
}

export function cleanupReactFileObject(f, columDef, objName) {
  const placeHolder = !f || (objName && ((columDef || {})[objName] || {}).keyId) ? "" : btoa(JSON.stringify({ ts: f.ts }));
  const _x = (f) => (
    !f.base64 || !f.ts // .ts is the only content that indicate new selection 
      ? {
      }
      : {
        base64: true ? placeHolder : f.base64,
        fileName: f.fileName,
        lastModified: f.lastModified,
        mimeType: f.mimeType,
        size: f.size,
        height: f.height,
        width: f.width,
      })
  return !f ? (f || undefined) : (!Array.isArray(f) ? _x(f) : f.filter(o => o && !o.isEmptyFileObject).map(_x))
}

/* these are for embedded doc upload/download, should be in _ScreenReducer as their are redux related but given the current reducers are not inheriting
, put it here */
export function downloadEmbeddedDoc(downloadEmbeddedService, mstId, dtlId, isMaster, screenColumnName, resultColumeName, reduxColumnName, { reduxRowIdKeyName, dispatch, actionTypeSuccess, actionTypeFailed }) {
  const options = {};
  return {
    screenColumnName: screenColumnName,
    MstId: mstId,
    DtlId: dtlId,
    isMaster: isMaster,

    downloadRequest: downloadEmbeddedService(mstId, dtlId, isMaster, screenColumnName, options)
      .then(data => {
        if (dispatch && actionTypeSuccess) {
          log.debug('on demand download return', reduxColumnName)
          delay(() => { dispatch({ type: actionTypeSuccess, payload: { mstId, dtlId, isMaster, fileObject: data.data[0][resultColumeName], reduxRowIdKeyName, reduxColumnName } }) }, 0);
        }
        return Promise.all(data.data);
      }
      )
  }
}


// export function uploadEmbeddedDoc(saveEmbeddedService, mstId, dtlId, isMaster, screenColumnName, fileList, { dispatch, reduxColumeName, actionTypeSuccess, actionTypeFailed }) {
//   const docId = null, overwrite = true, options = {};

//   return {
//     screenColumnName: screenColumnName,
//     mstId,
//     dtlId,
//     isMaster,
//     file: fileList,
//     actionTypeSuccess,
//     fileName: fileList && fileList.length > 0 ? fileList[0].fileName : '',
//     uploadRequest: !fileList ? Promise.resolve({ screenColumnName, reduxColumeName, result: fileList }) : saveEmbeddedService(mstId, dtlId, isMaster, screenColumnName, typeof fileList === "string" ? fileList : JSON.stringify(fileList), options)
//   }
// }

export function uploadEmbeddedDoc(saveEmbeddedService, mstId, dtlId, isMaster, screenColumnName, fileList, { dispatch, reduxColumeName, actionTypeSuccess, actionTypeFailed }) {
  const docId = null, overwrite = true, options = {};

  return {
    screenColumnName: screenColumnName,
    mstId,
    dtlId,
    isMaster,
    file: fileList,
    actionTypeSuccess,
    fileName: fileList && Array.isArray(fileList) && fileList.length > 0 ? fileList[0].fileName : '',
    uploadRequest: !fileList || (Array.isArray(fileList) && fileList.length > 0 && fileList.filter(f => f.ts).length === 0 && fileList.filter(f => !f.isEmptyFileObject).length > 0)
      ? Promise.resolve({ screenColumnName, reduxColumeName, result: fileList })
      : saveEmbeddedService(mstId, dtlId, isMaster, screenColumnName, typeof fileList === "string" ? fileList : ((fileList.length > 0 && fileList.filter(f => !f.isEmptyFileObject).length === 0) ? null : JSON.stringify(fileList.filter(f => !f.isEmptyFileObject).map(f => ({ ...f, icon: undefined })))), options)
  }
}

export function removeDocList(list, docIdList) {
  let removedList = docIdList;
  try {
    removedList = JSON.parse(docIdList || "[]")
  } catch (e) { }
  const x = removedList.reduce((a, o) => { a[o] = o; return a; }, {});
  return (list || []).filter(o => !o.DocId || (o.DocId && !x[o.DocId]))
}

export function reviseDocList(list, newFile) {
  const lookupByName = (list || []).reduce((a, o) => { a[o.fileName] = o; return a; }, {})
  const lookupByDocId = (list || []).reduce((a, o) => { a[o.DocId] = o; return a; }, {});
  log.debug(lookupByName, lookupByDocId, list, newFile, lookupByName[newFile.fileName]);
  if (lookupByDocId[newFile.DocId]) {
    return (list || []).map(o => o.DocId === newFile.DocId ? { ...o, ...newFile } : o);
  }
  else if (lookupByName[newFile.fileName]) {
    return (list || []).map(o => o.fileName === newFile.fileName ? { ...o, ...newFile } : o);
  }
  else { return [...(list || []), newFile] }
}

export function uploadMultiDoc(uploadService, mstId, dtlId, isMaster, screenColumnName, fileList, removeList, {reduxColumnName,  dispatch, actionTypeSuccess, actionTypeFailed }) {
  if (!fileList || fileList.length === 0) return []
  const docId = null, overwrite = true, options = {};
  return fileList.map(f => {
    const fileJSON = JSON.stringify(f);
    return {
      screenColumnName: screenColumnName,
      isMaster,
      file: { ...f, ts: undefined },
      removeList,
      actionTypeSuccess,
      fileName: f.fileName,
      reduxColumnName: reduxColumnName || screenColumnName,
      keyId:isMaster ? mstId : dtlId,
      uploadRequest: 
        uploadService(mstId, dtlId, isMaster, docId, overwrite, screenColumnName, typeof f === "string" ? f : fileJSON, options)
        .then(result=>{
          return result;
        }
        )
        .catch(error=>{
          log.debug("upload multi doc error", mstId, dtlId, isMaster, docId, screenColumnName, f, fileJSON);
          return Promise.reject(error);
        })
    }
  })
}

export function removeMultiDoc(removeService, mstId, dtlId, isMaster, screenColumnName, docIdList, { dispatch, actionTypeSuccess, actionTypeFailed }) {
  const docId = null, overwrite = true, options = {};
  return {
    screenColumnName: screenColumnName,
    isMaster,
    docList: docIdList,
    actionTypeSuccess,
    uploadRequest: !docIdList || docIdList.length === 0 ? Promise.resolve({ screenColumnName, result: docIdList }) : removeService(mstId, dtlId, isMaster, screenColumnName, docIdList, options)
  }
}

export function downloadMultiDoc(downloadService, mstId, dtlId, isMaster, screenColumnName, docIdList, { reduxColumnName, dispatch, actionTypeSuccess, actionTypeFailed }) {
  const options = {}
  return docIdList.map(f => {
    return {
      screenColumnName: screenColumnName,
      mstId,
      dtlId,
      isMaster,
      DocId: f.DocId,
      downloadRequest: downloadService(mstId, dtlId, isMaster, f.DocId, screenColumnName)
        .then(
          data => {
            if (dispatch && actionTypeSuccess) {
              delay(() => { dispatch({ type: actionTypeSuccess, payload: { result:{...f, ...data.data[0]}, reduxColumnName:reduxColumnName } }) }, 0)
            }
            return Promise.all(data.data);
          }
        )
    }
  })
}

export function downloadMultiDocContent({dispatch, reduxColumnName,  keyId, screenColumnName, actionTypeSuccess, actionTypeFailed, downloadService, docList, firstN}) {
  const currentMstId = keyId;
  const list = (docList || []).reduce((a,o,i)=>{ if (o.DocId && (!firstN || a.length < firstN)) a.push(o); return a;},[])
  const downloadPromises = downloadMultiDoc(downloadService.GetDoc, currentMstId, null, true, screenColumnName, list
      , {reduxColumnName:reduxColumnName, dispatch, actionTypeSuccess: actionTypeSuccess, actionTypeFailed:actionTypeFailed});
  return Promise.all( [...(downloadPromises || []).map(v=>v.downloadRequest)])
              .then(([result])=>{
                log.debug(result);
                return {
                  base64:result[0].DocImage,
                  fileName:result[0].DocName,
                  mimeType:result[0].MimeType,
                }
              })
              .catch(
                error=>{
                  return Promise.reject(error);
                }
              );
}

export function processMultiDoc(existingDocList, newDocList)
{
  const latestDocs = (newDocList || []).filter(o => !o.isEmptyFileObject);
  const currentDocsLookup = (existingDocList || []).filter(o => o.DocId).reduce((a, o) => { a[o.DocId] = o; return a; }, {})
  const revisedDocsLookup = latestDocs.filter(o => o.DocId).reduce((a, o) => { a[o.DocId] = o; return a; }, {});
  const removedDocs = existingDocList && Array.isArray(existingDocList) && existingDocList.filter(o => !revisedDocsLookup[o.DocId]);
  const newDocs = latestDocs.filter(o => (!o.DocId && o.base64) || (o.base64 && (currentDocsLookup[o.DocId] || {}).base64 !== o.base64));
  const mergedLatestDocs = latestDocs.map(o => ({
    ...o,
    base64: o.base64 || (o.DocId && (currentDocsLookup[o.DocId] || {}).base64),
    mimeType: o.mimeType || (o.DocId && (currentDocsLookup[o.DocId] || {}).mimeType),
    ts: undefined,
  }));
  return {
    newDocs:newDocs,
    removedDocs:removedDocs,
    mergedLatestDocs:mergedLatestDocs,
  }
}

export function reviseMultiDocFileObjectFromServer(list, o) {
  // update local multidoc file list from server multi-doc object
  return !Array.isArray(list) ? list : list.map(d => (d.DocId === o.DocId ? { ...d, mimeType: o.MimeType, base64: o.DocImage } : d))
}

export function makeMultiDocFileObjectFromServer(o, MstKeyName) {
  // convert server multi-doc list return to file object used in react
  return {
    fileName: o.DocName,
    DocId: o.DocId,
    MasterId: o[MstKeyName],
  }
}

export function makeBlob(fileObjWithBase64) {
  if (!fileObjWithBase64 || !fileObjWithBase64.base64) return fileObjWithBase64;

  const data = fileObjWithBase64.base64;
  const fileName = fileObjWithBase64.fileName;
  const mimeType = fileObjWithBase64.mimeType;
  var byteCharacters = atob(data);
  var byteNumbers = new Array(byteCharacters.length);
  for (var i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
  }
  var byteArray = new Uint8Array(byteNumbers);
  var blob = new Blob([byteArray], {
    type: mimeType,
  });
  return { ...fileObjWithBase64, blob: blob, }
}

export function htmlToBase64(htmlContent) {
  // const utf8 = unescape(encodeURIComponent(htmlContent));
  // const base64 = btoa(utf8);
  const base64 = btoa(htmlContent.replace(/[\u00A0-\u2666]/g, function (c) {
    return '&#' + c.charCodeAt(0) + ';';
  }));
  return base64;
  // const blob = new Blob(htmlContent,
  //   // Mime type is important for data url
  //   {type : 'text/html'}
  //   ); 
  return new Promise(function (resolve, reject) {
    resolve(base64);
    // const a = new FileReader();
    // a.onload = function(e) {
    //   resolve(e.target.result);
    // };
    // a.readAsDataURL(blob);
  });
}

export function previewContent({ dataObj, winObj, containerUrl, download, isImage, features, replace, dataPromise, previewSig } = {}) {
  const isIE = window.navigator && window.navigator.msSaveOrOpenBlob;
  const containerPage = containerUrl || process.env.PUBLIC_URL + '/helper/showAttachment.html' + (previewSig ? '?fileSig=' + previewSig : '');
  const win = !download && ((typeof dataPromise === "function" || typeof dataObj === "object") && (!isIE || isImage || (/image/i).test((dataObj || {}).mimeType)) && (winObj || window.open(containerPage, '_blank', features || "", replace)));
  const dataAvailable = dataObj && dataObj.base64;

  const deliverContent = function (dataObj) {
    if (previewSig) {
      sessionStorage.setItem("PreviewAttachment", JSON.stringify({ ...dataObj, fileSig: previewSig }));
      const x = JSON.parse(sessionStorage["PreviewAttachment"]);
    }

    if ((!isIE || (/image/i).test((dataObj || {}).mimeType)) && !download) {
      win.postMessage({ ...dataObj, fileSig: previewSig, download }, "*");
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
    else if (typeof dataPromise === "function" || typeof dataPromise === "object") {
      if (typeof dataPromise.then === "function") {
        dataPromise.then(dataJSON => {
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

export function uuid() {
  return Array.from((window.crypto || window.msCrypto).getRandomValues(new Uint32Array(4))).map(n => n.toString(16)).join('-');
}

/* redux autocomplete helper, already implemented in base reducer but this is here for those not comform to the coding style */
export function AutoCompleteSearch({ dispatch, v, topN, filterOn, searchApi, SucceededActionType, FailedActionType, ColumnName, forDtl, forMst }) {
  const keyLookup = (v || '').startsWith("**")
    ? searchApi(v, topN, filterOn)
    : new Promise((resolve) => resolve({ data: { data: [] } }));
  const promises = [
    keyLookup,
    searchApi((v || '').startsWith("**") ? null : v, topN, filterOn),
  ];
  return Promise.all(promises)
    .then(([keyLookup, ret]) => {
      const result = ToReactDropdownList((ret.data || {}).data || []);
      dispatch({
        type: SucceededActionType,
        payload: {
          ColumnName: ColumnName,
          forDtl: forDtl,
          forMst: forMst,
          data: mergeArray(keyLookup.data.data, result, (o) => o.key),
          backfill: (v || '').startsWith("**"),
        }
      })
      return Promise.all([result]);
    }
      , (err) => {
        log.trace(err);
        return Promise.reject(err);
      })
    .catch(err => {
      log.trace(err);
      return Promise.reject(err);
    })
}

export function removeEmptyFileObject(obj, fileObjectColumns) {
  return fileObjectColumns.reduce((o, c) => { o[c] = Array.isArray(o[c]) ? o[c].filter(o => !o.isEmptyFileObject) : o[c]; return o; }, { ...obj })
}


export function ifEmpty(value, defaultValue) {
  if ((isEmpty(value) || isEmptyObject(value)) && arguments.length > 1) return defaultValue;
  else return value;
}

export function getListDisplayContent(obj, column){
  const columnDef = column["ColumnName"] + column["TableId"];
  if(columnDef.length >0){
      if(column["DisplayMode"] === "AutoComplete" || column["DisplayMode"] === "DropDownList"){
          return obj[columnDef+"Text"];
      }
      else if (column["DisplayMode"].includes('Date')){
          return toLocalDateFormat(obj[columnDef]);
      }else{
        return formatContent(obj[columnDef], column["DisplayMode"]);
      }
  }
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

export function isHttps() {
  if (window.location.protocol === 'https:') {
    return true;
  }
}

export function GetCardType(number) {
  log.debug(number);
  // visa
  var re = new RegExp("^4");
  if (number.match(re) != null)
    return "Visa";

  // Mastercard 
  // Updated for Mastercard 2017 BINs expansion
  if (/^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$/.test(number))
    return "Mastercard";

  // AMEX
  re = new RegExp("^3[47]");
  if (number.match(re) != null)
    return "AMEX";

  // Discover
  re = new RegExp("^(6011|622(12[6-9]|1[3-9][0-9]|[2-8][0-9]{2}|9[0-1][0-9]|92[0-5]|64[4-9])|65)");
  if (number.match(re) != null)
    return "Discover";

  // Diners
  re = new RegExp("^36");
  if (number.match(re) != null)
    return "Diners";

  // Diners - Carte Blanche
  re = new RegExp("^30[0-5]");
  if (number.match(re) != null)
    return "Diners - Carte Blanche";

  // JCB
  re = new RegExp("^35(2[89]|[3-8][0-9])");
  if (number.match(re) != null)
    return "JCB";

  // Visa Electron
  re = new RegExp("^(4026|417500|4508|4844|491(3|7))");
  if (number.match(re) != null)
    return "Visa Electron";

  return "";
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
  const apiBasename = origin + appBase;
  const appDomainUrl = origin + appBase;
  rintagi.apiBasename = rintagi.apiBasename || apiBasename;
  rintagi.appDomainUrl = rintagi.appDomainUrl || appDomainUrl;
  rintagi.appNS = rintagi.appNS || appDomainUrl.replace(origin,'') || '/';
  if (location.pathname === "/" && location.protocol === "http:" && location.port >= 3000 && location.port <= 3100) {
      rintagi.apiBasename = (rintagi.localDev || {}).apiBasename || rintagi.apiBasename;
  }
  document.Rintagi = rintagi;
};

setupRuntime();