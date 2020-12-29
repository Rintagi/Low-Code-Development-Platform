
import { sidebarReducer } from './SideBar';
import { authReducer } from './Auth';
import { rintagiReducer } from './_Rintagi';
import { notificationReducer } from './Notification';
import { globalReducer } from './Global';
import { SqlReportReducer } from './SqlReport';
import { CustomReducer } from './Custom';

/* below are dynamic, put shared static one above this */

let redux = {
    auth: authReducer,
    rintagi: rintagiReducer,
    global: globalReducer,
    sidebar: sidebarReducer,
    notification: notificationReducer,
    SqlReport: SqlReportReducer,
    ...(CustomReducer || {}),
    /* dynamic go to here */
}
export default redux;