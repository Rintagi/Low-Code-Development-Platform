<%@ Page language="c#" Inherits="RO.Web.UpLoad" CodeFile="UpLoad.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="Module" TagName="Footer" Src="modules/FooterModule.ascx" %>
<%@ Register TagPrefix="Module" TagName="DnLoad" Src="modules/UpLoadModule.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Upload Image</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="GetCss.aspx" type="text/css" rel="Stylesheet">
	</head>
	<body>
		<form id="DnLoadForm" method="post" runat="server">
			<table align="center" cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
				<tr>
					<td><Module:DnLoad id="ModuleUpLoad" runat="server" /></td>
				</tr>
			</table>
		</form>
	</body>
</html>