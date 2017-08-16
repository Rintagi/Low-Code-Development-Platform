<%@ Control Language="c#" Inherits="RO.Web.GenConvSqlModule" CodeFile="GenConvSqlModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
    function beginRequestHandler() { ShowProgress(); document.body.style.cursor = 'wait'; }
    function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ChkErrNow)
	function ChkErrNow() {if (document.getElementById('<%=bErrNow.ClientID%>').value == 'Y') {window.open('Msg.aspx?typ=E','ErrMsg','resizable=yes,scrollbars=yes,width=600,height=150,modal=yes');return false;}}
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
    <div class="r-td rc-1-12">
        <div>
		    <table cellspacing="0px" cellpadding="0px" border="0px" width="100%">
            <tr>
	            <td colspan="3"><asp:TextBox TextMode="MultiLine" id="cSqlFr" CssClass="inp-txt" style="height:180px" runat="server" /></td>
            </tr>
            <tr>
                <td style="width:120px"><asp:Label CssClass="inp-lbl" runat="server" text="Target Data Tier:" /></td>
	            <td><asp:DropDownList id="cDataTierId" CssClass="inp-ddl" runat="server" AutoPostBack="false" DataValueField="DataTierId" DataTextField="DataTierName" /></td>
	            <td align="right"><asp:Button id="cGenButton" CssClass="small blue button" onClick="cGenButton_Click" runat="server" text="Convert" tooltip="Click here to convert the above Microsoft SQL to Sybase syntax." /></td>
            </tr>
            <tr>
	            <td colspan="3"><asp:TextBox TextMode="MultiLine" id="cSqlTo" CssClass="inp-txt" style="height:180px" runat="server" /></td>
            </tr>
            </table>
        </div>
    </div>
</div>
</div>
<br />
<input id="bErrNow" type="hidden" runat="server" />
</ContentTemplate></asp:UpdatePanel>
