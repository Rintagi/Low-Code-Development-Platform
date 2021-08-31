<%@ Control Language="c#" Inherits="RO.Web.AdmReleaseModule" CodeFile="AdmReleaseModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmRelease98List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmRelease98List_SelectedIndexChanged" OnTextChanged="cAdmRelease98List_TextChanged" OnDDFindClick="cAdmRelease98List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ReleaseId191" DataTextField="ReleaseId191Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab61" runat="server"/>
<ul id="tabs">
    <li><a id="cTab61" href="#" class="current" name="Tab61" runat="server"></a></li>
    <li><a id="cTab62" href="#" name="Tab62" runat="server"></a></li>
    <li><a id="cTab63" href="#" name="Tab63" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab61" runat="server">
    <asp:UpdatePanel id="UpdPanel61" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReleaseId191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseId191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseId191P2" class="r-td r-content" runat="server"><asp:TextBox id="cReleaseId191" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReleaseName191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseName191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseName191P2" class="r-td r-content" runat="server"><asp:TextBox id="cReleaseName191" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVReleaseName191" ControlToValidate="cReleaseName191" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReleaseBuild191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseBuild191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseBuild191P2" class="r-td r-content" runat="server"><asp:TextBox id="cReleaseBuild191" CssClass="inp-txt" MaxLength="20" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReleaseDate191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseDate191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseDate191P2" class="r-td r-content" runat="server"><asp:TextBox id="cReleaseDate191" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReleaseOs191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseOs191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseOs191P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReleaseOs191" CssClass="inp-ddl" DataValueField="ReleaseOs191" DataTextField="ReleaseOs191Text" runat="server" /><asp:RequiredFieldValidator id="cRFVReleaseOs191" ControlToValidate="cReleaseOs191" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cEntityId191P1" class="r-td r-labelR" runat="server"><asp:Label id="cEntityId191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cEntityId191P2" class="r-td r-content" runat="server"><asp:DropDownList id="cEntityId191" CssClass="inp-ddl" DataValueField="EntityId191" DataTextField="EntityId191Text" AutoPostBack="true" OnSelectedIndexChanged="cEntityId191_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVEntityId191" ControlToValidate="cEntityId191" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReleaseTypeId191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReleaseTypeId191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReleaseTypeId191P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReleaseTypeId191" CssClass="inp-ddl" DataValueField="ReleaseTypeId191" DataTextField="ReleaseTypeId191Text" runat="server" /><asp:RequiredFieldValidator id="cRFVReleaseTypeId191" ControlToValidate="cReleaseTypeId191" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDeployPath199P1" class="r-td r-labelR" runat="server"><asp:Label id="cDeployPath199Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDeployPath199P2" class="r-td r-content" runat="server"><asp:TextBox id="cDeployPath199" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
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
		    <div><asp:TextBox TextMode="Password" autocomplete="new-password" id="cImportPwd" CssClass="PwdBox" width="250px" MaxLength="32" runat="server" /></div>
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
	<asp:ListView id="cAdmReleaseGrid" DataKeyNames="ReleaseDtlId192" OnItemCommand="cAdmReleaseGrid_OnItemCommand" OnSorting="cAdmReleaseGrid_OnSorting" OnPreRender="cAdmReleaseGrid_OnPreRender" OnPagePropertiesChanging="cAdmReleaseGrid_OnPagePropertiesChanging" OnItemEditing="cAdmReleaseGrid_OnItemEditing" OnItemCanceling="cAdmReleaseGrid_OnItemCanceling" OnItemDeleting="cAdmReleaseGrid_OnItemDeleting" OnItemDataBound="cAdmReleaseGrid_OnItemDataBound" OnLayoutCreated="cAdmReleaseGrid_OnLayoutCreated" OnItemUpdating="cAdmReleaseGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmReleaseGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cReleaseDtlId192hl" CssClass="GrdHead" onClick="cReleaseDtlId192hl_Click" runat="server" /><asp:Image id="cReleaseDtlId192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cObjectType192hl" CssClass="GrdHead" onClick="cObjectType192hl_Click" runat="server" /><asp:Image id="cObjectType192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cRunOrder192hl" CssClass="GrdHead" onClick="cRunOrder192hl_Click" runat="server" /><asp:Image id="cRunOrder192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cSrcObject192hl" CssClass="GrdHead" onClick="cSrcObject192hl_Click" runat="server" /><asp:Image id="cSrcObject192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cSProcOnly192hl" CssClass="GrdHead" onClick="cSProcOnly192hl_Click" runat="server" /><asp:Image id="cSProcOnly192hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
			<asp:LinkButton id="cObjectName192hl" CssClass="GrdHead" onClick="cObjectName192hl_Click" runat="server" /><asp:Image id="cObjectName192hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:400px;text-align:left;' runat="server">
			<asp:LinkButton id="cObjectExempt192hl" CssClass="GrdHead" onClick="cObjectExempt192hl_Click" runat="server" /><asp:Image id="cObjectExempt192hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
			<asp:LinkButton id="cSrcClientTierId192hl" CssClass="GrdHead" onClick="cSrcClientTierId192hl_Click" runat="server" /><asp:Image id="cSrcClientTierId192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
			<asp:LinkButton id="cSrcRuleTierId192hl" CssClass="GrdHead" onClick="cSrcRuleTierId192hl_Click" runat="server" /><asp:Image id="cSrcRuleTierId192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
			<asp:LinkButton id="cSrcDataTierId192hl" CssClass="GrdHead" onClick="cSrcDataTierId192hl_Click" runat="server" /><asp:Image id="cSrcDataTierId192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
			<asp:LinkButton id="cTarDataTierId192hl" CssClass="GrdHead" onClick="cTarDataTierId192hl_Click" runat="server" /><asp:Image id="cTarDataTierId192hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
			<asp:LinkButton id="cDoSpEncrypt192hl" CssClass="GrdHead" onClick="cDoSpEncrypt192hl_Click" runat="server" /><asp:Image id="cDoSpEncrypt192hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmReleaseGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cReleaseDtlId192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cObjectType192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cRunOrder192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cSrcObject192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cSProcOnly192fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
		    <asp:Label id="cObjectName192fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:400px;text-align:left;' runat="server">
		    <asp:Label id="cObjectExempt192fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
		    <asp:Label id="cSrcClientTierId192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
		    <asp:Label id="cSrcRuleTierId192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
		    <asp:Label id="cSrcDataTierId192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
		    <asp:Label id="cTarDataTierId192fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' runat="server">
		    <asp:Label id="cDoSpEncrypt192fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmReleaseGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(10) %>" onclick='GridEdit("ReleaseDtlId192")' runat="server">
			<asp:Label id="cReleaseDtlId192l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReleaseDtlId192").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(11) %>" onclick='GridEdit("ObjectType192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"ObjectType192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' Visible="false" runat="server" />
			<asp:Label id="cObjectType192l" text='<%# DataBinder.Eval(Container.DataItem,"ObjectType192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(12) %>" onclick='GridEdit("RunOrder192")' runat="server">
			<asp:Label id="cRunOrder192l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"RunOrder192").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(13) %>" onclick='GridEdit("SrcObject192")' runat="server">
			<asp:Label id="cSrcObject192l" Text='<%# DataBinder.Eval(Container.DataItem,"SrcObject192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(14) %>" onclick='GridEdit("SProcOnly192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"SProcOnly192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' Visible="false" runat="server" />
			<asp:Label id="cSProcOnly192l" text='<%# DataBinder.Eval(Container.DataItem,"SProcOnly192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(15) %>" onclick='GridEdit("ObjectName192")' runat="server">
			<asp:Label id="cObjectName192l" Text='<%# DataBinder.Eval(Container.DataItem,"ObjectName192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(16) %>" onclick='GridEdit("ObjectExempt192")' runat="server">
			<asp:Label id="cObjectExempt192l" Text='<%# DataBinder.Eval(Container.DataItem,"ObjectExempt192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(17) %>" onclick='GridEdit("SrcClientTierId192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"SrcClientTierId192").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cSrcClientTierId192l" text='<%# DataBinder.Eval(Container.DataItem,"SrcClientTierId192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(18) %>" onclick='GridEdit("SrcRuleTierId192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"SrcRuleTierId192").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cSrcRuleTierId192l" text='<%# DataBinder.Eval(Container.DataItem,"SrcRuleTierId192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(19) %>" onclick='GridEdit("SrcDataTierId192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"SrcDataTierId192").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cSrcDataTierId192l" text='<%# DataBinder.Eval(Container.DataItem,"SrcDataTierId192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(20) %>" onclick='GridEdit("TarDataTierId192")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"TarDataTierId192").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cTarDataTierId192l" text='<%# DataBinder.Eval(Container.DataItem,"TarDataTierId192Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(21) %>" onclick='GridEdit("DoSpEncrypt192")' runat="server">
			<asp:CheckBox id="cDoSpEncrypt192l" Enabled='<%# AllowEdit(LcAuth,"DoSpEncrypt192") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"DoSpEncrypt192").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReleaseGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmReleaseGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(10) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cReleaseDtlId192ml" runat="server" /></div>
		    <asp:TextBox id="cReleaseDtlId192" CssClass="GrdTxt" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReleaseDtlId192").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(11) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cObjectType192ml" runat="server" /></div>
		    <asp:DropDownList id="cObjectType192" CssClass="GrdDdl" DataValueField="ObjectType192" DataTextField="ObjectType192Text" runat="server" /><asp:RequiredFieldValidator id="cRFVObjectType192" ControlToValidate="cObjectType192" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(12) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cRunOrder192ml" runat="server" /></div>
		    <asp:TextBox id="cRunOrder192" CssClass="GrdTxt" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"RunOrder192").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(13) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cSrcObject192ml" runat="server" /></div>
		    <asp:TextBox id="cSrcObject192" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"SrcObject192").ToString() %>' MaxLength="50" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(14) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cSProcOnly192ml" runat="server" /></div>
		    <asp:DropDownList id="cSProcOnly192" CssClass="GrdDdl" DataValueField="SProcOnly192" DataTextField="SProcOnly192Text" runat="server" /><asp:RequiredFieldValidator id="cRFVSProcOnly192" ControlToValidate="cSProcOnly192" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(15) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cObjectName192ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr><td>
		    <asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cObjectName192" CssClass="GrdTxt" height="135px" Text='<%# DataBinder.Eval(Container.DataItem,"ObjectName192").ToString() %>' runat="server" /><asp:RequiredFieldValidator id="cRFVObjectName192" ControlToValidate="cObjectName192" display="none" runat="server" /></td><td><asp:Image id="cObjectName192E" ImageUrl="~/images/Expand.gif" CssClass="show-expand-button" runat="server" />
		</td></tr></table>
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:400px;text-align:left;' visible="<%# GridColumnVisible(16) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cObjectExempt192ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr><td>
		    <asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cObjectExempt192" CssClass="GrdTxt" height="135px" Text='<%# DataBinder.Eval(Container.DataItem,"ObjectExempt192").ToString() %>' runat="server" /></td><td><asp:Image id="cObjectExempt192E" ImageUrl="~/images/Expand.gif" CssClass="show-expand-button" runat="server" />
		</td></tr></table>
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(17) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cSrcClientTierId192ml" runat="server" /></div>
		    <asp:DropDownList id="cSrcClientTierId192" CssClass="GrdDdl" DataValueField="SrcClientTierId192" DataTextField="SrcClientTierId192Text" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(18) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cSrcRuleTierId192ml" runat="server" /></div>
		    <asp:DropDownList id="cSrcRuleTierId192" CssClass="GrdDdl" DataValueField="SrcRuleTierId192" DataTextField="SrcRuleTierId192Text" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(19) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cSrcDataTierId192ml" runat="server" /></div>
		    <asp:DropDownList id="cSrcDataTierId192" CssClass="GrdDdl" DataValueField="SrcDataTierId192" DataTextField="SrcDataTierId192Text" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(20) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cTarDataTierId192ml" runat="server" /></div>
		    <asp:DropDownList id="cTarDataTierId192" CssClass="GrdDdl" DataValueField="TarDataTierId192" DataTextField="TarDataTierId192Text" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:180px;text-align:left;' visible="<%# GridColumnVisible(21) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDoSpEncrypt192ml" runat="server" /></div>
		    <asp:CheckBox id="cDoSpEncrypt192" CssClass="GrdBox" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReleaseGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmReleaseGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmReleaseGridDataPager" runat="server" Visible="false" PagedControlID="cAdmReleaseGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab62" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel62" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cTarScriptAft191P1" class="r-td r-labelR" runat="server"><asp:Label id="cTarScriptAft191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTarScriptAft191P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cTarScriptAft191" CssClass="inp-txt" runat="server" /><asp:Image id="cTarScriptAft191E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab63" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel63" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReadMe191P1" class="r-td r-labelR" runat="server"><asp:Label id="cReadMe191Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReadMe191P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cReadMe191" CssClass="inp-txt" runat="server" /><asp:Image id="cReadMe191E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
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
