import React, { Component } from 'react';
import log from '../../helpers/logger';
import classNames from 'classnames';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import fixOrientation from 'fix-orientation';
import { Row, Col, Spinner } from 'reactstrap';
import { showNotification } from '../../redux/Notification';
import Skeleton from 'react-skeleton-loader';
import { formatBytes, readUrl } from '../../helpers/formatter';
import { uuid } from '../../helpers/utils';
import { previewContent } from '../../helpers/domutils';
import moment from 'moment';

function calcSize(width, height, max_width, max_height, noSwap) {

  if (!max_height && !max_width) return { width, height };
  if (width > height) {
    if (max_height) {
      if (height > max_height) {
        width *= max_height / height;
        height = max_height;
      }
      else if (max_width) {
        if (width > max_width) {
          height *= max_width / width;
          width = max_width;
        }
      }
    }
    else if (max_width) {
      if (width > max_width) {
        height *= max_width / width;
        width = max_width;
      }
    }
  }
  else {
    if (noSwap) { const x = max_width; max_width = max_height; max_width = x; }
    if (max_height) {
      if (width > max_height) {
        height *= max_height / width;
        width = max_height;
      }
      else if (max_width) {
        if (height > max_width) {
          width *= max_width / height;
          height = max_width;
        }
      }
    }
    else {
      if (height > max_width) {
        width *= max_width / height;
        height = max_width;
      }
    }
  }

  return {
    width: width,
    height: height
  }
}

function stripEmbeddedBase64Prefix(base64string) {
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

function addPreviewUrl(o) {
  if (!o) {
    return {
      previewUrl: 'data:image/jpeg;base64,iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII='
    }
  }
  return {
    ...o,
    previewUrl: o.mimeType
      && o.mimeType.match(/image/)
      && (o.base64 || o.icon) ? 'data:' + o.mimeType + ';base64,' + (o.base64 || o.icon) : '',
    contentUrl: o.mimeType
      && (o.base64 || o.icon) ? 'data:' + o.mimeType + ';base64,' + (o.base64 || o.icon) : '',
  }
}

function getFile(file, index, options, success, state, setState) {
  let reader = new FileReader();
  var fileType = file['type'];
  var progress = false;

  // debugger;
  // log.debug(reader);

  reader.fileName = file.name;

  reader.onloadstart = () => {
    // log.debug('IM PROGRESSING!!!!');
    // success(index, {
    //   fileName: reader.fileName,
    //   width: '',
    //   height: '',
    //   mimeType: file.type,
    //   size: file.size,
    //   lastModified: file.lastModified,
    //   base64: stripEmbeddedBase64Prefix(readerResizedImg.result),
    //   progress: true,
    // });
    setState({ progress: true });
  }
  if (fileType.split('/')[0] === 'image') {

    // const _this = this;

    reader.onloadend = () => {
      // Handles autorotation first
      fixOrientation(reader.result, { image: true }, function (fixed) {

        var properlyRotatedImg = {
          result: fixed
        }

        var img = document.createElement("img");

        // Assiging rotated image to img canvas
        img.src = properlyRotatedImg.result;

        img.onload = () => {
          // Resizing image
          var canvas = document.createElement('canvas');
          var ctx = canvas.getContext("2d");

          ctx.drawImage(img, 0, 0);

          // var options = _this.props.options || {};
          // var width = img.width;
          // var height = img.height;

          var MAX_WIDTH = (options.MaxImageSize || {}).Width;
          var MAX_HEIGHT = (options.MaxImageSize || {}).Height;

          var { width, height } = calcSize(img.width, img.height, MAX_WIDTH, MAX_HEIGHT);

          canvas.width = width;
          canvas.height = height;

          // var ctx = canvas.getContext("2d");

          // This lines turns tranparent background on PNG image to white when transforming from PNG to JPEG at the end
          ctx.fillStyle = "#FFF";
          ctx.fillRect(0, 0, canvas.width, canvas.height);

          ctx.drawImage(img, 0, 0, width, height);

          var readerResizedImg = {
            result: canvas.toDataURL("image/jpeg")
          }

          success(index, {
            fileName: reader.fileName,
            width: width,
            height: height,
            mimeType: file.type,
            size: file.size,
            lastModified: file.lastModified,
            base64: stripEmbeddedBase64Prefix(readerResizedImg.result),
            progress: false,
          });

          setState({ progress: false });
          //log.debug('IM FINISHED');
        }
      })
    };
  }
  else {
    reader.onloadend = () => {
      success(index, {
        // file: file,
        fileName: reader.fileName,
        mimeType: file.type || (reader.fileName.includes('.xls') && 'application/vnd.ms-excel'),
        size: file.size,
        lastModified: file.lastModified,
        base64: stripEmbeddedBase64Prefix(reader.result),
        progress: false,
      });

      setState({ progress: false });
      // log.debug('IM FINISHED');
      // _this.setState({ progress: false });
      // this.props.onChange({ name: file.name, mimeType: file.type, size: file.size, lastModified: file.lastModified, base64: reader });
      // this.props.onChange({ name: files[0].name, base64: reader });
    };
  }

  try {
    reader.readAsDataURL(file);
  }
  catch (e) {
    // if (this.props.onError) {
    //   if (file === undefined) { } else {
    //     this.props.onError(e, file.name);
    //   }
    // }
  }
}

const MAX_FILE_SIZE = 10 * 1024 * 1024;
const ALLOWED_MIME_TYPE = ['image/png', 'image/jpeg', 'image/gif', 'pdf',
  'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
  'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
  'text/csv',
  'application/msword',
  'application/vnd.ms-excel'
];
const MAX_ALLOWED = 999999;

class FileInputField extends Component {
  constructor(props) {
    super(props);
    this.state = {
      files: props.files || [],
      deleteBtnVisibility: true,
      storedImgSize: { width: 0, height: 0 },
      progress: false,
    }

    this.getImgSize = this.getImgSize.bind(this);
    this.icon = this.icon.bind(this);
  }

  static getDerivedStateFromProps(nextProps, prevState) {
    if (!prevState.files || !prevState.files.length) return { files: nextProps.files };
    const revisedFiles = (nextProps.files || []).reduce((a, o) => { a[o.DocId || o.fileName] = o; return a; }, {});

    // log.debug(prevState);

    const x = [
      ...prevState.files.map(o => ({
        ...o
        , base64: o && (o.base64 || (revisedFiles[o.DocId || o.fileName] || {}).base64)
        , mimeType: o && (o.mimeType || (revisedFiles[o.DocId || o.fileName] || {}).mimeType)
      })),
    ];
    return {
      files: x
    }
  }

  componentDidUpdate(prevProps, prevStates) {
  }

  roundTrip = () => {
    return ((state) => {
      this.setState(state);
    }).bind(this);
  }

  getImgSize(img, stateKey) {
    var imgTag = document.createElement("img");
    var existingImgSize = this.state.storedImgSize;
    imgTag.src = img;
    imgTag.onload = () => {
      const { width, height } = imgTag;
      if (existingImgSize.width !== width || existingImgSize.height !== height) {
        this.setState({ [stateKey]: { width: width, height: height } });
      }
    };
  }

  handleChange = value => {
    // this is going to call setFieldValue and manually update values.this.props.name
    // this.props.onChange(this.props.name, value);
    this.setState({ deleteBtnVisibility: false });

    value.preventDefault();
    // convert files to an array
    const files = [...value.target.files];
    // log.debug(files);

    const _this = this;

    const fileCount = files.length;
    let fileList = [...(this.props.multiple ? _this.state.files.sort((a, b) => a.DocId - b.DocId) : [])];
    const existingCount = fileList.length;
    let x = 0;
    let maxErrorShown = false;
    const maxSize = (_this.props.options || {}).maxSize || MAX_FILE_SIZE;
    const allowedMime = (_this.props.options || {}).allowedMime || ALLOWED_MIME_TYPE;
    const fileTypeAllowed = (fileType) => allowedMime.filter(t => fileType.indexOf(t) >= 0).length > 0;
    const actionTimeStamp = Date.now();
    const newFiles = [];
    const maxAllowed = (_this.props.options || {}).maxFileCount || MAX_ALLOWED;
    const receiveFile = (i, file) => {
      x = x + 1;
      if (file.base64.length > maxSize * 4 / 3) {
        // alert('maximum allowed upload file size is ' + maxSize)
        this.props.showNotification("E", { message: 'Maximum allowed upload file size is ' + formatBytes(maxSize) })
      }
      else if (allowedMime && !fileTypeAllowed(file.mimeType)) {
        // alert('selected file type is not allowed')
        this.props.showNotification("E", { message: 'Selected file type is not allowed' })
      }
      else if (x + existingCount > maxAllowed) {
        // log.debug(x, existingCount, x + existingCount);
        // log.debug('MAX REACHED');
        if (!maxErrorShown) {
          this.props.showNotification("E", { message: 'Maximum allowed amount of files is ' + maxAllowed });
          maxErrorShown = true;
        }
        // return;
      }
      else {
        // log.debug(x, existingCount, file);
        const newFile = { ...file, ts: actionTimeStamp + 1 };
        fileList[existingCount + i] = newFile;
        newFiles.push(newFile);
        _this.setState({ files: fileList });

      }
      if (fileCount === x && typeof _this.props.onChange === "function") {
        // newFiles = newFile.filter(f => f);
        // log.debug(_this.state.files);
        _this.setState({ files: _this.state.files.filter(f => f && f.base64) });
        _this.props.onChange(fileList.filter(f => f), _this.props.name || (_this.props.field || {}).name, { fieldname: this.props.fieldname, listidx: _this.props.listidx, fieldpath: _this.props.fieldpath });
        if (typeof this.props.onAdd === "function" && this.props.multiple) {
          newFiles.forEach(o => {
            this.props.onAdd(o)
              .then(
                result => {

                }
              )
              .catch(
                error => {

                }
              )
          })
        }
      }

    }

    var filesArray = files.forEach((obj, i) => {
      getFile(obj, i, this.props.options, receiveFile, this.state, this.roundTrip());
    });
  };

  downloadFile = i => () => {
    const _this = this;
    const currentUrlTitle = document.getElementsByTagName("title")[0].innerHTML;
    const selectedFile = this.props.multiple ? this.state.files.sort((a, b) => a.DocId - b.DocId)[i] : this.state.files.sort((a, b) => a.DocId - b.DocId)[0];
    const envPublicUrl = process.env.PUBLIC_URL;
    const isIE = window.navigator && window.navigator.msSaveOrOpenBlob && false;
    const isImage = (/image/i).test((selectedFile || {}).mimeType);

    if (typeof this.props.onClick === "function" && !selectedFile.base64) {
      this.props.onClick(selectedFile);
    }
    else {
      log.debug('no content file selected document', selectedFile);
    };

  }
  previewSelectedFile = i => () => {
    // must open window during the click event and not in the promise, browser security prevent that from happening
    const _this = this;
    const currentUrlTitle = document.getElementsByTagName("title")[0].innerHTML;
    const selectedFile = this.props.multiple ? this.state.files.sort((a, b) => a.DocId - b.DocId)[i] : this.state.files.sort((a, b) => a.DocId - b.DocId)[0];
    const envPublicUrl = process.env.PUBLIC_URL;
    const isIE = window.navigator && window.navigator.msSaveOrOpenBlob && false;
    const isImage = (/image/i).test((selectedFile || {}).mimeType);
    const download = false;
    const previewSig = uuid();
    const dataObj = {
      ...selectedFile,
      title: currentUrlTitle + ' ' + ((selectedFile || {}).fileName || ''),
      fileSig: previewSig,
    };
    /* window.open MUST BE DONE here in the click even function or it would be blocked by popup blocker */
    // sessionStorage.setItem("PreviewAttachment", JSON.stringify({ ...dataObj, fileSig: previewSig }));
    const win = (!isIE || isImage) && !download && window.open(envPublicUrl + '/helper/showAttachment.html' + '?fileSig=' + previewSig, '_blank');
    previewContent({
      dataObj: dataObj
      , download: false
      , winObj: win
      , dataPromise: undefined
      , previewSig: previewSig
      , containerUrl: envPublicUrl + '/helper/showAttachment.html' + '?fileSig=' + previewSig
    });
    return;
    const makeDoc = (body) => {
      return "<html><header>" + "<title>" + currentUrlTitle + ' ' + (selectedFile.fileName || '') + "</title>"
        + "<header><body>" + body + "</body></html>"
    }
    const injectContent = function (file) {
      //debugger;
      const header = win.document.getElementById('header');
      const body = win.document.getElementsByTagName('body');
      const target = body[0] || body;
      const innerHTML = target.innerHTML;
      //const content = makeDoc('<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
      //const content = '<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>';
      const content = addPreviewUrl(selectedFile).contentUrl;
      //const content = 'https://www.youtube.com/embed/01ON04GCwKs'
      //const content = '<iframe width="560" height="315" src="https://www.youtube.com/embed/01ON04GCwKs" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>'
      win.postMessage(content, "*");
      // if (body[0])
      //   body[0].innerHTML = makeDoc('<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
      // else
      //   body.innerHTML = makeDoc('<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
    }
    if (typeof this.props.onClick === "function" && !selectedFile.base64) {

    }
    else {
      setTimeout(() => {
        injectContent(selectedFile);
      }, 500);
    };

    // win.onload = function(){

    //   //debugger;
    //   injectContent(selectedFile);
    //   // const header = win.document.getElementById('header');
    //   // const body = win.document.getElementsByTagName('body');
    //   // //header.innerHTML = "<title>" + currentUrlTitle + ' ' + (selectedFile.fileName || '') + "</title>";
    //   // if (body[0])
    //   //   body.innerHTML = makeDoc('<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
    //   // else
    //   //   body[0].innerHTML = makeDoc('<iframe src="' + addPreviewUrl(selectedFile).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
    // };
    // const currentUrlTitle = document.getElementsByTagName("title")[0].innerHTML;
    // const makeDoc = (body) => {
    //   return "<html><header>" + "<title>" + currentUrlTitle + ' ' + (selectedFile.fileName || '') + "</title>"
    //     + "<header><body>" + body + "</body></html>"
    // }
    // const preview = (file) => {
    //   win.document.write(makeDoc('<iframe src="' + addPreviewUrl(file).contentUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>'));
    // };

    // if (typeof this.props.onClick === "function" && !selectedFile.base64) {
    //   win.document.write(makeDoc('Loading... please wait'));
    //   Promise.resolve(this.props.onClick(this.state.files[i]))
    //     .then(
    //       result => {
    //         log.debug('file input return', result)
    //         //preview(result);
    //       }
    //     )
    //     .catch(
    //       error => {
    //         log.debug('error', error)
    //       }
    //     )
    // }
    // else {
    //   preview(selectedFile);
    // }
  }

  // previewServerFile = (base64ImgContent, mimeType) => {
  //   return () => {
  //     var win = window.open();
  //     var img = base64ImgContent;
  //     var wrappedImg = (img.startsWith('data') ? '' : "data:" + mimeType + ";base64,") + img;
  //     win.document.write('<iframe src="' + wrappedImg + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
  //   }
  // }

  removeSelectedFile = i => (event) => {
    // log.debug(event.target.value);
    // log.debug(i);
    // const sortedFiles = this.state.files.sort((a, b) => a.DocId - b.DocId);
    const file = this.state.files.sort((a, b) => a.DocId - b.DocId)[i];
    // log.debug(this.state.files)

    this.state.files.splice(i, 1);
    this.setState({
      // file: null,
      // previewUrl: null,
      // deleteBtnVisibility: true,
      files: [...this.state.files]
    });
    if ((file.DocId || !file.ts) && (typeof this.props.onDelete === "function")) {
      this.props.onDelete(file)
        .then(
          (result) => {
            //log.debug('remove file result', result)
          }
        )
        .catch(error => {
          log.debug('remove file from server error', error)
        })
    }

    const fileInput = document.getElementsByName(this.props.name || (this.props.field || {}).name)[0];
    // log.debug(fileInput);
    fileInput.value = '';
    // log.debug(fileInput);

    this.props.onChange(this.state.files, this.props.name || (this.props.field || {}).name, { fieldname: this.props.fieldname, listidx: this.props.listidx, fieldpath: this.props.fieldpath });
  }

  sendEmptyFile = () => {
    var emptyObject = {
      result: "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=",
    }

    this.props.onChange({ name: '', base64: emptyObject }, this.props.name || (this.props.field || {}).name, { fieldname: this.props.fieldname, listidx: this.props.listidx, fieldpath: this.props.fieldpath });
    this.setState({ deleteBtnVisibility: false });
  }

  icon = i => {
    const mimeType = (this.state.files.sort((a, b) => a.DocId - b.DocId)[i] || {}).mimeType;

    if (mimeType === 'image/png') {
      return 'fa-file-image-o';
    }
    if (mimeType === 'image/jpeg') {
      return 'fa-file-image-o';
    }
    if (mimeType === 'image/gif') {
      return 'fa-file-image-o';
    }
    if (mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
      return 'fa-file-word-o';
    }
    if (mimeType === 'application/msword') {
      return 'fa-file-word-o';
    }
    if (mimeType === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
      return 'fa-file-excel-o';
    }
    if (mimeType === 'text/csv') {
      return 'fa-file-excel-o';
    }
    if (mimeType === 'application/vnd.ms-excel') {
      return 'fa-file-excel-o';
    }
    if (mimeType === 'application/pdf') {
      return 'fa-file-pdf-o';
    }
    if (mimeType !== 'image/png' ||
      mimeType !== 'image/jpeg' ||
      mimeType !== 'image/gif' ||
      mimeType !== 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' ||
      mimeType !== 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' ||
      mimeType !== 'application/pdf' ||
      mimeType !== 'application/msword' ||
      mimeType !== 'text/csv' ||
      mimeType !== 'application/vnd.ms-excel') {
      return 'fa-file-o';
    }
  }

  render() {

    // let previewClass = classNames({
    //   'd-none': true,
    //   'd-block': this.state.files[0].previewUrl != null,
    // });

    // let deleteClass = classNames({
    //   'd-none': !this.state.deleteBtnVisibility,
    //   'd-block': this.state.deleteBtnVisibility,
    // });

    // let previewIconClass = classNames({
    //   'd-none': true,
    //   'd-block': this.state.files[0].previewUrl !== null,
    // })

    // let deleteIconClass = classNames({
    //   'd-none': !this.state.deleteBtnVisibility,
    //   'd-block': this.state.deleteBtnVisibility,
    // })

    // const fileInfo = this.props.value || {};
    const mimeType = ((this.state.files || [])[0] || {}).mimeType;
    const options = this.props.options || {};
    // const inPlaceImg = ((this.state.files[0] || {}).mimeType || '').startsWith('image/') || true;
    // const fileContent = ((this.state.files[0] || {}).base64 || '').startsWith('data:') ? '' : 'data:' + mimeType + ';base64,' + ((this.state.files[0] || {}).base64 || '');

    let iconClass = classNames({
      'fa-file-word-o': mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
      'fa-file-excel-o': mimeType === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'fa-file-pdf-o': mimeType === 'application/pdf',
      'fa-file-o': mimeType !== 'image/jpeg' || 'image/png' || 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' || 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || 'application/pdf',
    });

    // if ((this.state.files[0] || {}).base64) this.getImgSize(fileContent, 'storedImgSize');

    var MAX_WIDTH = (options.MinImageSize || {}).Width;
    var MAX_HEIGHT = (options.MinImageSize || {}).Height;

    const storedIconImageSize = calcSize(this.state.storedImgSize.width, this.state.storedImgSize.height, MAX_WIDTH, MAX_HEIGHT);
    const uploadIconImageSize = calcSize(this.state.width, this.state.height, MAX_WIDTH, MAX_HEIGHT);

    const getPreviewURL = (o) => {
      const abc = {
        ...o, previewUrl: o.previewUrl || ("data:" + o.mimeType + ";base64," + o.base64),
      }
      // log.debug(abc);
      return abc;
    }

    const maxAllowed = (this.props.options || {}).maxFileCount;
    const filesAmount = (this.state.files || {}).length;

    // log.debug(maxAllowed, filesAmount);

    return (
      <div className="wth-100">
        {/* <Row>
          <Col className="mb-15">
            <div className='form__form-group-input-wrap'>
              <div className='form__form-group-file'>
                <label htmlFor={this.props.name}><i className="fa fa-paperclip"></i> {this.props.label}</label>
                <label className={`mt-15 ${previewClass}`} onClick={this.removeSelectedFile}>{options.CancelFileButton}</label>
                {
                  (fileInfo.base64 || '').length > 0 && <label className={`ml-15 ${deleteClass}`} onClick={this.sendEmptyFile}>{options.DeleteFileButton}</label>
                }
                <input
                  className="fileInput"
                  type='file'
                  name={this.props.name}
                  id={this.props.name}
                  onChange={this.handleChange}
                />
              </div>
            </div>

          </Col>
          <Col className="mw-133">
            {
              (fileInfo.base64 || '').length > 0 && inPlaceImg &&
              <div className={`form__form-group mb-0 truncate pointer ${deleteIconClass}`} onClick={this.previewServerFile(fileContent, mimeType)} src={fileContent}>
                <p><i className={`fa ${iconClass} fs-38 color-green`}></i> {fileInfo.fileName}</p>
              </div>
            }
            <div className={`form__form-group mb-0 truncate pointer ${previewIconClass}`} src={this.state.previewUrl} onClick={this.previewSelectedFile}>
              <p><i className={`fa ${iconClass} mr-5 color-green`}></i> {fileInfo.fileName}</p>
            </div>
          </Col>
          <Col>
            {
              (fileInfo.base64 || '').length > 0 && inPlaceImg && (mimeType === 'image/jpeg' || mimeType === 'image/png' || mimeType === 'image/gif') &&
              <img width={storedIconImageSize.width} height={storedIconImageSize.height} className={`img-upload pointer ${deleteIconClass}`} onClick={this.previewServerFile(fileContent, mimeType)} src={fileContent} />
            }
            {
              (mimeType === 'image/jpeg' || mimeType === 'image/png' || mimeType === 'image/gif') &&
              <img width={uploadIconImageSize.width || 200} height={uploadIconImageSize.height || 150} className={`img-upload pointer ${previewIconClass}`} src={this.state.previewUrl} onClick={this.previewSelectedFile} />
            }
          </Col>
        </Row> */}

        <div className={`dropzone dropzone--multiple ${this.props.disabled && 'dropzone-disabled'}`}>
          <div className='dropzone__imgs-wrapper'>
            {(this.state.files || []).length > 0 &&
              this.state.files
                .filter(f => f && f.mimeType)
                .sort((a, b) => a.DocId - b.DocId)
                .map((obj, i) => {
                  return (
                    <div className="dropzone__holder" key={i}>
                      <div className={`dropzone__img pointer ${this.props.disabled && 'rad-4 mb-20'}`}
                        style={{ minWidth: "40px", minHeight: "40px", backgroundImage: 'url(' + addPreviewUrl(obj).previewUrl + ')' }}
                        onClick={(obj || {}).base64 || (obj || {}).icon ? this.previewSelectedFile(i) : this.downloadFile(i)} >
                        {(!(obj || {}).base64 && !(obj || {}).icon) && <Skeleton height="100px" widthRandomness="0" />}
                        {obj && (obj.mimeType || '').match(/image/) && (!obj.base64 && !obj.icon) &&
                          <i className={`fa ${this.icon(i)} fs-38 color-green fileUpload-icon`}></i>
                        }
                        {obj && !(obj.mimeType || '').match(/image/) &&
                          <i className={`fa ${this.icon(i)} fs-38 color-green fileUpload-icon`}></i>
                        }
                        {obj && obj.ts && <p className={`dropzone__img-name truncate-inline pb-17 ${this.props.disabled && 'bblr-4'}`}><u>{moment(obj.ts).format('MMM D, YYYY')}</u></p>}
                        {obj && obj.InputOn && <p className={`dropzone__img-name truncate-inline pb-17 ${this.props.disabled && 'bblr-4'}`}><u>{moment(obj.InputOn, moment.ISO_8601).format('MMM D, YYYY')}</u></p>}
                        <p className={`dropzone__img-name truncate-inline ${this.props.disabled && 'bblr-4'}`}>{obj && (((!obj.base64 && !obj.icon) ? 'Loading...' : obj.fileName))}</p>
                      </div>
                      {!this.props.disabled
                        && <button type='button' className='dropzone__img-delete-custom' onClick={this.removeSelectedFile(i)}>{'Remove'}</button>
                      }
                    </div>
                  )
                })
            }
            {(this.state.files || []).length < 1 && this.props.disabled &&
              <div className="fw-600 m-auto pt-20">No documents in this section</div>
            }
            {!this.props.disabled && <div className={`dropzone__holder ${filesAmount >= maxAllowed && 'd-none'}`}>
              <label className="dropzone__input-custom" htmlFor={this.props.name || (this.props.field || {}).name}>
                {!this.props.multiple && (this.state.files || []).length > 0 && <i className="fa fa-repeat"></i>}
                {!this.props.multiple && (this.state.files || []).length === 0 && (!this.state.progress
                  ? <i className="fa fa-plus"></i>
                  : <Spinner color="success" style={{ width: '2rem', height: '2rem', margin: 'auto' }} />
                )}
                {this.props.multiple && (!this.state.progress
                  ? <i className="fa fa-plus"></i>
                  : <Spinner color="success" style={{ width: '2rem', height: '2rem', margin: 'auto' }} />
                )}
              </label>
              <input
                className='fileInput no-fastclick'
                type='file'
                name={this.props.name || (this.props.field || {}).name}
                id={this.props.name || (this.props.field || {}).name}
                onChange={this.handleChange}
                multiple={this.props.multiple}
                disabled={filesAmount >= maxAllowed || this.props.disabled}
              />
            </div>}
          </div>
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
});

const mapDispatchToProps = (dispatch) => (
  bindActionCreators(Object.assign({},
    {
      showNotification: showNotification,
    },
  ), dispatch)
)

export default connect(mapStateToProps, mapDispatchToProps)(FileInputField);