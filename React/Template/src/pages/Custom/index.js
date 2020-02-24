/* hand coded custom pages stub, must present even if there is none for your system */
import { naviPath } from '../../helpers/utils'

export const CustomRoutePre = [
]

export const CustomRoutePost = [
]

function naviLabel(label, type) {
    if (type === 'MstList') return ((label || {}).MasterLst || {}).label || '';
    else if (type === 'MstRecord') return ((label || {}).MasterRec || {}).label || '';
    else if (type === 'DtlList') return ((label || {}).DetailLst || {}).label || '';
    else if (type === 'DtlRecord') return ((label || {}).DetailRec || {}).label || '';
    else return type;
  }
  
export function getNaviBar(type, mst, dtl, label) {
//    return pagesRoutes
      return []
      .sort((a, b) => a.order - b.order)
      .map((v, i) => {
        return {
          path: naviPath(v.type === 'MstList' ? '_' : mst.subTestId2, '', v.path),
          label: naviLabel(label, v.type),
          type: v.type,
          active: v.type === type,
        }
      });
  }
  