import { getAsyncTypes } from '../helpers/actionType';
import * as SqlReportService from '../services/SqlReportService'
import { dispatchWithNotification } from '../redux/Notification'
import log from '../helpers/logger';
import { toMoney } from '../helpers/formatter';
import { objectListToDict } from '../helpers/utils';
import { version } from 'react';

// action types
const SCREEN_PREFIX = 'SqlReport';
export const LOAD_PAGE = getAsyncTypes(SCREEN_PREFIX,'LOAD_PAGE');
export const CHANGE_REPORT_FILTER_VISIBILITY = getAsyncTypes(SCREEN_PREFIX,'CHANGE_REPORT_FILTER_VISIBILITY');

//reducer
const initialState = {
  Label: {
    HelpButton: "Help",
    PdfButton: "PDF",
    ExcelButton: "Excel",
    WordButton: "Word"
  },
  ReportHlp: {
    DefaultHlpMsg: '',
    ProgramName: 'ArRptAgingSum',
    ReportTitle: 'Expense Report',
    ReportTypeCd: 'S',
    TemplateName: ''
  },
  ReportCriteria: {
    Summary: {
      ColumnHeader: "Summary",
      LastCriteria: ''
    },
    CompanyId10: {
      ColumnHeader: "Company",
      LastCriteria: ''
    },
    BTrxId: {
      ColumnHeader: "Description",
      LastCriteria: ''
    },
    ShowFilter: true,
  },
  ReportCriDdl: {
    CompanyId: [],
    BTrxId: [],
  },
  page_loading: false,
  saving: false,
  initialized: false,
};

export function SqlReportReducer(state = initialState, action) {
  const payload = action.payload;
  switch (action.type) {
    case CHANGE_REPORT_FILTER_VISIBILITY.SUCCEEDED:
      return {...state
        , ReportCriteria : {
          ...state.ReportCriteria,
          ShowFilter: typeof action.ShowFilter === "undefined" ? !state.ReportCriteria.ShowFilter : action.ShowFilter,
        }
      };
    case LOAD_PAGE.STARTED:
      return { ...state, page_loading: true };
    case LOAD_PAGE.SUCCEEDED:
    const revisedState = {
      ...state,
      ReportHlp: payload.ReportHlp,
      ReportCriteria: {
        ...state.ReportCriteria,
        ...payload.ReportCriteria
      },
      ReportCriDdl: payload.ReportCriDdl,
      page_loading: false,
      initialized: true,
      access_denied: false,
      key: Date.now()
    };
      return (revisedState);
    case LOAD_PAGE.FAILED:
      return {
        ...state
        , page_loading: false
        , access_denied: (action.payload.error || {}).errType === "access denied error"
      };
    default:
      return state;
  }
}

export function ShowSpinner(state) {
  return !state || state.page_loading;
}

export function LoadPageStaticData(rptId, dispatch, apiService, user, current) {
  const isInitialized = current.initialized;
  const scope = (({ CompanyId, ProjectId, SystemId, CultureId }) => ({ CompanyId, ProjectId, SystemId, CultureId }))(user || {});
  if (isInitialized) {
    return new Promise(function (resolve, reject) {
      resolve({
      })
    })
  }
  else {
    // below should only happens once
    dispatchWithNotification(dispatch, { type: LOAD_PAGE.STARTED, payload: {} });
    const promises = [
      apiService.GetReportHlp(rptId, scope),
      apiService.GetReportCriteria(rptId, scope),
      apiService.GetReportCriDdl(rptId, scope),
    ]
    return Promise.all(promises)
      .then(
        ([ReportHlp, ReportCriteria, ReportCriDdl]) => {
          const payload = {
            ReportHlp: ReportHlp.data,
            ReportCriteria: ReportCriteria.data.data,
            ReportCriDdl: ReportCriDdl.data
          }
          dispatchWithNotification(dispatch, {
            type: LOAD_PAGE.SUCCEEDED,
            payload: payload
          });
          return payload;
        }
        ,
        (error) => {
          log.debug(error);
          dispatchWithNotification(dispatch, { type: LOAD_PAGE.FAILED, payload: { error: error } })
        }
      )
      .catch(error => {
        log.debug(error);
      });
  }
}

export function LoadPage(rptId) {
  return (dispatch, getState, { webApi }) => {
    const apiService = webApi.SqlReportService || SqlReportService;
    const current = getState().SqlReport || {};
    const user = (getState().auth || {}).user; // use with cautions, if possible should be passed in from callers if the coupling is tight
    const isInitialized = current.initialized;

    return LoadPageStaticData(rptId, dispatch, apiService, user, current)
      .then(
        (payload) => {

        }
        ,
        (error) => { });
  }
}

export function changeReportFilterVisibility(show) {
  return (dispatch, getState, { webApi }) => {
    dispatch({
      type: CHANGE_REPORT_FILTER_VISIBILITY.SUCCEEDED,
      ShowFilter: show
    })
    }
}