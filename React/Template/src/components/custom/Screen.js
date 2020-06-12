import React, { Component } from 'react';
import { getIn, setIn } from 'formik';
import moment from 'moment';
import { getAddMstPath, getAddDtlPath, getNaviPath, getEditDtlPath, getEditMstPath, debounce, isEmailFormat, isEmpty, isEmptyArray, isEmptyObject, isEmptyId, isValidRange, delay } from '../../helpers/utils'
import { registerBlocker, unregisterBlocker } from '../../helpers/navigation';
import { GetDropdownAction, GetBottomAction, GetRowAction } from '../../redux/_ScreenReducer'
import { getDefaultPath, getReactContainerInfo, getReactContainerStatus, uuid } from '../../helpers/utils';
import { parsedUrl } from '../../helpers/domutils';
import { getUrl } from '../../services/systemService';
import log from '../../helpers/logger';
import { toCapital , _getQueryString, getUrlBeforeRouter } from '../../helpers/formatter';
import { bindActionCreators } from 'redux';
import { connect, dispatch } from 'react-redux';
import { Redirect, withRouter } from 'react-router';
import { checkBundleUpdate, refreshApp } from '../../redux/_Rintagi';

const isEmptyFileList = (fileList) => Array.isArray(fileList) && (fileList.length === 0 || (fileList.length === 1 && fileList[0].isEmptyFileObject))

//export default class RintagiScreen extends Component {
class RintagiScreen extends Component {
  constructor(props) {
    super(props);
    this.ScreenButtonAction = {
      InsRow: this.AddNewDtl.bind(this),
      Clear: this.AddNewMst.bind(this),
      Delete: this.DelMst.bind(this),
      EditHdr: this.EditHdr.bind(this),
      EditRow: this.EditRow.bind(this),
      DrillDown: this.DrillDown.bind(this),
      DelRow: this.DelDtl.bind(this),
      Copy: this.CopyHdr.bind(this),
      CopyRow: this.CopyRow.bind(this),
      Save: this.SaveMst.bind(this),
      SaveDtl: this.SaveDtl.bind(this),
      New: this.AddNewMst.bind(this),
      NewSave: this.NewSaveMst.bind(this),
      NewSaveDtl: this.NewSaveDtl.bind(this),
      SaveClose: this.SaveCloseMst.bind(this),
      SaveCloseDtl: this.SaveCloseDtl.bind(this),
      ExpTxt: this.ExpMstTxt.bind(this),
      Print: this.Print.bind(this),
      MstFilter: this.ToggleMstListFilterVisibility.bind(this),
    }

    if (this.props.checkBundleUpdate) {
      const bundleCheck = this.props.checkBundleUpdate;
      if (bundleCheck) {
        bundleCheck(document, window.location)
        .then(bundle => {
          const latestJS = bundle.latestMe.myJS;
          if (latestJS 
            && latestJS !== bundle.currentJSBundleName 
            && !this.noAutoRefresh) {
              setTimeout(() => {
                if (!this.noAutoRefresh && refreshApp(true)) {
                  alert('A new version of application is available. Application will be refreshed!');
                  setTimeout(() => {
                    window.location.reload();                    
                  }, 100);
                }
                else {
    
                }
              }, 0);  
            }
          return bundle;
        })
        .catch(error => {
          console.log(error);
          return Promise.reject(error);
        })
      }
    }
  }

  static GetScreenButtonDef(buttons, screenType, prevReactState) {
    if (buttons.key && buttons.key !== ((prevReactState || {}).buttons || {}).key) {

      const dropdownMenuButtons = GetDropdownAction(buttons[screenType]);
      const rowButtons = GetRowAction(buttons[screenType]);
      const bottomButtons = GetBottomAction(buttons[screenType]);
      // const moreButton =
      const hasDropdownMenuButton = Object.keys(dropdownMenuButtons).filter(v => dropdownMenuButtons[v].visible).length > 0;
      const hasRowButton = Object.keys(rowButtons).filter(v => rowButtons[v].visible).length > 0;
      const hasBottomButton = Object.keys(bottomButtons).filter(v => bottomButtons[v].visible).length > 0;
      const dropdownMenuButtonList = Object.keys(dropdownMenuButtons)
        .map(n => dropdownMenuButtons[n])
        .filter(v => v.visible)
        .sort((a, b) => a.order - b.order);
      const rowMenuButtonList = Object.keys(rowButtons)
        .map(n => rowButtons[n])
        .filter(v => v.visible)
        .sort((a, b) => a.order - b.order);
      const bottomButtonList = Object.keys(bottomButtons)
        .map(n => bottomButtons[n])
        .filter(v => v.visible)
        .sort((a, b) => a.order - b.order);
      return {
        key: buttons.key,
        bottomButtons: bottomButtons,
        rowButtons: rowButtons,
        dropdownMenuButtons: dropdownMenuButtons,
        hasDropdownMenuButton: hasDropdownMenuButton,
        hasRowButton: hasRowButton,
        hasBottomButton: hasBottomButton,
        dropdownMenuButtonList: dropdownMenuButtonList,
        rowMenuButtonList: rowMenuButtonList,
        bottomButtonList: bottomButtonList,
      }
    }
    else return null;

  }

  static ShowSpinner(state, src) {
    return !state || (src === "MstList" && state.searchlist_loading) || (!src && (state.page_loading || state.page_saving));
  }

  static ShowMoreMstBtn(state) {
    return state && (state.ScreenCriteria.TopN < state.ScreenCriteria.MatchCount)
  }

  GetAuthCol(state) {
    const authCol = !Array.isArray(state.AuthCol) ? state.AuthCol : (state.AuthCol || []).reduce((a, v, i, _a) => {
      a[v.ColName] = {
        ...v,
        readonly: v.ColReadOnly === "Y",
        visible: v.ColVisible !== "N",
      }; return a;
    }, {});
    return authCol;
  }

  ActionSuppressed(authRow, buttonType, mstId, dtlId) {
    const canAdd = authRow.AllowAdd === "Y" && authRow.ViewOnly !== "Y";
    const canUpd = authRow.AllowUpd === "Y" && authRow.ViewOnly !== "Y";
    const canDel = authRow.AllowDel === "Y" && authRow.ViewOnly !== "Y";

    return (
      (buttonType === "EditHdr" && !this.state.isMobile)
      ||
      (buttonType === "EditRow" && !this.state.isMobile)
      ||
      (buttonType === "Delete" && (!canDel || !mstId))
      ||
      (buttonType === "DelRow" && (!mstId || !dtlId))
      ||
      (buttonType === "DrillDown" && (!mstId))
      ||
      (buttonType === "InsRow" && (!mstId))
      ||
      (buttonType === "Copy" && (!mstId))
      ||
      (buttonType === "CopyRow" && (!dtlId))
      ||
      ((buttonType || "").match(/New|Copy/i) && !canAdd)
      ||
      ((buttonType || "").match(/Save|InsRow|DelRow|CopyRow/i) && !canUpd && mstId)
      ||
      ((buttonType || "").match(/Save|InsRow|DelRow|CopyRow/i) && !canAdd && !mstId)
    );
  }

  OnModalReturn(result) {
    if (result === "Y" && this.state.ModalSuccess) {
      this.state.ModalSuccess();
    }
    else if (result === "N" && this.state.ModalCancel) {
      this.state.ModalCancel();
    }
    this.setState({ ModalOpen: false, ModalSuccess: null, ModalCancel: null });
  }

  /* search list action handlers */
  SearchBoxFocus(e) {
    e.preventDefault();
    this.setState({
      searchFocus: !this.state.searchFocus
    })
  }

  ShowMoreSearchList() {
    return function (e) {
      const cri = this.GetReduxState().ScreenCriteria || {};
      const auxSystemLabels = this.GetReduxState().SystemLabel || {};
      e.preventDefault();
      const searchMstFn = (() => {
        this.props.LoadSearchList("MstList", "_", {}, cri.SearchStr, cri.TopN + cri.Increment, cri.FilterId);
      }).bind(this);
      if (!this.hasChangedContent) searchMstFn();
      else this.setState({ ModalOpen: true, ModalSuccess: searchMstFn, ModalColor: "warning", ModalTitle: auxSystemLabels.UnsavedPageTitle || "", ModalMsg: auxSystemLabels.UnsavedPageMsg || "" });
    }.bind(this);
  }

  SearchFilterValueChange(handleSubmit, setFieldValue, type, controlName) {
    return function (name, value) {
      if (type === 'autocomplete') {
        setFieldValue(name, value[0]);
      }
      else {
        setFieldValue(name, value);
      }
      setTimeout(() => {
        handleSubmit();
      }, 0);
    }.bind(this);
  }

  SearchFilterTextValueChange(handleSubmit, setFieldValue, type, controlName) {
    return function (evt) {
      const value = evt.target.value;
      setFieldValue(controlName, value);
      setTimeout(() => {
        handleSubmit();
      }, 0);
    }.bind(this);
  }

  SetCurrentRecordState(isDirty) {
    this.hasChangedContent = isDirty;
  }

  SelectMstListRow({ naviBar, useMobileView }) {
    const buttons = this.state.Buttons;
    const hasNoActionButtons = !(buttons.hasBottomButton || buttons.hasRowButton || buttons.hasDropdownMenuButton);
    const reduxState = this.GetReduxState();
    const auxSystemLabels = reduxState.SystemLabel || {};


    return function (e) {
      e.preventDefault();

      const idx = e.currentTarget.getAttribute('listidx');
      const keyId = e.currentTarget.getAttribute('keyid');
      const altKeyId = e.currentTarget.getAttribute('altkeyid');

      const reduxState = this.GetReduxState();
      const auxSystemLabels = reduxState.SystemLabel || {};

      const selectMstFn = (() => {
        this.props.SelectMst(keyId, altKeyId, idx);
        if (hasNoActionButtons) {
          this.props.history.push(getEditMstPath(getNaviPath(naviBar, "MstRecord", "/"), keyId || '_'));
        }
      }).bind(this);
      if (!this.hasChangedContent) selectMstFn();
      else this.setState({ ModalOpen: true, ModalSuccess: selectMstFn, ModalColor: "warning", ModalTitle: auxSystemLabels.UnsavedPageTitle || "", ModalMsg: auxSystemLabels.UnsavedPageMsg || "" });
    }.bind(this);
  }

  SelectDtlListRow = (naviBar, mst, dtl, i, mstKeyColumnName, dtlKeyColumeName) => {
    const _this = this;
    const buttons = this.state.Buttons;
    const hasNoActionButtons = !(buttons.hasRowButton || buttons.hasDropdownMenuButton);
    const reduxState = this.GetReduxState();
    const auxSystemLabels = reduxState.SystemLabel || {};
    return ((e) => {
      e.preventDefault();
      const selectDtlFn = (() => {
        _this.props.SelectDtl(mst[mstKeyColumnName], dtl[dtlKeyColumeName], i);
        const path = getEditDtlPath(getNaviPath(naviBar, "DtlRecord", "/"), dtl[dtlKeyColumeName] || '_');
        if (hasNoActionButtons) {
          _this.props.history.push(path);
        }
      }
      ).bind(this);
      if (!this.hasChangedContent) selectDtlFn();
      else this.setState({ ModalOpen: true, ModalSuccess: selectDtlFn, ModalColor: "warning", ModalTitle: auxSystemLabels.UnsavedPageTitle || "", ModalMsg: auxSystemLabels.UnsavedPageMsg || "" });
    }).bind(this);
  }

  FilterDtlList(values, { setErrors, resetForm, setValues /* setValues and other goodies */ }) {
    this.props.ChangeDtlListFilter(values.FilterBy.value, values.FilteredValue);
  }

  ResetFilterList(values, { setFieldValue }) {
    this.props.ChangeDtlListFilter('', '');
    const input = document.querySelector('.FilteredValueClass');
    input.value = '';
  }

  FilteredColumnChange(handleSubmit, setFieldValue, type, controlName) {
    const _this = this;
    return function (name, value) {
      if (type === 'autocomplete') {
        setFieldValue(name, value[0]);
      }
      else {
        setFieldValue(name, value);
      }
      _this.setState({
        DtlFilter: {
          ..._this.state.DtlFilter,
          FilteredColumn: value,
        }
      })
    }
  }

  /* screen action handlers for different input type assume formik is used 
   * should works hand in hand with validator logic(see TextChange/DropdownChange) to indicate what field trigger the change
   * to work around formik deficiency 
   */
  AutocompleteChange = (setFieldValue, setFieldTouched, forName, blur, values, dependents = []) => {
    const _this = this;
    return function (name, value) {
      log.debug('autocomplete change', value, name, forName)
      if (blur) setFieldTouched(name || forName, true);
      else {
        _this.FieldInFocus = name || forName;
        const choice = ((value || [])[0] || {});
        // only the 'value'(or key) not the object
        const val = choice.value;
        setFieldValue(name || forName, val);
        dependents.filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, blur, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }
    }
  }

  FieldChange(setFieldValue, setFieldTouched, forName, blur, values, dependents = []) {
    const _this = this;
    return function (name, value) {
      log.debug('field change', value, name)
      if (blur) setFieldTouched(name || forName, true);
      else {
        _this.FieldInFocus = name || forName;
        // object itself(different from above AutocompleteChange)
        const val = value[0];
        setFieldValue(name || forName, val);
        dependents
          .filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, blur, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }
    }
  }

  PhoneChange(setFieldValue, setFieldTouched, name, blur, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (value) {
      log.debug('phone change', value, name);
      if (blur) setFieldTouched(name, true);
      else {
        _this.FieldInFocus = name;
        // const formattedPhone = value.replace(/[- )(]/g,'');
        // log.debug(value, formattedPhone);
        const val = value;
        setFieldValue(name, value);
        dependents.filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, blur, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }
    }
  }


  DateChange(setFieldValue, setFieldTouched, name, blur, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (value) {
      log.debug('date change', value, name)
      if (blur) setFieldTouched(name, true);
      else {
        _this.FieldInFocus = name;
        const date = (value || {})._d;
        const isDate = typeof date === "object" && date.constructor === Date;
        const _date = isDate ? date : new Date(date);
        const localSortableDateString = new Date(_date - _date.getTimezoneOffset() * 60 * 1000).toISOString().replace(/Z/, '');
        const val = localSortableDateString;
        setFieldValue(name, localSortableDateString);
        dependents.filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, blur, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }
    }
  }

  DateChangeUTC(setFieldValue, setFieldTouched, name, blur, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (value) {
      log.debug('date change', value, name)
      if (blur) setFieldTouched(name, true);
      else {
        _this.FieldInFocus = name;
        const date = (value || {})._d;
        const isDate = typeof date === "object" && date.constructor === Date;
        const date8601 = isDate ? date.toISOString() : date;
        const val = date8601;
        setFieldValue(name, date8601);
        dependents.filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, blur, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }
    }
  }

  DropdownChange(setFieldValue, setFieldTouched, forName, values, dependents = []) {
    const _this = this;
    return function (name, value) {
      _this.FieldInFocus = name || forName;
      const val = value.value;
      log.debug('drop down change', val, name, forName)
      setFieldTouched(name || forName, true);
      setFieldValue(name || forName, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  DropdownChangeV1(setFieldValue, setFieldTouched, forName, values, dependents = []) {
    const _this = this;
    return function (name, value) {
      _this.FieldInFocus = name || forName;
      const val = value;
      log.debug('drop down change', val, name, forName)
      setFieldTouched(name || forName, true);
      setFieldValue(name || forName, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  RadioChange(setFieldValue, setFieldTouched, name, list, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (evt) {
      const idx = evt.currentTarget.getAttribute('listidx');
      const val = list[idx].value;
      log.debug('radio change', val, name)
      _this.FieldInFocus = name;
      setFieldTouched(name, true);
      setFieldValue(name, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  CheckboxChange(setFieldValue, setFieldTouched, name, list, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (evt) {
      const idx = evt.currentTarget.getAttribute('listidx');
      const val = list[idx].value;
      log.debug('checkbox change', idx, val, name)
      _this.FieldInFocus = name;
      setFieldTouched(name, true);
      setFieldValue(name, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  CheckListChange(setFieldValue, setFieldTouched, name, value, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (evt) {
      const idx = evt.currentTarget.getAttribute('listidx');
      const selected = values[name] || '';
      const selectedList = selected ? selected.replace('(', '').replace(')', '').split(',') : [];
      const check = selectedList.filter(o => (value == o)).length > 0;
      var val = '';

      if (check) {
        val = selectedList.filter(o => (value != o)).join(",");

      } else {
        val = selected + "," + value;
      }

      _this.FieldInFocus = name;
      setFieldTouched(name, true);
      setFieldValue(name, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  ListBoxChange(setFieldValue, setFieldTouched, forName, formikCurValues, dependents = []) {
    const _this = this;
    return function (name, value) {
      _this.FieldInFocus = name || forName;
      const val = value && '(' + value + ')';
      log.debug('List Box Change', val, name, forName)
      setFieldTouched(name || forName, true);
      setFieldValue(name || forName, val);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , formikCurValues);
    }
  }

  TextFocus(event) {
    // log.debug(event);
    // event.target.select();
    // event.preventDefault();
    // setTimeout(() =>
    // event.target.setSelectionRange(0, 9999)
    // , 0);
    event.preventDefault();
    const target = event.target;
    target.setSelectionRange(0, target.value.length);
    // setTimeout(target.select.bind(target), 0);
    // setTimeout(target.setSelectionRange(0, target.value.length), 0);
  }

  TextChange(setFieldValue, setFieldTouched, name, values, dependents = [], debObj) {
    const _this = this;
    const forName = name;
    return function (evt) {
      const value = evt.target.value;
      const val = value;
      _this.FieldInFocus = name;
      setFieldTouched(name, true);
      setFieldValue(name, value);

      const deb = () => {
        log.debug(dependents, values);
        dependents.filter(f => typeof f === "function")
          .reduce((values, f) => {
            const ret = f(val, name || forName, values, { setFieldValue, setFieldTouched, forName, _this });
            if (typeof ret == "object" && ret.then === "function") {
              return values; // can't handle promise, just pass formik values down the pipe
            }
            else
              return ret;
          }
            , values);
      }

      if (!(debObj || {}).waitTime) deb();
      else debounce(deb, debObj)();
    }
  }
  
  TextChangeEX = (evt) => {
    const formikBag = this.FormikBag;
    const value = evt.currentTarget.value;
    const idx = evt.currentTarget.getAttribute('listidx');
    const fieldname = evt.currentTarget.getAttribute('fieldname');
    const fieldpath = evt.currentTarget.getAttribute('fieldpath');
    const name = evt.currentTarget.getAttribute('name');
    const values = formikBag.values;
    log.debug('TextChangeEX', value, idx, fieldname, fieldpath, name, values, evt.currentTarget);
    this.FieldInFocus = name;
    formikBag.setFieldValue(name, value);
    // if (idx !== undefined && idx !== null && (values[fieldpath] || []).length > idx ) {
    //   const revisedList = values[fieldpath].map((o,i)=>( i===(+idx) ? {...o, [fieldname]: value} : o));
    //   log.debug('revised list', revisedList)
    //   formikBag.setFieldValue(fieldpath,revisedList);
    //   // /* must be flatten to handle error in formik */
    //   // formikBag.setFieldValue(name,value);
    // }
  }

  FileUploadChange(setFieldValue, setFieldTouched, name, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (fileList) {
      const revisedList = !fileList || fileList.length === 0 ? [{ fileName: '', mimeType: '', lastModified: '', base64: '', isEmptyFileObject: true }] : [...fileList];
      setFieldValue(name, revisedList);
      (setFieldTouched || (_this.FormikBag || {}).setFieldTouched || (v => v))(name, true, false);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(revisedList, name, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  FileUploadChangeV1(setFieldValue, setFieldTouched, name, values, dependents = []) {
    const _this = this;
    const forName = name;
    return function (value) {
      const file = value.base64.result ? {
        fileName: value.name,
        mimeType: value.mimeType,
        lastModified: value.lastModified,
        base64: value.base64.result,
        ts: value.ts,
      } : null;
      _this.setState({ filename: (file || {}).fileName });
      setFieldValue(name, file);
      dependents.filter(f => typeof f === "function")
        .reduce((values, f) => {
          const ret = f(file, name, values, { setFieldValue, setFieldTouched, forName, _this });
          if (typeof ret == "object" && ret.then === "function") {
            return values; // can't handle promise, just pass formik values down the pipe
          }
          else
            return ret;
        }
          , values);
    }
  }

  FileUploadChangeEX = (fileList, name, { listidx, fieldpath, dependents }) => {
    const formikBag = this.FormikBag;
    const values = formikBag.values;
    const revisedList = !fileList || fileList.length === 0 ? [{ fileName: '', mimeType: '', lastModified: '', base64: '', isEmptyFileObject: true }] : [...fileList];
    this.FieldInFocus = name;
    formikBag.setFieldValue(name, revisedList);
    (dependents || []).filter(f => typeof f === "function")
      .reduce((values, f) => {
        const ret = f(revisedList, name, values);
        if (typeof ret == "object" && ret.then === "function") {
          return values; // can't handle promise, just pass formik values down the pipe
        }
        else
          return ret;
      }
        , values);
    // if (listidx !== undefined && listidx !== null && (values[fieldpath] || []).length > listidx ) {
    //   const revisedList = values[fieldpath].map((o,i)=>( i===(+listidx) ? {...o, [fieldname]: revisedList} : o));
    //   log.debug('revised list', revisedList)
    //   formikBag.setFieldValue(fieldpath,revisedList);
    // }
  }

  DropZoneChange(setFieldValue, name) {
    const _this = this;
    return function (value) {
      console.log(value);
      setFieldValue(name, value);
    }
    // return function (value) {
    //   const file = value.base64.result ? {
    //     fileName: value.name,
    //     mimeType: value.mimeType,
    //     lastModified: value.lastModified,
    //     base64: value.base64.result,
    //   } : null;
    //   _this.setState({ filename: (file || {}).fileName });
    //   setFieldValue(name, file);
    // }
  }
  /* end on change function */

  /* standard validators */
  SubmitForm = formGroup => (evt) => {
    /* 
    this is intended to be used as OnClick for form submission. would be run BEFORE actual submit so we can use state/instance variable
    to control ValidatePage() behavior as Formik cannot distinguish between field level triggered validation and form level triggered validation
    MUST BE reset in the form validation function
    */

    //  log.debug(this.FormikBag || {});

    //   if (!(this.FormikBag || {}).isValid) {
    //     this.props.showNotification('E', { message: 'One or more fields contain errors' });
    //   }

    this.IsFormSubmit = true;
    this.FormGroup = formGroup;
    this.FormAction = formGroup;
    /* block further, uncomment 
    evt.preventDefault();
    evt.stopPropagation();
    */
  }

  GrabFormikBag = ({ ...rest }) => {
    /* grab formik callbacks during rendering{this.GrabFormikBag({...})}, undersirable but only way to interact with formik from non-user events like 
     * server side retrieval, timer based action etc as well as writing EX version of set field value and  */
    /* we store in instance variable and not state so no re-render is triggered */

    /* store full bag value if available, otherwise whatever passed in */
    this.FormikBag = this.Formik ? this.Formik.getFormikBag() : { ...rest }
    if (this.NotifyPostValidation) {
      // this is set from the form validator indicating require post validation treatment
      this.NotifyPostValidation = false; // done once per validate
      if (typeof this.FormikPostValidate === "function") {
        delay(this.FormikPostValidate, 0); // always async(i.e. not current stack frame)
      }
    }
  }

  /* written in this form so it is auto bind to 'this', DO NOT USE this form if intended to be override by sub-class(i.e. virtual in C#/C++ lingual) */
  ReceiveFormikRef = (formik) => {
    /* this is done via standard React <Formik ref={this.ReceiveFormikRef} /> construct, formik === null is a reset back React */
    this.Formik = formik
  }

  ValidatePage(values) {
    /* a generic way to handle post validation errors for formik 
     * mainly design for passing Formik info to components outside Formik context
     * like global buttons that depends on state of error, dirty flags etc.
     * it is better to be a callback by Formik but not available as of Formik 1.4
     * this should be called by the Formik Page validator
     * works hand in hand with GrabFormikBag in the rendering 
     */
    if (this.Formik && typeof this.FormikPostValidate === "function") {
      this.NotifyPostValidation = true;
    }
    // always return true if we are here so subclass can go on
    return true;
  }

  PassAll({ setFieldValue, setFieldTouched, name, message, values, formGroup, validators = [] }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      for (var i = 0; i < validators.length; i = i + 1) {
        const validator = validators[i];
        if (typeof validator !== "string" && typeof validator !== "function" && typeof validator !== "boolean") {
          throw new TypeError("validator can only be function/string(considered failed if not empty)/boolean(consider fail if false)");
        }
        const error = typeof validator === "function" ? validator(value, { setFieldValue, setFieldTouched, name, message, values, formGroup }) : validator;
        if ((typeof error === "string" && error) || (typeof error === "boolean" && !error)) {
          if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
          return typeof error === "string" ? error : message;
        }
      }
      return null;
    }
  }
  PassAny({ setFieldValue, setFieldTouched, name, message, values, formGroup, validators = [] }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      for (var i = 0; i < validators.length; i = i + 1) {
        const validator = validators[i];
        if (typeof validator !== "string" && typeof validator !== "function" && typeof validator !== "boolean") {
          throw TypeError("validator can only be function/string(considered failed if not empty)/boolean(consider fail if false)");
        }
        const error = typeof validator === "function" ? validator(value, { setFieldValue, setFieldTouched, name, message, values, formGroup }) : validator;
        if ((typeof error === "boolean" && error) || (typeof error !== "boolean" && !error)) return null;
      }
      if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
      return message;
    }.bind(this);
  }
  PassAnyEX({ setFieldValue, setFieldTouched, fieldname, message, values, formGroup, validators = [] }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      for (var i = 0; i < validators.length; i = i + 1) {
        const validator = validators[i];
        if (typeof validator !== "string" && typeof validator !== "function" && typeof validator !== "boolean") {
          throw TypeError("validator can only be function/string(considered failed if not empty)/boolean(consider fail if false)");
        }
        const error = typeof validator === "function" ? validator(value, { setFieldValue, setFieldTouched, fieldname, message, values, formGroup }) : validator;
        if ((typeof error === "boolean" && error) || (typeof error !== "boolean" && !error)) return null;
      }
      if (_this.FieldInFocus && fieldname && _this.FieldInFocus.match(new RegExp(fieldname + '$')) && !_this.IsFormSubmit) {
        setFieldTouched(_this.FieldInFocus, true, false);
      }
      return message;
    }
  }
  FailNone({ setFieldValue, setFieldTouched, name, message, values, formGroup, validators = [] }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      for (var i = 0; i < validators.length; i = i + 1) {
        const validator = validators[i];
        if (typeof validator !== "string" && typeof validator !== "function" && typeof validator !== "boolean") {
          throw TypeError("validator can only be function/string(considered failed if not empty)/boolean(consider fail if false)");
        }
        const error = typeof validator === "function" ? validator(value, { setFieldValue, setFieldTouched, name, message, values, formGroup }) : validator;
        if ((typeof error === "boolean" && error) || (typeof error !== "boolean" && !error)) return null;
      }
      if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
      return message;
    }
  }
  IsEmptyFieldValue = (value) => isEmpty(value) || isEmptyArray(value) || isEmptyObject(value) || isEmptyFileList(value)
  IsNotEmptyFieldValue = (value) => {
    const v = !(isEmpty(value) || isEmptyArray(value) || isEmptyObject(value) || isEmptyFileList(value))
    return v;
  }

  IsEmptyFileList = (value) => isEmptyFileList(value)
  RequiredField({ setFieldValue, setFieldTouched, name, message, values, formGroup }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      if (isEmpty(value)
        || (Array.isArray(value) && value.length === 0)
        // only simple object test, anything that is not simple we assume it is not empty(including Date which has no key)
        || (!Array.isArray(value) && typeof value === 'object' && value.constructor === Object && Object.keys(value).length === 0)
        // file list array needs to be handled differently to work around problem of Formik dirty detection
        || (isEmptyFileList(value))
      ) {
        if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);

        return message;
      }
      return null;
    }
  }

  RequiredFieldEX({ setFieldValue, setFieldTouched, fieldname, message, values, formGroup }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      if (_this.IsEmptyFieldValue(value)) {
        if (_this.FieldInFocus || _this.IsFormSubmit) {
          setFieldTouched(_this.FieldInFocus, true, false);
        }
        return message;
      }
      return null;
    }
  }

  MinLengthField(length, { setFieldValue, setFieldTouched, name, message, values, formGroup, allowEmpty }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      if ((value && value.length < length) || (!allowEmpty && isEmpty(value))) {
        if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
        return message;
      }
      return null;
    }
  }
  ValidEmailField({ setFieldValue, setFieldTouched, name, message, values, formGroup, allowEmpty }) {
    const _this = this;
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      if ((value && !isEmailFormat(value)) || (!allowEmpty && isEmpty(value))) {
        if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
        return message;
      }
      return null;
    }
  }
  ValidRangeField(min, max, { setFieldValue, setFieldTouched, name, message, values, formGroup, lowerBound, upperBound, allowEmpty }) {
    const _this = this;
    const validRange = isValidRange(lowerBound, upperBound);
    return function (value) {
      if (formGroup && _this.FormGroup && _this.FormGroup !== formGroup) return null;
      if (this.Formik && _this.deferValidation && !_this.isDeferredValidation) return null;
      if ((value && !validRange(value)) || (!allowEmpty && isEmpty(value))) {
        if (_this.FieldInFocus === name || _this.IsFormSubmit) setFieldTouched(name, true, false);
        return message;
      }
      return null;
    }
  }
  /* end validator function */

  TranslateHyperLink(url, dtl, relative, options) {
    var fullUrl = (url || '').match('^[a-zA-Z0-9]+:');    
    if (!url) return url;
    else if (fullUrl) {
      //window.open(url);
      return url;
    }
    else {
      var x = parsedUrl(url);
      if (x.pathname.match(/\.aspx$/i)) {
        var path = (x.pathname ||'').replace(/\.aspx$/i,'');
        var key = _getQueryString(x,'key');
        var sub = key ? (dtl ? "/Dtl/" :"/Mst/") + (key || '') : (dtl ? "/DtlList" :""); 
        return (relative ? "" : "/#") + path + sub;  
      }
      else return url;
    }
  }

  PopUpSearchLink(url, dtl, sameWindow, options) {
    const _this = this;
    return function (evt) {
      var fullUrl = (url || '').match('^[a-zA-Z0-9]+:');
      if (fullUrl) {
        //window.open(url);
        return true;
      }
      else {
        var translatedPath = _this.TranslateHyperLink(url, dtl, true, options);
        var myBaseUrl = getUrlBeforeRouter();
        if (translatedPath) {
          var newUrl = myBaseUrl + "/#" + translatedPath;
          if (sameWindow) {
            //window.location = newUrl;
            this.props.history.push(translatedPath);
          }
          else {
            window.open(newUrl);
          }
          evt.preventDefault();
          return false;
        }
        else {

        }
        return true;
      }
      }.bind(this);
  }
 
  StripEmbeddedBase64Prefix(base64string) {
    if (base64string && base64string.length > 0) {
      return base64string.replace(/^data:.+;base64,/, '');
      if (base64string.indexOf("data:image/jpeg;base64,") >= 0) {
        return base64string.replace("data:image/jpeg;base64,", "");
      }
      else if (base64string.indexOf("data:image/png;base64,") >= 0) {
        return base64string.replace("data:image/png;base64,", "");
      }
    }
    return base64string;
  }

  SetPageTitle(reduxState) {
    const screenHlp = reduxState.ScreenHlp;
    const siteTitle = ((screenHlp || {}).ScreenTitle || '') + ' - ' + this.SystemName;

    if (!this.titleSet && reduxState.initialized) {
      this.props.setTitle(siteTitle);
      this.titleSet = true;
    }
  }

  /* end of standard functions */

  /* screen button handlers */
  AddNewMst({ naviBar }) {
    return function (evt) {
      const path = getNaviPath(naviBar, "MstRecord", "/");
      const x = getAddMstPath(path);
      const _this = this;
      this.props.AddMst(null, "MstRecord", 0)
                .then(newMst => {
                  _this.props.history.push(getAddMstPath(getNaviPath(naviBar, "MstRecord", "/")))
                })
      evt.preventDefault();
    }.bind(this);
  };
  AddNewDtl({ naviBar, mstId }) {
    return function (evt) {
      const _this = this;
      this.props.AddDtl(mstId, null, -1)
                .then(newDtl => {
                  _this.props.history.push(getAddDtlPath(getNaviPath(naviBar, "DtlRecord", "/")));
                })
      evt.preventDefault();
    }.bind(this);
  }
  DrillDown({ naviBar, mstId, useMobileView }) {
    return function (evt) {
      if (this.props.history) {
        this.props.history.push(getNaviPath(naviBar, "DtlList", "/"));
      }
      else {

      }
      evt.preventDefault();
    }.bind(this);
  }
  EditHdr({ naviBar, mstId, useMobileView }) {
    return function (evt) {
      if (useMobileView) {
        const path = getEditMstPath(getNaviPath(naviBar, "MstRecord", "/"), mstId);
        this.props.history.push(getEditMstPath(getNaviPath(naviBar, "MstRecord", "/"), mstId));
      }
      evt.preventDefault();
    }.bind(this);
  }
  EditRow({ naviBar, mstId, dtlId, useMobileView }) {
    return function (evt) {
      if (useMobileView) {
        this.props.history.push(getEditDtlPath(getNaviPath(naviBar, "DtlRecord", "/"), dtlId || '_'));
      }
      evt.preventDefault();
    }.bind(this);
  }

  ToggleMstListFilterVisibility() {
    return this.props.changeMstListFilterVisibility;
  }

  Prompt({ requireConfirm, okFn, cancelFn, message }) {
    const reduxState = this.GetReduxState();
    const auxSystemLabels = reduxState.SystemLabel || {};
    return function (evt) {
      evt.preventDefault();
      if (!requireConfirm || requireConfirm(evt)) {
        if (this.state.ModalOpen !== undefined) {
          this.setState({ ModalOpen: true, ModalSuccess: okFn, ModalColor: "danger", ModalTitle: auxSystemLabels.WarningTitle || "", ModalMsg: message });
        }
        else if (window.confirm()) okFn(evt);
        else if (cancelFn) cancelFn(evt);
      }
      else if (okFn) okFn(evt);
    }.bind(this);
  }

  /* dirty prompt coordination */
  setDirtyFlag(dirty) {
    /* this is called during rendering but has side-effect, undesirable but only way to pass formik dirty flag around */
    if (dirty) {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = this.confirmUnload;
      registerBlocker(this.confirmUnload);
    }
    else {
      if (this.blocker) unregisterBlocker(this.blocker);
      this.blocker = null;
    }
    if (this.props.updateChangedState) this.props.updateChangedState(dirty);
    this.SetCurrentRecordState(dirty);
    return true;
  }

  confirmUnload(message, callback) {
    const confirm = () => {
      callback(true);
    }
    const cancel = () => {
      callback(false);
    }
    this.setState({ ModalOpen: true, ModalSuccess: confirm, ModalCancel: cancel, ModalHeader: false, ModalColor: "danger", ModalTitle: 'Unsaved Changes', ModalMsg: 'You have unsaved changes on this page. Are you sure you want to leave?' });
  }

  GetTargetId() {
    const { mstId, dtlId } = { ...this.props.match.params };
    return {
      targetMstId: mstId,
      targetDtlId: dtlId,
    }
  }
  /* binding function to simple data to various component */
  BindDateField = (value, { keyfieldname, valuefieldname, selectedObject } = {}) => {
    return new moment(value);
  }
  BindDropDownField = (value, choices, { keyfieldname, valuefieldname, selectedObject } = {}) => {
    const x = (choices || []).filter(o =>
      o === value
      || o[keyfieldname || 'value' || 'key'] === value
      || o[keyfieldname || 'value' || 'key'] === (value || {})[keyfieldname || 'value' || 'key']
    )[0]
    return selectedObject || typeof x !== "object" || true ? x : x[keyfieldname || 'value' || 'key']
  }
  BindAutocompleteField = (value, choices, { keyfieldname, valuefieldname } = {}) => {
    return (choices || []).filter(o => o === value || o[keyfieldname || 'value' || 'key'] === value || o[keyfieldname || 'value' || 'key'] === (value || {})[keyfieldname || 'value' || 'key'])
    // .map(o => ({
    //   ...o,
    //   label: toCapital(o.label)
    // }));
  }
  BindRadioField = (value, choice, { keyfieldname, valuefieldname } = {}) => {
    return choice === value || choice[keyfieldname || 'value' || 'key'] === value
  }
  BindCheckBoxField = (value, choice, { keyfieldname, valuefieldname } = {}) => {
    return choice === value || choice[keyfieldname || 'value' || 'key'] === value
  }
  BindCheckListField = (value, selected, { keyfieldname, valuefieldname } = {}) => {
    const selectedList = selected ? selected.replace('(', '').replace(')', '').split(',') : [];
    return selectedList.filter(o => (value == o)).length > 0;
  }
  BindMultiDocFileObject = (serverList, currentList) => {
    /* the initial server list can be # of attachments ! */
    if (!currentList) return (typeof serverList !== "object") ? [] : serverList;
    if (!serverList || (Array.isArray(serverList) && serverList.length === 0)) return currentList;
    const lookup = (serverList || []).reduce((a, o) => { a[o.DocId] = o; return a }, {});

    const revisedList = !Array.isArray(currentList)
      ? currentList
      : currentList.map(o => {
        return {
          ...o,
          base64: o && (o.DocId && lookup[o.DocId] ? (lookup[o.DocId] || {}).base64 : o.base64),
          mimeType: o && (o.DocId && lookup[o.DocId] ? (lookup[o.DocId] || {}).mimeType : o.mimeType),
          src: o && ('data:' + (o.DocId && lookup[o.DocId] ? (lookup[o.DocId] || {}).mimeType : o.mimeType) + ';base64,' + (o.DocId && lookup[o.DocId] ? (lookup[o.DocId] || {}).base64 : o.base64)),
        }
      }
      )
    return revisedList;
  }
  BindFileObject = (serverList, currentList) => {
    if (!currentList) return serverList || [];
    if (!serverList || (Array.isArray(serverList) && serverList.length === 0)) {
      return currentList || [];
    }
    serverList = Array.isArray(serverList) ? serverList : [serverList];
    currentList = Array.isArray(currentList) ? currentList : [currentList];
    const revisedList =
      currentList.map(
        (o, i) => o.base64 || o.isEmptyFileObject ? o : (serverList[i].base64 ? serverList[i] : o)
      )
    return revisedList || [];
  }

  BindReferenceField = (value, choices, { keyfieldname, valuefieldname, selectedObject } = {}) => {
    const x = (choices || []).filter(o =>
      o === value
      || o[keyfieldname || 'value' || 'key'] === value
      || o[keyfieldname || 'value' || 'key'] === (value || {})[keyfieldname || 'value' || 'key']
    )[0];
    return selectedObject || typeof x !== "object" ? (x || {}).obj : ((x || {}).obj || {})[valuefieldname];
  }

  /* end binding function */
  DelMst({ mstId }) {
    throw new TypeError(this + " Must implement DelMst");
  }
  DelDtl({ mstId, dtlId }) {
    throw new TypeError(this + " Must implement DelDtl");
  }
  SaveMst({ mst }) {
    throw new TypeError("Must implement SaveMst");
  }
  SaveDtl({ mst }) {
    throw new TypeError("Must implement SaveDtl");
  }
  CopyHdr({ mstId }) {
    throw new TypeError("Must implement CopyHdr");
  }
  CopyRow({ mstId, dtlId }) {
    throw new TypeError("Must implement CopyRow");
  }
  // New({mst})
  // {
  //   throw new TypeError("Must implement New");
  // }
  NewSaveMst({ mst }) {
    throw new TypeError("Must implement NewSaveMst");
  }
  NewSaveDtl({ mstId, dtl }) {
    throw new TypeError("Must implement NewSaveDtl");
  }
  SaveCloseMst({ naviBar, mst, redirectTo }) {
    throw new TypeError("Must implement SaveCloseMst");
  }
  SaveCloseDtl({ naviBar, mstId, dtl, redirectTo }) {
    throw new TypeError("Must implement SaveCloseDtl");
  }
  ExpMstTxt() {
    throw new TypeError("Must implement ExpMstTxt");
  }
  ExpMstRtf() {
    throw new TypeError("Must implement ExpMstTxt");
  }
  Print() {
    throw new TypeError("Must implement Print");
  }
  FormatSearchTitleL(v) {
    return v;
  }

  FormatSearchTitleR(v) {
    return v;
  }

  FormatSearchSubTitleL(v) {
    return v;
  }

  FormatSearchSubTitleR(v) {
    return v;
  }
};

const mapStateToProps = (state) => ({
  global: state.global,
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    { checkBundleUpdate: checkBundleUpdate },
  ), dispatch)
)

export default RintagiScreen
//export default withRouter(connect(mapStateToProps, mapDispatchToProps)(RintagiScreen));