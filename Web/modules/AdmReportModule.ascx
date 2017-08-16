<%@ Control Language="c#" Inherits="RO.Web.AdmReportModule" CodeFile="AdmReportModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
      Sys.Application.add_load(function () { if (typeof(old_Page_ClientValidate)!='undefined' || typeof(Page_ClientValidate) =='undefined') return; try { old_Page_ClientValidate = Page_ClientValidate;} catch(e){}; Page_ClientValidate = function (g) {if (typeof(old_Page_ClientValidate) == 'function') {$.watermark.hideAll(); Page_BlockSubmit = false && !old_Page_ClientValidate(g);} else return true; if (Page_BlockSubmit) { ValidateThisModule(<%= this.ClientID %>); $('#<%=cValidSummary.ClientID%>:visible').hide();} ; if (Page_BlockSubmit && $('#<%=bPgDirty.ClientID%>').val() == 'Y') {$('#<%=bConfirm.ClientID%>').val('Y');}; return !Page_BlockSubmit;}});
	Sys.Application.add_load(function () { $('input[type=hidden]').each(function (i, e) { try { e.defaultValue = e.value; } catch (er) { } }); WatermarkInput(<%= this.ClientID %>,'<%= PanelUpd.ClientID %>');});
	Sys.Application.add_load(function () { var hlp={}; $('a.GrdHead').each(function(i,e){var ids = ($(this).attr('id')||'').split('_'); hlp[ids[ids.length-1].replace(/hl$/,'l')] = $(this).attr('title');});$('span.GrdTxtLb,a.GrdTxtLn,a.GrdBoxLn').each(function (i, e) {try { var style = ($(this).attr('style')||'').replace(/height/i, 'max-height'); var ids = ($(this).attr('id')||'').split('_'); $(this).attr('title', hlp[ids[ids.length-1]]).attr('style', style); } catch (e) { } }); });
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChkExpNow)
	Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ChkPgDirty)
	Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
	function GridUpload() { document.getElementById('<%=cGridUploadBtn.ClientID %>').click() }
	function GridEdit(cn) { document.getElementById('<%=bConfirm.ClientID %>').value = 'N'; colNam=cn; }
	function GridDelete() { document.getElementById('<%=bPgDirty.ClientID %>').value='Y'; ChkPgDirty(); document.getElementById('<%=bConfirm.ClientID %>').value='N';colNam='DeleteLink';}
	function GridCancel() { Page_Validators=[]; document.getElementById('<%=bConfirm.ClientID %>').value='N';}
	var xPos, yPos;
	function SaveScroll() { xPos = $(document).scrollLeft(); yPos = $(document).scrollTop(); }
	function RestoreScroll() { setTimeout(function() {$(document).scrollLeft(xPos);$(document).scrollTop(yPos);}, 0); }
	Sys.Application.add_load(function () {if(window.matchMedia("screen and (max-width: 1024px)").matches){if (window.matchMedia("screen and (max-width: 767px)").matches){AppendSingleTd();}else{$('.GrdEdtLabelText').hide();if($('.GrdEdtTmp').length > 0){$('.GrdHead .HideObjOnTablet').addClass(' ShowObjHeader');}}}else{ $('.GrdEdtLabelText').hide(); $('.GrdHead .HideObjOnTablet').removeClass(' ShowObjHeader');}});
	$(window).resize(function () { if(window.matchMedia("screen and (max-width: 1024px)").matches){ if (window.matchMedia("screen and (max-width: 767px)").matches){ AppendSingleTd();}else{$('.GrdEdtLabelText').hide();if($('.GrdEdtTmp').length > 0){$('.GrdHead .HideObjOnTablet').addClass(' ShowObjHeader');}AppendOrigTd();}} else {$('.GrdEdtLabelText').hide();$('.GrdHead .HideObjOnTablet').removeClass(' ShowObjHeader');}});
	function NoConfirm() {document.getElementById('<%=bConfirm.ClientID%>').value = 'N';}
	function ChkPgDirty()
	{
		var x = document.getElementById('<%=bPgDirty.ClientID%>');
		var y = document.getElementById('<%=cPgDirty.ClientID%>');
		if (y != null) {if (x != null && x.value == 'Y') {y.style.visibility = '';} else {y.style.visibility = 'hidden';}}
	}
	function initializeRequestHandler(sender, args) {if (!fConfirm2('<%=bPgDirty.ClientID%>','<%=bConfirm.ClientID%>','<%=aNam.ClientID%>','<%=aVal.ClientID%>')) {args.set_cancel(true);} else try {$.watermark.hideAll();} catch (e) { }}
	function beginRequestHandler(sender, e) { e.get_postBackElement().disabled=true; SaveScroll(); ShowProgress(); document.body.style.cursor='wait'; }
	function endRequestHandler() { initPageLoad=true; RestoreScroll(); HideProgress(); document.body.style.cursor='auto'; var v = $('#<%=cValidSummary.ClientID%>:visible'); if (v.length > 0 && typeof(Page_Validators) != 'undefined' && $(Page_Validators).length > 0) {ValidateThisModule(<%= this.ClientID %>);v.hide();}; }
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
<asp:UpdatePanel ID="PanelUpd" runat="server"><Triggers><asp:PostBackTrigger ControlID="cGridUploadBtn" /><asp:PostBackTrigger ControlID="cBrowseButton" /><asp:PostBackTrigger ControlID="cImportButton" /><asp:PostBackTrigger ControlID="cContinueButton" /></Triggers><ContentTemplate>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmReport67List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmReport67List_SelectedIndexChanged" OnTextChanged="cAdmReport67List_TextChanged" OnDDFindClick="cAdmReport67List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="ReportId22" DataTextField="ReportId22Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab28" runat="server"/>
<ul id="tabs">
    <li><a id="cTab28" href="#" class="current" name="Tab28" runat="server"></a></li>
    <li><a id="cTab29" href="#" name="Tab29" runat="server"></a></li>
    <li><a id="cTab30" href="#" name="Tab30" runat="server"></a></li>
    <li><a id="cTab31" href="#" name="Tab31" runat="server"></a></li>
    <li><a id="cTab32" href="#" name="Tab32" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab28" runat="server">
    <asp:UpdatePanel id="UpdPanel28" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cRptTemplate22Upl" /></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReportId22P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportId22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId22P2" class="r-td r-content" runat="server"><asp:TextBox id="cReportId22" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cProgramName22P1" class="r-td r-labelR" runat="server"><asp:Label id="cProgramName22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cProgramName22P2" class="r-td r-content" runat="server"><asp:TextBox id="cProgramName22" CssClass="inp-txt" MaxLength="20" runat="server" /><asp:RequiredFieldValidator id="cRFVProgramName22" ControlToValidate="cProgramName22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReportTypeCd22P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportTypeCd22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportTypeCd22P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReportTypeCd22" CssClass="inp-ddl" DataValueField="ReportTypeCd22" DataTextField="ReportTypeCd22Text" runat="server" /><asp:RequiredFieldValidator id="cRFVReportTypeCd22" ControlToValidate="cReportTypeCd22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cOrientationCd22P1" class="r-td r-labelR" runat="server"><asp:Label id="cOrientationCd22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cOrientationCd22P2" class="r-td r-content" runat="server">    		<div><asp:RadioButtonList id="cOrientationCd22" class="inp-rad" RepeatDirection="Horizontal" DataValueField="OrientationCd22" DataTextField="OrientationCd22Text" runat="server" /><asp:RequiredFieldValidator id="cRFVOrientationCd22" ControlToValidate="cOrientationCd22" display="none" runat="server" /></div></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCopyReportId22P1" class="r-td r-labelR" runat="server"><asp:Label id="cCopyReportId22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCopyReportId22P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCopyReportId22" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCopyReportId22" DataValueField="CopyReportId22" DataTextField="CopyReportId22Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cModifiedBy22P1" class="r-td r-labelR" runat="server"><asp:Label id="cModifiedBy22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModifiedBy22P2" class="r-td r-content" runat="server"><asp:DropDownList id="cModifiedBy22" CssClass="inp-ddl" DataValueField="ModifiedBy22" DataTextField="ModifiedBy22Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cModifiedOn22P1" class="r-td r-labelR" runat="server"><asp:Label id="cModifiedOn22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cModifiedOn22P2" class="r-td r-content" runat="server"><asp:TextBox id="cModifiedOn22" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cTemplateName22P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cTemplateName22Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cTemplateName22P2" class="r-td r-content" runat="server"><asp:TextBox id="cTemplateName22" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptTemplate22P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptTemplate22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptTemplate22P2" class="r-td r-content" runat="server">
    		<asp:Panel id="cRptTemplate22Pan" CssClass="DocPanel" Visible="false" runat="server">
    		<div>
    			<div style="text-align:center; padding-top:24px;"><asp:FileUpload id="cRptTemplate22Fi" runat="server" width="100%"/></div>
    			<div style=" padding:2px 0px 0 0px; text-align:center;">
    			<asp:Button id="cRptTemplate22Upl" CssClass="small blue button DocUpload" style="float:right;" OnClientClick='NoConfirm()' onclick="cRptTemplate22Upl_Click" text="Upload" runat="server" />
    			<asp:CheckBox id="cRptTemplate22Ow" CssClass="inp-chk" style="float:left;" Text="Overwrite" runat="server" />  
    			</div>
    		</div>
    		</asp:Panel>
    		<asp:Panel id="cRptTemplate22Div" CssClass="DocPanel" style="overflow-x:auto; overflow-y:hidden;" runat="server">
    		<asp:GridView id="cRptTemplate22GV" CssClass="GrdTxt" style="white-space:normal" PagerStyle-CssClass="pgr" ShowHeader="false" ShowFooter="false" PagerSettings-Mode="NumericFirstLast" AllowPaging="true" EnableViewState="true" OnPageIndexChanging="cRptTemplate22GV_PageIndexChanged" OnRowCommand="cRptTemplate22GV_RowCommand" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateSelectButton="false" runat="server">
    		<EmptyDataTemplate>No document has been uploaded.</EmptyDataTemplate>
    		<Columns>
    			<asp:TemplateField><ItemTemplate><asp:Hyperlink text='<%# DataBinder.Eval(Container.DataItem,"DocName").ToString() %>' NavigateUrl='<%# RO.Common3.Utils.AddTilde(GetUrlWithQSHash(DataBinder.Eval(Container.DataItem,"DocLink").ToString()+"&inline=Y")) %>' CssClass="GrdNwrLn" Style="cursor:pointer;" target="_blank" runat="server" /></ItemTemplate></asp:TemplateField>
    			<asp:TemplateField><ItemTemplate><asp:Label text='<%# DataBinder.Eval(Container.DataItem,"InputOn").ToString() %>' CssClass="GrdNwrLb" runat="server" /></ItemTemplate></asp:TemplateField>
    			<asp:TemplateField><ItemTemplate><asp:Label text='<%# DataBinder.Eval(Container.DataItem,"DocSize").ToString() %>' CssClass="GrdNwrLb" runat="server" /></ItemTemplate></asp:TemplateField>
    			<asp:TemplateField><ItemTemplate><asp:Label text='<%# DataBinder.Eval(Container.DataItem,"LoginName").ToString() %>' CssClass="GrdNwrLb" runat="server" /></ItemTemplate></asp:TemplateField>
    			<asp:TemplateField><ItemTemplate><asp:Hyperlink NavigateUrl='<%# RO.Common3.Utils.AddTilde(GetUrlWithQSHash(DataBinder.Eval(Container.DataItem,"DocLink").ToString())) %>' CssClass="DocDownload" runat="server" /></ItemTemplate></asp:TemplateField>
    			<asp:TemplateField><ItemTemplate><asp:ImageButton ImageUrl="~/Images/Btrash.gif" CommandName="deleterow" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' OnClientClick="if (ConfirmPrompt(this,'Are you sure?')) NoConfirm(); else return false;" runat="server" /></ItemTemplate></asp:TemplateField>
    		</Columns>
    		</asp:GridView></asp:Panel>
    		<asp:ImageButton id="cRptTemplate22Tgo" CssClass="r-docIcon" onclick="cRptTemplate22Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" ToolTip="Click to upload a document." /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-11"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUnitCd22P1" class="r-td r-labelR" runat="server"><asp:Label id="cUnitCd22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUnitCd22P2" class="r-td r-content" runat="server"><asp:DropDownList id="cUnitCd22" CssClass="inp-ddl" DataValueField="UnitCd22" DataTextField="UnitCd22Text" runat="server" /><asp:RequiredFieldValidator id="cRFVUnitCd22" ControlToValidate="cUnitCd22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTopMargin22P1" class="r-td r-labelR" runat="server"><asp:Label id="cTopMargin22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTopMargin22P2" class="r-td r-content" runat="server"><asp:TextBox id="cTopMargin22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVTopMargin22" ControlToValidate="cTopMargin22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBottomMargin22P1" class="r-td r-labelR" runat="server"><asp:Label id="cBottomMargin22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBottomMargin22P2" class="r-td r-content" runat="server"><asp:TextBox id="cBottomMargin22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVBottomMargin22" ControlToValidate="cBottomMargin22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLeftMargin22P1" class="r-td r-labelR" runat="server"><asp:Label id="cLeftMargin22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLeftMargin22P2" class="r-td r-content" runat="server"><asp:TextBox id="cLeftMargin22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVLeftMargin22" ControlToValidate="cLeftMargin22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRightMargin22P1" class="r-td r-labelR" runat="server"><asp:Label id="cRightMargin22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRightMargin22P2" class="r-td r-content" runat="server"><asp:TextBox id="cRightMargin22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVRightMargin22" ControlToValidate="cRightMargin22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPageWidth22P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageWidth22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageWidth22P2" class="r-td r-content" runat="server"><asp:TextBox id="cPageWidth22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVPageWidth22" ControlToValidate="cPageWidth22" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cPageHeight22P1" class="r-td r-labelR" runat="server"><asp:Label id="cPageHeight22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPageHeight22P2" class="r-td r-content" runat="server"><asp:TextBox id="cPageHeight22" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVPageHeight22" ControlToValidate="cPageHeight22" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-12-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cSyncByDbP1" class="r-td r-labelR" runat="server"><asp:Label id="cSyncByDbLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSyncByDbP2" class="r-td r-content" runat="server"><asp:ImageButton id="cSyncByDb" OnClientClick='NoConfirm()' OnClick="cSyncByDb_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cSyncToDbP1" class="r-td r-labelR" runat="server"><asp:Label id="cSyncToDbLabel" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cSyncToDbP2" class="r-td r-content" runat="server"><asp:ImageButton id="cSyncToDb" OnClientClick='NoConfirm()' OnClick="cSyncToDb_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAllowSelect22P1" class="r-td r-labelR" runat="server"><asp:Label id="cAllowSelect22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAllowSelect22P2" class="r-td r-content" runat="server"><asp:CheckBox id="cAllowSelect22" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cGenerateRp22P1" class="r-td r-labelR" runat="server"><asp:Label id="cGenerateRp22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGenerateRp22P2" class="r-td r-content" runat="server"><asp:CheckBox id="cGenerateRp22" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cLastGenDt22P1" class="r-td r-labelR" runat="server"><asp:Label id="cLastGenDt22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLastGenDt22P2" class="r-td r-content" runat="server"><asp:TextBox id="cLastGenDt22" CssClass="inp-txt" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-10-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAuthRequired22P1" class="r-td r-labelR" runat="server"><asp:Label id="cAuthRequired22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAuthRequired22P2" class="r-td r-content" runat="server"><asp:CheckBox id="cAuthRequired22" CssClass="inp-chk" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cWhereClause22P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cWhereClause22Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cWhereClause22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cWhereClause22" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cWhereClause22" display="none" ErrorMessage="WhereClause <= 1000 characters please." ValidationExpression="^[\s\S]{0,1000}$" runat="server" /><asp:Image id="cWhereClause22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
<div id="GridDiv" class="grid-container rg-1-12">
<asp:TextBox id="isMobile" CssClass="chkMobile" Text="notMobile" style="display:none;" runat="server" />
<div class="viewport rc-1-12">
<div class="button-container">
 <asp:Panel id="cNaviPanel" runat="server" visible="true">
		<div class="button-grp"><asp:Button id="cGridUploadBtn" Width="0px" style="display:none;" runat="server" /></div>
		<div class="button-grp"><asp:Button id="cInsRowButton" onclick="cInsRowButton_Click" runat="server" /></div>
		<div class="button-grp">
		    <div><asp:label ID="cGridFtrLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div class="grdFindFilter"><asp:DropDownList id="cFindFilter" CssClass="inp-ddl" runat="server" /></div>
		</div>
		<div class="button-grp">
		    <div><asp:label ID="cGridFndLabel" CssClass="inp-lbl grdFindLb" runat="server" /></div>
		    <div class="grdFind"><asp:TextBox id="cFind" CssClass="inp-txt" runat="server" /></div>
		    <div><asp:Button id="cFindButton" onclick="cFindButton_Click" runat="server" /></div>
		</div>
		<div class="button-grp">
		    <div class="btnPgGrp">
		        <div><asp:TextBox id="cPgSize" runat="server" CssClass="inp-ctr" width="25px" /></div>
		        <div><asp:Button id="cPgSizeButton" onclick="cPgSizeButton_Click" runat="server" /></div>
		        <div class="grdPaging">
		            <div><asp:Button id="cFirstButton" onclick="cFirstButton_Click" runat="server" /></div>
		            <div><asp:Button id="cPrevButton" onclick="cPrevButton_Click" runat="server" /></div>
		            <div><asp:TextBox id="cGoto" OnTextChanged="cGoto_TextChanged" AutoPostBack="true" CssClass="inp-ctr" width="25px" runat="server" /></div>
		            <div><asp:label id="cPageNoLabel" CssClass="inp-lbl" runat="server" /></div>
		            <div><asp:Button id="cNextButton" onclick="cNextButton_Click" runat="server" /></div>
		            <div><asp:Button id="cLastButton" CssClass="small blue button" onclick="cLastButton_Click" runat="server" /></div>
		        </div>
		        <div><asp:Button id="cShowImpButton" onclick="cShowImpButton_Click" runat="server" /></div>
		        <span id="grdCount" class="resultTotal" runat="server"></span>
		    </div>
		</div>
	</asp:Panel>
	<asp:Panel id="cImportPwdPanel" runat="server" visible="false">
		<div class="button-grp">
	        <div><asp:label ID="cImpPwdLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div><asp:TextBox TextMode="Password" id="cImportPwd" CssClass="PwdBox" width="250px" MaxLength="32" runat="server" /></div>
		    <div><asp:Button id="cContinueButton" onclick="cContinueButton_Click" runat="server" /></div>
		</div>
	</asp:Panel>
	<asp:Panel id="cImport" runat="server" visible="false">
		<div class="button-grp"><asp:FileUpload id="cBrowse" CssClass="inp-txt" runat="server" width="250px" /></div>
		<div class="button-grp"><asp:Button id="cBrowseButton" OnClick="cBrowseButton_Click" Width="0px" style="display:none;" runat="server" /><asp:TextBox id="cFNameO" text="" Width="0px" style="display:none;" runat="server" /><asp:TextBox id="cFName" text="" Width="0px" style="display:none;" runat="server" /></div>
		<div class="button-grp">
		    <div><asp:label ID="cGridWksLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div><asp:DropDownList id="cWorkSheet" CssClass="inp-ddl" runat="server" style="min-width:100px;" /></div>
		</div>
		<div class="button-grp">
		    <div><asp:label ID="cGridStrLabel" CssClass="inp-lbl" runat="server" /></div>
		    <div>
		        <asp:TextBox id="cStartRow" CssClass="inp-ctr" text="2" runat="server" width="30px" />
		        <asp:ImageButton id="cSchemaImage" ImageUrl="~/images/Schema.gif" runat="server" />
		        <asp:Button id="cImportButton" onclick="cImportButton_Click" runat="server" />
		    </div>
		    <div><asp:Button id="cHideImpButton" onclick="cHideImpButton_Click" runat="server" CausesValidation="true" /></div>
		</div>
	</asp:Panel>
</div>
<div class="ListView">
	<asp:ListView id="cAdmReportGrid" DataKeyNames="ReportHlpId96" OnItemCommand="cAdmReportGrid_OnItemCommand" OnSorting="cAdmReportGrid_OnSorting" OnPreRender="cAdmReportGrid_OnPreRender" OnPagePropertiesChanging="cAdmReportGrid_OnPagePropertiesChanging" OnItemEditing="cAdmReportGrid_OnItemEditing" OnItemCanceling="cAdmReportGrid_OnItemCanceling" OnItemDeleting="cAdmReportGrid_OnItemDeleting" OnItemDataBound="cAdmReportGrid_OnItemDataBound" OnLayoutCreated="cAdmReportGrid_OnLayoutCreated" OnItemUpdating="cAdmReportGrid_OnItemUpdating" runat="server">
	<LayoutTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr id="cAdmReportGridHeader" class="GrdHead" visible="true" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' runat="server">
			<asp:LinkButton id="cReportHlpId96hl" CssClass="GrdHead" onClick="cReportHlpId96hl_Click" runat="server" /><asp:Image id="cReportHlpId96hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
			<asp:LinkButton id="cCultureId96hl" CssClass="GrdHead" onClick="cCultureId96hl_Click" runat="server" /><asp:Image id="cCultureId96hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:800px;text-align:left;' runat="server">
			<asp:LinkButton id="cDefaultHlpMsg96hl" CssClass="GrdHead" onClick="cDefaultHlpMsg96hl_Click" runat="server" /><asp:Image id="cDefaultHlpMsg96hi" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:150px;text-align:left;' runat="server">
			<asp:LinkButton id="cReportTitle96hl" CssClass="GrdHead" onClick="cReportTitle96hl_Click" runat="server" /><asp:Image id="cReportTitle96hi" runat="server" />
		</div></div>
    </td>
    <td><asp:linkbutton id="cDeleteAllButton" CssClass="GrdDelAll" tooltip="DELETE ALL" onclick="cDeleteAllButton_Click" runat="server" onclientclick='GridDelete()' /></td>
	</tr>
	<tr id="itemPlaceholder" runat="server"></tr>
	<tr id="cAdmReportGridFooter" class="GrdFoot" visible="false" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' runat="server">
		    <asp:Label id="cReportHlpId96fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' runat="server">
		    <asp:Label id="cCultureId96fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:800px;text-align:left;' runat="server">
		    <asp:Label id="cDefaultHlpMsg96fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:150px;text-align:left;' runat="server">
		    <asp:Label id="cReportTitle96fl" class='GrdFoot' runat="server" />
		</div></div>
    </td>
    <td>&nbsp;</td>
	</tr></table></LayoutTemplate>
	<ItemTemplate>
	<tr id="cAdmReportGridRow" class='<%# Container.DisplayIndex % 2 == 0 ? "GrdItm" : "GrdAlt" %>' runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(31) %>" onclick='GridEdit("ReportHlpId96")' runat="server">
			<asp:Label id="cReportHlpId96l" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReportHlpId96").ToString(),base.LUser.Culture) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(32) %>" onclick='GridEdit("CultureId96")' runat="server">
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"CultureId96").ToString() %>' Visible="false" runat="server" />
			<asp:Label id="cCultureId96l" text='<%# DataBinder.Eval(Container.DataItem,"CultureId96Text") %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:800px;text-align:left;' visible="<%# GridColumnVisible(33) %>" onclick='GridEdit("DefaultHlpMsg96")' runat="server">
			<asp:Label id="cDefaultHlpMsg96l" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultHlpMsg96").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:150px;text-align:left;' visible="<%# GridColumnVisible(34) %>" onclick='GridEdit("ReportTitle96")' runat="server">
			<asp:Label id="cReportTitle96l" Text='<%# DataBinder.Eval(Container.DataItem,"ReportTitle96").ToString().Replace("\r\n","<br />").Replace("\r","<br />").Replace("\n","<br />").Replace("  ",HtmlSpace()) %>' CssClass="GrdTxtLb" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReportGridEdit" style="display:none;" CausesValidation="true" CommandName="Edit" runat="server" />
       <asp:LinkButton ID="cAdmReportGridDelete" CssClass="GrdDel" tooltip="DELETE" CommandName="Delete" onclientclick='GridDelete()' runat="server" />
	</td>
	</tr>
	</ItemTemplate>
	<EditItemTemplate>
	<tr class="GrdEdtTmp" runat="server">
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:40px;text-align:right;' visible="<%# GridColumnVisible(31) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cReportHlpId96ml" runat="server" /></div>
		    <asp:TextBox id="cReportHlpId96" CssClass="GrdNum" Text='<%# RO.Common3.Utils.fmNumeric("0",DataBinder.Eval(Container.DataItem,"ReportHlpId96").ToString(),base.LUser.Culture) %>' runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:200px;text-align:left;' visible="<%# GridColumnVisible(32) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cCultureId96ml" runat="server" /></div>
		    <rcasp:ComboBox autocomplete="off" id="cCultureId96" CssClass="GrdDdl" DataValueField="CultureId96" DataTextField="CultureId96Text" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureId96" runat="server" /><asp:RequiredFieldValidator id="cRFVCultureId96" ControlToValidate="cCultureId96" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:800px;text-align:left;' visible="<%# GridColumnVisible(33) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cDefaultHlpMsg96ml" runat="server" /></div>
		    <asp:TextBox id="cDefaultHlpMsg96" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"DefaultHlpMsg96").ToString() %>' runat="server" /><asp:RequiredFieldValidator id="cRFVDefaultHlpMsg96" ControlToValidate="cDefaultHlpMsg96" display="none" runat="server" />
		</div></div>
    </td>
    <td>
		<div class='GrdOuter' runat="server"><div class='GrdInner' style='max-width:150px;text-align:left;' visible="<%# GridColumnVisible(34) %>" runat="server"><div class="GrdEdtLabelText"><asp:label id="cReportTitle96ml" runat="server" /></div>
		    <asp:TextBox id="cReportTitle96" CssClass="GrdTxt" Text='<%# DataBinder.Eval(Container.DataItem,"ReportTitle96").ToString() %>' MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVReportTitle96" ControlToValidate="cReportTitle96" display="none" runat="server" />
		</div></div>
    </td>
	<td>
       <asp:LinkButton ID="cAdmReportGridCancel" CssClass="GrdCan" tooltip="CANCEL" OnClientClick="GridCancel();" CausesValidation="true" CommandName="Cancel" runat="server" />
       <asp:LinkButton ID="cAdmReportGridUpdate" style="display:none;" CommandName="Update" runat="server" />
	</td>
	</tr>
	</EditItemTemplate>
	<EmptyDataTemplate><div class="GrdHead" style="text-align:center;padding:3px 0;"><span>No data currently available.</span></div></EmptyDataTemplate>
	</asp:ListView>
	<asp:DataPager ID="cAdmReportGridDataPager" runat="server" Visible="false" PagedControlID="cAdmReportGrid"></asp:DataPager>
</div></div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab29" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel29" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRegClause22P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cRegClause22Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRegClause22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRegClause22" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cRegClause22" display="none" ErrorMessage="RegClause <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:Image id="cRegClause22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRegCode22P1" class="r-td r-labelL r-labelT" runat="server"><asp:Label id="cRegCode22Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRegCode22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRegCode22" CssClass="inp-txt" runat="server" /><asp:Image id="cRegCode22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab30" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel30" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cValClause22P1" class="r-td r-labelR" runat="server"><asp:Label id="cValClause22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cValClause22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cValClause22" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cValClause22" display="none" ErrorMessage="ValClause <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:Image id="cValClause22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cValCode22P1" class="r-td r-labelR" runat="server"><asp:Label id="cValCode22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cValCode22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cValCode22" CssClass="inp-txt" runat="server" /><asp:Image id="cValCode22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab31" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel31" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cUpdClause22P1" class="r-td r-labelR" runat="server"><asp:Label id="cUpdClause22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUpdClause22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cUpdClause22" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cUpdClause22" display="none" ErrorMessage="UpdClause <= 200 characters please." ValidationExpression="^[\s\S]{0,200}$" runat="server" /><asp:Image id="cUpdClause22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUpdCode22P1" class="r-td r-labelR" runat="server"><asp:Label id="cUpdCode22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUpdCode22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cUpdCode22" CssClass="inp-txt" runat="server" /><asp:Image id="cUpdCode22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab32" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel32" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cXlsClause22P1" class="r-td r-labelR" runat="server"><asp:Label id="cXlsClause22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cXlsClause22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cXlsClause22" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ControlToValidate="cXlsClause22" display="none" ErrorMessage="XlsClause <= 200 characters please." ValidationExpression="^[\s\S]{0,200}$" runat="server" /><asp:Image id="cXlsClause22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cXlsCode22P1" class="r-td r-labelR" runat="server"><asp:Label id="cXlsCode22Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cXlsCode22P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cXlsCode22" CssClass="inp-txt" runat="server" /><asp:Image id="cXlsCode22E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
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
