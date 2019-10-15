<%@ Control Language="c#" Inherits="RO.Web.AdmUsrPrefModule" CodeFile="AdmUsrPrefModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmUsrPref64List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmUsrPref64List_SelectedIndexChanged" OnTextChanged="cAdmUsrPref64List_TextChanged" OnDDFindClick="cAdmUsrPref64List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="UsrPrefId93" DataTextField="UsrPrefId93Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab24" runat="server"/>
<ul id="tabs">
    <li><a id="cTab24" href="#" class="current" name="Tab24" runat="server"></a></li>
    <li><a id="cTab25" href="#" name="Tab25" runat="server"></a></li>
    <li><a id="cTab26" href="#" name="Tab26" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab24" runat="server">
    <asp:UpdatePanel id="UpdPanel24" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cLoginImage93Upl" /><asp:PostBackTrigger ControlID="cMobileImage93Upl" /></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-7"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUsrPrefId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrPrefId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrPrefId93P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrPrefId93" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrPrefDesc93P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrPrefDesc93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrPrefDesc93P2" class="r-td r-content" runat="server"><asp:TextBox id="cUsrPrefDesc93" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVUsrPrefDesc93" ControlToValidate="cUsrPrefDesc93" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMenuOptId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cMenuOptId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMenuOptId93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cMenuOptId93" CssClass="inp-ddl" DataValueField="MenuOptId93" DataTextField="MenuOptId93Text" runat="server" /><asp:RequiredFieldValidator id="cRFVMenuOptId93" ControlToValidate="cMenuOptId93" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMasterPgFile93P1" class="r-td r-labelR" runat="server"><asp:Label id="cMasterPgFile93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMasterPgFile93P2" class="r-td r-content" runat="server"><asp:TextBox id="cMasterPgFile93" CssClass="inp-txt" MaxLength="100" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLoginImage93P1" class="r-td r-labelR" runat="server"><asp:Label id="cLoginImage93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLoginImage93P2" class="r-td r-content" runat="server">
    			<asp:Panel id="cLoginImage93Pan" CssClass="DocPanel" Visible="false" runat="server">
    			<table width="100%"><tr>
    			<td style="display:none;"><asp:Button id="cLoginImage93Upl" CssClass="small blue button" OnClientClick='NoConfirm()' onclick="cLoginImage93Upl_Click" text="Upload" runat="server" /></td>
    			<td><asp:FileUpload id="cLoginImage93Fi" runat="server" onchange="AutoUpload(this,event);" /></td>
    			</tr></table>
    			</asp:Panel>
    			<asp:TextBox id="cLoginImage93" CssClass="inp-txt" MaxLength="100" AutoPostBack="true" runat="server" />
    			<asp:ImageButton id="cLoginImage93Tgo" CssClass="r-icon" OnClientClick='NoConfirm()' onclick="cLoginImage93Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cMobileImage93P1" class="r-td r-labelR" runat="server"><asp:Label id="cMobileImage93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMobileImage93P2" class="r-td r-content" runat="server">
    			<asp:Panel id="cMobileImage93Pan" CssClass="DocPanel" Visible="false" runat="server">
    			<table width="100%"><tr>
    			<td style="display:none;"><asp:Button id="cMobileImage93Upl" CssClass="small blue button" OnClientClick='NoConfirm()' onclick="cMobileImage93Upl_Click" text="Upload" runat="server" /></td>
    			<td><asp:FileUpload id="cMobileImage93Fi" runat="server" onchange="AutoUpload(this,event);" /></td>
    			</tr></table>
    			</asp:Panel>
    			<asp:TextBox id="cMobileImage93" CssClass="inp-txt" MaxLength="100" AutoPostBack="true" runat="server" />
    			<asp:ImageButton id="cMobileImage93Tgo" CssClass="r-icon" OnClientClick='NoConfirm()' onclick="cMobileImage93Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cComListVisible93P1" class="r-td r-labelR" runat="server"><asp:Label id="cComListVisible93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cComListVisible93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cComListVisible93" CssClass="inp-ddl" DataValueField="ComListVisible93" DataTextField="ComListVisible93Text" runat="server" /><asp:RequiredFieldValidator id="cRFVComListVisible93" ControlToValidate="cComListVisible93" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPrjListVisible93P1" class="r-td r-labelR" runat="server"><asp:Label id="cPrjListVisible93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPrjListVisible93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cPrjListVisible93" CssClass="inp-ddl" DataValueField="PrjListVisible93" DataTextField="PrjListVisible93Text" runat="server" /><asp:RequiredFieldValidator id="cRFVPrjListVisible93" ControlToValidate="cPrjListVisible93" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSysListVisible93P1" class="r-td r-labelR" runat="server"><asp:Label id="cSysListVisible93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSysListVisible93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cSysListVisible93" CssClass="inp-ddl" DataValueField="SysListVisible93" DataTextField="SysListVisible93Text" runat="server" /><asp:RequiredFieldValidator id="cRFVSysListVisible93" ControlToValidate="cSysListVisible93" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPrefDefault93P1" class="r-td r-labelR" runat="server"><asp:Label id="cPrefDefault93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPrefDefault93P2" class="r-td r-content" runat="server"><asp:CheckBox id="cPrefDefault93" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSampleImage93P1" class="r-td r-labelR" runat="server"><asp:Label id="cSampleImage93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSampleImage93P2" class="r-td r-content" runat="server">
    			<asp:Panel id="cSampleImage93Pan" CssClass="DocPanel" Style="display: none;" runat="server">
    			<table width="100%"><tr>
    			<td><asp:FileUpload id="cSampleImage93Fi" runat="server" /></td>
    			<td><asp:ImageButton ImageUrl="~/images/Btrash.gif" ID="cSampleImage93Del" CssClass="BtnImgDel" style="display:none; margin-left: 6px;" OnClientClick='NoConfirm();' Text="Delete" runat="server" /></td>
    			</tr></table>
    			</asp:Panel>
    			<asp:ImageButton id="cSampleImage93" CssClass="DocView" OnClientClick='NoConfirm(); return false;' OnClick="cSampleImage93_Click" runat="server" />
    			<asp:ImageButton id="cSampleImage93Tgo" OnClientClick='NoConfirm(); switchStatus($(this)); return false;' onclick="cSampleImage93Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="false" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab25" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel25" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUsrStyleSheet93P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrStyleSheet93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrStyleSheet93P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cUsrStyleSheet93" CssClass="inp-txt" runat="server" /><asp:Image id="cUsrStyleSheet93E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab26" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel26" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUsrId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cUsrId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbUsrId93" DataValueField="UsrId93" DataTextField="UsrId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrGroupId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrGroupId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrGroupId93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cUsrGroupId93" CssClass="inp-ddl" DataValueField="UsrGroupId93" DataTextField="UsrGroupId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCompanyId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cCompanyId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCompanyId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCompanyId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCompanyId93" DataValueField="CompanyId93" DataTextField="CompanyId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cProjectId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cProjectId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cProjectId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cProjectId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbProjectId93" DataValueField="ProjectId93" DataTextField="ProjectId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSystemId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cSystemId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSystemId93P2" class="r-td r-content" runat="server"><asp:DropDownList id="cSystemId93" CssClass="inp-ddl" DataValueField="SystemId93" DataTextField="SystemId93Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cMemberId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cMemberId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cMemberId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cMemberId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbMemberId93" DataValueField="MemberId93" DataTextField="MemberId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAgentId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cAgentId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAgentId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cAgentId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbAgentId93" DataValueField="AgentId93" DataTextField="AgentId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBrokerId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cBrokerId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBrokerId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cBrokerId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbBrokerId93" DataValueField="BrokerId93" DataTextField="BrokerId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCustomerId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cCustomerId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCustomerId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCustomerId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCustomerId93" DataValueField="CustomerId93" DataTextField="CustomerId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cInvestorId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cInvestorId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cInvestorId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cInvestorId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbInvestorId93" DataValueField="InvestorId93" DataTextField="InvestorId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVendorId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cVendorId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVendorId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cVendorId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbVendorId93" DataValueField="VendorId93" DataTextField="VendorId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLenderId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cLenderId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLenderId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cLenderId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbLenderId93" DataValueField="LenderId93" DataTextField="LenderId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBorrowerId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cBorrowerId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBorrowerId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cBorrowerId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbBorrowerId93" DataValueField="BorrowerId93" DataTextField="BorrowerId93Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGuarantorId93P1" class="r-td r-labelR" runat="server"><asp:Label id="cGuarantorId93Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGuarantorId93P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cGuarantorId93" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbGuarantorId93" DataValueField="GuarantorId93" DataTextField="GuarantorId93Text" runat="server" /></div>
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
