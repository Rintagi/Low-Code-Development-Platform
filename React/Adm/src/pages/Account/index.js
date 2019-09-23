/* this is external interface(for import) for this screen */
import Profile from './Profile';
import NewPassword from './NewPassword';
import Setting from './Setting';
import {naviPath} from '../../helpers/utils'
/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */
export const pagesRoutes = [
  {
    path: "/Profile",
    name: "Profile",
    short: "Profile",
    component: Profile,
    icon: "",
    isPublic: false,
    type: "Profile",
    order: 1
  },
  {
    path: "/Setting",
    name: "Setting",
    short: "Setting",
    component: Setting,
    icon: "",
    isPublic: false,
    type: 'Setting',
    order: 2
  },
  {
    path: "/NewPassword",
    name: "NewPassword",
    short: "NewPassword",
    component: NewPassword,
    icon: "",
    isPublic: true,
    type: 'NewPassword',
    order: 3
  },
] 

function naviLabel(label,type)
{ 
    if (type==="Profile") return label.Profile;
    else if (type==="Setting") return label.Settings;
    else if (type==="NewPassword") return label.NewPassword;
    else return type;
}

 export function getNaviBar(type,label)
{
    return pagesRoutes
        .sort((a,b) => a.order - b.order)
        .map((v,i)=>{
        return {
          path: v.path,
          label: naviLabel(label,v.type),
          type:v.type,
          active: v.type===type,
        }
    });
 }