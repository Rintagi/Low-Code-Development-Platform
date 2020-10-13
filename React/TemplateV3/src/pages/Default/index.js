/* this is external interface(for import) for this screen */
import Default from './Default';
/* react router match by order of appearance in list so make sure wider match comes last, use order to control display order */
export const pagesRoutes = [
  {
    path: "/Default",
    name: "Default",
    short: "default",
    component: Default,
    icon: "",
    isPublic: false,
    type: "Default",
    order: 1
  }
] 

function naviLabel(label,type)
{ 
    if (type==="Default") return label.Default;
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