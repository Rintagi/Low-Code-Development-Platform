<%@ Control Language="c#" Inherits="RO.Web.AdmSignupModule" CodeFile="AdmSignupModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmSignup1018List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmSignup1018List_SelectedIndexChanged" OnTextChanged="cAdmSignup1018List_TextChanged" OnDDFindClick="cAdmSignup1018List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="UsrId270" DataTextField="UsrId270Text" /></div>
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
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace7P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace7" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSignUpTitleP2" class="r-td r-content signUpTitle" runat="server"><asp:Label id="cSignUpTitle" CssClass="inp-lab" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSignUpTopMsgP2" class="r-td r-content signUpLabel" runat="server"><asp:Label id="cSignUpTopMsg" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace8P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace8" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace1P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace1" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUsrId270P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrId270Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrId270P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrId270" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrName270P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrName270Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrName270P2" class="r-td r-content signUpInput" runat="server"><asp:TextBox id="cUsrName270" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLoginName270P1" class="r-td r-labelR" runat="server"><asp:Label id="cLoginName270Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLoginName270P2" class="r-td r-content signUpInput" runat="server"><asp:TextBox id="cLoginName270" CssClass="inp-txt" MaxLength="32" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrEmail270P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrEmail270Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrEmail270P2" class="r-td r-content signUpInput" runat="server"><asp:TextBox id="cUsrEmail270" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrPassword270P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrPassword270Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrPassword270P2" class="r-td r-content signUpInput" runat="server"><asp:TextBox TextMode="Password" autocomplete="new-password" id="cUsrPassword270" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace2P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace2" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace3P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace3" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-7"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cTokenMsgP2" class="r-td r-content tokenMsg" runat="server"><asp:Label id="cTokenMsg" CssClass="inp-lab" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cConfirmationTokenP1" class="r-td r-labelR" runat="server"><asp:Label id="cConfirmationTokenLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cConfirmationTokenP2" class="r-td r-content signUpInput" runat="server"><asp:TextBox id="cConfirmationToken" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTokenP1" class="r-td r-labelR" runat="server"><asp:Label id="cTokenLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTokenP2" class="r-td r-content" runat="server"><asp:TextBox id="cToken" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace9P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace9" CssClass="inp-lab" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cResnedTokenP1" class="r-td r-labelR" runat="server"><asp:Label id="cResnedTokenLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cResnedTokenP2" class="r-td r-content sendTokenBtn" runat="server"><asp:Button id="cResnedToken" CssClass="small blue button" OnClick="cResnedToken_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace4P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace4" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace5P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace5" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSubmitP1" class="r-td r-labelR" runat="server"><asp:Label id="cSubmitLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSubmitP2" class="r-td r-content submitBtn" runat="server"><asp:Button id="cSubmit" CssClass="small blue button" OnClick="cSubmit_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSignUpBtnP1" class="r-td r-labelR" runat="server"><asp:Label id="cSignUpBtnLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSignUpBtnP2" class="r-td r-content signUpBtn" runat="server"><asp:Button id="cSignUpBtn" CssClass="small blue button" OnClick="cSignUpBtn_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace6P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace6" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace10P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace10" CssClass="inp-lab" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<asp:PlaceHolder id="cSignUpMsg" runat="server" />
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cDummyWhiteSpace11P2" class="r-td r-content" runat="server"><asp:Label id="cDummyWhiteSpace11" CssClass="inp-lab" runat="server" /></div>
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
