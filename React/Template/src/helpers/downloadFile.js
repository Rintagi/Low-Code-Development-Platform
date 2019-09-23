// 'downloadFile.js', written by blending two solutions:
// 'js-download' https://github.com/kennethjiang/js-file-download
// 'Anders Paulsen' https://blog.jayway.com/2017/07/13/open-pdf-downloaded-api-javascript/

//https://gist.github.com/dreamyguy/6b4ab77d2f118adb8a63c4a03fba349d

export function downloadFile(data, filename, mime) {
    // It is necessary to create a new blob object with mime-type explicitly set
    // otherwise only Chrome works like it should
    const blob = new Blob([data], {type: mime || 'application/octet-stream'});
    if (typeof window.navigator.msSaveBlob !== 'undefined') {
      // IE doesn't allow using a blob object directly as link href.
      // Workaround for "HTML7007: One or more blob URLs were
      // revoked by closing the blob for which they were created.
      // These URLs will no longer resolve as the data backing
      // the URL has been freed."
      window.navigator.msSaveBlob(blob, filename);
      return;
    }
    // Other browsers
    // Create a link pointing to the ObjectURL containing the blob
    const blobURL = window.URL.createObjectURL(blob);
    const tempLink = document.createElement('a');
    tempLink.style.display = 'none';
    tempLink.href = blobURL;
    tempLink.setAttribute('download', filename);
    // Safari thinks _blank anchor are pop ups. We only want to set _blank
    // target if the browser does not support the HTML5 download attribute.
    // This allows you to download files in desktop safari if pop up blocking
    // is enabled.
    if (typeof tempLink.download === 'undefined') {
      tempLink.setAttribute('target', '_blank');
    }
    document.body.appendChild(tempLink);
    tempLink.click();
    document.body.removeChild(tempLink);
    setTimeout(() => {
      // For Firefox it is necessary to delay revoking the ObjectURL
      window.URL.revokeObjectURL(blobURL);
    }, 100);
  }
  
  // To use:
  // import {downloadFile} from '../helpers/downloadFile'; // <= HERE
  // export function fetchReport(token, sessionId) {
  //   return dispatch => {
  //     dispatch({type: 'FETCH_REPORT'});
  //     axios.get(`${API_ROOT}/report/${sessionId}`, {
  //       headers: {
  //         Authorization: `Custom ${token}`
  //       }
  //     })
  //       .then(response => {
  //         dispatch({type: 'FETCH_REPORT_FULFILLED', payload: response.data});
  //         downloadFile(response.data, 'ActionReport.csv', 'text/csv'); // <= HERE
  //       })
  //       .catch(err => {
  //         dispatch({type: 'FETCH_REPORT_REJECTED', payload: err});
  //       });
  //   };
  // }
  // Then:
  // dispatch(fetchReport(userToken, SessionId)); // at component level
  
  // ---------------- sources ---------------- //
  
  // // https://github.com/kennethjiang/js-file-download
  // module.exports = function(data, filename, mime) {
  //   var blob = new Blob([data], {type: mime || 'application/octet-stream'});
  //   if (typeof window.navigator.msSaveBlob !== 'undefined') {
  //       // IE workaround for "HTML7007: One or more blob URLs were
  //       // revoked by closing the blob for which they were created.
  //       // These URLs will no longer resolve as the data backing
  //       // the URL has been freed."
  //       window.navigator.msSaveBlob(blob, filename);
  //   }
  //   else {
  //       var blobURL = window.URL.createObjectURL(blob);
  //       var tempLink = document.createElement('a');
  //       tempLink.style.display = 'none';
  //       tempLink.href = blobURL;
  //       tempLink.setAttribute('download', filename);
  
  //       // Safari thinks _blank anchor are pop ups. We only want to set _blank
  //       // target if the browser does not support the HTML5 download attribute.
  //       // This allows you to download files in desktop safari if pop up blocking
  //       // is enabled.
  //       if (typeof tempLink.download === 'undefined') {
  //           tempLink.setAttribute('target', '_blank');
  //       }
  
  //       document.body.appendChild(tempLink);
  //       tempLink.click();
  //       document.body.removeChild(tempLink);
  //       window.URL.revokeObjectURL(blobURL);
  //   }
  // }
  
  // // https://blog.jayway.com/2017/07/13/open-pdf-downloaded-api-javascript/
  // function showFile(blob) {
  //   // It is necessary to create a new blob object with mime-type explicitly set
  //   // otherwise only Chrome works like it should
  //   var newBlob = new Blob([blob], {type: "application/pdf"})
  //   // IE doesn't allow using a blob object directly as link href
  //   // instead it is necessary to use msSaveOrOpenBlob
  //   if (window.navigator && window.navigator.msSaveOrOpenBlob) {
  //     window.navigator.msSaveOrOpenBlob(newBlob);
  //     return;
  //   }
  //   // For other browsers:
  //   // Create a link pointing to the ObjectURL containing the blob.
  //   const data = window.URL.createObjectURL(newBlob);
  //   var link = document.createElement('a');
  //   link.href = data;
  //   link.download="file.pdf";
  //   link.click();
  //   setTimeout(() => {
  //     // For Firefox it is necessary to delay revoking the ObjectURL
  //     window.URL.revokeObjectURL(data);
  //   }, 100);
  // };
  // fetch([url to fetch], {[options setting custom http-headers]})
  // .then(r => r.blob())
  // .then(showFile)