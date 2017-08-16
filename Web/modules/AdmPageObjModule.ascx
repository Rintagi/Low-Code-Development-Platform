<%@ Control Language="c#" Inherits="RO.Web.AdmPageObjModule" CodeFile="AdmPageObjModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
	function pageLoad() {
	$(window).resize(function () { try {$("#expand").dialog('option', 'position', { my:'center', at:'center', of:window });} catch(e){}; });
	$("#expand").dialog({ maxWidth:675, maxHeight:575, width:'80%', height:'80%', modal:true, autoOpen:false, title:"Text Editor",
	    buttons: {"Enter": function () {SaveExpand();$(this).dialog("close");}}
	    });
	}
	$(".show-expand-button").live('click', function () {
	    var title = $("#" + $(this).attr("label_id")).text();
	    var target_textbox = $(this).attr("target_id");
	    $('#<%=cExpandBox.ClientID %>').val($("#" + target_textbox).val());
	    $('#<%=TarBox.ClientID %>').val(target_textbox);
	    $("#expand").dialog("option", "title", title);
	    $("#expand").dialog('open');
	    document.getElementById('<%=cExpandBox.ClientID %>').focus();
	});
	function SaveExpand() {document.getElementById(document.getElementById('<%=TarBox.ClientID %>').value).value = document.getElementById('<%=cExpandBox.ClientID %>').value; document.getElementById('<%=bPgDirty.ClientID %>').value = 'Y'; ChkPgDirty();}
$(document).ready(function() {if($('.chkMobile').css('position')=='relative'){ $('.chkMobile').val('isMobile');};});
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmPageObj1001List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmPageObj1001List_SelectedIndexChanged" OnTextChanged="cAdmPageObj1001List_TextChanged" OnDDFindClick="cAdmPageObj1001List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="PageObjId1277" DataTextField="PageObjId1277Text" /></div>
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
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cPageObjId1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageObjId1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageObjId1277P2" class="r-td r-content" runat="server"><asp:TextBox id="cPageObjId1277" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSectionCd1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cSectionCd1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSectionCd1277P2" class="r-td r-content" runat="server"><asp:DropDownList id="cSectionCd1277" CssClass="inp-ddl" DataValueField="SectionCd1277" DataTextField="SectionCd1277Text" runat="server" /><asp:RequiredFieldValidator id="cRFVSectionCd1277" ControlToValidate="cSectionCd1277" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGroupRowId1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cGroupRowId1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGroupRowId1277P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGroupRowId1277" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGroupRowId1277" DataValueField="GroupRowId1277" DataTextField="GroupRowId1277Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGroupColId1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cGroupColId1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGroupColId1277P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGroupColId1277" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGroupColId1277" DataValueField="GroupColId1277" DataTextField="GroupColId1277Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLinkTypeCd1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cLinkTypeCd1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLinkTypeCd1277P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cLinkTypeCd1277" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbLinkTypeCd1277" DataValueField="LinkTypeCd1277" DataTextField="LinkTypeCd1277Text" AutoPostBack="true" OnSelectedIndexChanged="cLinkTypeCd1277_SelectedIndexChanged" OnTextChanged="cLinkTypeCd1277_TextChanged" OnDDFindClick="cLinkTypeCd1277_DDFindClick" runat="server" /><asp:RequiredFieldValidator id="cRFVLinkTypeCd1277" ControlToValidate="cLinkTypeCd1277" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPageObjOrd1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageObjOrd1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageObjOrd1277P2" class="r-td r-content" runat="server"><asp:TextBox id="cPageObjOrd1277" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVPageObjOrd1277" ControlToValidate="cPageObjOrd1277" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSctGrpRow1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cSctGrpRow1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSctGrpRow1277P2" class="r-td r-content" runat="server"><asp:HyperLink id="cSctGrpRow1277" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSctGrpCol1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cSctGrpCol1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSctGrpCol1277P2" class="r-td r-content" runat="server"><asp:HyperLink id="cSctGrpCol1277" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPageObjCss1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageObjCss1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageObjCss1277P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cPageObjCss1277" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cPageObjCss1277" display="none" ErrorMessage="PageObjCss <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cPageObjCss1277E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPageObjSrp1277P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageObjSrp1277Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageObjSrp1277P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cPageObjSrp1277" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cPageObjSrp1277" display="none" ErrorMessage="PageObjSrp <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cPageObjSrp1277E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBtnDefaultP1" class="r-td r-labelR" runat="server"><asp:Label id="cBtnDefaultLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBtnDefaultP2" class="r-td r-content" runat="server"><asp:Button id="cBtnDefault" CssClass="small blue button" OnClick="cBtnDefault_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBtnHeaderP1" class="r-td r-labelR" runat="server"><asp:Label id="cBtnHeaderLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBtnHeaderP2" class="r-td r-content" runat="server"><asp:Button id="cBtnHeader" CssClass="small blue button" OnClick="cBtnHeader_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBtnFooterP1" class="r-td r-labelR" runat="server"><asp:Label id="cBtnFooterLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBtnFooterP2" class="r-td r-content" runat="server"><asp:Button id="cBtnFooter" CssClass="small blue button" OnClick="cBtnFooter_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBtnSidebarP1" class="r-td r-labelR" runat="server"><asp:Label id="cBtnSidebarLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBtnSidebarP2" class="r-td r-content" runat="server"><asp:Button id="cBtnSidebar" CssClass="small blue button" OnClick="cBtnSidebar_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
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
	<asp:ListView id="cAdmPageObjGrid" DataKeyNames="PageLnkId1278" OnItemCommand="cAdmPageObjGrid_OnItemCommand" OnSorting="cAdmPageObjGrid_OnSorting" OnPreRender="cAdmPageObjGrid_OnPreRender" OnPagePropertiesChanging="cAdmPageObjGrid_OnPagePropertiesChanging" OnItemEditing="cAdmPageObjGrid_OnItemEditing" OnItemCanceling="cAdmPageObjGrid_OnItemCanceling" OnItemDeleting="cAdmPageObjGrid_OnItemDeleting" OnItemDataBound="cAdmPageObjGrid_OnItemDataBound" OnLayoutCreated="cAdmPageObjGrid_OnLayoutCreated" OnItemUpdating="cAdmPageObjGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmPageObjGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:50px;text-align:right;' runat="server">
			<asp:LinkButton id="cPageLnkId1278hl" CssClass="GrdHead" onClick="cPageLnkId1278hl_Click" runat="server" /><asp:Image id="cPageLnkId1278hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
			<asp:LinkButton id="cPageLnkTxt1278hl" CssClass="GrdHead" onClick="cPageLnkTxt1278hl_Click" style="color:#333;" runat="server" /><asp:Image id="cPageLnkTxt1278hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
			<asp:LinkButton id="cPageLnkRef1278hl" CssClass="GrdHead" onClick="cPageLnkRef1278hl_Click" runat="server" /><asp:Image id="cPageLnkRef1278hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
			<asp:LinkButton id="cPageLnkImg1278hl" CssClass="GrdHead" onClick="cPageLnkImg1278hl_Click" style="color:#333;" runat="server" /><asp:Image id="cPageLnkImg1278hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
			<asp:LinkButton id="cPageLnkAlt1278hl" CssClass="GrdHead" onClick="cPageLnkAlt1278hl_Click" runat="server" /><asp:Image id="cPageLnkAlt1278hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' runat="server">
			<asp:LinkButton id="cPageLnkOrd1278hl" CssClass="GrdHead" onClick="cPageLnkOrd1278hl_Click" style="color:#555;" runat="server" /><asp:Image id="cPageLnkOrd1278hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' runat="server">
			<asp:LinkButton id="cPopup1278hl" CssClass="GrdHead" onClick="cPopup1278hl_Click" runat="server" /><asp:Image id="cPopup1278hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:250px;text-align:left;' runat="server">
			<asp:LinkButton id="cPageLnkCss1278hl" CssClass="GrdHead" onClick="cPageLnkCss1278hl_Click" style="color:#333;" runat="server" /><asp:Image id="cPageLnkCss1278hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmPageObjGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:50px;text-align:right;' runat="server">
		    <asp:Label id="cPageLnkId1278fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
		    <asp:Label id="cPageLnkTxt1278fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
		    <asp:Label id="cPageLnkRef1278fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
		    <asp:Label id="cPageLnkImg1278fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' runat="server">
		    <asp:Label id="cPageLnkAlt1278fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' runat="server">
		    <asp:Label id="cPageLnkOrd1278fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' runat="server">
		    <asp:Label id="cPopup1278fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:250px;text-align:left;' runat="server">
		    <asp:Label id="cPageLnkCss1278fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmPageObjGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:50px;text-align:right;' visible="<%# GridColumnVisible(14) %>" onclick='GridEdit("PageLnkId1278")' runat="server">
			<asp:Label id="cPageLnkId1278l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"PageLnkId1278").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(15) %>" onclick='GridEdit("PageLnkTxt1278")' runat="server">
			<asp:Label id="cPageLnkTxt1278l" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkTxt1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#333;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(16) %>" onclick='GridEdit("PageLnkRef1278")' runat="server">
			<asp:Label id="cPageLnkRef1278l" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkRef1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(17) %>" onclick='GridEdit("PageLnkImg1278")' runat="server">
			<asp:Label id="cPageLnkImg1278l" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkImg1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#333;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(18) %>" onclick='GridEdit("PageLnkAlt1278")' runat="server">
			<asp:Label id="cPageLnkAlt1278l" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkAlt1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' visible="<%# GridColumnVisible(19) %>" onclick='GridEdit("PageLnkOrd1278")' runat="server">
			<asp:Label id="cPageLnkOrd1278l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"PageLnkOrd1278").ToString(),base.LUser.Culture) %>' CssClass="GrdBoxLb" style="color:#555;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' visible="<%# GridColumnVisible(20) %>" onclick='GridEdit("Popup1278")' runat="server">
			<asp:CheckBox id="cPopup1278l" Enabled='<%# AllowEdit(LcAuth,"Popup1278") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"Popup1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(21) %>" onclick='GridEdit("PageLnkCss1278")' runat="server">
			<asp:Label id="cPageLnkCss1278l" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkCss1278").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#333;" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmPageObjGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmPageObjGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:50px;text-align:right;' visible="<%# GridColumnVisible(14) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkId1278ml" runat="server" /></div>
		    <asp:TextBox id="cPageLnkId1278" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"PageLnkId1278").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(15) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkTxt1278ml" runat="server" /></div>
		    <asp:TextBox id="cPageLnkTxt1278" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkTxt1278").ToString() %>' MaxLength="4000" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(16) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkRef1278ml" runat="server" /></div>
		    <asp:TextBox id="cPageLnkRef1278" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkRef1278").ToString() %>' MaxLength="1000" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(17) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkImg1278ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr>
		    <td><asp:Panel id="cPageLnkImg1278Pan" CssClass="DocPanel" Visible="false" runat="server">
		    <table width="100%"><tr>
		    <td style="display:none;" ><asp:Button id="cPageLnkImg1278Upl" CssClass="small blue button DocUpload" OnClientClick="GridUpload(); return false;" text="Upload" runat="server" /></td>
		    <td><asp:FileUpload id="cPageLnkImg1278Fi" runat="server"  onchange="AutoUpload(this,event);" /></td>
		    </tr></table>
		    </asp:Panel>
		    <asp:TextBox id="cPageLnkImg1278" CssClass="GrdTxt" MaxLength="100" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkImg1278").ToString() %>' AutoPostBack="true" runat="server" /></td><td><asp:imagebutton id="cPageLnkImg1278Tgo" OnClientClick='NoConfirm()' onclick="cPageLnkImg1278Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></td>
		</tr></table>
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(18) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkAlt1278ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr>
		    <td><asp:Panel id="cPageLnkAlt1278Pan" CssClass="DocPanel" Visible="false" runat="server">
		    <table width="100%"><tr>
		    <td style="display:none;" ><asp:Button id="cPageLnkAlt1278Upl" CssClass="small blue button DocUpload" OnClientClick="GridUpload(); return false;" text="Upload" runat="server" /></td>
		    <td><asp:FileUpload id="cPageLnkAlt1278Fi" runat="server"  onchange="AutoUpload(this,event);" /></td>
		    </tr></table>
		    </asp:Panel>
		    <asp:TextBox id="cPageLnkAlt1278" CssClass="GrdTxt" MaxLength="100" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkAlt1278").ToString() %>' AutoPostBack="true" runat="server" /></td><td><asp:imagebutton id="cPageLnkAlt1278Tgo" OnClientClick='NoConfirm()' onclick="cPageLnkAlt1278Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></td>
		</tr></table>
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' visible="<%# GridColumnVisible(19) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkOrd1278ml" runat="server" /></div>
		    <asp:TextBox id="cPageLnkOrd1278" CssClass="GrdCtr" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"PageLnkOrd1278").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:70px;text-align:center;' visible="<%# GridColumnVisible(20) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPopup1278ml" runat="server" /></div>
		    <asp:CheckBox id="cPopup1278" CssClass="GrdBox" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:250px;text-align:left;' visible="<%# GridColumnVisible(21) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPageLnkCss1278ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr><td>
		    <asp:TextBox TextMode="MultiLine" id="cPageLnkCss1278" CssClass="GrdTxt" height="50px" Text='<%# DataBinder.Eval(Container.DataItem,"PageLnkCss1278").ToString() %>' runat="server" /><asp:RegularExpressionValidator ControlToValidate="cPageLnkCss1278" display="none" ErrorMessage="PageLnkCss <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /></td><td><asp:Image id="cPageLnkCss1278E" ImageUrl="~/images/Expand.gif" CssClass="show-expand-button" runat="server" />
		</td></tr></table>
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmPageObjGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmPageObjGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmPageObjGridDataPager" runat="server" Visible="false" PagedControlID="cAdmPageObjGrid"></asp:DataPager>
</div></div></div>
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
<div id="expand" style="display:none;">
	<input id="TarBox" type="hidden" runat="server" />
    <div id="expandbox"><asp:TextBox ID="cExpandBox" TextMode="MultiLine" CssClass="inp-txt" runat="server" /></div>
</div>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:PlaceHolder ID="LstPHolder" runat="server" Visible="false" />
