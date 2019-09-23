
                import {pagesRoutes as AccountRoute} from '../pages/Account/index'
                import {pagesRoutes as SqlReportRoute} from '../pages/SqlReport/index'
                import {pagesRoutes as DefaultRoute} from '../pages/Default/index'
                /* all these are dynamic, add the required route for each page */
           
                export default [
                ...AccountRoute,
                ...DefaultRoute,
                // ...SqlReportRoute,
                ];

             document.Rintagi.systemId = '3';