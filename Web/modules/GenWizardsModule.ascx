<%@ Control Language="c#" Inherits="RO.Web.GenWizardsModule" CodeFile="GenWizardsModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
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
                    <div class="r-td r-labelR"><asp:Label ID="Label8" CssClass="inp-lbl" runat="server" text="Entity:" /></div>&nbsp;
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
        <div class="wizard-help">
			<asp:label id="cHelpLabel" runat="server" />
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-image">
            <div style="float:right;"><img src="./images/wizard/wizard-dsk.jpg" class="wizard-image-dsk" style="max-width:200px" /></div>
            <div style="float:none;"><img src="./images/wizard/wizard-mob.jpg" class="wizard-image-mob" style="max-width:500px" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-content">
		    <table cellspacing="0px" cellpadding="0px" border="0px" width="100%">
		    <tr>
			    <td><asp:Label ID="Label2" cssclass="inp-lbl" runat="server" text="Client Tier:" /></td>
			    <td colspan="3"><asp:DropDownList id="cClientTierId" cssclass="inp-ddl" runat="server" onSelectedIndexChanged="cClientTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="ClientTierId" DataTextField="ClientTierName" /></td>
		    </tr>
		    <tr>
			    <td><asp:Label ID="Label3" cssclass="inp-lbl" runat="server" text="Rule Tier:" /></td>
			    <td colspan="3"><asp:DropDownList id="cRuleTierId" cssclass="inp-ddl" runat="server" onSelectedIndexChanged="cRuleTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="RuleTierId" DataTextField="RuleTierName" /></td>
		    </tr>
		    <tr>
			    <td><asp:Label ID="Label4" cssclass="inp-lbl" runat="server" text="Data Tier:" /></td>
			    <td colspan="3"><asp:DropDownList id="cDataTierId" cssclass="inp-ddl" runat="server" onSelectedIndexChanged="cDataTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="DataTierId" DataTextField="DataTierName" /></td>
		    </tr>
		    <tr>
			    <td colspan="4" height="10px"><hr></td>
		    </tr>
		    <tr>
			    <td><asp:Label ID="Label5" cssclass="inp-lbl" runat="server" text="Database:" /></td>
			    <td><asp:DropDownList id="cSystemId" CssClass="inp-ddl" AutoPostBack="true" onSelectedIndexChanged="cSystemId_SelectedIndexChanged" DataValueField="SystemId" DataTextField="SystemName" Runat="server" /></td>
			    <td align="right"><asp:Label ID="Label6" cssclass="inp-lbl" runat="server" text="All Wizards:" /></td>
			    <td width="20px"><asp:CheckBox id="cAllWizard" cssclass="inp-chk" AutoPostBack="true" onCheckedChanged="cAllWizard_CheckedChanged" tooltip="Check here for all wizards in the listbox below to be generated." Runat="server" /></td>
		    </tr>
		    <tr>
			    <td><asp:Label ID="Label7" cssclass="inp-lbl" runat="server" text="Search:" /></td>
			    <td colspan="2"><asp:textbox id="cSearch" CssClass="inp-txt" runat="server" MaxLength="25" /></td>
			    <td><asp:ImageButton id="cSearchButton" ImageUrl="~/images/Search.gif" onClick="cSearchButton_Click" AlternateText="Click here to search for wizards with specified text on the left." Runat="server" />
		    </tr>
		    <tr>
			    <td colspan="4">
				    <asp:listbox id="cWizardList" runat="server" CssClass="inp-pic" SelectionMode="Multiple" AutoPostBack="false" rows="6" DataValueField="WizardId" DataTextField="WizardTitle" />
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
			    <asp:Button id="cGenButton" CssClass="small blue button" onClick="cGenButton_Click" runat="server" text="Create" tooltip="Click here to generate codes for selected wizard(s)." />
		    </span>
        </div>
        <div class="wizard-footer">
            <span>Note: When ready for testing, <a href="ComProgram.aspx?csy=3&id=257">click here to compile all programs</a> once.</span>
        </div>
    </div>
</div>
</div>
<input id="bErrNow" type="hidden" runat="server" />
<input id="bInfoNow" type="hidden" runat="server" />
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
