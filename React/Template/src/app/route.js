
import {pagesRoutes as AccountRoute} from '../pages/Account/index'
import {pagesRoutes as SqlReportRoute} from '../pages/SqlReport/index'
import {pagesRoutes as DefaultRoute} from '../pages/Default/index'
import {CustomRoutePre, CustomRoutePost} from '../pages/CustomRoute'
/* all these are dynamic, add the required route for each page */

export default [
...(CustomRoutePre || []),   
...AccountRoute,
...DefaultRoute,
// ...SqlReportRoute,
/* dynamic generated goes below here */
...(CustomRoutePre || []),   
];

document.Rintagi.systemId = '3';