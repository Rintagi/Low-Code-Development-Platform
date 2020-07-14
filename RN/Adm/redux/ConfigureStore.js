import {createStore, applyMiddleware} from "redux";
import thunk from "redux-thunk";
import logger from 'redux-logger';
import ReduxPromise from "redux-promise";
import {persistCombineReducers, persistStore} from "redux-persist";
import { AsyncStorage } from 'react-native';
import reducers from './index';

const middlewares = [];

middlewares.push(thunk.withExtraArgument({
  apiBase:'',
  webApi:{}, // say replace the whole webapi with something else like a stub(basically dependency injection like for testing purpose)
}));
middlewares.push(ReduxPromise);

if (process.env.NODE_ENV === `development` && true) {
  middlewares.push(logger);
}

export const ConfigureStore = () => {
    const config ={
        key: 'root',
        storage: AsyncStorage,
        debug: true
    };

    const store = createStore(
        persistCombineReducers(config, {
                ...reducers,
        }),
        applyMiddleware(...middlewares)
    );

    const persistor = persistStore(store);

    return {persistor, store};
};