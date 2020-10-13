export const CHANGE_SIDEBAR_VISIBILITY = 'CHANGE_SIDEBAR_VISIBILITY';
export const CHANGE_MOBILE_SIDEBAR_VISIBILITY = 'CHANGE_MOBILE_SIDEBAR_VISIBILITY';
export const HIDE_MOBILE_SIDEBAR_VISIBILITY = 'HIDE_MOBILE_SIDEBAR_VISIBILITY';
export const HIDE_SIDEBAR = 'HIDE_SIDEBAR';

//reducer
const initialState = {
  show: false,
  collapse: false,
  hide: false
};

export function sidebarReducer(state = initialState, action) {
  switch (action.type) {
    case CHANGE_SIDEBAR_VISIBILITY:
      return {...state, collapse: !state.collapse};
    case CHANGE_MOBILE_SIDEBAR_VISIBILITY:
      return {...state, show: !state.show};
    case HIDE_MOBILE_SIDEBAR_VISIBILITY:
      return {...state, show: false};
    case HIDE_SIDEBAR:
      return {...state, hide: action.payload.status};
    default:
      return state;
  }
}

//action
export function changeSidebarVisibility() {
  return {
    type: CHANGE_SIDEBAR_VISIBILITY
  };
}

export function changeMobileSidebarVisibility() {
  return {
    type: CHANGE_MOBILE_SIDEBAR_VISIBILITY
  };
}

export function hideMobileSidebarVisibility() {
  return {
    type: HIDE_MOBILE_SIDEBAR_VISIBILITY
  };
}


export function hideSidebar(status) {
  return {
    type: HIDE_SIDEBAR,
    payload: {status: status}
  };
}
