<%@ Control Language="c#" Inherits="RO.Web.AdmWebRuleModule" CodeFile="AdmWebRuleModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmWebRule80List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmWebRule80List_SelectedIndexChanged" OnTextChanged="cAdmWebRule80List_TextChanged" OnDDFindClick="cAdmWebRule80List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="WebRuleId128" DataTextField="WebRuleId128Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab39" runat="server"/>
<ul id="tabs">
    <li><a id="cTab39" href="#" class="current" name="Tab39" runat="server"></a></li>
    <li><a id="cTab125" href="#" name="Tab125" runat="server"></a></li>
    <li><a id="cTab126" href="#" name="Tab126" runat="server"></a></li>
    <li><a id="cTab127" href="#" name="Tab127" runat="server"></a></li>
    <li><a id="cTab128" href="#" name="Tab128" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab39" runat="server">
    <asp:UpdatePanel id="UpdPanel39" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cWebRuleId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cWebRuleId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cWebRuleId128P2" class="r-td r-content" runat="server"><asp:TextBox id="cWebRuleId128" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleName128P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleName128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleName128P2" class="r-td r-content" runat="server"><asp:TextBox id="cRuleName128" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVRuleName128" ControlToValidate="cRuleName128" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleDescription128P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleDescription128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleDescription128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRuleDescription128" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cRuleDescription128" display="none" ErrorMessage="RuleDescription <= 500 characters please." ValidationExpression="^[\s\S]{0,500}$" runat="server" /><asp:Image id="cRuleDescription128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRuleTypeId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cRuleTypeId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRuleTypeId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRuleTypeId128" CssClass="inp-ddl" DataValueField="RuleTypeId128" DataTextField="RuleTypeId128Text" runat="server" /><asp:RequiredFieldValidator id="cRFVRuleTypeId128" ControlToValidate="cRuleTypeId128" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId128P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenId128" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenId128" DataValueField="ScreenId128" DataTextField="ScreenId128Text" AutoPostBack="true" OnSelectedIndexChanged="cScreenId128_SelectedIndexChanged" OnTextChanged="cScreenId128_TextChanged" OnDDFindClick="cScreenId128_DDFindClick" runat="server" /><asp:RequiredFieldValidator id="cRFVScreenId128" ControlToValidate="cScreenId128" display="none" runat="server" /><asp:ImageButton id="cScreenId128Search" CssClass="r-icon" onclick="cScreenId128Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenObjId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cScreenObjId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenObjId128P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenObjId128" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenObjId128" DataValueField="ScreenObjId128" DataTextField="ScreenObjId128Text" AutoPostBack="true" OnSelectedIndexChanged="cScreenObjId128_SelectedIndexChanged" OnTextChanged="cScreenObjId128_TextChanged" OnDDFindClick="cScreenObjId128_DDFindClick" runat="server" /><asp:ImageButton id="cScreenObjId128Search" CssClass="r-icon" onclick="cScreenObjId128Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cButtonTypeId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cButtonTypeId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cButtonTypeId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cButtonTypeId128" CssClass="inp-ddl" DataValueField="ButtonTypeId128" DataTextField="ButtonTypeId128Text" AutoPostBack="true" OnSelectedIndexChanged="cButtonTypeId128_SelectedIndexChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cEventId128P1" class="r-td r-labelR" runat="server"><asp:Label id="cEventId128Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cEventId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cEventId128" CssClass="inp-ddl" DataValueField="EventId128" DataTextField="EventId128Text" runat="server" /><asp:RequiredFieldValidator id="cRFVEventId128" ControlToValidate="cEventId128" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-11"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cWebRuleProg128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cWebRuleProg128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cWebRuleProg128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cWebRuleProg128" CssClass="inp-txt" runat="server" /><asp:Image id="cWebRuleProg128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSnippet1P1" class="r-td r-labelR" runat="server"><asp:Label id="cSnippet1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSnippet1P2" class="r-td r-content" runat="server"><asp:ImageButton id="cSnippet1" OnClientClick='NoConfirm()' OnClick="cSnippet1_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSnippet4P1" class="r-td r-labelR" runat="server"><asp:Label id="cSnippet4Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSnippet4P2" class="r-td r-content" runat="server"><asp:ImageButton id="cSnippet4" OnClientClick='NoConfirm()' OnClick="cSnippet4_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSnippet2P1" class="r-td r-labelR" runat="server"><asp:Label id="cSnippet2Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSnippet2P2" class="r-td r-content" runat="server"><asp:ImageButton id="cSnippet2" OnClientClick='NoConfirm()' OnClick="cSnippet2_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSnippet3P1" class="r-td r-labelR" runat="server"><asp:Label id="cSnippet3Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSnippet3P2" class="r-td r-content" runat="server"><asp:ImageButton id="cSnippet3" OnClientClick='NoConfirm()' OnClick="cSnippet3_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab125" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel125" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReactEventId128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cReactEventId128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReactEventId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReactEventId128" CssClass="inp-ddl" DataValueField="ReactEventId128" DataTextField="ReactEventId128Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReactRuleProg128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cReactRuleProg128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReactRuleProg128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cReactRuleProg128" CssClass="inp-txt" runat="server" /><asp:Image id="cReactRuleProg128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab126" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel126" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReduxEventId128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cReduxEventId128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReduxEventId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReduxEventId128" CssClass="inp-ddl" DataValueField="ReduxEventId128" DataTextField="ReduxEventId128Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReduxRuleProg128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cReduxRuleProg128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cReduxRuleProg128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cReduxRuleProg128" CssClass="inp-txt" runat="server" /><asp:Image id="cReduxRuleProg128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab127" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel127" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServiceEventId128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cServiceEventId128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServiceEventId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cServiceEventId128" CssClass="inp-ddl" DataValueField="ServiceEventId128" DataTextField="ServiceEventId128Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServiceRuleProg128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cServiceRuleProg128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServiceRuleProg128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cServiceRuleProg128" CssClass="inp-txt" runat="server" /><asp:Image id="cServiceRuleProg128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab128" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel128" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cAsmxEventId128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cAsmxEventId128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cAsmxEventId128P2" class="r-td r-content" runat="server"><asp:DropDownList id="cAsmxEventId128" CssClass="inp-ddl" DataValueField="AsmxEventId128" DataTextField="AsmxEventId128Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cAsmxRuleProg128P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cAsmxRuleProg128Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cAsmxRuleProg128P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cAsmxRuleProg128" CssClass="inp-txt" runat="server" /><asp:Image id="cAsmxRuleProg128E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
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
