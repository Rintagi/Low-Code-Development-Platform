<%@ Control Language="c#" Inherits="RO.Web.AdmRptCtrModule" CodeFile="AdmRptCtrModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmRptCtr90List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmRptCtr90List_SelectedIndexChanged" OnTextChanged="cAdmRptCtr90List_TextChanged" OnDDFindClick="cAdmRptCtr90List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="RptCtrId161" DataTextField="RptCtrId161Text" /></div>
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
    <div class="r-td rc-1-7"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRptCtrId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptCtrId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptCtrId161P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptCtrId161" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cReportId161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbReportId161" DataValueField="ReportId161" DataTextField="ReportId161Text" AutoPostBack="true" OnSelectedIndexChanged="cReportId161_SelectedIndexChanged" OnTextChanged="cReportId161_TextChanged" OnDDFindClick="cReportId161_DDFindClick" runat="server" /><asp:RequiredFieldValidator id="cRFVReportId161" ControlToValidate="cReportId161" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptCtrName161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptCtrName161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptCtrName161P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptCtrName161" CssClass="inp-txt" MaxLength="100" runat="server" /><asp:RequiredFieldValidator id="cRFVRptCtrName161" ControlToValidate="cRptCtrName161" display="none" runat="server" /><asp:RegularExpressionValidator id="cREVRptCtrName161" ControlToValidate="cRptCtrName161" display="none" ValidationExpression="[A-Za-z0-9]+" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPRptCtrId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cPRptCtrId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPRptCtrId161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cPRptCtrId161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbPRptCtrId161" DataValueField="PRptCtrId161" DataTextField="PRptCtrId161Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptElmId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptElmId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptElmId161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cRptElmId161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbRptElmId161" DataValueField="RptElmId161" DataTextField="RptElmId161Text" runat="server" /><asp:ImageButton id="cRptElmId161Search" CssClass="r-icon" onclick="cRptElmId161Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptCelId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptCelId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptCelId161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cRptCelId161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbRptCelId161" DataValueField="RptCelId161" DataTextField="RptCelId161Text" runat="server" /><asp:ImageButton id="cRptCelId161Search" CssClass="r-icon" onclick="cRptCelId161Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptStyleId161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptStyleId161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptStyleId161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cRptStyleId161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbRptStyleId161" DataValueField="RptStyleId161" DataTextField="RptStyleId161Text" runat="server" /><asp:ImageButton id="cRptStyleId161Search" CssClass="r-icon" onclick="cRptStyleId161Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-10"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRptCtrTypeCd161P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptCtrTypeCd161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptCtrTypeCd161P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRptCtrTypeCd161" CssClass="inp-ddl" DataValueField="RptCtrTypeCd161" DataTextField="RptCtrTypeCd161Text" runat="server" /><asp:RequiredFieldValidator id="cRFVRptCtrTypeCd161" ControlToValidate="cRptCtrTypeCd161" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrTop161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrTop161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrTop161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrTop161" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrLeft161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrLeft161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrLeft161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrLeft161" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrHeight161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrHeight161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrHeight161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrHeight161" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrWidth161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrWidth161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrWidth161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrWidth161" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrZIndex161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrZIndex161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrZIndex161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrZIndex161" CssClass="inp-num" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-11-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cCtrPgBrStart161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrPgBrStart161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrPgBrStart161P2" class="r-td r-content" runat="server"><asp:CheckBox id="cCtrPgBrStart161" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrPgBrEnd161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrPgBrEnd161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrPgBrEnd161P2" class="r-td r-content" runat="server"><asp:CheckBox id="cCtrPgBrEnd161" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrCanGrow161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrCanGrow161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrCanGrow161P2" class="r-td r-content" runat="server"><asp:CheckBox id="cCtrCanGrow161" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrCanShrink161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrCanShrink161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrCanShrink161P2" class="r-td r-content" runat="server"><asp:CheckBox id="cCtrCanShrink161" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrTogether161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrTogether161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrTogether161P2" class="r-td r-content" runat="server"><asp:CheckBox id="cCtrTogether161" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cCtrValue161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrValue161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrValue161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrValue161" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrAction161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrAction161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrAction161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrAction161" CssClass="inp-txt" MaxLength="500" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrVisibility161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrVisibility161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrVisibility161P2" class="r-td r-content" runat="server">    		<div><asp:RadioButtonList id="cCtrVisibility161" class="inp-rad" RepeatDirection="Horizontal" DataValueField="CtrVisibility161" DataTextField="CtrVisibility161Text" runat="server" /></div></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrToggle161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrToggle161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrToggle161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCtrToggle161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCtrToggle161" DataValueField="CtrToggle161" DataTextField="CtrToggle161Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrGrouping161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrGrouping161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrGrouping161P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCtrGrouping161" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCtrGrouping161" DataValueField="CtrGrouping161" DataTextField="CtrGrouping161Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCtrToolTip161P1" class="r-td r-labelR" runat="server"><asp:Label id="cCtrToolTip161Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCtrToolTip161P2" class="r-td r-content" runat="server"><asp:TextBox id="cCtrToolTip161" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
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
