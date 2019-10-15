import {toLocalDateFormat} from './formatter';

export function getSelectedFromList(key,keyList,keyColumeName, defaultObj) {
    const x = keyList.filter((v)=>v[keyColumeName] === key);
    return x;
    return (keyList.length > 0 ? keyList : [defaultObj || {}]).reduce(
        (r,v,i,a)=>{if (v[keyColumeName] === key) r.push(v); return r},[])
}

export function objectListToDropdownList(objectList, fn)
{
    return objectList.map((v,i)=>fn(v,i))
}

export function objectListToDict(objectList, keyColumnName, fn)
{
    return objectList.length === 0 ? {} : objectList.reduce((r,v,i,a)=>{
        r[v[keyColumnName]] = (typeof fn === "function") ? fn(v) : v; 
        return r
    },{});
}

export function mergeArray(l1, l2, keyFn) {
    if(l1.length === 0) return l2;
    const o = l1.reduce((a,v)=>{ a[keyFn(v)] = v; return a; },{});
    return [
      ...l1,
      ...l2.filter(v=>!o[keyFn(v)] && keyFn(v))
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
export function isEmptyId(val)
{
    return val === undefined || val === "" || val === null || val === 0;
}

export function isTouchDevice() {
    if('ontouchstart' in window || navigator.msMaxTouchPoints) {
      return true;
    }
  }


export function getListDisplayContet(obj, column){
    const columnDef = column["ColumnName"] + column["TableId"];
    if(columnDef.length >0){
        if(column["DisplayMode"] === "AutoComplete" || column["DisplayMode"] === "DropDownList"){
            return obj[columnDef+"Text"];
        }
        else if (column["DisplayMode"].includes('Date')){
            return toLocalDateFormat(obj[columnDef]);
        }else{
            return obj[columnDef];
        }
    }
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