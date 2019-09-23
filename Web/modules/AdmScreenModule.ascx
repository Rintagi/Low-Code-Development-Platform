<%@ Control Language="c#" Inherits="RO.Web.AdmScreenModule" CodeFile="AdmScreenModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmScreen9List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmScreen9List_SelectedIndexChanged" OnTextChanged="cAdmScreen9List_TextChanged" OnDDFindClick="cAdmScreen9List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ScreenId15" DataTextField="ScreenId15Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab7" runat="server"/>
<ul id="tabs">
    <li><a id="cTab7" href="#" class="current" name="Tab7" runat="server"></a></li>
    <li><a id="cTab8" href="#" name="Tab8" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab7" runat="server">
    <asp:UpdatePanel id="UpdPanel7" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cScreenId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId15P2" class="r-td r-content" runat="server"><asp:TextBox id="cScreenId15" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cProgramName15P1" class="r-td r-labelR" runat="server"><asp:Label id="cProgramName15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cProgramName15P2" class="r-td r-content" runat="server"><asp:TextBox id="cProgramName15" CssClass="inp-txt" MaxLength="20" runat="server" /><asp:RequiredFieldValidator id="cRFVProgramName15" ControlToValidate="cProgramName15" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenTypeId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenTypeId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenTypeId15P2" class="r-td r-content" runat="server"><asp:DropDownList id="cScreenTypeId15" CssClass="inp-ddl" DataValueField="ScreenTypeId15" DataTextField="ScreenTypeId15Text" AutoPostBack="true" OnSelectedIndexChanged="cScreenTypeId15_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVScreenTypeId15" ControlToValidate="cScreenTypeId15" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cViewOnly15P1" class="r-td r-labelR" runat="server"><asp:Label id="cViewOnly15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cViewOnly15P2" class="r-td r-content" runat="server"><asp:DropDownList id="cViewOnly15" CssClass="inp-ddl" DataValueField="ViewOnly15" DataTextField="ViewOnly15Text" runat="server" /><asp:RequiredFieldValidator id="cRFVViewOnly15" ControlToValidate="cViewOnly15" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchAscending15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchAscending15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchAscending15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cSearchAscending15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cMasterTableId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cMasterTableId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMasterTableId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cMasterTableId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbMasterTableId15" DataValueField="MasterTableId15" DataTextField="MasterTableId15Text" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterTableId15" ControlToValidate="cMasterTableId15" display="none" runat="server" /><asp:ImageButton id="cMasterTableId15Search" CssClass="r-icon" onclick="cMasterTableId15Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchTableId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchTableId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchTableId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchTableId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchTableId15" DataValueField="SearchTableId15" DataTextField="SearchTableId15Text" AutoPostBack="true" OnSelectedIndexChanged="cSearchTableId15_SelectedIndexChanged" OnTextChanged="cSearchTableId15_TextChanged" OnDDFindClick="cSearchTableId15_DDFindClick" runat="server" /><asp:ImageButton id="cSearchTableId15Search" CssClass="r-icon" onclick="cSearchTableId15Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchId15" DataValueField="SearchId15" DataTextField="SearchId15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchIdR15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchIdR15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchIdR15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchIdR15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchIdR15" DataValueField="SearchIdR15" DataTextField="SearchIdR15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchDtlId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchDtlId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchDtlId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchDtlId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchDtlId15" DataValueField="SearchDtlId15" DataTextField="SearchDtlId15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchDtlIdR15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchDtlIdR15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchDtlIdR15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchDtlIdR15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchDtlIdR15" DataValueField="SearchDtlIdR15" DataTextField="SearchDtlIdR15Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSearchUrlId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchUrlId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchUrlId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchUrlId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchUrlId15" DataValueField="SearchUrlId15" DataTextField="SearchUrlId15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSearchImgId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cSearchImgId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSearchImgId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cSearchImgId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbSearchImgId15" DataValueField="SearchImgId15" DataTextField="SearchImgId15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDetailTableId15P1" class="r-td r-labelR" runat="server"><asp:Label id="cDetailTableId15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDetailTableId15P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDetailTableId15" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDetailTableId15" DataValueField="DetailTableId15" DataTextField="DetailTableId15Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGridRows15P1" class="r-td r-labelR" runat="server"><asp:Label id="cGridRows15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGridRows15P2" class="r-td r-content" runat="server"><asp:TextBox id="cGridRows15" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHasDeleteAll15P1" class="r-td r-labelR" runat="server"><asp:Label id="cHasDeleteAll15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHasDeleteAll15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cHasDeleteAll15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cShowGridHead15P1" class="r-td r-labelR" runat="server"><asp:Label id="cShowGridHead15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cShowGridHead15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cShowGridHead15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-2"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cGenerateSc15P1" class="r-td r-labelR" runat="server"><asp:Label id="cGenerateSc15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGenerateSc15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cGenerateSc15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-3-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cGenerateSr15P1" class="r-td r-labelR" runat="server"><asp:Label id="cGenerateSr15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGenerateSr15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cGenerateSr15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cValidateReq15P1" class="r-td r-labelR" runat="server"><asp:Label id="cValidateReq15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cValidateReq15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cValidateReq15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cDeferError15P1" class="r-td r-labelR" runat="server"><asp:Label id="cDeferError15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDeferError15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cDeferError15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-10"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAuthRequired15P1" class="r-td r-labelR" runat="server"><asp:Label id="cAuthRequired15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAuthRequired15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cAuthRequired15" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-11-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cGenAudit15P1" class="r-td r-labelR" runat="server"><asp:Label id="cGenAudit15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGenAudit15P2" class="r-td r-content" runat="server"><asp:CheckBox id="cGenAudit15" CssClass="inp-chk" runat="server" /></div>
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
	<asp:ListView id="cAdmScreenGrid" DataKeyNames="ScreenHlpId16" OnItemCommand="cAdmScreenGrid_OnItemCommand" OnSorting="cAdmScreenGrid_OnSorting" OnPreRender="cAdmScreenGrid_OnPreRender" OnPagePropertiesChanging="cAdmScreenGrid_OnPagePropertiesChanging" OnItemEditing="cAdmScreenGrid_OnItemEditing" OnItemCanceling="cAdmScreenGrid_OnItemCanceling" OnItemDeleting="cAdmScreenGrid_OnItemDeleting" OnItemDataBound="cAdmScreenGrid_OnItemDataBound" OnLayoutCreated="cAdmScreenGrid_OnLayoutCreated" OnItemUpdating="cAdmScreenGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmScreenGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' runat="server">
			<asp:LinkButton id="cScreenHlpId16hl" CssClass="GrdHead" onClick="cScreenHlpId16hl_Click" runat="server" /><asp:Image id="cScreenHlpId16hi" runat="server" />
		</div><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
			<asp:LinkButton id="cCultureId16hl" CssClass="GrdHead" onClick="cCultureId16hl_Click" style="font-weight:bold; color:black;" runat="server" /><asp:Image id="cCultureId16hi" runat="server" />
		</div><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
			<asp:LinkButton id="cScreenTitle16hl" CssClass="GrdHead" onClick="cScreenTitle16hl_Click" style="font-weight:bold; color:black;" runat="server" /><asp:Image id="cScreenTitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' runat="server">
			<asp:LinkButton id="cDefaultHlpMsg16hl" CssClass="GrdHead" onClick="cDefaultHlpMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cDefaultHlpMsg16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' runat="server">
			<asp:LinkButton id="cFootNote16hl" CssClass="GrdHead" onClick="cFootNote16hl_Click" style="color:#50852C;" runat="server" /><asp:Image id="cFootNote16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
			<asp:LinkButton id="cAddMsg16hl" CssClass="GrdHead" onClick="cAddMsg16hl_Click" style="font-weight:bold; color:#00468C;" runat="server" /><asp:Image id="cAddMsg16hi" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
			<asp:LinkButton id="cUpdMsg16hl" CssClass="GrdHead" onClick="cUpdMsg16hl_Click" style="color:#555555;" runat="server" /><asp:Image id="cUpdMsg16hi" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
			<asp:LinkButton id="cDelMsg16hl" CssClass="GrdHead" onClick="cDelMsg16hl_Click" style="color:#00468C;" runat="server" /><asp:Image id="cDelMsg16hi" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
			<asp:LinkButton id="cIncrementMsg16hl" CssClass="GrdHead" onClick="cIncrementMsg16hl_Click" style="color:#555555;" runat="server" /><asp:Image id="cIncrementMsg16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' runat="server">
			<asp:LinkButton id="cNoMasterMsg16hl" CssClass="GrdHead" onClick="cNoMasterMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cNoMasterMsg16hi" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
			<asp:LinkButton id="cNoDetailMsg16hl" CssClass="GrdHead" onClick="cNoDetailMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cNoDetailMsg16hi" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
			<asp:LinkButton id="cAddMasterMsg16hl" CssClass="GrdHead" onClick="cAddMasterMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cAddMasterMsg16hi" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
			<asp:LinkButton id="cAddDetailMsg16hl" CssClass="GrdHead" onClick="cAddDetailMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cAddDetailMsg16hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cMasterLstTitle16hl" CssClass="GrdHead" onClick="cMasterLstTitle16hl_Click" style="font-weight:bold; color:black;" runat="server" /><asp:Image id="cMasterLstTitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cMasterLstSubtitle16hl" CssClass="GrdHead" onClick="cMasterLstSubtitle16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cMasterLstSubtitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cMasterRecTitle16hl" CssClass="GrdHead" onClick="cMasterRecTitle16hl_Click" style="color:#50852C;" runat="server" /><asp:Image id="cMasterRecTitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cMasterRecSubtitle16hl" CssClass="GrdHead" onClick="cMasterRecSubtitle16hl_Click" style="font-weight:bold; color:#00468C;" runat="server" /><asp:Image id="cMasterRecSubtitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cMasterFoundMsg16hl" CssClass="GrdHead" onClick="cMasterFoundMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cMasterFoundMsg16hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cDetailLstTitle16hl" CssClass="GrdHead" onClick="cDetailLstTitle16hl_Click" style="font-weight:bold; color:black;" runat="server" /><asp:Image id="cDetailLstTitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cDetailLstSubtitle16hl" CssClass="GrdHead" onClick="cDetailLstSubtitle16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cDetailLstSubtitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cDetailRecTitle16hl" CssClass="GrdHead" onClick="cDetailRecTitle16hl_Click" style="color:#50852C;" runat="server" /><asp:Image id="cDetailRecTitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet' style='text-align:left;' runat="server">
			<asp:LinkButton id="cDetailRecSubtitle16hl" CssClass="GrdHead" onClick="cDetailRecSubtitle16hl_Click" style="font-weight:bold; color:#00468C;" runat="server" /><asp:Image id="cDetailRecSubtitle16hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
			<asp:LinkButton id="cDetailFoundMsg16hl" CssClass="GrdHead" onClick="cDetailFoundMsg16hl_Click" style="color:#888888;" runat="server" /><asp:Image id="cDetailFoundMsg16hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmScreenGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' runat="server">
		    <asp:Label id="cScreenHlpId16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
		    <asp:Label id="cCultureId16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:400px;text-align:left;' runat="server">
		    <asp:Label id="cScreenTitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' runat="server">
		    <asp:Label id="cDefaultHlpMsg16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' runat="server">
		    <asp:Label id="cFootNote16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
		    <asp:Label id="cAddMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
		    <asp:Label id="cUpdMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' runat="server">
		    <asp:Label id="cDelMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
		    <asp:Label id="cIncrementMsg16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' runat="server">
		    <asp:Label id="cNoMasterMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
		    <asp:Label id="cNoDetailMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
		    <asp:Label id="cAddMasterMsg16fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='text-align:left;' runat="server">
		    <asp:Label id="cAddDetailMsg16fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cMasterLstTitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cMasterLstSubtitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cMasterRecTitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cMasterRecSubtitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cMasterFoundMsg16fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cDetailLstTitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cDetailLstSubtitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cDetailRecTitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet' style='text-align:left;' runat="server">
		    <asp:Label id="cDetailRecSubtitle16fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' runat="server">
		    <asp:Label id="cDetailFoundMsg16fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmScreenGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' visible="<%# GridColumnVisible(26) %>" onclick='GridEdit("ScreenHlpId16")' runat="server">
			<asp:Label id="cScreenHlpId16l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ScreenHlpId16").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(27) %>" onclick='GridEdit("CultureId16")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"CultureId16").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cCultureId16l" text='<%# DataBinder.Eval(Container.DataItem,"CultureId16Text") %>' CssClass="GrdTxtLb" style="font-weight:bold; color:black;" runat="server" />
		</div><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(28) %>" onclick='GridEdit("ScreenTitle16")' runat="server">
			<asp:Label id="cScreenTitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"ScreenTitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="font-weight:bold; color:black;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' visible="<%# GridColumnVisible(29) %>" onclick='GridEdit("DefaultHlpMsg16")' runat="server">
			<asp:Label id="cDefaultHlpMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultHlpMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#888888;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' visible="<%# GridColumnVisible(30) %>" onclick='GridEdit("FootNote16")' runat="server">
			<asp:Label id="cFootNote16l" Text='<%# DataBinder.Eval(Container.DataItem,"FootNote16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#50852C;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(31) %>" onclick='GridEdit("AddMsg16")' runat="server">
			<asp:Label id="cAddMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"AddMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="font-weight:bold; color:#00468C;" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(32) %>" onclick='GridEdit("UpdMsg16")' runat="server">
			<asp:Label id="cUpdMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"UpdMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#555555;" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(33) %>" onclick='GridEdit("DelMsg16")' runat="server">
			<asp:Label id="cDelMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"DelMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#00468C;" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(34) %>" onclick='GridEdit("IncrementMsg16")' runat="server">
			<asp:Label id="cIncrementMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"IncrementMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#555555;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(35) %>" onclick='GridEdit("NoMasterMsg16")' runat="server">
			<asp:Label id="cNoMasterMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"NoMasterMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(36) %>" onclick='GridEdit("NoDetailMsg16")' runat="server">
			<asp:Label id="cNoDetailMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"NoDetailMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(37) %>" onclick='GridEdit("AddMasterMsg16")' runat="server">
			<asp:Label id="cAddMasterMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"AddMasterMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(38) %>" onclick='GridEdit("AddDetailMsg16")' runat="server">
			<asp:Label id="cAddDetailMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"AddDetailMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(39) %>" onclick='GridEdit("MasterLstTitle16")' runat="server">
			<asp:Label id="cMasterLstTitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"MasterLstTitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="font-weight:bold; color:black;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(40) %>" onclick='GridEdit("MasterLstSubtitle16")' runat="server">
			<asp:Label id="cMasterLstSubtitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"MasterLstSubtitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(41) %>" onclick='GridEdit("MasterRecTitle16")' runat="server">
			<asp:Label id="cMasterRecTitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"MasterRecTitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#50852C;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(42) %>" onclick='GridEdit("MasterRecSubtitle16")' runat="server">
			<asp:Label id="cMasterRecSubtitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"MasterRecSubtitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="font-weight:bold; color:#00468C;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(43) %>" onclick='GridEdit("MasterFoundMsg16")' runat="server">
			<asp:Label id="cMasterFoundMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"MasterFoundMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(44) %>" onclick='GridEdit("DetailLstTitle16")' runat="server">
			<asp:Label id="cDetailLstTitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"DetailLstTitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="font-weight:bold; color:black;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(45) %>" onclick='GridEdit("DetailLstSubtitle16")' runat="server">
			<asp:Label id="cDetailLstSubtitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"DetailLstSubtitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(46) %>" onclick='GridEdit("DetailRecTitle16")' runat="server">
			<asp:Label id="cDetailRecTitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"DetailRecTitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#50852C;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet' style='text-align:left;' visible="<%# GridColumnVisible(47) %>" onclick='GridEdit("DetailRecSubtitle16")' runat="server">
			<asp:Label id="cDetailRecSubtitle16l" Text='<%# DataBinder.Eval(Container.DataItem,"DetailRecSubtitle16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="font-weight:bold; color:#00468C;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='text-align:left;' visible="<%# GridColumnVisible(48) %>" onclick='GridEdit("DetailFoundMsg16")' runat="server">
			<asp:Label id="cDetailFoundMsg16l" Text='<%# DataBinder.Eval(Container.DataItem,"DetailFoundMsg16").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="color:#888888;" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmScreenGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmScreenGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:right;' visible="<%# GridColumnVisible(26) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cScreenHlpId16ml" runat="server" /></div>
		    <asp:TextBox id="cScreenHlpId16" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ScreenHlpId16").ToString(),base.LUser.Culture) %>' runat="server" />
		</div><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(27) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cCultureId16ml" runat="server" /></div>
		    <rcasp:ComboBox autocomplete="off" id="cCultureId16" CssClass="GrdDdl" DataValueField="CultureId16" DataTextField="CultureId16Text" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId16" runat="server" /><asp:RequiredFieldValidator id="cRFVCultureId16" ControlToValidate="cCultureId16" display="none" runat="server" />
		</div><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(28) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cScreenTitle16ml" runat="server" /></div>
		    <asp:TextBox id="cScreenTitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ScreenTitle16").ToString() %>' MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVScreenTitle16" ControlToValidate="cScreenTitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' visible="<%# GridColumnVisible(29) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDefaultHlpMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cDefaultHlpMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultHlpMsg16").ToString() %>' runat="server" /><asp:RequiredFieldValidator id="cRFVDefaultHlpMsg16" ControlToValidate="cDefaultHlpMsg16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:600px;text-align:left;' visible="<%# GridColumnVisible(30) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cFootNote16ml" runat="server" /></div>
		    <asp:TextBox id="cFootNote16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"FootNote16").ToString() %>' MaxLength="400" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(31) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cAddMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cAddMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"AddMsg16").ToString() %>' MaxLength="100" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(32) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cUpdMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cUpdMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"UpdMsg16").ToString() %>' MaxLength="100" runat="server" />
		</div><div class='GrdInner' style='max-width:300px;text-align:left;' visible="<%# GridColumnVisible(33) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDelMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cDelMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DelMsg16").ToString() %>' MaxLength="100" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(34) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cIncrementMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cIncrementMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"IncrementMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVIncrementMsg16" ControlToValidate="cIncrementMsg16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(35) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cNoMasterMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cNoMasterMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"NoMasterMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVNoMasterMsg16" ControlToValidate="cNoMasterMsg16" display="none" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(36) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cNoDetailMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cNoDetailMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"NoDetailMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVNoDetailMsg16" ControlToValidate="cNoDetailMsg16" display="none" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(37) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cAddMasterMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cAddMasterMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"AddMasterMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVAddMasterMsg16" ControlToValidate="cAddMasterMsg16" display="none" runat="server" />
		</div><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(38) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cAddDetailMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cAddDetailMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"AddDetailMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVAddDetailMsg16" ControlToValidate="cAddDetailMsg16" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(39) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cMasterLstTitle16ml" runat="server" /></div>
		    <asp:TextBox id="cMasterLstTitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"MasterLstTitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterLstTitle16" ControlToValidate="cMasterLstTitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(40) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cMasterLstSubtitle16ml" runat="server" /></div>
		    <asp:TextBox id="cMasterLstSubtitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"MasterLstSubtitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterLstSubtitle16" ControlToValidate="cMasterLstSubtitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(41) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cMasterRecTitle16ml" runat="server" /></div>
		    <asp:TextBox id="cMasterRecTitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"MasterRecTitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterRecTitle16" ControlToValidate="cMasterRecTitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(42) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cMasterRecSubtitle16ml" runat="server" /></div>
		    <asp:TextBox id="cMasterRecSubtitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"MasterRecSubtitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterRecSubtitle16" ControlToValidate="cMasterRecSubtitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(43) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cMasterFoundMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cMasterFoundMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"MasterFoundMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVMasterFoundMsg16" ControlToValidate="cMasterFoundMsg16" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(44) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDetailLstTitle16ml" runat="server" /></div>
		    <asp:TextBox id="cDetailLstTitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DetailLstTitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVDetailLstTitle16" ControlToValidate="cDetailLstTitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(45) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDetailLstSubtitle16ml" runat="server" /></div>
		    <asp:TextBox id="cDetailLstSubtitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DetailLstSubtitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVDetailLstSubtitle16" ControlToValidate="cDetailLstSubtitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(46) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDetailRecTitle16ml" runat="server" /></div>
		    <asp:TextBox id="cDetailRecTitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DetailRecTitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVDetailRecTitle16" ControlToValidate="cDetailRecTitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(47) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDetailRecSubtitle16ml" runat="server" /></div>
		    <asp:TextBox id="cDetailRecSubtitle16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DetailRecSubtitle16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVDetailRecSubtitle16" ControlToValidate="cDetailRecSubtitle16" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='text-align:left;' visible="<%# GridColumnVisible(48) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDetailFoundMsg16ml" runat="server" /></div>
		    <asp:TextBox id="cDetailFoundMsg16" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DetailFoundMsg16").ToString() %>' MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVDetailFoundMsg16" ControlToValidate="cDetailFoundMsg16" display="none" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmScreenGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmScreenGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmScreenGridDataPager" runat="server" Visible="false" PagedControlID="cAdmScreenGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab8" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel8" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-9"><div class="r-tr">
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cScreenObj15P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenObj15Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenObj15P2" class="r-td r-content" style="font-size:14px;" runat="server"><asp:HyperLink id="cScreenObj15" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenFilterP1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenFilterLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenFilterP2" class="r-td r-content" runat="server"><asp:HyperLink id="cScreenFilter" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMoreInfoP1" class="r-td r-labelR" runat="server"><asp:Label id="cMoreInfoLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMoreInfoP2" class="r-td r-content" style="font-size:20px; color:#888; font-style:italic;" runat="server"><asp:HyperLink id="cMoreInfo" CssClass="inp-txtln" runat="server" /></div>
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
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:PlaceHolder ID="LstPHolder" runat="server" Visible="false" />
