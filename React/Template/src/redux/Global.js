import log from '../helpers/logger';
import { RememberCurrent, GetCurrent } from './Persist';

const SCREEN_PREFIX = 'IdCheck';
const PERSIST_KEY = SCREEN_PREFIX + "_LASTIDCHECK"
export const SET_PAGE_TITLE = 'SET_PAGE_TITLE';
export const SHOW_SPINNER = 'SHOW_SPINNER';
export const SWITCH_ROLE = 'SWITCH_ROW';
export const SET_SAVE_AND_EXIT = 'SET_SAVE_AND_EXIT';
export const TOPBAR_LEFT = 'TOPBAR_LEFT';
export const TOPBAR_CENTER = 'TOPBAR_CENTER';
export const TOPBAR_RIGHT = 'TOPBAR_RIGHT';
export const SHOW_SKELETON = 'SHOW_SKELETON';
export const HIDE_SIDEBAR = 'HIDE_SIDEBAR';

/* demo role based navigation */
export const roles = {
  "1": { role: "borrower", landingPage: "/borrower" },
  "2": { role: "guarantor", landingPage: "/guarantor" },
  "3": { role: "lender", landingPage: "/lender" },
}

let spinnerCount = 0;
let skeletonCount = 0;

// Reducer
export function globalReducer(state = { role: roles["1"] }, action) {
  switch (action.type) {
    case SET_SAVE_AND_EXIT:
      return { ...state, SaveAndExit: { ...action.payload } };
    case TOPBAR_LEFT:
      return { ...state, TopbarLeft: { ...action.payload } };
    case TOPBAR_CENTER:
      return { ...state, TopbarCenter: { ...action.payload } };
    case TOPBAR_RIGHT:
      return { ...state, TopbarRight: { ...action.payload } };
    case SET_PAGE_TITLE:
      return { ...state, pageTitle: action.payload.title };
    case SWITCH_ROLE:
      return { ...state, role: roles[action.payload.role] };
    case SHOW_SPINNER:
      return { ...state, pageSpinner: action.payload.showSpinner };
    case SHOW_SKELETON:
      return { ...state, pageSkeleton: action.payload.showSkeleton };
    case HIDE_SIDEBAR:
      return { ...state, hideSidebar: action.payload.hideSidebar };
    default:
      return state;
  }
}

export function registerSaveAndExit(saveFn, pathname, visibility, label) {
  return {
    type: SET_SAVE_AND_EXIT,
    payload: { callback: saveFn, pathname: pathname, visibility: visibility, label: label }
  };
}

export function registerTopbarLeft(saveFn, pathname, visibility, label, logo) {
  return {
    type: TOPBAR_LEFT,
    payload: { callback: saveFn, pathname: pathname, visibility: visibility, label: label, logo: logo }
  };
}

export function registerTopbarCenter(pathname, visibility, label, left, sublabel) {
  return {
    type: TOPBAR_CENTER,
    payload: { pathname: pathname, visibility: visibility, label: label, left: left, sublabel: sublabel }
  };
}

export function registerTopbarRight(saveFn, pathname, visibility, icon, text, button, countNumber, buttonIcon) {
  return {
    type: TOPBAR_RIGHT,
    payload: { callback: saveFn, pathname: pathname, visibility: visibility, icon: icon, text: text, button: button, countNumber: countNumber, buttonIcon: buttonIcon, }
  };
}

// Actions
export function setTitle(title) {
  return {
    type: SET_PAGE_TITLE,
    payload: { title: title }
  };
}

export function hideSidebar(hideSidebar) {
  return {
    type: HIDE_SIDEBAR,
    payload: { hideSidebar: hideSidebar }
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

export function setSkeleton(showSkeleton, source) {
  if (!source) {
    // debugger;
  }
  if (showSkeleton) skeletonCount++;
  else if (skeletonCount > 0) skeletonCount--;
  log.debug('#####skeleton log', skeletonCount, showSkeleton, source);
  return {
    type: SHOW_SKELETON,
    payload: { showSkeleton: skeletonCount === 0 || showSkeleton ? showSkeleton : true }
  };
}

export function switchRole(role) {
  return {
    type: SWITCH_ROLE,
    payload: { role: role }
  };
}