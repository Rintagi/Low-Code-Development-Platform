import {combineReducers, createStore, applyMiddleware} from 'redux';
import ReduxPromise from "redux-promise";
import thunk from 'redux-thunk';
import logger from 'redux-logger';
import reducers from '../redux/index'


const reducer = combineReducers(reducers);

const middlewares = [];

middlewares.push(thunk.withExtraArgument({
  apiBase:'',
  webApi:{}, // say replace the whole webapi with something else like a stub(basically dependency injection like for testing purpose)
}));
middlewares.push(ReduxPromise);

if (process.env.NODE_ENV === `development` && true) {
  middlewares.push(logger);
}

const createStoreWithMiddleware = applyMiddleware(...middlewares)(createStore);

const store = (window.devToolsExtension
  ? window.devToolsExtension()(createStoreWithMiddleware)
  : createStoreWithMiddleware)(reducer);

export default store;
