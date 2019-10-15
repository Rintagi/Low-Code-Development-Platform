import log from '../helpers/logger';

export const SET_PAGE_TITLE = 'SET_PAGE_TITLE';
export const SHOW_SPINNER = 'SHOW_SPINNER';

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

export function setSpinner(showSpinner) {
    return {
        type: SHOW_SPINNER,
        payload: { showSpinner: showSpinner }
    };
}