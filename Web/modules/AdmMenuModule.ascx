<%@ Control Language="c#" Inherits="RO.Web.AdmMenuModule" CodeFile="AdmMenuModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmMenu35List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmMenu35List_SelectedIndexChanged" OnTextChanged="cAdmMenu35List_TextChanged" OnDDFindClick="cAdmMenu35List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="MenuId39" DataTextField="MenuId39Text" /></div>
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
    <div class="r-table rg-1-6"><div class="r-tr">
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cMenuId39P1" class="r-td r-labelR" runat="server"><asp:Label id="cMenuId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMenuId39P2" class="r-td r-content" runat="server"><asp:TextBox id="cMenuId39" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPopup39P1" class="r-td r-labelR" runat="server"><asp:Label id="cPopup39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPopup39P2" class="r-td r-content" runat="server"><asp:CheckBox id="cPopup39" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cParentId39P1" class="r-td r-labelR" runat="server"><asp:Label id="cParentId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cParentId39P2" class="r-td r-content" runat="server"><asp:TextBox id="cParentId39" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMenuIndex39P1" class="r-td r-labelR" runat="server"><asp:Label id="cMenuIndex39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMenuIndex39P2" class="r-td r-content" runat="server"><asp:TextBox id="cMenuIndex39" CssClass="inp-num" runat="server" /><asp:RangeValidator id="cRVMenuIndex39" ControlToValidate="cMenuIndex39" display="none" MaximumValue="9999" MinimumValue="0" Type="Integer" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cScreenId39P1" class="r-td r-labelR" style="color:#555;" runat="server"><asp:Label id="cScreenId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cScreenId39P2" class="r-td r-content" style="color:#555;" runat="server"><rcasp:ComboBox autocomplete="off" id="cScreenId39" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbScreenId39" DataValueField="ScreenId39" DataTextField="ScreenId39Text" runat="server" /><asp:ImageButton id="cScreenId39Search" CssClass="r-icon" onclick="cScreenId39Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportId39P1" class="r-td r-labelR" style="color:#555;" runat="server"><asp:Label id="cReportId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId39P2" class="r-td r-content" style="color:#555;" runat="server"><rcasp:ComboBox autocomplete="off" id="cReportId39" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbReportId39" DataValueField="ReportId39" DataTextField="ReportId39Text" runat="server" /><asp:ImageButton id="cReportId39Search" CssClass="r-icon" onclick="cReportId39Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cWizardId39P1" class="r-td r-labelR" style="color:#555;" runat="server"><asp:Label id="cWizardId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cWizardId39P2" class="r-td r-content" style="color:#555;" runat="server"><rcasp:ComboBox autocomplete="off" id="cWizardId39" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbWizardId39" DataValueField="WizardId39" DataTextField="WizardId39Text" runat="server" /><asp:ImageButton id="cWizardId39Search" CssClass="r-icon" onclick="cWizardId39Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cStaticPgId39P1" class="r-td r-labelR" style="color:#555;" runat="server"><asp:Label id="cStaticPgId39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cStaticPgId39P2" class="r-td r-content" style="color:#555;" runat="server"><rcasp:ComboBox autocomplete="off" id="cStaticPgId39" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbStaticPgId39" DataValueField="StaticPgId39" DataTextField="StaticPgId39Text" runat="server" /><asp:ImageButton id="cStaticPgId39Search" CssClass="r-icon" onclick="cStaticPgId39Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMiscellaneous39P1" class="r-td r-labelR" style="color:#333;" runat="server"><asp:Label id="cMiscellaneous39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMiscellaneous39P2" class="r-td r-content" style="color:#333;" runat="server"><asp:TextBox id="cMiscellaneous39" CssClass="inp-txt" MaxLength="1000" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cIconUrl39P1" class="r-td r-labelR" style="color:#333;" runat="server"><asp:Label id="cIconUrl39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cIconUrl39P2" class="r-td r-content" style="color:#333;" runat="server">
    			<asp:TextBox id="cIconUrl39Div" CssClass="inp-txt" Visible="false" runat="server" />
    			<asp:ImageButton id="cIconUrl39" OnClientClick='NoConfirm()' OnClick="cIconUrl39_Click" runat="server" />
    		<asp:ImageButton id="cIconUrl39Tgo" OnClientClick='NoConfirm()' onclick="cIconUrl39Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReactQuickMenu39P1" class="r-td r-labelR" runat="server"><asp:Label id="cReactQuickMenu39Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReactQuickMenu39P2" class="r-td r-content" runat="server"><asp:CheckBox id="cReactQuickMenu39" CssClass="inp-chk" runat="server" /></div>
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
