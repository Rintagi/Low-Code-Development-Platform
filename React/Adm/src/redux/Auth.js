import { getAsyncTypes } from '../helpers/actionType';
import { authService } from '../services/authService'
import * as profileService from '../services/profileService'
import * as systemService from '../services/systemService'
import { showNotification, dispatchWithNotification } from '../redux/Notification'
import { switchLanguage, getCurrentLanguage } from '../helpers/formatter'
import log from '../helpers/logger';
import {setupRuntime} from '../helpers/utils';

// action type
const SCREEN_PREFIX = 'Login';
export const LOGIN = getAsyncTypes(SCREEN_PREFIX, 'AUTH_LOGIN');
export const LOGOUT = getAsyncTypes(SCREEN_PREFIX, 'AUTH_LOGOUT');
export const GET_TOKEN = getAsyncTypes(SCREEN_PREFIX, 'AUTH_GET_TOKEN');
export const GET_USER = getAsyncTypes(SCREEN_PREFIX, 'AUTH_GET_USER');
export const GET_MENU = getAsyncTypes(SCREEN_PREFIX,'AUTH_GET_MENU');
export const SESSION_TIMEOUT = getAsyncTypes(SCREEN_PREFIX, 'AUTH_LOGIN');
export const UPD_PROFILE = getAsyncTypes(SCREEN_PREFIX, 'AUTH_UPDATE_PROFILE');
export const CHANGE_PASSWORD = getAsyncTypes(SCREEN_PREFIX, 'AUTH_CHANGE_PASSWORD');
export const RESET_PASSWORD_EMAIL = getAsyncTypes(SCREEN_PREFIX, 'AUTH_RESET_PASSWORD_EMAIL');
export const SWITCH_CURRENT = getAsyncTypes(SCREEN_PREFIX, 'AUTH_SWITCH_CURRENT');

const runtimeConfig = (document.Rintagi || {});
const systemId = runtimeConfig.systemId || 3;
// reducer
const initState = {
  user: {
    desktopView: false,
  },
  menu: null,
  Label: {
    Login: "Login",
    Logout: "Logout",
    UserName: "User Name",
    Password: "Password",
    UserNameEmpty: "User Name cannot be empty",
    PasswordEmpty: "Password cannot be empty",
    LoginScreenTitle: "Login to Your Account",
    LoginScreenDesc: "Please enter your login details",
    Profile: "Profile",
    ProfileSubtitle: "Update your profile information",
    NewPassword: "New Password",
    NewPasswordSubtitle: "Change your password",
    NewLoginName: "Login Name",
    NewUserName: "Shown as",
    NewUserEmail: "Email",
    NewuserPassword: "New Password",
    ConfirmPwd: "Confirm Password",
    UpdProfileBtn: "Update",
    pdPwdBtn: "Change Password",
    NewUsrPasswordEmpty: "Please enter a new password",
    ConfirmPwdEmpty: "Please enter a matching password",
    PwdHlpMsgLabel: "Your password should contain minimum 8 characters with at least one digit, one capital, one lower case and one symbol.",
    ApplySettingsBtn: "Apply Settings",
    Settings: "Account Settings",
    SettingsSubtitle: "Select your settings",
    CompanyList: "Company",
    ProjectList: "Project",
    SystemsList: "System",
    TimeZoneList: "TimeZone",
    ResetLoginName: "User Name",
    ResetUsrEmail: "Email",
    ResetPwdBtn: "Reset",
    Or: "OR",
    ResetPwdTitle: "Reset Password",
    ResetPwdSubtitle: "Enter your username or email to reset password",
    Information: "Information",
    UnSavedPage: "You have not saved your changes. Are you sure you want to leave?",
    Language: "Language"
  },
  filter: {
    CompanyList: [
      { key: "1", Label: "company 1", value: "company 1" },
      { key: "2", Label: "company 2", value: "company 2" },
    ],
    ProjectList: [
      { key: "1", Label: "Project 1", value: "Project 1" },
      { key: "2", Label: "Project 2", value: "Project 2" },
    ],
    SystemsList: [
      { key: "1", Label: "System 1", value: "System 1" },
      { key: "2", Label: "System 2", value: "System 2" },
    ],
    TimeZoneList: [
      { key: "1", Label: "TimeZone 1", value: "TimeZone 1" },
      { key: "2", Label: "TimeZone 2", value: "TimeZone 2" },
    ]
  },
  nounce: null,
  ticketLeft: null,
  ticketRight: null,
}
export function authReducer(state = initState, action) {
  const payload = action.payload;

  switch (action.type) {
    case GET_USER.STARTED:
      return {
        //          ...(state), // flush stored info, as if it is not authenticated
        ...state,
        user: {
          ...state.user,
          loading: true,
          loadingTime: new Date()
        }
      }
    case GET_USER.SUCCEEDED:
      return {
        ...(state),
        user: {
          ...state.user,
          ...(action.payload),
          loading: false,
          key: Date.now()
        },
      }
    case GET_USER.FAILED:
      return {
        ...initState,
        user: {
          desktopView: state.user.desktopView,
          ...(action.payload),
          loading: false,
        }
      }
    case GET_USER.ENDED:
        return {
          ...initState,
          user: {
            ...(state.user || {}),
            loading: false,
          }
        }  
    case LOGIN.STARTED:
      return {
        ...state,
        user: {
          ...state.user,
          ...(action.payload),
          loading: true
        },
      }
    case LOGIN.SUCCEEDED:
      return {
        ...state,
        user: {
          desktopView: state.user.desktopView,
          ...(action.payload),
          loading: false
        },
      }
    case LOGIN.FAILED:
      return {
        ...state,
        user: {
          desktopView: state.user.desktopView,
          ...(action.payload),
          loading: false
        },
      }
    case LOGIN.ENDED:
      return {
        ...state,
        user: {
          ...(state.user || {}),
          loading: false
        },
      }
    case LOGOUT.STARTED:
      return {
        ...state,
        user: {
          ...state.user,
          loading: true
        },
      }
    case LOGOUT.SUCCEEDED:
    case LOGOUT.FAILED:
        return {
          ...state,
          user: {
            desktopView: state.user.desktopView,
            loading: false
          },
        }
    case LOGOUT.ENDED:
      return {
        ...state,
        user: {
          ...(state.user || {}),
          loading: false
        },
      }
    case UPD_PROFILE.STARTED:
      return {
        ...state,
        page_saving: true
      }
    case UPD_PROFILE.SUCCEEDED:
      return {
        ...state,
        page_saving: false,
        user: {
          ...(action.payload)
          , key: Date.now()
        },
      }
    case UPD_PROFILE.FAILED:
      return {
        ...state,
        page_saving: false
      }
    case CHANGE_PASSWORD.STARTED:
      return {
        ...state,
        page_saving: true,
        loading: true,
      }
    case CHANGE_PASSWORD.SUCCEEDED:
      return {
        ...state,
        page_saving: false,
        loading: false,
      }
    case CHANGE_PASSWORD.FAILED:
      return {
        ...state,
        page_saving: false,
        loading: false,
      }
    case RESET_PASSWORD_EMAIL.STARTED:
      return {
        ...state,
        page_saving: true,
        loading: true,
      }
    case RESET_PASSWORD_EMAIL.SUCCEEDED:
      return {
        // ...state,
        page_saving: false,
        email: payload.email,
        nounce: payload.nounce,
        ticketRight: payload.ticketRight,
        loading: false,
      }
    case RESET_PASSWORD_EMAIL.FAILED:
      return {
        ...state,
        page_saving: false,
        loading: false,
      }
    case SWITCH_CURRENT.STARTED:
      return {
        ...state,
        page_saving: true
      }
    case SWITCH_CURRENT.SUCCEEDED:
      return {
        ...state,
        user: {
          ...state.user,
          CompanyId: payload.CompanyId,
          ProjectId: payload.ProjectId,
          CultureId: payload.CultureId,
          lang: payload.lang,
          key: Date.now(),
        },
        page_saving: false
      }
    case SWITCH_CURRENT.FAILED:
      return {
        ...state,
        page_saving: false
      }
    case GET_MENU.STARTED:
        return {
            //          ...(state), // flush stored info, as if it is not authenticated
            ...state,
            menu: {
                menuList: state.menu.menuList,
                loading: true,
                loadingTime: new Date()
            }
        }
    case GET_MENU.SUCCEEDED:
        return {
            ...(state),
            menu: {
                ...state.menu,
                menuList:[...action.payload],
                loading: false,
                key: Date.now()
            },
        }
    case GET_MENU.FAILED:
        return {
            ...(state),
            menu: {
                menuList:[],
                // desktopView:state.menu.desktopView,
                // ...(action.payload),
                loading: false,
            }
        }      
    default:
      return state;
  }
}

function switchToSavedCulture(user) {
  const currentLang = getCurrentLanguage();
  if (currentLang.CultureId !== user.CultureId) {
    systemService.getCultureList(user.CultureId, "en")
      .then(ret => {
        //switchLanguage('en', data.data.CultureId);
        const mappedCulture = ret.data.data.filter(v => +v.CultureTypeId === +user.CultureId)[0];
        if (mappedCulture) {
          switchLanguage(mappedCulture.CultureTypeName, user.CultureId);
        }
      },
        err => {
          console.log(err);
        }
      )
  }
}
// action function maker
export function ShowSpinner(state) {
  return !state || state.page_loading || state.page_saving;
}

export function login(username, password) {

  return (dispatch, getState, { webApi }) => {
    dispatchWithNotification(dispatch, { type: LOGIN.STARTED, payload: { username: username } });
    return authService.login(username, password).then(
      (data) => {
        if (data.status === "success") {
          //dispatchWithNotification(dispatch, { type: LOGIN.SUCCEEDED, payload: data.accessCode});
          //dispatchWithNotification(dispatch, { type: GET_TOKEN.STARTED, payload: data.accessCode});
          return authService.getToken(data.accessCode).then(
            data => {
              //dispatchWithNotification(dispatch, { type: GET_TOKEN.SUCCEEDED, payload: data.data});
              //dispatchWithNotification(dispatch, { type: GET_USER.STARTED, payload: data.data});
              return authService.getUsr().then(
                data => {
                  dispatchWithNotification(dispatch, { type: GET_USER.SUCCEEDED, payload: data.data });
                  authService.getMenu(systemId).then(
                    data => {
                        dispatchWithNotification(dispatch, { type: GET_MENU.SUCCEEDED, payload: data.data });                            
                    },
                    error => {
                        dispatchWithNotification(dispatch, { type: GET_MENU.FAILED, payload: error });
                    }
                    ).catch(error => {
                    console.log(error);
                    })
                  return Promise.resolve([data.data]);
                },
                error => {
                  dispatchWithNotification(dispatch, { type: GET_USER.FAILED, payload: error });
                  return Promise.reject(error);
                }
              ).catch(error => {
                console.log(error);
                return Promise.reject(error);
              })
            }, error => {
              dispatchWithNotification(dispatch, { type: GET_TOKEN.FAILED, payload: error });
              return Promise.reject(error);
            }).catch(error => {
              console.log(error);
              return Promise.reject(error);
            })
        }
        else {
          dispatchWithNotification(dispatch, { type: LOGIN.FAILED, payload: data });
          return Promise.reject(data);
          //dispatchWithNotification(dispatch, getNotificationAction("E", data.error || data.errMsg));
        }
      },
      (error) => {
        console.log(error);
        dispatchWithNotification(dispatch, { type: LOGIN.FAILED, payload: {...error, errMsg:error.errMsg === "bot challenge" ? "login failed" : error.errMsg } });
        return Promise.reject(error);
      }
    );
  };
}

export function logout(keepToken,currentSessionOnly) {
  return (dispatch) => {
    dispatchWithNotification(dispatch, { type: LOGOUT.STARTED, payload: {} });

    return authService.logout(keepToken,currentSessionOnly).then(
      (result) => {
        dispatchWithNotification(dispatch, { type: LOGOUT.SUCCEEDED, payload: result });
        authService.getMenu(systemId).then(
            data => {
                dispatchWithNotification(dispatch, { type: GET_MENU.SUCCEEDED, payload: data.data });
                
            },
            error => {
                dispatchWithNotification(dispatch, { type: GET_MENU.FAILED, payload: error });
            }
        ).catch(error => {
            console.log(error);
        })
        return true;
      }
    )
      .catch(error => {
        dispatchWithNotification(dispatch, { type: error.errType === "network error" ? (keepToken ? LOGOUT.SUCCEEDED : LOGOUT.ENDED ) : LOGOUT.FAILED, payload: error });
        return Promise.reject(error);
      })
  }
}

export function reloadCurrentUser() {
  return (dispatch, getState, ...rest ) => {
    return authService.renewAccessToken()
      .then(
        token => {
          return getCurrentUser(true)(dispatch,getState,...rest);
        }
      )
      .then(
        (currentUser) => {
          return currentUser;
        }
      )
      .catch(
        error => {
          return Promise.reject(error);
        }
      )
  }
}
export function getCurrentUser(silent = false) {
  return async (dispatch, getState, { webApi }) => {

    //if (!authService.isAuthenticated()) return;
    const access_token = await authService.getAccessToken();
    const refresh_token = authService.getRefreshToken().refresh_token;
    if (!refresh_token) {
      log.debug('no refresh token');
      return Promise.resolve({});
    }
    dispatchWithNotification(dispatch, { type: GET_USER.STARTED, payload: null });
    return authService.getUsr()
      .then(
        (data) => {
          if (data.data) {
            /* have to do side effect here */
            switchToSavedCulture(data.data);
            dispatchWithNotification(dispatch, { type: GET_USER.SUCCEEDED, payload: data.data });
            const auth = getState().auth || {};
            if (!auth.menu) {
              authService.getMenu().then(
                data => {
                    dispatchWithNotification(dispatch, { type: GET_MENU.SUCCEEDED, payload: data.data });
                    
                },
                error => {
                    dispatchWithNotification(dispatch, { type: GET_MENU.FAILED, payload: error });
                });
            }
            return Promise.resolve(data.data);
          }
          else {
            //dispatchWithNotification(dispatch, { type: GET_USER.FAILED, payload: data });
            return Promise.reject(data.errMsg)
          }
        },
        (error) => {
          if (!silent) {
            // dispatchWithNotification(dispatch, getNotificationAction("E", {
            //     type: 'network',
            //     message: 'network error'
            // }));
          }
          if (authService.isAuthenticated()) {
            //dispatchWithNotification(dispatch, { type: GET_USER.FAILED, payload: error });
          }
          return Promise.reject(error);
        }
      ).catch(
        error => {
          if (authService.isAuthenticated()) {
            //dispatchWithNotification(dispatch, { type: GET_USER.FAILED, payload: error });
          }
          else 
          {
            // silent
            log.debug("flush user redux");
            dispatch({ type: error.errType === "network error" ? GET_USER.ENDED : GET_USER.FAILED, payload: error });
          }
          return Promise.reject(error);
        }
      );
  };
}

export function saveProfile(values) {
  return (dispatch, getState, { webApi }) => {
    dispatchWithNotification(dispatch, { type: UPD_PROFILE.STARTED, payload: { message: "Profile Update started" } });

    const { newLoginName, newUserName, newUserEmail } = values;

    profileService.updateProfile(newLoginName, newUserName, newUserEmail).then(
      (ret => {
        dispatchWithNotification(dispatch, { type: UPD_PROFILE.SUCCEEDED, payload: { message: "Profile Update succeeded" } });
      })
      ,
      (err => {
        dispatchWithNotification(dispatch, { type: UPD_PROFILE.FAILED, payload: { error: err, message: err.errMsg } });
      })
    );
  };
}

export function changePassword(values) {
  return (dispatch, getState, { webApi }) => {
    dispatchWithNotification(dispatch, { type: CHANGE_PASSWORD.STARTED, payload: { message: "Password changed started" } });

    const { j, p, newUsrPassword, confirmPwd } = values;

    profileService.updUsrPwd(j, p, newUsrPassword, confirmPwd).then(
      (ret => {
        dispatchWithNotification(dispatch, { type: CHANGE_PASSWORD.SUCCEEDED, payload: { message: "Password changed succeeded" } });
      })
      ,
      (err => {
        dispatchWithNotification(dispatch, { type: CHANGE_PASSWORD.FAILED, payload: { error: err, message: err.errMsg } });
      })
    );
  };
}

export function resetPasswordEmail(values) {
  return (dispatch, getState, { webApi }) => {
    dispatchWithNotification(dispatch, { type: RESET_PASSWORD_EMAIL.STARTED, payload: { message: "Password reset Email sent Successfully" } });

    const { resetLoginName, resetUsrEmail } = values;

    profileService.resetPwd(resetLoginName, resetUsrEmail).then(
      (ret => {
        dispatchWithNotification(dispatch, { type: RESET_PASSWORD_EMAIL.SUCCEEDED, payload: { message: "Password reset Email has been sent Successfully" } });
      })
      ,
      (err => {
        dispatchWithNotification(dispatch, { type: RESET_PASSWORD_EMAIL.FAILED, payload: { error: err, message: err.errMsg } });
      })
    );
  };
}

export function switchCurrent(companyId, projectId, culture) {
  return (dispatch, getState, { webApi }) => {
    dispatchWithNotification(dispatch, { type: SWITCH_CURRENT.STARTED, payload: { message: "Profile Update started" } });
    
    systemService.switchCurrent(companyId, projectId, (culture || { key: "1" }).key).then(
      (ret => {
        switchLanguage((culture || { lang: "en" }).lang, (culture || { CultureId: "1" }).CultureId);
        dispatchWithNotification(dispatch, { type: SWITCH_CURRENT.SUCCEEDED, payload: { ...ret.data, lang: (culture || { lang: "en" }).lang } });
      })
      ,
      (err => {
        dispatchWithNotification(dispatch, { type: SWITCH_CURRENT.FAILED, payload: { error: err, message: err.errMsg } });
      })
    );
  };
}

export function requestResetPwdEmail(email, reCaptchaRequest, refCode) {
  return (dispatch, getState, { webApi }) => {
    dispatch({ type: RESET_PASSWORD_EMAIL.STARTED, payload: {} })
    authService.resetPwdEmail(email, reCaptchaRequest, refCode)
      .then(
        data => {
          dispatch({ type: RESET_PASSWORD_EMAIL.SUCCEEDED, payload: { ...data.data } })
        },
        error => {
          dispatchWithNotification(dispatch, { type: RESET_PASSWORD_EMAIL.FAILED, payload: error });
        }
      )
     //dispatch({ type: RESET_PASSWORD_EMAIL.SUCCEEDED, payload: { nounce: 'abc', ticketRight: '46346' }  })
  }
}

export function resetPassword(email, password, nounce, ticketLeft, ticketRight) {
  return (dispatch, getState, { webApi }) => {
    dispatch({ type: CHANGE_PASSWORD.STARTED, payload: {} })
    return authService.resetPassword(email, password, nounce, ticketLeft, ticketRight)
      .then(
        data => {
          dispatch({ type: CHANGE_PASSWORD.SUCCEEDED, payload: data.data });
          return data;
        },
        error => {
          dispatchWithNotification(dispatch, { type: CHANGE_PASSWORD.FAILED, payload: error });
          return Promise.reject(error);
        }
      )
  }
}
