<%@ Control Language="c#" Inherits="RO.Web.SqlReportModule" CodeFile="SqlReportModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="rs" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<%@ Register TagPrefix="ajwc" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="rcasp" NameSpace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<%@ Register TagPrefix="Module" TagName="Help" Src="../modules/HelpModule.ascx" %>
<script lang="javascript" type="text/javascript">
    Sys.Application.add_load(function () { ApplyJQueryWidget('', ''); });
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
    function beginRequestHandler() { try { $('input[Behaviour=\'Date\']').datepicker('destroy'); myFocus = document.forms[0].__EVENTTARGET.value; } catch (e) { }; }
    function endRequestHandler() { try { var f = $('#' + myFocus.replace(/\$/g, '_')); if (f.attr('Behaviour') == 'Date') nextOnTabIndex(f).focus(); } catch (e) { }; }
</script>
<asp:Panel id="cBanPanel" CssClass="BannerGrp banner-container header-container" runat="server" Visible="false">
<div class="r-table BannerGrp">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="BannerNam">
            <asp:label id="cTitleLabel" CssClass="screen-title" runat="server" /><input type="image" name="cDefaultFocus" id="cClientFocusButton" src="images/Help_x.jpg" onclick="return false;" style="visibility:hidden;" />
        </div>
    </div>
    <div class="r-td rc-7-12">
        <div class="BannerBtn BtnTbl">
            <div><asp:Button id="cViewButton" onclick="cViewButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpPdfButton" onclick="cExpPdfButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpDocButton" onclick="cExpDocButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpXlsButton" onclick="cExpXlsButton_Click" runat="server" /></div>
            <div><asp:Button id="cPrintButton" onclick="cPrintButton_Click" runat="server" /></div>
            <div style="margin-top:4px;"><asp:DropDownList id="cPrinter" CssClass="inp-ddl" runat="server" DataTextField="PrinterName" DataValueField="PrinterPath" AutoPostBack="false" /></div>
            <div><asp:Button id="cShowCriButton" onclick="cShowCriButton_Click" runat="server" /></div>
            <div class="CustBtnCnt"><asp:Button id="cCustomizeButton" runat="server" /></div>
            <div class="HelpMsgCnt"><Module:Help id="cHelpMsg" runat="server" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
</div>
</div>
</asp:Panel>
<asp:UpdatePanel runat="server"><ContentTemplate>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
<tr><td>
	<asp:Panel id="cMemPanel" runat="server" Visible="false">
	<br />
	<table cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td><div><asp:Label id="cMemCriDdll" CssClass="inp-lbl" Text="Favorites:" runat="server" Width="80px" /></div></td>
		<td>&nbsp;</td>
		<td>
			<div><rcasp:ComboBox id="cMemCriDdl" CssClass="inp-ddl" runat="server" Width="400px" Mode="A" AutoPostBack="true" OnSelectedIndexChanged="cMemCriDdl_SelectedIndexChanged" OnPostBack="cbPostBack" OnSearch="cbMemCriDdl" DataValueField="RptMemCriId" DataTextField="RptMemCriName" Visible="true" /></div>
			<div><input id="cMemCriId" type="hidden" runat="server" /></div>
			<div><asp:TextBox id="cMemCriName" CssClass="inp-txt" runat="server" Width="400px" Visible="false" ToolTip="Please enter a name to mormorize this report citeria." /></div>
		</td>
		<td>&nbsp;</td>
		<td><asp:ImageButton id="cMemCriNew" ImageUrl="../Images/Badd.gif" OnClick="cMemCriNew_Click" runat="server" Visible="true" ToolTip="Press this to add this report criteria to favorites." /></td>
		<td><asp:ImageButton id="cMemCriUpd" ImageUrl="../Images/Bedit.gif" OnClick="cMemCriUpd_Click" runat="server" Visible="false" ToolTip="Press this to change the name of this favorite." /></td>
		<td><asp:ImageButton id="cMemCriDel" ImageUrl="../Images/Btrash.gif" OnClick="cMemCriDel_Click" runat="server" Visible="false" ToolTip="Press this to remove this favorite." /></td>
		<td><asp:ImageButton id="cMemCriSav" ImageUrl="../Images/Bsave.gif" OnClick="cMemCriSav_Click" runat="server" Visible="false" ToolTip="Press this to save this favorite." /></td>
		<td><asp:Button id="cMemCriCnc" BorderWidth="0" Font-Size="Small" Font-Underline="true" BackColor="transparent" Text="cancel" Style="cursor:pointer;" OnClick="cMemCriCnc_Click" runat="server" Visible="false" ToolTip="Press this to cancel current action." /></td>
		<td>&nbsp;</td>
		<td><div><asp:RadioButtonList id="cPublicCri" CssClass="inp-lbl" AutoPostBack="false" RepeatDirection="Horizontal" cellpadding="0" cellspacing="0" DataValueField="AccessCd" DataTextField="AccessCdText" Visible="false" runat="server" /></div></td>
	</tr></table>
	</asp:Panel>
</td></tr>
<tr><td>
	<asp:Panel id="cSavPanel" runat="server" Visible="false">
	<table cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td><div><asp:Label id="cMemCriDescl" CssClass="inp-lbl" Text="Description:" runat="server" Width="80px" /></div></td>
		<td>&nbsp;</td>
		<td><div><asp:TextBox id="cMemCriDesc" CssClass="inp-txt" runat="server" Width="400px" MaxLength="500" ToolTip="Please enter a description, if desired." /></div></td>
	</tr>
	<tr>
		<td><div><asp:Label id="cMemFldDdll" CssClass="inp-lbl" Text="Folder*:" runat="server" Width="80px" /></div></td>
		<td>&nbsp;</td>
		<td>
			<div><rcasp:ComboBox id="cMemFldDdl" CssClass="inp-ddl" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="cMemFldDdl_SelectedIndexChanged" OnPostBack="cbPostBack" OnSearch="cbMemFldDdl" DataValueField="RptMemFldId" DataTextField="RptMemFldName" ToolTip="Please select a folder, if applicable." /></div>
			<div><input id="cMemFldId" type="hidden" runat="server" /></div>
	        <div><asp:TextBox id="cMemFldName" CssClass="inp-txt" runat="server" Width="400px" Visible="false" /></div>
		</td>
	    <td>&nbsp;</td>
	    <td><asp:ImageButton id="cMemFldNew" ImageUrl="../Images/Badd.gif" OnClick="cMemFldNew_Click" runat="server" Visible="true" ToolTip="Press this for a new folder." /></td>
	    <td><asp:ImageButton id="cMemFldUpd" ImageUrl="../Images/Bedit.gif" OnClick="cMemFldUpd_Click" runat="server" Visible="false" ToolTip="Press this to change the name of this selected folder." /></td>
	    <td><asp:ImageButton id="cMemFldDel" ImageUrl="../Images/Btrash.gif" OnClick="cMemFldDel_Click" runat="server" Visible="false" ToolTip="Press this to remove this folder." /></td>
	    <td><asp:ImageButton id="cMemFldSav" ImageUrl="../Images/Bsave.gif" OnClick="cMemFldSav_Click" runat="server" Visible="false" ToolTip="Press this to save this folder by this name." /></td>
	    <td><asp:Button id="cMemFldCnc" BorderWidth="0" Font-Size="Small" Font-Underline="true" BackColor="transparent" Text="cancel" Style="cursor:pointer;" OnClick="cMemFldCnc_Click" runat="server" Visible="false" ToolTip="Press this to cancel current action." /></td>
	    <td>&nbsp</td>
	    <td><div><asp:RadioButtonList id="cPublicFld" CssClass="inp-rad" AutoPostBack="false" RepeatDirection="Horizontal" cellpadding="0" cellspacing="0" DataValueField="AccessCd" DataTextField="AccessCdText" Visible="false" runat="server" /></div></td>
	</tr></table>
	</asp:Panel>
</td></tr>
</table>
<asp:Panel id="cCriPanel" runat="server" Visible="true" style="min-height:440px;">
<fieldset class="criteria-grp"><legend>CRITERIA<span><asp:Button id="cClearCriButton" onclick="cClearCriButton_Click" runat="server" CausesValidation="false" /></span></legend>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-8">
        <div class="screen-criteria">
            <asp:PlaceHolder id="cCriteria" EnableViewState="true" runat="server" />
        </div>
        <div>
            <asp:Button id="cSelectHint" BorderWidth="0" Font-Size="Small" Font-Underline="false" BackColor="transparent" Style="text-align:left;cursor:pointer;" OnClick="cSelectHint_Click" runat="server" Visible="false" />
        </div>
        <div>
            <asp:CheckBoxList id="cSelectList" DataValueField="ColumnName" DataTextField="ColumnHeader" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4" TextAlign="Left" CssClass="inp-chk" CellSpacing="5" AutoPostBack="true" runat="server" Visible="false" />
        </div>
    </div>
</div>
</div>
</fieldset>
</asp:Panel>
<table>
<tr><td><div style="width:1px"><rs:ReportViewer id="cViewer" EnableViewState="true" Width="100%" BorderWidth="0" runat="server" Visible="false" Height="550px" PageCountMode="Actual" SizeToReportContent="true" ShowZoomControl="true" ShowBackButton="true" ShowReportBody="true" ShowParameterPrompts="false" ShowCredentialPrompts="false" /></div></td></tr>
<tr><td><asp:PlaceHolder id="cPlaceHolder" EnableViewState="false" runat="server" Visible="false" /></td></tr>
</table>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
