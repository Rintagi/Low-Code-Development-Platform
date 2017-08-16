<%@ Control Language="c#" Inherits="RO.Web.MsgModule" CodeFile="MsgModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<table width="60%" style="position:relative;margin:10% 0 0 20%;" border="0" cellpadding="0" cellspacing="0">
<tr><td>
<asp:panel id="cMsgPanel" width="100%" BorderWidth="10px" style="border-color:Silver;" runat="server" visible="true">
	<table border="0" cellspacing="5px" cellpadding="10px">
	<tr valign="top">
		<td width="1%"><asp:Image id="cImage" ImageUrl="~/images/info.gif" runat="server" /></td>
		<td align="center"><asp:label id="cMessage" EnableViewState="false" runat="server" font-size="12" font-bold="true" /></td>
	</tr>
	<tr>
		<td colspan="2" align="center"><input onclick="javascript:window.history.back(-1);" type="button" value="OK" style="width:100px;cursor:pointer;" />&nbsp;<input onclick="javascript:window.print();" type="button" value="PRINT" style="width:100px;cursor:pointer;" /></td>
	</tr>
	</table>
</asp:panel>
<asp:panel id="cTechPanel" Width="100%" runat="server" visible="false">
	<br /><br /><hr />
	<table border="0" cellspacing="5px" cellpadding="4">
	<tr>
		<td><asp:label id="cStackTrace" EnableViewState="false" runat="server" /></td>
	</tr>
	</table>
</asp:panel>
</td></tr>
</table>