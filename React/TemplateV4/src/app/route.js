
import {pagesRoutes as AccountRoute} from '../pages/Account/index'
import {pagesRoutes as SqlReportRoute} from '../pages/SqlReport/index'
import {pagesRoutes as DefaultRoute} from '../pages/Default/index'
import {CustomRoutePre, CustomRoutePost, SuppressGenRoute} from '../pages/CustomRoute'
/* all these are dynamic, add the required route for each page */
let route = [
...(CustomRoutePre || []),
...(
SuppressGenRoute ? [] : [
...AccountRoute,
...DefaultRoute,
// ...SqlReportRoute,
]
),
...(
SuppressGenRoute ? [] : [
]),
...(CustomRoutePost || []),
];

document.Rintagi.systemId = '3';
export default route;