<%@ Control Language="c#" Inherits="RO.Web.AdmRptStyleModule" CodeFile="AdmRptStyleModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmRptStyle89List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmRptStyle89List_SelectedIndexChanged" OnTextChanged="cAdmRptStyle89List_TextChanged" OnDDFindClick="cAdmRptStyle89List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="RptStyleId167" DataTextField="RptStyleId167Text" /></div>
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
    <div class="r-td rc-1-2"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRptStyleId167P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptStyleId167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptStyleId167P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptStyleId167" CssClass="inp-num" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-3-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cDefaultCd167P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefaultCd167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefaultCd167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDefaultCd167" CssClass="inp-ddl" DataValueField="DefaultCd167" DataTextField="DefaultCd167Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRptStyleDesc167P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptStyleDesc167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptStyleDesc167P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptStyleDesc167" CssClass="inp-txt" MaxLength="300" runat="server" /><asp:RequiredFieldValidator id="cRFVRptStyleDesc167" ControlToValidate="cRptStyleDesc167" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBorderColorD167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderColorD167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderColorD167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderColorD167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBorderColorD167Search" CssClass="r-icon" onclick="cBorderColorD167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderColorL167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderColorL167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderColorL167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderColorL167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBorderColorL167Search" CssClass="r-icon" onclick="cBorderColorL167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderColorR167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderColorR167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderColorR167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderColorR167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBorderColorR167Search" CssClass="r-icon" onclick="cBorderColorR167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderColorT167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderColorT167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderColorT167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderColorT167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBorderColorT167Search" CssClass="r-icon" onclick="cBorderColorT167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderColorB167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderColorB167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderColorB167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderColorB167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBorderColorB167Search" CssClass="r-icon" onclick="cBorderColorB167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cColor167P1" class="r-td r-labelR" runat="server"><asp:Label id="cColor167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cColor167P2" class="r-td r-content" runat="server"><asp:TextBox id="cColor167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cColor167Search" CssClass="r-icon" onclick="cColor167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBgColor167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBgColor167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBgColor167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBgColor167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBgColor167Search" CssClass="r-icon" onclick="cBgColor167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBgGradType167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBgGradType167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBgGradType167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBgGradType167" CssClass="inp-ddl" DataValueField="BgGradType167" DataTextField="BgGradType167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBgGradColor167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBgGradColor167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBgGradColor167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBgGradColor167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cBgGradColor167Search" CssClass="r-icon" onclick="cBgGradColor167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBgImage167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBgImage167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBgImage167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBgImage167" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cDirection167P1" class="r-td r-labelR" runat="server"><asp:Label id="cDirection167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDirection167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDirection167" CssClass="inp-ddl" DataValueField="Direction167" DataTextField="Direction167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cWritingMode167P1" class="r-td r-labelR" runat="server"><asp:Label id="cWritingMode167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cWritingMode167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cWritingMode167" CssClass="inp-ddl" DataValueField="WritingMode167" DataTextField="WritingMode167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLineHeight167P1" class="r-td r-labelR" runat="server"><asp:Label id="cLineHeight167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLineHeight167P2" class="r-td r-content" runat="server"><asp:TextBox id="cLineHeight167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cFormat167P1" class="r-td r-labelR" runat="server"><asp:Label id="cFormat167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFormat167P2" class="r-td r-content" runat="server"><asp:TextBox id="cFormat167" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:ImageButton id="cFormat167Search" CssClass="r-icon" onclick="cFormat167Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderStyleD167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderStyleD167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderStyleD167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBorderStyleD167" CssClass="inp-ddl" DataValueField="BorderStyleD167" DataTextField="BorderStyleD167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderStyleL167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderStyleL167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderStyleL167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBorderStyleL167" CssClass="inp-ddl" DataValueField="BorderStyleL167" DataTextField="BorderStyleL167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderStyleR167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderStyleR167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderStyleR167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBorderStyleR167" CssClass="inp-ddl" DataValueField="BorderStyleR167" DataTextField="BorderStyleR167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderStyleT167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderStyleT167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderStyleT167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBorderStyleT167" CssClass="inp-ddl" DataValueField="BorderStyleT167" DataTextField="BorderStyleT167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderStyleB167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderStyleB167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderStyleB167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cBorderStyleB167" CssClass="inp-ddl" DataValueField="BorderStyleB167" DataTextField="BorderStyleB167Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cFontStyle167P1" class="r-td r-labelR" runat="server"><asp:Label id="cFontStyle167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFontStyle167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cFontStyle167" CssClass="inp-ddl" DataValueField="FontStyle167" DataTextField="FontStyle167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cFontFamily167P1" class="r-td r-labelR" runat="server"><asp:Label id="cFontFamily167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFontFamily167P2" class="r-td r-content" runat="server"><asp:TextBox id="cFontFamily167" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cFontSize167P1" class="r-td r-labelR" runat="server"><asp:Label id="cFontSize167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFontSize167P2" class="r-td r-content" runat="server"><asp:TextBox id="cFontSize167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cFontWeight167P1" class="r-td r-labelR" runat="server"><asp:Label id="cFontWeight167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFontWeight167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cFontWeight167" CssClass="inp-ddl" DataValueField="FontWeight167" DataTextField="FontWeight167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTextDecor167P1" class="r-td r-labelR" runat="server"><asp:Label id="cTextDecor167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTextDecor167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cTextDecor167" CssClass="inp-ddl" DataValueField="TextDecor167" DataTextField="TextDecor167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTextAlign167P1" class="r-td r-labelR" runat="server"><asp:Label id="cTextAlign167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTextAlign167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cTextAlign167" CssClass="inp-ddl" DataValueField="TextAlign167" DataTextField="TextAlign167Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVerticalAlign167P1" class="r-td r-labelR" runat="server"><asp:Label id="cVerticalAlign167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVerticalAlign167P2" class="r-td r-content" runat="server"><asp:DropDownList id="cVerticalAlign167" CssClass="inp-ddl" DataValueField="VerticalAlign167" DataTextField="VerticalAlign167Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cBorderWidthD167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderWidthD167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderWidthD167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderWidthD167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderWidthL167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderWidthL167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderWidthL167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderWidthL167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderWidthR167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderWidthR167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderWidthR167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderWidthR167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderWidthT167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderWidthT167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderWidthT167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderWidthT167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorderWidthB167P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorderWidthB167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorderWidthB167P2" class="r-td r-content" runat="server"><asp:TextBox id="cBorderWidthB167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPadLeft167P1" class="r-td r-labelR" runat="server"><asp:Label id="cPadLeft167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPadLeft167P2" class="r-td r-content" runat="server"><asp:TextBox id="cPadLeft167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPadRight167P1" class="r-td r-labelR" runat="server"><asp:Label id="cPadRight167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPadRight167P2" class="r-td r-content" runat="server"><asp:TextBox id="cPadRight167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPadTop167P1" class="r-td r-labelR" runat="server"><asp:Label id="cPadTop167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPadTop167P2" class="r-td r-content" runat="server"><asp:TextBox id="cPadTop167" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPadBottom167P1" class="r-td r-labelR" runat="server"><asp:Label id="cPadBottom167Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPadBottom167P2" class="r-td r-content" runat="server"><asp:TextBox id="cPadBottom167" CssClass="inp-num" runat="server" /></div>
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
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:PlaceHolder ID="LstPHolder" runat="server" Visible="false" />
