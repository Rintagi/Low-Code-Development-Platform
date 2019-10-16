// Stock rules only. Written over by robot on each deployment.
var today = currentDate().split("/");
var openDatePickerId = null;

function currentDate() 
{
    var dd = new Date();
    return dd.getFullYear() + "." + normalizeDate(dd.getMonth() + 1) + "." + normalizeDate(dd.getDate());
}

function removeLeadingZeroes(numstr)
{
    if (numstr === "") return "0";
    var str = numstr.toString();
    var ii;
    for (ii = 0; ii < str.length && str.charAt(ii) === "0"; ii++);
    return (ii === str.length) ? "0" : str.substring(ii);
}

function normalizeDatestr(dds)
{
    var ddp = dds.split("/");
    var ln = ddp.length;
    var rst = "";

    if (ddp[ln - 1] === "") ln = ln - 1;
    if (ln >= 1)
	{
        rst = rst + normalizeDate(ddp[0]);
        if (ln >= 2)
		{
            rst = rst + "/" + normalizeDate(ddp[1]);
            if (ln >= 3) rst = rst + "/" + normalizeYear(ddp[2]); else rst = rst + "/" + today[2];
        }
        else rst = rst + "/" + today[1] + "/" + today[2];
    }
    return rst;
}

function normalizeDate(numstr) {

    var num = removeLeadingZeroes(numstr);
    if (num < 10) num = "0" + num;
    return num;
}

function normalizeYear(numstr) {

    var num = parseInt(removeLeadingZeroes(numstr));
    if (num < 1000) num = num + 2000;
    return num;
}

function ValidateDatestr(dds, err)
{
    var ddp = dds.split("/");
    if (ddp.length !== 3) return 1; // bad date format(?)
    err.Month = ddp[0];
    err.Date = ddp[1];
    if (ddp[0] >= 1 && ddp[0] <= 12)
        if (ddp[1] >= 1 && ddp[1] <= 31) return CheckMonthAndDate(ddp[0], ddp[1], ddp[2]); else return 3; // bad general date
    else
        return 2; // bad month
}

function CheckMonthAndDate(month, date, year)
{
    if (month === 4 || month === 6 || month === 9 || month === 11)
        if (date > 30) return 4; // bad date for month
    if (month === 2)
    {
        if (date > 29) return 4;
        if (date === 29) return (year % 4 === 0) ? ((year % 400 === 0) ? 0 : (year % 100 === 0) ? 4 : 0) : 4;
    }
    return 0;
}

function checkElementDateAndAlert(element)
{
    if (element.value === "") return true;
	if (document.selection) // IE
	{
		//  Directly assign to element.value would eliminate postback right away.
		sel = document.selection.createRange();
		sel.moveStart('textedit',-1);
		sel.moveEnd('textedit',1);
		sel.text = normalizeDatestr(sel.text);
	}
	else    // Other browsers
	{
		element.value = normalizeDatestr(element.value);
	}
    var dateErrorObject = new Object();
    var rc = ValidateDatestr(element.value, dateErrorObject);
    if (rc === 0) return true;
    var currentBackgroundColor = element.style.backgroundColor;
    var currentForegroundColor = element.style.color;
    element.style.backgroundColor = "red";
    element.style.color = "white";
    var errstr = "";
    switch (rc) 
    {
        case 1:
        errstr = "Invalid date format. Please enter e.g. " + currentDate() + ".";
        break;
        case 2:
        errstr = "Invalid month value (" + dateErrorObject.Month + "). Valid values are 01-12.";
        break;
        case 3:
        errstr = "Invalid day of month value (" + dateErrorObject.Date + "). Valid values are 01-31.";
        break;
        case 4:
        errstr = "Invalid date (" + dateErrorObject.Date + ") for month specified (" + dateErrorObject.Month + ").";
        break;
    }
    alert("Error: " + errstr + ". Please re-enter date.");
    element.style.backgroundColor = currentBackgroundColor;
    element.style.color = currentForegroundColor;
    element.select();
    element.focus();
    return false;
}

function checkElementPhnNumAndAlert(element)
{
    if (element.value === "") return true;
    if (element.value.match("^\\(\\d{3}\\) \\d{3}-\\d{4}[^\n]*$")) return true;
    var currentBackgroundColor = element.style.backgroundColor;
    var currentForegroundColor = element.style.color;
    element.style.backgroundColor = "red";
    element.style.color = "white";
    alert("Error: Invalid phone number " + element.value + ". Please try again.");
    element.style.backgroundColor = currentBackgroundColor;
    element.style.color = currentForegroundColor;
    element.select();
    element.focus();
    return false;
}

function isAllowableEditKey(key)
{
    return (key === 8)			// backspace
        || (key === 35)		// end
        || (key === 36)		// home
        || (key === 37)		// left-arrow
        || (key === 39)		// right-arrow
        || (key === 46)		// delete
        || (key === 38)		// up-arrow
        || (key === 40)		// down-arrow
        || (key === 33)		// page-up
        || (key === 34)		// page-down
        || (key === 27)		// Esc
        ;
}

// All public functions start here ...

// Confirm with user on page-dirty:
function fConfirm(Pid,Cid,aNam,aVal)
{
	var pp = document.getElementById(Pid);
	var cc = document.getElementById(Cid);
	var nm = document.getElementById(aNam);
	var va = document.getElementById(aVal);
	if (pp !== null && cc !== null && nm !== null && va !== null && pp.value === "Y" && cc.value === "Y")
	{
		var ctr = null;
		var nms = document.getElementById(aNam).value.split(',');
		var vas = document.getElementById(aVal).value.split(',');
		for (ii = 0; ii < nms.length; ii++)
		{
//			ctr = document.getElementById(nms[ii]);
//			if (ctr !== null) {ctr.value = vas[ii];}
            var ee = $('#' + nms[ii]).val(vas[ii]);
            try { if (ee[0].inACPostBack) ee.change(); }
            catch (er) {/**/}

        }
		cc.value = "N";
		setTimeout("document.getElementById('" + Cid + "').value='Y';", 500);
		var confirmMsg = 'Any changes will be lost if you proceed.';
		try {
            confirmMsg = CurrConfirmMsg || confirmMsg;
            CurrConfirmMsg = null;
		} catch (ee) {/**/}
		return confirmMsg;
	}
}

function fConfirm2(Pid,Cid,aNam,aVal)
{
	var pp = document.getElementById(Pid);
	var cc = document.getElementById(Cid);
	var nm = document.getElementById(aNam);
	var va = document.getElementById(aVal);
	var x = true;
	if (pp !== null && cc !== null && nm !== null && va !== null && pp.value === "Y" && cc.value === "Y")
	{
        var eventTarget = $('#__EVENTTARGET').val();
        var confirmMsg = 'Any changes will be lost if you proceed.';
        try {
            confirmMsg = CurrConfirmMsg || confirmMsg; 
            CurrConfirmMsg = null;
        } catch (ee) {/**/}
        x = confirm(confirmMsg);
		var ctr = null;
		var nms = document.getElementById(aNam).value.split(',');
		var vas = document.getElementById(aVal).value.split(',');
		for (ii = 0; ii < nms.length; ii++)
		{
            //			ctr = document.getElementById(nms[ii]);
            //			if (ctr !== null) {ctr.value = vas[ii];}
            // only restore value when the user cancel something
            // as otherwise the change can trigger another round of postback
            // in the case for autopostback field(that is the case in firefox)
            // 2012.6.13 gary
            if (nms[ii] === eventTarget && !x) {
                var ee = $('#' + nms[ii]).val(vas[ii]);
                try { if (ee[0].inACPostBack) { ee.change(); } }
                catch (er) {/**/}
                try { ee[0].inACPostBack = null; } catch (er) {/**/}
            }
        }
		if (x) {cc.value = "N";}
	}
    CurrConfirmMsg = null;
    return x;
}

// Add new item to DropDown, ListBox, etc. (bypass a problem in IE)
function AddOption(ddl, val)
{
	var cc = document.forms[0].elements[ddl];
	cc[cc.length] = new Option('', val, true);
	return true;
}

// stop a click event and prevent it from bubbling; should be used in the onclick/onchange event like hyperpopup 
function stopEvent(e, evt) {
    try {
        evt = evt || event || window.event || this.event;
        evt.cancelBubble = true; evt.returnValue = false;
        if (evt.stopPropagation) evt.stopPropagation();
        if (evt.preventDefault) evt.preventDefault();
    } catch (er) {/**/}
    return false;
}
// Cause next row in DataGrid to be in Edit Mode:
function fFocusedEdit(btnId, ctrlId, evt) {
    if (evt.type !== 'blur') {
        try {
            var src = evt.srcElement || evt.target;
            if (src.tagName === "A" || src.tagName === "BUTTON" || (src.tagName === "INPUT" && src.type === "submit")) return;
        } catch (er) {/**/}
        try { if (colNam === 'DeleteLink') { colNam = null; return; } } catch (e) {/**/}
    }
    try { ctrlId = colNam || ctrlId; colNam = null; } catch (e) {/**/}
    if (typeof(Page_ClientValidate) === "function" && $('input.GrdTxt,select.GrdDdl,input.autocomplete-box-input').length > 0) {
        if (Page_ClientValidate('')) __doPostBack(btnId, ctrlId);
        else Page_BlockSubmit = false;
    }
    else __doPostBack(btnId, ctrlId);
    try {
        var e = evt || event || window.event || this.event;
        e.cancelBubble = true; e.returnValue = false;
        e.stopPropagation();
        e.preventDefault();
    } catch (er) {/**/}
}

// Wait given number of milli-seconds to get around a response issue on datagrid:
function fWait(ms)
{
	var dp = new Date();
	while (dp) {dn = new Date(); if (dn - dp > ms) {break;}}
}

//For onKeyDown use only: Focus on (ctrl) upon Enter key pressed.
function AeFocusCtrl(ee, ctrl)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		if (key === 13) 
		{
            if (document.getElementById(ctrl)) {document.getElementById(ctrl).focus();}
		}
	}
}
//For onKeyDown use only: MMDDYY date mask with postback, if any.
function CrMMDDYY(ee)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		var sel;
		if (document.selection) {sel = document.selection.createRange();}
		var curlen = oElement.value.length;
		if (key >= 96 && key <= 105) {key = key - 48;}  // 0 - 9
        var ch = String.fromCharCode(key);
        if ((ch >= "0" && ch <= "9") && !ee.shiftKey)
        {
			if (document.selection) // IE
			{
                if (document.selection.type === 'Text') document.selection.clear();
                if (curlen < 10) sel.text = ch;
                if (curlen === 1 || curlen === 4) oElement.value += "/";
                window.event.returnValue = false;
			}
			else    // Other browsers
			{	
				var startpos = oElement.selectionStart;
				var endpos = oElement.selectionEnd;	
				if (curlen < 10)
				{
					oElement.value = oElement.value.substring(0, startpos) + ch + oElement.value.substring(endpos, oElement.value.length);
					oElement.selectionEnd = startpos + 1; oElement.selectionStart = startpos + 1;
				}
				else if (startpos !== 10)
				{
					//we have reached 10 characters and should be able to replace the existing date
					oElement.value = oElement.value.substring(0, startpos) + ch;
				}
				if (curlen === 1 || curlen === 4) oElement.value += "/";
				return false;
			}
        }
		else if (key === 9 || ee.ctrlKey)  // tab and enter, etc. keys
		{
            if (window.event) {window.event.returnValue = checkElementDateAndAlert(oElement);} else {return checkElementDateAndAlert(oElement);}
		}
		else {return isAllowableEditKey(key);}
	}
}

//For onKeyDown use only: (999)999-9999 phone number mask.
function CrNAPhone(ee)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
        var str;
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		var sel;
		if (document.selection) {sel = document.selection.createRange();}
		var curlen = oElement.value.length;
		if (key >= 96 && key <= 105) {key = key - 48;}  // 0 - 9
		var ch = String.fromCharCode(key);
		if (oElement.value.length > 13) // Allow anything after 13 characters.
		{
			if (document.selection) // IE
			{
				sel.moveStart('textedit',-1);
				var selInd = sel.text.length;
				str = oElement.value.substring(0, selInd) + ch + oElement.value.substring(selInd, oElement.value.length - 1);
				if (!str.match("^\\(\\d{3}\\) \\d{3}-\\d{4}[^\n]*$"))
				{
					window.event.returnValue = (key === 9 || ee.ctrlKey) ? checkElementPhnNumAndAlert(oElement) : isAllowableEditKey(key);
				}
			}
			else    // Other browsers
			{	
				str = oElement.value.substring(0, oElement.selectionStart) + ch + oElement.value.substring(oElement.selectionEnd, oElement.value.length-1);
				if (!str.match("^\\(\\d{3}\\) \\d{3}-\\d{4}[^\n]*$"))
				{
                    return (key === 9 || ee.ctrlKey) ? checkElementPhnNumAndAlert(oElement) : isAllowableEditKey(key);
				}
			}
		}
		else
		{
			if (document.selection)
			{
				if ((ch >= "0" && ch <= "9") && !window.event.shiftKey)
				{
					if (document.selection.type === 'Text') {document.selection.clear();}
					if (curlen === 0) {sel.text = "(" + ch ;}
					else if (curlen === 3) {sel.text = ch + ") ";}
					else if (curlen === 8) {sel.text = ch + "-";}
					else {sel.text = ch;}
					window.event.returnValue = false; 
				}
				else
				{
					window.event.returnValue = (key === 9 || ee.ctrlKey) ? checkElementPhnNumAndAlert(oElement) : isAllowableEditKey(key);
				}
			}
			else
			{
				if ((ch >= "0" && ch <= "9") && !ee.shiftKey)
				{
					if (curlen === 0) {oElement.value = "(" + ch;}
					else if (curlen === 3) {oElement.value = oElement.value + ch + ") ";}
					else if (curlen === 8) {oElement.value = oElement.value + ch + "-";}
					else {oElement.value += ch;}
					return false;
				}
				else
				{
					return (key === 9 || ee.ctrlKey) ? checkElementPhnNumAndAlert(oElement) : isAllowableEditKey(key);
				}
			}
		}
	}
}

// For onKeypress only. Decimal mask.
// Parameters:  ee - always "event" to capture window's event;
//              nn - number of decimal places after one period; 0 means unlimited;
//              mok - if true then minus is allowed;
function CrDeciDot(ee, nn, mok)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		var sel;
		var str;
		if (document.selection) {sel = document.selection.createRange();}
		if (key === 45 && mok)	// minus sign allowed
		{
			if (document.selection) //	IE
			{
				if (oElement.value.lastIndexOf("-") >= 0 && sel.text.length === 0) {window.event.returnValue = false;}	//field already in focus
				else if (sel.text.length > 0) {window.event.returnValue = true;}	//text field is in focus and selected
				else
				{
					str = oElement.value.replace(/[-]/gi,"");
					oElement.value = "-"  + str;
					window.event.returnValue = false;	
				}
			}
			else	// Other browsers
			{
				if ((oElement.value.lastIndexOf("-") >= 0) && (oElement.selectionEnd === oElement.selectionStart)) {return false;}
				else if (oElement.selectionEnd !== oElement.selectionStart) {return true;}	//some sort of selections
				else
				{
					str = oElement.value.replace(/[-]/gi,"");
					oElement.value = "-" + str;
					return false;
				}
			}
		}		
		else if (key === 46)	// period
		{
			if (document.selection)
			{
				if (sel.text.length > 0) {window.event.returnValue = true;}
				else if (oElement.value.indexOf('.') > -1) {window.event.returnValue = false;}
				else
				{
					sel.moveEnd('textedit',1);
					sel.text = '.' + sel.text.substr(0,nn);
					window.event.returnValue = false;
				}
			}
			else if (oElement.value.indexOf('.') > -1)
			{
				var selection = oElement.value.substring(oElement.selectionStart,oElement.selectionEnd);
                if (selection.indexOf('.') > -1) { return true; } else { return false;} 
			}
			else {return true;}
		}
		else if (key > 47 && key < 58)	// a numeric digit
		{
			if (document.selection)
			{
				sel2 = sel.duplicate();
				if (sel.text.length > 0) {window.event.returnValue = true;}
				else
				{ 
					sel.moveEnd('textedit');
					sel2.moveStart('textedit',-1);
					textAfterCursor = sel.text;
					textBeforeCursor = sel2.text;
					if (textBeforeCursor.indexOf('.') === -1) {window.event.returnValue=true;}
					else
					{
						decimalsBefore = textBeforeCursor.substr(textBeforeCursor.indexOf('.')+1).length;
						decimalsAfter = textAfterCursor.length;
						if ((decimalsBefore + decimalsAfter) >= nn) {window.event.returnValue=false;}
					}
				}
			}
			else 
			{
				if (oElement.selectionStart !== oElement.selectionEnd) {return true;}
				else
				{
					var textBeforeCursor = oElement.value.substr(0, oElement.selectionStart);
					var textAfterCursor = oElement.value.substr(oElement.selectionStart+1);
					if (textBeforeCursor.indexOf('.') === -1) {return true;}
					else
					{
						decimalsBefore = oElement.value.substr(oElement.value.indexOf('.')+1).length;
						if (decimalsBefore >= nn) {return false;} else {return true;}
					}
				}
			}
		}
		else if (key === 9 || ee.ctrlKey) {if (!window.event) {return true;}}  // tab
		else if (window.event) {window.event.returnValue = false;} else {return isAllowableEditKey(key);}
	}
}

//For onKeypress use only: Maximum length limit mask for combobox search;
//Integer maxlength is taken care of by CrInteger and alphanumeric textbox is taken care of by the database column length;
//No parameter means taking existing MaxLength; 0 means unlimited;
function CrMaxLength(ee, iLen)
{	
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		if (document.selection)
		{
			var sel = document.selection.createRange();
			if ((oElement.value.length - sel.text.length) >= iLen) {window.event.returnValue = false;}
		}
		else
		{
			if ((oElement.value.length >= iLen) && (oElement.selectionEnd === oElement.selectionStart))
			{
				return isAllowableEditKey(key);
			}
		}
	}
}

// For onKeypress only. Integer only mask.
// Parameters:  ee - always "event" to capture window's event;
//              nn - number of digits allowed; 0 means unlimited;
//              mok - if true then minus is allowed;
function CrInteger(ee, nn, mok)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		var sel;
		var str;
		if (document.selection) {sel = document.selection.createRange();}
		if (key === 45 && mok)	// minus sign allowed
		{
			if (document.selection) //	IE
			{
				if (oElement.value.lastIndexOf("-") >= 0 && sel.text.length === 0) {window.event.returnValue = false;}	//field already in focus
				else if (sel.text.length > 0) {window.event.returnValue = true;}	//text field is in focus and selected
				else
				{
					str = oElement.value.replace(/[-]/gi,"");
					oElement.value = "-"  + str;
					window.event.returnValue = false;	
				}
			}
			else	// Other browsers
			{
				if ((oElement.value.lastIndexOf("-") >= 0) && (oElement.selectionEnd === oElement.selectionStart)) {return false;}
				else if (oElement.selectionEnd !== oElement.selectionStart) {return true;}	//some sort of selections
				else
				{
					str = oElement.value.replace(/[-]/gi,"");
					oElement.value = "-" + str;
					return false;
				}
			}
		}		
		else if (key > 47 && key < 58)	// a numeric digit
		{
			str = oElement.value.replace(/[-]/gi,"");
            if (document.selection)
            {
                if (nn !== 0 && (str.length - sel.text.length) >= nn) { window.event.returnValue = false; }
            }
            else
            {
                if (nn !== 0 && str.length >= nn && oElement.selectionEnd === oElement.selectionStart)
                {
                    return isAllowableEditKey(key);
                }
            }
		}
		else if (key === 9 || ee.ctrlKey) {if (!window.event) {return true;}}  // tab and enter, etc. keys
		else if (window.event) {window.event.returnValue = false;}
		else if (key === 46) {return false;}    //take care of unexplainable Delete and period
		else {return isAllowableEditKey(key);}
	}	
}

//Function to automatically jump from one textbox to another when the max chars are reached
//For onKeyUp use only: autoTab(previous or null object, next or null object)
function AeAutoTab(ee, oPrev, oNext)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
		var key = (ee.charCode) ? ee.charCode : ((ee.which) ? ee.which : ee.keyCode);
		if (key !== 37 && key !== 39)	// not % or '
        {
            if (key === 8 && oElement.value.length === 0 && oPrev !== null) {oPrev.focus(); oPrev.select();}   //Backspace
            else if (key > 31 && oElement.value.length === oElement.maxLength && oNext !== null) {oNext.focus(); oNext.select();}
        }
	}
}

//For onBlur use only:
//typ: F-Float; (default is "string")
//opr: !=;>=;<=;>;<; (default is "==")
//oTar is the ClientID of the target control.
function IrIfaThenb(ee,typ,opr,val,oTar,sVal)
{
	ee = (ee) ? ee : ((window.event) ? event : null);
	if (ee)
	{
		var oElement = (ee.target) ? ee.target : ((ee.srcElement) ? ee.srcElement : null);
        if (typ === "F")
        {
            if (opr === "!=") {if (parseFloat(oElement.value) !== val) oTar.value = sVal;}
            else if (opr === ">=") {if (parseFloat(oElement.value) >= val) oTar.value = sVal;}
            else if (opr === "<=") {if (parseFloat(oElement.value) <= val) oTar.value = sVal;}
            else if (opr === ">") {if (parseFloat(oElement.value) > val) oTar.value = sVal;}
            else if (opr === "<") {if (parseFloat(oElement.value) < val) oTar.value = sVal;}
            else {if (parseFloat(oElement.value) === val) oTar.value = sVal;}
        }
        else
        {
            if (opr === "!=") {if (oElement.value !== val) oTar.value = sVal;}
            else if (opr === ">=") {if (oElement.value >= val) oTar.value = sVal;}
            else if (opr === "<=") {if (oElement.value <= val) oTar.value = sVal;}
            else if (opr === ">") {if (oElement.value > val) oTar.value = sVal;}
            else if (opr === "<") {if (oElement.value < val) oTar.value = sVal;}
            else {if (oElement.value === val) oTar.value = sVal;}
        }
    }
}

//For onBlur, onChange, onClick use only: oSrc and oTar are the ClientID of the source and target control.
function CmCalc2Deci(oSrc,oCond,oRate,oTar)
{
	if (!oCond.checked) {oTar.value = 0.00;} else {oTar.value = Math.round(oSrc.value * parseFloat(oRate.value) * 100) / 100;}
}

//The following four functions are needed for WebForm_AutoFocus:
function WebForm_FindFirstFocusableChild(control) {
    if (!control || !(control.tagName)) {return null;}
    var tagName = control.tagName.toLowerCase();
    if (tagName === "undefined") {return null;}
    var children = control.childNodes;
    if (children) {
        for (var i = 0; i < children.length; i++) {
            try {
                if (WebForm_CanFocus(children[i])) {return children[i];}
                else {
                    var focused = WebForm_FindFirstFocusableChild(children[i]);
                    if (WebForm_CanFocus(focused)) {return focused;}
                }
            } catch (e) {/**/}
        }
    }
    return null;
}

function WebForm_AutoFocus(focusId) {
    var targetControl;
    if (__nonMSDOMBrowser) {targetControl = document.getElementById(focusId);}
    else {targetControl = document.all[focusId];}
    var focused = targetControl;
    if (targetControl && (!WebForm_CanFocus(targetControl)) ) {focused = WebForm_FindFirstFocusableChild(targetControl);}
    if (focused) {
        try {
            focused.focus();
            if (__nonMSDOMBrowser) {
                focused.scrollIntoView(false);
            }
            if (window.__smartNav) {
                window.__smartNav.ae = focused.id;
            }
        }
        catch (e) {/**/}
    }
}

function WebForm_CanFocus(element) {
    if (!element || !(element.tagName)) return false;
    var tagName = element.tagName.toLowerCase();
    return (!(element.disabled) &&
            (!(element.type) || element.type.toLowerCase() !== "hidden") &&
            WebForm_IsFocusableTag(tagName) &&
            WebForm_IsInVisibleContainer(element)
            );
}

function WebForm_IsFocusableTag(tagName) {
    return (tagName === "input" ||
            tagName === "textarea" ||
            tagName === "select" ||
            tagName === "button" ||
            tagName === "a");
}

function WebForm_IsInVisibleContainer(ctrl) {
    var current = ctrl;
    while((typeof(current) !== "undefined") && (current !== null)) {
        if (current.disabled ||
            ( typeof(current.style) !== "undefined" &&
            ( ( typeof(current.style.display) !== "undefined" &&
                current.style.display === "none") ||
                ( typeof(current.style.visibility) !== "undefined" &&
                current.style.visibility === "hidden") ) ) ) {
            return false;
        }
        if (typeof(current.parentNode) !== "undefined" &&
                current.parentNode !== null &&
                current.parentNode !== current &&
                current.parentNode.tagName.toLowerCase() !== "body") {current = current.parentNode;}
        else {return true;}
    }
    return true;
}

function HideProgress() 
{
    document.getElementById('AjaxSpinner').style.display = 'none';
    /* Restore the menu fly-out function */
    if (typeof (Saved_Menu_HoverStatic) !== "undefined") { Menu_HoverStatic = Saved_Menu_HoverStatic; }
    $('#spinblocker').remove();
}

function ShowProgress() 
{
	/* Save the menu fly-out function as global variable and diable it temporary */
    if (typeof (Menu_HoverStatic) !== "undefined") { Saved_Menu_HoverStatic = Menu_HoverStatic; Menu_HoverStatic = function (o) { return; }; }
    var gridArea = $('.grid-container');
    var pos = gridArea.position();
    var height = gridArea.height();
    var width = gridArea.width();
    if (pos) {
        $("<div id='spinblocker'/>").click(function (e) { e.preventDefault(); }).css({ left: pos.left - $(document).scrollLeft(), top: pos.top - $(document).scrollTop(), width: width, height: height, "z-index": 100, position: 'fixed', background: '#999', opacity: 0.2 }).insertAfter(gridArea);
    }
    CenterAndShowElement(document.getElementById('AjaxSpinner'));
}

if (typeof(jQuery) !== 'undefined') {
    SetupMenuJs = function (menu) {
        var menuItems = jQuery(menu).children(":first");
        menuItems.each(function (idx, item) {
            item = jQuery(item);
            item.bind('click', RedirectToPage);
            item.addClass('TrMenuItem');

            //get sub menu if there is one..
            var sub = jQuery(item.id + 'Items');

            if (sub !== null) {
                SetupMenuJs(sub.children(":first"));
            }
        });
    };
    
    // jquery version
    RedirectToPage = function (event) {
        var menuItem = $(this);
        var links = menuItem.find('a');
        links.each(function (item) {
            window.location = item.href;
        });
    };    
    
    CenterAndShowElement = function (elem) {
        var fn = function (ele) {
            // these 3 are jQuery(1.2.6+) specific
            var screenWidth = jQuery(window).width();
            var screenHeight = jQuery(window).height();
            var scrollOffsets = jQuery(ele).offset();

            ele.style.display = '';
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(ele);
            var x = Math.round(screenWidth / 3) - Math.round(updateProgressDivBounds.width / 2);
            var y = Math.round(screenHeight / 3) - Math.round(updateProgressDivBounds.height / 2);
            Sys.UI.DomElement.setLocation(ele, x + scrollOffsets.left, y + scrollOffsets.top);
        };

        if (elem.each) elem.each(fn);
        else fn(elem);
    };    
    
    Event = {
        'observe': function (object, type, fn) { jQuery(object).bind(type, fn); }
    };
    
    getWidth = function (el) {
        return $(el).width();
    };
    hideElement = function (el) {
        jQuery(el).hide();
    };
    showElement = function (el) {
        jQuery(el).show();
    };
    setStyle = function (el, style) {
        jQuery(el).css(style);
    };
    jQuery.fn.extend({
        'getWidth': function () { 
            var x;
            this.each(function(e) { x = e.width();});
            return x;
            } ,
        'getHeight': function () { return this.height();} ,
        'getScrollOffsets': function () { return this.offset();},
        'setStyle': function (style) { this.css(style);}
    });
}

/* Pass ssd session (which contains company and project IDs) to window.open */
var oldOpen = window.open;
function fnWinOpen(_url, name, misc) {
    var l = window.location;
    var x = /ssd=[0-9]+/;
    var z = l.href.match(x);
    var url = url.replace(/^\~\//, "");
    var w = null;
    try {
        if ((url.startsWith(l.protocol + "//" + l.hostname) && l.href.match(/\?/)) || (!url.startsWith('http'))) {
            var xx = /[0-9]+/;
            var yy = z[0].match(xx)[0];
            w = oldOpen(url + (url.match(/\?/) ? "&" : "?") + "ssd=" + yy, name, misc);
        }
        else
            w = oldOpen(url, name, misc);
    } catch (e) {
        w = oldOpen(url, name, misc);
    }
    if (w) w.focus();
    return w;
} 
window.open = fnWinOpen;

//function adjust_height() {
//    $(".grid-container").each(function () {
//        var height = $(this).children(".viewport").height();
//        if (height === 0) return false;
//        var spacer = 5;
//        var total_height = height + spacer;
//        var min_height = 100;
//        var max_height = $(window).height() - min_height;
//        if (total_height > max_height)
//            total_height = max_height;
//        if (total_height < min_height)
//            total_height = min_height;
//        $(this).css("height", total_height + 14 + "px");
//    });
//}

/* typ: empty or 'G' means generic prefix@suffix, 'A' means pull admim email from system, 'C' means pull cs email from system; prefix and suffix are fall-back on A/C; */
function email(prefix, suffix, typ) {
    if (!typ || typ === 'G') {
        window.location = "mailto:" + prefix + "@" + suffix;
    }
    else {
        var currentSys = 1;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "AdminWs.asmx/GetSupportInfo",
            data: $.toJSON({ Typ: typ }),
            error: function (xhr, textStatus, errorThrown) {
                window.location = "mailto:" + prefix + "@" + suffix;
            },
            success: function (mr, textStatus, xhr) {
                var ret = mr.d;
                if (ret) {
                    window.location = "mailto:" + ret;
                }
                else
                    window.location = "mailto:" + prefix + "@" + suffix;
            }
        });
    }
}

/* Confirm button dialog */
function ConfirmPrompt(e, msg) {
    if (e.ask) {e.ask = null; return true; }
    e.ask = true;
    PopDialog("images/warning.gif", msg || "Are you sure", e.id, function (r) {
    if (r) {
        if (e.tagName === 'A' && (/^javascript:/).test(e.href))
            eval(e.href.replace('javascript:',''));
        else
            $(e).click();
    }
    else e.ask = null;
    });
    return false;
}
/* popup a modal message dialog */
function PopDialog(iconUrl, msgContent, focusOnCloseId, yesno) {
    var vpWidth = $(window).width();
    var smallphone = vpWidth <= 400;
    var mobileStyle = vpWidth < 1000;
    var noFocus = !$(':focus').attr('tagName');
    var currFocus = !noFocus ? $(':focus') : null;
    jQuery("#MsgIcon").attr("src", iconUrl);
    jQuery("#MsgContent").html(msgContent);
    var dlg = jQuery("#MsgPopup").dialog({
        autoOpen:false,
        modal: true,
        buttons: 
            !yesno ? smallphone ?
            {
            } : {
            Ok: function () {
                jQuery(this).dialog("close");
            },
            "Print Message": function () {
                $("<iframe id='printF' width=0 height=0></iframe>").appendTo($('Form'));
                var newWin = document.getElementById('printF').contentWindow;
                var doc = newWin.contentDocument || newWin.document;
                doc.open();
                doc.write('<body>');
                doc.write($('#MsgPopup').html());
                doc.write('</body>');
                doc.close();
                setTimeout(function () {
                    newWin.focus();
                    newWin.print();
                    $('#printF').remove();
                }, 100);
            }

        } : {
            Yes: function () {
                jQuery(this).dialog("close");
                yesno(true);
            },
            No: function () {
                jQuery(this).dialog("close");
                yesno(false);
            }
        },
        close: function (event, ui) {
            if (focusOnCloseId)
            {
                var isVisible = $('#' + focusOnCloseId).is(':visible');
                if (!isVisible)
                {
                    $('input:visible', $('#' + focusOnCloseId).parent()).focus();
                }
                else
                {
                    jQuery('#' + focusOnCloseId).focus();
                }
            }
        },
        open: function (event, ui) {
            var width = $(window).width();
            dlg.parent().css('max-width', width);
            $(document).keydown(function (e) {
                if (e.keyCode && e.keyCode === $.ui.keyCode.ENTER && dlg.dialog('isOpen')) {
                    e.preventDefault();
                    dlg.dialog("option", "buttons")['Ok'].apply(dlg);
                    return false;
                }
            });
        },
        position: mobileStyle ? { my: "center top", at: "center top+25%", of: window } : { my: "center", at: "center", of: window },
        resizable: !mobileStyle,
        draggable: !mobileStyle,
        minWidth: mobileStyle ? 250 : 800
    });
    jQuery("#MsgPopup").dialog('open');
}

function WindowsDate2DatetimePicker(val, notime) {
    var x = WindowsDateFormat2DatePickerDateFormat(val, notime);
    var ampm = val.match(/AM|PM/);
    x = x.replace(/yyyy/, 'Y').replace(/yy/, 'Y').replace(/MM/, 'mm').replace(/M/, 'mm').replace(/mm/, 'm').replace(/dd/, 'd') + (notime ? "" : (x.match(/H:i/) ? "" : ' H:i' + (ampm ? ' A' : '')));
    return x;
    //try {
    //    var format = WindowsDateFormat2DatePickerDateFormat(val);
    //    var valNoTime = val.replace(/[0-9]+:[0-9]+( (A|P)M)*$/, '');
    //    var valDate = new Date(valNoTime);
    //    var valTime = val.match(/[0-9]+:[0-9]+( (A|P)M)*$/)[0];
    //    var pm = val.match(/( PM)+$/);
    //    var hour = ('0' + (parseInt(valTime.match(/^[0-9]*/)) + (pm ? 12 : 0))).slice(-2);
    //    return $.datepicker.formatDate(WindowsDateFormat2DatePickerDateFormat(valNoTime), valDate) + ' ' + valTime.replace(/([0-9]+):([0-9]+)( (A|P)M)*$/, (hour === "12" && !pm ? "00" : hour) + ':$2');
    //} catch (e) {
    //    return val;
    //}

}
/* convert Windows DateFormat string to jquery datepicker date format */
function WindowsDateFormat2DatePickerDateFormat(sample, notime) {
    var result;
    var ampm = sample.match(/AM|PM/);
    var hasTime = sample.match(/:/);
    var sampleNoTime = sample.replace(/[0-9]+:[0-9]+( (A|P)M)*$/, '');
    var longFormat = sample && sampleNoTime.match(/[^: /.\-0-9]/); // induce from content
    var isSpecialDateFormat = sample && sample.match(/[0-9]+-[a-zA-Z]{3}-[0-9]{4}/); // induce from content of the form of 01-Nov-1990 specifically
    try {
        if (isSpecialDateFormat) result = 'd-M-yy';
        else if (longFormat) {
            result = ServerDateFormat.longFormat;
            if (result.match(/MMMM/)) result = result.replace(/MMMM/, "MM");
            else if (result.match(/MMM/)) result = result.replace(/MMM/, "M");
            else if (result.match(/MM/)) result = result.replace(/MM/, "mm");
            else if (result.match(/M/)) result = result.replace(/M/, "m");

            if (result.match(/yyyy/)) result = result.replace(/yyyy/, "yy");
            else if (result.match(/yy/)) result = result.replace(/yy/, "y");

            if (result.match(/dddd/)) result = result.replace(/dddd/, "DD");
            else if (result.match(/ddd/)) result = result.replace(/ddd/, "D");

        } else {
            result = ServerDateFormat.shortFormat;
            if (result.match(/MMMM/)) result = result.replace(/MMMM/, "MM");
            else if (result.match(/MMM/)) result = result.replace(/MMM/, "M");
            else if (result.match(/MM/)) result = result.replace(/MM/, "mm");
            else if (result.match(/M/)) result = result.replace(/M/, "m");

            if (result.match(/yyyy/)) result = result.replace(/yyyy/, "yy");
            else if (result.match(/yy/)) result = result.replace(/yy/, "y");

            if (result.match(/dddd/)) result = result.replace(/dddd/, "DD");
            else if (result.match(/ddd/)) result = result.replace(/ddd/, "D");
        }
    } catch (e) {
        result = 'yy/mm/dd';  // this would make sure it is parsable by C# as it is actually yyyy/mm/dd
    }
    if (sample) {
        try {
            if (!hasTime) var date = $.datepicker.parseDate(result, sample);
        } catch (e) {
            result = 'yy/mm/dd'; // if it cannot be parsed, fallback to this
        }
    }
    return hasTime ? ((longFormat ? ServerDateFormat.shortFormat : result.replace(/yy/, 'Y').replace(/mm/, 'm').replace(/dd/, 'd')) + (notime ? '' : ' H:i' + (ampm ? ' A' : ''))) : result;
}

/* add jquery widget functionalities */
function nextOnTabIndex(element) { var fields = $($('form').find('a[href], button, input, select, textarea').filter(':visible').filter(':enabled').toArray().sort(function (a, b) { return ((a.tabIndex > 0) ? a.tabIndex : 1000) - ((b.tabIndex > 0) ? b.tabIndex : 1000); })); return fields.eq((fields.index(element) + 1) % fields.length); }
jQuery.fn.extend({ fire: function (evttype) { el = this.get(0); if (document.createEventObject) { el.fireEvent('on' + evttype); } else if (document.createEvent) { var evt = document.createEvent('HTMLEvents'); evt.initEvent(evttype, false, false); window.event = evt; el.dispatchEvent(evt); } return this; } });
function ApplyJQueryWidget(aNamId, aValId) {
    
    var aNam = aNamId ? $('#' + aNamId) : null;
    var aVal = aValId ? $('#' + aValId) : null;
    var aNams = [];
    var aVals = [];
    $('span.starrating').stars({ inputType: "label", showTitles: true, disabled: true });
    $('span.progressbar').each(function (i, e) {
        var select = e;
        var width = $(e).width();
        var nolabel = $(e).attr('NoLabel') !== undefined;
        var l = $("<div/>").css({ float: 'left', margin: '0 0 0 20px', display: 'none' }).html($(e).val());
        var sdiv = $("<div/>")
                            .css({ width: width, float: 'left', background: $(e.element).css("background-color") })
                            .insertBefore(e);
        var slider = sdiv.slider(
                            { disabled: true, range: "min", value: parseInt($(e).html() || 0), min: parseInt($(e).attr("Min") || 0), max: parseInt($(e).attr("Max") || $(e).height()), step: parseInt($(e).attr("Step") || 1), slide: function (event, ui) {/**/} });
        if (!nolabel) l.insertAfter(sdiv);
        $(e).hide();
    });
    $('select,input[type=text][NeedConfirm=Y],input[type=text][Behaviour=Rating],input[type=text][Behaviour=Slider],input[type=text][Behaviour=DateTime],input[type=text][Behaviour=Date]').each(function (i, e) {
        var behaviour = $(e).attr('Behaviour');
        var tag = e.tagName;
        var needConfirm = $(e).attr('NeedConfirm') === 'Y';
        var onchangeScript = $(e).attr('onchange');
        var currVal = $(e).val();
        var changed = false;
        var select = undefined;
        if (behaviour === "Rating") $(e).stars({ inputType: "textbox", showTitles: true });
        else if (behaviour === "DateTime") {
            $(e).datetimepicker({
                step: 15, 'closeOnDateSelect': true, 'format': WindowsDate2DatetimePicker(currVal), 'formatDate': WindowsDateFormat2DatePickerDateFormat(currVal, true),
                'onSelectDate': function (a, b, c) { changed = true; var n = nextOnTabIndex($(this)); setTimeout(function () { $(n).focus(); }, 0); },
                'onSelectTime': function (a, b, c) { changed = true; var n = nextOnTabIndex($(this)); setTimeout(function () { $(n).focus(); }, 0); },
                'onChangeDateTime': function (a, b, c) { if ($(e).val() === currVal) return false; },
                //'onShow': function (a,b,c,d) {},
                //'onClose': function (a, b, c, d) { if (!changed && currVal && false) setTimeout(function () { $(e).val(currVal); }, 0); }
            });
        }
        else if (behaviour === "Date") {
            $(e).datepicker({
                'beforeShow': function (ele, inst) {
                    openDatePickerId = ele.id;
                    currVal = $(ele).val(); $(ele).attr('onchange', 'return false;');
                    return { dateFormat: WindowsDateFormat2DatePickerDateFormat($(ele).val()) };
                }, changeMonth: true, changeYear: true,
                'onSelect': function (el) { if (onchangeScript) { eval(onchangeScript); } else { if (this.fireEvent) this.fireEvent('onchange'); else $(this).fire('change'); } var n = nextOnTabIndex($(this)); setTimeout(function () { $(n).focus(); }, 0); },
                'onClose': function (dateText, instance) { openDatePickerId = null; if (onchangeScript) { $(this).attr('onchange', onchangeScript); if (currVal !== $(this).val()) if (this.fireEvent) this.fireEvent('onchange'); else $(this).fire('change'); } }
            });
            if (openDatePickerId) setTimeout(function () { $('#' + openDatePickerId).datepicker('show'); });
        }
        else if (behaviour === "Slider") {
            select = e, title = e.title;
            var nolabel = $(e).attr('NoLabel') !== undefined;
            var l = $("<div/>").css({ float: 'left', margin: '0 0 0 20px', display: 'none' }).html($(e).val());
            var handle = null;
            var sdiv = $("<div/>")
                            .css({ width: $(e).width(), float: 'left', background: $(e.element).css("background-color") })
                            .insertBefore(e);
            var slider = sdiv.slider(
                            {
                                disabled: $(e).attr('disabled') !== undefined, range: "min", value: $(e).val(), min: parseInt($(e).attr("Min")), max: parseInt($(e).attr("Max")), step: parseInt($(e).attr("Step")), slide: function (event, ui) {
                                    $(select).val(ui.value); $(l).html(ui.value); handle.qtip('option', 'content.text', '' + ui.value);
                                    try { $(select).fire('change'); } catch (e) {/**/}
                                    try { this.focus(); } catch (e) {/**/}
                                }
                            }).attr('title', title);
            if (!nolabel) l.insertAfter(sdiv);
            /* Grab and cache the newly created slider handle */
            handle = $('.ui-slider-handle', slider);
            handle.qtip({
                content: '' + slider.slider('option', 'value'), // Use the current value of the slider
                position: {
                    my: 'bottom center',
                    at: 'top center',
                    container: handle // Stick it inside the handle element so it keeps the position synched up
                },
                hide: {
                    delay: 1000 // Give it a longer delay so it doesn't hide frequently as we move the handle
                },
                style: {
                    classes: 'ui-tooltip-slider',
                    widget: true // Make it Themeroller compatible
                }
            }
        );
            $(e).hide();
        }
        else if (tag === "SELECT") {
            if ($(e).attr('multiple') === 'multiple') {
                ApplyMultiSelect(e);
            }
        }
        if (needConfirm) {
            if (aNamId) {
                select = e;
                aNams.push(select.id);
                aVals.push($(select).val());
                aNam.val(aNams.join(','));
                aVal.val(aVals.join(','));
            }
        }
    });
}

function ShowEmbedScript() {
    var searchString = window.location.search.substring(1), params = searchString.split("&"), hash = {};
    for (var i = 0; i < params.length; i++) { var val = params[i].split("="); hash[unescape(val[0].toLowerCase())] = unescape(val[1]); }
    delete hash['ssd']; delete hash['id']; delete hash['csy'];
    hash['typ'] = 'N';
    var qs = jQuery.param(hash);
    var newDiv = $(document.createElement('div'));
    var location = window.location;
    var iframe = '<div><div style="position:relative"><iframe frameborder="0" src="' + location.protocol + "//" + location.hostname + location.port + location.pathname + '?' + qs + '" style="width:100%;height:100vh;"></iframe></div></div>';
    var inst = 'In order to embed this screen into your site, just copy the code snippet below and paste into your html page';
    $(newDiv).html('<div>' + inst + '</div>' + '<textarea id="snippet">' + iframe + '</textarea>');
    $(newDiv).dialog({ modal: true, width: 'auto', buttons: { 'test': function () { var testDiv = $(document.createElement('div')); $(testDiv).html($('#' + 'snippet').val()); $(testDiv).dialog({ modal:true, width:'80%' }); } } });
}

function TranslateItems(translateDict) {
    $.each(translateDict, function (k, v) {
        $('#' + k).html(v);
    });
}

function RetrieveBrowserCap(callback, cbParm) {
    var browserCap = {};
    var now = new Date();
    var t1 = new Date(now.getFullYear(), 1, 1, 0, 0, 0);
    var t2 = new Date(now.getFullYear(), 7, 1, 0, 0, 0);
    var o1 = t1.getTimezoneOffset();
    var o2 = t2.getTimezoneOffset();
    browserCap.tzOffset = now.getTimezoneOffset();
    browserCap.tzBaseOffset = t1.getTimezoneOffset();
    browserCap.tzHasDST = o1 !== o2 ? 1 : 0;
    browserCap.tzDST = browserCap.tzOffset !== o1 ? 1 : 0;
    browserCap.tzFullDateTime = now + "";

    var codeLatLng = function (lat, lng) {
        var geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                //                console.log(results)
                if (results[1]) {
                    //formatted address
                    //                    alert(results[0].formatted_address)
                    //find country name
                    for (var i = 0; i < results[0].address_components.length; i++) {
                        for (var b = 0; b < results[0].address_components[i].types.length; b++) {
                            //there are different types that might hold a city admin_area_lvl_1 usually does in come cases looking for sublocality type will be more appropriate
                            if (results[0].address_components[i].types[b] === "locality" || results[0].address_components[i].types[b] === "administrative_area_level_3") {
                                //this is the object you are looking for
                                browserCap.myCity = results[0].address_components[i].short_name;
                            }
                            //                                if (results[0].address_components[i].types[b] === "administrative_area_level_2") {
                            //                                    //this is the object you are looking for
                            //                                    browserCap.myCity = (browserCap.myCity || "") + " " + results[0].address_components[i].short_name;
                            //                                }
                            if (results[0].address_components[i].types[b] === "administrative_area_level_1") {
                                //this is the object you are looking for
                                browserCap.myState = results[0].address_components[i].short_name;
                            }
                            if (results[0].address_components[i].types[b] === "country") {
                                //this is the object you are looking for
                                browserCap.myCountry = results[0].address_components[i].short_name;
                            }
                            if (results[0].address_components[i].types[b] === "postal_code") {
                                //this is the object you are looking for
                                browserCap.myPostal = results[0].address_components[i].short_name;
                            }
                        }
                    }

                } else {
                    //alert("No results found");
                }
            } else {
                //alert("Geocoder failed due to: " + status);
            }
            callback(browserCap, cbParm);
        });
    };

    var geo_success = function (position) {
        var myLat = position.coords.latitude;
        var myLng = position.coords.longitude;
        browserCap.myLat = myLat;
        browserCap.myLng = myLng;
        try {
            codeLatLng(myLat, myLng);
        } catch (err) {
            callback(browserCap, cbParm);
        }
    };

    var geo_failure = function (error) {
        browserCap.geoFailureCode = error.code;
        switch (error.code) {
            case error.PERMISSION_DENIED:
                browserCap.geoFailureReason = "User denied the request for Geolocation.";
                break;
            case error.POSITION_UNAVAILABLE:
                browserCap.geoFailureReason = "Location information is unavailable.";
                break;
            case error.TIMEOUT:
                browserCap.geoFailureReason = "The request to get user location timed out.";
                break;
            case error.UNKNOWN_ERROR:
                browserCap.geoFailureReason = "An unknown error occurred.";
                break;
        }
        callback(browserCap, cbParm);
    };

    if (!document && navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(geo_success, geo_failure);
    } else {
        callback(browserCap, cbParm);
        if (cbParm.noGeoAPI) cbParm.noGeoAPI();
    }
}

function AsyncInform(o, params) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: params.url,
        data: $.toJSON({ contextStr: $.toJSON(o) }),
        error: function (xhr, textStatus, errorThrown) { if (params.error) params.error(xhr, errorThrown); },
        success: function (result, textStatus, xhr) { if (params.success) params.success(result); }
    });
}

// keyCtrlID is needed for client-side communication without a postback.
function SearchLink(url, keyCtrlID, dlgWidth, dlgHeight) {
    if (url === '') return false;
    var canClose = [false];
    var vpWidth = $(window).width();
    var vpHeight = $(window).height();
    var smallphone = vpWidth <= 400;
    var mobileStyle = vpWidth < 767;
    var iframeHeight;
    var keyCtrl = $('#' + keyCtrlID.replace(/_KeyIdText$/, '_KeyId'));
    var key = keyCtrl.length > 0 ? "&key=" + keyCtrl.val() : "";
    url = url.replace(/^\~\//, "");
    if (url.match(/^file|mail|http|java/i) || IsMobile() || url.search(/\.jpg|\.png|\.gif|\.mp3|\.mp4|\.txt/i) >= 0) {
        var windowName = url.split("?")[0].split("#")[0];
        window.open(url + key, windowName, 'resizable=yes,scrollbars=yes,status=yes,width=700,height=500');
    }
    else if (url.search(/\.doc|\.docx|\DnLoad.aspx|\.avi|\.exe|\.xls|\.xlsx|\.xlsm|\.rtf|\.ppt|\.pptx|\.vsd|\.zip|\.tar|\.gzip/i) >= 0) {
        // Keep the above up-to-date for all file types that are forced to download on browser leaving a blank window behind.
        window.location.href = url;
    }
    else {
        iframeHeight = smallphone ? '85vh' : (mobileStyle ? '85vh' : (dlgHeight === '' ? '80vh' : dlgHeight));
        var content = ("<div id='searchLinkDlg'><div style='position:relative;'><iframe id='searchLinkIframe' src='" + url + key + "' style='width: 100%; height:" + iframeHeight + "' Frameborder='0' scrolling='auto'></iframe></div></div>");
        canClose[0] = false;
        var dlg = jQuery(content).dialog({
            dialogClass: 'searchDlg',
            width: smallphone ? '100%' : (mobileStyle ? '100%' : (dlgWidth === '' ? '90%' : dlgWidth)),
            autoOpen: true,
            modal: true,
            resizable: true,
            position: { my: 'center', at: 'center', of: window },
            minWidth: mobileStyle ? 250 : 400,
            maxWdith: '100%',
            maxHeight: '100%',
            buttons: [{
                text: "Maximize",
                click: function () { $('.ui-icon-restore').parent().show(); $('.ui-icon-maximize').parent().hide(); $(this).dialog('option', 'width', '100%'); $('#searchLinkIframe').css('height', $(window).height() - 55); $(this).dialog('option', 'position', { my: 'top', at: 'top', of: window }); },
                icons: { primary: 'ui-icon-maximize' },
                showText: false
            },
                {
                    text: "Restore Down",
                    click: function () { $('.ui-icon-maximize').parent().show(); $('.ui-icon-restore').parent().hide(); $(this).dialog('option', 'width', smallphone ? '100%' : (mobileStyle ? '100%' : (dlgWidth === '' ? '70%' : dlgWidth))); $('#searchLinkIframe').css('height', iframeHeight); $(this).dialog('option', 'position', { my: 'center', at: 'center', of: window }); },
                    icons: { primary: 'ui-icon-restore' },
                    showText: false
                }
            ],
            resize: function (event, ui) {
                var widthRatio = Math.round((ui.size.width / vpWidth) * 100);
                var heightRatio = Math.round((ui.size.height / vpHeight) * 100);
                $(this).parent().css('width', widthRatio + '%');
                $(this).parent().css('height', heightRatio + '%');
                $(this).find('#searchLinkIframe').css('height', heightRatio - 6 + 'vh');
            }
            , beforeClose: function (event, ui) {
                $(this).find('#searchLinkIframe').attr('src', 'about:blank');
                setTimeout(function () { if ($('#searchLinkIframe').contents()[0].location === "about:blank") { canClose[0] = true; dlg.dialog('destroy'); } }, 100); return canClose[0];
            }
        });

        $('.ui-icon-restore').parent().hide();
        $(window).resize(function () {
            smallphone = $(window).width() <= 400;
            mobileStyle = $(window).height() < 767;
            var dlg = $('#searchLinkDlg');
            dlg.dialog('option', 'position', { my:'center', at:'center', of:window });
            if (smallphone) {
                dlg.dialog('option', 'width', '100%');
                dlg.find("#searchLinkIframe").css("height", "85vh");
            }
            else if (mobileStyle) {
                dlg.dialog('option', 'width', '100%');
                dlg.find("#searchLinkIframe").css("height", "85vh");
            } else {
                if (dlgWidth === '') { dlg.dialog('option', 'width', '70%'); } else { dlg.dialog('option', 'width', dlgWidth); }
                if (dlgHeight === '') { dlg.find("#searchLinkIframe").css("height", "70vh"); } else { dlg.find("#searchLinkIframe").css("height", dlgHeight); }
            }
        });
    }
}

function removeFlag(e) { $(e).remove(); }

function flagInput(element, message, child, childBtn) {
    $spacer = 12;
    $width = $(child).outerWidth();
    $height = $(child).outerHeight() / 2;
    $height = $height > 16 ? 16 : $height;
    $margin = $spacer + $width;
    if (message !== "") {
        $(element).prepend("<div style=\"margin-left:" + $margin + "px;margin-top:-" + $height + "px;\" class=\"flag triangle-right left\" onclick='removeFlag(this);'>" + message + "</div>");
    }

    if ($(this).data("originalBorderColor") === undefined) {
        $(element).data("originalBorderColor", $(element).css("borderColor"));
    }

    if(child !== null){
        $(child).css("border", "1px solid red");
        $(child).addClass("errMsgBorder");
        if(childBtn !== null){
            $(childBtn).css({'border' : '1px solid red', 'border-left-width': '0px'});
            $(childBtn).addClass("acErrMsgBorder");
        }
    }else{
        $(element).css("border", "1px solid red");
        $(element).addClass("errMsgBorder");
    }
}
function ValidateThisModule(msgObj)
{
    $(".flag").remove();
    $(".errMsgBorder").css("border","").removeClass("errMsgBorder");
    $(".acErrMsgBorder").css("border","").removeClass("acErrMsgBorder");
    try { $.watermark.hideAll(); } catch (e) {/**/}
    var v = Page_Validators;
    var allValid = true;
    if (v && v !== "underfined") 
    {
        $(v).each(function(i,e){
            try { ValidatorEnable(Page_Validators[i]); }
            catch (er) {
                try {
                    var x = Page_Validators[i].controltovalidate;
                    /* combobox */
                    Page_Validators[i].controltovalidate = x + '_KeyId';
                    ValidatorEnable(Page_Validators[i]);

                } catch (er1) {
                    try {
                        /* signature */
                        Page_Validators[i].controltovalidate = x + '_SigId';
                        ValidatorEnable(Page_Validators[i]);
                    } catch (er2) {/**/}
                }
            }
            if (!e.isvalid) 
            {
                var mc = undefined;
                try {
                    var c = e.controltovalidate;
                    try {
                        var inTabId = null;
                        $('#content > div').each(function (i, e) { if ($('#' + c, $(e)).length > 0) inTabId = e.id.split('_').pop(); });
                        var inTab = $('ul#tabs > li > a[name="' + inTabId + '"]');
                        //var inTab = $('#__tab_' + c.replace('_' + $(c.split('_')).last()[0],''));
                        inTab.css({ 'color': 'red', 'text-decoration': 'blink' });
                        inTab.click();
                    } catch (e) {/**/}
                    if (!/_KeyId$/.test(c)) {
                        if (/_SigId$/.test(c)) {
                            mc = c.replace(/_SigId$/, '');
                            flagInput($('#' + c).parent().parent(), msgObj[$(c.split('_')).last()[0]].msg, $('#' + c).parent(), null);
                        }
                        else if ($('select#' + c.replace(/Hidden$/, '')).length > 0 && $('select#' + c.replace(/Hidden$/, '')).attr('size') > 1 && $('select#' + c.replace(/Hidden$/, '')).parent().is(':visible') && $('ul.ui-multiselect-checkboxes', $('select#' + c.replace(/Hidden$/, '')).parent()).length > 0) {
                            flagInput($('#' + c).parent(), msgObj[$(c.split('_')).last()[0]].msg, $('ul.ui-multiselect-checkboxes', $('select#' + c.replace(/Hidden$/, '')).parent()).parent().parent(), null);
                        }
                        else if ($('select#' + c.replace(/Hidden$/, '')).length > 0 && $('select#' + c.replace(/Hidden$/, '')).parent().is(':visible') && $('span.ui-multiselect-selected-list', $('select#' + c.replace(/Hidden$/, '')).parent()).length > 0) {
                            flagInput($('#' + c).parent(), msgObj[$(c.replace(/Hidden$/, '').split('_')).last()[0]].msg, $('span.ui-multiselect-selected-list', $('select#' + c.replace(/Hidden$/, '')).parent()).parent(), null);
                        }
                        else {
                            flagInput($('#' + c.replace(/Hidden$/, '')).parent(), msgObj[$(c.split('_')).last()[0].replace(/Hidden$/, '')].msg, $('#' + c.replace(/Hidden$/, '')), null);
                        }
                    } else {
                    
                        mc = c.replace(/_KeyId$/,'');
                        var autocomplete = $('input.autocomplete-box-input',$('#'+c).parent());
                        var autocompleteBtn = $('button.autocomplete-box-button',$('#'+c).parent());
                        flagInput(autocomplete.parent(),msgObj[$(mc.split('_')).last()[0]].msg, autocomplete, autocompleteBtn);
                    }
                } catch (er1) {/**/}
                allValid = false;
            }
//            ValidatorEnable(Page_Validators[i],true);
        });
    }
    if (!allValid) try { $.watermark.showAll(); } catch (e) {/**/}
    return allValid;
}

function WatermarkInput(msgObj, containerId)
{
    $.watermark.options.hideBeforeUnload = false;
    $('input.inp-num,input.GrdNum,input.inp-txt,input.GrdTxt,textarea.inp-txt').each(function (i, e) { if ($(e).attr('type') !== 'password') try { $(e).watermark(msgObj[$(e.id.split('_')).last()[0]].hint, { useNative: false }); } catch (er) {/**/} });
}

// Load iFrame after PageLoad to avoid flickering:
(function () {
    var div = document.createElement('div'),
        ref = document.getElementsByTagName('base')[0] || document.getElementsByTagName('script')[0];
    div.innerHTML = '&shy;<style> iframe { visibility: hidden; } </style>';
    ref.parentNode.insertBefore(div, ref);
    window.onload = function () { div.parentNode.removeChild(div); };
})();

// Use the enter key to act:
function EnterKeyCtrl(e, clientID) {
    if (e.keyCode === 13) {
        var tb = document.getElementById(clientID);
        tb.click();
        return false;
    }
}

function GoogleSignIn(clientid, cTokenId, cPostBackBtnId) {
    clientid = clientid || googleClientId;
    var isCheck = true;
    var googleAPIScopes = 'https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile';
    var handleAuthResult = function (authResult) {
        if (authResult && !authResult.error) {
            var token = gapi.auth.getToken();
            $('#' + cTokenId).val(token.access_token);
            $('#' + cPostBackBtnId).click();
        }
        else {
            if (isCheck) {
                // force sign in(dialog)
                isCheck = false;
                gapi.auth.authorize({ client_id: clientid, scope: googleAPIScopes, immediate: false }, handleAuthResult);
            }
            else {
                $('#' + cTokenId).val("");
                alert('google sign in failed');
            }
        }
    };

    // force login popup which would close itself if already signin, only way to avoid popup blocker by browser
    gapi.auth.authorize({ client_id: clientid, scope: googleAPIScopes, immediate: false }, handleAuthResult);

    return false;
}

function FacebookSignIn(appId, cTokenId, cPostBackBtnId) {
    appId = appId || facebookAppId;
    var isCheck = true;
    var handleAuthResult = function (authResult) {
        if (authResult && authResult.authResponse) {
            $('#' + cTokenId).val(authResult.authResponse.accessToken);
            setTimeout(function () { $('#' + cPostBackBtnId).click(); }, 0);
        }
        else {
            $('#' + cTokenId).val("");
            alert('facebook sign in failed');
        }
        return false;
    };

    // force login popup which would close itself if already signin, only way to avoid popup blocker by browser
    FB.login(handleAuthResult, { scope: 'email' });
    return false;
}

/* Client-side Charting */
function ShowChart(context, elementId, chartType) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //url: "AutoComplete.aspx/GetChartData",
        url: "AutoComplete.aspx/RptGetChart",
        data: $.toJSON({ contextStr: $.toJSON(context) }),
        error: function (xhr, textStatus, errorThrown) {
            //$(input).removeClass("wait");
            //response([]);
            var x = "";
        },

        success: function (result, textStatus, xhr) {
            //$(input).removeClass("wait");
            var points = result.d.data;
            var xLabel = context.xLabel || result.d.xLabel ;
            var yLabel = context.yLabel || result.d.yLabel ;
            var title = context.title || result.d.rptTitle ;
            if (chartType === "line") ClientSideLineChart(points, xLabel, yLabel, title, elementId);
            else ClientSideLineChart(points, xLabel, yLabel, title, elementId);
        }
    });
}

/* Cannot be responsive for now. */
function PopCharts(chartInfo) {
    var vpWidth = $(window).width();
    var smallphone = vpWidth <= 400;
    var mobileStyle = vpWidth < 1024;
    var info = chartInfo;
    var time = (new Date()).getTime();
    var holder = $.map(info,
        function (v, i) {
            return "<div><div class=\'example-plot\' id=\'" + "chart" + (time + i) + "\'></div></div>";
        });
    var popArea = "<div id=\'chartPanel" + time + "\' class=\'popCharts\' style=\'display: none;\'>" + holder.join("") + "<div style=\'clear: both;\'></div>" + "</div>";
    var popElem = $(popArea);
    var dialog = popElem.dialog(
    {
        width: info.length > 1 && vpWidth > 1024 ? '40%' : (smallphone ? '100%' : '90%'),
        modal: true,
        autoOpen: false,
        open: function (event, ui) {
            for (var i = 0; i < info.length; i++) {
                ShowChart(info[i], 'chart' + (time + i), info[i].chartType);
            }
        },
        close: function (event, ui) {
            $("#chartPanel" + time).dialog().remove();
        },
        position: { my: "center", at: "top", of: window },
        resizable: !mobileStyle,
        draggable: !mobileStyle
    });
    $('#chartPanel' + time).dialog('open');
    return dialog;
}

// Keep the following for backward compatibility only:
//function ClientSideLineChart(points, xlabel, ylabel, title, elementid) {
//    for (var i = 0; i < points.length; i++) {
//        points[i][1] = parseFloat(points[i][1]);
//    }
//    var plot2 = $.jqplot(elementid, [points], {
//        title: title,
//        axes: {
//            xaxis: {
//                renderer: $.jqplot.DateAxisRenderer,
//                label: xlabel,
//                labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
//                tickRenderer: $.jqplot.CanvasAxisTickRenderer,
//                tickOptions: {
//                    angle: -40,
//                    fontSize: '9pt'
//                }
//            },
//            yaxis: {
//                label: ylabel,
//                labelRenderer: $.jqplot.CanvasAxisLabelRenderer
//            }
//        }
//    });
//}

function ClientSideLineChart(points, xlabel, ylabel, title, elementid, displayformat) {
    $.jqplot._noToImageButton = true;
    $.jqplot.config.enablePlugins = true;
    for (var i = 0; i < points.length; i++) {
        points[i][1] = parseFloat(points[i][1]);
    }
    var format = '';
    if (displayformat === 'M') {
        format = '%b-%Y';
        tickrage = '1 month';
    } else if (displayformat === 'A') {
        format = '%Y';
        tickrage = '1 year';
    } else {
        format = '%e-%b-%Y';
        tickrage = '';
    }
    var plot2 = $.jqplot(elementid, [points], {
        title: title,
        seriesColors: ["rgba(211, 235, 59, 0.7)"],
        grid: {
            background: 'rgba(57,57,57,0.0)',
            drawBorder: false,
            shadow: false,
            gridLineColor: '#666666',
            gridLineWidth: 2
        },
        seriesDefaults: {
            rendererOptions: {
                smooth: true,
                //animation: {
                //    show: true
                //}
            },
            showMarker: true
        },
        series: [
            {
                //fill: true
                lineWidth: 5,
                markerOptions: { style: 'square' }
            }
        ],
        axesDefaults: {
            rendererOptions: {
                baselineWidth: 1.5,
                baselineColor: '#444444',
                drawBaseline: false
            }
        },
        axes: {
            xaxis: {
                renderer: $.jqplot.DateAxisRenderer,
                label: xlabel,
                //labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                tickOptions: {
                    angle: -30,
                    fontSize: '9pt',
                    textColor: '#dddddd',
                    formatString: format
                },
                tickInterval: tickrage,
                drawMajorGridlines: false
            },
            yaxis: {
                label: ylabel,

                numberTicks: 2,
                renderer: $.jqplot.LogAxisRenderer,
                pad: 0,
                rendererOptions: {
                    minorTicks: 1
                },
                tickOptions: {
                    angle: -40,
                    fontSize: '9pt',
                    textColor: '#dddddd',
                    showMark: false
                }
            }
        },
        highlighter: {
            show: true,
            sizeAdjust: 7.5
        },
        cursor: {
            show: false
        }
    });
}

/* Google minimal jquery tab-folder. */
function jqTab(msgObj) {
    (function () {
        var resetTabs = function (currentTab) {
            $("#content > div").each(function (i, e) { if ($(e).attr('id').split('_').pop() !== currentTab) $(e).hide(); }); //Hide all content, except current
            $("#tabs a").removeClass("current");
        };

        var switchTo = function (ele) {
            var myId = $(ele).attr('id');
            var myIdArr = myId.split('_');
            var myName = myIdArr.pop();
            var myIdPre = myIdArr.join('_');
            resetTabs(myName.substring(1));
            $(ele).addClass("current"); // Activate this
            $('#' + myIdPre + '_cCurrentTab').val(myName); // remember
            $('#' + myIdPre + '_' + $(ele).attr('name')).fadeIn(); // Show content for current tab
        };

        $("#tabs a").on("click", function (e) {
            e.preventDefault();
            switchTo(this);
        });
        var firstTabId = $("#tabs > li:first > a").attr('id');
        if (firstTabId) {
            var firstTabIdArr = firstTabId.split('_');
            firstTabIdArr.pop();
            var firstTabPre = firstTabIdArr.join('_');
            var currentTab = $('#' + firstTabPre + '_cCurrentTab').val();
            if (!currentTab) {
                $("#" + firstTabId).addClass('current');
            }
            else {
                switchTo($('#' + firstTabPre + '_' + currentTab));
            }
        }
    })();
}

function DuplicateMenuFromMobile() {
    var mobileMenu = $('ul#TpMobileMenu');
    mobileMenu.after("<ul id='TpMenu' class='TpMenuSub' style='display:none;'>" + mobileMenu.html() + "</ul>");
}

function InjectCrystalReportViewerCSS(viewer_id) {
    var cssUrl = window.location.href.substring(0, window.location.href.lastIndexOf("/") + 1) + "css/crystalreport.css";
    var x = $('#' + viewer_id).contents().find('iframe').contents().find('head');

    x.append('<link type="text/css" rel="stylesheet" href="' + cssUrl + '" />');
}

/* Calendar display */
function MakeMonthlyPlanner(dayList, activeMonth) {
    //    var dayList = [ {date:'2014/5/1',content:8.5},{date:'2014.5.8',content:7},{date:'2014.6.8',content:7},{date:'2014.6.18',content:7},,{date:'2014.9.8',content:7}];

    var makeDatePlanner = function (dayList, year, month) {
        var dateDiff = function (startDate, endDate) { return Math.round(((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24)), 0); };
        var daySlot = new Array(42);
        var firstDate = new Date(year, month, 1);
        var monthOf = firstDate.getMonth();
        var yearOf = firstDate.getFullYear();
        var lastDate = new Date(yearOf, monthOf, 1); lastDate.setMonth(lastDate.getMonth() + 1, 0);
        var shift = new Date(firstDate.getFullYear(), firstDate.getMonth(), 1).getDay();
        if (shift === 0) shift = 7;
        var nextMonthIdx = lastDate.getDate() + shift;
        var firstDateInSlot = new Date(yearOf, monthOf, 1); firstDateInSlot.setDate(firstDateInSlot.getDate() - shift);
        var lastDateInSlot = new Date(yearOf, monthOf, 1); lastDateInSlot.setMonth(lastDateInSlot.getMonth() + 1, 0); lastDateInSlot.setDate(lastDateInSlot.getDate() + (42 - nextMonthIdx));
        //        var dayListOf = $.grep(dayList, function (e, i) { try { var d = new Date(Date.parse(e.date.replace(/\./g, "/"))); return d.getFullYear() === year && d.getMonth() === month; } catch (er) { return false; } });
        var dayListOf = $.grep(dayList, function (e, i) { try { var d = new Date(Date.parse(e.date.replace(/\./g, "/"))); return d >= firstDateInSlot && d <= lastDateInSlot; } catch (er) { return false; } });
        var ii = 0;
        //        for (ii = shift - 1; ii >= 0; ii = ii - 1) {
        //            daySlot[ii] = { date: new Date(firstDate), prior: true };
        //            daySlot[ii].date.setDate(ii - shift + 1);
        //        }

        //        for (ii = lastDate.getDate() + shift; ii <= 41; ii = ii + 1) {
        //            daySlot[ii] = { date: new Date(lastDate), next: true };
        //            daySlot[ii].date.setDate(daySlot[ii].date.getDate() + (ii - lastDate.getDate() - shift) + 1);
        //        }

        for (ii = 0; ii < dayListOf.length; ii++) {
            var date = new Date(Date.parse(dayListOf[ii].date.replace(/\./g, "/")));
            var dom = date.getDate();
            var dow = date.getDay();
            var idx = dateDiff(firstDateInSlot, date);
            month = date.getMonth();
            year = date.getFullYear();
            daySlot[idx] = { date: date, curr: date >= firstDate && date <= lastDate, prior: date < firstDate, next: date > lastDate, obj: dayListOf[ii] };
            //            if (year === yearOf && month === monthOf) {
            //                daySlot[shift + (dom - 1)] = { date: date, curr: true, obj: dayListOf[ii] };
            //            }
        }

        for (ii = 0; ii < 42; ii++) {
            if (!daySlot[ii]) {
                date = new Date(firstDateInSlot); date.setDate(date.getDate() + ii);
                daySlot[ii] = { date: date, curr: ii >= shift && ii < nextMonthIdx, prior: ii < shift, next: ii >= nextMonthIdx };
            }
        }
        return daySlot;
    };
    var makeIntendedDate = function (d) { return new Date(d.year, d.month, 1); };
    var intendedMonth = activeMonth && activeMonth.year && activeMonth.month >= 0 && activeMonth.month < 12 ? new makeIntendedDate(activeMonth) : new Date();
    var planner = makeDatePlanner(dayList ? dayList : [], intendedMonth.getFullYear(), intendedMonth.getMonth());

    return planner;
}

function DrawCalenderPlanner(planner, year, month, init, whereAfterId) {
    var signature = whereAfterId;
    var colorMap = init.colorMap;
    var currentIntendedMonth = { year: year, month: month - 1 };
    var makeIntendedDate = function (d) { return new Date(d.year, d.month, 1); };
    var escapeForEmbeddedHTML = function (inp) {
        var htmlEscapes = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#x27;',
            "\r": '',
            "\n": ''
        };
        return ('' + inp).replace(/[&<>"'\r\n]/gm, function (match) { return htmlEscapes[match]; });
    };

    var drawCurrentPlanner = function (fullPlanner, currentIntendedMonth) {
        var planner = MakeMonthlyPlanner(fullPlanner, currentIntendedMonth);
        var dayNamesShort = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
        var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var firstDate = new Date(planner[0].date);
        firstDate.setMonth(firstDate.getMonth() + 1, 1);
        try {
            dayNamesShort = $.datepicker._defaults.dayNamesShort;
            monthNames = $.datepicker._defaults.monthNamesShort && $.datepicker._defaults.monthNamesShort.length > 0 && $.datepicker._defaults.monthNamesShort[0].length > 2 ? $.datepicker._defaults.monthNamesShort : $.datepicker._defaults.monthNames;
        } catch (er) {/**/}
        var mySomeFunc = function (a, c) { if (Array.prototype.some) return a.some(c); else return $.grep(a, c).length > 0; };
        var currentMonthIdx = currentIntendedMonth.month;
        var currMonth = makeIntendedDate(currentIntendedMonth);
        var currYear = currMonth.getFullYear();
        var priorMonth = currentMonthIdx > 0 ? new Date(currYear, currentMonthIdx - 1, 1) : new Date(currYear - 1, 11, 1);
        var nextMonth = currentMonthIdx < 11 ? new Date(currYear, currentMonthIdx + 1, 1) : new Date(currYear + 1, 0, 1);
        var hasPriorMonthData = fullPlanner && mySomeFunc(fullPlanner, function (e, i) { try { var d = new Date(Date.parse(e.date.replace(/\./g, "/"))); return d < currMonth; } catch (er) { return false; } });
        var hasNextMonthData = fullPlanner && mySomeFunc(fullPlanner, function (e, i) { try { var d = new Date(Date.parse(e.date.replace(/\./g, "/"))); return d >= nextMonth; } catch (er) { return false; } });
        var currentMonthName = monthNames[currentMonthIdx];
        var priorMonthName = hasPriorMonthData ? monthNames[priorMonth.getMonth()] : '';
        var nextMonthName = hasNextMonthData ? monthNames[nextMonth.getMonth()] : '';
        var calendarTitle = "<div class='responsive-calendar-date'><div class='date'><input type='hidden' readonly class='curr-month'/><span class='prior-month'>" + priorMonthName + "</span><span>" + currentMonthName + " " + currMonth.getFullYear() + "</span><span class='next-month'>" + nextMonthName + "</span></div></div>";
        var calendarHeader = $.map(planner.slice(0, 7),
        function (e, i) {
            return "<div class='dayname header" + (i === 0 ? " first-child" : "") + "'><span>" + dayNamesShort[e.date.getDay()] + "</span></div>";
        }
        ).join('');
        var calendarDayHeader = "<div class='calendar-day-headers'>" + calendarHeader + "<div class='clear'></div>" + "</div>";
        var calendar = $.map(planner,
            function (e, i) {
                var color = e && e.obj ? (colorMap[e.obj.color] || "") : "";
                var title = e && e.obj && e.obj.content && $.trim(e.obj.content) ? escapeForEmbeddedHTML(e.obj.content) : "";
                var url = e && e.obj && e.obj.url && $.trim(e.obj.url) ? escapeForEmbeddedHTML(e.obj.url) : "";
                var subTitle = e && e.obj && e.obj.subTitle && $.trim(e.obj.subTitle) ? escapeForEmbeddedHTML(e.obj.subTitle) : "";
                var imgOver = e && e.obj && e.obj.imgOver && $.trim(e.obj.imgOver) ? escapeForEmbeddedHTML(e.obj.imgOver) : "";
                var isPopupUrl = url.match(/^file|mail|http|java/i);
                var isJsUrl = url.match(/^javascript\:/i);
                var entry = "<div class='day " + (i % 7 === 0 ? "first-child" : "")
                    + (!e || e.curr ? " active " : " disabled ") + (e && e.obj ? " " + color : "")
                    + "' style='background-image:" + (imgOver === "" ? "none" : ("url(" + imgOver + ")")) + "' >"
                    + "<div class='day-head'>"
                    + "<span>" + (e && e.date ? e.date.getDate() : "") + "</span>"
                    + "</div>"
                    + "<div class='day-head-right'>"
                    + "<span>" + (e && e.obj ? (e.obj.subTitle && $.trim(e.obj.subTitle) ? subTitle : "") : "") + "</span>"
                    + "</div>"
                    + "<div class='day-text'>"
                    + (e && e.obj && e.obj.url && $.trim(e.obj.url)
                        ? "<a href='" + (isJsUrl ? "#" : encodeURI(url)) + "'" +
                        (isPopupUrl && !isJsUrl
                            ? (" target='_blank' title='" + url + "'")
                            : (" onclick='" +
                                (isJsUrl
                                    ? encodeURI(url.replace(/^javascript\:/i, ''))
                                    : escapeForEmbeddedHTML("SearchLink('" + (url) + "', '', '','')"))
                                + ";return false;' "))
                        + ">"
                        : "")
                    + "<span>" + (e && e.obj ? (e.obj.content && $.trim(e.obj.content) ? title : (e.obj.url && $.trim(e.obj.url) ? ".." : "")) : "") + "</span>"
                    + (e && e.obj && e.obj.url && $.trim(e.obj.url) ? "</a>" : "")
                    + "</div>"
                    + "</div>";
                return entry;
            }
        );
        var html = calendar.join('');
        var fullCalendar = "<div class='responsive-calendar'>" + calendarTitle + "<div class='responsive-calendar-content'>" + calendarDayHeader + "<div class='calendar-day-content'>" + html + "</div>" + "</div>";
        var myContainer = $('#' + whereAfterId).parent();
        $('div.responsive-calendar', $('#' + whereAfterId).parent()).remove();
        $('#' + whereAfterId).parent().append(fullCalendar);
        $('body').append("<input type='hidden' readonly id='curr-month_" + signature + "'/>");
        $('#curr-month_' + signature).val(currMonth.toDateString());
        if (hasPriorMonthData && myContainer) $('div.responsive-calendar-date  div.date span.prior-month', myContainer).click(function (a, b) {
            currentIntendedMonth.year = priorMonth.getFullYear(); currentIntendedMonth.month = priorMonth.getMonth(); drawCurrentPlanner(fullPlanner, currentIntendedMonth);
        }).css({ 'cursor': 'pointer' });
        if (hasNextMonthData && myContainer) $('div.responsive-calendar-date  div.date span.next-month', myContainer).click(function (a, b) {
            currentIntendedMonth.year = nextMonth.getFullYear(); currentIntendedMonth.month = nextMonth.getMonth(); drawCurrentPlanner(fullPlanner, currentIntendedMonth);
        }).css({ 'cursor': 'pointer' });
    };
    try {
        var currentSelectedMonth = new Date($('#curr-month_' + signature).val());
        if (planner.resetYear && planner.resetMonth) {
            currentIntendedMonth.year = planner.resetYear;
            currentIntendedMonth.month = planner.resetMonth - 1;
        }
        else if (!isNaN(currentSelectedMonth)) {
            currentIntendedMonth.year = currentSelectedMonth.getFullYear();
            currentIntendedMonth.month = currentSelectedMonth.getMonth();
        }
    } catch (e) {/**/}
    drawCurrentPlanner(planner.data, currentIntendedMonth);
}

function ShowCalenderPlanner(context, elementId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AutoComplete.aspx/RptGetCalender",
        data: $.toJSON({ contextStr: $.toJSON(context) }),
        error: function (xhr, textStatus, errorThrown) {
            //$(input).removeClass("wait");
            //response([]);
            var x = "";
        },

        success: function (result, textStatus, xhr) {
            //$(input).removeClass("wait");
            var dayList = result.d.data;
            var title = context.title || result.d.rptTitle;
        }
    });
}

function AppendSingleTd() {
    var tdLength = $('.GrdEdtTmp > td').length;
    if (tdLength > 1) {
        var $span = $(".GrdEdtTmp > td");
        $span.replaceWith(function () { return $('<div/>', { 'class': 'edtTd', 'html': this.innerHTML }); });
        $('.GrdEdtTmp').wrapInner('<td colspan= "' + tdLength + '"></td>');
    }
    $('.GrdEdtLabelText').show();
    $('.GrdHead .HideObjOnTablet').removeClass(' ShowObjHeader');
}

function AppendOrigTd() {
    $('.GrdEdtTmp > td[colspan]').contents().unwrap();
    var $span = $('.GrdEdtTmp > .edtTd');
    $span.replaceWith(function () { return $('<td/>', { html: this.innerHTML }); });
}

function IsMobile() {
    return (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i).test(navigator.userAgent);
}

//remove iframe and close the dialog box
function closeParentDlg() {
    setTimeout(function () { $('#searchLinkDlg').find('#searchLinkIframe').remove(); $('#searchLinkDlg').dialog('destroy'); }, 0);
}

function LayoutDashboard(dashboard, afterEleId, title) {
    var style = "display:inline-block; padding: 5px; background-color: skyblue; margin: 5px; color: #555; font-size: 16px; vertical-align: top;";
    var wrapsq = function (x) { return "'" + x + "'"; };
    var xmlEncode = function (x) { return x.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&apos;'); };
    var a = "<div>"
        + "<div>" + xmlEncode(title || "") + "</div>"
        + $(dashboard).map(function (i, e) {
            var x = "<div style=" + wrapsq(style) + ">" + "<div>" + xmlEncode(e.title) + "</div>" + "<iframe id=" + wrapsq(e.id) + " src=" + wrapsq(e.url) + " frameboder='0'" + " style=" + wrapsq(e.style) + ">" + "</iframe>" + "</div>";
            return x;
        }).toArray().join("")
        + "</div>";
    $('#' + afterEleId).after(a);
}

function ApplyMultiSelect(e) {
    var original = $(e);
    var searchable = $(e).attr('searchable') || true;
    if (searchable) {
        var selectedKeys = ($('#' + $(e).attr('id') + 'Hidden').val() || "").trim().replace(/\)|\(/g, "");
        var term = "**" + "(" + selectedKeys + ")";
        if (selectedKeys) {
            $('option', original).remove();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: $(e).attr('ac_url'),
                data: $.toJSON({ query: term, contextKey: $.toJSON({ contextKey: $(e).attr('ac_context'), refColVal: null, pMKeyVal: null, refColValIsList: "N" }), topN: 0 }),
                error: function (xhr, textStatus, errorThrown) {
                    _ApplyMultiSelect(e);
                },
                success: function (result, textStatus, xhr) {
                    $('option', original).remove();
                    $(result.d.data).each(function (i, e) {
                        var opt = $('<option />', {
                            value: e.key,
                            text: e.label,
                            selected: true
                        });
                        opt.appendTo($(original));
                    });
                    _ApplyMultiSelect(e);
                }
            });
        }
        else {
            _ApplyMultiSelect(e);
        }
    } else {
        _ApplyMultiSelect(e);
    }
}

function _ApplyMultiSelect(e) {
    var original = $(e);
    var intendedHeight = $(e).css('height').replace(/px/, '');
    var onchange = $(e).attr('onchange');
    var lines = $(e).attr('size');
    var disabled = $(e).attr('disabled') || $(e).hasClass('aspNetDisabled');
    var searchable = $(e).attr('searchable') !== undefined;
    var inPlace = !searchable &&  ($(e).attr('inPlace') !== undefined || lines > 1);
    var refColCID = $(e).attr('refColCID');
    var pMKeyColID = $(e).attr('pMKeyColID');
    var lineHeight = 13 * 2;
    var lastSelected = [null];
    //$(e).parent().height((inPlace ? lines : 1) * (lineHeight + 2));
    //if (!searchable) $(e).parent().height(intendedHeight - 4);
    var currentWidth = $(e).outerWidth();
    var optionCount = 0;
    var selectedCount = 0;
    var initSelectedKey = {};
    var initSelectEmptyEle = null;
    var hasEmptyEle = false;
    var nothingChanged = true;
    var selectedKey = {};
    var allSelected = true;
    var selectedText = $(e).find('option').map(
        function (i, e) {
            if (this.value === '') {
                if (this.selected) initSelectEmptyEle = this;
                /* remove empty choice(used as unselect)            
                */
                if (searchable || disabled || document) {
                    this.selected = false;
                }
                hasEmptyEle = true;
            }
            optionCount = optionCount + 1;
            if (this.selected) {
                selectedCount = selectedCount + 1;
                selectedKey[this.value] = this.innerHTML;
                initSelectedKey[this.value] = this.innerHTML;
                return this.innerHTML;
            } else {
                if (this.value !== '') allSelected = false;
                return null;
            }
        }
        ).toArray().join(',');

    if (disabled && selectedText === '') $(e).attr('disabled', '');
    var recentGroup = $("<optgroup show label='" + "Recent" + "'/>");
    var searchGroup = $("<optgroup label='" + "Match" + "'/>");

    var is_iOS = window.navigator.userAgent.match(/(iPod|iPhone|iPad)/) && !window.navigator.userAgent.match(/(IEMobile)/);
    var isTouch = (('ontouchstart' in window) || (navigator.msMaxTouchPoints > 0));
    //if (inPlace && !is_iOS) return; // do nothing for in place except iOS when multiselect listbox doesn't work
    //if (inPlace && is_iOS) {
    //    $('<optgroup></optgroup>').prepend($e); // another fix for iOS safari
    //    return;
    //}

    if (!hasEmptyEle && !disabled && !document) {
        $("<option value='' />").prependTo($(e));
    }

    if (searchable) {
        recentGroup.prependTo($(e));
        searchGroup.prependTo($(e));
        $('option', $(e)).appendTo(recentGroup);
    }
    if (onchange) {
        $(e).attr('onchange', ''); // suppress orginal onchange event(usually postback)
    }
    var moveToGroup = function (item, g) {
        var appendToEnd = true;
        $('option',g).each(function(i,e)
        {
            if (e.value === item[0].value) return appendToEnd = false;
            else if (e.innerHTML > item[0].innerHTML) {
                item.insertBefore(e);
                return appendToEnd = false;
            }
        });
        if (appendToEnd) item.appendTo(g);
    };
    var showHeader = (!disabled && (searchable || (lines >= 8 && optionCount >= 15 && false)));
    var headerHeight = lineHeight + 4;
    var select = $(e).multiselect(
        {
            hideButton: inPlace,
            hasEmptyElem: hasEmptyEle,
            minWidth: 0,
            header: showHeader,
            position: inPlace ? null : {
                my: 'left top',
                at: 'left bottom'

                // only include the "of" property if you want to position
                // the menu against an element other than the button.
                // multiselect automatically sets "of" unless you explictly
                // pass in a value.
            },
            searchable: searchable,
            noneSelectedText: disabled ? selectedText : '',
            checkAllText : '',
            uncheckAllText: '',
            inPlace: inPlace,
            disabled: disabled,
            selectedList: !inPlace && disabled ? 3 : 10,
            trimSelectedList: true,
            widgetHeight: inPlace ? intendedHeight - 4 : null,
            //height: inPlace || true ? lines * lineHeight : null,
            //height: inPlace ? intendedHeight - (showHeader ? headerHeight : 0) - 11 : lines * lineHeight,
            height: inPlace ? intendedHeight - 6 - (showHeader ? headerHeight : 0) : 200,
            appendTo: inPlace ? $(e).parent() : $(e).parent(),
            buttonWidth: $(e).css('max-width') !== 'none' ? $(e).css('max-width') : '100%',
            menuWidth: $(e).css('max-width') !== 'none' ? $(e).css('max-width') : (inPlace ? '100%' : null),
            autoOpen: ($(e).parent().is(':visible')) && inPlace,
            beforeopen: function (x) {
                if (!(inPlace)) {
                    var button = $(original).multiselect('getButton');
                    var menu = $(original).multiselect('getMenu');
                    var width = button.outerWidth();
                    menu.outerWidth(width);
                }
                return true;
            },
            beforeclose: function (x) {
                if (initSelectEmptyEle && nothingChanged) {
                    $(initSelectEmptyEle)[0].selected = true;
                }
                return !(inPlace) || (!(inPlace) && !$(this).parent().is(':visible'));
            },
            close: function (x) {
                var selectedItems = $(e).find('option').map(
                        function (i, e) {
                            if (this.selected) {
                                return "'" + this.value + "'";
                            } else {
                                return null;
                            }
                        }
                        ).toArray().join(',');
                $('#' + $(e).attr('id') + 'Hidden').val(selectedItems);
                if (onchange) {
                    var currentSelected = {};
                    var selectionChanged = false;
                    $('option', original).each(function (i, e) { if (this.selected) currentSelected[this.value] = this.innerHTML; });
                    $.each(initSelectedKey, function (k, v) { if (k && !currentSelected[k]) { selectionChanged = true; return false; } });
                    $.each(currentSelected, function (k, v) { if (k && !initSelectedKey[k]) { selectionChanged = true; return false; } });
                    if (selectionChanged) setTimeout(function() { eval(onchange);},0);
                }
            },
            click: function (event, ui) {
                if (disabled) return false;
                nothingChanged = false;
                if (searchable) {
                    if (isTouch) $('input[type="search"]', $(e).parent()).focus();
                    if (ui.value === '') {
                        ui.checked = false;
                        setTimeout(function () { $(e).multiselect().multiselectfilter('reload'); }, 0);
                        return false;
                    }
                    else
                    {
                        if (hasEmptyEle && initSelectEmptyEle) $(initSelectEmptyEle)[0].selected = false;
                        var found = false;
                        var more = false;
                        var noMore = false;
                        $('option', searchGroup).each(function (i, e) {
                            if (e.value === ui.value) {
                                more = $(e).attr('isMore') !== undefined;
                                noMore = $(e).attr('isNoMore') !== undefined;
                                if (!more && !noMore) {
                                    setTimeout(function () {
                                        $(e).attr('selected', 'selected');
                                        moveToGroup($(e), recentGroup);
                                        $(select).multiselect('refresh');
                                    }, 0);
                                    found = true;
                                }
                                return false;
                            }
                        }
                        );
                        if (!more && !noMore && !found && !ui.checked) {
                            setTimeout(function () {
                                $(select).multiselect('refresh');
                            }, 0);
                        }
                        if (more) {
                            $(e).multiselect().multiselectfilter('reload');
                        }
                        return !(more || noMore);
                    }
                }
                else {
                    var needRefresh = false;
                    var changed = false;
                    if (ui.value === '') {
                        $('option', original).each(function (i, e) {
                            e.selected = e.value === '' ? ui.checked : !ui.checked;
                            if (e.value !== '') changed = true, needRefresh = true;
                        });
                    }
                    else {
                        if (ui.checked) selectedKey[ui.value] = ui.text;
                        $('option', original).each(function (i, e) {
                            if (e.value === '' || (!document && !ui.clickOnInput && e.value !== ui.value)) {
                                needRefresh = true;
                                e.selected = false;
                            }
                        });
                    }
                    if (needRefresh) setTimeout(function(){$(select).multiselect('refresh');},0);
                    if (onchange && inPlace) {
                        setTimeout(function () { eval(onchange); }, 0);
                    }
                }
                if (ui.value !== '') lastSelected[0] = ui.value;
            },
            
            checkAll: function (x) {
                if (disabled) return false;
                nothingChanged = false;
                if (searchable) {
                    if (isTouch) $('input[type="search"]', $(e).parent()).focus();
                    $('option', $(e)).each(function (i, e) {
                        if ($(e).attr('isMore') !== undefined || $(e).attr('isNoMore') !== undefined) {
                            $(e).attr('selected', null);
                        }
                        else {
                            if (e.selected) {
                                moveToGroup($(e), recentGroup);
                                selectedKey[e.value] = e.innerHTML;
                            }
                        }
                    });
                    //setTimeout(function () { $(e).multiselect().multiselectfilter('reload'); }, 0);
                    setTimeout(function () {
                        $(select).multiselect('refresh');
                    }, 0);
                }
                else {
                    selectedKey[e.value] = e.innerHTML;
                    if (onchange && inPlace && !allSelected) {
                        setTimeout(function () { eval(onchange); }, 0);
                    }
                }
            },
            uncheckAll: function (x) {
                if (disabled) return false;
                nothingChanged = false;
                if (searchable) {
                    if (isTouch) $('input[type="search"]', $(e).parent()).focus();
                }
                if (onchange && inPlace) {
                    setTimeout(function () { eval(onchange); }, 0);
                }
            },
            beforeoptgrouptoggle: function (event, ui) {
                var target = event.srcElement.tagName;
                if (target === "INPUT") {
                    if ($(recentGroup)[0].label === ui.label) {
                        $('option', recentGroup).each(function (i, e) {
                            e.selected = event.srcElement.checked;
                        });
                    } else {
                        $('input[type="search"]', $(e).parent()).focus();
                        $('option', $(searchGroup)).each(function (i, e) {
                            if ($(e).attr('isMore') !== undefined || $(e).attr('isNoMore') !== undefined) {
                                $(e).attr('selected', null);
                            }
                            else {
                                e.selected = true;
                                moveToGroup($(e), recentGroup);
                                selectedKey[e.value] = e.innerHTML;
                            }
                        });

                    }
                    setTimeout(function () {
                        $(select).multiselect('refresh');
                    }, 0);
                    return false;
                }

                ui.opt.find('span.ui-icon').each(function (i, e) {
                    if ($(e).hasClass('ui-icon-triangle-1-n'))
                    {
                        $(e).removeClass('ui-icon-triangle-1-n');
                        $(e).addClass('ui-icon-triangle-1-s');
                    }
                    else 
                    {
                        $(e).removeClass('ui-icon-triangle-1-s');
                        $(e).addClass('ui-icon-triangle-1-n');
                    }
                });
                ui.opt.closest('li').nextAll('li').each(function (i, e)
                {
                    if ($(e).hasClass('ui-multiselect-optgroup-label')) return false;
                    else 
                    {
                        if ($(e).is(':visible')) $(e).hide();
                        else $(e).show();
                    }
                });
                return false;
            }

        });

    if (!inPlace) {
        if (initSelectEmptyEle && !searchable && nothingChanged) $(initSelectEmptyEle)[0].selected = true;
        $(e).multiselect('getButton').css('max-width', currentWidth);
    }
    else if ($(e)[0].style.width) {
        $(e).multiselect('getMenu').css('width', currentWidth);
    }
    $(e).multiselect('getMenu').prop('title', $(e).prop('title'));

    $(window).resize(function () {
        if (!inPlace && $(e).multiselect('isOpen')) {
            $(e).multiselect('close');
            $(e).multiselect('open');
            if (searchable) $('input[type="search"]', $(e).parent()).focus();
        }
    });
    var cache = {};
    if (searchable) {
        var refEle = undefined;
        $(e).multiselect().multiselectfilter({
            delay: 150,
            label: '',
            placeholder: '',
            source: function (term, response) {
                var refColVal = null;
                var pMKeyVal = null;
                var refColValIsList = "N";
                if (refColCID) {
                    // it is possible that the ref column is just textbox or dropdownlist
                    refEle = $('#' + refColCID);
                    if (refEle.length === 0) {
                        refColVal = $('#' + refColCID + "_KeyId").val();
                    }
                    else {
                        refColVal = refEle.val();
                        try {
                            // handle multiple select
                            if ($(refEle).prop("tagName") === "SELECT") refColValIsList = "Y";
                        } catch (e) {/**/}
                    }
                }
                // handle primary key field reference 2012.11.13 gary
                if (pMKeyColID) {
                    // it is possible that the ref column is just textbox or dropdownlist
                    refEle = $('#' + pMKeyColID);
                    if (refEle.length === 0) {
                        pMKeyVal = $('#' + pMKeyColID + "_KeyId").val();
                    }
                    else {
                        pMKeyVal = refEle.val();
                    }
                }
                var batchSize = 50;
                if (!cache[term]) {
                    cache[term] = { topN: batchSize };
                }
                else {
                    cache[term].topN = cache[term].topN + batchSize;
                }
                var topN = cache[term].topN;

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: $(e).attr('ac_url'),
                    data: $.toJSON({ query: term, contextKey: $.toJSON({ contextKey: $(e).attr('ac_context'), refColVal: refColVal, pMKeyVal: pMKeyVal, refColValIsList: refColValIsList }), topN: topN }),
                    error: function (xhr, textStatus, errorThrown) {
                        response([]);
                    },

                    success: function (result, textStatus, xhr) {
                        $('option', searchGroup).each(function (i, e) {
                            var item = $(e);
                            var appendToRecent = true;
                            if (e.selected) {
                                if ($('option', recentGroup).length > 0) {
                                    var x = $('option', recentGroup).each(function (i, e) {
                                        if (e.value === item[0].value) return appendToRecent = false;
                                        else if (e.innerHTML > item[0].innerHTML) {
                                            item.insertBefore(e);
                                            return appendToRecent = false;
                                        }
                                    });
                                }
                                if (appendToRecent) item.appendTo(recentGroup);
                            }
                            else item.remove();
                        });
                        $('option', recentGroup).each(function (i, e) {
                            var item = $(e);
                            if (!selectedKey[e.value] && !e.selected) $(e).remove();
                        });
                        if (term || document) {
                            $(result.d.data).each(function (i, e) {
                                var opt = $('<option />', {
                                    value: e.key,
                                    text: e.label
                                });
                                if ($('option[value=' + e.key + ']', recentGroup).length === 0) opt.appendTo(searchGroup);
                            }
                            );
                            if (result.d.total >= result.d.topN) {
                                $('<option />', {
                                    value: ' ',
                                    text: '...'
                                }).attr('isMore','').appendTo(searchGroup);
                            }
                            if ($('option', searchGroup).length === 0) {
                                $('<option />', {
                                    value: ' ',
                                    text: 'No Match'
                                }).attr('isNoMore', '').appendTo(searchGroup);
                            }
                        }
                        else {/**/
                        }
                        $(e).multiselect('refresh');
                        response([]);
                    }
                });

            }
        });
        if (hasEmptyEle && initSelectEmptyEle) {
            // must kick start a close event in case there is no open then a straight postback that trigger change event
            $(e).multiselect('close');
        }
    }
}

function CloneScript(CloneContent) {

    var newDiv = $(document.createElement('div'));
    $(newDiv).html('<textarea id="cloneContent">' + CloneContent + '</textarea><div><span class=\'initialMsg\'>To clone selected screen, run this SQL script on the design database of your choice.</span></div><div><span class=\'successMsg\' style=\'display:none; color: green;\'>Script copied to clipboard</span></div>');
    $(newDiv).dialog({
        modal: true, width: '600',
        buttons: {
            'Copy': function () { copyToClipboard(document.getElementById("cloneContent")); }
        },
        close: function (event, ui) {
            $(newDiv).dialog('destroy');
        }
    });
}

function copyToClipboard(elem) {
    // create hidden text element, if it doesn't already exist
    var targetId = "_hiddenCopyText_";
    var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
    var origSelectionStart, origSelectionEnd;
    if (isInput) {
        // can just use the original source element for the selection and copy
        target = elem;
        origSelectionStart = elem.selectionStart;
        origSelectionEnd = elem.selectionEnd;
    } else {
        // must use a temporary form element for the selection and copy
        target = document.getElementById(targetId);
        if (!target) {
            var target = document.createElement("textarea");
            target.style.position = "absolute";
            target.style.left = "-9999px";
            target.style.top = "0";
            target.id = targetId;
            document.body.appendChild(target);
        }
        target.textContent = elem.textContent;
    }
    // select the content
    var currentFocus = document.activeElement;
    target.focus();
    target.setSelectionRange(0, target.value.length);

    // copy the selection
    var succeed;
    try {
        succeed = document.execCommand("copy");
        $('.successMsg').show();
        $('.initialMsg').hide();    
    } catch (e) {
        succeed = false;
    }
    // restore original focus
    if (currentFocus && typeof currentFocus.focus === "function") {
        currentFocus.focus();
    }

    if (isInput) {
        // restore prior selection
        elem.setSelectionRange(origSelectionStart, origSelectionEnd);
    } else {
        // clear temporary content
        target.textContent = "";
    }
    return succeed;
}

/* Obtain current login user information for Client Tier manipulation:
e.g.
getCurrentUsrInfo(function (usr) {var jumpToList = { '5': { label: 'go to google', url: 'https://www.google.com' }, '18': { label: 'Upgrade', url: 'showpage.aspx?csy=1&pg=16' } }, jumpTo = jumpToList[usr.UsrGroup]; if (jumpTo) $('.learnMoreBtn').attr('href',jumpTo.url).html(jumpTo.label)});});
*/
function getCurrentUsrInfo(handler) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "AdminWs.asmx/GetCurrentUsrInfo",
        data: $.toJSON({ Scope: 'basic' }),
        error: function (xhr, textStatus, errorThrown) {
            /**/
        },
        success: function (mr, textStatus, xhr) {
            var ret = mr.d;
            if (ret && handler) {
                handler(ret);
            }
            else if (handler) {
                handler({});
            }
        }
    });
}

function switchStatus(ele) {
    if ($(ele).parent().find('.DocPanel').is(':visible')) {
        $(ele).parent().find('.DocPanel').hide();
        $(ele).parent().find('.DocView').show();
        $(ele).parent().find('.BtnImgDel').hide();
    } else {
        $(ele).parent().find('.DocPanel').show();
        $(ele).parent().find('.DocView').hide();
        $(ele).parent().find('.BtnImgDel').show();
    }
}

function refreshUploadCallback(src, imgEleId) {
    var btn = src;
    return function (data, txtStatus, jqXHR) {
        $('#' + imgEleId).attr('src', data.imgUrl);
        switchStatus('#' + imgEleId);
    };
}

function AutoUpload(e, evt) {
    $('input[type="submit"]', $(e).closest('table')).click();
}

// For future reference:
//function refreshMultiDocCallback(src, fileEleId, uploadBtnId) {
//    var btn = src;
//    return function (data, txtStatus, jqXHR) {
//        $('#' + 'Profile-SaveBtn').click();
//        //$('#' + uploadBtnId).click();;
//    };
//}

function sendFile(file, url, success, failure) {
    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        type: 'post',
        url: url,
        data: formData,
        success: function (data, txtStatus, jqXHR) {
            if (txtStatus !== 'error' && typeof (success) === "function") {
                success(data, txtStatus, jqXHR);
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}

function parsedUrl(url) {
    var parser = document.createElement("a");
    parser.href = url;
    var o = {};
    // IE 8 and 9 dont load the attributes "protocol" and "host" in case the source URL
    // is just a pathname, that is, "/example" and not "http://domain.com/example".
    parser.href = parser.href;

    // IE 7 and 6 wont load "protocol" and "host" even with the above workaround,
    // so we take the protocol/host from window.location and place them manually
    if (parser.host === "") {
        var newProtocolAndHost = window.location.protocol + "//" + window.location.host;
        if (url.charAt(1) === "/") {
            parser.href = newProtocolAndHost + url;
        } else {
            // the regex gets everything up to the last "/"
            // /path/takesEverythingUpToAndIncludingTheLastForwardSlash/thisIsIgnored
            // "/" is inserted before because IE takes it of from pathname
            var currentFolder = ("/" + parser.pathname).match(/.*\//)[0];
            parser.href = newProtocolAndHost + currentFolder + url;
        }
    }

    // copies all the properties to this object
    var properties = ['host', 'hostname', 'hash', 'href', 'port', 'protocol', 'search'];
    for (var i = 0, n = properties.length; i < n; i++) {
        o[properties[i]] = parser[properties[i]];
    }

    // pathname is special because IE takes the "/" of the starting of pathname
    o.pathname = (parser.pathname.charAt(0) !== "/" ? "/" : "") + parser.pathname;
    return o;
}

function wordToByteArray(word, length) {
    var ba = [], i, xFF = 0xFF;
    if (length > 0)
        ba.push(word >>> 24);
    if (length > 1)
        ba.push((word >>> 16) & xFF);
    if (length > 2)
        ba.push((word >>> 8) & xFF);
    if (length > 3)
        ba.push(word & xFF);
    return ba;
}

function wordArrayToByteArray(wordArray, length) {
    if (wordArray.hasOwnProperty("sigBytes") && wordArray.hasOwnProperty("words")) {
        length = wordArray.sigBytes;
        wordArray = wordArray.words;
    }

    var result = [],
        bytes,
        i = 0;
    while (length > 0) {
        bytes = wordToByteArray(wordArray[i], Math.min(4, length));
        length -= bytes.length;
        result.push(bytes);
        i++;
    }
    return [].concat.apply([], result);
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return btoa(binary);
}

function makeNameFromNS(appDomainUrl, name) {
    var appNS = (parsedUrl(appDomainUrl) || {}).pathname || '/';
    return ((appNS.toUpperCase().replace(/^\//, '') + '_') || '') + name;
}

function eraseUserHandle(appDomainUrl) {
    localStorage.removeItem(makeNameFromNS(appDomainUrl, "user_handle"));
}

function rememberUserHandle(appDomainUrl, userIdentity) {
    var h = sjcl.hash.sha256.hash(userIdentity);
    var v = arrayBufferToBase64(wordArrayToByteArray(h,h.length*4)).replace(/=/g, "_");
    localStorage.setItem(makeNameFromNS(appDomainUrl, "user_handle"), v.replace(/=/g, "_"));
}

function getUserHandle(appDomainUrl) {
    var x = localStorage[makeNameFromNS(appDomainUrl, "user_handle")];
    return x;
}

function getTokenName(appDomainUrl, name) {
    var x = btoa(sjcl.hash.sha256.hash((appDomainUrl || "").toLowerCase() + name + (getUserHandle(appDomainUrl) || "") + (myMachine || "")));
    return x;
}

function setCookie(name, value, days, path) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    var href = window.location.href;
    document.cookie = name + "=" + (value || "") + expires + "; path=" + (path || "/") + ";secure";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

//function MouseOverEffect(e, i) { e.src = i; }

//Retained for future reference:
//function ClientSideBarChart(points, xlabel, ylabel, title, elementid) {
//    for (var i = 0; i < points.length; i++) {
//        points[i][1] = parseFloat(points[i][1]);
//    }
//    var plot1 = $.jqplot(elementid, [points], {
//        title: title,
//        series: [{ renderer: $.jqplot.BarRenderer }],
//        axes: {
//            xaxis: {
//                renderer: $.jqplot.CategoryAxisRenderer,
//                label: xlabel,
//                labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
//                tickRenderer: $.jqplot.CanvasAxisTickRenderer,
//                tickOptions: {
//                    angle: -40,
//                    fontSize: '9pt',
//                }
//            },
//            yaxis: {
//                label: ylabel,
//                labelRenderer: $.jqplot.CanvasAxisLabelRenderer
//            }
//        }
//    });
//}

/* Obsolete 2011.02.01:
function CenterAndShowElement(elem) {
var screenWidth = document.viewport.getWidth();
var screenHeight = document.viewport.getHeight();
var scrollOffsets = document.viewport.getScrollOffsets();
elem.style.display = '';
var updateProgressDivBounds = Sys.UI.DomElement.getBounds(elem);
var x = Math.round(screenWidth / 2) - Math.round(updateProgressDivBounds.width / 2);
var y = Math.round(screenHeight / 2) - Math.round(updateProgressDivBounds.height / 2);
Sys.UI.DomElement.setLocation(elem, x + scrollOffsets.left, y + scrollOffsets.top);
}

function RedirectToPage(event) {
var menuItem = event.element();
var links = menuItem.select('a');
links.each(function(item) {
window.location = item.href;
});
}

function SetupMenuJs(menu) {
var menuItems = menu.firstDescendant().childElements();
menuItems.each(function(item) {
item.observe('click', RedirectToPage);
item.addClassName('TrMenuItem');

//get sub menu if there is one..
var sub = $(item.id + 'Items');

if (sub !== null) {
SetupMenuJs(sub.firstDescendant());
}
});
}
*/