import React, { Component } from 'react';
import { getAddMstPath, getAddDtlPath, getNaviPath, getEditDtlPath, getEditMstPath } from '../../helpers/utils'
import { GetDropdownAction, GetBottomAction, GetRowAction } from '../../redux/_ScreenReducer'
import log from '../../helpers/logger';

export default class RintagiScreen extends Component {
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

  static ShowSpinner(state,src) {
    return !state || (src==="MstList" && state.searchlist_loading) || (!src && (state.page_loading || state.page_saving));
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
      log.debug('show more');
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
      handleSubmit();
    }.bind(this);
  }

  SearchFilterTextValueChange(handleSubmit, setFieldValue, type, controlName) {
    return function (evt) {
       const value = evt.target.value;
        setFieldValue(controlName, value);
        handleSubmit();
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

  /* screen action handlers for different input type assume formik is used */
  FieldChange(setFieldValue, setFieldTouched, name, blur) {
    const _this = this;
    return function (name, value) {
      if (blur) setFieldTouched(name, true);
      else setFieldValue(name, value[0]);
    }
  }

  DateChange(setFieldValue, setFieldTouched, name, blur) {
    const _this = this;
    return function (value) {
      if (blur) setFieldTouched(name, true);
      else setFieldValue(name, (value || {})._d || "");
    }
  }

  DropdownChange(setFieldValue, setFieldTouched, name) {
    const _this = this;
    return function (name, value) {
      setFieldTouched(name, true);
      setFieldValue(name, value);
    }
  }


  FileUploadChange(setFieldValue, name) {
    const _this = this;
    return function (value) {
      const file = value.base64.result ? {
        fileName: value.name,
        mimeType: value.mimeType,
        lastModified: value.lastModified,
        base64: value.base64.result,
      } : null;
      _this.setState({ filename: (file || {}).fileName });
      setFieldValue(name, file);
    }
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
      this.props.AddMst(null, "MstRecord", 0);
      this.props.history.push(getAddMstPath(getNaviPath(naviBar, "MstRecord", "/")))
      evt.preventDefault();
    }.bind(this);
  };
  AddNewDtl({ naviBar, mstId }) {
    return function (evt) {
      this.props.AddDtl(mstId, null, -1);
      this.props.history.push(getAddDtlPath(getNaviPath(naviBar, "DtlRecord", "/")));
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

  GetTargetId() {
    const { mstId, dtlId } = { ...this.props.match.params };
    return {
      targetMstId: mstId,
      targetDtlId: dtlId,
    }
  }

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

  FormatSearchTitleL(v){
    return v;
  }

  FormatSearchTitleR(v){
    return v;
  }

  FormatSearchSubTitleL(v){
    return v;
  }

  FormatSearchSubTitleR(v){
    return v;
  }
};


