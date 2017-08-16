<%@ Page language="c#" Inherits="RO.Web.Msg" CodeFile="Msg.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="Module" TagName="Msg" Src="modules/MsgModule.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Robocoder Messaging</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="Stylesheet" href="GetCss.aspx" type="text/css">
	</head>
	<body bgcolor="gainsboro">
		<form id="MsgForm" method="post" runat="server">
			<Module:Msg id="ModuleMsg" runat="server" />
		</form>
	</body>
</html>