<%@ Page language="c#" Inherits="RO.Web.ImportSchema" CodeFile="ImportSchema.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="ajwc" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="Module" TagName="ImportSchemaModule" Src="modules/ImportSchemaModule.ascx" %>
<!doctype html>
<html>
	<head>
		<title>Import Schema</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link rel="Stylesheet" href="GetCss.aspx" type="text/css" />
        <link type="text/css" rel="Stylesheet" href="css/_screen.css" />
	</head>
	<body>
		<form id="ImportSchemaForm" method="post" runat="server">
			<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" AsyncPostBackTimeout="3600" runat="server">
				<Scripts><asp:ScriptReference Path="~/scripts/ajaxfix.js" /></Scripts>
			</asp:ScriptManager>
			<Module:ImportSchemaModule id="ImportSchemaModule" runat="server" visible="false" />
			<table width="100%" border="0" cellspacing="0" cellpadding="4">
				<tr>
					<td align="center"><img src="images/SchemaH.gif"></td>
				</tr>
				<tr valign="top">
					<td align="center"><asp:label id="cSchema" EnableViewState="false" runat="server" font-size="12" /></td>
				</tr>
				<tr>
					<td align="center"><input onclick="javascript:window.print();" type="button" value="PRINT" class="small blue button"><asp:Button ID="cTmplDownload" runat="server" OnClick="cTmplDownload_Clicked" Text="Download Template" class="small blue button" /></td>
				</tr>
			</table>
            <asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
		</form>
	</body>
</HTML>