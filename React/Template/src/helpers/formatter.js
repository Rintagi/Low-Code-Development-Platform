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