import React, { Component } from 'react';
import log from '../../helpers/logger';
import classNames from 'classnames';
import { connect } from 'react-redux';
import fixOrientation from 'fix-orientation';
import { Row, Col } from 'reactstrap';


function calcSize(width, height, max_width, max_height, noSwap) {

  if (!max_height && !max_width) return {width,height};
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
    if (noSwap) { const x=max_width;max_width = max_height;max_width=x;}
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

class FileInputField extends Component {
  constructor(props) {
    super(props);


    this.state = {
      previewUrl: null,
      deleteBtnVisibility: true,
      storedImgSize: { width:0,height:0}
    }
    this.getImgSize = this.getImgSize.bind(this);
  }
  
  getImgSize(img, stateKey) {
    var imgTag = document.createElement("img");
    var existingImgSize =this.state.storedImgSize;
    imgTag.src =  img;
    imgTag.onload = () => {
      const {width,height} = imgTag;
      if (existingImgSize.width !== width || existingImgSize.height !== height) {
        this.setState({[stateKey]:{width:width,height:height}});
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

    let reader = new FileReader();
    let file = value.target.files[0] || { type: '' };
    var fileType = file['type'];

    reader.fileName = file.name;

    if (fileType.split('/')[0] === 'image') {

      const _this = this;

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

            var options = _this.props.options || {};
            var _width = img.width;
            var _height = img.height;

            var MAX_WIDTH = (options.MaxImageSize || {}).Width;
            var MAX_HEIGHT = (options.MaxImageSize || {}).Height;

            var { width, height } = calcSize(_width, _height, MAX_WIDTH, MAX_HEIGHT);

            canvas.width = width;
            canvas.height = height;

            ctx = canvas.getContext("2d");

            ctx.drawImage(img, 0, 0, width, height);

            var readerResizedImg = {
              result: canvas.toDataURL("image/jpeg")
            }

            _this.setState({
              file: file,
              previewUrl: readerResizedImg.result,
              fileName: reader.fileName,
              width: width,
              height: height,
            });

            // Passing name and base64 value of our rotated and resized image
            _this.props.onChange({ name: files[0].name, mimeType: files[0].type, size: files[0].size, width: width, height: height, lastModified: files[0].lastModified, base64: readerResizedImg });
          }
        })
      };
    }
    else {
      reader.onloadend = () => {
        this.setState({
          file: file,
          previewUrl: reader.result,
          fileName: reader.fileName
        });
        this.props.onChange({ name: files[0].name, mimeType: files[0].type, size: files[0].size, lastModified: files[0].lastModified, base64: reader });
        // this.props.onChange({ name: files[0].name, base64: reader });
      };
    }

    try {
      reader.readAsDataURL(file);
    }
    catch (e) {
      if (this.props.onError) {
        if (files[0] === undefined) { } else {
          this.props.onError(e, files[0].name);
        }
      }
    }
  };

  previewSelectedFile = () => {
    var win = window.open();
    win.document.write('<iframe src="' + this.state.previewUrl + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
  }

  previewServerFile = (base64ImgContent, mimeType) => {
    return () => {
      var win = window.open();
      var img = base64ImgContent;
      var wrappedImg = (img.startsWith('data') ? '' : "data:" + mimeType + ";base64,") + img;
      win.document.write('<iframe src="' + wrappedImg + '" frameborder="0" style="position: absolute; border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
    }
  }

  removeSelectedFile = () => {
    this.setState({
      file: null,
      previewUrl: null,
      deleteBtnVisibility: true,
    });

    const fileInput = document.querySelector('.fileInput');
    fileInput.value = '';

    this.props.onChange({ name: '', base64: '' });
  }

  sendEmptyFile = () => {
    var emptyObject = {
      result: "iVBORw0KGgoAAAANSUhEUgAAAhwAAAABCAQAAAA/IL+bAAAAFElEQVR42mN89p9hFIyCUTAKSAIABgMB58aXfLgAAAAASUVORK5CYII=",
    }

    this.props.onChange({ name: '', base64: emptyObject });
    this.setState({ deleteBtnVisibility: false });
  }

  render() {
    let previewClass = classNames({
      'd-none': true,
      'd-table-cell': this.state.previewUrl != null,
    });

    let deleteClass = classNames({
      'd-none': !this.state.deleteBtnVisibility,
      'd-table-cell': this.state.deleteBtnVisibility,
    });

    let previewIconClass = classNames({
      'd-none': true,
      'd-block': this.state.previewUrl !== null,
    })

    let deleteIconClass = classNames({
      'd-none': !this.state.deleteBtnVisibility,
      'd-block': this.state.deleteBtnVisibility,
    })

    const fileInfo = this.props.value || {};
    const mimeType = fileInfo.mimeType;
    const options = this.props.options || {};
    const inPlaceImg = (fileInfo.mimeType || '').startsWith('image/') || true;
    const fileContent = (fileInfo.base64 || '').startsWith('data:') ? '' : 'data:' + mimeType + ';base64,' + (fileInfo.base64 || '');

    let iconClass = classNames({
      'fa-file-image-o': mimeType === 'image/jpeg' || mimeType === 'image/gif',
      'fa-file-photo-o': mimeType === 'image/png',
      'fa-file-word-o': mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
      'fa-file-excel-o': mimeType === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'fa-file-pdf-o': mimeType === 'application/pdf',
      'fa-file-o': mimeType !== 'image/jpeg' || 'image/png' || 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' || 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || 'application/pdf',
    });

    if (fileInfo.base64) this.getImgSize(fileContent,'storedImgSize');

    var MAX_WIDTH = (options.MinImageSize || {}).Width;
    var MAX_HEIGHT = (options.MinImageSize || {}).Height;

    const storedIconImageSize = calcSize(this.state.storedImgSize.width, this.state.storedImgSize.height, MAX_WIDTH, MAX_HEIGHT);
    const uploadIconImageSize = calcSize(this.state.width, this.state.height, MAX_WIDTH, MAX_HEIGHT);

    return (
      <div className="wth-100">
        <Row>
          <Col className="mw-133 mb-15">
            <div className='form__form-group-input-wrap w-103'>
              <div className='form__form-group-file'>
                <label htmlFor={this.props.name}>{this.props.label}</label>
                <label className={`ml-15 ${previewClass}`} onClick={this.removeSelectedFile}>{options.CancelFileButton}</label>
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
              <div className={`form__form-group truncate pointer ${deleteIconClass}`} onClick={this.previewServerFile(fileContent, mimeType)} src={fileContent}>
                <i className={`fa ${iconClass} fs-38 fill-fintrux`}></i>
                <p>{fileInfo.fileName}</p>
              </div>
            }
            <div className={`form__form-group truncate pointer ${previewIconClass}`} src={this.state.previewUrl} onClick={this.previewSelectedFile}>
              <i className={`fa ${iconClass} fs-38 fill-fintrux`}></i>
              <p>{fileInfo.fileName}</p>
            </div>
          </Col>
          <Col>
            {
              (fileInfo.base64 || '').length > 0 && inPlaceImg && (mimeType === 'image/jpeg' || mimeType === 'image/png' || mimeType === 'image/gif') &&
              <img alt='' width={storedIconImageSize.width} height={storedIconImageSize.height} className={`img-upload pointer ${deleteIconClass}`} onClick={this.previewServerFile(fileContent, mimeType)} src={fileContent} />
            }
            {
              (mimeType === 'image/jpeg' || mimeType === 'image/png' || mimeType === 'image/gif') &&
              <img alt='' width={uploadIconImageSize.width || 200} height={uploadIconImageSize.height || 150} className={`img-upload pointer ${previewIconClass}`} src={this.state.previewUrl} onClick={this.previewSelectedFile} />
            }
          </Col>
        </Row>
      </div>
    )
  }
}

const mapStateToProps = (state) => ({
});

export default connect(mapStateToProps)(FileInputField);