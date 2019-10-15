<%@ Control Language="c#" Inherits="RO.Web.AdmRptUsageModule" CodeFile="AdmRptUsageModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web" %>
<%@ Register TagPrefix="ajwc" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="rcasp" NameSpace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<%@ Register TagPrefix="Module" TagName="Help" Src="../modules/HelpModule.ascx" %>
<script type="text/javascript" lang="javascript">
	Sys.Application.add_load(function () { ApplyJQueryWidget('', ''); });
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
	function beginRequestHandler() {try {$('input[Behaviour=\'Date\']').datepicker('destroy');  myFocus = document.forms[0].__EVENTTARGET.value; } catch (e) { };   ShowProgress(); document.body.style.cursor = 'wait'; }
	function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; try {var f = $('#' + myFocus.replace(/\$/g,'_')); if (f.attr('Behaviour') == 'Date') nextOnTabIndex(f).focus(); } catch (e) { }; }
	function IniVars(BannerWidth) {BannerWidth = getWidth(document.getElementById('BannerTable'));}
	Event.observe(window,'load',function() {IniVars(document.getElementById('<%=cViewerWidth.ClientID%>'));});
	Event.observe(window,'resize',function() {IniVars(document.getElementById('<%=cViewerWidth.ClientID%>'));});
</script>
<div id="AjaxSpinner" class="AjaxSpinner" style="display:none;">
	<div style="padding:10px;">
		<img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment..." runat="server" />
	</div>
</div>
<asp:UpdatePanel UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cExpPdfButton" /><asp:PostBackTrigger ControlID="cExpDocButton" /><asp:PostBackTrigger ControlID="cExpXlsButton" /><asp:PostBackTrigger ControlID="cExpTxtButton" /><asp:PostBackTrigger ControlID="cViewButton" /><asp:PostBackTrigger ControlID="cShowCriButton" /></Triggers><ContentTemplate>
<div class="r-table BannerGrp">
<div class="r-tr">
    <div class="r-td rc-1-4">
        <div class="BannerNam"><asp:label id="cTitleLabel" CssClass="screen-title" runat="server" /><input type="image" name="cDefaultFocus" id="cClientFocusButton" src="images/Help_x.jpg" onclick="return false;" style="visibility:hidden;" /></div>
    </div>
    <div class="r-td rc-5-12">
        <div class="BannerBtn">
        <asp:Panel id="cButPanel" runat="server">
        <div class="BtnTbl">
            <div><asp:Button id="cViewButton" onclick="cViewButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpPdfButton" onclick="cExpPdfButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpDocButton" onclick="cExpDocButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpXlsButton" onclick="cExpXlsButton_Click" runat="server" /></div>
            <div><asp:Button id="cExpTxtButton" onclick="cExpTxtButton_Click" runat="server" /></div>
            <div><asp:Button id="cPrintButton" onclick="cPrintButton_Click" runat="server" /></div>
            <div style="margin-top:4px;"><asp:DropDownList id="cPrinter" CssClass="inp-ddl" runat="server" DataTextField="PrinterName" DataValueField="PrinterPath" AutoPostBack="false" /></div>
            <div><asp:Button id="cShowCriButton" onclick="cShowCriButton_Click" runat="server" /></div>
            <div><Module:Help id="cHelpMsg" runat="server" /></div>
            <div style="clear:both;"></div>
        </div>
        </asp:Panel>
        </div>
    </div>
</div>
</div>
<table cellspacing="0" cellpadding="0" border="0" style="margin:15px 10px 10px 10px;">
	<tr>
		<td>
			<asp:Panel id="cCriteria" runat="server" wrap="false" BorderWidth="0px" style="min-height:440px;">
			<fieldset class="criteria-grp" style="padding:10px;"><legend>CRITERIA<span><asp:Button id="cClearCriButton" onclick="cClearCriButton_Click" runat="server" CausesValidation="false" /></span></legend>
			<asp:Table cellspacing="0" cellpadding="0" runat="server">
			<asp:TableRow VerticalAlign="top"><asp:TableCell>
				<asp:Panel id="cGrp5" runat="server" BorderWidth="1px" CssClass="group-panel" style="padding: 8px 6px;">
				<asp:Table cellspacing="0" cellpadding="0" runat="server">
				<asp:TableRow VerticalAlign="top">
					<asp:TableCell id="cSummaryP1" CssClass="GrpContent" runat="server"><div><asp:Label id="cSummaryLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><asp:CheckBox id="cSummary" CssClass="inp-txt" AutoPostBack="true" OnCheckedChanged="cSummary_CheckedChanged" runat="server" /></asp:TableCell>
					<asp:TableCell id="cSystemIdP1" CssClass="GrpContent" runat="server"><div><asp:Label id="cSystemIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><asp:DropDownList id="cSystemId" CssClass="inp-ddl" DataValueField="SystemId" DataTextField="SystemName" AutoPostBack="true" OnSelectedIndexChanged="cSystemId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					<asp:TableCell id="cFrDateP1" CssClass="GrpContent" runat="server"><div><asp:Label id="cFrDateLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><asp:TextBox id="cFrDate" CssClass="inp-txt" AutoPostBack="true" OnTextChanged="cFrDate_TextChanged" runat="server" /></asp:TableCell>
					<asp:TableCell id="cToDateP1" CssClass="GrpContent" runat="server"><div><asp:Label id="cToDateLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><asp:TextBox id="cToDate" CssClass="inp-txt" AutoPostBack="true" OnTextChanged="cToDate_TextChanged" runat="server" /></asp:TableCell>
				</asp:TableRow>
				</asp:Table>
				</asp:Panel>
			</asp:TableCell></asp:TableRow>
			<asp:TableRow VerticalAlign="top"><asp:TableCell>
				<asp:Panel id="cGrp6" runat="server" BorderWidth="1px" CssClass="group-panel" style="padding: 8px 6px;">
				<asp:Table cellspacing="0" cellpadding="0" runat="server">
				<asp:TableRow VerticalAlign="top">
				<asp:TableCell>
					<asp:Panel id="cGrp7" runat="server" BorderWidth="0px" CssClass="group-panel">
					<asp:Table cellspacing="0" cellpadding="0" runat="server">
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cCompanyIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cCompanyIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cCompanyId" CssClass="inp-ddl" DataValueField="CompanyId" DataTextField="CompanyDesc" AutoPostBack="true" OnSelectedIndexChanged="cCompanyId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cProjectIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cProjectIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cProjectId" CssClass="inp-ddl" DataValueField="ProjectId" DataTextField="ProjectDesc" AutoPostBack="true" OnSelectedIndexChanged="cProjectId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cAgentIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cAgentIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cAgentId" CssClass="inp-ddl" DataValueField="AgentId" DataTextField="AgentName" AutoPostBack="true" OnSelectedIndexChanged="cAgentId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cBrokerIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cBrokerIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cBrokerId" CssClass="inp-ddl" DataValueField="BrokerId" DataTextField="BrokerName" AutoPostBack="true" OnSelectedIndexChanged="cBrokerId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cVendorIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cVendorIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cVendorId" CssClass="inp-ddl" DataValueField="VendorId" DataTextField="VendorName" AutoPostBack="true" OnSelectedIndexChanged="cVendorId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					</asp:Table>
					</asp:Panel>
				</asp:TableCell>
				<asp:TableCell>
					<asp:Panel id="cGrp8" runat="server" BorderWidth="0px" CssClass="group-panel">
					<asp:Table cellspacing="0" cellpadding="0" runat="server">
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cCustomerIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cCustomerIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cCustomerId" CssClass="inp-ddl" DataValueField="CustomerId" DataTextField="CustomerName" AutoPostBack="true" OnSelectedIndexChanged="cCustomerId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cInvestorIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cInvestorIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cInvestorId" CssClass="inp-ddl" DataValueField="InvestorId" DataTextField="InvestorName" AutoPostBack="true" OnSelectedIndexChanged="cInvestorId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cMemberIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cMemberIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cMemberId" CssClass="inp-ddl" DataValueField="MemberId" DataTextField="MemberName" AutoPostBack="true" OnSelectedIndexChanged="cMemberId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cBorrowerIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cBorrowerIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cBorrowerId" CssClass="inp-ddl" DataValueField="BorrowerId" DataTextField="BorrowerName" AutoPostBack="true" OnSelectedIndexChanged="cBorrowerId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cGuarantorIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cGuarantorIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cGuarantorId" CssClass="inp-ddl" DataValueField="GuarantorId" DataTextField="GuarantorName" AutoPostBack="true" OnSelectedIndexChanged="cGuarantorId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow VerticalAlign="top">
						<asp:TableCell id="cLenderIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cLenderIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
						<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cLenderId" CssClass="inp-ddl" DataValueField="LenderId" DataTextField="LenderName" AutoPostBack="true" OnSelectedIndexChanged="cLenderId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					</asp:TableRow>
					</asp:Table>
					</asp:Panel>
				</asp:TableCell>
				</asp:TableRow>
				</asp:Table>
				</asp:Panel>
			</asp:TableCell></asp:TableRow>
			<asp:TableRow VerticalAlign="top"><asp:TableCell>
				<asp:Panel id="cGrp9" runat="server" BorderWidth="1px" CssClass="group-panel" style="padding: 8px 6px;">
				<asp:Table cellspacing="0" cellpadding="0" runat="server">
				<asp:TableRow VerticalAlign="top">
					<asp:TableCell id="cUsrGroupIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cUsrGroupIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><asp:DropDownList id="cUsrGroupId" CssClass="inp-ddl" DataValueField="UsrGroupId" DataTextField="UsrGroupName" AutoPostBack="true" OnSelectedIndexChanged="cUsrGroupId_SelectedIndexChanged" runat="server" /></asp:TableCell>
					<asp:TableCell id="cUserIdP1" CssClass="GrpLabel100" runat="server"><div><asp:Label id="cUserIdLabel" CssClass="inp-lbl" runat="server" /></div></asp:TableCell>
					<asp:TableCell CssClass="GrpContent"><rcasp:ComboBox id="cUserId" CssClass="inp-ddl" DataValueField="UserId" DataTextField="UsrName" AutoPostBack="true" OnSelectedIndexChanged="cUserId_SelectedIndexChanged" runat="server" /></asp:TableCell>
				</asp:TableRow>
				</asp:Table>
				</asp:Panel>
			</asp:TableCell></asp:TableRow>
			</asp:Table>
			</fieldset>
			</asp:Panel>
		</td>
	</tr>
	<tr>
		<td>
			<cr:crystalreportviewer id="cViewer" runat="server" Visible="false" BestFitPage="true" HasPrintButton="false" HasToggleGroupTreeButton="false" HasExportButton="false" />
		</td>
	</tr>
</table>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
<input id="cViewerWidth" type="hidden" runat="server" />
