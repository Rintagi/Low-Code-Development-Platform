import moment from 'moment';
import log from './logger';


// Determine user device or browser locale
// Return string locale (e.g: 'ru' or 'en-US' or 'de-DE');
const getNavigatorLanguage = () => {
  if (navigator.languages && navigator.languages.length) {
    return navigator.languages[0];
  } else {
    return navigator.language || navigator.browserLanguage || 'en';
  }
}

/* current selected language, default to browser default */
let currentLanguage = getNavigatorLanguage();
let cultureId = null;
// Assigning locale to moment.js at the top level
moment.locale(currentLanguage);

export function getCurrentLanguage() {
  return { lang: currentLanguage, cultureId: cultureId }
}
export function switchLanguage(lang, CultureId) {
  currentLanguage = lang || currentLanguage || "en";
  cultureId = CultureId;
  moment.locale(currentLanguage);
}

// Localized amount formatting for display only
export function toLocalAmountFormat(value) {
  try {
    return new Intl.NumberFormat(currentLanguage, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(value);
  } catch (e) {
    return value;
  }
}

// Localized amount formatting for input only
export function toInputLocalAmountFormat(value) {
  try {
    return new Intl.NumberFormat(currentLanguage, { minimumFractionDigits: 2, maximumFractionDigits: 2, useGrouping: false }).format(value);
  } catch (e) {
    return value;
  }
}

// Localized date formatting
export function toLocalDateFormat(value) {
  try {
    var x = value.replace(/\./g, '-');
    var localDate = moment(x).format('L');

    if (localDate === 'Invalid date') {
      localDate = moment(value).format('L');
    }

    // Replaces slash (/) and dash (-) with a dot (.) in date format
    var formattedDate = localDate.replace(/\/|-/g, '.');
    return formattedDate;
  } catch (e) {
    return value;
  }
}

export function toMoney(value) {
  try {
    return parseFloat(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
  } catch (e) {
    return value;
  }
}

export function toMoneyWithoutDecimal(value) {
  try {
    return parseFloat(value).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  } catch (e) {
    return value;
  }
}

export function toDate(value) {
  try {
    // return moment(value).format("L");
    // return moment(value).format("LL");
    return moment(value).format("YYYY.MM.DD");
    // Full list of date/time formats is here: http://momentjs.com/docs/#/displaying/format/
  } catch (e) {
    return value;
  }
}

export function strFormat(str, ...args) {
  const x = (str || "").replace(/{(\d*)}/g, function (s, i, pos, ...captures) {
    const replacement = args[i];
    return replacement !== undefined ? replacement : s;
  });
  return x;
}

export function cutHash(value) {
  return value.split("#")[1];
}

export function toLower(value) {
  try {
      return value.toLowerCase();
  } catch (e) {
      return value;
  }
}

export function hideAccount(value) {
  try {
    var result = value.replace(/^(\+?[\d]{0})\d+(\d{2})$/g, "$1***********$2");
    return result;
    // Full list of date/time formats is here: http://momentjs.com/docs/#/displaying/format/
  } catch (e) {
    return value;
  }
}

export function hideCreditCard(value) {
  try {
    var result = value.replace(/^(\+?[\d]{0})\d+(\d{4})$/g, "$2");
    return result;
    // Full list of date/time formats is here: http://momentjs.com/docs/#/displaying/format/
  } catch (e) {
    return value;
  }
}

export function valueToNumber(value, defaultIfFailed) {
  try {
    var result = parseFloat(value.replace(/,/g, '.'));
    return isNaN(result) && defaultIfFailed !== undefined && defaultIfFailed !== null ? defaultIfFailed : result;
  } catch (e) {
    return defaultIfFailed || value;
  }
}

export function isNumber(value) {
  try {
    const regex = new RegExp(/^-?(?:\d+|\d{1,3}(?:\d{3})+)(?:(\.|,)\d+)?$/);
    var result = value.match(regex);
    return result;
  } catch (e) {
    return true;
  }
}

export function sliceK(value) {
  try {
    var result = value.toString().slice(0, -3);
    return result;
  } catch (e) {
    return true;
  }
}

export function sliceFirstLetter(value) {
  try {
    var result = value.toString().slice(0, 1);
    return result;
  } catch (e) {
    return true;
  }
}

export function removeSpaces(value) {
  try {
    var result = value.toString().replace(/\s/g, '');
    return result;
  } catch (e) {
    return true;
  }
}

export function takeAfterDash(value) {
  try {
    var result = value.toString().split('-')[1];
    return result;
  } catch (e) {
    return true;
  }
}

export function readUrl(value) {
  if (value.includes('http://') || value.includes('https://')) {
    return value;
  }
  else {
    return `http://${value}`;
  }
}

export function cutProtocol(value) {
  if (value.includes('http://') || value.includes('https://')) {
    return value.split("://")[1];
  }
  else {
    return value;
  }
}

export function getUrlBeforeRouter() {
  const routerStart = window.location.href.search("/#/");
  const beforeRouter = window.location.href.slice(0, routerStart);
  return beforeRouter;
}


export function _getQueryString(location, name) {
  const url = (location || window.location).href;

  name = name.replace(/[\[\]]/g, '\\$&');
  const regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
    results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return '';
  return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

export function getQueryString(location, name) {
  try {
    const query = new URLSearchParams(location.search);
    return query.get(name);
  }
  catch (e) {
    return _getQueryString(location, name);
  }
}


export function toCapital(value) {
  try {
    // var result = value.toLowerCase().replace(/\b[a-z]/g, function(letter) {
    //   return letter.toUpperCase();
    // });

    var uppercase = value.toUpperCase();
    if (value.toLowerCase() === uppercase) {
      return value;
    }
    else {
      if (value === uppercase) {
        var firstLtr = 0;
        for (var i = 0; i < value.length; i++) {
          if (i == 0 && /[a-zA-Z]/.test(value.charAt(i))) firstLtr = 2;
          if (firstLtr == 0 && /[a-zA-Z]/.test(value.charAt(i))) firstLtr = 2;
          if (firstLtr == 1 && /[^a-zA-Z]/.test(value.charAt(i))) {
            if (value.charAt(i) == "'") {
              if (i + 2 == value.length && /[a-zA-Z]/.test(value.charAt(i + 1))) firstLtr = 3;
              else if (i + 2 < value.length && /[^a-zA-Z]/.test(value.charAt(i + 2))) firstLtr = 3;
            }
            if (firstLtr == 3) firstLtr = 1;
            else firstLtr = 0;
          }
          if (firstLtr == 2) {
            firstLtr = 1;
            value = value.substr(0, i) + value.charAt(i).toUpperCase() + value.substr(i + 1);
          }
          else {
            value = value.substr(0, i) + value.charAt(i).toLowerCase() + value.substr(i + 1);
          }
        }
        return value;
      }
      else {
        return value;
      }
    }
  } catch (e) {
    return true;
  }
}

export function formatBytes(bytes, decimals = 2) {
  if (bytes === 0) return '0 Bytes';

  const k = 1024;
  const dm = decimals < 0 ? 0 : decimals;
  const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

  const i = Math.floor(Math.log(bytes) / Math.log(k));

  return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

export function formatContent(value, format) {
  if(format === 'Money' || format === 'Currency'){
    try {
      if(value.length > 0){
        return parseFloat(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
      }else{
        return value;
      }
    } catch (e) {
      return value;
    }
  }else{
    return value;
  }
}