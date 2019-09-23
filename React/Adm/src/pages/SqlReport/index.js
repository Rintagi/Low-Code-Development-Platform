/* this is external interface(for import) for this screen */
import {naviPath} from '../../helpers/utils'
import Print from './Print';
/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */
export const pagesRoutes = [
  {
    path: "/Print/",
    name: "Print",
    short: "Print",
    component: Print,
    icon: "print",
    isPublic: false,
    type: 'MstList',
    order: 1,
    inMenu: true,
    menuLabel: "Print Expenses"
  }
] 