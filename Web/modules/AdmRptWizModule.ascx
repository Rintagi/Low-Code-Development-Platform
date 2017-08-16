<%@ Control Language="c#" Inherits="RO.Web.AdmRptWizModule" CodeFile="AdmRptWizModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="ajwc" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="ajwced" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" %>
<%@ Register TagPrefix="rcasp" NameSpace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<%@ Register TagPrefix="Module" TagName="Help" Src="HelpModule.ascx" %>
<script type="text/javascript" lang="javascript">
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChkExpNow)
	Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ChkPgDirty)
	Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
	function ChkPgDirty()
	{
		var x = document.getElementById('<%=bPgDirty.ClientID%>');
		var y = document.getElementById('<%=cPgDirty.ClientID%>');
		if (y != null) {if (x != null && x.value == 'Y') {y.style.visibility = '';} else {y.style.visibility = 'hidden';}}
	}
	function initializeRequestHandler(sender, args) {if (!fConfirm2('<%=bPgDirty.ClientID%>','<%=bConfirm.ClientID%>','<%=aNam.ClientID%>','<%=aVal.ClientID%>')) {args.set_cancel(true);}}
	function beginRequestHandler() {ShowProgress(); document.body.style.cursor='wait';}
	function endRequestHandler() {HideProgress(); document.body.style.cursor='auto';}
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
	function pageLoad() {
	$(window).resize(function () { $("#expand").dialog('option', 'position', { my: 'center', at: 'center', of: window }); });
	$("#expand").dialog({ maxWidth: 675, maxHeight: 575, width: '80%', height: '80%', modal: true, autoOpen: false, title: "Text Editor",
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
</script>
<asp:UpdatePanel ID="PanelTop" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cNewButton" /><asp:PostBackTrigger ControlID="cNewSaveButton" /><asp:PostBackTrigger ControlID="cCopyButton" /><asp:PostBackTrigger ControlID="cCopySaveButton" /><asp:PostBackTrigger ControlID="cSaveButton" /><asp:PostBackTrigger ControlID="cDeleteButton" /></Triggers><ContentTemplate>
<asp:ValidationSummary id="cValidSummary" CssClass="ValidSumm" EnableViewState="false" runat="server" />
<div id="AjaxSpinner" class="AjaxSpinner" style="display:none;">
	<div style="padding:10px;">
		<img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment..." runat="server" />
	</div>
</div>
<div class="r-table BannerGrp">
<div class="r-tr">
    <div class="r-td rc-1-4">
        <div class="BannerNam">
		    <asp:label id="cTitleLabel" CssClass="screen-title" runat="server" /><input type="image" name="cDefaultFocus" id="cClientFocusButton" src="images/Help_x.jpg" onclick="return false;" style="visibility:hidden;" />
        </div>
    </div>
    <div class="r-td rc-5-12">
    <div class="BannerBtn">
        <asp:Panel id="cButPanel" runat="server">
        <div class="BtnTbl">
		<div><asp:Button id="cSaveButton" onclick="cSaveButton_Click" runat="server" /></div>
		<div><asp:image id="cPgDirty" Style="visibility:hidden;" ImageUrl="../images/Xclaim.png" runat="server" /></div>
        <div class="moreButtonSec">
			<div><asp:Button id="cDeleteButton" onclick="cDeleteButton_Click" runat="server" CausesValidation="false" /></div>
			<div><asp:Button id="cCopySaveButton" onclick="cCopySaveButton_Click" runat="server" /></div>
			<div><asp:Button id="cCopyButton" onclick="cCopyButton_Click" runat="server" CausesValidation="false" /></div>
			<div><asp:Button id="cNewSaveButton" onclick="cNewSaveButton_Click" runat="server" CausesValidation="false" /></div>
			<div><asp:Button id="cNewButton" onclick="cNewButton_Click" runat="server" CausesValidation="false" /></div>
			<div><asp:Button id="cPreviewButton" runat="server" CausesValidation="false" /></div>
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
                    <div class="r-td r-content"><rcasp:ComboBox id="cAdmRptWiz95List" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cAdmRptWiz95List_SelectedIndexChanged" OnTextChanged="cAdmRptWiz95List_TextChanged" OnDDFindClick="cAdmRptWiz95List_DDFindClick" OnPostBack="cbPostBack" DataValueField="RptwizId183" DataTextField="RptwizId183Text" /></div>
                </div>
            </div>
        </div>
    </div>
    <div class="r-td rc-9-12">
        <div id="cSystem" class="screen-system" runat="server" visible="false">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label id="cSystemLabel" CssClass="inp-lbl" runat="server" /></div>&nbsp;
                    <div class="r-td r-content"><asp:DropDownList id="cSystemId" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cSystemId_SelectedIndexChanged" DataValueField="SystemId" DataTextField="SystemName" /></div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
<asp:Panel id="cTabFolder" CssClass="TabFolder" runat="server">
<input type="hidden" id="cCurrentTab" style="display:none" value="cTab3" runat="server"/>
<ul id="tabs">
    <li><a id="cTab3" href="#" class="current" name="Tab3" runat="server"></a></li>
    <li><a id="cTab22" href="#" name="Tab22" runat="server"></a></li>
    <li><a id="cTab33" href="#" name="Tab33" runat="server"></a></li>
    <li><a id="cTab44" href="#" name="Tab44" runat="server"></a></li>
    <li><a id="cTab55" href="#" name="Tab55" runat="server"></a></li>
    <li><a id="cTab66" href="#" name="Tab66" runat="server"></a></li>
    <li><a id="cTab88" href="#" name="Tab88" runat="server"></a></li>
    <li><a id="cTab99" href="#" name="Tab99" runat="server"></a></li>
    <li><a id="cTab77" href="#" name="Tab77" runat="server"></a></li>
</ul>
<div id="content">
    <div id="Tab3" runat="server">
    <asp:UpdatePanel id="UpdPanel3" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cXfer" /></Triggers><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-6"><div class="screen-tabfolder" runat="server"><div class="r-table">
    	<div class="r-tr">
    		<div id="cRptwizId183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizId183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizId183P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptwizId183" CssClass="inp-num" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptwizName183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizName183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizName183P2" class="r-td r-content" runat="server"><asp:TextBox id="cRptwizName183" CssClass="inp-txt" MaxLength="50" runat="server" /><asp:RequiredFieldValidator id="cRFVRptwizName183" ControlToValidate="cRptwizName183" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptwizDesc183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizDesc183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizDesc183P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRptwizDesc183" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="cRptwizDesc183" display="none" ErrorMessage="RptwizDesc <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:RequiredFieldValidator id="cRFVRptwizDesc183" ControlToValidate="cRptwizDesc183" display="none" runat="server" /><asp:Image id="cRptwizDesc183E" ImageUrl="../images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptwizTypeCd183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizTypeCd183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizTypeCd183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRptwizTypeCd183" CssClass="inp-ddl" DataValueField="RptwizTypeCd183" DataTextField="RptwizTypeCd183Text" AutoPostBack="true" OnSelectedIndexChanged="cRptwizTypeCd183_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVRptwizTypeCd183" ControlToValidate="cRptwizTypeCd183" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptCtrTypeDesc157P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptCtrTypeDesc157Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptCtrTypeDesc157P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cRptCtrTypeDesc157" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="cRptCtrTypeDesc157" display="none" ErrorMessage="RptCtrTypeDesc <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:Image id="cRptCtrTypeDesc157E" ImageUrl="../images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cTemplateName183P1" class="r-td r-labelR" runat="server"><asp:Label id="cTemplateName183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTemplateName183P2" class="r-td r-content" runat="server"><asp:TextBox id="cTemplateName183" CssClass="inp-txt" MaxLength="50" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptwizCatId183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizCatId183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizCatId183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRptwizCatId183" CssClass="inp-ddl" DataValueField="RptwizCatId183" DataTextField="RptwizCatId183Text" AutoPostBack="true" OnSelectedIndexChanged="cRptwizCatId183_SelectedIndexChanged" runat="server" /><asp:RequiredFieldValidator id="cRFVRptwizCatId183" EnableClientScript="false" ControlToValidate="cRptwizCatId183" display="none" runat="server" /><asp:imagebutton id="cRptwizCatId183Search" CssClass="r-icon" onclick="cRptwizCatId183Search_Click" runat="server" ImageUrl="../Images/Link.gif" CausesValidation="false" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cCatDescription181P1" class="r-td r-labelR" runat="server"><asp:Label id="cCatDescription181Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cCatDescription181P2" class="r-td r-content" runat="server"><asp:TextBox TextMode="MultiLine" id="cCatDescription181" CssClass="inp-txt" runat="server" /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="cCatDescription181" display="none" ErrorMessage="CatDescription <= 400 characters please." ValidationExpression="^[\s\S]{0,400}$" runat="server" /><asp:Image id="cCatDescription181E" ImageUrl="../images/Expand.gif" CssClass="r-icon show-expand-button" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cAccessCd183P1" class="r-td r-labelR" runat="server"><asp:Label id="cAccessCd183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cAccessCd183P2" class="r-td r-content" runat="server"><asp:RadioButtonList id="cAccessCd183" CssClass="inp-rad" RepeatDirection="Horizontal" DataValueField="AccessCd183" DataTextField="AccessCd183Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cUsrId183P1" class="r-td r-labelR" runat="server"><asp:Label id="cUsrId183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cUsrId183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cUsrId183" CssClass="inp-ddl" DataValueField="UsrId183" DataTextField="UsrId183Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-7-12"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div id="cReportId183P1" class="r-td r-labelR" runat="server"><asp:Label id="cReportId183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cReportId183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cReportId183" CssClass="inp-ddl" DataValueField="ReportId183" DataTextField="ReportId183Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRptwizCatImg181P1" class="r-td r-labelR" runat="server"><asp:Label id="cRptwizCatImg181Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRptwizCatImg181P2" class="r-td r-content" runat="server"><asp:ImageButton id="cRptwizCatImg181" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td"><asp:ImageButton id="cXfer" OnClick="cXfer_Click" Visible="false" ImageUrl="~/images/special/run.jpg" ToolTip="Click here to migrate this report to advanced report definition for further customization." runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab22" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel22" UpdateMode="Conditional" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-4"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOrientationCd183P1" class="r-td r-labelL" runat="server"><asp:Label id="cOrientationCd183Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOrientationCd183P2" class="r-td r-content" runat="server"><asp:RadioButtonList id="cOrientationCd183" CssClass="inp-rad" RepeatDirection="Horizontal" DataValueField="OrientationCd183" DataTextField="OrientationCd183Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUnitCd183P1" class="r-td r-labelL" runat="server"><asp:Label id="cUnitCd183Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cUnitCd183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cUnitCd183" CssClass="inp-ddl" DataValueField="UnitCd183" DataTextField="UnitCd183Text" runat="server" /><asp:RequiredFieldValidator id="cRFVUnitCd183" ControlToValidate="cUnitCd183" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-8"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div id="cTopMargin183P1" class="r-td r-labelR" runat="server"><asp:Label id="cTopMargin183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cTopMargin183P2" class="r-td r-content" runat="server"><asp:TextBox id="cTopMargin183" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVTopMargin183" ControlToValidate="cTopMargin183" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cBottomMargin183P1" class="r-td r-labelR" runat="server"><asp:Label id="cBottomMargin183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cBottomMargin183P2" class="r-td r-content" runat="server"><asp:TextBox id="cBottomMargin183" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVBottomMargin183" ControlToValidate="cBottomMargin183" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cLeftMargin183P1" class="r-td r-labelR" runat="server"><asp:Label id="cLeftMargin183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cLeftMargin183P2" class="r-td r-content" runat="server"><asp:TextBox id="cLeftMargin183" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVLeftMargin183" ControlToValidate="cLeftMargin183" display="none" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cRightMargin183P1" class="r-td r-labelR" runat="server"><asp:Label id="cRightMargin183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cRightMargin183P2" class="r-td r-content" runat="server"><asp:TextBox id="cRightMargin183" CssClass="inp-num" runat="server" /><asp:RequiredFieldValidator id="cRFVRightMargin183" ControlToValidate="cRightMargin183" display="none" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab33" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel33" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId33P1" class="r-td r-labelL" runat="server"><asp:Label id="cOriColumnId33Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId33P2" class="r-td r-content" runat="server"><asp:listbox id="cOriColumnId33" CssClass="inp-pic" SelectionMode="Multiple" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId33Text" OnSelectedIndexChanged="cOriColumnId33_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-4"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cSelect33" ImageUrl="../images/SlRight.gif" onclick="cSelect33_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cRemove33" ImageUrl="../images/SlLeft.gif" onclick="cRemove33_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-7"><div class="screen-tabfolder"><div class="r-table">
     	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId33P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId33Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId33P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId33" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId33Text" OnSelectedIndexChanged="cSelColumnId33_SelectedIndexChanged" runat="server" /></div>
    	</div>
     	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelChange33P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelChange33Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelChange33P2" class="r-td r-content" runat="server"><asp:TextBox id="cSelChange33" CssClass="inp-txt" runat="server" /><asp:ImageButton id="cSelButton33" CssClass="r-icon" ImageUrl="../images/SlChange.gif" onclick="cSelButton33_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-8"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cTop33" ImageUrl="../images/SlTop.gif" onclick="cTop33_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Top" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cUp33" ImageUrl="../images/SlUp.gif" onclick="cUp33_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Up" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cDown33" ImageUrl="../images/SlDown.gif" onclick="cDown33_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Down" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cBottom33" ImageUrl="../images/SlBottom.gif" onclick="cBottom33_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Bottom" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab44" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel44" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId44P1" class="r-td r-labelL" runat="server"><asp:Label id="cOriColumnId44Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId44P2" class="r-td r-content" runat="server"><asp:listbox id="cOriColumnId44" CssClass="inp-pic" SelectionMode="Multiple" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId33Text" OnSelectedIndexChanged="cOriColumnId44_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-4"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cSelect44" ImageUrl="../images/SlRight.gif" onclick="cSelect44_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cRemove44" ImageUrl="../images/SlLeft.gif" onclick="cRemove44_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-5-7"><div class="screen-tabfolder"><div class="r-table">
     	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId44P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId44Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId44P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId44" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId44" DataTextField="ColumnId44Text" OnSelectedIndexChanged="cSelColumnId44_SelectedIndexChanged" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:RadioButtonList id="cColSort" CssClass="inp-rad" AutoPostBack="true" RepeatDirection="Horizontal" cellpadding="0" cellspacing="0" BorderWidth="0px" OnSelectedIndexChanged="cColSort_SelectedIndexChanged" DataValueField="LabelKey" DataTextField="LabelText" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-8"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cTop44" ImageUrl="../images/SlTop.gif" onclick="cTop44_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Top" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cUp44" ImageUrl="../images/SlUp.gif" onclick="cUp44_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Up" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cDown44" ImageUrl="../images/SlDown.gif" onclick="cDown44_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Down" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cBottom44" ImageUrl="../images/SlBottom.gif" onclick="cBottom44_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Bottom" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab55" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel55" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId55P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId55Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId55P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId55" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId55Text" OnSelectedIndexChanged="cSelColumnId55_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-6"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:RadioButtonList id="cAggregate" CssClass="inp-rad" AutoPostBack="true" cellpadding="0" cellspacing="0" BorderWidth="0px" DataValueField="AggregateCd184" DataTextField="AggregateCd184Text" OnSelectedIndexChanged="cAggregate_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab66" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel66" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId66P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId66Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId66P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId66" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId66Text" OnSelectedIndexChanged="cSelColumnId66_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-5"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:RadioButtonList id="cRptGroup" CssClass="inp-rad" AutoPostBack="true" cellpadding="0" cellspacing="0" BorderWidth="0px" DataValueField="RptGroupId184" DataTextField="RptGroupId184Text" OnSelectedIndexChanged="cRptGroup_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-12"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:Image id="cRptGroupImg" Visible="false" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab88" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel88" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId88P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId88Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId88P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId88" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId88Text" OnSelectedIndexChanged="cSelColumnId88_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-5"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:RadioButtonList id="cRptChart" CssClass="inp-rad" AutoPostBack="true" cellpadding="0" cellspacing="0" BorderWidth="0px" DataValueField="RptChartCd184" DataTextField="RptChartCd184Text" OnSelectedIndexChanged="cRptChart_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-7"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRptChaTypeCd183P1" class="r-td r-labelL" runat="server"><asp:Label id="cRptChaTypeCd183Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cRptChaTypeCd183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cRptChaTypeCd183" CssClass="inp-ddl" DataValueField="RptChaTypeCd183" DataTextField="RptChaTypeCd183Text" OnSelectedIndexChanged="cRptChaTypeCd183_SelectedIndexChanged" AutoPostBack="true" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cThreeD183P1" class="r-td r-labelL" runat="server"><asp:Label id="cThreeD183Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cThreeD183P2" class="r-td r-content" runat="server"><asp:CheckBox id="cThreeD183" CssClass="inp-chk" OnCheckedChanged="cThreeD183_CheckedChanged" AutoPostBack="true" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-8-12"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cRptChaTypeImg183" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab99" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel99" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-5"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:Image ID="Image1" ImageUrl="../images/DshBrd.gif" Width="370px" Height="190px" AutoPostBack="false" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-8"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div id="cGMinValue183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGMinValue183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGMinValue183P2" class="r-td r-content" runat="server" style="white-space:nowrap;"><asp:Textbox id="cGMinValue183" CssClass="inp-num" AutoPostBack="false" runat="server" /><asp:Image ImageUrl="../images/a.gif" Width="20px" Height="20px" style="margin-left: 5px; vertical-align:middle;" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGLowRange183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGLowRange183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGLowRange183P2" class="r-td r-content" runat="server" style="white-space:nowrap;"><asp:Textbox id="cGLowRange183" CssClass="inp-num" AutoPostBack="false" runat="server" /><asp:Image ImageUrl="../images/b.gif" Width="20px" Height="20px" style="margin-left: 5px; vertical-align:middle;" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGMidRange183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGMidRange183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGMidRange183P2" class="r-td r-content" runat="server" style="white-space:nowrap;"><asp:Textbox id="cGMidRange183" CssClass="inp-num" AutoPostBack="false" runat="server" /><asp:Image ImageUrl="../images/c.gif" Width="20px" Height="20px" style="margin-left: 5px; vertical-align:middle;" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGMaxValue183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGMaxValue183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGMaxValue183P2" class="r-td r-content" runat="server" style="white-space:nowrap;"><asp:Textbox id="cGMaxValue183" CssClass="inp-num" AutoPostBack="false" runat="server" /><asp:Image ImageUrl="../images/d.gif" Width="20px" Height="20px" style="margin-left: 5px; vertical-align:middle;" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGNeedle183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGNeedle183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGNeedle183P2" class="r-td r-content" runat="server" style="white-space:nowrap;"><asp:Textbox id="cGNeedle183" CssClass="inp-num" AutoPostBack="false" runat="server" /><asp:Image ImageUrl="../images/e.gif" Width="20px" Height="20px" style="margin-left: 5px; vertical-align:middle;" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div id="cGFormat183P1" class="r-td r-labelR" runat="server"><asp:Label id="cGFormat183Label" CssClass="inp-lbl" runat="server" /></div>
    		<div id="cGFormat183P2" class="r-td r-content" runat="server"><asp:DropDownList id="cGFormat183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="GFormat183" DataTextField="GFormat183Text" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-10"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cGMinValueId183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="ColumnId33" DataTextField="ColumnId99Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cGLowRangeId183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="ColumnId33" DataTextField="ColumnId99Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cGMidRangeId183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="ColumnId33" DataTextField="ColumnId99Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cGMaxValueId183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="ColumnId33" DataTextField="ColumnId99Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cGNeedleId183" CssClass="inp-ddl" AutoPostBack="false" DataValueField="ColumnId33" DataTextField="ColumnId99Text" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:RadioButtonList id="cGPositive183" CssClass="inp-rad" AutoPostBack="false" RepeatDirection="Horizontal" cellpadding="0" cellspacing="0" DataValueField="GPositive183" DataTextField="GPositive183Text" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>

    <div id="Tab77" style="display:none" runat="server">
	<asp:UpdatePanel id="UpdPanel77" UpdateMode="Always" runat="server"><ContentTemplate>
    <div class="r-table">
    <div class="r-tr">
    <div class="r-td rc-1-3"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId77P1" class="r-td r-labelL" runat="server"><asp:Label id="cOriColumnId77Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cOriColumnId77P2" class="r-td r-content" runat="server"><asp:listbox id="cOriColumnId77" CssClass="inp-pic" SelectionMode="Multiple" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId33" DataTextField="ColumnId33Text" OnSelectedIndexChanged="cOriColumnId77_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-4-5"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cSelect77" ImageUrl="../images/SlRight.gif" onclick="cSelect77_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cRemove77" ImageUrl="../images/SlLeft.gif" onclick="cRemove77_Click" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div class="r-td r-content" runat="server"><asp:DropDownList id="cOperator" CssClass="inp-ddl" AutoPostBack="true" DataValueField="Operator184" DataTextField="Operator184Text" OnSelectedIndexChanged="cOperator_SelectedIndexChanged" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-6-8"><div class="screen-tabfolder"><div class="r-table">
     	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelColumnId77P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelColumnId77Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div></div>
    		<div id="cSelColumnId77P2" class="r-td r-content" runat="server"><asp:listbox id="cSelColumnId77" CssClass="inp-pic" AutoPostBack="true" BorderWidth="2px" DataValueField="ColumnId77" DataTextField="ColumnId77Text" OnSelectedIndexChanged="cSelColumnId77_SelectedIndexChanged" runat="server" /></div>
    	</div>
     	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelChange77P1" class="r-td r-labelL" runat="server"><asp:Label id="cSelChange77Label" CssClass="inp-lbl" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td"></div>
    		<div id="cSelChange77P2" class="r-td r-content" runat="server"><asp:TextBox id="cSelChange77" CssClass="inp-txt" runat="server" /><asp:ImageButton id="cSelButton77" CssClass="r-icon" ImageUrl="../images/SlChange.gif" onclick="cSelButton77_Click" runat="server" /></div>
    	</div>
    </div></div></div>
    <div class="r-td rc-9-9"><div class="screen-tabfolder"><div class="r-table">
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cTop77" ImageUrl="../images/SlTop.gif" onclick="cTop77_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Top" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cUp77" ImageUrl="../images/SlUp.gif" onclick="cUp77_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Up" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cDown77" ImageUrl="../images/SlDown.gif" onclick="cDown77_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Down" runat="server" /></div>
    	</div>
    	<div class="r-tr">
    		<div class="r-td r-content" runat="server"><asp:ImageButton id="cBottom77" ImageUrl="../images/SlBottom.gif" onclick="cBottom77_Click" runat="server" /></div>
    		<div class="r-td r-labelL" runat="server"><asp:Label CssClass="inp-lbl" Text="Bottom" runat="server" /></div>
    	</div>
    </div></div></div>
    </div></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
</div>
</asp:Panel>
<asp:UpdatePanel ID="PanelVar" UpdateMode="Conditional" runat="server"><ContentTemplate>
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
