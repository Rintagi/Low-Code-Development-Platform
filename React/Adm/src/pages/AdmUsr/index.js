
/* this is external interface(for import) for this screen */
import { naviPath } from '../../helpers/utils'
import MstList from './MstList';
import MstRecord from './MstRecord';
   
/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */
export const pagesRoutes = [
  {
    path: '/AdmUsr/Mst/:mstId?',
    name: 'Master Record',
    short: 'MasterRecord',
    component: MstRecord,
    icon: 'briefcase',
    isPublic: false,
    type: 'MstRecord',
    order: 2,
    screenId : 1
  },
   
  {
    path: '/AdmUsr/:mstId?',
    name: 'Master List',
    short: 'MasterList',
    component: MstList,
    icon: 'usd',
    isPublic: false,
    type: 'MstList',
    order: 1,
    inMenu: true,
    menuLabel: 'AdmUsr',
    screenId : 1
  },
]

function naviLabel(label, type) {
  if (type === 'MstList') return ((label || {}).MasterLst || {}).label || '';
  else if (type === 'MstRecord') return ((label || {}).MasterRec || {}).label || '';
  else if (type === 'DtlList') return ((label || {}).DetailLst || {}).label || '';
  else if (type === 'DtlRecord') return ((label || {}).DetailRec || {}).label || '';
  else return type;
}

export function getNaviBar(type, mst, dtl, label) {
  return pagesRoutes
    .sort((a, b) => a.order - b.order)
    .map((v, i) => {
      return {
        path: naviPath(v.type === 'MstList' ? '_' : mst.UsrId1, '', v.path),
        label: naviLabel(label, v.type),
        type: v.type,
        active: v.type === type,
      }
    });
}
            