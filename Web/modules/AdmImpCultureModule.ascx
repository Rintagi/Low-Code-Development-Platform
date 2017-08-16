<%@ Control Language="c#" Inherits="RO.Web.AdmImpCultureModule" CodeFile="AdmImpCultureModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
    function beginRequestHandler() { ShowProgress(); document.body.style.cursor = 'wait'; }
    function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; }
</script>
<div id="AjaxSpinner" class="AjaxSpinner" style="display:none;">
	<div style="padding:10px;">
		<img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment..." runat="server" />
	</div>
</div>
<div class="r-table wizard-header">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-title"><asp:label id="cTitleLabel" runat="server" /></div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div class="wizard-help">
            <asp:label id="cHelpLabel" runat="server" />
        </div>
    </div>
</div>
</div>
<asp:UpdatePanel ID="PanelTop" UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cBrowseButton" /></Triggers><ContentTemplate>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-4">
        <div class="wizard-image">
            <div style="float:right;"><img src="./images/wizard/report-dsk.jpg" class="wizard-image-dsk" style="max-width:200px" /></div>
            <div style="float:none;"><img src="./images/wizard/report-mob.jpg" class="wizard-image-mob" style="max-width:500px" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div class="r-td rc-5-12">
        <div class="wizard-content">
        <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td colspan="3"><h3>Server Location:</h3></td>
        </tr>
        <tr>
            <td colspan="2"><asp:CheckBox id="cAllFile" CssClass="inp-chk" runat="server" AutoPostBack="true" onCheckedChanged="cAllFile_CheckedChanged" ToolTip="Check here to prepare all spreadsheets for import." /><span>All SpreadSheets</span></td>
            <td align="right"><span>WorkSheets:</span><asp:TextBox id="cWorkSheetM" CssClass="inp-txt" Text="Sheet1" runat="server" Style="max-width:200px;" ToolTip="Please enter a worksheet name or if an integer is entered instead, it could be interpreted as either worksheet order or name. May not function properly on Excel-97 spreadsheet." /></td>
        </tr>
        <tr>
            <td colspan="3"><asp:textbox id="cLocation" CssClass="inp-txt" runat="server" width="100%" /></td>
            <td><asp:Button id="cListButton" onclick="cListButton_Click" runat="server" CausesValidation="false" /></td>
        </tr>
        <tr>
            <td colspan="3"><asp:textbox id="cSearch" CssClass="inp-txt" runat="server" width="100%" MaxLength="25" /></td>
            <td><asp:Button id="cSearchButton" onclick="cSearchButton_Click" runat="server" CausesValidation="false" /></td>
        </tr>
        <tr>
            <td colspan="3"><asp:listbox id="cFileList" CssClass="inp-pic" SelectionMode="Multiple" runat="server" AutoPostBack="true" onSelectedIndexChanged="cFileList_SelectedIndexChanged" rows="8" width="100%" DataValueField="FileName" DataTextField="FileFullName" /></td>
        </tr>
        <tr>
            <td colspan="2"><h3>Local Location:</h3></td>
            <td align="right"><span>StartRow:</span><asp:TextBox id="cStartRow" CssClass="inp-ctr" Text="2" runat="server" Style="max-width:30px;" ToolTip="Please enter the starting row to be imported and must be greater than or equal to 2 because heading is compulsory." /></td>
        </tr>
        <tr>
            <td colspan="3">
            <div class="rg-1-5"><asp:FileUpload id="cBrowse" CssClass="inp-txt" runat="server" style="padding: 0px;" /><asp:button id="cBrowseButton" OnClick="cBrowseButton_Click" Style="display:none;" runat="server" /></div>
            <div class="rg-6-10"><span>WorkSheet:</span><asp:DropDownList id="cWorkSheet" CssClass="inp-ddl" runat="server" style="max-width:120px;" /></div>
            <div class="rg-11-12"><span>Overwrite:</span><asp:CheckBox id="cOverwrite" CssClass="inp-chk" runat="server" Checked="false" Enabled="false" AutoPostBack="false" ToolTip="Check here to overwrite previous import, if applicable." /><asp:ImageButton id="cSchemaImage" style="vertical-align:middle;" runat="server" ImageUrl="../images/Schema.gif" /></div>
            </td>
        </tr>
        <tr>
            <td colspan="3"><hr /><span style="float:right; text-align:right;"><asp:label id="cMsgLabel" CssClass="MsgText" runat="server" width="100%" /><asp:Button id="cImportButton" onclick="cImportButton_Click" runat="server" /></span></td>
        </tr>
        </table>
        </div>
    </div>
</div>
</div>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<asp:TextBox id="cFNameO" text="" Width="0px" style="visibility:hidden;" runat="server" />
<asp:TextBox id="cFName" text="" Width="0px" style="visibility:hidden;" runat="server" />
