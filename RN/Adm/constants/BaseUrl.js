import log from '../helpers/logger';
import Constants from 'expo-constants';

const isDevelopment = process.env.NODE_ENV === "development";
const expoManifest = (Constants || {}).manifest || {};

const serverConfig = (expoManifest.extra || {}).serverConfig || {};

/* Constants.manifest(in app.json) is only load once per startup so in development mode
 * it is easier to change the following on the fly than changing app.json then restart the commandline(recompile everything)
 */

/* pick one from below only for development use, deployed app will always use app.json setup */ 
/* for source from external access */
//const webServiceBaseUrl = 'https://www.rintagi.com';
/* same server development */
const webServiceBaseUrl = 'http://172.16.0.17/ro';
/* stub nodejs server */
//const webServiceBaseUrl = 'http://172.16.1.23:3001';

console.log(expoManifest);

export const baseUrl = (isDevelopment ? webServiceBaseUrl : serverConfig.apiBasename) || 'http://172.16.1.23:3001';
