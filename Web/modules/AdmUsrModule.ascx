<%@ Control Language="c#" Inherits="RO.Web.AdmUsrModule" CodeFile="AdmUsrModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmUsr1List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmUsr1List_SelectedIndexChanged" OnTextChanged="cAdmUsr1List_TextChanged" OnDDFindClick="cAdmUsr1List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="UsrId1" DataTextField="UsrId1Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab1" runat="server"/>
<ul id="tabs">
    <li><a id="cTab1" href="#" class="current" name="Tab1" runat="server"></a></li>
    <li><a id="cTab2" href="#" name="Tab2" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab1" runat="server">
    <asp:UpdatePanel id="UpdPanel1" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUsrId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrId1P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrId1" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLoginName1P1" class="r-td r-labelR" runat="server"><asp:Label id="cLoginName1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLoginName1P2" class="r-td r-content" runat="server"><asp:TextBox id="cLoginName1" CssClass="inp-txt" MaxLength="32" runat="server" /><asp:RequiredFieldValidator id="cRFVLoginName1" ControlToValidate="cLoginName1" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrName1P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrName1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrName1P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrName1" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVUsrName1" ControlToValidate="cUsrName1" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCultureId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cCultureId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCultureId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCultureId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId1" DataValueField="CultureId1" DataTextField="CultureId1Text" runat="server" /><asp:RequiredFieldValidator id="cRFVCultureId1" ControlToValidate="cCultureId1" display="none" runat="server" /><asp:ImageButton id="cCultureId1Search" CssClass="r-icon" onclick="cCultureId1Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefCompanyId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefCompanyId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefCompanyId1P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDefCompanyId1" CssClass="inp-ddl" DataValueField="DefCompanyId1" DataTextField="DefCompanyId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefProjectId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefProjectId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefProjectId1P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDefProjectId1" CssClass="inp-ddl" DataValueField="DefProjectId1" DataTextField="DefProjectId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cDefSystemId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cDefSystemId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cDefSystemId1P2" class="r-td r-content" runat="server"><asp:DropDownList id="cDefSystemId1" CssClass="inp-ddl" DataValueField="DefSystemId1" DataTextField="DefSystemId1Text" runat="server" /><asp:RequiredFieldValidator id="cRFVDefSystemId1" ControlToValidate="cDefSystemId1" display="none" runat="server" /><asp:ImageButton id="cDefSystemId1Search" CssClass="r-icon" onclick="cDefSystemId1Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrEmail1P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrEmail1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrEmail1P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrEmail1" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrMobile1P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrMobile1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrMobile1P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrMobile1" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUsrGroupLs1P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cUsrGroupLs1Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUsrGroupLs1P2" class="r-td r-content" runat="server"><asp:ListBox SelectionMode="Multiple" id="cUsrGroupLs1" CssClass="inp-pic" DataValueField="UsrGroupLs1" DataTextField="UsrGroupLs1Text" runat="server" /><asp:ImageButton id="cUsrGroupLs1Search" CssClass="r-icon" onclick="cUsrGroupLs1Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUsrImprLink1P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cUsrImprLink1Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUsrImprLink1P2" class="r-td r-content" runat="server"><asp:DataGrid id="cUsrImprLink1" CssClass="GrdTxt" AllowPaging="true" EnableViewState="true" OnPageIndexChanged="cUsrImprLink1_PageIndexChanged" AutoGenerateColumns="false" ShowHeader="false" runat="server">
    		<PagerStyle visible="true" mode="NumericPages" /><Columns><asp:TemplateColumn><ItemTemplate>
    		<asp:HyperLink text='<%# DataBinder.Eval(Container.DataItem,"UsrImprLink1Text").ToString() %>' NavigateUrl="#" onclick='<%# RO.Common3.Utils.AddTilde(GetUrlWithQSHash(DataBinder.Eval(Container.DataItem,"UsrImprLink1").ToString())) %>' CssClass="GrdNwrLn" width="100%" runat="server" />
    		</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cPicMed1P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cPicMed1Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cPicMed1P2" class="r-td r-content" runat="server">
    			<asp:Panel id="cPicMed1Pan" CssClass="DocPanel" Style="display: none;" runat="server">
    			<table width="100%"><tr>
    			<td><asp:FileUpload id="cPicMed1Fi" runat="server" /></td>
    			<td><asp:ImageButton ImageUrl="~/images/Btrash.gif" ID="cPicMed1Del" CssClass="BtnImgDel" style="display:none; margin-left: 6px;" OnClientClick='NoConfirm();' Text="Delete" runat="server" /></td>
    			</tr></table>
    			</asp:Panel>
    			<asp:ImageButton id="cPicMed1" CssClass="DocView" OnClientClick='NoConfirm(); return false;' OnClick="cPicMed1_Click" runat="server" />
    			<asp:ImageButton id="cPicMed1Tgo" OnClientClick='NoConfirm(); switchStatus($(this)); return false;' onclick="cPicMed1Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="false" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cIPAlert1P1" class="r-td r-labelR" runat="server"><asp:Label id="cIPAlert1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cIPAlert1P2" class="r-td r-content" runat="server"><asp:CheckBox id="cIPAlert1" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPwdNoRepeat1P1" class="r-td r-labelR" runat="server"><asp:Label id="cPwdNoRepeat1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPwdNoRepeat1P2" class="r-td r-content" runat="server"><asp:TextBox id="cPwdNoRepeat1" CssClass="inp-num" runat="server" /><asp:RegularExpressionValidator id="cREVPwdNoRepeat1" ControlToValidate="cPwdNoRepeat1" display="none" ValidationExpression="[0-9]*" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPwdDuration1P1" class="r-td r-labelR" runat="server"><asp:Label id="cPwdDuration1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPwdDuration1P2" class="r-td r-content" runat="server"><asp:TextBox id="cPwdDuration1" CssClass="inp-num" runat="server" /><asp:RegularExpressionValidator id="cREVPwdDuration1" ControlToValidate="cPwdDuration1" display="none" ValidationExpression="[0-9]*" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPwdWarn1P1" class="r-td r-labelR" runat="server"><asp:Label id="cPwdWarn1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPwdWarn1P2" class="r-td r-content" runat="server"><asp:TextBox id="cPwdWarn1" CssClass="inp-num" runat="server" /><asp:RegularExpressionValidator id="cREVPwdWarn1" ControlToValidate="cPwdWarn1" display="none" ValidationExpression="[0-9]*" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cActive1P1" class="r-td r-labelR" runat="server"><asp:Label id="cActive1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cActive1P2" class="r-td r-content" runat="server"><asp:CheckBox id="cActive1" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cInternalUsr1P1" class="r-td r-labelR" runat="server"><asp:Label id="cInternalUsr1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cInternalUsr1P2" class="r-td r-content" runat="server"><asp:CheckBox id="cInternalUsr1" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTechnicalUsr1P1" class="r-td r-labelR" runat="server"><asp:Label id="cTechnicalUsr1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTechnicalUsr1P2" class="r-td r-content" runat="server"><asp:CheckBox id="cTechnicalUsr1" CssClass="inp-chk" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cEmailLink1P1" class="r-td r-labelR" runat="server"><asp:Label id="cEmailLink1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cEmailLink1P2" class="r-td r-content" runat="server"><asp:HyperLink id="cEmailLink1" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMobileLink1P1" class="r-td r-labelR" runat="server"><asp:Label id="cMobileLink1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMobileLink1P2" class="r-td r-content" runat="server"><asp:HyperLink id="cMobileLink1" CssClass="inp-txtln" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab2" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel2" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cFailedAttempt1P1" class="r-td r-labelR" runat="server"><asp:Label id="cFailedAttempt1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cFailedAttempt1P2" class="r-td r-content" runat="server"><asp:TextBox id="cFailedAttempt1" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLastSuccessDt1P1" class="r-td r-labelR" runat="server"><asp:Label id="cLastSuccessDt1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLastSuccessDt1P2" class="r-td r-content" runat="server"><asp:TextBox id="cLastSuccessDt1" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLastFailedDt1P1" class="r-td r-labelR" runat="server"><asp:Label id="cLastFailedDt1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLastFailedDt1P2" class="r-td r-content" runat="server"><asp:TextBox id="cLastFailedDt1" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCompanyLs1P1" class="r-td r-labelR" runat="server"><asp:Label id="cCompanyLs1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCompanyLs1P2" class="r-td r-content" runat="server"><asp:ListBox SelectionMode="Multiple" id="cCompanyLs1" CssClass="inp-pic" DataValueField="CompanyLs1" DataTextField="CompanyLs1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cProjectLs1P1" class="r-td r-labelR" runat="server"><asp:Label id="cProjectLs1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cProjectLs1P2" class="r-td r-content" runat="server"><asp:ListBox SelectionMode="Multiple" id="cProjectLs1" CssClass="inp-pic" DataValueField="ProjectLs1" DataTextField="ProjectLs1Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cModifiedOn1P1" class="r-td r-labelR" runat="server"><asp:Label id="cModifiedOn1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModifiedOn1P2" class="r-td r-content" runat="server"><asp:TextBox id="cModifiedOn1" CssClass="inp-txt" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHintQuestionId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cHintQuestionId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHintQuestionId1P2" class="r-td r-content" runat="server"><asp:DropDownList id="cHintQuestionId1" CssClass="inp-ddl" DataValueField="HintQuestionId1" DataTextField="HintQuestionId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cHintAnswer1P1" class="r-td r-labelR" runat="server"><asp:Label id="cHintAnswer1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cHintAnswer1P2" class="r-td r-content" runat="server"><asp:TextBox id="cHintAnswer1" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cInvestorId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cInvestorId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cInvestorId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cInvestorId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbInvestorId1" DataValueField="InvestorId1" DataTextField="InvestorId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCustomerId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cCustomerId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCustomerId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCustomerId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCustomerId1" DataValueField="CustomerId1" DataTextField="CustomerId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVendorId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cVendorId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVendorId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cVendorId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbVendorId1" DataValueField="VendorId1" DataTextField="VendorId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAgentId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cAgentId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAgentId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cAgentId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbAgentId1" DataValueField="AgentId1" DataTextField="AgentId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBrokerId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cBrokerId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBrokerId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cBrokerId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbBrokerId1" DataValueField="BrokerId1" DataTextField="BrokerId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMemberId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cMemberId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMemberId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cMemberId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbMemberId1" DataValueField="MemberId1" DataTextField="MemberId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLenderId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cLenderId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLenderId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cLenderId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbLenderId1" DataValueField="LenderId1" DataTextField="LenderId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorrowerId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorrowerId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorrowerId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cBorrowerId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbBorrowerId1" DataValueField="BorrowerId1" DataTextField="BorrowerId1Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGuarantorId1P1" class="r-td r-labelR" runat="server"><asp:Label id="cGuarantorId1Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGuarantorId1P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGuarantorId1" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGuarantorId1" DataValueField="GuarantorId1" DataTextField="GuarantorId1Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUsageStatP2" class="r-td r-content" runat="server"><asp:Label id="cUsageStat" CssClass="inp-lab" runat="server" /></div>
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
