<%@ Control Language="c#" Inherits="RO.Web.AdmScreenCriModule" CodeFile="AdmScreenCriModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="ajwced" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" %>
<%@ Register TagPrefix="rcasp" NameSpace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<%@ Register TagPrefix="Module" TagName="Help" Src="HelpModule.ascx" %>
<script type="text/javascript" lang="javascript">
	initPageLoad=true;
	ServerDateFormat = { shortFormat: "<%= System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern %>" , longFormat: "<%= System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern %>"};
	if(IsMobile()){ var $body = jQuery('body');
	    $(document).live('mousedown', '.ui-button, input[type="text"], select, textarea', function(e) { var target = $( e.target );
	        if (target.is( '.ui-button, input[type="text"], select, textarea')) { $body.addClass('mobilekeyPop'); }
	        if((target.is( '.ui-button, select, textarea')||(e.target.tagName == 'INPUT' && e.target.type == 'text')) && !e.target.alreadyClicked){ target.on('focus', function(e) { $body.addClass('mobilekeyPop'); }); e.target.alreadyClicked = true; }
	    }).on('blur', 'input[type="text"], select, textarea', function(e) { $body.removeClass('mobilekeyPop'); });
	}
	window.matchMedia = window.matchMedia || function(x) { return {"matches":0}; };
      Sys.Application.add_load(function () { if (typeof(old_Page_ClientValidate)!='undefined' || typeof(Page_ClientValidate) =='undefined') return; try { old_Page_ClientValidate = Page_ClientValidate;} catch(e){}; Page_ClientValidate = function (g) {if (typeof(old_Page_ClientValidate) == 'function') {$.watermark.hideAll(); Page_BlockSubmit = false && !old_Page_ClientValidate(g);} else return true; if (Page_BlockSubmit) { ValidateThisModule(<%= this.ClientID %>); $('#<%=cValidSummary.ClientID%>:visible').hide();} ; if (Page_BlockSubmit && $('#<%=bPgDirty.ClientID%>').val() == 'Y') {$('#<%=bConfirm.ClientID%>').val('Y');}; return !Page_BlockSubmit;}});
	Sys.Application.add_load(function () { $('input[type=hidden]').each(function (i, e) { try { e.defaultValue = e.value; } catch (er) { } }); WatermarkInput(<%= this.ClientID %>,'<%= PanelUpd.ClientID %>');});
	Sys.Application.add_load(function () { var hlp={}; $('a.GrdHead').each(function(i,e){var ids = ($(this).attr('id')||'').split('_'); hlp[ids[ids.length-1].replace(/hl$/,'l')] = $(this).attr('title');});$('span.GrdTxtLb,a.GrdTxtLn,a.GrdBoxLn').each(function (i, e) {try { var style = ($(this).attr('style')||'').replace(/height/i, 'max-height'); var ids = ($(this).attr('id')||'').split('_'); $(this).attr('title', hlp[ids[ids.length-1]]).attr('style', style); } catch (e) { } }); });
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChkExpNow)
	Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ChkPgDirty)
	Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
	function GridUpload() { document.getElementById('<%=cGridUploadBtn.ClientID %>').click() }
	function GridEdit(cn) { document.getElementById('<%=bConfirm.ClientID %>').value = 'N'; colNam=cn; }
	function GridDelete() { document.getElementById('<%=bPgDirty.ClientID %>').value='Y'; ChkPgDirty(); document.getElementById('<%=bConfirm.ClientID %>').value='N';colNam='DeleteLink';}
	function GridCancel() { Page_Validators=[]; document.getElementById('<%=bConfirm.ClientID %>').value='N';}
	var xPos, yPos;
	function SaveScroll() { xPos = $(document).scrollLeft(); yPos = $(document).scrollTop(); }
	function RestoreScroll() { setTimeout(function() {$(document).scrollLeft(xPos);$(document).scrollTop(yPos);}, 0); }
	Sys.Application.add_load(function () {if(window.matchMedia("screen and (max-width: 1024px)").matches){if (window.matchMedia("screen and (max-width: 767px)").matches){AppendSingleTd();}else{$('.GrdEdtLabelText').hide();if($('.GrdEdtTmp').length > 0){$('.GrdHead .HideObjOnTablet').addClass(' ShowObjHeader');}}}else{ $('.GrdEdtLabelText').hide(); $('.GrdHead .HideObjOnTablet').removeClass(' ShowObjHeader');}});
	$(window).resize(function () { if(window.matchMedia("screen and (max-width: 1024px)").matches){ if (window.matchMedia("screen and (max-width: 767px)").matches){ AppendSingleTd();}else{$('.GrdEdtLabelText').hide();if($('.GrdEdtTmp').length > 0){$('.GrdHead .HideObjOnTablet').addClass(' ShowObjHeader');}AppendOrigTd();}} else {$('.GrdEdtLabelText').hide();$('.GrdHead .HideObjOnTablet').removeClass(' ShowObjHeader');}});
	function NoConfirm() {document.getElementById('<%=bConfirm.ClientID%>').value = 'N';}
	function ChkPgDirty()
	{
		var x = document.getElementById('<%=bPgDirty.ClientID%>');
		var y = document.getElementById('<%=cPgDirty.ClientID%>');
		if (y != null) {if (x != null && x.value == 'Y') {y.style.visibility = '';} else {y.style.visibility = 'hidden';}}
	}
	function initializeRequestHandler(sender, args) {if (!fConfirm2('<%=bPgDirty.ClientID%>','<%=bConfirm.ClientID%>','<%=aNam.ClientID%>','<%=aVal.ClientID%>')) {args.set_cancel(true);} else try {$.watermark.hideAll();} catch (e) { }}
	function beginRequestHandler(sender, e) { e.get_postBackElement().disabled=true; SaveScroll(); ShowProgress(); document.body.style.cursor='wait'; }
	function endRequestHandler() { initPageLoad=true; RestoreScroll(); HideProgress(); document.body.style.cursor='auto'; var v = $('#<%=cValidSummary.ClientID%>:visible'); if (v.length > 0 && typeof(Page_Validators) != 'undefined' && $(Page_Validators).length > 0) {ValidateThisModule(<%= this.ClientID %>);v.hide();}; }
	window.onbeforeunload = UnloadConfirm;
	function UnloadConfirm()
	{
		var xx = fConfirm('<%=bPgDirty.ClientID%>','<%=bConfirm.ClientID%>','<%=aNam.ClientID%>','<%=aVal.ClientID%>');
		window.saveerr = window.onerror; window.onerror = function() {return true;}
		setTimeout(function() {window.onerror = window.saveerr}, 10); return xx;
	}
    function ChkExpNow() {if (document.getElementById('<%=bExpNow.ClientID%>').value == 'Y') {window.location = 'Exp.aspx';}}
    Sys.Application.add_load(function () {
        jqTab();
        ApplyJQueryWidget('<%=aNam.ClientID%>', '<%=aVal.ClientID%>');
    });
    function openButtonSec() {
        if ($('.moreButtonSec').hasClass('hideMoreButtonSec')) { $('.moreButtonSec').removeClass('hideMoreButtonSec');} else { $('.moreButtonSec').addClass('hideMoreButtonSec');};
    }
    $(document).ready(function() {if ( $('.moreButtonSec > div').children().length > 0 ) {$('.MoreBtnCnt').removeClass('hideMoreButtonSec');} else {$('.MoreBtnCnt').addClass('hideMoreButtonSec');}});
    $(document).mouseup(function (e) {
        var container = $('.moreButtonSec');
        if ($(window).width() <= 1024) {
            if (!container.is(e.target) && container.has(e.target).length === 0 && !container.hasClass('hideMoreButtonSec')) { openButtonSec(); }
        }
    });
</script>
<asp:PlaceHolder ID="FstPHolder" runat="server" Visible="false" />
<asp:UpdatePanel ID="PanelTop" UpdateMode="Conditional" runat="server"><ContentTemplate>
<asp:ValidationSummary id="cValidSummary" CssClass="ValidSumm" EnableViewState="false" runat="server" />
<div id="AjaxSpinner" class="AjaxSpinner" style="display:none;">
	<div style="padding:10px;">
		<img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment..." runat="server" />
	</div>
</div>
<div class="r-table BannerGrp">
<div class="r-tr">
    <div class="r-td rc-1-5">
        <div class="BannerNam"><asp:label id="cTitleLabel" CssClass="screen-title" runat="server" /><input type="image" name="cDefaultFocus" id="cClientFocusButton" src="images/Help_x.jpg" onclick="return false;" style="visibility:hidden;" /></div>
    </div>
    <div class="r-td rc-6-12">
        <div class="BannerBtn">
        <asp:Panel id="cButPanel" runat="server">
            <div class="BtnTbl">
            <div><asp:Button id="cEditButton" onclick="cEditButton_Click" runat="server" CausesValidation="true" Visible="false" /></div>
            <div><asp:Button id="cSaveCloseButton" onclick="cSaveCloseButton_Click" runat="server" CausesValidation="true" Visible="false" /></div>
            <div><asp:Button id="cSaveButton" onclick="cSaveButton_Click" runat="server" Visible="<%# CanAct('S') && cSaveButton.Visible %>" /></div>
            <div><asp:Button id="cNewSaveButton" onclick="cNewSaveButton_Click" runat="server" Visible="<%# CanAct('S') && cNewSaveButton.Visible %>" /></div>
            <div><asp:Button id="cCopySaveButton" onclick="cCopySaveButton_Click" runat="server" Visible="<%# CanAct('S') && cCopySaveButton.Visible %>" /></div>
            <div><asp:Button id="cNewButton" onclick="cNewButton_Click" runat="server" CausesValidation="true" Visible="<%# CanAct('N') && cNewButton.Visible %>" /></div>
            <div><asp:image id="cPgDirty" Style="visibility:hidden;" ImageUrl="~/images/Xclaim.png" runat="server" /></div>
            <div class="moreButtonSec hideMoreButtonSec">
                <div><asp:Button id="cAuditButton" runat="server" CausesValidation="true" /></div>
                <div><asp:Button id="cDeleteButton" onclick="cDeleteButton_Click" runat="server" CausesValidation="true" Visible="<%# CanAct('D') && cDeleteButton.Visible %>" /></div>
                <div><asp:Button id="cUndoAllButton" onclick="cUndoAllButton_Click" runat="server"  CausesValidation="true" Visible="<%# CanAct('S') && cUndoAllButton.Visible %>" /></div>
                <div><asp:Button id="cCopyButton" onclick="cCopyButton_Click" runat="server" CausesValidation="true" Visible="<%# CanAct('N') && cCopyButton.Visible %>" /></div>
                <div><asp:Button id="cClearButton" onclick="cClearButton_Click" runat="server" CausesValidation="true" /></div>
                <div><asp:Button id="cExpRtfButton" onclick="cExpRtfButton_Click" runat="server" CausesValidation="true" /></div>
                <div><asp:Button id="cExpTxtButton" onclick="cExpTxtButton_Click" runat="server" CausesValidation="true" /></div>
                <div><asp:Button id="cPreviewButton" onclick="cPreviewButton_Click" runat="server" CausesValidation="true" Visible="false" /></div>
            </div>
            <div class="HelpMsgCnt"><Module:Help id="cHelpMsg" runat="server" /></div>
            <div class="MoreBtnCnt"><asp:Button ID="cMoreButton" OnClientClick="openButtonSec(); return false;" runat="server" /></div>
            <div style="clear:both;"></div>
        </div>
        </asp:Panel>
        </div>
    </div>
</div>
</div>
</ContentTemplate></asp:UpdatePanel>
<asp:UpdatePanel ID="PanelUpd" runat="server"><Triggers><asp:PostBackTrigger ControlID="cGridUploadBtn" /><asp:PostBackTrigger ControlID="cBrowseButton" /><asp:PostBackTrigger ControlID="cImportButton" /><asp:PostBackTrigger ControlID="cContinueButton" /></Triggers><ContentTemplate>
<asp:PlaceHolder ID="MidPHolder" runat="server" Visible="false" />
<asp:Panel id="cCriPanel" runat="server" wrap="false">
<fieldset class="criteria-grp"><legend>CRITERIA<span><asp:Button id="cClearCriButton" onclick="cClearCriButton_Click" runat="server" CausesValidation="true" /></span></legend>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-8">
        <div class="screen-criteria">
            <asp:PlaceHolder id="cCriteria" EnableViewState="true" runat="server" />
        </div>
    </div>
    <div class="r-td rc-9-12">
        <div class="screen-filter">
            <asp:Panel id="cFilter" runat="server" visible="false">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label id="cFilterLabel" CssClass="inp-lbl" runat="server" /></div>&nbsp;
                    <div class="r-td r-content"><asp:DropDownList id="cFilterId" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cFilterId_SelectedIndexChanged" DataValueField="ScreenFilterId" DataTextField="FilterName" /></div>
                </div>
            </div>
            </asp:Panel>
        </div>
    </div>
</div>
</div>
</fieldset>
</asp:Panel>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div><asp:label id="cGlobalFilter" cssclass="FiltText" runat="server" visible="false" /></div>
    </div>
</div>
</div>
<div class="r-table search-grp">
<div class="r-tr">
    <div class="r-td rc-1-8">
        <div id="cScreenSearch" class="screen-search" runat="server">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR r-labelM"><span class='autoCmptPref'>Search:</span></div>
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmScreenCri73List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmScreenCri73List_SelectedIndexChanged" OnTextChanged="cAdmScreenCri73List_TextChanged" OnDDFindClick="cAdmScreenCri73List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ScreenCriId104" DataTextField="ScreenCriId104Text" /></div>
                </div>
            </div>
        </div>
    </div>
    <div class="r-td rc-8-8">
        <asp:Label ID="searchCounter" CssClass="itemTotal" style='display:none' runat="server"></asp:Label>
    </div>
    <div class="r-td rc-9-12">
        <div id="cSystem" class="screen-system" runat="server" visible="false">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label id="cSystemLabel" CssClass="inp-lbl" runat="server" /></div>&nbsp;
                    <div class="r-td r-content r-sysid"><asp:DropDownList id="cSystemId" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cSystemId_SelectedIndexChanged" DataValueField="SystemId" DataTextField="SystemName" /></div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
<asp:Panel id="cTabFolder" runat="server">
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab36" runat="server"/>
<ul id="tabs">
    <li><a id="cTab36" href="#" class="current" name="Tab36" runat="server"></a></li>
    <li><a id="cTab108" href="#" name="Tab108" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab36" runat="server">
    <asp:UpdatePanel id="UpdPanel36" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cScreenCriId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenCriId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenCriId104P2" class="r-td r-content" runat="server"><asp:TextBox id="cScreenCriId104" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenId104" DataValueField="ScreenId104" DataTextField="ScreenId104Text" AutoPostBack="true" OnSelectedIndexChanged="cScreenId104_SelectedIndexChanged" OnTextChanged="cScreenId104_TextChanged" OnDDFindClick="cScreenId104_DDFindClick" runat="server" /><asp:RequiredFieldValidator id="cRFVScreenId104" ControlToValidate="cScreenId104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLabelCss104P1" class="r-td r-labelR" runat="server"><asp:Label id="cLabelCss104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLabelCss104P2" class="r-td r-content" runat="server"><asp:TextBox id="cLabelCss104" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cContentCss104P1" class="r-td r-labelR" runat="server"><asp:Label id="cContentCss104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cContentCss104P2" class="r-td r-content" runat="server"><asp:TextBox id="cContentCss104" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cColumnId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbColumnId104" DataValueField="ColumnId104" DataTextField="ColumnId104Text" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnId104" ControlToValidate="cColumnId104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cOperatorId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cOperatorId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cOperatorId104P2" class="r-td r-content" runat="server"><asp:DropDownList id="cOperatorId104" CssClass="inp-ddl" DataValueField="OperatorId104" DataTextField="OperatorId104Text" runat="server" /><asp:RequiredFieldValidator id="cRFVOperatorId104" ControlToValidate="cOperatorId104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDisplayModeId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cDisplayModeId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDisplayModeId104P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDisplayModeId104" CssClass="inp-ddl" DataValueField="DisplayModeId104" DataTextField="DisplayModeId104Text" runat="server" /><asp:RequiredFieldValidator id="cRFVDisplayModeId104" ControlToValidate="cDisplayModeId104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTabIndex104P1" class="r-td r-labelR" runat="server"><asp:Label id="cTabIndex104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTabIndex104P2" class="r-td r-content" runat="server"><asp:TextBox id="cTabIndex104" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVTabIndex104" ControlToValidate="cTabIndex104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRequiredValid104P1" class="r-td r-labelR" runat="server"><asp:Label id="cRequiredValid104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRequiredValid104P2" class="r-td r-content" runat="server"><asp:CheckBox id="cRequiredValid104" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cColumnJustify104P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnJustify104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnJustify104P2" class="r-td r-content" runat="server"><asp:DropDownList id="cColumnJustify104" CssClass="inp-ddl" DataValueField="ColumnJustify104" DataTextField="ColumnJustify104Text" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnJustify104" ControlToValidate="cColumnJustify104" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnSize104P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnSize104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnSize104P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnSize104" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRowSize104P1" class="r-td r-labelR" runat="server"><asp:Label id="cRowSize104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRowSize104P2" class="r-td r-content" runat="server"><asp:TextBox id="cRowSize104" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlKeyColumnId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlKeyColumnId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlKeyColumnId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlKeyColumnId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlKeyColumnId104" DataValueField="DdlKeyColumnId104" DataTextField="DdlKeyColumnId104Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlRefColumnId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlRefColumnId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlRefColumnId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlRefColumnId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlRefColumnId104" DataValueField="DdlRefColumnId104" DataTextField="DdlRefColumnId104Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlSrtColumnId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlSrtColumnId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlSrtColumnId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlSrtColumnId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlSrtColumnId104" DataValueField="DdlSrtColumnId104" DataTextField="DdlSrtColumnId104Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlFtrColumnId104P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlFtrColumnId104Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlFtrColumnId104P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlFtrColumnId104" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlFtrColumnId104" DataValueField="DdlFtrColumnId104" DataTextField="DdlFtrColumnId104Text" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab108" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel108" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
<div id="GridDiv" class="grid-container rg-1-12">
<asp:TextBox id="isMobile" CssClass="chkMobile" Text="notMobile" style="display:none;" runat="server" />
<div class="viewport rc-1-12">
<div class="button-container">
 <asp:Panel id="cNaviPanel" runat="server" visible="true">
		<div class="button-grp"><asp:Button id="cGridUploadBtn" Width="0px" style="display:none;" runat="server" /></div>
		<div class="button-grp"><asp:Button id="cInsRowButton" onclick="cInsRowButton_Click" runat="server" /></div>
		<div class="button-grp">
		    <div><asp:label ID="cGridFtrLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div class="grdFindFilter"><asp:DropDownList id="cFindFilter" CssClass="inp-ddl" runat="server" /></div>
		</div>
		<div class="button-grp">
		    <div><asp:label ID="cGridFndLabel" CssClass="inp-lbl grdFindLb" runat="server" /></div>
		    <div class="grdFind"><asp:TextBox id="cFind" CssClass="inp-txt" runat="server" /></div>
		    <div><asp:Button id="cFindButton" onclick="cFindButton_Click" runat="server" /></div>
		</div>
		<div class="button-grp">
		    <div class="btnPgGrp">
		        <div><asp:TextBox id="cPgSize" runat="server" CssClass="inp-ctr" width="25px" /></div>
		        <div><asp:Button id="cPgSizeButton" onclick="cPgSizeButton_Click" runat="server" /></div>
		        <div class="grdPaging">
		            <div><asp:Button id="cFirstButton" onclick="cFirstButton_Click" runat="server" /></div>
		            <div><asp:Button id="cPrevButton" onclick="cPrevButton_Click" runat="server" /></div>
		            <div><asp:TextBox id="cGoto" OnTextChanged="cGoto_TextChanged" AutoPostBack="true" CssClass="inp-ctr" width="25px" runat="server" /></div>
		            <div><asp:label id="cPageNoLabel" CssClass="inp-lbl" runat="server" /></div>
		            <div><asp:Button id="cNextButton" onclick="cNextButton_Click" runat="server" /></div>
		            <div><asp:Button id="cLastButton" CssClass="small blue button" onclick="cLastButton_Click" runat="server" /></div>
		        </div>
		        <div><asp:Button id="cShowImpButton" onclick="cShowImpButton_Click" runat="server" /></div>
		        <span id="grdCount" class="resultTotal" runat="server"></span>
		    </div>
		</div>
	</asp:Panel>
	<asp:Panel id="cImportPwdPanel" runat="server" visible="false">
		<div class="button-grp">
	        <div><asp:label ID="cImpPwdLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div><asp:TextBox TextMode="Password" id="cImportPwd" CssClass="PwdBox" width="250px" MaxLength="32" runat="server" /></div>
		    <div><asp:Button id="cContinueButton" onclick="cContinueButton_Click" runat="server" /></div>
		</div>
	</asp:Panel>
	<asp:Panel id="cImport" runat="server" visible="false">
		<div class="button-grp"><asp:FileUpload id="cBrowse" CssClass="inp-txt" runat="server" width="250px" /></div>
		<div class="button-grp"><asp:Button id="cBrowseButton" OnClick="cBrowseButton_Click" Width="0px" style="display:none;" runat="server" /><asp:TextBox id="cFNameO" text="" Width="0px" style="display:none;" runat="server" /><asp:TextBox id="cFName" text="" Width="0px" style="display:none;" runat="server" /></div>
		<div class="button-grp">
		    <div><asp:label ID="cGridWksLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div><asp:DropDownList id="cWorkSheet" CssClass="inp-ddl" runat="server" style="min-width:100px;" /></div>
		</div>
		<div class="button-grp">
		    <div><asp:label ID="cGridStrLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div>
		        <asp:TextBox id="cStartRow" CssClass="inp-ctr" text="2" runat="server" width="30px" />
		        <asp:ImageButton id="cSchemaImage" ImageUrl="~/images/Schema.gif" runat="server" />
		        <asp:Button id="cImportButton" onclick="cImportButton_Click" runat="server" />
		    </div>
		    <div><asp:Button id="cHideImpButton" onclick="cHideImpButton_Click" runat="server" CausesValidation="true" /></div>
		</div>
	</asp:Panel>
</div>
<div class="ListView">
	<asp:ListView id="cAdmScreenCriGrid" DataKeyNames="ScreenCriHlpId105" OnItemCommand="cAdmScreenCriGrid_OnItemCommand" OnSorting="cAdmScreenCriGrid_OnSorting" OnPreRender="cAdmScreenCriGrid_OnPreRender" OnPagePropertiesChanging="cAdmScreenCriGrid_OnPagePropertiesChanging" OnItemEditing="cAdmScreenCriGrid_OnItemEditing" OnItemCanceling="cAdmScreenCriGrid_OnItemCanceling" OnItemDeleting="cAdmScreenCriGrid_OnItemDeleting" OnItemDataBound="cAdmScreenCriGrid_OnItemDataBound" OnLayoutCreated="cAdmScreenCriGrid_OnLayoutCreated" OnItemUpdating="cAdmScreenCriGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmScreenCriGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:50px;text-align:right;' runat="server">
			<asp:LinkButton id="cScreenCriHlpId105hl" CssClass="GrdHead" onClick="cScreenCriHlpId105hl_Click" runat="server" /><asp:Image id="cScreenCriHlpId105hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
			<asp:LinkButton id="cCultureId105hl" CssClass="GrdHead" onClick="cCultureId105hl_Click" runat="server" /><asp:Image id="cCultureId105hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
			<asp:LinkButton id="cColumnHeader105hl" CssClass="GrdHead" onClick="cColumnHeader105hl_Click" runat="server" /><asp:Image id="cColumnHeader105hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmScreenCriGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:50px;text-align:right;' runat="server">
		    <asp:Label id="cScreenCriHlpId105fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
		    <asp:Label id="cCultureId105fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
		    <asp:Label id="cColumnHeader105fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmScreenCriGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:50px;text-align:right;' visible="<%# GridColumnVisible(16) %>" onclick='GridEdit("ScreenCriHlpId105")' runat="server">
			<asp:Label id="cScreenCriHlpId105l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ScreenCriHlpId105").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(17) %>" onclick='GridEdit("CultureId105")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"CultureId105").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cCultureId105l" text='<%# DataBinder.Eval(Container.DataItem,"CultureId105Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(18) %>" onclick='GridEdit("ColumnHeader105")' runat="server">
			<asp:Label id="cColumnHeader105l" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnHeader105").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmScreenCriGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmScreenCriGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:50px;text-align:right;' visible="<%# GridColumnVisible(16) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cScreenCriHlpId105ml" runat="server" /></div>
		    <asp:TextBox id="cScreenCriHlpId105" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ScreenCriHlpId105").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(17) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cCultureId105ml" runat="server" /></div>
		    <rcasp:ComboBox autocomplete="off" id="cCultureId105" CssClass="GrdDdl" DataValueField="CultureId105" DataTextField="CultureId105Text" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId105" runat="server" /><asp:RequiredFieldValidator id="cRFVCultureId105" ControlToValidate="cCultureId105" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(18) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnHeader105ml" runat="server" /></div>
		    <asp:TextBox id="cColumnHeader105" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnHeader105").ToString() %>' MaxLength="50" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmScreenCriGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmScreenCriGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmScreenCriGridDataPager" runat="server" Visible="false" PagedControlID="cAdmScreenCriGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
</div>
</asp:Panel>
<asp:label id="cFootLabel" CssClass="FootText" runat="server" />
<input id="bUseCri" type="hidden" runat="server" />
<input id="bPgDirty" type="hidden" text="N" runat="server" />
<input id="bConfirm" type="hidden" runat="server" />
<input id="aNam" type="hidden" runat="server" />
<input id="aVal" type="hidden" runat="server" />
<input id="bErrNow" type="hidden" runat="server" />
<input id="bInfoNow" type="hidden" runat="server" />
<input id="bExpNow" type="hidden" runat="server" />
<input id="CtrlToFocus" type="hidden" runat="server" />
<asp:TextBox ID="bViewState" runat="server" Visible="false" />
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:PlaceHolder ID="LstPHolder" runat="server" Visible="false" />
