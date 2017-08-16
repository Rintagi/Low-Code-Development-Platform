<%@ Control Language="c#" Inherits="RO.Web.AdmDbTableModule" CodeFile="AdmDbTableModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmDbTable2List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmDbTable2List_SelectedIndexChanged" OnTextChanged="cAdmDbTable2List_TextChanged" OnDDFindClick="cAdmDbTable2List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="TableId3" DataTextField="TableId3Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab3" runat="server"/>
<ul id="tabs">
    <li><a id="cTab3" href="#" class="current" name="Tab3" runat="server"></a></li>
    <li><a id="cTab4" href="#" name="Tab4" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab3" runat="server">
    <asp:UpdatePanel id="UpdPanel3" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cUploadSheetUpl" /></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cTableId3P1" class="r-td r-labelR" runat="server"><asp:Label id="cTableId3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTableId3P2" class="r-td r-content" runat="server"><asp:TextBox id="cTableId3" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSystemId3P1" class="r-td r-labelR" runat="server"><asp:Label id="cSystemId3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSystemId3P2" class="r-td r-content" runat="server"><asp:DropDownList id="cSystemId3" CssClass="inp-ddl" DataValueField="SystemId3" DataTextField="SystemId3Text" runat="server" /><asp:RequiredFieldValidator id="cRFVSystemId3" ControlToValidate="cSystemId3" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTableName3P1" class="r-td r-labelR" runat="server"><asp:Label id="cTableName3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTableName3P2" class="r-td r-content" runat="server"><asp:TextBox id="cTableName3" CssClass="inp-txt" MaxLength="500" runat="server" /><asp:RequiredFieldValidator id="cRFVTableName3" ControlToValidate="cTableName3" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTableDesc3P1" class="r-td r-labelR" runat="server"><asp:Label id="cTableDesc3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTableDesc3P2" class="r-td r-content" runat="server"><asp:TextBox id="cTableDesc3" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVTableDesc3" ControlToValidate="cTableDesc3" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cTblObjective3P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cTblObjective3Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cTblObjective3P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cTblObjective3" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cTblObjective3" display="none" ErrorMessage="TblObjective <= 500 characters please." ValidationExpression="^[\s\S]{0,500}$" runat="server" /><asp:Image id="cTblObjective3E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-11"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cVirtualTbl3P1" class="r-td r-labelR" runat="server"><asp:Label id="cVirtualTbl3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVirtualTbl3P2" class="r-td r-content" runat="server"><asp:CheckBox id="cVirtualTbl3" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMultiDesignDb3P1" class="r-td r-labelR" runat="server"><asp:Label id="cMultiDesignDb3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMultiDesignDb3P2" class="r-td r-content" runat="server"><asp:CheckBox id="cMultiDesignDb3" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cModelSampleP1" class="r-td r-labelR" runat="server"><asp:Label id="cModelSampleLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModelSampleP2" class="r-td r-content" runat="server"><asp:ImageButton id="cModelSample" OnClientClick='NoConfirm()' OnClick="cModelSample_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-12-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSyncByDbP1" class="r-td r-labelR" runat="server"><asp:Label id="cSyncByDbLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSyncByDbP2" class="r-td r-content" runat="server"><asp:ImageButton id="cSyncByDb" OnClientClick='NoConfirm()' OnClick="cSyncByDb_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAnalToDbP1" class="r-td r-labelR" runat="server"><asp:Label id="cAnalToDbLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAnalToDbP2" class="r-td r-content" runat="server"><asp:ImageButton id="cAnalToDb" OnClientClick='NoConfirm()' OnClick="cAnalToDb_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSyncToDbP1" class="r-td r-labelR" runat="server"><asp:Label id="cSyncToDbLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSyncToDbP2" class="r-td r-content" runat="server"><asp:ImageButton id="cSyncToDb" OnClientClick='NoConfirm()' OnClick="cSyncToDb_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUploadSheetP1" class="r-td r-labelR" runat="server"><asp:Label id="cUploadSheetLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUploadSheetP2" class="r-td r-content" runat="server">
    			<asp:Panel id="cUploadSheetPan" CssClass="DocPanel" Visible="false" runat="server">
    			<table width="100%"><tr>
    			<td style="display:none;"><asp:Button id="cUploadSheetUpl" CssClass="small blue button" OnClientClick='NoConfirm()' onclick="cUploadSheetUpl_Click" text="Upload" runat="server" /></td>
    			<td><asp:FileUpload id="cUploadSheetFi" runat="server" onchange="AutoUpload(this,event);" /></td>
    			</tr></table>
    			</asp:Panel>
    			<asp:TextBox id="cUploadSheet" CssClass="inp-txt" MaxLength="100" AutoPostBack="true" OnTextChanged="cUploadSheet_TextChanged" runat="server" />
    			<asp:ImageButton id="cUploadSheetTgo" CssClass="r-icon" OnClientClick='NoConfirm()' onclick="cUploadSheetTgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSheetNameListP1" class="r-td r-labelR" runat="server"><asp:Label id="cSheetNameListLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSheetNameListP2" class="r-td r-content" runat="server"><asp:DropDownList id="cSheetNameList" CssClass="inp-ddl" DataValueField="SheetNameList" DataTextField="SheetNameListText" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-10"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRowsToExamineP1" class="r-td r-labelR" runat="server"><asp:Label id="cRowsToExamineLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRowsToExamineP2" class="r-td r-content" runat="server"><asp:TextBox id="cRowsToExamine" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-11-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBtnScanP1" class="r-td r-labelR" runat="server"><asp:Label id="cBtnScanLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBtnScanP2" class="r-td r-content" runat="server"><asp:Button id="cBtnScan" CssClass="small blue button" OnClick="cBtnScan_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cModifiedBy3P1" class="r-td r-labelR" runat="server"><asp:Label id="cModifiedBy3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModifiedBy3P2" class="r-td r-content" runat="server"><asp:DropDownList id="cModifiedBy3" CssClass="inp-ddl" DataValueField="ModifiedBy3" DataTextField="ModifiedBy3Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cModifiedOn3P1" class="r-td r-labelR" runat="server"><asp:Label id="cModifiedOn3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModifiedOn3P2" class="r-td r-content" runat="server"><asp:TextBox id="cModifiedOn3" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cLastSyncDt3P1" class="r-td r-labelR" runat="server"><asp:Label id="cLastSyncDt3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLastSyncDt3P2" class="r-td r-content" runat="server"><asp:TextBox id="cLastSyncDt3" CssClass="inp-txt" runat="server" /></div>
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
	<asp:ListView id="cAdmDbTableGrid" DataKeyNames="ColumnId5" OnItemCommand="cAdmDbTableGrid_OnItemCommand" OnSorting="cAdmDbTableGrid_OnSorting" OnPreRender="cAdmDbTableGrid_OnPreRender" OnPagePropertiesChanging="cAdmDbTableGrid_OnPagePropertiesChanging" OnItemEditing="cAdmDbTableGrid_OnItemEditing" OnItemCanceling="cAdmDbTableGrid_OnItemCanceling" OnItemDeleting="cAdmDbTableGrid_OnItemDeleting" OnItemDataBound="cAdmDbTableGrid_OnItemDataBound" OnLayoutCreated="cAdmDbTableGrid_OnLayoutCreated" OnItemUpdating="cAdmDbTableGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmDbTableGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' runat="server">
			<asp:LinkButton id="cColumnId5hl" CssClass="GrdHead" onClick="cColumnId5hl_Click" runat="server" /><asp:Image id="cColumnId5hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' runat="server">
			<asp:LinkButton id="cColumnIndex5hl" CssClass="GrdHead" onClick="cColumnIndex5hl_Click" runat="server" /><asp:Image id="cColumnIndex5hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' runat="server">
			<asp:LinkButton id="cExternalTable5hl" CssClass="GrdHead" onClick="cExternalTable5hl_Click" style="color:#50852C;" runat="server" /><asp:Image id="cExternalTable5hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cColumnName5hl" CssClass="GrdHead" onClick="cColumnName5hl_Click" style="font-weight:bold; color:black;" runat="server" /><asp:Image id="cColumnName5hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
			<asp:LinkButton id="cDataType5hl" CssClass="GrdHead" onClick="cDataType5hl_Click" style="font-weight:bold; color:#555555;" runat="server" /><asp:Image id="cDataType5hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:60px;text-align:right;' runat="server">
			<asp:LinkButton id="cColumnLength5hl" CssClass="GrdHead" onClick="cColumnLength5hl_Click" runat="server" /><asp:Image id="cColumnLength5hi" runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:40px;text-align:right;' runat="server">
			<asp:LinkButton id="cColumnScale5hl" CssClass="GrdHead" onClick="cColumnScale5hl_Click" runat="server" /><asp:Image id="cColumnScale5hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cDefaultValue5hl" CssClass="GrdHead" onClick="cDefaultValue5hl_Click" style="font-style:italic; color:#555555;" runat="server" /><asp:Image id="cDefaultValue5hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cAllowNulls5hl" CssClass="GrdHead" onClick="cAllowNulls5hl_Click" runat="server" /><asp:Image id="cAllowNulls5hi" runat="server" /><asp:CheckBox id="cAllowNulls5hc" CssClass="GrdHead" AutoPostBack="true" onCheckedChanged="cAllowNulls5hc_CheckedChanged" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cColumnIdentity5hl" CssClass="GrdHead" onClick="cColumnIdentity5hl_Click" runat="server" /><asp:Image id="cColumnIdentity5hi" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cPrimaryKey5hl" CssClass="GrdHead" onClick="cPrimaryKey5hl_Click" runat="server" /><asp:Image id="cPrimaryKey5hi" runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cIsIndexU5hl" CssClass="GrdHead" onClick="cIsIndexU5hl_Click" runat="server" /><asp:Image id="cIsIndexU5hi" runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' runat="server">
			<asp:LinkButton id="cIsIndex5hl" CssClass="GrdHead" onClick="cIsIndex5hl_Click" runat="server" /><asp:Image id="cIsIndex5hi" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:500px;text-align:left;' runat="server">
			<asp:LinkButton id="cColObjective5hl" CssClass="GrdHead" onClick="cColObjective5hl_Click" style="color:#555555; font-size:12px;" runat="server" /><asp:Image id="cColObjective5hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmDbTableGridFooter" class="GrdFoot" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' runat="server">
		    <asp:Label id="cColumnId5fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' runat="server">
		    <asp:Label id="cColumnIndex5fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' runat="server">
		    <asp:Label id="cExternalTable5fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cColumnName5fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' runat="server">
		    <asp:Label id="cDataType5fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:60px;text-align:right;' runat="server">
		    <asp:Label id="cColumnLength5fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:40px;text-align:right;' runat="server">
		    <asp:Label id="cColumnScale5fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cDefaultValue5fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cAllowNulls5fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cColumnIdentity5fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cPrimaryKey5fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cIsIndexU5fl" class='GrdFoot' runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' runat="server">
		    <asp:Label id="cIsIndex5fl" class='GrdFoot' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:500px;text-align:left;' runat="server">
		    <asp:Label id="cColObjective5fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmDbTableGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(19) %>" onclick='GridEdit("ColumnId5")' runat="server">
			<asp:Label id="cColumnId5l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnId5").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' visible="<%# GridColumnVisible(20) %>" onclick='GridEdit("ColumnIndex5")' runat="server">
			<asp:Label id="cColumnIndex5l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnIndex5").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnMobile' style='max-width:110px;text-align:right;' visible="<%# GridColumnVisible(21) %>" onclick='GridEdit("ExternalTable5")' runat="server">
			<asp:Label id="cExternalTable5l" Text='<%# DataBinder.Eval(Container.DataItem,"ExternalTable5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#50852C;" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(22) %>" onclick='GridEdit("ColumnName5")' runat="server">
			<asp:Label id="cColumnName5l" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnName5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdNwrLb" style="font-weight:bold; color:black;" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(23) %>" onclick='GridEdit("DataType5")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"DataType5").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cDataType5l" text='<%# DataBinder.Eval(Container.DataItem,"DataType5Text") %>' CssClass="GrdTxtLb" style="font-weight:bold; color:#555555;" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:60px;text-align:right;' visible="<%# GridColumnVisible(24) %>" onclick='GridEdit("ColumnLength5")' runat="server">
			<asp:Label id="cColumnLength5l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnLength5").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(25) %>" onclick='GridEdit("ColumnScale5")' runat="server">
			<asp:Label id="cColumnScale5l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnScale5").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(26) %>" onclick='GridEdit("DefaultValue5")' runat="server">
			<asp:Label id="cDefaultValue5l" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultValue5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="font-style:italic; color:#555555;" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(27) %>" onclick='GridEdit("AllowNulls5")' runat="server">
			<asp:CheckBox id="cAllowNulls5l" Enabled='<%# AllowEdit(LcAuth,"AllowNulls5") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"AllowNulls5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(28) %>" onclick='GridEdit("ColumnIdentity5")' runat="server">
			<asp:CheckBox id="cColumnIdentity5l" Enabled='<%# AllowEdit(LcAuth,"ColumnIdentity5") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"ColumnIdentity5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(29) %>" onclick='GridEdit("PrimaryKey5")' runat="server">
			<asp:CheckBox id="cPrimaryKey5l" Enabled='<%# AllowEdit(LcAuth,"PrimaryKey5") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"PrimaryKey5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(30) %>" onclick='GridEdit("IsIndexU5")' runat="server">
			<asp:CheckBox id="cIsIndexU5l" Enabled='<%# AllowEdit(LcAuth,"IsIndexU5") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"IsIndexU5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div><div class='GrdInner HideObjOnMobile' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(31) %>" onclick='GridEdit("IsIndex5")' runat="server">
			<asp:CheckBox id="cIsIndex5l" Enabled='<%# AllowEdit(LcAuth,"IsIndex5") && CanAct((char) "S"[0]) %>' checked='<%# base.GetBool(DataBinder.Eval(Container.DataItem,"IsIndex5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace())) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner HideObjOnTablet HideObjOnMobile' style='max-width:500px;text-align:left;' visible="<%# GridColumnVisible(32) %>" onclick='GridEdit("ColObjective5")' runat="server">
			<asp:Label id="cColObjective5l" Text='<%# DataBinder.Eval(Container.DataItem,"ColObjective5").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" style="color:#555555; font-size:12px;" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmDbTableGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmDbTableGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(19) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnId5ml" runat="server" /></div>
		    <asp:TextBox id="cColumnId5" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnId5").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:110px;text-align:right;' visible="<%# GridColumnVisible(20) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnIndex5ml" runat="server" /></div>
		    <asp:TextBox id="cColumnIndex5" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnIndex5").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:110px;text-align:right;' visible="<%# GridColumnVisible(21) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cExternalTable5ml" runat="server" /></div>
		    <asp:TextBox id="cExternalTable5" CssClass="GrdNum" Text='<%# DataBinder.Eval(Container.DataItem,"ExternalTable5").ToString() %>' MaxLength="50" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(22) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnName5ml" runat="server" /></div>
		    <asp:TextBox id="cColumnName5" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ColumnName5").ToString() %>' MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnName5" ControlToValidate="cColumnName5" display="none" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:120px;text-align:left;' visible="<%# GridColumnVisible(23) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDataType5ml" runat="server" /></div>
		<table cellspacing="0" cellpadding="0"><tr><td>
		    <asp:DropDownList id="cDataType5" CssClass="GrdDdl" AutoPostBack="true" OnSelectedIndexChanged="cDataType5_SelectedIndexChanged" DataValueField="DataType5" DataTextField="DataType5Text" runat="server" /><asp:RequiredFieldValidator id="cRFVDataType5" ControlToValidate="cDataType5" display="none" runat="server" /></td><td><asp:imagebutton id="cDataType5Search" onclick="cDataType5Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" />
		</td></tr></table>
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:60px;text-align:right;' visible="<%# GridColumnVisible(24) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnLength5ml" runat="server" /></div>
		    <asp:TextBox id="cColumnLength5" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnLength5").ToString(),base.LUser.Culture) %>' runat="server" /><asp:RequiredFieldValidator id="cRFVColumnLength5" ControlToValidate="cColumnLength5" display="none" runat="server" /><asp:RangeValidator id="cRVColumnLength5" ControlToValidate="cColumnLength5" display="none" MaximumValue="4000" MinimumValue="0" Type="Integer" runat="server" />
		</div><div class='GrdInner' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(25) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnScale5ml" runat="server" /></div>
		    <asp:TextBox id="cColumnScale5" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ColumnScale5").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(26) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDefaultValue5ml" runat="server" /></div>
		    <asp:TextBox id="cDefaultValue5" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultValue5").ToString() %>' MaxLength="50" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(27) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cAllowNulls5ml" runat="server" /></div>
		    <asp:CheckBox id="cAllowNulls5" CssClass="GrdBox" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(28) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColumnIdentity5ml" runat="server" /></div>
		    <asp:CheckBox id="cColumnIdentity5" CssClass="GrdBox" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(29) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cPrimaryKey5ml" runat="server" /></div>
		    <asp:CheckBox id="cPrimaryKey5" CssClass="GrdBox" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(30) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cIsIndexU5ml" runat="server" /></div>
		    <asp:CheckBox id="cIsIndexU5" CssClass="GrdBox" runat="server" />
		</div><div class='GrdInner' style='max-width:100px;text-align:left;' visible="<%# GridColumnVisible(31) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cIsIndex5ml" runat="server" /></div>
		    <asp:CheckBox id="cIsIndex5" CssClass="GrdBox" runat="server" />
		</div></div>
		<div></div>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:500px;text-align:left;' visible="<%# GridColumnVisible(32) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cColObjective5ml" runat="server" /></div>
		    <asp:TextBox id="cColObjective5" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ColObjective5").ToString() %>' MaxLength="200" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmDbTableGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmDbTableGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmDbTableGridDataPager" runat="server" Visible="false" PagedControlID="cAdmDbTableGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab4" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel4" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-9"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cVirtualSql3P1" class="r-td r-labelR" runat="server"><asp:Label id="cVirtualSql3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVirtualSql3P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cVirtualSql3" CssClass="inp-txt" runat="server" /><asp:Image id="cVirtualSql3E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
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
