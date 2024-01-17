import { getAsyncTypes } from '../helpers/actionType';
import { dispatchWithNotification } from '../redux/Notification';
import {
  objectListToDict, mergeArray
  , makeMultiDocFileObjectFromServer
  , reviseMultiDocFileObjectFromServer
  , reviseDocList, removeDocList
} from '../helpers/utils';
import { RememberCurrentAsync, GetCurrentAsync } from './Persist';
import ButtonDef from './_ScreenButtonDef';
import log from '../helpers/logger';
import { } from '../helpers/actionType';

export const initialRintagiScreenReduxState = {

  AuthRow: [{}],
  AuthCol: {},
  ColumnLabel: {},
  Label: {
    SearchListTitle: "Search List",
    SearchListSubtitle: "Please enter the master record",
    SearchListNaviBar: "Search List Navigator",
    MstTitle: "Edit Master Record Title",
    MstSubtitle: "Edit Master Record SubTitle",
    MstNaviBar: "Master Navigator",
    MstFound: "Reports found",
    DtlListTitle: "Detail list Title",
    DtlListSubtitle: "List of detail inside current master",
    DtlListFilterLabel: "Filter",
    DltListFilteredCountLabel: "Dtl records found:",
    DtlListNaviBar: "Detail List",
    DtlTitle: "Edit Detail Record",
    DtlSubtitle: "Enter or update detail",
    DtlNaviBar: "Detail",
    ViewMoreButton: "View {0} more",
    HelpButton: "Help",
    UnSavedPage: "You have not saved your changes. Are you sure you want to leave?",
    SearchPlaceholder: "On",
    QuickFilterLabel: "Quick Filter",
    NoResultsLabel: "No results found",
    NoDtlMessage: "No detail record selected",
    AddDtlMessage: "Click here to add a detail record",
    NoMstMessage: "No master record selected",
    AddMstMessage: "Click here to start a new master",
    NewMstLabel: "New master",
    CancelFileButton: "Cancel",
    DeleteFileButton: "Delete",
    FileLabel: "File",
  },
  Buttons: {
    ...ButtonDef,
    key: Date.now(),
  },
  MaxImageSize: {
    Width: 1024,
    Height: 768,
  },
  SearchList: {
    key: Date.now(),
    data: [],
  },
  Mst: {},
  NewMst: {},
  EditDtl: {},
  NewDtl: {},
  DtlList: {
    key: Date.now(),
    list: [],
  },
  ddl: {},
  ScreenFilter: [],
  ScreenCriteria: {
    SearchStr: '',
    TopN: 5,
    MatchCount: 0,
    Increment: 5,
    FilterId: 0,
    ShowFilter: true,
    key: Date.now(),
  },
  DtlFilter: {
    FilterColumnDdl: [],
    FilteredColumn: "_",
    FilteredValue: "",
    PageSize: 5,
    SortColumn: "",
    SortOrder: "A",
    ShowFilter: false,
    Limit: 5,
  },
  page_loading: true,
  searchlist_loading: false,
  saving: false,
  initialized: false,
  searchListVersion: Date.now(),
  mstVersion: Date.now(),
  dtlListVersion: Date.now(),
  dtlVersion: Date.now(),
};

/* exported general helper functions(no this nor class) */
export function GetBottomAction(state) {
  return (state || {}).BottomButtons || {};
}
export function GetRowAction(state) {
  return (state || {}).RowButtons || {};
}
export function GetDropdownAction(state) {
  return (state || {}).DropdownMenuButtons || {};
}

/* internal helper functions */
function ReviseButton(button, buttonDef, label) {
  if (button) {
    const buttonType = buttonDef.ButtonTypeName
    if ((button.RowButtons || {})[buttonType + "Button"]) {
      button.RowButtons[buttonType + "Button"].visible = buttonDef.ButtonVisible === "Y" && (buttonDef.RowVisible === "L" || buttonDef.RowVisible === "B");
      button.RowButtons[buttonType + "Button"].expose = buttonDef.RowVisible === "B";
      if (buttonDef.ButtonName) button.RowButtons[buttonType + "Button"].label = buttonDef.ButtonName;
      if (buttonDef.ButtonLongNm) button.RowButtons[buttonType + "Button"].labelLong = buttonDef.ButtonLongNm;
      if (label) button.RowButtons[buttonType + "Button"].label = label;
      button.RowButtons[buttonType + "Button"].tid = buttonDef.ButtonTypeId;
    }
    if ((button.DropdownMenuButtons || {})[buttonType + "Button"]) {
      button.DropdownMenuButtons[buttonType + "Button"].visible = buttonDef.ButtonVisible === "Y" && (buttonDef.TopVisible === "L" || buttonDef.TopVisible === "B");
      button.DropdownMenuButtons[buttonType + "Button"].expose = buttonDef.TopVisible === "B";
      if (buttonDef.ButtonName) button.DropdownMenuButtons[buttonType + "Button"].label = buttonDef.ButtonName;
      if (buttonDef.ButtonLongNm) button.DropdownMenuButtons[buttonType + "Button"].labelLong = buttonDef.ButtonLongNm;
      if (label) button.DropdownMenuButtons[buttonType + "Button"].label = label;
      button.DropdownMenuButtons[buttonType + "Button"].tid = buttonDef.ButtonTypeId;
    }
    if ((button.BottomButtons || {})[buttonType + "Button"]) {
      button.BottomButtons[buttonType + "Button"].visible = buttonDef.ButtonVisible === "Y" && (buttonDef.BotVisible === "L" || buttonDef.BotVisible === "B");
      button.BottomButtons[buttonType + "Button"].expose = buttonDef.BotVisible === "B";
      if (buttonDef.ButtonName) button.BottomButtons[buttonType + "Button"].label = buttonDef.ButtonName;
      if (buttonDef.ButtonLongNm) button.BottomButtons[buttonType + "Button"].labelLong = buttonDef.ButtonLongNm;
      if (label) button.BottomButtons[buttonType + "Button"].label = label;
      button.BottomButtons[buttonType + "Button"].tid = buttonDef.ButtonTypeId;
    }
  }
}

function MakeAutocompleteSearchValue(v) {
  return v ? "**" + v : "";
}

function ReviseScreenButtons(Buttons, ScreenButtons, Label) {
  const revisedLabels = Label || {};
  return (ScreenButtons || []).reduce(
    (b, v, i, _a) => {
      const buttonType = v.ButtonTypeName;
      const ml = b.MstList;
      const m = b.Mst;
      const dl = b.DtlList;
      const d = b.Dtl;
      ReviseButton(b.MstList, v, revisedLabels[buttonType + "Button"]);
      ReviseButton(b.Mst, v, revisedLabels[buttonType + "Button"]);
      ReviseButton(b.DtlList, v, revisedLabels[buttonType + "Button"]);
      ReviseButton(b.Dtl, v, revisedLabels[buttonType + "Button"]);
      return b;
    },
    JSON.parse(JSON.stringify(Buttons))
  )
}

function RefreshMst(mst, refresh) { return refresh ? { ...mst, key: Date.now() } : mst }
function RefreshEditDtl(dtl, refresh) { return refresh ? { ...dtl, key: Date.now() } : dtl }
function ReviseSearchListSelection(SearchList, payload, masterKeyColumnName) {
  return SearchList
    .filter(v => v.key || v[masterKeyColumnName])
    .map(
      (v, i) => {
        return {
          ...v,
          isSelected: (payload.SelectedKeyId && payload.SelectedKeyId === (v[masterKeyColumnName] || v.key))
        }
      }
    )
}


function ExpandDtlFilter(authCol, columnLabel) {
  const cols = authCol
    .filter((v) => v.MasterTable === "N" && v.ColVisible !== "N" && !v.DisplayName.match(/Button/g))
    .map((v) => ({ ColName: v.ColName, ColumnHeader: (columnLabel[v.ColName] || {}).ColumnHeader || v.ColName }));
  return cols;
}

function AutoCompleteSearch({ dispatch, v, topN, filterOn, searchApi, SucceededActionType, FailedActionType, ColumnName, forDtl, forMst }) {

  const keyLookup = (v || '').startsWith("**")
    ? searchApi(v, topN, filterOn)
    : new Promise((resolve) => resolve({ data: { data: [] } }));
  const promises = [
    keyLookup,
    searchApi((v || '').startsWith("**") ? null : v, topN, filterOn),
  ];
  return Promise.all(promises)
    .then(([keyLookup, ret]) => {
      dispatchWithNotification(dispatch, {
        type: SucceededActionType,
        payload: {
          ColumnName: ColumnName,
          forDtl: forDtl,
          forMst: forMst,
          data: mergeArray(keyLookup.data.data, ret.data.data, (o) => o.key),
          backfill: (v || '').startsWith("**"),
        }
      });
    }
      , (err) => {
        log.trace(err);
      })
    .catch(err => {
      log.trace(err);
    })
}

export class RintagiScreenRedux {
  GetScreenName() { throw new TypeError(this + " Must implement GetScreenName function"); }
  GetMstKeyColumnName() { throw new TypeError(this + " Must implement GetMstKeyColumnName function"); }
  GetDtlKeyColumnName() { throw new TypeError(this + " Must implement GetDtlKeyColumnName function"); }
  GetPersistDtlName() { throw new TypeError(this + " Must implement GetPersistDtlName function"); }
  GetPersistMstName() { throw new TypeError(this + " Must implement GetPersistMstName function"); }
  GetWebService() { throw new TypeError(this + " Must implement GetWebSerice function"); }
  GetReducerActionTypePrefix() { throw new TypeError(this + " Must implement GetReducerActionTypePrefix function"); };
  GetActionType(actionTypeName) { throw new TypeError(this + " Must implement GetActionType function"); }
  GetInitState() { throw new TypeError(this + " Must implement GetInitState function"); };
  GetDefaultDtl(state) { throw new TypeError(this + " Must implement GetDefaultDtl function"); }
  ExpandMst(mst, state, copy) { throw new TypeError(this + " Must implement ExpandMst function"); };
  ExpandDtl(dtlList, copy) { throw new TypeError(this + " Must implement ExpandDtl function"); };
  SearchListToSelectList(state) { throw new TypeError(this + " Must implement SearchListToSelectList function"); }

  /* reducer */
  ExpandDtlReducer(dtlList, copy) {
    if (!copy) return dtlList;
    else return dtlList.map(v => {
      return {
        ...v,
        [this.GetMstKeyColumnName()]: null,
        [this.GetDtlKeyColumnName()]: null,
      }
    });
  }
  ViewMoreDetailReducer(state, action) {
    return {
      ...state,
      DtlFilter: {
        ...state.DtlFilter,
        Limit: state.DtlFilter.Limit + state.DtlFilter.PageSize
      },
    };
  }
  ToggleMstListFilterReducer(state, action) {
    return {
      ...state,
      ScreenCriteria: {
        ...state.ScreenCriteria,
        ShowFilter: typeof action.show === "undefined" ? !state.ScreenCriteria.ShowFilter : action.show
      }
    }
  }
  ToggleDtlListFilterReducer(state, action) {
    return {
      ...state,
      DtlFilter: {
        ...state.DtlFilter,
        ShowFilter: typeof action.show === "undefined" ? !state.DtlFilter.ShowFilter : action.show
      }
    }
  }
  ChangeDtlListFilterReducer(state, action) {
    const payload = action.payload;
    return {
      ...state,
      DtlFilter: {
        ...state.DtlFilter,
        FilteredColumn: payload.FilteredColumn || state.DtlFilter.FilteredColumn,
        FilteredValue: payload.FilteredValue,
      }
    }
  };

  LoadPageReducer(state, action) {
    const actionTypeString = action.type;

    if (actionTypeString.endsWith(".STARTED")) {
      return { ...state, page_loading: true };

    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return { ...state, page_loading: false, access_denied: (action.payload.error || {}).errType === "access denied error" };
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {

      const payload = action.payload;
      const revisedState = {
        ...state,
        AuthCol: payload.AuthCol,
        AuthRow: payload.AuthRow,
        ColumnLabel: payload.ColumnLabel,
        SearchList: {
          key: Date.now(),
          data: payload.SearchList || [],
        },
        NewMst: payload.NewMst,
        NewDtl: payload.NewDtl,
        ScreenFilter: payload.ScreenFilter,
        ScreenHlp: payload.ScreenHlp,
        ScreenButtonHlp: payload.ScreenButtonHlp,
        ScreenCriteria: {
          SearchStr: state.ScreenCriteria.SearchStr,
          FilterId: state.ScreenCriteria.FilterId,
          Increment: state.ScreenCriteria.Increment,
          ...payload.ScreenCriteria,
          TopN: state.ScreenCriteria.TopN,
          key: Date.now()
        },
        ScreenCriDdl: this.ScreenCriDdl(state, action),
        Label: {
          ...state.Label,
          ...payload.Label,
        },
        SystemLabel: {
          ...state.SystemLabel,
          ...payload.SystemLabel,
        },
        ddl:
        {
          ...state.ddl,
          ...this.ScreenDdl(state, action),
        },
        DtlFilter: {
          ...state.DtlFilter,
          FilterColumnDdl: [{ ColName: '_', ColumnHeader: 'All' }, ...ExpandDtlFilter(payload.AuthCol, payload.ColumnLabel)],
        },
        page_loading: false,
        initialized: true,
        access_denied: false,
        Buttons: {
          ...ReviseScreenButtons(state.Buttons, payload.ScreenButtonHlp, payload.Label),
          key: Date.now(),
        },
        //key: (payload.ScopeKey || {}).key,
        key: payload.ScopeKey,
      };
      return (revisedState);
    }
  };
  GetSearchListReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return { ...state, searchlist_loading: true };

    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return { ...state, searchlist_loading: false };
    }
    else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      return {
        ...state,
        SearchList: {
          key: Date.now(),
          data: ReviseSearchListSelection(payload.SearchList, payload, this.GetMstKeyColumnName()),
        },
        ScreenCriteria: {
          ...state.ScreenCriteria,
          SearchStr: payload.SearchStr,
          TopN: payload.TopN,
          Total: payload.Total,
          MatchCount: payload.MatchCount,
          FilterId: payload.FilterId,
          key: Date.now(),
        },
        searchlist_loading: false,
        searchListVersion: Date.now(),
      };
    }
  };
  SelectMstReducer(state, action) {
    const payload = action.payload;
    return {
      ...state,
      Mst: {
        ...state.Mst,
      },
      SearchList: {
        key: Date.now(),
        data: ReviseSearchListSelection((state.SearchList || {}).data, payload, this.GetMstKeyColumnName()),
      },
      searchListVersion: Date.now(),
      mstVersion: Date.now(),
    };
  };
  GetMstReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return { ...state, page_loading: true };

    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return { ...state, page_loading: false };
    }
    else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      const mstKeyColumnName = this.GetMstKeyColumnName();
      return {
        ...state
        , Mst: this.ExpandMst(payload.Mst, state, payload.copy)
        , SearchList: {
          key: Date.now(),
          data: ReviseSearchListSelection((state.SearchList || {}).data, { ...payload, SelectedKeyId: payload.Mst[mstKeyColumnName] }, mstKeyColumnName)
        }
        , EditDtl: {
          ...this.GetDefaultDtl(state),
          key: Date.now()
        }
        , page_loading: false
        , searchListVersion: Date.now()
        , mstVersion: Date.now()
      };
    }
  };
  GetDtlListReducer(state, action) {
    const payload = action.payload;
    return {
      ...state
      , DtlList: {
        key: Date.now(),
        data: this.ExpandDtl(payload.Dtl, payload.copy)
      },
    };
  };

  GetDdlReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      return {
        ...state,
        ddl:
        {
          ...state.ddl,
          [payload.ColumnName]: payload.data || state.ddl[payload.ColumnName]
        },
        EditDtl: RefreshEditDtl(state.EditDtl, payload.forDtl && payload.backfill),
        Mst: RefreshMst(state.Mst, payload.forMst && payload.backfill),
      };
    }
  }

  GetColumnOnDemandReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      if (payload.forMst) {
        return {
          ...state,
          Mst: {
            ...state.Mst,
            [payload.ColumnName]: payload.data,
            key: Date.now()
          }
        };

      }
      else {
        return {
          ...state,
          EditDtl: {
            ...state.EditDtl,
            [payload.ColumnName]: payload.data,
            key: Date.now()
          }
        };
      }
    }
  }
  GetDocumentListReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      if (payload.forMst) {
        return {
          ...state,
          Mst: {
            ...state.Mst,
            [(payload.reduxColumnName || payload.ColumnName)]: ((payload || {}).docList || []).map((o) => ({ ...makeMultiDocFileObjectFromServer(o), MasterId: payload.MasterId })),
            key: Date.now()
          }
        };

      }
      else {
        return {
          ...state,
          EditDtl: {
            ...state.EditDtl,
            [(payload.reduxColumnName || payload.ColumnName)]: ((payload || {}).docList || []).map((o) => ({ ...makeMultiDocFileObjectFromServer(o), MasterId: payload.MasterId })),
            key: Date.now()
          }
        };
      }
    }
  }
  GetDocumentContentReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      if (payload.forMst) {
        return {
          ...state,
          Mst: {
            ...state.Mst,
            [(payload.reduxColumnName || payload.ColumnName)]: reviseMultiDocFileObjectFromServer((state.Mst || {})[(payload.reduxColumnName || payload.ColumnName)], payload.result),
            key: Date.now()
          }
        };

      }
      else {
        return {
          ...state,
          EditDtl: {
            ...state.EditDtl,
            [(payload.reduxColumnName || payload.ColumnName)]: reviseMultiDocFileObjectFromServer((state.EditDtl || {})[(payload.reduxColumnName || payload.ColumnName)], payload.result),
            key: Date.now()
          }
        };
      }
    }
  }
  AddDocumentContentReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      if (payload.forMst) {
        return {
          ...state,
          Mst: {
            ...state.Mst,
            [(payload.reduxColumnName || payload.ColumnName)]: reviseDocList((state.Mst || {})[(payload.reduxColumnName || payload.ColumnName)], { ...payload.src, ...payload.result }),
            key: Date.now()
          }
        };

      }
      else {
        return {
          ...state,
          EditDtl: {
            ...state.EditDtl,
            [(payload.reduxColumnName || payload.ColumnName)]: reviseDocList((state.EditDtl || {})[(payload.reduxColumnName || payload.ColumnName)], { ...payload.src, ...payload.result }),
            key: Date.now()
          }
        };
      }
    }
  }
  DelDocumentContentReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return state;
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return state;
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      const payload = action.payload;
      if (payload.forMst) {
        return {
          ...state,
          Mst: {
            ...state.Mst,
            [(payload.reduxColumnName || payload.ColumnName)]: removeDocList((state.Mst || {})[(payload.reduxColumnName || payload.ColumnName)], payload.result.docIdList),
            key: Date.now()
          }
        };

      }
      else {
        return {
          ...state,
          EditDtl: {
            ...state.EditDtl,
            [(payload.reduxColumnName || payload.ColumnName)]: removeDocList((state.EditDtl || {})[(payload.reduxColumnName || payload.ColumnName)], payload.result.docIdList),
            key: Date.now()
          }
        };
      }
    }
  }
  EditDtlReducer(state, action) {
    const payload = action.payload;
    return {
      ...state
      ,
      EditDtl: {
        ...this.GetDefaultDtl(state),
        ...payload.dtl,
        ...payload.copy ? { [this.GetDtlKeyColumnName()]: null } : {},
        key: Date.now()
      }
    };
  };
  SaveMstReducer(state, action) {
    const actionTypeString = action.type;
    const payload = action.payload;
    if (actionTypeString.endsWith(".STARTED")) {
      return { ...state, page_saving: true, submittedOn: Date.now() };
    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return { ...state, page_saving: false, };
    } else if (actionTypeString.endsWith(".SUCCEEDED")) {
      return {
        ...state
        , Mst: this.ExpandMst(payload.Mst, state)
        , EditDtl:
        {
          ...(payload.keepDtl ? state.EditDtl : this.GetDefaultDtl(state)),
          key: Date.now()
        }
        , page_saving: false
        , page_loading: payload.deferredRelease ? true : state.page_loading
      };
    }

  };
  SaveCriReducer(state, action) {
    const actionTypeString = action.type;
    if (actionTypeString.endsWith(".STARTED")) {
      return { ...state, page_saving: true };

    } else if (actionTypeString.endsWith(".FAILED") || actionTypeString.endsWith(".ENDED")) {
      return { ...state, page_saving: false };
    }
    else if (actionTypeString.endsWith(".SUCCEEDED"))
      return {
        ...state,
        Mst: {
          ...state.Mst,
          key: Date.now()
        },
        ScreenCriteria: this.ReviseScreenCri(state, action),
        page_saving: false
      };
  };

  /* general selectors */
  GetDefaultMst(searchList, state) {
    const mst = searchList.reduce((r, v, i, a) => {
      return (v.isSelected || (i === 0) ? v : r);
    }, {});
    return mst;
  }
  LoadPageStaticData(dispatch, apiService, user, current) {
    const LOAD_PAGE = this.GetActionType("LOAD_PAGE");
    const isInitialized = current.initialized;
    const scopeKey = current.key;
    const scope = (({ CompanyId, ProjectId, SystemId, CultureId, key }) => ({ CompanyId, ProjectId, SystemId, CultureId, key }))(user || {});
    const screenName = this.GetScreenName();
    const _this = this;

    if (isInitialized && scopeKey && scopeKey >= user.key) {
      return new Promise(function (resolve, reject) {
        resolve({
          SystemLabel: current.SystemLabel,
          AuthCol: current.AuthCol,
          AuthRow: current.AuthRow,
          ColumnLabel: current.ColumnLabel,
          ScreenHlp: current.ScreenHlp,
          ScreenButtonHlp: current.ScreenButtonHlp,
          Label: current.Label,
          NewMst: current.NewMst,
          NewDtl: current.NewDtl,
          ..._this.ResolveDdlPromise(current),
        })
      })
    }
    else {

      // below should only happens once
      dispatchWithNotification(dispatch, { type: LOAD_PAGE.STARTED, payload: {} });
      const promises = [
        apiService.GetSystemLabels('cSystem'),
        apiService.GetAuthCol(scope),
        apiService.GetAuthRow(scope),
        apiService.GetScreenLabel(scope),
        apiService.GetScreenHlp(scope),
        apiService.GetScreenFilter(scope),
        apiService.GetScreenCriteria(scope),
        apiService.GetScreenButtonHlp(scope),
        apiService.GetLabels(screenName),
        apiService.GetNewMst(),
        apiService.GetNewDtl(),
        ..._this.GetScreenDdlApiPromise(apiService, scope),
        ..._this.GetCriDdlApiPromise(apiService, scope),
      ]

      return Promise.all(promises)
        .then(
          ([SystemLabels, AuthCol, AuthRow, ColumnLabel, ScreenHlp, ScreenFilter, ScreenCriteria, ScreenButtonHlp, Labels, NewMst, NewDtl, ...Rest
          ]) => {
            const payload = {
              SystemLabel: objectListToDict(SystemLabels.data.data, "LabelKey", (v) => (v.LabelText)),
              AuthCol: AuthCol.data.data,
              AuthRow: AuthRow.data.data,
              ColumnLabel: ColumnLabel.data.data,
              ScreenHlp: ScreenHlp.data,
              ScreenFilter: ScreenFilter.data.data,
              ScreenCriteria: ScreenCriteria.data.data,
              ScreenButtonHlp: ScreenButtonHlp.data.data,
              Label: objectListToDict(Labels.data.data, "LabelKey", (v) => (v.LabelText)),
              NewMst: NewMst.data[0],
              NewDtl: NewDtl.data[0],
              ..._this.ResolveDdlPromise(Rest),
              ScopeKey: scope.key || Date.now(),
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
            return Promise.reject(error);
          }
        )
        .catch(error => {
          log.debug(error);
          return Promise.reject(error);
        });
    }
  }


  /* exposed action creators via 'this' */
  LoadPage(src, options) {
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const persistMstName = this.GetPersistMstName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    return (async (dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const currentRedux = getState()[screenName] || {};
      const user = (getState().auth || {}).user; // use with cautions, if possible should be passed in from callers if the coupling is tight
      const { mstId, dtlId, reloadMst, reloadMstList, reloadDtl } = options;
      const searchStr = currentRedux.ScreenCriteria.SearchStr;
      const topN = ((options || {}).topN) || currentRedux.ScreenCriteria.TopN;
      const filterId = ((options || {}).FilterId) || currentRedux.ScreenCriteria.FilterId;
      const isInitialized = currentRedux.initialized;
      const specificMstId = typeof mstId !== 'undefined' && mstId !== '_';
      //      const rememberedMst = GetCurrent(persistMstName);
      const rememberedMst = await GetCurrentAsync(persistMstName);
      const rememberedMstId = (mstId === "_") && (rememberedMst || {})[mstKeyColumeName];
      const _this = this;
      const skipMetaData = (options || {}).SkipMetaData === true || (options || {}).SkipMetaData === 'Y';
      const skipSupportingData = (options || {}).SkipSupportingData === true || (options || {}).SkipSupportingData === 'Y';

      // shortcut due to programmer laziness
      if (skipMetaData
        || skipSupportingData
        // only first time and in some situation multiple download is better
        // for low latency network
        || (false && !isInitialized)
      ) {
        const currentKeyId = specificMstId ? mstId
          : (typeof mstId !== 'undefined'
            ? (currentRedux.Mst || {})[mstKeyColumeName] || rememberedMstId
            : undefined);
        return this.LoadInitPage({
          MstId: mstId !== '!' ? currentKeyId : null,
          TopN: topN,
          ...options,
          FirstOrDefault: options.FirstOrDefault || (mstId && (mstId !== '_' || rememberedMstId)),
        })(dispatch, getState, { webApi })
          .then(ret => {
            return _this.LoadSearchList(src, mstId, options, searchStr, topN, filterId)(dispatch, getState, { webApi });
          })
          .catch(error => {
            return Promise.reject(error);
          })
      }

      return this.LoadPageStaticData(dispatch, apiService, user, currentRedux)
        .then(
          (payload) => {
            return this.LoadSearchList(src, mstId, options, searchStr, topN, filterId)(dispatch, getState, { webApi });
          }
          ,
          (error) => {
            log.debug(error);
            return Promise.reject(error);
          })
        .catch(error => {
          return Promise.reject(error);
        });
    }).bind(this);
  }

  LoadInitPage(options, user = {}, current = {}) {
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const LOAD_PAGE = this.GetActionType("LOAD_PAGE");
    const scopeKey = current.key;
    const scope = (({ CompanyId, ProjectId, SystemId, CultureId, key }) => ({ CompanyId, ProjectId, SystemId, CultureId, key }))(user || {});
    const _this = this;
    return ((dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const user = (getState().auth || {}).user; // use with cautions, if possible should be passed in from callers if the coupling is tight
      const GET_SEARCH_LIST = this.GetActionType("GET_SEARCH_LIST");
      const loadPageOptions = {
        ...options,
        SkipMetaData: options.SkipMetaData === true || options.SkipMetaData == 'Y' ? 'Y' : 'N',
        SkipSupportingData: options.SkipSupportingData === true || options.SkipSupportingData == 'Y' ? 'Y' : 'N',
        SkipOnDemandData: options.SkipOnDemandData === true || options.SkipOnDemandData == 'Y' ? 'Y' : 'N',
        ReAuth: options.ReAuth === true || options.ReAuth == 'Y' ? 'Y' : 'N',
        FirstOrDefault: options.FirstOrDefault === true || options.FirstOrDefault == 'Y' ? 'Y' : 'N',
      }
      dispatchWithNotification(dispatch, { type: GET_SEARCH_LIST.STARTED, payload: {} });
      const promises = Promise.all([
        apiService.LoadInitPage(loadPageOptions),
        ...(!options.SkipSupportingData && _this.GetCriDdlApiPromise(apiService, scope)),
      ]);

      return promises
        .then(([ret, ...rest]) => {
          if (!options.skipMetaData) {
            const i = 0;
            const ScreenCriDdl =
              _this.ScreenCriDdlDef
                .filter(c => c.payloadDdlName)
                .reduce((a, v, i) => { a[v.payloadDdlName] = rest[i].data.data; return a; }, {})
            const payload = {
              SystemLabel: objectListToDict(ret.data.SystemLabels || [], "LabelKey", (v) => (v.LabelText)),
              AuthCol: ret.data.AuthCol,
              AuthRow: ret.data.AuthRow,
              ColumnLabel: objectListToDict(ret.data.ColumnDef, (v) => (v.ColumnName + v.TableId), (v) => (v)),
              ScreenHlp: (ret.data.ScreenHlp || [])[0] || {},
              ScreenFilter: ret.data.ScreenFilter,
              ScreenCriteria: objectListToDict(ret.data.ScreenCriteria || [], "ColumnName", (v) => (v)),
              ScreenButtonHlp: ret.data.ScreenButtonHlp,
              Label: objectListToDict(ret.data.Labels || [], "LabelKey", (v) => (v.LabelText)),
              NewMst: ret.data.NewMst || {},
              NewDtl: ret.data.NewDtl || {},
              ScopeKey: scope.key || Date.now(),
              ...(ret.data.Ddl || {}),
              ScreenCriDdl: ScreenCriDdl,
            }

            dispatchWithNotification(dispatch, {
              type: LOAD_PAGE.SUCCEEDED,
              payload: payload
            });
          }

          dispatchWithNotification(dispatch, {
            type: GET_SEARCH_LIST.SUCCEEDED,
            payload: {
              SearchStr: '',
              TopN: 50,
              Total: (ret.data.SearchList || []).length,
              FilterId: '',
              SearchList: ret.data.SearchList || [],
              SelectedKeyId: '',
              MatchCount: (ret.data.SearchList || []).length + 5,
            }
          });
          return Promise.resolve(ret);
        })
        .catch(
          (error => {
            dispatchWithNotification(dispatch, { type: GET_SEARCH_LIST.FAILED, payload: { error: error } })
            return Promise.reject(error);
          })
        )
    }).bind(this);
  }

  LoadSearchList(src, mstId, options, searchStr, topN, filterId) {
    const screenName = this.GetScreenName();
    const GET_SEARCH_LIST = this.GetActionType("GET_SEARCH_LIST");
    const webServiceName = screenName + "Service";
    const persistMstName = this.GetPersistMstName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const searchListApiName = this.GetApiName("GET_SEARCH_LIST");
    return (async (dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const { dtlId, reloadMst, reloadMstList, reloadDtl, refreshCri } = options;
      const rememberedMst = await GetCurrentAsync(persistMstName);
      const rememberedMstId = (mstId === "_" || (refreshCri)) && (rememberedMst || {})[mstKeyColumeName];
      const specificKeyId = typeof mstId !== 'undefined' && mstId !== '_';
      const currKeyId = specificKeyId ? mstId
        : (typeof mstId !== 'undefined'
          ? (current.Mst || {})[mstKeyColumeName] || rememberedMstId
          : undefined);
      dispatchWithNotification(dispatch, { type: GET_SEARCH_LIST.STARTED, payload: {} });

      return apiService[searchListApiName](searchStr, topN, filterId)
        .then(
          (SearchList => {
            return dispatchWithNotification(dispatch, {
              type: GET_SEARCH_LIST.SUCCEEDED,
              payload: {
                SearchStr: searchStr,
                TopN: topN,
                Total: SearchList.data.total,
                FilterId: filterId,
                SearchList: SearchList.data.data,
                SelectedKeyId: currKeyId,
                MatchCount: SearchList.data.matchCount,
              }
            }).then(
              () => {
                //this.LoadMst(currKeyId || mstId, src, options)(dispatch, getState, { webApi });
                const _currKeyId = mstId === '!' ? ((((SearchList || {}).data || {}).data || [])[0] || {}).key : currKeyId;
                const refreshedMstId = (mstId === '!' && !_currKeyId) ? 0 : (_currKeyId || mstId || (refreshCri && rememberedMstId));
                this.LoadMst(refreshedMstId, src, options)(dispatch, getState, { webApi })
                .catch(err =>{
                  log.debug(`fail to reload mst after search list refresh ${refreshedMstId}`);
                })
                return Promise.resolve(SearchList);
              }
            )
          })
          , (error => {
            log.debug(error);
            dispatchWithNotification(dispatch, { type: GET_SEARCH_LIST.FAILED, payload: { error: error } });
            return Promise.reject(error);
          })
        ).catch(error => {
          return Promise.reject(error);
        })
    }).bind(this);
  }
  LoadMst(mstId, src, options = {}) {
    const screenName = this.GetScreenName();
    const GET_MST = this.GetActionType("GET_MST");
    const GET_DTL_LIST = this.GetActionType("GET_DTL_LIST");
    const EDIT_DTL = this.GetActionType("EDIT_DTL");
    const webServiceName = screenName + "Service";
    const persistMstName = this.GetPersistMstName();
    const persistDtlName = this.GetPersistDtlName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const getMstApiName = this.GetApiName("GET_MST");
    const getDtlListApiName = this.GetApiName("GET_DTL_LIST");
    return (async (dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const rememberedMst = await GetCurrentAsync(persistMstName);
      const rememberedDtl = (await GetCurrentAsync(persistDtlName) || {}).dtl;
      const rememberedMstId = (rememberedMst || {})[mstKeyColumeName] || (rememberedDtl || {}).mstId;
      const selectedMst = this.GetDefaultMst((current.SearchList || {}).data, current);
      const newMst = current.NewMst || {};
      const newDtl = current.NewDtl || {};
      const currMst = current.Mst;
      const keyId = mstId !== '_' ? (mstId || ({}).key) : mstId && (rememberedMst || {})[mstKeyColumeName];
      const { dtlId, copy, refreshCri } = options;
      const currentDtlList = (current.DtlList || {}).data || [];

      if (keyId
        //&& src !== "Dtl" && src !== "DtlList" 
        &&
        ((currMst[mstKeyColumeName] !== keyId) || true)
      ) {
        dispatch({ type: GET_MST.STARTED, payload: { SelectedKeyId: keyId } })

        return Promise.all([
          apiService[getMstApiName](keyId),
          apiService[getDtlListApiName](keyId),
        ])
          .then(([mst, dtl]) => {
            if (mst.data.length > 0) {
              const revisedMst = this.ExpandMst({
                ...mst.data[0],
                [mstKeyColumeName]: copy ? null : mst.data[0][mstKeyColumeName]
              }, current, copy);
              RememberCurrentAsync(persistMstName, revisedMst);
              // setTimeout(() => {
              dispatchWithNotification(dispatch, { type: GET_MST.SUCCEEDED, payload: { Mst: revisedMst, message: copy ? "This is a new copy of the master" : "", copy, Src: src } })
              dispatchWithNotification(dispatch, { type: GET_DTL_LIST.SUCCEEDED, payload: { Dtl: dtl.data, copy, Src: src } });
              // }, 2000);
              this.BackFillMstAsyncColumns(revisedMst, dispatch, getState, { webApi }, options);
              if (dtlId) {
                this.SelectDtl(mstId, dtlId, -1)(dispatch, getState, { webApi });
              }
              else {
                this.BackFillDtlAsyncColumns(revisedMst, newDtl, dispatch, getState, { webApi });
              }
              return revisedMst;
            }
            else {
              RememberCurrentAsync(persistMstName, null);
              dispatchWithNotification(dispatch, { type: GET_MST.FAILED, payload: { message: "failed to load required record " + keyId } });
              return Promise.resolve({})
            }
          },
            (err) => {
              dispatchWithNotification(dispatch, { type: GET_MST.FAILED, payload: {} })
              dispatchWithNotification(dispatch, { type: GET_DTL_LIST.FAILED, payload: {} })
              const isAccessDenied = !!(err.errMsg || '').match(/access denied.*/);
              if (rememberedMstId === keyId && rememberedMstId && isAccessDenied) {
                // flush remembered, probably no longer valid(deleted/acl changed etc.)
                RememberCurrentAsync(persistMstName, null);
                RememberCurrentAsync(persistDtlName, null);
              }
              return Promise.reject(err);
            }
          )
          .catch(error => {
            log.debug(error);
            dispatchWithNotification(dispatch, { type: GET_MST.FAILED, payload: {} })
            dispatchWithNotification(dispatch, { type: GET_DTL_LIST.FAILED, payload: {} })
            const isAccessDenied = !!(error.errMsg || '').match(/access denied.*/);
            if (rememberedMstId === keyId && rememberedMstId && isAccessDenied) {
              // flush remembered, probably no longer valid(deleted/acl changed etc.)
              RememberCurrentAsync(persistMstName, null);
              RememberCurrentAsync(persistDtlName, null);
            }
          return Promise.reject(error);
          })
      }
      else {
        const useCopy = rememberedMst && !rememberedMst[mstKeyColumeName] && (keyId || mstId);
        const revisedMst = keyId ? currMst : (useCopy || refreshCri ? (rememberedMst || newMst) : newMst);
        dispatchWithNotification(dispatch, { type: GET_MST.SUCCEEDED, payload: { Mst: revisedMst, Src: src } });
        dispatchWithNotification(dispatch, { type: GET_DTL_LIST.SUCCEEDED, payload: { Dtl: keyId ? currentDtlList : useCopy || refreshCri ? currentDtlList : [], Src: src } });
        this.BackFillMstAsyncColumns(keyId ? currMst : useCopy || refreshCri ? rememberedMst : newMst, dispatch, getState, { webApi }, options);
        if (dtlId) {
          const dtl = await this.GetDtl(currentDtlList, dtlId, -1, revisedMst);
          dispatchWithNotification(dispatch, { type: EDIT_DTL.SUCCEEDED, payload: { dtl: dtl || (dtlId === '_' && (mstId === rememberedMstId) ? rememberedDtl : {}) } });
          return Promise.reslove(dtl);
        }
        else {
          this.BackFillDtlAsyncColumns(revisedMst, newDtl, dispatch, getState, { webApi });
          return Promise.resolve({});
        }
      }

    }).bind(this);
  }

  AddMst(mstId, src, idx) {
    return ((dispatch, getState, { webApi }) => {
      const persistMstName = this.GetPersistMstName();
      RememberCurrentAsync(persistMstName, null);
      return this.LoadMst(mstId, src, { copy: mstId && true })(dispatch, getState, { webApi });
    }).bind(this);
  }

  AddDtl(mstId, dtlId, idx) {
    const screenName = this.GetScreenName();
    const EDIT_DTL = this.GetActionType("EDIT_DTL");
    const persistDtlName = this.GetPersistDtlName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const dtlKeyColumnName = this.GetDtlKeyColumnName();
    return (async (dispatch, getState, { webApi }) => {
      const current = getState()[screenName] || {};
      const mst = current.Mst;
      const currentDtlList = (current.DtlList || {}).data;
      const dtl = await this.GetDtl(currentDtlList, dtlId, idx, mst);
      const newDtl = { ...(dtl || current.NewDtl), [dtlKeyColumnName]: null };
      RememberCurrentAsync(persistDtlName, { mstId: mst[mstKeyColumeName], dtl: newDtl });
      //log.debug("add detail", newDtl, GetCurrent(persistDtlName));
      dispatchWithNotification(dispatch, { type: EDIT_DTL.SUCCEEDED, payload: { dtl: newDtl || {}, message: dtlId ? "New copy of the detail" : "" } });
      // return Promise.reslove(newDtl);
      return newDtl;
    }).bind(this);
  }

  SelectMst(keyId, altKeyId, idx) {
    const SELECT_MST = this.GetActionType("SELECT_MST");
    const screenName = this.GetScreenName();
    return ((dispatch, getState, { webApi }) => {
      const current = getState()[screenName] || {};
      const currentList = (current.SearchList || {}).data || [];
      const currentMstId = currentList.length > 0 ? currentList[idx].keyId || currentList[idx].key : null;
      if (currentList.length > 0) {
        dispatchWithNotification(dispatch, { type: SELECT_MST.SUCCEEDED, payload: { SelectedKeyId: currentMstId } });
        return this.LoadMst(currentMstId, "MstList")(dispatch, getState, { webApi });
      }
      else {
        dispatchWithNotification(dispatch, { type: SELECT_MST.FAILED, payload: { error: "empty selection list" } });
        return Promise.resolve({});
      }
    }).bind(this);
  }

  SelectDtl(mstId, dtlId, idx) {
    const screenName = this.GetScreenName();
    const EDIT_DTL = this.GetActionType("EDIT_DTL");
    const webServiceName = screenName + "Service";
    const persistDtlName = this.GetPersistDtlName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const dtlKeyColumnName = this.GetDtlKeyColumnName();
    return (async (dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const currentMstList = (current.SearchList || {}).data;
      const mst = current.Mst;
      const currentDtlList = (current.DtlList || {}).data;
      if (mst[mstKeyColumeName] === mstId || mstId === "_") {
        const rememberedDtl = (await GetCurrentAsync(persistDtlName) || {}).dtl;
        const rememberedMstId = (await GetCurrentAsync(persistDtlName) || {}).mstId;
        if (currentDtlList.length > 0) {
          const dtl = await this.GetDtl(currentDtlList, dtlId, idx, mst);
          if (dtl) RememberCurrentAsync(persistDtlName, {
            mstId: mst[mstKeyColumeName],
            dtl: dtl
          });
          this.BackFillDtlAsyncColumns(mst, dtl, dispatch, getState, { webApi });
          dispatch({ type: EDIT_DTL.SUCCEEDED, payload: { dtl: dtl || (dtlId === '_' && (rememberedMstId === mst[mstKeyColumeName]) ? rememberedDtl : {}) } });
          return dtl;
        }
        else {
          dispatch({ type: EDIT_DTL.SUCCEEDED, payload: { dtl: {} } });
          return {};
        }
      }
      else {
        return this.LoadMst(mstId, { dtlId: dtlId })(dispatch, getState, { webApi });
      }
    }).bind(this);
  }

  async GetDtl(dtlList, dtlId, idx, mst) {
    const persistDtlName = this.GetPersistDtlName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const dtlKeyColumnName = this.GetDtlKeyColumnName();
    const { mstId, dtl } = await GetCurrentAsync(persistDtlName) || {};
    const rememberedDtl = dtl;
    return dtlList.reduce((a, v, i) =>
      (
        (dtlId && v[dtlKeyColumnName] === dtlId)
        ||
        (dtlId === "_"
          && idx < 0
          &&
          (
            (rememberedDtl || {})[dtlKeyColumnName] === v[dtlKeyColumnName] || (v.TmpKeyId && (rememberedDtl || {}).TmpKeyId === v.TmpKeyId && !v[dtlKeyColumnName])
          )
        )
        ||
        ((!dtlId || dtlId === "_") && i === idx && ((mstId === mst[mstKeyColumeName] && mst[mstKeyColumeName]) || !v[dtlKeyColumnName]))
      ) ? v : a, null);
  }

  async GetMst(mstList, mstId, idx) {
    const persistMstName = this.GetPersistMstName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const rememberedMst = await GetCurrentAsync(persistMstName);
    return mstList.reduce((a, v, i) =>
      (
        (mstId && v.key === mstId)
        ||
        (mstId === "_"
          && idx < 0
          &&
          (
            (rememberedMst || {})[mstKeyColumeName] === v.key || (v.TmpKeyId && (rememberedMst || {}).TmpKeyId === v.TmpKeyId && !v.key)
          )
        )
        ||
        ((!mstId || mstId === "_") && i === idx)
      ) ? v : a, null);
  }

  SavePage(currentState, mst, dtl, { persist, keepDtl, ...rest }) {
    const screenName = this.GetScreenName();
    const SAVE_MST = this.GetActionType("SAVE_MST");
    const webServiceName = screenName + "Service";
    const persistDtlName = this.GetPersistDtlName();
    const mstKeyColumeName = this.GetMstKeyColumnName();
    const dtlKeyColumnName = this.GetDtlKeyColumnName();
    return ((dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const currentCriteria = current.ScreenCriteria || {};
      dispatchWithNotification(dispatch, { type: SAVE_MST.STARTED, payload: {} });
      const _mst = {
        ...currentState.Mst,
        ...mst
      }
      const _dtl = [
        ...dtl
      ];

      const _options = {
        ...rest
      }

      return apiService.SaveData(
        Object.keys(_mst || {}).reduce((a, k, i) => { a[k] = Array.isArray(_mst[k]) ? null : _mst[k]; return a; }, {})
        , _dtl.map(o => Object.keys(o || {}).reduce((a, k, i) => { a[k] = Array.isArray(o[k]) ? null : o[k]; return a; }, {}))
        , { ...rest })
        .then(
          (ret => {
            dispatchWithNotification(dispatch, { type: SAVE_MST.SUCCEEDED, payload: { Mst: ret.data.mst, keepDtl: keepDtl, message: ret.data.message, deferredRelease: true, } });
            if (!keepDtl) RememberCurrentAsync(persistDtlName, null);
            return this.LoadSearchList("SaveData", ret.data.mst[mstKeyColumeName], { dtlId: keepDtl && (currentState.EditDtl || {})[dtlKeyColumnName] }, currentCriteria.SearchStr, currentCriteria.TopN || 0, currentCriteria.FilterId)(dispatch, getState, { webApi });
          })
          ,
          (err => {
            log.debug(err);
            dispatchWithNotification(dispatch, { type: SAVE_MST.FAILED, payload: { error: err, message: err.errMsg, validationErrors: err.validationErrors } })
            return Promise.reject(err);
          })
        )
        .catch(err => {
          return Promise.reject(err);
        })
      // .finally(x => {
      //   dispatchWithNotification(dispatch, { type: SAVE_MST.ENDED, payload: {} });
      // })
    }).bind(this);
  }

  DelMst(currentState, mstId, { ...rest }) {
    const screenName = this.GetScreenName();
    const DEL_MST = this.GetActionType("DEL_MST");
    const webServiceName = screenName + "Service";
    return ((dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      dispatchWithNotification(dispatch, { type: DEL_MST.STARTED, payload: {} });
      const _mst = {
        [this.GetMstKeyColumnName()]: mstId,
      }
      const _options = {
        ...rest
      }

      return apiService.DelMst(_mst, { ...rest })
        .then(
          (ret => {
            const persistMstName = this.GetPersistMstName();
            RememberCurrentAsync(persistMstName, null);
            dispatchWithNotification(dispatch, { type: DEL_MST.SUCCEEDED, payload: { message: ret.data.message } })
            return this.LoadPage("MstList", {})(dispatch, getState, { webApi });
          })
          ,
          (err => {
            log.debug(err);
            dispatchWithNotification(dispatch, { type: DEL_MST.FAILED, payload: { error: err, message: err.errMsg } })
            return Promise.reject(err);
          })
        )
        .catch(err => {
          return Promise.reject(err);
        })
      // .finally(x => {
      //   dispatchWithNotification(dispatch, { type: SAVE_MST.ENDED, payload: {} });
      // })
    }).bind(this);
  }

  SetScreenCriteria(searchStr, criteria, filterId) {
    const screenName = this.GetScreenName();
    const SAVE_CRI = this.GetActionType("SAVE_CRI");
    const webServiceName = screenName + "Service";
    return ((dispatch, getState, { webApi }) => {
      const apiService = (webApi || {})[webServiceName] || this.GetWebService();
      const current = getState()[screenName] || {};
      const topN = current.ScreenCriteria.TopN;
      dispatchWithNotification(dispatch, { type: SAVE_CRI.STARTED, payload: {} });

      return apiService.SetScreenCriteria(criteria)
        .then(
          (ret => {
            dispatchWithNotification(dispatch, { type: SAVE_CRI.SUCCEEDED, payload: { Cri: ret.data, message: ret.data.message } })
              .then((ret) => {
                return this.LoadSearchList(null, null, { refreshCri: true }, searchStr, topN, filterId)(dispatch, getState, { webApi });
              },
                (err) => {
                  return Promise.reject(err);
                });
          })
          ,
          (err => {
            dispatchWithNotification(dispatch, { type: SAVE_CRI.FAILED, payload: { error: err, message: err.errMsg } })
          })
        ).catch(err => {
          return Promise.reject(err);
        })
      // .finally(x => {
      //   dispatchWithNotification(dispatch, { type: SAVE_MST.ENDED, payload: {} });
      // })
    }).bind(this);
  }

  ViewMoreDtl() {
    const VIEW_MORE_DTL = this.GetActionType("VIEW_MORE_DTL");
    return {
      type: VIEW_MORE_DTL.SUCCEEDED, payload: {}
    };
  }

  ChangeMstListFilterVisibility(show) {
    const CHANGE_MSTLIST_FILTER_VISIBILITY = this.GetActionType("CHANGE_MSTLIST_FILTER_VISIBILITY");
    return {
      type: CHANGE_MSTLIST_FILTER_VISIBILITY.SUCCEEDED, payload: { show: show || true }
    };
  }

  ChangeDtlListFilterVisibility(show) {
    const CHANGE_DTLLIST_FILTER_VISIBILITY = this.GetActionType("CHANGE_DTLLIST_FILTER_VISIBILITY");
    return {
      type: CHANGE_DTLLIST_FILTER_VISIBILITY.SUCCEEDED, payload: { show: show || true }
    };
  }
  ChangeDtlListFilter(FilteredColumn, FilteredValue) {
    const CHANGE_DTLLIST_FILTER = this.GetActionType("CHANGE_DTLLIST_FILTER");
    return {
      type: CHANGE_DTLLIST_FILTER.SUCCEEDED, payload: { FilteredColumn, FilteredValue }
    };
  }

  ReviseScreenCri(state, action) {
    const payload = action.payload;
    return {
      ...state.ScreenCriteria,
      ...this.ScreenCriDdlDef.reduce((a, v) => {
        a[v.columnName] = { ...state.ScreenCriteria[v.columnName], LastCriteria: payload.Cri[v.columnName] }; return a;
      }, {}
      ),
      key: Date.now()
    }
  }

  /* exposed helper functions via 'this' */
  DtlListToSelectList(state, keyColumnName) {
    const isDdlType = {
      "ComboBox": true, "DropDownList": true, "ListBox": true, "RadioButtonList": true
    };
    const DtlFilter = state.DtlFilter;
    const DtlList = (state.DtlList || {}).data || [];
    const FilteredColumn = DtlFilter.FilteredColumn;

    if (!Array.isArray(state.AuthCol)) return DtlList;

    const columnsToCheck = state.AuthCol
      .filter((v) => v.MasterTable === "N" && v.ColVisible !== "N" && !v.DisplayName.match(/Button/g) && (FilteredColumn === "_" || v.ColName === FilteredColumn))
      .reduce((a, v) => { a[v.ColName + (isDdlType[v.DisplayName] ? "Text" : "")] = true; return a; }, {});
    const EditDtl = state.EditDtl || {};
    const FilterColumn = DtlFilter.FilterColumnDdl;
    const FilteredValue = DtlFilter.FilteredValue;
    const noFilter = FilteredValue === "" || FilteredValue === undefined || FilteredValue === null;
    const searchRegEx = new RegExp(FilteredValue, 'i');
    return DtlList
      .filter(v => {
        return noFilter
          || (Object.keys(v).filter(n => columnsToCheck[n] && (v[n] || "").match(searchRegEx)).length > 0)
      }
      )
      .map((v, i) => {
        return {
          ...v,
          idx: i,
          isSelected: ((EditDtl || {})[keyColumnName] && ((EditDtl || {})[keyColumnName] === (v || {})[keyColumnName])) || ((EditDtl || {}).TmpKeyId === v.TmpKeyId && v.TmpKeyId && !(v || {})[keyColumnName]),
        }
      })
  }

  ScreenCriDdl(state, { payload }) {
    return this.ScreenCriDdlDef.filter(c => c.payloadDdlName).reduce(
      (a, v) => { a[v.columnName] = (payload.ScreenCriDdl || {})[v.payloadDdlName]; return a }, {}
    )
  }
  ScreenDdl(state, { payload }) {
    return this.ScreenDdlDef.reduce(
      (a, v) => { a[v.columnName] = payload[v.payloadDdlName] || state.ddl[v.columnName]; return a }, {}
    )
  }

  GetActionHandler() {
    return this.actionReducers;
  }
  GetApiName(actionTypeName) {
    return this.ActionApiNameMapper[actionTypeName];
  }
  GetScreenButtons(state) {
    return ((state || {}).ScreenButtonHlp || []).reduce((a, v) => { a[v.ButtonTypeName] = { label: v.ButtonName, labelLong: v.ButtonLongNm, tid: v.ButtonTypeId, ButtonTypeName: v.ButtonTypeName }; return a; }, {})
  }

  ResolveDdlPromise(results) {
    if (Array.isArray(results)) {
      let screenDdlCount = 0;
      const screenDdl = this.ScreenDdlDef.reduce((a, v, i) => { a[v.payloadDdlName] = results[i].data.data; screenDdlCount++; return a; }, {})
      const screenCriDdl = this.ScreenCriDdlDef.filter(c => c.payloadDdlName).reduce((a, v, i) => { a[v.payloadDdlName] = results[i + screenDdlCount].data.data; return a; }, {})
      const x = {
        ...screenDdl,
        ScreenCriDdl: screenCriDdl,
      }
      return x;
    }
    else {
      return this.ScreenDdlDef.reduce((a, v, i) => { a[v.columeName] = results[v.columeName]; return a; }, {})
    }
  }
  GetScreenDdlApiPromise(apiService, scope) { return this.ScreenDdlDef.map(v => apiService[v.apiServiceName]("", 32767, "", scope)) }
  GetCriDdlApiPromise(apiService, scope) { return this.ScreenCriDdlDef.filter(c => c.payloadDdlName).map(v => apiService[v.apiServiceName]("", 32767, "", scope)) }
  BackFillDtlAsyncColumns(mst, dtl, dispatch, getState, { webApi }) {
    const mstId = (mst || {})[this.GetMstKeyColumnName()];
    const dtlId = (dtl || {})[this.GetDtlKeyColumnName()];
    this.ScreenDdlDef
      .filter(v => !v.forMst && v.isAutoComplete)
      .forEach(v => {
        const name = "Search" + v.columnName;
        this.SearchActions[name](MakeAutocompleteSearchValue((dtl || {})[v.columnName]), v.filterByColumnName ? (v.filterByMaster ? (mst || {})[v.filterByColumnName] : (dtl || {})[v.filterByColumnName]) : null)(dispatch, getState, { webApi })
      });
    this.ScreenOnDemandDef
      .filter(v => !v.forMst && mstId && dtlId)
      .filter(v => !v.forMst && v.type !== "RefColumn")
      .filter(v => !v.isFileObject || (mstId && dtlId))
      .forEach(v => {
        const name = "Get" + v.columnName;
        this.SearchActions[name](
          (mst || {})[this.GetMstKeyColumnName()]
          , (dtl || {})[this.GetDtlKeyColumnName()]
          , (mst || {})[v.refColumnName], (dtl || {})[v.refColumnName]
        )(dispatch, getState, { webApi })
      });
    const refColumns = this.ScreenOnDemandDef
      .filter(v => !v.forMst && v.type === "RefColumn")
      .reduce((a, v) => { a[v.refColumnName] = [...(a[v.refColumnName] || []), v]; return a; }, {})
    Object.keys(refColumns)
      .forEach(v => {
        const refColumnName = v;
        const name = 'GetRef' + refColumnName;
        this.SearchActions[name](
          (mst || {})[this.GetMstKeyColumnName()]
          , (dtl || {})[this.GetDtlKeyColumnName()]
          , (mst || {})[refColumnName], (dtl || {})[v.refColumnName], true)(dispatch, getState, { webApi })
      });

  }
  BackFillMstAsyncColumns(mst, dispatch, getState, { webApi }, options = {}) {
    const mstId = (mst || {})[this.GetMstKeyColumnName()];
    const _this = this;
    const skipDocList = options.SkipDocList;
    this.ScreenDdlDef
      .filter(v => v.forMst && v.isAutoComplete)
      .forEach(v => {
        const name = "Search" + v.columnName;
        this.SearchActions[("Search" + v.columnName)](MakeAutocompleteSearchValue((mst || {})[v.columnName]), v.filterByColumnName ? (mst || {})[v.filterByColumnName] : null)(dispatch, getState, { webApi })
      }
      );
    this.ScreenOnDemandDef
      .filter(v => v.forMst && mstId)
      .filter(v => v.forMst && v.type !== "RefColumn")
      .filter(v => !skipDocList || v.type !== "DocList")
      .filter(v => !v.isFileObject || mstId)
      .forEach(v => {
        const name = "Get" + v.columnName;
        console.log(`back fill ${name} `, mst)
        this.SearchActions[name](
          (mst || {})[this.GetMstKeyColumnName()]
          , null
          , (mst || {})[v.refColumnName], null)(dispatch, getState, { webApi })
      });
    const refColumns = this.ScreenOnDemandDef
      .filter(v => v.forMst && v.type === "RefColumn")
      .reduce((a, v) => { a[v.refColumnName] = [...(a[v.refColumnName] || []), v]; return a; }, {})
    Object.keys(refColumns)
      .forEach(v => {
        const refColumnName = v;
        const name = 'GetRef' + refColumnName;
        this.SearchActions[name](
          (mst || {})[this.GetMstKeyColumnName()]
          , null, (mst || {})[refColumnName], null, true)(dispatch, getState, { webApi })
      })
  }
  BackFillCriAsyncColumns(screenCriteria, dispatch, getState, { webApi }) {
    this.ScreenCriDdlDef
      .filter(v => v.isAutoComplete)
      .forEach(v => {
        const name = "Search" + v.columnName;
        this.SearchActions[("Search" + v.columnName)](MakeAutocompleteSearchValue(((screenCriteria || {})[v.columnName] || {}).LastCriteria), v.filterByColumnName ? ((screenCriteria || {})[v.columnName] || {}).LastCriteria : null)(dispatch, getState, { webApi })
      }
      )
  }

  MakeSearchAction(ddlColumnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    return ((v, filterByVal) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const actionType = _this.GetActionType(ddlColumnDef.actionTypeName);
        return AutoCompleteSearch(
          {
            dispatch,
            v,
            topN: 50,
            filterOn: filterByVal || "",
            forMst: ddlColumnDef.forMst,
            forDtl: !ddlColumnDef.forMst,
            searchApi: apiService[ddlColumnDef.apiServiceName],
            SucceededActionType: actionType.SUCCEEDED,
            FailedActionType: actionType.FAILED,
            ColumnName: ddlColumnDef.columnName
          })
      }).bind(this);
    }).bind(this);
  }

  MakeGetColumnOnDemandAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    return ((mstId, dtlId, mst = {}, dtl = {}) => {
      console.log(`get on demand column content`, columnName, mstId);
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        return apiService[columnDef.apiServiceName](mstId, dtlId, tableColumnName, forMst, columnName)
          .then(
            ret => {
              dispatch({ type: actionType.SUCCEEDED, payload: { data: ret.data && ret.data.length > 0 ? (ret.data[0] || {})[tableColumnName] : null, ColumnName: columnName, forMst: forMst, forDtl: !forMst } });
            }
            , err => {
              log.trace("dynamic column error", err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
            }
          )
          .catch(err => {
            log.trace("dynamic column exception", err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakeGetRefColumnOnDemandAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const refColumnName = columnDef.type === "RefColumn" ? columnDef.refColumnName : columnDef.columnName;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    return ((mstId, dtlId, refMstKeyId, refDtlKeyId, backfill) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        const refKeyId = (forMst ? refMstKeyId : refDtlKeyId);
        return apiService[columnDef.apiServiceName](mstId, dtlId, refKeyId, forMst, refColumnName)
          .then(
            ret => {
              dispatch({ type: actionType.SUCCEEDED, payload: { data: ret.data && ret.data.length > 0 ? (ret.data[0] || {})[tableColumnName] : null, ColumnName: columnName, backfill, forMst: forMst, forDtl: !forMst } });
            }
            , err => {
              log.trace("dynamic column error", err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
            }
          )
          .catch(err => {
            log.trace("dynamic column exception", err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakePullUpOnDemandAction(refObjs) {
    return Object.keys(refObjs).reduce(
      (a, k) => {
        const dependents = refObjs[k].dependents;
        const columnDef = dependents[0];
        const _this = this;
        const screenName = this.GetScreenName();
        const webServiceName = screenName + "Service";
        const forMst = columnDef.forMst;
        const refColumnName = columnDef.type === "RefColumn" ? columnDef.refColumnName : columnDef.columnName;
        a[k] = ((mstId, dtlId, refMstKeyId, refDtlKeyId, backfill) => {
          return ((dispatch, getState, { webApi }) => {
            const apiService = webApi[webServiceName] || _this.GetWebService();
            const api = apiService[columnDef.apiServiceName];
            const refKeyId = (forMst ? refMstKeyId : refDtlKeyId);
            return apiService[columnDef.apiServiceName](mstId, dtlId, refKeyId, forMst, refColumnName)
              .then(
                ret => {
                  (backfill) && dependents.forEach(dependent => {
                    // only dispatch on backfill(i.e. pull up for existing record), as this is also used for front end search which cannot update redux
                    const columnName = dependent.columnName;
                    const tableColumnName = dependent.tableColumnName;
                    const actionType = _this.GetActionType(dependent.actionTypeName);
                    dispatch({ type: actionType.SUCCEEDED, payload: { data: ret.data && ret.data.length > 0 ? (ret.data[0] || {})[tableColumnName] : null, ColumnName: columnName, backfill, forMst: forMst, forDtl: !forMst } });
                  });
                  return {
                    dependents: dependents,
                    result: ret.data && ret.data.length > 0 ? ret.data[0] : null
                  };
                }
                , err => {
                  log.trace("dynamic column error", err);
                  //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
                }
              )
              .catch(err => {
                log.trace("dynamic column exception", err);
              })
          }
          ).bind(this);
        }).bind(this);
        return a;
      },
      {}
    )
  }

  MakeGetDocumentListAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    return ((mstId, dtlId) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        return apiService[columnDef.apiServiceName](mstId, dtlId, forMst)
          .then(
            ret => {
              const resultDocList = (((ret || {}).data || {}).data || []);
              dispatch({
                type: actionType.SUCCEEDED
                , payload: { reduxColumnName: columnName, MasterId: mstId, forMst: forMst, forDtl: !forMst, docList: resultDocList }
              });
              return { mstId, dtlId, resultDocList };
            }
            , err => {
              log.trace("get document list column error", err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
              return Promise.reject(err);
            }
          )
          .catch(err => {
            log.trace("get document list column exception", err);
            return Promise.reject(err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakeGetDocumentContentAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    return (({ mstId, dtlId, docId }) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        return apiService[columnDef.apiServiceName](mstId, dtlId, forMst, docId, columnName)
          .then(
            ret => {
              dispatch({
                type: actionType.SUCCEEDED
                , payload: { result: { ...((ret || {}).data || [])[0], DocId: docId }, reduxColumnName: columnName, ColumnName: columnName, forMst: forMst, forDtl: !forMst }
              })
              return ret;
            }
            , err => {
              log.trace("get document content error", err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
              return Promise.reject(err);
            }
          )
          .catch(err => {
            log.trace("get document content exception", err);
            return Promise.reject(err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakeAddDocumentContentAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    const overwrite = true;
    return (({ mstId, dtlId, file }) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        const docId = (file || {}).DocId;
        const docJson = JSON.stringify(file);
        return apiService[columnDef.apiServiceName](mstId, dtlId, forMst, docId, overwrite, columnName, docJson)
          .then(
            ret => {
              dispatch({
                type: actionType.SUCCEEDED
                , payload: { keyId: mstId, reduxColumnName: columnName, forMst: forMst, forDtl: !forMst, result: ret.data[forMst ? 'mst' : 'dtl'] || {}, src: file }
              })
              return ret;
            }
            , err => {
              log.trace("add document content error", err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
              return Promise.reject(err);
            }
          )
          .catch(err => {
            log.trace("add document content exception ", err);
            return Promise.reject(err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakeDelDocumentContentAction(columnDef) {
    const _this = this;
    const screenName = this.GetScreenName();
    const webServiceName = screenName + "Service";
    const forMst = columnDef.forMst;
    const columnName = columnDef.columnName;
    const tableColumnName = columnDef.tableColumnName;
    const actionType = _this.GetActionType(columnDef.actionTypeName);
    return (({ mstId, dtlId, docId } = {}) => {
      return ((dispatch, getState, { webApi }) => {
        const apiService = webApi[webServiceName] || _this.GetWebService();
        const api = apiService[columnDef.apiServiceName];
        return apiService[columnDef.apiServiceName](mstId, dtlId, forMst, columnName, [docId + ""])
          .then(
            ret => {
              dispatch({
                type: actionType.SUCCEEDED,
                payload: { keyId: mstId, forMst: forMst, forDtl: !forMst, reduxColumnName: columnName, result: ret.data[forMst ? 'mst' : 'dtl'] || {}, src: { DocId: docId } }
              })
              return ret;
            }
            , err => {
              log.trace("delete document error", err);
              return Promise.reject(err);
              //                  dispatch({ type: actionType.FAILED, payload: { data:null, ColumnName:columeName, forMst:forMst, forDtl:!forMstr });
            }
          )
          .catch(err => {
            log.trace("delete document exception", err);
            return Promise.reject(err);
          })
      }
      ).bind(this);
    }).bind(this);
  }

  MakeDdlSelectors(ddlColumnDef) {
    const _this = this;
    return (state) => {
      return ((((state || {}).ddl || {})[ddlColumnDef.columnName]) || []).map((v, i) => {
        return {
          key: (ddlColumnDef.isAutoComplete ? v.key : v[ddlColumnDef.keyName]) || null,
          label: (ddlColumnDef.isAutoComplete ? v.label : v[ddlColumnDef.labelName]) || " ",
          value: (ddlColumnDef.isAutoComplete ? v.key : v[ddlColumnDef.keyName]) || "",
          obj: v,
          idx: i
        }
      })
    }
  }

  MakeCriDdlSelectors(ddlColumnDef) {
    const _this = this;
    return (state) => {
      return ((((state || {}).ScreenCriDdl || {})[ddlColumnDef.columnName]) || []).map((v, i) => {
        return {
          key: (ddlColumnDef.isAutoComplete ? v.key : v[ddlColumnDef.keyName]) || null,
          label: (ddlColumnDef.isAutoComplete ? v.label : v[ddlColumnDef.labelName]) || " ",
          value: (ddlColumnDef.isAutoComplete ? v.key : v[ddlColumnDef.keyName]) || "",
          obj: v,
          idx: i
        }
      })
    }
  }

  QuickFilterDdlToSelectList(state) {
    return ((state || {}).ScreenFilter || {}).map((v, i) => {
      return {
        key: v.ScreenFilterId,
        label: v.FilterName || " ",
        value: v.ScreenFilterId,
        obj: v,
        idx: i
      }
    })
  }

  MakeActionReducers() {
    const defaultReducer = (state, action) => (state);
    const LOAD_PAGE = getAsyncTypes(this.GetReducerActionTypePrefix(), 'LOAD_PAGE');
    const SAVE_CRI = getAsyncTypes(this.GetReducerActionTypePrefix(), 'SAVE_CRI');
    const GET_SEARCH_LIST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'GET_SEARCH_LIST');
    const GET_MST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'GET_MST');
    const SELECT_MST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'SELECT_MST');
    const GET_DTL_LIST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'GET_DTL_LIST');
    const SAVE_MST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'SAVE_MST');
    const ADD_MST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'ADD_MST');
    const DEL_MST = getAsyncTypes(this.GetReducerActionTypePrefix(), 'DEL_MST');
    const ADD_DTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'ADD_DTL');
    const EDIT_DTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'EDIT_DTL');
    const DEL_DTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'ADD_DTL');
    const DEL_ALLDTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'ADD_ALLDTL');
    const SAVE_DTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'SAVE_DTL');
    const VIEW_MORE_DTL = getAsyncTypes(this.GetReducerActionTypePrefix(), 'VIEW_MORE_DTL');
    const CHANGE_MSTLIST_FILTER_VISIBILITY = getAsyncTypes(this.GetReducerActionTypePrefix(), 'CHANGE_MSTLIST_FILTER_VISIBILITY');
    const CHANGE_DTLLIST_FILTER_VISIBILITY = getAsyncTypes(this.GetReducerActionTypePrefix(), 'CHANGE_DTLLIST_FILTER_VISIBILITY');
    const CHANGE_DTLLIST_FILTER = getAsyncTypes(this.GetReducerActionTypePrefix(), 'CHANGE_DTLLIST_FILTER');

    return {
      ...(LOAD_PAGE.bindActionReducer(this.LoadPageReducer.bind(this), true)),
      ...(SAVE_CRI.bindActionReducer(this.SaveCriReducer.bind(this), true)),
      ...(GET_SEARCH_LIST.bindActionReducer(this.GetSearchListReducer.bind(this), true)),
      ...(GET_MST.bindActionReducer(this.GetMstReducer.bind(this), true)),
      ...(SELECT_MST.bindActionReducer(this.SelectMstReducer.bind(this), true)),
      ...(GET_DTL_LIST.bindActionReducer(this.GetDtlListReducer.bind(this), true)),
      ...(SAVE_MST.bindActionReducer(this.SaveMstReducer.bind(this), true)),
      ...(ADD_MST.bindActionReducer(defaultReducer)),
      ...(DEL_MST.bindActionReducer(defaultReducer)),
      ...(ADD_DTL.bindActionReducer(defaultReducer)),
      ...(EDIT_DTL.bindActionReducer(this.EditDtlReducer.bind(this), true)),
      ...(DEL_DTL.bindActionReducer(defaultReducer)),
      ...(DEL_ALLDTL.bindActionReducer(defaultReducer)),
      ...(SAVE_DTL.bindActionReducer(defaultReducer)),
      ...(VIEW_MORE_DTL.bindActionReducer(this.ViewMoreDetailReducer.bind(this), true)),
      ...(CHANGE_MSTLIST_FILTER_VISIBILITY.bindActionReducer(this.ToggleMstListFilterReducer.bind(this), true)),
      ...(CHANGE_DTLLIST_FILTER_VISIBILITY.bindActionReducer(this.ToggleDtlListFilterReducer.bind(this), true)),
      ...(CHANGE_DTLLIST_FILTER.bindActionReducer(this.ChangeDtlListFilterReducer.bind(this), true)),
      ...(this.ScreenDdlDef.reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.GetDdlReducer.bind(this), true) }), {})),
      ...((this.ScreenOnDemandDef || []).filter(f => f.type !== "DocList").reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.GetColumnOnDemandReducer.bind(this), true) }), {})),
      ...((this.ScreenOnDemandDef || []).filter(f => f.type === "DocList").reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.GetDocumentListReducer.bind(this), true) }), {})),
      ...((this.ScreenDocumentDef || []).filter(f => f.type === "Get").reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.GetDocumentContentReducer.bind(this), true) }), {})),
      ...((this.ScreenDocumentDef || []).filter(f => f.type === "Add").reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.AddDocumentContentReducer.bind(this), true) }), {})),
      ...((this.ScreenDocumentDef || []).filter(f => f.type === "Del").reduce((a, v) => ({ ...a, ...getAsyncTypes(this.GetReducerActionTypePrefix(), v.actionTypeName).bindActionReducer(this.DelDocumentContentReducer.bind(this), true) }), {})),
    }
  }
  ReduxReducer() {
    const initState = this.GetInitState();
    const reducerPrefix = this.GetReducerActionTypePrefix();
    const reduxActionHandler = this.GetActionHandler();

    return (function (state = initState, action) {
      const actionType = action.type || "";
      if (reducerPrefix && actionType.startsWith(reducerPrefix)) {
        const handler = reduxActionHandler[action.type];
        if (typeof handler === "function") return handler(state, action);
        else return state;
      }
      else return state;
    }).bind(this);
  }
}
