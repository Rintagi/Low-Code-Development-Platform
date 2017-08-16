<%@ Control Language="c#" Inherits="RO.Web.AdmReportCriModule" CodeFile="AdmReportCriModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmReportCri69List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmReportCri69List_SelectedIndexChanged" OnTextChanged="cAdmReportCri69List_TextChanged" OnDDFindClick="cAdmReportCri69List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ReportCriId97" DataTextField="ReportCriId97Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab33" runat="server"/>
<ul id="tabs">
    <li><a id="cTab33" href="#" class="current" name="Tab33" runat="server"></a></li>
    <li><a id="cTab34" href="#" name="Tab34" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab33" runat="server">
    <asp:UpdatePanel id="UpdPanel33" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReportCriId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportCriId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportCriId97P2" class="r-td r-content" runat="server"><asp:TextBox id="cReportCriId97" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId97P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cReportId97" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbReportId97" DataValueField="ReportId97" DataTextField="ReportId97Text" AutoPostBack="true" OnSelectedIndexChanged="cReportId97_SelectedIndexChanged" OnTextChanged="cReportId97_TextChanged" OnDDFindClick="cReportId97_DDFindClick" runat="server" /><asp:RequiredFieldValidator id="cRFVReportId97" ControlToValidate="cReportId97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTabIndex97P1" class="r-td r-labelR" runat="server"><asp:Label id="cTabIndex97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTabIndex97P2" class="r-td r-content" runat="server"><asp:TextBox id="cTabIndex97" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVTabIndex97" ControlToValidate="cTabIndex97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnName97P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnName97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnName97P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnName97" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnName97" ControlToValidate="cColumnName97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportGrpId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportGrpId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportGrpId97P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReportGrpId97" CssClass="inp-ddl" DataValueField="ReportGrpId97" DataTextField="ReportGrpId97Text" runat="server" /><asp:RequiredFieldValidator id="cRFVReportGrpId97" ControlToValidate="cReportGrpId97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLabelCss97P1" class="r-td r-labelR" runat="server"><asp:Label id="cLabelCss97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLabelCss97P2" class="r-td r-content" runat="server"><asp:TextBox id="cLabelCss97" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cContentCss97P1" class="r-td r-labelR" runat="server"><asp:Label id="cContentCss97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cContentCss97P2" class="r-td r-content" runat="server"><asp:TextBox id="cContentCss97" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cDefaultValue97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefaultValue97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefaultValue97P2" class="r-td r-content" runat="server"><asp:TextBox id="cDefaultValue97" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTableId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cTableId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTableId97P2" class="r-td r-content" runat="server"><asp:DropDownList id="cTableId97" CssClass="inp-ddl" DataValueField="TableId97" DataTextField="TableId97Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTableAbbr97P1" class="r-td r-labelR" runat="server"><asp:Label id="cTableAbbr97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTableAbbr97P2" class="r-td r-content" runat="server"><asp:TextBox id="cTableAbbr97" CssClass="inp-txt" MaxLength="10" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRequiredValid97P1" class="r-td r-labelR" runat="server"><asp:Label id="cRequiredValid97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRequiredValid97P2" class="r-td r-content" runat="server"><asp:CheckBox id="cRequiredValid97" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDataTypeId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDataTypeId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDataTypeId97P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDataTypeId97" CssClass="inp-ddl" DataValueField="DataTypeId97" DataTextField="DataTypeId97Text" runat="server" /><asp:RequiredFieldValidator id="cRFVDataTypeId97" ControlToValidate="cDataTypeId97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDataTypeSize97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDataTypeSize97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDataTypeSize97P2" class="r-td r-content" runat="server"><asp:TextBox id="cDataTypeSize97" CssClass="inp-num" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cDisplayModeId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDisplayModeId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDisplayModeId97P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDisplayModeId97" CssClass="inp-ddl" DataValueField="DisplayModeId97" DataTextField="DisplayModeId97Text" runat="server" /><asp:RequiredFieldValidator id="cRFVDisplayModeId97" ControlToValidate="cDisplayModeId97" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnSize97P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnSize97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnSize97P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnSize97" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRowSize97P1" class="r-td r-labelR" runat="server"><asp:Label id="cRowSize97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRowSize97P2" class="r-td r-content" runat="server"><asp:TextBox id="cRowSize97" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlKeyColumnName97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlKeyColumnName97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlKeyColumnName97P2" class="r-td r-content" runat="server"><asp:TextBox id="cDdlKeyColumnName97" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlRefColumnName97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlRefColumnName97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlRefColumnName97P2" class="r-td r-content" runat="server"><asp:TextBox id="cDdlRefColumnName97" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlSrtColumnName97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlSrtColumnName97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlSrtColumnName97P2" class="r-td r-content" runat="server"><asp:TextBox id="cDdlSrtColumnName97" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlFtrColumnId97P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlFtrColumnId97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlFtrColumnId97P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlFtrColumnId97" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlFtrColumnId97" DataValueField="DdlFtrColumnId97" DataTextField="DdlFtrColumnId97Text" runat="server" /></div>
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
	<asp:ListView id="cAdmReportCriGrid" DataKeyNames="ReportCriHlpId98" OnItemCommand="cAdmReportCriGrid_OnItemCommand" OnSorting="cAdmReportCriGrid_OnSorting" OnPreRender="cAdmReportCriGrid_OnPreRender" OnPagePropertiesChanging="cAdmReportCriGrid_OnPagePropertiesChanging" OnItemEditing="cAdmReportCriGrid_OnItemEditing" OnItemCanceling="cAdmReportCriGrid_OnItemCanceling" OnItemDeleting="cAdmReportCriGrid_OnItemDeleting" OnItemDataBound="cAdmReportCriGrid_OnItemDataBound" OnLayoutCreated="cAdmReportCriGrid_OnLayoutCreated" OnItemUpdating="cAdmReportCriGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmReportCriGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' runat="server">
			<asp:LinkButton id="cReportCriHlpId98hl" CssClass="GrdHead" onClick="cReportCriHlpId98hl_Click" runat="server" /><asp:Image id="cReportCriHlpId98hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
			<asp:LinkButton id="cCultureId98hl" CssClass="GrdHead" onClick="cCultureId98hl_Click" runat="server" /><asp:Image id="cCultureId98hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
			<asp:LinkButton id="cColumnHeader98hl" CssClass="GrdHead" onClick="cColumnHeader98hl_Click" runat="server" /><asp:Image id="cColumnHeader98hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmReportCriGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' runat="server">
		    <asp:Label id="cReportCriHlpId98fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
		    <asp:Label id="cCultureId98fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
		    <asp:Label id="cColumnHeader98fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmReportCriGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' visible="<%# GridColumnVisible(22) %>" onclick='GridEdit("ReportCriHlpId98")' runat="server">
			<asp:Label id="cReportCriHlpId98l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReportCriHlpId98").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(23) %>" onclick='GridEdit("CultureId98")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"CultureId98").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cCultureId98l" text='<%# DataBinder.Eval(Container.DataItem,"CultureId98Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(24) %>" onclick='GridEdit("ColumnHeader98")' runat="server">
			<asp:Label id="cColumnHeader98l" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnHeader98").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReportCriGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmReportCriGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' visible="<%# GridColumnVisible(22) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cReportCriHlpId98ml" runat="server" /></div>
		    <asp:TextBox id="cReportCriHlpId98" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReportCriHlpId98").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(23) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cCultureId98ml" runat="server" /></div>
		    <rcasp:ComboBox autocomplete="off" id="cCultureId98" CssClass="GrdDdl" DataValueField="CultureId98" DataTextField="CultureId98Text" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId98" runat="server" /><asp:RequiredFieldValidator id="cRFVCultureId98" ControlToValidate="cCultureId98" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(24) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnHeader98ml" runat="server" /></div>
		    <asp:TextBox id="cColumnHeader98" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnHeader98").ToString() %>' MaxLength="50" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReportCriGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmReportCriGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmReportCriGridDataPager" runat="server" Visible="false" PagedControlID="cAdmReportCriGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab34" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel34" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cWhereClause97P1" class="r-td r-labelR" runat="server"><asp:Label id="cWhereClause97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cWhereClause97P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cWhereClause97" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cWhereClause97" display="none" ErrorMessage="WhereClause <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cWhereClause97E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRegClause97P1" class="r-td r-labelR" runat="server"><asp:Label id="cRegClause97Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRegClause97P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRegClause97" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cRegClause97" display="none" ErrorMessage="RegClause <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:Image id="cRegClause97E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
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
<div id="expand" style="display:none;">
	<input id="TarBox" type="hidden" runat="server" />
    <div id="expandbox"><asp:TextBox ID="cExpandBox" TextMode="MultiLine" CssClass="inp-txt" runat="server" /></div>
</div>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:PlaceHolder ID="LstPHolder" runat="server" Visible="false" />
