<%@ Control Language="c#" Inherits="RO.Web.AdmScreenObjModule" CodeFile="AdmScreenObjModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
      $(document).ready(function () { var old = null; try {old = Page_ClientValidate;} catch(e){}; Page_ClientValidate = function (g) {if (old && typeof(old) == 'function') {$.watermark.hideAll();Page_BlockSubmit = false && !old(g);} else return true; if (Page_BlockSubmit) { ValidateThisModule(<%= this.ClientID %>); $('#<%=cValidSummary.ClientID%>:visible').hide();} ; if (Page_BlockSubmit && $('#<%=bPgDirty.ClientID%>').val() == 'Y') {$('#<%=bConfirm.ClientID%>').val('Y');}; return !Page_BlockSubmit;}});
	Sys.Application.add_load(function () { $('input[type=hidden]').each(function (i, e) { try { e.defaultValue = e.value; } catch (er) { } }); WatermarkInput(<%= this.ClientID %>,'<%= PanelUpd.ClientID %>');});
	Sys.Application.add_load(function () { var hlp={}; $('a.GrdHead').each(function(i,e){var ids = ($(this).attr('id')||'').split('_'); hlp[ids[ids.length-1].replace(/hl$/,'l')] = $(this).attr('title');});$('span.GrdTxtLb,a.GrdTxtLn,a.GrdBoxLn').each(function (i, e) {try { var style = ($(this).attr('style')||'').replace(/height/i, 'max-height'); var ids = ($(this).attr('id')||'').split('_'); $(this).attr('title', hlp[ids[ids.length-1]]).attr('style', style); } catch (e) { } }); });
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChkExpNow)
	Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ChkPgDirty)
	Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
	function NoConfirm() {document.getElementById('<%=bConfirm.ClientID%>').value = 'N';}
	function ChkPgDirty()
	{
		var x = document.getElementById('<%=bPgDirty.ClientID%>');
		var y = document.getElementById('<%=cPgDirty.ClientID%>');
		if (y != null) {if (x != null && x.value == 'Y') {y.style.visibility = '';} else {y.style.visibility = 'hidden';}}
	}
	function initializeRequestHandler(sender, args) {if (!fConfirm2('<%=bPgDirty.ClientID%>','<%=bConfirm.ClientID%>','<%=aNam.ClientID%>','<%=aVal.ClientID%>')) {args.set_cancel(true);} else try {$.watermark.hideAll();} catch (e) { }}
	function beginRequestHandler(sender, e) { e.get_postBackElement().disabled=true; ShowProgress(); document.body.style.cursor='wait'; }
	function endRequestHandler() { initPageLoad=true; HideProgress(); document.body.style.cursor='auto'; var v = $('#<%=cValidSummary.ClientID%>:visible'); if (v.length > 0 && typeof(Page_Validators) != 'undefined' && $(Page_Validators).length > 0) {ValidateThisModule(<%= this.ClientID %>);v.hide();}; }
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
<asp:UpdatePanel ID="PanelUpd" runat="server"><Triggers></Triggers><ContentTemplate>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmScreenObj10List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmScreenObj10List_SelectedIndexChanged" OnTextChanged="cAdmScreenObj10List_TextChanged" OnDDFindClick="cAdmScreenObj10List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ScreenObjId14" DataTextField="ScreenObjId14Text" /></div>
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
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cScreenObjId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenObjId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenObjId14P2" class="r-td r-content" runat="server"><asp:TextBox id="cScreenObjId14" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMasterTable14P1" class="r-td r-labelR" runat="server"><asp:Label id="cMasterTable14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMasterTable14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cMasterTable14" CssClass="inp-chk" AutoPostBack="true" OnCheckedChanged="cMasterTable14_CheckedChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRequiredValid14P1" class="r-td r-labelR" runat="server"><asp:Label id="cRequiredValid14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRequiredValid14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cRequiredValid14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnWrap14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnWrap14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnWrap14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cColumnWrap14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGridGrpCd14P1" class="r-td r-labelR" runat="server"><asp:Label id="cGridGrpCd14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGridGrpCd14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cGridGrpCd14" CssClass="inp-ddl" DataValueField="GridGrpCd14" DataTextField="GridGrpCd14Text" runat="server" /><asp:RequiredFieldValidator id="cRFVGridGrpCd14" ControlToValidate="cGridGrpCd14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHideOnTablet14P1" class="r-td r-labelR" runat="server"><asp:Label id="cHideOnTablet14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHideOnTablet14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cHideOnTablet14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHideOnMobile14P1" class="r-td r-labelR" runat="server"><asp:Label id="cHideOnMobile14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHideOnMobile14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cHideOnMobile14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRefreshOnCUD14P1" class="r-td r-labelR" runat="server"><asp:Label id="cRefreshOnCUD14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRefreshOnCUD14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cRefreshOnCUD14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTrimOnEntry14P1" class="r-td r-labelR" runat="server"><asp:Label id="cTrimOnEntry14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTrimOnEntry14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cTrimOnEntry14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cIgnoreConfirm14P1" class="r-td r-labelR" runat="server"><asp:Label id="cIgnoreConfirm14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cIgnoreConfirm14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cIgnoreConfirm14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnJustify14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnJustify14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnJustify14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cColumnJustify14" CssClass="inp-ddl" DataValueField="ColumnJustify14" DataTextField="ColumnJustify14Text" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnJustify14" ControlToValidate="cColumnJustify14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnSize14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnSize14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnSize14P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnSize14" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnHeight14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnHeight14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnHeight14P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnHeight14" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cResizeWidth14P1" class="r-td r-labelR" runat="server"><asp:Label id="cResizeWidth14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cResizeWidth14P2" class="r-td r-content" runat="server"><asp:TextBox id="cResizeWidth14" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cResizeHeight14P1" class="r-td r-labelR" runat="server"><asp:Label id="cResizeHeight14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cResizeHeight14P2" class="r-td r-content" runat="server"><asp:TextBox id="cResizeHeight14" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSortOrder14P1" class="r-td r-labelR" runat="server"><asp:Label id="cSortOrder14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSortOrder14P2" class="r-td r-content" runat="server"><asp:TextBox id="cSortOrder14" CssClass="inp-num" runat="server" /><asp:RegularExpressionValidator id="cREVSortOrder14" ControlToValidate="cSortOrder14" display="none" ValidationExpression="-?[1-9]?" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cScreenId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenId14" DataValueField="ScreenId14" DataTextField="ScreenId14Text" runat="server" /><asp:RequiredFieldValidator id="cRFVScreenId14" ControlToValidate="cScreenId14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGroupRowId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cGroupRowId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGroupRowId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGroupRowId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGroupRowId14" DataValueField="GroupRowId14" DataTextField="GroupRowId14Text" runat="server" /><asp:RequiredFieldValidator id="cRFVGroupRowId14" ControlToValidate="cGroupRowId14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGroupColId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cGroupColId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGroupColId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGroupColId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGroupColId14" DataValueField="GroupColId14" DataTextField="GroupColId14Text" runat="server" /><asp:RequiredFieldValidator id="cRFVGroupColId14" ControlToValidate="cGroupColId14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbColumnId14" DataValueField="ColumnId14" DataTextField="ColumnId14Text" AutoPostBack="true" OnSelectedIndexChanged="cColumnId14_SelectedIndexChanged" OnTextChanged="cColumnId14_TextChanged" OnDDFindClick="cColumnId14_DDFindClick" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnName14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnName14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnName14P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnName14" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVColumnName14" ControlToValidate="cColumnName14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDisplayModeId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDisplayModeId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDisplayModeId14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDisplayModeId14" CssClass="inp-ddl" DataValueField="DisplayModeId14" DataTextField="DisplayModeId14Text" AutoPostBack="true" OnSelectedIndexChanged="cDisplayModeId14_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVDisplayModeId14" ControlToValidate="cDisplayModeId14" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDisplayDesc18P1" class="r-td r-labelR" runat="server"><asp:Label id="cDisplayDesc18Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDisplayDesc18P2" class="r-td r-content" style="font-size:8pt;" runat="server"><asp:TextBox TextMode="MultiLine" id="cDisplayDesc18" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cDisplayDesc18" display="none" ErrorMessage="DisplayDesc <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cDisplayDesc18E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlKeyColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlKeyColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlKeyColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlKeyColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlKeyColumnId14" DataValueField="DdlKeyColumnId14" DataTextField="DdlKeyColumnId14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlRefColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlRefColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlRefColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlRefColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlRefColumnId14" DataValueField="DdlRefColumnId14" DataTextField="DdlRefColumnId14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlSrtColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlSrtColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlSrtColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlSrtColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlSrtColumnId14" DataValueField="DdlSrtColumnId14" DataTextField="DdlSrtColumnId14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlAdnColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlAdnColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlAdnColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlAdnColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlAdnColumnId14" DataValueField="DdlAdnColumnId14" DataTextField="DdlAdnColumnId14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDdlFtrColumnId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDdlFtrColumnId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDdlFtrColumnId14P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cDdlFtrColumnId14" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbDdlFtrColumnId14" DataValueField="DdlFtrColumnId14" DataTextField="DdlFtrColumnId14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColumnLink14P1" class="r-td r-labelR" runat="server"><asp:Label id="cColumnLink14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColumnLink14P2" class="r-td r-content" runat="server"><asp:TextBox id="cColumnLink14" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDtlLstPosId14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDtlLstPosId14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDtlLstPosId14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDtlLstPosId14" CssClass="inp-ddl" DataValueField="DtlLstPosId14" DataTextField="DtlLstPosId14Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cLabelVertical14P1" class="r-td r-labelR" runat="server"><asp:Label id="cLabelVertical14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLabelVertical14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cLabelVertical14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLabelCss14P1" class="r-td r-labelR" runat="server"><asp:Label id="cLabelCss14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLabelCss14P2" class="r-td r-content" runat="server"><asp:TextBox id="cLabelCss14" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cContentCss14P1" class="r-td r-labelR" runat="server"><asp:Label id="cContentCss14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cContentCss14P2" class="r-td r-content" runat="server"><asp:TextBox id="cContentCss14" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefaultValue14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefaultValue14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefaultValue14P2" class="r-td r-content" runat="server"><asp:TextBox id="cDefaultValue14" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHyperLinkUrl14P1" class="r-td r-labelR" runat="server"><asp:Label id="cHyperLinkUrl14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHyperLinkUrl14P2" class="r-td r-content" runat="server"><asp:TextBox id="cHyperLinkUrl14" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefAfter14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefAfter14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefAfter14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cDefAfter14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSystemValue14P1" class="r-td r-labelR" runat="server"><asp:Label id="cSystemValue14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSystemValue14P2" class="r-td r-content" runat="server"><asp:TextBox id="cSystemValue14" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefAlways14P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefAlways14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefAlways14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cDefAlways14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAggregateCd14P1" class="r-td r-labelR" runat="server"><asp:Label id="cAggregateCd14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAggregateCd14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cAggregateCd14" CssClass="inp-ddl" DataValueField="AggregateCd14" DataTextField="AggregateCd14Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGenerateSp14P1" class="r-td r-labelR" runat="server"><asp:Label id="cGenerateSp14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGenerateSp14P2" class="r-td r-content" runat="server"><asp:CheckBox id="cGenerateSp14" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMaskValid14P1" class="r-td r-labelR" runat="server"><asp:Label id="cMaskValid14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMaskValid14P2" class="r-td r-content" runat="server"><asp:TextBox id="cMaskValid14" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRangeValidType14P1" class="r-td r-labelR" runat="server"><asp:Label id="cRangeValidType14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRangeValidType14P2" class="r-td r-content" runat="server"><asp:TextBox id="cRangeValidType14" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRangeValidMax14P1" class="r-td r-labelR" runat="server"><asp:Label id="cRangeValidMax14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRangeValidMax14P2" class="r-td r-content" runat="server"><asp:TextBox id="cRangeValidMax14" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRangeValidMin14P1" class="r-td r-labelR" runat="server"><asp:Label id="cRangeValidMin14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRangeValidMin14P2" class="r-td r-content" runat="server"><asp:TextBox id="cRangeValidMin14" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMatchCd14P1" class="r-td r-labelR" runat="server"><asp:Label id="cMatchCd14Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMatchCd14P2" class="r-td r-content" runat="server"><asp:DropDownList id="cMatchCd14" CssClass="inp-ddl" DataValueField="MatchCd14" DataTextField="MatchCd14Text" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
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
