<%@ Control Language="c#" Inherits="RO.Web.AdmClientRuleModule" CodeFile="AdmClientRuleModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmClientRule79List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmClientRule79List_SelectedIndexChanged" OnTextChanged="cAdmClientRule79List_TextChanged" OnDDFindClick="cAdmClientRule79List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ClientRuleId127" DataTextField="ClientRuleId127Text" /></div>
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
    <div class="r-td rc-1-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cClientRuleId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cClientRuleId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cClientRuleId127P2" class="r-td r-content" runat="server"><asp:TextBox id="cClientRuleId127" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleMethodId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleMethodId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleMethodId127P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRuleMethodId127" CssClass="inp-ddl" DataValueField="RuleMethodId127" DataTextField="RuleMethodId127Text" AutoPostBack="true" OnSelectedIndexChanged="cRuleMethodId127_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVRuleMethodId127" ControlToValidate="cRuleMethodId127" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleMethodDesc1295P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleMethodDesc1295Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleMethodDesc1295P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRuleMethodDesc1295" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cRuleMethodDesc1295" display="none" ErrorMessage="RuleMethodDesc <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cRuleMethodDesc1295E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleName127P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleName127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleName127P2" class="r-td r-content" runat="server"><asp:TextBox id="cRuleName127" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVRuleName127" ControlToValidate="cRuleName127" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleDescription127P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleDescription127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleDescription127P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRuleDescription127" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cRuleDescription127" display="none" ErrorMessage="RuleDescription <= 500 characters please." ValidationExpression="^[\s\S]{0,500}$" runat="server" /><asp:Image id="cRuleDescription127E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleTypeId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleTypeId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleTypeId127P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRuleTypeId127" CssClass="inp-ddl" DataValueField="RuleTypeId127" DataTextField="RuleTypeId127Text" runat="server" /><asp:RequiredFieldValidator id="cRFVRuleTypeId127" ControlToValidate="cRuleTypeId127" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenId127" DataValueField="ScreenId127" DataTextField="ScreenId127Text" AutoPostBack="true" OnSelectedIndexChanged="cScreenId127_SelectedIndexChanged" OnTextChanged="cScreenId127_TextChanged" OnDDFindClick="cScreenId127_DDFindClick" runat="server" /><asp:ImageButton id="cScreenId127Search" CssClass="r-icon" onclick="cScreenId127Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cReportId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbReportId127" DataValueField="ReportId127" DataTextField="ReportId127Text" AutoPostBack="true" OnSelectedIndexChanged="cReportId127_SelectedIndexChanged" OnTextChanged="cReportId127_TextChanged" OnDDFindClick="cReportId127_DDFindClick" runat="server" /><asp:ImageButton id="cReportId127Search" CssClass="r-icon" onclick="cReportId127Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCultureId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cCultureId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCultureId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCultureId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId127" DataValueField="CultureId127" DataTextField="CultureId127Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenObjHlpId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenObjHlpId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenObjHlpId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenObjHlpId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenObjHlpId127" DataValueField="ScreenObjHlpId127" DataTextField="ScreenObjHlpId127Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenCriHlpId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenCriHlpId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenCriHlpId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenCriHlpId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenCriHlpId127" DataValueField="ScreenCriHlpId127" DataTextField="ScreenCriHlpId127Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportCriHlpId127P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportCriHlpId127Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportCriHlpId127P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cReportCriHlpId127" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbReportCriHlpId127" DataValueField="ReportCriHlpId127" DataTextField="ReportCriHlpId127Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRuleCntTypeId127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cRuleCntTypeId127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRuleCntTypeId127P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRuleCntTypeId127" CssClass="inp-ddl" DataValueField="RuleCntTypeId127" DataTextField="RuleCntTypeId127Text" AutoPostBack="true" OnSelectedIndexChanged="cRuleCntTypeId127_SelectedIndexChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRuleCntTypeDesc1294P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cRuleCntTypeDesc1294Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRuleCntTypeDesc1294P2" class="r-td r-content" runat="server"><asp:TextBox id="cRuleCntTypeDesc1294" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientRuleProg127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cClientRuleProg127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientRuleProg127P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cClientRuleProg127" CssClass="inp-txt" runat="server" /><asp:Image id="cClientRuleProg127E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientScript127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cClientScript127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientScript127P2" class="r-td r-content" runat="server"><asp:DropDownList id="cClientScript127" CssClass="inp-ddl" DataValueField="ClientScript127" DataTextField="ClientScript127Text" AutoPostBack="true" OnSelectedIndexChanged="cClientScript127_SelectedIndexChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientScriptHelp126P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cClientScriptHelp126Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cClientScriptHelp126P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cClientScriptHelp126" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cClientScriptHelp126" display="none" ErrorMessage="ClientScriptHelp <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cClientScriptHelp126E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUserScriptEvent127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cUserScriptEvent127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUserScriptEvent127P2" class="r-td r-content" runat="server"><asp:TextBox id="cUserScriptEvent127" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUserScriptName127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cUserScriptName127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUserScriptName127P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cUserScriptName127" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cUserScriptName127" display="none" ErrorMessage="UserScriptName <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cUserScriptName127E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cScriptParam127P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cScriptParam127Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cScriptParam127P2" class="r-td r-content" runat="server"><asp:TextBox id="cScriptParam127" CssClass="inp-txt" MaxLength="500" runat="server" /></div>
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
