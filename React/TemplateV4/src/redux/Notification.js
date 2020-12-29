import log from '../helpers/logger';

export const SUCCESS = 'NOTIFICATION_SUCCESS';
export const ERROR = 'NOTIFICATION_ERROR';
export const CLEAR_ERROR = 'NOTIFICATION_CLEAR_ERROR';

export async function dispatchWithNotification(dispatch, action)
{
    dispatch(action);
    // const actionType = action.type;
    const message = ((action || {}).payload || {}).message || ((action || {}).payload || {}).errMsg || (((action || {}).payload || {}).error || {}).errMsg;
    const validationErrors = ((action || {}).payload || {}).validationErrors;
    if (message) {
        if (action.type.endsWith(".FAILED")) {
            dispatch(showNotification("E",{message, validationErrors}))
        }
        else if (action.type.endsWith(".SUCCEEDED")) {
            dispatch(showNotification("S",{message}))
        }
    }
}
// Reducer
export function notificationReducer(state = null, action) {
    switch (action.type) {
      case SUCCESS:
        return action.payload;
      case ERROR:
        return  action.payload;
      case CLEAR_ERROR:
        return null;
      default:
        return state;
    }
  }

// Actions
export function showNotification(msgType, {message,timeout,dtl,validationErrors}) {
    if (msgType === "E") {
        return {
            type: ERROR,
            payload: {msgType: msgType, message: message, timeout,validationErrors}
        };
    }
    else {
        return {
            type: SUCCESS,
            payload: {msgType: msgType, message: message, timeout}
        };        
    }
}

export function clearNotification() {
    return (dispatch) => {
        dispatch({ type: CLEAR_ERROR, payload: null });
    }
}
