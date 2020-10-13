import * as loglevel from 'loglevel';

if (process.env.NODE_ENV === `development`) {
    loglevel.setLevel('debug');
}
else {
    loglevel.setLevel('info', false);
}

export default loglevel;