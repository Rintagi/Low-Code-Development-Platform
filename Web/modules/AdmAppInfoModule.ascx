<%@ Control Language="c#" Inherits="RO.Web.AdmAppInfoModule" CodeFile="AdmAppInfoModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-content"><rcasp:ComboBox autocomplete="off" id="cAdmAppInfo82List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmAppInfo82List_SelectedIndexChanged" OnTextChanged="cAdmAppInfo82List_TextChanged" OnDDFindClick="cAdmAppInfo82List_DDFindClick" Mode="A" OnPostBack="cbPostBack" DataValueField="AppInfoId135" DataTextField="AppInfoId135Text" /></div>
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
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab40" runat="server"/>
<ul id="tabs">
    <li><a id="cTab40" href="#" class="current" name="Tab40" runat="server"></a></li>
    <li><a id="cTab41" href="#" name="Tab41" runat="server"></a></li>
</ul>
<div id="content">
<div id="Tab40" runat="server">
    <asp:UpdatePanel id="UpdPanel40" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cAppZipId135Upl" /></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAppInfoId135P1" class="r-td r-labelR" runat="server"><asp:Label id="cAppInfoId135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAppInfoId135P2" class="r-td r-content" runat="server"><asp:TextBox id="cAppInfoId135" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVersionMa135P1" class="r-td r-labelR" runat="server"><asp:Label id="cVersionMa135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVersionMa135P2" class="r-td r-content" runat="server"><asp:TextBox id="cVersionMa135" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVVersionMa135" ControlToValidate="cVersionMa135" display="none" runat="server" /><asp:RangeValidator id="cRVVersionMa135" ControlToValidate="cVersionMa135" display="none" MaximumValue="9999" MinimumValue="1" Type="Integer" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVersionMi135P1" class="r-td r-labelR" runat="server"><asp:Label id="cVersionMi135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVersionMi135P2" class="r-td r-content" runat="server"><asp:TextBox id="cVersionMi135" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVVersionMi135" ControlToValidate="cVersionMi135" display="none" runat="server" /><asp:RangeValidator id="cRVVersionMi135" ControlToValidate="cVersionMi135" display="none" MaximumValue="9999" MinimumValue="0" Type="Integer" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVersionDt135P1" class="r-td r-labelR" runat="server"><asp:Label id="cVersionDt135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVersionDt135P2" class="r-td r-content" runat="server"><asp:TextBox id="cVersionDt135" CssClass="inp-txt" AutoPostBack="true" OnTextChanged="cVersionDt135_TextChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cLunarDtP2" class="r-td r-content" runat="server"><asp:Label id="cLunarDt" CssClass="inp-lab" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCultureTypeName135P1" class="r-td r-labelR" runat="server"><asp:Label id="cCultureTypeName135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCultureTypeName135P2" class="r-td r-content" runat="server"><rcasp:ComboBox autocomplete="off" id="cCultureTypeName135" CssClass="inp-ddl" Mode="A" OnPostBack="cbPostBack" OnSearch="cbCultureTypeName135" DataValueField="CultureTypeName135" DataTextField="CultureTypeName135Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cVersionValue135P1" class="r-td r-labelR" runat="server"><asp:Label id="cVersionValue135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cVersionValue135P2" class="r-td r-content" runat="server"><asp:TextBox id="cVersionValue135" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAppZipId135P1" class="r-td r-labelR" runat="server"><asp:Label id="cAppZipId135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAppZipId135P2" class="r-td r-content" runat="server">
    		<asp:Panel id="cAppZipId135Pan" CssClass="DocPanel" Visible="false" runat="server">
    		<div>
    			<div style="text-align:center; padding-top:24px;"><asp:FileUpload id="cAppZipId135Fi" runat="server" width="100%"/></div>
    			<div style=" padding:2px 0px 0 0px; text-align:center;">
    			<asp:Button id="cAppZipId135Upl" CssClass="small blue button DocUpload" style="float:right;" OnClientClick='NoConfirm()' onclick="cAppZipId135Upl_Click" text="Upload" runat="server" />
    			<asp:CheckBox id="cAppZipId135Ow" CssClass="inp-chk" style="float:left;" Text="Overwrite" runat="server" />  
    			</div>
    		</div>
    		</asp:Panel>
    		<asp:Panel id="cAppZipId135Div" CssClass="DocPanel" style="overflow-x:auto; overflow-y:hidden;" runat="server">
    		<asp:GridView id="cAppZipId135GV" CssClass="GrdTxt" style="white-space:normal" PagerStyle-CssClass="pgr" ShowHeader="false" ShowFooter="false" PagerSettings-Mode="NumericFirstLast" AllowPaging="true" EnableViewState="true" OnPageIndexChanging="cAppZipId135GV_PageIndexChanged" OnRowCommand="cAppZipId135GV_RowCommand" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateSelectButton="false" runat="server">
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
    		<asp:ImageButton id="cAppZipId135Tgo" CssClass="r-docIcon" onclick="cAppZipId135Tgo_Click" runat="server" ImageUrl="~/Images/UpLoad.png" CausesValidation="true" ToolTip="Click to upload a document." /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-12"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cAppItemLink135P1" class="r-td r-labelR" runat="server"><asp:Label id="cAppItemLink135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAppItemLink135P2" class="r-td r-content" runat="server"><asp:DataGrid id="cAppItemLink135" CssClass="GrdTxt" AllowPaging="true" EnableViewState="true" OnPageIndexChanged="cAppItemLink135_PageIndexChanged" AutoGenerateColumns="false" ShowHeader="false" runat="server">
    		<PagerStyle visible="true" mode="NumericPages" /><Columns><asp:TemplateColumn><ItemTemplate>
    		<asp:HyperLink text='<%# DataBinder.Eval(Container.DataItem,"AppItemLink135Text").ToString() %>' NavigateUrl="#" onclick='<%# RO.Common3.Utils.AddTilde(GetUrlWithQSHash(DataBinder.Eval(Container.DataItem,"AppItemLink135").ToString())) %>' CssClass="GrdNwrLn" width="100%" runat="server" />
    		</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid><asp:ImageButton id="cAppItemLink135Search" CssClass="r-icon" onclick="cAppItemLink135Search_Click" runat="server" ImageUrl="~/Images/Link.gif" CausesValidation="true" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<div id="Tab41" style="display:none;" runat="server">
    <asp:UpdatePanel id="UpdPanel41" UpdateMode="Conditional" runat="server"><Triggers></Triggers><ContentTemplate>
    <div class="r-table rg-1-12"><div class="r-tr">
    <div class="r-td rc-1-9"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cPrerequisite135P1" class="r-td r-labelR" runat="server"><asp:Label id="cPrerequisite135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cPrerequisite135P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cPrerequisite135" CssClass="inp-txt" runat="server" /><asp:Image id="cPrerequisite135E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cReadme135P1" class="r-td r-labelR" runat="server"><asp:Label id="cReadme135Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReadme135P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" autocomplete="new-password" id="cReadme135" CssClass="inp-txt" runat="server" /><asp:Image id="cReadme135E" ImageUrl="~/images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
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
