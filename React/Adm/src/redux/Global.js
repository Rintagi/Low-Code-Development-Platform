import log from '../helpers/logger';

export const SET_PAGE_TITLE = 'SET_PAGE_TITLE';
export const SHOW_SPINNER = 'SHOW_SPINNER';

let spinnerCount = 0;

// Reducer
export function globalReducer(state = {}, action) {
    switch (action.type) {
        case SET_PAGE_TITLE:
            return { ...state, pageTitle: action.payload.title };
        case SHOW_SPINNER:
            return { ...state, pageSpinner: action.payload.showSpinner };
        default:
            return state;
    }
}

// Actions
export function setTitle(title) {
    return {
        type: SET_PAGE_TITLE,
        payload: { title: title }
    };
}

export function setSpinner(showSpinner, source) {
  if (!source) {
    // debugger;
  }
  if (showSpinner) spinnerCount++;
  else if (spinnerCount > 0) spinnerCount--;
  log.debug('#####spinner log', spinnerCount, showSpinner, source);
  return {
    type: SHOW_SPINNER,
    payload: { showSpinner: spinnerCount === 0 || showSpinner ? showSpinner : true }
  };
}