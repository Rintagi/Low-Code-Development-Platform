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