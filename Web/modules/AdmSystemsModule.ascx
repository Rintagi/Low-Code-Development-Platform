<%@ Control Language="c#" Inherits="RO.Web.AdmSystemsModule" CodeFile="AdmSystemsModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmSystems87List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmSystems87List_SelectedIndexChanged" OnTextChanged="cAdmSystems87List_TextChanged" OnDDFindClick="cAdmSystems87List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="SystemId45" DataTextField="SystemId45Text" /></div>
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
    <div class="r-td rc-1-1"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemId45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cSystemId45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemId45P2" class="r-td r-content" runat="server"><asp:TextBox id="cSystemId45" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVSystemId45" ControlToValidate="cSystemId45" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-2-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServerName45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cServerName45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cServerName45P2" class="r-td r-content" runat="server"><asp:TextBox id="cServerName45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVServerName45" ControlToValidate="cServerName45" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-7"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemName45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cSystemName45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemName45P2" class="r-td r-content" runat="server"><asp:TextBox id="cSystemName45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVSystemName45" ControlToValidate="cSystemName45" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemAbbr45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cSystemAbbr45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSystemAbbr45P2" class="r-td r-content" runat="server"><asp:TextBox id="cSystemAbbr45" CssClass="inp-txt" MaxLength="4" runat="server" /><asp:RequiredFieldValidator id="cRFVSystemAbbr45" ControlToValidate="cSystemAbbr45" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-10"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSysProgram45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cSysProgram45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSysProgram45P2" class="r-td r-content" runat="server"><asp:CheckBox id="cSysProgram45" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-11-11"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cActive45P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cActive45Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cActive45P2" class="r-td r-content" runat="server"><asp:CheckBox id="cActive45" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-12-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAddDbsP1" class="r-td r-labelR" runat="server"><asp:Label id="cAddDbsLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAddDbsP2" class="r-td r-content" runat="server"><asp:ImageButton id="cAddDbs" OnClientClick='NoConfirm()' OnClick="cAddDbs_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cdbAppProvider45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbAppProvider45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbAppProvider45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbAppProvider45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbAppProvider45" ControlToValidate="cdbAppProvider45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbAppServer45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbAppServer45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbAppServer45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbAppServer45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbAppServer45" ControlToValidate="cdbAppServer45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbAppDatabase45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbAppDatabase45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbAppDatabase45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbAppDatabase45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbAppDatabase45" ControlToValidate="cdbAppDatabase45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbDesDatabase45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbDesDatabase45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbDesDatabase45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbDesDatabase45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbDesDatabase45" ControlToValidate="cdbDesDatabase45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbAppUserId45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbAppUserId45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbAppUserId45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbAppUserId45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbAppUserId45" ControlToValidate="cdbAppUserId45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbAppPassword45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbAppPassword45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbAppPassword45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbAppPassword45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVdbAppPassword45" ControlToValidate="cdbAppPassword45" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cdbX01Provider45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01Provider45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01Provider45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01Provider45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbX01Server45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01Server45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01Server45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01Server45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbX01Database45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01Database45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01Database45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01Database45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbX01UserId45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01UserId45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01UserId45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01UserId45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbX01Password45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01Password45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01Password45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01Password45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cdbX01Extra45P1" class="r-td r-labelR" runat="server"><asp:Label id="cdbX01Extra45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cdbX01Extra45P2" class="r-td r-content" runat="server"><asp:TextBox id="cdbX01Extra45" CssClass="inp-txt" MaxLength="200" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAdminEmail45P1" class="r-td r-labelR" runat="server"><asp:Label id="cAdminEmail45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAdminEmail45P2" class="r-td r-content" runat="server"><asp:TextBox id="cAdminEmail45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVAdminEmail45" ControlToValidate="cAdminEmail45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAdminPhone45P1" class="r-td r-labelR" runat="server"><asp:Label id="cAdminPhone45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAdminPhone45P2" class="r-td r-content" runat="server"><asp:TextBox id="cAdminPhone45" CssClass="inp-txt" MaxLength="20" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCustServEmail45P1" class="r-td r-labelR" runat="server"><asp:Label id="cCustServEmail45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCustServEmail45P2" class="r-td r-content" runat="server"><asp:TextBox id="cCustServEmail45" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVCustServEmail45" ControlToValidate="cCustServEmail45" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCustServPhone45P1" class="r-td r-labelR" runat="server"><asp:Label id="cCustServPhone45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCustServPhone45P2" class="r-td r-content" runat="server"><asp:TextBox id="cCustServPhone45" CssClass="inp-txt" MaxLength="20" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCustServFax45P1" class="r-td r-labelR" runat="server"><asp:Label id="cCustServFax45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCustServFax45P2" class="r-td r-content" runat="server"><asp:TextBox id="cCustServFax45" CssClass="inp-txt" MaxLength="20" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cWebAddress45P1" class="r-td r-labelR" runat="server"><asp:Label id="cWebAddress45Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cWebAddress45P2" class="r-td r-content" runat="server"><asp:TextBox id="cWebAddress45" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cResetFromGitRepoP1" class="r-td r-labelR" runat="server"><asp:Label id="cResetFromGitRepoLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cResetFromGitRepoP2" class="r-td r-content" runat="server"><asp:Button id="cResetFromGitRepo" CssClass="small blue button" OnClick="cResetFromGitRepo_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cCreateReactBaseP1" class="r-td r-labelR" runat="server"><asp:Label id="cCreateReactBaseLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCreateReactBaseP2" class="r-td r-content" runat="server"><asp:Button id="cCreateReactBase" CssClass="small blue button" OnClick="cCreateReactBase_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRemoveReactBaseP1" class="r-td r-labelR" runat="server"><asp:Label id="cRemoveReactBaseLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRemoveReactBaseP2" class="r-td r-content" runat="server"><asp:Button id="cRemoveReactBase" CssClass="small blue button" OnClick="cRemoveReactBase_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cPublishReactToSiteP1" class="r-td r-labelR" runat="server"><asp:Label id="cPublishReactToSiteLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPublishReactToSiteP2" class="r-td r-content" runat="server"><asp:Button id="cPublishReactToSite" CssClass="small blue button" OnClick="cPublishReactToSite_Click" runat="server" /></div>
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
