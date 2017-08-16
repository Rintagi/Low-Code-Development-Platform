<%@ Control Language="c#" Inherits="RO.Web.GenDbPortingModule" CodeFile="GenDbPortingModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
<asp:UpdatePanel UpdateMode="Conditional" runat="server"><ContentTemplate>
<div class="r-table wizard-header">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-title">
            <asp:label id="cTitleLabel" runat="server" />
        </div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-entity">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label ID="Label2" CssClass="inp-lbl" runat="server" text="Entity:" /></div>&nbsp;
                    <div class="r-td r-content"><asp:DropDownList id="cEntityId" CssClass="inp-ddl" runat="server" AutoPostBack="true" onSelectedIndexChanged="cEntityId_SelectedIndexChanged" DataValueField="EntityId" DataTextField="EntityName" /></div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div class="wizard-help"><asp:label id="cHelpLabel" runat="server" /></div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-4">
        <div class="wizard-image">
            <div style="float:right;"><img src="./images/wizard/dbporting-dsk.jpg" class="wizard-image-dsk" style="max-width:200px" /></div>
            <div style="float:none;"><img src="./images/wizard/dbporting-mob.jpg" class="wizard-image-mob" style="max-width:500px" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div class="r-td rc-5-12">
        <div class="wizard-content">
		<table cellspacing="0" cellpadding="0" border="0" width="100%">
			<tr>
				<td>
					<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
			                <td style="width:100px"><asp:Label cssclass="inp-lbl" runat="server" text="Source:" /></td>
							<td><asp:DropDownList id="cSrcDataTierId" CssClass="inp-ddl" runat="server" AutoPostBack="true" DataValueField="DataTierId" DataTextField="DataTierName" /></td>
						</tr>
						<tr>
			                <td style="width:100px"><asp:Label cssclass="inp-lbl" runat="server" text="Database:" /></td>
							<td><asp:DropDownList id="cSrcSystemId" CssClass="inp-ddl" runat="server" AutoPostBack="true" DataValueField="SystemId" DataTextField="SystemName" /></td>
						</tr>
					</table>
				</td>
				<td>
					<asp:RadioButtonList id="cSrcSystemIdDb" CssClass="inp-rad" runat="server" AutoPostBack="true" cellpadding="0" cellspacing="1" borderstyle="Ridge" borderwidth="2">
						<asp:ListItem />
						<asp:ListItem Selected="true" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td colspan="2"><hr></td>
			</tr>
			<tr>
				<td>
					<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
			                <td style="width:100px"><asp:Label cssclass="inp-lbl" runat="server" text="Target:" /></td>
							<td><asp:DropDownList id="cTarDataTierId" CssClass="inp-ddl" runat="server" AutoPostBack="true" DataValueField="DataTierId" DataTextField="DataTierName" /></td>
						</tr>
						<tr>
			                <td style="width:100px"><asp:Label cssclass="inp-lbl" runat="server" text="Database:" /></td>
							<td><asp:DropDownList id="cTarSystemId" CssClass="inp-ddl" runat="server" AutoPostBack="true" DataValueField="SystemId" DataTextField="SystemName" /></td>
						</tr>
					</table>
				</td>
				<td>
					<asp:RadioButtonList id="cTarSystemIdDb" CssClass="inp-rad" runat="server" enabled="false" AutoPostBack="false" cellpadding="0" cellspacing="1" borderstyle="Ridge" borderwidth="2">
						<asp:ListItem />
						<asp:ListItem Selected="true" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td colspan="2"><hr></td>
			</tr>
			<tr>
				<td>
					<table cellspacing="1" cellpadding="0" border="0" width="100%">
						<tr>
							<td><asp:CheckBox id="cClearDb" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="Clear Respective Target First" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cEncryptSp" cssclass="inp-chk" runat="server" AutoPostBack="true" Enabled="false" Text="Encrypt Stored Procedures" TextAlign="Right" /></td>
						</tr>
					</table>
				</td>
				<td align="right">
					<table cellspacing="1" cellpadding="0" border="0" width="100%">
						<tr>
							<td align="right"><asp:CheckBox ID="cAllScript" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="All Scripts" TextAlign="Left" /></td>
						</tr>
						<tr>
							<td align="right"><asp:CheckBox ID="cExecScript" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="Execute Scripts" TextAlign="Left" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2"><hr></td>
			</tr>
			<tr>
				<td colspan="2">
					<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
							<td><asp:CheckBox id="cTable" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="1. Script all table and primary key to NTable.sql (Removes all tables, data, views, indexes and keys)" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cBcpOut" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="2. Script Bulk-Copy Out to BcpOut.bat" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cBcpIn" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="3. Script Bulk-Copy In to BcpIn.sql and BcpIn.bat (Removes all data and foreign keys)" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cIndex" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="4. Script all index and foreign key to NIndex.sql (Removes all indexes and foreign keys)" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cView" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="5. Script all view to NView.sql (Removes all views)" TextAlign="Right" /></td>
						</tr>
						<tr>
							<td><asp:CheckBox id="cSp" cssclass="inp-chk" runat="server" AutoPostBack="true" Text="6. Script all stored procedure to NSp.sql (Removes all stored procedures)" TextAlign="Right" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
				        <td style="width:120px; vertical-align:top;"><asp:label id="cExemptLabel" cssclass="inp-lbl" Text="Tables Exempted:" runat="server" /></td>
				        <td><asp:TextBox id="cExemptText" cssclass="inp-txt" TextMode="MultiLine" style="height:80px" MaxLength="1000" runat="server" /></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div class="wizard-action">
		    <span style="text-align:right;">
			    <asp:label cssclass="MsgText" id="cMsgLabel" runat="server" />
			    <asp:Button id="cGenButton" CssClass="small blue button" onClick="cGenButton_Click" runat="server" text="Start Porting" tooltip="Click here to start porting selected database." />
		    </span>
        </div>
    </div>
</div>
</div>
<input id="bErrNow" type="hidden" runat="server" />
<input id="bInfoNow" type="hidden" runat="server" />
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
