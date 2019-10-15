
                import { sidebarReducer } from './SideBar';
                import { authReducer } from './Auth';
                import { notificationReducer } from './Notification';
                import { globalReducer } from './Global';
                import { SqlReportReducer } from './SqlReport';

                /* below are dynamic, put shared static one above this */

              export default {
              auth: authReducer,
              global: globalReducer,
              sidebar: sidebarReducer,
              notification: notificationReducer,
              SqlReport: SqlReportReducer,

              /* dynamic go to here */

              }
            